namespace Koncar.Interview.Server.Starter.Common.ProblemDetails;

using Koncar.Interview.Server.Application.Contracts.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Net.Mime;

public static class ProblemDetailsConfiguration
{
    public static Action<ProblemDetailsOptions> ExceptionHandlingConfiguration =>
        options => options.CustomizeProblemDetails = CustomizeProblemDetails;

    private static Action<ProblemDetailsContext> CustomizeProblemDetails => async context =>
        {
            IExceptionHandlerFeature feature = context.HttpContext.Features.GetRequiredFeature<IExceptionHandlerFeature>();

            if (feature.Error is null)
            {
                return;
            }

            Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails = feature.Error switch
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
                    Detail = feature.Error.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal server error."
                }
            };

            
            context.HttpContext.Response.StatusCode = problemDetails.Status!.Value;
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;

            await context.HttpContext.Response.WriteAsJsonAsync(problemDetails);
        };
}
