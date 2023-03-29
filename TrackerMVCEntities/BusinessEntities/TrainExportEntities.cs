using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class TrainExportEntities
    {
        public string ContainerNo { get; set; }
        public string ContainerType { get; set; }

        public short Size { get; set; }
        public string Status { get; set; }
        public string Srno { get; set; }
        public string LineName { get; set; }
        public int LineId { get; set; }
        public string IcdIndate { get; set; }
        public string DocumentReadyDate { get; set; }
        public string PrePlanDate { get; set; }
        public string Remarks { get; set; }
        public string DwellDays { get; set; }
        public int entryid { get; set; }
        public string selected { get; set; }
        public string wagonNo { get; set; }
        public int TEUS { get; set; }
        public string process { get; set; }
        public int portId { get; set; }
        public int trainId { get; set; }
        public string TrailerNo { get; set; }
    }
}
