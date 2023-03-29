using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ExpensesForTransport
    {
        public int SrNo { get; set; }

        public string EntryDate { get; set; }
        public string Amount { get; set; }
        public string transexpensename { get; set; }
        public string Remark { get; set; }
        public int Voucherid { get; set; }
        public string VoucherNumber { get; set; }
        public string VoucherDate { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string Drivername { get; set; }
        public string Activity { get; set; }
        public string Party { get; set; }
        public string AutoID { get; set; }
        public string TrailerNo { get; set; }
        public string Username { get; set; }
        public string TrailerName { get; set; }
        public string ActivityMaster { get; set; }
        public string expenseFor { get; set; }
        public string Remarks { get; set; }
        public int Entryid { get; set; }
        public string ExpenseSlipNo { get; set; }
        public string PaymentMode { get; set; }
        public string ExpensesheadID { get; set; }
        public string AutoActivityid { get; set; }
        public string PaymentmodeID { get; set; }
        public int transexpenseID { get; set; }
        public string vendorID { get; set; }
        public string Taxid { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double IGST { get; set; }
        public double GrandTotal { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }

        public string CollectInvoiceNo { get; set; }
        public string CollectInvoiceDate { get; set; }

        public string CollectParty { get; set; }
        public string CollectAmount { get; set; }
         
    }
    public class TransportexpensesAttachment
    {
        public int SrNo { get; set; }
        public string MSNoFile { get; set; }
        public int DocID { get; set; }
        public string DocName { get; set; }
        public string FilePath { get; set; }
        public string FilePath1 { get; set; }
        public string DocumentType { get; set; }
        public string DocumenttypeID { get; set; }
        public string Filename { get; set; }
        public string Entryid { get; set; }



    }
}
