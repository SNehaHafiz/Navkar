using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonASR
{
    public class CargoItnry
    {
        public int prtOfCallSeqNmbr { get; set; }
        public string prtOfCallCdd { get; set; }
        public string prtOfCallName { get; set; }
        public string nxtPrtOfCallCdd { get; set; }
        public string nxtPrtOfCallName { get; set; }
        public string modeOfTrnsprt { get; set; }
    }
}
