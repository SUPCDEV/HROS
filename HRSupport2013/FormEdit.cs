using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Telerik.WinControls.Primitives;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using System.IO;
using System.Threading;
using SysApp;

namespace HROUTOFFICE
{
    public partial class FormEdit : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);

        string work = "";
        string comeback = "";
        string truck = "";

        public FormEdit()
        {
            InitializeComponent();
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;
            this.KeyPreview = true;

            this.radPanel1.Dock = DockStyle.Fill;
            this.radtruck2.CheckedChanged += new EventHandler(radtruck2_CheckedChanged); 
        }
        
        public FormEdit(string _docId) : this()
        {
            radLabelDocId.Text = _docId;
        }        

        private void FormEdit_Load(object sender, EventArgs e)
        {
            this.btnEditData.Enabled = !string.IsNullOrEmpty(this.radLabelDocId.Text.Trim());
            if (!this.radLabelDocId.Enabled)
                return;

            if (con.State == ConnectionState.Open) con.Close();          
            con.Open();
            try
            {
                string sql = @" SELECT 
	                                  [DocId],[CreatedBy],[CreatedName],[Dimention],[Dept]
                                      ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType
                                      ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS CombackType
                                      ,CASE [TruckType] WHEN '1' THEN 'รถส่วนตัว' WHEN '2' THEN 'รถบริษัท' ELSE 'ไม่มีข้อมูล' END AS TruckType
                                      ,[TruckId],[Reason],CONVERT(VARCHAR,[TrandDateTime],23) AS TrandDateTime
                                FROM [IVZ_HROUTOFFICE] 
                                WHERE [DocId] = '" + radLabelDocId.Text + "' GROUP BY  [DocId],[CreatedBy],[CreatedName],[Dimention],[Dept],OutType,[CombackType], [TruckType],[TruckId],[Reason],TrandDateTime  ";
                //" + radLabelDocId.Text + "
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        radLabelEmplName.Text = reader["CreatedName"].ToString();
                        radLabelEmplId.Text = reader["CreatedBy"].ToString();
                        radLabelDimention.Text = reader["Dimention"].ToString();
                        radLabelDept.Text = reader["Dept"].ToString();
                        radLabelOutType.Text = reader["OutType"].ToString();
                        radLabelCombackType.Text = reader["CombackType"].ToString();
                        radLabelTruckType.Text = reader["TruckType"].ToString();
                        radLabelTruckId.Text = reader["TruckId"].ToString();
                        radLabelReason.Text = reader["Reason"].ToString();
                        txtReason.Text = reader["Reason"].ToString();
                        radLabelDateTime.Text = reader["TrandDateTime"].ToString();
                        
                        txtReason.Text = reader["Reason"].ToString();

                        if (reader["OutType"].ToString().Equals("งานบริษัท"))
                        {
                            radwork1.Checked = true;
                        }
                        else
                        {
                            radwork2.Checked = true;
                        }
                        if (reader["CombackType"].ToString().Equals("กลับเข้ามา"))
                        {
                            radcomback1.Checked = true;
                        }
                        else
                        {
                            radcomback2.Checked = true;
                        }
                        if (reader["TruckType"].ToString().Equals("รถส่วนตัว"))
                        {
                            radtruck1.Checked = true;
                        }
                        else
                        {
                            radtruck2.Checked = true;
                            txttruckid.Text = reader["TruckId"].ToString();
                        }
                       
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        void radtruck2_CheckedChanged(object sender, EventArgs e)
        {
            //bool localCheck = ((RadioButton)(sender)).Checked;

            //this.txttruckid.Enabled = localCheck;
            //this.txttruckid.ReadOnly = !localCheck;
            //this.radtruck1.Checked = !localCheck;
        }
        //public void LookUpTruckId(string _Ptruckid)
        //{
        //    txttruckid.Text = _Ptruckid;
        //}
        #region <Function and Methods>
        private void LicenseLookup()
        {
            //if (radtruck2.Checked == true && txttruckid.Focused)
            //{
                if (radtruck2.Checked)
            {
                //this.txttruckid.Enabled = true;
                using (FormTruckIdEdit frmTruckEdit = new FormTruckIdEdit(this.txttruckid.Text.Trim()))
                {
                    frmTruckEdit.StartPosition = FormStartPosition.CenterParent;
                    if (frmTruckEdit.ShowDialog(this) == DialogResult.OK)
                    {
                        this.txttruckid.Text = frmTruckEdit.VEHICLEID;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        #endregion
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                this.LicenseLookup();
            }
            else if (keyData == Keys.Escape)
            {
                if (!IsSaveOnce)
                {
                    string message = string.Format(@"มีรายการที่ยังไม่ได้บันทึก{0}ยืนยันการเปิดหน้าต่างการทำงานหากไม่ต้องการบันทึกรายการ ?", Environment.NewLine);
                    if (MessageBox.Show(message, "หน้าต่างยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void UpdateData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            //SqlTransaction tr = con.BeginTransaction();

            #region radio

            if (radLabelDocId.Text != "")
            {
                if (radwork1.Checked == true)
                {
                    work = "1";
                }
                else
                {
                    work = "2";
                }

                if (radcomback1.Checked == true)
                {
                    comeback = "1";
                }
                else
                {
                    comeback = "2";
                }

                if (radtruck2.Checked == true && txttruckid.Text != "")
                {
                    truck = "2";
                }
                else if (radtruck2.Checked == true && txttruckid.Text == "")
                {
                    MessageBox.Show("กรุณาใส่ทะเบียนรถ");
                    this.txttruckid.Focus();
                    return;
                }
                else
                {
                    truck = "1";
                    this.txttruckid.Text = "";
                }
                // return;
            }
            #endregion

            //if(string.IsNullOrEmpty(this.radLabelDocId.Text))
            //{
            //    this.DialogResult = DialogResult.Cancel;
            //    //throw new Exception("DocId is empty.");                
            //}            

            if (txtReason.Text != "")
            {
                if (MessageBox.Show("คุณต้องการบันทึกข้อมูลเอกสารใบออกนอกออนไลน์ ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;    
             
                        sqlCommand.CommandText = string.Format(
                                    @" Update [dbo].[IVZ_HROUTOFFICE] Set [OutType] = @OutType
                                                                    ,[CombackType] = @CombackType
                                                                    ,[TruckType] = @TruckType
                                                                    ,[TruckId] = @TruckId
                                                                    ,[ModifiedBy] = @ModifiedBy
                                                                    ,[ModifiedName] = @ModifiedName
                                                                    ,[Reason] = @Reason
																	,[ModifiedDateTime] = (dateadd(millisecond, -datepart(millisecond,getdate()),getdate()))
                                    WHERE DocId = '" + radLabelDocId.Text + "' AND [Status] = 1 AND [HeadApproved] = 1 AND [HrApprovedOut] = 1 AND [HrApprovedIn] = 1");

                                sqlCommand.Parameters.AddWithValue(string.Format(@"OutType"), work);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"CombackType"), comeback);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"TruckType"), truck);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"TruckId"), txttruckid.Text.ToString().Trim());
                                sqlCommand.Parameters.AddWithValue(string.Format(@"ModifiedBy"), ClassCurUser.LogInEmplId);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"ModifiedName"), ClassCurUser.LogInEmplName);
                                sqlCommand.Parameters.AddWithValue(string.Format(@"Reason"), txtReason.Text.ToString());

                                sqlCommand.ExecuteNonQuery();

                                MessageBox.Show("การแก้ไขเอกสารเลขที่: " + radLabelDocId.Text + "  สำเร็จ");
                                this.btnEditData.Enabled = false;
                                this.radGroupBoxTypOffice.Enabled = false;
                                this.radGroupBoxComeback.Enabled = false;
                                this.radGroupBoxTruck.Enabled = false;
                                this.radGroupBoxReason.Enabled = false;

                                this.DialogResult = DialogResult.OK;                           
                    }
                    catch (Exception ex)
                    {
                        this.btnEditData.Enabled = true;
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("โปรดระบุเหตุผลการออกนอก");
                this.txtReason.Focus();
            }
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateData();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message, "การแจ้งเตือน");
            }
        }
    }
}
