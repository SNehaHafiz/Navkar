using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonASR
{
    public class EGM
    {
        public EGM()
        {
            headerField = new HeaderField();
            master = new Master();
            //digSign = new DigSign();
        }
        public HeaderField headerField { get; set; }
        public Master master { get; set; }
        //public DigSign digSign { get; set; }
    }
}
