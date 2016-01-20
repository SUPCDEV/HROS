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
    public partial class Chd_ReportHRApprove : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        #region MyRegion SysEmpl

        string sysuser = "";
        string sectionid = "";
        string approveout = "";

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

        protected string syshrapproveout;
        public string HrApproveOut
        {
            get { return syshrapproveout; }
            set { syshrapproveout = value; }
        }

        #endregion
        
        public Chd_ReportHRApprove()
        {
            InitializeComponent();
            this.Btn_Search.Click += new EventHandler(Btn_Search_Click);
            GridViewReport();
        }

        private void Chd_ReportHRApprove_Load(object sender, EventArgs e)
        {
            this.dtpStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dtpEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Load_Section();
        }

        void Btn_Search_Click(object sender, EventArgs e)
        {
            GetData();
        }

        #region Function
        
        void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
                    
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            DataTable dt = new DataTable();

            string TypeDate = "";
            string Section = "";

            if (Rcb_HRApprove.Checked == true)
            {
                TypeDate = "HRAPPROVEDDATE";
            }
            else
            {
                TypeDate = "TRANSDATE";
            }
            if (Rcb_Section.Checked == true)
            {
                Section = " AND HD.SECTIONID = '" + Cbb_Section.SelectedValue.ToString() + "' ";
            }
            else
            {
                Section = "";
            }
            //ทั้งหมด
            if (Rdb_All.ToggleState == ToggleState.On)
            {
                try
                {
                    sqlCommand.CommandText = string.Format(
                        @"  SELECT HD.DOCID ,CONVERT(VARCHAR, HD.TRANSDATE , 23) AS TRANSDATE
                            ,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.FROMHOLIDAY 
                            ,CASE DT.TOHOLIDAY WHEN '0000-00-00' THEN '* แจ้งภายหลัง'  
												  ELSE DT.TOHOLIDAY END AS TOHOLIDAY
                            ,CASE DT.TOSHIFTID WHEN '' THEN '-'  
												  ELSE DT.TOSHIFTID END AS TOSHIFTID
                            ,CASE DT.TOSHIFTDESC WHEN '' THEN '-'  
													ELSE DT.TOSHIFTDESC END AS TOSHIFTDESC ,DT.REASON
                            ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' 
													 WHEN '1' THEN 'อนุมัติ'                                                         
														WHEN '2' THEN 'ไม่อนุมัติ' 
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,HD.HEADAPPROVEDBYNAME AS HEADAPPROVEDBYNAME
                            ,CONVERT (VARCHAR, HD.HEADAPPROVEDDATE , 23) AS HEADAPPROVEDDATE
                            ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' 
													WHEN '1' THEN 'อนุมัติ' 
                                                        WHEN '2' THEN 'ไม่อนุมัติ' 
														    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,HD.HRAPPROVEDBYNAME AS HRAPPROVEDBYNAME
                            ,HD.HEADAPPORVEREMARK
                            ,CONVERT(VARCHAR, HD.HRAPPROVEDDATE ,23) AS HRAPPROVEDDATE
                            ,HD.HRAPPORVEREMARK,CASE HD.DOCSTAT WHEN '1' THEN 'ใช้งาน'  
												WHEN '0' THEN 'ยกเลิก'
												   ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            FROM SPC_JN_CHANGHOLIDAYDT DT LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD 
		                            ON DT.DOCID = HD.DOCID 
							WHERE HD.DOCSTAT != 0
                            AND HD.HEADAPPROVED = '1' 
                            AND CONVERT(VARCHAR,HD.{2},23) BETWEEN '{0}' AND '{1}' {3} 
                            ORDER BY {2}"

                        , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                        , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                        , TypeDate,Section);
                    //AND HD.HRAPPROVEDDATE BETWEEN '{0}' AND '{1}' 

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_StatusDoc.DataSource = dt;
                    }
                    else
                    {
                        rgv_StatusDoc.DataSource = dt;
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
            // อนุมัติ
            else if (Rdb_Approve.ToggleState == ToggleState.On)
            {
                try
                {
                    sqlCommand.CommandText = string.Format(
                           @"  SELECT HD.DOCID ,CONVERT(VARCHAR, HD.TRANSDATE , 23) AS TRANSDATE
                            ,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.FROMHOLIDAY 
                            ,CASE DT.TOHOLIDAY WHEN '0000-00-00' THEN '* แจ้งภายหลัง'  
												  ELSE DT.TOHOLIDAY END AS TOHOLIDAY
                            ,CASE DT.TOSHIFTID WHEN '' THEN '-'  
												  ELSE DT.TOSHIFTID END AS TOSHIFTID
                            ,CASE DT.TOSHIFTDESC WHEN '' THEN '-'  
													ELSE DT.TOSHIFTDESC END AS TOSHIFTDESC ,DT.REASON
                            ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' 
													 WHEN '1' THEN 'อนุมัติ'                                                         
														WHEN '2' THEN 'ไม่อนุมัติ' 
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,HD.HEADAPPROVEDBYNAME AS HEADAPPROVEDBYNAME
                            ,HD.HEADAPPORVEREMARK
                            ,CONVERT (VARCHAR, HD.HEADAPPROVEDDATE , 23) AS HEADAPPROVEDDATE
                            ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' 
													WHEN '1' THEN 'อนุมัติ' 
                                                        WHEN '2' THEN 'ไม่อนุมัติ' 
														    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,HD.HRAPPROVEDBYNAME AS HRAPPROVEDBYNAME
                            ,CONVERT(VARCHAR, HD.HRAPPROVEDDATE ,23) AS HRAPPROVEDDATE
                            ,HD.HRAPPORVEREMARK,CASE HD.DOCSTAT WHEN '1' THEN 'ใช้งาน'  
												WHEN '0' THEN 'ยกเลิก'
												   ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            FROM SPC_JN_CHANGHOLIDAYDT DT LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD 
		                            ON DT.DOCID = HD.DOCID 
							WHERE HD.DOCSTAT != 0 
                            AND HD.HEADAPPROVED = '1'
                            AND HD.HRAPPROVED = '1'
                            AND CONVERT(VARCHAR,HD.{2},23) BETWEEN '{0}' AND '{1}' {3} 
                            ORDER BY {2}"

                           , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                           , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                           , TypeDate,Section);

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_StatusDoc.DataSource = dt;
                    }
                    else
                    {
                        rgv_StatusDoc.DataSource = dt;
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

            else if (Rdb_NoApprove.ToggleState == ToggleState.On)
            {
                try
                {
                    sqlCommand.CommandText = string.Format(
                           @"  SELECT HD.DOCID ,CONVERT(VARCHAR, HD.TRANSDATE , 23) AS TRANSDATE
                            ,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.FROMHOLIDAY 
                            ,CASE DT.TOHOLIDAY WHEN '0000-00-00' THEN '* แจ้งภายหลัง'  
												  ELSE DT.TOHOLIDAY END AS TOHOLIDAY
                            ,CASE DT.TOSHIFTID WHEN '' THEN '-'  
												  ELSE DT.TOSHIFTID END AS TOSHIFTID
                            ,CASE DT.TOSHIFTDESC WHEN '' THEN '-'  
													ELSE DT.TOSHIFTDESC END AS TOSHIFTDESC ,DT.REASON
                            ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' 
													 WHEN '1' THEN 'อนุมัติ'                                                         
														WHEN '2' THEN 'ไม่อนุมัติ' 
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,HD.HEADAPPROVEDBYNAME AS HEADAPPROVEDBYNAME
                            ,HD.HEADAPPORVEREMARK
                            ,CONVERT (VARCHAR, HD.HEADAPPROVEDDATE , 23) AS HEADAPPROVEDDATE
                            ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' 
													WHEN '1' THEN 'อนุมัติ' 
                                                        WHEN '2' THEN 'ไม่อนุมัติ' 
														    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,HD.HRAPPROVEDBYNAME AS HRAPPROVEDBYNAME
                            ,CONVERT(VARCHAR, HD.HRAPPROVEDDATE ,23) AS HRAPPROVEDDATE
                            ,HD.HRAPPORVEREMARK,CASE HD.DOCSTAT WHEN '1' THEN 'ใช้งาน'  
												WHEN '0' THEN 'ยกเลิก'
												   ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            FROM SPC_JN_CHANGHOLIDAYDT DT LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD 
		                            ON DT.DOCID = HD.DOCID  
							WHERE HD.DOCSTAT != 0 
                            AND HD.HEADAPPROVED = '1'
                            AND HD.HRAPPROVED = '2'
                            AND CONVERT(VARCHAR,HD.{2},23) BETWEEN '{0}' AND '{1}' {3} 
                            ORDER BY {2} "

                           , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                           , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                           , TypeDate, Section);

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_StatusDoc.DataSource = dt;
                    }
                    else
                    {
                        rgv_StatusDoc.DataSource = dt;
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
            else if (Rdb_WaitApprove.ToggleState == ToggleState.On)
            {
                try
                {
                    sqlCommand.CommandText = string.Format(
                           @" SELECT HD.DOCID ,CONVERT(VARCHAR, HD.TRANSDATE , 23) AS TRANSDATE
                            ,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.FROMHOLIDAY 
                            ,CASE DT.TOHOLIDAY WHEN '0000-00-00' THEN '* แจ้งภายหลัง'  
												  ELSE DT.TOHOLIDAY END AS TOHOLIDAY
                            ,CASE DT.TOSHIFTID WHEN '' THEN '-'  
												  ELSE DT.TOSHIFTID END AS TOSHIFTID
                            ,CASE DT.TOSHIFTDESC WHEN '' THEN '-'  
													ELSE DT.TOSHIFTDESC END AS TOSHIFTDESC ,DT.REASON
                            ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' 
													 WHEN '1' THEN 'อนุมัติ'                                                         
														WHEN '2' THEN 'ไม่อนุมัติ' 
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,HD.HEADAPPROVEDBYNAME AS HEADAPPROVEDBYNAME
                            ,CONVERT (VARCHAR, HD.HEADAPPROVEDDATE , 23) AS HEADAPPROVEDDATE
                            ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' 
													WHEN '1' THEN 'อนุมัติ' 
                                                        WHEN '2' THEN 'ไม่อนุมัติ' 
														    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,HD.HRAPPROVEDBYNAME AS HRAPPROVEDBYNAME
                            ,HD.HEADAPPORVEREMARK
                            ,CONVERT(VARCHAR, HD.HRAPPROVEDDATE ,23) AS HRAPPROVEDDATE
                            ,HD.HRAPPORVEREMARK,CASE HD.DOCSTAT WHEN '1' THEN 'ใช้งาน'  
												WHEN '0' THEN 'ยกเลิก'
												   ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            FROM SPC_JN_CHANGHOLIDAYDT DT LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD 
		                            ON DT.DOCID = HD.DOCID 
							WHERE HD.DOCSTAT != 0 
                            AND HD.HEADAPPROVED = '1'                              
                            AND HD.HRAPPROVED = '0' 
                            AND CONVERT(VARCHAR,HD.{2},23) BETWEEN '{0}' AND '{1}' {3} 
                            ORDER BY {2} "

                           , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                           , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                           , TypeDate, Section);

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_StatusDoc.DataSource = dt;
                    }
                    else
                    {
                        rgv_StatusDoc.DataSource = dt;
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
            else
            {
                return;
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
                        MessageBox.Show("ไม่พบสิทธิ์ในการอนุมัติเอกสาร...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

        void GridViewReport()
        {

            this.rgv_StatusDoc.Dock = DockStyle.Fill;
            this.rgv_StatusDoc.ReadOnly = true;
            this.rgv_StatusDoc.AutoGenerateColumns = true;
            this.rgv_StatusDoc.EnableFiltering = false;
            this.rgv_StatusDoc.AllowAddNewRow = false;
            this.rgv_StatusDoc.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_StatusDoc.ShowGroupedColumns = true;
            this.rgv_StatusDoc.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_StatusDoc.EnableHotTracking = true;
            this.rgv_StatusDoc.AutoSizeRows = true;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_StatusDoc.Columns.Add(DOCID);

            GridViewTextBoxColumn EMPLID = new GridViewTextBoxColumn();
            EMPLID.Name = "EMPLID";
            EMPLID.FieldName = "EMPLID";
            EMPLID.HeaderText = "รหัส";
            EMPLID.IsVisible = true;
            EMPLID.ReadOnly = true;
            EMPLID.BestFit();
            rgv_StatusDoc.Columns.Add(EMPLID);

            GridViewTextBoxColumn EMPLNAME = new GridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.FieldName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.IsVisible = true;
            EMPLNAME.ReadOnly = true;
            EMPLNAME.BestFit();
            rgv_StatusDoc.Columns.Add(EMPLNAME);

            GridViewTextBoxColumn SECTIONNAME = new GridViewTextBoxColumn();
            SECTIONNAME.Name = "SECTIONNAME";
            SECTIONNAME.FieldName = "SECTIONNAME";
            SECTIONNAME.HeaderText = "แผนก";
            SECTIONNAME.IsVisible = true;
            SECTIONNAME.ReadOnly = true;
            SECTIONNAME.BestFit();
            rgv_StatusDoc.Columns.Add(SECTIONNAME);

            GridViewTextBoxColumn DEPTNAME = new GridViewTextBoxColumn();
            DEPTNAME.Name = "DEPTNAME";
            DEPTNAME.FieldName = "DEPTNAME";
            DEPTNAME.HeaderText = "ตำแหน่ง";
            DEPTNAME.IsVisible = true;
            DEPTNAME.ReadOnly = true;
            DEPTNAME.BestFit();
            rgv_StatusDoc.Columns.Add(DEPTNAME);



            GridViewTextBoxColumn FROMHOLIDAY = new GridViewTextBoxColumn();
            FROMHOLIDAY.Name = "FROMHOLIDAY";
            FROMHOLIDAY.FieldName = "FROMHOLIDAY";
            FROMHOLIDAY.HeaderText = "วันที่มาทำ";
            FROMHOLIDAY.IsVisible = true;
            FROMHOLIDAY.ReadOnly = true;
            FROMHOLIDAY.BestFit();
            rgv_StatusDoc.Columns.Add(FROMHOLIDAY);

            GridViewTextBoxColumn TOSHIFTID = new GridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.FieldName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "กะที่มาทำ";
            TOSHIFTID.IsVisible = true;
            TOSHIFTID.ReadOnly = true;
            TOSHIFTID.BestFit();
            rgv_StatusDoc.Columns.Add(TOSHIFTID);


            GridViewTextBoxColumn TOSHIFTDESC = new GridViewTextBoxColumn();
            TOSHIFTDESC.Name = "TOSHIFTDESC";
            TOSHIFTDESC.FieldName = "TOSHIFTDESC";
            TOSHIFTDESC.HeaderText = "คำอธิบาย";
            TOSHIFTDESC.IsVisible = true;
            TOSHIFTDESC.ReadOnly = true;
            TOSHIFTDESC.BestFit();
            rgv_StatusDoc.Columns.Add(TOSHIFTDESC);

            GridViewTextBoxColumn TOHOLIDAY = new GridViewTextBoxColumn();
            TOHOLIDAY.Name = "TOHOLIDAY";
            TOHOLIDAY.FieldName = "TOHOLIDAY";
            TOHOLIDAY.HeaderText = "วันที่หยุด";
            TOHOLIDAY.IsVisible = true;
            TOHOLIDAY.ReadOnly = true;
            TOHOLIDAY.BestFit();
            rgv_StatusDoc.Columns.Add(TOHOLIDAY);

            GridViewTextBoxColumn REASON = new GridViewTextBoxColumn();
            REASON.Name = "REASON";
            REASON.FieldName = "REASON";
            REASON.HeaderText = "เหตุผล";
            REASON.IsVisible = true;
            REASON.ReadOnly = true;
            REASON.BestFit();
            rgv_StatusDoc.Columns.Add(REASON);

            GridViewTextBoxColumn HEADAPPROVED = new GridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.FieldName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.IsVisible = true;
            HEADAPPROVED.ReadOnly = true;
            HEADAPPROVED.BestFit();
            rgv_StatusDoc.Columns.Add(HEADAPPROVED);

            GridViewTextBoxColumn HEADAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.FieldName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.IsVisible = true;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            HEADAPPROVEDBYNAME.BestFit();
            rgv_StatusDoc.Columns.Add(HEADAPPROVEDBYNAME);

            GridViewTextBoxColumn HEADAPPORVEREMARK = new GridViewTextBoxColumn();
            HEADAPPORVEREMARK.Name = "HEADAPPORVEREMARK";
            HEADAPPORVEREMARK.FieldName = "HEADAPPORVEREMARK";
            HEADAPPORVEREMARK.HeaderText = "หมายเหตุ";
            HEADAPPORVEREMARK.IsVisible = true;
            HEADAPPORVEREMARK.ReadOnly = true;
            HEADAPPORVEREMARK.BestFit();
            rgv_StatusDoc.Columns.Add(HEADAPPORVEREMARK);
            

            GridViewTextBoxColumn HEADAPPROVEDDATE = new GridViewTextBoxColumn();
            HEADAPPROVEDDATE.Name = "HEADAPPROVEDDATE";
            HEADAPPROVEDDATE.FieldName = "HEADAPPROVEDDATE";
            HEADAPPROVEDDATE.HeaderText = "วันที่อนุมัติ";
            HEADAPPROVEDDATE.IsVisible = true;
            HEADAPPROVEDDATE.ReadOnly = true;
            HEADAPPROVEDDATE.BestFit();
            rgv_StatusDoc.Columns.Add(HEADAPPROVEDDATE);


            GridViewTextBoxColumn HRAPPROVED = new GridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.FieldName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.IsVisible = true;
            HRAPPROVED.ReadOnly = true;
            HRAPPROVED.BestFit();
            rgv_StatusDoc.Columns.Add(HRAPPROVED);


            GridViewTextBoxColumn HRAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.FieldName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.IsVisible = true;
            HRAPPROVEDBYNAME.ReadOnly = true;
            HRAPPROVEDBYNAME.BestFit();
            rgv_StatusDoc.Columns.Add(HRAPPROVEDBYNAME);

            GridViewTextBoxColumn HRAPPORVEREMARK = new GridViewTextBoxColumn();
            HRAPPORVEREMARK.Name = "HRAPPORVEREMARK";
            HRAPPORVEREMARK.FieldName = "HRAPPORVEREMARK";
            HRAPPORVEREMARK.HeaderText = "หมายเหตุ";
            HRAPPORVEREMARK.IsVisible = true;
            HRAPPORVEREMARK.ReadOnly = true;
            HRAPPORVEREMARK.BestFit();
            rgv_StatusDoc.Columns.Add(HRAPPORVEREMARK);

            GridViewTextBoxColumn HRAPPROVEDDATE = new GridViewTextBoxColumn();
            HRAPPROVEDDATE.Name = "HRAPPROVEDDATE";
            HRAPPROVEDDATE.FieldName = "HRAPPROVEDDATE";
            HRAPPROVEDDATE.HeaderText = "วันที่อนุมัติ";
            HRAPPROVEDDATE.IsVisible = true;
            HRAPPROVEDDATE.ReadOnly = true;
            HRAPPROVEDDATE.BestFit();
            rgv_StatusDoc.Columns.Add(HRAPPROVEDDATE);
            
        }

        #region Print
        void exporter_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {

            string headerText = "รายงานเปลี่ยนวันหยุดออนไลน์ตั้งแต่วันที่:  " + dtpStart.Text + " ถึง " + dtpEnd.Text;
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
        private void Btn_ExportToExcel_Click(object sender, EventArgs e)
        {
            if (rgv_StatusDoc.RowCount != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                ExportToExcelML excelExporter = null;

                try
                {
                    ExportToExcelML excelML = new ExportToExcelML(this.rgv_StatusDoc);
                    excelExporter = new ExportToExcelML(this.rgv_StatusDoc);
                    excelExporter.ExcelCellFormatting += excelExporter_ExcelCellFormatting;
                    excelExporter.ExcelTableCreated += exporter_ExcelTableCreated;

                    this.Cursor = Cursors.WaitCursor;
                    saveFileDialog.Filter = "Excel (*.xls)|*.xls";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        excelExporter.RunExport(saveFileDialog.FileName);

                        DialogResult dr = RadMessageBox.Show("การบันทึกไฟล์สำเร็จ คุณต้องการเปิดไฟล์หรือไม่?",
                            "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
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
                RadMessageBox.Show("ไม่มีข้อมูล");
                return;
            }
        }
        #endregion

    }
}
