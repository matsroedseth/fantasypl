using System.Text.Json.Serialization;

namespace FantasyPL.Facade.Models;

public class LiveStats
{
    [JsonPropertyName("elements")]
    public List<Element> Elements { get; set; }
}

public class Element
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("stats")]
    public Stats Stats { get; set; }
}
public class Stats
{
    [JsonPropertyName("bonus")]
    public long Bonus { get; set; }

    [JsonPropertyName("total_points")]
    public long TotalPoints { get; set; }
}