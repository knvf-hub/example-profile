using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OccupationsController : ControllerBase
{
    private readonly IOccupationService _occupationService;

    public OccupationsController(IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OccupationResponse>>> Get()
    {
        var occupations = await _occupationService.ListAsync();

        return Ok(occupations);
    }
}