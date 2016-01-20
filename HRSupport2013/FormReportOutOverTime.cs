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

namespace HROUTOFFICE
{
    public partial class FormReportOutOverTime : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);
        string sysuser;
        string sectionid;
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

        List<GridData> listData = new List<GridData>();

        public FormReportOutOverTime()
        {
            InitializeComponent();

            #region <Form>
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;
            this.KeyPreview = true;
            if (this.Owner != this)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
                //this.WindowState = FormWindowState.Maximized;
            }
            #endregion

            #region <RadGridView>
            // <WS>
            this.radGridData.Dock = DockStyle.Fill;    
            this.radGridData.ReadOnly = true;            
            this.radGridData.AllowAddNewRow = false;
            this.radGridData.EnableHotTracking = true;
            this.radGridData.EnableFiltering = true;
            this.radGridData.MasterTemplate.AutoGenerateColumns = false;
            this.radGridData.MasterTemplate.ShowHeaderCellButtons = true;
            this.radGridData.MasterTemplate.ShowFilteringRow = false;      
            this.radGridData.MasterTemplate.ShowGroupedColumns = true;      
            this.radGridData.MasterTemplate.AutoExpandGroups = true;
            this.radGridData.MasterTemplate.EnableAlternatingRowColor = true;
            this.radGridData.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            this.radGridData.BestFitColumns(BestFitColumnMode.AllCells);
            // </WS>
            #endregion

            #region <Button>
            this.btnserch.Click += new EventHandler(btnserch_Click);
            #endregion
        }

        #region <Event Form>
        private void FormReportOutOverTime_Load(object sender, EventArgs e)
        {
            this.sysuser = SysOutoffice;
            this.sectionid = Section;

            this.dtpStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dtpEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion
        
        #region <Event Button>
        void btnserch_Click(object sender, EventArgs e)
        {
            this.refresh_DS();
        }
        void radButtonExport_Click(object sender, EventArgs e)
        {
            if (radGridData.RowCount != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                ExportToExcelML excelExporter = null;

                try
                {
                    ExportToExcelML excelML = new ExportToExcelML(this.radGridData);
                    excelExporter = new ExportToExcelML(this.radGridData);
                    excelExporter.ExcelCellFormatting += excelExporter_ExcelCellFormatting;
                    excelExporter.ExcelTableCreated += exporter_ExcelTableCreated;

                    this.Cursor = Cursors.WaitCursor;
                    saveFileDialog.Filter = "Excel (*.xls)|*.xls";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        excelExporter.RunExport(saveFileDialog.FileName);

                        DialogResult dr = RadMessageBox.Show("การบันทึกไฟล์สำเร็จ คุณต้องการเปิดไฟล์หรือไม่?",
                            "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveFileDialog.FileName);
                        }
                    }
                }
                catch (Exception) { }
            }
            else
            {
                RadMessageBox.Show("ไม่มีข้อมูล");
                return;
            }
        }
        #endregion

        #region <Function and Methods>
        System.Type getType(string _dataType)
        {
            switch (_dataType)
            {
                case "System.Int32":
                    return typeof(System.Int32);
                case "System.DateTime":
                    return typeof(System.DateTime);
                default:
                    return typeof(System.String);
            }
        }
        void refresh_DS()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                sqlCommand.CommandText = string.Format(
                            @"SELECT [DocId], [OutOfficeId], [EmplId], [EmplFname] + SPACE(1) + [EmplLname] AS [EmplFullName], [Dimention]
                                    , [ShiftId], [StartTime], [EndTime], CONVERT(NVARCHAR(10), [TrandDateTime], 23) AS [TrandDateTime]
	                                , CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS [OutType]
	                                , CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS [CombackType]
	                                , [Reason], [HeadApprovedName]
	                                , ISNULL(CONVERT(NVARCHAR, [HeadApprovedDateTime], 120), '0000-00-00 00:00:00') AS [HeadApprovedDateTime]
	                                , CASE [HrApprovedOut] WHEN '1' THEN 'รอนุมัติ' ELSE 'อนุมัติ'  END AS [HrApprovedOut], [HrApprovedOutName]
	                                , CONVERT(NVARCHAR, [HrApprovedOutDateTime], 120) AS [HrApprovedOutDateTime]
	                                , CONVERT(NVARCHAR, [HrApprovedInDateTime], 120) AS [HrApprovedInDateTime]
                                    , CONVERT(NVARCHAR, [ToTallTimeUse], 24) AS [ToTallTimeUse]
                                    , OverTime = Cast('1901-01-01' as DateTime)
                                FROM [IVZ_HROUTOFFICE] WITH (NOLOCK) 
                                WHERE 
                                    ([Status] = '1') 
                                    AND ([OutType] = '2')
                                    AND ([CombackType] = '1')
                                    AND ([ToTallTimeUse] > '1900-01-01 01:31:00.000')
                                    AND ([TrandDateTime] BETWEEN '{0}' AND  '{1}') "
                                , dtpStart.Text.ToString()
                                , dtpEnd.Text.ToString());

                //AND [DocId] IN('OUT140418-0077','OUT140420-0019') 
                
                // <WS>
                DateTime dtStartShift, dtEndShift, dtKeyOut, dtKeyIn;
                DataTable localTable = new DataTable("LocalTable");

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        localTable.Load(reader);
                        reader.Close();
                    }
                }
                if (localTable == null && localTable.Rows.Count < 1)
                {
                    radGridData.DataSource = new string[] { };
                    return;
                }
                
                //listData.Clear();
                listData = new List<GridData>();

                GridData gData;
                foreach (DataRow row in localTable.Rows)
                {
                    gData = new GridData();

                    #region <Field>
                    /*
                     * DocId	
                     * OutOfficeId	
                     * EmplId	
                     * EmplFullName	
                     * Dimention	
                     * ShiftId	
                     * StartTime	
                     * EndTime	
                     * TrandDateTime	
                     * OutType	
                     * CombackType	
                     * Reason	
                     * HeadApprovedName	
                     * HeadApprovedDateTime	
                     * HrApprovedOut	
                     * HrApprovedOutName	
                     * HrApprovedOutDateTime	
                     * HrApprovedInDateTime	
                     * ToTallTimeUse	
                     * OverTime
                    */
                    #endregion

                    gData.DocId = (string)row.Field<string>("DocId");
                    gData.OutOfficeId = row.Field<int>("OutOfficeId");
                    gData.EmplId = (string)row.Field<string>("EmplId");
                    gData.EmplFullName = (string)row.Field<string>("EmplFullName");
                    gData.Dimention = (string)row.Field<string>("Dimention");
                    gData.ShiftId = (string)row.Field<string>("ShiftId");
                    gData.StartTime = (string)row.Field<string>("StartTime");
                    gData.EndTime = (string)row.Field<string>("EndTime");
                    gData.TrandDateTime = (string)row.Field<string>("TrandDateTime");
                    gData.OutType = (string)row.Field<string>("OutType");
                    gData.CombackType = (string)row.Field<string>("CombackType");
                    gData.Reason = (string)row.Field<string>("Reason");
                    gData.HeadApprovedName = (string)row.Field<string>("HeadApprovedName");
                    gData.HeadApprovedDateTime = (string)row.Field<string>("HeadApprovedDateTime");
                    gData.HrApprovedOut = (string)row.Field<string>("HrApprovedOut");
                    gData.HrApprovedOutName = (string)row.Field<string>("HrApprovedOutName");
                    gData.HrApprovedOutDateTime = (string)row.Field<string>("HrApprovedOutDateTime");
                    gData.HrApprovedInDateTime = (string)row.Field<string>("HrApprovedInDateTime");
                    gData.ToTallTimeUse = (string)row.Field<string>("ToTallTimeUse");
                    gData.OverTime = (DateTime)row.Field<DateTime>("OverTime");

                    if (DateTime.TryParse(row["HrApprovedOutDateTime"].ToString(), out dtKeyOut))
                    {
                        if (DateTime.TryParse(string.Format(@"{0} {1}", row["TrandDateTime"].ToString(), row["EndTime"].ToString()),
                                               out dtEndShift))
                        {
                            dtStartShift = DateTime.Parse(string.Format(@"{0} {1}", row["TrandDateTime"].ToString(), row["StartTime"].ToString()));
                        }
                        else
                        {
                            dtEndShift = DateTime.Parse(string.Format(@"{0}", row["TrandDateTime"].ToString()));
                            dtStartShift = dtEndShift;
                        }

                        if (DateTime.TryParse(row["HrApprovedInDateTime"].ToString(), out dtKeyIn)) { }
                        else
                        {
                            dtKeyIn = DateTime.Parse(string.Format(@"{0}", row["TrandDateTime"].ToString())).AddDays(1);
                        }

                        // กรณีเป็นกะข้ามวันนั้น กำหนดวันที่เริ่มต้นกะย้อนหลัง 1 วัน set retro for a day.
                        if (dtStartShift < dtEndShift)
                        {
                            dtStartShift.AddDays(-1);
                        }

                        //<neung> เวลาเลิกงานก่อนเที่ยงคืนเป็นต้นไป
                        //กลับเข้ามาก่อนเลิกงาน
                        if (dtStartShift < dtEndShift) //<Neung>
                        {
                            if (dtKeyIn < dtEndShift)
                            {
                                TimeSpan sp = dtKeyIn - dtKeyOut;
                                TimeSpan spLimit = TimeSpan.FromSeconds(5400);
                                TimeSpan spOver = sp - spLimit;

                                var spFinal = new DateTime(1901, 1, 1) + spOver;
                                gData.OverTime = spFinal;
                            }
                            //กลับเข้ามาหลังเลิกงาน
                            else if (dtKeyIn > dtEndShift)
                            {
                                TimeSpan sp = dtEndShift - dtKeyOut;
                                TimeSpan spLimit = TimeSpan.FromSeconds(5400);
                                TimeSpan spOver = sp - spLimit;
                                if (sp > spLimit)
                                {
                                    //var spFinal = new DateTime(1901, 1, 1) + sp;

                                    //<Neung>
                                    var spFinal = new DateTime(1901, 1, 1) + spOver;
                                    gData.OverTime = spFinal;
                                }
                                else
                                {
                                    gData.OverTime = DateTime.Parse("1901-01-01 00:00:00");
                                }
                            }
                        }

                            //<neung> เวลาเลิกงานตั้งแต่เที่ยงคืนเป็นต้นไป
                        else if (dtStartShift > dtEndShift)
                        {
                            //กลับเข้ามาก่อนเลิกงาน
                            if (dtKeyIn < dtEndShift)
                            {
                                TimeSpan sp = dtKeyIn - dtKeyOut;
                                TimeSpan spLimit = TimeSpan.FromSeconds(5400);
                                TimeSpan spOver = sp - spLimit;

                                var spFinal = new DateTime(1901, 1, 1) + spOver;
                                gData.OverTime = spFinal;
                            }
                            //กลับเข้ามาหลังเลิกงาน
                            else if (dtKeyIn > dtEndShift)
                            {
                                //TimeSpan sp = dtEndShift - dtKeyOut;
                                TimeSpan sp = dtKeyIn - dtKeyOut;
                                TimeSpan spLimit = TimeSpan.FromSeconds(5400);
                                TimeSpan spOver = sp - spLimit;
                                if (sp > spLimit)
                                {
                                    //var spFinal = new DateTime(1901, 1, 1) + sp;

                                    //<Neung>
                                    var spFinal = new DateTime(1901, 1, 1) + spOver;
                                    gData.OverTime = spFinal;
                                }
                                else
                                {
                                    gData.OverTime = DateTime.Parse("1901-01-01 00:00:00");
                                }
                            }
                        }
                    }
                    else
                    {
                        gData.OverTime = DateTime.Parse("1901-01-01 00:00:00");
                    }

                    // กรณีไม่สามารถระบุกะได้หรือว่าไม่มีตารางกะ ยังไม่ได้คิด                    
                    listData.Add(gData);
                }

                if (listData == null && listData.Count() < 1)
                {
                    radGridData.DataSource = new string[] { };
                    return;
                }
                this.radGridData.DataSource = listData;
                this.radGridData.BestFitColumns(BestFitColumnMode.AllCells);
                // </WS>


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(@"Error Message: {0}{1}", Environment.NewLine, ex.ToString()), "การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        void EmplUseTime() { }
        #endregion

        #region <Export Print>
        void exporter_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {

            string headerText = "รายงานออกนอกบริษัทเกิน 1:30 ชม. ตั้งแต่วันที่:  " + dtpStart.Text + " ถึง " + dtpEnd.Text;
            Telerik.WinControls.UI.Export.ExcelML.SingleStyleElement style = ((ExportToExcelML)sender).AddCustomExcelRow(e.ExcelTableElement, 30, headerText);
            style.FontStyle.Bold = true;
            style.FontStyle.Size = 15;
            style.FontStyle.Color = Color.White;
            style.InteriorStyle.Color = Color.CadetBlue;
            style.InteriorStyle.Pattern = Telerik.WinControls.UI.Export.ExcelML.InteriorPatternType.Solid;
            style.AlignmentElement.HorizontalAlignment = Telerik.WinControls.UI.Export.ExcelML.HorizontalAlignmentType.Center;
            style.AlignmentElement.VerticalAlignment = Telerik.WinControls.UI.Export.ExcelML.VerticalAlignmentType.Center;
        }
        void excelExporter_ExcelCellFormatting(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventArgs e)
        {
            if (e.GridRowInfoType == typeof(GridViewTableHeaderRowInfo))
            {
                Telerik.WinControls.UI.Export.ExcelML.BorderStyles border = new Telerik.WinControls.UI.Export.ExcelML.BorderStyles();

                border.Color = Color.Black;
                border.Weight = 2;
                border.LineStyle = Telerik.WinControls.UI.Export.ExcelML.LineStyle.Continuous;
                border.PositionType = Telerik.WinControls.UI.Export.ExcelML.PositionType.Bottom;
                e.ExcelStyleElement.Borders.Add(border);
            }
        }
        #endregion
    }
    // <WS>
    class GridData
    {
        public string DocId { get; set; }
        public int OutOfficeId { get; set; }
        public string EmplId { get; set; }
        public string EmplFullName { get; set; }
        public string Dimention { get; set; }
        public string ShiftId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TrandDateTime { get; set; }
        public string OutType { get; set; }
        public string CombackType { get; set; }
        public string Reason { get; set; }
        public string HeadApprovedName { get; set; }
        public string HeadApprovedDateTime { get; set; }
        public string HrApprovedOut { get; set; }
        public string HrApprovedOutName { get; set; }
        public string HrApprovedOutDateTime { get; set; }
        public string HrApprovedInDateTime { get; set; }
        public string ToTallTimeUse { get; set; }
        public DateTime OverTime { get; set; }
    }
    // </WS>
}
