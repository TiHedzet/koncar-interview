namespace Koncar.Interview.Client.Application.Characters.Queries;

using ErrorOr;
using Koncar.Interview.Client.Application.Contracts.Adapters;
using Koncar.Interview.Client.Application.Internal;
using Koncar.Interview.Client.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public static class GetCharacter
{
    public sealed record Query(long Id) : IRequest<ErrorOr<Character>>;

    internal sealed class Handler : IRequestHandler<Query, ErrorOr<Character>>
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
            Query request,
            CancellationToken cancellationToken)
        {
            try
            {
                Character? character = await _characterServiceAdapter.GetAsync(
                    request.Id,
                    cancellationToken);

                if (character is null)
                {
                    return Error.NotFound(description: $"No character with id {request.Id} found.");
                }

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
