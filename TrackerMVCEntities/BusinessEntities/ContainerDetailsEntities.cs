using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ContainerDetailsEntities
    {

        public int JONO { get; set; }
        public string ContainerNO { get; set; }
        public string Size { get; set; }
        public string ContainerType { get; set; }

        public string INDate { get; set; }

        public string ShippingLine { get; set; }
        public string RMSPerc { get; set; }
    }
}
