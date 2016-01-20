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
    public partial class FormMNApproveOut : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);

        //string sysuser = "";
        string divisionid = "";
        string sectionid = "";
        string mnapproveout = "";
        string ApproveID = "";

        #region
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
        //
        protected string sysmnapproveout;
        public string MNApproveOut
        {
            get { return sysmnapproveout; }
            set { sysmnapproveout = value; }
        }

        protected string section;
        public string Section
        {
            get { return section; }
            set { section = value; }
        }
        protected string division;
        public string Division
        {
            get { return division; }
            set { division = value; }
        }
        //
        #endregion

        public FormMNApproveOut()
        {
            InitializeComponent();

            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;

            #region radGridViewl
            // GridData
            //this.GridViewShowData.Dock = DockStyle.Fill;
            //this.GridViewShowData.ReadOnly = true;
            //this.GridViewShowData.AllowAddNewRow = false;
            //this.GridViewShowData.EnableHotTracking = true;
            //this.GridViewShowData.AutoGenerateColumns = true;
            //this.GridViewShowData.EnableFiltering = false;
            //this.GridViewShowData.ShowGroupedColumns = true;

            //this.GridViewShowData.MasterTemplate.AutoGenerateColumns = false;
            //this.GridViewShowData.MasterTemplate.AutoExpandGroups = true;
            //this.GridViewShowData.MasterTemplate.ShowHeaderCellButtons = true;
            //this.GridViewShowData.MasterTemplate.ShowFilteringRow = false;
            //this.GridViewShowData.MasterTemplate.ShowGroupedColumns = true;

            //this.GridViewShowData.MasterTemplate.EnableAlternatingRowColor = true;
            //this.GridViewShowData.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            //this.GridViewShowData.AutoSizeRows = true;
            //this.GridViewShowData.BestFitColumns(BestFitColumnMode.AllCells);


            this.rgv_ApproveMN.Dock = DockStyle.Fill;
            this.rgv_ApproveMN.AutoGenerateColumns = true;
            this.rgv_ApproveMN.EnableFiltering = false;
            this.rgv_ApproveMN.AllowAddNewRow = false;

            this.rgv_ApproveMN.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_ApproveMN.ShowGroupedColumns = true;
            this.rgv_ApproveMN.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_ApproveMN.EnableHotTracking = true;
            this.rgv_ApproveMN.AutoSizeRows = true;

            #endregion

            this.Load += new EventHandler(FormMNHDAprove_Load);
        }
        void FormMNHDAprove_Load(object sender, EventArgs e)
        {
            //this.sysuser = SysOutoffice;
            this.sectionid = Section;
            this.mnapproveout = MNApproveOut;
            this.divisionid = Division;


            this.radButtonserch.Click += new EventHandler(radButtonserch_Click);
            this.radButtonSave.Click += new EventHandler(radButtonSave_Click);
            this.radButtonCheckAll.Enabled = false;
            this.radButtonUnChechAll.Enabled = false;

            this.dt_From.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dt_To.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.txt_DocDate.Text = DateTime.Now.ToString("yyMMdd");
            this.txt_DocId.Text = "OUT";

            // this.GetDimention();
            this.CheckApproveID();
            this.Load_Section();
        }

        #region Function

        void Load_Section()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(
                        @"SELECT DISTINCT * FROM(
	                             SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC 
	                             FROM PWEMPLOYEE  
		                              LEFT OUTER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS                                
	                             WHERE PWEMPLOYEE.PWDIVISION IN ('75','11') OR PWEMPLOYEE.PWSECTION IN('23') 
                                 ) AS PWSECTION
                           WHERE PWSECTION IS NOT NULL AND PWSECTION IN (
                                SELECT PWSECTION 
								FROM SPC_CM_AUTHORIZE
								WHERE EMPLID = '{0}' AND APPROVEID IN ('005','006','001')
                                )
                                ORDER BY PWDESC ", ClassCurUser.LogInEmplId);

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
                        MessageBox.Show("ไม่พบสิทธิ์ในการอนุมัติเอกสารใบเปลี่ยนวันหยุด...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
        void SearchData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                #region rdb_Section
                if (rdb_Section.ToggleState == ToggleState.On)
                {
                    if (ApproveID == "005")
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                    ,[ShiftId],[StartTime],[EndTime]
                                     ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType,[Reason]
                                     ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType] 
                              FROM [IVZ_HROUTOFFICE] 
                               WHERE DimentionId = '{0}' AND TrandDateTime BETWEEN '{1}' AND '{2}' 
                              AND  HeadApproved = 1 AND HrApprovedOut = 1 AND HrApprovedIn = 1 AND [Status] = 1
                              AND EmplId <> '{3}'
                              AND DimentionId IN( 
					                              SELECT PWSECTION FROM SPC_CM_AUTHORIZE
					                              WHERE EMPLID = '{3}' OR EmplCard = '{3}' AND APPROVEID = '005'
				                                 )"

                              , ddl_Section.SelectedValue.ToString()
                              , dt_From.Value.Date.ToString("yyyy-MM-dd")
                              , dt_To.Value.Date.ToString("yyyy-MM-dd")
                              , ClassCurUser.LogInEmplId);
                    }
                    else if (ApproveID == "006")
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                    ,[ShiftId],[StartTime],[EndTime]
                                     ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType,[Reason]
                                     ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
                              FROM [IVZ_HROUTOFFICE] 
                              WHERE DimentionId = '{0}' AND TrandDateTime BETWEEN '{1}' AND '{2}' 
                              AND  HeadApproved = 1 AND HrApprovedOut = 1 AND HrApprovedIn = 1 AND [Status] = 1 
                              AND EmplId <> '{3}'
                              AND EmplId NOT IN (
													SELECT EMPLID FROM SPC_CM_AUTHORIZE
						                            WHERE APPROVEID IN ('005','006')
												   )
                              AND DimentionId IN (
                                                    SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                    WHERE EMPLID = '{3}' OR EmplCard = '{3}'
                                                    AND APPROVEID IN ('006')
                                                  ) "

                              , ddl_Section.SelectedValue.ToString()
                              , dt_From.Value.Date.ToString("yyyy-MM-dd")
                              , dt_To.Value.Date.ToString("yyyy-MM-dd")
                              , ClassCurUser.LogInEmplId);
                    }
                    else if (ApproveID == "001")
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                    ,[ShiftId],[StartTime],[EndTime]
                                     ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType,[Reason]
                                     ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
                              FROM [IVZ_HROUTOFFICE] 
                              WHERE DimentionId = '{0}' AND TrandDateTime BETWEEN '{1}' AND '{2}' 
                              AND  HeadApproved = 1 AND HrApprovedOut = 1 AND HrApprovedIn = 1 AND [Status] = 1 
                              AND EmplId <> '{3}'                              
                              AND DimentionId IN (
                                                    SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                    WHERE EMPLID = '{3}' OR EmplCard = '{3}' 
                                                    AND APPROVEID IN ('001')
                                                  ) "

                              , ddl_Section.SelectedValue.ToString()
                              , dt_From.Value.Date.ToString("yyyy-MM-dd")
                              , dt_To.Value.Date.ToString("yyyy-MM-dd")
                              , ClassCurUser.LogInEmplId);
                    }
                }
                #endregion

                #region rdb_EmpId
                if (rdb_EmpId.ToggleState == ToggleState.On)
                {
                    if (ApproveID == "005")
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                  ,[ShiftId],[StartTime],[EndTime]
                                    ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType,[Reason]
                                     ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType] 
                              FROM [IVZ_HROUTOFFICE] 
                              WHERE EmplId = '{0}' 
                              AND HeadApproved = 1 AND HrApprovedOut = 1 AND HrApprovedIn = 1 AND [Status] = 1
                              AND EmplId <> '{1}'
                              AND DimentionId IN (SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                  WHERE EMPLID = '{1}' OR EmplCard = '{1}' 
                                                  AND APPROVEID = '005') ", txt_EmpId.Text, ClassCurUser.LogInEmplId);

                    }
                    else if (ApproveID == "006")
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                ,[ShiftId],[StartTime],[EndTime]
                                    ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType,[Reason]
                                     ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType] 
                              FROM [IVZ_HROUTOFFICE] 
                              WHERE EmplId = '{0}' 
                              AND HeadApproved = 1 AND HrApprovedOut = 1 AND HrApprovedIn = 1 AND [Status] = 1
                              AND EmplId <> '{1}'
                              AND EmplId NOT IN (SELECT EMPLID FROM SPC_CM_AUTHORIZE
						                         WHERE APPROVEID IN ('005','006')
						                        )
	                          AND DimentionId IN (  SELECT PWSECTION FROM SPC_CM_AUTHORIZE
						                            WHERE EMPLID = '{1}' OR EmplCard = '{1}' 
                                                    AND APPROVEID = '006' ) ", txt_EmpId.Text, ClassCurUser.LogInEmplId);

                    }

                    else if (ApproveID == "001")
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                ,[ShiftId],[StartTime],[EndTime]
                                ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType,[Reason]
                                     ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType] 
                              FROM [IVZ_HROUTOFFICE] 
                              WHERE EmplId = '{0}' 
                              AND HeadApproved = 1 AND HrApprovedOut = 1 AND HrApprovedIn = 1 AND [Status] = 1
                              AND EmplId <> '{1}'                              
	                          AND DimentionId IN (  SELECT PWSECTION FROM SPC_CM_AUTHORIZE
						                            WHERE EMPLID = '{1}' OR EmplCard = '{1}' 
                                                    AND APPROVEID = '001' ) ", txt_EmpId.Text, ClassCurUser.LogInEmplId);

                    }
                }
                #endregion

                #region rdb_DocId
                if (rdb_DocId.ToggleState == ToggleState.On)
                {
                    if (ApproveID == "005")
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                    ,[ShiftId],[StartTime],[EndTime]
                                     ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType,[Reason]
                                     ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
                              FROM [IVZ_HROUTOFFICE] 
                              WHERE DocId = '{0}'  +  '{1}' + '-' + '{2}'  
                              AND HeadApproved = 1 AND HrApprovedOut = 1 AND HrApprovedIn = 1 AND [Status] = 1
                              AND EmplId <> '{3}'
                              AND DimentionId IN (SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                   WHERE EMPLID = '{3}' OR EmplCard = '{3}' AND APPROVEID = '005') "
                                , txt_DocId.Text.Trim()
                                , txt_DocDate.Text.Trim()
                                , txtDocNumber.Text.Trim(), ClassCurUser.LogInEmplId);
                    }
                    else if (ApproveID == "006")
                    {
                        sqlCommand.CommandText = string.Format(
                              @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                    ,[ShiftId],[StartTime],[EndTime]
                                     ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType,[Reason]
                                     ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
                              FROM [IVZ_HROUTOFFICE] 
                              WHERE DocId = '{0}'  +  '{1}' + '-' + '{2}'  
                              AND HeadApproved = 1 AND HrApprovedOut = 1 AND HrApprovedIn = 1 AND [Status] = 1
                              AND EmplId <> '{3}'
                                                            AND EmplId NOT IN (SELECT EMPLID FROM SPC_CM_AUTHORIZE
						                                                                WHERE APPROVEID IN ('005','006')
						                                                                )
	                                                                AND DimentionId IN (SELECT PWSECTION from SPC_CM_AUTHORIZE
						                                                                WHERE EMPLID = '{3}' OR EmplCard = '{3}' 
                                                                                        AND APPROVEID = '006') "
                                , txt_DocId.Text.Trim()
                                , txt_DocDate.Text.Trim()
                                , txtDocNumber.Text.Trim(), ClassCurUser.LogInEmplId);
                    }

                    else if (ApproveID == "001")
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                    ,[ShiftId],[StartTime],[EndTime]
                                     ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType,[Reason]
                                     ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
                              FROM [IVZ_HROUTOFFICE] 
                              WHERE DocId = '{0}'  +  '{1}' + '-' + '{2}'  
                              AND HeadApproved = 1 AND HrApprovedOut = 1 AND HrApprovedIn = 1 AND [Status] = 1
                              AND EmplId <> '{3}'
                              AND DimentionId IN (SELECT PWSECTION from SPC_CM_AUTHORIZE
						                          WHERE EMPLID = '{3}' OR EmplCard = '{3}'
                                                  AND APPROVEID = '001') "
                                , txt_DocId.Text.Trim()
                                , txt_DocDate.Text.Trim()
                                , txtDocNumber.Text.Trim(), ClassCurUser.LogInEmplId);
                    }

                #endregion

                }
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_ApproveMN.DataSource = dt;
                    this.radButtonCheckAll.Enabled = true;
                    this.radButtonUnChechAll.Enabled = true;
                }
                else
                {
                    rgv_ApproveMN.DataSource = dt;
                    this.txt_EmpId.Clear();
                    this.txtDocNumber.Clear();
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
                for (int i = 0; i <= rgv_ApproveMN.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(rgv_ApproveMN.Rows[i].Cells["CHECK"].Value) == true)
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
                        foreach (GridViewDataRowInfo row in rgv_ApproveMN.Rows)
                        {
                            if (row.Index > -1)
                            {

                                if (row.Cells["CHECK"].Value != null)
                                {
                                    sqlCommand.CommandText = string.Format(
                                    @"UPDATE [IVZ_HROUTOFFICE] SET [HeadApproved] = 2 
                                                                    ,[HeadApprovedBy] = @HeadApprovedBy{0}
                                                                    ,[HeadApprovedName] = @HeadApprovedName{0}
                                                                    ,[HeadApprovedDateTime] =  (dateadd(millisecond, -datepart(millisecond,getdate()),getdate()))
                                                                    ,[HrApprovedOut] = 2 
                                                                    ,[HrApprovedOutBY] = @HrApprovedOutBY{0}
                                                                    ,[HrApprovedOutName] = @HrApprovedOutName{0}
                                                                    ,[HrApprovedOutDateTime] =  (dateadd(millisecond, -datepart(millisecond,getdate()),getdate()))

                                                               WHERE [OutOfficeId] = @OutOfficeId{0} 
                                                               AND DocId = @DocId{0} ", row.Index
                                      );
                                    sqlCommand.Parameters.AddWithValue(string.Format(@"HeadApprovedBy{0}", row.Index), ClassCurUser.LogInEmplId);
                                    sqlCommand.Parameters.AddWithValue(string.Format(@"HeadApprovedName{0}", row.Index), ClassCurUser.LogInEmplName);

                                    sqlCommand.Parameters.AddWithValue(string.Format(@"HrApprovedOutBY{0}", row.Index), ClassCurUser.LogInEmplId);
                                    sqlCommand.Parameters.AddWithValue(string.Format(@"HrApprovedOutName{0}", row.Index), ClassCurUser.LogInEmplName);

                                    sqlCommand.Parameters.AddWithValue(string.Format(@"OutOfficeId{0}", row.Index), row.Cells["OutOfficeId"].Value.ToString());
                                    sqlCommand.Parameters.AddWithValue(string.Format(@"DocId{0}", row.Index), row.Cells["DocId"].Value.ToString());

                                    sqlCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        tr.Commit();
                        // con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        tr.Rollback();
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                        //Getdata();
                        SearchData();
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
                                                                where EMPLID = '{0}'
                                                                order by APPROVEID ", ClassCurUser.LogInEmplId);

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

        # region oldcode
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
                        @" SELECT DISTINCT * FROM
                            (
	                            SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC 
	                            FROM PWEMPLOYEE  
		                             LEFT OUTER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS                                
	                            WHERE PWEMPLOYEE.PWDIVISION IN ('75','11') 
                                
                            )
                            AS PWSECTION
                            ORDER BY PWDESC   ");

                //                sqlCommand.CommandText = string.Format(
                //                        @"
                //                        (
                //		                    SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC,PWDIVISION, PWSECTION.PWSECTION
                //		                    FROM PWEMPLOYEE  
                //			                    LEFT OUTER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS   
                //		                    WHERE  PWEMPLOYEE.PWDIVISION IN ('05') 
                //			                    AND PWSECTION.PWSECTION IN ('136','137','138','139','140','09')
                //	                    )
                //
                //		                    UNION
                //                    	 
                //	                    (
                //		                    SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC,PWDIVISION, PWSECTION.PWSECTION 
                //		                    FROM PWEMPLOYEE  
                //			                    LEFT OUTER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS                             
                //		                    WHERE  PWEMPLOYEE.PWDIVISION IN ('75','11') 
                //	                    )
                //                        ");

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                ddl_Section.DataSource = dt;
                ddl_Section.ValueMember = "PWSECTION";
                ddl_Section.DisplayMember = "PWDESC";
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
            if (rdb_Section.ToggleState == ToggleState.On)
            {
                txt_EmpId.Text = "";
                txtDocNumber.Text = "";
                try
                {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                                    @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+' '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                        ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                        ,[Reason]
                                        ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType] 
                                  FROM [IVZ_HROUTOFFICE] 
                                  WHERE  HeadApproved = 1 
                                  AND HrApprovedOut = 1 
                                  AND HrApprovedIn = 1
                                  AND [Status] = 1 
                                  AND [DimentionId] = '" + ddl_Section.SelectedValue.ToString() + "' AND [TrandDateTime] BETWEEN '" + dt_From.Text.ToString() + "' AND  '" + dt_To.Text.ToString() + "' ");

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_ApproveMN.DataSource = dt;
                        this.radButtonCheckAll.Enabled = true;
                        this.radButtonUnChechAll.Enabled = true;
                    }
                    else
                    {
                        // MessageBox.Show("ไม่มีข้อมูล");
                        rgv_ApproveMN.DataSource = dt;
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
            else if (rdb_EmpId.ToggleState == ToggleState.On)
            {
                txtDocNumber.Text = "";
                try
                {
                    if (txt_EmpId.Text != "")
                    {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                                        @"SELECT IVZ_HROUTOFFICE.DocId ,IVZ_HROUTOFFICE.OutOfficeId ,IVZ_HROUTOFFICE.EmplId ,IVZ_HROUTOFFICE.EmplFname+' '+IVZ_HROUTOFFICE.EmplLname As EmplFullName
                                                ,IVZ_HROUTOFFICE.Dimention,IVZ_HROUTOFFICE.Dept
                                                ,CASE IVZ_HROUTOFFICE.OutType WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                                ,IVZ_HROUTOFFICE.Reason
                                                ,CASE IVZ_HROUTOFFICE.CombackType WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS CombackType 
                                            FROM PWEMPLOYEE  
		                                             LEFT OUTER JOIN IVZ_HROUTOFFICE  ON PWEMPLOYEE.PWEMPLOYEE = IVZ_HROUTOFFICE.EmplId collate Thai_CS_AS                                   
                                            WHERE PWEMPLOYEE.PWDIVISION IN ('75','11') 
                                            AND  IVZ_HROUTOFFICE.HeadApproved = 1 
                                            AND IVZ_HROUTOFFICE.HrApprovedOut = 1 
                                            AND IVZ_HROUTOFFICE.HrApprovedIn = 1
                                            AND IVZ_HROUTOFFICE.[Status] = 1
                                            AND (IVZ_HROUTOFFICE.[EmplId] = '{0}' OR IVZ_HROUTOFFICE.[EmplCard] =  '{0}') "
                                        , txt_EmpId.Text.ToString()
                                        , txt_EmpId.Text.ToString()
                                        );


                        //                        sqlCommand.CommandText = string.Format(
                        //                            @"(
                        //                                SELECT IVZ_HROUTOFFICE.DocId ,IVZ_HROUTOFFICE.OutOfficeId ,IVZ_HROUTOFFICE.EmplId ,IVZ_HROUTOFFICE.EmplFname+' '+IVZ_HROUTOFFICE.EmplLname As EmplFullName
                        //                                       ,IVZ_HROUTOFFICE.Dimention,IVZ_HROUTOFFICE.Dept
                        //                                       ,IVZ_HROUTOFFICE.[ShiftId],IVZ_HROUTOFFICE.[StartTime],IVZ_HROUTOFFICE.[EndTime]
                        //                                       ,CASE IVZ_HROUTOFFICE.OutType WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                        //                                       ,IVZ_HROUTOFFICE.Reason ,IVZ_HROUTOFFICE.HrApprovedOutDateTime
                        //                                       ,CASE IVZ_HROUTOFFICE.CombackType WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS CombackType 
                        //                                FROM PWEMPLOYEE  
                        //		                             LEFT OUTER JOIN IVZ_HROUTOFFICE  ON PWEMPLOYEE.PWEMPLOYEE = IVZ_HROUTOFFICE.EmplId collate Thai_CS_AS                                   
                        //                                WHERE PWEMPLOYEE.PWSECTION IN ('136','137','138','139','140','09')
                        //                                AND  IVZ_HROUTOFFICE.HeadApproved = 1 
                        //                                AND IVZ_HROUTOFFICE.HrApprovedOut = 1 
                        //                                AND IVZ_HROUTOFFICE.HrApprovedIn = 1
                        //                                AND IVZ_HROUTOFFICE.[Status] = 1
                        //                                AND (IVZ_HROUTOFFICE.[EmplId] = '{0}' OR IVZ_HROUTOFFICE.[EmplCard] =  '{0}') 
                        //                                AND (IVZ_HROUTOFFICE.[TrandDateTime] BETWEEN '{2}' AND  '{3}' ) 
                        //                              )
                        //
                        //                            UNION
                        //                             (
                        //	                            SELECT IVZ_HROUTOFFICE.DocId ,IVZ_HROUTOFFICE.OutOfficeId ,IVZ_HROUTOFFICE.EmplId ,IVZ_HROUTOFFICE.EmplFname+' '+IVZ_HROUTOFFICE.EmplLname As EmplFullName
                        //                                       ,IVZ_HROUTOFFICE.Dimention,IVZ_HROUTOFFICE.Dept
                        //                                       ,IVZ_HROUTOFFICE.[ShiftId],IVZ_HROUTOFFICE.[StartTime],IVZ_HROUTOFFICE.[EndTime]
                        //                                       ,CASE IVZ_HROUTOFFICE.OutType WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                        //                                       ,IVZ_HROUTOFFICE.Reason ,IVZ_HROUTOFFICE.HrApprovedOutDateTime
                        //                                       ,CASE IVZ_HROUTOFFICE.CombackType WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS CombackType 
                        //                                FROM PWEMPLOYEE  
                        //		                             LEFT OUTER JOIN IVZ_HROUTOFFICE  ON PWEMPLOYEE.PWEMPLOYEE = IVZ_HROUTOFFICE.EmplId collate Thai_CS_AS                                   
                        //                                WHERE PWEMPLOYEE.PWDIVISION IN ('75','11')
                        //                                AND  IVZ_HROUTOFFICE.HeadApproved = 1 
                        //                                AND IVZ_HROUTOFFICE.HrApprovedOut = 1 
                        //                                AND IVZ_HROUTOFFICE.HrApprovedIn = 1
                        //                                AND IVZ_HROUTOFFICE.[Status] = 1
                        //                                AND (IVZ_HROUTOFFICE.[EmplId] = '{0}' OR IVZ_HROUTOFFICE.[EmplCard] =  '{0}') 
                        //                                AND (IVZ_HROUTOFFICE.[TrandDateTime]  BETWEEN '{2}' AND  '{3}' )
                        //                            )"

                        //                            , txtEmplId.Text.ToString()
                        //                            , txtEmplId.Text.ToString()
                        //                            , dtEmplStart.Text.ToString()
                        //                            , dtEmplEnd.Text.ToString());




                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            rgv_ApproveMN.DataSource = dt;
                            this.radButtonCheckAll.Enabled = true;
                            this.radButtonUnChechAll.Enabled = true;
                        }
                        else
                        {
                            // MessageBox.Show("ไม่มีข้อมูล กรูณาตรวจสอบเลขรหัสพนักงานหรือวันที่ให้ถูกต้อง");
                            this.txt_EmpId.Focus();
                            rgv_ApproveMN.DataSource = dt;
                        }

                    }
                    //else
                    //{
                    //    MessageBox.Show("กรูณาใส่รหัสพนักงาน");
                    //    this.txtEmplId.Focus();
                    //}
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
            else if (rdb_DocId.ToggleState == ToggleState.On)
            {
                txt_EmpId.Text = "";
                try
                {
                    if (txt_DocDate.Text != "" && txtDocNumber.Text != "")
                    {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                                    @"SELECT IVZ_HROUTOFFICE.DocId ,IVZ_HROUTOFFICE.OutOfficeId ,IVZ_HROUTOFFICE.EmplId ,IVZ_HROUTOFFICE.EmplFname+' '+IVZ_HROUTOFFICE.EmplLname As EmplFullName
                                                ,IVZ_HROUTOFFICE.Dimention,IVZ_HROUTOFFICE.Dept
                                                ,CASE IVZ_HROUTOFFICE.OutType WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                                ,IVZ_HROUTOFFICE.Reason
                                                ,CASE IVZ_HROUTOFFICE.CombackType WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS CombackType 
                                        FROM PWEMPLOYEE  
		                                         LEFT OUTER JOIN IVZ_HROUTOFFICE  ON PWEMPLOYEE.PWEMPLOYEE = IVZ_HROUTOFFICE.EmplId collate Thai_CS_AS                                   
                                        WHERE PWEMPLOYEE.PWDIVISION IN ('75','11')
                                        AND  IVZ_HROUTOFFICE.HeadApproved = 1 
                                        AND IVZ_HROUTOFFICE.HrApprovedOut = 1 
                                        AND IVZ_HROUTOFFICE.HrApprovedIn = 1
                                        AND IVZ_HROUTOFFICE.[Status] = 1
                                        AND IVZ_HROUTOFFICE.[DocId] = '{0}'  +  '{1}'  + '-' + '{2}'
                                        "
                                        , txt_DocId.Text.ToString().Trim()
                                        , txt_DocDate.Text.Trim()
                                        , txtDocNumber.Text.Trim());

                        //                        sqlCommand.CommandText = string.Format(
                        //                            @"(
                        //                                SELECT IVZ_HROUTOFFICE.DocId ,IVZ_HROUTOFFICE.OutOfficeId ,IVZ_HROUTOFFICE.EmplId ,IVZ_HROUTOFFICE.EmplFname+' '+IVZ_HROUTOFFICE.EmplLname As EmplFullName
                        //                                       ,IVZ_HROUTOFFICE.Dimention,IVZ_HROUTOFFICE.Dept
                        //                                       ,IVZ_HROUTOFFICE.[ShiftId],IVZ_HROUTOFFICE.[StartTime],IVZ_HROUTOFFICE.[EndTime]
                        //                                       ,CASE IVZ_HROUTOFFICE.OutType WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                        //                                       ,IVZ_HROUTOFFICE.Reason ,IVZ_HROUTOFFICE.HrApprovedOutDateTime
                        //                                       ,CASE IVZ_HROUTOFFICE.CombackType WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS CombackType 
                        //                                FROM PWEMPLOYEE  
                        //		                             LEFT OUTER JOIN IVZ_HROUTOFFICE  ON PWEMPLOYEE.PWEMPLOYEE = IVZ_HROUTOFFICE.EmplId collate Thai_CS_AS                                   
                        //                                WHERE PWEMPLOYEE.PWSECTION IN ('136','137','138','139','140','09')
                        //                                AND  IVZ_HROUTOFFICE.HeadApproved = 1 
                        //                                AND IVZ_HROUTOFFICE.HrApprovedOut = 1 
                        //                                AND IVZ_HROUTOFFICE.HrApprovedIn = 1
                        //                                AND IVZ_HROUTOFFICE.[Status] = 1
                        //                                AND IVZ_HROUTOFFICE.[DocId] = '{0}'  +  '{1}'  + '-' + '{2}'
                        //                              )
                        //
                        //                            UNION
                        //
                        //                             (
                        //	                            SELECT IVZ_HROUTOFFICE.DocId ,IVZ_HROUTOFFICE.OutOfficeId ,IVZ_HROUTOFFICE.EmplId ,IVZ_HROUTOFFICE.EmplFname+' '+IVZ_HROUTOFFICE.EmplLname As EmplFullName
                        //                                       ,IVZ_HROUTOFFICE.Dimention,IVZ_HROUTOFFICE.Dept
                        //                                       ,IVZ_HROUTOFFICE.[ShiftId],IVZ_HROUTOFFICE.[StartTime],IVZ_HROUTOFFICE.[EndTime]
                        //                                       ,CASE IVZ_HROUTOFFICE.OutType WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                        //                                       ,IVZ_HROUTOFFICE.Reason ,IVZ_HROUTOFFICE.HrApprovedOutDateTime
                        //                                       ,CASE IVZ_HROUTOFFICE.CombackType WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS CombackType 
                        //                                FROM PWEMPLOYEE  
                        //		                             LEFT OUTER JOIN IVZ_HROUTOFFICE  ON PWEMPLOYEE.PWEMPLOYEE = IVZ_HROUTOFFICE.EmplId collate Thai_CS_AS                                   
                        //                                WHERE PWEMPLOYEE.PWDIVISION IN ('75','11')
                        //                                AND  IVZ_HROUTOFFICE.HeadApproved = 1 
                        //                                AND IVZ_HROUTOFFICE.HrApprovedOut = 1 
                        //                                AND IVZ_HROUTOFFICE.HrApprovedIn = 1
                        //                                AND IVZ_HROUTOFFICE.[Status] = 1
                        //                                AND IVZ_HROUTOFFICE.[DocId] = '{0}'  +  '{1}'  + '-' + '{2}'
                        //                            )"

                        //                            , txtOut.Text.ToString().Trim()
                        //                            , txtDocDate.Text.Trim()
                        //                            , txtDocNumber.Text.Trim()); 


                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            rgv_ApproveMN.DataSource = dt;

                            this.radButtonCheckAll.Enabled = true;
                            this.radButtonUnChechAll.Enabled = true;
                        }
                        else
                        {
                            // MessageBox.Show("ไม่มีข้อมูล กรูณาตรวจสอบเลขที่เอกสารให้ถูกต้อง");
                            this.txtDocNumber.Focus();
                            rgv_ApproveMN.DataSource = dt;
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
        #endregion

        #endregion

        #region button

        void radButtonserch_Click(object sender, EventArgs e)
        {
            //this.Getdata();
            this.SearchData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.SearchData();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void radButtonSave_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void radButtonCheckAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewDataRowInfo row in rgv_ApproveMN.Rows)
            {
                row.Cells["CHECK"].Value = true;
            }
        }

        private void radButtonUnChechAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewDataRowInfo row in rgv_ApproveMN.Rows)
            {
                row.Cells["CHECK"].Value = false;
            }
        }

        #endregion
    }
}