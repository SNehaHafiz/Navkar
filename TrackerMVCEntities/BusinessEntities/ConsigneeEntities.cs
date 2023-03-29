using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public  class ConsigneeEntities
    {
        public string ImporterName { get; set; }
        public string CHAName { get; set; }
        public string BillingPartyName { get; set; }
        public string ContainerNo { get; set; }
        public string ModeofTransport { get; set; }
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string ICDGateIn { get; set; }

        public string ClearanceDate { get; set; }
        public string MovementDatetoFactory { get; set; }
        public string LRNo { get; set; }
        public string VehicleNo { get; set; }
        public string ReportingDateatFactory { get; set; }
        public string VehicleReleasedfromFactory { get; set; }
        public string ValidTill { get; set; }
        public string BOENo { get; set; }
        public string TotalHours { get; set; } 
    }
}
