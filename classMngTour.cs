using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace travel_temp5_final
{
    public class classMngTour
    {

        string s = ConfigurationManager.ConnectionStrings["tour_db"].ToString();
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

        public SqlConnection startcon()
        {
            con = new SqlConnection(s);
            if (con.State == ConnectionState.Closed)
                con.Open();
            return con;  // ✅ Now it returns the connection
        }

        //public void startcon()
        //{
        //    con = new SqlConnection(s);
        //    con.Open();
        //}

        public void insert(string img, string cat, string pktnm, string strtdt, string enddt, string durtan, string destan, string dywisitine, string strtloctn, string endloctn, string trvlmod, string ltinc, string servis1, string servis2, string servis3, string price)
        {
            cmd = new SqlCommand("insert into tour_tbl (Image,Category,Package_Name,Start_Date,End_Date,Duration,Destination,Day_wise_Itinerary,Start_Location,End_Location,Travel_Mode,Local_Transfer_Included,Service_Inclusions1,Service_Inclusions2,Service_Inclusions3,Price) " +
                "values('" + img + "','" + cat + "','" + pktnm + "','" + strtdt + "','" + enddt + "','" + durtan + "','" + destan + "','" + dywisitine + "','" + strtloctn + "','" + endloctn + "','" + trvlmod + "','" + ltinc + "','" + servis1 + "','" + servis2 + "','" + servis3 + "','" + price + "')", con);
            cmd.ExecuteNonQuery();
        }

        public void update(int id, string cat, string pktnm, string strtdt, string enddt, string durtan, string destan, string dywisitine, string strtloctn, string endloctn, string trvlmod, string ltinc, string servis1, string servis2, string servis3, string price)
        {
            cmd = new SqlCommand("update tour_tbl set Category='" + cat + "', Package_Name='" + pktnm + "',Start_Date='" + strtdt + "',End_Date='" + enddt + "',Duration='" + durtan + "',Destination='" + destan + "',Day_wise_Itinerary='" + dywisitine + "',Start_Location='" + strtloctn + "',End_Location='" + endloctn + "',Travel_Mode='" + trvlmod + "',Local_Transfer_Included='" + ltinc + "',Service_Inclusions1='" + servis1 + "',Service_Inclusions2='" + servis2 + "',Service_Inclusions3='" + servis3 + "',Price='" + price + "' where Id='" + id + "' ", con);
            cmd.ExecuteNonQuery();
        }


        public DataSet filldata()
        {
            da = new SqlDataAdapter("select * from tour_tbl", con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void addcatgory(string txtcatg)
        {
            cmd = new SqlCommand("insert into add_catgory(Category_Name) values('" + txtcatg + "') ", con);
            cmd.ExecuteNonQuery();
        }

        public DataSet select(int id)
        {
            da = new SqlDataAdapter("select * from tour_tbl where Id='" + id + "'", con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }



    }
}