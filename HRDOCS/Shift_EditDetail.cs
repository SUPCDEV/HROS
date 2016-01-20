using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Telerik.WinControls.UI;
using SysApp;


namespace HRDOCS
{
    public partial class Shift_EditDetail : Form
    {
        string DSDOCNO = "";
        string PWEMPLID = "M1104008";
        string PWEMPLNAME = "ชาคร มงคลสถิตย์พร";
        string DEPTID = "109";
        string DEPTNAME = "โปรแกรมเมอร์";
        string SECTIONID = "32";
        string SECTIONNAME = "แผนกคอมพิวเตอร์ (มังกร)";
        string CreatedBy = "M1104008";
        DateTime ExpireDate = DateTime.Now.Date;

        public Shift_EditDetail(string pDSDocNo)
        {
            DSDOCNO = pDSDocNo;
            InitializeComponent();
            InitializeForm();
            InitializeButton();
            InitializeDataGridview();
            InitializeDateTimePicker();
            InitializeDropDownList();
        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Shift_EditDetail_Load);
        }

        void Shift_EditDetail_Load(object sender, EventArgs e)
        {

            Load_DataDS();
        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_Add.Click += new EventHandler(Btn_Add_Click);
            Btn_Save.Click += new EventHandler(Btn_Save_Click);
            Btn_Cancel.Click += new EventHandler(Btn_Cancel_Click);
            Btn_SearchShift1.Click += new EventHandler(Btn_SearchShift1_Click);
            Btn_SearchShift2.Click += new EventHandler(Btn_SearchShift2_Click);
            Btn_Delete.Click += new EventHandler(Btn_Delete_Click);
        }

        void Btn_Delete_Click(object sender, EventArgs e)
        {
            var row_select = this.Dg_Shift.SelectedRows;

            foreach (DataGridViewRow row in row_select)
            {
                Dg_Shift.Rows.Remove(row);
            }
            Dg_Shift.Refresh();
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

        void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการยกเลิกเอกสารเลขที่ " + Txt_DSDocno.Text + " ของ " + Txt_Emplname.Text + " หรือไม่ ?", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            DataTable dataTable = new DataTable();
            SqlTransaction INSTrans = null;
            SqlConnection sqlConnectionINS = new SqlConnection(DatabaseConfig.ServerConStr);

            INSTrans = null;
            sqlConnectionINS.Open();
            INSTrans = sqlConnectionINS.BeginTransaction();

            #region UpdateModified

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = sqlConnectionINS;
                sqlCommand.CommandText = @"update SPC_CM_SHIFTHD set DOCSTAT = 0,
                                                    MODIFIEDBY = @MODIFIEDBY ,MODIFIEDDATE = @MODIFIEDDATE 
                                                    where SPC_CM_SHIFTHD.DSDOCNO = @DSDOCNO ";
                sqlCommand.Transaction = INSTrans;

                sqlCommand.Parameters.AddWithValue("@DSDOCNO", DSDOCNO);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDDATE", MyTime.GetDateTime());
                sqlCommand.ExecuteNonQuery();

            }

            #endregion

            #region Submit

            INSTrans.Commit();
            sqlConnectionINS.Close();

            #endregion

            this.DialogResult = DialogResult.Yes;
        }

        void Btn_Save_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();
            SqlTransaction INSTrans = null;
            SqlConnection sqlConnectionINS = new SqlConnection(DatabaseConfig.ServerConStr);

            if (MessageBox.Show("ต้องการบันทึกเอกสารนี้ ?", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            Process_ExpireDate();

            INSTrans = null;
            sqlConnection.Open();
            sqlConnectionINS.Open();
            INSTrans = sqlConnectionINS.BeginTransaction();


            #region UpdateModified

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = sqlConnectionINS;
                sqlCommand.CommandText = @"update SPC_CM_SHIFTHD set MODIFIEDBY = @MODIFIEDBY ,MODIFIEDDATE = @MODIFIEDDATE 
                                                    ,RESTDAY1 = @RESTDAY1 ,RESTDAY2 = @RESTDAY2 ,EXPIREDATE = @EXPIREDATE
                                                    where SPC_CM_SHIFTHD.DSDOCNO = @DSDOCNO ";
                sqlCommand.Transaction = INSTrans;

                sqlCommand.Parameters.AddWithValue("@DSDOCNO", DSDOCNO);
                sqlCommand.Parameters.AddWithValue("@RESTDAY1", Ddl_Rest1.SelectedIndex);
                sqlCommand.Parameters.AddWithValue("@RESTDAY2", Ddl_Rest2.SelectedIndex);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDDATE", MyTime.GetDateTime());
                sqlCommand.Parameters.AddWithValue("@EXPIREDATE", ExpireDate);
                sqlCommand.ExecuteNonQuery();

            }

            #endregion

            #region DeleteDT

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = sqlConnectionINS;
                sqlCommand.CommandText = @"delete from SPC_CM_SHIFTDT where SPC_CM_SHIFTDT.DSDOCNO = @DSDOCNO ";
                sqlCommand.Transaction = INSTrans;

                sqlCommand.Parameters.AddWithValue("@DSDOCNO", DSDOCNO);
                sqlCommand.ExecuteNonQuery();

            }

            #endregion

            #region InsertDT

            foreach (DataGridViewRow row in Dg_Shift.Rows)
            {
                if (row.IsNewRow == false)
                {
                    try
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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }

            #endregion

            #region Submit

            INSTrans.Commit();

            sqlConnection.Close();
            sqlConnectionINS.Close();

            #endregion

            this.DialogResult = DialogResult.Yes;
        }

        void Btn_Add_Click(object sender, EventArgs e)
        {
            if (Txt_Emplid.Text == "" || Txt_Emplname.Text == "" || Txt_Remark.Text == "" || (Txt_FromShift.Text == "" || Txt_ToShift.Text == ""))
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
                Ddl_Rest1.Enabled = false;

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

        #endregion

        #region InitializeDataGridview

        private void InitializeDataGridview()
        {

            #region SetDataGrid

            Dg_Shift.AutoGenerateColumns = false;
            Dg_Shift.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_Shift.Dock = DockStyle.Fill;


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

            Dg_Shift.KeyDown += new KeyEventHandler(Dg_Shift_KeyDown);

            #endregion


        }

        void Dg_Shift_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyData == Keys.Delete)
            //    {

            //        Dg_Shift.Rows.RemoveAt(Dg_Shift.CurrentRow.Index);

            //    }
            //}
            //catch (Exception ex)
            //{

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

        }

        #endregion

        #region Function

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
                    sqlCommand.CommandText = string.Format(@"select convert(varchar,SHIFTDATE,103) as SHIFTDATE2,* from SPC_CM_SHIFTHD
                                                            left join SPC_CM_SHIFTDT on SPC_CM_SHIFTHD.DSDOCNO = SPC_CM_SHIFTDT.DSDOCNO
                                                            where SPC_CM_SHIFTHD.DSDOCNO = '{0}' ", DSDOCNO);

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

                    Txt_DSDocno.Text = DSDOCNO;
                    Txt_Emplid.Text = dataTable.Rows[0]["EMPLID"].ToString();
                    Txt_Emplname.Text = dataTable.Rows[0]["EMPLNAME"].ToString();
                    Txt_SectionName.Text = dataTable.Rows[0]["SECTIONNAME"].ToString();
                    Txt_DeptName.Text = dataTable.Rows[0]["DEPTNAME"].ToString();
                    Ddl_Rest1.SelectedIndex = Convert.ToInt16(dataTable.Rows[0]["RESTDAY1"].ToString());
                    Ddl_Rest2.SelectedIndex = Convert.ToInt16(dataTable.Rows[0]["RESTDAY2"].ToString());


                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        Dg_Shift.Invoke(new EventHandler(delegate
                        {
                            Dg_Shift.Rows.Add();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["SHIFTDATE"].Value =
                                Convert.ToDateTime(dataTable.Rows[i]["SHIFTDATE"].ToString()).Date.ToString("dd/MM/yyyy");
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["FROMSHIFTID"].Value =
                                dataTable.Rows[i]["FROMSHIFTID"].ToString();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["FROMSHIFTDESC"].Value =
                                dataTable.Rows[i]["FROMSHIFTDESC"].ToString();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["TOSHIFTID"].Value =
                                dataTable.Rows[i]["TOSHIFTID"].ToString();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["TOSHIFTDESC"].Value =
                                dataTable.Rows[i]["TOSHIFTDESC"].ToString();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["REFOTTIME"].Value =
                                dataTable.Rows[i]["REFOTTIME"].ToString();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["REMARK"].Value =
                                dataTable.Rows[i]["REMARK"].ToString();
                        }));
                    }

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

        void Clear()
        {

            Txt_DSDocno.Text = "";
            Txt_Emplid.Text = "";
            Txt_Emplid.Enabled = true;
            Txt_Emplname.Text = "";
            Txt_DeptName.Text = "";
            Txt_SectionName.Text = "";
            Ddl_Rest1.Text = "";
            Ddl_Rest1.Enabled = true;
            Ddl_Rest2.Text = "";
            Txt_FromShift.Text = "";
            Txt_FromShiftDesc.Text = "";
            Txt_ToShift.Text = "";
            Txt_ToShiftDesc.Text = "";
            Txt_Remark.Text = "";
            Dtp_Shift.Value = DateTime.Now.Date;
            Dtp_Shift2.Value = DateTime.Now.Date;


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
