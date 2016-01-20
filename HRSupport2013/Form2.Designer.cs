namespace HROUTOFFICE
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.panelContButtom = new System.Windows.Forms.Panel();
            this.radGridReport = new Telerik.WinControls.UI.RadGridView();
            this.panelContTop = new System.Windows.Forms.Panel();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.btnFuncProcess = new System.Windows.Forms.Button();
            this.dateTimePickerTransdate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.btnViewTimeTemp = new System.Windows.Forms.Button();
            this.toolStripMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            this.panelRight = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panelContButtom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridReport.MasterTemplate)).BeginInit();
            this.panelContTop.SuspendLayout();
            this.groupBoxGeneral.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContButtom
            // 
            this.panelContButtom.Controls.Add(this.radGridReport);
            this.panelContButtom.Location = new System.Drawing.Point(3, 61);
            this.panelContButtom.Name = "panelContButtom";
            this.panelContButtom.Size = new System.Drawing.Size(647, 329);
            this.panelContButtom.TabIndex = 2;
            // 
            // radGridReport
            // 
            this.radGridReport.Location = new System.Drawing.Point(3, 3);
            // 
            // radGridReport
            // 
            this.radGridReport.MasterTemplate.AllowAddNewRow = false;
            this.radGridReport.Name = "radGridReport";
            this.radGridReport.ReadOnly = true;
            this.radGridReport.Size = new System.Drawing.Size(139, 98);
            this.radGridReport.TabIndex = 0;
            this.radGridReport.Text = "radGridView1";
            this.radGridReport.ThemeName = "VisualStudio2012Light";
            // 
            // panelContTop
            // 
            this.panelContTop.Controls.Add(this.groupBoxGeneral);
            this.panelContTop.Location = new System.Drawing.Point(3, 3);
            this.panelContTop.Name = "panelContTop";
            this.panelContTop.Size = new System.Drawing.Size(647, 52);
            this.panelContTop.TabIndex = 1;
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Controls.Add(this.btnFuncProcess);
            this.groupBoxGeneral.Controls.Add(this.dateTimePickerTransdate);
            this.groupBoxGeneral.Controls.Add(this.label1);
            this.groupBoxGeneral.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Size = new System.Drawing.Size(348, 46);
            this.groupBoxGeneral.TabIndex = 0;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "ทั่วไป";
            // 
            // btnFuncProcess
            // 
            this.btnFuncProcess.Location = new System.Drawing.Point(213, 11);
            this.btnFuncProcess.Name = "btnFuncProcess";
            this.btnFuncProcess.Size = new System.Drawing.Size(75, 23);
            this.btnFuncProcess.TabIndex = 2;
            this.btnFuncProcess.Text = "ประมวลผล";
            this.btnFuncProcess.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerTransdate
            // 
            this.dateTimePickerTransdate.Location = new System.Drawing.Point(90, 13);
            this.dateTimePickerTransdate.Name = "dateTimePickerTransdate";
            this.dateTimePickerTransdate.Size = new System.Drawing.Size(117, 20);
            this.dateTimePickerTransdate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "วันที่ประมวลผล";
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.panelContButtom);
            this.panelContainer.Controls.Add(this.panelContTop);
            this.panelContainer.Location = new System.Drawing.Point(12, 63);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(664, 423);
            this.panelContainer.TabIndex = 9;
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(577, 3);
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(577, 28);
            this.toolStripContainer1.TabIndex = 6;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnExportExcel});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(35, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripBtnExportExcel
            // 
            this.toolStripBtnExportExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnExportExcel.Image = global::HROUTOFFICE.Properties.Resources.page_excel;
            this.toolStripBtnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnExportExcel.Name = "toolStripBtnExportExcel";
            this.toolStripBtnExportExcel.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnExportExcel.Text = "Excel";
            // 
            // btnViewTimeTemp
            // 
            this.btnViewTimeTemp.Location = new System.Drawing.Point(12, 17);
            this.btnViewTimeTemp.Name = "btnViewTimeTemp";
            this.btnViewTimeTemp.Size = new System.Drawing.Size(75, 23);
            this.btnViewTimeTemp.TabIndex = 0;
            this.btnViewTimeTemp.Tag = "ViewTimeTemp";
            this.btnViewTimeTemp.Text = "ดูพฤติกรรม";
            this.btnViewTimeTemp.UseVisualStyleBackColor = true;
            // 
            // toolStripMenuFile
            // 
            this.toolStripMenuFile.Name = "toolStripMenuFile";
            this.toolStripMenuFile.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuFile.Tag = "File";
            this.toolStripMenuFile.Text = "File";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.btnViewTimeTemp);
            this.panelRight.Location = new System.Drawing.Point(682, 63);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(90, 423);
            this.panelRight.TabIndex = 8;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 489);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.panelContButtom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridReport.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridReport)).EndInit();
            this.panelContTop.ResumeLayout(false);
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            this.panelContainer.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.Windows.Forms.Panel panelContButtom;
        private Telerik.WinControls.UI.RadGridView radGridReport;
        private System.Windows.Forms.Panel panelContTop;
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.Button btnFuncProcess;
        private System.Windows.Forms.DateTimePicker dateTimePickerTransdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnExportExcel;
        private System.Windows.Forms.Button btnViewTimeTemp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFile;
        private Telerik.WinControls.RadThemeManager radThemeManager1;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;

    }
}
