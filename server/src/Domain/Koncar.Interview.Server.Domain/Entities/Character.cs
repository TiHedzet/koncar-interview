namespace Koncar.Interview.Server.Domain.Entities;

public sealed class Character
{
    public Character(string name, string? description)
    {
        Name = name; 
        Description = description;
    }

    public long Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }
}
