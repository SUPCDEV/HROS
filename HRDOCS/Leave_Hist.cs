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
    public partial class Leave_Hist : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);
        string ApproveID = "";

        string emplcard = "";
        string sectionid = "";
        string deptid = "";
        string positionid = "";
        string position = "";

        string _empid;
        public Leave_Hist(string Empid)
        {
            _empid = Empid;
            InitializeComponent();

            CheckApproveID();
            Load_Section();

            this.dt_FromSec.Text = DateTime.Now.ToString("yyyy-01-01");
            this.dt_ToSec.Text = DateTime.Now.ToString("yyyy-MM-dd");
            btn_SearchData.Click += new EventHandler(btn_SearchData_Click);

            #region radGridView
            // GridData 
            this.radGridView1.Dock = DockStyle.Fill;
            this.radGridView1.ReadOnly = true;
            this.radGridView1.AutoGenerateColumns = true;
            this.radGridView1.EnableFiltering = false;
            this.radGridView1.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AutoGenerateColumns = false;
            this.radGridView1.ShowGroupedColumns = true;
            this.radGridView1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.radGridView1.EnableHotTracking = true;
            this.radGridView1.AutoSizeRows = true;

            GridViewTextBoxColumn YR = new GridViewTextBoxColumn();
            YR.Name = "YR";
            YR.FieldName = "YR";
            YR.HeaderText = "ปี";
            YR.IsVisible = true;
            YR.ReadOnly = true;
            YR.BestFit();
            radGridView1.Columns.Add(YR);

            GridViewTextBoxColumn FULLNAME = new GridViewTextBoxColumn();
            FULLNAME.Name = "FULLNAME";
            FULLNAME.FieldName = "FULLNAME";
            FULLNAME.HeaderText = "ชื่อ - สกุล";
            FULLNAME.IsVisible = true;
            FULLNAME.ReadOnly = true;

            FULLNAME.BestFit();
            radGridView1.Columns.Add(FULLNAME);

            GridViewTextBoxColumn EVENTNANE = new GridViewTextBoxColumn();
            EVENTNANE.Name = "EVENTNANE";
            EVENTNANE.FieldName = "EVENTNANE";
            EVENTNANE.HeaderText = "ประเภท";
            EVENTNANE.IsVisible = true;
            EVENTNANE.ReadOnly = true;
            EVENTNANE.BestFit();
            radGridView1.Columns.Add(EVENTNANE);

            GridViewTextBoxColumn USESUMMARY = new GridViewTextBoxColumn();
            USESUMMARY.Name = "USESUMMARY";
            USESUMMARY.FieldName = "USESUMMARY";
            USESUMMARY.HeaderText = "ใช้ไป";
            USESUMMARY.IsVisible = true;
            USESUMMARY.ReadOnly = true;
            USESUMMARY.BestFit();
            radGridView1.Columns.Add(USESUMMARY);

            GridViewTextBoxColumn TOTALL = new GridViewTextBoxColumn();
            TOTALL.Name = "TOTALL";
            TOTALL.FieldName = "TOTALL";
            TOTALL.HeaderText = "คงเหลือ";
            TOTALL.IsVisible = true;
            TOTALL.ReadOnly = true;
            TOTALL.BestFit();
            radGridView1.Columns.Add(TOTALL);

            #endregion

        }
        private void Leave_Hist_Load(object sender, EventArgs e)
        {
            Txt_EmplID.Text = _empid;
        }
        void btn_SearchData_Click(object sender, EventArgs e)
        {
            SearchData();
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
												                                                            WHERE EMPLID = '{0}' 
                                                                                                            )
                                                            OR PWSECTION = '{1}'
                                                            ORDER BY PWDESC ", ClassCurUser.LogInEmplId, ClassCurUser.LogInSection);

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
                        MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        void CheckApproveID()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                Cursor.Current = Cursors.WaitCursor;

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@" SELECT * FROM SPC_CM_AUTHORIZE
                                                              WHERE EMPLID = '{0}' 
                                                              AND APPROVEID in ('001','002','005','006')
                                                              ORDER BY APPROVEID ", ClassCurUser.LogInEmplId);
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
                else
                {
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
                                            AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
											 ", ClassCurUser.LogInEmplId);

                    //AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')  

                    SqlCommand cmd = new SqlCommand(sqlemp, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Txt_EmplID.Text = reader["PWEMPLOYEE"].ToString();
                            emplcard = reader["PWCARD"].ToString();
                            // txtEmplName.Text = reader["FULLNAME"].ToString().Trim();

                            deptid = reader["DEPTID"].ToString().Trim();
                            // txtDept.Text = reader["DEPT"].ToString().Trim();

                            sectionid = reader["SECTIONID"].ToString();
                            // txtSection.Text = reader["SECTION"].ToString();

                            positionid = reader["POSITIONID"].ToString();
                            position = reader["POSITION"].ToString();
                        }
                    }
                #endregion

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                if (con.State == ConnectionState.Open) con.Close();
            }

        }
        void SearchData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;



                //Cursor.Current = Cursors.WaitCursor;
                //radGridView1.Rows.Clear();

                string _leavetyp;
                int _leavercount;

                #region SQL
                if (rdb_LeaveTypE.IsChecked == true)
                {
                    _leavetyp = "E";
                    _leavercount = 20;
                }
                else if (rdb_LeaveTypF.IsChecked == true)
                {
                    _leavetyp = "F";
                    _leavercount = 6;
                }
                else
                {
                    return;
                }

                if (Rb_Section.IsChecked == true)
                {

                    sqlCommand.CommandText = string.Format(
                        @"WITH CTE AS 
	                            (
		                            SELECT	
			                            YEAR(PWDATEADJ) AS [YR]	
			                            , PWEVENT, PWEMPLOYEE, PWUPDTYPE
			                            , PWDATEADJ, CASE WHEN PWUPDTYPE LIKE N'=' THEN 1.0 ELSE 0.5 END AS DAYUSED
		                            FROM	PWADJTIME WITH(NOLOCK)
		                            WHERE	CAST(PWDATEADJ AS DATETIME) BETWEEN '{0}' AND '{1}'
		                            AND		PWEVENT = COALESCE(NULLIF('{2}', ''), PWEVENT) 
		                            AND		EXISTS
		                            (
			                            SELECT 'X' FROM	PWSTOPWORK1 WITH(NOLOCK)
			                            WHERE	PWADJTIME.PWADJTIME		= PWSTOPWORK1.PWSTOPWORK
			                            AND		PWADJTIME.PWEMPLOYEE	= PWSTOPWORK1.PWEMPLOYEE
			                            AND		PWSTOPWORK1.PWSTATUS  = 1
		                            )
	                            )
	                            SELECT	CTE.YR, CTE.PWEVENT, RTRIM(PWEVENT.PWDESC) AS EVENTNANE, CTE.PWEMPLOYEE
								, SUM(DAYUSED)  AS USESUMMARY, ({3} - SUM(DAYUSED)) AS TOTALL
	                            ,(PWEMPLOYEE.PWFNAME + PWEMPLOYEE.PWLNAME) AS FULLNAME
	                            FROM	CTE WITH(NOLOCK)
		                            LEFT JOIN PWEVENT WITH(NOLOCK)
			                            ON CTE.PWEVENT = PWEVENT.PWEVENT
		                            INNER JOIN PWEMPLOYEE WITH(NOLOCK)
			                            ON PWEMPLOYEE.PWEMPLOYEE = CTE.PWEMPLOYEE
	                                                        AND PWEMPLOYEE.PWSECTION IN(
									                                                        SELECT PWSECTION FROM SPC_CM_AUTHORIZE
									                                                        WHERE EMPLID = '{4}'
								                                                        )
								                            AND PWEMPLOYEE.PWSECTION = '{5}'
								                            AND PWEMPLOYEE.PWSTATWORK IN ('A','V')
	                            GROUP BY YR, CTE.PWEVENT ,PWEVENT.PWDESC ,CTE.PWEMPLOYEE
	                            ,PWEMPLOYEE.PWFNAME ,PWEMPLOYEE.PWLNAME
	                            ORDER BY CTE.PWEMPLOYEE ,YR  "
                        , dt_FromSec.Value.Date.ToString("yyyy-MM-dd")
                        , dt_ToSec.Value.Date.ToString("yyyy-MM-dd")
                        , _leavetyp.ToString()
                        , _leavercount
                        , ClassCurUser.LogInEmplId
                        , Cbb_Section.SelectedValue.ToString());

                }
                else if (Rb_Empl.IsChecked == true)
                {
                    sqlCommand.CommandText = string.Format(
                            @"WITH CTE AS 
	                            (
		                            SELECT	
			                            YEAR(PWDATEADJ) AS [YR]	
			                            , PWEVENT, PWEMPLOYEE, PWUPDTYPE
			                            , PWDATEADJ, CASE WHEN PWUPDTYPE LIKE N'=' THEN 1.0 ELSE 0.5 END AS DAYUSED
		                            FROM	PWADJTIME WITH(NOLOCK)
		                            WHERE	CAST(PWDATEADJ AS DATETIME) BETWEEN '{0}' AND '{1}'
									AND		PWEMPLOYEE = COALESCE(NULLIF('{2}', ''), PWEMPLOYEE)
		                            AND		PWEVENT = COALESCE(NULLIF('{3}', ''), PWEVENT) 
		                            AND		EXISTS
		                            (
			                            SELECT 'X' FROM	PWSTOPWORK1 WITH(NOLOCK)
			                            WHERE	PWADJTIME.PWADJTIME		= PWSTOPWORK1.PWSTOPWORK
			                            AND		PWADJTIME.PWEMPLOYEE	= PWSTOPWORK1.PWEMPLOYEE
			                            AND		PWSTOPWORK1.PWSTATUS  = 1
		                            )
	                            )
	                            SELECT	CTE.YR, CTE.PWEVENT, RTRIM(PWEVENT.PWDESC) AS EVENTNANE, CTE.PWEMPLOYEE
								, SUM(DAYUSED)  AS USESUMMARY, ({4} - SUM(DAYUSED)) AS TOTALL
	                            ,(PWEMPLOYEE.PWFNAME + PWEMPLOYEE.PWLNAME) AS FULLNAME
	                            FROM	CTE WITH(NOLOCK)
		                            LEFT JOIN PWEVENT WITH(NOLOCK)
			                            ON CTE.PWEVENT = PWEVENT.PWEVENT
		                            INNER JOIN PWEMPLOYEE WITH(NOLOCK)
			                            ON PWEMPLOYEE.PWEMPLOYEE = CTE.PWEMPLOYEE
	                                                        AND PWEMPLOYEE.PWSECTION IN(
									                                                        SELECT PWSECTION FROM SPC_CM_AUTHORIZE
									                                                        WHERE EMPLID = '{5}'
								                                                        )
								                            AND PWEMPLOYEE.PWSTATWORK IN ('A','V')
	                            GROUP BY YR, CTE.PWEVENT ,PWEVENT.PWDESC ,CTE.PWEMPLOYEE
	                            ,PWEMPLOYEE.PWFNAME ,PWEMPLOYEE.PWLNAME
	                            ORDER BY CTE.PWEMPLOYEE ,YR "
                            , dt_FromSec.Value.Date.ToString("yyyy-MM-dd")
                            , dt_ToSec.Value.Date.ToString("yyyy-MM-dd")
                            , Txt_EmplID.Text.ToString()
                            , _leavetyp.ToString()
                            , _leavercount
                            , ClassCurUser.LogInEmplId
                            );

                }

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    radGridView1.DataSource = dt;
                }
                else
                {
                    radGridView1.DataSource = dt;
                    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    }
}
