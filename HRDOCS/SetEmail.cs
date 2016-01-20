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

namespace HRDOCS
{
    public partial class SetEmail : Form
    {
        public SetEmail()
        {
            InitializeComponent();
            InitializeDropdownlist();
            InitializeButton();
        }

        #region InitializeForm

        private void InitializeForm()
        { 
        
        }

        #endregion

        #region InitializeDropdownlist

        private void InitializeDropdownlist()
        {
            Load_Email();
        }

        #endregion

        #region InitializeButton

        private void InitializeButton()
        {
            Btn_Submit.Click += new EventHandler(Btn_Submit_Click);
        
        }

        void Btn_Submit_Click(object sender, EventArgs e)
        {
            SqlTransaction INSTrans = null;
            SqlConnection sqlConnectionINS = new SqlConnection(DatabaseConfig.ServerConStr);

            INSTrans = null;
            sqlConnectionINS.Open();
            INSTrans = sqlConnectionINS.BeginTransaction();

            #region UpdateModified

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = sqlConnectionINS;
                sqlCommand.CommandText = @"update SPC_CM_EMAIL set EMPLID = @EMPLID 
                                                    where MAIL = @MAIL ";
                sqlCommand.Transaction = INSTrans;

                sqlCommand.Parameters.AddWithValue("@EMPLID", (ClassCurUser.LogInEmplId.ToString().Substring(1)));
                sqlCommand.Parameters.AddWithValue("@MAIL", Ddl_Email.SelectedValue.ToString());
                sqlCommand.ExecuteNonQuery();

            }

            #endregion

            #region Submit

            INSTrans.Commit();
            sqlConnectionINS.Close();

            #endregion

            this.DialogResult = DialogResult.Yes;
        }

        #endregion

        #region Function

        void Load_Email()
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
                    sqlCommand.CommandText = string.Format(@" select mail 
                                                            ,cn + '<' + mail + '>'
                                                            from SPC_CM_EMAIL
                                                            where EMPLID = '{0}' ", (ClassCurUser.LogInEmplId.ToString().Substring(1)));

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

                #endregion
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HRDOCS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                sqlConnection.Close();
                Cursor.Current = Cursors.Default;
            }
        
        }

        #endregion




    }
}
