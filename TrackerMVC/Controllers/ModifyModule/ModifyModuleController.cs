using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using CD = TrackerMVCDataLayer.Helper;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ModifyBL;
using SB = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ExportSBBL;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using DB = TrackerMVCDataLayer;
namespace TrackerMVC.Controllers.ModifyModule
{
    public class ModifyModuleController : Controller
    {
        DB.TrackerMVCDBManager DL = new DB.TrackerMVCDBManager();
        RP.ModifyBL BL = new RP.ModifyBL();
        SB.ExportSBBL SB = new SB.ExportSBBL();
        BM.LoadingPlan.LoadingPlan LP = new BM.LoadingPlan.LoadingPlan();
        BM.BLDataManager.GenerateBL GBL = new BM.BLDataManager.GenerateBL();


        // GET: Bond
        public ActionResult CartingModify()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }
        //CartingAllowID, SrNo, SBEntryID, SBNo, VehicleNo
        public JsonResult GetImpContainerDetails(string CartingAllowID, string SrNo,string SBEntryID,string SBNo,string VehicleNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ModificationEntities> receiptEntries = new List<BE.ModificationEntities>();
            dataTable = db.sub_GetDatatable("USP_GetCartingDetails '" + CartingAllowID + "','" + SrNo + "','" + SBEntryID + "','" + SBNo + "','" + VehicleNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ModificationEntities receiptEntry = new BE.ModificationEntities();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.DeclQty = Convert.ToInt32(row["DeclQty"]);
                    receiptEntry.UnitWT = Convert.ToInt32(row["UnitWT"]);
                    receiptEntry.Allowwt = Convert.ToInt32(row["Allowwt"]);
                    receiptEntry.Location = Convert.ToString(row["Location"]);
                    receiptEntry.EntryDate = Convert.ToString(row["EntryDate"]);
                    receiptEntry.SrNo = Convert.ToInt16(row["SrNo"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        //
        [HttpPost]

        // { "SBNo": SBNo, "Vehicleno": Vehicleno, "CartingDate": CartingDate, "Location": Location, "CartingWT": CartingWT, "CartingQty": CartingQty, "UnitWT": UnitWT, "OldLocation": OldLocation, "OldCartingWT": OldCartingWT, "OldCartingQty": OldCartingQty, "OldUnitWT": OldUnitWT };"SBEntryID": SBEntryID, "AllowID": AllowID, "SRNo": SRNo
        public JsonResult UpdateCartingDetails(string SBNo, string Vehicleno, string CartingDate, string Location, string CartingWT, string CartingQty,string UnitWT,string OldLocation,string OldCartingWT,string OldCartingQty,string OldUnitWT,string Remarks,string SBEntryID,string AllowID,string SRNo)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("UpdateCartingDetails '" + SBNo + "','" + Vehicleno + "','" + Convert.ToDateTime(CartingDate).ToString("yyyy-MM-dd HH:mm") + "','" + Location + "','" + CartingWT + "','" + CartingQty + "','" + UnitWT + "','" + userId + "','" + OldLocation + "','" + OldCartingWT + "','" + OldCartingQty + "','" + OldUnitWT + "','" + Remarks + "','" + SBEntryID + "','" + AllowID + "','" + SRNo + "'");

            if (retVal > 0)
            {
                Message = "Record Updated Successfully.";
            }
            else
            {
                Message = "Records Not Updated Successfully.";
            }
            
            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //public JsonResult GetSBAllowList(string SBNo)
        //{
        //    TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
        //    DataTable dataTable = new DataTable();
        //    List<BE.ModificationEntities> receiptEntries = new List<BE.ModificationEntities>();
        //    dataTable = db.sub_GetDatatable("CartingDetailsSBWise '" + SBNo + "'");
        //    if (dataTable != null)
        //    {
        //        foreach (DataRow row in dataTable.Rows)
        //        {
        //            BE.ModificationEntities receiptEntry = new BE.ModificationEntities();
        //            //DeclQty,UnitWT,Allowwt,[Location],EntryDate
        //            receiptEntry.Edit = Convert.ToString(row["Edit"]);
        //            receiptEntry.CartingAllowID = Convert.ToInt16(row["CartingAllowID"]);
        //            receiptEntry.SrNo = Convert.ToInt16(row["SrNo"]);
        //            receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
        //            receiptEntry.VehicleNo = Convert.ToString(row["VehicleNo"]);
        //            receiptEntry.DeclQty = Convert.ToInt32(row["DeclQty"]);
        //            receiptEntry.UnitWT = Convert.ToInt32(row["UnitWT"]);
        //            receiptEntry.Allowwt = Convert.ToInt32(row["Allowwt"]);
        //            receiptEntry.Location = Convert.ToString(row["Location"]);
        //            receiptEntry.EntryDate = Convert.ToString(row["EntryDate"]);
        //            receiptEntry.SBEntryID = Convert.ToInt32(row["SBEntryID"]);

        //            receiptEntries.Add(receiptEntry);
        //        }
        //    }
        //    var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;

        //    return jsonResult;
        //}

        public ActionResult UpdateDomesticRemarks()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetInvoiceDetails(string InvoiceNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            int SrNo = 0;
            List<BE.DomesticRemarks> receiptEntries = new List<BE.DomesticRemarks>();
            dataTable = db.sub_GetDatatable("GetDomesticRemarks '" + InvoiceNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.DomesticRemarks receiptEntry = new BE.DomesticRemarks();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                   
                    receiptEntry.SrNo = Convert.ToInt32(row["SRNo"]);
                    receiptEntry.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    receiptEntry.Remarks = Convert.ToString(row["Remarks"]);
                    receiptEntry.InWord = Convert.ToString(row["InWord"]);
                    receiptEntry.OutWord = Convert.ToString(row["OutWord"]);
                    receiptEntry.Commodity = Convert.ToString(row["Commodity"]);
                    receiptEntry.Qty = Convert.ToInt64(row["Pkgs"]);
                    receiptEntry.AccountId = Convert.ToInt32(row["AccountID"]);
                    receiptEntry.AccountName = Convert.ToString(row["AccountName"]);
                    receiptEntry.ISACK = Convert.ToString(row["ISACK"]);
                    receiptEntry.IsInvLocked = Convert.ToString(row["IsInvLocked"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        
       // var data1 = { "InvoiceNo": InvoiceNo, "Remarks": Remarks, "InWord": InWord, "OutWord": OutWord, "Commodity": Commodity, "oldRemarks": oldRemarks, "oldInWord": oldInWord, "oldOutWord": oldOutWord, "oldCommodity": oldCommodity };

        public JsonResult UpdateRemarksDetails(string InvoiceNo, string Remarks, string InWord, string OutWord, string Commodity, string oldRemarks, string oldInWord, string oldOutWord, string oldCommodity, List<BE.DomesticRemarks> tablearray)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";
            foreach (BE.DomesticRemarks item in tablearray) { 

            retVal = db.sub_ExecuteNonQuery("UpdateDomesticRemarks '" + InvoiceNo + "','" + Remarks.Replace("'","") + "','" + InWord.Replace("'", "") + "','" + OutWord.Replace("'", "") + "','" + Commodity.Replace("'", "") + "','" + oldRemarks.Replace("'", "") + "','" + oldInWord.Replace("'", "") + "','" + oldOutWord.Replace("'", "") + "','" + oldCommodity.Replace("'", "") + "','" + userId + "','" + item.oldQty + "','" + item.Qty + "','"+item.AccountId+"'");
            }
            if (retVal > 0)
            {
                Message = "Record Updated Successfully.";
            }
            else
            {
                Message = "Records Not Updated Successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ModifyWaraiInvoice()
        {

            List<BE.WaraiDetails> CustomerName = new List<BE.WaraiDetails>();
            CustomerName = BL.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.GSTParty = new SelectList(CustomerName, "PartyID", "PartyName");

            List<BE.WaraiDetails> TaxGroup = new List<BE.WaraiDetails>();
            TaxGroup = BL.GetTaxList();
            ViewBag.TaxGroup = new SelectList(TaxGroup, "TaxID", "Stax");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetWaraiDetails(string InvoiceNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WaraiDetails> receiptEntries = new List<BE.WaraiDetails>();
            dataTable = db.sub_GetDatatable("USP_GetWaraiAssessData '" + InvoiceNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WaraiDetails receiptEntry = new BE.WaraiDetails();
                    receiptEntry.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    receiptEntry.AssessDate = Convert.ToString(row["AssessDate"]);
                    receiptEntry.AssessNo = Convert.ToString(row["AssessNo"]);
                    receiptEntry.WorkYear = Convert.ToString(row["WorkYear"]);
                    receiptEntry.NetTotal = Convert.ToString(row["NetTotal"]);
                    receiptEntry.CGST = Convert.ToString(row["CGST"]);
                    receiptEntry.IGST = Convert.ToString(row["IGST"]);
                    receiptEntry.SGST = Convert.ToString(row["SGST"]);
                    receiptEntry.PartyID = Convert.ToInt32(row["PartyId"]);
                    receiptEntry.CargoQty = Convert.ToString(row["CargoQty"]);
                    receiptEntry.CargoWeight = Convert.ToString(row["CargoWeight"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetTaxAmount(string TaxID,string PartyID,string NetAmount)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WaraiDetails> receiptEntries = new List<BE.WaraiDetails>();
            dataTable = db.sub_GetDatatable("USP_Expenses_Tax_warai '" + TaxID + "','" + PartyID + "','" + NetAmount + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WaraiDetails receiptEntry = new BE.WaraiDetails();
                    receiptEntry.SGST = Convert.ToString(row["sgst"]);
                    receiptEntry.CGST = Convert.ToString(row["cgst"]);
                    receiptEntry.IGST = Convert.ToString(row["igst"]);
                    
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //{ "InvoiceNo": InvoiceNo, "AssessNo": AssessNo, "VehicleNo": VehicleNo, "CargoQty": CargoQty, "v": CargoWeight, "PartyId": PartyId, "NetTotal": NetTotal, "CGST": CGST, "SGST": SGST, "IGST": IGST, "WorkYear": WorkYear, "AssessDate": AssessDate, "oldCargoQty": oldCargoQty, "oldCargoWeight": oldCargoWeight, "oldNetTotal": oldNetTotal, "oldCGST": oldCGST, "oldSGST": oldSGST, "oldIGST": oldIGST };
        public JsonResult UpdateWaraiInvoiceDetails(string InvoiceNo, string AssessNo, string VehicleNo, string CargoQty, string CargoWeight, string PartyId, string NetTotal, string CGST, string SGST,string IGST,string WorkYear, string AssessDate,string oldCargoQty,string oldCargoWeight, string oldNetTotal,string oldCGST,string oldSGST,string oldIGST)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";
            
            retVal = db.sub_ExecuteNonQuery("USP_ModiFyCartingDetails '" + InvoiceNo + "','" + AssessNo + "','" + VehicleNo + "','" + CargoQty + "','" + CargoWeight + "','" + PartyId + "','" + NetTotal + "','" + CGST + "','" + SGST + "','" + IGST + "','" + WorkYear + "','" + Convert.ToDateTime(AssessDate).ToString("yyyy-MM-dd HH:mm") + "','" + oldCargoQty + "','" + oldCargoWeight + "','" + oldNetTotal + "','" + oldCGST + "','" + oldSGST + "','" + oldIGST + "','" + userId + "'");

            if (retVal > 0)
            {
                Message = "Record Updated Successfully.";
            }
            else
            {
                Message = "Records Not Updated Successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult Search(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("CartingDetailsSBWise'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult ModifyWorkOrder()
        {

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            List<BE.ExportShippingBillEnt> pkglist = new List<BE.ExportShippingBillEnt>();
            pkglist = SB.Pkgs();
            ViewBag.pkglist = new SelectList(pkglist, "pkgid", "pkgtype");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            List<BE.VendorList> VendorList = new List<BE.VendorList>();
            VendorList = BL.VendorList();
            ViewBag.Vendor = new SelectList(VendorList, "VendorID", "VendorName");
            return View();
        }

        public JsonResult GetDetails(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_ModifyWorkOrderDetails'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //var data1 = { "WO_NO": WO_NO, "ContainerNo": ContainerNo, "PKGSDestuff": PKGSDestuff, "SbNo": SbNo, "Vendor": Vendor, "PkgType": PkgType, "Weight": Weight, "VehicleNo": VehicleNo, "StuffLocation": StuffLocation, "Remarks": Remarks };

        // work order modify update

        public JsonResult UpdateWorkOrderDetails(string WO_NO, string ContainerNo, string PKGSDestuff, string SbNo, string Vendor, string PkgType, string Weight, string VehicleNo, string StuffLocation, string Remarks)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("USP_Update_Work_order_Details '" + WO_NO + "','" + ContainerNo + "','" + PKGSDestuff + "','" + SbNo + "','" + Vendor + "','" + PkgType + "','" + Weight + "','" + VehicleNo + "','" + StuffLocation + "','" + Remarks + "','" + userId + "'");

            if (retVal > 0)
            {
                Message = "Record Updated Successfully.";
            }
            else
            {
                Message = "Records Not Updated Successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        public ActionResult ModifyVehicleNo()
        {

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            List<BE.ActivityListGet> pkglist = new List<BE.ActivityListGet>();
            pkglist = SB.ActivityGet();
            ViewBag.ActivityList = new SelectList(pkglist, "Activity", "Activity");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            return View();
        }


        public ActionResult UpdateVehicleNo(string Location, string Activity, string ContainerNo, string EntryID, string VehicleNo, string OldVehicleNo, string Remarks)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            string message = BL.UpdateModifyVehicle(Location, Activity, ContainerNo, EntryID, VehicleNo, OldVehicleNo, Remarks, Userid);
            return Json(message);
        }


        [HttpPost]
        public ActionResult GetAllVehicle(string Location, string Activity, string ContainerNo)
        {
            List<BE.GetAllVehicleData> Driverpayment = new List<BE.GetAllVehicleData>();
            Driverpayment = BL.GetAllVehicle(Location, Activity, ContainerNo);

            var jsonResult = Json(Driverpayment, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult UpdateMCINPCIN()
        {



            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            return View();
        }

        public JsonResult GetSBDetails(string SBNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.SBDetails> receiptEntries = new List<BE.SBDetails>();
            dataTable = db.sub_GetDatatable("sp_getsbdetails '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.SBDetails receiptEntry = new BE.SBDetails();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate

                    receiptEntry.SBNo = Convert.ToString(row["SBDate"]);
                    receiptEntry.SBDate = Convert.ToString(row["SBDate"]);
                    receiptEntry.SBEntryID = Convert.ToInt32(row["SBEntryID"]);
                    // receiptEntry.InWord = Convert.ToString(row["InWord"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        //var data1 = { "SBNo": SBNo, "MCIN": MCIN };
        public JsonResult UpdateSBMCIN(string SBNo, string MCIN)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("USP_UPDATE_MCIN_PCIN '" + SBNo + "','" + MCIN + "'");

            if (retVal > 0)
            {
                Message = "Record Updated Successfully.";
            }
            else
            {
                Message = "Records Not Updated Successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ModifyCHAandLine()
        {

            List<BE.ExportShippingBillEnt> CHAList = new List<BE.ExportShippingBillEnt>();
            CHAList = SB.CHADLL();
            ViewBag.CHAList = new SelectList(CHAList, "CHAID", "CHAName");

            List<BE.ExportShippingBillEnt> ShipLines = new List<BE.ExportShippingBillEnt>();
            ShipLines = SB.SLID();
            ViewBag.SLID = new SelectList(ShipLines, "SLID", "SLName");


            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetIMPInvDetails(string InvoiceNo, string WorkYear)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ImportInvoiceMod> receiptEntries = new List<BE.ImportInvoiceMod>();
            dataTable = db.sub_GetDatatable("USP_Get_imp_inv_det '" + InvoiceNo + "','" + WorkYear + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ImportInvoiceMod receiptEntry = new BE.ImportInvoiceMod();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate

                    receiptEntry.slid = Convert.ToInt32(row["slid"]);
                    receiptEntry.chaid = Convert.ToInt32(row["chaid"]);
                    receiptEntry.transtypeid = Convert.ToInt32(row["transtypeid"]);
                    receiptEntry.deliverytype = Convert.ToString(row["deliverytype"]);
                    receiptEntry.Movement = Convert.ToString(row["Movement"]);
                    receiptEntry.Consignee = Convert.ToString(row["Consignee"]);
                    receiptEntry.IGMNo = Convert.ToString(row["IGMNo"]);
                    receiptEntry.ItemNo = Convert.ToString(row["ItemNo"]);
                    receiptEntry.AssessDate = Convert.ToString(row["AssessDate"]);
                    receiptEntry.Remarks = Convert.ToString(row["Remarks"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        //{ "InvoiceNo": InvoiceNo, "WorkYear": WorkYear, "IGMNo": IGMNo, "ItemNo": ItemNo, "DelType": DelType, "Movement": Movement, "TransType": TransType, "CHAID": CHAID, "": SLID, "Consignee": Consignee, "Remarks": Remarks };
        public JsonResult UpdateLineDetails(string InvoiceNo, string WorkYear, string IGMNo, string ItemNo, string DelType, string Movement, string TransType, string CHAID, string SLID, string Consignee, string Remarks)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("USP_UpdateImportInvoiceDet '" + InvoiceNo + "','" + WorkYear + "','" + IGMNo + "','" + ItemNo + "','" + DelType + "','" + Movement + "','" + TransType + "','" + CHAID + "','" + SLID + "','" + Consignee + "','" + Remarks + "','" + userId + "'");

            if (retVal > 0)
            {
                Message = "Record Updated Successfully.";
            }
            else
            {
                Message = "Records Not Updated Successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // Invoice Check invoice done.
        public JsonResult CheckAckDoneOrNot(string InvoiceNo, string WorkYear)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            //var retVal = 0;
            string Message = "";

            tblInvoiceList = db.sub_GetDatatable("USP_ACKDoneOrNot '" + InvoiceNo + "','" + WorkYear + "'");
            //tbl row will count
            if (tblInvoiceList.Rows.Count > 0)
            {
                Message = "ACK Done Cannot Modify .";
            }
            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // CODE BY SAGAR

        public ActionResult ModifyImportContainerD()
        {
            List<BE.ContainerSize> ContainerSize = new List<BE.ContainerSize>();
            List<BE.ISOCodes> ISOCodes = new List<BE.ISOCodes>();
            List<BE.CargoType> CargoType = new List<BE.CargoType>();
            List<BE.ImportJOType> JoType = new List<BE.ImportJOType>();
            List<BE.ContainerType> containerTypes = new List<BE.ContainerType>();

            ContainerSize = GBL.getContainerSize();
            ISOCodes = GBL.getISOCodes();
            CargoType = GBL.getCargoType();
            JoType = GBL.getJOType();
            containerTypes = GBL.ContainerType();

            ViewBag.ContainerSize = new SelectList(ContainerSize, "ContainerSizeID", "ContainerSizeName");
            ViewBag.ISOCodes = new SelectList(ISOCodes, "ISOID", "ISOCode");
            ViewBag.CargoType = new SelectList(CargoType, "Cargotypeid", "Cargotype");
            ViewBag.JoTypes = new SelectList(JoType, "JotypeId", "Jotype");
            ViewBag.Type = new SelectList(containerTypes, "ContainerTypeID", "ContainerTypeName");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            return View();
        }
        public JsonResult GetContainerWiseDetails(string ContainerNo)
        {
            BE.ModifyImpContainer data = new BE.ModifyImpContainer();
            data = GBL.GetContainerWiseDetails(ContainerNo);
            return Json(data);
        }



        public JsonResult UpdateJOBMDetails(BE.ModifyImpContainer Master)
        {
            BE.Response response = new BE.Response();
            try
            {
                Master.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now;
                response = GBL.UpdateImportJoDet(Master);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }


        public JsonResult CheckIMPInvDoneOrNot(string ContainerNo)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            //var retVal = 0;
            string Message = "";

            tblInvoiceList = db.sub_GetDatatable("Check_IMP_Inv_for_container '" + ContainerNo + "'");
            //tbl row will count
            if (tblInvoiceList.Rows.Count > 0)
            {
                Message = "Invoice generated cannot modify .";
            }
            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GateInSlip()
        {

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            return View();
        }

        public JsonResult GetDetailsINContainer(string ContainerNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ModifyImpContainer> receiptEntries = new List<BE.ModifyImpContainer>();
            dataTable = db.sub_GetDatatable("USP_GET_IMP_IN_Det '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ModifyImpContainer receiptEntry = new BE.ModifyImpContainer();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate

                    receiptEntry.INDate = Convert.ToString(row["INDate"]);

                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    // receiptEntry.InWord = Convert.ToString(row["InWord"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public JsonResult GetImportInprintDetails(string ContainerNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ModifyImpContainer> receiptEntries = new List<BE.ModifyImpContainer>();
            dataTable = db.sub_GetDatatable("USP_Import_Print_IN '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ModifyImpContainer receiptEntry = new BE.ModifyImpContainer();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate

                    receiptEntry.INDate = Convert.ToString(row["INDate"]);
                    receiptEntry.DOCNo = Convert.ToString(row["DOCNo"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    receiptEntry.CFSCode = Convert.ToInt32(row["CFSCode"]);
                    receiptEntry.JONo = Convert.ToInt32(row["JONo"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult PrintGateInSlip(string CFSCode)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();


            CD.DBOperations db = new CD.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("USP_SLIP_Gatein_Print '" + CFSCode + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblInvoiceDetails = getJobOrderSet.Tables[1];


                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];

                }

            }

            ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();//
            return PartialView();
        }

        // sagar code done..

        public ActionResult UpdateCreditNoteRemarks()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetCNDetails(string InvoiceNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.DomesticRemarks> receiptEntries = new List<BE.DomesticRemarks>();
            dataTable = db.sub_GetDatatable("USP_GetCNRemarks '" + InvoiceNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.DomesticRemarks receiptEntry = new BE.DomesticRemarks();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate


                    receiptEntry.Remarks = Convert.ToString(row["Remarks"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult UpdateCNRDetails(BE.DomesticRemarks Master)
        {
            BE.Response response = new BE.Response();
            try
            {
                Master.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now;
                response = GBL.UpdateCRNRem(Master);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }
        public ActionResult WeighmentModify()
        {
            return View();
        }

        public JsonResult GetWeightMent(string Slipno, string WorkYear)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Weightment> receiptEntries = new List<BE.Weightment>();
            dataTable = db.sub_GetDatatable("SP_MODIFYWeightmengtEntry '" + Slipno + "','" + WorkYear + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.Weightment receiptEntry = new BE.Weightment();



                    receiptEntry.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    receiptEntry.VehicleType = Convert.ToString(row["VehicleType"]);
                    receiptEntry.VehicleTareWT = Convert.ToString(row["VehicleTareWT"]);
                    receiptEntry.AddedOn = Convert.ToString(row["AddedOn"]);
                    receiptEntry.WeighDate1 = Convert.ToString(row["WeighDate1"]);
                    receiptEntry.Size = Convert.ToString(row["Size"]);
                    receiptEntry.TareWeight = Convert.ToString(row["TareWeight"]);
                    receiptEntry.GrossWeight = Convert.ToString(row["GrossWeight"]);
                    receiptEntry.GetWeigth = Convert.ToString(row["GetWeigth"]);
                    receiptEntry.NetWeight = Convert.ToString(row["NetWeight"]);
                   receiptEntry.ID = Convert.ToInt32(row["ID"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        //"Date": Date, "SlipDate": SlipDate, "Tarewt": TareWt, "GrossWt": GrossWt, "NetWt": NetWt, "VtareWt": VtareWt, "GetWt": GetWt, "Slipno": Slipno, "WorkYear": WorkYear, "ID": ID

        public JsonResult UpdateWeightment(string Date, string SlipDate, string TareWt, string GrossWt, string NetWt, string VtareWt, string GetWt, string Slipno, string WorkYear, int ID,string Conatainerno)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("SP_UpdateWeightmententry'" + Convert.ToDateTime(Date).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(SlipDate).ToString("yyyy-MM-dd HH:mm") + "','" + TareWt + "','" + GrossWt + "','" + NetWt + "','" + VtareWt + "','" + GetWt + "','" + Slipno + "','" + WorkYear + "','" + ID + "','" + Conatainerno + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult CancelExportGateIn()
        {

            List<BE.ContainerType> containerTypes = new List<BE.ContainerType>();
            containerTypes = GBL.ContainerType();
            ViewBag.Type = new SelectList(containerTypes, "ContainerTypeID", "ContainerTypeName");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetCancelDetailExpIn(string ContainerNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ModifyEXPContainer> receiptEntries = new List<BE.ModifyEXPContainer>();
            dataTable = db.sub_GetDatatable("USP_CANCEL_EXPORT_IN_CTRS '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ModifyEXPContainer receiptEntry = new BE.ModifyEXPContainer();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate

                    receiptEntry.INDate = Convert.ToString(row["INDate"]);
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    receiptEntry.Agency = Convert.ToString(row["Agency"]);
                    receiptEntry.Type = Convert.ToInt32(row["Type"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);
                    receiptEntry.Remarks = Convert.ToString(row["Remarks"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult CancelExpContainerByID(BE.ModifyEXPContainer Details)
        {
            BE.Response response = new BE.Response();
            try
            {
                Details.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now;
                response = GBL.CancelExportContainer(Details);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }


        public ActionResult ExportCLPModification()
        {

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");
            //For Shipper ExportShippingBillEnt Is Entities
            List<BE.ExportShippingBillEnt> Exporter = new List<BE.ExportShippingBillEnt>();
            Exporter = SB.ExporterDDL();
            ViewBag.Exporter = new SelectList(Exporter, "shipperid", "Shippername");
            //CHA LIST
            List<BE.ExportShippingBillEnt> CHAList = new List<BE.ExportShippingBillEnt>();
            CHAList = SB.CHADLL();
            ViewBag.CHAList = new SelectList(CHAList, "CHAID", "CHAName");

            List<BE.ExportShippingBillEnt> CargoL = new List<BE.ExportShippingBillEnt>();
            CargoL = SB.CargoType();
            ViewBag.CargoList = new SelectList(CargoL, "CID", "CType");

            ViewBag.CargoList = JsonConvert.SerializeObject(CargoL);

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetExportCLPDetails(string Category, string Search)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportModifyMaster> receiptEntries = new List<BE.ExportModifyMaster>();
            dataTable = db.sub_GetDatatable("USP_ShowCLPDetailsWeb '" + Category + "','" + Search + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportModifyMaster receiptEntry = new BE.ExportModifyMaster();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Edit = Convert.ToString(row["Edit"]);
                    receiptEntry.EntryDate = Convert.ToString(row["EntryDate"]);
                    receiptEntry.CLPNo = Convert.ToInt32(row["CLPNo"]);
                    receiptEntry.containerNo = Convert.ToString(row["containerNo"]);
                    receiptEntry.AgentSeal = Convert.ToString(row["AgentSeal"]);
                    receiptEntry.CustomSeal = Convert.ToString(row["CustomSeal"]);
                    receiptEntry.CargoDesc = Convert.ToString(row["CargoDesc"]);
                    receiptEntry.Cargo_Type = Convert.ToString(row["Cargo_Type"]);
                    receiptEntry.Qty = Convert.ToInt32(row["Qty"]);
                    receiptEntry.Weight = Convert.ToInt32(row["Weight"]);
                    receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntry.Shipper = Convert.ToString(row["Shipper"]);
                    receiptEntry.Agent = Convert.ToString(row["Agent"]);
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    receiptEntry.SBEntryID = Convert.ToInt32(row["SBEntryID"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);



                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [HttpPost]
        public ActionResult SaveExportTariffSettingDetails(List<BE.ExportModifyMaster> Debitdata)
        {



            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("containerNo");
            dataTable.Columns.Add("SBNo");
            dataTable.Columns.Add("SBEntryID");
            dataTable.Columns.Add("AgentSeal");
            dataTable.Columns.Add("CustomSeal");
            dataTable.Columns.Add("Cargo_Type");
            dataTable.Columns.Add("CargoDesc");
            dataTable.Columns.Add("Qty");
            dataTable.Columns.Add("Weight");

            dataTable.TableName = "UTYPE_EXPORT_CLP_MODIFY";
            foreach (BE.ExportModifyMaster item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["EntryID"] = item.EntryID;
                row["containerNo"] = item.containerNo;
                row["SBNo"] = item.SBNo;
                row["SBEntryID"] = item.SBEntryID;
                row["AgentSeal"] = item.AgentSeal;
                row["CustomSeal"] = item.CustomSeal;
                row["Cargo_Type"] = item.Cargo_Type;
                row["CargoDesc"] = item.CargoDesc;
                row["Qty"] = item.Qty;
                row["Weight"] = item.Weight;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);



            BE.Response response = new BE.Response();
            response = BL.SaveExportTariffSettingDetails(Userid, dataTable);






            return Json(response);

        }


        // 19-04-2021 SAGAR CODE MERGE

        public ActionResult CancelSBEntry()
        {

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");
            //For Shipper ExportShippingBillEnt Is Entities
            List<BE.ExportShippingBillEnt> Exporter = new List<BE.ExportShippingBillEnt>();
            Exporter = SB.ExporterDDL();
            ViewBag.Exporter = new SelectList(Exporter, "shipperid", "Shippername");
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetSBDetails1(string SBNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            dataTable = db.sub_GetDatatable("USP_SBDetailsForCancel '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt getsbdata = new BE.ExportShippingBillEnt();
                    // getsbdata.EntryDate = Convert.ToDateTime(row["EntryDate"]).ToString("yyyy-MM-dd HH:mm");

                    getsbdata.AGName = Convert.ToString(row["AGName"]);
                    getsbdata.Shippername = Convert.ToString(row["Shippername"]);
                    getsbdata.EntryID = Convert.ToInt32(row["SBEntryID"]);
                    getsbdata.CargoDesc = Convert.ToString(row["CargoDesc"]);

                    receiptEntries.Add(getsbdata);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult GetSbDetailsWithCheck(string SBNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            dataTable = db.sub_GetDatatable("USP_ShowSbDetilsForCancel '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt getsbdata = new BE.ExportShippingBillEnt();
                    // getsbdata.EntryDate = Convert.ToDateTime(row["EntryDate"]).ToString("yyyy-MM-dd HH:mm");

                    getsbdata.Cancel = Convert.ToString(row["Cancel"]);
                    getsbdata.SBNO = Convert.ToString(row["SBNo"]);
                    getsbdata.SBDate = Convert.ToString(row["SBDate"]);
                    getsbdata.ContNo = Convert.ToString(row["Container No"]);
                    getsbdata.EntryID = Convert.ToInt16(row["SBEntryID"]);
                    getsbdata.CargoDesc = Convert.ToString(row["CargoDesc"]);

                    receiptEntries.Add(getsbdata);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult CancelStuffingRequest()
        {

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");
            //For Shipper ExportShippingBillEnt Is Entities
            List<BE.ExportShippingBillEnt> Exporter = new List<BE.ExportShippingBillEnt>();
            Exporter = SB.ExporterDDL();
            ViewBag.Exporter = new SelectList(Exporter, "shipperid", "Shippername");
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }


        public JsonResult GetExportStuffingDetails(string Category, string Search)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportModifyMaster> receiptEntries = new List<BE.ExportModifyMaster>();
            dataTable = db.sub_GetDatatable("USP_ShowStuffingRequestDetailsWeb '" + Category + "','" + Search + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportModifyMaster receiptEntry = new BE.ExportModifyMaster();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Edit = Convert.ToString(row["Edit"]);
                    receiptEntry.EntryDate = Convert.ToString(row["EntryDate"]);
                    receiptEntry.CLPNo = Convert.ToInt32(row["StuffRequestID"]);
                    receiptEntry.containerNo = Convert.ToString(row["containerNo"]);
                    receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntry.SBEntryID = Convert.ToInt32(row["SBEntryID"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);



                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult ShowStuffingRequestDetC(string Category, string Search)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            dataTable = db.sub_GetDatatable("USP_ShowStuffingRequestDet '" + Category + "','" + Search + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt receiptEntry = new BE.ExportShippingBillEnt();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.AGName = Convert.ToString(row["AGName"]);
                    receiptEntry.EntryDate = Convert.ToString(row["InDate"]);




                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult CancelStuffingReqDetails(List<BE.ExportModifyMaster> Debitdata, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("SBNO");
            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("containerNo");
            dataTable.Columns.Add("SBEntryID");


            //list to the table
            foreach (BE.ExportModifyMaster item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["SBNO"] = item.SBNo;
                row["EntryID"] = item.EntryID;
                row["containerNo"] = item.containerNo;
                row["SBEntryID"] = item.SBEntryID;


                dataTable.Rows.Add(row);
            }
            message = BL.CancelSRDetails(dataTable, Remarks, UserID);
            return Json(message);
        }

        public ActionResult CancelStuffingDetails()
        {

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");
            //For Shipper ExportShippingBillEnt Is Entities
            List<BE.ExportShippingBillEnt> Exporter = new List<BE.ExportShippingBillEnt>();
            Exporter = SB.ExporterDDL();
            ViewBag.Exporter = new SelectList(Exporter, "shipperid", "Shippername");
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetExportStuffingDetailsCancel(string Category, string Search)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportModifyMaster> receiptEntries = new List<BE.ExportModifyMaster>();
            dataTable = db.sub_GetDatatable("USP_ShowStuffingDetailsWeb '" + Category + "','" + Search + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportModifyMaster receiptEntry = new BE.ExportModifyMaster();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Edit = Convert.ToString(row["Edit"]);
                    receiptEntry.EntryDate = Convert.ToString(row["EntryDate"]);
                    receiptEntry.CLPNo = Convert.ToInt32(row["StuffRequestID"]);
                    receiptEntry.containerNo = Convert.ToString(row["containerNo"]);
                    receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntry.SBEntryID = Convert.ToInt32(row["SBEntryID"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);



                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult CancelStuffingDetailsData(List<BE.ExportModifyMaster> Debitdata, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("SBNO");
            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("containerNo");
            dataTable.Columns.Add("SBEntryID");


            //list to the table
            foreach (BE.ExportModifyMaster item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["SBNO"] = item.SBNo;
                row["EntryID"] = item.EntryID;
                row["containerNo"] = item.containerNo;
                row["SBEntryID"] = item.SBEntryID;


                dataTable.Rows.Add(row);
            }
            message = BL.CancelStuffingDetails(dataTable, Remarks, UserID);
            return Json(message);
        }


        public ActionResult UpdateExportSealNo()
        {


            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult ShowSealDetailsWeb(string Category, string Search)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportModifyMaster> receiptEntries = new List<BE.ExportModifyMaster>();
            dataTable = db.sub_GetDatatable("USP_ShowSealDetailsWeb '" + Category + "','" + Search + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportModifyMaster receiptEntry = new BE.ExportModifyMaster();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Edit = Convert.ToString(row["Edit"]);
                    receiptEntry.containerNo = Convert.ToString(row["Container No"]);
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    receiptEntry.ContainerType = Convert.ToString(row["Container Type"]);
                    receiptEntry.AgentSeal = Convert.ToString(row["Agent Seal"]);
                    receiptEntry.CustomSeal = Convert.ToString(row["Custom Seal"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult UpdationOfSealNoExport(List<BE.ExportModifyMaster> Debitdata, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table

            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("containerNo");
            dataTable.Columns.Add("AgentSeal");
            dataTable.Columns.Add("CustomSeal");


            //list to the table
            foreach (BE.ExportModifyMaster item in Debitdata)
            {
                DataRow row = dataTable.NewRow();


                row["EntryID"] = item.EntryID;
                row["containerNo"] = item.containerNo;
                row["AgentSeal"] = item.AgentSeal;
                row["CustomSeal"] = item.CustomSeal;


                dataTable.Rows.Add(row);
            }
            message = BL.UpdateExpSealNo(dataTable, Remarks, UserID);
            return Json(message);
        }

        //GP Convert Destuff To Loaded

        public ActionResult LoadedToDestuffGP()
        {


            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetImportGPDetails(string GPNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ImportCommonEnt> receiptEntries = new List<BE.ImportCommonEnt>();
            dataTable = db.sub_GetDatatable("USP_GET_GP_DETAILS_FOR_CONVERT '" + GPNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ImportCommonEnt receiptEntry = new BE.ImportCommonEnt();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.EntryDate = Convert.ToString(row["gpdate"]);
                    receiptEntry.IGMNo = Convert.ToString(row["IGMNo"]);

                    receiptEntry.IGM_ItemNo = Convert.ToString(row["IGM_ItemNo"]);
                    receiptEntry.SLName = Convert.ToString(row["SLName"]);
                    receiptEntry.DeliveryType = Convert.ToString(row["DeliveryType"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }



        public JsonResult GetImportGridGPCDetails(string GPNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ImportCommonEnt> receiptEntries = new List<BE.ImportCommonEnt>();
            dataTable = db.sub_GetDatatable("USP_GET_GRID_GPDETAILS_CONVERT '" + GPNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ImportCommonEnt receiptEntry = new BE.ImportCommonEnt();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.JONo = Convert.ToInt32(row["JO No"]);
                    receiptEntry.containerNo = Convert.ToString(row["Container No"]);
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);


                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult UpdateDeliveryTypeDToL(List<BE.ImportCommonEnt> Debitdata, string Remarks, string GPNo)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table

            dataTable.Columns.Add("JONo");
            dataTable.Columns.Add("containerNo");



            //list to the table
            foreach (BE.ImportCommonEnt item in Debitdata)
            {
                DataRow row = dataTable.NewRow();


                row["JONo"] = item.JONo;
                row["containerNo"] = item.containerNo;



                dataTable.Rows.Add(row);
            }
            message = BL.UpdateDelType(dataTable, Remarks, GPNo, UserID);
            return Json(message);
        }


        public ActionResult DomesticCustomerUpdate()
        {

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");

            List<BE.DomesticTrainMaster> Train = new List<BE.DomesticTrainMaster>();
            Train = BL.TrainM();
            ViewBag.DomesticTrainList = new SelectList(Train, "TrainID", "TrainNo");
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetDomesticTrainList(string TrainID)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.DomesticMasterEnt> receiptEntries = new List<BE.DomesticMasterEnt>();
            dataTable = db.sub_GetDatatable("USP_GET_DOMESTIC_TRAIN_LIST '" + TrainID + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.DomesticMasterEnt receiptEntry = new BE.DomesticMasterEnt();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Srno = Convert.ToInt32(row["Sr No"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);
                    receiptEntry.LoadingID = Convert.ToInt32(row["LoadingID"]);
                    receiptEntry.TrainNo = Convert.ToString(row["TrainNo"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["Container No"]);
                    receiptEntry.AGName = Convert.ToString(row["Customer"]);
                    receiptEntry.LaodedDate = Convert.ToString(row["Loaded Date"]);
                    receiptEntry.Remarks = Convert.ToString(row["Remark"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult UpdateDomesticLoadingDetails(List<BE.DomesticMasterEnt> Debitdata, int TrainID, int AGID, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table

            dataTable.Columns.Add("LoadingID");
            dataTable.Columns.Add("EntryID");


            //list to the table
            foreach (BE.DomesticMasterEnt item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["LoadingID"] = item.LoadingID;
                row["EntryID"] = item.EntryID;
                dataTable.Rows.Add(row);
            }
            message = BL.UpdateDomesticData(dataTable, TrainID, AGID, Remarks, UserID);
            return Json(message);
        }



        public ActionResult ClearDocumentForInvoice()
        {


            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }


        public JsonResult GetClearDocumentForInvoice(string ContinerNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportModifyMaster> receiptEntries = new List<BE.ExportModifyMaster>();
            dataTable = db.sub_GetDatatable("USP_GET_CONTAINER_GPO_DETAILS '" + ContinerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportModifyMaster receiptEntry = new BE.ExportModifyMaster();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate

                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);
                    receiptEntry.GPNo = Convert.ToInt32(row["GPNo"]);

                    receiptEntry.EntryDate = Convert.ToString(row["OutDate"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult UpdateDocForInvExportPList(BE.ExportModifyMaster Master)
        {
            BE.Response response = new BE.Response();
            try
            {
                Master.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now;
                response = BL.UpdateDocForInvExport(Master);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }



        public ActionResult UpdateExportCargoType()
        {

            List<BE.CargoType> CargoType = new List<BE.CargoType>();
            CargoType = GBL.getCargoType();
            ViewBag.CargoType = new SelectList(CargoType, "Cargotypeid", "Cargotype");
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult ShowDetailsForCargoDet(string ContainerNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            dataTable = db.sub_GetDatatable("USP_EXP_Get_ConDetails '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt receiptEntry = new BE.ExportShippingBillEnt();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.AGName = Convert.ToString(row["AGName"]);
                    receiptEntry.CargoDesc = Convert.ToString(row["Cargotype"]);
                    receiptEntry.EntryDate = Convert.ToString(row["InDate"]);
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult UpdateExportCargoTypeDet(BE.ExportModifyMaster Master)
        {
            BE.Response response = new BE.Response();
            try
            {
                Master.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now;
                response = BL.UpdateCargoTypeExport(Master);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }

        // SCANNING GATE PASS OUT CODE BY SAGAR

        public ActionResult ScanningGateOutPass()
        {

            List<BE.ScanningModule> CustomerName = new List<BE.ScanningModule>();
            CustomerName = BL.DriverDDL();
            ViewBag.DriverList = new SelectList(CustomerName, "DriverID", "DriverName");
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }
        public JsonResult GetScanningPassDetails(string ContainerNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ScanningModule> receiptEntries = new List<BE.ScanningModule>();
            dataTable = db.sub_GetDatatable("get_sp_ContainerNoLostFocus '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ScanningModule receiptEntry = new BE.ScanningModule();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    receiptEntry.JONo = Convert.ToInt32(row["JONo"]);

                    receiptEntry.Type = Convert.ToString(row["Type"]);


                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetScanningPassDetailsForPrint(string ContainerNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ScanningModule> receiptEntries = new List<BE.ScanningModule>();
            dataTable = db.sub_GetDatatable("USP_Import_Scanning_Print_GP_Out '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ScanningModule receiptEntry = new BE.ScanningModule();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.OutDate = Convert.ToString(row["OutDate"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);
                    receiptEntry.JONo = Convert.ToInt32(row["JONo"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult SaveScanningGPDetails(List<BE.ScanningModule> Debitdata)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table

            dataTable.Columns.Add("JONo");
            dataTable.Columns.Add("containerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("trailerID");
            dataTable.Columns.Add("driverID");
            dataTable.Columns.Add("Remarks");
            dataTable.Columns.Add("trailername");

            //list to the table
            foreach (BE.ScanningModule item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["JONo"] = item.JONo;
                row["containerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["trailerID"] = item.TrailerID;
                row["driverID"] = item.DriverID;
                row["Remarks"] = item.Remarks;
                row["trailername"] = item.trailername;

                dataTable.Rows.Add(row);
            }
            message = BL.SaveIMPOutGP(dataTable, UserID);
            return Json(message);
        }

        public ActionResult PrintGateOutScanningIMP(string EntryID)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();


            CD.DBOperations db = new CD.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("Sp_ScanningGP_Search_Print '" + EntryID + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];



                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.Con_For = dr["Con_For"];
                    //ViewBag.PANNo = dr["PANNo"];
                    //  ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.EntryID = dr["EntryID"];
                    ViewBag.ContainerNo = dr["ContainerNo"];
                    ViewBag.size = dr["size"];
                    ViewBag.trailerno = dr["trailerno"];
                    ViewBag.OutDate = dr["OutDate"];
                    ViewBag.drivername = dr["drivername"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.SealNoI = dr["SealNoI"];

                }

            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";

            return PartialView();
        }


        public ActionResult ScanningGPOutReport(string Fromdate, string Todate)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("Sp_ScanningGP_Search_Report '" + Convert.ToDateTime(Fromdate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["Sp_ScanningGP_Search_Report"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelScanningPassOut()
        {
            DataTable dt = (DataTable)Session["Sp_ScanningGP_Search_Report"];
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Import Scanning Out Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Import Scanning Out Report <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return View();
        }


        public ActionResult ScanningGateIN()
        {

            List<BE.ScanningModule> CustomerName = new List<BE.ScanningModule>();
            CustomerName = BL.DriverDDL();
            ViewBag.DriverList = new SelectList(CustomerName, "DriverID", "DriverName");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetScanningPassINDetails(string ContainerNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ScanningModule> receiptEntries = new List<BE.ScanningModule>();
            dataTable = db.sub_GetDatatable("get_sp_ContainerNoLostFocus '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ScanningModule receiptEntry = new BE.ScanningModule();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    receiptEntry.JONo = Convert.ToInt32(row["JONo"]);
                    receiptEntry.Type = Convert.ToString(row["Type"]);
                    receiptEntry.TrailerNo = Convert.ToString(row["Trailer"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult GetScanInDetailsForImport(string ContainerNo, int JONo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ScanningModule> receiptEntries = new List<BE.ScanningModule>();
            dataTable = db.sub_GetDatatable("USP_GET_IMPORT_IN_SCANNING_GRID '" + ContainerNo + "','" + JONo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ScanningModule receiptEntry = new BE.ScanningModule();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.SrNo = Convert.ToInt32(row["Sr No"]);
                    receiptEntry.JONo = Convert.ToInt32(row["JO No"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["Container No"]);
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    receiptEntry.Type = Convert.ToString(row["Type"]);
                    receiptEntry.trailername = Convert.ToString(row["Trailer Name"]);
                    receiptEntry.OutDate = Convert.ToString(row["Out Date"]);
                    receiptEntry.INDate = Convert.ToString(row["In Date"]);
                    receiptEntry.DriverName = Convert.ToString(row["Driver Name"]);
                    receiptEntry.Remarks = Convert.ToString(row["Remarks"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult SaveScanningINDetails(List<BE.ScanningModule> Debitdata, bool IsScan, bool CSD, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table

            dataTable.Columns.Add("JONo");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("EntryID");


            //list to the table
            foreach (BE.ScanningModule item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["JONo"] = item.JONo;
                row["ContainerNo"] = item.ContainerNo;
                row["EntryID"] = item.EntryID;


                dataTable.Rows.Add(row);
            }
            message = BL.SaveIMPINGP(dataTable, IsScan, CSD, Remarks, UserID);
            return Json(message);
        }


        public ActionResult ScanningGPINReport(string Fromdate, string Todate)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_GET_IMPORT_IN_SCANNING_GRID_Report '" + Convert.ToDateTime(Fromdate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["Sp_ScanningGP_Search_Report"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelScanningGPINReport()
        {
            DataTable dt = (DataTable)Session["USP_GET_IMPORT_IN_SCANNING_GRID_Report"];
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Import_Scanning_In_Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Import Scanning IN Report <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return View();
        }


        public ActionResult PrintGateINScanningIMP(string EntryID)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();


            CD.DBOperations db = new CD.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("Sp_ScanningGP_Search_IN_Print '" + EntryID + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];



                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.Con_For = dr["Con_For"];
                    //ViewBag.PANNo = dr["PANNo"];
                    //  ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.EntryID = dr["EntryID"];
                    ViewBag.ContainerNo = dr["ContainerNo"];
                    ViewBag.size = dr["size"];
                    ViewBag.trailerno = dr["trailerno"];
                    ViewBag.InDate = dr["InDate"];
                    ViewBag.drivername = dr["drivername"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.SealNoI = dr["SealNoI"];

                }

            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";

            return PartialView();
        }

        // UPDATE CLP DATE AND TIME DONE BY SAGAR

        public ActionResult UpdationOfCLPDate()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }


        public JsonResult GetExportCLPdate(string Category, string Search)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportModifyMaster> receiptEntries = new List<BE.ExportModifyMaster>();
            dataTable = db.sub_GetDatatable("USP_ShowCLPDetailsForDate '" + Category + "','" + Search + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportModifyMaster receiptEntry = new BE.ExportModifyMaster();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Edit = Convert.ToString(row["Edit"]);
                    receiptEntry.EntryDate = Convert.ToString(row["EntryDate"]);
                    receiptEntry.CLPNo = Convert.ToInt32(row["CLPNo"]);
                    receiptEntry.containerNo = Convert.ToString(row["containerNo"]);
                    receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntry.SBEntryID = Convert.ToInt32(row["SBEntryID"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }



        public JsonResult UpdateDateCLPDetailsData(List<BE.ExportModifyMaster> Debitdata, string Remarks, string Date)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("SBNO");
            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("containerNo");
            dataTable.Columns.Add("SBEntryID");


            //list to the table
            foreach (BE.ExportModifyMaster item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["SBNO"] = item.SBNo;
                row["EntryID"] = item.EntryID;
                row["containerNo"] = item.containerNo;
                row["SBEntryID"] = item.SBEntryID;


                dataTable.Rows.Add(row);
            }
            message = BL.UpdateCLPDate(dataTable, Remarks, Date, UserID);
            return Json(message);
        }


        // UPDATE SCANNING TYPE
        public ActionResult UpdateScanningType()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }


        public JsonResult showScanningEntrydetails(string ContainerNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ScanningModule> receiptEntries = new List<BE.ScanningModule>();
            dataTable = db.sub_GetDatatable("GET_showScanningEntry_WEB '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ScanningModule receiptEntry = new BE.ScanningModule();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Edit = Convert.ToString(row["Edit"]);
                    receiptEntry.JONo = Convert.ToInt32(row["JONo"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["Container No"]);
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    receiptEntry.Type = Convert.ToString(row["Type"]);
                    receiptEntry.ScanType = Convert.ToString(row["Scan Type"]);
                    receiptEntry.JODATE = Convert.ToString(row["JODATE"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult UpdateContainerScanType(List<BE.ScanningModule> Debitdata, string ScanType, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("JONo");
            //list to the table
            foreach (BE.ScanningModule item in Debitdata)
            {
                DataRow row = dataTable.NewRow();
                row["ContainerNo"] = item.ContainerNo;
                row["JONo"] = item.JONo;
                dataTable.Rows.Add(row);
            }
            message = BL.UpdateScanTypeDate(dataTable, ScanType, Remarks, UserID);
            return Json(message);
        }


        public ActionResult MarkCartingInvoiceFree()
        {
            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }



        public JsonResult SaveFreeInvoiceData(BE.ModificationEntities Master)
        {
            BE.Response response = new BE.Response();
            try
            {
                Master.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now;
                response = BL.SaveFreeInvoiceDataBL(Master);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }



        // SB Direct allow Code


        public ActionResult SBDirectCartingAllow()
        {

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }


        public JsonResult ShowSBDirectCartingAllowDetails(string SBNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            dataTable = db.sub_GetDatatable("USP_Show_SB_Direct_Allow_Details '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt receiptEntry = new BE.ExportShippingBillEnt();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.AGName = Convert.ToString(row["AGName"]);
                    receiptEntry.CargoDesc = Convert.ToString(row["Cargotype"]);
                    receiptEntry.CHAName = Convert.ToString(row["CHAName"]);

                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult UpdateSBDirectCartingAllowDetails(BE.ExportModifyMaster Master)
        {
            BE.Response response = new BE.Response();
            try
            {
                Master.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now;
                response = BL.UpdateSBDirectCartingAllowDetails(Master);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }
        //FCL TO LCL
        public ActionResult FclToLcl()
        {
            return View();
        }
        public JsonResult ContainerFetching(string Container)
        {
            CD.DBOperations db = new CD.DBOperations();
            int Count = 0;
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_FetchingJobOrder  '" + Container + "'");
            BE.GateInEntities CategoryData = new BE.GateInEntities();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CategoryData.Line = Convert.ToString(dt.Rows[0][0]);
                    CategoryData.JONo = Convert.ToString(dt.Rows[0][1]);
                    CategoryData.Size = Convert.ToString(dt.Rows[0][2]);
                    CategoryData.Type = Convert.ToString(dt.Rows[0][3]);
                    CategoryData.FL = Convert.ToString(dt.Rows[0][4]);
                    CategoryData.Count = 1;

                }

            }
            return Json(CategoryData);
        }
        public JsonResult SaveFclToLcl(string Container, string JoNo, string  Fltype)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();

            dt = db.sub_GetDatatable("USP_SaveFclToLcl '" + Container + "','" + JoNo + "','" + Fltype + "'");
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult CancelDomesticTruckOut()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult CancelDomesticGP(string GPNo)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            var InvoiceList = "";
            ImpFinalOut = db.sub_GetDatatable("Cancel_Domestic_GP '" + GPNo + "'");
            //added by suraj to check null values
            if (ImpFinalOut.Rows.Count > 0)
            {
                InvoiceList = JsonConvert.SerializeObject(ImpFinalOut);
            }
            else
            {
                InvoiceList = "0";
            }
            return new JsonResult() { Data = InvoiceList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult GetDomesticTruckList(string Search)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.DomesticMasterEnt> receiptEntries = new List<BE.DomesticMasterEnt>();
            dataTable = db.sub_GetDatatable("USP_CANCEL_DOMESTIC_TRUCK '" + Search + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.DomesticMasterEnt receiptEntry = new BE.DomesticMasterEnt();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Edit = Convert.ToString(row["Edit"]);
                    receiptEntry.Srno = Convert.ToInt32(row["SrNo"]);
                    receiptEntry.SlipNo = Convert.ToInt32(row["Slip No"]);
                    receiptEntry.BatchNo = Convert.ToString(row["BatchNo"]);
                    receiptEntry.OutDate = Convert.ToString(row["Out Date"]);
                    receiptEntry.RakeNo = Convert.ToString(row["RakeNo"]);

                    receiptEntry.LRNo = Convert.ToString(row["LRNo"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult GetMainDomesticDet(string Search)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.DomesticMasterEnt> receiptEntries = new List<BE.DomesticMasterEnt>();
            dataTable = db.sub_GetDatatable("USP_GET_DOMESTIC_IN '" + Search + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.DomesticMasterEnt receiptEntry = new BE.DomesticMasterEnt();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    receiptEntry.InDate = Convert.ToString(row["Slip Date"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult CancelDetailsOFDomesticTruckO(List<BE.DomesticMasterEnt> Debitdata, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("Srno");
            dataTable.Columns.Add("SlipNo");
            dataTable.Columns.Add("BatchNo");
            dataTable.Columns.Add("LRNo");


            //list to the table
            foreach (BE.DomesticMasterEnt item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["Srno"] = item.Srno;
                row["SlipNo"] = item.SlipNo;
                row["BatchNo"] = item.BatchNo;
                row["LRNo"] = item.LRNo;


                dataTable.Rows.Add(row);
            }
            message = BL.DeleteDomesticOut(dataTable, Remarks, UserID);
            return Json(message);
        }

        public ActionResult TruckBillChargeswaivedoff()
        {

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");
            //For Shipper ExportShippingBillEnt Is Entities
            List<BE.ExportShippingBillEnt> Exporter = new List<BE.ExportShippingBillEnt>();
            Exporter = SB.ExporterDDL();
            ViewBag.Exporter = new SelectList(Exporter, "shipperid", "Shippername");
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult GetExportTruckDetailsGrid(string Category, string Search)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportModifyMaster> receiptEntries = new List<BE.ExportModifyMaster>();
            dataTable = db.sub_GetDatatable("USP_GET_EXPORT_TRUCK_GRID '" + Category + "','" + Search + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportModifyMaster receiptEntry = new BE.ExportModifyMaster();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.EntryID = Convert.ToInt32(row["SrNo"]);
                    receiptEntry.GPNo = Convert.ToInt32(row["GateIn"]);
                    receiptEntry.EntryDate = Convert.ToString(row["EntryDate"]);
                    receiptEntry.TruckNO = Convert.ToString(row["TruckNO"]);
                    receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntry.SBEntryID = Convert.ToInt32(row["SBEntryID"]);




                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult MakeWaivedOffInvoice(List<BE.ExportModifyMaster> Debitdata, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table


            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("GPNo");
            dataTable.Columns.Add("TruckNO");
            dataTable.Columns.Add("SBNo");
            dataTable.Columns.Add("SBEntryID");
            //list to the table
            foreach (BE.ExportModifyMaster item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["EntryID"] = item.EntryID;
                row["GPNo"] = item.GPNo;
                row["TruckNO"] = item.TruckNO;
                row["SBNo"] = item.SBNo;
                row["SBEntryID"] = item.SBEntryID;



                dataTable.Rows.Add(row);
            }
            message = BL.MakeWaivedOffInvoiceBL(dataTable, Remarks, UserID);
            return Json(message);
        }

        public ActionResult TransportFuelVendorMaster()
        {
            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");

            List<BE.WaraiDetails> PartyName = new List<BE.WaraiDetails>();
            PartyName = SB.getCustomerCommonList();
            //AccountID And Name Is Entities same as entities
            ViewBag.GSTParty = new SelectList(PartyName, "PartyID", "PartyName");
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        public JsonResult SaveFuelVendor(BE.ModificationEntities Master)
        {
            BE.Response response = new BE.Response();
            try
            {
                Master.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now;
                response = BL.SaveFuelVendorDataBL(Master);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }

        public JsonResult FuelVendorSummary(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_GET_VENDOR_LIST'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult HSNCodeMaster()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }


        public JsonResult SaveHSNCode(BE.ModificationEntities Master)
        {
            BE.Response response = new BE.Response();
            try
            {
                Master.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now; 
                response = BL.SaveHSNCodeBL(Master);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }

        public JsonResult gethsnlist(string Search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_HSN_LIST'" + Search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // CODE BY SAGAR
        public ActionResult ModifyOthersInvoice()
        {

            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            ViewBag.Location = JsonConvert.SerializeObject(Location);


            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }

        // CODE BY SAGAR
        public JsonResult GetOtherMainDetails(string InvoiceNo, string WorkYear)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WaraiDetails> receiptEntries = new List<BE.WaraiDetails>();
            dataTable = db.sub_GetDatatable("USP_GET_other_MODIFY_Main '" + InvoiceNo + "','" + WorkYear + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WaraiDetails receiptEntry = new BE.WaraiDetails();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.isack = Convert.ToInt32(row["isack"]);
                    receiptEntry.isinvlocked = Convert.ToInt32(row["isinvlocked"]);
                    receiptEntry.Iscancel = Convert.ToInt32(row["Iscancel"]);
                    receiptEntry.PartyName = Convert.ToString(row["PartyName"]);
                    receiptEntry.cargodesc = Convert.ToString(row["cargodesc"]);



                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult GETOTHERMODIFYDETAILS(string InvoiceNo, string WorkYear)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WaraiDetails> receiptEntries = new List<BE.WaraiDetails>();
            dataTable = db.sub_GetDatatable("USP_GET_OTHER_MODIFY_DETAILS '" + InvoiceNo + "','" + WorkYear + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WaraiDetails receiptEntry = new BE.WaraiDetails();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Series = Convert.ToInt32(row["Series"]);

                    receiptEntry.AssessNo = Convert.ToString(row["AssessNo"]);
                    receiptEntry.WorkYear = Convert.ToString(row["WorkYear"]);
                    receiptEntry.AccountName = Convert.ToString(row["AccountName"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    receiptEntry.Size = Convert.ToString(row["Size"]);
                    receiptEntry.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    receiptEntry.FromID = Convert.ToInt32(row["FromID"]);
                    receiptEntry.ToID = Convert.ToInt32(row["ToID"]);
                    receiptEntry.TransName = Convert.ToString(row["TransName"]);
                    receiptEntry.narration = Convert.ToString(row["narration"]);
                    receiptEntry.C2 = Convert.ToString(row["C2"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult WorkYearMaster()
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WaraiDetails> receiptEntries = new List<BE.WaraiDetails>();
            dataTable = db.sub_GetDatatable("USP_Work_Year");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WaraiDetails receiptEntry = new BE.WaraiDetails();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate

                    receiptEntry.WorkYear = Convert.ToString(row["WorkYear"]);


                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult UpdateOtherInvoicedetails(List<BE.WaraiDetails> Debitdata, string Remarks, string CargoDesc)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table

            dataTable.Columns.Add("AssessNo");
            dataTable.Columns.Add("WorkYear");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("FromID");
            dataTable.Columns.Add("ToID");
            dataTable.Columns.Add("narration");
            dataTable.Columns.Add("C2");

            //list to the table
            foreach (BE.WaraiDetails item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["AssessNo"] = item.AssessNo;
                row["WorkYear"] = item.WorkYear;
                row["ContainerNo"] = item.ContainerNo;
                row["VehicleNo"] = item.VehicleNo;
                row["FromID"] = item.FromID;
                row["ToID"] = item.ToID;
                row["narration"] = item.narration;
                row["C2"] = item.C2;

                dataTable.Rows.Add(row);
            }
            message = BL.UpdateOtherInvoicedetailsBL(dataTable, Remarks, CargoDesc, UserID);
            return Json(message);
        }



        //CARTING LOCATION MODIFY 
        public ActionResult CartingLocationMofify()
        {
            List<BE.CartingLOcation> CustomerName = new List<BE.CartingLOcation>();
            CustomerName = BL.CartingLocation();
            ViewBag.CartingLocation = new SelectList(CustomerName, "LocationID", "Location");
            ViewBag.CartingLocation = JsonConvert.SerializeObject(CustomerName);
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }


        public JsonResult GetCartingModLocDetails(string Search)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ModificationEntities> receiptEntries = new List<BE.ModificationEntities>();
            dataTable = db.sub_GetDatatable("USP_GET_CARTING_DETAILS_MODIFY '" + Search + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ModificationEntities receiptEntry = new BE.ModificationEntities();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Edit = Convert.ToString(row["Edit"]);
                    receiptEntry.SrNo = Convert.ToInt32(row["SrNo"]);
                    receiptEntry.CartingAllowID = Convert.ToInt32(row["CartingID"]);
                    receiptEntry.EntryDate = Convert.ToString(row["CartingDate"]);
                    receiptEntry.Location = Convert.ToString(row["Location"]);
                    receiptEntry.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntry.DeclQty = Convert.ToInt16(row["pkgs"]);
                    receiptEntry.Allowwt = Convert.ToInt32(row["CartingWT"]);
                    receiptEntry.SBEntryID = Convert.ToInt32(row["SBEntryID"]);



                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult UpdateCartingLocationDet(List<BE.ModificationEntities> Debitdata, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("CartingAllowID");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("SBNo");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("SBEntryID");
            //list to the table
            foreach (BE.ModificationEntities item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["CartingAllowID"] = item.CartingAllowID;
                row["VehicleNo"] = item.VehicleNo;
                row["SBNo"] = item.SBNo;
                row["Location"] = item.Location;
                row["SBEntryID"] = item.SBEntryID;

                dataTable.Rows.Add(row);
            }
            message = BL.UpdateCartingLocationDetBL(dataTable, Remarks, UserID);
            return Json(message);
        }

        public ActionResult ModifyLorryReceipt()
        {

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");

            List<BE.ExportShippingBillEnt> PodListDDL = new List<BE.ExportShippingBillEnt>();
            PodListDDL = SB.PodMaster();
            ViewBag.PodList = new SelectList(PodListDDL, "PodID", "PODName");


            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            ViewBag.Location = JsonConvert.SerializeObject(Location);

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }


        public JsonResult GetSubLRDetails(string LRNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            dataTable = db.sub_GetDatatable("select top 1 LRNo,format(LRDate,'dd-MMM-yyyy HH:mm') as LRDate ,AGID,IsOpen,Iscancel from Lorry_Receipt where LRNo= '" + LRNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt receiptEntry = new BE.ExportShippingBillEnt();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate

                    receiptEntry.AGID = Convert.ToInt32(row["AGID"]);
                    receiptEntry.EntryDate = Convert.ToString(row["LRDate"]);
                    receiptEntry.IsCancel = Convert.ToInt32(row["Iscancel"]);
                    receiptEntry.IsOpen = Convert.ToInt32(row["IsOpen"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetMainGridLRDetails(string LRNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            dataTable = db.sub_GetDatatable("USP_GET_LR_GRID_DETAILS '" + LRNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt receiptEntry = new BE.ExportShippingBillEnt();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate


                    receiptEntry.Series = Convert.ToInt32(row["Series"]);
                    receiptEntry.LRNo = Convert.ToInt32(row["LRNo"]);
                    receiptEntry.LRDate = Convert.ToString(row["LRDate"]);
                    receiptEntry.Activity = Convert.ToString(row["Activity"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    receiptEntry.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    receiptEntry.BookingNo = Convert.ToString(row["BookingNo"]);
                    receiptEntry.BOENo = Convert.ToString(row["BOENo"]);
                    receiptEntry.FromLocationID = Convert.ToInt32(row["FromLocationID"]);
                    receiptEntry.ToLocationID = Convert.ToInt32(row["ToLocationID"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["entryId"]);
                    receiptEntry.consignee = Convert.ToString(row["consignee"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult UpdateLorryReceiptdetails(List<BE.ExportShippingBillEnt> Debitdata, string Remarks, string AGID)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table


            dataTable.Columns.Add("LRNo");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("BookingNo");
            dataTable.Columns.Add("BOENo");
            dataTable.Columns.Add("FromLocationID");
            dataTable.Columns.Add("ToLocationID");
            dataTable.Columns.Add("consignee");


            //list to the table
            foreach (BE.ExportShippingBillEnt item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["LRNo"] = item.LRNo;

                row["ContainerNo"] = item.ContainerNo;
                row["VehicleNo"] = item.VehicleNo;
                row["BookingNo"] = item.BookingNo;
                row["BOENo"] = item.BOENo;
                row["FromLocationID"] = item.FromLocationID;
                row["ToLocationID"] = item.ToLocationID;
                row["consignee"] = item.consignee;

                dataTable.Rows.Add(row);
            }
            message = BL.UpdateLorryReceiptdetailsBL(dataTable, Remarks, AGID, UserID);
            return Json(message);
        }
        public ActionResult DefineDestinationKM(BE.DefineDestination item)
        {

            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");

            if (item.FromLocation != 0 && item.ToLocation != 0)
            {
                ViewBag.FormLocation = item.FromLocation;
                ViewBag.ToLocation = item.ToLocation;
                ViewBag.KM = item.KM;
            }


            return View();
        }


        public ActionResult SaveDefineDestination(int FromLocation, int ToLocation, double KM)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            dataTable = db.sub_GetDatatable("[USP_SaveDefineDestination  ]  '" + FromLocation + "',' " + ToLocation + "', '" + KM + "',' " + UserID  + "'");


            BE.ResponseMessage item = new BE.ResponseMessage();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    item.Status = Convert.ToInt32(row["Status"]);
                    item.Message = Convert.ToString(row["message"]);

                }
            }
            return Json(item);
        }


        public ActionResult getDestinationKMList()
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("[USP_Get_DefineDestinationList ] ");
            Session["USP_Get_DefineDestinationList"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelDefineDestination()
        {

            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["USP_Get_DefineDestinationList"];

            string Tittle = "";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=DefineDestination.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Define Destination List<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        public ActionResult ExcessAmountAdjustment()
        {



            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }


        public JsonResult UpdateExcessAmt(BE.UpdateExcessAmt Master)
        {
            BE.Response response = new BE.Response();
            try
            {
                Master.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);
                //  doc.AddedOn = DateTime.Now; 
                response = BL.UpdateExcessAmtBL(Master);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return Json(response);
        }

        public ActionResult UpdateUnplugDate()
        {


            return View();
        }


        public JsonResult GetContainerDetails(string Containerno)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_GetContainerWiseDetails '" + Containerno + "'");
            List<BE.ContainerDetails> Customerlst = new List<BE.ContainerDetails>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ContainerDetails customer = new BE.ContainerDetails();
                    customer.Size = Convert.ToString(row["size"]);
                    customer.Type = Convert.ToString(row["ContainerType"]);
                    customer.ArrivalDate = Convert.ToString(row["GateInDate"]);
                    customer.JONO = Convert.ToInt32(row["JONo"]);

                    Customerlst.Add(customer);
                }
            }
            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult SaveUnplugDetails(string Contaierno, string Jono, string Uplugdate, string Indate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_InserUnplugDate '" + Contaierno + "','" + Jono + "','" + Uplugdate + "','" + Indate + "'," + Userid + "");
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetContainerunplugdetails()
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_GetContainerunplugdetails");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult CancelExportTruckIn()
        {

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");

            return View();
        }


        public JsonResult GetTruckGridExportIn(string SBNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ModificationEntities> receiptEntries = new List<BE.ModificationEntities>();
            dataTable = db.sub_GetDatatable("USP_CANCEL_EXPORT_TRUCK_IN '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ModificationEntities receiptEntry = new BE.ModificationEntities();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Cancel = Convert.ToString(row["Cancel"]);
                    receiptEntry.SrNo = Convert.ToInt16(row["GateIn"]);
                    receiptEntry.CartingAllowID = Convert.ToInt16(row["AllowID"]);
                    receiptEntry.DeclQty = Convert.ToInt32(row["GateInQty"]);
                    receiptEntry.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult CancelExpTruckIn(List<BE.ModificationEntities> Debitdata, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("SrNo");
            dataTable.Columns.Add("CartingAllowID");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("SBNo");

            //list to the table
            foreach (BE.ModificationEntities item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["SrNo"] = item.SrNo;
                row["CartingAllowID"] = item.CartingAllowID;
                row["VehicleNo"] = item.VehicleNo;
                row["SBNo"] = item.SBNo;

                dataTable.Rows.Add(row);
            }
            message = BL.CancelExpTruckInBL(dataTable, Remarks, UserID);
            return Json(message);
        }

        public ActionResult CancelExportCartingTally()
        {

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");

            return View();
        }


        public JsonResult GetCartingTallyGridCancel(string SBNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ModificationEntities> receiptEntries = new List<BE.ModificationEntities>();
            dataTable = db.sub_GetDatatable("USP_CANCEL_CARTING_TALLY '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ModificationEntities receiptEntry = new BE.ModificationEntities();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Cancel = Convert.ToString(row["SrNo1"]);
                    receiptEntry.SrNo = Convert.ToInt16(row["Srno"]);
                    receiptEntry.EntryDate = Convert.ToString(row["CartingDate"]);
                    receiptEntry.CartingID = Convert.ToInt32(row["CartingID"]);
                    receiptEntry.DeclQty = Convert.ToInt32(row["CartingQty"]);
                    receiptEntry.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult CancelCartingTally(List<BE.ModificationEntities> Debitdata, string Remarks)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("SrNo");
            dataTable.Columns.Add("CartingID");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("SBNo");

            //list to the table
            foreach (BE.ModificationEntities item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["SrNo"] = item.SrNo;
                row["CartingID"] = item.CartingID;
                row["VehicleNo"] = item.VehicleNo;
                row["SBNo"] = item.SBNo;

                dataTable.Rows.Add(row);
            }
            message = BL.CancelCartingTallyBL(dataTable, Remarks, UserID);
            return Json(message);
        }


        public ActionResult CancelCartingAllow()
        {

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");

            return View();
        }


        public JsonResult GetAllowIDDetails(string SBNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ModificationEntities> receiptEntries = new List<BE.ModificationEntities>();
            dataTable = db.sub_GetDatatable("USP_CancelCartingAllow '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ModificationEntities receiptEntry = new BE.ModificationEntities();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Cancel = Convert.ToString(row["Cancel"]);
                    receiptEntry.SrNo = Convert.ToInt16(row["SrNo"]);
                    receiptEntry.CartingAllowID = Convert.ToInt16(row["CartingAllowID"]);
                    receiptEntry.DeclQty = Convert.ToInt32(row["DeclQty"]);
                    receiptEntry.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult FetchSBDetails(string SBNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ModificationEntities> receiptEntries = new List<BE.ModificationEntities>();
            dataTable = db.sub_GetDatatable("GetExportSBDetails '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ModificationEntities getsbdata = new BE.ModificationEntities();
                    // getsbdata.EntryDate = Convert.ToDateTime(row["EntryDate"]).ToString("yyyy-MM-dd HH:mm");

                    getsbdata.AGID = Convert.ToInt32(row["AGID"]);
                    getsbdata.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntries.Add(getsbdata);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // cancel IGM Details with checkboxs
        public JsonResult CancelIGMDetails(List<BE.ModificationEntities> Debitdata)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            //Make temp data store table
            dataTable.Columns.Add("CartingAllowID");

            //list to the table
            foreach (BE.ModificationEntities item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["CartingAllowID"] = item.CartingAllowID;

                dataTable.Rows.Add(row);
            }
            message = BL.CancelCartingallowID(dataTable, UserID);
            return Json(message);
        }


        //Cost Mapping
        public ActionResult ActivityCostMapping()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            return View();

        }

        public ActionResult SaveCostMappingDetails(BE.ActivityMasters AM)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            dataTable = db.sub_GetDatatable("USP_INSERT_COST_MAPPING '" + AM.EntryID + "','" + AM.ActivityName + "','" + AM.ActivityStatus + "','" + AM.ISIN + "','" + AM.ISOUT + "','" + AM.KMIN + "','" + AM.KMOUT + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");


            BE.ResponseMessage item = new BE.ResponseMessage();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    item.Status = Convert.ToInt32(row["Status"]);
                    item.Message = Convert.ToString(row["message"]);

                }
            }
            return Json(item);
        }


        


        public ActionResult GetListOfCostMapping(string search)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_COST_MAPPING_LIST '" + search + "'");
          
            Session["USP_COST_MAPPING_LIST"] = dt;
            Session["AsOn"] = search;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelGetListOfCostMapping()
        {

            DataTable dt = (DataTable)Session["USP_COST_MAPPING_LIST"];
            dt.Columns.Remove("Edit");
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            //DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            string Tittle = "Activity Cost Mapping Report";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Activity Cost Mapping.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Activity Cost Mapping <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }






        public ActionResult bondImportermodify()
        {
            List<BE.Consignee> Consignee = new List<BE.Consignee>();
            Consignee = GBL.getConsignee();
            ViewBag.Consignee = new SelectList(Consignee, "ImporterID", "ImporterName");
            List<BE.ExportShippingBillEnt> CHAList = new List<BE.ExportShippingBillEnt>();
            CHAList = SB.CHADLL();
            ViewBag.CHAList = new SelectList(CHAList, "CHAID", "CHAName");
            return View();
        }

        public JsonResult GetBondModifyDetails(string NOCNO)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ModificationEntities> receiptEntries = new List<BE.ModificationEntities>();
            dataTable = db.sub_GetDatatable("USP_GET_IMP_MODIFYDETS '" + NOCNO + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ModificationEntities receiptEntry = new BE.ModificationEntities();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.NOCDate = Convert.ToString(row["NOCDate"]);
                    receiptEntry.BOENo = Convert.ToString(row["BOENo"]);
                    receiptEntry.ImporterName = Convert.ToString(row["ImporterName"]);
                    receiptEntry.CHAName = Convert.ToString(row["CHAName"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult UpdateBondImporterName(BE.ModificationEntities AM)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            dataTable = db.sub_GetDatatable("USP_BOND_IMPORTER_MODIFY '" + AM.NOCNO + "','" + AM.ImporterName + "','" + AM.CHAName + "','" + AM.EntryID + "','" + AM.Remarks + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");


            BE.ResponseMessage item = new BE.ResponseMessage();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    item.Status = Convert.ToInt32(row["Status"]);
                    item.Message = Convert.ToString(row["message"]);

                }
            }
            return Json(item);
        }



        public ActionResult ModifyOtherTaxRemarks()
        {

            List<BE.Items> TaxRem = new List<BE.Items>();
            TaxRem = SB.TaxRemarks();
            //AccountID And Name Is Entities same as entities
            ViewBag.TaxRemarks = new SelectList(TaxRem, "ID", "Tax_Type_Desc");

            return View();
        }

        public JsonResult GetOtherTaxDetails(string InvoiceNo, string WorkYear)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WaraiDetails> receiptEntries = new List<BE.WaraiDetails>();
            dataTable = db.sub_GetDatatable("USP_GET_OTHER_TAX_MODIFY '" + InvoiceNo + "','" + WorkYear + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WaraiDetails receiptEntry = new BE.WaraiDetails();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.isack = Convert.ToInt32(row["isack"]);
                    receiptEntry.isinvlocked = Convert.ToInt32(row["isinvlocked"]);
                    receiptEntry.Iscancel = Convert.ToInt32(row["Iscancel"]);
                    receiptEntry.IGST = Convert.ToString(row["stax_category_id"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult UpdateOtherRemarks(BE.OtherInvoicesEntities AM)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            dataTable = db.sub_GetDatatable("USP_UPDATE_OTHER_TAX '" + AM.Activity + "','" + AM.AssessNo + "','" + AM.WorkYear + "','" + AM.TaxID + "','" + AM.Remarks + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");


            BE.ResponseMessage item = new BE.ResponseMessage();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    item.Status = Convert.ToInt32(row["Status"]);
                    item.Message = Convert.ToString(row["message"]);

                }
            }
            return Json(item);
        }



        public ActionResult ModifyTransportBillPrefix()
        {



            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            return View();
        }

        public JsonResult GetTransPrefix(string InvoiceNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.SBDetails> receiptEntries = new List<BE.SBDetails>();
            dataTable = db.sub_GetDatatable("USP_GET_TRANSPORT_PREFIX '" + InvoiceNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.SBDetails receiptEntry = new BE.SBDetails();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate

                    receiptEntry.AssessDate = Convert.ToString(row["AssessDate"]);
                    receiptEntry.InvoiceNo = Convert.ToString(row["InvoiceNo"]);

                    // receiptEntry.InWord = Convert.ToString(row["InWord"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult UpdateModifyTransportBillPrefix(BE.SBDetails AM)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            dataTable = db.sub_GetDatatable("USP_Update_TRANSPORT_PREFIX '" + AM.InvoiceNo + "','" + AM.NewInvoiceNo + "','" + AM.AssessDate + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");


            BE.ResponseMessage item = new BE.ResponseMessage();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    item.Status = Convert.ToInt32(row["Status"]);
                    item.Message = Convert.ToString(row["message"]);

                }
            }
            return Json(item);
        }


    }
}