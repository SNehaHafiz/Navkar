using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ReadyVehicleMovementEntities
    {
        public int SrNo { get; set; }
        public string Priority { get; set; }
        public string trailername { get; set; }
        public string TrailerSize { get; set; }
        public string Grade { get; set; }
        public string FromLocation { get; set; }
        public string Drivername { get; set; }
        public string contactno { get; set; }
        public string TransDate { get; set; }
        public string reportedon { get; set; }
        public string DwellHours { get; set; }
        public string Transporter { get; set; }

    }
}
