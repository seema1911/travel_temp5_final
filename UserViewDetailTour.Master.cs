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
    public partial class UserViewDetailTour : System.Web.UI.MasterPage
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        classMngTour cs;
        PagedDataSource pg;
        int p, row;
        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();
            if (!IsPostBack)  // ✅ Ensure this runs only once per page load
            {
                ViewState["id"] = 0;  // ✅ Initialize page index
                display_second();  // ✅ Display tour data
            }
        }
        void getcon()
        {
            cs = new classMngTour();
            cs.startcon();
            //LinkButton4.Enabled = false;
        }
        protected void DataList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("UserShowTour.aspx");
        }

        void display_second()
        {
            try
            {
                string id = Session["SelectedTourID"].ToString();
                da = new SqlDataAdapter("SELECT * FROM tour_tbl WHERE Id = " + id, cs.startcon());
                ds = new DataSet();
                da.Fill(ds);
                row = ds.Tables[0].Rows.Count;

                if (row > 0)
                {
                    DataList2.DataSource = ds;
                    DataList2.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }


            //da = new SqlDataAdapter("SELECT * FROM tour_tbl", cs.startcon());
            //ds = new DataSet();
            //da.Fill(ds);
            //row = ds.Tables[0].Rows.Count;


            //pg.AllowPaging = true;
            //pg.PageSize = 1;
            //pg.DataSource = ds.Tables[0].DefaultView;
            //pg.CurrentPageIndex=Convert.ToInt32(ViewState["id"]);
            ////DataList2.DataSource = pg;
            ////DataList2.DataBind();

            //if (row > 0)
            //{
            //    pg = new PagedDataSource();
            //    pg.AllowPaging = true;
            //    pg.PageSize = 1;  
            //    pg.DataSource = ds.Tables[0].DefaultView;

            //    int pageIndex = Convert.ToInt32(ViewState["id"]);
            //    pg.CurrentPageIndex = (pageIndex >= 0 && pageIndex < pg.PageCount) ? pageIndex : 0; 

            //    DataList2.DataSource = pg;
            //    DataList2.DataBind(); 
            //    DataList2.Visible = true;  
            //}
            //else
            //{
            //    DataList2.Visible = false; 
            //}
        }
    }


}
