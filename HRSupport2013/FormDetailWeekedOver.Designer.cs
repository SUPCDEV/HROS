namespace HROUTOFFICE
{
    partial class FormDetailWeekedOver
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.radGridegetdata = new Telerik.WinControls.UI.RadGridView();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radGroupBoxMain = new Telerik.WinControls.UI.RadGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.radGridegetdata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridegetdata.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBoxMain)).BeginInit();
            this.radGroupBoxMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // radGridegetdata
            // 
            this.radGridegetdata.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radGridegetdata.Location = new System.Drawing.Point(16, 26);
            this.radGridegetdata.Margin = new System.Windows.Forms.Padding(4);
            // 
            // radGridegetdata
            // 
            gridViewTextBoxColumn1.FieldName = "DocId";
            gridViewTextBoxColumn1.HeaderText = "เลขทีเอกสาร";
            gridViewTextBoxColumn1.Name = "DocId";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn2.FieldName = "EmplId";
            gridViewTextBoxColumn2.HeaderText = "รหัสพนักงาน";
            gridViewTextBoxColumn2.Name = "EmplId";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn3.FieldName = "EmplFullName";
            gridViewTextBoxColumn3.HeaderText = "ชื่อ-สกุล";
            gridViewTextBoxColumn3.Name = "EmplFullName";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn4.FieldName = "Dimention";
            gridViewTextBoxColumn4.HeaderText = "แผนก";
            gridViewTextBoxColumn4.Name = "Dimention";
            gridViewTextBoxColumn5.FieldName = "OutType";
            gridViewTextBoxColumn5.HeaderText = "ออกนอก";
            gridViewTextBoxColumn5.Name = "OutType";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn6.FieldName = "CombackType";
            gridViewTextBoxColumn6.HeaderText = "กลับเข้าบริษัท";
            gridViewTextBoxColumn6.Name = "CombackType";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn7.FieldName = "Reason";
            gridViewTextBoxColumn7.HeaderText = "เหตุผล";
            gridViewTextBoxColumn7.Name = "Reason";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn8.FieldName = "TrandDateTime";
            gridViewTextBoxColumn8.HeaderText = "วันที่";
            gridViewTextBoxColumn8.Name = "TrandDateTime";
            gridViewTextBoxColumn8.ReadOnly = true;
            this.radGridegetdata.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8});
            this.radGridegetdata.MasterTemplate.ShowGroupedColumns = true;
            this.radGridegetdata.Name = "radGridegetdata";
            this.radGridegetdata.ShowGroupPanel = false;
            this.radGridegetdata.Size = new System.Drawing.Size(623, 188);
            this.radGridegetdata.TabIndex = 0;
            this.radGridegetdata.Text = "radGridView1";
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.radGroupBoxMain);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(784, 561);
            this.radPanel1.TabIndex = 2;
            // 
            // radGroupBoxMain
            // 
            this.radGroupBoxMain.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBoxMain.Controls.Add(this.radGridegetdata);
            this.radGroupBoxMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radGroupBoxMain.HeaderText = "แสดงข้อมูล";
            this.radGroupBoxMain.Location = new System.Drawing.Point(0, 4);
            this.radGroupBoxMain.Margin = new System.Windows.Forms.Padding(4);
            this.radGroupBoxMain.Name = "radGroupBoxMain";
            this.radGroupBoxMain.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            // 
            // 
            // 
            this.radGroupBoxMain.RootElement.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            this.radGroupBoxMain.Size = new System.Drawing.Size(651, 227);
            this.radGroupBoxMain.TabIndex = 0;
            this.radGroupBoxMain.Text = "แสดงข้อมูล";
            // 
            // FormDetailWeekedOver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.radPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormDetailWeekedOver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormDetailWeekedOver";
            this.Load += new System.EventHandler(this.FormDetailWeekedOver_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGridegetdata.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridegetdata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBoxMain)).EndInit();
            this.radGroupBoxMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView radGridegetdata;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBoxMain;
    }
}