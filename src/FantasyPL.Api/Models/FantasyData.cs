namespace FantasyPL.Api.Models;

public record FantasyData(List<FantasyEvent> Events, List<PremierLeagueTeam> Teams);

public record FantasyEvent(
    int Id,
    string Name,
    bool Finished,
    bool IsPrevious,
    bool IsCurrent,
    bool IsNext,
    DateTimeOffset Deadline);

public record PremierLeagueTeam(int Id, string Name, string ShortName);

public record PremierLeaguePlayer(int Id, string FirstName, string LastName, decimal Price, int TeamId);

public record Fixture(int Id, int Code, PremierLeagueTeam HomeTeam, PremierLeagueTeam AwayTeam);