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

namespace HROUTOFFICE
{
    public partial class FormHrApprovedIn : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);

        string sysuser = "";
        string sectionid = "";
        string approvein = "";

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

        protected string syshrapprovein;
        public string HrApproveIn
        {
            get { return syshrapprovein; }
            set { syshrapprovein = value; }
        }

        public FormHrApprovedIn()
        {
            InitializeComponent();
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;

            #region radGridViewl
            // GridData
            this.GridViewShowData.Dock = DockStyle.Fill;
            this.GridViewShowData.AutoGenerateColumns = true;
            this.GridViewShowData.EnableFiltering = false;
            this.GridViewShowData.AllowAddNewRow = false;

            this.GridViewShowData.MasterTemplate.AutoGenerateColumns = false;
            this.GridViewShowData.ShowGroupedColumns = true;
            this.GridViewShowData.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.GridViewShowData.EnableHotTracking = true;
            this.GridViewShowData.AutoSizeRows = true;
            #endregion
        }
        private void FormHrApprovedIn_Load(object sender, EventArgs e)
        {
            this.sysuser = SysOutoffice;
            this.sectionid = Section;
            this.approvein = HrApproveIn;

            this.btnserch.Click += new EventHandler(btnserch_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.radButtonCheckAll.Enabled = false;
            this.radButtonUnChechAll.Enabled = false;

            this.dtpStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dtpEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.txtDocDate.Text = DateTime.Now.ToString("yyMMdd");
            this.txtOut.Text = "OUT";

            this.GetDimention();
        }

        #region Function

        private void GetDimention()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                sqlCommand.CommandText = string.Format(
                        @"  SELECT RTRIM([PWSECTION]) AS PWSECTION,RTRIM ([PWDESC]) AS PWDESC
                            FROM [dbo].[PWSECTION]
                            ORDER BY [PWDESC] ");

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                DdlDimention.DataSource = dt;
                DdlDimention.ValueMember = "PWSECTION";
                DdlDimention.DisplayMember = "PWDESC";
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
        public void Getdata()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            #region RadioDimention
            if (RadioDimention.ToggleState == ToggleState.On)
            {
                txtEmplId.Text = "";
                txtDocNumber.Text = "";
                try
                {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                                @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                        ,[ShiftId],[StartTime],[EndTime]
                                        ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                        ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
                                        ,CASE [TruckType] WHEN '1' THEN 'รถส่วนตัว' WHEN '2' THEN 'รถบริษัท' ELSE 'ไม่มีข้อมูล' END AS [TruckType]
                                        ,[TruckId],[Reason] ,HrApprovedOutDateTime
                                  FROM [IVZ_HROUTOFFICE] 
                                  WHERE  HeadApproved = 2 
                                  AND HrApprovedOut = 2 
                                  AND HrApprovedIn = 1 
                                  AND CombackType = 1
                                  AND [Status] = 1
                                  AND [DimentionId] = '" + DdlDimention.SelectedValue.ToString() + "' AND [TrandDateTime] BETWEEN '" + dtpStart.Text.ToString() + "' AND  '" + dtpEnd.Text.ToString() + "' ");
                    //convert(varchar,getdate(),23)
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        GridViewShowData.DataSource = dt;
                        this.radButtonCheckAll.Enabled = true;
                        this.radButtonUnChechAll.Enabled = true;
                    }
                    else
                    {

                        //  MessageBox.Show("ไม่มีข้อมูล");
                        GridViewShowData.DataSource = dt;

                    }
                }
                catch (Exception ex)
                {
                    this.radButtonCheckAll.Enabled = false;
                    this.radButtonUnChechAll.Enabled = false;

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
            }
            #endregion

            #region RadioEmpl
            else if (RadioEmpl.ToggleState == ToggleState.On)
            {
                txtDocNumber.Text = "";
                try
                {
                    if (txtEmplId.Text != "")
                    {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                                    @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                        ,[ShiftId],[StartTime],[EndTime]
                                        ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                        ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
                                        ,CASE [TruckType] WHEN '1' THEN 'รถส่วนตัว' WHEN '2' THEN 'รถบริษัท' ELSE 'ไม่มีข้อมูล' END AS [TruckType]
                                        ,[TruckId],[Reason] ,HrApprovedOutDateTime
                                  FROM [IVZ_HROUTOFFICE] 
                                  WHERE  HeadApproved = 2 
                                  AND HrApprovedOut = 2 
                                  AND HrApprovedIn = 1 
                                  AND CombackType = 1
                                  AND [Status] = 1
                                  AND ([EmplId] = '" + txtEmplId.Text.ToString() + "' OR [EmplCard] =  '" + txtEmplId.Text.ToString() + "') ");
                        //convert(varchar,getdate(),23)
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridViewShowData.DataSource = dt;
                            this.radButtonCheckAll.Enabled = true;
                            this.radButtonUnChechAll.Enabled = true;
                        }
                        else
                        {
                            // MessageBox.Show("ไม่มีข้อมูล กรูณาตรวจสอบเลขรหัสพนักงานหรือวันที่ให้ถูกต้อง");
                            this.txtEmplId.Text = "";
                            this.txtEmplId.Focus();
                            GridViewShowData.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.radButtonCheckAll.Enabled = false;
                    this.radButtonUnChechAll.Enabled = false;

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
            }
            #endregion

            #region RadioDocId
            else if (RadioDocId.ToggleState == ToggleState.On)
            {
                txtEmplId.Text = "";
                try
                {
                    if (txtDocDate.Text != "" && txtDocNumber.Text != "")
                    {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                                    @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                        ,[ShiftId],[StartTime],[EndTime]
                                        ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                        ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
                                        ,CASE [TruckType] WHEN '1' THEN 'รถส่วนตัว' WHEN '2' THEN 'รถบริษัท' ELSE 'ไม่มีข้อมูล' END AS [TruckType]
                                        ,[TruckId],[Reason],HrApprovedOutDateTime 
                                  FROM [IVZ_HROUTOFFICE] 
                                  WHERE  HeadApproved = 2 
                                  AND HrApprovedOut = 2 
                                  AND HrApprovedIn = 1 
                                  AND CombackType = 1
                                  AND [Status] = 1
                                  AND [DocId] = '" + txtOut.Text.ToString().Trim() + "' + '" + txtDocDate.Text.Trim() + "' + '-' + '" + txtDocNumber.Text.Trim() + "' ");
                        //convert(varchar,getdate(),23)
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            GridViewShowData.DataSource = dt;
                            this.radButtonCheckAll.Enabled = true;
                            this.radButtonUnChechAll.Enabled = true;
                        }

                        else
                        {
                            // MessageBox.Show("ไม่มีข้อมูล กรูณาตรวจสอบเลขที่เอกสารให้ถูกต้อง");
                            this.txtDocNumber.Text = "";
                            this.txtDocNumber.Focus();
                            GridViewShowData.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.radButtonCheckAll.Enabled = false;
                    this.radButtonUnChechAll.Enabled = false;

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
            }
            #endregion

            else
            {
                MessageBox.Show("เลือกการกรองข้อมูล");
            }
        }
        public void UpdateData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            SqlTransaction tr = con.BeginTransaction();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            sqlCommand.Transaction = tr;
            {
                // chackbox = true
                int Checktrue = 0;
                for (int i = 0; i <= GridViewShowData.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(GridViewShowData.Rows[i].Cells["CHECK"].Value) == true)
                    {
                        Checktrue++;
                    }
                }
                if (Checktrue == 0)
                {
                    MessageBox.Show("กรุณาเลือกรายชื่อที่ต้องการอนุมัติ");
                    return;
                }
                if (MessageBox.Show("คุณต้องการบันทึกข้อมูลเอกสารใบออกนอกออนไลน์ ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        foreach (GridViewDataRowInfo row in GridViewShowData.Rows)
                        {
                            if (row.Index > -1)
                            {

                                if (row.Cells["CHECK"].Value != null)
                                {
                                    sqlCommand.CommandText = string.Format(
                                    @"UPDATE [IVZ_HROUTOFFICE] SET [HrApprovedIn] = 2 ,[HrApprovedInBy] = @HrApprovedInBy{0}
                                                                    ,[HrApprovedInName] = @HrApprovedInName{0}
                                                                    ,[HrApprovedInDateTime] =  (dateadd(millisecond, -datepart(millisecond,getdate()),getdate()))
                                                                    ,[ToTallTimeUse] = (dateadd(millisecond, -datepart(millisecond,getdate()),getdate())) - [HrApprovedOutDateTime]
                                                               WHERE [OutOfficeId] = @OutOfficeId{0} 
                                                               AND [DocId] = @DocId{0}     ", row.Index
                                      );
                                    sqlCommand.Parameters.AddWithValue(string.Format(@"HrApprovedInBy{0}", row.Index), ClassCurUser.LogInEmplId);
                                    sqlCommand.Parameters.AddWithValue(string.Format(@"HrApprovedInName{0}", row.Index), ClassCurUser.LogInEmplName);
                                    sqlCommand.Parameters.AddWithValue(string.Format(@"OutOfficeId{0}", row.Index), row.Cells["OutOfficeId"].Value.ToString());
                                    sqlCommand.Parameters.AddWithValue(string.Format(@"DocId{0}", row.Index), row.Cells["DocId"].Value.ToString());

                                    sqlCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        try
                        {
                            tr.Commit();
                        }// con.Close();
                        catch { }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        try
                        {
                            tr.Rollback();
                        }
                        catch { }
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                        Getdata();
                    }
                }
            }

        }

        #endregion


        #region Button

        private void radButtonCheckAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewDataRowInfo row in GridViewShowData.Rows)
            {
                row.Cells["CHECK"].Value = true;
            }
        }

        private void radButtonUnChechAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewDataRowInfo row in GridViewShowData.Rows)
            {
                row.Cells["CHECK"].Value = false;
            }
        }

        private void btnserch_Click(object sender, EventArgs e)
        {
            this.Getdata();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.Getdata();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.UpdateData();
        }

        #endregion

        private void GridViewShowData_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.Column.Name == "OutOfficeId")
                {
                    GridViewRowInfo row = (GridViewRowInfo)GridViewShowData.Rows[e.RowIndex];

                    string DocId = row.Cells["DocId"].Value.ToString();
                    string OutId = row.Cells["OutOfficeId"].Value.ToString();

                    FormDetailShowData Frm = new FormDetailShowData(DocId, OutId);
                    Frm.Show();
                }
            }
        }
    }
}
