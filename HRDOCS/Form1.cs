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
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        public Form1()
        {
            InitializeComponent();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            // <Create Email Contents>
            StringBuilder sb_mailBody = new StringBuilder();
            sb_mailBody.Append("คุณมีรายการพิจารณาอนุมัติการขอเปลี่ยนวันหยุดออนไลน์" + Environment.NewLine);
            sb_mailBody.Append(@"sample url or website here." + Environment.NewLine);
            sb_mailBody.Append(string.Format(@"ข้อความเพิ่มเติม:{0}{1}", Environment.NewLine, this.radTxtOterMessage.Text.Trim())); //ข้อความเพิ่มเติม
            // </Create Email Contents>

            //MessageBox.Show(sb_mailBody.ToString());

            // If you to ref. your e-mail address, change Email Address: noreply@hros.supc.net to YourEmailAddress@supercheapphuket.com
           
            //HKSendEmail("noreply@hros.supc.net", "jittakorn@supercheapphuket.com",
            //    "รายการพิจารณาอนุมัติการขอเปลี่ยนวันหยุดออนไลน์", sb_mailBody.ToString());

            WSSendEmail(@"noreply@hros.supc.net", @"jittakorn@supercheapphuket.com", @"", "รายการพิจารณาอนุมัติการเปลี่ยนวันหยุดออนไลน์", sb_mailBody.ToString());
        }
        //Send To... Not have CC...
        private void HKSendEmail(string FromEmailAddress, string ToEmailAddress, string Subjects, string BodyMessages)
        {
            try
            {
                // <code P' A(HK)>
                // Create NetworkCredential with your Windows login
                // NetworkCredential("Username for login Windows", "Password for login Windows", "supc.net");
                System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential("Administrator", "Mknr1904", "supc.net");
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
                    MessageBox.Show(@"An email was sent", @"Send Email Process Aler Message(s).", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // </WS>
                }
                // </code P' A(HK)>
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Send Email Process Alert Message(s).", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Sends an e-mail message using the designated SMTP mail server.
        /// </summary>
        /// <param name="subject">The subject of the message being sent.</param>
        /// <param name="messageBody">The message body.</param>
        /// <param name="fromAddress">The sender's e-mail address.</param>
        /// <param name="toAddress">The recipient's e-mail address (separate multiple e-mail addresses
        /// with a semi-colon).</param>
        /// <param name="ccAddress">The address(es) to be CC'd (separate multiple e-mail addresses with
        /// a semi-colon).</param>
        /// <remarks>You must set the SMTP server within this method prior to calling.</remarks>
        /// <example>
        /// <code>
        ///   // Send a quick e-mail message
        ///   SendEmail.SendMessage("This is a Test", 
        ///                         "This is a test message...",
        ///                         "noboday@nowhere.com",
        ///                         "somebody@somewhere.com", 
        ///                         "ccme@somewhere.com");
        /// </code>
        /// </example>
        /// 
        // ToEmailAddress: "addr1@xxx;addr2@xxx;"
        // CcEmailAddresses: "addr1@yyy;addr2@yyy;"
        //Send To...& CC To
        public void WSSendEmail(string FromEmailAddress, string ToEmailAddress, string CcEmailAddresses,string Subjects, string BodyMessages)
        {
            // <WS:2014-11-01>
            System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential("Administrator", "Mknr1904", "supc.net");
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress(FromEmailAddress);
            if (ToEmailAddress.Trim().Length > 0)
            {
                foreach (string addr in ToEmailAddress.Split(';'))
                    mailMessage.To.Add(new System.Net.Mail.MailAddress(addr.Trim()));
            }
            if (CcEmailAddresses.Trim().Length > 0)
            {
                foreach (string addr in CcEmailAddresses.Split(';'))
                    mailMessage.CC.Add(new System.Net.Mail.MailAddress(addr.Trim()));
            }
            mailMessage.Subject = Subjects;
            mailMessage.Body = BodyMessages;
            //mailMessage.IsBodyHtml = true; //Modified by WS
            mailMessage.IsBodyHtml = false;
            mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;

            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("spc655srv.supc.net");
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = networkCredential;
            smtpClient.EnableSsl = true;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
            smtpClient.Send(mailMessage);

            // Message Here //


        }

    }
}
