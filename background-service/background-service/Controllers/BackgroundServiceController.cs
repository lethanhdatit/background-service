using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace background_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BackgroundServiceController : ControllerBase
    {
        private readonly ILogger<BackgroundServiceController> _logger;

        public BackgroundServiceController(ILogger<BackgroundServiceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Started!");
        }
    }
}
