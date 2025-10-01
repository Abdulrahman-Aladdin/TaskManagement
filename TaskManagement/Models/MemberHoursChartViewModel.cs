using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagement.Models
{
    public class MemberHoursChartViewModel
    {
        public string MemberName { get; set; }
        public double CompletedHours { get; set; }
        public double RemainingHours { get; set; }
    }
}