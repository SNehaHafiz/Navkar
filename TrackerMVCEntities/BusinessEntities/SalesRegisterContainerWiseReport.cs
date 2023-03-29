using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class SalesRegisterContainerWiseReport
    {

        public int Srno { get; set; }
        public string LINENAME { get; set; }

        public string CHANAME { get; set; }

        public string Category { get; set; }

        public string InvoiceNo { get; set; }

        //public string LINENAME { get; set; }

        public string FINALINVOICEREFNO { get; set; }

        public string ContainerNo { get; set; }

        public string size { get; set; }

        public string ContainerType { get; set; }

        public string CargoType { get; set; }

        public string INVOICEDATE { get; set; }

        public string BILLITEMDESCRIPTION { get; set; }

        public string billamount { get; set; }

        public string GSTAmt { get; set; }

        public string GST { get; set; }

        public string Fdate { get; set; }

        public string Todate { get; set; }

        public string Activity { get; set; }

        public string partyName { get; set; }

        public string receiptREFno { get; set; }



    }
}
