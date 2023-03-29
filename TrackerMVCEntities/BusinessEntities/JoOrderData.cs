using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class JoOrderData
    {
        public JoOrderData()
        {

            JOList = new JobOrderMEntities();
            Containerlist = new List<JobOrderDEntities>();
        }
        public JobOrderMEntities JOList { get; set; }
        public List<JobOrderDEntities> Containerlist { get; set; }
    }
}
