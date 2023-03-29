using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public  class RTOTaxEntities
    {
        public string InvoiceNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TODate { get; set; }
        public string Amount { get; set; }
        public string VehicleNo { get; set; }
    }
}
