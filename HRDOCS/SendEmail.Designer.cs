namespace HRDOCS
{
    partial class SendEmail
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.Txt_Subject = new Telerik.WinControls.UI.RadTextBox();
            this.Txt_Detail = new Telerik.WinControls.UI.RadTextBox();
            this.Ddl_To = new Telerik.WinControls.UI.RadDropDownList();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radTextBox1 = new Telerik.WinControls.UI.RadTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Subject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ddl_To)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(52, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "ถึง : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(34, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "หัวข้อ : ";
            // 
            // Btn_Send
            // 
            this.Btn_Send.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Btn_Send.Location = new System.Drawing.Point(223, 408);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(88, 42);
            this.Btn_Send.TabIndex = 5;
            this.Btn_Send.Text = "Send";
            this.Btn_Send.UseVisualStyleBackColor = true;
            // 
            // Txt_Subject
            // 
            this.Txt_Subject.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Txt_Subject.Location = new System.Drawing.Point(97, 74);
            this.Txt_Subject.Name = "Txt_Subject";
            this.Txt_Subject.Size = new System.Drawing.Size(339, 25);
            this.Txt_Subject.TabIndex = 7;
            this.Txt_Subject.TabStop = false;
            // 
            // Txt_Detail
            // 
            this.Txt_Detail.AutoSize = false;
            this.Txt_Detail.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Txt_Detail.Location = new System.Drawing.Point(97, 115);
            this.Txt_Detail.Multiline = true;
            this.Txt_Detail.Name = "Txt_Detail";
            this.Txt_Detail.ReadOnly = true;
            this.Txt_Detail.Size = new System.Drawing.Size(339, 102);
            this.Txt_Detail.TabIndex = 8;
            this.Txt_Detail.TabStop = false;
            // 
            // Ddl_To
            // 
            this.Ddl_To.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Ddl_To.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Ddl_To.Location = new System.Drawing.Point(97, 36);
            this.Ddl_To.Name = "Ddl_To";
            this.Ddl_To.Size = new System.Drawing.Size(339, 27);
            this.Ddl_To.TabIndex = 10;
            this.Ddl_To.Text = "radDropDownList1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(3, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "รายละเอียด : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(22, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "เพิ่มเติม : ";
            // 
            // radTextBox1
            // 
            this.radTextBox1.AutoSize = false;
            this.radTextBox1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.radTextBox1.Location = new System.Drawing.Point(97, 237);
            this.radTextBox1.Multiline = true;
            this.radTextBox1.Name = "radTextBox1";
            this.radTextBox1.Size = new System.Drawing.Size(339, 102);
            this.radTextBox1.TabIndex = 12;
            this.radTextBox1.TabStop = false;
            // 
            // SendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.radTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Ddl_To);
            this.Controls.Add(this.Txt_Detail);
            this.Controls.Add(this.Txt_Subject);
            this.Controls.Add(this.Btn_Send);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SendEmail";
            this.Text = "SendEmail";
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Subject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ddl_To)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Btn_Send;
        private Telerik.WinControls.UI.RadTextBox Txt_Subject;
        private Telerik.WinControls.UI.RadTextBox Txt_Detail;
        private Telerik.WinControls.UI.RadDropDownList Ddl_To;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Telerik.WinControls.UI.RadTextBox radTextBox1;
    }
}