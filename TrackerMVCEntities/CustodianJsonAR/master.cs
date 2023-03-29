using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonAR
{
    public class Master
    {
        public Master()
        {
            declaration = new Declaration();
            location = new Location();
            cargoContainer = new List<CargoContainer>();
            transportMeans = new TransportMeansType();
            CIM = new CIM();
            supportingDocuments = new List<SupportingDocuments>();
        }
        public Declaration declaration { get; set; }
        public Location location { get; set; }
        public List<CargoContainer> cargoContainer { get; set; }
        public TransportMeansType transportMeans { get; set; }
        public Events events { get; set; }
        public CIM CIM { get; set; }
        public List<SupportingDocuments> supportingDocuments { get; set; }
    }
}
