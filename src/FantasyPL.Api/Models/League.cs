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
    List<ResultWithManager> Standing
);

public record ResultWithManager(
    Manager manager,
    int GameWeekPoints,
    int CurrentRank,
    int LastRank
);