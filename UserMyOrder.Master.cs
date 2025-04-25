using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace travel_temp5_final
{
    public partial class UserMyOrder : System.Web.UI.MasterPage
    {
        string connString = ConfigurationManager.ConnectionStrings["tour_db"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMyOrdersData();
            }

            if (Session["User_Name"] != null)
            {

                lblusr.Text = "Welcome, " + Session["User_Name"].ToString();

            }
            else
            {
                Response.Redirect("UserLogin.aspx");
            }
        }

        private void BindMyOrdersData()
        {
            if (Session["User_Name"] != null)
            {
                string username = Session["User_Name"].ToString();
                con = new SqlConnection(connString);
                cmd = new SqlCommand("SELECT Order_Id, User_Name, Package_Name, Price, Image FROM MyOrder_tbl WHERE User_Name = @User_Name", con);
                cmd.Parameters.AddWithValue("@User_Name", username);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();

                gvMyOrders.DataSource = dt;
                gvMyOrders.DataBind();
            }
            //con = new SqlConnection(connString);
            //cmd = new SqlCommand("SELECT Order_Id, User_Name, Package_Name, Price, Image FROM MyOrder_tbl", con);
            //da = new SqlDataAdapter(cmd);
            //dt = new DataTable();

            //con.Open();
            //da.Fill(dt);
            //con.Close();

            //gvMyOrders.DataSource = dt;
            //gvMyOrders.DataBind();
        }
        protected void gvMyOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void gvMyOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvMyOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Your row formatting logic here
            }
        }
    }
}