namespace Koncar.Interview.Client.Application.Characters.Commands;

using ErrorOr;
using Koncar.Interview.Client.Application.Contracts.Adapters;
using Koncar.Interview.Client.Application.Internal;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public static partial class DeleteCharacter
{
    public sealed record Command(long Id) : IRequest<ErrorOr<Success>>;

    internal sealed class Handler : IRequestHandler<Command, ErrorOr<Success>>
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

        public async Task<ErrorOr<Success>> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _characterServiceAdapter.DeleteAsync(
                    request.Id,
                    cancellationToken);

                return Result.Success;
            }
            catch (Exception ex) 
            {
                _logger.LogUnexpectedError(ex);
                return Error.Failure(description: ex.Message);
            }
        }
    }
}
