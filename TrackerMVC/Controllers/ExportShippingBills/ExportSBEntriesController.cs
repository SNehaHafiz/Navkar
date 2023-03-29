using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ExportSBBL;
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

namespace TrackerMVC.Controllers.ExportShippingBills
{
    public class ExportSBEntriesController : Controller
    {
        RP.ExportSBBL BL = new RP.ExportSBBL();
        ///RP. reportprovider = new RP.Chequebounce();
        DB.TrackerMVCDBManager DL = new DB.TrackerMVCDBManager();


        public ActionResult ExportShippingBillEntries()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = BL.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");
            //For Shipper ExportShippingBillEnt Is Entities
            List<BE.ExportShippingBillEnt> Exporter = new List<BE.ExportShippingBillEnt>();
            Exporter = BL.ExporterDDL();
            ViewBag.Exporter = new SelectList(Exporter, "shipperid", "Shippername");
            //CHA LIST
            List<BE.ExportShippingBillEnt> CHAList = new List<BE.ExportShippingBillEnt>();
            CHAList = BL.CHADLL();
            ViewBag.CHAList = new SelectList(CHAList, "CHAID", "CHAName");

            List<BE.ExportShippingBillEnt> CargoList = new List<BE.ExportShippingBillEnt>();
            CargoList = BL.CargoType();
            ViewBag.CargoList = new SelectList(CargoList, "CID", "CType");

            List<BE.ExportShippingBillEnt> pkglist = new List<BE.ExportShippingBillEnt>();
            pkglist = BL.Pkgs();
            ViewBag.pkglist = new SelectList(pkglist, "pkgid", "pkgtype");

            List<BE.ExportShippingBillEnt> PodListDDL = new List<BE.ExportShippingBillEnt>();
            PodListDDL = BL.PodMaster();
            ViewBag.PodList = new SelectList(PodListDDL, "PodID", "PODName");

            List<BE.ExportShippingBillEnt> CommodityList = new List<BE.ExportShippingBillEnt>();
            CommodityList = BL.CommodityM();
            ViewBag.Commodity = new SelectList(CommodityList, "ComID", "CommodityName");


            List<BE.ExportShippingBillEnt> StuffList = new List<BE.ExportShippingBillEnt>();
            StuffList = BL.Stuffing();
            ViewBag.Stuff = new SelectList(StuffList, "StuffID", "Stuffing");
            return View();
        }

        public JsonResult Save(BE.ExportShippingBillEnt ExportShippingBillEnt)
        {
            var EntryDate = ExportShippingBillEnt.EntryDate;
            var SbSBDate = ExportShippingBillEnt.SBDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            db.sub_ExecuteNonQuery("USP_WebSBEntry '" + Convert.ToDateTime(ExportShippingBillEnt.EntryDate).ToString("yyyy-MM-dd HH:mm") + "','" + ExportShippingBillEnt.SBNO + "','" + Convert.ToDateTime(ExportShippingBillEnt.SBDate).ToString("yyyy-MM-dd HH:mm") + "','" + ExportShippingBillEnt.InvNo + "','" + ExportShippingBillEnt.CHAID + "','" + ExportShippingBillEnt.shipperid + "','" + ExportShippingBillEnt.AGID + "','" + ExportShippingBillEnt.con + "','" + ExportShippingBillEnt.CargoDesc + "','" + ExportShippingBillEnt.CID + "','" + ExportShippingBillEnt.StuffID + "','" + ExportShippingBillEnt.carQty + "','" + ExportShippingBillEnt.cargowt + "','" + ExportShippingBillEnt.CommodityName + "','" + ExportShippingBillEnt.Remark + "','" + ExportShippingBillEnt.PodID + "','" + ExportShippingBillEnt.pkgid + "','" + ExportShippingBillEnt.FobVal + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + ExportShippingBillEnt.conaddrs + "'");
            return Json(ExportShippingBillEnt);
        }

        // Codes By Sagar 20-05-2020 To Check Duplicate SBno
        [HttpPost]
        //public JsonResult CheckSBNo(string Sbno)
        //{
        //    string Message = "";
        //    Message = BL.CheckSBNo(Sbno);

        //    return Json(Message);
        //}

        public JsonResult Search(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("sp_searchsbno'" + search + "'");
            var summaryGet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryGet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ShippingBamendment()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = BL.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");
            //For Shipper ExportShippingBillEnt Is Entities
            List<BE.ExportShippingBillEnt> Exporter = new List<BE.ExportShippingBillEnt>();
            Exporter = BL.ExporterDDL();
            ViewBag.Exporter = new SelectList(Exporter, "shipperid", "Shippername");
            //CHA LIST
            List<BE.ExportShippingBillEnt> CHAList = new List<BE.ExportShippingBillEnt>();
            CHAList = BL.CHADLL();
            ViewBag.CHAList = new SelectList(CHAList, "CHAID", "CHAName");

            List<BE.ExportShippingBillEnt> CargoList = new List<BE.ExportShippingBillEnt>();
            CargoList = BL.CargoType();
            ViewBag.CargoList = new SelectList(CargoList, "CID", "CType");

            List<BE.ExportShippingBillEnt> pkglist = new List<BE.ExportShippingBillEnt>();
            pkglist = BL.Pkgs();
            ViewBag.pkglist = new SelectList(pkglist, "pkgid", "pkgtype");

            List<BE.ExportShippingBillEnt> PodListDDL = new List<BE.ExportShippingBillEnt>();
            PodListDDL = BL.PodMaster();
            ViewBag.PodList = new SelectList(PodListDDL, "PodID", "PODName");

            List<BE.ExportShippingBillEnt> CommodityList = new List<BE.ExportShippingBillEnt>();
            CommodityList = BL.CommodityM();
            ViewBag.Commodity = new SelectList(CommodityList, "ComID", "CommodityName");


            List<BE.ExportShippingBillEnt> StuffList = new List<BE.ExportShippingBillEnt>();
            StuffList = BL.Stuffing();
            ViewBag.Stuff = new SelectList(StuffList, "StuffID", "Stuffing");
            return View();
        }
        // Codes By Sagar 20-05-2020 To Check Duplicate SBno
        [HttpPost]
        //public JsonResult CheckStuffSBno(string SBnumber)
        //{
        //    string Message = "";
        //    Message = BL.CheckSBNo(SBnumber);
        //    return Json(Message);
        //}

        // get details in text box
        public JsonResult GetSBDetails(string SBNo)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            dataTable = db.sub_GetDatatable("sp_getsbdetails '" + SBNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt getsbdata = new BE.ExportShippingBillEnt();
                   // getsbdata.EntryDate = Convert.ToDateTime(row["EntryDate"]).ToString("yyyy-MM-dd HH:mm");
                    
                    getsbdata.SBDate = Convert.ToDateTime(row["SBDate"]).ToString("yyyy-MM-dd HH:mm");
                    getsbdata.InvNo = Convert.ToString(row["InvoiceNo"]);
                    getsbdata.AGID = Convert.ToInt32(row["AGID"]);
                    getsbdata.CHAID = Convert.ToInt32(row["CHAID"]);
                    getsbdata.shipperid = Convert.ToInt32(row["shipperID"]);
                    getsbdata.PodID = Convert.ToInt32(row["PODID"]);
                    getsbdata.cargowt = Convert.ToInt32(row["CargoWeight"]);
                    getsbdata.con = Convert.ToString(row["consignee"]);
                    getsbdata.conaddrs = Convert.ToString(row["Conaddress"]);
                    getsbdata.EntryID = Convert.ToInt32(row["SBEntryID"]);
                    getsbdata.pkgid = Convert.ToInt32(row["Unit"]);
                    getsbdata.FobVal = Convert.ToInt32(row["FOBValue"]);
                    getsbdata.carQty = Convert.ToInt32(row["TotalPKGS"]);
                    getsbdata.CID = Convert.ToInt32(row["CargoTypeID"]);
                    getsbdata.CargoDesc = Convert.ToString(row["CargoDesc"]);
                    getsbdata.ComID = Convert.ToString(row["Commodity_Group_ID"]);
                    receiptEntries.Add(getsbdata);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

            public  ActionResult VesselCutOff()
        {
            List<BE.VesselCutOff> PodListDDL = new List<BE.VesselCutOff>();
            PodListDDL = BL.PortMaster();
            ViewBag.PodList = new SelectList(PodListDDL, "PortID", "PortName");

            List<BE.VesselCutOff> VesselDDL = new List<BE.VesselCutOff>();
            VesselDDL = BL.Vesseles();
            ViewBag.VesselList = new SelectList(VesselDDL, "VesselID", "VesselName");

            return View();
        }

        public JsonResult FetchVesselDet(string ViaNO)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.VesselCutOff> receiptEntries = new List<BE.VesselCutOff>();
            dataTable = db.sub_GetDatatable("fetchdetailviano '" + ViaNO + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.VesselCutOff receiptEntry = new BE.VesselCutOff();


                    receiptEntry.VesselID = Convert.ToInt32(row["VesselID"]);
                    receiptEntry.Voyage = Convert.ToString(row["Voyage"]);
                    receiptEntry.RotationNo = Convert.ToString(row["RotationNo"]);
                    receiptEntry.CutofDate = Convert.ToString(row["CutofDate"]);
                    receiptEntry.gateopendate = Convert.ToString(row["gateopendate"]);
                    receiptEntry.PortName = Convert.ToString(row["POL"]);

                    receiptEntries.Add(receiptEntry);
                }
            }


            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult AddVessel(BE.VesselCutOff data)
        {


            var EntryDate = data.CutofDate;
            var SbSBDate = data.gateopendate;


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            db.sub_ExecuteNonQuery("SP_VesselMovements '" + data.VesselID + "','" + data.ViaNo + "','" + data.Voyage + "','" + data.RotationNo + "','" + data.PortName+"','" + Convert.ToDateTime(data.CutofDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(data.gateopendate).ToString("yyyy-MM-dd HH:mm") + "', '" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + data.IsOpen+"'");
            
            return Json(data);
        }


        public JsonResult Search1(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("GetViaDetails'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ChageVesselDetails()
        {
            List<BE.VesselCutOff> PodListDDL = new List<BE.VesselCutOff>();
            PodListDDL = BL.PortMaster();
            ViewBag.PodList = new SelectList(PodListDDL, "PortID", "PortName");

            List<BE.VesselCutOff> VesselDDL = new List<BE.VesselCutOff>();
            VesselDDL = BL.Vesseles();
            ViewBag.VesselList = new SelectList(VesselDDL, "VesselID", "VesselName");

            List<BE.VesselCutOff> PODMaster = new List<BE.VesselCutOff>();
            PODMaster = BL.PodMasters();
            ViewBag.POList = new SelectList(PODMaster, "PODID", "PODName");

            return View();
        }

        public JsonResult VesselGrid(string Category , string Search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            List<BE.VesselCutOff> receiptEntries = new List<BE.VesselCutOff>();
            dt = db.sub_GetDatatable("USP_ShowVesselsDetails'" + Category + "','" + Search + "'");
            if (dt != null)
            {
                //ViaNo as [Via No],FPD,POD
                foreach (DataRow row in dt.Rows)
                {
                    BE.VesselCutOff receiptEntry = new BE.VesselCutOff();
                    receiptEntry.Select = Convert.ToString(row["Select"]);
                    receiptEntry.EntryID = Convert.ToUInt16(row["Entry ID"]);
                    receiptEntry.containerNo = Convert.ToString(row["Container No"]);
                    receiptEntry.csize = Convert.ToInt32(row["Size"]);
                    receiptEntry.CType = Convert.ToString(row["Container Type"]);
                    receiptEntry.CargoType = Convert.ToString(row["Cargo Type"]);
                    receiptEntry.SBNO = Convert.ToString(row["SB No"]);
                    receiptEntry.SBEntryID = Convert.ToInt32(row["SBEntryID"]);
                    receiptEntry.ViaNo = Convert.ToString(row["Via No"]);
                    receiptEntry.PortName = Convert.ToString(row["FPD"]);
                    receiptEntry.PODName = Convert.ToString(row["POD"]);


                    receiptEntries.Add(receiptEntry);
                }
            }



            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult FetchVesselDetails(string ViaNO)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.VesselCutOff> receiptEntries = new List<BE.VesselCutOff>();
            dataTable = db.sub_GetDatatable("SP_FetchVesselDetails '" + ViaNO + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.VesselCutOff receiptEntry = new BE.VesselCutOff();


                    receiptEntry.VesselID = Convert.ToInt32(row["VesselID"]);
                    receiptEntry.Voyage = Convert.ToString(row["Voyage"]);
                    receiptEntry.RotationNo = Convert.ToString(row["RotationNo"]);
                    receiptEntry.CutofDate = Convert.ToString(row["CutofDate"]);
                    receiptEntry.PortName = Convert.ToString(row["POL"]);

                    receiptEntries.Add(receiptEntry);
                }
            }


            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult UpdateVesselDet(List<BE.VesselCutOff> tablearray, string ViaNo, string pod, string fpd,int vesselID, string vesselName)
        {
            string Message = "";
            string message1 = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("containerNo");
            dataTable.Columns.Add("EntryID");
           

            foreach (BE.VesselCutOff item in tablearray)
            {
                DataRow row = dataTable.NewRow();

                row["containerNo"] = item.containerNo;
                row["EntryID"] = item.EntryID;
                

                dataTable.Rows.Add(row);

                int i = new int();
                int j = new int();
                CD.DBOperations db1 = new CD.DBOperations();
                i = db1.sub_ExecuteNonQuery("USP_UpdateVessebcn '" + ViaNo +"','"+ pod +"','"+fpd+"','"+vesselID+"','"+vesselName+"','" + row["EntryID"] + 
                    "','" + row["containerNo"] + "'");

                //j = db1.sub_ExecuteNonQuery("Usp_Import_Holds_status '" + ViaNo + "','" + pod + "','" + fpd + "','" + vesselID + "','" + vesselName + "','" + row["EntryID"] +
                //    "','" + row["containerNo"] + "'");


            }

           

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public ActionResult FactorySBEntry()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = BL.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");
            //For Shipper ExportShippingBillEnt Is Entities
            List<BE.ExportShippingBillEnt> Exporter = new List<BE.ExportShippingBillEnt>();
            Exporter = BL.ExporterDDL();
            ViewBag.Exporter = new SelectList(Exporter, "shipperid", "Shippername");
            //CHA LIST
            List<BE.ExportShippingBillEnt> CHAList = new List<BE.ExportShippingBillEnt>();
            CHAList = BL.CHADLL();
            ViewBag.CHAList = new SelectList(CHAList, "CHAID", "CHAName");

            List<BE.ExportShippingBillEnt> CargoList = new List<BE.ExportShippingBillEnt>();
            CargoList = BL.CargoType();
            ViewBag.CargoList = new SelectList(CargoList, "CID", "CType");

            List<BE.ExportShippingBillEnt> pkglist = new List<BE.ExportShippingBillEnt>();
            pkglist = BL.Pkgs();
            ViewBag.pkglist = new SelectList(pkglist, "pkgid", "pkgtype");

            List<BE.ExportShippingBillEnt> PodListDDL = new List<BE.ExportShippingBillEnt>();
            PodListDDL = BL.PodMaster();
            ViewBag.PodList = new SelectList(PodListDDL, "PodID", "PODName");

            List<BE.ExportShippingBillEnt> CommodityList = new List<BE.ExportShippingBillEnt>();
            CommodityList = BL.CommodityM();
            ViewBag.Commodity = new SelectList(CommodityList, "ComID", "CommodityName");


            List<BE.ExportShippingBillEnt> StuffList = new List<BE.ExportShippingBillEnt>();
            StuffList = BL.Stuffing();
            ViewBag.Stuff = new SelectList(StuffList, "StuffID", "Stuffing");
            
            List<BE.ExportShippingBillEnt> ContainerType = new List<BE.ExportShippingBillEnt>();
            ContainerType = BL.ContainerType();
            ViewBag.ContainerType = new SelectList(ContainerType, "ContainerTypeID", "ContainerType");

            List<BE.ExportShippingBillEnt> PortName = new List<BE.ExportShippingBillEnt>();
            PortName = BL.PName();
            ViewBag.Ports = new SelectList(PortName, "PID", "PName");

            List<BE.ExportShippingBillEnt> PODCountry = new List<BE.ExportShippingBillEnt>();
            PODCountry = BL.PODCountry();
            ViewBag.Country = new SelectList(PODCountry, "PCountryID", "CountryName");

            
            return View();
            
        }

        // fetch factory stuff container 
        public JsonResult ShowBufferContainerDetails(string ContainerNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExportShippingBillEnt> receiptEntries = new List<BE.ExportShippingBillEnt>();
            dataTable = db.sub_GetDatatable("USP_ShowBufferContainerDetails2 '" + ContainerNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExportShippingBillEnt receiptEntry = new BE.ExportShippingBillEnt();


                    receiptEntry.ContainerTypeID = Convert.ToInt32(row["containertypeid"]);
                    receiptEntry.Size = Convert.ToInt32(row["size"]);
                    receiptEntry.TareWt = Convert.ToInt32(row["TareWeight"]);
                    receiptEntry.EntryID = Convert.ToInt32(row["entryid"]);


                    receiptEntries.Add(receiptEntry);
                }
            }


            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        // { "Sbno": Sbno, "sbdate": sbdate, "InvoiceNo": InvoiceNo, "Customer": Customer, "CHA": CHA, "CHAName": CHAName, "Exporter": Exporter, "ExporterName": ExporterName, "Cons": Cons, "ConsAdd": ConsAdd, "CargoDes": CargoDes, "Remarks": Cargo, "CargoName": CargoName, "FOBValues": FOBValues, "StuffID": StuffID, "crrqty": crrqty, "pkgs": pkgs, "pkgsname": pkgsname, "cargowt": cargowt, "pod": pod, "commodity": commodity, "remarks": remarks, "oldcha": oldcha, "oldexporter": oldexporter, "oldcust": oldcust, "oldpkgs": oldpkgs,"oldCargo":oldCargo };

        public JsonResult UpdateSBDetails(string EntryID , string Sbno, string sbdate, string InvoiceNo, string Customer, string AGName, string CHA, string CHAName, string Exporter, string ExporterName, string Cons, string ConsAdd, string CargoDes, string cargo,string CargoName, string FOBValues, string StuffID,
            string crrqty, string pkgs, string pkgsname, string cargowt, string pod, string commodity, string remarks, string oldcha, string oldexporter, string oldcust, string oldpkgs, string oldCargo)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";
            
            retVal = db.sub_ExecuteNonQuery("USP_UpdateSBDetails '" + EntryID + "','" + Sbno + "','" + Convert.ToDateTime(sbdate).ToString("yyyy-MM-dd HH:mm") + "','" +InvoiceNo  + "','" + Customer  + "','" + CHA +"','" + Exporter + "','" + Cons +  "','" + CargoDes + "','" + cargo  + "','" + FOBValues + "','" + StuffID + "','" + crrqty +  "','" + cargowt + "','" + pod + "','" + commodity + "','" + remarks +  "','" + userId + "'");

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

        public JsonResult getPartyNameCHA(string prefixText, string Mode)
        {
            try
            {


                CD.DBOperations db = new CD.DBOperations();
                DataTable dataTable = new DataTable();
                List<BE.Customer> Customerlst = new List<BE.Customer>();
                dataTable = db.sub_GetDatatable("USP_GetCHANameList '" + Mode + "','" + prefixText + "'");

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        BE.Customer customer = new BE.Customer();
                        customer.CHAID = Convert.ToInt32(row["GSTID"]);
                        customer.CHAName = Convert.ToString(row["GSTName"]);
                        Customerlst.Add(customer);
                    }
                }

                var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
