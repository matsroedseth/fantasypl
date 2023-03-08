namespace FantasyPL.Dtos;

public record ManagerPicksDataDto(
    ManagerInfoDto ManagerInfo,
    Chip? ActiveChip,
    TeamInfoDto TeamInfo,
    List<PlayerPickDto> Players,
    List<TransferDto> Transfers
);

public record LiveDataDto(
    int ManagerId,
    int Points
);

public record ManagerInfoDto(
    int Id,
    string FirstName,
    string LastName,
    int OverallPoints,
    int OverallRank,
    int GameWeekPoints,
    int? GameWeekRank,
    string TeamName);

public record TeamInfoDto(
    int ITB,
    int TeamValue,
    int Transfers,
    int TransferCost,
    int PointsBenched);

public record PlayerPickDto(
    int Id,
    string FirstName,
    string LastName,
    decimal Price,
    int TeamId,
    int Position,
    int Multiplier,
    bool IsCaptain,
    bool IsViceCaptain);

public record TransferDto(
    PremierLeaguePlayerDto TransferredIn,
    PremierLeaguePlayerDto TransferredOut
);

public enum Chip
{
    Wildcard,
    Freehit,
    TC
}