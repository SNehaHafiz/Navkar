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
namespace TrackerMVC.Controllers.ModifyExternalFuelSlip
{
    [UserAuthenticationFilter]
    public class ModifyExternalFuelSlipController : Controller
    {
        public JsonResult Save(BE.ModifyExternalFuelSlipEntities ModifyExternalFuelSlipEntities)
        {
            int i = 0;
            //var EffectiveFromDate = TPTariffDetailsEntities.EffectiveFromDate;
            //var EffectivetoDate = TPTariffDetailsEntities.EffectivetoDate;
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("usp_update_fuel_M '" + ModifyExternalFuelSlipEntities.fuelRefNo + "','" + ModifyExternalFuelSlipEntities.Qty + "','" + ModifyExternalFuelSlipEntities.Remarks + "','"  + Convert.ToInt32(Session["Tracker_userID"]) + "'");
            //Master();

            return Json(i);
        }
        BM.ModifyExternalFuelSlip.ModifyExternalFuelSlip LP = new BM.ModifyExternalFuelSlip.ModifyExternalFuelSlip();
        [UserAuthenticationFilter]
        // GET: ModifyExternalFuelSlip

        public ActionResult ModifyExternalFuelSlip()
        {
            return View();
        }

        public JsonResult AjaxGetSlipEditDetails(string SlipNo)
        {
            List<BE.ModifyExternalFuelSlipEntities> Containerdetails = new List<BE.ModifyExternalFuelSlipEntities>();
            Containerdetails = LP.GetSlipEditDetails(SlipNo);
            var jsonResult = Json(Containerdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult AjaxCancelFuelDetails(string SlipNo, string CancelRemarks)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.CancelFueldetails(SlipNo, CancelRemarks, Userid);
            return Json(message);
        }


        public ActionResult ISprintSave(string SlipNo)
        {
            string message = "";

            message = LP.ReprintFuelSlipdetails(SlipNo);
            return Json(message);
        }

       

    }
}