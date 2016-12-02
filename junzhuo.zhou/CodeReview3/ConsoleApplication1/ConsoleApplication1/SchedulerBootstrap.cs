using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using Common.Logging;

namespace ConsoleApplication1
{
    public class SchedulerBootstrap
    {
        private readonly IScheduler _scheduler;
        public SchedulerBootstrap()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler();
            new RandomCodeReviewJob().Register(_scheduler);
            new SendEmailJob().Register(_scheduler);
        }
        public void Run()
        {
            _scheduler.Start();
        }

        public void Shutdown()
        {
            _scheduler.Shutdown(true);
        }
    }

}
