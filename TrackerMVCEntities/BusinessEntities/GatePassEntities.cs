using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class GatePassEntities
    {
        public int SrNo { get; set; }
        public int GPNO { get; set; }
        public string GPDate { get; set; }
        public string  CargoDesc { get; set; }
        public string TrailerNo { get; set; }
        public Decimal BTTWT { get; set; }
        public Decimal BTTQty { get; set; }
        public Decimal Qty { get; set; }
        public Decimal Weight { get; set; }
        public int SBEntryID { get; set; }
        public string SBNo { get; set; }
        public int AddedBy { get; set; }
        public string BTTType { get; set; }
        public int BalanceQty { get; set; }
        public int BalanceWT { get; set; }

        
    }
}
