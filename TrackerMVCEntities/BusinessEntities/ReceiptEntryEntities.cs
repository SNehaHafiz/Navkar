using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ReceiptEntryEntities
    {

        public string Select {get;set;} 
        public string JoNo { get;set;} 
        public string ContainerNo { get;set;} 
        public string ISOCode { get;set;} 
        public string Size { get;set;} 
        public string ContainerType { get;set;} 
        public string InDate { get;set;} 
        public string GPDate { get;set;} 
        public string DeliveryType { get;set;} 
        public string RMS { get;set;} 
        public string IGM_PackType { get;set;} 
        public string IGM_QTY { get;set;} 
        public string Tare_Weight { get;set;} 
        public string IGM_MT_WT { get;set;} 
        public string GrossWt { get;set;} 
        public string ScanType { get;set;} 
        public string SealNoI { get;set;} 
        public string examinper { get;set;} 
        public string agentid { get;set;} 
        public string DoValiddate { get;set;} 
        public string CheckStatus { get;set;}
        public int ReceiptNo { get; set; }
        public string ReceiptRefNo { get; set; }
        public double TotalAmount { get; set; }
        public double TotalAmountForJB { get; set; }
        public string Invoiceamt { get; set; }
        public string ReceivedAMt { get; set; }
        public string NetAmount { get; set; }
        public string TDS { get; set; }
        public string Addedby { get; set; }
        public string AssessDate { get; set; }
        public string WorkYear { get; set; }
        public string InvoiceNo { get; set; }
    }
}
