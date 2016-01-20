using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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


namespace HROUTOFFICE
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand command;
        SqlDataReader reader;
        //DataSet dataset;

        protected string queryRun = "";
        protected string commandRun = "";

        public Form2()
        {
            InitializeComponent();
            // <Create objects>

            #region <Window Form>
            this.Text = string.Format(@"Usr:{0}; {1}", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "รายงานการวิเคราะห์จำนวนพนักงาน(วันปัจจุบัน)");
            this.KeyPreview = true;
            this.ShowIcon = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Shown += new EventHandler(Form2_Shown);
            #endregion

            #region <layout>
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(196, 206, 221);
            this.toolStripContainer1.Dock = DockStyle.Top;
            this.panelRight.Dock = DockStyle.Right;
            this.panelContainer.Dock = DockStyle.Fill;
            this.panelContTop.Dock = DockStyle.Top;
            this.panelContButtom.Dock = DockStyle.Fill;
            this.groupBoxGeneral.Dock = DockStyle.Fill;
            #endregion

            #region <DateTimePicker>
            this.dateTimePickerTransdate.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerTransdate.CustomFormat = @"dd/MM/yyyy";
            this.dateTimePickerTransdate.Value = DateTime.Now;
            this.dateTimePickerTransdate.MinDate = DateTime.Now.AddDays(-5);
            this.dateTimePickerTransdate.MaxDate = DateTime.Now;
            #endregion

            #region <RadGridView>
            this.radGridReport.Dock = DockStyle.Fill;
            this.radGridReport.AutoGenerateColumns = true;
            this.radGridReport.EnableFiltering = true;
            this.radGridReport.AllowAddNewRow = false;
            this.radGridReport.ReadOnly = true;
            this.radGridReport.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.HeaderCells);
            this.radGridReport.MasterTemplate.ShowHeaderCellButtons = true;
            this.radGridReport.MasterTemplate.ShowFilteringRow = false;
            this.radGridReport.MasterTemplate.AutoGenerateColumns = false;
            this.radGridReport.ShowGroupedColumns = true;
            this.radGridReport.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;

            GridViewTextBoxColumn txtCol0 = new GridViewTextBoxColumn();
            txtCol0.FieldName = "PWDATE";
            txtCol0.Name = "PWDATE";
            txtCol0.HeaderText = "วันที่";
            txtCol0.FormatString = "{0:dd/MM/yyyy}";
            txtCol0.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol0);

            GridViewTextBoxColumn txtCol1 = new GridViewTextBoxColumn();
            txtCol1.FieldName = "PWRDATE";
            txtCol1.Name = "PWRDATE";
            txtCol1.HeaderText = "วันที่(2)";
            txtCol1.FormatString = "{0:dd/MM/yyyy}";
            txtCol1.IsVisible = false;
            this.radGridReport.Columns.Add(txtCol1);

            GridViewTextBoxColumn txtCol2 = new GridViewTextBoxColumn();
            txtCol2.FieldName = "PWEMPLOYEE";
            txtCol2.Name = "PWEMPLOYEE";
            txtCol2.HeaderText = "พนักงาน";
            txtCol2.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol2);

            GridViewTextBoxColumn txtCol3 = new GridViewTextBoxColumn();
            txtCol3.FieldName = "PWCARD";
            txtCol3.Name = "PWCARD";
            txtCol3.HeaderText = "บัตร";
            txtCol3.IsVisible = false;
            this.radGridReport.Columns.Add(txtCol3);

            GridViewTextBoxColumn txtCol4 = new GridViewTextBoxColumn();
            txtCol4.FieldName = "PWFNAME";
            txtCol4.Name = "PWFNAME";
            txtCol4.HeaderText = "ชื่อ";
            txtCol4.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol4);

            GridViewTextBoxColumn txtCol5 = new GridViewTextBoxColumn();
            txtCol5.FieldName = "PWLNAME";
            txtCol5.Name = "PWLNAME";
            txtCol5.HeaderText = "สกุล";
            txtCol5.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol5);
            ////
            GridViewTextBoxColumn txtCol6 = new GridViewTextBoxColumn();
            txtCol6.FieldName = "PWSECTION";
            txtCol6.Name = "PWSECTION";
            txtCol6.HeaderText = "แผนก";
            txtCol6.IsVisible = false;
            this.radGridReport.Columns.Add(txtCol6);

            GridViewTextBoxColumn txtCol7 = new GridViewTextBoxColumn();
            txtCol7.FieldName = "SECTIONDESC";
            txtCol7.Name = "SECTIONDESC";
            txtCol7.HeaderText = "ชื่อแผนก";
            txtCol7.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol7);

            GridViewTextBoxColumn txtCol8 = new GridViewTextBoxColumn();
            txtCol8.FieldName = "PWDEPT";
            txtCol8.Name = "PWDEPT";
            txtCol8.HeaderText = "ตำแหน่ง";
            txtCol8.IsVisible = false;
            this.radGridReport.Columns.Add(txtCol8);

            GridViewTextBoxColumn txtCol9 = new GridViewTextBoxColumn();
            txtCol9.FieldName = "DEPTDESC";
            txtCol9.Name = "DEPTDESC";
            txtCol9.HeaderText = "ชื่อตำแหน่ง";
            txtCol9.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol9);

            GridViewTextBoxColumn txtCol10 = new GridViewTextBoxColumn();
            txtCol10.FieldName = "MINTIME";
            txtCol10.Name = "MINTIME";
            txtCol10.HeaderText = "เวลารูดต่ำสุด";
            txtCol10.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol10);

            GridViewTextBoxColumn txtCol12 = new GridViewTextBoxColumn();
            txtCol12.FieldName = "MAXTIME";
            txtCol12.Name = "MAXTIME";
            txtCol12.HeaderText = "เวลารูดสูงสุด";
            txtCol12.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol12);

            GridViewTextBoxColumn txtCol13 = new GridViewTextBoxColumn();
            txtCol13.FieldName = "IDCARD";
            txtCol13.Name = "IDCARD";
            txtCol13.HeaderText = "บัตร";
            txtCol13.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol13);

            GridViewTextBoxColumn txtCol14 = new GridViewTextBoxColumn();
            txtCol14.FieldName = "CODE";
            txtCol14.Name = "CODE";
            txtCol14.HeaderText = "รหัสกะ";
            txtCol14.IsVisible = false;
            this.radGridReport.Columns.Add(txtCol14);

            GridViewTextBoxColumn txtCol141 = new GridViewTextBoxColumn();
            txtCol141.FieldName = "SHIFTID";
            txtCol141.Name = "SHIFTID";
            txtCol141.HeaderText = "รหัสกะ(2)";
            txtCol141.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol141);

            GridViewTextBoxColumn txtCol15 = new GridViewTextBoxColumn();
            txtCol15.FieldName = "STARTTIME";
            txtCol15.Name = "STARTTIME";
            txtCol15.HeaderText = "เวลาเริ่มต้น";
            txtCol15.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol15);

            GridViewTextBoxColumn txtCol16 = new GridViewTextBoxColumn();
            txtCol16.FieldName = "ENDTIME";
            txtCol16.Name = "ENDTIME";
            txtCol16.HeaderText = "เวลาสิ้นสุด";
            txtCol16.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol16);

            GridViewTextBoxColumn txtCol17 = new GridViewTextBoxColumn();
            txtCol17.FieldName = "CHECKING";
            txtCol17.Name = "CHECKING";
            txtCol17.HeaderText = "การตรวจสอบ";
            txtCol17.IsVisible = true;
            this.radGridReport.Columns.Add(txtCol17);
            #endregion

            #region <Button>
            this.btnFuncProcess.Click += new EventHandler(btnFuncProcess_Click);
            this.btnViewTimeTemp.Click += new EventHandler(btnViewTimeTemp_Click);
            #endregion

            #region <MenuItem>
            this.toolStripMenuFile.Visible = false;
            this.toolStripBtnExportExcel.ToolTipText = "Export to Excel (Ctrl+Alt+X)";
            this.toolStripBtnExportExcel.Click += new EventHandler(toolStripBtnExportExcel_Click);
            #endregion

            #region <StatusLabel>
            this.statusLabel.Text = "ready.";
            #endregion
        }

        #region Window Form
        void Form2_Shown(object sender, EventArgs e)
        {
            //startprogress.Start();
            this.radGridReport.DataSource = new string[] { };
            //this.radGridReport.ShowColumnChooser(this.radGridReport.MasterTemplate);
        }
        #endregion

        // <Overide methode force combine multikey in textbox>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F5))
            {
                this.FuncProcess();                
            }
            else if (keyData == Keys.Escape)
            {
                var activeWin = HROUTOFFICE.Form1.ActiveForm;
                activeWin.Close();
            }
            else if ((keyData == Keys.ControlKey) && (keyData == Keys.L))
            {
                this.FuncViewTimeTemp();               
            }
            else if ((keyData == Keys.ControlKey) && (keyData == Keys.Alt) && (keyData == Keys.X))
            {
                this.FuncExportExcel();              
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Button
        void btnFuncProcess_Click(object sender, EventArgs e)
        {
            this.FuncProcess();
        }
        public delegate void updatebar();
        private void UpdateProgress()
        {
            // progressBar1.Value += 1;

            // Here we are just updating a label to show the progressbar value
            statusLabel.Text = DateTime.Now.ToString(); //Convert.ToString(Convert.ToInt64(label1.Text) + 1);
        }
        void btnDateTime_Click(object sender, EventArgs e)
        {
            var message = string.Format(@"{0}||{1}||{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), DateTime.Now.Millisecond, DateTime.Now.ToFileTimeUtc());
            RadMessageBox.SetThemeName(this.radGridReport.ThemeName);
            RadMessageBox.Show(this, message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Info);

        }
        void btnViewTimeTemp_Click(object sender, EventArgs e)
        {
            this.FuncViewTimeTemp();
        }
        #endregion

        #region MenuItems
        void toolStripBtnExportExcel_Click(object sender, EventArgs e)
        {
            this.FuncExportExcel();
        }
        #endregion

        #region <Functions and Methods>
        void SetParameter()
        {

        }
        string SetTargetField(int _day)
        {
            string ret = "";
            if (_day < 10)
            {
                ret = string.Format("PWTIME0{0}1", _day);
            }
            else
            {
                ret = string.Format("PWTIME{0}1", _day);
            }
            return ret;
        }
        string SetQueryRun()
        {
            String target = this.SetTargetField(this.dateTimePickerTransdate.Value.Day);

            #region Old QueryRun            
            string ret1 =
                @"SELECT	*
                    FROM	(
                    SELECT	
	                    PWDATE, PWRDATE, PWTIMETEMP.PWEMPLOYEE, PWTRANTYPE
	                    , PWEMPLOYEE.PWCARD, RTRIM(PWFNAME) AS PWFNAME, RTRIM(PWLNAME) AS PWLNAME, PWEMPLOYEE.PWSECTION, PWSECTION.PWDESC AS SECTIONDESC, PWEMPLOYEE.PWDEPT, PWDEPT.PWDESC AS DEPTDESC
	                    , MIN(PWTIME) AS MINTIME, MAX(PWTIME) AS MAXTIME
	                    , IDCARD, CODE
	                    , STARTTIME, ENDTIME
	                    , CASE
		                    WHEN (MIN(PWTIME) = MAX(PWTIME)) AND (STARTTIME IS NULL AND ENDTIME IS NULL) THEN 'NONE'
		                    WHEN (MIN(PWTIME) != MAX(PWTIME)) AND (STARTTIME IS NULL AND ENDTIME IS NULL) THEN 'NONE'
		                    WHEN (MIN(PWTIME) < MAX(PWTIME)) AND (STARTTIME IS NOT NULL AND ENDTIME IS NOT NULL) AND (STARTTIME > ENDTIME) THEN 'OI'
		                    ELSE 'IO'
	                      END AS CHECKING
                    FROM	PWTIMETEMP WITH (NOLOCK)
	                    INNER JOIN PWEMPLOYEE ON RTRIM(PWTIMETEMP.PWEMPLOYEE) = RTRIM(PWEMPLOYEE.PWEMPLOYEE)
	                    LEFT OUTER JOIN PWSECTION ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION
	                    LEFT OUTER JOIN PWDEPT ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT
	                    LEFT OUTER JOIN (
		                    SELECT	PWTIME0 AS [IDCARD], {0} AS [CODE], SHIFTID, STARTTIME, ENDTIME
		                    FROM	PWTIME2 WITH (NOLOCK)
			                    LEFT OUTER JOIN 
			                    (
				                    SELECT	DISTINCT PWTIME0 AS SHIFTID, PWTIMEIN1 AS STARTTIME, PWTIMEOUT2 AS ENDTIME
				                    FROM	PWTIME1 WITH (NOLOCK)
			                    ) AS T
			                    ON PWTIME2.{0} = T.SHIFTID
                    		
		                    WHERE	PWTIME0 != '' AND PWYEAR = {1} AND PWMONTH = {2}
	                    ) AS W
	                    ON PWEMPLOYEE.PWCARD = W.IDCARD
                    WHERE	(PWRDATE = '{3}' OR (PWDATE = '{3}' AND PWRDATE = '1900-01-01'))
		                    AND PWEVENT NOT IN ('E', 'J')
		                    AND PWEMPLOYEE.PWDIVISION != '75'
                    GROUP BY 
		                    PWDATE, PWRDATE, PWTIMETEMP.PWEMPLOYEE, PWTRANTYPE
		                    , PWCARD, PWFNAME, PWLNAME, PWEMPLOYEE.PWSECTION, PWSECTION.PWDESC, PWEMPLOYEE.PWDEPT, PWDEPT.PWDESC
		                    , IDCARD, CODE
		                    , STARTTIME, ENDTIME
                    ) AS MNEVATIME
                    WHERE CHECKING != 'OI'";
            #endregion

            string ret =
                string.Format(@"SELECT	*
    FROM	(
    SELECT	
	    PWDATE, PWRDATE, PWTIMETEMP.PWEMPLOYEE
	    , PWEMPLOYEE.PWCARD, RTRIM(PWFNAME) AS PWFNAME, RTRIM(PWLNAME) AS PWLNAME, PWEMPLOYEE.PWSECTION, PWSECTION.PWDESC AS SECTIONDESC, PWEMPLOYEE.PWDEPT, PWDEPT.PWDESC AS DEPTDESC
	    , MIN(PWTIME) AS MINTIME, MAX(PWTIME) AS MAXTIME
	    , IDCARD, CODE, SHIFTID
	    , STARTTIME, ENDTIME
	    , CASE
			WHEN (IDCARD IS NULL) AND (CODE IS NULL) THEN 'NONE*'
		    WHEN (MIN(PWTIME) = MAX(PWTIME)) AND (STARTTIME IS NULL AND ENDTIME IS NULL) THEN 'NONE'
		    WHEN (MIN(PWTIME) != MAX(PWTIME)) AND (STARTTIME IS NULL AND ENDTIME IS NULL) THEN 'NONE'
			WHEN (MIN(PWTIME) = MAX(PWTIME)) AND (STARTTIME IS NOT NULL AND ENDTIME IS NOT NULL) AND (STARTTIME < ENDTIME) AND ((MAX(PWTIME) - STARTTIME) < 3.00) THEN 'I'
			WHEN (MIN(PWTIME) = MAX(PWTIME)) AND (STARTTIME IS NOT NULL AND ENDTIME IS NOT NULL) AND (STARTTIME < ENDTIME) AND ((MAX(PWTIME) - ENDTIME) < 3.00) THEN 'O'
			WHEN (MIN(PWTIME) = MAX(PWTIME)) AND (STARTTIME IS NOT NULL AND ENDTIME IS NOT NULL) AND (STARTTIME > ENDTIME) AND (ABS(MAX(PWTIME) - ENDTIME) < 3.00) THEN 'O'
			WHEN (MIN(PWTIME) = MAX(PWTIME)) AND (STARTTIME IS NOT NULL AND ENDTIME IS NOT NULL) AND (STARTTIME > ENDTIME) AND (ABS(MAX(PWTIME) - STARTTIME) < 3.00) THEN 'I'
			WHEN (MIN(PWTIME) < MAX(PWTIME)) AND (STARTTIME IS NOT NULL AND ENDTIME IS NOT NULL) AND (STARTTIME > ENDTIME) AND (ABS(MAX(PWTIME) - ENDTIME) < 3.00) THEN 'O'
			WHEN (MIN(PWTIME) < MAX(PWTIME)) AND (STARTTIME IS NOT NULL AND ENDTIME IS NOT NULL) AND (STARTTIME > ENDTIME) AND (ABS(MAX(PWTIME) - STARTTIME) < 3.00) THEN 'I'
            --WHEN (MIN(PWTIME) = MAX(PWTIME)) AND (STARTTIME IS NOT NULL AND ENDTIME IS NOT NULL) AND (STARTTIME > ENDTIME) THEN 'DO'
		    --WHEN (MIN(PWTIME) < MAX(PWTIME)) AND (STARTTIME IS NOT NULL AND ENDTIME IS NOT NULL) AND (STARTTIME > ENDTIME) THEN 'OI'
		    ELSE 'IO'
	    END AS CHECKING
    FROM	PWTIMETEMP WITH (NOLOCK)
	    INNER JOIN PWEMPLOYEE ON RTRIM(PWTIMETEMP.PWEMPLOYEE) = RTRIM(PWEMPLOYEE.PWEMPLOYEE)
	    LEFT OUTER JOIN PWSECTION ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION
	    LEFT OUTER JOIN PWDEPT ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT
	    LEFT OUTER JOIN (
		    SELECT	PWTIME0 AS [IDCARD], {0} AS [CODE], SHIFTID, STARTTIME, ENDTIME
		    FROM	PWTIME2 WITH (NOLOCK)
			    LEFT OUTER JOIN 
			    (
				    SELECT	DISTINCT PWTIME0 AS SHIFTID, PWTIMEIN1 AS STARTTIME, PWTIMEOUT2 AS ENDTIME
				    FROM	PWTIME1 WITH (NOLOCK)
			    ) AS T
			    ON REPLACE(RTRIM(PWTIME2.{0}), ' ', '') = RTRIM(T.SHIFTID)                    		
		    WHERE	PWTIME0 != '' AND PWYEAR = {1} AND PWMONTH = {2}
	    ) AS W
	    ON RTRIM(PWEMPLOYEE.PWCARD) = RTRIM(W.IDCARD)
    WHERE	(PWRDATE = '{3}' OR (PWDATE = '{3}' AND PWRDATE = '1900-01-01'))
		    AND PWEVENT NOT IN ('E', 'J')
		    AND PWEMPLOYEE.PWDIVISION != '75'
    GROUP BY 
		    PWDATE, PWRDATE, PWTIMETEMP.PWEMPLOYEE
		    , PWCARD, PWFNAME, PWLNAME, PWEMPLOYEE.PWSECTION, PWSECTION.PWDESC, PWEMPLOYEE.PWDEPT, PWDEPT.PWDESC
		    , IDCARD, CODE, SHIFTID
		    , STARTTIME, ENDTIME
    ) AS MNEVATIME
    WHERE CHECKING != 'O'"
                , target, this.dateTimePickerTransdate.Value.Year, this.dateTimePickerTransdate.Value.Month
                       , this.dateTimePickerTransdate.Value.ToString("yyyy-MM-dd"));
            return ret;
        }
        void FuncProcess()
        {
            //startprogress.Start();
            //System.Diagnostics.Process p = new System.Diagnostics.Process();

            //this.statusLabel.Text = "Processing...";
            this.Cursor = Cursors.WaitCursor;

            this.queryRun = this.SetQueryRun();

            //MessageBox.Show(queryRun);
            //return;

            using (con = new SqlConnection(DatabaseConfig.ServerConStr))
            {
                try
                {
                    con.Open();
                    using (command = new SqlCommand())
                    {
                        command.Connection = con;
                        command.CommandType = CommandType.Text;
                        command.CommandText = queryRun;
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            DataTable mnTimeEvaTrans = new DataTable("MNTIMEEVATRANS");
                            mnTimeEvaTrans.Load(reader);
                            this.radGridReport.DataSource = mnTimeEvaTrans;
                            this.radGridReport.BestFitColumns();
                            //this.radGridReport.Invoke(new updatebar(this.UpdateProgress));
                        }
                        else
                        {
                            this.radGridReport.DataSource = new string[] { };
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();

                    queryRun = string.Empty;
                    reader = null;

                    this.Cursor = Cursors.Default;

                    this.statusLabel.Text = "Finished on " + DateTime.Now.ToString();
                }
            }
        }
        void FuncExportExcel()
        {
            if (this.radGridReport.RowCount < 1) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) { return; }
            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                RadMessageBox.SetThemeName(this.radGridReport.ThemeName);
                RadMessageBox.Show("Please enter a file name.");
                return;
            }
            string fileName = saveFileDialog.FileName;
            bool openExportFile = false;
            RunExportToExcelML(fileName, ref openExportFile);

        }
        void RunExportToExcelML(string fileName, ref bool openExportFile)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(this.radGridReport);
            excelExporter.SheetName = string.Format(@"MNRP{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), DateTime.Now.Millisecond);
            //excelExporter.SheetMaxRows = ExcelMaxRows._1048576; //ExcelMaxRows._65536

            //set exporting visual settings
            excelExporter.ExportVisualSettings = true;

            //if (this.radTextBoxSheet.Text != String.Empty)
            //{
            //    excelExporter.SheetName = this.radTextBoxSheet.Text;
            //}
            //switch (this.radComboBoxSummaries.SelectedIndex)
            //{
            //    case 0:
            //        excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            //    break;
            //    case 1:
            //        excelExporter.SummariesExportOption = SummariesOption.ExportOnlyTop;
            //        break;
            //    case 2:
            //        excelExporter.SummariesExportOption = SummariesOption.ExportOnlyBottom;
            //        break;
            //    case 3:
            //        excelExporter.SummariesExportOption = SummariesOption.DoNotExport;
            //        break;
            //}
            //set max sheet rows
            //if (this.radRadioButton1.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            //{
            //    excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            //}
            //else if (this.radRadioButton2.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            //{
            //    excelExporter.SheetMaxRows = ExcelMaxRows._65536;
            //}              
            //set exporting visual settings
            //excelExporter.ExportVisualSettings = this.exportVisualSettings;
            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(this.radGridReport.ThemeName);
                DialogResult dr = RadMessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?", "Export to Excel", MessageBoxButtons.OK, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(this.radGridReport.ThemeName);
                RadMessageBox.Show(this, ex.Message, "Info", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }
        void FuncViewTimeTemp()
        {
            try
            {
                string emplId = this.radGridReport.CurrentRow.Cells["PWEMPLOYEE"].Value.ToString();
                string transDate = DateTime.Parse(this.radGridReport.CurrentRow.Cells["PWDATE"].Value.ToString()).ToString("yyyy-MM-dd");
                this.FuncLookupTimeTemp(emplId, transDate);
            }
            catch (NullReferenceException)
            {
                return;
            }
        }
        void FuncLookupTimeTemp(string _emplId, string _transDate)
        {
            queryRun =
                string.Format(@"SELECT   * FROM  PWTIMETEMP WHERE    PWDATE = '{0}' AND PWEMPLOYEE = '{1}'", _transDate, _emplId);
            using (con = new SqlConnection(DatabaseConfig.ServerConStr))
            {
                try
                {
                    con.Open();
                    using (command = new SqlCommand())
                    {
                        command.Connection = con;
                        command.CommandType = CommandType.Text;
                        command.CommandText = @queryRun;
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            DataTable pwtimetemp = new DataTable("PWTIMETEMP");
                            pwtimetemp.Load(reader);

                            PWTIMETEMP frmTimeTemp = new PWTIMETEMP(pwtimetemp);

                            frmTimeTemp.StartPosition = FormStartPosition.CenterParent;
                            frmTimeTemp.Text = string.Format(@"Usr: {0}; Transdate: {1}; EmplId: {2}", System.Security.Principal.WindowsIdentity.GetCurrent().Name, _transDate, _emplId);
                            frmTimeTemp.Show(this);
                        }
                        else
                        {
                            RadMessageBox.SetThemeName(this.radGridReport.ThemeName);
                            RadMessageBox.Show(this, "ไม่พบรายการที่ต้องการ", "Info", MessageBoxButtons.OK, RadMessageIcon.Info);
                        }
                    }
                }
                catch (Exception ex)
                {
                    RadMessageBox.SetThemeName(this.radGridReport.ThemeName);
                    RadMessageBox.Show(this, ex.ToString(), "Info", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    queryRun = string.Empty;
                    reader = null;
                }
            }
        }
        #endregion
    }
}
