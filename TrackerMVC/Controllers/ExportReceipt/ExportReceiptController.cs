using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using DP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrailerTransport;
using train = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrainMaster;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
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

namespace TrackerMVC.Controllers.ExportReceipt
{
    public class ExportReceiptController : Controller
    {
        BM.IGM.IGM IG = new BM.IGM.IGM();
        BM.BLDataManager.GenerateBL BL = new BM.BLDataManager.GenerateBL();
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
        public JsonResult getPartyNameReceipt(string  Mode)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable =db.sub_GetDatatable("USP_GetPartyNameReceipt '" + Mode + "'");

            if(dataTable != null)
            {
                foreach(DataRow row in dataTable.Rows)
                {
                    BE.Customer customer = new BE.Customer();
                    customer.AGID =Convert.ToInt32(row["GSTID"]);
                    customer.AGName = Convert.ToString(row["GSTName"]);
                    Customerlst.Add(customer);
                }
            }

            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportReceipt()
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
            ModelState.Clear();
            Session["PaymentSubDet"] = null;

            return View();
        }

        [HttpPost]
        public JsonResult SaveReceiptForm(BE.CategorywiseInvoice viewModel,List<BE.CategorywiseInvoice> invoiceData,string PayFrom,List<PaymentDet> paymentData,string Category,string TDSPerc,string ReceiptType,string Remarks,string PayParty)
        {
            string message = "";
            int ReceiptNo = 0;
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
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

            dtPaymentDetails.Columns.Add("PaymentId");
            dtPaymentDetails.Columns.Add("Amount");
            dtPaymentDetails.Columns.Add("ModeNo");
            dtPaymentDetails.Columns.Add("BankId");
            dtPaymentDetails.Columns.Add("ModeDate");

            if (invoiceData.Count > 0)
            {
                foreach(var item in invoiceData)
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
                    dtInvoiceDetails.Rows.Add(dr);
                }

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
            else
            {
                message = "Please Enter Invoice Details.";
                return Json(message);
            }

            if (message == "" || message == "Success")
            {
                viewModel.BillParty =Convert.ToInt64(PayParty);
                ReceiptNo = BL.AddInvoiceDataExport(viewModel, dtInvoiceDetails, dtPaymentDetails, userId, Category, TDSPerc, ReceiptType,Remarks);
            }
            else if (message == "-1")
            {
                ReceiptNo = -1;
            }

            return Json(ReceiptNo);
        }

            

        [HttpPost]
        public JsonResult DailyCollection()
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dsList = new DataTable();
         
            dsList = db.sub_GetDatatable("get_sp_today_Export_collection_new");

            var listData = JsonConvert.SerializeObject(dsList);

            var jsonResult = Json(listData, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult ExportReceiptPrint(string receiptNo,string AssessWork)
        {
            ///AssessWork = "2020-21";
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblReceiptDetails = new DataTable();
            DataTable tblPaymentDetails = new DataTable();
            DataTable tblPaymentDetails2 = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetExportReceiptDet '" + receiptNo + "','" +AssessWork+ "'"  );

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
            tblInvoiceList = db.sub_GetDatatable("SP_ExportPDADJUSTMENT_NEW '" + InvoiceType + "','" + AgentId + "','" + IGMNo + "','" + ItemNo + "','" + BLNo +"','" + BOENo + "','" + BondNo + "','" + ShippingBillNo + "','" + AssessmentNo + "','" + TDSType +"'");

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
        public JsonResult getReceiptSummaryDet(string FromDate, string ToDate)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("GetReceiptDet_Export '" + FromDate + "','" + ToDate + "'");
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
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("USP_CancelReceipt_Export '" + ReceiptNo + "','" + userId + "','" + Remarks + "'");

            if (retVal > 0)
            {
                Message = "Receipt Cancel Successfully.";
            }
            else
            {
                Message = "Receipt Not Cancel Successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult getPenginInvoiceList()
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("GetPendingInvoiceList_Export");
            if (tblInvoiceList.Rows.Count > 0)
            {
                Session["GetPendingInvoiceList_Export"] = tblInvoiceList;
                InvoiceList = JsonConvert.SerializeObject(tblInvoiceList);
            }
            else
            {
                Session["GetPendingInvoiceList_Export"] = null;
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
            DataTable ContainerArrival = (DataTable)Session["GetPendingInvoiceList_Export"];
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
        string ValidateReceipt(string Category, string AssessNo, string AssessYear)
        {
            string Message = "";
            HC.DBOperations db = new HC.DBOperations();
            DataTable dtValidate = new DataTable();
            try
            {
                dtValidate = db.sub_GetDatatable("ValidateReceipt '" + AssessNo + "','" + AssessYear + "','" + Category + "'");
                if (dtValidate != null)
                {
                    foreach (DataRow row in dtValidate.Rows)
                    {
                        Message = row["Message"].ToString();
                    }
                }
            }
            catch (Exception ex) { Message = ex.Message; }

            return Message;
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