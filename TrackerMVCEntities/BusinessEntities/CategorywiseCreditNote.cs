using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class CategorywiseCreditNote
    {
        public int Select { get; set; }
        public string Category { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceDate1 { get; set; }
        public string TallyName { get; set; }
        public string GSTNo { get; set; }
        public string GSTName { get; set; }
        public string State { get; set; }
        public double SGST { get; set; }
        public double CGST { get; set; }
        public double IGST { get; set; }
        public double TotalGST { get; set; }
        public double GrandTotal { get; set; }
        public string WorkYear { get; set; }


        public string V_INVOICE_DATE { get; set; }
        public string VOUCHER_TYPE { get; set; }
        public string REF_T { get; set; }
        public string LEDERNAME { get; set; }
        public string DR_CR { get; set; }
        public string AMOUNT { get; set; }
    }
}
