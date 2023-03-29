using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class LoadingPlanEntities
    {
        public long JONo { get; set; }

        public string ContainerNo { get; set; }
        public string GPNo { get; set; }
        public string CustomerName { get; set; }
        public string Destination { get; set; }
        public string TrailerNo { get; set; }
        public string KalmarNo { get; set; }
        public int KalmarID { get; set; }
        public short Size { get; set; }
    }
}
