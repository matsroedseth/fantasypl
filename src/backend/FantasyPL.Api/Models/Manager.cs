using FantasyPL.Domain.Enums;

namespace FantasyPL.Api.Models;

public record ManagerPicksData(
    ManagerInfo ManagerInfo,
    Chip? ActiveChip,
    TeamInfo TeamInfo,
    List<PlayerPick> Players,
    List<Transfer> Transfers
);

public record LiveData(
    int ManagerId,
    int Points
);

public record ManagerInfo(
    int Id,
    string FirstName,
    string LastName,
    int OverallPoints,
    int OverallRank,
    int GameWeekPoints,
    int? GameWeekRank,
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
    string Name,
    decimal Price,
    int TeamId,
    int Position,
    int Multiplier,
    bool IsCaptain,
    bool IsViceCaptain,
    Position ElementType);

public record Transfer(
    PremierLeaguePlayer TransferredIn,
    PremierLeaguePlayer TransferredOut
);