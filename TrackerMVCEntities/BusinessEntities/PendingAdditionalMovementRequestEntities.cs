using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class PendingAdditionalMovementRequestEntities
    {
        public string RequestID { get; set; }
        public string RequestOn { get; set; }
        public string VoucherNo { get; set; }
        public string ContainerNo { get; set; }
        public string Activity { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string Remarks { get; set; }
        public string FuelQuanity { get; set; }
        public string Kilometer { get; set; }
    }
}
