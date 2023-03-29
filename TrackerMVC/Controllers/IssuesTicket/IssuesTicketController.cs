using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CU = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using BE=TrackerMVCEntities.BusinessEntities;
using CD = TrackerMVCDataLayer.Helper;
using System.Data;
using Newtonsoft.Json;

namespace TrackerMVC.Controllers.IssuesTicket
{
    public class IssuesTicketController : Controller
    {
        CU.IssuesTicket objCompanyUserProvider = new CU.IssuesTicket();
        //TrackerMVCBusinessLayer.TrackerMVCBusinessManger.IssuesTicket issuesTicket = new TrackerMVCBusinessLayer.TrackerMVCBusinessManger.IssuesTicket();
        // GET: IssuesTicket
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IssuesTicket()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            ViewBag.Userid = Convert.ToInt32(Session["Tracker_userID"]);
            return View();
        }

        public JsonResult AddCompanyTicket(BE.CompanyTicketInfo ticket, HttpPostedFileBase file)
        {
            List<BE.CompanyTicketInfo> CompanyticketList = new List<BE.CompanyTicketInfo>();
            string message = "";
            if (ticket.Description != null)
            {
                int Userid = Convert.ToInt32(Session["Tracker_userID"]);
                var Description = ticket.Description;
                var Mailid = ticket.MailID;
                var Subject = ticket.Subject;
                var AddedDate = DateTime.Now.ToString("dd-MM-yyyy");
                string contentType = "";
                string fname="";

                //byte[] attachByte = null;
                //BinaryReader rdr = new BinaryReader(file.InputStream);
                //attachByte = rdr.ReadBytes((int)file.ContentLength);

                //var data = file.ContentLength;

                if (file != null)
                {
                    string filefullpath = Path.GetFullPath(file.FileName);
                    var fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                    fname = Path.Combine(Server.MapPath("~/ImportFile/"), file.FileName);
                    file.SaveAs(fname);
                    contentType = MimeMapping.GetMimeMapping(fname);
                }

                int CompanyID = Convert.ToInt16(Session["CompanyID"]);
                string CompanyName = Convert.ToString(Session["LoggedInCompanyName"]);

                CD.DBOperations db = new CD.DBOperations();
                int TicketNumber = 0;
                string strSql = "SELECT CASE WHEN MAX(TicketNumber) IS NULL THEN 1 ELSE MAX(TicketNumber)+1 END FROM trackerNCFS.dbo.companyticket";
                DataTable dt1 = db.sub_GetDatatable(strSql);
                if (dt1.Rows.Count > 0)
                {
                    TicketNumber = Convert.ToInt32(dt1.Rows[0][0].ToString());
                }


                CompanyticketList = objCompanyUserProvider.AddCompanyticket(ticket, fname, CompanyID, contentType, CompanyName, AddedDate, Userid, TicketNumber);

            }

            return Json(message);
        }

        public JsonResult Search(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_OPEN_TICKET'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult SearchClosed(string FromDate, string ToDate)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_Closed_TICKET '" + FromDate + "','" + ToDate + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult SaveClear(string ID, string search)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            //int retVal = 0;
            db.sub_ExecuteNonQuery("USP_CompanyTicketDeleteDetails '" + ID + "','" + search + "'");
            //Master();

            return Json(i);
        }
    }
}