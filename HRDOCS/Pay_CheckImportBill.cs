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
    public partial class Pay_CheckImportBill : Form
    {
        SqlConnection con119srv = new SqlConnection(SysApp.DatabaseConfig.ServerConspc119srv);

        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string _InvioceId = "";
        string _InvioceDate = "";
        string _docid = "";
        string _sumsupc = "";
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

        public Pay_CheckImportBill()
        {
            InitializeComponent();
            InitializeSetGridView();
            this.btn_Search_IP.Click += new EventHandler(btn_Search_Click);
            this.btn_Confirm.Click += new EventHandler(btn_Confirm_Click);
            this.btn_Search_Invoid.Click += new EventHandler(btn_Search_Invoid_Click); 

            this.txt_Date.Text = DateTime.Now.ToString("yyMMdd");
            //this.txt_InvioceId.Enabled = false;
            this.txt_PrintId.Enabled = false;
            // txt_InvioceId.Text ="S1503009-0008231";
            // <WS>
            this.radBtnValidate.Click += new EventHandler(radBtnValidate_Click);
            // </WS>

        }
        
        private void InitializeSetGridView()
        {

            this.rgv_INVOICEID.ReadOnly = true;            
            this.rgv_INVOICEID.AutoGenerateColumns = true;            
            this.rgv_INVOICEID.AllowAddNewRow = false;
            this.rgv_INVOICEID.AllowDeleteRow = false;
            this.rgv_INVOICEID.AllowEditRow = false;

            this.rgv_INVOICEID.ShowGroupPanel = false;
            this.rgv_INVOICEID.EnableFiltering = false;
            this.rgv_INVOICEID.ShowGroupedColumns = true;
            this.rgv_INVOICEID.EnableCustomGrouping = false;
            this.rgv_INVOICEID.EnableCustomFiltering = false;

            this.rgv_INVOICEID.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_INVOICEID.MasterTemplate.AllowCellContextMenu = false;
            this.rgv_INVOICEID.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.rgv_INVOICEID.MasterTemplate.EnableCustomGrouping = false;
            this.rgv_INVOICEID.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_INVOICEID.EnableHotTracking = true;
            this.rgv_INVOICEID.AutoSizeRows = true;

            GridViewSummaryRowItem sumsaleprice = new GridViewSummaryRowItem();
            sumsaleprice.Add(new GridViewSummaryItem("SALESPRICE", "{0:N2}", GridAggregateFunction.Sum));
            this.rgv_INVOICEID.MasterTemplate.SummaryRowsBottom.Add(sumsaleprice);



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
            summary.Add(new GridViewSummaryItem("SUPC", "{0:N2}", GridAggregateFunction.Sum));
            this.rgv_GetData.MasterTemplate.SummaryRowsBottom.Add(summary);
        }

        #region MyButton

        void btn_Search_Invoid_Click(object sender, EventArgs e)
        {
            GetInvoiceId();
        }
        void btn_Search_Click(object sender, EventArgs e)
        {
            //GetInvoiceId();
            Getdata();
        }
        void btn_Confirm_Click(object sender, EventArgs e)
        {
            ConfirmInvoice();
        }
        private void txt_InvioceId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                GetInvoiceId();
            }
        }
        private void txt_PrintId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Getdata();
            }
        }
        // <WS> 24/03/2015
        void radBtnValidate_Click(object sender, EventArgs e)
        {
            decimal sumVal = 0;
            decimal sumVal2 = 0;
            try
            {
                GridViewSummaryRowCollection sumRow = rgv_INVOICEID.MasterView.SummaryRows;
                foreach (var row in sumRow)
                {
                    try
                    {
                        sumVal += Decimal.Parse(row.Cells["SALESPRICE"].Value.ToString());
                    }
                    catch
                    {
                        sumVal += 0;
                    }
                }
                sumRow = null;
                sumRow = rgv_GetData.MasterView.SummaryRows;
                foreach (var row in sumRow)
                {
                    try
                    {
                        sumVal2 += Decimal.Parse(row.Cells["SUPC"].Value.ToString());
                    }
                    catch
                    {
                        sumVal2 += 0;
                    }
                }

                MessageBox.Show(string.Format(@"The val1: {0}, val2: {1}, Compare: {2}", sumVal, sumVal2, (sumVal == sumVal2).ToString()));

            }
            catch (Exception ex)
            {
                MessageBox.Show("WS" + Environment.NewLine + ex.ToString());
            }
        }
        // <WS> 24/03/2015
       
        #endregion
        
        #region fucntion
        
        void GetInvoiceId()
        {
            if (con119srv.State == ConnectionState.Open) con119srv.Close();
            con119srv.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con119srv;

                sqlCommand.CommandText = string.Format(

                   @"SELECT HD.INVOICEID ,DT.ITEMBARCODE , DT.NAME
                   ,CONVERT(numeric(10, 2),DT.SALESPRICE) AS SALESPRICE
                   ,CONVERT (VARCHAR,HD.INVOICEDATE,23) AS INVOICEDATE
                                           FROM [SHOP2013TMP].[dbo].[XXX_POSTABLE] HD
                                            LEFT OUTER JOIN [dbo].[XXX_POSLINE] DT ON HD.[INVOICEID] = DT.[INVOICEID]
                                           WHERE HD.INVOICEID = '{0}'"

                    , txt_InvioceId.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_INVOICEID.DataSource = dt;
                    this.txt_PrintId.Enabled = true;
                    this.txt_PrintId.Focus();
                }
                else
                {
                    MessageBox.Show(" คุณใส่เลขที่บิลขายไม่ถูกต้อง ");
                    rgv_INVOICEID.DataSource = dt;
                    rgv_GetData.DataSource = dt;
                    txt_PrintId.Text = "";
                    this.txt_PrintId.Enabled = false;
                }

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _InvioceId = reader["INVOICEID"].ToString();
                        _InvioceDate = reader["INVOICEDATE"].ToString();
                        break;
                    }
                }
                else
                {
                    this.txt_InvioceId.Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con119srv.State == ConnectionState.Open) con119srv.Close();
            }
        }
        private void Getdata()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                sqlCommand.CommandText = string.Format(
                       @"SELECT DISTINCT( HD.DOCID), HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME
                                ,CONVERT(numeric(10, 2), SUM(DT.SUPC)) AS SUPC ,HD.PRINTID
                            FROM CAMPING_PAYMENTHD HD LEFT OUTER JOIN CAMPING_PAYMENTDT DT ON HD.DOCID = DT.DOCID
                            WHERE HD.PRINTID ='{0}'+'-'+'{1}'
                            AND HD.PRINTSTAT = '1'
                            AND HD.PAYSTAT = '0' 
                            GROUP BY HD.DOCID ,HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME ,HD.PRINTID "
                           , txt_Date.Text.Trim()
                           , txt_PrintId.Text.Trim()
                          );

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_GetData.DataSource = dt;
                    this.txt_PrintId.Enabled = true;
                }
                else
                {
                    rgv_GetData.DataSource = dt;

                    MessageBox.Show(" คุณใส่เลขที่ PI ไม่ถูกต้อง ");
                    rgv_GetData.DataSource = dt;
                    rgv_GetData.DataSource = dt;
                    txt_PrintId.Text = "";
                    txt_PrintId.Focus();
                }

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _docid = reader["DOCID"].ToString();
                        _sumsupc = reader["SUPC"].ToString();
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
        }
        void ConfirmInvoice()
        {
            decimal sumVal = 0;
            decimal sumVal2 = 0;
            try
            {
                GridViewSummaryRowCollection sumRow = rgv_INVOICEID.MasterView.SummaryRows;
                foreach (var row in sumRow)
                {
                    try
                    {
                        sumVal += Decimal.Parse(row.Cells["SALESPRICE"].Value.ToString());
                    }
                    catch
                    {
                        sumVal += 0;
                    }
                }
                sumRow = null;
                sumRow = rgv_GetData.MasterView.SummaryRows;
                foreach (var row in sumRow)
                {
                    try
                    {
                        sumVal2 += Decimal.Parse(row.Cells["SUPC"].Value.ToString());
                    }
                    catch
                    {
                        sumVal2 += 0;
                    }
                }
                MessageBox.Show(string.Format
                    (@"ไม่อนุญาติให้บันทึกข้อมูลเนื่องจากยอดเงินในบิลขายและเลขที่ PIไม่เท่ากัน " + Environment.NewLine + "" + Environment.NewLine +
                         "ยอดเงินในบิลขาย : {0}" + Environment.NewLine + 
                            "ยอดเงินในเลขที่ PI : {1}" + Environment.NewLine + 
                                "" + Environment.NewLine + "Compare: {2}"
                        , sumVal, sumVal2, (sumVal == sumVal2).ToString()), "เกิดข้อผิดพลาด");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("WS" + Environment.NewLine + ex.ToString());
            }

            if (MessageBox.Show("คุณต้องกาบันทึกข้อมูล ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();


            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            SqlTransaction tr = con.BeginTransaction();
            sqlCommand.Transaction = tr;

            try
            {
                sqlCommand.CommandText = string.Format(
//                        @" UPDATE CAMPING_PAYMENTHD SET PAYSTAT  = 1
//                                 ,UPPAYSTATBY = '{0}' 
//                                 ,UPPAYSTATNAME = '{1}'
//								 ,UPPAYSTATDATE = convert(varchar, getdate(), 23)
//                                 ,INVOICEID = @INVOICEID
//                                ,INVOICEDATE = @INVOICEDATE
//                            WHERE DOCID = @DOCID
//                            AND DOCSTAT = 1 "
                        
                        //24/03/58
                        @"UPDATE CAMPING_PAYMENTHD SET PAYSTAT  = 1
                                 ,UPPAYSTATBY = '{0}'  
                                 ,UPPAYSTATNAME = '{1}'
								 ,UPPAYSTATDATE = convert(varchar, getdate(), 23)
                                 ,INVOICEID = @INVOICEID
                                 ,INVOICEDATE = @INVOICEDATE
                            WHERE DOCID =@DOCID
							AND INVOICEID NOT IN 
							(
								SELECT INVOICEID FROM CAMPING_PAYMENTHD
								WHERE INVOICEID = @INVOICEID OR INVOICEID != NULL
							)
							AND PAYSTAT != 1	
                            AND DOCSTAT = 1"
                        , ClassCurUser.LogInEmplId
                        , ClassCurUser.LogInEmplName);




                sqlCommand.Parameters.AddWithValue("@DOCID", _docid);
                sqlCommand.Parameters.AddWithValue("@INVOICEID", _InvioceId);
                sqlCommand.Parameters.AddWithValue("@INVOICEDATE", _InvioceDate);
                sqlCommand.ExecuteNonQuery();

                tr.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
                this.Close();

                this.DialogResult = DialogResult.Yes;
            }
        }

        #endregion
       
    }
}
