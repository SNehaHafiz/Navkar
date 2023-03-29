using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class VesselMaster
    {
        public int VesselID { get; set; }
        public string VesselName { get; set; }
        public bool IsActive { get; set; }
        public int AddedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
