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
    public partial class Chd_ApproveHR_Detail : Form
    {
        protected bool IsSaveOnce = true;

        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);
        string _docid;
        public Chd_ApproveHR_Detail(string DocId)
        {
            _docid = DocId;
            InitializeComponent();
            
            InitializeDataGridView();

            this.btnApprove.Click += new EventHandler(btnApprove_Click);
            btnNonApprove.Click += new EventHandler(btnNonApprove_Click);
        }

        
        void InitializeDataGridView()
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

            GridViewTextBoxColumn COUNTDOC = new GridViewTextBoxColumn();
            COUNTDOC.Name = "COUNTDOC";
            COUNTDOC.FieldName = "COUNTDOC";
            COUNTDOC.HeaderText = "ลำดับ";
            COUNTDOC.IsVisible = true;
            COUNTDOC.ReadOnly = true;
            COUNTDOC.BestFit();
            rgv_EmpData.Columns.Add(COUNTDOC);

            GridViewTextBoxColumn FROMHOLIDAY = new GridViewTextBoxColumn();
            FROMHOLIDAY.Name = "FROMHOLIDAY";
            FROMHOLIDAY.FieldName = "FROMHOLIDAY";
            FROMHOLIDAY.HeaderText = "วันที่มาทำ";
            FROMHOLIDAY.IsVisible = true;
            FROMHOLIDAY.ReadOnly = true;
            FROMHOLIDAY.BestFit();
            rgv_EmpData.Columns.Add(FROMHOLIDAY);

            GridViewTextBoxColumn TOSHIFTID = new GridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.FieldName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "กะที่มาทำ";
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

            GridViewTextBoxColumn TOHOLIDAY = new GridViewTextBoxColumn();
            TOHOLIDAY.Name = "TOHOLIDAY";
            TOHOLIDAY.FieldName = "TOHOLIDAY";
            TOHOLIDAY.HeaderText = "วันที่หยุด";
            TOHOLIDAY.IsVisible = true;
            TOHOLIDAY.ReadOnly = true;
            TOHOLIDAY.BestFit();
            rgv_EmpData.Columns.Add(TOHOLIDAY);

            GridViewTextBoxColumn REASON = new GridViewTextBoxColumn();
            REASON.Name = "REASON";
            REASON.FieldName = "REASON";
            REASON.HeaderText = "เหตุผล";
            REASON.IsVisible = true;
            REASON.ReadOnly = true;
            REASON.BestFit();
            rgv_EmpData.Columns.Add(REASON);

            GridViewTextBoxColumn TRANSDATE = new GridViewTextBoxColumn();
            TRANSDATE.Name = "TRANSDATE";
            TRANSDATE.FieldName = "TRANSDATE";
            TRANSDATE.HeaderText = "วันที่เอกสาร";
            TRANSDATE.IsVisible = true;
            TRANSDATE.ReadOnly = true;
            TRANSDATE.BestFit();
            rgv_EmpData.Columns.Add(TRANSDATE);
            #endregion
        }

        private void Chd_ApproveHR_Detail_Load(object sender, EventArgs e)
        {
            this.GetData();
        }

        void GetData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            DataTable dt = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            #region HD
            try
            {
                sqlCommand.CommandText = string.Format(

                            @"SELECT HD.DOCID,HD.EMPLID,HD.EMPLNAME
                                ,HD.SECTIONNAME,HD.DEPTNAME
                                ,HD.HOLIDAY1,HD.HOLIDAY2 
                                ,HEADAPPORVEREMARK                               
                                FROM SPC_JN_CHANGHOLIDAYDT DT
	                                 LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD ON DT.DOCID = HD.DOCID
                                WHERE HD.DOCID = '{0}' 
                                AND HEADAPPROVED  = 1
                                AND HRAPPROVED  = 0
                                AND DOCSTAT = 1 "
                                , _docid.ToString());


                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtDocId.Text = reader["DOCID"].ToString();
                        txtEmpId.Text = reader["EMPLID"].ToString();
                        txtEmpName.Text = reader["EMPLNAME"].ToString();
                        txtSection.Text = reader["SECTIONNAME"].ToString();
                        txtPosition.Text = reader["DEPTNAME"].ToString();
                        txtholiday1.Text = reader["HOLIDAY1"].ToString();
                        txtHoloday2.Text = reader["HOLIDAY2"].ToString();
                        txtRemark.Text = reader["HEADAPPORVEREMARK"].ToString();
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
            #endregion

            #region DT

            try
            {
                sqlCommand.CommandText = string.Format(

                            @"SELECT DT.COUNTDOC ,HD.DOCID, HD.EMPLID ,HD.EMPLNAME
                                ,HD.SECTIONNAME ,HD.DEPTNAME
                                ,HD.HOLIDAY1 ,HD.HOLIDAY2                                
                                ,DT.FROMHOLIDAY AS FROMHOLIDAY 
                                ,CASE DT.TOHOLIDAY WHEN '0000-00-00' THEN '* แจ้งภายหลัง'  
												  ELSE DT.TOHOLIDAY END AS TOHOLIDAY 
                                ,DT.TOSHIFTID AS TOSHIFTID ,DT.TOSHIFTDESC AS TOSHIFTDESC ,DT.REASON AS REASON
                                ,CONVERT(VARCHAR ,HD.TRANSDATE,23)  AS TRANSDATE 
                                FROM SPC_JN_CHANGHOLIDAYDT DT
	                                 LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD ON DT.DOCID = HD.DOCID
                                WHERE HD.DOCID = '{0}' 
                                AND HEADAPPROVED  = 1
                                AND HRAPPROVED= 0
                                AND DOCSTAT = 1 "
                                , _docid.ToString());

                

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

            #endregion
        }

        void UpdatateApprove()
        {
            if (MessageBox.Show("คุณต้องการอนุมัติเอกสาร : " + txtDocId.Text + " ใช่หรือไม่", "HRDOCS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            SqlTransaction tr = con.BeginTransaction();
            sqlCommand.Transaction = tr;
            if (txtDocId.Text != "")
            {
                try
                {
                    sqlCommand.CommandText = string.Format(
                        @"
                        UPDATE SPC_JN_CHANGHOLIDAYHD SET HRAPPROVED  = 1
                                 ,HRAPPROVEDBY = '{0}' 
                                 ,HRAPPROVEDBYNAME = '{1}'
								 ,HRAPPROVEDDATE = convert(varchar, getdate(), 23)
                        WHERE SPC_JN_CHANGHOLIDAYHD.DOCID = @DOCID
                        AND DOCSTAT = 1"
                        ,ClassCurUser.LogInEmplId
                        ,ClassCurUser.LogInEmplName );

                    sqlCommand.Parameters.AddWithValue("@DOCID", txtDocId.Text);
                    sqlCommand.ExecuteNonQuery();

                    tr.Commit();
                    this.IsSaveOnce = true;
                   // MessageBox.Show("อนุมัติเอกสารเลขที่เอกสาร : " + txtDocId.Text + " สำเร็จ");
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

        void UpdatateNonApprove(string remark)
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            SqlTransaction tr = con.BeginTransaction();
            sqlCommand.Transaction = tr;
            if (txtDocId.Text != "")
            {
                try
                {
                    sqlCommand.CommandText = string.Format(
                        @"
                        UPDATE SPC_JN_CHANGHOLIDAYHD SET HRAPPROVED  = 2
                                 ,HRAPPROVEDBY = '{0}' 
                                 ,HRAPPROVEDBYNAME = '{1}'								 
                                 ,HRAPPROVEDDATE = convert(varchar, getdate(), 23)
                                 ,HRAPPORVEREMARK = '{2}'                               
                        WHERE SPC_JN_CHANGHOLIDAYHD.DOCID = @DOCID
                        AND DOCSTAT = 1"
                        , ClassCurUser.LogInEmplId
                        , ClassCurUser.LogInEmplName
                        , remark);

                    sqlCommand.Parameters.AddWithValue("@DOCID", txtDocId.Text);
                    sqlCommand.ExecuteNonQuery();

                    tr.Commit();
                    this.IsSaveOnce = true;
                    MessageBox.Show("เอกสารเลขที่เอกสาร : " + txtDocId.Text + " ทำรายการเรียบร้อยแล้ว");
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
        void btnApprove_Click(object sender, EventArgs e)
        {
            UpdatateApprove();
        }
        void btnNonApprove_Click(object sender, EventArgs e)
        {
            string DocId = txtDocId.Text;
            using (Chd_ApproveHR_Remark frm = new Chd_ApproveHR_Remark(DocId))
            {
                //frm.Show();

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {
                    UpdatateNonApprove(frm.remark);

                    this.DialogResult = DialogResult.Yes;
                }
            }
        }
        
    }
}
