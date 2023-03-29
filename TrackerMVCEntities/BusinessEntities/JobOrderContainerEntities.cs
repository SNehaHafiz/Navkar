using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class JobOrderContainerEntities
    {
        public JobOrderContainerEntities()
        {

            JOValidation = new JobOrderDEntities();
            Containerlist = new List<JobOrderDEntities>();
        }
        public List<JobOrderDEntities> Containerlist { get; set; }
        public JobOrderDEntities JOValidation { get; set; }
    }
}
