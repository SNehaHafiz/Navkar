using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class FuelBillverificationsEntiites
    {
        public int SRno { get; set; }
        public int ID { get; set; }
        public string BillYear { get; set; }
        public string Billno { get; set; }
        public string BillDate { get; set; }
    }


    public class FuelBillVerificationDetails
    {
        public string BillNo { get; set; }
        public string BillYear { get; set; }
        public string periodfrom { get; set; }
        public string periodto { get; set; }
        public string BillDate { get; set; }
        public string InvoiceNo { get; set; }
        public string SlipNo { get; set; }
        public string Fromloc { get; set; }
        public string Toloc { get; set; }
        public string FuelQty { get; set; }
        public string fueltype { get; set; }
        public string trailerNo { get; set; }
        public string Activity { get; set; }
        public string fuel_VendorName { get; set; }
        public string Rate { get; set; }
        public double totamt { get; set; }
        public string Voucherno { get; set; }
        public string SLIpdate { get; set; }
        public string VendorQty { get; set; }
        public string MessageValidation { get; set; }
        public string MessageValidation1 { get; set; }
        public int IsMassegePresent { get; set; }
    }


}
