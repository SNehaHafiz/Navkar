using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class BankEntities
    {
        public string BankID { get; set; }
        public string BankName { get; set; }

        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string AccountName { get; set; }
        public string BranchName { get; set; }
        public string EmailID { get; set; }
        public long TempID { get; set; }
    }
}
