using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Newtonsoft.Json;
using HC = TrackerMVCDataLayer.Helper;

namespace TrackerMVC.Controllers.Trailer
{
     
    public class TrailerController : Controller
    {
         BM.Trailer.Trailer TL=new BM.Trailer.Trailer();
         BM.LoadingPlan.LoadingPlan LP = new BM.LoadingPlan.LoadingPlan();
        // GET: Trailer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TrailerGrade()
        {
            List<BE.TrailersEntities> TrailersList = new List<BE.TrailersEntities>();
            TrailersList = TL.getTrailer();

            return View(TrailersList);
        }

        [HttpPost]
        public JsonResult ModifyTrailerGrade(List<BE.TrailersEntities> trailerList)
        {
            string message = "";

            int userId = Convert.ToInt32(Session["Tracker_userID"]);

          //  message = BL.AddJobOrderData(viewModel, containerData, userId);
            // ViewBag.Message = message;
            DataTable TrailerDT = new DataTable();

            TrailerDT.Columns.Add("trailerid");
            TrailerDT.Columns.Add("Grade");
            TrailerDT.Columns.Add("userID");

            TrailerDT.TableName = "PT_TrailerGrade";
            foreach (BE.TrailersEntities item in trailerList)
            {
                DataRow row = TrailerDT.NewRow();
                row["trailerid"] = item.trailerid;
                row["Grade"] = item.Grade;
                row["userID"] = userId;

                TrailerDT.Rows.Add(row);
            }

            message = TL.UpdateTrailerGrade(TrailerDT, userId);

            return Json(message);
        }


        public ActionResult TrolleySummary()
        {
            List<BE.TrolleyEntities> TrollyList = new List<BE.TrolleyEntities>();
            TrollyList = TL.getTrolly();

            return View(TrollyList);
        }

        public ActionResult TrailerTrolly()
        {
            List<BE.TrailersEntities> TrailersList = new List<BE.TrailersEntities>();
            TrailersList = TL.getTrailer();

            List<BE.TrailersEntities> TrolleyList = new List<BE.TrailersEntities>();
            TrolleyList = TL.getTrollyForMapping();

           
            ViewBag.TrolleyList = new SelectList(TrolleyList, "TrollyID", "TrolleyNo");
           // ViewBag.TrolleyList = TrailersList[0].TrollyID;
            
            return View(TrailersList);
        }

        public JsonResult AddTrolley(BE.TrailersEntities TrolleyData)
        {
            string message = "";

            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = TL.AddTrolleyData(TrolleyData, userId);
            return Json(message);
        }

        public JsonResult ModifyTrailerTrolley(List<BE.TrailersEntities> trailerList)
        {
            string message = "";

            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            //  message = BL.AddJobOrderData(viewModel, containerData, userId);
            // ViewBag.Message = message;
            DataTable TrailerDT = new DataTable();

            TrailerDT.Columns.Add("trailerid");
            TrailerDT.Columns.Add("trolley");
            TrailerDT.Columns.Add("userID");

            TrailerDT.TableName = "PT_TrailerTrolley";
            foreach (BE.TrailersEntities item in trailerList)
            {
                DataRow row = TrailerDT.NewRow();
                row["trailerid"] = item.trailerid;
                row["trolley"] = item.TrollyID;
                row["userID"] = userId;

                TrailerDT.Rows.Add(row);
            }

            message = TL.UpdateTrailerTrolley(TrailerDT, userId);

            return Json(message);
        }

        public ActionResult TrailerDriver()
        
        {
            List<BE.TrailersEntities> TrailersDriverList = new List<BE.TrailersEntities>();
            TrailersDriverList = TL.getTrailerDriver();

            List<BE.DriversEntities> DriverList = new List<BE.DriversEntities>();
            DriverList = LP.getDrivers();

            ViewBag.DriverList = new SelectList(DriverList, "driverID", "driverName");
            // ViewBag.TrolleyList = TrailersList[0].TrollyID;

            return View(TrailersDriverList);
        }

        //public JsonResult ModifyTrailerDriver(List<BE.DriverTrailerEntities> trailerList12)
        //{
        //    string message = "";

        //    int userId = Convert.ToInt32(Session["Tracker_userID"]);

        //    //  message = BL.AddJobOrderData(viewModel, containerData, userId);
        //    // ViewBag.Message = message;
        //    DataTable TrailerDT = new DataTable();

        //    TrailerDT.Columns.Add("trailerid");
        //    TrailerDT.Columns.Add("driver");
        //    TrailerDT.Columns.Add("userID");

        //    TrailerDT.TableName = "PT_DriverTrailer";
        //    foreach (BE.DriverTrailerEntities item in trailerList12)
        //    {
        //        DataRow row = TrailerDT.NewRow();
        //        row["trailerid"] = item.trailerid;
        //        row["driver"] = item.DriverID1;
        //        row["userID"] = userId;

        //        TrailerDT.Rows.Add(row);
        //    }

        //    message = TL.UpdateTrailerDriver(TrailerDT, userId);

        //    return Json(message);
        //}

        [HttpPost]
        public JsonResult UpdateTrailerDriver(string trailerID, string driverID)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = TL.UpdateTrailerDriver(trailerID, driverID, userId);

            return Json(message);

        }


        [HttpPost]
        public JsonResult AjaxDriverNo(string DriverCOde)
        {
            List<BE.TrailersEntities> Trailerno = new List<BE.TrailersEntities>();
            Trailerno = TL.GetDriverno(DriverCOde);
            return new JsonResult() { Data = Trailerno, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public ActionResult SearchTrailer()
        {
            return View();
        }

        public ActionResult AjaxGetTrailerDetails(string trailername)
        {
            List<BE.TrailerSearchEntities> TrailerDetails = new List<BE.TrailerSearchEntities>();
            TrailerDetails = TL.GetTrailerDetails(trailername);
            var JsonResult = Json(TrailerDetails, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;


        }
        [HttpPost]
        public ActionResult GetTrailerMovementdetails(string Trailername)
        {
            DataTable GetHSNCodeWiseSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetHSNCodeWiseSummary = db.sub_GetDatatable("USPSearchTrailersDetails '" + Trailername + "'");
            Session["SearchTrailersDetails"] = GetHSNCodeWiseSummary;
            Session["TrailerName"] = Trailername;

            var json = JsonConvert.SerializeObject(GetHSNCodeWiseSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        //Trailer And Trolley Mapping
        public ActionResult TrailerTrollyMapping()
        {
            List<BE.TrailersEntities> TrailersList = new List<BE.TrailersEntities>();
            TrailersList = TL.getTrailer();

            List<BE.TrailersEntities> TrolleyList = new List<BE.TrailersEntities>();
            TrolleyList = TL.getTrollyForMapping();


            ViewBag.TrolleyList = new SelectList(TrolleyList, "TrollyID", "TrolleyNo");
            // ViewBag.TrolleyList = TrailersList[0].TrollyID;

            return View(TrailersList);
        }
        public JsonResult Update(string TrailerNo, string TrolleryNo)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("TrailerAndTrolleyMapping '" + TrailerNo + "','" + TrolleryNo + "','" + userId + "'");

            if (retVal > 0)
            {
                Message = "Record Save Successfully.";
            }
            else
            {
                Message = "Records Not Save Successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult Summary()
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("Usp_Remove_Summery");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult UpdateRemove(string TrolleyNo)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("UpdateRemoveMapping '" + TrolleyNo + "','" + userId + "'");

            if (retVal > 0)
            {
                Message = "Record Remove Successfully.";
            }
            else
            {
                Message = "Records Not Remove Successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult Validation_Trailer(string TrailerNo)
        {

            int i = 0;
            string Messageget = "";
            DataTable dt = new DataTable();
            string strSql = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            HC.DBOperations db = new HC.DBOperations();

            strSql = "";
            strSql = "USP_Validation_Trailer_No'" + TrailerNo + "'";
            dt = db.sub_GetDatatable(strSql);
            if (dt.Rows.Count > 0)
            {
                Messageget = Convert.ToString(dt.Rows[0]["mgs"]);
            }

            return Json(Messageget);

        }
        public JsonResult Validation_Trolley(string TrolleryNo)
        {

            int i = 0;
            string Messageget = "";
            DataTable dt = new DataTable();
            string strSql = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            HC.DBOperations db = new HC.DBOperations();

            strSql = "";
            strSql = "USP_Validation_Trolley_No'" + TrolleryNo + "'";
            dt = db.sub_GetDatatable(strSql);
            if (dt.Rows.Count > 0)
            {
                Messageget = Convert.ToString(dt.Rows[0]["mgs"]);
            }

            return Json(Messageget);

        }
        [HttpPost]
        public JsonResult getTrolleyNo(string prefixText, string Mode)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.TrolleyM> Trolleylst = new List<BE.TrolleyM>();
            dataTable = db.sub_GetDatatable("USP_GetTrolleyM '" + Mode + "','" + prefixText + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.TrolleyM Trolley = new BE.TrolleyM();
                    Trolley.TrolleyID = Convert.ToInt32(row["GSTID"]);
                    Trolley.TrolleyNo = Convert.ToString(row["GSTName"]);
                    Trolleylst.Add(Trolley);
                }
            }

            var jsonResult = Json(Trolleylst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
    }
}