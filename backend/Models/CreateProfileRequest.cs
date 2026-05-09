namespace backend.Models;

public class CreateProfileRequest
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    public DateTime BirthDate { get; set; }

    public required List<int> OccupationIds { get; set; }

    public required string Gender { get; set; }

    public required string ProfileBase64 { get; set; }
}