namespace HRDOCS
{
    partial class Shift_Main
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
            Telerik.WinControls.UI.RadTreeNode radTreeNode1 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode2 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode3 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode4 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode5 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode6 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode7 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode8 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode9 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode10 = new Telerik.WinControls.UI.RadTreeNode();
            this.radTreeView1 = new Telerik.WinControls.UI.RadTreeView();
            this.Btn_ShiftCreate = new System.Windows.Forms.Button();
            this.Btn_ShiftEdit = new System.Windows.Forms.Button();
            this.Btn_HDApprove = new System.Windows.Forms.Button();
            this.Btn_HRApprove = new System.Windows.Forms.Button();
            this.Btn_User = new System.Windows.Forms.Button();
            this.Btn_Search = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).BeginInit();
            this.SuspendLayout();
            // 
            // radTreeView1
            // 
            this.radTreeView1.BackColor = System.Drawing.Color.White;
            this.radTreeView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radTreeView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radTreeView1.ForeColor = System.Drawing.Color.Black;
            this.radTreeView1.Location = new System.Drawing.Point(0, 0);
            this.radTreeView1.Name = "radTreeView1";
            radTreeNode1.Expanded = true;
            radTreeNode1.Name = "Shift";
            radTreeNode2.Name = "Shift_Create";
            radTreeNode2.Text = "สร้างเอกสาร";
            radTreeNode3.Name = "Shift_Edit";
            radTreeNode3.Text = "แก้ไข/ยกเลิกเอกสาร";
            radTreeNode4.Name = "Shift_Search";
            radTreeNode4.Text = "ค้นหาเอกสาร";
            radTreeNode1.Nodes.AddRange(new Telerik.WinControls.UI.RadTreeNode[] {
            radTreeNode2,
            radTreeNode3,
            radTreeNode4});
            radTreeNode1.Text = "เอกสารใบเปลี่ยนกะ";
            radTreeNode5.Expanded = true;
            radTreeNode5.Name = "HD";
            radTreeNode6.Name = "HD_Approve";
            radTreeNode6.Text = "อนุมัติเอกสาร";
            radTreeNode5.Nodes.AddRange(new Telerik.WinControls.UI.RadTreeNode[] {
            radTreeNode6});
            radTreeNode5.Text = "หัวหน้า/ผู้ช่วย";
            radTreeNode7.Expanded = true;
            radTreeNode7.Name = "HR";
            radTreeNode8.Name = "HR_Approve";
            radTreeNode8.Text = "อนุมัติเอกสาร";
            radTreeNode7.Nodes.AddRange(new Telerik.WinControls.UI.RadTreeNode[] {
            radTreeNode8});
            radTreeNode7.Text = "แผนกบุคคล";
            radTreeNode9.Expanded = true;
            radTreeNode9.Name = "Admin";
            radTreeNode10.Name = "Admin";
            radTreeNode10.Text = "เพิ่มสิทธิ์";
            radTreeNode9.Nodes.AddRange(new Telerik.WinControls.UI.RadTreeNode[] {
            radTreeNode10});
            radTreeNode9.Text = "ผู้ดูแลระบบ";
            this.radTreeView1.Nodes.AddRange(new Telerik.WinControls.UI.RadTreeNode[] {
            radTreeNode1,
            radTreeNode5,
            radTreeNode7,
            radTreeNode9});
            this.radTreeView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radTreeView1.Size = new System.Drawing.Size(188, 414);
            this.radTreeView1.SpacingBetweenNodes = -1;
            this.radTreeView1.TabIndex = 0;
            this.radTreeView1.Text = "radTreeView1";
            // 
            // Btn_ShiftCreate
            // 
            this.Btn_ShiftCreate.Location = new System.Drawing.Point(100, 63);
            this.Btn_ShiftCreate.Name = "Btn_ShiftCreate";
            this.Btn_ShiftCreate.Size = new System.Drawing.Size(174, 64);
            this.Btn_ShiftCreate.TabIndex = 0;
            this.Btn_ShiftCreate.Text = "สร้างเอกสาร";
            this.Btn_ShiftCreate.UseVisualStyleBackColor = true;
            // 
            // Btn_ShiftEdit
            // 
            this.Btn_ShiftEdit.Location = new System.Drawing.Point(329, 63);
            this.Btn_ShiftEdit.Name = "Btn_ShiftEdit";
            this.Btn_ShiftEdit.Size = new System.Drawing.Size(174, 64);
            this.Btn_ShiftEdit.TabIndex = 1;
            this.Btn_ShiftEdit.Text = "แก้ไข/ยกเลิก";
            this.Btn_ShiftEdit.UseVisualStyleBackColor = true;
            // 
            // Btn_HDApprove
            // 
            this.Btn_HDApprove.Location = new System.Drawing.Point(111, 219);
            this.Btn_HDApprove.Name = "Btn_HDApprove";
            this.Btn_HDApprove.Size = new System.Drawing.Size(174, 64);
            this.Btn_HDApprove.TabIndex = 2;
            this.Btn_HDApprove.Text = "หัวหน้า/ผู้ช่วย อนุมัติ";
            this.Btn_HDApprove.UseVisualStyleBackColor = true;
            // 
            // Btn_HRApprove
            // 
            this.Btn_HRApprove.Location = new System.Drawing.Point(434, 219);
            this.Btn_HRApprove.Name = "Btn_HRApprove";
            this.Btn_HRApprove.Size = new System.Drawing.Size(174, 64);
            this.Btn_HRApprove.TabIndex = 3;
            this.Btn_HRApprove.Text = "บุคคล อนุมัติ";
            this.Btn_HRApprove.UseVisualStyleBackColor = true;
            // 
            // Btn_User
            // 
            this.Btn_User.Location = new System.Drawing.Point(256, 417);
            this.Btn_User.Name = "Btn_User";
            this.Btn_User.Size = new System.Drawing.Size(247, 92);
            this.Btn_User.TabIndex = 4;
            this.Btn_User.Text = "หัวหน้า";
            this.Btn_User.UseVisualStyleBackColor = true;
            // 
            // Btn_Search
            // 
            this.Btn_Search.Location = new System.Drawing.Point(551, 63);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(174, 64);
            this.Btn_Search.TabIndex = 5;
            this.Btn_Search.Text = "ดูข้อมูล";
            this.Btn_Search.UseVisualStyleBackColor = true;
            // 
            // Shift_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.Btn_Search);
            this.Controls.Add(this.Btn_User);
            this.Controls.Add(this.Btn_HRApprove);
            this.Controls.Add(this.Btn_HDApprove);
            this.Controls.Add(this.Btn_ShiftEdit);
            this.Controls.Add(this.Btn_ShiftCreate);
            this.Name = "Shift_Main";
            this.Text = "Shift_Main";
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadTreeView radTreeView1;
        private System.Windows.Forms.Button Btn_ShiftCreate;
        private System.Windows.Forms.Button Btn_ShiftEdit;
        private System.Windows.Forms.Button Btn_HDApprove;
        private System.Windows.Forms.Button Btn_HRApprove;
        private System.Windows.Forms.Button Btn_User;
        private System.Windows.Forms.Button Btn_Search;

    }
}