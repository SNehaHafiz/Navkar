using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonDT
{
    public class Declaration
    {
        public string messageType { get; set; }
        public string portOfReporting { get; set; }
        public string reportingEvent { get; set; }

        public int jobNo { get; set; }
        public string jobDate { get; set; }

    }
}
