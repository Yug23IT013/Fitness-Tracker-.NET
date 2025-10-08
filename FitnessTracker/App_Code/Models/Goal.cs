using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessTracker.App_Code.Models
{
    public class Goal
    {
        public int GoalID { get; set; }
        public int UserID { get; set; }
        public string GoalType { get; set; }
        public decimal TargetValue { get; set; }
        public decimal CurrentValue { get; set; }
        public string Unit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }

        public decimal ProgressPercentage
        {
            get
            {
                if (TargetValue == 0) return 0;
                return Math.Min(100, (CurrentValue / TargetValue) * 100);
            }
        }

        public int DaysRemaining
        {
            get
            {
                var days = (TargetDate - DateTime.Now).Days;
                return Math.Max(0, days);
            }
        }
    }
}