namespace HRDOCS
{
    partial class Select_Shift
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Ddl_Time2 = new Telerik.WinControls.UI.RadDropDownList();
            this.Ddl_Time1 = new Telerik.WinControls.UI.RadDropDownList();
            this.Cbb_Time2 = new System.Windows.Forms.ComboBox();
            this.Cbb_Time1 = new System.Windows.Forms.ComboBox();
            this.Btn_Search = new Telerik.WinControls.UI.RadButton();
            this.Dg_SelectShiftAll = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.Dg_SelectShift = new System.Windows.Forms.DataGridView();
            this.Dg_SelectShiftCompensate = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusF3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ddl_Time2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ddl_Time1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Search)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_SelectShiftAll)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_SelectShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_SelectShiftCompensate)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "เวลาเข้างาน : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "เวลาเลิกงาน : ";
            // 
            // splitContainer1
            // 
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Ddl_Time2);
            this.splitContainer1.Panel1.Controls.Add(this.Ddl_Time1);
            this.splitContainer1.Panel1.Controls.Add(this.Cbb_Time2);
            this.splitContainer1.Panel1.Controls.Add(this.Cbb_Time1);
            this.splitContainer1.Panel1.Controls.Add(this.Btn_Search);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.splitContainer1.Panel1MinSize = 60;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Dg_SelectShiftAll);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(584, 345);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 4;
            // 
            // Ddl_Time2
            // 
            this.Ddl_Time2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Ddl_Time2.Location = new System.Drawing.Point(241, 20);
            this.Ddl_Time2.Name = "Ddl_Time2";
            this.Ddl_Time2.Size = new System.Drawing.Size(71, 22);
            this.Ddl_Time2.TabIndex = 6;
            this.Ddl_Time2.Text = "radDropDownList2";
            // 
            // Ddl_Time1
            // 
            this.Ddl_Time1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Ddl_Time1.Location = new System.Drawing.Point(88, 20);
            this.Ddl_Time1.Name = "Ddl_Time1";
            this.Ddl_Time1.Size = new System.Drawing.Size(71, 22);
            this.Ddl_Time1.TabIndex = 5;
            this.Ddl_Time1.Text = "radDropDownList1";
            // 
            // Cbb_Time2
            // 
            this.Cbb_Time2.FormattingEnabled = true;
            this.Cbb_Time2.Items.AddRange(new object[] {
            "0.00",
            "0.30",
            "1.00",
            "1.30",
            "2.00",
            "2.30",
            "3.00",
            "3.30",
            "4.00",
            "4.30",
            "5.00",
            "5.30",
            "6.00",
            "6.30",
            "7.00",
            "7.30",
            "8.00",
            "8.30",
            "9.00",
            "9.30",
            "10.00",
            "10.30",
            "11.00",
            "11.30",
            "12.00",
            "12.30",
            "13.00",
            "13.30",
            "14.00",
            "14.30",
            "15.00",
            "15.30",
            "16.00",
            "16.30",
            "17.00",
            "17.30",
            "18.00",
            "18.30",
            "19.00",
            "19.30",
            "20.00",
            "20.30",
            "21.00",
            "21.30",
            "22.00",
            "22.30",
            "23.00",
            "23.30"});
            this.Cbb_Time2.Location = new System.Drawing.Point(501, 21);
            this.Cbb_Time2.Name = "Cbb_Time2";
            this.Cbb_Time2.Size = new System.Drawing.Size(71, 22);
            this.Cbb_Time2.TabIndex = 2;
            this.Cbb_Time2.Visible = false;
            // 
            // Cbb_Time1
            // 
            this.Cbb_Time1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Cbb_Time1.FormattingEnabled = true;
            this.Cbb_Time1.Items.AddRange(new object[] {
            "0.00",
            "0.30",
            "1.00",
            "1.30",
            "2.00",
            "2.30",
            "3.00",
            "3.30",
            "4.00",
            "4.30",
            "5.00",
            "5.30",
            "6.00",
            "6.30",
            "7.00",
            "7.30",
            "8.00",
            "8.30",
            "9.00",
            "9.30",
            "10.00",
            "10.30",
            "11.00",
            "11.30",
            "12.00",
            "12.30",
            "13.00",
            "13.30",
            "14.00",
            "14.30",
            "15.00",
            "15.30",
            "16.00",
            "16.30",
            "17.00",
            "17.30",
            "18.00",
            "18.30",
            "19.00",
            "19.30",
            "20.00",
            "20.30",
            "21.00",
            "21.30",
            "22.00",
            "22.30",
            "23.00",
            "23.30"});
            this.Cbb_Time1.Location = new System.Drawing.Point(478, 21);
            this.Cbb_Time1.Name = "Cbb_Time1";
            this.Cbb_Time1.Size = new System.Drawing.Size(71, 22);
            this.Cbb_Time1.TabIndex = 1;
            this.Cbb_Time1.Visible = false;
            // 
            // Btn_Search
            // 
            this.Btn_Search.Location = new System.Drawing.Point(339, 18);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(100, 25);
            this.Btn_Search.TabIndex = 3;
            this.Btn_Search.Text = "ค้นหา";
            // 
            // Dg_SelectShiftAll
            // 
            this.Dg_SelectShiftAll.AllowUserToAddRows = false;
            this.Dg_SelectShiftAll.AllowUserToResizeRows = false;
            this.Dg_SelectShiftAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dg_SelectShiftAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dg_SelectShiftAll.Location = new System.Drawing.Point(0, 0);
            this.Dg_SelectShiftAll.Name = "Dg_SelectShiftAll";
            this.Dg_SelectShiftAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dg_SelectShiftAll.Size = new System.Drawing.Size(584, 281);
            this.Dg_SelectShiftAll.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.Dg_SelectShift);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Dg_SelectShiftCompensate);
            this.splitContainer2.Size = new System.Drawing.Size(584, 281);
            this.splitContainer2.SplitterDistance = 292;
            this.splitContainer2.TabIndex = 1;
            // 
            // Dg_SelectShift
            // 
            this.Dg_SelectShift.AllowUserToAddRows = false;
            this.Dg_SelectShift.AllowUserToResizeRows = false;
            this.Dg_SelectShift.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dg_SelectShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dg_SelectShift.Location = new System.Drawing.Point(0, 0);
            this.Dg_SelectShift.Name = "Dg_SelectShift";
            this.Dg_SelectShift.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dg_SelectShift.Size = new System.Drawing.Size(292, 281);
            this.Dg_SelectShift.TabIndex = 0;
            // 
            // Dg_SelectShiftCompensate
            // 
            this.Dg_SelectShiftCompensate.AllowUserToAddRows = false;
            this.Dg_SelectShiftCompensate.AllowUserToResizeRows = false;
            this.Dg_SelectShiftCompensate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dg_SelectShiftCompensate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dg_SelectShiftCompensate.Location = new System.Drawing.Point(0, 0);
            this.Dg_SelectShiftCompensate.Name = "Dg_SelectShiftCompensate";
            this.Dg_SelectShiftCompensate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dg_SelectShiftCompensate.Size = new System.Drawing.Size(288, 281);
            this.Dg_SelectShiftCompensate.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage,
            this.statusF3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 348);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 24);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusMessage
            // 
            this.statusMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.statusMessage.ForeColor = System.Drawing.Color.Red;
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(284, 19);
            this.statusMessage.Spring = true;
            this.statusMessage.Text = "เลือก: ดับเบิ้ลคลิกแถวหรือเรคคอร์ดที่ต้องการ";
            this.statusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusF3
            // 
            this.statusF3.AutoToolTip = true;
            this.statusF3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusF3.Name = "statusF3";
            this.statusF3.Size = new System.Drawing.Size(284, 19);
            this.statusF3.Spring = true;
            this.statusF3.Text = "F3: ค้นหา";
            this.statusF3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Select_Shift
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(584, 372);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "Select_Shift";
            this.Text = "เลือกกะ";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Ddl_Time2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ddl_Time1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Search)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_SelectShiftAll)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dg_SelectShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_SelectShiftCompensate)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Telerik.WinControls.UI.RadButton Btn_Search;
        private System.Windows.Forms.DataGridView Dg_SelectShift;
        private System.Windows.Forms.ComboBox Cbb_Time1;
        private System.Windows.Forms.ComboBox Cbb_Time2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView Dg_SelectShiftCompensate;
        private Telerik.WinControls.UI.RadDropDownList Ddl_Time2;
        private Telerik.WinControls.UI.RadDropDownList Ddl_Time1;
        private System.Windows.Forms.DataGridView Dg_SelectShiftAll;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.ToolStripStatusLabel statusF3;
    }
}