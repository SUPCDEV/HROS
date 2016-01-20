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

namespace HRDOCS
{
    public partial class Cancle_ApproveHD_DetailDS : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string _docid;
        string _dsdoc;
        public Cancle_ApproveHD_DetailDS(string DocId, string Doctype)
        {
            _docid = DocId;

            InitializeComponent();

            InitializeDataGridView();
            this.btnApprove.Click += new EventHandler(btnApprove_Click);
            this.btnNonApprove.Click += new EventHandler(btnNonApprove_Click);
            GetData();
        }

        void btnApprove_Click(object sender, EventArgs e)
        {
            ApproveDS();
        }
        void btnNonApprove_Click(object sender, EventArgs e)
        {
            NonApprove();
        }
        private void Cancle_ApproveHD_DetailDS_Load(object sender, EventArgs e)
        {

        }

        void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            DataTable dt = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            try
            {
                sqlCommand.CommandText = string.Format(

                         @"SELECT HD.DOCID,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.REASON
                                ,DSHD.DSDOCNO,DOCTYP.TYPDESC ,DT.DOCREFER
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DSHD ON DT.DOCREFER = DSHD.DSDOCNO
                                LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTDT] DSDT ON DSHD.DSDOCNO = DSDT.DSDOCNO
                            WHERE HD.DOCID = '{0}'
                            AND  HD.HEADAPPROVED = '0'
                            AND HD.DOCSTAT = '1'
                            AND DSHD.DOCSTAT = '1' ", _docid.ToString());
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lbl_DocId.Text = reader["DOCID"].ToString();
                        lbl_EmplId.Text = reader["EMPLID"].ToString();
                        lbl_EmplName.Text = reader["EMPLNAME"].ToString();
                        lbl_SectionName.Text = reader["SECTIONNAME"].ToString();
                        lbl_DeptName.Text = reader["DEPTNAME"].ToString();
                        //lbl_Holliday1.Text = reader["HOLIDAY1"].ToString();
                        //lbl_Holliday2.Text = reader["HOLIDAY2"].ToString();
                        lbl_Reason.Text = reader["REASON"].ToString();

                        _dsdoc = reader["DSDOCNO"].ToString();
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

            // แสดงข้อมูลในตาราง
            try
            {
                sqlCommand.CommandText = string.Format(
                           @"SELECT DOCTYP.TYPDESC ,DSHD.DSDOCNO
                                ,CONVERT(VARCHAR,DSDT.SHIFTDATE,23) AS SHIFTDATE 
                                ,DSDT.FROMSHIFTID ,DSDT.FROMSHIFTDESC ,DSDT.TOSHIFTID ,DSDT.TOSHIFTDESC ,DSDT.REMARK 
                                ,CONVERT(VARCHAR,DSHD.CREATEDDATE,23) AS CREATEDDATE
                            FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DSHD ON DT.DOCREFER = DSHD.DSDOCNO
                                LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTDT] DSDT ON DSHD.DSDOCNO = DSDT.DSDOCNO
                            WHERE HD.DOCID = '{0}'
                            AND  HD.HEADAPPROVED = '0'
                            AND HD.DOCSTAT = '1'
                            AND DSHD.DOCSTAT = '1'
                            AND DT.DATEREFER  = DSDT.SHIFTDATE ", _docid.ToString());

                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_EmpData.DataSource = dt;
                }
                else
                {
                    rgv_EmpData.DataSource = dt;
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
        private void InitializeDataGridView()
        {
            #region SetDataGrid

            this.rgv_EmpData.Dock = DockStyle.Fill;
            this.rgv_EmpData.ReadOnly = true;
            this.rgv_EmpData.AutoGenerateColumns = true;
            this.rgv_EmpData.EnableFiltering = false;
            this.rgv_EmpData.AllowAddNewRow = false;
            this.rgv_EmpData.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_EmpData.ShowGroupedColumns = true;
            this.rgv_EmpData.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_EmpData.EnableHotTracking = true;
            this.rgv_EmpData.AutoSizeRows = true;

            GridViewTextBoxColumn TYPDESC = new GridViewTextBoxColumn();
            TYPDESC.Name = "TYPDESC";
            TYPDESC.FieldName = "TYPDESC";
            TYPDESC.HeaderText = "ประเภทเอกสาร";
            TYPDESC.IsVisible = true;
            TYPDESC.ReadOnly = true;
            TYPDESC.BestFit();
            rgv_EmpData.Columns.Add(TYPDESC);

            GridViewTextBoxColumn DSDOCNO = new GridViewTextBoxColumn();
            DSDOCNO.Name = "DSDOCNO";
            DSDOCNO.FieldName = "DSDOCNO";
            DSDOCNO.HeaderText = "เลขที่เอกสาร";
            DSDOCNO.IsVisible = true;
            DSDOCNO.ReadOnly = true;
            DSDOCNO.BestFit();
            rgv_EmpData.Columns.Add(DSDOCNO);

            GridViewTextBoxColumn SHIFTDATE = new GridViewTextBoxColumn();
            SHIFTDATE.Name = "SHIFTDATE";
            SHIFTDATE.FieldName = "SHIFTDATE";
            SHIFTDATE.HeaderText = "วันที่เปลี่ยนกะ";
            SHIFTDATE.IsVisible = true;
            SHIFTDATE.ReadOnly = true;
            SHIFTDATE.BestFit();
            rgv_EmpData.Columns.Add(SHIFTDATE);

            GridViewTextBoxColumn FROMSHIFTID = new GridViewTextBoxColumn();
            FROMSHIFTID.Name = "FROMSHIFTID";
            FROMSHIFTID.FieldName = "FROMSHIFTID";
            FROMSHIFTID.HeaderText = "กะเดิม";
            FROMSHIFTID.IsVisible = true;
            FROMSHIFTID.ReadOnly = true;
            FROMSHIFTID.BestFit();
            rgv_EmpData.Columns.Add(FROMSHIFTID);

            GridViewTextBoxColumn FROMSHIFTDESC = new GridViewTextBoxColumn();
            FROMSHIFTDESC.Name = "FROMSHIFTDESC";
            FROMSHIFTDESC.FieldName = "FROMSHIFTDESC";
            FROMSHIFTDESC.HeaderText = "คำอธิบาย";
            FROMSHIFTDESC.IsVisible = true;
            FROMSHIFTDESC.ReadOnly = true;
            FROMSHIFTDESC.BestFit();
            rgv_EmpData.Columns.Add(FROMSHIFTDESC);

            GridViewTextBoxColumn TOSHIFTID = new GridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.FieldName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "กะใหม่";
            TOSHIFTID.IsVisible = true;
            TOSHIFTID.ReadOnly = true;
            TOSHIFTID.BestFit();
            rgv_EmpData.Columns.Add(TOSHIFTID);

            GridViewTextBoxColumn TOSHIFTDESC = new GridViewTextBoxColumn();
            TOSHIFTDESC.Name = "TOSHIFTDESC";
            TOSHIFTDESC.FieldName = "TOSHIFTDESC";
            TOSHIFTDESC.HeaderText = "คำอธิบาย";
            TOSHIFTDESC.IsVisible = true;
            TOSHIFTDESC.ReadOnly = true;
            TOSHIFTDESC.BestFit();
            rgv_EmpData.Columns.Add(TOSHIFTDESC);

            GridViewTextBoxColumn REMARK = new GridViewTextBoxColumn();
            REMARK.Name = "REMARK";
            REMARK.FieldName = "REMARK";
            REMARK.HeaderText = "เหตุผล";
            REMARK.IsVisible = true;
            REMARK.ReadOnly = true;
            REMARK.BestFit();
            rgv_EmpData.Columns.Add(REMARK);

            GridViewTextBoxColumn CREATEDDATE = new GridViewTextBoxColumn();
            CREATEDDATE.Name = "CREATEDDATE";
            CREATEDDATE.FieldName = "CREATEDDATE";
            CREATEDDATE.HeaderText = "วันที่บันทึก";
            CREATEDDATE.IsVisible = true;
            CREATEDDATE.ReadOnly = true;
            CREATEDDATE.BestFit();
            rgv_EmpData.Columns.Add(CREATEDDATE);


            #endregion
        }
        void ApproveDS()
        {
            #region Old Code        
//            if (MessageBox.Show("คุณต้องการบันทึกเอกสาร ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
//            {
//                return;
//            }
//            try
//            {
//                if (con.State == ConnectionState.Open) con.Close();
//                con.Open();

//                SqlCommand sqlCommand = new SqlCommand();
//                sqlCommand.Connection = con;
//                DataTable dt = new DataTable();
//                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

//                SqlTransaction tr = con.BeginTransaction();
//                sqlCommand.Transaction = tr;

//                #region Update CN_Doc
//                sqlCommand.CommandText = string.Format(
//                    @"UPDATE SPC_JN_CANCLEDOCHD SET HEADAPPROVED = '1'
//								,HEADAPPROVEDBY = @HEADAPPROVEDBY
//								,HEADAPPROVEDBYNAME = @HEADAPPROVEDBYNAME
//								,HEADAPPROVEDDATE = convert(varchar, getdate(), 23)
//                                
//                      WHERE DOCID = @DOCID
//                      AND DOCSTAT = 1 ");

//                sqlCommand.Parameters.AddWithValue("@DOCID", _docid);
//                sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDBY", ClassCurUser.LogInEmplId);
//                sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDBYNAME", ClassCurUser.LogInEmplName);

//                sqlCommand.ExecuteNonQuery();
//                #endregion

//                tr.Commit();
//                this.IsSaveOnce = true;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.ToString());
//                return;
//            }
//            finally
//            {
//                if (con.State == ConnectionState.Open) con.Close();
//                this.Close();
//                this.DialogResult = DialogResult.Yes;
//            }
            #endregion
            
                if (MessageBox.Show("คุณต้องการบันทึกเอกสาร ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                using (ConfirmReject frm = new ConfirmReject())
                {
                    frm.Text = "อนุมัติ";

                    if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                        con.Open();

                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = con;
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

                        SqlTransaction tr = con.BeginTransaction();
                        sqlCommand.Transaction = tr;
                        if (lbl_DocId.Text != "")
                        {
                            try
                            {
                                sqlCommand.CommandText = string.Format(
                                @"UPDATE SPC_JN_CANCLEDOCHD SET HEADAPPROVED = '1'
								,HEADAPPROVEDBY = @HEADAPPROVEDBY
								,HEADAPPROVEDBYNAME = @HEADAPPROVEDBYNAME
								,HEADAPPROVEDDATE = convert(varchar, getdate(), 23)
                                ,HEADAPPORVEREMARK = '{0}'
                              WHERE DOCID = @DOCID
                              AND DOCSTAT = 1 ", frm.Remark);

                                sqlCommand.Parameters.AddWithValue("@DOCID", _docid);
                                sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDBY", ClassCurUser.LogInEmplId);
                                sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDBYNAME", ClassCurUser.LogInEmplName);
                                sqlCommand.ExecuteNonQuery();

                                tr.Commit();
                                this.IsSaveOnce = true;
                                // MessageBox.Show("เอกสารเลขที่เอกสาร : " + txtDocId.Text + " ทำรายการเรียบร้อยแล้ว");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            finally
                            {
                                if (con.State == ConnectionState.Open) con.Close();
                                this.Close();

                                this.DialogResult = DialogResult.Yes;
                            }
                        }
                    }
                }
            
        }
        void NonApprove()
        {
            if (MessageBox.Show("คุณต้องการบันทึกเอกสาร ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            using (ConfirmReject frm = new ConfirmReject())
            {
                frm.Text = "อนุมัติ";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open();

                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

                    SqlTransaction tr = con.BeginTransaction();
                    sqlCommand.Transaction = tr;
                    if (lbl_DocId.Text != "")
                    {
                        try
                        {
                            sqlCommand.CommandText = string.Format(
                            @"UPDATE SPC_JN_CANCLEDOCHD SET HEADAPPROVED = '2'
								,HEADAPPROVEDBY = @HEADAPPROVEDBY
								,HEADAPPROVEDBYNAME = @HEADAPPROVEDBYNAME
								,HEADAPPROVEDDATE = convert(varchar, getdate(), 23)
                                ,HEADAPPORVEREMARK = '{0}'
                              WHERE DOCID = @DOCID
                              AND DOCSTAT = 1 ", frm.Remark);

                            sqlCommand.Parameters.AddWithValue("@DOCID", _docid);
                            sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDBY", ClassCurUser.LogInEmplId);
                            sqlCommand.Parameters.AddWithValue("@HEADAPPROVEDBYNAME", ClassCurUser.LogInEmplName);
                            sqlCommand.ExecuteNonQuery();

                            tr.Commit();
                            this.IsSaveOnce = true;
                            // MessageBox.Show("เอกสารเลขที่เอกสาร : " + txtDocId.Text + " ทำรายการเรียบร้อยแล้ว");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            if (con.State == ConnectionState.Open) con.Close();
                            this.Close();

                            this.DialogResult = DialogResult.Yes;
                        }
                    }
                }
            }
        }
    }
}
