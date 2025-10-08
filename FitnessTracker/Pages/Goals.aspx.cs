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
    public partial class Goals : System.Web.UI.Page
    {
        private List<Goal> allGoals = new List<Goal>();

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
                LoadGoals();
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void LoadGoals()
        {
            try
            {
                User currentUser = (User)Session["User"];
                allGoals = DatabaseHelper.GetUserGoals(currentUser.UserID);
                
                // Separate active and completed goals
                var activeGoals = allGoals.Where(g => !g.IsCompleted).ToList();
                var completedGoals = allGoals.Where(g => g.IsCompleted).ToList();
                
                gvActiveGoals.DataSource = activeGoals;
                gvActiveGoals.DataBind();
                
                gvCompletedGoals.DataSource = completedGoals;
                gvCompletedGoals.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading goals. Please try again.", "danger");
            }
        }

        protected void btnCreateGoal_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate form
                if (string.IsNullOrEmpty(txtGoalType.Text.Trim()) ||
                    string.IsNullOrEmpty(txtTargetValue.Text.Trim()) ||
                    string.IsNullOrEmpty(txtUnit.Text.Trim()) ||
                    string.IsNullOrEmpty(txtStartDate.Text) ||
                    string.IsNullOrEmpty(txtTargetDate.Text))
                {
                    ShowMessage("Please fill in all required fields.", "danger");
                    return;
                }

                DateTime startDate = DateTime.Parse(txtStartDate.Text);
                DateTime targetDate = DateTime.Parse(txtTargetDate.Text);

                if (targetDate <= startDate)
                {
                    ShowMessage("Target date must be after start date.", "danger");
                    return;
                }

                User currentUser = (User)Session["User"];
                decimal currentValue = string.IsNullOrEmpty(txtCurrentValue.Text) ? 0 : decimal.Parse(txtCurrentValue.Text);

                Goal newGoal = new Goal
                {
                    UserID = currentUser.UserID,
                    GoalType = txtGoalType.Text.Trim(),
                    TargetValue = decimal.Parse(txtTargetValue.Text),
                    CurrentValue = currentValue,
                    Unit = txtUnit.Text.Trim(),
                    StartDate = startDate,
                    TargetDate = targetDate
                };

                if (DatabaseHelper.CreateGoal(newGoal))
                {
                    ShowMessage("Goal created successfully!", "success");
                    ClearForm();
                    LoadGoals();
                }
                else
                {
                    ShowMessage("Failed to create goal. Please try again.", "danger");
                }
            }
            catch (FormatException)
            {
                ShowMessage("Please enter valid numeric values for target and current values.", "danger");
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred while creating the goal. Please try again.", "danger");
            }
        }

        protected void btnUpdateProgress_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int goalId = int.Parse(btn.CommandArgument);
                
                // Find the textbox in the same row
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                TextBox txtUpdateValue = (TextBox)row.FindControl("txtUpdateValue");
                
                if (txtUpdateValue != null && !string.IsNullOrEmpty(txtUpdateValue.Text))
                {
                    decimal newValue = decimal.Parse(txtUpdateValue.Text);
                    
                    if (DatabaseHelper.UpdateGoalProgress(goalId, newValue))
                    {
                        ShowMessage("Goal progress updated successfully!", "success");
                        LoadGoals();
                    }
                    else
                    {
                        ShowMessage("Failed to update goal progress. Please try again.", "danger");
                    }
                }
                else
                {
                    ShowMessage("Please enter a valid value to update.", "danger");
                }
            }
            catch (FormatException)
            {
                ShowMessage("Please enter a valid numeric value.", "danger");
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred while updating the goal. Please try again.", "danger");
            }
        }

        protected void gvActiveGoals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActiveGoals.PageIndex = e.NewPageIndex;
            var activeGoals = allGoals.Where(g => !g.IsCompleted).ToList();
            gvActiveGoals.DataSource = activeGoals;
            gvActiveGoals.DataBind();
        }

        protected void gvCompletedGoals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompletedGoals.PageIndex = e.NewPageIndex;
            var completedGoals = allGoals.Where(g => g.IsCompleted).ToList();
            gvCompletedGoals.DataSource = completedGoals;
            gvCompletedGoals.DataBind();
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            divMessage.Attributes["class"] = $"alert alert-{type}";
            pnlMessage.Visible = true;
        }

        private void ClearForm()
        {
            txtGoalType.Text = "";
            txtTargetValue.Text = "";
            txtUnit.Text = "";
            txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtTargetDate.Text = "";
            txtCurrentValue.Text = "0";
        }
    }
}