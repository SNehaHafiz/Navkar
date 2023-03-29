using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using CHA = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.CHA;
using TrackerMVC.Filters;

namespace TrackerMVC.Controllers.CHAController
{
    [UserAuthenticationFilter] 
    public class CHAController : Controller
    {
        string message = "";
        // GET: CHAMaster
        CHA.CHAMaster chaTrackerProvider = new CHA.CHAMaster();
        public ActionResult NewCHAProfile()
        {
            ViewBag.Message = TempData["MessageValue"]; 
            return View();
        }
        public JsonResult GetExisitingCHACode(string chaCode)
        {
            bool isCodeExisiting = false;
            if (chaCode != "")
            {
                isCodeExisiting =chaTrackerProvider.GetExisitingCHACode(chaCode); 
            }
             
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExisitingCHAName(string chaName)
        {
            bool isCodeExisiting = false;
            if (chaName != "")
            {
                isCodeExisiting = chaTrackerProvider.GetExisitingCHAName(chaName);
            }              
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateAddress(string chaAddress)
        {
            bool isCodeExisiting = false;
            if (chaAddress != "")
            {
                isCodeExisiting = true;
            }
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetNewCHAID()
        {
            int  id = chaTrackerProvider.GetNewCHAID(); 
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult SaveCHAProfile(BE.CHAMaster chaDetails)
        {
            int saved = 0; 
            if (ModelState.IsValid && chaDetails != null && chaDetails.CHACode != "" && chaDetails.CHAName != "" && chaDetails.Address != "" && chaDetails.CHACode != null && chaDetails.CHAName != null && chaDetails.Address != null)
            {
                bool chaCodeExisiting;
                bool chaNameExisting;
                chaCodeExisiting = chaTrackerProvider.GetExisitingCHACode(chaDetails.CHACode);
                chaNameExisting = chaTrackerProvider.GetExisitingCHAName(chaDetails.CHAName);
                if (chaCodeExisiting && chaNameExisting)
                {
                    chaDetails.CHAID = chaTrackerProvider.GetNewCHAID();
                    chaDetails.Addedby = Convert.ToString(Session["Tracker_userID"]);
                    saved = chaTrackerProvider.AddCHAMasterDetails(chaDetails);
                }
                
                //return Json(saved, JsonRequestBehavior.AllowGet);
                
            }
           
            if (saved == 0)
            {
                message = "Data provided incomplete or already Exisiting. Try again!";
                TempData["MessageValue"] = message;
            }else if(saved == 1 ){
                message = "Profile added sucessfully";
                TempData["MessageValue"] = message;
                ModelState.Clear();
                return RedirectToAction("NewCHAProfile"); 
            }

            ViewBag.Message = message;
             

             return View("NewCHAProfile");

           
        }
        //public JsonResult ClearForm()
        //{
        //    int clear= 0;
        //    if (ModelState.IsValid)
        //    {
        //        ModelState.Clear();
        //    }
        //    else
        //    {
        //        ModelState.Clear();
        //    }
            
        //    return Json(clear, JsonRequestBehavior.AllowGet);
        //}      

        [HttpPost]
        public JsonResult GetNameForAutoComplete(string name)
        {
           List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = chaTrackerProvider.GetNameForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet); 
        }
        
        [HttpPost]
        public JsonResult GetContactPersonForAutoComplete(string name)
        {
           List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = chaTrackerProvider.GetContactPersonForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet); 
        } 
        [HttpPost]
        public JsonResult GetDesignationForAutoComplete(string name)
        {
           List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = chaTrackerProvider.GetDesignationForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet); 
        }

    }
    
}