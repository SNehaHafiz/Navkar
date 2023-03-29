using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class BCNNocUpdation
    {
        public string NOCNo { get; set; }
        public string NOCDate { get; set; }
        public string ShipperName { get; set; }
        public long ShipperId { get; set; }
        public string OrginStation { get; set; }
        public long OrginStationId { get; set; }
        public long CommodityId { get; set; }
        public long StuffingTypeId { get; set; }
        public long NoOfWagonPlanned { get; set; }
        public string PlanedDate { get; set; }
        public string StuffingDate { get; set; }
        public string ETADate { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ArrivalDate { get; set; }
        public string ErrorMessage { get; set; }

        public BCNNocUpdation()
        {
            NOCNo = "";
            NOCDate = "";
            ShipperName= "";
            ShipperId = 0;
            OrginStation = "";
            OrginStationId = 0;
            CommodityId = 0;
            StuffingTypeId = 0;
            NoOfWagonPlanned = 0;
            PlanedDate = "";
            StuffingDate = "";
            ETADate = "";
            CreatedBy = 0;
            CreatedDate = "";
            ArrivalDate = "";
            ErrorMessage = "";
        }
    }
}
