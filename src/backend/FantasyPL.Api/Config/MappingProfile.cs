using AutoMapper;
using FantasyPL.Domain.Enums;
using FantasyPL.Facade.Models;

namespace FantasyPL.Api.Config;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Facade to model
        CreateMap<Facade.Models.Fixture, Api.Models.Fixture>();
        CreateMap<Facade.Models.FantasyData, Api.Models.FantasyData>();
        CreateMap<Facade.Models.FantasyEvent, Api.Models.FantasyEvent>();
        CreateMap<Facade.Models.PremierLeagueTeam, Api.Models.PremierLeagueTeam>();
        CreateMap<Facade.Models.PremierLeaguePlayer, Api.Models.PremierLeaguePlayer>();
        CreateMap<Facade.Models.LeagueData, Api.Models.LeagueData>();
        CreateMap<Facade.Models.League, Api.Models.League>();
        CreateMap<Facade.Models.StandingData, Api.Models.StandingData>();
        CreateMap<Facade.Models.Result, Api.Models.Result>();

        CreateMap<string, Domain.Enums.Chip?>().ConvertUsing(val => MapChip(val));
        CreateMap<int, Domain.Enums.Position>().ConvertUsing(val => MapPosition(val));
        CreateMap<Facade.Models.ManagerInfo, Api.Models.ManagerInfo>();
        CreateMap<Facade.Models.TeamInfo, Api.Models.TeamInfo>();
        CreateMap<Facade.Models.PlayerPick, Api.Models.PlayerPick>();

        // Model to dto
        CreateMap<Api.Models.Fixture, Dtos.FixtureDto>();
        CreateMap<Api.Models.FantasyData, Dtos.FantasyDataDto>();
        CreateMap<Api.Models.FantasyEvent, Dtos.FantasyEventDto>();
        CreateMap<Api.Models.PremierLeagueTeam, Dtos.PremierLeagueTeamDto>();
        CreateMap<Api.Models.PremierLeaguePlayer, Dtos.PremierLeaguePlayerDto>();
        CreateMap<Api.Models.ManagerInfo, Dtos.ManagerInfoDto>();
        CreateMap<Api.Models.LeagueData, Dtos.LeagueDataDto>();
        CreateMap<Api.Models.League, Dtos.LeagueDto>();
        CreateMap<Api.Models.StandingData, Dtos.StandingDataDto>();
        CreateMap<Api.Models.Result, Dtos.ResultDto>();
        CreateMap<Api.Models.LeagueWithStandings, Dtos.LeagueWithStandingsDto>();
        CreateMap<Api.Models.ResultWithManager, Dtos.ResultWithManagerDto>();
        CreateMap<Api.Models.PlayerPick, Dtos.PlayerPickDto>();
        CreateMap<Api.Models.CaptaincyPick, Dtos.CaptaincyPickDto>();
        CreateMap<Api.Models.Captain, Dtos.CaptainDto>();

        CreateMap<Api.Models.ManagerPicksData, Dtos.ManagerPicksDataDto>();
        CreateMap<Api.Models.ManagerInfo, Dtos.ManagerInfoDto>();
        CreateMap<Api.Models.TeamInfo, Dtos.TeamInfoDto>();
        CreateMap<Api.Models.PlayerPick, Dtos.PlayerPickDto>();
        CreateMap<Api.Models.Transfer, Dtos.TransferDto>();
        CreateMap<Api.Models.LiveData, Dtos.LiveDataDto>();

    }

    private Chip? MapChip(string value)
    {
        if (string.Equals(value, "wildcard", StringComparison.InvariantCultureIgnoreCase))
        {
            return Chip.Wildcard;
        }
        else if (string.Equals(value, "freehit", StringComparison.InvariantCultureIgnoreCase))
        {
            return Chip.Freehit;
        }
        else if (string.Equals(value, "3xc", StringComparison.InvariantCultureIgnoreCase))
        {
            return Chip.TC;
        }

        return null;
    }

    private Position MapPosition(int value)
    {
        switch (value)
        {
            case 1:
                return Position.Goalkeeper;
            case 2:
                return Position.Defender;
            case 3:
                return Position.Midfielder;
            case 4:
            default:
                return Position.Attacker;

        }
    }
}