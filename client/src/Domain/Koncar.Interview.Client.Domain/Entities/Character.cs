namespace Koncar.Interview.Client.Domain.Entities;

public sealed record Character(
    long Id,
    string Name,
    string? Description);
