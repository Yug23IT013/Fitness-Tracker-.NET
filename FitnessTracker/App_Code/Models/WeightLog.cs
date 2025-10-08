using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessTracker.App_Code.Models
{
    public class WeightLog
    {
        public int WeightLogID { get; set; }
        public int UserID { get; set; }
        public decimal Weight { get; set; }
        public DateTime LogDate { get; set; }
        public string Notes { get; set; }
    }
}
