using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using System.Data;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TallySyncronizations
{
    public class TallySyncronizations
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();

        public List<EN.CategorywiseInvoice> getCategorywiseInvoice(string Date,string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getCategorywiseInvoice(Date,Category);
            List<EN.CategorywiseInvoice> ContainerList = new List<EN.CategorywiseInvoice>();
            
            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.CategorywiseInvoice ContainerDL = new EN.CategorywiseInvoice();

                    ContainerDL.Select      = Convert.ToInt32(row["Select"]);
                    ContainerDL.Category    = Convert.ToString(row["Category"]);
                    ContainerDL.InvoiceNo   = Convert.ToString(row["Invoice No"]);
                    ContainerDL.InvoiceDate = Convert.ToString(row["Invoice Date"]);
                    ContainerDL.InvoiceDate1 = Convert.ToString(Convert.ToDateTime(row["InvoiceDate"]).ToString("dd MMM yy"));
                    ContainerDL.TallyName   = Convert.ToString(row["Tally Name"]);
                    ContainerDL.GSTNo       = Convert.ToString(row["GST No"]);
                    ContainerDL.GSTName     = Convert.ToString(row["GSTName"]);
                    ContainerDL.State       = Convert.ToString(row["State"]);
                    ContainerDL.CGST        = Convert.ToDouble(row["CGST"]);
                    ContainerDL.IGST        = Convert.ToDouble(row["IGST"]);
                    ContainerDL.TotalGST    = Convert.ToDouble(row["Total GST"]);
                    ContainerDL.GrandTotal = Convert.ToDouble(row["Grand Total"]);
                    ContainerDL.WorkYear    = Convert.ToString(row["workyear"]);




                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.CategorywiseInvoice> getCategorywiseInvoiceCarting(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getCategorywiseInvoiceCarting(Date, Category);
            List<EN.CategorywiseInvoice> ContainerList = new List<EN.CategorywiseInvoice>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.CategorywiseInvoice ContainerDL = new EN.CategorywiseInvoice();

                    ContainerDL.Select = Convert.ToInt32(row["Select"]);
                    ContainerDL.Category = Convert.ToString(row["Category"]);
                    ContainerDL.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    ContainerDL.InvoiceDate = Convert.ToString(row["Invoice Date"]);
                    ContainerDL.InvoiceDate1 = Convert.ToString(Convert.ToDateTime(row["InvoiceDate"]).ToString("dd MMM yy"));
                    ContainerDL.TallyName = Convert.ToString(row["Tally Name"]);
                    ContainerDL.GSTNo = Convert.ToString(row["GST No"]);
                    ContainerDL.GSTName = Convert.ToString(row["GSTName"]);
                    ContainerDL.State = Convert.ToString(row["State"]);
                    ContainerDL.CGST = Convert.ToDouble(row["CGST"]);
                    ContainerDL.IGST = Convert.ToDouble(row["IGST"]);
                    ContainerDL.TotalGST = Convert.ToDouble(row["Total GST"]);
                    ContainerDL.GrandTotal = Convert.ToDouble(row["Grand Total"]);
                    ContainerDL.WorkYear = Convert.ToString(row["workyear"]);




                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.CategorywiseInvoice> getCategorywiseInvoiceEDI(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getCategorywiseInvoiceEDI(Date, Category);
            List<EN.CategorywiseInvoice> ContainerList = new List<EN.CategorywiseInvoice>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.CategorywiseInvoice ContainerDL = new EN.CategorywiseInvoice();

                    ContainerDL.Select = Convert.ToInt32(row["Select"]);
                    ContainerDL.Category = Convert.ToString(row["Category"]);
                    ContainerDL.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    ContainerDL.InvoiceDate = Convert.ToString(row["Invoice Date"]);
                    ContainerDL.InvoiceDate1 = Convert.ToString(Convert.ToDateTime(row["InvoiceDate"]).ToString("dd MMM yy"));
                    ContainerDL.TallyName = Convert.ToString(row["Tally Name"]);
                    ContainerDL.GSTNo = Convert.ToString(row["GST No"]);
                    ContainerDL.GSTName = Convert.ToString(row["GSTName"]);
                    ContainerDL.State = Convert.ToString(row["State"]);
                    ContainerDL.CGST = Convert.ToDouble(row["CGST"]);
                    ContainerDL.IGST = Convert.ToDouble(row["IGST"]);
                    ContainerDL.TotalGST = Convert.ToDouble(row["Total GST"]);
                    ContainerDL.GrandTotal = Convert.ToDouble(row["Grand Total"]);
                    ContainerDL.WorkYear = Convert.ToString(row["workyear"]);




                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.CategorywiseInvoice> getCategorywiseYardInvoice(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getCategorywiseYardInvoice(Date, Category);
            List<EN.CategorywiseInvoice> ContainerList = new List<EN.CategorywiseInvoice>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.CategorywiseInvoice ContainerDL = new EN.CategorywiseInvoice();

                    ContainerDL.Select = Convert.ToInt32(row["Select"]);
                    ContainerDL.Category = Convert.ToString(row["Category"]);
                    ContainerDL.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    ContainerDL.InvoiceDate = Convert.ToString(row["Invoice Date"]);
                    ContainerDL.InvoiceDate1 = Convert.ToString(Convert.ToDateTime(row["InvoiceDate"]).ToString("dd MMM yy"));
                    ContainerDL.TallyName = Convert.ToString(row["Tally Name"]);
                    ContainerDL.GSTNo = Convert.ToString(row["GST No"]);
                    ContainerDL.GSTName = Convert.ToString(row["GSTName"]);
                    ContainerDL.State = Convert.ToString(row["State"]);
                    ContainerDL.CGST = Convert.ToDouble(row["CGST"]);
                    ContainerDL.IGST = Convert.ToDouble(row["IGST"]);
                    ContainerDL.TotalGST = Convert.ToDouble(row["Total GST"]);
                    ContainerDL.GrandTotal = Convert.ToDouble(row["Grand Total"]);
                    ContainerDL.WorkYear = Convert.ToString(row["workyear"]);




                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.ImportReceipt> getCategorywiseReceipt(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getCategorywiseReceipt(Date, Category);
            List<EN.ImportReceipt> ContainerList = new List<EN.ImportReceipt>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.ImportReceipt ContainerDL = new EN.ImportReceipt();

                    ContainerDL.Select = Convert.ToInt32(row["Select"]);
                    ContainerDL.Category = Convert.ToString(row["Category"]);
                    ContainerDL.ReceiptNo = Convert.ToString(row["ReceiptNo"]);
                    ContainerDL.ReceiptDate = Convert.ToString(Convert.ToDateTime(row["ReceiptDate"]).ToString("dd MMM yy"));
                    ContainerDL.ReceivedAmount = Convert.ToDouble(row["Receivedamt"]);

                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.ImportReceipt> FetchCategorywiseEDI(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.FetchCategorywiseEDI(Date, Category);
            List<EN.ImportReceipt> ContainerList = new List<EN.ImportReceipt>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.ImportReceipt ContainerDL = new EN.ImportReceipt();

                    ContainerDL.Select = Convert.ToInt32(row["Select"]);
                    ContainerDL.Category = Convert.ToString(row["Category"]);
                    ContainerDL.ReceiptNo = Convert.ToString(row["ReceiptNo"]);
                    ContainerDL.ReceiptDate = Convert.ToString(Convert.ToDateTime(row["ReceiptDate"]).ToString("dd MMM yy"));
                    ContainerDL.ReceivedAmount = Convert.ToDouble(row["Receivedamt"]);

                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.ImportReceipt> getJVReceipt(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getJVDBReceipt(Date, Category);
            List<EN.ImportReceipt> ContainerList = new List<EN.ImportReceipt>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.ImportReceipt ContainerDL = new EN.ImportReceipt();

                    ContainerDL.Select = Convert.ToInt32(row["Select"]);
                    ContainerDL.Category = Convert.ToString(row["Category"]);
                    ContainerDL.ReceiptNo = Convert.ToString(row["ReceiptNo"]);
                    ContainerDL.ReceiptDate = Convert.ToString(Convert.ToDateTime(row["ReceiptDate"]).ToString("dd MMM yy"));
                    ContainerDL.ReceivedAmount = Convert.ToDouble(row["Receivedamt"]);

                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.ImportReceipt> getCategorywiseYardReceipt(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getCategorywiseYardReceipt(Date, Category);
            List<EN.ImportReceipt> ContainerList = new List<EN.ImportReceipt>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.ImportReceipt ContainerDL = new EN.ImportReceipt();

                    ContainerDL.Select = Convert.ToInt32(row["Select"]);
                    ContainerDL.Category = Convert.ToString(row["Category"]);
                    ContainerDL.ReceiptNo = Convert.ToString(row["ReceiptNo"]);
                    ContainerDL.ReceiptDate = Convert.ToString(Convert.ToDateTime(row["ReceiptDate"]).ToString("dd MMM yy"));
                    ContainerDL.ReceivedAmount = Convert.ToDouble(row["Receivedamt"]);

                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.ImportReceipt> GetExporttoExcelDataReceipt(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.GetExporttoExcelDataReceipt(Date, Category);
            List<EN.ImportReceipt> ContainerList = new List<EN.ImportReceipt>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.ImportReceipt ContainerDL = new EN.ImportReceipt();

                    ContainerDL.ReceiptDate = Convert.ToString(row["DATE_"]);
                    ContainerDL.VoucherName = Convert.ToString(row["VCHTYPENAME"]);
                    ContainerDL.VCNNo = Convert.ToString(row["VCHNO"]);
                    ContainerDL.BillNo = Convert.ToString(row["BILL_NO"]);
                    ContainerDL.REF = Convert.ToString(row["REF"]);
                    ContainerDL.LedgerName = Convert.ToString(row["LEDGERNAME"]);
                    ContainerDL.DRCR = Convert.ToString(row["DR/CR"]);
                    ContainerDL.ReceivedAmount = Convert.ToDouble(row["AMOUNT"]);
                    ContainerDL.Narration = Convert.ToString(row["NARRATION"]);
                    ContainerDL.FavouringName = Convert.ToString(row["FAVOURING_NAME"]);
                    ContainerDL.TransType = Convert.ToString(row["TRANS_TYPE"]);
                    ContainerDL.InstNo = Convert.ToString(row["INST_NUMBER"]);
                    ContainerDL.InstDate = Convert.ToString(row["INST_DATE"]);
                    ContainerDL.BankName = Convert.ToString(row["BANK_NAME"]);
                    ContainerDL.Branch = Convert.ToString(row["BRANCH"]);

                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.CategorywiseInvoice> GetExporttoExcelData(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.GetExporttoExcelData(Date, Category);
            List<EN.CategorywiseInvoice> ContainerList = new List<EN.CategorywiseInvoice>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.CategorywiseInvoice ContainerDL = new EN.CategorywiseInvoice();

                    ContainerDL.V_INVOICE_DATE = Convert.ToString(row["V_INVOICE_DATE"]);
                    ContainerDL.VOUCHER_TYPE = Convert.ToString(row["VOUCHER_TYPE"]);
                    ContainerDL.REF_T = Convert.ToString(row["REF_T"]);
                    ContainerDL.LEDERNAME = Convert.ToString(row["LEDERNAME"]);
                    ContainerDL.DR_CR = Convert.ToString(row["DR_CR"]);
                    ContainerDL.Amount = Convert.ToString(row["AMOUNT"]);





                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public string LockInvoiceData(DataTable Invoicedata, String Category)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Category", Category);


            int i = db.AddTypeTableData("USP_TallySynchronizationLockInvoice", parameterList, Invoicedata, "PT_InvoiceLock", "@PT_InvoiceLock");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public string LockInvoiceDataEDI(DataTable Invoicedata, String Category)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            //parameterList.Add("EDI", Category);


            int i = db.AddTypeTableData("USP_TallySynchronizationLockInvoiceEDI", parameterList, Invoicedata, "PT_InvoiceLock", "@PT_InvoiceLock");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public string LockInvoiceYardData(DataTable Invoicedata, String Category)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Category", Category);


            int i = db.AddTypeTableData("USP_TallySynchronizationLockYardInvoice", parameterList, Invoicedata, "PT_InvoiceLock", "@PT_InvoiceLock");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public string LockReceiptData(DataTable Receiptdata, String Category)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Category", Category);


            int i = db.AddTypeTableData("USP_TallySynchronizationLockReceipt", parameterList, Receiptdata, "PT_ReceiptLock", "@PT_ReceiptLock");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public string LockEDIData(DataTable Receiptdata, String Category)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Category", Category);


            int i = db.AddTypeTableData("USP_TallySynchronizationLockEDI", parameterList, Receiptdata, "PT_ReceiptLock", "@PT_ReceiptLock");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public string LockReceiptYardData(DataTable Receiptdata, String Category)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Category", Category);


            int i = db.AddTypeTableData("USP_TallySynchronizationLockReceiptYard", parameterList, Receiptdata, "PT_ReceiptLock", "@PT_ReceiptLock");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public List<EN.CategorywiseCreditNote> GetExporttoExcelDataCreditNote(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.GetExporttoExcelDataCreditNote(Date, Category);
            List<EN.CategorywiseCreditNote> ContainerList = new List<EN.CategorywiseCreditNote>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.CategorywiseCreditNote ContainerDL = new EN.CategorywiseCreditNote();

                    ContainerDL.V_INVOICE_DATE = Convert.ToString(row["V_INVOICE_DATE"]);
                    ContainerDL.VOUCHER_TYPE = Convert.ToString(row["VOUCHER_TYPE"]);
                    ContainerDL.REF_T = Convert.ToString(row["REF_T"]);
                    ContainerDL.LEDERNAME = Convert.ToString(row["LEDERNAME"]);
                    ContainerDL.DR_CR = Convert.ToString(row["DR_CR"]);
                    ContainerDL.AMOUNT = Convert.ToString(row["AMOUNT"]);





                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.CategorywiseCreditNote> getCategorywiseCreditNote1(string Date, string Category)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getCategorywiseCreditNote(Date, Category);
            List<EN.CategorywiseCreditNote> ContainerList = new List<EN.CategorywiseCreditNote>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.CategorywiseCreditNote ContainerDL = new EN.CategorywiseCreditNote();

                    ContainerDL.Select = Convert.ToInt32(row["Select"]);
                    ContainerDL.Category = Convert.ToString(row["Category"]);
                    ContainerDL.InvoiceNo = Convert.ToString(row["Credit Note No"]);
                    ContainerDL.InvoiceDate = Convert.ToString(row["Credit Note Date"]);
                    ContainerDL.TallyName = Convert.ToString(row["Tally Name"]);
                    ContainerDL.GSTNo = Convert.ToString(row["GST No"]);
                    ContainerDL.GSTName = Convert.ToString(row["GSTName"]);
                    ContainerDL.State = Convert.ToString(row["State"]);
                    ContainerDL.CGST = Convert.ToDouble(row["CGST"]);
                    ContainerDL.SGST = Convert.ToDouble(row["SGST"]);
                    ContainerDL.IGST = Convert.ToDouble(row["IGST"]);
                    ContainerDL.TotalGST = Convert.ToDouble(row["Total GST"]);
                    ContainerDL.GrandTotal = Convert.ToDouble(row["Grand Total"]);
                    ContainerDL.WorkYear = Convert.ToString(row["workyear"]);




                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public string LockCreditNoteData(DataTable Invoicedata, String Category)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Category", Category);


            int i = db.AddTypeTableData("USP_TallySynchronizationLockCreditNote", parameterList, Invoicedata, "PT_creditNoteLock", "@PT_creditNoteLock");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }
        // COde By Prashant
        public List<EN.CategorywiseInvoice> getPocketCategorywiseInvoice(string Date, string Category, string FromDate, string BasedOn)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.DocketgetCategorywiseInvoice(Date, Category, FromDate, BasedOn);
            List<EN.CategorywiseInvoice> ContainerList = new List<EN.CategorywiseInvoice>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.CategorywiseInvoice ContainerDL = new EN.CategorywiseInvoice();

                    ContainerDL.Select = Convert.ToInt32(row["Select"]);
                    ContainerDL.Category = Convert.ToString(row["Category"]);
                    ContainerDL.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    ContainerDL.InvoiceDate1 = Convert.ToString(Convert.ToDateTime(row["InvoiceDate"]).ToString("dd MMM yy"));

                    ContainerDL.GSTNo = Convert.ToString(row["GST No"]);
                    ContainerDL.GSTName = Convert.ToString(row["GSTName"]);
                    ContainerDL.State = Convert.ToString(row["State"]);
                    ContainerDL.CGST = Convert.ToDouble(row["CGST"]);
                    ContainerDL.IGST = Convert.ToDouble(row["IGST"]);
                    ContainerDL.TotalGST = Convert.ToDouble(row["Total GST"]);
                    ContainerDL.GrandTotal = Convert.ToDouble(row["Grand Total"]);
                    ContainerDL.WorkYear = Convert.ToString(row["workyear"]);
                    ContainerDL.DocketNo = Convert.ToString(row["DocketNo"]);
                    ContainerDL.DocketDate = Convert.ToString(row["DocketDate"]);
                    ContainerDL.CourierAddress = Convert.ToString(row["CourierLocid"]);
                    ContainerDL.ChaName = Convert.ToString(row["chaname"]);
                    ContainerDL.LRno = Convert.ToString(row["LRNO"]);

                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        


        // Code End By Prashant

        public string LockInvoiceDocketData(DataTable Invoicedata, String Category)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Category", Category);


            int i = db.AddTypeTableData("USP_TallySynchronizationInvoiceDocket", parameterList, Invoicedata, "InvoiceDocketType", "@InvoiceDocketType");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }


      

        public List<EN.CategorywiseInvoice> getLRWIseContainere(string Lrno)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.GetLrWiseContainerno(Lrno);
            List<EN.CategorywiseInvoice> ContainerList = new List<EN.CategorywiseInvoice>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.CategorywiseInvoice ContainerDL = new EN.CategorywiseInvoice();


                    ContainerDL.LRno = Convert.ToString(row["Containerno"]);




                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }


        public List<EN.CategorywiseInvoice> getCouriorAddress(int couriorid)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.GetCouriorAddress(couriorid);
            List<EN.CategorywiseInvoice> ContainerList = new List<EN.CategorywiseInvoice>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.CategorywiseInvoice ContainerDL = new EN.CategorywiseInvoice();

                    ContainerDL.CourierAddress = Convert.ToString(row["courieraddress"]);





                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<EN.KDM> GetKDM()
        {
            List<EN.KDM> GetList = new List<EN.KDM>();
            DataTable GetDL = new DataTable();
            string Table = "KDMs";
            string Column = "KDMID,KDM";
            string Condition = "";
            string OrderBy = "KDM";
            GetDL = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);

            if (GetDL.Rows.Count > 0)
            {
                foreach (DataRow row in GetDL.Rows)
                {
                    EN.KDM GetListDL = new EN.KDM();
                    GetListDL.Kdmid = Convert.ToInt32(row["KDMID"]);
                    GetListDL.KdmName = Convert.ToString(row["KDM"]);
                    GetList.Add(GetListDL);
                }
            }



            return GetList;
        }
        public List<EN.SalesPersonM> GetSalesPersonM()
        {
            List<EN.SalesPersonM> sList = new List<EN.SalesPersonM>();
            DataTable GetDL = new DataTable();
            string Table = "SalesPersonM";
            string Column = "SalesPerson_ID1,SalesPerson_Name";
            string Condition = "";
            string OrderBy = "SalesPerson_Name";
            GetDL = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);

            if (GetDL.Rows.Count > 0)
            {
                foreach (DataRow row in GetDL.Rows)
                {
                    EN.SalesPersonM GetListDL = new EN.SalesPersonM();
                    GetListDL.SalesPerson_ID1 = Convert.ToInt32(row["SalesPerson_ID1"]);
                    GetListDL.SalesPerson_Name = Convert.ToString(row["SalesPerson_Name"]);
                    sList.Add(GetListDL);
                }
            }



            return sList;
        }

        public List<EN.SalesPersonM> Getgrade()
        {
            List<EN.SalesPersonM> GetList = new List<EN.SalesPersonM>();
            DataTable GetDL = new DataTable();
            string Table = "Grade_M";
            string Column = "Gradeid,Grade";
            string Condition = "";
            string OrderBy = "Grade";
            GetDL = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);

            if (GetDL.Rows.Count > 0)
            {
                foreach (DataRow row in GetDL.Rows)
                {
                    EN.SalesPersonM GetListDL = new EN.SalesPersonM();
                    GetListDL.gradeid = Convert.ToInt32(row["Gradeid"]);
                    GetListDL.gradename = Convert.ToString(row["Grade"]);
                    GetList.Add(GetListDL);
                }
            }



            return GetList;
        }


        public DataTable GetLedgerXMLdetails()
        {
            DataTable LedgerDT = new DataTable();
            LedgerDT = trackerdatamanager.GetledgerDetailsForXML();
            return LedgerDT;
        }
        public DataTable GetInvoiceXMLdetails(DataTable table)
        {
            DataTable LedgerDT = new DataTable();
            LedgerDT = trackerdatamanager.GetInvoicesDetailsForXML(table);
            return LedgerDT;
        }

        public DataTable GetCreditFileDetails(String category)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            DataTable Getdetails = new DataTable();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            //Getdetails = db.DataTableAddTypeTable("USP_XML_CREDITNOTE", parameterList, Receiptdata, "PT_XMLReceiptDownload", "@PT_XMLReceiptDownload");
            Getdetails = db.sub_GetDatatable("USP_GENERATE_XML_Credit '" + category + "'");
            return Getdetails;
        }

        public DataTable GetXmlFileDetails(string CategoryName)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            DataTable Getdetails = new DataTable();

            //Dictionary<object, object> parameterList = new Dictionary<object, object>();
            //parameterList.Add("Category", CategoryName);
            //Getdetails = db.DataTableAddTypeTable("USP_GENERATE_XML_Receipt", parameterList);



            Getdetails = db.sub_GetDatatable("USP_GENERATE_XML_Receipt '" + CategoryName + "'");
           
            return Getdetails;
        }

        public List<EN.LedgerDetails> GetLedgerDetails()
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.GetLedgerDetails();
            List<EN.LedgerDetails> ContainerList = new List<EN.LedgerDetails>();
            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.LedgerDetails ContainerDL = new EN.LedgerDetails();
                    ContainerDL.LedgerID = Convert.ToString(row["LedgerID"]);
                    ContainerDL.GSTName = Convert.ToString(row["Ledger Name"]);
                    ContainerDL.State = Convert.ToString(row["State"]);
                    ContainerDL.GSTIn_uniqID = Convert.ToString(row["GST"]);
                    ContainerDL.GSTAddress = Convert.ToString(row["Address"]);
                    ContainerDL.Panno = Convert.ToString(row["Pan No"]);
                    ContainerDL.PINCODE = Convert.ToString(row["PINCODE"]);
                    ContainerList.Add(ContainerDL);
                }
            }
            return ContainerList;
        }
        // Code End By Prashant

        public EN.ResponseMessage SaveReleaseRequest(EN.ReleaseRequest data, int userid)
        {
            EN.ResponseMessage result = new EN.ResponseMessage();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.SaveReleaseRequest(data, userid);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result.Message = Convert.ToString(row["message"]);
                    result.Status = Convert.ToInt32(row["Status"]);
                }
            }

            return result;
        }

        public List<EN.ReleaseRequest> GetReleaseRequestSummary(string AsOn)
        {
            List<EN.ReleaseRequest> List = new List<EN.ReleaseRequest>();
            DataTable table = new DataTable();
            table = trackerdatamanager.GetReleaseRequestSummary(AsOn);

            if (table != null)
            {
                int SrNo = 0;
                foreach (DataRow row in table.Rows)
                {
                    SrNo++;
                    EN.ReleaseRequest data = new EN.ReleaseRequest();
                    data.SrNo = SrNo;
                    data.CustomerName = Convert.ToString(row["Name"]);
                    data.RequestedBy = Convert.ToString(row["UserName"]);
                    data.RequestOn = Convert.ToString(row["RequestOn"]);
                    data.IsApproved = Convert.ToInt32(row["IsApproved"]);
                    data.IsDecline = Convert.ToInt32(row["IsDeclined"]);
                    data.DaysFor = Convert.ToInt32(row["DaysFor"]);
                    data.CRLimitID = Convert.ToInt32(row["CRLimitID"]);
                    List.Add(data);
                }
            }
            return List;
        }


        public EN.ResponseMessage ApproveReleaseRequest(int limitDays, long cRID, int userid, DateTime AddedOn)
        {
            EN.ResponseMessage result = new EN.ResponseMessage();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.ApproveReleaseRequest(limitDays, cRID, userid, AddedOn);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result.Message = Convert.ToString(row["message"]);
                    result.Status = Convert.ToInt32(row["Status"]);
                }
            }

            return result;
        }

        public EN.ResponseMessage DeclineReleaseRequest(int limitDays, long cRID, int userid, DateTime AddedOn)
        {
            EN.ResponseMessage result = new EN.ResponseMessage();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.DeclineReleaseRequest(limitDays, cRID, userid, AddedOn);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result.Message = Convert.ToString(row["message"]);
                    result.Status = Convert.ToInt32(row["Status"]);
                }
            }

            return result;
        }


        public List<EN.ReleaseRequest> GetreleaseRequestSatus(string asOn)
        {
            List<EN.ReleaseRequest> List = new List<EN.ReleaseRequest>();
            DataTable table = new DataTable();
            table = trackerdatamanager.GetreleaseRequestSatus(asOn);

            if (table != null)
            {
                int SrNo = 0;
                foreach (DataRow row in table.Rows)
                {
                    SrNo++;
                    EN.ReleaseRequest data = new EN.ReleaseRequest();
                    data.SrNo = SrNo;
                    data.CustomerName = Convert.ToString(row["Name"]);
                    data.RequestedBy = Convert.ToString(row["UserName"]);
                    data.RequestOn = Convert.ToString(row["RequestOn"]);
                    data.IsApproved = Convert.ToInt32(row["IsApproved"]);
                    data.IsDecline = Convert.ToInt32(row["IsDeclined"]);
                    data.DaysFor = Convert.ToInt32(row["DaysFor"]);
                    data.CRLimitID = Convert.ToInt32(row["CRLimitID"]);
                    List.Add(data);
                }
            }
            return List;
        }

    }
}
