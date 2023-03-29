using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CI = TrackerMVCEntities.BusinessEntities;
using CD = TrackerMVCDataLayer.Helper;
using DB = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using TrackerMVC.Filters;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Data.Sql;
using HC = TrackerMVCDataLayer.Helper;
using System.Web.UI.WebControls;
using System.Web.UI;
using TrackerMVCEntities.BusinessEntities;
 

namespace TrackerMVC.Controllers.EDICollections
{
    [UserAuthenticationFilter]
    public class EDICollectionsController : Controller
    {
        DB.EDICollections trackerdatamanager = new DB.EDICollections();
        CD.DBOperations db = new CD.DBOperations();
        // GET: EDICollections
        public ActionResult EDICollections()
        {
            List<CI.ActivityEntities> ActivityList = new List<CI.ActivityEntities>();
            List<CI.CHAEntities> CHAList = new List<CI.CHAEntities>();
            List<CI.ModeEntities> ModeList = new List<CI.ModeEntities>();
            List<CI.EDIBankEntities> EDIBankList = new List<CI.EDIBankEntities>();
            List<CI.EDIAccountHeads> EDIAccountList = new List<CI.EDIAccountHeads>();


            CI.EDICollections CollectionList = trackerdatamanager.GetExisitinchaname();
            //DB.EDICollections reportprovider = new DB.EDICollections();
            //List<CI.AcitvityEntities> InvoiceList = new List<CI.AcitvityEntities>();
            ActivityList = CollectionList.ActivityList;
            CHAList = CollectionList.CHAList;
            ModeList = CollectionList.ModeList;
            EDIBankList = CollectionList.EDIBankList;
            EDIAccountList = CollectionList.EDIAccountHeads;


            ViewBag.InvoiceList = new SelectList(ActivityList, "ID", "Criteria");                        
            ViewBag.chaList = new SelectList(CHAList, "id", "Criteria");
            ViewBag.MOdeList = new SelectList(ModeList, "paymodeID", "paymode");
            ViewBag.EDIBankList = new SelectList(EDIBankList, "bankID", "bankname");
            ViewBag.EDIAccountList = new SelectList(EDIAccountList, "Accountid", "Accountname");

            return View();
        }
        [HttpPost]
        public ActionResult SearchGST()
        {
            //return View();
            //return View(accountingCodeList);
            return PartialView();
        }
        
        public JsonResult CalculateAmount(int PageFrom,int AccountID, int State)
        {
            List<CI.EDICollections> AmountList = new List<CI.EDICollections>();
            AmountList = trackerdatamanager.GetAmount(PageFrom, AccountID, State);
            var jsonResult = Json(AmountList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult EDISave(List<CI.EDICollections> Receiptdata, List<CI.EDIImportExportDets> ImportExportdata, string InvoiceDate, string Activity, string CHAName,int GSTID,string PageFrom,string PageTo, string AccountID, string NetTotal, string SGST, string CGST, string IGST, string GrandTotal)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);

            DataTable dataTable1 = new DataTable();

            dataTable1.Columns.Add("SBNo");
            dataTable1.Columns.Add("SBDate");
            dataTable1.Columns.Add("BOENo");
            dataTable1.Columns.Add("BOEDate");

            foreach (CI.EDIImportExportDets item in ImportExportdata)
            {
                DataRow row = dataTable1.NewRow();

                row["SBNo"] = item.SBNo;
                row["SBDate"] = Convert.ToDateTime(item.SBDate).ToString("yyyy-MM-dd HH:mm");
                row["BOENo"] = item.BOENo;
                row["BOEDate"] = Convert.ToDateTime(item.BOEDate).ToString("yyyy-MM-dd HH:mm");

                dataTable1.Rows.Add(row);
            }

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ModeID");
            dataTable.Columns.Add("ModeAmount");
            dataTable.Columns.Add("ModeNo");
            dataTable.Columns.Add("BankName");
            dataTable.Columns.Add("ModeDate");

            foreach (CI.EDICollections item in Receiptdata)
            {
                DataRow row = dataTable.NewRow();

                row["ModeID"] = item.ModeID;
                row["ModeAmount"] = item.ModeAmount;
                row["ModeNo"] = item.ModeNo;
                row["BankName"] = item.BankName;
                row["ModeDate"] = Convert.ToDateTime(item.ModeDate).ToString("yyyy-MM-dd HH:mm");

                dataTable.Rows.Add(row);
            }
            CI.EDICollections CollectionList = trackerdatamanager.SaveEDI(dataTable,dataTable1, InvoiceDate, Activity,CHAName,GSTID,PageFrom, PageTo,AccountID,NetTotal,SGST,CGST,IGST,GrandTotal, UserID);

            var jsonResult = Json(CollectionList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult PrintEDI(int AssessNo, string WorkYear)
        {
            CI.EDICollections PT = new CI.EDICollections();
            PT = trackerdatamanager.GetPrintEDIList(AssessNo, WorkYear);
            List<CI.EDIPrintEntities> EDIReceipt = new List<CI.EDIPrintEntities>();
            EDIReceipt = PT.EDIPrintEntities;
            List<CI.EDIImportExportDetsPrint> EDIImportExportDets = new List<CI.EDIImportExportDetsPrint>();
            EDIImportExportDets = PT.EDIImportExportDetsPrint;
            ViewBag.EDIReceipt = PT.EDIPrintEntities.AsEnumerable();
            ViewBag.EDIImportExport = PT.EDIImportExportDetsPrint.AsEnumerable();
            ViewBag.AssessNo = PT.AssessNo;
            ViewBag.WorkYear = PT.WorkYear;
            ViewBag.Activity = PT.Activity;
            ViewBag.Con_Name = PT.Con_Name;
            ViewBag.AddressI = PT.AddressI;
            ViewBag.GSTIN = PT.GSTIN;
            //ViewBag.BOEDate = PT.BOEDate;
            ViewBag.CHA = PT.CHA;
            ViewBag.PartyName = PT.GSTName;
            ViewBag.PartyAddress = PT.GSTAddress;
            ViewBag.GSTNo = PT.GSTIn_uniqID;
            ViewBag.State = PT.State;
            ViewBag.AccountName = PT.AccountName;
            ViewBag.PageFrom = PT.PageFrom;
            ViewBag.PageTo = PT.PageTo;
            ViewBag.NetAmount = PT.NetTotal;
            ViewBag.SGST = PT.SGST;
            ViewBag.CGST = PT.CGST;
            ViewBag.IGST = PT.IGST;
            ViewBag.GrandTotal = PT.GrandTotal;
            ViewBag.InvoiceNO = PT.InvoiceNO;
            ViewBag.NoteI = PT.NoteI;
            ViewBag.NoteII = PT.NoteII;
            ViewBag.NoteIII = PT.NoteIII;
            ViewBag.NoteVI = PT.NoteVI;
            ViewBag.PANNo = PT.PANNo;
            ViewBag.ConFor = PT.ConFor;
            ViewBag.SignedQRcode = PT.SignedQRcode;
            ViewBag.AckNo = PT.AckNo;
            ViewBag.Irn = PT.Irn;
            ViewBag.AckDt = PT.AckDt;
            ViewBag.Registered = PT.Registered;
            ViewBag.UPINumber = PT.UPINumber;

            return PartialView();
        }
        public JsonResult EDISummary(string FromDate, string ToDate, string category, string Search1, string Search)
        {
            List<CI.EDICollections> AmountList = new List<CI.EDICollections>();
            AmountList = trackerdatamanager.GetEDISummary(FromDate, ToDate, category, Search1, Search);
            var jsonResult = Json(AmountList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult Search(string SearchText)
        {
            List<CI.EDISearchGST> GstList = new List<CI.EDISearchGST>();
            GstList = trackerdatamanager.GetGSTList(SearchText);
            var jsonResult = Json(GstList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetGstsearch(string SearchText)
        {
            List<CI.EDISearchGST> trainList = new List<CI.EDISearchGST>();
            trainList = trackerdatamanager.GetGSTList(SearchText);
            return Json(trainList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSBBOEData(string Category, string SBNo, string BOENo)
        {
            List<CI.EDICollections> SBBOEList = new List<CI.EDICollections>();
            SBBOEList = trackerdatamanager.GetSBBOEList(Category,SBNo,BOENo);
            var jsonResult = Json(SBBOEList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult CancelEDI(CI.EDICollections AM)        {            CD.DBOperations db = new CD.DBOperations();            DataTable dataTable = new DataTable();            int UserID = Convert.ToInt16(Session["Tracker_userID"]);            dataTable = db.sub_GetDatatable("USP_CANCEL_EDI_OPTION '" + AM.AssessNo + "','" + AM.WorkYear + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");            CI.ResponseMessage response = new CI.ResponseMessage();            if (dataTable != null)            {                foreach (DataRow row in dataTable.Rows)                {                    response.Status = Convert.ToInt32(row["Status"]);                    response.Message = Convert.ToString(row["message"]);                }            }            return Json(response);        }

        public JsonResult ValidateVendor(string category, string Search)
        {
            DataTable dataTable = new DataTable();
            string Message = "";
            try
            {
                CD.DBOperations db = new CD.DBOperations();
                dataTable = db.sub_GetDatatable("usp_validtion_EDIS '" + category + "','" + Search +  "'");

                if (dataTable != null)
                {
                    Message = dataTable.Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                Message = "";
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DailyCollection()
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dsList = new DataTable();

            dsList = db.sub_GetDatatable("get_sp_today_EDI_collection");

            var listData = JsonConvert.SerializeObject(dsList);

            var jsonResult = Json(listData, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult DailyEDICollectionReoprt()
        {
            return View();
        }

        public ActionResult getDailyCollection(string Criteria, string FromDate, string ToDate)
        {
            DataTable getMovementICDNew = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("GetDailyCollection_EDI  '" + FromDate + "','" + ToDate + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["GetDailyCollection"] = getMovementICDNew;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

       

        public ActionResult ExportTransport()
        {
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dtFCLDestuffTallyList = (DataTable)Session["GetDailyCollection"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=EDI Summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>EDISummary<strong></td></tr>");

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

    }
}