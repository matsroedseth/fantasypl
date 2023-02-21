namespace FantasyPL.Dtos;

public record FantasyDataDto(List<FantasyEventDto> Events, List<PremierLeagueTeamDto> Teams);

public record FantasyEventDto(
    int Id,
    string Name,
    bool Finished,
    bool IsPrevious,
    bool IsCurrent,
    bool IsNext,
    DateTimeOffset Deadline);

public record PremierLeagueTeamDto(int Id, string Name);

public record PremierLeaguePlayerDto(int Id, string FirstName, string LastName, decimal Price, int TeamId);