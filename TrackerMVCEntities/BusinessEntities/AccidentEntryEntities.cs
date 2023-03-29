using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class AccidentEntryEntities
    {
        public string EntryDate { get; set; }
        public string AccidentDate { get; set; }
        public string trailerno { get; set; }
        public string AccidentPlace { get; set; }
        public string DriverName { get; set; }
        public string Remarks { get; set; }
        public string AddedOn { get; set; }
        public int AddedBy { get; set; }

    }
}
