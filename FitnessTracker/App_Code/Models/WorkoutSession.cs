using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessTracker.App_Code.Models
{
    public class WorkoutSession
    {
        public int SessionID { get; set; }
        public int UserID { get; set; }
        public int ActivityID { get; set; }
        public DateTime SessionDate { get; set; }
        public int DurationMinutes { get; set; }
        public decimal? CaloriesBurned { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public string ActivityName { get; set; }
        public string ActivityCategory { get; set; }
        public decimal ActivityCaloriesPerMinute { get; set; }
    }
}