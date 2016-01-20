namespace HRDOCS
{
    partial class SetEmail
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
            this.Ddl_Email = new Telerik.WinControls.UI.RadDropDownList();
            this.Btn_Submit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Ddl_Email)).BeginInit();
            this.SuspendLayout();
            // 
            // Ddl_Email
            // 
            this.Ddl_Email.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Ddl_Email.Location = new System.Drawing.Point(143, 51);
            this.Ddl_Email.Name = "Ddl_Email";
            this.Ddl_Email.Size = new System.Drawing.Size(299, 25);
            this.Ddl_Email.TabIndex = 0;
            this.Ddl_Email.Text = "radDropDownList1";
            // 
            // Btn_Submit
            // 
            this.Btn_Submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Btn_Submit.Location = new System.Drawing.Point(182, 135);
            this.Btn_Submit.Name = "Btn_Submit";
            this.Btn_Submit.Size = new System.Drawing.Size(103, 41);
            this.Btn_Submit.TabIndex = 1;
            this.Btn_Submit.Text = "ตกลง";
            this.Btn_Submit.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "เลือกอีเมลล์ผู้ใช้งาน : ";
            // 
            // SetEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 219);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_Submit);
            this.Controls.Add(this.Ddl_Email);
            this.Name = "SetEmail";
            this.Text = "SetEmail";
            ((System.ComponentModel.ISupportInitialize)(this.Ddl_Email)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList Ddl_Email;
        private System.Windows.Forms.Button Btn_Submit;
        private System.Windows.Forms.Label label1;
    }
}