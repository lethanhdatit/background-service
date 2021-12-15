using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace background_service
{
    [DisallowConcurrentExecution]
    public class MainJob : IJob
    {
        private readonly ILogger<MainJob> _logger;
        public MainJob(ILogger<MainJob> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new {

                    userid = "",
                    username = "BG Server",
                    groupid = "0",
                    groupname = "Chung",
                    message = "hello from bg job"
                });
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                //API at chathub service
                var res = await client.PostAsync("https://localhost:44381/api/Hub/Send", data);
            }
            _logger.LogInformation("MainJob Executed!");
        }
    }
}
