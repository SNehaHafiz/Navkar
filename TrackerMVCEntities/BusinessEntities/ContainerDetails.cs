using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ContainerDetails
    {
        public int JONO { get; set; }
        public string ContainerNO { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public bool IsSelected { get; set; }
        public string WagonNo { get; set; }
        public string Remarks { get; set; }
        public int TEUS { get; set; }
        public int PortID { get; set; }
        public int TrainID { get; set; }
        public string DischargeDate { get; set; }
        public string ArrivalDate { get; set; }
        public long PKGS { get; set; }
        public double Weight { get; set; }
        public string selected { get; set; }
        public string TrailerNo { get; set; }

        public string portName { get; set; }
        public string portOutDate { get; set; }
        public int validationMessage { get; set; }
        public string containerList { get; set; }
        public string JOStatus { get; set; }
        public string OutBy { get; set; }
        public int SRNo { get; set; }
        public string FCLLCType { get; set; }
        public string Message { get; set; }
    }
}
