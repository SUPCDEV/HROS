namespace HRDOCS
{
    partial class Chd_CheckEmpHoliday
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
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.txt_empid = new Telerik.WinControls.UI.RadTextBox();
            this.lbl_empid = new Telerik.WinControls.UI.RadLabel();
            this.rgv_empholiday = new Telerik.WinControls.UI.RadGridView();
            this.txt_year = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.lbl_empname = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.lbl_section = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_empid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_empid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_empholiday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_empholiday.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_empname)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_section)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.lbl_section);
            this.radGroupBox1.Controls.Add(this.radLabel3);
            this.radGroupBox1.Controls.Add(this.lbl_empname);
            this.radGroupBox1.Controls.Add(this.radLabel1);
            this.radGroupBox1.Controls.Add(this.label1);
            this.radGroupBox1.Controls.Add(this.txt_year);
            this.radGroupBox1.Controls.Add(this.lbl_empid);
            this.radGroupBox1.Controls.Add(this.txt_empid);
            this.radGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radGroupBox1.HeaderText = "";
            this.radGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.radGroupBox1.Name = "radGroupBox1";
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(784, 136);
            this.radGroupBox1.TabIndex = 0;
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.Controls.Add(this.rgv_empholiday);
            this.radGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGroupBox2.HeaderText = "";
            this.radGroupBox2.Location = new System.Drawing.Point(0, 136);
            this.radGroupBox2.Name = "radGroupBox2";
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox2.Size = new System.Drawing.Size(784, 426);
            this.radGroupBox2.TabIndex = 1;
            // 
            // txt_empid
            // 
            this.txt_empid.Location = new System.Drawing.Point(94, 43);
            this.txt_empid.Name = "txt_empid";
            this.txt_empid.Size = new System.Drawing.Size(100, 20);
            this.txt_empid.TabIndex = 0;
            this.txt_empid.TabStop = false;
            this.txt_empid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_empid_KeyPress);
            // 
            // lbl_empid
            // 
            this.lbl_empid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_empid.Location = new System.Drawing.Point(11, 44);
            this.lbl_empid.Name = "lbl_empid";
            this.lbl_empid.Size = new System.Drawing.Size(71, 18);
            this.lbl_empid.TabIndex = 1;
            this.lbl_empid.Text = "รหัสพนักงาน";
            // 
            // rgv_empholiday
            // 
            this.rgv_empholiday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgv_empholiday.Location = new System.Drawing.Point(2, 18);
            this.rgv_empholiday.Name = "rgv_empholiday";
            this.rgv_empholiday.Size = new System.Drawing.Size(780, 406);
            this.rgv_empholiday.TabIndex = 0;
            this.rgv_empholiday.Text = "radGridView1";
            // 
            // txt_year
            // 
            this.txt_year.Location = new System.Drawing.Point(94, 12);
            this.txt_year.Name = "txt_year";
            this.txt_year.Size = new System.Drawing.Size(65, 20);
            this.txt_year.TabIndex = 2;
            this.txt_year.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "ปี";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(30, 78);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(49, 18);
            this.radLabel1.TabIndex = 4;
            this.radLabel1.Text = "ชื่อ-สกุล";
            // 
            // lbl_empname
            // 
            this.lbl_empname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_empname.Location = new System.Drawing.Point(94, 78);
            this.lbl_empname.Name = "lbl_empname";
            this.lbl_empname.Size = new System.Drawing.Size(11, 18);
            this.lbl_empname.TabIndex = 5;
            this.lbl_empname.Text = "-";
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(42, 102);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(37, 18);
            this.radLabel3.TabIndex = 6;
            this.radLabel3.Text = "แผนก";
            // 
            // lbl_section
            // 
            this.lbl_section.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_section.Location = new System.Drawing.Point(94, 102);
            this.lbl_section.Name = "lbl_section";
            this.lbl_section.Size = new System.Drawing.Size(11, 18);
            this.lbl_section.TabIndex = 7;
            this.lbl_section.Text = "-";
            // 
            // Chd_CheckEmpHoliday
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.radGroupBox2);
            this.Controls.Add(this.radGroupBox1);
            this.Name = "Chd_CheckEmpHoliday";
            this.Text = "ตรวจสอบวันหยุดประเพณี";
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_empid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_empid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_empholiday.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_empholiday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_empname)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_section)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadLabel lbl_empid;
        private Telerik.WinControls.UI.RadTextBox txt_empid;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadGridView rgv_empholiday;
        private Telerik.WinControls.UI.RadTextBox txt_year;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadLabel lbl_section;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel lbl_empname;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}