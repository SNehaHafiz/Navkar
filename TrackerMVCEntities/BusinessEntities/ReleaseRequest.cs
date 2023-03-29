using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ReleaseRequest
    {
        public int SrNo { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime RequestDate { get; set; }
        public int DaysFor { get; set; }
        public string Remarks { get; set; }
        public string RequestedBy { get; set; }
        public string RequestOn { get; set; }
        public int IsApproved { get; set; }
        public int Conditional { get; set; }
        public int IsDecline { get; set; }
        public DateTime ApprovedOn { get; set; }
        public long CRLimitID { get; set; }

    }
}
