namespace TaskTracker.BLL.Shared;

public static class TodoItemsErrors
{
    public static readonly Error NoExists = new(
        ErrorCodes.NoExists, 
        "TodoItem doesn't exists");

    public static readonly Error DatabaseError = new(
        ErrorCodes.DatabaseError, 
        "An exception occurred while working with the database");

    public static readonly Error UnexpectedError = new(
        ErrorCodes.UnexpectedError,
        "An unexpected error occurred.");

    public static readonly Error ValidationError = new(
        ErrorCodes.ValidationError,
        "A validation problem occured.");
}
