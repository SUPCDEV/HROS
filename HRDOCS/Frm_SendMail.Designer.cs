namespace HRDOCS
{
    partial class Frm_SendMail
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
            this.radButton3 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.radTxtOterMessage = new Telerik.WinControls.UI.RadTextBox();
            this.btn_SendMail = new Telerik.WinControls.UI.RadButton();
            this.txt_CCTo = new Telerik.WinControls.UI.RadTextBox();
            this.txt_To = new Telerik.WinControls.UI.RadTextBox();
            this.autoCompleteBox = new Telerik.WinControls.UI.RadAutoCompleteBox();
            this.ddl_Employee = new Telerik.WinControls.UI.RadDropDownList();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTxtOterMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_SendMail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CCTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_To)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoCompleteBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Employee)).BeginInit();
            this.SuspendLayout();
            // 
            // radButton3
            // 
            this.radButton3.Location = new System.Drawing.Point(79, 38);
            this.radButton3.Name = "radButton3";
            this.radButton3.Size = new System.Drawing.Size(49, 20);
            this.radButton3.TabIndex = 8;
            this.radButton3.Text = "CC To";
            // 
            // radButton2
            // 
            this.radButton2.Location = new System.Drawing.Point(79, 12);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(49, 20);
            this.radButton2.TabIndex = 10;
            this.radButton2.Text = "To";
            // 
            // radTxtOterMessage
            // 
            this.radTxtOterMessage.AutoSize = false;
            this.radTxtOterMessage.Location = new System.Drawing.Point(12, 77);
            this.radTxtOterMessage.Multiline = true;
            this.radTxtOterMessage.Name = "radTxtOterMessage";
            this.radTxtOterMessage.Size = new System.Drawing.Size(499, 134);
            this.radTxtOterMessage.TabIndex = 9;
            this.radTxtOterMessage.TabStop = false;
            this.radTxtOterMessage.Text = "Other Message.....";
            // 
            // btn_SendMail
            // 
            this.btn_SendMail.Location = new System.Drawing.Point(12, 12);
            this.btn_SendMail.Name = "btn_SendMail";
            this.btn_SendMail.Size = new System.Drawing.Size(61, 46);
            this.btn_SendMail.TabIndex = 7;
            this.btn_SendMail.Text = "Send";
            this.btn_SendMail.Click += new System.EventHandler(this.btn_SendMail_Click);
            // 
            // txt_CCTo
            // 
            this.txt_CCTo.Location = new System.Drawing.Point(134, 38);
            this.txt_CCTo.Name = "txt_CCTo";
            this.txt_CCTo.Size = new System.Drawing.Size(377, 20);
            this.txt_CCTo.TabIndex = 6;
            this.txt_CCTo.TabStop = false;
            // 
            // txt_To
            // 
            this.txt_To.Location = new System.Drawing.Point(134, 12);
            this.txt_To.Name = "txt_To";
            this.txt_To.Size = new System.Drawing.Size(377, 20);
            this.txt_To.TabIndex = 5;
            this.txt_To.TabStop = false;
            // 
            // autoCompleteBox
            // 
            this.autoCompleteBox.Location = new System.Drawing.Point(13, 227);
            this.autoCompleteBox.Name = "autoCompleteBox";
            this.autoCompleteBox.Size = new System.Drawing.Size(498, 26);
            this.autoCompleteBox.TabIndex = 11;
            // 
            // ddl_Employee
            // 
            this.ddl_Employee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddl_Employee.Location = new System.Drawing.Point(13, 282);
            this.ddl_Employee.Name = "ddl_Employee";
            this.ddl_Employee.Size = new System.Drawing.Size(498, 22);
            this.ddl_Employee.TabIndex = 12;
            this.ddl_Employee.Text = "radDropDownList1";
            this.ddl_Employee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ddl_Employee_KeyPress);
            // 
            // Frm_SendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 342);
            this.Controls.Add(this.ddl_Employee);
            this.Controls.Add(this.autoCompleteBox);
            this.Controls.Add(this.radButton3);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radTxtOterMessage);
            this.Controls.Add(this.btn_SendMail);
            this.Controls.Add(this.txt_CCTo);
            this.Controls.Add(this.txt_To);
            this.Name = "Frm_SendMail";
            this.Text = "Frm_SendMail";
            this.Load += new System.EventHandler(this.Frm_SendMail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTxtOterMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_SendMail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CCTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_To)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoCompleteBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Employee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButton3;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadTextBox radTxtOterMessage;
        private Telerik.WinControls.UI.RadButton btn_SendMail;
        private Telerik.WinControls.UI.RadTextBox txt_CCTo;
        private Telerik.WinControls.UI.RadTextBox txt_To;
        private Telerik.WinControls.UI.RadAutoCompleteBox autoCompleteBox;
        private Telerik.WinControls.UI.RadDropDownList ddl_Employee;
    }
}