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
        public void SendEmail()
        {
            try
            {
                ReadWriteData read = new ConsoleApplication1.ReadWriteData();
                var user = read.GetUser();
                string senderServerIp = ConfigurationSettings.AppSettings["Host"].ToString();
                string fromMailAddress = ConfigurationSettings.AppSettings["User"].ToString();
                string subjectInfo = "";
                var emailData = read.GetEmailData();
                string bodyInfo = read.GetWinningPeopleStrByHtml("ConsoleApplication1.Template.WinningPeople.html",
                    new { Data = emailData.List, Title = emailData.Title });
                string mailUsername = ConfigurationSettings.AppSettings["User"].ToString();
                string mailPassword = ConfigurationSettings.AppSettings["Pwd"].ToString(); //发送邮箱的密码（）
                string mailPort = ConfigurationSettings.AppSettings["Port"].ToString();
                MyEmail email = new MyEmail(senderServerIp, user.Select(p => p.Email), fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, false);
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
            bool isTest = Convert.ToBoolean(ConfigurationSettings.AppSettings["IsTest"]);
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
                     .WithCronSchedule(ConfigurationSettings.AppSettings["SendEmailJob"])
                     .Build();
            }

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
