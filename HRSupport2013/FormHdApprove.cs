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
    public partial class FormHdApprove : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);

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

        public FormHdApprove()
        {
            InitializeComponent();

            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;

            #region <Windown Form>

            #endregion
            #region radGridViewl
            // GridData
            this.GridViewShowData.Dock = DockStyle.Fill;
            this.GridViewShowData.AutoGenerateColumns = true;
            this.GridViewShowData.EnableFiltering = false;
            this.GridViewShowData.AllowAddNewRow = false;
            //this.GridData.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.HeaderCells);
            //this.GridData.MasterTemplate.ShowHeaderCellButtons = true;
            //this.GridData.MasterTemplate.ShowFilteringRow = false;
            this.GridViewShowData.MasterTemplate.AutoGenerateColumns = false;
            this.GridViewShowData.ShowGroupedColumns = true;
            this.GridViewShowData.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.GridViewShowData.EnableHotTracking = true;
            this.GridViewShowData.AutoSizeRows = true;
            #endregion

            this.Load += new EventHandler(Form4_Load);
        }

        void Form4_Load(object sender, EventArgs e)
        {

            this.sysuser = SysOutoffice;

            this.radButtonserch.Click += new EventHandler(radButtonserch_Click);
            this.radButtonSave.Click += new EventHandler(radButtonSave_Click);
            this.radButtonCheckAll.Enabled = false;
            this.radButtonUnChechAll.Enabled = false;

            //this.LabelDateTop.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //this.LabelTimeTop.Text = DateTime.Now.ToString("HH:mm:ss");

            this.dtpStart.Text = DateTime.Now.AddDays(-7).Date.ToString("yyyy-MM-dd");
            this.dtpEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            

            this.txtDocDate.Text = DateTime.Now.ToString("yyMMdd");
            this.txtOut.Text = "OUT";
            
            this.GetDimention();
        }
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
//                        @" SELECT RTRIM([PWSECTION]) AS PWSECTION,RTRIM ([PWDESC]) AS PWDESC
//                            FROM [dbo].[PWSECTION]
//                            ORDER BY [PWDESC] ");


                          @" SELECT DISTINCT * FROM
                            (
	                            SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC 
	                            FROM PWEMPLOYEE  
		                             LEFT OUTER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS                                
	                            WHERE PWEMPLOYEE.PWDIVISION NOT IN ('75','11') 
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
                                        ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                        ,[Reason]
                                        ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType] 
                                  FROM [IVZ_HROUTOFFICE] 
                                  WHERE  HeadApproved = 1 AND HrApprovedOut = 1 AND HrApprovedIn = 1
                                  AND [Status] = 1 
                                  AND [DimentionId] = '" + DdlDimention.SelectedValue.ToString() + "' AND [TrandDateTime] BETWEEN '" + dtpStart.Text.ToString() + "' AND  '" + dtpEnd.Text.ToString() + "' ");
                    //convert(varchar,getdate(),23)
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if(dt.Rows.Count>0)
                        {
                            GridViewShowData.DataSource = dt;
                            this.radButtonCheckAll.Enabled = true;
                            this.radButtonUnChechAll.Enabled = true;
                    }
                    else
                        {
                           // MessageBox.Show("ไม่มีข้อมูล");
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
                                        @"SELECT IVZ_HROUTOFFICE.DocId ,IVZ_HROUTOFFICE.OutOfficeId ,IVZ_HROUTOFFICE.EmplId ,IVZ_HROUTOFFICE.EmplFname+' '+IVZ_HROUTOFFICE.EmplLname As EmplFullName
                                                ,IVZ_HROUTOFFICE.Dimention,IVZ_HROUTOFFICE.Dept
                                                ,CASE IVZ_HROUTOFFICE.OutType WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                                ,IVZ_HROUTOFFICE.Reason
                                                ,CASE IVZ_HROUTOFFICE.CombackType WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS CombackType 
                                        FROM PWEMPLOYEE  
		                                         LEFT OUTER JOIN IVZ_HROUTOFFICE  ON PWEMPLOYEE.PWEMPLOYEE = IVZ_HROUTOFFICE.EmplId collate Thai_CS_AS                                   
                                        WHERE PWEMPLOYEE.PWDIVISION NOT IN ('75','11')
                                        AND  IVZ_HROUTOFFICE.HeadApproved = 1 
                                        AND IVZ_HROUTOFFICE.HrApprovedOut = 1 
                                        AND IVZ_HROUTOFFICE.HrApprovedIn = 1
                                        AND IVZ_HROUTOFFICE.[Status] = 1
                                        AND (IVZ_HROUTOFFICE.[EmplId] = '" + txtEmplId.Text.ToString() + "' OR IVZ_HROUTOFFICE.[EmplCard] =  '" + txtEmplId.Text.ToString() + "') ");
                        //AND [TrandDateTime] BETWEEN '" + dtEmplStart.Text.ToString() + "' AND  '" + dtEmplEnd.Text.ToString() + "'
                        //convert(varchar,getdate(),23)
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if(dt.Rows.Count>0)
                        {
                            GridViewShowData.DataSource = dt;
                            this.radButtonCheckAll.Enabled = true;
                            this.radButtonUnChechAll.Enabled = true;
                        }
                        else
                        {
                           // MessageBox.Show("ไม่มีข้อมูล กรูณาตรวจสอบเลขรหัสพนักงานหรือวันที่ให้ถูกต้อง");
                            this.txtEmplId.Focus();
                            GridViewShowData.DataSource = dt;
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
                                        @"SELECT IVZ_HROUTOFFICE.DocId ,IVZ_HROUTOFFICE.OutOfficeId ,IVZ_HROUTOFFICE.EmplId ,IVZ_HROUTOFFICE.EmplFname+' '+IVZ_HROUTOFFICE.EmplLname As EmplFullName
                                                ,IVZ_HROUTOFFICE.Dimention,IVZ_HROUTOFFICE.Dept
                                                ,CASE IVZ_HROUTOFFICE.OutType WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                                ,IVZ_HROUTOFFICE.Reason
                                                ,CASE IVZ_HROUTOFFICE.CombackType WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS CombackType 
                                        FROM PWEMPLOYEE  
		                                         LEFT OUTER JOIN IVZ_HROUTOFFICE  ON PWEMPLOYEE.PWEMPLOYEE = IVZ_HROUTOFFICE.EmplId collate Thai_CS_AS                                   
                                        WHERE PWEMPLOYEE.PWDIVISION NOT IN ('75','11') 
                                        AND  IVZ_HROUTOFFICE.HeadApproved = 1 
                                        AND IVZ_HROUTOFFICE.HrApprovedOut = 1 
                                        AND IVZ_HROUTOFFICE.HrApprovedIn = 1
                                        AND IVZ_HROUTOFFICE.[Status] = 1
                                        AND IVZ_HROUTOFFICE.[DocId] = '" + txtOut.Text.ToString().Trim() + "' + '" + txtDocDate.Text.Trim() + "' + '-' + '" + txtDocNumber.Text.Trim() + "' ");
                            //AND [DocId] = '" + txtDocId.Text.ToString().Trim() + "' 
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

        #region buttom
        #region Even CheckAll
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
        #endregion
        void radButtonserch_Click(object sender, EventArgs e)
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
        void radButtonSave_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
        #endregion

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
                                    @"UPDATE [IVZ_HROUTOFFICE] SET [HeadApproved] = 2 
                                                                    ,[HeadApprovedBy] = @HeadApprovedBy{0}
                                                                    ,[HeadApprovedName] = @HeadApprovedName{0}
                                                                    ,[HeadApprovedDateTime] =  (dateadd(millisecond, -datepart(millisecond,getdate()),getdate()))
                                                               WHERE [OutOfficeId] = @OutOfficeId{0} 
                                                               AND DocId = @DocId{0} ", row.Index
                                      );
                                    sqlCommand.Parameters.AddWithValue(string.Format(@"HeadApprovedBy{0}", row.Index), ClassCurUser.LogInEmplId);
                                    sqlCommand.Parameters.AddWithValue(string.Format(@"HeadApprovedName{0}", row.Index), ClassCurUser.LogInEmplName);
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
                        Getdata();
                    }
                }
            }
        }

        private void GridData_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.Column.Name == "OutOfficeId")
                {
                        GridViewRowInfo row = (GridViewRowInfo)GridViewShowData.Rows[e.RowIndex];
                        string DocId = row.Cells["DocId"].Value.ToString();
                        string OutId = row.Cells["OutOfficeId"].Value.ToString();

                        FormDetailHeadApproved Frm = new FormDetailHeadApproved(DocId, OutId);
                        Frm.Show();
                }
            }
        }

        

    }

}
