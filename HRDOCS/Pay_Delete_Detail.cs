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
    public partial class Pay_Delete_Detail : Form
    {
        protected bool IsSaveOnce = true;

        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);
        string _docid;
        public Pay_Delete_Detail( string DocId)
        {
            _docid = DocId;
            InitializeComponent();
            this.btn_Delete.Click += new EventHandler(btn_Delete_Click);
            InitializeGridView();
            GetData();
        }

        void btn_Delete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
        void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            DataTable dt = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;

            try
            {
                sqlCommand.CommandText = string.Format(
                    @"SELECT HD.DOCID, HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME,CONVERT(VARCHAR, HD.TRANSDATE, 23) AS TRANSDATE,DT.PAYID,TYP.PAYDESC
                            , DT.JOBS, DT.BROKERS, DT.CENTER, DT.MIMIGRATION, DT.HOSPITAL, DT.DISTRICT, DT.SUMPAY, DT.SUPC,DT.TOTAL
                            FROM CAMPING_PAYMENTHD HD 
	                             LEFT OUTER JOIN CAMPING_PAYMENTDT DT ON HD.DOCID = DT.DOCID
	                             LEFT OUTER JOIN CAMPING_PAYMENTTYPE TYP ON  TYP.PAYID = DT.PAYID 
                            WHERE HD.DOCID = '{0}'
                            AND HD.PRINTSTAT = '0'
                            AND HD.DOCSTAT = '1' ", _docid.Trim().ToString());

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lbl_DOCID.Text = reader["DOCID"].ToString();
                    }      
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

            try
            {
                sqlCommand.CommandText = string.Format(
                    @"SELECT HD.DOCID, HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME,CONVERT(VARCHAR, HD.TRANSDATE, 23) AS TRANSDATE,DT.PAYID,TYP.PAYDESC
                            , DT.JOBS, DT.BROKERS, DT.CENTER, DT.MIMIGRATION, DT.HOSPITAL, DT.DISTRICT, DT.SUMPAY, DT.SUPC,DT.TOTAL
                            FROM CAMPING_PAYMENTHD HD 
	                             LEFT OUTER JOIN CAMPING_PAYMENTDT DT ON HD.DOCID = DT.DOCID
	                             LEFT OUTER JOIN CAMPING_PAYMENTTYPE TYP ON  TYP.PAYID = DT.PAYID 
                            WHERE HD.DOCID = '{0}'
                            AND HD.PRINTSTAT = '0'
                            AND HD.DOCSTAT = '1' ", _docid.Trim().ToString());

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_EmplData.DataSource = dt;
                }
                else
                {
                    rgv_EmplData.DataSource = dt;
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
        private void DeleteData()
        {
            if (MessageBox.Show("คุณต้องการบันทึกเอกสาร ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            #region Update CN_Doc
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

                SqlTransaction tr = con.BeginTransaction();
                sqlCommand.Transaction = tr;

                sqlCommand.CommandText = string.Format(
                                            @" UPDATE CAMPING_PAYMENTHD SET [DOCSTAT] = '0'
                                                        ,MODIFIEDBY = @MODIFIEDBY
                                                        ,MODIFIEDNAME = @MODIFIEDNAME
            								          ,[MODIFIEDDATE] = @MODIFIEDDATE
                                               WHERE [DOCID] = @DOCID ");
                sqlCommand.Parameters.AddWithValue("@DOCID", _docid);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDNAME", ClassCurUser.LogInEmplName);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDDATE", MyTime.GetDateTime());

                sqlCommand.ExecuteNonQuery();

                tr.Commit();
                this.IsSaveOnce = true;
                // MessageBox.Show("อนุมัติเอกสารเลขที่เอกสาร : " + txtDocId.Text + " สำเร็จ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
                this.Close();

                this.DialogResult = DialogResult.Yes;
            }
            #endregion

        }
        void InitializeGridView()
        {

            this.rgv_EmplData.Dock = DockStyle.Fill;
            this.rgv_EmplData.ReadOnly = true;
            this.rgv_EmplData.AutoGenerateColumns = true;
            this.rgv_EmplData.EnableFiltering = false;
            this.rgv_EmplData.AllowAddNewRow = false;
            this.rgv_EmplData.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_EmplData.ShowGroupedColumns = true;
            this.rgv_EmplData.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_EmplData.EnableHotTracking = true;
            this.rgv_EmplData.AutoSizeRows = true;


            GridViewTextBoxColumn PAYDESC = new GridViewTextBoxColumn();
            PAYDESC.Name = "PAYDESC";
            PAYDESC.FieldName = "PAYDESC";
            PAYDESC.HeaderText = "รายการ";
            PAYDESC.IsVisible = true;
            PAYDESC.ReadOnly = true;
            PAYDESC.BestFit();
            rgv_EmplData.Columns.Add(PAYDESC);

            GridViewTextBoxColumn JOBS = new GridViewTextBoxColumn();
            JOBS.Name = "JOBS";
            JOBS.FieldName = "JOBS";
            JOBS.HeaderText = "จัดหางาน";
            JOBS.IsVisible = true;
            JOBS.ReadOnly = true;
            JOBS.BestFit();
            rgv_EmplData.Columns.Add(JOBS);

            GridViewTextBoxColumn BROKERS = new GridViewTextBoxColumn();
            BROKERS.Name = "BROKERS";
            BROKERS.FieldName = "BROKERS";
            BROKERS.HeaderText = "โบรกเกอร์";
            BROKERS.IsVisible = true;
            BROKERS.ReadOnly = true;
            BROKERS.BestFit();
            rgv_EmplData.Columns.Add(BROKERS);

            GridViewTextBoxColumn CENTER = new GridViewTextBoxColumn();
            CENTER.Name = "CENTER";
            CENTER.FieldName = "CENTER";
            CENTER.HeaderText = "ศูนย์พูลผล";
            CENTER.IsVisible = true;
            CENTER.ReadOnly = true;
            CENTER.BestFit();
            rgv_EmplData.Columns.Add(CENTER);

            GridViewTextBoxColumn MIMIGRATION = new GridViewTextBoxColumn();
            MIMIGRATION.Name = "MIMIGRATION";
            MIMIGRATION.FieldName = "MIMIGRATION";
            BROKERS.HeaderText = "ตม.";
            MIMIGRATION.IsVisible = true;
            MIMIGRATION.ReadOnly = true;
            MIMIGRATION.BestFit();
            rgv_EmplData.Columns.Add(MIMIGRATION);

            GridViewTextBoxColumn HOSPITAL = new GridViewTextBoxColumn();
            HOSPITAL.Name = "HOSPITAL";
            HOSPITAL.FieldName = "HOSPITAL";
            HOSPITAL.HeaderText = "รพ.";
            HOSPITAL.IsVisible = true;
            HOSPITAL.ReadOnly = true;
            HOSPITAL.BestFit();
            rgv_EmplData.Columns.Add(HOSPITAL);

            GridViewTextBoxColumn DISTRICT = new GridViewTextBoxColumn();
            DISTRICT.Name = "DISTRICT";
            DISTRICT.FieldName = "DISTRICT";
            DISTRICT.HeaderText = "อำเภอ";
            DISTRICT.IsVisible = true;
            DISTRICT.ReadOnly = true;
            DISTRICT.BestFit();
            rgv_EmplData.Columns.Add(DISTRICT);

            GridViewTextBoxColumn SUMPAY = new GridViewTextBoxColumn();
            SUMPAY.Name = "SUMPAY";
            SUMPAY.FieldName = "SUMPAY";
            SUMPAY.HeaderText = "รวม";
            SUMPAY.IsVisible = true;
            SUMPAY.ReadOnly = true;
            SUMPAY.BestFit();
            rgv_EmplData.Columns.Add(SUMPAY);

            GridViewTextBoxColumn SUPC = new GridViewTextBoxColumn();
            SUPC.Name = "SUPC";
            SUPC.FieldName = "SUPC";
            SUPC.HeaderText = "SUPC";
            SUPC.IsVisible = true;
            SUPC.ReadOnly = true;
            SUPC.BestFit();
            rgv_EmplData.Columns.Add(SUPC);

            GridViewTextBoxColumn TOTAL = new GridViewTextBoxColumn();
            TOTAL.Name = "TOTAL";
            TOTAL.FieldName = "TOTAL";
            TOTAL.HeaderText = "ยอดเรียกเก็บ";
            TOTAL.IsVisible = true;
            TOTAL.ReadOnly = true;
            TOTAL.BestFit();
            rgv_EmplData.Columns.Add(TOTAL);

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

            this.rgv_EmplData.MasterTemplate.SummaryRowsBottom.Add(summary);
        }

    }
}
