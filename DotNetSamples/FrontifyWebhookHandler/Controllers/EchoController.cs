using FrontifyWebhookHandler.Model;
using FrontifyWebhookHandler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
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
            var eventPayload = await Request.GetRawBodyAsync();
            Root eventObject = JsonConvert.DeserializeObject<Root>(eventPayload);

            var receivedSignature = Request.Headers["X-Frontify-Webhook-Signature"].ToString();

            if (eventObject == null)
            {
                return BadRequest();
            }

            var calculatedSignature = GetCalculatedSignature(eventPayload);

            if (calculatedSignature != receivedSignature)
            {
                return BadRequest("Bad signature");
            }

            AddEvent(eventObject, eventPayload);

            return Ok(eventObject.@event.action);
        }

        public void AddEvent(Root request, string body)
        {
            //string jsonString = JsonConvert.SerializeObject(request);

            var ev = new FrontifyWebhookEvent();
            ev.Action = request.@event.action;
            ev.OccurredAt = request.@event.occurredAt;
            ev.ProcessedAt = request.@event.processedAt;
            ev.Payload = body;

            using (var context = new tstmstemplatesdbContext())
            {
                context.FrontifyWebhookEvents.Add(ev);
                context.SaveChanges();
            }
        }

        public static String GetCalculatedSignature(String payload)
        {
            string webhookSecret = "TuxJmCYjSqkgsu4aU9sd6dZ6XSDeB1D9";

            Encoding ascii = Encoding.ASCII;
            HMACSHA256 hmac = new HMACSHA256(ascii.GetBytes(webhookSecret));

            var calculatedSignature = Convert.ToBase64String(hmac.ComputeHash(ascii.GetBytes(payload)));

            return calculatedSignature;
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
