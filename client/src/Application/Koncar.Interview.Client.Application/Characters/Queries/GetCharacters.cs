namespace Koncar.Interview.Client.Application.Characters.Queries;

using ErrorOr;
using Koncar.Interview.Client.Application.Contracts.Adapters;
using Koncar.Interview.Client.Application.Internal;
using Koncar.Interview.Client.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public static class GetCharacters
{
    public sealed record Query : IRequest<ErrorOr<List<Character>>>;

    internal sealed class Handler : IRequestHandler<Query, ErrorOr<List<Character>>>
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

        public async Task<ErrorOr<List<Character>>> Handle(
            Query request,
            CancellationToken cancellationToken)
        {
            try
            {
                List<Character> characters = await _characterServiceAdapter.GetAsync(cancellationToken);
                
                return ErrorOrFactory.From(characters);
            }
            catch (Exception ex)
            {
                _logger.LogUnexpectedError(ex);
                return Error.Failure(description: ex.Message);
            }

        }
    }
}
