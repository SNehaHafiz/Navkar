using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class VehicleInOutDetails
    {
        public string ActivityEntryID { get; set; }
        public string Activity { get; set; }
        public string ActivityDate { get; set; }
        public string Fromlocation { get; set; }
        public string TOLocation { get; set; }
        public string Party { get; set; }
        public string Remarks { get; set; }
    }
}
