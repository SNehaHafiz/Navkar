using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
 public   class VehicleActivityInOut
    {
        public int SrNo { get; set; }
        public string ActivityDate { get; set; }
        public string VehicleNo { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string Activity { get; set; }
        public string PartyName { get; set; }
        public string UserName { get; set; }
        public string btnClass { get; set; }
        public string partyName { get; set; }
        public string Drivername { get; set; }
        public string Drivercontact { get; set; }
        public string Containercount { get; set; }
        public string Inout { get; set; }
        public string ContainerNo { get; set; }
        public string ActivityCycle { get; set; }
       
    }
}
