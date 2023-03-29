using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using CU = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Login;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Report;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using TrackerMVC.Filters;
using System.Data;
using System.Data.Sql;
using HC = TrackerMVCDataLayer.Helper;
using monthEnum = TrackerMVCEntities.Enums;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace TrackerMVC.Controllers.PipeLineReport
{
    [UserAuthenticationFilter]
    public class PipeLineReportController : Controller
    {
        RP.Report reportprovider = new RP.Report();
        BM.BLDataManager.GenerateBL BL = new BM.BLDataManager.GenerateBL();
        CU.Login objTrackerProvider = new CU.Login();
        // GET: PipeLineReport
        public ActionResult Index()
        {
            return View();
        }
        [UserAuthenticationFilter]
        public ActionResult PipeLineReport()
        {
            
            DataTable dt = new DataTable();
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month - 1, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            dt = objTrackerProvider.GetPipeLineArrival(DateTime.Now.Month);

            ViewBag.totalTEUSForPipeLineDisplay = objTrackerProvider.totalTEUSForPipeLineDisplay;
            ViewBag.donutValuePipeLineDisplay = objTrackerProvider.donutValuePipeLineDisplay;
            return View(dt);
        }

        [UserAuthenticationFilter]
        [HttpPost]
        public JsonResult GetMonthlyDataForPipeLineReportPerson(string personName)
        {
            List<BE.GraphDetailsForPerson> nameList = new List<BE.GraphDetailsForPerson>();
            nameList = objTrackerProvider.GetMonthlyDataForPipeLineReportPerson(personName);

            return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        [UserAuthenticationFilter]
        [HttpPost]
        public JsonResult GetPipelineReportForGivenMonth(string month)
        {
            List<BE.CustArrivalEntities> ce = new List<BE.CustArrivalEntities>();

            int monthNo = getMonthNO(month);

            ce = objTrackerProvider.GetPipeLineArrivalMonthWise((monthNo + 1));
            if (ce.Count != 0)
            {
                ViewBag.newDonutValue = ce[ce.Count - 1].donutValue;
            }
           
            //ModelState.Clear();
            return Json(ce, JsonRequestBehavior.AllowGet);
        }
        [UserAuthenticationFilter]
        [HttpPost]
        public JsonResult GetMonthlyTEUSForPipeLine()
        {
            List<BE.LineGraphValue> lineGraph = new List<BE.LineGraphValue>();
            lineGraph = objTrackerProvider.GetMonthlyTEUSForPipeLine();

            return Json(lineGraph, JsonRequestBehavior.AllowGet);
        }
        

        private int getMonthNO(string month)
        {
            if (month == "Jan")
            {
                return (int)monthEnum.MonthEnum.Jan;
            }
            if (month == "Feb")
            {
                return (int)monthEnum.MonthEnum.Feb;
            }
            if (month == "Mar")
            {
                return (int)monthEnum.MonthEnum.Mar;
            }
            if (month == "Apr")
            {
                return (int)monthEnum.MonthEnum.Apr;
            }
            if (month == "May")
            {
                return (int)monthEnum.MonthEnum.May;
            }
            if (month == "June")
            {
                return (int)monthEnum.MonthEnum.June;
            }
            if (month == "July")
            {
                return (int)monthEnum.MonthEnum.July;
            }
            if (month == "Aug")
            {
                return (int)monthEnum.MonthEnum.Aug;
            }
            if (month == "Sep")
            {
                return (int)monthEnum.MonthEnum.Sep;
            }
            if (month == "Oct")
            {
                return (int)monthEnum.MonthEnum.Oct;
            }
            if (month == "Nov")
            {
                return (int)monthEnum.MonthEnum.Nov;
            }
            if (month == "Dec")
            {
                return (int)monthEnum.MonthEnum.Dec;
            }
            return 0;
        }
        //Codes BY Prashant

        //public ActionResult DailyMovementReport()
        //{
        //    List<BE.DailyMovementRequestEntities> DailyMovement = new List<BE.DailyMovementRequestEntities>();
        //    DailyMovement = objTrackerProvider.GetrDailyMovement();
        //    List<BE.CustomerWiseLDDReport> ImportDelivery = new List<BE.CustomerWiseLDDReport>();
        //    ImportDelivery = reportprovider.GetImportDelivery();
        //    ViewBag.ImportDeliverList = ImportDelivery;

        //    List<BE.CustomerWiseLDDReport> ImportStuffDelivery = new List<BE.CustomerWiseLDDReport>();
        //    ImportStuffDelivery = reportprovider.GetImportStuffDelivery();
        //    ViewBag.ImportDestuffList = ImportStuffDelivery;


        //    List<BE.MonthlyEntites> MonthlySummary = new List<BE.MonthlyEntites>();
        //    MonthlySummary = reportprovider.GetMonthlyreport();
        //    ViewBag.MonthlySummary = MonthlySummary;

        //    List<BE.CustomerWiseLDDReport> ImportJoHand = new List<BE.CustomerWiseLDDReport>();
        //    ImportJoHand = reportprovider.GetImportJo();
        //    ViewBag.ImportJo = ImportJoHand;

        //    List<BE.CustomerWiseLDDReport> upcomingList = new List<BE.CustomerWiseLDDReport>();
        //    upcomingList = reportprovider.GetPortWisePendency();
        //    ViewBag.PortWisePendency = upcomingList;


        //    List<BE.CustomerWiseLDDReport> GetImportInventory = new List<BE.CustomerWiseLDDReport>();
        //    GetImportInventory = reportprovider.GetImportInventory();
        //    ViewBag.ImportInventory = GetImportInventory;
        //    return View(DailyMovement);
        //}

        //Code by gauri
        public ActionResult DailyMovementReport()
        {
            List<BE.DailyMovementRequestEntities> DailyMovement = new List<BE.DailyMovementRequestEntities>();
            DailyMovement = objTrackerProvider.GetrDailyMovement();
            List<BE.CustomerWiseLDDReport> ImportDelivery = new List<BE.CustomerWiseLDDReport>();
            ImportDelivery = reportprovider.GetImportDelivery();
            ViewBag.ImportDeliverList = ImportDelivery;

            List<BE.CustomerWiseLDDReport> ImportStuffDelivery = new List<BE.CustomerWiseLDDReport>();
            ImportStuffDelivery = reportprovider.GetImportStuffDelivery();
            ViewBag.ImportDestuffList = ImportStuffDelivery;


            List<BE.MonthlyEntites> MonthlySummary = new List<BE.MonthlyEntites>();
            MonthlySummary = reportprovider.GetMonthlyreport();
            ViewBag.MonthlySummary = MonthlySummary;

            List<BE.CustomerWiseLDDReport> ImportJoHand = new List<BE.CustomerWiseLDDReport>();
            ImportJoHand = reportprovider.GetImportJo();
            ViewBag.ImportJo = ImportJoHand;

            List<BE.CustomerWiseLDDReport> upcomingList = new List<BE.CustomerWiseLDDReport>();
            upcomingList = reportprovider.GetPortWisePendency();
            ViewBag.PortWisePendency = upcomingList;


            List<BE.CustomerWiseLDDReport> GetImportInventory = new List<BE.CustomerWiseLDDReport>();
            GetImportInventory = reportprovider.GetImportInventory();
            ViewBag.ImportInventory = GetImportInventory;

            List<BE.CustomerWiseLDDReport> JOInHandMonthWise = new List<BE.CustomerWiseLDDReport>();
            JOInHandMonthWise = reportprovider.GetJOInHandMonthWise();
            ViewBag.JOInHandMonthWise = JOInHandMonthWise;

            List<BE.CustomerWiseLDDReport> JOReceived = new List<BE.CustomerWiseLDDReport>();
            JOReceived = reportprovider.GetJOReceived();
            ViewBag.JOReceived = JOReceived;

            List<BE.CustomerWiseLDDReport> INTRANSIT = new List<BE.CustomerWiseLDDReport>();
            INTRANSIT = reportprovider.GetINTRANSIT();
            ViewBag.INTRANSIT = INTRANSIT;

            return View(DailyMovement);
        }

        //code end by gauri


        public ActionResult JobOrderListDetails()
        {
            List<BE.SalesPersonM> SalesPersonM = new List<BE.SalesPersonM>();
            SalesPersonM = BL.getSalesPersonM();
            ViewBag.SalesPersonM = new SelectList(SalesPersonM, "SalesPerson_ID1", "SalesPerson_Name");
            return View();
        }
        [HttpPost]
        public JsonResult AjaxJobOrderDetails(int salesid,string date)
        {
            List<BE.JobOrderListSummaryEntities> MovementAtICDList = new List<BE.JobOrderListSummaryEntities>();
            string fromdate = "";
            string todate = "";
            if (date == "Monthly")
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                fromdate = startDate.ToString("yyyy-MM-dd");
                todate = endDate.ToString("yyyy-MM-dd 23:59");
               
            }
            if (date == "Yesterday")
            {
                 fromdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                 todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59");
            }
            if (date == "Today")
            {
                 fromdate = DateTime.Now.ToString("yyyy-MM-dd");
                 todate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }

            MovementAtICDList = objTrackerProvider.GetJoborderdetails(salesid, fromdate, todate);

            var jsonResult = Json(MovementAtICDList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Customer Wise Report
        [HttpPost]
        public JsonResult AjaxCustomerJObOrder(string date)
        {
            List<BE.JobOrderListSummaryEntities> MovementAtICDList = new List<BE.JobOrderListSummaryEntities>();
            string fromdate = "";
            string todate = "";
            if (date == "Monthly")
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                fromdate = startDate.ToString("yyyy-MM-dd");
                todate = endDate.ToString("yyyy-MM-dd 23:59");

            }
            if (date == "Yesterday")
            {
                fromdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59");
            }
            if (date == "Today")
            {
                fromdate = DateTime.Now.ToString("yyyy-MM-dd");
                todate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }

            MovementAtICDList = objTrackerProvider.GetCustomerJObOrder(fromdate, todate);

            var jsonResult = Json(MovementAtICDList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Sales Wise Report
        [HttpPost]
        public JsonResult AjaxSalesJObOrder(string date)
        {
            List<BE.JobOrderListSummaryEntities> MovementAtICDList = new List<BE.JobOrderListSummaryEntities>();
            string fromdate = "";
            string todate = "";
            if (date == "Monthly")
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                fromdate = startDate.ToString("yyyy-MM-dd");
                todate = endDate.ToString("yyyy-MM-dd 23:59");

            }
            if (date == "Yesterday")
            {
                fromdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59");
            }
            if (date == "Today")
            {
                fromdate = DateTime.Now.ToString("yyyy-MM-dd");
                todate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }

            MovementAtICDList = objTrackerProvider.GetSalesWise(fromdate, todate);

            var jsonResult = Json(MovementAtICDList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult PipeLineReports()
        {
            DateTime now = DateTime.Now;
            //var startDate = new DateTime(now.Year, now.Month, 1);
            //var endDate = startDate.AddMonths(1).AddDays(-1);
            //var  fromdate = startDate.ToString("yyyy-MM-dd");
            //var  todate = endDate.ToString("yyyy-MM-dd 23:59");
            List<BE.DailyMovementRequestEntities> PIpelineReportList = new List<BE.DailyMovementRequestEntities>();
            PIpelineReportList = objTrackerProvider.GetPipeline();
            ViewBag.PipeLineReports = objTrackerProvider.GetPipeline();
            return View(PIpelineReportList);
        }

        public ActionResult AjaxPipeLineReport(string Month)
        {
            List<BE.DailyMovementRequestEntities> PIpelineReportList = new List<BE.DailyMovementRequestEntities>();
            string Monthdate = "";
           
            if (Month == "May")
            {
                Monthdate = "5";

            }
            if (Month == "June")
            {
                Monthdate = "6";
            }
            if (Month == "July")
            {
                Monthdate = "7";
            }
            if (Month == "August")
            {
                Monthdate = "8";
            }
            if (Month == "september")
            {
                Monthdate = "9";
            }
            if (Month == "october")
            {
                Monthdate = "10";
            }
            if (Month == "november")
            {
                Monthdate = "11";
            }
            if (Month == "december")
            {
                Monthdate = "12";
            }
            PIpelineReportList = objTrackerProvider.GetPipelinemonth(Monthdate);

            var jsonResult = Json(PIpelineReportList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult FuelReport()
        {

            return View();
        }


        public ActionResult GetFuelReport(string fromdate, string todate)
        {
            List<BE.DailyMovementRequestEntities> GetFueReport = new List<BE.DailyMovementRequestEntities>();
            GetFueReport = objTrackerProvider.GetFuelReport(fromdate, todate);
            Session["FuelReport"] = GetFueReport;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            //var json = JsonConvert.SerializeObject(GetFueReport);
            DataTable dt = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("USP_SalesRegister_Container_HeadWise_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            dt = db.sub_GetDatatable("USP_Get_Fuel_Report '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["FuelReport"] = dt;
            Session["fromdate"] = fromdate;
            Session["todate"] = todate;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            var jsonResult = Json(GetFueReport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcel(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetExportTuesWiseRevenue = (DataTable)Session["FuelReport"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetExportTuesWiseRevenue;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=FuelReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Export TuesWise Revenue<strong></td></tr>");
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
    }
}