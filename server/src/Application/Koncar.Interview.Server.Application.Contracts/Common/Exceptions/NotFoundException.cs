namespace Koncar.Interview.Server.Application.Contracts.Common.Exceptions;

using System;

public sealed class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message)
    {
    }
}
