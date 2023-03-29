using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BS = TrackerMVCEntities.BusinessEntities;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ExportModel;
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
using HC = TrackerMVCDataLayer.Helper;
using LR = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.LRClosing;
using II = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ImportInvoice;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using QRCoder;
using System.Drawing;

namespace TrackerMVC.Controllers.ExportModel
{
    [UserAuthenticationFilter]
    public class ExportModelController : Controller
    {
        II.ImportInvoice invDataProvider = new II.ImportInvoice();
        HC.DBOperations db = new HC.DBOperations();
        LR.LRClosing trackerProvider = new LR.LRClosing();
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        RP.ExportModel reportprovider = new RP.ExportModel();
        BM.WorkOrder.WorkOrder WO = new BM.WorkOrder.WorkOrder();
        // GET: ExportModel

        public ActionResult Index()
        {
            return View();
        }
        string Cartingid = "";
        public ActionResult ExportCartingAllow()
        {
            List<BE.ExportCartingAllowEntities> Activity = new List<BE.ExportCartingAllowEntities>();
            Activity = reportprovider.getLoaction();
            ViewBag.ActivityL = new SelectList(Activity, "LocationIID", "Location");
            return View();
        }

        public JsonResult OnAddSBNo(string SBNo)
        {

            //string SBNo =""

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportCartingAllowEntities> receiptEntries = new List<BE.ExportCartingAllowEntities>();
            dataTable = db.sub_GetDatatable("USP_SHOWSBWISEDETAILS '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportCartingAllowEntities receiptEntry = new BE.ExportCartingAllowEntities();
                    receiptEntry.agency = Convert.ToString(row["AGNAME"]);
                    receiptEntry.invoice = Convert.ToString(row["InvoiceNo"]);
                    receiptEntry.sbentryid = Convert.ToString(row["entryID"]);
                    receiptEntry.cha = Convert.ToString(row["CHAName"]);
                    receiptEntry.exporter = Convert.ToString(row["shipperName"]);
                    receiptEntry.cargodesc = Convert.ToString(row["CargoDesc"]);
                    receiptEntry.qty = Convert.ToString(row["TotalPKGS"]);
                    receiptEntry.wt = Convert.ToString(row["CargoWeight"]);
                    receiptEntry.consignee = Convert.ToString(row["Consignee"]);
                    receiptEntry.cargotype = Convert.ToString(row["cargotype"]);
                    receiptEntry.stuffingtype = Convert.ToString(row["StuffingType"]);
                    receiptEntry.bqty = Convert.ToString(row["BalQty"]);
                    receiptEntry.bqty1 = Convert.ToInt32(row["BalQty1"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult Save(List<BE.ExportCartingAllowEntities> Debitdata, int CartingAllowNo, string AllowFromDate)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("LocationName");
            dataTable.Columns.Add("Area");
            dataTable.Columns.Add("Space");
            dataTable.Columns.Add("DelQty");
            dataTable.Columns.Add("ALLOWwT");
            dataTable.Columns.Add("SBNo");
            dataTable.Columns.Add("SBEntryID");
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("Unit");

            dataTable.TableName = "cartingallow";

            foreach (BE.ExportCartingAllowEntities item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["VehicleNo"] = item.VehicleNo;
                row["LocationName"] = item.LocationName;
                row["Area"] = item.Area;
                row["Space"] = item.Space;
                row["DelQty"] = item.DelQty;
                row["ALLOWwT"] = item.ALLOWwT;
                row["SBNo"] = item.SBNo;
                row["SBEntryID"] = item.sbentryid;
                row["InvoiceNo"] = item.InvoiceNo;
                row["Unit"] = item.Unit;

                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.SavepartyDebitEntry(dataTable, CartingAllowNo, AllowFromDate,Userid);
            return Json(message);

        }
        public ActionResult SearchView(string Fromdate,string Todate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("usp_carting_Entry_search '" + Fromdate + "','" + Todate + "'");
            Session["usp_carting_Entry_search"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult WithReason(string SBNO, string SBEntryID)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";

            tblInvoiceList = db.sub_GetDatatable("USP_Hold_SB_No '" + SBNO + "','" + SBEntryID + "'");
            if (tblInvoiceList.Rows.Count > 0) 
            {
                Message = tblInvoiceList.Rows[0]["message"].ToString();
            }
            

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult ChecktheCartingisDone(string SBNo, string sbentryid)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";
            tblInvoiceList = db.sub_GetDatatable("USP_CheckcartingDone '" + SBNo + "','" + sbentryid + "'");
            if (tblInvoiceList.Rows.Count > 0)
            {
                Message = "1";
            }
            else
            {
                Message = "0";
            }
            
            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ExportTruckIn()
        {
            List<BE.ExportCartingAllowEntities> Equipment = new List<BE.ExportCartingAllowEntities>();
            Equipment = reportprovider.GetEquipment();
            ViewBag.EquipmentList = new SelectList(Equipment, "VehicleTypeID", "VehicleType");

            ViewBag.EquipmentList = JsonConvert.SerializeObject(Equipment);
            return View();
        }
        public JsonResult ExportTrukIn(string Allow)
        {

            string SBNo = "";

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportCartingAllowEntities> receiptEntries = new List<BE.ExportCartingAllowEntities>();
            dataTable = db.sub_GetDatatable("get_SP_gateInDetls '" + Allow + "','" + SBNo  + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportCartingAllowEntities receiptEntry = new BE.ExportCartingAllowEntities();
                    receiptEntry.Select = Convert.ToString(row["Sr No"]);
                    receiptEntry.TruckNo = Convert.ToString(row["TruckNo"]);
                    receiptEntry.sbentryid = Convert.ToString(row["SBEntryID"]);
                    receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntry.SBQty = Convert.ToString(row["SBQty"]);
                    receiptEntry.SBWeight = Convert.ToString(row["SBWeight"]);
                    receiptEntry.GateINQty = Convert.ToString(row["GateINQty"]);
                    receiptEntry.GateINWt = Convert.ToString(row["GateINWt"]);
                    receiptEntry.Description = Convert.ToString(row["Description"]);
                    receiptEntry.AgencyName = Convert.ToString(row["AgencyName"]);
                    receiptEntry.VehicleType = Convert.ToString(row["VehicleType"]);
                    receiptEntry.Unit = Convert.ToString(row["Unit"]);
                    receiptEntry.Agid = Convert.ToString(row["AGID"]);

                    receiptEntries.Add(receiptEntry);
                }
            }



            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportTruckSave(List<BE.ExportCartingAllowEntities> Containerdetails, int GateInAllowID, string GateInNoDate)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("TruckNo");
            dataTable.Columns.Add("SBNo");
            dataTable.Columns.Add("VehicleType");
            dataTable.Columns.Add("SBQTY");
            dataTable.Columns.Add("SBWeight");
            dataTable.Columns.Add("GateInQty");
            dataTable.Columns.Add("GateInWT");
            dataTable.Columns.Add("UniT");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("Agid");
            dataTable.Columns.Add("SBEntryID");

            dataTable.TableName = "ExportTruckIn";

            foreach (BE.ExportCartingAllowEntities item in Containerdetails)
            {
                DataRow row = dataTable.NewRow();

                row["TruckNo"] = item.TruckNo;
                row["SBNo"] = item.SBNo;
                row["VehicleType"] = item.VehicleType;
                row["SBQTY"] = item.SBQty;
                row["SBWeight"] = item.SBWeight;
                row["GateInQty"] = item.GateINQty;
                row["GateInWT"] = item.GateINWt;
                row["UniT"] = item.Unit;
                row["Description"] = item.Description;
                row["Agid"] = item.Agid;
                row["SBEntryID"] = item.sbentryid;

                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.SaveExportTruckIn(dataTable, GateInAllowID, GateInNoDate, Userid);
            return Json(message);

        }

        public ActionResult ExportVehicleInventoryDetails()
        {
            
            return View();
        }

        public ActionResult ExportVehicleInventory( string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("get_sp_PendingVehicleDtlss '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["get_sp_PendingVehicleDtls"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelPending()
        {
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["get_sp_PendingVehicleDtls"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExportVehicleInventoryDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Vehicle Inventory Details<strong></td></tr>");

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

        public ActionResult ExportCartingTally(string Cartingid)
        {
            List<BE.ExportCartingAllowEntities> EqmanrtType = new List<BE.ExportCartingAllowEntities>();
            EqmanrtType = reportprovider.GetEquipmentType();
            ViewBag.EqmanrtType = new SelectList(EqmanrtType, "EnID", "EquipmentType");

            List<BE.ExportCartingAllowEntities> Activity = new List<BE.ExportCartingAllowEntities>();
            Activity = reportprovider.getLoaction();
            ViewBag.ActivityL = new SelectList(Activity, "LocationIID", "Location");

            List<BE.ExportCartingTallyEntities> Vendor = new List<BE.ExportCartingTallyEntities>();
            Vendor = reportprovider.getVendor();
            ViewBag.Vendor = new SelectList(Vendor, "VendorID", "VendorName");

            List<BE.CHAtable> CHA = new List<BE.CHAtable>();
            CHA = reportprovider.CHAtable();
            ViewBag.CHAName = new SelectList(CHA, "CHAID", "CHAName");

            List<BE.Agencytable> Agency = new List<BE.Agencytable>();
            Agency = reportprovider.Agencytable();
            ViewBag.Agency = new SelectList(Agency, "agid", "agname");

            List<BE.Shippertable> Shipper = new List<BE.Shippertable>();
            Shipper = reportprovider.Shippertable();
            ViewBag.Shipper = new SelectList(Shipper, "ShipperID", "ShipperName");


            List<BE.Allocatedspace> allocatedspaces = new List<BE.Allocatedspace>();
            allocatedspaces = reportprovider.GetAllocatedSpace();
            ViewBag.Allocated = new SelectList(allocatedspaces, "AllocatedID", "AllocatedName");




            ViewBag.CartingID = Cartingid;



            ViewBag.EquipmentList = JsonConvert.SerializeObject(EqmanrtType);
            ViewBag.Location1 = JsonConvert.SerializeObject(Activity);
            ViewBag.Allocated1 = JsonConvert.SerializeObject(allocatedspaces);
          
            return View();
        }

        public JsonResult GetTariffDetailsForSearch(string SearchType, string Search)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_ShowListPendingvehicleforcarting_Web '" + SearchType + "','" + Search + "'");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ExportCartingSbNo(string SBNo)
        {

            //string SBNo =""

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportCartingTallyEntities> receiptEntries = new List<BE.ExportCartingTallyEntities>();
            dataTable = db.sub_GetDatatable("USP_Carting_Tall_Dets_Allow '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportCartingTallyEntities receiptEntry = new BE.ExportCartingTallyEntities();
                    receiptEntry.AllowID = Convert.ToInt32(row["allowid"]);
                    receiptEntry.SBNo = Convert.ToString(row["SB Number"]);
                    receiptEntry.SBentryid = Convert.ToString(row["SBentryid"]);
                    receiptEntry.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    receiptEntry.QtY = Convert.ToString(row["Cargo Qty"]);
                    receiptEntry.CargoWt = Convert.ToString(row["Cargo Weight"]);
                    receiptEntry.GateInQty = Convert.ToString(row["GateIn Qty"]);
                    receiptEntry.GateInWt = Convert.ToString(row["GateIn Weight"]);
                    receiptEntry.RcvdQty = Convert.ToString(row["Received Qty"]);
                    receiptEntry.RcvdWt = Convert.ToString(row["Received Weight"]);
                    receiptEntry.Loaction = Convert.ToString(row["Carting Location"]);
                    receiptEntry.AllotedSpace = Convert.ToString(row["Alloted Space"]);
                    receiptEntry.SRNO = Convert.ToString(row["Sr No"]);
                    receiptEntry.Unit = Convert.ToInt32(row["unit"]);
                    receiptEntry.AgencyName = Convert.ToString(row["AGName"]);
                  

                    receiptEntries.Add(receiptEntry);
                }
            }
             var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult VehicleNoSBNo(string SBentryid, string SBNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportCartingTallyEntities> receiptEntries = new List<BE.ExportCartingTallyEntities>();
            dataTable = db.sub_GetDatatable("USP_Tally_VEHICLE_NOS_FROM_SBNO '" + SBentryid + "','" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportCartingTallyEntities receiptEntry = new BE.ExportCartingTallyEntities();

                    receiptEntry.AllowID = Convert.ToInt32(row["AllowID"]);
                    receiptEntry.VehicleNo = Convert.ToString(row["TruckNo"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GateInSBNo(string SBentryid, string SBNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportCartingTallyEntities> receiptEntries = new List<BE.ExportCartingTallyEntities>();
            dataTable = db.sub_GetDatatable("USP_Tally_VEHICLE_NOS_FROM_Qty '" + SBentryid + "','" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportCartingTallyEntities receiptEntry = new BE.ExportCartingTallyEntities();

                    receiptEntry.AllowID = Convert.ToInt32(row["AllowID"]);
                    receiptEntry.GateInQty = Convert.ToString(row["gateinqty"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult cmbvehicle(string SBNo, string VehicleNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportCartingTallyEntities> receiptEntries = new List<BE.ExportCartingTallyEntities>();
            dataTable = db.sub_GetDatatable("Select   allowID, GateInQty,GateInWt FROM export_TruckIN WHERE SBNo= '" + SBNo + "'AND TruckNO ='" + VehicleNo + "'AND Status='P' AND Iscancel=0 Order by GateiN desc");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportCartingTallyEntities receiptEntry = new BE.ExportCartingTallyEntities();

                    receiptEntry.GateInQty = Convert.ToString(row["GateInQty"]);
                    receiptEntry.GateInWt = Convert.ToString(row["GateInWt"]);
                    receiptEntry.AllowIDS= Convert.ToInt32(row["allowID"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult SaveTally(List<BE.ExportCartingTallyEntities> Debitdata,string VendorID, string CartingID)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("RcvdQty");
            dataTable.Columns.Add("RcvdWt");
            dataTable.Columns.Add("Area");
            dataTable.Columns.Add("EquipmentTypeID");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("AllotedSpace");
            dataTable.Columns.Add("SBentryid");
            dataTable.Columns.Add("SBNo");
            dataTable.Columns.Add("AllowID");
            dataTable.Columns.Add("SRNO");
            dataTable.Columns.Add("Unit");

            dataTable.TableName = "Carting";

            foreach (BE.ExportCartingTallyEntities item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["VehicleNo"] = item.VehicleNo;
                row["RcvdQty"] = item.RcvdQty;
                row["RcvdWt"] = item.RcvdWt;
                row["Area"] = item.Area;
                row["EquipmentTypeID"] = item.EquipmentTypeID;
                row["Location"] = item.Location;
                row["AllotedSpace"] = item.AllotedSpace;
                row["SBentryid"] = item.SBentryid;
                row["SBNo"] = item.SBNo;
                row["AllowID"] = item.AllowID;
                row["SRNO"] = item.SRNO;
                row["Unit"] = item.Unit;

                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.SaveCartingTally(dataTable, VendorID, CartingID, Userid);
            return Json(message);

        }

        public ActionResult GetSearchInvoice(string FromDate, string ToDate, string DepartmentID,string Search)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("usp_Carting_Summary_Web '" + FromDate + "','" + ToDate + "','" + DepartmentID + "','" + DepartmentID + "'");
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult Cancel(int cartingid)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("usp_cencel_carting '" + cartingid + "','" + UserID + "'");
            string message = "";
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt16(dt.Rows[0][0]) > 0)
                {
                    message = "1";
                }
                else
                {
                    message = "0";
                }
            }
            else
            {
                message = "0";
            }

            return Json(message);
        }

        public ActionResult ExportStuffingRequest()
        {
            List<BE.CHAtable> CHA = new List<BE.CHAtable>();
            CHA = reportprovider.CHAtable();
            ViewBag.CHAName = new SelectList(CHA, "CHAID", "CHAName");

            List<BE.Agencytable> Agency = new List<BE.Agencytable>();
            Agency = reportprovider.Agencytable();
            ViewBag.Agency = new SelectList(Agency, "agid", "agname");

            List<BE.CargoTypes> CargoTypes = new List<BE.CargoTypes>();
            CargoTypes = reportprovider.CargoType();
            ViewBag.CargoTypes = new SelectList(CargoTypes, "Cargotypeid", "Cargotype");


            List<BE.Vessel> Vessel = new List<BE.Vessel>();
            List<BE.Ports> Ports = new List<BE.Ports>();
            List<BE.POL> POL = new List<BE.POL>();
            List<BE.StuffingType> StuffingType = new List<BE.StuffingType>();
            List<BE.equipmentm> equipmentm = new List<BE.equipmentm>();

            Vessel = reportprovider.getVessel();
            Ports = reportprovider.getPorts();
            POL = reportprovider.getPOL();
            StuffingType = reportprovider.getStuffing();
            equipmentm = reportprovider.equipmentm();

            ViewBag.Vessel = new SelectList(Vessel, "VesselID", "VesselName");
            ViewBag.Ports = new SelectList(Ports, "PortID", "PortName");
            ViewBag.POL = new SelectList(POL, "PODID", "PODName");
            ViewBag.StuffingType = new SelectList(StuffingType, "StuffingTypeId", "StuffingTypeM");
            ViewBag.equipmentm = new SelectList(equipmentm, "Id", "Equipment");
            return View();
        }

        public JsonResult ExportStuffingRequestContainerNo(string ContainerNo)
        {

            //string SBNo =""

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportStuffingRequest> receiptEntries = new List<BE.ExportStuffingRequest>();
            dataTable = db.sub_GetDatatable("USP_ShowContainerwiseDetails_Web '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportStuffingRequest receiptEntry = new BE.ExportStuffingRequest();
                    receiptEntry.ShippinglineID = Convert.ToUInt16(row["slid"]);
                    receiptEntry.ShippinglineName = Convert.ToString(row["SLName"]);
                    receiptEntry.lblcontentryID = Convert.ToUInt16(row["entryID"]);
                    receiptEntry.lblagencyid = Convert.ToUInt16(row["agencyid"]);
                    receiptEntry.agencyName = Convert.ToString(row["AGName"]);
                    receiptEntry.txtSize = Convert.ToString(row["size"]);
                    receiptEntry.cmbcontainertype = Convert.ToString(row["ContainerType"]);
                    receiptEntry.txttareweight = Convert.ToString(row["TareWeight"]);
                    receiptEntry.cmbisocode = Convert.ToString(row["ISOCode"]);
                    

                    receiptEntries.Add(receiptEntry);
                }
            }



            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult ExportStuffingRequestSBNo(string SBNo)
        {

            //string SBNo =""

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportStuffingRequest> receiptEntries = new List<BE.ExportStuffingRequest>();
            dataTable = db.sub_GetDatatable("USP_ShowSBWiseSTUFFING '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportStuffingRequest receiptEntry = new BE.ExportStuffingRequest();
                    receiptEntry.cmbstuffingtype = Convert.ToString(row["stuffingtypeID"]);
                    receiptEntry.txtcargodesc = Convert.ToString(row["ShipperName"]);
                    receiptEntry.txtcha = Convert.ToString(row["CHAName"]);
                    receiptEntry.txtagency = Convert.ToString(row["AGNAME"]);
                    receiptEntry.txtcargodesc = Convert.ToString(row["CargoDesc"]);
                    receiptEntry.lblsbentryid = Convert.ToString(row["ENTRYID"]);
                    receiptEntry.lblshipperid = Convert.ToUInt16(row["shipperID"]);
                    receiptEntry.cmbcarotype = Convert.ToString(row["CargoTypeID"]);
                    receiptEntry.txtshipper = Convert.ToString(row["shippername"]);
                    receiptEntry.lblcagencyid = Convert.ToString(row["AGID"]);
                    receiptEntry.txttotalpkgs = Convert.ToString(row["TotalPKGS"]);
                    receiptEntry.txttotalwt = Convert.ToString(row["CargoWeight"]);
                    receiptEntry.txtconsignee = Convert.ToString(row["Consignee"]);


                    receiptEntries.Add(receiptEntry);
                }
            }



            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public JsonResult Getdtcarting(string SBEntryID)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportStuffingRequest> receiptEntries = new List<BE.ExportStuffingRequest>();
            dataTable = db.sub_GetDatatable("SELECT CASE WHEN SUM(CartingQty) IS NULL THEN 0 ELSE SUM(CartingQty) END FROM exp_carting WHERE SBEntryID= '" + SBEntryID + "' AND Iscancel=0 ");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportStuffingRequest receiptEntry = new BE.ExportStuffingRequest();
                    receiptEntry.dblqty = Convert.ToString(dataTable.Rows[0][0].ToString());
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult Getdt(string SBEntryID)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportStuffingRequest> receiptEntries = new List<BE.ExportStuffingRequest>();
            dataTable = db.sub_GetDatatable("SELECT TotalPKGS FROM exp_shippingbill WHERE EntryID= '" + SBEntryID + "' AND Iscancel=0 ");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportStuffingRequest receiptEntry = new BE.ExportStuffingRequest();
                    receiptEntry.dblTotalQty = Convert.ToString(dataTable.Rows[0][0].ToString());
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetdtSB(string SBEntryID)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportStuffingRequest> receiptEntries = new List<BE.ExportStuffingRequest>();
            dataTable = db.sub_GetDatatable("SELECT CASE WHEN SUM(stuffedqty) IS NULL THEN 0 ELSE SUM(stuffedqty) END FROM exp_stuffing WHERE SBEntryID= '" + SBEntryID + "' AND Iscancel=0 ");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportStuffingRequest receiptEntry = new BE.ExportStuffingRequest();
                    receiptEntry.dblqtys = Convert.ToString(dataTable.Rows[0][0].ToString());
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult Getviano(string viano)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportStuffingRequest> receiptEntries = new List<BE.ExportStuffingRequest>();
            dataTable = db.sub_GetDatatable("SELECT rotationNo, MovementID, VesselName, POL, CutOfDate,  movement.VesselID,voyage FROM movement, Vessels WHERE Vessels.VesselID=movement.VesselID and ViaNo = '" + viano + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportStuffingRequest receiptEntry = new BE.ExportStuffingRequest();
                    receiptEntry.txtvessel = Convert.ToString(row["vesselname"]);
                    receiptEntry.txtpol = Convert.ToString(row["POL"]);
                    receiptEntry.txtcutoffdate = Convert.ToString(row["CutOfDate"]);
                    receiptEntry.txtrotation = Convert.ToString(row["rotationNo"]);
                    receiptEntry.lblvesselID = Convert.ToString(row["VesselID"]);
                    receiptEntry.txtvoyage = Convert.ToString(row["Voyage"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult SaveRequest(List<BE.ExportStuffingSave> Debitdata, int StuffingRequestNo, string lblcagencyid,
            string lblvesselID, string VIANo, string POD, string Rotation, string Remarks)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("sbentryid");
            dataTable.Columns.Add("SBNo");
            dataTable.Columns.Add("lblcontentryID");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("ContainerType");
            dataTable.Columns.Add("Unno");
            dataTable.Columns.Add("Temp");
            dataTable.Columns.Add("CargoDesc");
            dataTable.Columns.Add("dblTotalQty");
            dataTable.Columns.Add("TareWeight");
            dataTable.Columns.Add("ddlFPD");
            dataTable.Columns.Add("CargoType");
            dataTable.Columns.Add("Classno");
            dataTable.Columns.Add("lblshipperid");
            dataTable.Columns.Add("EquipmentID");
            dataTable.Columns.Add("ScanStatus");
            dataTable.Columns.Add("NetTotalWeight");
            dataTable.Columns.Add("DestuffQty");
            dataTable.Columns.Add("Destuffweight");
            dataTable.Columns.Add("TotalWeight");

            dataTable.TableName = "StuffingRequest";

            foreach (BE.ExportStuffingSave item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["sbentryid"] = item.sbentryid;
                row["SBNo"] = item.SBNo;
                row["lblcontentryID"] = item.lblcontentryID;
                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["ContainerType"] = item.ContainerType;
                row["Unno"] = item.Unno;
                row["Temp"] = item.Temp;
                row["CargoDesc"] = item.CargoDesc;
                row["dblTotalQty"] = item.dblTotalQty;
                row["TareWeight"] = item.TareWeight;
                row["ddlFPD"] = item.ddlFPD;
                row["CargoType"] = item.CargoType;
                row["Classno"] = item.Classno;
                row["lblshipperid"] = item.lblshipperid;
                row["EquipmentID"] = item.EquipmentID;
                row["ScanStatus"] = item.ScanStatus;
                row["NetTotalWeight"] = item.NetTotalWeight;
                row["DestuffQty"] = item.DestuffQty;
                row["Destuffweight"] = item.Destuffweight;
                row["TotalWeight"] = item.TotalWeight;

                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.StuffingSave(dataTable, StuffingRequestNo, lblcagencyid, lblvesselID, VIANo, POD, Rotation, Remarks, Userid);
            return Json(message);

        }

        public ActionResult GetExportStuffingRequest(string FromDate, string ToDate, string DepartmentID, string Search)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("Sp_ExpSuffingRequestReport '" + FromDate + "','" + ToDate + "','" + DepartmentID + "','" + DepartmentID + "'");
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult ExportStuffingCancel(int StuffRequestID)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_Export_Stuffing_Request '" + StuffRequestID + "','" + UserID + "'");
            string message = "";
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt16(dt.Rows[0][0]) > 0)
                {
                    message = "1";
                }
                else
                {
                    message = "0";
                }
            }
            else
            {
                message = "0";
            }

            return Json(message);
        }
        //WeightmentEntry
        public ActionResult WeightmentEntry()
        {
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = reportprovider.getCustomer();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
            return View();
        }
        public JsonResult TrailorSearch( string TrailorLastDigit)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WeightmentEntities> receiptEntries = new List<BE.WeightmentEntities>();
            dataTable = db.sub_GetDatatable("USP_GET_TRAILER_NO_TEXTCHANGED_WEB_Wemt'" + TrailorLastDigit + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WeightmentEntities receiptEntry = new BE.WeightmentEntities();
                    receiptEntry.TrailerNo = Convert.ToString(row["trailername"]);
                    receiptEntry.lblcfs = Convert.ToString(row["ownedby"]);
                    receiptEntry.txtvehicletarewt = Convert.ToString(row["Vehicle Weight"]);
                    if (receiptEntry.lblcfs == "")
                    {
                        receiptEntry.lblcfs = "Others";
                    }
                    

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetTrailerWeight(string TrailorLastDigit)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WeightmentEntities> receiptEntries = new List<BE.WeightmentEntities>();
            dataTable = db.sub_GetDatatable("USP_GET_TRAILER_NO_TEXTCHANGED_WEB_Wemt'" + TrailorLastDigit + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WeightmentEntities receiptEntry = new BE.WeightmentEntities();
             
                    receiptEntry.txtvehicletarewt = Convert.ToString(row["Vehicle Weight"]);
                  


                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public JsonResult TrailerNoSearch(string TrailerNo)
        {

            DataTable Getdetails = new DataTable();
            DataTable Getdt = new DataTable();

            string message = "";
            List<BE.WeightmentEntities> receiptEntries = new List<BE.WeightmentEntities>();
            CD.DBOperations db = new CD.DBOperations();
            Getdetails = db.sub_GetDatatable("USP_GET_TRAILER_NO_TEXTCHANGED_Wemt '" + TrailerNo + "'");

            

            if (Getdetails.Rows.Count != 0)
            {
                foreach (DataRow row in Getdetails.Rows)
                {
                    BE.WeightmentEntities receiptEntry = new BE.WeightmentEntities();
                    ////Trailer.TrailerNo = Convert.ToString(row["trailername"]);
                    //Trailer.lblcfs = Convert.ToString(row["ownedby"]);
                    //Trailer.txtvehicletarewt = Convert.ToString(row["Vehicle Weight"]);
                    receiptEntry.lblID = Convert.ToString(row["ID"]);
                    if (receiptEntry.lblID != "")
                    {
                        receiptEntry.cmbtranstype = "Second";
                    }
                    receiptEntry.cmbvehicletype = Convert.ToString(row["VehicleType"]) ;
                    if (Convert.ToString(row["Vehiclestatus"]) == "Full")
                    {
                        receiptEntry.cmbvehiclestetus = "Empty";
                    }
                    else
                    {
                        receiptEntry.cmbvehiclestetus = "Full";
                    }
                    receiptEntry.txtpayload = Convert.ToString(row["PayLoad"]);
                    receiptEntry.txtgrosswt = Convert.ToString(row["GrossWeight"]);
                    receiptEntry.txtreceipt = Convert.ToString(row["ReceiptNo"]);
                    receiptEntry.txtremarks = Convert.ToString(row["Remarks"]);
                    receiptEntry.txtslipNo = Convert.ToString(row["SlipNo"]);
                    if (receiptEntry.txtslipNo == "")
                    {
                        receiptEntry.txtslipNo = "NEW";
                    }
                    receiptEntry.txtparty = Convert.ToString(row["partyName"]);
                    if (receiptEntry.lblcfs == "Others")
                    {
                        
                    }
                    receiptEntry.txtvehicletarewt = Convert.ToString(row["VehicleTareWT"]);
                    receiptEntry.txtnetwt = Convert.ToString(row["NetWeight"]);
                    receiptEntry.txtgetweight = "";

                    if (receiptEntry.cmbvehicletype == "Truck")
                    {
                        receiptEntry.txtcontainerno = Convert.ToString(row["sbno"]);
                        receiptEntry.lblJONo = Convert.ToString(row["SBEntryID"]);
                        receiptEntry.lblContainerNo = "Container No";
                    }
                    else
                    {
                        receiptEntry.txtcontainerno = Convert.ToString(row["ContainerNo"]);
                        receiptEntry.lblJONo = Convert.ToString(row["ID_JONo"]);
                        receiptEntry.txtsize = Convert.ToString(row["size"]);
                        receiptEntry.lblContainerNo = "Container No";
                    }
                    receiptEntry.cmbtranstype = "Second";
                    receiptEntry.txttarewt = Convert.ToString(row["TareWeight"]);
                    receiptEntry.cmbtyres = Convert.ToString(row["NO_OF_TYRES"]);
                    receiptEntry.cmbcontype = Convert.ToString(row["NO_OF_CONT"]);

                    receiptEntries.Add(receiptEntry);
                }

            }


            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult SUb_fetchamt(string Tyres,string VehicleStaus,string GrossWeight,string VehicleType)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WeightmentEntities> receiptEntries = new List<BE.WeightmentEntities>();
            dataTable = db.sub_GetDatatable("SELECT  * FROM  WEIGHTMENT_TARIFF WHERE TYRES_NO='" + Tyres + "'AND VEHICLE_STATUS='" + VehicleStaus + "'AND WEIGHT_FROM  <='" + GrossWeight + "'AND WEIGHT_TO >='"+ GrossWeight + "'AND VEHICLE_TYPE='" + VehicleType + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WeightmentEntities receiptEntry = new BE.WeightmentEntities();
                    receiptEntry.lblamount = Convert.ToString(row["Rate"]);
                    
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult SUb_fetchamts(string cmbtyres, string cmbvehiclestetus, string txtgrosswt, string cmbvehicletype)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.WeightmentEntities> receiptEntries = new List<BE.WeightmentEntities>();
            dataTable = db.sub_GetDatatable("SELECT  * FROM  WEIGHTMENT_TARIFF WHERE TYRES_NO='" + cmbtyres + "'AND VEHICLE_STATUS='" + cmbvehiclestetus + "'AND WEIGHT_FROM  <='" + txtgrosswt + "'AND WEIGHT_TO >='" + txtgrosswt + "'AND VEHICLE_TYPE='" + cmbvehicletype + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.WeightmentEntities receiptEntry = new BE.WeightmentEntities();
                    receiptEntry.lblamount = Convert.ToString(row["Rate"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult UpdateweighmentDetailsForTrailer(string Trailerno)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            List<BE.WeightmentEntities> receiptEntries = new List<BE.WeightmentEntities>();
            dataTable = db.sub_GetDatatable("USP_UpdateWeighmentdetails '"+ Trailerno + "','"+ UserID + "'");
            string message = "";
            return Json(message);
        }

        public JsonResult Save1(BE.WeightmentEntities WeightmentEntities)
        {
            string Message = "";
            int Id = 0;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.

            if (WeightmentEntities.txtslipNo == "NEW")
            {
                string strSql = "SELECT CASE WHEN MAX(ID) IS NULL THEN 1 ELSE MAX(ID) + 1 END FROM Weighment_transactions ";
                DataTable dt1 = db.sub_GetDatatable(strSql);
                if (dt1.Rows.Count > 0)
                {
                    Id = Convert.ToInt32(dt1.Rows[0][0].ToString());
                }

            }
            else
            {
                Id = Convert.ToInt32(WeightmentEntities.txtslipNo);
            }



            //string str = WeightmentEntities.strWorkYear;
            //str = str.Remove(0, 5);

            db.sub_ExecuteNonQuery("USP_INSERT_WEIGHMENT_TRANSACTIONS '" + Id + "','"+ WeightmentEntities.cmbvehicletype + "','"+ WeightmentEntities.cmbvehiclestetus + "','"+ WeightmentEntities.cmbtranstype + "','" + WeightmentEntities.chkIsLowVGN + "','"+ WeightmentEntities.TrailerNo + "','"+ WeightmentEntities.txtcontainerno + "','"+ WeightmentEntities.txttarewt + "','"+ WeightmentEntities.txtpayload + "','"+ WeightmentEntities.txtgrosswt + "','"+ WeightmentEntities.txtvehicletarewt + "','"+ WeightmentEntities.txtnetwt + "','"+ WeightmentEntities.txtKGS + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + WeightmentEntities.lblJONo + "','" +Id+ "','" + WeightmentEntities.txtparty + "','" + WeightmentEntities.txtremarks + "','" + WeightmentEntities.txtreceipt + "','" + WeightmentEntities.strWorkYear +"','" + WeightmentEntities.cmbvehiclestetus + "','" + WeightmentEntities.txtsize + "','" +WeightmentEntities.ddlCycle+ "','" + WeightmentEntities.txtContainerNo2 + "','" + WeightmentEntities.txtTareWeight2 + "','" +WeightmentEntities.cmbtyres + "','" + WeightmentEntities.cmbcontype + "','" + WeightmentEntities.ddlCycle + "'");
            // Master();
            string Messageget = "Record Saved Successfully";
            Message = Messageget + ',' +Id;
            return Json(Message);
        }
        //clp

        public ActionResult ExpCLP()
        {
            return View();
        }

        public JsonResult ExpClpContainerShow(string ContainerNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExpCLP> receiptEntries = new List<BE.ExpCLP>();
            dataTable = db.sub_GetDatatable("USP_Show_Container_Wise_CLP'" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpCLP receiptEntry = new BE.ExpCLP();
                    receiptEntry.lblContainerNoAgency = Convert.ToString(row["Agency Name"]);
                    receiptEntry.lblContainerNoSize = Convert.ToString(row["Size"]);
                    receiptEntry.lblContainerNoentryID = Convert.ToString(row["EntryID"]);
                    receiptEntry.lblContainerNoISOCode = Convert.ToString(row["ISOCode"]);
                    receiptEntry.lblContainerNocontainertype = Convert.ToString(row["containertype"]);
                    receiptEntry.lblShippingBillNo = Convert.ToString(row["SBNumber"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult ExpClpSbNumberShow(string ShippingBillNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExpCLP> receiptEntries = new List<BE.ExpCLP>();
            dataTable = db.sub_GetDatatable("SELECT top 1  SBEntryID, EntryID, (select COUNT(ContainerNo) from exp_stuffing   WHERE SBNumber='" + ShippingBillNo + "' and iscancel=0) as ContainerNo, Size,dbo.Fn_GET_NAME(agencyID, 'EXP Agencies')[Agency Name],movementtype, SBNumber, containertype, viano, POD, linename,ISOCode, sealno FROM exp_stuffing WHERE SBNumber = '" + ShippingBillNo + "' and iscancel = 0");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpCLP receiptEntry = new BE.ExpCLP();
                    receiptEntry.lblAgencySB = Convert.ToString(row["Agency Name"]);
                    receiptEntry.lblMType = Convert.ToString(row["movementtype"]);
                    receiptEntry.lblSBEntryID = Convert.ToString(row["SBEntryID"]);
                    receiptEntry.lblContainer = Convert.ToString(row["ContainerNo"]); 

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult SaveCLp( string lblSBEntryID, string lblContainerNoentryID, string txtContainerNo)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string Message = "";
             

            string strSQL = "";
            Int64 dblgatein = 0;
            Int64 intEntryID = 0;
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable InsertDL = new DataTable();
            strSQL = "";
            strSQL = "SELECT isnull(MAX(CLPNo),0)+1 as[CLPNo] FROM exp_CLP WITH(XLOCK)";
            dt = db.sub_GetDatatable(strSQL);
            if (dt.Rows.Count > 0)
                dblgatein = Convert.ToInt64(dt.Rows[0].Field<Int64>("CLPNo"));
            else
                dblgatein = 1;
            var EntryDate = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
            strSQL = "";
            strSQL = "exec GET_Sp_CLPData '" + lblSBEntryID + "','" + lblContainerNoentryID + "','" + txtContainerNo + "'";
            dt1 = db.sub_GetDatatable(strSQL);

            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                {

                    strSQL = "";
                    strSQL = "INSERT into exp_CLP(CLPNo,EntryDate ,SBEntryID ,SBNo ,SBDate ,ShipperID ,AgentID ,LineName ,CartingDate ,ConsigneeID ,CargoDesc ,TotalQty ,Qty ,PkgType ,Weight ,EntryID ,ContainerNo ,Size ,ContainerType ";
                    strSQL += " ,SealNo ,AgentSeal ,CustomSeal ,ViaNo ,POD ,FPD ,AgencyID ,subagencyname ,AddedBy ,AddedOn ,VesselName ,FOB ,TareWeight ,Shipper ,Consignee ,CHAID ,UNNo ,Voyage ,RotationNo ,CHAName ,AgentName ";
                    strSQL += " ,IsCancel, classno, Cargo_Type ,scan, srno) ";
                    strSQL += "Values(" + dblgatein + ",'" + EntryDate + "','" + dt1.Rows[i].Field<Int64>("SBEntryID") + "','" + dt1.Rows[i].Field<string>("SBNumber") + "','" + Convert.ToDateTime(dt1.Rows[i].Field<DateTime>("SBDate")).ToString("yyyy-MM-dd HH:mm") + "','" + dt1.Rows[i].Field<Int64>("ShipperID") + "',";
                    strSQL += "'" + dt1.Rows[i].Field<Int64>("AGID") + "','" + dt1.Rows[i].Field<string>("linename") + "','" + Convert.ToDateTime(dt1.Rows[i].Field<DateTime>("CartingDate")).ToString("yyyy-MM-dd HH:mm") + "','" + "" + "','" + dt1.Rows[i].Field<string>("CargoDesc") + "',";
                    strSQL += "'" + dt1.Rows[i].Field<Double>("TotalQty") + "','" + dt1.Rows[i].Field<Double>("StuffedQty") + "','" + dt1.Rows[i].Field<string>("QtyUnit") + "','" + dt1.Rows[i].Field<Double>("stuffedweight") + "','" + dt1.Rows[i].Field<Int64>("EntryID") + "',";
                    strSQL += "'" + dt1.Rows[i].Field<string>("ContainerNo") + "','" + dt1.Rows[i].Field<Int16>("Size") + "','" + dt1.Rows[i].Field<string>("ContainerType") + "',";
                    strSQL += "'" + dt1.Rows[i].Field<string>("SealNo") + "','" + dt1.Rows[i].Field<string>("ASealNo") + "','" + dt1.Rows[i].Field<string>("CSealno") + "', '" + dt1.Rows[i].Field<string>("ViaNo") + "','" + dt1.Rows[i].Field<string>("POD") + "' ,'" + dt1.Rows[i].Field<string>("fpd") + "',";
                    strSQL += "'" + dt1.Rows[i].Field<Int64>("AGID") + " ','" + dt1.Rows[i].Field<string>("subagencyname") + "','" + UserID + "','" + EntryDate + "','" + dt1.Rows[i].Field<string>("VesselName") + "','" + dt1.Rows[i].Field<double>("FOB") + "',";
                    strSQL += "'" + dt1.Rows[i].Field<Int64>("TareWeight") + "','" + dt1.Rows[i].Field<string>("ShipperName") + "','" + dt1.Rows[i].Field<string>("Consignee") + "','" + dt1.Rows[i].Field<Int64>("CHAID") + "','" + dt1.Rows[i].Field<string>("UNNo") + "',";
                    strSQL += "'" + dt1.Rows[i].Field<string>("Voyage") + "','" + dt1.Rows[i].Field<string>("RotationNo") + "','" + dt1.Rows[i].Field<string>("CHAName") + "','" + dt1.Rows[i].Field<string>("lineName") + "','" + "0" + "','" + dt1.Rows[i].Field<string>("ClassNo") + "',";
                    strSQL += "'" + dt1.Rows[i].Field<string>("Cargotype") + "','" + dt1.Rows[i].Field<string>("scanstatus") + "'," + dt1.Rows[i].Field<Int64>("srno") + ")";
                    InsertDL = db.sub_GetDatatable(strSQL);


                    Message = "Record Saved Successfully !";

                }
            }

            
            
            return Json(Message);
        }


        public JsonResult GetWeightmentReportDetails(string searchcerteria, string Searchtext, string FromDate, string ToDate)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("Sp_WeighmentSummary '" + FromDate + "','" + ToDate + "','" + searchcerteria + "','" + Searchtext + "'");


            string json = JsonConvert.SerializeObject(dt);
            dt.Columns.Remove("#");
            Session["WeightmentReports"] = dt;

            Session["Fromdate"] = FromDate;
            Session["TOdate"] = ToDate;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelWeightmentReport()
        {

            DataTable dt = (DataTable)Session["WeightmentReports"];
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "Category " + Session["category"] + "   " + "From " + Session["Fromdate"] + "  " + " To " + Session["TOdate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Weightment Report Summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Weightment Report<strong></td></tr>");
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



        public ActionResult WeightmentdetailsPrint(string Slipno)
        {

            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable Weightmentdetails = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("USp_GetWeightmentPrint '" + Slipno + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                Weightmentdetails = getJobOrderSet.Tables[1];

                ViewBag.CompanyAddress = Convert.ToString(tblComDetails.Rows[0]["AddressI"]);
                ViewBag.CompanyAddress2 = Convert.ToString(tblComDetails.Rows[0]["AddressI"]);
                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.CINNO = dr["CINNO"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                }


                foreach (DataRow dr in Weightmentdetails.Rows)
                {
                    ViewBag.VehicleType = dr["VehicleType"];
                    ViewBag.VehicleNo = dr["VehicleNo"];
                    ViewBag.ContainerNo = dr["ContainerNo"];
                    ViewBag.TareWeight = dr["TareWeight"];
                    ViewBag.GrossWeight = dr["GrossWeight"];
                    ViewBag.VehicleTareWT = dr["VehicleTareWT"];
                    ViewBag.NetWeight = dr["NetWeight"];
                    ViewBag.Addedon = dr["Addedon"];
                    ViewBag.Remarks = dr["Remarks"];

                    ViewBag.ReceiptNo = dr["SlipNo"];
                    ViewBag.ContainerNo2 = dr["ContainerNo2"];
                    ViewBag.TareWeight2 = dr["TareWeight2"];

                    ViewBag.diffwt = dr["diffwt"];
                    ViewBag.WeighmentActivity = dr["WeighmentActivity"];
                    ViewBag.oldNetWeight = dr["oldNetWeight"];
                    ViewBag.FullDate = dr["FullDate"];
                    ViewBag.EmptyDate = dr["EmptyDate"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.Grand_Total = dr["Grand_Total"];
                    ViewBag.UserName1 = dr["UserName1"];
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.GSTIN = dr["GSTIN"];
                    ViewBag.PrintDate = dr["Print Date"];
                 
                    ViewBag.Totalweightsum = Convert.ToDouble(dr["TareWeight"]) + Convert.ToDouble(dr["TareWeight2"]);
                    ViewBag.weighment_registationno = dr["weighment_registationno"];

                }
                ViewBag.ImportLoadedTable = Weightmentdetails.AsEnumerable();

            }
            return PartialView();
        }






        //Code By prashant


        public ActionResult EmptyInEntry()
        {
            BE.EmptyInEntities EmptyInList = new BE.EmptyInEntities();
            EmptyInList = reportprovider.GetEmptyInDetails();
            ViewBag.ISOCodeList = new SelectList(EmptyInList.ISOCodesGateIn, "ID", "ISOCode");
            ViewBag.PortList = new SelectList(EmptyInList.Customer, "AGID", "AGName");
            ViewBag.ShipLine = new SelectList(EmptyInList.ShipLines, "SLID", "SLName");
            ViewBag.ContainerType = new SelectList(EmptyInList.ContainerTypeGateIn, "ID", "Type");
            ViewBag.TransporterList = new SelectList(EmptyInList.TransporterGateIn, "ID", "Transporter");
            ViewBag.LocationList = new SelectList(EmptyInList.LocationGateIn, "ID", "Location");
            ViewBag.Condition = new SelectList(EmptyInList.Condition, "ConditionName", "ConditionName");
            ViewBag.Movement = new SelectList(EmptyInList.MovementType, "MovementType", "MovementType");
            ViewBag.Size = new SelectList(EmptyInList.SizeEmpty, "SizeEmpty", "SizeEmpty");
            return View(EmptyInList);


        }

        public ActionResult GetTrailerTransporter(string trailerno)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("Usp_gettrailern '" + trailerno + "'");
            BE.TrailersEntities EmptyInList = new BE.TrailersEntities();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EmptyInList.TransID = Convert.ToInt32(row["transid"]);
                    EmptyInList.OwnedBy = Convert.ToString(row["OwnedBy"]);
                }
            }

            return Json(EmptyInList);
        }
        //public ActionResult BookinNo_Focus(string BookingNo)
        //{
        //    DataTable dt = new DataTable();
        //    CD.DBOperations db = new CD.DBOperations();
        //    dt = db.sub_GetDatatable("Usp_gettrailern '" + trailerno + "'");
        //    BE.TrailersEntities EmptyInList = new BE.TrailersEntities();

        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            EmptyInList.TransID = Convert.ToInt32(row["transid"]);
        //            EmptyInList.OwnedBy = Convert.ToString(row["OwnedBy"]);
        //        }
        //    }

        //    return Json(EmptyInList);
        //}



        public JsonResult SaveEmptyin(List<BE.EmptyInEntities> Containerdetails, string EntryType, string Indate, string TrailerNo, string Trailerid)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string messgage = "";

            DataTable dataTable = new DataTable();


            dataTable.Columns.Add("ISOCODE");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("GrossWeight");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Payload");
            dataTable.Columns.Add("tareWeight");
            dataTable.Columns.Add("Containertype");
            dataTable.Columns.Add("Tranname");
            dataTable.Columns.Add("Condition1");
            dataTable.Columns.Add("Movementtype");
            dataTable.Columns.Add("ShipLine");
            dataTable.Columns.Add("Customer1");
            dataTable.Columns.Add("Source");
            dataTable.Columns.Add("sealNo");
            dataTable.Columns.Add("Remarks");
            dataTable.Columns.Add("allowid");
            dataTable.Columns.Add("BookingNo");
            dataTable.Columns.Add("TransID");
            dataTable.Columns.Add("Custid");

            dataTable.Columns.Add("Lineid");
            dataTable.Columns.Add("LocationID");
            dataTable.Columns.Add("ContainertypeID");
            dataTable.Columns.Add("ISOCODEID");
            // dataTable.TableName = "PT_AddDTADetails";
            foreach (BE.EmptyInEntities item in Containerdetails)
            {
                DataRow row = dataTable.NewRow();
                row["ISOCODE"] = item.ISOCODE;
                row["Location"] = item.Location;
                row["GrossWeight"] = item.GrossWeight;
                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Payload"] = item.Payload;
                row["tareWeight"] = item.tareWeight;
                row["Containertype"] = item.Containertype;
                row["Tranname"] = item.Tranname;
                row["Movementtype"] = item.Movementtype;
                row["ShipLine"] = item.ShipLine;
                row["Customer1"] = item.Customer1;
                row["Source"] = item.Source;
                row["sealNo"] = item.sealNo;
                row["Remarks"] = item.Remarks;
                row["allowid"] = item.allowid;
                row["BookingNo"] = item.BookingNo;
                row["TransID"] = item.TransID;
                row["Custid"] = item.Custid;
                row["Lineid"] = item.Lineid;
                row["LocationID"] = item.LocationID;
                row["ContainertypeID"] = item.ContainertypeID;
                row["ISOCODEID"] = item.ISOCODEID;
                dataTable.Rows.Add(row);
            }



            string strSQL = "";
            Int64 dblgatein = 0;
            Int64 intEntryID = 0;
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            DataTable InsertDL = new DataTable();
            strSQL = "";
            strSQL = "SELECT Isnull(MAX(gateInNo),0)+1 as gateInNo FROM exp_IN";
            dt = db.sub_GetDatatable(strSQL);
            if (dt.Rows.Count > 0)
                dblgatein = Convert.ToInt64(dt.Rows[0].Field<Int64>("gateInNo"));
            else
                dblgatein = 1;

            for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
            {
                dt.Clear();
                strSQL = "SELECT TOP 1 EntryID FROM exp_IN WHERE ContainerNo='" + dataTable.Rows[0].Field<string>("ContainerNo") + "' And Status='P' AND IsCancel=0 ORDER BY EntryID DESC";
                dt = db.sub_GetDatatable(strSQL);
                if (dt.Rows.Count > 0)
                {
                    messgage = "this container is already IN. please go to Export admin for editing if any!";

                }

                if (messgage == "")
                {
                    strSQL = "";
                    strSQL = "SELECT Isnull(MAX(entryID),0)+1 as EntryID FROM exp_IN";
                    dt = db.sub_GetDatatable(strSQL);
                    if (dt.Rows.Count > 0)

                        intEntryID = Convert.ToInt64(dt.Rows[0].Field<Int64>("EntryID"));

                    else
                        intEntryID = 1;
                    strSQL = "";
                    strSQL = "Insert Into exp_IN(entryID,agencyID,containerNo,LocationID,Size,IsocodeID,GrossWeight,TareWeight,PayLoad,containertypeid,trailerno,trailerID,MType,"; 
                     strSQL += " TransID,condition,LineID,Source,SealNo,BookingNo,indate,remarks,status,addedby,addedon,EntryType, pickuplocation,gateinallowID, gateInNo) ";
                    strSQL += "Values(" + intEntryID + "," + dataTable.Rows[i].Field<string>("Custid") + ",'" + dataTable.Rows[i].Field<string>("ContainerNo") + "','" + dataTable.Rows[i].Field<string>("LocationID") + "','" + dataTable.Rows[i].Field<string>("Size") + "','" + dataTable.Rows[i].Field<string>("ISOCODEID") + "','" + dataTable.Rows[i].Field<string>("GrossWeight") + "','" + dataTable.Rows[i].Field<string>("tareWeight") + "',";
                    strSQL += "'" + dataTable.Rows[i].Field<string>("Payload") + "','" + dataTable.Rows[i].Field<string>("ContainertypeID") + "','" + Trailerid + "','" + TrailerNo + "','" + dataTable.Rows[i].Field<string>("Movementtype") + "',";
                    strSQL += "'" + dataTable.Rows[i].Field<string>("TransID") + "','" + dataTable.Rows[i].Field<string>("Condition1") + "','" + dataTable.Rows[i].Field<string>("Lineid") + "','" + dataTable.Rows[i].Field<string>("Source") + "','" + dataTable.Rows[i].Field<string>("sealNo") + "',";
                    strSQL += "'" + dataTable.Rows[i].Field<string>("BookingNo") + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd MMM yyyy HH:mm") + "','" + dataTable.Rows[i].Field<string>("Remarks") + "','P'," + UserID + ",'" + Convert.ToDateTime(DateTime.Now).ToString("dd MMM yyyy HH:mm") + "','" + EntryType + "', '" + "NHS" + "','" + dataTable.Rows[i].Field<string>("allowid") + "' ," + dblgatein + ")";
                    InsertDL = db.sub_GetDatatable(strSQL);

                    messgage = "Record Saved Successfully !";

                }
            }
            return Json(messgage);
        }


        public ActionResult GetEMptyInsummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("sp_GetEmptyInDetsNew '" + FromDate + "','" + ToDate + "'");
            Session["getEmptyIndetails"] = dt;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelEmptyIN()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["getEmptyIndetails"];
            //DataTable dt = (DataTable)ViewData["VoucherDetails"];
            string Tittle = "From " + Session["FromDate"] + " To " + Session["ToDate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=EmptyinDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Empty In Details Summary<strong></td></tr>");
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

        public ActionResult GetContainerWisePrint(string Containerno)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_GetContainerNoForPrint '" + Containerno + "'");

            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult PrintEmptyIndetails(string Entryid)
        {
            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getInDL = db.sub_GetDatatable("USP_GetEmptyInPrintdetails '" + Entryid + "'");

            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {
                    ViewBag.entryID = dr["entryID"];
                    ViewBag.containerNo = dr["containerNo"];
                    ViewBag.size = dr["size"];
                    ViewBag.trailerno = dr["trailerno"];
                    ViewBag.indate = dr["indate"];
                    ViewBag.remarks = dr["remarks"];
                    ViewBag.EntryType = dr["EntryType"];
                    ViewBag.SealNo = dr["SealNo"];
                    ViewBag.BookingNo = dr["BookingNo"];
                    ViewBag.TareWeight = dr["TareWeight"];
                    ViewBag.ContainerType = dr["ContainerType"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.AGName = dr["AGName"];
                    ViewBag.SLName = dr["SLName"];
                    ViewBag.TransName = dr["TransName"];
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.Con_For = dr["Con_For"];




                    ViewBag.PrintDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                    ViewBag.PrintTime = Convert.ToDateTime(DateTime.Now).ToString("HH:mm:ss");

                    ViewBag.ImportloadedDetails = getInDL.AsEnumerable();
                }
            }
            return PartialView();
        }


        public ActionResult ExportFactoryIn()
        {
            BE.EmptyInEntities EmptyInList = new BE.EmptyInEntities();
            EmptyInList = reportprovider.GetEmptyInDetails();
            ViewBag.ISOCodeList = new SelectList(EmptyInList.ISOCodesGateIn, "ID", "ISOCode");
            ViewBag.PortList = new SelectList(EmptyInList.Customer, "AGID", "AGName");
            ViewBag.ShipLine = new SelectList(EmptyInList.ShipLines, "SLID", "SLName");
            ViewBag.ContainerType = new SelectList(EmptyInList.ContainerTypeGateIn, "ID", "Type");
            ViewBag.TransporterList = new SelectList(EmptyInList.TransporterGateIn, "ID", "Transporter");
            ViewBag.LocationList = new SelectList(EmptyInList.LocationGateIn, "ID", "Location");
            ViewBag.Condition = new SelectList(EmptyInList.Condition, "ConditionName", "ConditionName");
            ViewBag.Movement = new SelectList(EmptyInList.MovementType, "MovementType", "MovementType");
            ViewBag.Size = new SelectList(EmptyInList.SizeEmpty, "SizeEmpty", "SizeEmpty");
            return View(EmptyInList);


        }


        public JsonResult SaveExportfactoryLoaded(List<BE.EmptyInEntities> Containerdetails, string EntryType, string Indate)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string messgage = "";

            DataTable dataTable = new DataTable();


            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Entrytype");
            dataTable.Columns.Add("BookingNo");
            dataTable.Columns.Add("TrailerNo");
            dataTable.Columns.Add("Tranname");
            dataTable.Columns.Add("DriverNo");
            dataTable.Columns.Add("ISOCODE");
            dataTable.Columns.Add("AgentSealNO");
            dataTable.Columns.Add("Condition1");
            dataTable.Columns.Add("ReportingTime");
            dataTable.Columns.Add("FactoryInTime");
            dataTable.Columns.Add("FactoryOut");
            dataTable.Columns.Add("Remarks");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Containertype");
            dataTable.Columns.Add("ShipLine");
            dataTable.Columns.Add("Customer1");
            dataTable.Columns.Add("SealNo");
            dataTable.Columns.Add("GrossWeight");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("tareWeight");

            dataTable.Columns.Add("LocationID");
            dataTable.Columns.Add("Lineid");
            dataTable.Columns.Add("Custid");
            dataTable.Columns.Add("TransID");
            dataTable.Columns.Add("ISOIDCode");
            dataTable.Columns.Add("ContainertypeID");
            // dataTable.TableName = "PT_AddDTADetails";
            foreach (BE.EmptyInEntities item in Containerdetails)
            {
                DataRow row = dataTable.NewRow();
                row["ContainerNo"] = item.ContainerNo;
                row["Entrytype"] = item.Entrytype;
                row["BookingNo"] = item.BookingNo;
                row["TrailerNo"] = item.TrailerNo;
                row["Tranname"] = item.Tranname;
                row["DriverNo"] = item.DriverNo;
                row["ISOCODE"] = item.ISOCODE;
                row["AgentSealNO"] = item.AgentSealNO;
                row["Condition1"] = item.Condition1;
                row["ReportingTime"] = item.ReportingTime;
                row["FactoryInTime"] = item.FactoryInTime;
                row["FactoryOut"] = item.FactoryOut;
                row["Remarks"] = item.Remarks;
                row["Size"] = item.Size;
                row["Containertype"] = item.Containertype;
                row["ShipLine"] = item.ShipLine;
                row["Customer1"] = item.Customer1;
                row["sealNo"] = item.sealNo;
                row["GrossWeight"] = item.GrossWeight;
                row["Location"] = item.Location;
                row["tareWeight"] = item.tareWeight;
                row["Remarks"] = item.Remarks;
                row["LocationID"] = item.LocationID;
                row["Lineid"] = item.Lineid;
                row["Custid"] = item.Custid;
                row["TransID"] = item.TransID;
                row["ISOIDCode"] = item.ISOIDCode;
                row["ContainertypeID"] = item.ContainertypeID;
                dataTable.Rows.Add(row);
            }
            string strSQL = "";
            string StrEntrytype = "";
            Int64 dblgatein = 0;
            Int64 intEntryID = 0;
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            DataTable InsertDL = new DataTable();
            strSQL = "";
            strSQL = "SELECT Isnull(MAX(gateInNo),0)+1 as gateInNo FROM exp_IN";
            dt = db.sub_GetDatatable(strSQL);
            if (dt.Rows.Count > 0)
                dblgatein = Convert.ToInt64(dt.Rows[0].Field<Int64>("gateInNo"));
            else
                dblgatein = 1;

            for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
            {
                dt.Clear();
                strSQL = "SELECT TOP 1 EntryID FROM exp_IN WHERE ContainerNo='" + dataTable.Rows[0].Field<string>("ContainerNo") + "' And Status='P' AND IsCancel=0 ORDER BY EntryID DESC";
                dt = db.sub_GetDatatable(strSQL);
                if (dt.Rows.Count > 0)
                {
                    messgage = "this container is already IN. please go to Export admin for editing if any!";

                }



                if (EntryType == "Factory Stuffed")
                {
                    StrEntrytype = "F";
                }
                else
                {
                    StrEntrytype = "D";
                }

                if (messgage == "")
                {
                    strSQL = "";
                    strSQL = "SELECT Isnull(MAX(entryID),0)+1 as EntryID FROM exp_IN";
                    dt = db.sub_GetDatatable(strSQL);
                    if (dt.Rows.Count > 0)

                        intEntryID = Convert.ToInt64(dt.Rows[0].Field<Int64>("EntryID"));

                    else
                        intEntryID = 1;
                    strSQL = "";
                    strSQL = "USP_INSERT_EXP_IN '" + dataTable.Rows[i].Field<string>("ContainerNo") + "','" + dataTable.Rows[i].Field<string>("Entrytype") + "','" + dataTable.Rows[i].Field<string>("BookingNo") + "','" + dataTable.Rows[i].Field<string>("TrailerNo") + "','" + dataTable.Rows[i].Field<string>("TransID") + "','" + dataTable.Rows[i].Field<string>("DriverNo") + "',";
                    strSQL += "'" + dataTable.Rows[i].Field<string>("ISOIDCode") + "','" + dataTable.Rows[i].Field<string>("AgentSealNO") + "','" + dataTable.Rows[i].Field<string>("Condition1") + "','" + dataTable.Rows[i].Field<string>("ReportingTime") + "','" + dataTable.Rows[i].Field<string>("FactoryInTime") + "','" + dataTable.Rows[i].Field<string>("FactoryOut") + "','" + dataTable.Rows[i].Field<string>("Remarks") + "','";
                    strSQL += "" + dataTable.Rows[i].Field<string>("Size") + "','" + dataTable.Rows[i].Field<string>("ContainerTypeid") + "','" + dataTable.Rows[i].Field<string>("Lineid") + "','" + dataTable.Rows[i].Field<string>("Custid") + "','" + dataTable.Rows[i].Field<string>("sealNo") + "','" + dataTable.Rows[i].Field<string>("GrossWeight") + "','" + dataTable.Rows[i].Field<string>("LocationID") + "','" + StrEntrytype + "','" + UserID + "','" + dataTable.Rows[i].Field<string>("tareWeight") + "'," + intEntryID + "";

                    InsertDL = db.sub_GetDatatable(strSQL);

                    messgage = "Record Saved Successfully !";

                }
            }
            return Json(messgage);
        }

        public ActionResult GetContainerFactoryWisePrint(string Containerno)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_GetFactoryContainerNoForPrint '" + Containerno + "'");

            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult PrintExportFactoryIndetails(string Entryid)
        {
            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getInDL = db.sub_GetDatatable("USP_GetExportfactoryPrintdetails '" + Entryid + "'");

            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {
                    ViewBag.entryID = dr["entryID"];
                    ViewBag.containerNo = dr["containerNo"];
                    ViewBag.size = dr["size"];
                    ViewBag.trailerno = dr["trailerno"];
                    ViewBag.indate = dr["indate"];
                    ViewBag.remarks = dr["remarks"];
                    ViewBag.EntryType = dr["EntryType"];
                    ViewBag.SealNo = dr["SealNo"];
                    ViewBag.AgentSeal = dr["AgentSeal"];

                    ViewBag.ContainerType = dr["ContainerType"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.AGName = dr["AGName"];
                    ViewBag.SLName = dr["SLName"];
                    ViewBag.TransName = dr["TransName"];
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.Con_For = dr["Con_For"];




                    ViewBag.PrintDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                    ViewBag.PrintTime = Convert.ToDateTime(DateTime.Now).ToString("HH:mm:ss");

                    ViewBag.ImportloadedDetails = getInDL.AsEnumerable();
                }
            }
            return PartialView();
        }



        //end code by prashant

        //code by Sonal

        public ActionResult ExportbttcargoJO()
        {
            return View();
        }
        public ActionResult ExportbttcargoJOSummary()
        {
            return View();
        }

        public ActionResult GetJOBttCargoDetailsbySBNO(string SBNo)
        {
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            DataTable dataTable = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dataTable = db.sub_GetDatatable("USP_GetJOBttCargoDetailsbySBNO'" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt receiptEntry = new BE.ExportShippingBillEnt();
                    receiptEntry.AGName = Convert.ToString(row["Agency Name"]);
                    receiptEntry.CargoDesc = Convert.ToString(row["CargoDescription"]);
                    receiptEntry.cargowt = Convert.ToInt32(row["CartingWT"]);
                    receiptEntry.carQty = Convert.ToInt32(row["CartingQty"]);
                    receiptEntry.StuffQty = Convert.ToInt32(row["StuffQty"]);
                    receiptEntry.StuffWT = Convert.ToInt32(row["StuffWT"]);
                    receiptEntry.BttQty = Convert.ToInt32(row["BttQty"]);
                    receiptEntry.Type = Convert.ToString(row["Type"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["EntryID"]);
                    receiptEntry.balanceqty = Convert.ToInt32(row["balanceqty"]);
                    receiptEntry.balancewt = Convert.ToInt32(row["balancewt"]);
                    receiptEntry.BttWt = Convert.ToInt32(row["bttwt"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetExportbttcargoJOSummary(string FromDate, string ToDate)
        {
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            DataTable dataTable = new DataTable();


            CD.DBOperations db = new CD.DBOperations();
            dataTable = db.sub_GetDatatable("USP_GetExportbttcargoJOSummary'" + FromDate + "','" + ToDate + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt receiptEntry = new BE.ExportShippingBillEnt();
                    receiptEntry.JONO = Convert.ToString(row["JONO"]);
                    receiptEntry.SrNo = Convert.ToInt32(row["SrNo"]);
                    receiptEntry.JODate = Convert.ToString(row["JODate"]);
                    receiptEntry.SBNO = Convert.ToString(row["SBNo"]);

                    receiptEntry.BttArea = Convert.ToInt32(row["bttarea"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult SaveBTTCARGO(BE.ExportShippingBillEnt BttcargoJOData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.SaveBTTCARGO(BttcargoJOData, Userid);
            return Json(message);
        }


        [HttpPost]
        public JsonResult GetVehiclesBTT(string SBNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            dataTable = db.sub_GetDatatable("GET_VEHICLE_LIST '" + SBNo + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt receiptEntry = new BE.ExportShippingBillEnt();

                    receiptEntry.VehicleNo = Convert.ToString(row["VehicleNo"]);

                    receiptEntries.Add(receiptEntry);
                }
            }

            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        /*****************************************************ExpCargoBTTGP*******************************************/

        public ActionResult ExpCargoBTTGP()
        {
            return View();
        }

        public ActionResult ExpCargoBTTGPSummary(string FromDate, string ToDate)
        {
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            DataTable dataTable = new DataTable();

            //string Fromdate = Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss");
            //string Todate = Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss");
            CD.DBOperations db = new CD.DBOperations();
            dataTable = db.sub_GetDatatable("USP_GetExportbttcargoJOSummary'" + FromDate + "','" + ToDate + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt receiptEntry = new BE.ExportShippingBillEnt();
                    receiptEntry.JONO = Convert.ToString(row["JONO"]);
                    receiptEntry.SrNo = Convert.ToInt32(row["SrNo"]);
                    receiptEntry.JODate = Convert.ToString(row["JODate"]);
                    receiptEntry.SBNO = Convert.ToString(row["SBNo"]);
                    //receiptEntry.carQty = Convert.ToInt32(row["CartingQty"]);
                    //receiptEntry.StuffQty = Convert.ToInt32(row["StuffQty"]);
                    //receiptEntry.StuffWT = Convert.ToInt32(row["StuffWT"]);
                    //receiptEntry.BttQty = Convert.ToInt32(row["BttQty"]);
                    //receiptEntry.Type = Convert.ToString(row["Type"]);
                    receiptEntry.BttArea = Convert.ToInt32(row["bttarea"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetJOBttCargoGPDetailsbySBNO(string SBNo)
        {
            List<BE.GatePassEntities> receiptEntries = new List<BE.GatePassEntities>();
            DataTable dataTable = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dataTable = db.sub_GetDatatable("USP_GetJOBttCargoGPDetailsbySBNO'" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.GatePassEntities receiptEntry = new BE.GatePassEntities();

                    receiptEntry.CargoDesc = Convert.ToString(row["CargoDescription"]);
                    receiptEntry.TrailerNo = Convert.ToString(row["TrailerNo"]);
                    receiptEntry.BTTWT = Convert.ToDecimal(row["BTTWT"]);
                    receiptEntry.BTTQty = Convert.ToDecimal(row["BTTQty"]);
                    receiptEntry.SBEntryID = Convert.ToInt32(row["SBEntryID"]);
                    receiptEntry.BalanceQty = Convert.ToInt32(row["BalanceQty"]);
                    receiptEntry.BalanceWT = Convert.ToInt32(row["BalanceWT"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult SaveExpCargoGP(BE.GatePassEntities BttcargoGPData, List<BE.GatePassEntities> GPDetailsData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            DataTable GPElementsdataTable = new DataTable();
            GPElementsdataTable.Columns.Add("SBEntryID");
            GPElementsdataTable.Columns.Add("SBNo");
            GPElementsdataTable.Columns.Add("TrailerNo");
            GPElementsdataTable.Columns.Add("BTTQty");
            GPElementsdataTable.Columns.Add("BTTWT");


            GPElementsdataTable.TableName = "PT_SaveExpGPDetails";
            if (GPDetailsData != null)
            {
                foreach (BE.GatePassEntities element in GPDetailsData)
                {
                    DataRow row = GPElementsdataTable.NewRow();
                    row["SBEntryID"] = element.SBEntryID;
                    row["SBNo"] = element.SBNo;
                    row["TrailerNo"] = element.TrailerNo;
                    row["BTTQty"] = element.BTTQty;
                    row["BTTWT"] = element.BTTWT;


                    GPElementsdataTable.Rows.Add(row);
                }
            }

            string message = reportprovider.SaveExpCargoGP(BttcargoGPData, GPElementsdataTable, Userid);
            return Json(message);
        }

        public ActionResult ExportbttcargoGPSummary()
        {
            return View();

        }

        public ActionResult GetExportbttcargoGPSummary(string FromDate, string ToDate, string Criteria, string SearchText)
        {
            List<BE.GatePassEntities> receiptEntries = new List<BE.GatePassEntities>();
            DataTable dataTable = new DataTable();


            CD.DBOperations db = new CD.DBOperations();
            dataTable = db.sub_GetDatatable("USP_GetExportbttcargoGPSummary'" + FromDate + "','" + ToDate + "','" + Criteria + "','" + SearchText + "'");
            if (dataTable != null)
            {
                //int i = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    // i++;
                    BE.GatePassEntities receiptEntry = new BE.GatePassEntities();
                    // receiptEntry.SrNo = i;

                    receiptEntry.GPNO = Convert.ToInt32(row["GPNO"]);
                    receiptEntry.GPDate = Convert.ToString(row["GPDate"]);
                    receiptEntry.SBNo = Convert.ToString(row["SBNo"]);
                    receiptEntry.TrailerNo = Convert.ToString(row["TrailerNo"]);
                    receiptEntry.BTTWT = Convert.ToDecimal(row["BTTWT"]);
                    receiptEntry.BTTQty = Convert.ToDecimal(row["BTTQty"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult PrintExportBTTCargoGatePass(string GPNO)
        {

            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getInDL = db.sub_GetDatatable("USP_Export_BTT_Cargo_Gatepass_Print '" + GPNO + "'");

            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.GPNO = dr["BTTID"];
                    ViewBag.GPDate = dr["BTTDate"];
                    ViewBag.AGName = dr["customer"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.CargoDesc = dr["CargoDesc"];
                    ViewBag.PrintDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                    ViewBag.PrintTime = Convert.ToDateTime(DateTime.Now).ToString("HH:mm:ss");

                    ViewBag.ImportloadedDetails = getInDL.AsEnumerable();
                }
            }

            return PartialView();
        }
        /********************************************* ExportMovementAcceptCTR *****************************************/

        public ActionResult ExportMovementAcceptCTR()
        {
            return View();

        }

        public ActionResult GetMovementAccCTRDetailsbyContainnerNo(string ContainnerNo)
        {
            List<BE.MovementAcceptCTR> receiptEntries = new List<BE.MovementAcceptCTR>();
            DataTable dataTable = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dataTable = db.sub_GetDatatable("USP_SHOWmovementaccept'" + ContainnerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.MovementAcceptCTR receiptEntry = new BE.MovementAcceptCTR();
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    //receiptEntry.SBNo = Convert.ToString(row["TrailerNo"]);
                    //receiptEntry.AGName = Convert.ToString(row["AGName"]);
                    receiptEntry.SealNo = Convert.ToString(row["SealNo"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["entryID"]);
                    receiptEntry.AgencyID = Convert.ToInt32(row["agencyID"]);
                    receiptEntry.AgentSeal = Convert.ToString(row["AgentSeal"]);
                    receiptEntry.PODName = Convert.ToString(row["PODName"]);
                    receiptEntry.Shippername = Convert.ToString(row["shippername"]);
                    receiptEntry.CHAName = Convert.ToString(row["CHAName"]);
                    receiptEntry.SLName = Convert.ToString(row["SLName"]);
                    receiptEntry.Cargotype = Convert.ToString(row["Cargotype"]);
                    receiptEntry.Viano = Convert.ToString(row["viano"]);
                    receiptEntry.LEONo = Convert.ToString(row["LEONo"]);
                    receiptEntry.LEODate = Convert.ToString(row["Leodate"]);
                    receiptEntry.TransportType = Convert.ToString(row["Road"]);
                    receiptEntry.OnAccount = Convert.ToString(row["NA"]);
                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult SaveMovementAccCTR(BE.MovementAcceptCTR MovementCTRData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.SaveMovementAccCTR(MovementCTRData, Userid);
            return Json(message);
        }

        /************************************************** Supplier Bill Wise *****************************************/
        public ActionResult SBWiseMovementAcceptEntry()
        {
            return View();
        }

        public ActionResult GetMovementAccDetailsbySBNO(string SBNo)
        {
            List<BE.MovementAcceptCTR> MovementAccCTRItemList = new List<BE.MovementAcceptCTR>();
            DataSet datatable = new DataSet();
            DataTable Master = new DataTable();
            DataTable Details = new DataTable();


            CD.DBOperations db = new CD.DBOperations();
            datatable = db.sub_GetDataSets("USP_ShowSBWiseMovementAccept '" + SBNo + "'");
            Master = datatable.Tables[0];
            Details = datatable.Tables[1];

            BE.MovementAcceptCTR MovementAcceptCTRData = new BE.MovementAcceptCTR();


            if (Master != null)
            {
                foreach (DataRow row in Master.Rows)
                {
                    MovementAcceptCTRData.Size = Convert.ToInt32(row["Size"]);
                    MovementAcceptCTRData.AgencyID = Convert.ToInt32(row["AgencyID"]);
                    MovementAcceptCTRData.AgentName = Convert.ToString(row["Customer Name"]);
                    MovementAcceptCTRData.Shippername = Convert.ToString(row["Shipper Name"]);
                    MovementAcceptCTRData.SLName = Convert.ToString(row["Shipping Line"]);
                    MovementAcceptCTRData.CHAName = Convert.ToString(row["CHA Name"]);
                    MovementAcceptCTRData.Cargotype = Convert.ToString(row["Cargo type"]);
                    MovementAcceptCTRData.SealNo = Convert.ToString(row["Seal No"]);
                    MovementAcceptCTRData.AgentSeal = Convert.ToString(row["Agent Seal"]); // custom seal
                    MovementAcceptCTRData.EntryID = Convert.ToInt32(row["entryID"]); // custom seal
                    MovementAcceptCTRData.Viano = Convert.ToString(row["viano"]); // custom seal
                    MovementAcceptCTRData.PODName = Convert.ToString(row["POD Name"]); // custom seal

                }
            }

            if (Details != null)
            {
                int i = 0;
                foreach (DataRow row in Details.Rows)
                {
                    i++;
                    BE.MovementAcceptCTR receiptEntry = new BE.MovementAcceptCTR();
                    receiptEntry.SrNo = i;
                    receiptEntry.Select = Convert.ToBoolean(row["Select"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["Container No"]);
                    receiptEntry.Size = Convert.ToInt32(row["Size"]);
                    receiptEntry.AgencyID = Convert.ToInt32(row["AgencyID"]);
                    receiptEntry.AgentName = Convert.ToString(row["Customer Name"]);
                    receiptEntry.Shippername = Convert.ToString(row["Shipper Name"]);
                    receiptEntry.SLName = Convert.ToString(row["Shipping Line"]);
                    receiptEntry.CHAName = Convert.ToString(row["CHA Name"]);
                    receiptEntry.Cargotype = Convert.ToString(row["Cargo type"]);
                    receiptEntry.SealNo = Convert.ToString(row["Seal No"]);
                    receiptEntry.AgentSeal = Convert.ToString(row["Agent Seal"]); // custom seal
                    receiptEntry.EntryID = Convert.ToInt32(row["entryID"]); // custom seal
                    //receiptEntry.Viano = Convert.ToString(row["viano"]); // custom seal
                    receiptEntry.PODName = Convert.ToString(row["POD Name"]); // custom seal


                    MovementAccCTRItemList.Add(receiptEntry);
                }
            }


            BE.MovementAcceptCTRDetailsData SBCTRData = new BE.MovementAcceptCTRDetailsData();

            SBCTRData.MovementAcceptCTRData = MovementAcceptCTRData;
            SBCTRData.MovementAccCTRItemList = MovementAccCTRItemList;


            var returnField = new { SBCTRData = SBCTRData };
            var jsonResult = Json(returnField, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }

        public ActionResult GetAcceptedMovementCountSBNO(string ContainerNos)
        {
            string Status = "";
            DataTable dataTable = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dataTable = db.sub_GetDatatable("USP_GetAcceptedMovements'" + ContainerNos + "'");

            if (dataTable.Rows.Count > 0)
            {
                Status = Convert.ToString(dataTable.Rows[0][0].ToString());
            }

            var jsonResult = Json(Status, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult SaveSBWiseMovementAcceptEntry(BE.MovementAcceptCTR SBWiseMovementAccData, List<BE.MovementAcceptCTR> SBCTRDetailsList)
        {

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            DataTable SBElementsdataTable = new DataTable();
            SBElementsdataTable.Columns.Add("AgencyID");
            SBElementsdataTable.Columns.Add("EntryID");
            SBElementsdataTable.Columns.Add("ContainerNo");
            SBElementsdataTable.Columns.Add("Size");
            SBElementsdataTable.Columns.Add("AgentSeal");
            SBElementsdataTable.Columns.Add("SealNo");
            SBElementsdataTable.Columns.Add("Viano");


            SBElementsdataTable.TableName = "PT_SaveSBWiseMovement";
            if (SBCTRDetailsList != null)
            {
                foreach (BE.MovementAcceptCTR element in SBCTRDetailsList)
                {
                    DataRow row = SBElementsdataTable.NewRow();
                    row["AgencyID"] = element.AgencyID;
                    row["EntryID"] = element.EntryID;
                    row["ContainerNo"] = element.ContainerNo;
                    row["Size"] = element.Size;
                    row["AgentSeal"] = element.AgentSeal;
                    row["SealNo"] = element.SealNo;
                    row["Viano"] = element.Viano;


                    SBElementsdataTable.Rows.Add(row);
                }
            }

            string message = reportprovider.SaveSBWiseMovementAcceptEntry(SBWiseMovementAccData, SBElementsdataTable, Userid);
            return Json(message);

        }




        // Code for the export tariff details

        public ActionResult ExportAccountMaster()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<BE.ExpHeadMasterEnt> InvoiceType = new List<BE.ExpHeadMasterEnt>();
            InvoiceType = reportprovider.InvoiceTypeDDL();
            ViewBag.InvoiceDDL = new SelectList(InvoiceType, "InvTId", "InvType");

            List<BE.ExpHeadMasterEnt> HSNSelect = new List<BE.ExpHeadMasterEnt>();
            HSNSelect = reportprovider.HSNCodeDDL();
            ViewBag.HSNDDList = new SelectList(HSNSelect, "HSNID", "HSNCodeL");

            List<BE.ExpHeadMasterEnt> TaxName = new List<BE.ExpHeadMasterEnt>();
            TaxName = reportprovider.getTaxName();
            ViewBag.TaxName = new SelectList(TaxName, "TaxID", "TaxName");

            List<BE.ExpHeadMasterEnt> IMPGroup = new List<BE.ExpHeadMasterEnt>();
            IMPGroup = reportprovider.IMPGroupDDl();
            ViewBag.importg = new SelectList(IMPGroup, "IMPGID", "IMPGName");
            return View();

        }

        public JsonResult ChecktheExportAccountmasterAlready(BE.ExpHeadMasterEnt ExpHeadMasterEnt)
        {
            string message = "";
            var EntryDate = ExpHeadMasterEnt.EntryDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            DataTable SvaDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            SvaDT = db.sub_GetDatatable("SP_SaveExportAccountMaster_Check_EXISTS '" + ExpHeadMasterEnt.EntryID + "','" + ExpHeadMasterEnt.AcName + "','" + ExpHeadMasterEnt.TallyName + "','" + ExpHeadMasterEnt.disp + "','" + ExpHeadMasterEnt.IsActive + "','" + ExpHeadMasterEnt.IMPGID + "','" + ExpHeadMasterEnt.HSNCodeL + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + ExpHeadMasterEnt.InvTId + "','" + ExpHeadMasterEnt.TaxID + "'");

            if (SvaDT.Rows.Count > 0)
            {
                message = Convert.ToString(SvaDT.Rows[0]["Message"]);
            }

            return Json(message);
        }

        public JsonResult SaveExportAccountmaster(BE.ExpHeadMasterEnt ExpHeadMasterEnt)
        {
            string message = "";
            string strSQL = "";
            Int64 intEntryID = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            var EntryDate = ExpHeadMasterEnt.EntryDate;

            DataTable SvaDT = new DataTable();
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();



            if (ExpHeadMasterEnt.EntryID == 0)
            {
                strSQL = "";
                strSQL = "select ISNULL(MAX(AccountID),0) as maxID from exp_accountmaster with(xlock)";
                dt = db.sub_GetDatatable(strSQL);
                if (dt.Rows.Count > 0)
                {
                    intEntryID = Convert.ToInt64(dt.Rows[0].Field<Int64>("maxID") + 1);
                }
                else
                {
                    intEntryID = 1;
                }


                SvaDT = db.sub_GetDatatable("SP_SUExportAccountmaster '" + intEntryID + "','" + ExpHeadMasterEnt.AcName + "','" + ExpHeadMasterEnt.TallyName + "','" + ExpHeadMasterEnt.IMPGID + "','" + ExpHeadMasterEnt.HSNCodeL + "','" + ExpHeadMasterEnt.disp + "','" + ExpHeadMasterEnt.IsActive + "','" + UserID + "','" + ExpHeadMasterEnt.InvTId + "','" + ExpHeadMasterEnt.TaxID + "'");

            }
            else
            {
                SvaDT = db.sub_GetDatatable("SP_SUExportAccountmaster '" + ExpHeadMasterEnt.EntryID + "','" + ExpHeadMasterEnt.AcName + "','" + ExpHeadMasterEnt.TallyName + "','" + ExpHeadMasterEnt.IMPGID + "','" + ExpHeadMasterEnt.HSNCodeL + "','" + ExpHeadMasterEnt.disp + "','" + ExpHeadMasterEnt.IsActive + "','" + UserID + "','" + ExpHeadMasterEnt.InvTId + "','" + ExpHeadMasterEnt.TaxID + "'");

            }
            if (SvaDT.Rows.Count > 0)
            {
                message = Convert.ToString(SvaDT.Rows[0]["Message"]);
            }

            return Json(message);
        }

        public JsonResult GetExportAcList(string search)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_SearchExportAccountDetails'" + search + "'");

            var summaryDet = JsonConvert.SerializeObject(dt);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("EDIT");
            }

            Session["SearchAccountDetails"] = dt;
            Session["SearchAccountDetailssearch"] = search;
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }




        public ActionResult ExportTariffMaster()
        {



            List<BE.exporterShipping> ShippingLine = new List<BE.exporterShipping>();
            ShippingLine = reportprovider.GetExportShippingDetails();
            ViewBag.ShippingLineList = new SelectList(ShippingLine, "Exportershippingid", "ExporterShippingname");
            List<BE.CHA> CHA = new List<BE.CHA>();
            CHA = trackerProvider.getCHA();
            ViewBag.CHA = new SelectList(CHA, "CHANo", "CHAName");

            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = trackerProvider.GetCustomer();
            ViewBag.CustomerList = new SelectList(Customer, "AGID", "AGName");

            List<BE.Consignee> Consignee = new List<BE.Consignee>();
            Consignee = trackerProvider.GetImporter();
            ViewBag.Consignee = new SelectList(Consignee, "ImporterID", "ImporterName");

            List<BE.TariffGroup> TariffGroup = new List<BE.TariffGroup>();
            TariffGroup = trackerProvider.GettaiffGroup();
            ViewBag.TariffGroup = new SelectList(TariffGroup, "Group_ID", "Group_Name");


            List<BE.importtariffdetails> importtariffdetails = new List<BE.importtariffdetails>();
            importtariffdetails = trackerProvider.Getimporttariffdetails();
            ViewBag.importtariffdetails = new SelectList(importtariffdetails, "TariffID", "TariffDescription");


            List<BE.ContainerType> ContainerType = new List<BE.ContainerType>();
            ContainerType = trackerProvider.GetContainerType();
            ViewBag.ContainerType = new SelectList(ContainerType, "ContainerTypeID", "ContainerTypeName");



            List<BE.ImportAccountMaster> AccountHead = new List<BE.ImportAccountMaster>();
            AccountHead = trackerProvider.GetAccountHead();
            ViewBag.AccountHead = new SelectList(AccountHead, "AccountID", "AccountName");

            return View();
        }

        public ActionResult SaveExportTariffmaster(string TariffID, string txtDescription, string ddlshippingline, int ddlCHA, string ddlcustomer, string txtFromDate, string txtToDate, string txtday,
            string StorageDay, string EmptyDay, int isactive)

        {
            string message = "";
            string strSQL = "";
            Int64 intEntryID = 0;
            DataTable dt = new DataTable();
            DataTable SvaDT = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);

            if (TariffID == "")
            {
                strSQL = "";
                strSQL = "select ISNULL(MAX(TariffID),0) as TariffID from exp_tariffmaster with(xlock)";
                dt = db.sub_GetDatatable(strSQL);
                if (dt.Rows.Count > 0)
                {
                    intEntryID = Convert.ToInt64(dt.Rows[0].Field<Int64>("TariffID") + 1);
                }
                else
                {
                    intEntryID = 1;
                }


                SvaDT = db.sub_GetDatatable("exec SP_SUExpTariffmaster '" + intEntryID + "', '" + txtDescription + "', '" + Convert.ToDateTime(txtFromDate).ToString("yyyyMMdd") + "', '" + Convert.ToDateTime(txtToDate).ToString("yyyyMMdd") + "', '" + isactive + "', '" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "', '" + UserID + "', '" + ddlcustomer + "', '" + ddlshippingline + "', '" + ddlCHA + "', '" + txtday + "', " + EmptyDay + ", " + StorageDay + "");

            }
            else
            {
                SvaDT = db.sub_GetDatatable("exec SP_SUExpTariffmaster '" + TariffID + "', '" + txtDescription + "', '" + Convert.ToDateTime(txtFromDate).ToString("yyyyMMdd") + "', '" + Convert.ToDateTime(txtToDate).ToString("yyyyMMdd") + "', '" + isactive + "', '" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "', '" + UserID + "', '" + ddlcustomer + "', '" + ddlshippingline + "', '" + ddlCHA + "', '" + txtday + "', " + EmptyDay + ", " + StorageDay + "");

            }
            if (SvaDT.Rows.Count > 0)
            {
                message = Convert.ToString(SvaDT.Rows[0]["Message"]);
            }
            return Json(message);
        }


        public ActionResult GetImportTariffSettingSummary()
        {
            DataTable dtscList = new DataTable();
            dtscList = db.sub_GetDatatable("SP_ExportTariffDetails");

            string json = JsonConvert.SerializeObject(dtscList);
            dtscList.Columns.Remove("Edit");
            Session["Exporttariffmaster"] = dtscList;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult ExportToExcelImportAccountMaster()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["Exporttariffmaster"];


            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;


            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Export Tariff Master.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Export Tariff Master Report<strong></td></tr>");

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


        public JsonResult CheckExportTariffMasterAlready(string TariffDescription)
        {
            string message = "";

            //HC.DBOperation object = new HC.DBOperations(); From Helper
            DataTable SvaDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            SvaDT = db.sub_GetDatatable("SP_SaveExportTariffMaster_Check_EXISTS '" + TariffDescription + "'");

            if (SvaDT.Rows.Count > 0)
            {
                message = Convert.ToString(SvaDT.Rows[0]["Message"]);
            }

            return Json(message);
        }





        // COde for Export tariff setting details

        public ActionResult ExportTariffSetting()
        {
            BE.ImportTraiffSettingEntities ImportTariffSettingList = new BE.ImportTraiffSettingEntities();
            ImportTariffSettingList = reportprovider.ExporttrariffSettingDropDown();
            ViewBag.ImportaccountMaster = new SelectList(ImportTariffSettingList.ImportaccountMaster, "AccountID", "AccountName");
            ViewBag.CommodityMaster = new SelectList(ImportTariffSettingList.CommodityMaster, "Commodity_Group_ID", "Commodity_Group_Name");
            ViewBag.ImportInvoiceType = new SelectList(ImportTariffSettingList.ImportInvoiceType, "ID", "InvoiceType");
            ViewBag.ChagresBasedOn = new SelectList(ImportTariffSettingList.ChagresBasedOn, "Chargeid", "BasedOn");
            ViewBag.SettingTax = new SelectList(ImportTariffSettingList.SettingTax, "Settingid", "TaxName");
            ViewBag.TransportType_m = new SelectList(ImportTariffSettingList.TransportType_m, "TransportID", "TransportType");
            ViewBag.ImportJoType = new SelectList(ImportTariffSettingList.ImportJoType, "Jo_type_id", "Jo_type");
            ViewBag.PortsEntites = new SelectList(ImportTariffSettingList.PortsEntites, "Portid", "PortName");
            ViewBag.CargoEntititesList = new SelectList(ImportTariffSettingList.CargoEntititesList, "cargoid", "cargoname");
            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = reportprovider.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");


            List<BE.ContainerSize> Containersize = new List<BE.ContainerSize>();
            Containersize = reportprovider.GetSizeDetails();
            ViewBag.ContainerSize = new SelectList(Containersize, "ContainerSizeID", "ContainerSizeName");

            List<BE.DeliveryTypeDetails> DeliveryType = new List<BE.DeliveryTypeDetails>();
            DeliveryType = reportprovider.GetDeliveryDetails();
            ViewBag.DeliveryType = new SelectList(DeliveryType, "DeliveryID", "DeliveryType");


            List<BE.SlabDetails> SlabDetails = new List<BE.SlabDetails>();
            SlabDetails = reportprovider.GetSlabDetails();
            ViewBag.SlabDetailsList = new SelectList(SlabDetails, "SlabId", "SlabId");




            List<BE.importtariffdetails> importtariffdetails = new List<BE.importtariffdetails>();
            importtariffdetails = reportprovider.Getimporttariffdetails();
            ViewBag.importtariffdetails = new SelectList(importtariffdetails, "TariffID", "TariffDescription");


            List<BE.ContainerType> ContainerType = new List<BE.ContainerType>();
            ContainerType = reportprovider.GetContainerType();
            ViewBag.ContainerType = new SelectList(ContainerType, "ContainerTypeID", "ContainerTypeName");



            List<BE.ImportAccountMaster> AccountHead = new List<BE.ImportAccountMaster>();
            AccountHead = reportprovider.GetAccountHead();
            ViewBag.AccountHead = new SelectList(AccountHead, "AccountID", "AccountName");
            return View();


        }

        public JsonResult GetExportTariffDetails(string SearchType)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Search_Exp_Tariff '" + SearchType + "'");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult AjaxGetExportTariffDetailsForaddtable(string TariffID, string From, string TO, string Accounting
    , string Size, string ChargesBased, string FixedAmt, string Days, string rate
    , string Slabid, string ScanType, string Location, string StuffLocation, string Jotype, string Commodity, string TransType,
    string Port, string Insurance, string AccountingID, List<BE.TariffAddDetailsEntites> DeliveryType1, List<BE.TariffAddDetailsEntites> Type1, string TaxID, string InvoiceType)
        {

            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            List<BE.TariffAddDetailsEntites> Getdetails1 = new List<BE.TariffAddDetailsEntites>();
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("DeliveryType");


            foreach (BE.TariffAddDetailsEntites item in DeliveryType1)
            {
                DataRow row = dataTable.NewRow();

                row["DeliveryType"] = item.DeliveryType;


                dataTable.Rows.Add(row);
            }
            DataTable dataTable1 = new DataTable();

            dataTable1.Columns.Add("Type");


            foreach (BE.TariffAddDetailsEntites item in Type1)
            {
                DataRow row = dataTable1.NewRow();

                row["Type"] = item.Type;


                dataTable1.Rows.Add(row);
            }
            Getdetails = reportprovider.GetExportAddTabledetails(TariffID, From, TO, Accounting, Size, ChargesBased, FixedAmt, Days, rate, Slabid, ScanType, Location, StuffLocation,
            Jotype, Commodity, TransType, Port, Insurance, AccountingID, dataTable, dataTable1, TaxID, InvoiceType);


            //Data To insert into 
            DataTable dataTableadd = new DataTable();

            dataTableadd.Columns.Add("TariffID");
            dataTableadd.Columns.Add("DeliveryType11");
            dataTableadd.Columns.Add("From");
            dataTableadd.Columns.Add("TO");
            dataTableadd.Columns.Add("Accounting");
            dataTableadd.Columns.Add("Size");
            dataTableadd.Columns.Add("Type1");
            dataTableadd.Columns.Add("Ftype");
            dataTableadd.Columns.Add("Slabid");
            dataTableadd.Columns.Add("Insurance");
            dataTableadd.Columns.Add("FixedAmt");
            dataTableadd.Columns.Add("rate");
            dataTableadd.Columns.Add("Tax");
            dataTableadd.Columns.Add("InvoiceType");
            dataTableadd.Columns.Add("CurrencyType");
            dataTableadd.Columns.Add("TransType");
            dataTableadd.Columns.Add("Port");
            dataTableadd.Columns.Add("Freedays");
            dataTableadd.Columns.Add("Location");
            dataTableadd.Columns.Add("StuffLocation");
            dataTableadd.Columns.Add("Jotype");
            dataTableadd.Columns.Add("ScanType");
            dataTableadd.Columns.Add("AccountingID");
            dataTableadd.Columns.Add("Days");


            foreach (BE.TariffAddDetailsEntites item in Getdetails)
            {
                DataRow row1 = dataTableadd.NewRow();

                row1["TariffID"] = item.TariffID;
                row1["DeliveryType11"] = item.DeliveryType;
                row1["From"] = item.From;
                row1["TO"] = item.TO;
                row1["Accounting"] = item.Accounting;
                row1["Size"] = item.Size;
                row1["Type1"] = item.Type;
                row1["Ftype"] = item.Ftype;
                row1["Slabid"] = item.Slabid;
                row1["Insurance"] = item.Insurance;
                row1["FixedAmt"] = item.FixedAmt;
                row1["rate"] = item.rate;
                row1["Tax"] = item.Tax;
                row1["InvoiceType"] = item.InvoiceType;
                row1["CurrencyType"] = item.CurrencyType;
                row1["TransType"] = item.TransType;
                row1["Port"] = item.Port;
                row1["Freedays"] = item.Freedays;
                row1["Location"] = item.Location;
                row1["StuffLocation"] = item.StuffLocation;
                row1["Jotype"] = item.Jotype;
                row1["ScanType"] = item.ScanType;
                row1["AccountingID"] = item.AccountingID;
                row1["Days"] = item.Days;

                dataTableadd.Rows.Add(row1);
            }
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            Getdetails1 = reportprovider.ExportSavedataForGetdata(dataTableadd, userId);

            //data to end insert

            var jsonResult = Json(Getdetails1, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ExportTariffSettingDetailsForUserdelete()
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_DeleteExporttariff '" + Userid + "'");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ExportTariffSettingDetailsForUserWisedelete(int Entryid)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_DeleteExporttariffWise '" + Entryid + "'," + Userid + "");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public ActionResult SaveExportTariffSettingDetails(List<BE.TariffAddDetailsEntites> ImportData, string commodity,string Fromdate, string Todate)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("TariffID");
            dataTable.Columns.Add("DeliveryType");
            dataTable.Columns.Add("From");
            dataTable.Columns.Add("TO");
            dataTable.Columns.Add("Accounting");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("Ftype");
            dataTable.Columns.Add("Slabid");
            dataTable.Columns.Add("Insurance");
            dataTable.Columns.Add("FixedAmt");
            dataTable.Columns.Add("rate");
            dataTable.Columns.Add("Tax");
            dataTable.Columns.Add("InvoiceType");
            dataTable.Columns.Add("CurrencyType");
            dataTable.Columns.Add("TransType");
            dataTable.Columns.Add("Port");
            dataTable.Columns.Add("Freedays");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("StuffLocation");
            dataTable.Columns.Add("Jotype");
            dataTable.Columns.Add("ScanType");
            dataTable.Columns.Add("AccountingID");


            foreach (BE.TariffAddDetailsEntites item in ImportData)
            {
                DataRow row = dataTable.NewRow();

                row["TariffID"] = item.TariffID;
                row["DeliveryType"] = item.DeliveryType;
                row["From"] = item.From;
                row["TO"] = item.TO;
                row["Accounting"] = item.Accounting;
                row["Size"] = item.Size;
                row["Type"] = item.Type;
                row["Ftype"] = item.Ftype;
                row["Slabid"] = item.Slabid;
                row["Insurance"] = item.Insurance;
                row["FixedAmt"] = item.FixedAmt;
                row["rate"] = item.rate;
                row["Tax"] = item.Tax;
                row["InvoiceType"] = item.InvoiceType;
                row["CurrencyType"] = item.CurrencyType;
                row["TransType"] = item.TransType;
                row["Port"] = item.Port;
                row["Freedays"] = item.Freedays;
                row["Location"] = item.Location;
                row["StuffLocation"] = item.StuffLocation;
                row["Jotype"] = item.Jotype;
                row["ScanType"] = item.ScanType;
                row["AccountingID"] = item.AccountingID;

                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.ExportCheckEffective(dataTable, Userid);

            if (message == "")
            {
                message = reportprovider.SaveExportSettingTariff(dataTable, Userid, commodity, Fromdate, Todate);
            }
            else
            {

            }



            return Json(message);

        }


        public ActionResult ExporttariffSettingDetails(string TariffID, string Deliverytype, string Containertype)
        {
            List<BE.TariffAddDetailsEntites> CancelDetails = new List<BE.TariffAddDetailsEntites>();
            CancelDetails = reportprovider.GetexporttariffDetails(TariffID, Deliverytype, Containertype);

            var jsonResult = Json(CancelDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public ActionResult ApproveDetailsForExportTariff(List<BE.TariffAddDetailsEntites> TariffNo)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Entryid");



            foreach (BE.TariffAddDetailsEntites item in TariffNo)
            {
                DataRow row = dataTable.NewRow();

                row["Entryid"] = item.Entryid;


                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.ApproveDetailsExportTariff(dataTable, Userid);
            return Json(message);

        }

        [HttpPost]
        public ActionResult CancelDetailsForExportTariff(List<BE.TariffAddDetailsEntites> TariffNo)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Entryid");



            foreach (BE.TariffAddDetailsEntites item in TariffNo)
            {
                DataRow row = dataTable.NewRow();

                row["Entryid"] = item.Entryid;


                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.CancelDetailsExportTariff(dataTable, Userid);
            return Json(message);

        }

        public ActionResult PrintCartingAllow(string CartingAllowID)
        {
            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getInDL = db.sub_GetDatatable("USP_Carting_allow_Print '" + CartingAllowID + "'");

            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.SRno = dr["Sr No"];
                    ViewBag.CartingAllowID = dr["CartingAllowID"];
                    ViewBag.EntryDate = dr["EntryDate"];
                    ViewBag.DeclQty = dr["DeclQty"];
                    ViewBag.SBNo = dr["SBNo"];
                    ViewBag.VehicleNo = dr["VehicleNo"];
                    ViewBag.Location = dr["Location"];
                    ViewBag.Allowwt = dr["Allowwt"];
                    ViewBag.UnitWT = dr["UnitWT"];
                    ViewBag.TotalPKGS = dr["TotalPKGS"];
                    ViewBag.CargoWeight = dr["CargoWeight"];
                    ViewBag.CargoDesc = dr["CargoDesc"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.Cargotype = dr["Cargotype"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.StuffingType = dr["StuffingType"];
                    ViewBag.shippername = dr["shippername"];
                    ViewBag.AGName = dr["AGName"];
                    ViewBag.Package = dr["Package"];
                    ViewBag.PrintDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                    ViewBag.PrintTime = Convert.ToDateTime(DateTime.Now).ToString("HH:mm:ss");
                    ViewBag.ImportloadedDetails = getInDL.AsEnumerable();
                }
            }
            return PartialView();
        }


        public ActionResult PrintCartingTallyAllow(string CartingAllowID)
        {
            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getInDL = db.sub_GetDatatable("USP_Carting_Tally_allow_Print '" + CartingAllowID + "'");

            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.SRno = dr["Sr No"];
                    ViewBag.CartingID = dr["CartingID"];
                    ViewBag.CartingDate = dr["CartingDate"];
                    ViewBag.CartingQty = dr["CartingQty"];
                    ViewBag.SBNo = dr["SBNo"];
                    ViewBag.Area = dr["Area"];
                    ViewBag.Location = dr["Location"];
                    ViewBag.SBDate = dr["SBDate"];
                    ViewBag.CargoDesc = dr["CargoDesc"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.shippername = dr["shippername"];
                    ViewBag.StuffingType = dr["StuffingType"];
                    ViewBag.Cargotype = dr["Cargotype"];
                    ViewBag.AGName = dr["AGName"];
                    ViewBag.PrintDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                    ViewBag.PrintTime = Convert.ToDateTime(DateTime.Now).ToString("HH:mm:ss");

                    ViewBag.ImportloadedDetails = getInDL.AsEnumerable();
                }
            }
            return PartialView();
        }



        public ActionResult PrintExportStuffingRequest(string StuffRequestID)
        {
            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getInDL = db.sub_GetDatatable("USP_GetStuffingRequestprint '" + StuffRequestID + "'");

            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {
                    ViewBag.StuffRequestID = dr["StuffRequestID"];
                    ViewBag.RequestDate = dr["RequestDate"];
                    ViewBag.SBNo = dr["SBNo"];
                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.ContainerNo = dr["ContainerNo"];
                    ViewBag.CSize = dr["CSize"];
                    ViewBag.Cargodesc = dr["Cargodesc"];
                    ViewBag.ViaNo = dr["ViaNo"];
                    ViewBag.FPD = dr["FPD"];
                    ViewBag.Rotation = dr["Rotation"];
                    ViewBag.TotalPKGS = dr["TotalPKGS"];
                    ViewBag.VesselName = dr["VesselName"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.SLName = dr["SLName"];
                    ViewBag.AGName = dr["AGName"];
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.Con_For = dr["Con_For"];
                    ViewBag.shippername = dr["shippername"];
                    ViewBag.PrintDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                    ViewBag.PrintTime = Convert.ToDateTime(DateTime.Now).ToString("HH:mm:ss");

                    ViewBag.ImportloadedDetails = getInDL.AsEnumerable();
                }
            }
            return PartialView();
        }

        public ActionResult ExportStuffingTally(string Cartingid)
        {
            ViewBag.cartingid = Cartingid;
            return View();
        }

        public JsonResult ExportStuffing(string StuffingRequestID)
        {

            DataTable Getdetails = new DataTable();
            DataTable Getdt = new DataTable();

            string message = "";
            List<BE.ExportStuffingTallly> receiptEntries = new List<BE.ExportStuffingTallly>();
            CD.DBOperations db = new CD.DBOperations();
            Getdetails = db.sub_GetDatatable("USP_SHOWSTUFFINGDETAILS '" + StuffingRequestID + "'");



            if (Getdetails.Rows.Count != 0)
            {
                foreach (DataRow row in Getdetails.Rows)
                {
                    BE.ExportStuffingTallly receiptEntry = new BE.ExportStuffingTallly();
                    ////Trailer.TrailerNo = Convert.ToString(row["trailername"]);
                    //Trailer.lblcfs = Convert.ToString(row["ownedby"]);
                    //Trailer.txtvehicletarewt = Convert.ToString(row["Vehicle Weight"]);
                    receiptEntry.dblCargoQty = Convert.ToString(row["Cargo Qty"]);
                    receiptEntry.dblSQty = Convert.ToString(row["Stuffed Qty"]);
                    receiptEntry.SBNumber = Convert.ToString(row["SB Number"]);
                    receiptEntry.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["Container No"]);
                    receiptEntry.Size = Convert.ToString(row["Size"]);
                    receiptEntry.ISOCode = Convert.ToString(row["ISO Code"]);
                    receiptEntry.CargoDesc = Convert.ToString(row["Cargo Desc"]);
                    receiptEntry.CargoQty = Convert.ToString(row["Cargo Qty"]);
                    receiptEntry.CargoWeight = Convert.ToString(row["Cargo Weight"]);
                    receiptEntry.StuffedQty = Convert.ToString(row["Stuffed Qty"]);
                    receiptEntry.BLQty = Convert.ToString(row["BL Qty"]);
                    receiptEntry.QtyUnit = Convert.ToString(row["Qty Unit"]);
                    receiptEntry.StuffedWeight = Convert.ToString(row["Stuffed Weight"]);
                    receiptEntry.BLWeight = Convert.ToString(row["BL Weight"]);
                    receiptEntry.TareWeight = Convert.ToString(row["Tare Weight"]);

                    receiptEntry.AgentSeal = Convert.ToString(row["Agent Seal"]);
                    receiptEntry.CustomSeal = Convert.ToString(row["Custom Seal"]);
                    receiptEntry.SealNo = Convert.ToString(row["Seal No"]);
                    receiptEntry.SBEntryID = Convert.ToString(row["SBEntryID"]);
                    receiptEntry.EntryID = Convert.ToString(row["EntryID"]);

 

                    receiptEntries.Add(receiptEntry);
                }

            }


            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult ExportStuffingName(string StuffingRequestID)
        {

            DataTable Getdetails = new DataTable();
            DataTable Getdt = new DataTable();

            string message = "";
            List<BE.ExportStuffingTallly> receiptEntries = new List<BE.ExportStuffingTallly>();
            CD.DBOperations db = new CD.DBOperations();
            Getdetails = db.sub_GetDatatable("USP_ShowStuffingRequest '" + StuffingRequestID + "'");



            if (Getdetails.Rows.Count != 0)
            {
                foreach (DataRow row in Getdetails.Rows)
                {
                    BE.ExportStuffingTallly receiptEntry = new BE.ExportStuffingTallly();
                    receiptEntry.txtvessell = Convert.ToString(row["Vessel Name"]);
                    receiptEntry.txtvia = Convert.ToString(row["Via No"]);
                    receiptEntry.txtpod = Convert.ToString(row["POD"]);
                    receiptEntry.txtshipper = Convert.ToString(row["Customer Name"]);
                    receiptEntry.dblCargoQty = Convert.ToString(row["Cargo Qty"]);
                    receiptEntry.dblSQty = Convert.ToString(row["Stuffed Qty"]);
                    receiptEntry.SBNumber = Convert.ToString(row["SB Number"]);
                    receiptEntry.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["Container No"]);
                    receiptEntry.Size = Convert.ToString(row["Size"]);
                    receiptEntry.ISOCode = Convert.ToString(row["ISO Code"]);
                    receiptEntry.CargoDesc = Convert.ToString(row["Cargo Desc"]);
                    receiptEntry.CargoQty = Convert.ToString(row["Cargo Qty"]);
                    receiptEntry.CargoWeight = Convert.ToString(row["Cargo Weight"]);
                    receiptEntry.StuffedQty = Convert.ToString(row["Stuffed Qty"]);
                    receiptEntry.BLQty = Convert.ToString(row["BL Qty"]);
                    receiptEntry.QtyUnit = Convert.ToString(row["Qty Unit"]);
                    receiptEntry.StuffedWeight = Convert.ToString(row["Stuffed Weight"]);
                    receiptEntry.BLWeight = Convert.ToString(row["BL Weight"]);
                    receiptEntry.TareWeight = Convert.ToString(row["Tare Weight"]);
                    receiptEntry.AgentSeal = Convert.ToString(row["Agent Seal"]);
                    receiptEntry.CustomSeal = Convert.ToString(row["Custom Seal"]);
                    receiptEntry.SealNo = Convert.ToString(row["Seal No"]);
                    receiptEntry.SBEntryID = Convert.ToString(row["SBEntryID"]);
                    receiptEntry.EntryID = Convert.ToString(row["EntryID"]);



                    receiptEntries.Add(receiptEntry);
                }

            }


            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public JsonResult ExportStuffingTallySave(List<BE.ExportStuffingTallly> Debitdata, string txtStuffingRequestID,string txtStuffingDate,string txtPOD, string hdnddlVendorID)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SBNumber");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("ISOCode");
            dataTable.Columns.Add("CargoDesc");
            dataTable.Columns.Add("CargoWeight");
            dataTable.Columns.Add("StuffedQty");
            dataTable.Columns.Add("BLQty");
            dataTable.Columns.Add("StuffedWeight");
            dataTable.Columns.Add("BLWeight");
            dataTable.Columns.Add("TareWeight");
            dataTable.Columns.Add("AgentSeal");
            dataTable.Columns.Add("CustomSeal");
            dataTable.Columns.Add("SBEntryID");
            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("QtyUnit");
            foreach (BE.ExportStuffingTallly item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["SBNumber"] = item.SBNumber;
                row["ContainerNo"] = item.ContainerNo;
                row["ISOCode"] = item.ISOCode;
                row["CargoDesc"] = item.CargoDesc;
                row["CargoWeight"] = item.CargoWeight;
                row["StuffedQty"] =  item.StuffedQty;
                row["BLQty"] = item.BLQty;
                row["StuffedWeight"] = item.StuffedWeight;
                row["BLWeight"] = item.BLWeight;
                row["TareWeight"] = item.TareWeight;
                row["AgentSeal"] = item.AgentSeal;
                row["CustomSeal"] = item.CustomSeal;
                row["SBEntryID"] = item.SBEntryID;
                row["EntryID"] = item.EntryID;
                row["QtyUnit"] = item.QtyUnit;

                dataTable.Rows.Add(row);
            }

            message = reportprovider.ExportStuffingTallySave(dataTable, UserID, txtStuffingRequestID, txtStuffingDate, txtPOD, hdnddlVendorID);

            return Json(message);
        }

        public ActionResult GetStuffingTallySummary()
        {
            DataTable dtscList = new DataTable();
            dtscList = db.sub_GetDatatable("SP_ExportTariffDetails");

            string json = JsonConvert.SerializeObject(dtscList);
            dtscList.Columns.Remove("Edit");
            Session["Exporttariffmaster"] = dtscList;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        //code by prashant

        // Export Invoice Details 



        public ActionResult ExportProformaInvoice()
        {
            BE.ExportProformaInvoice GetDetails = new BE.ExportProformaInvoice();
            GetDetails = reportprovider.ProformaFillDropDownList();

            ViewBag.Customer = new SelectList(GetDetails.ExportProformaCusomter, "CustomerID", "CustomerName");
            ViewBag.Shipper = new SelectList(GetDetails.ExportProformaShipper, "shipperID", "shippername");
            ViewBag.Cha = new SelectList(GetDetails.ExportProformaCha, "ChaID", "ChaName");
            ViewBag.Transtype = new SelectList(GetDetails.ExportProformaTransType, "Transport_Type_ID", "Transport_Type");
            ViewBag.BillType = new SelectList(GetDetails.ExportProformaBillType, "TypeID", "BillType");
            ViewBag.Tariffmaster = new SelectList(GetDetails.ExportProformaTariffmaster, "TariffID", "TariffDescription");
            ViewBag.Accountmaster = new SelectList(GetDetails.ExportProformaAccountmaster, "AccountID", "AccountName");
            ViewBag.Location = new SelectList(GetDetails.ExportProformaLocation, "LocationID", "LocationName");
            ViewBag.Allotment = new SelectList(GetDetails.ExportProformaAllotment, "ID", "Name");
            ViewBag.Comodity = new SelectList(GetDetails.ExportProformaCommodity, "Commodity_Group_ID", "Commodity");
            ViewBag.Tax_Services = new SelectList(GetDetails.ExportProformaTax_Service, "ID", "Tax_Type_Desc");
            ViewBag.InvoiceType = new SelectList(GetDetails.ExportProformaInvoiceType, "InvoiceTypeID", "InvoiceType");



            List<BE.CargoTypes> CargoList = new List<BE.CargoTypes>();
            CargoList = reportprovider.CargoType();
            ViewBag.CargoList = new SelectList(CargoList, "Cargotypeid", "Cargotype");
            ViewBag.CargotypeList = JsonConvert.SerializeObject(CargoList);
            return View();
        }


        public ActionResult ExportProformaGSTSearch()
        {

            return PartialView();
        }
        public JsonResult ExportGSTSearch(string SearchText)
        {
            List<BE.ImportProformaSearchGST> GstList = new List<BE.ImportProformaSearchGST>();
            GstList = reportprovider.GetExportGSTList(SearchText);
            var jsonResult = Json(GstList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public ActionResult GetContainerWiseProformaDetails(string Containerno ,string MovementType)
        {

            List<BE.ContainerWiseProformaDetails> GetDetails = new List<BE.ContainerWiseProformaDetails>();
            GetDetails = reportprovider.GetContainerWiseDetailForExportProforma(Containerno, MovementType);

            List<BE.ShippingBIllDetailsForExportProforma> Shippingbill = new List<BE.ShippingBIllDetailsForExportProforma>();
            Shippingbill = reportprovider.GetContainerWiseDetailForExportProformaShippingbill(Containerno);



            var returnField = new { ContainerDetails = GetDetails, ShippingDetails = Shippingbill };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public JsonResult GetContainerWiseProformaDetailsSBDetails(string CustomerID)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("SELECT top 1 TariffID,TariffDescription FROM EXP_tariffmaster WHERE AGENCYID = '" + CustomerID + "'");
            BE.TariffDetailsForExport Obj = new BE.TariffDetailsForExport();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Obj.TariffID = Convert.ToString(row["TariffID"]);
                    Obj.TariffDescription = Convert.ToString(row["TariffDescription"]);
                }
            }

            var jsonResult = Json(Obj, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult GetSBWiseProformaDetails(string SBNumber)
        {

            List<BE.ContainerWiseProformaDetails> GetDetails = new List<BE.ContainerWiseProformaDetails>();
            GetDetails = reportprovider.GetsbWiseDetailForExportProforma(SBNumber);

            List<BE.ShippingBIllDetailsForExportProforma> Shippingbill = new List<BE.ShippingBIllDetailsForExportProforma>();
            Shippingbill = reportprovider.GetsbWiseDetailForExportProformaShippingbill(SBNumber);



            var returnField = new { ContainerDetails = GetDetails, ShippingDetails = Shippingbill };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public ActionResult GetBLWiseWiseProformaDetails(string BLNumber)
        {

            List<BE.ContainerWiseProformaDetails> GetDetails = new List<BE.ContainerWiseProformaDetails>();
            GetDetails = reportprovider.GetBLWiseDetailForExportProforma(BLNumber);

            List<BE.ShippingBIllDetailsForExportProforma> Shippingbill = new List<BE.ShippingBIllDetailsForExportProforma>();
            Shippingbill = reportprovider.GeTBLWiseDetailForExportProformaShippingbill(BLNumber);



            var returnField = new { ContainerDetails = GetDetails, ShippingDetails = Shippingbill };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }


        [HttpPost]
        public ActionResult CalculateExportPerformaDetails(string InvoiceDate, string BlNo, string GSTNO, string GSTID, string GSTName, string TransType, string PickUP, string StuffLocation,
      string Commodity, string RRno, string RRDate, string Customer, string CHA, string Importer, string Line, string CargoDesc, string Portname, string Remakrs, string TariffID, string TariffDesc,
      string FreeDays, string Empty, string Storage, string StateCode, string Movementtype, string SBNO, string TAxID, string Containerno,string ValidUpto,string InvoiceType,string MovementtypeName, bool additionalcheck,
      List<BE.ContainerWiseProformaDetails> tablearrayCont, List<BE.additionalAccountDetails> tableAccountadditional)
        {
            DataTable dataTable = new DataTable();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            string strSQL = "";




            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("ContainerType");
            dataTable.Columns.Add("CargoType");
            dataTable.Columns.Add("indate");
            dataTable.Columns.Add("StuffingDate");
            dataTable.Columns.Add("Gpdate");
            dataTable.Columns.Add("EmptyDays");
            dataTable.Columns.Add("LoadedDays");
            dataTable.Columns.Add("PickUP");
            dataTable.Columns.Add("Transport_Type");
            dataTable.Columns.Add("NetWeight");
            dataTable.Columns.Add("TareWeight");
            dataTable.Columns.Add("GrossWeight");
            dataTable.Columns.Add("MovementType");
            dataTable.Columns.Add("SBNumber");
            dataTable.Columns.Add("LineID");
            dataTable.Columns.Add("SLName");
            dataTable.Columns.Add("Port");
            dataTable.Columns.Add("PortName");
            dataTable.Columns.Add("PickupLocID");
            dataTable.Columns.Add("StuffLocID");
            dataTable.Columns.Add("NoOfStuffLoc");
            dataTable.Columns.Add("entryID");



            foreach (BE.ContainerWiseProformaDetails item in tablearrayCont)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["ContainerType"] = item.ContainerType;
                row["CargoType"] = item.CargoType;
                row["indate"] = item.indate;
                row["StuffingDate"] = item.StuffingDate;
                row["Gpdate"] = item.Gpdate;
                row["EmptyDays"] = item.EmptyDays;
                row["LoadedDays"] = item.LoadedDays;
                row["PickUP"] = item.PickUP;
                row["Transport_Type"] = item.Transport_Type;

                row["NetWeight"] = item.NetWeight;
                row["TareWeight"] = item.TareWeight;
                row["GrossWeight"] = item.GrossWeight;
                row["MovementType"] = item.MovementType;
                row["SBNumber"] = item.SBNumber;
                row["LineID"] = item.LineID;
                row["SLName"] = item.SLName;
                row["Port"] = item.Port;
                row["PortName"] = item.PortName;
                row["PickupLocID"] = item.PickupLocID;
                row["StuffLocID"] = item.StuffLocID;
                row["NoOfStuffLoc"] = item.NoOfStuffLoc;
                row["entryID"] = item.entryID;

                dataTable.Rows.Add(row);
            }

            
            int intChargesCounter;
            double dblNetAmount_IND;

            bool blnContainerFound;
            int intContCounter;
            DataTable dtp = new DataTable();
            double dblAssessNo;
            double dblCartingAssessno;
            double dblMainAssessNo;

            double dblTempSTax;
            DateTime dblDestuffDate;
            double dblSQM;
            double dblDestuffDays;
            DataTable dtQ = new DataTable();
            DataTable AddDatable = new DataTable();
            double dblDestuffWeek;
            double dblweight;
            bool blIsIGMWise;
            double dblPerc;
            int intRowCount;
            double dblIGST;
            double dblCGST;
            double dblSGST;
            double dblSumSGSTAmt;
            double dblSumCGSTAmt;
            double dblSumIGSTAmt;
            double dblSumNetAmtTotal;
            string workyear = "2020-21";
            double dblIGSTax = 0;
            double dblCGSTax = 0;
            double dblSGSTax = 0;
            double dbltaxgroupid = 11;

            DataTable additionaldataTable = new DataTable();
            if (tableAccountadditional != null)
            {

                additionaldataTable.Columns.Add("AccountNameAdditional");
                additionaldataTable.Columns.Add("ContainernoAdditional");
                additionaldataTable.Columns.Add("AmountAdditional");
                additionaldataTable.Columns.Add("Accountadditional");


                foreach (BE.additionalAccountDetails item in tableAccountadditional)
                {
                    DataRow row = additionaldataTable.NewRow();

                    row["AccountNameAdditional"] = item.AccountNameAdditional;
                    row["ContainernoAdditional"] = item.ContainernoAdditional;
                    row["AmountAdditional"] = item.AmountAdditional;
                    row["Accountadditional"] = item.Accountadditional;

                    additionaldataTable.Rows.Add(row);
                }

            }


            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            strSQL = "";
            strSQL = " Delete From  Temp_Export_assessD Where UserID=" + Userid + "";
            dtp = db.sub_GetDatatable(strSQL);
            strSQL = "";
            strSQL = " Delete From Temp_Export_assessDII Where UserID=" + Userid + "";
            dtp = db.sub_GetDatatable(strSQL);
            string message = "";

            for (int k = 0; k <= dataTable.Rows.Count - 1; k++)
            {

                dtQ.Clear();
                strSQL = "SELECT TOP 1 * FROM Export_tariffdetails WHERE TariffID='" + TariffID + "' and " + " deliverytype='" + Movementtype + "' and " + Convert.ToDateTime(InvoiceDate).ToString("yyyyMMdd") + " BETWEEN EffectiveFrom and EffectiveUpto  AND IsApproved=1  " + "  AND (Containertype='" + dataTable.Rows[k].Field<string>("ContainerNo") + "' or Containertype='All') AND Containersize='" + dataTable.Rows[k].Field<string>("Size") + "'";
                dtQ = db.sub_GetDatatable(strSQL);

                if (dtQ.Rows.Count == 0)

                    message = "Tariff ID: " + TariffDesc + " not found in database for Cargo Type-size " + dataTable.Rows[k].Field<string>("CargoType") + "  " + dataTable.Rows[k].Field<string>("Size") + "  . Please contact your administrator!";

            }

            if (Movementtype != "SSR")
            {
                string strSQL3;
                for (intContCounter = 0; intContCounter <= dataTable.Rows.Count - 1; intContCounter++)
                {
                    
                    DataTable dtRSFetch = new DataTable();
                    DataTable GetInDcharges = new DataTable();
                    strSQL3 = "";
                    strSQL3 = "   USP_Export_TARIFF_FETCH_ALL '" + TariffID + "' ,'" + Movementtype + "'  ,'" + dataTable.Rows[intContCounter].Field<string>("MovementType") + "' , " + "   '" + dataTable.Rows[intContCounter].Field<string>("CargoType") + "', " + dataTable.Rows[intContCounter].Field<string>("Size") + "" + " ,'" + dataTable.Rows[intContCounter].Field<string>("Port") + "' ";
                    dtRSFetch = db.sub_GetDatatable(strSQL3);
                    if (dtRSFetch.Rows.Count > 0)
                    {
                        for (int j = 0; j <= dtRSFetch.Rows.Count - 1; j++)
                        {
                            dblNetAmount_IND = 0;
                            DataTable dtRSTemp1 = new DataTable();
                            Int64 Getasscountid = Convert.ToInt64(dtRSFetch.Rows[j].Field<Int64>("AccountID"));
                            strSQL = "";
                            strSQL = " SELECT DISTINCT AccountName  FROM Exp_AccountMaster WHERE AccountID=" + Convert.ToInt64(dtRSFetch.Rows[j].Field<Int64>("AccountID"));
                            dtRSTemp1 = db.sub_GetDatatable(strSQL);
                            if (dtRSTemp1.Rows.Count > 0)
                            {

                                GetInDcharges = db.sub_GetDatatable("USP_IMP_SUB_FETCHCHARGES_IND_export '" + Convert.ToInt64(dataTable.Rows[intContCounter].Field<string>("entryID")) + "','" + dataTable.Rows[intContCounter].Field<string>("ContainerNo") + "'," +
                                   "'" + dataTable.Rows[intContCounter].Field<string>("Cargotype") + "','" + dataTable.Rows[intContCounter].Field<string>("Size") + "','" + Convert.ToInt64(dtRSFetch.Rows[j].Field<Int64>("AccountID")) + "','" + Convert.ToInt32(dataTable.Rows[intContCounter].Field<string>("LoadedDays")) + "','" + dataTable.Rows[intContCounter].Field<string>("GrossWeight") + "'," +
                                   "'" + Convert.ToDateTime(dataTable.Rows[intContCounter].Field<string>("InDate")).ToString("dd MMM yyyy HH:mm") + "','" + dataTable.Rows[intContCounter].Field<string>("TareWeight") + "','" + dataTable.Rows[intContCounter].Field<string>("MovementType") + "'," +
                                   "'" + Convert.ToInt16(dataTable.Rows[intContCounter].Field<string>("Port")) + "','" + Convert.ToInt16(dataTable.Rows[intContCounter].Field<string>("PickupLocID")) + "','" + dataTable.Rows[intContCounter].Field<string>("StuffLocID") + "'" +
                                   ",'" + Convert.ToInt16(dataTable.Rows[intContCounter].Field<string>("NoOfStuffLoc")) + "','"+ TariffID + "','"+ 0 +"','"+ TransType + "','"+ Commodity + "','"+ Convert.ToDateTime(ValidUpto).ToString("dd MMM yyyy HH:mm")   + "','"+   InvoiceType + "'");


                                double dblAmount = 0;
                                if (GetInDcharges.Rows.Count > 0)
                                {
                                    dblNetAmount_IND = Convert.ToDouble(GetInDcharges.Rows[0][0]);
                                }

                                string COntainerant = dataTable.Rows[intContCounter].Field<string>("ContainerNo");
                                //'''**************************** ADDITIONAL CHARGES ''

                                if (additionalcheck == true)
                                {
                                    if (additionaldataTable.Rows.Count > 0)
                                    {
                                        for (int e = 0; e < additionaldataTable.Rows.Count; e++)
                                        {
                                            if (additionaldataTable.Rows[e].Field<string>("AmountAdditional") == "")
                                            {
                                                if (additionalcheck == true && Getasscountid == Convert.ToDouble(additionaldataTable.Rows[e].Field<string>("Accountadditional")))
                                                {
                                                    dblAmount = 0;
                                                }
                                            }
                                            if (additionaldataTable.Rows[e].Field<string>("AmountAdditional") != "")
                                            {
                                                if (additionaldataTable.Rows[e].Field<string>("ContainernoAdditional") == null)
                                                {
                                                    if (additionalcheck == true && Getasscountid == Convert.ToDouble(additionaldataTable.Rows[e].Field<string>("Accountadditional")))
                                                    {
                                                        if (additionaldataTable.Rows[e].Field<string>("AccountNameAdditional") != "" && Getasscountid == Convert.ToDouble(additionaldataTable.Rows[e].Field<string>("Accountadditional")))
                                                        {
                                                            dblAmount = dblAmount + Convert.ToDouble(additionaldataTable.Rows[e].Field<string>("AmountAdditional"));
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (additionalcheck == true && Getasscountid == Convert.ToDouble(additionaldataTable.Rows[e].Field<string>("Accountadditional")) && additionaldataTable.Rows[e].Field<string>("ContainernoAdditional") == COntainerant)
                                                    {
                                                        if (additionaldataTable.Rows[e].Field<string>("AccountNameAdditional") != "" && Getasscountid == Convert.ToDouble(additionaldataTable.Rows[e].Field<string>("Accountadditional")))
                                                        {
                                                            dblAmount = dblAmount + Convert.ToDouble(additionaldataTable.Rows[e].Field<string>("AmountAdditional"));
                                                        }

                                                    }

                                                }
                                            }

                                        }
                                    }
                                }

                                dblNetAmount_IND = dblNetAmount_IND + dblAmount;
                            }
                            DateTime Datetime = new DateTime();
                           

                            if (dblNetAmount_IND > 0)
                            {


                                DataTable DTRSfetchForGST = new DataTable();
                                strSQL3 = "";
                                
                                    strSQL = "";
                                    strSQL = "  select * from exp_accountmaster WHERE accountid='" + Convert.ToInt64(dtRSFetch.Rows[0].Field<Int64>("AccountID")) + "'";
                                    dtp = db.sub_GetDatatable(strSQL);

                                
                                DataTable dtget = new DataTable();
                                if (dtp.Rows.Count > 0)
                                {
                                    strSQL = "";
                                    strSQL = "SELECT TOP 1 * FROM settings_taxes WHERE  settingsID='" + dtp.Rows[0].Field<int>("taxid") + "' and " + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + " BETWEEN EffectiveFrom and EffectiveUpto";
                                    dtget = db.sub_GetDatatable(strSQL);

                                }
                                if (dtget.Rows.Count > 0)
                                {

                                    dblSGSTax = Convert.ToInt16(dtget.Rows[0].Field<double>("SGST"));
                                    dblCGSTax = Convert.ToInt16(dtget.Rows[0].Field<double>("SGST"));
                                    dblIGSTax = Convert.ToInt16(dtget.Rows[0].Field<double>("IGST"));
                                    TAxID = Convert.ToString(dtget.Rows[0].Field<Int16>("settingsID"));
                                }

                                strSQL = "";
                                strSQL = "SELECT TOP 1 * FROM Settings";
                                dtget = db.sub_GetDatatable(strSQL);

                                if (StateCode == dtget.Rows[0].Field<string>("tinnumber"))
                                {
                                    dblIGSTax = 0;
                                }
                                else
                                {
                                    dblSGSTax = 0;
                                    dblCGSTax = 0;
                                }
                                int intTotalDay = 0;
                                int FreeDays1 = 0;
                                int inrchargable = 0;
                                double dblemptydays = 0;

                                intTotalDay = Convert.ToInt32(dataTable.Rows[intContCounter].Field<string>("LoadedDays"));
                                dblemptydays = Convert.ToInt32(dataTable.Rows[intContCounter].Field<string>("EmptyDays"));

                                if (FreeDays1 - Convert.ToInt32(dataTable.Rows[intContCounter].Field<string>("LoadedDays")) > 0)
                                    inrchargable = 0;
                                else
                                    inrchargable = Convert.ToInt32(dataTable.Rows[intContCounter].Field<string>("LoadedDays")) - FreeDays1;
                                string strSQL2;
                                strSQL2 = "";
                                strSQL2 = " Exec USP_Insert_Temp_Export_assessD 0,'" + workyear + "','" + dataTable.Rows[intContCounter].Field<string>("entryID") + "','" + dataTable.Rows[intContCounter].Field<string>("ContainerNo") + "' , ";
                                strSQL2 += " '" + dataTable.Rows[intContCounter].Field<string>("GrossWeight") + "','" + dataTable.Rows[intContCounter].Field<string>("MovementType") + "', ";
                                strSQL2 += " '" + Convert.ToDateTime(dataTable.Rows[intContCounter].Field<string>("indate")).ToString("dd-MMM-yyyy HH:mm") + "','" + intTotalDay + "','" + FreeDays1 + "','" + inrchargable + "','" + Convert.ToInt64(dtRSFetch.Rows[j].Field<Int64>("AccountID")) + "',";
                                strSQL2 += " '" + dblNetAmount_IND + "','" + (Convert.ToDecimal(dblNetAmount_IND) * Convert.ToDecimal(dblSGSTax)) / 100 + "','" + (Convert.ToDecimal(dblNetAmount_IND) * Convert.ToDecimal(dblCGSTax)) / 100 + "',";
                                strSQL2 += " '" + (Convert.ToDecimal(dblNetAmount_IND) * Convert.ToDecimal(dblIGSTax)) / 100 + "','" + TAxID + "', ";
                                strSQL2 += " '" + SBNO + "','" + SBNO + "','" + Userid + "','" + dataTable.Rows[intContCounter].Field<string>("TareWeight") + "','" + dataTable.Rows[intContCounter].Field<string>("NetWeight") + "','', " + dblemptydays + " , '" + Convert.ToDateTime(dataTable.Rows[intContCounter].Field<string>("StuffingDate")).ToString("dd-MMM-yyyy HH:mm") + "' ";
                                AddDatable = db.sub_GetDatatable(strSQL2);

                            }
                        }
                    }

                }
            }

            int intcntcount;
            for (intcntcount = 0; intcntcount <= dataTable.Rows.Count - 1; intcntcount++)
            {

                dtp.Clear();
                DataTable Amount1 = new DataTable();
                strSQL = "";
                strSQL = "  select e.accountid, SUM(amount) as amount,ContainerNo, em.taxid as taxcode  FROM exp_additional e inner join exp_accountmaster em on em.accountid=e.accountid  WHERE containerno = '" + dataTable.Rows[intcntcount].Field<string>("ContainerNo") + "' AND  ";
                strSQL += " e.entryid='" + dataTable.Rows[intcntcount].Field<string>("entryID") + "' and ReceiptNo=0 and TransNo=0 and IsCancel=0 and ContainerNo is not null  GROUP BY e.accountID, Containerno, em.taxid  ";
                Amount1 = db.sub_GetDatatable(strSQL);
                if (Amount1.Rows.Count > 0)
                {
                    for (int j = 0; j <= Amount1.Rows.Count - 1; j++)
                    {
                        if (Convert.ToInt64(Amount1.Rows[j].Field<Int64>("AccountID")) != 0)
                        {
                            DataTable DTfetch = new DataTable();
                            DataTable DTfetch1 = new DataTable();
                            strSQL = "";
                            strSQL = " select settingsid taxid, taxname from  settings_taxes   where  settingsid= " + Convert.ToInt64(Amount1.Rows[j].Field<Int32>("taxcode")) + " ";
                            DTfetch = db.sub_GetDatatable(strSQL);


                            strSQL = "";
                            strSQL = " select * from Commodity_Group_M where Commodity_Group_ID=" + Commodity + "";
                            DTfetch1 = db.sub_GetDatatable(strSQL);

                            int intTotalDay = 0;
                            int FreeDays1 = 0;
                            int inrchargable = 0;
                            double dblemptydays = 0;

                            intTotalDay = Convert.ToInt32(dataTable.Rows[intcntcount].Field<string>("LoadedDays"));
                            dblemptydays = Convert.ToInt32(dataTable.Rows[intcntcount].Field<string>("EmptyDays"));

                            if (FreeDays1 - Convert.ToInt32(dataTable.Rows[intcntcount].Field<string>("LoadedDays")) > 0)
                                inrchargable = 0;
                            else
                                inrchargable = Convert.ToInt32(dataTable.Rows[intcntcount].Field<string>("LoadedDays")) - FreeDays1;


                            if (DTfetch.Rows.Count > 0)
                            {

                                DataTable DTRSfetchForGST = new DataTable();
                               
                                
                                    strSQL = "";
                                    strSQL = "  select * from exp_accountmaster WHERE accountid='" + Convert.ToInt64(Amount1.Rows[j].Field<Int64>("AccountID")) + "'";
                                    dtp = db.sub_GetDatatable(strSQL);

                                
                                DataTable dtget = new DataTable();
                                if (dtp.Rows.Count > 0)
                                {
                                    strSQL = "";
                                    strSQL = "SELECT TOP 1 * FROM settings_taxes WHERE  settingsID='" + dtp.Rows[0].Field<int>("taxid") + "' and " + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + " BETWEEN EffectiveFrom and EffectiveUpto";
                                    dtget = db.sub_GetDatatable(strSQL);

                                }
                                if (dtget.Rows.Count > 0)
                                {

                                    dblSGSTax = Convert.ToInt16(dtget.Rows[0].Field<double>("SGST"));
                                    dblCGSTax = Convert.ToInt16(dtget.Rows[0].Field<double>("SGST"));
                                    dblIGSTax = Convert.ToInt16(dtget.Rows[0].Field<double>("IGST"));
                                    TAxID = Convert.ToString(dtget.Rows[0].Field<Int16>("settingsID"));
                                }

                                strSQL = "";
                                strSQL = "SELECT TOP 1 * FROM Settings";
                                dtget = db.sub_GetDatatable(strSQL);

                                if (StateCode == dtget.Rows[0].Field<string>("tinnumber"))
                                {
                                    dblIGSTax = 0;
                                }
                                else
                                {
                                    dblSGSTax = 0;
                                    dblCGSTax = 0;
                                }
                                strSQL = "";
                                strSQL = " Exec USP_Insert_Temp_Export_assessD 0,'" + workyear + "','" + dataTable.Rows[intcntcount].Field<string>("entryID") + "','" + dataTable.Rows[intcntcount].Field<string>("ContainerNo") + "' , ";
                                strSQL += " '" + dataTable.Rows[intcntcount].Field<string>("GrossWeight") + "','" + dataTable.Rows[intcntcount].Field<string>("MovementType") + "', ";
                                strSQL += " '" + Convert.ToDateTime(dataTable.Rows[intcntcount].Field<string>("indate")).ToString("dd-MMM-yyyy HH:mm") + "','" + intTotalDay + "','" + FreeDays1 + "','" + inrchargable + "','" + Convert.ToInt64(Amount1.Rows[j].Field<Int64>("AccountID")) + "',";
                                strSQL += " '" + Convert.ToInt64(Amount1.Rows[j][1]) + "','" + (Convert.ToDecimal(Convert.ToInt64(Amount1.Rows[j][1]) * Convert.ToDecimal(dblSGSTax)) / 100) + "','" + (Convert.ToDecimal(Convert.ToInt64(Amount1.Rows[j][1]) * Convert.ToDecimal(dblCGSTax)) / 100) + "',";
                                strSQL += " '" + (Convert.ToDecimal(Convert.ToInt64(Amount1.Rows[j][1]) * Convert.ToDecimal(dblIGSTax)) / 100) + "','" + TAxID + "', ";
                                strSQL += " '" + SBNO + "','" + SBNO + "','" + Userid + "','" + dataTable.Rows[intcntcount].Field<string>("TareWeight") + "','" + dataTable.Rows[intcntcount].Field<string>("NetWeight") + "','', " + dblemptydays + " , '" + Convert.ToDateTime(dataTable.Rows[intcntcount].Field<string>("StuffingDate")).ToString("dd-MMM-yyyy HH:mm") + "' ";
                                AddDatable = db.sub_GetDatatable(strSQL);
                            }
                        }
                    }
                }

            }
            //string strctrsb = "";
            //double dblCalStorageAmt = 0;
            //strctrsb = Containerno + SBNO;
            //strSQL = "";
            //strSQL = "  delete  from Temp_Export_Storage where SBCTRNo ='" + strctrsb + "'";
            //dtp = db.sub_GetDatatable(strSQL);
            //// Storage CHARGE Calculations
            //if (Trim(cmbAssessType.Text) == "All")
            //    sub_fetchcharges_Storage(106);
            //// Annexure Details Details
            //DataTable dTFETCHAn = new DataTable();
            //strSQL = "";
            //strSQL = " USP_Storage_Annexure'" + Strings.Trim(strctrsb) + "'";
            //dTFETCHAn = sub_GetDatatable(strSQL);
            //if (dTFETCHAn.Rows.Count > 0)
            //{
            //    mshfannexure.DataSource = null;
            //    mshfannexure.Columns.Clear();
            //    mshfannexure.DataSource = dTFETCHAn;
            //    GridSettingsstorage();
            //}
            //else
            //{
            //    mshfannexure.DataSource = null;
            //    mshfannexure.Columns.Clear();
            //}

            //if (Val(cmbservicetype.SelectedValue) != 0)
            //{
            //    dblIGSTax = 0;
            //    dblCGSTax = 0;
            //    dblSGSTax = 0;
            //}
            //// inserting storage
            //if (Trim(cmbAssessType.Text) == "All")
            //{
            //    strSQL = "";
            //    strSQL = "  select 106 accountid, Round(isnull(sum(amt),0),0) as amount, ( select top 1 taxname from exp_accountmaster A inner join settings_taxes S on s.settingsid=a.taxid where accountid=106 )   taxname, ( select top 1 settingsid from exp_accountmaster A inner join settings_taxes S on s.settingsid=a.taxid where accountid=106 )   taxid  from  Temp_Export_Storage where SBCTRNo ='" + Strings.Trim(strctrsb) + "'";
            //    dtp = sub_GetDatatable(strSQL);
            //    if (dtp.Rows.Count > 0)
            //    {
            //        double dbltotalctr = 0;
            //        double dblstorageamt = 0;
            //        double dblsaveamt;
            //        double dbldiffamt;

            //        dbltotalctr = Val(lbl20.Text) + Val(lbl40.Text) + Val(lbl45.Text);
            //        dblstorageamt = Val(dtp.Rows(0)("amount"));
            //        if (Val(dtp.Rows(0)("amount")) > 0)
            //        {
            //            dblstorageamt = Strings.Format(dblstorageamt / dbltotalctr, "0");
            //            dbldiffamt = Val(dtp.Rows(0)("amount")) - Conversion.Val(dblstorageamt * dbltotalctr);

            //            for (intcntcount = 0; intcntcount <= grdcontainers.Rows.Count - 1; intcntcount++)
            //            {
            //                // If dbldiffamt < 0 Then
            //                // dblstorageamt = dblstorageamt + dbldiffamt
            //                // Else
            //                // dblstorageamt = dblstorageamt + dbldiffamt
            //                // End If



            //                if (sub_CheckIsIGST(lblGSTID.Text, dtp.Rows(0)("taxname")) == false)
            //                {
            //                }
            //                if (Val(cmbservicetype.SelectedValue) != 0)
            //                {
            //                    dblIGSTax = 0;
            //                    dblCGSTax = 0;
            //                    dblSGSTax = 0;
            //                    dbltaxgroupid = 11;
            //                }

            //                strSQL = "";
            //                strSQL = " Exec USP_Insert_Temp_Export_assessD 0,'" + strWorkYear + "','" + Trim(grdcontainers.Rows(intcntcount).Cells("CtrentryID").Value + "") + "', '" + Trim(grdcontainers.Rows(intcntcount).Cells("Container No").Value + "") + "' , ";
            //                strSQL += "'" + Val(grdcontainers.Rows(intcntcount).Cells("Gross Weight").Value + "") + "','" + Trim(grdcontainers.Rows(intcntcount).Cells("Movement Type").Value + "") + "', ";
            //                strSQL += " '" + Format(Convert.ToDateTime(grdcontainers.Rows(intcntcount).Cells("In date").Value), "dd-MMM-yyyy HH:mm") + "', '" + Val(grdcontainers.Rows(intcntcount).Cells("Loaded days").Value + "") + "',0,0,106, '" + Conversion.Val(dblstorageamt + dbldiffamt) + "',";
            //                strSQL += " '" + Format(Conversion.Val(dblstorageamt + dbldiffamt) * (dblSGSTax / (double)100), "0.00") + "','" + Format(Conversion.Val(dblstorageamt + dbldiffamt) * (dblCGSTax / (double)100), "0.00") + "','" + Format(Conversion.Val(dblstorageamt + dbldiffamt) * (dblIGSTax / (double)100), "0.00") + "' ";
            //                strSQL += " ," + Val(dtp.Rows(0)("taxid")) + ", ";
            //                strSQL += " '" + Trim(txtsbno.Text) + "','" + Trim(txtsbno.Text) + "'," + Val(intUserID) + " ,'" + Val(grdcontainers.Rows(intcntcount).Cells("Tare Weight").Value + "") + "','" + Val(grdcontainers.Rows(intcntcount).Cells("Net Weight").Value + "") + "',0,  '" + Val(grdcontainers.Rows(intcntcount).Cells("Empty days").Value + "") + "' , '" + Format(Convert.ToDateTime(grdcontainers.Rows(intcntcount).Cells("Stuffing Date").Value), "dd-MMM-yyyy HH:mm") + "'  ";
            //                sub_ExecuteNonQuery(strSQL);
            //                dbldiffamt = 0;
            //            }
            //        }
            //    }
            //}

            dtp.Clear();


            strSQL = "";
            strSQL = " Exec sp_export_inv_charges '" + SBNO + "','" + Userid + "'";
            dtp = db.sub_GetDatatable(strSQL);

            string json = JsonConvert.SerializeObject(dtp);



            BE.TextBoXValuesForImportPerforma GetContainerAmtdetails = new BE.TextBoXValuesForImportPerforma();
            strSQL = "";
            strSQL = "get_sum_charges_Export_TMT '" + SBNO + "','" + workyear + "'," + Userid + "   ";
            dtp = db.sub_GetDatatable(strSQL);
            if (dtp.Rows.Count > 0)
            {

                foreach (DataRow row in dtp.Rows)
                {

                    GetContainerAmtdetails.SGST = Convert.ToDouble(row["SGST"]);
                    GetContainerAmtdetails.CGST = Convert.ToDouble(row["CGST"]);
                    GetContainerAmtdetails.IGST = Convert.ToDouble(row["IGST"]);
                    GetContainerAmtdetails.Amount = Convert.ToDouble(row["Amount"]);
                    GetContainerAmtdetails.nettotal = Convert.ToDouble(row["Amount"]) + Convert.ToDouble(row["SGST"]) + Convert.ToDouble(row["CGST"]) + Convert.ToDouble(row["IGST"]);
                }

            }
            var returnField = new { GetContainerSHowList = json, ContainerAmtShowList = GetContainerAmtdetails };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };



        }


        public ActionResult SaveExpoerinvoicePerformaDetails(string InvoiceDate, string BlNo, string GSTNO, string GSTID, string GSTName, string TransType, string PickUP, string StuffLocation,
      string Commodity, string RRno, string RRDate, string Customer, string CHA, string Importer, string Line, string CargoDesc, string Portname, string Remakrs, string TariffID, string TariffDesc,
      string FreeDays, string Empty, string Storage, string StateCode, string Movementtype, string SBNO, string TAxID, string Containerno,
               string netamount, string SGST, string CGST, string IGST, string GrandTotal, string shipperID, string ValidDate, List<BE.ShippingBIllDetailsForExportProforma> tableShippingbill)
        {



            DataTable dataTable = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            dataTable.Columns.Add("SBNumber");
            dataTable.Columns.Add("SBDate");
            dataTable.Columns.Add("CartingDate");
            dataTable.Columns.Add("StuffingDate");
            dataTable.Columns.Add("TotalDays");
            dataTable.Columns.Add("CartingQty");
            dataTable.Columns.Add("CartingWeight");
            dataTable.Columns.Add("Area");
            dataTable.Columns.Add("Space");
            dataTable.Columns.Add("CargoDescriptions");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("entryid");
            dataTable.Columns.Add("CargoWeight");
            dataTable.Columns.Add("TotalPKGS");





            if (tableShippingbill != null)
            {
                foreach (BE.ShippingBIllDetailsForExportProforma item in tableShippingbill)
                {
                    DataRow row = dataTable.NewRow();

                    row["SBNumber"] = item.SBNumber;
                    row["SBDate"] = item.SBDate;
                    row["CartingDate"] = item.CartingDate;
                    row["StuffingDate"] = item.StuffingDate;
                    row["TotalDays"] = item.TotalDays;
                    row["CartingQty"] = item.CartingQty;
                    row["CartingWeight"] = item.CartingWeight;
                    row["Area"] = item.Area;
                    row["Space"] = item.Space;
                    row["CargoDescriptions"] = item.CargoDescriptions;
                    row["VehicleNo"] = item.VehicleNo;

                    row["entryid"] = item.entryid;
                    row["CargoWeight"] = item.CargoWeight;
                    row["TotalPKGS"] = item.TotalPKGS;


                    dataTable.Rows.Add(row);
                }
            }

           

            Int64 intid = 0;
            string strinvoiceNo = "";
            Int64 txtassessno = 0;
            DataTable dtwo = new DataTable();
            DataTable dt12 = new DataTable();
            DataTable DTfetch1 = new DataTable();
            double dbltaxcategoryid = 0;
            HC.DBOperations db = new HC.DBOperations();


            string strSQL1;
            string strSQL;
            string strWorkYear = "2020-21";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string strSGSTPer = "";
            string strCGSTPer = "";
            string strIGSTPer = "";
            strSQL = "";
            strSQL = " select * from Commodity_Group_M where Commodity_Group_ID=" + Commodity + "";
            DTfetch1 = db.sub_GetDatatable(strSQL);
            if (Convert.ToInt32(DTfetch1.Rows[0].Field<Int32>("TaxGroupID")) != 0)
            {
                DataTable dtgroupid = new DataTable();
                strSQL = "";
                strSQL = "UPDATE Temp_export_AssessD SEt taxgroupid=11 WHERE  UserID=" + Userid + " and IGMNo='" + SBNO + "' ";
                dt12 = db.sub_GetDatatable(strSQL);
            }
            string strctrsb = "";
            strctrsb = Containerno + SBNO;

            strSGSTPer = "SGST" + " @ " + 9 + "%";
            strCGSTPer = "CGST" + " @ " + 9 + "%";
            strIGSTPer = "IGST" + " @ " + 18 + "%";
            strSQL = "";
            strSQL = "SELECT MAX(EXP_ProformaNo) as[exp_assessno] FROM settings_assess WHERE EXP_ProformaWY='" + strWorkYear + "'";
            DataTable dt1 = new DataTable();
            dt1 = db.sub_GetDatatable(strSQL);
            intid = Convert.ToInt64(dt1.Rows[0].Field<Int64>("exp_assessno") + 1);
            txtassessno = Convert.ToInt64(dt1.Rows[0].Field<Int64>("exp_assessno") + 1);
            string strinvyear;
            int allowcount = (int)(Math.Log10(intid) + 1);
            string str = strWorkYear;
            str = str.Remove(0, 5);


            if (allowcount == 1)
            {
                strinvoiceNo = "EXP/" + "0000" + intid + "/" + str;
            }
            else if (allowcount == 2)
            {
                strinvoiceNo = "EXP/" + "000" + intid + "/" + str;
            }
            else if (allowcount == 3)
            {
                strinvoiceNo = "EXP/" + "00" + intid + "/" + str;
            }
            else if (allowcount == 4)
            {
                strinvoiceNo = "EXP/" + "0" + intid + "/" + str;
            }
            else if (allowcount == 5)
            {
                strinvoiceNo = "EXP/" + "" + intid + "/" + str;
            }


            if (Commodity == "1" || Commodity == "2")
            {
                dbltaxcategoryid = Convert.ToDouble(Commodity);
            }

            else
            {
                dbltaxcategoryid = 0;
            }




            strSQL1 = "";
            strSQL1 = " EXEC USP_INSERT_EXP_ASSESSM '" + txtassessno + "','" + strWorkYear + "', '" + strinvoiceNo + "', ";
            strSQL1 += " '" + Convert.ToDateTime(InvoiceDate).ToString("dd-MMM-yyyy HH:mm") + "','" + Movementtype + "',";
            strSQL1 += " '" + TariffID + "','" + Customer + "','" + shipperID + "','" + CHA + "','" + 0 + "','" + SBNO + "','" + Convert.ToDateTime(ValidDate).ToString("dd-MMM-yyyy HH:mm") + "' ,";
            strSQL1 += " '" + netamount + "','" + 0 + "','" + 0 + "','" + GrandTotal + "','" + "" + "','" + 0 + "','" + 0 + "','" + 0 + "','" + Remakrs + "',";
            strSQL1 += " '" + Userid + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy HH:mm") + "','" + "P" + "','" + strSGSTPer + "','" + strCGSTPer + "', '" + strIGSTPer + "',";
            strSQL1 += " '" + 0 + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy HH:mm") + "','" + "NULL" + "','" + 0 + "','" + 0 + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy HH:mm") + "','" + Importer + "','" + "Null" + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + SGST + "','" + IGST + "','" + CGST + "','" + GSTID + "','" + 0 + "','" + dbltaxcategoryid + "', '" + CargoDesc + "', '" + Portname + "'," + Commodity + " ," + 0 + " ,'" + RRno + "', '" + RRDate + "','" + BlNo + "' ";
            dt1 = db.sub_GetDatatable(strSQL1);


            DataTable dt = new DataTable();
            strSQL = "";
            strSQL = "UPDATE settings_assess SEt EXP_ProformaNo=" + intid + " WHERE EXP_ProformaWY='" + strWorkYear + "'";
            dt1 = db.sub_GetDatatable(strSQL);

            strSQL = "";
            strSQL = " exec USP_Insert_Export_assessD " + Userid + ",'" + SBNO + "' , " + intid + ", '" + strWorkYear + "' ,'" + strctrsb + "'";
            dt1 = db.sub_GetDatatable(strSQL);

            //   sb details
            for (int k = 0; k <= dataTable.Rows.Count - 1; k++)
            {
                strSQL = "";
                strSQL = " Exec USP_INSERT_EXPORT_ASSESSSB '" + txtassessno + "','" + strWorkYear + "',0,'" + dataTable.Rows[k].Field<string>("SBNumber") + "',";
                strSQL += " '" + Convert.ToDateTime(dataTable.Rows[k].Field<string>("CartingDate")).ToString("dd-MMM-yyyy HH:mm") + "', '" + dataTable.Rows[k].Field<string>("CargoDescriptions") + "', '" + dataTable.Rows[k].Field<string>("CartingWeight") + "',  '" + dataTable.Rows[k].Field<string>("CartingQty") + "',";
                strSQL += " '" + dataTable.Rows[k].Field<string>("Area") + "', '" + dataTable.Rows[k].Field<string>("Space") + "', '" + Convert.ToDateTime(dataTable.Rows[k].Field<string>("StuffingDate")).ToString("dd-MMM-yyyy HH:mm") + "',  '" + dataTable.Rows[k].Field<string>("TotalDays") + "',0,  '" + dataTable.Rows[k].Field<string>("VehicleNo") + "', '" + Convert.ToDateTime(dataTable.Rows[k].Field<string>("SBDate")).ToString("dd-MMM-yyyy HH:mm") + "'";
                dt1 = db.sub_GetDatatable(strSQL);
            }

            string Messageget = "Record Saved Successfully!";
            string message = Messageget + ',' + strinvoiceNo;

            return Json(message);

        }

        public JsonResult GetCargoDetailsForExportproforma(string SbNO)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetSBDetailsForCargoChange '" + SbNO + "'");

            List<BE.ContainerCargotypeDetails> Customerlst = new List<BE.ContainerCargotypeDetails>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ContainerCargotypeDetails customer = new BE.ContainerCargotypeDetails();

                    customer.containerno = Convert.ToString(row["Containerno"]);
                    customer.Size = Convert.ToInt16(row["size"]);
                   
                    customer.Cargotypeid = Convert.ToString(row["cargotypeid"]);
                  
                    customer.JONo = Convert.ToString(row["sbentryid"]);
                    customer.PKGS = Convert.ToString(row["SBNo"]);
                    Customerlst.Add(customer);
                }
            }

            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;


        }

        public JsonResult GetExportInvoiceporformadetails(string fromdate, string Todate, string searchCerteria, string Searchtext, string Searchtext1)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_ExportPerformaInvoiceList '" + fromdate + "','" + Todate + "','" + searchCerteria + "','" + Searchtext + "','" + Searchtext1 + "'");
            dt.Columns.Remove("Sr No");

            string json = JsonConvert.SerializeObject(dt);
            dt.Columns.Remove("Print");
            Session["ExportToExcelPerformaInvoice"] = dt;
            Session["fromdate"] = fromdate;
            Session["Todate"] = Todate;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        [HttpPost]
        public ActionResult ExportInvoicePerformaPrint(string InvoiceNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable lblGetassessno = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblchargesDetails = new DataTable();
            DataTable tblshippingdetails = new DataTable();
            DataTable tblAnxedetails = new DataTable();


            HC.DBOperations db = new HC.DBOperations();
            double Amount = 0;
            double Discount = 0;
            double NetAmount = 0;
            string Srate = "";
            double SGST = 0;
            string CRate = "";
            double CGST = 0;
            string IRate = "";
            double IGST = 0;
            double GrandAmount =0;

            getJobOrderSet = db.sub_GetDataSets("USP_GetExportProformaPrint '" + InvoiceNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                lblGetassessno = getJobOrderSet.Tables[0];
                tblInvoiceDetails = getJobOrderSet.Tables[1];
                tblContainerDetails = getJobOrderSet.Tables[2];
                tblchargesDetails = getJobOrderSet.Tables[3];
                tblshippingdetails = getJobOrderSet.Tables[4];
                tblAnxedetails = getJobOrderSet.Tables[5];


                foreach (DataRow dr in lblGetassessno.Rows)
                {
                    ViewBag.AssessNo = dr["AssessNo"];
                    ViewBag.WorkYear = dr["WorkYear"];
                    ViewBag.InvNo = dr["InvNo"];
                    ViewBag.AssessDate = dr["AssessDate"];
                    ViewBag.assesstype = dr["assesstype"];
                    ViewBag.Port = dr["Port"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.shippername = dr["shippername"];
                    ViewBag.ShippingLine = dr["SlName"];
                    ViewBag.con_Name = dr["con_Name"];

                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.CINNo = dr["CINNo"];
                    ViewBag.IMP_AC_EMAILID = dr["IMP_AC_EMAILID"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.GSTName = dr["GSTName"];
                    ViewBag.GSTAddress = dr["GSTAddress"];
                    ViewBag.State = dr["State"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.GSTIn_uniqID = dr["GSTIn_uniqID"];
                    ViewBag.TotalAmountInWords = dr["TotalAmountInWords"];
                    ViewBag.BankName = dr["BankName"];
                    ViewBag.AccountNo = dr["AccountNo"];
                    ViewBag.BranchName = dr["BranchName"];
                    ViewBag.IFSCCode = dr["IFSCCode"];
                    ViewBag.remarks = dr["remarks"];
                    ViewBag.NoteVI = dr["NoteVI"];
                    ViewBag.GSTIN = dr["GSTIN"];
                    ViewBag.Panno = dr["Panno"];


                    ViewBag.TotalAmount = dr["GrandTotal"];
                    ViewBag.AmountWithoutTax = dr["NetTotal"];
                    ViewBag.SGSTAmount = dr["SGST"];
                    ViewBag.CGSTAmount = dr["CGST"];
                    ViewBag.IGSTAmount = dr["IGST"];
                    ViewBag.TaxAmount = dr["TaxTotal"];
                    ViewBag.Datetime = Convert.ToDateTime(DateTime.Now).ToString("dd MM yyyy");
                    ViewBag.TIme = Convert.ToDateTime(DateTime.Now).ToString("HH:mm");
                }


            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();
            ViewBag.chargesDetails = tblchargesDetails.AsEnumerable();
            ViewBag.ShippingDetails = tblshippingdetails.AsEnumerable();
            ViewBag.AnexDetails = tblAnxedetails.AsEnumerable();

            foreach (DataRow data in tblchargesDetails.Rows)
            {
                Amount = Convert.ToDouble(data["Amount"]);
                Discount = Convert.ToDouble(data["Discount"]);
                NetAmount = Convert.ToDouble(data["NetAmount"]);
                Srate = Convert.ToString(data["Srate"]);
                CRate = Convert.ToString(data["CRate"]);
                IRate = Convert.ToString(data["IRate"]);
                SGST = Convert.ToDouble(data["SGST"]);
                CGST = Convert.ToDouble(data["CGST"]);
                IGST = Convert.ToDouble(data["IGST"]);
            }

            

            foreach (DataRow data in tblchargesDetails.Rows)
            {
                Amount = Convert.ToDouble(data["Amount"]);
                Discount = Convert.ToDouble(data["Discount"]);
                NetAmount = Convert.ToDouble(data["NetAmount"]);
                Srate = Convert.ToString(data["Srate"]);
                CRate = Convert.ToString(data["CRate"]);
                IRate = Convert.ToString(data["IRate"]);
                SGST = Convert.ToDouble(data["SGST"]);
                CGST = Convert.ToDouble(data["CGST"]);
                IGST = Convert.ToDouble(data["IGST"]);
            }

            foreach (DataRow data in tblAnxedetails.Rows)
            {
                GrandAmount = GrandAmount+ Convert.ToDouble(data["Amt"]);

            }

            ViewBag.GrandToatls = GrandAmount;
            Session["alertCount"] = GrandAmount;
            ViewBag.Amount = Amount;
            ViewBag.Discount = Discount;
            ViewBag.NetAmount = NetAmount;
            ViewBag.Srate = Srate;
            ViewBag.CRate = CRate;
            ViewBag.IRate = IRate;
            ViewBag.SGST = SGST;
            ViewBag.CGST = CGST;
            ViewBag.IGST = IGST;



            return PartialView();

        }
        public ActionResult PendingExportProformaForInvoice()
        {
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = invDataProvider.getParty();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
            return View();

        }

        public JsonResult GetExportPendingproformaDetails(string SearchCriteria, string Search, string Search1, string Search2)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_PendingExportPerformaInvoiceList '" + SearchCriteria + "','" + Search + "','" + Search1 + "','" + Search2 + "'");

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            dt.Columns.Remove("View");
            dt.Columns.Remove("Submit");
            dt.Columns.Remove("Cancel");
            Session["ListOfPendingProformaInvoiceforFinalConfirm"] = dt;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult CancelExportinvoiceProforma(string AssessNo, string workyear)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = reportprovider.CancelExportInvoicePorforma(AssessNo, workyear, userId);
            return Json(message);
        }


        [HttpPost]
        public ActionResult SubmitExportDetailsEntry(string AssessNo, string workyear,string assesstype)
        {
            string message = "";
            string strinvoiceNo = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            if (assesstype != "")
             
            {
                message = reportprovider.SubmitExportDetailsforporforma(AssessNo, workyear, assesstype);
            }


           

            if (message == "")
            {
                message = "Invoice code is not mapped. Cannot proceed!";
            }
            else
            {
                int maxallowid = 0;
                maxallowid = reportprovider.getExportallowid(AssessNo, workyear, userId);
                int allowind = maxallowid + 1;
                string str = workyear;
                str = str.Remove(0, 5);

                int allowcount = (int)(Math.Log10(allowind) + 1);
                if (allowcount == 1)
                {
                    strinvoiceNo =  message + "0000" + allowind + "/" + str;
                }
                else if (allowcount == 2)
                {
                    strinvoiceNo =   message + "000" + allowind + "/" + str;
                }
                else if (allowcount == 3)
                {
                    strinvoiceNo =   message + "00" + allowind + "/" + str;
                }
                else if (allowcount == 4)
                {
                    strinvoiceNo =   message + "0" + allowind + "/" + str;
                }
                else if (allowcount == 5)
                {
                    strinvoiceNo = message + "" + allowind + "/" + str;
                }


                message = reportprovider.SubmitExportFinalDetails(AssessNo, workyear, userId, strinvoiceNo, allowind);

                message = strinvoiceNo;
            }
            return Json(message);
        }


        public JsonResult GetExportPendingInvoiceToday(string fromdate, string Todate, string searchCerteria, string Searchtext)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_ExportAssessListWeb '" + fromdate + "','" + Todate + "','" + searchCerteria + "','" + Searchtext + "'");
            dt.Columns.Remove("Sr No");
            string json = JsonConvert.SerializeObject(dt);
            dt.Columns.Remove("Print");
            dt.Columns.Remove("Cancel");
            Session["importAssessListPending"] = dt;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        [HttpPost]
        public ActionResult ExportInvoiceTaxPrint(string InvoiceNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable lblGetassessno = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblchargesDetails = new DataTable();
            DataTable tblshippingdetails = new DataTable();
            DataTable tblAnxedetails = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            double Amount = 0;
            double Discount = 0;
            double NetAmount = 0;
            string Srate = "";
            double SGST = 0;
            string CRate = "";
            double CGST = 0;
            string IRate = "";
            double IGST = 0;
            double GrandAmount = 0;
            string SignedQRcode = "";

            getJobOrderSet = db.sub_GetDataSets("USP_GetExportTAXInvoicePrint '" + InvoiceNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                lblGetassessno = getJobOrderSet.Tables[0];
                tblInvoiceDetails = getJobOrderSet.Tables[2];
                tblContainerDetails = getJobOrderSet.Tables[1];
                tblchargesDetails = getJobOrderSet.Tables[3];
                tblshippingdetails = getJobOrderSet.Tables[4];
                tblAnxedetails = getJobOrderSet.Tables[5];



                foreach (DataRow dr in lblGetassessno.Rows)
                {
                    ViewBag.AssessNo = dr["AssessNo"];
                    ViewBag.WorkYear = dr["WorkYear"];
                    ViewBag.InvNo = dr["InvNo"];
                    ViewBag.AssessDate = dr["AssessDate"];
                    ViewBag.assesstype = dr["assesstype"];
                    ViewBag.Port = dr["Port"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.shippername = dr["shippername"];
                    ViewBag.ShippingLine = dr["SLName"];
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.CheckedBy = dr["Checked By"];

                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.CINNo = dr["CINNo"];
                    ViewBag.IMP_AC_EMAILID = dr["IMP_AC_EMAILID"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.GSTName = dr["GSTName"];
                    ViewBag.GSTAddress = dr["GSTAddress"];
                    ViewBag.State = dr["State"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.GSTIn_uniqID = dr["GSTIn_uniqID"];
                    ViewBag.TotalAmountInWords = dr["TotalAmountInWords"];
                    ViewBag.BankName = dr["BankName"];
                    ViewBag.AccountNo = dr["AccountNo"];
                    ViewBag.BranchName = dr["BranchName"];
                    ViewBag.IFSCCode = dr["IFSCCode"];
                    ViewBag.remarks = dr["remarks"];
                    ViewBag.NoteVI = dr["NoteVI"];
                    ViewBag.GSTIN = dr["GSTIN"];
                    ViewBag.Panno = dr["Panno"];
                    ViewBag.INVheader = dr["INVheader"];
                    ViewBag.NoteRemarks = dr["Note"];



                    ViewBag.TotalAmount = dr["GrandTotal"];
                    ViewBag.AmountWithoutTax = dr["NetTotal"];
                    ViewBag.SGSTAmount = dr["SGST"];
                    ViewBag.CGSTAmount = dr["CGST"];
                    ViewBag.IGSTAmount = dr["IGST"];
                    ViewBag.TaxAmount = dr["TaxTotal"];
                    ViewBag.AGName = dr["AGName"];

                    ViewBag.Irn = dr["Irn"];
                    ViewBag.AckNo = dr["AckNo"];
                    ViewBag.AckDt = dr["AckDt"];
                    //ViewBag.SignedQRcode = dr["SignedQRcode"];
                    SignedQRcode= dr["SignedQRcode"].ToString();
                    ViewBag.RRNo = dr["RRNo"];
                    ViewBag.RRDATE = dr["RRDATE"];
                    ViewBag.BLNumber = dr["BLNumber"];
                    ViewBag.UPINO = dr["UPINumber"];
                    ViewBag.Registered = dr["TyepReg"];
                    ViewBag.Datetime = Convert.ToDateTime(DateTime.Now).ToString("dd MM yyyy");
                    ViewBag.TIme = Convert.ToDateTime(DateTime.Now).ToString("HH:mm");



                }


            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();
            ViewBag.chargesDetails = tblchargesDetails.AsEnumerable();
            ViewBag.ShippingDetails = tblshippingdetails.AsEnumerable();
            ViewBag.AnexDetails = tblAnxedetails.AsEnumerable();

            foreach (DataRow data in tblchargesDetails.Rows)
            {
                Amount = Convert.ToDouble(data["Amount"]);
                Discount = Convert.ToDouble(data["Discount"]);
                NetAmount = Convert.ToDouble(data["NetAmount"]);
                Srate = Convert.ToString(data["Srate"]);
                CRate = Convert.ToString(data["CRate"]);
                IRate = Convert.ToString(data["IRate"]);
                SGST = Convert.ToDouble(data["SGST"]);
                CGST = Convert.ToDouble(data["CGST"]);
                IGST = Convert.ToDouble(data["IGST"]);
            }

            foreach (DataRow data in tblAnxedetails.Rows)
            {
                GrandAmount = GrandAmount + Convert.ToDouble(data["Amt"]);
               
            }

            ViewBag.Amount = Amount;
            ViewBag.Discount = Discount;
            ViewBag.NetAmount = NetAmount;
            ViewBag.Srate = Srate;
            ViewBag.CRate = CRate;
            ViewBag.IRate = IRate;
            ViewBag.SGST = SGST;
            ViewBag.CGST = CGST;
            ViewBag.IGST = IGST;


            ViewBag.GrandToatls = GrandAmount;
            Session["alertCount"]= GrandAmount;

            //To get E-Invoice QR Code
            if (SignedQRcode != "")
            {
                QRCoder.QRCodeGenerator qrGenerator = new QRCodeGenerator();

                QRCodeData qrCodeData = qrGenerator.CreateQrCode(SignedQRcode, QRCodeGenerator.ECCLevel.Q);

                QRCode qrCode = new QRCode(qrCodeData);


                //QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(ViewBag.SignedQRcode, QRCodeGenerator.ECCLevel.Q);

                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        ViewBag.path = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                }
            }

            return PartialView();

        }
       // [HttpPost]
        public ActionResult ExportCartingInvoiceTaxPrint(string txtAssessmentCarting, string txtWorkCarting)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable lblGetassessno = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblchargesDetails = new DataTable();
            DataTable tblshippingdetails = new DataTable();
            DataTable tblAnxedetails = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            double Amount = 0;
            double Discount = 0;
            double NetAmount = 0;
            string Srate = "";
            double SGST = 0;
            string CRate = "";
            double CGST = 0;
            string IRate = "";
            double IGST = 0;
            double GrandAmount = 0;

            getJobOrderSet = db.sub_GetDataSets("USP_EXPORT_PRINT_WEBS '" + txtAssessmentCarting + "','"+ txtWorkCarting + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                lblGetassessno = getJobOrderSet.Tables[0];
                tblInvoiceDetails = getJobOrderSet.Tables[2];
                tblContainerDetails = getJobOrderSet.Tables[1];
                tblchargesDetails = getJobOrderSet.Tables[3];
                tblshippingdetails = getJobOrderSet.Tables[4];
               // tblAnxedetails = getJobOrderSet.Tables[5];



                foreach (DataRow dr in lblGetassessno.Rows)
                {
                    ViewBag.AssessNo = dr["AssessNo"];
                    ViewBag.WorkYear = dr["WorkYear"];
                    ViewBag.InvNo = dr["InvNo"];
                    ViewBag.AssessDate = dr["AssessDate"];
                    ViewBag.assesstype = dr["assesstype"];
                    ViewBag.Port = dr["Port"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.shippername = dr["shippername"];
                    ViewBag.ShippingLine = dr["SLName"];
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.CheckedBy = dr["Checkedby"];

                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.CINNo = dr["CINNo"];
                    ViewBag.IMP_AC_EMAILID = dr["IMP_AC_EMAILID"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.GSTName = dr["GSTName"];
                    ViewBag.GSTAddress = dr["GSTAddress"];
                    ViewBag.State = dr["State"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.GSTIn_uniqID = dr["GSTIn_uniqID"];
                    ViewBag.TotalAmountInWords = dr["AmtInWord"];
                    ViewBag.BankName = dr["BankName"];
                    ViewBag.AccountNo = dr["AccountNo"];
                    ViewBag.BranchName = dr["BranchName"];
                    ViewBag.IFSCCode = dr["IFSCCode"];
                    ViewBag.remarks = dr["remarks"];
                    ViewBag.NoteVI = dr["NoteVI"];
                    ViewBag.GSTIN = dr["GSTIN"];
                    ViewBag.Panno = dr["Panno"];
                    //ViewBag.INVheader = "Carting Tax Invoice";
                    ViewBag.NoteRemarks = dr["Note"];



                    ViewBag.TotalAmount = dr["GrandTotal"];
                    ViewBag.AmountWithoutTax = dr["NetTotal"];
                    ViewBag.SGSTAmount = dr["SGST"];
                    ViewBag.CGSTAmount = dr["CGST"];
                    ViewBag.IGSTAmount = dr["IGST"];
                    ViewBag.TaxAmount = dr["NetAmount"];
                    //ViewBag.AGName = dr["AGName"];

                    ViewBag.Irn = dr["Irn"];
                    ViewBag.AckNo = dr["AckNo"];
                    ViewBag.AckDt = dr["AckDt"];
                    ViewBag.SignedQRcode = dr["SignedQRcode"];
                    ViewBag.RRNo = dr["RRNo"];
                    ViewBag.RRDATE = dr["RRDATE"];
                    ViewBag.BLNumber = dr["BLNumber"];
                    ViewBag.UPINO = dr["UPINumber"];
                    ViewBag.Registered = dr["TyepReg"];
                    ViewBag.Datetime = Convert.ToDateTime(DateTime.Now).ToString("dd MM yyyy");
                    ViewBag.TIme = Convert.ToDateTime(DateTime.Now).ToString("HH:mm");



                }


            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();
            ViewBag.chargesDetails = tblchargesDetails.AsEnumerable();
            ViewBag.ShippingDetails = tblshippingdetails.AsEnumerable();
            ViewBag.AnexDetails = tblAnxedetails.AsEnumerable();

            foreach (DataRow data in tblchargesDetails.Rows)
            {
                Amount = Convert.ToDouble(data["Amount"]);
                Discount = Convert.ToDouble(data["Discount"]);
                NetAmount = Convert.ToDouble(data["NetAmount"]);
                Srate = Convert.ToString(data["Srate"]);
                CRate = Convert.ToString(data["CRate"]);
                IRate = Convert.ToString(data["IRate"]);
                SGST = Convert.ToDouble(data["SGST"]);
                CGST = Convert.ToDouble(data["CGST"]);
                IGST = Convert.ToDouble(data["IGST"]);
            }

            foreach (DataRow data in tblAnxedetails.Rows)
            {
                GrandAmount = GrandAmount + Convert.ToDouble(data["Amt"]);

            }

            ViewBag.Amount = Amount;
            ViewBag.Discount = Discount;
            ViewBag.NetAmount = NetAmount;
            ViewBag.Srate = Srate;
            ViewBag.CRate = CRate;
            ViewBag.IRate = IRate;
            ViewBag.SGST = SGST;
            ViewBag.CGST = CGST;
            ViewBag.IGST = IGST;


            ViewBag.GrandToatls = GrandAmount;
            Session["alertCount"] = GrandAmount;

            return PartialView();

        }

        [HttpPost]
        public ActionResult CancelExportAssessmentDetails(string remarks, string Assessno, string WorkYear)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = reportprovider.CancelExportAssessmentDetails(remarks, Assessno, WorkYear, userId);
            return Json(message);
        }





        //Export Gate pass Details

        public ActionResult GenerateExportGatePass()
        {

            //List<BE.ExportGatePassEntities> CustomerName = new List<BE.ExportGatePassEntities>();
            //CustomerName = reportprovider.GetAGName();
            ////AccountID And Name Is Entities same as entities
            //ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");

            List<BE.ExportGatePassEntities> Transport = new List<BE.ExportGatePassEntities>();
            Transport = reportprovider.Transport();
            //AccountID And Name Is Entities same as entities
            ViewBag.Transport = new SelectList(Transport, "TransporterID", "TrasporterName");

            return View();
        }

        public JsonResult GetReceiptEntryDoneExport(string ContainernoCheck,string EntryIdCheck)
        {

            string message = "";

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportGatePassEntities> receiptEntries = new List<BE.ExportGatePassEntities>();
            dataTable = db.sub_GetDatatable("USP_EXP_Validate_Receipt '" + EntryIdCheck + "','" + ContainernoCheck + "'");
            if (dataTable.Rows.Count <= 0)
            {
                message = "Credit/Receipt is not generated. Cannot proceed.";
            }


            return Json(message);
        }


        public JsonResult GetContainerDetsForExport(string ContainerNo)
        {

            //string SBNo =""

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportGatePassEntities> receiptEntries = new List<BE.ExportGatePassEntities>();
            dataTable = db.sub_GetDatatable("SP_SearchExportShowContainer_Search '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportGatePassEntities receiptEntry = new BE.ExportGatePassEntities();
                    receiptEntry.Size = Convert.ToString(row["size"]);
                    receiptEntry.Type = Convert.ToString(row["ContainerType"]);
                    receiptEntry.SLName = Convert.ToString(row["LineName"]);
                    receiptEntry.VesselName = Convert.ToString(row["vessel"]);
                    receiptEntry.CustomSeal = Convert.ToString(row["customseal"]);
                    receiptEntry.AgentSeal = Convert.ToString(row["Agentseal"]);
                    receiptEntry.AGName = Convert.ToString(row["customer"]);
                    receiptEntry.AGID = Convert.ToInt32(row["agencyID"]);
                    receiptEntry.entryID = Convert.ToString(row["entryID"]);
                    receiptEntry.ViaNo = Convert.ToString(row["ViaNo"]);
                    receiptEntry.TransType = Convert.ToString(row["Transport_Type"]);
                    receiptEntry.GrossWeight = Convert.ToString(row["GrossWeight"]);
                    receiptEntry.Remark = Convert.ToString(row["Remark"]);
                    receiptEntry.Vehicle = Convert.ToString(row["TRAILERNO"]);
                    receiptEntry.VehicleID = Convert.ToInt32(row["trailerID"]);
                    var dtfetch = new DataTable();
                    string strSQL = "";
                    strSQL = "";
                    strSQL = "USP_SUM_EXP_Cargo_Wt " + (receiptEntry.entryID) + ", '" + (ContainerNo) + "'";
                    dtfetch = db.sub_GetDatatable(strSQL);
                    if (dtfetch.Rows.Count > 0)
                    {
                        receiptEntry.cargowt = Convert.ToString(dtfetch.Rows[0][0]);
                        receiptEntry.TareWeight = Convert.ToString(row["TareWeight"]);
                    }
                    {
                        strSQL = "";
                        strSQL = "SELECT TOP 1 customSeal CSEALNO, agentseal ASEALNO FROM Exp_CLP WHERE CONTAINERNO ='" + (ContainerNo) + "' AND ENTRYID = " + (receiptEntry.entryID) + " AND ISCANCEL=0 ";
                        dtfetch = db.sub_GetDatatable(strSQL);
                        if (dtfetch.Rows.Count > 0)
                        {

                            receiptEntry.AgentSeal = Convert.ToString(row["Agentseal"]);
                            receiptEntry.CustomSeal = Convert.ToString(row["customseal"]);
                        }
                    }


                    receiptEntries.Add(receiptEntry);
                }
            }



            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        
        public JsonResult ImportLoadedSave(List<BE.ExportGatePassEntities> Debitdata, string TxtTranstype)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("GrossWeight");
            dataTable.Columns.Add("AGID");
            dataTable.Columns.Add("VesselName");
            dataTable.Columns.Add("CustomSeal");
            dataTable.Columns.Add("AgentSeal");
            dataTable.Columns.Add("Wagon");
            dataTable.Columns.Add("Train");
            dataTable.Columns.Add("Remark");
            dataTable.Columns.Add("Vehicle");
            dataTable.Columns.Add("VehicleID");
            dataTable.Columns.Add("TransporterID");
            dataTable.Columns.Add("ViaNo");
            dataTable.Columns.Add("NetWeight");
            dataTable.Columns.Add("entryID");
            foreach (BE.ExportGatePassEntities item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Type"] = item.Type;
                row["GrossWeight"] = item.GrossWeight;
                row["AGID"] = item.AGID;
                row["VesselName"] = item.VesselName;
                row["CustomSeal"] = item.CustomSeal;
                row["AgentSeal"] = item.AgentSeal;
                row["Wagon"] = item.Wagon;
                row["Train"] = item.Train;
                row["Remark"] = item.Remark;
                row["Vehicle"] = item.Vehicle;
                row["VehicleID"] = item.VehicleID;
                row["TransporterID"] = item.TransporterID;
                row["ViaNo"] = item.ViaNo;
                row["NetWeight"] = item.NetWeight;
                row["entryID"] = item.entryID;

                dataTable.Rows.Add(row);
            }

            message = reportprovider.SaveLoadedGatepassdetails(dataTable, UserID, TxtTranstype);

            return Json(message);
        }

        public ActionResult PrintExportGatePass(string gpno)
        {
            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getInDL = db.sub_GetDatatable("USP_Export_Gatepass_Print '" + gpno + "'");

            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.GPCode = dr["GPCode"];
                    ViewBag.GPDate = dr["GPDate"];
                    ViewBag.trailerno = dr["trailerno"];
                    ViewBag.Vessel = dr["Vessel"];
                    ViewBag.TransName = dr["TransName"];
                    ViewBag.gpviano = dr["gpviano"];
                    ViewBag.AGName = dr["AGName"];
                    ViewBag.POD = dr["POD"];
                    ViewBag.shippername = dr["shippername"];
                    ViewBag.Port = dr["Port"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.SBNumber = dr["SBNumber"];
                    ViewBag.drivername = dr["drivername"];
                    ViewBag.Transport_Type = dr["Transport_Type"];
                    ViewBag.TrainNo = dr["TrainNo"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.PrintDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                    ViewBag.PrintTime = Convert.ToDateTime(DateTime.Now).ToString("HH:mm:ss");

                    ViewBag.ImportloadedDetails = getInDL.AsEnumerable();
                }
            }
            return PartialView();
        }

        public ActionResult GetExportGatePassSeaech(string Fromdate,string Todate)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("SP_SearchExpGatePassContainer_Search '" + Fromdate + "','" + Todate + "'");
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [HttpPost]
        public ActionResult SaveExportSlabDetails(List<BE.SlabDetailsEntites> Invoicedata, string TariffID, List<BE.TariffAddDetailsEntites> DeliveryType1, string Accounting, string AccountingID,
            string Location,string StuffLocation)
        {


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SlabOn");
            dataTable.Columns.Add("RangeFrom");
            dataTable.Columns.Add("RangeTo");
            dataTable.Columns.Add("Value");
            dataTable.Columns.Add("SlabSize");
            dataTable.Columns.Add("SlabCargoType");
            dataTable.Columns.Add("Location");

            foreach (BE.SlabDetailsEntites item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();
                row["SlabOn"] = item.SlabOn;
                row["RangeFrom"] = item.RangeFrom;
                row["RangeTo"] = item.RangeTo;
                row["Value"] = item.Value;
                row["SlabSize"] = item.SlabSize;
                row["SlabCargoType"] = item.SlabCargoType;
                row["Location"] = item.Location;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            List<BE.TariffAddDetailsEntites> Getdetails1 = new List<BE.TariffAddDetailsEntites>();
            DataTable dataTable1 = new DataTable();

            dataTable1.Columns.Add("DeliveryType");
            foreach (BE.TariffAddDetailsEntites item in DeliveryType1)
            {
                DataRow row = dataTable1.NewRow();

                row["DeliveryType"] = item.DeliveryType;
                dataTable1.Rows.Add(row);
            }

            Getdetails = reportprovider.SaveExportCargoDetails(dataTable, Userid, TariffID, dataTable1, Accounting, AccountingID, Location, StuffLocation);


            //Data To insert into 
            DataTable dataTableadd = new DataTable();


            dataTableadd.Columns.Add("TariffID");
            dataTableadd.Columns.Add("DeliveryType11");
            dataTableadd.Columns.Add("From");
            dataTableadd.Columns.Add("TO");
            dataTableadd.Columns.Add("Accounting");
            dataTableadd.Columns.Add("Size");
            dataTableadd.Columns.Add("Type1");
            dataTableadd.Columns.Add("Ftype");
            dataTableadd.Columns.Add("Slabid");
            dataTableadd.Columns.Add("Insurance");
            dataTableadd.Columns.Add("FixedAmt");
            dataTableadd.Columns.Add("rate");
            dataTableadd.Columns.Add("Tax");
            dataTableadd.Columns.Add("InvoiceType");
            dataTableadd.Columns.Add("CurrencyType");
            dataTableadd.Columns.Add("TransType");
            dataTableadd.Columns.Add("Port");
            dataTableadd.Columns.Add("Freedays");
            dataTableadd.Columns.Add("Location");
            dataTableadd.Columns.Add("StuffLocation");
            dataTableadd.Columns.Add("Jotype");
            dataTableadd.Columns.Add("ScanType");
            dataTableadd.Columns.Add("AccountingID");
            dataTableadd.Columns.Add("Days"); ;


            foreach (BE.TariffAddDetailsEntites item in Getdetails)
            {
                DataRow row1 = dataTableadd.NewRow();

                row1["TariffID"] = item.TariffID;
                row1["DeliveryType11"] = item.DeliveryType;
                row1["From"] = item.From;
                row1["TO"] = item.TO;
                row1["Accounting"] = item.Accounting;
                row1["Size"] = item.Size;
                row1["Type1"] = item.Type;
                row1["Ftype"] = item.Ftype;
                row1["Slabid"] = item.Slabid;
                row1["Insurance"] = item.Insurance;
                row1["FixedAmt"] = item.FixedAmt;
                row1["rate"] = item.rate;
                row1["Tax"] = item.Tax;
                row1["InvoiceType"] = item.InvoiceType;
                row1["CurrencyType"] = item.CurrencyType;
                row1["TransType"] = item.TransType;
                row1["Port"] = item.Port;
                row1["Freedays"] = item.Freedays;
                row1["Location"] = item.Location;
                row1["StuffLocation"] = item.StuffLocation;
                row1["Jotype"] = StuffLocation;
                row1["ScanType"] = item.ScanType;
                row1["AccountingID"] = item.AccountingID;
                row1["Days"] = item.Days;

                dataTableadd.Rows.Add(row1);
            }
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            Getdetails1 = reportprovider.Export1SavedataForGetdata(dataTableadd, userId);

            //data to end insert

            var jsonResult = Json(Getdetails1, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public JsonResult GetExportAdditionalcharges(string SearchOn, string number)
        {
            List<BE.ImportInvoiceContainerDetails> GstList = new List<BE.ImportInvoiceContainerDetails>();
            GstList = reportprovider.GetExportAdditionalcharges(SearchOn, number);
            var jsonResult = Json(GstList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult SaveExportadditionalchargesdetails(List<BE.ImportInvoiceContainerDetails> Invoicedata, string WorkorderNo, string Workorderyear,
          string accountheadID, string additionnarritioin, string Number)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("jono");
            dataTable.Columns.Add("COntainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Containertype");
            dataTable.Columns.Add("amount");
            foreach (BE.ImportInvoiceContainerDetails item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["jono"] = item.JONo;
                row["COntainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Containertype"] = item.Cargotype;
                row["amount"] = item.Amount;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.ExportSaveadditionalcharges(dataTable, WorkorderNo, Workorderyear, accountheadID, additionnarritioin, Number, Userid);
            return Json(message);

        }

        public JsonResult GetExportsummaryaddtionalcharges()
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_ExportShowAdditionalDetails");
            Session["showadditionialcharges"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult ExportCanceladditionalcharges(string WONO)
        {
            string message = "";
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_ExportCanceladditionaldetails " + Userid + ",'" + WONO + "'");




            return Json(message);


        }
        public JsonResult  CheckStuffingTallyDone(string StuffingRequestID)
        {
            string message = "";
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_CheckStuffingTally_Done " + StuffingRequestID + "");


            if (dt.Rows.Count > 0)
            {
                message = "Record already saved for this Request ID Please check. cannot proceed!";
            }
            else
            {
                message = "";
            }

            return Json(message);


        }


        public ActionResult FileDataEntry()
        {

            return View();
        }
        //END HERE

        //file entry form
        public JsonResult SaveFileDetails(string Search, string IGMNo, string ItemNO, string SBNo, string Containerno, string NOCNo, string BOENo, string RefrenceNo, string FileNo, string Remarks)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("USP_File_Data_Entry_M '" + Search + "','" + IGMNo + "','" + ItemNO + "','" + SBNo + "','" + Containerno + "','" + NOCNo + "','" + BOENo + "','" + RefrenceNo + "','" + FileNo + "','" + Remarks + "','" + userId + "'");

            if (retVal > 0)
            {
                Message = "Record saved successfully.";
            }
            else
            {
                Message = "Record not saved successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetIGmdetails(string IGMNo, string ItemNO)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            tblInvoiceList = db.sub_GetDatatable("USP_CheclIGMNOValid '" + IGMNo + "','" + ItemNO + "'");

            if (tblInvoiceList.Rows.Count > 0)
            {
                Message = "1";
            }
            else
            {
                Message = "0";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FileDataEntryReport(string FromDate, string ToDate)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable ActivityOBJ = new DataTable();

            ActivityOBJ = db.sub_GetDatatable("USP_GetFileEntryDetails '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "', '" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["ExpInvList"] = ActivityOBJ;
            var summaryDet = JsonConvert.SerializeObject(ActivityOBJ);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult FileDataEntryExcel()
        {
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable obj = (DataTable)Session["ExpInvList"]; //object.columns.remove("column_name");
                                                              // obj.Columns.Remove("Edit");s
            GridView gridview = new GridView();
            gridview.DataSource = obj;
            gridview.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Data_File_Entry_Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>File Entry Details<strong></td></tr>");
                    //htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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


        [HttpPost]
        public ActionResult ExportCargoDetails(string SBNO, List<BE.ContainerCargotypeDetails> Invoicedata)
        {
            string message = "";


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("entryID");
            dataTable.Columns.Add("containerno");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Cargotype");
            dataTable.Columns.Add("SBNO");
         
            foreach (BE.ContainerCargotypeDetails item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["entryID"] = item.entryID;
                row["containerno"] = item.containerno;
                row["Size"] = item.Size;
                row["Cargotype"] = item.Cargotype;
                row["SBNO"] = item.SBNO;
               
                dataTable.Rows.Add(row);
            }




            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = reportprovider.UpdateCargoDetailsForExport(dataTable,SBNO);



            return Json(message);
        }


        public JsonResult GetInvoiceTyprForSlabDetailsExport(int AccountID)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_ShowAccountDetailsExport '" + AccountID + "'");
            string message = "";
            string message1 = "";

            if (dt.Rows.Count > 0)
            {
                message = Convert.ToString(dt.Rows[0].Field<int>("InvoiceTypeID"));
                message1 = Convert.ToString(dt.Rows[0].Field<int>("TaxID"));
            }



            string Getdetails = " " + message + " ," + message1 + "";


            var jsonResult = Json(Getdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Manish Kumar Code
        public ActionResult WeighmentTariff()
        {
            return View();
        }

        public JsonResult SaveWeighmenttariff(DateTime EFFECTIVEFROM, DateTime EFFECTIVEUPTO, string VEHICLE_TYPE, string TYRES_NO, string VEHICLE_STATUS, string WEIGHT_FROM, string WEIGHT_TO, string NO_OF_CONT, float RATE)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("USP_INSERT_WEIGHTMENT_TARIFF '" + Convert.ToDateTime(EFFECTIVEFROM).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(EFFECTIVEUPTO).ToString("yyyy-MM-dd HH:mm") + "','" + VEHICLE_TYPE + "','" + TYRES_NO + "','" + VEHICLE_STATUS + "','" + WEIGHT_FROM + "','" + WEIGHT_TO + "','" + NO_OF_CONT + "','" + RATE + "'");

            if (retVal > 0)
            {
                Message = "Record saved successfully.";
            }
            else
            {
                Message = "Records not saved successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult WSearch(string fromDate, string ToDate)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_WeighmentDetails '" + fromDate + "','" + ToDate + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //End Manish
        public ActionResult ImportWorkOrder()
        {
            List<BE.VendorMaster> FuelVendorMaster = new List<BE.VendorMaster>();
            FuelVendorMaster = reportprovider.Getvendor();
            ViewBag.VendorName = new SelectList(FuelVendorMaster, "VendorId", "Name");

            List<BE.ExportCartingAllowEntities> Equipment = new List<BE.ExportCartingAllowEntities>();
            Equipment = reportprovider.GetEquipmentType();
            ViewBag.EquipmentList = new SelectList(Equipment, "EnID", "EquipmentType");


            ViewBag.Equi = JsonConvert.SerializeObject(Equipment);



            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = reportprovider.GetCustomer();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");


            List<BE.Surveyor> SurveyorList = new List<BE.Surveyor>();
            SurveyorList = reportprovider.GetSurveyor();
            ViewBag.Surveyor = new SelectList(SurveyorList, "SurveyorId", "SurveyorName");


            List<BE.CHA> Cha = new List<BE.CHA>();
            Cha = reportprovider.Getcha();
            ViewBag.Cha = new SelectList(Cha, "CHANo", "CHAName");
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListImportWorkOrder();
            ViewBag.WOType = new SelectList(WorkOrderList.WOTypeList, "Wo_Type", "Wo_Type");
            return View();
        }
        public JsonResult AjaxGetWorkOrdertype(string WorkType)
        {


            List<BE.Category> Trailerno = new List<BE.Category>();
            Trailerno = reportprovider.getWorkordercategory(WorkType);
            return new JsonResult() { Data = Trailerno, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
        public JsonResult AjxImportWorkDetails(string ContainerNo, string IGMNo, string ItemNo, string Category, string SearchOn)
        {
            BE.FCLDestuffDetailsEntites Igmdetails = new BE.FCLDestuffDetailsEntites();
            List<BE.FCLsummaryDetails> Igmsummary = new List<BE.FCLsummaryDetails>();

            Igmdetails = reportprovider.AjxImportWorkDetails(ContainerNo, IGMNo, ItemNo, Category, SearchOn);
            Igmsummary = Igmdetails.FCLsummaryDetails;
            var returnField = new { List1 = Igmdetails, List2 = Igmsummary };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult WorkOrderTEmpSave(List<BE.WorkOrderReport> Containerdetails, string Containerno1)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("JoNo");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("DeliveryType");
            dataTable.Columns.Add("IGMQty");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("Unit");

            dataTable.Columns.Add("DestuffQty");
            dataTable.Columns.Add("ShortQty");

            dataTable.Columns.Add("Examine");
            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("EquipmentID");
            dataTable.Columns.Add("Remarks");
            dataTable.Columns.Add("Hours");
            dataTable.Columns.Add("CGM");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("Userid");
            dataTable.Columns.Add("MaterialQty");
            dataTable.Columns.Add("MaterialDescriptions");
            foreach (BE.WorkOrderReport item in Containerdetails)
            {
                DataRow row = dataTable.NewRow();
                row["JoNo"] = item.JONo;
                row["ContainerNo"] = item.Containerno;
                row["Size"] = item.Size;
                row["DeliveryType"] = item.DeliveryType;
                row["IGMQty"] = item.IGMQty;
                row["Unit"] = item.Unit;
                row["EntryID"] = item.EntryID;
                row["DestuffQty"] = item.DestuffQty;
                row["ShortQty"] = item.ShortQty;
                row["Weight"] = item.Weight;
                row["Examine"] = item.Examine;
                row["EquipmentID"] = item.Equipment;
                row["Remarks"] = item.Remarks;
                row["Hours"] = item.Hours;
                row["CGM"] = item.CGM;
                row["VehicleNo"] = item.VehicleNo;
                row["Userid"] = UserID;
                row["MaterialQty"] = item.MaterialQty;
                row["MaterialDescriptions"] = item.MaterialDescriptions;

                dataTable.Rows.Add(row);
            }
            List<BE.FCLsummaryDetails> Igmsummary = new List<BE.FCLsummaryDetails>();

            Igmsummary = reportprovider.SaveTempWorkOrder(dataTable, Containerno1, UserID);

            var returnField = new { List2 = Igmsummary };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult ClearTempWorkorder()
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("delete from  temp_Work_Order_d where userid='" + UserID + "' ");
            string message = "";
            return Json(message);
        }
        public JsonResult WorkOrderSave(List<BE.WorkOrderReport> Containerdetails, string DestuffDate, string Category, string WOType, string CHAID, string Vendorname, string Surveyor, string NocNo,string DoValidate)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            if (CHAID == null)
            {
                CHAID = "0";
            }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("JoNo");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("DestuffQty");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("Examine");
            dataTable.Columns.Add("EquipmentID");
            dataTable.Columns.Add("Remarks");
            dataTable.Columns.Add("Hours");
            dataTable.Columns.Add("CGM");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("IGMQty");
            dataTable.Columns.Add("igmWeight");
            dataTable.Columns.Add("MaterialQty");
            dataTable.Columns.Add("MaterialDescriptions");
            dataTable.Columns.Add("Unit");
            foreach (BE.WorkOrderReport item in Containerdetails)
            {
                DataRow row = dataTable.NewRow();
                row["JoNo"] = item.JONo;
                row["ContainerNo"] = item.Containerno;
                row["Size"] = item.Size;
                row["DestuffQty"] = item.DestuffQty;
                row["Weight"] = item.Weight;
                row["Examine"] = item.Examine;
                row["EquipmentID"] = item.Equipment;
                row["Remarks"] = item.Remarks;
                row["Hours"] = item.Hours;
                row["CGM"] = item.CGM;
                row["VehicleNo"] = item.VehicleNo;
                row["IGMQty"] = item.IGMQty;
                row["igmWeight"] = item.igmWeight;
                row["MaterialQty"] = item.MaterialQty;
                row["MaterialDescriptions"] = item.MaterialDescriptions;
                row["Unit"] = item.Unit;
                dataTable.Rows.Add(row);
            }

            string message = reportprovider.WorkOrder(dataTable, Containerdetails, DestuffDate, Category, WOType, CHAID, Vendorname, UserID, Surveyor, NocNo, DoValidate);
            return Json(message);
        }
        public ActionResult Printworkorder(string GPNo, string Workyear)
        {
            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getInDL = db.sub_GetDatatable("usp_Print_workOrder '" + GPNo + "','" + Workyear + "'");

            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {
                    ViewBag.IGMNo = dr["IGMNo"];
                    ViewBag.SrNo = dr["Sr No"];
                    ViewBag.WO_NO = dr["WO_NO"];
                    ViewBag.AddedOn = dr["AddedOn"];
                    ViewBag.ItemNo = dr["ItemNo"];
                    ViewBag.ContainerNo = dr["ContainerNo"];
                    ViewBag.Size = dr["Size"];
                    ViewBag.PKGSDestuff = dr["PKGSDestuff"];
                    ViewBag.ExamPer = dr["ExamPer"];
                    ViewBag.Remarks = dr["Remarks"];
                    ViewBag.con_Name = dr["con_Name"];
                    ViewBag.AddressI = dr["AddressI"];
                    ViewBag.Weight = dr["Weight"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.EquipmentName = dr["EquipmentName"];
                    ViewBag.Wo_Type = dr["Wo_Type"];
                    ViewBag.IGM_GoodsDesc = dr["IGM_GoodsDesc"];
                    ViewBag.Consignee = dr["Consignee"];
                    ViewBag.IGM_Qty = dr["IGM_Qty"];
                    ViewBag.IGM_GrossWt = dr["IGM_GrossWt"];
                    ViewBag.indate = dr["indate"];
                    ViewBag.SurveyorName = dr["SurveyorName"];
                    ViewBag.VendorName = dr["VendorName"];
                    ViewBag.BLNO = dr["IGM_BLNo"];

                    ViewBag.SLName = dr["SLName"];
                    ViewBag.BOENo = dr["BOENo"];
                    ViewBag.DoValidate = dr["DoValidate"];
                    ViewBag.ImportFCLdetailsCustomtally = getInDL.AsEnumerable();
                }
            }
            return PartialView();
        }
        public JsonResult GetWorkorder(string certeria, string igmno, string itemno,string Container)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_OPEN_WORK_ORDER_IMPORT '" + certeria + "','" + igmno + "','" + itemno + "','" + Container + "'");
            string json = JsonConvert.SerializeObject(dt);
            Session["USP_OPEN_WORK_ORDER_IMPORT"] = dt;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public ActionResult ExportToExcelGetWorkorder()
        {

            DataTable dt = (DataTable)Session["USP_OPEN_WORK_ORDER_IMPORT"];

            HC.DBOperations db = new HC.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ExportLoadInventory = dt;
            string Tittle = " LOADED TO DESTUFF Report";
            GridView gridview = new GridView();
            gridview.DataSource = ExportLoadInventory;
            gridview.DataBind();
            ////////gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=loaded to destuff.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; ><strong style='font-size: 20px'>" + "SrNo" + " <strong></td></tr>");
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; ><strong style='font-size: 20px'>" + "Date/Year" + " <strong></td></tr>");

                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center '; colspan ='8'><strong style='font-size: 15px'>EXPORT<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center; border:1px solid black'; colspan ='20'><strong style='font-size: 22px'>" + "IMPORT DETAILS" + "<strong></td><td style='font-weight: bold; text-align: center; border:1px solid black'; colspan ='16'><strong style='font-size: 20px'>" + "MTY REPO DETAILS" + " <strong></td><td style='font-weight: bold; text-align: center; border:1px solid black'; colspan ='8'><strong style='font-size: 20px'>" + "EXPORT" + " <strong></td></tr>");
                    //htw.Write("<table><tr></tr>");
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


        public ActionResult EditWorkDetails(string WONo)
        {
            List<BE.FCLDestuffDetailsEntites> GetDetails = new List<BE.FCLDestuffDetailsEntites>();
            GetDetails = reportprovider.GetEditDetails(WONo);

            var jsonResult = Json(GetDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public DataTable GetEditDetailsgrid(string WONo)
        {
            DataTable DlList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DlList = db.sub_GetDatatable("usp_grid_bind_work_order '" + WONo + "'");
            return DlList;

        }
        public JsonResult WorkOrderUpdate(List<BE.WorkOrderReport> Containerdetails, string DestuffDate, string Category, string WOType, string CHAID, string Vendorname, string WoNo)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            if (CHAID == null)
            {
                CHAID = "0";
            }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("JoNo");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("DestuffQty");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("Examine");
            dataTable.Columns.Add("EquipmentID");
            dataTable.Columns.Add("Remarks");
            dataTable.Columns.Add("Hours");
            dataTable.Columns.Add("CGM");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("IGMNo");
            dataTable.Columns.Add("ITEMNO");
            dataTable.Columns.Add("MaterialQty");
            dataTable.Columns.Add("MaterialDescriptions");
            foreach (BE.WorkOrderReport item in Containerdetails)
            {
                DataRow row = dataTable.NewRow();
                row["JoNo"] = item.JONo;
                row["ContainerNo"] = item.Containerno;
                row["Size"] = item.Size;
                row["DestuffQty"] = item.DestuffQty;
                row["Weight"] = item.Weight;
                row["Examine"] = item.Examine;
                row["EquipmentID"] = item.Equipment;
                row["Remarks"] = item.Remarks;
                row["Hours"] = item.Hours;
                row["CGM"] = item.CGM;
                row["VehicleNo"] = item.VehicleNo;
                row["EntryID"] = item.EntryID;
                row["IGMNo"] = item.IGMNo;
                row["ITEMNO"] = item.ITEMNO;
                row["MaterialQty"] = item.MaterialQty;
                row["MaterialDescriptions"] = item.MaterialDescriptions;
                dataTable.Rows.Add(row);
            }

            string message = reportprovider.WorkOrderUpdate(dataTable, Containerdetails, DestuffDate, Category, WOType, CHAID, Vendorname, WoNo, UserID);
            return Json(message);
        }
        public ActionResult EditWorkDetailsGrid(string WONo)
        {
            List<BE.FCLsummaryDetails> GetDetails = new List<BE.FCLsummaryDetails>();
            GetDetails = reportprovider.GetEditDetailsgird(WONo);

            var jsonResult = Json(GetDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult GetCheckIgmItemno(string Igmno,string ItemNo,string ContainerNo)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_Check_IgmItemNo_WorkOrder '" + Igmno + "','" + ItemNo + "','" + ContainerNo + "'");
            string message = "";
            string message1 = "";

            if (dt.Rows[0].Field<int>("ID") != 0)
            {
                message = Convert.ToString(dt.Rows[0].Field<int>("ID"));
          
            }



            string Getdetails = " " + message  + "";


            var jsonResult = Json(Getdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult CancelImportworkOrder(string WO_NO)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable ActivityOBJ = new DataTable();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            ActivityOBJ = db.sub_GetDatatable("USP_ImportCloseWork_order '" + WO_NO + "','" + userId + "'");
            var summaryDet = JsonConvert.SerializeObject(ActivityOBJ);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}