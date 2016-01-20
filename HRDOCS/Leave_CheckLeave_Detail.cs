using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using System.IO;
using System.Data.SqlClient;
using SysApp;

namespace HRDOCS
{
    public partial class Leave_CheckLeave_Detail : Form
    {
        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);

        string _empid;
        string _getyear;
        string _empname;
        string _leavetype;

        public Leave_CheckLeave_Detail(string EmplId,string EmpName,string LeveType, string GetYear) 
        {
            _empid = EmplId;
            _getyear = GetYear;
            _empname = EmpName;
            _leavetype = LeveType;

            InitializeComponent();
            InitializeDataGrid();

            GeatData();
        }
        private void InitializeDataGrid()
        {
            rvg_LeaveDetail.AutoGenerateColumns = false;
            rvg_LeaveDetail.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rvg_LeaveDetail.Dock = DockStyle.Fill;
            rvg_LeaveDetail.ReadOnly = true;

            GridViewTextBoxColumn PWDATEADJ = new GridViewTextBoxColumn();
            PWDATEADJ.Name = "PWDATEADJ";
            PWDATEADJ.FieldName = "PWDATEADJ";
            PWDATEADJ.HeaderText = "วันที่ลา";
            PWDATEADJ.IsVisible = true;
            PWDATEADJ.ReadOnly = true;
            PWDATEADJ.BestFit();
            rvg_LeaveDetail.Columns.Add(PWDATEADJ);

            GridViewTextBoxColumn LEAVETYPE = new GridViewTextBoxColumn();
            LEAVETYPE.Name = "LEAVETYPE";
            LEAVETYPE.FieldName = "LEAVETYPE";
            LEAVETYPE.HeaderText = "ประเภทการลา";
            LEAVETYPE.IsVisible = true;
            LEAVETYPE.ReadOnly = true;
            LEAVETYPE.BestFit();
            rvg_LeaveDetail.Columns.Add(LEAVETYPE);


            GridViewTextBoxColumn PWUPDTYPE = new GridViewTextBoxColumn();
            PWUPDTYPE.Name = "PWUPDTYPE";
            PWUPDTYPE.FieldName = "PWUPDTYPE";
            PWUPDTYPE.HeaderText = "จำนวนวัน";
            PWUPDTYPE.IsVisible = true;
            PWUPDTYPE.ReadOnly = true;
            PWUPDTYPE.BestFit();
            rvg_LeaveDetail.Columns.Add(PWUPDTYPE);

        }

        void GeatData()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {

                lbl_year.Text = _getyear;
                lbl_empname.Text = _empname;

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                DataTable dt = new DataTable();

                sqlCommand.CommandText = string.Format(
                    @"SELECT PWSTOPWORK1.PWEMPLOYEE,RTRIM(LTRIM(PWEMPLOYEE.PWFNAME)) + ' ' + RTRIM(LTRIM(PWEMPLOYEE.PWLNAME)) AS PWNAME
                        ,CONVERT (VARCHAR ,PWADJTIME.PWDATEADJ,23) AS PWDATEADJ,PWEVENT.PWDESC AS LEAVETYPE 
                        ,(CASE WHEN PWADJTIME.PWUPDTYPE  = '=' then 'ทั้งวัน' ELSE 'ครึ่งวัน' END) AS PWUPDTYPE																   
                    FROM PWSTOPWORK1
                    LEFT JOIN PWEMPLOYEE ON PWSTOPWORK1.PWEMPLOYEE = PWEMPLOYEE.PWEMPLOYEE
                    LEFT JOIN PWADJTIME ON PWSTOPWORK1.PWSTOPWORK = PWADJTIME.PWADJTIME
                    LEFT JOIN PWEVENT ON PWADJTIME.PWEVENT = PWEVENT.PWEVENT
                    WHERE  (PWEMPLOYEE.PWEMPLOYEE = '{0}' OR PWEMPLOYEE.PWCARD = '{0}')
                    AND PWEVENT.PWDESC = '{1}'
                    AND YEAR(PWADJTIME.PWDATEADJ) = YEAR('{2}')"
                    , _empid
                    , _leavetype
                    , _getyear);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                dt.Clear();
                            }
                            dt.Load(sqlDataReader);
                        }
                    }

                if (dt.Rows.Count > 0)
                {
                    rvg_LeaveDetail.DataSource = dt;
                }
                else
                {
                    
                    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rvg_LeaveDetail.DataSource = null;
                    this.Close();
                }
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
    }
}
