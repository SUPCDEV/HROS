using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using System.IO;
using System.Data.SqlClient;
using SysApp;

namespace HRDOCS
{
    public partial class Shift_ReportOT : Form
    {
        List<RestDayItem> listRestDay = new List<RestDayItem>();

        public Shift_ReportOT()
        {
            BindRestDay();
            InitializeComponent();

            InitailizeDateTimePicker();
            InitializeDataGrid();
            InitializeButton();
        }

        #region InitailizeDateTimePicker

        private void InitailizeDateTimePicker()
        {
            dtpStart.Text = DateTime.Now.Date.ToString();
            dtpEnd.Text = DateTime.Now.Date.ToString();

        }

        #endregion

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {
            #region SetDataGrid


            Rgv_ReportApprove.AutoGenerateColumns = false;

            //var  cell1 = this.Rgv_ReportApprove.Columns["RESTDAy1"];
            //cell1.DataSourceNullValue = listRestDay;
            this.Rgv_ReportApprove.Columns.Add(
                new GridViewComboBoxColumn()
                {
                    Name = "RESTDAY1",
                    FieldName = "RESTDAY1",
                    HeaderText = "วันหยุด1",
                    DisplayMember = "RestDayName",
                    ValueMember = "RestDayIdx",
                    DataSource = listRestDay,
                    DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
                });

            this.Rgv_ReportApprove.Columns.Add(
                new GridViewComboBoxColumn()
                {
                    Name = "RESTDAY2",
                    FieldName = "RESTDAY2",
                    HeaderText = "วันหยุด2",
                    DisplayMember = "RestDayName",
                    ValueMember = "RestDayIdx",
                    DataSource = listRestDay,
                    DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
                });


            Rgv_ReportApprove.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            Rgv_ReportApprove.Dock = DockStyle.Fill;
            Rgv_ReportApprove.ReadOnly = true;





            #endregion


        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_Search.Click += new EventHandler(Btn_Search_Click);
            Btn_ExportToExcel.Click += new EventHandler(Btn_ExportToExcel_Click);
        }

        void Btn_ExportToExcel_Click(object sender, EventArgs e)
        {
            if (Rgv_ReportApprove.RowCount != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                ExportToExcelML excelExporter = null;

                try
                {
                    ExportToExcelML excelML = new ExportToExcelML(this.Rgv_ReportApprove);
                    excelExporter = new ExportToExcelML(this.Rgv_ReportApprove);
                    excelExporter.ExcelCellFormatting += excelExporter_ExcelCellFormatting;
                    excelExporter.ExcelTableCreated += exporter_ExcelTableCreated;

                    this.Cursor = Cursors.WaitCursor;
                    saveFileDialog.Filter = "Excel (*.xls)|*.xls";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        excelExporter.RunExport(saveFileDialog.FileName);

                        DialogResult dr = MessageBox.Show("การบันทึกไฟล์สำเร็จ คุณต้องการเปิดไฟล์หรือไม่?",
                            "Export to Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                MessageBox.Show("ไม่มีข้อมูล");
                return;
            }
        }

        void Btn_Search_Click(object sender, EventArgs e)
        {
            string TypeDate = "";

            if (Rcb_HRApprove.Checked == true)
            {
                TypeDate = "HRAPPROVEDDATE";
            }
            else
            {
                TypeDate = "DOCDATE";
            }

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //Rgv_ReportApprove.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;

                    if (Rdb_All.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select case HEADAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                            WHEN 2 THEN 'ไม่อนุมัติ'
						                                                            else 'รออนุมัติ' END AS 'HEADAPPROVED1',
	                                                            case HRAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                            WHEN 2 THEN 'ไม่อนุมัติ'
						                                                            else 'รออนุมัติ' END AS 'HRAPPROVED1',* from SPC_CM_SHIFTHD
                                                            left join SPC_CM_SHIFTDT on SPC_CM_SHIFTHD.DSDOCNO = SPC_CM_SHIFTDT.DSDOCNO
                                                            where  SPC_CM_SHIFTHD.DOCSTAT != 0 AND SPC_CM_SHIFTHD.HEADAPPROVED = 1
                                                            AND (remark like '%โอที%' OR remark like '%โอ.ที%' 
                                                            OR remark like '%OT%' OR remark like '%O.T%' OR remark like '%ชดเชย%'
                                                            OR len(REFOTTIME) > 3 )
                                                            AND convert(varchar,SPC_CM_SHIFTHD.{2},23) BETWEEN '{0}' and '{1}' ", dtpStart.Text, dtpEnd.Text, TypeDate);

                    }
                    else if (Rdb_Approve.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select case HEADAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                            WHEN 2 THEN 'ไม่อนุมัติ'
						                                                            else 'รออนุมัติ' END AS 'HEADAPPROVED1',
	                                                            case HRAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                            WHEN 2 THEN 'ไม่อนุมัติ'
						                                                            else 'รออนุมัติ' END AS 'HRAPPROVED1',* from SPC_CM_SHIFTHD
                                                            left join SPC_CM_SHIFTDT on SPC_CM_SHIFTHD.DSDOCNO = SPC_CM_SHIFTDT.DSDOCNO
                                                            where SPC_CM_SHIFTHD.HRAPPROVED = 1  AND SPC_CM_SHIFTHD.DOCSTAT != 0
                                                            AND (remark like '%โอที%' OR remark like '%โอ.ที%' 
                                                            OR remark like '%OT%' OR remark like '%O.T%' OR remark like '%ชดเชย%'
                                                            OR len(REFOTTIME) > 3 )
                                                            AND convert(varchar,SPC_CM_SHIFTHD.{2},23) BETWEEN '{0}' and '{1}' ", dtpStart.Text, dtpEnd.Text, TypeDate);

                    }
                    else if (Rdb_NoApprove.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select case HEADAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                            WHEN 2 THEN 'ไม่อนุมัติ'
						                                                            else 'รออนุมัติ' END AS 'HEADAPPROVED1',
	                                                            case HRAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                            WHEN 2 THEN 'ไม่อนุมัติ'
						                                                            else 'รออนุมัติ' END AS 'HRAPPROVED1',* from SPC_CM_SHIFTHD
                                                            left join SPC_CM_SHIFTDT on SPC_CM_SHIFTHD.DSDOCNO = SPC_CM_SHIFTDT.DSDOCNO
                                                            where  SPC_CM_SHIFTHD.HRAPPROVED = 2  AND SPC_CM_SHIFTHD.DOCSTAT != 0 
                                                            AND (remark like '%โอที%' OR remark like '%โอ.ที%' 
                                                            OR remark like '%OT%' OR remark like '%O.T%' OR remark like '%ชดเชย%'
                                                            OR len(REFOTTIME) > 3 )
                                                            AND convert(varchar,SPC_CM_SHIFTHD.{2},23) BETWEEN '{0}' and '{1}' ", dtpStart.Text, dtpEnd.Text, TypeDate);

                    }
                    else 
                    {
                        sqlCommand.CommandText = string.Format(@"select case HEADAPPROVED WHEN 1 THEN 'เธญเธเธธเธกเธฑเธ•เธด'
						                                                            WHEN 2 THEN 'เนเธกเนเธญเธเธธเธกเธฑเธ•เธด'
						                                                            else 'เธฃเธญเธญเธเธธเธกเธฑเธ•เธด' END AS 'HEADAPPROVED1',
	                                                            case HRAPPROVED WHEN 1 THEN 'เธญเธเธธเธกเธฑเธ•เธด'
						                                                            WHEN 2 THEN 'เนเธกเนเธญเธเธธเธกเธฑเธ•เธด'
						                                                            else 'เธฃเธญเธญเธเธธเธกเธฑเธ•เธด' END AS 'HRAPPROVED1',* from SPC_CM_SHIFTHD
                                                            left join SPC_CM_SHIFTDT on SPC_CM_SHIFTHD.DSDOCNO = SPC_CM_SHIFTDT.DSDOCNO
                                                            where SPC_CM_SHIFTHD.HRAPPROVED = 0 AND SPC_CM_SHIFTHD.DOCSTAT != 0
                                                             AND (remark like '%โอที%' OR remark like '%โอ.ที%' 
                                                            OR remark like '%OT%' OR remark like '%O.T%' OR remark like '%ชดเชย%'
                                                            OR len(REFOTTIME) > 3 )
                                                            AND convert(varchar,SPC_CM_SHIFTHD.{2},23) BETWEEN '{0}' and '{1}' ", dtpStart.Text, dtpEnd.Text, TypeDate);
                    }

                    
                    

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
                    Rgv_ReportApprove.DataSource = dataTable;

                }
                else
                {
                    Rgv_ReportApprove.DataSource = null;
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

        #endregion

        #region Function

        void exporter_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {

            string headerText = "รายงานการเปลี่ยนกะประจำวันที่:  " + dtpStart.Text + " ถึง " + dtpEnd.Text;
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

        public class RestDayItem
        {
            public RestDayItem(int _restdayIdx, string _restDayName)
            {
                RestDayIdx = _restdayIdx;
                RestDayName = _restDayName;
            }
            public int RestDayIdx { get; set; }
            public string RestDayName { get; set; }
        }

        public enum RestDayTH
        {
            อาทิตย์ = 0, จันทร์ = 1, อังคาร = 2, พุธ = 3, พฤหัสบดี = 4, ศุกร์ = 5, เสาร์ = 6
        }

        void BindRestDay()
        {
            foreach (string name in System.Enum.GetNames(typeof(RestDayTH)))
            {
                listRestDay.Add(new RestDayItem(listRestDay.Count, name));
            }
        }


        #endregion


    }
}
