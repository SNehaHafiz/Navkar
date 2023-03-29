using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public   class CreditNoteEntities
    {
        public string CATEGORYID { get; set; }
        public string CATEGORYType { get; set; }

        public string AssessNo { get; set; }
        public string WorkYear { get; set; }
        public string InvoiceNo { get; set; }
        public string GSTName { get; set; }
        public string GSTAddress { get; set; }
        public string IsTax { get; set; }
        public string JONo { get; set; }
        public string TaxID { get; set; }
        public string AccountID { get; set; }
        public string accountname { get; set; }
        public string CONTAINERNO { get; set; }
        public string Size { get; set; }
        public string creditamount { get; set; }
        public string amount { get; set; }
        public string PaidAmount { get; set; }
        public string Srno { get; set; }
        public string ZeroAmount { get; set; }


    }

    public class CreditNoteAmountEntities
    {
        
        public string AssessNo { get; set; }
        public string WorkYear { get; set; }
        public string InvoiceNo { get; set; }
        public string GSTName { get; set; }
        public string GSTAddress { get; set; }
        public string nettotal { get; set; }
        public string sgst { get; set; }
        public string cgst { get; set; }
        public string igst { get; set; }
        public string grandtotal { get; set; }
        public string partyid { get; set; }
        public string Size { get; set; }
        public string creditamount { get; set; }
        public string amount { get; set; }
        public string statecode { get; set; }
        public string ContainerNo { get; set; }
        public string JONo { get; set; }
        public string AccountID { get; set; }
        public string accountname { get; set; }
        public string CreditedAmount { get; set; }
        public string TaxID { get; set; }
    }

    public class CreditNoteCre
    {

        public string txtcreditamount { get; set; }
        public string txtsgstcredit { get; set; }
        public string txtcgstcredit { get; set; }
        public string txtigstcredit { get; set; }
        public Double txtcredittotal { get; set; }
        public string dblalltotal { get; set; }
        public string dbltotal { get; set; }
        public string dbldisc { get; set; }
        

    }

}
