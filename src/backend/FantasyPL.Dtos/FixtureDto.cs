namespace FantasyPL.Dtos;

public record FixtureDto(int Id, int Code, PremierLeagueTeamDto? HomeTeam, PremierLeagueTeamDto? AwayTeam);