using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SysApp;
namespace HRDOCS
{
    public partial class Shift_Main : Form
    {


        public Shift_Main()
        {
            InitializeComponent();
            InitializeButton();
        }

        #region InitializeForm

        private void InitializeForm()
        {

        }


        #endregion

        #region InitializeButton

        private void InitializeButton()
        {

            Btn_ShiftEdit.Click += new EventHandler(Btn_ShiftEdit_Click);
            Btn_ShiftCreate.Click += new EventHandler(Btn_ShiftCreate_Click);
            Btn_HDApprove.Click += new EventHandler(Btn_HDApprove_Click);
            Btn_HRApprove.Click += new EventHandler(Btn_HRApprove_Click);
            Btn_User.Click += new EventHandler(Btn_User_Click);
            Btn_Search.Click += new EventHandler(Btn_Search_Click);

            ClassCurUser.LogInEmplId = "M0111029";
            ClassCurUser.LogInEmplName = "ภูษิต อาญาสิทธิ์";
            ClassCurUser.LogInEmplDivision = "76";
            ClassCurUser.LogInSection = "32";
        }

        void Btn_Search_Click(object sender, EventArgs e)
        {
            using (Shift_SearchData frm = new Shift_SearchData())
            {
                frm.Text = "ดูข้อมูล";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {

                }
            }
        }

        void Btn_User_Click(object sender, EventArgs e)
        {
            

            if(Btn_User.Text == "หัวหน้า")
            {
                ClassCurUser.LogInEmplId = "M1106163";
                ClassCurUser.LogInEmplName = "ศศิธร เก้าเอี้ยน";
                ClassCurUser.LogInEmplDivision = "76";
                ClassCurUser.LogInSection = "332";
                Btn_User.Text = "ผู้ช่วย";
            }
            else if (Btn_User.Text == "ผู้ช่วย")
            {
                ClassCurUser.LogInEmplId = "M1104008";
                ClassCurUser.LogInEmplName = "ชาคร มงคลสถิตย์พร";
                ClassCurUser.LogInEmplDivision = "76";
                ClassCurUser.LogInSection = "32";
                Btn_User.Text = "พนักงาน";
            }
            else if (Btn_User.Text == "พนักงาน")
            {
                ClassCurUser.LogInEmplId = "M1007066";
                ClassCurUser.LogInEmplName = "วิศรุต สามารถ";
                ClassCurUser.LogInEmplDivision = "76";
                ClassCurUser.LogInSection = "32";
                Btn_User.Text = "บุคคล";
            }
            else 
            {
                ClassCurUser.LogInEmplId = "M0111029";
                ClassCurUser.LogInEmplName = "ภูษิต อาญาสิทธิ์";
                ClassCurUser.LogInEmplDivision = "76";
                ClassCurUser.LogInSection = "32";
                Btn_User.Text = "หัวหน้า";
            }

        }

        void Btn_HRApprove_Click(object sender, EventArgs e)
        {
            using (Shift_ApproveHR frm = new Shift_ApproveHR())
            {
                frm.Text = "บุคคล อนุมัติ";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {

                }
            }
        }

        void Btn_HDApprove_Click(object sender, EventArgs e)
        {
            using (Shift_ApproveHD frm = new Shift_ApproveHD())
            {
                frm.Text = "หัวหน้า/ผู้ช่วย อนุมัติ";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {

                }
            }
        }

        void Btn_ShiftCreate_Click(object sender, EventArgs e)
        {
            using (Shift_Create frm = new Shift_Create())
            {
                frm.Text = "สร้างเอกสารใบเปลี่ยนกะ";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {
                   
                }
            }
        }

        void Btn_ShiftEdit_Click(object sender, EventArgs e)
        {
            using (Shift_Edit frm = new Shift_Edit())
            {
                frm.Text = "แก้ไขข้อมูล";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {
                   
                }
            }
        }

        #endregion


    }
}
