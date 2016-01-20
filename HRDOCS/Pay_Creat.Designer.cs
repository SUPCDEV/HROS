namespace HRDOCS
{
    partial class Pay_Creat
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
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.rgv_Payment = new Telerik.WinControls.UI.RadGridView();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.blb_Payment = new Telerik.WinControls.UI.RadLabel();
            this.ddl_Paymenttype = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtEmplName = new Telerik.WinControls.UI.RadTextBox();
            this.txtEmpId = new Telerik.WinControls.UI.RadTextBox();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.btn_Delete = new Telerik.WinControls.UI.RadButton();
            this.btn_Newdoc = new Telerik.WinControls.UI.RadButton();
            this.btn_AddData = new Telerik.WinControls.UI.RadButton();
            this.btn_Save = new Telerik.WinControls.UI.RadButton();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_Payment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_Payment.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blb_Payment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Paymenttype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmplName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Newdoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_AddData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Save)).BeginInit();
            this.SuspendLayout();
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitPanel2);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
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
            this.splitPanel1.Controls.Add(this.radGroupBox2);
            this.splitPanel1.Controls.Add(this.radGroupBox1);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(607, 561);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.2783641F, 0.08258259F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(266, -91);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.Controls.Add(this.rgv_Payment);
            this.radGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGroupBox2.HeaderText = "";
            this.radGroupBox2.Location = new System.Drawing.Point(0, 158);
            this.radGroupBox2.Name = "radGroupBox2";
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox2.Size = new System.Drawing.Size(607, 403);
            this.radGroupBox2.TabIndex = 1;
            // 
            // rgv_Payment
            // 
            this.rgv_Payment.BackColor = System.Drawing.SystemColors.Control;
            this.rgv_Payment.Cursor = System.Windows.Forms.Cursors.Default;
            this.rgv_Payment.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rgv_Payment.ForeColor = System.Drawing.Color.Black;
            this.rgv_Payment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rgv_Payment.Location = new System.Drawing.Point(116, 98);
            this.rgv_Payment.Name = "rgv_Payment";
            this.rgv_Payment.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rgv_Payment.ShowGroupPanel = false;
            this.rgv_Payment.Size = new System.Drawing.Size(240, 150);
            this.rgv_Payment.TabIndex = 0;
            this.rgv_Payment.Text = "radGridView1";
            this.rgv_Payment.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.rgv_Payment_CellFormatting);
            this.rgv_Payment.CommandCellClick += new Telerik.WinControls.UI.CommandCellClickEventHandler(this.rgv_Payment_CommandCellClick);
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.blb_Payment);
            this.radGroupBox1.Controls.Add(this.ddl_Paymenttype);
            this.radGroupBox1.Controls.Add(this.radLabel1);
            this.radGroupBox1.Controls.Add(this.txtEmplName);
            this.radGroupBox1.Controls.Add(this.txtEmpId);
            this.radGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radGroupBox1.HeaderText = "";
            this.radGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.radGroupBox1.Name = "radGroupBox1";
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(607, 158);
            this.radGroupBox1.TabIndex = 0;
            // 
            // blb_Payment
            // 
            this.blb_Payment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blb_Payment.Location = new System.Drawing.Point(91, 84);
            this.blb_Payment.Name = "blb_Payment";
            this.blb_Payment.Size = new System.Drawing.Size(47, 19);
            this.blb_Payment.TabIndex = 17;
            this.blb_Payment.Text = "รายการ";
            // 
            // ddl_Paymenttype
            // 
            this.ddl_Paymenttype.DefaultItemsCountInDropDown = 15;
            this.ddl_Paymenttype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddl_Paymenttype.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.ddl_Paymenttype.Location = new System.Drawing.Point(144, 82);
            this.ddl_Paymenttype.Name = "ddl_Paymenttype";
            this.ddl_Paymenttype.Size = new System.Drawing.Size(291, 21);
            this.ddl_Paymenttype.TabIndex = 16;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(61, 45);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(77, 19);
            this.radLabel1.TabIndex = 10;
            this.radLabel1.Text = "รหัสพนักงาน";
            // 
            // txtEmplName
            // 
            this.txtEmplName.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtEmplName.Location = new System.Drawing.Point(242, 43);
            this.txtEmplName.Name = "txtEmplName";
            this.txtEmplName.ReadOnly = true;
            this.txtEmplName.Size = new System.Drawing.Size(193, 21);
            this.txtEmplName.TabIndex = 9;
            this.txtEmplName.TabStop = false;
            // 
            // txtEmpId
            // 
            this.txtEmpId.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtEmpId.Location = new System.Drawing.Point(144, 43);
            this.txtEmpId.Name = "txtEmpId";
            this.txtEmpId.Size = new System.Drawing.Size(92, 21);
            this.txtEmpId.TabIndex = 8;
            this.txtEmpId.TabStop = false;
            this.txtEmpId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpId_KeyPress);
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.btn_Delete);
            this.splitPanel2.Controls.Add(this.btn_Newdoc);
            this.splitPanel2.Controls.Add(this.btn_AddData);
            this.splitPanel2.Controls.Add(this.btn_Save);
            this.splitPanel2.Location = new System.Drawing.Point(611, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(173, 561);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.2783641F, -0.08258259F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(-266, 91);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(35, 121);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(110, 24);
            this.btn_Delete.TabIndex = 3;
            this.btn_Delete.Text = "ยกเลิกเอกสาร";
            // 
            // btn_Newdoc
            // 
            this.btn_Newdoc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Newdoc.Location = new System.Drawing.Point(35, 162);
            this.btn_Newdoc.Name = "btn_Newdoc";
            this.btn_Newdoc.Size = new System.Drawing.Size(110, 24);
            this.btn_Newdoc.TabIndex = 2;
            this.btn_Newdoc.Text = "สร้างเอกสารใหม่";
            // 
            // btn_AddData
            // 
            this.btn_AddData.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddData.Location = new System.Drawing.Point(35, 40);
            this.btn_AddData.Name = "btn_AddData";
            this.btn_AddData.Size = new System.Drawing.Size(110, 24);
            this.btn_AddData.TabIndex = 0;
            this.btn_AddData.Text = "เพิ่มข้อมูล";
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(35, 79);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(110, 24);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "บันทึก";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // Pay_Creat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "Pay_Creat";
            this.Text = "บันทึกรายการต่อใบอนุญาติทำงานต่างๆ";
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgv_Payment.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgv_Payment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blb_Payment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Paymenttype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmplName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Newdoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_AddData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Save)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.RadButton btn_Save;
        private Telerik.WinControls.UI.RadButton btn_AddData;
        private Telerik.WinControls.UI.RadGridView rgv_Payment;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtEmplName;
        private Telerik.WinControls.UI.RadTextBox txtEmpId;
        private Telerik.WinControls.UI.RadDropDownList ddl_Paymenttype;
        private Telerik.WinControls.UI.RadLabel blb_Payment;
        private Telerik.WinControls.UI.RadButton btn_Newdoc;
        private Telerik.WinControls.UI.RadButton btn_Delete;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
    }
}