using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ExpensesHead;
using RS = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ReceiptAdjustment;
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

namespace TrackerMVC.Controllers.ExpensesHead
{
    public class ExpensesHeadController : Controller
    {
        RP.ExpensesHead reportprovider = new RP.ExpensesHead();
        ///RP. reportprovider = new RP.Chequebounce();
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        RS.ReceiptAdjustment trainTrackerProvider = new RS.ReceiptAdjustment();
        public ActionResult ExpensesHeadMasters()
        {
            List<BE.ExpHeadMasterEnt> ExpGroupDDL = new List<BE.ExpHeadMasterEnt>();
            ExpGroupDDL = reportprovider.GroupGetDDl();
            ViewBag.GroupDDL = new SelectList(ExpGroupDDL, "ExpGroupID", "ExpGroupName");

            List<BE.ExpHeadMasterEnt> SubExpDDL = new List<BE.ExpHeadMasterEnt>();
            SubExpDDL = reportprovider.SubEXPGetDll();
            ViewBag.SubHead = new SelectList(SubExpDDL, "SubExpID", "SubExpHead");

            List<BE.ExpHeadMasterEnt> ExpensesType = new List<BE.ExpHeadMasterEnt>();
            ExpensesType = reportprovider.getExpensesType();
            ViewBag.ExpensesType = new SelectList(ExpensesType, "ExpTypeID", "ExpType");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();

        }
        public JsonResult Save(BE.ExpHeadMasterEnt ExpHeadMasterEnt)
        {
            var EntryDate = ExpHeadMasterEnt.EntryDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            db.sub_ExecuteNonQuery("USP_InsertExpensesHeadMSuraj '" + Convert.ToDateTime(ExpHeadMasterEnt.EntryDate).ToString("yyyy-MM-dd HH:mm") + "','" + ExpHeadMasterEnt.HeadName + "','" + ExpHeadMasterEnt.IsActive + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + ExpHeadMasterEnt.ExpGroupID + "','" + ExpHeadMasterEnt.SubExpID + "','" + ExpHeadMasterEnt.ExpTypeID + "','" + ExpHeadMasterEnt.EntryID + "'");
            // Master();
            return Json(ExpHeadMasterEnt);
        }

        public JsonResult Search(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("Expenses_Summary_List'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExpensesTypeMaster()
        {
            List<BE.ExpHeadMasterEnt> ExpGroupDDL = new List<BE.ExpHeadMasterEnt>();
            ExpGroupDDL = reportprovider.GroupGetDDl();
            ViewBag.GroupDDL = new SelectList(ExpGroupDDL, "ExpGroupID", "ExpGroupName");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();

        }

        public JsonResult Save1(BE.ExpHeadMasterEnt ExpHeadMasterEnt)
        {
            var EntryDate = ExpHeadMasterEnt.EntryDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            db.sub_ExecuteNonQuery("USP_InsertExpensesTypeM '" + Convert.ToDateTime(ExpHeadMasterEnt.EntryDate).ToString("yyyy-MM-dd HH:mm") + "','" + ExpHeadMasterEnt.ExpType + "','" + ExpHeadMasterEnt.IsActive + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + ExpHeadMasterEnt.ExpGroupID + "'");
            // Master();
            return Json(ExpHeadMasterEnt);
        }

        public JsonResult Search1(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("usp_expenses_type'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ExpensesSubHead()
        {
            List<BE.ExpHeadMasterEnt> ExpGroupDDL = new List<BE.ExpHeadMasterEnt>();
            ExpGroupDDL = reportprovider.GroupGetDDl();
            ViewBag.GroupDDL = new SelectList(ExpGroupDDL, "ExpGroupID", "ExpGroupName");

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();

        }
        public JsonResult SubSave(BE.ExpHeadMasterEnt ExpHeadMasterEnt)
        {
            var EntryDate = ExpHeadMasterEnt.EntryDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            db.sub_ExecuteNonQuery("USP_InsertSubExpensesHeadM '" + ExpHeadMasterEnt.HeadName + "','" + ExpHeadMasterEnt.IsActive + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + ExpHeadMasterEnt.ExpGroupID + "'");
            // Master();
            return Json(ExpHeadMasterEnt);
        }

        public JsonResult SubSearch(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("SearchExpGroup'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ExpensesEntryMaster()
        {
            List<BE.ExpHeadMasterEnt> Customer = new List<BE.ExpHeadMasterEnt>();
            Customer = reportprovider.getExpenses();
            ViewBag.Customer = new SelectList(Customer, "EntryID", "HeadName");

            List<BE.ExpHeadMasterEnt> ExpensesType = new List<BE.ExpHeadMasterEnt>();
            ExpensesType = reportprovider.getExpensesType();
            ViewBag.ExpensesType = new SelectList(ExpensesType, "EntryID", "ExpType");

            List<BE.ExpHeadMasterEnt>  TaxName = new List<BE.ExpHeadMasterEnt>();
            TaxName = reportprovider.getTaxName();
            ViewBag.TaxName = new SelectList(TaxName, "TaxID", "TaxName");
            

            List<BE.ExpHeadMasterEnt> PayMode = new List<BE.ExpHeadMasterEnt>();
            PayMode = reportprovider.getPayMode();
            ViewBag.PayMode = new SelectList(PayMode, "PaymodeId", "Paymode");
            

            List<BE.ExpHeadMasterEnt> EntryType = new List<BE.ExpHeadMasterEnt>();
            EntryType = reportprovider.getEntryType();
            ViewBag.EntryType = new SelectList(EntryType, "EntryTypeID", "EntryType");
            return View();

        }

        public JsonResult GetIGSTCGST(string Taxname, string partyID, string netamount,string DiscountAmount)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExpHeadMasterEnt> receiptEntries = new List<BE.ExpHeadMasterEnt>();
            dataTable = db.sub_GetDatatable("USP_Expenses_Tax '" + Taxname + "','" + partyID + "','" + netamount + "','" + DiscountAmount + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpHeadMasterEnt receiptEntry = new BE.ExpHeadMasterEnt();
                    receiptEntry.sgst = Convert.ToInt32(row["sgst"]);
                    receiptEntry.cgst = Convert.ToInt32(row["cgst"]);
                    receiptEntry.igst = Convert.ToInt32(row["igst"]);
                    receiptEntries.Add(receiptEntry);
                }
            }

            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetGSTNOForCustomer(string gstID)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExpHeadMasterEnt> receiptEntries = new List<BE.ExpHeadMasterEnt>();
            dataTable = db.sub_GetDatatable("USP_Expenses_Type_Fillter '" + gstID + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpHeadMasterEnt receiptEntry = new BE.ExpHeadMasterEnt();
                    receiptEntry.ExpType = Convert.ToString(row["ExpType"]);
                    receiptEntry.Sub_Exp_Head = Convert.ToString(row["Sub_Exp_Head"]);
                    receiptEntry.Group_Name = Convert.ToString(row["Group_Name"]);
                    receiptEntries.Add(receiptEntry);
                }
            }

            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult GetGSTNOForVendor(string gstID)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ExpHeadMasterEnt> receiptEntries = new List<BE.ExpHeadMasterEnt>();
            dataTable = db.sub_GetDatatable("usp_Vendor_GST_No '" + gstID + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpHeadMasterEnt receiptEntry = new BE.ExpHeadMasterEnt();
                    receiptEntry.GSTNo = Convert.ToString(row["GSTIn_uniqID"]);
                  
                    receiptEntries.Add(receiptEntry);
                }
            }

            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult getPartyNameReceipt(string prefixText, string Mode)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GetPartyNameVendor '" + Mode + "','" + prefixText + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.Customer customer = new BE.Customer();
                    customer.AGID = Convert.ToInt32(row["VendorID"]);
                    customer.AGName = Convert.ToString(row["VendorName"]);
                    Customerlst.Add(customer);
                }
            }

            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public JsonResult GetautpcompleteDebitDetails(string prefixText, string Mode)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GetPartyNameExpensese '" + Mode + "','" + prefixText + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.Customer customer = new BE.Customer();
                    customer.AGID = Convert.ToInt32(row["Expenses_Head_id"]);
                    customer.AGName = Convert.ToString(row["HeadName"]);
                    Customerlst.Add(customer);
                }
            }

            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public JsonResult GetautpcompleteHSnCode(string prefixText, string Mode)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GetHSNExpensese '" + Mode + "','" + prefixText + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.Customer customer = new BE.Customer();
                    customer.AGID = Convert.ToInt32(row["HSNCodeID"]);
                    customer.AGName = Convert.ToString(row["HSN_COde"]);
                    Customerlst.Add(customer);
                }
            }

            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult SavePartyDebitEntryDetails(List<BE.ExpHeadMasterEnt> Debitdata, string Billno, string Billdate, double VendorID, string netTotalamount,
         string Cgst, string IGST, string Sgst, string Grandtotal, string Remarks,int entryTypeID, string NetAmounts, string VOUCHER_NO, string VoucherDate)
        {


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FromDate");
            dataTable.Columns.Add("ToDate");
            dataTable.Columns.Add("ExpensesHead");
            dataTable.Columns.Add("Untt");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("HSNCode");
            dataTable.Columns.Add("Netamount");
            dataTable.Columns.Add("CGST1");
            dataTable.Columns.Add("SGST1");
            dataTable.Columns.Add("IGST1");
            dataTable.Columns.Add("TaxName");
            dataTable.Columns.Add("netDiscount");
            dataTable.Columns.Add("netDiscountS");
            dataTable.Columns.Add("ddlparTDS");
            dataTable.Columns.Add("tdsAmount");


            dataTable.TableName = "PT_ExpensesVoucher";


            foreach (BE.ExpHeadMasterEnt item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["FromDate"] = item.FromDate;
                row["ToDate"] = Convert.ToDateTime(item.ToDate).ToString("yyyy-MM-dd HH:mm:ss");
                row["ExpensesHead"] = item.ExpensesHead;
                row["Untt"] = item.HSNCode1;
                row["Description"] = item.Description;
                row["HSNCode"] = item.HSNCode;
                row["Netamount"] = item.Netamount;
                row["CGST1"] = item.CGST1;
                row["SGST1"] = item.SGST1;
                row["IGST1"] = item.IGST1;
                row["TaxName"] = item.TaxName;
                row["netDiscount"] = item.netDiscount;
                row["netDiscountS"] = item.netDiscountS;
                row["ddlparTDS"] = item.ddlparTDS;
                row["tdsAmount"] = item.tdsAmount;


                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = reportprovider.SavepartyDebitEntry(dataTable, VendorID, Billno, Billdate , Grandtotal, Sgst,Cgst, IGST , Remarks, Userid, entryTypeID, NetAmounts, VOUCHER_NO, VoucherDate);
            return Json(message);

        }

        public ActionResult PendingForApprovel(string AsOn)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("sp_showExpensesEntry_approved '" + Convert.ToDateTime(AsOn).ToString("yyyy-MM-dd HH:mm:ss")  + "'");
            Session["spshowExpensesEntryapproved"] = dt;
            Session["AsOn"] = AsOn;
             
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelPendingForApprovel()
        {

            DataTable dt = (DataTable)Session["spshowExpensesEntryapproved"];
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            //DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            string Tittle = "AsOn " + Session["AsOn"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Pending For Approvel.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Debit Bill Report <strong></td></tr>");
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



        public JsonResult CancelJO(string VOUCHER_NO)
        {
            //var EntryDate = ExpHeadMasterEnt.EntryDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            db.sub_ExecuteNonQuery("usp_Approved_Voucher '" + VOUCHER_NO + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");
            // Master
            string json = JsonConvert.SerializeObject(db);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return Json(jsonResult);
        }

        public JsonResult DeleteVoucher(string VOUCHER_NO)
        {
            //var EntryDate = ExpHeadMasterEnt.EntryDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            db.sub_ExecuteNonQuery("USP_Delete_Vouchar '" + VOUCHER_NO + "'");
            // Master
            string json = JsonConvert.SerializeObject(db);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return Json(jsonResult);
        }
        public ActionResult Pendinfopaymen(string AsOn)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("sp_showExpensesEntry_pAYMENT '" + Convert.ToDateTime(AsOn).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["sp_showExpensesEntry_pAYMENT"] = dt;
            Session["AsOn"] = AsOn;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelPendinfopaymen()
        {

            DataTable dt = (DataTable)Session["sp_showExpensesEntry_pAYMENT"];
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            //DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            string Tittle = "AsOn " + Session["AsOn"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Pending For Approvel.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Debit Bill Report <strong></td></tr>");
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

        public ActionResult AjaxGetDebitDetails(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("sp_showExpensesEntry '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["DebitBillEntryDetails"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult ModifyExistingJo(string VO_No)
        {

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.ExpHeadMasterEnt> JOLIst = new List<BE.ExpHeadMasterEnt>();
            JOLIst = reportprovider.GetJOListForSummary(VO_No);
            return Json(JOLIst);
            //var jsonResult = Json(JOLIst, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return Json(jsonResult);

        }

        public JsonResult ModifyExistingView(string VOUCHER_NO)
        {

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.ExpHeadMasterEnt> JOLIst = new List<BE.ExpHeadMasterEnt>();
            JOLIst = reportprovider.GetViewSummary(VOUCHER_NO);
            return Json(JOLIst);
            //var jsonResult = Json(JOLIst, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return Json(jsonResult);

        }

        public ActionResult SearchView(string Search)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("sp_showExpensesEntry_View '" + Search + "'");
            Session["spshowExpensesEntryapproved"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult BankMaster()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }
        public ActionResult ChangeViaNo()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }
        public JsonResult AddBank(BE.ExpHeadMasterEnt ExpHeadMasterEnt)
        {
            var EntryDate = ExpHeadMasterEnt.EntryDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            db.sub_ExecuteNonQuery("USP_InsertBankDetails '" + ExpHeadMasterEnt.BankName + "','" + ExpHeadMasterEnt.IsActive + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");
            // Master();
            return Json(ExpHeadMasterEnt);
        }
        public JsonResult UpdateViaNo(string OldViaNo,string NewViaNo)
        { 
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            db.sub_ExecuteNonQuery("USP_UpdateViaNo_OldToNew '" + OldViaNo + "','" + NewViaNo + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");
            // Master();
            return Json(1);
        }
        public JsonResult BankSearch(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("BankDetaisM'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ImportAccountMaster()
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

        public JsonResult GetImportAcList(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_SearchAccountDetails'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult SaveIAM(BE.ExpHeadMasterEnt ExpHeadMasterEnt)
        {
            var EntryDate = ExpHeadMasterEnt.EntryDate;
            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            db.sub_ExecuteNonQuery("SP_SaveAccountMaster_New '" + ExpHeadMasterEnt.AcName + "','" + ExpHeadMasterEnt.TallyName + "','" + ExpHeadMasterEnt.disp + "','" + ExpHeadMasterEnt.IsActive + "','" + ExpHeadMasterEnt.IMPGID + "','" + ExpHeadMasterEnt.HSNCodeL + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + ExpHeadMasterEnt.isdpd + "','" + ExpHeadMasterEnt.InvTId + "','" + ExpHeadMasterEnt.TaxID + "','" + ExpHeadMasterEnt.chargefors + "'");
            // Master();
            return Json(ExpHeadMasterEnt);
        }
        public ActionResult CityMaster()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            List<BE.CityMaster> List = new List<BE.CityMaster>();
            List = reportprovider.GetState();
            ViewBag.GroupDDL = new SelectList(List, "StateID", "StateName");
            return View();

        }

        public JsonResult SaveCityDetails(BE.CityMaster CityMaster)
        {

            //HC.DBOperation object = new HC.DBOperations(); From Helper
            CD.DBOperations db = new CD.DBOperations();
            //Code For Insert Data Sequence Should Be Same As Created SP.
            if (CityMaster != null)
                db.sub_ExecuteNonQuery("sp_InserCityMaster '" + CityMaster.StateID + "','" + CityMaster.CityName + "','" + CityMaster.PinCode + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + CityMaster.IsActive + "'");
            // Master();

            return Json(CityMaster);
        }
        public JsonResult CityList(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("sp_CityList'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult LockedData()
        {

            List<BE.LockData> List = new List<BE.LockData>();
            List = reportprovider.UserDetails();
            ViewBag.UserList = new SelectList(List, "UserID", "UserName");

            return View();
        }


        // var data1 = {
        //      'FromDate': FromDate, 'Activity': Activity, 'ToDate': ToDate, 'Remarks': Remarks
        //  };

        public JsonResult SaveLockedData(string FromDate, string Activity, string ToDate, string Remarks)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("SP_Locaked_Save '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Activity + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + Remarks + "','" + userId + "'");

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

        public JsonResult LockedDataList(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("Locked_Data_M_Summary_List'" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetLockedData(string Locked_ID)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.LockData> receiptEntries = new List<BE.LockData>();
            dataTable = db.sub_GetDatatable("USp_GetLockdetails '" + Locked_ID + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.LockData receiptEntry = new BE.LockData();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.Locked_ID = Convert.ToInt32(row["Locked_ID"]);

                    receiptEntry.Locked_on = Convert.ToString(row["Locked_on"]);
                    receiptEntry.Locked_Till_Date = Convert.ToString(row["Locked_Till_Date"]);
                    receiptEntry.Remarks = Convert.ToString(row["Remarks"]);
                    receiptEntry.Activity = Convert.ToString(row["Activity"]);

                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult SaveUnlockedData(string FromDate, string Activity, string ToDate, string Remarks, string RequestBy)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("SP_Unlocaked_Save '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Activity + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + Remarks + "','" + RequestBy + "','" + userId + "'");

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


        //TCS Sagar
        public ActionResult UpdateTCS()
        {

            return View();
        }


        public JsonResult GetTCSAmountDet(string AssessNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.TCSAmountMod> receiptEntries = new List<BE.TCSAmountMod>();
            dataTable = db.sub_GetDatatable("USP_IMP_SHOW_INVDetas '" + AssessNo + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.TCSAmountMod receiptEntry = new BE.TCSAmountMod();
                    //DeclQty,UnitWT,Allowwt,[Location],EntryDate
                    receiptEntry.GrandTotal = Convert.ToInt32(row["GrandTotal"]);

                    receiptEntry.tcsamt = Convert.ToInt64(row["tcsamt"]);
                    receiptEntry.AssessDate = Convert.ToString(row["AssessDate"]);


                    receiptEntries.Add(receiptEntry);
                }
            }
            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public JsonResult UpdateTCSAmt(string InvoiceNo, string TcsAmt)
        {
            DataTable tblInvoiceList = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("USP_IMP_UPDATTCSAMT '" + InvoiceNo + "','" + TcsAmt + "'");

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

        // CODE BY SAGAR
        public ActionResult ActivityMaster()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            return View();

        }
        public JsonResult SaveActivityMaster(BE.ActivityMasters ActivityMasters)
        {

            CD.DBOperations db = new CD.DBOperations();
            db.sub_ExecuteNonQuery("Usp_InsertActivityMaster '" + ActivityMasters.ActivityName + "','" + ActivityMasters.TimeType + "','" + ActivityMasters.MaxAllow + "','" + ActivityMasters.LR + "','" + ActivityMasters.IO + "','" + ActivityMasters.ActivityStatus + "','" + ActivityMasters.FG + "','" + ActivityMasters.Max + "','" + ActivityMasters.Cycle + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + ActivityMasters.IsActive + "','" + ActivityMasters.EntryID + "'");
            return Json(ActivityMasters);
        }
        //public JsonResult ActivityReport(string search)
        //{
        //    CD.DBOperations db = new CD.DBOperations();
        //    DataTable dt = new DataTable();
        //    dt = db.sub_GetDatatable("GetActivityDetails'" + search + "'");
        //    var summaryDet = JsonConvert.SerializeObject(dt);
        //    var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
        public JsonResult ActivityReport(string Search)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ActivityOBJ = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            ActivityOBJ = db.sub_GetDatatable("GetActivityDetails '" + Search + "'");
            Session["ExpInvList"] = ActivityOBJ;
            //Session["fromdate"] = AGID;
            //Session["todate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(ActivityOBJ), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ExportActivityMasterReport()
        {
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable obj = (DataTable)Session["ExpInvList"]; //object.columns.remove("column_name");
            obj.Columns.Remove("Edit");
            GridView gridview = new GridView();
            gridview.DataSource = obj;
            gridview.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Activity-Master-Report.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Activity Master List<strong></td></tr>");
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
        // get details in text box
        public JsonResult GetActivity(string AutoID)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ActivityMasters> receiptEntries = new List<BE.ActivityMasters>();
            dataTable = db.sub_GetDatatable("getactivitymdata '" + AutoID + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ActivityMasters receiptEntry = new BE.ActivityMasters();
                    receiptEntry.EntryID = Convert.ToInt32(row["AutoID"]);
                    receiptEntry.ActivityName = Convert.ToString(row["Activity"]);
                    receiptEntry.TimeType = Convert.ToInt32(row["Timetype"]);
                    receiptEntry.MaxAllow = Convert.ToInt32(row["maxAllow"]);
                    receiptEntry.LR = Convert.ToBoolean(row["islr"]);
                    receiptEntry.IO = Convert.ToBoolean(row["VehicleInOut"]);
                    receiptEntry.ActivityStatus = Convert.ToString(row["Activity_Status"]);
                    receiptEntry.FG = Convert.ToString(row["Fuel_Group"]);
                    receiptEntry.Max = Convert.ToInt32(row["AfterHRSAllow"]);
                    receiptEntry.Cycle = Convert.ToString(row["cycle"]);

                    receiptEntries.Add(receiptEntry);
                }
            }

            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult UpdateOpeningAmount()
        {
            List<BE.ReceiptAdjustments> CategoryList = new List<BE.ReceiptAdjustments>();
            CategoryList = trainTrackerProvider.Categorys();
            ViewBag.CategoryList = new SelectList(CategoryList, "ID", "Criteria");
            return View();
        }

        public ActionResult SaveUpdateOpeningDtl(BE.UpdateOpeningAmount FromData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.ResponseMessage responseMessage = new BE.ResponseMessage();
            responseMessage = reportprovider.SaveUpdateOpeningDtl(FromData, Userid);
            return Json(responseMessage);
        } 

        public ActionResult SaveHoldDtls(BE.UpdateOpeningAmount HoldData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.ResponseMessage responseMessage = new BE.ResponseMessage();
            responseMessage = reportprovider.SaveHoldDtls(HoldData, Userid);
            return Json(responseMessage);
        }

    }
}