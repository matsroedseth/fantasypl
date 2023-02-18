using AutoMapper;
using FantasyPL.Api.Services;
using FantasyPL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPL.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IFantasyService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<PlayersController> _logger;

    public PlayersController(
        IFantasyService service,
        IMapper mapper,
        ILogger<PlayersController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllPlayers()
    {
        var result = await _service.GetAllPlayers();
        var response = new List<PremierLeaguePlayerDto>();
        foreach (var player in result)
        {
            response.Add(_mapper.Map<PremierLeaguePlayerDto>(player));
        }
        return response.Any() ? Ok(response) : NoContent();
    }

    [HttpGet("{teamId}")]
    public async Task<IActionResult> GetAllPlayersForTeam(int teamId)
    {
        var result = await _service.GetAllPlayersByTeamId(teamId);
        var response = new List<PremierLeaguePlayerDto>();
        foreach (var player in result)
        {
            response.Add(_mapper.Map<PremierLeaguePlayerDto>(player));
        }
        return response.Any() ? Ok(response) : NoContent();
    }
}
