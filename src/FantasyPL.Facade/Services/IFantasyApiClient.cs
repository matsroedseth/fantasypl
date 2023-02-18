using System.Text.Json;
using FantasyPL.Facade.Models;

namespace FantasyPL.Facade.Clients;

public interface IFantasyApiClient
{
    Task<FantasyData> GetGameData();
    Task<IEnumerable<Fixture>> GetFixtures();
    Task<IEnumerable<Fixture>> GetFixturesByGameweekNumber(int gameweek);
    Task<Manager> GetManagerById(int managerId);
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

    public async Task<Manager> GetManagerById(int managerId)
    {
        var result = await _httpService.GetAsync<Manager>(new Uri(new Uri(_baseUrl), $"entry/{managerId}/"));
        return result;
    }
}
