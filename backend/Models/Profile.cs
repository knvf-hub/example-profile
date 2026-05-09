namespace backend.Models;

public class Profile
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    public DateTime BirthDate { get; set; }

    public List<ProfileOccupation> ProfileOccupations { get; set; } = [];

    public required string Gender { get; set; }

    public required string ProfileBase64 { get; set; }
}