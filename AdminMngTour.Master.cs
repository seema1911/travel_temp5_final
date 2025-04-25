using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Configuration;

namespace travel_temp5_final
{
    public partial class AdminMngTour : System.Web.UI.MasterPage
    {
        string s = ConfigurationManager.ConnectionStrings["tour_db"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        classMngTour cs;
        String fnm, s1, s2, s3;
        string[] Service = new string[3];

        void getcon()
        {
            cs = new classMngTour();
            //cs.startcon();
            con = cs.startcon();
        }
        void deleteRecord(int id)
        {
            getcon();
            cmd = new SqlCommand("delete from tour_tbl where Id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            //getcon();
            //cmd = new SqlCommand("delete from tour_tbl where Id=@id", con);
            //cmd.Parameters.AddWithValue("@id", ViewState["id"]);
            //cmd.ExecuteNonQuery();
            //fillgrid();
        }


        void fillcatgory()
        {
            getcon();
            da = new SqlDataAdapter("select * from add_catgory", cs.startcon());
            ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlcat.Items.Add(ds.Tables[0].Rows[i][1].ToString());
            }
        }

        void fillgrid()
        {
            cs = new classMngTour();
            getcon();
            GridView1.DataSource = cs.filldata();
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd_edt")
            {
                int id = Convert.ToInt16(e.CommandArgument);
                ViewState["id"] = id;
                btnadd.Text = "Update";
                filltext();
            }
            else if (e.CommandName == "cmd_delet")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                deleteRecord(id);
                fillgrid();
                //int id = Convert.ToInt16(e.CommandArgument);
                //deleteRecord();
                //fillgrid();
            }
        }

        void Services()
        {
            List<string> selectedServices = new List<string>();

            // Loop through the list and check for selected items
            foreach (ListItem item in chblistservis.Items)
            {
                if (item.Selected)
                {
                    selectedServices.Add(item.Text);
                }
            }

            // Ensure we capture up to 3 services, and if fewer, set them as "Null"
            s1 = selectedServices.Count > 0 ? selectedServices[0] : "Null";
            s2 = selectedServices.Count > 1 ? selectedServices[1] : "Null";
            s3 = selectedServices.Count > 2 ? selectedServices[2] : "Null";
            //for (int i = 0; i < Service.Length; i++)
            //{
            //    if (chblistservis.Items[i].Selected == true)
            //    {
            //        Service[i] = chblistservis.Items[i].Text;
            //    }
            //}
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (btnadd.Text == "Add Package")
            {
                getcon();
                Services();
                uploadimg();

                //for (int i = 0; i < 1; i++)
                //{
                //    if (Service[i] == "Meals")
                //    {
                //        s1 = "Meals";
                //        i++;
                //    }
                //    else
                //    {
                //        s1 = "Null";
                //        i++;
                //    }

                //    //2
                //    if (Service[i] == "Guided Sightseeing")
                //    {
                //        s2 = "Guided Sightseeing";
                //        i++;
                //    }
                //    else
                //    {
                //        s2 = "Null";
                //        i++;
                //    }

                //    //3
                //    if (Service[i] == "Entry Fees")
                //    {
                //        s3 = "Entry Fees";
                //        i++;
                //    }
                //    else
                //    {
                //        s3 = "Null";
                //    }
                //}

                cs.insert(fnm, ddlcat.SelectedValue, txtpkgnm.Text, txtstrtdt.Text, txtenddt.Text, txtdurtsn.Text, txtdestn.Text, txtdwistnti.Text, ddlstrtloctn.SelectedValue, ddlendloctn.SelectedValue, ddltrvlmod.SelectedValue, rdbllclinc.Text, s1, s2, s3, txtprice.Text);
                fillgrid();

            }

            else
            {
                cs = new classMngTour();
                getcon();
                Services();

                //for (int i = 0; i < 1; i++)
                //{
                //    if (Service[i] == "Meals")
                //    {
                //        s1 = "Meals";
                //        i++;
                //    }
                //    else
                //    {
                //        s1 = "Null";
                //        i++;
                //    }

                //    //2
                //    if (Service[i] == "Guided Sightseeing")
                //    {
                //        s2 = "Guided Sightseeing";
                //        i++;
                //    }
                //    else
                //    {
                //        s2 = "Null";
                //        i++;
                //    }

                //    //3
                //    if (Service[i] == "Entry Fees")
                //    {
                //        s3 = "Entry Fees";
                //        i++;
                //    }
                //    else
                //    {
                //        s3 = "Null";
                //    }
                //}


                cs.update(Convert.ToInt16(ViewState["id"]), ddlcat.SelectedValue, txtpkgnm.Text, txtstrtdt.Text, txtenddt.Text, txtdurtsn.Text, txtdestn.Text, txtdwistnti.Text, ddlstrtloctn.SelectedValue, ddlendloctn.SelectedValue, ddltrvlmod.SelectedValue, rdbllclinc.Text, s1, s2, s3, txtprice.Text);
                fillgrid();
            }
        }

        void filltext()
        {
            cs = new classMngTour();
            getcon();
            ds = cs.select(Convert.ToInt16(ViewState["id"]));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlcat.SelectedValue = ds.Tables[0].Rows[0][2].ToString();
                txtpkgnm.Text = ds.Tables[0].Rows[0][3].ToString();
                txtstrtdt.Text = ds.Tables[0].Rows[0][4].ToString();
                txtenddt.Text = ds.Tables[0].Rows[0][5].ToString();
                txtdurtsn.Text = ds.Tables[0].Rows[0][6].ToString();
                txtdestn.Text = ds.Tables[0].Rows[0][7].ToString();
                txtdwistnti.Text = ds.Tables[0].Rows[0][8].ToString();
                ddlstrtloctn.SelectedValue = ds.Tables[0].Rows[0][9].ToString();
                ddlendloctn.SelectedValue = ds.Tables[0].Rows[0][10].ToString();
                ddltrvlmod.SelectedValue = ds.Tables[0].Rows[0][11].ToString();
                txtprice.Text = ds.Tables[0].Rows[0][16].ToString();

                rdbllclinc.ClearSelection();
                string local = ds.Tables[0].Rows[0][12].ToString();
                ListItem item = rdbllclinc.Items.FindByText(local);
                if (item != null) item.Selected = true;

                chblistservis.ClearSelection();
                for (int i = 13; i <= 15; i++)
                {
                    string val = ds.Tables[0].Rows[0][i].ToString();
                    ListItem li = chblistservis.Items.FindByText(val);
                    if (li != null) li.Selected = true;
                }
            }

            //    cs = new classMngTour();
            //    getcon();
            //    ds = new DataSet();
            //    ds = cs.select(Convert.ToInt16(ViewState["id"]));
            //    ddlcat.SelectedValue = ds.Tables[0].Rows[0][2].ToString();
            //    txtpkgnm.Text = ds.Tables[0].Rows[0][3].ToString();
            //    txtstrtdt.Text = ds.Tables[0].Rows[0][4].ToString();
            //    txtenddt.Text = ds.Tables[0].Rows[0][5].ToString();
            //    txtdurtsn.Text = ds.Tables[0].Rows[0][6].ToString();
            //    txtdestn.Text = ds.Tables[0].Rows[0][7].ToString();
            //    txtdwistnti.Text = ds.Tables[0].Rows[0][8].ToString();
            //    ddlstrtloctn.SelectedValue = ds.Tables[0].Rows[0][9].ToString();
            //    ddlendloctn.SelectedValue = ds.Tables[0].Rows[0][10].ToString();
            //    ddltrvlmod.SelectedValue = ds.Tables[0].Rows[0][11].ToString();
            //    //rdbllclinc.Text = ds.Tables[0].Rows[0][11].ToString();
            //    txtprice.Text = ds.Tables[0].Rows[0][16].ToString();

            //    //rdb
            //    if (ds.Tables[0].Rows[0][12].ToString() == "Yes")
            //    {
            //        rdbllclinc.Items[0].Selected = true;
            //    }

            //    if (ds.Tables[0].Rows[0][12].ToString() == "No")
            //    {
            //        rdbllclinc.Items[1].Selected = true;
            //    }

            //    //chbox
            //    //itm 0
            //    if (ds.Tables[0].Rows[0][13].ToString() == "Meals")

            //    {
            //        chblistservis.Items[0].Selected = true;
            //    }
            //    else
            //    {
            //        chblistservis.Items[0].Selected = false;
            //    }

            //    //itm1
            //    if (ds.Tables[0].Rows[0][14].ToString() == " Guided Sightseeing")

            //    {
            //        chblistservis.Items[1].Selected = true;
            //    }
            //    else
            //    {
            //        chblistservis.Items[1].Selected = false;
            //    }
            //    //itm2

            //    if (ds.Tables[0].Rows[0][15].ToString() == "Entry Fees")

            //    {
            //        chblistservis.Items[2].Selected = true;
            //    }
            //    else
            //    {
            //        chblistservis.Items[2].Selected = false;
            //    }
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Services();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
           
            ddlcat.ClearSelection();
            txtpkgnm.Text = "";
            txtstrtdt.Text = "";
            txtenddt.Text = "";
            txtdurtsn.Text = "";
            txtdestn.Text = "";
            txtdwistnti.Text = "";
            ddlstrtloctn.ClearSelection();
            ddlendloctn.ClearSelection();
            ddltrvlmod.ClearSelection();
            rdbllclinc.ClearSelection();
            chblistservis.ClearSelection();
            txtprice.Text = "";
        }

        protected void ddlcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("select * from add_catgory where Category_Name=@cat", cs.startcon());
            da.SelectCommand.Parameters.AddWithValue("@cat", ddlcat.SelectedItem.ToString());
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["id"] = Convert.ToInt16(ds.Tables[0].Rows[0][0]);
            }
            //da = new SqlDataAdapter("select * from add_catgory where Category_Name='" + ddlcat.SelectedItem.ToString() + "'", cs.startcon());
            //ds = new DataSet();
            //da.Fill(ds);
            //ViewState["id"] = Convert.ToInt16(ds.Tables[0].Rows[0][0]);

        }

        void uploadimg()
        {
            if (fldimg.HasFile)
            {
                fnm = "images/" + fldimg.FileName;
                fldimg.SaveAs(Server.MapPath(fnm));

            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(s);
            if (Session["Admin_UserName"] == null)
            {
                Response.Redirect("AdminLogin.aspx");
                return;
            }
            lblAdmin.Text = "Welcome, " + Session["Admin_UserName"].ToString();


            getcon();
            fillgrid();
            
            if (!IsPostBack)
            {
                fillcatgory();
            }
        }
    }
}