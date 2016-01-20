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
    public partial class Select_Shift : Form
    {
        public string Shift;
        public string ShiftDesc;

        public Select_Shift()
        {
            InitializeComponent();
            InitializeForm();

            // <WS>
            #region <LayOut>
            this.splitContainer1.Dock = DockStyle.Fill;
            #endregion
            // </WS>

            InitializeDataGrid();
            InitializeButton();
            //InitializeComboBox();
            InitializeDropDownList();
        }

        #region  InitializeForm

        private void InitializeForm()
        {
            // <WS: 25/08/2015>
            this.KeyPreview = true;
            if (this.Modal)
            {
                this.StartPosition = FormStartPosition.CenterParent;
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            // </WS: 25/08/2015>

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

            Dg_SelectShift.CellDoubleClick += new DataGridViewCellEventHandler(Dg_SelectShift_CellDoubleClick);
            Dg_SelectShiftCompensate.CellDoubleClick += new DataGridViewCellEventHandler(Dg_SelectShiftCompensate_CellDoubleClick);
            Dg_SelectShiftAll.CellDoubleClick += new DataGridViewCellEventHandler(Dg_SelectShiftAll_CellDoubleClick);
            #endregion

        }

        void Dg_SelectShiftAll_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Shift = Dg_SelectShiftAll.Rows[e.RowIndex].Cells["PWTIME0"].Value.ToString();
                ShiftDesc = Dg_SelectShiftAll.Rows[e.RowIndex].Cells["PWDESC"].Value.ToString();
                this.DialogResult = DialogResult.Yes;
            }
        }

        void Dg_SelectShiftCompensate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Shift = Dg_SelectShiftCompensate.Rows[e.RowIndex].Cells["PWTIME0"].Value.ToString();
                this.DialogResult = DialogResult.Yes;
            }
        }

        void Dg_SelectShift_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Shift = Dg_SelectShift.Rows[e.RowIndex].Cells["PWTIME0"].Value.ToString();
                ShiftDesc = Dg_SelectShift.Rows[e.RowIndex].Cells["PWDESC"].Value.ToString();
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

        void Load_Shift()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_SelectShift.Rows.Clear();

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
                                                                   ISNULL(LTRIM(RTRIM(PWTIME0.PWDESC)), '') AS PWDESC
                                                            FROM   PWTIME1 WITH (NOLOCK)
                                                                   INNER JOIN PWTIME0 WITH (NOLOCK) ON PWTIME1.PWTIME0 = PWTIME0.PWTIME0
                                                            WHERE  
                                                                   ((PWTIME1.[PWTIME0] LIKE 'T[A-Z][A-Z]%') OR 
                                                                   (PWTIME1.[PWTIME0] LIKE 'PT[A-Z][A-Z]%') OR 
                                                                   (PWTIME1.[PWTIME0] LIKE 'T[AB][0-9]%') OR 
                                                                   (PWTIME1.[PWTIME0] LIKE 'PT[AB][0-9]%')) AND
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
                        Dg_SelectShift.Invoke(new EventHandler(delegate
                        {
                            Dg_SelectShift.Rows.Add();
                            Dg_SelectShift.Rows[Dg_SelectShift.RowCount - 1].Cells["PWTIME0"].Value =
                                dataTable.Rows[i]["PWTIME0"].ToString();
                            Dg_SelectShift.Rows[Dg_SelectShift.RowCount - 1].Cells["PWHOUR_D"].Value =
                                dataTable.Rows[i]["PWHOUR_D"].ToString();
                            Dg_SelectShift.Rows[Dg_SelectShift.RowCount - 1].Cells["PWTIMEIN1"].Value =
                                dataTable.Rows[i]["PWTIMEIN1"].ToString();
                            Dg_SelectShift.Rows[Dg_SelectShift.RowCount - 1].Cells["PWTIMEOUT2"].Value =
                                dataTable.Rows[i]["PWTIMEOUT2"].ToString();
                            Dg_SelectShift.Rows[Dg_SelectShift.RowCount - 1].Cells["PWDESC"].Value =
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

        void Load_Shift_Compensate()
        {

            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_SelectShift.Rows.Clear();

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
                                                                   ISNULL(LTRIM(RTRIM(PWTIME0.PWDESC)), '') AS PWDESC
                                                            FROM   PWTIME1 WITH (NOLOCK)
                                                                   INNER JOIN PWTIME0 WITH (NOLOCK) ON PWTIME1.PWTIME0 = PWTIME0.PWTIME0
                                                            WHERE  
                                                                   ((PWTIME1.[PWTIME0] LIKE 'T[A-Z][A-Z]%') OR 
                                                                   (PWTIME1.[PWTIME0] LIKE 'PT[A-Z][A-Z]%') OR 
                                                                   (PWTIME1.[PWTIME0] LIKE 'T[AB][0-9]%') OR 
                                                                   (PWTIME1.[PWTIME0] LIKE 'PT[AB][0-9]%')) AND
                                                                   (PWTIME1.PWHOUR_D > 0)     
                                                                    AND PWTIME0.PWDESC LIKE '%ชดเชย%'
                                                            ) AS SHFITTABLE
                                                            ORDER BY
                                                                   CAST(PWTIMEIN1 AS FLOAT), CAST(PWHOUR_D AS FLOAT), CAST(PWTIMEOUT2 AS FLOAT) 
                                                            ");

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
                        Dg_SelectShiftCompensate.Invoke(new EventHandler(delegate
                        {
                            Dg_SelectShiftCompensate.Rows.Add();
                            Dg_SelectShiftCompensate.Rows[Dg_SelectShiftCompensate.RowCount - 1].Cells["PWTIME0"].Value =
                                dataTable.Rows[i]["PWTIME0"].ToString();
                            //Dg_SelectShiftCompensate.Rows[Dg_SelectShiftCompensate.RowCount - 1].Cells["PWHOUR_D"].Value =
                            //    dataTable.Rows[i]["PWHOUR_D"].ToString();
                            //Dg_SelectShiftCompensate.Rows[Dg_SelectShiftCompensate.RowCount - 1].Cells["PWTIMEIN1"].Value =
                            //    dataTable.Rows[i]["PWTIMEIN1"].ToString();
                            //Dg_SelectShiftCompensate.Rows[Dg_SelectShiftCompensate.RowCount - 1].Cells["PWTIMEOUT2"].Value =
                            //    dataTable.Rows[i]["PWTIMEOUT2"].ToString();
                            Dg_SelectShiftCompensate.Rows[Dg_SelectShiftCompensate.RowCount - 1].Cells["PWDESC"].Value =
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

        void Load_ShiftAll()
        {
            SqlConnection sqlConnection = new SqlConnection(DatabaseConfig.ServerConStr);
            DataTable dataTable = new DataTable();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Dg_SelectShiftAll.Rows.Clear();

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
                                                                   ((PWTIME1.[PWTIME0] LIKE 'T[A-Z][A]%') OR
                                                                   (PWTIME1.[PWTIME0] LIKE 'T[A-Z][B]%') OR 
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
                        Dg_SelectShiftAll.Invoke(new EventHandler(delegate
                        {
                            Dg_SelectShiftAll.Rows.Add();
                            Dg_SelectShiftAll.Rows[Dg_SelectShiftAll.RowCount - 1].Cells["PWTIME0"].Value =
                                dataTable.Rows[i]["PWTIME0"].ToString();
                            Dg_SelectShiftAll.Rows[Dg_SelectShiftAll.RowCount - 1].Cells["PWHOUR_D"].Value =
                                dataTable.Rows[i]["PWHOUR_D"].ToString();
                            Dg_SelectShiftAll.Rows[Dg_SelectShiftAll.RowCount - 1].Cells["PWTIMEIN1"].Value =
                                dataTable.Rows[i]["PWTIMEIN1"].ToString();
                            Dg_SelectShiftAll.Rows[Dg_SelectShiftAll.RowCount - 1].Cells["PWTIMEOUT2"].Value =
                                dataTable.Rows[i]["PWTIMEOUT2"].ToString();
                            Dg_SelectShiftAll.Rows[Dg_SelectShiftAll.RowCount - 1].Cells["PWDESC"].Value =
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
                Dg_SelectShift.Rows.Clear();

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
                Dg_SelectShift.Rows.Clear();

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

        void Set_Dg_shift()
        {
            Dg_SelectShift.AutoGenerateColumns = false;
            Dg_SelectShift.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_SelectShift.Dock = DockStyle.Fill;


            DataGridViewTextBoxColumn PWTIME0 = new DataGridViewTextBoxColumn();
            PWTIME0.Name = "PWTIME0";
            PWTIME0.DataPropertyName = "PWTIME0";
            PWTIME0.HeaderText = "รหัสกะ";
            PWTIME0.ToolTipText = "รหัสกะ";
            PWTIME0.Width = 100;
            PWTIME0.ReadOnly = true;
            PWTIME0.Visible = true;
            Dg_SelectShift.Columns.Add(PWTIME0);

            DataGridViewTextBoxColumn PWHOUR_D = new DataGridViewTextBoxColumn();
            PWHOUR_D.Name = "PWHOUR_D";
            PWHOUR_D.DataPropertyName = "PWHOUR_D";
            PWHOUR_D.HeaderText = "ชั่วโมงทำงาน";
            PWHOUR_D.ToolTipText = "ชั่วโมงทำงาน";
            PWHOUR_D.Width = 100;
            PWHOUR_D.ReadOnly = true;
            PWHOUR_D.Visible = false;
            Dg_SelectShift.Columns.Add(PWHOUR_D);

            DataGridViewTextBoxColumn PWTIMEIN1 = new DataGridViewTextBoxColumn();
            PWTIMEIN1.Name = "PWTIMEIN1";
            PWTIMEIN1.DataPropertyName = "PWTIMEIN1";
            PWTIMEIN1.HeaderText = "เวลาเข้างาน";
            PWTIMEIN1.ToolTipText = "เวลาเข้างาน";
            PWTIMEIN1.Width = 100;
            PWTIMEIN1.ReadOnly = true;
            PWTIMEIN1.Visible = false;
            Dg_SelectShift.Columns.Add(PWTIMEIN1);

            DataGridViewTextBoxColumn PWTIMEOUT2 = new DataGridViewTextBoxColumn();
            PWTIMEOUT2.Name = "PWTIMEOUT2";
            PWTIMEOUT2.DataPropertyName = "PWTIMEOUT2";
            PWTIMEOUT2.HeaderText = "เวลาเลิกงาน";
            PWTIMEOUT2.ToolTipText = "เวลาเลิกงาน";
            PWTIMEOUT2.Width = 100;
            PWTIMEOUT2.ReadOnly = true;
            PWTIMEOUT2.Visible = false;
            Dg_SelectShift.Columns.Add(PWTIMEOUT2);

            DataGridViewTextBoxColumn PWDESC = new DataGridViewTextBoxColumn();
            PWDESC.Name = "PWDESC";
            PWDESC.DataPropertyName = "PWDESC";
            PWDESC.HeaderText = "กะทำงาน";
            PWDESC.ToolTipText = "กะทำงาน";
            PWDESC.Width = 175;
            PWDESC.ReadOnly = true;
            Dg_SelectShift.Columns.Add(PWDESC);

        }

        void Set_Dg_ShiftCompensate()
        {
            Dg_SelectShiftCompensate.AutoGenerateColumns = false;
            Dg_SelectShiftCompensate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dg_SelectShiftCompensate.Dock = DockStyle.Fill;

            DataGridViewTextBoxColumn PWTIME0_2 = new DataGridViewTextBoxColumn();
            PWTIME0_2.Name = "PWTIME0";
            PWTIME0_2.DataPropertyName = "PWTIME0";
            PWTIME0_2.HeaderText = "รหัสกะ";
            PWTIME0_2.ToolTipText = "รหัสกะ";
            PWTIME0_2.Width = 100;
            PWTIME0_2.ReadOnly = true;
            PWTIME0_2.Visible = true;
            Dg_SelectShiftCompensate.Columns.Add(PWTIME0_2);

            DataGridViewTextBoxColumn PWDESC2 = new DataGridViewTextBoxColumn();
            PWDESC2.Name = "PWDESC";
            PWDESC2.DataPropertyName = "PWDESC";
            PWDESC2.HeaderText = "กะชดเชย";
            PWDESC2.ToolTipText = "กะชดเชย";
            PWDESC2.Width = 175;
            PWDESC2.ReadOnly = true;
            Dg_SelectShiftCompensate.Columns.Add(PWDESC2);

        }

        void Set_Dg_All()
        {
            Dg_SelectShiftAll.AutoGenerateColumns = false;            
            Dg_SelectShiftAll.Dock = DockStyle.Fill;
            Dg_SelectShiftAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            Dg_SelectShiftAll.AllowUserToResizeColumns = true;

            DataGridViewTextBoxColumn PWTIME0 = new DataGridViewTextBoxColumn();
            PWTIME0.Name = "PWTIME0";
            PWTIME0.DataPropertyName = "PWTIME0";
            PWTIME0.HeaderText = "รหัสกะ";
            PWTIME0.ToolTipText = "รหัสกะ";
            PWTIME0.Width = 60;
            PWTIME0.ReadOnly = true;
            PWTIME0.Visible = true;
            PWTIME0.Resizable = DataGridViewTriState.True;
            Dg_SelectShiftAll.Columns.Add(PWTIME0);

            DataGridViewTextBoxColumn PWHOUR_D = new DataGridViewTextBoxColumn();
            PWHOUR_D.Name = "PWHOUR_D";
            PWHOUR_D.DataPropertyName = "PWHOUR_D";
            PWHOUR_D.HeaderText = "ชั่วโมงทำงาน";
            PWHOUR_D.ToolTipText = "ชั่วโมงทำงาน";
            PWHOUR_D.Width = 65;
            PWHOUR_D.ReadOnly = true;
            PWHOUR_D.Visible = true;
            PWHOUR_D.Resizable = DataGridViewTriState.True;
            Dg_SelectShiftAll.Columns.Add(PWHOUR_D);

            DataGridViewTextBoxColumn PWTIMEIN1 = new DataGridViewTextBoxColumn();
            PWTIMEIN1.Name = "PWTIMEIN1";
            PWTIMEIN1.DataPropertyName = "PWTIMEIN1";
            PWTIMEIN1.HeaderText = "เวลาเข้างาน";
            PWTIMEIN1.ToolTipText = "เวลาเข้างาน";
            PWTIMEIN1.Width = 65;
            PWTIMEIN1.ReadOnly = true;
            PWTIMEIN1.Visible = true;
            PWTIMEIN1.Resizable = DataGridViewTriState.True;
            Dg_SelectShiftAll.Columns.Add(PWTIMEIN1);

            DataGridViewTextBoxColumn PWTIMEOUT2 = new DataGridViewTextBoxColumn();
            PWTIMEOUT2.Name = "PWTIMEOUT2";
            PWTIMEOUT2.DataPropertyName = "PWTIMEOUT2";
            PWTIMEOUT2.HeaderText = "เวลาเลิกงาน";
            PWTIMEOUT2.ToolTipText = "เวลาเลิกงาน";
            PWTIMEOUT2.Width = 65;
            PWTIMEOUT2.ReadOnly = true;
            PWTIMEOUT2.Visible = true;
            PWTIMEOUT2.Resizable = DataGridViewTriState.True;
            Dg_SelectShiftAll.Columns.Add(PWTIMEOUT2);

            DataGridViewTextBoxColumn PWDESC = new DataGridViewTextBoxColumn();
            PWDESC.Name = "PWDESC";
            PWDESC.DataPropertyName = "PWDESC";
            PWDESC.HeaderText = "คำอธิบาย";
            PWDESC.ToolTipText = "คำอธิบาย";
            PWDESC.Width = 250;
            PWDESC.ReadOnly = true;
            PWDESC.Resizable = DataGridViewTriState.True;
            Dg_SelectShiftAll.Columns.Add(PWDESC);            
        }

        #endregion
        
        // <WS: 25/08/2015>
        // Keyboard Hotkeys: F3
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyData == Keys.F3)
                this.Btn_Search.PerformClick();

        }
        // </WS: 25/08/2015>
    }
}
