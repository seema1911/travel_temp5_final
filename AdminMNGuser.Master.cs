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
    public partial class AdminMNGuser : System.Web.UI.MasterPage
    {

        string s = ConfigurationManager.ConnectionStrings["tour_db"].ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        classMngUser cs;
        string fnm, h1, h2, h3;
        string[] hb = new string[3];



        void empty()
        {
            txtfnm.Text = "";
            txteml.Text = "";
            txtphon.Text = "";
            txtunm.Text = "";
            txtpass.Text = "";
            txtconfpass.Text = "";
            rdbgen.ClearSelection();
            chblhob.ClearSelection();
            txtdob.Text = "";
            txtadd.Text = "";
            ddlcontry.ClearSelection();
            ddlct.ClearSelection();
            ddlstate.ClearSelection();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd_edt")
            {
                int id = Convert.ToInt16(e.CommandArgument);
                ViewState["id"] = id;
                btnview.Text = "Update";
                filltext();
                //empty();
            }
            else if (e.CommandName == "cmd_delet")
            {
                int id = Convert.ToInt16(e.CommandArgument);
                ViewState["id"] = id;
                deleteRecord(id);
                referesh();
            }
        }

        void deleteRecord(int id)
        {
            using (con = new SqlConnection(s))
            {
                con.Open();
                cmd = new SqlCommand("DELETE FROM user_tbl Where Id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            referesh();
        }

        void referesh()
        {
            using (con = new SqlConnection(s))
            {
                con.Open();
                da = new SqlDataAdapter("SELECT * FROM user_tbl", con);
                ds = new DataSet();
                da.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            //da = new SqlDataAdapter("select * from user_tbl", con);
            //ds = new DataSet();
            //da.Fill(ds);
            //GridView1.DataSource = ds;
            //GridView1.DataBind();

        }

        void fillgrid()
        {
            cs = new classMngUser();
            getcon();
            GridView1.DataSource = cs.filldata();
            GridView1.DataBind();
        }

        void filltext()
        {
            cs = new classMngUser();
            getcon();
            ds = new DataSet();
            ds = cs.select(Convert.ToInt16(ViewState["id"]));

            txtfnm.Text = ds.Tables[0].Rows[0][2].ToString();
            txteml.Text = ds.Tables[0].Rows[0][3].ToString();
            txtphon.Text = ds.Tables[0].Rows[0][4].ToString();
            txtunm.Text = ds.Tables[0].Rows[0][5].ToString();
            txtpass.Text = ds.Tables[0].Rows[0][6].ToString();
            txtconfpass.Text = ds.Tables[0].Rows[0][7].ToString();
            rdbgen.SelectedValue = ds.Tables[0].Rows[0][8].ToString();
            txtdob.Text = ds.Tables[0].Rows[0][12].ToString();
            txtadd.Text = ds.Tables[0].Rows[0][13].ToString();
            ddlcontry.SelectedValue = ds.Tables[0].Rows[0][14].ToString();
            ddlct.SelectedValue = ds.Tables[0].Rows[0][15].ToString();
            ddlstate.SelectedValue = ds.Tables[0].Rows[0][16].ToString();

            if (ds.Tables[0].Rows[0][9].ToString() == "Reading")
            {
                chblhob.Items[0].Selected = true;
            }
            else
            {
                chblhob.Items[0].Selected = false;
            }

            if (ds.Tables[0].Rows[0][10].ToString() == "Chess")
            {
                chblhob.Items[1].Selected = true;
            }
            else
            {
                chblhob.Items[1].Selected = false;
            }

            if (ds.Tables[0].Rows[0][11].ToString() == "Chess")
            {
                chblhob.Items[2].Selected = true;
            }
            else
            {
                chblhob.Items[2].Selected = false;
            }


        }


        void getcon()
        {
            cs = new classMngUser();
            cs.startcon();
            if (con == null || con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
            {
                con = new SqlConnection(s);
                con.Open();
            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            if (btnview.Text == "Add")
            {
                getcon();
                Hobbie();
                uploadimg();

                //for (int i = 0; i < 1; i++)
                //{
                //    if (hb[i] == "Reading")
                //    {
                //        h1 = "Reading";
                //        i++;
                //    }
                //    else
                //    {
                //        h1 = "Null";
                //        i++;
                //    }
                //}

                //for (int i = 0; i < 1; i++)
                //{
                //    if (hb[i] == "Chess")
                //    {
                //        h2 = "Chess";
                //        i++;
                //    }
                //    else
                //    {
                //        h2 = "Null";
                //        i++;
                //    }
                //}

                //for (int i = 0; i < 1; i++)
                //{
                //    if (hb[i] == "Other")
                //    {
                //        h3 = "Other";
                //        i++;
                //    }
                //    else
                //    {
                //        h3 = "Null";
                //        i++;
                //    }
                //}

                h1 = hb[0];
                h2 = hb[1];
                h3 = hb[2];

                cs.insert(fnm, txtfnm.Text, txteml.Text, txtphon.Text, txtunm.Text, txtpass.Text, txtconfpass.Text, rdbgen.Text, h1, h2, h3, txtdob.Text, txtadd.Text, ddlcontry.SelectedValue, ddlct.SelectedValue, ddlstate.SelectedValue);
                fillgrid();
                empty();
            }
            else
            {

                cs = new classMngUser();
                getcon();
                Hobbie();

                h1 = hb[0];
                h2 = hb[1];
                h3 = hb[2];

                //for (int i = 0; i < 1; i++)
                //{
                //    if (hb[i] == "Reading")
                //    {
                //        h1 = "Reading";
                //        i++;
                //    }
                //    else
                //    {
                //        h1 = "Null";
                //        i++;

                //    }
                //    if (hb[i] == "Chess")
                //    {
                //        h2 = "Chess";
                //        i++;
                //    }
                //    else
                //    {
                //        h2 = "Null";
                //        i++;

                //    }
                //    if (hb[i] == "Other")
                //    {
                //        h3 = "Other";
                //        i++;
                //    }
                //    else
                //    {
                //        h3 = "Null";

                //    }

                //}
                cs.update(Convert.ToInt16(ViewState["id"]),txtfnm.Text, txteml.Text, txtphon.Text, txtunm.Text, txtpass.Text, txtconfpass.Text, rdbgen.Text, h1, h2, h3, txtdob.Text, txtadd.Text, ddlcontry.SelectedValue, ddlct.SelectedValue, ddlstate.SelectedValue);
                fillgrid();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            empty();
            btnview.Text = "Add";
        }

        void Hobbie()
        {
            for (int i = 0; i < hb.Length; i++)
            {
                if (chblhob.Items[i].Selected == true)
                {
                    hb[i] = chblhob.Items[i].Text;
                }
            }
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
            getcon();
            fillgrid();

            con = new SqlConnection(s);
            if (Session["Admin_UserName"] == null)
            {
                Response.Redirect("AdminLogin.aspx");
                return;
            }
            lblAdmin.Text = "Welcome, " + Session["Admin_UserName"].ToString();
        }
    }
}