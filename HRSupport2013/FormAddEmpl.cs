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

namespace HROUTOFFICE
{
    public partial class FormAddEmpl : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);        
        
        //string cardid = "";
        //string birthdate = "";

        //string sectionid = "";
        //string positionid = "";
        //string deptid = "";

        string admin = "";

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
        protected string sysadmin;
        public string SysAdmin
        {
            get { return sysadmin; }
            set { sysadmin = value; }
        }

        public FormAddEmpl()
        {
            InitializeComponent();
            this.btn_save.Click += new EventHandler(btn_save_Click);

            #region GridVied
            this.GridViewShowData.Dock = DockStyle.Fill;
            this.GridViewShowData.ReadOnly = true;
            this.GridViewShowData.AutoGenerateColumns = true;
            this.GridViewShowData.EnableFiltering = false;
            this.GridViewShowData.AllowAddNewRow = false;
            this.GridViewShowData.MasterTemplate.AutoGenerateColumns = false;
            this.GridViewShowData.ShowGroupedColumns = true;
            this.GridViewShowData.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.GridViewShowData.EnableHotTracking = true;
            this.GridViewShowData.AutoSizeRows = true;

            GridViewTextBoxColumn PWCARD = new GridViewTextBoxColumn();
            PWCARD.Name = "PWCARD";
            PWCARD.FieldName = "PWCARD";
            PWCARD.HeaderText = "รหัสบัตร";
            PWCARD.IsVisible = false;
            PWCARD.ReadOnly = true;
            PWCARD.BestFit();
            GridViewShowData.Columns.Add(PWCARD);

            GridViewTextBoxColumn PWEMPLOYEE = new GridViewTextBoxColumn();
            PWEMPLOYEE.Name = "PWEMPLOYEE";
            PWEMPLOYEE.FieldName = "PWEMPLOYEE";
            PWEMPLOYEE.HeaderText = "รหัสพนักงาน";
            PWEMPLOYEE.IsVisible = true;
            PWEMPLOYEE.ReadOnly = true;
            PWEMPLOYEE.BestFit();
            GridViewShowData.Columns.Add(PWEMPLOYEE);

            GridViewTextBoxColumn EmplFullName = new GridViewTextBoxColumn();
            EmplFullName.Name = "EmplFullName";
            EmplFullName.FieldName = "EmplFullName";
            EmplFullName.HeaderText = "ชื่อ-สกุล";
            EmplFullName.IsVisible = true;
            EmplFullName.ReadOnly = true;
            EmplFullName.BestFit();
            GridViewShowData.Columns.Add(EmplFullName);


            GridViewTextBoxColumn PWBIRTHDAY = new GridViewTextBoxColumn();
            PWBIRTHDAY.Name = "PWBIRTHDAY";
            PWBIRTHDAY.FieldName = "PWBIRTHDAY";
            PWBIRTHDAY.HeaderText = "วันเกิด";
            PWBIRTHDAY.IsVisible = false;
            PWBIRTHDAY.ReadOnly = true;
            PWBIRTHDAY.BestFit();
            GridViewShowData.Columns.Add(PWBIRTHDAY);

            GridViewTextBoxColumn SECTIONID = new GridViewTextBoxColumn();
            SECTIONID.Name = "SECTIONID";
            SECTIONID.FieldName = "SECTIONID";
            SECTIONID.HeaderText = "รหัสแผนก";
            SECTIONID.IsVisible = false;
            SECTIONID.ReadOnly = true;
            SECTIONID.BestFit();
            GridViewShowData.Columns.Add(SECTIONID);

            GridViewTextBoxColumn SECTION = new GridViewTextBoxColumn();
            SECTION.Name = "SECTION";
            SECTION.FieldName = "SECTION";
            SECTION.HeaderText = "ชื่อแผนก";
            SECTION.IsVisible = true;
            SECTION.ReadOnly = true;
            SECTION.BestFit();
            GridViewShowData.Columns.Add(SECTION);

            GridViewTextBoxColumn DEPTID = new GridViewTextBoxColumn();
            DEPTID.Name = "DEPTID";
            DEPTID.FieldName = "DEPTID";
            DEPTID.HeaderText = "รหัสตำแหน่ง";
            DEPTID.IsVisible = false;
            DEPTID.ReadOnly = true;
            DEPTID.BestFit();
            GridViewShowData.Columns.Add(DEPTID);

            GridViewTextBoxColumn DEPT = new GridViewTextBoxColumn();
            DEPT.Name = "DEPT";
            DEPT.FieldName = "DEPT";
            DEPT.HeaderText = "ตำแหน่ง";
            DEPT.IsVisible = true;
            DEPT.ReadOnly = true;
            DEPT.BestFit();
            GridViewShowData.Columns.Add(DEPT);

            GridViewTextBoxColumn POSITIONID = new GridViewTextBoxColumn();
            POSITIONID.Name = "POSITIONID";
            POSITIONID.FieldName = "POSITIONID";
            POSITIONID.HeaderText = "รหัส";
            POSITIONID.IsVisible = false;
            POSITIONID.ReadOnly = true;
            POSITIONID.BestFit();
            GridViewShowData.Columns.Add(POSITIONID);

            GridViewTextBoxColumn POSITION = new GridViewTextBoxColumn();
            POSITION.Name = "POSITION";
            POSITION.FieldName = "POSITION";
            POSITION.HeaderText = "ชื่อตำแหน่ง";
            POSITION.IsVisible = false;
            POSITION.ReadOnly = true;
            POSITION.BestFit();
            GridViewShowData.Columns.Add(POSITION);
            #endregion

        }

        private void FormAddEmpl_Load(object sender, EventArgs e)
        {
            this.admin = SysAdmin;
            
            this.ShowDataEmpl();
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;
        }

        //Add 27-03-58
        void btn_save_Click(object sender, EventArgs e)
        {
            this.InsertDataEmpl();
        }

        private void ShowDataEmpl()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.CommandText = string.Format(
                       @"SELECT	RTRIM(PWEMPLOYEE.PWEMPLOYEE) AS PWEMPLOYEE, RTRIM(PWEMPLOYEE.PWCARD) AS PWCARD
		                                ,(RTRIM(PWEMPLOYEE.PWFNAME) +' '+ RTRIM(PWEMPLOYEE.PWLNAME)) AS EmplFullName
			                            ,CONVERT(VARCHAR,[PWEMPLOYEE].[PWBIRTHDAY],112)AS [PWBIRTHDAY] 
			                            ,RTRIM(PWSECTION.PWSECTION) AS SECTIONID ,RTRIM(PWSECTION.PWDESC) AS SECTION
                                        ,RTRIM(PWDEPT.PWDEPT) AS DEPTID ,RTRIM(PWDEPT.PWDESC) AS DEPT
		                                ,RTRIM(PWEMPLOYEE.PWPOSITION) AS POSITIONID ,RTRIM(PWPOSITION.PWDESC) AS POSITION			
	                            FROM[HROS2013].[dbo].[PWEMPLOYEE]
			                            LEFT OUTER JOIN [PWSECTION] ON [PWEMPLOYEE].[PWSECTION] = [PWSECTION].[PWSECTION] COLLATE Thai_CS_AS
			                            LEFT OUTER JOIN [PWPOSITION ]ON [PWPOSITION].[PWPOSITION] = [PWEMPLOYEE].[PWPOSITION] COLLATE DATABASE_DEFAULT
										LEFT OUTER JOIN PWDEPT WITH (NOLOCK) ON PWEMPLOYEE.PWDEPT = PWDEPT.PWDEPT COLLATE Thai_CS_AS
										LEFT OUTER JOIN [HROS_TSYSUSER] ON [PWEMPLOYEE].[PWEMPLOYEE] = [HROS_TSYSUSER].[PWEMPLOYEE] COLLATE Thai_CS_AS
	                            WHERE PWEMPLOYEE.[PWEMPLOYEE] NOT IN
	                            (
		                            SELECT [PWEMPLOYEE] COLLATE DATABASE_DEFAULT FROM [HROS_TSYSUSER]
	                            )
                                AND PWEMPLOYEE.PWSTATWORK IN ('A','V')
                                AND PWEMPLOYEE.[PWGROUP] IN ('01')
                                ORDER BY PWEMPLOYEE.[PWEMPLOYEE] ");

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridViewShowData.DataSource = dt;
                }

                else
                {
                    GridViewShowData.DataSource = dt;
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
       
        private void InsertDataEmpl()
        {
            if (MessageBox.Show("คุณต้องการบันทึกข้อมูล ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            SqlTransaction tr = con.BeginTransaction();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            sqlCommand.Transaction = tr;
            //if (MessageBox.Show("คุณต้องการเพิ่มพนักงานเพื่อใช้งานโปรแกรมออกนอกออนไลน์ ?", "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            
                try
                {
                    foreach (GridViewDataRowInfo row in GridViewShowData.Rows)
                    {
                        if (row.Index > -1)
                        {
                            sqlCommand.CommandText = string.Format(
                                        @" INSERT INTO [HROS_TSYSUSER]
		                                (	
			                                PWCARD,PWEMPLOYEE,KEYPASS,PWSECTION,PWPOSITION,PWDEPT
		                                )
	                                    VALUES 
                                        (
                                            @PWCARD{0} 
                                            ,@PWEMPLOYEE{0} 
                                            ,@KEYPASS{0} 
                                            ,@PWSECTION{0} 
                                            ,@PWPOSITION{0} 
                                            ,@PWDEPT{0} )", row.Index);

                            sqlCommand.Parameters.AddWithValue(string.Format(@"PWCARD{0}", row.Index), row.Cells["PWCARD"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue(string.Format(@"PWEMPLOYEE{0}", row.Index), row.Cells["PWEMPLOYEE"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue(string.Format(@"KEYPASS{0}", row.Index), row.Cells["PWBIRTHDAY"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue(string.Format(@"PWSECTION{0}", row.Index), row.Cells["SECTIONID"].Value.ToString()); //sectionid);
                            sqlCommand.Parameters.AddWithValue(string.Format(@"PWPOSITION{0}", row.Index), row.Cells["POSITIONID"].Value.ToString()); //,positionid);
                            sqlCommand.Parameters.AddWithValue(string.Format(@"PWDEPT{0}", row.Index), row.Cells["DEPTID"].Value.ToString()); //,deptid);
                           
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    this.ShowDataEmpl();
                    if (con.State == ConnectionState.Open) con.Close();
                    
                }
            //}
        }   
    }
}
