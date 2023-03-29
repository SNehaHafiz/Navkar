using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class LocationEditEntiites
    {
        public string Location { get; set; }

        public int ID { get; set; }

        public string DistanceGroupID { get; set; }
        public string LocationGroupID { get; set; }
        public string OnWayKM { get; set; }
        public string TwoWayKM { get; set; }
        public string KiloMeter { get; set; }
        public string LocationOtherGroupId { get; set; }
    }
}
