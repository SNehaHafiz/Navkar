using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    
   public  class ReceiptAdjustments

    {
        public string Criteria { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptYear { get; set; }
        public string NetAmount { get; set; }
        public string RecieptDate { get; set; }
        public string Mode { get; set; }
        public int ModeID { get; set; }
        public string Amount { get; set; }
        public string ModeNo { get; set; }
        public string BankName { get; set; }
        public string BankID { get; set; }
        public string ModeDate { get; set; }
        public string  Payfromid { get; set; }
        public string ID { get; set; }

    }
}
