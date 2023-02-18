namespace FantasyPL.Api.Models;

public record Manager
(
    int Id,
    string FirstName,
    string LastName,
    int OverallPoints,
    int OverallRank,
    int CurrentGameWeek,
    int GameWeekPoints,
    int GameWeekRank,
    string TeamName
);