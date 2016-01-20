using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HRDOCS
{
    public partial class ConfirmReject : Form
    {
        public string Remark = "";

        public ConfirmReject()
        {
            InitializeComponent();
            InitializeButton();
        }

        #region InitializeButton

        private void InitializeButton()
        {

            Btn_Confirm.Click += new EventHandler(Btn_Confirm_Click);
        
        }

        void Btn_Confirm_Click(object sender, EventArgs e)
        {
            Remark = Txt_Remark.Text;
            this.DialogResult = DialogResult.Yes;
        }

        #endregion 



    }
}
