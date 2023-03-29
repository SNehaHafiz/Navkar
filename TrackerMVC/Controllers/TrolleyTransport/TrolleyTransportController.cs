using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrolleyTransport;
using CI = TrackerMVCEntities.BusinessEntities;
using TrackerMVC.Filters;
using System.Dynamic;
using HC = TrackerMVCDataLayer.Helper;
using System.Data;

namespace TrackerMVC.Controllers.TrolleyTransport
{
    [UserAuthenticationFilter] 
    public class TrolleyTransportController : Controller
    {
    

        [HttpPost]
        public JsonResult Save(CI.Trolleytransport Trolleytransport)
        {
            int i = 0;
            var EntryDate = Trolleytransport.EntryDate;
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            dt=db.sub_GetDatatable("USP_INSERT_TROLLEYM_Transport '" + Convert.ToDateTime(EntryDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Trolleytransport.TrolleyNumber + "','" + Trolleytransport.InstalledTyres + "','" + Trolleytransport.ID + "','" + Trolleytransport.Weight + "','" + Trolleytransport.ContainerSizeID + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");
            //Master();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    i = Convert.ToInt16(row["message"]);
                }
            }
            return Json(i);
        }
        DP.TrolleyTransport objTrailerProvider = new DP.TrolleyTransport();
        // GET: TrolleyTransport
        public ActionResult TrolleyTransport()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<CI.TransportTrolleyEntities> TransporterList = new List<CI.TransportTrolleyEntities>();
            List<CI.VehicleTypeIEntities> VehicleTypeList = new List<CI.VehicleTypeIEntities>();
            CI.Trolleytransport TransportTrolleyEntities = objTrailerProvider.getDropDownList();

            TransporterList = TransportTrolleyEntities.TransporterList;
            VehicleTypeList = TransportTrolleyEntities.VehicleTypeList;
            ViewBag.TransporterList = new SelectList(TransporterList, "ID", "trailerType");
            ViewBag.VehicleTypeList = new SelectList(VehicleTypeList, "ContainerSize", "ContainerSize");
            return View();
        }

        [HttpPost]
        public ActionResult TrolleySummary()
        {
            List<CI.Trolleytransport> TrolleyList = new List<CI.Trolleytransport>();
            TrolleyList = objTrailerProvider.getTrolleyList();
            return PartialView(TrolleyList);
            //return View(TrolleyList);
        }

       
    }
}