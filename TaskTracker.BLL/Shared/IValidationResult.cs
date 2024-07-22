namespace TaskTracker.BLL.Shared;

public interface IValidationResult
{
    // ToDo: Specify predefined error
    public static readonly Error ValidationError = new(
        code: "ValidationError",
        "A validation problem occured.");

    Error[] Errors { get; }
}
