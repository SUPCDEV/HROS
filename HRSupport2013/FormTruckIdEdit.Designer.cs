﻿namespace HROUTOFFICE
{
    partial class FormTruckIdEdit
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
            Telerik.WinControls.UI.GridViewHyperlinkColumn gridViewHyperlinkColumn3 = new Telerik.WinControls.UI.GridViewHyperlinkColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtTruckId = new Telerik.WinControls.UI.RadTextBox();
            this.radGridViewTruckId = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTruckId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewTruckId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewTruckId.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Khaki;
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.statusStrip1.Location = new System.Drawing.Point(0, 449);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(384, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(286, 399);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOk.Location = new System.Drawing.Point(198, 399);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 25);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "ตกลง";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radLabel1.Location = new System.Drawing.Point(16, 13);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(60, 18);
            this.radLabel1.TabIndex = 8;
            this.radLabel1.Text = "ทะเบียนรถ";
            // 
            // txtTruckId
            // 
            this.txtTruckId.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtTruckId.Location = new System.Drawing.Point(84, 13);
            this.txtTruckId.Margin = new System.Windows.Forms.Padding(4);
            this.txtTruckId.Name = "txtTruckId";
            this.txtTruckId.Size = new System.Drawing.Size(280, 20);
            this.txtTruckId.TabIndex = 7;
            this.txtTruckId.TabStop = false;
            this.txtTruckId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTruckId_KeyDown);
            // 
            // radGridViewTruckId
            // 
            this.radGridViewTruckId.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radGridViewTruckId.Location = new System.Drawing.Point(16, 41);
            this.radGridViewTruckId.Margin = new System.Windows.Forms.Padding(4);
            // 
            // radGridViewTruckId
            // 
            this.radGridViewTruckId.MasterTemplate.AllowAddNewRow = false;
            gridViewHyperlinkColumn3.FieldName = "VEHICLEID";
            gridViewHyperlinkColumn3.HeaderText = "ทะเบียนรถ";
            gridViewHyperlinkColumn3.HyperlinkOpenAction = Telerik.WinControls.UI.HyperlinkOpenAction.None;
            gridViewHyperlinkColumn3.Name = "VEHICLEID";
            gridViewHyperlinkColumn3.Width = 120;
            gridViewTextBoxColumn3.FieldName = "NAME";
            gridViewTextBoxColumn3.HeaderText = "ประเภทรถ";
            gridViewTextBoxColumn3.Name = "NAME";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 120;
            this.radGridViewTruckId.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewHyperlinkColumn3,
            gridViewTextBoxColumn3});
            this.radGridViewTruckId.MasterTemplate.ShowGroupedColumns = true;
            this.radGridViewTruckId.Name = "radGridViewTruckId";
            this.radGridViewTruckId.ReadOnly = true;
            this.radGridViewTruckId.ShowGroupPanel = false;
            this.radGridViewTruckId.Size = new System.Drawing.Size(350, 350);
            this.radGridViewTruckId.TabIndex = 6;
            this.radGridViewTruckId.Text = "radGridView1";
            // 
            // FormTruckIdEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 471);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.txtTruckId);
            this.Controls.Add(this.radGridViewTruckId);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormTruckIdEdit";
            this.Text = "ทะเบียนรถ";
            this.Load += new System.EventHandler(this.FormTruckIdEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTruckId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewTruckId.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewTruckId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtTruckId;
        private Telerik.WinControls.UI.RadGridView radGridViewTruckId;
    }
}