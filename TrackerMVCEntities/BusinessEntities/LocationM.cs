using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class LocationM
    {
        public string Location { get; set; }
        
        public int ID { get; set; }
        public int IDOtherLocation { get; set; }
        public int DistanceGroupID { get; set; }
        public int LocationGroupID { get; set; }
        public string OnWayKM { get; set; }
        public string TwoWayKM { get; set; }
        public string KiloMeter { get; set; }
        public bool ISActive { get; set; }
    }

    public class LocationEntities
    {
        public string Criteria { get; set; }
        public string CriteriaOther { get; set; }
        public int ID { get; set; }
        public int IDOtherLocation { get; set; }
    }

    public class DistanceEntities
    {
        public int DistanceGroupID { get; set; }

        public string DistanceGroup { get; set; }
    }
}
