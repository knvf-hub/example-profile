using backend.Models;

namespace backend.Services.Interfaces;

public interface IProfileService
{
    Task<CreateProfileResponse> CreateAsync(CreateProfileRequest request);
    Task<GetProfileResponse?> GetAsync(int id);
    Task<List<GetProfileResponse>> GetListAsync();
}