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
    public partial class Cancle_Edit : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string _dtdoctype;

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

        public Cancle_Edit()
        {
            InitializeComponent();

            InitializeGridView();

            btn_Edit.Click += new EventHandler(btn_Edit_Click);
        }

        private void Cancle_Edit_Load(object sender, EventArgs e)
        {
            LoadDataHD(); 
        }

        void btn_Edit_Click(object sender, EventArgs e)
        {
            
            try
            {
                //using (Chd_EditDetail frm = new Chd_EditDetail(rgv_HD.CurrentRow.Cells["DOCID"].Value.ToString()))
                //{
                string DocId = (rgv_HD.CurrentRow.Cells["DOCID"].Value.ToString());
                string Doctype = (rgv_HD.CurrentRow.Cells["DOCTYP"].Value.ToString().Trim());
                string Docrefer = (rgv_HD.CurrentRow.Cells["DOCREFER"].Value.ToString());

                if (Doctype == "001")
                {
                    using (Cancle_EditDetaiDL frm = new Cancle_EditDetaiDL(DocId, Doctype, Docrefer))
                    {
                        frm.Text = "แก้ไขข้อมูลยกเลิกเอกสารใบลาหยุด";

                        if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                        {
                            //โหลดข้อมูลใหม่
                            LoadDataHD();
                            rgv_DLDT.Visible = false;
                            
                        }
                    }
                }
                if (Doctype == "002")
                {
                    using (Cancle_EditDetailCHD frm = new Cancle_EditDetailCHD (DocId, Doctype ,Docrefer))
                    {
                        frm.Text = "แก้ไขข้อมูลยกเลิกเอกสารใบเปลี่ยนวันหยุด";

                        if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                        {
                            //โหลดข้อมูลใหม่
                            LoadDataHD();
                            rgv_CHDDT.Visible = false;
                            //if (rgv_CHDDT.Rows.Count > 0)
                            //{
                            //    rgv_CHDDT.Rows.Clear();
                            //    if (rgv_HD.Rows.Count > 0)
                            //    {
                            //        LoadDataCHDDT(rgv_HD.CurrentRow.Cells["DOCID"].Value.ToString(), rgv_HD.CurrentRow.Cells["DOCTYP"].Value.ToString());
                            //    }
                            //}
                        }
                    }
                }
                if (Doctype == "003")
                {
                    using (Cancle_EditDetailDS frm = new Cancle_EditDetailDS(DocId, Doctype, Docrefer))
                    {
                        frm.Text = "แก้ไขข้อมูลยกเลิกเอกสารใบเปลี่ยนกะ";

                        if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
                        {
                            //โหลดข้อมูลใหม่
                            LoadDataHD();
                            rgv_DSDT.Visible = false;
                            //if (rgv_CHDDT.Rows.Count > 0)
                            //{
                            //    rgv_DSDT.Rows.Clear();
                            //    if (rgv_HD.Rows.Count > 0)
                            //    {
                            //        LoadDataDSDT(rgv_HD.CurrentRow.Cells["DOCID"].Value.ToString(), rgv_HD.CurrentRow.Cells["DOCTYP"].Value.ToString());
                            //    }
                            //}
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region GridView

        void InitializeGridView()
        {
            GridViewHD();
            GridViewDLDT();
            GridViewCHDDT();
            GridViewDS();
        }

        void GridViewHD()
        {

            this.rgv_HD.Dock = DockStyle.Fill;
            this.rgv_HD.ReadOnly = true;
            this.rgv_HD.AutoGenerateColumns = true;
            this.rgv_HD.EnableFiltering = false;
            this.rgv_HD.AllowAddNewRow = false;
            this.rgv_HD.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_HD.ShowGroupedColumns = true;
            this.rgv_HD.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_HD.EnableHotTracking = true;
            this.rgv_HD.AutoSizeRows = true;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_HD.Columns.Add(DOCID);

            GridViewTextBoxColumn DOCTYP = new GridViewTextBoxColumn();
            DOCTYP.Name = "DOCTYP";
            DOCTYP.FieldName = "DOCTYP";
            DOCTYP.HeaderText = "ประเภท";
            DOCTYP.IsVisible = true;
            DOCTYP.ReadOnly = true;
            DOCTYP.BestFit();
            rgv_HD.Columns.Add(DOCTYP);

            GridViewTextBoxColumn DOCREFER = new GridViewTextBoxColumn();
            DOCREFER.Name = "DOCREFER";
            DOCREFER.FieldName = "DOCREFER";
            DOCREFER.HeaderText = "เลขที่ที่อ้างอิง";
            DOCREFER.IsVisible = true;
            DOCREFER.ReadOnly = true;
            DOCREFER.BestFit();
            rgv_HD.Columns.Add(DOCREFER);

            GridViewTextBoxColumn EMPLNAME = new GridViewTextBoxColumn();
            EMPLNAME.Name = "EMPLNAME";
            EMPLNAME.FieldName = "EMPLNAME";
            EMPLNAME.HeaderText = "ชื่อ";
            EMPLNAME.IsVisible = true;
            EMPLNAME.ReadOnly = true;
            EMPLNAME.BestFit();
            rgv_HD.Columns.Add(EMPLNAME);

            GridViewTextBoxColumn HEADAPPROVED = new GridViewTextBoxColumn();
            HEADAPPROVED.Name = "HEADAPPROVED";
            HEADAPPROVED.FieldName = "HEADAPPROVED";
            HEADAPPROVED.HeaderText = "หน./ผช.";
            HEADAPPROVED.IsVisible = true;
            HEADAPPROVED.ReadOnly = true;
            HEADAPPROVED.BestFit();
            rgv_HD.Columns.Add(HEADAPPROVED);

            GridViewTextBoxColumn HEADAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HEADAPPROVEDBYNAME.Name = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.FieldName = "HEADAPPROVEDBYNAME";
            HEADAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HEADAPPROVEDBYNAME.IsVisible = true;
            HEADAPPROVEDBYNAME.ReadOnly = true;
            HEADAPPROVEDBYNAME.BestFit();
            rgv_HD.Columns.Add(HEADAPPROVEDBYNAME);

            GridViewTextBoxColumn HRAPPROVED = new GridViewTextBoxColumn();
            HRAPPROVED.Name = "HRAPPROVED";
            HRAPPROVED.FieldName = "HRAPPROVED";
            HRAPPROVED.HeaderText = "บุคคล";
            HRAPPROVED.IsVisible = true;
            HRAPPROVED.ReadOnly = true;
            HRAPPROVED.BestFit();
            rgv_HD.Columns.Add(HRAPPROVED);

            GridViewTextBoxColumn HRAPPROVEDBYNAME = new GridViewTextBoxColumn();
            HRAPPROVEDBYNAME.Name = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.FieldName = "HRAPPROVEDBYNAME";
            HRAPPROVEDBYNAME.HeaderText = "ผู้อนุมัติ";
            HRAPPROVEDBYNAME.IsVisible = true;
            HRAPPROVEDBYNAME.ReadOnly = true;
            HRAPPROVEDBYNAME.BestFit();
            rgv_HD.Columns.Add(HRAPPROVEDBYNAME);

            GridViewTextBoxColumn DOCSTAT = new GridViewTextBoxColumn();
            DOCSTAT.Name = "DOCSTAT";
            DOCSTAT.FieldName = "DOCSTAT";
            DOCSTAT.HeaderText = "สถานะเอกสาร";
            DOCSTAT.IsVisible = true;
            DOCSTAT.ReadOnly = true;
            DOCSTAT.BestFit();
            rgv_HD.Columns.Add(DOCSTAT);

        }
        void GridViewDLDT()
        {
            this.rgv_DLDT.Dock = DockStyle.Fill;
            this.rgv_DLDT.ReadOnly = true;
            this.rgv_DLDT.AutoGenerateColumns = true;
            this.rgv_DLDT.EnableFiltering = false;
            this.rgv_DLDT.AllowAddNewRow = false;
            this.rgv_DLDT.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_DLDT.ShowGroupedColumns = true;
            this.rgv_DLDT.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_DLDT.EnableHotTracking = true;
            this.rgv_DLDT.AutoSizeRows = true;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_DLDT.Columns.Add(DOCID);

            GridViewTextBoxColumn TYPDESC = new GridViewTextBoxColumn();
            TYPDESC.Name = "TYPDESC";
            TYPDESC.FieldName = "TYPDESC";
            TYPDESC.HeaderText = "ประเภทเอกสาร";
            TYPDESC.IsVisible = true;
            TYPDESC.ReadOnly = true;
            TYPDESC.BestFit();
            rgv_DLDT.Columns.Add(TYPDESC);

            GridViewTextBoxColumn DLDOCNO = new GridViewTextBoxColumn();
            DLDOCNO.Name = "DLDOCNO";
            DLDOCNO.FieldName = "DLDOCNO";
            DLDOCNO.HeaderText = "เลขที่ที่อ้างอิง";
            DLDOCNO.IsVisible = true;
            DLDOCNO.ReadOnly = true;
            DLDOCNO.BestFit();
            rgv_DLDT.Columns.Add(DLDOCNO);

            GridViewTextBoxColumn LEAVEDATE = new GridViewTextBoxColumn();
            LEAVEDATE.Name = "LEAVEDATE";
            LEAVEDATE.FieldName = "LEAVEDATE";
            LEAVEDATE.HeaderText = "วันที่ลาหยุด";
            LEAVEDATE.IsVisible = true;
            LEAVEDATE.ReadOnly = true;
            LEAVEDATE.BestFit();
            rgv_DLDT.Columns.Add(LEAVEDATE);


            GridViewTextBoxColumn LEAVETYPEDETAIL = new GridViewTextBoxColumn();
            LEAVETYPEDETAIL.Name = "LEAVETYPEDETAIL";
            LEAVETYPEDETAIL.FieldName = "LEAVETYPEDETAIL";
            LEAVETYPEDETAIL.HeaderText = "ประเภทการลา";
            LEAVETYPEDETAIL.IsVisible = true;
            LEAVETYPEDETAIL.ReadOnly = true;
            LEAVETYPEDETAIL.BestFit();
            rgv_DLDT.Columns.Add(LEAVETYPEDETAIL);

            GridViewTextBoxColumn ATTACH = new GridViewTextBoxColumn();
            ATTACH.Name = "ATTACH";
            ATTACH.FieldName = "ATTACH";
            ATTACH.HeaderText = "ใบรับรองแพทย์";
            ATTACH.IsVisible = true;
            ATTACH.ReadOnly = true;
            ATTACH.BestFit();
            rgv_DLDT.Columns.Add(ATTACH);

            GridViewTextBoxColumn HALFDAY = new GridViewTextBoxColumn();
            HALFDAY.Name = "HALFDAY";
            HALFDAY.FieldName = "HALFDAY";
            HALFDAY.HeaderText = "จำนวนวัน";
            HALFDAY.IsVisible = true;
            HALFDAY.ReadOnly = true;
            HALFDAY.BestFit();
            rgv_DLDT.Columns.Add(HALFDAY);

            GridViewTextBoxColumn HALFDAYTIME1 = new GridViewTextBoxColumn();
            HALFDAYTIME1.Name = "HALFDAYTIME1";
            HALFDAYTIME1.FieldName = "HALFDAYTIME1";
            HALFDAYTIME1.HeaderText = "ตั้งแต่เวลา";
            HALFDAYTIME1.IsVisible = true;
            HALFDAYTIME1.ReadOnly = true;
            HALFDAYTIME1.BestFit();
            rgv_DLDT.Columns.Add(HALFDAYTIME1);

            GridViewTextBoxColumn HALFDAYTIME2 = new GridViewTextBoxColumn();
            HALFDAYTIME2.Name = "HALFDAYTIME2";
            HALFDAYTIME2.FieldName = "HALFDAYTIME2";
            HALFDAYTIME2.HeaderText = "ถึงเวลา";
            HALFDAYTIME2.IsVisible = true;
            HALFDAYTIME2.ReadOnly = true;
            HALFDAYTIME2.BestFit();
            rgv_DLDT.Columns.Add(HALFDAYTIME2);

            GridViewTextBoxColumn LEAVEREMARK = new GridViewTextBoxColumn();
            LEAVEREMARK.Name = "LEAVEREMARK";
            LEAVEREMARK.FieldName = "LEAVEREMARK";
            LEAVEREMARK.HeaderText = "เหตุผล";
            LEAVEREMARK.IsVisible = true;
            LEAVEREMARK.ReadOnly = true;
            LEAVEREMARK.BestFit();
            rgv_DLDT.Columns.Add(LEAVEREMARK);

            GridViewTextBoxColumn CREATEDDATE = new GridViewTextBoxColumn();
            CREATEDDATE.Name = "CREATEDDATE";
            CREATEDDATE.FieldName = "CREATEDDATE";
            CREATEDDATE.HeaderText = "วันที่บันทึก";
            CREATEDDATE.IsVisible = true;
            CREATEDDATE.ReadOnly = true;
            CREATEDDATE.BestFit();
            rgv_DLDT.Columns.Add(CREATEDDATE);
        }
        void GridViewCHDDT()
        {
            this.rgv_CHDDT.Dock = DockStyle.Fill;
            this.rgv_CHDDT.ReadOnly = true;
            this.rgv_CHDDT.AutoGenerateColumns = true;
            this.rgv_CHDDT.EnableFiltering = false;
            this.rgv_CHDDT.AllowAddNewRow = false;
            this.rgv_CHDDT.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_CHDDT.ShowGroupedColumns = true;
            this.rgv_CHDDT.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_CHDDT.EnableHotTracking = true;
            this.rgv_CHDDT.AutoSizeRows = true;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_CHDDT.Columns.Add(DOCID);

            GridViewTextBoxColumn TYPDESC = new GridViewTextBoxColumn();
            TYPDESC.Name = "TYPDESC";
            TYPDESC.FieldName = "TYPDESC";
            TYPDESC.HeaderText = "ประเภทเอกสาร";
            TYPDESC.IsVisible = true;
            TYPDESC.ReadOnly = true;
            TYPDESC.BestFit();
            rgv_CHDDT.Columns.Add(TYPDESC);

            GridViewTextBoxColumn CHG_DOCID = new GridViewTextBoxColumn();
            CHG_DOCID.Name = "CHG_DOCID";
            CHG_DOCID.FieldName = "CHG_DOCID";
            CHG_DOCID.HeaderText = "เลขที่ที่อ้างอิง";
            CHG_DOCID.IsVisible = true;
            CHG_DOCID.ReadOnly = true;
            CHG_DOCID.BestFit();
            rgv_CHDDT.Columns.Add(CHG_DOCID);

            GridViewTextBoxColumn FROMHOLIDAY = new GridViewTextBoxColumn();
            FROMHOLIDAY.Name = "FROMHOLIDAY";
            FROMHOLIDAY.FieldName = "FROMHOLIDAY";
            FROMHOLIDAY.HeaderText = "วันที่มาทำ";
            FROMHOLIDAY.IsVisible = true;
            FROMHOLIDAY.ReadOnly = true;
            FROMHOLIDAY.BestFit();
            rgv_CHDDT.Columns.Add(FROMHOLIDAY);

            GridViewTextBoxColumn TOSHIFTID = new GridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.FieldName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "กะที่มาทำ";
            TOSHIFTID.IsVisible = true;
            TOSHIFTID.ReadOnly = true;
            TOSHIFTID.BestFit();
            rgv_CHDDT.Columns.Add(TOSHIFTID);


            GridViewTextBoxColumn TOSHIFTDESC = new GridViewTextBoxColumn();
            TOSHIFTDESC.Name = "TOSHIFTDESC";
            TOSHIFTDESC.FieldName = "TOSHIFTDESC";
            TOSHIFTDESC.HeaderText = "คำอธิบาย";
            TOSHIFTDESC.IsVisible = true;
            TOSHIFTDESC.ReadOnly = true;
            TOSHIFTDESC.BestFit();
            rgv_CHDDT.Columns.Add(TOSHIFTDESC);

            GridViewTextBoxColumn TOHOLIDAY = new GridViewTextBoxColumn();
            TOHOLIDAY.Name = "TOHOLIDAY";
            TOHOLIDAY.FieldName = "TOHOLIDAY";
            TOHOLIDAY.HeaderText = "วันที่หยุด";
            TOHOLIDAY.IsVisible = true;
            TOHOLIDAY.ReadOnly = true;
            TOHOLIDAY.BestFit();
            rgv_CHDDT.Columns.Add(TOHOLIDAY);

            GridViewTextBoxColumn CHG_REASON = new GridViewTextBoxColumn();
            CHG_REASON.Name = "CHG_REASON";
            CHG_REASON.FieldName = "CHG_REASON";
            CHG_REASON.HeaderText = "เหตุผล";
            CHG_REASON.IsVisible = true;
            CHG_REASON.ReadOnly = true;
            CHG_REASON.BestFit();
            rgv_CHDDT.Columns.Add(CHG_REASON);

            GridViewTextBoxColumn TRANSDATE = new GridViewTextBoxColumn();
            TRANSDATE.Name = "TRANSDATE";
            TRANSDATE.FieldName = "TRANSDATE";
            TRANSDATE.HeaderText = "วันที่บันทึก";
            TRANSDATE.IsVisible = true;
            TRANSDATE.ReadOnly = true;
            TRANSDATE.BestFit();
            rgv_CHDDT.Columns.Add(TRANSDATE);
        }
        void GridViewDS()
        {

            this.rgv_DSDT.Dock = DockStyle.Fill;
            this.rgv_DSDT.ReadOnly = true;
            this.rgv_DSDT.AutoGenerateColumns = true;
            this.rgv_DSDT.EnableFiltering = false;
            this.rgv_DSDT.AllowAddNewRow = false;
            this.rgv_DSDT.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_DSDT.ShowGroupedColumns = true;
            this.rgv_DSDT.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_DSDT.EnableHotTracking = true;
            this.rgv_DSDT.AutoSizeRows = true;

            GridViewTextBoxColumn DOCID = new GridViewTextBoxColumn();
            DOCID.Name = "DOCID";
            DOCID.FieldName = "DOCID";
            DOCID.HeaderText = "เลขที่เอกสาร";
            DOCID.IsVisible = true;
            DOCID.ReadOnly = true;
            DOCID.BestFit();
            rgv_DSDT.Columns.Add(DOCID);

            GridViewTextBoxColumn TYPDESC = new GridViewTextBoxColumn();
            TYPDESC.Name = "TYPDESC";
            TYPDESC.FieldName = "TYPDESC";
            TYPDESC.HeaderText = "ประเภทเอกสาร";
            TYPDESC.IsVisible = true;
            TYPDESC.ReadOnly = true;
            TYPDESC.BestFit();
            rgv_DSDT.Columns.Add(TYPDESC);

            GridViewTextBoxColumn DSDOCNO = new GridViewTextBoxColumn();
            DSDOCNO.Name = "DSDOCNO";
            DSDOCNO.FieldName = "DSDOCNO";
            DSDOCNO.HeaderText = "เลขที่ที่อ้างอิง";
            DSDOCNO.IsVisible = true;
            DSDOCNO.ReadOnly = true;
            DSDOCNO.BestFit();
            rgv_DSDT.Columns.Add(DSDOCNO);

            GridViewTextBoxColumn SHIFTDATE = new GridViewTextBoxColumn();
            SHIFTDATE.Name = "SHIFTDATE";
            SHIFTDATE.FieldName = "SHIFTDATE";
            SHIFTDATE.HeaderText = "วันที่เปลี่ยนกะ";
            SHIFTDATE.IsVisible = true;
            SHIFTDATE.ReadOnly = true;
            SHIFTDATE.BestFit();
            rgv_DSDT.Columns.Add(SHIFTDATE);

            GridViewTextBoxColumn FROMSHIFTID = new GridViewTextBoxColumn();
            FROMSHIFTID.Name = "FROMSHIFTID";
            FROMSHIFTID.FieldName = "FROMSHIFTID";
            FROMSHIFTID.HeaderText = "กะเดิม";
            FROMSHIFTID.IsVisible = true;
            FROMSHIFTID.ReadOnly = true;
            FROMSHIFTID.BestFit();
            rgv_DSDT.Columns.Add(FROMSHIFTID);

            GridViewTextBoxColumn FROMSHIFTDESC = new GridViewTextBoxColumn();
            FROMSHIFTDESC.Name = "FROMSHIFTDESC";
            FROMSHIFTDESC.FieldName = "FROMSHIFTDESC";
            FROMSHIFTDESC.HeaderText = "คำอธิบาย";
            FROMSHIFTDESC.IsVisible = true;
            FROMSHIFTDESC.ReadOnly = true;
            FROMSHIFTDESC.BestFit();
            rgv_DSDT.Columns.Add(FROMSHIFTDESC);

            GridViewTextBoxColumn TOSHIFTID = new GridViewTextBoxColumn();
            TOSHIFTID.Name = "TOSHIFTID";
            TOSHIFTID.FieldName = "TOSHIFTID";
            TOSHIFTID.HeaderText = "กะใหม่";
            TOSHIFTID.IsVisible = true;
            TOSHIFTID.ReadOnly = true;
            TOSHIFTID.BestFit();
            rgv_DSDT.Columns.Add(TOSHIFTID);

            GridViewTextBoxColumn TOSHIFTDESC = new GridViewTextBoxColumn();
            TOSHIFTDESC.Name = "TOSHIFTDESC";
            TOSHIFTDESC.FieldName = "TOSHIFTDESC";
            TOSHIFTDESC.HeaderText = "คำอธิบาย";
            TOSHIFTDESC.IsVisible = true;
            TOSHIFTDESC.ReadOnly = true;
            TOSHIFTDESC.BestFit();
            rgv_DSDT.Columns.Add(TOSHIFTDESC);

            GridViewTextBoxColumn REMARK = new GridViewTextBoxColumn();
            REMARK.Name = "REMARK";
            REMARK.FieldName = "REMARK";
            REMARK.HeaderText = "เหตุผล";
            REMARK.IsVisible = true;
            REMARK.ReadOnly = true;
            REMARK.BestFit();
            rgv_DSDT.Columns.Add(REMARK);

            GridViewTextBoxColumn CREATEDDATE = new GridViewTextBoxColumn();
            CREATEDDATE.Name = "CREATEDDATE";
            CREATEDDATE.FieldName = "CREATEDDATE";
            CREATEDDATE.HeaderText = "วันที่บันทึก";
            CREATEDDATE.IsVisible = true;
            CREATEDDATE.ReadOnly = true;
            CREATEDDATE.BestFit();
            rgv_DSDT.Columns.Add(CREATEDDATE);

        }

        #endregion

        #region Load Data
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
                    @" SELECT HD.DOCID 
                        ,CASE HD.DOCSTAT WHEN '1' THEN 'ใช้งาน'  WHEN '0' THEN 'ยกเลิก' ELSE 'ไม่มีข้อมูล' END AS DOCSTAT
                        ,HD.EMPLID ,HD.EMPLNAME
                        ,CASE HD.HEADAPPROVED WHEN '0' THEN 'รออนุมัติ'  ELSE 'ไม่มีข้อมูล' END AS HEADAPPROVED
                        ,CASE HD.HEADAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HEADAPPROVEDBYNAME
                        ,CASE HD.HRAPPROVED WHEN '0' THEN 'รออนุมัติ' ELSE 'ไม่มีข้อมูล' END AS HRAPPROVED 
                        ,CASE HD.HRAPPROVEDBYNAME WHEN null THEN ''  ELSE '-' END AS HRAPPROVEDBYNAME
                        ,DT.DOCREFER ,DT.DOCTYP
                        FROM SPC_JN_CANCLEDOCHD HD
	                        LEFT OUTER JOIN SPC_JN_CANCLEDOCDT DT ON HD.DOCID = DT.DOCID                                                  
                        WHERE DOCSTAT != 0                            
                        AND HEADAPPROVED = 0   
						AND (EMPLID = '{0}' OR [CREATEDBY] = '{0}') 
                        GROUP BY HD.DOCID,HD.DOCSTAT,HD.EMPLID ,HD.EMPLNAME,HD.HEADAPPROVED,HD.HEADAPPROVEDBYNAME
                                ,HD.HRAPPROVED,HD.HRAPPROVEDBYNAME,DT.DOCTYP ,DT.DOCREFER "
                    // AND HEADAPPROVED = 0
                    , ClassCurUser.LogInEmplId);


                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_HD.DataSource = dt;
                }
                else
                {
                    rgv_HD.DataSource = dt;
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
        void LoadDataDLDT(string DOCNO, string DOCTYP)
        {
            _dtdoctype = DOCTYP;
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlTransaction tr = null;
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                sqlCommand.Connection = con;

                if (_dtdoctype == "001")
                {
                    this.rgv_DLDT.Visible = true;
                    this.rgv_CHDDT.Visible = false;
                    this.rgv_DSDT.Visible = false;

                    sqlCommand.CommandText = string.Format(
                        @" SELECT HD.DOCID,DOCTYP.TYPDESC ,DLHD.DLDOCNO
                                ,CONVERT(VARCHAR,DLDT.LEAVEDATE,23) AS LEAVEDATE ,DLDT.LEAVETYPEDETAIL ,DLDT.LEAVEREMARK
                                ,CASE DLDT.ATTACH WHEN 0 THEN 'ไม่มี'
					                                WHEN 1 THEN 'มี'
					                                  ELSE '' END AS ATTACH
                                ,CASE DLDT.HALFDAY WHEN 0 THEN 'ทั้งวัน' 
					                                 WHEN 1 THEN 'ครึ่งวัน' 
						                                ELSE '' END AS HALFDAY 
                                ,DLDT.HALFDAYTIME1 ,DLDT.HALFDAYTIME2 ,CONVERT(VARCHAR,DLHD.CREATEDDATE,23) AS CREATEDDATE
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
	                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
	                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
	                                LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DLHD ON DT.DOCREFER = DLHD.DLDOCNO
	                                LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEDT] DLDT ON DLHD.DLDOCNO = DLDT.DLDOCNO
                                WHERE HD.DOCID = '{0}'
                                AND HD.DOCSTAT = '1' 
                                AND DT.DATEREFER  = DLDT.LEAVEDATE ", DOCNO.ToString());


                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_DLDT.DataSource = dt;
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
        void LoadDataCHDDT(string DOCNO, string DOCTYP)
        {
            _dtdoctype = DOCTYP;
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlTransaction tr = null;
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                sqlCommand.Connection = con;

                if (_dtdoctype == "002")
                {
                    this.rgv_DLDT.Visible = false;
                    this.rgv_CHDDT.Visible = true;
                    this.rgv_DSDT.Visible = false;

                    sqlCommand.CommandText = string.Format(

                           @"SELECT HD.DOCID, DOCTYP.TYPDESC,HD.DOCID,CHGHD.DOCID AS CHG_DOCID
                                    ,DOCTYP.TYPDESC ,DT.DOCREFER
                                    ,CHGDT.FROMHOLIDAY,CHGDT.TOHOLIDAY,CHGDT.TOSHIFTID,CHGDT.TOSHIFTDESC ,CHGDT.REASON AS CHG_REASON
                                    ,CONVERT(VARCHAR,CHGHD.TRANSDATE,23) AS TRANSDATE
                            FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                   LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                   LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                   LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYHD] CHGHD ON DT.DOCREFER = CHGHD.DOCID
	                               LEFT OUTER JOIN [dbo].[SPC_JN_CHANGHOLIDAYDT] CHGDT ON CHGHD.DOCID = CHGDT.DOCID
                            WHERE  HD.DOCID = '{0}'
                            AND HD.DOCSTAT = '1' 
                            AND CONVERT (VARCHAR,DT.DATEREFER,23) = CHGDT.TOHOLIDAY ", DOCNO.ToString());

                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_CHDDT.DataSource = dt;
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
        void LoadDataDSDT(string DOCNO, string DOCTYP)
        {
            _dtdoctype = DOCTYP;
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                DataTable dt = new DataTable();
                SqlTransaction tr = null;
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                sqlCommand.Connection = con;

                if (_dtdoctype == "003")
                {
                    this.rgv_DLDT.Visible = false;
                    this.rgv_CHDDT.Visible = false;
                    this.rgv_DSDT.Visible = true;

                    sqlCommand.CommandText = string.Format(
                          @"SELECT HD.DOCID, DOCTYP.TYPDESC ,DSHD.DSDOCNO
                                ,CONVERT(VARCHAR,DSDT.SHIFTDATE,23) AS SHIFTDATE 
                                ,DSDT.FROMSHIFTID ,DSDT.FROMSHIFTDESC ,DSDT.TOSHIFTID ,DSDT.TOSHIFTDESC ,DSDT.REMARK 
                                ,CONVERT(VARCHAR,DSHD.CREATEDDATE,23) AS CREATEDDATE
                            FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTHD] DSHD ON DT.DOCREFER = DSHD.DSDOCNO
                                LEFT OUTER JOIN [dbo].[SPC_CM_SHIFTDT] DSDT ON DSHD.DSDOCNO = DSDT.DSDOCNO
                            WHERE HD.DOCID = '{0}'
                            AND HD.DOCSTAT = '1'  
                            AND DT.DATEREFER  = DSDT.SHIFTDATE ", DOCNO.ToString());

                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rgv_DSDT.DataSource = dt;
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
        #endregion

        private void rgv_HD_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string DOCID = (rgv_HD.CurrentRow.Cells["DOCID"].Value.ToString().Trim());
                string DOCTYP = (rgv_HD.CurrentRow.Cells["DOCTYP"].Value.ToString().Trim());
               
                if (DOCTYP == "001")
                {
                    LoadDataDLDT(DOCID, DOCTYP);
                }
                else if (DOCTYP == "002")
                {
                    LoadDataCHDDT(DOCID, DOCTYP);
                }
                else if (DOCTYP == "003")
                {
                    LoadDataDSDT(DOCID, DOCTYP);
                }
                else
                {
                    return;
                }
            }

            #region MyRegion
            
            
            //if (e.RowIndex >= 0)
            //{
            //    if (e.Column.Name == "EditButton")
            //    {
            //        GridViewRowInfo row = (GridViewRowInfo)rgv_HD.Rows[e.RowIndex];

            //        string DocId = row.Cells["DOCID"].Value.ToString();
            //        string Doctype = row.Cells["DOCTYP"].Value.ToString().Trim();
            //        string Docrefer = row.Cells["DOCREFER"].Value.ToString().Trim();

            //        if (Doctype == "001")
            //        {
            //            using (Cancle_EditDetaiDL frm = new Cancle_EditDetaiDL(DocId, Doctype, Docrefer))
            //            {
            //                //frm.Show();
            //                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
            //                {
            //                    //โหลดข้อมูลใหม่
            //                    LoadDataHD();
            //                }
            //            }
            //        }
            //        else if (Doctype == "002")
            //        {
            //            using (Cancle_EditDetailCHD frm = new Cancle_EditDetailCHD(DocId, Doctype))
            //            {
            //                //frm.Show();
            //                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
            //                {
            //                    //โหลดข้อมูลใหม่
            //                    LoadDataHD();
            //                }
            //            }
            //        }
            //        else if (Doctype == "003")
            //        {
            //            using (Cancle_EditDetailDS frm = new Cancle_EditDetailDS(DocId, Doctype))
            //            {
            //                //frm.Show();
            //                if (ClassForm.ShowDialog(frm) == DialogResult.Yes)
            //                {
            //                    //โหลดข้อมูลใหม่
            //                    LoadDataHD();
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion
        }
    }
}
