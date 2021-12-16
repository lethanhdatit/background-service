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
    [DisallowConcurrentExecution]
    public abstract class JobBase : IJob
    {
        private readonly ILogger<JobBase> _logger;
        private readonly JobConfig _jobConfig;
        protected JobBase(ILogger<JobBase> logger, IConfiguration configuration, JobConfig jobConfig = null)
        {
            _logger = logger;

            _jobConfig = jobConfig ?? configuration?.GetSection("JobConfig")?.Get<JobConfig>();
            if (_jobConfig == null)
                _jobConfig = new JobConfig();
        }
        /// <summary>
        /// Process your customize logic with wrap config;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract Task Process(IJobExecutionContext context);

        /// <summary>
        /// Entry point when the job is reparing fire.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task Execute(IJobExecutionContext context)
        {
            if (!_jobConfig.IsRefireWhenFail || context.RefireCount >= _jobConfig.RefireLimit)
            {
                JobExecutionException e = new JobExecutionException("Retries exceeded");
                e.RefireImmediately = false;
                throw e;
            }
            try
            {
                await this.Process(context);
            }
            catch (Exception e)
            {
                Thread.Sleep(1000);

                if (_jobConfig.IsRefireWhenFail)
                {
                    JobExecutionException e2 = new JobExecutionException(e);
                    e2.RefireImmediately = true;
                    throw e2;
                }
            }
        }
    }
}
