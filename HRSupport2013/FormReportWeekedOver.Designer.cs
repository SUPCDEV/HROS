namespace HROUTOFFICE
{
    partial class FormReportWeekedOver
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewHyperlinkColumn gridViewHyperlinkColumn2 = new Telerik.WinControls.UI.GridViewHyperlinkColumn();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.radGridegetdata = new Telerik.WinControls.UI.RadGridView();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.radButtonExport = new Telerik.WinControls.UI.RadButton();
            this.btnserch = new Telerik.WinControls.UI.RadButton();
            this.dtpEnd = new Telerik.WinControls.UI.RadDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpStart = new Telerik.WinControls.UI.RadDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridegetdata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridegetdata.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnserch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.radGroupBox2);
            this.radPanel1.Controls.Add(this.radGroupBox1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(584, 461);
            this.radPanel1.TabIndex = 0;
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.Controls.Add(this.radGridegetdata);
            this.radGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGroupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radGroupBox2.HeaderText = "แสดงข้อมูล";
            this.radGroupBox2.Location = new System.Drawing.Point(0, 90);
            this.radGroupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            this.radGroupBox2.Size = new System.Drawing.Size(584, 371);
            this.radGroupBox2.TabIndex = 2;
            this.radGroupBox2.Text = "แสดงข้อมูล";
            // 
            // radGridegetdata
            // 
            this.radGridegetdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridegetdata.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radGridegetdata.Location = new System.Drawing.Point(3, 22);
            this.radGridegetdata.Margin = new System.Windows.Forms.Padding(4);
            // 
            // radGridegetdata
            // 
            gridViewTextBoxColumn5.FieldName = "EmplId";
            gridViewTextBoxColumn5.HeaderText = "รหัสพนักงาน";
            gridViewTextBoxColumn5.Name = "EmplId";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn6.FieldName = "EmplFname";
            gridViewTextBoxColumn6.HeaderText = "ชื่อ";
            gridViewTextBoxColumn6.Name = "EmplFname";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn7.FieldName = "EmplLname";
            gridViewTextBoxColumn7.HeaderText = "สกุล";
            gridViewTextBoxColumn7.Name = "EmplLname";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn8.FieldName = "Dimention";
            gridViewTextBoxColumn8.HeaderText = "แผนก";
            gridViewTextBoxColumn8.Name = "Dimention";
            gridViewTextBoxColumn8.ReadOnly = true;
            gridViewHyperlinkColumn2.FieldName = "CEmplId";
            gridViewHyperlinkColumn2.HeaderText = "จำนวน";
            gridViewHyperlinkColumn2.Name = "CEmplId";
            this.radGridegetdata.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewHyperlinkColumn2});
            this.radGridegetdata.MasterTemplate.ShowGroupedColumns = true;
            this.radGridegetdata.Name = "radGridegetdata";
            this.radGridegetdata.ShowGroupPanel = false;
            this.radGridegetdata.Size = new System.Drawing.Size(578, 347);
            this.radGridegetdata.TabIndex = 0;
            this.radGridegetdata.Text = "radGridView1";
            this.radGridegetdata.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.radGridegetdata_CellClick);
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.radButtonExport);
            this.radGroupBox1.Controls.Add(this.btnserch);
            this.radGroupBox1.Controls.Add(this.dtpEnd);
            this.radGroupBox1.Controls.Add(this.label4);
            this.radGroupBox1.Controls.Add(this.dtpStart);
            this.radGroupBox1.Controls.Add(this.label3);
            this.radGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radGroupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radGroupBox1.HeaderText = "ค้นหา";
            this.radGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.radGroupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(584, 90);
            this.radGroupBox1.TabIndex = 1;
            this.radGroupBox1.Text = "ค้นหา";
            // 
            // radButtonExport
            // 
            this.radButtonExport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radButtonExport.Location = new System.Drawing.Point(108, 54);
            this.radButtonExport.Margin = new System.Windows.Forms.Padding(4);
            this.radButtonExport.Name = "radButtonExport";
            this.radButtonExport.Size = new System.Drawing.Size(88, 25);
            this.radButtonExport.TabIndex = 28;
            this.radButtonExport.Text = "Export To Excel";
            this.radButtonExport.Click += new System.EventHandler(this.radButtonExport_Click);
            // 
            // btnserch
            // 
            this.btnserch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnserch.Location = new System.Drawing.Point(14, 54);
            this.btnserch.Margin = new System.Windows.Forms.Padding(4);
            this.btnserch.Name = "btnserch";
            this.btnserch.Size = new System.Drawing.Size(88, 25);
            this.btnserch.TabIndex = 27;
            this.btnserch.Text = "ค้นหา";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd";
            this.dtpEnd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(162, 26);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(87, 20);
            this.dtpEnd.TabIndex = 24;
            this.dtpEnd.TabStop = false;
            this.dtpEnd.Text = "2014-02-12";
            this.dtpEnd.Value = new System.DateTime(2014, 2, 12, 16, 33, 8, 281);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(137, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 14);
            this.label4.TabIndex = 26;
            this.label4.Text = "ถึง";
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy-MM-dd";
            this.dtpStart.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(44, 26);
            this.dtpStart.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(87, 20);
            this.dtpStart.TabIndex = 23;
            this.dtpStart.TabStop = false;
            this.dtpStart.Text = "2014-02-12";
            this.dtpStart.Value = new System.DateTime(2014, 2, 12, 16, 33, 8, 281);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(11, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 14);
            this.label3.TabIndex = 25;
            this.label3.Text = "วันที่";
            // 
            // FormReportWeekedOver
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.radPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormReportWeekedOver";
            this.Text = "รายงานออกนอกเกินสองครั้ง / สัปดาห์";
            this.Load += new System.EventHandler(this.FormReportWeekedOver_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridegetdata.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridegetdata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnserch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadButton radButtonExport;
        private Telerik.WinControls.UI.RadButton btnserch;
        private Telerik.WinControls.UI.RadDateTimePicker dtpEnd;
        private System.Windows.Forms.Label label4;
        private Telerik.WinControls.UI.RadDateTimePicker dtpStart;
        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadGridView radGridegetdata;
    }
}