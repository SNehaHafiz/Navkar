using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using Imp = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ImporteMaster;
using TrackerMVC.Filters;
namespace TrackerMVC.Controllers.ImporterController
{
    [UserAuthenticationFilter] 
    public class ImporterController : Controller
    {
        Imp.ImporterMaster impTrackerProvider = new Imp.ImporterMaster();
        // GET: ImporterMaster
        public ActionResult NewImporterProfile()
        {
            ViewBag.Message = TempData["MessageValue"]; 
            return View(new BE.ImporterMaster());
        }
        public JsonResult GetExisitingImporterCode(string impCode)
        {
            bool isCodeExisiting = false;
            if (impCode != "")
            {
                isCodeExisiting = impTrackerProvider.GetExisitingImporterCode(impCode);
            }

            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExisitingImporterName(string impName)
        {
            bool isCodeExisiting = false;
            if (impName != "")
            {
                isCodeExisiting = impTrackerProvider.GetExisitingImporterName(impName);
            }
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateAddress(string impAddress)
        {
            bool isCodeExisiting = false;
            if (impAddress != "")
            {
                isCodeExisiting = true;
            }
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetNewImporterID()
        {
            int id = impTrackerProvider.GetNewImporterID();
            return Json(id, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveImporterProfile(BE.ImporterMaster impDetails)
        {
            int saved = 0;
            string message = "";
            if ( impDetails != null && impDetails.ImporterName != "" && impDetails.ImporterCode != "" && impDetails.ImpAddress != "" && impDetails.ImporterName != null && impDetails.ImporterCode != null && impDetails.ImpAddress != null)
            {
                bool isImpName;
                bool isImpCode;
                isImpCode = impTrackerProvider.GetExisitingImporterCode(impDetails.ImporterCode);
                isImpName = impTrackerProvider.GetExisitingImporterName(impDetails.ImporterName);
                if (isImpCode && isImpName)
                {
                    impDetails.ImporterID = impTrackerProvider.GetNewImporterID();
                    impDetails.Addedby = Convert.ToInt32(Session["Tracker_userID"]);
                    saved = impTrackerProvider.AddImporterMasterDetails(impDetails);
                }                
                //return Json(saved, JsonRequestBehavior.AllowGet);

            }

            if (saved == 0)
            {
                message = "Data provided incomplete or already Exisiting. Try again!";
                TempData["MessageValue"] = message;
            }
            else if (saved == 1)
            {
                message = "Profile added sucessfully";
                TempData["MessageValue"] = message;
                ModelState.Clear();
                return RedirectToAction("NewImporterProfile");
            }

            ViewBag.Message = message; 
            return View("NewImporterProfile",new BE.ImporterMaster());
        }

        [HttpPost]
        public JsonResult GetNameForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = impTrackerProvider.GetNameForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetContactPersonForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = impTrackerProvider.GetContactPersonForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDesignationForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = impTrackerProvider.GetDesignationForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }
    }
}