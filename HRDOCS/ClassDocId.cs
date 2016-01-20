using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic; 

namespace HRDOCS
{
    class ClassDocId
    {
        public static string runDocno(string tablename, string fieldname, string doctype)
        {

            SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

            string sql = " select ";
            sql += " case when max(substring(" + fieldname + ",4,6)) <> convert(varchar,getdate(),12) then ";
            sql += " '" + doctype + "' + convert(varchar,getdate(),12) + '-0001' ";
            sql += " when max(substring(" + fieldname + ",4,6)) = convert(varchar,getdate(),12) then ";
            sql += " case ";
            sql += " when (select max(substring(" + fieldname + ",11,4))+1 from " + tablename + " where substring(" + fieldname + ",4,6) = convert(varchar,getdate(),12) and " + fieldname + " like '" + doctype + "%') <10 then '" + doctype + "' + convert(varchar,getdate(),12) + '-000' + convert(varchar,(select max(substring(" + fieldname + ",11,4))+1 from " + tablename + " where substring(" + fieldname + ",4,6) = convert(varchar,getdate(),12) and " + fieldname + " like '" + doctype + "%')) ";
            sql += " when (select max(substring(" + fieldname + ",11,4))+1 from " + tablename + " where substring(" + fieldname + ",4,6) = convert(varchar,getdate(),12) and " + fieldname + " like '" + doctype + "%') <100 then '" + doctype + "' + convert(varchar,getdate(),12) + '-00' + convert(varchar,(select max(substring(" + fieldname + ",11,4))+1 from " + tablename + " where substring(" + fieldname + ",4,6) = convert(varchar,getdate(),12) and " + fieldname + " like '" + doctype + "%')) ";
            sql += " when (select max(substring(" + fieldname + ",11,4))+1 from " + tablename + " where substring(" + fieldname + ",4,6) = convert(varchar,getdate(),12) and " + fieldname + " like '" + doctype + "%') <1000 then '" + doctype + "' + convert(varchar,getdate(),12) + '-0' + convert(varchar,(select max(substring(" + fieldname + ",11,4))+1 from " + tablename + " where substring(" + fieldname + ",4,6) = convert(varchar,getdate(),12) and " + fieldname + " like '" + doctype + "%')) ";
            sql += " else ";
            sql += " '" + doctype + "' + convert(varchar,getdate(),12) + '-' + convert(varchar,(select max(substring(" + fieldname + ",11,4))+1 from " + tablename + " where substring(" + fieldname + ",4,6) = convert(varchar,getdate(),12) and " + fieldname + " like '" + doctype + "%')) ";
            sql += " end ";
            sql += " else ";
            sql += " '" + doctype + "' + convert(varchar,getdate(),12) + '-0001' ";
            sql += " end ";
            sql += " as " + fieldname;
            sql += " from " + tablename;
            sql += " where " + fieldname + " like '" + doctype + "%' ";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "runDocno");
            string docno = ds.Tables["runDocno"].Rows[0][fieldname].ToString();
            da = null;
            return docno;
        }


    }
}
