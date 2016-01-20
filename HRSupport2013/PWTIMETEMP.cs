using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using System.IO;


namespace HROUTOFFICE
{
    public partial class PWTIMETEMP : Form
    {
        SqlConnection con;
        SqlCommand command;
        SqlDataReader reader;

        protected string queryRun = "";
        protected string commandRun = "";

        protected DataTable source;
        public DataTable Source
        {
            get { return (source != null) ? source : null; }
            set { source = value; }
        }

        public PWTIMETEMP()
        {
            InitializeComponent();

            #region <Window Form>
            this.Text = string.Format(@"Usr:{0}; {1}", System.Security.Principal.WindowsIdentity.GetCurrent().Name, "พฤติกรรมการรูดบัตร");
            this.KeyPreview = true;
            this.ShowIcon = true;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Load += new EventHandler(PWTIMETEMP_Load);
            #endregion            

            #region <layout>
            this.toolStrip1.Dock = DockStyle.Top;
            this.panelContainer.Dock = DockStyle.Fill;
            #endregion

            #region <RadGridView>
            this.radGridTimeTemp.Dock = DockStyle.Fill;
            this.radGridTimeTemp.AutoGenerateColumns = true;
            this.radGridTimeTemp.EnableFiltering = true;
            this.radGridTimeTemp.AllowAddNewRow = false;
            this.radGridTimeTemp.ReadOnly = true;
            this.radGridTimeTemp.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.HeaderCells);
            this.radGridTimeTemp.MasterTemplate.ShowHeaderCellButtons = true;
            this.radGridTimeTemp.MasterTemplate.ShowFilteringRow = false;
            this.radGridTimeTemp.MasterTemplate.AutoGenerateColumns = false;

            GridViewTextBoxColumn txtCol0 = new GridViewTextBoxColumn();
            txtCol0.FieldName = "PWDATE";
            txtCol0.Name = "PWDATE";
            txtCol0.HeaderText = "วันที่";
            txtCol0.FormatString = "{0:dd/MM/yyyy}";
            txtCol0.IsVisible = true;
            this.radGridTimeTemp.Columns.Add(txtCol0);

            GridViewTextBoxColumn txtCol1 = new GridViewTextBoxColumn();
            txtCol1.FieldName = "PWRDATE";
            txtCol1.Name = "PWRDATE";
            txtCol1.HeaderText = "วันที่(2)";
            txtCol1.FormatString = "{0:dd/MM/yyyy}";
            txtCol1.IsVisible = true;
            this.radGridTimeTemp.Columns.Add(txtCol1);

            GridViewTextBoxColumn txtCol2 = new GridViewTextBoxColumn();
            txtCol2.FieldName = "PWEMPLOYEE";
            txtCol2.Name = "PWEMPLOYEE";
            txtCol2.HeaderText = "พนักงาน";
            txtCol2.IsVisible = true;
            this.radGridTimeTemp.Columns.Add(txtCol2);

            GridViewTextBoxColumn txtCol3 = new GridViewTextBoxColumn();
            txtCol3.FieldName = "PWSOURCE";
            txtCol3.Name = "PWSOURCE";
            txtCol3.HeaderText = "PWSOURCE";
            txtCol3.IsVisible = true;
            this.radGridTimeTemp.Columns.Add(txtCol3);

            GridViewDecimalColumn txtCol4 = new GridViewDecimalColumn();
            txtCol4.FieldName = "PWTIME";
            txtCol4.Name = "PWTIME";
            txtCol4.HeaderText = "เวลา";
            txtCol4.IsVisible = true;
            this.radGridTimeTemp.Columns.Add(txtCol4);

            GridViewTextBoxColumn txtCol5 = new GridViewTextBoxColumn();
            txtCol5.FieldName = "PWTRANTYPE";
            txtCol5.Name = "PWTRANTYPE";
            txtCol5.HeaderText = "ประเภท";
            txtCol5.IsVisible = true;
            this.radGridTimeTemp.Columns.Add(txtCol5);

            GridViewTextBoxColumn txtCol6 = new GridViewTextBoxColumn();
            txtCol6.FieldName = "PWEVENT";
            txtCol6.Name = "PWEVENT";
            txtCol6.HeaderText = "เหตุการณ์";
            txtCol6.IsVisible = true;
            this.radGridTimeTemp.Columns.Add(txtCol6);
            
            #endregion

            #region <MenuItem>
            toolStripBtnExportExcel.Click += new EventHandler(toolStripBtnExportExcel_Click);
            #endregion
        }

        void toolStripBtnExportExcel_Click(object sender, EventArgs e)
        {
            this.FuncExportExcel();
        }

        void PWTIMETEMP_Load(object sender, EventArgs e)
        {
            try
            {
                this.radGridTimeTemp.DataSource = source;
                this.radGridTimeTemp.BestFitColumns();
            }
            catch (NullReferenceException nullEx) { }
            catch (Exception ex) { }
        }

        public PWTIMETEMP(object _source) : this()
        {
            Source = (DataTable)_source;
        }

        // <Overide methode force combine multikey in textbox>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F5))
            {
                //this.FuncProcess();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                var activeWin = HROUTOFFICE.PWTIMETEMP.ActiveForm;
                activeWin.Close();
            }
            else if (keyData == Keys.ControlKey && keyData == Keys.Alt && keyData == Keys.X)
            {
                //this.FuncExportExcel();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region <Functions and Methods>
        DataTable SetDefaultSource()
        {            
            var ret = source = new DataTable("PWTIMETEMP");
            source.Columns.Add(new DataColumn(@"PWDATE", typeof(DateTime)));
            source.Columns.Add(new DataColumn(@"PWRDATE", typeof(DateTime)));
            source.Columns.Add(new DataColumn(@"PWEMPLOYEE", typeof(DateTime)));
            source.Columns.Add(new DataColumn(@"PWSOURCE", typeof(DateTime)));
            source.Columns.Add(new DataColumn(@"PWTIME", typeof(DateTime)));
            source.Columns.Add(new DataColumn(@"PWTRANTYPE", typeof(DateTime)));
            source.Columns.Add(new DataColumn(@"PWEVENT", typeof(DateTime)));
            return ret;
        }
        void FuncExportExcel()
        {
            if (this.radGridTimeTemp.RowCount < 1) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) { return; }
            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                RadMessageBox.SetThemeName(this.radGridTimeTemp.ThemeName);
                RadMessageBox.Show("Please enter a file name.");
                return;
            }
            string fileName = saveFileDialog.FileName;
            bool openExportFile = false;
            RunExportToExcelML(fileName, ref openExportFile);
        }
        void RunExportToExcelML(string fileName, ref bool openExportFile)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(this.radGridTimeTemp);
            excelExporter.SheetName = string.Format(@"MNRPSRC{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), DateTime.Now.Millisecond);
            excelExporter.ExportVisualSettings = true;
            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(this.radGridTimeTemp.ThemeName);
                RadMessageBox.Show("The data in the grid was exported successfully.", "Export to Excel", MessageBoxButtons.OK, RadMessageIcon.Info);
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(this.radGridTimeTemp.ThemeName);
                RadMessageBox.Show(this, ex.Message, "Info", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }
        #endregion

    }
}
