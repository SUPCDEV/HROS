using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Telerik.WinControls.Primitives;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using System.IO;
using System.Threading;
using SysApp;

namespace HRDOCS
{
    public partial class Chd_Create : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string ApproveID = "";

        string emplcard = "";
        string sectionid = "";
        string deptid = "";
        string positionid = "";
        string position = "";

        //<Neung>
        string[] secureKey = new string[] { };

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
        //
        public Chd_Create()
        {
            InitializeComponent();

            InitializeradGridview();
            InitializeDateTimePicker();
            InitializeDropDownLise();

            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btn_AddData.Click += new EventHandler(btn_AddData_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnNewDoc.Click += new EventHandler(btnNewDoc_Click);
            this.btn_Sentmail.Click += new EventHandler(btn_Sentmail_Click);

            this.txtEmpId.ReadOnly = false;

            this.CheckApproveID();
            this.GetdataEmpl();
        }

        private void Chd_Create_Load(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
        }
        private void InitializeradGridview()
        {

            rgv_changholiday.Dock = DockStyle.Fill;
            rgv_changholiday.AutoGenerateColumns = true;
            rgv_changholiday.EnableFiltering = false;
            rgv_changholiday.AllowAddNewRow = false;
            rgv_changholiday.MasterTemplate.AutoGenerateColumns = false;
            rgv_changholiday.ShowGroupedColumns = true;
            rgv_changholiday.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgv_changholiday.EnableHotTracking = true;
            rgv_changholiday.AutoSizeRows = true;

            GridViewTextBoxColumn FROMHOLIDAY = new GridViewTextBoxColumn();
            FROMHOLIDAY.Name = "FROMHOLIDAY";
            FROMHOLIDAY.FieldName = "FROMHOLIDAY";
            FROMHOLIDAY.HeaderText = "วันที่มาทำ";
            FROMHOLIDAY.IsVisible = true;
            FROMHOLIDAY.ReadOnly = true;
            FROMHOLIDAY.BestFit();
            rgv_changholiday.Columns.Add(FROMHOLIDAY);

            GridViewTextBoxColumn TOSHIFTID = new GridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.FieldName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "รหัสกะ";
            TOSHIFTID.IsVisible = true;
            TOSHIFTID.ReadOnly = true;
            TOSHIFTID.BestFit();
            rgv_changholiday.Columns.Add(TOSHIFTID);

            GridViewTextBoxColumn TOSHIFTDESC = new GridViewTextBoxColumn();
            TOSHIFTDESC.Name = "TOSHIFTDESC";
            TOSHIFTDESC.FieldName = "TOSHIFTDESC";
            TOSHIFTDESC.HeaderText = "คำอธิบาย";
            TOSHIFTDESC.IsVisible = true;
            TOSHIFTDESC.ReadOnly = true;
            TOSHIFTDESC.BestFit();
            rgv_changholiday.Columns.Add(TOSHIFTDESC);

            GridViewTextBoxColumn TOHOLIDAY = new GridViewTextBoxColumn();
            TOHOLIDAY.Name = "TOHOLIDAY";
            TOHOLIDAY.FieldName = "TOHOLIDAY";
            TOHOLIDAY.HeaderText = "วันที่หยุด";
            TOHOLIDAY.IsVisible = true;
            TOHOLIDAY.ReadOnly = true;
            TOHOLIDAY.BestFit();
            rgv_changholiday.Columns.Add(TOHOLIDAY);

            //GridViewTextBoxColumn HOLIDAY1 = new GridViewTextBoxColumn();
            //HOLIDAY1.Name = "HOLIDAY1";
            //HOLIDAY1.FieldName = "HOLIDAY1";
            //HOLIDAY1.HeaderText = "วันหยุด(1)";
            //HOLIDAY1.IsVisible = true;
            //HOLIDAY1.ReadOnly = true;
            //HOLIDAY1.BestFit();
            //rgv_changholiday.Columns.Add(HOLIDAY1);

            //GridViewTextBoxColumn HOLIDAY2 = new GridViewTextBoxColumn();
            //HOLIDAY2.Name = "HOLIDAY2";
            //HOLIDAY2.FieldName = "HOLIDAY2";
            //HOLIDAY2.HeaderText = "วันหยุด(2)";
            //HOLIDAY2.IsVisible = true;
            //HOLIDAY2.ReadOnly = true;
            //HOLIDAY2.BestFit();
            //rgv_changholiday.Columns.Add(HOLIDAY2);

            GridViewTextBoxColumn REASON = new GridViewTextBoxColumn();
            REASON.Name = "REASON";
            REASON.FieldName = "REASON";
            REASON.HeaderText = "เหตุผล";
            REASON.IsVisible = true;
            REASON.ReadOnly = true;
            REASON.BestFit();
            rgv_changholiday.Columns.Add(REASON);

            GridViewCommandColumn BTDELETE = new GridViewCommandColumn();
            BTDELETE.Name = "BTDELETE";
            BTDELETE.HeaderText = "ลบ";
            BTDELETE.Width = 20;
            rgv_changholiday.Columns.Add(BTDELETE);

        }
        private void InitializeDateTimePicker()
        {
            this.dtFromHoliday.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dtToHolidat.Text = DateTime.Now.AddDays(-3).Date.ToString("yyyy-MM-dd");
            this.dtToHolidat.MinDate = DateTime.Now.Date.AddDays(-3);
            this.dtToHolidat.MaxDate = DateTime.Now.Date.AddDays(+10);

        }
        private void InitializeDropDownList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RestIndex", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("RestName", typeof(System.String)));
            dt.Rows.Add(0, "อาทิตย์");
            dt.Rows.Add(1, "จันทร์");
            dt.Rows.Add(2, "อังคาร");
            dt.Rows.Add(3, "พุธ");
            dt.Rows.Add(4, "พฤหัสบดี");
            dt.Rows.Add(5, "ศุกร์");
            dt.Rows.Add(6, "เสาร์");

            ddlholiday1.DataSource = dt;
            ddlholiday1.DisplayMember = "RestName";
            ddlholiday1.ValueMember = "RestIndex";

            DataTable dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("RestIndex", typeof(System.Int32)));
            dt2.Columns.Add(new DataColumn("RestName", typeof(System.String)));
            dt2.Rows.Add(0, "อาทิตย์");
            dt2.Rows.Add(1, "จันทร์");
            dt2.Rows.Add(2, "อังคาร");
            dt2.Rows.Add(3, "พุธ");
            dt2.Rows.Add(4, "พฤหัสบดี");
            dt2.Rows.Add(5, "ศุกร์");
            dt2.Rows.Add(6, "เสาร์");

            ddlholiday2.DataSource = dt2;
            ddlholiday2.DisplayMember = "RestName";
            ddlholiday2.ValueMember = "RestIndex";

            //Ddl_Rest1.SelectedIndex = 0;

        }
        private void InitializeDropDownLise()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("HolidayIndex", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("HolidayName", typeof(System.String)));
            dt.Rows.Add(0, "อาทิตย์");
            dt.Rows.Add(1, "จันทร์");
            dt.Rows.Add(2, "อังคาร");
            dt.Rows.Add(3, "พุธ");
            dt.Rows.Add(4, "พฤหัสบดี");
            dt.Rows.Add(5, "ศุกร์");
            dt.Rows.Add(6, "เสาร์");

            ddlholiday1.DataSource = dt;
            ddlholiday1.DisplayMember = "HolidayName";
            ddlholiday1.ValueMember = "HolidayIndex";

            DataTable dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("HolidayIndex", typeof(System.Int32)));
            dt2.Columns.Add(new DataColumn("HolidayName", typeof(System.String)));
            dt2.Rows.Add(0, "อาทิตย์");
            dt2.Rows.Add(1, "จันทร์");
            dt2.Rows.Add(2, "อังคาร");
            dt2.Rows.Add(3, "พุธ");
            dt2.Rows.Add(4, "พฤหัสบดี");
            dt2.Rows.Add(5, "ศุกร์");
            dt2.Rows.Add(6, "เสาร์");

            ddlholiday2.DataSource = dt2;
            ddlholiday2.DisplayMember = "HolidayName";
            ddlholiday2.ValueMember = "HolidayIndex";

            //Ddl_Rest1.SelectedIndex = 0;

        }

        #region MyRegion Buttom

        void btnNewDoc_Click(object sender, EventArgs e)
        {
            GetdataEmpl();

            txtShift.Text = "";
            txtShiftDesc.Text = "";
            txtreason.Text = "";
            rgv_changholiday.Rows.Clear();

            this.btnSave.Enabled = false;           

        }
        void btnSave_Click(object sender, EventArgs e)
        {
            this.InsertData();
        }
        void btn_AddData_Click(object sender, EventArgs e)
        {
            var _fromdate = dtFromHoliday.Value.Date.ToString("yyyy-MM-dd");
            var _todate = dtToHolidat.Value.Date.ToString("yyyy-MM-dd");
            
            if (_todate != "" && _todate == _fromdate && rdbOpen.ToggleState == ToggleState.On)
            {
                MessageBox.Show("โปรดตรวจสอบวันที่ต้องการเปลี่ยนวันหยุดอีกครั้ง","แจ้งเตือน");
                return;
            }
            
            //เช็ควันซ้ำ
            //if (rgv_changholiday.Rows.Count > 0 && rdbOpen.ToggleState == ToggleState.On) //ถ้า GriewView เป็นค่าว่าง && เลือก rdbOpen
            //{
            //    for (int i = 0; i <= rgv_changholiday.Rows.Count - 1; i++)
            //    {
            //        if (_fromdate == rgv_changholiday.Rows[i].Cells["FROMHOLIDAY"].Value.ToString())
            //        {
            //            MessageBox.Show("คุณได้เลือกวันที่มาทำงานเป็นวันที่ " + dtFromHoliday.Value.Date.ToString("yyyy-MM-dd") + " ไปแล้วโปรดเปลี่ยนเป็นวันอื่น", "แจ้งเตือน");
            //            return;
            //        }
            //        if (_todate == rgv_changholiday.Rows[i].Cells["TOHOLIDAY"].Value.ToString()) // ถ้าวันที่ที่เปลี่ยนและวันที่มาทำเป็นวันเดียวกัน
            //        {
            //            MessageBox.Show("คุณได้เปลี่ยนวันที่ " + dtToHolidat.Value.Date.ToString("yyyy-MM-dd") + " เป็นวันหยุดไปแล้ว โปรดเปลี่ยนเป็นวันอื่น", "แจ้งเตือน");
            //            return;
            //        }
            //    }
            //}

            if (txtShift.Text == "" || txtShiftDesc.Text == "" || txtreason.Text == "")
            {
                MessageBox.Show("กรุณาใส่ข้อมูลให้ครบ...", "แจ้งเตือน");
            }
            
            else if (rdbClose.ToggleState == ToggleState.On)
            {
                this.rgv_changholiday.Rows.Add(dtFromHoliday.Value.Date.ToString("yyyy-MM-dd"), txtShift.Text, txtShiftDesc.Text, dtToHolidat.Value.Date.ToString("0000-00-00")
                     , txtreason.Text);

                rgv_changholiday.Invoke(new EventHandler(delegate
                {
                    this.txtShift.Text = "";
                    this.txtShiftDesc.Text = "";
                    this.txtreason.Text = "";
                }));

                this.btnSave.Enabled = true;
            }

            else
            {
                this.rgv_changholiday.Rows.Add(dtFromHoliday.Value.Date.ToString("yyyy-MM-dd"), txtShift.Text, txtShiftDesc.Text, dtToHolidat.Value.Date.ToString("yyyy-MM-dd")
                    , txtreason.Text);

                rgv_changholiday.Invoke(new EventHandler(delegate
                {
                    this.txtShift.Text = "";
                    this.txtShiftDesc.Text = "";
                    this.txtreason.Text = "";
                }));

                this.btnSave.Enabled = true;
            }
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            using (Chd_SelectShift frm = new Chd_SelectShift())
            {
                frm.Text = "ค้นหากะ";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {
                    txtShift.Text = frm.Shift;
                    txtShiftDesc.Text = frm.ShiftDesc;
                }
            }
            //}
        }

        // <Not Use>
        void btnNewDoc_Click_1(object sender, EventArgs e)
        {
            txtShift.Text = "";
            txtShiftDesc.Text = "";
            txtreason.Text = "";
            rgv_changholiday.Rows.Clear();

            rdbOpen.ToggleState = ToggleState.On;
            dtToHolidat.Enabled = true;
            //btnSearch.Enabled = true;
        }
        void btn_Sentmail_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region  Function
        void CheckApproveID()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                Cursor.Current = Cursors.WaitCursor;

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@" SELECT * FROM SPC_CM_AUTHORIZE
                                                              WHERE EMPLID = '{0}' 
                                                              AND APPROVEID in ('001','002','005','006')
                                                              ORDER BY APPROVEID ", ClassCurUser.LogInEmplId);
                    //, ClassCurUser.LogInEmplId);
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
                else
                {
                    string sqlemp =
                        string.Format(@"
                    SELECT	RTRIM(PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWCARD) AS PWCARD, RTRIM(PWFNAME) AS PWFNAME , RTRIM(PWLNAME) AS PWLNAME ,RTRIM(PWLNAME) AS PWLNAME 
                                            ,(RTRIM(PWEMPLOYEE.PWFNAME) +'  '+ RTRIM(PWEMPLOYEE.PWLNAME)) AS FULLNAME
		                                    ,RTRIM(PWSECTION.PWSECTION)AS SECTIONID  , RTRIM(PWSECTION.PWDESC) AS SECTION
                                            ,RTRIM(PWDEPT.PWDEPT)AS DEPTID , RTRIM(PWDEPT.PWDESC) AS DEPT
                                            ,RTRIM(PWEMPLOYEE.PWPOSITION) AS POSITIONID ,RTRIM(PWPOSITION.PWDESC) AS POSITION
                                     FROM	PWEMPLOYEE WITH (NOLOCK)
	                                    LEFT OUTER JOIN PWSECTION WITH (NOLOCK) ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS
	                                    LEFT OUTER JOIN PWDEPT WITH (NOLOCK) ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT collate Thai_CS_AS
                                        LEFT OUTER JOIN PWPOSITION  WITH (NOLOCK) ON PWEMPLOYEE.PWPOSITION = PWPOSITION.PWPOSITION collate Thai_CS_AS
                                     WHERE	PWSTATWORK LIKE '[AV]'
                                            AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
											 ", ClassCurUser.LogInEmplId);

                    //AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')  

                    SqlCommand cmd = new SqlCommand(sqlemp, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtEmpId.Text = reader["PWEMPLOYEE"].ToString();
                            emplcard = reader["PWCARD"].ToString();
                            txtEmplName.Text = reader["FULLNAME"].ToString().Trim();

                            deptid = reader["DEPTID"].ToString().Trim();
                            txtDept.Text = reader["DEPT"].ToString().Trim();

                            sectionid = reader["SECTIONID"].ToString();
                            txtSection.Text = reader["SECTION"].ToString();

                            positionid = reader["POSITIONID"].ToString();
                            position = reader["POSITION"].ToString();
                        }
                    }
                #endregion

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                if (con.State == ConnectionState.Open) con.Close();
            }

        }
        
        private void GetdataEmpl()
        {
            if (con.State == ConnectionState.Open) con.Close();
                con.Open();
            try
            {
                if (ApproveID == "001" || ApproveID == "002" || ApproveID == "005" || ApproveID == "006")
                {
                    this.txtEmpId.ReadOnly = false;
                    string sqlemp =
                    string.Format(@"
                    SELECT	RTRIM(PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWCARD) AS PWCARD, RTRIM(PWFNAME) AS PWFNAME , RTRIM(PWLNAME) AS PWLNAME ,RTRIM(PWLNAME) AS PWLNAME 
                                            ,(RTRIM(PWEMPLOYEE.PWFNAME) +'  '+ RTRIM(PWEMPLOYEE.PWLNAME)) AS FULLNAME
		                                    ,RTRIM(PWSECTION.PWSECTION)AS SECTIONID  , RTRIM(PWSECTION.PWDESC) AS SECTION
                                            ,RTRIM(PWDEPT.PWDEPT)AS DEPTID , RTRIM(PWDEPT.PWDESC) AS DEPT
                                            ,RTRIM(PWEMPLOYEE.PWPOSITION) AS POSITIONID ,RTRIM(PWPOSITION.PWDESC) AS POSITION
                                     FROM	PWEMPLOYEE WITH (NOLOCK)
	                                    LEFT OUTER JOIN PWSECTION WITH (NOLOCK) ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS
	                                    LEFT OUTER JOIN PWDEPT WITH (NOLOCK) ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT collate Thai_CS_AS
                                        LEFT OUTER JOIN PWPOSITION  WITH (NOLOCK) ON PWEMPLOYEE.PWPOSITION = PWPOSITION.PWPOSITION collate Thai_CS_AS
                                     WHERE	PWSTATWORK LIKE '[AV]'
                                            AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
											AND PWEMPLOYEE.PWSECTION IN ( 
                                                                          SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                                          WHERE EMPLID = '{0}' 
                                                                        ) ", ClassCurUser.LogInEmplId);

                    //AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')  

                    SqlCommand cmd = new SqlCommand(sqlemp, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtEmpId.Text = reader["PWEMPLOYEE"].ToString();
                            emplcard = reader["PWCARD"].ToString();
                            txtEmplName.Text = reader["FULLNAME"].ToString().Trim();

                            deptid = reader["DEPTID"].ToString().Trim();
                            txtDept.Text = reader["DEPT"].ToString().Trim();

                            sectionid = reader["SECTIONID"].ToString();
                            txtSection.Text = reader["SECTION"].ToString();

                            positionid = reader["POSITIONID"].ToString();
                            position = reader["POSITION"].ToString();

                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูล โปรดตรวจสอบสิทธิ์ในการจัดการกับเอกสารของพนักงานอีกครั้ง");
                    }
                }

                //พนักงาน
                if (ApproveID == "003" || ApproveID == "007")
                {
                    this.txtEmpId.ReadOnly = true;

                    string sqlemp =
                    string.Format(@"
                    SELECT	RTRIM(PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWCARD) AS PWCARD, RTRIM(PWFNAME) AS PWFNAME , RTRIM(PWLNAME) AS PWLNAME ,RTRIM(PWLNAME) AS PWLNAME 
                                            ,(RTRIM(PWEMPLOYEE.PWFNAME) +'  '+ RTRIM(PWEMPLOYEE.PWLNAME)) AS FULLNAME
		                                    ,RTRIM(PWSECTION.PWSECTION)AS SECTIONID  , RTRIM(PWSECTION.PWDESC) AS SECTION
                                            ,RTRIM(PWDEPT.PWDEPT)AS DEPTID , RTRIM(PWDEPT.PWDESC) AS DEPT
                                            ,RTRIM(PWEMPLOYEE.PWPOSITION) AS POSITIONID ,RTRIM(PWPOSITION.PWDESC) AS POSITION
                                     FROM	PWEMPLOYEE WITH (NOLOCK)
	                                    LEFT OUTER JOIN PWSECTION WITH (NOLOCK) ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS
	                                    LEFT OUTER JOIN PWDEPT WITH (NOLOCK) ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT collate Thai_CS_AS
                                        LEFT OUTER JOIN PWPOSITION  WITH (NOLOCK) ON PWEMPLOYEE.PWPOSITION = PWPOSITION.PWPOSITION collate Thai_CS_AS
                                     WHERE	PWSTATWORK LIKE '[AV]'
		                                    AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
											AND PWEMPLOYEE.PWSECTION IN ( 
                                                                          SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                                          WHERE EMPLID = '{0}' 
                                                                        ) ", ClassCurUser.LogInEmplId);

                    //AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')  

                    SqlCommand cmd = new SqlCommand(sqlemp, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtEmpId.Text = reader["PWEMPLOYEE"].ToString();
                            emplcard = reader["PWCARD"].ToString();
                            txtEmplName.Text = reader["FULLNAME"].ToString().Trim();

                            deptid = reader["DEPTID"].ToString().Trim();
                            txtDept.Text = reader["DEPT"].ToString().Trim();

                            sectionid = reader["SECTIONID"].ToString();
                            txtSection.Text = reader["SECTION"].ToString();

                            positionid = reader["POSITIONID"].ToString();
                            position = reader["POSITION"].ToString();

                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูล โปรดตรวจสอบสิทธิ์ในการจัดการกับเอกสารของพนักงานอีกครั้ง");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        private void InsertData()
        {
            #region sql_insert

            int rowAffectedMs = 0;
            int rowAffectedDt = 0;

            if (MessageBox.Show("คุณต้องการบันทึกเอกสาร ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            SqlTransaction tr = con.BeginTransaction();
            string tmpGenDocId = ClassDocId.runDocno("SPC_JN_CHANGHOLIDAYHD", "DOCID", "CHD");
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            sqlCommand.Transaction = tr;

            try
            {
                #region sql_HD
                sqlCommand.CommandText = string.Format(
                                            @"INSERT INTO SPC_JN_CHANGHOLIDAYHD (
                                                DOCID,TRANSDATE
                                                ,EMPLID,EMPLCARD,EMPLNAME
                                                ,SECTIONID,SECTIONNAME
                                                ,DEPTID,DEPTNAME
                                                ,POSITIONID,POSITIONNAME  
                                                ,HOLIDAY1,HOLIDAY2                                           
                                                ,CREATEDBY,CREATEDNAME,CREATEDDATE
                                                ,MODIFIEDBY,MODIFIEDNAME,MODIFIEDDATE
                                            )                                            
                                        VALUES (
                                                @DOCID1,convert(varchar, getdate(), 23)
                                                ,@EMPLID,@EMPLCARD,@EMPLNAME
                                                ,@SECTIONID,@SECTIONNAME
                                                ,@DEPTID,@DEPTNAME
                                                ,@POSITIONID,@POSITIONNAME 
                                                ,@HOLIDAY1,@HOLIDAY2                                                
                                                ,@CREATEDBY,@CREATEDNAME,convert(varchar, getdate(), 23)
                                                ,@MODIFIEDBY,@MODIFIEDNAME,convert(varchar, getdate(), 23) )");

                sqlCommand.Parameters.AddWithValue("@DOCID1", tmpGenDocId);
                sqlCommand.Parameters.AddWithValue("@EMPLID", txtEmpId.Text);
                sqlCommand.Parameters.AddWithValue("@EMPLCARD", emplcard);
                sqlCommand.Parameters.AddWithValue("@EMPLNAME", txtEmplName.Text);
                sqlCommand.Parameters.AddWithValue("@SECTIONID", sectionid);
                sqlCommand.Parameters.AddWithValue("@SECTIONNAME", txtSection.Text);
                sqlCommand.Parameters.AddWithValue("@DEPTID", deptid);
                sqlCommand.Parameters.AddWithValue("@DEPTNAME", txtDept.Text);

                sqlCommand.Parameters.AddWithValue("@POSITIONID", positionid);
                sqlCommand.Parameters.AddWithValue("@POSITIONNAME", position);

                sqlCommand.Parameters.AddWithValue("@HOLIDAY1", ddlholiday1.Text);
                sqlCommand.Parameters.AddWithValue("@HOLIDAY2", ddlholiday2.Text);

                sqlCommand.Parameters.AddWithValue("@CREATEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@CREATEDNAME", ClassCurUser.LogInEmplName);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDNAME", ClassCurUser.LogInEmplName);

                // <WS:Modified> 2014-11-13
                //sqlCommand.ExecuteNonQuery();
                rowAffectedMs = sqlCommand.ExecuteNonQuery();
                // </WS:Modified>
                #endregion

                #region sql_DT
                foreach (GridViewDataRowInfo row in rgv_changholiday.Rows)
                {
                    if (row.Index > -1)
                    {
                        sqlCommand.CommandText = string.Format(
                                            @"INSERT INTO [dbo].[SPC_JN_CHANGHOLIDAYDT](
                                                             DOCID , COUNTDOC ,FROMHOLIDAY
                                                            ,TOHOLIDAY ,TOSHIFTID ,TOSHIFTDESC ,REASON                                               
                                                            ,MODIFIEDDATE
                                                            )
                                                     VALUES (
                                                             @DOCID2{0},@COUNTDOC{0},@FROMHOLIDAY{0}
                                                            ,@TOHOLIDAY{0} ,@TOSHIFTID{0} ,@TOSHIFTDESC{0} ,@REASON{0}
                                                            ,convert(varchar, getdate(), 23)
                                                            )"
                                            , row.Index);

                        sqlCommand.Parameters.AddWithValue(string.Format(@"DOCID2{0}", row.Index), tmpGenDocId);
                        sqlCommand.Parameters.AddWithValue(string.Format(@"COUNTDOC{0}", row.Index), row.Index + 1);
                        sqlCommand.Parameters.AddWithValue(string.Format(@"FROMHOLIDAY{0}", row.Index), row.Cells["FROMHOLIDAY"].Value.ToString());
                        sqlCommand.Parameters.AddWithValue(string.Format(@"TOHOLIDAY{0}", row.Index), row.Cells["TOHOLIDAY"].Value.ToString());
                        sqlCommand.Parameters.AddWithValue(string.Format(@"TOSHIFTID{0}", row.Index), row.Cells["TOSHIFTID"].Value.ToString());
                        sqlCommand.Parameters.AddWithValue(string.Format(@"REASON{0}", row.Index), row.Cells["REASON"].Value.ToString());
                        sqlCommand.Parameters.AddWithValue(string.Format(@"TOSHIFTDESC{0}", row.Index), row.Cells["TOSHIFTDESC"].Value.ToString());

                        // <WS:modified> 2014-11-13
                        //sqlCommand.ExecuteNonQuery();
                        rowAffectedDt += sqlCommand.ExecuteNonQuery();
                        // </WS:modified> 2014-11-13
                    }

                }
                #endregion

                // <WS:Modified> 2014-11-13
                if ((rowAffectedMs > 0) && (rowAffectedDt > 0))
                {
                    tr.Commit();
                    this.IsSaveOnce = true;
                    MessageBox.Show("การบันทึกเอกสารสำเร็จ เลขที่เอกสาร : " + tmpGenDocId + " สำเร็จ");

                    rgv_changholiday.Rows.Clear();

                }
                else
                {
                    MessageBox.Show("การบันทึกเอกสารสำเร็จ เลขที่เอกสาร : " + tmpGenDocId + " ไม่สำเร็จ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                // </WS:Modified> 2014-11-13
            }

            catch (Exception ex)
            {
                MessageBox.Show(String.Format(@"เกิดข้ิผิดพลาด:{0}{1}", Environment.NewLine, ex.Message), @"การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            #endregion
        }
        #endregion

        #region GridView Formatting
        private void rgv_changholiday_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (!(e.CellElement.RowElement is GridTableHeaderRowElement))
            {
                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    GridViewCommandColumn column = (GridViewCommandColumn)e.CellElement.ColumnInfo;
                    RadButtonElement button = (RadButtonElement)e.CellElement.Children[0];

                    if (column.Name == "BTDELETE")
                    {
                        RadButtonElement element = (RadButtonElement)e.CellElement.Children[0];
                        element.DisplayStyle = DisplayStyle.Image;

                        element.Image = Properties.Resources.Close;
                        element.ImageAlignment = ContentAlignment.MiddleCenter;

                    }
                }
            }
        }
        private void rgv_changholiday_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "BTDELETE")
            {
                if (this.rgv_changholiday.SelectedRows.Count > 0)
                {
                    rgv_changholiday.Rows.RemoveAt(this.rgv_changholiday.SelectedRows[0].Index);
                }
            }
        }
        #endregion

        #region  Evenclick

        private void rdbOpen_Click(object sender, EventArgs e)
        {
            dtToHolidat.Enabled = true;
        }

        private void rdbClose_Click(object sender, EventArgs e)
        {
            dtToHolidat.Enabled = false;
        }

        #endregion

        #region EnvenKeyPress

        private void HrCreatEmpl()
        {
            
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                if (ApproveID == "001" || ApproveID == "002" || ApproveID == "005" || ApproveID == "006")
                {
                    this.txtEmpId.ReadOnly = false;

                    string sqlemp =
                    string.Format(@"
                    SELECT	RTRIM(PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWCARD) AS PWCARD, RTRIM(PWFNAME) AS PWFNAME , RTRIM(PWLNAME) AS PWLNAME ,RTRIM(PWLNAME) AS PWLNAME 
                                            ,(RTRIM(PWEMPLOYEE.PWFNAME) +'  '+ RTRIM(PWEMPLOYEE.PWLNAME)) AS FULLNAME
		                                    ,RTRIM(PWSECTION.PWSECTION)AS SECTIONID  , RTRIM(PWSECTION.PWDESC) AS SECTION
                                            ,RTRIM(PWDEPT.PWDEPT)AS DEPTID , RTRIM(PWDEPT.PWDESC) AS DEPT
                                            ,RTRIM(PWEMPLOYEE.PWPOSITION) AS POSITIONID ,RTRIM(PWPOSITION.PWDESC) AS POSITION
                                     FROM	PWEMPLOYEE WITH (NOLOCK)
	                                    LEFT OUTER JOIN PWSECTION WITH (NOLOCK) ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS
	                                    LEFT OUTER JOIN PWDEPT WITH (NOLOCK) ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT collate Thai_CS_AS
                                        LEFT OUTER JOIN PWPOSITION  WITH (NOLOCK) ON PWEMPLOYEE.PWPOSITION = PWPOSITION.PWPOSITION collate Thai_CS_AS
                                     WHERE	PWSTATWORK LIKE '[AV]'
                                            AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
											AND PWEMPLOYEE.PWSECTION IN 
                                                                        ( SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                                          WHERE EMPLID = '{1}' ) "
                                                                        ,txtEmpId.Text.Trim()                   
                                                                        , ClassCurUser.LogInEmplId);

                    //AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')  

                    SqlCommand cmd = new SqlCommand(sqlemp, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtEmpId.Text = reader["PWEMPLOYEE"].ToString();
                            emplcard = reader["PWCARD"].ToString();
                            txtEmplName.Text = reader["FULLNAME"].ToString().Trim();

                            deptid = reader["DEPTID"].ToString().Trim();
                            txtDept.Text = reader["DEPT"].ToString().Trim();

                            sectionid = reader["SECTIONID"].ToString();
                            txtSection.Text = reader["SECTION"].ToString();

                            positionid = reader["POSITIONID"].ToString();
                            position = reader["POSITION"].ToString();

                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูล โปรดตรวจสอบสิทธิ์ในการจัดการกับเอกสารของพนักงานอีกครั้ง");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        private void txtEmpId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                HrCreatEmpl();
            }
        }

        #endregion

        private void btn_CheckHist_Click(object sender, EventArgs e)
        {
            string Empid = txtEmpId.Text;
            using (Chd_CheckHist frm = new Chd_CheckHist(Empid))
            {
                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Yes;
                }
            }
        }
    }
}
