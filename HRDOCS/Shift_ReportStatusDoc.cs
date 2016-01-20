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
    public partial class Shift_ReportStatusDoc : Form
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

        public Shift_ReportStatusDoc()
        {
            BindRestDay();
            InitializeComponent();

            InitializeDateTimePicker();
            InitializeDataGrid();
            InitializeButton();
            InitializeComboBox();
            InitializeTextBox();

        }

        #region InitializeComboBox

        private void InitializeComboBox()
        {
            Load_Section();
        }

        #endregion

        #region InitializeDateTimePicker

        private void InitializeDateTimePicker()
        {
            Dtp_Start.Text = DateTime.Now.Date.ToString();
            Dtp_End.Text = DateTime.Now.Date.ToString();
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

                    if (Rb_Section.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select  case HEADAPPROVED WHEN 1 THEN 'อนุมัติ'
					                                                                        WHEN 2 THEN 'ไม่อนุมัติ'
					                                                                        else 'รออนุมัติ' END AS 'HEADAPPROVED1',
                                                                        case HRAPPROVED WHEN 1 THEN 'อนุมัติ'
					                                                                        WHEN 2 THEN 'ไม่อนุมัติ'
					                                                                        else 'รออนุมัติ' END AS 'HRAPPROVED1',* from SPC_CM_SHIFTHD
                                                                left join SPC_CM_SHIFTDT on SPC_CM_SHIFTHD.DSDOCNO = SPC_CM_SHIFTDT.DSDOCNO
                                                                where SPC_CM_SHIFTHD.SECTIONID = '{0}' AND SPC_CM_SHIFTHD.DOCDATE BETWEEN '{1}' and '{2}' ", Cbb_Section.SelectedValue.ToString(), Dtp_Start.Value.Date.ToString("yyyy-MM-dd"), Dtp_End.Value.Date.ToString("yyyy-MM-dd"));

                    }
                    else if (Rb_Empl.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select  case HEADAPPROVED WHEN 1 THEN 'อนุมัติ'
					                                                                        WHEN 2 THEN 'ไม่อนุมัติ'
					                                                                        else 'รออนุมัติ' END AS 'HEADAPPROVED1',
                                                                        case HRAPPROVED WHEN 1 THEN 'อนุมัติ'
					                                                                        WHEN 2 THEN 'ไม่อนุมัติ'
					                                                                        else 'รออนุมัติ' END AS 'HRAPPROVED1',* from SPC_CM_SHIFTHD
                                                                left join SPC_CM_SHIFTDT on SPC_CM_SHIFTHD.DSDOCNO = SPC_CM_SHIFTDT.DSDOCNO
                                                                where SPC_CM_SHIFTHD.EMPLID = '{0}' AND SPC_CM_SHIFTHD.DOCDATE BETWEEN '{1}' and '{2}' ", Txt_EmplID.Text, Dtp_Start.Value.Date.ToString("yyyy-MM-dd"), Dtp_End.Value.Date.ToString("yyyy-MM-dd"));

                    }
                    else
                    {
                        sqlCommand.CommandText = string.Format(@"select  case HEADAPPROVED WHEN 1 THEN 'อนุมัติ'
					                                                                        WHEN 2 THEN 'ไม่อนุมัติ'
					                                                                        else 'รออนุมัติ' END AS 'HEADAPPROVED1',
                                                                        case HRAPPROVED WHEN 1 THEN 'อนุมัติ'
					                                                                        WHEN 2 THEN 'ไม่อนุมัติ'
					                                                                        else 'รออนุมัติ' END AS 'HRAPPROVED1',* from SPC_CM_SHIFTHD
                                                            left join SPC_CM_SHIFTDT on SPC_CM_SHIFTHD.DSDOCNO = SPC_CM_SHIFTDT.DSDOCNO
                                                            where SPC_CM_SHIFTHD.DSDOCNO = '{0}' ", Txt_DocNo.Text);
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

        #endregion

        #region InitializeTextBox

        private void InitializeTextBox()
        {
            Txt_DocNo.Text = "DS" + DateTime.Now.Date.ToString("yyMMdd") + "-";
        }

        #endregion

        #region Function

        void exporter_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {

            string headerText = "รายงานการเปลี่ยนกะประจำวันที่:  " + Dtp_Start.Text + " ถึง " + Dtp_End.Text;
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
                        MessageBox.Show("ไม่พบสิทธิ์ในการอนุมัติเอกสารใบเปลี่ยนกะ...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

    }
}
