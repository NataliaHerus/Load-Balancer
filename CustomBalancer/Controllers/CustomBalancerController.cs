using LoadBalancer.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomBalancer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBalancerController : ControllerBase
    {
        private int[] PORTS = { 7070, 44327 };

        private readonly LoadBalancerDbContext context;

        public CustomBalancerController(LoadBalancerDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult Balance()
        {
            var listeners = context.Listeners.ToList();

            var dict = new Dictionary<int, int>();

            for (int i = 0; i < PORTS.Length; i++)
            {
                dict.Add(PORTS[i], 0);
            }

            for (int i = 0; i < listeners.Count; i++)
            {
                if (dict.ContainsKey(listeners[i].Port))
                {
                    dict[listeners[i].Port] += listeners[i].Load;
                }
            }

            if (dict.Count == 0)
            {
                return Ok(new
                {
                    port = PORTS[0]
                });
            }

            var port = dict.OrderBy(x => x.Value).First().Key;

            return Ok(new
            {
                port = port
            });
        }
    }
}
