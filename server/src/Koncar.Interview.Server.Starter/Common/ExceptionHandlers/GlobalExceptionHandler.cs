namespace Koncar.Interview.Server.Starter.Common.ExceptionHandlers;

using Koncar.Interview.Server.Application.Contracts.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        if (exception is null)
        {
            return false;
        }


        ProblemDetails problemDetails = exception switch
        {
            NotFoundException notFoundException => new()
            {
                Detail = notFoundException.Message,
                Status = StatusCodes.Status404NotFound,
                Title = "Resource not found."
            },
            ValidationException validationException => new()
            {
                Detail = validationException.Message,
                Status = StatusCodes.Status400BadRequest,
                Title = "One or more validation errors occurred."
            },
            _ => new()
            {
                Detail = exception.Message,
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal server error."
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status!.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
