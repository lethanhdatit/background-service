using background_service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace background_service
{
    public class OtherJob : JobBase
    {
        private readonly ILogger<OtherJob> _logger;
        private readonly IConfiguration _configuration;

        public OtherJob(ILogger<OtherJob> logger, IConfiguration configuration) : base (logger, configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public override Task Process(IJobExecutionContext context)
        {
            _logger.LogInformation(context.FireInstanceId);
            throw new Exception("any error");
        }
    }
}
