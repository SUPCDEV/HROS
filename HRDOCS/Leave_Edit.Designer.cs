namespace HRDOCS
{
    partial class Leave_Edit
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Dg_LeaveHD = new System.Windows.Forms.DataGridView();
            this.Dg_LeaveDT = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Cancel = new Telerik.WinControls.UI.RadButton();
            this.Btn_Edit = new Telerik.WinControls.UI.RadButton();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_LeaveHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_LeaveDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Cancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Edit)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer2.TabIndex = 2;
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
            this.splitContainer1.Panel1.Controls.Add(this.Dg_LeaveHD);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Dg_LeaveDT);
            this.splitContainer1.Size = new System.Drawing.Size(669, 561);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 1;
            // 
            // Dg_LeaveHD
            // 
            this.Dg_LeaveHD.AllowUserToAddRows = false;
            this.Dg_LeaveHD.AllowUserToResizeColumns = false;
            this.Dg_LeaveHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dg_LeaveHD.Location = new System.Drawing.Point(47, 26);
            this.Dg_LeaveHD.Name = "Dg_LeaveHD";
            this.Dg_LeaveHD.RowHeadersVisible = false;
            this.Dg_LeaveHD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dg_LeaveHD.Size = new System.Drawing.Size(240, 150);
            this.Dg_LeaveHD.TabIndex = 0;
            // 
            // Dg_LeaveDT
            // 
            this.Dg_LeaveDT.AllowUserToAddRows = false;
            this.Dg_LeaveDT.AllowUserToResizeColumns = false;
            this.Dg_LeaveDT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dg_LeaveDT.Location = new System.Drawing.Point(47, 28);
            this.Dg_LeaveDT.Name = "Dg_LeaveDT";
            this.Dg_LeaveDT.RowHeadersVisible = false;
            this.Dg_LeaveDT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dg_LeaveDT.Size = new System.Drawing.Size(240, 150);
            this.Dg_LeaveDT.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 536);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
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
            // Leave_Edit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer2);
            this.Name = "Leave_Edit";
            this.Text = "แก้ไขข้อมูลใบลา";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dg_LeaveHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_LeaveDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Cancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Edit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView Dg_LeaveHD;
        private System.Windows.Forms.DataGridView Dg_LeaveDT;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadButton Btn_Cancel;
        private Telerik.WinControls.UI.RadButton Btn_Edit;
    }
}