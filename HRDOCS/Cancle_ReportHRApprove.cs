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
    public partial class Cancle_ReportHRApprove : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        public Cancle_ReportHRApprove()
        {
            InitializeComponent();

            this.Btn_Search.Click += new EventHandler(Btn_Search_Click);
            this.Btn_ExportToExcel.Click += new EventHandler(Btn_ExportToExcel_Click);
            GridViewReport();
        }


        private void Cancle_ReportHRApprove_Load(object sender, EventArgs e)
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
                TypeDate = "HD.HRAPPROVEDDATE";
            }
            else
            {
                TypeDate = "HD.TRANSDATE";
            }
            if (Rcb_Section.Checked == true)
            {
                Section = " AND HD.SECTIONID = '" + Cbb_Section.SelectedValue.ToString() + "' ";
            }
            else
            {
                Section = "";
            }

            //ใบลาหยุด
            #region
            if (Rdb_DL.ToggleState == ToggleState.On)
            {
                this.rgv_ReportDLHR.Visible = true;
                this.rgv_ReportDSHR.Visible = false;
                this.rgv_ReportCHDHR.Visible = false;
                //ทั้งหมด
                #region Rdb_All
                if (Rdb_All.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								,(DOCTYP.TYPDESC + ' [ ' +  DTL.LEAVETYPEDETAIL + ' ] '  ) AS TYPDESC
								,DT.DOCREFER
                                ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER,DT.REASON
                                ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME 
								,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME  
								,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                    LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DL ON DT.DOCREFER = DL.DLDOCNO
									LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEDT] DTL ON DL.DLDOCNO = DTL.DLDOCNO	
                                WHERE HD.DOCSTAT != 0 
                                AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '001' 
                                AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 

                                GROUP BY HD.DOCID, HD.CREATEDDATE,HD.SECTIONNAME ,HD.DEPTNAME,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								    ,DOCTYP.TYPDESC,DTL.LEAVETYPEDETAIL,DT.DOCREFER,DT.DATEREFER,DT.REASON,HD.HEADAPPROVED
                                    ,HD.HEADAPPROVEDBYNAME,HD.HEADAPPROVEDDATE,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME,HD.HRAPPROVEDDATE
                                ORDER BY HD.DOCID "

//                            @" SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                            //                                ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
                            //								,(DOCTYP.TYPDESC + ' [ ' +  DTL.LEAVETYPEDETAIL + ' ] '  ) AS TYPDESC
                            //								,DT.DOCREFER
                            //                                ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER,DT.REASON
                            //                                ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                            //                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                            //                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            //                                ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
                            //								,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                            //                                ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                            //                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                            //                                                            ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                            //                                ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
                            //								,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                            //                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                            //                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                            //                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                            //                                    LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DL ON DT.DOCREFER = DL.DLDOCNO
                            //									LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEDT] DTL ON DL.DLDOCNO = DTL.DLDOCNO	
                            //                                WHERE HD.DOCSTAT != 0
                            //                                AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '001'
                            //                                AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                            //                                GROUP BY HD.DOCID, HD.CREATEDDATE,HD.SECTIONNAME ,HD.DEPTNAME,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
                            //								,DOCTYP.TYPDESC,DTL.LEAVETYPEDETAIL,DT.DOCREFER,DT.DATEREFER,DT.REASON,HD.HEADAPPROVED
                            //                                ,HD.HEADAPPROVEDBYNAME,HD.HEADAPPROVEDDATE,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME,HD.HRAPPROVEDDATE
                            //                                ORDER BY {2}"

                            , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                            , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                            , TypeDate, Section);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {

                            rgv_ReportDLHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportDLHR.DataSource = dt;
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
                #endregion

                //hr อนุมัติ
                #region Rdb_Approve
                else if (Rdb_Approve.ToggleState == ToggleState.On)
                {
                    try
                    {
                        {
                            sqlCommand.CommandText = string.Format(
                                @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								,(DOCTYP.TYPDESC + ' [ ' +  DTL.LEAVETYPEDETAIL + ' ] '  ) AS TYPDESC
								,DT.DOCREFER
                                ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER,DT.REASON
                                ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                    LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DL ON DT.DOCREFER = DL.DLDOCNO
									LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEDT] DTL ON DL.DLDOCNO = DTL.DLDOCNO	
                                WHERE HD.DOCSTAT != 0 
                                AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '001' 
                                AND HD.HRAPPROVED = '1'
                                AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                                GROUP BY HD.DOCID, HD.CREATEDDATE,HD.SECTIONNAME ,HD.DEPTNAME,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								    ,DOCTYP.TYPDESC,DTL.LEAVETYPEDETAIL,DT.DOCREFER,DT.DATEREFER,DT.REASON,HD.HEADAPPROVED
                                    ,HD.HEADAPPROVEDBYNAME,HD.HEADAPPROVEDDATE,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME,HD.HRAPPROVEDDATE
                                ORDER BY HD.DOCID "

                               , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                               , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                               , TypeDate, Section);

                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                rgv_ReportDLHR.DataSource = dt;
                            }
                            else
                            {
                                rgv_ReportDLHR.DataSource = dt;
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
                #endregion

                //hr ไม่อนุมัติ
                #region Rdb_NoApprove
                else if (Rdb_NoApprove.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                               @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								,(DOCTYP.TYPDESC + ' [ ' +  DTL.LEAVETYPEDETAIL + ' ] '  ) AS TYPDESC
								,DT.DOCREFER
                                ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER,DT.REASON
                                ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                    LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DL ON DT.DOCREFER = DL.DLDOCNO
									LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEDT] DTL ON DL.DLDOCNO = DTL.DLDOCNO	
                                WHERE HD.DOCSTAT != 0 
                                AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '001' 
                                AND HD.HRAPPROVED = '2'
                                AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                                GROUP BY HD.DOCID, HD.CREATEDDATE,HD.SECTIONNAME ,HD.DEPTNAME,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								    ,DOCTYP.TYPDESC,DTL.LEAVETYPEDETAIL,DT.DOCREFER,DT.DATEREFER,DT.REASON,HD.HEADAPPROVED
                                    ,HD.HEADAPPROVEDBYNAME,HD.HEADAPPROVEDDATE,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME,HD.HRAPPROVEDDATE
                                ORDER BY HD.DOCID "


                               , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                               , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                               , TypeDate, Section);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            rgv_ReportDLHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportDLHR.DataSource = dt;
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
                #endregion

                // รออนุมัติ
                #region Rdb_WaitApprovegion

                else if (Rdb_WaitApprove.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                               @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								,(DOCTYP.TYPDESC + ' [ ' +  DTL.LEAVETYPEDETAIL + ' ] '  ) AS TYPDESC
								,DT.DOCREFER
                                ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER,DT.REASON
                                ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                        WHEN '2' THEN 'ไม่อนุมัติ'  
                                                            ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                    LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                    LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DL ON DT.DOCREFER = DL.DLDOCNO
									LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEDT] DTL ON DL.DLDOCNO = DTL.DLDOCNO	
                                WHERE HD.DOCSTAT != 0 
                                AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '001'                             
                                AND HD.HRAPPROVED = '0' 
                                AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                                GROUP BY HD.DOCID, HD.CREATEDDATE,HD.SECTIONNAME ,HD.DEPTNAME,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								        ,DOCTYP.TYPDESC,DTL.LEAVETYPEDETAIL,DT.DOCREFER,DT.DATEREFER,DT.REASON,HD.HEADAPPROVED
                                        ,HD.HEADAPPROVEDBYNAME,HD.HEADAPPROVEDDATE,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME,HD.HRAPPROVEDDATE
                                ORDER BY HD.DOCID "

                               , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                               , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                               , TypeDate, Section);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            rgv_ReportDLHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportDLHR.DataSource = dt;
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

                #endregion
            }
            #endregion

            //ใบเปลี่ยนกะ
            #region
            else if (Rdb_DS.ToggleState == ToggleState.On)
            {
                this.rgv_ReportDSHR.Visible = true;
                this.rgv_ReportDLHR.Visible = false;
                this.rgv_ReportCHDHR.Visible = false;
                //ทั้งหมด
                #region Rdb_All
                if (Rdb_All.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                        ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								                        ,DOCTYP.TYPDESC  AS TYPDESC,DT.DOCREFER
                                                        ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER
								                        ,(DTS.FROMSHIFTID + ' [ ' + DTS.FROMSHIFTDESC +' ] ' ) AS FROMSHIFT
								                        ,(DTS.TOSHIFTID + ' [ ' + DTS.TOSHIFTDESC +' ] ' ) AS TOSHIFT
								                        ,DT.REASON
                                                        ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                                        ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								                        ,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                                        ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                                        ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								                        ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                                        FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                            		                        LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DS ON DT.DOCREFER = DS.DSDOCNO 
									                        LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTDT] DTS ON DS.DSDOCNO = DTS.DSDOCNO 
                                                        WHERE HD.DOCSTAT != 0
                                                        AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '003'
                                                        AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 

                                                        GROUP BY HD.DOCID,HD.CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                            ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC ,DT.DOCREFER ,DT.DATEREFER 
														    ,DTS.FROMSHIFTID ,DTS.FROMSHIFTDESC ,DTS.TOSHIFTID ,DTS.TOSHIFTDESC ,DT.REASON 
														    ,HD.HEADAPPROVED,HD.HEADAPPROVEDBYNAME ,HD.HEADAPPROVEDDATE
														    ,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME ,HD.HRAPPROVEDDATE
                                                        ORDER BY HD.DOCID "

                            , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                            , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                            , TypeDate, Section);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {

                            rgv_ReportDSHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportDSHR.DataSource = dt;
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
                #endregion

                // HR อนุมัติ
                #region Rdb_HRAprove
                if (Rdb_Approve.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                        ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								                        ,DOCTYP.TYPDESC  AS TYPDESC,DT.DOCREFER
                                                        ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER
								                        ,(DTS.FROMSHIFTID + ' [ ' + DTS.FROMSHIFTDESC +' ] ' ) AS FROMSHIFT
								                        ,(DTS.TOSHIFTID + ' [ ' + DTS.TOSHIFTDESC +' ] ' ) AS TOSHIFT
								                        ,DT.REASON
                                                        ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                                        ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								                        ,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                                        ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                                        ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								                        ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                                        FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                            		                        LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DS ON DT.DOCREFER = DS.DSDOCNO 
									                        LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTDT] DTS ON DS.DSDOCNO = DTS.DSDOCNO 
                                                        WHERE HD.DOCSTAT != 0
                                                        AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '003'
                                                        AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                                                        AND HD.HRAPPROVED = '1'
                                                        GROUP BY HD.DOCID,HD.CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                            ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC ,DT.DOCREFER ,DT.DATEREFER 
														    ,DTS.FROMSHIFTID ,DTS.FROMSHIFTDESC ,DTS.TOSHIFTID ,DTS.TOSHIFTDESC ,DT.REASON 
														    ,HD.HEADAPPROVED,HD.HEADAPPROVEDBYNAME ,HD.HEADAPPROVEDDATE
														    ,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME ,HD.HRAPPROVEDDATE
                                                        ORDER BY HD.DOCID "

                            , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                            , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                            , TypeDate, Section);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {

                            rgv_ReportDSHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportDSHR.DataSource = dt;
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
                #endregion

                // HR ไม่อนุมัติ
                #region Rdb_NoApprove
                if (Rdb_NoApprove.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                        ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								                        ,DOCTYP.TYPDESC  AS TYPDESC,DT.DOCREFER
                                                        ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER
								                        ,(DTS.FROMSHIFTID + ' [ ' + DTS.FROMSHIFTDESC +' ] ' ) AS FROMSHIFT
								                        ,(DTS.TOSHIFTID + ' [ ' + DTS.TOSHIFTDESC +' ] ' ) AS TOSHIFT
								                        ,DT.REASON
                                                        ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                                        ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								                        ,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                                        ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                                        ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								                        ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                                        FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                            		                        LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DS ON DT.DOCREFER = DS.DSDOCNO 
									                        LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTDT] DTS ON DS.DSDOCNO = DTS.DSDOCNO 
                                                        WHERE HD.DOCSTAT != 0
                                                        AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '003'
                                                        AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                                                        AND HD.HRAPPROVED = '2'
                                                        GROUP BY HD.DOCID,HD.CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                            ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC ,DT.DOCREFER ,DT.DATEREFER 
														    ,DTS.FROMSHIFTID ,DTS.FROMSHIFTDESC ,DTS.TOSHIFTID ,DTS.TOSHIFTDESC ,DT.REASON 
														    ,HD.HEADAPPROVED,HD.HEADAPPROVEDBYNAME ,HD.HEADAPPROVEDDATE
														    ,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME ,HD.HRAPPROVEDDATE
                                                        ORDER BY HD.DOCID "

                            , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                            , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                            , TypeDate, Section);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {

                            rgv_ReportDSHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportDSHR.DataSource = dt;
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
                #endregion

                // รอHRอนุมัติ
                #region Rdb_WaitApprove
                if (Rdb_WaitApprove.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                        ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								                        ,DOCTYP.TYPDESC  AS TYPDESC,DT.DOCREFER
                                                        ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER
								                        ,(DTS.FROMSHIFTID + ' [ ' + DTS.FROMSHIFTDESC +' ] ' ) AS FROMSHIFT
								                        ,(DTS.TOSHIFTID + ' [ ' + DTS.TOSHIFTDESC +' ] ' ) AS TOSHIFT
								                        ,DT.REASON
                                                        ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                                        ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								                        ,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                                        ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                                        ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								                        ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                                        FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                            		                        LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DS ON DT.DOCREFER = DS.DSDOCNO 
									                        LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTDT] DTS ON DS.DSDOCNO = DTS.DSDOCNO 
                                                        WHERE HD.DOCSTAT != 0
                                                        AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '003'
                                                        AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                                                        AND HD.HRAPPROVED = '0'
                                                        GROUP BY HD.DOCID,HD.CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                            ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC ,DT.DOCREFER ,DT.DATEREFER 
														    ,DTS.FROMSHIFTID ,DTS.FROMSHIFTDESC ,DTS.TOSHIFTID ,DTS.TOSHIFTDESC ,DT.REASON 
														    ,HD.HEADAPPROVED,HD.HEADAPPROVEDBYNAME ,HD.HEADAPPROVEDDATE
														    ,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME ,HD.HRAPPROVEDDATE
                                                        ORDER BY HD.DOCID "

                            , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                            , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                            , TypeDate, Section);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {

                            rgv_ReportDSHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportDSHR.DataSource = dt;
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
                #endregion
            }
            #endregion

            //เปลี่ยนวันหยุด
            #region
            else if (Rdb_CHD.ToggleState == ToggleState.On)
            {
                this.rgv_ReportDSHR.Visible = false;
                this.rgv_ReportDLHR.Visible = false;
                this.rgv_ReportCHDHR.Visible = true;

                //ทั้งหมด
                #region Rdb_All
                if (Rdb_All.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                        ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								                        ,DOCTYP.TYPDESC  AS TYPDESC,DT.DOCREFER
                                                        ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER
								                        ,(DT.SHIFTID_CHD + ' [ ' + DT.SHIFTDESC_CHD + ' ] ') AS EMPLSHIFT  
								                        ,DT.FROMHOLIDAY_CHD ,DT.REASON
                                                        ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                                        ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								                        ,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                                        ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                                        ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								                        ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                            FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHD ON DT.DOCREFER = CHD.DOCID
	                            LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYDT] CHDT ON CHD.DOCID = CHDT.DOCID 
                            WHERE HD.DOCSTAT != 0
                            AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '002'
                            AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                            GROUP BY HD.DOCID ,HD.CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                     ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC ,DT.DOCREFER ,DT.DATEREFER
                                     ,DT.SHIFTID_CHD ,DT.SHIFTDESC_CHD ,DT.FROMHOLIDAY_CHD ,DT.REASON
                                     ,HD.HEADAPPROVED ,HD.HEADAPPROVEDBYNAME ,HD.HEADAPPROVEDDATE
                                     ,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME ,HD.HRAPPROVEDDATE
                            ORDER BY HD.DOCID"


                            , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                            , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                            , TypeDate, Section);
                        //,CHDT.FROMHOLIDAY

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            rgv_ReportCHDHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportCHDHR.DataSource = dt;
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
                #endregion

                // HR_อนุมัติ
                #region Rdb_Approve
                if (Rdb_Approve.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                        ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								                        ,DOCTYP.TYPDESC  AS TYPDESC,DT.DOCREFER
                                                        ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER
								                        ,(DT.SHIFTID_CHD + ' [ ' + DT.SHIFTDESC_CHD + ' ] ') AS EMPLSHIFT  
								                        ,DT.FROMHOLIDAY_CHD ,DT.REASON
                                                        ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                                        ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								                        ,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                                        ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                                        ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								                        ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                                        FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                            		                        LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHD ON DT.DOCREFER = CHD.DOCID
									                        LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYDT] CHDT ON CHD.DOCID = CHDT.DOCID 
                                                        WHERE HD.DOCSTAT != 0
                                                        AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '002'
                                                        AND HD.HRAPPROVED = '1'
                                                            AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                                                        GROUP BY HD.DOCID ,HD.CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                            ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC ,DT.DOCREFER ,DT.DATEREFER
                                                            ,DT.SHIFTID_CHD ,DT.SHIFTDESC_CHD ,DT.FROMHOLIDAY_CHD  ,DT.REASON
                                                            ,HD.HEADAPPROVED ,HD.HEADAPPROVEDBYNAME ,HD.HEADAPPROVEDDATE
                                                            ,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME ,HD.HRAPPROVEDDATE
                                                        ORDER BY HD.DOCID"


                            , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                            , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                            , TypeDate, Section);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {

                            rgv_ReportCHDHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportCHDHR.DataSource = dt;
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
                #endregion

                // HR_ไม่อนุมัติ
                #region Rdb_Approve
                if (Rdb_NoApprove.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                        ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								                        ,DOCTYP.TYPDESC  AS TYPDESC,DT.DOCREFER
                                                        ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER
								                        ,(DT.SHIFTID_CHD + ' [ ' + DT.SHIFTDESC_CHD + ' ] ') AS EMPLSHIFT  
								                        ,DT.FROMHOLIDAY_CHD ,DT.REASON
                                                        ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                                        ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								                        ,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                                        ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                                        ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								                        ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                                        FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                            		                        LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHD ON DT.DOCREFER = CHD.DOCID
									                        LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYDT] CHDT ON CHD.DOCID = CHDT.DOCID 
                                                        WHERE HD.DOCSTAT != 0
                                                        AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '002'
                                                        AND HD.HRAPPROVED = '2'
                                                            AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                                                        GROUP BY HD.DOCID ,HD.CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                            ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC ,DT.DOCREFER ,DT.DATEREFER
                                                            ,DT.SHIFTID_CHD ,DT.SHIFTDESC_CHD ,DT.FROMHOLIDAY_CHD  ,DT.REASON
                                                            ,HD.HEADAPPROVED ,HD.HEADAPPROVEDBYNAME ,HD.HEADAPPROVEDDATE
                                                            ,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME ,HD.HRAPPROVEDDATE
                                                        ORDER BY HD.DOCID"


                            , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                            , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                            , TypeDate, Section);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {

                            rgv_ReportCHDHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportCHDHR.DataSource = dt;
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
                #endregion

                // รออนุมัติ
                #region Rdb_WaitApprove
                if (Rdb_WaitApprove.ToggleState == ToggleState.On)
                {
                    try
                    {
                        sqlCommand.CommandText = string.Format(
                            @"SELECT HD.DOCID,CONVERT(VARCHAR, HD.CREATEDDATE,23) AS CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                        ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP 
								                        ,DOCTYP.TYPDESC  AS TYPDESC,DT.DOCREFER
                                                        ,CONVERT(VARCHAR ,DT.DATEREFER,23) AS DATEREFER
								                        ,(DT.SHIFTID_CHD + ' [ ' + DT.SHIFTDESC_CHD + ' ] ') AS EMPLSHIFT
								                        ,DT.FROMHOLIDAY_CHD,DT.REASON
                                                        ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                                                        ,CASE HD.HEADAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HEADAPPROVEDBYNAME END AS HEADAPPROVEDBYNAME
								                        ,CONVERT(VARCHAR ,HD.HEADAPPROVEDDATE,23) AS HEADAPPROVEDDATE
                                                        ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' WHEN '1' THEN 'อนุมัติ'  
                                                                                WHEN '2' THEN 'ไม่อนุมัติ'  
                                                                                    ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED
                                                        ,CASE HD.HRAPPROVEDBYNAME WHEN NULL THEN '-'  ELSE HD.HRAPPROVEDBYNAME END AS HRAPPROVEDBYNAME 
								                        ,CONVERT(VARCHAR,HD.HRAPPROVEDDATE,23) AS HRAPPROVEDDATE
                                                        FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                                            LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                            		                        LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHD ON DT.DOCREFER = CHD.DOCID
									                        LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYDT] CHDT ON CHD.DOCID = CHDT.DOCID 
                                                        WHERE HD.DOCSTAT != 0
                                                        AND HD.HEADAPPROVED = '1' AND DT.DOCTYP = '002'
                                                        AND HD.HRAPPROVED = '0'
                                                            AND CONVERT(VARCHAR,{2},23) BETWEEN '{0}' AND '{1}' {3} 
                                                        GROUP BY HD.DOCID ,HD.CREATEDDATE ,HD.SECTIONNAME ,HD.DEPTNAME
                                                            ,HD.EMPLID ,HD.EMPLNAME ,DT.DOCTYP ,DOCTYP.TYPDESC ,DT.DOCREFER ,DT.DATEREFER
                                                            ,DT.SHIFTID_CHD ,DT.SHIFTDESC_CHD ,DT.FROMHOLIDAY_CHD,DT.REASON
                                                            ,HD.HEADAPPROVED ,HD.HEADAPPROVEDBYNAME ,HD.HEADAPPROVEDDATE
                                                            ,HD.HRAPPROVED ,HD.HRAPPROVEDBYNAME ,HD.HRAPPROVEDDATE
                                                        ORDER BY HD.DOCID"


                            , dtpStart.Value.Date.ToString("yyyy-MM-dd")
                            , dtpEnd.Value.Date.ToString("yyyy-MM-dd")
                            , TypeDate, Section);

                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {

                            rgv_ReportCHDHR.DataSource = dt;
                        }
                        else
                        {
                            rgv_ReportCHDHR.DataSource = dt;
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
                #endregion
            }
            #endregion

        }
        #endregion

        void GridViewReport()
        {
            #region DL
            this.rgv_ReportDLHR.Dock = DockStyle.Fill;
            this.rgv_ReportDLHR.ReadOnly = true;
            this.rgv_ReportDLHR.AutoGenerateColumns = true;
            this.rgv_ReportDLHR.EnableFiltering = false;
            this.rgv_ReportDLHR.AllowAddNewRow = false;
            this.rgv_ReportDLHR.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_ReportDLHR.ShowGroupedColumns = true;
            this.rgv_ReportDLHR.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_ReportDLHR.EnableHotTracking = true;
            this.rgv_ReportDLHR.AutoSizeRows = true;
            this.rgv_ReportDLHR.Visible = false;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_ReportDLHR.Columns.Add(DOCID);

            GridViewTextBoxColumn EMPLID = new GridViewTextBoxColumn();
            EMPLID.Name = "EMPLID";
            EMPLID.FieldName = "EMPLID";
            EMPLID.HeaderText = "รหัส";
            EMPLID.IsVisible = true;
            EMPLID.ReadOnly = true;
            EMPLID.BestFit();
            rgv_ReportDLHR.Columns.Add(EMPLID);

            GridViewTextBoxColumn EMPLNAME = new GridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.FieldName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.IsVisible = true;
            EMPLNAME.ReadOnly = true;
            EMPLNAME.BestFit();
            rgv_ReportDLHR.Columns.Add(EMPLNAME);

            GridViewTextBoxColumn SECTIONNAME = new GridViewTextBoxColumn();
            SECTIONNAME.Name = "SECTIONNAME";
            SECTIONNAME.FieldName = "SECTIONNAME";
            SECTIONNAME.HeaderText = "แผนก";
            SECTIONNAME.IsVisible = true;
            SECTIONNAME.ReadOnly = true;
            SECTIONNAME.BestFit();
            rgv_ReportDLHR.Columns.Add(SECTIONNAME);

            GridViewTextBoxColumn DEPTNAME = new GridViewTextBoxColumn();
            DEPTNAME.Name = "DEPTNAME";
            DEPTNAME.FieldName = "DEPTNAME";
            DEPTNAME.HeaderText = "ตำแหน่ง";
            DEPTNAME.IsVisible = true;
            DEPTNAME.ReadOnly = true;
            DEPTNAME.BestFit();
            rgv_ReportDLHR.Columns.Add(DEPTNAME);

            GridViewTextBoxColumn TYPDESC = new GridViewTextBoxColumn();
            TYPDESC.Name = "TYPDESC";
            TYPDESC.FieldName = "TYPDESC";
            TYPDESC.HeaderText = "เอกสารยกเลิก";
            TYPDESC.IsVisible = true;
            TYPDESC.ReadOnly = true;
            TYPDESC.BestFit();
            rgv_ReportDLHR.Columns.Add(TYPDESC);

            GridViewTextBoxColumn DOCREFER = new GridViewTextBoxColumn();
            DOCREFER.Name = "DOCREFER";
            DOCREFER.FieldName = "DOCREFER";
            DOCREFER.HeaderText = "เลขที่อ้างอิง";
            DOCREFER.IsVisible = true;
            DOCREFER.ReadOnly = true;
            DOCREFER.BestFit();
            rgv_ReportDLHR.Columns.Add(DOCREFER);

            GridViewTextBoxColumn DATEREFER = new GridViewTextBoxColumn();
            DATEREFER.Name = "DATEREFER";
            DATEREFER.FieldName = "DATEREFER";
            DATEREFER.HeaderText = "วันที่อ้างอิง";
            DATEREFER.IsVisible = true;
            DATEREFER.ReadOnly = true;
            DATEREFER.BestFit();
            rgv_ReportDLHR.Columns.Add(DATEREFER);

            GridViewTextBoxColumn REASON = new GridViewTextBoxColumn();
            REASON.Name = "REASON";
            REASON.FieldName = "REASON";
            REASON.HeaderText = "เหตุผล";
            REASON.IsVisible = true;
            REASON.ReadOnly = true;
            REASON.BestFit();
            rgv_ReportDLHR.Columns.Add(REASON);

            GridViewTextBoxColumn HEADAPPROVED = new GridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.FieldName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.IsVisible = true;
            HEADAPPROVED.ReadOnly = true;
            HEADAPPROVED.BestFit();
            rgv_ReportDLHR.Columns.Add(HEADAPPROVED);

            GridViewTextBoxColumn HEADAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.FieldName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.IsVisible = true;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            HEADAPPROVEDBYNAME.BestFit();
            rgv_ReportDLHR.Columns.Add(HEADAPPROVEDBYNAME);

            GridViewTextBoxColumn HEADAPPROVEDDATE = new GridViewTextBoxColumn();
            HEADAPPROVEDDATE.Name = "HEADAPPROVEDDATE";
            HEADAPPROVEDDATE.FieldName = "HEADAPPROVEDDATE";
            HEADAPPROVEDDATE.HeaderText = "วันที่อนุมัติ";
            HEADAPPROVEDDATE.IsVisible = true;
            HEADAPPROVEDDATE.ReadOnly = true;
            HEADAPPROVEDDATE.BestFit();
            rgv_ReportDLHR.Columns.Add(HEADAPPROVEDDATE);


            GridViewTextBoxColumn HRAPPROVED = new GridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.FieldName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.IsVisible = true;
            HRAPPROVED.ReadOnly = true;
            HRAPPROVED.BestFit();
            rgv_ReportDLHR.Columns.Add(HRAPPROVED);


            GridViewTextBoxColumn HRAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.FieldName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.IsVisible = true;
            HRAPPROVEDBYNAME.ReadOnly = true;
            HRAPPROVEDBYNAME.BestFit();
            rgv_ReportDLHR.Columns.Add(HRAPPROVEDBYNAME);

            GridViewTextBoxColumn HRAPPORVEREMARK = new GridViewTextBoxColumn();
            HRAPPORVEREMARK.Name = "HRAPPORVEREMARK";
            HRAPPORVEREMARK.FieldName = "HRAPPORVEREMARK";
            HRAPPORVEREMARK.HeaderText = "หมายเหตุ";
            HRAPPORVEREMARK.IsVisible = true;
            HRAPPORVEREMARK.ReadOnly = true;
            HRAPPORVEREMARK.BestFit();
            rgv_ReportDLHR.Columns.Add(HRAPPORVEREMARK);

            GridViewTextBoxColumn HRAPPROVEDDATE = new GridViewTextBoxColumn();
            HRAPPROVEDDATE.Name = "HRAPPROVEDDATE";
            HRAPPROVEDDATE.FieldName = "HRAPPROVEDDATE";
            HRAPPROVEDDATE.HeaderText = "วันที่อนุมัติ";
            HRAPPROVEDDATE.IsVisible = true;
            HRAPPROVEDDATE.ReadOnly = true;
            HRAPPROVEDDATE.BestFit();
            rgv_ReportDLHR.Columns.Add(HRAPPROVEDDATE);
            #endregion

            #region DS
            this.rgv_ReportDSHR.Dock = DockStyle.Fill;
            this.rgv_ReportDSHR.ReadOnly = true;
            this.rgv_ReportDSHR.AutoGenerateColumns = true;
            this.rgv_ReportDSHR.EnableFiltering = false;
            this.rgv_ReportDSHR.AllowAddNewRow = false;
            this.rgv_ReportDSHR.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_ReportDSHR.ShowGroupedColumns = true;
            this.rgv_ReportDSHR.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_ReportDSHR.EnableHotTracking = true;
            this.rgv_ReportDSHR.AutoSizeRows = true;
            this.rgv_ReportDSHR.Visible = false;

            GridViewTextBoxColumn DOCID_DS = new GridViewTextBoxColumn();
            DOCID_DS.Name = "DOCID";
            DOCID_DS.FieldName = "DOCID";
            DOCID_DS.HeaderText = "เลขที่เอกสาร";
            DOCID_DS.IsVisible = true;
            DOCID_DS.ReadOnly = true;
            DOCID_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(DOCID_DS);

            GridViewTextBoxColumn EMPLID_DS = new GridViewTextBoxColumn();
            EMPLID_DS.Name = "EMPLID";
            EMPLID_DS.FieldName = "EMPLID";
            EMPLID_DS.HeaderText = "รหัส";
            EMPLID_DS.IsVisible = true;
            EMPLID_DS.ReadOnly = true;
            EMPLID_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(EMPLID_DS);

            GridViewTextBoxColumn EMPLNAME_DS = new GridViewTextBoxColumn();
            EMPLNAME_DS.Name = "EMPLNAME";
            EMPLNAME_DS.FieldName = "EMPLNAME";
            EMPLNAME_DS.HeaderText = "ชื่อ";
            EMPLNAME_DS.IsVisible = true;
            EMPLNAME_DS.ReadOnly = true;
            EMPLNAME_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(EMPLNAME_DS);

            GridViewTextBoxColumn SECTIONNAME_DS = new GridViewTextBoxColumn();
            SECTIONNAME_DS.Name = "SECTIONNAME";
            SECTIONNAME_DS.FieldName = "SECTIONNAME";
            SECTIONNAME_DS.HeaderText = "แผนก";
            SECTIONNAME_DS.IsVisible = true;
            SECTIONNAME_DS.ReadOnly = true;
            SECTIONNAME_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(SECTIONNAME_DS);

            GridViewTextBoxColumn DEPTNAME_DS = new GridViewTextBoxColumn();
            DEPTNAME_DS.Name = "DEPTNAME";
            DEPTNAME_DS.FieldName = "DEPTNAME";
            DEPTNAME_DS.HeaderText = "ตำแหน่ง";
            DEPTNAME_DS.IsVisible = true;
            DEPTNAME_DS.ReadOnly = true;
            DEPTNAME_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(DEPTNAME_DS);

            GridViewTextBoxColumn TYPDESC_DS = new GridViewTextBoxColumn();
            TYPDESC_DS.Name = "TYPDESC";
            TYPDESC_DS.FieldName = "TYPDESC";
            TYPDESC_DS.HeaderText = "เอกสารยกเลิก";
            TYPDESC_DS.IsVisible = true;
            TYPDESC_DS.ReadOnly = true;
            TYPDESC_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(TYPDESC_DS);

            GridViewTextBoxColumn DOCREFER_DS = new GridViewTextBoxColumn();
            DOCREFER_DS.Name = "DOCREFER";
            DOCREFER_DS.FieldName = "DOCREFER";
            DOCREFER_DS.HeaderText = "เลขที่อ้างอิง";
            DOCREFER_DS.IsVisible = true;
            DOCREFER_DS.ReadOnly = true;
            DOCREFER_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(DOCREFER_DS);

            GridViewTextBoxColumn DATEREFER_DS = new GridViewTextBoxColumn();
            DATEREFER_DS.Name = "DATEREFER";
            DATEREFER_DS.FieldName = "DATEREFER";
            DATEREFER_DS.HeaderText = "วันที่อ้างอิง";
            DATEREFER_DS.IsVisible = true;
            DATEREFER_DS.ReadOnly = true;
            DATEREFER_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(DATEREFER_DS);

            GridViewTextBoxColumn FROMSHIFT_DS = new GridViewTextBoxColumn();
            FROMSHIFT_DS.Name = "FROMSHIFT";
            FROMSHIFT_DS.FieldName = "FROMSHIFT";
            FROMSHIFT_DS.HeaderText = "กะเดิม";
            FROMSHIFT_DS.IsVisible = true;
            FROMSHIFT_DS.ReadOnly = true;
            FROMSHIFT_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(FROMSHIFT_DS);

            GridViewTextBoxColumn TOSHIFT_DS = new GridViewTextBoxColumn();
            TOSHIFT_DS.Name = "TOSHIFT";
            TOSHIFT_DS.FieldName = "TOSHIFT";
            TOSHIFT_DS.HeaderText = "กะใหม่";
            TOSHIFT_DS.IsVisible = true;
            TOSHIFT_DS.ReadOnly = true;
            TOSHIFT_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(TOSHIFT_DS);


            GridViewTextBoxColumn REASON_DS = new GridViewTextBoxColumn();
            REASON_DS.Name = "REASON";
            REASON_DS.FieldName = "REASON";
            REASON_DS.HeaderText = "เหตุผล";
            REASON_DS.IsVisible = true;
            REASON_DS.ReadOnly = true;
            REASON_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(REASON_DS);

            GridViewTextBoxColumn HEADAPPROVED_DS = new GridViewTextBoxColumn();
            HEADAPPROVED_DS.Name = "HEADAPPROVED";
            HEADAPPROVED_DS.FieldName = "HEADAPPROVED";
            HEADAPPROVED_DS.HeaderText = "หน./ผช.";
            HEADAPPROVED_DS.IsVisible = true;
            HEADAPPROVED_DS.ReadOnly = true;
            HEADAPPROVED_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(HEADAPPROVED_DS);

            GridViewTextBoxColumn HEADAPPROVEDBYNAME_DS = new GridViewTextBoxColumn();
            HEADAPPROVEDBYNAME_DS.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME_DS.FieldName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME_DS.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME_DS.IsVisible = true;
            HEADAPPROVEDBYNAME_DS.ReadOnly = true;
            HEADAPPROVEDBYNAME_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(HEADAPPROVEDBYNAME_DS);

            GridViewTextBoxColumn HEADAPPROVEDDATE_DS = new GridViewTextBoxColumn();
            HEADAPPROVEDDATE_DS.Name = "HEADAPPROVEDDATE";
            HEADAPPROVEDDATE_DS.FieldName = "HEADAPPROVEDDATE";
            HEADAPPROVEDDATE_DS.HeaderText = "วันที่อนุมัติ";
            HEADAPPROVEDDATE_DS.IsVisible = true;
            HEADAPPROVEDDATE_DS.ReadOnly = true;
            HEADAPPROVEDDATE_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(HEADAPPROVEDDATE_DS);


            GridViewTextBoxColumn HRAPPROVED_DS = new GridViewTextBoxColumn();
            HRAPPROVED_DS.Name = "HRAPPROVED";
            HRAPPROVED_DS.FieldName = "HRAPPROVED";
            HRAPPROVED_DS.HeaderText = "บุคคล";
            HRAPPROVED_DS.IsVisible = true;
            HRAPPROVED_DS.ReadOnly = true;
            HRAPPROVED_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(HRAPPROVED_DS);


            GridViewTextBoxColumn HRAPPROVEDBYNAME_DS = new GridViewTextBoxColumn();
            HRAPPROVEDBYNAME_DS.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME_DS.FieldName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME_DS.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME_DS.IsVisible = true;
            HRAPPROVEDBYNAME_DS.ReadOnly = true;
            HRAPPROVEDBYNAME_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(HRAPPROVEDBYNAME_DS);

            GridViewTextBoxColumn HRAPPORVEREMARK_DS = new GridViewTextBoxColumn();
            HRAPPORVEREMARK_DS.Name = "HRAPPORVEREMARK";
            HRAPPORVEREMARK_DS.FieldName = "HRAPPORVEREMARK";
            HRAPPORVEREMARK_DS.HeaderText = "หมายเหตุ";
            HRAPPORVEREMARK_DS.IsVisible = true;
            HRAPPORVEREMARK_DS.ReadOnly = true;
            HRAPPORVEREMARK_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(HRAPPORVEREMARK_DS);

            GridViewTextBoxColumn HRAPPROVEDDATE_DS = new GridViewTextBoxColumn();
            HRAPPROVEDDATE_DS.Name = "HRAPPROVEDDATE";
            HRAPPROVEDDATE_DS.FieldName = "HRAPPROVEDDATE";
            HRAPPROVEDDATE_DS.HeaderText = "วันที่อนุมัติ";
            HRAPPROVEDDATE_DS.IsVisible = true;
            HRAPPROVEDDATE_DS.ReadOnly = true;
            HRAPPROVEDDATE_DS.BestFit();
            rgv_ReportDSHR.Columns.Add(HRAPPROVEDDATE_DS);
            #endregion

            #region CHD
            this.rgv_ReportCHDHR.Dock = DockStyle.Fill;
            this.rgv_ReportCHDHR.ReadOnly = true;
            this.rgv_ReportCHDHR.AutoGenerateColumns = true;
            this.rgv_ReportCHDHR.EnableFiltering = false;
            this.rgv_ReportCHDHR.AllowAddNewRow = false;
            this.rgv_ReportCHDHR.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_ReportCHDHR.ShowGroupedColumns = true;
            this.rgv_ReportCHDHR.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_ReportCHDHR.EnableHotTracking = true;
            this.rgv_ReportCHDHR.AutoSizeRows = true;
            this.rgv_ReportCHDHR.Visible = false;

            GridViewTextBoxColumn DOCID_CHD = new GridViewTextBoxColumn();
            DOCID_CHD.Name = "DOCID";
            DOCID_CHD.FieldName = "DOCID";
            DOCID_CHD.HeaderText = "เลขที่เอกสาร";
            DOCID_CHD.IsVisible = true;
            DOCID_CHD.ReadOnly = true;
            DOCID_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(DOCID_CHD);


            GridViewTextBoxColumn EMPLID_CHD = new GridViewTextBoxColumn();
            EMPLID_CHD.Name = "EMPLID";
            EMPLID_CHD.FieldName = "EMPLID";
            EMPLID_CHD.HeaderText = "รหัส";
            EMPLID_CHD.IsVisible = true;
            EMPLID_CHD.ReadOnly = true;
            EMPLID_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(EMPLID_CHD);

            GridViewTextBoxColumn EMPLNAME_CHD = new GridViewTextBoxColumn();
            EMPLNAME_CHD.Name = "EMPLNAME";
            EMPLNAME_CHD.FieldName = "EMPLNAME";
            EMPLNAME_CHD.HeaderText = "ชื่อ";
            EMPLNAME_CHD.IsVisible = true;
            EMPLNAME_CHD.ReadOnly = true;
            EMPLNAME_DS.BestFit();
            rgv_ReportCHDHR.Columns.Add(EMPLNAME_CHD);

            GridViewTextBoxColumn SECTIONNAME_CHD = new GridViewTextBoxColumn();
            SECTIONNAME_CHD.Name = "SECTIONNAME";
            SECTIONNAME_CHD.FieldName = "SECTIONNAME";
            SECTIONNAME_CHD.HeaderText = "แผนก";
            SECTIONNAME_CHD.IsVisible = true;
            SECTIONNAME_CHD.ReadOnly = true;
            SECTIONNAME_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(SECTIONNAME_CHD);

            GridViewTextBoxColumn DEPTNAME_CHD = new GridViewTextBoxColumn();
            DEPTNAME_CHD.Name = "DEPTNAME";
            DEPTNAME_CHD.FieldName = "DEPTNAME";
            DEPTNAME_CHD.HeaderText = "ตำแหน่ง";
            DEPTNAME_CHD.IsVisible = true;
            DEPTNAME_CHD.ReadOnly = true;
            DEPTNAME_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(DEPTNAME_CHD);

            GridViewTextBoxColumn TYPDESC_CHD = new GridViewTextBoxColumn();
            TYPDESC_CHD.Name = "TYPDESC";
            TYPDESC_CHD.FieldName = "TYPDESC";
            TYPDESC_CHD.HeaderText = "เอกสารยกเลิก";
            TYPDESC_CHD.IsVisible = true;
            TYPDESC_CHD.ReadOnly = true;
            TYPDESC_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(TYPDESC_CHD);

            GridViewTextBoxColumn DOCREFER_CHD = new GridViewTextBoxColumn();
            DOCREFER_CHD.Name = "DOCREFER";
            DOCREFER_CHD.FieldName = "DOCREFER";
            DOCREFER_CHD.HeaderText = "เลขที่อ้างอิง";
            DOCREFER_CHD.IsVisible = true;
            DOCREFER_CHD.ReadOnly = true;
            DOCREFER_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(DOCREFER_CHD);

            GridViewTextBoxColumn DATEREFER_CHD = new GridViewTextBoxColumn();
            DATEREFER_CHD.Name = "DATEREFER";
            DATEREFER_CHD.FieldName = "DATEREFER";
            DATEREFER_CHD.HeaderText = "วันที่หยุด";
            DATEREFER_CHD.IsVisible = true;
            DATEREFER_CHD.ReadOnly = true;
            DATEREFER_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(DATEREFER_CHD);

            GridViewTextBoxColumn EMPLSHIFT_CHD = new GridViewTextBoxColumn();
            EMPLSHIFT_CHD.Name = "EMPLSHIFT";
            EMPLSHIFT_CHD.FieldName = "EMPLSHIFT";
            EMPLSHIFT_CHD.HeaderText = "กะเดิมวันที่หยุด";
            EMPLSHIFT_CHD.IsVisible = true;
            EMPLSHIFT_CHD.ReadOnly = true;
            EMPLSHIFT_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(EMPLSHIFT_CHD);

            GridViewTextBoxColumn FROMHOLIDAY_CHD = new GridViewTextBoxColumn();
            FROMHOLIDAY_CHD.Name = "FROMHOLIDAY_CHD";
            FROMHOLIDAY_CHD.FieldName = "FROMHOLIDAY_CHD";
            FROMHOLIDAY_CHD.HeaderText = "วันที่มาทำ";
            FROMHOLIDAY_CHD.IsVisible = true;
            FROMHOLIDAY_CHD.ReadOnly = true;
            FROMHOLIDAY_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(FROMHOLIDAY_CHD);

            GridViewTextBoxColumn REASON_CHD = new GridViewTextBoxColumn();
            REASON_CHD.Name = "REASON";
            REASON_CHD.FieldName = "REASON";
            REASON_CHD.HeaderText = "เหตุผล";
            REASON_CHD.IsVisible = true;
            REASON_CHD.ReadOnly = true;
            REASON_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(REASON_CHD);

            GridViewTextBoxColumn HEADAPPROVED_CHD = new GridViewTextBoxColumn();
            HEADAPPROVED_CHD.Name = "HEADAPPROVED";
            HEADAPPROVED_CHD.FieldName = "HEADAPPROVED";
            HEADAPPROVED_CHD.HeaderText = "หน./ผช.";
            HEADAPPROVED_CHD.IsVisible = true;
            HEADAPPROVED_CHD.ReadOnly = true;
            HEADAPPROVED_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(HEADAPPROVED_CHD);

            GridViewTextBoxColumn HEADAPPROVEDBYNAME_CHD = new GridViewTextBoxColumn();
            HEADAPPROVEDBYNAME_CHD.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME_CHD.FieldName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME_CHD.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME_CHD.IsVisible = true;
            HEADAPPROVEDBYNAME_CHD.ReadOnly = true;
            HEADAPPROVEDBYNAME_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(HEADAPPROVEDBYNAME_CHD);

            GridViewTextBoxColumn HEADAPPROVEDDATE_CHD = new GridViewTextBoxColumn();
            HEADAPPROVEDDATE_CHD.Name = "HEADAPPROVEDDATE";
            HEADAPPROVEDDATE_CHD.FieldName = "HEADAPPROVEDDATE";
            HEADAPPROVEDDATE_CHD.HeaderText = "วันที่อนุมัติ";
            HEADAPPROVEDDATE_CHD.IsVisible = true;
            HEADAPPROVEDDATE_CHD.ReadOnly = true;
            HEADAPPROVEDDATE_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(HEADAPPROVEDDATE_CHD);


            GridViewTextBoxColumn HRAPPROVED_CHD = new GridViewTextBoxColumn();
            HRAPPROVED_CHD.Name = "HRAPPROVED";
            HRAPPROVED_CHD.FieldName = "HRAPPROVED";
            HRAPPROVED_CHD.HeaderText = "บุคคล";
            HRAPPROVED_CHD.IsVisible = true;
            HRAPPROVED_CHD.ReadOnly = true;
            HRAPPROVED_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(HRAPPROVED_CHD);


            GridViewTextBoxColumn HRAPPROVEDBYNAME_CHD = new GridViewTextBoxColumn();
            HRAPPROVEDBYNAME_CHD.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME_CHD.FieldName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME_CHD.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME_CHD.IsVisible = true;
            HRAPPROVEDBYNAME_CHD.ReadOnly = true;
            HRAPPROVEDBYNAME_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(HRAPPROVEDBYNAME_CHD);

            GridViewTextBoxColumn HRAPPORVEREMARK_CHD = new GridViewTextBoxColumn();
            HRAPPORVEREMARK_CHD.Name = "HRAPPORVEREMARK";
            HRAPPORVEREMARK_CHD.FieldName = "HRAPPORVEREMARK";
            HRAPPORVEREMARK_CHD.HeaderText = "หมายเหตุ";
            HRAPPORVEREMARK_CHD.IsVisible = true;
            HRAPPORVEREMARK_CHD.ReadOnly = true;
            HRAPPORVEREMARK_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(HRAPPORVEREMARK_CHD);

            GridViewTextBoxColumn HRAPPROVEDDATE_CHD = new GridViewTextBoxColumn();
            HRAPPROVEDDATE_CHD.Name = "HRAPPROVEDDATE";
            HRAPPROVEDDATE_CHD.FieldName = "HRAPPROVEDDATE";
            HRAPPROVEDDATE_CHD.HeaderText = "วันที่อนุมัติ";
            HRAPPROVEDDATE_CHD.IsVisible = true;
            HRAPPROVEDDATE_CHD.ReadOnly = true;
            HRAPPROVEDDATE_CHD.BestFit();
            rgv_ReportCHDHR.Columns.Add(HRAPPROVEDDATE_CHD);

            #endregion
        }

        #region Print
        void exporter_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {

            string headerText = "รายงานยกเลิกเอกสารออนไลน์ตั้งแต่วันที่:  " + dtpStart.Text + " ถึง " + dtpEnd.Text;
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
        void Btn_ExportToExcel_Click(object sender, EventArgs e)
        {
            if (this.rgv_ReportDLHR.Visible == true)
            {
                if (rgv_ReportDLHR.RowCount != 0)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    ExportToExcelML excelExporter = null;
                    try
                    {
                        ExportToExcelML excelML = new ExportToExcelML(this.rgv_ReportDLHR);
                        excelExporter = new ExportToExcelML(this.rgv_ReportDLHR);
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
                    RadMessageBox.Show("ไม่มีข้อมูล");
                    return;
                }
            }
            else if (this.rgv_ReportDSHR.Visible == true)
            {
                if (rgv_ReportDSHR.RowCount != 0)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    ExportToExcelML excelExporter = null;
                    try
                    {
                        ExportToExcelML excelML = new ExportToExcelML(this.rgv_ReportDSHR);
                        excelExporter = new ExportToExcelML(this.rgv_ReportDSHR);
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
                    RadMessageBox.Show("ไม่มีข้อมูล");
                    return;
                }
            }

            else if (this.rgv_ReportCHDHR.Visible == true)
            {
                if (rgv_ReportCHDHR.RowCount != 0)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    ExportToExcelML excelExporter = null;
                    try
                    {
                        ExportToExcelML excelML = new ExportToExcelML(this.rgv_ReportCHDHR);
                        excelExporter = new ExportToExcelML(this.rgv_ReportCHDHR);
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
                    RadMessageBox.Show("ไม่มีข้อมูล");
                    return;
                }
            }

        }
        #endregion
    }
}
