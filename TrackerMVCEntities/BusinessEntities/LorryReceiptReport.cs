using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class LorryReceiptReport
    {
        public int SrNo { get; set; }
        public string LRNo { get; set; }

        public string LRDate { get; set; }

        public string BookingNo { get; set; }

        public string AGName { get; set; }

        public string ShippingLine { get; set; }

        public string FromLocation { get; set; }

        public string ToLocation { get; set; }

        public string DoDate { get; set; }

        public string Activity { get; set; }

        public string ContainerNo { get; set; }


        public string Size { get; set; }
        public string Type { get; set; }

        public string VehicleNo { get; set; }

        public string AgentSeal { get; set; }


        public string BOENo { get; set; }

        public string AddedBy { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string category { get; set; }
        public string Remarks { get; set; }
    }
}
