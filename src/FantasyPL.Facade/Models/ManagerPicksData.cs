using System.Text.Json.Serialization;

namespace FantasyPL.Facade.Models;

public class ManagerPicksData
{
    [JsonPropertyName("active_chip")]
    public string ActiveChip { get; set; }

    [JsonPropertyName("entry_history")]
    public TeamInfo TeamInfo { get; set; }

    [JsonPropertyName("picks")]
    public List<PlayerPick> Players { get; set; }
}

public class TeamInfo
{
    [JsonPropertyName("bank")]
    public int ITB { get; set; }

    [JsonPropertyName("value")]
    public int TeamValue { get; set; }

    [JsonPropertyName("event_transfers")]
    public int Transfers { get; set; }

    [JsonPropertyName("event_transfers_cost")]
    public int TransferCost { get; set; }

    [JsonPropertyName("points_on_bench")]
    public int PointsBenched { get; set; }
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