using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class FastagDetailsEntries
    {
        public string TransactionDateTime { get; set; }
        public string ProcessedDateTime { get; set; }
        public string LicencePlateNo { get; set; }
        public string TagAccountNo { get; set; }
        public string Group { get; set; }
        public string Activity { get; set; }
        public string PlazaCode { get; set; }
        public string TransactionDescription { get; set; }
        public string UniqueTransactionID { get; set; }
        public string AmountCR { get; set; }
        public string AmountDR { get; set; }
        public int SRNo { get; set; }
        public int validationMessage { get; set; }
        public string containerList { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherDate { get; set; }
        public string FASTAGamt { get; set; }
        public string fromlocation { get; set; }
        public string Tolocation { get; set; }
    }
}
