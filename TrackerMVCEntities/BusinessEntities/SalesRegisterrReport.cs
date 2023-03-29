using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class SalesRegisterrReport
    {

      public int Srno { get; set; }
        public string InvoiceNo { get; set; }

        public string AssessDate { get; set; }

        public string Activity { get; set; }
        public string Fdate { get; set; }

        public string Todate { get; set; }

        public string Deliverytype { get; set; }

        public string BLNumber { get; set; }

        public string CHAName { get; set; }

        public string SLName { get; set; }

        public string Consignee { get; set; }

        public string GSTName { get; set; }

        public string GSTIn_uniqID { get; set; }

        public string NetTotal { get; set; }

        public string DiscAmount { get; set; }

        public string TaxableAmount { get; set; }

        public string TotalGST { get; set; }

        public string GrandTotal { get; set; }

        public string Remarks { get; set; }

        public string UserName { get; set; }

        public string Category { get; set; }

        public string DocketNo { get; set; }

        public string DocketDate { get; set; }



    }
}
