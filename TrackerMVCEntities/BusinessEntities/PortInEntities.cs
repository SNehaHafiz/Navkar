using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class PortInEntities
    {

        public int  PlanningID {get ;set;}

        public string PlanningDate { get; set; }
        public string ProcessType { get; set; }
        public string EntryID { get; set; }
        public string ContainerNo { get; set; }
        public string TransType { get; set; }
        public string TrainNo { get; set; }
        public string WagonNo { get; set; }
        public string TrailerNo { get; set; }
        public string FromDestination { get; set; }
        public string ToDestination { get; set; }
        public string PlannedBy { get; set; }
        public string PlannedON { get; set; }
        public string OutDate { get; set; }
        public string PortIndate { get; set; }
        public string PortInRemarks { get; set; }

        public string PortInBy { get; set; }
        public string LoadingBy { get; set; }
        public string LoadingDate { get; set; }
        public string LoadRemars { get; set; }

        public string Remarks { get; set; }
    }
}
