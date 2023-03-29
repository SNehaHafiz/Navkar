using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class PendingContainerForEmptyOffLoading
    {
        public int SrNo { get; set; }
        public string ContainerNo  { get;set;}
        public string SLName { get;set;}
        public string DoValiddate { get;set;}
        public string BLNumber { get;set;}
        public string VehicleNo { get;set;}
        public string OutDate  { get;set;}
        public string ImporterName { get;set;}
        public string InDate { get;set;}
        public string OffloadingLocation { get; set; }
        public string Deliverytype { get; set; }
        public string validdate { get; set; }


        public string remarks { get; set; }


        public string Status { get; set; }
        public string TypeSize { get; set; }

        public string Type { get; set; }
        public string Size { get; set; }

        public string CHAName { get; set; }
        public string DocReceivedDate { get; set; }

    }
}
