using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskTracker.WebAPI.Abstractions
{
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender Sender;

        protected ApiController(ISender sender)
        {
            Sender = sender;
        }
    }
}
