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

namespace HROUTOFFICE
{
    public partial class FormEmplFood : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);
        
        int _prindoc ;
        int _princount ;
        string Docno;


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


        public FormEmplFood()
        {
            InitializeComponent();
            InitializeGridView();
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
        }
        private void FormEmplFood_Load(object sender, EventArgs e)
        {
            this.txtDocDate.Text = DateTime.Now.ToString("yyMMdd");
            this.txtOut.Text = "OUT";
            this.txtDocNumber.KeyDown += new KeyEventHandler(txtDocNumber_KeyDown);
        }
        void InitializeGridView()
        {

            this.GridViewShowData.Dock = DockStyle.Fill;
            this.GridViewShowData.ReadOnly = true;
            this.GridViewShowData.AutoGenerateColumns = true;
            this.GridViewShowData.EnableFiltering = false;
            this.GridViewShowData.AllowAddNewRow = false;
            this.GridViewShowData.MasterTemplate.AutoGenerateColumns = false;
            this.GridViewShowData.ShowGroupedColumns = true;
            this.GridViewShowData.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.GridViewShowData.EnableHotTracking = true;
            this.GridViewShowData.AutoSizeRows = true;

            GridViewTextBoxColumn OutOfficeId = new GridViewTextBoxColumn();
            OutOfficeId.Name = "OutOfficeId";
            OutOfficeId.FieldName = "OutOfficeId";
            OutOfficeId.HeaderText = "จำนวน";
            OutOfficeId.Width = 15;
            OutOfficeId.IsVisible = true;
            OutOfficeId.ReadOnly = true;
            OutOfficeId.BestFit();
            GridViewShowData.Columns.Add(OutOfficeId);

            GridViewTextBoxColumn EmplId = new GridViewTextBoxColumn();
            EmplId.Name = "EmplId";
            EmplId.FieldName = "EmplId";
            EmplId.HeaderText = "รหัสพนักงาน";
            EmplId.Width = 30;
            EmplId.IsVisible = true;
            EmplId.ReadOnly = true;
            EmplId.BestFit();
            GridViewShowData.Columns.Add(EmplId);

            GridViewTextBoxColumn EmplFullName = new GridViewTextBoxColumn();
            EmplFullName.Name = "EmplFullName";
            EmplFullName.FieldName = "EmplFullName";
            EmplFullName.HeaderText = "ชื่อ-สกุล";
            EmplFullName.IsVisible = true;
            EmplFullName.ReadOnly = true;
            EmplFullName.BestFit();
            GridViewShowData.Columns.Add(EmplFullName);

            GridViewTextBoxColumn Dimention = new GridViewTextBoxColumn();
            Dimention.Name = "Dimention";
            Dimention.FieldName = "Dimention";
            Dimention.HeaderText = "แผนก";
            Dimention.IsVisible = true;
            Dimention.ReadOnly = true;
            Dimention.BestFit();
            GridViewShowData.Columns.Add(Dimention);
        }

        void txtDocNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetDataEmpl();
            }
        }
        void btnPrint_Click(object sender, EventArgs e)
        {
            //
            GetPrint();
           // GetPrindoc();
        }

        void GetDataEmpl()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
                try
                {
                    if (txtDocDate.Text != "" && txtDocNumber.Text != "")
                    {
                      
                        DataTable dt = new DataTable();
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;

                        sqlCommand.CommandText = string.Format(
                                    @"SELECT DocId,OutOfficeId, [EmplId],[EmplFname]+'  '+[EmplLname] As EmplFullName,[Dimention],convert(varchar,PrintDoc) as PrintDoc
                                      FROM [IVZ_HROUTOFFICE] 
                                      WHERE  HeadApproved = 2 
                                      AND HrApprovedOut = 2 
                                      AND [Status] = 1
                                      AND DocId  = '{0}'  +  '{1}' + '-' + '{2}' "
                                    , txtOut.Text.ToString().Trim()
                                    , txtDocDate.Text.Trim()
                                    , txtDocNumber.Text.Trim());
                        //convert(varchar,getdate(),23)
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            GridViewShowData.DataSource = dt;
                        }

                        else
                        {
                            // MessageBox.Show("ไม่มีข้อมูล กรูณาตรวจสอบเลขที่เอกสารให้ถูกต้อง");
                            this.txtDocNumber.Text = "";
                            this.txtDocNumber.Focus();
                            GridViewShowData.DataSource = dt;
                        }
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                _prindoc = Convert.ToInt32(reader["PrintDoc"]);
                                Docno = reader["DocId"].ToString();
                                break;
                            }
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

        void GetPrindoc()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            SqlTransaction tr = con.BeginTransaction();
            sqlCommand.Transaction = tr;

            _princount = _prindoc + 1;

            try
            {
                sqlCommand.CommandText = string.Format(
                    @"UPDATE IVZ_HROUTOFFICE SET PrintDoc = @PrintDoc	
                                      WHERE DocId  = '{0}'  +  '{1}' + '-' + '{2}'
                                      AND Status = 1 "
                    , txtOut.Text.ToString().Trim()
                    , txtDocDate.Text.Trim()
                    , txtDocNumber.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@PrintDoc", _princount);

                sqlCommand.ExecuteNonQuery();
                tr.Commit();
                this.IsSaveOnce = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
               // this.Close();

            }
        }
        

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
                
                GetPrindoc();
                this.txtDocNumber.Text = "";
                this.txtDocNumber.Focus();
                this.GridViewShowData.DataSource = "";
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int _count = _prindoc + 1;
            int Y = 0;
            int X = 5;

            //e.Graphics.DrawString("พิมพ์ครั้งที่" + LBCprint.Text + "-1", new Font("Tahoma", 8), Brushes.Black, 50, Y);
            //Y = Y + 20;//ระยะห่างระหว่างบรรทัด
            e.Graphics.DrawString("บิลเบิกอาหารพนักงานออกนอกบริษัท", new Font("Tahoma", 12), Brushes.Black, 25, Y);
            Y = Y + 35;
            //e.Graphics.DrawString("ID:" + DocIdPrint, new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 35;

            //bacord
            //e.Graphics.DrawString("*1956*", new Font("IDAutomationHC39M", 12), Brushes.Black, 30, Y);
            //Y = Y + 50;


            //e.Graphics.DrawString("ชื่อ:" + txtEmplName.Text, new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 20;
            ////200
            //e.Graphics.DrawString("--------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 20;
            ////180
            //e.Graphics.DrawString("รายการ", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 20;
            e.Graphics.DrawString("เลขที่เอกสาร : " + Docno, new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 20;
            e.Graphics.DrawString("รายชื่อพนักงานคนไทย", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 20;
            int T = 0;
            int P = 0;
            int TP = 0;
            foreach (GridViewDataRowInfo row in GridViewShowData.Rows)
            {
                if (row.Index > -1)
                {
                    if (row.Cells["EmplId"].Value.ToString().Substring (0,1)!="P")
                    {
                        T = T + 1;
                        e.Graphics.DrawString(T + ". " + row.Cells["EmplFullName"].Value.ToString() + " (" + row.Cells["EmplId"].Value.ToString() + ")" , new Font("Tahoma", 9), Brushes.Black, 0, Y);
                        Y = Y + 20;
                    }
                    else
                    {
                        P = P+1;
                    }
                }
            }
            TP = T + P;
            //e.Graphics.DrawString("รายชื่อพนักงาน  " + i + " ", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 15;
            //e.Graphics.DrawString("----------------------------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 15;

            //foreach (GridViewDataRowInfo row in GridViewShowData.Rows)
            //{
            //    if (row.Index > -1)
            //    {
            //        P = P + 1;
            //        // e.Graphics.DrawString(row.Cells["DOCID"].Value.ToString() + " " + row.Cells["SUPC"].Value.ToString() + ".-", new Font("Tahoma", 8), Brushes.Black, 0, Y);
            //        e.Graphics.DrawString(P + " " + row.Cells["EmplId"].Value.ToString() + " " + row.Cells["EmplFullName"].Value.ToString(), new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //        Y = Y + 20;

            //        //i = i + Int32.Parse(row.Cells["EmplId"].Value.ToString());
            //        //    Y = Y + 20;
            //    }
            //}
            //e.Graphics.DrawString("รายชื่อพนักงาน  " + i + " ", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            //Y = Y + 15;
            e.Graphics.DrawString("-----------------------------------------------------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;

            e.Graphics.DrawString("จำนวนพนักงานพม่า   " + P + "  คน", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;

            e.Graphics.DrawString("-----------------------------------------------------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;

            e.Graphics.DrawString("จำนวนรวม   "+ TP +"  คน", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;

            e.Graphics.DrawString("-----------------------------------------------------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;

            e.Graphics.DrawString("ผู้ออกเอกสาร " + " คุณ: " + ClassCurUser.LogInEmplName, new Font("Tahoma", 8), Brushes.Black, 0, Y);
            Y = Y + 20;

            e.Graphics.DrawString("พิมพ์ " + System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "  ครั้งที่  " + _count, new Font("Tahoma", 8), Brushes.Black, 0, Y);
            Y = Y + 20;
            e.Graphics.DrawString("-----------------------------------------------------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;
            e.Graphics.PageUnit = GraphicsUnit.Inch;
        }
    }
}
