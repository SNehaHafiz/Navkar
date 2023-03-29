using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class VoucherDetailsEntities
    {
        public string VoucherDate  { get;set;}
        public string TrailerNo { get; set; }
        public string Status { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string Drivername { get; set; }
        public string Activity { get; set; }
        public string AmountCash { get; set; }
        public string AmountBank { get; set; }
        public string Fuel { get; set; }
        public string AdjustmentsFuel { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string StartReading { get; set; }
        public string EndReading { get; set; }
        public string Fastagamount { get; set; }
        public string VoucherNO { get; set; }

        public string SlipNo { get; set; }
        public string InOut { get; set; }
        public string ENTRY_Type { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string TrollySize { get; set; }
        public string Transpoter { get; set; }
        public string SlipDate { get; set; }
        public string SlipID { get; set; }
        public string FullSlipVendorBillNo { get; set; }
        public string CashPaymentTransID { get; set; }

        public string remarks { get; set; }
        public int LocationID { get; set; }
        public int Driverid { get; set; }
        public int FromLocationIDd { get; set; }
        public int TOLocationIDd { get; set; }
        public string ActivityAutoid { get; set; }
        public string ActivityName { get; set; }
        
        public string ContainerSize { get; set; }
        public string AutoID { get; set; }
        public string SRNo { get; set; }
        public string Count { get; set; }

    }
}
