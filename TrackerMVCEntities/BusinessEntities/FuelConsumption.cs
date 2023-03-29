using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class FuelConsumption
    {
        public string BalQty { get; set; }
        public string fuelRefNo { get; set; }
        public string IssueDate { get; set; }
        public string trailername { get; set; }
        public string drivername { get; set; }
        public string issueQty { get; set; }
        public string fuelType { get; set; }
        public string lastReading { get; set; }
        public string currentReading { get; set; }
        public string Costcenter { get; set; }
        public string Remaining { get; set; }
        
        public string id { get; set; }
        public string btnClass { get; set; }
        public string AddedBy { get; set; }
        public string SlipNo { get; set; }
        public int VehicleType { get; set; }
        public string CostCenterFor { get; set; }
        public string Differencereading { get; set; }
        public string Fueltype { get; set; }
        public string VehicleTypeName { get; set; }
        public string Remarks { get; set; }
        public string CostCenterFid { get; set; }
        public string workyear { get; set; }
        
        
        public string StockSLipNo { get; set; }
        public string ReceiveQty { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
    }
}
