using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using train = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrainMaster;
using TrackerMVC.Filters;
using System.Data;


namespace TrackerMVC.Controllers.TrainDeparture_From_PortController
{
    [UserAuthenticationFilter] 
    public class TrainDeparture_From_PortController : Controller
    {
        train.TrainDeparture_From_Port trainTrackerProvider = new train.TrainDeparture_From_Port();
        // GET: TrainDeparture_From_Port
        public ActionResult TrainDeparture_From_Port()
        {
            List<BE.Port> portList = new List<BE.Port>();
            portList = trainTrackerProvider.GetPortList();
            ViewBag.portList = portList;

            List<BE.ContainerDetails> containerList = new List<BE.ContainerDetails>();
            if (portList != null && portList.Count != 0)
            {
                trainTrackerProvider.DeleteFromTempTrain_Container_Selection(Convert.ToInt32(Session["Tracker_userID"]));
                containerList = trainTrackerProvider.GetContainerForTrainDeparture(portList[0].PortID,Convert.ToInt32(Session["Tracker_userID"]));
           
            }
            ViewBag.containerList = containerList;
            List<string> trainNO = new List<string>();
            trainNO = trainTrackerProvider.GetTrainNOForTrainDeparture();
            ViewBag.TrainNO = new SelectList(trainNO, "trainNO"); ;
            return View(containerList);
        }

        public JsonResult GetContainerForPortTrainDeparture(int portID)
        {
            List<BE.ContainerDetails> containerList = new List<BE.ContainerDetails>(); 
            containerList = trainTrackerProvider.GetContainerForTrainDeparture(portID,Convert.ToInt32(Session["Tracker_userID"]));
            ViewBag.containerList = containerList;
            return Json(containerList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateSelectedContainer(int joNo ,string containerNo, bool isChecked)
        {
            int totalCount = 0;
            totalCount = trainTrackerProvider.UpdateSelectedContainer(joNo, containerNo,isChecked,Convert.ToInt32(Session["Tracker_userID"])); 
            return Json(totalCount, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTotalCount()
        {
            int totalCount = 0;
            totalCount = trainTrackerProvider.GetTotalCount(Convert.ToInt32(Session["Tracker_userID"]));
            return Json(totalCount, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateWagonNoForSelectedContainer( string wagonNo, string containerNo)
        {
            int totalCount = 0;
            totalCount = trainTrackerProvider.UpdateWagonNoForSelectedContainer(wagonNo,containerNo,Convert.ToInt32(Session["Tracker_userID"]));
            return Json(totalCount, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateTrainNoForSelectedContainer(string trainNo)
        {
            int totalCount = 0;
            if (trainNo != " " && trainNo != "" && trainNo != null && trainNo != "undefined")
            {
                totalCount = trainTrackerProvider.UpdateTrainNoForSelectedContainer(trainNo, Convert.ToInt32(Session["Tracker_userID"]));
            }
           
           
            return Json(totalCount, JsonRequestBehavior.AllowGet);
        }

        /////////////by durga/////

        public JsonResult UpdateRemarksSelectedContainer(string Remarks, string containerNo, string jono)
        {
            int totalCount = 0;
            totalCount = trainTrackerProvider.UpdateRemarksSelectedContainer(Remarks, containerNo, jono, Convert.ToInt32(Session["Tracker_userID"]));
            return Json(totalCount, JsonRequestBehavior.AllowGet);
        }
        /////end by durga/////////
        /// Codes By Arti /

            public ActionResult TrainDepartureFromPort()
            {
            List<BE.Port> portList = new List<BE.Port>();
            portList = trainTrackerProvider.GetPortList();
            ViewBag.portList = new SelectList(portList,"PortID", "PortName"); ;

            List<BE.ContainerDetails> containerList = new List<BE.ContainerDetails>();
            if (portList != null && portList.Count != 0)
            {
            trainTrackerProvider.DeleteFromTempTrain_Container_Selection(Convert.ToInt32(Session["Tracker_userID"]));
            containerList = trainTrackerProvider.GetContainerForTrainDeparture(portList[0].PortID, Convert.ToInt32(Session["Tracker_userID"]));

            }
            ViewBag.containerList = containerList;

            List<BE.TrainMaster> TrainList = new List<BE.TrainMaster>();
            TrainList = trainTrackerProvider.GetTrainList();
            ViewBag.TrainList = new SelectList(TrainList, "TrainID", "TrainNO"); 




            return View(containerList);
            }

            [HttpPost]
            public JsonResult AjaxPortwiseContainerList(int Portid, int Trainid)
            {
            int PortID = Convert.ToInt32(Portid);
            int TrainID = Convert.ToInt32(Trainid);



            List<BE.ContainerDetails> containerList = new List<BE.ContainerDetails>();
            containerList = trainTrackerProvider.GetContainerList(PortID);
            ViewBag.containerList = containerList;



            //DBView.VendorDataProvider dashboardBussinesManager = new DBView.VendorDataProvider();

            //List<BO.Item> ItemsList = new List<BO.Item>();
            //ItemsList = dashboardBussinesManager.GetcategoryWiseItems(CategoryID);


            //List<BO.Unit> UnitList = new List<BO.Unit>();
            //UnitList = dashboardBussinesManager.getUnitlist();

            //List<BE.ContainerDetails> ExistingContainerlist = new List<BE.ContainerDetails>();
            //List<BE.ContainerDetails> TrainList = new List<BE.ContainerDetails>();

            //ExistingContainerlist = trainTrackerProvider.FetchExistingContainerList(PortID, TrainID);

            //object[] obj1 = ExistingContainerlist.ToArray();
            //object[] obj2;

            //foreach (string st in obj1)
            //{

            //}

            var returnField = new { ContainerList = containerList};


            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


            }

            [HttpPost]
            public ActionResult SaveContainerList(List<BE.ContainerDetails> Containerdata, int portid, int TrainID)
            {

                DataTable dataTable = new DataTable();


                dataTable.Columns.Add("PortID");
                dataTable.Columns.Add("TrainID");
                dataTable.Columns.Add("JONO");
                dataTable.Columns.Add("ContainerNo");
                dataTable.Columns.Add("Size");
                dataTable.Columns.Add("WagonNo");
                dataTable.Columns.Add("Remarks");

                dataTable.TableName = "PT_ContainerDetails";
                foreach (BE.ContainerDetails item in Containerdata)
                {
                    DataRow row = dataTable.NewRow();


                    row["PortID"] = item.PortID;
                    row["TrainID"] = item.TrainID;
                    row["JONO"] = item.JONO;
                    row["ContainerNo"] = item.ContainerNO;
                    row["Size"] = item.Size;
                    row["WagonNo"] = item.WagonNo;
                    row["Remarks"] = item.Remarks;


                    dataTable.Rows.Add(row);
                }
                int UserID = Convert.ToInt32(Session["Tracker_userID"]);

                BE.ContainerDetails ValidationList = new BE.ContainerDetails();
                ValidationList = trainTrackerProvider.SaveContainerList(dataTable, UserID);
                return new JsonResult() { Data = ValidationList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };



                //string message = trainTrackerProvider.SaveContainerList(dataTable, UserID);
                //return Json(message);

            }



            // Codes By Arti end/

        //code by durga for road planning//
            public ActionResult RoadPlanning()
            {
                List<BE.Port> portList = new List<BE.Port>();
                portList = trainTrackerProvider.GetPortList();
                ViewBag.portList = new SelectList(portList, "PortID", "PortName"); ;
                return View();
            }

            [HttpPost]
            public JsonResult AjaxPortwiseRoadContainerList(int Portid)
            {
                int PortID = Convert.ToInt32(Portid);
               // int TrainID = Convert.ToInt32(Trainid);

                List<BE.ContainerDetails> containerList = new List<BE.ContainerDetails>();
                containerList = trainTrackerProvider.GetRoadContainerList(PortID);
                ViewBag.containerList = containerList;


                var returnField = new { ContainerList = containerList };


                return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


            }
            public ActionResult SaveRoadContainerList(List<BE.ContainerDetails> Containerdata, int portid, string TrailerNo)
            {

                DataTable dataTable = new DataTable();


                dataTable.Columns.Add("PortID");
                dataTable.Columns.Add("TrailerNo");
                dataTable.Columns.Add("JONO");
                dataTable.Columns.Add("ContainerNo");
                dataTable.Columns.Add("Size");               
                dataTable.Columns.Add("Remarks");

                dataTable.TableName = "PT_RoadPlanning";
                foreach (BE.ContainerDetails item in Containerdata)
                {
                    DataRow row = dataTable.NewRow();


                    row["PortID"] = item.PortID;
                    row["TrailerNo"] = TrailerNo;
                    row["JONO"] = item.JONO;
                    row["ContainerNo"] = item.ContainerNO;
                    row["Size"] = item.Size;                   
                    row["Remarks"] = item.Remarks;

                    dataTable.Rows.Add(row);
                }
                int UserID = Convert.ToInt32(Session["Tracker_userID"]);
                BE.ContainerDetails ValidationList = new BE.ContainerDetails();
                ValidationList = trainTrackerProvider.SaveRoadContainerList(dataTable, UserID);
                return new JsonResult() { Data = ValidationList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


            }


            public ActionResult RoadPlanningSummary()
            {
                List<BE.ContainerDetails> TrainList = new List<BE.ContainerDetails>();
                TrainList = trainTrackerProvider.getTrailerRoadList();

                return View(TrainList);
            }


            public ActionResult RoadPlanningExport()
            {
                List<BE.Port> portList = new List<BE.Port>();
                portList = trainTrackerProvider.GetPortExpList();
                ViewBag.portList = new SelectList(portList, "PortID", "PortName"); ;
                return View();
            }

            public JsonResult PortwiseRoadContainerExportList(int Portid)
            {
                int PortID = Convert.ToInt32(Portid);
                // int TrainID = Convert.ToInt32(Trainid);

                List<BE.TrainExportEntities> containerList = new List<BE.TrainExportEntities>();
                containerList = trainTrackerProvider.GetRoadContainerExportList();
                ViewBag.containerList = containerList;


                var jsonResult = Json(containerList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }

            public ActionResult SaveRoadContainerExportList(List<BE.TrainExportEntities> Containerdata, int PortID, string TrailerNo)
            {
                string message = "";
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("EntryID");
                dt.Columns.Add("ContainerNo");
                dt.Columns.Add("process");
                dt.Columns.Add("TrailerNo");
                dt.Columns.Add("Remarks");
                dt.Columns.Add("Port");
               // dt.Columns.Add("Train");

                dt.TableName = "PT_ContainerPrePlanned";
                int Count = 1;
                foreach (BE.TrainExportEntities Item in Containerdata)
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = Count++;
                    row["EntryID"] = Item.entryid;
                    row["ContainerNo"] = Item.ContainerNo;
                    row["process"] = Item.process;
                    row["TrailerNo"] = Item.TrailerNo;
                    row["Remarks"] = Item.Remarks;
                    row["Port"] = Item.portId;
                  //  row["Train"] = Item.trainId;

                    dt.Rows.Add(row);
                }
                int UserID = Convert.ToInt32(Session["Tracker_userID"]);
                message = trainTrackerProvider.SaveExportRoadPlanned(dt, UserID, PortID, TrailerNo);

                return new JsonResult() { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


            }

        //end code by durga for road planning//
               

     // Codes By Prashant /

public ActionResult SMTPDateSummary()
{

List<BE.Port> portList = new List<BE.Port>();
portList = trainTrackerProvider.GetPortList();
ViewBag.portList = new SelectList(portList, "PortID", "PortName"); ;


return View();
}

[HttpPost]
public JsonResult AjaxPortWiseContainerDetails(int Portid)
{
int PortID = Convert.ToInt32(Portid);

List<BE.JobOrderMEntities> ContainerDataList = new List<BE.JobOrderMEntities>();
ContainerDataList = trainTrackerProvider.GetSMPTDetails(PortID);

return new JsonResult() { Data = ContainerDataList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


}
// Codes By Prashant /
        [HttpPost]
public ActionResult UpdateSMTPDate(List<BE.JobOrderMEntities> Containerdata, string SMTPRCVDDate)
{

    DataTable dataTable = new DataTable();

    dataTable.Columns.Add("JONO");
    dataTable.Columns.Add("ContainerNo");
    

    dataTable.TableName = "PT_RoadPlanning";
    foreach (BE.JobOrderMEntities item in Containerdata)
    {
        DataRow row = dataTable.NewRow();


       
        row["JONO"] = item.JONo;
        row["ContainerNo"] = item.ContainerNo;
       

        dataTable.Rows.Add(row);
    }
    int UserID = Convert.ToInt32(Session["Tracker_userID"]);
     
   // BE.JobOrderMEntities ValidationList = new BE.JobOrderMEntities();
    string Message = "";
    Message = trainTrackerProvider.UpdateSMTPContainerList(dataTable, UserID, SMTPRCVDDate);
    return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };



}

    }
}