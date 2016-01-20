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
    public partial class Leave_SearchData : Form
    {
        public Leave_SearchData()
        {
            InitializeComponent();
            InitializeForm();
            InitializeTextBox();
            InitializeDropDownList();
            InitializeDateTimePicker();
            InitializeDataGrid();
            InitializeButton();
        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Leave_SearchData_Load);
        }

        void Leave_SearchData_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region InitializeTextBox

        private void InitializeTextBox()
        {
            Txt_DocNo.Text = "DL" + DateTime.Now.Date.ToString("yyMMdd") + "-";
        }

        #endregion
           
        #region InitializeDropDownList

        private void InitializeDropDownList()
        {
            Load_Section();
        }

        #endregion

        #region InitializeDateTimePicker

        private void InitializeDateTimePicker()
        {
            Dtp_Start.Text = DateTime.Now.AddDays(-7).Date.ToString();
            Dtp_End.Text = DateTime.Now.Date.ToString();
        }

        #endregion

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {
            #region SetDataGrid

            Dg_LeaveHD.AutoGenerateColumns = false;
            Dg_LeaveHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_LeaveHD.Dock = DockStyle.Fill;

            DataGridViewTextBoxColumn DLDOCNO = new DataGridViewTextBoxColumn();
            DLDOCNO.Name = "DLDOCNO";
            DLDOCNO.DataPropertyName = "DLDOCNO";
            DLDOCNO.HeaderText = "เลขที่เอกสาร";
            DLDOCNO.ToolTipText = "เลขที่เอกสาร";
            DLDOCNO.Width = 100;
            DLDOCNO.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(DLDOCNO);

            DataGridViewTextBoxColumn EXPIREDATE = new DataGridViewTextBoxColumn();
            EXPIREDATE.Name = "EXPIREDATE";
            EXPIREDATE.DataPropertyName = "EXPIREDATE";
            EXPIREDATE.HeaderText = "อนุมัติภายใน";
            EXPIREDATE.ToolTipText = "อนุมัติภายใน";
            EXPIREDATE.Width = 100;
            EXPIREDATE.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(EXPIREDATE);

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

            DataGridViewTextBoxColumn HEADAPPROVEDDATE = new DataGridViewTextBoxColumn();
            HEADAPPROVEDDATE.Name = "HEADAPPROVEDDATE";
            HEADAPPROVEDDATE.DataPropertyName = "HEADAPPROVEDDATE";
            HEADAPPROVEDDATE.HeaderText = "อนุมัติเมื่อ";
            HEADAPPROVEDDATE.ToolTipText = "อนุมัติเมื่อ";
            HEADAPPROVEDDATE.Width = 100;
            HEADAPPROVEDDATE.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(HEADAPPROVEDDATE);


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

            DataGridViewTextBoxColumn HRAPPROVEDDATE = new DataGridViewTextBoxColumn();
            HRAPPROVEDDATE.Name = "HRAPPROVEDDATE";
            HRAPPROVEDDATE.DataPropertyName = "HRAPPROVEDDATE";
            HRAPPROVEDDATE.HeaderText = "อนุมัติเมื่อ";
            HRAPPROVEDDATE.ToolTipText = "อนุมัติเมื่อ";
            HRAPPROVEDDATE.Width = 100;
            HRAPPROVEDDATE.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(HRAPPROVEDDATE);

            DataGridViewTextBoxColumn HRAPPROVEREMARK = new DataGridViewTextBoxColumn();
            HRAPPROVEREMARK.Name = "HRAPPROVEREMARK";
            HRAPPROVEREMARK.DataPropertyName = "HRAPPROVEREMARK";
            HRAPPROVEREMARK.HeaderText = "หมายเหตุไม่อนุมัติ";
            HRAPPROVEREMARK.ToolTipText = "หมายเหตุไม่อนุมัติ";
            HRAPPROVEREMARK.Width = 100;
            HRAPPROVEREMARK.ReadOnly = true;
            Dg_LeaveHD.Columns.Add(HRAPPROVEREMARK);

            DataGridViewButtonColumn Detail = new DataGridViewButtonColumn();
            Detail.Name = "Detail";
            Detail.DataPropertyName = "Detail";
            Detail.Text = "รายละเอียด";
            Detail.HeaderText = "รายละเอียด";
            Detail.ToolTipText = "รายละเอียด";
            Detail.Width = 100;
            Detail.UseColumnTextForButtonValue = true;
            Dg_LeaveHD.Columns.Add(Detail);

            #endregion

            #region SetEvent

            Dg_LeaveHD.CellContentClick += new DataGridViewCellEventHandler(Dg_LeaveHD_CellContentClick);

            #endregion
        }

        void Dg_LeaveHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Dg_LeaveHD.Columns["Detail"].Index)
            {
                using (Leave_SearchDataDetail frm = new Leave_SearchDataDetail(Dg_LeaveHD.Rows[e.RowIndex].Cells["DLDOCNO"].Value.ToString()))
                {
                    frm.Text = "รายละเอียดข้อมูล";

                    if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                    {

                    }
                }
            }
        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_Search.Click += new EventHandler(Btn_Search_Click);
        }

        void Btn_Search_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        #endregion

        #region Function

        void SearchData()
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

                    if (Rb_Section.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
                                                                where SECTIONID = '{0}' AND DOCDATE BETWEEN '{1}' and '{2}' 
                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                    where EMPLID = '{3}' ) ", Cbb_Section.SelectedValue.ToString(), Dtp_Start.Value.Date.ToString("yyyy-MM-dd"), Dtp_End.Value.Date.ToString("yyyy-MM-dd"), ClassCurUser.LogInEmplId);

                    }
                    else if (Rb_Empl.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
                                                                where EMPLID = '{0}' AND DOCDATE BETWEEN '{1}' and '{2}' 
                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                    where EMPLID = '{3}' ) ", Txt_EmplID.Text, Dtp_Start.Value.Date.ToString("yyyy-MM-dd"), Dtp_End.Value.Date.ToString("yyyy-MM-dd"), ClassCurUser.LogInEmplId);

                    }
                    else
                    {
                        sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
                                                            where SPC_CM_LEAVEHD.DLDOCNO = '{0}' 
                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                    where EMPLID = '{1}' ) ", Txt_DocNo.Text, ClassCurUser.LogInEmplId);
                    }


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
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["EXPIREDATE"].Value =
                                Convert.ToDateTime(dataTable.Rows[i]["EXPIREDATE"].ToString()).Date.ToString("dd/MM/yyyy");
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["DOCSTAT"].Value =
                                dataTable.Rows[i]["DOCSTAT"].ToString() == "0" ? "ยกเลิก" : "ใช้งาน";
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["EMPLNAME"].Value =
                                dataTable.Rows[i]["EMPLNAME"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HEADAPPROVED"].Value =
                                dataTable.Rows[i]["HEADAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HEADAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HEADAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBY"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HEADAPPROVEDDATE"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDDATE"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HEADAPPROVEDBYNAME"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBYNAME"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HRAPPROVED"].Value =
                                dataTable.Rows[i]["HRAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HRAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HRAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HRAPPROVEDBY"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HRAPPROVEDBYNAME"].Value =
                                dataTable.Rows[i]["HRAPPROVEDBYNAME"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HRAPPROVEDDATE"].Value =
                                dataTable.Rows[i]["HRAPPROVEDDATE"].ToString();
                            Dg_LeaveHD.Rows[Dg_LeaveHD.RowCount - 1].Cells["HRAPPROVEREMARK"].Value =
                               dataTable.Rows[i]["HRAPPROVEREMARK"].ToString();
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

        void Load_Section()
        {

            Cbb_Section.Items.Clear();

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"SELECT DISTINCT * FROM(
	                                                                                    SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC 
	                                                                                    FROM PWEMPLOYEE  
		                                                                                        LEFT OUTER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS                                
	                                                                                    
                                                                                    ) AS PWSECTION
                                                            WHERE PWSECTION IS NOT NULL AND PWSECTION IN (SELECT PWSECTION 
												                                                            FROM SPC_CM_AUTHORIZE
												                                                            WHERE EMPLID = '{0}' 
                                                                                                            )
                                                            OR PWSECTION = '{1}'
                                                            ORDER BY PWDESC ", ClassCurUser.LogInEmplId, ClassCurUser.LogInSection);

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

                    if (dataTable.Rows.Count > 0)
                    {
                        Cbb_Section.DataSource = dataTable;
                        Cbb_Section.ValueMember = "PWSECTION";
                        Cbb_Section.DisplayMember = "PWDESC";

                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        #endregion


    }
}
