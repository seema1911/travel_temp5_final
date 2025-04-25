using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace travel_temp5_final
{
    public partial class UserLogin : System.Web.UI.MasterPage
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        classMngUser cs;
        PagedDataSource pg;
        int p, row;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] != null)
            {
                Response.Redirect("WebForm1.aspx");
            }
        }

        void getcon()
        {
            cs = new classMngUser();
            cs.startcon();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            cs = new classMngUser();  // Initialize mngUser class
            getcon();  // Ensure this method initializes the `con` object

            DataSet ds = cs.login(txtunm.Text.Trim(), txtpass.Text.Trim());

            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["User_Name"] = txtunm.Text.Trim();
                Session["userid"] = ds.Tables[0].Rows[0]["Id"].ToString();
                Response.Redirect("WebForm1.aspx");
            }
            else
            {
                Label1.Text = "Invalid Username or Password";
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
}