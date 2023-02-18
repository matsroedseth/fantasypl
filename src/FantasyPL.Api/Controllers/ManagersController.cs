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
        return result != null ? Ok(_mapper.Map<ManagerDto>(result)) : NotFound();
    }
}
