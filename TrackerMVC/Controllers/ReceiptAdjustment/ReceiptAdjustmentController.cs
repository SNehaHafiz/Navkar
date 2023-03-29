using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ReceiptAdjustment;
using CD = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace TrackerMVC.Controllers.ReceiptAdjustment
{
    [UserAuthenticationFilter]
    public class ReceiptAdjustmentController : Controller
    {
        // GET: ReceiptAdjustment
        BM.ReceiptAdjustment trainTrackerProvider = new BM.ReceiptAdjustment();
        public ActionResult ReceiptAdjustment()
        {

            List<BE.ReceiptAdjustments> ModeGroup = new List<BE.ReceiptAdjustments>();
            List<BE.ReceiptAdjustments> BankList = new List<BE.ReceiptAdjustments>();
            List<BE.ReceiptAdjustments> CategoryList = new List<BE.ReceiptAdjustments>();
            //List<BE.StuffingType> stuffingTypes = new List<BE.StuffingType>();
            //List<BE.Shippers> shippers = new List<BE.Shippers>();

            ModeGroup = trainTrackerProvider.getModeGroup();
            BankList = trainTrackerProvider.getBankGroup();
            CategoryList = trainTrackerProvider.Category();


            ViewBag.Mode = new SelectList(ModeGroup, "ModeID", "Mode");
            ViewBag.BankName = new SelectList(BankList, "BankID", "BankName");
            ViewBag.CategoryList = new SelectList(CategoryList, "ID", "Criteria");
            //ViewBag.shippers = new SelectList(shippers, "shipperID", "shippername");
            ViewBag.WorkYear = "2022-23";

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");


            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = trainTrackerProvider.getParty();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
            return View();
        }

        public ActionResult GetLocationDetails(string Criteria, string ReceiptNo, string WorkYear)
        {
            List<BE.ReceiptAdjustments> LocationDetails = new List<BE.ReceiptAdjustments>();
            LocationDetails = trainTrackerProvider.ReceiptFillDate(Criteria, ReceiptNo, WorkYear);
            var JsonResult = Json(LocationDetails, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public JsonResult GetSearch(string Criteria, string ReceiptNo, string WorkYear)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_RCPT_Mode_Dets_Grid '" + Criteria + "','" + ReceiptNo + "','" + WorkYear + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ExternalmovementSave(string ReceiptNo, string WorkYear, string ReceiptDate,string Payfromid, List<BE.ReceiptAdjustments> SelectedData)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            //DataTable dt = new DataTable();
            try
            {
                if (SelectedData.Count > 0)
                {
                    foreach (BE.ReceiptAdjustments movementEntities in SelectedData)
                    {
                        i = db.sub_ExecuteNonQuery("USP_RECEIPT_ADJUSTMENT_INSERT '" + ReceiptNo + "','" + WorkYear + "','"+ ReceiptDate + "','"+ Payfromid + "','" + movementEntities.Mode + "','"  + movementEntities .Amount+ "','" + movementEntities.ModeNo + "','" + movementEntities.BankName + "','" + Convert.ToDateTime(movementEntities.ModeDate).ToString("yyyy-MM-dd HH:mm") + "',"+ UserID + "");
                        if (i == 0)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                i = 0;
            }

            //Master();

            return Json(i);
        }
        [HttpPost]
        public ActionResult ReceiptMovementmovementSave(string ReceiptNo, string WorkYear, string ReceiptDate, string Payfromid, string Category, List<BE.ReceiptAdjustments> SelectedData)
        {


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ModeNo");
            dataTable.Columns.Add("Mode");
            dataTable.Columns.Add("ModeDate");
            dataTable.Columns.Add("BankName");
            dataTable.Columns.Add("Amount");
            
            dataTable.TableName = "ReceiptMovementSave";
            foreach(BE.ReceiptAdjustments item in SelectedData)
            {
                DataRow row = dataTable.NewRow();

                row["ModeNo"] = item.ModeNo;
                row["Mode"] = item.Mode;
                row["ModeDate"] = Convert.ToDateTime(item.ModeDate).ToString("yyyy-MM-dd HH:mm") ;
                row["BankName"] = item.BankName;
                row["Amount"] = item.Amount;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = trainTrackerProvider.SaveReceiptChangesDetails(ReceiptNo, WorkYear, ReceiptDate, Payfromid, Userid, Category, dataTable);
            return Json(message);

        }
        //{
        //    int UserID = Convert.ToInt16(Session["Tracker_userID"]);
        //    string message = "";


        //    DataTable dataTable = new DataTable();


        //    dataTable.Columns.Add("Mode");
        //    dataTable.Columns.Add("AmountReciept");
        //    dataTable.Columns.Add("ModeNo");
        //    dataTable.Columns.Add("BankName");
        //    dataTable.Columns.Add("ModeDate");




        //    foreach (BE.ReceiptAdjustments item in SelectedData)
        //    {
        //        DataRow row = dataTable.NewRow();

        //        row["Mode"] = item.Mode;
        //        row["AmountReciept"] = item.Amount;
        //        row["ModeNo"] = item.ModeNo;
        //        row["BankName"] = item.BankName;
        //        row["ModeDate"] = item.ModeDate;

        //        dataTable.Rows.Add(row);

        //        //message = ValidateDuplicate(WOType, item.ContainerNo, item.VendorID, item.StuffPkgs, item.VehicleNo, item.Weight);

        //    }

        //    if (message == "")
        //    {
        //        message = trainTrackerProvider.SaveExternal( UserID,ReceiptNo, WorkYear, dataTable);
        //    }

        //    return Json(message);
        //}

        public JsonResult ReceiptSalesReceipt(string ReceiptNo, string WorkYear, string Category)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            string message = "";
            //DataTable dt = new DataTable();
             
                  
                        i = db.sub_ExecuteNonQuery("USP_RECEIPT_ADJUSTMENT_Update_Category '" + ReceiptNo + "','" + WorkYear + "','" + Category + "','" + UserID + "'");

                if (i > 0)
                {

                    message = "Record saved successfully";

                }
                else
                {
                    message = "Record not saved please try again!";

                }

            return Json(message);
        }
        public JsonResult ReceiptCustmerChange(string ReceiptNo, string WorkYear, string Customer)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            string message = "";
            //DataTable dt = new DataTable();


            i = db.sub_ExecuteNonQuery("USP_RECEIPT_ADJUSTMENT_Update_Customer '" + ReceiptNo + "','" + WorkYear + "','" + Customer + "','" + UserID + "'");

            if (i > 0)
            {

                message = "Record saved successfully";

            }
            else
            {
                message = "Record not saved please try again!";

            }

            return Json(message);
        }
        public JsonResult ReceiptSalesCategory(string ReceiptNo, string WorkYear, string Category)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            string message = "";
            //DataTable dt = new DataTable();


            i = db.sub_ExecuteNonQuery("USP_RECEIPT_ADJUSTMENT_Update_Category_Invoice '" + ReceiptNo + "','" + WorkYear + "','" + Category + "','" + UserID + "'");

            if (i > 0)
            {

                message = "Record saved successfully";

            }
            else
            {
                message = "Record not saved please try again!";

            }

            return Json(message);
        }

        public JsonResult ReceiptCancel_Credit(string ReceiptNo, string WorkYear, string Remarks)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            string message = "";
            //DataTable dt = new DataTable();


            i = db.sub_ExecuteNonQuery("usp_Cancel_Credit_Receiptmaster '" + ReceiptNo + "','" + WorkYear + "','" + Remarks + "','" + UserID + "'");

            if (i > 0)
            {

                message = "Record Cancel  successfully";

            }
            else
            {
                message = "Record not Cancel  please try again!";

            }

            return Json(message);
        }
        public JsonResult ReceiptCancelDebit(string ReceiptNo, string WorkYear, string Remarks)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            string message = "";
            //DataTable dt = new DataTable();


            i = db.sub_ExecuteNonQuery("usp_Cancel_Debit_Receiptmaster '" + ReceiptNo + "','" + WorkYear + "','" + Remarks + "','" + UserID + "'");

            if (i > 0)
            {

                message = "Record Cancel  successfully";

            }
            else
            {
                message = "Record not Cancel  please try again!";

            }

            return Json(message);
        }

        public JsonResult ReceiptCancel(string ReceiptNo, string modeno, string modename, string modeamount,string Receiptyear,string EntryId)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            string message = "";
            //DataTable dt = new DataTable();
            DataTable PortsDS = new DataTable();

            //i = db.sub_ExecuteNonQuery("USP_Receipt_Adjustment_Cancel '" + ReceiptNo + "','" + modeno + "','" + modename + "','" + modeamount + "','"+ UserID + "'");
            PortsDS = db.sub_GetDatatable("USP_Receipt_Adjustment_Cancel '" + ReceiptNo + "','" + modeno + "','" + modename + "','" + modeamount + "','" + Receiptyear + "','" + UserID + "','" + EntryId + "'");
            if (PortsDS.Rows.Count > 0)
            {
                message = "" + PortsDS.Rows[0][0] + "";
            }
            else
            {
                message = "Record Cancel  successfully";
            }

             

            return Json(message);
        }
    }
}