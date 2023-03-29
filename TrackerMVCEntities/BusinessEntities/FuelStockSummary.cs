using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class FuelStockSummary
    {

        public int SrNo { get; set; }
        public string fuelregid { get; set; }
        public string EntryDate { get; set; }
        public string CostCenter { get; set; }
        public string vendorname { get; set; }
        public string vehicleNo { get; set; }
        public string fuelType { get; set; }
        public string qty { get; set; }
        public string Rate { get; set; }
        public string username { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string cmbFueltype { get; set; }
        public string fuelRefNo { get; set; }
        public string btnClass { get; set; }
        public string CostCenterFor { get; set; }
        public string FuelFor { get; set; }
        public string BillNo { get; set; }
        public string BillAmt { get; set; }
    }
}
