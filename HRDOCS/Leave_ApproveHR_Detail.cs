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
    public partial class Leave_ApproveHR_Detail : Form
    {
        string DLDOCNO = "";

        public Leave_ApproveHR_Detail(string pDLDocNo)
        {
            DLDOCNO = pDLDocNo;
            InitializeComponent();
            InitializeForm();
            InitializeDataGrid();
            InitializeButton();
            InitializeDropDownList();

        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Leave_ApproveHR_Detail_Load);   
        
        }

        void Leave_ApproveHR_Detail_Load(object sender, EventArgs e)
        {
            Load_DataDL();
            Load_WorkTime();
            Load_LeavePersonalCount();
            Load_LeaveSickCount();
            Load_LeaveVacationCount();
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

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {
            #region SetDataGrid

            Dg_Leave.AutoGenerateColumns = false;
            Dg_Leave.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_Leave.Dock = DockStyle.Fill;
            Dg_Leave.Font = new Font("Segoe UI", 10);

            //DataGridViewCheckBoxColumn Chk = new DataGridViewCheckBoxColumn();
            //Chk.Name = "Chk";
            //Chk.DataPropertyName = "Chk";
            //Chk.HeaderText = "ลาหักเงิน";
            //Chk.ToolTipText = "";
            //Chk.TrueValue = 1;
            //Chk.FalseValue = 0;
            //Chk.Width = 25;
            //Dg_Leave.Columns.Add(Chk);

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
            Btn_Approve.Click += new EventHandler(Btn_Approve_Click);
            Btn_Reject.Click += new EventHandler(Btn_Reject_Click);
        }

        void Btn_Reject_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการไม่อนุมัติเอกสารนี้ ?", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            using (ConfirmReject frm = new ConfirmReject())
            {
                frm.Text = "ไม่อนุมัติ";

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
                        sqlCommand.CommandText = @"update SPC_CM_LEAVEHD set HRAPPROVED = 2 ,HRAPPROVEREMARK = @HRAPPROVEREMARK ,
                                                    HRAPPROVEDBY = @HRAPPROVEDBY ,HRAPPROVEDDATE = @HRAPPROVEDDATE 
                                                    ,HRAPPROVEDBYNAME = @HRAPPROVEDBYNAME
                                                    where SPC_CM_LEAVEHD.DLDOCNO = @DLDOCNO ";
                        sqlCommand.Transaction = INSTrans;

                        sqlCommand.Parameters.AddWithValue("@DLDOCNO", DLDOCNO);
                        sqlCommand.Parameters.AddWithValue("@HRAPPROVEREMARK", frm.Remark);
                        sqlCommand.Parameters.AddWithValue("@HRAPPROVEDBY", ClassCurUser.LogInEmplId);
                        sqlCommand.Parameters.AddWithValue("@HRAPPROVEDBYNAME", ClassCurUser.LogInEmplName);
                        sqlCommand.Parameters.AddWithValue("@HRAPPROVEDDATE", MyTime.GetDateTime());
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

        void Btn_Approve_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการอนุมัติเอกสารนี้ ?", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            DataTable dataTable = new DataTable();
            SqlTransaction INSTrans = null;
            SqlConnection sqlConnectionINS = new SqlConnection(DatabaseConfig.ServerConStr);

            INSTrans = null;
            sqlConnectionINS.Open();
            INSTrans = sqlConnectionINS.BeginTransaction();

//            foreach (DataGridViewRow row in Dg_Leave.Rows)
//            {
//                if (row.Cells["Chk"].Value.ToString() == "1")
//                {
                    
//                    #region UpdateHRApprove

//                    using (SqlCommand sqlCommand = new SqlCommand())
//                    {
//                        sqlCommand.Connection = sqlConnectionINS;
//                        sqlCommand.CommandText = @"update SPC_CM_LEAVEDT set LEAVETYPE = 4,
//                                                    LEAVETYPEDETAIL = 'ลากิจไม่รับเงิน' 
//                                                    where SPC_CM_LEAVEDT.DLDOCNO = @DLDOCNO and SPC_CM_LEAVEDT.LEAVEDATE = @LEAVEDATE ";
//                        sqlCommand.Transaction = INSTrans;

//                        sqlCommand.Parameters.AddWithValue("@DLDOCNO", DLDOCNO);
//                        sqlCommand.Parameters.AddWithValue("@LEAVEDATE", row.Cells["LEAVEDATE"].Value.ToString());
//                        sqlCommand.ExecuteNonQuery();

//                    }

//                    #endregion
//                }


//            }

            #region UpdateHRApprove

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = sqlConnectionINS;
                sqlCommand.CommandText = @"update SPC_CM_LEAVEHD set HRAPPROVED = 1,
                                                    HRAPPROVEDBY = @HRAPPROVEDBY ,HRAPPROVEDDATE = @HRAPPROVEDDATE 
                                                    ,HRAPPROVEDBYNAME = @HRAPPROVEDBYNAME
                                                    where SPC_CM_LEAVEHD.DLDOCNO = @DLDOCNO ";
                sqlCommand.Transaction = INSTrans;

                sqlCommand.Parameters.AddWithValue("@DLDOCNO", DLDOCNO);
                sqlCommand.Parameters.AddWithValue("@HRAPPROVEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@HRAPPROVEDBYNAME", ClassCurUser.LogInEmplName);
                sqlCommand.Parameters.AddWithValue("@HRAPPROVEDDATE", MyTime.GetDateTime());
                sqlCommand.ExecuteNonQuery();

            }

            #endregion

            #region Submit

            INSTrans.Commit();
            sqlConnectionINS.Close();

            #endregion

            this.DialogResult = DialogResult.Yes;
        }

        #endregion

        #region InitializeCheckBox

        private void InitializeCheckBox()
        {
            if (Chk_All.Checked)
            {
                foreach (DataGridViewRow row in Dg_Leave.Rows)
                {
                    if (!row.ReadOnly)
                    {
                        row.Cells["Chk"].Value = 1;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in Dg_Leave.Rows)
                {
                    if (!row.ReadOnly)
                    {
                        row.Cells["Chk"].Value = 0;
                    }
                }
            }
        
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
                    Txt_Remark.Text = dataTable.Rows[0]["HEADAPPROVEDREMARK"].ToString();
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

        void Load_LeaveVacationCount()
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
                    sqlCommand.CommandText = string.Format(@"select PWEVENT.PWEVENT,PWEVENT.PWDESC,
		                                                            sum(case when PWADJTIME.PWUPDTYPE = '=' then 1
			                                                            else 0.5 end) as LEAVECOUNT
                                                            from PWSTOPWORK1		
                                                            left join PWADJTIME on PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
                                                            left join PWEVENT on PWADJTIME.PWEVENT = PWEVENT.PWEVENT
                                                            where  PWADJTIME.PWEMPLOYEE = '{0}' 
		                                                            and YEAR(PWADJTIME.PWDATEADJ) = YEAR(GETDATE())
		                                                            and PWADJTIME.PWEVENT = 'F'
                                                            group by PWEVENT.PWEVENT,PWEVENT.PWDESC ", Txt_Emplid.Text);

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
                    Txt_LeaveVacationCount.Text = dataTable.Rows[0]["LEAVECOUNT"].ToString();
                }
                else
                {
                    Txt_LeaveVacationCount.Text = "0";
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

        void Load_LeavePersonalCount()
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
                    sqlCommand.CommandText = string.Format(@"select PWEVENT.PWEVENT,PWEVENT.PWDESC,
		                                                            sum(case when PWADJTIME.PWUPDTYPE = '=' then 1
			                                                            else 0.5 end) as LEAVECOUNT
                                                            from PWSTOPWORK1		
                                                            left join PWADJTIME on PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
                                                            left join PWEVENT on PWADJTIME.PWEVENT = PWEVENT.PWEVENT
                                                            where  PWADJTIME.PWEMPLOYEE = '{0}' 
		                                                            and YEAR(PWADJTIME.PWDATEADJ) = YEAR(GETDATE())
		                                                            and PWADJTIME.PWEVENT = 'E'
                                                            group by PWEVENT.PWEVENT,PWEVENT.PWDESC ", Txt_Emplid.Text);

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
                    Txt_LeavePersonalCount.Text = dataTable.Rows[0]["LEAVECOUNT"].ToString();
                }
                else
                {
                    Txt_LeavePersonalCount.Text = "0";
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

        void Load_LeaveSickCount()
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
                    sqlCommand.CommandText = string.Format(@"select PWEVENT.PWEVENT,PWEVENT.PWDESC,
		                                                            sum(case when PWADJTIME.PWUPDTYPE = '=' then 1
			                                                            else 0.5 end) as LEAVECOUNT
                                                            from PWSTOPWORK1		
                                                            left join PWADJTIME on PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
                                                            left join PWEVENT on PWADJTIME.PWEVENT = PWEVENT.PWEVENT
                                                            where  PWADJTIME.PWEMPLOYEE = '{0}' 
		                                                            and YEAR(PWADJTIME.PWDATEADJ) = YEAR(GETDATE())
		                                                            and PWADJTIME.PWEVENT = 'B'
                                                            group by PWEVENT.PWEVENT,PWEVENT.PWDESC ", Txt_Emplid.Text);

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
                    Txt_LeaveSickCount.Text = dataTable.Rows[0]["LEAVECOUNT"].ToString();
                }
                else
                {
                    Txt_LeaveSickCount.Text = "0";
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
