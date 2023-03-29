using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class ExportOutReport
    {
        public int SrNo { get; set; }
        public string ContainerNo { get; set; }

        public string SBNo { get; set; }

        public string SBDate { get; set; }

        public string SBCargoType { get; set; }

        public string Size { get; set; }
        public string Type { get; set; }
        public string Commodity { get; set; }
        public string FoB { get; set; }
        public string StuffingDate { get; set; }
        public string GPDate { get; set; }
        public string MovOrderDate { get; set; }
        public string CHAName { get; set; }
        public string OnAccount { get; set; }

        public string ShippingLine { get; set; }
        public string VesselName { get; set; }
        public string ExportName { get; set; }
        public string Port { get; set; }
        public string POD { get; set; }
        public string FOD { get; set; }
        public string TrailerNo { get; set; }

        public string Transporter { get; set; }
        public string EmptyInDate { get; set; }
        public string EmptyPickupYard { get; set; }

        public string AgentSeal { get; set; }

        public string CustomSeal { get; set; }

        public string MoveType { get; set; }

        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }

        public string TareWeight { get; set; }

        public string Remarks { get; set; }
        public string GPGeneratedBy { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string Customer { get; set; }
        public string TransTYpe { get; set; }


        public string FinalOutDate { get; set; }

        public string Location { get; set; }





    }
}
