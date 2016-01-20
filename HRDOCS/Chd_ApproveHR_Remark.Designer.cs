namespace HRDOCS
{
    partial class Chd_ApproveHR_Remark
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
            this.txtHrRemark = new Telerik.WinControls.UI.RadTextBox();
            this.rbt_Confirm = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtDocId = new Telerik.WinControls.UI.RadTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHrRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbt_Confirm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocId)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.rbt_Confirm);
            this.radGroupBox1.Controls.Add(this.txtHrRemark);
            this.radGroupBox1.HeaderText = "แสดงความคิดเห็น";
            this.radGroupBox1.Location = new System.Drawing.Point(12, 30);
            this.radGroupBox1.Name = "radGroupBox1";
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(462, 160);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "แสดงความคิดเห็น";
            // 
            // txtHrRemark
            // 
            this.txtHrRemark.AutoSize = false;
            this.txtHrRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHrRemark.Location = new System.Drawing.Point(8, 21);
            this.txtHrRemark.Multiline = true;
            this.txtHrRemark.Name = "txtHrRemark";
            this.txtHrRemark.Size = new System.Drawing.Size(446, 102);
            this.txtHrRemark.TabIndex = 0;
            this.txtHrRemark.TabStop = false;
            // 
            // rbt_Confirm
            // 
            this.rbt_Confirm.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbt_Confirm.Location = new System.Drawing.Point(344, 129);
            this.rbt_Confirm.Name = "rbt_Confirm";
            this.rbt_Confirm.Size = new System.Drawing.Size(110, 24);
            this.rbt_Confirm.TabIndex = 1;
            this.rbt_Confirm.Text = "ยืนยัน";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(20, 6);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(65, 18);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "เลขที่เอกสาร";
            // 
            // txtDocId
            // 
            this.txtDocId.Location = new System.Drawing.Point(91, 6);
            this.txtDocId.Name = "txtDocId";
            this.txtDocId.ReadOnly = true;
            this.txtDocId.Size = new System.Drawing.Size(132, 20);
            this.txtDocId.TabIndex = 2;
            this.txtDocId.TabStop = false;
            // 
            // Chd_ApproveHR_Remark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 196);
            this.Controls.Add(this.txtDocId);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Chd_ApproveHR_Remark";
            this.Text = "ความคิดเห็นแผนกบุคคล";
            this.Load += new System.EventHandler(this.Chd_ApproveHR_Remark_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtHrRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbt_Confirm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadButton rbt_Confirm;
        private Telerik.WinControls.UI.RadTextBox txtHrRemark;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtDocId;
    }
}