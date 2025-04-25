using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace travel_temp5_final
{
    public partial class AdminLogin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserLogin.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text.Trim();
            string password = TextBox2.Text.Trim();

            // Check if username and password match the valid credentials
            if ((username == "Admin1" && password == "1234567890") || (username == "Admin2" && password == "Admin234"))
            {
                // If credentials are correct, show success message
                //LabelMessage.ForeColor = System.Drawing.Color.Green;
                //LabelMessage.Text = "Login Successful!";
                Session["Admin_UserName"] = username;
                Response.Redirect("AdminDashboard.aspx");

                // You can redirect to another page (e.g., Admin Dashboard) here
                // Response.Redirect("AdminDashboard.aspx");
            }
            else
            {
                // If the credentials are incorrect, show an error message
                LabelMessage.ForeColor = System.Drawing.Color.Red;
                LabelMessage.Text = "Incorrect Username or Password!";
            }
        }



    }
}