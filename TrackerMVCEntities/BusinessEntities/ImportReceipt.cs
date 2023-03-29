using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class ImportReceipt
    {
        public int Select { get; set; }
        public string Category { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public double ReceivedAmount { get; set; }
        public string VoucherName { get; set; }
        public string VCNNo { get; set; }
        public string BillNo { get; set; }
        public string REF { get; set; }
        public string LedgerName { get; set; }
        public string DRCR { get; set; }
        public string Narration { get; set; }
        public string FavouringName { get; set; }
        public string TransType { get; set; }
        public string InstNo { get; set; }
        public string InstDate { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string CategoryName { get; set; }
    }
    public class LedgerDetails
    {
        public string LedgerID { get; set; }
        public string GSTName { get; set; }
        public string State { get; set; }
        public string GSTIn_uniqID { get; set; }
        public string GSTAddress { get; set; }
        public string Panno { get; set; }
        public string PINCODE { get; set; }
    }
}
