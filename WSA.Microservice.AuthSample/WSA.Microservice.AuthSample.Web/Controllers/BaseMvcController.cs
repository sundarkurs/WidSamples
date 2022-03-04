using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WSA.Microservice.AuthSample.Web.Controllers
{
    [Authorize]
    public class BaseMvcController : Controller
    {
    }
}
