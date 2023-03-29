using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class LashingAndChockingInfo
    {
        public int AutoID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int ItemID { get; set; }
        public decimal Rate { get; set; }
        public decimal Qty { get; set; }
        public string GRNNo { get; set; }
        public string IssueNo { get; set; }
        public bool IsCancelled { get; set; }
        public int SentBy { get; set; }
        public string SentByName { get; set; }
        public string SentOn { get; set; }

        //for material group info
        public string MaterialGroupName { get; set; }
        public int MaterialGroupID { get; set; }
        public int MaterialQty { get; set; }
        public int MaterialMappingID { get; set; }
    }


    public class WOtransfer
    {

        public float Amount { get; set; }
        public int SrNo { get; set; }
        public int AutoID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ReceivedQty { get; set; }
        public string IssQty { get; set; }
        public string Balance { get; set; }
        public string TransferQty { get; set; }

    }

}
