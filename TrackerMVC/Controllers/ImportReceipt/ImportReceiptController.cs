using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using DP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrailerTransport;
using train = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrainMaster;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using RS = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ReceiptAdjustment;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using HC = TrackerMVCDataLayer.Helper;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;

namespace TrackerMVC.Controllers.ImportReceipt
{
    [UserAuthenticationFilter]
    public class ImportReceiptController : Controller
    {
        BM.IGM.IGM IG = new BM.IGM.IGM();
        BM.BLDataManager.GenerateBL BL = new BM.BLDataManager.GenerateBL();
        RS.ReceiptAdjustment trainTrackerProvider = new RS.ReceiptAdjustment();
        // GET: ImportReceipt
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getAutoCustomerList(string prefixText, string CustomerType)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = BL.getAutoCustomer(prefixText, CustomerType);

            return Json(Customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getPartyNameReceipt(string prefixText, string Mode)
            {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GetPartyNameReceipt '" + Mode + "','" + prefixText + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.Customer customer = new BE.Customer();
                    customer.AGID = Convert.ToInt32(row["GSTID"]);
                    customer.AGName = Convert.ToString(row["GSTName"]);
                    Customerlst.Add(customer);
                }
            }

            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult CreditReceipt()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            if (ModelState.IsValid)
            {
                List<BE.Customer> Customer = new List<BE.Customer>();
                List<BE.PaymentModes> PaymentMode = new List<BE.PaymentModes>();
                List<BE.ImportBank> BankName = new List<BE.ImportBank>();
                List<BE.TDSPerM> TDSPer = new List<BE.TDSPerM>();

                Customer = BL.getParty();
                PaymentMode = BL.getPaymentModes();
                BankName = BL.getImportBank();
                TDSPer = BL.getTDSPerM();

                ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
                ViewBag.PaymentMode = new SelectList(PaymentMode, "PaymentId", "PaymentMode");
                ViewBag.BankName = new SelectList(BankName, "BankId", "BankName");
                ViewBag.TDSPerM = new SelectList(TDSPer, "TDSPerId", "Percentage");


            }
            //ModelState.Clear();
            //Session["PaymentSubDet"] = null;

            return View();
        }

        //[HttpPost]
        //public JsonResult getPartyNameReceipt(string  Mode)
        //{
        //    HC.DBOperations db = new HC.DBOperations();
        //    DataTable dataTable = new DataTable();
        //    List<BE.Customer> Customerlst = new List<BE.Customer>();
        //    dataTable =db.sub_GetDatatable("USP_GetPartyNameReceipt '" + Mode + "'");

        //    if(dataTable != null)
        //    {
        //        foreach(DataRow row in dataTable.Rows)
        //        {
        //            BE.Customer customer = new BE.Customer();
        //            customer.AGID =Convert.ToInt32(row["GSTID"]);
        //            customer.AGName = Convert.ToString(row["GSTName"]);
        //            Customerlst.Add(customer);
        //        }
        //    }

        //    var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;

        //    return jsonResult;
        //}
         
        public ActionResult ImportReceipt()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            if (ModelState.IsValid)
            {
                List<BE.Customer> Customer = new List<BE.Customer>();
                List<BE.PaymentModes> PaymentMode = new List<BE.PaymentModes>();
                List<BE.ImportBank> BankName = new List<BE.ImportBank>();
                List<BE.TDSPerM> TDSPer = new List<BE.TDSPerM>();
                List<BE.CommonAccountDets> commonAccountDets = new List<BE.CommonAccountDets>();
                List<BE.WorkyearReceipt> WorkyearReceipt = new List<BE.WorkyearReceipt>();
                Customer = BL.getParty();
                PaymentMode = BL.getPaymentModes();
                BankName = BL.getImportBank();
                TDSPer = BL.getTDSPerM();
                commonAccountDets = BL.getCommonAccountDet();
                WorkyearReceipt = BL.getWorkyear();
                ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
                ViewBag.PaymentMode = new SelectList(PaymentMode, "PaymentId", "PaymentMode");
                ViewBag.BankName = new SelectList(BankName, "BankId", "BankName");
                ViewBag.TDSPerM = new SelectList(TDSPer, "TDSPerId", "Percentage");
                ViewBag.AccountDetails = new SelectList(commonAccountDets, "ConAccId", "AccountNo");
                ViewBag.Workyear = new SelectList(WorkyearReceipt, "WorkyearTDS", "WorkyearTDS");


            }
            //ModelState.Clear();
            //Session["PaymentSubDet"] = null;

            return View();
        }

        [HttpPost]
        public ActionResult SaveReceiptForm(BE.CategorywiseInvoice viewModel, List<BE.CategorywiseInvoice> invoiceData, string PayFrom, List<PaymentDet> paymentData, string Category, string TDSPerc, string ReceiptType, string Remarks, string PayParty , string ADVReceiptNo, string BillParty, string Search, string TDSWorkyear)
        {
            string message = "";
            int ReceiptNo = 0;
            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            if (userId == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ADVReceiptNo == "")
            {
                ADVReceiptNo = "0";
            }

            if (PayParty == "")
            {
                PayParty = "0";
            }

            DataTable dtInvoiceDetails = new DataTable();
            DataTable dtPaymentDetails = new DataTable();

            dtInvoiceDetails.Columns.Add("AssessNo");
            dtInvoiceDetails.Columns.Add("AssessYear");
            dtInvoiceDetails.Columns.Add("ReceiptAmount");
            dtInvoiceDetails.Columns.Add("payfrom");
            dtInvoiceDetails.Columns.Add("payfromID");
            dtInvoiceDetails.Columns.Add("CHAID");
            dtInvoiceDetails.Columns.Add("IsPDAccount");
            dtInvoiceDetails.Columns.Add("AddedBy");
            dtInvoiceDetails.Columns.Add("TDS_Per");
            dtInvoiceDetails.Columns.Add("TDSAmount");
            dtPaymentDetails.Columns.Add("PaymentId");
            dtPaymentDetails.Columns.Add("Amount");
            dtPaymentDetails.Columns.Add("ModeNo");
            dtPaymentDetails.Columns.Add("BankId");
            dtPaymentDetails.Columns.Add("ModeDate");

            try
            {
                if (invoiceData.Count > 0)
                {
                    foreach (var item in invoiceData)
                    {
                        message = ValidateReceipt(Category, item.AssessNo, item.AssessYear);
                        if (message != "Success")
                        {
                            message = "-1";
                            break;
                        }
                        DataRow dr = dtInvoiceDetails.NewRow();
                        dr["AssessNo"] = item.AssessNo;
                        dr["AssessYear"] = item.AssessYear;
                        dr["ReceiptAmount"] = item.ReceivedAmount;
                        dr["payfrom"] = PayFrom;
                        dr["payfromID"] = Convert.ToInt64(PayParty);
                        dr["CHAID"] = viewModel.CHAID;
                        dr["IsPDAccount"] = 1;
                        dr["AddedBy"] = userId;
                        dr["TDS_Per"] = item.TDS;
                        dr["TDSAmount"] = 0;
                        dtInvoiceDetails.Rows.Add(dr);
                    }

                }
                else
                {
                    message = "Please Enter Invoice Details.";
                    return Json(message);
                }
            }
            catch (Exception ex) { }

            try
            {
                foreach (var item in paymentData)
                {
                    DataRow dr1 = dtPaymentDetails.NewRow();
                    dr1["PaymentId"] = item.PaymentId;
                    dr1["Amount"] = item.Amount;
                    dr1["ModeNo"] = item.ModeNo;
                    dr1["BankId"] = item.BankId;
                    dr1["ModeDate"] = item.ModeDate;
                    dtPaymentDetails.Rows.Add(dr1);
                }
            }
            catch (Exception ex) { }

            try
            {
                if (viewModel.AccountNoId == null)
                {
                    viewModel.AccountNoId = 0;
                }
            }
            catch (Exception ex) { viewModel.AccountNoId = 0; }

            if (message == "" || message == "Success")
            {
                viewModel.BillParty = Convert.ToInt64(PayParty);
                ReceiptNo = BL.AddInvoiceData(viewModel, dtInvoiceDetails, dtPaymentDetails, userId, Category, TDSPerc, ReceiptType, Remarks, PayFrom, ADVReceiptNo, Search, TDSWorkyear);
            }
            else if (message == "-1")
            {
                ReceiptNo = -1;
            }

            return Json(ReceiptNo);
        }

        [HttpPost]
        public JsonResult SaveCreditReceiptForm(List<BE.CategorywiseInvoice> invoiceData, string PayFrom, string Category, string ReceiptType, string Remarks,string PendingPendingChequeRTGS)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            if (userId == 0)
            {
                message = "Session Expired. Please Try Again";
            }

            Dictionary<object, object> output = null;

            DataTable dtInvoiceDetails = new DataTable();

            dtInvoiceDetails.Columns.Add("AssessNo");
            dtInvoiceDetails.Columns.Add("AssessYear");
            dtInvoiceDetails.Columns.Add("ReceiptAmount");
            dtInvoiceDetails.Columns.Add("payfrom");
            dtInvoiceDetails.Columns.Add("payfromID");
            dtInvoiceDetails.Columns.Add("CHAID");
            dtInvoiceDetails.Columns.Add("IsPDAccount");
            dtInvoiceDetails.Columns.Add("AddedBy");
            dtInvoiceDetails.Columns.Add("TDS_Per");
            dtInvoiceDetails.Columns.Add("TDSAmount");
            if (invoiceData.Count > 0)
            {
                foreach (var item in invoiceData)
                {
                    DataRow dr = dtInvoiceDetails.NewRow();
                    dr["AssessNo"] = item.AssessNo;
                    dr["AssessYear"] = item.AssessYear;
                    dr["ReceiptAmount"] = 0;
                    dr["payfrom"] = PayFrom;
                    dr["payfromID"] =0;
                    dr["CHAID"] =0;
                    dr["IsPDAccount"] = 1;
                    dr["AddedBy"] = userId;
                    dr["TDS_Per"] = 0;

                    if (item.TDS == null)
                    {
                        item.TDS = 0;
                    }

                    dr["TDSAmount"] = item.TDS;
                    dtInvoiceDetails.Rows.Add(dr);
                }
            }

            if (message == "")
            {
                output = BL.AddCreditInvoiceData(dtInvoiceDetails, userId, Category, ReceiptType, Remarks, PendingPendingChequeRTGS);

                foreach (var item in output)
                {
                    if (item.Key.ToString() == "Message" && item.Value.ToString() != "")
                    {
                        message = "Getting Error :- " + item.Value.ToString();
                        break;
                    }
                    else
                    {
                        if (item.Key.ToString() == "TransNo")
                        {
                            message = "Data Save Successfully ~" + item.Value.ToString();
                        }
                    }
                }
            }

            return Json(message);
        }

        [HttpPost]
        public JsonResult DailyCollection()
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dsList = new DataTable();
         
            dsList = db.sub_GetDatatable("get_sp_today_Import_collection_new");

            var listData = JsonConvert.SerializeObject(dsList);

            var jsonResult = Json(listData, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult ImportReceiptPrint(string receiptNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblReceiptDetails = new DataTable();
            DataTable tblPaymentDetails = new DataTable();
            DataTable tblPaymentDetails2 = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetImportReceiptDet '" + receiptNo + "'" );

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblReceiptDetails = getJobOrderSet.Tables[1];
                tblInvoiceDetails = getJobOrderSet.Tables[2];
                tblPaymentDetails = getJobOrderSet.Tables[3];
                tblPaymentDetails2 = getJobOrderSet.Tables[4];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                }
                foreach (DataRow dr in tblReceiptDetails.Rows)
                {
                    ViewBag.ReceiptNo = dr["ReceiptNo"];
                    ViewBag.ReceiptDate = dr["ReceiptDate"];
                    ViewBag.IGMNo = dr["IGMNo"];
                    ViewBag.ItemNo = dr["ItemNo"];
                    ViewBag.PayFrom = dr["PayFrom"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.ImporterName = dr["ImporterName"];
                    ViewBag.Category = dr["Category"];
                }
                foreach (DataRow dr in tblPaymentDetails2.Rows)
                {
                    ViewBag.NetAmount = dr["Net Amount"];
                    ViewBag.TDSAmount = dr["TDS Amount"];
                    ViewBag.ReceivedAmount = dr["Receipt Amount"];
                    ViewBag.AmountInWord = dr["AmtInWord"]; ;
                    ViewBag.ReceiptRemarks = dr["Remarks"];
                    ViewBag.AccessAmount = dr["AccessAmount"];
                    ViewBag.GeneratedBy = dr["Generated By"];
                }

                foreach (DataRow dr1 in tblInvoiceDetails.Rows)
                {
                    if(dr1["Party Name"].ToString() != "")
                    {
                        ViewBag.BillParty = dr1["Party Name"];
                        break;
                    }
                }
            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.PaymentList = tblPaymentDetails.AsEnumerable();

            return PartialView();

        }

        [HttpPost]
        public ActionResult AdvanceReceiptPrint(string receiptNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblReceiptDetails = new DataTable();
            DataTable tblPaymentDetails = new DataTable();
            DataTable tblPaymentDetails2 = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetImportReceiptDet '" + receiptNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblReceiptDetails = getJobOrderSet.Tables[1];
                tblInvoiceDetails = getJobOrderSet.Tables[2];
                tblPaymentDetails = getJobOrderSet.Tables[3];
                tblPaymentDetails2 = getJobOrderSet.Tables[4];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                }
                foreach (DataRow dr in tblReceiptDetails.Rows)
                {
                    ViewBag.ReceiptNo = dr["ReceiptNo"];
                    ViewBag.ReceiptDate = dr["ReceiptDate"];
                    ViewBag.IGMNo = dr["IGMNo"];
                    ViewBag.ItemNo = dr["ItemNo"];
                    ViewBag.PayFrom = dr["PayFrom"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.ImporterName = dr["ImporterName"];
                    ViewBag.Category = dr["Category"];
                }
                foreach (DataRow dr in tblPaymentDetails2.Rows)
                {
                    ViewBag.NetAmount = dr["Net Amount"];
                    ViewBag.TDSAmount = dr["TDS Amount"];
                    ViewBag.ReceivedAmount = dr["Receipt Amount"];
                    ViewBag.AmountInWord = dr["AmtInWord"]; ;
                    ViewBag.ReceiptRemarks = dr["Remarks"];
                    ViewBag.AccessAmount = dr["AccessAmount"];
                    ViewBag.GeneratedBy = dr["Generated By"];
                }

                foreach (DataRow dr1 in tblInvoiceDetails.Rows)
                {
                    if (dr1["Party Name"].ToString() != "")
                    {
                        ViewBag.BillParty = dr1["Party Name"];
                        break;
                    }
                }
            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
           // ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.PaymentList = tblPaymentDetails.AsEnumerable();

            return PartialView();

        }

        [HttpPost]
        public ActionResult CreditReceiptPrint(string receiptNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblReceiptDetails = new DataTable();
            DataTable tblPaymentDetails = new DataTable();
            DataTable tblPaymentDetails2 = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetCreditReceiptDet '" + receiptNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblReceiptDetails = getJobOrderSet.Tables[1];
                tblInvoiceDetails = getJobOrderSet.Tables[2];
                tblPaymentDetails = getJobOrderSet.Tables[3];
                tblPaymentDetails2 = getJobOrderSet.Tables[4];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                }
                foreach (DataRow dr in tblReceiptDetails.Rows)
                {
                    ViewBag.ReceiptNo = dr["TransNo"];
                    ViewBag.ReceiptDate = dr["TransDate"];
                    //ViewBag.IGMNo = dr["IGMNo"];
                    //ViewBag.ItemNo = dr["ItemNo"];
                    ViewBag.PayFrom = dr["PayFrom"];
                    ViewBag.CHAName = dr["CHAName"];
                    //ViewBag.ImporterName = dr["ImporterName"];
                    ViewBag.Category = dr["Category"];
                }
                foreach (DataRow dr in tblPaymentDetails2.Rows)
                {
                    ViewBag.NetAmount = dr["Net Amount"];
                    ViewBag.TDSAmount = dr["TDS Amount"];
                    ViewBag.ReceivedAmount = dr["Receipt Amount"];
                    ViewBag.AmountInWord = dr["AmtInWord"]; ;
                    ViewBag.ReceiptRemarks = dr["Remarks"];
                    ViewBag.AccessAmount = dr["AccessAmount"];
                    ViewBag.GeneratedBy = dr["Generated By"];
                }

                foreach (DataRow dr1 in tblInvoiceDetails.Rows)
                {
                    if (dr1["Party Name"].ToString() != "")
                    {
                        ViewBag.BillParty = dr1["Party Name"];
                        break;
                    }
                }
            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.PaymentList = tblPaymentDetails.AsEnumerable();

            return PartialView();

        }

        [HttpPost]
        public JsonResult getPendingReceipt(string CustomerId)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ReceiptEntryEntities> receiptEntries = new List<BE.ReceiptEntryEntities>();
            dataTable = db.sub_GetDatatable("USP_IMP_AD_BAL_RECEIPT '" + CustomerId + "','" + "" + "','" + "" + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ReceiptEntryEntities receiptEntry = new BE.ReceiptEntryEntities();
                    receiptEntry.ReceiptNo = Convert.ToInt32(row["ReceiptNo"]);
                    receiptEntry.ReceiptRefNo = Convert.ToString(row["ReceiptRefNo"]);
                    receiptEntry.TotalAmount = Convert.ToDouble(row["receiptAmount"]);
                    receiptEntries.Add(receiptEntry);
                }
            }

            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetInvoiceList(string InvoiceType,string AgentId,string IGMNo,string ItemNo,string BLNo,string BOENo,string BondNo,string ShippingBillNo,string AssessmentNo, string TDSType)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            long CustId = 0;
            if (AgentId == "" || AgentId==null)
            {
                CustId = 0;
            }
            else{CustId =Convert.ToInt64(AgentId);}

            List<BE.CategorywiseInvoice> containerList = new List<BE.CategorywiseInvoice>();
            tblInvoiceList = db.sub_GetDatatable("SP_ImportPDADJUSTMENT_NEW '" + InvoiceType + "','" + AgentId + "','" + IGMNo + "','" + ItemNo + "','" + BLNo +"','" + BOENo + "','" + BondNo + "','" + ShippingBillNo + "','" + AssessmentNo + "','" + TDSType +"'");

            if (tblInvoiceList.Rows.Count != 0)
            {  
                foreach (DataRow row in tblInvoiceList.Rows)
                {
                    BE.CategorywiseInvoice rowdata = new BE.CategorywiseInvoice();
                    rowdata.AssessNo = Convert.ToString(row["Assess No"]);
                    rowdata.AssessYear = Convert.ToString(row["Assess Year"]);
                    rowdata.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    rowdata.InvoiceDate = Convert.ToString(row["Invoice Date"]);
                    rowdata.InvoiceAmount = Convert.ToDouble(row["Invoice Amt"]);
                    rowdata.CreditAmount = Convert.ToDouble(row["Credit Amt"]);
                    rowdata.PrevReceiptAmount = Convert.ToDouble(row["Prev Received Amt"]);
                    rowdata.ReceivalAmount = Convert.ToDouble(row["Receival Amt"]);
                    rowdata.ReceivedAmount = Convert.ToDouble(row["Received Amt"]);
                    rowdata.TDS = Convert.ToDouble(row["TDS"]);
                    rowdata.NetReceivedAmount = Convert.ToDouble(row["Net Received"]);
                    rowdata.OS = Convert.ToDouble(row["OS"]);
                    containerList.Add(rowdata);
                }
            }

            var jsonResult = Json(containerList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult getPendingInvoice(string hdnPayFromId, string ason, string Category,string partyfullname)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            DataSet dataSet = new DataSet();
            List<BE.ReceiptEntryEntities> receiptEntries = new List<BE.ReceiptEntryEntities>();

            dataSet = db.sub_GetDataSets("Usp_Peniding_Invoice '" + hdnPayFromId + "','" + ason + "','" + Category + "'");

      
            if (dataSet.Tables[0] != null)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    BE.ReceiptEntryEntities receiptEntry = new BE.ReceiptEntryEntities();
                    
                    receiptEntry.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    receiptEntry.NetAmount = Convert.ToString(row["OpeningAMt"]);
                    receiptEntry.TotalAmount = Convert.ToDouble(row["PendingAMt"]);
                    receiptEntries.Add(receiptEntry);
                }
            }

            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        [HttpPost]
        public JsonResult getPendingRect(string hdnPayFromId, string ason, string Category, string partyfullname)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ReceiptEntryEntities> receiptEntries = new List<BE.ReceiptEntryEntities>();
            DataSet dataSet = new DataSet();
    

            dataSet = db.sub_GetDataSets("Usp_Peniding_Invoice '" + hdnPayFromId + "','" + ason + "','" + Category + "'");


            if (dataSet.Tables[1] != null)
            
            {
                foreach (DataRow row in dataSet.Tables[1].Rows)
                {
                    BE.ReceiptEntryEntities receiptEntry = new BE.ReceiptEntryEntities();

                   
                    receiptEntry.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    receiptEntry.ReceiptRefNo = Convert.ToString(row["OpeningAMt"]);
                    receiptEntry.TotalAmount = Convert.ToDouble(row["PendingAMt"]);
                    receiptEntries.Add(receiptEntry);
                }
            }

            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        [HttpPost]
        public JsonResult GetCreditInvoiceList(string InvoiceType, string AdjustmentType, string InvoiceNo)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            long CustId = 0;

            List<BE.CategorywiseInvoice> containerList = new List<BE.CategorywiseInvoice>();
            tblInvoiceList = db.sub_GetDatatable("getCreditAdjustmentDet '" + InvoiceType + "','" + AdjustmentType + "','" + InvoiceNo + "'");

            if (tblInvoiceList.Rows.Count != 0)
            {
                foreach (DataRow row in tblInvoiceList.Rows)
                {
                    BE.CategorywiseInvoice rowdata = new BE.CategorywiseInvoice();
                    rowdata.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    rowdata.InvoiceDate = Convert.ToString(row["InvoiceDate"]);
                    rowdata.GSTName = Convert.ToString(row["PartyName"]);
                    rowdata.Amount = Convert.ToString(row["INVOICE AMOUNT"]);
                    containerList.Add(rowdata);
                }
            }

            var jsonResult = Json(containerList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetTempAddPaymentDet(string PaymentId,string Amount,string ModeNo,string BankId,string ModeDate, string ReceivedAmount)
        {
            double TotalReceiptAmt = 0;
            double TotalAmount = 0;
            string message = "";
            DataTable tdDisplay = new DataTable();
            DataTable dtExist = (DataTable)Session["PaymentSubDet"];
            var dtlPaymentList = "";

            if (ReceivedAmount=="" || ReceivedAmount==null)
            {
                TotalReceiptAmt = 0;
            }
            else
            {
                TotalReceiptAmt =Convert.ToDouble(ReceivedAmount);
            }

            if (Amount == "" || Amount == null)
            {
                TotalAmount = 0;
            }
            else
            {
                TotalAmount = Convert.ToDouble(Amount);
            }

            if(TotalAmount > TotalReceiptAmt)
            {
                message = "Payment Amount cannot be Greater then Receipt Amount.";
            }
            
            if(message == "")
            {
                if (dtExist != null)
                {
                    message=ValidateAmount(dtExist, TotalReceiptAmt,TotalAmount);
                    if (message == "")
                    {
                        DataRow dr = dtExist.NewRow();
                        dr["PaymentId"] = Convert.ToInt64(PaymentId);
                        dr["Amount"] = Convert.ToDouble(Amount);
                        dr["ModeNo"] = ModeNo;
                        dr["BankId"] = BankId;
                        dr["ModeDate"] = ModeDate;

                        dtExist.Rows.Add(dr);
                    }
                }
                else
                {

                    dtExist = new DataTable();
                    dtExist.Columns.Add("PaymentId", typeof(System.String));
                    dtExist.Columns.Add("Amount", typeof(System.Double));
                    dtExist.Columns.Add("ModeNo", typeof(System.String));
                    dtExist.Columns.Add("BankId", typeof(System.String));
                    dtExist.Columns.Add("ModeDate", typeof(System.String));

                    DataRow dr = dtExist.NewRow();
                    dr["PaymentId"] = Convert.ToInt64(PaymentId);
                    dr["Amount"] = Convert.ToDouble(Amount);
                    dr["ModeNo"] = ModeNo;
                    dr["BankId"] = BankId;
                    dr["ModeDate"] = ModeDate;
                    dtExist.Rows.Add(dr);
                }

                Session["PaymentSubDet"] = dtExist;


                List<BE.PaymentModes> paymentModes = new List<BE.PaymentModes>();
                List<BE.ImportBank> importBank = new List<BE.ImportBank>();
                paymentModes = BL.getPaymentModes();
                importBank = BL.getImportBank();

                foreach (DataRow item in dtExist.Rows)
                {
                    foreach (BE.PaymentModes t1 in paymentModes)
                    {
                        if (Convert.ToString(t1.PaymentId) == item["PaymentId"].ToString())
                        {
                            item["PaymentId"] = t1.PaymentMode;
                            break;
                        }
                    }

                    foreach (BE.ImportBank t2 in importBank)
                    {
                        if (Convert.ToString(t2.BankId) == item["BankId"].ToString())
                        {
                            item["BankId"] = t2.BankName;
                            break;
                        }
                    }
                }
            }

            dtlPaymentList = JsonConvert.SerializeObject(dtExist);

            var returnField = new { Issuedata = message, PaymentList = dtlPaymentList };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult getReceiptSummaryDet(string FromDate, string ToDate,string searchcerteria,string Searchtext)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("GetReceiptDet '" + FromDate + "','" + ToDate + "','"+ searchcerteria + "','"+ Searchtext + "'");
            if (tblInvoiceList.Rows.Count > 0)
            {
                InvoiceList = JsonConvert.SerializeObject(tblInvoiceList);
            }
            else
            {
                InvoiceList = "0";
            }
            Session["getReceiptDetailsSummary"] = tblInvoiceList;
            var jsonResult = Json(InvoiceList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [HttpPost]
        public JsonResult getCreditReceiptSummaryDet(string FromDate, string ToDate)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("GetCreditReceiptSummary '" + FromDate + "','" + ToDate + "'");
            if (tblInvoiceList.Rows.Count > 0)
            {
                InvoiceList = JsonConvert.SerializeObject(tblInvoiceList);
            }
            else
            {
                InvoiceList = "0";
            }

            return new JsonResult() { Data = InvoiceList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult cancelReceipt(string ReceiptNo,string FromDate,string ToDate)
        {
            ViewBag.ReceiptNo = ReceiptNo;
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;

            return PartialView();
        }

        [HttpPost]
        public JsonResult CancelReceiptWithReason(string ReceiptNo, string Remarks)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";

            tblInvoiceList = db.sub_GetDatatable("USP_CancelReceipt '" + ReceiptNo + "','" + userId + "','" + Remarks + "'");

            if (tblInvoiceList.Rows.Count > 0)
            {
                Message = tblInvoiceList.Rows[0]["ERRMESSAGE"].ToString();
            }
            else
            {
                Message = "No Data Found.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult getPenginInvoiceList()
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("GetPendingInvoiceList");
            if (tblInvoiceList.Rows.Count > 0)
            {
                Session["GetPendingInvoice"] = tblInvoiceList;
                InvoiceList = JsonConvert.SerializeObject(tblInvoiceList);
            }
            else
            {
                Session["GetPendingInvoice"] = null;
                InvoiceList = "0";
            }

            return new JsonResult() { Data = InvoiceList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ExportToExcelPendingInvoice()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ContainerArrival = (DataTable)Session["GetPendingInvoice"];
            GridView gridview1 = new GridView();
            gridview1.DataSource = ContainerArrival;
            gridview1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PendingInvoice.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Total Pending Invoice List<strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview1.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        string ValidateAmount(DataTable dtExist, double TotalReceiptAmt, double TotalAmount1)
        {
            string Message = "";
            if (dtExist.Rows.Count > 0)
            {
                double TotalAmount = 0;
                foreach (DataRow dr in dtExist.Rows)
                {
                    TotalAmount = TotalAmount + Convert.ToDouble(dr["Amount"]);
                }

                TotalAmount = TotalAmount + TotalAmount1;

                if (TotalAmount > TotalReceiptAmt)
                {
                    Message = "Payment Amount cannot be Greater then Receipt Amount.";
                }
            }
            return Message;
        }

        string ValidateReceipt(string Category,string AssessNo,string AssessYear)
        {
            string Message = "";
            HC.DBOperations db = new HC.DBOperations();
            DataTable dtValidate = new DataTable();
            try
            {
                dtValidate = db.sub_GetDatatable("ValidateReceipt '" + AssessNo + "','" + AssessYear + "','" + Category + "'");
                if(dtValidate != null)
                {
                    foreach(DataRow row in dtValidate.Rows)
                    {
                        Message = row["Message"].ToString();
                    }
                }
            }
            catch (Exception ex) { Message = ex.Message; }

            return Message;
        }

        public ActionResult LedgerStatement()
        {

            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = BL.getParty();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
             
            List<BE.Category> Category = new List<BE.Category>();
            Category = BL.getCategory();
            ViewBag.Category = new SelectList(Category, "ID", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult GetLedgerstatement(string partyname, string Fromdate, string todate, string partyfullname,string Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_CUST_OutStanding_Statement '" + partyname + "','" + Fromdate + "','" + todate + "','" + Category + "'");
            Session["GetOutstandingLedgerstateentaccount"] = dt;

            Session["PartyName"] = partyfullname;

            Session["FromDate"] = Fromdate;
            Session["ToDate"] = todate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public ActionResult ExportToExcelOnLedgerstatement()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["GetOutstandingLedgerstateentaccount"];
            string Tittle = "Party Name " + Session["PartyName"] + " FromDate " + Session["FromDate"] + "ToDate " + Session["ToDate"] + "";

            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Ledger OutStanding Statement Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Ledger OutStanding Statement Report<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }



        public ActionResult OutStandingStatement()
        {

            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = BL.getParty();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");

            List<BE.Category> Category = new List<BE.Category>();
            Category = BL.getCategory();
            ViewBag.Category = new SelectList(Category, "ID", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult GetOutStandingStatement(string hdnPayFromId, string ason, string partyfullname, string Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            if (hdnPayFromId == "")
            {
                dt = db.sub_GetDatatable("USP_OUTStanding_Statement_ALL '" + hdnPayFromId + "','" + ason + "','" + Category + "'");
                Session["PartyName"] = "All";


            }
            else
            {
                dt = db.sub_GetDatatable("USP_OUTStanding_Statement '" + hdnPayFromId + "','" + ason + "'");
                Session["PartyName"] = partyfullname;


            }
            Session["GetOtustandingreportList"] = dt;
            Session["PartyName"] = partyfullname;
            Session["ason"] = ason;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult GetOutStandingStatement1(string hdnPayFromId, string ason, string Category,string partyfullname)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
           
                dt = db.sub_GetDatatable("USP_OUTStanding_Statement_ALL '" + hdnPayFromId + "','" + ason + "','" + Category + "'");
            
            Session["GetOtustandingreportList"] = dt;
            Session["PartyName"] = partyfullname;
            Session["ason"] = ason;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public ActionResult ExportToExcelOutStandingStatement()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["GetOtustandingreportList"];
            string Tittle = "Party Name " + Session["PartyName"] + " As On " + Session["ason"] + "";

            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Ledger OutStanding Statement Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Ledger OutStanding Statement Report<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        public ActionResult MapCustomerWiseAutomailInvoice()
        {

            List<BE.ShipLines> ShipLines = new List<BE.ShipLines>();
            List<BE.exporterShipping> ExporterShipLines = new List<BE.exporterShipping>();
            List<BE.Customer> Customer = new List<BE.Customer>();
            List<BE.CHA> CHA = new List<BE.CHA>();
            List<BE.PartyNameEntities> Partyname = new List<BE.PartyNameEntities>();

            ShipLines = BL.getShipLines();
            Customer = BL.getCustomer();
            CHA = BL.getCHA();
            Partyname = BL.GetGstPartyName();
            ExporterShipLines = BL.GetExporterShippingline();
            ViewBag.ShipLines = new SelectList(ShipLines, "SLID", "SLName");
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
            ViewBag.CHA = new SelectList(CHA, "CHANo", "CHAName");
            ViewBag.Partyname = new SelectList(Partyname, "Common_ID", "GSTName");
            ViewBag.ExporterShip = new SelectList(ExporterShipLines, "Exportershippingid", "ExporterShippingname");
            return View();
        }


        [HttpPost]
        public ActionResult MapCustomerWiseAutomailsave(string Activity, string Entrydate, string mailto, string mailtoID, string mailtoemailid,
                   string cc, string Replyto, int IsActive, string entryid)
        {

            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.mapCustomerWiseAutomailsave(Activity, Entrydate, mailto, mailtoID, mailtoemailid, cc, Replyto, IsActive, entryid, userId);

            return Json(message);

        }

        [HttpPost]
        public ActionResult GetMapcustomerwiseinoice()
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetAmmpedautomail_Details");
            Session["ListMapcustomerList"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        [HttpPost]
        public JsonResult GetmaxMappingDetails(string mailto, string mailtoID)
        {

            int Userid = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";

            message = BL.CheckMapEmilID(mailto, mailtoID);
            return Json(message);
        }
        public JsonResult GetmaxMapping()
        {

            int Userid = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";

            message = BL.CheckMapEmil(Userid,20);
            return Json(message);
        }

        [HttpPost]
        public JsonResult GetInvoiceListAdjstment(string InvoiceType, string AgentId, string IGMNo, string ItemNo, string BLNo, string BOENo, string BondNo, string ShippingBillNo, string AssessmentNo, string TDSType)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            long CustId = 0;
            if (AgentId == "" || AgentId == null)
            {
                CustId = 0;
            }
            else { CustId = Convert.ToInt64(AgentId); }

            List<BE.CategorywiseInvoice> containerList = new List<BE.CategorywiseInvoice>();
            tblInvoiceList = db.sub_GetDatatable("SP_ImportPDADJUSTMENT_NEW_1 '" + InvoiceType + "','" + AgentId + "','" + IGMNo + "','" + ItemNo + "','" + BLNo + "','" + BOENo + "','" + BondNo + "','" + ShippingBillNo + "','" + AssessmentNo + "','" + TDSType + "'");

            if (tblInvoiceList.Rows.Count != 0)
            {
                foreach (DataRow row in tblInvoiceList.Rows)
                {
                    BE.CategorywiseInvoice rowdata = new BE.CategorywiseInvoice();
                    rowdata.AssessNo = Convert.ToString(row["Assess No"]);
                    rowdata.AssessYear = Convert.ToString(row["Assess Year"]);
                    rowdata.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    rowdata.InvoiceDate = Convert.ToString(row["Invoice Date"]);
                    rowdata.InvoiceAmount = Convert.ToDouble(row["Invoice Amt"]);
                    rowdata.CreditAmount = Convert.ToDouble(row["Credit Amt"]);
                    rowdata.PrevReceiptAmount = Convert.ToDouble(row["Prev Received Amt"]);
                    rowdata.ReceivalAmount = Convert.ToDouble(row["Receival Amt"]);
                    rowdata.ReceivedAmount = Convert.ToDouble(row["Received Amt"]);
                    rowdata.TDS = Convert.ToDouble(row["TDS"]);
                    rowdata.NetReceivedAmount = Convert.ToDouble(row["Net Received"]);
                    rowdata.OS = Convert.ToDouble(row["OS"]);
                    containerList.Add(rowdata);
                }
            }

            var jsonResult = Json(containerList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }




        public ActionResult DirectAdjsutments()
        {

            List<BE.WorkyearReceipt> WorkyearReceipt = new List<BE.WorkyearReceipt>();

            WorkyearReceipt = BL.getWorkyear();

            ViewBag.Workyear = new SelectList(WorkyearReceipt, "WorkyearTDS", "WorkyearTDS");
            return View();
        }



        [HttpPost]
        public JsonResult AjaxDirectAdjustments()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            //var TDSType = fileData.TDSvalue;
            BE.DirectadjustmentsEntities VOuchertraiffList = new BE.DirectadjustmentsEntities();
            List<BE.DirectadjustmentsEntities> VouchertraiffDetails = new List<BE.DirectadjustmentsEntities>();
            if (Request.Files.Count > 0)
            {
                try
                {

                    // Get all files from Request object
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                        //string filename = Path.GetFileName(Request.Files[i].FileName);

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        //string TPNO = fileData.tdstype;
                        // Get the complete folder path and store the file inside it.
                        fname = Path.Combine(Server.MapPath("~/ImportFileReceipt/"), fname);
                        // fname = Path.Combine(fname);
                        file.SaveAs(fname);
                        string extension = Path.GetExtension(fname);
                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                         // conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                            case ".xlsx": //Excel 07 and above.
                                          //conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                        }
                        //BL.AddErrorLog(conString.Replace("'",""));
                        //BL.AddErrorLog(fname.Replace("'", ""));
                        // conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                        // BL.AddErrorLog("Connection close");
                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            // BL.AddErrorLog("OleDbConnection");
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                // BL.AddErrorLog("cmdExcel");
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    // BL.AddErrorLog("odaExcel");
                                    DataTable dt = new DataTable();
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    // BL.AddErrorLog("Open connection");
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    // BL.AddErrorLog(sheetName);
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();

                                    DataTable VOucherTraifftDT = new DataTable();


                                    VOucherTraifftDT.Columns.Add("InvoiceNo");
                                    VOucherTraifftDT.Columns.Add("ReferenceNo");
                                    VOucherTraifftDT.Columns.Add("AdjustmentAmount");
                                    VOucherTraifftDT.Columns.Add("TDS");

                                    //DriverPaymentDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        if (row[0].ToString() != "" && row[1].ToString() != "" && row[2].ToString() != "" && row[3].ToString() != "")
                                        {


                                            DataRow dar = VOucherTraifftDT.NewRow();

                                            dar["InvoiceNo"] = row[0].ToString().ToUpper();
                                            dar["ReferenceNo"] = row[1].ToString().ToUpper();
                                            dar["AdjustmentAmount"] = row[2].ToString().ToUpper();
                                            dar["TDS"] = row[3].ToString().ToUpper();
                                            VOucherTraifftDT.Rows.Add(dar);
                                            int linenum = dt.Rows.IndexOf(row);
                                            if (linenum == 1050)
                                            {
                                                int count1 = 0;
                                            }
                                        }
                                        else
                                        {
                                            return Json("Some columns are blank. Please check and re-import...");
                                        }
                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }
                                    int UserID = Convert.ToInt32(Session["Tracker_userID"]);

                                    List<BE.CategorywiseInvoice> Vouchertariff = new List<BE.CategorywiseInvoice>();

                                    message = BL.GetValidateReceipt(VOucherTraifftDT, UserID);


                                    if (message == "")
                                    {
                                        Vouchertariff = BL.ImportDirectAdjustments(VOucherTraifftDT);
                                    }
                                    var returnField = new { List1 = message, List2 = Vouchertariff };
                                    return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                    // }

                                }
                            }
                        }
                    }
                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred. Error details: " + ex.Message);
                    BL.AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", ""));
                    return Json("Error occurred. Error details: " + ex.Message);
                    //return Json(1);
                    // return Json("FormData is not supported.");
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }



        [HttpPost]
        public ActionResult SaveDirectAdjsuments(List<BE.CategorywiseInvoice> DirectAdjustment,string TDSWorkyear)
        {


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("InvoiceDate");
            dataTable.Columns.Add("InvoiceAmount");
            dataTable.Columns.Add("CreditAmount");
            dataTable.Columns.Add("PrevReceiptAmount");
            dataTable.Columns.Add("ReceivalAmount");
            dataTable.Columns.Add("ReceivedAmount");
            dataTable.Columns.Add("TDS");
            dataTable.Columns.Add("NetReceivedAmount");
            dataTable.Columns.Add("OS");
            dataTable.Columns.Add("ReceiptNo");
            dataTable.Columns.Add("WorkYear");
            dataTable.Columns.Add("AssessNo");
            dataTable.Columns.Add("AssessYear");
            dataTable.Columns.Add("AssessDate1");
            dataTable.Columns.Add("ReceiptRef_No");
            dataTable.TableName = "PT_AddReceiptDirectAdjustments";


            foreach (BE.CategorywiseInvoice item in DirectAdjustment)
            {
                DataRow row = dataTable.NewRow();

                row["InvoiceNo"] = item.InvoiceNo;
                row["InvoiceDate"] = Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MMM-dd hh:mm");
                row["InvoiceAmount"] = item.InvoiceAmount;
                row["CreditAmount"] = item.CreditAmount;
                row["PrevReceiptAmount"] = item.PrevReceiptAmount;
                row["ReceivalAmount"] = item.ReceivalAmount;
                row["ReceivedAmount"] = item.ReceivedAmount;
                row["TDS"] = item.TDS;
                row["NetReceivedAmount"] = item.NetReceivedAmount;
                row["OS"] = item.OS;
                row["ReceiptNo"] = item.ReceiptNo;
                row["WorkYear"] = item.WorkYear;
                row["AssessNo"] = item.AssessNo;
                if (item.AssessYear == null)
                {
                    item.AssessYear = "";
                }
                row["AssessYear"] = item.AssessYear;
                row["AssessDate1"] = Convert.ToDateTime(item.AssessDate1).ToString("yyyy-MMM-dd hh:mm");
                row["ReceiptRef_No"] = item.ReferenceNo;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = BL.SaveDirectAdjustmentDetails(dataTable, Userid, TDSWorkyear);
            return Json(message);

        }



        [HttpPost]
        public ActionResult SaveOnAdjsuments(List<BE.OnScreenAj> Direct, string TDSWorkyear)
        {


            string message = "";

            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("InvoiceNo");
            dataTable1.Columns.Add("ReferenceNo");
            dataTable1.Columns.Add("AdjustmentAmount");
            dataTable1.Columns.Add("TDS");
            foreach (BE.OnScreenAj item in Direct)
            {
                DataRow row = dataTable1.NewRow();
                row["InvoiceNo"] = item.InvoiceNo;
                row["ReferenceNo"] = item.ReciptNo;
                row["AdjustmentAmount"] = item.ReceivalAmount;
                row["TDS"] = item.ReceivedAmountTDS;

                dataTable1.Rows.Add(row);
                int Userid = Convert.ToInt32(Session["Tracker_userID"]);
                message = BL.GetValidateReceipt(dataTable1, Userid);

            }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ReciptNo");
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("InvoiceAmount");
            dataTable.Columns.Add("TDS");
            dataTable.Columns.Add("PrevReceiptAmount");
            dataTable.Columns.Add("ReceivalAmount");
            dataTable.Columns.Add("ReceivedAmountTDS");
            dataTable.Columns.Add("NetReceivedAmount");
            dataTable.Columns.Add("CustID");
            dataTable.Columns.Add("PendingReceipt");
            dataTable.TableName = "PT_OnScreenAdjustment";


            foreach (BE.OnScreenAj item in Direct)
            {
                DataRow row = dataTable.NewRow();

                row["ReciptNo"] = item.ReciptNo;
                row["InvoiceNo"] = item.InvoiceNo;
                row["InvoiceAmount"] = item.InvoiceAmount;
                row["TDS"] = item.TDS;
                row["PrevReceiptAmount"] = item.PrevReceiptAmount;
                row["ReceivalAmount"] = item.ReceivalAmount;
                row["ReceivedAmountTDS"] = item.ReceivedAmountTDS;
                row["NetReceivedAmount"] = item.NetReceivedAmount;
                row["CustID"] = item.CustID;
                row["PendingReceipt"] = item.PendingReceipt;
                dataTable.Rows.Add(row);
                int Userid = Convert.ToInt32(Session["Tracker_userID"]);
                 


                if (message == "")
                {
                    message = BL.SaveAdjsuments(dataTable, Userid, TDSWorkyear);
                }
              
            }

          
            return Json(message);

        }

        [HttpPost]
        public ActionResult GetOutStandingStatementSummary(string Status,string ason)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

         
            if (Status == "ParytWise")
            {
                dt = db.sub_GetDatatable("USP_OUTStanding_Statement_all_Summary '" + 1 + "','" + ason + "'");
                Session["PartyName"] = "Party Wise";


            }
            else
            {
                dt = db.sub_GetDatatable("USP_OUTStanding_Statement_all_Summary '" + 0 + "','" + ason + "'");
                Session["PartyName"] = "Customer Wise Summary";


            }
            Session["GetOtustandingreportList"] = dt;
            Session["ason"] = ason;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult Party_bill_Entry()
        {

            return View();
        }

        public JsonResult GetautpcompleteCustomer(string prefixText)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = BL.GetcustomerAutocompletedebit(prefixText);

            return Json(Customer, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetautpcompleteDebitDetails(string prefixText)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.GstDetails> Customer = new List<BE.GstDetails>();
            Customer = BL.GetDebitamountfor(prefixText);

            return Json(Customer, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SavePartyDebitEntryDetails(List<BE.partyDebitEntitiesDetails> Debitdata, string NetAmount, string CGST, string GST, string GrandTotal,
           string DebitAcoount, string Remark, string DebitID, string DebitDateList, string DebitReferenceNo, string GSTNo, string IGST)
        {


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Debitreferenceno");
            dataTable.Columns.Add("DebitDate");
            dataTable.Columns.Add("Customername");
            dataTable.Columns.Add("GstNo");
            dataTable.Columns.Add("Descriptionno");
            dataTable.Columns.Add("HSNNo");
            dataTable.Columns.Add("Netamount");
            dataTable.Columns.Add("CustomerNameID");

            dataTable.TableName = "PT_AddDebitDetails";


            foreach (BE.partyDebitEntitiesDetails item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["Debitreferenceno"] = item.Debitreferenceno;
                row["DebitDate"] = Convert.ToDateTime(item.DebitDate).ToString("yyyy-MM-dd HH:mm:ss");
                row["Customername"] = item.Customername;
                row["GstNo"] = item.GstNo;
                row["Descriptionno"] = item.Descriptionno;
                row["HSNNo"] = item.HSNNo;
                row["Netamount"] = item.Netamount;
                row["CustomerNameID"] = item.CustomerNameID;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = BL.SavepartyDebitEntry(dataTable, NetAmount, CGST, GST, GrandTotal, DebitAcoount, Remark, DebitID, DebitDateList, DebitReferenceNo, GSTNo, IGST, Userid);
            return Json(message);

        }

        public JsonResult GetGSTNOForCustomer(string gstID)
        {

            int Userid = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";

            message = BL.GetGSTDetails(gstID);
            return Json(message);
        }

        public ActionResult AjaxGetDebitDetails(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_DebitBillentryDetails '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["DebitBillEntryDetails"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelDebitBillDetails()
        {

            DataTable dt = (DataTable)Session["DebitBillEntryDetails"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Debit Bill Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Debit Bill Report <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        public JsonResult CancelDebitEntryDetails(string debitnoteno)
        {

            int Userid = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";

            message = BL.CancelDebitDetails(debitnoteno, Userid);
            return Json(message);
        }

         public ActionResult Mapmailsave(string mailtoID, string mailto, string mailcc,string Entryid,string mailtoemailid)
        {

            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.MapMailSave(mailtoID, mailto, mailcc, userId, Entryid, mailtoemailid);

            return Json(message);

        }
        public ActionResult MapMail()
        {

            List<BE.ShipLines> ShipLines = new List<BE.ShipLines>();
            List<BE.exporterShipping> ExporterShipLines = new List<BE.exporterShipping>();
            List<BE.Customer> Customer = new List<BE.Customer>();
            List<BE.CHA> CHA = new List<BE.CHA>();
            List<BE.PartyNameEntities> Partyname = new List<BE.PartyNameEntities>();

            ShipLines = BL.getShipLines();
            Customer = BL.getCustomer();
            CHA = BL.getCHA();
          
            ExporterShipLines = BL.GetExporterShippingline();
            ViewBag.ShipLines = new SelectList(ShipLines, "SLID", "SLName");
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
            ViewBag.CHA = new SelectList(CHA, "CHANo", "CHAName");
            return View();
        }
        public JsonResult GetmaxMappingDetail(string mailto, string mailtoID)
        {

            int Userid = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";

            message = BL.CheckMapEmilIDs(mailto, mailtoID);
            return Json(message);
        }

        public ActionResult MapCustomerWiseAutomail(string Activity, string Entrydate, string mailto, string mailtoID, string mailtoemailid,
                   string cc, string Replyto, int IsActive, string entryid)
        {

            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.MapCustomerWiseAutomailsaveMap(Activity, Entrydate, mailto, mailtoID, mailtoemailid, cc, Replyto, IsActive, entryid, userId);

            return Json(message);

        }

        public ActionResult GetMapmail()
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetMapMail_Details");
            Session["ListMapcustomerList"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult CheckDuplicateForMail(string mailto, string mailtoID, string Entryid)
        {

            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.CheckDuplicateMailID(mailtoID, mailto, Entryid);

            return Json(message);

        }


        public JsonResult AddDirectFuel(string ModeNo)
        {
            string message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


            DataTable PortsDS = new DataTable();
            PortsDS = db.sub_GetDatatable("Usp_Mode_validation_CFS '" + ModeNo + "'");


            if (PortsDS.Rows.Count > 0)
            {
                message = "" + PortsDS.Rows[0][0] + "";
            }
            else
            {
                message = "";
            }


            return Json(message);

        }
        public JsonResult AddModeVeli(string ModeNo, string PaymentType,string Amount)
        {
            string message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


            DataTable PortsDS = new DataTable();
            PortsDS = db.sub_GetDatatable("USP_IMP_AD_BAL_RECEIPT_Running_Validations '" + ModeNo + "','" + PaymentType + "','" + Amount + "'");


            if (PortsDS.Rows.Count > 0)
            {
                message = "" + PortsDS.Rows[0][0] + "";
            }
            else
            {
                message = "";
            }


            return Json(message);

        }


        // JOURNAL ENTRY SAGAR 
        public ActionResult JournalEntry()
        {
            List<BE.ReceiptAdjustments> CategoryList = new List<BE.ReceiptAdjustments>();
            CategoryList = trainTrackerProvider.Categorys();
            ViewBag.CategoryList = new SelectList(CategoryList, "ID", "Criteria");


            return View();
        }
        // get amount by ref no
        [HttpPost]
        public JsonResult GetAmountByRefNo(string ReferenceNo)
        {

            try
            {

                HC.DBOperations db = new HC.DBOperations();
                DataTable dataTable = new DataTable();
                List<BE.ReceiptEntryEntities> receiptEntries = new List<BE.ReceiptEntryEntities>();
                dataTable = db.sub_GetDatatable("USP_GET_JV_Amount '" + ReferenceNo + "'");

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        BE.ReceiptEntryEntities receiptEntry = new BE.ReceiptEntryEntities();
                        receiptEntry.TotalAmountForJB = Convert.ToDouble(row["receiptAmount"]);
                        receiptEntries.Add(receiptEntry);
                    }
                }

                var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
            }
            catch (Exception p)
            {
                return Json(p.Message);
            }

        }

        public ActionResult SaveJVDetails(string CustomerName, string CustomerName2, string PendingReceipt, string TransAmt, string Remarks,string EntryType,string Criteria,string JVFor)
        {
            try
            {
                int Userid = Convert.ToInt32(Session["Tracker_userID"]);


                BE.ResponseMessage GetResponse = new BE.ResponseMessage();


                GetResponse = BL.SaveJVDetails(CustomerName, CustomerName2, PendingReceipt, TransAmt, Remarks, Userid, EntryType, Criteria, JVFor);
                return Json(GetResponse);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }

        public ActionResult ReImportReceiptPrint(string receiptNo, string workyear)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblReceiptDetails = new DataTable();
            DataTable tblPaymentDetails = new DataTable();
            DataTable tblPaymentDetails2 = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("[GetImportReceiptDetReprint] '" + receiptNo + "','" + workyear + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblReceiptDetails = getJobOrderSet.Tables[1];
                tblInvoiceDetails = getJobOrderSet.Tables[2];
                tblPaymentDetails = getJobOrderSet.Tables[3];
                tblPaymentDetails2 = getJobOrderSet.Tables[4];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                }
                foreach (DataRow dr in tblReceiptDetails.Rows)
                {
                    ViewBag.ReceiptNo = dr["ReceiptNo"];
                    ViewBag.ReceiptDate = dr["ReceiptDate"];
                    ViewBag.IGMNo = dr["IGMNo"];
                    ViewBag.ItemNo = dr["ItemNo"];
                    ViewBag.PayFrom = dr["PayFrom"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.ImporterName = dr["ImporterName"];
                    ViewBag.Category = dr["Category"];
                    ViewBag.AccountNo = dr["AccountNo"];
                }
                foreach (DataRow dr in tblPaymentDetails2.Rows)
                {
                    ViewBag.NetAmount = dr["Net Amount"];
                    ViewBag.TDSAmount = dr["TDS Amount"];
                    ViewBag.ReceivedAmount = dr["Receipt Amount"];
                    ViewBag.AmountInWord = dr["AmtInWord"]; ;
                    ViewBag.ReceiptRemarks = dr["Remarks"];
                    ViewBag.AccessAmount = dr["AccessAmount"];
                    ViewBag.GeneratedBy = dr["Generated By"];
                }

                foreach (DataRow dr1 in tblInvoiceDetails.Rows)
                {
                    if (dr1["Party Name"].ToString() != "")
                    {
                        ViewBag.BillParty = dr1["Party Name"];
                        break;
                    }
                }
            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.PaymentList = tblPaymentDetails.AsEnumerable();

            return PartialView();

        }
        public ActionResult ExportToExcelReceiptSummary()
        {

            DataTable dt = (DataTable)Session["getReceiptDetailsSummary"];

            HC.DBOperations db = new HC.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            string Tittle = "AS On " + Session["txtentryDate"] + ".";
            GridView gridview = new GridView();
            dt.Columns.Remove("#");
            dt.Columns.Remove("##");
            gridview.DataSource = dt;
            gridview.DataBind();
            gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Receipt_Summary_Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Receipt_Summary_Details<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult GetAgeingOutstanding(string Number, string ason)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            dt = db.sub_GetDatatable("[USP_AGING_CUST_WISE_OS_Summary] '" + Number + "','" + ason + "'");
            Session["PartyName"] = "All";


            Session["USP_AGING_CUST_WISE_OS_Summary"] = dt;
            Session["ason"] = ason;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult GetEdiAgeing(string Number, string ason, string fromS, string tos)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            dt = db.sub_GetDatatable("USP_AGING_CUST_WISE_OS_Summary_Details'" + Number + "','" + ason + "','" + fromS + "','" + tos + "'");
            Session["PartyName"] = "All";


            Session["USP_AGING_CUST_WISE_OS_Summary_Details"] = dt;
            Session["ason"] = ason;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult ReceiptSearch()
        {
            return View();
        }
        public ActionResult getReceiptSearchDtl(string FromDate, string ToDate, string searchcerteria, string Searchtext)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("GetReceiptSeachDtl '" + FromDate + "','" + ToDate + "','" + searchcerteria + "','" + Searchtext + "'");
            if (tblInvoiceList.Rows.Count > 0)
            {
                InvoiceList = JsonConvert.SerializeObject(tblInvoiceList);
            }
            else
            {
                InvoiceList = "0";
            }

            return new JsonResult() { Data = InvoiceList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ViewReceiptDetail(string receiptNo, string workyear)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblReceiptDetails = new DataTable();
            DataTable tblPaymentDetails = new DataTable();
            DataTable tblPaymentDetails2 = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("[GetImportReceiptDetReprint] '" + receiptNo + "','" + workyear + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblReceiptDetails = getJobOrderSet.Tables[1];
                tblInvoiceDetails = getJobOrderSet.Tables[2];
                tblPaymentDetails = getJobOrderSet.Tables[3];
                tblPaymentDetails2 = getJobOrderSet.Tables[4];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                }
                foreach (DataRow dr in tblReceiptDetails.Rows)
                {
                    ViewBag.ReceiptNo = dr["ReceiptNo"];
                    ViewBag.ReceiptDate = dr["ReceiptDate"];
                    ViewBag.IGMNo = dr["IGMNo"];
                    ViewBag.ItemNo = dr["ItemNo"];
                    ViewBag.PayFrom = dr["PayFrom"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.ImporterName = dr["ImporterName"];
                    ViewBag.Category = dr["Category"];
                }
                foreach (DataRow dr in tblPaymentDetails2.Rows)
                {
                    ViewBag.NetAmount = dr["Net Amount"];
                    ViewBag.TDSAmount = dr["TDS Amount"];
                    ViewBag.ReceivedAmount = dr["Receipt Amount"];
                    ViewBag.AmountInWord = dr["AmtInWord"]; ;
                    ViewBag.ReceiptRemarks = dr["Remarks"];
                    ViewBag.AccessAmount = dr["AccessAmount"];
                    ViewBag.GeneratedBy = dr["Generated By"];
                }


            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.PaymentList = tblPaymentDetails.AsEnumerable();
            ViewBag.receiptNo = receiptNo;
            ViewBag.workyear = workyear;

            return PartialView();
        }

        public ActionResult SearchReceipt()
        {
            string receiptNo = "";
            string workyear = "";
            string Type = "";

            receiptNo = Convert.ToString(TempData["receiptNo"]);
            workyear = Convert.ToString(TempData["workyear"]);
            Type = Convert.ToString(TempData["Type"]);

            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblReceiptDetails = new DataTable();
            DataTable tblPaymentDetails = new DataTable();
            DataTable tblPaymentDetails2 = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("[GetImportReceiptDetReprint] '" + receiptNo + "','" + workyear + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblReceiptDetails = getJobOrderSet.Tables[1];
                tblInvoiceDetails = getJobOrderSet.Tables[2];
                tblPaymentDetails = getJobOrderSet.Tables[3];
                tblPaymentDetails2 = getJobOrderSet.Tables[4];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                }
                foreach (DataRow dr in tblReceiptDetails.Rows)
                {
                    ViewBag.ReceiptNo = dr["ReceiptNo"];
                    ViewBag.ReceiptDate = dr["ReceiptDate"];
                    ViewBag.IGMNo = dr["IGMNo"];
                    ViewBag.ItemNo = dr["ItemNo"];
                    ViewBag.PayFrom = dr["PayFrom"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.ImporterName = dr["ImporterName"];
                    ViewBag.Category = dr["Category"];
                }
                foreach (DataRow dr in tblPaymentDetails2.Rows)
                {
                    ViewBag.NetAmount = dr["Net Amount"];
                    ViewBag.TDSAmount = dr["TDS Amount"];
                    ViewBag.ReceivedAmount = dr["Receipt Amount"];
                    ViewBag.AmountInWord = dr["AmtInWord"]; ;
                    ViewBag.ReceiptRemarks = dr["Remarks"];
                    ViewBag.AccessAmount = dr["AccessAmount"];
                    ViewBag.GeneratedBy = dr["Generated By"];
                }




            }

            DataTable JVDetails = new DataTable();
            JVDetails = db.sub_GetDatatable("USP_Receipt_JV_Search_Dets '" + receiptNo + "'");

            var json = JsonConvert.SerializeObject(JVDetails);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);

            ViewBag.JVDetails = jsonResult.MaxJsonLength = int.MaxValue;

            DataTable Bank_Reco_Details = new DataTable();
            Bank_Reco_Details = db.sub_GetDatatable("[USP_Receipt_Search_BK_Reco_Dets] '" + receiptNo + "'");
            ViewBag.Bank_Reco_Details = JsonConvert.SerializeObject(Bank_Reco_Details);

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.InvoiceItemListCount = tblInvoiceDetails.Rows.Count;
            ViewBag.PaymentList = tblPaymentDetails.AsEnumerable();
            ViewBag.receiptNo = receiptNo;
            ViewBag.workyear = workyear;
            ViewBag.Type = Type;

            return View();
        }
        public JsonResult GetDetails(string receiptNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();


            dt = db.sub_GetDatatable("USP_Receipt_JV_Search_Dets '" + receiptNo + "'");
            Session["USP_Receipt_JV_Search_Dets"] = dt;

            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;


        }

        public JsonResult BankRecoDetail(string receiptNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();


            dt = db.sub_GetDatatable("USP_Receipt_Search_BK_Reco_Dets '" + receiptNo + "'");
            Session["USP_Receipt_Search_BK_Reco_Dets"] = dt;

            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;


        }
        [HttpPost]
        public ActionResult StoreReceiptNoInTemp(string receiptNo, string workyear, string Type)
        {
            TempData["receiptNo"] = receiptNo;
            TempData["workyear"] = workyear;
            TempData["Type"] = Type;
            return Json(1);
        }



        public ActionResult ImportTimelineReceiptSearch(string receiptRefNo)
        {

            DataTable tblTimeLineData = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            tblTimeLineData = db.sub_GetDatatable("[USP_Receipt_TimeLine_Dets] '" + receiptRefNo + "'");
            ViewBag.TimeLine = tblTimeLineData.AsEnumerable();
            return PartialView();
        }

        public ActionResult ExportToExcelBillWiseDetail(string receiptNo, string workyear)
        {


            HC.DBOperations db = new HC.DBOperations();
            DataSet getJobOrderSet = new DataSet();

            DataTable CompanyMaster = new DataTable();
            DataTable dt = new DataTable();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);

            getJobOrderSet = db.sub_GetDataSets("[GetImportReceiptDetReprint] '" + receiptNo + "','" + workyear + "'");
            string Tittle = "Bill Wise Detail";

            dt = getJobOrderSet.Tables[2];
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();
            gridview.HeaderRow.Style.Add("background-color", "LightBlue");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BillWiseDetail.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='" + dt.Columns.Count + "'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='" + dt.Columns.Count + "'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='" + dt.Columns.Count + "'><strong style='font-size: 15px'>Receipt Search<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='" + dt.Columns.Count + "'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='" + dt.Columns.Count + "'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }
        public JsonResult getReceiptSummaryPending(  string ToDate )
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("GetCreditReceiptPendingSummary '" + ToDate + "'");
            if (tblInvoiceList.Rows.Count > 0)
            {
                InvoiceList = JsonConvert.SerializeObject(tblInvoiceList);
            }
            else
            {
                InvoiceList = "0";
            }
            Session["GetCreditReceiptPendingSummary"] = tblInvoiceList;
            var jsonResult = Json(InvoiceList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult OnScreenAdjustment()
        {

            List<BE.WorkyearReceipt> WorkyearReceipt = new List<BE.WorkyearReceipt>();

            WorkyearReceipt = BL.getWorkyear();

            ViewBag.Workyear = new SelectList(WorkyearReceipt, "WorkyearTDS", "WorkyearTDS");
            return View();
        }
        public ActionResult RefundRequestEntry()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult _AddAttachmentForRefund(int SrNo1)
        {
            ViewBag.SrNo1 = SrNo1;
            return PartialView();
        }
        public ActionResult UploadNewRefundachment(BE.AttachmentList fileData)
        {
            BE.AttachmentList QCAttach = new BE.AttachmentList();
            if (Request.Files.Count > 0)
            {
                try
                {
                    int Userid = Convert.ToInt16(Session["Tracker_userID"]);
                    string Type = fileData.Type;
                    Type = Type + Userid;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fname;
                        string root = Server.MapPath("~/Uploads/TempFolder/" + Type + "/");
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        string contentType;
                        contentType = MimeMapping.GetMimeMapping(fname);

                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        string check = Path.Combine(Server.MapPath("~/Uploads/TempFolder/" + Type + "/"), fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(check);
                        QCAttach.DocName = fname;
                        QCAttach.FilePath = check;
                        QCAttach.ContentType = contentType;
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return Json(QCAttach);
        }

        public JsonResult RefundRequestSave(string CategoryMode, string hdnCustomerName, string PendingReceipt, string PendingReceiptAmount, string BalanceAmount, string ReceivedAmount
            , string Remarks, string UploadDocument )
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            HC.DBOperations db = new HC.DBOperations();
            string message = "";
            //DataTable dt = new DataTable();


            i = db.sub_ExecuteNonQuery("USP_INSERT_REFUND_REQUEST_ENTRY '" + CategoryMode + "','" + hdnCustomerName + "','" + PendingReceipt + "','" + PendingReceiptAmount + "','" + BalanceAmount + "','" + ReceivedAmount + "','" + Remarks + "','" + UploadDocument + "','" + UserID + "'");

            if (i > 0)
            {

                message = "Record saved successfully";

            }
            else
            {
                message = "Record not saved please try again!";

            }

            return Json(message);
        }
        public ActionResult GetRefundRequesSummary(string From, string To)
        {
            //List<BE.PendingContainersForJoUpdationsEntities> InventoryList = new List<BE.PendingContainersForJoUpdationsEntities>();
            //InventoryList = reportprovider.GetInventoryList(AsonDate);
            //// ViewBag.InventoryDL = InventoryList;
            //return Json(InventoryList, JsonRequestBehavior.AllowGet);
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_summery_Refund '" + Convert.ToDateTime(From).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(To).ToString("yyyyMMddHHmm") +   "'");
            Session["CategoryInventory"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
 
        }
    }



    public class PaymentDet {
       public string PaymentId { get; set; }
        public string Amount { get; set; }
        public string ModeNo { get; set; }
        public string BankId { get; set; }
        public string ModeDate { get; set; }

        public PaymentDet()
        {
            ModeNo = "";
            BankId = "";
            ModeDate = "";
        }
    }
}