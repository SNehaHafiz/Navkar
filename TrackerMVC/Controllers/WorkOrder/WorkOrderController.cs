using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using SB = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ExportSBBL;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ModifyBL;
using System.Data;
using DB = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Web.UI;
using HC = TrackerMVCDataLayer.Helper;
 
namespace TrackerMVC.Controllers
{
    [UserAuthenticationFilter]
    public class WorkOrderController : Controller
    {
        // GET: WorkOrder

        BM.WorkOrder.WorkOrder WO = new BM.WorkOrder.WorkOrder();
        SB.ExportSBBL SB = new SB.ExportSBBL();
        DB.DBOperations db = new DB.DBOperations();
        RP.ModifyBL BL = new RP.ModifyBL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult WorkOrder()
        {
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListWorkOrder();
            ViewBag.WOType = new SelectList(WorkOrderList.WOTypeList, "Wo_Type", "Wo_Type");
            ViewBag.EQType = new SelectList(WorkOrderList.EQWOList, "Id", "Equipment");
            ViewBag.WHList = new SelectList(WorkOrderList.WHList, "WHID", "WHName");
            ViewBag.VendorList = new SelectList(WorkOrderList.VendorWOList, "VendorId", "Name");
            ViewBag.PKGList = new SelectList(WorkOrderList.PKGList, "CodeID", "Package");

            return View();
        }

        public ActionResult ExportWorkOrderSSR()
        {
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListWorkOrder();
            ViewBag.WOType = new SelectList(WorkOrderList.WOTypeList, "Wo_Type", "Wo_Type");
            ViewBag.EQType = new SelectList(WorkOrderList.EQWOList, "Id", "Equipment");
            ViewBag.AccountList = new SelectList(WorkOrderList.ExportAccountMasterList, "AccountID", "AccountName");
            ViewBag.VendorList = new SelectList(WorkOrderList.VendorMasterList, "VendorId", "Name");


            return View();
        }
        
        public ActionResult WorkOrderTariffDefine()
        {
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListWorkOrderTariff();
            ViewBag.SizeList = new SelectList(WorkOrderList.containerSizesList, "ContainerSizeID", "ContainerSizeName");
            //ViewBag.EQType = new SelectList(WorkOrderList.containerTypesList, "Id", "Equipment");
            ViewBag.CargoType = new SelectList(WorkOrderList.cargoTypesList, "Cargotypeid", "Cargotype");
            ViewBag.Commodity = new SelectList(WorkOrderList.commodityGroupsList, "Commodity_Group_ID", "Commodity_Group_Name");
            ViewBag.PackageType = new SelectList(WorkOrderList.PKGList, "CodeID", "Package");
            return View();
        }

        public ActionResult WorkOrderTariffDetails()
        {
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListWorkOrder();
            ViewBag.WOType = new SelectList(WorkOrderList.WOTypeList, "Wo_Type", "Wo_Type");
            return View();
        }

        public ActionResult OtherWorkOrder()
        {
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListWorkOrder();
            //ViewBag.WOType = new SelectList(WorkOrderList.WOTypeList, "Wo_Type", "Wo_Type");
            //ViewBag.EQType = new SelectList(WorkOrderList.EQWOList, "Id", "Equipment");
            //ViewBag.AccountList = new SelectList(WorkOrderList.ExportAccountMasterList, "AccountID", "AccountName");
            ViewBag.VendorList = new SelectList(WorkOrderList.VendorMasterList, "VendorId", "Name");
            return View();
        }

        public ActionResult RailWorkOrder()
        {
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListWorkOrder();
            ViewBag.VendorList = new SelectList(WorkOrderList.VendorWOList, "VendorId", "Name");

            return View();
        }

        public JsonResult ShowSBNODets(string SBNO, string WorkType)
        {
            List<BE.WorkOrderEntities> SBList = new List<BE.WorkOrderEntities>();
            SBList = WO.GetSBDets(SBNO, WorkType);
            var jsonResult = Json(SBList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ShowSBNODetsByInvoice(string InvoiceNo, string WorkType)
        {
            List<BE.WorkOrderEntities> SBList = new List<BE.WorkOrderEntities>();
            SBList = WO.GetSBDetsByInvoiceNo(InvoiceNo, WorkType);
            var jsonResult = Json(SBList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ShowCartWeightDets(string SBNO, string WorkType)
        {
            BE.WorkOrderEntities SBList = new BE.WorkOrderEntities();
            SBList = WO.GetCartWeightPkgs(SBNO, WorkType);
            var jsonResult = Json(SBList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ShowCartWeightDetsByInvoice(string InvoiceNo, string WorkType)
        {
            BE.WorkOrderEntities SBList = new BE.WorkOrderEntities();
            SBList = WO.GetCartWeightPkgsInvoice(InvoiceNo, WorkType);
            var jsonResult = Json(SBList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult VehicleNos(string SBEntryID, string SBNO)
        {
            List<BE.VehicleWOEntities> VehicleList = new List<BE.VehicleWOEntities>();
            VehicleList = WO.GetVehicleNos(SBEntryID, SBNO);
            return new JsonResult() { Data = VehicleList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }        
        public JsonResult TotalPkgsVehicleWise(string SBEntryID, string SBNO, string VehicleNo,string CartingId)
        {
            List<BE.WorkOrderEntities> PkgsList = new List<BE.WorkOrderEntities>();
            PkgsList = WO.GetPkgsVehicleWise(SBEntryID, SBNO, VehicleNo,CartingId);
            var jsonResult = Json(PkgsList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult WorkOrderSave(List<BE.WorkOrderEntities> WOdata, string WODate, string WOType, string SBNo, int SBEntryID, int CHAID, int OnAccountID)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);

            string message = "";

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("WareHouseID");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("StuffPkgs");
            dataTable.Columns.Add("VendorID");
            dataTable.Columns.Add("EquipmentID");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("StuffLocation");
            dataTable.Columns.Add("PkgType");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("CartingAllowId");

            foreach (BE.WorkOrderEntities item in WOdata)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["WareHouseID"] = item.WareHouseID;
                row["VehicleNo"] = item.VehicleNo;
                row["StuffPkgs"] = item.StuffPkgs;
                row["VendorID"] = item.VendorID;
                row["EquipmentID"] = item.EquipmentID;
                row["Description"] = item.Description;
                row["StuffLocation"] = item.StuffLocation;
                row["PkgType"] = item.PkgTypeID;
                row["Weight"] = item.Weight;
                row["CartingAllowId"] = item.CartingAllowId;
                dataTable.Rows.Add(row);

                message = ValidateDuplicate(WOType, item.ContainerNo, item.VendorID, item.StuffPkgs, item.VehicleNo, item.Weight);

            }

            if (message == "")
            {
                message = WO.SaveWorkOrder(dataTable, WODate, WOType, SBNo, SBEntryID, CHAID, OnAccountID, UserID);
            }
           
            return Json(message);
        }
        public JsonResult ListOpenWorkOrder()
        {
            List<BE.OpenWOListEntities> OpenWOList = new List<BE.OpenWOListEntities>();
            OpenWOList = WO.GetOpenWOList();
            var jsonResult = Json(OpenWOList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //public JsonResult CloseWorkOrder(string WONo,string SequenceNo)
        //{
        //    int UserID = Convert.ToInt16(Session["Tracker_userID"]);
        //    string message = "";
        //    int i = 0;
        //    i = db.sub_ExecuteNonQuery("USP_CLOSE_OPEN_WORK_ORDER '" + WONo + "','" + UserID + "','" + SequenceNo + "'");
        //    if (i != 0)
        //    {
        //        message = "Work Order " + WONo + " closed successfully";
        //    }
        //    else
        //    {
        //        message = "Work Order " + WONo + " not closed successfully";
        //    }
        //    return Json(message);
        //}

        public JsonResult CloseWorkOrder(string WONo, string SequenceNo)
        {
            string message = "";
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string MenuID = "6344";
            dataTable = db.sub_GetDatatable("USP_Check_rights '" + MenuID + "','" + UserID + "'");
            if (dataTable.Rows[0].Field<int>("ID") != 1)
            {
                message = "You are not authorised to closed please contact to I.T team.";
            }
            if (message == "")
            {
                int i = 0;
                i = db.sub_ExecuteNonQuery("USP_CLOSE_OPEN_WORK_ORDER '" + WONo + "','" + UserID + "','" + SequenceNo + "'");
                if (i != 0)
                {
                    message = "Work Order " + WONo + " closed successfully";
                }
                else
                {
                    message = "Work Order " + WONo + " not closed successfully";
                }
            }

            return Json(message);
        }
        public JsonResult ContainerValidation(string ContainerNo)
        {
            List<BE.WorkOrderEntities> ConList = new List<BE.WorkOrderEntities>();
            ConList = WO.GetConValList(ContainerNo);
            var jsonResult = Json(ConList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult PrintWorkOrder(string WONo)
        {
            DataTable CompanyMaster = new DataTable();
            DataSet WODS = new DataSet();
            List<BE.PrintWOEntities> WOList = new List<BE.PrintWOEntities>();
            WOList = WO.GetWOPrintList(WONo);

            WODS = db.sub_GetDataSets("USP_WO_PRINT_DETAILS '" + WONo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            ViewBag.CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            ViewBag.CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            ViewBag.WONo = WOList[0].WONo;
            ViewBag.WODate = WOList[0].WODate;
            ViewBag.WOType = WOList[0].WOType;
            ViewBag.Agent = WOList[0].Agent;
            ViewBag.CHAName = WOList[0].CHAName;
            ViewBag.SBNoPrint = WOList[0].SBNoPrint;

            return PartialView(WOList);
        }
        public ActionResult WorkOrderReport(string FromDate,string ToDate,string Criteria,string SearchText)
        {
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListWorkOrder();
            ViewBag.VendorList = new SelectList(WorkOrderList.VendorWOList, "VendorId", "Name");
            ViewBag.WOType = new SelectList(WorkOrderList.WOTypeList, "Wo_Type", "Wo_Type");
            return View();
        }
        public JsonResult ShowWorkOrderReport(string FromDate, string ToDate, string Criteria, string SearchText)
        {
            List<BE.WorkOrderReportEntities> WOReportList = new List<BE.WorkOrderReportEntities>();
            WOReportList = WO.GetWOReportList(FromDate, ToDate, Criteria, SearchText);
            var jsonResult = Json(WOReportList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult LabourActivityReportPrint(string FromDate,string ToDate,string Vendor,string WoType)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblLabourVendorList = new DataTable();
            DataTable tblLabourTCWCList = new DataTable();
            DataTable tblLabourTWList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetLabourActivityReport '" + FromDate + "','" + ToDate + "','" + Vendor + "','" + WoType + "'");

            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblLabourVendorList = getJobOrderSet.Tables[1];
                tblLabourTCWCList = getJobOrderSet.Tables[2];
                tblLabourTWList = getJobOrderSet.Tables[3];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.Code = dr["ConCode"];
                }
            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.LabourVendorList = tblLabourVendorList.AsEnumerable();
            ViewBag.LabourTCWCList = tblLabourTCWCList.AsEnumerable();
            ViewBag.LabourTWList = tblLabourTWList.AsEnumerable();

            return PartialView();

        }

        [HttpPost]
        public ActionResult LatterActivityReportPrint(string FromDate, string ToDate, string Vendor)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblLabourVendorList = new DataTable();
            DataTable tblLatterList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetLabourActivityReportLatter '" + FromDate + "','" + ToDate + "','" + Vendor + "'");

            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblLabourVendorList = getJobOrderSet.Tables[1];
                tblLatterList = getJobOrderSet.Tables[2];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.Code = dr["ConCode"];
                }
            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.LabourVendorList = tblLabourVendorList.AsEnumerable();
            ViewBag.LabourLatterReport = tblLatterList.AsEnumerable();

            return PartialView();

        }

        public string ValidateDuplicate(string WOType, string ContaienrNo,int Vendor,decimal Pkgs,string VehicleNo ,string Weight)
        {
            string Message = "";
            try
            {
                DataTable dt = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                dt = db.sub_GetDatatable("ValidateDuplicateWo '" + WOType + "','" + ContaienrNo + "','" + Vendor + "','" + Pkgs + "','" + VehicleNo + "','" + Weight + "'");

                if (dt.Rows.Count > 0)
                {
                    Message = dt.Rows[0]["Message"].ToString();
                }
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }

            return Message;
            
        }

        public JsonResult getInvoiceWorkOrderDet(string SBNo,string SSRType)
        {
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            BE.WorkOrderEntities workOrder = new BE.WorkOrderEntities();
            List<BE.WorkOrderEntities> ContainerList = new List<BE.WorkOrderEntities>();
            dt = db.sub_GetDatatable("SP_SHOWEXPORT_SSR '" + SBNo + "'");


            if(dt != null)
            {
                workOrder.SBEntryID =Convert.ToInt32(dt.Rows[0]["EntryID"]);
                workOrder.AgencyId = Convert.ToInt32(dt.Rows[0]["AGId"]);
                workOrder.AgencyName = dt.Rows[0]["AGName"].ToString();
                workOrder.Weight = dt.Rows[0]["CargoWeight"].ToString();
                workOrder.CargoType = dt.Rows[0]["CargoType"].ToString();
                workOrder.StuffLocation = dt.Rows[0]["StuffingType"].ToString();
            }

            if ((SSRType == "Stuffing" || SSRType == "SSR") && workOrder.SBEntryID > 0)
            {
                dt1 = db.sub_GetDatatable("SP_BINDCONGTAINERNO '" + workOrder.SBEntryID + "'");

                foreach(DataRow row in dt1.Rows)
                {
                    BE.WorkOrderEntities work = new BE.WorkOrderEntities();
                    work.ContainerNo = row["ContainerNo"].ToString();
                    ContainerList.Add(work);
                }
            }

            var returnField = new { workOrder = workOrder, ContainerData = ContainerList };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult getContainerDet(string ContainerNo, string SSRType)
        {
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            BE.WorkOrderEntities workOrder = new BE.WorkOrderEntities();
            List<BE.WorkOrderEntities> ContainerList = new List<BE.WorkOrderEntities>();
            dt = db.sub_GetDatatable("getContainerDetSSR '" + ContainerNo + "'");


            if (dt != null)
            {
                workOrder.SBEntryID = Convert.ToInt32(dt.Rows[0]["entryID"]);
                workOrder.ContainerNo = Convert.ToString(dt.Rows[0]["containerNo"]);
                workOrder.CargoType = dt.Rows[0]["ContainerType"].ToString();
                workOrder.StuffLocation = dt.Rows[0]["EntryType"].ToString();
                workOrder.Size = dt.Rows[0]["size"].ToString();
            }

            var returnField = new { workOrder = workOrder};

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult SpecialWorkOrderSave(List<BE.WorkOrderEntities> WOdata, string SSRType, string JoType, string IsServiceTax)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);

            string message = "";

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("AccountID");
            dataTable.Columns.Add("AccountName");
            dataTable.Columns.Add("AcountToCollect");
            dataTable.Columns.Add("Narration");
            dataTable.Columns.Add("SBEntryId");
            dataTable.Columns.Add("VendorId");
            dataTable.Columns.Add("EntryId");
            dataTable.Columns.Add("Pallets");
            dataTable.Columns.Add("Shifts");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("IsStax");

            foreach (BE.WorkOrderEntities item in WOdata)
            {
                DataRow row = dataTable.NewRow();
                if (item.ContainerNo == null)
                {
                    item.ContainerNo = "";
                }
                if (item.Size == null)
                {
                    item.Size = "";
                }
                if (item.Narration == null)
                {
                    item.Narration = "";
                }

                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["AccountID"] = item.AccountID;
                row["AccountName"] = item.AccountName;
                row["AcountToCollect"] = item.AcountToCollect;
                row["Narration"] = item.Narration;
                row["SBEntryId"] = item.SBEntryID;
                row["VendorId"] = item.VendorID;
                row["EntryId"] = item.EntryId;
                row["Pallets"] = item.Pallets;
                row["Shifts"] = item.Shifts;
                row["Weight"] = item.Weight;
                row["IsStax"] = item.IsStax;
                dataTable.Rows.Add(row);
            }

            if (message == "")
            {
                message = WO.SaveSpecialWorkOrder(dataTable, JoType, SSRType, IsServiceTax, UserID);
            }

            return Json(message);
        }

        //=================Add Code For Rail Work Order Exim & Domestic==================

        public JsonResult ValidateTrain(string TrainNo, string FileType, string RRNo)
        {
            string Message = "";
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            dataTable = db.sub_GetDatatable("ValidateRRDownload '" + TrainNo + "','" + FileType + "','" + RRNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    Message = row["Message"].ToString();
                }
            }
            else
            {
                Message = "Invalid Details.";
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UploadRRFile(BE.BCNRRDownload fileData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            long RRFileId = 0;
            string message = "";

            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/ImportFile/"), fname);
                        //  fname = Path.Combine(fname);
                        file.SaveAs(fname);
                        string extension = Path.GetExtension(fname);
                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                         // conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                            case ".xlsx": //Excel 07 and above.
                                          //conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                        }

                        if (extension == ".csv")
                        {
                            string contentType;
                            Stream fs = Request.Files[i].InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);

                            contentType = MimeMapping.GetMimeMapping(fname);
                            //string filedata = fileContents;
                            DataTable DT = new DataTable();

                            DT.Columns.Add("SeqNo");
                            DT.Columns.Add("Line");

                            using (StreamReader file12 = new StreamReader(fname))
                            {
                                int counter = 0;
                                int counter1 = 0;
                                string ln;

                                while ((ln = file12.ReadLine()) != null)
                                {
                                    if (counter1 == 0)
                                    {
                                        counter1++;
                                    }
                                    else
                                    {
                                        counter++;
                                        DataRow dar = DT.NewRow();
                                        dar["SeqNo"] = counter;
                                        dar["Line"] = ln.Replace(",", "~");
                                        DT.Rows.Add(dar);
                                    }
                                }
                                file12.Close();
                            }

                            RRFileId = WO.AddRailRRData(DT, Userid, fileData.TrainNo, fileData.FileType,fileData.RRWagonNo,fileData.Date);
                            if (RRFileId <= 0)
                            {
                                return new JsonResult() { Data = "RR Download not save successfully. Plz Upload Again !", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                            }
                            //Session.Add("IgmFileId1", IgmFileId);
                            Session["RRFileId"] = RRFileId;
                        }
                        else
                        {
                            using (OleDbConnection connExcel = new OleDbConnection(conString))
                            {
                                // BL.AddErrorLog("OleDbConnection");
                                using (OleDbCommand cmdExcel = new OleDbCommand())
                                {
                                    // BL.AddErrorLog("cmdExcel");
                                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                    {
                                        //  BL.AddErrorLog("odaExcel");
                                        DataTable dt = new DataTable();
                                        cmdExcel.Connection = connExcel;

                                        //Get the name of First Sheet.
                                        connExcel.Open();
                                        //  BL.AddErrorLog("Open connection");
                                        DataTable dtExcelSchema;
                                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                        //  BL.AddErrorLog(sheetName);
                                        connExcel.Close();

                                        //Read Data from First Sheet.
                                        connExcel.Open();
                                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                        odaExcel.SelectCommand = cmdExcel;
                                        odaExcel.Fill(dt);
                                        connExcel.Close();

                                        DataTable DT1 = new DataTable();
                                        String strb = "";
                                        string strSeprate = "~";
                                        int counter = 0;
                                        DT1.Columns.Add("SeqNo");
                                        DT1.Columns.Add("Line");

                                        if (dt != null)
                                        {
                                            foreach (DataRow dataRow in dt.Rows)
                                            {
                                                counter += 1;
                                                strb = "";
                                                foreach (DataColumn column in dt.Columns)
                                                {
                                                    if (strb == "")
                                                    {
                                                        strb = dataRow[column].ToString().Replace("~", "");
                                                    }
                                                    else
                                                    {
                                                        strb = strb + strSeprate + dataRow[column].ToString().Replace("~", "");
                                                    }
                                                }
                                                DataRow dar = DT1.NewRow();
                                                dar["SeqNo"] = counter;
                                                dar["Line"] = strb;
                                                DT1.Rows.Add(dar);
                                            }
                                            RRFileId = WO.AddRailRRData(DT1, Userid, fileData.TrainNo, fileData.FileType, fileData.RRWagonNo, fileData.Date);
                                            if (RRFileId <= 0)
                                            {
                                                return new JsonResult() { Data = "RR Download not save successfully. Plz Upload Again !", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                            }
                                            //Session.Add("IgmFileId1", IgmFileId);
                                            Session["RRFileId"] = RRFileId;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    message = "Exception:- " + ex.Message;
                }
            }
            return Json(message);
        }

        [HttpPost]
        public JsonResult ShowRailMapping(string TrainNo, string WagonRRNo)
        {
            List<BE.WorkOrderEntities> workOrderEntities = new List<BE.WorkOrderEntities>();

            if(TrainNo ==null || TrainNo == "")
            {
                TrainNo = "0";
            }

            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("GetRailWorkOreder '" + TrainNo + "','" + WagonRRNo + "'");

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        BE.WorkOrderEntities OrderEntities = new BE.WorkOrderEntities();
                        OrderEntities.EntryId = Convert.ToInt32(data["EntryId"]);
                        OrderEntities.ContainerNo = Convert.ToString(data["ContainerNo"]);
                        OrderEntities.Size = Convert.ToString(data["ContainerSize"]);
                        OrderEntities.Type = Convert.ToString(data["ContainerType"]);
                        OrderEntities.GroupCode = Convert.ToString(data["GroupCode"]);
                        OrderEntities.CargoType = Convert.ToString(data["Commodity"]);
                        OrderEntities.PackageType = Convert.ToString(data["PackageType"]);
                        OrderEntities.StuffPkgs = Convert.ToDecimal(data["NoofPkgs"]);
                        OrderEntities.UnitWt = Convert.ToDecimal(data["UnitWeight"]);
                        OrderEntities.TotWeight = Convert.ToDecimal(data["TotalWeight"]);

                        workOrderEntities.Add(OrderEntities);
                    }
                }
            }

            var returnField = new { WagonContainerList = workOrderEntities };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult ValidateDuplicateRRNo(string WagonRRNo)
        {
            int Count = 0;
            try
            {
                DataTable dt = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                dt = db.sub_GetDatatable("GetRailDataByRRNo '" + WagonRRNo + "'");

                if (dt.Rows.Count > 0)
                {
                    Count =Convert.ToInt32(dt.Rows[0]["CountData"]);
                }
            }
            catch(Exception ex)
            {
                Count = 1;
            }
            
           

            return Json(Count);
        }


        //=================End Code For Rail Work Order Exim & Domestic==================

        [HttpPost]
        public JsonResult getAutoPackageTpyeList(string prefixText, string CustomerType)
        {
            List<PackageTypeM> packageTypeMs = new List<PackageTypeM>();
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("SELECT CodeID,Package FROM PackageM");

            if(db != null)
            {
                foreach(DataRow data in LoginDT.Rows)
                {
                    PackageTypeM typeM = new PackageTypeM();
                    typeM.PackageId = Convert.ToInt32(data["CodeID"]);
                    typeM.PackageName = Convert.ToString(data["Package"]);
                    packageTypeMs.Add(typeM);
                }
            }

            return Json(packageTypeMs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAutoGroupCodeTypeList(string prefixText, string CustomerType)
        {
            List<PackageTypeM> packageTypeMs = new List<PackageTypeM>();
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("select VendorId,Name from Vendor_M");

            if (db != null)
            {
                foreach (DataRow data in LoginDT.Rows)
                {
                    PackageTypeM typeM = new PackageTypeM();
                    typeM.PackageId = Convert.ToInt32(data["VendorId"]);
                    typeM.PackageName = Convert.ToString(data["Name"]);
                    packageTypeMs.Add(typeM);
                }
            }

            return Json(packageTypeMs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAutoCargoTypeList(string prefixText, string CustomerType)
        {
            List<PackageTypeM> packageTypeMs = new List<PackageTypeM>();
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("select Commodity_Group_ID,Commodity_Group_Name from Commodity_Group_M");

            if (db != null)
            {
                foreach (DataRow data in LoginDT.Rows)
                {
                    PackageTypeM typeM = new PackageTypeM();
                    typeM.PackageId = Convert.ToInt32(data["Commodity_Group_ID"]);
                    typeM.PackageName = Convert.ToString(data["Commodity_Group_Name"]);
                    packageTypeMs.Add(typeM);
                }
            }

            return Json(packageTypeMs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RailWorkOrderSave(List<BE.WorkOrderEntities> WOdata, string TrainNo, string RRNo, string FileType)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);

            string message = "";

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("EntryId");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("GroupCode");
            dataTable.Columns.Add("CargoType");
            dataTable.Columns.Add("StuffPkgs");
            dataTable.Columns.Add("UnitWt");
            dataTable.Columns.Add("TotWeight");
            dataTable.Columns.Add("PackageType");

            foreach (BE.WorkOrderEntities item in WOdata)
            {
                DataRow row = dataTable.NewRow();
                if (item.PackageType == null)
                {
                    item.PackageType = "";
                }

                row["ContainerNo"] = item.ContainerNo;
                row["EntryId"] = item.EntryId;
                row["Size"] = item.Size;
                row["Type"] = item.Type;
                row["GroupCode"] = item.GroupCode;
                row["CargoType"] = item.CargoType;
                row["StuffPkgs"] = item.StuffPkgs;
                row["UnitWt"] = item.UnitWt;
                row["TotWeight"] = item.TotWeight;
                row["PackageType"] = item.PackageType;
                dataTable.Rows.Add(row);
            }

            if (message == "")
            {
                message = WO.SaveRrailWorkOrder(dataTable, TrainNo, RRNo, FileType,UserID);
            }

            return Json(message);
        }

        public JsonResult ShowRailWorkOrderReport(string FromDate, string ToDate,string Category,string Vendor)
        {
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("GetRailWorkOrderSummary '" + FromDate + "','" + ToDate + "','" + Category + "','" +Vendor + "'");
            Session["RailWorkOrder"] = dt;
            Session["FromDateR"] = FromDate;
            Session["ToDateR"] = ToDate;

            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult RailActivityReportPrint(string FromDate, string ToDate, string Vendor,string Category)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblVendorList = new DataTable();
            DataTable tblRailList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetRailPrintSummary '" + FromDate + "','" + ToDate + "','" + Vendor + "','" + Category + "'");

            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblVendorList = getJobOrderSet.Tables[1];
                tblRailList = getJobOrderSet.Tables[2];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.Location = dr["ConCode"];
                }
            }

            ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.LabourVendorList = tblVendorList.AsEnumerable();
            ViewBag.LabourList = tblRailList.AsEnumerable();

            return PartialView();

        }

        public JsonResult getExpWoSSr(string FromDate, string ToDate)
        {
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("GetExportWOrkOrederSSSummary '" + FromDate + "','" + ToDate + "'");
            
            Session["GetExportWOrkOrederSSSummary"] = dt;
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelExportSSR()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetLCLDestuffTally = (DataTable)Session["GetExportWOrkOrederSSSummary"];
            GetLCLDestuffTally.Columns.Remove("Action");
            GridView gridview = new GridView();
            gridview.DataSource = GetLCLDestuffTally;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=SSR Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>SSR Report</h3></td></tr>");

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

        public JsonResult CancelSSR(string SSRNo)
        {
            HC.DBOperations db = new DB.DBOperations();
            string Message = "";
            int i = 0;
            i = db.sub_ExecuteNonQuery("USP_CancelSSRWorkOrder '"+ SSRNo + "'" );

            if (i > 0)
            {
                Message = "SSR Cancel Successfully.";
            }
            else
            {
                Message = "SSR Not Cancel Successfully.";
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SSRWorkOrderPrint(string SSRNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetSSRWorkOrderPrint '" + SSRNo + "'");

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
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                }
                foreach (DataRow dr in tblInvoiceDetails.Rows)
                {
                    ViewBag.SSRNo = dr["SSRNo"];
                    ViewBag.SSRDate = dr["SSRDate"];
                    ViewBag.SBNo = dr["SBNo"];
                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.AgencyName = dr["AgencyName"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.PreparedBy = dr["PreparedBy"];
                    ViewBag.Category = dr["JoType"];
                }
            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();

            return PartialView();

        }

        public ActionResult ExportToExcelRailWorkOrder(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["RailWorkOrder"];
            string Tittle = "From " + Session["FromDateR"] + " To " + Session["ToDateR"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=RailWorkOrder.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Rail Work Order Report<strong></td></tr>");
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

        public JsonResult ViewWorkOrderEdit(string WorkOrder, string SrNo)
        {
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            BE.WorkOrderEntities workOrder = new BE.WorkOrderEntities();
            dt = db.sub_GetDatatable("GetWOrkOrderDetEdit '" + WorkOrder + "','" + SrNo + "'");

            workOrder.WoNo = dt.Rows[0]["WO_NO"].ToString();
            workOrder.WoDate = dt.Rows[0]["WO_Date"].ToString();
            workOrder.SrNo = Convert.ToString(dt.Rows[0]["SrNo"]);
            workOrder.StuffPkgs =Convert.ToDecimal(dt.Rows[0]["PKGSDestuff"]);
            workOrder.Weight = dt.Rows[0]["Weight"].ToString();
            workOrder.VendorID = Convert.ToInt32(dt.Rows[0]["Vendor"]);

            var jsonResult = Json(workOrder, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult WorkOrderEdit(string WoNo,string SrNo, string StuffPkgs,string Weight,string VendorID)
        {
            int rowCount = 0;
            try
            {
                HC.DBOperations db = new DB.DBOperations();

                rowCount = db.sub_ExecuteNonQuery("USPEditWorkOrderExport '" + WoNo + "','" + SrNo + "','" + StuffPkgs + "','" + Weight + "','" + VendorID + "'");
            }
            catch(Exception ex)
            {
                rowCount = 0;
            }

            return Json(rowCount, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateVendor(string ContainerNo, string VendorId,string Package,string Weight)
        {
            DataTable dataTable = new DataTable();
            string Message = "";
            try
            {
                HC.DBOperations db = new DB.DBOperations();
                dataTable = db.sub_GetDatatable("ValidateExpWorkOrder '" + ContainerNo + "','" + VendorId + "','" + Package + "','" + Weight + "'");

                if(dataTable != null)
                {
                    Message = dataTable.Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                Message = "";
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowTariffDetails(string Month, string Year, string WOType, string Activity)
        {
            string Message = "";
            var jsonResult = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("GetWOrkOrederTariffGenDet '" + Month + "','" + Year + "','" + WOType + "','" + Activity + "'");
            Session["WOrkOrderTariff"] = dt;
            Session["Month"] = Month;
            Session["Year"] = Year;
            Session["WOType"] = WOType;
            Session["Activity"] = Activity;

            if (dt.Rows.Count <= 0)
            {
                jsonResult = "Record not Found.";
            }
            else
            {
                jsonResult = JsonConvert.SerializeObject(dt);
            }

            return Json(jsonResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ExportToWorkOrderTariff(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["WOrkOrderTariff"];
            string Tittle = "Month:- " + Session["Month"] + " Year:- " + Session["Year"] + " WOType:- " + Session["WOType"] + " Activity:- " + Session["Activity"]  + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=WorkOrderTariffSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Rail Work Order Report<strong></td></tr>");
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

        public JsonResult SaveTariffDetails(string Month, string Year, string TariffId, string WOType, string Activity)
        {
            string Message = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_InsWorkOrderTariffDetails '" + Month + "','" + Year + "','" + TariffId + "','" + WOType + "','" + Activity + "','" + UserID + "'");

            if (dt.Rows.Count <= 0)
            {
                Message = "Record not saved.";
            }
            else
            {
                Message = dt.Rows[0]["ErrMessage"].ToString();
            }

            return Json(Message, JsonRequestBehavior.AllowGet);

        }

            public JsonResult ShowTariffWorkOrder(string Month, string Year,string WOType,string Activity)
        {
            string Message = "";
            string TotalContainer = "0";
            string TotalWeight = "0";
            string TotalAmount = "0";
            long TariffId = 0;
            HC.DBOperations db = new DB.DBOperations();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            List<BE.WorkOrderEntities> workOrderList = new List<BE.WorkOrderEntities>();
            ds = db.sub_GetDataSets("GetWorkOrderTariffDetails '" + Month + "','" + Year + "','" + WOType + "','" + Activity + "'");

            try
            {
                if(ds != null)
                {
                    dt = ds.Tables[0];
                    dt1 = ds.Tables[1];

                    if (dt.Rows.Count <= 0)
                    {
                        Message = "Somthing went wrong. Plz try again.";
                    }
                    else
                    {
                        Message = dt.Rows[0]["ErrorMessage"].ToString();
                        TotalContainer = dt.Rows[0]["TotalCont"].ToString();
                        TotalWeight = dt.Rows[0]["TotalWeight"].ToString();
                        TotalAmount = dt.Rows[0]["TotalAmount"].ToString();
                        TariffId =Convert.ToInt64(dt.Rows[0]["TariffId"]);
                    }

                    if (dt1 != null)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt1.Rows)
                            {
                                BE.WorkOrderEntities workOrder = new BE.WorkOrderEntities();
                                workOrder.SrNo = row["Activity"].ToString();
                                workOrder.Wo_Type = row["WOType"].ToString();
                                workOrder.ContainerNo = row["ContainerNo"].ToString();
                                workOrder.Size = row["Size"].ToString();
                                workOrder.Type = row["Type"].ToString();
                                workOrder.PackageType = row["PackageType"].ToString();
                                workOrder.CargoType = row["Commodity"].ToString();
                                workOrder.UnitWt = Convert.ToDecimal(row["UnitWeight"]);
                                workOrder.Weight = Convert.ToString(row["Weight"]);
                                workOrder.Amount = Convert.ToDouble(row["Amount"].ToString());

                                workOrderList.Add(workOrder);
                            }

                            Session["TariffDetailsDT"] = dt1;
                            Session["TariffMonth"] = Month;
                            Session["TariffYear"] = Year;
                            Session["TariffWOType"] = WOType;
                            Session["TariffActivity"] =Activity;
                        }
                    }
                }
                else
                {
                    Message = "Somthing went wrong. Plz try again.";
                }
                
            }
            catch(Exception ex)
            {
                Message = ex.Message.ToString();
            }

            var returnField = new { TariffList = workOrderList, Message=Message, TotalCount=TotalContainer,TotalWeight=TotalWeight,TotalAmount=TotalAmount,TariffId=TariffId };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult OtherWorkOrderSave(string WONo,string WODate,string Name,string AssignBy,string Date,string Weight,string VendorName)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            HC.DBOperations db = new DB.DBOperations();
            int returnVal = 0;
            string Message = "";

            returnVal = db.sub_ExecuteNonQuery("USP_INSOtherWorkOrder '" + WONo + "','" + WODate + "','" + "" + "','" + Name + "','" + AssignBy + "','" + Date + "','" + Weight + "','" + UserID + "','" + VendorName + "'");

            return Json(returnVal, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ViewOpenWOrkOrder(string FromDate, string ToDate,string Vendor)
        {
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            BE.WorkOrderEntities workOrder = new BE.WorkOrderEntities();
            dt = db.sub_GetDatatable("getOtherWOrkOrderDet '" + FromDate + "','" + ToDate + "','" + Vendor + "'");

            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult OtherWorkReportPrint(string FromDate, string ToDate, string Vendor)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblLabourVendorList = new DataTable();
            DataTable tblLatterList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetLabourActivityReportOtherWork '" + FromDate + "','" + ToDate + "','" + Vendor + "'");

            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblLabourVendorList = getJobOrderSet.Tables[1];
                tblLatterList = getJobOrderSet.Tables[2];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.Code = dr["ConCode"];
                }
            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.LabourVendorList = tblLabourVendorList.AsEnumerable();
            ViewBag.LabourLatterReport = tblLatterList.AsEnumerable();

            return PartialView();

        }

        public JsonResult WorkOrderTariffSave(List<BE.WorkOrderTariff> WOdata, string Category, string RangeFrom, string RangeTo,string TariffId)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);

            if (TariffId == "")
            {
                TariffId = "0";
            }

            string message = "";

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("EntryId");
            dataTable.Columns.Add("TariffId");
            dataTable.Columns.Add("WoType");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("PackageType");
            dataTable.Columns.Add("CargoType");
            dataTable.Columns.Add("Commodity");
            dataTable.Columns.Add("TransType");
            dataTable.Columns.Add("UnitType");
            dataTable.Columns.Add("Amount");
            dataTable.Columns.Add("FromRange");
            dataTable.Columns.Add("ToRange");

            foreach (BE.WorkOrderTariff item in WOdata)
            {
                DataRow row = dataTable.NewRow();

                row["EntryId"] = item.EntryId;
                row["TariffId"] = TariffId;
                row["WoType"] = item.WoType;
                row["Size"] = item.SizeId;
                row["PackageType"] = item.PackageId;
                row["CargoType"] = item.CargoTypeId;
                row["Commodity"] = item.CommodityId;
                row["TransType"] = item.TransType;
                row["UnitType"] = item.UnitType;
                row["Amount"] = item.Amount;
                row["FromRange"] = item.RangeFrom;
                row["ToRange"] = item.RangeTo;
                dataTable.Rows.Add(row);
            }

            if (message == "")
            {
                message = WO.SaveWorkOrderTariff(dataTable, Category, RangeFrom, RangeTo, UserID,TariffId);
            }

            return Json(message);
        }

        [HttpPost]
        public ActionResult WorkOrderTariffSummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("getWorkOrderTariffDet '" + FromDate + "','" + ToDate + "'");
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult EditViewTariffDetails(string TariffId)
        {
            string Message = "";
            BE.WorkOrderTariff headerData = new BE.WorkOrderTariff();
            List<BE.WorkOrderTariff> workOrderTariffsList = new List<BE.WorkOrderTariff>();
            try
            {

                DataSet ds = new DataSet();
                DataTable dtHeaderData = new DataTable();
                DataTable dtContainerDet = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                ds = db.sub_GetDataSets("getTariffSubDetails '" + TariffId + "'");

                if (ds != null)
                {
                    dtHeaderData = ds.Tables[0];

                    if (dtHeaderData.Rows.Count > 0)
                    {
                        headerData.TariffId = Convert.ToInt32(dtHeaderData.Rows[0]["TariffId"]);
                        headerData.Category = dtHeaderData.Rows[0]["Category"].ToString();
                        headerData.RangeFrom = dtHeaderData.Rows[0]["Effective From Date"].ToString();
                        headerData.RangeTo = Convert.ToString(dtHeaderData.Rows[0]["Effective To Date"]);

                        dtContainerDet = ds.Tables[1];
                        if (dtContainerDet.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtContainerDet.Rows)
                            {
                                BE.WorkOrderTariff orderTariff = new BE.WorkOrderTariff();
                                orderTariff.TariffId = Convert.ToInt32(row["TariffId"]);
                                orderTariff.EntryId = Convert.ToInt32(row["EntryId"]);
                                orderTariff.Category = row["Mode"].ToString();
                                orderTariff.TranTypeId = Convert.ToString(row["TransType"]);
                                orderTariff.WoType = row["Activity"].ToString();
                                orderTariff.SizeId = Convert.ToInt32(row["ContainerSizeID"]);
                                orderTariff.ContainerType = row["Type"].ToString();
                                orderTariff.CommodityId = Convert.ToInt32(row["CommodityId"]);
                                orderTariff.PackageId = Convert.ToInt32(row["PackageType"]);
                                orderTariff.RangeFrom = Convert.ToString(row["WeightRangeFrom"]);
                                orderTariff.RangeTo = Convert.ToString(row["WeightRangeTo"]);
                                orderTariff.Amount = Convert.ToString(row["Amount"]);
                                orderTariff.Size = Convert.ToString(row["ContainerSize"]);
                                orderTariff.CargoType = Convert.ToString(row["Cargotype"]);
                                orderTariff.Commodity = Convert.ToString(row["Commodity_Group_Name"]);
                                orderTariff.PackageType = Convert.ToString(row["Package"]);
                                orderTariff.UnitType = Convert.ToString(row["WeightType"]);

                                workOrderTariffsList.Add(orderTariff);
                            }
                        }
                        else
                        {
                            Message = "No Data Found.";
                        }
                    }
                    else
                    {
                        Message = "No Data Found.";
                    }
                }
                else
                {
                    Message = "No Data Found.";
                }
            }
            catch (Exception ex)
            {
                Message = "Invalid Jo Number.";
            }

            var returnField = new { ContainerList = workOrderTariffsList, message = Message, HeaderDetails = headerData };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult getTariffByEntryId(string EntryId)
        {
            string Message = "";
            BE.WorkOrderTariff headerData = new BE.WorkOrderTariff();
            
            try
            {

                DataTable dtContainerDet = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                dtContainerDet = db.sub_GetDatatable("getTariffSubDetailsByEntryId '" + EntryId + "'");

                if(dtContainerDet != null)
                {
                    headerData.TariffId = Convert.ToInt32(dtContainerDet.Rows[0]["TariffId"]);
                    headerData.EntryId = Convert.ToInt32(dtContainerDet.Rows[0]["EntryId"]);
                    headerData.Category = dtContainerDet.Rows[0]["Mode"].ToString();
                    headerData.TranTypeId = Convert.ToString(dtContainerDet.Rows[0]["TransType"]);
                    headerData.WoType = dtContainerDet.Rows[0]["Activity"].ToString();
                    headerData.SizeId = Convert.ToInt32(dtContainerDet.Rows[0]["ContainerSizeID"]);
                    headerData.ContainerType = dtContainerDet.Rows[0]["Type"].ToString();
                    headerData.CommodityId = Convert.ToInt32(dtContainerDet.Rows[0]["CommodityId"]);
                    headerData.PackageId = Convert.ToInt32(dtContainerDet.Rows[0]["PackageType"]);
                    headerData.RangeFrom = Convert.ToString(dtContainerDet.Rows[0]["WeightRangeFrom"]);
                    headerData.RangeTo = Convert.ToString(dtContainerDet.Rows[0]["WeightRangeTo"]);
                    headerData.Amount = Convert.ToString(dtContainerDet.Rows[0]["Amount"]);
                    headerData.Size = Convert.ToString(dtContainerDet.Rows[0]["ContainerSize"]);
                    headerData.CargoType = Convert.ToString(dtContainerDet.Rows[0]["Cargotype"]);
                    headerData.Commodity = Convert.ToString(dtContainerDet.Rows[0]["Commodity_Group_Name"]);
                    headerData.PackageType = Convert.ToString(dtContainerDet.Rows[0]["Package"]);
                    headerData.UnitType = Convert.ToString(dtContainerDet.Rows[0]["WeightType"]);
                }
                else
                {
                    Message = "Invalid Data.";
                }

            }
            catch (Exception ex)
            {
                Message = "Invalid Tariff Number.";
            }

            var returnField = new { message = Message, HeaderDetails = headerData };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult ExportToExcelTariffDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetGSTReturnCreditNoteSummary = (DataTable)Session["TariffDetailsDT"];
            string Tittle = "Month " + Session["TariffMonth"] + " Year " + Session["TariffYear"] + " Wo Type " + Session["TariffWOType"] + " Activity " + Session["TariffActivity"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetGSTReturnCreditNoteSummary;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=CreditNoteSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Credit Note Summary</h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + Tittle + " </h3></td></tr>");
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
        //developed by prathamesh mane
        public ActionResult WorkOrderLashingAndChocking()
        {
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListWorkOrder();
            ViewBag.WOType = new SelectList(WorkOrderList.WOTypeList, "Wo_Type", "Wo_Type");
            ViewBag.EQType = new SelectList(WorkOrderList.EQWOList, "Id", "Equipment");
            ViewBag.WHList = new SelectList(WorkOrderList.WHList, "WHID", "WHName");
            ViewBag.VendorList = new SelectList(WorkOrderList.VendorWOList, "VendorId", "Name");
            ViewBag.PKGList = new SelectList(WorkOrderList.PKGList, "CodeID", "Package");



            List<BE.EquipmentWOEntities> Equipment = new List<BE.EquipmentWOEntities>();
            Equipment = WO.GetEquipment();
            ViewBag.EquipmentList = new SelectList(Equipment, "Id", "Equipment");


            List<BE.Surveyor> SurveyorList = new List<BE.Surveyor>();
            SurveyorList = WO.GetSurveyor();
            ViewBag.Surveyor = new SelectList(SurveyorList, "SurveyorId", "SurveyorName");


            ViewBag.EquipmentList = JsonConvert.SerializeObject(Equipment);
            ViewBag.VendorLists = JsonConvert.SerializeObject(WorkOrderList.VendorWOList);
            ViewBag.PKGLists = JsonConvert.SerializeObject(WorkOrderList.PKGList);
            ViewBag.SurveyorL = JsonConvert.SerializeObject(SurveyorList);
            return View();

        }

        public JsonResult WorkOrderSaveLashingAndChocking(List<BE.WorkOrderEntities> WOdata, string WODate, string WOType, string SBNo, string SBEntryID, string CHAID, string OnAccountID)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);

            string message = "";

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("WareHouseID");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("StuffPkgs");
            dataTable.Columns.Add("VendorID");
            dataTable.Columns.Add("EquipmentID");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("StuffLocation");
            dataTable.Columns.Add("PkgType");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("CartingAllowId");
            dataTable.Columns.Add("Hours");
            dataTable.Columns.Add("CMB");
            dataTable.Columns.Add("SBEntryID");
            dataTable.Columns.Add("SBNo");
            dataTable.Columns.Add("OpPkgs");
            dataTable.Columns.Add("OpWt");
            dataTable.Columns.Add("Examine");
            dataTable.Columns.Add("Surveyor");
            dataTable.Columns.Add("MaterialQty");
            dataTable.Columns.Add("MaterialDescriptions");
            dataTable.Columns.Add("MaterialMappingAutoID");

            foreach (BE.WorkOrderEntities item in WOdata)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["WareHouseID"] = item.WareHouseID;
                row["VehicleNo"] = item.VehicleNo;
                row["StuffPkgs"] = item.StuffPkgs;
                row["VendorID"] = item.VendorID;
                row["EquipmentID"] = item.EquipmentID;
                row["Description"] = item.Description;
                row["StuffLocation"] = item.StuffLocation;
                row["PkgType"] = item.PkgTypeID;
                row["Weight"] = item.Weight;
                row["CartingAllowId"] = item.CartingAllowId;
                row["Hours"] = item.Hours;
                row["CMB"] = item.CMB;
                row["SBEntryID"] = item.SBEntryID;
                row["SBNo"] = item.SBNo;
                row["OpPkgs"] = item.OpPkgs;
                row["OpWt"] = item.OpWt;
                row["Examine"] = item.Examine;
                row["Surveyor"] = item.Surveyor;
                row["MaterialQty"] = item.MaterialQty;
                row["MaterialDescriptions"] = item.MaterialDescriptions;
                row["MaterialMappingAutoID"] = item.MaterialMappingAutoID;
                dataTable.Rows.Add(row);

                message = ValidateDuplicate(WOType, item.ContainerNo, item.VendorID, item.StuffPkgs, item.VehicleNo, item.Weight);

            }

            if (message == "")
            {
                message = WO.WorkOrderSaveLashingAndChocking(dataTable, WODate, WOType, SBNo, SBEntryID, CHAID, OnAccountID, UserID);
            }

            return Json(message);
        }

        public ActionResult PrintWorkOrderLashingAndChocking(string WONo)
        {
            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getInDL = db.sub_GetDatatable("usp_Print_Export_workOrder_Close '" + WONo + "'");

            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {


                    ViewBag.SrNo = dr["Sr No"];
                    if (!dr.IsNull("WO_NO"))
                    {
                        ViewBag.WO_NO = dr["WO_NO"];
                    }
                    if (!dr.IsNull("CHAName"))
                    {
                        ViewBag.CHAName = dr["CHAName"];
                    }
                    if (!dr.IsNull("con_Name"))
                    {
                        ViewBag.con_Name = dr["con_Name"];
                    }
                    if (!dr.IsNull("AddressI"))
                    {
                        ViewBag.AddressI = dr["AddressI"];
                    }
                    if (!dr.IsNull("shippername"))
                    {
                        ViewBag.shippername = dr["shippername"];
                    }
                    if (!dr.IsNull("IGM_GoodsDesc"))
                    {
                        ViewBag.IGM_GoodsDesc = dr["IGM_GoodsDesc"];
                    }
                    if (!dr.IsNull("Consignee"))
                    {
                        ViewBag.Consignee = dr["Consignee"];

                    }
                    if (!dr.IsNull("Consignee"))
                    {
                        ViewBag.UserName = dr["UserName"];

                    }
                    if (!dr.IsNull("AddedOn"))
                    {
                        ViewBag.AddedOn = dr["AddedOn"];

                    }
                    if (!dr.IsNull("CartingAllowId"))
                    {
                        ViewBag.CartingAllowId = dr["CartingAllowId"];

                    }



                    ViewBag.SBNo = dr["sbno"];
                    ViewBag.ContainerNo = dr["ContainerNo"];
                    ViewBag.Size = dr["Size"];
                    ViewBag.PKGSDestuff = dr["PKGSDestuff"];
                    ViewBag.ExamPer = dr["ExamPer"];
                    ViewBag.Remarks = dr["Remarks"];

                    ViewBag.Weight = dr["Weight"];

                    ViewBag.EquipmentName = dr["EquipmentName"];
                    ViewBag.Wo_Type = dr["Wo_Type"];

                    ViewBag.IGM_Qty = dr["IGM_Qty"];
                    ViewBag.IGM_GrossWt = dr["IGM_GrossWt"];

                    ViewBag.indate = dr["indate"];
                    ViewBag.SurveyorName = dr["SurveyorName"];

                    ViewBag.SurveyorName = dr["SurveyorName"];
                    ViewBag.PKGSRestuff = dr["PKGSRestuff"];
                    ViewBag.CargoWeight = dr["CargoWeight"];
                    ViewBag.Examine = dr["Examine"];
                    ViewBag.vehicleno = dr["vehicleno"];
                    ViewBag.invoiceno = dr["invoiceno"];

                    ViewBag.invoicePKGS = dr["invoicePKGS"];


                    ViewBag.Wo_Type1 = dr["WO type"];
                    ViewBag.printdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy HH:mm");





                    ViewBag.ImportFCLdetailsCustomtally = getInDL.AsEnumerable();
                }
            }
            return PartialView();

        }

        public ActionResult GetcartingIDForworkorder_LashingAndChocking(string cartingNo)
        {
            List<BE.WorkOrderReport> GetcartingDetails = new List<BE.WorkOrderReport>();
            GetcartingDetails = WO.GetcartingIDForworkorder_LashingAndChocking(cartingNo);

            var jsonResult = Json(GetcartingDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetSBNOforworkorder_LashingAndChocking(string SBNO)
        {
            List<BE.WorkOrderReport> GetcartingDetails = new List<BE.WorkOrderReport>();
            GetcartingDetails = WO.GetSBNOForworkorder_LashingAndChocking(SBNO);

            var jsonResult = Json(GetcartingDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetRequestIDforworkorder_LashingAndChocking(string Requestid)
        {
            List<BE.WorkOrderReport> GetcartingDetails = new List<BE.WorkOrderReport>();
            GetcartingDetails = WO.GetRequestIDForworkorder_LashingAndChocking(Requestid);

            var jsonResult = Json(GetcartingDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetContaiinernoforworkorder_LashingAndChocking(string Containerno)
        {
            List<BE.WorkOrderReport> GetcartingDetails = new List<BE.WorkOrderReport>();
            GetcartingDetails = WO.GetContainerWiseworkorderDetails_LashingAndChocking(Containerno);

            var jsonResult = Json(GetcartingDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult EditExportWorkDetailsGrid(string WONo)
        {
            List<BE.FCLsummaryDetails> GetDetails = new List<BE.FCLsummaryDetails>();
            GetDetails = WO.GetExportEditDetailsgird(WONo);

            var jsonResult = Json(GetDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult VendorPrintsurveyor(string WONo)
        {
            DataSet GetDetails = new DataSet();
            DataTable getInDL = new DataTable();
            DataTable GateInLoadedDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            GetDetails = db.sub_GetDataSets("USP_surveyor_Print '" + WONo + "'");

            getInDL = GetDetails.Tables[0];
            GateInLoadedDetails = GetDetails.Tables[1];




            if (getInDL.Rows.Count > 0)
            {
                foreach (DataRow dr in getInDL.Rows)
                {
                    ViewBag.SrNo = dr["Sr No"];
                    ViewBag.WO_NO = dr["WO_NO"];
                    ViewBag.AddedOn = dr["AddedOn"];
                    ViewBag.SBNo = dr["sbno"];
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

                    ViewBag.SurveyorName = dr["SurveyorName"];
                    ViewBag.PKGSRestuff = dr["PKGSRestuff"];
                    ViewBag.CargoWeight = dr["CargoWeight"];
                    ViewBag.Examine = dr["Examine"];
                    ViewBag.vehicleno = dr["vehicleno"];
                    ViewBag.invoiceno = dr["invoiceno"];
                    ViewBag.shippername = dr["shippername"];
                    ViewBag.invoicePKGS = dr["invoicePKGS"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.CartingAllowId = dr["CartingAllowId"];
                    ViewBag.Wo_Type1 = dr["WO type"];
                    ViewBag.imp_ac_emailid = dr["imp_ac_emailid"];
                    ViewBag.notev = dr["notev"];
                    ViewBag.Surveyor_type = dr["Surveyor_type"];
                    ViewBag.printdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy HH:mm");

                    ViewBag.ImportFCLdetailsCustomtally = getInDL.AsEnumerable();

                    ViewBag.FiledsDetails = GateInLoadedDetails.AsEnumerable();
                    ViewBag.count = GateInLoadedDetails.Rows.Count;
                }
            }
            return PartialView();

        }
        public ActionResult GetStuffingRequestDone(string Requestid, string WOType)
        {
            try
            {
                string message = "";
                message = WO.GetCheckStuffingRequestDone(Requestid, WOType);

                var jsonResult = Json(message, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public JsonResult ExportWorkOrderTEmpSave(List<BE.FCLsummaryDetails> Containerdetails, string Containerno1)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("StuffLocation");
            dataTable.Columns.Add("Containerno");
            dataTable.Columns.Add("WarehouseID");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("PKGSDestuff");
            dataTable.Columns.Add("PkgType");
            dataTable.Columns.Add("Weight");

            dataTable.Columns.Add("Vendor");


            dataTable.Columns.Add("EquipmentID");
            dataTable.Columns.Add("Remarks");
            dataTable.Columns.Add("Hours");
            dataTable.Columns.Add("CGM");
            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("CartingAllowId");
            dataTable.Columns.Add("SBentryid");
            dataTable.Columns.Add("SBNO");
            dataTable.Columns.Add("OpeartionalPksg");
            dataTable.Columns.Add("OperationWt");
            dataTable.Columns.Add("Examine");
            dataTable.Columns.Add("Surveyor");
            dataTable.Columns.Add("Userid");
            dataTable.Columns.Add("MaterialQty");
            dataTable.Columns.Add("MaterialDescriptions");
            foreach (BE.FCLsummaryDetails item in Containerdetails)
            {
                DataRow row = dataTable.NewRow();
                row["StuffLocation"] = item.StuffLocation;
                row["Containerno"] = item.Containerno;
                row["WarehouseID"] = item.WarehouseID;
                row["VehicleNo"] = item.VehicleNo;
                row["PKGSDestuff"] = item.PKGSDestuff;
                row["PkgType"] = item.PkgType;
                row["Weight"] = item.Weight;
                row["Vendor"] = item.Vendor;

                row["EquipmentID"] = item.EquipmentID;
                row["Remarks"] = item.Remarks;
                row["Hours"] = item.Hours;
                row["CGM"] = item.CGM;
                row["EntryID"] = item.EntryID;
                row["CartingAllowId"] = item.CartingAllowId;
                row["SBentryid"] = item.SBentryid;
                row["SBNO"] = item.SBNO;
                row["OpeartionalPksg"] = item.OpeartionalPksg;
                row["OperationWt"] = item.OperationWt;
                row["Examine"] = item.Examine;
                row["Surveyor"] = item.Surveyor;
                row["Userid"] = UserID;
                row["MaterialQty"] = item.MaterialQty;
                row["MaterialDescriptions"] = item.MaterialDescriptions;
                dataTable.Rows.Add(row);
            }
            List<BE.FCLsummaryDetails> Igmsummary = new List<BE.FCLsummaryDetails>();

            Igmsummary = WO.ExportSaveTempWorkOrder(dataTable, Containerno1, UserID);

            var returnField = new { List2 = Igmsummary };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
        public ActionResult ClearExportTempWorkorder(string ContainerNo)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            List<BE.WorkOrderReport> GetcartingDetails = new List<BE.WorkOrderReport>();
            GetcartingDetails = WO.ClearExportTempWorkorder(ContainerNo, UserID);

            var jsonResult = Json(GetcartingDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //developed by prathamesh
        public ActionResult LashingAndChockingExpVsRevenueReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetLashingAndChockingExpVsRevenueReport(string FromDate, string ToDate, string Activity)
        {
            DataTable dtOutSourceList = new DataTable();

            if (Activity == "Material")
            {
                int UserID = Convert.ToInt32(Session["userid"]);

                dtOutSourceList = db.sub_GetDatatable("USP_WO_LNC_DETAILS '" + FromDate + "','" + ToDate + "'");

                Session["GetLashingAndChockingExpVsRevenueReport"] = dtOutSourceList;
                string json = JsonConvert.SerializeObject(dtOutSourceList);
                var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }

            if (Activity == "Revenue")
            {
                int UserID = Convert.ToInt32(Session["userid"]);

                dtOutSourceList = db.sub_GetDatatable("USP_GET_WORKORDER_REVENUE_DETAILS '" + FromDate + "','" + ToDate + "'");

                Session["GetLashingAndChockingExpVsRevenueReport"] = dtOutSourceList;
                string json = JsonConvert.SerializeObject(dtOutSourceList);
                var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            else
            {
                int UserID = Convert.ToInt32(Session["userid"]);
                dtOutSourceList = WO.GetLashingAndChockingExpVsRevenueReport(FromDate, ToDate);
                Session["GetLashingAndChockingExpVsRevenueReport"] = dtOutSourceList;
                string json = JsonConvert.SerializeObject(dtOutSourceList);
                var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }

        }
        public ActionResult ExportToExcelReport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["GetLashingAndChockingExpVsRevenueReport"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=WO L&C Revenue & Material Wise.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> WO L&C Revenue & Material Wise Report<strong></td></tr>");
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
        public JsonResult LNCFinalCose(string WOID)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            int i = 0;
            i = db.sub_ExecuteNonQuery("USP_CLOSE_OPEN_LNC_WORK_ORDER '" + WOID + "','" + UserID + "'");
            if (i != 0)
            {
                message = "Work Order " + WOID + " closed successfully";
            }
            else
            {
                message = "Work Order " + WOID + " not closed successfully";
            }
            return Json(message);
        }

        //CODE BY SAGAR MATERIAL ENTRY
        public ActionResult MaterialStockEntry()
        {
            List<BE.Items> ItemList = new List<BE.Items>();
            ItemList = SB.GetItemList();
            //AccountID And Name Is Entities same as entities
            ViewBag.ItemList = new SelectList(ItemList, "itemid", "name");

            List<BE.Items> DepartMent = new List<BE.Items>();
            DepartMent = SB.DepartmentList();
            //AccountID And Name Is Entities same as entities
            ViewBag.DepartMentList = new SelectList(DepartMent, "ID", "Department");
            return View();
        }

        [HttpPost]
        public ActionResult SaveMaterialsDetails(List<BE.Items> Debitdata, int DepartID, string Remarks)
        {



            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Itemno");
            dataTable.Columns.Add("ItemnoName");
            dataTable.Columns.Add("Qty");
            dataTable.Columns.Add("Rate");

            dataTable.TableName = "UTYPE_INSERT_WO_MATERAL_DET";
            foreach (BE.Items item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["Itemno"] = item.Itemno;
                row["ItemnoName"] = item.ItemnoName;
                row["Qty"] = item.Qty;
                row["Rate"] = item.Rate;

                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.Response response = new BE.Response();
            response = BL.SaveWOMaterialBL(Userid, DepartID, Remarks, dataTable);
            return Json(response);

        }

        [HttpPost]
        public JsonResult GetItemsDetails(string prefixText, string Mode)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GET_PURCHASE_ITEM '" + Mode + "','" + prefixText + "'");

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









    }

    class PackageTypeM
    {
       public long PackageId { get; set; }
       public string PackageName { get; set; }
    }
}