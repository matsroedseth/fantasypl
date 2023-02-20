using AutoMapper;
using FantasyPL.Facade.Clients;
using FantasyPL.Api.Models;

namespace FantasyPL.Api.Services;

public interface IFantasyService
{
    Task<FantasyData> GetGameData();
    Task<IEnumerable<Fixture>> GetAllFixtures();
    Task<IEnumerable<Fixture>> GetFixturesByGameweekNumber(int gameweek);
    Task<IEnumerable<PremierLeaguePlayer>> GetAllPlayers();
    Task<IEnumerable<PremierLeaguePlayer>> GetAllPlayersByTeamId(int teamId);
    Task<Manager> GetManagerById(int managerId);
    Task<LeagueData> GetLeagueById(int leagueId);
    Task<LeagueWithStandings> GetLeagueWithStandings(int leagueId);
    Task<IEnumerable<PlayerPick>> GetAllPlayersByManagerIdAndGameweekNumber(int teamId, int gameweek);
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

    public async Task<IEnumerable<PremierLeaguePlayer>> GetAllPlayers()
    {
        var gameData = await _api.GetGameData();
        return gameData.Players.Select(p => _mapper.Map<PremierLeaguePlayer>(p));
    }

    public async Task<IEnumerable<PremierLeaguePlayer>> GetAllPlayersByTeamId(int teamId)
    {
        var gameData = await _api.GetGameData();
        return gameData.Players.Where(p => p.TeamId == teamId).Select(p => _mapper.Map<PremierLeaguePlayer>(p));
    }

    public async Task<IEnumerable<Fixture>> GetAllFixtures()
    {
        var gameData = await _api.GetGameData();
        var fixtures = await _api.GetFixtures();

        return MergeFixturesWithData(gameData, fixtures);
    }

    public async Task<IEnumerable<Fixture>> GetFixturesByGameweekNumber(int gameweek)
    {
        var gameData = await _api.GetGameData();
        var fixtures = await _api.GetFixturesByGameweekNumber(gameweek);
        return MergeFixturesWithData(gameData, fixtures);
    }

    public async Task<Manager> GetManagerById(int managerId)
    {
        var manager = await _api.GetManagerById(managerId);
        return _mapper.Map<Manager>(manager);
    }

    public async Task<IEnumerable<PlayerPick>> GetAllPlayersByManagerIdAndGameweekNumber(int managerId, int gameweek)
    {
        var managerPicks = await _api.GetPlayersByManagerIdAndGameWeekNumber(managerId, gameweek);
        var playerData = (await _api.GetGameData()).Players;

        return managerPicks.Players.Select(p => MergePlayerWithPlayerData(p, playerData.Where(d => d.Id == p.Element).FirstOrDefault()));
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
        var leagueData = await GetLeagueById(leagueId);
        var managers = await GetManagersFromStandings(leagueData);

        var data = new LeagueWithStandings(
                        new League(leagueData.League.Id, leagueData.League.Name),
                        leagueData.Standing.Results.Select(r => PopulateStandingWithManagerData(r, managers.Where(m => m.Id == r.ManagerId).FirstOrDefault())).ToList());

        return data;
    }

    private async Task<List<Manager>> GetManagersFromStandings(LeagueData leagueData)
    {
        var managers = new List<Manager>();

        foreach (var result in leagueData.Standing.Results)
        {
            var manager = await GetManagerById(result.ManagerId);
            managers.Add(manager);
        }

        return managers;
    }

    private ResultWithManager PopulateStandingWithManagerData(Result result, Manager manager)
    => new ResultWithManager(
            manager,
            result.GameWeekPoints,
            result.CurrentRank,
            result.LastRank);

    private static IEnumerable<Fixture> MergeFixturesWithData(Facade.Models.FantasyData gameData, IEnumerable<Facade.Models.Fixture> fixtures)
        => fixtures.Select(f =>
                    new Fixture(
                        Id: f.Id,
                        Code: f.Code,
                        HomeTeam: ToTeam(f.HomeTeamId, gameData),
                        AwayTeam: ToTeam(f.AwayTeamId, gameData)));

    private static PremierLeagueTeam? ToTeam(int id, Facade.Models.FantasyData gameData)
    {
        var team = gameData.Teams.Where(t => t.Id == id).FirstOrDefault();
        return team != null
            ? new PremierLeagueTeam(team.Id, team.Name, team.ShortName)
            : null;
    }
}