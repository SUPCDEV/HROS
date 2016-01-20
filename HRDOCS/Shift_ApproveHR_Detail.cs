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
    public partial class Shift_ApproveHR_Detail : Form
    {
        string DSDOCNO;

        public Shift_ApproveHR_Detail()
        {
            InitializeComponent();
            InitializeForm();
            InitializeDataGridView();
            InitializeButton();
            InitializeDropDownList();
        }


        public Shift_ApproveHR_Detail(string _pDSDocNo)
            : this()
        {
            DSDOCNO = _pDSDocNo;

        }

        #region InitializeForm

        private void InitializeForm()
        {
            //Load_DataDS();
            this.Load += new EventHandler(Shift_ApproveHR_Detail_Load);
            this.Shown += new EventHandler(Shift_ApproveHR_Detail_Shown);
        }

        void Shift_ApproveHR_Detail_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0} DocNumber: {1}", this.Text, DSDOCNO.ToString());
        }

        void Shift_ApproveHR_Detail_Shown(object sender, EventArgs e)
        {
            this.Load_DataDS();
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
                        sqlCommand.CommandText = @"update SPC_CM_SHIFTHD set HRAPPROVED = 2 ,HRAPPORVEREMARK = @HRAPPORVEREMARK ,
                                                    HRAPPROVEDBY = @HRAPPROVEDBY ,HRAPPROVEDDATE = @HRAPPROVEDDATE 
                                                    ,HRAPPROVEDBYNAME = @HRAPPROVEDBYNAME
                                                    where SPC_CM_SHIFTHD.DSDOCNO = @DSDOCNO ";
                        sqlCommand.Transaction = INSTrans;

                        sqlCommand.Parameters.AddWithValue("@DSDOCNO", DSDOCNO);
                        sqlCommand.Parameters.AddWithValue("@HRAPPORVEREMARK", frm.Remark);
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

            #region UpdateModified

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = sqlConnectionINS;
                sqlCommand.CommandText = @"update SPC_CM_SHIFTHD set HRAPPROVED = 1, HRAPPORVEREMARK = @HRAPPORVEREMARK ,
                                                    HRAPPROVEDBY = @HRAPPROVEDBY ,HRAPPROVEDDATE = @HRAPPROVEDDATE 
                                                    ,HRAPPROVEDBYNAME = @HRAPPROVEDBYNAME
                                                    where SPC_CM_SHIFTHD.DSDOCNO = @DSDOCNO ";
                sqlCommand.Transaction = INSTrans;

                sqlCommand.Parameters.AddWithValue("@DSDOCNO", DSDOCNO);
                sqlCommand.Parameters.AddWithValue("@HRAPPORVEREMARK", Txt_Remark.Text);
                sqlCommand.Parameters.AddWithValue("@HRAPPROVEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@HRAPPROVEDBYNAME", ClassCurUser.LogInEmplName);
                sqlCommand.Parameters.AddWithValue("@HRAPPROVEDDATE", MyTime.GetDateTime());
                sqlCommand.ExecuteNonQuery();
            }

            #endregion

            #region Submit
            // Mod by WS on 2015-11-04 : Start

            // -- Call create backup HROS.PWTIME2 records and update records of HROS.PWTIME2 --
            // -- Pass parameter by DSDOCNO to make linked/relate table and records backup. --
            try
            {
                if (this.SPC_Pwtime2UpdateAndCreateTemp(DSDOCNO, ref sqlConnectionINS, ref INSTrans))
                {
                    if (INSTrans != null)
                        INSTrans.Commit(); // Commit ShiftHD when update HROS.PWTIME2 records successful.
                    else
                    {
                        MessageBox.Show("ไม่ปรับปรุงเอกสารได้สำเร็จ เนื่อง Transaction หมดอายุ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }
            catch (Exception ws_ex)
            {
                if (INSTrans != null)
                    INSTrans.Rollback();

                MessageBox.Show(string.Format("เกิดข้อผิดพลาด\nข้อความ: {0}", ws_ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            sqlConnectionINS.Close();


            // Mod by WS on 2015-11-04 : End
            #endregion

            this.DialogResult = DialogResult.Yes;
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
            SHIFTDATE.Name = "SHIFTDATE2";
            SHIFTDATE.DataPropertyName = "SHIFTDATE2";
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
                    Txt_Remark.Text = dataTable.Rows[0]["HEADAPPROVEDREMARK"].ToString();
                    Ddl_Rest1.SelectedIndex = Convert.ToInt16(dataTable.Rows[0]["RESTDAY1"].ToString());
                    Ddl_Rest2.SelectedIndex = Convert.ToInt16(dataTable.Rows[0]["RESTDAY2"].ToString());


                    this.Dg_Shift.DataSource = dataTable;

                    //int cnt = Dg_Shift.Rows.Count;

                    //for (int i = 0; i < dataTable.Rows.Count; i++)
                    //{
                    //    Dg_Shift.Invoke(new EventHandler(delegate
                    //    {
                    //        Dg_Shift.Rows.Add();
                    //        Dg_Shift.Rows[i].Cells["SHIFTDATE"].Value =
                    //            dataTable.Rows[i]["SHIFTDATE"].ToString();
                    //        Dg_Shift.Rows[i].Cells["FROMSHIFTID"].Value =
                    //            dataTable.Rows[i]["FROMSHIFTID"].ToString();
                    //        Dg_Shift.Rows[i].Cells["TOSHIFTID"].Value =
                    //            dataTable.Rows[i]["TOSHIFTID"].ToString();
                    //        Dg_Shift.Rows[i].Cells["REFOTTIME"].Value =
                    //            dataTable.Rows[i]["REFOTTIME"].ToString();
                    //        Dg_Shift.Rows[i].Cells["REMARK"].Value =
                    //            dataTable.Rows[i]["REMARK"].ToString();
                    //    }));
                    //}

                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            //if (keyData == Keys.Control)
            //{
            //    if (keyData == Keys.P)
            //    {
            //        using (Shift_ApproveHR_Detail frm = new Shift_ApproveHR_Detail())
            //        {
            //            frm.Text = "แผนกบุคคลอนุมัติ";

            //            if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
            //            {
            //                //โหลดข้อมูลใหม่
            //                //SearchData();
            //                //LoadData();
            //            }
            //        }
            //    }
            //}
            //else if (keyData == Keys.Escape)
            //{
            //    this.Close();
            //}

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        // Mod by WS on 2015-10-29: Start
        #region <Functions by WS>
        DataTable GenerateSPCPwtime2TempSchema()
        {
            DataTable spc_pwtime2temp = new DataTable("SPC_PWTIME2TEMP");
            //spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("{0}", ""), typeof(System.String)));
            spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("{0}", "REFDOCNO"), typeof(System.String)));
            spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("{0}", "PWYEAR"), typeof(System.String)));
            spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("{0}", "PWMONTH"), typeof(System.String)));
            spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("{0}", "PWTIME0"), typeof(System.String)));
            spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("{0}", "ISCANCEL"), typeof(int)));

            for (var i = 1; i <= 31; i++)
            {
                if (i < 10)
                {
                    spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("PWTIME0{0}1", i), typeof(System.String)));
                    spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("PWTIME0{0}2", i), typeof(System.String)));
                    spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("PWTIME0{0}3", i), typeof(System.String)));
                }
                else
                {
                    spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("PWTIME{0}1", i), typeof(System.String)));
                    spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("PWTIME{0}2", i), typeof(System.String)));
                    spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("PWTIME{0}3", i), typeof(System.String)));
                }
            }
            spc_pwtime2temp.Columns.Add(new DataColumn(string.Format("{0}", "PWLASTUPDATE"), typeof(System.String)));

            // Set primary keys
            DataColumn[] keys = new DataColumn[] { spc_pwtime2temp.Columns[0], spc_pwtime2temp.Columns[1], spc_pwtime2temp.Columns[2] };
            spc_pwtime2temp.PrimaryKey = keys;

            return spc_pwtime2temp;
        }
        bool SPC_Pwtime2UpdateAndCreateTemp(string _dsDocNo, ref SqlConnection _con, ref SqlTransaction _trans)
        {
            bool ret = false;
            DateTime minDate, maxDate;
            int year1, year2, month1, month2;
            DataTable tableLines = new DataTable("SPC_CM_SHIFTDT");

            StringBuilder sbUpdate = new StringBuilder(string.Empty);
            string emplTime0 = string.Empty;

            int rowAffBackup = 0;
            int rowAffUpdate = 0;

            int commitBackup = 0;
            int commitUpdate = 0;
            ;

            year1 = year2 = month1 = 0;
            month2 = month1;

            string query =
                string.Format(
                                @"
                                SELECT	DSDOCNO, MIN(SHIFTDATE) AS MINDATE, MAX(SHIFTDATE) AS MAXDATE
                                FROM	SPC_CM_SHIFTDT WITH(NOLOCK)
                                WHERE	DSDOCNO = N'{0}'
                                GROUP BY DSDOCNO
                                OPTION	(FAST 47);
                                    
                                SELECT  *   FROM	SPC_CM_SHIFTHD WITH(NOLOCK)
                                WHERE	DSDOCNO = N'{0}' OPTION	(FAST 47);

                                SELECT	*
                                FROM	SPC_CM_SHIFTDT WITH(NOLOCK)
                                WHERE	DSDOCNO = N'{0}'
                                ORDER BY SHIFTDATE ASC
                                OPTION	(FAST 47);", _dsDocNo);

            if (_con.State == ConnectionState.Open && _trans != null)
            {
                try
                {

                    using (SqlCommand command = new SqlCommand() { Connection = _con, Transaction = _trans, CommandText = query, CommandType = CommandType.Text })
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (!reader.HasRows)
                            return ret; ;

                        while (reader.Read())
                        {
                            minDate = Convert.ToDateTime(reader["MINDATE"].ToString());
                            maxDate = Convert.ToDateTime(reader["MAXDATE"].ToString());

                            year1 = minDate.Year;
                            month1 = minDate.Month;

                            year2 = maxDate.Year;
                            month2 = maxDate.Month;
                        }

                        if (reader.NextResult())
                        {
                            if (!reader.HasRows)
                                return ret;

                            while (reader.Read())
                            {
                                //var tableHd = new DataTable();
                                //tableHd.Load(reader);
                                var emplid = reader["EMPLID"].ToString().Trim();
                                emplTime0 = new string(emplid.Where(r => char.IsDigit(r)).ToArray());
                            }
                        }

                        if (reader.NextResult())
                        {
                            if (!reader.HasRows)
                                return ret;

                            tableLines.Load(reader);

                            var selectedRow = (from t in tableLines.AsEnumerable()
                                               where t.Field<DateTime>("SHIFTDATE").Year == year1 && t.Field<DateTime>("SHIFTDATE").Month == month1
                                               select t).ToArray<DataRow>();

                            //DataRow[] selectedRow = tableLines.Select(string.Format(@"YEAR(SHIFTDATE) = {0} AND MONTH(SHIFTDATE) = {1}", year1, month1));
                            if (selectedRow == null || selectedRow.Count() < 1)
                                return ret;

                            sbUpdate.AppendFormat(@"{0}", this.SPC_SetQueryRunUpdate(selectedRow, emplTime0));

                            commitBackup += 1;
                            commitUpdate += 1;

                            if (month1 != month2)
                            {
                                //DataRow[] selectedRow2 = tableLines.Select(string.Format(@"YEAR(SHIFTDATE) = {0} AND MONTH(SHIFTDATE) = {1}", year2, month2));
                                var selectedRow2 = (from t in tableLines.AsEnumerable()
                                                    where t.Field<DateTime>("SHIFTDATE").Year == year2 && t.Field<DateTime>("SHIFTDATE").Month == month2
                                                    select t).ToArray<DataRow>();
                                if (selectedRow2 == null || selectedRow2.Count() < 1)
                                    return ret;

                                sbUpdate.AppendFormat(@"{0}", this.SPC_SetQueryRunUpdate(selectedRow2, emplTime0));

                                commitBackup += 1;
                                commitUpdate += 1;
                            }
                        }

                        if ((reader != null)) reader.Close();

                        // Set sql procedure name, 4 parameters
                        // 1. DSDocNo 2. PWTIME 3. Year 4. Month
                        var queryRun =
                            string.Format(@"SPC_HROS_CREATEPWTIME2BACKUP"); //SPC_HROS_CREATEPWTIME2BACKUP

                        //trans = localCon.BeginTransaction();

                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = _con;
                        cmd1.Transaction = _trans;
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.CommandText = queryRun;

                        cmd1.Parameters.AddWithValue(@"DSDOCNO", DSDOCNO);
                        cmd1.Parameters.AddWithValue(@"PWTIME0", emplTime0);
                        cmd1.Parameters.AddWithValue(@"YEAR", year1);
                        cmd1.Parameters.AddWithValue(@"MONTH", month1);
                        cmd1.Parameters.Add(@"ROWAFF", SqlDbType.Int); // return value
                        cmd1.Parameters[@"ROWAFF"].Direction = ParameterDirection.Output;
                        cmd1.ExecuteNonQuery();
                        rowAffBackup += int.Parse(cmd1.Parameters[@"ROWAFF"].Value.ToString());

                        if (month1 != month2)
                        {
                            SqlCommand cmd2 = new SqlCommand();

                            cmd2.Connection = _con;
                            cmd2.Transaction = _trans;
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.CommandText = queryRun;

                            cmd2.Parameters.AddWithValue(@"DSDOCNO", DSDOCNO);
                            cmd2.Parameters.AddWithValue(@"PWTIME0", emplTime0);
                            cmd2.Parameters.AddWithValue(@"YEAR", year2);
                            cmd2.Parameters.AddWithValue(@"MONTH", month2);
                            cmd2.Parameters.Add(@"ROWAFF", SqlDbType.Int); // return value
                            cmd2.Parameters[@"ROWAFF"].Direction = ParameterDirection.Output;
                            cmd2.ExecuteNonQuery();
                            rowAffBackup += int.Parse(cmd2.Parameters[@"ROWAFF"].Value.ToString());
                        }

                        SqlCommand cmd3 = new SqlCommand();

                        cmd3.Connection = _con;
                        cmd3.Transaction = _trans;
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = sbUpdate.ToString();
                        rowAffUpdate = cmd3.ExecuteNonQuery();

                        ret = (commitBackup == rowAffBackup) && (commitUpdate == rowAffUpdate);
                    }
                }
                catch (Exception ex)
                {
                    ret = false;
                    throw new Exception(string.Format("Error: ขณะสร้างการสำรองบรรทัดและปรับปรุงบรรทัดตารางกะการทำงาน.\nข้อความ: {0}", ex.Message));
                }
            }

            //using (SqlConnection localCon = new SqlConnection(DatabaseConfig.ServerConStr))
            //{
            //    SqlTransaction trans = null;
            //    try
            //    {
            //        localCon.Open();


            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(ex.Message);
            //    }
            //    finally
            //    {
            //        if (localCon.State == ConnectionState.Open) localCon.Close();
            //    }
            //}

            return ret;
        }
        string SPC_SetQueryRunUpdate(DataRow[] _rows, string _emplPwtime0)
        {
            string ret = string.Empty;
            DateTime lineDate = new DateTime(1901, 1, 1);
            StringBuilder sb = new StringBuilder();

            if (_rows.Count() > 0)
            {
                sb.AppendFormat(@"{0} ", "UPDATE PWTIME2 SET");
                for (var rowIdx = 0; rowIdx < _rows.Count(); rowIdx++)
                {
                    lineDate = _rows[rowIdx].Field<DateTime>("SHIFTDATE").Date;
                    if (rowIdx == 0)
                        sb.AppendFormat(@"{0} = N'{1}'", this.SPC_SetFieldNameByDay(lineDate.Day), _rows[rowIdx].Field<string>("TOSHIFTID").Trim());
                    else
                        sb.AppendFormat(@", {0} = N'{1}'", this.SPC_SetFieldNameByDay(lineDate.Day), _rows[rowIdx].Field<string>("TOSHIFTID").Trim());
                }
                sb.AppendFormat(@" WHERE PWTIME0 = N'{0}' AND PWYEAR = {1} AND PWMONTH = {2};", _emplPwtime0, lineDate.Year, lineDate.Month);
            }

            ret = sb.ToString();
            return ret;
        }
        string SPC_SetFieldNameByDate(DateTime _date)
        {
            return this.SPC_SetFieldNameByDay(_date.Day);
        }
        string SPC_SetFieldNameByDay(int _day)
        {
            return (_day < 10) ? string.Format(@"PWTIME0{0}1", _day) : string.Format(@"PWTIME{0}1", _day);
        }
        #endregion
        // Mod by WS on 2015-10-29: End

    }
}