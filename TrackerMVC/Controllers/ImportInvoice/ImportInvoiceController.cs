using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using II = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ImportInvoice;
using System.IO;
using System.Data;
using HC = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Data.OleDb;
using TrackerMVC.Filters;
using System.Data.SqlClient;
using System.Data.Objects.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using DB = TrackerMVCDataLayer.Helper;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;

namespace TrackerMVC.Controllers.ImportInvoice
{
    [UserAuthenticationFilter]

    public class ImportInvoiceController : Controller
    {
        // GET: ImportInvoice
        BM.LoadingPlan.LoadingPlan LP = new BM.LoadingPlan.LoadingPlan();
        DB.DBOperations db = new DB.DBOperations();
        II.ImportInvoice invDataProvider = new II.ImportInvoice();
        public ActionResult ImportProformaInvoice()
        {
            List<BE.ActivityMaster> Activitymaster = new List<BE.ActivityMaster>();
            Activitymaster = invDataProvider.Activitymaster();
            ViewBag.activitymaster = new SelectList(Activitymaster, "AutoID", "Activity");



            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = invDataProvider.getParty();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");


            return View();
        }
        public ActionResult CustomerProformaInvoice()
        {
            List<BE.ActivityMaster> Activitymaster = new List<BE.ActivityMaster>();
            Activitymaster = invDataProvider.Getactivitymasterdetails();
            ViewBag.activitymaster = new SelectList(Activitymaster, "Activity", "Activity");

            List<BE.ImportInvoiceType> Invoietype = new List<BE.ImportInvoiceType>();
            Invoietype = invDataProvider.Invoicetype();
            ViewBag.Invoicetype = new SelectList(Invoietype, "ID", "InvoiceType");



            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = invDataProvider.GetCustomer();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");


            List<BE.AccountProformaEntities> account = new List<BE.AccountProformaEntities>();
            account = invDataProvider.accountmasterdetails();
            ViewBag.TariffList = new SelectList(account, "ID", "Name");

            List<BE.CostCenterEntities> CostCenter = new List<BE.CostCenterEntities>();
            CostCenter = LP.getCostCenter();
            ViewBag.CostCenter = new SelectList(CostCenter, "Cost_ID", "Cost_Center");



            List<BE.Transport> Transporter = new List<BE.Transport>();
            Transporter = invDataProvider.GetTransportDetails();
            ViewBag.TransType = new SelectList(Transporter, "Transport_Type_ID", "Transport_Type");

            return View();
        }
        public ActionResult ImportProformaGSTSearch()
        {

            return PartialView();
        }
        public JsonResult GSTSearch(string SearchText)
        {
            List<BE.ImportProformaSearchGST> GstList = new List<BE.ImportProformaSearchGST>();
            GstList = invDataProvider.GetGSTList(SearchText);
            var jsonResult = Json(GstList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetProformaInvoicedetails(string Invoicetype, string Fromdate, string Todate, string Customers, string Activity, string ActivityStatus, string CostCenter, string Transid)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Getfueldataforproforma '" + Invoicetype + "','" + Fromdate + "','" + Todate + "','" + Customers + "','" + Activity + "','" + ActivityStatus + "','" + CostCenter + "','" + Transid + "'");
            Session["showadditionialcharges"] = dt;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult GetAdditionalchanrges(string workyear, string IgmN0, string ItemNo)
        {
            List<BE.ImportInvoiceContainerDetails> GstList = new List<BE.ImportInvoiceContainerDetails>();
            GstList = invDataProvider.GetAdditionalcharges(workyear, IgmN0, ItemNo);
            var jsonResult = Json(GstList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult Getsummaryaddtionalcharges(string IgmN0, string ItemNo)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_ShowAdditionalDetails '" + IgmN0 + "','" + ItemNo + "'");
            Session["showadditionialcharges"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult Saveadditionalchargesdetails(List<BE.ImportInvoiceContainerDetails> Invoicedata, string WorkorderNo, string Workorderyear,
            string accountheadID, string additionnarritioin, string AdditionalIGmno, string additionalItem)
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
            string message = invDataProvider.Saveadditionalcharges(dataTable, WorkorderNo, Workorderyear, accountheadID, additionnarritioin, AdditionalIGmno, additionalItem, Userid);
            return Json(message);
        }

        public JsonResult Canceladditionalcharges(string WONO)
        {
            string message = "";
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_Canceladditionaldetails " + Userid + ",'" + WONO + "'");
            return Json(message);
        }

        public JsonResult GetUpdateRmsDetails(string IgmN0, string ItemNo)
        {
            List<BE.ImportInvoiceContainerDetails> GstList = new List<BE.ImportInvoiceContainerDetails>();
            GstList = invDataProvider.GetUpdateRmsDetails(IgmN0, ItemNo);
            var jsonResult = Json(GstList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public ActionResult SaveUpdateRmsDetails(List<BE.ImportInvoiceContainerDetails> Invoicedata, string UpdateRmsIGmNo, string UpdateRmsItemno)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("jono");
            dataTable.Columns.Add("COntainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Containertype");
            dataTable.Columns.Add("Rms");
            foreach (BE.ImportInvoiceContainerDetails item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["jono"] = item.JONo;
                row["COntainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Containertype"] = item.Cargotype;
                row["Rms"] = item.RMS;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = invDataProvider.SaveUpdateRmsDetails(dataTable, UpdateRmsIGmNo, UpdateRmsItemno, Userid);
            return Json(message);

        }

        public JsonResult GetInvoiceporformadetails(string fromdate, string Todate, string searchCerteria, string Searchtext, string Searchtext1)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_ImportPerformaInvoiceList '" + fromdate + "','" + Todate + "','" + searchCerteria + "','" + Searchtext + "','" + Searchtext1 + "'");
            dt.Columns.Remove("SR No");

            string json = JsonConvert.SerializeObject(dt);
            dt.Columns.Remove("#");
            Session["ExportToExcelPerformaInvoice"] = dt;
            Session["fromdate"] = fromdate;
            Session["Todate"] = Todate;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelInvoicePorforma()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["ExportToExcelPerformaInvoice"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["Todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BL_Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>BL Details<strong></td></tr>");
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

        public JsonResult Getcargotypedetails(string Igmno, string Itemno)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_IGM_Fetch_Ammend '" + Igmno + "','" + Itemno + "'");

            List<BE.importcargotypeentitiescs> Customerlst = new List<BE.importcargotypeentitiescs>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.importcargotypeentitiescs customer = new BE.importcargotypeentitiescs();

                    customer.Consignee = Convert.ToString(row["Consignee"]);
                    customer.Con_IGMAddress = Convert.ToString(row["Con_IGMAddress"]);
                    customer.NConsignee = Convert.ToString(row["NConsignee"]);
                    customer.NCon_IGMAddress = Convert.ToString(row["NCon_IGMAddress"]);
                    customer.IGM_BLNo = Convert.ToString(row["IGM_BLNo"]);
                    customer.IGM_GoodsDesc = Convert.ToString(row["IGM_GoodsDesc"]);
                    customer.IGM_GrossWt = Convert.ToString(row["IGM_GrossWt"]);
                    customer.IGM_WtUnit = Convert.ToString(row["IGM_WtUnit"]);
                    customer.IGM_Qty = Convert.ToString(row["IGM_Qty"]);
                    customer.IGM_PackType = Convert.ToString(row["IGM_PackType"]);
                    customer.Remarks = Convert.ToString(row["Remarks"]);
                    Customerlst.Add(customer);
                }
            }
            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetContainerDetailsForCargoType(string Igmno, string Itemno)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_SHOWIGMDETAILS '" + Igmno + "','" + Itemno + "'");

            List<BE.ContainerCargotypeDetails> Customerlst = new List<BE.ContainerCargotypeDetails>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ContainerCargotypeDetails customer = new BE.ContainerCargotypeDetails();

                    customer.containerno = Convert.ToString(row["Container No"]);
                    customer.Size = Convert.ToInt16(row["Size"]);
                    customer.ISOCode = Convert.ToString(row["ISO Code"]);
                    customer.Cargotype = Convert.ToString(row["Cargo Type"]);
                    customer.Weight = Convert.ToString(row["Weight"]);
                    customer.JONo = Convert.ToString(row["Jo No"]);
                    customer.PKGS = Convert.ToString(row["PKGS"]);
                    Customerlst.Add(customer);
                }
            }
            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public ActionResult UpdateCarfoDetailsInvoiceProforma(string IGmNo, string ItemNo, string Consignee, string Con_IGMAddress,
            string NConsignee, string NCon_IGMAddres, string IGM_BLNo, string IGM_GoodsDesc, string IGM_GrossWt, string IGM_WtUnit,
            string IGM_Qty, string IGM_PackType, string Remarks)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = invDataProvider.UpdateCargoDetailsInvoiceProforma(IGmNo, ItemNo, Consignee, Con_IGMAddress, NConsignee, NCon_IGMAddres,
                IGM_BLNo, IGM_GoodsDesc, IGM_GrossWt, IGM_WtUnit, IGM_Qty, IGM_PackType, Remarks, userId);
            return Json(message);
        }

        [HttpPost]
        public ActionResult UpdateCargoDetailsEntry(List<BE.ContainerCargotypeDetails> Invoicedata, string IGmNo, string ItemNo
        )
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("JONo");
            dataTable.Columns.Add("containerno");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("ISOCode");
            dataTable.Columns.Add("Cargotype");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("PKGS");
            foreach (BE.ContainerCargotypeDetails item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["JONo"] = item.JONo;
                row["containerno"] = item.containerno;
                row["Size"] = item.Size;
                row["ISOCode"] = item.ISOCode;
                row["Cargotype"] = item.Cargotype;
                row["Weight"] = item.Weight;
                row["PKGS"] = item.PKGS;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            string message = invDataProvider.UpdateCargoDetails(dataTable, IGmNo, ItemNo, Userid);
            return Json(message);
        }

        public ActionResult PendingProformaForInvoice()
        {
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = invDataProvider.getParty();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
            return View();
        }

        public JsonResult GetPendingproformaDetails(string SearchCriteria, string Search, string Search1)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_ListOfPendingProformaInvoiceforFinalConfirmCus '" + SearchCriteria + "','" + Search + "','" + Search1 + "'");

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

        public ActionResult ExportToExcelPendingproformaDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["ListOfPendingProformaInvoiceforFinalConfirm"];


            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;


            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Pending Proforma For Invoice.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Pending Proforma For Invoice Report<strong></td></tr>");

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
        public ActionResult CancelinvoiceProforma(string AssessNo, string workyear)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = invDataProvider.CancelInvoicePorforma(AssessNo, workyear, userId);
            return Json(message);
        }

        [HttpPost]
        public ActionResult SubmitDetailsEntry(string AssessNo, string workyear, string transid, string AssessType)
        {
            string message = "";
            string strinvoiceNo = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = invDataProvider.GetTransportbillingPrefix(transid);

            if (message == "")
            {
                message = "Transporter code is not mapped. Cannot proceed!";
            }
            else
            {


                message = invDataProvider.SubmitFinalDetails(AssessNo, workyear, userId, transid, AssessType);
            }
            return Json(message);
        }

        public JsonResult GetPendingInvoiceToday(string fromdate, string Todate, string searchCerteria, string Searchtext, string Searchtext1)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_Party_AssessL_ist '" + fromdate + "','" + Todate + "','" + searchCerteria + "','" + Searchtext + "','" + Searchtext1 + "'");
            dt.Columns.Remove("Sr No");
            string json = JsonConvert.SerializeObject(dt);
            dt.Columns.Remove("Annexure");
            dt.Columns.Remove("Print");
            dt.Columns.Remove("Cancel");
            Session["importAssessListPending"] = dt;
            Session["fromdate"] = fromdate;
            Session["Todate"] = Todate;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public ActionResult ExportToExcelImportImportAssessListproforma()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["importAssessListPending"];


            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;


            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Import Assessment Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Import Assessment Details Report<strong></td></tr>");

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


        public ActionResult Checkalreadyproformagenerated(string IgmN0, string ItemNo, string AssessType, string MovementType)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";
            string strSQL = "";
            strSQL = "SELECT TOP 1 * FROM Import_ProformaM WHERE IGMNo='" + IgmN0 + "' AND ItemNO='" + ItemNo + "' AND Iscancel=0  and AssessType ='" + AssessType + "' order by assessdate desc ";
            dt = db.sub_GetDatatable(strSQL);
            if (dt.Rows.Count > 0)
            {
                Message = "Again specified document same invoice type already proforma generated. First u need to generate final invoice. Cannot proceed";
            }

            if (AssessType != "SSR")
            {
                if (MovementType == "NORMAL")
                {
                    strSQL = "USP_CHECK_RMS_EXAMIN '" + IgmN0 + "','" + ItemNo + "'";
                    dt = db.sub_GetDatatable(strSQL);
                    if (dt.Rows.Count > 0)
                    {
                        Message = "";
                    }
                    else
                    {
                        Message = "Again specified document custom examine is not updated. Cannot proceed";
                    }
                }

            }

            return Json(Message);
        }

        [HttpPost]
        public ActionResult CalculatePerformaDetails(string GSTNo, string GSTName, string StateCode, string TariffID, string Deliverytype, List<BE.ImportInvoiceContainerDetails> GetContainerDetails)
        {
            DataTable dataTable = new DataTable();
            DataTable dttemp = new DataTable();
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            string strSQL = "";
            dataTable.Columns.Add("VOucherno");
            dataTable.Columns.Add("Voucherdate");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("TrailerName");
            dataTable.Columns.Add("TransName");
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("Fromlocation");
            dataTable.Columns.Add("Tolocation");
            dataTable.Columns.Add("DriverName");
            dataTable.Columns.Add("ContainerType");
            dataTable.Columns.Add("Frlocid");
            dataTable.Columns.Add("ToLocid");
            dataTable.Columns.Add("lineID");


            foreach (BE.ImportInvoiceContainerDetails item in GetContainerDetails)
            {
                DataRow row = dataTable.NewRow();

                row["VOucherno"] = item.VOucherno;
                row["Voucherdate"] = item.Voucherdate;
                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["TrailerName"] = item.TrailerName;
                row["TransName"] = item.TransName;
                row["Activity"] = item.Activity;
                row["Fromlocation"] = item.Fromlocation;
                row["Tolocation"] = item.Tolocation;
                row["DriverName"] = item.DriverName;
                row["ContainerType"] = item.ContainerType;
                row["Frlocid"] = item.Frlocid;
                row["ToLocid"] = item.ToLocid;
                row["lineID"] = item.lineID;

                dataTable.Rows.Add(row);
            }
            double dblchargesdays = 0;

            int TxGroupID = 0;
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            var strSQL3 = "";
            double dblNetAmount_IND = 0;
            int dblSGSTax = 0;
            int dblCGSTax = 0;
            int dblIGSTax = 0;


            // Code For work year 

            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string FinYear = null;

            if (DateTime.Today.Month > 3)
            {
                FinYear = CurrentYear.ToString() + "-" + NextYear.ToString().Remove(0, 2);
            }
            else
            {
                FinYear = PreviousYear.ToString() + "-" + CurrentYear.ToString().Remove(0, 2);
            }
            string workyear = FinYear.Trim();

            // code end for work year





            string deletetemp = "";
            DataTable deletetempDL = new DataTable();
            deletetemp = " delete from temp_Party_assessD where  userid= '" + Userid + "' ";
            deletetempDL = db.sub_GetDatatable(deletetemp);
            DataTable dtp = new DataTable();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataTable DTRSfetch = new DataTable();

                strSQL3 = "";
                strSQL3 = "   USP_IMP_TARIFF_FETCH_ALL_NEW_Transport '" + TariffID + "','" + dataTable.Rows[i].Field<string>("Activity") + "' ,'" + dataTable.Rows[i].Field<string>("ContainerType") + "' ,'" + dataTable.Rows[i].Field<string>("Size") + "','" + dataTable.Rows[i].Field<string>("Frlocid") + "','" + dataTable.Rows[i].Field<string>("ToLocid") + "'";
                DTRSfetch = db.sub_GetDatatable(strSQL3);

                if (DTRSfetch.Rows.Count > 0)
                {
                    strSQL = "";
                    strSQL = "  select * from accountmaster WHERE accountid='" + Convert.ToInt64(DTRSfetch.Rows[0][0]) + "'";
                    dtp = db.sub_GetDatatable(strSQL);

                }



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
                    TxGroupID = Convert.ToInt16(dtget.Rows[0].Field<Int16>("settingsID"));
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


                if (DTRSfetch.Rows.Count > 0)
                {
                    for (int j = 0; j < DTRSfetch.Rows.Count; j++)
                    {
                        dblNetAmount_IND = 0;
                        DataTable GetAccoubtDL = new DataTable();
                        DataTable InsertDL = new DataTable();
                        DataTable GetInDcharges = new DataTable();
                        string Accountvalue = "";
                        Int64 Getasscountid = Convert.ToInt64(DTRSfetch.Rows[j].Field<string>("AccountID"));
                        Accountvalue = " SELECT DISTINCT AccountName from AccountMaster WHERE AccountID= '" + Getasscountid + "'";
                        GetAccoubtDL = db.sub_GetDatatable(Accountvalue);

                        if (GetAccoubtDL.Rows.Count > 0)
                        {
                            GetInDcharges = db.sub_GetDatatable("GetTariffConbinationDet_Charges_for_customer '" + dataTable.Rows[i].Field<string>("Activity") + "','" + TariffID + "'," +
                                                           "'" + dataTable.Rows[i].Field<string>("Size") + "','" + DTRSfetch.Rows[j].Field<string>("AccountID") + "','" + dataTable.Rows[i].Field<string>("ContainerType") + "','" + dataTable.Rows[i].Field<string>("Frlocid") + "','" + dataTable.Rows[i].Field<string>("ToLocid") + "','" + dataTable.Rows[i].Field<string>("DriverName") + "'");


                            if (GetInDcharges.Rows.Count > 0)
                            {
                                dblNetAmount_IND = Convert.ToDouble(GetInDcharges.Rows[0][0]);
                            }
                        }

                        double AccountID = Convert.ToDouble(DTRSfetch.Rows[j].Field<string>("AccountID"));
                        string strSQL7 = "";
                        DataTable getactivityDL = new DataTable();
                        strSQL7 = " select autoid from ActivityMaster where activity='" + dataTable.Rows[i].Field<string>("Activity") + "' ";
                        getactivityDL = db.sub_GetDatatable(strSQL7);

                        int activityid = 0;
                        if (getactivityDL.Rows.Count > 0)
                        {
                            activityid = Convert.ToInt32(getactivityDL.Rows[0][0]);
                        }






                        if (dblNetAmount_IND > 0)
                        {
                            string strsql5 = "";
                            strsql5 = "";
                            strsql5 = " Exec USP_INSERT_TEMP_PARTY_ASSESSD 0,'" + workyear + "','" + 1 + "','" + dataTable.Rows[i].Field<string>("ContainerNo") + "',";
                            strsql5 += " '" + DTRSfetch.Rows[j].Field<string>("AccountID") + "','" + dblNetAmount_IND + "',";
                            strsql5 += " '" + 0 + "'," + 1 + ",'" + TxGroupID + "'," + (Convert.ToDecimal(dblNetAmount_IND) * Convert.ToDecimal(dblSGSTax)) / 100 + ",'" + (Convert.ToDecimal(dblNetAmount_IND) * Convert.ToDecimal(dblIGSTax)) / 100 + "',";
                            strsql5 += " '" + (Convert.ToDecimal(dblNetAmount_IND) * Convert.ToDecimal(dblCGSTax)) / 100 + "','" + dataTable.Rows[i].Field<string>("Size") + "','" + dblNetAmount_IND + "',";
                            strsql5 += " '" + TariffID + "'," + dataTable.Rows[i].Field<string>("Frlocid") + ",";
                            strsql5 += " '" + dataTable.Rows[i].Field<string>("ToLocid") + "','" + activityid + "','" + dataTable.Rows[i].Field<string>("TrailerName") + "','" + dataTable.Rows[i].Field<string>("VOucherno") + "'," + Userid + ",'" + dataTable.Rows[i].Field<string>("lineID") + "'";
                            InsertDL = db.sub_GetDatatable(strsql5);


                        }
                    }
                }
            }




            string strSQL8 = "";
            DataTable getactivityDL1 = new DataTable();
            strSQL8 = " select autoid from ActivityMaster where activity='" + dataTable.Rows[0].Field<string>("Activity") + "' ";
            getactivityDL1 = db.sub_GetDatatable(strSQL8);

            int activityid1 = 0;
            if (getactivityDL1.Rows.Count > 0)
            {
                activityid1 = Convert.ToInt32(getactivityDL1.Rows[0][0]);
            }
            string mes1 = "";


            // Code For work year 



            if (DateTime.Today.Month > 3)
            {
                FinYear = CurrentYear.ToString() + "-" + NextYear.ToString().Remove(0, 2);
            }
            else
            {
                FinYear = PreviousYear.ToString() + "-" + CurrentYear.ToString().Remove(0, 2);
            }
            string Workyear1 = FinYear.Trim();

            // code end for work year


            List<BE.ContainerAmountSHow> GetContainerAmt = new List<BE.ContainerAmountSHow>();
            dtp.Clear();
            strSQL = "";
            strSQL = " Exec sp_ContainerShow_for_customer '" + TariffID + "','" + mes1 + "'," + Userid + "";
            dtp = db.sub_GetDatatable(strSQL);
            //if (dtp.Rows.Count > 0)
            //{
            //    //foreach(DataRow row in dtp.Rows)
            //    //{
            //    //    BE.ContainerAmountSHow GetList = new BE.ContainerAmountSHow();
            //    //    GetList.AccountName = Convert.ToString(row["AccountName"]);
            //    //    GetList.ContainerNo = Convert.ToString(row["Container No"]);
            //    //    GetList.NetAmount = Convert.ToString(row["Net Amount"]);
            //    //    GetContainerAmt.Add(GetList);

            //    //}

            //}
            string json = JsonConvert.SerializeObject(dtp);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            BE.TextBoXValuesForImportPerforma GetContainerAmtdetails = new BE.TextBoXValuesForImportPerforma();
            strSQL = "";
            strSQL = "get_sum_charges_imports_TMT_for_customer '" + TariffID + "', '" + Workyear1 + "'," + Userid + "   ";
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



        public ActionResult SaveInvoicePerformaDetails(string GSTNo, string GSTName, string StateCode, string TariffID, string Deliverytype, string Fromdate,
          string Todate,
          string netamount, string SGST, string CGST, string IGST, string GrandTotal, string InvoiceDate, string Customers, string Transid)
        {

            Int64 intid = 0;
            string strinvoiceNo = "";
            Int64 txtassessno = 0;
            DataTable dtwo = new DataTable();
            double dbltaxcategoryid = 0;
            HC.DBOperations db = new HC.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            string strSQL1;
            string strSQL;
            // Code For work year 

            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string FinYear = null;

            if (DateTime.Today.Month > 3)
            {
                FinYear = CurrentYear.ToString() + "-" + NextYear.ToString().Remove(0, 2);
            }
            else
            {
                FinYear = PreviousYear.ToString() + "-" + CurrentYear.ToString().Remove(0, 2);
            }
            string strWorkYear = FinYear.Trim();

            // code end for work year
            string txtremarks = "";
            string strSGSTPer = "";
            string strCGSTPer = "";
            string strIGSTPer = "";

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            strSGSTPer = "SGST" + " @ " + 9 + "%";
            strCGSTPer = "CGST" + " @ " + 9 + "%";
            strIGSTPer = "IGST" + " @ " + 18 + "%";
            strSQL = "";
            strSQL = "SELECT isnull(MAX(Trans_proformano),0) as[Trans_proformano] FROM settings_assess WHERE Trans_proformaWY='" + strWorkYear + "'";
            DataTable dt1 = new DataTable();
            dt1 = db.sub_GetDatatable(strSQL);
            intid = Convert.ToInt64(dt1.Rows[0].Field<Int64>("Trans_proformano") + 1);
            txtassessno = Convert.ToInt64(dt1.Rows[0].Field<Int64>("Trans_proformano") + 1);
            string strinvyear;
            int allowcount = (int)(Math.Log10(intid) + 1);
            string str = strWorkYear;
            str = str.Remove(0, 5);


            if (allowcount == 1)
            {
                strinvoiceNo = "TP/NLT/" + "0000" + intid + "/" + str;
            }
            else if (allowcount == 2)
            {
                strinvoiceNo = "TP/NLT/" + "000" + intid + "/" + str;
            }
            else if (allowcount == 3)
            {
                strinvoiceNo = "TP/NLT/" + "00" + intid + "/" + str;
            }
            else if (allowcount == 4)
            {
                strinvoiceNo = "TP/NLT/" + "0" + intid + "/" + str;
            }
            else if (allowcount == 5)
            {
                strinvoiceNo = "TP/NLT/" + "" + intid + "/" + str;
            }

            string assesstype = "";
            string remarks = "";
            int discount = 0;
            string remarksw = "";
            strSQL1 = "";

            strSQL1 = " EXEC USP_INSERT_PARTY_PROFORMAM " + txtassessno + ",'" + strWorkYear + "', '" + strinvoiceNo + "', ";
            strSQL1 += " '" + Convert.ToDateTime(InvoiceDate).ToString("dd-MMM-yyyy HH:mm:ss") + "',";
            strSQL1 += " '" + Fromdate + "','" + Todate + "','" + TariffID + "','" + Deliverytype + "','" + remarks + "','" + netamount + "' ,  ";
            strSQL1 += " " + GrandTotal + "," + discount + ",'" + remarksw + "', '" + Userid + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy HH:mm:ss") + "', ";
            strSQL1 += "" + SGST + ", " + IGST + ", " + CGST + ",'" + GSTName + "',0," + Customers + ",0,'" + assesstype + "','0',0,0,'" + Transid + "'";

            dt1 = db.sub_GetDatatable(strSQL1);

            DataTable dt = new DataTable();
            strSQL = "";
            strSQL = "UPDATE settings_assess SET Trans_proformano=" + intid + " WHERE Trans_proformaWY='" + strWorkYear + "'";
            dt = db.sub_GetDatatable(strSQL);
            string strSQL8 = "";
            DataTable getactivityDL1 = new DataTable();
            strSQL8 = " select autoid from ActivityMaster where activity='" + Deliverytype + "' ";
            getactivityDL1 = db.sub_GetDatatable(strSQL8);

            int activityid1 = 0;
            if (getactivityDL1.Rows.Count > 0)
            {
                activityid1 = Convert.ToInt32(getactivityDL1.Rows[0][0]);
            }



            strSQL = "";
            strSQL = " Update temp_party_assessd set AssessNo=" + txtassessno + " where UserID=" + Userid + " and tariffid='" + TariffID + "'";
            dt = db.sub_GetDatatable(strSQL);

            strSQL = "";
            strSQL = " exec USP_Insert_party_PerformaD " + Userid + ",'" + TariffID + "'";
            dt = db.sub_GetDatatable(strSQL);


            string Messageget = "Record Saved Successfully!";
            string message = Messageget + ',' + strinvoiceNo;
            strSQL1 = "";
            return Json(message);

        }


        // code By Prashant


        public ActionResult ImportTariffSetting()
        {
            BE.ImportTraiffSettingEntities ImportTariffSettingList = new BE.ImportTraiffSettingEntities();
            ImportTariffSettingList = invDataProvider.ImporttrariffSettingDropDown();
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
            Location = invDataProvider.getLocationForall();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");


            List<BE.ContainerSize> Containersize = new List<BE.ContainerSize>();
            Containersize = invDataProvider.GetSizeDetails();
            ViewBag.ContainerSize = new SelectList(Containersize, "ContainerSizeID", "ContainerSizeName");

            List<BE.DeliveryTypeDetails> DeliveryType = new List<BE.DeliveryTypeDetails>();
            DeliveryType = invDataProvider.GetDeliveryDetails();
            ViewBag.DeliveryType = new SelectList(DeliveryType, "DeliveryID", "DeliveryType");


            List<BE.SlabDetails> SlabDetails = new List<BE.SlabDetails>();
            SlabDetails = invDataProvider.GetSlabDetails();
            ViewBag.SlabDetailsList = new SelectList(SlabDetails, "SlabId", "SlabId");




            List<BE.importtariffdetails> importtariffdetails = new List<BE.importtariffdetails>();
            importtariffdetails = invDataProvider.Getimporttariffdetails();
            ViewBag.importtariffdetails = new SelectList(importtariffdetails, "TariffID", "TariffDescription");


            List<BE.ContainerType> ContainerType = new List<BE.ContainerType>();
            ContainerType = invDataProvider.GetContainerType();
            ViewBag.ContainerType = new SelectList(ContainerType, "ContainerTypeID", "ContainerTypeName");



            List<BE.ImportAccountMaster> AccountHead = new List<BE.ImportAccountMaster>();
            AccountHead = invDataProvider.GetAccountHead();
            ViewBag.AccountHead = new SelectList(AccountHead, "AccountID", "AccountName");


            List<BE.ActivityMaster> Activitymaster = new List<BE.ActivityMaster>();
            Activitymaster = invDataProvider.Activitymaster();
            ViewBag.activitymaster = new SelectList(Activitymaster, "AutoID", "Activity");


            List<BE.DriverTypeEntities> Drivertype = new List<BE.DriverTypeEntities>();
            Drivertype = invDataProvider.Driver_type();
            ViewBag.DrivertypeList = new SelectList(Drivertype, "DriverID", "DriverType");
            return View();
        }



        [HttpPost]
        public ActionResult SaveSlabDetails(List<BE.SlabDetailsEntites> Invoicedata, string TariffID, List<BE.TariffAddDetailsEntites> DeliveryType1, string Accounting, string AccountingID)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("SlabOn");
            dataTable.Columns.Add("RangeFrom");
            dataTable.Columns.Add("RangeTo");
            dataTable.Columns.Add("Value");
            dataTable.Columns.Add("SlabSize");
            dataTable.Columns.Add("SlabCargoType");

            foreach (BE.SlabDetailsEntites item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["SlabOn"] = item.SlabOn;
                row["RangeFrom"] = item.RangeFrom;
                row["RangeTo"] = item.RangeTo;
                row["Value"] = item.Value;
                row["SlabSize"] = item.SlabSize;
                row["SlabCargoType"] = item.SlabCargoType;

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

            Getdetails = invDataProvider.SaveCargoDetails(dataTable, Userid, TariffID, dataTable1, Accounting, AccountingID);





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
                row1["Jotype"] = item.Jotype;
                row1["ScanType"] = item.ScanType;
                row1["AccountingID"] = item.AccountingID;
                row1["Days"] = item.Days;

                dataTableadd.Rows.Add(row1);
            }
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            Getdetails1 = invDataProvider.SavedataForGetdata(dataTableadd, userId);

            //data to end insert

            var jsonResult = Json(Getdetails1, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }



        public JsonResult AjxImportTariffSettingFile()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";

            List<BE.SlabDetailsEntites> SalbDetails = new List<BE.SlabDetailsEntites>();
            if (Request.Files.Count > 0)
            {
                try
                {

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

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
                        //BL.AddErrorLog(conString.Replace("'",""));
                        //BL.AddErrorLog(fname.Replace("'", ""));
                        //  conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                        //  BL.AddErrorLog("Connection close");
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

                                    DataTable DriverPaymentDT = new DataTable();


                                    DriverPaymentDT.Columns.Add("Slab On");
                                    DriverPaymentDT.Columns.Add("From");
                                    DriverPaymentDT.Columns.Add("To");
                                    DriverPaymentDT.Columns.Add("Value");
                                    DriverPaymentDT.Columns.Add("Size");
                                    DriverPaymentDT.Columns.Add("Cargo Type");

                                    //DriverPaymentDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DataRow dar = DriverPaymentDT.NewRow();

                                        dar["Slab On"] = row[0].ToString().ToUpper();
                                        dar["From"] = row[1].ToString().ToUpper();
                                        dar["To"] = row[2].ToString();
                                        dar["Value"] = row[3].ToString();
                                        dar["Size"] = row[4].ToString();
                                        dar["Cargo Type"] = row[5].ToString();

                                        DriverPaymentDT.Rows.Add(dar);
                                        int linenum = dt.Rows.IndexOf(row);
                                        if (linenum == 1050)
                                        {
                                            int count1 = 0;
                                        }


                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }

                                    if (DriverPaymentDT != null)
                                    {

                                        SalbDetails = invDataProvider.UpdateDriverPaymentDetails(DriverPaymentDT, Userid);


                                        var jsonResult = Json(SalbDetails, JsonRequestBehavior.AllowGet);
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

                    return Json(1);

                }
            }
            else
            {
                return Json("No files selected.");
            }
        }


        [HttpPost]
        public JsonResult TariffValidation(List<BE.SlabDetailsEntites> Invoicedata)
        {
            string message = "";
            string message1 = "";

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("SlabOn");
            dataTable.Columns.Add("RangeFrom");
            dataTable.Columns.Add("RangeTo");
            dataTable.Columns.Add("Value");
            dataTable.Columns.Add("SlabSize");
            dataTable.Columns.Add("SlabCargoType");

            foreach (BE.SlabDetailsEntites item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["SlabOn"] = item.SlabOn;
                row["RangeFrom"] = item.RangeFrom;
                row["RangeTo"] = item.RangeTo;
                row["Value"] = item.Value;
                row["SlabSize"] = item.SlabSize;
                row["SlabCargoType"] = item.SlabCargoType;

                dataTable.Rows.Add(row);
            }


            message = invDataProvider.ImportValidation(dataTable);
            if (message != "")
            {

                message1 += message;
            }
            else
            {
                message1 = "New";
            }
            return new JsonResult() { Data = message1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }


        public JsonResult GetTariffDetailsForSearch(string SearchType)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Search_IMP_Tariff_new '" + SearchType + "'");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult AjaxGetTariffDetailsForaddtable(string TariffID, string From, string TO, string Accounting
          , string Size, string ChargesBased, string FixedAmt, string Days, string rate
          , string Slabid, string ScanType, string Location, string Jotype, string Commodity, string TransType,
          string Port, string Insurance, string AccountingID, List<BE.TariffAddDetailsEntites> DeliveryType1, List<BE.TariffAddDetailsEntites> Type1, string TaxID, string InvoiceType,
          string fromlocation, string tolocation, string Drivertype)
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
            Getdetails = invDataProvider.GetAddTabledetails(TariffID, From, TO, Accounting, Size, ChargesBased, FixedAmt, Days, rate, Slabid, ScanType, Location,
            Jotype, Commodity, TransType, Port, Insurance, AccountingID, dataTable, dataTable1, TaxID, InvoiceType, fromlocation, tolocation, Drivertype);




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
            dataTableadd.Columns.Add("Jotype");
            dataTableadd.Columns.Add("ScanType");
            dataTableadd.Columns.Add("AccountingID");
            dataTableadd.Columns.Add("Days");
            dataTableadd.Columns.Add("Tolocation");
            dataTableadd.Columns.Add("Drivertype");


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
                row1["Jotype"] = item.Jotype;
                row1["ScanType"] = item.ScanType;
                row1["AccountingID"] = item.AccountingID;
                row1["Tolocation"] = item.Tolocation;
                row1["Days"] = item.Days;
                row1["Drivertype"] = item.Drivertype;

                dataTableadd.Rows.Add(row1);
            }
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            Getdetails1 = invDataProvider.SavedataForGetdata(dataTableadd, userId);

            // data to end insert

            var jsonResult = Json(Getdetails1, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        //Save the Import Tariff Setting 


        [HttpPost]
        public ActionResult SaveImportTariffSettingDetails(List<BE.TariffAddDetailsEntites> ImportData, string commodity)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("TariffID");
            dataTable.Columns.Add("DeliveryType");
            dataTable.Columns.Add("From");
            dataTable.Columns.Add("TO");
            dataTable.Columns.Add("Accounting");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Type");

            dataTable.Columns.Add("Insurance");
            dataTable.Columns.Add("FixedAmt");
            dataTable.Columns.Add("rate");
            dataTable.Columns.Add("Tax");


            dataTable.Columns.Add("Freedays");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("Tolocation");
            dataTable.Columns.Add("Drivertype");



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
                row["Insurance"] = item.Insurance;
                row["FixedAmt"] = item.FixedAmt;
                row["rate"] = item.rate;
                row["Tax"] = item.Tax;
                row["Freedays"] = item.Freedays;
                row["Location"] = item.Location;
                row["Tolocation"] = item.Tolocation;
                row["Drivertype"] = item.Drivertype;

                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            //string message = invDataProvider.CheckEffective(dataTable, Userid);
            string message = "";

            if (message == "")
            {
                message = invDataProvider.SaveImportSettingTariff(dataTable, Userid, commodity);
            }
            else
            {

            }
            return Json(message);

        }


        public ActionResult CancelImportTariffSetting(string TariffID, string ActivityMaster, string Containertype, string AccountHead)
        {
            List<BE.TariffAddDetailsEntites> CancelDetails = new List<BE.TariffAddDetailsEntites>();
            CancelDetails = invDataProvider.GetCancelTariffDetails(TariffID, ActivityMaster, Containertype, AccountHead);

            var jsonResult = Json(CancelDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public ActionResult CancelDetailsForTariff(List<BE.TariffAddDetailsEntites> TariffNo)
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
            string message = invDataProvider.CancelDetailsTariff(dataTable, Userid);
            return Json(message);

        }

        [HttpPost]
        public ActionResult ApproveDetailsForTariff(List<BE.TariffAddDetailsEntites> TariffNo)
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
            string message = invDataProvider.ApproveDetailsTariff(dataTable, Userid);
            return Json(message);

        }

        public ActionResult ExportToFormatImport_Slab()
        {
            string fileName = "~/Format/Import_Slab.xlsx";
            if (fileName != "")
            {
                string filePath = fileName;
                Response.ContentType = "doc/docx";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }

            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("ContainerNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("Size", typeof(string)));
            //dt.Columns.Add(new DataColumn("Commodity", typeof(string)));
            //dt.Columns.Add(new DataColumn("Cargo Type", typeof(string)));
            //dt.Columns.Add(new DataColumn("ISOCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("FL", typeof(string)));
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            ////dt.Columns.Add(new DataColumn("FL", typeof(string)));
            //GridView gridview = new GridView();
            //gridview.DataSource = dt;
            //gridview.DataBind();

            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment;filename=ExcelUploadFormat.xls");
            //using (StringWriter sw = new StringWriter())
            //{
            //    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            //    {
            //        // render the GridView to the HtmlTextWriter


            //        gridview.RenderControl(htw);
            //        // Output the GridView content saved into StringWriter
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }
            //}

            return View();
        }

        public JsonResult ImportTariffSettingDetailsForUserdelete()
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_DeleteTempDetails '" + Userid + "'");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult ImportTariffSettingDetailsForUserWisedelete(int Entryid)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_DeleteTempDetailsentrywise '" + Entryid + "'," + Userid + "");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public ActionResult ImportInvoicePerformaPrint(string InvoiceNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblHeadDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblBankDetails = new DataTable();
            DataTable tblWordAmount = new DataTable();
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

            getJobOrderSet = db.sub_GetDataSets("getInvoicePerformaPrintMVC '" + InvoiceNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblHeadDetails = getJobOrderSet.Tables[1];
                tblContainerDetails = getJobOrderSet.Tables[2];
                tblWordAmount = getJobOrderSet.Tables[3];


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

                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.InvoiceDate = dr["AssessDate"];
                    ViewBag.Name = dr["GSTName"];
                    ViewBag.Address = dr["GSTAddress"];
                    ViewBag.GSTIN = dr["GSTIn_uniqID"];
                    ViewBag.PlaceofSupply = dr["State"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.BankName = dr["BankName"];
                    ViewBag.AccountNo = dr["AccountNo"];
                    ViewBag.IFSCCode = dr["IFSCCode"];
                    ViewBag.Remarks = dr["Remarks"];
                    ViewBag.BranchName = dr["BranchName"];
                    ViewBag.fromDate = dr["fromdate"];
                    ViewBag.Todate = dr["todate"];

                }



                ViewBag.TotalAmountInWord = tblWordAmount.Rows[0]["TotalAmountInWords"];
                ViewBag.TotalAmount = tblWordAmount.Rows[0]["GrandTotal"];
                ViewBag.AmountWithoutTax = tblWordAmount.Rows[0]["NetTotal"];
                ViewBag.SGSTAmount = tblWordAmount.Rows[0]["SGST"];
                ViewBag.CGSTAmount = tblWordAmount.Rows[0]["CGST"];
                ViewBag.IGSTAmount = tblWordAmount.Rows[0]["IGST"];
                ViewBag.TaxAmount = Convert.ToDouble(Convert.ToDouble(tblWordAmount.Rows[0]["SGST"]) + Convert.ToDouble(tblWordAmount.Rows[0]["CGST"]) + Convert.ToDouble(tblWordAmount.Rows[0]["IGST"]));

            }

            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();
            ViewBag.HeadDetailsList = tblHeadDetails.AsEnumerable();
            //ViewBag.ChargesDetailsList = tblChargesDetails.AsEnumerable();



            foreach (DataRow data in tblHeadDetails.Rows)
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



        [HttpPost]
        public ActionResult CancelAssessmentDetails(string remarks, string Assessno, string WorkYear)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = invDataProvider.CancelAssessmentDetails(remarks, Assessno, WorkYear, userId);
            return Json(message);
        }
        [HttpPost]
        public ActionResult ImportInvoicePrint(string InvoiceNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblHeadDetails = new DataTable();
            DataTable tblChargesDetails = new DataTable();
            DataTable tblWordAmount = new DataTable();
            DataTable tblBankDetails = new DataTable();
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
            double TotoalamountForContaniner = 0;

            getJobOrderSet = db.sub_GetDataSets("getInvoicePrintMVC '" + InvoiceNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblHeadDetails = getJobOrderSet.Tables[1];
                tblContainerDetails = getJobOrderSet.Tables[2];
                tblWordAmount = getJobOrderSet.Tables[3];


                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo1"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.CINNo = dr["CINNo"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.Addressl = dr["AddressI"];
                    ViewBag.IMP_AC_EMAILID = dr["IMP_AC_EMAILID"];
                    ViewBag.NoteVS = dr["NoteVI"];

                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.InvoiceDate = dr["AssessDate"];
                    ViewBag.Name = dr["GSTName"];
                    ViewBag.Address = dr["GSTAddress"];
                    ViewBag.GSTIN = dr["GSTIn_uniqID"];
                    ViewBag.PlaceofSupply = dr["State"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.BankName = dr["BankName"];
                    ViewBag.AccountNo = dr["AccountNo"];
                    ViewBag.IFSCCode = dr["IFSCCode"];
                    ViewBag.Remarks = dr["Remarks"];
                    ViewBag.BranchName = dr["BranchName"];
                    ViewBag.fromDate = dr["fromdate"];
                    ViewBag.Todate = dr["todate"];

                }



                ViewBag.TotalAmountInWord = tblWordAmount.Rows[0]["TotalAmountInWords"];
                ViewBag.TotalAmount = tblWordAmount.Rows[0]["GrandTotal"];
                ViewBag.AmountWithoutTax = tblWordAmount.Rows[0]["NetTotal"];
                ViewBag.SGSTAmount = tblWordAmount.Rows[0]["SGST"];
                ViewBag.CGSTAmount = tblWordAmount.Rows[0]["CGST"];
                ViewBag.IGSTAmount = tblWordAmount.Rows[0]["IGST"];
                ViewBag.TaxAmount = Convert.ToDouble(Convert.ToDouble(tblWordAmount.Rows[0]["SGST"]) + Convert.ToDouble(tblWordAmount.Rows[0]["CGST"]) + Convert.ToDouble(tblWordAmount.Rows[0]["IGST"]));

            }

            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();
            ViewBag.HeadDetailsList = tblHeadDetails.AsEnumerable();
            //ViewBag.ChargesDetailsList = tblChargesDetails.AsEnumerable();



            foreach (DataRow data in tblHeadDetails.Rows)
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

            foreach (DataRow data in tblContainerDetails.Rows)
            {
                TotoalamountForContaniner = Amount + SGST + CGST + IGST;

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




            ViewBag.TotoalamountForContaniner = TotoalamountForContaniner;

            return PartialView();

        }

        public JsonResult GetInvoiceTyprForSlabDetails(int AccountID)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("USP_ShowAccountDetails_new '" + AccountID + "'");
            string message = "";
            string message1 = "";
            message = Convert.ToString(dt.Rows[0].Field<int>("InvoiceTypeID"));
            message1 = Convert.ToString(dt.Rows[0].Field<int>("TaxID"));

            string Getdetails = " " + message + " ," + message1 + "";


            var jsonResult = Json(Getdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetFreeDays(int TariffID)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("select freeday from import_tariffmaster where tariffid =  '" + TariffID + "'");
            int message = 0;
            if (dt.Rows.Count > 0)
            {
                message = dt.Rows[0].Field<int>("freeday");
            }

            return Json(message);
        }

        public ActionResult ImportTariffMaster()
        {





            List<BE.ShipLines> ShippingLine = new List<BE.ShipLines>();
            ShippingLine = invDataProvider.getShipLines();
            ViewBag.ShippingLineList = new SelectList(ShippingLine, "SLID", "SLName");
            List<BE.CHA> CHA = new List<BE.CHA>();
            CHA = invDataProvider.getCHA();
            ViewBag.CHA = new SelectList(CHA, "CHANo", "CHAName");

            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = invDataProvider.GetCustomer();
            ViewBag.CustomerList = new SelectList(Customer, "AGID", "AGName");

            List<BE.Consignee> Consignee = new List<BE.Consignee>();
            Consignee = invDataProvider.GetImporter();
            ViewBag.Consignee = new SelectList(Consignee, "ImporterID", "ImporterName");

            List<BE.TariffGroup> TariffGroup = new List<BE.TariffGroup>();
            TariffGroup = invDataProvider.GettaiffGroup();
            ViewBag.TariffGroup = new SelectList(TariffGroup, "Group_ID", "Group_Name");


            List<BE.importtariffdetails> importtariffdetails = new List<BE.importtariffdetails>();
            importtariffdetails = invDataProvider.Getimporttariffdetails();
            ViewBag.importtariffdetails = new SelectList(importtariffdetails, "TariffID", "TariffDescription");


            List<BE.ContainerType> ContainerType = new List<BE.ContainerType>();
            ContainerType = invDataProvider.GetContainerType();
            ViewBag.ContainerType = new SelectList(ContainerType, "ContainerTypeID", "ContainerTypeName");



            List<BE.ImportAccountMaster> AccountHead = new List<BE.ImportAccountMaster>();
            AccountHead = invDataProvider.GetAccountHead();
            ViewBag.AccountHead = new SelectList(AccountHead, "AccountID", "AccountName");

            return View();
        }


        public ActionResult GetImportTariffSettingSummary()
        {
            DataTable dtscList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dtscList = db.sub_GetDatatable("Sp_tariffDetails_New");
            Session["ImportTariffMasterDetails"] = dtscList;
            string json = JsonConvert.SerializeObject(dtscList);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult CheckTariffMasterAlready(string TariffDescription)
        {
            string message = "";

            //HC.DBOperation object = new HC.DBOperations(); From Helper
            DataTable SvaDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            SvaDT = db.sub_GetDatatable("SP_SaveTariffMaster_Check_EXISTS '" + TariffDescription + "'");

            if (SvaDT.Rows.Count > 0)
            {
                message = Convert.ToString(SvaDT.Rows[0]["Message"]);
            }

            return Json(message);
        }

        public ActionResult SaveImportTariff(string TariffDescription, string EffectiveFrom, string EffectiveUpto, int ToShippingLine,
          int ToCHA, int Rebate20, int Rebate40,
                 int RebatePercentage, int RebateOther20, float RebateOther40, int RebateOtherPercentage,
                 string discPercent, int LineID, int CHAId, int CustomerID, int ImporterID, int IsActive,
                 int isDPD, int IsPreferred, int Port, int Jotype, int FreeDay, int Tariff_Group_ID, string TariffID)

        {
            string message = "";

            if (TariffID == "")
            {
                TariffID = "0";
            }
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            dt = db.sub_GetDatatable("SP_SaveTariffmaster_new_for_customer  '" + TariffID + "','" + TariffDescription + "','" + Convert.ToDateTime(EffectiveFrom).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(EffectiveUpto).ToString("yyyyMMdd") + "','" +
                ToShippingLine + "','" + ToCHA + "','" + Rebate20 + "','" + Rebate40 + "','" + RebatePercentage + "','" + RebateOther20 + "','" + RebateOther40 + "','" + RebateOtherPercentage + "','" + discPercent + "','" + LineID + "','" + CHAId + "','" + CustomerID + "','" + ImporterID + "'," + IsActive + ",'" + Convert.ToDateTime(DateTime.Now).ToString("dd MMM yyy HH:mm") + "'," + Userid + "," + isDPD + ",'" + Port + "','" + Jotype + "'," + FreeDay + "," + Tariff_Group_ID + "");
            return Json(message);
        }


        public ActionResult AccountMaster()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<BE.ExpHeadMasterEnt> InvoiceType = new List<BE.ExpHeadMasterEnt>();
            InvoiceType = invDataProvider.InvoiceTypeDDL();
            ViewBag.InvoiceDDL = new SelectList(InvoiceType, "InvTId", "InvType");

            List<BE.ExpHeadMasterEnt> HSNSelect = new List<BE.ExpHeadMasterEnt>();
            HSNSelect = invDataProvider.HSNCodeDDL();
            ViewBag.HSNDDList = new SelectList(HSNSelect, "HSNID", "HSNCodeL");

            List<BE.ExpHeadMasterEnt> TaxName = new List<BE.ExpHeadMasterEnt>();
            TaxName = invDataProvider.getTaxName();
            ViewBag.TaxName = new SelectList(TaxName, "TaxID", "TaxName");

            List<BE.ExpHeadMasterEnt> IMPGroup = new List<BE.ExpHeadMasterEnt>();
            IMPGroup = invDataProvider.IMPGroupDDl();
            ViewBag.importg = new SelectList(IMPGroup, "IMPGID", "IMPGName");
            return View();

        }

        public JsonResult GetImportAcList(string search)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_SearchAccountDetails_web'" + search + "'");

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
        public JsonResult ChecktheAccountmasterAlready(BE.ExpHeadMasterEnt ExpHeadMasterEnt)
        {
            string message = "";
            var EntryDate = ExpHeadMasterEnt.EntryDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            DataTable SvaDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            SvaDT = db.sub_GetDatatable("SP_SaveAccountMaster_Check_EXISTS '" + ExpHeadMasterEnt.EntryID + "','" + ExpHeadMasterEnt.AcName + "','" + ExpHeadMasterEnt.TallyName + "','" + ExpHeadMasterEnt.disp + "','" + ExpHeadMasterEnt.IsActive + "','" + ExpHeadMasterEnt.IMPGID + "','" + ExpHeadMasterEnt.HSNCodeL + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + ExpHeadMasterEnt.isdpd + "','" + ExpHeadMasterEnt.InvTId + "','" + ExpHeadMasterEnt.TaxID + "','" + ExpHeadMasterEnt.chargefors + "'");

            if (SvaDT.Rows.Count > 0)
            {
                message = Convert.ToString(SvaDT.Rows[0]["Message"]);
            }

            return Json(message);
        }

        public JsonResult SaveIAM(BE.ExpHeadMasterEnt ExpHeadMasterEnt)
        {
            string message = "";
            var EntryDate = ExpHeadMasterEnt.EntryDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            DataTable SvaDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            SvaDT = db.sub_GetDatatable("SP_SaveAccountMaster_New '" + ExpHeadMasterEnt.EntryID + "','" + ExpHeadMasterEnt.AcName + "','" + ExpHeadMasterEnt.TallyName + "','" + ExpHeadMasterEnt.disp + "','" + ExpHeadMasterEnt.IsActive + "','" + ExpHeadMasterEnt.IMPGID + "','" + ExpHeadMasterEnt.HSNCodeL + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + ExpHeadMasterEnt.isdpd + "','" + ExpHeadMasterEnt.InvTId + "','" + ExpHeadMasterEnt.TaxID + "','" + ExpHeadMasterEnt.chargefors + "'");

            if (SvaDT.Rows.Count > 0)
            {
                message = Convert.ToString(SvaDT.Rows[0]["Message"]);
            }

            return Json(message);
        }

        public ActionResult ExportToExcelImportAccountMaster()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["SearchAccountDetails"];


            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;


            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Import_Account_Master_Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Import Account Master Reportt<strong></td></tr>");

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


        public ActionResult GetCompanyNameTariff(string CustomerName)
        {
            List<BE.ImportProformaSearchGST> GstList = new List<BE.ImportProformaSearchGST>();
            GstList = invDataProvider.GetCustomertariffDetails(CustomerName);
            var jsonResult = Json(GstList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ViewTaiffDetails()
        {
            List<BE.importtariffdetails> importtariffdetails = new List<BE.importtariffdetails>();
            importtariffdetails = invDataProvider.Getimporttariffdetails();
            ViewBag.importtariffdetails = new SelectList(importtariffdetails, "TariffID", "TariffDescription");


            List<BE.ContainerType> ContainerType = new List<BE.ContainerType>();
            ContainerType = invDataProvider.GetContainerType();
            ViewBag.ContainerType = new SelectList(ContainerType, "ContainerTypeID", "ContainerTypeName");



            List<BE.ImportAccountMaster> AccountHead = new List<BE.ImportAccountMaster>();
            AccountHead = invDataProvider.GetAccountHead();
            ViewBag.AccountHead = new SelectList(AccountHead, "AccountID", "AccountName");


            List<BE.ActivityMaster> Activitymaster = new List<BE.ActivityMaster>();
            Activitymaster = invDataProvider.Activitymaster();
            ViewBag.activitymaster = new SelectList(Activitymaster, "AutoID", "Activity");

            return View();
        }

        [HttpPost]
        public ActionResult AjaxGetTariffDetails(string TariffID, string ActivityMaster, string Containertype, string AccountHead)
        {
            DataTable dt = new DataTable();
            var sessionTime = Session.Timeout;
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_View_TariffDetails  '" + TariffID + "','" + ActivityMaster + "','" + Containertype + "','" + AccountHead + "'");
            Session["Viewtariffdetails"] = dt;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelViewTariffDetailst()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["Viewtariffdetails"];
            //DataTable dt = (DataTable)ViewData["VoucherDetails"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=View_Tariff_Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>View Tariff Details<strong></td></tr>");
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


        public ActionResult ExporttoExcelAnnexure(string InvoiceNo)
        {
            DataTable LoadingExcel = new DataTable();

            DataSet TrainEXPORTList = new DataSet();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TrainEXPORTList = db.sub_GetDataSets("getTransportInvoicePrintMVC '" + InvoiceNo + "'");


            var InvoiceNo1 = Convert.ToString(TrainEXPORTList.Tables[0].Rows[0]["invoiceno"]);
            var InvoiceDate = Convert.ToString(TrainEXPORTList.Tables[0].Rows[0]["assessDate"]);

            string Headtitile = "Tax Invoice Annexure";
            string InvoiceNO2 = "Invoice No :" + InvoiceNo1 + "";
            string InvoiceDate2 = "Invoice Date :" + InvoiceDate + "";
            string Fromdate = "From Date :" + Session["fromdate"] + "";
            string ToDate = "To Date :" + Session["Todate"] + "";

            GridView gridview = new GridView();
            TrainEXPORTList.Tables[2].Columns.Remove("vddate");
            TrainEXPORTList.Tables[2].Columns.Remove("TripID");
            TrainEXPORTList.Tables[2].Columns.Remove("type");

            gridview.DataSource = TrainEXPORTList.Tables[2];
            gridview.DataBind();


            gridview.HeaderRow.BackColor = System.Drawing.Color.Yellow;
            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Controls.Add(gridview);

            tr1.Cells.Add(cell1);
            tb.Rows.Add(tr1);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Transport assessment details.xls");

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr style='height=20px;border:0.05em solid;background-color:yellow'><td colspan ='" + 10 + "'><h1 style='text-align:center'>" + Headtitile + " </h1></td></tr>");
                    htw.Write("<table><tr style='height=10px;border:0.05em solid;background-color:yellow'><th colspan ='" + 5 + "'><h1 style='text-align:center'>" + InvoiceNO2 + " </h1></th>" +
                                          "<td colspan ='" + 5 + "'><h1 style='text-align:center'>" + InvoiceDate2 + " </h1></td></tr>");
                    htw.Write("<table><tr style='height=10px;border:0.05em solid;background-color:yellow'><th colspan ='" + 5 + "'><h1 style='text-align:center'>" + Fromdate + " </h1></th>" +
                                        "<td colspan ='" + 5 + "'><h1 style='text-align:center'>" + ToDate + " </h1></td></tr>");
                    tb.RenderControl(htw); htw.Write("<table><tr style='height=5px;border:0.05em solid'><td colspan='" + 10 + "'><h6 style='text-align:right'> *output generated by tracker </h6></td></tr>");
                    tb.RenderControl(htw);

                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        [HttpPost]
        public JsonResult GetStatusWiseactivity(string ActivityStatus)
        {


            List<BE.ActivityMaster> Trailerno = new List<BE.ActivityMaster>();
            Trailerno = invDataProvider.getStatusWiseactivity(ActivityStatus);
            return new JsonResult() { Data = Trailerno, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }


        public ActionResult ExporttoExcelBillrecoAnnexure(string InvoiceNo)
        {
            DataTable LoadingExcel = new DataTable();

            DataSet TrainEXPORTList = new DataSet();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TrainEXPORTList = db.sub_GetDataSets("getInvoicePerformaBillrecoExcelMVC '" + InvoiceNo + "'");
            DataTable CompanyMaster = new DataTable();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";


            var InvoiceNo1 = Convert.ToString(TrainEXPORTList.Tables[0].Rows[0]["invoiceno"]);
            var InvoiceDate = Convert.ToString(TrainEXPORTList.Tables[0].Rows[0]["assessDate"]);
            var Fromdate1 = Convert.ToString(TrainEXPORTList.Tables[0].Rows[0]["fromdate"]);
            var Todate1 = Convert.ToString(TrainEXPORTList.Tables[0].Rows[0]["todate"]);

            string Headtitile = "Proforma Invoice Annexure";
            string InvoiceNO2 = "Invoice No :" + InvoiceNo1 + "";
            string InvoiceDate2 = "Invoice Date :" + InvoiceDate + "";
            string Fromdate = "From Date :" + Fromdate1 + "";
            string ToDate = "To Date :" + Todate1 + "";

            GridView gridview = new GridView();

            TrainEXPORTList.Tables[2].Columns.Remove("Voucher date");
            gridview.DataSource = TrainEXPORTList.Tables[2];
            gridview.DataBind();



            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Controls.Add(gridview);

            tr1.Cells.Add(cell1);
            tb.Rows.Add(tr1);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=proforma_Annexure_List For Bill Reco.xls");

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr style='height=20px;border:0.05em solid;background-color:yellow'><td colspan ='" + 8 + "'><h1 style='text-align:center'>" + Headtitile + " </h1></td></tr>");
                    htw.Write("<table><tr style='height=10px;border:0.05em solid;background-color:yellow'><th colspan ='" + 4 + "'><h1 style='text-align:center'>" + InvoiceNO2 + " </h1></th>" +
                                          "<td colspan ='" + 4 + "'><h1 style='text-align:center'>" + InvoiceDate2 + " </h1></td></tr>");
                    htw.Write("<table><tr style='height=10px;border:0.05em solid;background-color:yellow'><th colspan ='" + 4 + "'><h1 style='text-align:center'>" + Fromdate + " </h1></th>" +
                                        "<td colspan ='" + 4 + "'><h1 style='text-align:center'>" + ToDate + " </h1></td></tr>");
                    htw.Write("<table><tr style='height=5px;border:0.05em solid'><td colspan='" + 8 + "'><h6 style='text-align:right'> *output generated by tracker </h6></td></tr>");
                    tb.RenderControl(htw);


                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }


        public ActionResult ExporttoExcelAnnexureBillreco(string InvoiceNo)
        {
            DataTable LoadingExcel = new DataTable();

            DataSet TrainEXPORTList = new DataSet();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TrainEXPORTList = db.sub_GetDataSets("getInvoicePrintMVC_billReco '" + InvoiceNo + "'");


            DataTable CompanyMaster = new DataTable();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";


            var InvoiceNo1 = Convert.ToString(TrainEXPORTList.Tables[0].Rows[0]["invoiceno"]);
            var InvoiceDate = Convert.ToString(TrainEXPORTList.Tables[0].Rows[0]["assessDate"]);
            var Fromdate1 = Convert.ToString(TrainEXPORTList.Tables[0].Rows[0]["fromdate"]);
            var Todate1 = Convert.ToString(TrainEXPORTList.Tables[0].Rows[0]["todate"]);

            string Headtitile = "Proforma Invoice Annexure";
            string InvoiceNO2 = "Invoice No :" + InvoiceNo1 + "";
            string InvoiceDate2 = "Invoice Date :" + InvoiceDate + "";
            string Fromdate = "From Date :" + Fromdate1 + "";
            string ToDate = "To Date :" + Todate1 + "";


            GridView gridview = new GridView();

            TrainEXPORTList.Tables[2].Columns.Remove("Voucher date");
            gridview.DataSource = TrainEXPORTList.Tables[2];
            gridview.DataBind();



            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Controls.Add(gridview);

            tr1.Cells.Add(cell1);
            tb.Rows.Add(tr1);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Tax_Invoice_Annexure_Report.xls");

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr style='height=20px;border:0.05em solid;background-color:yellow'><td colspan ='" + 8 + "'><h1 style='text-align:center'>" + Headtitile + " </h1></td></tr>");
                    htw.Write("<table><tr style='height=10px;border:0.05em solid;background-color:yellow'><th colspan ='" + 4 + "'><h1 style='text-align:center'>" + InvoiceNO2 + " </h1></th>" +
                                          "<td colspan ='" + 4 + "'><h1 style='text-align:center'>" + InvoiceDate2 + " </h1></td></tr>");
                    htw.Write("<table><tr style='height=10px;border:0.05em solid;background-color:yellow'><th colspan ='" + 4 + "'><h1 style='text-align:center'>" + Fromdate + " </h1></th>" +
                                        "<td colspan ='" + 4 + "'><h1 style='text-align:center'>" + ToDate + " </h1></td></tr>");
                    htw.Write("<table><tr style='height=5px;border:0.05em solid'><td colspan='" + 8 + "'><h6 style='text-align:right'> *output generated by tracker </h6></td></tr>");
                    tb.RenderControl(htw);


                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }


        public JsonResult AjxImportTariffSettingFileForCustomer(BE.TariffAddDetailsEntites fileData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";

            string TariffID = fileData.TariffID;
            string AccountingName = fileData.Accounting;

            List<BE.TariffAddDetailsEntites> CustomerTariffDetails = new List<BE.TariffAddDetailsEntites>();
            if (Request.Files.Count > 0)
            {
                try
                {

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

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

                        //string MSNO = file.TariffID;
                        //string MSNO1 = file.AccountingName;


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
                        //BL.AddErrorLog(conString.Replace("'",""));
                        //BL.AddErrorLog(fname.Replace("'", ""));
                        //  conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                        //  BL.AddErrorLog("Connection close");
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

                                    DataTable DriverPaymentDT = new DataTable();


                                    DriverPaymentDT.Columns.Add("Tariff");
                                    DriverPaymentDT.Columns.Add("Account Name");
                                    DriverPaymentDT.Columns.Add("Activity");
                                    DriverPaymentDT.Columns.Add("From");
                                    DriverPaymentDT.Columns.Add("To");
                                    DriverPaymentDT.Columns.Add("Size");
                                    DriverPaymentDT.Columns.Add("Type");
                                    DriverPaymentDT.Columns.Add("Amount");
                                    DriverPaymentDT.Columns.Add("From Location");
                                    DriverPaymentDT.Columns.Add("To Location");

                                    DriverPaymentDT.Columns.Add("rate");
                                    DriverPaymentDT.Columns.Add("Insurance");
                                    DriverPaymentDT.Columns.Add("Tax");
                                    DriverPaymentDT.Columns.Add("Free Days");
                                    DriverPaymentDT.Columns.Add("Driver_type");
                                    //DriverPaymentDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[0].ToString() != "" && row[1].ToString() != "" && row[2].ToString() != "" && row[3].ToString() != "" && row[4].ToString() != ""
                                            && row[5].ToString() != "" && row[6].ToString() != "" && row[7].ToString() != "" && row[8].ToString() != "" && row[9].ToString() != "" && row[10].ToString() != "")
                                        {
                                            DataRow dar = DriverPaymentDT.NewRow();

                                            dar["Tariff"] = row[0].ToString().ToUpper();
                                            dar["Account Name"] = row[1].ToString().ToUpper();
                                            dar["Activity"] = row[2].ToString().ToUpper();
                                            dar["From"] = row[3].ToString().ToUpper();
                                            dar["To"] = row[4].ToString();
                                            dar["Size"] = row[5].ToString();
                                            dar["Type"] = row[6].ToString();
                                            dar["Amount"] = row[7].ToString();
                                            dar["From Location"] = row[8].ToString();
                                            dar["To Location"] = row[9].ToString();
                                            dar["rate"] = row[10].ToString();
                                            dar["Insurance"] = row[11].ToString();
                                            dar["Tax"] = row[12].ToString();
                                            dar["Free Days"] = row[13].ToString();
                                            dar["Driver_type"] = row[14].ToString();

                                            DriverPaymentDT.Rows.Add(dar);
                                            int linenum = dt.Rows.IndexOf(row);
                                            if (linenum == 1050)
                                            {
                                                int count1 = 0;
                                            }

                                        }
                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }

                                    if (DriverPaymentDT != null)
                                    {

                                        CustomerTariffDetails = invDataProvider.GetCustomerTariffDetails(DriverPaymentDT, TariffID, AccountingName, Userid);


                                        var jsonResult = Json(CustomerTariffDetails, JsonRequestBehavior.AllowGet);
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

                    return Json("Error occurred. Error details: " + ex.Message);

                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult ExportToFormatCustomertariff()
        {
            string fileName = "~/Format/Tariff_Setting_Details.xlsx";
            if (fileName != "")
            {
                string filePath = fileName;
                Response.ContentType = "doc/docx";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }

            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("ContainerNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("Size", typeof(string)));
            //dt.Columns.Add(new DataColumn("Commodity", typeof(string)));
            //dt.Columns.Add(new DataColumn("Cargo Type", typeof(string)));
            //dt.Columns.Add(new DataColumn("ISOCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("FL", typeof(string)));
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            ////dt.Columns.Add(new DataColumn("FL", typeof(string)));
            //GridView gridview = new GridView();
            //gridview.DataSource = dt;
            //gridview.DataBind();

            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment;filename=ExcelUploadFormat.xls");
            //using (StringWriter sw = new StringWriter())
            //{
            //    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            //    {
            //        // render the GridView to the HtmlTextWriter


            //        gridview.RenderControl(htw);
            //        // Output the GridView content saved into StringWriter
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }
            //}

            return View();
        }

        [HttpPost]
        public ActionResult ImportTransportInvoicePrint(string InvoiceNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblHeadDetails = new DataTable();
            DataTable tblChargesDetails = new DataTable();
            DataTable tblWordAmount = new DataTable();
            DataTable tblBankDetails = new DataTable();
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
            double TotoalamountForContaniner = 0;

            getJobOrderSet = db.sub_GetDataSets("getTransportInvoicePrintMVC '" + InvoiceNo + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblHeadDetails = getJobOrderSet.Tables[1];
                tblContainerDetails = getJobOrderSet.Tables[2];
                tblWordAmount = getJobOrderSet.Tables[3];


                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo1"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.CINNo = dr["CINNo"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.Addressl = dr["AddressI"];
                    ViewBag.IMP_AC_EMAILID = dr["IMP_AC_EMAILID"];
                    ViewBag.NoteVS = dr["NoteVI"];

                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.InvoiceDate = dr["AssessDate"];
                    ViewBag.Name = dr["GSTName"];
                    ViewBag.Address = dr["GSTAddress"];
                    ViewBag.GSTIN = dr["GSTIn_uniqID"];
                    ViewBag.PlaceofSupply = dr["State"];
                    ViewBag.state_Code = dr["state_Code"];
                    ViewBag.BankName = dr["BankName"];
                    ViewBag.AccountNo = dr["AccountNo"];
                    ViewBag.IFSCCode = dr["IFSCCode"];
                    ViewBag.Remarks = dr["Remarks"];
                    ViewBag.BranchName = dr["BranchName"];
                    ViewBag.fromDate = dr["fromdate"];
                    ViewBag.Todate = dr["todate"];

                }



                ViewBag.TotalAmountInWord = tblWordAmount.Rows[0]["TotalAmountInWords"];
                ViewBag.TotalAmount = tblWordAmount.Rows[0]["GrandTotal"];
                ViewBag.AmountWithoutTax = tblWordAmount.Rows[0]["NetTotal"];
                ViewBag.SGSTAmount = tblWordAmount.Rows[0]["SGST"];
                ViewBag.CGSTAmount = tblWordAmount.Rows[0]["CGST"];
                ViewBag.IGSTAmount = tblWordAmount.Rows[0]["IGST"];
                ViewBag.TaxAmount = Convert.ToDouble(Convert.ToDouble(tblWordAmount.Rows[0]["SGST"]) + Convert.ToDouble(tblWordAmount.Rows[0]["CGST"]) + Convert.ToDouble(tblWordAmount.Rows[0]["IGST"]));

            }

            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();
            ViewBag.HeadDetailsList = tblHeadDetails.AsEnumerable();
            //ViewBag.ChargesDetailsList = tblChargesDetails.AsEnumerable();



            foreach (DataRow data in tblHeadDetails.Rows)
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

            foreach (DataRow data in tblContainerDetails.Rows)
            {
                TotoalamountForContaniner = Amount + SGST + CGST + IGST;

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




            ViewBag.TotoalamountForContaniner = TotoalamountForContaniner;

            return PartialView();

        }

        //IGM Item Wise Periodic Billing

        public ActionResult IGMItemWiseMapping()
        {

            BM.LRClosing.LRClosing trackerProvider = new BM.LRClosing.LRClosing();
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = trackerProvider.GetCustomer1();
            ViewBag.CustomerList = new SelectList(Customer, "AGID", "AGName");

            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.UpdateCustomer> List = new List<BE.UpdateCustomer>();
            dataTable = db.sub_GetDatatable("USP_GetSalesPersonDDL");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.UpdateCustomer item = new BE.UpdateCustomer();
                    item.SalesID = Convert.ToInt32(row["SalesPerson_ID1"]);
                    item.SalesName = Convert.ToString(row["SalesPerson_Name"]);
                    List.Add(item);
                }
            }
            ViewBag.SalesPerson = new SelectList(List, "SalesID", "SalesName");
            return View();
        }
        public ActionResult getUpdateCustomerList(string Category, string searchText1, string searchText2)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();

            dataTable = db.sub_GetDatatable("USP_Get_UpadtePeriodicCustomerDtl '" + searchText1 + "','" + searchText2 + "', '" + Category + "'");

            var summaryDet = JsonConvert.SerializeObject(dataTable);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult UpdateCustomerDtls(List<BE.UpdateCustomer> Tabledtl, BE.UpdateCustomer formData)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            BE.ResponseMessage responseMessage = new BE.ResponseMessage();
            string messgage = "";
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            foreach (BE.UpdateCustomer item in Tabledtl)
            {

                dataTable = db.sub_GetDatatable("USP_UpdateCustomerPeriodicMapping " + formData.AgID + ", " + formData.SalesID + ",'" + item.ContainerNo + "', " + item.JONo + "," + UserID + ",'" + DateTime.Now.ToString("dd MMM yyyy HH:mm") + "'");
                if (dataTable.Rows.Count != 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        responseMessage.Status = Convert.ToInt32(row["Status"]);
                        responseMessage.Message = Convert.ToString(row["message"]);
                        if (responseMessage.Status == 0)
                        {
                            return Json(responseMessage);
                        }
                    }
                }
            }
            return Json(responseMessage);
        }
        public ActionResult GetPeriodicMapsummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("sp_GetPeriodicMappingSummary '" + FromDate + "','" + ToDate + "'");
            Session["sp_GetPeriodicMappingSummary"] = dt;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelGetPeriodicMapsummary()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
   
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["sp_GetPeriodicMappingSummary"];
            getMovementICDNew.Columns.Remove("#");
            string Tittle = "From " + Session["FromDate"] + " To " + Session["ToDate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=IGM-Item_Wise_Mapping.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>IGM-Item Wise Mapping<strong></td></tr>");
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
        public ActionResult getPeriodicMapPendency(string AsOn)
        {
            try
            {
                HC.DBOperations db = new HC.DBOperations(); DataTable dataTable = new DataTable();
                dataTable = db.sub_GetDatatable("usp_Awating_GeneratePeriodicInvoice");
                Session["usp_Awating_GeneratePeriodicInvoice"] = dataTable;
                var summaryDet = JsonConvert.SerializeObject(dataTable);
                var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;


            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        public ActionResult ExportToExcelgetPeriodicMapPendency()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["usp_Awating_GeneratePeriodicInvoice"];
            //getMovementICDNew.Columns.Remove("#");
            //string Tittle = "From " + Session["FromDate"] + " To " + Session["ToDate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=AwaitngPeriodicPendency.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Awaiting Periodic Pendency<strong></td></tr>");
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
        public JsonResult GetGSTAdressPeriodic(string prefixText, string Mode)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GetGSTAdressPeriodic '" + Mode + "','" + prefixText + "'");

            //if (dataTable != null)
            //{
            //    foreach (DataRow row in dataTable.Rows)
            //    {
            //        BE.Customer customer = new BE.Customer();
            //        customer.AGID = Convert.ToInt32(row["GSTID"]);
            //        customer.AGName = Convert.ToString(row["GSTName"]);
            //        Customerlst.Add(customer);
            //    }
            //}
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        BE.Customer customer = new BE.Customer();
                        customer.AGID = Convert.ToInt32(row["GSTID"]);
                        customer.AGName = Convert.ToString(row["GSTName"]);
                        Customerlst.Add(customer);
                    }
                }
                else
                {
                    BE.Customer customer = new BE.Customer();
                    customer.AGID = 0;
                    customer.AGName = "No Data Found";
                    Customerlst.Add(customer);
                }
            }

            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        //Mapping Customer for Periodic Bill

        public ActionResult MappingCustomer()
        {

            BM.LRClosing.LRClosing trackerProvider = new BM.LRClosing.LRClosing();
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = trackerProvider.GetCustomer1();
            ViewBag.CustomerList = new SelectList(Customer, "AGID", "AGName");

            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.UpdateCustomer> List = new List<BE.UpdateCustomer>();
            dataTable = db.sub_GetDatatable("USP_GetSalesPersonDDL");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.UpdateCustomer item = new BE.UpdateCustomer();
                    item.SalesID = Convert.ToInt32(row["SalesPerson_ID1"]);
                    item.SalesName = Convert.ToString(row["SalesPerson_Name"]);
                    List.Add(item);
                }
            }
            ViewBag.SalesPerson = new SelectList(List, "SalesID", "SalesName");
            List<BE.GetProcesses> GetProcessesID = new List<BE.GetProcesses>();

            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_getDeliveryType");
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.GetProcesses item = new BE.GetProcesses();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Processes = Convert.ToString(row["Processes"]);
                    GetProcessesID.Add(item);
                }
            }
            ViewBag.ProcessesName = new SelectList(GetProcessesID, "ID", "Processes");
            return View();
        }

        public ActionResult UpdatePeriodicCustomerDtls(BE.UpdateCustomer formData)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            BE.ResponseMessage responseMessage = new BE.ResponseMessage();
            string messgage = "";
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            dataTable = db.sub_GetDatatable("USP_UpdatePeriodicCustomerDtl '" + formData.AgID + "', '" + formData.Processes + "', '" + formData.EffectiveFrom + "', '" + formData.EffectiveUpTodate + "'," + UserID + ",'" + DateTime.Now.ToString("dd MMM yyyy HH:mm") + "'");
            if (dataTable.Rows.Count != 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    responseMessage.Status = Convert.ToInt32(row["Status"]);
                    responseMessage.Message = Convert.ToString(row["message"]);
                    if (responseMessage.Status == 0)
                    {
                        return Json(responseMessage);
                    }
                }
            }
            return Json(responseMessage);
        }

        public ActionResult GetPeriodicMappingsummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("sp_GetPeriodiccustomerMappingSummary");
            Session["GetPeriodicMappingsummary"] = dt;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelGetPeriodicMappingsummary()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["GetPeriodicMappingsummary"];
            
            string Tittle = "From " + Session["FromDate"] + " To " + Session["ToDate"] + ".";
          
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Customer_Wise_Mapping.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Customer Wise Mapping<strong></td></tr>");
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
    }
}