using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagement.Models
{
    public class ChartAttributes
    {
        public string DataUrl { get; set; }
        public string ChartTitle { get; set; }
        public string XAxisTitle { get; set; }
        public string YAxisTitle { get; set; }
        public string ChartId { get; set; }
    }
}