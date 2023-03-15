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

public record LeagueWithStandingsDto(
    LeagueDto League,
    List<ResultWithManagerDto> Standing,
    List<CaptaincyPickDto> CaptaincyPicks
);

public record ResultWithManagerDto(
    ManagerInfoDto ManagerInfo,
    Chip? ActiveChip,
    TeamInfoDto TeamInfo,
    List<PlayerPickDto> Players,
    int GameWeekPoints,
    int CurrentRank,
    int LastRank);

public record CaptaincyPickDto(
    CaptainDto Player,
    int PickedByPercentage
);

public record CaptainDto(
    int Id,
    string FirstName,
    string LastName
);