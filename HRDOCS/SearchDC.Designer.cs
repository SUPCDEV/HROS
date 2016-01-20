namespace HRDOCS
{
    partial class SearchDC
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Btn_Search = new Telerik.WinControls.UI.RadButton();
            this.Dtp_EndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.Dtp_StartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_DocNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Dg_SearchDS = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Search)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_SearchDS)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(584, 121);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Btn_Search);
            this.tabPage1.Controls.Add(this.Dtp_EndDate);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.Dtp_StartDate);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.Txt_DocNo);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 94);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ค้นหา";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Btn_Search
            // 
            this.Btn_Search.Location = new System.Drawing.Point(6, 63);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(100, 25);
            this.Btn_Search.TabIndex = 6;
            this.Btn_Search.Text = "ค้นหา";
            // 
            // Dtp_EndDate
            // 
            this.Dtp_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_EndDate.Location = new System.Drawing.Point(218, 37);
            this.Dtp_EndDate.Name = "Dtp_EndDate";
            this.Dtp_EndDate.Size = new System.Drawing.Size(89, 22);
            this.Dtp_EndDate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "ถึง :";
            // 
            // Dtp_StartDate
            // 
            this.Dtp_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_StartDate.Location = new System.Drawing.Point(89, 37);
            this.Dtp_StartDate.Name = "Dtp_StartDate";
            this.Dtp_StartDate.Size = new System.Drawing.Size(89, 22);
            this.Dtp_StartDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "วันที่เอกสาร :";
            // 
            // Txt_DocNo
            // 
            this.Txt_DocNo.Location = new System.Drawing.Point(89, 9);
            this.Txt_DocNo.Name = "Txt_DocNo";
            this.Txt_DocNo.Size = new System.Drawing.Size(93, 22);
            this.Txt_DocNo.TabIndex = 1;
            this.Txt_DocNo.Text = "DS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "เลขที่เอกสาร : ";
            // 
            // Dg_SearchDS
            // 
            this.Dg_SearchDS.AllowUserToAddRows = false;
            this.Dg_SearchDS.AllowUserToDeleteRows = false;
            this.Dg_SearchDS.BackgroundColor = System.Drawing.Color.Honeydew;
            this.Dg_SearchDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dg_SearchDS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dg_SearchDS.Location = new System.Drawing.Point(0, 121);
            this.Dg_SearchDS.Name = "Dg_SearchDS";
            this.Dg_SearchDS.ReadOnly = true;
            this.Dg_SearchDS.Size = new System.Drawing.Size(584, 340);
            this.Dg_SearchDS.TabIndex = 3;
            // 
            // SearchDC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.Dg_SearchDS);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "SearchDC";
            this.Text = "SearchDC";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Search)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dg_SearchDS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DateTimePicker Dtp_EndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker Dtp_StartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_DocNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView Dg_SearchDS;
        private Telerik.WinControls.UI.RadButton Btn_Search;
    }
}