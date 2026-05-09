namespace backend.Models;

public class Occupation
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public List<ProfileOccupation> ProfileOccupations { get; set; } = [];

}