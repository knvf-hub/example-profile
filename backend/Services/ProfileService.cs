using backend.Data;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class ProfileService : IProfileService
{
    private readonly AppDbContext _db;

    public ProfileService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<CreateProfileResponse> CreateAsync(CreateProfileRequest request)
    {
        var occupations = await _db.Occupations
            .Where(x => request.OccupationIds.Contains(x.Id))
            .ToListAsync();

        if (occupations.Count != request.OccupationIds.Count)
        {
            throw new Exception("Invalid occupation selected");
        }

        var profile = new Profile
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            BirthDate = request.BirthDate,
            Gender = request.Gender,
            ProfileBase64 = request.ProfileBase64,
            ProfileOccupations = occupations.Select(x => new ProfileOccupation
            {
                OccupationId = x.Id
            }).ToList()
        };

        _db.Profiles.Add(profile);
        await _db.SaveChangesAsync();

        return new CreateProfileResponse
        {
            Id = profile.Id,
            Message = "save data success"
        };
    }

    public async Task<GetProfileResponse?> GetAsync(int id)
    {
        var profile = await _db.Profiles
            .Include(x => x.ProfileOccupations)
                .ThenInclude(x => x.Occupation)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (profile == null)
        {
            return null;
        }

        return MapToGetProfileResponse(profile);
    }

    public async Task<List<GetProfileResponse>> GetListAsync()
    {
        var profiles = await _db.Profiles
            .Include(x => x.ProfileOccupations)
                .ThenInclude(x => x.Occupation)
            .OrderByDescending(x => x.Id)
            .ToListAsync();

        return profiles.Select(MapToGetProfileResponse).ToList();
    }

    private static GetProfileResponse MapToGetProfileResponse(Profile profile)
    {
        return new GetProfileResponse
        {
            Id = profile.Id,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Email = profile.Email,
            Phone = profile.Phone,
            BirthDate = profile.BirthDate,
            Gender = profile.Gender,
            ProfileBase64 = profile.ProfileBase64,
            Occupations = profile.ProfileOccupations.Select(x => new OccupationResponse
            {
                Id = x.Occupation.Id,
                Name = x.Occupation.Name
            }).ToList()
        };
    }
}