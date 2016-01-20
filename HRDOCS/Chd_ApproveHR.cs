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
    public partial class Chd_ApproveHR : Form
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

        public Chd_ApproveHR()
        {
            InitializeComponent();
            InitializeGridVeiw();
            InitializeButtom();
        }

        private void Chd_ApproveHR_Load(object sender, EventArgs e)
        {
            this.dt_FromSec.Text = DateTime.Now.AddDays(-7).Date.ToString("yyyy-MM-dd");
            this.dt_ToSec.Text = DateTime.Now.ToString("yyyy-MM-dd");
            
            this.txt_DocId.Text = "CHD";
            this.txt_DocDate.Text = DateTime.Now.ToString("yyMMdd");

            this.Load_Section();
        }
        void InitializeGridVeiw()
        {
            // GridData 
            this.rgv_ApproveHR.Dock = DockStyle.Fill;
            this.rgv_ApproveHR.ReadOnly = true;
            this.rgv_ApproveHR.AutoGenerateColumns = true;
            this.rgv_ApproveHR.EnableFiltering = false;
            this.rgv_ApproveHR.AllowAddNewRow = false;
            this.rgv_ApproveHR.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_ApproveHR.ShowGroupedColumns = true;
            this.rgv_ApproveHR.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_ApproveHR.EnableHotTracking = true;
            this.rgv_ApproveHR.AutoSizeRows = true;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_ApproveHR.Columns.Add(DOCID);

            GridViewTextBoxColumn DOCSTAT = new GridViewTextBoxColumn();
            DOCSTAT.Name = "DOCSTAT";
            DOCSTAT.FieldName = "DOCSTAT";
            DOCSTAT.HeaderText = "สถานะเอกสาร";
            DOCSTAT.IsVisible = true;
            DOCSTAT.ReadOnly = true;
            DOCSTAT.BestFit();
            rgv_ApproveHR.Columns.Add(DOCSTAT);

            GridViewTextBoxColumn CREATEDNAME = new GridViewTextBoxColumn();
            CREATEDNAME.Name = "CREATEDNAME";
            CREATEDNAME.FieldName = "CREATEDNAME";
            CREATEDNAME.HeaderText = "ผู้สร้าง";
            CREATEDNAME.IsVisible = true;
            CREATEDNAME.ReadOnly = true;
            CREATEDNAME.BestFit();
            rgv_ApproveHR.Columns.Add(CREATEDNAME);

            GridViewTextBoxColumn MODIFIEDDATE = new GridViewTextBoxColumn();
            MODIFIEDDATE.Name = "MODIFIEDDATE";
            MODIFIEDDATE.FieldName = "MODIFIEDDATE";
            MODIFIEDDATE.HeaderText = "แก้ไขล่าสุด";
            MODIFIEDDATE.IsVisible = true;
            MODIFIEDDATE.ReadOnly = true;
            MODIFIEDDATE.BestFit();
            rgv_ApproveHR.Columns.Add(MODIFIEDDATE);

            GridViewTextBoxColumn EMPLNAME = new GridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.FieldName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.IsVisible = true;
            EMPLNAME.ReadOnly = true;
            EMPLNAME.BestFit();
            rgv_ApproveHR.Columns.Add(EMPLNAME);


            GridViewTextBoxColumn HEADAPPROVED = new GridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.FieldName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.IsVisible = true;
            HEADAPPROVED.ReadOnly = true;
            HEADAPPROVED.BestFit();
            rgv_ApproveHR.Columns.Add(HEADAPPROVED);

            GridViewTextBoxColumn HEADAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.FieldName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.IsVisible = true;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            HEADAPPROVEDBYNAME.BestFit();
            rgv_ApproveHR.Columns.Add(HEADAPPROVEDBYNAME);


            GridViewTextBoxColumn HRAPPROVED = new GridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.FieldName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.IsVisible = true;
            HRAPPROVED.ReadOnly = true;
            HRAPPROVED.BestFit();
            rgv_ApproveHR.Columns.Add(HRAPPROVED);


            GridViewTextBoxColumn HRAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.FieldName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.IsVisible = true;
            HRAPPROVEDBYNAME.ReadOnly = true;
            HRAPPROVEDBYNAME.BestFit();
            rgv_ApproveHR.Columns.Add(HRAPPROVEDBYNAME);


            GridViewCommandColumn ApproveButton = new GridViewCommandColumn();
            ApproveButton.Name = "ApproveButton";
            ApproveButton.FieldName = "ApproveButton";
            ApproveButton.HeaderText = "อนุมัติ";
            rgv_ApproveHR.Columns.Add(ApproveButton);
        }
        void InitializeButtom()
        {
            this.btn_Search.Click += new EventHandler(btn_Search_Click);
        }

        #region MyRegion Buttom
        void btn_Search_Click(object sender, EventArgs e)
        {
            Getdata();
        }
        #endregion

        #region MyRegion Function
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
                    sqlCommand.CommandText = string.Format(@"SELECT DISTINCT * FROM(
	                                                                                    SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC 
	                                                                                    FROM PWEMPLOYEE  
		                                                                                        LEFT OUTER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS                                
	                                                                                    
                                                                                    ) AS PWSECTION
                                                            WHERE PWSECTION IS NOT NULL AND PWSECTION IN 
                                                                                                         ( SELECT A.PWSECTION 
											                                                               FROM SPC_CM_AUTHORIZE A 
												                                                                LEFT OUTER JOIN PWEMPLOYEE B ON A.EMPLID = B.PWEMPLOYEE
                                                                                                           WHERE  A.APPROVEID = '004' 
											                                                               AND B.PWEMPLOYEE  = '{0}' OR B.PWCARD = '{0}'
                                                                                                          )
                                                            ORDER BY PWDESC "
                        , ClassCurUser.LogInEmplId
                                                            );

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
        void Getdata()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            #region rdb_Section

            try
            {
                //section
                if (rdb_Section.ToggleState == ToggleState.On)
                {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                        @" SELECT DOCID 
                            ,CASE DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            ,CREATEDNAME,CONVERT(VARCHAR,MODIFIEDDATE,23) AS MODIFIEDDATE,EMPLID,EMPLNAME
                            ,CASE HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,CASE HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
                            ,CASE HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ' ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,CASE HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                           FROM [SPC_JN_CHANGHOLIDAYHD]                                                      
                           WHERE SECTIONID = '{0}'
                           AND TRANSDATE BETWEEN '{1}' AND '{2}' 
                           AND DOCSTAT != 0 
                           AND HEADAPPROVED = 1
                           AND HRAPPROVED = 0
                           AND SECTIONID IN (
                                               SELECT A.PWSECTION 
											   FROM SPC_CM_AUTHORIZE A 
												    LEFT OUTER JOIN PWEMPLOYEE B ON A.EMPLID = B.PWEMPLOYEE
                                               WHERE  A.APPROVEID = '004' 
											   AND B.PWEMPLOYEE  = '{3}' OR B.PWCARD = '{3}'
                                             ) ORDER BY DOCID DESC "

                        , ddl_Section.SelectedValue.ToString()
                        , dt_FromSec.Value.Date.ToString("yyyy-MM-dd")
                        , dt_ToSec.Value.Date.ToString("yyyy-MM-dd")
                        , ClassCurUser.LogInEmplId
                        );

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_ApproveHR.DataSource = dt;
                    }
                    else
                    {
                        rgv_ApproveHR.DataSource = dt;
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
                //EmplID
                if (rdb_EmpId.ToggleState == ToggleState.On)
                {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                        @" SELECT DOCID 
                            ,CASE DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            ,CREATEDNAME,CONVERT(VARCHAR,MODIFIEDDATE,23) AS MODIFIEDDATE,EMPLID,EMPLNAME
                            ,CASE HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,CASE HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
                            ,CASE HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ' ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,CASE HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                           FROM [SPC_JN_CHANGHOLIDAYHD]                                                     
                           WHERE EMPLID = '{0}' OR EMPLCARD = '{0}'
                           AND DOCSTAT != 0 
                           AND HEADAPPROVED = 1
                           AND HRAPPROVED = 0
                           AND SECTIONID IN (
                                               SELECT A.PWSECTION 
											   FROM SPC_CM_AUTHORIZE A 
												    LEFT OUTER JOIN PWEMPLOYEE B ON A.EMPLID = B.PWEMPLOYEE
                                               WHERE  A.APPROVEID = '004' 
											   AND B.PWEMPLOYEE  = '{1}' OR B.PWCARD = '{1}'
                                             ) ORDER BY DOCID DESC "

                        , txt_EmpId.Text.ToString()
                        , ClassCurUser.LogInEmplId  
                        );

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_ApproveHR.DataSource = dt;
                    }
                    else
                    {
                        rgv_ApproveHR.DataSource = dt;
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
                        @" SELECT DOCID 
                            ,CASE DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            ,CREATEDNAME,CONVERT(VARCHAR,MODIFIEDDATE,23) AS MODIFIEDDATE,EMPLID,EMPLNAME
                            ,CASE HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,CASE HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
                            ,CASE HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ' ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,CASE HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                           FROM [SPC_JN_CHANGHOLIDAYHD]                                                      
                           WHERE DOCID = '{0}'  +  '{1}' + '-' + '{2}'
                           AND DOCSTAT != 0 
                           AND HEADAPPROVED = 1
                           AND HRAPPROVED = 0
                           AND SECTIONID IN (
                                               SELECT A.PWSECTION 
											   FROM SPC_CM_AUTHORIZE A 
												    LEFT OUTER JOIN PWEMPLOYEE B ON A.EMPLID = B.PWEMPLOYEE
                                               WHERE  A.APPROVEID = '004' 
											   AND B.PWEMPLOYEE  = '{3}' OR B.PWCARD = '{3}'
                                             ) "

                        , txt_DocId.Text.Trim()
                        , txt_DocDate.Text.Trim()
                        , txt_DocNum.Text.Trim()

                        , ClassCurUser.LogInEmplId  
                        );

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_ApproveHR.DataSource = dt;
                    }
                    else
                    {
                        rgv_ApproveHR.DataSource = dt;
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

            #region rdb_SelectAll

            try
            {
                if (rdb_SelectAll.ToggleState == ToggleState.On)
                {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                        @" SELECT DOCID 
                            ,CASE DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            ,CREATEDNAME,CONVERT(VARCHAR,MODIFIEDDATE,23) AS MODIFIEDDATE,EMPLID,EMPLNAME
                            ,CASE HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,CASE HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
                            ,CASE HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ' ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,CASE HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                           FROM [SPC_JN_CHANGHOLIDAYHD]                                                      
                           WHERE DOCSTAT != 0 
                           AND HEADAPPROVED = 1
                           AND HRAPPROVED = 0
                           AND SECTIONID IN (
                                               SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                               WHERE EMPLID = '{0}' 
                                               AND APPROVEID = '004'
                                             ) ORDER BY DOCID DESC "

                        , ClassCurUser.LogInEmplId
                        );

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_ApproveHR.DataSource = dt;
                    }
                    else
                    {
                        rgv_ApproveHR.DataSource = dt;
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
        #endregion

        private void rgv_ApproveHR_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (!(e.CellElement.RowElement is GridTableHeaderRowElement))
            {
                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    GridViewCommandColumn column = (GridViewCommandColumn)e.CellElement.ColumnInfo;
                    RadButtonElement button = (RadButtonElement)e.CellElement.Children[0];

                    if (column.Name == "ApproveButton")
                    {
                        RadButtonElement element = (RadButtonElement)e.CellElement.Children[0];
                        element.Text = "อนุมัติ";
                        element.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                }
            }
        }
        private void rgv_ApproveHR_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.Column.Name == "ApproveButton")
                {
                    GridViewRowInfo row = (GridViewRowInfo)rgv_ApproveHR.Rows[e.RowIndex];

                    string DocId = row.Cells["DOCID"].Value.ToString();

                    using (Chd_ApproveHR_Detail frm = new Chd_ApproveHR_Detail(DocId))
                    {
                        //frm.Show();

                        if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                        {
                            //โหลดข้อมูลใหม่
                            Getdata();
                        }
                    }
                }
            }
        }
    }
}
