using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using vessel = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.VesselMaster;
using TrackerMVC.Filters;

namespace TrackerMVC.Controllers.VesselController
{
    [UserAuthenticationFilter] 
    public class VesselSummaryController : Controller
    {
        vessel.VesselSummary vesselTrackerProvider = new vessel.VesselSummary();
        // GET: VesselSummary
        string message = "";
        public ActionResult GetVesselSummary()
        { 
            ViewBag.Message = TempData["MessageValue"]; 
            List<BE.Vessel> vesselList = new List<BE.Vessel>();
            vesselList = vesselTrackerProvider.GetVesselSummaryList();
            return View(vesselList);
        }

        public JsonResult EditVesselSummary(int vesselID)
        {
             BE.VesselMaster vesselList = new  BE.VesselMaster ();
             vesselList = vesselTrackerProvider.EditVesselSummary(vesselID);
             return Json(vesselList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateVesselDetails(int vesselID, string vesselName, bool isActive)
        {
            int saved = 0;

            if (vesselName != null && vesselName != "" && vesselName != null && vesselID != null && vesselID != 0 && vesselID != null && isActive != null &&  isActive != null)
            {

                saved = vesselTrackerProvider.UpdateVesselMasterDetails(vesselID, vesselName, isActive); 
            }
            var display = false;
            if (saved == 0)
            {

                message = "Data provided incomplete or already Exisiting. Try again!";
                TempData["MessageValue"] = message;
            }
            if (saved == 1)
            {
                display = true;
                List<BE.Vessel> vesselList = new List<BE.Vessel>();
                vesselList = vesselTrackerProvider.GetVesselSummaryList();
                message = "Vessel added sucessfully!";
                TempData["MessageValue"] = message;
            }

            return Json(display, JsonRequestBehavior.AllowGet);
        }
    }
}