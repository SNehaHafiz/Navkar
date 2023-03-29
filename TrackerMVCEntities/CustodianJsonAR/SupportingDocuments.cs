using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonAR
{
   public class SupportingDocuments
    {
        public string messageType { get; set; }
        public int equipmentSerialNumber { get; set; }
        public int documentSerialNumber { get; set; }
        public string documentTypeCode { get; set; }
        public string ICEGATEUserID { get; set; }
        public int IRNNumber { get; set; }
        public string documentReferenceNumber { get; set; }
    }
}
