using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.BL;
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
namespace TrackerMVC.Controllers.Chequebounce
{
    public class ChequebounceController : Controller
    {
        RP.Chequebounce reportprovider = new RP.Chequebounce();
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        //BM.BL.Chequebounce Bl = new BM.BL.Chequebounce();
        [UserAuthenticationFilter]
        // GET: Chequebounce

        public ActionResult Chequebounce()
        {
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = reportprovider.getParty();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }
   public JsonResult Save(BE.Chequebounce Chequebounce)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string Message = "";
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();

            dt = db.sub_GetDatatable("USP_INSERT_CHEQUE_BOUNCE_DETS '" + Convert.ToDateTime(Chequebounce.ENTRYDATE).ToString("yyyy-MM-dd HH:mm") + "','" + Chequebounce.CHEQUENO + "','" + Chequebounce.RECEIPTNO +"','"+ Chequebounce .ACTIVITY+ "','"+ Chequebounce.CHEQUE_AMOUNT+"','"+ Convert.ToDateTime(Chequebounce.CHEQUEDATE).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(Chequebounce.CHEQUE_RETURNDATE).ToString("yyyy-MM-dd HH:mm") + "','" + Chequebounce.BANKNAME + "','" + Chequebounce.CONTACTPERSONNAME + "','" + Chequebounce.CONTACTNO +  "','" + Chequebounce.REMARKS + "','" + UserID + "'");
            if (dt.Rows.Count > 0)
            {
                Message = dt.Rows[0][0].ToString();
            }

            return Json(Message);
        }

        public JsonResult Update(BE.Chequebounce Chequebounce)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            //int retVal = 0;
            db.sub_ExecuteNonQuery("usp_update_cheque_Bounce '" + Chequebounce.ENTRYID + "','" + Chequebounce.paymentMode + "','" + Chequebounce.ModeNo + "','" + Convert.ToDateTime(Chequebounce.ModeDate).ToString("yyyy-MM-dd HH:mm") + "','" + Chequebounce.ModeAmount + "','" + Chequebounce.REMARKS + "'");
            //Master();

            return Json(Chequebounce);
        }


        public ActionResult GetAssessInvoice(string CHEQUENO)
        {
            List<BE.Chequebounce> SearchInvoice = new List<BE.Chequebounce>();

            SearchInvoice = reportprovider.GetAssessNoInvoice(CHEQUENO);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetAssessReciept(string CHEQUENO)
        {
            List<BE.Chequebounce> SearchInvoice = new List<BE.Chequebounce>();

            SearchInvoice = reportprovider.GetAssessReciept(CHEQUENO);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult Search(string ason)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("Sp_ListOfChequeBounceSummary '" + ason + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult SearchClosed(string FromDate, string ToDate)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("Sp_Cheque_Bounce_Dets_Summary  '" + FromDate + "','" + ToDate + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult WithReason(string ModeNo)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";

            tblInvoiceList = db.sub_GetDatatable("usp_cheque_Modeno_Validtion '" + ModeNo +  "'");
            if (tblInvoiceList.Rows.Count > 0)
            {
                Message = tblInvoiceList.Rows[0]["message"].ToString();
            }


            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



    }
}