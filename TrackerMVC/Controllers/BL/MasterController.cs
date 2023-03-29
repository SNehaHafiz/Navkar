using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using System.Data;
using DB = TrackerMVCDataLayer;
using CD = TrackerMVCDataLayer.Helper;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;

namespace TrackerMVC.Controllers.BL
{
    [UserAuthenticationFilter] 
    public class MasterController : Controller
    {
        BM.BLDataManager.GSTSummary GS = new BM.BLDataManager.GSTSummary();
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MasterSummary()
        {
            
            return View();
        }
        public JsonResult GlobalSearch(String SearchText,String Master)
        {
            List<BE.GSTEntities> GSTList = new List<BE.GSTEntities>();
            GSTList = GS.getShippers(SearchText, Master);
            return Json(GSTList);
        }

        public ActionResult GlobalSearchSummary()
        {
           ViewBag.Message= TempData["MessageValue"];
            return View();
        }

        public JsonResult GetGlobalGSTList(String SearchText)
        {
             
            List<BE.GSTEntities> GSTList = new List<BE.GSTEntities>();
            GSTList = GS.getGlobalGSTList(SearchText);
            return Json(GSTList);
           // return View();
        }

        [HttpPost]
        public ActionResult EditCustomerDetails(Int64 ID)
        {
            BE.MasterEntities CustomerData = new BE.MasterEntities();
            CustomerData = GS.GetCutomerData(ID);
            return PartialView(CustomerData);
           // return Json(CustomerData);
        }

        [HttpPost]
        public ActionResult UpdateMasterData(BE.MasterEntities CustomerData)
        {
            string Message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            Message = GS.UpdateMasterData(CustomerData, userId);
            ViewBag.Message = Message;
            TempData["MessageValue"] = Message;
            return RedirectToAction("GlobalSearchSummary");
           // return RedirectToAction("GlobalSearchSummary", new { SearchText = CustomerData.SearchText });
            //return RedirectResult("");
           // return View("GlobalSearchSummary"); ;
        }

        [HttpPost]
        public ActionResult AddCustomerDetails()
        {
            return PartialView("EditCustomerDetails");
        }

        [HttpPost]
        public JsonResult CheckExisitMasterCode(string Code)
        {
            bool isCodeExisiting = false;
            int res=1;
            if (Code != "")
            {
                //isCodeExisiting = GS.GetExisitingCode(Code);
                res = GS.GetExisitingCode(Code);
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckExisitMasterName(string Name, string ID)
        {
            bool isNameExisiting = false;
            Int64 masterID;
            if (ID == "")
            {
                 masterID = 0;
            }
            else {
                 masterID = Convert.ToInt64(ID);
            
            }
            
            int res = 1;
            if (Name != "")
            {
              //  isNameExisiting = GS.GetExisitingName(Name);
                res = GS.GetExisitingName(Name, masterID);
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        // Codes by Arti

        [HttpPost]
        public ActionResult LocationDetails(Int64 ID, string Name)
        {
            // BE.MasterEntities CustomerData = new BE.MasterEntities();
            //CustomerData = GS.GetCutomerData(ID);

            List<BE.Ext_location_Master> LocationList = new List<BE.Ext_location_Master>();
            LocationList = GS.GetLocationList();
            ViewBag.LocationList = new SelectList(LocationList, "LocationID", "Location");

            List<BE.GST_Registration_Type> RegTypeList = new List<BE.GST_Registration_Type>();
            RegTypeList = GS.GetGSTRegistrationType();
            ViewBag.RegTypeList = new SelectList(RegTypeList, "RGID", "RGType");

           
            List<BE.Ext_location_Master> CustomerLocationList = new List<BE.Ext_location_Master>();
            CustomerLocationList = GS.GetCustomerLocationList(ID);
            ViewBag.CustomerLocationList = CustomerLocationList;

            ViewBag.CommonID = ID;
            ViewBag.Name = Name;
           
            //if (Convert.ToString(TempData["MessageValue"]) != null)
            //{
            //    //string msg = TempData["url"].ToString();
            //    string msg1 = Convert.ToString(TempData["MessageValue"]);
            //    ViewBag.Message = msg1;
            //}
            return PartialView();

        }


        [HttpPost]
        public ActionResult SaveLocationDetails(BE.LocationMaster LocationDetails)
        {
            string Message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            Message = GS.AddLocationDetails(LocationDetails, userId);

            //Int64 id = LocationDetails.Common_ID;
            //ViewBag.Message = Message;

            //List<BE.Ext_location_Master> LocationList = new List<BE.Ext_location_Master>();
            //LocationList = GS.GetLocationList();
            //ViewBag.LocationList = new SelectList(LocationList, "LocationID", "Location");

            //List<BE.GST_Registration_Type> RegTypeList = new List<BE.GST_Registration_Type>();
            //RegTypeList = GS.GetGSTRegistrationType();
            //ViewBag.RegTypeList = new SelectList(RegTypeList, "RGID", "RGType");

            //List<BE.Ext_location_Master> CustomerLocationList = new List<BE.Ext_location_Master>();
            //CustomerLocationList = GS.GetCustomerLocationList(id);
            //ViewBag.CustomerLocationList = CustomerLocationList;

            //ViewBag.CommonID = LocationDetails.Common_ID;
            //ViewBag.Name = LocationDetails.GSTName;

            //return PartialView("LocationDetails");
            //TempData["MessageValue"] = Message;
            //return RedirectToAction("LocationDetails", new { ID = LocationDetails.Common_ID, Name=LocationDetails.GSTName });

            return Json(Message, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult getStateCode(string GSTNO)
        {
            List<BE.StateMaster> stateList = new List<BE.StateMaster>();
            stateList = GS.getStateCode(GSTNO);



            //ViewBag.Message = Message;
            //TempData["MessageValue"] = Message;
            //return RedirectToAction("GlobalSearchSummary");

            return Json(stateList, JsonRequestBehavior.AllowGet);

        }
        
        [HttpPost]
        public JsonResult GetLocationWiseData(Int32 id, Int64 Common_ID, Int64 GSTID)
        {
            BE.LocationMaster lm = new BE.LocationMaster();
            lm = GS.getLocationDataCustomerWise(id, Common_ID, GSTID);

            return Json(lm, JsonRequestBehavior.AllowGet); 
        
        }
        //Codes by Rahul
        [HttpPost]
        public ActionResult DeliveryAddresses(Int64 ID, string Name)
        {
            List<BE.DeliveryAddresses> LocationList = new List<BE.DeliveryAddresses>();
            LocationList = GS.GetDeliveryAddresses();
            ViewBag.LocationList = new SelectList(LocationList, "LocationID", "Location");

            List<BE.DeliveryAddresses> CustomerLocationList = new List<BE.DeliveryAddresses>();
            CustomerLocationList = GS.GetPreviousDeliveryAddresses(ID);
            ViewBag.CustomerLocationList = CustomerLocationList;

            ViewBag.CommonID = ID;
            ViewBag.Name = Name;

            return PartialView();
        }
        [HttpPost]
        public JsonResult GetDeliveryAddresswiseData(Int32 id, Int64 Common_ID, Int64 GSTID)
        {
            BE.DeliveryAddresses lm = new BE.DeliveryAddresses();
            lm = GS.getLocationWiseDeliveryAddress(id, Common_ID, GSTID);

            return Json(lm, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult SaveDeliveryAddresses(BE.DeliveryAddresses AddressDetails)
        {
            string Message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            Message = GS.AddDeliveryAddresses(AddressDetails, userId);        
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ValidationforDuplicate(BE.DeliveryAddresses AddressDetails1)
        {
            BE.DeliveryAddresses lm = new BE.DeliveryAddresses();
            lm = GS.getDuplicateValidation(AddressDetails1);
            return Json(lm, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getpincode(string State_ID)        {
            CD.DBOperations db = new CD.DBOperations();            DataTable dataTable = new DataTable();            List<BE.PinCode> receiptEntries = new List<BE.PinCode>();            dataTable = db.sub_GetDatatable("usp_getpincode '" + State_ID + "'");            if (dataTable != null)            {                foreach (DataRow row in dataTable.Rows)                {                    BE.PinCode receiptEntry = new BE.PinCode();                    receiptEntry.Id = Convert.ToString(row["Pin_Code"]);                    receiptEntry.PINCODE = Convert.ToString(row["Pin_Code"]);
                    receiptEntries.Add(receiptEntry);                }            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;        }

        public JsonResult WithReason(string State, string PinCode)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";

            tblInvoiceList = db.sub_GetDatatable("USP_Validate_PIN_Code '" + State + "','" + PinCode + "'");
            if (tblInvoiceList.Rows.Count > 0)
            {
                Message = tblInvoiceList.Rows[0]["message"].ToString();
            }


            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        //CODE END BY RAHUL
    }
}