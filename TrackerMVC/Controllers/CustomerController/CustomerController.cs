using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using Cust = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.CustomerMaster;
using TrackerMVC.Filters;
namespace TrackerMVC.Controllers.CustomerController
{
    [UserAuthenticationFilter] 
    public class CustomerController : Controller
    {
        // GET: Customer
        Cust.CustomerMaster custTrackerProvider = new Cust.CustomerMaster();
        public ActionResult NewCustomerProfile(BE.CustomerMaster custDetails)
        {
            int saved = 0;
            string message = "";
            if (ModelState.IsValid && custDetails != null && custDetails.AGaID != "" && custDetails.AGName != "" && custDetails.AGAddress != "" && custDetails.AGName != null && custDetails.AGaID != null && custDetails.AGAddress != null)
            {
                bool isAGAIDExist;
                bool isAGNameExist;
                isAGAIDExist = custTrackerProvider.GetExisitingCustomerCode(custDetails.AGaID);
                isAGNameExist = custTrackerProvider.GetExisitingCustomerName(custDetails.AGName);
                if (isAGAIDExist && isAGNameExist)
                {
                    custDetails.AGID = custTrackerProvider.GetNewCustomerID();
                    custDetails.Addedby = Convert.ToInt32(Session["Tracker_userID"]);
                    saved = custTrackerProvider.AddCustomerMasterDetails(custDetails);
                }

                //return Json(saved, JsonRequestBehavior.AllowGet);

            }

            //if (saved == 0)
            //{
            //    message = "Data provided incomplete or already Exisiting. Try again!";
            //}
            //else 
                if (saved == 1)
            {
                message = "Profile add sucessfully";
                ModelState.Clear();
                //return RedirectToAction("NewCustomerProfile");
            }

            ViewBag.Message = message;

            //return View("NewCustomerProfile");
            return View();
        }
        public JsonResult GetExisitingCustomerCode(string custCode)
        {
            bool isCodeExisiting = false;
            if (custCode != "")
            {
                isCodeExisiting = custTrackerProvider.GetExisitingCustomerCode(custCode);
            }

            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExisitingCustomerName(string custName)
        {
            bool isCodeExisiting = false;
            if (custName != "")
            {
                isCodeExisiting = custTrackerProvider.GetExisitingCustomerName(custName);
            }
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateAddress(string custAddress)
        {
            bool isCodeExisiting = false;
            if (custAddress != "")
            {
                isCodeExisiting = true;
            }
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetNewCHAID()
        {
            int id = custTrackerProvider.GetNewCustomerID();
            return Json(id, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveCustomerProfile(BE.CustomerMaster custDetails)
        {
            int saved = 0;
            string message = "";
            if (ModelState.IsValid && custDetails != null && custDetails.AGaID != "" && custDetails.AGName != "" && custDetails.AGAddress != "" && custDetails.AGName != null && custDetails.AGaID != null && custDetails.AGAddress != null)
            {
                bool isAGAIDExist;
                bool isAGNameExist;
                isAGAIDExist = custTrackerProvider.GetExisitingCustomerCode(custDetails.AGaID);
                isAGNameExist = custTrackerProvider.GetExisitingCustomerName(custDetails.AGName);
                if (isAGAIDExist && isAGNameExist)
                {
                    custDetails.AGID = custTrackerProvider.GetNewCustomerID();
                    custDetails.Addedby = Convert.ToInt32(Session["Tracker_userID"]);
                    saved = custTrackerProvider.AddCustomerMasterDetails(custDetails);
                }
                
                //return Json(saved, JsonRequestBehavior.AllowGet);

            }

            if (saved == 0)
            {
                message = "Data provided incomplete or already Exisiting. Try again!";
            }
            else if (saved == 1)
            {
                message = "Profile add sucessfully";
                ModelState.Clear();
               // return RedirectToAction("NewCustomerProfile");
            }

            ViewBag.Message = message;

            return View("NewCustomerProfile");


        }
        [HttpPost]
        public JsonResult GetNameForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = custTrackerProvider.GetNameForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetContactPersonForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = custTrackerProvider.GetContactPersonForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDesignationForAutoComplete(string name)
        {
            List<string> nameList = new List<string>();
            if (name != "")
            {
                nameList = custTrackerProvider.GetDesignationForAutoComplete(name);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }
    }
}