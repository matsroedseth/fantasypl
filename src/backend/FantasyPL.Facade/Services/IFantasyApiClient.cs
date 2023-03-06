using System.Text.Json;
using FantasyPL.Facade.Models;

namespace FantasyPL.Facade.Clients;

public interface IFantasyApiClient
{
    Task<FantasyData> GetGameData();
    Task<IEnumerable<Fixture>> GetFixtures();
    Task<IEnumerable<Fixture>> GetFixturesByGameweekNumber(int gameweek);
    Task<ManagerInfo> GetManagerById(int managerId);
    Task<LeagueData> GetLeagueById(int leagueId);
    Task<ManagerPicksData> GetPicksByManagerIdAndGameWeekNumber(int managerId, int gameweek);
    Task<List<Transfer>> GetTransfersByManagerIdAndGameWeekNumber(int managerId, int gameweek);
    Task<ManagerPicksData> GetManagerPicksByIdAndGameWeekNumber(int managerId, int gameweek);
}

public class FantasyApiClient : IFantasyApiClient
{
    private readonly IHttpService _httpService;
    private readonly string _baseUrl;

    public FantasyApiClient(IHttpService httpService)
    {
        _httpService = httpService;
        _baseUrl = "https://fantasy.premierleague.com/api/";
    }

    public async Task<FantasyData> GetGameData()
    {
        var result = await _httpService.GetAsync<FantasyData>(new Uri(new Uri(_baseUrl), $"bootstrap-static/"));
        return result;
    }

    public async Task<IEnumerable<Fixture>> GetFixtures()
    {
        var result = await _httpService.GetAsync<IEnumerable<Fixture>>(new Uri(new Uri(_baseUrl), $"fixtures"));
        return result;
    }

    public async Task<IEnumerable<Fixture>> GetFixturesByGameweekNumber(int gameweek)
    {
        var queryParams = new Dictionary<string, string>()
        {
            {"event", gameweek.ToString()}
        };

        var result = await _httpService.GetAsync<IEnumerable<Fixture>>(new Uri(new Uri(_baseUrl), $"fixtures"), queryParams);
        return result;
    }

    public async Task<ManagerInfo> GetManagerById(int managerId)
    {
        var result = await _httpService.GetAsync<ManagerInfo>(new Uri(new Uri(_baseUrl), $"entry/{managerId}/"));
        return result;
    }

    public async Task<LeagueData> GetLeagueById(int leagueId)
    {
        var result = await _httpService.GetAsync<LeagueData>(new Uri(new Uri(_baseUrl), $"leagues-classic/{leagueId}/standings"));
        return result;
    }

    public async Task<ManagerPicksData> GetPicksByManagerIdAndGameWeekNumber(int managerId, int gameweek)
    {
        if (gameweek < 1 || gameweek > 38)
        {
            return null;
        }

        var result = await _httpService.GetAsync<ManagerPicksData>(new Uri(new Uri(_baseUrl), $"entry/{managerId}/event/{gameweek}/picks/"));
        return result;
    }

    public async Task<List<Transfer>> GetTransfersByManagerIdAndGameWeekNumber(int managerId, int gameweek)
    {
        var result = await _httpService.GetAsync<List<Transfer>>(new Uri(new Uri(_baseUrl), $"entry/{managerId}/transfers"));
        return result.Where(t => t.Event == gameweek).ToList();
    }
}
