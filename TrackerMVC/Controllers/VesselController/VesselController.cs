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
    
    public class VesselController : Controller
    {
        int messageStatus = 0;
        string message = "";
        // GET: Vessel
        vessel.VesselMaster vesselTrackerProvider = new vessel.VesselMaster();
        
        public ActionResult NewVesselDetails()
        {
            ViewBag.Message = TempData["MessageValue"]; 
            return View();
        }

        public JsonResult GetExisitingVesselName(string vesselName)
        {
            bool isCodeExisiting = false;
            if (vesselName != "")
            {
                isCodeExisiting = vesselTrackerProvider.GetExisitingVesselName(vesselName);
            }
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult SaveVesselDetails(BE.VesselMaster vesselDetails)
        {
            int saved = 0;
           
            if (ModelState.IsValid && vesselDetails != null && vesselDetails.VesselName != "" && vesselDetails.VesselName != null )
            {                 
                bool isVesselName;

                isVesselName = vesselTrackerProvider.GetExisitingVesselName(vesselDetails.VesselName);
                if (isVesselName)
                {
                    vesselDetails.VesselID = vesselTrackerProvider.GetNewVesselID();
                    vesselDetails.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                    saved = vesselTrackerProvider.AddVesselMasterDetails(vesselDetails);
                }

                //return Json(saved, JsonRequestBehavior.AllowGet);

            }

            if (saved == 0)
            {
                message = "Data provided incomplete or already Exisiting. Try again!";
            }
            else if (saved == 1)
            {
                message = "Vessel added sucessfully";
                TempData["MessageValue"] = message;
                ViewBag.Message = message;
                ModelState.Clear(); 
                return RedirectToAction("NewVesselDetails");
            }

            ViewBag.Message = message;

            return View("NewVesselDetails");
        }
        [HttpPost]
        public JsonResult GetNameForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = vesselTrackerProvider.GetNameForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        } 
        
    }
}