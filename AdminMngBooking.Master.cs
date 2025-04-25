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

    public partial class AdminMngBooking : System.Web.UI.MasterPage
    {
        string s = ConfigurationManager.ConnectionStrings["tour_db"].ToString();
        SqlConnection con;
        classMngUser cs;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(s);
            if (Session["Admin_UserName"] == null)
            {
                Response.Redirect("AdminLogin.aspx");
                return;
            }

            lblAdmin.Text = "Welcome, " + Session["Admin_UserName"].ToString();

            if (!IsPostBack)
            {
                BindBookingData();
            }
            //con = new SqlConnection(s);
            //if (!IsPostBack)
            //{
            //    BindBookingData();
            //}

            //if (Session["Admin_UserName"] != null)
            //{
            //    lblAdmin.Text = "Welcome, " + Session["Admin_UserName"].ToString();
            //}
            //else
            //{
            //    Response.Redirect("AdminLogin.aspx");
            //}
        }
        private void BindBookingData()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT B.Booking_Id, B.User_Id, U.User_Name, B.Package_Id, B.Image, B.Package_Name, B.Price FROM Booking_tbl B INNER JOIN user_tbl U ON B.User_Id = U.Id", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dlCartPackages.DataSource = dt;
            dlCartPackages.DataBind();
            con.Close();
        }
        protected void dlCartPackages_ItemCommand(object source, DataListCommandEventArgs e)
        {
            int bookingId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Approve")
            {
                ApproveBooking(bookingId);
            }
            else if (e.CommandName == "Decline")
            {
                DeclineBooking(bookingId);
            }

            BindBookingData();
        }

        private void ApproveBooking(int bookingId)
        {
            con.Open();
            SqlCommand getCmd = new SqlCommand("SELECT B.User_Id, U.User_Name, B.Package_Id, B.Image, B.Package_Name, B.Price FROM Booking_tbl B INNER JOIN user_tbl U ON B.User_Id = U.Id WHERE B.Booking_Id = @Booking_Id", con);
            getCmd.Parameters.AddWithValue("@Booking_Id", bookingId);
            SqlDataReader dr = getCmd.ExecuteReader();

            if (dr.Read())
            {
                string userId = dr["User_Id"].ToString();
                string userName = dr["User_Name"].ToString();
                string packageId = dr["Package_Id"].ToString();
                string image = dr["Image"].ToString();
                string packageName = dr["Package_Name"].ToString();
                string price = dr["Price"].ToString();
                dr.Close();

                SqlCommand insertCmd = new SqlCommand("INSERT INTO MyOrder_tbl (User_Id, User_Name, Package_Id, Image, Package_Name, Price) VALUES (@User_Id, @User_Name, @Package_Id, @Image, @Package_Name, @Price)", con);
                insertCmd.Parameters.AddWithValue("@User_Id", userId);
                insertCmd.Parameters.AddWithValue("@User_Name", userName);
                insertCmd.Parameters.AddWithValue("@Package_Id", packageId);
                insertCmd.Parameters.AddWithValue("@Image", image);
                insertCmd.Parameters.AddWithValue("@Package_Name", packageName);
                insertCmd.Parameters.AddWithValue("@Price", price);
                insertCmd.ExecuteNonQuery();

                SqlCommand deleteCmd = new SqlCommand("DELETE FROM Booking_tbl WHERE Booking_Id = @Booking_Id", con);
                deleteCmd.Parameters.AddWithValue("@Booking_Id", bookingId);
                deleteCmd.ExecuteNonQuery();
            }
            else
            {
                dr.Close();
            }

            con.Close();
        }

        private void DeclineBooking(int bookingId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Booking_tbl WHERE Booking_Id = @Booking_Id", con);
            cmd.Parameters.AddWithValue("@Booking_Id", bookingId);
            cmd.ExecuteNonQuery();
            con.Close();
        }


    }
}