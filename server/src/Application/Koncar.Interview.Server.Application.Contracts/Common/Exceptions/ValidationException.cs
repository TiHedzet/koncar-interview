namespace Koncar.Interview.Server.Application.Contracts.Common.Exceptions;

using System;

public sealed class ValidationException : ApplicationException
{
    public ValidationException(string message) : base(message)
    {
    }
}
