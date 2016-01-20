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
    public partial class Shift_ApproveHD_Detail : Form
    {
        string DSDOCNO = "";
        
        public Shift_ApproveHD_Detail(string pDocNo)
        {
            DSDOCNO = pDocNo;
            InitializeComponent();
            InitializeForm();
            InitializeDataGridView();
            InitializeButton();
            InitializeDropDownList();
        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Shift_ApproveHD_Detail_Load);
        }

        void Shift_ApproveHD_Detail_Load(object sender, EventArgs e)
        {
            Load_DataDS();
        }

        #endregion 

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_Approve.Click += new EventHandler(Btn_Approve_Click);
            Btn_Reject.Click += new EventHandler(Btn_Reject_Click);
        }

        void Btn_Reject_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการไม่อนุมัติเอกสารนี้ ?", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
                sqlCommand.CommandText = @"update SPC_CM_SHIFTHD set HEADAPPROVED = 2,
                                                    HEADAPPROVEDBY = @HEADAPPROVEDBY ,HEADAPPROVEDDATE = @HEADAPPROVEDDATE 
                                                    ,HEADAPPROVEDBYNAME = @HEADAPPROVEDBYNAME
                                                    where SPC_CM_SHIFTHD.DSDOCNO = @DSDOCNO ";
                sqlCommand.Transaction = INSTrans;

                sqlCommand.Parameters.AddWithValue("@DSDOCNO", DSDOCNO);
                sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDBYNAME", ClassCurUser.LogInEmplName);
                sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDDATE", MyTime.GetDateTime());
                sqlCommand.ExecuteNonQuery();

            }

            #endregion

            #region Submit

            INSTrans.Commit();
            sqlConnectionINS.Close();

            #endregion

            this.DialogResult = DialogResult.Yes;
        }

        void Btn_Approve_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการอนุมัติเอกสารนี้ ?", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            using (ConfirmReject frm = new ConfirmReject())
            {
                frm.Text = "อนุมัติ";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {

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
                        sqlCommand.CommandText = @"update SPC_CM_SHIFTHD set HEADAPPROVED = 1,
                                                    HEADAPPROVEDBY = @HEADAPPROVEDBY ,HEADAPPROVEDDATE = @HEADAPPROVEDDATE
                                                    , HEADAPPROVEDBYNAME = @HEADAPPROVEDBYNAME
                                                    , HEADAPPROVEDREMARK = @HEADAPPROVEDREMARK
                                                    where SPC_CM_SHIFTHD.DSDOCNO = @DSDOCNO ";
                        sqlCommand.Transaction = INSTrans;

                        sqlCommand.Parameters.AddWithValue("@DSDOCNO", DSDOCNO);
                        sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDBY", ClassCurUser.LogInEmplId);
                        sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDBYNAME", ClassCurUser.LogInEmplName);
                        sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDDATE", MyTime.GetDateTime());
                        sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDREMARK", frm.Remark);
                        sqlCommand.ExecuteNonQuery();

                    }

                    #endregion

                    #region Submit

                    INSTrans.Commit();
                    sqlConnectionINS.Close();

                    #endregion

                    this.DialogResult = DialogResult.Yes;
                }
            }
        }

        #endregion

        #region InitializeDataGridView

        private void InitializeDataGridView()
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
            dt.Rows.Add(4, "พฤหัส");
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
            dt2.Rows.Add(4, "พฤหัส");
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
                    sqlCommand.CommandText = string.Format(@"select * from SPC_CM_SHIFTHD
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

        #endregion


    }
}
