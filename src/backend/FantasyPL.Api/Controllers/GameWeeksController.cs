using AutoMapper;
using FantasyPL.Api.Services;
using FantasyPL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPL.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameWeeksController : ControllerBase
{
    private readonly IFantasyService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<GameWeeksController> _logger;

    public GameWeeksController(
        IFantasyService service,
        IMapper mapper,
        ILogger<GameWeeksController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllGameWeeks()
    {
        var result = await _service.GetAllGameWeeks();
        return Ok(result);
    }

    [HttpGet("previous")]
    public async Task<IActionResult> GetPreviousGameWeek()
    {
        var result = await _service.GetPreviousGameWeek();
        return Ok(result);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentGameWeek()
    {
        var result = await _service.GetCurrentGameWeek();
        return Ok(result);
    }

    [HttpGet("next")]
    public async Task<IActionResult> GetNextGameWeek()
    {
        var result = await _service.GetNextGameWeek();
        return Ok(result);
    }
}
