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
    public partial class Leave_ReportHRApprove : Form
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


        public Leave_ReportHRApprove()
        {
            BindRestDay();
            InitializeComponent();

            InitializeDateTimePicker();
            InitializeDataGrid();
            InitializeButton();
            InitializeComboBox();
        }

        #region InitializeDateTimePicker

        private void InitializeDateTimePicker()
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
            Btn_ExportToExcel.Click += new EventHandler(Btn_ExportToExcel_Click);
            Btn_Search.Click += new EventHandler(Btn_Search_Click);
        }

        void Btn_Search_Click(object sender, EventArgs e)
        {
            string TypeDate = "";
            string Section = "";

            if (Rcb_HRApprove.Checked == true)
            {
                TypeDate = "HRAPPROVEDDATE";
            }
            else
            {
                TypeDate = "DOCDATE";
            }

            if (Rcb_Section.Checked == true)
            {
                Section = " and SPC_CM_LEAVEHD.SECTIONID = '" + Cbb_Section.SelectedValue.ToString() + "' ";
            }
            else
            {
                Section = "";
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
                        sqlCommand.CommandText = string.Format(@"select  convert(varchar,DATEDIFF(year, PWSTARTDATE, getdate()) - (CASE WHEN DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE) > getdate() THEN 1 ELSE 0 END))+'ปี '+
                                                                         convert(varchar,MONTH(getdate() - DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE)) - 1) + 'เดือน ' +
                                                                         convert(varchar,DAY(getdate() - DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE)) - 1) + 'วัน'as WORKTIME,
                                                                        case HEADAPPROVED WHEN 1 THEN 'อนุมัติ'
					                                                                        WHEN 2 THEN 'ไม่อนุมัติ'
					                                                                        else 'รออนุมัติ' END AS 'HEADAPPROVED1',
                                                                        case HRAPPROVED WHEN 1 THEN 'อนุมัติ'
					                                                                        WHEN 2 THEN 'ไม่อนุมัติ'
					                                                                        else 'รออนุมัติ' END AS 'HRAPPROVED1',
	                                                                    case HALFDAY WHEN 1 THEN 'ครึ่งวัน'
				                                                                            else 'ทั้งวัน' END AS 'HALFDAY1',
                                                                        case ATTACH WHEN 1 THEN 'มี'
				                                                                            else 'ไม่มี' END AS 'ATTACH1',* from SPC_CM_LEAVEHD
                                                                left join SPC_CM_LEAVEDT ON SPC_CM_LEAVEHD.DLDOCNO = SPC_CM_LEAVEDT.DLDOCNO
                                                                left join PWEMPLOYEE ON SPC_CM_LEAVEHD.EMPLID = PWEMPLOYEE.PWEMPLOYEE
                                                                left join 
                                                                        (
	                                                                       select PWEMPLOYEE.PWEMPLOYEE,case when PWADJTIME.PWEVENT = 'E' then 1
												                                                            when PWADJTIME.PWEVENT = 'B' then 2
												                                                            when PWADJTIME.PWEVENT = 'F' then 3
												                                                            when PWADJTIME.PWEVENT = 'C' then 6
												                                                        else 0 end as PWEVENT
				                                                                        ,sum(case when PWADJTIME.PWUPDTYPE = '=' then 1
					                                                                        else 0.5 end) as LEAVEPERSONALCOUNT   
	                                                                        from PWEMPLOYEE
	                                                                        left join PWSTOPWORK1 on PWEMPLOYEE.PWEMPLOYEE = PWSTOPWORK1.PWEMPLOYEE
	                                                                        left join PWADJTIME on PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
	                                                                        where YEAR(PWADJTIME.PWDATEADJ) = YEAR(GETDATE())
			                                                                        
	                                                                        group by PWEMPLOYEE.PWEMPLOYEE,PWADJTIME.PWEVENT
                                                                        )LEAVE on SPC_CM_LEAVEHD.EMPLID = LEAVE.PWEMPLOYEE
					                                                            AND SPC_CM_LEAVEDT.LEAVETYPE = LEAVE.PWEVENT
                                                                where SPC_CM_LEAVEHD.DOCSTAT != 0 AND SPC_CM_LEAVEHD.HEADAPPROVED = 1
                                                                        AND convert(varchar,SPC_CM_LEAVEHD.{2},23) between '{0}' and '{1}' {3} ", dtpStart.Text, dtpEnd.Text, TypeDate,Section);

                    }
                    else if (Rdb_Approve.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select  convert(varchar,DATEDIFF(year, PWSTARTDATE, getdate()) - (CASE WHEN DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE) > getdate() THEN 1 ELSE 0 END))+'ปี '+
                                                                         convert(varchar,MONTH(getdate() - DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE)) - 1) + 'เดือน ' +
                                                                         convert(varchar,DAY(getdate() - DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE)) - 1) + 'วัน'as WORKTIME,
                                                                        case HEADAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                                    WHEN 2 THEN 'ไม่อนุมัติ'
						                                                                    else 'รออนุมัติ' END AS 'HEADAPPROVED1',
	                                                                    case HRAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                                    WHEN 2 THEN 'ไม่อนุมัติ'
						                                                                    else 'รออนุมัติ' END AS 'HRAPPROVED1',
	                                                                    case HALFDAY WHEN 1 THEN 'ครึ่งวัน'
				                                                                            else 'ทั้งวัน' END AS 'HALFDAY1',
                                                                        case ATTACH WHEN 1 THEN 'มี'
				                                                                            else 'ไม่มี' END AS 'ATTACH1',* from SPC_CM_LEAVEHD
                                                                left join SPC_CM_LEAVEDT ON SPC_CM_LEAVEHD.DLDOCNO = SPC_CM_LEAVEDT.DLDOCNO
                                                                left join PWEMPLOYEE ON SPC_CM_LEAVEHD.EMPLID = PWEMPLOYEE.PWEMPLOYEE
                                                                left join 
                                                                        (
	                                                                       select PWEMPLOYEE.PWEMPLOYEE,case when PWADJTIME.PWEVENT = 'E' then 1
												                                                            when PWADJTIME.PWEVENT = 'B' then 2
												                                                            when PWADJTIME.PWEVENT = 'F' then 3
												                                                            when PWADJTIME.PWEVENT = 'C' then 6
												                                                        else 0 end as PWEVENT
				                                                                        ,sum(case when PWADJTIME.PWUPDTYPE = '=' then 1
					                                                                        else 0.5 end) as LEAVEPERSONALCOUNT   
	                                                                        from PWEMPLOYEE
	                                                                        left join PWSTOPWORK1 on PWEMPLOYEE.PWEMPLOYEE = PWSTOPWORK1.PWEMPLOYEE
	                                                                        left join PWADJTIME on PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
	                                                                        where YEAR(PWADJTIME.PWDATEADJ) = YEAR(GETDATE())
			                                                                        
	                                                                        group by PWEMPLOYEE.PWEMPLOYEE,PWADJTIME.PWEVENT
                                                                        )LEAVE on SPC_CM_LEAVEHD.EMPLID = LEAVE.PWEMPLOYEE
					                                                            AND SPC_CM_LEAVEDT.LEAVETYPE = LEAVE.PWEVENT
                                                                where SPC_CM_LEAVEHD.HRAPPROVED = 1  AND SPC_CM_LEAVEHD.DOCSTAT != 0
                                                                        AND convert(varchar,SPC_CM_LEAVEHD.{2},23) between '{0}' and '{1}' {3} ", dtpStart.Text, dtpEnd.Text, TypeDate, Section);

                    }
                    else if (Rdb_NoApprove.IsChecked == true)
                    {
                        sqlCommand.CommandText = string.Format(@"select   convert(varchar,DATEDIFF(year, PWSTARTDATE, getdate()) - (CASE WHEN DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE) > getdate() THEN 1 ELSE 0 END))+'ปี '+
                                                                         convert(varchar,MONTH(getdate() - DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE)) - 1) + 'เดือน ' +
                                                                         convert(varchar,DAY(getdate() - DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE)) - 1) + 'วัน'as WORKTIME,
                                                                        case HEADAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                                    WHEN 2 THEN 'ไม่อนุมัติ'
						                                                                    else 'รออนุมัติ' END AS 'HEADAPPROVED1',
	                                                                    case HRAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                                    WHEN 2 THEN 'ไม่อนุมัติ'
						                                                                    else 'รออนุมัติ' END AS 'HRAPPROVED1',
	                                                                    case HALFDAY WHEN 1 THEN 'ครึ่งวัน'
				                                                                            else 'ทั้งวัน' END AS 'HALFDAY1',
                                                                        case ATTACH WHEN 1 THEN 'มี'
				                                                                            else 'ไม่มี' END AS 'ATTACH1',* from SPC_CM_LEAVEHD
                                                                left join SPC_CM_LEAVEDT ON SPC_CM_LEAVEHD.DLDOCNO = SPC_CM_LEAVEDT.DLDOCNO
                                                                left join PWEMPLOYEE ON SPC_CM_LEAVEHD.EMPLID = PWEMPLOYEE.PWEMPLOYEE
                                                                left join 
                                                                        (
	                                                                       select PWEMPLOYEE.PWEMPLOYEE,case when PWADJTIME.PWEVENT = 'E' then 1
												                                                            when PWADJTIME.PWEVENT = 'B' then 2
												                                                            when PWADJTIME.PWEVENT = 'F' then 3
												                                                            when PWADJTIME.PWEVENT = 'C' then 6
												                                                        else 0 end as PWEVENT
				                                                                        ,sum(case when PWADJTIME.PWUPDTYPE = '=' then 1
					                                                                        else 0.5 end) as LEAVEPERSONALCOUNT   
	                                                                        from PWEMPLOYEE
	                                                                        left join PWSTOPWORK1 on PWEMPLOYEE.PWEMPLOYEE = PWSTOPWORK1.PWEMPLOYEE
	                                                                        left join PWADJTIME on PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
	                                                                        where YEAR(PWADJTIME.PWDATEADJ) = YEAR(GETDATE())
			                                                                        
	                                                                        group by PWEMPLOYEE.PWEMPLOYEE,PWADJTIME.PWEVENT
                                                                        )LEAVE on SPC_CM_LEAVEHD.EMPLID = LEAVE.PWEMPLOYEE
					                                                            AND SPC_CM_LEAVEDT.LEAVETYPE = LEAVE.PWEVENT
                                                                where SPC_CM_LEAVEHD.HRAPPROVED = 2  AND SPC_CM_LEAVEHD.DOCSTAT != 0
                                                                    AND convert(varchar,SPC_CM_LEAVEHD.{2},23) between '{0}' and '{1}' {3} ", dtpStart.Text, dtpEnd.Text, TypeDate, Section);
                    }
                    else
                    {
                        sqlCommand.CommandText = string.Format(@"select  convert(varchar,DATEDIFF(year, PWSTARTDATE, getdate()) - (CASE WHEN DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE) > getdate() THEN 1 ELSE 0 END))+'ปี '+
                                                                         convert(varchar,MONTH(getdate() - DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE)) - 1) + 'เดือน ' +
                                                                         convert(varchar,DAY(getdate() - DATEADD(year, DATEDIFF(year, PWSTARTDATE, getdate()), PWSTARTDATE)) - 1) + 'วัน'as WORKTIME,
                                                                        case HEADAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                                    WHEN 2 THEN 'ไม่อนุมัติ'
						                                                                    else 'รออนุมัติ' END AS 'HEADAPPROVED1',
	                                                                    case HRAPPROVED WHEN 1 THEN 'อนุมัติ'
						                                                                    WHEN 2 THEN 'ไม่อนุมัติ'
						                                                                    else 'รออนุมัติ' END AS 'HRAPPROVED1',
	                                                                    case HALFDAY WHEN 1 THEN 'ครึ่งวัน'
				                                                                            else 'ทั้งวัน' END AS 'HALFDAY1',
                                                                        case ATTACH WHEN 1 THEN 'มี'
				                                                                            else 'ไม่มี' END AS 'ATTACH1',* from SPC_CM_LEAVEHD
                                                                left join SPC_CM_LEAVEDT ON SPC_CM_LEAVEHD.DLDOCNO = SPC_CM_LEAVEDT.DLDOCNO
                                                                left join PWEMPLOYEE ON SPC_CM_LEAVEHD.EMPLID = PWEMPLOYEE.PWEMPLOYEE
                                                                left join 
                                                                        (
	                                                                       select PWEMPLOYEE.PWEMPLOYEE,case when PWADJTIME.PWEVENT = 'E' then 1
												                                                            when PWADJTIME.PWEVENT = 'B' then 2
												                                                            when PWADJTIME.PWEVENT = 'F' then 3
												                                                            when PWADJTIME.PWEVENT = 'C' then 6
												                                                        else 0 end as PWEVENT
				                                                                        ,sum(case when PWADJTIME.PWUPDTYPE = '=' then 1
					                                                                        else 0.5 end) as LEAVEPERSONALCOUNT   
	                                                                        from PWEMPLOYEE
	                                                                        left join PWSTOPWORK1 on PWEMPLOYEE.PWEMPLOYEE = PWSTOPWORK1.PWEMPLOYEE
	                                                                        left join PWADJTIME on PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
	                                                                        where YEAR(PWADJTIME.PWDATEADJ) = YEAR(GETDATE())
			                                                                        
	                                                                        group by PWEMPLOYEE.PWEMPLOYEE,PWADJTIME.PWEVENT
                                                                        )LEAVE on SPC_CM_LEAVEHD.EMPLID = LEAVE.PWEMPLOYEE
					                                                            AND SPC_CM_LEAVEDT.LEAVETYPE = LEAVE.PWEVENT
                                                                where SPC_CM_LEAVEHD.HRAPPROVED = 0  AND SPC_CM_LEAVEHD.DOCSTAT != 0 AND SPC_CM_LEAVEHD.HEADAPPROVED = 1
                                                                    AND convert(varchar,SPC_CM_LEAVEHD.{2},23) between '{0}' and '{1}' {3} ", dtpStart.Text, dtpEnd.Text, TypeDate, Section);
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

        #region InitializeComboBox

        private void InitializeComboBox()
        {
            Load_Section();
        }

        #endregion

        #region Function

        void exporter_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {

            string headerText = "รายงานการลาประจำวันที่:  " + dtpStart.Text + " ถึง " + dtpEnd.Text;
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
                        MessageBox.Show("ไม่พบข้อมูลแผนก...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
