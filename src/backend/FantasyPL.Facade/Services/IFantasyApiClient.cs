using System.Text.Json;
using FantasyPL.Facade.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

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
    Task<LiveStats> GetLiveData(int gameweek);
}

public class FantasyApiClient : IFantasyApiClient
{
    private const string GameDataCacheKey = "gameData";
    private const string FixturesCacheKey = "fixtures";
    private const string ManagerCacheBaseKey = "managerPicks";
    private readonly IHttpService _httpService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<FantasyApiClient> _logger;
    private readonly string _baseUrl;
    private MemoryCacheEntryOptions _cacheEntryOptions;

    public FantasyApiClient(
        IHttpService httpService,
        IMemoryCache cache,
        ILogger<FantasyApiClient> logger)
    {
        _httpService = httpService;
        _cache = cache;
        _cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
        _logger = logger;
        _baseUrl = "https://fantasy.premierleague.com/api/";
    }

    public async Task<FantasyData> GetGameData()
    {
        if (_cache.TryGetValue(GameDataCacheKey, out FantasyData data))
        {
            _logger.LogInformation("GameData found in cache.");
        }
        else
        {
            data = await _httpService.GetAsync<FantasyData>(new Uri(new Uri(_baseUrl), $"bootstrap-static/"));
            _cache.Set(GameDataCacheKey, data, _cacheEntryOptions);
        }

        return data;
    }

    public async Task<IEnumerable<Fixture>> GetFixtures()
    {
        if (_cache.TryGetValue(FixturesCacheKey, out IEnumerable<Fixture> data))
        {
            _logger.LogInformation("Fixtures found in cache.");
        }
        else
        {
            data = await _httpService.GetAsync<IEnumerable<Fixture>>(new Uri(new Uri(_baseUrl), $"fixtures"));
            _cache.Set(FixturesCacheKey, data, _cacheEntryOptions);
        }
        return data;
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

        var cacheKey = $"{ManagerCacheBaseKey}_{managerId}_{gameweek}";
        if (_cache.TryGetValue(cacheKey, out ManagerPicksData data))
        {
            _logger.LogInformation("ManagerPicks found in cache.");
        }
        else
        {
            data = await _httpService.GetAsync<ManagerPicksData>(new Uri(new Uri(_baseUrl), $"entry/{managerId}/event/{gameweek}/picks/"));
            _cache.Set(cacheKey, data, _cacheEntryOptions);
        }
        return data;
    }

    public async Task<List<Transfer>> GetTransfersByManagerIdAndGameWeekNumber(int managerId, int gameweek)
    {
        var result = await _httpService.GetAsync<List<Transfer>>(new Uri(new Uri(_baseUrl), $"entry/{managerId}/transfers"));
        return result.Where(t => t.Event == gameweek).ToList();
    }

    public async Task<LiveStats> GetLiveData(int gameweek)
    => await _httpService.GetAsync<LiveStats>(new Uri(new Uri(_baseUrl), $"event/{gameweek}/live"));
}
