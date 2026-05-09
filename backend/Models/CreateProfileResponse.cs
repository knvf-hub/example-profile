namespace backend.Models;

public class CreateProfileResponse
{
    public int Id { get; set; }

    public required string Message { get; set; }
}

public class GetProfileResponse
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public required List<OccupationResponse> Occupations { get; set; }
    public required string Gender { get; set; }
    public required string ProfileBase64 { get; set; }
}