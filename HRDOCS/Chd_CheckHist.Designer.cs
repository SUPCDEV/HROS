namespace HRDOCS
{
    partial class Chd_CheckHist
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
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.rgv_CheckHist = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_CheckHist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_CheckHist.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.Controls.Add(this.rgv_CheckHist);
            this.radGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGroupBox2.HeaderText = "แสตดงข้อมูล";
            this.radGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.radGroupBox2.Name = "radGroupBox2";
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox2.Size = new System.Drawing.Size(701, 524);
            this.radGroupBox2.TabIndex = 4;
            this.radGroupBox2.Text = "แสตดงข้อมูล";
            // 
            // rgv_CheckHist
            // 
            this.rgv_CheckHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgv_CheckHist.Location = new System.Drawing.Point(2, 18);
            this.rgv_CheckHist.Name = "rgv_CheckHist";
            this.rgv_CheckHist.Size = new System.Drawing.Size(697, 504);
            this.rgv_CheckHist.TabIndex = 0;
            this.rgv_CheckHist.Text = "radGridView1";
            // 
            // Chd_CheckHist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 524);
            this.Controls.Add(this.radGroupBox2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Chd_CheckHist";
            this.Text = "ตรวจสอบ : เฉพาะเอกสารที่ทำเปลี่ยนวันหยุดออนไลน์";
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgv_CheckHist.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_CheckHist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadGridView rgv_CheckHist;
    }
}