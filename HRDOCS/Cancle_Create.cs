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
    public partial class Cancle_Create : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);
        string DL_DocId;
        string CHD_DocId;
        string DS_DocId;

        string ApproveID = "";

        string emplcard = "";
        string sectionid = "";
        string deptid = "";
        string positionid = "";
        string position = "";

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


        public Cancle_Create()
        {
            InitializeComponent();
            
            this.CheckApproveID();
            this.GetdataEmpl();
        }

        private void Cancle_Create_Load(object sender, EventArgs e)
        {
            this.txt_DL.Text = "DL";
            this.txt_DLDate.Text = DateTime.Now.ToString("yyMMdd");
            this.txt_CHDDate.Text = "CHD";
            this.txt_CHDDate.Text = DateTime.Now.ToString("yyMMdd");
            this.txt_DS.Text = "DS";
            this.txt_DSDate.Text = DateTime.Now.ToString("yyMMdd");
        }

        #region fucntion

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
        private void GetdataEmpl()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                if (ApproveID == "001" || ApproveID == "002" || ApproveID == "005" || ApproveID == "006")
                {
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
                                            AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
											AND PWEMPLOYEE.PWSECTION IN 
                                                                        ( SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                                          WHERE EMPLID = '{0}' ) ", ClassCurUser.LogInEmplId);

                    //AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')  

                    SqlCommand cmd = new SqlCommand(sqlemp, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtEmpId.Text = reader["PWEMPLOYEE"].ToString();
                            emplcard = reader["PWCARD"].ToString();
                            lblEmplName.Text = reader["FULLNAME"].ToString().Trim();

                            deptid = reader["DEPTID"].ToString().Trim();
                            lblDept.Text = reader["DEPT"].ToString().Trim();

                            sectionid = reader["SECTIONID"].ToString();
                            lblSection.Text = reader["SECTION"].ToString();

                            positionid = reader["POSITIONID"].ToString();
                            position = reader["POSITION"].ToString();

                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูล โปรดตรวจสอบสิทธิ์ในการจัดการกับเอกสารของพนักงานอีกครั้ง");
                    }
                }

                //พนักงาน
                if (ApproveID == "003" || ApproveID == "007")
                {
                    this.txtEmpId.ReadOnly = true;

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
											AND PWEMPLOYEE.PWSECTION IN 
                                                                        ( SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                                          WHERE EMPLID = '{0}' ) ", ClassCurUser.LogInEmplId);

                    //AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')  

                    SqlCommand cmd = new SqlCommand(sqlemp, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtEmpId.Text = reader["PWEMPLOYEE"].ToString();
                            emplcard = reader["PWCARD"].ToString();
                            lblEmplName.Text = reader["FULLNAME"].ToString().Trim();

                            deptid = reader["DEPTID"].ToString().Trim();
                            lblDept.Text = reader["DEPT"].ToString().Trim();

                            sectionid = reader["SECTIONID"].ToString();
                            lblSection.Text = reader["SECTION"].ToString();

                            positionid = reader["POSITIONID"].ToString();
                            position = reader["POSITION"].ToString();

                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูล โปรดตรวจสอบรหัสพนักงานอีกครั้ง");
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

        void GetDSData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                string sqlemp =
                    string.Format(
                         @" SELECT DISTINCT HD.DSDOCNO 
                            FROM SPC_CM_SHIFTHD HD 
	                            LEFT OUTER JOIN SPC_CM_SHIFTDT DT ON HD.DSDOCNO = DT.DSDOCNO
                            WHERE HD.HEADAPPROVED = '1'
                            AND  HD.DSDOCNO  = '{0}'  +  '{1}' + '-' + '{2}' 
                            AND (HD.EMPLID  = '{3}' OR HD.CREATEDBY = '{3}')
                            AND DT.SHIFTDATE NOT IN 
	                                    ( 
		                                   SELECT DATEREFER FROM [SPC_JN_CANCLEDOCDT] 
                                           WHERE DOCREFER = '{0}'  +  '{1}' + '-' + '{2}'
	                                    ) "

                    , txt_DS.Text.Trim()
                    , txt_DSDate.Text.Trim()
                    , txt_DSId.Text.Trim()
                    , txtEmpId.Text.Trim()
                    );
                SqlCommand cmd = new SqlCommand(sqlemp, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DS_DocId = reader["DSDOCNO"].ToString();
                        this.txt_DSId.Text = "";
                    }

                    using (Cancle_DetailDS frm = new Cancle_DetailDS(DS_DocId))
                    {
                        frm.Text = "แสดงข้อมูลเอกสารใบเปลียนวันหยุดออนไลน์";
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("เลขที่เอกสารไม่ถูกต้อง โปรดตรวจสอบใหม่อีกครั้ง");
                    this.txt_DSId.Clear();
                    this.txt_DSId.Focus();
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
        void GetDLData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                string sqlemp =
                     string.Format(@"
                                    SELECT DISTINCT HD.DLDOCNO
                                    FROM SPC_CM_LEAVEHD HD 
		                                    LEFT OUTER JOIN  SPC_CM_LEAVEDT DT ON  HD.DLDOCNO =  DT.DLDOCNO
                                    WHERE HD.HEADAPPROVED = '1'
                                    AND HD.DLDOCNO  = '{0}'  +  '{1}' + '-' + '{2}'
                                    AND (HD.EMPLID  = '{3}' OR HD.CREATEDBY = '{3}')
                                    AND DT.LEAVEDATE  NOT IN 
	                                      ( 
					                        SELECT DATEREFER FROM [SPC_JN_CANCLEDOCDT] 
                                            WHERE DOCREFER = '{0}'  +  '{1}' + '-' + '{2}'
	                                      ) "

                    , txt_DL.Text.Trim()
                    , txt_DLDate.Text.Trim()
                    , txt_DLId.Text.Trim()
                    , txtEmpId.Text.Trim()
                    );
                SqlCommand cmd = new SqlCommand(sqlemp, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DL_DocId = reader["DLDOCNO"].ToString();
                        this.txt_DLId.Text = "";
                    }

                    using (Cancle_DetailDL frm = new Cancle_DetailDL(DL_DocId))
                    {
                        frm.Text = "แสดงข้อมูลเอกสารใบลาหยุดออนไลน์";
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("เลขที่เอกสารไม่ถูกต้อง โปรดตรวจสอบใหม่อีกครั้ง");
                    this.txt_DLId.Clear();
                    this.txt_DLId.Focus();
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
        void GetCHDData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {

                string sqlemp =
                    string.Format(@"SELECT DISTINCT HD.DOCID
                                    FROM SPC_JN_CHANGHOLIDAYHD HD 
	                                    LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYDT DT ON HD.DOCID = DT.DOCID
                                    WHERE HEADAPPROVED = '1'
                                    AND HD.DOCID  = '{0}'  +  '{1}' + '-' + '{2}'
                                    AND (HD.EMPLID  = '{3}' OR HD.CREATEDBY = '{3}')
                                    AND DT.TOHOLIDAY NOT IN 
	                                    ( 
		                                    SELECT DATEREFER FROM [SPC_JN_CANCLEDOCDT] 
                                            WHERE DOCREFER = '{0}'  +  '{1}' + '-' + '{2}'
	                                    ) "

                    , txt_CHD.Text.Trim()
                    , txt_CHDDate.Text.Trim()
                    , txt_CHDId.Text.Trim()
                    , txtEmpId.Text.Trim()
                    );
                SqlCommand cmd = new SqlCommand(sqlemp, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CHD_DocId = reader["DOCID"].ToString();
                        this.txt_CHDId.Text = "";
                    }

                    using (Cancle_DetailCHD frm = new Cancle_DetailCHD(CHD_DocId))
                    {
                        frm.Text = "แสดงข้อมูลเอกสารใบเปลียนวันหยุดออนไลน์";
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show(" โปรดตรวจสอบใหม่อีกครั้ง เลขที่เอกสารนี้ไม่ถูกต้องหรืออาจจะถูกยกเลิกไปแล้ว ");
                    this.txt_CHDId.Clear();
                    this.txt_CHDId.Focus();
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

        private void HrCreatEmpl()
        {

            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                if (ApproveID == "001" || ApproveID == "002" || ApproveID == "005" || ApproveID == "006")
                {
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
                                            AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
											AND PWEMPLOYEE.PWSECTION IN 
                                                                        ( SELECT PWSECTION FROM SPC_CM_AUTHORIZE
                                                                          WHERE EMPLID = '{1}' ) "
                                                                        , txtEmpId.Text.Trim()
                                                                        , ClassCurUser.LogInEmplId);

                    //AND (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')  

                    SqlCommand cmd = new SqlCommand(sqlemp, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtEmpId.Text = reader["PWEMPLOYEE"].ToString();
                            emplcard = reader["PWCARD"].ToString();
                            lblEmplName.Text = reader["FULLNAME"].ToString().Trim();
                            
                            deptid = reader["DEPTID"].ToString().Trim();
                            lblDept.Text = reader["DEPT"].ToString().Trim();

                            sectionid = reader["SECTIONID"].ToString();
                            lblSection.Text = reader["SECTION"].ToString();

                            positionid = reader["POSITIONID"].ToString();
                            position = reader["POSITION"].ToString();

                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูล โปรดตรวจสอบสิทธิ์ในการจัดการกับเอกสารของพนักงานอีกครั้ง");
                        this.txtEmpId.Focus();
                        this.lblEmplName.Text = "-";
                        this.lblDept.Text = "-";
                        this.lblSection.Text = "-";
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

        private void txtEmpId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                HrCreatEmpl();
            }
        }

        private void txt_DSId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (rdb_DS.ToggleState == ToggleState.On)
                {
                    GetDSData();
                }
            }
        }

        private void txt_DLId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (rdb_DL.ToggleState == ToggleState.On)
                {
                    GetDLData();
                }
            }
        }

        private void txt_CHDId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (rdb_CHD.ToggleState == ToggleState.On)
                {
                    GetCHDData();
                }
            }
        }
    }
}
