using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using SysApp;
using System.Data.SqlClient;
using System.IO;

namespace HRDOCS
{

    public partial class Shift_Create : Form
    {
        string DSDOCNO = "";
        string PWEMPLID = "";
        string PWEMPLNAME = "";
        string DEPTID = "";
        string DEPTNAME = "";
        string SECTIONID = "";
        string SECTIONNAME = "";
        string CreatedBy = "";
        DateTime ExpireDate = DateTime.Now.Date;

        //<Neung>
        string[] secureKey = new string[] { };

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

        public Shift_Create()
        {
            InitializeComponent();
            InitializeForm();
            InitializeDataGrid();
            InitializeButton();
            InitializeTextbox();
            InitializeDateTimePicker();
            InitializeDropDownList();
        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Shift_Create_Load);
            this.Shown += new EventHandler(Shift_Create_Shown);
        }

        void Shift_Create_Load(object sender, EventArgs e)
        {
            //SysApp.MiscFunctions.initializeGridViewCell(ref this.dataGridView1, "ชื่อเทเบิ้ล");
            //SysApp.MiscFunctions.initializeRadgridCell(ref this.radgridLines, @"SPC_TAFFDT");
            //radgridline.Columns[""].VisibleInColumnChooser = true;

            //Txt_DSDocno.Text = ClassRunDoc.runDocno("SPC_CM_SHIFTHD", "DSDOCNO", "DS");
            //MessageBox.Show("");
            Check_Authen();


        }

        void Shift_Create_Shown(object sender, EventArgs e)
        {
            //try
            //{
            //    this.Refresh_DS();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(string.Format(@"{0}", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        #endregion

        #region InitializeDateTimePicker

        private void InitializeDateTimePicker()
        {
            Dtp_Shift.Text = DateTime.Now.Date.ToString();
            Dtp_Shift.MinDate = DateTime.Now.AddDays(Convert.ToDouble(ClassCurUser.BEFOREDATECREATE)).Date;
            Dtp_Shift.MaxDate = DateTime.Now.AddDays(Convert.ToDouble(ClassCurUser.AFTERDATECREATE)).Date;
            Dtp_Shift2.Text = DateTime.Now.Date.ToString();
            Dtp_Shift2.MinDate = DateTime.Now.AddDays(Convert.ToDouble(ClassCurUser.BEFOREDATECREATE)).Date;
            Dtp_Shift2.MaxDate = DateTime.Now.AddDays(Convert.ToDouble(ClassCurUser.AFTERDATECREATE)).Date;
        }

        #endregion

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {

            #region SetRadGridLine

            this.radgridline.Dock = DockStyle.Fill;
            this.radgridline.AutoGenerateColumns = false;
            this.radgridline.EnableAlternatingRowColor = true;
            this.radgridline.ShowGroupPanel = false;
            this.radgridline.ShowGroupedColumns = true;
            this.radgridline.EnableFiltering = true;
            this.radgridline.AllowAddNewRow = false;
            this.radgridline.AllowEditRow = false;
            this.radgridline.ReadOnly = false;
            this.radgridline.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.AllCells);
            this.radgridline.MasterTemplate.ShowHeaderCellButtons = true;
            this.radgridline.MasterTemplate.ShowFilteringRow = false;
            this.radgridline.MasterTemplate.AutoGenerateColumns = false;
            this.radgridline.MasterTemplate.AllowEditRow = true;
            this.radgridline.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            this.radgridline.ShowCellErrors = true;
            this.radgridline.MasterTemplate.MultiSelect = true;



            #endregion

            #region SetDataGrid

            Dg_Shift.AutoGenerateColumns = false;
            Dg_Shift.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_Shift.Dock = DockStyle.Fill;
            Dg_Shift.Font = new Font("Segoe UI", 10);

            DataGridViewTextBoxColumn SHIFTDATE = new DataGridViewTextBoxColumn();
            SHIFTDATE.Name = "SHIFTDATE";
            SHIFTDATE.DataPropertyName = "SHIFTDATE";
            SHIFTDATE.HeaderText = "วันที่เปลี่ยนกะ";
            SHIFTDATE.ToolTipText = "วันที่เปลียนกะ";
            SHIFTDATE.Width = 100;
            SHIFTDATE.ReadOnly = true;
            Dg_Shift.Columns.Add(SHIFTDATE);

            DataGridViewTextBoxColumn FROMSHIFTID = new DataGridViewTextBoxColumn();
            FROMSHIFTID.Name = "FROMSHIFTID";
            FROMSHIFTID.DataPropertyName = "FROMSHIFTID";
            FROMSHIFTID.HeaderText = "กะเดิม";
            FROMSHIFTID.ToolTipText = "กะเดิม";
            FROMSHIFTID.Width = 100;
            FROMSHIFTID.ReadOnly = true;
            Dg_Shift.Columns.Add(FROMSHIFTID);

            DataGridViewTextBoxColumn FROMSHIFTDESC = new DataGridViewTextBoxColumn();
            FROMSHIFTDESC.Name = "FROMSHIFTDESC";
            FROMSHIFTDESC.DataPropertyName = "FROMSHIFTDESC";
            FROMSHIFTDESC.HeaderText = "คำอธิบาย";
            FROMSHIFTDESC.ToolTipText = "คำอธิบาย";
            FROMSHIFTDESC.Width = 100;
            FROMSHIFTDESC.ReadOnly = true;
            Dg_Shift.Columns.Add(FROMSHIFTDESC);

            DataGridViewTextBoxColumn TOSHIFTID = new DataGridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.DataPropertyName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "กะที่เปลี่ยน";
            TOSHIFTID.ToolTipText = "กะที่เปลี่ยน";
            TOSHIFTID.Width = 100;
            TOSHIFTID.ReadOnly = true;
            Dg_Shift.Columns.Add(TOSHIFTID);

            DataGridViewTextBoxColumn TOSHIFTDESC = new DataGridViewTextBoxColumn();
            TOSHIFTDESC.Name = "TOSHIFTDESC";
            TOSHIFTDESC.DataPropertyName = "TOSHIFTDESC";
            TOSHIFTDESC.HeaderText = "คำอธิบาย";
            TOSHIFTDESC.ToolTipText = "คำอธิบาย";
            TOSHIFTDESC.Width = 100;
            TOSHIFTDESC.ReadOnly = true;
            Dg_Shift.Columns.Add(TOSHIFTDESC);

            DataGridViewTextBoxColumn REFOTTIME = new DataGridViewTextBoxColumn();
            REFOTTIME.Name = "REFOTTIME";
            REFOTTIME.DataPropertyName = "REFOTTIME";
            REFOTTIME.HeaderText = "ช่วงเวลาทำโอที";
            REFOTTIME.ToolTipText = "ช่วงเวลาทำโอที";
            REFOTTIME.Width = 100;
            REFOTTIME.ReadOnly = true;
            Dg_Shift.Columns.Add(REFOTTIME);

            DataGridViewTextBoxColumn REMARK = new DataGridViewTextBoxColumn();
            REMARK.Name = "REMARK";
            REMARK.DataPropertyName = "REMARK";
            REMARK.HeaderText = "หมายเหตุ";
            REMARK.ToolTipText = "หมายเหตุ";
            REMARK.Width = 100;
            REMARK.ReadOnly = true;
            Dg_Shift.Columns.Add(REMARK);

            #endregion

            #region SetEvent


            #endregion

        }

        #endregion

        #region InitializeTextbox

        private void InitializeTextbox()
        {
            Txt_Emplid.KeyDown += new KeyEventHandler(Txt_Emplid_KeyDown);
        }

        void Txt_Emplid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Check_EMP();
            }
        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_SearchShift1.Click += new EventHandler(Btn_SearchShift1_Click);
            Btn_SearchShift2.Click += new EventHandler(Btn_SearchShift2_Click);
            Btn_Add.Click += new EventHandler(Btn_Add_Click);
            Btn_Save.Click += new EventHandler(Btn_Save_Click);
            Btn_New.Click += new EventHandler(Btn_New_Click);
            Btn_Delete.Click += new EventHandler(Btn_Delete_Click);
        }

        void Btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                var row_select = this.Dg_Shift.SelectedRows;

                foreach (DataGridViewRow row in row_select)
                {
                    Dg_Shift.Rows.Remove(row);
                }
                Dg_Shift.Refresh();
            }
            catch (Exception ex)
            {

            }
        }

        void Btn_New_Click(object sender, EventArgs e)
        {
            Clear();
            Check_Authen();
        }

        //        void Btn_Cancel_Click(object sender, EventArgs e)
        //        {

        //            SqlTransaction INSTrans = null;
        //            SqlConnection sqlConnectionINS = new SqlConnection(DatabaseConfig.ServerConStr);


        //            INSTrans = null;
        //            sqlConnectionINS.Open();
        //            INSTrans = sqlConnectionINS.BeginTransaction();


        //            #region UpdateModified

        //            using (SqlCommand sqlCommand = new SqlCommand())
        //            {
        //                sqlCommand.Connection = sqlConnectionINS;
        //                sqlCommand.CommandText = @"update SPC_CM_SHIFTHD set DOCSTAT = 0 ,MODIFIEDDATE = @MODIFIEDDATE 
        //                                                    ,MODIFIEDBY = @MODIFIEDBY
        //                                                    where SPC_CM_SHIFTHD.DSDOCNO = @DSDOCNO ";
        //                sqlCommand.Transaction = INSTrans;

        //                sqlCommand.Parameters.AddWithValue("@DSDOCNO", Txt_DSDocno.Text);
        //                sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", "M1104008");
        //                sqlCommand.Parameters.AddWithValue("@MODIFIEDDATE", MyTime.GetDateTime());
        //                sqlCommand.ExecuteNonQuery();

        //            }

        //            #endregion

        //            #region Submit

        //            INSTrans.Commit();
        //            sqlConnectionINS.Close();

        //            #endregion

        //            MessageBox.Show("ยกเลิกเอกสารเรียบร้อย...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            Clear();

        //        }

        void Btn_Save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการบันทึกเอกสารนี้หรือไม่ ?", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (Dg_Shift.Rows.Count > 0)
            {
                Process_ExpireDate();

                SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
                DataTable dataTable = new DataTable();
                SqlTransaction INSTrans = null;
                SqlConnection sqlConnectionINS = new SqlConnection(DatabaseConfig.ServerConStr);


                INSTrans = null;
                sqlConnection.Open();
                sqlConnectionINS.Open();
                INSTrans = sqlConnectionINS.BeginTransaction();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"SELECT	top 1 *
                                                            FROM    SPC_CM_SHIFTHD 
                                                            WHERE SPC_CM_SHIFTHD.DSDOCNO = '{0}' ", Txt_DSDocno.Text);

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
                    if (dataTable.Rows[0]["HEADAPPROVED"].ToString() != "0" || dataTable.Rows[0]["HEADAPPROVED"].ToString() != "0")
                    {
                        MessageBox.Show("เอกสารใบนี้ไม่สามารถแก้ไขข้อมูลได้", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    DSDOCNO = ClassRunDoc.runDocno("SPC_CM_SHIFTHD", "DSDOCNO", "DS");

                    #region InsertHD

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnectionINS;
                        sqlCommand.CommandText = @"insert into SPC_CM_SHIFTHD(DSDOCNO,DOCDATE,EMPLID,EMPLNAME,SECTIONID,SECTIONNAME,DEPTID,DEPTNAME,RESTDAY1,RESTDAY2,CREATEDBY,CREATEDDATE,MODIFIEDBY,MODIFIEDDATE,EXPIREDATE)
                                                           values(@DSDOCNO,@DOCDATE,@EMPLID,@EMPLNAME,@SECTIONID,@SECTIONNAME,@DEPTID,@DEPTNAME,@RESTDAY1,@RESTDAY2,@CREATEDBY,@CREATEDDATE,@MODIFIEDBY,@MODIFIEDDATE,@EXPIREDATE)";
                        sqlCommand.Transaction = INSTrans;

                        sqlCommand.Parameters.AddWithValue("@DSDOCNO", DSDOCNO);
                        sqlCommand.Parameters.AddWithValue("@DOCDATE", MyTime.GetDate());
                        sqlCommand.Parameters.AddWithValue("@EMPLID", PWEMPLID);
                        sqlCommand.Parameters.AddWithValue("@EMPLNAME", PWEMPLNAME);
                        sqlCommand.Parameters.AddWithValue("@SECTIONID", SECTIONID);
                        sqlCommand.Parameters.AddWithValue("@SECTIONNAME", SECTIONNAME);
                        sqlCommand.Parameters.AddWithValue("@DEPTID", DEPTID);
                        sqlCommand.Parameters.AddWithValue("@DEPTNAME", DEPTNAME);
                        sqlCommand.Parameters.AddWithValue("@RESTDAY1", Ddl_Rest1.SelectedIndex);
                        sqlCommand.Parameters.AddWithValue("@RESTDAY2", Ddl_Rest2.SelectedIndex);
                        sqlCommand.Parameters.AddWithValue("@CREATEDBY", ClassCurUser.LogInEmplId);
                        sqlCommand.Parameters.AddWithValue("@CREATEDDATE", MyTime.GetDateTime());
                        sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                        sqlCommand.Parameters.AddWithValue("@MODIFIEDDATE", MyTime.GetDateTime());
                        sqlCommand.Parameters.AddWithValue("@EXPIREDATE", ExpireDate);

                        sqlCommand.ExecuteNonQuery();

                    }
                    #endregion

                    #region InsertDT

                    foreach (DataGridViewRow row in Dg_Shift.Rows)
                    {
                        if (row.IsNewRow == false)
                        {

                            using (SqlCommand sqlCommand = new SqlCommand())
                            {
                                sqlCommand.Connection = sqlConnectionINS;
                                sqlCommand.CommandText = @"insert into SPC_CM_SHIFTDT(DSDOCNO,SHIFTDATE,FROMSHIFTID,FROMSHIFTDESC,TOSHIFTID,TOSHIFTDESC,REFOTTIME,REMARK)
                                                           values(@DSDOCNO,@SHIFTDATE,@FROMSHIFTID,@FROMSHIFTDESC,@TOSHIFTID,@TOSHIFTDESC,@REFOTTIME,@REMARK)";
                                sqlCommand.Transaction = INSTrans;

                                sqlCommand.Parameters.AddWithValue("@DSDOCNO", DSDOCNO);
                                sqlCommand.Parameters.AddWithValue("@SHIFTDATE", Convert.ToDateTime(row.Cells["SHIFTDATE"].Value.ToString()));
                                sqlCommand.Parameters.AddWithValue("@FROMSHIFTID", row.Cells["FROMSHIFTID"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue("@FROMSHIFTDESC", row.Cells["FROMSHIFTDESC"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue("@TOSHIFTID", row.Cells["TOSHIFTID"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue("@TOSHIFTDESC", row.Cells["TOSHIFTDESC"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue("@REFOTTIME", row.Cells["REFOTTIME"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue("@REMARK", row.Cells["REMARK"].Value.ToString());
                                sqlCommand.ExecuteNonQuery();

                            }

                        }
                    }

                    #endregion

                    #region Submit

                    INSTrans.Commit();

                    sqlConnection.Close();
                    sqlConnectionINS.Close();

                    #endregion


                }
                MessageBox.Show("บันทึกข้อมูลเสร็จสมบูรณ์ เลขที่เอกสาร " + DSDOCNO, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Clear();
                Check_Authen();
            }
            else
            {
                MessageBox.Show("ไม่มีข้อมูล กรุณาเพิ่มข้อมูลการเปลี่ยนกะ...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void Btn_Add_Click(object sender, EventArgs e)
        {
            if (PWEMPLID == "" || PWEMPLNAME == "" || Txt_Remark.Text == "" || (Txt_FromShift.Text == "" || Txt_ToShift.Text == "" || Ddl_Rest1.Text == ""))
            {
                MessageBox.Show("กรุณาใส่ข้อมูลให้ครบ...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Dtp_Shift.Value.Date > Dtp_Shift2.Value.Date)
            {
                MessageBox.Show("กรุณาเลือกช่วงวันให้ถูกต้อง...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i <= Dtp_Shift2.Value.Date.Subtract(Dtp_Shift.Value.Date).TotalDays; i++)
            {

                if (Dg_Shift.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in Dg_Shift.Rows)
                    {
                        if (Dtp_Shift.Value.Date.AddDays(i).ToString("dd/MM/yyyy") == row.Cells["SHIFTDATE"].Value.ToString())
                        {
                            MessageBox.Show("มีข้อมูลวันที่ " + Dtp_Shift.Value.Date.AddDays(i).ToString("dd/MM/yyyy") + " นี้อยู่แล้ว", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                Txt_Emplid.Enabled = false;
                //Ddl_Rest1.Enabled = false;

                Dg_Shift.Invoke(new EventHandler(delegate
                {
                    Dg_Shift.Rows.Add();
                    Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["SHIFTDATE"].Value =
                         Dtp_Shift.Value.Date.AddDays(i).ToString("dd/MM/yyyy");
                    Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["FROMSHIFTID"].Value =
                        Txt_FromShift.Text;
                    Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["FROMSHIFTDESC"].Value =
                        Txt_FromShiftDesc.Text;
                    Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["TOSHIFTID"].Value =
                        Txt_ToShift.Text;
                    Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["TOSHIFTDESC"].Value =
                        Txt_ToShiftDesc.Text;
                    Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["REFOTTIME"].Value =
                        Txt_OTTime.Text;
                    Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["REMARK"].Value =
                        Txt_Remark.Text;
                }));
            }
        }

        void Btn_SearchShift2_Click(object sender, EventArgs e)
        {
            using (Select_Shift frm = new Select_Shift())
            {
                frm.Text = "ค้นหากะ";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {
                    Txt_ToShift.Text = frm.Shift;
                    Txt_ToShiftDesc.Text = frm.ShiftDesc;
                }
            }
        }

        void Btn_SearchShift1_Click(object sender, EventArgs e)
        {
            using (Select_Shift frm = new Select_Shift())
            {
                frm.Text = "ค้นหากะ";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {
                    Txt_FromShift.Text = frm.Shift;
                    Txt_FromShiftDesc.Text = frm.ShiftDesc;
                }
            }
        }

        #endregion

        #region InitializeDropDownList

        private void InitializeDropDownList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RestIndex", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("RestName", typeof(System.String)));
            dt.Rows.Add(0, "อาทิตย์");
            dt.Rows.Add(1, "จันทร์");
            dt.Rows.Add(2, "อังคาร");
            dt.Rows.Add(3, "พุธ");
            dt.Rows.Add(4, "พฤหัสบดี");
            dt.Rows.Add(5, "ศุกร์");
            dt.Rows.Add(6, "เสาร์");

            Ddl_Rest1.DataSource = dt;
            Ddl_Rest1.DisplayMember = "RestName";
            Ddl_Rest1.ValueMember = "RestIndex";

            DataTable dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("RestIndex", typeof(System.Int32)));
            dt2.Columns.Add(new DataColumn("RestName", typeof(System.String)));
            dt2.Rows.Add(0, "อาทิตย์");
            dt2.Rows.Add(1, "จันทร์");
            dt2.Rows.Add(2, "อังคาร");
            dt2.Rows.Add(3, "พุธ");
            dt2.Rows.Add(4, "พฤหัสบดี");
            dt2.Rows.Add(5, "ศุกร์");
            dt2.Rows.Add(6, "เสาร์");

            Ddl_Rest2.DataSource = dt2;
            Ddl_Rest2.DisplayMember = "RestName";
            Ddl_Rest2.ValueMember = "RestIndex";

            //Ddl_Rest1.SelectedIndex = 0;

        }

        #endregion

        #region Function

        private void Refresh_DS()
        {
            DataTable datatable = null;
            try
            {
                using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(@"SELECT    TOP 100 *   FROM    SPC_TAFFDT WITH (NOLOCK) ORDER BY CONVERTDATE DESC", SysApp.DatabaseConfig.ServerConStr))
                {
                    datatable = new DataTable();
                    if (da.Fill(datatable) < 0)
                        this.radgridline.DataSource = new object[] { };
                    else
                        this.radgridline.DataSource = datatable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        void Load_DataDS()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_Shift.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"");

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


                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        void Check_EMP()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_Shift.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"SELECT	RTRIM(PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWCARD) AS PWCARD, RTRIM(PWFNAME) AS PWFNAME , RTRIM(PWLNAME) AS PWLNAME
		                                    ,RTRIM(PWSECTION.PWSECTION)AS SECTIONID  , RTRIM(PWSECTION.PWDESC) AS SECTION,RTRIM(PWDEPT.PWDEPT)AS DEPTID , RTRIM(PWDEPT.PWDESC) AS DEPT
                                     FROM	PWEMPLOYEE WITH (NOLOCK)
	                                    LEFT OUTER JOIN PWSECTION WITH (NOLOCK) ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS
	                                    LEFT OUTER JOIN PWDEPT WITH (NOLOCK) ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT collate Thai_CS_AS
                                     WHERE	(PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
		                                    AND PWSTATWORK LIKE '[AV]' 
                                            AND PWEMPLOYEE.PWSECTION IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                            where EMPLID = '{1}')", Txt_Emplid.Text, ClassCurUser.LogInEmplId);

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
                    Txt_Emplid.Text = dataTable.Rows[0]["PWEMPLOYEE"].ToString();
                    PWEMPLID = dataTable.Rows[0]["PWEMPLOYEE"].ToString();
                    PWEMPLNAME = dataTable.Rows[0]["PWFNAME"].ToString() + "  " + dataTable.Rows[0]["PWLNAME"].ToString();
                    SECTIONID = dataTable.Rows[0]["SECTIONID"].ToString();
                    SECTIONNAME = dataTable.Rows[0]["SECTION"].ToString();
                    DEPTID = dataTable.Rows[0]["DEPTID"].ToString();
                    DEPTNAME = dataTable.Rows[0]["DEPT"].ToString();
                    Txt_Emplname.Text = dataTable.Rows[0]["PWFNAME"].ToString() + "  " + dataTable.Rows[0]["PWLNAME"].ToString();
                    Txt_SectionName.Text = dataTable.Rows[0]["SECTION"].ToString();
                    Txt_DeptName.Text = dataTable.Rows[0]["DEPT"].ToString();
                    Txt_Emplid.Text.ToUpper();
                    Ddl_Rest1.Focus();

                }
                else
                {

                    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Clear();
                    Check_Authen();
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

        void Clear()
        {

            Txt_DSDocno.Text = "";
            Txt_Emplid.Text = "";
            Txt_Emplid.Enabled = true;
            Txt_Emplname.Text = "";
            Txt_DeptName.Text = "";
            Txt_SectionName.Text = "";
            Ddl_Rest1.SelectedIndex = 0;
            Ddl_Rest1.Enabled = true;
            Ddl_Rest2.SelectedIndex = 0;
            Txt_FromShift.Text = "";
            Txt_FromShiftDesc.Text = "";
            Txt_ToShift.Text = "";
            Txt_ToShiftDesc.Text = "";
            Txt_Remark.Text = "";
            Dtp_Shift.Value = DateTime.Now.Date;
            Dg_Shift.Rows.Clear();
            Txt_OTTime.Text = "";

        }

        void Check_Authen()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_Shift.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"select PWSECTION from SPC_CM_AUTHORIZE
                                                                where EMPLID = '{0}'  AND APPROVEID in ('001','002','005','006')", ClassCurUser.LogInEmplId);

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

                }
                else
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = string.Format(@"SELECT	RTRIM(PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWCARD) AS PWCARD, RTRIM(PWFNAME) AS PWFNAME , RTRIM(PWLNAME) AS PWLNAME
		                                    ,RTRIM(PWSECTION.PWSECTION)AS SECTIONID  , RTRIM(PWSECTION.PWDESC) AS SECTION,RTRIM(PWDEPT.PWDEPT)AS DEPTID , RTRIM(PWDEPT.PWDESC) AS DEPT
                                     FROM	PWEMPLOYEE WITH (NOLOCK)
	                                    LEFT OUTER JOIN PWSECTION WITH (NOLOCK) ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS
	                                    LEFT OUTER JOIN PWDEPT WITH (NOLOCK) ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT collate Thai_CS_AS
                                     WHERE	PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}'
		                                    AND PWSTATWORK LIKE '[AV]'  ", ClassCurUser.LogInEmplId);

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
                        Txt_Emplid.Text = dataTable.Rows[0]["PWEMPLOYEE"].ToString();
                        PWEMPLID = dataTable.Rows[0]["PWEMPLOYEE"].ToString();
                        PWEMPLNAME = dataTable.Rows[0]["PWFNAME"].ToString() + "  " + dataTable.Rows[0]["PWLNAME"].ToString();
                        SECTIONID = dataTable.Rows[0]["SECTIONID"].ToString();
                        SECTIONNAME = dataTable.Rows[0]["SECTION"].ToString();
                        DEPTID = dataTable.Rows[0]["DEPTID"].ToString();
                        DEPTNAME = dataTable.Rows[0]["DEPT"].ToString();
                        Txt_Emplname.Text = dataTable.Rows[0]["PWFNAME"].ToString() + "  " + dataTable.Rows[0]["PWLNAME"].ToString();
                        Txt_SectionName.Text = dataTable.Rows[0]["SECTION"].ToString();
                        Txt_DeptName.Text = dataTable.Rows[0]["DEPT"].ToString();
                        Txt_Emplid.Text.ToUpper();
                        Ddl_Rest1.Focus();
                        Txt_Emplid.IsReadOnly = true;
                        Txt_Emplname.IsReadOnly = true;
                        Txt_DeptName.IsReadOnly = true;
                        Txt_SectionName.IsReadOnly = true;
                    }

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

        void Process_ExpireDate()
        {
            ExpireDate = DateTime.Now.Date;
            foreach (DataGridViewRow row in Dg_Shift.Rows)
            {
                if ((DateTime.Compare(ExpireDate, Convert.ToDateTime(row.Cells["SHIFTDATE"].Value.ToString()))) < 0)
                {
                    ExpireDate = Convert.ToDateTime(row.Cells["SHIFTDATE"].Value.ToString());
                }
            }

            ExpireDate = ExpireDate.AddDays(Convert.ToDouble(ClassCurUser.LASTDATEAPPROVE_SHIFT));

        }

        #endregion
    }
}
