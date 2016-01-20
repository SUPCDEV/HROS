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
    public partial class FormReportWeekedOver : Form
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

        public FormReportWeekedOver()
        {
            InitializeComponent();
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;

            #region radGridView

            this.radGridegetdata.Dock = DockStyle.Fill;
            this.radGridegetdata.AutoGenerateColumns = true;
            this.radGridegetdata.EnableFiltering = false;
            this.radGridegetdata.AllowAddNewRow = false;
            this.radGridegetdata.MasterTemplate.AutoGenerateColumns = false;
            this.radGridegetdata.ShowGroupedColumns = true;
            this.radGridegetdata.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.radGridegetdata.EnableHotTracking = true;
            this.radGridegetdata.AutoSizeRows = true;

            #endregion
        }
        private void FormReportWeekedOver_Load(object sender, EventArgs e)
        {
            this.sysuser = SysOutoffice;
            this.sectionid = Section;

            this.btnserch.Click += new EventHandler(btnserch_Click);
            this.dtpStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dtpEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                sqlCommand.CommandText = string.Format(
                        @"SELECT [EmplId],[EmplFname],[EmplLname],[Dimention]
	                              ,COUNT([EmplId]) AS CEmplId
                          FROM [dbo].[IVZ_HROUTOFFICE]
                          WHERE [Status] = 1
                          AND [OutType] = 2 
                          AND [CombackType] = 1
                          AND [TrandDateTime] BETWEEN '{0}' AND  '{1}'
                          AND [HrApprovedOut] = 2
						  AND [HrApprovedIn] = 2 
                          GROUP BY [EmplId],[EmplFname],[EmplLname],[Dimention] 
                          HAVING COUNT(EmplId)>2 
                          ORDER BY [EmplId]"
                          , dtpStart.Text.ToString().Trim()
                          , dtpEnd.Text.ToString().Trim());

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    radGridegetdata.DataSource = dt;
                }
                else
                {
                    MessageBox.Show(@"ไม่มีข้อมูล", "การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    radGridegetdata.DataSource = dt;
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
        void btnserch_Click(object sender, EventArgs e)
        {
            GetData();
        }

        #region Print
        void exporter_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {

            string headerText = "รายงานออกนอกบริษัทเกินสองครั้ง / สัปดาห์ ตั้งแต่วันที่:  " + dtpStart.Text + " ถึง " + dtpEnd.Text;
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
        private void radButtonExport_Click(object sender, EventArgs e)
        {
            if (radGridegetdata.RowCount != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                ExportToExcelML excelExporter = null;

                try
                {
                    ExportToExcelML excelML = new ExportToExcelML(this.radGridegetdata);
                    excelExporter = new ExportToExcelML(this.radGridegetdata);
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
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                RadMessageBox.Show("ไม่มีข้อมูล");
                return;
            }
        }
        #endregion

        private void radGridegetdata_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.Column.Name == "CEmplId")
            {
                GridViewRowInfo row = (GridViewRowInfo)radGridegetdata.Rows[e.RowIndex];
                string EmplId = row.Cells["EmplId"].Value.ToString();
                DateTime dateFrom, dateTo;

                dateFrom = this.dtpStart.Value;
                dateTo = this.dtpEnd.Value;

                if (dateFrom > dateTo)
                {
                    MessageBox.Show("วันที่สิ้นสุดต้องมีค่ามากกว่าหรือเท่ากับวันที่เริ่มต้น", "การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (FormDetailWeekedOver Frm = new FormDetailWeekedOver(EmplId, dateFrom, dateTo))
                {
                    Frm.Text = string.Format(@"พนักงาน: {0}, วันที่ {1} ถึง  {2}", EmplId, dateFrom.ToShortDateString(), dateTo.ToShortDateString());
                    Frm.ShowDialog(this);
                }
            }

        }
    }
}
