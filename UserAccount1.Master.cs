using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace travel_temp5_final
{
    public partial class UserAccount1 : System.Web.UI.MasterPage
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        classMngTour cs;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User_Name"] != null)
            {

                lblusr.Text = "Welcome, " + Session["User_Name"].ToString();

            }
            else
            {
                Response.Redirect("UserLogin.aspx");
            }

            Totalcart();
            Totalorder();

        }

        void getcon()
        {
            cs = new classMngTour();
            cs.startcon();
        }


        private void Totalcart()
        {
            getcon();
            SqlDataAdapter da = new SqlDataAdapter("select Count(Cart_Id) from Cart_tbl ", cs.startcon());
            ds = new DataSet();
            da.Fill(ds);
            lbltotalcart.Text = ds.Tables[0].Rows[0][0].ToString();
        }

        private void Totalorder()
        {
            getcon();
            SqlDataAdapter da = new SqlDataAdapter("select Count(Order_Id) from MyOrder_tbl ", cs.startcon());
            ds = new DataSet();
            da.Fill(ds);
            lbltotalcart.Text = ds.Tables[0].Rows[0][0].ToString();
        }
    }
}