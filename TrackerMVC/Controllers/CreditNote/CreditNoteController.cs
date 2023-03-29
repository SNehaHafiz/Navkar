using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.CreditNote;
using CD = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace TrackerMVC.Controllers
{
    public class CreditNoteController : Controller
    {
        // GET: CreditNote
        BM.CreditNote trainTrackerProvider = new BM.CreditNote();
        public ActionResult CreditNote()
        {
            List<BE.CreditNoteEntities> ModeGroup = new List<BE.CreditNoteEntities>();
            ModeGroup = trainTrackerProvider.getModeGroup();
            ViewBag.Mode = new SelectList(ModeGroup, "CATEGORYID", "CATEGORYType");
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<BE.PartyMaster> PartyMaster = new List<BE.PartyMaster>();
            PartyMaster = trainTrackerProvider.getPartyMaster();
            ViewBag.GSTName = new SelectList(PartyMaster, "GSTID", "GSTName");
            return View();
             
        }

        public JsonResult GetSearch(string ddlCategory, string SearchType, string SearchO)
        {

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.CreditNoteEntities> JOLIst = new List<BE.CreditNoteEntities>();
            JOLIst = trainTrackerProvider.GetJOListForSummary(ddlCategory, SearchType, SearchO);
            return Json(JOLIst);
        }

        public ActionResult SaveTally(List<BE.CreditNoteEntities> Debitdata, string ddlCategory)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("AssessNo");
            dataTable.Columns.Add("WorkYear");
            dataTable.Columns.Add("InvoiceNo");
           

            foreach (BE.CreditNoteEntities item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["AssessNo"] = item.AssessNo;
                row["WorkYear"] = item.WorkYear;
                row["InvoiceNo"] = item.InvoiceNo;
                 
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = trainTrackerProvider.SaveLoadedGatepassdetails(dataTable, ddlCategory, Userid);
            return Json(message);

        }

        public JsonResult GetSearchGrid()
        {

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.CreditNoteEntities> JOLIst = new List<BE.CreditNoteEntities>();
            JOLIst = trainTrackerProvider.GetGridSummary(Userid);
            return Json(JOLIst);
        }

        public JsonResult AjaxGetItemDetails(string AssessNo, string WorkYear, string Categorys)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.CreditNoteAmountEntities> JOLIst = new List<BE.CreditNoteAmountEntities>();
            JOLIst = trainTrackerProvider.GetAssessno(AssessNo, WorkYear, Userid, Categorys);
            return Json(JOLIst);
        }


        public JsonResult GetTaxID(string PaidAmount,int lblTaxID,string statecode)
        {

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.CreditNoteCre> JOLIst = new List<BE.CreditNoteCre>();
            JOLIst = trainTrackerProvider.GetTaxIDAmount(PaidAmount, lblTaxID, statecode);
            return Json(JOLIst);
        }

        public JsonResult CreditSave(List<BE.CreditNoteAmountEntities> Debitdata, string CreditNoteNo, string Assessno, string CreditNoteDate, string Category, string PartyID, string GrandTotal, string remarks, string statecode)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("Workyear");
            dataTable.Columns.Add("AccountName");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("NetAmount");
            dataTable.Columns.Add("CreditedAmount");
            dataTable.Columns.Add("CreditAmount");
            dataTable.Columns.Add("AccountID");
            dataTable.Columns.Add("JONo");
            dataTable.Columns.Add("TaxID");

            foreach (BE.CreditNoteAmountEntities item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["InvoiceNo"] = item.InvoiceNo;
                row["Workyear"] = item.WorkYear;
                row["AccountName"] = item.accountname;
                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["NetAmount"] = item.nettotal;
                row["CreditedAmount"] = item.CreditedAmount;
                row["CreditAmount"] = item.creditamount;
                row["AccountID"] = item.AccountID;
                row["JONo"] = item.JONo;
                row["TaxID"] = item.TaxID;

                dataTable.Rows.Add(row);
            }

            message = trainTrackerProvider.SaveLoaded(dataTable, CreditNoteNo, Assessno, CreditNoteDate, Category, PartyID, GrandTotal, remarks, Userid, statecode);

            return Json(message);
        }

        public ActionResult ImportInvoicePrint(string creditnoteno,string workyear,string ID)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblHeadDetails = new DataTable();
            DataTable tblChargesDetails = new DataTable();
            DataTable tblWordAmount = new DataTable();
            DataTable tblBankDetails = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            double Amount = 0;
            double Discount = 0;
            double NetAmount = 0;
            string Srate = "";
            double SGST = 0;
            string CRate = "";
            double CGST = 0;
            string IRate = "";
            double IGST = 0;

            getJobOrderSet = db.sub_GetDataSets("USP_CREDIT_NOTE_PRINT '" + creditnoteno + "','" + workyear + "','"  + ID + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblInvoiceDetails = getJobOrderSet.Tables[1];
                tblContainerDetails = getJobOrderSet.Tables[2];
                tblHeadDetails = getJobOrderSet.Tables[3];
                tblChargesDetails = getJobOrderSet.Tables[4];
             
                foreach (DataRow dr in tblInvoiceDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.AddressII = dr["AddressII"];
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.Con_For = dr["Con_For"];
                    ViewBag.NoteI = dr["NoteI"];
                    ViewBag.NoteII = dr["NoteII"];
                    ViewBag.BankName = dr["BankName"];
                    ViewBag.AccountNo = dr["AccountNo"];
                    ViewBag.BranchName = dr["BranchName"];
                    ViewBag.IFSCCode = dr["IFSCCode"];
                    ViewBag.NoteII = dr["NoteII"];


                    ViewBag.Datetime = Convert.ToDateTime(DateTime.Now).ToString("dd MM yyyy");
                    ViewBag.TIme = Convert.ToDateTime(DateTime.Now).ToString("HH:mm");
                }
                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CreditDate = dr["CreditDate"];
                    ViewBag.GSTName = dr["GSTName"];
                    ViewBag.GSTAddress = dr["GSTAddress"];
                
                    ViewBag.GSTIn_uniqID = dr["GSTIn_uniqID"];
                    ViewBag.State = dr["State"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.AmountInWord = dr["AmountInWord"];
                    ViewBag.Nettotal = dr["Nettotal"];
                    ViewBag.sgst = dr["sgst"];
                    ViewBag.cgst = dr["cgst"];
                    ViewBag.igst = dr["igst"];
                    
                    ViewBag.GrandTotal = dr["GrandTotal"];
                    ViewBag.Remarks = dr["Remarks"];
                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.Today = dr["Today"];
                    ViewBag.CreditNoteCategory = dr["CreditNoteCategory"];
                    ViewBag.CRNO = dr["CRNO"];
                    ViewBag.Irn = dr["Irn"];
                    ViewBag.AckNo = dr["AckNo"];
                    ViewBag.AckDt = dr["AckDt"];
                    ViewBag.SignedQRcode = dr["SignedQRcode"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.Remarks = dr["Remarks"];
                    ViewBag.CreditNoteCategory = dr["CreditNoteCategory"];


                }

                //ViewBag.BankName = tblBankDetails.Rows[0]["BankName"];
                //ViewBag.AccountNo = tblBankDetails.Rows[0]["AccountNo"];
                //ViewBag.IFSCCode = tblBankDetails.Rows[0]["IFSCCode"];
                //ViewBag.Remarks = tblBankDetails.Rows[0]["Remarks"];
                //ViewBag.BranchName = tblBankDetails.Rows[0]["BranchName"];

                //ViewBag.TotalAmountInWord = tblWordAmount.Rows[0]["TotalAmountInWords"];
                //ViewBag.TotalAmount = tblWordAmount.Rows[0]["GrandTotal"];
                //ViewBag.AmountWithoutTax = tblWordAmount.Rows[0]["NetTotal"];
                //ViewBag.SGSTAmount = tblWordAmount.Rows[0]["SGST"];
                //ViewBag.CGSTAmount = tblWordAmount.Rows[0]["CGST"];
                //ViewBag.IGSTAmount = tblWordAmount.Rows[0]["IGST"];
               ViewBag.TaxAmount = Convert.ToDouble(Convert.ToDouble(tblComDetails.Rows[0]["SGST"]) + Convert.ToDouble(tblComDetails.Rows[0]["CGST"]) + Convert.ToDouble(tblComDetails.Rows[0]["IGST"]));
            }

            
            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();
            ViewBag.HeadDetailsList = tblHeadDetails.AsEnumerable();
            ViewBag.ChargesDetailsList = tblChargesDetails.AsEnumerable();
 

            return PartialView();

        }

        public ActionResult GetCreditNoteSummary(string fromdate, string todate, string Search, string Assessno, string ddlGSTName)
        {
            DataTable GetCreditNoteSummary = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetCreditNoteSummary = db.sub_GetDatatable("USP_CREDIT_NOTE_SUMMARY_MVC '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm") + "','" + Search + "','" + Assessno + "','" + ddlGSTName + "'");
            Session["GetCreditNoteSummary"] = GetCreditNoteSummary;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            var json = JsonConvert.SerializeObject(GetCreditNoteSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        //Reverse Debit Note
        public ActionResult ReverseDebitNote()
        {
            List<BE.CreditNoteEntities> ModeGroup = new List<BE.CreditNoteEntities>();
            ModeGroup = trainTrackerProvider.getModeGroup();
            ViewBag.Mode = new SelectList(ModeGroup, "CATEGORYID", "CATEGORYType");
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<BE.PartyMaster> PartyMaster = new List<BE.PartyMaster>();
            PartyMaster = trainTrackerProvider.getPartyMaster();
            ViewBag.GSTName = new SelectList(PartyMaster, "GSTID", "GSTName");
            return View();

        }

        public JsonResult GetReverseDebitShow(string CreditNoteNo )
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.ReverseDebitNoteEntities> JOLIst = new List<BE.ReverseDebitNoteEntities>();
            JOLIst = trainTrackerProvider.GetReverseDebitShow(CreditNoteNo);
            return Json(JOLIst);
        }

        public JsonResult GatDebitGridNote(string Activity, string OldCredit, string WorkYear)
        {

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.CreditNoteEntities> JOLIst = new List<BE.CreditNoteEntities>();
            JOLIst = trainTrackerProvider.GatDebitGridNote(Activity, OldCredit, WorkYear);
            return Json(JOLIst);
        }

        public JsonResult GetTaxIDDebit(string PaidAmount, int lblTaxID, string statecode)
        {

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.CreditNoteCre> JOLIst = new List<BE.CreditNoteCre>();
            JOLIst = trainTrackerProvider.GetTaxIDDebit(PaidAmount, lblTaxID, statecode);
            return Json(JOLIst);
        }

        public JsonResult DeditSave(List<BE.CreditNoteAmountEntities> Debitdata, string DeditNoteNo, string OldCreditNOte, string DebitNoteDate, string Category, string PartyID, string GrandTotal, string remarks, string statecode)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("Workyear");
            dataTable.Columns.Add("AccountName");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("NetAmount");
            dataTable.Columns.Add("CreditedAmount");
            dataTable.Columns.Add("CreditAmount");
            dataTable.Columns.Add("AccountID");
            dataTable.Columns.Add("JONo");
            dataTable.Columns.Add("TaxID");

            foreach (BE.CreditNoteAmountEntities item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["InvoiceNo"] = item.InvoiceNo;
                row["Workyear"] = item.WorkYear;
                row["AccountName"] = item.accountname;
                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["NetAmount"] = item.nettotal;
                row["CreditedAmount"] = item.CreditedAmount;
                row["CreditAmount"] = item.creditamount;
                row["AccountID"] = item.AccountID;
                row["JONo"] = item.JONo;
                row["TaxID"] = item.TaxID;

                dataTable.Rows.Add(row);
            }

            message = trainTrackerProvider.DeditSave(dataTable, DeditNoteNo, OldCreditNOte, DebitNoteDate, Category, PartyID, GrandTotal, remarks, Userid, statecode);

            return Json(message);
        }

        public ActionResult GetDebitNoteSummary(string fromdate, string todate, string Search, string Assessno, string ddlGSTName)
        {
            DataTable GetCreditNoteSummary = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetCreditNoteSummary = db.sub_GetDatatable("USP_Debit_NOTE_SUMMARY '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm") + "','" + Search + "','" + Assessno + "','" + ddlGSTName + "'");
            Session["GetCreditNoteSummary"] = GetCreditNoteSummary;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            var json = JsonConvert.SerializeObject(GetCreditNoteSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
 
        public ActionResult ReverseCancelCreditNote()
        {
            return View();
        }
        public JsonResult GetCreditNoteList(String CrNote)
        {
             
            BE.TaxSettings lm = new BE.TaxSettings();
            lm = trainTrackerProvider.GetCreditNoteList(CrNote);

            return Json(lm);
            
        }
        public JsonResult SaveCreditNote(string dtFromdate,string CreditNoteNo, string Remarks)
        {

            int i = 0;
            string Messageget = "";
            DataTable dt = new DataTable();
            string strSql = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
           
            string WorkYear = "2021-22";
            strSql = "";
            strSql = "USP_SaveCreditNoteDetails'" + dtFromdate + "','" + WorkYear + "','" + CreditNoteNo + "','" + UserID + "','" + Remarks + "'";
            dt = db.sub_GetDatatable(strSql);
            if (dt.Rows.Count > 0)
            {
                Messageget = Convert.ToString(dt.Rows[0]["mgs"]);
            }

            return Json(Messageget);

        }
        // Prashant Kuppili
        //Self Audit
        public ActionResult SelfAudit()
        {
            List<BE.SelfAudit> ddlValues = new List<BE.SelfAudit>();
            ddlValues = trainTrackerProvider.getSelfAuditDDLValues();
            ViewBag.AuditOn = new SelectList(ddlValues, "SelfAuditID", "ValuesTypes");
            return View();
        }

     
        public ActionResult GetSelfAuditSummary(string AuditOn, string fromDate, string toDate, string FromValues, string ToValues)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();        
            dt = db.sub_GetDatatable("USP_SelfAudit '" + AuditOn + "' , '" + fromDate + "' , '" + toDate + "' , '" + FromValues + "' , '" + ToValues + "'");
            Session["USP_SelfAudit"] = dt;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        // Invoice Audit

        public ActionResult AuditTrail()
        {
            return View();
        }
        public ActionResult GetAuditTrailSummary(string fromDate, string toDate)
        {
            DataTable dt = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_Amend_Logs '" + fromDate + "' , '" + toDate + "' ");
            Session["USP_Amend_Logs"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExporttoExcelAuditTrail()
        {
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);


            DataTable AmendLogsDT = (DataTable)Session["USP_Amend_Logs"];
            GridView gridview = new GridView();
            gridview.DataSource = AmendLogsDT;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=AuditTrail.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>MT_Repo_Pendency<strong></td></tr>");

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
        public ActionResult RevisedInvoiceLog()
        {
            return View();
        }
        public ActionResult GetRevisedInvoiceLogsSummary(string fromDate, string toDate)
        {
            DataTable dt = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_ShowReviseddetails '" + fromDate + "' , '" + toDate + "' ");
            Session["USP_ShowReviseddetails"] = dt;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExporttoExcelRevisedInvoiceLog()
        {

            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);


            DataTable ShowReviseddetailsDT = (DataTable)Session["USP_ShowReviseddetails"];
            GridView gridview = new GridView();
            gridview.DataSource = ShowReviseddetailsDT;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=RevisedInvoiceLog.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>MT_Repo_Pendency<strong></td></tr>");

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


        public ActionResult InvoiceAudit()
        {
            List<BE.Audit_Invoice_Note> CategoryList = new List<BE.Audit_Invoice_Note>();
            CategoryList = trainTrackerProvider.GetCategoryList();
            ViewBag.Category = new SelectList(CategoryList, "CreditCategoryID", "CreditCategoryType");
            return View();
        }

        //Not in use as of now
        public ActionResult GetInvoiceResultForAll()
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            //dt = db.sub_GetDatatable("SP_AuditInvoiceSearchAll '" + category + "','" + Search + "','" + txtItemNo + "'");
            dt.Columns.Remove("View");
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult _InvoiceAuditForm(string InvoiceNo)
        {
            ViewBag.InvoiceNo = InvoiceNo;
            return PartialView();
        }

        public ActionResult SaveInvoiceAuditForm(BE.Audit_Invoice_Note InvoiceAuditForm)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.ResponseMessage responseMessage = new BE.ResponseMessage();
            responseMessage = trainTrackerProvider.SaveInvoiceAuditForm(InvoiceAuditForm, Userid);
            return Json(responseMessage);
        }

        public ActionResult GetInvoiceAuditSearch(string category, string Search, string txtItemNo)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_InvoiceAuditSearch '" + category + "','" + Search + "','" + txtItemNo + "'");
            dt.Columns.Remove("View");
            Session["USP_InvoiceAuditSearch"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult GetInvoiceAuditSearchByCategory(string category, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_InvoiceAuditSearchByCategory '" + category + "','" + FromDate + "','" + ToDate + "'");
            Session["USP_InvoiceAuditSearchByCategory"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelInvoiceAuditReport()
        {

            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);


            DataTable InvoiceAudit = (DataTable)Session["USP_InvoiceAuditSearch"];
            InvoiceAudit.Columns.Remove("View1");
            GridView gridview = new GridView();
            gridview.DataSource = InvoiceAudit;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=InvoiceAudit.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>MT_Repo_Pendency<strong></td></tr>");

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

        public ActionResult ExportToExcelInvoiceAuditWithCategory()
        {

            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);


            DataTable InvoiceAudit = (DataTable)Session["USP_InvoiceAuditSearchByCategory"];
            InvoiceAudit.Columns.Remove("View");
            GridView gridview = new GridView();
            gridview.DataSource = InvoiceAudit;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=InvoiceAudit.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>MT_Repo_Pendency<strong></td></tr>");

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

        //Update Additional Charges

        public ActionResult ImportUpdateChargesRemarks()
        {
            List<BE.ImportUpdateCharges> DDLaccountMaster = new List<BE.ImportUpdateCharges>();
            DDLaccountMaster = trainTrackerProvider.GetAccountListMaster();
            ViewBag.AccountHead = new SelectList(DDLaccountMaster, "AccountID", "AccountName");
            return View();
        }

        public ActionResult ShowTariffRemarkDetail(string SearchBy, string SearchDetails, string JO_Itemno)
        {
            DataTable dt = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("sp_getTarriffRemarksDetails '" + SearchBy + "' , '" + SearchDetails + "', '" + JO_Itemno + "'");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ShowJONoForDDL(string ContainerNo)
        {
            List<BE.ImportUpdateCharges> DDLaccountMaster = new List<BE.ImportUpdateCharges>();
            DDLaccountMaster = trainTrackerProvider.ShowJONoForDDL(ContainerNo);
            return Json(DDLaccountMaster);
        }

        public ActionResult GetAdditionalRemarksDetails(string ContainerNo, int JONo)
        {
            BE.ImportUpdateCharges remarkDetail = new BE.ImportUpdateCharges();
            remarkDetail = trainTrackerProvider.GetAdditionalRemarksDetails(ContainerNo, JONo);
            return Json(remarkDetail);
        }

        public ActionResult SaveRemarksData(BE.ImportUpdateCharges FormData)
        {
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            BE.ResponseMessage responseMessage = new BE.ResponseMessage();
            responseMessage = trainTrackerProvider.SaveRemarksData(FormData, UserID);
            return Json(responseMessage);

        }

        public ActionResult getTarriffRemarksDetailsSummary(string fromDate, string toDate)
        {
            DataTable dt = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("sp_getTarriffRemarksDetailsSummary  '" + fromDate + "' , '" + toDate + "'");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult getTarriffRemarksPendencySummary(string fromDate, string toDate)
        {
            DataTable dt = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("sp_getTarriffRemarksPendencySummary  '" + fromDate + "' , '" + toDate + "'");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

    }
}