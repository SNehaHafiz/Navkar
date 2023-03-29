using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonDT
{
    public class CargoDocument
    {
        public string messageType { get; set; }
        public int cargoSequenceNo { get; set; }
        public string documentType { get; set; }
        public string documentSite { get; set; }
        public int documentNumber { get; set; }
        public string documentDate { get; set; }
        public string shipmentLoadStatus { get; set; }
        public string packageType { get; set; }
        public string MCINPCIN { get; set; }
    }
}
