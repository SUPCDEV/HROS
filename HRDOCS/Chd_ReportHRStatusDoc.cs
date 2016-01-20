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
    public partial class Chd_ReportHRStatusDoc : Form
    {
        protected bool IsSaveOnce = true;

        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        #region MyRegion SysEmpl

        string sysuser = "";
        string sectionid = "";
        string approveout = "";

        protected string emplId;
        public string EmplId
        {
            get { return emplId; }
            set { emplId = value; }
        }

        protected string key;
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        protected string emplname;
        public string EmplName
        {
            get { return emplname; }
            set { emplname = value; }
        }

        protected string sysoutoffice;
        public string SysOutoffice
        {
            get { return sysoutoffice; }
            set { sysoutoffice = value; }
        }

        protected string section;
        public string Section
        {
            get { return section; }
            set { section = value; }
        }

        protected string syshrapproveout;
        public string HrApproveOut
        {
            get { return syshrapproveout; }
            set { syshrapproveout = value; }
        }

        #endregion

        public Chd_ReportHRStatusDoc()
        {
            InitializeComponent();

            #region GridView
            this.rgv_StatusDoc.Dock = DockStyle.Fill;
            this.rgv_StatusDoc.ReadOnly = true;
            this.rgv_StatusDoc.AutoGenerateColumns = true;
            this.rgv_StatusDoc.EnableFiltering = false;
            this.rgv_StatusDoc.AllowAddNewRow = false;
            this.rgv_StatusDoc.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_StatusDoc.ShowGroupedColumns = true;
            this.rgv_StatusDoc.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_StatusDoc.EnableHotTracking = true;
            this.rgv_StatusDoc.AutoSizeRows = true;
            #endregion

            this.btn_Search.Click += new EventHandler(btn_Search_Click);
        }
        
        private void Chd_ReportHRStatusDoc_Load(object sender, EventArgs e)
        {
            this.dt_FromSec.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dt_ToSec.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.txt_DocId.Text = "CHD";
            this.txt_DocDate.Text = DateTime.Now.ToString("yyMMdd");

            Load_Section();
        }
        void btn_Search_Click(object sender, EventArgs e)
        {
            this.Getdata();
        }

        #region Function

        void Getdata()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            #region rdb_Section

            try
            {
                if (rdb_Section.ToggleState == ToggleState.On)
                {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                        @" SELECT HD.DOCID ,CONVERT(VARCHAR, HD.TRANSDATE , 23) AS TRANSDATE
                            ,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.FROMHOLIDAY 
                            ,CASE DT.TOHOLIDAY WHEN '0000-00-00' THEN '* แจ้งภายหลัง'  
												  ELSE DT.TOHOLIDAY END AS TOHOLIDAY
                            ,CASE DT.TOSHIFTID WHEN '' THEN '-'  
												  ELSE DT.TOSHIFTID END AS TOSHIFTID
                            ,CASE DT.TOSHIFTDESC WHEN '' THEN '-'  
													ELSE DT.TOSHIFTDESC END AS TOSHIFTDESC ,DT.REASON
                            ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' 
												  WHEN '1' THEN 'อนุมัติ' 
												  WHEN '2' THEN 'ไม่อนุมัติ'  
												  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,HD.HEADAPPROVEDBYNAME AS HEADAPPROVEDBYNAME ,CONVERT(VARCHAR,HD.HEADAPPROVEDDATE ,23) AS HEADAPPROVEDDATE
                            ,HD.HEADAPPORVEREMARK
                            ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' 
												WHEN '1' THEN 'อนุมัติ'
												WHEN '2' THEN 'ไม่อนุมัติ'  
												ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,HD.HRAPPROVEDBYNAME AS HRAPPROVEDBYNAME ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE ,23) AS HRAPPROVEDDATE
                            ,CASE HD.HRAPPORVEREMARK  WHEN '' THEN '-' 
														ELSE HD.HRAPPORVEREMARK END AS HRAPPORVEREMARK
                            ,CASE HD.DOCSTAT WHEN '1' THEN 'ใช้งาน'  
												WHEN '0' THEN 'ยกเลิก' 
												   ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                           FROM SPC_JN_CHANGHOLIDAYDT DT LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD
		                            ON DT.DOCID = HD.DOCID                                                           
                           WHERE HD.SECTIONID = '{0}'
                           AND HD.TRANSDATE BETWEEN '{1}' AND '{2}' 
                           AND HD.DOCSTAT != 0 "

                        , ddl_Section.SelectedValue.ToString()
                        , dt_FromSec.Value.Date.ToString("yyyy-MM-dd")
                        , dt_ToSec.Value.Date.ToString("yyyy-MM-dd")
                        //, ClassCurUser.LogInEmplId
                        );

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_StatusDoc.DataSource = dt;
                    }
                    else
                    {
                        rgv_StatusDoc.DataSource = dt;
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

            #endregion

            #region rdb_EmpID
            try
            {
                if (rdb_EmpId.ToggleState == ToggleState.On)
                {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                        @" SELECT HD.DOCID ,CONVERT(VARCHAR, HD.TRANSDATE , 23) AS TRANSDATE
                            ,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.FROMHOLIDAY 
                            ,CASE DT.TOHOLIDAY WHEN '0000-00-00' THEN '* แจ้งภายหลัง'  
												  ELSE DT.TOHOLIDAY END AS TOHOLIDAY
                            ,CASE DT.TOSHIFTID WHEN '' THEN '-'  
												  ELSE DT.TOSHIFTID END AS TOSHIFTID
                            ,CASE DT.TOSHIFTDESC WHEN '' THEN '-'  
													ELSE DT.TOSHIFTDESC END AS TOSHIFTDESC ,DT.REASON
                            ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' 
												  WHEN '1' THEN 'อนุมัติ' 
												  WHEN '2' THEN 'ไม่อนุมัติ'  
												  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,HD.HEADAPPROVEDBYNAME AS HEADAPPROVEDBYNAME ,CONVERT(VARCHAR,HD.HEADAPPROVEDDATE ,23) AS HEADAPPROVEDDATE
                            ,HD.HEADAPPORVEREMARK
                            ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' 
												WHEN '1' THEN 'อนุมัติ'
												WHEN '2' THEN 'ไม่อนุมัติ'  
												ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,HD.HRAPPROVEDBYNAME AS HRAPPROVEDBYNAME ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE ,23) AS HRAPPROVEDDATE
                            ,CASE HD.HRAPPORVEREMARK  WHEN '' THEN '-' 
														ELSE HD.HRAPPORVEREMARK END AS HRAPPORVEREMARK
                            ,CASE HD.DOCSTAT WHEN '1' THEN 'ใช้งาน'  
												WHEN '0' THEN 'ยกเลิก' 
												   ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                           FROM SPC_JN_CHANGHOLIDAYDT DT LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD
		                            ON DT.DOCID = HD.DOCID                                                       
                           WHERE HD.EMPLID = '{0}' OR EMPLCARD = '{0}'
                           AND HD.DOCSTAT != 0 "

                        , txt_EmpId.Text.ToString()
                        //, ClassCurUser.LogInEmplId  
                        );

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_StatusDoc.DataSource = dt;
                    }
                    else
                    {
                        rgv_StatusDoc.DataSource = dt;
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
            #endregion

            #region rdb_DocId

            try
            {
                if (rdb_DocId.ToggleState == ToggleState.On)
                {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                        @" SELECT HD.DOCID ,CONVERT(VARCHAR, HD.TRANSDATE , 23) AS TRANSDATE
                            ,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.FROMHOLIDAY 
                            ,CASE DT.TOHOLIDAY WHEN '0000-00-00' THEN '* แจ้งภายหลัง'  
												  ELSE DT.TOHOLIDAY END AS TOHOLIDAY
                            ,CASE DT.TOSHIFTID WHEN '' THEN '-'  
												  ELSE DT.TOSHIFTID END AS TOSHIFTID
                            ,CASE DT.TOSHIFTDESC WHEN '' THEN '-'  
													ELSE DT.TOSHIFTDESC END AS TOSHIFTDESC ,DT.REASON
                            ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' 
												  WHEN '1' THEN 'อนุมัติ' 
												  WHEN '2' THEN 'ไม่อนุมัติ'  
												  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,HD.HEADAPPROVEDBYNAME AS HEADAPPROVEDBYNAME ,CONVERT(VARCHAR,HD.HEADAPPROVEDDATE ,23) AS HEADAPPROVEDDATE
                            ,HD.HEADAPPORVEREMARK
                            ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' 
												WHEN '1' THEN 'อนุมัติ'
												WHEN '2' THEN 'ไม่อนุมัติ'  
												ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,HD.HRAPPROVEDBYNAME AS HRAPPROVEDBYNAME ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE ,23) AS HRAPPROVEDDATE
                            ,CASE HD.HRAPPORVEREMARK  WHEN '' THEN '-' 
														ELSE HD.HRAPPORVEREMARK END AS HRAPPORVEREMARK
                            ,CASE HD.DOCSTAT WHEN '1' THEN 'ใช้งาน'  
												WHEN '0' THEN 'ยกเลิก' 
												   ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                           FROM SPC_JN_CHANGHOLIDAYDT DT LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD
		                            ON DT.DOCID = HD.DOCID                                                        
                           WHERE HD.DOCID = '{0}'  +  '{1}' + '-' + '{2}'
                           AND HD.DOCSTAT != 0 "

                        , txt_DocId.Text.Trim()
                        , txt_DocDate.Text.Trim()
                        , txt_DocNum.Text.Trim()

                        //, ClassCurUser.LogInEmplId  
                        );

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_StatusDoc.DataSource = dt;
                    }
                    else
                    {
                        rgv_StatusDoc.DataSource = dt;
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

            #endregion

        }

        void Load_Section()
        {

            ddl_Section.Items.Clear();

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(
                            @" SELECT RTRIM([PWSECTION]) AS PWSECTION,RTRIM ([PWDESC]) AS PWDESC
                                FROM [dbo].[PWSECTION]
                                ORDER BY [PWDESC] ");

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

                    if (dataTable.Rows.Count > 0)
                    {
                        ddl_Section.DataSource = dataTable;
                        ddl_Section.ValueMember = "PWSECTION";
                        ddl_Section.DisplayMember = "PWDESC";

                    }



                    else
                    {
                        MessageBox.Show("ไม่พบสิทธิ์ในการอนุมัติเอกสาร...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        #endregion
    }
}
