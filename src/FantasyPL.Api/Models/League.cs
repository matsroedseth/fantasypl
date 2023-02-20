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