using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Data.SqlClient;
using SysApp;
using HRDOCS;
using HRCHANGHOLIDAY;

namespace HROUTOFFICE
{
    public partial class FormMain : Form
    {
        private int childFormNumber = 0;

        string sys_hdapprove;
        string sys_hrapprovein;
        string sys_hrapproveout;
        string sys_mnapprovein;
        string sys_mnapproveout;
        string sys_administrater;

        static internal string GetVersion()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public FormMain()
        {
            InitializeComponent();
            //this.Icon = HRSupport2013.Properties.Resources.
            this.Text = string.Format(@"HROS [{0}] - เอกสารบุคคลออนไลน์"
                , GetVersion());
            this.radPageViewStart.Dock = DockStyle.Fill;
            this.radPageViewPage1.Dock = DockStyle.Fill;
            this.radPageViewPage2.Dock = DockStyle.Fill;
            this.radPageViewPage3.Dock = DockStyle.Fill;
            this.radPageViewPage4.Dock = DockStyle.Fill;
            this.radPageViewPage5.Dock = DockStyle.Fill;
            this.radPageViewPage6.Dock = DockStyle.Fill;

            this.radTreeViewAdmin.Dock = DockStyle.Fill;
            this.radTreeViewGournal.Dock = DockStyle.Fill;
            this.radTreeViewHD_Approve.Dock = DockStyle.Fill;
            this.radTreeViewHR_Approve.Dock = DockStyle.Fill;
            this.radTreeViewMN_Approve.Dock = DockStyle.Fill;
            this.radTreeViewSecur.Dock = DockStyle.Fill;

            this.radPageViewPage1.Text = "ทั่วไป";
            this.radPageViewPage2.Text = "หัวหน้า / ผู้ช่วย (SUPC)";
            this.radPageViewPage4.Text = "มินิมาร์ท / คลังยา";
            this.radPageViewPage5.Text = "แผนกบุคคล";
            this.radPageViewPage3.Text = "ผู้ดูแลระบบ";
            this.radPageViewPage6.Text = "แผนก ร.ป.ภ.";
        }


        #region <Default MenuStrip>
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }
        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        #region Even Click
        private void FormMain_Shown(object sender, EventArgs e)
        {
            this.LogIn();
        }
        private void ToolStripMenuItemLogOut_Click(object sender, EventArgs e)
        {
            LogOut();
        }
        private void ToolStripMenuItemChangPass_Click(object sender, EventArgs e)
        {
            ChangPass();
        }
        #endregion

        #region radTreeView

        private void radTreeViewGournal_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "CreatDoc":
                    this.CreatedNewDoc();
                    break;
                case "EditDoc":
                    this.EditDoc();
                    break;
                case "DeleteDoc":
                    this.DeleteDoc();
                    break;
                case "StatusDoc":
                    this.ReportStatusDoc();
                    break;
                case "Shift_CreateDoc":
                    this.Shift_CreateNewDoc();
                    break;
                case "Shift_EditDoc":
                    this.Shift_EditNewDoc();
                    break;
                case "Shift_StatusDoc":
                    this.Shift_StatusNewDoc();
                    break;
                case "Leave_CreateDoc":
                    this.Leave_CreateNewDoc();
                    break;
                case "Leave_EditDoc":
                    this.Leave_EditDoc();
                    break;
                case "Leave_StatusDoc":
                    this.Leave_StatusDoc();
                    break;

                case "CHD_CreateDoc":
                    this.CHD_CreateDoc();
                    break;
                case "CHD_Edit":
                    this.CHD_Edit();
                    break;
                case "CHD_DocStatust":
                    this.CHD_DocStatust();
                    break;
                case "CN_CreateDoc":
                    this.CN_CreateDoc();
                    break;
                case "CN_EditDoc":
                    this.CN_EditDoc();
                    break;
                case "CN_DocStatust":
                    this.CN_DocStatust();
                    break;
                default: break;
            }
        }

        private void radTreeViewHD_Approve_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "HeadApprove":
                    this.HeadApprove();
                    break;
                case "ShiftHeadApprove":
                    this.Shift_HeadApprove();
                    break;
                case "LeaveHeadApprove":
                    this.Leave_HeadApprove();
                    break;
                case "ChdHeadApprove":
                    this.Chd_HeadApprove();
                    break;
                case "CNHeadApprove":
                    this.CN_HeadApprove();
                    break;
                default: break;

            }
        }

        private void radTreeViewMN_Approve_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "MN_ApproveOut":
                    this.MN_ApproveOut();
                    break;
                case "MN_ApproveIn":
                    this.MN_ApproveIn();
                    break;
                case "Shift_MNApprove":
                    this.Shift_MN_Approve();
                    break;
                case "Leave_MNApprove":
                    this.Leave_MNApprove();
                    break;
                case "Chd_ApproveMN":
                    this.Chd_MN_Approve();
                    break;
                case "CN_ApproveMN":
                    this.CN_MN_Approve();
                    break;
                default: break;
            }
        }

        private void radTreeViewHR_Approve_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "HrApproveOut":
                    this.HrApproveOut();
                    break;
                case "HrApproveIn":
                    this.HrApproveIn();
                    break;
                case "HrEmplFood":
                    this.HrEmplFood();
                    break;
                case "HrReportOutType":
                    this.ReportHrOutType();
                    break;
                case "ReportOutOverTime":
                    this.ReportOutOverTime();
                    break;
                case "ReportWeekedOver":
                    this.ReportWeekedOver();
                    break;
                case "ShiftHRApprove":
                    this.Shift_HrApprove();
                    break;
                case "HRShiftReport":
                    this.Shift_ReportApprove();
                    break;
                case "HRShiftStatusReport":
                    this.Shift_ReportStatus();
                    break;
                case "HRShiftReportOT":
                    this.Shift_ReportOT();
                    break;
                case "Leave_HRApprove":
                    this.Leave_HRApprove();
                    break;
                case "HRLeaveReport":
                    this.HRLeaveReport();
                    break;
                case "HRLeaveStatusReport":
                    this.HRLeaveStatusReport();
                    break;
                case "HRAllLeaveStatusReport":
                    this.HRAllLeaveStatusReport();
                    break;
                case "ChdHRApprove":
                    this.Chd_HRApprove();
                    break;
                case "HRChdReport":
                    this.Chd_HRReport();
                    break;
                case "HRChdStatusReport":
                    this.Chd_HRStatusReport();
                    break;
                case "CNHRApprove":
                    this.CN_HRApprove();
                    break;
                case "HRCNReport":
                    this.HRCNReport();
                    break;
                case "HRCNStatusReport":
                    this.HRCNStatusReport();
                    break;

                default: break;
            }
        }

        private void radTreeViewSecur_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "PayCreat":
                    this.PayCreat();
                    break;
                default: break;

            }
            switch (e.Node.Name)
            {
                case "PayCheck":
                    this.PayCheck();
                    break;
                default: break;
            }
            switch (e.Node.Name)
            {
                case "PayCheckImportBill":
                    this.PayCheckImportBill();
                    break;
                default: break;
            }
            switch (e.Node.Name)
            {
                case "PayReport":
                    this.PayReport();
                    break;
                default: break;
            }
            switch (e.Node.Name)
            {
                case "PayDelete":
                    this.PayDelete();
                    break;
                default: break;  
            }


        }

        private void radTreeViewAdmin_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "AddsysUse":
                    this.AddsysUse();
                    break;
                default: break;

            }
            switch (e.Node.Name)
            {
                case "AddEmpl":
                    this.AddEmpl();
                    break;
                default: break;
            }
            switch (e.Node.Name)
            {
                case "AddAuthen":
                    this.AddAuthen();
                    break;
                default: break;
            }
        }

        //private void radTreeViewSecur_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        //{
        //    switch (e.Node.Name)
        //    {
        //        case "Pay_SignUp":
        //            this.PaySignUp();
        //            break;
        //        default:break;
        //    }
        //    switch (e.Node.Name)
        //    {
        //        case "Pay_Diagnosis":
        //            this.PayDiagnosis();
        //            break;
        //        default:break;
        //    }
        //    switch (e.Node.Name)
        //    {
        //        case "Pay_Workpermit":
        //            this.PayWorkpermit();
        //            break;
        //        default: break;
        //    }
        //    switch (e.Node.Name)
        //    {
        //        case "Pay_Visa":
        //            this.PayVisa();
        //            break;
        //        default: break;
        //    }

        //    switch (e.Node.Name)
        //    {
        //        case "Pay_Report":
        //            this.PayReport();
        //            break;
        //        default: break;
        //    }
        //}

        #endregion

        #region <Functions and Methods>
        // <SignIn and SignOut>
        private void LogIn()
        {
            if (ClassCurUser.LogInEmplId == null || string.IsNullOrEmpty(ClassCurUser.LogInEmplId.ToString()))
            {
                using (FormLogIn frmLogIn = new FormLogIn())
                {
                    frmLogIn.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == frmLogIn.ShowDialog(this))
                    {
                        this.statusLabelUserName.Text = ClassCurUser.LogInEmplName;
                        //<neung 26-04-57>
                        this.sys_hdapprove = ClassCurUser.SysOutoffice;

                        this.sys_hrapprovein = ClassCurUser.SysHrApproveIn;
                        this.sys_hrapproveout = ClassCurUser.SysHrApproveOut;

                        this.sys_mnapprovein = ClassCurUser.SysMNApproveIn;
                        this.sys_mnapproveout = ClassCurUser.SysMNApproveOut;
                        this.sys_administrater = ClassCurUser.SysAdministrator;

                        if (sys_hdapprove != "")
                        {
                            if (sys_hdapprove == "1")
                            {
                                // radPageViewStart.Pages[0].Hide();
                                this.radPageViewPage2.Enabled = true;
                            }
                            else
                            {
                                this.radPageViewPage2.Enabled = false;
                            }
                        }
                        if (sys_mnapprovein != "" && sys_mnapproveout != "")
                        {
                            if (sys_mnapprovein == "1" || sys_mnapproveout == "1")
                            {
                                this.radPageViewPage4.Enabled = true;
                            }
                            else
                            {
                                this.radPageViewPage4.Enabled = false;
                            }
                        }

                        if (sys_hrapprovein != "" && sys_hrapproveout != "")
                        {
                            if (sys_hrapprovein == "1" || sys_hrapproveout == "1")
                            {
                                this.radPageViewPage5.Enabled = true;
                            }
                            else
                            {
                                this.radPageViewPage5.Enabled = false;
                            }
                        }
                        if (sys_administrater != "")
                        {
                            if (sys_administrater == "1")
                            {
                                this.radPageViewPage3.Enabled = true;
                            }
                            else
                            {
                                this.radPageViewPage3.Enabled = false;
                            }
                        }
                        //<end neung>
                    }
                    else
                    {
                        this.statusLabelUserName.Text = "";
                        this.Close();
                    }
                }
            }
        }
        private void LogOut()
        {
            if (MessageBox.Show("ยืนยันการออกจากระบบ ?", "ออกจากระบบ ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.statusLabelUserName.Text = "";
                ClassCurUser.LogInEmplId = "";
                ClassCurUser.LogInEmplKey = "";
                ClassCurUser.LogInEmplName = "";
                ClassCurUser.LogInSection = "";

                ClassCurUser.SysOutoffice = "";

                ClassCurUser.SysHrApproveOut = "";
                ClassCurUser.SysHrApproveIn = "";

                ClassCurUser.SysMNApproveOut = "";
                ClassCurUser.SysMNApproveIn = "";

                ClassCurUser.LogInEmplDivision = "";

                foreach (Form childForm in MdiChildren)
                {
                    childForm.Close();
                }
                LogIn();
            }
        }
        private void ChangPass()
        {
            FormCreateNewPassword frmChangPass = new FormCreateNewPassword();
            frmChangPass.EmplId = ClassCurUser.LogInEmplId;
            frmChangPass.EmplName = ClassCurUser.LogInEmplName;
            frmChangPass.Key = ClassCurUser.LogInEmplKey;
            frmChangPass.StartPosition = FormStartPosition.CenterParent;
            // frmChangPass.MdiParent = this;
            frmChangPass.ShowDialog(this);
        }

        // <Forms and Popup Windows>
        private bool ActivateForm(Control _form)
        {
            bool ret = false;
            foreach (Form childForm in MdiChildren)
            {
                if (_form.Name == childForm.Name)
                {
                    childForm.Activate();
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        // <General Form>
        private void CreatedNewDoc()
        {
            FormCreateNew frm3 = new FormCreateNew();
            //if (!ActivateForm(frm3))
            if (!this.CheckOpened(frm3.Name))
            {
                frm3.EmplId = ClassCurUser.LogInEmplId;
                frm3.EmplName = ClassCurUser.LogInEmplName;
                frm3.Key = ClassCurUser.LogInEmplKey;

                frm3.StartPosition = FormStartPosition.CenterParent;
                frm3.WindowState = FormWindowState.Maximized;
                frm3.MdiParent = this;
                frm3.Show();
            }
        }
        private void EditDoc()
        {
            FormEmplEdit EditData = new FormEmplEdit();
            //if (!ActivateForm(EditData))
            if (!this.CheckOpened(EditData.Name))
            {
                EditData.EmplId = ClassCurUser.LogInEmplId;
                EditData.EmplName = ClassCurUser.LogInEmplName;
                EditData.Key = ClassCurUser.LogInEmplKey;
                EditData.StartPosition = FormStartPosition.CenterParent;
                EditData.WindowState = FormWindowState.Maximized;
                EditData.MdiParent = this;
                EditData.Show();
            }
        }
        private void DeleteDoc()
        {
            FormDeleteEmpl DeleteData = new FormDeleteEmpl();
            //if (!ActivateForm(DeleteData))
            if (!this.CheckOpened(DeleteData.Name))
            {
                DeleteData.EmplId = ClassCurUser.LogInEmplId;
                DeleteData.EmplName = ClassCurUser.LogInEmplName;
                DeleteData.Key = ClassCurUser.LogInEmplKey;

                DeleteData.StartPosition = FormStartPosition.CenterParent;
                DeleteData.WindowState = FormWindowState.Maximized;
                DeleteData.MdiParent = this;
                DeleteData.Show();
            }
        }

        // <Head and Assistant Form>
        private void HeadApprove()
        {
            FormHdApprove HeadApprove = new FormHdApprove();
            //if (!ActivateForm(HeadApprove))
            if (!this.CheckOpened(HeadApprove.Name))
            {
                HeadApprove.EmplId = ClassCurUser.LogInEmplId;
                HeadApprove.EmplName = ClassCurUser.LogInEmplName;
                HeadApprove.Key = ClassCurUser.LogInEmplKey;
                HeadApprove.SysOutoffice = ClassCurUser.SysOutoffice;
                HeadApprove.StartPosition = FormStartPosition.CenterParent;
                HeadApprove.WindowState = FormWindowState.Maximized;

                if (HeadApprove.SysOutoffice == "1")
                {
                    HeadApprove.MdiParent = this;
                    HeadApprove.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }
        }

        // <Hr Form>
        private void HrApproveOut()
        {
            FormHrApproveOut HrApproveOut = new FormHrApproveOut();
            //if (!ActivateForm(HrApproveOut))
            if (!this.CheckOpened(HrApproveOut.Name))
            {
                HrApproveOut.EmplId = ClassCurUser.LogInEmplId;
                HrApproveOut.EmplName = ClassCurUser.LogInEmplName;
                HrApproveOut.Key = ClassCurUser.LogInEmplKey;

                HrApproveOut.SysOutoffice = ClassCurUser.SysOutoffice;
                HrApproveOut.Section = ClassCurUser.LogInSection;
                HrApproveOut.HrApproveOut = ClassCurUser.SysHrApproveOut;

                HrApproveOut.StartPosition = FormStartPosition.CenterParent;
                HrApproveOut.WindowState = FormWindowState.Maximized;
                //if (HrApproveOut.Section == "02" || HrApproveOut.EmplId == "M9999999")
                //{
                if (HrApproveOut.HrApproveOut == "1")
                {
                    HrApproveOut.MdiParent = this;
                    HrApproveOut.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
                //}
                //else
                //{
                //    MessageBox.Show("สำหรับแผนกบุคคลเท่านั้น");
                //}
            }
        }

        private void HrApproveIn()
        {
            FormHrApprovedIn HrApproveIn = new FormHrApprovedIn();
            //if (!ActivateForm(HrApproveIn))
            if (!this.CheckOpened(HrApproveIn.Name))
            {
                HrApproveIn.EmplId = ClassCurUser.LogInEmplId;
                HrApproveIn.EmplName = ClassCurUser.LogInEmplName;
                HrApproveIn.Key = ClassCurUser.LogInEmplKey;

                HrApproveIn.SysOutoffice = ClassCurUser.SysOutoffice;
                HrApproveIn.Section = ClassCurUser.LogInSection;
                HrApproveIn.HrApproveIn = ClassCurUser.SysHrApproveIn;

                HrApproveIn.StartPosition = FormStartPosition.CenterParent;
                HrApproveIn.WindowState = FormWindowState.Maximized;
                //if (HrApproveIn.Section == "02" || HrApproveIn.EmplId == "M9999999")
                //{
                if (HrApproveIn.HrApproveIn == "1")
                {
                    HrApproveIn.MdiParent = this;
                    HrApproveIn.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }

            }
        }
        private void HrEmplFood()
        {
            FormEmplFood emplfood = new FormEmplFood();
            if (!this.CheckOpened(emplfood.Name))
            {
                emplfood.EmplId = ClassCurUser.LogInEmplId;
                emplfood.EmplName = ClassCurUser.LogInEmplName;
                emplfood.Key = ClassCurUser.LogInEmplKey;

                emplfood.SysOutoffice = ClassCurUser.SysOutoffice;
                emplfood.Section = ClassCurUser.LogInSection;

                emplfood.StartPosition = FormStartPosition.CenterParent;
                emplfood.WindowState = FormWindowState.Maximized;
                if (emplfood.Section == "02" || emplfood.EmplId == "M9999999")
                {

                    emplfood.MdiParent = this;
                    emplfood.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }
        }

        // <MN Form>
        private void MN_ApproveOut()
        {
            FormMNApproveOut MNApproveOut = new FormMNApproveOut();
            //if (!ActivateForm(MNApproveOut))
            if (!this.CheckOpened(MNApproveOut.Name))
            {
                MNApproveOut.EmplId = ClassCurUser.LogInEmplId;
                MNApproveOut.EmplName = ClassCurUser.LogInEmplName;
                MNApproveOut.Key = ClassCurUser.LogInEmplKey;
                MNApproveOut.Division = ClassCurUser.LogInEmplDivision;

                MNApproveOut.MNApproveOut = ClassCurUser.SysMNApproveOut;
                MNApproveOut.Section = ClassCurUser.LogInSection;

                MNApproveOut.StartPosition = FormStartPosition.CenterParent;
                MNApproveOut.WindowState = FormWindowState.Maximized;

                if (MNApproveOut.Division == "75" || MNApproveOut.Division == "11" || MNApproveOut.Division == "03"
                    || MNApproveOut.Section == "23" || MNApproveOut.EmplId == "M9999999")
                {
                    if (MNApproveOut.MNApproveOut == "1")
                    {
                        MNApproveOut.MdiParent = this;
                        MNApproveOut.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }
        }
        private void MN_ApproveIn()
        {
            FormMNApproveIn MNApproveIn = new FormMNApproveIn();
            //if (!ActivateForm(MNApproveIn))
            if (!this.CheckOpened(MNApproveIn.Name))
            {
                MNApproveIn.EmplId = ClassCurUser.LogInEmplId;
                MNApproveIn.EmplName = ClassCurUser.LogInEmplName;
                MNApproveIn.Key = ClassCurUser.LogInEmplKey;
                MNApproveIn.Division = ClassCurUser.LogInEmplDivision;

                MNApproveIn.MNApproveIn = ClassCurUser.SysMNApproveOut;
                MNApproveIn.Section = ClassCurUser.LogInSection;
                //MNApproveOut.HrApproveOut = ClassCurUser.SysHrApproveOut;

                MNApproveIn.StartPosition = FormStartPosition.CenterParent;
                MNApproveIn.WindowState = FormWindowState.Maximized;

                if (MNApproveIn.Division == "75" || MNApproveIn.Division == "11" || MNApproveIn.Division == "03"
                    || MNApproveIn.Section == "23" || MNApproveIn.EmplId == "M9999999")
                {
                    if (MNApproveIn.MNApproveIn == "1")
                    {
                        MNApproveIn.MdiParent = this;
                        MNApproveIn.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                        //MessageBox.Show("สำหรับแผนกบุคคลเท่านั้น");
                    }
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }
        }

        // <Report Form>
        private void ReportHrOutType()
        {
            FormReportHROut ReportHrOutType = new FormReportHROut();
            //if (!ActivateForm(ReportHrOutType))
            if (!this.CheckOpened(ReportHrOutType.Name))
            {
                ReportHrOutType.EmplId = ClassCurUser.LogInEmplId;
                ReportHrOutType.EmplName = ClassCurUser.LogInEmplName;
                ReportHrOutType.Key = ClassCurUser.LogInEmplKey;

                ReportHrOutType.SysOutoffice = ClassCurUser.SysOutoffice;
                ReportHrOutType.Section = ClassCurUser.LogInSection;
                ReportHrOutType.StartPosition = FormStartPosition.CenterParent;
                ReportHrOutType.WindowState = FormWindowState.Maximized;

                if ((ReportHrOutType.Section == "02" || ReportHrOutType.EmplId == "M9999999"))
                {
                    ReportHrOutType.MdiParent = this;
                    ReportHrOutType.Show();
                }
                else
                {
                    MessageBox.Show("สำหรับแผนกบุคคลเท่านั้น");
                }
            }
        }
        private void ReportOutOverTime()
        {
            FormReportOutOverTime ReportOverTime = new FormReportOutOverTime();
            //if (!ActivateForm(ReportOverTime))
            if (!this.CheckOpened(ReportOverTime.Name))
            {
                ReportOverTime.EmplId = ClassCurUser.LogInEmplId;
                ReportOverTime.EmplName = ClassCurUser.LogInEmplName;
                ReportOverTime.Key = ClassCurUser.LogInEmplKey;

                ReportOverTime.SysOutoffice = ClassCurUser.SysOutoffice;
                ReportOverTime.Section = ClassCurUser.LogInSection;
                ReportOverTime.StartPosition = FormStartPosition.CenterParent;
                ReportOverTime.WindowState = FormWindowState.Maximized;

                if ((ReportOverTime.Section == "02" || ReportOverTime.EmplId == "M9999999"))
                {
                    ReportOverTime.MdiParent = this;
                    ReportOverTime.Show();
                }
                else
                {
                    MessageBox.Show("สำหรับแผนกบุคคลเท่านั้น");
                }
            }
        }
        private void ReportWeekedOver()
        {
            FormReportWeekedOver ReportWeekedOver = new FormReportWeekedOver();
            //if (!ActivateForm(ReportWeekedOver))
            if (!this.CheckOpened(ReportWeekedOver.Name))
            {
                ReportWeekedOver.EmplId = ClassCurUser.LogInEmplId;
                ReportWeekedOver.EmplName = ClassCurUser.LogInEmplName;
                ReportWeekedOver.Key = ClassCurUser.LogInEmplKey;

                ReportWeekedOver.SysOutoffice = ClassCurUser.SysOutoffice;
                ReportWeekedOver.Section = ClassCurUser.LogInSection;
                ReportWeekedOver.StartPosition = FormStartPosition.CenterParent;
                ReportWeekedOver.WindowState = FormWindowState.Maximized;

                if ((ReportWeekedOver.Section == "02" || ReportWeekedOver.EmplId == "M9999999"))
                {
                    ReportWeekedOver.MdiParent = this;
                    ReportWeekedOver.Show();
                }
                else
                {
                    MessageBox.Show("สำหรับแผนกบุคคลเท่านั้น");
                }
            }
        }
        private void ReportStatusDoc()
        {
            FormReportStatusDoc ReportEmpl = new FormReportStatusDoc();
            //if (!ActivateForm(ReportEmpl))
            if (!this.CheckOpened(ReportEmpl.Name))
            {
                ReportEmpl.EmplId = ClassCurUser.LogInEmplId;
                ReportEmpl.EmplName = ClassCurUser.LogInEmplName;
                ReportEmpl.Key = ClassCurUser.LogInEmplKey;

                ReportEmpl.StartPosition = FormStartPosition.CenterParent;
                ReportEmpl.WindowState = FormWindowState.Maximized;


                ReportEmpl.MdiParent = this;
                ReportEmpl.Show();
            }
        }

        private void AddsysUse()
        {
            FormAddSysUser addsys = new FormAddSysUser();
            //if (!ActivateForm(addsys))
            if (!this.CheckOpened(addsys.Name))
            {
                addsys.EmplId = ClassCurUser.LogInEmplId;
                addsys.EmplName = ClassCurUser.LogInEmplName;
                addsys.Key = ClassCurUser.LogInEmplKey;

                addsys.StartPosition = FormStartPosition.CenterParent;
                addsys.WindowState = FormWindowState.Maximized;
                addsys.SysAdmin = ClassCurUser.SysAdministrator;

                if (addsys.SysAdmin == "1")
                {

                    addsys.MdiParent = this;
                    addsys.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }
        }
        private void AddEmpl()
        {
            FormAddEmpl addempl = new FormAddEmpl();
            //if (!ActivateForm(addempl))
            if (!this.CheckOpened(addempl.Name))
            {
                addempl.EmplId = ClassCurUser.LogInEmplId;
                addempl.EmplName = ClassCurUser.LogInEmplName;
                addempl.Key = ClassCurUser.LogInEmplKey;

                addempl.StartPosition = FormStartPosition.CenterParent;
                addempl.WindowState = FormWindowState.Maximized;
                addempl.SysAdmin = ClassCurUser.SysAdministrator;
                if (addempl.SysAdmin == "1")
                {

                    addempl.MdiParent = this;
                    addempl.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }
        }

        // <Shift>
        #region Shift

        private void Shift_CreateNewDoc()
        {

            //SPC_WS_OIBUSER frmSPC_WS_OIBUSER = new SPC_WS_OIBUSER();
            //if (!this.CheckOpened(frmSPC_WS_OIBUSER.Name))
            //{
            //    frmSPC_WS_OIBUSER.MdiParent = this;
            //    frmSPC_WS_OIBUSER.Show();
            //    frmSPC_WS_OIBUSER.Activate();
            //}


            //if (ClassCurUser.LogInSection == "32" || ClassCurUser.LogInSection == "322")
            //{
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Shift_Create shiftcreate = new Shift_Create();
                if (!this.CheckOpened(shiftcreate.Name))
                {
                    shiftcreate.EmplId = ClassCurUser.LogInEmplId;
                    shiftcreate.EmplName = ClassCurUser.LogInEmplName;
                    shiftcreate.Key = ClassCurUser.LogInEmplKey;


                    shiftcreate.StartPosition = FormStartPosition.CenterParent;
                    shiftcreate.WindowState = FormWindowState.Maximized;
                    shiftcreate.MdiParent = this;
                    shiftcreate.Show();
                }


                //if (!ActivateForm(shiftcreate))
                //{
                //    shiftcreate.EmplId = ClassCurUser.LogInEmplId;
                //    shiftcreate.EmplName = ClassCurUser.LogInEmplName;
                //    shiftcreate.Key = ClassCurUser.LogInEmplKey;


                //    shiftcreate.StartPosition = FormStartPosition.CenterParent;
                //    shiftcreate.WindowState = FormWindowState.Maximized;
                //    shiftcreate.MdiParent = this;
                //    shiftcreate.Show();
                //}
            }
            //}
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void Shift_EditNewDoc()
        {
            //if (ClassCurUser.LogInSection == "32" || ClassCurUser.LogInSection == "322")
            //{
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Shift_Edit shiftedit = new Shift_Edit();
                if (!this.CheckOpened(shiftedit.Name))
                {
                    shiftedit.EmplId = ClassCurUser.LogInEmplId;
                    shiftedit.EmplName = ClassCurUser.LogInEmplName;
                    shiftedit.Key = ClassCurUser.LogInEmplKey;


                    shiftedit.StartPosition = FormStartPosition.CenterParent;
                    shiftedit.WindowState = FormWindowState.Maximized;
                    shiftedit.MdiParent = this;
                    shiftedit.Show();
                }

                //HRDOCS.Shift_Edit shiftedit = new Shift_Edit();
                //if (!ActivateForm(shiftedit))
                //{
                //    shiftedit.EmplId = ClassCurUser.LogInEmplId;
                //    shiftedit.EmplName = ClassCurUser.LogInEmplName;
                //    shiftedit.Key = ClassCurUser.LogInEmplKey;

                //    shiftedit.StartPosition = FormStartPosition.CenterParent;
                //    shiftedit.WindowState = FormWindowState.Maximized;
                //    shiftedit.MdiParent = this;
                //    shiftedit.Show();
                //}
            }
            //}
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void Shift_StatusNewDoc()
        {
            //if (ClassCurUser.LogInSection == "32" || ClassCurUser.LogInSection == "322")
            //{
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {

                HRDOCS.Shift_SearchData shiftstatus = new Shift_SearchData();
                if (!this.CheckOpened(shiftstatus.Name))
                {
                    shiftstatus.StartPosition = FormStartPosition.CenterParent;
                    shiftstatus.WindowState = FormWindowState.Maximized;
                    shiftstatus.MdiParent = this;
                    shiftstatus.Show();
                }

                //HRDOCS.Shift_SearchData shiftstatus = new Shift_SearchData();
                //if (!ActivateForm(shiftstatus))
                //{
                //    shiftstatus.StartPosition = FormStartPosition.CenterParent;
                //    shiftstatus.WindowState = FormWindowState.Maximized;
                //    shiftstatus.MdiParent = this;
                //    shiftstatus.Show();
                //}
            }
            //}
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }

        }

        private void Shift_HeadApprove()
        {
            //if (ClassCurUser.LogInSection == "32" || ClassCurUser.LogInSection == "322")
            //{
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Shift_ApproveHD shiftheadapprove = new Shift_ApproveHD();

                if (!this.CheckOpened(shiftheadapprove.Name))
                {
                    shiftheadapprove.EmplId = ClassCurUser.LogInEmplId;
                    shiftheadapprove.EmplName = ClassCurUser.LogInEmplName;
                    shiftheadapprove.Key = ClassCurUser.LogInEmplKey;
                    shiftheadapprove.SysOutoffice = ClassCurUser.SysOutoffice;
                    shiftheadapprove.StartPosition = FormStartPosition.CenterParent;
                    shiftheadapprove.WindowState = FormWindowState.Maximized;

                    if (shiftheadapprove.SysOutoffice == "1")
                    {
                        shiftheadapprove.MdiParent = this;
                        shiftheadapprove.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }

                //if (!ActivateForm(shiftheadapprove))
                //{
                //    shiftheadapprove.EmplId = ClassCurUser.LogInEmplId;
                //    shiftheadapprove.EmplName = ClassCurUser.LogInEmplName;
                //    shiftheadapprove.Key = ClassCurUser.LogInEmplKey;
                //    shiftheadapprove.SysOutoffice = ClassCurUser.SysOutoffice;
                //    shiftheadapprove.StartPosition = FormStartPosition.CenterParent;
                //    shiftheadapprove.WindowState = FormWindowState.Maximized;

                //    if (shiftheadapprove.SysOutoffice == "1")
                //    {
                //        shiftheadapprove.MdiParent = this;
                //        shiftheadapprove.Show();
                //    }
                //    else
                //    {
                //        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                //    }
                //}
            }
            //}
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }

        }

        private void Shift_MN_Approve()
        {
            //if (ClassCurUser.LogInSection == "32" || ClassCurUser.LogInSection == "322")
            //{
            // if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Shift_ApproveMN ShiftMNApprove = new Shift_ApproveMN();
                //if (!ActivateForm(ShiftMNApprove))
                if (!this.CheckOpened(ShiftMNApprove.Name))
                {
                    ShiftMNApprove.EmplId = ClassCurUser.LogInEmplId;
                    ShiftMNApprove.EmplName = ClassCurUser.LogInEmplName;
                    ShiftMNApprove.Key = ClassCurUser.LogInEmplKey;
                    ShiftMNApprove.Division = ClassCurUser.LogInEmplDivision;

                    ShiftMNApprove.MNApproveOut = ClassCurUser.SysMNApproveOut;
                    ShiftMNApprove.Section = ClassCurUser.LogInSection;

                    ShiftMNApprove.StartPosition = FormStartPosition.CenterParent;
                    ShiftMNApprove.WindowState = FormWindowState.Maximized;

                    if (ShiftMNApprove.Division == "75" || ShiftMNApprove.Division == "11" || ShiftMNApprove.Division == "03"

                       || ShiftMNApprove.EmplId == "M9999999")
                    {
                        if (ShiftMNApprove.MNApproveOut == "1")
                        {
                            ShiftMNApprove.MdiParent = this;
                            ShiftMNApprove.Show();
                        }
                        else
                        {
                            MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                        }
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            //}
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }


        }

        private void Shift_HrApprove()
        {

            HRDOCS.Shift_ApproveHR shifthrapprove = new Shift_ApproveHR();
            //if (!ActivateForm(shifthrapprove))
            if (!this.CheckOpened(shifthrapprove.Name))
            {
                shifthrapprove.EmplId = ClassCurUser.LogInEmplId;
                shifthrapprove.EmplName = ClassCurUser.LogInEmplName;
                shifthrapprove.Key = ClassCurUser.LogInEmplKey;

                shifthrapprove.SysOutoffice = ClassCurUser.SysOutoffice;
                shifthrapprove.Section = ClassCurUser.LogInSection;
                shifthrapprove.HrApproveOut = ClassCurUser.SysHrApproveOut;

                shifthrapprove.StartPosition = FormStartPosition.CenterParent;
                shifthrapprove.WindowState = FormWindowState.Maximized;
                //if (HrApproveOut.Section == "02" || HrApproveOut.EmplId == "M9999999")
                //{
                if (shifthrapprove.HrApproveOut == "1")
                {
                    shifthrapprove.MdiParent = this;
                    shifthrapprove.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }




        }

        private void Shift_ReportApprove()
        {
            HRDOCS.Shift_ReportHRApprove shifreport = new Shift_ReportHRApprove();
            //if (!ActivateForm(shifreport))
            if (!this.CheckOpened(shifreport.Name))
            {
                shifreport.EmplId = ClassCurUser.LogInEmplId;
                shifreport.EmplName = ClassCurUser.LogInEmplName;
                shifreport.Key = ClassCurUser.LogInEmplKey;

                shifreport.SysOutoffice = ClassCurUser.SysOutoffice;
                shifreport.Section = ClassCurUser.LogInSection;
                shifreport.StartPosition = FormStartPosition.CenterParent;
                shifreport.WindowState = FormWindowState.Maximized;


                if (ClassCurUser.LogInSection == "02" || ClassCurUser.LogInEmplId == "M9999999")
                {
                    shifreport.MdiParent = this;
                    shifreport.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }

            }
        }

        private void Shift_ReportStatus()
        {

            HRDOCS.Shift_ReportStatusDoc shifreportstatus = new Shift_ReportStatusDoc();
            //if (!ActivateForm(shifreportstatus))
            if (!this.CheckOpened(shifreportstatus.Name))
            {
                shifreportstatus.EmplId = ClassCurUser.LogInEmplId;
                shifreportstatus.EmplName = ClassCurUser.LogInEmplName;
                shifreportstatus.Key = ClassCurUser.LogInEmplKey;

                shifreportstatus.SysOutoffice = ClassCurUser.SysOutoffice;
                shifreportstatus.Section = ClassCurUser.LogInSection;
                shifreportstatus.StartPosition = FormStartPosition.CenterParent;
                shifreportstatus.WindowState = FormWindowState.Maximized;


                if (ClassCurUser.LogInSection == "02" || ClassCurUser.LogInEmplId == "M9999999")
                {
                    shifreportstatus.MdiParent = this;
                    shifreportstatus.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }

            }

        }

        private void Shift_ReportOT()
        {
            HRDOCS.Shift_ReportOT shifreportot = new Shift_ReportOT();
            //if (!ActivateForm(shifreportot))
            if (!this.CheckOpened(shifreportot.Name))
            {
                if (ClassCurUser.LogInSection == "02" || ClassCurUser.LogInEmplId == "M9999999")
                {
                    shifreportot.MdiParent = this;
                    shifreportot.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }

            }

        }

        //bool CheckShiftLogIn()
        bool CheckLogIn()
        {
            bool ChkShiftLogIn = false;

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@"SELECT * FROM [dbo].[SPC_CM_AUTHORIZE]
                                                             WHERE APPROVEID <> '004' 
                                                             AND PWSECTION = '{0}' ", ClassCurUser.LogInSection);

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
                        ChkShiftLogIn = true;
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
            return ChkShiftLogIn;

        }

        bool CheckPayLogIn()
        {
            bool CheckPayLogIn = false;

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    //                    sqlCommand.CommandText = string.Format(@"select * from [dbo].[SPC_CM_AUTHORIZE]
                    //                                                            where APPROVEID <> '008' AND PWSECTION = '{0}' ", ClassCurUser.LogInSection);

                    sqlCommand.CommandText = string.Format(
                    @"SELECT A.EMPLID, A.APPROVEID, A.PWSECTION ,B.PWPOSITION ,B.PWDIVISION
                      FROM [SPC_CM_AUTHORIZE] A 
                        LEFT OUTER JOIN [dbo].[PWEMPLOYEE] B  ON A.EMPLID = B.PWEMPLOYEE
                      WHERE A.APPROVEID = '008' ");

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
                        CheckPayLogIn = true;
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
            return CheckPayLogIn;
        }

        #endregion
        //...............

        // <Leave>
        #region Leave

        private void Leave_CreateNewDoc()
        {
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Leave_Create leavecreate = new Leave_Create();
                if (!this.CheckOpened(leavecreate.Name))
                {
                    leavecreate.StartPosition = FormStartPosition.CenterParent;
                    leavecreate.WindowState = FormWindowState.Maximized;
                    leavecreate.MdiParent = this;
                    leavecreate.Show();
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void Leave_EditDoc()
        {
            // if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Leave_Edit LeaveEdit = new Leave_Edit();
                if (!this.CheckOpened(LeaveEdit.Name))
                {
                    LeaveEdit.StartPosition = FormStartPosition.CenterParent;
                    LeaveEdit.WindowState = FormWindowState.Maximized;
                    LeaveEdit.MdiParent = this;
                    LeaveEdit.Show();
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void Leave_StatusDoc()
        {
            // if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Leave_SearchData LeaveStatusDoc = new Leave_SearchData();
                if (!this.CheckOpened(LeaveStatusDoc.Name))
                {
                    LeaveStatusDoc.StartPosition = FormStartPosition.CenterParent;
                    LeaveStatusDoc.WindowState = FormWindowState.Maximized;
                    LeaveStatusDoc.MdiParent = this;
                    LeaveStatusDoc.Show();
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void Leave_HeadApprove()
        {
            // if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Leave_ApproveHD LeaveHeadApprove = new Leave_ApproveHD();
                if (!this.CheckOpened(LeaveHeadApprove.Name))
                {
                    LeaveHeadApprove.EmplId = ClassCurUser.LogInEmplId;
                    LeaveHeadApprove.EmplName = ClassCurUser.LogInEmplName;
                    LeaveHeadApprove.Key = ClassCurUser.LogInEmplKey;
                    LeaveHeadApprove.SysOutoffice = ClassCurUser.SysOutoffice;
                    LeaveHeadApprove.StartPosition = FormStartPosition.CenterParent;
                    LeaveHeadApprove.WindowState = FormWindowState.Maximized;
                    if (LeaveHeadApprove.SysOutoffice == "1")
                    {
                        LeaveHeadApprove.MdiParent = this;
                        LeaveHeadApprove.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void Leave_MNApprove()
        {
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Leave_ApproveMN LeaveMNApprove = new Leave_ApproveMN();
                if (!this.CheckOpened(LeaveMNApprove.Name))
                {
                    LeaveMNApprove.EmplId = ClassCurUser.LogInEmplId;
                    LeaveMNApprove.EmplName = ClassCurUser.LogInEmplName;
                    LeaveMNApprove.Key = ClassCurUser.LogInEmplKey;
                    LeaveMNApprove.Division = ClassCurUser.LogInEmplDivision;

                    LeaveMNApprove.MNApproveOut = ClassCurUser.SysMNApproveOut;
                    LeaveMNApprove.Section = ClassCurUser.LogInSection;

                    LeaveMNApprove.StartPosition = FormStartPosition.CenterParent;
                    LeaveMNApprove.WindowState = FormWindowState.Maximized;
                    if (LeaveMNApprove.Division == "75" || LeaveMNApprove.Division == "11" || LeaveMNApprove.Division == "03"

                       || LeaveMNApprove.EmplId == "M9999999")
                    {
                        if (LeaveMNApprove.MNApproveOut == "1")
                        {
                            LeaveMNApprove.MdiParent = this;
                            LeaveMNApprove.Show();
                        }
                        else
                        {
                            MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                        }
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void Leave_HRApprove()
        {
            // if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Leave_ApproveHR LeaveHRApprove = new Leave_ApproveHR();
                if (!this.CheckOpened(LeaveHRApprove.Name))
                {
                    LeaveHRApprove.EmplId = ClassCurUser.LogInEmplId;
                    LeaveHRApprove.EmplName = ClassCurUser.LogInEmplName;
                    LeaveHRApprove.Key = ClassCurUser.LogInEmplKey;

                    LeaveHRApprove.SysOutoffice = ClassCurUser.SysOutoffice;
                    LeaveHRApprove.Section = ClassCurUser.LogInSection;
                    LeaveHRApprove.HrApproveOut = ClassCurUser.SysHrApproveOut;


                    LeaveHRApprove.StartPosition = FormStartPosition.CenterParent;
                    LeaveHRApprove.WindowState = FormWindowState.Maximized;
                    if (LeaveHRApprove.HrApproveOut == "1")
                    {
                        LeaveHRApprove.MdiParent = this;
                        LeaveHRApprove.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void HRLeaveReport()
        {
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Leave_ReportHRApprove HRLeaveReport = new Leave_ReportHRApprove();
                if (!this.CheckOpened(HRLeaveReport.Name))
                {
                    HRLeaveReport.StartPosition = FormStartPosition.CenterParent;
                    HRLeaveReport.WindowState = FormWindowState.Maximized;
                    if (ClassCurUser.LogInSection == "02" || ClassCurUser.LogInEmplId == "M9999999")
                    {
                        HRLeaveReport.MdiParent = this;
                        HRLeaveReport.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void HRLeaveStatusReport()
        {
            // if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Leave_ReportStatusDoc HRLeaveStatusReport = new Leave_ReportStatusDoc();
                if (!this.CheckOpened(HRLeaveStatusReport.Name))
                {
                    HRLeaveStatusReport.StartPosition = FormStartPosition.CenterParent;
                    HRLeaveStatusReport.WindowState = FormWindowState.Maximized;
                    if (ClassCurUser.LogInSection == "02" || ClassCurUser.LogInEmplId == "M9999999")
                    {
                        HRLeaveStatusReport.MdiParent = this;
                        HRLeaveStatusReport.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void HRAllLeaveStatusReport()
        {
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Report_AllLeaveCount HRAllLeaveStatusReport = new Report_AllLeaveCount();
                if (!this.CheckOpened(HRAllLeaveStatusReport.Name))
                {
                    HRAllLeaveStatusReport.StartPosition = FormStartPosition.CenterParent;
                    HRAllLeaveStatusReport.WindowState = FormWindowState.Maximized;
                    if (ClassCurUser.LogInSection == "02" || ClassCurUser.LogInEmplId == "M9999999")
                    {
                        HRAllLeaveStatusReport.MdiParent = this;
                        HRAllLeaveStatusReport.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void AddAuthen()
        {
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Shift_Authorize AddAuthen = new Shift_Authorize();
                if (!this.CheckOpened(AddAuthen.Name))
                {
                    AddAuthen.EmplId = ClassCurUser.LogInEmplId;
                    AddAuthen.EmplName = ClassCurUser.LogInEmplName;
                    AddAuthen.Key = ClassCurUser.LogInEmplKey;
                    AddAuthen.SysAdmin = ClassCurUser.SysAdministrator;

                    AddAuthen.StartPosition = FormStartPosition.CenterParent;
                    AddAuthen.WindowState = FormWindowState.Maximized;

                    if (AddAuthen.SysAdmin == "1")
                    {

                        AddAuthen.MdiParent = this;
                        AddAuthen.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }


        #endregion
        //...............

        //<Chd>
        #region ChangHolliday
        private void CHD_CreateDoc()
        {
            //if (CheckShiftLogIn() == true)
            //if (CheckLogIn() == true)
            //{
            //    HRDOCS.Chd_Create CHD_create = new Chd_Create();

            //    if (!this.CheckOpened(CHD_create.Name))
            //    {
            //        CHD_create.StartPosition = FormStartPosition.CenterParent;
            //        CHD_create.WindowState = FormWindowState.Maximized;
            //        CHD_create.MdiParent = this;
            //        CHD_create.Show();
            //    }

            //    else
            //    {
            //        // MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            //        return;
            //    }
            //}

            if (CheckLogIn() == true)
            {
                HRDOCS.Chd_Create CHD_create = new Chd_Create();

                if (!this.CheckOpened(CHD_create.Name))
                {
                    CHD_create.StartPosition = FormStartPosition.CenterParent;
                    CHD_create.WindowState = FormWindowState.Maximized;
                    CHD_create.MdiParent = this;
                    CHD_create.Show();
                }

            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }

        }
        private void CHD_Edit()
        {

            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {

                HRDOCS.Chd_Edit CHD_edit = new Chd_Edit();
                if (!this.CheckOpened(CHD_edit.Name))
                {
                    CHD_edit.EmplId = ClassCurUser.LogInEmplId;
                    CHD_edit.EmplName = ClassCurUser.LogInEmplName;
                    CHD_edit.Key = ClassCurUser.LogInEmplKey;

                    CHD_edit.StartPosition = FormStartPosition.CenterParent;
                    CHD_edit.WindowState = FormWindowState.Maximized;
                    CHD_edit.MdiParent = this;
                    CHD_edit.Show();
                }
            }

        //}
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }
        private void CHD_DocStatust()
        {

            // if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Chd_ReportStatusDoc CHD_statusdoc = new Chd_ReportStatusDoc();

                if (!this.CheckOpened(CHD_statusdoc.Name))
                {
                    CHD_statusdoc.StartPosition = FormStartPosition.CenterParent;
                    CHD_statusdoc.WindowState = FormWindowState.Maximized;
                    CHD_statusdoc.MdiParent = this;
                    CHD_statusdoc.Show();
                }
            }

            else
            {
                // MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                return;
            }
        }
        private void Chd_HeadApprove()
        {
            //if (ClassCurUser.LogInSection == "32" || ClassCurUser.LogInSection == "322")
            //{
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {
                HRDOCS.Chd_ApproveHD Chdheadapprove = new Chd_ApproveHD();

                if (!this.CheckOpened(Chdheadapprove.Name))
                {
                    Chdheadapprove.EmplId = ClassCurUser.LogInEmplId;
                    Chdheadapprove.EmplName = ClassCurUser.LogInEmplName;
                    Chdheadapprove.Key = ClassCurUser.LogInEmplKey;
                    Chdheadapprove.SysOutoffice = ClassCurUser.SysOutoffice;
                    Chdheadapprove.StartPosition = FormStartPosition.CenterParent;
                    Chdheadapprove.WindowState = FormWindowState.Maximized;

                    if (Chdheadapprove.SysOutoffice == "1")
                    {
                        Chdheadapprove.MdiParent = this;
                        Chdheadapprove.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }

        }
        private void Chd_HRApprove()
        {
            HRDOCS.Chd_ApproveHR hrapprove = new Chd_ApproveHR();
            if (!this.CheckOpened(hrapprove.Name))
            {
                hrapprove.EmplId = ClassCurUser.LogInEmplId;
                hrapprove.EmplName = ClassCurUser.LogInEmplName;
                hrapprove.Key = ClassCurUser.LogInEmplKey;

                hrapprove.SysOutoffice = ClassCurUser.SysOutoffice;
                hrapprove.Section = ClassCurUser.LogInSection;
                hrapprove.HrApproveOut = ClassCurUser.SysHrApproveOut;

                hrapprove.StartPosition = FormStartPosition.CenterParent;
                hrapprove.WindowState = FormWindowState.Maximized;
                //if (HrApproveOut.Section == "02" || HrApproveOut.EmplId == "M9999999")
                //{
                if (hrapprove.HrApproveOut == "1")
                {
                    hrapprove.MdiParent = this;
                    hrapprove.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }

        }
        private void Chd_HRReport()
        {
            HRDOCS.Chd_ReportHRApprove reporthr = new Chd_ReportHRApprove();
            if (!this.CheckOpened(reporthr.Name))
            {
                reporthr.EmplId = ClassCurUser.LogInEmplId;
                reporthr.EmplName = ClassCurUser.LogInEmplName;
                reporthr.Key = ClassCurUser.LogInEmplKey;

                reporthr.SysOutoffice = ClassCurUser.SysOutoffice;
                reporthr.Section = ClassCurUser.LogInSection;
                reporthr.HrApproveOut = ClassCurUser.SysHrApproveOut;

                reporthr.StartPosition = FormStartPosition.CenterParent;
                reporthr.WindowState = FormWindowState.Maximized;
                //if (HrApproveOut.Section == "02" || HrApproveOut.EmplId == "M9999999")
                //{
                if (reporthr.HrApproveOut == "1")
                {
                    reporthr.MdiParent = this;
                    reporthr.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }
        }
        private void Chd_HRStatusReport()
        {
            HRDOCS.Chd_ReportHRStatusDoc hrstatusdoc = new Chd_ReportHRStatusDoc();
            if (!this.CheckOpened(hrstatusdoc.Name))
            {
                hrstatusdoc.EmplId = ClassCurUser.LogInEmplId;
                hrstatusdoc.EmplName = ClassCurUser.LogInEmplName;
                hrstatusdoc.Key = ClassCurUser.LogInEmplKey;

                hrstatusdoc.SysOutoffice = ClassCurUser.SysOutoffice;
                hrstatusdoc.Section = ClassCurUser.LogInSection;
                hrstatusdoc.HrApproveOut = ClassCurUser.SysHrApproveOut;

                hrstatusdoc.StartPosition = FormStartPosition.CenterParent;
                hrstatusdoc.WindowState = FormWindowState.Maximized;
                if (hrstatusdoc.Section == "02" || hrstatusdoc.EmplId == "M9999999")
                {
                    hrstatusdoc.MdiParent = this;
                    hrstatusdoc.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }
        }

        private void Chd_MN_Approve()
        {
            //if (ClassCurUser.LogInSection == "32" || ClassCurUser.LogInSection == "322")
            //{
            if (CheckLogIn() == true)
            {
                //HRDOCS.Shift_ApproveMN ShiftMNApprove = new Shift_ApproveMN();
                HRDOCS.Chd_ApproveMN mn_approve = new Chd_ApproveMN();

                //if (!ActivateForm(ShiftMNApprove))
                if (!this.CheckOpened(mn_approve.Name))
                {
                    mn_approve.EmplId = ClassCurUser.LogInEmplId;
                    mn_approve.EmplName = ClassCurUser.LogInEmplName;
                    mn_approve.Key = ClassCurUser.LogInEmplKey;
                    mn_approve.Division = ClassCurUser.LogInEmplDivision;

                    mn_approve.MNApproveOut = ClassCurUser.SysMNApproveOut;
                    mn_approve.Section = ClassCurUser.LogInSection;

                    mn_approve.StartPosition = FormStartPosition.CenterParent;
                    mn_approve.WindowState = FormWindowState.Maximized;

                    if (mn_approve.Division == "75" || mn_approve.Division == "11" || mn_approve.Division == "03"

                       || mn_approve.EmplId == "M9999999")
                    {
                        if (mn_approve.MNApproveOut == "1")
                        {
                            mn_approve.MdiParent = this;
                            mn_approve.Show();
                        }
                        else
                        {
                            MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                        }
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            //}
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }


        }
        #endregion
        //...............

        //<CN>
        #region CancleDoc
        private void CN_CreateDoc()
        {
            if (CheckLogIn() == true)
            {
                HRDOCS.Cancle_Create CN_create = new Cancle_Create();
                if (!this.CheckOpened(CN_create.Name))
                {
                    //CN_create.Section = ClassCurUser.LogInSection; //ทดสอบเฉพาะแผนก
                    CN_create.EmplId = ClassCurUser.LogInEmplId;
                    CN_create.StartPosition = FormStartPosition.CenterParent;
                    CN_create.WindowState = FormWindowState.Maximized;
                    
                    CN_create.MdiParent = this;
                    CN_create.Show();
                }

                else
                {
                    // MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    return;
                }
            }
        }
        private void CN_EditDoc()
        {
            //if (CheckShiftLogIn() == true)
            if (CheckLogIn() == true)
            {

                HRDOCS.Cancle_Edit CN_edit = new Cancle_Edit();
                if (!this.CheckOpened(CN_edit.Name))
                {
                    CN_edit.EmplId = ClassCurUser.LogInEmplId;
                    CN_edit.EmplName = ClassCurUser.LogInEmplName;
                    CN_edit.Key = ClassCurUser.LogInEmplKey;

                    CN_edit.StartPosition = FormStartPosition.CenterParent;
                    CN_edit.WindowState = FormWindowState.Maximized;
                    CN_edit.MdiParent = this;
                    CN_edit.Show();
                }
            }

        //}
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }
        private void CN_DocStatust()
        {
            if (CheckLogIn() == true)
            {
                HRDOCS.Cancle_ReportStatusDoc ReportStat = new Cancle_ReportStatusDoc();

                if (!this.CheckOpened(ReportStat.Name))
                {
                    ReportStat.Section = ClassCurUser.LogInSection; //ทดสอบเฉพาะแผนก
                    ReportStat.EmplId = ClassCurUser.LogInEmplId;

                    ReportStat.StartPosition = FormStartPosition.CenterParent;
                    ReportStat.WindowState = FormWindowState.Maximized;
                    
                    ReportStat.MdiParent = this;
                    ReportStat.Show();

                    //if (ReportStat.Section == "02" || ReportStat.Section == "32" || ReportStat.Section == "321" || ReportStat.Section == "322" || ReportStat.Section == "28")
                    //{
                    //    ReportStat.MdiParent = this;
                    //    ReportStat.Show();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("เมนูนี้อยู่ระหว่างทดลองใช้งานโดยสามารถใช้งานได้ในบางแผนกเท่านั้น");
                    //}
                }
                else
                {
                    // MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    return;
                }
            }
        }
        private void CN_HeadApprove()
        {
            if (CheckLogIn() == true)
            {
                HRDOCS.Cancle_ApproveHD CNheaapprove = new Cancle_ApproveHD();

                if (!this.CheckOpened(CNheaapprove.Name))
                {
                    CNheaapprove.Section = ClassCurUser.LogInSection; //ทดสอบเฉพาะแผนก
                    CNheaapprove.EmplId = ClassCurUser.LogInEmplId;
                    CNheaapprove.EmplName = ClassCurUser.LogInEmplName;
                    CNheaapprove.Key = ClassCurUser.LogInEmplKey;
                    CNheaapprove.SysOutoffice = ClassCurUser.SysOutoffice;
                    CNheaapprove.StartPosition = FormStartPosition.CenterParent;
                    CNheaapprove.WindowState = FormWindowState.Maximized;

                    if (CNheaapprove.SysOutoffice == "1")
                    {
                        //if (CNheaapprove.Section == "02" || CNheaapprove.Section == "32" || CNheaapprove.Section == "321" || CNheaapprove.Section == "322" || CNheaapprove.Section == "28")
                        //{
                            CNheaapprove.MdiParent = this;
                            CNheaapprove.Show();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("เมนูนี้อยู่ระหว่างทดลองใช้งานโดยสามารถใช้งานได้ในบางแผนกเท่านั้น");
                        //}
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        private void CN_HRApprove()
        {
            HRDOCS.Cancle_ApproveHR hrapprove = new Cancle_ApproveHR();
            if (!this.CheckOpened(hrapprove.Name))
            {
                hrapprove.EmplId = ClassCurUser.LogInEmplId;
                hrapprove.EmplName = ClassCurUser.LogInEmplName;
                hrapprove.Key = ClassCurUser.LogInEmplKey;

                hrapprove.SysOutoffice = ClassCurUser.SysOutoffice;
                hrapprove.Section = ClassCurUser.LogInSection;
                hrapprove.HrApproveOut = ClassCurUser.SysHrApproveOut;

                hrapprove.StartPosition = FormStartPosition.CenterParent;
                hrapprove.WindowState = FormWindowState.Maximized;
                //if (HrApproveOut.Section == "02" || HrApproveOut.EmplId == "M9999999")
                //{
                if (hrapprove.HrApproveOut == "1")
                {
                    hrapprove.MdiParent = this;
                    hrapprove.Show();
                }
                else
                {
                    MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                }
            }
        }
        private void HRCNReport()
        {
            if (CheckLogIn() == true)
            {
                HRDOCS.Cancle_ReportHRApprove HRCNReport = new Cancle_ReportHRApprove();
                if (!this.CheckOpened(HRCNReport.Name))
                {
                    HRCNReport.StartPosition = FormStartPosition.CenterParent;
                    HRCNReport.WindowState = FormWindowState.Maximized;
                    if (ClassCurUser.LogInSection == "02" || ClassCurUser.LogInEmplId == "M9999999")
                    {
                        HRCNReport.MdiParent = this;
                        HRCNReport.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }
        private void HRCNStatusReport()
        {
            if (CheckLogIn() == true)
            {
                HRDOCS.Cancle_ReportStatusDoc HRCNStatusReport = new Cancle_ReportStatusDoc();
                if (!this.CheckOpened(HRCNStatusReport.Name))
                {
                    HRCNStatusReport.StartPosition = FormStartPosition.CenterParent;
                    HRCNStatusReport.WindowState = FormWindowState.Maximized;
                    if (ClassCurUser.LogInSection == "02" || ClassCurUser.LogInEmplId == "M9999999")
                    {
                        HRCNStatusReport.MdiParent = this;
                        HRCNStatusReport.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }
        private void CN_MN_Approve()
        {
            //if (ClassCurUser.LogInSection == "32" || ClassCurUser.LogInSection == "322")
            //{
            if (CheckLogIn() == true)
            {
                //HRDOCS.Shift_ApproveMN ShiftMNApprove = new Shift_ApproveMN();
                HRDOCS.Cancle_ApproveMN mn_approve_cn = new Cancle_ApproveMN();

                //if (!ActivateForm(ShiftMNApprove))
                if (!this.CheckOpened(mn_approve_cn.Name))
                {
                    mn_approve_cn.EmplId = ClassCurUser.LogInEmplId;
                    mn_approve_cn.EmplName = ClassCurUser.LogInEmplName;
                    mn_approve_cn.Key = ClassCurUser.LogInEmplKey;
                    mn_approve_cn.Division = ClassCurUser.LogInEmplDivision;

                    mn_approve_cn.MNApproveOut = ClassCurUser.SysMNApproveOut;
                    mn_approve_cn.Section = ClassCurUser.LogInSection;

                    mn_approve_cn.StartPosition = FormStartPosition.CenterParent;
                    mn_approve_cn.WindowState = FormWindowState.Maximized;
                    //if (mn_approve_cn.Section == "02" || mn_approve_cn.Section == "32" || mn_approve_cn.Section == "321" || mn_approve_cn.Section == "322" || mn_approve_cn.Section == "28")
                    //{

                        if (mn_approve_cn.Division == "75" || mn_approve_cn.Division == "11" || mn_approve_cn.Division == "03"
                           || mn_approve_cn.EmplId == "M9999999")
                        {
                            if (mn_approve_cn.MNApproveOut == "1")
                            {
                                mn_approve_cn.MdiParent = this;
                                mn_approve_cn.Show();
                            }
                            else
                            {
                                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                            }
                        }
                        else
                        {
                            MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                        }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("เมนูนี้อยู่ระหว่างทดลองใช้งานโดยสามารถใช้งานได้ในบางแผนกเท่านั้น");
                    //}
                }
            }
            //}
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }

        #endregion
        //...............

        //<Payment>
        #region Payment
        private void PayCreat()
        {
            if (CheckPayLogIn() == true)
            {
                HRDOCS.Pay_Creat creat = new Pay_Creat();

                if (!this.CheckOpened(creat.Name))
                {
                    creat.EmplId = ClassCurUser.LogInEmplId;
                    creat.EmplName = ClassCurUser.LogInEmplName;
                    creat.Key = ClassCurUser.LogInEmplKey;
                    creat.Section = ClassCurUser.LogInSection;

                    creat.StartPosition = FormStartPosition.CenterParent;
                    creat.WindowState = FormWindowState.Maximized;

                    if ((creat.Section == "01") || (creat.EmplId == "M9999999"))
                    {
                        creat.MdiParent = this;
                        creat.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                // MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                return;
            }
        }

        private void PayCheck()
        {
            if (CheckPayLogIn() == true)
            {
                HRDOCS.Pay_CheckPay checkpay = new Pay_CheckPay();

                if (!this.CheckOpened(checkpay.Name))
                {
                    checkpay.EmplId = ClassCurUser.LogInEmplId;
                    checkpay.EmplName = ClassCurUser.LogInEmplName;
                    checkpay.Key = ClassCurUser.LogInEmplKey;
                    checkpay.Section = ClassCurUser.LogInSection;

                    checkpay.StartPosition = FormStartPosition.CenterParent;
                    checkpay.WindowState = FormWindowState.Maximized;

                    if ((checkpay.Section == "01") || (checkpay.EmplId == "M9999999"))
                    {
                        checkpay.MdiParent = this;
                        checkpay.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                // MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                return;
            }
        }
        private void PayCheckImportBill()
        {
            if (CheckPayLogIn() == true)
            {
                HRDOCS.Pay_CheckImportBill ImportBill = new Pay_CheckImportBill();

                if (!this.CheckOpened(ImportBill.Name))
                {
                    ImportBill.EmplId = ClassCurUser.LogInEmplId;
                    ImportBill.EmplName = ClassCurUser.LogInEmplName;
                    ImportBill.Key = ClassCurUser.LogInEmplKey;
                    ImportBill.Section = ClassCurUser.LogInSection;

                    ImportBill.StartPosition = FormStartPosition.CenterParent;
                    ImportBill.WindowState = FormWindowState.Maximized;

                    if ((ImportBill.Section == "01") || (ImportBill.EmplId == "M9999999"))
                    {
                        ImportBill.MdiParent = this;
                        ImportBill.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                // MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                return;
            }
        }
        private void PayReport()
        {
            if (CheckPayLogIn() == true)
            {
                HRDOCS.Pay_Report report = new Pay_Report();

                if (!this.CheckOpened(report.Name))
                {
                    report.EmplId = ClassCurUser.LogInEmplId;
                    report.EmplName = ClassCurUser.LogInEmplName;
                    report.Key = ClassCurUser.LogInEmplKey;
                    report.Section = ClassCurUser.LogInSection;

                    report.StartPosition = FormStartPosition.CenterParent;
                    report.WindowState = FormWindowState.Maximized;

                    if ((report.Section == "01") || (report.EmplId == "M9999999"))
                    {
                        report.MdiParent = this;
                        report.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                // MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                return;
            }
        }
        private void PayDelete()
        {
            if (CheckPayLogIn() == true)
            {
                HRDOCS.Pay_Delete report = new Pay_Delete();

                if (!this.CheckOpened(report.Name))
                {
                    report.EmplId = ClassCurUser.LogInEmplId;
                    report.EmplName = ClassCurUser.LogInEmplName;
                    report.Key = ClassCurUser.LogInEmplKey;
                    report.Section = ClassCurUser.LogInSection;

                    report.StartPosition = FormStartPosition.CenterParent;
                    report.WindowState = FormWindowState.Maximized;

                    if ((report.Section == "01") || (report.EmplId == "M9999999"))
                    {
                        report.MdiParent = this;
                        report.Show();
                    }
                    else
                    {
                        MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                    }
                }
            }
            else
            {
                // MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
                return;
            }
        }
        //private void PayVisa()
        //{
        //    HRDOCS.Pay_Diagnosis diag = new Pay_Diagnosis();
        //    if (!this.CheckOpened(diag.Name))
        //    {
        //        diag.EmplId = ClassCurUser.LogInEmplId;
        //        diag.EmplName = ClassCurUser.LogInEmplName;
        //        diag.Key = ClassCurUser.LogInEmplKey;
        //        diag.SysOutoffice = ClassCurUser.SysOutoffice;
        //        diag.Section = ClassCurUser.LogInSection;
        //        // signup.HrApproveOut = ClassCurUser.SysHrApproveOut;

        //        diag.StartPosition = FormStartPosition.CenterParent;
        //        diag.WindowState = FormWindowState.Maximized;
        //        if (diag.Section == "01" || diag.EmplId == "M9999999")
        //        {

        //            diag.MdiParent = this;
        //            diag.Show();
        //        }
        //        else
        //        {
        //            MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
        //        }
        //    }
        //}

        //private void PayReport()
        //{
        //    HRDOCS.Pay_Diagnosis diag = new Pay_Diagnosis();
        //    if (!this.CheckOpened(diag.Name))
        //    {
        //        diag.EmplId = ClassCurUser.LogInEmplId;
        //        diag.EmplName = ClassCurUser.LogInEmplName;
        //        diag.Key = ClassCurUser.LogInEmplKey;
        //        diag.SysOutoffice = ClassCurUser.SysOutoffice;
        //        diag.Section = ClassCurUser.LogInSection;
        //        // signup.HrApproveOut = ClassCurUser.SysHrApproveOut;

        //        diag.StartPosition = FormStartPosition.CenterParent;
        //        diag.WindowState = FormWindowState.Maximized;
        //        if (diag.Section == "01" || diag.EmplId == "M9999999")
        //        {

        //            diag.MdiParent = this;
        //            diag.Show();
        //        }
        //        else
        //        {
        //            MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
        //        }
        //    }
        //}
        #endregion
        //.........

        // <WS>
        bool CheckOpened(string _formName)
        {

            foreach (var childForm in this.radDock1.MdiChildren)
            {
                if (childForm.Name == _formName)
                {
                    this.setActiveDocumant(childForm);
                    return true;
                }
            }
            return false;
        }
        void setActiveDocumant(object _form)
        {
            this.radDock1.ActivateMdiChild((Form)_form);
        }

        private void radTreeViewMN_Approve_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {

        }
        // </WS>
        #endregion
    }
}
