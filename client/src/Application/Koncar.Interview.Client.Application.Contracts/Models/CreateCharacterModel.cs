namespace Koncar.Interview.Client.Application.Contracts.Models;

public sealed record CreateCharacterModel(
    string Name,
    string? Description);
