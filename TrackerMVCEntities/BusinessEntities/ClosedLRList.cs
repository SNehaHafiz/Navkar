using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ClosedLRList
    {
        public int SrNo { get; set; }
        public string LRNo { get; set; }
        public string LRDate { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Activity { get; set; }
        public string Vehicleno { get; set; }
        public string Customer { get; set; }
        public string F_location { get; set; }
        public string T_Location { get; set; }
        public string Weight { get; set; }
        public string BOENo { get; set; }
        public string Stuffloc { get; set; }
        public string LRdateOpen { get; set; }
        public string LRdateClose { get; set; }
        public string DwellHours { get; set; }
        public string FactoryReportingTime { get; set; }
        public string FactoryInTime { get; set; }
        public string FactoryOutTime { get; set; }
        public string DwellHoursFactoryInOut { get; set; }
        public string PreparedBy { get; set; }
        public string LRClosedBy { get; set; }
        public int DocID { get; set; }
        public string DocumentType { get; set; }
        public string FilePath { get; set; }
    }
}
