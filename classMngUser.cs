using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace travel_temp5_final
{
    public class classMngUser
    {
        string s = ConfigurationManager.ConnectionStrings["tour_db"].ToString();
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

        public DataSet login(string username, string password)
        {
            DataSet ds = new DataSet();
            try
            {
                con = startcon();

                string query = "SELECT * FROM user_tbl WHERE User_Name = @User_Name AND Password = @Password";
                SqlDataAdapter da = new SqlDataAdapter(query, con);

                da.SelectCommand.Parameters.AddWithValue("@User_Name", username);
                da.SelectCommand.Parameters.AddWithValue("@Password", password);

                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return ds;
        }

        //public void startcon()
        //{
        //    con = new SqlConnection(s);
        //    con.Open();
        //}

        public SqlConnection startcon()
        {
            con = new SqlConnection(s);
            if (con.State == ConnectionState.Closed)
                con.Open();
            return con;  // ✅ Now it returns the connection
        }

        public void update(int id, string nm, string eml, string phon, string unm, string pass, string confpass, string gen, string h1, string h2, string h3, string dob, string add, string contry, string ct, string stat)
        {
            cmd = new SqlCommand("update user_tbl set  Name='" + nm + "', Email='" + eml + "',Phone='" + phon + "',User_Name='" + unm + "',Password='" + pass + "',Confirm_Password='" + confpass + "',Gender='" + gen + "',Hobby1='" + h1 + "',Hobby2='" + h2 + "',Hobby3='" + h3 + "',DOB='" + dob + "',Address='" + add + "',Country='" + contry + "',City='" + ct + "',State='" + stat + "'  WHERE Id='" + id + "'", con);
            cmd.ExecuteNonQuery();
        }

        public void insert(string profilepic, string nm, string eml, string phon, string unm, string pass, string confpass, string gen, string h1, string h2, string h3, string dob, string add, string contry, string ct, string stat)
        {
            cmd = new SqlCommand("insert into user_tbl(Profile_pic, Name, Email,Phone,User_Name,Password,Confirm_Password,Gender,Hobby1,Hobby2,Hobby3,DOB,Address,Country,City,State) values('"+ profilepic + "','"+nm+"','"+eml+"','"+phon+"','"+unm+"','"+pass+"','"+confpass+"','"+gen+"','"+h1+"','"+h2+"','"+h3+"','"+dob+"','"+add+"','"+contry+"','"+ct+"','"+stat+"')", con);
            cmd.ExecuteNonQuery();
        }

        //public void AddToCart(string userId, string Id, string image, string packageName, string duration, string price)
        //{
        //    cmd = new SqlCommand("INSERT INTO Cart_tbl (Id, Image, Package_Name, Duration, Price) " +
        //                         "VALUES ('" + Id + "', '" + Image + "', '" + packageName + "', '" + duration + "', '" + price + "')", con);
        //    cmd.ExecuteNonQuery();
        //}

        //public void AddToCart(String Id,string image, string package_Name, string Duration, string Price)
        //{
        //    cmd = new SqlCommand("insert into Cart_tbl( )", con);
        //    cmd.ExecuteNonQuery();
        //}

        
        public DataSet filldata()
        {
            da = new SqlDataAdapter("select * from user_tbl", con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet select(int id)
        {
            da = new SqlDataAdapter("SELECT * FROM user_tbl WHERE Id = '" + id + "'", con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}