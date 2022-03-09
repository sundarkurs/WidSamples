using Microsoft.AspNetCore.Mvc;
using WSA.Microservice.AuthSample.Web.Attributes;

namespace WSA.Microservice.AuthSample.Web.Controllers.Api
{
    //[TokenAuthorize]
    public class TodoController : BaseApiController
    {
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
        }
    }
}
