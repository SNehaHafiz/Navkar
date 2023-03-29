using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class ReverseDebitNoteEntities
    {
        public string PartName { get; set; }
        public string Activity { get; set; }
        public string CreditAmount { get; set; }
        public string CreditSGSTt { get; set; }
        public string CreditCGST { get; set; }
        public string CreditIGST { get; set; }
        public string CreditTotal { get; set; }
     public string WorkYear { get; set; }
        public string OldCredit { get; set; }
        public string statecode { get; set; }
        public string partyid { get; set; }
    }
}
