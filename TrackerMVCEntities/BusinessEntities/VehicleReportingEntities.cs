using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class VehicleReportingEntities
    {
        public int ReportingID { get; set; }
        public int SrNo { get; set; }
        public string Reportingon { get; set; }
        public string TrailerNO { get; set; }
        public string Driverid { get; set; }
        public string DriverName { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }
        public string DWellHours { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string TransDate { get; set; }
        public string Size { get; set; }
        public string Transporter { get; set; }
        public string TrolleyNo { get; set; }
        public string MobileNo { get; set; }

    }
}
