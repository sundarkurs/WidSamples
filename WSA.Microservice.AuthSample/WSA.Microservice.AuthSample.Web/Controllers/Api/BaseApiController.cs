using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WSA.Microservice.AuthSample.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class BaseApiController : ControllerBase
    {
    }
}
