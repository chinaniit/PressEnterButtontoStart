using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class WinningPeople
    {
        public Department Department { get; set; }
        public DayOfWeek Week { get; set; }
        public string CodeReviewName { get; set; }
        public string UpdateCodeName { get; set; }
    }
    public enum Department
    {
        Internal,
        Client,
        Affiliate
    }
}

