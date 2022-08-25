using FrontifyWebhookHandler.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var bodyText = await Request.GetRawBodyAsync();

            Root request = JsonConvert.DeserializeObject<Root>(bodyText);

            var headers = Request.Headers;

            if (request == null)
            {
                return BadRequest();
            }
            return Ok(request.@event.action);
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
