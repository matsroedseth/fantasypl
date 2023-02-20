namespace FantasyPL.Dtos;

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