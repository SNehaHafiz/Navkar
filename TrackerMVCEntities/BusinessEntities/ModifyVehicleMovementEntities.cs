using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ModifyVehicleMovementEntitiesave
    {
        public int ID { get; set; }
        public int ToID { get; set; }
        public string TrailerNo { get; set; }
        public string Remarks { get; set; }
        public string Activity { get; set; }
        public string ContainerNo { get; set; }
        public string Trailer { get; set; }
    }

    public  class ModifyVehicleMovementEntities
    {
        public string fromid { get; set; }
        public string toid { get; set; }
        public string TrailerNo { get; set; }
        public string Remarks { get; set; }
        public string Activity { get; set; }
        public string ContainerNo { get; set; }

    }

    public class LocationFrom
    {
        public string Criteria { get; set; }
        public int ID { get; set; }
    }

    public class LocationTo
    {
        public string ToCriteria { get; set; }
        public int ToID { get; set; }
    }

    public class trailer
    {
        public string trailerName { get; set; }
        
    }
}
