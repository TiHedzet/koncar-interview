namespace Koncar.Interview.Server.Application.Characters.Queries;

using Koncar.Interview.Server.Application.Contracts.Repositories;
using Koncar.Interview.Server.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public static class GetCharacters
{
    public sealed record Query : IRequest<List<Character>>;

    internal sealed class Handler : IRequestHandler<Query, List<Character>>
    {
        private readonly ICharacterRepository _characterRepository;

        public Handler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }
        public async Task<List<Character>> Handle(Query request, CancellationToken cancellationToken)
        {
            List<Character> characters = await _characterRepository.GetAllAsync(cancellationToken);

            return characters;
        }
    }
}
