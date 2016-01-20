﻿using System;
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
    public partial class Leave_SearchDataDetail : Form
    {
        string DLDOCNO = "";
        public Leave_SearchDataDetail(string pDLDocNo)
        {
            DLDOCNO = pDLDocNo;
            InitializeComponent();
            InitailizeForm();
            InitializeDataGrid();
        }

        #region InitailizeForm

        private void InitailizeForm()
        {
            this.Load += new EventHandler(Leave_SearchDataDetail_Load);
        }

        void Leave_SearchDataDetail_Load(object sender, EventArgs e)
        {
            Load_DataDL();
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
                    Txt_DLDocno.Text = dataTable.Rows[0]["DLDOCNO"].ToString();
                    Txt_Emplid.Text = dataTable.Rows[0]["EMPLID"].ToString();
                    Txt_Emplname.Text = dataTable.Rows[0]["EMPLNAME"].ToString();
                    Txt_SectionName.Text = dataTable.Rows[0]["SECTIONNAME"].ToString();
                    Txt_DeptName.Text = dataTable.Rows[0]["DEPTNAME"].ToString();
                    Ddl_Rest1.SelectedIndex = Convert.ToInt16(dataTable.Rows[0]["RESTDAY1"].ToString());
                    Ddl_Rest2.SelectedIndex = Convert.ToInt16(dataTable.Rows[0]["RESTDAY2"].ToString());


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

        #endregion
    }
}