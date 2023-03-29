using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class TransportEntities
    {
        public string BankID { get; set; }
        public string BankName { get; set; }

        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string AccountName { get; set; }
        public string BranchName { get; set; }
        public string EmailID { get; set; }
        public bool IsActive { get; set; }
        public long TempID { get; set; }
        public string TRANSNAME { get; set; }
        public string EMAILIDT { get; set; }
        public string ADDRESS { get; set; }
        public string MOBILENO { get; set; }
        public string CONTACTPERSON { get; set; }
        public DateTime RegDate { get; set; }
        public string RegDateString { get; set; }
        public Int32 TransID { get; set; }
        public TransportEntities()
        {
            BankList = new List<TransporterBankEntities>();            
        }
        public List<TransporterBankEntities> BankList { get; set; }
    }
    public class TransporterBankEntities
    {
        public string bankID { get; set; }

        public string bankname { get; set; }
    }
}
