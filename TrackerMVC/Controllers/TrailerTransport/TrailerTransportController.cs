using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using DP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrailerTransport;
using CI = TrackerMVCEntities.BusinessEntities;
using TrackerMVC.Filters;
using System.Dynamic;
using HC = TrackerMVCDataLayer.Helper;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using TrackerMVCEntities.BusinessEntities;

namespace TrackerMVC.Controllers.TrailerTransport
{
    [UserAuthenticationFilter]

    public class TrailerTransportController : Controller
    {
        // GET: Trailer
        BM.LoadingPlan.LoadingPlan LP = new BM.LoadingPlan.LoadingPlan();
        DP.TrailerDataProvider objTrailerProvider = new DP.TrailerDataProvider();
        public ActionResult TrailerTransport()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            List<CI.TransporterEntities> TransporterList = new List<CI.TransporterEntities>();
            List<CI.VehicleTypeEntities> VehicleTypeList = new List<CI.VehicleTypeEntities>();
            List<CI.Model_M> Model_Master = new List<CI.Model_M>();
            CI.TrailerEntities TrailerList = objTrailerProvider.getDropDownList();
            TransporterList = TrailerList.TransporterList;
            VehicleTypeList = TrailerList.VehicleTypeList;
            ViewBag.TransporterList = new SelectList(TransporterList, "TransID", "TransName");
            ViewBag.VehicleTypeList = new SelectList(VehicleTypeList, "VehicleTypeID", "VehicleType");

            List<CI.TransporterEntities> Transpoter = new List<CI.TransporterEntities>();
            Transpoter = objTrailerProvider.getTranspoter();


            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");




            List<CI.VehicleTypeGroup> VehicleGroupID = new List<CI.VehicleTypeGroup>();
            List<CI.FuelType> FuelType = new List<CI.FuelType>();
            VehicleGroupID = objTrailerProvider.GetVehicleGroup();
            FuelType = objTrailerProvider.GetFuelMaster();
            ViewBag.VehicleGroupType = new SelectList(VehicleGroupID, "VehicleTypeid", "VehicleTypeGroupNae");
            ViewBag.Fueltype = new SelectList(FuelType, "Fuelid", "FuelName");

            Model_Master = objTrailerProvider.GetModel_Master();
            ViewBag.Model_M = new SelectList(Model_Master, "Modelid", "ModelName");
            return View();
        }
        [HttpPost]
        public JsonResult Save(CI.TrailerEntities TrailerTransport)
        {
            var EntryDate = TrailerTransport.EntryDate;

            HC.DBOperations db = new HC.DBOperations();
            db.sub_ExecuteNonQuery("USP_INSERT_TRAILERS '" + Convert.ToDateTime(EntryDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + TrailerTransport.TrailerNo + "','" + TrailerTransport.OwnedBy + "','','0','" + TrailerTransport.TransID + "','" + TrailerTransport.VehicleTypeID + "',''," + TrailerTransport.chkIsActive + ",'" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + TrailerTransport.Permit + "','" + Convert.ToDateTime(TrailerTransport.PermitExpity).ToString("yyyy-MM-dd HH:mm:ss") + "','" + TrailerTransport.VehicleGroup + "','" + TrailerTransport.IsShifting + "','" + TrailerTransport.VendorName + "','" + TrailerTransport.Vehicletypegroup + "','" + TrailerTransport.Fueltype + "','" + TrailerTransport.PurchasedDate + "','" + TrailerTransport.SoldDate + "','" + TrailerTransport.MFGYR + "','" + TrailerTransport.ModelID + "','" + TrailerTransport.Mileage + "','" + TrailerTransport.GVW + "'");
            //Master();
            return Json(TrailerTransport);
        }

        //Codes By Prashant

        [HttpPost]
        public JsonResult AjaxCheckTrailerNumber(string TrailerNumber)
        {
            string Message = "";
            Message = objTrailerProvider.CheckTrailerNumber(TrailerNumber);

            return Json(Message);
        }

        [HttpPost]
        public JsonResult AjaxGettrailersDetails(string vehicletype)
        {
            List<CI.TrailersEntities> TrailerDetails = new List<CI.TrailersEntities>();
            TrailerDetails = objTrailerProvider.GetTrailersDetails(vehicletype);
            var jsonResult = Json(TrailerDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
            
        }



        [HttpPost]
        public ActionResult AjaxUpdateTrailerDetails(int trailerid, string trailerNo, string ownedby, string Transportername, string passing, string grade, string modelno, string chasisno, string engine, string trailertype, string permit, string permittype, int EditchkIsActive, string EditVehicleTYpe,
             string EditIsshifting, string EditVehicleGroupType, string PolicyDate, string GreenTax, string PucDate, string UsedFor, string Taxdate,
             string Fitnessdate, string EditVEhicleTypeID, string EditVendorName, string TrailerSize, string VehicleGroupType, string FuelType,string Model,string SoldDateEdit,string MFGYREdit,string MileageEDIt,string GVWEDIT)
        {

            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = objTrailerProvider.UpdateTrailerDetails(trailerid, ownedby, Transportername, passing, grade, modelno, chasisno, engine,
                trailertype, permit, permittype, userId, EditchkIsActive, EditVehicleTYpe, EditIsshifting, EditVehicleGroupType,
                PolicyDate, GreenTax, PucDate, UsedFor, Taxdate, Fitnessdate, EditVEhicleTypeID, EditVendorName, TrailerSize, VehicleGroupType, FuelType, Model, SoldDateEdit, MFGYREdit, MileageEDIt, GVWEDIT);

            return Json(message);
        }
        // Codes End By Prashant


        public ActionResult VehicleReporting()
        {

            return View();
        }

        public ActionResult SaveVehicleReporting(string Reprtingdate, string Trailerno, int Driverid, string Contactno, string Remakes)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = objTrailerProvider.SaveVehicleReporting(Reprtingdate, Trailerno, Driverid, Contactno, Remakes, userId);

            return Json(message);
        }

        [HttpPost]
        public ActionResult AjaxVehicleReportingDetails()
        {
            List<CI.VehicleReportingEntities> VehicleReportingList = new List<CI.VehicleReportingEntities>();
            VehicleReportingList = objTrailerProvider.GetVehicleReporting();

            var jsonResult = Json(VehicleReportingList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Codes By Manish

        public ActionResult PendingVehicleSummary()
        {
            List<CI.VehicleReportingEntities> TPApprovedetails = new List<CI.VehicleReportingEntities>();
            TPApprovedetails = objTrailerProvider.GetVehicleReportingSummary();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_READY_VEHICLES_FOR_REPORTING");
            dt.Columns.Remove("driverid");
            Session["TPApprovedetails"] = dt;
            return View(TPApprovedetails);


        }

        public ActionResult ExportToExcelPendingVehicle()
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
            DataTable TPApprovedetails = (DataTable)Session["TPApprovedetails"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = TPApprovedetails;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PendingVehicleForReporting .xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Pending Vehicle For Reporting <strong></td></tr>");
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

        public ActionResult SaveVehiclePendency(string Reprtingdate, string Trailerno, string Driverid, string Contactno, string Remakes)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = objTrailerProvider.SaveVehiclePendencySummary(Reprtingdate, Trailerno, Driverid, Contactno, Remakes, userId);

            return Json(message);
        }


        //Codes ENd By Manish

        //Code by Prashant

        public ActionResult ReadyVehicleForMovement()
        {
            List<CI.ReadyVehicleMovementEntities> ReadyVehicleMovementList = new List<CI.ReadyVehicleMovementEntities>();
            ReadyVehicleMovementList = objTrailerProvider.GetReadyVehicleReporting();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ReadyVehicleMovementList);
            Session["ReadyVehicleMovementList"] = dt;
            List<CI.LoadingPlanEntities> LoadingPlanList = new List<CI.LoadingPlanEntities>();
            LoadingPlanList = LP.getLoadingContainerList();
            ViewBag.LoadingPlan = LoadingPlanList;
            ViewBag.ReturnCount = LoadingPlanList.Count();

            return View(ReadyVehicleMovementList);
        }

        //Code End By Prashant

        [HttpPost]
        public ActionResult PrintVehicleLoading(string containerNo, string TrailerNo, long JONO)
        {

            List<CI.PrintVehicleDetails> ReadyVehicleMovementList = new List<CI.PrintVehicleDetails>();
            ReadyVehicleMovementList = objTrailerProvider.GetPrintDetailsforVehicle(containerNo, TrailerNo, JONO);
            ViewBag.PlanID = ReadyVehicleMovementList[0].PlanID;
            ViewBag.PlanDate = ReadyVehicleMovementList[0].PlanDate;
            ViewBag.JoNO = ReadyVehicleMovementList[0].JoNO;
            ViewBag.TrailerNo = ReadyVehicleMovementList[0].TrailerNo;
            ViewBag.Containerno = ReadyVehicleMovementList[0].Containerno;
            ViewBag.fromlocation = ReadyVehicleMovementList[0].fromlocation;
            ViewBag.tolocation = ReadyVehicleMovementList[0].tolocation;
            ViewBag.ChaName = ReadyVehicleMovementList[0].ChaName;
            ViewBag.Drivername = ReadyVehicleMovementList[0].Drivername;
            ViewBag.size = ReadyVehicleMovementList[0].size;
          
            return PartialView();
            
        }

        [HttpPost]
        public ActionResult SaveVehicleReporting(string containerNo, string TrailerNo, long JONO,string TOdegistaton,string Size)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = objTrailerProvider.VehicleSaveLoadingPlan(containerNo, TrailerNo, JONO, Userid, TOdegistaton, Size);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintLoading()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveVendorReporting(string TrailerName, string vendor)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = objTrailerProvider.SaveVendorName(TrailerName, vendor);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ThirdPartyVehicle()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            List<CI.TransporterEntities> Transpoter = new List<CI.TransporterEntities>();
            Transpoter = objTrailerProvider.getTranspoter();
            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");
            return View();
        }

        public JsonResult SaveVehicleDetails(string Vehicleno, string TransID, string vehiclegroup, string EntryID, Boolean IsActive)        {            DataTable tblInvoiceList = new DataTable();            HC.DBOperations db = new HC.DBOperations();            int userId = Convert.ToInt32(Session["Tracker_userID"]);            var retVal = 0;            string Message = "";            retVal = db.sub_ExecuteNonQuery("USP_ThirdPartyVehicle '" + Vehicleno + "','" + TransID + "','" + vehiclegroup + "','" + EntryID + "','" + IsActive + "','" + userId + "'");            if (retVal > 0)            {                Message = "Record Saved Successfully.";            }            else            {                Message = "Records Not Saved Successfully.";            }            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };        }

        //public JsonResult Search(string search)
        //{
        //    HC.DBOperations db = new HC.DBOperations();
        //    DataTable dt = new DataTable();
        //    dt = db.sub_GetDatatable("GetThirdPartyVDetails'" + search + "'");
        //    var summaryDet = JsonConvert.SerializeObject(dt);
        //    var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}

        public JsonResult Search(string search)        {            DataTable dt = new DataTable();            HC.DBOperations db = new HC.DBOperations();            dt = db.sub_GetDatatable("GetThirdPartyVDetails'" + search + "'");            Session["GetThirdPartyVDetails"] = dt;            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;        }        public ActionResult ExportToExcelThirdPartyReport()        {            DataTable dt = (DataTable)Session["GetThirdPartyVDetails"];            DataTable CompanyMaster = new DataTable();            HC.DBOperations db = new HC.DBOperations();            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);            string Tittle = "Third Party Vehicles";            GridView gridview = new GridView();            gridview.DataSource = dt;            dt.Columns.Remove("Edit");            gridview.DataBind();            Response.Clear();            Response.Buffer = true;            Response.Charset = "";            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";            Response.AddHeader("content-disposition", "attachment;filename=Third Party Vehicle Details.xls");            using (StringWriter sw = new StringWriter())            {                using (HtmlTextWriter htw = new HtmlTextWriter(sw))                {                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Third Party Vehicle Details <strong></td></tr>");                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());                    Response.Flush();                    Response.End();                }            }            return View();        }
    }
}