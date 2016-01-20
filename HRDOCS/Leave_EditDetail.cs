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
    public partial class Leave_EditDetail : Form
    {
        string DLDOCNO = "";
        string LEAVETYPE = "1";
        string LEAVETYPEDETAIL = "ลากิจ";
        string PWEMPLID = "";
        string PWEMPLNAME = "";
        string DEPTID = "";
        string DEPTNAME = "";
        string SECTIONID = "";
        string SECTIONNAME = "";
        string CreatedBy = "";
        DateTime ExpireDate = DateTime.Now.Date;

        public Leave_EditDetail(string pDLDocNo)
        {
            DLDOCNO = pDLDocNo;
            InitializeComponent();
            InitializeForm();
            InitializeButton();
            InitializeDataGrid();
            InitializeDateTimePicker();
            InitializeRadioButton();
            InitializeCheckBox();
            InitializeDropDownList();
            InitializeTextBox();
        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Leave_EditDetail_Load);
        }

        void Leave_EditDetail_Load(object sender, EventArgs e)
        {
            Load_DataDL();
        }

        #endregion

        #region InitializeTextBox

        private void InitializeTextBox()
        {
            Txt_Half_Time1.KeyPress += new KeyPressEventHandler(Txt_Half_Time1_KeyPress);
            Txt_Half_Time2.KeyPress += new KeyPressEventHandler(Txt_Half_Time2_KeyPress);
        }

        void Txt_Half_Time2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int cInt = Convert.ToInt32(e.KeyChar);

            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || cInt == 8 || (int)e.KeyChar == 46)
            {
                e.Handled = false;
            }

            else
            {
                e.Handled = true;
            }
        }

        void Txt_Half_Time1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int cInt = Convert.ToInt32(e.KeyChar);

            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || cInt == 8 || (int)e.KeyChar == 46)
            {
                e.Handled = false;
            }

            else
            {
                e.Handled = true;
            }
        }

        #endregion

        #region InitializeDateTimePicker

        private void InitializeDateTimePicker()
        {
            Dtp_Leave.Text = DateTime.Now.Date.ToString();
            Dtp_Leave.MinDate = DateTime.Now.Date.AddDays(Convert.ToDouble(ClassCurUser.BEFOREDATECREATE));
            Dtp_Leave.MaxDate = DateTime.Now.Date.AddDays(Convert.ToDouble(ClassCurUser.AFTERDATECREATE));
        }

        #endregion

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {
            #region SetDataGrid

            Dg_Leave.AutoGenerateColumns = false;
            Dg_Leave.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_Leave.Dock = DockStyle.Fill;
            Dg_Leave.Font = new Font("Segoe UI", 10);

            DataGridViewTextBoxColumn LEAVEDATE = new DataGridViewTextBoxColumn();
            LEAVEDATE.Name = "LEAVEDATE";
            LEAVEDATE.DataPropertyName = "LEAVEDATE";
            LEAVEDATE.HeaderText = "วันที่ลา";
            LEAVEDATE.ToolTipText = "วันที่ลา";
            LEAVEDATE.Width = 100;
            LEAVEDATE.ReadOnly = true;
            Dg_Leave.Columns.Add(LEAVEDATE);

            DataGridViewTextBoxColumn LEAVETYPE = new DataGridViewTextBoxColumn();
            LEAVETYPE.Name = "LEAVETYPE";
            LEAVETYPE.DataPropertyName = "LEAVETYPE";
            LEAVETYPE.HeaderText = "ประเภทการลา";
            LEAVETYPE.ToolTipText = "ประเภทการลา";
            LEAVETYPE.Width = 100;
            LEAVETYPE.ReadOnly = true;
            LEAVETYPE.Visible = false;
            Dg_Leave.Columns.Add(LEAVETYPE);

            DataGridViewTextBoxColumn LEAVETYPEDETAIL = new DataGridViewTextBoxColumn();
            LEAVETYPEDETAIL.Name = "LEAVETYPEDETAIL";
            LEAVETYPEDETAIL.DataPropertyName = "LEAVETYPEDETAIL";
            LEAVETYPEDETAIL.HeaderText = "ประเภทการลา";
            LEAVETYPEDETAIL.ToolTipText = "ประเภทการลา";
            LEAVETYPEDETAIL.Width = 100;
            LEAVETYPEDETAIL.ReadOnly = true;
            Dg_Leave.Columns.Add(LEAVETYPEDETAIL);

            DataGridViewTextBoxColumn ATTACH = new DataGridViewTextBoxColumn();
            ATTACH.Name = "ATTACH";
            ATTACH.DataPropertyName = "ATTACH";
            ATTACH.HeaderText = "แนบใบรับรองแพทย์";
            ATTACH.ToolTipText = "แนบใบรับรองแพทย์";
            ATTACH.Width = 100;
            ATTACH.ReadOnly = true;
            ATTACH.Visible = false;
            Dg_Leave.Columns.Add(ATTACH);

            DataGridViewTextBoxColumn ATTACHDOC = new DataGridViewTextBoxColumn();
            ATTACHDOC.Name = "ATTACHDOC";
            ATTACHDOC.DataPropertyName = "ATTACHDOC";
            ATTACHDOC.HeaderText = "แนบใบรับรองแพทย์";
            ATTACHDOC.ToolTipText = "แนบใบรับรองแพทย์";
            ATTACHDOC.Width = 100;
            ATTACHDOC.ReadOnly = true;
            ATTACHDOC.Visible = true;
            Dg_Leave.Columns.Add(ATTACHDOC);

            DataGridViewTextBoxColumn HALFDAYTYPE = new DataGridViewTextBoxColumn();
            HALFDAYTYPE.Name = "HALFDAYTYPE";
            HALFDAYTYPE.DataPropertyName = "HALFDAYTYPE";
            HALFDAYTYPE.HeaderText = "จำนวนวัน";
            HALFDAYTYPE.ToolTipText = "จำนวนวัน";
            HALFDAYTYPE.Width = 100;
            HALFDAYTYPE.ReadOnly = true;
            HALFDAYTYPE.Visible = false;
            Dg_Leave.Columns.Add(HALFDAYTYPE);

            DataGridViewTextBoxColumn HALFDAY = new DataGridViewTextBoxColumn();
            HALFDAY.Name = "HALFDAY";
            HALFDAY.DataPropertyName = "HALFDAY";
            HALFDAY.HeaderText = "จำนวนวัน";
            HALFDAY.ToolTipText = "จำนวนวัน";
            HALFDAY.Width = 100;
            HALFDAY.ReadOnly = true;
            Dg_Leave.Columns.Add(HALFDAY);

            DataGridViewTextBoxColumn HALFDAYTIME1 = new DataGridViewTextBoxColumn();
            HALFDAYTIME1.Name = "HALFDAYTIME1";
            HALFDAYTIME1.DataPropertyName = "HALFDAYTIME1";
            HALFDAYTIME1.HeaderText = "ตั้งแต่เวลา";
            HALFDAYTIME1.ToolTipText = "ตั้งแต่เวลา";
            HALFDAYTIME1.Width = 100;
            HALFDAYTIME1.ReadOnly = true;
            Dg_Leave.Columns.Add(HALFDAYTIME1);

            DataGridViewTextBoxColumn HALFDAYTIME2 = new DataGridViewTextBoxColumn();
            HALFDAYTIME2.Name = "HALFDAYTIME2";
            HALFDAYTIME2.DataPropertyName = "HALFDAYTIME2";
            HALFDAYTIME2.HeaderText = "ถึงเวลา";
            HALFDAYTIME2.ToolTipText = "ถึงเวลา";
            HALFDAYTIME2.Width = 100;
            HALFDAYTIME2.ReadOnly = true;
            Dg_Leave.Columns.Add(HALFDAYTIME2);

            DataGridViewTextBoxColumn LEAVEREMARK = new DataGridViewTextBoxColumn();
            LEAVEREMARK.Name = "LEAVEREMARK";
            LEAVEREMARK.DataPropertyName = "LEAVEREMARK";
            LEAVEREMARK.HeaderText = "หมายเหตุ";
            LEAVEREMARK.ToolTipText = "หมายเหตุ";
            LEAVEREMARK.Width = 100;
            LEAVEREMARK.ReadOnly = true;
            Dg_Leave.Columns.Add(LEAVEREMARK);

            #endregion
        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_Add.Click += new EventHandler(Btn_Add_Click);
            Btn_Delete.Click += new EventHandler(Btn_Delete_Click);
            Btn_Cancel.Click += new EventHandler(Btn_Cancel_Click);
            Btn_Save.Click += new EventHandler(Btn_Save_Click);
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
                sqlCommand.CommandText = @"update SPC_CM_LEAVEHD set MODIFIEDBY = @MODIFIEDBY ,MODIFIEDDATE = @MODIFIEDDATE 
                                                    ,RESTDAY1 = @RESTDAY1 ,RESTDAY2 = @RESTDAY2 ,EXPIREDATE = @EXPIREDATE
                                                    where SPC_CM_LEAVEHD.DLDOCNO = @DSDOCNO ";
                sqlCommand.Transaction = INSTrans;

                sqlCommand.Parameters.AddWithValue("@DSDOCNO", DLDOCNO);
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
                sqlCommand.CommandText = @"delete from SPC_CM_LEAVEDT where SPC_CM_LEAVEDT.DLDOCNO = @DLDOCNO ";
                sqlCommand.Transaction = INSTrans;

                sqlCommand.Parameters.AddWithValue("@DLDOCNO", DLDOCNO);
                sqlCommand.ExecuteNonQuery();

            }

            #endregion

            #region InsertDT

            foreach (DataGridViewRow row in Dg_Leave.Rows)
            {
                if (row.IsNewRow == false)
                {
                    try
                    {

                        using (SqlCommand sqlCommand = new SqlCommand())
                        {
                            sqlCommand.Connection = sqlConnectionINS;
                            sqlCommand.CommandText = @"insert into SPC_CM_LEAVEDT(DLDOCNO,LEAVEDATE,LEAVEREMARK,LEAVETYPE,LEAVETYPEDETAIL,ATTACH,HALFDAY,HALFDAYTIME1,HALFDAYTIME2)
                                                           values(@DLDOCNO,@LEAVEDATE,@LEAVEREMARK,@LEAVETYPE,@LEAVETYPEDETAIL,@ATTACH,@HALFDAY,@HALFDAYTIME1,@HALFDAYTIME2)";
                            sqlCommand.Transaction = INSTrans;

                            sqlCommand.Parameters.AddWithValue("@DLDOCNO", DLDOCNO);
                            sqlCommand.Parameters.AddWithValue("@LEAVEDATE", Convert.ToDateTime(row.Cells["LEAVEDATE"].Value.ToString()));
                            sqlCommand.Parameters.AddWithValue("@LEAVEREMARK", row.Cells["LEAVEREMARK"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@LEAVETYPE", row.Cells["LEAVETYPE"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@LEAVETYPEDETAIL", row.Cells["LEAVETYPEDETAIL"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@ATTACH", row.Cells["ATTACH"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@HALFDAY", row.Cells["HALFDAYTYPE"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@HALFDAYTIME1", row.Cells["HALFDAYTIME1"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@HALFDAYTIME2", row.Cells["HALFDAYTIME2"].Value.ToString());
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

        void Btn_Cancel_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("ต้องการยกเลิกเอกสารเลขที่ " + DLDOCNO.ToString() + " ของ " + Txt_Emplname.Text + " หรือไม่ ?", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
                sqlCommand.CommandText = @"update SPC_CM_LEAVEHD set DOCSTAT = 0,
                                                    MODIFIEDBY = @MODIFIEDBY ,MODIFIEDDATE = @MODIFIEDDATE 
                                                    where SPC_CM_LEAVEHD.DLDOCNO = @DLDOCNO ";
                sqlCommand.Transaction = INSTrans;

                sqlCommand.Parameters.AddWithValue("@DLDOCNO", DLDOCNO);
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

        void Btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                var row_select = this.Dg_Leave.SelectedRows;

                foreach (DataGridViewRow row in row_select)
                {
                    Dg_Leave.Rows.Remove(row);
                }
                Dg_Leave.Refresh();
            }
            catch (Exception ex)
            {

            }
        }

        void Btn_Add_Click(object sender, EventArgs e)
        {
            if (Rcb_Half.Checked == true && (Txt_Half_Time1.Text == "" || Txt_Half_Time2.Text == ""))
            {
                MessageBox.Show("กรุณาใส่เวลาที่ต้องการลาครึ่งวัน...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Txt_Emplid.Text == "" || Txt_Emplname.Text == "" || Txt_Remark.Text == "" || Ddl_Rest1.Text == "")
            {
                MessageBox.Show("กรุณาใส่ข้อมูลให้ครบ...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (Dtp_Leave.Value.Date > Dtp_Leave2.Value.Date)
            {
                MessageBox.Show("กรุณาเลือกช่วงวันให้ถูกต้อง...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i <= Dtp_Leave2.Value.Date.Subtract(Dtp_Leave.Value.Date).TotalDays; i++)
            {

                if (Dg_Leave.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in Dg_Leave.Rows)
                    {
                        if (Dtp_Leave.Value.Date.AddDays(i).ToString("dd/MM/yyyy") == row.Cells["LEAVEDATE"].Value.ToString())
                        {
                            MessageBox.Show("มีข้อมูลวันที่ " + Dtp_Leave.Value.Date.AddDays(i).ToString("dd/MM/yyyy") + " นี้อยู่แล้ว", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    }
                }

                #region Check_LeaveDateOtherDL

                SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
                DataTable dataTable = new DataTable();

                try
                {
                    #region SQL

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
                                                            left join SPC_CM_LEAVEDT on SPC_CM_LEAVEHD.DLDOCNO = SPC_CM_LEAVEDT.DLDOCNO
                                                            where DOCSTAT = 1 AND HEADAPPROVED = 0 AND SPC_CM_LEAVEHD.DLDOCNO <> '{2}' 
                                                            AND EMPLID = '{0}' AND LEAVEDATE = '{1}' ", Txt_Emplid.Text, Dtp_Leave.Value.Date.AddDays(i).ToString("yyyy-MM-dd"), DLDOCNO);

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

                        if (dataTable.Rows.Count != 0)
                        {
                            MessageBox.Show("มีวันลาที่ " + Dtp_Leave.Value.Date.AddDays(i).ToString("dd/MM/yyyy") + " นี้อยู่ในเลขที่เอกสาร " + dataTable.Rows[0]["DLDOCNO"].ToString() + " แล้ว...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
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
                    sqlConnection.Close();
                }


                #endregion


                Txt_Emplid.Enabled = false;

                Dg_Leave.Invoke(new EventHandler(delegate
                {
                    Dg_Leave.Rows.Add();
                    Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["LEAVEDATE"].Value =
                        Dtp_Leave.Value.Date.AddDays(i).ToString("dd/MM/yyyy");
                    Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["LEAVETYPE"].Value =
                        LEAVETYPE;
                    Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["LEAVETYPEDETAIL"].Value =
                        Rdb_Other.IsChecked == false ? LEAVETYPEDETAIL : Txt_Remark_OtherLeave.Text == "" ? "ลาอื่นๆ" : Txt_Remark_OtherLeave.Text;
                    Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["ATTACH"].Value =
                        Rcb_Attach.Checked == true ? "1" : "0";
                    Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["ATTACHDOC"].Value =
                        Rcb_Attach.Checked == true ? "มี" : "ไม่มี";
                    Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["HALFDAYTYPE"].Value =
                        Rcb_Half.Checked == true ? "1" : "0";
                    Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["HALFDAY"].Value =
                        Rcb_Half.Checked == true ? "ครึ่งวัน" : "ทั้งวัน";
                    Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["HALFDAYTIME1"].Value =
                        Rcb_Half.Checked == true ? Txt_Half_Time1.Text : "";
                    Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["HALFDAYTIME2"].Value =
                        Rcb_Half.Checked == true ? Txt_Half_Time2.Text : "";
                    Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["LEAVEREMARK"].Value =
                        Txt_Remark.Text;
                }));
            }
        }

        #endregion

        #region InitializeRadioButton

        private void InitializeRadioButton()
        {
            Rdb_Vacation.ToggleStateChanged += new StateChangedEventHandler(Rdb_Vacation_ToggleStateChanged);
            Rdb_Personal.ToggleStateChanged += new StateChangedEventHandler(Rdb_Personal_ToggleStateChanged);
            Rdb_Personal_NM.ToggleStateChanged += new StateChangedEventHandler(Rdb_Personal_NM_ToggleStateChanged);
            Rdb_Sick.ToggleStateChanged += new StateChangedEventHandler(Rdb_Sick_ToggleStateChanged);
            Rdb_Sick_NM.ToggleStateChanged += new StateChangedEventHandler(Rdb_Sick_NM_ToggleStateChanged);
            Rdb_Maternity.ToggleStateChanged += new StateChangedEventHandler(Rdb_Maternity_ToggleStateChanged);
            Rdb_Other.ToggleStateChanged += new StateChangedEventHandler(Rdb_Other_ToggleStateChanged);
        }

        void Rdb_Other_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (Rdb_Other.IsChecked)
            {
                LEAVETYPE = "7";
                LEAVETYPEDETAIL = Txt_Remark_OtherLeave.Text == "" ? "ลาอื่นๆ" : Txt_Remark_OtherLeave.Text;
                Txt_Remark_OtherLeave.Enabled = true;
                Rcb_Half.Enabled = true;

            }
        }

        void Rdb_Maternity_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (Rdb_Maternity.IsChecked)
            {
                LEAVETYPE = "6";
                LEAVETYPEDETAIL = Rdb_Maternity.Text;
                Txt_Remark_OtherLeave.Enabled = false;
                Rcb_Half.Enabled = true;

            }
        }

        void Rdb_Sick_NM_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (Rdb_Sick_NM.IsChecked)
            {
                LEAVETYPE = "5";
                LEAVETYPEDETAIL = Rdb_Sick_NM.Text;
                Txt_Remark_OtherLeave.Enabled = false;
                Rcb_Half.Enabled = true;

            }
        }

        void Rdb_Sick_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (Rdb_Sick.IsChecked)
            {
                LEAVETYPE = "2";
                LEAVETYPEDETAIL = Rdb_Sick.Text;
                Txt_Remark_OtherLeave.Enabled = false;
                Rcb_Half.Enabled = true;
            }
        }

        void Rdb_Personal_NM_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (Rdb_Personal_NM.IsChecked)
            {
                LEAVETYPE = "4";
                LEAVETYPEDETAIL = Rdb_Personal_NM.Text;
                Txt_Remark_OtherLeave.Enabled = false;
                Rcb_Half.Enabled = true;
            }
        }

        void Rdb_Personal_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (Rdb_Personal.IsChecked)
            {
                LEAVETYPE = "1";
                LEAVETYPEDETAIL = Rdb_Personal.Text;
                Txt_Remark_OtherLeave.Enabled = false;
                Rcb_Half.Enabled = true;
            }
        }

        void Rdb_Vacation_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (Rdb_Vacation.IsChecked)
            {
                LEAVETYPE = "3";
                LEAVETYPEDETAIL = Rdb_Vacation.Text;
                Txt_Remark_OtherLeave.Enabled = false;
                Rcb_Half.Enabled = false;
            }
        }

        #endregion

        #region InitializeCheckBox

        private void InitializeCheckBox()
        {
            Rcb_Half.ToggleStateChanged += new StateChangedEventHandler(Rcb_Half_ToggleStateChanged);
        }

        void Rcb_Half_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (Rcb_Half.Checked == true)
            {
                Txt_Half_Time1.Enabled = true;
                Txt_Half_Time2.Enabled = true;
            }
            else
            {
                Txt_Half_Time1.Enabled = false;
                Txt_Half_Time2.Enabled = false;
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
        }

        #endregion

        #region Function

        void Load_DataDL()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_Leave.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"select convert(varchar,LEAVEDATE,103) as LEAVEDATE2,* from SPC_CM_LEAVEHD
                                                            left join SPC_CM_LEAVEDT on SPC_CM_LEAVEHD.DLDOCNO = SPC_CM_LEAVEDT.DLDOCNO
                                                            where SPC_CM_LEAVEHD.DLDOCNO = '{0}' ", DLDOCNO);

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

                    Txt_Emplid.Text = dataTable.Rows[0]["EMPLID"].ToString();
                    Txt_Emplname.Text = dataTable.Rows[0]["EMPLNAME"].ToString();
                    Txt_SectionName.Text = dataTable.Rows[0]["SECTIONNAME"].ToString();
                    Txt_DeptName.Text = dataTable.Rows[0]["DEPTNAME"].ToString();
                    Ddl_Rest1.SelectedIndex = Convert.ToInt16(dataTable.Rows[0]["RESTDAY1"].ToString());
                    Ddl_Rest2.SelectedIndex = Convert.ToInt16(dataTable.Rows[0]["RESTDAY2"].ToString());
                    Load_WorkTime();

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        Dg_Leave.Invoke(new EventHandler(delegate
                        {
                            Dg_Leave.Rows.Add();
                            Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["LEAVEDATE"].Value =
                                Convert.ToDateTime(dataTable.Rows[i]["LEAVEDATE"].ToString()).Date.ToString("dd/MM/yyyy");
                            Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["LEAVETYPE"].Value =
                                dataTable.Rows[i]["LEAVETYPE"].ToString();
                            Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["LEAVETYPEDETAIL"].Value =
                                dataTable.Rows[i]["LEAVETYPEDETAIL"].ToString();
                            Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["ATTACH"].Value =
                                dataTable.Rows[i]["ATTACH"].ToString();
                            Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["ATTACHDOC"].Value =
                                dataTable.Rows[i]["ATTACH"].ToString() == "1" ? "มี" : "ไม่มี";
                            Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["HALFDAYTYPE"].Value =
                                dataTable.Rows[i]["HALFDAY"].ToString();
                            Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["HALFDAY"].Value =
                                dataTable.Rows[i]["HALFDAY"].ToString() == "1" ? "ครึ่งวัน" : "ทั้งวัน";
                            Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["HALFDAYTIME1"].Value =
                                dataTable.Rows[i]["HALFDAYTIME1"].ToString();
                            Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["HALFDAYTIME2"].Value =
                                dataTable.Rows[i]["HALFDAYTIME2"].ToString();
                            Dg_Leave.Rows[Dg_Leave.RowCount - 1].Cells["LEAVEREMARK"].Value =
                                dataTable.Rows[i]["LEAVEREMARK"].ToString();
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

            Txt_Emplid.Text = "";
            Txt_Emplid.Enabled = true;
            Txt_Emplname.Text = "";
            Txt_DeptName.Text = "";
            Txt_SectionName.Text = "";
            Ddl_Rest1.Text = "";
            Ddl_Rest1.Enabled = true;
            Ddl_Rest2.Text = "";
            Txt_Remark.Text = "";
            Txt_Remark_OtherLeave.Text = "";
            Dtp_Leave.Value = DateTime.Now.Date;
            Dtp_Leave2.Value = DateTime.Now.Date;
            Rdb_Personal.IsChecked = true;
            Rcb_Half.Checked = false;
            Txt_WorkTime.Text = "";
        }

        void Process_ExpireDate()
        {
            ExpireDate = DateTime.Now.Date;
            foreach (DataGridViewRow row in Dg_Leave.Rows)
            {
                if ((DateTime.Compare(ExpireDate, Convert.ToDateTime(row.Cells["LEAVEDATE"].Value.ToString()))) < 0)
                {
                    ExpireDate = Convert.ToDateTime(row.Cells["LEAVEDATE"].Value.ToString());
                }
            }

            ExpireDate = ExpireDate.AddDays(Convert.ToDouble(ClassCurUser.LASTDATEAPPROVE_LEAVE));

        }

        void Load_WorkTime()
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
                    sqlCommand.CommandText = string.Format(@"select convert(varchar,DATEDIFF(year, PWSTARTDATE, getdate()) - (CASE WHEN DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE) > getdate() THEN 1 ELSE 0 END))+'ปี '+
                                                                         convert(varchar,MONTH(getdate() - DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE)) - 1) + 'เดือน ' +
                                                                         convert(varchar,DAY(getdate() - DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE)) - 1) + 'วัน'as WORKTIME
                                                                 from PWEMPLOYEE
                                                                where PWEMPLOYEE = '{0}' ", Txt_Emplid.Text);

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
                    Txt_WorkTime.Text = dataTable.Rows[0]["WORKTIME"].ToString();
                }
                else
                {

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

        #endregion

    }
}
