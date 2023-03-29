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
    public class ManualPortOutController : Controller
    {
        // GET: ManualPortOut
        public ActionResult Index()
        {
            return View();
        }
        vessel.ManualPortOut ManualPortoutProvider = new vessel.ManualPortOut();
        public ActionResult ManualPortOutDetails()
        {
            List<BE.Ports> portList = new List<BE.Ports>();
            portList = ManualPortoutProvider.getPorts();
            ViewBag.portList = new SelectList(portList, "PortID", "PortName");

            List<BE.ContainerSize> ContainerSize = new List<BE.ContainerSize>();
            ContainerSize = ManualPortoutProvider.getContainerSize();
           
            ViewBag.ContainerSize = new SelectList(ContainerSize, "ContainerSizeID", "ContainerSizeName");


            List<BE.ContainerType> ContainerType = new List<BE.ContainerType>();
            ContainerType = ManualPortoutProvider.getContainerType();
            ViewBag.ContainerType = new SelectList(ContainerType, "ContainerTypeID", "ContainerTypeName");

            List<BE.TrailersEntities> Trailerno = new List<BE.TrailersEntities>();
            Trailerno = ManualPortoutProvider.getTrailers();
            ViewBag.Trailerno = new SelectList(Trailerno, "trailerid", "trailername");


            List<BE.Location> Location = new List<BE.Location>();
            Location = ManualPortoutProvider.getLocation();
            ViewBag.LocationSource = new SelectList(Location, "LocationID", "LocationName");
            ViewBag.Location = new SelectList(Location, "LocationID", "LocationName","4");



            List<BE.Transport> Transport = new List<BE.Transport>();
            Transport = ManualPortoutProvider.getTransport();
            ViewBag.Transport = new SelectList(Transport, "Transport_Type_ID", "Transport_Type");
       

            List<BE.Cycle> Cycle = new List<BE.Cycle>();
            Cycle = ManualPortoutProvider.getCycle();
            ViewBag.Cycle = new SelectList(Cycle, "Activity_ID", "Activity_Name");


            List<BE.Train> Train = new List<BE.Train>();
            Train = ManualPortoutProvider.getTrain();
            ViewBag.Train = new SelectList(Train, "TrainId", "TrainNo");

            return View();
        }






        public ActionResult AjaxAddManualPortOut(string TrailerNo, string ContainerNo, int ContainerSize, int ContainerType, int Portname, int Cycle, int TransportType, int FromSource, int FromDestination, int TrainNo, string WagonNo, string SMTPReceived)
        {
            string message;
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = ManualPortoutProvider.AddManualportOut(TrailerNo, ContainerNo, ContainerSize, ContainerType, Portname, Cycle, TransportType, FromSource, FromDestination, TrainNo, WagonNo, SMTPReceived, userId);
         
           message = "Record submitted successfully!";
           
            return Json(message);
        }

        
        [HttpPost]
        public JsonResult AjaxGetTrailerNo(string TrailerNo)
        {
            

            List<BE.TrailersEntities> Trailerno = new List<BE.TrailersEntities>();
            Trailerno = ManualPortoutProvider.getTrailerNo(TrailerNo);
            return new JsonResult() { Data = Trailerno, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
        [HttpPost]
        public JsonResult AjaxGetTrailerNo_For_Fuel_Con(string TrailerNo)
        {


            List<BE.TrailersEntities> Trailerno = new List<BE.TrailersEntities>();
            Trailerno = ManualPortoutProvider.getTrailerNoFor_Fuel_Con(TrailerNo);
            return new JsonResult() { Data = Trailerno, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        [HttpPost]
        public JsonResult GetTEUSCalculation(string TrainNo)
        {
            string message = "";

            List<BE.ContainerDetails> Validation = new List<BE.ContainerDetails>();
            Validation = ManualPortoutProvider.GetTEUSCalculation(TrainNo);

            if (Validation.Count > 0)
            {

                message = Validation[0].Message;
            }
            else
            {
                message = "New";
            }
            return new JsonResult() { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        [HttpPost]
        public JsonResult CheckDuplicateContainer(string ContainerNo)
        {
            string message = "";

            List<BE.ContainerDetails> Validation = new List<BE.ContainerDetails>();
            Validation = ManualPortoutProvider.CheckDuplicateContainer(ContainerNo);

            if (Validation.Count > 0)
            {

                message = "Following container has been already planned. Please re-check." + ContainerNo;
            }
            else
            {
                message = "New";
            }
            return new JsonResult() { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
    }
}