using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Telerik.WinControls.Primitives;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using System.IO;
using System.Threading;

namespace HROUTOFFICE
{
    public partial class FormDetailHrApprove : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);

        public FormDetailHrApprove(string DocId, string OutId )
        {
            InitializeComponent();
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;
            
            radLabelDocId.Text = DocId;
            radLabelOutId.Text = OutId;
           

            this.radPanel1.Dock = DockStyle.Fill;
            this.radGroupBox1.Dock = DockStyle.Fill;
        }

        private void FormDetailHrApprove_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                string sql = @"SELECT [OutOfficeId] ,[EmplId],[EmplFname] + ' ' +[EmplLname] As EmplFullName,[Dimention],[Dept]
                                        ,[ShiftId],[StartTime],[EndTime]
                                        ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                        ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
                                        ,CASE [TruckType] WHEN '1' THEN 'รถส่วนตัว' WHEN '2' THEN 'รถบริษัท' ELSE 'ไม่มีข้อมูล' END AS [TruckType]
                                        ,[TruckId],[Reason] 
                                        ,CASE [HeadApproved] WHEN '1' THEN 'รออนุมัติ' WHEN '2' THEN 'อนุมัติ' ELSE 'ไม่มีข้อมูล' END AS [HeadApproved]
                                        ,[HeadApprovedName], HeadApprovedDateTime
                                  FROM [IVZ_HROUTOFFICE] 
                                  WHERE [DocId] = '" + radLabelDocId.Text.ToString() + "'  AND [OutOfficeId] = '" + radLabelOutId.Text.ToString() + "' ";

                //SqlCommand cmd = new SqlCommand(sql, con);


                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        radLabelEmplName.Text = reader["EmplFullName"].ToString();
                        radLabelEmplId.Text = reader["EmplId"].ToString();
                        radLabelDimention.Text = reader["Dimention"].ToString();
                        radLabelDept.Text = reader["Dept"].ToString();
                        radLabelShiftId.Text = reader["ShiftId"].ToString();
                        radLabel1StartTime.Text = reader["StartTime"].ToString();
                        radLabelEndTime.Text = reader["EndTime"].ToString();
                        radLabelOutType.Text = reader["OutType"].ToString();
                        radLabelCombackType.Text = reader["CombackType"].ToString();
                        radLabelTruckType.Text = reader["TruckType"].ToString();
                        radLabelTruckId.Text = reader["TruckId"].ToString();
                        radLabelReason.Text = reader["Reason"].ToString();
                        radLabelHdStatus.Text = reader["HeadApproved"].ToString();
                        radLabelHdApprove.Text = reader["HeadApprovedName"].ToString();
                        radLabelHdApprovedDateTime.Text = reader["HeadApprovedDateTime"].ToString();
                        
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

    }
}
