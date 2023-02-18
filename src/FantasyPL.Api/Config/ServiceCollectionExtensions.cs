using AutoMapper;
using FantasyPL.Facade.Clients;
using FantasyPL.Api.Services;

namespace FantasyPL.Api.Config;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFantasyService, FantasyService>();
        services.AddScoped<IFantasyApiClient, FantasyApiClient>();
        services.AddScoped<IHttpService, HttpService>();
        services.AddHttpClient();
        services.AddAutoMapper(c => c.AddProfile<MappingProfile>());

        return services;
    }
}