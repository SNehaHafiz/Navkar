using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonDT
{
    public class CargoContainer
    {
        public CargoContainer()
        {
            cargoDocument = new List<CargoDocument>();
        }

        public string messageType { get; set; }
        public int equipmentSequenceNo { get; set; }
        public string equipmentID { get; set; }
        public string equipmentType { get; set; }
        public string equipmentSize { get; set; }
        public string equipmentLoadStatus { get; set; }
        //public string equipmentStatus { get; set; }
        public string finalDestinationLocation { get; set; }
        public string equipmentSealType { get; set; }
        public string equipmentSealNumber { get; set; }
        public List<CargoDocument> cargoDocument { get; set; }
    }
}
