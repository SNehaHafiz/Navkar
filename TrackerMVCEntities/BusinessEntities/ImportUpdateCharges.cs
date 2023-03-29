using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ImportUpdateCharges
    {
        public int SrNo { get; set; }
        public int AccountID { get; set; }
        public int ItemNo { get; set; }
        public string AccountName { get; set; }
        public string ContainerNo { get; set; }
        public string IGMNo { get; set; }
        public string BLNo { get; set; }
        public int JONo { get; set; }
        public string LineName { get; set; }
        public string PortName { get; set; }
        public string FirstPartyName { get; set; }
        public string SecondPartyName { get; set; }
        public decimal AmountCollect { get; set; }
        public string Remarks { get; set; }
        public int UpdatedBy { get; set; }
        public int UpdatedOn { get; set; }
        public int AddedBy { get; set; }
        public int IsChecked { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
