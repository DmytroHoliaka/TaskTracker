namespace TaskTracker.BLL.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        ErrorCodes.ValidationError,
        "A validation problem occured.");

    Error[] Errors { get; }
}
