using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using SL = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ShipLineMaster;
using TrackerMVC.Filters;

namespace TrackerMVC.Controllers.ShipLineController
{
    [UserAuthenticationFilter] 
    public class ShipLineController : Controller
    {
        SL.ShipLineMaster slTrackerProvider = new SL.ShipLineMaster();
        // GET: ShipLineMaster
        public ActionResult NewShipLineProfile()
        {
            ViewBag.Message = TempData["MessageValue"]; 
            return View();
        }
        public JsonResult GetExisitingShippingLineCode(string shippingLineCode)
        {
            bool isCodeExisiting = false;
            if (shippingLineCode != "")
            {
                isCodeExisiting = slTrackerProvider.GetExisitingShippingLineCode(shippingLineCode);
            }

            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExisitingShippingLineName(string shippingName)
        {
            bool isCodeExisiting = false;
            if (shippingName != "")
            {
                isCodeExisiting = slTrackerProvider.GetExisitingShippingLineName(shippingName);
            }
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateAddress(string shippingAddress)
        {
            bool isCodeExisiting = false;
            if (shippingAddress != "")
            {
                isCodeExisiting = true;
            }
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetNewShippingLineID()
        {
            int id = slTrackerProvider.GetNewShippingLineID();
            return Json(id, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveShippingLineProfile(BE.ShipLinesMaster shippingDetails)
        {
            int saved = 0;
            string message = "";
            if (ModelState.IsValid && shippingDetails != null && shippingDetails.SLName != "" && shippingDetails.SaID != "" && shippingDetails.SLAddressI != "" && shippingDetails.SLName != null && shippingDetails.SaID != null && shippingDetails.SLAddressI != null)
            {
                bool isSLName = slTrackerProvider.GetExisitingShippingLineName(shippingDetails.SLName);
                bool isSAID = slTrackerProvider.GetExisitingShippingLineCode(shippingDetails.SaID);
                if (isSLName && isSAID)
                {
                    shippingDetails.SLID = slTrackerProvider.GetNewShippingLineID();
                    shippingDetails.AddedBy = Convert.ToString(Session["Tracker_userID"]);
                    saved = slTrackerProvider.AddShipLineMasterDetails(shippingDetails);
                    //return Json(saved, JsonRequestBehavior.AllowGet);
                } 
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
                return RedirectToAction("NewShipLineProfile");
            }

            ViewBag.Message = message;

            return View("NewShipLineProfile");
        }
        [HttpPost]
        public JsonResult GetNameForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = slTrackerProvider.GetNameForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetContactPersonForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = slTrackerProvider.GetContactPersonForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDesignationForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = slTrackerProvider.GetDesignationForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }
    }
}