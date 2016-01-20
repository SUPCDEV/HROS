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
    public partial class Chd_CheckEmpHoliday : Form
    {
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);
      
        #region MyRegion SysEmpl

        string sysuser = "";
        string sectionid = "";
        string approveout = "";

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

        protected string sysoutoffice;
        public string SysOutoffice
        {
            get { return sysoutoffice; }
            set { sysoutoffice = value; }
        }

        protected string section;
        public string Section
        {
            get { return section; }
            set { section = value; }
        }

        protected string syshrapproveout;
        public string HrApproveOut
        {
            get { return syshrapproveout; }
            set { syshrapproveout = value; }
        }

        #endregion

        public Chd_CheckEmpHoliday()
        {
            

            InitializeComponent();
            InitializeGridVeiw();
            string _year = DateTime.Now.Year.ToString();

            txt_year.Text = _year;
            txt_empid.Text.ToUpper();
        }

        void InitializeGridVeiw()
        {
            // GridData 
            this.rgv_empholiday.Dock = DockStyle.Fill;
            this.rgv_empholiday.ReadOnly = true;
            this.rgv_empholiday.AutoGenerateColumns = true;
            this.rgv_empholiday.EnableFiltering = false;
            this.rgv_empholiday.AllowAddNewRow = false;
            this.rgv_empholiday.MasterTemplate.AutoGenerateColumns = false;
            this.rgv_empholiday.ShowGroupedColumns = true;
            this.rgv_empholiday.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.rgv_empholiday.EnableHotTracking = true;
            this.rgv_empholiday.AutoSizeRows = true;

            //GridViewTextBoxColumn PWEMPLOYEE = new GridViewTextBoxColumn();
            //PWEMPLOYEE.Name = "PWEMPLOYEE";
            //PWEMPLOYEE.FieldName = "PWEMPLOYEE";
            //PWEMPLOYEE.HeaderText = "รหัสพนักงาน";
            //PWEMPLOYEE.IsVisible = true;
            //PWEMPLOYEE.ReadOnly = true;
            //PWEMPLOYEE.BestFit();
            //rgv_empholiday.Columns.Add(PWEMPLOYEE);

            //GridViewTextBoxColumn PWNAME = new GridViewTextBoxColumn();
            //PWNAME.Name = "PWNAME";
            //PWNAME.FieldName = "PWNAME";
            //PWNAME.HeaderText = "ชื่อ-สกุล";
            //PWNAME.IsVisible = true;
            //PWNAME.ReadOnly = true;
            //PWNAME.BestFit();
            //rgv_empholiday.Columns.Add(PWNAME);

            //GridViewTextBoxColumn SECTIONNAME = new GridViewTextBoxColumn();
            //SECTIONNAME.Name = "SECTIONNAME";
            //SECTIONNAME.FieldName = "SECTIONNAME";
            //SECTIONNAME.HeaderText = "แผนก";
            //SECTIONNAME.IsVisible = true;
            //SECTIONNAME.ReadOnly = true;
            //SECTIONNAME.BestFit();
            //rgv_empholiday.Columns.Add(SECTIONNAME);

            GridViewTextBoxColumn PWDATE = new GridViewTextBoxColumn();
            PWDATE.Name = "PWDATE";
            PWDATE.FieldName = "PWDATE";
            PWDATE.HeaderText = "วันที่";
            PWDATE.IsVisible = true;
            PWDATE.ReadOnly = true;
            PWDATE.BestFit();
            rgv_empholiday.Columns.Add(PWDATE);

            GridViewTextBoxColumn PWDESC = new GridViewTextBoxColumn();
            PWDESC.Name = "PWDESC";
            PWDESC.FieldName = "PWDESC";
            PWDESC.HeaderText = "คำอธิบาย";
            PWDESC.IsVisible = true;
            PWDESC.ReadOnly = true;
            PWDESC.BestFit();
            rgv_empholiday.Columns.Add(PWDESC);

            GridViewTextBoxColumn PWTIME0 = new GridViewTextBoxColumn();
            PWTIME0.Name = "PWTIME0";
            PWTIME0.FieldName = "PWTIME0";
            PWTIME0.HeaderText = "รหัสกะ";
            PWTIME0.IsVisible = true;
            PWTIME0.ReadOnly = true;
            PWTIME0.BestFit();
            rgv_empholiday.Columns.Add(PWTIME0);

            
        }
        void GetDataEmp()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                DataTable dt = new DataTable();

                sqlCommand.CommandText = string.Format(
                    @"SELECT  PWEMPLOYEE.PWEMPLOYEE ,(RTRIM(PWEMPLOYEE.PWFNAME) + '  ' + RTRIM(PWEMPLOYEE.PWLNAME)) AS PWNAME ,PWSECTION.PWDESC
                        FROM PWEMPLOYEE
                         LEFT OUTER JOIN PWSECTION ON PWEMPLOYEE.PWSECTION = PWSECTION.PWSECTION
                        WHERE PWEMPLOYEE.PWEMPLOYEE = '{0}'",txt_empid.Text.ToString().Trim());

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lbl_empname.Text = reader["PWNAME"].ToString();
                        lbl_section.Text = reader["PWDESC"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

        }

        void GetEmpholiday()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                DataTable dt = new DataTable();
                sqlCommand.CommandText = string.Format(
                      @"SELECT CONVERT(VARCHAR,PWTIMETEMP.PWDATE,23) AS PWDATE ,PWHOLIDAY.PWDESC ,PWTIMETEMP.PWTIME0 
                        FROM PWTIMETEMP 
                            LEFT OUTER JOIN PWHOLIDAY ON PWTIMETEMP.PWDATE = PWHOLIDAY.PWHOLIDAY
                        WHERE PWTIMETEMP.PWDATE = PWTIMETEMP.PWRDATE  
                        AND PWTIMETEMP.PWEMPLOYEE = '{0}'
                        AND YEAR(PWTIMETEMP.PWRDATE) = YEAR('{1}') 
                        AND PWTIMETEMP.PWTRANTYPE = 0 
                        AND EXISTS(		SELECT 'X' FROM PWHOLIDAY 
				                        WHERE CONVERT (VARCHAR,PWTIMETEMP.PWDATE,23) = CONVERT (VARCHAR,PWHOLIDAY.PWHOLIDAY,23)
		                          )
                        AND PWTIMETEMP.PWDATE NOT IN(
								                         SELECT DISTINCT( DT.FROMHOLIDAY) FROM SPC_JN_CHANGHOLIDAYDT DT 
									                        LEFT OUTER JOIN SPC_JN_CHANGHOLIDAYHD HD ON DT.DOCID = HD.DOCID
									                        LEFT OUTER JOIN PWTIMETEMP PWTIMETEMP ON HD.EMPLID = PWTIMETEMP.PWEMPLOYEE 
									                        LEFT OUTER JOIN PWHOLIDAY  PWHOLIDAY ON PWTIMETEMP.PWDATE = PWHOLIDAY.PWHOLIDAY
								                         WHERE HD.EMPLID = '{0}'
								                         AND HD.HRAPPROVED = 1
								                         AND DT.FROMHOLIDAY = PWTIMETEMP.PWDATE
								                         AND HD.DOCSTAT = 1
							                        )"
                    , txt_empid.Text.ToString().Trim()
                    , txt_year.Text.ToString().Trim());

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    rgv_empholiday.DataSource = dt;
                }
                else
                {
                    rgv_empholiday.DataSource = dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void txt_empid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                GetDataEmp();
                GetEmpholiday();
            }
        }
    }
}
