using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SysApp;
using Telerik.WinControls.UI;

namespace HRDOCS
{
    public partial class SendEmail : Form
    {
        string DOCNO = "";
        string ApproveID = "";
        string ApproveName = "";

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


        public SendEmail(string pDOCNO)
        {
            DOCNO = pDOCNO;
            InitializeForm();
            InitializeComponent();
            InitializeButton();
            InitializeTextbox();
        }

        #region InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(SendEmail_Load);
        }

        void SendEmail_Load(object sender, EventArgs e)
        {
            CheckEmail();
        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_Send.Click += new EventHandler(Btn_Send_Click);
        }

        void Btn_Send_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Ddl_To.Text != "" || Txt_Subject.Text != "" || Txt_Detail.Text != "")
            {
                HKSendEmail("noreply@hros.supc.net", Ddl_To.SelectedValue.ToString(),
                    Txt_Subject.Text, Txt_Detail.Text);
            }
            Cursor.Current = Cursors.Default;
            this.DialogResult = DialogResult.Yes; 
        }

        #endregion

        #region InitializeTextbox

        private void InitializeTextbox()
        {
            ;
            Ddl_To.DropDownListElement.AutoCompleteSuggest.SuggestMode = Telerik.WinControls.UI.SuggestMode.Contains;

            Txt_Subject.Text = "แจ้งอนุมัติเอกสาร";
            Txt_Detail.Text = "เลขที่เอกสาร : " + DOCNO +System.Environment.NewLine
                                + "ผู้สร้างเอกสาร : " + ClassCurUser.LogInEmplName ;

            Load_EmailList();
        }

        #endregion

        #region Function

        void Load_EmailList()
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
                    sqlCommand.CommandText = string.Format(@" select * from SPC_CM_EMAIL
                                            order by cn ");

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
                    Ddl_To.DataSource = dataTable;
                    Ddl_To.DisplayMember = "CN";
                    Ddl_To.ValueMember = "MAIL";
                }
                else
                {
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
                sqlConnection.Close();
                Cursor.Current = Cursors.Default;
            }
        
        }

        void btnSendEmail_Click(object sender, EventArgs e)
        {
            //string serverPrefix = string.Format("http://{0}", @"Localhost");
            //string destinationServerPath = "/hros/aprrove/";
            //string destinationServerFile = "approve-change-shift.aspx";
            //string urlParam = string.Format("dn#{0}+pwemp#{1}+appv#{2}",
            //    @"CHDxxxxxx-yyyy",
            //    @"M1007066",
            //    @"M0111029");

            //// <Create Sample Url>
            //string approverUrl = string.Format("{0}{1}{2}?para={3}",
            //                        serverPrefix.ToString(),
            //                        destinationServerPath.ToString(),
            //                        destinationServerFile.ToString(),
            //                        urlParam.ToString());
            //// </Create Sample Url>

            //// <Create Email Contents>
            //StringBuilder sb_mailBody = new StringBuilder();
            //sb_mailBody.Append("คุณมีรายการพิจารณาอนุมัติการขอเปลี่ยนกะ" + Environment.NewLine);
            //sb_mailBody.Append(approverUrl.ToString() + Environment.NewLine);
            //sb_mailBody.Append(string.Format(@"ข้อความเพิ่มเติม:{0}{1}", Environment.NewLine, this.richTextBox1.Text.Trim())); //ข้อความเพิ่มเติม
            //// </Create Email Contents>

            ////MessageBox.Show(sb_mailBody.ToString());

            //// If you to ref. your e-mail address, change Email Address: noreply@hros.supc.net to YourEmailAddress@supercheapphuket.com
            //HKSendEmail("noreply@hros.supc.net", "wissaroot@supercheapphuket.com",
            //    "รายการพิจารณาอนุมัติการขอเปลี่ยนกะ", sb_mailBody.ToString());
        }

        // Change this mothods as Global mothod, support access from anywhere
        private void HKSendEmail(string FromEmailAddress, string ToEmailAddress, string Subjects, string BodyMessages)
        {
            try
            {
                // <code P' A(HK)>
                // Create NetworkCredential with your Windows login
                // NetworkCredential("Username for login Windows", "Password for login Windows", "supc.net");
                System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential("wissaroot", "1234567890", "supc.net");
                using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(FromEmailAddress, ToEmailAddress, Subjects, BodyMessages))
                {
                    mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
                    System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("spc655srv.supc.net");
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = networkCredential;
                    smtpClient.EnableSsl = true;
                    //smtpClient.Timeout = 30;

                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };

                    // <WS:Modified> 2014-10-31
                    // set IsBodyHtml = false;, support return line(Enter Line)
                    // if you want to display  email content as table or html, just set IsBodyHtml = true;
                    mailMessage.IsBodyHtml = false;
                    // </WS:Modified> 2014-10-31

                    smtpClient.Send(mailMessage);

                    // <WS> 2014-10-31
                    MessageBox.Show(@"ส่งอีเมลล์เรียบร้อย...", @"Send Email Process Alert Message(s).", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // </WS>
                }
                // </code P' A(HK)>
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Send Email Process Alert Message(s).", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CheckEmail()
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
                    sqlCommand.CommandText = string.Format(@" select * from SPC_CM_EMAIL
                                            where EMPLID = '{0}' ",(ClassCurUser.LogInEmplId.ToString().Substring(1)));

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

                //if (dataTable.Rows.Count > 0)
                //{
                //    string LogiInEmail = dataTable.Rows[0]["MAIL"].ToString();
                //}
                //else
                //{
                //    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                sqlConnection.Close();
                Cursor.Current = Cursors.Default;
            }
        
        }

        #endregion

    }
}
