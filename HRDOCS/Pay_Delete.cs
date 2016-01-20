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
    public partial class Pay_Delete : Form
    {
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        //string _InvioceId = "";
        //string _InvioceDate = "";
        //string _docid = "";
        //string _sumsupc = "";
        //string _suminvoice = "";

        #region
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

        public Pay_Delete()
        {
            InitializeComponent();
            InitializeGridView();

            btn_Search.Click += new EventHandler(btn_Search_Click);
            this.txtEmplId.Focus();
        }

        private void Pay_Delete_Load(object sender, EventArgs e)
        {
            this.txtPay.Text = "PAY";
            this.txtDate.Text = DateTime.Now.ToString("yyMMdd");
            this.txtDocNo.Focus();
        }


        void btn_Search_Click(object sender, EventArgs e)
        {
            GetData();
        }
        void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                if (rdb_Empl.ToggleState == ToggleState.On)
                {
                    sqlCommand.CommandText = string.Format(
                        @"SELECT DOCID, EMPLID, EMPLNAME, SECTIONNAME,CREATEDNAME
                            ,CONVERT(VARCHAR, TRANSDATE, 23) AS TRANSDATE
                            FROM CAMPING_PAYMENTHD  
                            WHERE PRINTSTAT = '0'
                            AND EMPLID = '{0}' OR EMPLCARD = '{0}' 
                            AND DOCSTAT = 1"
                        , txtEmplId.Text.Trim());
                    
                }
                else if (rdb_DocId.ToggleState == ToggleState.On)
                {
                    sqlCommand.CommandText = string.Format(
                        @"SELECT DOCID, EMPLID, EMPLNAME, SECTIONNAME,CREATEDNAME
                            ,CONVERT(VARCHAR, TRANSDATE, 23) AS TRANSDATE
                            FROM CAMPING_PAYMENTHD  
                            WHERE PRINTSTAT = '0'                            
                            AND DOCID = '{0}'  +  '{1}' + '-' + '{2}' 
                            AND DOCSTAT = 1 "
                        , txtPay.Text.Trim()
                        , txtDate.Text.Trim()
                        , txtDocNo.Text.Trim());
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
        }
        void InitializeGridView()
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

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_GetData.Columns.Add(DOCID);

            GridViewTextBoxColumn EMPLID = new GridViewTextBoxColumn();
            EMPLID.Name = "EMPLID";
            EMPLID.FieldName = "EMPLID";
            EMPLID.HeaderText = "รหัส";
            EMPLID.IsVisible = true;
            EMPLID.ReadOnly = true;
            EMPLID.BestFit();
            rgv_GetData.Columns.Add(EMPLID);

            GridViewTextBoxColumn EMPLNAME = new GridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.FieldName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.IsVisible = true;
            EMPLNAME.ReadOnly = true;
            EMPLNAME.BestFit();
            rgv_GetData.Columns.Add(EMPLNAME);

            GridViewTextBoxColumn SECTIONNAME = new GridViewTextBoxColumn();
            SECTIONNAME.Name = "SECTIONNAME";
            SECTIONNAME.FieldName = "SECTIONNAME";
            SECTIONNAME.HeaderText = "แผนก";
            SECTIONNAME.IsVisible = true;
            SECTIONNAME.ReadOnly = true;
            SECTIONNAME.BestFit();
            rgv_GetData.Columns.Add(SECTIONNAME);

            GridViewTextBoxColumn CREATEDNAME = new GridViewTextBoxColumn();
            CREATEDNAME.Name = "CREATEDNAME";
            CREATEDNAME.FieldName = "CREATEDNAME";
            CREATEDNAME.HeaderText = "ผู้บันทึก";
            CREATEDNAME.IsVisible = true;
            CREATEDNAME.ReadOnly = true;
            CREATEDNAME.BestFit();
            rgv_GetData.Columns.Add(CREATEDNAME);



            GridViewTextBoxColumn TRANSDATE = new GridViewTextBoxColumn();
            TRANSDATE.Name = "TRANSDATE";
            TRANSDATE.FieldName = "TRANSDATE";
            TRANSDATE.HeaderText = "วันที่บันทึก";
            TRANSDATE.IsVisible = true;
            TRANSDATE.ReadOnly = true;
            TRANSDATE.BestFit();
            rgv_GetData.Columns.Add(TRANSDATE);

            //GridViewTextBoxColumn BROKERS = new GridViewTextBoxColumn();
            //BROKERS.Name = "BROKERS";
            //BROKERS.FieldName = "BROKERS";
            //BROKERS.HeaderText = "โบรกเกอร์";
            //BROKERS.IsVisible = true;
            //BROKERS.ReadOnly = true;
            //BROKERS.BestFit();
            //rgv_GetData.Columns.Add(BROKERS);

            //GridViewTextBoxColumn CENTER = new GridViewTextBoxColumn();
            //CENTER.Name = "CENTER";
            //CENTER.FieldName = "CENTER";
            //CENTER.HeaderText = "ศูนย์พูลผล";
            //CENTER.IsVisible = true;
            //CENTER.ReadOnly = true;
            //CENTER.BestFit();
            //rgv_GetData.Columns.Add(CENTER);

            //GridViewTextBoxColumn MIMIGRATION = new GridViewTextBoxColumn();
            //MIMIGRATION.Name = "MIMIGRATION";
            //MIMIGRATION.FieldName = "MIMIGRATION";
            //BROKERS.HeaderText = "ตม.";
            //MIMIGRATION.IsVisible = true;
            //MIMIGRATION.ReadOnly = true;
            //MIMIGRATION.BestFit();
            //rgv_GetData.Columns.Add(MIMIGRATION);

            //GridViewTextBoxColumn HOSPITAL = new GridViewTextBoxColumn();
            //HOSPITAL.Name = "HOSPITAL";
            //HOSPITAL.FieldName = "HOSPITAL";
            //HOSPITAL.HeaderText = "รพ.";
            //HOSPITAL.IsVisible = true;
            //HOSPITAL.ReadOnly = true;
            //HOSPITAL.BestFit();
            //rgv_GetData.Columns.Add(HOSPITAL);

            //GridViewTextBoxColumn DISTRICT = new GridViewTextBoxColumn();
            //DISTRICT.Name = "DISTRICT";
            //DISTRICT.FieldName = "DISTRICT";
            //DISTRICT.HeaderText = "อำเภอ";
            //DISTRICT.IsVisible = true;
            //DISTRICT.ReadOnly = true;
            //DISTRICT.BestFit();
            //rgv_GetData.Columns.Add(DISTRICT);

            //GridViewTextBoxColumn SUMPAY = new GridViewTextBoxColumn();
            //SUMPAY.Name = "SUMPAY";
            //SUMPAY.FieldName = "SUMPAY";
            //SUMPAY.HeaderText = "รวม";
            //SUMPAY.IsVisible = true;
            //SUMPAY.ReadOnly = true;
            //SUMPAY.BestFit();
            //rgv_GetData.Columns.Add(SUMPAY);

            //GridViewTextBoxColumn SUPC = new GridViewTextBoxColumn();
            //SUPC.Name = "SUPC";
            //SUPC.FieldName = "SUPC";
            //SUPC.HeaderText = "SUPC";
            //SUPC.IsVisible = true;
            //SUPC.ReadOnly = true;
            //SUPC.BestFit();
            //rgv_GetData.Columns.Add(SUPC);

            //GridViewTextBoxColumn TOTAL = new GridViewTextBoxColumn();
            //TOTAL.Name = "TOTAL";
            //TOTAL.FieldName = "TOTAL";
            //TOTAL.HeaderText = "ยอดเรียกเก็บ";
            //TOTAL.IsVisible = true;
            //TOTAL.ReadOnly = true;
            //TOTAL.BestFit();
            //rgv_GetData.Columns.Add(TOTAL);

            GridViewCommandColumn DeleteButton = new GridViewCommandColumn();
            DeleteButton.Name = "DeleteButton";
            DeleteButton.FieldName = "DeleteButton";
            DeleteButton.HeaderText = "ยกเลิก";
            rgv_GetData.Columns.Add(DeleteButton);


            //GridViewSummaryRowItem summary = new GridViewSummaryRowItem();
            //summary.Add(new GridViewSummaryItem("JOBS", "{0:N2}", GridAggregateFunction.Sum));
            //summary.Add(new GridViewSummaryItem("BROKERS", "{0:N2}", GridAggregateFunction.Sum));
            //summary.Add(new GridViewSummaryItem("CENTER", "{0:N2}", GridAggregateFunction.Sum));
            //summary.Add(new GridViewSummaryItem("MIMIGRATION", "{0:N2}", GridAggregateFunction.Sum));
            //summary.Add(new GridViewSummaryItem("HOSPITAL", "{0:N2}", GridAggregateFunction.Sum));
            //summary.Add(new GridViewSummaryItem("DISTRICT", "{0:N2}", GridAggregateFunction.Sum));
            //summary.Add(new GridViewSummaryItem("SUMPAY", "{0:N2}", GridAggregateFunction.Sum));
            //summary.Add(new GridViewSummaryItem("SUPC", "{0:N2}", GridAggregateFunction.Sum));
            //summary.Add(new GridViewSummaryItem("TOTAL", "{0:N2}", GridAggregateFunction.Sum));
            //this.rgv_GetData.MasterTemplate.SummaryRowsBottom.Add(summary);
        }
        private void rgv_GetData_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (!(e.CellElement.RowElement is GridTableHeaderRowElement))
            {
                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    GridViewCommandColumn column = (GridViewCommandColumn)e.CellElement.ColumnInfo;
                    RadButtonElement button = (RadButtonElement)e.CellElement.Children[0];

                    if (column.Name == "DeleteButton")
                    {
                        RadButtonElement element = (RadButtonElement)e.CellElement.Children[0];
                        element.Text = "รายละเอียด";
                        element.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        private void rgv_GetData_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                {
                    if (e.Column.Name == "DeleteButton")
                    {
                        GridViewRowInfo row = (GridViewRowInfo)rgv_GetData.Rows[e.RowIndex];

                        string DocId = row.Cells["DOCID"].Value.ToString();

                        using (Pay_Delete_Detail frm = new Pay_Delete_Detail(DocId))
                        {
                            //frm.Show();
                            if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                            {
                                //โหลดข้อมูลใหม่
                                GetData();
                            }
                        }
                    }
                }
                
            }
            #region OldCode

            //            if (e.RowIndex >= 0)
            //            {
            //                if (e.Column.Name == "DeleteButton")
            //                {
            //                    GridViewRowInfo row = (GridViewRowInfo)rgv_GetData.Rows[e.RowIndex];

            //                    string DocId = row.Cells["DOCID"].Value.ToString();

            //                    if (con.State == ConnectionState.Open) con.Close();
            //                    con.Open();

            //                    SqlTransaction tr = con.BeginTransaction();
            //                    SqlCommand sqlCommand = new SqlCommand();
            //                    sqlCommand.Connection = con;
            //                    sqlCommand.Transaction = tr;

            //                    try
            //                    {

            //                        sqlCommand.CommandText = string.Format(
            //                                @" UPDATE CAMPING_PAYMENTHD SET [DOCSTAT] = '0'
            //								                            ,[MODIFIEDDATE] = @MODIFIEDDATE
            //                                   WHERE [DOCID] = @DOCID ");
            //                        sqlCommand.Parameters.AddWithValue("@DOCID", DocId);
            //                        sqlCommand.Parameters.AddWithValue("@MODIFIEDDATE", MyTime.GetDateTime());

            //                        sqlCommand.ExecuteNonQuery();

            //                        tr.Commit();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        MessageBox.Show(ex.ToString());
            //                    }
            //                    finally
            //                    {
            //                        GetData();

            //                        if (con.State == ConnectionState.Open) con.Close();
            //                    }
            //                }
            //            }
            #endregion
    }
}
