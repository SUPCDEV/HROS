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
    public partial class Cancle_ApproveHD : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string ApproveID = "";
        
        #region MyRegion SysEmpl

        string sysuser = "";


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
        protected string sysoutoffice;
        public string SysOutoffice
        {
            get { return sysoutoffice; }
            set { sysoutoffice = value; }
        }

        #endregion

        public Cancle_ApproveHD()
        {
            InitializeComponent();
            InitializeSetGridView();

            this.btn_Search.Click += new EventHandler(btn_Search_Click);
        }

        private void Cancle_ApproveHD_Load(object sender, EventArgs e)
        {
            this.dt_FromSec.Text = DateTime.Now.AddDays(-3).Date.ToString("yyyy-MM-dd");
            this.dt_FromSec.MinDate = DateTime.Now.Date.AddDays(-3);
            this.dt_ToSec.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.txt_DocId.Text = "CN";
            this.txt_DocDate.Text = DateTime.Now.ToString("yyMMdd");

            this.CheckApproveID();
            this.Load_Section();
            this.ShowdataAll();
        }

        #region Button

        void btn_Search_Click(object sender, EventArgs e)
        {
            GetdataCN();
        }

        #endregion

        #region Function

        void CheckApproveID()
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
                    sqlCommand.CommandText = string.Format(@" select * from SPC_CM_AUTHORIZE
                                                                where EMPLID = '{0}'
                                                                order by APPROVEID ", ClassCurUser.LogInEmplId);
                    //, ClassCurUser.LogInEmplId);

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
                    ApproveID = dataTable.Rows[0]["APPROVEID"].ToString();
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

        void ShowdataAll()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            if (ApproveID == "001" || ApproveID == "002")
            {
                #region SelectAll

                try
                {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    if (rdb_SelectAll.ToggleState == ToggleState.On)
                    {
                            sqlCommand.CommandText = string.Format(
                                @" SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE 
                                ,HD.CREATEDNAME ,CONVERT(VARCHAR,HD.MODIFIEDDATE,23) AS MODIFIEDDATE
                                ,CONVERT(VARCHAR, HD.EXPIREDATE,23) AS EXPIREDATE 
                                ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC
                                ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
                                ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                    LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DL ON DT.DOCREFER = DL.DLDOCNO
									LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHG ON DT.DOCREFER = CHG.DOCID
									LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DS ON DT.DOCREFER = DS.DSDOCNO
                                WHERE HD.HEADAPPROVED = '0'                                
                                AND HD.DOCSTAT = '1'
                                AND HD.SECTIONID IN (	SELECT PWSECTION FROM SPC_CM_AUTHORIZE
						                                WHERE EMPLID = '{0}' 
						                                AND APPROVEID = '001'
					                                ) 
                                GROUP BY HD.DOCID,HD.CREATEDDATE,HD.CREATEDNAME ,HD.MODIFIEDDATE,HD.EXPIREDATE,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC
								,HD.HEADAPPROVED,HD.HEADAPPROVEDBYNAME, HD.HRAPPROVED,HD.HRAPPROVEDBYNAME 
                                ORDER BY HD.DOCID DESC ", ClassCurUser.LogInEmplId);
                            //AND DATEDIFF(DAY,ISNULL(HD.EXPIREDATE,GETDATE()),GETDATE())<=3
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            rgv_ApproveHD.DataSource = dt;
                        }
                        else
                        {
                            rgv_ApproveHD.DataSource = dt;
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

                #endregion
            }
        }
        void Load_Section()
        {

            //ddl_Section.Items.Clear();

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
												                                                            WHERE EMPLID = '{0}' AND APPROVEID = '001'
                                                            )
                                                            ORDER BY PWDESC "
                        , ClassCurUser.LogInEmplId
                                                            );

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
                        ddl_Section.DataSource = dataTable;
                        ddl_Section.ValueMember = "PWSECTION";
                        ddl_Section.DisplayMember = "PWDESC";

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

        void GetdataCN()
        {
           // this.GetDocTyp();

            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            DataTable dt = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            #region rdb_Section
            try
            {
                if (rdb_Section.ToggleState == ToggleState.On)
                {

                    sqlCommand.CommandText = string.Format(
                        @" SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE 
                                ,HD.CREATEDNAME ,CONVERT(VARCHAR,HD.MODIFIEDDATE,23) AS MODIFIEDDATE
                                ,CONVERT(VARCHAR, HD.EXPIREDATE,23) AS EXPIREDATE 
                                ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC
                                ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
                                ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                    LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DL ON DT.DOCREFER = DL.DLDOCNO
									LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHG ON DT.DOCREFER = CHG.DOCID
									LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DS ON DT.DOCREFER = DS.DSDOCNO
                                WHERE HD.SECTIONID = '{0}'
                                AND HD.CREATEDDATE BETWEEN '{1}' AND '{2}'
                                AND HD.HEADAPPROVED = '0'
                                AND HD.DOCSTAT = '1'
                                AND HD.SECTIONID IN (	SELECT PWSECTION FROM SPC_CM_AUTHORIZE
						                                WHERE EMPLID = '{3}' 
						                                AND APPROVEID = '001'
					                                ) 
                                GROUP BY HD.DOCID,HD.CREATEDDATE,HD.CREATEDNAME ,HD.MODIFIEDDATE,HD.EXPIREDATE,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC
								,HD.HEADAPPROVED,HD.HEADAPPROVEDBYNAME, HD.HRAPPROVED,HD.HRAPPROVEDBYNAME 
                                ORDER BY HD.DOCID DESC "
                            , ddl_Section.SelectedValue.ToString()
                            , dt_FromSec.Value.Date.ToString("yyyy-MM-dd")
                            , dt_ToSec.Value.Date.ToString("yyyy-MM-dd")
                            , ClassCurUser.LogInEmplId);

                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_ApproveHD.DataSource = dt;
                    }
                    else
                    {
                        rgv_ApproveHD.DataSource = dt;
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
            #endregion

            #region rdb_EmpID
            try
            {
                if (rdb_EmpId.ToggleState == ToggleState.On)
                {
                    sqlCommand.CommandText = string.Format(
                        @" SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE 
                                ,HD.CREATEDNAME ,CONVERT(VARCHAR,HD.MODIFIEDDATE,23) AS MODIFIEDDATE
                                ,CONVERT(VARCHAR, HD.EXPIREDATE,23) AS EXPIREDATE 
                                ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC
                                ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
                                ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                    LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DL ON DT.DOCREFER = DL.DLDOCNO
									LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHG ON DT.DOCREFER = CHG.DOCID
									LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DS ON DT.DOCREFER = DS.DSDOCNO
                                WHERE HD.EMPLID = '{0}'
                                AND HD.HEADAPPROVED = '0'
                                AND HD.DOCSTAT = '1' 
                                AND HD.SECTIONID IN (	SELECT PWSECTION FROM SPC_CM_AUTHORIZE
						                                WHERE EMPLID = '{1}' 
						                                AND APPROVEID = '001'
					                                ) 
                                GROUP BY HD.DOCID,HD.CREATEDDATE,HD.CREATEDNAME ,HD.MODIFIEDDATE,HD.EXPIREDATE,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC
								,HD.HEADAPPROVED,HD.HEADAPPROVEDBYNAME, HD.HRAPPROVED,HD.HRAPPROVEDBYNAME 
                                ORDER BY HD.DOCID DESC "
                            , txt_EmpId.Text.ToString()
                            , ClassCurUser.LogInEmplId);


                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_ApproveHD.DataSource = dt;
                    }
                    else
                    {
                        rgv_ApproveHD.DataSource = dt;
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
            #endregion

            #region rdb_DocId

            try
            {
                if (rdb_DocId.ToggleState == ToggleState.On)
                {
                    sqlCommand.CommandText = string.Format(
                    @" SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE 
                                ,HD.CREATEDNAME ,CONVERT(VARCHAR,HD.MODIFIEDDATE,23) AS MODIFIEDDATE
                                ,CONVERT(VARCHAR, HD.EXPIREDATE,23) AS EXPIREDATE 
                                ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC
                                ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
                                ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                    LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DL ON DT.DOCREFER = DL.DLDOCNO
									LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHG ON DT.DOCREFER = CHG.DOCID
									LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DS ON DT.DOCREFER = DS.DSDOCNO
                                WHERE HD.DOCID = '{0}'  +  '{1}' + '-' + '{2}'
                                AND HD.HEADAPPROVED = '0'
                                AND HD.DOCSTAT = '1' 
                                AND HD.SECTIONID IN (	SELECT PWSECTION FROM SPC_CM_AUTHORIZE
						                                WHERE EMPLID = '{3}' 
						                                AND APPROVEID = '001'
					                                ) 
                                GROUP BY HD.DOCID,HD.CREATEDDATE,HD.CREATEDNAME ,HD.MODIFIEDDATE,HD.EXPIREDATE,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC
								,HD.HEADAPPROVED,HD.HEADAPPROVEDBYNAME, HD.HRAPPROVED,HD.HRAPPROVEDBYNAME 
                                ORDER BY HD.DOCID DESC "
                        , txt_DocId.Text.Trim()
                        , txt_DocDate.Text.Trim()
                        , txt_DocNum.Text.Trim()
                        , ClassCurUser.LogInEmplId);

                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_ApproveHD.DataSource = dt;
                    }
                    else
                    {
                        rgv_ApproveHD.DataSource = dt;
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

            #endregion

            #region rdb_SelectAll

            try
            {
                if (rdb_SelectAll.ToggleState == ToggleState.On)
                {
                    sqlCommand.CommandText = string.Format(
                            @" SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE 
                                 ,HD.CREATEDNAME ,CONVERT(VARCHAR,HD.MODIFIEDDATE,23) AS MODIFIEDDATE
                                ,CONVERT(VARCHAR, HD.EXPIREDATE,23) AS EXPIREDATE 
                                ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC
                                ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
                                ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                    LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DL ON DT.DOCREFER = DL.DLDOCNO
									LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHG ON DT.DOCREFER = CHG.DOCID
									LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DS ON DT.DOCREFER = DS.DSDOCNO
                                WHERE HD.HEADAPPROVED = '0'
                                AND HD.DOCSTAT = '1'
                                AND HD.SECTIONID IN (	SELECT PWSECTION FROM SPC_CM_AUTHORIZE
						                                WHERE EMPLID = '{0}' 
						                                AND APPROVEID = '001'
					                                ) 
                                GROUP BY HD.DOCID,HD.CREATEDDATE,HD.CREATEDNAME ,HD.MODIFIEDDATE,HD.EXPIREDATE,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC
								,HD.HEADAPPROVED,HD.HEADAPPROVEDBYNAME, HD.HRAPPROVED,HD.HRAPPROVEDBYNAME 
                                ORDER BY HD.DOCID DESC "
                            , ClassCurUser.LogInEmplId);

                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_ApproveHD.DataSource = dt;
                    }
                    else
                    {
                        rgv_ApproveHD.DataSource = dt;
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

            #endregion

        }

        #endregion

        #region GriewView

        void InitializeSetGridView()
        {

            // GridData 
            this.rgv_ApproveHD.Dock = DockStyle.Fill;
            this.rgv_ApproveHD.ReadOnly = true;
            this.rgv_ApproveHD.AutoGenerateColumns = true;
            this.rgv_ApproveHD.EnableFiltering = false;
            this.rgv_ApproveHD.AllowAddNewRow = false;
            this.rgv_ApproveHD.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_ApproveHD.ShowGroupedColumns = true;
            this.rgv_ApproveHD.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_ApproveHD.EnableHotTracking = true;
            this.rgv_ApproveHD.AutoSizeRows = true;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_ApproveHD.Columns.Add(DOCID);

            GridViewTextBoxColumn CREATEDDATE = new GridViewTextBoxColumn();
            CREATEDDATE.Name = "CREATEDDATE";
            CREATEDDATE.FieldName = "CREATEDDATE";
            CREATEDDATE.HeaderText = "วันที่สร้าง";
            CREATEDDATE.IsVisible = true;
            CREATEDDATE.ReadOnly = true;
            CREATEDDATE.BestFit();
            rgv_ApproveHD.Columns.Add(CREATEDDATE);

            GridViewTextBoxColumn CREATEDNAME = new GridViewTextBoxColumn();
            CREATEDNAME.Name = "CREATEDNAME";
            CREATEDNAME.FieldName = "CREATEDNAME";
            CREATEDNAME.HeaderText = "ผู้สร้าง";
            CREATEDNAME.IsVisible = true;
            CREATEDNAME.ReadOnly = true;
            CREATEDNAME.BestFit();
            rgv_ApproveHD.Columns.Add(CREATEDNAME);

            GridViewTextBoxColumn MODIFIEDDATE = new GridViewTextBoxColumn();
            MODIFIEDDATE.Name = "MODIFIEDDATE";
            MODIFIEDDATE.FieldName = "MODIFIEDDATE";
            MODIFIEDDATE.HeaderText = "แก้ไขล่าสุด";
            MODIFIEDDATE.IsVisible = true;
            MODIFIEDDATE.ReadOnly = true;
            MODIFIEDDATE.BestFit();
            rgv_ApproveHD.Columns.Add(MODIFIEDDATE);

            GridViewTextBoxColumn EXPIREDATE = new GridViewTextBoxColumn();
            EXPIREDATE.Name = "EXPIREDATE";
            EXPIREDATE.FieldName = "EXPIREDATE";
            EXPIREDATE.HeaderText = "อนุมัติภายใน";
            EXPIREDATE.IsVisible = true;
            EXPIREDATE.ReadOnly = true;
            EXPIREDATE.BestFit();
            rgv_ApproveHD.Columns.Add(EXPIREDATE);

            GridViewTextBoxColumn EMPLNAME = new GridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.FieldName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.IsVisible = true;
            EMPLNAME.ReadOnly = true;
            EMPLNAME.BestFit();
            rgv_ApproveHD.Columns.Add(EMPLNAME);

            GridViewTextBoxColumn DOCTYP = new GridViewTextBoxColumn();
            DOCTYP.Name = "DOCTYP";
            DOCTYP.FieldName = "DOCTYP";
            DOCTYP.HeaderText = "ชนิดเอกสารยกเลิก";
            DOCTYP.IsVisible = false;
            DOCTYP.ReadOnly = true;
            DOCTYP.BestFit();
            rgv_ApproveHD.Columns.Add(DOCTYP);

            GridViewTextBoxColumn TYPDESC = new GridViewTextBoxColumn();
            TYPDESC.Name = "TYPDESC";
            TYPDESC.FieldName = "TYPDESC";
            TYPDESC.HeaderText = "เอกสารยกเลิก";
            TYPDESC.IsVisible = true;
            TYPDESC.ReadOnly = true;
            TYPDESC.BestFit();
            rgv_ApproveHD.Columns.Add(TYPDESC);

            GridViewTextBoxColumn HEADAPPROVED = new GridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.FieldName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.IsVisible = true;
            HEADAPPROVED.ReadOnly = true;
            HEADAPPROVED.BestFit();
            rgv_ApproveHD.Columns.Add(HEADAPPROVED);

            GridViewTextBoxColumn HEADAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.FieldName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.IsVisible = true;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            HEADAPPROVEDBYNAME.BestFit();
            rgv_ApproveHD.Columns.Add(HEADAPPROVEDBYNAME);


            GridViewTextBoxColumn HRAPPROVED = new GridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.FieldName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.IsVisible = true;
            HRAPPROVED.ReadOnly = true;
            HRAPPROVED.BestFit();
            rgv_ApproveHD.Columns.Add(HRAPPROVED);

            GridViewTextBoxColumn HRAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.FieldName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.IsVisible = true;
            HRAPPROVEDBYNAME.ReadOnly = true;
            HRAPPROVEDBYNAME.BestFit();
            rgv_ApproveHD.Columns.Add(HRAPPROVEDBYNAME);

            GridViewCommandColumn ApproveButton = new GridViewCommandColumn();
            ApproveButton.Name = "ApproveButton";
            ApproveButton.FieldName = "ApproveButton";
            ApproveButton.HeaderText = "อนุมัติ";
            rgv_ApproveHD.Columns.Add(ApproveButton);

        }

        private void rgv_ApproveHD_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (!(e.CellElement.RowElement is GridTableHeaderRowElement))
            {
                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    GridViewCommandColumn column = (GridViewCommandColumn)e.CellElement.ColumnInfo;
                    RadButtonElement button = (RadButtonElement)e.CellElement.Children[0];

                    if (column.Name == "ApproveButton")
                    {
                        RadButtonElement element = (RadButtonElement)e.CellElement.Children[0];
                        element.Text = "อนุมัติ";
                        element.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                }
            }
        }
        private void rgv_ApproveHD_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
                if (e.Column.Name == "ApproveButton")
                {
                    
                        GridViewRowInfo row = (GridViewRowInfo)rgv_ApproveHD.Rows[e.RowIndex];

                        string DocId = row.Cells["DOCID"].Value.ToString();
                        string Doctype = row.Cells["DOCTYP"].Value.ToString().Trim();
                        
                        
                        if ((DateTime.Compare(DateTime.Now.Date, Convert.ToDateTime(rgv_ApproveHD.CurrentRow.Cells["EXPIREDATE"].Value.ToString()))) > 0)
                        {
                            MessageBox.Show("ไม่สามารถอนุมัติได้ เนื่องจากเกินวันที่ที่อนุมัติ", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            if (Doctype == "001")
                            {
                                using (Cancle_ApproveHD_DetailDL frm = new Cancle_ApproveHD_DetailDL(DocId, Doctype))
                                {
                                    //frm.Show();
                                    if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                                    {
                                        //โหลดข้อมูลใหม่
                                        GetdataCN();
                                    }
                                }

                            }
                            else if (Doctype == "002")
                            {
                                using (Cancle_ApproveHD_DetailCHD frm = new Cancle_ApproveHD_DetailCHD(DocId, Doctype))
                                {
                                    //frm.Show();
                                    if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                                    {
                                        //โหลดข้อมูลใหม่
                                        GetdataCN();
                                    }
                                }
                                
                            }
                            if (Doctype == "003")
                            {
                                
                                using (Cancle_ApproveHD_DetailDS frm = new Cancle_ApproveHD_DetailDS(DocId, Doctype))
                                {
                                    //frm.Show();
                                    if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                                    {
                                        //โหลดข้อมูลใหม่
                                        GetdataCN();
                                    }
                                }
                            }
                        }
                }
            }
        }

        #endregion
    }
}
