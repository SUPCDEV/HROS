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
    public partial class FormReportHROut : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);

        string sysuser;
        string sectionid;
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

        public FormReportHROut()
        {
            InitializeComponent();
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;

            #region GridViewShowData

            this.radGridViewStatusDoc.Dock = DockStyle.Fill;
            this.radGridViewStatusDoc.AutoGenerateColumns = true;
            this.radGridViewStatusDoc.EnableFiltering = false;
            this.radGridViewStatusDoc.AllowAddNewRow = false;
            this.radGridViewStatusDoc.MasterTemplate.AutoGenerateColumns = false;
            this.radGridViewStatusDoc.ShowGroupedColumns = true;
            this.radGridViewStatusDoc.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.radGridViewStatusDoc.EnableHotTracking = true;
            this.radGridViewStatusDoc.AutoSizeRows = true;

            #endregion
        }

        private void FormReportHROut_Load(object sender, EventArgs e)
        {
            this.sysuser = SysOutoffice;
            this.sectionid = Section;
            this.radButtonserch.Click += new EventHandler(radButtonserch_Click);

            this.dtpStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dtpEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.GetDimention();
        }

        void radButtonserch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            GetData();
        }

        public void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            #region ทั้งหมด
            if (rdb_All.ToggleState == ToggleState.On)
            {
                //ธุระส่วนตัว
                #region ธุระส่วนตัว
                if (Radio1.ToggleState == ToggleState.On)
                {
                    #region กลับเข้ามา

                    if (CheckBox1.ToggleState == ToggleState.On && CheckBox2.ToggleState == ToggleState.Off)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = con;

                            sqlCommand.CommandText = string.Format(
                                        @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 2 
                                      AND [CombackType] = 1 
                                      AND [TrandDateTime] BETWEEN '" + dtpStart.Text.ToString() + "' AND  '" + dtpEnd.Text.ToString() + "' ");
                            //convert(varchar,getdate(),23)
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);
                            radGridViewStatusDoc.DataSource = dt;
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
                    #endregion

                    #region ไม่กลับเข้ามา


                    else if (CheckBox2.ToggleState == ToggleState.On && CheckBox1.ToggleState == ToggleState.Off)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = con;

                            sqlCommand.CommandText = string.Format(
                                        @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 2
                                      AND [CombackType] = 2  
                                      AND [TrandDateTime] BETWEEN '" + dtpStart.Text.ToString() + "' AND  '" + dtpEnd.Text.ToString() + "' ");
                            //convert(varchar,getdate(),23)
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);
                            radGridViewStatusDoc.DataSource = dt;
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
                    #endregion

                    #region กลับเข้ามา && ไม่กลับ


                    else if (CheckBox1.ToggleState == ToggleState.On && CheckBox2.ToggleState == ToggleState.On)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = con;

                            sqlCommand.CommandText = string.Format(
                                        @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 2
                                      AND [CombackType] IN ('1','2')
                                      AND [TrandDateTime] BETWEEN '" + dtpStart.Text.ToString() + "' AND  '" + dtpEnd.Text.ToString() + "' ");
                            //convert(varchar,getdate(),23)
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);
                            radGridViewStatusDoc.DataSource = dt;
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
                    #endregion
                    else
                    {
                        MessageBox.Show("กรุณาเลือกCheckBox");
                    }
                }
                #endregion
                // งานบริษัท
                #region งานบริษัท


                else if (Radio2.ToggleState == ToggleState.On)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                                    @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 1 
                                      AND [TrandDateTime] BETWEEN '" + dtpStart.Text.ToString() + "' AND  '" + dtpEnd.Text.ToString() + "' ");
                        //convert(varchar,getdate(),23)
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        radGridViewStatusDoc.DataSource = dt;
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
                #endregion
            }
            #endregion

            #region แยกตามแผนก
            if (rdb_Section.ToggleState == ToggleState.On)
            {
                #region ธุระส่วนตัว
                if (Radio1.ToggleState == ToggleState.On)
                {
                    #region กลับเข้ามา

                    if (CheckBox1.ToggleState == ToggleState.On && CheckBox2.ToggleState == ToggleState.Off)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = con;

                            sqlCommand.CommandText = string.Format(
                                        @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 2 
                                      AND [CombackType] = 1 
                                      AND  [DimentionId] = '{0}'
                                      AND [TrandDateTime] BETWEEN '{1}' AND '{2}'"
                                    , DdlDimention.SelectedValue.ToString()
                                    , dtpStart.Text.ToString()
                                    , dtpEnd.Text.ToString()
                                        );
                            //convert(varchar,getdate(),23)
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);
                            radGridViewStatusDoc.DataSource = dt;
            
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
                    #endregion

                    #region ไม่กลับเข้ามา
                    else if (CheckBox2.ToggleState == ToggleState.On && CheckBox1.ToggleState == ToggleState.Off)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = con;

                            sqlCommand.CommandText = string.Format(
                                        @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 2
                                      AND [CombackType] = 2  
                                      AND  [DimentionId] = '{0}'
                                      AND [TrandDateTime] BETWEEN '{1}' AND '{2}'"
                                    , DdlDimention.SelectedValue.ToString()
                                    , dtpStart.Text.ToString()
                                    , dtpEnd.Text.ToString()
                                        );
                            //convert(varchar,getdate(),23)
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);
                            radGridViewStatusDoc.DataSource = dt;
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
                    #endregion

                    #region กลับเข้ามา && ไม่กลับ
                    else if (CheckBox1.ToggleState == ToggleState.On && CheckBox2.ToggleState == ToggleState.On)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = con;

                            sqlCommand.CommandText = string.Format(
                                        @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 2
                                      AND [CombackType] IN ('1','2')
                                      AND  [DimentionId] = '{0}'
                                      AND [TrandDateTime] BETWEEN '{1}' AND '{2}'"
                                    , DdlDimention.SelectedValue.ToString()
                                    , dtpStart.Text.ToString()
                                    , dtpEnd.Text.ToString()
                                        );
                            //convert(varchar,getdate(),23)
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);
                            radGridViewStatusDoc.DataSource = dt;
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
                    #endregion
                    else
                    {
                        MessageBox.Show("กรุณาเลือกCheckBox");
                    }
                }
                #endregion

                #region งานบริษัท
                if (Radio2.ToggleState == ToggleState.On)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                                    @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 1
                                      AND  [DimentionId] = '{0}'
                                      AND [TrandDateTime] BETWEEN '{1}' AND '{2}'"
                                    , DdlDimention.SelectedValue.ToString()
                                    , dtpStart.Text.ToString()
                                    , dtpEnd.Text.ToString()
                                        );
                        //AND [TrandDateTime] BETWEEN '" + dtpStart.Text.ToString() + "' AND  '" + dtpEnd.Text.ToString() + "' 
                        //convert(varchar,getdate(),23)
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        radGridViewStatusDoc.DataSource = dt;
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
                #endregion
            }
            #endregion

            #region แยกตามรหัสพนักงาน
            if (rdb_Emplid.ToggleState == ToggleState.On)
            {
                #region ธุระส่วนตัว
                if (Radio1.ToggleState == ToggleState.On)
                {
                    #region กลับเข้ามา

                    if (CheckBox1.ToggleState == ToggleState.On && CheckBox2.ToggleState == ToggleState.Off)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = con;

                            sqlCommand.CommandText = string.Format(
                                        @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 2 
                                      AND [CombackType] = 1 
                                      AND  EmplId = '{0}'
                                      AND [TrandDateTime] BETWEEN '{1}' AND '{2}'"
                                    , txt_Emplid.Text
                                    , dtpStart.Text.ToString()
                                    , dtpEnd.Text.ToString()
                                        );
                            //convert(varchar,getdate(),23)
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);
                            radGridViewStatusDoc.DataSource = dt;
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
                    #endregion

                    #region ไม่กลับเข้ามา


                    else if (CheckBox2.ToggleState == ToggleState.On && CheckBox1.ToggleState == ToggleState.Off)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = con;

                            sqlCommand.CommandText = string.Format(
                                        @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 2
                                      AND [CombackType] = 2  
                                      AND  EmplId = '{0}'
                                      AND [TrandDateTime] BETWEEN '{1}' AND '{2}'"
                                    , txt_Emplid.Text
                                    , dtpStart.Text.ToString()
                                    , dtpEnd.Text.ToString()
                                        );
                            //convert(varchar,getdate(),23)
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);
                            radGridViewStatusDoc.DataSource = dt;
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
                    #endregion

                    #region กลับเข้ามา && ไม่กลับ


                    else if (CheckBox1.ToggleState == ToggleState.On && CheckBox2.ToggleState == ToggleState.On)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = con;

                            sqlCommand.CommandText = string.Format(
                                        @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 2
                                      AND [CombackType] IN ('1','2')
                                      AND  EmplId = '{0}'
                                      AND [TrandDateTime] BETWEEN '{1}' AND '{2}'"
                                    , txt_Emplid.Text
                                    , dtpStart.Text.ToString()
                                    , dtpEnd.Text.ToString()
                                        );
                            //convert(varchar,getdate(),23)
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);
                            radGridViewStatusDoc.DataSource = dt;
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
                    #endregion
                    else
                    {
                        MessageBox.Show("กรุณาเลือกCheckBox");
                    }
                }
                #endregion

                #region งานบริษัท
                if (Radio2.ToggleState == ToggleState.On)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                                    @"SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,Reason,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],[HrApprovedInName]
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
									  AND [OutType] = 1
                                      AND  EmplId = '{0}'
                                      AND [TrandDateTime] BETWEEN '{1}' AND '{2}'"
                                    ,txt_Emplid.Text    
                                    ,dtpStart.Text.ToString()
                                    ,dtpEnd.Text.ToString()
                                        );
                        //AND [TrandDateTime] BETWEEN '" + dtpStart.Text.ToString() + "' AND  '" + dtpEnd.Text.ToString() + "' 
                        //convert(varchar,getdate(),23)
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        radGridViewStatusDoc.DataSource = dt;
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
                #endregion
            }
            #endregion
        }

        #region Print
        void exporter_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {

            string headerText = "รายงานออกนอกบริษัทประจำวันที่:  " + dtpStart.Text + " ถึง " + dtpEnd.Text;
            Telerik.WinControls.UI.Export.ExcelML.SingleStyleElement style = ((ExportToExcelML)sender).AddCustomExcelRow(e.ExcelTableElement, 30, headerText);
            style.FontStyle.Bold = true;
            style.FontStyle.Size = 15;
            style.FontStyle.Color = Color.White;
            style.InteriorStyle.Color = Color.CadetBlue;
            style.InteriorStyle.Pattern = Telerik.WinControls.UI.Export.ExcelML.InteriorPatternType.Solid;
            style.AlignmentElement.HorizontalAlignment = Telerik.WinControls.UI.Export.ExcelML.HorizontalAlignmentType.Center;
            style.AlignmentElement.VerticalAlignment = Telerik.WinControls.UI.Export.ExcelML.VerticalAlignmentType.Center;
        }
        void excelExporter_ExcelCellFormatting(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventArgs e)
        {
            if (e.GridRowInfoType == typeof(GridViewTableHeaderRowInfo))
            {
                Telerik.WinControls.UI.Export.ExcelML.BorderStyles border = new Telerik.WinControls.UI.Export.ExcelML.BorderStyles();

                border.Color = Color.Black;
                border.Weight = 2;
                border.LineStyle = Telerik.WinControls.UI.Export.ExcelML.LineStyle.Continuous;
                border.PositionType = Telerik.WinControls.UI.Export.ExcelML.PositionType.Bottom;
                e.ExcelStyleElement.Borders.Add(border);
            }

        }
        private void radButtonExport_Click(object sender, EventArgs e)
        {

            if (radGridViewStatusDoc.RowCount != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                ExportToExcelML excelExporter = null;

                try
                {
                    ExportToExcelML excelML = new ExportToExcelML(this.radGridViewStatusDoc);
                    excelExporter = new ExportToExcelML(this.radGridViewStatusDoc);
                    excelExporter.ExcelCellFormatting += excelExporter_ExcelCellFormatting;
                    excelExporter.ExcelTableCreated += exporter_ExcelTableCreated;

                    this.Cursor = Cursors.WaitCursor;
                    saveFileDialog.Filter = "Excel (*.xls)|*.xls";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        excelExporter.RunExport(saveFileDialog.FileName);

                        DialogResult dr = RadMessageBox.Show("การบันทึกไฟล์สำเร็จ คุณต้องการเปิดไฟล์หรือไม่?",
                            "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveFileDialog.FileName);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                RadMessageBox.Show("ไม่มีข้อมูล");
                return;
            }
        }
        #endregion

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
	                             WHERE PWSECTION.PWDESC NOT IN ('NULL','NONE') 
                            )
                            AS PWSECTION
                            ORDER BY PWDESC  ");


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
    }
}
