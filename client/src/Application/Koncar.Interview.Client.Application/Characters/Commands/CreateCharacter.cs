namespace Koncar.Interview.Client.Application.Characters.Commands;

using ErrorOr;
using Koncar.Interview.Client.Application.Contracts.Adapters;
using Koncar.Interview.Client.Application.Contracts.Models;
using Koncar.Interview.Client.Application.Internal;
using Koncar.Interview.Client.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public static class CreateCharacter
{
    public sealed record Command(string Name, string? Description): IRequest<ErrorOr<Character>>;

    internal sealed class Handler : IRequestHandler<Command, ErrorOr<Character>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly ICharacterServiceAdapter _characterServiceAdapter;

        public Handler(
            ILogger<Handler> logger,
            ICharacterServiceAdapter characterServiceAdapter)
        {
            _logger = logger;
            _characterServiceAdapter = characterServiceAdapter;
        }

        public async Task<ErrorOr<Character>> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            try
            {
                CreateCharacterModel model = new(
                    request.Name,
                    request.Description);

                Character character = await _characterServiceAdapter.CreateAsync(
                    model,
                    cancellationToken);

                return ErrorOrFactory.From(character);
            }
            catch (Exception ex) 
            {
                _logger.LogUnexpectedError(ex);
                return Error.Failure(description: ex.Message);
            }
        }
    }
}
