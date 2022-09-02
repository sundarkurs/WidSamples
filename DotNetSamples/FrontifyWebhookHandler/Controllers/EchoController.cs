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
            var eventObject = JsonConvert.DeserializeObject<Root>(eventPayload);

            var receivedSignature = Request.Headers["X-Frontify-Webhook-Signature"].ToString();

            if (eventObject == null)
            {
                return BadRequest();
            }

            var calculatedSignature = CalculateSignature(eventPayload);

            if (calculatedSignature != receivedSignature)
            {
                return BadRequest("Bad signature");
            }

            StoreEvent(eventObject, eventPayload);

            return Ok(eventObject.@event.action);
        }

        public void StoreEvent(Root request, string body)
        {
            // Exclude ASSET_CREATE actions, because we are interested only on UPDATE AND DELETE
            if (request.@event.action == EventActions.AssetCreated)
            {
                return;
            }

            var webhookEvent = new FrontifyWebhookEvent();
            webhookEvent.Action = request.@event.action;
            webhookEvent.OccurredAt = request.@event.occurredAt;
            webhookEvent.ProcessedAt = request.@event.processedAt;
            webhookEvent.Payload = body;

            using (var context = new tstmstemplatesdbContext())
            {
                context.FrontifyWebhookEvents.Add(webhookEvent);
                context.SaveChanges();
            }
        }

        public String CalculateSignature(string payload)
        {
            string webhookSecret = "H6dySN8wkgarw3tHkmGAL9KiB8utu7PB";
            var ascii = Encoding.ASCII;
            var hmac = new HMACSHA256(ascii.GetBytes(webhookSecret));

            return Convert.ToBase64String(hmac.ComputeHash(ascii.GetBytes(payload)));
        }
    }
}
