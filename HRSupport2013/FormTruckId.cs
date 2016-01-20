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
    public partial class FormTruckId : Form
    {
        SqlConnection con = new SqlConnection(DatabaseConfig.ServerConStr);

        //private string truckId = "";
        public string TruckId { get; set; }
        public string VEHICLEID { get; set; }
        public string VEHICLENAME { get; set; }


        public FormTruckId()
        {
            InitializeComponent();
            //this.radGridViewTruckId.Dock = DockStyle.Fill;
            this.radGridViewTruckId.AutoGenerateColumns = true;
            this.radGridViewTruckId.EnableFiltering = false;
            this.radGridViewTruckId.AllowAddNewRow = false;
            this.radGridViewTruckId.ReadOnly = true;
            this.radGridViewTruckId.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.HeaderCells);
            this.radGridViewTruckId.MasterTemplate.ShowHeaderCellButtons = true;
            this.radGridViewTruckId.MasterTemplate.ShowFilteringRow = false;
            this.radGridViewTruckId.MasterTemplate.AutoGenerateColumns = false;
            this.radGridViewTruckId.ShowGroupedColumns = true;
            this.radGridViewTruckId.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.radGridViewTruckId.EnableHotTracking = true;
            this.radGridViewTruckId.AutoSizeRows = true;

            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        public FormTruckId(string _truckKey)
            : this()
        {
            TruckId = _truckKey;
            this.txtTruckId.Focus();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            VEHICLEID = radGridViewTruckId.CurrentRow.Cells["VEHICLEID"].Value.ToString();
            VEHICLENAME = radGridViewTruckId.CurrentRow.Cells["NAME"].Value.ToString();
            DialogResult = DialogResult.OK;
        }        

        private void FormTruckId_Load(object sender, EventArgs e)
        {
           this.txtTruckId.Text = TruckId;
           this.txtTruckId.Focus();
           this.LookUp();
        }
        // public string Msg { get; set; } 

        private void txtTruckId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.LookUp();
            }
        }

        private void LookUp()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;

                sqlCommand.CommandText = string.Format(
                                @"SELECT DISTINCT [VEHICLEID],[NAME] FROM [dbo].[SPC_VEHICLETABLE]
                                     WHERE [VEHICLEID] LIKE '%{0}%' ORDER BY VEHICLEID "
                                    , txtTruckId.Text.Trim());
                //, txtTruckId.Text.Trim().Replace("*", "%").Replace("'", ""));
                //convert(varchar,getdate(),23)
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    radGridViewTruckId.DataSource = dt;
                    this.radGridViewTruckId.Enabled = true;
                    this.radGridViewTruckId.Enabled = true;
                }
                else
                {
                    radGridViewTruckId.DataSource = dt;
                    MessageBox.Show("ไม่มีข้อมูล");
                    this.txtTruckId.Clear();
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
        //private void radGridViewTruckId_CellDoubleClick(object sender, GridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        if (e.Column.Name == "VEHICLEID")
        //        {
        //            GridViewRowInfo row = (GridViewRowInfo)radGridViewTruckId.Rows[e.RowIndex];
        //            string _Ptruckid = row.Cells["VEHICLEID"].Value.ToString();

        //            FormCreateNew frm3 = new FormCreateNew();
        //            frm3.LookUpTruckId(_Ptruckid);
        //            this.Close();
        //        }
        //    }
        //}        
    }
}
