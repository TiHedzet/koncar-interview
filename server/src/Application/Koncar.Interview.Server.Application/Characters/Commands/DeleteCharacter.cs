namespace Koncar.Interview.Server.Application.Characters.Commands;

using Koncar.Interview.Server.Application.Contracts.Common.Exceptions;
using Koncar.Interview.Server.Application.Contracts.Repositories;
using Koncar.Interview.Server.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

public static class DeleteCharacter
{
    public sealed record Command(long Id) : IRequest<Character>;

    internal sealed class Handler : IRequestHandler<Command, Character>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(ICharacterRepository characterRepository, ILogger<Handler> logger)
        {
            _characterRepository = characterRepository;
            _logger = logger;
        }
        public async Task<Character> Handle(Command request, CancellationToken cancellationToken)
        {
            Character? character = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);

            if (character is null)
            {
                _logger.LogWarning("Character with id {Id} not found", request.Id);
                throw new NotFoundException($"Character with id {request.Id} not found.");
            }

            await _characterRepository.DeleteAsync(character, cancellationToken);

            return character;
        }
    }
}
