using backend.Data;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class OccupationService : IOccupationService
{
    private readonly AppDbContext _db;

    public OccupationService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<OccupationResponse>> ListAsync()
    {
        return await _db.Occupations
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new OccupationResponse
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }
}