namespace HROUTOFFICE
{
    partial class FormDeleteEmpl
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewHyperlinkColumn gridViewHyperlinkColumn1 = new Telerik.WinControls.UI.GridViewHyperlinkColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusF5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.GridViewShowData = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewShowData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewShowData.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.statusStrip1);
            this.radPanel1.Controls.Add(this.radGroupBox1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(784, 561);
            this.radPanel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Khaki;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage,
            this.statusF5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusMessage
            // 
            this.statusMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(646, 17);
            this.statusMessage.Spring = true;
            this.statusMessage.Text = "toolStripStatusLabel1";
            this.statusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusF5
            // 
            this.statusF5.Name = "statusF5";
            this.statusF5.Size = new System.Drawing.Size(118, 17);
            this.statusF5.Text = "toolStripStatusLabel2";
            this.statusF5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.GridViewShowData);
            this.radGroupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox1.HeaderText = "แสดงข้อมูล";
            this.radGroupBox1.Location = new System.Drawing.Point(7, 4);
            this.radGroupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(764, 520);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "แสดงข้อมูล";
            // 
            // GridViewShowData
            // 
            this.GridViewShowData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewShowData.Location = new System.Drawing.Point(16, 26);
            this.GridViewShowData.Margin = new System.Windows.Forms.Padding(4);
            // 
            // GridViewShowData
            // 
            gridViewTextBoxColumn1.FieldName = "DocId";
            gridViewTextBoxColumn1.HeaderText = "เลขที่เอกสาร";
            gridViewTextBoxColumn1.Name = "DocId";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewHyperlinkColumn1.FieldName = "OutOfficeId";
            gridViewHyperlinkColumn1.HeaderText = "ลำดับ";
            gridViewHyperlinkColumn1.Name = "OutOfficeId";
            gridViewTextBoxColumn2.FieldName = "EmplId";
            gridViewTextBoxColumn2.HeaderText = "รหัสพนักงาน";
            gridViewTextBoxColumn2.Name = "EmplId";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn3.FieldName = "EmplFullName";
            gridViewTextBoxColumn3.HeaderText = "ชื่อ-สกุล";
            gridViewTextBoxColumn3.Name = "EmplFullName";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn4.FieldName = "Dimention";
            gridViewTextBoxColumn4.HeaderText = "แผนก";
            gridViewTextBoxColumn4.Name = "Dimention";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn5.FieldName = "Dept";
            gridViewTextBoxColumn5.HeaderText = "ตำแหน่ง";
            gridViewTextBoxColumn5.Name = "Dept";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn6.FieldName = "OutType";
            gridViewTextBoxColumn6.HeaderText = "ออกนอก";
            gridViewTextBoxColumn6.Name = "OutType";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn7.FieldName = "CreatedName";
            gridViewTextBoxColumn7.HeaderText = "สร้างโดย";
            gridViewTextBoxColumn7.Name = "CreatedName";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn8.FieldName = "HeadApproved";
            gridViewTextBoxColumn8.HeaderText = "หน./ผช.";
            gridViewTextBoxColumn8.Name = "HeadApproved";
            gridViewTextBoxColumn8.ReadOnly = true;
            gridViewTextBoxColumn9.FieldName = "HeadApprovedName";
            gridViewTextBoxColumn9.HeaderText = "ผู้อนุมัติ";
            gridViewTextBoxColumn9.Name = "HeadApprovedName";
            gridViewTextBoxColumn9.ReadOnly = true;
            gridViewCommandColumn1.HeaderText = "ยกเลิก";
            gridViewCommandColumn1.Name = "Delete";
            this.GridViewShowData.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewHyperlinkColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewCommandColumn1});
            this.GridViewShowData.MasterTemplate.ShowGroupedColumns = true;
            this.GridViewShowData.Name = "GridViewShowData";
            this.GridViewShowData.ShowGroupPanel = false;
            this.GridViewShowData.Size = new System.Drawing.Size(739, 488);
            this.GridViewShowData.TabIndex = 1;
            this.GridViewShowData.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GridViewShowData_CellClick);
            this.GridViewShowData.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.GridViewShowData_CellFormatting);
            this.GridViewShowData.CommandCellClick += new Telerik.WinControls.UI.CommandCellClickEventHandler(this.GridViewShowData_CommandCellClick);
            // 
            // FormDeleteEmpl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.radPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormDeleteEmpl";
            this.Text = "ลบเอกสารออกนอกออนไลน์";
            this.Load += new System.EventHandler(this.FormReportEmpl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewShowData.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewShowData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGridView GridViewShowData;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.ToolStripStatusLabel statusF5;
    }
}