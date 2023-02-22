using AutoMapper;
using FantasyPL.Api.Services;
using FantasyPL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPL.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameDataController : ControllerBase
{
    private readonly IFantasyService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<GameDataController> _logger;

    public GameDataController(
        IFantasyService service,
        IMapper mapper,
        ILogger<GameDataController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetGameData()
    {
        var result = await _service.GetGameData();
        return Ok(_mapper.Map<FantasyDataDto>(result));
    }
}
