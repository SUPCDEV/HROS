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
    public partial class Leave_ApproveMN : Form
    {
        string divisionid = "";
        string sectionid = "";
        string mnapproveout = "";
        string ApproveID = "";

        #region MyRegion SysEmpl

        string sysuser = "";

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

        protected string sysmnapproveout;
        public string MNApproveOut
        {
            get { return sysmnapproveout; }
            set { sysmnapproveout = value; }
        }

        protected string section;
        public string Section
        {
            get { return section; }
            set { section = value; }
        }
        protected string division;
        public string Division
        {
            get { return division; }
            set { division = value; }
        }



        #endregion

        public Leave_ApproveMN()
        {
            InitializeComponent();
            InitializeDropDownList();
            InitializeDateTimePicker();
            InitializeDataGrid();
            InitializeButton();
            InitializeForm();
        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Leave_ApproveMN_Load);

        }

        void Leave_ApproveMN_Load(object sender, EventArgs e)
        {
            this.sectionid = Section;
            this.mnapproveout = MNApproveOut;
            this.divisionid = Division;
            CheckApproveID();

            Txt_DocNo.Text = "DL" + DateTime.Now.Date.ToString("yyMMdd") + "-";
            SearchData();
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
            Dtp_Start.Text = DateTime.Now.AddDays(-3).Date.ToString();
            Dtp_End.Text = DateTime.Now.Date.ToString();
        }

        #endregion

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {
            #region SetDataGrid

            Dg_MNApprove.AutoGenerateColumns = false;
            Dg_MNApprove.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_MNApprove.Dock = DockStyle.Fill;

            DataGridViewTextBoxColumn DLDOCNO = new DataGridViewTextBoxColumn();
            DLDOCNO.Name = "DLDOCNO";
            DLDOCNO.DataPropertyName = "DLDOCNO";
            DLDOCNO.HeaderText = "เลขที่เอกสาร";
            DLDOCNO.ToolTipText = "เลขที่เอกสาร";
            DLDOCNO.Width = 100;
            DLDOCNO.ReadOnly = true;
            Dg_MNApprove.Columns.Add(DLDOCNO);

            DataGridViewTextBoxColumn DOCSTAT = new DataGridViewTextBoxColumn();
            DOCSTAT.Name = "DOCSTAT";
            DOCSTAT.DataPropertyName = "DOCSTAT";
            DOCSTAT.HeaderText = "สถานะเอกสาร";
            DOCSTAT.ToolTipText = "สถานะเอกสาร";
            DOCSTAT.Width = 100;
            DOCSTAT.ReadOnly = true;
            Dg_MNApprove.Columns.Add(DOCSTAT);

            DataGridViewTextBoxColumn EMPLNAME = new DataGridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.DataPropertyName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.ToolTipText = "ชื่อ";
            EMPLNAME.Width = 100;
            EMPLNAME.ReadOnly = true;
            Dg_MNApprove.Columns.Add(EMPLNAME);

            DataGridViewTextBoxColumn HEADAPPROVED = new DataGridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.DataPropertyName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.ToolTipText = "หน./ผช.";
            HEADAPPROVED.Width = 100;
            HEADAPPROVED.ReadOnly = true;
            Dg_MNApprove.Columns.Add(HEADAPPROVED);

            DataGridViewTextBoxColumn HEADAPPROVEDBY = new DataGridViewTextBoxColumn();
            HEADAPPROVEDBY.Name = "HEADAPPROVEDBY";
            HEADAPPROVEDBY.DataPropertyName = "HEADAPPROVEDBY";
            HEADAPPROVEDBY.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBY.ToolTipText = "ผู้อนุมัติ";
            HEADAPPROVEDBY.Width = 100;
            HEADAPPROVEDBY.ReadOnly = true;
            HEADAPPROVEDBY.Visible = false;
            Dg_MNApprove.Columns.Add(HEADAPPROVEDBY);

            DataGridViewTextBoxColumn HEADAPPROVEDBYNAME = new DataGridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.DataPropertyName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.ToolTipText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.Width = 100;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            Dg_MNApprove.Columns.Add(HEADAPPROVEDBYNAME);

            DataGridViewTextBoxColumn HRAPPROVED = new DataGridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.DataPropertyName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.ToolTipText = "บุคคล";
            HRAPPROVED.Width = 100;
            HRAPPROVED.ReadOnly = true;
            Dg_MNApprove.Columns.Add(HRAPPROVED);

            DataGridViewTextBoxColumn HRAPPROVEDBY = new DataGridViewTextBoxColumn();
            HRAPPROVEDBY.Name = "HRAPPROVEDBY";
            HRAPPROVEDBY.DataPropertyName = "HRAPPROVEDBY";
            HRAPPROVEDBY.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBY.ToolTipText = "ผู้อนุมัติ";
            HRAPPROVEDBY.Width = 100;
            HRAPPROVEDBY.ReadOnly = true;
            HRAPPROVEDBY.Visible = false;
            Dg_MNApprove.Columns.Add(HRAPPROVEDBY);

            DataGridViewTextBoxColumn HRAPPROVEDBYNAME = new DataGridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.DataPropertyName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.ToolTipText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.Width = 100;
            HRAPPROVEDBYNAME.ReadOnly = true;
            Dg_MNApprove.Columns.Add(HRAPPROVEDBYNAME);

            DataGridViewButtonColumn ApproveButton = new DataGridViewButtonColumn();
            ApproveButton.Name = "ApproveButton";
            ApproveButton.DataPropertyName = "ApproveButton";
            ApproveButton.Text = "อนุมัติ";
            ApproveButton.HeaderText = "อนุมัติ";
            ApproveButton.ToolTipText = "อนุมัติ";
            ApproveButton.Width = 100;
            ApproveButton.UseColumnTextForButtonValue = true;
            Dg_MNApprove.Columns.Add(ApproveButton);


            #endregion

            #region SetEvent

            Dg_MNApprove.CellContentClick += new DataGridViewCellEventHandler(Dg_MNApprove_CellContentClick);

            #endregion
        }

        void Dg_MNApprove_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Dg_MNApprove.Columns["ApproveButton"].Index)
            {
                using (Leave_ApproveMN_Detail frm = new Leave_ApproveMN_Detail(Dg_MNApprove.Rows[e.RowIndex].Cells["DLDOCNO"].Value.ToString()))
                {
                    frm.Text = "มินิมาร์ท อนุมัติใบลา";

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
												                                                            WHERE EMPLID = '{0}' AND APPROVEID IN ('005','006')
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

        void SearchData()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_MNApprove.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;

                    if (Rb_Section.IsChecked == true)
                    {
                        if (ApproveID == "005")
                        {
                            sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
                                                                where SECTIONID = '{0}' AND DOCDATE BETWEEN '{1}' and '{2}' AND DOCSTAT != 0 AND HEADAPPROVED = 0 
                                                                and datediff(day,isnull(SPC_CM_LEAVEHD.EXPIREDATE,getdate()),getdate())<=0
                                                                AND EMPLID <> '{3}'
                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                    where EMPLID = '{3}' AND APPROVEID = '005') ", Cbb_Section.SelectedValue.ToString(), Dtp_Start.Value.Date.ToString("yyyy-MM-dd"), Dtp_End.Value.Date.ToString("yyyy-MM-dd"), ClassCurUser.LogInEmplId);

                        }
                        else
                        {
                            sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
	                                                                where SECTIONID = '{0}' AND DOCDATE BETWEEN '{1}' and '{2}' AND DOCSTAT != 0 AND HEADAPPROVED = 0 
                                                                    and datediff(day,isnull(SPC_CM_LEAVEHD.EXPIREDATE,getdate()),getdate())<=0
	                                                                AND EMPLID <> '{3}' 
	                                                                AND EMPLID NOT IN (select EMPLID from SPC_CM_AUTHORIZE
						                                                                where APPROVEID IN ('005','006')
						                                                                )
	                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
						                                                                where EMPLID = '{3}' AND APPROVEID = '006') ", Cbb_Section.SelectedValue.ToString(), Dtp_Start.Value.Date.ToString("yyyy-MM-dd"), Dtp_End.Value.Date.ToString("yyyy-MM-dd"), ClassCurUser.LogInEmplId);
                        }

                    }
                    else if (Rb_Empl.IsChecked == true)
                    {
                        if (ApproveID == "005")
                        {
                            sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
                                                                where EMPLID = '{0}' AND DOCDATE BETWEEN '{1}' and '{2}'  AND DOCSTAT != 0 AND HEADAPPROVED = 0 
                                                                and datediff(day,isnull(SPC_CM_LEAVEHD.EXPIREDATE,getdate()),getdate())<=0 
                                                                AND EMPLID <> '{3}'
                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                    where EMPLID = '{3}' AND APPROVEID = '005') ", Txt_EmplID.Text, Dtp_Start.Value.Date.ToString("yyyy-MM-dd"), Dtp_End.Value.Date.ToString("yyyy-MM-dd"), ClassCurUser.LogInEmplId);

                        }
                        else
                        {
                            sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
                                                                where EMPLID = '{0}' AND DOCDATE BETWEEN '{1}' and '{2}'  AND DOCSTAT != 0 AND HEADAPPROVED = 0 
                                                                and datediff(day,isnull(SPC_CM_LEAVEHD.EXPIREDATE,getdate()),getdate())<=0 
                                                                AND EMPLID <> '{3}'
                                                                AND EMPLID NOT IN (select EMPLID from SPC_CM_AUTHORIZE
						                                                                where APPROVEID IN ('005','006')
						                                                                )
	                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
						                                                                where EMPLID = '{3}' AND APPROVEID = '006') ", Txt_EmplID.Text, Dtp_Start.Value.Date.ToString("yyyy-MM-dd"), Dtp_End.Value.Date.ToString("yyyy-MM-dd"), ClassCurUser.LogInEmplId);
                        }

                    }
                    else
                    {
                        if (ApproveID == "005")
                        {
                            sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
                                                            where SPC_CM_LEAVEHD.DLDOCNO = '{0}'  AND DOCSTAT != 0 AND HEADAPPROVED = 0 
                                                            and datediff(day,isnull(SPC_CM_LEAVEHD.EXPIREDATE,getdate()),getdate())<=0 
                                                            AND EMPLID <> '{1}'
                                                            AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
                                                                                where EMPLID = '{1}' AND APPROVEID = '005') ", Txt_DocNo.Text, ClassCurUser.LogInEmplId);

                        }
                        else
                        {
                            sqlCommand.CommandText = string.Format(@"select * from SPC_CM_LEAVEHD
                                                            where SPC_CM_LEAVEHD.DLDOCNO = '{0}'  AND DOCSTAT != 0 AND HEADAPPROVED = 0 
                                                            and datediff(day,isnull(SPC_CM_LEAVEHD.EXPIREDATE,getdate()),getdate())<=0
                                                            AND EMPLID <> '{1}'
                                                            AND EMPLID NOT IN (select EMPLID from SPC_CM_AUTHORIZE
						                                                                where APPROVEID IN ('005','006')
						                                                                )
	                                                                AND SECTIONID IN (select PWSECTION from SPC_CM_AUTHORIZE
						                                                                where EMPLID = '{1}' AND APPROVEID = '006') ", Txt_DocNo.Text, ClassCurUser.LogInEmplId);

                        }
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
                        Dg_MNApprove.Invoke(new EventHandler(delegate
                        {
                            Dg_MNApprove.Rows.Add();
                            Dg_MNApprove.Rows[Dg_MNApprove.RowCount - 1].Cells["DLDOCNO"].Value =
                                dataTable.Rows[i]["DLDOCNO"].ToString();
                            Dg_MNApprove.Rows[Dg_MNApprove.RowCount - 1].Cells["DOCSTAT"].Value =
                                dataTable.Rows[i]["DOCSTAT"].ToString() == "0" ? "ยกเลิก" : "ใช้งาน";
                            Dg_MNApprove.Rows[Dg_MNApprove.RowCount - 1].Cells["EMPLNAME"].Value =
                                dataTable.Rows[i]["EMPLNAME"].ToString();
                            Dg_MNApprove.Rows[Dg_MNApprove.RowCount - 1].Cells["HEADAPPROVED"].Value =
                                dataTable.Rows[i]["HEADAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HEADAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_MNApprove.Rows[Dg_MNApprove.RowCount - 1].Cells["HEADAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBY"].ToString();
                            Dg_MNApprove.Rows[Dg_MNApprove.RowCount - 1].Cells["HEADAPPROVEDBYNAME"].Value =
                                dataTable.Rows[i]["HEADAPPROVEDBYNAME"].ToString();
                            Dg_MNApprove.Rows[Dg_MNApprove.RowCount - 1].Cells["HRAPPROVED"].Value =
                                dataTable.Rows[i]["HRAPPROVED"].ToString() == "0" ? "รออนุมัติ" : dataTable.Rows[i]["HRAPPROVED"].ToString() == "1" ? "อนุมัติ" : "ไม่อนุมัติ";
                            Dg_MNApprove.Rows[Dg_MNApprove.RowCount - 1].Cells["HRAPPROVEDBY"].Value =
                                dataTable.Rows[i]["HRAPPROVEDBY"].ToString();
                            Dg_MNApprove.Rows[Dg_MNApprove.RowCount - 1].Cells["HRAPPROVEDBYNAME"].Value =
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

        void CheckApproveID()
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
                    sqlCommand.CommandText = string.Format(@" select * from SPC_CM_AUTHORIZE
                                                                where EMPLID = '{0}'
                                                                order by APPROVEID ", ClassCurUser.LogInEmplId);

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
                    ApproveID = dataTable.Rows[0]["APPROVEID"].ToString();
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
