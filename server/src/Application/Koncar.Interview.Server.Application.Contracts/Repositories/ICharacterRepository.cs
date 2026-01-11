namespace Koncar.Interview.Server.Application.Contracts.Repositories;

using Koncar.Interview.Server.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface ICharacterRepository
{
    Task<Character?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken);

    Task<List<Character>> GetAllAsync(CancellationToken cancellationToken);

    Task<Character> UpdateAsync(
        Character character,
        CancellationToken cancellationToken);

    Task<Character> CreateAsync(
        Character character,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Character character,
        CancellationToken cancellationToken);
}
