namespace backend.Models;

public class ProfileOccupation
{
    public int ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;

    public int OccupationId { get; set; }
    public Occupation Occupation { get; set; } = null!;
}