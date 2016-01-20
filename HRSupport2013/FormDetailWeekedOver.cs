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
    public partial class FormDetailWeekedOver : Form
    {
        // <Properties>
        public string LocalEmplId { get; set; }
        public string LocalDateFrom { get; set; }
        public string LocalDateTo { get; set; }
        // </Properties>
        public static System.Globalization.CultureInfo cEN = new System.Globalization.CultureInfo("en-US"); 


        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);

        public FormDetailWeekedOver()
        {
            InitializeComponent();
            this.Icon = HROUTOFFICE.Properties.Resources.sign_out_ico;

            // <layOut>
            this.radGroupBoxMain.Dock = DockStyle.Fill;
            // </layOut>

            #region <RadGridView>
            this.radGridegetdata.Dock = DockStyle.Fill;
            this.radGridegetdata.AutoGenerateColumns = true;
            this.radGridegetdata.EnableFiltering = false;
            this.radGridegetdata.AllowAddNewRow = false;
            this.radGridegetdata.MasterTemplate.AutoGenerateColumns = false;
            this.radGridegetdata.ShowGroupedColumns = true;
            this.radGridegetdata.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.radGridegetdata.EnableHotTracking = true;
            this.radGridegetdata.AutoSizeRows = true;

            #endregion
        }
        public static string Convertyyyy_MM_dd(DateTime strDate)
        {
            return strDate.ToString("yyyy-MM-dd", cEN);
        } 

        public FormDetailWeekedOver(string _emplId, DateTime _dateFrom, DateTime _dateTo)
            : this()
        {
            //LocalDateFrom = (LocalDateFrom != null) ? _dateFrom : new DateTime(1901, 1, 1);
            //LocalDateTo = (LocalDateTo != null) ? _dateTo : new DateTime(1901, 1, 1);
            
            LocalEmplId = (!string.IsNullOrEmpty(_emplId)) ? _emplId : "n/a";  
            LocalDateFrom = Convertyyyy_MM_dd(_dateFrom);
            LocalDateTo = Convertyyyy_MM_dd(_dateTo);
        }

        #region <Event Form>
        private void FormDetailWeekedOver_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LocalEmplId) || LocalDateFrom == null || LocalDateTo == null)
            {
                this.radGridegetdata.DataSource = new string[] { };
                return;
            }

            this.Getdata();
        }
        #endregion       

        #region <Function and Methods>
        private void Getdata()
        {           
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                sqlCommand.CommandText = string.Format(
                    @" SELECT DocId,EmplId,(EmplFname + '  ' + EmplLname) AS EmplFullName
                             ,Dimention
                             ,CASE [OutType] WHEN '1' THEN 'งานบริษัท' WHEN '2' THEN 'ธุระส่วนตัว' ELSE 'ไม่มีข้อมูล' END AS OutType							 
							 ,CASE [CombackType] WHEN '1' THEN 'กลับเข้ามา' WHEN '2' THEN 'ไม่กลับเข้ามา' ELSE 'ไม่มีข้อมูล' END AS CombackType
                             ,Reason,convert (NVARCHAR(10),TrandDateTime, 23) AS TrandDateTime
                        FROM IVZ_HROUTOFFICE
                        WHERE [Status] = 1
                            AND [OutType] = 2 
                            AND [CombackType] = 1
                            AND [HrApprovedOut] = 2
						    AND [HrApprovedIn] = 2
                            AND TrandDateTime BETWEEN '{0}' AND  '{1}' 
                            AND EmplId = '{2}'
                        ORDER BY DocId "

                     , LocalDateFrom
                     , LocalDateTo
                     , LocalEmplId );

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                
                da.Fill(dt);
                radGridegetdata.DataSource = dt;
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
    }
}
