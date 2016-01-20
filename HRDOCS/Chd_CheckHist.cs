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
    public partial class Chd_CheckHist : Form
    {
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string _empid;
        string ApproveID = "";

        public Chd_CheckHist(string Empid)
        {
            _empid = Empid;
            InitializeComponent();
            InitializeradGridview();
            this.CheckApproveID();
            this.GetData();
        }
        public void InitializeradGridview()
        {
            this.rgv_CheckHist.Dock = DockStyle.Fill;
            this.rgv_CheckHist.ReadOnly = true;
            this.rgv_CheckHist.AutoGenerateColumns = true;
            this.rgv_CheckHist.EnableFiltering = false;
            this.rgv_CheckHist.AllowAddNewRow = false;
            this.rgv_CheckHist.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_CheckHist.ShowGroupedColumns = true;
            this.rgv_CheckHist.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_CheckHist.EnableHotTracking = true;
            this.rgv_CheckHist.AutoSizeRows = true;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_CheckHist.Columns.Add(DOCID);

            GridViewTextBoxColumn EMPLID = new GridViewTextBoxColumn();
            EMPLID.Name = "EMPLID";
            EMPLID.FieldName = "EMPLID";
            EMPLID.HeaderText = "รหัสพนักงาน";
            EMPLID.IsVisible = true;
            EMPLID.ReadOnly = true;
            EMPLID.BestFit();
            rgv_CheckHist.Columns.Add(EMPLID);

            GridViewTextBoxColumn EMPLNAME = new GridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.FieldName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ - สกุล";
            EMPLNAME.IsVisible = true;
            EMPLNAME.ReadOnly = true;

            EMPLNAME.BestFit();
            rgv_CheckHist.Columns.Add(EMPLNAME);

            GridViewTextBoxColumn FROMHOLIDAY = new GridViewTextBoxColumn();
            FROMHOLIDAY.Name = "FROMHOLIDAY";
            FROMHOLIDAY.FieldName = "FROMHOLIDAY";
            FROMHOLIDAY.HeaderText = "วันที่มาทำ";
            FROMHOLIDAY.IsVisible = true;
            FROMHOLIDAY.ReadOnly = true;
            FROMHOLIDAY.BestFit();
            rgv_CheckHist.Columns.Add(FROMHOLIDAY);

            GridViewTextBoxColumn TOSHIFTID = new GridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.FieldName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "รหัสกะ";
            TOSHIFTID.IsVisible = true;
            TOSHIFTID.ReadOnly = true;
            TOSHIFTID.BestFit();
            rgv_CheckHist.Columns.Add(TOSHIFTID);

            GridViewTextBoxColumn TOSHIFTDESC = new GridViewTextBoxColumn();
            TOSHIFTDESC.Name = "TOSHIFTDESC";
            TOSHIFTDESC.FieldName = "TOSHIFTDESC";
            TOSHIFTDESC.HeaderText = "คำอธิบาย";
            TOSHIFTDESC.IsVisible = true;
            TOSHIFTDESC.ReadOnly = true;
            TOSHIFTDESC.BestFit();
            rgv_CheckHist.Columns.Add(TOSHIFTDESC);

            GridViewTextBoxColumn REASON = new GridViewTextBoxColumn();
            REASON.Name = "REASON";
            REASON.FieldName = "REASON";
            REASON.HeaderText = "เหตุผล";
            REASON.IsVisible = true;
            REASON.ReadOnly = true;
            REASON.BestFit();
            rgv_CheckHist.Columns.Add(REASON);
        }
        void CheckApproveID()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                Cursor.Current = Cursors.WaitCursor;

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@" SELECT * FROM SPC_CM_AUTHORIZE
                                                              WHERE EMPLID = '{0}' 
                                                              ORDER BY APPROVEID ", ClassCurUser.LogInEmplId);
                    //, ClassCurUser.LogInEmplId);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            if (dataTable.Rows.Count > 0)
                            {
                                dataTable.Clear();
                            }
                            dataTable.Load(sqlDataReader);
                        }
                    }
                }
                if (dataTable.Rows.Count > 0)
                {
                    ApproveID = dataTable.Rows[0]["APPROVEID"].ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                if (con.State == ConnectionState.Open) con.Close();
            }

        }
        void GetData()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(
                    @"SELECT A.* FROM
                                        (
	                                        SELECT HD.DOCID, HD.EMPLID ,HD.EMPLNAME,DT.FROMHOLIDAY AS FROMHOLIDAY ,DT.TOSHIFTID AS TOSHIFTID ,DT.TOSHIFTDESC AS TOSHIFTDESC
	                                        ,CASE DT.TOHOLIDAY WHEN '0000-00-00' THEN '* แจ้งภายหลัง' ELSE DT.TOHOLIDAY END AS TOHOLIDAY  ,DT.REASON AS REASON
	                                        FROM SPC_JN_CHANGHOLIDAYDT DT
	                                        LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD ON DT.DOCID = HD.DOCID
	                                        WHERE DOCSTAT = 1 
	                                        AND HD.HRAPPROVED = 1
	                                        AND DT.TOHOLIDAY = '0000-00-00'
	                                        AND HD.EMPLID = '{0}'
                                        )
                                        A LEFT OUTER JOIN
                                        (
	                                        SELECT HD.DOCID ,DT.FROMHOLIDAY AS FROMHOLIDAY 
	                                        FROM SPC_JN_CHANGHOLIDAYDT DT
	                                        LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD ON DT.DOCID = HD.DOCID
	                                        WHERE DOCSTAT = 1 
	                                        AND DT.FROMHOLIDAY IN
	                                        ( 
		                                        SELECT DT.FROMHOLIDAY FROM SPC_JN_CHANGHOLIDAYDT DT
		                                        LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD ON DT.DOCID = HD.DOCID
		                                        WHERE DOCSTAT = 1
		                                        AND HD.HRAPPROVED = 1
		                                        AND DT.TOHOLIDAY = '0000-00-00'
		                                        AND HD.EMPLID = '{0}'
	                                        )
	                                        AND DT.TOHOLIDAY != '0000-00-00'
	                                        AND HD.EMPLID = '{0}'
                                        )B 
                                        ON A.FROMHOLIDAY=B.FROMHOLIDAY
                                        LEFT OUTER JOIN
                                        (
	                                        SELECT HD.DOCID 
	                                        FROM SPC_JN_CHANGHOLIDAYDT DT LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD
	                                        ON DT.DOCID = HD.DOCID
	                                        WHERE HD.DOCSTAT != 0 
	                                        AND HD.EMPLID = '{0}'
	                                        AND DT.TOHOLIDAY NOT IN 
	                                        ( 
		                                        SELECT DATEREFER FROM [SPC_JN_CANCLEDOCDT] 
		                                        WHERE DOCTYP = '002'
	                                        )
                                        ) C 
                                        ON A.DOCID = C.DOCID
                                        WHERE B.DOCID IS NULL
                                        ", _empid.ToString());


                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            if (dataTable.Rows.Count > 0)
                            {
                                dataTable.Clear();
                            }
                            dataTable.Load(sqlDataReader);
                        }
                    }
                }
                
                if (dataTable.Rows.Count > 0)
                {
                    rgv_CheckHist.DataSource = dataTable;
                }
                else
                {
                    rgv_CheckHist.DataSource = null;
                    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                if (con.State == ConnectionState.Open) con.Close();
            }
        }   
    }
}
