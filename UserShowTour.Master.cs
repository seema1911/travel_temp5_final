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
    public partial class UserShowTour : System.Web.UI.MasterPage
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        classMngTour ck;
        classMngUser cs;
        PagedDataSource pg;
        SqlCommand cmd;
        int p, row;
        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();
            display();
            if (Session["User_Name"] != null)
            {

                lblusr.Text = "Welcome, " + Session["User_Name"].ToString();

            }
            else
            {
                Response.Redirect("UserLogin.aspx");
            }
        }

        void connection()
        {
            cs = new classMngUser();
            cs.startcon();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //LinkButton3.Enabled = true;
            //p += Convert.ToInt32(ViewState["id"]) - 1;
            //ViewState["id"] = Convert.ToInt32(p);

            //if (p == 0)
            //{
            //    LinkButton3.Enabled = false;
            //    LinkButton4.Enabled = true;
            //}

            //display();

            p = Convert.ToInt32(ViewState["id"]);
            p--;

            if (p <= 0)
            {
                p = 0;
                ImageButton1.Enabled = false; // disable if at start
            }

            ViewState["id"] = p;
            ImageButton2.Enabled = true; // enable Next
            display();

            //if (p == 0)
            //{
            //    ImageButton1.Enabled = false; // Disable Previous button on first page
            //}
            //p = Convert.ToInt32(ViewState["id"]) - 1; // Move to the previous page
            //if (p < 0) p = 0; // Prevent negative values

            //ViewState["id"] = p;
            //ImageButton2.Enabled = true; // Enable Next button
            //display();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton1.Enabled = true;

            p += Convert.ToInt32(ViewState["id"]) + 1;
            ViewState["id"] = Convert.ToInt32(p);
            int temp = row / pg.PageSize;
            if (p == temp)
            {
                ImageButton2.Enabled = false;
                ImageButton1.Enabled = true;
            }
            display();
        }

        void display()
        {
            da = new SqlDataAdapter("select * from tour_tbl", cs.startcon());
            ds = new DataSet();
            da.Fill(ds);
            row = ds.Tables[0].Rows.Count;
            pg = new PagedDataSource();
            pg.AllowPaging = true;
            pg.PageSize = 3;
            pg.DataSource = ds.Tables[0].DefaultView;
            pg.CurrentPageIndex = Convert.ToInt32(ViewState["id"]);
            DataList1.DataSource = pg;
            DataList1.DataBind();
        }

        //void display_second()
        //{
        //    if (ViewState["selectedId"] != null) // Ensure an ID is selected
        //    {
        //        string selectedId = ViewState["selectedId"].ToString();

        //        da = new SqlDataAdapter("SELECT * FROM tour_tbl WHERE Id = @Id", cs.startcon());
        //        da.SelectCommand.Parameters.AddWithValue("@Id", selectedId);
        //        ds = new DataSet();
        //        da.Fill(ds);

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            DataList2.DataSource = ds;  // ✅ Correct binding
        //            DataList2.DataBind();       // ✅ Correct DataBind
        //            DataList2.Visible = true;   // ✅ Show DataList2
        //        }
        //        else
        //        {
        //            DataList2.Visible = false; // Hide if no data found
        //        }
        //    }
        //}

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            //    if (e.CommandName == "cmd_vwmor") // When "View More" is clicked
            //    {
            //        //string selectedId = e.CommandArgument.ToString();
            //        //ViewState["selectedId"] = selectedId; // Store selected ID in ViewState
            //        //display_second(); // Call the method to show detailed data
            //        Response.Redirect("UserViewDetailTour");
            //    }


            if (e.CommandName == "cmd_vwmor") // When "View More" is clicked
            {
                string selectedId = e.CommandArgument.ToString(); // Get the selected ID
                Session["SelectedTourID"] = selectedId; // Store it in a session variable

                Response.Redirect("UserViewDetailTour.aspx"); // Redirect to details page
            }
            else if (e.CommandName == "cmd_by")
            {
                //int cartId = Convert.ToInt32(e.CommandArgument);
                //// Redirect to booking page with Cart ID as a query string or handle booking directly
                //Response.Redirect("UserCart.aspx?CartId=" + cartId);
                string userId = Session["userId"]?.ToString();
                string packageId = e.CommandArgument.ToString(); // Get the selected package ID

                if (!string.IsNullOrEmpty(userId))
                {
                    // Fetch package details from Package_tbl based on Package_Id
                    connection(); // Assuming this is your method to establish a DB connection
                    cmd = new SqlCommand("SELECT * FROM tour_tbl WHERE Id = @packageId", cs.startcon());
                    cmd.Parameters.AddWithValue("@packageId", packageId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) // Package exists
                    {
                        string packageName = reader["Package_Name"].ToString();
                        string price = reader["Price"].ToString();
                        string duration = reader["Duration"].ToString();
                        string image = reader["Image"].ToString();

                        // Insert the package details into the Cart_tbl for the logged-in user
                        cmd = new SqlCommand("INSERT INTO Cart_tbl (user_Id, Package_Id, Package_Name, Price, Duration, Image) " +
                                             "VALUES (@userId, @packageId, @packageName, @price, @duration, @image)", cs.startcon());
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@packageId", packageId);
                        cmd.Parameters.AddWithValue("@packageName", packageName);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@duration", duration);
                        cmd.Parameters.AddWithValue("@image", image);
                        cmd.ExecuteNonQuery();

                        // Optionally, you can show a message or redirect to the cart page
                        Response.Redirect("UserCart.aspx"); // Redirecting to the cart page
                    }
                }
                else
                {
                    // Handle case where user is not logged in
                    Response.Redirect("UserLogin.aspx"); // Redirect to login page
                }
            }
        }



        //if (e.CommandName == "cmd_by")
        //{
        //    string userId = Session["userid"]?.ToString();  // Get user ID from session

        //    if (!string.IsNullOrEmpty(userId))
        //    {
        //        int packageId = Convert.ToInt32(e.CommandArgument); // Read PackageId from CommandArgument
        //        classMngUser cu = new classMngUser();
        //        cu.AddToCart(userId, packageId);

        //        Response.Write("<script>alert('Tour package added to cart successfully!');</script>");
        //    }
        //    else
        //    {
        //        Response.Redirect("UserLogin.aspx");
        //    }
        //}


        //else if (e.CommandName == "cmd_by")
        //{
        //    getcon();

        //    if (cs.startcon().State != ConnectionState.Open)
        //    {
        //        cs.startcon().Open();
        //    }

        //    SqlConnection con = cs.startcon();  // open connection once

        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT Id, Image, Package_Name, Duration, Price FROM add_catgory WHERE Id = @Id", con);
        //        cmd.Parameters.AddWithValue("@Id", ViewState["id"]);

        //        SqlDataReader dr = cmd.ExecuteReader();

        //        if (dr.Read())
        //        {
        //            string id = dr["Id"].ToString();
        //            string image = dr["Image"].ToString();
        //            string packageName = dr["Package_Name"].ToString();
        //            string duration = dr["Duration"].ToString();
        //            string price = dr["Price"].ToString();
        //            dr.Close();

        //            // Close connection only after reading is done
        //            con.Close();

        //            // ✅ Add to cart (you need to match this signature with your method)
        //            cs.AddToCart(Session["UserId"].ToString(), Convert.ToInt32(id));
        //        }
        //        else
        //        {
        //            dr.Close();
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle errors
        //        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
        //        if (con.State == ConnectionState.Open)
        //            con.Close();
        //    }

        //}

        void getcon()
        {
            ck = new classMngTour();
            cs = new classMngUser();
            cs.startcon();
            //LinkButton4.Enabled = false;
        }



    }
}