using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using train = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrainMaster;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using HC = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;

namespace TrackerMVC.Controllers.TrainController
{
    [UserAuthenticationFilter]
    public class TrainController : Controller
    {
        train.TrainMaster trainTrackerProvider = new train.TrainMaster();
        train.TrainDeparture_From_Port trainTrackerDepartureProvider = new train.TrainDeparture_From_Port();
        // GET: Train
        public ActionResult NewTrainDetails()
        {
            //ViewBag.Message = TempData["MessageValue"];
            string message = "";
             message=Convert.ToString(TempData["MessageValue"]);
            ViewBag.Message = message;
            List<BE.OperatorMaster> operatorDetails = new List<BE.OperatorMaster>();
            operatorDetails = trainTrackerProvider.GetOperatorListForTrainMaster();
            ViewBag.OperatorList = new SelectList(operatorDetails, "OperatorID", "OperatorName");
            List<BE.Port> portList = new List<BE.Port>();
            portList = trainTrackerProvider.GetPortList();
            ViewBag.portList = new SelectList(portList, "PortID", "PortName");
            return View();
        }
        public JsonResult GetExisitingTrainNO(string trainNo)
        {
            bool isCodeExisiting = false;
            if (trainNo != "")
            {
                isCodeExisiting = trainTrackerProvider.GetExisitingTrainNO(trainNo);
            }
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveTrainDetails(BE.TrainMaster trainDetails)
        {
            int saved = 0;
            string message = "";
         //   if (ModelState.IsValid && trainDetails != null && trainDetails.TrainNO != "" && trainDetails.TrainNO != null)
            if ( trainDetails.TrainNO != "" && trainDetails.TrainNO != null)
          
            {
                bool isTrainNo;

                isTrainNo = trainTrackerProvider.GetExisitingTrainNO(trainDetails.TrainNO);
                if (isTrainNo)
                {
                    trainDetails.TrainID = trainTrackerProvider.GetNewTrainID();
                    trainDetails.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                    //trainDetails.PortFrom = document.getElementById("portFrom").value;

                    saved = trainTrackerProvider.AddTrainMasterDetails(trainDetails);
                }

                //return Json(saved, JsonRequestBehavior.AllowGet);

            }
            List<BE.OperatorMaster> operatorDetails = new List<BE.OperatorMaster>();
            operatorDetails = trainTrackerProvider.GetOperatorListForTrainMaster();
            ViewBag.OperatorList = new SelectList(operatorDetails, "OperatorID", "OperatorName");
            List<BE.Port> portList = new List<BE.Port>();
            portList = trainTrackerProvider.GetPortList();
            ViewBag.portList = new SelectList(portList, "PortID", "PortName");
            if (saved == 0)
            {
                message = "Data provided incomplete or already Exisiting. Try again!";
                TempData["MessageValue"] = message;
                ViewBag.Message = message;
            }
            else if (saved == 1)
            {
                message = "Train added sucessfully";
                TempData["MessageValue"] = message;
                ViewBag.Message = message;
                ModelState.Clear();
              //  return RedirectToAction("NewTrainDetails");
            }


            return RedirectToAction("NewTrainDetails");
           // return View("NewTrainDetails");
        }
        [HttpPost]
        public JsonResult GetNoForAutoComplete(string no)
        {
            List<string> nameList = new List<string>();
            if (no != "")
            {
                nameList = trainTrackerProvider.GetNoForAutoComplete(no);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }
        public string SaveNewOperator(string operatorName)
        {
            int saved = 0;
            string message = "";
            if (ModelState.IsValid && operatorName != null && operatorName != "" && operatorName != null)
            {
                bool isTrainNo;

                isTrainNo = trainTrackerProvider.GetExisitingOperatorName(operatorName);
                if (isTrainNo)
                {
                    BE.OperatorMaster newOperatorName = new BE.OperatorMaster();
                    newOperatorName.OperatorID = trainTrackerProvider.GetNewOperatorID();
                    newOperatorName.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                    newOperatorName.OperatorName = Convert.ToString(operatorName);
                    saved = trainTrackerProvider.AddOperatorMasterDetails(newOperatorName);
                }

                //return Json(saved, JsonRequestBehavior.AllowGet);

            }
            List<BE.OperatorMaster> operatorDetails = new List<BE.OperatorMaster>();
            operatorDetails = trainTrackerProvider.GetOperatorListForTrainMaster();
            ViewBag.OperatorList = new SelectList(operatorDetails, "OperatorID", "OperatorName");
            List<BE.Port> portList = new List<BE.Port>();
            portList = trainTrackerProvider.GetPortList();
            ViewBag.portList = new SelectList(portList, "PortID", "PortName");

            if (saved == 0)
            {
                message = "Data provided incomplete or already Exisiting. Try again!";
            }
            else if (saved == 1)
            {
                message = "Profile add sucessfully";
                ModelState.Clear();
                return message;
            }

            ViewBag.Message = message;

            return message;
        }
        public JsonResult GetExisitingOperatorName(string operatorName)
        {
            bool isCodeExisiting = false;
            if (operatorName != "")
            {
                isCodeExisiting = trainTrackerProvider.GetExisitingOperatorName(operatorName);
            }
            return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetOperatorNameForAutoComplete(string no)
        {
            List<string> nameList = new List<string>();
            if (no != "")
            {
                nameList = trainTrackerProvider.GetOperatorNameForAutoComplete(no);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTrainSummary(string process)
        {
            List<BE.TrainMaster> trainList = new List<BE.TrainMaster>();
            trainList = trainTrackerProvider.GetTrainSummary(process);
            return Json(trainList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditTrainSummary(int trainID)
        {
            BE.TrainMaster trainList = new BE.TrainMaster();
            trainList = trainTrackerProvider.EditTrainSummary(trainID);
            return Json(trainList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateTrainMasterDetails(int trainID, string trainNO, string portFrom, string portTo, int operatorID, string operatorName)
        {
            int saved = 0;
            string message = "";
            if (trainNO != null && trainNO != "" && trainNO != null)
            {

                BE.TrainMaster trainDetails = new BE.TrainMaster();
                trainDetails.TrainID = trainID;
                trainDetails.TrainNO = trainNO;
                trainDetails.PortFrom = portFrom;
                trainDetails.PortTO = portTo;
                trainDetails.OperatorID = operatorID;
                trainDetails.Operator = operatorName;
                trainDetails.ModifiedBy = Convert.ToInt32(Session["Tracker_userID"]);
                saved = trainTrackerProvider.UpdateTrainMasterDetails(trainDetails);

            }


            List<BE.OperatorMaster> operatorDetails = new List<BE.OperatorMaster>();
            operatorDetails = trainTrackerProvider.GetOperatorListForTrainMaster();
            ViewBag.OperatorList = new SelectList(operatorDetails, "OperatorID", "OperatorName");
            List<BE.Port> portList = new List<BE.Port>();
            portList = trainTrackerProvider.GetPortList();
            ViewBag.portList = new SelectList(portList, "PortID", "PortName");
            var display = false;

            if (saved != 0)
            {
                display = true;

            }


            return Json(display, JsonRequestBehavior.AllowGet);
        }


        //////////Added By durga 19 Apr ////////////
        public ActionResult TrainDeparture()
        {
            List<BE.TrainMaster> TrainList = new List<BE.TrainMaster>();
            TrainList = trainTrackerProvider.getTrainList();

            return View(TrainList);
        }

        [HttpPost]
        public JsonResult TrainModifyDeparture(List<BE.TrainMaster> TrainList)
        {
            string message = "";

            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            //  message = BL.AddJobOrderData(viewModel, containerData, userId);
            // ViewBag.Message = message;
            DataTable DT = new DataTable();

            DT.Columns.Add("TrainNo");
            DT.Columns.Add("DepartureDate");
            DT.Columns.Add("userID");
            DT.Columns.Add("RemovalDate");
            DT.Columns.Add("PlacementDate");
            DT.Columns.Add("PortLine");

            DT.TableName = "PT_DepartureDate";
            foreach (BE.TrainMaster item in TrainList)
            {
                DataRow row = DT.NewRow();
                row["TrainNo"] = item.TrainNO;
                row["DepartureDate"] = Convert.ToDateTime(item.DepartureDate).ToString("yyyy-MM-dd HH:mm:ss");
                row["userID"] = userId;
                row["RemovalDate"] = Convert.ToDateTime(item.RemovalDate).ToString("yyyy-MM-dd HH:mm:ss");
                row["PlacementDate"] = Convert.ToDateTime(item.PlacementDate).ToString("yyyy-MM-dd HH:mm:ss");
                row["PortLine"] = item.PortLineNo;

                DT.Rows.Add(row);
            }

            message = trainTrackerProvider.UpdateTrainDeparture(DT, userId);

            return Json(message);
        }

        public ActionResult TrainPlanningExport()
        {
            BM.BLDataManager.GenerateBL BL = new BM.BLDataManager.GenerateBL();
            List<BE.ShipLines> ShipLines = new List<BE.ShipLines>();
            ShipLines = BL.getShipLines();
            ViewBag.ShipLines = new SelectList(ShipLines, "SLID", "SLName");
            return View();
        }

        public JsonResult getLineWiseData(string ShiplineID)
        {
            List<BE.TrainExportEntities> ContainerList = new List<BE.TrainExportEntities>();
            ContainerList = trainTrackerProvider.getLineWiseContainer(ShiplineID);

            var jsonResult = Json(ContainerList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

         //   return new JsonResult() { Data = TrainList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public ActionResult AddPrePlanContainerData(List<BE.TrainExportEntities> te)
        {
            string message="";
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("EntryID");
            dt.Columns.Add("ContainerNo");
              dt.TableName = "PT_ContainerPrePlanned";
              int Count = 1;
            foreach (BE.TrainExportEntities Item in te)
            {
                DataRow row = dt.NewRow();
                row["ID"] = Count++;
                row["EntryID"] = Item.entryid;
                row["ContainerNo"] = Item.ContainerNo;
                dt.Rows.Add(row);
            }
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = trainTrackerProvider.SaveContainerPrePlannerList(dt, UserID);

            return new JsonResult() { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public ActionResult ExportTrainPlanning()
        {
            List<BE.Port> portList = new List<BE.Port>();
            portList = trainTrackerProvider.GetPortList();
            ViewBag.portList = new SelectList(portList, "PortID", "PortName"); ;

            List<BE.TrainMaster> TrainList = new List<BE.TrainMaster>();
            TrainList = trainTrackerProvider.GetTrainListforExport();
            ViewBag.TrainList = new SelectList(TrainList, "TrainID", "TrainNO");

            //List<BE.TrainExportEntities> TrainExpList = new List<BE.TrainExportEntities>();
            //TrainExpList = trainTrackerProvider.GetTrainExpList();

            return View();
        }

        public ActionResult TrainExportContainerList(string Portid, string Trainid)
        {
            List<BE.TrainExportEntities> TrainExpList = new List<BE.TrainExportEntities>();
            TrainExpList = trainTrackerProvider.GetTrainExpList();
            return Json(TrainExpList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddExportTrainPlanning(List<BE.TrainExportEntities> Containerdata, int PortID, int TrainID)

        {  string message="";
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("EntryID");
            dt.Columns.Add("ContainerNo");
            dt.Columns.Add("process");
            dt.Columns.Add("wagonNo");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("Port");
            dt.Columns.Add("Train");

              dt.TableName = "PT_ContainerPrePlanned";
              int Count = 1;
              foreach (BE.TrainExportEntities Item in Containerdata)
            {
                DataRow row = dt.NewRow();
                row["ID"] = Count++;
                row["EntryID"] = Item.entryid;
                row["ContainerNo"] = Item.ContainerNo;
                row["process"] = Item.process;
                row["wagonNo"] = Item.wagonNo;
                row["Remarks"] = Item.Remarks;
                row["Port"] = Item.portId;
                row["Train"] = Item.trainId;

                dt.Rows.Add(row);
            }
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = trainTrackerProvider.SaveExportTrainPlanned(dt, UserID, PortID, TrainID);

            return new JsonResult() { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        
        }
        //////////End here  ////////////
        [HttpPost]
        public ActionResult TrainwiseContainerDetails(String TrainNo)
        {

            List<BE.ContainerDetails> ContainerList = new List<BE.ContainerDetails>();

            ContainerList = trainTrackerProvider.GetContainerListforTrainNo(TrainNo);
            return PartialView(ContainerList.ToList());

        }

        //by aarti//////
        public ActionResult TrainStausSheet()
        {
            List<BE.TrainMaster> TrainList = new List<BE.TrainMaster>();
            TrainList = trainTrackerProvider.GetTrainStatusSheetList();

            return View(TrainList);
        }


        [HttpPost]
        public JsonResult AjaxTrainCompletedList()
        {

            List<BE.TrainMaster> TrainList = new List<BE.TrainMaster>();
            TrainList = trainTrackerProvider.GetTrainStatusSheetCompletedList();


            return new JsonResult() { Data = TrainList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
        //end

        public ActionResult TrainmasterEntry()
        {
            List<BE.OperatorMaster> operatorDetails = new List<BE.OperatorMaster>();
            List<BE.Shippers> customerDetails = new List<BE.Shippers>();
            operatorDetails = trainTrackerProvider.GetOperatorListForTrainMaster();
            ViewBag.OperatorList = new SelectList(operatorDetails, "OperatorID", "OperatorName");

            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = trainTrackerProvider.getLocation();

            customerDetails = trainTrackerProvider.getShipper();

            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            ViewBag.PartyList = new SelectList(customerDetails, "shipperID", "shippername");

            List<BE.OwnerStatus> Ownerstatus = new List<BE.OwnerStatus>();
            Ownerstatus = trainTrackerProvider.getOwnerStatus();

            ViewBag.OwnerStatus = new SelectList(Ownerstatus, "OwnerID", "OwnerName");

            return View();
        }

        [HttpPost]
        public ActionResult SaveTrainMasterEntry(string TrainNo, string NoCno, string OwnerStatus, string RankOperator, string FromLocation, string Tolocation, string OriginDepartureDate, string ETA
            , string ETD, string ATA, string ATD, string LoadingStartDate, string LoadingCompleteDate, string Unloadingstartdate, string unloadingcompletedate,
            string LIneNoalloated, string Trainstatus, string StatusUpdatedDate, string Remarks,string PartyId)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = trainTrackerProvider.SaveTrainMaster(TrainNo, NoCno, OwnerStatus, RankOperator, FromLocation, Tolocation, OriginDepartureDate, ETA, ETD, ATA
                , ATD, LoadingStartDate, LoadingCompleteDate, Unloadingstartdate, unloadingcompletedate, LIneNoalloated, Trainstatus, StatusUpdatedDate, Remarks, UserID,PartyId);
            return Json(message);
        }
        
        [HttpPost]
        public JsonResult getAutoNOCList(string prefixText, string CustomerType)
        {
            List<BE.NOCUpdation> nameList = new List<BE.NOCUpdation>();
            if (prefixText != "")
            {
                nameList = trainTrackerProvider.GetNoNOCForAutoComplete(prefixText);
            }

            return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BCNTrainSummary(string FromDate, string ToDate)
        {
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("GetTrainSummary '" + FromDate + "','" + ToDate + "'");
            Session["BCNTrainSummaryDet"] = getAuctionGenList;
            Session["CFromDate"] = FromDate;
            Session["CToDate"] = ToDate;
            var json = "";

            if (getAuctionGenList.Rows.Count > 0)
            {
                json = JsonConvert.SerializeObject(getAuctionGenList);
            }
            else { json = "0"; }

            var returnField = new { DocumentList = json };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult BCNTrainSummaryExport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string FromDate = Session["CFromDate"].ToString();
            string ToDate = Session["CToDate"].ToString();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["BCNTrainSummaryDet"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BCNTrainSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> BCN Train Summary Details<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> FromDate :- " + FromDate + " To Date:- " + todate + "<strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }
    }
}