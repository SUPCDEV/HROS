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
    public partial class FormEmplEdit : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);

        //string sysuser = "";
        //string sectionid = "";

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

        private RadGridView radGridLines;

        public FormEmplEdit()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;

            // <layOut>
            this.panelRightSide.Dock = DockStyle.Right;
            this.radGroupBox1.Dock = DockStyle.Fill;
            this.panelTop.Dock = DockStyle.Top;
            this.panelBottom.Dock = DockStyle.Fill;
            this.radGridViewEditDoc.Dock = DockStyle.Fill;

            // <Status>
            this.statusLabelMessage.Text = string.Empty;
            this.stutusF5.Text = string.Format(@"{0}: {1}", "F5", "Refresh");

            // <Button>
            this.btnAdjust.Click += new EventHandler(btnAdjust_Click);

            // <RadGridView Bottom>

            #region radGridViewl
            // GridData
            this.radGridViewEditDoc.Dock = DockStyle.Fill;
            this.radGridViewEditDoc.AutoGenerateColumns = true;
            this.radGridViewEditDoc.EnableFiltering = false;
            this.radGridViewEditDoc.AllowAddNewRow = false;

            this.radGridViewEditDoc.MasterTemplate.AutoGenerateColumns = false;
            this.radGridViewEditDoc.ShowGroupedColumns = true;
            this.radGridViewEditDoc.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.radGridViewEditDoc.EnableHotTracking = true;
            this.radGridViewEditDoc.AutoSizeRows = true;
            this.radGridViewEditDoc.ReadOnly = true;

            this.radGridViewEditDoc.CurrentRowChanged += new CurrentRowChangedEventHandler(radGridViewEditDoc_CurrentRowChanged);

            #endregion

            #region Gridlines

            this.radGridLines = new RadGridView();
            // <WS>
            this.radGridLines.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radGridLines.ShowGroupPanel = false;
            this.radGridLines.ShowGroupedColumns = true;
            // </WS>
            this.radGridLines.Dock = DockStyle.Fill;
            this.radGridLines.AutoGenerateColumns = true;
            this.radGridLines.EnableFiltering = true;
            this.radGridLines.AllowAddNewRow = false;            
            this.radGridLines.ReadOnly = true;
            this.radGridLines.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.HeaderCells);
            this.radGridLines.MasterTemplate.ShowHeaderCellButtons = true;
            this.radGridLines.MasterTemplate.ShowFilteringRow = false;
            this.radGridLines.MasterTemplate.AutoGenerateColumns = false;
            this.radGridLines.ShowGroupedColumns = true;
            this.radGridLines.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            GridViewTextBoxColumn col0_0 = new GridViewTextBoxColumn();
            col0_0.Name = @"DocId";
            col0_0.HeaderText = @"เอกสาร";
            col0_0.FieldName = @"DocId";
            col0_0.ReadOnly = true;
            radGridLines.Columns.Add(col0_0);
            panelBottom.Controls.Add(radGridLines);


            GridViewTextBoxColumn col0_1 = new GridViewTextBoxColumn();
            col0_1.Name = @"OutOfficeId";
            col0_1.HeaderText = @"ลำดับ";
            col0_1.FieldName = @"OutOfficeId";
            col0_1.ReadOnly = true;
            radGridLines.Columns.Add(col0_1);
            panelBottom.Controls.Add(radGridLines);

            GridViewTextBoxColumn col0 = new GridViewTextBoxColumn();
            col0.Name = @"EmplId";
            col0.HeaderText = @"พนักงาน";
            col0.FieldName = @"EmplId";
            col0.ReadOnly = true;
            radGridLines.Columns.Add(col0);
            panelBottom.Controls.Add(radGridLines);

            GridViewTextBoxColumn col1 = new GridViewTextBoxColumn();
            col1.Name = @"EmplFullName";
            col1.HeaderText = @"ชื่อ-สกุล";
            col1.FieldName = @"EmplFullName";
            col1.ReadOnly = true;
            radGridLines.Columns.Add(col1);
            panelBottom.Controls.Add(radGridLines);

            GridViewTextBoxColumn col1_1 = new GridViewTextBoxColumn();
            col1_1.Name = @"Dept";
            col1_1.HeaderText = @"ชื่อตำแหน่ง";
            col1_1.FieldName = @"Dept";
            col1_1.ReadOnly = true;
            radGridLines.Columns.Add(col1_1);
            panelBottom.Controls.Add(radGridLines);

            GridViewTextBoxColumn col3 = new GridViewTextBoxColumn();
            col3.Name = @"DimentionId";
            col3.HeaderText = @"รหัสแผนก";
            col3.FieldName = @"DimentionId";
            col3.ReadOnly = true;
            col3.IsVisible = false;
            radGridLines.Columns.Add(col3);
            panelBottom.Controls.Add(radGridLines);

            GridViewTextBoxColumn col4 = new GridViewTextBoxColumn();
            col4.Name = @"Dimention";
            col4.HeaderText = @"แผนก";
            col4.FieldName = @"Dimention";
            col4.ReadOnly = true;
            radGridLines.Columns.Add(col4);



            GridViewTextBoxColumn col4_0 = new GridViewTextBoxColumn();
            col4_0.Name = @"OutType";
            col4_0.HeaderText = @"ประเภท";
            col4_0.FieldName = @"OutType";
            col4_0.ReadOnly = true;
            radGridLines.Columns.Add(col4_0);

            //OutType

            GridViewTextBoxColumn col4_1 = new GridViewTextBoxColumn();
            col4_1.Name = @"HeadApproved";
            col4_1.HeaderText = @"หน./ผช.";
            col4_1.FieldName = @"HeadApproved";
            col4_1.ReadOnly = true;
            radGridLines.Columns.Add(col4_1);

            GridViewTextBoxColumn col5 = new GridViewTextBoxColumn();
            col5.Name = @"HeadApprovedName";
            col5.HeaderText = @"อนุมัติโดย";
            col5.FieldName = @"HeadApprovedName";
            col5.ReadOnly = true;
            radGridLines.Columns.Add(col5);

            panelBottom.Controls.Add(radGridLines);

            #endregion
        }

        void btnAdjust_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.radGridViewEditDoc.CurrentRow == null)
                    throw new NullReferenceException("No select row item.");

                var curRow = radGridViewEditDoc.CurrentRow;
                if (curRow.Index < 0)
                    throw new Exception("No select row item.");

                using (FormEdit frmAdjust = new FormEdit(curRow.Cells["DocId"].Value.ToString()))
                {
                    frmAdjust.StartPosition = FormStartPosition.CenterScreen;
                    if (frmAdjust.ShowDialog(this) == DialogResult.OK)
                    {
                        Refresh_DS();
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
        }

        void radGridViewEditDoc_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            bool ret = true;
            if (ret && (radGridViewEditDoc.Rows.Count < 1))
            {
                ret = false;
                return;
            }

            if (ret && (e.CurrentRow.Index < 0))
            {
                ret = false;
                return;
            }

            this.RefreshLine_DS();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (radGridViewEditDoc.RowCount > 0)
            {
                if (keyData == Keys.F5)
                {
                    this.Refresh_DS(); // GetData();                
                }
                else if (keyData == Keys.Escape)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(@"ไม่มีข้อมูล", "การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Refresh_DS()
        {
            this.Getdata();
            this.RefreshLine_DS();
        }
        //แสดงเอกสารทั้งหมดของผู้ที่ Login มีส่วนเกี่ยวข้อง (Grid ด้านบน)
        public void Getdata()
        {
            string queryRun =
                   string.Format(@"SELECT  DocId,[CreatedBy],[CreatedName],[Dimention],[Dept]
	                                   ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                                   ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                                   ,ISNULL([HeadApprovedName],'-') AS [HeadApprovedName]
		                               ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
                                FROM   [IVZ_HROUTOFFICE] WITH (NOLOCK)
                                WHERE  [DocId] IN 
	                                (
	                                    SELECT	DISTINCT DocId
	                                    FROM	[IVZ_HROUTOFFICE] WITH (NOLOCK)
	                                    WHERE(
				                                [EmplId] = '{0}'
				                                OR [CreatedBy] = '{0}'
			                                 )
                                    )
                                AND (
			                                [EmplId] = '{0}' 
			                                AND [Status] = 1 
			                                AND [HeadApproved] = 1
	                                )
                                OR  (
			                                [CreatedBy] = '{0}' 
			                                AND [Status] = 1 
			                                AND [HeadApproved] = 1
	                                )
                                GROUP BY DocId,[CreatedBy],[CreatedName],[Dimention],[Dept],OutType,[HeadApproved],[HeadApprovedName],[HeadApprovedDateTime]
                                ORDER BY DocId ", ClassCurUser.LogInEmplId);


            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.CommandText = queryRun;

                //SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                //da.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    this.radGridViewEditDoc.DataSource = dt;
                //}
                //else
                //{
                //    btnAdjust.Enabled = false;                    
                //}

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        DataTable localTable = new DataTable("IVZ_OUROFFICE");
                        localTable.Load(reader);

                        this.radGridViewEditDoc.DataSource = localTable;
                    }
                    else
                    {
                        // <WS>
                        this.radGridViewEditDoc.DataSource = new string[] { };
                        // </WS>
                        btnAdjust.Enabled = false;
                        // this.radGridViewEditDoc.DataSource = new string[] { };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        //แสดงพนักงานที่อยู่ในเอกสารทั้งหมด (Grid ล่าง)
        private void RefreshLine_DS()
        {
            bool ret = true;

            try
            {
                if (ret && (radGridViewEditDoc.Rows.Count < 1))
                {
                    this.radGridLines.DataSource = new string[] { };
                    ret = false;
                    return;
                }

                var curRow = radGridViewEditDoc.CurrentRow;
                if (curRow.Index < 0)
                    throw new Exception("No select row item.");

                string queryRun =
                    string.Format(@"SELECT DocId,[OutOfficeId] ,[EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],[Dept]
	                            ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
	                            ,CASE [HeadApproved] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HeadApproved]
	                            ,ISNULL([HeadApprovedName],'-') AS [HeadApprovedName]
	                            ,ISNULL(CONVERT(VARCHAR, [HeadApprovedDateTime],120),'0000-00-00 00:00:00') AS [HeadApprovedDateTime]
                                FROM	[IVZ_HROUTOFFICE] WITH (NOLOCK)
                                WHERE	[DocId] = '{0}'
                                AND [Status] = '1'", curRow.Cells[0].Value.ToString());

                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (SqlCommand command = new SqlCommand()
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = @queryRun
                })
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable localTable = new DataTable("IVZ_OUROFFICE");
                            localTable.Load(reader);
                            //var ret = (from res in localTable.AsEnumerable()
                            //           select res).DefaultIfEmpty();
                            //this.gridLines.DataSource = ret.ToList();
                            this.radGridLines.DataSource = localTable;
                        }
                        else
                        {
                            this.radGridLines.DataSource = new string[] { };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void FormEmplEdit_Load(object sender, EventArgs e)
        {
            this.Getdata();
        }

        //private void radGridViewEditDoc_CellClick(object sender, GridViewCellEventArgs e)
        //{
        //if (e.RowIndex >= 0)
        //{
        //    if (e.Column.Name == "OutOfficeId")
        //    {
        //        GridViewRowInfo row = (GridViewRowInfo)radGridViewEditDoc.Rows[e.RowIndex];

        //        string DocId = row.Cells["DocId"].Value.ToString();
        //        string OutId = row.Cells["OutOfficeId"].Value.ToString();

        //        FormEdit Frm = new FormEdit(DocId,OutId);
        //        Frm.Show();
        //    }
        //}
        //return;
        //}
    }
}
