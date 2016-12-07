using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public abstract class JobBase : IJob
    {
        protected static IContainer Container;
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                ExecuteJob(context);
            }
            catch (Exception ex)
            {
                //throw;
            }


        }
        public abstract void Register(IScheduler scheduler);
        protected abstract void ExecuteJob(IJobExecutionContext context);

    }
}
