using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace travel_temp5_final
{
    public partial class AdminAddCatg : System.Web.UI.MasterPage
    {
        string s = ConfigurationManager.ConnectionStrings["tour_db"].ToString();
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        classMngTour cs;
        SqlCommand cmd;

        void getcon()
        {
            cs = new classMngTour();
            cs.startcon();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();
            con = new SqlConnection(s);
            if (Session["Admin_UserName"] == null)
            {
                Response.Redirect("AdminLogin.aspx");
                return;
            }
            lblAdmin.Text = "Welcome, " + Session["Admin_UserName"].ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            getcon();
            cs.addcatgory(txtcatg.Text);
        }

        protected void txtcatg_TextChanged(object sender, EventArgs e)
        {

        }
    }
}