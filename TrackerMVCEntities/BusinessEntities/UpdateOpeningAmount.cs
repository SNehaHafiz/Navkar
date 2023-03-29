using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class UpdateOpeningAmount
    {
        public int SrNo { get; set; }
        public string TransactionType { get; set; }
        public string CustomerName { get; set; }
        public int CustomerID { get; set; }
        public decimal OpeningAmount { get; set; }
        public string ReferenceNo { get; set; }
        public string Remarks { get; set; }
        public DateTime ReferenceDate { get; set; }
        public DateTime AddedOn { get; set; }
        public string Criteria { get; set; }
    }
    
}
