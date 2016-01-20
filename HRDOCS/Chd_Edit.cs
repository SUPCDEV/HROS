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
    public partial class Chd_Edit : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        protected string emplId;
        public string EmplId
        {
            get { return emplId; }
            set { emplId = value; }
        }
        protected string key;
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        protected string emplname;
        public string EmplName
        {
            get { return emplname; }
            set { emplname = value; }
        }

        public Chd_Edit()
        {
            InitializeComponent();

            InitializeGridView();

            btn_Edit.Click += new EventHandler(btn_Edit_Click);
            Btn_Delete.Click += new EventHandler(Btn_Delete_Click);
        }

        void Btn_Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องยกเลิกเอกสารนี้ ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                SqlTransaction tr = con.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.Transaction = tr ;
                try
                {
                    sqlCommand.CommandText = string.Format(
                        @" UPDATE [SPC_JN_CHANGHOLIDAYHD] SET [DOCSTAT] = '0'
								,[MODIFIEDBY] = @MODIFIEDBY
								,[MODIFIEDNAME] = @MODIFIEDNAME
								,[MODIFIEDDATE] = @MODIFIEDDATE
                       WHERE [DOCID] = @DOCID ");
                    sqlCommand.Parameters.AddWithValue("@DOCID", rgv_ChdHD.CurrentRow.Cells["DOCID"].Value.ToString());
                    sqlCommand.Parameters.AddWithValue("@MODIFIEDBY", ClassCurUser.LogInEmplId);
                    sqlCommand.Parameters.AddWithValue("@MODIFIEDNAME", ClassCurUser.LogInEmplName);
                    sqlCommand.Parameters.AddWithValue("@MODIFIEDDATE", MyTime.GetDateTime());

                    sqlCommand.ExecuteNonQuery();

                    tr.Commit();
                    
                    this.IsSaveOnce = true;
                    
                    //โหลดข้อมูลใหม่
                    LoadDataHD();
                    LoadDataDT("");
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
        }

        void btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                using (Chd_EditDetail frm = new Chd_EditDetail(rgv_ChdHD.CurrentRow.Cells["DOCID"].Value.ToString()))
                {
                    frm.Text = "แก้ไขข้อมูล";

                    if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                    {
                        //โหลดข้อมูลใหม่
                        LoadDataHD();
                        if (rgv_ChdDT.Rows.Count > 0)
                        {
                            //rgv_ChdDT.Rows.Clear();
                            if (rgv_ChdHD.Rows.Count > 0)
                            {
                                LoadDataDT(rgv_ChdHD.CurrentRow.Cells["DOCID"].Value.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }
        private void Chd_Edit_Load(object sender, EventArgs e)
        {
            
            LoadDataHD();
            if (rgv_ChdHD.Rows.Count > 0)
            {
                LoadDataDT(rgv_ChdHD.CurrentRow.Cells["DOCID"].Value.ToString());
            }
        }

        void InitializeGridView()
        {
            GridViewHD();
            GridViewDT();
        }
        void GridViewHD()
        {

            this.rgv_ChdHD.Dock = DockStyle.Fill;
            this.rgv_ChdHD.ReadOnly = true;
            this.rgv_ChdHD.AutoGenerateColumns = true;
            this.rgv_ChdHD.EnableFiltering = false;
            this.rgv_ChdHD.AllowAddNewRow = false;
            this.rgv_ChdHD.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_ChdHD.ShowGroupedColumns = true;
            this.rgv_ChdHD.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_ChdHD.EnableHotTracking = true;
            this.rgv_ChdHD.AutoSizeRows = true;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_ChdHD.Columns.Add(DOCID);



            GridViewTextBoxColumn DOCSTAT = new GridViewTextBoxColumn();
            DOCSTAT.Name = "DOCSTAT";
            DOCSTAT.FieldName = "DOCSTAT";
            DOCSTAT.HeaderText = "สถานะเอกสาร";
            DOCSTAT.IsVisible = true;
            DOCSTAT.ReadOnly = true;
            DOCSTAT.BestFit();
            rgv_ChdHD.Columns.Add(DOCSTAT);

            GridViewTextBoxColumn EMPLNAME = new GridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.FieldName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.IsVisible = true;
            EMPLNAME.ReadOnly = true;
            EMPLNAME.BestFit();
            rgv_ChdHD.Columns.Add(EMPLNAME);


            GridViewTextBoxColumn HEADAPPROVED = new GridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.FieldName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.IsVisible = true;
            HEADAPPROVED.ReadOnly = true;
            HEADAPPROVED.BestFit();
            rgv_ChdHD.Columns.Add(HEADAPPROVED);

            GridViewTextBoxColumn HEADAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.FieldName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.IsVisible = true;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            HEADAPPROVEDBYNAME.BestFit();
            rgv_ChdHD.Columns.Add(HEADAPPROVEDBYNAME);


            GridViewTextBoxColumn HRAPPROVED = new GridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.FieldName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.IsVisible = true;
            HRAPPROVED.ReadOnly = true;
            HRAPPROVED.BestFit();
            rgv_ChdHD.Columns.Add(HRAPPROVED);


            GridViewTextBoxColumn HRAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.FieldName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.IsVisible = true;
            HRAPPROVEDBYNAME.ReadOnly = true;
            HRAPPROVEDBYNAME.BestFit();
            rgv_ChdHD.Columns.Add(HRAPPROVEDBYNAME);



        }
        void GridViewDT()
        {
            this.rgv_ChdDT.Dock = DockStyle.Fill;
            this.rgv_ChdDT.ReadOnly = true;
            this.rgv_ChdDT.AutoGenerateColumns = true;
            this.rgv_ChdDT.EnableFiltering = false;
            this.rgv_ChdDT.AllowAddNewRow = false;
            this.rgv_ChdDT.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_ChdDT.ShowGroupedColumns = true;
            this.rgv_ChdDT.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_ChdDT.EnableHotTracking = true;
            this.rgv_ChdDT.AutoSizeRows = true;

            GridViewTextBoxColumn COUNTDOC = new GridViewTextBoxColumn();
            COUNTDOC.Name = "COUNTDOC";
            COUNTDOC.FieldName = "COUNTDOC";
            COUNTDOC.HeaderText = "จำนวน";
            COUNTDOC.IsVisible = true;
            COUNTDOC.ReadOnly = true;
            COUNTDOC.BestFit();
            rgv_ChdDT.Columns.Add(COUNTDOC);

            GridViewTextBoxColumn FROMHOLIDAY = new GridViewTextBoxColumn();
            FROMHOLIDAY.Name = "FROMHOLIDAY";
            FROMHOLIDAY.FieldName = "FROMHOLIDAY";
            FROMHOLIDAY.HeaderText = "วันที่มาทำ";
            FROMHOLIDAY.IsVisible = true;
            FROMHOLIDAY.ReadOnly = true;
            FROMHOLIDAY.BestFit();
            rgv_ChdDT.Columns.Add(FROMHOLIDAY);

            GridViewTextBoxColumn TOSHIFTID = new GridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.FieldName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "กะที่มาทำ";
            TOSHIFTID.IsVisible = true;
            TOSHIFTID.ReadOnly = true;
            TOSHIFTID.BestFit();
            rgv_ChdDT.Columns.Add(TOSHIFTID);


            GridViewTextBoxColumn TOSHIFTDESC = new GridViewTextBoxColumn();
            TOSHIFTDESC.Name = "TOSHIFTDESC";
            TOSHIFTDESC.FieldName = "TOSHIFTDESC";
            TOSHIFTDESC.HeaderText = "คำอธิบาย";
            TOSHIFTDESC.IsVisible = true;
            TOSHIFTDESC.ReadOnly = true;
            TOSHIFTDESC.BestFit();
            rgv_ChdDT.Columns.Add(TOSHIFTDESC);

            GridViewTextBoxColumn TOHOLIDAY = new GridViewTextBoxColumn();
            TOHOLIDAY.Name = "TOHOLIDAY";
            TOHOLIDAY.FieldName = "TOHOLIDAY";
            TOHOLIDAY.HeaderText = "วันที่หยุด";
            TOHOLIDAY.IsVisible = true;
            TOHOLIDAY.ReadOnly = true;
            TOHOLIDAY.BestFit();
            rgv_ChdDT.Columns.Add(TOHOLIDAY);

            GridViewTextBoxColumn REASON = new GridViewTextBoxColumn();
            REASON.Name = "REASON";
            REASON.FieldName = "REASON";
            REASON.HeaderText = "เหตุผล";
            REASON.IsVisible = true;
            REASON.ReadOnly = true;
            REASON.BestFit();
            rgv_ChdDT.Columns.Add(REASON);

            GridViewTextBoxColumn TRANSDATE = new GridViewTextBoxColumn();
            TRANSDATE.Name = "TRANSDATE";
            TRANSDATE.FieldName = "TRANSDATE";
            TRANSDATE.HeaderText = "วันที่บันทึก";
            TRANSDATE.IsVisible = true;
            TRANSDATE.ReadOnly = true;
            TRANSDATE.BestFit();
            rgv_ChdDT.Columns.Add(TRANSDATE);
        }
        void LoadDataHD()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                    DataTable dt = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;

                    sqlCommand.CommandText = string.Format(
                        @" SELECT DOCID 
                            ,CASE DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                            ,EMPLID,EMPLNAME
                            ,CASE HEADAPPROVED WHEN '0' THEN 'รออนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                            ,CASE HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HEADAPPROVEDBYNAME
                            ,CASE HRAPPROVED WHEN '0' THEN 'รออนุมัติ' ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                            ,CASE HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                           FROM [SPC_JN_CHANGHOLIDAYHD]                                                      
                           WHERE DOCSTAT != 0                           
                           AND HEADAPPROVED = 0
						   AND (EMPLID = '{0}' OR [CREATEDBY] = '{0}') "

                        , ClassCurUser.LogInEmplId );

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_ChdHD.DataSource = dt;
                        Btn_Delete.Enabled = true;
                        btn_Edit.Enabled = true;
                    }
                    else
                    {
                        rgv_ChdHD.DataSource = dt;
                        
                        Btn_Delete.Enabled = false;
                        btn_Edit.Enabled = false;
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
        void LoadDataDT(string DOCNO)
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlTransaction tr = null;
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                sqlCommand.CommandText = string.Format(
                    @" SELECT DT.COUNTDOC ,HD.DOCID, HD.EMPLID ,HD.EMPLNAME
                                ,HD.SECTIONNAME ,HD.DEPTNAME
                                ,HD.HOLIDAY1 ,HD.HOLIDAY2                                
                                ,DT.FROMHOLIDAY AS FROMHOLIDAY 
                                ,CASE DT.TOHOLIDAY WHEN '0000-00-00' THEN '* แจ้งภายหลัง'  
												  ELSE DT.TOHOLIDAY END AS TOHOLIDAY 
                                ,DT.TOSHIFTID AS TOSHIFTID ,DT.TOSHIFTDESC AS TOSHIFTDESC ,DT.REASON AS REASON
                                ,CONVERT(VARCHAR ,HD.TRANSDATE,23)  AS TRANSDATE 
                                FROM SPC_JN_CHANGHOLIDAYDT DT
	                                 LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD ON DT.DOCID = HD.DOCID
                                WHERE HD.DOCSTAT !=0 
                                AND HD.HEADAPPROVED  = 0
                                AND HD.DOCID = '{0}' " , DOCNO);

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_ChdDT.DataSource = dt;
                }
                else
                {
                    rgv_ChdDT.DataSource = dt;
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

        private void rgv_ChdHD_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LoadDataDT(rgv_ChdHD.CurrentRow.Cells["DOCID"].Value.ToString());
            }
        }        
    }
}
