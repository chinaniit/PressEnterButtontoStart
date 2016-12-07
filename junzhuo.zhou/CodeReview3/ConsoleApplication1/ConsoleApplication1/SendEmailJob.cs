using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.Net.Mail;
using System.Configuration;
using System.Threading;

namespace ConsoleApplication1
{
    public class SendEmailJob : JobBase
    {
        SysManager _manager = new SysManager();
        public void SendEmail()
        {
            if ((int)DateTime.Now.DayOfWeek > 5)
            {
                return;
            }
            try
            {
                var users = _manager.GetOneUser();
                string senderServerIp = ConfigManager.GetConfig().EmailHost;
                string fromMailAddress = ConfigManager.GetConfig().EmailUser;
                ReadWriteData read = new ConsoleApplication1.ReadWriteData();
                var winningPeoples = _manager.GetWinningPeoples();
                var subject = "抽奖日期：" + winningPeoples.FirstOrDefault().ReviewDate;
                string bodyInfo = read.GetWinningPeopleStrByHtml("ConsoleApplication1.Template.WinningPeople.html",
                    new { Data = winningPeoples, Title = subject });
                string mailUsername = ConfigManager.GetConfig().EmailUser;
                string mailPassword = ConfigManager.GetConfig().EmailPwd;
                string mailPort = ConfigManager.GetConfig().EmailPort;
                MyEmail email = new MyEmail(senderServerIp, users.Select(p => p.Email), fromMailAddress, subject, bodyInfo, mailUsername, mailPassword, mailPort, false, false);
                email.Send();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected override void ExecuteJob(IJobExecutionContext context)
        {
            SendEmail();
        }

        public override void Register(IScheduler scheduler)
        {
            bool isTest = Convert.ToBoolean(ConfigManager.GetConfig().IsTest);
            IJobDetail job = JobBuilder.Create(this.GetType())
                .Build();
            ITrigger trigger;
            if (isTest)
            {
                trigger = TriggerBuilder.Create()
               .StartNow()
               .Build();
            }
            else
            {
                trigger = (ICronTrigger)TriggerBuilder.Create()
                     .WithCronSchedule(ConfigManager.GetConfig().SendEmailJob)
                     .Build();
            }

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
