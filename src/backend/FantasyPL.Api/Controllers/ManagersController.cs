using AutoMapper;
using FantasyPL.Api.Services;
using FantasyPL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPL.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManagersController : ControllerBase
{
    private readonly IFantasyService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<ManagersController> _logger;

    public ManagersController(
        IFantasyService service,
        IMapper mapper,
        ILogger<ManagersController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("{managerId}")]
    public async Task<IActionResult> GetManagerById(int managerId)
    {
        var result = await _service.GetManagerById(managerId);
        return result != null ? Ok(_mapper.Map<ManagerInfoDto>(result)) : NotFound();
    }

    [HttpGet("{managerId}/picks")]
    public async Task<IActionResult> GetManagerPicksByGameweek(int managerId, [FromQuery] int gameweek)
    {
        if (gameweek <= 0 || gameweek > 38)
        {
            return BadRequest("QueryParam 'gameweek' should be in range 1-38");
        }
        else if (gameweek > (await _service.GetCurrentGameWeek()).Id)
        {
            return BadRequest("You are asking for info about the future");
        }

        var result = await _service.GetManagerPicksByIdAndGameWeek(managerId, gameweek);
        return result != null ? Ok(_mapper.Map<ManagerPicksDataDto>(result)) : NotFound();
    }

    [HttpGet("{managerId}/players")]
    public async Task<IActionResult> GetPlayersByGameweek(int managerId, [FromQuery] int gameweek)
    {
        if (gameweek <= 0 || gameweek > 38)
        {
            return BadRequest("QueryParam 'gameweek' should be in range 1-38");
        }
        else if (gameweek > (await _service.GetCurrentGameWeek()).Id)
        {
            return BadRequest("You are asking for info about the future");
        }

        var result = await _service.GetPlayersByManagerIdAndGameweekNumber(managerId, gameweek);
        var response = new List<PlayerPickDto>();
        foreach (var player in result)
        {
            response.Add(_mapper.Map<PlayerPickDto>(player));
        }
        return response.Any() ? Ok(response) : NoContent();
    }
}
