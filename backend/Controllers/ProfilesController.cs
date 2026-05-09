using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfilesController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProfileRequest request)
    {
        var response = await _profileService.CreateAsync(request);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await _profileService.GetListAsync();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _profileService.GetAsync(id);

        if (response == null)
        {
            return NotFound(new { message = "Profile not found" });
        }

        return Ok(response);
    }
}

