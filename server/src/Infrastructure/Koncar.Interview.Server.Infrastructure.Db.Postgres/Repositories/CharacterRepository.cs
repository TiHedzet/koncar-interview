namespace Koncar.Interview.Server.Infrastructure.Db.Postgres.Repositories;

using Koncar.Interview.Server.Application.Contracts.Repositories;
using Koncar.Interview.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

internal sealed class CharacterRepository : ICharacterRepository
{
    private readonly DatabaseContext _databaseContext;

    public CharacterRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    public async Task<Character> CreateAsync(
        Character character,
        CancellationToken cancellationToken)
    {
        _databaseContext.Characters.Add(character);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return character;
    }

    public async Task DeleteAsync(
        Character character,
        CancellationToken cancellationToken)
    {
        _databaseContext.Characters.Remove(character);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Character>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _databaseContext.Characters
            .ToListAsync(cancellationToken);
    }

    public async Task<Character?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken)
    {
        return await _databaseContext.Characters
            .FirstOrDefaultAsync(
                character => character.Id == id, 
                cancellationToken);
    }

    public async Task<Character> UpdateAsync(
        Character character,
        CancellationToken cancellationToken)
    {
        _databaseContext.Characters.Update(character);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return character;
    }
}
