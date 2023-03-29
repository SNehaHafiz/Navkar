using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.EmptyOut;
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
namespace TrackerMVC.Controllers.EmptyOut
{
   /// [UserAuthenticationFilter]
    public class EyardOutController : Controller
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        RP.EmptyOut reportprovider = new RP.EmptyOut();
        // GET: EyardOut
        public ActionResult EyardOut()
        {

            List<BE.EyardOut> ExpGroupDDL = new List<BE.EyardOut>();
            ExpGroupDDL = reportprovider.getLoaction();
            ViewBag.GroupDDL = new SelectList(ExpGroupDDL, "Location", "Location");

            List<BE.EyardOut> SubExpDDL = new List<BE.EyardOut>();
            SubExpDDL = reportprovider.getVessel();
            ViewBag.SubHead = new SelectList(SubExpDDL, "VesselID", "VesselName");

            return View();
        }

        public JsonResult GetIGSTCGST(string BookingNo)        {            CD.DBOperations db = new CD.DBOperations();            DataTable dataTable = new DataTable();            List<BE.EyardOut> receiptEntries = new List<BE.EyardOut>();            dataTable = db.sub_GetDatatable("USP_EYARD_OUT_BOOKINGNO_CHANGE '" + BookingNo + "'");            if (dataTable != null)            {                foreach (DataRow row in dataTable.Rows)                {                    BE.EyardOut receiptEntry = new BE.EyardOut();                    receiptEntry.ShipperName = Convert.ToString(row["shipperName"]);                    receiptEntry.LineName = Convert.ToString(row["LineName"]);                    receiptEntry.Transporter = Convert.ToString(row["TransName"]);                    receiptEntry.POD = Convert.ToString(row["POD"]);                    receiptEntry.VesselID = Convert.ToInt32(row["VesselID"]);                    receiptEntry.ports = Convert.ToString(row["portname"]);                    receiptEntry.SealNo = Convert.ToString(row["SealNo"]);                    receiptEntry.MovementType = Convert.ToString(row["Movement_Type"]);                    receiptEntry.BookingLineID = Convert.ToString(row["lineID"]);                    receiptEntry.Location = Convert.ToString(row["Destination"]);                    receiptEntries.Add(receiptEntry);                }            }            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;        }

        public JsonResult GetContainer(string ContainerNo)        {            CD.DBOperations db = new CD.DBOperations();            DataTable dataTable = new DataTable();            List<BE.EyardOut> receiptEntries = new List<BE.EyardOut>();            dataTable = db.sub_GetDatatable("get_sp_container_fetch_for_Out '" + ContainerNo + "'");            if (dataTable != null)            {                foreach (DataRow row in dataTable.Rows)                {                    BE.EyardOut receiptEntry = new BE.EyardOut();                    receiptEntry.EntryID = Convert.ToInt32(row["ENTRYID"]);                    receiptEntry.Size = Convert.ToString(row["Size"]);                    receiptEntry.ISOCode = Convert.ToString(row["Isocode"]);                    receiptEntry.Type = Convert.ToString(row["Type"]);                    receiptEntry.TypeID = Convert.ToInt32(row["ContainerTypeID"]);                    receiptEntry.txtForwarderLine = Convert.ToString(row["LineI"]);                    receiptEntry.lineid = Convert.ToInt32(row["LineID"]);                    receiptEntry.custo = Convert.ToInt32(row["AgentId"]);                    receiptEntry.DamageRemarks = Convert.ToString(row["damageRemarks"]);                                        receiptEntries.Add(receiptEntry);                }            }            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;        }

        public ActionResult LorryReciept()
        {
            List<BE.EyardOut> Activity = new List<BE.EyardOut>();
            Activity = reportprovider.getActivity();
            ViewBag.ActivityL = new SelectList(Activity, "AutoID", "Activity");

            List<BE.EyardOut> LineName = new List<BE.EyardOut>();
            LineName = reportprovider.getLineName();
            ViewBag.LineName = new SelectList(LineName, "SLID", "SLName");

            List<BE.EyardOut> Type = new List<BE.EyardOut>();
            Type = reportprovider.getType();
            ViewBag.Type = new SelectList(Type, "ContainerTypeID", "ContainerType");


            List<BE.EyardOut> Customer = new List<BE.EyardOut>();
            Customer = reportprovider.getCustomer();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGNAME");


            List<BE.EyardOut> Location = new List<BE.EyardOut>();
            Location = reportprovider.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Locations");


            List<BE.EyardOut> Transporter = new List<BE.EyardOut>();
            Transporter = reportprovider.getTransporter();
            ViewBag.Transporter = new SelectList(Transporter, "TransID", "TransName");


            return View();
            
        }

        public JsonResult LorryBooking(string BookingNo)        {            CD.DBOperations db = new CD.DBOperations();            DataTable dataTable = new DataTable();            List<BE.EyardOut> receiptEntries = new List<BE.EyardOut>();            dataTable = db.sub_GetDatatable("sp_SearchByBookingNo '" + BookingNo + "'");            if (dataTable != null)            {                foreach (DataRow row in dataTable.Rows)                {                    BE.EyardOut receiptEntry = new BE.EyardOut();                    receiptEntry.cmbline = Convert.ToInt32(row["lineid"]);                    receiptEntry.cmbagent = Convert.ToInt32(row["agencyid"]);                    receiptEntry.cmbfrom = Convert.ToInt32(row["from location"]);                    receiptEntry.cmbto = Convert.ToInt32(row["pickuplocid"]);                    receiptEntries.Add(receiptEntry);                }            }            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;        }


        public JsonResult LorryContainer(string ContainerNo,string ddlActivity)        {            CD.DBOperations db = new CD.DBOperations();            DataTable dataTable = new DataTable();            List<BE.EyardOut> receiptEntries = new List<BE.EyardOut>();            dataTable = db.sub_GetDatatable("sp_SearchContainerNo '" + ContainerNo + "','" + ddlActivity + "'");            if (dataTable != null)            {                foreach (DataRow row in dataTable.Rows)                {                    BE.EyardOut receiptEntry = new BE.EyardOut();                    receiptEntry.cmbActivity = Convert.ToString(row["Activity"]);                    receiptEntry.cmbsize = Convert.ToString(row["Size"]);                    receiptEntry.cmbsize1 = Convert.ToString(row["Size1"]);                    receiptEntry.cmbtype = Convert.ToString(row["Type"]);                    receiptEntry.cmbline = Convert.ToInt32(row["slid"]);                    receiptEntry.cmbagent = Convert.ToInt32(row["Customer Name"]);                   ///receiptEntry.cmbfrom = Convert.ToInt32(row["From Location"]);                    //receiptEntry.cmbto = Convert.ToInt32(row["To Location"]);                    receiptEntry.txtagentseal = Convert.ToString(row["Seal No"]);                    receiptEntry.txtboeno = Convert.ToString(row["BOE No"]);
                    receiptEntry.txtwt = Convert.ToString(row["Weight"]);
                    receiptEntry.txtconsignee = Convert.ToString(row["Consignee"]);
                    receiptEntry.BatchPkgs = Convert.ToString(row["BatchPkgs"]);
                    receiptEntry.Commodity = Convert.ToString(row["Commodity"]);
                    receiptEntry.ComInvoiceNo = Convert.ToString(row["ComInvoiceNo"]);
                    receiptEntry.TrainNo = Convert.ToString(row["TrainNo"]);
                    receiptEntries.Add(receiptEntry);                }            }            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;        }

        public JsonResult LorryContainers(string ContainerNo, string AllowID, string BookingNo)        {            CD.DBOperations db = new CD.DBOperations();            DataTable dataTable = new DataTable();            List<BE.EyardOut> receiptEntries = new List<BE.EyardOut>();            dataTable = db.sub_GetDatatable("usp_check_exp_do_allotment '" + ContainerNo + "','" + AllowID +"','" + BookingNo + "'");            if (dataTable != null)            {                foreach (DataRow row in dataTable.Rows)                {                    BE.EyardOut receiptEntry = new BE.EyardOut();                    //receiptEntry.cmbActivity = Convert.ToString(row["Activity"]);                    receiptEntry.cmbsize = Convert.ToString(row["Size"]);                    receiptEntry.cmbtype = Convert.ToString(row["ContainerTypeID"]);                    receiptEntry.cmbline = Convert.ToInt32(row["slid"]);
                    receiptEntry.txttareWt = Convert.ToString(row["TareWeight"]);                   

                    receiptEntries.Add(receiptEntry);                }            }            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;            return jsonResult;        }

        public ActionResult SavePartyDebitEntryDetails(List<BE.EyardOut> Debitdata, int Lrno, string LrDate, string ShippingLine,   string Customer,
     string Consignee)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Types");
            dataTable.Columns.Add("TrailerNo");
            dataTable.Columns.Add("AgentSeal");
            dataTable.Columns.Add("BookingNo");
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("FromLocation");
            dataTable.Columns.Add("ToLocation");
            dataTable.Columns.Add("BOENo");
            dataTable.Columns.Add("Pkgs");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("TareWeight");
            dataTable.Columns.Add("Commodity");
            dataTable.Columns.Add("Remarks");
            dataTable.Columns.Add("EWayBillNo");
            dataTable.Columns.Add("CoilSizeDescriptions");
            dataTable.Columns.Add("TrainNo");
            dataTable.TableName = "LorryReceipt";

            foreach (BE.EyardOut item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Types"] = item.Type;
                row["TrailerNo"] = item.TrailerNo;
                row["AgentSeal"] = item.AgentSeal;
                row["BookingNo"] = item.BookingNo;
                row["Activity"] = item.Activity;
                row["FromLocation"] = item.FromLocation;
                row["ToLocation"] = item.ToLocation;
                row["BOENo"] = item.BOENo;
                row["Pkgs"] = item.Pkgs;
                row["Weight"] = item.Weight;
                row["TareWeight"] = item.TareWeight;
                row["Commodity"] = item.Commodity;
                row["Remarks"] = item.Remarks;
                row["EWayBillNo"] = item.EWayBillNo;
                row["CoilSizeDescriptions"] = item.CoilSizeDescriptions;
                row["TrainNo"] = item.TrainNo;

                dataTable.Rows.Add(row);
            }

            //DB.DBConnections db1 = new DA.DBConnections();
            //string strSql = "SELECT isnull(MAX(DocumentId),0)  as DocumentId FROM PCS_DOC_HEADER_ICREP WITH (nolock)";
            //DataTable dt1 = db1.sub_GetDatatable(strSql);
            //if (dt1.Rows.Count > 0)
            //{
            //    newDocumentID = Convert.ToInt32(dt1.Rows[0][0].ToString());
            //}

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.SavepartyDebitEntry(dataTable, Lrno, LrDate, ShippingLine, Customer, Consignee,  Userid);
            return Json(message);

        }
        public ActionResult GetTrackLorryReceiptReport(string FromDate, string ToDate, string category, string Size)
        {
            DataTable GetLorryReceipt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetLorryReceipt = db.sub_GetDatatable("SP_ShowLorryReceiptReport '" + FromDate + "','" + ToDate + "','" + category + "','" + Size + "'");
            Session["GetLorryReceipt"] = GetLorryReceipt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(GetLorryReceipt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;



        }

        //code by suraj
        public ActionResult DomestichubLorryReciept()
        {
            List<BE.EyardOut> Activity = new List<BE.EyardOut>();
            Activity = reportprovider.getActivity();
            ViewBag.ActivityL = new SelectList(Activity, "AutoID", "Activity");

            List<BE.EyardOut> LineName = new List<BE.EyardOut>();
            LineName = reportprovider.getLineName();
            ViewBag.LineName = new SelectList(LineName, "SLID", "SLName");

            List<BE.EyardOut> Type = new List<BE.EyardOut>();
            Type = reportprovider.getType();
            ViewBag.Type = new SelectList(Type, "ContainerTypeID", "ContainerType");


            List<BE.EyardOut> Customer = new List<BE.EyardOut>();
            Customer = reportprovider.getCustomer();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGNAME");


            List<BE.EyardOut> Location = new List<BE.EyardOut>();
            Location = reportprovider.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Locations");


            List<BE.EyardOut> Transporter = new List<BE.EyardOut>();
            Transporter = reportprovider.getTransporter();
            ViewBag.Transporter = new SelectList(Transporter, "TransID", "TransName");

            return View();

        }


        public ActionResult GetDomesticSearch(string Search)
        {
            DataTable GetLorryReceipt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetLorryReceipt = db.sub_GetDatatable("Usp_Lr_Search '" + Search + "'");
            var json = JsonConvert.SerializeObject(GetLorryReceipt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;



        }


        public ActionResult DomesticHubUpdate(List<BE.EyardOut> Debitdata, string Lrno, string LrDate, string ShippingLine, string Customer,
    string Consignee)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Types");
            dataTable.Columns.Add("TrailerNo");
            dataTable.Columns.Add("AgentSeal");
            dataTable.Columns.Add("BookingNo");
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("FromLocation");
            dataTable.Columns.Add("ToLocation");
            dataTable.Columns.Add("BOENo");
            dataTable.Columns.Add("Pkgs");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("TareWeight");
            dataTable.Columns.Add("Commodity");
            dataTable.Columns.Add("Remarks");

            dataTable.TableName = "Domestic_Hub_LorryReceipt";

            foreach (BE.EyardOut item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Types"] = item.Type;
                row["TrailerNo"] = item.TrailerNo;
                row["AgentSeal"] = item.AgentSeal;
                row["BookingNo"] = item.BookingNo;
                row["Activity"] = item.Activity;
                row["FromLocation"] = item.FromLocation;
                row["ToLocation"] = item.ToLocation;
                row["BOENo"] = item.BOENo;
                row["Pkgs"] = item.Pkgs;
                row["Weight"] = item.Weight;
                row["TareWeight"] = item.TareWeight;
                row["Commodity"] = item.Commodity;
                row["Remarks"] = item.Remarks;



                dataTable.Rows.Add(row);
            }

            //DB.DBConnections db1 = new DA.DBConnections();
            //string strSql = "SELECT isnull(MAX(DocumentId),0)  as DocumentId FROM PCS_DOC_HEADER_ICREP WITH (nolock)";
            //DataTable dt1 = db1.sub_GetDatatable(strSql);
            //if (dt1.Rows.Count > 0)
            //{
            //    newDocumentID = Convert.ToInt32(dt1.Rows[0][0].ToString());
            //}

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.DomesticUpdateHub(dataTable, Lrno, LrDate);
            return Json(message);

        }

        public ActionResult GetTrackLorryReceipt(BE.LorryReceiptReport data)
        {
            List<BE.LorryReceiptReport> LorryReceiptReport = new List<BE.LorryReceiptReport>();

            LorryReceiptReport = reportprovider.getLorryReceiptReport(data.FromDate, data.ToDate, data.category, data.Size);
            Session["FromDate"] = data.FromDate;
            Session["Todate"] = data.ToDate;
            Session["category"] = data.category;
            Session["search"] = data.Size;
            var jsonResult = Json(LorryReceiptReport, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult PrintDomesticLorryReceipt(string LRNo)
        {


            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();

            float cargocount = 0;
            float cargoWeightcount = 0;
            CD.DBOperations db = new CD.DBOperations();
            getInDL = db.sub_GetDatatable("USP_GetLorryReceiptPrint '" + LRNo + "'");

            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];

                    ViewBag.Address = "Survey No.44/1, 44/1/2 Village Tumb, Taluka Umbergaon, District Valsad, Gujarat, Pin No-396150";
                    ViewBag.AddressI = "";


                    ViewBag.LRNo = dr["LRNo"];
                    ViewBag.ContainerNo = dr["ContainerNo"];
                    ViewBag.LRDate = dr["LRDate"];
                    ViewBag.Size = dr["Size"];
                    ViewBag.VehicleNo = dr["VehicleNo"];
                    ViewBag.AgentSeal = dr["AgentSeal"];
                    ViewBag.BOENo = dr["BOENo"];
                    ViewBag.Weight = dr["Weight"];
                    ViewBag.Qty = dr["Qty"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.SLName = dr["SLName"];
                    ViewBag.Activity = dr["Activity"];
                    ViewBag.Remarks = dr["Remarks"];
                    ViewBag.DoValiddate = dr["DoValiddate"];
                    ViewBag.EWayBillNo = dr["EWayBillNo"];
                    ViewBag.ContactNo = dr["ContactNo"];
                    ViewBag.Location = dr["From Locaiton"];
                    ViewBag.TOLocation = dr["TO Locaiton"];
                    ViewBag.GSTIN = dr["GSTIN"];
                    ViewBag.Con_For = dr["Con_For"];
                    ViewBag.ImporterName = dr["ImporterName"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.PrintDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                    ViewBag.PrintTime = Convert.ToDateTime(DateTime.Now).ToString("HH:mm");





                    ViewBag.ImportloadedDetails = getInDL.AsEnumerable();
                }
            }
            return PartialView();
        }


    }
}