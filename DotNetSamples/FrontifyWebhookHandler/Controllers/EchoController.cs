using FrontifyWebhookHandler.Model;
using FrontifyWebhookHandler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FrontifyWebhookHandler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
           {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<EchoController> _logger;

        public EchoController(ILogger<EchoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to Webhook.");
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var bodyText = await Request.GetRawBodyAsync();

            Root request = JsonConvert.DeserializeObject<Root>(bodyText);

            var headers = Request.Headers;

            if (request == null)
            {
                return BadRequest();
            }

            AddEvent(request, bodyText);

            return Ok(request.@event.action);
        }

        public void AddEvent(Root request, string body)
        {
            //string jsonString = JsonConvert.SerializeObject(request);

            var ev = new FrontifyWebhookEvent();
            ev.Action = request.@event.action;
            ev.OccurredAt = request.@event.occurredAt;
            ev.ProcessedAt = request.@event.processedAt;
            ev.Payload = body;

            using (tstmstemplatesdbContext context = new tstmstemplatesdbContext())
            {
                context.FrontifyWebhookEvents.Add(ev);
                context.SaveChanges();
            }
        }


        //[HttpPost]
        //public IActionResult PostRaw([FromBody] string input)
        //{
        //    if (input == null)
        //    {
        //        return BadRequest();
        //    }

        //    Root request = JsonConvert.DeserializeObject<Root>(input);
        //    return Ok(request.@event.action);
        //}
    }
}
