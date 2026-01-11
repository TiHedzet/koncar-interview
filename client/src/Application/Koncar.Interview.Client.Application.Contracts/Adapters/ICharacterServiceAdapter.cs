namespace Koncar.Interview.Client.Application.Contracts.Adapters;

using Koncar.Interview.Client.Application.Contracts.Models;
using Koncar.Interview.Client.Domain.Entities;

public interface ICharacterServiceAdapter
{
    Task<List<Character>> GetAsync(CancellationToken cancellationToken = default);

    Task<Character?> GetAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<Character> CreateAsync(
        CreateCharacterModel model,
        CancellationToken cancellationToken = default);

    Task<Character> UpdateAsync(
        Character character,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        long id,
        CancellationToken cancellationToken = default);
}   
