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
using SysApp;

namespace HRDOCS
{
    public partial class Cancle_ApproveHD_Detail : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string _docid;
        public Cancle_ApproveHD_Detail(string DocId)
        {
            _docid = DocId;

            InitializeComponent();
            InitializeDataGridView();

            GetData();
        }

        private void InitializeDataGridView()
        {
            #region SetDataGrid

            this.rgv_EmpData.Dock = DockStyle.Fill;
            this.rgv_EmpData.ReadOnly = true;
            this.rgv_EmpData.AutoGenerateColumns = true;
            this.rgv_EmpData.EnableFiltering = false;
            this.rgv_EmpData.AllowAddNewRow = false;
            this.rgv_EmpData.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_EmpData.ShowGroupedColumns = true;
            this.rgv_EmpData.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_EmpData.EnableHotTracking = true;
            this.rgv_EmpData.AutoSizeRows = true;

            GridViewTextBoxColumn TYPDESC = new GridViewTextBoxColumn();
            TYPDESC.Name = "TYPDESC";
            TYPDESC.FieldName = "TYPDESC";
            TYPDESC.HeaderText = "ประเภทเอกสาร";
            TYPDESC.IsVisible = true;
            TYPDESC.ReadOnly = true;
            TYPDESC.BestFit();
            rgv_EmpData.Columns.Add(TYPDESC);

            GridViewTextBoxColumn CHG_DOCID = new GridViewTextBoxColumn();
            CHG_DOCID.Name = "CHG_DOCID";
            CHG_DOCID.FieldName = "CHG_DOCID";
            CHG_DOCID.HeaderText = "เลขที่เอกสาร";
            CHG_DOCID.IsVisible = true;
            CHG_DOCID.ReadOnly = true;
            CHG_DOCID.BestFit();
            rgv_EmpData.Columns.Add(CHG_DOCID);

            GridViewTextBoxColumn COUNTDOC = new GridViewTextBoxColumn();
            COUNTDOC.Name = "COUNTDOC";
            COUNTDOC.FieldName = "COUNTDOC";
            COUNTDOC.HeaderText = "ลำดับ";
            COUNTDOC.IsVisible = true;
            COUNTDOC.ReadOnly = true;
            COUNTDOC.BestFit();
            rgv_EmpData.Columns.Add(COUNTDOC);

            GridViewTextBoxColumn FROMHOLIDAY = new GridViewTextBoxColumn();
            FROMHOLIDAY.Name = "FROMHOLIDAY";
            FROMHOLIDAY.FieldName = "FROMHOLIDAY";
            FROMHOLIDAY.HeaderText = "วันที่มาทำ";
            FROMHOLIDAY.IsVisible = true;
            FROMHOLIDAY.ReadOnly = true;
            FROMHOLIDAY.BestFit();
            rgv_EmpData.Columns.Add(FROMHOLIDAY);

            GridViewTextBoxColumn TOSHIFTID = new GridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.FieldName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "กะที่มาทำ";
            TOSHIFTID.IsVisible = true;
            TOSHIFTID.ReadOnly = true;
            TOSHIFTID.BestFit();
            rgv_EmpData.Columns.Add(TOSHIFTID);


            GridViewTextBoxColumn TOSHIFTDESC = new GridViewTextBoxColumn();
            TOSHIFTDESC.Name = "TOSHIFTDESC";
            TOSHIFTDESC.FieldName = "TOSHIFTDESC";
            TOSHIFTDESC.HeaderText = "คำอธิบาย";
            TOSHIFTDESC.IsVisible = true;
            TOSHIFTDESC.ReadOnly = true;
            TOSHIFTDESC.BestFit();
            rgv_EmpData.Columns.Add(TOSHIFTDESC);

            GridViewTextBoxColumn TOHOLIDAY = new GridViewTextBoxColumn();
            TOHOLIDAY.Name = "TOHOLIDAY";
            TOHOLIDAY.FieldName = "TOHOLIDAY";
            TOHOLIDAY.HeaderText = "วันที่หยุด";
            TOHOLIDAY.IsVisible = true;
            TOHOLIDAY.ReadOnly = true;
            TOHOLIDAY.BestFit();
            rgv_EmpData.Columns.Add(TOHOLIDAY);

            GridViewTextBoxColumn CHG_REASON = new GridViewTextBoxColumn();
            CHG_REASON.Name = "CHG_REASON";
            CHG_REASON.FieldName = "CHG_REASON";
            CHG_REASON.HeaderText = "เหตุผล";
            CHG_REASON.IsVisible = true;
            CHG_REASON.ReadOnly = true;
            CHG_REASON.BestFit();
            rgv_EmpData.Columns.Add(CHG_REASON);

            GridViewTextBoxColumn TRANSDATE = new GridViewTextBoxColumn();
            TRANSDATE.Name = "TRANSDATE";
            TRANSDATE.FieldName = "TRANSDATE";
            TRANSDATE.HeaderText = "วันที่บันทึก";
            TRANSDATE.IsVisible = true;
            TRANSDATE.ReadOnly = true;
            TRANSDATE.BestFit();
            rgv_EmpData.Columns.Add(TRANSDATE);

            #endregion
        }
        void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            DataTable dt = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            try
            {
                sqlCommand.CommandText = string.Format(

                         @"SELECT HD.DOCID
                               ,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.REASON
                               ,CHGHD.HOLIDAY1 ,CHGHD.HOLIDAY2
                               ,DOCTYP.TYPDESC ,DT.DOCREFER
                               
                            FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                   LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                   LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                   LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHGHD ON DT.DOCREFER = CHGHD.DOCID
	                               LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYDT] CHGDT ON CHGHD.DOCID = CHGDT.DOCID
                            WHERE  HD.DOCID = '{0}'
                            AND HD.HEADAPPROVED = '0'
                            AND HD.DOCSTAT = '1'
                            AND CHGHD.DOCSTAT = '1' ", _docid.ToString());
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtDocId.Text = reader["DOCID"].ToString();
                        txtEmpId.Text = reader["EMPLID"].ToString();
                        txtEmpName.Text = reader["EMPLNAME"].ToString();
                        txtSection.Text = reader["SECTIONNAME"].ToString();
                        txtPosition.Text = reader["DEPTNAME"].ToString();
                        txtholiday1.Text = reader["HOLIDAY1"].ToString();
                        txtHoloday2.Text = reader["HOLIDAY2"].ToString();
                        txtReason.Text = reader["REASON"].ToString();
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


            try
            {
                sqlCommand.CommandText = string.Format(

                           @"SELECT DOCTYP.TYPDESC,HD.DOCID,CHGHD.DOCID AS CHG_DOCID ,CHGDT.COUNTDOC
                                    ,DOCTYP.TYPDESC ,DT.DOCREFER
                                    ,CHGDT.FROMHOLIDAY,CHGDT.TOHOLIDAY,CHGDT.TOSHIFTID,CHGDT.TOSHIFTDESC ,CHGDT.REASON AS CHG_REASON
                                    ,CONVERT(VARCHAR,CHGHD.TRANSDATE,23) AS TRANSDATE
                            FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                   LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                   LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                   LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHGHD ON DT.DOCREFER = CHGHD.DOCID
	                               LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYDT] CHGDT ON CHGHD.DOCID = CHGDT.DOCID
                            WHERE  HD.DOCID = '{0}'
                            AND HD.HEADAPPROVED = '0'
                            AND HD.DOCSTAT = '1'
                            AND CHGHD.DOCSTAT = '1' ", _docid.ToString());

                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_EmpData.DataSource = dt;
                }
                else
                {
                    rgv_EmpData.DataSource = dt;
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
