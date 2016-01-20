using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HROUTOFFICE
{
    public class ClassUser
    {
        public  string LogInEmplId { get; set; }
        public  string LogInEmplName { get; set; }
        public  string LogInEmplKey { get; set; }

        // <WS>:: 2014-04-19
        public  string LogInEmplDivision { get; set; }
        public  string LogInEmplArea { get; set; }
        // </WS>

        public  string[] LogInEmplSecureconfig { get; set; }
        
        public  string LogInSection { get; set; }
        
        public  string SysOutoffice { get; set; }
        public  string SysHrApproveOut { get; set; }
        public  string SysHrApproveIn { get; set; }
        public  string SysMNApproveOut { get; set; }
        public  string SysMNApproveIn { get; set; }
        public  string SysAdministrator { get; set; }

    }
}
