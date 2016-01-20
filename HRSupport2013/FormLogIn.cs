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
    public partial class FormLogIn : Form
    {
        // Declaration
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);
        

        #region Declare
        string tmpPWEMPLOYEE = "";

        string tmpSECUREMPLID = "";
        string tmpSECUREMPL = "";

        DataTable emplTable = new DataTable("EmplTable");
        DataTable tsysUser = new DataTable("TsysUser");
        DataTable dtcondition = new DataTable("ConditionTable");

        #endregion

        public FormLogIn()
        {
            InitializeComponent();

            #region <Cap InputLanguage>
            this.InputLanguageChanging += new InputLanguageChangingEventHandler(FormLogIn_InputLanguageChanging);
            this.statusLabelLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName.ToUpper();
            #endregion


            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;
           //this.Load += new EventHandler(FormLogIn_Load);  drop 14-11-58
            //this.KeyPress += new KeyPressEventHandler(FormLogIn_KeyPress);
            //this.KeyDown += new KeyEventHandler(FormLogIn_KeyDown);

            this.btnLogin.Click += new EventHandler(btnLogin_Click);            
            this.txtUser.KeyDown += new KeyEventHandler(txtUser_KeyDown);            
            this.txtPass.KeyDown += new KeyEventHandler(txtPass_KeyDown);

            this.groupBox1.Dock = DockStyle.Top;
            this.radPanelDesc.Dock = DockStyle.Fill;
            this.toolStripStatusLabel1.Text = string.Empty;
        }

        void FormLogIn_InputLanguageChanging(object sender, InputLanguageChangingEventArgs e)
        {
            this.statusLabelLang.Text = e.Culture.TwoLetterISOLanguageName.ToUpper();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //MessageBox.Show(keyData.ToString());
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            } 
            return base.ProcessCmdKey(ref msg, keyData);
        }
        void FormLogIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            //this.SetShowLang();
        }
        void FormLogIn_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyData.ToString());
        }              
        // Events
        private void FormLogIn_Load(object sender, EventArgs e)
        {
            this.Update_empl();
            //this.SetShowLang();
            this.txtUser.Focus();
            
        }
        //private void SetShowLang()
        //{
        //    string Langu = InputLanguage.CurrentInputLanguage.Culture.CompareInfo.Name;
        //    statusLabelLang.Text = string.Format(@"lang: {0}", Langu);
        //}
        void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            //this.SetShowLang();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (string.IsNullOrEmpty(txtUser.Text.Trim()))
                {
                    MessageBox.Show("ชื่อผู้ใช้ต้องไม่เป็นค่าว่าง");
                    //this.SetClearControls();
                    this.txtUser.Focus();
                    return;
                }

                this.txtPass.Focus();

                if (string.IsNullOrEmpty(this.txtPass.Text.Trim()))
                {
                    //MessageBox.Show("รหัสผ่านต้องไม่เป็นค่าว่าง");
                    //this.SetClearControls();
                    txtPass.Focus();
                    return;
                }                
            }
        }
        void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            //this.SetShowLang();
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin.PerformClick();
            }
        }
        void btnLogin_Click(object sender, EventArgs e)
        {
            // Compare Password
            ComparePassord();
        }

        // Function and methods
        void CheckSecurityEmpl()
        {
            if (con.State == ConnectionState.Open) con.Close(); con.Open();
            try
            {
                string sqlSecur =
                   @"SELECT [EMPLID],[SECURKEY]
                     FROM [HROS_TSYSSECUR] 
                     WHERE EMPLID = '{0}' ) ";

                sqlSecur = string.Format(@sqlSecur, tmpPWEMPLOYEE.ToUpper().Trim());

                SqlCommand cmdSecur = new SqlCommand(@sqlSecur, con);
                SqlDataReader reader = cmdSecur.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tmpSECUREMPLID = reader["EMPLID"].ToString();
                        tmpSECUREMPL = reader["SECURKEY"].ToString();
                        break;
                    }

                    this.GetTSysUser();
                    //this.LogInEmpl();
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลพนักงาน ลองใหม่อีกครั้ง !!!");
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
        void CheckEmpl()
        {                        
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                string sql = @"SELECT top 1 [PWCARD] ,[PWEMPLOYEE] ,[PWFNAME] ,[PWLNAME] 
                                ,(RTRIM(PWFNAME) + ' ' + RTRIM(PWLNAME) + '[' + RTRIM(PWNICKNAME) + ']') AS PWFULLNAME 
                                ,[PWSECTION],[PWPOSITION],[PWDEPT],PWBIRTHDAY ,PWDIVISION 
                                FROM [dbo].[PWEMPLOYEE] WHERE (PWEMPLOYEE = '{0}' OR PWCARD = '{0}') AND [PWSTATWORK] IN ('A','V')";

                sql = string.Format(@sql, txtUser.Text.ToUpper().Trim());

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    emplTable = new DataTable("EmplTable");
                    emplTable.Load(reader);
                    reader.Close();

                    this.GetTSysUser();
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลพนักงาน ลองใหม่อีกครั้ง !!!");
                    this.txtUser.Focus();
                    return;
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
        void CheckCondition()
        {

            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                string sql = string.Format(@"SELECT top 1 * from SPC_CM_CONDITION_DOC ");

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dtcondition = new DataTable("ConditionTable");
                    dtcondition.Load(reader);
                    reader.Close();

                }
                else
                {
                    MessageBox.Show("ไม่พบเงื่อนไขการอนุมัติ !!!");
                    return;
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
        void GetTSysUser()
        {
            //
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
//                string sql =
//                   @"SELECT [PWCARD] ,[PWEMPLOYEE],[PWPASS],[KEYPASS],[PWSECTION],PWPOSITION,PWDEPT
//                     ,[SYS_OUTOFFICE],[SYS_HRAPPROVEOUT],[SYS_HRAPPROVEIN]
//                     FROM HROS_TSYSUSER 
//                     WHERE (PWEMPLOYEE = '{0}' OR PWCARD = '{0}') ";

                string sql =
                    @"SELECT A.PWCARD ,A.PWEMPLOYEE ,A.PWPASS ,A.KEYPASS ,A.PWSECTION ,A.PWPOSITION ,A.PWDEPT
                            ,A.SYS_OUTOFFICE ,A.SYS_HRAPPROVEOUT ,A.SYS_HRAPPROVEIN
                            ,A.SYS_MNAPPROVEOUT ,A.SYS_MNAPPROVEIN ,A.SYS_ADMIN
                            ,B.PWDIVISION
                      FROM HROS_TSYSUSER A LEFT OUTER JOIN [PWEMPLOYEE] B 
                            ON A.PWEMPLOYEE = B.PWEMPLOYEE collate Thai_CS_AS
                      WHERE (B.PWEMPLOYEE = '{0}' OR B.PWCARD = '{0}')
                      AND B.PWSTATWORK LIKE '[AV]' ";

                sql = string.Format(@sql, txtUser.Text.ToUpper().Trim());

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.HasRows)
                    {
                        tsysUser = new DataTable("TSysUser");
                        tsysUser.Load(rd);
                        rd.Close();
                    }
                    else
                    {
                        tsysUser = null;
                        MessageBox.Show("this user is not in system.");
                        return;
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally {}
        }
        void ComparePassord()
        {
            CheckEmpl();
            CheckCondition();
            if ((emplTable != null && emplTable.Rows.Count > 0) && (tsysUser != null && tsysUser.Rows.Count > 0))
            {
                if (this.txtPass.Text == tsysUser.Rows[0]["KEYPASS"].ToString() && tsysUser.Rows[0]["PWPASS"].ToString() == "")
                {
                    using (var frmNewPass = new FormCreateNewPassword() { StartPosition = FormStartPosition.CenterParent, Text = tmpPWEMPLOYEE })
                    {
                        frmNewPass.EmplId = emplTable.Rows[0]["PWEMPLOYEE"].ToString().Trim();
                        frmNewPass.EmplName = emplTable.Rows[0]["PWFULLNAME"].ToString();
                        frmNewPass.Key = tsysUser.Rows[0]["KEYPASS"].ToString();
                        if (DialogResult.OK == frmNewPass.ShowDialog(this))
                        {
                            MessageBox.Show("เปลี่ยนรหัสผ่านใหม่เรียบร้อย");
                            txtPass.Clear();
                            this.txtPass.Focus();
                            return;
                        }
                        else
                        {
                            var UserName = ClassCurUser.LogInEmplId;
                            return;
                        }
                    }
                }
                else
                {
                    // Commare password
                    try
                    {                     
                        var keyPass = tsysUser.Rows[0]["KEYPASS"].ToString().Trim();
                        var pwPass = tsysUser.Rows[0]["PWPASS"].ToString().Trim();
                        var decryption = HROUTOFFICE.ClassCryptography.TripleDESDecrypt(pwPass, keyPass , true);
                        //var decryption = ClassCryptography.TripleDESEncrypt(this.txtPass.Text.Trim(), tmpPASS.ToString(), true);
                        if (this.txtPass.Text.Trim() == decryption)
                        {
                            ClassCurUser.LogInEmplId = emplTable.Rows[0]["PWEMPLOYEE"].ToString().Trim();
                            ClassCurUser.LogInEmplName = emplTable.Rows[0]["PWFULLNAME"].ToString().Trim();
                            ClassCurUser.LogInEmplDivision = emplTable.Rows[0]["PWDIVISION"].ToString().Trim();

                            ClassCurUser.SysOutoffice = tsysUser.Rows[0]["SYS_OUTOFFICE"].ToString().Trim();
                            ClassCurUser.SysHrApproveOut = tsysUser.Rows[0]["SYS_HRAPPROVEOUT"].ToString().Trim();
                            ClassCurUser.SysHrApproveIn = tsysUser.Rows[0]["SYS_HRAPPROVEIN"].ToString().Trim();
                            
                            ClassCurUser.SysMNApproveOut = tsysUser.Rows[0]["SYS_MNAPPROVEOUT"].ToString().Trim();
                            ClassCurUser.SysMNApproveIn = tsysUser.Rows[0]["SYS_MNAPPROVEIN"].ToString().Trim();

                            ClassCurUser.SysAdministrator = tsysUser.Rows[0]["SYS_ADMIN"].ToString().Trim();

                            ClassCurUser.LogInSection = emplTable.Rows[0]["PWSECTION"].ToString().Trim();
                            ClassCurUser.LogInEmplKey = tsysUser.Rows[0]["KEYPASS"].ToString();

                            ClassCurUser.BEFOREDATECREATE = dtcondition.Rows[0]["BEFOREDATECREATE"].ToString();
                            ClassCurUser.AFTERDATECREATE = dtcondition.Rows[0]["AFTERDATECREATE"].ToString();
                            ClassCurUser.LASTDATEAPPROVE_SHIFT = dtcondition.Rows[0]["LASTDATEAPPROVE_SHIFT"].ToString();
                            ClassCurUser.LASTDATEAPPROVE_LEAVE = dtcondition.Rows[0]["LASTDATEAPPROVE_LEAVE"].ToString();
                            ClassCurUser.LASTDATEAPPROVE_CHANGE = dtcondition.Rows[0]["LASTDATEAPPROVE_CHANGE"].ToString();

                            //MDIParent1 mdiparent = new MDIParent1();
                            //mdiparent.Show();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {                           
                            MessageBox.Show("รหัสผ่านของคุณไม่ถูกต้อง");
                            this.txtPass.Clear();
                            this.txtPass.Focus();
                        }
                    }
                    catch (DecoderFallbackException callbackEx)
                    {
                        MessageBox.Show(callbackEx.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                return;
            }
        }
        void SetClearControls()
        {
            this.txtUser.Clear();
            this.txtPass.Clear();
            this.txtUser.Focus();
        }
        void Update_empl()
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;
                        con.Open();
             
                        sqlCommand.CommandText = string.Format(
                                    @" UPDATE A
	                                        SET A.PWEMPLOYEE = E.PWEMPLOYEE, A.PWSECTION = E.PWSECTION ,A.PWPOSITION = E.PWPOSITION ,A.PWDEPT = E.PWDEPT

                                        FROM	[dbo].[HROS_TSYSUSER] A INNER JOIN PWEMPLOYEE E ON A.PWCARD = E.PWCARD COLLATE DATABASE_DEFAULT
                                        WHERE	A.PWCARD 

                                                IN 
                                                  (

				                                        SELECT PWCARD COLLATE DATABASE_DEFAULT AS PWCARD FROM
				                                        (
							                                        SELECT
								                                        A.PWCARD as PWCARDG, A.PWEMPLOYEE as PWEMPLOYEEG,  A.PWSECTION as PWSECTIONG, A.PWPOSITION as PWPOSITIONG, A.PWDEPT as PWDEPTG, E.PWCARD, E.PWEMPLOYEE, E.PWFNAME, E.PWLNAME,  E.PWSECTION, E.PWPOSITION , E.PWDEPT
								                                        , CASE WHEN A.PWEMPLOYEE != E.PWEMPLOYEE COLLATE DATABASE_DEFAULT THEN '0' ELSE '1' END AS [MARKED_EMPLID]
								                                        , CASE WHEN A.PWSECTION != E.PWSECTION COLLATE DATABASE_DEFAULT THEN '0' ELSE '1' END AS [MARKED_PWSECTION]
								                                        , CASE WHEN A.PWPOSITION != E.PWPOSITION COLLATE DATABASE_DEFAULT THEN '0' ELSE '1' END AS [MARKED_PWPOSITION]
								                                        , CASE WHEN A.PWDEPT != E.PWDEPT COLLATE DATABASE_DEFAULT THEN '0' ELSE '1' END AS [MARKED_PWDEPT]
							                                        FROM	[dbo].[HROS_TSYSUSER] A WITH (NOLOCK)
								                                        LEFT OUTER JOIN PWEMPLOYEE E WITH(NOLOCK) ON A.PWCARD = E.PWCARD COLLATE DATABASE_DEFAULT
                                                          )
				                                           AS  DDS 

				                                          WHERE  [MARKED_EMPLID]='0'   OR [MARKED_PWSECTION]='0' OR [MARKED_PWPOSITION] = '0' OR [MARKED_PWDEPT] = '0'
                                                    )
                                    ");

                                sqlCommand.ExecuteNonQuery();
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
}
