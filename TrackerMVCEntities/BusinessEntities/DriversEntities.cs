using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class DriversEntities
    { 
        public long driverID {get;set;}
        public string driverName {get;set;}
 
    }

    public class TranspoterEntities
    {
        public long TransID { get; set; }
        public string TransName { get; set; }

    }


    public class DriverLeaveEntities
    {
        public long LeaveID { get; set; }
        public string LeaveName { get; set; }

    }

    public class PurposeEntites
    {
        public long PurposeID { get; set; }
        public string PurposeName { get; set; }

    }

    public class VehicleSubGoupEntites
    {
        public long GroupID { get; set; }
        public string Group { get; set; }

    }
}
