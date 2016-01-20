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
    public partial class Chd_ApproveHR_Remark : Form
    {
        public string remark = "";

        protected bool IsSaveOnce = true;

        SqlConnection con = new SqlConnection(SysApp.DatabaseConfig.ServerConStr);
        string _docid;
        public Chd_ApproveHR_Remark(string DocId)
        {
            _docid = DocId;

            InitializeComponent();

            this.rbt_Confirm.Click += new EventHandler(rbt_Confirm_Click);
        }
        

        private void Chd_ApproveHR_Remark_Load(object sender, EventArgs e)
        {
            txtDocId.Text = _docid;
        }
        void rbt_Confirm_Click(object sender, EventArgs e)
        {
            remark = txtHrRemark.Text;
            this.DialogResult = DialogResult.Yes;
        }
    }
}
