using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Newtonsoft.Json;

namespace TrackerMVC.Controllers.Trailer
{
     [UserAuthenticationFilter] 
    public class PassingController : Controller
    {
        // GET: Passing
         BM.Trailer.Trailer TL = new BM.Trailer.Trailer();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Passing()
        {
            return View();
        }
         [HttpPost]
        public ActionResult UpdatePassing(string trailerNo,string NewtrailerNo,string Remarks,long trailerID)
        {
            string Message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            Message = TL.UpdatePassing(trailerNo, NewtrailerNo, Remarks, trailerID, Userid);
            return Json(Message, JsonRequestBehavior.AllowGet);
            
        }
    }
}