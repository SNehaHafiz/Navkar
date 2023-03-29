using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class AuctionMaster
    {
        public long AuctionId { get; set; }
        public long ClearId { get; set; }
        public string ClearRemarks { get; set; }
        public string AuctionDate { get; set; }
        public string NoticeNo { get; set; }
        public string SendDate { get; set; }
        public string JoNo { get; set; }
        public string ContainerNo { get; set; }
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string Remarks { get; set; }
        public string ReferenceNo { get; set; }
    }
}
