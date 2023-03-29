using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using train = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrainMaster;
using BE = TrackerMVCEntities.BusinessEntities;
using EN = TrackerMVCEntities.BusinessEntities;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Report;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using TrackerMVC.Filters;
using System.Data;
using System.IO;
using System.Data.Sql;
using HC = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using CD = TrackerMVCDataLayer.Helper;
using System.Web.UI;
using TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using System.Data.OleDb;
using System.Text;

namespace TrackerMVC.Controllers.Report
{
    [UserAuthenticationFilter]
    public class ReportController : Controller
    {
        train.UpdateDischargeDate trainTrackerProvider = new train.UpdateDischargeDate();
        BM.BLDataManager.GenerateBL BL = new BM.BLDataManager.GenerateBL();
        BM.LoadingPlan.LoadingPlan LP = new BM.LoadingPlan.LoadingPlan();
        
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        RP.Report reportprovider = new RP.Report();
        public ActionResult PendingContainerAgainZeroJo()
        {
            List<BE.PendingContainerAgainZeroJo> pendingContainer = new List<BE.PendingContainerAgainZeroJo>();
            pendingContainer = reportprovider.GetpendingContainer();
            return View(pendingContainer);
        }
        public ActionResult PendingContainerEmptyOffloading()
        {
            List<BE.PendingContainerForEmptyOffLoading> pendingContainerforempty = new List<BE.PendingContainerForEmptyOffLoading>();
            pendingContainerforempty = reportprovider.GetImportInList();
            return View(pendingContainerforempty);

        }

        public ActionResult PendingContainerForJOUpdation()
        {
            List<BE.PendingContainersForJoUpdationsEntities> pendingContainer = new List<BE.PendingContainersForJoUpdationsEntities>();
            pendingContainer = reportprovider.GetPendingContainerForJOUpdation();
            return View(pendingContainer);
        }

        public ActionResult ExportReMovementReport()
        {
            return View();
        }

        public ActionResult ExportCustomReport()
        {
            return View();
        }

        public ActionResult ListofPendingDoReport()
        {
            return View();
        }

        public ActionResult InvoiceRevenueReport()
        {
            List<BE.ShipLines> LineList = new List<BE.ShipLines>();
            List<BE.CHA> CHAList = new List<BE.CHA>();
            List<BE.Consignee> ImporterMasterList = new List<BE.Consignee>();

            LineList = BL.getShipLines();
            CHAList = BL.getCHA();
            ImporterMasterList = BL.getConsignee();

            ViewBag.Line = new SelectList(LineList, "SLID", "SLName");
            ViewBag.CHA = new SelectList(CHAList, "CHANo", "CHAName");
            ViewBag.Importer = new SelectList(ImporterMasterList, "ImporterID", "ImporterName");

            return View();
        }

        public ActionResult CategoryWiseInventory()
        {

            return View();
        }

        public ActionResult WaraiReport()
        {
            return View();
        }

        public ActionResult ExportLoadedOut()
        {
            return View();
        }

        public ActionResult ExportStuffingReport()
        {
            return View();
        }

        public ActionResult AuctionUCCReport()
        {
            return View();
        }


        public ActionResult IGMSummeryDetails()
        {

            return View();
        }

        public ActionResult ImportLoadedInventory()
        {
            return View();
        }

        public ActionResult CreditReceiptReoprt()
        {
            return View();
        }

        public ActionResult OtherInvoiceSummary()
        {
            return View();
        }



        public JsonResult getExportOtherDetailsReport(string FromDate, string ToDate)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            ImpFinalOut = db.sub_GetDatatable("USP_GET_OTHER_INV_SUMMARY_DETAILS '" + FromDate + "','" + ToDate + "'");

            Session["ExpCustomRep"] = ImpFinalOut;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelExportOtherInvoiceReport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["ExpCustomRep"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Others_invoice_details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Other Invoice Summary<strong></td></tr>");
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



        [HttpPost]
        public ActionResult getInventory(string AsonDate)
        {
            //List<BE.PendingContainersForJoUpdationsEntities> InventoryList = new List<BE.PendingContainersForJoUpdationsEntities>();
            //InventoryList = reportprovider.GetInventoryList(AsonDate);
            //// ViewBag.InventoryDL = InventoryList;
            //return Json(InventoryList, JsonRequestBehavior.AllowGet);
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("sp_categorywiseinventory '" + Convert.ToDateTime(AsonDate).ToString("yyyyMMddHHmm") + "','" + Convert.ToDateTime(AsonDate).ToString("dd-MMM-yyyy HH:mm") + "'");
            Session["CategoryInventory"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult teusSearchStocks(string AsonDate)
        {
            List<BE.TeusSearch> SearchInvoice = new List<BE.TeusSearch>();

            SearchInvoice = BL.teusSearchLoadeds(AsonDate);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult getInventoryAll(string AsonDate)
        {
            //List<BE.PendingContainersForJoUpdationsEntities> InventoryList = new List<BE.PendingContainersForJoUpdationsEntities>();
            //InventoryList = reportprovider.GetInventoryList(AsonDate);
            //// ViewBag.InventoryDL = InventoryList;
            //return Json(InventoryList, JsonRequestBehavior.AllowGet);
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("sp_categorywiseinventory_All_Yard '" + Convert.ToDateTime(AsonDate).ToString("yyyyMMddHHmm") + "','" + Convert.ToDateTime(AsonDate).ToString("dd-MMM-yyyy HH:mm") + "'");
            Session["sp_categorywiseinventory_All_Yard"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult teusSearchStockAll(string AsonDate)
        {
            List<BE.TeusSearch> SearchInvoice = new List<BE.TeusSearch>();

            SearchInvoice = BL.teusSearchLoadedAll(AsonDate);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult MovementAtICD()
        {
            List<BE.CriteriaEntities> CriteriaList = new List<BE.CriteriaEntities>();
            CriteriaList = reportprovider.GetCriteria();
            ViewBag.CriteriaList = new SelectList(CriteriaList, "ID", "Criteria");
            return View();
        }

        public ActionResult DailyCollectionReoprt()
        {
            return View();
        }

        public ActionResult ExportDailyCollectionReoprt()
        {
            return View();
        }

        [HttpPost]
        public ActionResult getMovementICD(string Criteria, string FromDate, string ToDate)
        {
            List<BE.MovementAtICD> MovementAtICDList = new List<BE.MovementAtICD>();
            MovementAtICDList = reportprovider.GetMovementAtICD(Criteria, FromDate, ToDate);

            var jsonResult = Json(MovementAtICDList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            //return Json(MovementAtICDList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getMovementICDNew(string Criteria, string FromDate, string ToDate)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("usp_MovementAtICD  '" + FromDate + "','" + ToDate + "','" + Criteria + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["getMovementICDNew"] = getMovementICDNew;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult getRevenueSummary(string FromDate, string ToDate,string CustomerType,string JoType, string CustomerId)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("usp_getImportHeadWise_SummaryNew  '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm")  + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + CustomerType + "','" + JoType + "','" + CustomerId + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["usp_getImportHeadWise"] = getMovementICDNew;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelMovementIcd(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["getMovementICDNew"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=MovementAtIcdReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Movement At ICD Report<strong></td></tr>");
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

        public ActionResult ExportToExcelRevenueReport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["usp_getImportHeadWise"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=MovementAtIcdReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Container Wise Report<strong></td></tr>");
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

        public ActionResult ExportToExcelCategoryWiseInventory(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["CategoryInventory"];
            DataTable getMovementICDNews = (DataTable)Session["sp_categorywiseinventory_All_Yard"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();
            if (getMovementICDNews != null)
            {
                gridview.DataSource = getMovementICDNews;
                gridview.DataBind();
            }

           

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=CategoryWiseInventory.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Category Wise Inventory Report<strong></td></tr>");
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

        public ActionResult getDailyCollection(string Criteria, string FromDate, string ToDate)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("GetDailyCollection  '" + FromDate + "','" + ToDate + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["GetDailyCollection"] = getMovementICDNew;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult getDailyCollectionExport(string Criteria, string FromDate, string ToDate)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("GetDailyCollectionExport  '" + FromDate + "','" + ToDate + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["GetDailyCollectionExport"] = getMovementICDNew;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult DailyCollectionReport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["GetDailyCollection"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=DailyCollection.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Daily Collection Report<strong></td></tr>");
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

        public ActionResult DailyCollectionReportExport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["GetDailyCollectionExport"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExportDailyCollection.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Daily Collection Report<strong></td></tr>");
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

        // Codes by Prashant
        public ActionResult GetConsigneeWiseList()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ajaxGetConsigneeWiseList(string FromDate, string ToDate)
        {
            List<BE.ConsigneeEntities> MovementAtICDList = new List<BE.ConsigneeEntities>();
            MovementAtICDList = reportprovider.GetConsigneeDetails(FromDate, ToDate);

            var jsonResult = Json(MovementAtICDList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        // Codes By Prashant 01 July 2019
        public ActionResult EmptyYardGateINSummary()
        {
            List<BE.CriteriaEntities> CriteriaList = new List<BE.CriteriaEntities>();
            CriteriaList = reportprovider.GetCriteria();
            ViewBag.CriteriaList = new SelectList(CriteriaList, "ID", "Criteria");

            return View();
        }
        [HttpPost]
        public JsonResult AjaxGetEmptyGateDetails(string fromdate, string TOdate, string Search, string name)
        {
            List<BE.EmptyYardGateINSummaryEntities> EmptyDateDetailsList = new List<BE.EmptyYardGateINSummaryEntities>();
            EmptyDateDetailsList = reportprovider.GetEmptyAddGateInDetails(fromdate, TOdate, Search, name);

            var jsonResult = Json(EmptyDateDetailsList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult EmptyYardGateOutSummary()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxGetEmptyGateOutDetails(string fromdate, string TOdate, string Search, string name)
        {
            List<BE.EmptyYardGateOutSummaryEntities> EmptyDateoutDetailsList = new List<BE.EmptyYardGateOutSummaryEntities>();
            EmptyDateoutDetailsList = reportprovider.GetEmptyAddGateOutDetails(fromdate, TOdate, Search, name);
            DataTable EmptyDateoutDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            EmptyDateoutDetails = db.sub_GetDatatable("Get_Sp_EyardoUTDetails '" + fromdate + "','" + TOdate + "','" + Search + "','" + name + "'");
            Session["EmptyDateoutDetails"] = EmptyDateoutDetails;
            Session["fromdate"] = fromdate;
            Session["todate"] = TOdate;
            var jsonResult = Json(EmptyDateoutDetailsList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelEmptyYardGateOutSummary(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable EmptyDateoutDetails = (DataTable)Session["EmptyDateoutDetails"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = EmptyDateoutDetails;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=EmptyyardGateOutReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Empty Yard Gate out Summary<strong></td></tr>");
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


        // Codes By Prashant 02 july 2019
        public ActionResult ExportEmptyGateInSummary()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxExportGetEmptyGateINDetails(string fromdate, string TOdate, string Search, string search1)
        {
            List<BE.ExportEmptyGateInSummaryEntities> ExportEmptyDateInDetailsList = new List<BE.ExportEmptyGateInSummaryEntities>();
            ExportEmptyDateInDetailsList = reportprovider.GetExportEmptyAddGateINDetails(fromdate, TOdate, Search, search1);

            var jsonResult = Json(ExportEmptyDateInDetailsList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }




        [HttpPost]
        public JsonResult AjaxSeachWiseShippingorAccountname(string SearchCriteria)
        {
            List<BE.ShipLines> ShippingorAccountnameList = new List<BE.ShipLines>();
            ShippingorAccountnameList = reportprovider.getGetShippingName(SearchCriteria);
            return new JsonResult() { Data = ShippingorAccountnameList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult ExportLoadedGateInSummary()
        {

            return View();
        }
        [HttpPost]
        public JsonResult AjaxExportLoadedGateINDetails(string fromdate, string TOdate, string Search, string search1, string Search2)
        {
            List<BE.ExportLoadedGateInSummaryEntities> ExportLoadedDateInDetailsList = new List<BE.ExportLoadedGateInSummaryEntities>();
            ExportLoadedDateInDetailsList = reportprovider.GetExportLoadedAddGateINDetails(fromdate, TOdate, Search, search1, Search2);

            var jsonResult = Json(ExportLoadedDateInDetailsList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        // Codes End By Prashant


        public ActionResult TrackContainerList()
        {
            return View();
        }

        public ActionResult getContainerSearchList(string ContainerNo)
        {
            int userid = Convert.ToInt32(Session["Tracker_userID"]);
            HC.DBOperations db = new HC.DBOperations();
            db.sub_ExecuteNonQuery("usp_Insert_SearchContainer '" + ContainerNo + "','" + userid + "'");
            List<BE.JobOrderDEntities> ContainerSearchList = new List<BE.JobOrderDEntities>();
            ContainerSearchList = reportprovider.getContainerSearchList(ContainerNo);
            return Json(ContainerSearchList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IMPSearchTimeline(string Process, string ContainerNo, string Jono)
        {
            DataTable GetMonthSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            GetMonthSummary = db.sub_GetDatatable("USP_IMP_Search_Timeline '" + Process + "','" + Jono + "','" + ContainerNo + "'");
            //Session["SP_ImpInDayWise_Summary"] = GetMonthSummary;
            Session["Ason"] = DateTime.Now;
            var json = JsonConvert.SerializeObject(GetMonthSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        public ActionResult ImportTimeline(string ContainerNo, string Jono)
        {
            string ContainerNumber = Convert.ToString(ContainerNo);
            string Jonumber = Convert.ToString(Jono);
            List<BE.ImportSearchEntities> TimeLineList = new List<BE.ImportSearchEntities>();
            TimeLineList = trainTrackerProvider.GetTimeLine(Jonumber, ContainerNumber);
            return PartialView(TimeLineList.ToList());
        }
        public JsonResult ajaxImportSearchlistdetails(string ContainerNo, string Jono)
        {
            string ContainerNumber = Convert.ToString(ContainerNo);
            string Jonumber = Convert.ToString(Jono);

            BE.ImportSearchEntities ImportSearchdetails = new BE.ImportSearchEntities();
            ImportSearchdetails = trainTrackerProvider.GetImportSearchDetailsS(ContainerNumber, Jonumber);

            List<BE.GetIGMDetailsOnJONO> ImportSearchdetailsList = new List<BE.GetIGMDetailsOnJONO>();
            ImportSearchdetailsList = trainTrackerProvider.SearchImportSearchList(Jonumber, ContainerNumber);

            //List<BE.GetIGMDetailsOnJONO> TimeLineList = new List<BE.GetIGMDetailsOnJONO>();
            //TimeLineList = trainTrackerProvider.GetTimeLine(Jonumber, ContainerNumber);
            //if (TimeLineList is null)
            //{
            //    ViewBag.TimeLineList = 0;
            //}
            //ViewBag.TimeLineList = TimeLineList;

            var returnField = new { ImportList = ImportSearchdetails, ImportDetails = ImportSearchdetailsList };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult GetIGMDeatails(string Jono, string ContainerNo)
        {

            DataTable GetMonthSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            GetMonthSummary = db.sub_GetDatatable("sp_GetIGMDetailsOn_JOConts2  '" + Jono + "','" + ContainerNo + "'");
            //Session["SP_ImpInDayWise_Summary"] = GetMonthSummary;
            Session["Ason"] = DateTime.Now;
            var json = JsonConvert.SerializeObject(GetMonthSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult GetIGMInvoiceDeatails(string Jono, string ContainerNo)
        {

            DataTable GetMonthSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            GetMonthSummary = db.sub_GetDatatable("usp_showAssessdetails  '" + Jono + "','" + ContainerNo + "'");
            //Session["SP_ImpInDayWise_Summary"] = GetMonthSummary;
            Session["Ason"] = DateTime.Now;
            var json = JsonConvert.SerializeObject(GetMonthSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult GetIGMHoldDeatails(string Jono, string ContainerNo)
        {

            DataTable GetMonthSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            GetMonthSummary = db.sub_GetDatatable("usp_Holddet  '" + Jono + "','" + ContainerNo + "'");
            //Session["SP_ImpInDayWise_Summary"] = GetMonthSummary;
            Session["Ason"] = DateTime.Now;
            var json = JsonConvert.SerializeObject(GetMonthSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public JsonResult ajaxJoNoList(string ContainerNo3)
        {
            List<BE.JobOrderDEntities> Shippers = new List<BE.JobOrderDEntities>();
            Shippers = trainTrackerProvider.getjoborderlist(ContainerNo3);
            return new JsonResult() { Data = Shippers, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //by durga

        public ActionResult GSTReturnSummary()
        {
            //List<BE.GSTReturnEntities> GSTList = new List<BE.GSTReturnEntities>();
            //GSTList=reportprovider.getGSTReturnReport()
            return View();

        }

        public ActionResult getGSTReturnList(string FromDate, string ToDate)
        {
            //List<BE.GSTReturnEntities> GSTReturnList = new List<BE.GSTReturnEntities>();
           // GSTReturnList = reportprovider.getGSTReturnList(FromDate, ToDate);
            //Session["GSTReturnList"] = GSTReturnList;
            //Session["fromdate"] = FromDate;
            //Session["todate"] = ToDate;
            //var jsonResult = Json(GSTReturnList, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
            DataTable getGSTReturnList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getGSTReturnList = db.sub_GetDatatable("USP_GST_RETUN_SUMMARY '" + FromDate + "','" + ToDate + "'");
            Session["getGSTReturnList"] = getGSTReturnList;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(getGSTReturnList);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelGSTReturnSummary(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getGSTReturnList = (DataTable)Session["getGSTReturnList"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getGSTReturnList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=GST Return List.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Gst Return Credit Note Summary<strong></td></tr>");
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


        public ActionResult CodecoStatus()
        {
            BM.BLDataManager.GenerateBL BL = new BM.BLDataManager.GenerateBL();
            List<BE.ShipLines> ShipLines = new List<BE.ShipLines>();
            ShipLines = reportprovider.getShipLinesForCodeco();
            ViewBag.ShipLines = new SelectList(ShipLines, "SLID", "SLCode");
            return View();
        }
        public ActionResult getCodecoStatusList(string FromDate, string ToDate, string ShipLines, string Event, string ContainerNo, string SearchCriteria)
        {
            List<BE.CodecoStatusEntties> CodecoStatusList = new List<BE.CodecoStatusEntties>();
            CodecoStatusList = reportprovider.getCodecoStatusList(FromDate, ToDate, ShipLines, Event, ContainerNo, SearchCriteria);
            return Json(CodecoStatusList, JsonRequestBehavior.AllowGet);

        }
        //end by durga

        // Codes By Manish

        public ActionResult TrackLinewiseTuesReport()
        {
            return View();
        }

        public ActionResult GetTrackLinewiseTuesReport(BE.LinewiseTuesReport data)
        {
            List<BE.LinewiseTuesReport> LinewiseTuesReport = new List<BE.LinewiseTuesReport>();
            LinewiseTuesReport = reportprovider.getLineWiseTuesReportList(data.Activity, data.date,data.Yard);
            return Json(LinewiseTuesReport, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LineWiseReportSummary()
        {
            return View("TrackLinewiseTuesReportSummary");
        }

        public ActionResult GetLineWiseReport(BE.LinewiseTuesReport data)
        {
            List<BE.LinewiseTuesReport> LinewiseTuesReport = new List<BE.LinewiseTuesReport>();
            LinewiseTuesReport = reportprovider.getLineWiseReportList(data.Activity, data.FromDate,data.ToDate, data.Yard);
            return Json(LinewiseTuesReport, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TrackPortPendencyTuesReport()
        {
            return View();
        }

        public ActionResult GetTrackPortTuesReport(BE.PortWiseTuesReport data)
        {
            List<BE.PortWiseTuesReport> PortWiseTuesReport = new List<BE.PortWiseTuesReport>();
            PortWiseTuesReport = reportprovider.getportTuesReportList(data.Activity);
            return Json(PortWiseTuesReport, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalesRegisterReport()
        {
            return View();
        }


        public ActionResult GetTrackSalesRegisterReport(BE.SalesRegisterrReport data)
        {
           // List<BE.SalesRegisterrReport> SalesRegisterReport = new List<BE.SalesRegisterrReport>();
            //SalesRegisterReport = reportprovider.getSalesRegisterrReportList(data.Activity, data.Fdate, data.Todate);
            // return Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            //var jsonResult = Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
            DataTable SalesRegisterReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            if (data.Activity== "CanceledCTR")
            {
                SalesRegisterReport = db.sub_GetDatatable("USP_Container_Wise_Cancel '" + data.Fdate + "','" + data.Todate + "'");
            }
            else
            {
                SalesRegisterReport = db.sub_GetDatatable("SP_SalesRegister1 '" + data.Activity + "','" + data.Fdate + "','" + data.Todate + "','" +data.GSTIn_uniqID+  "'");
            }
           
            Session["SalesRegisterReport"] = SalesRegisterReport;
            Session["fromdate"] = data.Fdate;
            Session["todate"] = data.Todate;
            var json = JsonConvert.SerializeObject(SalesRegisterReport);
            int i = 0;
            int total = 0;
            for (i= 0; i < json.Length; i++)
            {
                //total = total + js
            }
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult GetTrackSalesRegisterReportTextBoxbind(BE.SalesRegisterrReport data)
        {
            List<BE.SalesRegisterrReport> SalesRegisterReport = new List<BE.SalesRegisterrReport>();
            SalesRegisterReport = reportprovider.getSalesRegisterrReportList(data.Activity, data.Fdate, data.Todate);
            var jsonResult = Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelSalesRegisterReport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["SalesRegisterReport"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=SalesRegisterReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Sales Register Report<strong></td></tr>");
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

        public ActionResult FuelStockSummary()
        {
            List<BE.fuelType> fuelType = new List<BE.fuelType>();
            fuelType = trainTrackerProvider.getfuelType();
            ViewBag.FuelType = new SelectList(fuelType, "Fuelid", "FuelType");
            return View();
            //return View();
        }
        [HttpPost]
        public ActionResult GetFuelStockSummary(string cmbFueltype, string fromdate, string ToDate)
        {
            List<BE.FuelStockSummary> FuelStockSummary = new List<BE.FuelStockSummary>();

            FuelStockSummary = reportprovider.GetFuelStockSummary(cmbFueltype, fromdate, ToDate);
            string costcenter = "";
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_fuel_stock_summary '" + fromdate + "','" + ToDate + "','" + costcenter + "','" + cmbFueltype + "'");
            dt.Columns.Remove("fuelregid");
            dt.Columns.Remove("btnClass");
            Session["FuelStockSummary"] = dt;
            var jsonResult = Json(FuelStockSummary, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        
        // Codes by Manish

        public ActionResult SalesRegisterContainerWiseReport()
        {
            return View();
        }

        public ActionResult GetTrackSalesRegisterContainerWiseReport(BE.SalesRegisterrReport data)
        {
            List<BE.SalesRegisterContainerWiseReport> SalesRegisterContainerWiseReport = new List<BE.SalesRegisterContainerWiseReport>();
            SalesRegisterContainerWiseReport = reportprovider.getSalesRegisterrContainerWiseReportList(data.Activity, data.Fdate, data.Todate);
            // return Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(SalesRegisterContainerWiseReport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult ExportStuffingPlanReport()
        {
            return View();
        }


        public ActionResult GetTrackExportStuffingPlanReport(BE.ExportStuffingPlanReport data)
        {
            List<BE.ExportStuffingPlanReport> ExportStuffingPlanReport = new List<BE.ExportStuffingPlanReport>();
            ExportStuffingPlanReport = reportprovider.getExportStuffingPlanReport(data.AsOnDate);
            // return Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(ExportStuffingPlanReport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult EmptyBookingReport()
        {
            return View();
        }

        public ActionResult GetTrackEmptyBookingReport(BE.EmptyBookingReport data)
        {
            List<BE.EmptyBookingReport> EmptyBookingReport = new List<BE.EmptyBookingReport>();
            EmptyBookingReport = reportprovider.getEmptyBookingReport(data.FromDate, data.Todate, data.category, data.search);
            // return Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(EmptyBookingReport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult LorryReceiptReport()
        {
            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = LP.getActivity();
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");
            return View();
        }




        //public ActionResult GetTrackLorryReceiptReport(BE.LorryReceiptReport data)
        //{
        //    List<BE.LorryReceiptReport> LorryReceiptReport = new List<BE.LorryReceiptReport>();

        //    LorryReceiptReport = reportprovider.getLorryReceiptReport(data.FromDate, data.ToDate, data.category);
        //    // return Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
        //    var jsonResult = Json(LorryReceiptReport, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}


        public ActionResult GetTrackLorryReceiptReport(string FromDate, string ToDate, string category, string Size)
        {
            DataTable GetLorryReceipt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetLorryReceipt = db.sub_GetDatatable("SP_ShowLorryReceiptReports '" + FromDate + "','" + ToDate + "','" + category + "','" + Size + "'");
            Session["GetLorryReceipt"] = GetLorryReceipt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(GetLorryReceipt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;



        }

        public ActionResult ExportToExcelLorryreceiptdetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["GetLorryReceipt"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Lorryreceiptdetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Lorry Receipt Details<strong></td></tr>");
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


        public ActionResult ExportEmptyOutReport()
        {
            List<BE.CustomerMaster> CustomerMaster = new List<BE.CustomerMaster>();
            CustomerMaster = reportprovider.getCustomerMaster();
            ViewBag.customer = new SelectList(CustomerMaster, "AGID", "AGName");
            return View();
        }

        public ActionResult GetTrackExportEmptyOutReport(BE.ExportEmptyOutReport data)
        {
            List<BE.ExportEmptyOutReport> ExportEmptyOutReport = new List<BE.ExportEmptyOutReport>();

            ExportEmptyOutReport = reportprovider.getExportEmptyOutReport(data.FromDate, data.ToDate, data.Customer);
            // return Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(ExportEmptyOutReport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult SalesRegisterContainerWiseHeadReport()
        {
            return View();
        }

        public ActionResult GetTrackSalesRegisterContainerWiseHeadReport(BE.EmptyBookingReport data)
        {
            List<BE.SalesRegisterContainerWiseHeadReport> SalesRegisterContainerWiseHeadReport = new List<BE.SalesRegisterContainerWiseHeadReport>();
            SalesRegisterContainerWiseHeadReport = reportprovider.getSalesRegisterContainerWiseHeadReport(data.FromDate, data.Todate);
            // return Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(SalesRegisterContainerWiseHeadReport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportOutReport()
        {
            List<BE.CustomerMaster> CustomerMaster = new List<BE.CustomerMaster>();
            CustomerMaster = reportprovider.getCustomerMaster();
            ViewBag.customer = new SelectList(CustomerMaster, "AGID", "AGName");
            return View();
            //return View();
        }
        public ActionResult GetEmptyOutreport(string Customername, string fromdate, string todate)
        {
            List<BE.ExportOutReport> EmptyOutReport = new List<BE.ExportOutReport>();

            EmptyOutReport = reportprovider.GetExportOutReport(Customername, fromdate, todate);
            DataTable EmptyOutReportExcel = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            EmptyOutReportExcel = db.sub_GetDatatable("USP_ShowExportOutdetails '" + fromdate + "','" + todate + "','" + Customername + "'");
            EmptyOutReportExcel.Columns.Remove("Sr No");
            Session["EmptyOutReportExcel"] = EmptyOutReportExcel;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;

            var jsonResult = Json(EmptyOutReport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelGetEmptyOutreport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable EmptyOutReportExcel = (DataTable)Session["EmptyOutReportExcel"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            DataTable Summarry = new DataTable();
            //HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            Summarry = db.sub_GetDatatable("Export_PortWise_movement_tues_report'" + Session["fromdate"] + "','" + Session["todate"] + "'");
            GridView gridview = new GridView();
            GridView gridview1 = new GridView();
            GridView gridview2 = new GridView();
            gridview1.DataSource = EmptyOutReportExcel;
            gridview1.DataBind();
            gridview2.DataSource = Summarry;
            gridview2.DataBind();
            if (gridview1.Rows.Count != 0)
            {
                PrepareForExport(gridview1);
            }
            if (gridview2.Rows.Count != 0)
            {
                PrepareForExport(gridview2);

            }

            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Controls.Add(gridview1);

            tr1.Cells.Add(cell1);

            TableCell cell3 = new TableCell();

            cell3.Controls.Add(gridview2);

            TableCell cell2 = new TableCell();

            cell2.Text = " ";


            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);

            TableRow tr3 = new TableRow();

            tr3.Cells.Add(cell3);

            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.Rows.Add(tr3);

            //}


            //DataTable dt = new DataTable();
            //foreach (System.Web.UI.WebControls.TableCell cell in gridview1.HeaderRow.Cells)
            //{
            //    dt.Columns.Add(cell.Text);
            //}
            //foreach (GridViewRow row in gridview1.Rows)
            //{
            //    dt.Rows.Add();
            //    for (int j = 0; j < row.Cells.Count; j++)
            //    {
            //        dt.Rows[dt.Rows.Count - 1][j] = row.Cells[j].Text;
            //    }
            //}
            //foreach (GridViewRow row in gridview2.Rows)
            //{
            //    dt.Rows.Add();
            //    for (int j = 0; j < row.Cells.Count; j++)
            //    {
            //        dt.Rows[dt.Rows.Count - 1][j] = row.Cells[j].Text;
            //    }
            //}
            //gridview.DataSource = dt;
            //gridview.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExportLoadedOutReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Loaded Out Report<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    tb.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        protected void PrepareForExport(GridView Gridview)
        {

            // Gridview.AllowPaging = Convert.ToBoolean(rbPaging.SelectedItem.Value);

            Gridview.AllowPaging = false;
            //GridView2.AllowPaging = false;
            //btnview_Click1(this, null);
            // Gridview.DataBind();



            //Change the Header Row back to white color

            Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF");



            //Apply style to Individual Cells

            for (int k = 0; k < Gridview.HeaderRow.Cells.Count; k++)
            {

                Gridview.HeaderRow.Cells[k].Style.Add("background-color", "yellow");

            }



            for (int i = 0; i < Gridview.Rows.Count; i++)
            {

                GridViewRow row = Gridview.Rows[i];

                row.BackColor = System.Drawing.Color.White;

                row.Attributes.Add("class", "textmode");

                if (i % 2 != 0)
                {

                    for (int j = 0; j < Gridview.Rows[i].Cells.Count; j++)
                    {

                        row.Cells[j].Style.Add("background-color", "#C2D69B");

                    }

                }

            }

        }


        public ActionResult TrackExportMovementTracking()
        {
            List<BE.Shippers> Shippers = new List<BE.Shippers>();
            List<BE.Transport> Transport = new List<BE.Transport>();
            List<BE.Ports> Ports = new List<BE.Ports>();
            List<BE.CHA> CHA = new List<BE.CHA>();
            Shippers = BL.getShippers();
            CHA = BL.getCHA();
            Transport = BL.getTransport();
            Ports = BL.getPorts();
            ViewBag.Shippers = new SelectList(Shippers, "shipperID", "shippername");
            ViewBag.CHA = new SelectList(CHA, "CHANo", "CHAName");
            ViewBag.Ports = new SelectList(Ports, "PortID", "PortName");
            ViewBag.Transport = new SelectList(Transport, "Transport_Type_ID", "Transport_Type");
            return View();
        }

        public ActionResult GetExportMovementTracking(string AsOnDate)
        {
            DataTable GetExportMovementTracking = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetExportMovementTracking = db.sub_GetDatatable("SP_PendingMovement_NEW '" + Convert.ToDateTime(AsOnDate).ToString("yyyy-MM-dd HH:mm") + "',''");
            var json = JsonConvert.SerializeObject(GetExportMovementTracking);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult GetExportPortPendency()
        {
            DataTable GetExportMovementTracking = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetExportMovementTracking = db.sub_GetDatatable("SP_ExpPendancy_NEW ");
            var json = JsonConvert.SerializeObject(GetExportMovementTracking);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult GetTotalContainerPendency(string searchcerteria, string CMBName)
        {
            DataTable GetExportMovementTracking = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetExportMovementTracking = db.sub_GetDatatable("USP_SHOWCONTAINERPENDENCY '" + searchcerteria + "','" + CMBName + "'");
            Session["SHOWCONTAINERPENDENCY"] = GetExportMovementTracking;
            Session["searchcerteria"] = searchcerteria;
            Session["CMBName"] = CMBName;
            List<BE.ContainerCountPendency> Containerdetails = new List<ContainerCountPendency>();

            if (GetExportMovementTracking != null)
            {
                foreach (DataRow row in GetExportMovementTracking.Rows)
                {
                    BE.ContainerCountPendency Containerdata = new ContainerCountPendency();
                    Containerdata.SrNo = Convert.ToString(row["Sr No"]);
                    Containerdata.ContainerNo = Convert.ToString(row["Container No"]);
                    Containerdata.Size = Convert.ToString(row["Size"]);
                    Containerdata.Type = Convert.ToString(row["Type"]);
                    Containerdata.ExporterName = Convert.ToString(row["Exporter Name"]);
                    Containerdata.Commodity = Convert.ToString(row["Commodity"]);
                    Containerdata.CHAName = Convert.ToString(row["CHA Name"]);
                    Containerdata.DocumentReceivedDate = Convert.ToString(row["Document Received Date"]);
                    Containerdata.Vessel = Convert.ToString(row["Vessel"]);
                    Containerdata.CutofDate = Convert.ToString(row["Cut of Date"]);
                    Containerdata.CutofTime = Convert.ToString(row["Cut of Time"]);
                    Containerdata.Gate = Convert.ToString(row["Gate"]);
                    Containerdata.Form11 = Convert.ToString(row["Form11"]);
                    Containerdata.SealNo = Convert.ToString(row["Seal No"]);
                    Containerdata.POD = Convert.ToString(row["POD"]);
                    Containerdata.Grossweight = Convert.ToString(row["Gross weight"]);
                    Containerdata.LineCode = Convert.ToString(row["Line Code"]);
                    Containerdata.Teus = Convert.ToString(row["Teus"]);
                    Containerdata.SBNo = Convert.ToString(row["SB No"]);
                    Containerdata.ModeOfTransport = Convert.ToString(row["Mode Of Transport"]);
                    Containerdetails.Add(Containerdata);
                }
            }
            var jsonResult = Json(Containerdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        //Codes End By manish


        //Codes by Prashant

        //public ActionResult ExportOutReport()
        //{
        //    List<BE.CustomerMaster> CustomerMaster = new List<BE.CustomerMaster>();
        //    CustomerMaster = reportprovider.getCustomerMaster();
        //    ViewBag.customer = new SelectList(CustomerMaster, "AGID", "AGName");
        //    return View();
        //    //return View();
        //}
        //public ActionResult GetEmptyOutreport(string Agencyname,string fromdate,string todate)
        //{
        //    List<BE.ExportEmptyOutReport> EmptyOutReport = new List<BE.ExportEmptyOutReport>();

        //    EmptyOutReport = reportprovider.GetExportOutReport(Agencyname, fromdate, todate);

        //    var jsonResult = Json(EmptyOutReport, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
        //COdes End Prashant 


        // Codes By Prashant  14 July 2019


        public ActionResult ImportEmpty()
        {
            List<BE.LinewiseTuesReport> ImportEmptyList = new List<BE.LinewiseTuesReport>();
            ImportEmptyList = reportprovider.GetImportEmptyList();
            return View(ImportEmptyList);
        }

        public ActionResult ImportLoaded()
        {
            List<BE.LinewiseTuesReport> ImportLoadedList = new List<BE.LinewiseTuesReport>();
            ImportLoadedList = reportprovider.GetImportLoadedList();
            return View(ImportLoadedList);
        }

        public ActionResult ExportEmpty()
        {
            List<BE.LinewiseTuesReport> ExportEmptyList = new List<BE.LinewiseTuesReport>();
            ExportEmptyList = reportprovider.GetExportEmpty();
            return View(ExportEmptyList);
        }

        public ActionResult ExportLoaded()
        {
            List<BE.LinewiseTuesReport> ExportEmptyList = new List<BE.LinewiseTuesReport>();
            ExportEmptyList = reportprovider.GetExportLoaded();
            return View(ExportEmptyList);

        }


        public ActionResult AJaxGetInventoryCount(string Activity, int LineID, int Size)
        {
            List<BE.DoValidityEntities> GetInvenotyLIst = new List<BE.DoValidityEntities>();
            GetInvenotyLIst = reportprovider.GetInventoryCountDetails(Activity, LineID, Size);
            var jsonResult = Json(GetInvenotyLIst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //Codes End by prashant



        public ActionResult DoValidityDetails()
        {
            List<BE.DoValidityEntities> DovalidityExpired = new List<BE.DoValidityEntities>();
            DovalidityExpired = reportprovider.GetDoValidityExpired();
            ViewBag.DoValidityExpired = DovalidityExpired;

            return View();
        }

        public ActionResult Upcomingexpiries()
        {
            List<BE.DoValidityEntities> DovalidityExpiringinSevenDays = new List<BE.DoValidityEntities>();
            DovalidityExpiringinSevenDays = reportprovider.GetDoValidityExpiringInSevenDays();
            ViewBag.DoValidityExpiredSevenDays = DovalidityExpiringinSevenDays;
            return View();
        }


        public ActionResult UpcomingVehicleStatus()
        {
            List<BE.UpcomingVehicleStatusEntities> upcomingList = new List<BE.UpcomingVehicleStatusEntities>();
            upcomingList = reportprovider.GetUpComingVehicleDetails();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_UPCOMMING_VEHICLES_STATUS");
            //DataTable dt = converter.ToDataTable(upcomingList);
            Session["upcomingList1"] = dt;
            return View(upcomingList);
        }

        public ActionResult ExportToExcelVehiclesoutIcd()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            // DataTable dt = List<BE.UpcomingVehicleStatusEntities> Session["upcomingList"];
            //  DataTable dt = new DataTable();
            // List<BE.UpcomingVehicleStatusEntities> obj = new List<BE.UpcomingVehicleStatusEntities>();
            DataTable upcomingList1 = (DataTable)Session["upcomingList1"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = upcomingList1;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=upcomingList.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Vehicle Out OF Icd Report<strong></td></tr>");
                    //htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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

        public ActionResult CustomerWiseImportInventory()
        {
            List<BE.CustomerMaster> CustomerMaster = new List<BE.CustomerMaster>();
            CustomerMaster = reportprovider.getCustomerMaster();
            ViewBag.customer = new SelectList(CustomerMaster, "AGID", "AGName");
            return View();
        }


        public ActionResult ajaxgetCUstomerWiseInventory(string Customerid, string date)
        {

            List<BE.CustomerWiseLDDReport> CustomerImportInventory = new List<BE.CustomerWiseLDDReport>();
            CustomerImportInventory = reportprovider.GetCustomerWiseImportInventory(Customerid, date);
            var JsonResult = Json(CustomerImportInventory, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public ActionResult MarketingWiseImportInventory()
        {
            List<BE.SalesPersonM> SalesPersonM = new List<BE.SalesPersonM>();
            SalesPersonM = BL.getSalesPersonM();
            ViewBag.SalesPersonM = new SelectList(SalesPersonM, "SalesPerson_ID1", "SalesPerson_Name");
            return View();
        }

        public ActionResult ajaxgetSalesWiseInventory(string SalesID, string date)
        {

            List<BE.CustomerWiseLDDReport> CustomerImportInventory = new List<BE.CustomerWiseLDDReport>();
            CustomerImportInventory = reportprovider.GetSalesWiseImportInventory(SalesID, date);
            var JsonResult = Json(CustomerImportInventory, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }


        public ActionResult PortWisePendency()
        {
            List<BE.CustomerWiseLDDReport> upcomingList = new List<BE.CustomerWiseLDDReport>();
            upcomingList = reportprovider.GetPortWisePendency();
            return View(upcomingList);
        }
        

        //Code by Prashant

        public ActionResult EmptyPortMovement()
        {
            List<BE.RevenueAlloperationEntities> ImportEmptyPortMovement = new List<BE.RevenueAlloperationEntities>();
            ImportEmptyPortMovement = reportprovider.GetImportEmptyPortMovement();
            ViewBag.ImportEmpty = ImportEmptyPortMovement;
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            string fromdate = startDate.ToString("yyyy-MM-dd");
            String todate = endDate.ToString("yyyy-MM-dd");
            List<BE.RevenueAlloperationEntities> ExportEmptyPortMovement = new List<BE.RevenueAlloperationEntities>();
            ExportEmptyPortMovement = reportprovider.GetExportEmptyPortMovement(fromdate, todate);
            ViewBag.ExportEmpty = ExportEmptyPortMovement;
            List<BE.RevenueAlloperationEntities> HarizraEmptyPortMovement = new List<BE.RevenueAlloperationEntities>();
            HarizraEmptyPortMovement = reportprovider.GetHaziraEmptyPortMovement(fromdate, todate);
            ViewBag.Hariza = HarizraEmptyPortMovement;
            return View();
        }


        public ActionResult RevenueOperations()
        {
           

            return View();
        }
        [HttpPost]
        public ActionResult AjaxGetReveneOperation(string fromdate, string todate)
        {

            List<BE.RevenueAlloperationICDEntities> revenueOperatioin = new List<BE.RevenueAlloperationICDEntities>();
            revenueOperatioin = reportprovider.GetRevenue(fromdate, todate);
        
            var JsonResult = Json(revenueOperatioin, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }


        public ActionResult SearchVoucher()
        {
            ////List<BE.VoucherNoDropDown> VoucherList = new List<BE.VoucherNoDropDown>();
            ////VoucherList = reportprovider.GetVOucherDropDown();
            ////ViewBag.VoucherDropDown = new SelectList(VoucherList, "tripID", "VoucherNo");

            ////List<BE.TrailerDropDown> TrailerList = new List<BE.TrailerDropDown>();
            ////TrailerList = reportprovider.GetTrailerDropDown();
            ////ViewBag.TrailerDropDown = new SelectList(TrailerList, "trailerid", "trailername");



            ////List<BE.SlipDropDown> SLipList = new List<BE.SlipDropDown>();
            ////SLipList = reportprovider.GetSlipDropDown();
            ////ViewBag.SLipDropDown = new SelectList(SLipList, "SLipID", "SlipNo");

            ////List<BE.TPDropDown> TPDetails = new List<BE.TPDropDown>();
            ////TPDetails = reportprovider.GetTPDropDown();
            ////ViewBag.TPDropDown = new SelectList(TPDetails, "TPNo", "TPSLipNo");

            return View();
        }

        [HttpPost]
        public ActionResult AjaxGetMovementDetail(string VoucherNo, string SlipNo, string Containerno, string TrailerNo)
        {
            List<BE.VoucherDetailsEntities> VoucherList = new List<BE.VoucherDetailsEntities>();
            VoucherList = reportprovider.GetMovementDetails(VoucherNo, SlipNo, Containerno, TrailerNo);
            var jsonResult = Json(VoucherList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        [HttpPost]
        public ActionResult AjaxGetTPClosedetails(string TpNumber)
        {
            List<BE.TPEntryEntities> TPCloseSummary = new List<BE.TPEntryEntities>();
            TPCloseSummary = reportprovider.AjaxGetTPCloseDetailsList(TpNumber);
            var jsonResult = Json(TPCloseSummary, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }

        [HttpPost]
        public ActionResult AjaxGetTrailerMovementDetail(string TrailerNo, string fromdate, string Todate)
        {
            List<BE.VoucherDetailsEntities> VoucherList = new List<BE.VoucherDetailsEntities>();
            VoucherList = reportprovider.GettrailerMovementDetails(TrailerNo, fromdate, Todate);
            var jsonResult = Json(VoucherList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public ActionResult AjaxGetTrailerTPClosedetails(string TrailerNumber)
        {
            List<BE.TPEntryEntities> TPCloseSummary = new List<BE.TPEntryEntities>();
            TPCloseSummary = reportprovider.AjaxTrailerGetTPCloseDetailsList(TrailerNumber);
            var jsonResult = Json(TPCloseSummary, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }


        public ActionResult ImportJoInHand()
        {
            List<BE.CustomerWiseLDDReport> ImportJoHand = new List<BE.CustomerWiseLDDReport>();
            ImportJoHand = reportprovider.GetImportJo();

            return View(ImportJoHand);

            
        }

        public ActionResult MonthlyCollectionReport()
        {
            List<BE.MonthlyEntites> MonthlySummary = new List<BE.MonthlyEntites>();
            MonthlySummary = reportprovider.GetMonthlyreport();

            return View(MonthlySummary);
        }
    

        public ActionResult ImportDelivery()
        {
            List<BE.CustomerWiseLDDReport> ImportDelivery = new List<BE.CustomerWiseLDDReport>();
            ImportDelivery = reportprovider.GetImportDelivery();
            ViewBag.ImportDeliverList = ImportDelivery;

            List<BE.CustomerWiseLDDReport> ImportStuffDelivery = new List<BE.CustomerWiseLDDReport>();
            ImportStuffDelivery = reportprovider.GetImportStuffDelivery();
            ViewBag.ImportDestuffList = ImportStuffDelivery;

            return View();
        }
        // ICD - Export  By Prashant
        public ActionResult ICDExport()
        {
            decimal count = 0;
            List<BE.CustomerWiseLDDReport> ICDExportArrival = new List<BE.CustomerWiseLDDReport>();
            ICDExportArrival = reportprovider.GetICDExportArrival();
            ViewBag.ImportExportArrival = ICDExportArrival;
            int ICDImportCount;

            ICDImportCount = reportprovider.GetICDExportArrivalCOunt();
            ViewBag.ImportExportCount = ICDImportCount;
            count = Convert.ToDecimal(ICDImportCount) / 10000;
            ViewBag.ImportExportValue = count;

            //ICD Export Stuffed

            decimal countvaluestudded = 0;
            List<BE.CustomerWiseLDDReport> ICDExportstuffed = new List<BE.CustomerWiseLDDReport>();
            ICDExportstuffed = reportprovider.GetICDExportStuffed();
            ViewBag.ImportDestuffList = ICDExportstuffed;
            int countStuffed;

            countStuffed = reportprovider.GetICDExportstuffedCOunt();
            ViewBag.ImportExportstuffedCount = countStuffed;
            countvaluestudded = Convert.ToDecimal(countStuffed) / 10000;
            ViewBag.ImportExportValuestudded = countvaluestudded;


            // ICD Export Movement

            List<BE.CustomerWiseLDDReport> ICDExportMovemet = new List<BE.CustomerWiseLDDReport>();
            ICDExportMovemet = reportprovider.GetICDExportMovement();
            ViewBag.ExportMovement = ICDExportMovemet;
            int CountMovenet;
            CountMovenet = reportprovider.GetICDExportMovementCount();
            ViewBag.ImportExportMovementCount = CountMovenet;
            ViewBag.ImportExportMovementValue = Convert.ToDecimal(CountMovenet) / 10000;


            // ICD Export Inventory

            List<BE.CustomerWiseLDDReport> ICDExportInventory = new List<BE.CustomerWiseLDDReport>();
            ICDExportInventory = reportprovider.GetICDExportInventory();
            ViewBag.ExportInventory = ICDExportInventory;
            int CountInventory;
            CountInventory = reportprovider.GetICDExportInventoryCount();
            ViewBag.ImportExportInventoryCount = CountInventory;
            ViewBag.ImportExportInventoryValue = Convert.ToDecimal(CountInventory) / 10000;



            // ICD Export Empty Out

            List<BE.CustomerWiseLDDReport> ICDExportEmptyOut = new List<BE.CustomerWiseLDDReport>();
            ICDExportEmptyOut = reportprovider.GetICDExportemptyOut();
            ViewBag.ExportEmptyOut = ICDExportEmptyOut;
            int CountEmptyOut;
            CountEmptyOut = reportprovider.GetICDExportEmptyOutCount();
            ViewBag.ImportExportEmptyCount = CountEmptyOut;
            ViewBag.ImportExportEmptyValue = Convert.ToDecimal(CountEmptyOut) / 10000;




            // ICD Export Month Wise Movement
            List<BE.MonthlyEntites> ICDExportMonthWiseCollection = new List<BE.MonthlyEntites>();
            ICDExportMonthWiseCollection = reportprovider.GetICDExportMonthlyWiseCollection();
            ViewBag.ExportMonthWiseCollection = ICDExportMonthWiseCollection;


            int CountMonthWiseCollection;
            CountMonthWiseCollection = reportprovider.GetICDExportMonthlyWiseCollectionCount();
            ViewBag.ImportExportMonthCount = CountMonthWiseCollection;
            ViewBag.ImportExportMonthValue = Convert.ToDecimal(CountMonthWiseCollection) / 10000;



            // ICD Export Month Wise Collection
            List<BE.MonthlyEntites> ICDExportMonthWiseCollectionList = new List<BE.MonthlyEntites>();
            ICDExportMonthWiseCollectionList = reportprovider.GetICDExportMonthlyWiseMovement();
            ViewBag.ExportMonthWiseCollectionList = ICDExportMonthWiseCollectionList;

            string CountMonthWiseCollectionList = "";
            CountMonthWiseCollectionList = reportprovider.GetICDExportMonthlyWiseMovementCount();
            ViewBag.ImportExportMonthCountList = CountMonthWiseCollectionList;
            ///ViewBag.ImportExportMonthValueList = Convert.ToDecimal(CountMonthWiseCollectionList) / 10000;



            // ICD Export Month Port Wise Movement Pendency

            List<BE.CustomerWiseLDDReport> ICDExportPortWisePendency = new List<BE.CustomerWiseLDDReport>();
            ICDExportPortWisePendency = reportprovider.GetICDExportPortWiseMovement();
            ViewBag.ExportPortWisePendency = ICDExportPortWisePendency;
            int CountPortWisePendency;
            CountPortWisePendency = reportprovider.GetICDExportEmptyPortWiseMovement();
            ViewBag.ImportExportPortWisePendencycOUNT = CountPortWisePendency;
            ViewBag.ImportExportPortWisePendencyvALUE = Convert.ToDecimal(CountPortWisePendency) / 10000;


            // ICD _ Export Port Shut Out

            List<BE.CustomerWiseLDDReport> ICDExportPortShutOut = new List<BE.CustomerWiseLDDReport>();
            ICDExportPortShutOut = reportprovider.GetICDExportShutOut();
            ViewBag.ExportPortShutOut = ICDExportPortShutOut;

            int CountPortShutOut;
            CountPortShutOut = reportprovider.GetICDExportShutOutCount();
            ViewBag.ImportExportPortPortShutOutCount = CountPortShutOut;
            ViewBag.ImportExportPortShutOutValue = Convert.ToDecimal(CountPortShutOut) / 10000;


            // ICD _ Export Re - Movement

            List<BE.CustomerWiseLDDReport> ICDExportReMovement = new List<BE.CustomerWiseLDDReport>();
            ICDExportReMovement = reportprovider.GetICDExportReMovement();
            ViewBag.ExportPortReMovement = ICDExportReMovement;

            int CountPortReMovement;
            CountPortReMovement = reportprovider.GetICDExportShutOutCount();
            ViewBag.ImportExportReMovementCount = CountPortReMovement;
            ViewBag.ImportExportRemovementValue = Convert.ToDecimal(CountPortReMovement) / 10000;



            // ICD _ Export Re Working

            List<BE.CustomerWiseLDDReport> ICDExportReWorking = new List<BE.CustomerWiseLDDReport>();
            ICDExportReWorking = reportprovider.GetICDExportReWorking();
            ViewBag.ExportPortReWorking = ICDExportReWorking;


            int CountPortReWorking;
            CountPortReWorking = reportprovider.GetICDExportShutOutCount();
            ViewBag.ImportExportReWorkingCount = CountPortReWorking;
            ViewBag.ImportExportReWorkingValue = Convert.ToDecimal(CountPortReWorking) / 10000;

            return View();

        }

        public ActionResult CreditNoteSummary()
        {
            List<BE.ActivityMaster> ActivityMaster = new List<BE.ActivityMaster>();
            ActivityMaster = reportprovider.getActivityMasterCreditNote();
            ViewBag.Activity = new SelectList(ActivityMaster, "TYPEID", "Activity");
            List<BE.PartyMaster> PartyMaster = new List<BE.PartyMaster>();
            PartyMaster = reportprovider.getPartyMaster();
            ViewBag.GSTName = new SelectList(PartyMaster, "GSTID", "GSTName");
            return View();
            //return View();
        }

        public ActionResult GetCreditNoteSummary(string ddlActivity, string ddlGSTName, string fromdate, string todate, string CFor)
        {
            DataTable GetCreditNoteSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetCreditNoteSummary = db.sub_GetDatatable("USP_CATEGORY_WISE_CREDIT_NOTE_SUMMARY '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm") + "','" + ddlActivity + "','" + ddlGSTName + "','" + CFor + "'");
            Session["GetCreditNoteSummary"] = GetCreditNoteSummary;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            var json = JsonConvert.SerializeObject(GetCreditNoteSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelCreditNoteSummary()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetCreditNoteSummary = (DataTable)Session["GetCreditNoteSummary"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetCreditNoteSummary;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=CreditNoteSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Credit Note Summary</h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + Tittle + " </h3></td></tr>");
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

        public ActionResult GSTReturnCreditNoteSummary()
        {
        
            return View();
        }


        public ActionResult GetGSTReturnCreditNoteSummary( string fromdate, string todate)
        {
            DataTable GetGSTReturnCreditNoteSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetGSTReturnCreditNoteSummary = db.sub_GetDatatable("USP_GST_RETURN_FOR_CREDIT_NOTE_SUMMARY '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["GetGSTReturnCreditNoteSummary"] = GetGSTReturnCreditNoteSummary;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            var json = JsonConvert.SerializeObject(GetGSTReturnCreditNoteSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelGstReturnCNSummary(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetGSTReturnCreditNoteSummary = (DataTable)Session["GetGSTReturnCreditNoteSummary"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetGSTReturnCreditNoteSummary;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=GstReturnCreditNoteSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Gst Return Credit Note Summary </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + Tittle + " </h3></td></tr>");
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

        public ActionResult HSNCodeWiseSummary()
        {

            return View();
        }


        public ActionResult GetHSNCodeWiseSummary(string Category, string fromdate, string todate)
        {
            DataTable GetHSNCodeWiseSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetHSNCodeWiseSummary = db.sub_GetDatatable("USP_HSNWISE_SUMMARY '" + Category + "','" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["GetHSNCodeWiseSummary"] = GetHSNCodeWiseSummary;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            var json = JsonConvert.SerializeObject(GetHSNCodeWiseSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelHSNWiseSummary(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetHSNCodeWiseSummary = (DataTable)Session["GetHSNCodeWiseSummary"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetHSNCodeWiseSummary;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=HSNCodeWiseSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  colspan ='7'><h2 style='text-align:center'>" + CompanyName + " </h2></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>HSN Code Wise Summary </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + Tittle + " </h3></td></tr>");
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

        public ActionResult OutwardSupplySummary()
        {

            return View();
        }

        public ActionResult GetOutwardSupplySummary( string fromdate, string todate)
        {
            DataTable GetOutwardSupplySummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetOutwardSupplySummary = db.sub_GetDatatable("SP_ImportOutwordSupply '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["GetOutwardSupplySummary"] = GetOutwardSupplySummary;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            var json = JsonConvert.SerializeObject(GetOutwardSupplySummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelOSupplySummary(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetOutwardSupplySummary = (DataTable)Session["GetOutwardSupplySummary"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetOutwardSupplySummary;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=OutwardSupplySummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  colspan ='7'><h2 style='text-align:center'>" + CompanyName + " </h2></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Outward Supply Summary </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + Tittle + " </h3></td></tr>");
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

        public ActionResult ExportTuesWiseRevenue()
        {

            return View();
        }

        public ActionResult GetExportTuesWiseRevenue(string fromdate, string todate)
        {
            DataTable GetExportTuesWiseRevenue = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetExportTuesWiseRevenue = db.sub_GetDatatable("SP_ExportFinal_Sales'" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm") + "', 'ALL','', '',''");
            Session["GetExportTuesWiseRevenue"] = GetExportTuesWiseRevenue;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            var json = JsonConvert.SerializeObject(GetExportTuesWiseRevenue);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelExptTuesWiseRevenue(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetExportTuesWiseRevenue = (DataTable)Session["GetExportTuesWiseRevenue"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetExportTuesWiseRevenue;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExportTuesWiseRevenue.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Export TuesWise Revenue<strong></td></tr>");
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

        public ActionResult TrackDayWiseFuelallocationReport()
        {
            List<BE.CostCenter> CostCenter = new List<BE.CostCenter>();
            CostCenter = trainTrackerProvider.getCostCenter();
            ViewBag.CostCenter = new SelectList(CostCenter, "Costid", "CostCenterName");
            return View();
        }

        public ActionResult GetDayWiseFuelallocationReport(string fromdate, string todate, int ddlCostCenter)
        {
            DataTable GetDayWiseFuelallocationReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetDayWiseFuelallocationReport = db.sub_GetDatatable("USP_DAY_Wise_Fuelallocation_D_test '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','"+ Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm") + "','','"+ ddlCostCenter + "'");
            //GetDayWiseFuelallocationReport.Columns.Remove("receiveddate");
            GetDayWiseFuelallocationReport.Columns.Remove("receiveddate");
            GetDayWiseFuelallocationReport.Columns.Remove("PreviousBal");
            GetDayWiseFuelallocationReport.Columns.Remove("Receivedqty");
            GetDayWiseFuelallocationReport.Columns.Remove("Totalqty");
            GetDayWiseFuelallocationReport.Columns.Remove("id");
            GetDayWiseFuelallocationReport.Columns.Remove("receiveddatevar");
            Session["GetDayWiseFuelallocationReport"] = GetDayWiseFuelallocationReport;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            var json = JsonConvert.SerializeObject(GetDayWiseFuelallocationReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExceldayWisefAReport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetExportTuesWiseRevenue = (DataTable)Session["GetDayWiseFuelallocationReport"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetExportTuesWiseRevenue;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=DayWiseFuelAllocationReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Day Wise Fuel Allocation Report<strong></td></tr>");
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

        public ActionResult DailyActivityTripINOutreport()
        {

            return View();
        }

        public ActionResult GetDailyActivityTripINOutreport(string fromdate, string todate)
        {
            DataTable GetDailyActivityTripINOutreport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetDailyActivityTripINOutreport = db.sub_GetDatatable("DAILY_TRIP_REPORT '" + Convert.ToDateTime(fromdate).ToString("dd/MM/yyyy") + "','" + Convert.ToDateTime(todate).ToString("dd/MM/yyyy") + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["GetDailyActivityTripINOutreport"] = GetDailyActivityTripINOutreport;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            var json = JsonConvert.SerializeObject(GetDailyActivityTripINOutreport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelActivityTripINOut(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetDailyActivityTripINOutreport = (DataTable)Session["GetDailyActivityTripINOutreport"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetDailyActivityTripINOutreport;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=DailyActivityINOutreport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Daily Activity Trip Report<strong></td></tr>");
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

        public ActionResult FuelSlipDetailsReport()
        {
            List<BE.CostCenterFor> CostCenterFor = new List<BE.CostCenterFor>();
            CostCenterFor = trainTrackerProvider.getCostCenterFor();
            ViewBag.CostCenterFor = new SelectList(CostCenterFor, "CodeCenterID", "CodeCenterType");
            List<BE.fuelType> fuelType = new List<BE.fuelType>();
            fuelType = trainTrackerProvider.getfuelType();
            ViewBag.FuelType = new SelectList(fuelType, "Fuelid", "FuelType");
            return View();
        }

        public ActionResult GetFuelSlipDetailsReport(string fromdate, string todate, string CostCenterFor, string Fueltype)
        {
            DataTable GetFuelSlipDetailsReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetFuelSlipDetailsReport = db.sub_GetDatatable("FuelSlipDetails_Report '" + Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy hh:mm") + "','" + Convert.ToDateTime(todate).ToString("dd-MMM-yyyy hh:mm") + "','" + CostCenterFor + "','" + Fueltype + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["GetDailyActivityTripINOutreport"] = GetFuelSlipDetailsReport;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            Session["Fueltype"] = Fueltype;

            var json = JsonConvert.SerializeObject(GetFuelSlipDetailsReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelFuelSlipDetailsReport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetDailyActivityTripINOutreport = (DataTable)Session["GetDailyActivityTripINOutreport"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + "Fuel Type  :" + Session["Fueltype"] + "";
            GridView gridview = new GridView();
            gridview.DataSource = GetDailyActivityTripINOutreport;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=FuelSlipDetailsReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Fuel Slip Details Report<strong></td></tr>");
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

        public ActionResult SearchInvoice(string invoiceNo)
        {
            if (invoiceNo != "")
            {
                ViewBag.invoiceNo = invoiceNo;
            }

            return View();
        }


        public ActionResult GetSearchInvoice(string category, string Search, string Search1,string Search2)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_InvoiceSearch '" + category + "','" + Search + "','" + Search1 + "','" + Search2 + "'");
            dt.Columns.Remove("View");
            Session["SP_InvoiceSearch"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult ExportToExcelGetSearchInvoice(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetDailyActivityTripINOutreport = (DataTable)Session["SP_InvoiceSearch"];
            GetDailyActivityTripINOutreport.Columns.Remove("View1");
            ////string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + "Fuel Type  :" + Session["Fueltype"] + "";
            GridView gridview = new GridView();
            gridview.DataSource = GetDailyActivityTripINOutreport;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=SearchReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Search Report<strong></td></tr>");
                    //htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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
        //public ActionResult GetSearchInvoice(string category, string Search, string txtItemNo)
        //{
        //    List<BE.CategorywiseInvoice> GetSearchInvoice = new List<BE.CategorywiseInvoice>();

        //    GetSearchInvoice = reportprovider.GetSearchInvoice(category, Search, txtItemNo);

        //    var jsonResult = Json(GetSearchInvoice, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}

        public ActionResult LineWiseOpeningTuesReport()
        {
          
            return View();
        }

        public ActionResult GetLineWiseOpeningTuesReport(string Fromdate, string Todate)
        {
            List<BE.LinewiseTuesReport> OpeningclosingList = new List<BE.LinewiseTuesReport>();
            OpeningclosingList = reportprovider.GetLineWiseOpeningTuesReport(Fromdate, Todate);
            DataTable OpeningclosingListNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            OpeningclosingListNew = db.sub_GetDatatable("LineWise_Opening_and_Closing_tues_Report '" + Convert.ToDateTime(Fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd HH:mm") + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["OpeningclosingList"] = OpeningclosingListNew;
            //Session["OpeningclosingList"] = OpeningclosingList;
            Session["fromdate"] = Fromdate;
            Session["todate"] = Todate;
            var jsonResult = Json(OpeningclosingList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelGetLineWiseOpeningTuesReport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable OpeningclosingList = (DataTable)Session["OpeningclosingList"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = OpeningclosingList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=LineWiseOpeningAndClosingTEUS.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Line Wise Opening And Closing TEUS<strong></td></tr>");
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

        public ActionResult ImportJobOrderReport()
        {
            List<BE.ImportJOType> JoType = new List<BE.ImportJOType>();
            JoType = BL.getJOType();
            ViewBag.JoTypes = new SelectList(JoType, "JotypeId", "Jotype");
            return View();
        }

        public ActionResult getImportJobOrderReport(string FromDate, string ToDate,string JoType)
        {
            DataTable getImportJobOrderReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getImportJobOrderReport = db.sub_GetDatatable("USP_JOBORDER_REPORT '" + FromDate + "','" + ToDate + "','" + JoType + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["getImportJobOrderReport"] = getImportJobOrderReport;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            Session["JoType"] = JoType;
            var json = JsonConvert.SerializeObject(getImportJobOrderReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult getImportJoReport(string FromDate, string ToDate, string JoType)
        {
            DataTable getImportJobReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getImportJobReport = db.sub_GetDatatable("USP_JODetail '" + FromDate + "','" + ToDate + "','" + JoType + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["getImportJobReport"] = getImportJobReport;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            Session["JoType"] = JoType;
            var json = JsonConvert.SerializeObject(getImportJobReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelgetImportJobOrderReport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getImportJobOrderReport = (DataTable)Session["getImportJobOrderReport"];
            DataTable getImportJobReport = (DataTable)Session["getImportJobReport"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            string FromDate = Session["fromdate"].ToString();
            string ToDate = Session["todate"].ToString();
            string JoType = Session["JoType"].ToString();

            if (JoType == "")
            {
                JoType = "0";
            }

            DataTable Summarry = new DataTable();
            //HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            Summarry = db.sub_GetDatatable("YARD_BALANCE_INVENTRY_NEW '" + FromDate + "','" + ToDate + "','" + JoType + "'");
            GridView gridview3 = new GridView();
            GridView gridview1 = new GridView();
            GridView gridview2 = new GridView();
            gridview3.DataSource = Summarry; 
            gridview3.DataBind();

            gridview1.DataSource = getImportJobOrderReport;
            gridview1.DataBind();
            gridview2.DataSource = getImportJobReport; 
            gridview2.DataBind();

            if (gridview3.Rows.Count != 0)
            {
                PrepareForExport(gridview3);
            }

            if (gridview1.Rows.Count != 0)
            {
                PrepareForExport(gridview1);
            }
            if (gridview2.Rows.Count != 0)
            {
                PrepareForExport(gridview2);

            }

            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Controls.Add(gridview1);

            tr1.Cells.Add(cell1);

            TableCell cell3 = new TableCell();

            cell3.Controls.Add(gridview2);

            TableCell cell2 = new TableCell();

            cell2.Text = " ";


            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);

            TableRow tr3 = new TableRow();

            tr3.Cells.Add(cell3);

            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.Rows.Add(tr3);


            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=JobOrderSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Job Order Summary<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    tb.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        public ActionResult getIGMDetailsSummary(string FromDate, string ToDate,string TransType,string JoType)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("GetIGMSummaryReports  '" + FromDate + "','" + ToDate + "','" + TransType + "','" + JoType + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["getIGMSummary"] = getMovementICDNew;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelIGMDetailsSummary(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["getIGMSummary"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=IGMDetailsSummaryList.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> IGM Details Summary Details<strong></td></tr>");
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

        public ActionResult getLoadedInventory(string AsonDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("sp_GetImportInventoryD '" + Convert.ToDateTime(AsonDate).ToString("yyyyMMddHHmm") + "'");
            Session["spGetImportInventoryD"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelLoadedInventory()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable spGetImportInventoryD = (DataTable)Session["spGetImportInventoryD"];
            GridView gridview = new GridView();
            gridview.DataSource = spGetImportInventoryD;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=LoadedInventory.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Loaded Inventory></td></tr>");

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



        public ActionResult ImportHeadWiseDetails()
        {
            string name = "";
            int id = 0;
            List<BE.CriteriaEntities> AccountHeadList = new List<BE.CriteriaEntities>();
            AccountHeadList = reportprovider.GetExisitingAccountName(name, id);
            ViewBag.AccountHeadList = new SelectList(AccountHeadList, "Id", "Criteria");

            List<BE.CriteriaEntities> SlnameList = new List<BE.CriteriaEntities>();
            SlnameList = reportprovider.GetExisitingSlnameName(name, id);
            ViewBag.SlnameList = new SelectList(SlnameList, "Id", "Criteria");
            return View();
        }

        public ActionResult GetDataBind(string fromDate, string ToDate, int Slid, int Accountid)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_getImportHeadWise_Summary'" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Slid + "','" + Accountid + "'");
            Session["ImportHeadWise"] = dt;
            Session["fromdate"] = fromDate;
            Session["todate"] = ToDate;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExcelImphead()
        {
            DataTable dt = (DataTable)Session["ImportHeadWise"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ImportHeadWise = (DataTable)Session["ImportHeadWise"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ImportHeadWiseSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Import Head Wise Summary Report <strong></td></tr>");
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

        public ActionResult WaraiInvoiceLst(string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_showwaraiassessmentS'" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["WaraiWise"] = dt;
            Session["fromdate"] = fromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExcelWaraiInv()
        {
            DataTable dt = (DataTable)Session["WaraiWise"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=WaraiSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Warai Invoice Report <strong></td></tr>");
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

        public ActionResult ExportLoadedOutReport(string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_ShowExportOutdetails'" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["eXPlOADEDWise"] = dt;
            Session["fromdate"] = fromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExcelExpLoadedOut()
        {
            DataTable dt = (DataTable)Session["eXPlOADEDWise"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExpLoadedOut.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Loaded Out Report <strong></td></tr>");
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

        public ActionResult ExportStuffingRep(string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_NewDaqilyStuffingReport'" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["ExpStuffingWise"] = dt;
            Session["fromdate"] = fromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExcelExpStuffRep()
        {
            DataTable dt = (DataTable)Session["ExpStuffingWise"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExpStuffReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Stuffing Report <strong></td></tr>");
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
        public ActionResult WeightmentReport()
        {

            return View();
        }

        public ActionResult GetWeightmentReport(string Fromdate, string Todate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Show_weighmentdetails '" + Convert.ToDateTime(Fromdate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["USP_Show_weighmentdetails"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelGetReport()
        {
            DataTable dt = (DataTable)Session["USP_Show_weighmentdetails"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ImportHeadWise = (DataTable)Session["USP_Show_weighmentdetails"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=weighmentSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>weighment Summary Report <strong></td></tr>");
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
        public ActionResult InvoiceRevenueReportPrint(string FromDate, string ToDate, string CustomerType,string JoType,string CustomerId)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceList = new DataTable();
            FromDate = Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm");
            ToDate = Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm");

            HC.DBOperations db = new HC.DBOperations();

            if (CustomerType == "Line")
            {
                getJobOrderSet = db.sub_GetDataSets("InvoiceRevenueReport '" + FromDate + "','" + ToDate + "','" + CustomerType + "','" + JoType + "','" + CustomerId + "'");
            }

            if (CustomerType == "CHA")
            {
                getJobOrderSet = db.sub_GetDataSets("InvoiceRevenueReportCHA '" + FromDate + "','" + ToDate + "','" + CustomerType + "','" + JoType + "','" + CustomerId + "'");
            }

            if (CustomerType == "Importer")
            {
                getJobOrderSet = db.sub_GetDataSets("InvoiceRevenueReportImporter '" + FromDate + "','" + ToDate + "','" + CustomerType + "','" + JoType + "','" + CustomerId + "'");
            }


            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblInvoiceList = getJobOrderSet.Tables[1];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                }
            }

            ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.InvoiceListList = tblInvoiceList.AsEnumerable();

            return PartialView();

        }

        //code end by prashant

        public ActionResult GetAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<BE.CategorySearchInvoice> SearchInvoice = new List<BE.CategorySearchInvoice>();

            SearchInvoice = reportprovider.GetAssessNoInvoice(AssessNo, WorkYear, Category);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetInvoiceBind(string AssessNo, string WorkYear)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_Grid_Serach_invoice'" + AssessNo + "','" + WorkYear + "'");
            Session["usp_Grid_Serach_invoice"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetReciptBind(string AssessNo, string WorkYear)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Show_ReceiptDetails'" + AssessNo + "','" + WorkYear + "'");
            Session["USP_Show_ReceiptDetails"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetCreditBind(string AssessNo, string WorkYear, String Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_SearchAssessmnet_CreditNote'" + AssessNo + "','" + WorkYear + "','" + Category + "'");
            Session["SP_SearchAssessmnet_CreditNote"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetRebitBind(string Jono)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_rebit_bill'" + Jono + "'");
            Session["usp_rebit_bill"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<BE.CategorySearchInvoice> SearchInvoice = new List<BE.CategorySearchInvoice>();

            SearchInvoice = reportprovider.ExportAssessNoInvoice(AssessNo, WorkYear, Category);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult BondAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<BE.CategorySearchInvoice> SearchInvoice = new List<BE.CategorySearchInvoice>();

            SearchInvoice = reportprovider.BondAssessNoInvoice(AssessNo, WorkYear, Category);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult DomesticAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<BE.CategorySearchInvoice> SearchInvoice = new List<BE.CategorySearchInvoice>();

            SearchInvoice = reportprovider.DomestcAssessNoInvoice(AssessNo, WorkYear, Category);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult MNRAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<BE.CategorySearchInvoice> SearchInvoice = new List<BE.CategorySearchInvoice>();

            SearchInvoice = reportprovider.MNRAssessNoInvoice(AssessNo, WorkYear, Category);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult CartingAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<BE.CategorySearchInvoice> SearchInvoice = new List<BE.CategorySearchInvoice>();

            SearchInvoice = reportprovider.CartingAssessInvoice(AssessNo, WorkYear, Category);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult CreditAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<BE.CategorySearchInvoice> SearchInvoice = new List<BE.CategorySearchInvoice>();

            SearchInvoice = reportprovider.CreditAssessInvoice(AssessNo, WorkYear, Category);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ImportInvoiceCredit(string creditnoteno, string workyear, string ID)
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

            getJobOrderSet = db.sub_GetDataSets("USP_CREDIT_NOTE_PRINT '" + creditnoteno + "','" + workyear + "','" + ID + "'");

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
                    ViewBag.Con_Dets = dr["Con_Dets"];
                    ViewBag.CINNo = dr["CINNo"];


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
                    ViewBag.TrasnportReamarks = dr["TrasnportReamarks"];

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
        public ActionResult GetAssessBond(string AssessNo, string WorkYear, String Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_Bond_Account_Search'" + AssessNo + "','" + WorkYear + "'");
            Session["usp_Bond_Account_Search"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetAssessBondCharges(string AssessNo, string WorkYear, String Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_Bond_Account_Search_Acc'" + AssessNo + "','" + WorkYear + "'");
            Session["usp_Bond_Account_Search_Acc"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetAssessBondBank(string AssessNo, string WorkYear, String Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_Bond_Account_Search_Bank'" + AssessNo + "','" + WorkYear + "'");
            Session["usp_Bond_Account_Search_Bank"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetAssessBondDis(string AssessNo, string WorkYear, String Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_Bond_Account_Search_Dis'" + AssessNo + "','" + WorkYear + "'");
            Session["usp_Bond_Account_Search_Dis"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetAssessBondCredit(string AssessNo, string WorkYear, String Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_SearchAssessmnet_CreditNote'" + AssessNo + "','" + WorkYear + "'");
            Session["SP_SearchAssessmnet_CreditNote"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        [HttpPost]
        public ActionResult ImportInvoicePrint(string InvoiceNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblHeadDetails = new DataTable();
            DataTable tblChargesDetails = new DataTable();
            DataTable tblWordAmount = new DataTable();
            DataTable tblBankDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            double Amount = 0;
            double Discount = 0;
            double NetAmount = 0;
            string Srate = "";
            double SGST = 0;
            string CRate = "";
            double CGST = 0;
            string IRate = "";
            double IGST = 0;

            getJobOrderSet = db.sub_GetDataSets("getInvoicePrintMVC '" + InvoiceNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblInvoiceDetails = getJobOrderSet.Tables[1];
                tblContainerDetails = getJobOrderSet.Tables[2];
                tblHeadDetails = getJobOrderSet.Tables[3];
                tblChargesDetails = getJobOrderSet.Tables[4];
                tblWordAmount = getJobOrderSet.Tables[5];
                tblBankDetails = getJobOrderSet.Tables[6];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.CINNo = dr["CINNo"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.Addressl = dr["AddressI"];
                    ViewBag.IMP_AC_EMAILID = dr["IMP_AC_EMAILID"];
                    ViewBag.Datetime = Convert.ToDateTime(DateTime.Now).ToString("dd MM yyyy");
                    ViewBag.TIme = Convert.ToDateTime(DateTime.Now).ToString("HH:mm");
                }
                foreach (DataRow dr in tblInvoiceDetails.Rows)
                {
                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.InvoiceDate = dr["AssessDate"];
                    ViewBag.Name = dr["GSTName"];
                    ViewBag.Address = dr["GSTAddress"];
                    ViewBag.GSTIN = dr["GSTIn_uniqID"];
                    ViewBag.PlaceofSupply = dr["State"];
                    ViewBag.Consignee = dr["Consignee"];
                    ViewBag.Address = dr["Con_IGMAddress"];
                    ViewBag.ShippingLine = dr["SLName"];
                    ViewBag.BOENo = dr["BOEno"];
                    ViewBag.IgmItemNo = dr["IGMNo"] + "/" + dr["ItemNo"];
                    ViewBag.BLNo = dr["IGM_BLNo"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.CargoWeight = dr["GrossWt"];
                    ViewBag.CargoDesc = dr["IGM_GoodsDesc"];
                    ViewBag.Movement = dr["Movement"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.Irn = dr["Irn"];
                    ViewBag.AckNo = dr["AckNo"];
                    ViewBag.AckDt = dr["AckDt"];
                    ViewBag.SignedQRcode = dr["SignedQRcode"];
                    ViewBag.UserName = dr["UserName"];
                }

                ViewBag.BankName = tblBankDetails.Rows[0]["BankName"];
                ViewBag.AccountNo = tblBankDetails.Rows[0]["AccountNo"];
                ViewBag.IFSCCode = tblBankDetails.Rows[0]["IFSCCode"];
                ViewBag.Remarks = tblBankDetails.Rows[0]["Remarks"];
                ViewBag.BranchName = tblBankDetails.Rows[0]["BranchName"];

                ViewBag.TotalAmountInWord = tblWordAmount.Rows[0]["TotalAmountInWords"];
                ViewBag.TotalAmount = tblWordAmount.Rows[0]["GrandTotal"];
                ViewBag.AmountWithoutTax = tblWordAmount.Rows[0]["NetTotal"];
                ViewBag.SGSTAmount = tblWordAmount.Rows[0]["SGST"];
                ViewBag.CGSTAmount = tblWordAmount.Rows[0]["CGST"];
                ViewBag.IGSTAmount = tblWordAmount.Rows[0]["IGST"];
                ViewBag.TaxAmount = Convert.ToDouble(Convert.ToDouble(tblWordAmount.Rows[0]["SGST"]) + Convert.ToDouble(tblWordAmount.Rows[0]["CGST"]) + Convert.ToDouble(tblWordAmount.Rows[0]["IGST"]));
            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();
            ViewBag.HeadDetailsList = tblHeadDetails.AsEnumerable();
            ViewBag.ChargesDetailsList = tblChargesDetails.AsEnumerable();

            foreach(DataRow data in tblChargesDetails.Rows)
            {
                Amount =Convert.ToDouble(data["Amount"]);
                Discount = Convert.ToDouble(data["Discount"]);
                NetAmount = Convert.ToDouble(data["NetAmount"]);
                Srate = Convert.ToString(data["Srate"]);
                CRate = Convert.ToString(data["CRate"]);
                IRate = Convert.ToString(data["IRate"]);
                SGST = Convert.ToDouble(data["SGST"]);
                CGST = Convert.ToDouble(data["CGST"]);
                IGST = Convert.ToDouble(data["IGST"]);
            }

            ViewBag.Amount = Amount;
            ViewBag.Discount = Discount;
            ViewBag.NetAmount = NetAmount;
            ViewBag.Srate = Srate;
            ViewBag.CRate = CRate;
            ViewBag.IRate = IRate;
            ViewBag.SGST = SGST;
            ViewBag.CGST = CGST;
            ViewBag.IGST = IGST;

            return PartialView();

        }

        public ActionResult GetMuptiplePrintM(string Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("select PrintURL from MuptiplePrintM where CycleName='" + Category + "'");
            string URLDet = "";
            URLDet = dt.Rows[0]["PrintURL"].ToString();
            var jsonResult = Json(URLDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetMuptiplePrintImport(string Category)
        {
            string url = Request.Url.Host;
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("UPS_MuptiplePrintM'" + url + "','" + Category+ "'");
            string URLDet = "";
            URLDet = dt.Rows[0]["PrintURL"].ToString();
            var jsonResult = Json(URLDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ContainerSearchDetails()
        {
            return View();
        }
        public ActionResult GetContainerDetails(string ContainerNo)
        {
            DataTable GetDayWiseFuelallocationReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            GetDayWiseFuelallocationReport = db.sub_GetDatatable("USP_SCANNING_DATA_Search '" + ContainerNo + "'");

            var json = JsonConvert.SerializeObject(GetDayWiseFuelallocationReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult getCreditInvoiceCollection(string Criteria, string FromDate, string ToDate)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("GetCreditReceipt  '" + FromDate + "','" + ToDate + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["GetCreditCollection"] = getMovementICDNew;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult CreditInvoiceReport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["GetCreditCollection"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=CreditInvoiceCollection.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Credit Invoice Report<strong></td></tr>");
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

        public ActionResult Unlocktransactions()
        {

            return View();
        }

        public ActionResult Unlocktransactio(string ddlCriteria, string ddlType, string txtInvoice, string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_unlocktransactions '" + ddlCriteria + "','" + ddlType + "','" + txtInvoice + "','" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["USP_Show_weighmentdetails"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult AjaxUnlockInvoiesDetails(string ddlCriteria, string ddlType, string txtInvoice, string Fromdate, string Todate)
        {
            string message = "";
            //int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = reportprovider.UnlockDetails(ddlCriteria, ddlType, txtInvoice, Fromdate, Todate);
            return Json(message);
        }
        public ActionResult AjaxCancelDetails(string ddlCriteria, string ddlType, string txtInvoice)
        {
            string message = "";
            //int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = reportprovider.CancelDtails(ddlCriteria, ddlType, txtInvoice);
            return Json(message);
        }

        public ActionResult ExportReMovementRep(string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_PortshutoutReport'" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["ExpStuffingWise"] = dt;
            Session["fromdate"] = fromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExcelExpReMovRep()
        {
            DataTable dt = (DataTable)Session["ExpStuffingWise"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExpReMovementReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Re-Movement Report <strong></td></tr>");
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
        public ActionResult getAuctionUCCReport(string FromDate,string ToDate,string RepType)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            if (RepType == "In")
            {
                dt = db.sub_GetDatatable("SP_AIUploadFormat '" + FromDate + "','" + ToDate + "'");
            }
            else
            {
                dt = db.sub_GetDatatable("Get_SP_ImportLoadedOutdtls '" + FromDate + "','" + ToDate + "'");
            }

            Session["AuctionUCCRep"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            Session["RepType"] = RepType;

            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelUCCReport()
        {
            DataTable dt = (DataTable)Session["AuctionUCCRep"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string FileName =Convert.ToString(Session["RepType"])+ "UCCReport";

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename="+ FileName + ".xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Re-Movement Report <strong></td></tr>");
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


        //code by rahul 08-01-2020
        public ActionResult GetTypeWiseRegister(string Activity, string Date,string Yard)
        {
            DataTable TypeReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TypeReport = db.sub_GetDatatable("LINEWISE_TUES_REPORT_EMPTY_LOADED_TYPE_WISE '" + Activity + "','" + Convert.ToDateTime(Date).ToString("yyyyMMddHHmm") + "','" + Yard + "'");
            Session["TypeWiseTeusReport"] = TypeReport;
            Session["todate"] = Date;
            Session["Activity"] = Activity;
            var json = JsonConvert.SerializeObject(TypeReport);

            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult TrackLineNTypeWiseTEUSReport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable TypeReportDT = (DataTable)Session["TypeWiseTeusReport"];
            string Tittle = "As On " + Session["todate"];
            string Tittle1 = "Activity " + Session["Activity"];
            GridView gridview = new GridView();
            gridview.DataSource = TypeReportDT;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=LineNTypeWiseInventorySummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='" + TypeReportDT.Columns.Count + "'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='" + TypeReportDT.Columns.Count + "'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='" + TypeReportDT.Columns.Count + "'><strong style='font-size: 15px'> Line & Type Wise Inventory Summary<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='" + TypeReportDT.Columns.Count + "'><strong style='font-size: 15px'>" + Tittle + " " + Tittle1 + " <strong></td></tr>");
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
        //code end by rahul 08-01-2020

        public JsonResult getExportCustomReport(string FromDate, string ToDate)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            ImpFinalOut = db.sub_GetDatatable("USP_ExportCustomReport '" + FromDate + "','" + ToDate + "'");

            Session["ExpCustomRep"] = ImpFinalOut;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelExportCustomerReport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["ExpCustomRep"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExportCustomReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Custom Report<strong></td></tr>");
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

        public JsonResult getListofPenginDo(string FromDate, string ToDate)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            DataTable ImpFinalSum = new DataTable();
            DataSet dataSet = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            string TotalDo = "";
            string TotalDOReceived = "";
            string VehicleSent = "";
            string TotalGateIn = "";
            string BalanceDo = "";

            dataSet = db.sub_GetDataSets("GetUspBookingWiseExp '" + FromDate + "','" + ToDate + "'");

            ImpFinalOut = dataSet.Tables[0];
            ImpFinalSum = dataSet.Tables[1];

            TotalDo = ImpFinalSum.Rows[0]["Total Do"].ToString();
            TotalDOReceived = ImpFinalSum.Rows[0]["Total DO Received"].ToString();
            VehicleSent = ImpFinalSum.Rows[0]["Vehicle Sent"].ToString();
            TotalGateIn = ImpFinalSum.Rows[0]["Total Gate In"].ToString();
            BalanceDo = ImpFinalSum.Rows[0]["Balance Do"].ToString();

            Session["ListofPenginDoRep"] = ImpFinalOut;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;

            var returnField = new { Issuedata = JsonConvert.SerializeObject(ImpFinalOut), TotalDo = TotalDo, TotalDOReceived= TotalDOReceived, VehicleSent= VehicleSent, TotalGateIn= TotalGateIn, BalanceDo= BalanceDo };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            //var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }

        public ActionResult ExportToExcelListofPendingDo(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = new DataTable();
            getMovementICDNew = db.sub_GetDatatable("GetUspBookingWiseExpExcel '" + Session["fromdate"] + "','" + Session["todate"] + "'");
            
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ListofPendingDoReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> List of Pending Do Report<strong></td></tr>");
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

        public JsonResult ListDOSummary(string BookingNo)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable LorryList = new DataTable();
            DataTable ExpInList = new DataTable();
            DataSet ds = new DataSet();
            HC.DBOperations db = new HC.DBOperations();

            LorryList = db.sub_GetDatatable("USP_GetBookingSummaryLorry '" + BookingNo + "'");

            //Session["ListofPenginDoRep"] = ImpFinalOut;

            var jsonLorryList = JsonConvert.SerializeObject(LorryList);

            var returnField = new { LorryList = jsonLorryList};

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult BookingSeach()
        {

            return View();
        }

        public ActionResult BookingSeachDetails(string txtbookingNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_showTotalbooking '" + txtbookingNo + "'");
            Session["USP_Show_weighmentdetails"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult BookingSeachContainer(string Booking)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_ShowoUTDetails '" + Booking + "'");
            Session["USP_ShowoUTDetails"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult teusSearch(string Booking)
        {
            List<BE.TeusSearch> SearchInvoice = new List<BE.TeusSearch>();

            SearchInvoice = reportprovider.teusSearche(Booking);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult ExportToExcelGetTeus()
        {
            DataTable dt = (DataTable)Session["USP_ShowoUTDetails"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExportToExcelGetTeus.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Booking Wise Details<strong></td></tr>");
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

        public ActionResult CurrentDriverList()
        {

            return View();
        }


        public ActionResult GetCurrentDriverList()
        {
            DataTable TypeReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TypeReport = db.sub_GetDatatable("USP_GetCurrentDriverList");
            Session["CurrentDriverList"] = TypeReport;

            var json = JsonConvert.SerializeObject(TypeReport);

            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelCurrentDriverlist()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);

            DataTable upcomingList1 = (DataTable)Session["CurrentDriverList"];
            GridView gridview = new GridView();
            gridview.DataSource = upcomingList1;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=CurrentDriverList.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Current Driver List<strong></td></tr>");
                    //htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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

        //code by Prashnat 09 march 2020
        public ActionResult Customerandcategorywiseoutstanding()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AjaxCustomerandcategorywiseoutstanding()
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_CUST_OUTStanding_Cust_Wise 0");
            Session["CustomerWiseOutStanding"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelCustomerandCategoryDetailsreport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["CustomerWiseOutStanding"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Customer Wise OutStanding.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Customer Wise OutStanding Report<strong></td></tr>");
                    // htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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



        [HttpPost]
        public ActionResult AjaxCustomerandcategorywiseoutstanding2()
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_CUST_OUTStanding_Cust_Wise 2");
            Session["CustomerWiseOutStanding2"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelCustomerandcategorywiseDetailsreport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["CustomerWiseOutStanding2"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Customer and category wise OutStanding.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Customer and category wise OutStanding Report<strong></td></tr>");
                    // htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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


        public ActionResult CustomerAndInvoiceOutStanding()
        {
            List<BE.PartyNameEntities> CustomerMaster = new List<BE.PartyNameEntities>();
            CustomerMaster = reportprovider.Getpartyname();
            ViewBag.customer = new SelectList(CustomerMaster, "Common_ID", "GSTName");
            return View();
        }


        [HttpPost]
        public ActionResult AjaxCustomerAndInvoiceOutStanding(string Partyname)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_CUST_OUTStanding_INV_Wise '" + Partyname + "'");
            Session["CustomerAndInoiveReport"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public ActionResult ExportToExcelCustomerandInvoiceDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["CustomerAndInoiveReport"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Customer & Invoice Wise Outstanding.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Customer & Invoice Wise Outstanding Report<strong></td></tr>");
                    // htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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


        [HttpPost]
        public ActionResult AjaxCustomerandInvoicePartyName(string party)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetCustomerandgingamount_wise_Party '" + party + "'");
            Session["CustomerandpartywiseDetails"] = dt;
            Session["asonpartywise"] = DateTime.Now;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelCustomerandInvoice_partyDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["CustomerandpartywiseDetails"];
            string Tittle = "asonpartywise " + Session["asonpartywise"] + "";

            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Customer & Invoice Wise Outstanding.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Customer & Invoice Wise Outstanding Report<strong></td></tr>");
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

        public ActionResult CustomerandAgingWiseOutstanding()
        {

            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = BL.getParty();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");

            List<BE.Category> Category = new List<BE.Category>();
            Category = BL.getCategory();
            ViewBag.Category = new SelectList(Category, "ID", "CategoryName");
            

            return View();
        }

        public ActionResult GetCustomerandagingreport(string hdnPayFromId,string Category)
        {
            if (hdnPayFromId == "")
            {
                hdnPayFromId = "0";
            }

            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getMovementICDNew = db.sub_GetDatatable("USP_AGING_CUST_WISE_OS '" + hdnPayFromId + "','" + Category + "'");
            Session["Customerandageingreport"] = getMovementICDNew;
            Session["Ason"] = DateTime.Now;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult GetCustomerandageingDetailsreport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["Customerandageingreport"];
            string Tittle = "AsOn " + Session["Ason"] + "";

            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Customerandageingoutstandingreport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Customer and Aging Wise Out standing Report<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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

        public ActionResult Profile_Register()
        {
            return View();
        }


        public ActionResult GetProfit_registerDetails(BE.SalesRegisterrReport data,string ddlLocation)
        {
            DataTable SalesRegisterReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            if (data.Activity == "IMPORT")
            {
                SalesRegisterReport = db.sub_GetDatatable("SP_Import_Profit_Register '" + ddlLocation + "','"+ Convert.ToDateTime(data.Fdate).ToString("yyyyMMddHHmm") + "','" + Convert.ToDateTime(data.Todate).ToString("yyyyMMddHHmm") + "'");

            }
            if (data.Activity == "EXPORT")
            {
                SalesRegisterReport = db.sub_GetDatatable("SP_EXPORT_Profit_Register '" + ddlLocation + "','"  + data.Fdate + "','" + data.Todate + "'");

            }

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            //SalesRegisterReport = db.sub_GetDatatable("SP_SalesRegister1 '" + data.Activity + "','" + data.Fdate + "','" + data.Todate + "'");
            Session["SalesRegisterReport"] = SalesRegisterReport;
            Session["fromdate"] = data.Fdate;
            Session["todate"] = data.Todate;
            var json = JsonConvert.SerializeObject(SalesRegisterReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelProbalilityRegister()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable SalesRegisterReport = (DataTable)Session["SalesRegisterReport"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";

            GridView gridview = new GridView();
            gridview.DataSource = SalesRegisterReport;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ProbalilityRegister.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Probalility Register Report<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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
        public ActionResult DriverWisePaymentReport()
        {

            return View();
        }

        public ActionResult GetDriverWisepaymentReport(string FromDate, string ToDate, string Value)
        {
            //List<BE.DriverpaymentDetailsentities> MovementReport = new List<BE.DriverpaymentDetailsentities>();
            //MovementReport = reportprovider.GetDriverdetailssummary(FromDate, ToDate, Value);

            //var jsonResult = Json(MovementReport, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;

            DataTable DriverBilldetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverBilldetails = db.sub_GetDatatable("USP_DriverPaymentDetails '" + FromDate + "','" + ToDate + "','" + Value + "'");
            Session["ListofDriverBilldetails"] = DriverBilldetails;
            Session["Values"] = Value;
            var json = JsonConvert.SerializeObject(DriverBilldetails);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }




        public ActionResult ExportToExcelDriverWisepaymentDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["ListofDriverBilldetails"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            string Tittle = "Based On :  " + Session["Values"] + "";
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Driver Wise Payment Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Driver Wise Payment Report<strong></td></tr>");
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
        //sagar lockdown 07-05-2020
        public ActionResult ExportEmptyInventory()
        {
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = BL.getCustomer();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
            return View();
        }
        public ActionResult teusSearchStock(string AGID, string ToDate)
        {
            List<BE.TeusSearch> SearchInvoice = new List<BE.TeusSearch>();

            SearchInvoice = reportprovider.teusSearchStock(AGID, Convert.ToDateTime(ToDate).ToString("yyyyMMddHHmm"));

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult ExportEmptyInventoryReport(string AGID, string ToDate)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            ImpFinalOut = db.sub_GetDatatable("sp_GetExpEmptyStockNew '" + AGID + "','" + Convert.ToDateTime(ToDate).ToString("yyyyMMddHHmm") + "'");

            Session["ExpInvList"] = ImpFinalOut;
            //Session["fromdate"] = AGID;
            //Session["todate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ExportEmptyInventoryExcel()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);

            DataTable upcomingList1 = (DataTable)Session["ExpInvList"];
            GridView gridview = new GridView();
            gridview.DataSource = upcomingList1;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Export-Empty-Inventory.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Current Driver List<strong></td></tr>");
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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

        public ActionResult expLoadedstockReport()
        {
            List<BE.CustomerMaster> CustomerMaster = new List<BE.CustomerMaster>();
            CustomerMaster = reportprovider.getCustomerMaster();
            ViewBag.customer = new SelectList(CustomerMaster, "AGID", "AGName");
            return View();
        }
        public ActionResult teusSearchLoaded(string DepartmentID, string FromDate)
        {
            List<BE.TeusSearch> SearchInvoice = new List<BE.TeusSearch>();

            SearchInvoice = reportprovider.teusSearchLoaded(DepartmentID, Convert.ToDateTime(FromDate).ToString("yyyyMMddHHmm"));

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult GetexpLoadedstockReport(string DepartmentID, string FromDate)
        {
            if (DepartmentID == "")
            {
                DepartmentID = "0";
            }
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("Sp_ExportLoadInventory '" + DepartmentID + "','" + Convert.ToDateTime(FromDate).ToString("yyyyMMddHHmm") + "'");
            Session["ExportLoadInventory"] = dt;
            Session["CustomerID"] = DepartmentID;
            Session["FromDate"] = FromDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult ExportToExcelexpLoadedstockReport()
        {

            DataTable dt = (DataTable)Session["ExportLoadInventory"];

            HC.DBOperations db = new HC.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ExportLoadInventory = dt;
            string Tittle = "As On" + Session["FromDate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = ExportLoadInventory;
            gridview.DataBind();
            ////////gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExportLoadedInventory.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Loaded Inventory <strong></td></tr>");
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
        public ActionResult DailyCartingReport()
        {
            List<BE.CriteriaEntities> CriteriaList = new List<BE.CriteriaEntities>();
            CriteriaList = reportprovider.GetCriteria();
            ViewBag.CriteriaList = new SelectList(CriteriaList, "ID", "Criteria");

            return View();
        }
        [HttpPost]
        public JsonResult GETDailyCartingReport(string fromdate, string TOdate, string Search, string name)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("sp_get_CartingReport'" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(TOdate).ToString("yyyy-MM-dd HH:mm") + "','" + Search + "','" + name + "'");
            Session["DailyCartingReports"] = dt;
            Session["fromdate"] = fromdate;
            Session["todate"] = TOdate;
            Session["Search"] = Search;
            Session["name"] = name;
            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult ExportToExcelDailyCartingReports()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getDailyCartingReportsNew = (DataTable)Session["DailyCartingReports"];
            GridView gridview = new GridView();
            gridview.DataSource = getDailyCartingReportsNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Daily Carting Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Daily Carting Report<strong></td></tr>");
                    // htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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

        public ActionResult ExportTruckInReport()
        {
            List<BE.CustomerMaster> CustomerMaster = new List<BE.CustomerMaster>();
            CustomerMaster = reportprovider.getCustomerMaster();
            ViewBag.customer = new SelectList(CustomerMaster, "AGID", "AGName");
            return View();
        }

        public JsonResult AjaxgetExportTruckInReport(string FromDate, string ToDate, string DepartmentID)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            if (DepartmentID == "")
            {
                DepartmentID = "0";
            }

            dt = db.sub_GetDatatable("get_sp_AgencyWiseVehicleDtls '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + DepartmentID + "'");
            Session["AgencyWiseVehicleDtls"] = dt;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            Session["DepartmentID"] = DepartmentID;
            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;


        }

        public JsonResult AjaxgetExportTruckInCount(string FromDate, string ToDate, string DepartmentID)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            if (DepartmentID == "")
            {
                DepartmentID = "0";
            }

            dt = db.sub_GetDatatable("get_sp_AgencyWiseCount '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + DepartmentID + "'");
            Session["AgencyWiseVehicleDtls"] = dt;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            Session["DepartmentID"] = DepartmentID;
            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;


        }
        public ActionResult ExportToExcelgetExportTruckInReport()
        {

            DataTable dt = (DataTable)Session["AgencyWiseVehicleDtls"];

            HC.DBOperations db = new HC.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable AgencyWiseVehicleDtls = (DataTable)Session["AgencyWiseVehicleDtls"];
            string Tittle = "As On" + Session["txtentryDate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = AgencyWiseVehicleDtls;
            gridview.DataBind();
            ////////gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Export Truck In Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Truck In Report<strong></td></tr>");
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
        public ActionResult PendingContainerForBilling()
        {
            List<BE.CustomerMaster> CustomerMaster = new List<BE.CustomerMaster>();
            CustomerMaster = reportprovider.getCustomerMaster();
            ViewBag.customer = new SelectList(CustomerMaster, "AGID", "AGName");
            return View();
        }
        public ActionResult GetPendingContainerDetails(string Searchby, string Customer)
        {
            DataTable SalesRegisterReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            SalesRegisterReport = db.sub_GetDatatable("Get_SP_PendingCtrBill '" + Searchby + "','" + Customer + "'");
            Session["PendingContainerforbilling"] = SalesRegisterReport;

            var json = JsonConvert.SerializeObject(SalesRegisterReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelPendingContainerDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["PendingContainerforbilling"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Pending Container For Billing Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Pending Container For Billing Report<strong></td></tr>");
                    // htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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
        public ActionResult PendingContainerAgain()
        {
            return View();
        }
        public JsonResult PendingContainerAgainZeroJoA()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("Sp_UpdateJODateList ");
            Session["PendingContainerAgainZeroJoA"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelPendingContainerAgainZeroJoA()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["PendingContainerAgainZeroJoA"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Pending Container Again Zero Jo.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Pending Container Again Zero Jo<strong></td></tr>");

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
        public ActionResult GetCreditNoteCancelSummary(string ddlActivity, string ddlGSTName, string fromdate, string todate, string CFor)
        {
            DataTable GetCreditNoteSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetCreditNoteSummary = db.sub_GetDatatable("USP_CATEGORY_WISE_CREDIT_NOTE_CANCEL_SUMMARY '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm") + "','" + ddlActivity + "','" + ddlGSTName + "','" + CFor + "'");
            Session["GetCreditNoteSummary"] = GetCreditNoteSummary;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            var json = JsonConvert.SerializeObject(GetCreditNoteSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        //sagar

        //sagar lockdown 2020-07-05
        public ActionResult CancelLR()
        {
            return View();
        }

        //public JsonResult CancelReceiptInvoiceReport(string ReceiptNo, string WorkYear)
        //{
        // int Userid = Convert.ToInt32(Session["Tracker_userID"]);
        // DataTable ImpFinalOut = new DataTable();
        // HC.DBOperations db = new HC.DBOperations();

        // ImpFinalOut = db.sub_GetDatatable("GetreceiptinvoiceD '" + ReceiptNo + "','" + WorkYear + "'");

        // Session["ExpInvList"] = ImpFinalOut;
        // //Session["fromdate"] = AGID;
        // //Session["todate"] = ToDate;
        // var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
        // jsonResult.MaxJsonLength = int.MaxValue;
        // return jsonResult;
        //}

        public JsonResult CancelReceiptLRReport(string LRNo)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            ImpFinalOut = db.sub_GetDatatable("CancelLR '" + LRNo + "'");

            Session["ExpInvList"] = ImpFinalOut;
            var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult CancelLRWithReason(string LRNo)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("Usp_CancelLR '" + LRNo + "','" + userId + "'");

            if (retVal > 0)
            {
                Message = "LR Cancel Successfully.";
            }
            else
            {
                Message = "LR Not Cancel Successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult CategoryAndHeadWiseReport()
        {
            List<BE.PartyNameEntities> CustomerMaster = new List<BE.PartyNameEntities>();
            CustomerMaster = reportprovider.Getpartyname();
            ViewBag.customer = new SelectList(CustomerMaster, "Common_ID", "GSTName");
            return View();
        }

        [HttpPost]
        public ActionResult GetCategoryAndHeadWiseReport(string FromDate, string ToDate, string category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            if (category == "IMPORT")
            {
                dt = db.sub_GetDatatable("Get_sp_ImportWiseCollectionSummary '" + FromDate + "','" + ToDate + "'");

            }

            if (category == "EXPORT")
            {
                dt = db.sub_GetDatatable("Get_sp_EXPORTWiseCollectionSummary '" + FromDate + "','" + ToDate + "'");

            }
            if (category == "BOND")
            {
                dt = db.sub_GetDatatable("Get_sp_BONDWiseCollectionSummary '" + FromDate + "','" + ToDate + "'");

            }
            if (category == "DOMESTIC")
            {
                dt = db.sub_GetDatatable("Get_sp_DOMESTICWiseCollectionSummary  '" + FromDate + "','" + ToDate + "'");

            }
            if (category == "MISC")
            {
                dt = db.sub_GetDatatable("Get_sp_MISCWiseCollectionSummary '" + FromDate + "','" + ToDate + "'");

            }

            if (category == "MNR")
            {
                dt = db.sub_GetDatatable("Get_sp_MNRWiseCollectionSummary '" + FromDate + "','" + ToDate + "'");

            }

            Session["TDSSummaryList"] = dt;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            Session["category"] = category;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelCategoryAndHeadWiseReport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["TDSSummaryList"];
            string Tittle = "FromDate " + Session["FromDate"] + "ToDate " + Session["ToDate"] + " " + "category " + Session["category"] + "";

            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=CategoryAndHeadWiseReport Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Category And Head Wise Report<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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

        public ActionResult ContainerMaster()
        {
            List<BE.PartyNameEntities> VendorMaster = new List<BE.PartyNameEntities>();
            VendorMaster = reportprovider.VendorFill();
            ViewBag.VendorFill = new SelectList(VendorMaster, "transporterID", "vendorname");
            return View();
          
        }

        public ActionResult GetShowFCLDestuffTallyList(String FromDate, string ToDate, string Transport,string ddlSearch, string Search)
        {
            if (Transport == "")
            {
                Transport = "0";
            }

            DataTable dtFCLDestuffTallyList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            dtFCLDestuffTallyList = db.sub_GetDatatable("exec get_sp_BillSummary' " + FromDate + "','" + ToDate + "','" + Transport + "','" + ddlSearch + "','" + Search +  "'");
            Session["get_sp_BillSummary"] = dtFCLDestuffTallyList;

            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dtFCLDestuffTallyList);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        public ActionResult ExportTransport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dtFCLDestuffTallyList = (DataTable)Session["get_sp_BillSummary"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Transport Summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>TransportSummary<strong></td></tr>");

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


        public ActionResult ACKNOWLEDGEMENTEInvoice()
        {
            return View();
        }

        public ActionResult GetTrackACKNOWLEDGEMENTEInvoiceReport(BE.SalesRegisterrReport data)
        {
            // List<BE.SalesRegisterrReport> SalesRegisterReport = new List<BE.SalesRegisterrReport>();
            //SalesRegisterReport = reportprovider.getSalesRegisterrReportList(data.Activity, data.Fdate, data.Todate);
            // return Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            //var jsonResult = Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
            DataTable SalesRegisterReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            SalesRegisterReport = db.sub_GetDatatable("Sales_GST_ACK_List_Request_M '" + data.Activity + "','" + data.Fdate + "'");
            Session["Sales_GST_ACK_List_Request"] = SalesRegisterReport;
            Session["fromdate"] = data.Fdate;
            Session["todate"] = data.Todate;
            var json = JsonConvert.SerializeObject(SalesRegisterReport);
            int i = 0;
            int total = 0;
            for (i = 0; i < json.Length; i++)
            {
                //total = total + js
            }
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult GetTrackACKNOWLEDGEMENTEInvoiceReportTextBoxbind(BE.SalesRegisterrReport data)
        {
            List<BE.SalesRegisterrReport> SalesRegisterReport = new List<BE.SalesRegisterrReport>();
            SalesRegisterReport = reportprovider.getEInvoiceReportList(data.Activity, data.Fdate);
            var jsonResult = Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public JsonResult Ackresult(string Category, string InvoiceNo)
        {
            DataTable tblInvoiceList = new DataTable();
            DataTable tblcheckInvoicePin = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            string Message = "";



            //tblcheckInvoicePin = db.sub_GetDatatable("USP_Validate_Invoice_PIN_Code  '" + InvoiceNo + "'");
            //if (tblcheckInvoicePin.Rows.Count > 0)
            //{
            //    Message = tblcheckInvoicePin.Rows[0]["msg"].ToString();
            //}
            //else
            //{
            tblInvoiceList = db.sub_GetDatatable("Sales_GST_ACK_Posting '" + Category + "','" + InvoiceNo + "','" + userId + "'");
            if (tblInvoiceList.Rows.Count > 0)
            {
                Message = tblInvoiceList.Rows[0]["msg"].ToString();
            }
            //}

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveMultpleAsk(List<BE.CategorywiseInvoice> Invoicedata)
        {


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("Category");
            foreach (BE.CategorywiseInvoice item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["InvoiceNo"] = item.InvoiceNo;
                row["Category"] = item.Category;

                dataTable.Rows.Add(row);
            }
            DataTable tblInvoiceList = new DataTable();
            DataTable ChecktblInvoiceList = new DataTable();
            string Message = "";
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            //if (dataTable.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dataTable.Rows)
            //    {
            //        ChecktblInvoiceList = db.sub_GetDatatable("USP_Validate_Invoice_PIN_Code  '" + row["InvoiceNo"] + "'");
            //        if (ChecktblInvoiceList.Rows.Count > 0)
            //        {
            //            Message = ChecktblInvoiceList.Rows[0]["msg"].ToString();
            //        }
            //    }

            //}


            //if (Message == "")
            //{
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    tblInvoiceList = db.sub_GetDatatable("Sales_GST_ACK_Posting '" + row["Category"] + "','" + row["InvoiceNo"] + "','" + Userid + "'");
                    if (tblInvoiceList.Rows.Count > 0)
                    {
                        Message = tblInvoiceList.Rows[0]["msg"].ToString();
                    }
                }

            }
            //}

            return Json(Message);

        }
        public ActionResult GetTrackACKNOWLEDGEMENTEInvoiceError(string FromDate, string Todate)
        {
            // List<BE.SalesRegisterrReport> SalesRegisterReport = new List<BE.SalesRegisterrReport>();
            //SalesRegisterReport = reportprovider.getSalesRegisterrReportList(data.Activity, data.Fdate, data.Todate);
            // return Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            //var jsonResult = Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
            DataTable SalesRegisterReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            SalesRegisterReport = db.sub_GetDatatable("USP_GST_ACK_Errors_Logs '" + FromDate + "','" + Todate + "'");
            Session["USP_GST_ACK_Errors_Logs"] = SalesRegisterReport;
            //Session["fromdate"] = data.Fdate;
            //Session["todate"] = data.Todate;
            var json = JsonConvert.SerializeObject(SalesRegisterReport);
            int i = 0;
            int total = 0;
            for (i = 0; i < json.Length; i++)
            {
                //total = total + js
            }
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult GetTrackACKNOWLEDGEMENTEError()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dtFCLDestuffTallyList = (DataTable)Session["USP_GST_ACK_Errors_Logs"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Error Summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>ErrorSummary<strong></td></tr>");

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

        public ActionResult GetTrackACKNOWLEDGEMENTEInvoiceSuccess(string FromDate, string Todate)
        {
            // List<BE.SalesRegisterrReport> SalesRegisterReport = new List<BE.SalesRegisterrReport>();
            //SalesRegisterReport = reportprovider.getSalesRegisterrReportList(data.Activity, data.Fdate, data.Todate);
            // return Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            //var jsonResult = Json(SalesRegisterReport, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
            DataTable SalesRegisterReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            SalesRegisterReport = db.sub_GetDatatable("USP_GST_ACK_success_Logs '" + FromDate + "','" + Todate + "'");
            Session["USP_GST_ACK_success_Logs"] = SalesRegisterReport;
            //Session["fromdate"] = data.Fdate;
            //Session["todate"] = data.Todate;
            var json = JsonConvert.SerializeObject(SalesRegisterReport);
            int i = 0;
            int total = 0;
            for (i = 0; i < json.Length; i++)
            {
                //total = total + js
            }
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult GetTrackACKNOWLEDGEMENTESuccess()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dtFCLDestuffTallyList = (DataTable)Session["USP_GST_ACK_success_Logs"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=success Summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>successSummary<strong></td></tr>");

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

        public ActionResult GetDaywiseDetails(string Activity, string FromDate, string Todate)
        {
            DataTable TypeReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");



            TypeReport = db.sub_GetDatatable("SP_GST_ACK_Summary'" + FromDate + "','" + Todate + "'");
            Session["GSTAsksummarydetails"] = TypeReport;

            var json = JsonConvert.SerializeObject(TypeReport);

            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetGST_Ack_SummaryDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dtFCLDestuffTallyList = (DataTable)Session["GSTAsksummarydetails"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=GST_Ack_SummaryDetail.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>GST_Ack_Report<strong></td></tr>");

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


        public JsonResult AckCloseResult(string AutoID, string InvoiceNo)
        {
            DataTable tblInvoiceList = new DataTable();
            DataTable tblcheckInvoicePin = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            string Message = "";

            tblInvoiceList = db.sub_GetDatatable("USP_CLosed_ACK_Errors '" + AutoID + "','" + InvoiceNo + "'");



            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        public ActionResult CustomeDetailsReport()
        {
            return View();
        }

        public ActionResult CustomeDetailsReports(string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Custom_Data_Export'" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["ExpStuffingWise"] = dt;
            Session["fromdate"] = fromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult CustomeExportToExcel()
        {
            DataTable dt = (DataTable)Session["ExpStuffingWise"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=CustomeDetailsReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Stuffing Report <strong></td></tr>");
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

        public ActionResult FullMovementReport()
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            ViewBag.startDate = Convert.ToDateTime(startDate).ToString("dd MMM yyyy 08:00");
            ViewBag.EndDate = Convert.ToDateTime(endDate).ToString("dd MMM yyyy 08:00");
            return View();
        }

        public JsonResult GetICDFullImportExportMovementSeparate(string FromDate, string ToDate)
        {
            BE.RevenueAlloperationEntities ICDFullImportExport = new BE.RevenueAlloperationEntities();
            ICDFullImportExport = reportprovider.GetICDFullImportExportSeparate(FromDate, ToDate);
            var jsonResult = Json(ICDFullImportExport, JsonRequestBehavior.AllowGet);
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult ExportToExcelFullMovementReport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable OnwordDelivery = new DataTable();

            string Tittle = "From " + Session["FromDate"] + " To " + Session["ToDate"] + ".";
            DataTable Import_Port = db.sub_GetDatatable("Import_PortWise_movement_tues_report_MIS_NEW '" + Session["FromDate"] + "','" + Session["ToDate"] + "'");
            DataTable Export_Movement = db.sub_GetDatatable("Export_PortWise_movement_tues_report_MIS_NEW '" + Session["FromDate"] + "','" + Session["ToDate"] + "'");
            DataTable Exporthaxira = db.sub_GetDatatable("EmptyRepoPort_hazira_movement_tues_report_MIS_new '" + Session["FromDate"] + "','" + Session["ToDate"] + "'");








            GridView gridview = new GridView();
            GridView gridview1 = new GridView();
            GridView gridview2 = new GridView();
            GridView gridview3 = new GridView();

            gridview1.DataSource = Import_Port;
            gridview1.DataBind();
            gridview2.DataSource = Export_Movement;
            gridview2.DataBind();
            gridview3.DataSource = Exporthaxira;
            gridview3.DataBind();

            if (gridview1.Rows.Count != 0)
            {
                PrepareForExport(gridview1);
            }
            if (gridview2.Rows.Count != 0)
            {
                PrepareForExport(gridview2);

            }
            if (gridview3.Rows.Count != 0)
            {
                PrepareForExport(gridview3);

            }



            Table tb = new Table();
            TableRow tr1 = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Text = "Import Done";
            tr1.Cells.Add(cell1);
            tr1.Style.Add("background-color", "Green");
            tr1.Style.Add("color", "White");

            // For Empty Row 

            TableRow tr2 = new TableRow();
            TableCell cell2 = new TableCell();
            cell2.Controls.Add(gridview1);
            tr2.Cells.Add(cell2);





            TableRow tr3 = new TableRow();
            TableCell cell3 = new TableCell();

            cell3.Text = "Export Done";
            tr3.Cells.Add(cell3);
            tr3.Style.Add("background-color", "Green");
            tr3.Style.Add("color", "White");


            // For Empty Row 

            TableRow tr4 = new TableRow();
            TableCell cell4 = new TableCell();
            cell4.Controls.Add(gridview2);
            tr4.Cells.Add(cell4);

            TableRow tr5 = new TableRow();
            TableCell cell5 = new TableCell();
            cell5.Text = "Export Repo(Port & hazira)";
            tr5.Cells.Add(cell5);
            tr5.Style.Add("background-color", "Green");
            tr5.Style.Add("color", "White");

            // For Empty Row 

            TableRow tr6 = new TableRow();
            TableCell cell6 = new TableCell();

            cell6.Controls.Add(gridview3);
            tr6.Cells.Add(cell6);







            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.Rows.Add(tr3);
            tb.Rows.Add(tr4);
            tb.Rows.Add(tr5);
            tb.Rows.Add(tr6);


            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=FullMovementReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Full Movement Report<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    tb.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        public ActionResult ImportCustomDetailsReport()
        {
            return View();
        }


        public ActionResult GetImportCustData(string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_IMP_CUST_Data2  '" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["ExpStuffingWise"] = dt;
            Session["fromdate"] = fromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToImportCustReport()
        {
            DataTable dt = (DataTable)Session["ExpStuffingWise"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ImportCustomeDetailsReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Import Custom Details <strong></td></tr>");
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

        public ActionResult ExportCustomDetailsRep()
        {
            return View();
        }


        public ActionResult GetExportCustData(string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_exportcustreport  '" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["ExpStuffingWise"] = dt;
            Session["fromdate"] = fromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExportCustReport()
        {
            DataTable dt = (DataTable)Session["ExpStuffingWise"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExportCustomeDetailsReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Custom Details <strong></td></tr>");
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

        public ActionResult VendorBillDateWise()
        {

            return View();
        }

        public ActionResult GetVendorBillDateWise(string FromDate, string ToDate, string Value)
        {
            DataTable SalesRegisterReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            if (Value == "Amount")
            {
                SalesRegisterReport = db.sub_GetDatatable("USP_GetVendorBillDateWise '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
                Session["ListOFVendorbilldatewise"] = SalesRegisterReport;
                Session["Values"] = "Amount";
            }

            if (Value == "Liter")
            {
                SalesRegisterReport = db.sub_GetDatatable("USP_GetVendorBillLiterDateWise '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
                Session["ListOFVendorbilldatewise"] = SalesRegisterReport;
                Session["Values"] = "Liter";
            }


            var json = JsonConvert.SerializeObject(SalesRegisterReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult ExportToExcelVendorBilldateWise()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["ListOFVendorbilldatewise"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            string Tittle = "Based On :  " + Session["Values"] + "";
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Vendor Bill Date Wise.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Vendor Bill Date Wise Report<strong></td></tr>");
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

        public ActionResult InvoicePrint(string InvoiceNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblHeadDetails = new DataTable();
            DataTable tblChargesDetails = new DataTable();
            DataTable tblWordAmount = new DataTable();
            DataTable tblBankDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            double Amount = 0;
            double Discount = 0;
            double NetAmount = 0;
            string Srate = "";
            double SGST = 0;
            string CRate = "";
            double CGST = 0;
            string IRate = "";
            double IGST = 0;

            getJobOrderSet = db.sub_GetDataSets("getInvoicePrintMVC '" + InvoiceNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblInvoiceDetails = getJobOrderSet.Tables[1];
                tblContainerDetails = getJobOrderSet.Tables[2];
                tblHeadDetails = getJobOrderSet.Tables[3];
                tblChargesDetails = getJobOrderSet.Tables[4];
                tblWordAmount = getJobOrderSet.Tables[5];
                tblBankDetails = getJobOrderSet.Tables[6];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.CINNo = dr["CINNo"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.Addressl = dr["AddressI"];
                    ViewBag.IMP_AC_EMAILID = dr["IMP_AC_EMAILID"];
                    ViewBag.Datetime = Convert.ToDateTime(DateTime.Now).ToString("dd MM yyyy");
                    ViewBag.TIme = Convert.ToDateTime(DateTime.Now).ToString("HH:mm");
                }
                foreach (DataRow dr in tblInvoiceDetails.Rows)
                {
                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.InvoiceDate = dr["AssessDate"];
                    ViewBag.Name = dr["GSTName"];
                    ViewBag.GSTAddress = dr["GSTAddress"];
                    ViewBag.GSTIN = dr["GSTIn_uniqID"];
                    ViewBag.PlaceofSupply = dr["State"];
                    ViewBag.Consignee = dr["Consignee"];
                    ViewBag.Address = dr["Con_IGMAddress"];
                    ViewBag.ShippingLine = dr["SLName"];
                    ViewBag.BOENo = dr["BOEno"];
                    ViewBag.IgmItemNo = dr["IGMNo"] + "/" + dr["ItemNo"];
                    ViewBag.BLNo = dr["IGM_BLNo"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.CargoWeight = dr["GrossWt"];
                    ViewBag.CargoDesc = dr["IGM_GoodsDesc"];
                    ViewBag.Movement = dr["Movement"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.Irn = dr["Irn"];
                    ViewBag.AckNo = dr["AckNo"];
                    ViewBag.AckDt = dr["AckDt"];
                    ViewBag.SignedQRcode = dr["SignedQRcode"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.TCSAmt = dr["TCSAmt"];
                    ViewBag.INVheader = dr["INVheader"];
                }

                ViewBag.BankName = tblBankDetails.Rows[0]["BankName"];
                ViewBag.AccountNo = tblBankDetails.Rows[0]["AccountNo"];
                ViewBag.IFSCCode = tblBankDetails.Rows[0]["IFSCCode"];
                ViewBag.Remarks = tblBankDetails.Rows[0]["Remarks"];
                ViewBag.BranchName = tblBankDetails.Rows[0]["BranchName"];

                ViewBag.TotalAmountInWord = tblWordAmount.Rows[0]["TotalAmountInWords"];
                ViewBag.TotalAmount = tblWordAmount.Rows[0]["GrandTotal"];
                ViewBag.AmountWithoutTax = tblWordAmount.Rows[0]["NetTotal"];
                ViewBag.SGSTAmount = tblWordAmount.Rows[0]["SGST"];
                ViewBag.CGSTAmount = tblWordAmount.Rows[0]["CGST"];
                ViewBag.IGSTAmount = tblWordAmount.Rows[0]["IGST"];
                
                ViewBag.TaxAmount = Convert.ToDouble(Convert.ToDouble(tblWordAmount.Rows[0]["SGST"]) + Convert.ToDouble(tblWordAmount.Rows[0]["CGST"]) + Convert.ToDouble(tblWordAmount.Rows[0]["IGST"]));
            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();
            ViewBag.HeadDetailsList = tblHeadDetails.AsEnumerable();
            ViewBag.ChargesDetailsList = tblChargesDetails.AsEnumerable();

            foreach (DataRow data in tblChargesDetails.Rows)
            {
                Amount = Convert.ToDouble(data["Amount"]);
                Discount = Convert.ToDouble(data["Discount"]);
                NetAmount = Convert.ToDouble(data["NetAmount"]);
                Srate = Convert.ToString(data["Srate"]);
                CRate = Convert.ToString(data["CRate"]);
                IRate = Convert.ToString(data["IRate"]);
                SGST = Convert.ToDouble(data["SGST"]);
                CGST = Convert.ToDouble(data["CGST"]);
                IGST = Convert.ToDouble(data["IGST"]);
            }

            ViewBag.Amount = Amount;
            ViewBag.Discount = Discount;
            ViewBag.NetAmount = NetAmount;
            ViewBag.Srate = Srate;
            ViewBag.CRate = CRate;
            ViewBag.IRate = IRate;
            ViewBag.SGST = SGST;
            ViewBag.CGST = CGST;
            ViewBag.IGST = IGST;

            return PartialView();

        }


        public ActionResult CancelCtrReport()
        {
            return View();
        }

        public ActionResult VesselAndCustomerWisePendency()
        {
            List<BE.VesselandCustomerWisePendency> ICDExportVesselWiseCustomerPendency = new List<BE.VesselandCustomerWisePendency>();
            ICDExportVesselWiseCustomerPendency = reportprovider.GetVesselCustomerWisePendency();
            return View(ICDExportVesselWiseCustomerPendency);
        }

        public ActionResult VesselWiseMovementPendency()
        {
            List<BE.VesselandCustomerWisePendency> ICDExportVesselWisePendency = new List<BE.VesselandCustomerWisePendency>();
            ICDExportVesselWisePendency = reportprovider.GetVesselWisePendency();
            return PartialView(ICDExportVesselWisePendency);
        }
        public ActionResult CustomerWiseMovementDetails()
        {
            List<BE.VesselandCustomerWisePendency> ICDExportCustomerWisePendency = new List<BE.VesselandCustomerWisePendency>();
            ICDExportCustomerWisePendency = reportprovider.GetCustomerWisePendency();
            return PartialView(ICDExportCustomerWisePendency);
        }

        public ActionResult ShippingBillWiseSearch()
        {

            List<BE.CustomerMaster> CustomerMaster = new List<BE.CustomerMaster>();
            CustomerMaster = reportprovider.getCustomerMaster();
            ViewBag.customer = new SelectList(CustomerMaster, "AGID", "AGName");

            List<BE.Shippers> ExpShipper = new List<BE.Shippers>();
            List<BE.CHA> CHAList = new List<BE.CHA>();
            List<BE.Consignee> ImporterMasterList = new List<BE.Consignee>();

            ExpShipper = BL.getShippers();
            CHAList = BL.getCHA();


            ViewBag.ExpShipper = new SelectList(ExpShipper, "shipperID", "shippername");
            ViewBag.CHA = new SelectList(CHAList, "CHANo", "CHAName");

            return View();
        }


        public JsonResult GetExportReportDetails(string searchon, string Value, string Fromdate, string Todate)
        {

            DataTable GetList = new DataTable();
            GetList = reportprovider.GetExportReportDetails(searchon, Value, Fromdate, Todate);



            var json = JsonConvert.SerializeObject(GetList);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ajaxExportSBEntryidList(string SBNumber)
        {
            List<BE.JobOrderDEntities> Shippers = new List<BE.JobOrderDEntities>();
            Shippers = reportprovider.getExportSBENtryidlist(SBNumber);
            return new JsonResult() { Data = Shippers, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult GetShippingBillWiseSearch(string Containerno, string Jono)
        {
            List<BE.ShippingBillWiseSearch> CFSImportExport = new List<BE.ShippingBillWiseSearch>();
            CFSImportExport = reportprovider.GetShippingBillExportWiseContainerDetails(Containerno, Jono);
            List<BE.ShippingBillWiseSearch> Containertypedetails = new List<BE.ShippingBillWiseSearch>();
            Containertypedetails = reportprovider.GetSBNoCartingDetailsDetails(Containerno, Jono);

            //List<BE.ExportContainerSearchEntities> ContainerSlipNo = new List<BE.ExportContainerSearchEntities>();
            //ContainerSlipNo = reportprovider.GetExportWiseContainerSlipNoDetails(Containerno, Jono);


            var returnField = new { ExportReport = CFSImportExport, CartingDetails = Containertypedetails };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult GetShoppingBillDEtail(string ContainerNo, string Jono, string tableName)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("GET_Sp_EXPORT_SBSearch_Summary '" + ContainerNo + "','" + Jono + "','" + tableName + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["ListofExcelShippingBillsDetails"] = getMovementICDNew;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelShippingBillsDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["ListofExcelShippingBillsDetails"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            string Tittle = "Based On :  " + Session["Values"] + "";
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Shipping Bill Date Wise.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Shipping Bill  Wise Report<strong></td></tr>");
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
        public ActionResult GetExportShippingBillingDetails(string sbno, string entryid)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("USP_GetExportShippingBill_Details '" + sbno + "','" + entryid + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["ListofExcelShippingBillsDetails"] = getMovementICDNew;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportContainerWiseSearch()
        {
            return View();
        }

        public JsonResult ajaxEntryidList(string ContainerNo3)
        {
            List<BE.JobOrderDEntities> Shippers = new List<BE.JobOrderDEntities>();
            Shippers = reportprovider.getENtryidlist(ContainerNo3);
            return new JsonResult() { Data = Shippers, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult GetExportContainerDetails(string Containerno, string Jono)
        {
            List<BE.ExportContainerSearchEntities> CFSImportExport = new List<BE.ExportContainerSearchEntities>();
            CFSImportExport = reportprovider.GetExportWiseContainerDetails(Containerno, Jono);
            List<BE.ExportContainerSearchEntities> Containertypedetails = new List<BE.ExportContainerSearchEntities>();
            Containertypedetails = reportprovider.GetExportWiseContainerTypeDetails(Containerno, Jono);

            List<BE.ExportContainerSearchEntities> ContainerSlipNo = new List<BE.ExportContainerSearchEntities>();
            ContainerSlipNo = reportprovider.GetExportWiseContainerSlipNoDetails(Containerno, Jono);


            var returnField = new { ExportReport = CFSImportExport, ContainerType = Containertypedetails, getContainerSlipNo = ContainerSlipNo };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult GetExportSearchReportDetails(string ContainerNo, string Jono, string tableName)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("GET_Sp_EXPORT_ContainerNo_Summary '" + ContainerNo + "','" + Jono + "','" + tableName + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["ListOFExportSearchDetails"] = getMovementICDNew;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult GetExportSearchHoldReportDetails(string ContainerNo, string Jono)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("USP_GetExportSearchHoldDetails '" + ContainerNo + "','" + Jono + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["ListofExportSearchHoldingDetails"] = getMovementICDNew;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportTimeline(string ContainerNo, string Jono)
        {
            string ContainerNumber = Convert.ToString(ContainerNo);
            string Jonumber = Convert.ToString(Jono);
            List<BE.ImportSearchEntities> TimeLineList = new List<BE.ImportSearchEntities>();
            TimeLineList = trainTrackerProvider.GetTimeLineExport(ContainerNumber, Jonumber);
            return PartialView(TimeLineList.ToList());
        }

        public ActionResult ContainerTimeline(string ContainerNo, string Jono)
        {
            string ContainerNumber = Convert.ToString(ContainerNo);
            string Jonumber = Convert.ToString(Jono);
            List<BE.ImportSearchEntities> TimeLineList = new List<BE.ImportSearchEntities>();
            TimeLineList = trainTrackerProvider.GetTimeLineContainer(ContainerNumber, Jonumber);
            return PartialView(TimeLineList.ToList());
        }

        public ActionResult ConsolidateActivitySummary()
        {
            return View();
        }

        public ActionResult GetConsolidateActivitySummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_ALL_DATA'" + FromDate + "','" + ToDate + "'");
            Session["USP_ALL_DATA"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }


        public ActionResult ExportToExcelConsolidateActivitySummary()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["USP_ALL_DATA"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ConsolidateActivitySummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Consolidate Activity Summaryt<strong></td></tr>");
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

        public ActionResult BankRecoData()
        {
            List<BE.BankMaster> CustomerMaster = new List<BE.BankMaster>();
            CustomerMaster = reportprovider.getBankMaster();
            ViewBag.customer = new SelectList(CustomerMaster, "AccountID", "AccountNumber");

            List<BE.BankMaster> BakName = new List<BE.BankMaster>();
            BakName = reportprovider.getBankName();
            ViewBag.BakName = new SelectList(BakName, "AccountID", "BankName");


            return View();
        }

        public JsonResult ImportFile()
        {
            int Userid = Convert.ToInt32(Session["userid"]);


            EN.BankMasterEntites DriverPaymentList = new EN.BankMasterEntites();
            List<EN.BankMasterEntites> DriverPaymentRecoList = new List<EN.BankMasterEntites>();
            if (Request.Files.Count > 0)
            {
                try
                {

                    // Get all files from Request object
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

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

                        // Get the complete folder path and store the file inside it.
                        fname = Path.Combine(Server.MapPath("~/ImportFile/"), fname);
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
                       
                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                             
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

                                    DataTable DriverPaymentDT = new DataTable();


                                    DriverPaymentDT.Columns.Add("TransDate");
                                    DriverPaymentDT.Columns.Add("Description");
                                    DriverPaymentDT.Columns.Add("RefNo");
                                    DriverPaymentDT.Columns.Add("Debit");
                                    DriverPaymentDT.Columns.Add("Credit");
                                    DriverPaymentDT.Columns.Add("Balance");

                                    int irow = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        ///string accNo = row[0].ToString();

                                        if (irow >= 10)
                                        {
                                            DataRow dar = DriverPaymentDT.NewRow();

                                            dar["TransDate"] = row[0].ToString().ToUpper();
                                            dar["Description"] = row[2].ToString().ToUpper();
                                            dar["RefNo"] = row[3].ToString();
                                            dar["Debit"] = row[5].ToString();
                                            dar["Credit"] = row[6].ToString();
                                            dar["Balance"] = row[7].ToString();

                                            DriverPaymentDT.Rows.Add(dar);
                                            int linenum = dt.Rows.IndexOf(row);
                                            if (linenum == 1050)
                                            {
                                                int count1 = 0;
                                            }

                                        }

                                        irow++;
                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }
                                    }
 

                                    if (DriverPaymentDT != null)
                                    {
                                        DriverPaymentRecoList = reportprovider.UpdateVoucherPaymentDetails(DriverPaymentDT, Userid);
                                        string json = JsonConvert.SerializeObject(DriverPaymentRecoList);
                                        var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;


                                    }
                                }
                            }
                        }

                    }

                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    // Console.WriteLine("Error occurred. Error details: " + ex.Message);
                    // BL.AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'",""));
                    // return Json("Error occurred. Error details: " + ex.Message);
                    return Json(1);
                    // return Json("FormData is not supported.");
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public JsonResult BankRecoValidations(List<BE.BankMasterEntites> Invoicedata, string AccountNumber)
        {
            string Message = "";
            string message1 = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("TransDate");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("RefNo");
            dataTable.Columns.Add("Debit");
            dataTable.Columns.Add("Credit");
            dataTable.Columns.Add("Balance");

            foreach (BE.BankMasterEntites item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["TransDate"] = Convert.ToDateTime(item.TransDate).ToString("dd MMM yyyy HH:mm");
                row["Description"] = item.Description;
                row["RefNo"] = item.RefNo;
                row["Debit"] = item.Debit;
                row["Credit"] = item.Credit;
                row["Balance"] = item.Balance;

                dataTable.Rows.Add(row);
            }


            Message = reportprovider.BankRecoValidation(dataTable);

            if (Message != "")
            {

                message1 += Message;
            }
            else
            {
                message1 = "New";
            }
            return new JsonResult() { Data = message1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult SaveBankRecoDetails(List<BE.BankMasterEntites> Invoicedata, string AccountNumber)
        {
            string Message = "";
            string message1 = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("TransDate");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("RefNo");
            dataTable.Columns.Add("Debit");
            dataTable.Columns.Add("Credit");
            dataTable.Columns.Add("Balance");

            foreach (BE.BankMasterEntites item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["TransDate"] = Convert.ToDateTime(item.TransDate).ToString("dd MMM yyyy HH:mm");
                row["Description"] = item.Description;
                row["RefNo"] = item.RefNo;
                row["Debit"] = item.Debit;
                row["Credit"] = item.Credit;
                row["Balance"] = item.Balance;

                dataTable.Rows.Add(row);

                int i = new int();
                HC.DBOperations db1 = new HC.DBOperations();
                i = db1.sub_ExecuteNonQuery("USP_INSERT_BANK_RECO_DATA_M '" + row["TransDate"] + "','" + row["Description"] + "','" + row["RefNo"] + "','" + row["Debit"] + "','" +
                    row["Balance"] + "','" + row["Credit"] + "','" + AccountNumber + "','" + Userid + "'");

            }
            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }




        public JsonResult GetDriverRecoFileDetails(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();


            dt = db.sub_GetDatatable("USP_GetBankRecoDetails '" + FromDate + "','" + ToDate + "'");
            Session["ExporttoexcelGetBankdetails"] = dt;

            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;


        }


        public ActionResult OtherAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<BE.CategorySearchInvoice> SearchInvoice = new List<BE.CategorySearchInvoice>();

            SearchInvoice = reportprovider.OtherAssessInvoice(AssessNo, WorkYear, Category);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult ImportOtherInvoicePrint(string InvoiceNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblChargesDetails = new DataTable();
            DataTable tblSacDesc = new DataTable();
            DataTable tblContainerdetails = new DataTable();

            DataTable tblBankDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            double Amount = 0;
            double Discount = 0;
            double NetAmount = 0;
            string Srate = "";
            double SGST = 0;
            string CRate = "";
            double CGST = 0;
            string IRate = "";
            double IGST = 0;

            getJobOrderSet = db.sub_GetDataSets("getOtherInvoicePrintMVC '" + InvoiceNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblInvoiceDetails = getJobOrderSet.Tables[1];

                tblChargesDetails = getJobOrderSet.Tables[2];

                tblBankDetails = getJobOrderSet.Tables[3];
                tblSacDesc = getJobOrderSet.Tables[4];
                tblContainerdetails = getJobOrderSet.Tables[5];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.CINNo = dr["CINNo"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.Addressl = dr["AddressI"];
                    ViewBag.IMP_AC_EMAILID = dr["IMP_AC_EMAILID"];
                    ViewBag.NoteVS = dr["NoteVI"];
                    ViewBag.UPINO = dr["UPINumber"];
                    ViewBag.Datetime = Convert.ToDateTime(DateTime.Now).ToString("dd MM yyyy");
                    ViewBag.TIme = Convert.ToDateTime(DateTime.Now).ToString("HH:mm");
                }
                foreach (DataRow dr in tblInvoiceDetails.Rows)
                {
                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.InvoiceDate = dr["AssessDate"];
                    ViewBag.Name = dr["GSTName"];
                    ViewBag.Address = dr["GSTAddress"];
                    ViewBag.GSTIN = dr["GSTIn_uniqID"];
                    ViewBag.PlaceofSupply = dr["State"];
                    ViewBag.IgmItemNo = dr["IGMNo"] + "/" + dr["ItemNo"];
                    ViewBag.BLNo = dr["BLNumber"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.AGName = dr["AGName"];
                    ViewBag.Irn = dr["Irn"];
                    ViewBag.AckNo = dr["AckNo"];
                    ViewBag.AckDt = dr["AckDt"];
                    ViewBag.SignedQRcode = dr["SignedQRcode"];
                    ViewBag.Note = dr["Note"];
                    ViewBag.Other_DisplayName = dr["Other_DisplayName"];
                 
                }

                ViewBag.BankName = tblBankDetails.Rows[0]["BankName"];
                ViewBag.AccountNo = tblBankDetails.Rows[0]["AccountNo"];
                ViewBag.IFSCCode = tblBankDetails.Rows[0]["IFSCCode"];
                ViewBag.Remarks = tblBankDetails.Rows[0]["Remarks"];
                ViewBag.BranchName = tblBankDetails.Rows[0]["BranchName"];

                ViewBag.TotalAmountInWord = tblChargesDetails.Rows[0]["TotalAmountInWords"];
                ViewBag.TotalAmount = tblChargesDetails.Rows[0]["GrandTotal"];
                ViewBag.AmountWithoutTax = tblChargesDetails.Rows[0]["NetTotal"];
                ViewBag.SGSTAmount = tblChargesDetails.Rows[0]["SGST"];
                ViewBag.CGSTAmount = tblChargesDetails.Rows[0]["CGST"];
                ViewBag.IGSTAmount = tblChargesDetails.Rows[0]["IGST"];
                ViewBag.TaxAmount = Convert.ToDouble(Convert.ToDouble(tblChargesDetails.Rows[0]["SGST"]) + Convert.ToDouble(tblChargesDetails.Rows[0]["CGST"]) + Convert.ToDouble(tblChargesDetails.Rows[0]["IGST"]));
            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.AccountDetails = tblSacDesc.AsEnumerable();
            ViewBag.Containerdetails = tblContainerdetails.AsEnumerable();

            if (tblContainerdetails.Rows.Count > 0)
            {
                ViewBag.ContainerdetailsCount = tblContainerdetails.Rows.Count;
            }





            foreach (DataRow data in tblSacDesc.Rows)
            {
                Amount = Amount + Convert.ToDouble(data["Amount"]);
                Discount = Convert.ToDouble(data["Discount"]);
                NetAmount = NetAmount + Convert.ToDouble(data["NetAmount"]);
                Srate = Convert.ToString(data["Srate"]);
                CRate = Convert.ToString(data["CRate"]);
                IRate = Convert.ToString(data["IRate"]);
                SGST = SGST + Convert.ToDouble(data["SGST"]);
                CGST = CGST + Convert.ToDouble(data["CGST"]);
                IGST = IGST + Convert.ToDouble(data["IGST"]);
            }

            ViewBag.Amount = Amount;
            ViewBag.Discount = Discount;
            ViewBag.NetAmount = NetAmount;
            ViewBag.Srate = Srate;
            ViewBag.CRate = CRate;
            ViewBag.IRate = IRate;
            ViewBag.SGST = SGST;
            ViewBag.CGST = CGST;
            ViewBag.IGST = IGST;

            return PartialView();

        }

        // COde By suraj
        public ActionResult BankDayBook()
        {
            return View();
        }

        public JsonResult USP_Day_BooK_Tansaction(string FromDate, string ToDate)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            ImpFinalOut = db.sub_GetDatatable("USP_Day_BooK_Tansaction '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");

            Session["ImpFinalOut"] = ImpFinalOut;
            //Session["fromdate"] = AGID;
            //Session["todate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExporttoExcelBooK_Tansaction(string FromDate, string ToDate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string pass = "";
            DataTable dtFCLDestuffTallyList = (DataTable)Session["ImpFinalOut"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BankDayBook.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Bank Day Book<strong></td></tr>");

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

        // COde By suraj
        public ActionResult DayBook()
        {
            return View();
        }

        public JsonResult DayBooks(string FromDate, string ToDate)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            ImpFinalOut = db.sub_GetDatatable("USP_Day_BooK '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");

            Session["ImpFinalOut"] = ImpFinalOut;
            //Session["fromdate"] = AGID;
            //Session["todate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportExcelDaYBook(string FromDate, string ToDate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string pass = "";
            DataTable dtFCLDestuffTallyList = (DataTable)Session["ImpFinalOut"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Day_Book.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Day Book<strong></td></tr>");

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

        public ActionResult PartyWiseCollectionDetails()
        {


            return View();
        }

        public ActionResult GetPartyWiseCollection(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Party_Wise_Collection'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["EXPPartyWise"] = dt;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }


        public ActionResult ExportToExcelPartyWiseCollection()
        {
            DataTable dt = (DataTable)Session["EXPPartyWise"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["FromDate"] + " To " + Session["ToDate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PartyWiseCollectionReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Party Wise Collection Report <strong></td></tr>");
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
        public ActionResult LocationWiseMovementReport()
        {

            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            return View();
        }

        public ActionResult GetLocationWiseReport(string FromDate, string ToDate, int FromLoc, int ToLoc)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Location_Wise_Report'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + FromLoc + "','" + ToLoc + "'");
            Session["ExpStuffingWise"] = dt;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            Session["FromLoc"] = FromLoc;
            Session["ToDate"] = ToLoc;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportLocationWiseReport()
        {
            DataTable dt = (DataTable)Session["ExpStuffingWise"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=LocationWiseReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Location Wise Report <strong></td></tr>");
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





        // Code by sagar soni


        public ActionResult ActivityWiseMovementReport()
        {

            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = LP.getActivity();
            ViewBag.activityList = new SelectList(Activity, "AutoID", "Activity");
            return View();
        }

        public ActionResult GetActivityWiseReportMovementReport(string FromDate, string ToDate, int Activity)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable CompanyMaster = new DataTable();

                HC.DBOperations db = new HC.DBOperations();
                dt = db.sub_GetDatatable("USP_Activity_Wise_Movement_Report'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Activity + "'");
                Session["ActivityWiseDetails"] = dt;
                Session["FromDate"] = FromDate;
                Session["ToDate"] = ToDate;
                Session["Activity"] = Activity;

                string json = JsonConvert.SerializeObject(dt);
                var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }




        }

        public ActionResult ExportActivityWiseReport()
        {
            DataTable dt = (DataTable)Session["ActivityWiseDetails"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Activity_Wise_Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Activity Wise Report <strong></td></tr>");
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


        public ActionResult BankReconciliaztion()
        {
            return View();
        }
        public JsonResult USP_BANK_Reconsilations(string ToDate)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            ImpFinalOut = db.sub_GetDatatable("USP_BANK_Reconsilations '" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");

            Session["ExpInvList"] = ImpFinalOut;
            //Session["fromdate"] = AGID;
            //Session["todate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExporttoExcelReconsilations()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string pass = "";
            DataTable dtFCLDestuffTallyList = db.sub_GetDatatable("USP_BANK_Reconsilations");
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BANKReconsilations.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>BANK Reconsilations<strong></td></tr>");

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

        public ActionResult CancelReceiptInvoice()
        {
            ViewBag.WorkYear = "2021-22";
            return View();
        }
        public JsonResult CancelReceiptInvoiceReport(string ReceiptNo, string WorkYear)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable cancelreceiptdetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            cancelreceiptdetails = db.sub_GetDatatable("GetreceiptinvoiceD '" + ReceiptNo + "','" + WorkYear + "'");

            List<BE.ReceiptEntryEntities> Receiptdetails = new List<ReceiptEntryEntities>();


            if (cancelreceiptdetails.Rows.Count != 0)
            {

                foreach (DataRow row in cancelreceiptdetails.Rows)
                {
                    BE.ReceiptEntryEntities GetList = new BE.ReceiptEntryEntities();

                    GetList.ReceiptNo = Convert.ToInt32(row["Receipt No"]);
                    GetList.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    GetList.AssessDate = Convert.ToString(row["Assess-Date"]);
                    GetList.WorkYear = Convert.ToString(row["WorkYear"]);
                    GetList.Invoiceamt = Convert.ToString(row["Invoice Amount"]);
                    GetList.ReceivedAMt = Convert.ToString(row["Received Amount"]);
                    GetList.NetAmount = Convert.ToString(row["Net Received Amt"]);
                    GetList.TDS = Convert.ToString(row["TDS"]);
                    GetList.Addedby = Convert.ToString(row["UserName"]);
                    Receiptdetails.Add(GetList);
                }
            }
            Session["ExpInvList"] = cancelreceiptdetails;
            //Session["fromdate"] = AGID;
            //Session["todate"] = ToDate;
            var jsonResult = Json(Receiptdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult CancelReceiptWithReason(List<BE.ReceiptEntryEntities> tablearray,string ReceiptRefNo)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";



            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ReceiptNo");
            dataTable.Columns.Add("InvoiceNO");
            dataTable.Columns.Add("WorkYear");

            foreach (BE.ReceiptEntryEntities item in tablearray)
            {
                DataRow row = dataTable.NewRow();

                row["ReceiptNo"] = item.ReceiptNo;
                row["InvoiceNo"] = item.InvoiceNo;
                row["WorkYear"] = item.WorkYear;

                dataTable.Rows.Add(row);
            }
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            string message = reportprovider.Cancelreceiptdetails(dataTable, Userid, ReceiptRefNo);


            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult VoucherReportInandOutDetails()
        {
            //List<BE.GSTReturnEntities> GSTList = new List<BE.GSTReturnEntities>();
            //GSTList=reportprovider.getGSTReturnReport()
            return View();

        }

        public JsonResult VoucherReportInandOutDetailsShow(string fromdate, string TOdate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_VoucherReportIN_Out'" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(TOdate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["USP_VoucherReportIN_Out"] = dt;
            Session["fromdate"] = fromdate;
            Session["todate"] = TOdate;

            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult ExportToExcelVoucherReportInandOutDetails()
        {

            DataTable dt = (DataTable)Session["USP_VoucherReportIN_Out"];

            HC.DBOperations db = new HC.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ExportLoadInventory = dt;
            string Tittle = "As On" + Session["txtentryDate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = ExportLoadInventory;
            gridview.DataBind();
            ////////gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=VoucherReportInandOutDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Voucher Report In and Out Details<strong></td></tr>");
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

        public ActionResult DailyMovementTeusSummary()
        {

            return View();

        }

        public ActionResult GetDailyMovementTeusSummaryShow(string For, string FromDate, string ToDate)
        {
            DataTable dtGRNList = new DataTable();

            int UserID = Convert.ToInt16(Session["userid"]);
            HC.DBOperations db = new HC.DBOperations();
            dtGRNList = db.sub_GetDatatable("Get_ALL_Values_DAY'" + For + "','" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["Get_ALL_Values_DAY"] = dtGRNList;
            Session["For"] = For;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;

            string json = JsonConvert.SerializeObject(dtGRNList);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelGetDailyMovementTeusSummary()
        {

            DataTable dt = (DataTable)Session["Get_ALL_Values_DAY"];

            HC.DBOperations db = new HC.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ExportLoadInventory = dt;
            string Tittle = "For :- " + Session["For"] + "From Date :- " + Session["FromDate"] + " To Date :- " + Session["ToDAte"];
            GridView gridview = new GridView();
            gridview.DataSource = ExportLoadInventory;
            gridview.DataBind();
            ////////gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=VoucherReportInandOutDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; ><strong style='font-size: 20px'>" + "SrNo" + " <strong></td></tr>");
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; ><strong style='font-size: 20px'>" + "Date/Year" + " <strong></td></tr>");

                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center '; colspan ='8'><strong style='font-size: 15px'>EXPORT<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center; border:1px solid black'; colspan ='20'><strong style='font-size: 22px'>" + "IMPORT DETAILS" + "<strong></td><td style='font-weight: bold; text-align: center; border:1px solid black'; colspan ='16'><strong style='font-size: 20px'>" + "MTY REPO DETAILS" + " <strong></td><td style='font-weight: bold; text-align: center; border:1px solid black'; colspan ='8'><strong style='font-size: 20px'>" + "EXPORT" + " <strong></td></tr>");
                    //htw.Write("<table><tr></tr>");
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
        public ActionResult TransportOpsarReport()
        {
            return View();
        }

        public ActionResult GetOprar(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();


            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("sp_TrailerTripSummary'" + FromDate + "','" + ToDate + "'");
            Session["sp_TrailerTripSummary"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExportGetOprar()
        {
            DataTable dt = (DataTable)Session["sp_TrailerTripSummary"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=TransportTripSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Transport Trip Summary <strong></td></tr>");
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

        public ActionResult DomesticHeadWiseReport()
        {
            return View();
        }

        public ActionResult GetDomesticHeadWise(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();


            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_DOMESTIC_HEAD_WISE_REPO'" + FromDate + "','" + ToDate + "'");
            Session["USP_DOMESTIC_HEAD_WISE_REPO"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExportDomesticHeadWise()
        {
            DataTable dt = (DataTable)Session["USP_DOMESTIC_HEAD_WISE_REPO"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();


            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Domestc_head_wise_report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Domestic Head Wise Summary <strong></td></tr>");
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


        // neha sid
        public ActionResult Listofgatepass()
        {

            return View();
        }



        public ActionResult GetListofgatepass(string FromDate, string ToDate, string Type)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ActivityWise = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            ActivityWise = db.sub_GetDatatable("USP_Value_Duty '" + FromDate + "','" + ToDate + "','" + Type + "'");
            Session["ImpMonthlyOutWord"] = ActivityWise;
            Session["TYPE"] = Type;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(ActivityWise), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }






        public ActionResult ExportToExcelGetListofgatepass(string FromDate, string ToDate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string FileName = "value&dutysummary";
            string DispName = "summary";
            string AType = Session["TYPE"].ToString();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getActivityWise = (DataTable)Session["ImpMonthlyOutWord"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getActivityWise;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> " + DispName + "<strong></td></tr>");
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

        public ActionResult CargoLyingSummaryLCL()
        {


            return View();
        }
        public ActionResult GetCargoLyingSummaryLCL(DateTime FromDate, string size)
        {

            DataTable SalesRegisterReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            SalesRegisterReport = db.sub_GetDatatable("SP_LCLLongStanding_ONLY_LCL '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + size + "'");

            Session["LCLLongExporttoexcel"] = SalesRegisterReport;
            Session["fromdate"] = FromDate;

            var json = JsonConvert.SerializeObject(SalesRegisterReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelCargoLyingSummaryLCL()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetImportEmptyInventory = (DataTable)Session["LCLLongExporttoexcel"];
            string Tittle = "From " + Session["fromdate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetImportEmptyInventory;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Cargo_Lying_summary_Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Cargo Lying summary Report</h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + Tittle + " </h3></td></tr>");
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

        public ActionResult MovementTripCostDetails()
        {
            List<BE.GSTReturnEntities> TripsList = new List<BE.GSTReturnEntities>();
            TripsList = reportprovider.GetTripList();
            ViewBag.TriList = new SelectList(TripsList, "Trip", "Trip");
            return View();

        }

        public JsonResult MovementTripCostDetailsShow(string fromdate, string TOdate, string Criteria)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Transport_Cost'" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(TOdate).ToString("yyyy-MM-dd HH:mm") + "','" + Criteria + "'");
            Session["USP_Transport_Cost"] = dt;
            Session["fromdate"] = fromdate;
            Session["todate"] = TOdate;

            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult ExportToExcelMovementTripCostDetails()
        {

            DataTable dt = (DataTable)Session["USP_Transport_Cost"];

            HC.DBOperations db = new HC.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ExportLoadInventory = dt;
            string Tittle = "Movement Trip Cost Details.";
            GridView gridview = new GridView();
            gridview.DataSource = ExportLoadInventory;
            gridview.DataBind();
            ////////gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=MovementTripCostDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Voucher Report In and Out Details<strong></td></tr>");
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
        public ActionResult ExportToExcelCSVTripCostDetails(string fromdate, string TOdate, string Criteria)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dtProduct = db.sub_GetDatatable("USP_Transport_Cost'" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(TOdate).ToString("yyyy-MM-dd HH:mm") + "','" + Criteria + "'");

            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dtProduct.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dtProduct.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field =>
                  string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                sb.AppendLine(string.Join(",", fields));
            }
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=MovementTripCostDetails.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(sb);
            Response.Flush();
            Response.End();

            return View();
        }

        public ActionResult EIRWeighmentReport()
        {
            return View();
        }

        public JsonResult EIRWeighmentReportShow(string fromdate, string TOdate )
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_CUS_Arrival_Dets'" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(TOdate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["USP_CUS_Arrival_Dets"] = dt;
            Session["fromdate"] = fromdate;
            Session["todate"] = TOdate;

            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult ExportToExcelEIRWeighmentReport()
        {

            DataTable dt = (DataTable)Session["USP_CUS_Arrival_Dets"];

            HC.DBOperations db = new HC.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ExportLoadInventory = dt;
            string Tittle = "EIR & Weighment Report";
            GridView gridview = new GridView();
            gridview.DataSource = ExportLoadInventory;
            gridview.DataBind();
            ////////gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=EIRWeighmentReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Voucher Report In and Out Details<strong></td></tr>");
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
        public ActionResult MovementActivityValidation()
        {

            List<BE.OutActivity> OutList = new List<BE.OutActivity>();
            OutList = reportprovider.OutActi();
            ViewBag.OutList = new SelectList(OutList, "OutActivityID", "OutActivityName");
            List<BE.InActivity> InList = new List<BE.InActivity>();
            InList = reportprovider.InActi();
            ViewBag.TriList = new SelectList(InList, "InActivityID", "InActivityName");
            return View();
        }
        public JsonResult Movementsave(string OutActivity, string InActivity)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            HC.DBOperations db1 = new HC.DBOperations();
            DataTable Result = db1.sub_GetDatatable("USP_MovementActivityValidation '" + OutActivity + "','" + InActivity + "','" + UserID + "'");

            if (Result != null)
            {
                foreach (DataRow row1 in Result.Rows)
                {

                    message = Convert.ToString(row1["message"]);
                }

            }
            return Json(message);

        }

        public ActionResult GetActivitySummary(string From, string To)
        {
            //List<BE.PendingContainersForJoUpdationsEntities> InventoryList = new List<BE.PendingContainersForJoUpdationsEntities>();
            //InventoryList = reportprovider.GetInventoryList(AsonDate);
            //// ViewBag.InventoryDL = InventoryList;
            //return Json(InventoryList, JsonRequestBehavior.AllowGet);
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetSummaryActivity '" + Convert.ToDateTime(From).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(To).ToString("yyyyMMddHHmm") + "'");
            Session["CategoryInventory"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

            //List<BE.DefineCostRate> CargoTypeSummary = new List<BE.DefineCostRate>();
            //HC.DBOperations db = new HC.DBOperations();
            ////CargoTypeSummary = reportprovider.GetCostRateSummary( From, To);
            //CargoTypeSummary = db.sub_GetDatatable("USP_GetSummaryCost_Tariff_M '" + Convert.ToDateTime(From).ToString("yyyyMMddHHmm") + "','" + Convert.ToDateTime(To).ToString("yyyyMMddHHmm") + "'");
            //return Json(CargoTypeSummary);
        }
        public ActionResult ExportToExcelCSV(string from, string to)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dtProduct = db.sub_GetDatatable("USP_GetSummaryActivity'" + Convert.ToDateTime(from).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(to).ToString("yyyy-MM-dd HH:mm") + "'");

            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dtProduct.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dtProduct.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field =>
                  string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                sb.AppendLine(string.Join(",", fields));
            }
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=MovementActivityValidation.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(sb);
            Response.Flush();
            Response.End();

            return View();
        }

        public JsonResult MovementCancel(string AutoID)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            HC.DBOperations db1 = new HC.DBOperations();
            DataTable Result = db1.sub_GetDatatable("USP_Cancel_Movement_Activity '" + AutoID + "','" + UserID + "'");

            if (Result != null)
            {
                foreach (DataRow row1 in Result.Rows)
                {

                    message = Convert.ToString(row1["message"]);
                }

            }
            return Json(message);

        }
        public ActionResult CategoryWiseTDSSummary()
        {
            List<BE.PartyNameEntities> CustomerMaster = new List<BE.PartyNameEntities>();
            CustomerMaster = reportprovider.Getpartyname();
            ViewBag.customer = new SelectList(CustomerMaster, "Common_ID", "GSTName");
            return View();
        }

        [HttpPost]
        public ActionResult GetTDSSummary(string FromDate, string ToDate, string category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_CategoryWiseTDSAll '" + FromDate + "','" + ToDate + "','" + category + "'");
            Session["TDSSummaryList"] = dt;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelTDSSummaryDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["TDSSummaryList"];
            string Tittle = "FromDate " + Session["FromDate"] + "ToDate " + Session["ToDate"] + "";

            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=TDS Summary Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>TDS Report<strong></td></tr>");
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
        public ActionResult PartyWiseHoldEntry()
        {
            List<BE.ActivityMaster> Activity = reportprovider.getPartyWiseActivity();
            ViewBag.Activity = new SelectList(Activity, "ID", "TYPE");
            return View();
        }
        public JsonResult SaveHoldDetails(BE.PartyWiseHold HoldDetails)
        {

            int i = 0;
            ////int userId = Convert.ToInt32(Session["Tracker_userID"]);
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            int retVal = 0;
            retVal = db.sub_ExecuteNonQuery("USP_INSERT_PARTY_WISE_HOLD '" + HoldDetails.HoldDate + "','" + HoldDetails.Activity + "','" + HoldDetails.Hold_To + "','" + HoldDetails.Hold_TO_ID + "','" + HoldDetails.Hold_Reason + "','" + HoldDetails.HoldReamrks + "','" + UserID + "'");
            return Json(i);
        }
        public ActionResult HoldDetailsList()
        {

            List<BE.PartyWiseHold> HoldLists = new List<BE.PartyWiseHold>();
            HoldLists = reportprovider.getHoldDetailsLists();
            return PartialView(HoldLists);

        }
        public ActionResult HoldEntryReleased(BE.PartyWiseHold ReleasedRemarks)
        {
            ReleasedRemarks.ReleasedBy = Convert.ToInt16(Session["Tracker_userID"]);
            ReleasedRemarks.ReleasedOn = new DateTime().ToString();
            BE.ResponseMessage responseMessage = new BE.ResponseMessage();
            responseMessage = reportprovider.HoldEntryReleased(ReleasedRemarks);
            return Json(responseMessage);
        }
        public ActionResult ReleaseDetailsList()
        {

            //list<be.partywisehold> releaselists = new list<be.partywisehold>();
            //releaselists = gs.getreleasedetailslist(fromdate, todate);
            return PartialView();

        }
        public ActionResult ReleaseDetailSummary(string FromDate, string ToDate)
        {

            List<BE.PartyWiseHold> ReleaseLists = new List<BE.PartyWiseHold>();
            ReleaseLists = reportprovider.getReleaseDetailsList(FromDate, ToDate);
            return Json(ReleaseLists);

        }
        public JsonResult GetValidateDetails(string TrailerNo, string ActivityTypeID, string Activity)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            HC.DBOperations db1 = new HC.DBOperations();
            DataTable Result = db1.sub_GetDatatable("USP_Validation_IN_OUTActivity '" + TrailerNo + "','" + ActivityTypeID + "','" + Activity + "'");

            if (Result != null)
            {
                foreach (DataRow row1 in Result.Rows)
                {

                    message = Convert.ToString(row1["msg"]);
                }

            }
            return Json(message);

        }



        public ActionResult LdbDetails()        {            return View();        }        public ActionResult GetLdbDetails(string ToDate)        {            DataTable getMovementICDNew = new DataTable();            HC.DBOperations db = new HC.DBOperations();            getMovementICDNew = db.sub_GetDatatable("USP_LDB_DATA  '" + ToDate + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["GetLdb"] = getMovementICDNew;            Session["todate"] = ToDate;            var json = JsonConvert.SerializeObject(getMovementICDNew);            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;        }        public ActionResult ExportToExcelLdbDetails(string ToDate)        {            DataTable CompanyMaster = new DataTable();            HC.DBOperations db = new HC.DBOperations();            string FileName = "";            string DispName = "";
            //string AType = Session["Criteria"].ToString();

            FileName = "CFS_ICD_01_INNSA1VTC1" + Convert.ToDateTime(ToDate).ToString("yyyyMMdd") + "'";            DispName = "CFS_ICD_01_INNSA1VTC1";            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);            DataTable TrsReport = (DataTable)Session["GetLdb"];            string Tittle = " Ason Date " + Session["todate"] + ".";            GridView gridview = new GridView();            gridview.DataSource = TrsReport;            gridview.DataBind();            Response.Clear();            Response.Buffer = true;            Response.Charset = "";            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";            Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");            using (StringWriter sw = new StringWriter())            {                using (HtmlTextWriter htw = new HtmlTextWriter(sw))                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> " + DispName + "<strong></td></tr>");                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());                    Response.Flush();                    Response.End();                }            }            return View();        }
        public ActionResult ExportToExcelCSVGetLdbDetails(string ToDate)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dtProduct = db.sub_GetDatatable("USP_LDB_DATA'" + ToDate +  "'");
            string FileName = "";            string DispName = "";
            //string AType = Session["Criteria"].ToString();

            FileName = "CFS_ICD_01_INNSA1VTC1" ;            DispName = "CFS_ICD_01_INNSA1VTC1";
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dtProduct.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dtProduct.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field =>
                  string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                sb.AppendLine(string.Join(",", fields));
            }
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".csv");
 
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(sb);
            Response.Flush();
            Response.End();

            return View();
        }

        public ActionResult OwnContainerMaster()
        {
            List<BE.ContainerType> containerTypes = BL.ContainerType();
            ViewBag.Type = new SelectList(containerTypes, "ContainerTypeID", "ContainerTypeName");
            List<BE.Condition> conditions = BL.GetCondition();
            ViewBag.Condition = new SelectList(conditions, "ConditionID", "ConditionName");
            List<BE.Location> locations = BL.GetLocation();
            ViewBag.Location = new SelectList(locations, "LocationID", "LocationName");
            List<BE.ISOCodes> iSOCodes = BL.getISOCodes();
            ViewBag.ISOCode = new SelectList(iSOCodes, "ISOID", "ISOCode");
            List<BE.ContainerSize> containerSizes = BL.getContainerSize();
            ViewBag.Size = new SelectList(containerSizes, "ContainerSizeID", "ContainerSizeName");
            return View();
        }
        public JsonResult SaveOwnContainerMaster(string ContainerNo, int Size, int Type, int Condition, int Status, int Location, int ISOCode, string TareWt, string PermissibleWt, string OwnLease, string PurchaseDate, string LeaserName, string LeaseFrom, string LeaseTill, bool IsActive)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";
            retVal = db.sub_ExecuteNonQuery("USP_INSERT_OWN_CTR_MASTER '" + ContainerNo + "','" + Size + "','" + Type + "','" + Condition + "','" + Status + "','" + Location + "','" + ISOCode + "','" + TareWt + "','" + PermissibleWt + "','" + OwnLease + "','" + Convert.ToDateTime(PurchaseDate).ToString("yyyy-MM-dd") + "','" + LeaserName + "','" + LeaseFrom + "','" + LeaseTill + "','" + userId + "','" + IsActive + "'");
            if (retVal > 0)
            {
                Message = "Record saved successfully.";
            }
            else
            {
                Message = "Records not saved successfully.";
            }
            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult LoadVendorWiseServiceMapping()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_LoadVendorWiseServiceMapping");
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult LoadOwnContainerMaster(string Search)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_search_own '" + Search + "'");
            string json = JsonConvert.SerializeObject(dt);
            Session["usp_search_own"] = dt;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ExportToExcelOwnContainerMaster()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetLCLDestuffTally = (DataTable)Session["usp_search_own"];

            GridView gridview = new GridView();
            gridview.DataSource = GetLCLDestuffTally;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Own Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Own Report</h3></td></tr>");

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
        public ActionResult StuffDestuff()
        {
            List<BE.Location> locations = BL.GetLocation();
            ViewBag.Location = new SelectList(locations, "LocationID", "LocationName");
            return View();
        }

        public JsonResult SaveStuffDestuff(string Type, string ContainerNo, string Location, string Remarks)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";
            retVal = db.sub_ExecuteNonQuery("USP_SaveStuffDestuff '" + Type + "','" + ContainerNo + "','" + Location + "','" + Remarks + "','" + userId + "'");
            if (retVal > 0)
            {
                Message = "Record saved successfully.";
            }
            else
            {
                Message = "Records not saved successfully.";
            }
            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult LoadStuffDestuff()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_LoadStuffDestuff");
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult OwnContainer()
        {
            return View();
        }

        public ActionResult LoadOwnContainer()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_LoadOwnContainer");
            Session["USP_LoadOwnContainer"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ExportToExcelLoadOwnContainer()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetLCLDestuffTally = (DataTable)Session["USP_LoadOwnContainer"];

            GridView gridview = new GridView();
            gridview.DataSource = GetLCLDestuffTally;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Own Container Inventory.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Own Container Inventory</h3></td></tr>");

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
        [HttpPost]        public JsonResult GetCustomer(string prefixText, string Mode)        {            HC.DBOperations db = new HC.DBOperations();            DataTable dataTable = new DataTable();            List<BE.Customer> Customerlst = new List<BE.Customer>();            dataTable = db.sub_GetDatatable("USP_GetBL_Drop_Down_Dets '" + Mode + "','" + prefixText + "'");            if (dataTable != null)            {                foreach (DataRow row in dataTable.Rows)                {                    BE.Customer customer = new BE.Customer();                    customer.AGID = Convert.ToInt32(row["GSTID"]);                    customer.AGName = Convert.ToString(row["GSTName"]);                    Customerlst.Add(customer);                }            }            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;        }

        public ActionResult DomesticChangeContainer()
        {
            List<BE.Location> locations = BL.GetLocation();
            ViewBag.Location = new SelectList(locations, "LocationID", "LocationName");
            return View();
        }
        public JsonResult SaveChanges(string OldContainerNo, string NewContainerNo)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";
            retVal = db.sub_ExecuteNonQuery("USP_SaveChanges '" + OldContainerNo + "','" + NewContainerNo + "','" + userId + "'");
            if (retVal > 0)
            {
                Message = "Container changes successfully.";
            }
            else
            {
                Message = "Container changes not successfully.";
            }
            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult CheckContainer(string Search)
        {
            DataTable dt = new DataTable();
            string message = "";
            HC.DBOperations db = new HC.DBOperations();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dt = db.sub_GetDatatable("USP_Check_Own_Contanier '" + Search + "'");
            if (Convert.ToInt32(dt.Rows[0]["mgs"]) != 0)            {
                message = "";            }            else
            {
                message = "Contanier not found";

            }

            return Json(message);
        }
        public ActionResult CheckContainerNew(string Search)
        {
            DataTable dt = new DataTable();
            string message = "";
            HC.DBOperations db = new HC.DBOperations();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dt = db.sub_GetDatatable("USP_Check_Own_Contanier '" + Search + "'");
            if (Convert.ToInt32(dt.Rows[0]["mgs"]) != 0)            {
                message = "Specified container already have in domestic stock. Cannot proceed.";            }            else
            {
                message = "";

            }

            return Json(message);
        }
        public ActionResult LoadOldContanier()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("UPS_Old_ContainerSummer");
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public ActionResult ConsolidateTransporterSummary()
        {
            return View();
        }

        public ActionResult GetConsolidateTransporterActivitySummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_ALL_DATA_Transporterwise'" + FromDate + "','" + ToDate + "','" + userId + "'");
            Session["USP_ALL_DATA_Transporterwise"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }


        public ActionResult ExportToExcelConsolidateTransporterActivitySummary()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["USP_ALL_DATA_Transporterwise"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Consolidate Transporter Summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Consolidate Transporter Summary<strong></td></tr>");
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

        public ActionResult LoadedToDestuffReport()        {            return View();        }        public ActionResult GetLoadedToDestuffReport(string FromDate, string ToDate)        {            DataTable getMovementICDNew = new DataTable();            HC.DBOperations db = new HC.DBOperations();            getMovementICDNew = db.sub_GetDatatable("USP_WO_LOADED_TO_DESTUFF_LIST  '" + FromDate + "','" + ToDate + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["USP_WO_LOADED_TO_DESTUFF_LIST"] = getMovementICDNew;
            Session["FromDate"] = FromDate;            Session["ToDate"] = ToDate;            var json = JsonConvert.SerializeObject(getMovementICDNew);            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;        }        public ActionResult ExportToExcelLoadedToDestuffReport(string FromDate, string ToDate)        {            DataTable CompanyMaster = new DataTable();            HC.DBOperations db = new HC.DBOperations();                     CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);            DataTable TrsReport = (DataTable)Session["USP_WO_LOADED_TO_DESTUFF_LIST"];            string Tittle = " From " + Session["FromDate"] + " And To "+ Session["todate"] + " Date.";            string Tittle1 = " Loaded To Destuff Report";            GridView gridview = new GridView();            gridview.DataSource = TrsReport;            gridview.DataBind();            Response.Clear();            Response.Buffer = true;            Response.Charset = "";            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";            Response.AddHeader("content-disposition", "attachment;filename=" + Tittle1 + ".xls");            using (StringWriter sw = new StringWriter())            {                using (HtmlTextWriter htw = new HtmlTextWriter(sw))                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> " + Tittle1 + "<strong></td></tr>");                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());                    Response.Flush();                    Response.End();                }            }            return View();        }




        public ActionResult ExportPortReturnSummary()
        {
            return View();
        }

        public ActionResult GetExportPortReturnSummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_PORT_RETRUN_SUMMARY'" + FromDate + "','" + ToDate + "'");
            Session["USP_PORT_RETRUN_SUMMARY"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }


        public ActionResult ExportToExcelExportPortReturnSummary()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["USP_PORT_RETRUN_SUMMARY"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Export Port Return Summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Port Return Summary<strong></td></tr>");
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
        public ActionResult ProfitabilitySummary()
        {
            return View();
        }

        public ActionResult GetProfitabilitySummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Summary_Profit'" + Convert.ToDateTime(FromDate).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(ToDate).ToString("yyyyMMdd") + "','" + FromDate + "','" + ToDate + "'");

            Session["USP_Summary_Profit"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }


        public ActionResult ExportToExcelProfitabilitySummary()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["USP_Summary_Profit"];
            getMovementICDNew.Columns.Remove("Revenue Breakup");
            getMovementICDNew.Columns.Remove("Expense Breakup");
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ProfitabilitySummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Profitability Summary<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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
        public ActionResult GetEdiAgeing(string FromDate, string ToDate, string Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);


            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Details_Profit_Revenue'" + Convert.ToDateTime(FromDate).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(ToDate).ToString("yyyyMMdd") + "','" + FromDate + "','" + ToDate + "','" + Category + "'");
            Session["USP_Details_Profit_Revenue"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetExpensesDetails(string FromDate, string ToDate, string Category)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);


            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Details_Profit_Expenses'" + Convert.ToDateTime(FromDate).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(ToDate).ToString("yyyyMMdd") + "','" + FromDate + "','" + ToDate + "','" + Category + "'");
            Session["USP_Details_Profit_Revenue"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExcelProfitabilityRevenue()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["USP_Details_Profit_Revenue"];

            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ProfitabilityRevenue.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Profitability Revenue<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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
        public ActionResult ExportCargobalDetails()
        {
            List<BE.CustomerMaster> CustomerMaster = new List<BE.CustomerMaster>();
            CustomerMaster = reportprovider.getCustomerMaster();
            ViewBag.customer = new SelectList(CustomerMaster, "AGID", "AGName");

            return View();
        }
        public ActionResult GetExportCargobalDetails(string FromDate, string Agentname, string FreeDays)
        {

            DataTable SalesRegisterReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            SalesRegisterReport = db.sub_GetDatatable("Get_sp_ExportCargoStock  '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Agentname + "','" + FreeDays + "'");

            Session["ExportcargoStockDetails"] = SalesRegisterReport;
            Session["fromdate"] = FromDate;

            var json = JsonConvert.SerializeObject(SalesRegisterReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public ActionResult ExportToExcelCargobalDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetImportEmptyInventory = (DataTable)Session["ExportcargoStockDetails"];
            string Tittle = "From " + Session["fromdate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetImportEmptyInventory;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Cargo_Bal_Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Cargo Bal Details Report</h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + Tittle + " </h3></td></tr>");
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




        public ActionResult DepotTeusWiseReport()
        {
          

            return View();
        }
        public ActionResult GetSummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_depot_teus_inv_dets'" + FromDate + "','" + ToDate + "'");

            Session["usp_depot_teus_inv_dets"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExcelSummaryDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetImportEmptyInventory = (DataTable)Session["usp_depot_teus_inv_dets"];
            string Tittle = "From " + Session["fromdate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetImportEmptyInventory;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=DepotTeusWiseReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Depot Teus Wise Report</h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + Tittle + " </h3></td></tr>");
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
        public ActionResult DockDestuffCancelWorkOrder()
        {
             
            return View();
        }
        public JsonResult CancelWorkOrder(string WoNo)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            ImpFinalOut = db.sub_GetDatatable("USP_Cancel_WorkOrder_Dock '" + WoNo + "'");
 
            var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult CancelWorkDetailsData(List<BE.CancelWorkOrderDock> Debitdata)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("WoNo");
            dataTable.Columns.Add("containerNo");
            dataTable.Columns.Add("JoEntryID");
          


            //list to the table
            foreach (BE.CancelWorkOrderDock item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["WoNo"] = item.WoNo;
                row["containerNo"] = item.containerNo;
                row["JoEntryID"] = item.JoEntryID;
          


                dataTable.Rows.Add(row);
            }
            message = reportprovider.CancelWorkDetails(dataTable, UserID);
            return Json(message);
        }
        public ActionResult Ministryreport()
        {

            return View();
        }
        public ActionResult MinistryreportSummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Ministryreport'" + FromDate + "','" + ToDate + "'");

            Session["USP_Ministryreport"] = dt;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        public ActionResult ExportToExcelSummaryMinistry()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetImportEmptyInventory = (DataTable)Session["USP_Ministryreport"];
            string Tittle = "";
            GridView gridview = new GridView();
            gridview.DataSource = GetImportEmptyInventory;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=MinistryReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Ministry Report</h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + Tittle + " </h3></td></tr>");
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


        public ActionResult CategoryAndPartyWiseRevenue()
        {

            return View();
        }



        public ActionResult GetCategoryAndPartyWiseRevenue(string SearchCriteria, string Fromdate, string ToDate)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            if (SearchCriteria == "BOND")
            {
                getMovementICDNew = db.sub_GetDatatable("USP_CHA_WISE_BOnd_Revenue '" + Fromdate + "','" + ToDate + "'");
                Session["SearchType"] = SearchCriteria;
                Session["Fromdate"] = Fromdate;
                Session["ToDate"] = ToDate;
            }
            if (SearchCriteria == "Domestic")
            {
                getMovementICDNew = db.sub_GetDatatable("USP_Billed_WISE_Domestic_Revenue '" + Fromdate + "','" + ToDate + "'");
                Session["SearchType"] = SearchCriteria;
                Session["Fromdate"] = Fromdate;
                Session["ToDate"] = ToDate;
            }
            if (SearchCriteria == "Export")
            {
                getMovementICDNew = db.sub_GetDatatable("Get_ExportRevenueLineWise_BilledTo_Wise '" + Fromdate + "','" + ToDate + "'");
                Session["SearchType"] = SearchCriteria;
                Session["Fromdate"] = Fromdate;
                Session["ToDate"] = ToDate;
            }
            if (SearchCriteria == "Import CHA Wise")
            {
                getMovementICDNew = db.sub_GetDatatable("Get_ImportRevenue_CHA_Wise '" + Fromdate + "','" + ToDate + "'");
                Session["SearchType"] = SearchCriteria;
                Session["Fromdate"] = Fromdate;
                Session["ToDate"] = ToDate;
            }
            if (SearchCriteria == "Import Bill To Wise")
            {
                getMovementICDNew = db.sub_GetDatatable("Get_ImportRevenueLineWise_BilledTo_Wise '" + Fromdate + "','" + ToDate + "'");
                Session["SearchType"] = SearchCriteria;
                Session["Fromdate"] = Fromdate;
                Session["ToDate"] = ToDate;
            }
            if (SearchCriteria == "MNR")
            {
                getMovementICDNew = db.sub_GetDatatable("USP_Billed_WISE_MNR_Revenue '" + Fromdate + "','" + ToDate + "'");
                Session["SearchType"] = SearchCriteria;
                Session["Fromdate"] = Fromdate;
                Session["ToDate"] = ToDate;
            }
            if (SearchCriteria == "Import Line Wise Revenue")
            {
                getMovementICDNew = db.sub_GetDatatable("Get_ImportRevenueLineWise '" + Fromdate + "','" + ToDate + "'");
                Session["SearchType"] = SearchCriteria;
                Session["Fromdate"] = Fromdate;
                Session["ToDate"] = ToDate;
            }


            Session["ListOfCatrgoryAndpartyRevenue"] = getMovementICDNew;
            var json = JsonConvert.SerializeObject(getMovementICDNew);

            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelCategoryAndPartyWiseRevenue()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["ListOfCatrgoryAndpartyRevenue"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + "" + Session["SearchType"] + "";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Category And Party Wise Revenue.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Category And Party Wise Revenue<strong></td></tr>");
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
        public JsonResult getPartyName(string prefixText, string Mode)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GetPartyName '" + Mode + "','" + prefixText + "'");

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

        public ActionResult BCNCalcel()
        {

            return View();
        }
        public JsonResult CancelStuffingReqDetails(List<BE.ExportModifyMaster> Debitdata, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("SBNO");
            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("containerNo");
            dataTable.Columns.Add("SBEntryID");


            //list to the table
            foreach (BE.ExportModifyMaster item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["SBNO"] = item.SBNo;
                row["EntryID"] = item.EntryID;
                row["containerNo"] = item.containerNo;
                row["SBEntryID"] = item.SBEntryID;


                dataTable.Rows.Add(row);
            }
            //message = BL.CancelSRDetails(dataTable, Remarks, UserID);
            return Json(message);
        }
        public ActionResult CategoryAndPartyWisedescription()
        {

            return View();
        }
        public ActionResult ActivityInoutStatus()
        {
            return View();
        }
        public ActionResult SaveActivty(string Activityname ,string SizeOUT,string OUTStatus ,string SIzeIN, string INStatus)
        {
            DataTable dt = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            HC.DBOperations db = new HC.DBOperations();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dt = db.sub_GetDatatable("USP_INSERT_ACTIVITY_IN_OUT_STATUS '" + Activityname + "','" + SizeOUT + "','" + OUTStatus + "','" + SIzeIN + "','" + INStatus + "','" + UserID + "'");
             

            return Json(message);
        }
        public JsonResult ActivityReport(string Search)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ActivityOBJ = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            ActivityOBJ = db.sub_GetDatatable("USP_ActivityShow '" + Search + "'");
            Session["ExpInvList"] = ActivityOBJ;
            //Session["fromdate"] = AGID;
            //Session["todate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(ActivityOBJ), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ImportAndExportMovementReport()
        {
            // for Current Month
            List<BE.ImportExportProcessActivitycs> ICDList = new List<BE.ImportExportProcessActivitycs>();
            ICDList = reportprovider.GetImportExportReport();

            List<BE.ImportExportProcessActivitycs> ICDCompareList = new List<BE.ImportExportProcessActivitycs>();
            ICDCompareList = reportprovider.GetImportExportCompareReport();
            //ViewBag.ListICD = ICDList;

            // For Last Month
            List<BE.ImportExportProcessActivitycs> LastMonth = new List<BE.ImportExportProcessActivitycs>();
            LastMonth = reportprovider.GetLastImportExportReport();

            List<BE.ImportExportProcessActivitycs> LastCompareMonth = new List<BE.ImportExportProcessActivitycs>();
            LastCompareMonth = reportprovider.GetLastImportExportCompareReport();
            //ViewBag.List_LastICD = LastMonth;

            // Last 24 Hours

            List<BE.ImportExportProcessActivitycs> Last24hours = new List<BE.ImportExportProcessActivitycs>();
            Last24hours = reportprovider.GetLastDayImportExportReport();

            List<BE.ImportExportProcessActivitycs> LastCompare24hours = new List<BE.ImportExportProcessActivitycs>();
            LastCompare24hours = reportprovider.GetLastDayImportExportCompareReport();
            //ViewBag.ListLastday = Last24hours;

            // Last 12 Hours
            List<BE.ImportExportProcessActivitycs> Last12hours = new List<BE.ImportExportProcessActivitycs>();
            Last12hours = reportprovider.GetLastDay12HrsImportExportReport();
            //Port Pendency

            List<BE.ImportExportProcessActivitycs> PortPenency = new List<BE.ImportExportProcessActivitycs>();
            List<BE.ImportExportProcessActivitycs> PortPenency1 = new List<BE.ImportExportProcessActivitycs>();
            PortPenency = reportprovider.GetPortPenencyReportReport();
            ViewBag.PortPendencyCFSRail = PortPenency[1].CFSRail;
            ViewBag.PortPendencyCFSRoad = PortPenency[1].CFSRoad;
            ViewBag.PortPendencyICDRail = PortPenency[0].ICDRail;
            ViewBag.PortPendencyICDRoad = PortPenency[0].ICDRoad;

            ViewBag.CurrentMonthName = ICDList[1].CurrentMonthName;
            ViewBag.LastMonthName = LastMonth[1].LastMonthName;
            ViewBag.Last24HrsName = Last24hours[1].Last24HRSName;

            ViewBag.CurrentMonthCompareName = ICDCompareList[1].CurrentMonthName;
            ViewBag.LastMonthCompareName = LastCompareMonth[1].LastMonthName;
            ViewBag.Last24HrsCompareName = LastCompare24hours[1].Last24HRSName;

            ViewBag.Last12HrsName = Last12hours[1].Last24HRSName;

            //for (i = 0; i < reportprovider.length; i++)
            //{
            //    for (j = 0; j < reportprovider.length; j++)
            //    {
            //        if (temp[i].Containerno == temp1[j].Containerno)
            //        {
            //            temp[i].expenesname = temp1[j].accountname;
            //            temp[i].expensesamount = temp1[j].Amount;
            //        }
            //        //console.log(temp)
            //    }
            //}

            List<BE.ImportExportProcessActivitycs> Inventory = new List<BE.ImportExportProcessActivitycs>();
            Inventory = reportprovider.GetInventoryDetails();
            ViewBag.InventoryList = Inventory;


            List<BE.ImportExportProcessActivitycs> Destuffdetails = new List<BE.ImportExportProcessActivitycs>();
            Destuffdetails = reportprovider.GetDestuffDetails();
            ViewBag.Destuffdetails = Destuffdetails;


            List<BE.ImportExportProcessActivitycs> TodayDestuffdetails = new List<BE.ImportExportProcessActivitycs>();
            TodayDestuffdetails = reportprovider.GetTodayDestuffDetails();
            ViewBag.TodayDestuffdetails = TodayDestuffdetails;



            // for Current Month
            DataTable Currentmonth = new DataTable();

            Currentmonth.Columns.Add("process");
            Currentmonth.Columns.Add("Import");
            Currentmonth.Columns.Add("Export");
            Currentmonth.Columns.Add("Total");
            Currentmonth.TableName = "PP";
            double ImportCount = 0;
            double ExportCount = 0;
            double TotalCount = 0;


            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in ICDList)
            {
                DataRow row = Currentmonth.NewRow();

                row["process"] = item.process;
                row["Import"] = item.Import;
                row["Export"] = item.Export;
                row["Total"] = item.Total;
                ImportCount += Convert.ToDouble(item.Import);
                ExportCount += Convert.ToDouble(item.Export);
                TotalCount += Convert.ToDouble(item.Total);
                Currentmonth.Rows.Add(row);
            }
            DataRow row1 = Currentmonth.NewRow();

            row1["process"] = "Total";
            row1["Import"] = ImportCount;
            row1["Export"] = ExportCount;
            row1["Total"] = TotalCount;
            Currentmonth.Rows.Add(row1);

            Session["CurrentList"] = Currentmonth;

            ViewBag.ListICD = Currentmonth.AsEnumerable();
            //end for current month

            //for Current Month Compare
            DataTable CurrentmonthCompare = new DataTable();

            CurrentmonthCompare.Columns.Add("process");
            CurrentmonthCompare.Columns.Add("Import");
            CurrentmonthCompare.Columns.Add("Export");
            CurrentmonthCompare.Columns.Add("Total");
            CurrentmonthCompare.TableName = "PP";
            double ImportCountCompare = 0;
            double ExportCountCompare = 0;
            double TotalCountCompare = 0;


            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in ICDCompareList)
            {
                DataRow row = CurrentmonthCompare.NewRow();

                row["process"] = item.process;
                row["Import"] = item.Import;
                row["Export"] = item.Export;
                row["Total"] = item.Total;
                ImportCountCompare += Convert.ToDouble(item.Import);
                ExportCountCompare += Convert.ToDouble(item.Export);
                TotalCountCompare += Convert.ToDouble(item.Total);
                CurrentmonthCompare.Rows.Add(row);
            }
            DataRow row1Compare = CurrentmonthCompare.NewRow();

            row1Compare["process"] = "Total";
            row1Compare["Import"] = ImportCountCompare;
            row1Compare["Export"] = ExportCountCompare;
            row1Compare["Total"] = TotalCountCompare;
            CurrentmonthCompare.Rows.Add(row1Compare);

            Session["CurrentList"] = CurrentmonthCompare;

            ViewBag.ListICDCompare = CurrentmonthCompare.AsEnumerable();
            //end for current month compare




            // for Last Month
            DataTable LastMonthReport = new DataTable();

            LastMonthReport.Columns.Add("process");
            LastMonthReport.Columns.Add("Import");
            LastMonthReport.Columns.Add("Export");
            LastMonthReport.Columns.Add("Total");
            LastMonthReport.TableName = "PPp";
            double ImportCountLast = 0;
            double ExportCountLast = 0;
            double TotalCountLast = 0;


            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in LastMonth)
            {
                DataRow row3 = LastMonthReport.NewRow();

                row3["process"] = item.process;
                row3["Import"] = item.Import;
                row3["Export"] = item.Export;
                row3["Total"] = item.Total;
                ImportCountLast += Convert.ToDouble(item.Import);
                ExportCountLast += Convert.ToDouble(item.Export);
                TotalCountLast += Convert.ToDouble(item.Total);
                LastMonthReport.Rows.Add(row3);
            }
            DataRow row4 = LastMonthReport.NewRow();

            row4["process"] = "Total";
            row4["Import"] = ImportCountLast;
            row4["Export"] = ExportCountLast;
            row4["Total"] = TotalCountLast;
            LastMonthReport.Rows.Add(row4);

            Session["LastMonthReport"] = LastMonthReport;

            ViewBag.List_LastICD = LastMonthReport.AsEnumerable();
            //end forLast Month

            // for Last Month Compare
            DataTable LastMonthReportCompare = new DataTable();

            LastMonthReportCompare.Columns.Add("process");
            LastMonthReportCompare.Columns.Add("Import");
            LastMonthReportCompare.Columns.Add("Export");
            LastMonthReportCompare.Columns.Add("Total");
            LastMonthReportCompare.TableName = "PPp";
            double ImportCountLastCompare = 0;
            double ExportCountLastCompare = 0;
            double TotalCountLastCompare = 0;


            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in LastCompareMonth)
            {
                DataRow row3 = LastMonthReportCompare.NewRow();

                row3["process"] = item.process;
                row3["Import"] = item.Import;
                row3["Export"] = item.Export;
                row3["Total"] = item.Total;
                ImportCountLastCompare += Convert.ToDouble(item.Import);
                ExportCountLastCompare += Convert.ToDouble(item.Export);
                TotalCountLastCompare += Convert.ToDouble(item.Total);
                LastMonthReportCompare.Rows.Add(row3);
            }
            DataRow row4Compare = LastMonthReportCompare.NewRow();

            row4Compare["process"] = "Total";
            row4Compare["Import"] = ImportCountLastCompare;
            row4Compare["Export"] = ExportCountLastCompare;
            row4Compare["Total"] = TotalCountLastCompare;
            LastMonthReportCompare.Rows.Add(row4Compare);

            Session["LastMonthReportCompare"] = LastMonthReportCompare;

            ViewBag.List_LastICDCompare = LastMonthReportCompare.AsEnumerable();
            //end forLast Month Compare



            // for 24 Hours
            DataTable LastDayReport = new DataTable();

            LastDayReport.Columns.Add("process");
            LastDayReport.Columns.Add("Import");
            LastDayReport.Columns.Add("Export");
            LastDayReport.Columns.Add("Total");
            LastDayReport.TableName = "PPpPP";
            double ImportCountLastDay = 0;
            double ExportCountLastDay = 0;
            double TotalCountLastDay = 0;


            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in Last24hours)
            {
                DataRow row5 = LastDayReport.NewRow();

                row5["process"] = item.process;
                row5["Import"] = item.Import;
                row5["Export"] = item.Export;
                row5["Total"] = item.Total;
                ImportCountLastDay += Convert.ToDouble(item.Import);
                ExportCountLastDay += Convert.ToDouble(item.Export);
                TotalCountLastDay += Convert.ToDouble(item.Total);
                LastDayReport.Rows.Add(row5);
            }
            DataRow row6 = LastDayReport.NewRow();

            row6["process"] = "Total";
            row6["Import"] = ImportCountLastDay;
            row6["Export"] = ExportCountLastDay;
            row6["Total"] = TotalCountLastDay;
            LastDayReport.Rows.Add(row6);

            Session["LastDayReport"] = LastDayReport;

            ViewBag.ListLastday = LastDayReport.AsEnumerable();

            // for 24 Hours

            // for 24 Hours Compare
            DataTable LastDayReportCompare = new DataTable();

            LastDayReportCompare.Columns.Add("process");
            LastDayReportCompare.Columns.Add("Import");
            LastDayReportCompare.Columns.Add("Export");
            LastDayReportCompare.Columns.Add("Total");
            LastDayReportCompare.TableName = "PPpPP";
            double ImportCountLastDayCompare = 0;
            double ExportCountLastDayCompare = 0;
            double TotalCountLastDayCompare = 0;


            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in LastCompare24hours)
            {
                DataRow row5 = LastDayReportCompare.NewRow();

                row5["process"] = item.process;
                row5["Import"] = item.Import;
                row5["Export"] = item.Export;
                row5["Total"] = item.Total;
                ImportCountLastDayCompare += Convert.ToDouble(item.Import);
                ExportCountLastDayCompare += Convert.ToDouble(item.Export);
                TotalCountLastDayCompare += Convert.ToDouble(item.Total);
                LastDayReportCompare.Rows.Add(row5);
            }
            DataRow row6Compare = LastDayReportCompare.NewRow();

            row6Compare["process"] = "Total";
            row6Compare["Import"] = ImportCountLastDayCompare;
            row6Compare["Export"] = ExportCountLastDayCompare;
            row6Compare["Total"] = TotalCountLastDayCompare;
            LastDayReportCompare.Rows.Add(row6Compare);

            Session["LastDayReportCompare"] = LastDayReportCompare;

            ViewBag.ListLastdayCompare = LastDayReportCompare.AsEnumerable();

            // end for 24 Hours Compare

            // for 24 Hours Compare
            DataTable TodayReport = new DataTable();

            TodayReport.Columns.Add("process");
            TodayReport.Columns.Add("Import");
            TodayReport.Columns.Add("Export");
            TodayReport.Columns.Add("Total");
            TodayReport.TableName = "PPpPP";
            double ImportCountToday = 0;
            double ExportCountToday = 0;
            double TotalCountToday = 0;


            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in Last12hours)
            {
                DataRow row5 = TodayReport.NewRow();

                row5["process"] = item.process;
                row5["Import"] = item.Import;
                row5["Export"] = item.Export;
                row5["Total"] = item.Total;
                ImportCountToday += Convert.ToDouble(item.Import);
                ExportCountToday += Convert.ToDouble(item.Export);
                TotalCountToday += Convert.ToDouble(item.Total);
                TodayReport.Rows.Add(row5);
            }
            DataRow row10 = TodayReport.NewRow();

            row10["process"] = "Total";
            row10["Import"] = ImportCountToday;
            row10["Export"] = ExportCountToday;
            row10["Total"] = TotalCountToday;
            TodayReport.Rows.Add(row10);

            Session["TodayReport"] = TodayReport;

            ViewBag.ListToday = TodayReport.AsEnumerable();

            // end for 24 Hours Compare

            DataTable PortPendency = new DataTable();

            PortPendency.Columns.Add("ICDRail");
            PortPendency.Columns.Add("ICDRoad");
            PortPendency.Columns.Add("CFSRail");
            PortPendency.Columns.Add("CFSRoad");
            PortPendency.TableName = "PPrpPP";
            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in PortPenency)
            {
                DataRow row7 = PortPendency.NewRow();

                row7["ICDRail"] = item.ICDRail;
                row7["ICDRoad"] = item.ICDRoad;
                row7["CFSRail"] = item.CFSRail;
                row7["CFSRoad"] = item.CFSRoad;
                PortPendency.Rows.Add(row7);
            }


            Session["Portpendency"] = PortPendency;
            //end for current month

            //inventory

            DataTable InventoryReport = new DataTable();

            InventoryReport.Columns.Add("process");
            InventoryReport.Columns.Add("BalInventory");
            InventoryReport.Columns.Add("Inventory");
            InventoryReport.Columns.Add("ExportBalInventory");
            InventoryReport.TableName = "InventoryTable";
            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in Inventory)
            {
                DataRow row9 = InventoryReport.NewRow();

                row9["process"] = item.process;
                row9["BalInventory"] = item.BalInventory;
                row9["Inventory"] = item.Inventory;
                row9["ExportBalInventory"] = item.ExportBalInventory;
                InventoryReport.Rows.Add(row9);
            }
            Session["InventoryReport"] = InventoryReport;






            //24 Hours Delivery

            DataTable lastHoursdelivery = new DataTable();

            lastHoursdelivery.Columns.Add("process");
            lastHoursdelivery.Columns.Add("FCLDestuff");
            lastHoursdelivery.Columns.Add("LCLDestuff");
            lastHoursdelivery.Columns.Add("IMPLoadedDelivery");
            lastHoursdelivery.TableName = "lastdadelivleryTable";
            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in Destuffdetails)
            {
                DataRow row9 = lastHoursdelivery.NewRow();

                row9["process"] = item.process;
                row9["FCLDestuff"] = item.FCLDestuff;
                row9["LCLDestuff"] = item.LCLDestuff;
                row9["IMPLoadedDelivery"] = item.IMPLoadedDelivery;
                lastHoursdelivery.Rows.Add(row9);
            }
            Session["LastDayDeliveryReport"] = lastHoursdelivery;

            //12 Houes Delivery

            DataTable TodayDeliveryReport = new DataTable();

            TodayDeliveryReport.Columns.Add("process");
            TodayDeliveryReport.Columns.Add("FCLDestuff");
            TodayDeliveryReport.Columns.Add("LCLDestuff");
            TodayDeliveryReport.Columns.Add("IMPLoadedDelivery");
            TodayDeliveryReport.TableName = "TodayDelivery";
            //int Count = 1;
            foreach (BE.ImportExportProcessActivitycs item in TodayDestuffdetails)
            {
                DataRow row9 = TodayDeliveryReport.NewRow();

                row9["process"] = item.process;
                row9["FCLDestuff"] = item.FCLDestuff;
                row9["LCLDestuff"] = item.LCLDestuff;
                row9["IMPLoadedDelivery"] = item.IMPLoadedDelivery;
                TodayDeliveryReport.Rows.Add(row9);
            }
            Session["TodayDeliveryDate"] = TodayDeliveryReport;

            return View();
        }
        public ActionResult ChequeAutoPrint()
        {
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = reportprovider.getParty();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
             
            return View();

        }
         
        public JsonResult ChequeAutoPrintDetails(string fromdate, string Todate, string searchCerteria, string Searchtext)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetChequeprintList '" + fromdate + "','" + Todate + "','" + searchCerteria + "','" + Searchtext + "'");

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            dt.Columns.Remove("Print");
            Session["ListOfCheques"] = dt;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult AutoChequexPrint(string ReceiptNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable lblGetchequeno = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("USP_GetChequeprint '" + ReceiptNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                lblGetchequeno = getJobOrderSet.Tables[0];



                foreach (DataRow dr in lblGetchequeno.Rows)
                {
                    ViewBag.Date = dr["Date"];
                    ViewBag.Pay = dr["Pay"];
                    ViewBag.AmountWords = dr["AmountWords"];
                    ViewBag.Amount = dr["Amount"];
                }

            }

            return PartialView();

        }
        public ActionResult ExportToExcelCheque()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["ListOfCheques"];
            //DataTable dt = (DataTable)ViewData["VoucherDetails"];
            string Tittle = "From " + Session["FromDate"] + " To " + Session["ToDate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ImportHubIn.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Import Hub In Summary<strong></td></tr>");
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

        public ActionResult AccrudeDetails()        {            return View();        }

        public ActionResult ExportToExcelAccrudeDetails(string ToDate)
        {

            string Blank = "";            string Blank1 = "";
            DataTable lblGetchequeno = new DataTable();
            DataSet getJobOrderSet = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            DataSet dtProduct = db.sub_GetDataSets("USP_ShowCalculate_Accrude_Amt'" + Blank + "','" + Blank1 + "','" + Blank1 + "','" + Convert.ToDateTime(ToDate).ToString("yyyyMMddHHmm") + "','" + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy HH:mm") + "'");
            string FileName = "";            string DispName = "";
              lblGetchequeno = dtProduct.Tables[2];

            FileName = "Accrude";            DispName = "Accrude";
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = lblGetchequeno.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in lblGetchequeno.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field =>
                  string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                sb.AppendLine(string.Join(",", fields));
            }
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".csv");

            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(sb);
            Response.Flush();
            Response.End();

            return View();
        }

        //Cancel Periodic Billing
        public ActionResult Cancel_periodic(string ContainerNo, string Jono)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable CompanyMaster = new DataTable();
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("[USP_Cancel_periodic] '" + ContainerNo + "','" + Jono + "','" + Userid + "'");



            if (dt.Rows.Count > 0)
            {
                message = Convert.ToString((dt.Rows[0]["mgs"]));

            }
            else
            {

                message = "";



            }


            return Json(message);

        }
    }

}

 