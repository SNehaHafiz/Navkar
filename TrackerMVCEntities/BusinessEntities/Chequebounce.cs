using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class Chequebounce
    {
        public int ENTRYID { get; set; }
        public DateTime ENTRYDATE { get; set;}
        public string CHEQUENO { get; set; }
        public string RECEIPTNO { get; set; }
        public string ACTIVITY { get; set; }
        public string CHEQUE_AMOUNT { get; set; }
        public string CHEQUEDATE { get; set; }
        public string CHEQUE_RETURNDATE { get; set; }

        public string BANKNAME { get; set; }
        public string CONTACTPERSONNAME { get; set; }
        public string CONTACTNO { get; set; }
        public string REMARKS { get; set; }

        public string paymentMode { get; set; }
        public string  ModeNo { get; set; }
        public string ModeDate { get; set; }
        public string  ModeAmount { get; set; }

    }
}
