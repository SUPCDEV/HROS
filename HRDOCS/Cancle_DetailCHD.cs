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
    public partial class Cancle_DetailCHD : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);
        string CHD_id ;
        string _doctyp = "002";

        string _EXPIREDATE = "";
        string ApproveID = "";
        string _enplid = "";
        string _emplname = "";
        string _sectionid = ""; 
        string _sectionname = "";
        string _deptid = ""; 
        string _deptname = "";
       
        public Cancle_DetailCHD(string CHD_DocId)
        {
            CHD_id = CHD_DocId;
            InitializeComponent();

            this.tbn_Save.Click += new EventHandler(tbn_Save_Click);

            _EXPIREDATE = DateTime.Now.AddDays(+3).Date.ToString("yyyy-MM-dd");

            this.btnSearch.Click += new EventHandler(btnSearch_Click);

            #region GridView
            this.rgv_StatusDoc.Dock = DockStyle.Fill;
           // this.rgv_StatusDoc.ReadOnly = true;
            this.rgv_StatusDoc.AutoGenerateColumns = true;
            this.rgv_StatusDoc.EnableFiltering = false;
            this.rgv_StatusDoc.AllowAddNewRow = false;
            this.rgv_StatusDoc.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_StatusDoc.ShowGroupedColumns = true;
            this.rgv_StatusDoc.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_StatusDoc.EnableHotTracking = true;
            this.rgv_StatusDoc.AutoSizeRows = true;
            #endregion

            
            this.GetDataCHD();
            this.GetdataEmpl();
        }

        void tbn_Save_Click(object sender, EventArgs e)
        {
            InsertData();
        }
        void GetDataCHD()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                        @" SELECT HD.DOCID ,CONVERT(VARCHAR, HD.TRANSDATE , 23) AS TRANSDATE
                            ,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.FROMHOLIDAY 
                            ,DT.TOHOLIDAY AS TOHOLIDAY
                            ,CASE DT.TOSHIFTID WHEN '' THEN '-'  
												  ELSE DT.TOSHIFTID END AS TOSHIFTID
                            ,CASE DT.TOSHIFTDESC WHEN '' THEN '-'  
													ELSE DT.TOSHIFTDESC END AS TOSHIFTDESC ,DT.REASON
                            ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ' 
												  WHEN '1' THEN 'อนุมัติ' 
												  WHEN '2' THEN 'ไม่อนุมัติ'  
												  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,HD.HEADAPPROVEDBYNAME AS HEADAPPROVEDBYNAME
                            ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' 
												WHEN '1' THEN 'อนุมัติ'
												WHEN '2' THEN 'ไม่อนุมัติ'  
												ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,HD.HRAPPROVEDBYNAME AS HRAPPROVEDBYNAME 
                            ,CASE HD.HRAPPORVEREMARK  WHEN '' THEN '-' 
														ELSE HD.HRAPPORVEREMARK END AS HRAPPORVEREMARK
                            ,CASE HD.DOCSTAT WHEN '1' THEN 'ใช้งาน'  
												WHEN '0' THEN 'ยกเลิก' 
												   ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                           FROM SPC_JN_CHANGHOLIDAYDT DT LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD
		                            ON DT.DOCID = HD.DOCID                                                        
                           WHERE HD.DOCSTAT != 0 
                            AND HD.DOCID = '{0}'
                           AND DT.TOHOLIDAY NOT IN 
	                                    ( 
		                                    SELECT DATEREFER FROM [SPC_JN_CANCLEDOCDT] 
                                            WHERE DOCREFER = '{0}'
	                                    )", CHD_id );

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_StatusDoc.DataSource = dt;
                    }
                    else
                    {
                        rgv_StatusDoc.DataSource = dt;
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
        void GetdataEmpl()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                string sqlemp = string.Format(
                    @" SELECT * FROM SPC_JN_CHANGHOLIDAYHD
                        WHERE [DOCID] = '{0}' ", CHD_id);

                SqlCommand cmd = new SqlCommand(sqlemp, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _enplid = reader["EMPLID"].ToString();
                        _emplname = reader["EMPLNAME"].ToString().Trim();
                        _deptid = reader["DEPTID"].ToString().Trim();
                        _deptname = reader["DEPTNAME"].ToString().Trim();
                        _sectionid = reader["SECTIONID"].ToString();
                        _sectionname = reader["SECTIONNAME"].ToString();
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
        void CheckApproveID()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@" select * from SPC_CM_AUTHORIZE
                                                                where EMPLID = '{0}'
                                                                order by APPROVEID ", ClassCurUser.LogInEmplId);
                    //, ClassCurUser.LogInEmplId);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            if (dataTable.Rows.Count > 0)
                            {
                                dataTable.Clear();
                            }
                            dataTable.Load(sqlDataReader);
                        }
                    }
                }

                if (dataTable.Rows.Count > 0)
                {
                    ApproveID = dataTable.Rows[0]["APPROVEID"].ToString();
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }
        void InsertData()
        {
            int rowAffectedMs = 0;
            int rowAffectedDt = 0;

            int Checktrue = 0;
            for (int i = 0; i <= rgv_StatusDoc.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(rgv_StatusDoc.Rows[i].Cells["CHECK"].Value) == true)
                {
                    Checktrue++;
                }
            }
            if (Checktrue == 0)
            {
                MessageBox.Show("กรุณาเลือกรายชื่อที่ต้องการอนุมัติ");
                return;
            }
            if (txtShift.Text == "" || txtShiftDesc.Text == "" || txt_Remark.Text == "")
            {
                MessageBox.Show("กรุณาใส่ข้อมูลให้ครบ...", "แจ้งเตือน");
                return;
            }
            if (MessageBox.Show("คุณต้องการยกเลิกเอกสาร ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }



            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            SqlTransaction tr = con.BeginTransaction();
            
            string tmpGenDocId = ClassRunDoc.runDocno("SPC_JN_CANCLEDOCHD", "DOCID", "CN");
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            sqlCommand.Transaction = tr;

            try
            {
                #region sql_HD
                sqlCommand.CommandText = string.Format(
                                            @"INSERT INTO SPC_JN_CANCLEDOCHD (
                                                DOCID,TRANSDATE
                                                ,EMPLID,EMPLNAME
                                                ,SECTIONID,SECTIONNAME
                                                ,DEPTID,DEPTNAME                                                                                        
                                                ,CREATEDBY,CREATEDNAME,CREATEDDATE
                                                ,MODIFIEDBY,MODIFIEDNAME,MODIFIEDDATE
                                                ,EXPIREDATE
                                            )                                            
                                        VALUES (
                                                @DOCID1,convert(varchar, getdate(), 23)
                                                ,@EMPLID,@EMPLNAME
                                                ,@SECTIONID,@SECTIONNAME
                                                ,@DEPTID,@DEPTNAME                                                                                                
                                                ,@CREATEDBY,@CREATEDNAME,convert(varchar, getdate(), 23)
                                                ,@MODIFIEDBY,@MODIFIEDNAME,convert(varchar, getdate(), 23)  
                                                ,@EXPIREDATE
                                                )");

                sqlCommand.Parameters.AddWithValue("@DOCID1", tmpGenDocId);
                sqlCommand.Parameters.AddWithValue("@EMPLID", _enplid);
                sqlCommand.Parameters.AddWithValue("@EMPLNAME", _emplname);
                sqlCommand.Parameters.AddWithValue("@SECTIONID", _sectionid);
                sqlCommand.Parameters.AddWithValue("@SECTIONNAME", _sectionname);
                sqlCommand.Parameters.AddWithValue("@DEPTID", _deptid);
                sqlCommand.Parameters.AddWithValue("@DEPTNAME", _deptname);

                sqlCommand.Parameters.AddWithValue("@CREATEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@CREATEDNAME", ClassCurUser.LogInEmplName);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                sqlCommand.Parameters.AddWithValue("@MODIFIEDNAME", ClassCurUser.LogInEmplName);
                sqlCommand.Parameters.AddWithValue("@EXPIREDATE", _EXPIREDATE);

                // <WS:Modified> 2014-11-13
                //sqlCommand.ExecuteNonQuery();
                rowAffectedMs = sqlCommand.ExecuteNonQuery();
                // </WS:Modified>
                #endregion

                #region sql_DT
                
                foreach (GridViewDataRowInfo row in rgv_StatusDoc.Rows)
                {
                    if (row.Index > -1)
                    {
                        if (row.Cells["CHECK"].Value != null)
                        {
                            sqlCommand.CommandText = string.Format(
                                                @"INSERT INTO SPC_JN_CANCLEDOCDT (
                                                    DOCID,DOCTYP,SEQNO,DOCREFER,DATEREFER,FROMHOLIDAY_CHD
                                                    ,SHIFTID_CHD,SHIFTDESC_CHD,REASON,MODIFIEDDATE
                                                    )                                            
                                                VALUES (
                                                        @DOCID2{0}
                                                        ,@DOCTYP{0}
                                                        ,@SEQNO{0},@DOCREFER{0} ,@DATEREFER{0},@FROMHOLIDAY_CHD{0}
                                                        ,@SHIFTID_CHD{0},@SHIFTDESC_CHD{0},@REASON{0}                                               
                                                        ,convert(varchar, getdate(), 23) )"
                                                , row.Index);

                           //Edit 21-03-15
                           //sqlCommand.Parameters.AddWithValue("@DOCID2", tmpGenDocId);
                            sqlCommand.Parameters.AddWithValue(string.Format(@"DOCID2{0}", row.Index), tmpGenDocId);
                           //sqlCommand.Parameters.AddWithValue("@DOCTYP", _doctyp);
                            sqlCommand.Parameters.AddWithValue(string.Format(@"DOCTYP{0}", row.Index), _doctyp);
                           //sqlCommand.Parameters.AddWithValue("@DOCREFER", CHD_id);

                            sqlCommand.Parameters.AddWithValue(string.Format(@"SEQNO{0}", row.Index ), row.Index + 1);

                            sqlCommand.Parameters.AddWithValue(string.Format(@"DOCREFER{0}", row.Index), CHD_id);
                            sqlCommand.Parameters.AddWithValue(string.Format(@"DATEREFER{0}", row.Index), row.Cells["TOHOLIDAY"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue(string.Format(@"FROMHOLIDAY_CHD{0}", row.Index), row.Cells["FROMHOLIDAY"].Value.ToString());
                           //sqlCommand.Parameters.AddWithValue("@REASON", txt_Remark.Text.ToString());
                            sqlCommand.Parameters.AddWithValue(string.Format(@"SHIFTID_CHD{0}", row.Index), txtShift.Text.ToString());
                            sqlCommand.Parameters.AddWithValue(string.Format(@"SHIFTDESC_CHD{0}", row.Index), txtShiftDesc.Text.ToString());
                            
                            sqlCommand.Parameters.AddWithValue(string.Format(@"REASON{0}", row.Index), txt_Remark.Text.ToString());
                           

                            // <WS:modified> 2014-11-13
                            //sqlCommand.ExecuteNonQuery();
                            rowAffectedDt += sqlCommand.ExecuteNonQuery();
                            // </WS:modified> 2014-11-13
                        }
                    }
                }
           
                #endregion


                if ((rowAffectedMs > 0) && (rowAffectedDt > 0))
                {
                    tr.Commit();
                    this.IsSaveOnce = true;
                    MessageBox.Show("การบันทึกเอกสารสำเร็จ เลขที่เอกสาร : " + tmpGenDocId + " สำเร็จ");

                }
                else
                {
                    MessageBox.Show("การบันทึกเอกสารสำเร็จ เลขที่เอกสาร : " + tmpGenDocId + " ไม่สำเร็จ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
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

        void btnSearch_Click(object sender, EventArgs e)
        {
            using (Cancle_SelectShifr frm = new Cancle_SelectShifr())
            
            {
                frm.Text = "ค้นหากะ";

                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                {
                    txtShift.Text = frm.Shift;
                    txtShiftDesc.Text = frm.ShiftDesc;
                }
            }
            //}
        }

      

    }
}
