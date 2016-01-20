using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SysApp
{
    public static class ClassCurUser
    {
        public static string LogInEmplId { get; set; }
        public static string LogInEmplName { get; set; }
        public static string LogInEmplKey { get; set; }

        // <WS>:: 2014-04-19
        public static string LogInEmplDivision { get; set; }
        public static string LogInEmplArea { get; set; }
        // </WS>

        public static string[] LogInEmplSecureconfig { get; set; }

        public static string LogInSection { get; set; }

        public static string SysOutoffice { get; set; }
        public static string SysHrApproveOut { get; set; }
        public static string SysHrApproveIn { get; set; }
        public static string SysMNApproveOut { get; set; }
        public static string SysMNApproveIn { get; set; }
        public static string SysAdministrator { get; set; }

        public static string BEFOREDATECREATE { get; set; }
        public static string AFTERDATECREATE { get; set; }
        public static string LASTDATEAPPROVE_SHIFT { get; set; }
        public static string LASTDATEAPPROVE_LEAVE { get; set; }
        public static string LASTDATEAPPROVE_CHANGE { get; set; }

    }
}
