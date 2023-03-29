using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class GSTReturnEntities
    {
        public string Category { get; set; }
        public string GSTRecipient {get;set;}
        public string ReceiverName {get;set;}
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceValue { get; set; }
        public string PlaceOfSupply { get; set; }
        public string ReverseChange { get; set; }
        public string InvoiceType { get; set; }
        public string Rate { get; set; }
        public string TaxValue { get; set; }
        public string IGST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string TotalGST { get; set; }
        public string HSNCode { get; set; }
        public string srno { get; set; }
        public string Trip { get; set; }
    }
    public class OutActivity
    {
        public string OutActivityID { get; set; }

        public string OutActivityName { get; set; }
    }

    public class InActivity
    {
        public string InActivityID { get; set; }

        public string InActivityName { get; set; }
    }
}
