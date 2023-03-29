using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class VesselMovement
    {
        public int MovementID { get; set; }
        public int VesselID { get; set; }
        public string ViaNO { get; set; }
        public int PortID { get; set; }
        public string DateNTime { get; set; }
        public int AddedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string VoyageNo { get; set; }
        public string IsImportORExportSelected { get; set; }
        public DateTime berthingdate { get; set; }
        public string IGMNo { get; set; }
        public DateTime IGMDate { get; set; }
        public string IGMDateFormat { get; set; }

    }
}
