namespace HRDOCS
{
    partial class Shift_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shift_Edit));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Dg_ShiftHD = new System.Windows.Forms.DataGridView();
            this.Dg_ShiftDT = new System.Windows.Forms.DataGridView();
            this.Btn_Edit = new Telerik.WinControls.UI.RadButton();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Cancel = new Telerik.WinControls.UI.RadButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_ShiftHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_ShiftDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Edit)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Cancel)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Dg_ShiftHD);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Dg_ShiftDT);
            this.splitContainer1.Size = new System.Drawing.Size(669, 561);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 1;
            // 
            // Dg_ShiftHD
            // 
            this.Dg_ShiftHD.AllowUserToAddRows = false;
            this.Dg_ShiftHD.AllowUserToResizeColumns = false;
            this.Dg_ShiftHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dg_ShiftHD.Location = new System.Drawing.Point(47, 26);
            this.Dg_ShiftHD.Name = "Dg_ShiftHD";
            this.Dg_ShiftHD.RowHeadersVisible = false;
            this.Dg_ShiftHD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dg_ShiftHD.Size = new System.Drawing.Size(240, 150);
            this.Dg_ShiftHD.TabIndex = 0;
            // 
            // Dg_ShiftDT
            // 
            this.Dg_ShiftDT.AllowUserToAddRows = false;
            this.Dg_ShiftDT.AllowUserToResizeColumns = false;
            this.Dg_ShiftDT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dg_ShiftDT.Location = new System.Drawing.Point(47, 28);
            this.Dg_ShiftDT.Name = "Dg_ShiftDT";
            this.Dg_ShiftDT.RowHeadersVisible = false;
            this.Dg_ShiftDT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dg_ShiftDT.Size = new System.Drawing.Size(240, 150);
            this.Dg_ShiftDT.TabIndex = 1;
            // 
            // Btn_Edit
            // 
            this.Btn_Edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Btn_Edit.Location = new System.Drawing.Point(5, 30);
            this.Btn_Edit.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Edit.Name = "Btn_Edit";
            this.Btn_Edit.Size = new System.Drawing.Size(100, 25);
            this.Btn_Edit.TabIndex = 13;
            this.Btn_Edit.Text = "แก้ไข";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Column1";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.Btn_Cancel);
            this.splitContainer2.Panel2.Controls.Add(this.Btn_Edit);
            this.splitContainer2.Size = new System.Drawing.Size(784, 561);
            this.splitContainer2.SplitterDistance = 669;
            this.splitContainer2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 536);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "F5 : Refresh";
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Location = new System.Drawing.Point(5, 62);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(100, 25);
            this.Btn_Cancel.TabIndex = 14;
            this.Btn_Cancel.Text = "ยกเลิก";
            this.Btn_Cancel.Visible = false;
            // 
            // Shift_Edit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer2);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "Shift_Edit";
            this.Text = "แก้ไขข้อมูล";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dg_ShiftHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_ShiftDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Edit)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Cancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Telerik.WinControls.UI.RadButton Btn_Edit;
        private System.Windows.Forms.DataGridView Dg_ShiftHD;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridView Dg_ShiftDT;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Telerik.WinControls.UI.RadButton Btn_Cancel;
        private System.Windows.Forms.Label label1;
    }
}