using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CU = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ModifyBL;

using CD = TrackerMVCDataLayer.Helper;
using System.Data;
using Newtonsoft.Json;


namespace TrackerMVCz.Controllers.LashingAndChocking
{

    public class LashingAndChockingController : Controller
    {
        // GET: LashingAndChocking
        BM.LashingAndChocking.LashingAndChockingDataProvider LCBusinessManager = new BM.LashingAndChocking.LashingAndChockingDataProvider();
        RP.ModifyBL BL = new RP.ModifyBL();


        public ActionResult MaterialsReceivingConfirmations()
        {
            List<BE.LashingAndChockingInfo> MaterialGroupList = new List<BE.LashingAndChockingInfo>();
            MaterialGroupList = LCBusinessManager.GetMaterialGroup();
            ViewBag.MaterialGroupList = new SelectList(MaterialGroupList, "MaterialGroupID", "MaterialGroupName");

            return View();
        }


        public JsonResult GetMaterialsReceivingConfirmationsList()
        {

            List<BE.LashingAndChockingInfo> Invoicelist = new List<BE.LashingAndChockingInfo>();

            Invoicelist = LCBusinessManager.GetMaterialsReceivingConfirmationsList();
            var jsonResult = Json(Invoicelist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            // return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }


        public ActionResult UpdateMaterialGroup(string AutoID, int MaterialGroupID)
        {

            string message = LCBusinessManager.UpdateMaterialGroup(AutoID, MaterialGroupID);
            return Json(message);
        }
        // TEMP COMMENT
        //public JsonResult GetMappedMaterialsReceivingConfirmationsList(string ItemName, string AllAutoID)
        //{

        //    List<BE.LashingAndChockingInfo> Invoicelist = new List<BE.LashingAndChockingInfo>();

        //    Invoicelist = LCBusinessManager.GetMappedMaterialsReceivingConfirmationsList(ItemName, AllAutoID);
        //    var jsonResult = Json(Invoicelist, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;

        //    // return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        //}



        public JsonResult GetMappedMaterialsReceivingConfirmationsList(string ItemName, string AllAutoID, List<BE.LashingAndChockingInfo> Material)        {

            List<BE.LashingAndChockingInfo> Invoicelist = new List<BE.LashingAndChockingInfo>();            DataTable dataTable = new DataTable();            dataTable.Columns.Add("MaterialQty");            dataTable.Columns.Add("MaterialMappingID");            dataTable.TableName = "PT_MaterialDetailsLAndC";            foreach (BE.LashingAndChockingInfo element in Material)            {                DataRow row = dataTable.NewRow();                row["MaterialQty"] = element.MaterialQty;                row["MaterialMappingID"] = element.MaterialMappingID;                dataTable.Rows.Add(row);            }            Invoicelist = LCBusinessManager.GetMappedMaterialsReceivingConfirmationsList(ItemName, AllAutoID, dataTable);            var jsonResult = Json(Invoicelist, JsonRequestBehavior.AllowGet);            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;

            // return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public ActionResult SSRInventory()
        {

            return View();
        }

        public ActionResult GetSSRInventory(string AsOnDate)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_GetSSRInventory '" + AsOnDate + "'");
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public ActionResult WOMaterialTransfer()
        {

            return View();
        }

        public JsonResult GetInventoryTransfer(string AsOnDate)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WOtransfer> receiptEntries = new List<BE.WOtransfer>();
            dataTable = db.sub_GetDatatable("USP_GetSSRInventory_Transfer '" + AsOnDate + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WOtransfer receiptEntry = new BE.WOtransfer();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.SrNo = Convert.ToInt32(row["Sr No"]);
                    receiptEntry.ItemCode = Convert.ToString(row["Item Code"]);
                    receiptEntry.ItemName = Convert.ToString(row["Item Name"]);
                    receiptEntry.ReceivedQty = Convert.ToString(row["Received Qty"]);
                    receiptEntry.IssQty = Convert.ToString(row["Iss Qty"]);
                    receiptEntry.Balance = Convert.ToString(row["Balance"]);
                    receiptEntry.TransferQty = Convert.ToString(row["TransferQty"]);
                    receiptEntry.AutoID = Convert.ToInt32(row["AutoID"]);
                    receiptEntry.Amount = Convert.ToInt32(row["Rate"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [HttpPost]
        public ActionResult MaterialTransferYardToYard(List<BE.WOtransfer> Debitdata, int Location)
        {



            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ItemCode");
            dataTable.Columns.Add("ItemName");
            dataTable.Columns.Add("TransferQty");
            dataTable.Columns.Add("AutoID");
            dataTable.Columns.Add("Amount");

            dataTable.TableName = "UTYPE_INSERT_WO_MATERAL_DET_Transfer";
            foreach (BE.WOtransfer item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["ItemCode"] = item.ItemCode;
                row["ItemName"] = item.ItemName;
                row["TransferQty"] = item.TransferQty;
                row["AutoID"] = item.AutoID;
                row["Amount"] = item.Amount;

                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.Response response = new BE.Response();
            response = BL.SaveWOMaterialTransferBL(Userid, Location, dataTable);
            return Json(response);

        }

    }
}