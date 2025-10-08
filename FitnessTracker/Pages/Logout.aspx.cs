using System;
using System.Web;
using System.Web.UI;

namespace FitnessTracker.Pages
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Session.Abandon();

                if (Request.Cookies[".ASPXAUTH"] != null)
                {
                    HttpCookie authCookie = new HttpCookie(".ASPXAUTH");
                    authCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(authCookie);
                }

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

                Response.Redirect("Login.aspx");
            }
            catch
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}



