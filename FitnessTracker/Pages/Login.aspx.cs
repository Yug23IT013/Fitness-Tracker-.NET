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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ShowMessage("Please enter both email and password.", "danger");
                    return;
                }

                User user = DatabaseHelper.AuthenticateUser(email, password);
                if (user != null)
                {
                    Session["User"] = user;
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    ShowMessage("Invalid email or password. Please try again.", "danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred during login. Please try again.", "danger");
            }
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            divMessage.Attributes["class"] = $"alert alert-{type}";
            pnlMessage.Visible = true;
        }
    }
}