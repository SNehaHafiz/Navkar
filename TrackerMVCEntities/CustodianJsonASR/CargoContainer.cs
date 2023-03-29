using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonASR
{
    public class CargoContainer
    {
      
        public string messageType { get; set; }
        public int equipmentSequenceNo { get; set; }
        public string equipmentID { get; set; }
        public string equipmentType { get; set; }
        public string equipmentSize { get; set; }
        public string equipmentLoadStatus { get; set; }
        public string finalDestinationLocation { get; set; }
        public string eventDate { get; set; }
        public string equipmentSealType { get; set; }
        public string equipmentSealNumber { get; set; }
        public string equipmentStatus { get; set; }

    }
}
