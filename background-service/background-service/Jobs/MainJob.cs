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
    public class MainJob : JobBase
    {
        private readonly ILogger<MainJob> _logger;
        private readonly IConfiguration _configuration;

        public MainJob(ILogger<MainJob> logger, IConfiguration configuration) : base(logger, configuration, new JobConfig
        {
            IsRefireWhenFail = false,
            RefireLimit = 3
        })
        {
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task Process(IJobExecutionContext context)
        {
            _logger.LogInformation(context.FireInstanceId);
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new
                {
                    userid = "",
                    username = "BG Server",
                    groupid = "0",
                    groupname = "Chung",
                    message = "hello from bg job"
                });
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                //API at chathub service
                var res = await client.PostAsync("https://localhost:44381/api/Hub/Send", data);
            }
        }
    }
}
