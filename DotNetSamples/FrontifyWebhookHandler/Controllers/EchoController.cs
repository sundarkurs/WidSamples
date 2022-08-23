using FrontifyWebhookHandler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Post([FromBody] Root input)
        {
            if (input == null)
            {
                return BadRequest();
            }

            //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(input);
            return Ok(input.@event.action);
        }
    }
}
