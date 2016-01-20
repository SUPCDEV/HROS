using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SysApp;
using Telerik.WinControls.UI;

namespace HRDOCS
{
    public partial class Cancle_SelectShifr : Form
    {
        public string Shift;
        public string ShiftDesc;

        public Cancle_SelectShifr()
        {
            InitializeComponent();

            InitializeForm();
            InitializeDataGrid();
            InitializeButton();
            InitializeDropDownList();  
        }

        #region  InitializeForm

        private void InitializeForm()
        {
            this.Load += new EventHandler(Select_Shift_Load);
        }

        void Select_Shift_Load(object sender, EventArgs e)
        {
            //Load_Shift_Compensate();
        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_Search.Click += new EventHandler(Btn_Search_Click);
        }

        void Btn_Search_Click(object sender, EventArgs e)
        {
            if (Ddl_Time1.Text == "" || Ddl_Time2.Text == "")
            {
                MessageBox.Show("กรุณาเลือกเวลา...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Load_ShiftAll();
            }
        }

        #endregion

        #region InitializeDataGrid

        private void InitializeDataGrid()
        {

            #region SetDataGrid
            //Set_Dg_shift();
            //Set_Dg_ShiftCompensate();
            Set_Dg_All();

            #endregion

            #region Event

            //Dg_SelectShift.CellDoubleClick += new DataGridViewCellEventHandler(Dg_SelectShift_CellDoubleClick);
            //Dg_SelectShiftCompensate.CellDoubleClick += new DataGridViewCellEventHandler(Dg_SelectShiftCompensate_CellDoubleClick);
            Dg_Shift.CellDoubleClick += new DataGridViewCellEventHandler(Dg_SelectShiftAll_CellDoubleClick);
            #endregion

        }

        void Dg_SelectShiftAll_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Shift = Dg_Shift.Rows[e.RowIndex].Cells["PWTIME0"].Value.ToString();
                ShiftDesc = Dg_Shift.Rows[e.RowIndex].Cells["PWDESC"].Value.ToString();
                this.DialogResult = DialogResult.Yes;
            }
        }


        #endregion

        #region InitializeComboBox

        private void InitializeComboBox()
        {
        }

        #endregion

        #region InitializeDropDownList

        private void InitializeDropDownList()
        {
            Ddl_Time1.DropDownListElement.AutoCompleteSuggest.SuggestMode = Telerik.WinControls.UI.SuggestMode.Contains;
            Ddl_Time2.DropDownListElement.AutoCompleteSuggest.SuggestMode = Telerik.WinControls.UI.SuggestMode.Contains;

            Load_DdlTime1();
            Load_DdlTime2();
        }

        #endregion

        #region Function

        void Load_ShiftAll()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_Shift.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@" SELECT *
                                                            FROM   (
                                                            SELECT 
                                                                   DISTINCT RTRIM(PWTIME1.PWTIME0) AS PWTIME0, RTRIM(PWTIME1.PWHOUR_D) AS PWHOUR_D, 
                                                                   RTRIM(PWTIME1.PWTIMEIN1) AS PWTIMEIN1, RTRIM(PWTIME1.PWTIMEOUT2) AS PWTIMEOUT2,
                                                                   ISNULL(RTRIM(PWTIME0.PWDESC), '') AS PWDESC
                                                            FROM   PWTIME1 WITH (NOLOCK)
                                                                   LEFT JOIN PWTIME0 WITH (NOLOCK) ON PWTIME1.PWTIME0 = PWTIME0.PWTIME0
                                                            WHERE  
                                                                   ((PWTIME1.[PWTIME0] LIKE 'T[A-Z][B]%') OR 
                                                                   (PWTIME1.[PWTIME0] LIKE 'PT[A-Z][B]%') OR 
                                                                   (PWTIME1.[PWTIME0] LIKE 'T[B][0-9]%') OR 
                                                                   (PWTIME1.[PWTIME0] LIKE 'PT[B][0-9]%')) AND
                                                                   (PWTIME1.PWHOUR_D > 0)     
                                                                    AND PWTIME1.PWTIMEIN1 = {0} AND PWTIME1.PWTIMEOUT2 = {1}
                                                            ) AS SHFITTABLE
                                                            ORDER BY
                                                                   CAST(PWTIMEIN1 AS FLOAT), CAST(PWHOUR_D AS FLOAT), CAST(PWTIMEOUT2 AS FLOAT)
                                                            ", Ddl_Time1.Text.ToString(), Ddl_Time2.Text.ToString());

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

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        Dg_Shift.Invoke(new EventHandler(delegate
                        {
                            Dg_Shift.Rows.Add();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["PWTIME0"].Value =
                                dataTable.Rows[i]["PWTIME0"].ToString();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["PWHOUR_D"].Value =
                                dataTable.Rows[i]["PWHOUR_D"].ToString();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["PWTIMEIN1"].Value =
                                dataTable.Rows[i]["PWTIMEIN1"].ToString();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["PWTIMEOUT2"].Value =
                                dataTable.Rows[i]["PWTIMEOUT2"].ToString();
                            Dg_Shift.Rows[Dg_Shift.RowCount - 1].Cells["PWDESC"].Value =
                                dataTable.Rows[i]["PWDESC"].ToString();
                        }));
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        void Load_DdlTime1()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //Dg_SelectShift.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@" SELECT *
                                                                FROM   (
                                                                SELECT 
                                                                        DISTINCT 
                                                                        RTRIM(PWTIME1.PWTIMEIN1) AS PWTIMEIN1
                                                                FROM   PWTIME1 WITH (NOLOCK)
                                                                        INNER JOIN PWTIME0 WITH (NOLOCK) ON PWTIME1.PWTIME0 = PWTIME0.PWTIME0
                                                                WHERE  
                                                                        ((PWTIME1.[PWTIME0] LIKE 'T[A-Z][A-Z]%') OR 
                                                                        (PWTIME1.[PWTIME0] LIKE 'PT[A-Z][A-Z]%') OR 
                                                                        (PWTIME1.[PWTIME0] LIKE 'T[AB][0-9]%') OR 
                                                                        (PWTIME1.[PWTIME0] LIKE 'PT[AB][0-9]%')) AND
                                                                        (PWTIME1.PWHOUR_D > 0)     
                                                                ) AS SHFITTABLE
                                                                ORDER BY
                                                                        CAST(PWTIMEIN1 AS FLOAT) ");

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
                    Ddl_Time1.DataSource = dataTable;
                    Ddl_Time1.DisplayMember = "PWTIMEIN1";
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        void Load_DdlTime2()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //Dg_SelectShift.Rows.Clear();

                #region SQL

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = string.Format(@" SELECT *
                                                                FROM   (
                                                                SELECT 
                                                                        DISTINCT 
                                                                        RTRIM(PWTIME1.PWTIMEOUT2) AS PWTIMEOUT2
                                                                       
                                                                FROM   PWTIME1 WITH (NOLOCK)
                                                                        INNER JOIN PWTIME0 WITH (NOLOCK) ON PWTIME1.PWTIME0 = PWTIME0.PWTIME0
                                                                WHERE  
                                                                        ((PWTIME1.[PWTIME0] LIKE 'T[A-Z][A-Z]%') OR 
                                                                        (PWTIME1.[PWTIME0] LIKE 'PT[A-Z][A-Z]%') OR 
                                                                        (PWTIME1.[PWTIME0] LIKE 'T[AB][0-9]%') OR 
                                                                        (PWTIME1.[PWTIME0] LIKE 'PT[AB][0-9]%')) AND
                                                                        (PWTIME1.PWHOUR_D > 0)     
                                                                        
                                                                ) AS SHFITTABLE
                                                                ORDER BY
                                                                        CAST(PWTIMEOUT2 AS FLOAT)  ");

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
                    Ddl_Time2.DataSource = dataTable;
                    Ddl_Time2.DisplayMember = "PWTIMEOUT2";
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูล...", "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        void Set_Dg_All()
        {

            Dg_Shift.AutoGenerateColumns = false;
            Dg_Shift.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_Shift.Dock = DockStyle.Fill;


            DataGridViewTextBoxColumn PWTIME0 = new DataGridViewTextBoxColumn();
            PWTIME0.Name = "PWTIME0";
            PWTIME0.DataPropertyName = "PWTIME0";
            PWTIME0.HeaderText = "รหัสกะ";
            PWTIME0.ToolTipText = "รหัสกะ";
            PWTIME0.Width = 100;
            PWTIME0.ReadOnly = true;
            PWTIME0.Visible = true;
            Dg_Shift.Columns.Add(PWTIME0);

            DataGridViewTextBoxColumn PWHOUR_D = new DataGridViewTextBoxColumn();
            PWHOUR_D.Name = "PWHOUR_D";
            PWHOUR_D.DataPropertyName = "PWHOUR_D";
            PWHOUR_D.HeaderText = "ชั่วโมงทำงาน";
            PWHOUR_D.ToolTipText = "ชั่วโมงทำงาน";
            PWHOUR_D.Width = 100;
            PWHOUR_D.ReadOnly = true;
            PWHOUR_D.Visible = true;
            Dg_Shift.Columns.Add(PWHOUR_D);

            DataGridViewTextBoxColumn PWTIMEIN1 = new DataGridViewTextBoxColumn();
            PWTIMEIN1.Name = "PWTIMEIN1";
            PWTIMEIN1.DataPropertyName = "PWTIMEIN1";
            PWTIMEIN1.HeaderText = "เวลาเข้างาน";
            PWTIMEIN1.ToolTipText = "เวลาเข้างาน";
            PWTIMEIN1.Width = 100;
            PWTIMEIN1.ReadOnly = true;
            PWTIMEIN1.Visible = true;
            Dg_Shift.Columns.Add(PWTIMEIN1);

            DataGridViewTextBoxColumn PWTIMEOUT2 = new DataGridViewTextBoxColumn();
            PWTIMEOUT2.Name = "PWTIMEOUT2";
            PWTIMEOUT2.DataPropertyName = "PWTIMEOUT2";
            PWTIMEOUT2.HeaderText = "เวลาเลิกงาน";
            PWTIMEOUT2.ToolTipText = "เวลาเลิกงาน";
            PWTIMEOUT2.Width = 100;
            PWTIMEOUT2.ReadOnly = true;
            PWTIMEOUT2.Visible = true;
            Dg_Shift.Columns.Add(PWTIMEOUT2);

            DataGridViewTextBoxColumn PWDESC = new DataGridViewTextBoxColumn();
            PWDESC.Name = "PWDESC";
            PWDESC.DataPropertyName = "PWDESC";
            PWDESC.HeaderText = "กะทำงาน";
            PWDESC.ToolTipText = "กะทำงาน";
            PWDESC.Width = 175;
            PWDESC.ReadOnly = true;
            Dg_Shift.Columns.Add(PWDESC);
        }

        #endregion
    }
}
