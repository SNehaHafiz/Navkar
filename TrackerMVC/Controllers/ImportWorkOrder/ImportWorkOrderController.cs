using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using System.Data;
using DB = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using HC = TrackerMVCDataLayer.Helper;
using System.Data.OleDb;

namespace TrackerMVC.Controllers
{
    [UserAuthenticationFilter]
    public class ImportWorkOrderController : Controller
    {
        // GET: WorkOrder
        BM.WorkOrder.WorkOrder WO = new BM.WorkOrder.WorkOrder();
        DB.DBOperations db = new DB.DBOperations();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ImportWorkOrder()
        {
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListImportWorkOrder();
            ViewBag.WOType = new SelectList(WorkOrderList.WOTypeList, "Wo_Type", "Wo_Type");
            ViewBag.EQType = new SelectList(WorkOrderList.EQWOList, "Id", "Equipment");
            ViewBag.WHList = new SelectList(WorkOrderList.WHList, "WHID", "WHName");
            ViewBag.VendorList = new SelectList(WorkOrderList.VendorWOList, "VendorId", "Name");
            ViewBag.PKGList = new SelectList(WorkOrderList.PKGList, "CodeID", "Package");

            return View();
        }

        public ActionResult ImportWorkOrderSSR()
        {

            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListImportWorkOrder();
            ViewBag.WOType = new SelectList(WorkOrderList.WOTypeList, "Wo_Type", "Wo_Type");
            ViewBag.EQType = new SelectList(WorkOrderList.EQWOList, "Id", "Equipment");
             ViewBag.AccountList = new SelectList(WorkOrderList.ImportAccountMasterList, "AccountID", "AccountName");
            ViewBag.VendorList = new SelectList(WorkOrderList.VendorWOList, "VendorId", "Name");
            ViewBag.CHAList = new SelectList(WorkOrderList.CHAList, "CHANo", "CHAName");

            return View();
        }

        public ActionResult ImportWorkOrderReport()
        {
            BE.WorkOrderEntities WorkOrderList = new BE.WorkOrderEntities();
            WorkOrderList = WO.GetDropDownListImportWorkOrder();
            ViewBag.WOType = new SelectList(WorkOrderList.WOTypeList, "Wo_Type", "Wo_Type");
            ViewBag.EQType = new SelectList(WorkOrderList.EQWOList, "Id", "Equipment");
            ViewBag.WHList = new SelectList(WorkOrderList.WHList, "WHID", "WHName");
            ViewBag.VendorList = new SelectList(WorkOrderList.VendorWOList, "VendorId", "Name");
            ViewBag.PKGList = new SelectList(WorkOrderList.PKGList, "CodeID", "Package");
            return View();
        }

        public ActionResult PortEntry()
        {
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
        public JsonResult ShowCartWeightDets(string SBNO, string WorkType)
        {
            BE.WorkOrderEntities SBList = new BE.WorkOrderEntities();
            SBList = WO.GetCartWeightPkgs(SBNO, WorkType);
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
        public JsonResult TotalPkgsVehicleWise(string SBEntryID, string SBNO, string VehicleNo)
        {
            string CartingId = "0";
            List<BE.WorkOrderEntities> PkgsList = new List<BE.WorkOrderEntities>();
            PkgsList = WO.GetPkgsVehicleWise(SBEntryID, SBNO, VehicleNo, CartingId);
            var jsonResult = Json(PkgsList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult WorkOrderSave(List<BE.WorkOrderEntities> WOdata, string WODate, string WOType,string CHAID,string OnAccountID,string Category, string TruckNo ,string CargoDescription)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            if(CHAID == null)
            {
                CHAID = "0";
            }
            if (OnAccountID == null)
            {
                OnAccountID = "0";
            }
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("StuffPkgs");
            dataTable.Columns.Add("VendorID");
            dataTable.Columns.Add("EquipmentID");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("JoNo");

            foreach (BE.WorkOrderEntities item in WOdata)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["StuffPkgs"] = item.StuffPkgs;
                row["VendorID"] = item.VendorID;
                row["EquipmentID"] = item.EquipmentID;
                row["Description"] = item.Description;
                row["Weight"] = item.Weight;
                row["JoNo"] = item.SBEntryID;
                dataTable.Rows.Add(row);
            }

            string message = WO.SaveWorkOrderImport(dataTable, WODate, WOType,UserID,Convert.ToInt32(CHAID),Convert.ToInt32(OnAccountID),Category,TruckNo, CargoDescription);
            return Json(message);
        }
        public JsonResult ListOpenWorkOrder()
        {
            List<BE.OpenWOListEntities> OpenWOList = new List<BE.OpenWOListEntities>();
            OpenWOList = WO.GetOpenWOListImport();
            var jsonResult = Json(OpenWOList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult CloseWorkOrder(string WONo,string SequenceNo)
        {
            string message = "";
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
           
        
         
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
          
        
            return Json(message);
        }
        public JsonResult ContainerValidation(string ContainerNo, string Category, string ddlWOType)
        {
            List<BE.WorkOrderEntities> ConList = new List<BE.WorkOrderEntities>();
            ConList = WO.GetConValListImport(ContainerNo, Category, ddlWOType);
            var jsonResult = Json(ConList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult TruckValidation(string TruckNo, string Category, string ddlWOType)
        {
            List<BE.WorkOrderEntities> ConList = new List<BE.WorkOrderEntities>();
            ConList = WO.GetTruckValListImport(TruckNo, Category, ddlWOType);
            var jsonResult = Json(ConList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult TruckBOE(string TruckNo, string Category, string ddlWOType)
        {
            List<BE.WorkOrderEntities> ConList = new List<BE.WorkOrderEntities>();
            ConList = WO.GetBOEValListImport(TruckNo, Category, ddlWOType);
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
            return View();
        }
        public JsonResult ShowWorkOrderReport(string FromDate, string ToDate, string Criteria, string SearchText, string Category)
        {
            DB.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("GetImportWorkOrderRep '" + FromDate + "','" + ToDate + "','" + Criteria + "','" + SearchText + "','" + Category + "'");
            Session["ImpWorkOrderList"] = dt;
            Session["FromDateW"] = FromDate;
            Session["ToDateW"] = ToDate;
            var WOReportList = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(WOReportList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelImpWorkOrder()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["ImpWorkOrderList"];
            string FromDate = Session["FromDateW"].ToString();
            string ToDate = Session["ToDateW"].ToString();
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ImportWoReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Import Work Order Reports <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> From :" + FromDate + " To:" + ToDate +" <strong></td></tr>");
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
        public ActionResult ImportLabourActivityReportPrint(string FromDate, string ToDate, string Vendor, string Category)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblLabourVendorList = new DataTable();
            DataTable tblLabourCustomExamList = new DataTable();
            DataTable tblLabourDestuffThapiList = new DataTable();
            DataTable tblLabourDrumList = new DataTable();
            DataTable tblLabourSealingList = new DataTable();
            DataTable tblLabourTapiEntryList = new DataTable();
            DataTable tblBondLoading = new DataTable();
            DataTable tblBondUnLoading = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetLabourActivityReportImport '" + FromDate + "','" + ToDate + "','" + Vendor + "','" + Category + "'");

            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblLabourVendorList = getJobOrderSet.Tables[1];
                tblLabourCustomExamList = getJobOrderSet.Tables[2];
                tblLabourDestuffThapiList = getJobOrderSet.Tables[3];
                tblLabourDrumList = getJobOrderSet.Tables[4];
                tblLabourSealingList = getJobOrderSet.Tables[5];
                tblLabourTapiEntryList = getJobOrderSet.Tables[6];
                tblBondLoading = getJobOrderSet.Tables[7];
                tblBondUnLoading = getJobOrderSet.Tables[8];



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

            ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.LabourVendorList = tblLabourVendorList.AsEnumerable();
            ViewBag.LabourCustomExamList = tblLabourCustomExamList.AsEnumerable();
            ViewBag.LabourDestuffThapiList = tblLabourDestuffThapiList.AsEnumerable();
            ViewBag.LabourDrumList = tblLabourDrumList.AsEnumerable();
            ViewBag.LabourSealingList = tblLabourSealingList.AsEnumerable();
            ViewBag.LabourTapiEntryList = tblLabourTapiEntryList.AsEnumerable();
            ViewBag.tblBondLoading = tblBondLoading.AsEnumerable();
            ViewBag.tblBondUnLoading = tblBondUnLoading.AsEnumerable();


            return PartialView();

        }

        public JsonResult getInvoiceWorkOrderDet(string ContainerNo,string IGMNo,string ItemNo, string SSRType)
        {
            string Message = "";
            HC.DBOperations db = new DB.DBOperations();
            
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            BE.CHAMaster cHA = new BE.CHAMaster();
            List<BE.WorkOrderEntities> ContainerList = new List<BE.WorkOrderEntities>();
            dt = db.sub_GetDatatable("Select TOP 1 * From Import_SSR WHERE   Type='" + SSRType + "' AND (IGMNo='" + IGMNo + "' AND ItemNo='" + ItemNo + "' ) or ContainerNo='" + ContainerNo + "' AND iscancel=0 ");


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Message = "IGM-Item No is already exit for selected equipments type. are you sure to generate again SSR. ?";
                }
            }

            ds = db.sub_GetDataSets("GetImportSSRData '" + IGMNo + "','" + ItemNo + "','" + ContainerNo + "'");

            if (ds.Tables[0].Rows.Count <= 0)
            {
                cHA.errorMessage = "IGM/Item No is Not Valid. Please try again.";
            }
            else
            {
                if (ds != null)
                {
                    dt1 = ds.Tables[0];
                    dt2 = ds.Tables[1];

                    cHA.CHAID = Convert.ToInt32(dt1.Rows[0]["CHAID"]);
                    cHA.ContactNo1 = dt1.Rows[0]["ContactNo"].ToString();
                    cHA.CHAName = dt1.Rows[0]["ChaName"].ToString();
                    cHA.ContactPerson = dt1.Rows[0]["IGMNo"].ToString();
                    cHA.City = dt1.Rows[0]["ItemNo"].ToString();
                    cHA.ContactNo2 = dt1.Rows[0]["ContainerNo"].ToString();
                    cHA.errorMessage = "";
                }

                foreach (DataRow row in dt2.Rows)
                {
                    BE.WorkOrderEntities workOrderEntities = new BE.WorkOrderEntities();
                    workOrderEntities.ContainerNo = row["ContainerNo"].ToString();
                    workOrderEntities.Type = row["ContainerType"].ToString();
                    workOrderEntities.Size = row["Size"].ToString();
                    workOrderEntities.ManifestQty = row["IGM_Qty"].ToString();
                    workOrderEntities.Weight = row["Weight"].ToString();
                    workOrderEntities.JoNo = Convert.ToInt64(row["JONo"]);
                    workOrderEntities.IGMNo = row["IGMNo"].ToString();
                    workOrderEntities.ItemNo = row["ItemNo"].ToString();

                    ContainerList.Add(workOrderEntities);
                }
            }

            var returnField = new { workOrder = cHA, ContainerData = ContainerList, Message=Message };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult SSRWorkOrderSave(List<BE.WorkOrderEntities> WOdata, string SSRType, string Equipment, string Vendor,string CHA,string AccountID)
        {
            string message = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);

            if (UserID == 0)
            {
                message = "Session expire. Please try Again.";
            }
           
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("JoNo");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("IGMNo");
            dataTable.Columns.Add("ItemNo");
            dataTable.Columns.Add("IGMQty");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("ExaminPerc");
            dataTable.Columns.Add("Amount");
            dataTable.Columns.Add("Remarks");

            foreach (BE.WorkOrderEntities item in WOdata)
            {
                DataRow row = dataTable.NewRow();

                row["JoNo"] = item.JoNo;
                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Type"] = item.Type;
                row["IGMNo"] = item.IGMNo;
                row["ItemNo"] = item.ItemNo;
                row["IGMQty"] = item.ManifestQty;
                row["Weight"] = item.Weight;
                row["ExaminPerc"] = item.ExamPerc;
                row["Amount"] = item.Amount;
                row["Remarks"] = item.Narration;

                dataTable.Rows.Add(row);
            }

            if (message == "")
            {
                message = WO.SaveSSRImport(dataTable, SSRType, Equipment, Vendor, UserID.ToString(),CHA, AccountID);
            }

            return Json(message);
        }

        public JsonResult getExpWoSSr(string FromDate, string ToDate)
        {
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("GetImportWOrkOrederSSSummary '" + FromDate + "','" + ToDate + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            dt.Columns.Remove("Action");
            Session["GetImportWOrkOrederSSSummary"] = dt;
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ExportToExcelSSR()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetLCLDestuffTally = (DataTable)Session["GetImportWOrkOrederSSSummary"];

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
            DataTable dataTable = new DataTable();
            string Message = "";
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            string MenuID = "6343";
            dataTable = db.sub_GetDatatable("USP_Check_rights '" + MenuID + "','" + UserID + "'");
            if (dataTable.Rows[0].Field<int>("ID") != 1)
            {
                Message = "You are not authorised to cancel please contact to I.T team.";
            }
            if (Message == "")
            {
                i = db.sub_ExecuteNonQuery("USP_CancelSSRImport '" + SSRNo + "','" + UserID + "'");

                if (i > 0)
                {
                    Message = "SSR Cancel Successfully.";
                }
                else
                {
                    Message = "SSR Not Cancel Successfully.";
                }
            }
          

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportSSRPrint(string SSRNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("ImportSSRWorkOrderPrint '" + SSRNo + "'");

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
                    ViewBag.IgmNo = dr["IgmNo"];
                    ViewBag.ItemNo = dr["ItemNo"];
                    ViewBag.CHAName = dr["CHAName"];
                    ViewBag.PreparedBy = dr["PreparedBy"];
                    ViewBag.Category = dr["Type"];
                }
            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();

            return PartialView();

        }

        public JsonResult PortEntryView(string ContainerNo, string TruckNo,string CycleType)
        {
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            var summaryDet = "";
            dt = db.sub_GetDatatable("USP_ValidatePortContCheck '" + ContainerNo + "','" + TruckNo + "','" + CycleType + "'");
            if (dt.Rows.Count <= 0)
            {
                summaryDet = "Invlaid Details. Please enter corrent details.";
            }
            else
            {
                summaryDet=JsonConvert.SerializeObject(dt);
            }

            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult PartyWiseSave(string PortEntryNo, string PortEntryDate, string CycleType, string ContNo, string TruckNo)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            HC.DBOperations db = new DB.DBOperations();
            int returnVal = 0;
            string Message = "";

            returnVal = db.sub_ExecuteNonQuery("USP_INSPortUserEntry '" + PortEntryDate + "','" + CycleType + "','" + ContNo + "','" + TruckNo + "','" + UserID + "'");

            return Json(returnVal, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PortEntrySummary(string FromDate, string ToDate)
        {
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            //BE.WorkOrderEntities workOrder = new BE.WorkOrderEntities();
            dt = db.sub_GetDatatable("USP_GetPortUserEntry '" + FromDate + "','" + ToDate + "'");

            Session["PortEntryExcel"] = dt;
            Session["FromDateP"] = FromDate;
            Session["ToDateP"] = ToDate;

            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToPortUserEntry()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["PortEntryExcel"];
            string FromDate = Session["FromDateP"].ToString();
            string ToDate = Session["ToDateP"].ToString();
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PortUserEntry.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Port User Entry Summary Report <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> From :" + FromDate + " To:" + ToDate + " <strong></td></tr>");
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




        // other Invoices



        // Generate Other invoices

        public ActionResult GenerateotherInvoices()
        {
            List<BE.ImportAccountMaster> AccountHead = new List<BE.ImportAccountMaster>();
            AccountHead = WO.GetAccountHead();
            ViewBag.AccountHead = new SelectList(AccountHead, "AccountID", "AccountName");
            List<BE.SettingTax> settingTaxes = new List<BE.SettingTax>();
            settingTaxes = WO.GetSettingtax();
            ViewBag.Settingtax = new SelectList(settingTaxes, "Settingid", "TaxName");


            List<BE.CHA> ChaList = new List<BE.CHA>();
            ChaList = WO.getCHA();
            ViewBag.ChaLists = new SelectList(ChaList, "CHANo", "CHAName");


            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = WO.getLocation();
            ViewBag.LocationDetails = new SelectList(Location, "LocationID", "Location");

            List<BE.Commodity> Commodity = new List<BE.Commodity>();
            Commodity = WO.getAccountAdd();
            ViewBag.CommodityDetails = new SelectList(Commodity, "CommodityID", "CommodityName");


            List<BE.TransportMode> Transport  = new List<BE.TransportMode>();
            Transport = WO.GetTranspot();
            ViewBag.TransportyDetails = new SelectList(Transport, "TrasportID", "TransportName");

            List<BE.CargoType> CargoType = new List<BE.CargoType>();
            CargoType = WO.GatCargoTypeFill();
            ViewBag.CargoTypeDetails = new SelectList(CargoType, "Cargotypeid", "Cargotype");


            BE.SettingTax TINNUMBER = new BE.SettingTax();
            TINNUMBER = WO.GetsettingCode();
            ViewBag.TINUMBER = TINNUMBER.TaxName;

            return View();

        }
        [HttpPost]
        public ActionResult SaveOtherInvoicesDetails(List<BE.OtherInvoicesEntities> InvoiceDetails, string InvoiceNo, string WorkYear, string InvoiceDate,
            string validDate, string GSTNo, string GSTName, string GSTID, string IGMNo, string ItemNo, string BlNo, string Containerno, string Size,
            string Consignee, string Port, string TotalAmt, string SGST, string CGST, string IGST, string GrandAmt, string StateCode, string Remarks, string Chaid, 
            string CargoDesc, string CmmodityID,string InvoiceType,string TCSAmount)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Amount");
            dataTable.Columns.Add("AccountID");
            dataTable.Columns.Add("AccountName");
            dataTable.Columns.Add("GroupID");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("PKGS");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("Vehicleno");
            dataTable.Columns.Add("Fromlocation");
            dataTable.Columns.Add("TOlocation");
            dataTable.Columns.Add("IGSTADD");
            dataTable.Columns.Add("CGSTADD");
            dataTable.Columns.Add("SGSTADD");
            dataTable.Columns.Add("Frlocid");
            dataTable.Columns.Add("Tolocid");
            dataTable.Columns.Add("TransportID");
            dataTable.Columns.Add("CargoTypeID");


            foreach (BE.OtherInvoicesEntities item in InvoiceDetails)
            {
                DataRow row = dataTable.NewRow();
                row["Amount"] = item.Amount;
                row["AccountID"] = item.AccountID;
                row["AccountName"] = item.AccountName;
                row["GroupID"] = item.GroupID;
                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["PKGS"] = item.PKGS;
                row["Weight"] = item.Weight;
                row["Vehicleno"] = item.Vehicleno;
                row["Fromlocation"] = item.Fromlocation;
                row["TOlocation"] = item.TOlocation;
                row["IGSTADD"] = item.IGSTADD;
                row["CGSTADD"] = item.CGSTADD;
                row["SGSTADD"] = item.SGSTADD;
                row["Frlocid"] = item.Frlocid;
                row["Tolocid"] = item.Tolocid;
                row["TransportID"] = item.TransportID;
                row["CargoTypeID"] = item.CargoTypeID;
                dataTable.Rows.Add(row);
            }


            string strsql;
            string strsql1;
            string strsql2;
            string strInvoiceNo = "";
            double intid = 0;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            double dblassessno;
            int intContCounter;
            int intSLID;
            int intCHAID;
            int intOtherID;
            string AccountId = "";
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            //strsql = "";
            //strsql = "Delete from Temp_Import_assessD Where userid='" + Userid + "' ";
            //dt = db.sub_GetDatatable(strsql);
            if (InvoiceNo == "New")
            {
                strsql = "";
                strsql = "select isnull(max(assessno),0)   as imp_proformano from Other_Proformam WHERE WorkYear='" + WorkYear + "'";
                dt = db.sub_GetDatatable(strsql);
                string str = WorkYear;
                str = str.Remove(0, 2);
                if (dt.Rows.Count > 0)
                {
                    intid = Convert.ToInt32(dt.Rows[0].Field<Int64>("imp_proformano")) + 1;
                    strInvoiceNo = "MISC/" + str + "/000" + intid;
                    InvoiceNo = Convert.ToString(intid);
                }
                else
                {
                    intid = 1;
                    strInvoiceNo = "MIS/" + str + "/000" + intid;
                    InvoiceNo = Convert.ToString(intid);
                }
                if (InvoiceNo.Length == 1)
                    strInvoiceNo = "MISC/" + "0000" + intid + "/" + str;
                else if (InvoiceNo.Length == 2)
                    strInvoiceNo = "MISC/" + "000" + intid + "/" + str;
                else if (InvoiceNo.Length == 3)
                    strInvoiceNo = "MISC/" + "00" + intid + "/" + str;
                else if (InvoiceNo.Length == 4)
                    strInvoiceNo = "MISC/" + "0" + intid + "/" + str;
                else if (InvoiceNo.Length == 5)
                    strInvoiceNo = "MISC/" + "" + intid + "/" + str;
            }


            // ========================================= Save in Other_assessDII=================================
            strsql = "";
            strsql = "Delete from Other_ProformaDII Where AssessNo='" + InvoiceNo + "' AND workyear ='" + WorkYear + "'";
            dt = db.sub_GetDatatable(strsql);
            for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
            {
                AccountId = dataTable.Rows[i].Field<string>("AccountID");
                strsql = "";
                strsql = "exec sp_insert_assessDIIWeb'" + InvoiceNo + "','" + IGMNo + "' ,'" + ItemNo + "',";
                strsql += " '" + dataTable.Rows[i].Field<string>("AccountID") + "' ,'" + Remarks + "','" + dataTable.Rows[i].Field<string>("Amount") + "',";
                strsql += " '" + dataTable.Rows[i].Field<string>("SGSTADD") + "','" + dataTable.Rows[i].Field<string>("CGSTADD") + "','" + dataTable.Rows[i].Field<string>("IGSTADD") + "','" + WorkYear + "' ,'" + dataTable.Rows[i].Field<string>("GroupID") + "' ,'" + dataTable.Rows[i].Field<string>("ContainerNo") + "' ,'" + dataTable.Rows[i].Field<string>("Size") + "',";
                strsql += " '" + dataTable.Rows[i].Field<string>("PKGS") + "','" + dataTable.Rows[i].Field<string>("Weight") + "','" + dataTable.Rows[i].Field<string>("Vehicleno") + "','" + dataTable.Rows[i].Field<string>("Frlocid") + "' ,'" + dataTable.Rows[i].Field<string>("Tolocid") + "','" + dataTable.Rows[i].Field<string>("TransportID") + "','" + dataTable.Rows[i].Field<string>("CargoTypeID") + "'";

                dt = db.sub_GetDatatable(strsql);
            }

            // ========================================= Save in other_assessM =================================
            strsql = "";
            strsql = "SELECT top 1 assessno FROM Other_Proformam where workyear='" + WorkYear + "' and assessno=" + InvoiceNo + " ";
            dt = db.sub_GetDatatable(strsql);
            if (dt.Rows.Count > 0)
            {
                strsql = "";
                strsql = " SP_UpdateOtherInvoiceweb '" + InvoiceNo + "','" + WorkYear + "','" + Convert.ToDateTime(validDate).ToString("dd MMM yyyy HH:mm") + "','" + IGMNo + "',";
                strsql += " '" + ItemNo + "','" + BlNo + "','" + Remarks + "','" + TotalAmt + "','" + GrandAmt + "','" + CGST + "','" + SGST + "','" + IGST + "',";
                strsql += " '" + GSTID + " ', '" + GrandAmt + "'";
                dt = db.sub_GetDatatable(strsql);
            }
            else
            {
                strsql = "";
                string strSGSTPer = "@";
                strsql = " SP_OtherInvoices_Save_MWeb '" + intid + "','" + WorkYear + "','" + strInvoiceNo + "','" + Convert.ToDateTime(InvoiceDate).ToString("dd MMM yyyy HH:mm") + "','" + Convert.ToDateTime(validDate).ToString("dd MMM yyyy HH:mm") + "','" + IGMNo + "',";
                strsql += " '" + ItemNo + "','" + BlNo + "','" + Remarks + "','" + TotalAmt + "','" + GrandAmt + "', '" + strSGSTPer + "','" + strSGSTPer + "','" + strSGSTPer + "','" + SGST + "','" + CGST + "','" + IGST + "',";
                strsql += " '" + Userid + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd MMM yyyy HH:mm") + "','" + GSTID + " ', '" + Consignee + "','" + Chaid + "','" + CargoDesc + "','" + CmmodityID + "','" + AccountId + "','" + InvoiceType + "','"+TCSAmount+"'";
                dt = db.sub_GetDatatable(strsql);
            }
            string message = " Proforma no.:  " + InvoiceNo + "  generated successfully.!";
            return Json(message);

        }


        public JsonResult GetGSTDetailsForCalculate(string TaxID)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.TextBoXValuesForImportPerforma GetGST = new BE.TextBoXValuesForImportPerforma();
            dt = db.sub_GetDatatable("select SGST,CGst,igst  from settings_taxes where settingsID =  '" + TaxID + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    GetGST.SGST = Convert.ToDouble(row["SGST"]);
                    GetGST.CGST = Convert.ToDouble(row["CGst"]);
                    GetGST.IGST = Convert.ToDouble(row["igst"]);
                }
            }

            return Json(GetGST);
        }

        public ActionResult PendingProformaForOtherInvoice()
        {
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = WO.getParty();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
            return View();

        }

        public JsonResult GetPendingproformaOtherDetails(string SearchCriteria, string Search, string Search1)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_ListOfPendingProformaInvoiceforOthers '" + SearchCriteria + "','" + Search + "','" + Search1 + "'");

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);

            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("View");
                dt.Columns.Remove("Submit");
                dt.Columns.Remove("Cancel");
            }



            Session["ListOfPendingProformaInvoiceforFinalConfirm"] = dt;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult CancelOtherinvoiceProforma(string AssessNo, string workyear)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = WO.CancelOtherInvoicePorforma(AssessNo, workyear, userId);
            return Json(message);
        }

        [HttpPost]
        public ActionResult SubmitOtherDetailsEntry(string AssessNo, string workyear)
        {
            string message = "";
            string strinvoiceNo = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);


            message = WO.SubmitOtherFinalDetails(AssessNo, workyear, userId);

            message = AssessNo;
            return Json(message);
        }
        [HttpPost]
        public ActionResult CheckCancelAssessmentDetails(string InvoiceNo, string workyear)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = WO.CheckCancelAssessmentDetails(InvoiceNo, workyear);
            return Json(message);
        }
        [HttpPost]
   
        public ActionResult CancelAssessmentDetails(string remarks, string Assessno, string WorkYear)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = WO.CancelAssessmentDetails(remarks, Assessno, WorkYear, userId);
            return Json(message);
        }
        public ActionResult ImportOtherProformaPrint(string InvoiceNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblChargesDetails = new DataTable();
            DataTable tblSacDesc = new DataTable();
            DataTable tblContainerdetails = new DataTable();

            DataTable tblBankDetails = new DataTable();
            DataTable tabkchargs = new DataTable();
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

            getJobOrderSet = db.sub_GetDataSets("getOtherProformaPrintMVC '" + InvoiceNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblInvoiceDetails = getJobOrderSet.Tables[1];

                tblChargesDetails = getJobOrderSet.Tables[2];

                tblBankDetails = getJobOrderSet.Tables[3];
                tblSacDesc = getJobOrderSet.Tables[4];
                tblContainerdetails = getJobOrderSet.Tables[5];
                tabkchargs = getJobOrderSet.Tables[6];
                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.CINNo = dr["CINNo"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.Addressl = dr["AddressI"];
                    ViewBag.IMP_AC_EMAILID = dr["IMP_AC_EMAILID"];
                    ViewBag.NoteVS = dr["NoteVI"];

                    ViewBag.Datetime = Convert.ToDateTime(DateTime.Now).ToString("dd MM yyyy");
                    ViewBag.TIme = Convert.ToDateTime(DateTime.Now).ToString("HH:mm");
                }
                foreach (DataRow dr in tblInvoiceDetails.Rows)
                {
                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.InvoiceDate = dr["AssessDate"];
                    ViewBag.Name = dr["GSTName"];
                    ViewBag.Address = dr["GSTAddress"];
                    ViewBag.GSTIN = dr["GSTIn_uniqID"];
                    ViewBag.PlaceofSupply = dr["State"];
                    ViewBag.IgmItemNo = dr["IGMNo"] + "/" + dr["ItemNo"];
                    ViewBag.BLNo = dr["BLNumber"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.AGName = dr["AGName"];
                    ViewBag.chaName = dr["chaName"];
                    ViewBag.cargodesc = dr["cargodesc"];
                    ViewBag.Note = dr["Note"];
                    ViewBag.Other_DisplayName = dr["Other_DisplayName"];
                    ViewBag.TrasnportReamarks = dr["TrasnportReamarks"];
                }

                ViewBag.BankName = tblBankDetails.Rows[0]["BankName"];
                ViewBag.AccountNo = tblBankDetails.Rows[0]["AccountNo"];
                ViewBag.IFSCCode = tblBankDetails.Rows[0]["IFSCCode"];
                ViewBag.Remarks = tblBankDetails.Rows[0]["Remarks"];
                ViewBag.BranchName = tblBankDetails.Rows[0]["BranchName"];

                ViewBag.TotalAmountInWord = tblChargesDetails.Rows[0]["TotalAmountInWords"];
                ViewBag.TotalAmount = tblChargesDetails.Rows[0]["GrandTotal"];
                ViewBag.AmountWithoutTax = tblChargesDetails.Rows[0]["NetTotal"];
                ViewBag.SGSTAmount = tblChargesDetails.Rows[0]["SGST"];
                ViewBag.CGSTAmount = tblChargesDetails.Rows[0]["CGST"];
                ViewBag.IGSTAmount = tblChargesDetails.Rows[0]["IGST"];
                ViewBag.TaxAmount = Convert.ToDouble(Convert.ToDouble(tblChargesDetails.Rows[0]["SGST"]) + Convert.ToDouble(tblChargesDetails.Rows[0]["CGST"]) + Convert.ToDouble(tblChargesDetails.Rows[0]["IGST"]));
            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.AccountDetails = tblSacDesc.AsEnumerable();
            ViewBag.Containerdetails = tblContainerdetails.AsEnumerable();
            ViewBag.HeadDetailsList = tabkchargs.AsEnumerable();
            if (tblContainerdetails.Rows.Count > 0)
            {
                ViewBag.ContainerdetailsCount = tblContainerdetails.Rows.Count;
            }




            foreach (DataRow data in tblSacDesc.Rows)
            {
                Amount = Amount + Convert.ToDouble(data["Amount"]);
                Discount = Convert.ToDouble(data["Discount"]);
                NetAmount = NetAmount + Convert.ToDouble(data["NetAmount"]);
                Srate = Convert.ToString(data["Srate"]);
                CRate = Convert.ToString(data["CRate"]);
                IRate = Convert.ToString(data["IRate"]);
                SGST = SGST + Convert.ToDouble(data["SGST"]);
                CGST = CGST + Convert.ToDouble(data["CGST"]);
                IGST = IGST + Convert.ToDouble(data["IGST"]);
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

            return PartialView();

        }
         
        //[HttpPost]
        public ActionResult ImportOtherInvoicePrint(string InvoiceNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblChargesDetails = new DataTable();
            DataTable tblSacDesc = new DataTable();
            DataTable tblContainerdetails = new DataTable();

            DataTable tblBankDetails = new DataTable();
            DataTable tabkchargs = new DataTable();
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

            getJobOrderSet = db.sub_GetDataSets("getOtherInvoicePrintMVC '" + InvoiceNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblInvoiceDetails = getJobOrderSet.Tables[1];

                tblChargesDetails = getJobOrderSet.Tables[2];

                tblBankDetails = getJobOrderSet.Tables[3];
                tblSacDesc = getJobOrderSet.Tables[4];
                tblContainerdetails = getJobOrderSet.Tables[5];
                tabkchargs = getJobOrderSet.Tables[6];
                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.CINNo = dr["CINNo"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.Addressl = dr["AddressI"];
                    ViewBag.IMP_AC_EMAILID = dr["IMP_AC_EMAILID"];
                    ViewBag.NoteVS = dr["NoteVI"];
                  

                    ViewBag.Datetime = Convert.ToDateTime(DateTime.Now).ToString("dd MM yyyy");
                    ViewBag.TIme = Convert.ToDateTime(DateTime.Now).ToString("HH:mm");
                }
                foreach (DataRow dr in tblInvoiceDetails.Rows)
                {
                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.InvoiceDate = dr["AssessDate"];
                    ViewBag.Name = dr["GSTName"];
                    ViewBag.Address = dr["GSTAddress"];
                    ViewBag.GSTIN = dr["GSTIn_uniqID"];
                    ViewBag.PlaceofSupply = dr["State"];
                    ViewBag.IgmItemNo = dr["IGMNo"] + "/" + dr["ItemNo"];
                    ViewBag.BLNo = dr["BLNumber"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.UserName = dr["UserName"];
                    ViewBag.AGName = dr["AGName"];
                    ViewBag.chaName = dr["chaName"];
                    ViewBag.cargodesc = dr["cargodesc"];
                    ViewBag.Note = dr["Note"];
                    ViewBag.Other_DisplayName = dr["Other_DisplayName"];
                    ViewBag.Irn = dr["Irn"];
                    ViewBag.AckNo = dr["AckNo"];
                    ViewBag.AckDt = dr["AckDt"];
                    ViewBag.SignedQRcode = dr["SignedQRcode"];
                    ViewBag.TrasnportReamarks = dr["TrasnportReamarks"];
                    ViewBag.UPINO = dr["UPINumber"];
                    ViewBag.Registered = dr["TyepReg"];
                }

                ViewBag.BankName = tblBankDetails.Rows[0]["BankName"];
                ViewBag.AccountNo = tblBankDetails.Rows[0]["AccountNo"];
                ViewBag.IFSCCode = tblBankDetails.Rows[0]["IFSCCode"];
                ViewBag.Remarks = tblBankDetails.Rows[0]["Remarks"];
                ViewBag.BranchName = tblBankDetails.Rows[0]["BranchName"];

                ViewBag.TotalAmountInWord = tblChargesDetails.Rows[0]["TotalAmountInWords"];
                ViewBag.TotalAmount = tblChargesDetails.Rows[0]["GrandTotal"];
                ViewBag.AmountWithoutTax = tblChargesDetails.Rows[0]["NetTotal"];
                ViewBag.SGSTAmount = tblChargesDetails.Rows[0]["SGST"];
                ViewBag.CGSTAmount = tblChargesDetails.Rows[0]["CGST"];
                ViewBag.IGSTAmount = tblChargesDetails.Rows[0]["IGST"];
                ViewBag.TCSAmt = tblBankDetails.Rows[0]["TCSAmt"];
                ViewBag.TaxAmount = Convert.ToDouble(Convert.ToDouble(tblChargesDetails.Rows[0]["SGST"]) + Convert.ToDouble(tblChargesDetails.Rows[0]["CGST"]) + Convert.ToDouble(tblChargesDetails.Rows[0]["IGST"]));
            }

            ViewBag.InvoiceItemList = tblInvoiceDetails.AsEnumerable();
            ViewBag.AccountDetails = tblSacDesc.AsEnumerable();
            ViewBag.Containerdetails = tblContainerdetails.AsEnumerable();
            ViewBag.HeadDetailsList = tabkchargs.AsEnumerable();
            if (tblContainerdetails.Rows.Count > 0)
            {
                ViewBag.ContainerdetailsCount = tblContainerdetails.Rows.Count;
            }

           


            foreach (DataRow data in tblSacDesc.Rows)
            {
                Amount = Amount + Convert.ToDouble(data["Amount"]);
                Discount = Convert.ToDouble(data["Discount"]);
                NetAmount = NetAmount + Convert.ToDouble(data["NetAmount"]);
                Srate = Convert.ToString(data["Srate"]);
                CRate = Convert.ToString(data["CRate"]);
                IRate = Convert.ToString(data["IRate"]);
                SGST = SGST + Convert.ToDouble(data["SGST"]);
                CGST = CGST + Convert.ToDouble(data["CGST"]);
                IGST = IGST + Convert.ToDouble(data["IGST"]);
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

            return PartialView();

        }

        public JsonResult GetOtherPendingInvoiceToday(string fromdate, string Todate, string searchCerteria, string Searchtext, string Searchtext1)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_ImportotherAssessList '" + fromdate + "','" + Todate + "','" + searchCerteria + "','" + Searchtext + "','" + Searchtext1 + "'");
             
            string json = JsonConvert.SerializeObject(dt);
            dt.Columns.Remove("Print");
            dt.Columns.Remove("Cancel");
            Session["importAssessListPending"] = dt;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ImportProformaGSTSearch()
        {

            return PartialView();
        }
        public JsonResult GSTSearch(string SearchText)
        {
            List<BE.ImportProformaSearchGST> GstList = new List<BE.ImportProformaSearchGST>();
            GstList = WO.GetGSTList(SearchText);
            var jsonResult = Json(GstList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult getLoctionName(string prefixText, string Mode)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GetLoctionName '" + Mode + "','" + prefixText + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.Customer customer = new BE.Customer();
                    customer.GSTuniqueid = Convert.ToInt32(row["GSTID"]);
                    customer.GstuniqueName = Convert.ToString(row["GSTName"]);
                    Customerlst.Add(customer);
                }
            }

            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public JsonResult getLoctionNameto(string prefixText, string Mode)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GetLoctionName '" + Mode + "','" + prefixText + "'");

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
        [HttpPost]
        public ActionResult CheckContainerGenInvoice(string Containerno)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = WO.CheckContainerGenInvoiceBL(Containerno);
            return Json(message);
        }
        public JsonResult getExpWoSSRPending(string FromDate, string ToDate)
        {
            HC.DBOperations db = new DB.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("GetImportWOrkOrederPending '" + FromDate + "','" + ToDate + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
           
            Session["GetImportWOrkOrederPending"] = dt;
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ExportToExcelSSRPending()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetLCLDestuffTally = (DataTable)Session["GetImportWOrkOrederPending"];

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
        [HttpPost]
        public JsonResult AjaxGetInvoiceDetails(BE.InvoiceUpload fileData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            
            BE.InvoiceUpload VouchertraiffDetails = new BE.InvoiceUpload();
            List<BE.InvoiceUpload> GetListMsg = new List<BE.InvoiceUpload>();
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
                          
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                         
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

                                    DataTable VOucherTraifftDT = new DataTable();


                                    VOucherTraifftDT.Columns.Add("Amount");
                                    VOucherTraifftDT.Columns.Add("AccountID");
                                    VOucherTraifftDT.Columns.Add("AccountName");
                                    VOucherTraifftDT.Columns.Add("Group");
                                    VOucherTraifftDT.Columns.Add("ContainerNo");
                                    VOucherTraifftDT.Columns.Add("Size");
                                    VOucherTraifftDT.Columns.Add("PKGS");
                                    VOucherTraifftDT.Columns.Add("Weight");
                                    VOucherTraifftDT.Columns.Add("FromLocation");
                                    VOucherTraifftDT.Columns.Add("TOlocation");
                                    VOucherTraifftDT.Columns.Add("Vehicleno");
                                    VOucherTraifftDT.Columns.Add("dblIGST");
                                    VOucherTraifftDT.Columns.Add("dblCGST");
                                    VOucherTraifftDT.Columns.Add("dblSGST");
                                    VOucherTraifftDT.Columns.Add("FromLocationID");
                                    VOucherTraifftDT.Columns.Add("TOlocationID");
                                    VOucherTraifftDT.Columns.Add("TransportID");
                                    VOucherTraifftDT.Columns.Add("CargoTypeID");
                                    VOucherTraifftDT.Columns.Add("TransportType");
                                    VOucherTraifftDT.Columns.Add("CargoType");
                                    VOucherTraifftDT.Columns.Add("message");
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        if (row[0].ToString() != "" && row[1].ToString() != "" && row[2].ToString() != "" && row[3].ToString() != "" && row[4].ToString() != "" && row[5].ToString() != "" && row[6].ToString() != "" && row[7].ToString() != "")
                                        {
                                            DataRow dar = VOucherTraifftDT.NewRow();
                                            dar["Amount"] = row[7].ToString();
                                            dar["AccountID"] = fileData.AccountID;
                                            dar["AccountName"] = fileData.AccountName;
                                            dar["Group"] = fileData.Group;
                                            dar["ContainerNo"] = row[0].ToString();
                                            dar["Size"] = row[1].ToString();
                                            dar["PKGS"] = row[3].ToString().ToUpper();
                                            dar["Weight"] = row[4].ToString().ToUpper();
                                            dar["FromLocation"] = row[5].ToString();
                                            if(row[5].ToString() != "")
                                            {
                                                DataTable dt1 = new DataTable();
                                                dt1 = db.sub_GetDatatable("select top 1 LocationID from Ext_Location_M Where Location =  '" + row[5].ToString() + "'");
                                                if (dt1.Rows.Count > 0)
                                                {
                                                    foreach (DataRow row1 in dt1.Rows)
                                                    {
                                                        dar["FromLocationID"] = Convert.ToString(row1["LocationID"]);
                                                      
                                                    }
                                                }
                                                else
                                                {
                                                    dar["message"] = "From location not found";
                                                }
                                            }
                                            else
                                            {
                                                
                                            }

                                            dar["TOlocation"] = row[6].ToString();
                                            if (row[6].ToString() != "")
                                            {
                                                DataTable dt2 = new DataTable();
                                                dt2 = db.sub_GetDatatable("select top 1 LocationID from Ext_Location_M Where Location =  '" + row[6].ToString() + "'");
                                                if (dt2.Rows.Count > 0)
                                                {
                                                    foreach (DataRow row2 in dt2.Rows)
                                                    {
                                                        dar["TOlocationID"] = Convert.ToString(row2["LocationID"]);

                                                    }

                                                }
                                                else
                                                {
                                                    dar["message"] = "To location not found";
                                                }
                                            }
                                            dar["Vehicleno"] = row[2].ToString();

                                            if(fileData.checkGST == "27")
                                            {
                                                double dblCGST = 0;
                                                double dblSGST = 0;
                                                double Amount = 0;
                                                double SGSTcal = 0;
                                                double CGSTcal = 0;
                                                double dblIGST = 0;
                                                Amount = Convert.ToDouble(row[7].ToString());
                                                SGSTcal  = Convert.ToDouble(fileData.SGSTcal);
                                                CGSTcal = Convert.ToDouble(fileData.CGSTcal);
                                                dblCGST = Amount *SGSTcal / 100;
                                                dblSGST =  (Amount) * CGSTcal / 100;
                                                dar["dblCGST"] = dblCGST;
                                                dar["dblSGST"] = dblSGST;
                                            }
                                            else
                                            {
                                                double dblCGST = 0;
                                                double dblSGST = 0;
                                                double Amount = 0;
                                                double IGSTcal = 0;
                                                double CGSTcal = 0;
                                                double dblIGST = 0;
                                                Amount = Convert.ToDouble(row[7].ToString());
                                                IGSTcal = Convert.ToDouble(fileData.IGSTcal);
                                                dblIGST = Amount * IGSTcal / 100;

                                                dar["dblIGST"] = dblIGST;

                                            }
                                            
                                             
                                            dar["TransportID"] = fileData.TransportID;
                                            dar["CargoTypeID"] = fileData.CargoTypeID;

                                            dar["TransportType"] = fileData.TransportType;
                                            dar["CargoType"] = fileData.CargoType;

                                            VOucherTraifftDT.Rows.Add(dar);
                                            int linenum = dt.Rows.IndexOf(row);
                                            if (linenum == 1050)
                                            {
                                                int count1 = 0;
                                            }
                                        }
                                        else
                                        {
                                            return Json("Some columns are blank. Please check and re-import...");


                                        }
                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }

                                    if (VOucherTraifftDT != null)
                                    {

                                        var summaryDet = JsonConvert.SerializeObject(VOucherTraifftDT);
                                       
                                        var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;
                                         
                                    }


                                }
                            }
                        }
                    }
                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred. Error details: " + ex.Message);
                 
                    return Json("Error occurred. Error details: " + ex.Message);

                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

    }
}