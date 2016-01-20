using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SysApp;
using System.Data;

namespace HRDOCS
{
    class MyTime
    {
        public static DateTime GetDateTime()
        {
            SqlConnection Conn656 = new SqlConnection(DatabaseConfig.ServerConStr);
            string sql = "select getdate() as Datetime";
            SqlDataAdapter da = new SqlDataAdapter(sql, Conn656);
            DataSet ds = new DataSet();
            da.Fill(ds, "Datetime");

            return Convert.ToDateTime(ds.Tables["Datetime"].Rows[0]["Datetime"].ToString());
        }

        public static DateTime GetDate()
        {
            SqlConnection Conn656 = new SqlConnection(DatabaseConfig.ServerConStr);
            string sql = "select convert(varchar,getdate(),23) as Date";
            SqlDataAdapter da = new SqlDataAdapter(sql, Conn656);
            DataSet ds = new DataSet();
            da.Fill(ds, "Date");

            return Convert.ToDateTime(ds.Tables["Date"].Rows[0]["Date"].ToString());
        }

    }
}
