using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class TrainMaster
    {
        public int TrainID { get; set; }
        public string TrainNO { get; set; }
        public string PortFrom { get; set; }
        public string PortTO { get; set; }
        public int OperatorID { get; set; }
        public string Operator { get; set; }
        public string DateNTime { get; set; }
        public int AddedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string IsImportORExportSelected { get; set; }
        public string DepartureDate { get; set; }
        public int TEUS { get; set; }
        public string RemovalDate { get; set; }
        public string PortLineNo { get; set; }
        public string PlacementDate { get; set; }
        public string PortName { get; set; }
        public int SRNo { get; set; }
        public string InTransitSince { get; set; }
        public string TrainArrivedDate { get; set; }
    }
}
