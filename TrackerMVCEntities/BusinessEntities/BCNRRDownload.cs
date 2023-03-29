using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class BCNRRDownload
    {
        public long RRNo { get; set; }
        public long TrainId { get; set; }
        public string TrainNo { get; set; }
        public long FromStationId { get; set; }
        public string FromStation { get; set; }
        public long ToStationId { get; set; }
        public string ToStation { get; set; }
        public long CommodityId { get; set; }
        public double FreightAmount { get; set; }
        public long PartyId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Date { get; set; }
        public string RRWagonNo { get; set; }
        public string RRWagonContNo { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string FileType { get; set; }
        public string ErrorMessage { get; set; }

        public BCNRRDownload()
        {
            RRNo = 0;
            TrainId = 0;
            TrainNo = "";
            FromStationId = 0;
            ToStationId = 0;
            CommodityId = 0;
            FreightAmount = 0;
            PartyId = 0;
            RRWagonNo = "";
            Date = "";
            CreatedBy = 0;
            CreatedDate = "";
            FromStation = "";
            ToStation = "";
            RRWagonContNo = "";
            ErrorMessage = "";
        }
    }
}
