using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class IGMEntities
    {
        public int AuctionId { get; set; }
        public int AuctionId2 { get; set; }
        public string JoNo { get; set; }
        public int FileID { get; set; }
        public int seqID { get; set; }
        public string data { get; set; }
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string IGMDate { get; set; }
        public string Date { get; set; }
        public string NoticeId { get; set; }
        public string NoticeDate { get; set; }
        public string NoticeType { get; set; }
        public string Agent { get; set; }
        public string CargoDescription { get; set; }
        public string ImporterName { get; set; }
        public string NotifyParty { get; set; }
        public string NoOfPkgs { get; set; }
        public string PackageType { get; set; }
        public string Weight { get; set; }
        public string UOM { get; set; }
        public string BLNo { get; set; }
        public string BLDate { get; set; }
    }
}
