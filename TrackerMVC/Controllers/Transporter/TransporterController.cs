using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using DB = TrackerMVCDataLayer.Helper;
using HC = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data.OleDb;

namespace TrackerMVC.Controllers.Transporter
{
    [UserAuthenticationFilter]
    public class TransporterController : Controller
    {
        // GET: Transporter
        BM.Transporter.TransporterDataProvider BL = new BM.Transporter.TransporterDataProvider();
        BM.LoadingPlan.LoadingPlan LP = new BM.LoadingPlan.LoadingPlan();
        // BM.BLDataManager.GenerateBL BL = new BM.BLDataManager.GenerateBL();
        public ActionResult Transporter()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            ViewBag.Checked = 1;

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BL.DeleteDataFromTemp_Transport_Table(Userid);

            List<BE.TransporterBankEntities> TransporterBankList = new List<BE.TransporterBankEntities>();
            BE.TransportEntities BankList = BL.getDropDownList();
            TransporterBankList = BankList.BankList;
            ViewBag.BankList = new SelectList(TransporterBankList, "bankID", "bankname");
            return View();
        }
        [HttpPost]
        public JsonResult AddTempBankData(BE.TransportEntities Tr)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.TransporterBankIEntities JDContainerList = new BE.TransporterBankIEntities();
            JDContainerList = BL.AddContainerData(Tr, Userid);
            return Json(JDContainerList);
        }
        [HttpPost]
        public JsonResult DeleteBankData(string ContainerId)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.TransportEntities> JDContainerList = new List<BE.TransportEntities>();
            JDContainerList = BL.DeleteBankData(ContainerId, Userid);
            return Json(JDContainerList);
        }
        public JsonResult DeleteTempAllRecords()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BL.DeleteDataFromTemp_Transport_Table(Userid);
            return Json("dataDelete");
        }
        [HttpPost]
        public JsonResult Save(BE.TransportEntities Transporter)
        {
            var EntryDate = Transporter.RegDate;

            DB.DBOperations db = new DB.DBOperations();
           int i= db.sub_ExecuteNonQuery("USP_INSERT_TRANSPORTER '" + Transporter.TRANSNAME.Replace("'","''") + "','" + Transporter.EMAILIDT.Replace("'","''") + "','" + Transporter.ADDRESS.Replace("'","''") + "','" + Transporter.MOBILENO + "','" + Transporter.CONTACTPERSON.Replace("'","''") + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + Convert.ToDateTime(EntryDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Transporter.IsActive + "',"+Transporter.TransID+"");
            //Master();

            return Json(i);
        }

        [HttpPost]
        public ActionResult TransporterSummary()
        {
            List<BE.TransportEntities> TransportorList = new List<BE.TransportEntities>();
            TransportorList = BL.getTransporterSummary();
            return PartialView(TransportorList);
            // return Json(CustomerData);
        }

        [HttpPost]
        public ActionResult EditTransportorDetails(string ID)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.TransportData TransportorList = new BE.TransportData();
            TransportorList = BL.getTransporterData(ID, Userid);

            return Json(TransportorList);
        }
        // Codes Start By Prashant
        // Codes Start By Prashant
        public ActionResult TransportExpenses()
        {
            List<BE.TransportExpenses> expensesList = new List<BE.TransportExpenses>();
            expensesList = BL.GettransportDropdownList();
            ViewBag.ExpensesList = new SelectList(expensesList, "transexpenseid", "transexpensename");

            List<BE.PaymentModes> PaymentMode = new List<BE.PaymentModes>();
            PaymentMode = BL.getForPaymentModes();
            ViewBag.PaymentMode = new SelectList(PaymentMode, "PaymentId", "PaymentMode");


            List<BE.VendorMaster> VendorList = new List<BE.VendorMaster>();
            VendorList = BL.GetVendorDetails();
            ViewBag.VendorLists = new SelectList(VendorList, "VendorId", "Name");

            List<BE.SettingTaxdetails> TaxID = new List<BE.SettingTaxdetails>();
            TaxID = BL.GetsettingDetails();
            ViewBag.TaxIDs = new SelectList(TaxID, "TaxID", "Taxname");

            return View();
        }

        public JsonResult getPartyNameReceipt(string prefixText, string Mode)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.VendorMaster> TransportExpenses = new List<BE.VendorMaster>();
            dataTable = db.sub_GetDatatable("USP_vendorListforexpensesTransport '" + Mode + "','" + prefixText + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.VendorMaster ExpensesList = new BE.VendorMaster();
                    ExpensesList.VendorId = Convert.ToInt32(row["Vendor_ID"]);
                    ExpensesList.Name = Convert.ToString(row["Vendor_Name"]);
                    TransportExpenses.Add(ExpensesList);
                }
            }

            var jsonResult = Json(TransportExpenses, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult SaveTransportExpenses(string ExpensesDate, string ExpensesFor, string Amount, string Remark, string Voucherno)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            message = BL.SaveExpensesDetails(ExpensesDate, ExpensesFor, Amount, Remark, Userid, Voucherno);


            return Json(message);
        }


        //public ActionResult GetTransportExpenses(string FromDate, string ToDate)
        //{
        //    List<BE.ExpensesForTransport> expensesList = new List<BE.ExpensesForTransport>();
        //    expensesList = BL.gettransportexpenses(FromDate, ToDate);
        //    var jsonResult = Json(expensesList, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
        //public ActionResult GetTransportExpenses(string FromDate, string ToDate)
        //{
        //    List<BE.ExpensesForTransport> expensesList = new List<BE.ExpensesForTransport>();
        //    expensesList = BL.gettransportexpenses(FromDate, ToDate);
        //    Session["FromDate"] = FromDate;
        //    Session["ToDate"] = ToDate;
        //    var jsonResult = Json(expensesList, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
        public ActionResult CancelTransportExpense(int Entryid, int transexpenseID)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";


            message = BL.CancelexpenseID(Entryid, transexpenseID, Userid);
            return Json(message);
        }

        [HttpPost]
        public JsonResult AjaxVoucherDetails(string Containerno)
        {

            List<BE.ExpensesForTransport> VoucherList = new List<BE.ExpensesForTransport>();
            VoucherList = BL.GetVoucherNumber(Containerno);
            return new JsonResult() { Data = VoucherList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult AjaxOffloadingDetails(string Containerno)
        {

            List<BE.ExpensesForTransport> VoucherList = new List<BE.ExpensesForTransport>();
            VoucherList = BL.GetOffloadingtransdetails(Containerno);
            return new JsonResult() { Data = VoucherList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult AjaxExpensefor()
        {


            List<BE.TransportExpenses> VoucherList = new List<BE.TransportExpenses>();
            VoucherList = BL.GettransportDropdownList();
            return new JsonResult() { Data = VoucherList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        [HttpPost]
        public ActionResult CheckVoucherDetails(string Voucherno, int ExpenseFordata, string ContainerNo ,string Type)
        {
            string message = "";

            message = BL.CheckVoucherno(Voucherno, ExpenseFordata, ContainerNo, Type);

            return Json(message);
        }

        public ActionResult TransporterDashboard()
        {
            List<BE.TransporterEntities> TransporterList = new List<BE.TransporterEntities>();
            TransporterList = BL.GetTransportername();
            ViewBag.Transportername = TransporterList;

            List<BE.DriverEntities> DriverList = new List<BE.DriverEntities>();
            DriverList = BL.GetdriverCount();
            ViewBag.DriverCount = DriverList[0].DriverID;

            List<BE.TrailersEntities> TrailerList = new List<BE.TrailersEntities>();
            TrailerList = BL.GetTrailersCount();
            ViewBag.TrailerCount = TrailerList[0].trailerid;

            List<BE.TrailersEntities> TrailerLocation = new List<BE.TrailersEntities>();
            TrailerLocation = BL.GetTrailersLocation();
            ViewBag.TrailerLocation = TrailerLocation;
            ViewBag.LocationCount = TrailerLocation.Count();


            List<BE.TrailersEntities> TrailerLocationICD = new List<BE.TrailersEntities>();
            TrailerLocationICD = BL.GetTrailersLocationICDTUMB();
            ViewBag.TrailerLocationICD = TrailerLocationICD;
            ViewBag.LocationCountICD = TrailerLocationICD[0].Count;


            List<BE.TrailersEntities> TrailerLocationCount = new List<BE.TrailersEntities>();
            TrailerLocationCount = BL.GetTrailersLocationCount();
            ViewBag.TrailerLocationcount = TrailerLocationCount[0].Count;

            List<BE.TrailersEntities> TrailerGarageCount = new List<BE.TrailersEntities>();
            TrailerGarageCount = BL.GetTrailerGarageCount();
            ViewBag.TrailergarageCount = TrailerGarageCount[0].Count;


            List<BE.TrailersEntities> WothourDrivers = new List<BE.TrailersEntities>();
            WothourDrivers = BL.GetTrailerWihoutDriver();
            ViewBag.DriverWihoutDrivers = WothourDrivers[0].Count;


            List<BE.TrailersEntities> accident = new List<BE.TrailersEntities>();
            accident = BL.Getaccident();
            ViewBag.accidentDetails = accident[0].Count;

            //List<BE.TrailersEntities> BreakDown = new List<BE.TrailersEntities>();
            //BreakDown = BL.GetBreakDown();
            //ViewBag.Breakdown = BreakDown[0].Count;
            List<BE.TrailersEntities> Servicing = new List<BE.TrailersEntities>();
            Servicing = BL.GetServicingDetails();
            ViewBag.Servicing = Servicing[0].Count;

            // Code For Shifting 
            List<BE.TrailersEntities> TrailerShifingLocation = new List<BE.TrailersEntities>();
            TrailerShifingLocation = BL.GetTrailerShifting();
            ViewBag.TrailerShiftingLocation = TrailerShifingLocation[0].Count;


            // Code For Shifting 
            string ALLInsurance = "";
            List<BE.TrailersEntities> RToandInsuranceTracking = new List<BE.TrailersEntities>();
            RToandInsuranceTracking = BL.GetInsuranceANDRTOCount(ALLInsurance);
            ViewBag.InsuranceandRtoTracking = RToandInsuranceTracking.Count;


            // For InsuranceDate count
            string message = "Insurance";
            List<BE.TrailersEntities> Insurance = new List<BE.TrailersEntities>();
            Insurance = BL.GetInsuranceANDRTOCount(message);
            ViewBag.PolicyCount = Insurance.Count;


            // For Tax counts
            string message1 = "Tax";
            List<BE.TrailersEntities> TaxDate = new List<BE.TrailersEntities>();
            TaxDate = BL.GetInsuranceANDRTOCount(message1);
            ViewBag.TaxDate = TaxDate.Count;


            // For Tax counts
            string message2 = "Fitness";
            List<BE.TrailersEntities> Fitness = new List<BE.TrailersEntities>();
            Fitness = BL.GetInsuranceANDRTOCount(message2);
            ViewBag.Fitness = Fitness.Count;

            // For Green Tax
            string message3 = "Green Tax";
            List<BE.TrailersEntities> GreenTax = new List<BE.TrailersEntities>();
            GreenTax = BL.GetInsuranceANDRTOCount(message3);
            ViewBag.GreenTax = GreenTax.Count;


            // For Green Tax
            string message4 = "National Permit Expiry";
            List<BE.TrailersEntities> NationalPermitExpiry = new List<BE.TrailersEntities>();
            NationalPermitExpiry = BL.GetInsuranceANDRTOCount(message4);
            ViewBag.NationalPermitExpiry = NationalPermitExpiry.Count;


            // Code For Shifting 
            List<BE.TrailersEntities> TrailersWithoutDate = new List<BE.TrailersEntities>();
            TrailersWithoutDate = BL.GetWithoutpaper();
            ViewBag.WithoutPaper = TrailersWithoutDate[0].Count;

            // For Green Tax
            string message5 = "Local Permit Expiry";
            List<BE.TrailersEntities> LocalPermitExpiry = new List<BE.TrailersEntities>();
            LocalPermitExpiry = BL.GetInsuranceANDRTOCount(message5);
            ViewBag.LocalPermitExpiry = LocalPermitExpiry.Count;

            List<BE.TrailersEntities> TrailerLocationNavkar = new List<BE.TrailersEntities>();
            TrailerLocationNavkar = BL.GetTrailerLocationCount();

            ViewBag.NavkarLocationCount = TrailerLocationNavkar[0].Count;


            return View();
        }


        //[HttpPost]
        //public JsonResult AjaxTrailerLocation(int LocationID)
        //{



        //    List<BE.TrailersEntities> TrailerLocationList = new List<BE.TrailersEntities>();
        //    TrailerLocationList = BL.GetTrailersLocationlIST(LocationID);
        //    return new JsonResult() { Data = TrailerLocationList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        //}
        [HttpPost]
        public JsonResult AjaxTrailerLocation(int LocationID)
        {



            DataTable GetTrailerTransdate = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetTrailerTransdate = db.sub_GetDatatable("USP_Trailer_Location_AT " + LocationID + "");
            Session["TrailerLocationCount"] = GetTrailerTransdate;
            Session["LocationID"] = LocationID;
            var json = JsonConvert.SerializeObject(GetTrailerTransdate);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;


        }
      


       
        [HttpPost]
        public ActionResult ReExpencePrintDetails(string Entryid)
        {
            DataTable tblGetDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            tblGetDetails = db.sub_GetDatatable("USP_TransportExpensesRePrintFetchDetails '" + Entryid + "'");

            if (tblGetDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in tblGetDetails.Rows)
                {
                    ViewBag.CompanyName = "M/s NAVKAR CORPORATION LTD";
                    ViewBag.Companyaddress = "Admin Office:Survey No 89/93/95/97, Somathane Village,";
                    ViewBag.Companyaddress1 = " Kon-Savla Road, Panvel, Raigad  410 206, Maharashtra, India";
                    ViewBag.Expensesslipno = dr["expenseslipno"];
                    ViewBag.EntryDate = dr["EntryDate1"];
                    ViewBag.VoucherNo = dr["VoucherNo"];
                    ViewBag.TrailerNo = dr["TrailerNo"];
                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.InvoiceDate = dr["InvoiceDate"];
                    ViewBag.VendorName = dr["Vendor_Name"];
                    ViewBag.Username = dr["UserName"];

                }
            }

            ViewBag.InvoiceItemList = tblGetDetails.AsEnumerable();
            return PartialView();
        }







        public ActionResult ExportToExcelexpence()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ExpensesFOr = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpensesFOr = db.sub_GetDatatable("USP_TransportExpensesFetchDetails '" + Session["FromDate"] + "','" + Session["ToDate"] + "','" + Userid + "'");
            Session["TransportExpensesFetchDetails"] = ExpensesFOr;
            DataTable dt = (DataTable)Session["TransportExpensesFetchDetails"];


            DataTable CompanyMaster = new DataTable();
            // HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            string Tittle = "From " + Session["FromDate"] + " To " + Session["ToDate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=TransportExpenceReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Transport Expenses Report <strong></td></tr>");
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



        public ActionResult ExpencePrintDetails()
        {


            DataTable tblGetDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            tblGetDetails = db.sub_GetDatatable("USP_TransportExpensesPrintFetchDetails");

            if (tblGetDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in tblGetDetails.Rows)
                {
                    ViewBag.CompanyName = "M/s NAVKAR CORPORATION LTD";
                    ViewBag.Companyaddress = "Admin Office:Survey No 89/93/95/97, Somathane Village,";
                    ViewBag.Companyaddress1 = "Kon-Savla Road, Panvel, Raigad  410 206, Maharashtra, India";
                    ViewBag.Expensesslipno = dr["expenseslipno"];
                    ViewBag.EntryDate = dr["EntryDate1"];
                    ViewBag.VoucherNo = dr["VoucherNo"];
                    ViewBag.TrailerNo = dr["TrailerNo"];
                    ViewBag.InvoiceNo = dr["InvoiceNo"];
                    ViewBag.InvoiceDate = dr["InvoiceDate"];
                    ViewBag.VendorName = dr["Vendor_Name"];
                    ViewBag.Username = dr["UserName"];

                }
            }

            ViewBag.InvoiceItemList = tblGetDetails.AsEnumerable();
            return PartialView();
        }




        // Codes End By Prashant


        // 21/11/2019 

        [HttpPost]
        //public ActionResult SaveTransportData(List<BE.ExpensesForTransport> Invoicedata)
        //{


        //    DataTable dataTable = new DataTable();

        //    dataTable.Columns.Add("VoucherNumber");
        //    dataTable.Columns.Add("expenseFor");
        //    dataTable.Columns.Add("TrailerNo");
        //    dataTable.Columns.Add("ContainerNo");
        //    dataTable.Columns.Add("Size");
        //    dataTable.Columns.Add("Activity");
        //    dataTable.Columns.Add("Amount");
        //    dataTable.Columns.Add("Remarks");
        //    dataTable.Columns.Add("PaymentMode");


        //    dataTable.TableName = "PT_ExpensesEntry";


        //    foreach (BE.ExpensesForTransport item in Invoicedata)
        //    {
        //        DataRow row = dataTable.NewRow();

        //        row["VoucherNumber"] = item.VoucherNumber;
        //        row["expenseFor"] = item.expenseFor;
        //        row["TrailerNo"] = item.TrailerNo;
        //        row["ContainerNo"] = item.ContainerNo;
        //        row["Size"] = item.Size;
        //        row["Activity"] = item.Activity;
        //        row["Amount"] = item.Amount;
        //        row["Remarks"] = item.Remarks;
        //        row["PaymentMode"] = item.PaymentMode;
        //        dataTable.Rows.Add(row);
        //    }

        //    int Userid = Convert.ToInt32(Session["Tracker_userID"]);
        //    string message = BL.SaveTransportExpenses(dataTable, Userid);
        //    return Json(message);

        //}
        public ActionResult SaveTransportData1(List<BE.ExpensesForTransport> Invoicedata, string InvoiceDate, string InvoiceNo,string ddlGSTNo)
        {


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("VoucherNumber");
            dataTable.Columns.Add("expenseFor");
            dataTable.Columns.Add("TrailerNo");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("Amount");
            dataTable.Columns.Add("Remarks");
            dataTable.Columns.Add("PaymentMode");
            dataTable.Columns.Add("vendorID");
            dataTable.Columns.Add("Taxid");
            dataTable.Columns.Add("CGST");
            dataTable.Columns.Add("SGST");
            dataTable.Columns.Add("IGST");
            dataTable.Columns.Add("GrandTotal");
            dataTable.TableName = "PT_ExpensesEntry";


            foreach (BE.ExpensesForTransport item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["VoucherNumber"] = item.VoucherNumber;
                row["expenseFor"] = item.expenseFor;
                row["TrailerNo"] = item.TrailerNo;
                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Activity"] = item.Activity;
                row["Amount"] = item.Amount;
                row["Remarks"] = item.Remarks;
                row["PaymentMode"] = item.PaymentMode;
                row["vendorID"] = item.vendorID;
                row["Taxid"] = item.Taxid;
                row["CGST"] = item.CGST;
                row["SGST"] = item.SGST;
                row["IGST"] = item.IGST;
                row["GrandTotal"] = item.GrandTotal;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = BL.SaveTransportExpenses(dataTable, InvoiceDate, InvoiceNo, Userid, ddlGSTNo);
            return Json(message);

        }
        public ActionResult GetTransportExpenses(string FromDate, string ToDate)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            List<BE.ExpensesForTransport> expensesList = new List<BE.ExpensesForTransport>();
            expensesList = BL.gettransportexpenses(FromDate, ToDate, Userid);
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            var jsonResult = Json(expensesList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public ActionResult EditExpenseHead(string entryid, string transexpenseID)
        {
            List<BE.ExpensesForTransport> expensesList = new List<BE.ExpensesForTransport>();
            expensesList = BL.GetEditExpenseHead(entryid, transexpenseID);

            var jsonResult = Json(expensesList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        [HttpPost]
        public ActionResult EditSaveTransportData(List<BE.ExpensesForTransport> Invoicedata, int Editentryid, int Activityid, int ExpensesID, string ExpensesDate)
        {


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("VoucherNumber");
            dataTable.Columns.Add("expenseFor");
            dataTable.Columns.Add("TrailerNo");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("Amount");
            dataTable.Columns.Add("Remarks");
            dataTable.Columns.Add("Entryid");
            dataTable.Columns.Add("PaymentMode");
            dataTable.Columns.Add("vendorID");
            dataTable.Columns.Add("Taxid");
            dataTable.Columns.Add("CGST");
            dataTable.Columns.Add("SGST");
            dataTable.Columns.Add("IGST");
            dataTable.Columns.Add("GrandTotal");
            dataTable.Columns.Add("Invocieno");

            dataTable.TableName = "PT_EditExpensesEntry";


            foreach (BE.ExpensesForTransport item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["VoucherNumber"] = item.VoucherNumber;
                row["expenseFor"] = item.expenseFor;
                row["TrailerNo"] = item.TrailerNo;
                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Activity"] = item.Activity;
                row["Amount"] = item.Amount;
                row["Remarks"] = item.Remarks;
                row["Entryid"] = item.Entryid;
                row["PaymentMode"] = item.PaymentMode;
                row["vendorID"] = item.vendorID;
                row["Taxid"] = item.Taxid;
                row["CGST"] = item.CGST;
                row["SGST"] = item.SGST;
                row["IGST"] = item.IGST;
                row["GrandTotal"] = item.GrandTotal;
                row["Invocieno"] = item.InvoiceNo;
                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = BL.EditSaveTransportExpenses(dataTable, Userid, Editentryid, Activityid, ExpensesID, ExpensesDate);
            return Json(message);

        }

        public ActionResult GetVendorGSTDetails(string VendorID)
        {
            List<BE.GstDetails> expensesList = new List<BE.GstDetails>();
            expensesList = BL.GetVendorGSTDetails(VendorID);

            var jsonResult = Json(expensesList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult GetGSTDetailsForCalculate(string TaxID)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.GSTReturnEntities GetGST = new BE.GSTReturnEntities();
            dt = db.sub_GetDatatable("USP_getGSTtaxID '" + TaxID + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    GetGST.SGST = Convert.ToString(row["SGST"]);
                    GetGST.CGST = Convert.ToString(row["CGst"]);
                    GetGST.IGST = Convert.ToString(row["igst"]);
                }
            }

            return Json(GetGST);
        }

        public JsonResult SaveTransportExpensesAttachmentToDirectory(BE.ExpensesForTransport fileData)
        {
            int Userid = Convert.ToInt16(Session["Tracker_userID"]);
            HC.DBOperations db = new HC.DBOperations();
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string fname1;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        string contentType;


                        Stream fs = Request.Files[i].InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        contentType = MimeMapping.GetMimeMapping(fname);
                        string MSNO = Convert.ToString(fileData.Entryid);


                        //int DocID = fileData.DocID;



                        string root = Path.Combine(Server.MapPath("~/ExpensesDocumentDocs/"), MSNO);
                        string PathSave = "~/ExpensesDocumentDocs/" + MSNO + "/" + fname;
                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        fname1 = Convert.ToString(fname);
                        fname = Path.Combine(Server.MapPath("~/ExpensesDocumentDocs/" + MSNO + "/"), fname);
                        //fname = Path.Combine(root, "/" + fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(fname);
                        if (System.IO.File.Exists(fname))
                        {
                            db.sub_ExecuteNonQuery("USP_INSERT_Transport_Expenses_DOCS " + MSNO + ",'" + PathSave + "'," + Userid + ",'" + fname1 + "','" + contentType + "'");
                            return Json("");
                        }
                        else
                        {
                            return Json("Document not saved successfully!");
                        }
                    }
                    return Json(1);
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

        public ActionResult ShowExpensesattachments(int Entryid)
        {


            List<BE.TransportexpensesAttachment> JOAttachmentList = new List<BE.TransportexpensesAttachment>();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            JOAttachmentList = BL.GetTransportExpensesDoc(Entryid);
            var jsonResult = Json(JOAttachmentList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult SaveExpenseName(string expensename)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";

            
                message = BL.CheckUservalidate(Userid);

            if (message == "1")
            {
                message = BL.SaveExpenseName(expensename);
            }
            else
            {
                message = "Please Check For The Rights!";
            }
          


            return Json(message);
        }


        public ActionResult DeleteExpenseHead(int Entryid, int transexpenseID)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";


            message = BL.DeleteExpenseID(Entryid, transexpenseID);
            return Json(message);
        }


        //code By Prashant

        // Code By Trailer Shifting Details

        [HttpPost]
        public JsonResult GetTrailerShiftingDetails()
        {
            DataTable GetTrailerTransdate = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetTrailerTransdate = db.sub_GetDatatable("USP_GetTrailerShiftingDetails");
            Session["TrailerShiftingDetails"] = GetTrailerTransdate;
            var json = JsonConvert.SerializeObject(GetTrailerTransdate);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToexcelGetTrailerShiftingDetails()
        {
            DataTable dt = (DataTable)Session["TrailerShiftingDetails"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=TrailerShiftingDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Trailer Shifting Details Report <strong></td></tr>");
                    // htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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



        [HttpPost]
        public JsonResult GetallInsuranceDetailsList(string searchcriteria)
        {
            DataTable GetTrailerTransdate = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetTrailerTransdate = db.sub_GetDatatable("USP_GetInsuranceandRtoDetails '" + searchcriteria + "'");
            Session["GetallinsuranceDetails"] = GetTrailerTransdate;
            var json = JsonConvert.SerializeObject(GetTrailerTransdate);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelInsureanceDetails()
        {
            DataTable dt = (DataTable)Session["GetallinsuranceDetails"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Insurance and Rto Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Insurance and Rto Details Report <strong></td></tr>");
                    // htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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



        [HttpPost]
        public JsonResult GetwithoutpaperDetails()
        {
            DataTable GetTrailerTransdate = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetTrailerTransdate = db.sub_GetDatatable("USP_GetWithPaperDriversList");
            Session["Withoutpaper"] = GetTrailerTransdate;
            var json = JsonConvert.SerializeObject(GetTrailerTransdate);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelWithoutPaper()
        {
            DataTable dt = (DataTable)Session["Withoutpaper"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Trailers Wihtout Paper.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Trailers Wihtout Paper Report <strong></td></tr>");
                    // htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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


        [HttpPost]
        public JsonResult GetGarageDetails()
        {



            DataTable GetTrailerTransdate = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetTrailerTransdate = db.sub_GetDatatable("USP_Trailer_Location_AT_Garage");
            Session["Trailergaragecount"] = GetTrailerTransdate;
            var json = JsonConvert.SerializeObject(GetTrailerTransdate);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;


        }


        public ActionResult ExportToexcelGaregecount()
        {
            DataTable dt = (DataTable)Session["Trailergaragecount"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=GarageDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Garege Details Summary Report <strong></td></tr>");
                    // htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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


        [HttpPost]
        public JsonResult GetAccidentDetails()
        {
            DataTable GetTrailerTransdate = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetTrailerTransdate = db.sub_GetDatatable("USP_AccidentDetsforSearch_Report_list");
            Session["AccidentList"] = GetTrailerTransdate;
            var json = JsonConvert.SerializeObject(GetTrailerTransdate);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToexcelGetAccidentDetails()
        {
            DataTable dt = (DataTable)Session["AccidentList"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=AccidentDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Accident Details Summary Report <strong></td></tr>");
                    // htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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


        [HttpPost]
        public JsonResult GetServicingDetails()
        {
            DataTable GetTrailerTransdate = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetTrailerTransdate = db.sub_GetDatatable("USP_GetServicingDetails");
            Session["ServicingDetails"] = GetTrailerTransdate;
            var json = JsonConvert.SerializeObject(GetTrailerTransdate);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToexcelGetServicingDetails()
        {
            DataTable dt = (DataTable)Session["ServicingDetails"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ServicingDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Servicing Details Summary Report <strong></td></tr>");
                    // htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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
        public ActionResult ExportToEListOfVehicleWithoutEntries()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["GetListOfvehicleWithoutDrivers"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ListOfVehicleWihoutDrivers.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Vehicle Without Drivers Report<strong></td></tr>");
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


        public ActionResult TransportListOFVehicle()
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetListOFTrailerWithoutVehicle");
            Session["GetListOfvehicleWithoutDrivers"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult VehiclesLocation()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AjaxGetLocationWiseVechile()
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GET_LocationWiseVechile");
            Session["LocationWiseVechile"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public ActionResult ExportToExcel()
        {
            DataTable dt = (DataTable)Session["LocationWiseVechile"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=LocationWiseVechiles.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Location Wise vehicles Summary Report <strong></td></tr>");
                    // htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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



        [HttpPost]
        public ActionResult AjaxGetIdleVechile(string VehicleGroup)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GET_IdleWiseVechile '" + VehicleGroup + "'");
            Session["IdleVechile"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }



        public ActionResult FasterMaster()
        {


            return View();
        }


        [HttpPost]
        public ActionResult SaveFastagDetails(string trailerid, string tagno, string remakrs, int chkIsActive)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.SavefastagNoDetails(trailerid, tagno, remakrs, chkIsActive, Userid);

            return Json(message);
        }



        [HttpPost]
        public ActionResult ChecktagNoExists(string trailerid, string tagno, string remakrs, int chkIsActive)
        {
            string message = "";

            message = BL.CheckFastagNo(trailerid, tagno, remakrs, chkIsActive);

            return Json(message);
        }


        public ActionResult GetFasttagDetails()
        {
            List<BE.FastagMasterEntities> expensesList = new List<BE.FastagMasterEntities>();
            expensesList = BL.GetFastagMaster();

            var jsonResult = Json(expensesList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public ActionResult EditFastagDetails(string EditEntryid, string EditTagNo, int chkIsActive)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.EditFastagDetails(EditEntryid, EditTagNo, chkIsActive, Userid);

            return Json(message);
        }

        public ActionResult FastagTariffSetting()
        {
            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");

            List<BE.TollSummary> Toll = new List<BE.TollSummary>();
            Toll = BL.getTollLocation();
            ViewBag.TollLocation = new SelectList(Toll, "TollID", "TollName");

            return View();
        }


        [HttpPost]
        public ActionResult SaveFastagSetting(int fromlocation, int Tolocation, int Toll, int amount, string Passing)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.GetfastagtariffDetails(fromlocation, Tolocation, Toll, amount, Passing, Userid);

            return Json(message);
        }


        [HttpPost]
        public ActionResult CheckTariffexist(int fromlocation, int Tolocation, int Toll, int amount, string Passing)
        {
            string message = "";

            message = BL.Checkfastagtariffalready(fromlocation, Tolocation, Toll, amount, Passing);

            return Json(message);
        }


        public ActionResult GetFasttagtariffdetails()
        {
            List<BE.FastagMasterEntities> expensesList = new List<BE.FastagMasterEntities>();
            expensesList = BL.GetFastagtariffDetails();

            var jsonResult = Json(expensesList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public ActionResult GetfastagsummaryDetails1()
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetFastagTollLocationDetails");
            Session["FastagSummaryDetails"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelFastag()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["FastagSummaryDetails"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=FasTag Summary Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Fastag Summary Report<strong></td></tr>");
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



        public ActionResult TollMaster()
        {
            List<BE.TollSummary> Toll = new List<BE.TollSummary>();
            Toll = BL.getTollLocation();
            ViewBag.TollLocation = new SelectList(Toll, "TollID", "TollName");
            return View();
        }


        [HttpPost]
        public ActionResult SaveTollMasterDetails(string TollName, int OngoingAmount, int Returnamount, string Passing)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.SaveTollDetails(TollName, OngoingAmount, Returnamount, Passing, Userid);

            return Json(message);
        }


        public ActionResult GetTollMasterDetails()
        {
            List<BE.TollSummary> expensesList = new List<BE.TollSummary>();
            expensesList = BL.GetTollDetails();

            var jsonResult = Json(expensesList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public ActionResult EditTollMasterDetails(string TollName, int OngoingAmount, int Returnamount, int TollID, string PassingEdit)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.EditTollDetails(TollName, OngoingAmount, Returnamount, TollID, PassingEdit, Userid);

            return Json(message);
        }


        [HttpPost]
        public ActionResult AjaxGetTollAmount(int TollID, string Passing)
        {
            string message = "";

            message = BL.GetTrollAmount(TollID, Passing);

            return Json(message);
        }


        [HttpPost]
        public ActionResult CheckTollName(string TollNames, string Passing)
        {
            string message = "";

            message = BL.ChecktollName(TollNames, Passing);

            return Json(message);
        }


        public ActionResult SaveTollName(string Tollname)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";


            message = BL.CheckUservalidate(Userid);

            if (message == "1")
            {
                message = BL.SaveTollName(Tollname);
            }
            else
            {
                message = "Please Check For The Rights!";
            }

            return Json(message);
        }


        [HttpPost]
        public JsonResult AjaxTollefor()
        {


            List<BE.TollSummary> VoucherList = new List<BE.TollSummary>();
            VoucherList = BL.getTollLocation();
            return new JsonResult() { Data = VoucherList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public ActionResult CancelDetails(int entryid)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.CanceltariffDetails(entryid);

            return Json(message);
        }

        //code End By Prashant
        // Fastg Reco
        public ActionResult FastagRecoDetails()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            return View();
        }
        public JsonResult ImportFile()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            BE.FastagDetailsEntries DriverPaymentList = new BE.FastagDetailsEntries();
            List<BE.FastagDetailsEntries> DriverPaymentRecoList = new List<BE.FastagDetailsEntries>();
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
                        fname = Path.Combine(Server.MapPath("~/ImportFileForfastag/"), fname);
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


                                    DriverPaymentDT.Columns.Add("Transaction Date Time");
                                    DriverPaymentDT.Columns.Add("Processed Date Time");
                                    DriverPaymentDT.Columns.Add("Licence Plate No");
                                    DriverPaymentDT.Columns.Add("Tag Account No.");
                                    DriverPaymentDT.Columns.Add("Group");
                                    DriverPaymentDT.Columns.Add("Activity");
                                    DriverPaymentDT.Columns.Add("Plaza Code");
                                    DriverPaymentDT.Columns.Add("Transaction Description");
                                    DriverPaymentDT.Columns.Add("Unique Transaction ID");
                                    DriverPaymentDT.Columns.Add("Amount(CR)");
                                    DriverPaymentDT.Columns.Add("Amount(DR)");
                                    //DriverPaymentDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        if (row[0].ToString() != "" && row[1].ToString() != "")
                                        {

                                            DataRow dar = DriverPaymentDT.NewRow();

                                            dar["Transaction Date Time"] = row[0].ToString();
                                            dar["Processed Date Time"] = row[1].ToString();
                                            dar["Licence Plate No"] = row[2].ToString();
                                            dar["Tag Account No."] = row[3].ToString();
                                            dar["Group"] = row[4].ToString();
                                            dar["Activity"] = row[5].ToString();
                                            dar["Plaza Code"] = row[6].ToString();
                                            dar["Transaction Description"] = row[7].ToString();
                                            dar["Unique Transaction ID"] = row[8].ToString();
                                            dar["Amount(CR)"] = row[9].ToString();
                                            dar["Amount(DR)"] = row[10].ToString();
                                            DriverPaymentDT.Rows.Add(dar);
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

                                    if (DriverPaymentDT != null)
                                    {

                                        DriverPaymentRecoList = BL.UpdateTransportRecoDetails(DriverPaymentDT, Userid);

                                        //if (message == "Records updated successfully")
                                        //{
                                        //    message = "Records imported and updated successfully";
                                        //}
                                        //   trainTrackerProvider.UpdateDischargeDateJO(DischargeListDT, userId);
                                        //  DischargeList = trainTrackerProvider.UpdateDischargeDateJO(DischargeListDT, Userid);
                                        var jsonResult = Json(DriverPaymentRecoList, JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;
                                        //var returnField = new { Issuedata = 1, message = message };
                                        //return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                                    }
                                }
                            }
                        }
                    }
                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    //  Console.WriteLine("Error occurred. Error details: " + ex.Message);
                    //  BL.AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'",""));
                    // return Json("Error occurred. Error details: " + ex.Message);
                    return Json(1);
                    //  return Json("FormData is not supported.");
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        [HttpPost]
        public ActionResult SavePaymentList(List<BE.FastagDetailsEntries> Paymentdata, DateTime entrydate)
        {

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("TransactionDateTime");
            dataTable.Columns.Add("ProcessedDateTime");
            dataTable.Columns.Add("LicencePlateNo");
            dataTable.Columns.Add("TagAccountNo");
            dataTable.Columns.Add("Group");
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("PlazaCode");
            dataTable.Columns.Add("TransactionDescription");
            dataTable.Columns.Add("UniqueTransactionID");
            dataTable.Columns.Add("AmountCR");
            dataTable.Columns.Add("AmountDR");

            dataTable.TableName = "PT_FastagSaveDetails";
            foreach (BE.FastagDetailsEntries item in Paymentdata)
            {


                DataRow row = dataTable.NewRow();

                //   row["TransactionDateTime"] = item.TransactionDateTime;
                row["TransactionDateTime"] = Convert.ToDateTime(item.TransactionDateTime).ToString("yyyy-MM-dd HH:mm:ss");
                // row["ProcessedDateTime"] = item.ProcessedDateTime;
                row["ProcessedDateTime"] = Convert.ToDateTime(item.ProcessedDateTime).ToString("yyyy-MM-dd HH:mm:ss");
                row["LicencePlateNo"] = item.LicencePlateNo;
                row["TagAccountNo"] = item.TagAccountNo;
                row["Group"] = item.Group;
                row["Activity"] = item.Activity;
                row["PlazaCode"] = item.PlazaCode;
                row["TransactionDescription"] = item.TransactionDescription;
                row["UniqueTransactionID"] = item.UniqueTransactionID;
                row["AmountCR"] = item.AmountCR;
                row["AmountDR"] = item.AmountDR;

                dataTable.Rows.Add(row);
            }
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            BE.FastagDetailsEntries PaymentList = new BE.FastagDetailsEntries();
            PaymentList = BL.SavePaymentList(dataTable, UserID, entrydate);
            return new JsonResult() { Data = PaymentList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


       
        //public JsonResult GetFastagDetailsForReco(string FromDate, string ToDate)
        //{
        //    DataTable GetTrailerTransdate = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
        //    GetTrailerTransdate = db.sub_GetDatatable("USP_GetFastagRecoDetails '" + FromDate + "','" + ToDate + "'");
        //    Session["FastagRecoDetailssummary"] = GetTrailerTransdate;
        //    var json = JsonConvert.SerializeObject(GetTrailerTransdate);
        //    var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}

        public ActionResult ExportToexcelfastagDEtails()
        {
            DataTable dt = (DataTable)Session["FastagRecoDetailssummary"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);

            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=FastagDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Fastag Details Summary<strong></td></tr>");
                    // htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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


        [HttpPost]
        public JsonResult GetFastagSummaryDetails(string FromDate, string ToDate)
        {
            DataTable GetTrailerTransdate = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetTrailerTransdate = db.sub_GetDatatable("USP_GetFastagrecoSummaryDetails '" + FromDate + "','" + ToDate + "'");
            Session["FastagRecoDetailssummary"] = GetTrailerTransdate;
            var json = JsonConvert.SerializeObject(GetTrailerTransdate);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult CancelFastagDetails(List<BE.FastagDetailsEntries> SlipNoList)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("UniqueTransactionID");
            dataTable.Columns.Add("Voucherno");

            //int Count = 1;
            foreach (BE.FastagDetailsEntries item in SlipNoList)
            {
                DataRow row = dataTable.NewRow();

                row["UniqueTransactionID"] = item.UniqueTransactionID;
                row["VoucherNo"] = item.VoucherNo;

                dataTable.Rows.Add(row);
            }
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            message = BL.CancelfastagUploadedDetails(dataTable, Userid);

            return Json(message, JsonRequestBehavior.AllowGet);

            // return Json(1);
        }
        [HttpPost]
        public JsonResult GetFastagDetailsForReco(string FromDate, string ToDate)
        {
            DataTable GetTrailerTransdate = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetTrailerTransdate = db.sub_GetDatatable("USP_GetFastagRecoDetails '" + FromDate + "','" + ToDate + "'");






            //List<BE.FastagDetailsEntries> DriverPaymentList = new List<BE.FastagDetailsEntries>();
            //if (GetTrailerTransdate != null)
            //{
            //    int count = 1;
            //    foreach (DataRow row in GetTrailerTransdate.Rows)
            //    {
            //        BE.FastagDetailsEntries DPDTList = new BE.FastagDetailsEntries();

            //        DPDTList.VoucherNo = Convert.ToString(row["Voucher No"]);
            //        DPDTList.VoucherDate = Convert.ToString(row["Voucher Date"]);
            //        DPDTList.ProcessedDateTime = Convert.ToString(row["Processed Date Time"]);
            //        DPDTList.TransactionDateTime = Convert.ToString(row["Transaction Date Time"]);
            //        DPDTList.LicencePlateNo = Convert.ToString(row["Licence Plate No"]);
            //        DPDTList.TagAccountNo = Convert.ToString(row["Tag Account No"]);
            //        DPDTList.Activity = Convert.ToString(row["Activity"]);
            //        DPDTList.PlazaCode = Convert.ToString(row["Plaza Code"]);
            //        DPDTList.TransactionDescription = Convert.ToString(row["Transaction Description"]);
            //        DPDTList.UniqueTransactionID = Convert.ToString(row["Unique Transaction ID"]);

            //        DPDTList.AmountCR = Convert.ToString(row["Amount CR"]);
            //        DPDTList.AmountDR = Convert.ToString(row["Amount DR"]);
            //        DPDTList.FASTAGamt = Convert.ToString(row["Fastag amount"]);
            //        DPDTList.fromlocation = Convert.ToString(row["From Location"]);
            //        DPDTList.Tolocation = Convert.ToString(row["To Location"]);






            //        DriverPaymentList.Add(DPDTList);
            //    }
            //}



            var json = JsonConvert.SerializeObject(GetTrailerTransdate);
            GetTrailerTransdate.Columns.Remove("Cancel");
            Session["FastagRecoDetailssummary"] = GetTrailerTransdate;
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult FastagValidationValidation(List<BE.FastagDetailsEntries> Paymentdata)
        {
            string message = "";
            string message1 = "";

            List<BE.FastagDetailsEntries> Validation = new List<BE.FastagDetailsEntries>();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("UniqueTransactionID");
            dataTable.Columns.Add("TransactionDateTime");


            dataTable.TableName = "PT_FastagSaveDetails";
            foreach (BE.FastagDetailsEntries item in Paymentdata)
            {


                DataRow row = dataTable.NewRow();

                row["UniqueTransactionID"] = item.UniqueTransactionID;
                row["TransactionDateTime"] = Convert.ToDateTime(item.TransactionDateTime).ToString("yyyy-MM-dd HH:mm:ss");


                dataTable.Rows.Add(row);
            }

            message = BL.fatsgDetailsValidation(dataTable);


            return new JsonResult() { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }


    }
}