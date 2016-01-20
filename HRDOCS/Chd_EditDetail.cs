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
    public partial class Chd_EditDetail : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string _docid;

        string emplcard = "";
        string sectionid = "";
        string deptid = "";
        string positionid = "";
        string position = "";
        string holiday1 = "";
        string holiday2 = "";

        public Chd_EditDetail(string DOCID)
        {
            _docid = DOCID;

            InitializeComponent();
            InitializeradGridview();
            InitializeDateTimePicker();
            InitializeDropDownList();

            this.btn_AddData.Click += new EventHandler(btn_AddData_Click);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);

            this.txtDocId.Text = _docid;
        }

        private void Chd_EditDetail_Load(object sender, EventArgs e)
        {
            GetData();
        }
        private void InitializeradGridview()
        {

            rgv_changholiday.Dock = DockStyle.Fill;
            rgv_changholiday.AutoGenerateColumns = true;
            rgv_changholiday.EnableFiltering = false;
            rgv_changholiday.AllowAddNewRow = false;
            rgv_changholiday.MasterTemplate.AutoGenerateColumns = false;
            rgv_changholiday.ShowGroupedColumns = true;
            rgv_changholiday.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgv_changholiday.EnableHotTracking = true;
            rgv_changholiday.AutoSizeRows = true;

            GridViewTextBoxColumn FROMHOLIDAY = new GridViewTextBoxColumn();
            FROMHOLIDAY.Name = "FROMHOLIDAY";
            FROMHOLIDAY.FieldName = "FROMHOLIDAY";
            FROMHOLIDAY.HeaderText = "วันที่มาทำ";
            FROMHOLIDAY.IsVisible = true;
            FROMHOLIDAY.ReadOnly = true;
            FROMHOLIDAY.BestFit();
            rgv_changholiday.Columns.Add(FROMHOLIDAY);

            GridViewTextBoxColumn TOSHIFTID = new GridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.FieldName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "รหัสกะ";
            TOSHIFTID.IsVisible = true;
            TOSHIFTID.ReadOnly = true;
            TOSHIFTID.BestFit();
            rgv_changholiday.Columns.Add(TOSHIFTID);

            GridViewTextBoxColumn TOSHIFTDESC = new GridViewTextBoxColumn();
            TOSHIFTDESC.Name = "TOSHIFTDESC";
            TOSHIFTDESC.FieldName = "TOSHIFTDESC";
            TOSHIFTDESC.HeaderText = "คำอธิบาย";
            TOSHIFTDESC.IsVisible = true;
            TOSHIFTDESC.ReadOnly = true;
            TOSHIFTDESC.BestFit();
            rgv_changholiday.Columns.Add(TOSHIFTDESC);

            GridViewTextBoxColumn TOHOLIDAY = new GridViewTextBoxColumn();
            TOHOLIDAY.Name = "TOHOLIDAY";
            TOHOLIDAY.FieldName = "TOHOLIDAY";
            TOHOLIDAY.HeaderText = "วันที่หยุด";
            TOHOLIDAY.IsVisible = true;
            TOHOLIDAY.ReadOnly = true;
            TOHOLIDAY.BestFit();
            rgv_changholiday.Columns.Add(TOHOLIDAY);

            GridViewTextBoxColumn REASON = new GridViewTextBoxColumn();
            REASON.Name = "REASON";
            REASON.FieldName = "REASON";
            REASON.HeaderText = "เหตุผล";
            REASON.IsVisible = true;
            REASON.ReadOnly = true;
            REASON.BestFit();
            rgv_changholiday.Columns.Add(REASON);

            GridViewCommandColumn BTDELETE = new GridViewCommandColumn();
            BTDELETE.Name = "BTDELETE";
            BTDELETE.HeaderText = "ยกเลิก";
            BTDELETE.Width = 20;
            rgv_changholiday.Columns.Add(BTDELETE);

        }
        private void InitializeDateTimePicker()
        {
            this.dtFromHoliday.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dtToHolidat.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }
        private void InitializeDropDownList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("HolidayIndex", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("HolidayName", typeof(System.String)));
            dt.Rows.Add(0, "อาทิตย์");
            dt.Rows.Add(1, "จันทร์");
            dt.Rows.Add(2, "อังคาร");
            dt.Rows.Add(3, "พุธ");
            dt.Rows.Add(4, "พฤหัสบดี");
            dt.Rows.Add(5, "ศุกร์");
            dt.Rows.Add(6, "เสาร์");

            ddlholiday1.DataSource = dt;
            ddlholiday1.DisplayMember = "HolidayName";
            ddlholiday1.ValueMember = "HolidayIndex";

            DataTable dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("HolidayIndex", typeof(System.Int32)));
            dt2.Columns.Add(new DataColumn("HolidayName", typeof(System.String)));
            dt2.Rows.Add(0, "อาทิตย์");
            dt2.Rows.Add(1, "จันทร์");
            dt2.Rows.Add(2, "อังคาร");
            dt2.Rows.Add(3, "พุธ");
            dt2.Rows.Add(4, "พฤหัสบดี");
            dt2.Rows.Add(5, "ศุกร์");
            dt2.Rows.Add(6, "เสาร์");

            ddlholiday2.DataSource = dt2;
            ddlholiday2.DisplayMember = "HolidayName";
            ddlholiday2.ValueMember = "HolidayIndex";

            //Ddl_Rest1.SelectedIndex = 0;

        }
        void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                sqlCommand.CommandText = string.Format(
                    @"SELECT * FROM SPC_JN_CHANGHOLIDAYHD
                          WHERE DOCID = '{0}' "
                    //, txtDocId.Text.Trim()
                    , _docid);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtEmpId.Text = reader["EMPLID"].ToString();
                        txtEmplName.Text = reader["EMPLNAME"].ToString().Trim();
                        txtDept.Text = reader["DEPTNAME"].ToString().Trim();
                        txtSection.Text = reader["SECTIONNAME"].ToString();
                        holiday1 = reader["HOLIDAY1"].ToString();
                        holiday1 = ddlholiday1.SelectedValue.ToString();
                        holiday2 = reader["HOLIDAY2"].ToString();
                        // holiday2 = ddlholiday2.SelectedItem.ToString();

                    }
                }
                else
                {
                    MessageBox.Show("ไม่มีข้อมูล");
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

            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {

                DataTable dt = new DataTable();
                //SqlTransaction tr = con.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                //sqlCommand.Transaction = tr;

                sqlCommand.CommandText = string.Format(
                        @"SELECT * FROM SPC_JN_CHANGHOLIDAYDT 
                          WHERE DOCID = '{0}' ", _docid);
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_changholiday.DataSource = dt;
                }
                else if (dt.Rows.Count < 1)
                {
                    rgv_changholiday.DataSource = dt;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        void btn_AddData_Click(object sender, EventArgs e)
        {
            var _fromdate = dtFromHoliday.Value.Date.ToString("yyyy-MM-dd");
            var _todate = dtToHolidat.Value.Date.ToString("yyyy-MM-dd");

            if (_todate != "" && _todate == _fromdate && rdbOpen.ToggleState == ToggleState.On)
            {
                MessageBox.Show("โปรดตรวจสอบวันที่ต้องการเปลี่ยนวันหยุดอีกครั้ง ");
                return;
            }

            if (rgv_changholiday.Rows.Count > 0 && rdbOpen.ToggleState == ToggleState.On) //ถ้า GriewView เป็นค่าว่าง && เลือก rdbOpen
            {
                for (int i = 0; i <= rgv_changholiday.Rows.Count - 1; i++)
                {
                    if (_fromdate == rgv_changholiday.Rows[i].Cells["FROMHOLIDAY"].Value.ToString())
                    {
                        MessageBox.Show("คุณได้เลือกวันที่มาทำงานเป็นวันที่ " + dtFromHoliday.Value.Date.ToString("yyyy-MM-dd") + " ไปแล้วโปรดเปลี่ยนเป็นวันอื่น", System.Reflection.MethodBase.GetCurrentMethod().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (_todate == rgv_changholiday.Rows[i].Cells["TOHOLIDAY"].Value.ToString()) // ถ้าวันที่ที่เปลี่ยนและวันที่มาทำเป็นวันเดียวกัน
                    {
                        MessageBox.Show("คุณได้เปลี่ยนวันที่ " + dtToHolidat.Value.Date.ToString("yyyy-MM-dd") + " เป็นวันหยุดไปแล้ว โปรดเปลี่ยนเป็นวันอื่น", System.Reflection.MethodBase.GetCurrentMethod().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (txtShift.Text == "" || txtShiftDesc.Text == "" || txtreason.Text == "")
            {
                MessageBox.Show("กรุณาใส่ข้อมูลให้ครบ...", System.Reflection.MethodBase.GetCurrentMethod().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rdbClose.ToggleState == ToggleState.On)
            {
                this.rgv_changholiday.Rows.Add(dtFromHoliday.Value.Date.ToString("yyyy-MM-dd"), txtShift.Text, txtShiftDesc.Text, dtToHolidat.Value.Date.ToString("0000-00-00")
                     , txtreason.Text);

                rgv_changholiday.Invoke(new EventHandler(delegate
                {
                    this.txtShift.Text = "";
                    this.txtShiftDesc.Text = "";
                    this.txtreason.Text = "";
                }));

                this.btnSave.Enabled = true;
            }

            else
            {
                this.rgv_changholiday.Rows.Add(dtFromHoliday.Value.Date.ToString("yyyy-MM-dd"), txtShift.Text, txtShiftDesc.Text, dtToHolidat.Value.Date.ToString("yyyy-MM-dd")
                    , txtreason.Text);

                rgv_changholiday.Invoke(new EventHandler(delegate
                {
                    this.txtShift.Text = "";
                    this.txtShiftDesc.Text = "";
                    this.txtreason.Text = "";
                }));

                this.btnSave.Enabled = true;
            }
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            using (Chd_SelectShift frm = new Chd_SelectShift())
            {
                frm.Text = "ค้นหากะ";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {
                    txtShift.Text = frm.Shift;
                    txtShiftDesc.Text = frm.ShiftDesc;
                }
            }
            //}
        }
        void btnSave_Click(object sender, EventArgs e)
        {

            //SqlCommand sqlCommand = new SqlCommand();
            //sqlCommand.Connection = con;
            DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            SqlTransaction tr = null;

            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            tr = con.BeginTransaction();
            //sqlCommand.Transaction = tr;
            if (rgv_changholiday.Rows.Count > 0)
            {
                try
                {

                    #region Update

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = con;
                        sqlCommand.CommandText = string.Format(
                            @" UPDATE SPC_JN_CHANGHOLIDAYHD SET HOLIDAY1 = @HOLIDAY1
								                            ,HOLIDAY2 = @HOLIDAY2                                    
                                                            ,MODIFIEDBY = '{0}'
								                            ,MODIFIEDNAME = '{1}'
								                            ,MODIFIEDDATE = convert(varchar, getdate(), 23) 
                            WHERE SPC_JN_CHANGHOLIDAYHD.DOCID = @DOCID
                            AND DOCSTAT = 1 "
                            , ClassCurUser.LogInEmplId
                            , ClassCurUser.LogInEmplName);

                        sqlCommand.Transaction = tr;

                        sqlCommand.Parameters.AddWithValue("@DOCID", _docid.ToString().Trim());
                        sqlCommand.Parameters.AddWithValue("@HOLIDAY1", ddlholiday1.Text);
                        sqlCommand.Parameters.AddWithValue("@HOLIDAY2", ddlholiday2.Text);
                        sqlCommand.ExecuteNonQuery();
                    }


                    #endregion

                    #region deleteDT

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = con;
                        sqlCommand.CommandText = string.Format(
                                @" DELETE FROM SPC_JN_CHANGHOLIDAYDT 
                            WHERE SPC_JN_CHANGHOLIDAYDT.DOCID = @DOCID ");
                        sqlCommand.Transaction = tr;
                        sqlCommand.Parameters.AddWithValue("@DOCID", _docid.ToString().Trim());
                        sqlCommand.ExecuteNonQuery();
                    }

                    #endregion

                    #region Insert DT
                    foreach (GridViewDataRowInfo row in rgv_changholiday.Rows)
                    {
                        if (row.Index > -1)
                        {
                            using (SqlCommand sqlCommand = new SqlCommand())
                            {
                                sqlCommand.Connection = con;
                                sqlCommand.CommandText = string.Format(
                                                        @"INSERT INTO [dbo].[SPC_JN_CHANGHOLIDAYDT](
                                                             DOCID , COUNTDOC ,FROMHOLIDAY
                                                            ,TOHOLIDAY ,TOSHIFTID ,TOSHIFTDESC ,REASON                                               
                                                            ,MODIFIEDDATE
                                                            )
                                                     VALUES (
                                                             @DOCID2{0},@COUNTDOC{0},@FROMHOLIDAY{0}
                                                            ,@TOHOLIDAY{0} ,@TOSHIFTID{0} ,@TOSHIFTDESC{0} ,@REASON{0}
                                                            ,convert(varchar, getdate(), 23)
                                                            )"
                                                        , row.Index);
                                sqlCommand.Transaction = tr;

                                sqlCommand.Parameters.AddWithValue(string.Format(@"DOCID2{0}", row.Index), _docid);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"COUNTDOC{0}", row.Index), row.Index + 1);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"FROMHOLIDAY{0}", row.Index), row.Cells["FROMHOLIDAY"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"TOHOLIDAY{0}", row.Index), row.Cells["TOHOLIDAY"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"TOSHIFTID{0}", row.Index), row.Cells["TOSHIFTID"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"REASON{0}", row.Index), row.Cells["REASON"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"TOSHIFTDESC{0}", row.Index), row.Cells["TOSHIFTDESC"].Value.ToString());

                                sqlCommand.ExecuteNonQuery();

                            }

                        }

                    }
                    #endregion

                    #region submit

                    tr.Commit();
                    this.IsSaveOnce = true;
                    MessageBox.Show("การบันทึกเอกสารสำเร็จ เลขที่เอกสาร : " + _docid.ToString().Trim() + " สำเร็จ");

                    if (con.State == ConnectionState.Open) con.Close();
                    this.Close();

                    #endregion

                }
                catch (Exception ex)
                {
                    if (tr != null)
                    {
                        tr.Rollback();
                    }
                    MessageBox.Show(ex.ToString());
                }
                this.DialogResult = DialogResult.Yes;
            }
            else
            {
                MessageBox.Show("ไม่มีข้อมูล โปรดใส่ข้อมูลก่อนบันทึก");
                return;
            }
            

        }



        private void rgv_changholiday_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (!(e.CellElement.RowElement is GridTableHeaderRowElement))
            {
                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    GridViewCommandColumn column = (GridViewCommandColumn)e.CellElement.ColumnInfo;
                    RadButtonElement button = (RadButtonElement)e.CellElement.Children[0];

                    if (column.Name == "BTDELETE")
                    {
                        RadButtonElement element = (RadButtonElement)e.CellElement.Children[0];
                        element.DisplayStyle = DisplayStyle.Image;

                        element.Image = Properties.Resources.Close;
                        element.ImageAlignment = ContentAlignment.MiddleCenter;

                    }
                }
            }
        }

        private void rgv_changholiday_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "BTDELETE")
            {
                if (this.rgv_changholiday.SelectedRows.Count > 0)
                {
                    rgv_changholiday.Rows.RemoveAt(this.rgv_changholiday.SelectedRows[0].Index);
                }
            }
        }

    }
}
