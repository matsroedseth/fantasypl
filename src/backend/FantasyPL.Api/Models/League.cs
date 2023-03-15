namespace FantasyPL.Api.Models;

public record LeagueData(
    League League,
    StandingData Standing
);

public record League(
    int Id,
    string Name
);

public record StandingData(
    bool HasNext,
    int Page,
    List<Result> Results);

public record Result(
    int ManagerId,
    int GameWeekPoints,
    int CurrentRank,
    int LastRank
);


public record LeagueWithStandings(
    League League,
    List<ResultWithManager> Standing,
    List<CaptaincyPick> CaptaincyPicks
);

public record ResultWithManager(
    ManagerInfo ManagerInfo,
    Chip? ActiveChip,
    TeamInfo TeamInfo,
    List<PlayerPick> Players,
    int GameWeekPoints,
    int CurrentRank,
    int LastRank);

public record CaptaincyPick(
    Captain Player,
    decimal PickedByPercentage
);

public record Captain(
    int Id,
    string FirstName,
    string LastName
);