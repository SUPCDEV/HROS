using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SysApp;

namespace HRDOCS
{
    public partial class Shift_ApproveHR : Form
    {
        #region MyRegion SysEmpl

        string sysuser = "";
        string sectionid = "";
        string approveout = "";

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

        protected string sysoutoffice;
        public string SysOutoffice
        {
            get { return sysoutoffice; }
            set { sysoutoffice = value; }
        }

        protected string section;
        public string Section
        {
            get { return section; }
            set { section = value; }
        }

        protected string syshrapproveout;
        public string HrApproveOut
        {
            get { return syshrapproveout; }
            set { syshrapproveout = value; }
        }

        #endregion

        public Shift_ApproveHR()
        {
            InitializeComponent();
            InitializeForm();
            InitializeButton();
            InitializeDataGrid();
            InitializeDateTimePicker();
            InitializeComboBox();
        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Shift_ApproveHR_Load);
        }

        void Shift_ApproveHR_Load(object sender, EventArgs e)
        {
            this.sysuser = SysOutoffice;
            this.sectionid = Section;
            this.approveout = HrApproveOut;

            Txt_DocNo.Text = "DS" + DateTime.Now.Date.ToString("yyMMdd") + "-";

        }

        #endregion

        #region InitializeComboBox

        private void InitializeComboBox()
        {
            Load_Section();
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

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {
            #region SetDataGrid

            Dg_HRApprove.AutoGenerateColumns = false;
            Dg_HRApprove.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_HRApprove.Dock = DockStyle.Fill;

            DataGridViewTextBoxColumn DSDOCNO = new DataGridViewTextBoxColumn();
            DSDOCNO.Name = "DSDOCNO";
            DSDOCNO.DataPropertyName = "DSDOCNO";
            DSDOCNO.HeaderText = "เลขที่เอกสาร";
            DSDOCNO.ToolTipText = "เลขที่เอกสาร";
            DSDOCNO.Width = 100;
            DSDOCNO.ReadOnly = true;
            Dg_HRApprove.Columns.Add(DSDOCNO);

            DataGridViewTextBoxColumn DOCSTAT = new DataGridViewTextBoxColumn();
            DOCSTAT.Name = "DOCSTAT";
            DOCSTAT.DataPropertyName = "DOCSTAT";
            DOCSTAT.HeaderText = "สถานะเอกสาร";
            DOCSTAT.ToolTipText = "สถานะเอกสาร";
            DOCSTAT.Width = 100;
            DOCSTAT.ReadOnly = true;
            Dg_HRApprove.Columns.Add(DOCSTAT);

            DataGridViewTextBoxColumn EMPLNAME = new DataGridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.DataPropertyName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.ToolTipText = "ชื่อ";
            EMPLNAME.Width = 100;
            EMPLNAME.ReadOnly = true;
            Dg_HRApprove.Columns.Add(EMPLNAME);

            DataGridViewTextBoxColumn CREATEDNAME = new DataGridViewTextBoxColumn();
            CREATEDNAME.Name = "CREATEDNAME";
            CREATEDNAME.DataPropertyName = "CREATEDNAME";
            CREATEDNAME.HeaderText = "ผู้สร้าง";
            CREATEDNAME.ToolTipText = "ผู้สร้าง";
            CREATEDNAME.Width = 100;
            CREATEDNAME.ReadOnly = true;
            Dg_HRApprove.Columns.Add(CREATEDNAME);


            DataGridViewTextBoxColumn MODIFIEDDATE = new DataGridViewTextBoxColumn();
            MODIFIEDDATE.Name = "MODIFIEDDATE";
            MODIFIEDDATE.DataPropertyName = "MODIFIEDDATE";
            MODIFIEDDATE.HeaderText = "แก้ไขล่าสุด";
            MODIFIEDDATE.ToolTipText = "แก้ไขล่าสุด";
            MODIFIEDDATE.Width = 100;
            MODIFIEDDATE.ReadOnly = true;
            MODIFIEDDATE.Visible = true;
            Dg_HRApprove.Columns.Add(MODIFIEDDATE);

            DataGridViewTextBoxColumn HEADAPPROVED = new DataGridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.DataPropertyName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.ToolTipText = "หน./ผช.";
            HEADAPPROVED.Width = 100;
            HEADAPPROVED.ReadOnly = true;
            Dg_HRApprove.Columns.Add(HEADAPPROVED);

            DataGridViewTextBoxColumn HEADAPPROVEDBY = new DataGridViewTextBoxColumn();
            HEADAPPROVEDBY.Name = "HEADAPPROVEDBY";
            HEADAPPROVEDBY.DataPropertyName = "HEADAPPROVEDBY";
            HEADAPPROVEDBY.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBY.ToolTipText = "ผู้อนุมัติ";
            HEADAPPROVEDBY.Width = 100;
            HEADAPPROVEDBY.ReadOnly = true;
            HEADAPPROVEDBY.Visible = false;
            Dg_HRApprove.Columns.Add(HEADAPPROVEDBY);

            DataGridViewTextBoxColumn HEADAPPROVEDBYNAME = new DataGridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.DataPropertyName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.ToolTipText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.Width = 100;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            Dg_HRApprove.Columns.Add(HEADAPPROVEDBYNAME);

            DataGridViewTextBoxColumn HRAPPROVED = new DataGridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.DataPropertyName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.ToolTipText = "บุคคล";
            HRAPPROVED.Width = 100;
            HRAPPROVED.ReadOnly = true;
            Dg_HRApprove.Columns.Add(HRAPPROVED);

            DataGridViewTextBoxColumn HRAPPROVEDBY = new DataGridViewTextBoxColumn();
            HRAPPROVEDBY.Name = "HRAPPROVEDBY";
            HRAPPROVEDBY.DataPropertyName = "HRAPPROVEDBY";
            HRAPPROVEDBY.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBY.ToolTipText = "ผู้อนุมัติ";
            HRAPPROVEDBY.Width = 100;
            HRAPPROVEDBY.ReadOnly = true;
            HRAPPROVEDBY.Visible = false;
            Dg_HRApprove.Columns.Add(HRAPPROVEDBY);

            DataGridViewTextBoxColumn HRAPPROVEDBYNAME = new DataGridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.DataPropertyName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.ToolTipText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.Width = 100;
            HRAPPROVEDBYNAME.ReadOnly = true;
            Dg_HRApprove.Columns.Add(HRAPPROVEDBYNAME);

            DataGridViewButtonColumn ApproveButton = new DataGridViewButtonColumn();
            ApproveButton.Name = "ApproveButton";
            ApproveButton.DataPropertyName = "ApproveButton";
            ApproveButton.Text = "อนุมัติ";
            ApproveButton.HeaderText = "อนุมัติ";
            ApproveButton.ToolTipText = "อนุมัติ";
            ApproveButton.Width = 100;
            ApproveButton.UseColumnTextForButtonValue = true;
            Dg_HRApprove.Columns.Add(ApproveButton);


            #endregion

            #region SetEvent

            Dg_HRApprove.CellContentClick += new DataGridViewCellEventHandler(Dg_HRApprove_CellContentClick);

            #endregion
        }

        void Dg_HRApprove_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Dg_HRApprove.Columns["ApproveButton"].Index)
            {
                using (Shift_ApproveHR_Detail frm = new Shift_ApproveHR_Detail(Dg_HRApprove.Rows[e.RowIndex].Cells["DSDOCNO"].Value.ToString()))
                {
                    frm.Text = "แผนกบุคคลอนุมัติ";

                    if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                    {
                        //โหลดข้อมูลใหม่
                        SearchData();
                        //LoadData();
                    }
                }
            }
        }

        #endregion

        #region InitializeDateTimePicker

        private void InitializeDateTimePicker()
        {
            Dtp_Start.Text = DateTime.Now.AddDays(-3).Date.ToString();
            Dtp_End.Text = DateTime.Now.Date.ToString();
        }

        #endregion

        #region Function

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
												                                                            WHERE EMPLID = '{0}' AND APPROVEID = '004'
                                                            )
                                                            ORDER BY PWDESC ", ClassCurUser.LogInEmplId);

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
                        MessageBox.Show("ไม่พบสิทธิ์ในการอนุมัติเอกสารใบเปลี่ยนกะ...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
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

        void LoadData()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_HRApprove.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"select * from SPC_CM_SHIFTHD
                                                                where DOCSTAT != 0 AND HEADAPPROVED = 1 AND HRAPPROVED = 0
                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                    where EMPLID = '{0}' AND APPROVEID = '004') ",ClassCurUser.LogInEmplId);

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
                        Dg_HRApprove.Invoke(new EventHandler(delegate
                        {
                            Dg_HRApprove.Rows.Add();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["DSDOCNO"].Value =
                                dataTable.Rows[i]["DSDOCNO"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["DOCSTAT"].Value =
                                dataTable.Rows[i]["DOCSTAT"].ToString() == "0" ? "ยกเลิก" : "รออนุมัติ";
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["EMPLNAME"].Value =
                                dataTable.Rows[i]["EMPLNAME"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HEADAPPROVED"].Value =
                                dataTable.Rows[i]["HEADAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HEADAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HEADAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBY"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HEADAPPROVEDBYNAME"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBYNAME"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HRAPPROVED"].Value =
                                dataTable.Rows[i]["HRAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HRAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HRAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HRAPPROVEDBY"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HRAPPROVEDBYNAME"].Value =
                                dataTable.Rows[i]["HRAPPROVEDBYNAME"].ToString();
                        }));
                    }

                }
                else
                {
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

        void SearchData()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_HRApprove.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;

                    if (Rb_Section.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@" select ltrim(rtrim(PWEMPLOYEE.PWFNAME)) + ' ' + ltrim(rtrim(PWEMPLOYEE.PWLNAME)) as CREATEDNAME, * from SPC_CM_SHIFTHD
                                                                left join PWEMPLOYEE on SPC_CM_SHIFTHD.createdby = PWEMPLOYEE.PWEMPLOYEE
                                                                where SECTIONID = '{0}' AND DOCDATE BETWEEN '{1}' and '{2}' AND DOCSTAT != 0 AND HEADAPPROVED = 1 AND HRAPPROVED = 0  
                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                    where EMPLID = '{3}' AND APPROVEID = '004') ", Cbb_Section.SelectedValue.ToString(), Dtp_Start.Value.Date.ToString("yyyy-MM-dd"), Dtp_End.Value.Date.ToString("yyyy-MM-dd"), ClassCurUser.LogInEmplId);

                    }
                    else if (Rb_Empl.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@" select ltrim(rtrim(PWEMPLOYEE.PWFNAME)) + ' ' + ltrim(rtrim(PWEMPLOYEE.PWLNAME)) as CREATEDNAME, * from SPC_CM_SHIFTHD
                                                                left join PWEMPLOYEE on SPC_CM_SHIFTHD.createdby = PWEMPLOYEE.PWEMPLOYEE
                                                                where EMPLID = '{0}' AND DOCDATE BETWEEN '{1}' and '{2}'  AND DOCSTAT != 0 AND HEADAPPROVED = 1 AND HRAPPROVED = 0 
                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                    where EMPLID = '{3}' AND APPROVEID = '004') ", Txt_EmplID.Text, Dtp_Start.Value.Date.ToString("yyyy-MM-dd"), Dtp_End.Value.Date.ToString("yyyy-MM-dd"), ClassCurUser.LogInEmplId);

                    }
                    else if (Rb_DocNo.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@" select ltrim(rtrim(PWEMPLOYEE.PWFNAME)) + ' ' + ltrim(rtrim(PWEMPLOYEE.PWLNAME)) as CREATEDNAME, * from SPC_CM_SHIFTHD
                                                                left join PWEMPLOYEE on SPC_CM_SHIFTHD.createdby = PWEMPLOYEE.PWEMPLOYEE
                                                                where SPC_CM_SHIFTHD.DSDOCNO = '{0}'  AND DOCSTAT != 0 AND HEADAPPROVED = 1 AND HRAPPROVED = 0 
                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                    where EMPLID = '{1}' AND APPROVEID = '004') ", Txt_DocNo.Text, ClassCurUser.LogInEmplId);
                    }
                    else
                    {
                        sqlCommand.CommandText = string.Format(@" select ltrim(rtrim(PWEMPLOYEE.PWFNAME)) + ' ' + ltrim(rtrim(PWEMPLOYEE.PWLNAME)) as CREATEDNAME, * from SPC_CM_SHIFTHD
                                                                left join PWEMPLOYEE on SPC_CM_SHIFTHD.createdby = PWEMPLOYEE.PWEMPLOYEE
                                                                where DOCSTAT != 0  AND HEADAPPROVED = 1 AND HRAPPROVED = 0 
                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                    where EMPLID = '{1}' AND APPROVEID = '004') ", Txt_DocNo.Text, ClassCurUser.LogInEmplId);
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
                        Dg_HRApprove.Invoke(new EventHandler(delegate
                        {
                            Dg_HRApprove.Rows.Add();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["DSDOCNO"].Value =
                                dataTable.Rows[i]["DSDOCNO"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["DOCSTAT"].Value =
                                dataTable.Rows[i]["DOCSTAT"].ToString() == "0" ? "ยกเลิก" : "ใช้งาน";
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["EMPLNAME"].Value =
                                dataTable.Rows[i]["EMPLNAME"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["MODIFIEDDATE"].Value =
                               dataTable.Rows[i]["MODIFIEDDATE"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["CREATEDNAME"].Value =
                                dataTable.Rows[i]["CREATEDNAME"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HEADAPPROVED"].Value =
                                dataTable.Rows[i]["HEADAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HEADAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HEADAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBY"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HEADAPPROVEDBYNAME"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBYNAME"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HRAPPROVED"].Value =
                                dataTable.Rows[i]["HRAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HRAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HRAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HRAPPROVEDBY"].ToString();
                            Dg_HRApprove.Rows[Dg_HRApprove.RowCount - 1].Cells["HRAPPROVEDBYNAME"].Value =
                                dataTable.Rows[i]["HRAPPROVEDBYNAME"].ToString();
                        }));
                    }
                }
                else
                {
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

        #endregion

    }


}
