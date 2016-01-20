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
    public partial class Pay_Report : Form
    {
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        #region MyRegion SysEmpl

        string sysuser = "";
        string sectionid = "";
        string approveout = "";

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

        protected string section;
        public string Section
        {
            get { return section; }
            set { section = value; }
        }
        #endregion

        public Pay_Report()
        {
            InitializeComponent();

            btn_Search.Click += new EventHandler(btn_Search_Click);

            this.dt_Start.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.dt_End.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.txt_Date.Text = DateTime.Now.ToString("yyMMdd");

            this.SetGridView();
        }

        void btn_Search_Click(object sender, EventArgs e)
        {
            GetData();
        }

        void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            string paystat = "";
            
            if (rdb_CheckAll.ToggleState == ToggleState.On)
            {
                paystat = "HD.PAYSTAT = 1 OR HD.PAYSTAT = 0 ";
            }
            else if (rdb_CheckPay.ToggleState == ToggleState.On)
            {
                paystat = "HD.PAYSTAT = 1 ";
            }
            else if (rdb_CheckNotPay.ToggleState == ToggleState.On)
            {
                paystat = "HD.PAYSTAT = 0";
            }
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                if (rdb_Date.ToggleState == ToggleState.On)
                {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT HD.DOCID,DT.COUNTDOC ,HD.PRINTID ,HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME,CONVERT(VARCHAR, HD.TRANSDATE, 23) AS TRANSDATE,DT.PAYID,TYP.PAYDESC
                            ,DT.JOBS, DT.BROKERS, DT.CENTER, DT.MIMIGRATION, DT.HOSPITAL, DT.DISTRICT, DT.SUMPAY, DT.SUPC,DT.TOTAL
                            ,CASE HD.PAYSTAT WHEN '0' THEN 'รอยืนยันจ่าย' 
											  WHEN '1' THEN 'จ่ายแล้ว'
											  ELSE 'ไม่มีข้อมูล' END AS PAYSTAT
                            FROM CAMPING_PAYMENTHD HD 
	                             LEFT OUTER JOIN CAMPING_PAYMENTDT DT ON HD.DOCID = DT.DOCID
	                             LEFT OUTER JOIN CAMPING_PAYMENTTYPE TYP ON  TYP.PAYID = DT.PAYID 
                            WHERE HD.TRANSDATE BETWEEN '{0}' AND '{1}' AND {2}
                            ORDER BY HD.DOCID "

                            , dt_Start.Value.Date.ToString("yyyy-MM-dd")
                            , dt_End.Value.Date.ToString("yyyy-MM-dd")
                            , paystat
                            );
                }
                //, ClassCurUser.LogInEmplId
               else if (rdb_EmplId.ToggleState == ToggleState.On)
                {
                    sqlCommand.CommandText = string.Format(
                        @"SELECT HD.DOCID, DT.COUNTDOC, HD.PRINTID, HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME,CONVERT(VARCHAR, HD.TRANSDATE, 23) AS TRANSDATE,DT.PAYID,TYP.PAYDESC
                            ,DT.JOBS, DT.BROKERS, DT.CENTER, DT.MIMIGRATION, DT.HOSPITAL, DT.DISTRICT, DT.SUMPAY, DT.SUPC,DT.TOTAL
                            ,CASE HD.PAYSTAT WHEN '0' THEN 'รอยืนยันจ่าย' 
											  WHEN '1' THEN 'จ่ายแล้ว'
											  ELSE 'ไม่มีข้อมูล' END AS PAYSTAT
                            FROM CAMPING_PAYMENTHD HD 
	                             LEFT OUTER JOIN CAMPING_PAYMENTDT DT ON HD.DOCID = DT.DOCID
	                             LEFT OUTER JOIN CAMPING_PAYMENTTYPE TYP ON  TYP.PAYID = DT.PAYID
                            WHERE HD.EMPLID = '{0}' OR HD.EMPLCARD  = '{0}' AND {1}
                            ORDER BY HD.DOCID"
                        , txtEmplId.Text
                        , paystat);
                }
                else if (rdb_PI.ToggleState == ToggleState.On)
                {
                    sqlCommand.CommandText = string.Format(
                        @"SELECT HD.DOCID, DT.COUNTDOC, HD.PRINTID, HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME,CONVERT(VARCHAR, HD.TRANSDATE, 23) AS TRANSDATE,DT.PAYID,TYP.PAYDESC
                            ,DT.JOBS, DT.BROKERS, DT.CENTER, DT.MIMIGRATION, DT.HOSPITAL, DT.DISTRICT, DT.SUMPAY, DT.SUPC,DT.TOTAL
                            ,CASE HD.PAYSTAT WHEN '0' THEN 'รอยืนยันจ่าย' 
											  WHEN '1' THEN 'จ่ายแล้ว'
											  ELSE 'ไม่มีข้อมูล' END AS PAYSTAT
                            FROM CAMPING_PAYMENTHD HD 
	                             LEFT OUTER JOIN CAMPING_PAYMENTDT DT ON HD.DOCID = DT.DOCID
	                             LEFT OUTER JOIN CAMPING_PAYMENTTYPE TYP ON  TYP.PAYID = DT.PAYID
                            WHERE HD.PRINTID ='{0}'+'-'+'{1}'
                            ORDER BY HD.DOCID"
                        , txt_Date.Text.Trim()
                        , txt_PrintId.Text.Trim());
                }
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_GetData.DataSource = dt;
                }
                else
                {
                    rgv_GetData.DataSource = dt;
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

        void SetGridView()
        {
            this.rgv_GetData.Dock = DockStyle.Fill;
            this.rgv_GetData.ReadOnly = true;
            this.rgv_GetData.AutoGenerateColumns = true;
            this.rgv_GetData.EnableFiltering = false;
            this.rgv_GetData.AllowAddNewRow = false;
            this.rgv_GetData.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_GetData.ShowGroupedColumns = true;
            this.rgv_GetData.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_GetData.EnableHotTracking = true;
            this.rgv_GetData.AutoSizeRows = true;

            GridViewSummaryRowItem summary = new GridViewSummaryRowItem();            
            summary.Add(new GridViewSummaryItem("JOBS", "{0:N2}", GridAggregateFunction.Sum));
            summary.Add(new GridViewSummaryItem("BROKERS", "{0:N2}", GridAggregateFunction.Sum));
            summary.Add(new GridViewSummaryItem("CENTER", "{0:N2}", GridAggregateFunction.Sum));
            summary.Add(new GridViewSummaryItem("MIMIGRATION", "{0:N2}", GridAggregateFunction.Sum));
            summary.Add(new GridViewSummaryItem("HOSPITAL", "{0:N2}", GridAggregateFunction.Sum));
            summary.Add(new GridViewSummaryItem("DISTRICT", "{0:N2}", GridAggregateFunction.Sum));
            summary.Add(new GridViewSummaryItem("SUMPAY", "{0:N2}", GridAggregateFunction.Sum));
            summary.Add(new GridViewSummaryItem("SUPC", "{0:N2}", GridAggregateFunction.Sum));
            summary.Add(new GridViewSummaryItem("TOTAL", "{0:N2}", GridAggregateFunction.Sum));
            

            this.rgv_GetData.MasterTemplate.SummaryRowsBottom.Add(summary);
        }

        #region Print
        void exporter_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {
            string headerText = "รายงานรายละเอียดการดำเนินการต่อใบอนุญาติต่างๆ";
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
        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (rgv_GetData.RowCount != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                ExportToExcelML excelExporter = null;

                try
                {
                    ExportToExcelML excelML = new ExportToExcelML(this.rgv_GetData);
                    excelExporter = new ExportToExcelML(this.rgv_GetData);
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
    }
}
