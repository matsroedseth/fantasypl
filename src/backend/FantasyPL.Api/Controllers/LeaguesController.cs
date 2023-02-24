using AutoMapper;
using FantasyPL.Api.Services;
using FantasyPL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPL.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaguesController : ControllerBase
{
    private readonly IFantasyService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<LeaguesController> _logger;

    public LeaguesController(
        IFantasyService service,
        IMapper mapper,
        ILogger<LeaguesController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("{leagueId}")]
    public async Task<IActionResult> GetLeagueById(int leagueId)
    {
        var result = await _service.GetLeagueById(leagueId);
        return result != null ? Ok(_mapper.Map<LeagueDataDto>(result)) : NotFound();
    }

    [HttpGet("{leagueId}/standings")]
    public async Task<IActionResult> GetLeagueStandingsById(int leagueId)
    {
        var result = await _service.GetLeagueWithStandings(leagueId);
        return result != null ? Ok(_mapper.Map<LeagueWithStandingsDto>(result)) : NotFound();
    }
}