using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class ModifyInternalFuelConsumptionEntities
    {
        public string TrailerNo { get; set; }
        public string fuelRefNo { get; set; }
        public string Qty { get; set; }
        public string Remarks { get; set; }
        public string fuelqty { get; set; }

        public string lastReading { get; set; }
        public string currentReading { get; set; }
    }
}
