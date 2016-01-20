namespace HROUTOFFICE
{
    partial class PWTIMETEMP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PWTIMETEMP));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.radGridTimeTemp = new Telerik.WinControls.UI.RadGridView();
            this.toolStrip1.SuspendLayout();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridTimeTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridTimeTemp.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnExportExcel});
            this.toolStrip1.Location = new System.Drawing.Point(9, 9);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(35, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripBtnExportExcel
            // 
            this.toolStripBtnExportExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnExportExcel.Image = global::HROUTOFFICE.Properties.Resources.page_excel;
            this.toolStripBtnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnExportExcel.Name = "toolStripBtnExportExcel";
            this.toolStripBtnExportExcel.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnExportExcel.Tag = "ExportExcel";
            this.toolStripBtnExportExcel.Text = "Excel";
            this.toolStripBtnExportExcel.ToolTipText = "Export to Excel (Ctrl+Alt+X)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.radGridTimeTemp);
            this.panelContainer.Location = new System.Drawing.Point(12, 88);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(560, 248);
            this.panelContainer.TabIndex = 2;
            // 
            // radGridTimeTemp
            // 
            this.radGridTimeTemp.Location = new System.Drawing.Point(3, 3);
            // 
            // radGridTimeTemp
            // 
            this.radGridTimeTemp.MasterTemplate.AllowAddNewRow = false;
            this.radGridTimeTemp.Name = "radGridTimeTemp";
            this.radGridTimeTemp.ReadOnly = true;
            this.radGridTimeTemp.Size = new System.Drawing.Size(240, 150);
            this.radGridTimeTemp.TabIndex = 0;
            this.radGridTimeTemp.Text = "radGridView1";
            this.radGridTimeTemp.ThemeName = "VisualStudio2012Light";
            // 
            // PWTIMETEMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PWTIMETEMP";
            this.Text = "PWTIMETEMP";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridTimeTemp.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridTimeTemp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnExportExcel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelContainer;
        private Telerik.WinControls.UI.RadGridView radGridTimeTemp;
    }
}