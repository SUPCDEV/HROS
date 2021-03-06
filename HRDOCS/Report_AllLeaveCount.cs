﻿using System;
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
    public partial class Report_AllLeaveCount : Form
    {
        List<RestDayItem> listRestDay = new List<RestDayItem>();

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

        public Report_AllLeaveCount()
        {
            BindRestDay();
            InitializeComponent();

            InitializeDataGrid();
            InitializeButton();
            InitializeComboBox();
        }

        #region InitializeComboBox

        private void InitializeComboBox()
        {
            Load_Section();
            Load_Year();
        }

        #endregion

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {
            #region SetDataGrid

            Rgv_ReportAllLeaveCount.AutoGenerateColumns = false;
            Rgv_ReportAllLeaveCount.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            Rgv_ReportAllLeaveCount.Dock = DockStyle.Fill;
            Rgv_ReportAllLeaveCount.ReadOnly = true;

            #endregion
        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_ExportToExcel.Click += new EventHandler(Btn_ExportToExcel_Click);
            Btn_Search.Click += new EventHandler(Btn_Search_Click);
        }

        void Btn_Search_Click(object sender, EventArgs e)
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

                    if (Rb_All.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select PWSTOPWORK1.PWEMPLOYEE,RTRIM(LTRIM(PWEMPLOYEE.PWFNAME)) + ' ' + RTRIM(LTRIM(PWEMPLOYEE.PWLNAME)) as PWNAME
		                                                                ,PWEVENT.PWDESC as LEAVETYPE,
		                                                                sum(case when PWADJTIME.PWUPDTYPE = '=' then 1
			                                                                else 0.5 end) as LEAVECOUNT
                                                                        from PWSTOPWORK1
		                                                                left join PWEMPLOYEE on PWSTOPWORK1.PWEMPLOYEE = PWEMPLOYEE.PWEMPLOYEE
                                                                        left join PWADJTIME on PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
                                                                        left join PWEVENT on PWADJTIME.PWEVENT = PWEVENT.PWEVENT
                                                                        where YEAR(PWADJTIME.PWDATEADJ) = YEAR('{0}')
                                                                        group by PWSTOPWORK1.PWEMPLOYEE,RTRIM(LTRIM(PWEMPLOYEE.PWFNAME)) + ' ' + RTRIM(LTRIM(PWEMPLOYEE.PWLNAME)),PWEVENT.PWDESC
                                                                  ", Cbb_Year.SelectedValue.ToString());
                    }
                    else if (Rb_Section.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select PWSTOPWORK1.PWEMPLOYEE,RTRIM(LTRIM(PWEMPLOYEE.PWFNAME)) + ' ' + RTRIM(LTRIM(PWEMPLOYEE.PWLNAME)) as PWNAME
		                                                                ,PWEVENT.PWDESC as LEAVETYPE,
		                                                                sum(case when PWADJTIME.PWUPDTYPE = '=' then 1
			                                                                else 0.5 end) as LEAVECOUNT
                                                                        from PWSTOPWORK1
		                                                                left join PWEMPLOYEE on PWSTOPWORK1.PWEMPLOYEE = PWEMPLOYEE.PWEMPLOYEE
                                                                        left join PWADJTIME on PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
                                                                        left join PWEVENT on PWADJTIME.PWEVENT = PWEVENT.PWEVENT
                                                                        where  PWEMPLOYEE.PWSECTION = '{0}'
		                                                                        and YEAR(PWADJTIME.PWDATEADJ) = YEAR('{1}')
                                                                        group by PWSTOPWORK1.PWEMPLOYEE,RTRIM(LTRIM(PWEMPLOYEE.PWFNAME)) + ' ' + RTRIM(LTRIM(PWEMPLOYEE.PWLNAME)),PWEVENT.PWDESC
                                                                  ", Cbb_Section.SelectedValue.ToString(), Cbb_Year.SelectedValue.ToString());
                    }

                    else
                    {
                        sqlCommand.CommandText = string.Format(@"select PWSTOPWORK1.PWEMPLOYEE,RTRIM(LTRIM(PWEMPLOYEE.PWFNAME)) + ' ' + RTRIM(LTRIM(PWEMPLOYEE.PWLNAME)) as PWNAME
		                                                                ,PWEVENT.PWDESC as LEAVETYPE,
		                                                                sum(case when PWADJTIME.PWUPDTYPE = '=' then 1
			                                                                else 0.5 end) as LEAVECOUNT
                                                                        from PWSTOPWORK1
		                                                                left join PWEMPLOYEE on PWSTOPWORK1.PWEMPLOYEE = PWEMPLOYEE.PWEMPLOYEE
                                                                        left join PWADJTIME on PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
                                                                        left join PWEVENT on PWADJTIME.PWEVENT = PWEVENT.PWEVENT
                                                                        where  (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
		                                                                        and YEAR(PWADJTIME.PWDATEADJ) = YEAR('{1}')
                                                                        group by PWSTOPWORK1.PWEMPLOYEE,RTRIM(LTRIM(PWEMPLOYEE.PWFNAME)) + ' ' + RTRIM(LTRIM(PWEMPLOYEE.PWLNAME)),PWEVENT.PWDESC
                                                                 ", Txt_EmplID.Text, Cbb_Year.SelectedValue.ToString());
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
                    Rgv_ReportAllLeaveCount.DataSource = dataTable;
                }
                else
                {
                    Rgv_ReportAllLeaveCount.DataSource = null;
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

        void Btn_ExportToExcel_Click(object sender, EventArgs e)
        {
            if (Rgv_ReportAllLeaveCount.RowCount != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                ExportToExcelML excelExporter = null;

                try
                {
                    ExportToExcelML excelML = new ExportToExcelML(this.Rgv_ReportAllLeaveCount);
                    excelExporter = new ExportToExcelML(this.Rgv_ReportAllLeaveCount);
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

        #endregion

        #region Function

        void exporter_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {

            string headerText = "รายงานการใช้วันลา";
            if (Rb_Section.IsChecked == true)
            {
                headerText += "แผนก : " + Cbb_Section.Text;
            }
            else
            {
                headerText += "พนักงานรหัส : " + Txt_EmplID.Text;
            }

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

        void Load_Section()
        {

            Cbb_Section.Items.Clear();

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"SELECT DISTINCT * FROM(
	                                                                                    SELECT  RTRIM(PWSECTION.PWSECTION) AS PWSECTION ,RTRIM (PWSECTION.PWDESC) AS PWDESC 
	                                                                                    FROM PWEMPLOYEE  
		                                                                                        LEFT OUTER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS                                
	                                                                                   
                                                                                    ) AS PWSECTION
                                                            WHERE PWSECTION IS NOT NULL AND PWSECTION IN (SELECT PWSECTION 
												                                                            FROM SPC_CM_AUTHORIZE
												                                                            WHERE EMPLID = '{0}' AND APPROVEID = '004'
                                                            )
                                                            ORDER BY PWDESC ", ClassCurUser.LogInEmplId);

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

                    if (dataTable.Rows.Count > 0)
                    {
                        Cbb_Section.DataSource = dataTable;
                        Cbb_Section.ValueMember = "PWSECTION";
                        Cbb_Section.DisplayMember = "PWDESC";

                    }
                    else
                    {
                        MessageBox.Show("ไม่พบรายชื่อแผนกที่สามารถดึงรายงาน...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        void Load_Year()
        {

            Cbb_Year.Items.Clear();

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"select year(PWADJTIME.PWDATEADJ) as YEAR from PWADJTIME
                                                            group by year(PWADJTIME.PWDATEADJ)
                                                            order by year(PWADJTIME.PWDATEADJ) ");

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

                    if (dataTable.Rows.Count > 0)
                    {
                        Cbb_Year.DataSource = dataTable;
                        Cbb_Year.ValueMember = "YEAR";
                        Cbb_Year.DisplayMember = "YEAR";
                        Cbb_Year.SelectedIndex = dataTable.Rows.Count;
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลการลาในแต่ล่ะปี...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        #endregion

        //-----By JN 24-11-25  เพิ่มปุ่มดูรายเอียดวันที่ลา------------------------------------------------------------------------
        private void Rgv_ReportAllLeaveCount_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (!(e.CellElement.RowElement is GridTableHeaderRowElement))
            {
                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    GridViewCommandColumn column = (GridViewCommandColumn)e.CellElement.ColumnInfo;
                    RadButtonElement button = (RadButtonElement)e.CellElement.Children[0];

                    if (column.Name == "Detail")
                    {
                        RadButtonElement element = (RadButtonElement)e.CellElement.Children[0];
                        element.Text = "รายละเอียด";
                        element.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                }
            }
        }
        private void Rgv_ReportAllLeaveCount_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string GetYear = Cbb_Year.SelectedValue.ToString();

                if (e.Column.Name == "Detail")
                {
                    GridViewRowInfo row = (GridViewRowInfo)Rgv_ReportAllLeaveCount.Rows[e.RowIndex];
                    string EmplId = row.Cells["PWEMPLOYEE"].Value.ToString();
                    string EmpName = row.Cells["PWNAME"].Value.ToString();
                    string LeveType = row.Cells["LEAVETYPE"].Value.ToString().Trim();

                    using (Leave_CheckLeave_Detail frm = new Leave_CheckLeave_Detail(EmplId, EmpName, LeveType, GetYear))
                    {
                        //frm.Show();
                        if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------------


    }
}
