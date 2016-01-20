namespace HRDOCS
{
    partial class Pay_Menu
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
            this.lbl_workpermit = new Telerik.WinControls.UI.RadLabel();
            this.lbl_visa = new Telerik.WinControls.UI.RadLabel();
            this.lbl_Diagnosis = new Telerik.WinControls.UI.RadLabel();
            this.lbl_Report = new Telerik.WinControls.UI.RadLabel();
            this.lbl_SignUp = new Telerik.WinControls.UI.RadLabel();
            this.lbl_Creat = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_workpermit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_visa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_Diagnosis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_Report)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_SignUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_Creat)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_workpermit
            // 
            this.lbl_workpermit.Location = new System.Drawing.Point(23, 79);
            this.lbl_workpermit.Name = "lbl_workpermit";
            this.lbl_workpermit.Size = new System.Drawing.Size(76, 18);
            this.lbl_workpermit.TabIndex = 0;
            this.lbl_workpermit.Text = "เช็คการจ่ายเงิน";
            this.lbl_workpermit.Click += new System.EventHandler(this.lbl_workpermit_Click);
            // 
            // lbl_visa
            // 
            this.lbl_visa.Location = new System.Drawing.Point(23, 114);
            this.lbl_visa.Name = "lbl_visa";
            this.lbl_visa.Size = new System.Drawing.Size(38, 18);
            this.lbl_visa.TabIndex = 1;
            this.lbl_visa.Text = "ต่อวีซ่า";
            // 
            // lbl_Diagnosis
            // 
            this.lbl_Diagnosis.Location = new System.Drawing.Point(23, 154);
            this.lbl_Diagnosis.Name = "lbl_Diagnosis";
            this.lbl_Diagnosis.Size = new System.Drawing.Size(49, 18);
            this.lbl_Diagnosis.TabIndex = 2;
            this.lbl_Diagnosis.Text = "ยกเลิกบิล";
            this.lbl_Diagnosis.Click += new System.EventHandler(this.lbl_Diagnosis_Click);
            // 
            // lbl_Report
            // 
            this.lbl_Report.Location = new System.Drawing.Point(23, 195);
            this.lbl_Report.Name = "lbl_Report";
            this.lbl_Report.Size = new System.Drawing.Size(42, 18);
            this.lbl_Report.TabIndex = 3;
            this.lbl_Report.Text = "รายงาน";
            this.lbl_Report.Click += new System.EventHandler(this.lbl_Report_Click);
            // 
            // lbl_SignUp
            // 
            this.lbl_SignUp.Location = new System.Drawing.Point(23, 41);
            this.lbl_SignUp.Name = "lbl_SignUp";
            this.lbl_SignUp.Size = new System.Drawing.Size(54, 18);
            this.lbl_SignUp.TabIndex = 4;
            this.lbl_SignUp.Text = "send mail";
            this.lbl_SignUp.Click += new System.EventHandler(this.lbl_SignUp_Click);
            // 
            // lbl_Creat
            // 
            this.lbl_Creat.Location = new System.Drawing.Point(23, 231);
            this.lbl_Creat.Name = "lbl_Creat";
            this.lbl_Creat.Size = new System.Drawing.Size(29, 18);
            this.lbl_Creat.TabIndex = 5;
            this.lbl_Creat.Text = "สร้าง";
            this.lbl_Creat.Click += new System.EventHandler(this.lbl_Creat_Click);
            // 
            // Pay_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lbl_Creat);
            this.Controls.Add(this.lbl_SignUp);
            this.Controls.Add(this.lbl_Report);
            this.Controls.Add(this.lbl_Diagnosis);
            this.Controls.Add(this.lbl_visa);
            this.Controls.Add(this.lbl_workpermit);
            this.Name = "Pay_Menu";
            this.Text = "รายการ";
            ((System.ComponentModel.ISupportInitialize)(this.lbl_workpermit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_visa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_Diagnosis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_Report)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_SignUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_Creat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lbl_workpermit;
        private Telerik.WinControls.UI.RadLabel lbl_visa;
        private Telerik.WinControls.UI.RadLabel lbl_Diagnosis;
        private Telerik.WinControls.UI.RadLabel lbl_Report;
        private Telerik.WinControls.UI.RadLabel lbl_SignUp;
        private Telerik.WinControls.UI.RadLabel lbl_Creat;
    }
}