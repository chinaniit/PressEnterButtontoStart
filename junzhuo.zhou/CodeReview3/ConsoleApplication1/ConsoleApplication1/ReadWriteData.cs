using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public class ReadWriteData
    {
        public bool WriteWinningPeople(List<WinningPeople> list)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("WinningPeople.txt"))
                {
                    sw.WriteLine("抽奖日期：" + DateTime.Now);
                    foreach (var item in list)
                    {
                        string str = item.Department + "," + item.Week + "," + "CodeReviewName" + "," + item.CodeReviewName + "," + "UpdateCodeName" + "," + item.UpdateCodeName;
                        sw.WriteLine(str);
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }

        public EmailData GetEmailData()
        {
            EmailData result = new EmailData();
            string[] WinningPeoples = File.ReadAllLines("WinningPeople.txt");

            result.Title = WinningPeoples[0];
            List<WinningPeople> list = new List<WinningPeople>();
            for (int i = 1; i < WinningPeoples.Length; i++)
            {
                var item = WinningPeoples[i];
                string[] winningPeople = item.Split(',');
                WinningPeople mod = new WinningPeople();
                mod.Department = (Department)Enum.Parse(typeof(Department), winningPeople[0]);
                mod.Week = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), winningPeople[1]);
                mod.CodeReviewName = winningPeople[3].ToString();
                mod.UpdateCodeName = winningPeople[5].ToString();
                list.Add(mod);
            }
            result.List = list;
            return result;
        }

        public List<UserModel> GetUser()
        {
            StreamReader sr = new StreamReader("Users.json");
            JsonSerializer serializer = new JsonSerializer();
            //构建Json.net的读取流  
            JsonReader reader = new JsonTextReader(sr);
            //对读取出的Json.net的reader流进行反序列化，并装载到模型中  
            List<UserModel> users = serializer.Deserialize<List<UserModel>>(reader);
            return users;
        }

        public string ReadTextByFile(string path)
        {
            return File.ReadAllText(path);
        }

        public DateTime GetFileWriteDate(string path)
        {
            return File.GetLastWriteTime(path);
        }
        public Stream ReflectionStream(string path)
        {
            var sm = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            return sm;
        }
        public string GetFileText(string path)
        {
            var sm = ReflectionStream(path);
            if (sm == null) throw new IOException("Template " + path + " not found!");
            var bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);
            sm.Close();
            string templateString = Encoding.UTF8.GetString(bs);
            return templateString;
        }

        public string GetFileText(Stream sm)
        {
            var bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);
            sm.Close();
            string templateString = Encoding.UTF8.GetString(bs);
            return templateString;
        }

        public string GetWinningPeopleStrByHtml(string path, object source)
        {
            var templateString = GetFileText(path);
            return Nustache.Core.Render.StringToString(templateString, source);
        }
    }
}
