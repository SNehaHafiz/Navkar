using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using shipper = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ShipperMaster;
using TrackerMVC.Filters;

namespace TrackerMVC.Controllers.ShipperController
{
    [UserAuthenticationFilter] 
    public class ShipperController : Controller
    {
        shipper.ShipperMaster shipperTrackerProvider = new shipper.ShipperMaster();
        // GET: Shipper
        public ActionResult NewShipperProfile()
        {
            ViewBag.Message = TempData["MessageValue"]; 
            return View();
        }
        public JsonResult GetExisitingShipperCode(string shipperCode)
        {
            bool isCodeExisiting = false;
            if (shipperCode != "")
            {
                isCodeExisiting = shipperTrackerProvider.GetExisitingShipperCode(shipperCode);
            }

            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExisitingShipperName(string shipperName)
        {
            bool isCodeExisiting = false;
            if (shipperName != "")
            {
                isCodeExisiting = shipperTrackerProvider.GetExisitingShipperName(shipperName);
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
            int id = shipperTrackerProvider.GetNewShipperID();
            return Json(id, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveShipperProfile(BE.ShipperMaster shipperDetails)
        {
            int saved = 0;
            string message = "";
            if (ModelState.IsValid && shipperDetails != null && shipperDetails.ShipperIECNO != "" && shipperDetails.ShipperName != "" && shipperDetails.ShipperAddress != "" && shipperDetails.ShipperIECNO != null && shipperDetails.ShipperName != null && shipperDetails.ShipperAddress != null)
            {
                bool isShipperICENO;
                bool isShipperName;
                isShipperICENO = shipperTrackerProvider.GetExisitingShipperCode(shipperDetails.ShipperIECNO);
                isShipperName = shipperTrackerProvider.GetExisitingShipperName(shipperDetails.ShipperName);
                if (isShipperICENO && isShipperName)
                {
                    shipperDetails.ShipperID = shipperTrackerProvider.GetNewShipperID();
                    shipperDetails.AddedBY = Convert.ToString(Session["Tracker_userID"]);
                    saved = shipperTrackerProvider.AddShipperMasterDetails(shipperDetails);
                }
                
                //return Json(saved, JsonRequestBehavior.AllowGet);

            }

            if (saved == 0)
            {
                message = "Data provided incomplete or already Exisiting.Try again!";
                TempData["MessageValue"] = message;
            }
            else if (saved == 1)
            {
                message = "Profile added sucessfully";
                TempData["MessageValue"] = message;
                ModelState.Clear();
                return RedirectToAction("NewShipperProfile");
            }

            ViewBag.Message = message;

            return View("NewShipperProfile");
        }
        [HttpPost]
        public JsonResult GetNameForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = shipperTrackerProvider.GetNameForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetContactPersonForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = shipperTrackerProvider.GetContactPersonForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDesignationForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = shipperTrackerProvider.GetDesignationForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }
    }
}