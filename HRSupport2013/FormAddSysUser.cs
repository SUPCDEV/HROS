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
    public partial class FormAddSysUser : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);
        
        string sectionid = "";
        string positionid = "";
        string deptid = "";
        string division = "";

        string headappove = "";
        string mnapproveout = "";
        string mnapprovein = "";
        string hrapproveout = "";
        string hrapprovein = "";
        
        string admin = "";

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
        protected string sysadmin;
        public string SysAdmin
        {
            get { return sysadmin; }
            set { sysadmin = value; }
        }

        public FormAddSysUser()
        {
            InitializeComponent();
            // <WS>
            this.KeyPreview = true;
            // </WS>
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.txtEmplId.KeyDown += new KeyEventHandler(txtEmplId_KeyDown);
            //this.txtEmplId.KeyPress += new KeyPressEventHandler(txtEmplId_KeyPress);
        }

        

        private void FormUpdateSysUser_Load(object sender, EventArgs e)
        {
            this.admin = SysAdmin;

            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;
            
            this.radCheckHdApprove.Enabled = false;
            this.radCheckBoxHrApproveOut.Enabled = false;
            this.radCheckBoxHrApproveIn.Enabled = false;
            this.radCheckMNApprove.Enabled = false;
            this.radCheckAdmin.Enabled = false;
        }

        private void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                string sql =
                 string.Format(@"SELECT	RTRIM(PWEMPLOYEE.PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWEMPLOYEE.PWCARD) AS PWCARD
		                                ,(RTRIM(PWEMPLOYEE.PWFNAME) +' '+ RTRIM(PWEMPLOYEE.PWLNAME)) AS EmplFullName
		                                ,RTRIM(PWSECTION.PWSECTION)AS SECTIONID ,RTRIM(PWSECTION.PWDESC) AS SECTION
                                        ,RTRIM(PWDEPT.PWDEPT)AS DEPTID ,RTRIM(PWDEPT.PWDESC) AS DEPT
		                                ,RTRIM(PWEMPLOYEE.PWPOSITION) AS POSITIONID ,RTRIM(PWPOSITION.PWDESC) AS POSITION
                                        ,RTRIM(PWEMPLOYEE.PWDIVISION) AS DIVISIONID
                                        ,HROS_TSYSUSER.[SYS_OUTOFFICE] ,HROS_TSYSUSER.[SYS_HRAPPROVEOUT] ,HROS_TSYSUSER.[SYS_HRAPPROVEIN] 
										,HROS_TSYSUSER.[SYS_MNAPPROVEOUT] ,HROS_TSYSUSER.[SYS_MNAPPROVEIN] ,HROS_TSYSUSER.[SYS_ADMIN]
                                 FROM	PWEMPLOYEE WITH (NOLOCK)
	                                   INNER JOIN PWSECTION  ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION collate Thai_CS_AS
	                                   INNER JOIN PWDEPT  ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT collate Thai_CS_AS
                                       INNER JOIN PWPOSITION  ON PWEMPLOYEE.PWPOSITION = PWPOSITION.PWPOSITION collate Thai_CS_AS
	                                   INNER JOIN HROS_TSYSUSER  ON PWEMPLOYEE.PWCARD = HROS_TSYSUSER.PWCARD collate Thai_CS_AS
                                WHERE  HROS_TSYSUSER.PWEMPLOYEE = '" + txtEmplId.Text.Trim() + "' OR HROS_TSYSUSER.PWCARD = '" + txtEmplId.Text.Trim() + "' AND PWSTATWORK IN ('A','V')  ");

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lblEmplId.Text = reader["PWEMPLOYEE"].ToString();
                        lblFullName.Text = reader["EmplFullName"].ToString();
                        lblSection.Text = reader["SECTION"].ToString();
                        lblDept.Text = reader["POSITION"].ToString();
                        sectionid = reader["SECTIONID"].ToString();
                        positionid = reader["POSITIONID"].ToString();
                        deptid = reader["DEPTID"].ToString();
                        division = reader["DIVISIONID"].ToString();
                        headappove = reader["SYS_OUTOFFICE"].ToString();
                        hrapproveout = reader["SYS_HRAPPROVEOUT"].ToString();
                        hrapprovein = reader["SYS_HRAPPROVEIN"].ToString();
                        mnapproveout = reader["SYS_MNAPPROVEOUT"].ToString();
                        mnapprovein = reader["SYS_MNAPPROVEIN"].ToString();
                        admin = reader["SYS_ADMIN"].ToString();

                        this.radCheckHdApprove.Enabled = true;
                        this.radCheckBoxHrApproveOut.Enabled = true;
                        this.radCheckBoxHrApproveIn.Enabled = true;
                        this.radCheckMNApprove.Enabled = true; 
                        this.radCheckAdmin.Enabled = true;
                        this.radCheckAdmin.Enabled = true;

                        #region แสดงสิทธิร์การใช้งานที่มีอยู่
                        // headappove
                        if (headappove == "1")
                        {
                            radCheckHdApprove.Checked = true;
                        }
                        else
                        {
                            radCheckHdApprove.Checked = false;
                        }
                        // hrapproveout
                        if (hrapproveout == "1")
                        {
                            radCheckBoxHrApproveOut.Checked = true;
                        }
                        else
                        {
                            radCheckBoxHrApproveOut.Checked = false;
                        }
                        //hrapprovein
                        if (hrapprovein == "1")
                        {
                            radCheckBoxHrApproveIn.Checked = true;
                        }
                        else
                        {
                            radCheckBoxHrApproveIn.Checked = false;
                        }
                        // mnapproveout
                        if (mnapproveout == "1" && mnapprovein == "1")
                        {
                            radCheckMNApprove.Checked = true;
                        }
                        else
                        {
                            radCheckMNApprove.Checked = false;
                        }
                        if (admin == "1")
                        {
                            radCheckAdmin.Checked = true;
                        }
                        else
                        {
                            radCheckAdmin.Checked = false;
                        }
                        #endregion

                        #region oldcode
                        //if (sectionid == "02")
                        //{
                        //    this.radCheckHdApprove.Enabled = true;
                        //    this.radCheckBoxHrApproveOut.Enabled = true;
                        //    this.radCheckBoxHrApproveIn.Enabled = true;
                        //    this.radCheckMNApprove.Enabled = true;
                        //}
                        //else if (division == "75" || division == "11"
                        //             || sectionid == "136" || sectionid == "137" || sectionid == "138"
                        //             || sectionid == "139" || sectionid == "140" || sectionid == "09" )
                        //{
                        //    this.radCheckMNApprove.Enabled = true;
                        //}
                        //else if (sectionid != "02")
                        //{
                        //    if (division != "75" || division != "11"
                        //             || sectionid != "136" || sectionid != "137" || sectionid != "138"
                        //             || sectionid != "139" || sectionid != "140" || sectionid != "09")
                        //    {
                        //        this.radCheckHdApprove.Enabled = true;
                        //        this.radCheckBoxHrApproveOut.Enabled = false;
                        //        this.radCheckBoxHrApproveIn.Enabled = false;
                        //        this.radCheckMNApprove.Enabled = false;

                        //    }
                        //    else
                        //    {
                        //        this.radCheckBoxHrApproveOut.Enabled = false;
                        //        this.radCheckBoxHrApproveIn.Enabled = false;
                        //    }
                        //}

                        //else
                        //{
                        //    this.radCheckHdApprove.Enabled = true;
                        //}
                        //break;

                        #endregion
  
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบพนักงาน");
                    this.txtEmplId.Text = "";
                    this.txtEmplId.Focus();
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

        private void txtEmplId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Return)) { this.GetData(); }
        }
        void txtEmplId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                this.GetData();
            }
        }

        private void AddSysUser()
        {
            if (radCheckHdApprove.Checked == true)
            {
                headappove = "1";
            }
            else
            {
                headappove = "0";
            }

            if (radCheckMNApprove.Checked == true)
            {
                mnapproveout = "1";
                mnapprovein = "1";
            }
            else
            {
                mnapproveout = "0";
                mnapprovein = "0";
            }


            if (radCheckBoxHrApproveOut.Checked == true)
            {
                hrapproveout = "1";
            }
            else
            {
                hrapproveout = "0";
            }
            if (radCheckBoxHrApproveIn.Checked == true)
            {
                hrapprovein = "1";
            }
            else
            {
                hrapprovein = "0";
            }
            if (radCheckAdmin.Checked == true)
            {
                admin = "1";
            }
            else
            {
                admin = "0";
            }

            #region บันทึกสิทธิ์

            //if (sectionid == "02")
            //{
                if (MessageBox.Show("คุณต้องการบันทึกสิทธิ์การใช้งานใบออกนอกออนไลน์ ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                        con.Open();
                        {
                            string slq =
                                    @" UPDATE [HROS_TSYSUSER] SET [SYS_OUTOFFICE] = @outoffice 
                                                                  ,[SYS_MNAPPROVEOUT] = @mnapproveout 
                                                                  ,[SYS_MNAPPROVEIN] = @mnapprovein                                                                 
                                                                  ,[SYS_HRAPPROVEOUT] = @hrapproveout
                                                                  ,[SYS_HRAPPROVEIN] = @hrapprovein
                                                                  ,[SYS_ADMIN] = @admin
                                                                  ,[ADDSYSNAME] = @addsysyname
                                                                  ,[ADDSYSDATE] =  (dateadd(millisecond, -datepart(millisecond,getdate()),getdate()))

                                       WHERE ([PWEMPLOYEE] = @employee )";

                            SqlCommand cmd = new SqlCommand(slq, con);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@employee", lblEmplId.Text);
                            cmd.Parameters.AddWithValue("@outoffice", headappove);
                            cmd.Parameters.AddWithValue("@mnapproveout", mnapproveout);
                            cmd.Parameters.AddWithValue("@mnapprovein", mnapprovein);
                            cmd.Parameters.AddWithValue("@hrapproveout", hrapproveout);
                            cmd.Parameters.AddWithValue("@hrapprovein", hrapprovein);
                            cmd.Parameters.AddWithValue("@addsysyname", ClassCurUser.LogInEmplName);
                            cmd.Parameters.AddWithValue("@admin", admin);
                            cmd.ExecuteNonQuery();

                            sectionid = "";
                            positionid = "";
                            deptid = "";

                            txtEmplId.Text = "";
                            lblEmplId.Text = "";
                            lblFullName.Text = "";
                            lblSection.Text = "";
                            lblDept.Text = "";

                            radCheckHdApprove.Checked = false;
                            radCheckBoxHrApproveOut.Checked = false;
                            radCheckBoxHrApproveIn.Checked = false;
                            radCheckMNApprove.Checked = false;
                            radCheckAdmin.Checked = false;

                            headappove = "";
                            mnapprovein = "";
                            mnapproveout = "";
                            hrapproveout = "";
                            hrapprovein = "";
                            admin = "";
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
            //}
            else
            {
                MessageBox.Show(" คุณ " + ClassCurUser.LogInEmplName + " ไม่มีสิทธ์อนุมัติเอกสารใบออกนอกในส่วนของแผนกบุคคล");
                //radCheckHdApprove.Checked = false;
                //radCheckBoxHrApproveOut.Checked = false;
                //radCheckBoxHrApproveIn.Checked = false;
                //radCheckMNApprove.Checked = false;
            }
            #endregion

            #region oldcode

            //            if (division == "75" || division == "11"
//                       || sectionid == "136" || sectionid == "137" || sectionid == "138"
//                       || sectionid == "139" || sectionid == "140" || sectionid == "09")
//            {
//                if (MessageBox.Show("คุณต้องการบันทึกสิทธิ์การใช้งานใบออกนอกออนไลน์ ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//                {
//                    try
//                    {
//                        if (con.State == ConnectionState.Open) con.Close();
//                        con.Open();
//                        {
//                            string slq =
//                                    @" UPDATE [HROS_TSYSUSER] SET [SYS_OUTOFFICE] = @outoffice 
//                                                                  ,[SYS_MNAPPROVEOUT] = @mnapproveout 
//                                                                  ,[SYS_MNAPPROVEIN] = @mnapprovein                                                                 
//                                                                  ,[SYS_HRAPPROVEOUT] = @hrapproveout
//                                                                  ,[SYS_HRAPPROVEIN] = @hrapprovein
//                                                                  ,[ADDSYSNAME] = @addsysyname
//                                                                  ,[ADDSYSDATE] =  (dateadd(millisecond, -datepart(millisecond,getdate()),getdate()))
//
//                                       WHERE ([PWEMPLOYEE] = @employee )";

//                            SqlCommand cmd = new SqlCommand(slq, con);
//                            cmd.CommandType = CommandType.Text;
//                            cmd.Parameters.AddWithValue("@employee", lblEmplId.Text);
//                            cmd.Parameters.AddWithValue("@outoffice", headappove);
//                            cmd.Parameters.AddWithValue("@mnapproveout", mnapproveout);
//                            cmd.Parameters.AddWithValue("@mnapprovein", mnapprovein);
//                            cmd.Parameters.AddWithValue("@hrapproveout", hrapproveout);
//                            cmd.Parameters.AddWithValue("@hrapprovein", hrapprovein);
//                            cmd.Parameters.AddWithValue("@addsysyname", ClassCurUser.LogInEmplName);



//                            cmd.ExecuteNonQuery();

//                            sectionid = "";
//                            positionid = "";
//                            deptid = "";

//                            txtEmplId.Text = "";
//                            lblEmplId.Text = "";
//                            lblFullName.Text = "";
//                            lblSection.Text = "";
//                            lblDept.Text = "";

//                            radCheckHdApprove.Checked = false;
//                            radCheckBoxHrApproveOut.Checked = false;
//                            radCheckBoxHrApproveIn.Checked = false;
//                            radCheckMNApprove.Checked = false;

//                            headappove = "";
//                            mnapprovein = "";
//                            mnapproveout = "";
//                            hrapproveout = "";
//                            hrapprovein = "";
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        MessageBox.Show(ex.ToString());
//                    }
//                    finally
//                    {
//                        if (con.State == ConnectionState.Open) con.Close();
//                    }
//                }
//            }
//            else
//            {
//                MessageBox.Show(" คุณ " + ClassCurUser.LogInEmplName + " ไม่มีสิทธ์อนุมัติเอกสารใบออกนอกในส่วนของมินิมาร์ท / โรงไม้นาคา ");
//                radCheckHdApprove.Checked = false;
//                radCheckBoxHrApproveOut.Checked = false;
//                radCheckBoxHrApproveIn.Checked = false;
//                //radCheckMNApprove.Checked = false;
//            }
            
            #endregion

        }

        private void OldSysUser()
        {

        }
        void btnSave_Click(object sender, EventArgs e)
        {
            this.AddSysUser();
        }
    }
}
