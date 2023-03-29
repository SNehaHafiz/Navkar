using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class BIDEntities
    {
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string IGMDate { get; set; }
        public string LotNo { get; set; }
        public string Descriptions { get; set; }
        public string FileNo { get; set; }
        public string ContainerNo { get; set; }
        public string Datewhich { get; set; }
        public string ValuationDate { get; set; }
        public string Quantity { get; set; }
        public string Weight { get; set; }
        public string ValueI { get; set; }
        public string ValueII { get; set; }
        public string ReservePrice { get; set; }

        public string NumberOfAuction { get; set; }
        public string AuctionDate { get; set; }
        public string BidAmount { get; set; }
        public string BidPercentage { get; set; }
        public string ValueAscertainedI { get; set; }
        public string ValueAscertainedII { get; set; }
        public string ReserveAscertained { get; set; }
    }
}
