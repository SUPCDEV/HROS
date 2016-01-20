namespace HROUTOFFICE
{
    partial class MDIParent1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemHROS_ONLINE = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNewCreated = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemHeadDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemHeadApprove = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemHRDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemHrApprove = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOut = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemComeback = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemReportHrOutType = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemReport = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemStatusDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemChangPass = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemHROS_ONLINE,
            this.ToolStripMenuItemHeadDoc,
            this.ToolStripMenuItemHRDoc,
            this.ToolStripMenuItemReport,
            this.ToolStripMenuItemChangPass});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(872, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // ToolStripMenuItemHROS_ONLINE
            // 
            this.ToolStripMenuItemHROS_ONLINE.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemNewCreated,
            this.ToolStripMenuItemEdit,
            this.ToolStripMenuItemDelete,
            this.ToolStripMenuItemLogOut});
            this.ToolStripMenuItemHROS_ONLINE.Name = "ToolStripMenuItemHROS_ONLINE";
            this.ToolStripMenuItemHROS_ONLINE.Size = new System.Drawing.Size(93, 20);
            this.ToolStripMenuItemHROS_ONLINE.Text = "ออกนอกออนไลน์";
            // 
            // ToolStripMenuItemNewCreated
            // 
            this.ToolStripMenuItemNewCreated.Name = "ToolStripMenuItemNewCreated";
            this.ToolStripMenuItemNewCreated.Size = new System.Drawing.Size(134, 22);
            this.ToolStripMenuItemNewCreated.Text = "สร้าง";
            this.ToolStripMenuItemNewCreated.Click += new System.EventHandler(this.ToolStripMenuItemOutOffice_Click);
            // 
            // ToolStripMenuItemEdit
            // 
            this.ToolStripMenuItemEdit.Name = "ToolStripMenuItemEdit";
            this.ToolStripMenuItemEdit.Size = new System.Drawing.Size(134, 22);
            this.ToolStripMenuItemEdit.Text = "แก้ไข";
            this.ToolStripMenuItemEdit.Click += new System.EventHandler(this.ToolStripMenuItemEdit_Click);
            // 
            // ToolStripMenuItemDelete
            // 
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            this.ToolStripMenuItemDelete.Size = new System.Drawing.Size(134, 22);
            this.ToolStripMenuItemDelete.Text = "ยกเลิก";
            this.ToolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
            // 
            // ToolStripMenuItemLogOut
            // 
            this.ToolStripMenuItemLogOut.Name = "ToolStripMenuItemLogOut";
            this.ToolStripMenuItemLogOut.Size = new System.Drawing.Size(134, 22);
            this.ToolStripMenuItemLogOut.Text = "ออกจากระบบ";
            this.ToolStripMenuItemLogOut.Click += new System.EventHandler(this.ToolStripMenuItemLogOut_Click);
            // 
            // ToolStripMenuItemHeadDoc
            // 
            this.ToolStripMenuItemHeadDoc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemHeadApprove});
            this.ToolStripMenuItemHeadDoc.Name = "ToolStripMenuItemHeadDoc";
            this.ToolStripMenuItemHeadDoc.Size = new System.Drawing.Size(104, 20);
            this.ToolStripMenuItemHeadDoc.Text = "เอกสารหัวหน้า/ผช.";
            // 
            // ToolStripMenuItemHeadApprove
            // 
            this.ToolStripMenuItemHeadApprove.Name = "ToolStripMenuItemHeadApprove";
            this.ToolStripMenuItemHeadApprove.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItemHeadApprove.Text = "ใบออกนอกออนไลน์";
            this.ToolStripMenuItemHeadApprove.Click += new System.EventHandler(this.ToolStripMenuItemHeadApprove_Click);
            // 
            // ToolStripMenuItemHRDoc
            // 
            this.ToolStripMenuItemHRDoc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemHrApprove,
            this.ToolStripMenuItemReportHrOutType});
            this.ToolStripMenuItemHRDoc.Name = "ToolStripMenuItemHRDoc";
            this.ToolStripMenuItemHRDoc.Size = new System.Drawing.Size(105, 20);
            this.ToolStripMenuItemHRDoc.Text = "เอกสารแผนกบุคคล";
            // 
            // ToolStripMenuItemHrApprove
            // 
            this.ToolStripMenuItemHrApprove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOut,
            this.ToolStripMenuItemComeback});
            this.ToolStripMenuItemHrApprove.Name = "ToolStripMenuItemHrApprove";
            this.ToolStripMenuItemHrApprove.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItemHrApprove.Text = "ใบออกนอกออนไลน์";
            // 
            // ToolStripMenuItemOut
            // 
            this.ToolStripMenuItemOut.Name = "ToolStripMenuItemOut";
            this.ToolStripMenuItemOut.Size = new System.Drawing.Size(123, 22);
            this.ToolStripMenuItemOut.Text = "ลงเวลาออก";
            this.ToolStripMenuItemOut.Click += new System.EventHandler(this.ToolStripMenuItemOut_Click);
            // 
            // ToolStripMenuItemComeback
            // 
            this.ToolStripMenuItemComeback.Name = "ToolStripMenuItemComeback";
            this.ToolStripMenuItemComeback.Size = new System.Drawing.Size(123, 22);
            this.ToolStripMenuItemComeback.Text = "ลงเวลาเข้า";
            this.ToolStripMenuItemComeback.Click += new System.EventHandler(this.ToolStripMenuItemComeback_Click);
            // 
            // ToolStripMenuItemReportHrOutType
            // 
            this.ToolStripMenuItemReportHrOutType.Name = "ToolStripMenuItemReportHrOutType";
            this.ToolStripMenuItemReportHrOutType.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItemReportHrOutType.Text = "รายงาน";
            this.ToolStripMenuItemReportHrOutType.Click += new System.EventHandler(this.ToolStripMenuItemReportHrOutType_Click);
            // 
            // ToolStripMenuItemReport
            // 
            this.ToolStripMenuItemReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemStatusDoc});
            this.ToolStripMenuItemReport.Name = "ToolStripMenuItemReport";
            this.ToolStripMenuItemReport.Size = new System.Drawing.Size(52, 20);
            this.ToolStripMenuItemReport.Text = "รายงาน";
            // 
            // ToolStripMenuItemStatusDoc
            // 
            this.ToolStripMenuItemStatusDoc.Name = "ToolStripMenuItemStatusDoc";
            this.ToolStripMenuItemStatusDoc.Size = new System.Drawing.Size(135, 22);
            this.ToolStripMenuItemStatusDoc.Text = "สถานะเอกสาร";
            this.ToolStripMenuItemStatusDoc.Click += new System.EventHandler(this.ToolStripMenuItemStatusDoc_Click);
            // 
            // ToolStripMenuItemChangPass
            // 
            this.ToolStripMenuItemChangPass.Name = "ToolStripMenuItemChangPass";
            this.ToolStripMenuItemChangPass.Size = new System.Drawing.Size(85, 20);
            this.ToolStripMenuItemChangPass.Text = "เปลี่ยนรหัสผ่าน";
            this.ToolStripMenuItemChangPass.Click += new System.EventHandler(this.ToolStripMenuItemChangPass_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.statusLabelUserName});
            this.statusStrip.Location = new System.Drawing.Point(0, 466);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(872, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // statusLabelUserName
            // 
            this.statusLabelUserName.Name = "statusLabelUserName";
            this.statusLabelUserName.Size = new System.Drawing.Size(69, 17);
            this.statusLabelUserName.Text = "LoginName";
            // 
            // MDIParent1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 488);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MDIParent1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เอกสารออนไลน์";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHeadDoc;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHRDoc;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemReport;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHROS_ONLINE;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNewCreated;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHeadApprove;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHrApprove;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOut;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemComeback;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemChangPass;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStatusDoc;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemLogOut;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelUserName;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemReportHrOutType;
    }
}



