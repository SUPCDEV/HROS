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
    public partial class FormReportStatusDoc : Form
    {
        //SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);
        string[] secureKey = new string[] { };
       // string configSecureKey = "OF-HR-W";

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


        public FormReportStatusDoc()
        {
            InitializeComponent();
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;
            this.KeyPreview = true;

            #region GridViewShowData
            // GridData
          
            this.radGridViewStatusDoc.Dock = DockStyle.Fill;
            this.radGridViewStatusDoc.AutoGenerateColumns = true;
            this.radGridViewStatusDoc.EnableFiltering = false;
            this.radGridViewStatusDoc.AllowAddNewRow = false;
            //this.GridData.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.HeaderCells);
            //this.GridData.MasterTemplate.ShowHeaderCellButtons = true;
            //this.GridData.MasterTemplate.ShowFilteringRow = false;
            this.radGridViewStatusDoc.MasterTemplate.AutoGenerateColumns = false;
            this.radGridViewStatusDoc.ShowGroupedColumns = true;
            this.radGridViewStatusDoc.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.radGridViewStatusDoc.EnableHotTracking = true;
            this.radGridViewStatusDoc.AutoSizeRows = true;

            #endregion
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                radButtonserch.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void FormShowData_Load(object sender, EventArgs e)
        {
            this.radButtonserch.Click += new EventHandler(radButtonserch_Click);

            this.dtpStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dtpEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.txtDocDate.Text = DateTime.Now.ToString("yyMMdd");
            this.txtOut.Text = "OUT";
            

            this.GetDimention();

            //this.GetDataEmpl();
        }

        #region GetDimention
        private void GetDimention()
        {
            #region SQL
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

//                sqlCommand.CommandText = string.Format(
//                        @"  SELECT DISTINCT * FROM(
//                            SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC 
//                            FROM PWEMPLOYEE  
//	                                LEFT OUTER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS   
//                            where PWEMPLOYEE = '{0}'                         
//                                
//                            ) AS PWSECTION "
//                        , ClassCurUser.LogInEmplId);

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

            #endregion
        }

        #endregion

        #region GetData
        public void Getdata()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            if (RadioDocId.ToggleState == ToggleState.On)
            {
                
                try
                {
                    if (txtDocDate.Text !="" && txtDocNumber.Text != "")
                    {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                                    @"
                                      SELECT [DocId],[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                          ,Reason
	                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],HrApprovedInName
                                          ,[ToTallTimeUse]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1 
                                      AND [DocId] = '" + txtOut.Text.ToString().Trim() + "' + '" + txtDocDate.Text.Trim() + "' + '-' + '" + txtDocNumber.Text.Trim() + "' ");
                        //convert(varchar,getdate(),23)
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        radGridViewStatusDoc.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("กรูณาใส่เลขที่ใบออกนอก");
                        this.txtDocNumber.Focus();
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
            else if (RadioDimention.ToggleState == ToggleState.On)
            {
                txtDocNumber.Text = "";
                try
                {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                                @"SELECT DocId,[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,Reason
                                          ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
										  ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut]
	                                      ,[HrApprovedOutDateTime],[HrApprovedOutName]
	                                      ,[HrApprovedInDateTime],HrApprovedInName
                                          ,[ToTallTimeUse]
                                 FROM [IVZ_HROUTOFFICE] 
                                 WHERE [Status] = 1
                                 AND [DimentionId] = '" + DdlDimention.SelectedValue.ToString() + "' AND [TrandDateTime] BETWEEN '" + dtpStart.Text.ToString() + "' AND  '" + dtpEnd.Text.ToString() + "' ");
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
            else
            {
                MessageBox.Show("เลือกการกรองข้อมูล");
            }
        }
        #endregion

        void radButtonserch_Click(object sender, EventArgs e)
        {
            this.Getdata();
        }        

        #region old code
        public void GetDataEmpl()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
                try
                {
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                                    @"SELECT [OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                      ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                      ,[HeadApprovedName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                      ,CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut],[HrApprovedOutName]
	                                      ,ISNULL(CONVERT(VARCHAR, [HrApprovedOutDateTime],120),'0000-00-00 00:00:00') AS [HrApprovedOutDateTime]
	                                      ,ISNULL(CONVERT(VARCHAR, [HrApprovedInDateTime],120),'0000-00-00 00:00:00')  AS [HrApprovedInDateTime]
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE [Status] = 1
                                      AND HrApprovedOut = 1
                                      AND [EmplId] = 'M1007034' ");
                        //convert(varchar,getdate(),23) " + lblEmplId.Text + "
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

//        private void GridViewShowData_CellFormatting(object sender, CellFormattingEventArgs e)
//        {
//            if (!(e.CellElement.RowElement is GridTableHeaderRowElement))
//            {
//                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
//                {
//                    GridViewCommandColumn column = (GridViewCommandColumn)e.CellElement.ColumnInfo;
//                    RadButtonElement button = (RadButtonElement)e.CellElement.Children[0];

//                    if (column.Name == "Delete")
//                    {
//                        RadButtonElement element = (RadButtonElement)e.CellElement.Children[0];
//                        element.DisplayStyle = DisplayStyle.Image;
//                        // This is an example of course 
//                        element.Image = Properties.Resources.Close;
//                        element.ImageAlignment = ContentAlignment.MiddleCenter;
//                        //e.CellElement.StringAlignment = StringAlignment.Center;


//                    }
//                }
//            }
//        }
//        private void GridViewShowData_CommandCellClick(object sender, EventArgs e)
//        {
//            GridCommandCellElement cell = (GridCommandCellElement)sender;

//            string Docno = cell.RowInfo.Cells["OutOfficeId"].Value.ToString();

//            if (con.State == ConnectionState.Open) con.Close();
//            con.Open();
//            if (cell.ColumnInfo.Name == "Delete")
//            {
//                if (MessageBox.Show("ยืนยันยกเลิกเอกสารใบออกนอกออนไลน์ ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//                {
//                    try
//                    {
//                        SqlCommand sqlCommand = new SqlCommand();
//                        sqlCommand.Connection = con;
//                        //sqlCommand.Transaction = tr;
//                        foreach (GridViewDataRowInfo row in GridViewShowData.Rows)
//                        {
//                            if (row.Index > -1)
//                            {
//                                sqlCommand.CommandText = string.Format(
//                                @" UPDATE [IVZ_HROUTOFFICE] SET [Status] = 0 
//                                                                    ,[ModifiedBy] = @ModifiedBy{0}
//                                                                    ,[ModifiedName] = @ModifiedName{0}
//                                                                    ,[ModifiedDateTime] =  (dateadd(millisecond, -datepart(millisecond,getdate()),getdate()))
//                                                               WHERE [OutOfficeId] = @OutOfficeId{0}", row.Index
//                                      );

//                                sqlCommand.Parameters.AddWithValue(string.Format(@"ModifiedBy{0}", row.Index), lblEmplId.Text.ToString().Trim());
//                                sqlCommand.Parameters.AddWithValue(string.Format(@"ModifiedName{0}", row.Index), lblEmplName.Text.ToString().Trim());
//                                sqlCommand.Parameters.AddWithValue(string.Format(@"OutOfficeId{0}", row.Index), row.Cells["OutOfficeId"].Value.ToString());

//                                sqlCommand.ExecuteNonQuery();
//                                break;
//                            }
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        MessageBox.Show(ex.ToString());

//                    }
//                    finally
//                    {
//                        if (con.State == ConnectionState.Open) con.Close();
//                        this.GetDataEmpl();
//                    }
//                }
//            }
//        }

        #endregion 

        private void GridViewShowData_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.Column.Name == "OutOfficeId")
                {
                    GridViewRowInfo row = (GridViewRowInfo)radGridViewStatusDoc.Rows[e.RowIndex];

                    string DocId = row.Cells["DocId"].Value.ToString();
                    string OutId = row.Cells["OutOfficeId"].Value.ToString();

                    FormDetailShowData Frm = new FormDetailShowData(DocId,OutId);
                    Frm.Show();
                }
            }
        }
    }
}
