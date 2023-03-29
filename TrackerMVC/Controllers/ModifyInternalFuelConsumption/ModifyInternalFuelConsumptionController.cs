using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using System.Data;
using CD = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace TrackerMVC.Controllers.ModifyInternalFuelConsumption
{
    public class ModifyInternalFuelConsumptionController : Controller
    {
        public JsonResult Save(BE.ModifyInternalFuelConsumptionEntities ModifyInternalFuelConsumptionEntities)
        {
            int i = 0;
            //var EffectiveFromDate = TPTariffDetailsEntities.EffectiveFromDate;
            //var EffectivetoDate = TPTariffDetailsEntities.EffectivetoDate;
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("usp_update_FuelConsumption '" + ModifyInternalFuelConsumptionEntities.fuelRefNo + "','" + ModifyInternalFuelConsumptionEntities.Qty + "','" + ModifyInternalFuelConsumptionEntities.lastReading + "','" + ModifyInternalFuelConsumptionEntities.currentReading + "','" + ModifyInternalFuelConsumptionEntities.Remarks + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");
            //Master();

            return Json(i);
        }



        BM.ModifyInternalFuelConsumption.ModifyInternalFuelConsumption LP = new BM.ModifyInternalFuelConsumption.ModifyInternalFuelConsumption();
        [UserAuthenticationFilter]
        // GET: ModifyInternalFuelConsumption
        public ActionResult ModifyInternalFuelConsumption()
        {
            return View();
        }

        public JsonResult AjaxGetSlipFuelDetails(string SlipNo)
        {
            List<BE.ModifyInternalFuelConsumptionEntities> Containerdetails = new List<BE.ModifyInternalFuelConsumptionEntities>();
            Containerdetails = LP.GetFuelConsumptionDetails(SlipNo);
            var jsonResult = Json(Containerdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult AjaxCancelFuelConsumptionDetails(string SlipNo, string CancelRemarks)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.CancelFuelConsumptiondetails(SlipNo, CancelRemarks, Userid);
            return Json(message);
        }

        public ActionResult ISprintSave(string SlipNo)
        {
            string message = "";
            
            message = LP.ReprintFuelConsumptiondetails(SlipNo);
            return Json(message);
        }
    }
}