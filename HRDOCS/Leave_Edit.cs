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
    public partial class Leave_Edit : Form
    {
        public Leave_Edit()
        {
            InitializeComponent();
            InitializeForm();
            InitializeDataGrid();
            InitializeButton();
        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Leave_Edit_Load);
        }

        void Leave_Edit_Load(object sender, EventArgs e)
        {
            LoadDataHD();
            if (Dg_LeaveHD.Rows.Count > 0)
            {
                LoadDataDT();
            }
        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_Cancel.Click += new EventHandler(Btn_Cancel_Click);
            Btn_Edit.Click += new EventHandler(Btn_Edit_Click);
        }

        void Btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                using (Leave_EditDetail frm = new Leave_EditDetail(Dg_LeaveHD.CurrentRow.Cells["DLDOCNO"].Value.ToString()))
                {
                    frm.Text = "แก้ไขข้อมูล";

                    if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                    {
                        //โหลดข้อมูลใหม่
                        LoadDataHD();
                        if (Dg_LeaveDT.Rows.Count > 0)
                        {
                            Dg_LeaveDT.Rows.Clear();
                            if (Dg_LeaveHD.Rows.Count > 0)
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

        void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการยกเลิกเอกสาร " + Dg_LeaveHD.CurrentRow.Cells["DLDOCNO"].Value.ToString() + " นี้ ?", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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

                sqlCommand.Parameters.AddWithValue("@DLDOCNO", Dg_LeaveHD.CurrentRow.Cells["DLDOCNO"].Value.ToString());
                sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDDATE", MyTime.GetDateTime());
                sqlCommand.ExecuteNonQuery();

            }

            #endregion

            #region Submit

            INSTrans.Commit();
            sqlConnectionINS.Close();

            #endregion

            LoadDataHD();
            if (Dg_LeaveDT.Rows.Count > 0)
            {
                Dg_LeaveDT.Rows.Clear();
                if (Dg_LeaveHD.Rows.Count > 0)
                {
                    LoadDataDT();
                }
            }
        }

        #endregion

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {
            #region SetDataGrid

            SetDataGrid_LeaveHD();
            SetDataGrid_LeaveDT();

            #endregion

            #region SetEvent

            Dg_LeaveHD.CellClick += new DataGridViewCellEventHandler(Dg_LeaveHD_CellClick);

            #endregion

        }

        void Dg_LeaveHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LoadDataDT();
            }
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
                Dg_LeaveHD.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;


                    sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
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
                        Dg_LeaveHD.Invoke(new EventHandler(delegate
                        {
                            Dg_LeaveHD.Rows.Add();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["DLDOCNO"].Value =
                                dataTable.Rows[i]["DLDOCNO"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["DOCSTAT"].Value =
                                dataTable.Rows[i]["DOCSTAT"].ToString() == "0" ? "ยกเลิก" : "ใช้งาน";
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["EMPLNAME"].Value =
                                dataTable.Rows[i]["EMPLNAME"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HEADAPPROVED"].Value =
                                dataTable.Rows[i]["HEADAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HEADAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HEADAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBY"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HEADAPPROVEDBYNAME"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBYNAME"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HRAPPROVED"].Value =
                                dataTable.Rows[i]["HRAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HRAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HRAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HRAPPROVEDBY"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HRAPPROVEDBYNAME"].Value =
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
                Dg_LeaveDT.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
                                                            left join SPC_CM_LEAVEDT on SPC_CM_LEAVEHD.DLDOCNO = SPC_CM_LEAVEDT.DLDOCNO
                                                            where SPC_CM_LEAVEHD.DLDOCNO = '{0}' ", Dg_LeaveHD.CurrentRow.Cells["DLDOCNO"].Value.ToString());

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
                        Dg_LeaveDT.Invoke(new EventHandler(delegate
                        {
                            Dg_LeaveDT.Rows.Add();
                            Dg_LeaveDT.Rows[Dg_LeaveDT.RowCount - 1].Cells["LEAVEDATE"].Value =
                                Convert.ToDateTime(dataTable.Rows[i]["LEAVEDATE"].ToString()).Date.ToString("dd/MM/yyyy");
                            Dg_LeaveDT.Rows[Dg_LeaveDT.RowCount - 1].Cells["LEAVETYPE"].Value =
                                dataTable.Rows[i]["LEAVETYPE"].ToString();
                            Dg_LeaveDT.Rows[Dg_LeaveDT.RowCount - 1].Cells["LEAVETYPEDETAIL"].Value =
                                dataTable.Rows[i]["LEAVETYPEDETAIL"].ToString();
                            Dg_LeaveDT.Rows[Dg_LeaveDT.RowCount - 1].Cells["ATTACH"].Value =
                                dataTable.Rows[i]["ATTACH"].ToString();
                            Dg_LeaveDT.Rows[Dg_LeaveDT.RowCount - 1].Cells["ATTACHDOC"].Value =
                                dataTable.Rows[i]["ATTACH"].ToString() == "1" ? "มี" : "ไม่มี";
                            Dg_LeaveDT.Rows[Dg_LeaveDT.RowCount - 1].Cells["HALFDAYTYPE"].Value =
                                dataTable.Rows[i]["HALFDAY"].ToString();
                            Dg_LeaveDT.Rows[Dg_LeaveDT.RowCount - 1].Cells["HALFDAY"].Value =
                                dataTable.Rows[i]["HALFDAY"].ToString() == "1" ? "ครึ่งวัน" : "ทั้งวัน";
                            Dg_LeaveDT.Rows[Dg_LeaveDT.RowCount - 1].Cells["HALFDAYTIME1"].Value =
                                dataTable.Rows[i]["HALFDAYTIME1"].ToString();
                            Dg_LeaveDT.Rows[Dg_LeaveDT.RowCount - 1].Cells["HALFDAYTIME2"].Value =
                                dataTable.Rows[i]["HALFDAYTIME2"].ToString();
                            Dg_LeaveDT.Rows[Dg_LeaveDT.RowCount - 1].Cells["LEAVEREMARK"].Value =
                                dataTable.Rows[i]["LEAVEREMARK"].ToString();
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

        void SetDataGrid_LeaveHD()
        {
            Dg_LeaveHD.AutoGenerateColumns = false;
            Dg_LeaveHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_LeaveHD.Dock = DockStyle.Fill;
            Dg_LeaveHD.ReadOnly = true;
            Dg_LeaveHD.Font = new Font("Segoe UI", 10);

            DataGridViewTextBoxColumn DLDOCNO = new DataGridViewTextBoxColumn();
            DLDOCNO.Name = "DLDOCNO";
            DLDOCNO.DataPropertyName = "DLDOCNO";
            DLDOCNO.HeaderText = "เลขที่เอกสาร";
            DLDOCNO.ToolTipText = "เลขที่เอกสาร";
            DLDOCNO.Width = 100;
            DLDOCNO.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(DLDOCNO);

            DataGridViewTextBoxColumn DOCSTAT = new DataGridViewTextBoxColumn();
            DOCSTAT.Name = "DOCSTAT";
            DOCSTAT.DataPropertyName = "DOCSTAT";
            DOCSTAT.HeaderText = "สถานะเอกสาร";
            DOCSTAT.ToolTipText = "สถานะเอกสาร";
            DOCSTAT.Width = 100;
            DOCSTAT.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(DOCSTAT);

            DataGridViewTextBoxColumn EMPLNAME = new DataGridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.DataPropertyName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.ToolTipText = "ชื่อ";
            EMPLNAME.Width = 100;
            EMPLNAME.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(EMPLNAME);

            DataGridViewTextBoxColumn HEADAPPROVED = new DataGridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.DataPropertyName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.ToolTipText = "หน./ผช.";
            HEADAPPROVED.Width = 100;
            HEADAPPROVED.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(HEADAPPROVED);

            DataGridViewTextBoxColumn HEADAPPROVEDBY = new DataGridViewTextBoxColumn();
            HEADAPPROVEDBY.Name = "HEADAPPROVEDBY";
            HEADAPPROVEDBY.DataPropertyName = "HEADAPPROVEDBY";
            HEADAPPROVEDBY.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBY.ToolTipText = "ผู้อนุมัติ";
            HEADAPPROVEDBY.Width = 100;
            HEADAPPROVEDBY.ReadOnly = true;
            HEADAPPROVEDBY.Visible = false;
            Dg_LeaveHD.Columns.Add(HEADAPPROVEDBY);

            DataGridViewTextBoxColumn HEADAPPROVEDBYNAME = new DataGridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.DataPropertyName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.ToolTipText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.Width = 100;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(HEADAPPROVEDBYNAME);

            DataGridViewTextBoxColumn HRAPPROVED = new DataGridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.DataPropertyName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.ToolTipText = "บุคคล";
            HRAPPROVED.Width = 100;
            HRAPPROVED.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(HRAPPROVED);

            DataGridViewTextBoxColumn HRAPPROVEDBY = new DataGridViewTextBoxColumn();
            HRAPPROVEDBY.Name = "HRAPPROVEDBY";
            HRAPPROVEDBY.DataPropertyName = "HRAPPROVEDBY";
            HRAPPROVEDBY.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBY.ToolTipText = "ผู้อนุมัติ";
            HRAPPROVEDBY.Width = 100;
            HRAPPROVEDBY.ReadOnly = true;
            HRAPPROVEDBY.Visible = false;
            Dg_LeaveHD.Columns.Add(HRAPPROVEDBY);

            DataGridViewTextBoxColumn HRAPPROVEDBYNAME = new DataGridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.DataPropertyName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.ToolTipText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.Width = 100;
            HRAPPROVEDBYNAME.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(HRAPPROVEDBYNAME);
        }

        void SetDataGrid_LeaveDT()
        {

            Dg_LeaveDT.AutoGenerateColumns = false;
            Dg_LeaveDT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_LeaveDT.Dock = DockStyle.Fill;
            Dg_LeaveDT.Font = new Font("Segoe UI", 10);

            DataGridViewTextBoxColumn LEAVEDATE = new DataGridViewTextBoxColumn();
            LEAVEDATE.Name = "LEAVEDATE";
            LEAVEDATE.DataPropertyName = "LEAVEDATE";
            LEAVEDATE.HeaderText = "วันที่ลา";
            LEAVEDATE.ToolTipText = "วันที่ลา";
            LEAVEDATE.Width = 100;
            LEAVEDATE.ReadOnly = true;
            Dg_LeaveDT.Columns.Add(LEAVEDATE);

            DataGridViewTextBoxColumn LEAVETYPE = new DataGridViewTextBoxColumn();
            LEAVETYPE.Name = "LEAVETYPE";
            LEAVETYPE.DataPropertyName = "LEAVETYPE";
            LEAVETYPE.HeaderText = "ประเภทการลา";
            LEAVETYPE.ToolTipText = "ประเภทการลา";
            LEAVETYPE.Width = 100;
            LEAVETYPE.ReadOnly = true;
            LEAVETYPE.Visible = false;
            Dg_LeaveDT.Columns.Add(LEAVETYPE);

            DataGridViewTextBoxColumn LEAVETYPEDETAIL = new DataGridViewTextBoxColumn();
            LEAVETYPEDETAIL.Name = "LEAVETYPEDETAIL";
            LEAVETYPEDETAIL.DataPropertyName = "LEAVETYPEDETAIL";
            LEAVETYPEDETAIL.HeaderText = "ประเภทการลา";
            LEAVETYPEDETAIL.ToolTipText = "ประเภทการลา";
            LEAVETYPEDETAIL.Width = 100;
            LEAVETYPEDETAIL.ReadOnly = true;
            Dg_LeaveDT.Columns.Add(LEAVETYPEDETAIL);

            DataGridViewTextBoxColumn ATTACH = new DataGridViewTextBoxColumn();
            ATTACH.Name = "ATTACH";
            ATTACH.DataPropertyName = "ATTACH";
            ATTACH.HeaderText = "แนบใบรับรองแพทย์";
            ATTACH.ToolTipText = "แนบใบรับรองแพทย์";
            ATTACH.Width = 100;
            ATTACH.ReadOnly = true;
            ATTACH.Visible = false;
            Dg_LeaveDT.Columns.Add(ATTACH);

            DataGridViewTextBoxColumn ATTACHDOC = new DataGridViewTextBoxColumn();
            ATTACHDOC.Name = "ATTACHDOC";
            ATTACHDOC.DataPropertyName = "ATTACHDOC";
            ATTACHDOC.HeaderText = "แนบใบรับรองแพทย์";
            ATTACHDOC.ToolTipText = "แนบใบรับรองแพทย์";
            ATTACHDOC.Width = 100;
            ATTACHDOC.ReadOnly = true;
            ATTACHDOC.Visible = true;
            Dg_LeaveDT.Columns.Add(ATTACHDOC);

            DataGridViewTextBoxColumn HALFDAYTYPE = new DataGridViewTextBoxColumn();
            HALFDAYTYPE.Name = "HALFDAYTYPE";
            HALFDAYTYPE.DataPropertyName = "HALFDAYTYPE";
            HALFDAYTYPE.HeaderText = "จำนวนวัน";
            HALFDAYTYPE.ToolTipText = "จำนวนวัน";
            HALFDAYTYPE.Width = 100;
            HALFDAYTYPE.ReadOnly = true;
            HALFDAYTYPE.Visible = false;
            Dg_LeaveDT.Columns.Add(HALFDAYTYPE);

            DataGridViewTextBoxColumn HALFDAY = new DataGridViewTextBoxColumn();
            HALFDAY.Name = "HALFDAY";
            HALFDAY.DataPropertyName = "HALFDAY";
            HALFDAY.HeaderText = "จำนวนวัน";
            HALFDAY.ToolTipText = "จำนวนวัน";
            HALFDAY.Width = 100;
            HALFDAY.ReadOnly = true;
            Dg_LeaveDT.Columns.Add(HALFDAY);

            DataGridViewTextBoxColumn HALFDAYTIME1 = new DataGridViewTextBoxColumn();
            HALFDAYTIME1.Name = "HALFDAYTIME1";
            HALFDAYTIME1.DataPropertyName = "HALFDAYTIME1";
            HALFDAYTIME1.HeaderText = "ตั้งแต่เวลา";
            HALFDAYTIME1.ToolTipText = "ตั้งแต่เวลา";
            HALFDAYTIME1.Width = 100;
            HALFDAYTIME1.ReadOnly = true;
            Dg_LeaveDT.Columns.Add(HALFDAYTIME1);

            DataGridViewTextBoxColumn HALFDAYTIME2 = new DataGridViewTextBoxColumn();
            HALFDAYTIME2.Name = "HALFDAYTIME2";
            HALFDAYTIME2.DataPropertyName = "HALFDAYTIME2";
            HALFDAYTIME2.HeaderText = "ถึงเวลา";
            HALFDAYTIME2.ToolTipText = "ถึงเวลา";
            HALFDAYTIME2.Width = 100;
            HALFDAYTIME2.ReadOnly = true;
            Dg_LeaveDT.Columns.Add(HALFDAYTIME2);

            DataGridViewTextBoxColumn LEAVEREMARK = new DataGridViewTextBoxColumn();
            LEAVEREMARK.Name = "LEAVEREMARK";
            LEAVEREMARK.DataPropertyName = "LEAVEREMARK";
            LEAVEREMARK.HeaderText = "หมายเหตุ";
            LEAVEREMARK.ToolTipText = "หมายเหตุ";
            LEAVEREMARK.Width = 100;
            LEAVEREMARK.ReadOnly = true;
            Dg_LeaveDT.Columns.Add(LEAVEREMARK);

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.F5)
            {
                LoadDataHD();
                if (Dg_LeaveHD.Rows.Count > 0)
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
