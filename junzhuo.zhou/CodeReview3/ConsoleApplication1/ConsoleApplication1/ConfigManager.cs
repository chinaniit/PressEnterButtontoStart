using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ConsoleApplication1
{

    public static class ConfigManager
    {
        public static ConfigModel GetConfig()
        {
            bool isTest = false;
            bool.TryParse(ConfigurationManager.AppSettings["IsTest"], out isTest);
            ConfigModel model = new ConfigModel();
            model.EmailHost = ConfigurationManager.AppSettings["EmailHost"].ToString();
            model.EmailUser = ConfigurationManager.AppSettings["EmailUser"].ToString();
            model.EmailPwd = ConfigurationManager.AppSettings["EmailPwd"].ToString();
            model.EmailPort = ConfigurationManager.AppSettings["EmailPort"].ToString();
            model.SendEmailJob = ConfigurationManager.AppSettings["SendEmailJob"].ToString();
            model.RandomCodeReviewJob = ConfigurationManager.AppSettings["RandomCodeReviewJob"].ToString();
            model.TestEmail = ConfigurationManager.AppSettings["TestEmail"].ToString();
            model.IsTest = isTest;
            model.ConStr = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;
            return model;
        }
    }
    public class ConfigModel
    {
        public string EmailHost { get; set; }
        public string EmailUser { get; set; }
        public string EmailPwd { get; set; }
        public string EmailPort { get; set; }
        public string SendEmailJob { get; set; }
        public string RandomCodeReviewJob { get; set; }
        public bool IsTest { get; set; }
        public string TestEmail { get; set; }
        public string ConStr { get; set; }
    }
}
