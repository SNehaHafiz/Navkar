using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using CI = TrackerMVCEntities.BusinessEntities;
using CD = TrackerMVCDataLayer.Helper;
using DB = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ModifyVehicleMovement;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using HC = TrackerMVCDataLayer.Helper;
namespace TrackerMVC.Controllers.ModifyVehicleMovement
{
    public class ModifyVehicleMovementController : Controller
    {
        [UserAuthenticationFilter]
        // GET: ModifyVehicleMovement
        public JsonResult Save(CI.ModifyVehicleMovementEntitiesave ModifyVehicleMovementEntitiesave)
        {
            int i = 0;
            //var EffectiveFromDate = TPTariffDetailsEntities.EffectiveFromDate;
            //var EffectivetoDate = TPTariffDetailsEntities.EffectivetoDate;
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("usp_Update_Vehicle_Transactions_Movement '" + ModifyVehicleMovementEntitiesave.Activity + "','" + ModifyVehicleMovementEntitiesave.ContainerNo + "','" + ModifyVehicleMovementEntitiesave.ID + "','" + ModifyVehicleMovementEntitiesave.ToID + "','" + ModifyVehicleMovementEntitiesave.TrailerNo + "','" + ModifyVehicleMovementEntitiesave.Remarks + "'");
            //Master();

            return Json(i);
        }
        public ActionResult ModifyVehicleMovement()
        {
            string name = "";
            int id = 0;
            List<CI.LocationFrom> Location = new List<CI.LocationFrom>();
            Location = trainTrackerProvider.GetExisitingLocationame(name, id);
            ViewBag.Location = new SelectList(Location, "ID", "Criteria");

            List<CI.LocationTo> LocationTo = new List<CI.LocationTo>();
            LocationTo = trainTrackerProvider.GetELocationame(name, id);
            ViewBag.LocationTo = new SelectList(LocationTo, "ToID", "ToCriteria");

            return View();


        }
        DB.ModifyVehicleMovement trainTrackerProvider = new DB.ModifyVehicleMovement();



        //public JsonResult ajaxSearchlistdetails(string Activity, string ContainerNo)
        //{

        //    CI.ModifyVehicleMovementEntities ImportSearchdetails = new CI.ModifyVehicleMovementEntities();
        //    ImportSearchdetails = trainTrackerProvider.GetAccidentSearchDetails(Activity, ContainerNo);

        //    var returnField = new { ImportList = ImportSearchdetails };
        //    return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        //}
        [HttpPost]
        public ActionResult ajaxSearchlistdetails(string VehicleNo, string Activity)
        {
            List<CI.modifyVehiclemovemetDetailsentiites> trailerName = new List<CI.modifyVehiclemovemetDetailsentiites>();

            trailerName = trainTrackerProvider.GetVehicleMovementdetails(VehicleNo, Activity);


            var jsonResult = Json(trailerName, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult AjaxGetTrailerNo(string TrailerNo)    
        {
            List<CI.trailer> trailerName = new List<CI.trailer>();
            trailerName = trainTrackerProvider.getTrailerNo(TrailerNo);
            string Data = trailerName[0].trailerName;
            return Json(Data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateVehicleDetails(List<CI.modifiedTrailerMovementEntities> TransDetails , string MovementType, string VehicleNo,
            string LocationFrom, string Tolocation, string NewVehicleNo,
            string Remarks)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("TransID");
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("FromLocation");
            dataTable.Columns.Add("ToLocation");
            dataTable.Columns.Add("ContainerNO");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("JoNOEntryID");
            dataTable.Columns.Add("ContainerType");


            dataTable.TableName = "PT_AddVehicleMovementDetails";


            foreach (CI.modifiedTrailerMovementEntities item in TransDetails)
            {
                DataRow row = dataTable.NewRow();

                row["TransID"] = item.TransID;
                row["Activity"] = item.Activity;
                row["FromLocation"] = item.FromLocation;
                row["ToLocation"] = item.ToLocation;
                row["ContainerNO"] = item.ContainerNO;
                row["Size"] = item.Size;
                row["JoNOEntryID"] = item.JoNOEntryID;
                row["ContainerType"] = item.ContainerType;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = trainTrackerProvider.UpdateTrailerDetails(dataTable, MovementType, VehicleNo, LocationFrom, Tolocation, NewVehicleNo, Remarks, Userid);
            return Json(message);

        }
    }
}