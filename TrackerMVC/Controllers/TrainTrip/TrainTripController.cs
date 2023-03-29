using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using System.Data;
using DB = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using HC = TrackerMVCDataLayer.Helper;
using System.Text;

namespace TrackerMVC.Controllers.TrainTrip
{
    [UserAuthenticationFilter]
    public class TrainTripController : Controller
    {
        BM.TrainTrip.TrainTrip Trip = new BM.TrainTrip.TrainTrip();
        DB.DBOperations db = new DB.DBOperations();
        // GET: TrainTrip
        
        //code by suraj
        public ActionResult TrainLoadingPlan()
        {
            //Get trip List
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.TrainTrip> TripList = new List<BE.TrainTrip>();
            TripList = Trip.GetTripList(Userid);
            ViewBag.TripList = new SelectList(TripList, "TripID", "TripNo");

            return View(TripList);
        }
        public JsonResult DeleteTempAllRecords()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            Trip.DeleteDataFromTempTable(Userid);
            return Json("dataDelete");
        }

        [HttpPost]
        public ActionResult GetTripNoWiseData(int TripID)
        {
            // Get TrainNo
            List<BE.TrainTrip> TrainNo = new List<BE.TrainTrip>();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            TrainNo = Trip.GetTrainNo(TripID, Userid);
            ViewBag.TrainNo = TrainNo[0].TrainNo;
            ViewBag.Origin = TrainNo[0].Origin;
            ViewBag.Destination = TrainNo[0].Destination;
            //Get WagonNo
            List<BE.TrainTrip> WagonList = new List<BE.TrainTrip>();
            WagonList = Trip.GetWagonList(ViewBag.TrainNo);
            ViewBag.WagonList = new SelectList(WagonList, "WagonID", "WagonNo");
            //Get Container No1 List
            List<BE.TrainTrip> ContainerList1 = new List<BE.TrainTrip>();
            ContainerList1 = Trip.GetContainerList();
            ViewBag.ContainerList1 = new SelectList(ContainerList1, "SrNo", "ContainerNo");
            //Get Container No2 List
            //List<BE.TrainTrip> ContainerList2 = new List<BE.TrainTrip>();
            //ContainerList2 = Trip.GetContainerList();
            //ViewBag.ContainerList2 = new SelectList(ContainerList2, "SrNo", "ContainerNo");


            var returnField = new { TrainNo = TrainNo, WagonList = WagonList, ContainerList1 = ContainerList1 };
            var jsonResult = Json(returnField, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            // return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            //var jsonResult = Json((ViewBag.WagonList, ViewBag.ContainerList1, ViewBag.ContainerList2), JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult SearchExportTripDetails()
        {
            List<BE.TrainTrip> TripDets = new List<BE.TrainTrip>();
            TripDets = Trip.GetTripDets();
            return PartialView(TripDets);
        }

        public JsonResult AddDomesticTempDataOnlyForWagon(string TrainNo, Int64 TripNo)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.TrainTrip TripSummary = new BE.TrainTrip();
            TripSummary = Trip.AddDomsticWagonDataOnly(TrainNo, Userid, TripNo);
            var jsonResult = Json(TripSummary, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetExportContainerSize(string ContainerNo, int Number, string WagonNo, int TripNo)
        {
            BE.TrainTrip SizeList = new BE.TrainTrip();
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            SizeList = Trip.GetExportSizeList(ContainerNo, Number, WagonNo, TripNo, UserID);
            //DataSet SizeDS = new DataSet();
            //SizeDS = db.sub_GetDataSets("USP_UPDATE_temp_wagonNoData '" + ContainerNo + "','" + Number + "','" + WagonNo + "','" + TripNo + "','" + UserID + "'");
            var jsonResult = Json(SizeList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult GetDeleteTraintripDetails(string ContainerNo, int Number, string WagonNo, int TripNo)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = Trip.DeletetheContainernofromwagon(ContainerNo, Number, WagonNo, TripNo, UserID);

            var jsonResult = Json(message, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult UpdateExportTempData(string ContainerNo, int Number, string WagonNo, int TripNo, int Size, string NetWt1, string TareWt1, string ActualWt1, string Status1, string commodity1, string POL1)
        {
            List<BE.TotalCountSizeWise> TripList = new List<BE.TotalCountSizeWise>();
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            TripList = Trip.UpdateExportData(ContainerNo, Number, WagonNo, TripNo, UserID, Size, NetWt1, TareWt1, ActualWt1, Status1, commodity1, POL1);
            var jsonResult = Json(TripList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult AddTempWagonData(string WagonNo, string Container1, string Container2, Int64 TripNo)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.WagonContainerList JDContainerList = new BE.WagonContainerList();
            JDContainerList = Trip.AddWagonData(WagonNo, Container1, Container2, Userid, TripNo);
            return Json(JDContainerList);
        }

        [HttpPost]
        public ActionResult GetSizeAgainstContainer1(string ContainerNo1)
        {
            List<BE.TrainTrip> Size1 = new List<BE.TrainTrip>();
            Size1 = Trip.GetSize1(ContainerNo1);
            ViewBag.Size1 = Size1;

            var jsonResult = Json(Size1, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult SaveDomesticTrainData(BE.TrainTrip TripDetails)
        {
            string Message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            Message = Trip.DomesticTrainData(TripDetails, userId);
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDomesticTrainTripsForModify()
        {
            List<BE.TrainTrip> TrainImportList = new List<BE.TrainTrip>();
            TrainImportList = Trip.GetDomesticTrainTripsForModify();
            var jsonResult = Json(TrainImportList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult SubmitDomesticTrainTrip(BE.TrainTrip TripDetails)
        {
            string Message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            Message = Trip.DomesticTrainData(TripDetails, userId);

            Message = Trip.SubmitEDomesticTrainTripList(TripDetails, userId);
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

    }
}