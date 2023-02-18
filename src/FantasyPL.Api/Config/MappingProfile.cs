using AutoMapper;
using FantasyPL.Api.Models;
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
        CreateMap<Facade.Models.Manager, Api.Models.Manager>();

        // Model to dto
        CreateMap<Api.Models.Fixture, FantasyPL.Dtos.FixtureDto>();
        CreateMap<Api.Models.FantasyData, FantasyPL.Dtos.FantasyDataDto>();
        CreateMap<Api.Models.FantasyEvent, FantasyPL.Dtos.FantasyEventDto>();
        CreateMap<Api.Models.PremierLeagueTeam, FantasyPL.Dtos.PremierLeagueTeamDto>();
        CreateMap<Api.Models.PremierLeaguePlayer, FantasyPL.Dtos.PremierLeaguePlayerDto>();
        CreateMap<Api.Models.Manager, FantasyPL.Dtos.ManagerDto>();

    }
}