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
    public partial class Cancle_DetailDL : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);
        
        string DL_id;

        string _doctyp = "001";
        string _EXPIREDATE = "";
        string ApproveID = "";

        string _enplid = "";
        string _emplname = "";
        string _sectionid = "";
        string _sectionname = "";
        string _deptid = "";
        string _deptname = "";


        public Cancle_DetailDL(string DL_DocId)
        {
            DL_id = DL_DocId;
            InitializeComponent();
            this.btn_Save.Click += new EventHandler(btn_Save_Click);
            _EXPIREDATE = DateTime.Now.AddDays(+3).Date.ToString("yyyy-MM-dd");
           
            //#region GridView
            //this.rgv_StatusDoc.Dock = DockStyle.Fill;
            ////this.rgv_StatusDoc.ReadOnly = true;
            //this.rgv_StatusDoc.AutoGenerateColumns = true;
            //this.rgv_StatusDoc.EnableFiltering = false;
            //this.rgv_StatusDoc.AllowAddNewRow = false;
            //this.rgv_StatusDoc.MasterTemplate.AutoGenerateColumns = false;
            //this.rgv_StatusDoc.ShowGroupedColumns = true;
            //this.rgv_StatusDoc.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            //this.rgv_StatusDoc.EnableHotTracking = true;
            //this.rgv_StatusDoc.AutoSizeRows = true;
            //#endregion

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

            this.GetDataDL();
            this.GetdataEmpl();
        }

        void btn_Save_Click(object sender, EventArgs e)
        {
            this.InsertData();
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
        void GetDataDL()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                sqlCommand.CommandText = string.Format(
                    @" SELECT HD.DLDOCNO,CONVERT(VARCHAR,HD.DOCDATE,23) AS DOCDATE, HD.EMPLID,HD.EMPLNAME
                        ,DT.LEAVEDATE, DT.LEAVETYPE,DT.LEAVETYPEDETAIL
                        ,CASE DT.ATTACH WHEN 0 THEN 'ไม่มี'
					                        WHEN 1 THEN 'มี'
					                          ELSE '' END AS ATTACH
                        , DT.LEAVEREMARK
                        ,CASE DT.HALFDAY WHEN 0 THEN 'ทั้งวัน' 
					                        WHEN 1 THEN 'ครึ่งวัน' 
						                        ELSE '' END AS HALFDAY 
                        ,DT.HALFDAYTIME1 ,DT.HALFDAYTIME2
                        FROM SPC_CM_LEAVEHD HD
                        LEFT OUTER JOIN SPC_CM_LEAVEDT DT ON HD.DLDOCNO = DT.DLDOCNO
                        WHERE HD.DOCSTAT != 0 
                        AND HD.DLDOCNO = '{0}'
                        AND DT.LEAVEDATE  NOT IN 
	                                                 ( 
					                                    SELECT DATEREFER FROM [SPC_JN_CANCLEDOCDT] 
                                                        WHERE DOCREFER = '{0}'
	                                                 )  ", DL_id);

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
                    @" SELECT * FROM SPC_CM_LEAVEHD
                        WHERE [DLDOCNO] = '{0}' ", DL_id);

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

            if (MessageBox.Show("คุณต้องการยกเลิกเอกสาร ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            SqlTransaction tr = con.BeginTransaction();
            //string tmpGenDocId = ClassDocId.runDocno("SPC_JN_CHANGHOLIDAYHD", "DOCID", "CHD");
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
                                                ,@EXPIREDATE )");

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
                                                DOCID,DOCTYP,SEQNO,DOCREFER,DATEREFER,REASON,MODIFIEDDATE
                                            )                                            
                                        VALUES (
                                                @DOCID2{0}
                                                ,@DOCTYP{0}
                                                ,@SEQNO{0},@DOCREFER{0} ,convert(varchar,@DATEREFER{0},23)
                                                ,@REASON{0}                                              
                                                ,convert(varchar, getdate(), 23) )", row.Index);
               // Edit 25-03-58
               // sqlCommand.Parameters.AddWithValue("@DOCID2", tmpGenDocId);
                sqlCommand.Parameters.AddWithValue(string.Format(@"DOCID2{0}", row.Index), tmpGenDocId);
               // sqlCommand.Parameters.AddWithValue("@DOCTYP", _doctyp);
                sqlCommand.Parameters.AddWithValue(string.Format(@"DOCTYP{0}", row.Index), _doctyp);
                sqlCommand.Parameters.AddWithValue(string.Format(@"SEQNO{0}", row.Index), row.Index + 1);
                sqlCommand.Parameters.AddWithValue(string.Format("@DOCREFER{0}",row.Index), DL_id);
                sqlCommand.Parameters.AddWithValue(string.Format(@"DATEREFER{0}", row.Index),row.Cells["LEAVEDATE"].Value);
                //sqlCommand.Parameters.AddWithValue(string.Format(@"DATEREFER{0}", row.Index), Convert.ToDateTime(row.Cells["LEAVEDATE"].Value).ToString("yyyy-MM-dd"));
               
                sqlCommand.Parameters.AddWithValue(string.Format(@"@REASON{0}", row.Index), txt_Remark.Text.ToString());

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
    }
}
