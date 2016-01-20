namespace HRDOCS
{
    partial class Cancle_EditDetaiDL
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
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.rgv_StatusDoc = new Telerik.WinControls.UI.RadGridView();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.txt_Remark = new Telerik.WinControls.UI.RadTextBox();
            this.btn_Save = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_StatusDoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_StatusDoc.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Remark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitPanel2);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
            this.radSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
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
            this.splitPanel1.Controls.Add(this.rgv_StatusDoc);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(784, 193);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.1542056F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -83);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // rgv_StatusDoc
            // 
            this.rgv_StatusDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgv_StatusDoc.Location = new System.Drawing.Point(0, 0);
            // 
            // rgv_StatusDoc
            // 
            gridViewCheckBoxColumn1.FieldName = "CHECK";
            gridViewCheckBoxColumn1.HeaderText = "";
            gridViewCheckBoxColumn1.Name = "CHECK";
            gridViewTextBoxColumn1.FieldName = "DLDOCNO";
            gridViewTextBoxColumn1.HeaderText = "เลขที่เอกสาร";
            gridViewTextBoxColumn1.Name = "DLDOCNO";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn2.FieldName = "EMPLID";
            gridViewTextBoxColumn2.HeaderText = "รหัสพนักงาน";
            gridViewTextBoxColumn2.Name = "EMPLID";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn3.FieldName = "EMPLNAME";
            gridViewTextBoxColumn3.HeaderText = "ชื่อ - สกุล";
            gridViewTextBoxColumn3.Name = "EMPLNAME";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn4.FieldName = "LEAVEDATE";
            gridViewTextBoxColumn4.HeaderText = "วันที่ลาหยุด";
            gridViewTextBoxColumn4.Name = "LEAVEDATE";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn5.FieldName = "LEAVETYPEDETAIL";
            gridViewTextBoxColumn5.HeaderText = "ประเภทการลา";
            gridViewTextBoxColumn5.Name = "LEAVETYPEDETAIL";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn6.FieldName = "ATTACH";
            gridViewTextBoxColumn6.HeaderText = "ใบรับรองแพทย์";
            gridViewTextBoxColumn6.Name = "ATTACH";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn7.FieldName = "HALFDAY";
            gridViewTextBoxColumn7.HeaderText = "จำนวนวัน";
            gridViewTextBoxColumn7.Name = "HALFDAY";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn8.FieldName = "HALFDAYTIME1";
            gridViewTextBoxColumn8.HeaderText = "ตั้งแต่เวลา";
            gridViewTextBoxColumn8.Name = "HALFDAYTIME1";
            gridViewTextBoxColumn8.ReadOnly = true;
            gridViewTextBoxColumn9.FieldName = "HALFDAYTIME2";
            gridViewTextBoxColumn9.HeaderText = "ถึงเวลา";
            gridViewTextBoxColumn9.Name = "HALFDAYTIME2";
            gridViewTextBoxColumn9.ReadOnly = true;
            gridViewTextBoxColumn10.FieldName = "LEAVEREMARK";
            gridViewTextBoxColumn10.HeaderText = "หมายเหตุ";
            gridViewTextBoxColumn10.Name = "LEAVEREMARK";
            gridViewTextBoxColumn10.ReadOnly = true;
            this.rgv_StatusDoc.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCheckBoxColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10});
            this.rgv_StatusDoc.Name = "rgv_StatusDoc";
            this.rgv_StatusDoc.Size = new System.Drawing.Size(784, 193);
            this.rgv_StatusDoc.TabIndex = 1;
            this.rgv_StatusDoc.Text = "radGridView1";
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.txt_Remark);
            this.splitPanel2.Controls.Add(this.btn_Save);
            this.splitPanel2.Controls.Add(this.radLabel1);
            this.splitPanel2.Location = new System.Drawing.Point(0, 197);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(784, 364);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.1542056F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 83);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // txt_Remark
            // 
            this.txt_Remark.AutoSize = false;
            this.txt_Remark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Remark.Location = new System.Drawing.Point(34, 61);
            this.txt_Remark.Multiline = true;
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.Size = new System.Drawing.Size(411, 85);
            this.txt_Remark.TabIndex = 6;
            this.txt_Remark.TabStop = false;
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(335, 163);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(110, 24);
            this.btn_Save.TabIndex = 5;
            this.btn_Save.Text = "แก้ไข";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(34, 36);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(100, 19);
            this.radLabel1.TabIndex = 4;
            this.radLabel1.Text = "สาเหตุการยกเลิก";
            // 
            // Cancle_EditDetaiDL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "Cancle_EditDetaiDL";
            this.Text = "Cancle_EditDetaiDL";
            this.Load += new System.EventHandler(this.Cancle_EditDetaiDL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgv_StatusDoc.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_StatusDoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.splitPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Remark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.RadGridView rgv_StatusDoc;
        private Telerik.WinControls.UI.RadTextBox txt_Remark;
        private Telerik.WinControls.UI.RadButton btn_Save;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}