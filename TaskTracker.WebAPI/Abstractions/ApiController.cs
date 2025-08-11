using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.BLL.Shared;

namespace TaskTracker.WebAPI.Abstractions;

public abstract class ApiController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            
            { Error.Code: ErrorCodes.DatabaseError } =>
                StatusCode(
                    StatusCodes.Status500InternalServerError,
                    CreateProblemDetails(
                        title: "Database Error",
                        status: StatusCodes.Status500InternalServerError,
                        error: result.Error)),

            { Error.Code: ErrorCodes.NoExists } =>
                NotFound(
                    CreateProblemDetails(
                        title: "No Exists",
                        status: StatusCodes.Status404NotFound,
                        error: result.Error)),

            IValidationResult validationResult =>
                BadRequest(
                    CreateProblemDetails(
                        title: "Validation Error",
                        status: StatusCodes.Status400BadRequest,
                        error: result.Error,
                        errors: validationResult.Errors)),

            _ =>
                StatusCode(
                    StatusCodes.Status500InternalServerError,
                    CreateProblemDetails(
                        title: "Unexpected Error",
                        status: StatusCodes.Status500InternalServerError,
                        error: TodoItemsErrors.UnexpectedError))
        };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = default) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
