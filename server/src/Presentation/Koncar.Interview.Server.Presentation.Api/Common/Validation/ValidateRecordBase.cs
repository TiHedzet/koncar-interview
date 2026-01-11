namespace Koncar.Interview.Server.Presentation.Api.Common.Validation;

public abstract record ValidateRecordBase<T> 
    where T : ValidateRecordBase<T>
{
    public abstract void Validate();
}
