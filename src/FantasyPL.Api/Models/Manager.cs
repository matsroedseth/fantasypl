namespace FantasyPL.Api.Models;

public record ManagerPicksData(
    ManagerInfo ManagerInfo,
    Chip? ActiveChip,
    TeamInfo TeamInfo,
    List<PlayerPick> Players
);

public record ManagerInfo(
    int Id,
    string FirstName,
    string LastName,
    int OverallPoints,
    int OverallRank,
    int CurrentGameWeek,
    int GameWeekPoints,
    int GameWeekRank,
    string TeamName);

public record TeamInfo(
    int ITB,
    int TeamValue,
    int Transfers,
    int TransferCost,
    int PointsBenched);

public record PlayerPick(
    int Id,
    string FirstName,
    string LastName,
    decimal Price,
    int TeamId,
    int Position,
    int Multiplier,
    bool IsCaptain,
    bool IsViceCaptain);

public enum Chip
{
    Wildcard,
    Freehit,
    TC
}