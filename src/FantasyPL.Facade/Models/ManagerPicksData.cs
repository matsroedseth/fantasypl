using System.Text.Json.Serialization;

namespace FantasyPL.Facade.Models;

public class ManagerPicksData
{
    [JsonPropertyName("picks")]
    public List<PlayerPick> Players { get; set; }
}

public class PlayerPick
{
    [JsonPropertyName("element")]
    public int Element { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("multiplier")]
    public int Multiplier { get; set; }

    [JsonPropertyName("is_captain")]
    public bool IsCaptain { get; set; }

    [JsonPropertyName("is_vice_captain")]
    public bool IsViceCaptain { get; set; }
}