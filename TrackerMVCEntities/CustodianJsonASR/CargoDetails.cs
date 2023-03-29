using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonASR
{
    public class CargoDetails
    {
        public CargoDetails()
        {
            cargoContainer = new List<CargoContainer>();
            cargoItnry = new List<CargoItnry>();
        }
        public string messageType { get; set; }
        public int cargoSequenceNo { get; set; }
        public string documentType { get; set; }
        public string documentSite { get; set; }
        public int documentNo { get; set; }
        public string documentDate { get; set; }
        public string shipmentLoadStatus { get; set; }
        public string packageType { get; set; }
        public int quantity { get; set; }
        public string MCINPCIN { get; set; }
        public List<CargoContainer> cargoContainer { get; set; }
        public List<CargoItnry> cargoItnry { get; set; }

    }
}
