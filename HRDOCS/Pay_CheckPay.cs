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
    public partial class Pay_CheckPay : Form
    {
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

       // string ApproveID = "";
        string tmpGenPrintid = ClassRunId.runDocno("CAMPING_PAYMENTHD", "PRINTID", "");

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

        public Pay_CheckPay()
        {
            InitializeComponent();
            InitializeSetGridView();

            this.btn_Search.Click += new EventHandler(btn_Search_Click);
            this.btn_Confirm.Click += new EventHandler(btn_Confirm_Click);

            this.dt_Start.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.dt_End.Text = DateTime.Now.ToString("yyyy-MM-dd");
            
            this.txtPay.Text = "PAY";
            this.txtDate.Text = DateTime.Now.ToString("yyMMdd");
        }

        void btn_Confirm_Click(object sender, EventArgs e)
        {
            ConfremData();
        }

        void btn_Search_Click(object sender, EventArgs e)
        {
            Getdata();
        }
        private void InitializeSetGridView()
        {
            this.rgv_GetData.Dock = DockStyle.Fill;
           // this.rgv_GetData.ReadOnly = true;
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

        private void Getdata()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                if (rdb_Date.ToggleState == ToggleState.On)
                {
                    sqlCommand.CommandText = string.Format(
                        @"  SELECT DISTINCT( HD.DOCID), HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME, SUM (DT.SUPC) as SUPC
                            FROM CAMPING_PAYMENTHD HD LEFT OUTER JOIN CAMPING_PAYMENTDT DT ON HD.DOCID = DT.DOCID
                            WHERE HD.TRANSDATE BETWEEN '{0}' AND '{1}' 
                            AND HD.PRINTSTAT = '0'
                            GROUP BY HD.DOCID ,HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME
                            ORDER BY HD.DOCID "
                            

                        , dt_Start.Value.Date.ToString("yyyy-MM-dd")
                        , dt_End.Value.Date.ToString("yyyy-MM-dd")
                        );
                    //, ClassCurUser.LogInEmplId
                }
                else if (rdb_EmplId.ToggleState == ToggleState.On)
                {
                    sqlCommand.CommandText = string.Format(
                       @"SELECT DISTINCT( HD.DOCID), HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME, SUM (DT.SUPC) as SUPC
                            FROM CAMPING_PAYMENTHD HD LEFT OUTER JOIN CAMPING_PAYMENTDT DT ON HD.DOCID = DT.DOCID
                            WHERE HD.EMPLID = '{0}' OR HD.EMPLCARD = '{0}'
                            AND HD.PRINTSTAT = '0' 
                            GROUP BY HD.DOCID ,HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME "
                          , txtEmplId.Text.Trim());
                }
                else if(rdb_DocId.ToggleState == ToggleState.On)
                {
                    sqlCommand.CommandText = string.Format(
                       @"SELECT DISTINCT( HD.DOCID), HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME, SUM (DT.SUPC) as SUPC
                            FROM CAMPING_PAYMENTHD HD LEFT OUTER JOIN CAMPING_PAYMENTDT DT ON HD.DOCID = DT.DOCID
                            WHERE HD.DOCID = '{0}'  +  '{1}' + '-' + '{2}' 
                            AND HD.PRINTSTAT = '0' 
                            GROUP BY HD.DOCID ,HD.EMPLID, HD.EMPLNAME, HD.SECTIONNAME"
                          ,txtPay.Text.Trim()
                          ,txtDate.Text.Trim()
                          ,txtDocNo.Text.Trim());
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

        private void ConfremData()
        {
            if (MessageBox.Show("คุณต้องกาบันทึกข้อมูล ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                SqlTransaction tr = con.BeginTransaction();
                //string tmpGenPrintid = ClassRunId.runDocno("CAMPING_PAYMENTHD", "PRINTID", "");
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.Transaction = tr;
                // chackbox = true

                int Checktrue = 0;
                for (int i = 0; i <= rgv_GetData.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(rgv_GetData.Rows[i].Cells["CHECK"].Value) == true)
                    {
                        Checktrue++;
                    }
                    
                }
                if (Checktrue == 0)
                {
                    MessageBox.Show("กรุณาเลือกรายชื่อที่ต้องการอนุมัติ");
                    return;
                }
            try 
	        {
                foreach (GridViewDataRowInfo row in rgv_GetData.Rows)
                {
                    if (row.Index > -1)
                    {

                        if (row.Cells["CHECK"].Value != null)
                        {
                            sqlCommand.CommandText = string.Format(
                            @"UPDATE [CAMPING_PAYMENTHD] SET PRINTSTAT = '1'
                                                            ,PRINTID = @PRINTID{0}  
                                                            ,PRINTBY = @PRINTBY{0}
                                                            ,PRINTNAME = @PRINTNAME{0}
                                                            ,PRINDATE = convert(varchar, getdate(), 23)   
                              WHERE DOCID =  @DOCID{0}", row.Index
                              );
                            sqlCommand.Parameters.AddWithValue(string.Format(@"PRINTID{0}", row.Index), tmpGenPrintid);
                            sqlCommand.Parameters.AddWithValue(string.Format(@"PRINTBY{0}", row.Index), ClassCurUser.LogInEmplId);
                            sqlCommand.Parameters.AddWithValue(string.Format(@"PRINTNAME{0}", row.Index), ClassCurUser.LogInEmplName);

                            sqlCommand.Parameters.AddWithValue(string.Format(@"DOCID{0}", row.Index), row.Cells["DOCID"].Value.ToString());


                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                tr.Commit();
                GetPrint();
	        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                tr.Rollback();
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
                Getdata();
            }
        }

        #region print
        private System.Drawing.Printing.PrintDocument docToPrint = new System.Drawing.Printing.PrintDocument();
        private void GetPrint()
        {
            printDialog1.AllowSomePages = true;

            // Show the help button.
            printDialog1.ShowHelp = true;

            // Set the Document property to the PrintDocument for  
            // which the PrintPage Event has been handled. To display the 
            // dialog, either this property or the PrinterSettings property  
            // must be set 
            printDialog1.Document = docToPrint;

            DialogResult result = printDialog1.ShowDialog();

            // If the result is OK then print the document. 
            if (result == DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;

                printDocument1.Print();
                //printDocument1.Print();
            }
        }
       
        #endregion

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int Y = 0;

            int X = 5;

              //e.Graphics.DrawString("พิมพ์ครั้งที่" + LBCprint.Text + "-1", new Font("Tahoma", 8), Brushes.Black, 50, Y);
              //Y = Y + 20;//ระยะห่างระหว่างบรรทัด
            e.Graphics.DrawString("รายได้การต่อใบอนุญาติทำงานต่างๆ", new Font("Tahoma", 12), Brushes.Black, 5, Y);
            Y = Y + 35;
            //e.Graphics.DrawString("ID:" + DocIdPrint, new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 35;

            e.Graphics.DrawString("*1956*", new Font("IDAutomationHC39M", 12), Brushes.Black, 30, Y);
            Y = Y + 50;
            //e.Graphics.DrawString("ชื่อ:" + txtEmplName.Text, new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 20;
            ////200
            //e.Graphics.DrawString("--------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 20;
            ////180
            //e.Graphics.DrawString("รายการ", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 20;

            int i = 0;
            foreach (GridViewDataRowInfo row in rgv_GetData.Rows)
            {
                if (row.Index > -1)
                {
                    if (row.Cells["CHECK"].Value != null)
                    {
                        // e.Graphics.DrawString(row.Cells["DOCID"].Value.ToString() + " " + row.Cells["SUPC"].Value.ToString() + ".-", new Font("Tahoma", 8), Brushes.Black, 0, Y);
                        i = i + Int32.Parse(row.Cells["SUPC"].Value.ToString());
                        Y = Y + 20;
                    }
                }
            }
            e.Graphics.DrawString("ยอดเงินรวม  " + i + ".00  บาท", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;
            e.Graphics.DrawString("----------------------------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;

            e.Graphics.DrawString("ผู้ออกเอกสาร", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 20;

            e.Graphics.DrawString(ClassCurUser.LogInEmplId + " คุณ: " + ClassCurUser.LogInEmplName, new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 20;

            e.Graphics.DrawString("PI:" + tmpGenPrintid, new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 20;
            e.Graphics.DrawString("พิมพ์ " + System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), new Font("Tahoma", 8), Brushes.Black, 0, Y);
            Y = Y + 20;
            e.Graphics.DrawString("----------------------------------------", new Font("Tahoma", 8), Brushes.Black, 0, Y);
            Y = Y + 15;
            e.Graphics.PageUnit = GraphicsUnit.Inch;
        }
    }
}
