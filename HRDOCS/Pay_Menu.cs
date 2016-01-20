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
    public partial class Pay_Menu : Form
    {
        public Pay_Menu()
        {
            InitializeComponent();
        }

       
        private void lbl_Report_Click(object sender, EventArgs e)
        {
            Pay_Report report = new Pay_Report();
            report.Show();
        }

        private void lbl_Creat_Click(object sender, EventArgs e)
        {
            Pay_Creat frm = new Pay_Creat();
            frm.Show();
        }

        private void lbl_Diagnosis_Click(object sender, EventArgs e)
        {
            Pay_Delete frm = new Pay_Delete();
            frm.Show();
        }

        private void lbl_SignUp_Click(object sender, EventArgs e)
        {
            Frm_SendMail frm = new Frm_SendMail();
            frm.Show();
        }

        private void lbl_workpermit_Click(object sender, EventArgs e)
        {
            Pay_CheckPay frm = new Pay_CheckPay();
            frm.Show();
        }
       
    }
}
