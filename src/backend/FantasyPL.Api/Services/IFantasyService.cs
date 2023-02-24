using AutoMapper;
using FantasyPL.Facade.Clients;
using FantasyPL.Api.Models;

namespace FantasyPL.Api.Services;

public interface IFantasyService
{
    Task<FantasyData> GetGameData();
    Task<List<Fixture>> GetAllFixtures();
    Task<List<Fixture>> GetFixturesByGameweekNumber(int gameweek);
    Task<List<PremierLeaguePlayer>> GetAllPlayers();
    Task<List<PremierLeaguePlayer>> GetAllPlayersByTeamId(int teamId);
    Task<ManagerInfo> GetManagerById(int managerId);
    Task<LeagueData> GetLeagueById(int leagueId);
    Task<LeagueWithStandings> GetLeagueWithStandings(int leagueId);
    Task<ManagerPicksData> GetManagerPicksByIdAndGameWeek(int managerId, int gameweek);
    Task<List<PlayerPick>> GetPlayersByManagerIdAndGameweekNumber(int teamId, int gameweek);
    Task<List<FantasyEvent>> GetAllGameWeeks();
    Task<FantasyEvent> GetPreviousGameWeek();
    Task<FantasyEvent> GetCurrentGameWeek();
    Task<FantasyEvent> GetNextGameWeek();
}

public class FantasyService : IFantasyService
{
    private IFantasyApiClient _api;
    private IMapper _mapper;
    private ILogger<FantasyService> _logger;

    public FantasyService(
        IFantasyApiClient api,
        IMapper mapper,
        ILogger<FantasyService> logger
    )
    {
        _api = api;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<FantasyData> GetGameData()
    {
        var result = await _api.GetGameData();
        return _mapper.Map<FantasyData>(result);
    }

    public async Task<List<FantasyEvent>> GetAllGameWeeks()
    {
        var result = await _api.GetGameData();
        return result.Events.Select(e => _mapper.Map<FantasyEvent>(e)).ToList();
    }

    public async Task<FantasyEvent> GetPreviousGameWeek()
    {
        var result = await _api.GetGameData();
        var previousGameWeek = result.Events.Where(e => e.IsPrevious).FirstOrDefault();
        return previousGameWeek != null
                ? _mapper.Map<FantasyEvent>(previousGameWeek)
                : null;
    }

    public async Task<FantasyEvent> GetCurrentGameWeek()
    {
        var result = await _api.GetGameData();
        var currentGameWeek = result.Events.Where(e => e.IsCurrent).FirstOrDefault();
        return currentGameWeek != null
                ? _mapper.Map<FantasyEvent>(currentGameWeek)
                : null;
    }

    public async Task<FantasyEvent> GetNextGameWeek()
    {
        var result = await _api.GetGameData();
        var nextGameWeek = result.Events.Where(e => e.IsNext).FirstOrDefault();
        return nextGameWeek != null
                ? _mapper.Map<FantasyEvent>(nextGameWeek)
                : null;
    }

    public async Task<List<PremierLeaguePlayer>> GetAllPlayers()
    {
        var gameData = await _api.GetGameData();
        return gameData.Players.Select(p => _mapper.Map<PremierLeaguePlayer>(p)).ToList();
    }

    public async Task<List<PremierLeaguePlayer>> GetAllPlayersByTeamId(int teamId)
    {
        var gameData = await _api.GetGameData();
        return gameData.Players.Where(p => p.TeamId == teamId).Select(p => _mapper.Map<PremierLeaguePlayer>(p)).ToList();
    }

    public async Task<List<Fixture>> GetAllFixtures()
    {
        var gameData = await _api.GetGameData();
        var fixtures = await _api.GetFixtures();

        return MergeFixturesWithData(gameData, fixtures);
    }

    public async Task<List<Fixture>> GetFixturesByGameweekNumber(int gameweek)
    {
        var gameData = await _api.GetGameData();
        var fixtures = await _api.GetFixturesByGameweekNumber(gameweek);
        return MergeFixturesWithData(gameData, fixtures);
    }

    public async Task<ManagerInfo> GetManagerById(int managerId)
    {
        var manager = await _api.GetManagerById(managerId);
        return _mapper.Map<ManagerInfo>(manager);
    }

    public async Task<ManagerPicksData> GetManagerPicksByIdAndGameWeek(int managerId, int gameweek)
    {
        var managerPicks = await _api.GetManagerPicksByIdAndGameWeekNumber(managerId, gameweek);
        var manager = await _api.GetManagerById(managerId);
        var playerData = (await _api.GetGameData()).Players;

        return new ManagerPicksData(
            new ManagerInfo(manager.Id, manager.FirstName, manager.LastName, manager.OverallPoints, manager.OverallRank, manager.GameWeekPoints, manager.GameWeekRank, manager.TeamName),
            _mapper.Map<Chip?>(managerPicks.ActiveChip),
            _mapper.Map<TeamInfo>(managerPicks.TeamInfo),
            managerPicks.Players.Select(p => MergePlayerWithPlayerData(p, playerData.Where(d => d.Id == p.Element).FirstOrDefault())).ToList()
        );
    }

    public async Task<List<PlayerPick>> GetPlayersByManagerIdAndGameweekNumber(int managerId, int gameweek)
    {
        var managerPicks = await _api.GetManagerPicksByIdAndGameWeekNumber(managerId, gameweek);
        var playerData = (await _api.GetGameData()).Players;

        return managerPicks.Players.Select(p => MergePlayerWithPlayerData(p, playerData.Where(d => d.Id == p.Element).FirstOrDefault())).ToList();
    }

    private PlayerPick MergePlayerWithPlayerData(Facade.Models.PlayerPick pick, Facade.Models.PremierLeaguePlayer playerData)
        => new PlayerPick(
            playerData.Id,
            playerData.FirstName,
            playerData.LastName,
            playerData.Price,
            playerData.TeamId,
            pick.Position,
            pick.Multiplier,
            pick.IsCaptain,
            pick.IsViceCaptain);

    public async Task<LeagueData> GetLeagueById(int leagueId)
    {
        var league = await _api.GetLeagueById(leagueId);
        return _mapper.Map<LeagueData>(league);
    }

    public async Task<LeagueWithStandings> GetLeagueWithStandings(int leagueId)
    {
        var currentGameWeek = await GetCurrentGameWeek();
        var leagueData = await GetLeagueById(leagueId);
        var playerData = (await _api.GetGameData()).Players.ToDictionary(p => p.Id);
        var managers = await GetManagerPicksFromStandings(leagueData, playerData, currentGameWeek.Id);

        var data = new LeagueWithStandings(
                        new League(leagueData.League.Id, leagueData.League.Name),
                        leagueData.Standing.Results.Select(r =>
                                    PopulateStandingWithManagerData(r, managers[r.ManagerId])).ToList()
                                    );
        return data;
    }

    private async Task<Dictionary<int, ManagerPicksData>> GetManagerPicksFromStandings(LeagueData leagueData, Dictionary<int, Facade.Models.PremierLeaguePlayer> playerData, int gameweek)
    {
        var managerIds = leagueData.Standing.Results.Select(result => result.ManagerId);
        var managers = await Task.WhenAll(managerIds.Select(managerId => _api.GetManagerById(managerId)));
        var managerPicks = new Dictionary<int, Facade.Models.ManagerPicksData>();
        foreach (var managerId in managerIds)
        {
            var managerPick = await _api.GetManagerPicksByIdAndGameWeekNumber(managerId, gameweek);
            managerPicks.Add(managerId, managerPick);
        }

        return managerIds.Select(id => new ManagerPicksData(
                    _mapper.Map<ManagerInfo>(managers.Where(m => m.Id == id).FirstOrDefault()),
                    _mapper.Map<Chip?>(managerPicks[id].ActiveChip),
                    _mapper.Map<TeamInfo>(managerPicks[id].TeamInfo),
                    managerPicks[id].Players.Select(p => MergePlayerWithPlayerData(p, playerData[p.Element])).ToList())).ToDictionary(m => m.ManagerInfo.Id);
    }

    private ResultWithManager PopulateStandingWithManagerData(Result result, ManagerPicksData manager)
    => new ResultWithManager(
            manager.ManagerInfo,
            manager.ActiveChip,
            manager.TeamInfo,
            manager.Players,
            result.GameWeekPoints,
            result.CurrentRank,
            result.LastRank);

    private static List<Fixture> MergeFixturesWithData(Facade.Models.FantasyData gameData, IEnumerable<Facade.Models.Fixture> fixtures)
        => fixtures.Select(f =>
                    new Fixture(
                        Id: f.Id,
                        Code: f.Code,
                        HomeTeam: ToTeam(f.HomeTeamId, gameData),
                        AwayTeam: ToTeam(f.AwayTeamId, gameData))).ToList();

    private static PremierLeagueTeam ToTeam(int id, Facade.Models.FantasyData gameData)
    {
        var team = gameData.Teams.Where(t => t.Id == id).FirstOrDefault();
        return team != null
            ? new PremierLeagueTeam(team.Id, team.Name, team.ShortName)
            : null;
    }
}