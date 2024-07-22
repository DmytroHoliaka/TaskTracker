namespace TaskTracker.BLL.Shared;

public class TodoItemsErrors
{
    public static readonly Error NoExists = new(
        TodoItemErrorCodes.NoExists, 
        "TodoItem doesn't exists");

    public static readonly Error DatabaseError = new(
        TodoItemErrorCodes.DatabaseError, 
        "An exception occurred while working with the database");

    public static readonly Error UnexpectedError = new(
        TodoItemErrorCodes.UnexpectedError,
        "An unexpected error occurred.");
}
