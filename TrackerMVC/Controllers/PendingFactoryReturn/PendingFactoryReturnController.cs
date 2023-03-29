using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using TrackerMVC.Filters;

namespace TrackerMVC.Controllers.PendingFactoryReturn
{
    [UserAuthenticationFilter]
    public class PendingFactoryReturnController : Controller
    {
        BM.PendingFactoryReturn.PendingFactoryReturn PFR = new BM.PendingFactoryReturn.PendingFactoryReturn();
        // GET: PendingFactoryReturn
        public ActionResult PendingFactoryReturn()
        {
            return View();
        }
        public JsonResult getPendingFactoryReturnList()
        {            
            List<BE.PendingFactoryReturn> PendingList = new List<BE.PendingFactoryReturn>();
            PendingList = PFR.getPendingFactoryReturn();
            var jsonResult = Json(PendingList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}