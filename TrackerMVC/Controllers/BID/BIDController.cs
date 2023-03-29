using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.BID;
using CD = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace TrackerMVC.Controllers.BID
{
    [UserAuthenticationFilter]
    public class BIDController : Controller
    {
        // GET: BID
        //BM.BID.BID BL = new BM.BID.BID();
        public ActionResult BID()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();


        }
        public JsonResult IgmSearch(string IGMNo,string temNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_IGM_BID_FILL '" + IGMNo + "','" + temNo + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //suraj
        BM.BID reportprovider = new BM.BID();
        public ActionResult GetAssessInvoice(string IGMNo, string temNo)
        {
            List<BE.BIDEntities> SearchInvoice = new List<BE.BIDEntities>();

            SearchInvoice = reportprovider.GetAssessNoInvoice(IGMNo, temNo);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetJonoChange(string jono )
        {
            List<BE.BIDEntities> SearchInvoice = new List<BE.BIDEntities>();

            SearchInvoice = reportprovider.GetJonoChanges(jono);

            var jsonResult = Json(SearchInvoice, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult Save(BE.BIDEntities BIDEntities)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            //int retVal = 0;
            db.sub_ExecuteNonQuery("USP_INSERT_BID_HISTORY_ENTRY '" + BIDEntities.IGMNo + "','" + BIDEntities.ItemNo +  "','" + Convert.ToDateTime(BIDEntities.IGMDate).ToString("yyyy-MM-dd HH:mm") + "','" + BIDEntities.LotNo + "','" + BIDEntities.Descriptions + "','" + BIDEntities.FileNo + "','" + BIDEntities.ContainerNo + "','" + Convert.ToDateTime(BIDEntities.Datewhich).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(BIDEntities.ValuationDate).ToString("yyyy-MM-dd HH:mm") + "','" + BIDEntities.Quantity  + "','" + BIDEntities .Weight+ "','" + BIDEntities .ValueI+ "','" + BIDEntities.ValueII + "','" + BIDEntities.ReservePrice + "','" + BIDEntities.NumberOfAuction + "','" + Convert.ToDateTime(BIDEntities.AuctionDate).ToString("yyyy-MM-dd HH:mm") + "','" +BIDEntities.BidAmount + "','" +BIDEntities.BidPercentage+ "','" +UserID +  "','" + BIDEntities.ValueAscertainedI +"','" + BIDEntities .ValueAscertainedII+ "','" + BIDEntities.ReserveAscertained+ "'");
            //Master();

            return Json(BIDEntities);
        }

        public JsonResult BIDSearch(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_BID_Summary '" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult BIDPrint(string IGMNo, string ItemNo, string AuctionId)
        {

            DataSet getJobOrderSet = new DataSet();
            DataTable tblDocumentDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("USP_BID_Summary_Print '" + IGMNo + "','" + ItemNo + "'");
            tblDocumentDetails = getJobOrderSet.Tables[0];
            tblContainerDetails = getJobOrderSet.Tables[1];

            if (tblDocumentDetails != null)
            {
                ViewBag.LotNo = tblDocumentDetails.Rows[0]["LotNo"].ToString();
                ViewBag.FileNo = tblDocumentDetails.Rows[0]["FileNo"].ToString();
                ViewBag.IGMItem = tblDocumentDetails.Rows[0]["IgmItem"].ToString();
                ViewBag.ContainerNo = tblDocumentDetails.Rows[0]["ContainerNo"].ToString();
                ViewBag.ContainerDetails = tblDocumentDetails.Rows[0]["ContainerDetails"].ToString();
                ViewBag.DateOfArrival = tblDocumentDetails.Rows[0]["ArrivalDate"].ToString();
                ViewBag.DateNOC = tblDocumentDetails.Rows[0]["Date_Which"].ToString();
                ViewBag.Description = tblDocumentDetails.Rows[0]["Descriptions"].ToString();
                ViewBag.Quantity = tblDocumentDetails.Rows[0]["Quantity"].ToString();
                ViewBag.Weight = tblDocumentDetails.Rows[0]["Weight"].ToString();
                ViewBag.ValueAscertainedI = tblDocumentDetails.Rows[0]["ValueAscertainedI"].ToString();
                ViewBag.ValueI = tblDocumentDetails.Rows[0]["ValueI"].ToString();
                ViewBag.ValueAscertainedII = tblDocumentDetails.Rows[0]["ValueAscertainedII"].ToString();
                ViewBag.ValueIII = tblDocumentDetails.Rows[0]["ValueII"].ToString();
                ViewBag.ReservePriceAscertained = tblDocumentDetails.Rows[0]["Reserve_Price"].ToString();
                ViewBag.ReservePrice = tblDocumentDetails.Rows[0]["Number_of_Auctions"].ToString();
            }

            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();

            return PartialView();
        }

        public ActionResult ClearParty()
        {
         
            //List<BE.PartyNameEntities> Partyname = new List<BE.PartyNameEntities>();

            //Partyname = reportprovider.GetGstPartyName();
       
            //ViewBag.Partyname = new SelectList(Partyname, "Common_ID", "GSTName");
           
            return View();


        }

        public JsonResult getPartyNameReceipt(string prefixText, string Mode)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GetPartyNameReceipt '" + Mode + "','" + prefixText + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.Customer customer = new BE.Customer();
                    customer.AGID = Convert.ToInt32(row["GSTID"]);
                    customer.AGName = Convert.ToString(row["GSTName"]);
                    Customerlst.Add(customer);
                }
            }

            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult PartySeach(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_GetPartyNameFIll '" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult SaveClear(string hdnPayFromId, string hdnReplaceId, string ddlReplacerName, string ddlCustomerName)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            //int retVal = 0;
            db.sub_ExecuteNonQuery("USP_Clean_DUPLIcate_Master_Data '" + hdnPayFromId + "','" + hdnReplaceId + "','" + ddlReplacerName + "','" + ddlCustomerName + "','" + UserID + "'");
            //Master();

            return Json(i);
        }


    }
}