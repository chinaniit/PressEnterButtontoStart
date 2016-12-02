using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;

namespace ConsoleApplication1
{
    public class RandomCodeReviewManager
    {
        public List<string> _candidateList;
        public int _index;
        public List<WinningPeople> _winningPeoples = new List<WinningPeople>();
        public string _user;
        public RandomCodeReviewManager()
        {
            ReadWriteData write = new ReadWriteData();
            var users = write.GetUser();
            string WinningPeoplesCount = ConfigurationSettings.AppSettings["LotteryNumber"];
            if (WinningPeoplesCount != null)
            {
                _candidateList = users.Select(p => p.User).ToList();
                var lotteryNumber = int.Parse(WinningPeoplesCount) > _candidateList.Count() ? _candidateList.Count() : int.Parse(WinningPeoplesCount);
                DateTime dt = DateTime.Now;
                var nowWeek = dt.DayOfWeek;
                var tomorrwWeek = nowWeek + 1;
                if ((int)tomorrwWeek > 5)
                {
                    tomorrwWeek = DayOfWeek.Monday;
                }
                _winningPeoples.Add(new WinningPeople() { Week = tomorrwWeek, Department = Department.Internal });
                _winningPeoples.Add(new WinningPeople() { Week = tomorrwWeek, Department = Department.Client });
                _winningPeoples.Add(new WinningPeople() { Week = tomorrwWeek, Department = Department.Affiliate });
            }
            else
            {
                throw new Exception("请填写正确的配置");
            }
        }
        public void LoadData()
        {
            Thread.Sleep(20);
            var count = _candidateList.Count;
            Random rd = new Random();
            _index = rd.Next(0, count);
            _user = _candidateList[_index];
        }
        public void LuckDraw()
        {
            LoadData();
            for (int i = 0; i < _winningPeoples.Count; i++)
            {
                var item = _winningPeoples[i];
                if (item.CodeReviewName == null)
                {
                    item.CodeReviewName = _user;
                    Console.WriteLine(item.Week + "  " + "CodeReviewName" + "  " + _user);
                    LuckDraw();
                    return;
                }
                else if (item.UpdateCodeName == null)
                {
                    if (item.CodeReviewName != _user)
                    {
                        item.UpdateCodeName = _user;
                        Console.WriteLine(item.Week + "  " + "UpdateCodeName" + "  " + _user);
                    }
                    LuckDraw();
                    return;
                }
            }
        }
        public void WriteData()
        {
            var readWrite = new ReadWriteData();
            LuckDraw();
            readWrite.WriteWinningPeople(_winningPeoples);
        }
    }
}
