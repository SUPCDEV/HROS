namespace HRDOCS
{
    partial class Cancle_Edit
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
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.rgv_HD = new Telerik.WinControls.UI.RadGridView();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.rgv_DLDT = new Telerik.WinControls.UI.RadGridView();
            this.rgv_CHDDT = new Telerik.WinControls.UI.RadGridView();
            this.rgv_DSDT = new Telerik.WinControls.UI.RadGridView();
            this.btn_Edit = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_HD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_HD.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_DLDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_DLDT.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_CHDDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_CHDDT.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_DSDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_DSDT.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Edit)).BeginInit();
            this.SuspendLayout();
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitPanel2);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radSplitContainer1.Size = new System.Drawing.Size(784, 561);
            this.radSplitContainer1.SplitterWidth = 4;
            this.radSplitContainer1.TabIndex = 0;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.radGroupBox2);
            this.splitPanel1.Controls.Add(this.radGroupBox1);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(634, 561);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.3126649F, 0F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(237, 0);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.rgv_HD);
            this.radGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radGroupBox1.HeaderText = "รายการ";
            this.radGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.radGroupBox1.Name = "radGroupBox1";
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(634, 216);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "รายการ";
            // 
            // rgv_HD
            // 
            this.rgv_HD.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.rgv_HD.Location = new System.Drawing.Point(48, 19);
            this.rgv_HD.Name = "rgv_HD";
            this.rgv_HD.Size = new System.Drawing.Size(240, 150);
            this.rgv_HD.TabIndex = 1;
            this.rgv_HD.Text = "radGridView1";
            this.rgv_HD.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.rgv_HD_CellClick);
            //this.rgv_HD.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.rgv_HD_CellFormatting);
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.btn_Edit);
            this.splitPanel2.Location = new System.Drawing.Point(638, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(146, 561);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.Controls.Add(this.rgv_DSDT);
            this.radGroupBox2.Controls.Add(this.rgv_CHDDT);
            this.radGroupBox2.Controls.Add(this.rgv_DLDT);
            this.radGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGroupBox2.HeaderText = "แสดงรายละเอียด";
            this.radGroupBox2.Location = new System.Drawing.Point(0, 216);
            this.radGroupBox2.Name = "radGroupBox2";
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox2.Size = new System.Drawing.Size(634, 345);
            this.radGroupBox2.TabIndex = 1;
            this.radGroupBox2.Text = "แสดงรายละเอียด";
            // 
            // rgv_DLDT
            // 
            this.rgv_DLDT.Location = new System.Drawing.Point(13, 22);
            this.rgv_DLDT.Name = "rgv_DLDT";
            this.rgv_DLDT.Size = new System.Drawing.Size(240, 150);
            this.rgv_DLDT.TabIndex = 0;
            this.rgv_DLDT.Text = "radGridView1";
            this.rgv_DLDT.Visible = false;
            // 
            // rgv_CHDDT
            // 
            this.rgv_CHDDT.Location = new System.Drawing.Point(275, 22);
            this.rgv_CHDDT.Name = "rgv_CHDDT";
            this.rgv_CHDDT.Size = new System.Drawing.Size(240, 150);
            this.rgv_CHDDT.TabIndex = 1;
            this.rgv_CHDDT.Text = "radGridView1";
            this.rgv_CHDDT.Visible = false;
            // 
            // rgv_DSDT
            // 
            this.rgv_DSDT.Location = new System.Drawing.Point(13, 183);
            this.rgv_DSDT.Name = "rgv_DSDT";
            this.rgv_DSDT.Size = new System.Drawing.Size(240, 150);
            this.rgv_DSDT.TabIndex = 2;
            this.rgv_DSDT.Text = "radGridView2";
            this.rgv_DSDT.Visible = false;
            // 
            // btn_Edit
            // 
            this.btn_Edit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Edit.Location = new System.Drawing.Point(19, 19);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(110, 24);
            this.btn_Edit.TabIndex = 0;
            this.btn_Edit.Text = "แก้ไข";
            // 
            // Cancle_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "Cancle_Edit";
            this.Text = "Cancle_Edit";
            this.Load += new System.EventHandler(this.Cancle_Edit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgv_HD.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_HD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgv_DLDT.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_DLDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_CHDDT.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_CHDDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_DSDT.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_DSDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Edit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGridView rgv_HD;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadGridView rgv_DLDT;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.RadGridView rgv_DSDT;
        private Telerik.WinControls.UI.RadGridView rgv_CHDDT;
        private Telerik.WinControls.UI.RadButton btn_Edit;
    }
}