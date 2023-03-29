using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using HC = TrackerMVCDataLayer.Helper;
using BE = TrackerMVCEntities.BusinessEntities;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using LR = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.LRClosing;


namespace TrackerMVC.Controllers.LRClosing
{
    [UserAuthenticationFilter]
    public class LRClosingController : Controller
    {
        // GET: LRClosing
      
        LR.LRClosing trackerProvider = new LR.LRClosing();
        HC.DBOperations db = new HC.DBOperations();

        public ActionResult LRClosing()
        {
            //BE.LRListOpen DocList = trackerProvider.GetDocList(0);
            //ViewBag.DocList = new SelectList(DocList.DocList, "DocID", "DocName");
            //ViewBag.ContainerNo = DocList.ContainerNo;
            //ViewBag.FromLocation = DocList.FromLocation;
            //ViewBag.ToLocation = DocList.ToLocation;
            //ViewBag.VehicleNo = DocList.VehicleNo;
            return View();
        }
        public ActionResult GetLRListDataBind(string fromDate, string ToDate,string Searchcerteria,string SearchNumber)
        {
            List<BE.LRListOpen> LRListOpen = new List<BE.LRListOpen>();
            LRListOpen = trackerProvider.GetLRList(fromDate, ToDate, Searchcerteria, SearchNumber);
            //DataTable dt = new DataTable();
            //HC.DBOperations db = new HC.DBOperations();
            //dt = db.sub_GetDatatable("USP_LIST_LR_OPEN");
            
            //Session["LROpenList"] = dt;
            //string json = JsonConvert.SerializeObject(LRListOpen);
            var jsonResult = Json(LRListOpen, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        [HttpPost]
        public ActionResult CloseLRWithData(int LRNo)
        {
            //List<BE.DocList> DocLRList = new List<BE.DocList>();
            //List<BE.DocList> DocList = new List<BE.DocList>();
            BE.LRListOpen DocList = new BE.LRListOpen();
            DocList = trackerProvider.GetDocList(LRNo);
            //DocLRList = DocList.DocList;
            ViewBag.DocList = new SelectList(DocList.DocList, "DocID", "DocName");
            ViewBag.ContainerNo = DocList.ContainerNo;
            ViewBag.FromLocation = DocList.FromLocation;
            ViewBag.ToLocation = DocList.ToLocation;
            ViewBag.VehicleNo = DocList.VehicleNo;
            ViewBag.ReportingDate = DocList.FactoryReportingTime;
            ViewBag.ReportingInDate = DocList.FactoryInTime;
            ViewBag.ReportingOutdate = DocList.FactoryOutTime;
            ViewBag.LRDate = DocList.LRDate;
            ViewBag.LorryNo = LRNo;

            // Code by prashant
            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = trackerProvider.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            //Code End by Prashant
            return PartialView(DocList);
        }
        public ActionResult GetScannedList(int LRNo)
        {
            DataTable dtscList = new DataTable();
            dtscList = db.sub_GetDatatable("USP_SHOW_SCANNED_LIST_LR_WISE " + LRNo + "");
            string json = JsonConvert.SerializeObject(dtscList);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public JsonResult ImportFile(BE.LRListOpen fileData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        string LRNO = fileData.LRNoFile;                        
                        int DocID = fileData.DocID;

                        string root = Path.Combine(Server.MapPath("~/LRDocs/"),LRNO);
                        string PathSave = "~/LRDocs/" + LRNO + "/" + fname;
                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        fname = Path.Combine(Server.MapPath("~/LRDocs/" + LRNO + "/"), fname);
                        //fname = Path.Combine(root, "/" + fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(fname);
                        if (System.IO.File.Exists(fname))
                        {
                            db.sub_ExecuteNonQuery("USP_INSERT_INTO_LR_SCANNEDDOCS " + LRNO + "," + DocID + ",'" + PathSave + "'," + Userid + "");
                            return Json("Document uploaded successfully!");
                        }
                        else
                        {
                            return Json("Document not saved successfully!");
                        }
                    }
                    return Json(1);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult SaveClosing(int LRNo,DateTime FactoryRepDate,DateTime FactoryInDate,DateTime FactoryOutDate,string CloseRemarks,string InFromLocation)
        {
            DataTable dt = new DataTable();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt=db.sub_GetDatatable("USP_UPDATE_CLOSING_DETAILS_LRNO_WISE " + LRNo + ",'" + Convert.ToDateTime(FactoryRepDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(FactoryInDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(FactoryOutDate).ToString("yyyy-MM-dd HH:mm") + "','" + CloseRemarks.Replace("'","''") + "'," + Userid + ",'"+ InFromLocation + "'");
            return Json(Convert.ToString(dt.Rows[0][0]));
        }

        public ActionResult GetLocationID(string ToLocation)
        {
            DataTable dt = new DataTable();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_GetLocationID '" + ToLocation + "'");
            return Json(Convert.ToString(dt.Rows[0][0]));
        }

        public ActionResult GenerateLorryReceipt()
        {
            List<BE.ActivityMaster> ActivityList = new List<BE.ActivityMaster>();
            ActivityList = trackerProvider.GetActivity();
            ViewBag.ActivityMaster = new SelectList(ActivityList, "AutoID", "Activity");


            List<BE.ShipLines> ShippingLine = new List<BE.ShipLines>();
            ShippingLine = trackerProvider.getShipLines();
            ViewBag.ShippingLineList = new SelectList(ShippingLine, "SLID", "SLName");

            List<BE.MovementCountEntities> MovCount = new List<BE.MovementCountEntities>();
            MovCount = trackerProvider.getMovCount();
            ViewBag.MovCount = new SelectList(MovCount, "MovCountID", "MovCount");

            List<BE.TransporterEntities> Transpoter = new List<BE.TransporterEntities>();
            Transpoter = trackerProvider.getTranspoter();
            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");

            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = trackerProvider.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");

            List<BE.MovementTypeEntities> MovType = new List<BE.MovementTypeEntities>();
            MovType = trackerProvider.getMovType();
            ViewBag.MovType = new SelectList(MovType, "Mov_ID", "Mov_Type");

            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = trackerProvider.GetCustomer();
            ViewBag.CustomerList = new SelectList(Customer, "AGID", "AGName");
            return View();
        }



        public ActionResult AjaxGetEntryid(string containerno,string Activity)
        {
            string message = "";

            message = trackerProvider.GetENtryid(containerno, Activity);
            
            return Json(message);

        }

        //Code by suraj
        public ActionResult DomesticHubBookingLr()
        {
            return View();
        }

        public JsonResult GetContainer(string Containerno)
        {
            List<BE.DomesticHubLrEntities> ContainerList = new List<BE.DomesticHubLrEntities>();
            ContainerList = trackerProvider.LRContainerSearch(Containerno);
            var jsonResult = Json(ContainerList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult UpdateJOBMDetails(BE.DomesticHubLrEntities Master)
        {
            BE.Response response = new BE.Response();
            try
            {
                Master.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now;
                response = trackerProvider.UpdateImportJoDet(Master);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }

        public JsonResult Search(string search)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("Usp_Domestic_lr_search '" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}