namespace Koncar.Interview.Server.Application.Characters.Commands;

using Koncar.Interview.Server.Application.Contracts.Repositories;
using Koncar.Interview.Server.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public static class CreateCharacter
{
    public sealed record Command(
        string Name,
        string? Description) : IRequest<Character>;

    internal sealed class Handler : IRequestHandler<Command, Character>
    {
        private readonly ICharacterRepository _characterRepository;

        public Handler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<Character> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            Character character = new(
                request.Name,
                request.Description);

            return await _characterRepository.CreateAsync(character, cancellationToken);
        }
    }
}
