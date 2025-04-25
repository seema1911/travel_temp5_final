using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;


namespace travel_temp5_final
{
    public partial class Report : System.Web.UI.Page
    {
        ReportDocument cr = new ReportDocument();
        classMngTour cs;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;

        string s = ConfigurationManager.ConnectionStrings["tour_db"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Example: get ID from query string or set default
                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                    id = "1"; // fallback ID
                }

                using (SqlConnection con = new SqlConnection(s))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tour_tbl WHERE Id = @Id", con);
                    da.SelectCommand.Parameters.AddWithValue("@Id", id);

                    DataSet ds = new DataSet();
                    da.Fill(ds, "tour_tbl");

                    // Save schema for debugging (optional)
                    ds.WriteXmlSchema(Server.MapPath("~/data.xml"));

                    cr.Load(Server.MapPath("CrystalReport1.rpt")); // Load your .rpt file
                    cr.SetDataSource(ds.Tables["tour_tbl"]);
                    CrystalReportViewer1.ReportSource = cr;
                }
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (cr != null)
            {
                cr.Close();
                cr.Dispose();
            }
        }
    }
}