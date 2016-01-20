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
    public partial class Shift_SearchDataDetail : Form
    {
        string DSDOCNO = "";
        public Shift_SearchDataDetail(string pDSDocNo)
        {
            DSDOCNO = pDSDocNo;
            InitializeComponent();
            initializeForm();
            InitializeDataGrid();
        }

        #region initializeForm

        private void initializeForm()
        {
            this.Load += new EventHandler(Shift_SearchDataDetail_Load);
        }

        void Shift_SearchDataDetail_Load(object sender, EventArgs e)
        {
            Load_DataDS();
        }

        #endregion

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {
            #region SetDataGrid

            Dg_Shift.AutoGenerateColumns = false;
            Dg_Shift.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_Shift.Dock = DockStyle.Fill;



            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            

            DataGridViewTextBoxColumn SHIFTDATE = new DataGridViewTextBoxColumn();
            SHIFTDATE.Name = "SHIFTDATE";
            SHIFTDATE.DataPropertyName = "SHIFTDATE";
            SHIFTDATE.HeaderText = "วันที่เปลี่ยนกะ";
            SHIFTDATE.ToolTipText = "วันที่เปลียนกะ";
            SHIFTDATE.Width = 100;
            SHIFTDATE.ReadOnly = true;
            SHIFTDATE.DefaultCellStyle.Format = "d";
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

            DataGridViewTextBoxColumn HRAPPORVEREMARK = new DataGridViewTextBoxColumn();
            HRAPPORVEREMARK.Name = "HRAPPORVEREMARK";
            HRAPPORVEREMARK.DataPropertyName = "HRAPPORVEREMARK";
            HRAPPORVEREMARK.HeaderText = "หมายเหตุ(บุคคล)";
            HRAPPORVEREMARK.ToolTipText = "หมายเหตุ(บุคคล)";
            HRAPPORVEREMARK.Width = 100;
            HRAPPORVEREMARK.ReadOnly = true;
            Dg_Shift.Columns.Add(HRAPPORVEREMARK);

            #endregion
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
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["HRAPPORVEREMARK"].Value =
                                dataTable.Rows[i]["HRAPPORVEREMARK"].ToString();
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
