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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrEmpty(txtFirstName.Text.Trim()) || 
                    string.IsNullOrEmpty(txtLastName.Text.Trim()) ||
                    string.IsNullOrEmpty(txtEmail.Text.Trim()) ||
                    string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    ShowMessage("Please fill in all required fields.", "danger");
                    return;
                }

                // Validate password confirmation
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    ShowMessage("Passwords do not match.", "danger");
                    return;
                }

                // Check if email already exists
                if (DatabaseHelper.EmailExists(txtEmail.Text.Trim()))
                {
                    ShowMessage("An account with this email already exists.", "danger");
                    return;
                }

                // Create user object
                User newUser = new User
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Age = !string.IsNullOrEmpty(txtAge.Text) ? int.Parse(txtAge.Text) : (int?)null,
                    Gender = ddlGender.SelectedValue,
                    Height = !string.IsNullOrEmpty(txtHeight.Text) ? decimal.Parse(txtHeight.Text) : (decimal?)null,
                    Weight = !string.IsNullOrEmpty(txtWeight.Text) ? decimal.Parse(txtWeight.Text) : (decimal?)null
                };

                // Register user
                if (DatabaseHelper.RegisterUser(newUser))
                {
                    ShowMessage("Account created successfully! Please login.", "success");
                    // Clear form
                    ClearForm();
                }
                else
                {
                    ShowMessage("Failed to create account. Please try again.", "danger");
                }
            }
            catch (FormatException)
            {
                ShowMessage("Please enter valid numeric values for age, height, and weight.", "danger");
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred during registration. Please try again.", "danger");
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
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtAge.Text = "";
            ddlGender.SelectedIndex = 0;
            txtHeight.Text = "";
            txtWeight.Text = "";
        }
    }
}