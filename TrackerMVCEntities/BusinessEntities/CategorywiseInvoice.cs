using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class CategorywiseInvoice
    {
        public int Select { get; set; }
        public string Category { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceDate1 { get; set; }
        public string TallyName { get; set; }
        public string GSTNo { get; set; }
        public string GSTName { get; set; }
        public string State { get; set; }
        public double SGST { get; set; }
        public double CGST { get; set; }
        public double IGST { get; set; }
        public double TotalGST { get; set; }
        public double GrandTotal { get; set; }
        public double InvoiceAmount { get; set; }
        public double CreditAmount { get; set; }
        public double PrevReceiptAmount { get; set; }
        public double ReceivalAmount { get; set; }
        public double ReceivedAmount { get; set; }
        public double NetReceivedAmount { get; set; }
        public double TDS { get; set; }
        public double OS { get; set; }
        public string WorkYear { get; set; }
        public int TDSPerId { get; set; }
        public string LRno { get; set; }
        public string DocketNo { get; set; }
        public string DocketDate { get; set; }
        public string CourierAddress { get; set; }
        public string ChaName { get; set; }
        public int AccountNoId { get; set; }

        public string V_INVOICE_DATE { get; set; }
        public string VOUCHER_TYPE { get; set; }
        public string REF_T { get; set; }
        public string LEDERNAME { get; set; }
        public string DR_CR { get; set; }
        public string Amount { get; set; }
        public string TDSAmount { get; set; }
        public int SRNO { get; set; }
        public string AssessNo { get; set; }
        public string AssessYear { get; set; }
        public long BillParty { get; set; }
        public long CHAID { get; set; }
        public long PaymentId { get; set; }
        public string BankId { get; set; }
        public string ModeNo { get; set; }
        public string ModeDate { get; set; }
        public string PayFrom { get; set; }
        public long ReceiptNo { get; set; }
        public string CancelReasonID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BtnEditCss { get; set; }
        public string AssessDate1 { get; set; }
        public string ReferenceNo { get; set; }
        // string Workyear { get; set; }
         
    }

    public class CategorySearchInvoice
    {
        //public string Category1 { get; set; }
        public string Category { get; set; }
        public string AssessNo { get; set; }
        public string AssessYear { get; set; }
        public string InvoiceNo { get; set; }
        public string AssessDate { get; set; }
        public string ValidData { get; set; }
        public string AssessType { get; set; }
        public string DeliveryType { get; set; }
        public string BIrtingDate { get; set; }
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string BlNumberNO { get; set; }
        public string TariffID { get; set; }
        public string Des { get; set; }
        public string LineName { get; set; }
        public string CHaNAme { get; set; }

        public string Consignee { get; set; }
        public string WaiverRemarks { get; set; }
        public string TallyLedger { get; set; }
        public string Remarks { get; set; }
        public string AssessmentBy { get; set; }
        public string AssessmentOn { get; set; }
        public string ReceiptBy { get; set; }
        public string ReceipOn { get; set; }
        public string NetTotal { get; set; }
        public string DiscountAmt { get; set; }
        public string TariffMappedBy { get; set; }
        public string TariffMappedOn { get; set; }
        public string ServiceTax { get; set; }
        public string SGST { get; set; }
        public string RebataBillNo { get; set; }
        public string PortChargesinvoiceNo { get; set; }
        public string CGST { get; set; }
        public string IGST { get; set; }
        public string GSTInNumber { get; set; }
        public string PartyName { get; set; }
        public string GrandTotal { get; set; }
        public string Checkedby { get; set; }
        public string AuditRemark { get; set; }
        public string Value { get; set; }
        public string Duty { get; set; }
        public string Jono { get; set; }
        public string Sbno { get; set; }
        public string ShipperName { get; set; }
       public string Noof20 { get; set; }
        public string Noof40 { get; set; }
        public string Noof45 { get; set; }
    }
    public class TeusSearch
    {
        public string txt20 { get; set; }
        public string txt40 { get; set; }
        public string txt45 { get; set; }
        public string Teus { get; set; }
    }


    public class OnScreenAj
    {
        public string ReciptNo { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceAmount { get; set; }
        public string TDS { get; set; }
        public string PrevReceiptAmount { get; set; }
        public string ReceivalAmount { get; set; }
        public string ReceivedAmountTDS { get; set; }
        public string NetReceivedAmount { get; set; }
        public string CustID { get; set; }
        public string PendingReceipt { get; set; }
        
    }


    }
