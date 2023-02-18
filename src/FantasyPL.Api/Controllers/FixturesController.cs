using AutoMapper;
using FantasyPL.Api.Services;
using FantasyPL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPL.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FixtureController : ControllerBase
{
    private readonly IFantasyService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<FixtureController> _logger;

    public FixtureController(
        IFantasyService service,
        IMapper mapper,
        ILogger<FixtureController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetFixtures()
    {
        var result = await _service.GetAllFixtures();
        var response = new List<FixtureDto>();
        foreach (var fixture in result)
        {
            response.Add(_mapper.Map<FixtureDto>(fixture));
        }
        return response.Any() ? Ok(response) : NoContent();
    }

    [HttpGet("{gameweek}")]
    public async Task<IActionResult> GetFixtures(int gameweek)
    {
        var result = await _service.GetFixturesByGameweekNumber(gameweek);
        var response = new List<FixtureDto>();
        foreach (var fixture in result)
        {
            response.Add(_mapper.Map<FixtureDto>(fixture));
        }
        return response.Any() ? Ok(response) : NoContent();
    }
}
