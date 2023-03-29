using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class CustArrivalEntities
    {
        public string TEUS { get; set; }
        public string size20 { get; set; }
        public string size40 { get; set; }
        public string size45 { get; set; }
        public string Person { get; set; }
        public string TTLCount { get; set; }

        public string css { get; set; }

        public double totalTEUSForImportArrivalDisplay { get; set; }
        public double donutValue { get; set; }
    }
}
