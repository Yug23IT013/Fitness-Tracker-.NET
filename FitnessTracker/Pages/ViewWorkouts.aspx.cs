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
    public partial class ViewWorkouts : System.Web.UI.Page
    {
        private List<WorkoutSession> allWorkouts = new List<WorkoutSession>();

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
                LoadWorkouts();
            }
        }

        private void LoadWorkouts()
        {
            try
            {
                User currentUser = (User)Session["User"];
                
                DateTime? startDate = null;
                DateTime? endDate = null;

                if (!string.IsNullOrEmpty(txtStartDate.Text))
                    startDate = DateTime.Parse(txtStartDate.Text);
                if (!string.IsNullOrEmpty(txtEndDate.Text))
                    endDate = DateTime.Parse(txtEndDate.Text);

                allWorkouts = DatabaseHelper.GetUserWorkoutSessions(currentUser.UserID, startDate, endDate);
                
                gvWorkouts.DataSource = allWorkouts;
                gvWorkouts.DataBind();
                
                CalculateSummaryStats();
            }
            catch (Exception ex)
            {
                // Handle error silently
            }
        }

        private void CalculateSummaryStats()
        {
            if (allWorkouts.Any())
            {
                lblTotalWorkouts.Text = allWorkouts.Count.ToString();
                lblTotalCalories.Text = allWorkouts.Sum(w => w.CaloriesBurned ?? 0).ToString("F0");
                lblTotalMinutes.Text = allWorkouts.Sum(w => w.DurationMinutes).ToString();
                lblAvgDuration.Text = allWorkouts.Average(w => w.DurationMinutes).ToString("F1");
            }
            else
            {
                lblTotalWorkouts.Text = "0";
                lblTotalCalories.Text = "0";
                lblTotalMinutes.Text = "0";
                lblAvgDuration.Text = "0";
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            LoadWorkouts();
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            LoadWorkouts();
        }

        protected void gvWorkouts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorkouts.PageIndex = e.NewPageIndex;
            gvWorkouts.DataSource = allWorkouts;
            gvWorkouts.DataBind();
        }
    }
}