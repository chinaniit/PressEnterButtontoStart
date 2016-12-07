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
        private SysManager _manager = new SysManager();
        public IEnumerable<UserModel> _candidateList;
        public int _index;
        public List<WinningPeople> _winningPeoples = new List<WinningPeople>();
        public UserModel _user;
        public RandomCodeReviewManager()
        {
            SysManager userManager = new ConsoleApplication1.SysManager();
            _candidateList = userManager.GetOneUser();
            DateTime dt = DateTime.Now;
            var reviewDate = dt;
            _winningPeoples.Add(new WinningPeople() { ReviewDate = reviewDate, Department = Department.Internal });
            _winningPeoples.Add(new WinningPeople() { ReviewDate = reviewDate, Department = Department.Client });
            _winningPeoples.Add(new WinningPeople() { ReviewDate = reviewDate, Department = Department.Affiliate });
        }



        public void LoadData()
        {
            Thread.Sleep(20);
            var count = _candidateList.Count();
            Random rd = new Random();
            _index = rd.Next(0, count);
            _user = _candidateList.ElementAt(_index);
        }
        public void LuckDraw()
        {
            LoadData();
            for (int i = 0; i < _winningPeoples.Count; i++)
            {
                var item = _winningPeoples[i];
                if (item.CodeReviewId == null)
                {
                    item.CodeReviewId = _user.UserId;
                    Console.WriteLine(item.ReviewDate + "  " + "CodeReviewName" + "  " + _user.UserName);
                    LuckDraw();
                    return;
                }
                else if (item.UpdateCodeId == null)
                {
                    if (item.CodeReviewId != _user.UserId)
                    {
                        item.UpdateCodeId = _user.UserId;
                        Console.WriteLine(item.ReviewDate + "  " + "UpdateCodeName" + "  " + _user.UserName);
                    }
                    LuckDraw();
                    return;
                }
            }
        }
        public void WriteData()
        {
            LuckDraw();
            _manager.AddWinningPeoples(_winningPeoples);
        }
    }
}
