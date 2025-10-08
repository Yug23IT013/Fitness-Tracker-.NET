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
    public partial class LogWorkout : System.Web.UI.Page
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
                LoadActivities();
                txtSessionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void LoadActivities()
        {
            try
            {
                var activities = DatabaseHelper.GetAllActivities();
                ddlActivity.DataSource = activities;
                ddlActivity.DataTextField = "ActivityName";
                ddlActivity.DataValueField = "ActivityID";
                ddlActivity.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading activities. Please try again.", "danger");
            }
        }

        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlActivity.SelectedValue != "")
            {
                try
                {
                    int activityId = int.Parse(ddlActivity.SelectedValue);
                    var activity = DatabaseHelper.GetActivityById(activityId);
                    
                    if (activity != null)
                    {
                        lblActivityName.Text = activity.ActivityName;
                        lblActivityCategory.Text = activity.Category;
                        lblCaloriesPerMinute.Text = activity.CaloriesPerMinute.ToString("F1");
                        activityInfo.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Error loading activity information.", "danger");
                }
            }
            else
            {
                activityInfo.Visible = false;
            }
        }

        protected void btnLogWorkout_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate form
                if (ddlActivity.SelectedValue == "")
                {
                    ShowMessage("Please select an activity.", "danger");
                    return;
                }

                if (string.IsNullOrEmpty(txtSessionDate.Text) || string.IsNullOrEmpty(txtDuration.Text))
                {
                    ShowMessage("Please fill in all required fields.", "danger");
                    return;
                }

                User currentUser = (User)Session["User"];
                int activityId = int.Parse(ddlActivity.SelectedValue);
                DateTime sessionDate = DateTime.Parse(txtSessionDate.Text);
                int duration = int.Parse(txtDuration.Text);

                // Calculate calories if not provided
                decimal? calories = null;
                if (!string.IsNullOrEmpty(txtCalories.Text))
                {
                    calories = decimal.Parse(txtCalories.Text);
                }
                else
                {
                    // Calculate calories based on activity
                    var activity = DatabaseHelper.GetActivityById(activityId);
                    if (activity != null)
                    {
                        calories = activity.CaloriesPerMinute * duration;
                    }
                }

                // Create workout session
                WorkoutSession session = new WorkoutSession
                {
                    UserID = currentUser.UserID,
                    ActivityID = activityId,
                    SessionDate = sessionDate,
                    DurationMinutes = duration,
                    CaloriesBurned = calories,
                    Notes = txtNotes.Text.Trim()
                };

                // Save to database
                if (DatabaseHelper.LogWorkoutSession(session))
                {
                    ShowMessage("Workout logged successfully!", "success");
                    ClearForm();
                }
                else
                {
                    ShowMessage("Failed to log workout. Please try again.", "danger");
                }
            }
            catch (FormatException)
            {
                ShowMessage("Please enter valid numeric values for duration and calories.", "danger");
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred while logging the workout. Please try again.", "danger");
            }
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            divMessage.Attributes["class"] = $"alert alert-{type}";
            pnlMessage.Visible = true;
        }

        private void ClearForm()
        {
            ddlActivity.SelectedIndex = 0;
            txtSessionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtDuration.Text = "";
            txtCalories.Text = "";
            txtNotes.Text = "";
            activityInfo.Visible = false;
        }
    }
}