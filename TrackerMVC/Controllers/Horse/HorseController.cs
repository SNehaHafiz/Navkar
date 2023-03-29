using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CI = TrackerMVCEntities.BusinessEntities;
using CD = TrackerMVCDataLayer.Helper;
using DB = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.HorseSummary;
using TrackerMVC.Filters;
using System.Data;

namespace Transport.Controllers.Horse
{
    [UserAuthenticationFilter]
    public class HorseController : Controller
    {
        
        DB.HorseSummary trainTrackerProvider = new DB.HorseSummary();
        [HttpPost]
        public JsonResult Save(CI.Horse Horse)
        {
            var EntryDate = Horse.EntryDate;

            CD.DBOperations db = new CD.DBOperations();
            db.sub_ExecuteNonQuery("USP_INSERT_HORSEMASTER '" + Convert.ToDateTime(EntryDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Horse.HorseNumber + "','" + Horse.InstalledTyres + "','" + Horse.Weight + "','" + Horse.Model + "','"  + Convert.ToInt32(Session["Tracker_userID"]) + "'");
            //Master();
            return Json(Horse);
        }
        public ActionResult NewTrainDetails()
        {
            //ViewBag.Message = TempData["MessageValue"];
            string message = "";
            message = Convert.ToString(TempData["MessageValue"]);
            ViewBag.Message = message;
            List<CI.HorseSummary> operatorDetails = new List<CI.HorseSummary>();
            operatorDetails = trainTrackerProvider.GetHorseSummary();
            ViewBag.OperatorList = new SelectList(operatorDetails, "EntryID", "HorseNumber");
            return View();
        }

        public JsonResult GetHorseSummary()
        {
            List<CI.HorseSummary> trainList = new List<CI.HorseSummary>();
            trainList = trainTrackerProvider.GetHorseSummary();
            return Json(trainList, JsonRequestBehavior.AllowGet);
        }
        // GET: Horse
        public ActionResult Horse()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }
        // Codes By Prashant
        [HttpPost]
        public JsonResult AjaxCheckHorseNumber(string HorseNumber)
        {
            string Message = "";
            Message = trainTrackerProvider.CheckHorseNumber(HorseNumber);

            return Json(Message);
        }

        // Codes  End By Prashant
        [HttpPost]
        public JsonResult getUSP_Gettrailers(string prefixText, string Mode)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<CI.Trailer> Horselst = new List<CI.Trailer>();
            dataTable = db.sub_GetDatatable("USP_Gettrailers '" + Mode + "','" + prefixText + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    CI.Trailer Horse = new CI.Trailer();
                    Horse.TrailerID = Convert.ToInt32(row["GSTID"]);
                    Horse.TrailerNo = Convert.ToString(row["GSTName"]);
                    Horselst.Add(Horse);
                }
            }

            var jsonResult = Json(Horselst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
    }
}