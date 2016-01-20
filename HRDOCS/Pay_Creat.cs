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
using System.Drawing.Printing;
namespace HRDOCS
{
    public partial class Pay_Creat : Form
    {
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        #region ตัวแปล
        //string ApproveID;
        string emplid;
        string emplcard;
        string deptid;
        string deptname;
        string secid;
        string sectionname;
        string positionid;
        string positionname;

        string PAYID;
        string PAYNAME;
        string TOTAL;
        string JOBS;
        string BROKERS;
        string CENTER;
        string MIMIGRATION;
        string HOSPITAL;
        string DISTRICT;
        string SUMPAY;
        string SUPC;
        //string tmpGenDocId = ClassDocId.runDocno("CAMPING_PAYMENTHD", "DOCID", "PAY");
        string DocIdPrint;

        #endregion
        //<Neung>
        string[] secureKey = new string[] { };

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
        //
        public Pay_Creat()
        {
            InitializeComponent();

            InitializeGridView();
            this.btn_AddData.Click += new EventHandler(btn_AddData_Click);
            this.btn_Save.Click += new EventHandler(btn_Save_Click);
            this.btn_Newdoc.Click += new EventHandler(btn_Newdoc_Click);
            this.btn_Delete.Click += new EventHandler(btn_Delete_Click);
            

            this.btn_AddData.Enabled = false;
            this.btn_Save.Enabled = false;

            Load_Paymenttype();
        }

        void btn_Delete_Click(object sender, EventArgs e)
        {
            Pay_Delete frm = new Pay_Delete();
            frm.Show();
        }

        #region Function

        void Load_Paymenttype()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = con;
                    sqlCommand.CommandText = string.Format(
                            @" SELECT RTRIM(PAYID) AS PAYID,RTRIM (PAYDESC) AS PAYDESC
                                FROM CAMPING_PAYMENTTYPE
                                ORDER BY PAYID ");

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                dt.Clear();
                            }
                            dt.Load(sqlDataReader);
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        ddl_Paymenttype.DataSource = dt;
                        ddl_Paymenttype.ValueMember = "PAYID";
                        ddl_Paymenttype.DisplayMember = "PAYDESC";
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
        void Getdata()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            if (txtEmpId.Text == "")
            {
                MessageBox.Show("กรุณาใส่รหัสพนักงาน");
                this.txtEmpId.Focus();
            }
            else
            {
                try
                {
                    //if (ApproveID == "008")
                    //{
                    this.txtEmpId.ReadOnly = false;

                    string sqlemp =
                    string.Format(@"
                    SELECT	RTRIM(PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWCARD) AS PWCARD, RTRIM(PWFNAME) AS PWFNAME , RTRIM(PWLNAME) AS PWLNAME ,RTRIM(PWLNAME) AS PWLNAME 
                                            ,(RTRIM(PWEMPLOYEE.PWFNAME) +'  '+ RTRIM(PWEMPLOYEE.PWLNAME)) AS FULLNAME
		                                    ,RTRIM(PWSECTION.PWSECTION)AS SECTIONID  , RTRIM(PWSECTION.PWDESC) AS SECTION
                                            ,RTRIM(PWDEPT.PWDEPT)AS DEPTID , RTRIM(PWDEPT.PWDESC) AS DEPT
                                            ,RTRIM(PWEMPLOYEE.PWPOSITION) AS POSITIONID ,RTRIM(PWPOSITION.PWDESC) AS POSITION
                                     FROM	PWEMPLOYEE WITH (NOLOCK)
	                                    LEFT OUTER JOIN PWSECTION WITH (NOLOCK) ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS
	                                    LEFT OUTER JOIN PWDEPT WITH (NOLOCK) ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT collate Thai_CS_AS
                                        LEFT OUTER JOIN PWPOSITION  WITH (NOLOCK) ON PWEMPLOYEE.PWPOSITION = PWPOSITION.PWPOSITION collate Thai_CS_AS
                                     WHERE	PWSTATWORK LIKE '[AV]' 
                                            AND PWEMPLOYEE.PWGROUP IN ('03') 
                                            AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')"
                                         , txtEmpId.Text.Trim());

                    //AND PWEMPLOYEE.PWSECTION IN 
                    //( SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                    //  WHERE EMPLID = 'M9999999' ) "
                    //, ClassCurUser.LogInEmplId

                    //AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')  

                    SqlCommand cmd = new SqlCommand(sqlemp, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            emplid = reader["PWEMPLOYEE"].ToString();
                            emplcard = reader["PWCARD"].ToString();
                            txtEmplName.Text = reader["FULLNAME"].ToString().Trim();

                            deptid = reader["DEPTID"].ToString().Trim();
                            deptname = reader["DEPT"].ToString().Trim();

                            secid = reader["SECTIONID"].ToString();
                            sectionname = reader["SECTION"].ToString();

                            positionid = reader["POSITIONID"].ToString();
                            positionname = reader["POSITION"].ToString();
                        }
                        this.txtEmpId.ReadOnly = true;
                        this.btn_AddData.Enabled = true;
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
            }

        }
        void InsertData()
        {
            int rowAffectedMs = 0;
            int rowAffectedDt = 0;

            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            SqlTransaction tr = con.BeginTransaction();
            string tmpGenDocId = ClassDocId.runDocno("CAMPING_PAYMENTHD", "DOCID", "PAY");
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            sqlCommand.Transaction = tr;

            if (rgv_Payment.Rows.Count > 0)
            {
                if (MessageBox.Show("คุณต้องการบันทึกเอกสาร ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                    try
                    {
                        #region sql_HD
                        sqlCommand.CommandText = string.Format(
                                                @"INSERT INTO CAMPING_PAYMENTHD (
                                                 DOCID,EMPLID,EMPLCARD,EMPLNAME
                                                ,SECTIONID,SECTIONNAME
                                                ,DEPTID,DEPTNAME
                                                ,POSITIONID,POSITIONNAME,TRANSDATE                                           
                                                ,CREATEDBY,CREATEDNAME,CREATEDDATE
                                                ,MODIFIEDBY,MODIFIEDNAME,MODIFIEDDATE
                                            )                                            
                                        VALUES (
                                                @DOCID_HD,@EMPLID,@EMPLCARD,@EMPLNAME
                                                ,@SECTIONID,@SECTIONNAME
                                                ,@DEPTID,@DEPTNAME
                                                ,@POSITIONID,@POSITIONNAME,convert(varchar, getdate(), 23)                                                
                                                ,@CREATEDBY,@CREATEDNAME,convert(varchar, getdate(), 23)
                                                ,@MODIFIEDBY,@MODIFIEDNAME,convert(varchar, getdate(), 23) )");
                        sqlCommand.Parameters.AddWithValue("@DOCID_HD", tmpGenDocId);
                        sqlCommand.Parameters.AddWithValue("@EMPLID", emplid);
                        sqlCommand.Parameters.AddWithValue("@EMPLCARD", emplcard);
                        sqlCommand.Parameters.AddWithValue("@EMPLNAME", txtEmplName.Text);
                        sqlCommand.Parameters.AddWithValue("@SECTIONID", secid);
                        sqlCommand.Parameters.AddWithValue("@SECTIONNAME", sectionname);
                        sqlCommand.Parameters.AddWithValue("@DEPTID", deptid);
                        sqlCommand.Parameters.AddWithValue("@DEPTNAME", deptname);

                        sqlCommand.Parameters.AddWithValue("@POSITIONID", positionid);
                        sqlCommand.Parameters.AddWithValue("@POSITIONNAME", positionname);

                        sqlCommand.Parameters.AddWithValue("@CREATEDBY", ClassCurUser.LogInEmplId);
                        sqlCommand.Parameters.AddWithValue("@CREATEDNAME", ClassCurUser.LogInEmplName);
                        sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                        sqlCommand.Parameters.AddWithValue("@MODIFIEDNAME", ClassCurUser.LogInEmplName);

                        //sqlCommand.ExecuteNonQuery();
                        rowAffectedMs = sqlCommand.ExecuteNonQuery();
                        #endregion

                        #region sql_DT
                        foreach (GridViewDataRowInfo row in rgv_Payment.Rows)
                        {
                            if (row.Index > -1)
                            {
                                sqlCommand.CommandText = string.Format(
                                                    @"INSERT INTO [dbo].[CAMPING_PAYMENTDT](
                                                             COUNTDOC,DOCID,PAYID,JOBS,BROKERS,CENTER,MIMIGRATION
                                                            ,HOSPITAL,DISTRICT,SUMPAY,SUPC,TOTAL
                                                            )
                                                     VALUES (
                                                             @COUNTDOC{0},@DOCID_DT{0},@PAYID{0}
                                                            ,@JOBS{0},@BROKERS{0},@CENTER{0},@MIMIGRATION{0}
                                                            ,@HOSPITAL{0},@DISTRICT{0},@SUMPAY{0},@SUPC{0},@TOTAL{0}
                                                            )"
                                                    , row.Index);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"COUNTDOC{0}", row.Index), row.Index + 1);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"DOCID_DT{0}", row.Index), tmpGenDocId);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"PAYID{0}", row.Index), row.Cells["PAYID"].Value.ToString());

                                sqlCommand.Parameters.AddWithValue(string.Format(@"JOBS{0}", row.Index), row.Cells["JOBS"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"BROKERS{0}", row.Index), row.Cells["BROKERS"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"CENTER{0}", row.Index), row.Cells["CENTER"].Value.ToString());

                                sqlCommand.Parameters.AddWithValue(string.Format(@"MIMIGRATION{0}", row.Index), row.Cells["MIMIGRATION"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"HOSPITAL{0}", row.Index), row.Cells["HOSPITAL"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"DISTRICT{0}", row.Index), row.Cells["DISTRICT"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"SUMPAY{0}", row.Index), row.Cells["SUMPAY"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"SUPC{0}", row.Index), row.Cells["SUPC"].Value.ToString());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"TOTAL{0}", row.Index), row.Cells["TOTAL"].Value.ToString());

                               // sqlCommand.ExecuteNonQuery();
                                rowAffectedDt += sqlCommand.ExecuteNonQuery();
                            }
                        }
                        #endregion
                        if ((rowAffectedMs > 0) && (rowAffectedDt > 0))
                        {
                            tr.Commit();
                            DocIdPrint = tmpGenDocId;

                            GetPrint();
                            rgv_Payment.Rows.Clear();
                        }
                        else
                        {
                            MessageBox.Show("การบันทึกเอกสารสำเร็จไม่สำเร็จ ลองใหม่อีกครั้ง", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }

                    catch (Exception ex)
                    {
                       // MessageBox.Show(ex.ToString());
                        MessageBox.Show(String.Format(@"เกิดข้อผิดพลาด:{0}{1}", Environment.NewLine, ex.Message), @"การแจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                    }
            }
            else
            {
                MessageBox.Show("ไม่มีข้อมูลสำหรับบันทึก", "แจ้งเตือน");
                this.btn_Save.Enabled = false;

            }
        }
        void GetNewDoc()
        {
            this.rgv_Payment.Rows.Clear();
            this.txtEmpId.Enabled = true;
            this.txtEmplName.Enabled = true;
            this.txtEmpId.Clear();
            this.txtEmplName.Clear();
            this.txtEmpId.ReadOnly = false;
            this.txtEmpId.Focus();

            this.btn_Save.Enabled = false;
            this.btn_AddData.Enabled = false;
        }
        #endregion

        #region  Button
        void btn_AddData_Click(object sender, EventArgs e)
        {
            this.Checkddl_Paymenttype();

            PAYID = ddl_Paymenttype.SelectedValue.ToString();
            PAYNAME = ddl_Paymenttype.SelectedItem.ToString();


            if (emplid == "")
            {
                MessageBox.Show("กรุณาใส่รหัสพนักงาน");
                this.txtEmpId.Focus();
            }
            else 
            {
                if (rgv_Payment.Rows.Count > 0) //ถ้า GriewView เป็นค่าว่าง
                {
                    for                                                                                                                                                                                        (int i = 0; i <= rgv_Payment.Rows.Count - 1; i++ )
                    {
                        if (PAYID == rgv_Payment.Rows[i].Cells["PAYID"].Value.ToString())
                        {
                            MessageBox.Show("คุณได้เลือกรายการ " + " ' "+ PAYNAME +" ' "+" ไปแล้วไม่สามารถเลือกซ้ำได้", "แจ้งเตือน");
                            return;
                        }
                    }
                    this.rgv_Payment.Rows.Add(emplid, PAYID, PAYNAME, JOBS, BROKERS, CENTER, MIMIGRATION, HOSPITAL, DISTRICT, SUMPAY, SUPC, TOTAL);

                    rgv_Payment.Invoke(new EventHandler(delegate
                    {
                        this.btn_Save.Enabled = true;
                        this.txtEmpId.Enabled = false;
                        this.txtEmplName.Enabled = false;
                     }));        
                }
                else
                {
                    this.rgv_Payment.Rows.Add(emplid, PAYID, PAYNAME, JOBS, BROKERS, CENTER, MIMIGRATION, HOSPITAL, DISTRICT, SUMPAY, SUPC, TOTAL);

                    rgv_Payment.Invoke(new EventHandler(delegate
                    {
                        this.btn_Save.Enabled = true;

                        this.txtEmpId.Enabled = false;
                        this.txtEmplName.Enabled = false;
                    }));
                }
            }
        }
        void btn_Save_Click(object sender, EventArgs e)
        {
            InsertData();
        }
        void btn_Newdoc_Click(object sender, EventArgs e)
        {
            GetNewDoc();
        }
        #endregion

        #region EventClick
        private void txtEmpId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Getdata();

            }
        }
        private void rgv_Payment_CellFormatting(object sender, CellFormattingEventArgs e)
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
        private void rgv_Payment_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "BTDELETE")
            {
                if (this.rgv_Payment.SelectedRows.Count > 0)
                {
                    rgv_Payment.Rows.RemoveAt(this.rgv_Payment.SelectedRows[0].Index);
                }
            }
        }
        #endregion

        #region Checkddl_Paymenttype
        void Checkddl_Paymenttype()
        {
            //ขึ้นทะเบียนใหม่
            #region
            if (ddl_Paymenttype.SelectedValue.ToString() == "011")
            {
                JOBS = "900";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "2100";
                DISTRICT = "80";
                SUMPAY = "3080";

                SUPC = "300";
                TOTAL = "3380";
            }
            #endregion

            //ตรวจโรค
            #region
            else if (ddl_Paymenttype.SelectedValue.ToString() == "021")
            {
                JOBS = "";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "2200";
                DISTRICT = "";
                SUMPAY = "2200";

                SUPC = "100";
                TOTAL = "2300";
            }
            else if (ddl_Paymenttype.SelectedValue.ToString() == "022")
            {
                JOBS = "";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "600";
                DISTRICT = "";
                SUMPAY = "600";


                SUPC = "100";
                TOTAL = "700";
            }
            else if (ddl_Paymenttype.SelectedValue.ToString() == "023")
            {
                JOBS = "";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "800";
                DISTRICT = "";
                SUMPAY = "800";


                SUPC = "100";
                TOTAL = "900";
            }
            #endregion

            //workpermit
            #region

            else if (ddl_Paymenttype.SelectedValue.ToString() == "031")
            {
                JOBS = "1300";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "1300";


                SUPC = "1000";
                TOTAL = "2300";
            }
            else if (ddl_Paymenttype.SelectedValue.ToString() == "032")
            {
                JOBS = "1300";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "1300";


                SUPC = "1500";
                TOTAL = "2800";
            }
            else if (ddl_Paymenttype.SelectedValue.ToString() == "033")
            {
                JOBS = "2300";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "2300";


                SUPC = "700";
                TOTAL = "3000";
            }
            else if (ddl_Paymenttype.SelectedValue.ToString() == "034")
            {
                JOBS = "3800";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "3800";


                SUPC = "500";
                TOTAL = "4300";
            }
            else if (ddl_Paymenttype.SelectedValue.ToString() == "035")
            {
                JOBS = "3800";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "3800";


                SUPC = "500";
                TOTAL = "4300";
            }
            #endregion

            //ต่อวีซ่า
            #region
            if (ddl_Paymenttype.SelectedValue.ToString() == "041")
            {
                JOBS = "";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "1500";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "1500";


                SUPC = "500";
                TOTAL = "2000";
            }
            if (ddl_Paymenttype.SelectedValue.ToString() == "042")
            {
                JOBS = "";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "2000";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "2000";


                SUPC = "500";
                TOTAL = "2500";
            }
            if (ddl_Paymenttype.SelectedValue.ToString() == "043")
            {
                JOBS = "";
                BROKERS = "2700";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "2700";


                SUPC = "1000";
                TOTAL = "3700";
            }
            #endregion

            //ต่่อวีซ่าครบ4ปี
            #region
            if (ddl_Paymenttype.SelectedValue.ToString() == "051")
            {
                JOBS = "";
                BROKERS = "";
                CENTER = "4000";
                MIMIGRATION = "";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "4000";


                SUPC = "1000";
                TOTAL = "5000";
            }
            if (ddl_Paymenttype.SelectedValue.ToString() == "052")
            {
                JOBS = "";
                BROKERS = "4900";
                CENTER = "";
                MIMIGRATION = "";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "4900";


                SUPC = "1600";
                TOTAL = "6500";
            }
            if (ddl_Paymenttype.SelectedValue.ToString() == "053")
            {
                JOBS = "";
                BROKERS = "";
                CENTER = "5200";
                MIMIGRATION = "";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "5200";


                SUPC = "1300";
                TOTAL = "6500";
            }
            #endregion

            //ต่ออายุ90วัน & ค่าแจ้งที่พัก
            #region
            if (ddl_Paymenttype.SelectedValue.ToString() == "061")
            {
                JOBS = "";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "150";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "";


                SUPC = "50";
                TOTAL = "200";
            }

            if (ddl_Paymenttype.SelectedValue.ToString() == "071")
            {
                JOBS = "";
                BROKERS = "";
                CENTER = "";
                MIMIGRATION = "150";
                HOSPITAL = "";
                DISTRICT = "";
                SUMPAY = "";


                SUPC = "50";
                TOTAL = "200";
            }
            #endregion
        }
        #endregion
        
        #region InitializeGridView
        private void InitializeGridView()
        {
            rgv_Payment.Dock = DockStyle.Fill;
            rgv_Payment.AutoGenerateColumns = true;
            rgv_Payment.EnableFiltering = false;
            rgv_Payment.AllowAddNewRow = false;
            rgv_Payment.MasterTemplate.AutoGenerateColumns = false;
            rgv_Payment.ShowGroupedColumns = true;
            rgv_Payment.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgv_Payment.EnableHotTracking = true;
            rgv_Payment.AutoSizeRows = true;

            GridViewTextBoxColumn EMPLID = new GridViewTextBoxColumn();
            EMPLID.Name = "EMPLID";
            EMPLID.FieldName = "EMPLID";
            EMPLID.HeaderText = "พนักงาน";
            EMPLID.IsVisible = true;
            EMPLID.ReadOnly = true;
            EMPLID.BestFit();
            rgv_Payment.Columns.Add(EMPLID);

            GridViewTextBoxColumn PAYID = new GridViewTextBoxColumn();
            PAYID.Name = "PAYID";
            PAYID.FieldName = "PAYID";
            PAYID.HeaderText = "ID";
            PAYID.IsVisible = true;
            PAYID.ReadOnly = true;
            PAYID.BestFit();
            rgv_Payment.Columns.Add(PAYID);

            GridViewTextBoxColumn PAYNAME = new GridViewTextBoxColumn();
            PAYNAME.Name = "PAYNAME";
            PAYNAME.FieldName = "PAYNAME";
            PAYNAME.HeaderText = "รายการ";
            PAYNAME.IsVisible = true;
            PAYNAME.ReadOnly = true;
            PAYNAME.BestFit();
            rgv_Payment.Columns.Add(PAYNAME);


            GridViewTextBoxColumn JOBS = new GridViewTextBoxColumn();
            JOBS.Name = "JOBS";
            JOBS.FieldName = "JOBS";
            JOBS.HeaderText = "จัดหางาน";
            JOBS.IsVisible = true;
            JOBS.ReadOnly = true;
            JOBS.BestFit();
            rgv_Payment.Columns.Add(JOBS);

            GridViewTextBoxColumn BROKERS = new GridViewTextBoxColumn();
            BROKERS.Name = "BROKERS";
            BROKERS.FieldName = "BROKERS";
            BROKERS.HeaderText = "โบรกเกอร์";
            BROKERS.IsVisible = true;
            BROKERS.ReadOnly = true;
            BROKERS.BestFit();
            rgv_Payment.Columns.Add(BROKERS);

            GridViewTextBoxColumn CENTER = new GridViewTextBoxColumn();
            CENTER.Name = "CENTER";
            CENTER.FieldName = "CENTER";
            CENTER.HeaderText = "ศูนย์พูนผล";
            CENTER.IsVisible = true;
            CENTER.ReadOnly = true;
            CENTER.BestFit();
            rgv_Payment.Columns.Add(CENTER);


            GridViewTextBoxColumn MIMIGRATION = new GridViewTextBoxColumn();
            MIMIGRATION.Name = "MIMIGRATION";
            MIMIGRATION.FieldName = "MIMIGRATION";
            MIMIGRATION.HeaderText = "ตม.";
            MIMIGRATION.IsVisible = true;
            MIMIGRATION.ReadOnly = true;
            MIMIGRATION.BestFit();
            rgv_Payment.Columns.Add(MIMIGRATION);


            GridViewTextBoxColumn HOSPITAL = new GridViewTextBoxColumn();
            HOSPITAL.Name = "HOSPITAL";
            HOSPITAL.FieldName = "HOSPITAL";
            HOSPITAL.HeaderText = "รพ.";
            HOSPITAL.IsVisible = true;
            HOSPITAL.ReadOnly = true;
            HOSPITAL.BestFit();
            rgv_Payment.Columns.Add(HOSPITAL);

            GridViewTextBoxColumn DISTRICT = new GridViewTextBoxColumn();
            DISTRICT.Name = "DISTRICT";
            DISTRICT.FieldName = "DISTRICT";
            DISTRICT.HeaderText = "อำเภอ.";
            DISTRICT.IsVisible = true;
            DISTRICT.ReadOnly = true;
            DISTRICT.BestFit();
            rgv_Payment.Columns.Add(DISTRICT);

            GridViewTextBoxColumn SUMPAY = new GridViewTextBoxColumn();
            SUMPAY.Name = "SUMPAY";
            SUMPAY.FieldName = "SUMPAY";
            SUMPAY.HeaderText = "รวม";
            SUMPAY.IsVisible = true;
            SUMPAY.ReadOnly = true;
            SUMPAY.BestFit();
            rgv_Payment.Columns.Add(SUMPAY);

            GridViewTextBoxColumn SUPC = new GridViewTextBoxColumn();
            SUPC.Name = "SUPC";
            SUPC.FieldName = "SUPC";
            SUPC.HeaderText = "เข้าบริษัท";
            SUPC.IsVisible = true;
            SUPC.ReadOnly = true;
            SUPC.BestFit();
            rgv_Payment.Columns.Add(SUPC);

            GridViewTextBoxColumn TOTAL = new GridViewTextBoxColumn();
            TOTAL.Name = "TOTAL";
            TOTAL.FieldName = "TOTAL";
            TOTAL.HeaderText = "ยอดเรียกเก็บ";
            TOTAL.IsVisible = true;
            TOTAL.ReadOnly = true;
            TOTAL.BestFit();
            rgv_Payment.Columns.Add(TOTAL);

            GridViewCommandColumn BTDELETE = new GridViewCommandColumn();
            BTDELETE.Name = "BTDELETE";
            BTDELETE.HeaderText = "ลบ";
            BTDELETE.Width = 100;
            rgv_Payment.Columns.Add(BTDELETE);

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
            this.rgv_Payment.MasterTemplate.SummaryRowsBottom.Add(summary);
        }
        #endregion

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
                printDocument1.Print();
                //printDocument1.Print();
            }
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int Y = 0;
            int X = 5;

            /*  e.Graphics.DrawString("พิมพ์ครั้งที่" + LBCprint.Text + "-1", new Font("Tahoma", 8), Brushes.Black, 50, Y);
              Y = Y + 20;//ระยะห่างระหว่างบรรทัด*/
            e.Graphics.DrawString("บันทึกรายการต่อใบอนุญาติทำงานต่างๆ", new Font("Tahoma", 12), Brushes.Black, 5, Y);
            Y = Y + 35;
            e.Graphics.DrawString("ID:" + DocIdPrint, new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 20;

            e.Graphics.DrawString("รหัส:" + emplid, new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 20;
            e.Graphics.DrawString("ชื่อ:" + txtEmplName.Text, new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 20;
            //200
            e.Graphics.DrawString("--------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 20;
            //180
            e.Graphics.DrawString("รายการ", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 20;

            int i = 0;
            foreach (GridViewDataRowInfo row in rgv_Payment.Rows)
            {
                e.Graphics.DrawString(row.Cells["PAYNAME"].Value.ToString() + " " + row.Cells["TOTAL"].Value.ToString() + ".-", new Font("Tahoma", 8), Brushes.Black, 0, Y);
                i = i +  Int32.Parse(row.Cells["TOTAL"].Value.ToString()) ;
                Y = Y + 20;
            }
            e.Graphics.DrawString("ยอดรวม  " + i +".-", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;
            e.Graphics.DrawString("--------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;

            e.Graphics.DrawString("ผู้บันทึก : " + ClassCurUser.LogInEmplName, new Font("Tahoma", 8), Brushes.Black, 0, Y);
            Y = Y + 15;
            e.Graphics.DrawString("พิมพ์ " + System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), new Font("Tahoma", 8), Brushes.Black, 0, Y);
            Y = Y + 15;
            e.Graphics.DrawString("--------------------", new Font("Tahoma", 10), Brushes.Black, 0, Y);
            Y = Y + 15;
            e.Graphics.PageUnit = GraphicsUnit.Inch;
        }
        #endregion

        
    }
}
