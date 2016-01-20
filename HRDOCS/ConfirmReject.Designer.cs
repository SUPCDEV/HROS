namespace HRDOCS
{
    partial class ConfirmReject
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
            this.Txt_Remark = new Telerik.WinControls.UI.RadTextBoxControl();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.Btn_Confirm = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Remark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Confirm)).BeginInit();
            this.SuspendLayout();
            // 
            // Txt_Remark
            // 
            this.Txt_Remark.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Txt_Remark.Location = new System.Drawing.Point(76, 47);
            this.Txt_Remark.Name = "Txt_Remark";
            this.Txt_Remark.Size = new System.Drawing.Size(300, 20);
            this.Txt_Remark.TabIndex = 37;
            // 
            // radLabel6
            // 
            this.radLabel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radLabel6.Location = new System.Drawing.Point(3, 47);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(67, 18);
            this.radLabel6.TabIndex = 38;
            this.radLabel6.Text = "หมายเหตุ : ";
            // 
            // Btn_Confirm
            // 
            this.Btn_Confirm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Btn_Confirm.Location = new System.Drawing.Point(156, 99);
            this.Btn_Confirm.Name = "Btn_Confirm";
            this.Btn_Confirm.Size = new System.Drawing.Size(100, 25);
            this.Btn_Confirm.TabIndex = 39;
            this.Btn_Confirm.Text = "ตกลง";
            // 
            // ConfirmReject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.ControlBox = false;
            this.Controls.Add(this.Btn_Confirm);
            this.Controls.Add(this.radLabel6);
            this.Controls.Add(this.Txt_Remark);
            this.Name = "ConfirmReject";
            this.Text = "ConfirmReject";
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Remark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Confirm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBoxControl Txt_Remark;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadButton Btn_Confirm;
    }
}