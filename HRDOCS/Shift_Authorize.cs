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
    public partial class Shift_Authorize : Form
    {
        string ApproveID = "";
        string ApproveName = "";

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
        protected string sysadmin;
        public string SysAdmin
        {
            get { return sysadmin; }
            set { sysadmin = value; }
        }

        public Shift_Authorize()
        {
            InitializeComponent();
            InitializeForm();
            InitializeButton();
            InitializeTextBox();
            InitializeDataGrid();
            InitializeComboBox();
        }

        #region InitializeForm

        private void InitializeForm()
        {

        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {

            Btn_Add.Click += new EventHandler(Btn_Add_Click);
            Btn_Delete.Click += new EventHandler(Btn_Delete_Click);
            Btn_New.Click += new EventHandler(Btn_New_Click);
            Btn_Save.Click += new EventHandler(Btn_Save_Click);

        }

        void Btn_Save_Click(object sender, EventArgs e)
        {
            if (Dg_Authorize.Rows.Count > 0)
            {
                SqlTransaction INSTrans = null;
                SqlConnection sqlConnectionINS = new SqlConnection(DatabaseConfig.ServerConStr);

                INSTrans = null;
                sqlConnectionINS.Open();
                INSTrans = sqlConnectionINS.BeginTransaction();

                #region Delete

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnectionINS;
                    sqlCommand.CommandText = @"delete from SPC_CM_AUTHORIZE where SPC_CM_AUTHORIZE.EMPLID = @EMPLID ";
                    sqlCommand.Transaction = INSTrans;

                    sqlCommand.Parameters.AddWithValue("@EMPLID", Lb_Emplid.Text);
                    sqlCommand.ExecuteNonQuery();

                }

                #endregion

                foreach (DataGridViewRow row in Dg_Authorize.Rows)
                {
                    #region Insert

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnectionINS;
                        sqlCommand.CommandText = @"insert into SPC_CM_AUTHORIZE(EMPLID,APPROVEID,PWSECTION,MODIFIEDBY,MODIFIEDDATE)
                                                           values(@EMPLID,@APPROVEID,@PWSECTION,@MODIFIEDBY,@MODIFIEDDATE)";
                        sqlCommand.Transaction = INSTrans;

                        sqlCommand.Parameters.AddWithValue("@EMPLID", Lb_Emplid.Text);
                        sqlCommand.Parameters.AddWithValue("@APPROVEID", row.Cells["APPROVEID"].Value.ToString());
                        sqlCommand.Parameters.AddWithValue("@PWSECTION", row.Cells["PWSECTION"].Value.ToString());
                        sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                        sqlCommand.Parameters.AddWithValue("@MODIFIEDDATE", MyTime.GetDateTime());
                        sqlCommand.ExecuteNonQuery();

                    }
                    #endregion
                }

                #region Submit

                INSTrans.Commit();

                sqlConnectionINS.Close();

                #endregion

                MessageBox.Show("บันทึกข้อมูลเสร็จสมบูรณ์ ", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();

            }
            else
            {
                MessageBox.Show("ไม่มีข้อมูลในการบันทึก", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        void Btn_New_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                var row_select = this.Dg_Authorize.SelectedRows;

                foreach (DataGridViewRow row in row_select)
                {
                    Dg_Authorize.Rows.Remove(row);
                }
                Dg_Authorize.Refresh();
            }
            catch (Exception ex)
            {

            }
        }

        void Btn_Add_Click(object sender, EventArgs e)
        {
            if (Rdb_HDApprove.IsChecked == true)
            {
                ApproveID = "001";
                ApproveName = Rdb_HDApprove.Text;
            }
            else if (Rdb_Assistant.IsChecked == true)
            {
                ApproveID = "002";
                ApproveName = Rdb_Assistant.Text;
            }
            else if (Rdb_Emp.IsChecked == true)
            {
                ApproveID = "003";
                ApproveName = Rdb_Emp.Text;
            }
            else if (Rdb_HRApprove.IsChecked == true)
            {
                ApproveID = "004";
                ApproveName = Rdb_HRApprove.Text;
            }
            else if (Rdb_MNApprove.IsChecked == true)
            {
                ApproveID = "005";
                ApproveName = Rdb_MNApprove.Text;
            }
            else if (Rdb_MNAssistant.IsChecked == true)
            {
                ApproveID = "006";
                ApproveName = Rdb_MNAssistant.Text;
            }

            if (Dg_Authorize.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in Dg_Authorize.Rows)
                {
                    if (Cbb_Section.SelectedValue.ToString() == row.Cells["PWSECTION"].Value.ToString())
                    {
                        if (ApproveID == row.Cells["APPROVEID"].Value.ToString())
                        {
                            MessageBox.Show("มีสิทธิ์นี้อยู่แล้ว...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }

            if (Lb_Emplid.Text == "")
            {
                MessageBox.Show("กรุณาใส่ข้อมูลพนักงาน...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                Dg_Authorize.Invoke(new EventHandler(delegate
                {
                    Dg_Authorize.Rows.Add();
                    Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["APPROVEID"].Value =
                        ApproveID;
                    Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["APPROVENAME"].Value =
                        ApproveName;
                    Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["PWSECTION"].Value =
                        Cbb_Section.SelectedValue.ToString();
                    Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["SECTIONNAME"].Value =
                        Cbb_Section.Text;
                }));

            }
        }

        #endregion

        #region InitializeTextBox

        private void InitializeTextBox()
        {
            Txt_EmplId.KeyDown += new KeyEventHandler(Txt_EmplId_KeyDown);
        }

        void Txt_EmplId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Check_EMP();
            }
        }


        #endregion

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {
            #region SetDataGrid

            Dg_Authorize.AutoGenerateColumns = false;
            Dg_Authorize.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_Authorize.Dock = DockStyle.Fill;
            Dg_Authorize.Font = new Font("Segoe UI", 10);

            DataGridViewTextBoxColumn APPROVEID = new DataGridViewTextBoxColumn();
            APPROVEID.Name = "APPROVEID";
            APPROVEID.DataPropertyName = "APPROVEID";
            APPROVEID.HeaderText = "รหัสสิทธิ์";
            APPROVEID.ToolTipText = "รหัสสิทธิ์";
            APPROVEID.Width = 100;
            APPROVEID.ReadOnly = true;
            APPROVEID.Visible = false;
            Dg_Authorize.Columns.Add(APPROVEID);

            DataGridViewTextBoxColumn PWSECTION = new DataGridViewTextBoxColumn();
            PWSECTION.Name = "PWSECTION";
            PWSECTION.DataPropertyName = "PWSECTION";
            PWSECTION.HeaderText = "รหัสแผนก";
            PWSECTION.ToolTipText = "รหัสแผนก";
            PWSECTION.Width = 100;
            PWSECTION.ReadOnly = true;
            //PWSECTION.Visible = false;
            Dg_Authorize.Columns.Add(PWSECTION);

            DataGridViewTextBoxColumn SECTIONNAME = new DataGridViewTextBoxColumn();
            SECTIONNAME.Name = "SECTIONNAME";
            SECTIONNAME.DataPropertyName = "SECTIONNAME";
            SECTIONNAME.HeaderText = "แผนก";
            SECTIONNAME.ToolTipText = "แผนก";
            SECTIONNAME.Width = 100;
            SECTIONNAME.ReadOnly = true;
            Dg_Authorize.Columns.Add(SECTIONNAME);

            DataGridViewTextBoxColumn APPROVENAME = new DataGridViewTextBoxColumn();
            APPROVENAME.Name = "APPROVENAME";
            APPROVENAME.DataPropertyName = "APPROVENAME";
            APPROVENAME.HeaderText = "ระดับสิทธิ์";
            APPROVENAME.ToolTipText = "ระดับสิทธิ์";
            APPROVENAME.Width = 100;
            APPROVENAME.ReadOnly = true;
            Dg_Authorize.Columns.Add(APPROVENAME);

            #endregion
        }

        #endregion

        #region InitializeComboBox

        private void InitializeComboBox()
        {
            Load_Section();
        }

        #endregion

        #region Function

        void Clear()
        {
            Txt_EmplId.Text = "";
            Lb_Emplid.Text = "";
            Lb_FullName.Text = "";
            Lb_Section.Text = "";
            Lb_Dept.Text = "";
            Rdb_HDApprove.IsChecked = true;
            Dg_Authorize.Rows.Clear();
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
                    sqlCommand.CommandText = string.Format(@"SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC 
                                                            FROM PWSECTION                          
                                                            where PWSECTION not in ('99','00')
                                                            ORDER BY PWDESC ");

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

        void Check_EMP()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_Authorize.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"SELECT	RTRIM(PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWCARD) AS PWCARD, RTRIM(PWFNAME) AS PWFNAME , RTRIM(PWLNAME) AS PWLNAME
		                                    ,RTRIM(PWSECTION.PWSECTION)AS SECTIONID  , RTRIM(PWSECTION.PWDESC) AS SECTION,RTRIM(PWDEPT.PWDEPT)AS DEPTID , RTRIM(PWDEPT.PWDESC) AS DEPT
                                     FROM	PWEMPLOYEE WITH (NOLOCK)
	                                    LEFT OUTER JOIN PWSECTION WITH (NOLOCK) ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS
	                                    LEFT OUTER JOIN PWDEPT WITH (NOLOCK) ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT collate Thai_CS_AS
                                     WHERE	(PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
		                                    AND PWSTATWORK LIKE '[AV]' ", Txt_EmplId.Text);

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
                    Txt_EmplId.Text = dataTable.Rows[0]["PWEMPLOYEE"].ToString();
                    Lb_Emplid.Text = dataTable.Rows[0]["PWEMPLOYEE"].ToString();
                    Lb_FullName.Text = dataTable.Rows[0]["PWFNAME"].ToString() + "  " + dataTable.Rows[0]["PWLNAME"].ToString();
                    Lb_Section.Text = dataTable.Rows[0]["SECTION"].ToString();
                    Lb_Dept.Text = dataTable.Rows[0]["DEPT"].ToString();
                    Txt_EmplId.Text.ToUpper();
                    Cbb_Section.Focus();
                    Load_Authorize();
                }
                else
                {

                    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Clear();
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

        void Load_Authorize()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_Authorize.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"select * from SPC_CM_AUTHORIZE
                                                                left join PWSECTION on SPC_CM_AUTHORIZE.PWSECTION = PWSECTION.PWSECTION
                                                                where EMPLID = '{0}' ", Txt_EmplId.Text);

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
                        Dg_Authorize.Invoke(new EventHandler(delegate
                        {
                            Dg_Authorize.Rows.Add();
                            Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["APPROVEID"].Value =
                                dataTable.Rows[i]["APPROVEID"].ToString();

                            if (dataTable.Rows[i]["APPROVEID"].ToString() == "001")
                            {
                                Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["APPROVENAME"].Value = Rdb_HDApprove.Text;
                            }
                            else if (dataTable.Rows[i]["APPROVEID"].ToString() == "002")
                            {
                                Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["APPROVENAME"].Value = Rdb_Assistant.Text;
                            }
                            else if (dataTable.Rows[i]["APPROVEID"].ToString() == "003")
                            {
                                Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["APPROVENAME"].Value = Rdb_Emp.Text;
                            }
                            else if (dataTable.Rows[i]["APPROVEID"].ToString() == "004")
                            {
                                Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["APPROVENAME"].Value = Rdb_HRApprove.Text;
                            }
                            else if (dataTable.Rows[i]["APPROVEID"].ToString() == "005")
                            {
                                Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["APPROVENAME"].Value = Rdb_MNApprove.Text;
                            }
                            else if (dataTable.Rows[i]["APPROVEID"].ToString() == "006")
                            {
                                Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["APPROVENAME"].Value = Rdb_MNAssistant.Text;
                            }

                            Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["PWSECTION"].Value =
                                dataTable.Rows[i]["PWSECTION"].ToString();
                            Dg_Authorize.Rows[Dg_Authorize.RowCount - 1].Cells["SECTIONNAME"].Value =
                                dataTable.Rows[i]["PWDESC"].ToString();
                        }));
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
                Cursor.Current = Cursors.Default;
            }

        }

        #endregion



    }
}
