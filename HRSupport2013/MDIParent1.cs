using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SysApp;

namespace HROUTOFFICE
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
            this.Load += new EventHandler(MDIParent1_Load);
            this.Shown += new EventHandler(MDIParent1_Shown);
        }

        void MDIParent1_Shown(object sender, EventArgs e)
        {
            if (SysApp.ClassCurUser.LogInEmplId == null || string.IsNullOrEmpty(ClassCurUser.LogInEmplId.ToString()))
            {
                using (FormLogIn frmLogIn = new FormLogIn()) 
                {
                    frmLogIn.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == frmLogIn.ShowDialog(this))
                    {
                        this.statusLabelUserName.Text = ClassCurUser.LogInEmplName;
                    }
                    else
                    {
                        this.statusLabelUserName.Text = "";
                        this.Close();
                    }
                    
                }
            }
        }

        void MDIParent1_Load(object sender, EventArgs e)
        {
            //UForm.WindowState = FormWindowState.Maximized; 
        }

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


        #region function
        public void CreatedNewDoc()
        { 
            FormCreateNew frm3 = new FormCreateNew();
            frm3.EmplId = ClassCurUser.LogInEmplId;
            frm3.EmplName = ClassCurUser.LogInEmplName;
            frm3.Key = ClassCurUser.LogInEmplKey;

            frm3.StartPosition = FormStartPosition.CenterParent;


            frm3.MdiParent = this;
            frm3.Show();
        }
        public void HeadApprove()
        {

            FormHdApprove HeadApprove = new FormHdApprove();
            HeadApprove.EmplId = ClassCurUser.LogInEmplId;
            HeadApprove.EmplName = ClassCurUser.LogInEmplName;
            HeadApprove.Key = ClassCurUser.LogInEmplKey;
            HeadApprove.SysOutoffice = ClassCurUser.SysOutoffice;
            HeadApprove.StartPosition = FormStartPosition.CenterParent;
            
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
        public void HrApproveOut()
        {
            FormHrApproveOut HrApproveOut = new FormHrApproveOut();
            HrApproveOut.EmplId = ClassCurUser.LogInEmplId;
            HrApproveOut.EmplName = ClassCurUser.LogInEmplName;
            HrApproveOut.Key = ClassCurUser.LogInEmplKey;

            HrApproveOut.SysOutoffice = ClassCurUser.SysOutoffice;
            HrApproveOut.Section = ClassCurUser.LogInSection;
            HrApproveOut.StartPosition = FormStartPosition.CenterParent;

            if (HrApproveOut.Section == "02" && HrApproveOut.SysOutoffice == "2" || HrApproveOut.SysOutoffice == "1")
            {
                HrApproveOut.MdiParent = this;
                HrApproveOut.Show();
            }
            else
            {

                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }
        public void HrApproveIn()
        {
            FormHrApprovedIn HrApproveIn = new FormHrApprovedIn();
            HrApproveIn.EmplId = ClassCurUser.LogInEmplId;
            HrApproveIn.EmplName = ClassCurUser.LogInEmplName;
            HrApproveIn.Key = ClassCurUser.LogInEmplKey;

            HrApproveIn.SysOutoffice = ClassCurUser.SysOutoffice;
            HrApproveIn.Section = ClassCurUser.LogInSection;
            HrApproveIn.StartPosition = FormStartPosition.CenterParent;

            if (HrApproveIn.Section == "02" && HrApproveIn.SysOutoffice == "2" || HrApproveIn.SysOutoffice == "1")
            {
                HrApproveIn.MdiParent = this;
                HrApproveIn.Show();
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }  
        }
        public void ReportHrOutType()
        {
            FormReportHROut ReportHrOutType = new FormReportHROut();
            ReportHrOutType.EmplId = ClassCurUser.LogInEmplId;
            ReportHrOutType.EmplName = ClassCurUser.LogInEmplName;
            ReportHrOutType.Key = ClassCurUser.LogInEmplKey;

            ReportHrOutType.SysOutoffice = ClassCurUser.SysOutoffice;
            ReportHrOutType.Section = ClassCurUser.LogInSection;
            ReportHrOutType.StartPosition = FormStartPosition.CenterParent;


            if (ReportHrOutType.Section == "02" && ReportHrOutType.SysOutoffice == "2" || ReportHrOutType.SysOutoffice == "1")
            {
                ReportHrOutType.MdiParent = this;
                ReportHrOutType.Show();
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิเข้าถึงหน้านี้ได้");
            }
        }
        public void EditDoc()
        {
            FormEmplEdit EditData = new FormEmplEdit();
            EditData.EmplId = ClassCurUser.LogInEmplId;
            EditData.EmplName = ClassCurUser.LogInEmplName;
            EditData.Key = ClassCurUser.LogInEmplKey;

            EditData.StartPosition = FormStartPosition.CenterParent;

            EditData.MdiParent = this;
            EditData.Show();
        }

        private void DeleteDoc()
        {
            FormDeleteEmpl DeleteData = new FormDeleteEmpl();
            DeleteData.EmplId = ClassCurUser.LogInEmplId;
            DeleteData.EmplName = ClassCurUser.LogInEmplName;
            DeleteData.Key = ClassCurUser.LogInEmplKey;

            DeleteData.StartPosition = FormStartPosition.CenterParent;

            DeleteData.MdiParent = this;
            DeleteData.Show();
        }
        private void ReportStatusDoc()
        {
            FormReportStatusDoc ReportEmpl = new FormReportStatusDoc();
            ReportEmpl.EmplId = ClassCurUser.LogInEmplId;
            ReportEmpl.EmplName = ClassCurUser.LogInEmplName;
            ReportEmpl.Key = ClassCurUser.LogInEmplKey;

            ReportEmpl.StartPosition = FormStartPosition.CenterParent;

            ReportEmpl.MdiParent = this;
            ReportEmpl.Show();
        }

        private void LogOut()
        {
           this.Close();
        }
        #endregion

        #region EventClick

        private void ToolStripMenuItemOutOffice_Click(object sender, EventArgs e)
        {
            CreatedNewDoc();
        }
        private void ToolStripMenuItemHeadApprove_Click(object sender, EventArgs e)
        {
            HeadApprove();
        }

        private void ToolStripMenuItemStatusDoc_Click(object sender, EventArgs e)
        {

            ReportStatusDoc();
        }

        private void ToolStripMenuItemOut_Click(object sender, EventArgs e)
        {
            HrApproveOut();
        }

        private void ToolStripMenuItemComeback_Click(object sender, EventArgs e)
        {
            HrApproveIn();
        }
        private void ToolStripMenuItemLogOut_Click(object sender, EventArgs e)
        {
            LogOut();
        }
        

        private void ToolStripMenuItemChangPass_Click(object sender, EventArgs e)
        {
            FormCreateNewPassword frmChangPass = new FormCreateNewPassword();
            frmChangPass.EmplId = ClassCurUser.LogInEmplId;
            frmChangPass.EmplName = ClassCurUser.LogInEmplName;
            frmChangPass.Key = ClassCurUser.LogInEmplKey;
            frmChangPass.StartPosition = FormStartPosition.CenterParent;
           // frmChangPass.MdiParent = this;
            frmChangPass.ShowDialog(this);
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            DeleteDoc();
        }

        private void ToolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            EditDoc();
        }
       #endregion

        private void ToolStripMenuItemReportHrOutType_Click(object sender, EventArgs e)
        {
            this.ReportHrOutType();
        }

    }
}
