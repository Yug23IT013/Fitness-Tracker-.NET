using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessTracker.App_Code.Models
{
    public class Activity
    {
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public decimal CaloriesPerMinute { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
    }
}