using AutoMapper;

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
        CreateMap<Facade.Models.Manager, Api.Models.Manager>();
        CreateMap<Facade.Models.LeagueData, Api.Models.LeagueData>();
        CreateMap<Facade.Models.League, Api.Models.League>();
        CreateMap<Facade.Models.StandingData, Api.Models.StandingData>();
        CreateMap<Facade.Models.Result, Api.Models.Result>();

        // Model to dto
        CreateMap<Api.Models.Fixture, Dtos.FixtureDto>();
        CreateMap<Api.Models.FantasyData, Dtos.FantasyDataDto>();
        CreateMap<Api.Models.FantasyEvent, Dtos.FantasyEventDto>();
        CreateMap<Api.Models.PremierLeagueTeam, Dtos.PremierLeagueTeamDto>();
        CreateMap<Api.Models.PremierLeaguePlayer, Dtos.PremierLeaguePlayerDto>();
        CreateMap<Api.Models.Manager, Dtos.ManagerDto>();
        CreateMap<Api.Models.LeagueData, Dtos.LeagueDataDto>();
        CreateMap<Api.Models.League, Dtos.LeagueDto>();
        CreateMap<Api.Models.StandingData, Dtos.StandingDataDto>();
        CreateMap<Api.Models.Result, Dtos.ResultDto>();

    }
}