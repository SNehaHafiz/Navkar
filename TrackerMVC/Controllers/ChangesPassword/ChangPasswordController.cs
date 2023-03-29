using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ChangPassword;
using CD = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using DB = TrackerMVCDataLayer;

namespace TrackerMVC.Controllers.ChangesPassword
{
    public class ChangPasswordController : Controller
    {
        // GET: ChangPassword
        RP.ChangPassword reportprovider = new RP.ChangPassword();
        ///RP. reportprovider = new RP.Chequebounce();
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public ActionResult ChangPassword()
        {
            //List<BE.ChangPassword> ExpensesType = new List<BE.ChangPassword>();
            //ExpensesType = reportprovider.getExpenses();
            //ViewBag.ExpensesType = new SelectList(ExpensesType, "userID", "UserName");

            ViewBag.ExpensesType = Convert.ToString(Session["Tracker_userName"]);
            return View();
        }

        public JsonResult Save(BE.ChangPassword ChangPassword)
        {
           
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            db.sub_ExecuteNonQuery("USP_Changes_Password '" + ChangPassword.NewPassword + "','" + Convert.ToInt32(Session["Tracker_userID"]) +"'");
            // Master();
            return Json(ChangPassword);
        }
        public JsonResult AjaxCheckNumber(string oldtpassword)
        {
            string Message = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            Message = reportprovider.CheckOldPass(oldtpassword, UserID);

            return Json(Message);
        }

    }
}