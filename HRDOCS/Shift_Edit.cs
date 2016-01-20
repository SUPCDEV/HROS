using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SysApp;
using System.Data.SqlClient;

namespace HRDOCS
{
    public partial class Shift_Edit : Form
    {
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

        public Shift_Edit()
        {
            InitializeComponent();
            InitializeForm();
            InitializeDataGridview();
            InitializeButton();

        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Shift_Edit_Load);
            this.KeyDown += new KeyEventHandler(Shift_Edit_KeyDown);
        }

        void Shift_Edit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.F5)
                {
                    LoadDataHD();
                    if (Dg_ShiftHD.Rows.Count > 0)
                    {
                        LoadDataDT();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        void Shift_Edit_Load(object sender, EventArgs e)
        {
            //LoadData();
            LoadDataHD();
            if (Dg_ShiftHD.Rows.Count > 0)
            {
                LoadDataDT();
            }

        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_Edit.Click += new EventHandler(Btn_Edit_Click);
            Btn_Cancel.Click += new EventHandler(Btn_Cancel_Click);
        }

        void Btn_Cancel_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("ต้องการยกเลิกเอกสาร " + Dg_ShiftHD.CurrentRow.Cells["DSDOCNO"].Value.ToString() + " นี้ ?", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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

                sqlCommand.Parameters.AddWithValue("@DSDOCNO", Dg_ShiftHD.CurrentRow.Cells["DSDOCNO"].Value.ToString());
                sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDDATE", MyTime.GetDateTime());
                sqlCommand.ExecuteNonQuery();

            }

            #endregion

            #region Submit

            INSTrans.Commit();
            sqlConnectionINS.Close();

            #endregion

        }

        void Btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                using (Shift_EditDetail frm = new Shift_EditDetail(Dg_ShiftHD.CurrentRow.Cells["DSDOCNO"].Value.ToString()))
                {
                    frm.Text = "แก้ไขข้อมูล";

                    if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                    {
                        //โหลดข้อมูลใหม่
                        LoadDataHD();
                        if (Dg_ShiftDT.Rows.Count > 0)
                        {
                            Dg_ShiftDT.Rows.Clear();
                            if (Dg_ShiftHD.Rows.Count > 0)
                            {
                                LoadDataDT();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region InitializeDataGridview

        private void InitializeDataGridview()
        {

            #region SetDataGrid

            SetDataGrid_ShiftHD();
            SetDataGrid_ShiftDT();

            #endregion

            #region SetEvent

            Dg_ShiftHD.CellFormatting += new DataGridViewCellFormattingEventHandler(Dg_ShiftHD_CellFormatting);
            Dg_ShiftHD.CellContentClick += new DataGridViewCellEventHandler(Dg_ShiftHD_CellContentClick);
            Dg_ShiftHD.CellClick += new DataGridViewCellEventHandler(Dg_ShiftHD_CellClick);
            #endregion


        }

        void Dg_ShiftHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LoadDataDT();
            }
        }

        void Dg_ShiftHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    LoadDataDT();
            //}


        }

        void Dg_ShiftHD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {


        }

        #endregion

        #region Function

        void LoadDataHD()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_ShiftHD.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;


                    sqlCommand.CommandText = string.Format(@"select * from SPC_CM_SHIFTHD
                                                            where DOCSTAT != 0 AND HEADAPPROVED = 0 AND CREATEDBY = '{0}' ", ClassCurUser.LogInEmplId);

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

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        Dg_ShiftHD.Invoke(new EventHandler(delegate
                        {
                            Dg_ShiftHD.Rows.Add();
                            Dg_ShiftHD.Rows[Dg_ShiftHD.RowCount - 1].Cells["DSDOCNO"].Value =
                                dataTable.Rows[i]["DSDOCNO"].ToString();
                            Dg_ShiftHD.Rows[Dg_ShiftHD.RowCount - 1].Cells["DOCSTAT"].Value =
                                dataTable.Rows[i]["DOCSTAT"].ToString() == "0" ? "ยกเลิก" : "ใช้งาน";
                            Dg_ShiftHD.Rows[Dg_ShiftHD.RowCount - 1].Cells["EMPLNAME"].Value =
                                dataTable.Rows[i]["EMPLNAME"].ToString();
                            Dg_ShiftHD.Rows[Dg_ShiftHD.RowCount - 1].Cells["HEADAPPROVED"].Value =
                                dataTable.Rows[i]["HEADAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HEADAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_ShiftHD.Rows[Dg_ShiftHD.RowCount - 1].Cells["HEADAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBY"].ToString();
                            Dg_ShiftHD.Rows[Dg_ShiftHD.RowCount - 1].Cells["HEADAPPROVEDBYNAME"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBYNAME"].ToString();
                            Dg_ShiftHD.Rows[Dg_ShiftHD.RowCount - 1].Cells["HRAPPROVED"].Value =
                                dataTable.Rows[i]["HRAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HRAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_ShiftHD.Rows[Dg_ShiftHD.RowCount - 1].Cells["HRAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HRAPPROVEDBY"].ToString();
                            Dg_ShiftHD.Rows[Dg_ShiftHD.RowCount - 1].Cells["HRAPPROVEDBYNAME"].Value =
                                dataTable.Rows[i]["HRAPPROVEDBYNAME"].ToString();
                        }));
                    }
                    Btn_Edit.Enabled = true;
                    Btn_Cancel.Enabled = true;
                }
                else
                {
                    Btn_Edit.Enabled = false;
                    Btn_Cancel.Enabled = false;
                    //MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        void LoadDataDT()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_ShiftDT.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"select * from SPC_CM_SHIFTHD
                                                            left join SPC_CM_SHIFTDT on SPC_CM_SHIFTHD.DSDOCNO = SPC_CM_SHIFTDT.DSDOCNO
                                                            where SPC_CM_SHIFTHD.DSDOCNO = '{0}' ", Dg_ShiftHD.CurrentRow.Cells["DSDOCNO"].Value.ToString());

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

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        Dg_ShiftDT.Invoke(new EventHandler(delegate
                        {
                            Dg_ShiftDT.Rows.Add();
                            Dg_ShiftDT.Rows[Dg_ShiftDT.RowCount - 1].Cells["SHIFTDATE"].Value =
                                dataTable.Rows[i]["SHIFTDATE"].ToString();
                            Dg_ShiftDT.Rows[Dg_ShiftDT.RowCount - 1].Cells["FROMSHIFTID"].Value =
                                dataTable.Rows[i]["FROMSHIFTID"].ToString();
                            Dg_ShiftDT.Rows[Dg_ShiftDT.RowCount - 1].Cells["TOSHIFTID"].Value =
                                dataTable.Rows[i]["TOSHIFTID"].ToString();
                            Dg_ShiftDT.Rows[Dg_ShiftDT.RowCount - 1].Cells["REFOTTIME"].Value =
                                dataTable.Rows[i]["REFOTTIME"].ToString();
                            Dg_ShiftDT.Rows[Dg_ShiftDT.RowCount - 1].Cells["REMARK"].Value =
                                dataTable.Rows[i]["REMARK"].ToString();
                        }));
                    }

                }
                else
                {
                    LoadDataHD();
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

        void SetDataGrid_ShiftHD()
        {
            Dg_ShiftHD.AutoGenerateColumns = false;
            Dg_ShiftHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_ShiftHD.Dock = DockStyle.Fill;
            Dg_ShiftHD.ReadOnly = true;

            DataGridViewTextBoxColumn DSDOCNO = new DataGridViewTextBoxColumn();
            DSDOCNO.Name = "DSDOCNO";
            DSDOCNO.DataPropertyName = "DSDOCNO";
            DSDOCNO.HeaderText = "เลขที่เอกสาร";
            DSDOCNO.ToolTipText = "เลขที่เอกสาร";
            DSDOCNO.Width = 100;
            DSDOCNO.ReadOnly = true;
            Dg_ShiftHD.Columns.Add(DSDOCNO);

            DataGridViewTextBoxColumn DOCSTAT = new DataGridViewTextBoxColumn();
            DOCSTAT.Name = "DOCSTAT";
            DOCSTAT.DataPropertyName = "DOCSTAT";
            DOCSTAT.HeaderText = "สถานะเอกสาร";
            DOCSTAT.ToolTipText = "สถานะเอกสาร";
            DOCSTAT.Width = 100;
            DOCSTAT.ReadOnly = true;
            Dg_ShiftHD.Columns.Add(DOCSTAT);

            DataGridViewTextBoxColumn EMPLNAME = new DataGridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.DataPropertyName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.ToolTipText = "ชื่อ";
            EMPLNAME.Width = 100;
            EMPLNAME.ReadOnly = true;
            Dg_ShiftHD.Columns.Add(EMPLNAME);

            DataGridViewTextBoxColumn HEADAPPROVED = new DataGridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.DataPropertyName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.ToolTipText = "หน./ผช.";
            HEADAPPROVED.Width = 100;
            HEADAPPROVED.ReadOnly = true;
            Dg_ShiftHD.Columns.Add(HEADAPPROVED);

            DataGridViewTextBoxColumn HEADAPPROVEDBY = new DataGridViewTextBoxColumn();
            HEADAPPROVEDBY.Name = "HEADAPPROVEDBY";
            HEADAPPROVEDBY.DataPropertyName = "HEADAPPROVEDBY";
            HEADAPPROVEDBY.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBY.ToolTipText = "ผู้อนุมัติ";
            HEADAPPROVEDBY.Width = 100;
            HEADAPPROVEDBY.ReadOnly = true;
            HEADAPPROVEDBY.Visible = false;
            Dg_ShiftHD.Columns.Add(HEADAPPROVEDBY);

            DataGridViewTextBoxColumn HEADAPPROVEDBYNAME = new DataGridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.DataPropertyName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.ToolTipText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.Width = 100;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            Dg_ShiftHD.Columns.Add(HEADAPPROVEDBYNAME);

            DataGridViewTextBoxColumn HRAPPROVED = new DataGridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.DataPropertyName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.ToolTipText = "บุคคล";
            HRAPPROVED.Width = 100;
            HRAPPROVED.ReadOnly = true;
            Dg_ShiftHD.Columns.Add(HRAPPROVED);

            DataGridViewTextBoxColumn HRAPPROVEDBY = new DataGridViewTextBoxColumn();
            HRAPPROVEDBY.Name = "HRAPPROVEDBY";
            HRAPPROVEDBY.DataPropertyName = "HRAPPROVEDBY";
            HRAPPROVEDBY.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBY.ToolTipText = "ผู้อนุมัติ";
            HRAPPROVEDBY.Width = 100;
            HRAPPROVEDBY.ReadOnly = true;
            HRAPPROVEDBY.Visible = false;
            Dg_ShiftHD.Columns.Add(HRAPPROVEDBY);

            DataGridViewTextBoxColumn HRAPPROVEDBYNAME = new DataGridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.DataPropertyName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.ToolTipText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.Width = 100;
            HRAPPROVEDBYNAME.ReadOnly = true;
            Dg_ShiftHD.Columns.Add(HRAPPROVEDBYNAME);
        }

        void SetDataGrid_ShiftDT()
        {

            Dg_ShiftDT.AutoGenerateColumns = false;
            Dg_ShiftDT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_ShiftDT.Dock = DockStyle.Fill;
            Dg_ShiftDT.ReadOnly = true;

            DataGridViewTextBoxColumn SHIFTDATE = new DataGridViewTextBoxColumn();
            SHIFTDATE.Name = "SHIFTDATE";
            SHIFTDATE.DataPropertyName = "SHIFTDATE";
            SHIFTDATE.HeaderText = "วันที่เปลี่ยนกะ";
            SHIFTDATE.ToolTipText = "วันที่เปลียนกะ";
            SHIFTDATE.Width = 100;
            SHIFTDATE.ReadOnly = true;
            Dg_ShiftDT.Columns.Add(SHIFTDATE);

            DataGridViewTextBoxColumn FROMSHIFTID = new DataGridViewTextBoxColumn();
            FROMSHIFTID.Name = "FROMSHIFTID";
            FROMSHIFTID.DataPropertyName = "FROMSHIFTID";
            FROMSHIFTID.HeaderText = "กะเดิม";
            FROMSHIFTID.ToolTipText = "กะเดิม";
            FROMSHIFTID.Width = 100;
            FROMSHIFTID.ReadOnly = true;
            Dg_ShiftDT.Columns.Add(FROMSHIFTID);

            DataGridViewTextBoxColumn TOSHIFTID = new DataGridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.DataPropertyName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "กะที่เปลี่ยน";
            TOSHIFTID.ToolTipText = "กะที่เปลี่ยน";
            TOSHIFTID.Width = 100;
            TOSHIFTID.ReadOnly = true;
            Dg_ShiftDT.Columns.Add(TOSHIFTID);

            DataGridViewTextBoxColumn REFOTTIME = new DataGridViewTextBoxColumn();
            REFOTTIME.Name = "REFOTTIME";
            REFOTTIME.DataPropertyName = "REFOTTIME";
            REFOTTIME.HeaderText = "ช่วงเวลาทำโอที";
            REFOTTIME.ToolTipText = "ช่วงเวลาทำโอที";
            REFOTTIME.Width = 100;
            REFOTTIME.ReadOnly = true;
            Dg_ShiftDT.Columns.Add(REFOTTIME);

            DataGridViewTextBoxColumn REMARK = new DataGridViewTextBoxColumn();
            REMARK.Name = "REMARK";
            REMARK.DataPropertyName = "REMARK";
            REMARK.HeaderText = "หมายเหตุ";
            REMARK.ToolTipText = "หมายเหตุ";
            REMARK.Width = 100;
            REMARK.ReadOnly = true;
            Dg_ShiftDT.Columns.Add(REMARK);

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
                if (keyData == Keys.F5)
                {
                    LoadDataHD();
                    if (Dg_ShiftHD.Rows.Count > 0)
                    {
                        LoadDataDT();
                    }          
                }
                else if (keyData == Keys.Escape)
                {
                    this.Close();
                }
           
            return base.ProcessCmdKey(ref msg, keyData);
        }


        #endregion


    }
}
