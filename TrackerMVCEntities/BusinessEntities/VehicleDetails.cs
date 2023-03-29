using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class VehicleDetails
    {
        public int SrNo { get; set; }
        public string VehicleNo { get; set; }
        public string Transporter { get; set; }
        public string TotalTeus { get; set; }
        public string Activity { get; set; }
        public string Customer { get; set; }
        public string Destination { get; set; }
        public string Fuel { get; set; }
        public string Amount { get; set; }
        public string FitnessUpto { get; set; }
        public string InsuranceUpto { get; set; }
        public string TaxUpto { get; set; }
        public string KAM { get; set; }
        public string DelayHours { get; set; }
    }
}
