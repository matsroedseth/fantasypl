namespace FantasyPL.Dtos;

public record LeagueDataDto(
    LeagueDto League,
    StandingDataDto Standing
);

public record LeagueDto(
    int Id,
    string Name
);

public record StandingDataDto(
    bool HasNext,
    int Page,
    List<ResultDto> Results);

public record ResultDto(
    int ManagerId,
    int GameWeekPoints,
    int CurrentRank,
    int LastRank
);