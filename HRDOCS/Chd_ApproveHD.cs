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
    public partial class Chd_ApproveHD : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string ApproveID = "";

        #region MyRegion SysEmpl

        string sysuser = "";


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

        #endregion

        public Chd_ApproveHD()
        {
            InitializeComponent();

            #region radGridView
            // GridData 
            this.rgv_ApproveHD.Dock = DockStyle.Fill;
            this.rgv_ApproveHD.ReadOnly = true;
            this.rgv_ApproveHD.AutoGenerateColumns = true;
            this.rgv_ApproveHD.EnableFiltering = false;
            this.rgv_ApproveHD.AllowAddNewRow = false;
            this.rgv_ApproveHD.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_ApproveHD.ShowGroupedColumns = true;
            this.rgv_ApproveHD.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_ApproveHD.EnableHotTracking = true;
            this.rgv_ApproveHD.AutoSizeRows = true;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_ApproveHD.Columns.Add(DOCID);


            GridViewTextBoxColumn DOCSTAT = new GridViewTextBoxColumn();
            DOCSTAT.Name = "DOCSTAT";
            DOCSTAT.FieldName = "DOCSTAT";
            DOCSTAT.HeaderText = "สถานะเอกสาร";
            DOCSTAT.IsVisible = true;
            DOCSTAT.ReadOnly = true;
            DOCSTAT.BestFit();
            rgv_ApproveHD.Columns.Add(DOCSTAT);

            GridViewTextBoxColumn TRANSDATE = new GridViewTextBoxColumn();
            TRANSDATE.Name = "TRANSDATE";
            TRANSDATE.FieldName = "TRANSDATE";
            TRANSDATE.HeaderText = "วันที่สร้าง";
            TRANSDATE.IsVisible = true;
            TRANSDATE.ReadOnly = true;
            TRANSDATE.BestFit();
            rgv_ApproveHD.Columns.Add(TRANSDATE);

            GridViewTextBoxColumn CREATEDNAME = new GridViewTextBoxColumn();
            CREATEDNAME.Name = "CREATEDNAME";
            CREATEDNAME.FieldName = "CREATEDNAME";
            CREATEDNAME.HeaderText = "ผู้สร้าง";
            CREATEDNAME.IsVisible = true;
            CREATEDNAME.ReadOnly = true;
            CREATEDNAME.BestFit();
            rgv_ApproveHD.Columns.Add(CREATEDNAME);

            GridViewTextBoxColumn MODIFIEDDATE = new GridViewTextBoxColumn();
            MODIFIEDDATE.Name = "MODIFIEDDATE";
            MODIFIEDDATE.FieldName = "MODIFIEDDATE";
            MODIFIEDDATE.HeaderText = "แก้ไขล่าสุด";
            MODIFIEDDATE.IsVisible = true;
            MODIFIEDDATE.ReadOnly = true;
            MODIFIEDDATE.BestFit();
            rgv_ApproveHD.Columns.Add(MODIFIEDDATE);

            GridViewTextBoxColumn EMPLNAME = new GridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.FieldName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.IsVisible = true;
            EMPLNAME.ReadOnly = true;
            EMPLNAME.BestFit();
            rgv_ApproveHD.Columns.Add(EMPLNAME);


            GridViewTextBoxColumn HEADAPPROVED = new GridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.FieldName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.IsVisible = true;
            HEADAPPROVED.ReadOnly = true;
            HEADAPPROVED.BestFit();
            rgv_ApproveHD.Columns.Add(HEADAPPROVED);

            GridViewTextBoxColumn HEADAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.FieldName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.IsVisible = true;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            HEADAPPROVEDBYNAME.BestFit();
            rgv_ApproveHD.Columns.Add(HEADAPPROVEDBYNAME);


            GridViewTextBoxColumn HRAPPROVED = new GridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.FieldName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.IsVisible = true;
            HRAPPROVED.ReadOnly = true;
            HRAPPROVED.BestFit();
            rgv_ApproveHD.Columns.Add(HRAPPROVED);


            GridViewTextBoxColumn HRAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.FieldName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.IsVisible = true;
            HRAPPROVEDBYNAME.ReadOnly = true;
            HRAPPROVEDBYNAME.BestFit();
            rgv_ApproveHD.Columns.Add(HRAPPROVEDBYNAME);

            GridViewCommandColumn ApproveButton = new GridViewCommandColumn();
            ApproveButton.Name = "ApproveButton";
            ApproveButton.FieldName = "ApproveButton";
            ApproveButton.HeaderText = "อนุมัติ";
            rgv_ApproveHD.Columns.Add(ApproveButton);


            #endregion

            this.btn_Search.Click += new EventHandler(btn_Search_Click);
        }
        private void Chd_ApproveHD_Load(object sender, EventArgs e)
        {
            this.sysuser = SysOutoffice;

            //this.dt_FromSec.Text = DateTime.Now.AddDays(-3).Date.ToString();
            this.dt_FromSec.Text = DateTime.Now.AddDays(-3).Date.ToString("yyyy-MM-dd");
            this.dt_FromSec.MinDate = DateTime.Now.Date.AddDays(-3);
            this.dt_ToSec.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.txt_DocId.Text = "CHD";
            this.txt_DocDate.Text = DateTime.Now.ToString("yyMMdd");

            this.CheckApproveID();
            this.Load_Section();
            this.ShowdataAll();
        }

        void btn_Search_Click(object sender, EventArgs e)
        {
            Getdata();
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
                    sqlCommand.CommandText = string.Format(@"SELECT DISTINCT * FROM(
	                                                                                    SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC 
	                                                                                    FROM PWEMPLOYEE  
		                                                                                        LEFT OUTER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS                                
	                                                                                    
                                                                                    ) AS PWSECTION
                                                            WHERE PWSECTION IS NOT NULL AND PWSECTION IN (SELECT PWSECTION 
												                                                            FROM SPC_CM_AUTHORIZE
												                                                            WHERE EMPLID = '{0}' AND APPROVEID = '001'
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

            if (ApproveID == "001" || ApproveID == "002")
            {

                #region rdb_Section

                try
                {
                    if (rdb_Section.ToggleState == ToggleState.On)
                    {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                            @" SELECT DOCID ,CONVERT(VARCHAR,TRANSDATE,23) AS TRANSDATE ,CREATEDNAME
                            ,CASE DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            ,EMPLID,EMPLNAME
                            ,CASE HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,CASE HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HEADAPPROVEDBYNAME
                            ,CASE HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,CONVERT(VARCHAR,MODIFIEDDATE,23) AS MODIFIEDDATE
                            ,CASE HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                            
                           FROM [SPC_JN_CHANGHOLIDAYHD]                                                      
                           WHERE SECTIONID = '{0}'
                           AND TRANSDATE BETWEEN '{1}' AND '{2}' 
                            
                           AND DOCSTAT != 0 
                           AND HEADAPPROVED = 0 
                           AND SECTIONID IN (
                                               SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                               WHERE EMPLID = '{3}' 
                                               AND APPROVEID = '001'
                                             ) 
                           ORDER BY DOCID DESC "

                            , ddl_Section.SelectedValue.ToString()
                            , dt_FromSec.Value.Date.ToString("yyyy-MM-dd")
                            , dt_ToSec.Value.Date.ToString("yyyy-MM-dd")
                            , ClassCurUser.LogInEmplId);

                        //AND TRANSDATE BETWEEN dateadd(day,-3,getdate()) and getdate()

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            rgv_ApproveHD.DataSource = dt;
                        }
                        else
                        {
                            rgv_ApproveHD.DataSource = dt;
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
                            @" SELECT DOCID ,CONVERT(VARCHAR,TRANSDATE,23) AS TRANSDATE ,CREATEDNAME
                            ,CASE DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            ,EMPLID,EMPLNAME
                            ,CASE HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,CASE HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HEADAPPROVEDBYNAME
                            ,CASE HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,CONVERT(VARCHAR,MODIFIEDDATE,23) AS MODIFIEDDATE
                            ,CASE HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                           FROM [SPC_JN_CHANGHOLIDAYHD]                                                    
                           WHERE EMPLID = '{0}' OR EMPLCARD = '{0}' 
                           
                           AND DOCSTAT != 0 
                           AND HEADAPPROVED = 0 
                           AND SECTIONID IN (
                                               SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                               WHERE EMPLID = '{1}' 
                                               AND APPROVEID = '001'
                                             ) 
                           ORDER BY DOCID DESC "

                            , txt_EmpId.Text.ToString()
                            , ClassCurUser.LogInEmplId);


                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            rgv_ApproveHD.DataSource = dt;
                        }
                        else
                        {
                            rgv_ApproveHD.DataSource = dt;
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
                            @" SELECT DOCID ,CONVERT(VARCHAR,TRANSDATE,23) AS TRANSDATE ,CREATEDNAME
                            ,CASE DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            ,EMPLID,EMPLNAME
                            ,CASE HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,CASE HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HEADAPPROVEDBYNAME
                            ,CASE HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,CONVERT(VARCHAR,MODIFIEDDATE,23) AS MODIFIEDDATE
                            ,CASE HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                           FROM [SPC_JN_CHANGHOLIDAYHD]                                               
                           WHERE DOCID = '{0}'  +  '{1}' + '-' + '{2}'
                           
                           AND DOCSTAT != 0 
                           AND HEADAPPROVED = 0 
                           AND SECTIONID IN (
                                               SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                               WHERE EMPLID = '{3}' 
                                               AND APPROVEID = '001'
                                             )"

                            , txt_DocId.Text.Trim()
                            , txt_DocDate.Text.Trim()
                            , txt_DocNum.Text.Trim()
                            , ClassCurUser.LogInEmplId);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            rgv_ApproveHD.DataSource = dt;
                        }
                        else
                        {
                            rgv_ApproveHD.DataSource = dt;
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
                            @" SELECT DOCID ,CONVERT(VARCHAR,TRANSDATE,23) AS TRANSDATE ,CREATEDNAME
                                            ,CASE DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                                            ,EMPLID,EMPLNAME
                                            ,CASE HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                            ,CASE HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HEADAPPROVEDBYNAME
                                            ,CASE HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                                            ,CONVERT(VARCHAR,MODIFIEDDATE,23) AS MODIFIEDDATE
                                            ,CASE HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                                           FROM [SPC_JN_CHANGHOLIDAYHD]                                                      
                                           WHERE DOCSTAT != 0 
                                           
                                           AND HEADAPPROVED = 0 
                                            AND SECTIONID IN (
                                                               SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                               WHERE EMPLID = '{0}' 
                                                               AND APPROVEID = '001'
                                                             ) 
                                            ORDER BY DOCID DESC ", ClassCurUser.LogInEmplId);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            rgv_ApproveHD.DataSource = dt;
                        }
                        else
                        {
                            rgv_ApproveHD.DataSource = dt;
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

        }
        void ShowdataAll()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            if (ApproveID == "001" || ApproveID == "002")
            {
            #region SelectAll

                try
                {
                    if (rdb_SelectAll.ToggleState == ToggleState.On)
                    {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                            @" SELECT DOCID ,CONVERT(VARCHAR,TRANSDATE,23) AS TRANSDATE ,CREATEDNAME
                                            ,CASE DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                                            ,EMPLID,EMPLNAME
                                            ,CASE HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                            ,CASE HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HEADAPPROVEDBYNAME
                                            ,CASE HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  WHEN '2' THEN 'ไม่อนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                                            ,CONVERT(VARCHAR,MODIFIEDDATE,23) AS MODIFIEDDATE
                                            ,CASE HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                                           FROM [SPC_JN_CHANGHOLIDAYHD]                                                      
                                           WHERE DOCSTAT != 0 
                                           
                                           AND HEADAPPROVED = 0 
                                            AND SECTIONID IN (
                                                               SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                               WHERE EMPLID = '{0}' 
                                                               AND APPROVEID = '001'
                                                             ) 
                                            ORDER BY DOCID DESC ", ClassCurUser.LogInEmplId);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            rgv_ApproveHD.DataSource = dt;
                        }
                        else
                        {
                            rgv_ApproveHD.DataSource = dt;
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
        }
        private void rgv_ApproveHD_CellFormatting(object sender, CellFormattingEventArgs e)
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

        private void rgv_ApproveHD_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.Column.Name == "ApproveButton")
                {
                    GridViewRowInfo row = (GridViewRowInfo)rgv_ApproveHD.Rows[e.RowIndex];

                    string DocId = row.Cells["DOCID"].Value.ToString();

                    using (Chd_ApproveHD_Detail frm = new Chd_ApproveHD_Detail(DocId))
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
        void CheckApproveID()
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
                    sqlCommand.CommandText = string.Format(@" select * from SPC_CM_AUTHORIZE
                                                                where EMPLID = '{0}' AND APPROVEID = '001'
                                                                order by APPROVEID ", ClassCurUser.LogInEmplId);
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
            }

        }

    }
}
