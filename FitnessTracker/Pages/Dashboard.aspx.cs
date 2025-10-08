using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FitnessTracker.App_Code;
using FitnessTracker.App_Code.Models;

namespace FitnessTracker.Pages
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            try
            {
                User currentUser = (User)Session["User"];
                lblUserName.Text = currentUser.FirstName;

                // Load dashboard statistics
                var stats = DatabaseHelper.GetDashboardStats(currentUser.UserID);
                lblMonthlyWorkouts.Text = stats["MonthlyWorkouts"].ToString();
                lblMonthlyCalories.Text = stats["MonthlyCalories"].ToString();
                lblMonthlyMinutes.Text = stats["MonthlyMinutes"].ToString();
                lblActiveGoals.Text = stats["ActiveGoals"].ToString();

                // Load recent workouts (last 5)
                var recentWorkouts = DatabaseHelper.GetUserWorkoutSessions(currentUser.UserID)
                    .Take(5)
                    .ToList();
                gvRecentWorkouts.DataSource = recentWorkouts;
                gvRecentWorkouts.DataBind();

                // Load active goals
                var activeGoals = DatabaseHelper.GetUserGoals(currentUser.UserID)
                    .Where(g => !g.IsCompleted)
                    .Take(5)
                    .ToList();
                gvActiveGoals.DataSource = activeGoals;
                gvActiveGoals.DataBind();
            }
            catch (Exception ex)
            {
                // Handle error silently or show message
            }
        }
    }
}