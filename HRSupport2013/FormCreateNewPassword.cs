using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HROUTOFFICE
{
    public partial class FormCreateNewPassword : Form
    {
        string[] secureKey = new string[] { };
        string configSecureKey = "OF-HR-W";
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


        protected string newPassword = "";
        public string NewPassword
        {
            get { return newPassword; }
        }
        public FormCreateNewPassword()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;

            this.Load += new EventHandler(FormCreateNewPassword_Load);

            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);

            this.txtNewPassword.Focus();
        }
        public FormCreateNewPassword(String _emplID, string _key)
            : this()
        {
            
            emplId = _emplID.Trim().ToUpper();
            key = _key;
            //this.txtEmplID.Text = EmplId;
        }

        void FormCreateNewPassword_Load(object sender, EventArgs e)
        {
            this.txtEmplID.Text = EmplId;
            this.txtEmplName.Text = EmplName;
            this.txtNewPassword.Focus();

            secureKey = this.AccessibleName.Split(new char[] { ',' });

            foreach (var key in secureKey)
            {
                if (configSecureKey == key)
                {
                    this.groupBox1.Enabled = true;
                }
                else
                {
                    this.groupBox1.Enabled = false;
                    MessageBox.Show("การดำเนินการถูกยกเลิก เนื่องจากสิทธิในการเข้าถึงถูกจำกัด");
                    DialogResult = DialogResult.No;
                    this.Close();
                }
            }
        }
        void btnOK_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtNewPassword.Text != "" || this.txtNewPasswordConfirm.Text != "")
            {
                if (this.txtNewPassword.Text != this.txtNewPasswordConfirm.Text)
                {
                    MessageBox.Show("Password not match.");
                    DialogResult = DialogResult.No;
                    return;
                }

                if (ClassCryptography.FuctionChangePassword(EmplId, this.txtNewPasswordConfirm.Text, Key))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.No;
                }
            }
            else
            {
                MessageBox.Show("รหัสผ่านต้องไม่เป็นค่าว่าง");
                //return;
            }
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }  
    }
}
