using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class TollSummary
    {
        public int TollID { get; set; }
        public int SRno { get; set; }
        public string TollName { get; set; }
        public int Amount { get; set; }
        public int Returnamount { get; set; }
        public string Passing { get; set; }
        public string tollMasterid { get; set; }
      
    }
}
