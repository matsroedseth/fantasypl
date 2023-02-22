using System.Text.Json.Serialization;

namespace FantasyPL.Facade.Models;

public class LeagueData
{
    [JsonPropertyName("league")]
    public League League { get; set; }

    [JsonPropertyName("standings")]
    public StandingData Standing { get; set; }
}

public class League
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class StandingData
{
    [JsonPropertyName("has_next")]
    public bool HasNext { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<Result> Results { get; set; }
}

public class Result
{
    [JsonPropertyName("entry")]
    public int ManagerId { get; set; }

    [JsonPropertyName("event_total")]
    public int GameWeekPoints { get; set; }

    [JsonPropertyName("rank")]
    public int CurrentRank { get; set; }

    [JsonPropertyName("last_rank")]
    public int LastRank { get; set; }
}