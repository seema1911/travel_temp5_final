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
    public partial class AdminDashboard : System.Web.UI.MasterPage
    {
        string s = ConfigurationManager.ConnectionStrings["tour_db"].ToString();

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        classMngTour cs;
        protected void Page_Load(object sender, EventArgs e)
        {
            TotalPackages();
            TotalBookedpkg();
            TotalUser();

            con = new SqlConnection(s);
            if (Session["Admin_UserName"] == null)
            {
                Response.Redirect("AdminLogin.aspx");
                return;
            }
            lblAdmin.Text = "Welcome, " + Session["Admin_UserName"].ToString();

        }
        void getcon()
        {
            cs = new classMngTour();
            cs.startcon();
        }

        private void TotalBookedpkg()
        {
            getcon();
            SqlDataAdapter da = new SqlDataAdapter("select Count(Booking_Id) from Booking_tbl ", cs.startcon());
            ds = new DataSet();
            da.Fill(ds);
            lbltotalbookedpkg.Text = ds.Tables[0].Rows[0][0].ToString();
        }

        private void TotalPackages()
        {
            getcon();
            SqlDataAdapter da = new SqlDataAdapter("select Count(Package_Name) from tour_tbl ", cs.startcon());
            ds = new DataSet();
            da.Fill(ds);
            lbltotalpkg.Text = ds.Tables[0].Rows[0][0].ToString();
        }

        private void TotalUser()
        {
            getcon();
            SqlDataAdapter da = new SqlDataAdapter("select Count(Id) from user_tbl ", cs.startcon());
            ds = new DataSet();
            da.Fill(ds);
            lbltotaluser.Text = ds.Tables[0].Rows[0][0].ToString();
        }

     
    }
}