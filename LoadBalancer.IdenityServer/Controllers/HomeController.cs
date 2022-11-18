using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.IdenityServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("get")]
        [Authorize]
        public ActionResult Get()
        {
            return Ok("Works");
        }
    }
}