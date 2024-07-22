using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.BLL.Shared;

namespace TaskTracker.WebAPI.Abstractions;

public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),

            IValidationResult validationResult =>
                BadRequest(
                    CreateProblemDetails(
                        title: "Validation Error",
                        status: StatusCodes.Status400BadRequest,
                        error: result.Error,
                        errors: validationResult.Errors)),

            _ =>
                BadRequest(
                    CreateProblemDetails(
                        title: "Bad Request",
                        status: StatusCodes.Status400BadRequest,
                        error: result.Error)),
        };

    private static ProblemDetails CreateProblemDetails(
        string title,   // ToDo: Change to predefined error code
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
