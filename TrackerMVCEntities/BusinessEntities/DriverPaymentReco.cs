using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class DriverPaymentReco
    {
        public int SRNo { get; set; }
        public string IFSCCode { get; set; }
        public string Amount { get; set; }
        public string TransferFromAccnt { get; set; }
        public string Transporter { get; set; }
        public int TransporterID { get; set; }
        public string TransferInAccnt { get; set; }
        public string Driver { get; set; }
        public int DriverID { get; set; }
        public string TransferType { get; set; }
        public string PaymentStatus { get; set; }
        public string ReferenceNo { get; set; }
        public string FileName { get; set; }
        public string TransDate { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherFor { get; set; }
        public int validationMessage { get; set; }
        public string containerList { get; set; }
        public DriverPaymentReco()
        {
            DriverList = new List<DriverDropDown>();
        }
        public List<DriverDropDown> DriverList { get; set; }
    }
    public class DriverDropDown
    {
        public int DriverID { get; set; }
        public string DriverName { get; set; }
    }
}
