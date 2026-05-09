using backend.Models;

namespace backend.Services.Interfaces;

public interface IOccupationService
{
    Task<List<OccupationResponse>> ListAsync();
}