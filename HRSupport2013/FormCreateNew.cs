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

namespace HROUTOFFICE
{
    public partial class FormCreateNew : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);
        SqlConnection conPW7;

        string empid;
        //RadioButtom
        string work = "";
        string comeback = "";
        string truck = "";
        //string tmpGenDocid = ClassDocId.runDocno("IVZ_HROUTOFFICE", "DocId", "OUT");

        string[] secureKey = new string[] { };
        //string configSecureKey = "OF-HR-W";

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

        DataTable tableLine = new DataTable("TableLine");

        public FormCreateNew()
        {

            InitializeComponent();
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;
            this.KeyPreview = true;

            this.Load += new EventHandler(Form3_Load);
            //<Windowe Form>
            this.radPanelRigth.Dock = DockStyle.Right;
            this.radPanelData.Dock = DockStyle.Fill;
            this.radPanelTop.Dock = DockStyle.Top;
            this.radPanelGridView.Dock = DockStyle.Top;
            this.radPanelBottom.Dock = DockStyle.Fill;

            #region radGridViewEmpl
            // radGridViewEmpl
            this.radGridViewEmpl.Dock = DockStyle.Fill;
            this.radGridViewEmpl.AutoGenerateColumns = true;
            this.radGridViewEmpl.EnableFiltering = false;
            this.radGridViewEmpl.AllowAddNewRow = false;
            this.radGridViewEmpl.ReadOnly = true;
            this.radGridViewEmpl.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.HeaderCells);
            this.radGridViewEmpl.MasterTemplate.ShowHeaderCellButtons = true;
            this.radGridViewEmpl.MasterTemplate.ShowFilteringRow = false;
            this.radGridViewEmpl.MasterTemplate.AutoGenerateColumns = false;
            this.radGridViewEmpl.ShowGroupedColumns = true;
            this.radGridViewEmpl.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.radGridViewEmpl.EnableHotTracking = true;
            this.radGridViewEmpl.AutoSizeRows = true;
            
            #endregion

            this.btnInsertData.Click += new EventHandler(btnInsertData_Click);

            tableLine.Columns.Add(new DataColumn("ROW", typeof(string)));
            tableLine.Columns.Add(new DataColumn("PWEMPLOYEE", typeof(string)));
            tableLine.Columns.Add(new DataColumn("PWCARD", typeof(string)));
            tableLine.Columns.Add(new DataColumn("PWFNAME", typeof(string)));
            tableLine.Columns.Add(new DataColumn("PWLNAME", typeof(string)));
            tableLine.Columns.Add(new DataColumn("SECTIONID", typeof(string)));
            tableLine.Columns.Add(new DataColumn("SECTION", typeof(string)));
            tableLine.Columns.Add(new DataColumn("DEPTID", typeof(string)));
            tableLine.Columns.Add(new DataColumn("DEPT", typeof(string)));
            tableLine.Columns.Add(new DataColumn("SHIFTID", typeof(string)));
            tableLine.Columns.Add(new DataColumn("STARTTIME", typeof(string)));
            tableLine.Columns.Add(new DataColumn("ENDTIME", typeof(string)));

            this.radtruck2.CheckedChanged += new EventHandler(radtruck2_CheckedChanged);
        }

        #region <Event Form>
        void Form3_Load(object sender, EventArgs e)
        {
            this.txtEmplId.Focus();
            this.txtEmplId.KeyDown += new KeyEventHandler(txtEmplId_KeyDown);
            this.btnInsertData.Enabled = false;
            this.radGroupBoxTypOffice.Enabled = false;
            this.radGroupBoxComeback.Enabled = false;
            this.radGroupBoxTruck.Enabled = false;
            this.radGroupBoxReason.Enabled = false;
        }
        #endregion

        #region <Event Text>        
        void txtEmplId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow newRow = tableLine.NewRow();
                //var header = radGridViewEmpl.Rows[-1];

                string pwcard = "";
                this.empid = txtEmplId.Text.ToString().Trim();
                DataTable employee = new DataTable("PWEMPLOYEE");
                employee = (DataTable)this.LookUpEmployee(txtEmplId.Text.ToString().Trim());

                if (employee != null && employee.Rows.Count > 0)
                {
                    foreach (var row in this.radGridViewEmpl.Rows)
                    {
                        if (row.Cells["PWEMPLOYEE"].Value.ToString().Trim() == this.txtEmplId.Text.Trim() ||
                            row.Cells["PWCARD"].Value.ToString().Trim() == this.txtEmplId.Text.Trim())
                        {
                            this.radGridViewEmpl.CurrentRow = row;
                            try
                            {
                                this.txtEmplId.Select(0, this.txtEmplId.Text.Length);
                            }
                            catch (Exception) { }
                            this.txtEmplId.Focus();
                            return;
                        }
                    }
                    newRow["PWEMPLOYEE"] = employee.Rows[0]["PWEMPLOYEE"].ToString().Trim();
                    newRow["PWCARD"] = employee.Rows[0]["PWCARD"].ToString().Trim();
                    newRow["PWFNAME"] = employee.Rows[0]["PWFNAME"].ToString().Trim();
                    newRow["PWLNAME"] = employee.Rows[0]["PWLNAME"].ToString().Trim();
                    newRow["SECTIONID"] = employee.Rows[0]["SECTIONID"].ToString().Trim();
                    newRow["SECTION"] = employee.Rows[0]["SECTION"].ToString().Trim();
                    newRow["DEPTID"] = employee.Rows[0]["DEPTID"].ToString().Trim();
                    newRow["DEPT"] = employee.Rows[0]["DEPT"].ToString().Trim();
                    pwcard = employee.Rows[0]["PWCARD"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(pwcard))
                {
                    DataTable pwtime2 = (DataTable)this.LookUpWorkSchedule(pwcard);
                    if (pwtime2 != null && pwtime2.Rows.Count > 0)
                    {
                        newRow["SHIFTID"] = pwtime2.Rows[0]["SHIFTID"].ToString().Trim();
                        newRow["STARTTIME"] = pwtime2.Rows[0]["STARTTIME"].ToString().Trim();
                        newRow["ENDTIME"] = pwtime2.Rows[0]["ENDTIME"].ToString().Trim();

                        tableLine.Rows.Add(newRow);
                        this.radGridViewEmpl.DataSource = tableLine;
                        try
                        {
                            this.txtEmplId.Select(0, this.txtEmplId.Text.Length);

                            this.btnInsertData.Enabled = true;
                            this.radGroupBoxTypOffice.Enabled = true;
                            this.radGroupBoxComeback.Enabled = true;
                            this.radGroupBoxTruck.Enabled = true;
                            this.radGroupBoxReason.Enabled = true;

                            this.IsSaveOnce = false;

                        }
                        catch (Exception) { }
                        this.txtEmplId.Focus();
                    }
                    else
                    {
                        var message = @"ไม่พบข้อมูลตารางกะการทำงานของพนักงาน ซึ่งจำเป็นในการบันทึกข้อมูล" + Environment.NewLine + " ติดต่อสอบถามตารางกะการทำงานได้จากเจ้าหน้าที่ฝ่ายบุคคล" + Environment.NewLine + "ดาวน์โหลดตารางกะของพนักงานคนดังกล่าว กด <yes> หากไม่ต้องการ ให้กด <no> หรือ <cancel>";
                        var res = MessageBox.Show(@message, @"การแจ้งเตือน", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                        if (res == DialogResult.Yes)
                        {
                            if (this.CopyPwtime2FromPiswin7(pwcard, DateTime.Now))
                            {
                                MessageBox.Show(string.Format(@"ดาวน์โหลดตารางกะพนักงาน <{0}> เรียบร้อย", pwcard)
                                    , "การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(string.Format(@"ไม่สามารถดาวน์โหลดตารางกะพนักงาน<{0}> ได้", pwcard)
                                    , "การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    this.txtEmplId.SelectAll();
                    this.setEnableControl(true);
                }
            }
        }       
        #endregion

        #region <Event Button>
        void btnInsertData_Click(object sender, EventArgs e)
        {
            this.InsertData();
        }
        #endregion

        #region <RadioButton>
        void radtruck2_CheckedChanged(object sender, EventArgs e)
        {
            //bool localCheck = ((RadioButton)(sender)).Checked;

            //this.txttruckid.Enabled = localCheck;
            //this.txttruckid.ReadOnly = !localCheck;
            //this.radtruck1.Checked = !localCheck;
        }
        #endregion

        #region <Event Keyboard>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                this.LicenseLookup();
            }
            else if (keyData == Keys.Escape)
            {
                if (!IsSaveOnce)
                {
                    string message = string.Format(@"มีรายการที่ยังไม่ได้บันทึก{0}ยืนยันการเปิดหน้าต่างการทำงานหากไม่ต้องการบันทึกรายการ ?", Environment.NewLine);
                    if (MessageBox.Show(message, "หน้าต่างยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region <Function and Methods>
        
        string SetQueryRun(string _idCard)
        {
            DateTime transDate = DateTime.Now;
            int year = transDate.Year;
            int month = transDate.Month;
            string idCard = _idCard;
            string fieldName = this.SetTargetFieldName(transDate.Day);

            string ret =
            string.Format(@"SELECT	PWTIME0 AS [IDCARD], {0} AS [CODE], SHIFTID, STARTTIME, ENDTIME
                                FROM	PWTIME2 WITH (NOLOCK)
                                LEFT OUTER JOIN 
                                (
	                                SELECT	DISTINCT PWTIME0 AS SHIFTID
	                                , CONVERT(NVARCHAR(10), CAST(REPLACE(CONVERT(NVARCHAR(10), PWTIMEIN1), '.', ' :' ) AS DATETIME), 108) AS STARTTIME
	                                , CONVERT(NVARCHAR(10), CAST(REPLACE(CONVERT(NVARCHAR(10), PWTIMEOUT2), '.', ' :' )  AS DATETIME), 108) AS ENDTIME
	                                FROM	PWTIME1 WITH (NOLOCK)
                                ) AS T
                                ON REPLACE(RTRIM(PWTIME2.{0}), ' ', '') = RTRIM(T.SHIFTID) collate Thai_CS_AS                    		
                                WHERE	PWTIME0 = '{1}' AND PWYEAR = {2} AND PWMONTH = {3}  
                                ", fieldName, idCard, year, month);
            return ret;
        } 
        string SetTargetFieldName(int _day)
        {
            string ret = @"";
            if (_day > 9)
            {
                ret = string.Format(@"PWTIME{0}1", _day);
            }
            else
            {
                ret = string.Format(@"PWTIME0{0}1", _day);
            }
            return ret;
        }
        DataTable LookUpWorkSchedule(string _idCard)
        {
            DataTable ret = new DataTable("PWTIME2");
            string queryRun = this.SetQueryRun(_idCard);

            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandType = CommandType.Text;
                command.CommandText = queryRun;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    ret.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                ret = null;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return ret;
        }
        DataTable LookUpEmployee(string _emplId)
        {
            DataTable ret = new DataTable("employee");
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                string sql =
                     string.Format(@"SELECT	RTRIM(PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWCARD) AS PWCARD, RTRIM(PWFNAME) AS PWFNAME , RTRIM(PWLNAME) AS PWLNAME
		                                    ,RTRIM(PWSECTION.PWSECTION)AS SECTIONID  , RTRIM(PWSECTION.PWDESC) AS SECTION,RTRIM(PWDEPT.PWDEPT)AS DEPTID , RTRIM(PWDEPT.PWDESC) AS DEPT
                                     FROM	PWEMPLOYEE WITH (NOLOCK)
	                                    LEFT OUTER JOIN PWSECTION WITH (NOLOCK) ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS
	                                    LEFT OUTER JOIN PWDEPT WITH (NOLOCK) ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT collate Thai_CS_AS
                                     WHERE	PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}'
		                                    AND PWSTATWORK LIKE '[AV]'", _emplId.Trim());

                using (SqlCommand command = new SqlCommand()
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = @sql
                })
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        ret.Load(reader);
                    }
                    else
                    {
                        MessageBox.Show("ไม่มีข้อมูล");
                    }
                }
            }
            catch (Exception ex)
            {
                ret = null;
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return ret;
        }
        bool CopyPwtime2FromPiswin7(string _idCard, DateTime _date)
        {
            bool ret = false;
            string queryRun =
                string.Format(@"SELECT	*
                                FROM	PWTIME2 WITH (NOLOCK)
                                WHERE	(PWTIME0 = '{0}') AND (PWYEAR = {1}) AND (PWMONTH = {2});", _idCard.Trim(), DateTime.Now.Year, DateTime.Now.Month);
            using (conPW7 = new SqlConnection(DatabaseConfig.ServerConStrPW7))
            {
                SqlTransaction trans = null;
                try
                {
                    conPW7.Open();
                    //trans = conPW7.BeginTransaction();
                    using (SqlCommand command = new SqlCommand()
                    {
                        Connection = conPW7,
                        CommandType = CommandType.Text,
                        CommandText = @queryRun
                    })
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable localPwtime2 = new DataTable("PWTIME2");
                            if (reader.HasRows)
                            {
                                localPwtime2.Load(reader);
                                reader.Close();

                                if (con.State == ConnectionState.Closed) con.Open();
                                trans = con.BeginTransaction();
                                using (SqlBulkCopy bulk = new SqlBulkCopy(con, SqlBulkCopyOptions.CheckConstraints, trans))
                                {
                                    bulk.DestinationTableName = @"dbo.PWTIME2";
                                    try
                                    {
                                        bulk.WriteToServer(localPwtime2);
                                        trans.Commit();
                                        ret = true;
                                    }
                                    catch (Exception)
                                    {
                                        trans.Rollback();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Err Message: " + Environment.NewLine + ex.ToString()
                        , "การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conPW7.State == ConnectionState.Open) con.Close();
                }
            }
            return ret;
        }
        void InsertData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            SqlTransaction tr = con.BeginTransaction();
            #region radio

            if (radGridViewEmpl.Rows.Count > 0)
            {
                if (radwork1.Checked == true)
                {
                    work = "1";
                }
                else
                {
                    work = "2";
                }

                if (radcomback1.Checked == true)
                {
                    comeback = "1";
                }
                else
                {
                    comeback = "2";
                }

                if (radtruck2.Checked == true && txttruckid.Text != "")
                {
                    truck = "2";

                }
                else if (radtruck2.Checked == true && txttruckid.Text == "")
                {
                    MessageBox.Show("กรุณาใส่ทะเบียนรถ");
                    this.txttruckid.Focus();
                    return;
                }
                else
                {
                    truck = "1";
                    this.txttruckid.Text = "";


                }
                // return;
            }
            #endregion
            if (txtReason.Text != "")
            {
                if (MessageBox.Show("คุณต้องการบันทึกข้อมูลเอกสารใบออกนอกออนไลน์ ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string tmpGenDocid = ClassDocId.runDocno("IVZ_HROUTOFFICE", "DocId", "OUT");

                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;
                        sqlCommand.Transaction = tr;

                        foreach (GridViewDataRowInfo row in radGridViewEmpl.Rows)
                        {
                            if (row.Index > -1)
                            {
                                sqlCommand.CommandText = string.Format(
                                @" INSERT INTO [dbo].[IVZ_HROUTOFFICE](
                                        [DocId]
                                        ,[OutOfficeId]
                                        ,[EmplId]
                                        ,[EmplCard]
                                        ,[EmplFname]
                                        ,[EmplLName]
                                        ,[DimentionId]
                                        ,[Dimention]
                                        ,[DeptId]
                                        ,[Dept]
                                        ,[ShiftId]
                                        ,[StartTime],[EndTime]
                                        ,[OutType],[CombackType],[TruckType],[TruckId]
                                        ,[CreatedBy],[CreatedName]
                                        ,[Reason] 
                                        ,Status
                                         )
                                     VALUES (
                                        @DocId{0}
                                        ,@OutOfficeId{0}
                                        ,@EmplId{0}
                                        ,@EmplCard{0}
                                        ,@EmplFname{0}
                                        ,@EmplLname{0}
                                        ,@DimentionId{0}
                                        ,@Dimention{0}
                                        ,@DeptId{0}
                                        ,@Dept{0}
                                        ,@ShiftId{0}
                                        ,@StartTime{0},@EndTime{0}
                                        ,@OutType{0},@CombackType{0},@TruckType{0},@TruckId{0}
                                        ,@CreatedBy{0},@CreatedName{0}
                                        ,@Reason{0}
                                        ,1
                                         )", row.Index
                                    //, convert(varchar, getdate(), 23)
                                    //,convert(varchar, getdate(), 23)
                                  );
                                sqlCommand.Parameters.AddWithValue(string.Format(@"DocId{0}", row.Index), tmpGenDocid);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"OutOfficeId{0}", row.Index), row.Index + 1);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"EmplId{0}", row.Index), row.Cells["PWEMPLOYEE"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"EmplCard{0}", row.Index), row.Cells["PWCARD"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"EmplFname{0}", row.Index), row.Cells["PWFNAME"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"EmplLname{0}", row.Index), row.Cells["PWLNAME"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"DimentionId{0}", row.Index), row.Cells["SECTIONID"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"Dimention{0}", row.Index), row.Cells["SECTION"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"DeptId{0}", row.Index), row.Cells["DEPTID"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"Dept{0}", row.Index), row.Cells["DEPT"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"ShiftId{0}", row.Index), row.Cells["SHIFTID"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"StartTime{0}", row.Index), row.Cells["STARTTIME"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"EndTime{0}", row.Index), row.Cells["ENDTIME"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"OutType{0}", row.Index), work);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"CombackType{0}", row.Index), comeback);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"TruckType{0}", row.Index), truck);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"TruckId{0}", row.Index), txttruckid.Text.ToString().Trim());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"CreatedBy{0}", row.Index), ClassCurUser.LogInEmplId);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"CreatedName{0}", row.Index), ClassCurUser.LogInEmplName);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"Reason{0}", row.Index), txtReason.Text.ToString());

                                sqlCommand.ExecuteNonQuery();

                                this.btnInsertData.Enabled = false;

                                this.txtEmplId.Text = "";
                                this.txtEmplId.Enabled = false;

                                this.radGroupBoxTypOffice.Enabled = false;
                                this.radGroupBoxComeback.Enabled = false;
                                this.radGroupBoxTruck.Enabled = false;
                                this.radGroupBoxReason.Enabled = false;
                            }
                        }
                        tr.Commit();
                        this.IsSaveOnce = true;
                        MessageBox.Show("การบันทึกเอกสารสำเร็จ เลขที่เอกสาร : " + tmpGenDocid + " สำเร็จ");
                    }
                    catch (Exception ex)
                    {
                        this.btnInsertData.Enabled = true;
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("โปรดระบุเหตุผลการออกนอก");
                this.txtReason.Focus();
            }
        }

        void setEnableControl(bool _isEnabled)
        {
            this.btnInsertData.Enabled = _isEnabled;
            this.radGroupBoxTypOffice.Enabled = _isEnabled;
            this.radGroupBoxComeback.Enabled = _isEnabled;
            this.radGroupBoxTruck.Enabled = _isEnabled;
            this.radGroupBoxReason.Enabled = _isEnabled;
        }
        void LicenseLookup()
        {
            //if (radtruck2.Checked == true && txttruckid.Focused)
            //{
                if (radtruck2.Checked)
            {
                //this.txttruckid.Enabled = true;
                using (FormTruckId frmTruck = new FormTruckId(this.txttruckid.Text.Trim()))
                {
                    frmTruck.StartPosition = FormStartPosition.CenterParent;
                    if (frmTruck.ShowDialog(this) == DialogResult.OK)
                    {
                        this.txttruckid.Text = frmTruck.VEHICLEID;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        #endregion

        #region <Event RadGridView>
        private void radGridViewEmpl_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (!(e.CellElement.RowElement is GridTableHeaderRowElement))
            {
                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    GridViewCommandColumn column = (GridViewCommandColumn)e.CellElement.ColumnInfo;
                    RadButtonElement button = (RadButtonElement)e.CellElement.Children[0];

                    if (column.Name == "Delete")
                    {
                        RadButtonElement element = (RadButtonElement)e.CellElement.Children[0];
                        element.DisplayStyle = DisplayStyle.Image;

                        element.Image = Properties.Resources.Close;
                        element.ImageAlignment = ContentAlignment.MiddleCenter;
                    }
                }
            }
        }
        private void radGridViewEmpl_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "Delete")
            {
                if (this.radGridViewEmpl.SelectedRows.Count > 0)
                {
                    radGridViewEmpl.Rows.RemoveAt(this.radGridViewEmpl.SelectedRows[0].Index);

                    if (this.radGridViewEmpl.SelectedRows.Count <= 0)
                    {
                        this.txtEmplId.Clear();
                        this.txtEmplId.Focus();

                        this.btnInsertData.Enabled = false;
                        this.radGroupBoxTypOffice.Enabled = false;
                        this.radGroupBoxComeback.Enabled = false;
                        this.radGroupBoxTruck.Enabled = false;
                        this.radGroupBoxReason.Enabled = false;                        
                    }
                }                 
            }
        }
        #endregion

    }
}
