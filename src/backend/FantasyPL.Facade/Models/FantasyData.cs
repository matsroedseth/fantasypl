using System.Text.Json.Serialization;

namespace FantasyPL.Facade.Models
{
    public class FantasyData
    {
        [JsonPropertyName("events")]
        public List<FantasyEvent> Events { get; set; }

        [JsonPropertyName("teams")]
        public List<PremierLeagueTeam> Teams { get; set; }

        [JsonPropertyName("elements")]
        public List<PremierLeaguePlayer> Players { get; set; }
    }

    public class PremierLeagueTeam
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }
    }

    public class FantasyEvent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("finished")]
        public bool Finished { get; set; }

        [JsonPropertyName("is_previous")]
        public bool IsPrevious { get; set; }

        [JsonPropertyName("is_current")]
        public bool IsCurrent { get; set; }

        [JsonPropertyName("is_next")]
        public bool IsNext { get; set; }

        [JsonPropertyName("deadline_time")]
        public DateTimeOffset Deadline { get; set; }
    }

    public class Fixture
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("team_h")]
        public int HomeTeamId { get; set; }

        [JsonPropertyName("team_a")]
        public int AwayTeamId { get; set; }
    }


    public class PremierLeaguePlayer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("second_name")]
        public string LastName { get; set; }

        [JsonPropertyName("web_name")]
        public string Name { get; set; }

        [JsonPropertyName("now_cost")]
        public int Price { get; set; }

        [JsonPropertyName("team")]
        public int TeamId { get; set; }

        [JsonPropertyName("element_type")]
        public int ElementType { get; set; }
    }
}