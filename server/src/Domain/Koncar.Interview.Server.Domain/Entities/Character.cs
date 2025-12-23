namespace Koncar.Interview.Server.Domain.Entities;

public sealed class Character
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
