namespace HRDOCS
{
    partial class Cancle_DetailCHD
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cancle_DetailCHD));
            this.rgv_StatusDoc = new Telerik.WinControls.UI.RadGridView();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.txtShiftDesc = new Telerik.WinControls.UI.RadTextBox();
            this.txtShift = new Telerik.WinControls.UI.RadTextBox();
            this.btnSearch = new Telerik.WinControls.UI.RadButton();
            this.txt_Remark = new Telerik.WinControls.UI.RadTextBox();
            this.tbn_Save = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_StatusDoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_StatusDoc.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Remark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbn_Save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // rgv_StatusDoc
            // 
            this.rgv_StatusDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgv_StatusDoc.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.rgv_StatusDoc.Location = new System.Drawing.Point(0, 0);
            // 
            // rgv_StatusDoc
            // 
            this.rgv_StatusDoc.MasterTemplate.AllowAddNewRow = false;
            gridViewCheckBoxColumn1.HeaderText = "";
            gridViewCheckBoxColumn1.Name = "CHECK";
            gridViewTextBoxColumn1.FieldName = "DOCID";
            gridViewTextBoxColumn1.HeaderText = "เลขที่เอกสาร";
            gridViewTextBoxColumn1.Name = "DOCID";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn2.FieldName = "EMPLID";
            gridViewTextBoxColumn2.HeaderText = "รหัสพนักงาน";
            gridViewTextBoxColumn2.Name = "EMPLID";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn3.FieldName = "EMPLNAME";
            gridViewTextBoxColumn3.HeaderText = "ชื่อ-สกุล";
            gridViewTextBoxColumn3.Name = "EMPLNAME";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn4.FieldName = "FROMHOLIDAY";
            gridViewTextBoxColumn4.HeaderText = "วันที่มาทำ";
            gridViewTextBoxColumn4.Name = "FROMHOLIDAY";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn5.FieldName = "TOSHIFTID";
            gridViewTextBoxColumn5.HeaderText = "กะที่มาทำ";
            gridViewTextBoxColumn5.Name = "TOSHIFTID";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn6.FieldName = "TOSHIFTDESC";
            gridViewTextBoxColumn6.HeaderText = "คำอธิบาย";
            gridViewTextBoxColumn6.Name = "TOSHIFTDESC";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn7.FieldName = "TOHOLIDAY";
            gridViewTextBoxColumn7.HeaderText = "วันที่หยุด";
            gridViewTextBoxColumn7.Name = "TOHOLIDAY";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn8.FieldName = "REASON";
            gridViewTextBoxColumn8.HeaderText = "เหตุผล";
            gridViewTextBoxColumn8.Name = "REASON";
            gridViewTextBoxColumn8.ReadOnly = true;
            gridViewTextBoxColumn9.FieldName = "HEADAPPROVED";
            gridViewTextBoxColumn9.HeaderText = "หน./ผช.";
            gridViewTextBoxColumn9.Name = "HEADAPPROVED";
            gridViewTextBoxColumn9.ReadOnly = true;
            gridViewTextBoxColumn10.FieldName = "HEADAPPROVEDBYNAME";
            gridViewTextBoxColumn10.HeaderText = "ผู้อนุมัติ";
            gridViewTextBoxColumn10.Name = "HEADAPPROVEDBYNAME";
            gridViewTextBoxColumn10.ReadOnly = true;
            gridViewTextBoxColumn11.FieldName = "HRAPPROVED";
            gridViewTextBoxColumn11.HeaderText = "บุคคล";
            gridViewTextBoxColumn11.Name = "HRAPPROVED";
            gridViewTextBoxColumn11.ReadOnly = true;
            gridViewTextBoxColumn12.FieldName = "HRAPPROVEDBYNAME";
            gridViewTextBoxColumn12.HeaderText = "ผู้อนุมัติ";
            gridViewTextBoxColumn12.Name = "HRAPPROVEDBYNAME";
            gridViewTextBoxColumn12.ReadOnly = true;
            gridViewTextBoxColumn13.FieldName = "DOCSTAT";
            gridViewTextBoxColumn13.HeaderText = "สถานะ";
            gridViewTextBoxColumn13.Name = "DOCSTAT";
            gridViewTextBoxColumn13.ReadOnly = true;
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
            gridViewTextBoxColumn10,
            gridViewTextBoxColumn11,
            gridViewTextBoxColumn12,
            gridViewTextBoxColumn13});
            this.rgv_StatusDoc.Name = "rgv_StatusDoc";
            this.rgv_StatusDoc.Size = new System.Drawing.Size(784, 194);
            this.rgv_StatusDoc.TabIndex = 3;
            this.rgv_StatusDoc.Text = "radGridView1";
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
            this.radSplitContainer1.TabIndex = 4;
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
            this.splitPanel1.Size = new System.Drawing.Size(784, 194);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.251282F, -0.1517056F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(189, -84);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.radLabel2);
            this.splitPanel2.Controls.Add(this.txtShiftDesc);
            this.splitPanel2.Controls.Add(this.txtShift);
            this.splitPanel2.Controls.Add(this.btnSearch);
            this.splitPanel2.Controls.Add(this.txt_Remark);
            this.splitPanel2.Controls.Add(this.tbn_Save);
            this.splitPanel2.Controls.Add(this.radLabel1);
            this.splitPanel2.Location = new System.Drawing.Point(0, 198);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(784, 363);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.251282F, 0.1517056F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(-189, 84);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Underline);
            this.radLabel2.Location = new System.Drawing.Point(41, 32);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(134, 19);
            this.radLabel2.TabIndex = 13;
            this.radLabel2.Text = "กะเดิมวันที่ต้องการหยุด";
            // 
            // txtShiftDesc
            // 
            this.txtShiftDesc.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtShiftDesc.Location = new System.Drawing.Point(262, 32);
            this.txtShiftDesc.Name = "txtShiftDesc";
            this.txtShiftDesc.ReadOnly = true;
            this.txtShiftDesc.Size = new System.Drawing.Size(145, 21);
            this.txtShiftDesc.TabIndex = 11;
            this.txtShiftDesc.TabStop = false;
            // 
            // txtShift
            // 
            this.txtShift.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtShift.Location = new System.Drawing.Point(185, 32);
            this.txtShift.Name = "txtShift";
            this.txtShift.ReadOnly = true;
            this.txtShift.Size = new System.Drawing.Size(71, 21);
            this.txtShift.TabIndex = 10;
            this.txtShift.TabStop = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSearch.Location = new System.Drawing.Point(413, 34);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 19);
            this.btnSearch.TabIndex = 12;
            // 
            // txt_Remark
            // 
            this.txt_Remark.AutoSize = false;
            this.txt_Remark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Remark.Location = new System.Drawing.Point(41, 83);
            this.txt_Remark.Multiline = true;
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.Size = new System.Drawing.Size(411, 85);
            this.txt_Remark.TabIndex = 6;
            this.txt_Remark.TabStop = false;
            // 
            // tbn_Save
            // 
            this.tbn_Save.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbn_Save.Location = new System.Drawing.Point(342, 185);
            this.tbn_Save.Name = "tbn_Save";
            this.tbn_Save.Size = new System.Drawing.Size(110, 24);
            this.tbn_Save.TabIndex = 5;
            this.tbn_Save.Text = "บันทึก";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(41, 58);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(100, 19);
            this.radLabel1.TabIndex = 4;
            this.radLabel1.Text = "สาเหตุการยกเลิก";
            // 
            // Cancle_DetailCHD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "Cancle_DetailCHD";
            this.Text = "ยกเลิกเอกสารเปลี่ยนวันหยุดออนไลน์";
            ((System.ComponentModel.ISupportInitialize)(this.rgv_StatusDoc.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_StatusDoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.splitPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Remark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbn_Save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView rgv_StatusDoc;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.RadTextBox txt_Remark;
        private Telerik.WinControls.UI.RadButton tbn_Save;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox txtShiftDesc;
        private Telerik.WinControls.UI.RadTextBox txtShift;
        private Telerik.WinControls.UI.RadButton btnSearch;

    }
}