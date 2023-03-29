using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class WagonContainerList
    {
        public WagonContainerList()
        {

            JOValidation = new TrainTrip();
            Containerlist = new List<TrainTrip>();
        }
        public List<TrainTrip> Containerlist { get; set; }
        public TrainTrip JOValidation { get; set; }
    }
}
