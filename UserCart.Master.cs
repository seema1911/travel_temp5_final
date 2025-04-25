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
    public partial class UserCart : System.Web.UI.MasterPage
    {
        string s = ConfigurationManager.ConnectionStrings["tour_db"].ToString();
        classMngUser cs;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                connection();
                loadcart();
                BindCartItems();
            }
            if (Session["User_Name"] != null)
            {

                lblusr.Text = "Welcome, " + Session["User_Name"].ToString();

            }
            else
            {
                Response.Redirect("UserLogin.aspx");
            }
            //connection();
            //loadcart();
        }

        void connection()
        {
            cs = new classMngUser();
            cs.startcon();
        }

        void loadcart()
        {
            //connection();
            //string userId = Session["userId"]?.ToString();
            string userId = Session["userId"]?.ToString();
            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    da = new SqlDataAdapter("SELECT * FROM Cart_tbl WHERE user_Id = @userId", cs.startcon());
                    da.SelectCommand.Parameters.AddWithValue("@userId", userId);

                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        Response.Write("No items in the cart.");
                    }
                    else
                    {
                        dlCartPackages.DataSource = ds;
                        dlCartPackages.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
            else
            {
                Response.Write("User ID is null or empty.");
            }
            //if (userId != null)
            //if (!string.IsNullOrEmpty(userId))
            //{
            //    da = new SqlDataAdapter("SELECT * FROM Cart_tbl WHERE user_Id='" + userId + "'", cs.startcon());
            //    ds = new DataSet();
            //    da.Fill(ds);
            //    dlCartPackages.DataSource = ds;
            //    dlCartPackages.DataBind();
            //}
        }


        protected void dlCartPackages_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBookAll_Click(object sender, EventArgs e)
        {

        }

        private void BindCartItems()
        {
            string userId = Session["userId"]?.ToString();
            if (!string.IsNullOrEmpty(userId))
            {
                string query = "SELECT c.Cart_Id, c.Package_Id, c.Image, c.Package_Name, c.Price, c.Duration FROM Cart_tbl c WHERE c.User_Id = @UserId";


                using (SqlConnection con = new SqlConnection(s))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", Session["userId"]);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dlCartPackages.DataSource = dt;
                    dlCartPackages.DataBind();
                }
            }
            else
            {
                Response.Write("Session userId is missing. Please login again.");
            }
        }

        protected void dlCartPackages_ItemCommand(object source, DataListCommandEventArgs e)
        {
            connection();

            int cartId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Remove")
            {
                RemoveFromCart(cartId);
            }

            else if (e.CommandName == "PayNow")
            {
                PayNow(cartId, e);
            }

            else if (e.CommandName == "BookPackage")
            {
                LinkButton btnPay = (LinkButton)e.Item.FindControl("LinkButton1");
                if (btnPay != null && btnPay.Text == "Paid")
                {
                    string bookQuery = @"INSERT INTO Booking_tbl (user_Id, Package_Id, Image, Package_Name, Duration, Price)
                     SELECT User_Id, Package_Id, Image, Package_Name, Duration, Price 
                     FROM Cart_tbl WHERE Cart_Id = @CartId";

                    using (SqlConnection con = new SqlConnection(s))
                    {
                        SqlCommand cmd = new SqlCommand(bookQuery, con);
                        cmd.Parameters.AddWithValue("@CartId", cartId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // Optionally, remove the item from the cart after booking
                    string deleteQuery = "DELETE FROM Cart_tbl WHERE Cart_Id = @CartId";
                    using (SqlConnection con = new SqlConnection(s))
                    {
                        SqlCommand cmd = new SqlCommand(deleteQuery, con);
                        cmd.Parameters.AddWithValue("@CartId", cartId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    BindCartItems(); // Rebind to update the cart
                                     //Response.Redirect("UserBookNOW.aspx");

                }
            }

        }


        private void PayNow(int cartId, DataListCommandEventArgs e)
        {
            // Insert into booking
            LinkButton btnPay = (LinkButton)e.Item.FindControl("LinkButton1");
            if (btnPay != null)
            {
                btnPay.Text = "Paid";
                btnPay.Enabled = false;
            }

            // Enable Book button
            LinkButton btnBook = (LinkButton)e.Item.FindControl("btnBook");
            if (btnBook != null)
            {
                btnBook.Enabled = true;
            }

            Response.Write("<script>alert('Payment Successful! You can now book the package.');</script>");

        }


        private void RemoveFromCart(int cartId)
        {
            try
            {
                connection();
                cmd = new SqlCommand("DELETE FROM Cart_tbl WHERE Cart_Id=@CartId", cs.startcon());
                cmd.Parameters.AddWithValue("@CartId", cartId);
                cmd.ExecuteNonQuery();
                loadcart();
            }
            catch (Exception ex)
            {
                // Handle error (could log the error or show a message to the user)
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }




    }
}