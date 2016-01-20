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
    public partial class Cancle_ReportStatusDoc_DetailDL : Form
    {
        protected bool IsSaveOnce = true;
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string _docid;
        string _dldoc;

        public Cancle_ReportStatusDoc_DetailDL(string DocId, string Doctype)
        {
            _docid = DocId;
            InitializeComponent();
            InitializeDataGridView();
            GetData();
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

            GridViewTextBoxColumn DLDOCNO = new GridViewTextBoxColumn();
            DLDOCNO.Name = "DLDOCNO";
            DLDOCNO.FieldName = "DLDOCNO";
            DLDOCNO.HeaderText = "เลขที่เอกสาร";
            DLDOCNO.IsVisible = true;
            DLDOCNO.ReadOnly = true;
            DLDOCNO.BestFit();
            rgv_EmpData.Columns.Add(DLDOCNO);

            GridViewTextBoxColumn LEAVEDATE = new GridViewTextBoxColumn();
            LEAVEDATE.Name = "LEAVEDATE";
            LEAVEDATE.FieldName = "LEAVEDATE";
            LEAVEDATE.HeaderText = "วันที่ลาหยุด";
            LEAVEDATE.IsVisible = true;
            LEAVEDATE.ReadOnly = true;
            LEAVEDATE.BestFit();
            rgv_EmpData.Columns.Add(LEAVEDATE);


            GridViewTextBoxColumn LEAVETYPEDETAIL = new GridViewTextBoxColumn();
            LEAVETYPEDETAIL.Name = "LEAVETYPEDETAIL";
            LEAVETYPEDETAIL.FieldName = "LEAVETYPEDETAIL";
            LEAVETYPEDETAIL.HeaderText = "ประเภทการลา";
            LEAVETYPEDETAIL.IsVisible = true;
            LEAVETYPEDETAIL.ReadOnly = true;
            LEAVETYPEDETAIL.BestFit();
            rgv_EmpData.Columns.Add(LEAVETYPEDETAIL);

            GridViewTextBoxColumn ATTACH = new GridViewTextBoxColumn();
            ATTACH.Name = "ATTACH";
            ATTACH.FieldName = "ATTACH";
            ATTACH.HeaderText = "ใบรับรองแพทย์";
            ATTACH.IsVisible = true;
            ATTACH.ReadOnly = true;
            ATTACH.BestFit();
            rgv_EmpData.Columns.Add(ATTACH);

            GridViewTextBoxColumn HALFDAY = new GridViewTextBoxColumn();
            HALFDAY.Name = "HALFDAY";
            HALFDAY.FieldName = "HALFDAY";
            HALFDAY.HeaderText = "จำนวนวัน";
            HALFDAY.IsVisible = true;
            HALFDAY.ReadOnly = true;
            HALFDAY.BestFit();
            rgv_EmpData.Columns.Add(HALFDAY);

            GridViewTextBoxColumn HALFDAYTIME1 = new GridViewTextBoxColumn();
            HALFDAYTIME1.Name = "HALFDAYTIME1";
            HALFDAYTIME1.FieldName = "HALFDAYTIME1";
            HALFDAYTIME1.HeaderText = "ตั้งแต่เวลา";
            HALFDAYTIME1.IsVisible = true;
            HALFDAYTIME1.ReadOnly = true;
            HALFDAYTIME1.BestFit();
            rgv_EmpData.Columns.Add(HALFDAYTIME1);

            GridViewTextBoxColumn HALFDAYTIME2 = new GridViewTextBoxColumn();
            HALFDAYTIME2.Name = "HALFDAYTIME2";
            HALFDAYTIME2.FieldName = "HALFDAYTIME2";
            HALFDAYTIME2.HeaderText = "ถึงเวลา";
            HALFDAYTIME2.IsVisible = true;
            HALFDAYTIME2.ReadOnly = true;
            HALFDAYTIME2.BestFit();
            rgv_EmpData.Columns.Add(HALFDAYTIME2);

            GridViewTextBoxColumn LEAVEREMARK = new GridViewTextBoxColumn();
            LEAVEREMARK.Name = "LEAVEREMARK";
            LEAVEREMARK.FieldName = "LEAVEREMARK";
            LEAVEREMARK.HeaderText = "เหตุผล";
            LEAVEREMARK.IsVisible = true;
            LEAVEREMARK.ReadOnly = true;
            LEAVEREMARK.BestFit();
            rgv_EmpData.Columns.Add(LEAVEREMARK);

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

                         @"SELECT HD.DOCID
                                ,HD.EMPLID ,HD.EMPLNAME ,HD.SECTIONNAME ,HD.DEPTNAME ,DT.REASON
                                ,DLHD.DLDOCNO,DOCTYP.TYPDESC ,DT.DOCREFER
                                FROM [dbo].[SPC_JN_CANCLEDOCHD] HD
                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCDT] DT ON HD.DOCID = DT.DOCID
                                LEFT OUTER JOIN [dbo].[SPC_JN_CANCLEDOCTYP] DOCTYP ON DT.DOCTYP = DOCTYP.DOCTYP
                                LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEHD] DLHD ON DT.DOCREFER = DLHD.DLDOCNO
                                LEFT OUTER JOIN [dbo].[SPC_CM_LEAVEDT] DLDT ON DLHD.DLDOCNO = DLDT.DLDOCNO
                            WHERE HD.DOCID = '{0}'                            
                            AND HD.DOCSTAT = '1' ", _docid.ToString());
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

                        _dldoc = reader["DLDOCNO"].ToString();
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


            try
            {
                sqlCommand.CommandText = string.Format(

                           @"SELECT DOCTYP.TYPDESC ,DLHD.DLDOCNO
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
                                AND DT.DATEREFER  = DLDT.LEAVEDATE ", _docid.ToString());

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
    }
}
