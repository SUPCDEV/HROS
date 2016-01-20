namespace HROUTOFFICE
{
    partial class FormEmplEdit
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
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.radGridViewEditDoc = new Telerik.WinControls.UI.RadGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabelMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.stutusF5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelRightSide = new System.Windows.Forms.Panel();
            this.btnAdjust = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewEditDoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewEditDoc.MasterTemplate)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panelRightSide.SuspendLayout();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.panelBottom);
            this.radGroupBox1.Controls.Add(this.panelTop);
            this.radGroupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox1.HeaderText = "แสดงข้อมูล";
            this.radGroupBox1.Location = new System.Drawing.Point(9, 6);
            this.radGroupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(3, 22, 3, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(652, 519);
            this.radGroupBox1.TabIndex = 1;
            this.radGroupBox1.Text = "แสดงข้อมูล";
            // 
            // panelBottom
            // 
            this.panelBottom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panelBottom.Location = new System.Drawing.Point(8, 245);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(637, 259);
            this.panelBottom.TabIndex = 3;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.radGridViewEditDoc);
            this.panelTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panelTop.Location = new System.Drawing.Point(8, 27);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(637, 210);
            this.panelTop.TabIndex = 2;
            // 
            // radGridViewEditDoc
            // 
            this.radGridViewEditDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridViewEditDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.radGridViewEditDoc.Location = new System.Drawing.Point(0, 0);
            this.radGridViewEditDoc.Margin = new System.Windows.Forms.Padding(4);
            // 
            // radGridViewEditDoc
            // 
            gridViewTextBoxColumn1.FieldName = "DocId";
            gridViewTextBoxColumn1.HeaderText = "เลขที่เอกสาร";
            gridViewTextBoxColumn1.Name = "DocId";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn2.FieldName = "CreatedBy";
            gridViewTextBoxColumn2.HeaderText = "รหัสผู้สร้าง";
            gridViewTextBoxColumn2.Name = "CreatedBy";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn3.FieldName = "CreatedName";
            gridViewTextBoxColumn3.HeaderText = "ผู้สร้าง";
            gridViewTextBoxColumn3.Name = "CreatedName";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn4.FieldName = "OutType";
            gridViewTextBoxColumn4.HeaderText = "ออกนอก";
            gridViewTextBoxColumn4.Name = "OutType";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn5.FieldName = "HeadApproved";
            gridViewTextBoxColumn5.HeaderText = "หน./ผช.";
            gridViewTextBoxColumn5.Name = "HeadApproved";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn6.FieldName = "HeadApprovedName";
            gridViewTextBoxColumn6.HeaderText = "ผู้อนุมัติ";
            gridViewTextBoxColumn6.Name = "HeadApprovedName";
            gridViewTextBoxColumn6.ReadOnly = true;
            this.radGridViewEditDoc.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6});
            this.radGridViewEditDoc.Name = "radGridViewEditDoc";
            this.radGridViewEditDoc.ShowGroupPanel = false;
            this.radGridViewEditDoc.Size = new System.Drawing.Size(637, 210);
            this.radGridViewEditDoc.TabIndex = 1;
            this.radGridViewEditDoc.Text = "radGridView1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Khaki;
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelMessage,
            this.stutusF5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 538);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(784, 23);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabelMessage
            // 
            this.statusLabelMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.statusLabelMessage.Name = "statusLabelMessage";
            this.statusLabelMessage.Size = new System.Drawing.Size(740, 18);
            this.statusLabelMessage.Spring = true;
            this.statusLabelMessage.Text = "toolStripStatusLabel1";
            this.statusLabelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stutusF5
            // 
            this.stutusF5.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.stutusF5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.stutusF5.Name = "stutusF5";
            this.stutusF5.Size = new System.Drawing.Size(24, 18);
            this.stutusF5.Text = "F5";
            this.stutusF5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelRightSide
            // 
            this.panelRightSide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRightSide.Controls.Add(this.btnAdjust);
            this.panelRightSide.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panelRightSide.Location = new System.Drawing.Point(669, 6);
            this.panelRightSide.Margin = new System.Windows.Forms.Padding(4);
            this.panelRightSide.Name = "panelRightSide";
            this.panelRightSide.Size = new System.Drawing.Size(110, 520);
            this.panelRightSide.TabIndex = 3;
            // 
            // btnAdjust
            // 
            this.btnAdjust.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAdjust.Location = new System.Drawing.Point(5, 30);
            this.btnAdjust.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(100, 25);
            this.btnAdjust.TabIndex = 0;
            this.btnAdjust.Tag = "Adjust";
            this.btnAdjust.Text = "ปรับปรุง";
            this.btnAdjust.UseVisualStyleBackColor = true;
            // 
            // FormEmplEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.radGroupBox1);
            this.Controls.Add(this.panelRightSide);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormEmplEdit";
            this.Text = "แก้ไขเอกสารออกนอกออนไลน์";
            this.Load += new System.EventHandler(this.FormEmplEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewEditDoc.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewEditDoc)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelRightSide.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGridView radGridViewEditDoc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelMessage;
        private System.Windows.Forms.ToolStripStatusLabel stutusF5;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelRightSide;
        private System.Windows.Forms.Button btnAdjust;
    }
}