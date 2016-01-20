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
    public partial class FormDeleteEmpl : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);

        private RadGridView gridLines;

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

        public FormDeleteEmpl()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;
            // <layOut>
            this.radGroupBox1.Dock = DockStyle.Fill;
            this.GridViewShowData.Dock = DockStyle.Fill;

            // <Status>
            this.statusMessage.Text = string.Empty;
            this.statusF5.Text = string.Format(@"{0}: {1}", "F5", "Refresh");

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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                this.Getdata(); // GetData();                       
            }
            else if (keyData == Keys.Escape)
            {
                this.Close();                
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void FormReportEmpl_Load(object sender, EventArgs e)
        {
            this.Getdata();
        }

        public void Getdata()
        {
            //string queryRun =
//                string.Format(@"SELECT
//	                            DocId,[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
//	                            ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
//	                            ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
//	                            ,ISNULL([HeadApprovedName],'-') AS [HeadApprovedName]
//	                            ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
//                            FROM	[IVZ_HROUTOFFICE] WITH (NOLOCK)
//                            WHERE	[DocId] IN (
//	                            SELECT	DISTINCT DocId
//	                            FROM	[IVZ_HROUTOFFICE] WITH (NOLOCK)
//	                            WHERE
//		                            ([EmplId] = '{0}' OR [CreatedBy] = '{0}')
//                            )
//                            AND ([EmplId] = '{0}' AND [Status] = 1 AND [HrApprovedOut] = 1)
//                                    OR ([CreatedBy] = '{0}' AND [Status] = 1 AND [HrApprovedOut] = 1)", ClassUser.LogInEmplId);
            string queryRun =
            string.Format(@"SELECT
	                            DocId,[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
	                            ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType 
	                            ,[CreatedName],CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                            ,ISNULL([HeadApprovedName],'-') AS [HeadApprovedName]
	                            ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
                            FROM	[IVZ_HROUTOFFICE] WITH (NOLOCK)
                            WHERE	[DocId] IN (
	                            SELECT	DISTINCT DocId
	                            FROM	[IVZ_HROUTOFFICE] WITH (NOLOCK)
	                            WHERE
		                            ([EmplId] = '{0}' OR [CreatedBy] = '{0}')
                            )
                            AND ([EmplId] = '{0}' AND [Status] = 1 AND [HrApprovedOut] = 1 )
                            OR [CreatedBy] = '{0}' AND [Status] = 1 AND [HrApprovedOut] = 1 ", ClassCurUser.LogInEmplId);



            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.CommandText = queryRun;

//                sqlCommand.CommandText = string.Format(
//                            @"SELECT DocId,[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
//                                          ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
//	                                      ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
//	                                      ,ISNULL([HeadApprovedName],'-') AS HeadApprovedName
//	                                      ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
//	                                      
//                                      FROM [IVZ_HROUTOFFICE] 
//                                      WHERE [Status] = 1
//                                      AND HrApprovedOut = 1
//                                      AND ([EmplId] = '{0}' OR CreatedBy = '{0}')", ClassUser.LogInEmplId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                GridViewShowData.DataSource = dt;
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

        private void GridViewShowData_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.Column.Name == "OutOfficeId")
                {
                    GridViewRowInfo row = (GridViewRowInfo)GridViewShowData.Rows[e.RowIndex];

                    

                    string DocId = row.Cells["DocId"].Value.ToString();
                    string OutId = row.Cells["OutOfficeId"].Value.ToString();


                    FormDetailShowData Frm = new FormDetailShowData(DocId,OutId);
                    Frm.Show();
                }
            }
        }

        private void GridViewShowData_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (!(e.CellElement.RowElement is GridTableHeaderRowElement))
            {
                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    GridViewCommandColumn column = (GridViewCommandColumn)e.CellElement.ColumnInfo;
                    RadButtonElement button = (RadButtonElement)e.CellElement.Children[0];

                    if (column.Name == "Delete")
                    {
                        RadButtonElement element = (RadButtonElement)e.CellElement.Children[0];
                        element.DisplayStyle = DisplayStyle.Image;

                        element.Image = Properties.Resources.Close;
                        element.ImageAlignment = ContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        private void GridViewShowData_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;

            string Docno = cell.RowInfo.Cells["DocId"].Value.ToString();
            string Docid = cell.RowInfo.Cells["OutOfficeId"].Value.ToString();
            con.Open();

            if (cell.ColumnInfo.Name == "Delete")
            {
                if (MessageBox.Show("ยืนยันยกเลิกเอกสารใบออกนอกออนไลน์ ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                            if (cell.RowIndex > -1)
                            {
                                sqlCommand.CommandText = string.Format(
                                @" UPDATE [IVZ_HROUTOFFICE] SET [Status] = 0 
                                                                    ,[ModifiedBy] = @ModifiedBy{0}
                                                                    ,[ModifiedName] = @ModifiedName{0}
                                                                    ,[ModifiedDateTime] =  (dateadd(millisecond, -datepart(millisecond,getdate()),getdate()))
                                                               WHERE [DocId] = @DocId{0} 
                                                                AND OutOfficeId = @OutOfficeId{0}", cell.RowIndex
                                    //AND OutOfficeId = @OutOfficeId{0}
                                      );
                                sqlCommand.Parameters.AddWithValue(string.Format(@"ModifiedBy{0}", cell.RowIndex), ClassCurUser.LogInEmplId);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"ModifiedName{0}", cell.RowIndex), ClassCurUser.LogInEmplName);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"DocId{0}", cell.RowIndex), cell.RowInfo.Cells["DocId"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"OutOfficeId{0}", cell.RowIndex), cell.RowInfo.Cells["OutOfficeId"].Value.ToString());
                                
                                //sqlCommand.Parameters.AddWithValue(string.Format(@"ModifiedBy{0}", cell.RowIndex), ClassUser.LogInEmplId);
                                //sqlCommand.Parameters.AddWithValue(string.Format(@"ModifiedName{0}", cell.RowIndex), ClassUser.LogInEmplName);
                                //sqlCommand.Parameters.AddWithValue(string.Format(@"DocId{0}", cell.RowIndex), row.Cells["DocId"].Value.ToString());
                                //sqlCommand.Parameters.AddWithValue(string.Format(@"OutOfficeId{0}", cell.RowIndex), row.Cells["OutOfficeId"].Value.ToString());

                                var rowAffected = sqlCommand.ExecuteNonQuery();                              
                            }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                        Getdata();
                    }
                }
            }
        }
    }
}
