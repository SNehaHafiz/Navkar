using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class DriverPaymentRecoSummaryEntities
    {
        public int SRNo { get; set; }
        public int PaidID { get; set; }
        public string FileName { get; set; }
        public string PaidDate { get; set; }
        public string VoucherNo { get; set; }
        public string TransDate { get; set; }
        public string Amount { get; set; }
        public string VoucherFor { get; set; }
        public string Transporter { get; set; }
        public string Driver { get; set; }
        public string DriverAccountNo { get; set; }
        public string TransferType { get; set; }
        public string ReferenceNo { get; set; }

        //public string FromDate { get; set; }
        //public string Todate { get; set; }
        //public string VoucherNoSearch { get; set; }
        //public string DriverSearch { get; set; }
    }
}
