using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonAR
{
    public class HeaderField
    {
        public string indicator { get; set; }
        public string messageID { get; set; }
        public int sequenceOrControlNumber { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string reportingEvent { get; set; }
        public string senderID { get; set; }
        public string receiverID { get; set; }
        public string versionNo { get; set; }
    }
}
