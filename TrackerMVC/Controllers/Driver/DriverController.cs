using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using DB = TrackerMVCDataLayer.Helper;
using DP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrailerTransport;

using HC = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace TrackerMVC.Controllers.Driver
{
    [UserAuthenticationFilter]
    public class DriverController : Controller
    {
        HC.DBOperations db = new HC.DBOperations();
        BM.Driver.DriverDataProvider BL = new BM.Driver.DriverDataProvider();
        DP.TrailerDataProvider objTrailerProvider = new DP.TrailerDataProvider();
        // GET: Driverss
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DriverSummary()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            ViewBag.Checked = 1;
            BM.LoadingPlan.LoadingPlan LP = new BM.LoadingPlan.LoadingPlan();
            //int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            //BM.DeleteDataFromTemp_Transport_Table(Userid);
            List<BE.TransporterEntities> Transpoter = new List<BE.TransporterEntities>();
            Transpoter = LP.getDriverTranspoter();



            List<BE.DriverBankEntities> TransporterBankList = new List<BE.DriverBankEntities>();
            BE.DriverEntities BankList = BL.getDropDownList();
            TransporterBankList = BankList.BankList;
            ViewBag.BankList = new SelectList(TransporterBankList, "bankID", "bankname");
            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");

            List<BE.DriverDropDownEntites> DLtype = new List<BE.DriverDropDownEntites>();
            DLtype = BL.GetDLtypeList();
            ViewBag.DLtype = new SelectList(DLtype, "DLID", "Dltype");

            List<BE.DriverDropDownEntites> Equipement = new List<BE.DriverDropDownEntites>();
            Equipement = BL.GetEquipment();
            ViewBag.Equipement = new SelectList(Equipement, "ID", "Equipment");


            List<BE.DriverDropDownEntites> InsuranceCompany = new List<BE.DriverDropDownEntites>();
            InsuranceCompany = BL.GetInsuranceCompany();
            ViewBag.InsuranceCompany = new SelectList(InsuranceCompany, "InsuranceID", "InsuranceCompany");

            List<BE.DriverDropDownEntites> EmployeeType = new List<BE.DriverDropDownEntites>();
            EmployeeType = BL.GetEmployeetype();
            ViewBag.EmployeeType = new SelectList(EmployeeType, "Emp_Type_ID", "Emp_Type");

            List<BE.VehicleTypeEntities> VehicleTypeList = new List<BE.VehicleTypeEntities>();
            BE.TrailerEntities TrailerList = objTrailerProvider.getDropDownList();
            VehicleTypeList = TrailerList.VehicleTypeList;
            ViewBag.VehicleTypeList = new SelectList(VehicleTypeList, "VehicleTypeID", "VehicleType");

            List<BE.DriverDocumentTypeEntites> DriverDocumenttype = new List<BE.DriverDocumentTypeEntites>();
            DriverDocumenttype = BL.GetDriverdocumenttype();
            ViewBag.DocumentType = new SelectList(DriverDocumenttype, "DocumentID", "DocumentType");

            List<BE.DriverTypeEntities> Drivertype = new List<BE.DriverTypeEntities>();
            Drivertype = LP.getDriverTypeDetails();
            ViewBag.Driver = new SelectList(Drivertype, "DriverID", "DriverType");


            return View();
        }





        public ActionResult ajaxDriverSaveDetails(BE.DriverEntities Driver)
        {


            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            string DriverName = Driver.DriverName;
            bool IsActive = Driver.IsActive;
            long DriverID = Driver.DriverID;
            int TranspoterID = Driver.TransporterID;

            string DlNo = Driver.DlNo;
            string DlType = Driver.DlType;
            string DlDate = Driver.DlDate;
            string DLExpityDate = Driver.DLExpityDate;
            string EquipmentType = Driver.EquipmentType;
            string DContactNo = Driver.DContactNo;

            string Insuranceno = Driver.Insuranceno;
            string InsuranceCompany = Driver.InsuranceCompany;
            string Referenceby = Driver.Referenceby;
            string ReferenceContactno = Driver.ReferenceContactno;
            string CurrentAddress = Driver.CurrentAddress;
            string PermanentAddress = Driver.PermanentAddress;
            string chkIsBlackList = Driver.chkIsBlackList;
            string TxtRemakrs = Driver.TxtRemakrs;
            string Vehicletype = Driver.Vehicletype;
            string BankName = Driver.BankName;
            string BankID = Driver.BankID;
            string AccountNo = Driver.AccountNo;
            string IFSCCode = Driver.IFSCCode;
            string AccountName = Driver.AccountName;
            string BranchName = Driver.BranchName;
            string PaymentMode = Driver.PaymentMode;
            string Employetype = Driver.Employetype;
            string AdharNumber = Driver.AdharNumber;
            string Pannumber = Driver.Pannumber;
            string Drivertype = Driver.Drivertype;
            string DOBDATE = Driver.DOBDATE;

            string message = BL.AddDriversDetails(DriverName, IsActive, DriverID, Userid, BankName, BankID, AccountNo, IFSCCode, AccountName, BranchName, PaymentMode,
                TranspoterID, DlNo, DlType, DlDate, DLExpityDate, EquipmentType, DContactNo, Insuranceno, InsuranceCompany, Referenceby, ReferenceContactno,
                CurrentAddress, PermanentAddress, chkIsBlackList, TxtRemakrs, Vehicletype, Employetype, AdharNumber, Pannumber, Drivertype, DOBDATE);


            return Json(message);
            //
            //return RedirectToAction("Index", "MyController");
        }

        //public ActionResult ajaxDriverSaveDetails(BE.DriverEntities Driver)
        //{


        //    int Userid = Convert.ToInt32(Session["Tracker_userID"]);

        //    string DriverName = Driver.DriverName;
        //    bool IsActive = Driver.IsActive;
        //    long DriverID = Driver.DriverID;
        //    string BankName = Driver.BankName;
        //    string BankID = Driver.BankID;
        //    string AccountNo = Driver.AccountNo;
        //    string IFSCCode = Driver.IFSCCode;
        //    string AccountName = Driver.AccountName;
        //    string BranchName = Driver.BranchName;

        //    string PaymentMode = Driver.PaymentMode;
        //    int TranspoterID = Driver.TransporterID;

        //    string message = BL.AddDriversDetails(DriverName, IsActive, DriverID, Userid, BankName, BankID, AccountNo, IFSCCode, AccountName, BranchName, PaymentMode, TranspoterID);


        //    return Json(message);
        //    //
        //    // return RedirectToAction("Index", "MyController");
        //}




        [HttpPost]
        public ActionResult CheckDrivername(string DriverName)
        {
            string message = "";
            message = BL.checkUser(DriverName);

            return Json(message);
        }

        [HttpPost]
        public ActionResult getDriver()
        {
            List<BE.DriversummaryEntiites> MovementAtICDList = new List<BE.DriversummaryEntiites>();
            MovementAtICDList = BL.GetDrivers();

            var jsonResult = Json(MovementAtICDList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        // code by Prashant on 21 sept 2019
        public ActionResult UploadedDriverattachment()
        {


            return PartialView();
        }


        public ActionResult HoldPaymentDetails()
        {


            return View();
        }

        [HttpPost]
        public ActionResult getDriverBankDetails(string driverid)
        {
            List<BE.DriverEntities> Driverpayment = new List<BE.DriverEntities>();
            Driverpayment = BL.GetDriverDetails(driverid);

            var jsonResult = Json(Driverpayment, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public ActionResult AjaxGetholdDriverid(string holdDriverid, string Remakrs)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            string message = BL.GetDriveridholding(holdDriverid, Userid, Remakrs); ;
            return Json(message);
        }
        public ActionResult GetDriverHoldEntries()
        {
            List<BE.DriverEntities> upcomingList = new List<BE.DriverEntities>();
            upcomingList = BL.GetDriverHoldDetails();

            var jsonResult = Json(upcomingList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //Codes end by prashant
        public ActionResult DriverDetailsForApprove()
        {
            List<BE.DriverEntities> DriverlistList = new List<BE.DriverEntities>();
            DriverlistList = BL.GetDriverApproveDetails();
            return View(DriverlistList);
        }

        public ActionResult AJaxGetDriverDetailsApprove(int driverid, string trailername)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.GetDriveridApproveTrailerDetails(driverid, trailername, Userid);
            return Json(message);
        }


        [HttpPost]
        public JsonResult UploadDriverAttachment()
        {
            List<BE.DriversDocumentDetailsEntiites> JOAttachmentList = new List<BE.DriversDocumentDetailsEntiites>();
            if (Request.Files.Count > 0)
            {
                try
                {
                    int Userid = Convert.ToInt16(Session["Tracker_userID"]);
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;
                        string root = Server.MapPath("~/DriverDocumentDocs/");
                        //string root = Path.Combine(Server.MapPath("~/MeetingScheduleDocs/"), MSNO);
                        //string PathSave = "~/MeetingScheduleDocs/" + MSNO + "/" + fname;
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
                        string contentType;


                        Stream fs = Request.Files[i].InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        contentType = MimeMapping.GetMimeMapping(fname);
                        JOAttachmentList = BL.AddJOAttachment(Userid, bytes, contentType, fname, root);


                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }


            var jsonResult = Json(JOAttachmentList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public JsonResult DeleteFile(string id)
        {
            long AttachmentId = Convert.ToInt64(id);
            int Userid = Convert.ToInt16(Session["Tracker_userID"]);

            string message = "";

            message = BL.DeleteFile(AttachmentId, Userid);
            return Json(message);
        }

        [HttpPost]
        public FileResult DownLoadFile(int id)
        {

            BE.DriversDocumentDetailsEntiites JOAttachmentList = new BE.DriversDocumentDetailsEntiites();
            JOAttachmentList = BL.GetFile(id);
            return File(JOAttachmentList.Data, JOAttachmentList.ContentType, JOAttachmentList.DocFileName);

        }

        [HttpPost]
        public JsonResult SaveAttachmentToDirectory(BE.DriverAttachment fileData)
        {
            int Userid = Convert.ToInt16(Session["Tracker_userID"]);

            if (Request.Files.Count > 0)
            {
                try
                {
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
                        string contentType;


                        Stream fs = Request.Files[i].InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        contentType = MimeMapping.GetMimeMapping(fname);
                        string MSNO = fileData.MSNoFile;
                        string MSNO1 = fileData.MSNoFile;
                        string DocumenttypeID = fileData.DocumenttypeID;
                        //int DocID = fileData.DocID;

                        string root = Path.Combine(Server.MapPath("~/DriverDocumentDocs/"), MSNO);
                        string PathSave = "~/DriverDocumentDocs/" + MSNO + "/" + fname;
                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        fname = Path.Combine(Server.MapPath("~/DriverDocumentDocs/" + MSNO + "/"), fname);
                        //fname = Path.Combine(root, "/" + fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(fname);
                        if (System.IO.File.Exists(fname))
                        {
                            db.sub_ExecuteNonQuery("USP_INSERT_Drivers_DOCS " + MSNO + ",'" + PathSave + "'," + Userid + ",'"+ fname + "','"+contentType+"','"+ DocumenttypeID + "'");
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

        [HttpPost]
        public JsonResult GetMaxIDFromDriver()
        {
           
            int Userid = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";

            message = BL.GetDrivermaxID();
            return Json(message);
        }

        public ActionResult ShowDriverattachment(int DriverID)
        {
            ViewBag.DriverID = DriverID;

            return PartialView();
           
        }

        public ActionResult ShowDriverattachments(int DriverID)
        {
            ViewBag.DriverID = DriverID;

            List<BE.DriverAttachment> JOAttachmentList = new List<BE.DriverAttachment>();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            JOAttachmentList = BL.GetDriverdocumentList(DriverID);
            var jsonResult = Json(JOAttachmentList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        //[HttpPost]
        public FileResult DownLoadFiles(int id, string id1)
        {
            BE.DriverAttachment LRDocumentList = new BE.DriverAttachment();
            LRDocumentList = BL.GetDriverdocumentDownloadList(id, id1);
            //return File(LRDocumentList.FilePath, LRDocumentList.DocumentType);
            return File(LRDocumentList.FilePath, "application.pdf");
        }


        public ActionResult SearchDriver()
        {


            return View();
        }


        public ActionResult GetDriversDetails(string searchcerteria, string searchon)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetDriverDetailslist '" + searchcerteria + "','" + searchon + "'");
            Session["GetDriverDetailsReport"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelDriverEntries(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["GetDriverDetailsReport"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=DriverReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Driver Details Report<strong></td></tr>");
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


        public ActionResult GetDriverLeaveEntriesDetails(string searchcerteria, string searchon)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetDriverLeaveEntriesreport '" + searchcerteria + "','" + searchon + "'");
            Session["GetDriverList"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

            //List<BE.UpcomingVehicleStatusEntities> upcomingList = new List<BE.UpcomingVehicleStatusEntities>();
            //upcomingList = reportprovider.GetUpComingVehicleDetails(ddlActivity);
            //Session["Activity"] = ddlActivity;
            //var jsonResult = Json(upcomingList, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }
        public ActionResult ExportToExcelDriverLeaveEntities(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["GetDriverList"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=DriverLeaveEntries.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Driver Leaves Entries<strong></td></tr>");
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

        public ActionResult DriverLeaveEntities()
        {
            List<BE.DriverLeaveEntities> LeaveList = new List<BE.DriverLeaveEntities>();
            LeaveList = BL.GetDriverLeaveDetails();
            ViewBag.LeaveList = new SelectList(LeaveList, "LeaveID", "LeaveName");


            return View();
        }


        public ActionResult AddDriverDetails(string Trailername)
        {
            BE.DriverEntities DriverlistList = new BE.DriverEntities();
            DriverlistList = BL.GetTrailerDetails(Trailername);
            var jsonResult = Json(DriverlistList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult AddDriverLeaveEntites(string Driverid, string TrailerNo, string DriverleaveFor, string Remarks)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.SaveLeaveDetails(Driverid, TrailerNo, DriverleaveFor, Remarks, Userid);
            return Json(message);
        }

        public ActionResult GetDriverLeaveEntries()
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetDriverLeaveEntriesSummary");
            Session["GetDriverList"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

            //List<BE.UpcomingVehicleStatusEntities> upcomingList = new List<BE.UpcomingVehicleStatusEntities>();
            //upcomingList = reportprovider.GetUpComingVehicleDetails(ddlActivity);
            //Session["Activity"] = ddlActivity;
            //var jsonResult = Json(upcomingList, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }
        public ActionResult ReleaseDriverDetails(string DriverID, string HoldID)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.ReleaseDriverid(DriverID, HoldID, Userid);
            return Json(message);
        }
        public ActionResult DriverLoanDeductionMapping()
        {

            return View();
        }


        public ActionResult DriverLoanDetails(string Loadid, string LoanDate, string Driverid, string DriverName, string SlipNo, string FuelQty, string FuelCash, string DeductionType, string DeductionFuel
             , string DeductionAmount, string Deductionbank, string Remarks, string deductionFor)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.SaveDriverLoanDetails(Loadid, LoanDate, Driverid, DriverName, SlipNo, FuelQty, FuelCash, DeductionType,
                DeductionFuel, DeductionAmount, Deductionbank, Remarks, Userid, deductionFor);
            return Json(message);
        }

        //public ActionResult GetDriverLoadSummaryReport(string FromDate, string Todate, string Value)
        //{
        //    DataTable Driverdeduction = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    if (Value == "Summary")
        //    {
        //        Driverdeduction = db.sub_GetDatatable("USP_GetDriverLoanSummary '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd HH:mm") + "'");


        //        Session["fromdate"] = FromDate;
        //        Session["todate"] = Todate;

        //    }

        //    var json = JsonConvert.SerializeObject(Driverdeduction);
        //    Driverdeduction.Columns.Remove("Edit");
        //    Session["DriverLoandeduction"] = Driverdeduction;
        //    var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}

        public ActionResult GetDriverLoadSummaryReport(string FromDate, string Todate, string Value)
        {
            var json = "";
            DataTable SalesRegisterReport = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            if (Value == "Summary")
            {
                SalesRegisterReport = db.sub_GetDatatable("USP_GetDriverLoanSummary '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd HH:mm") + "','" + Value + "'");
                json = JsonConvert.SerializeObject(SalesRegisterReport);
                SalesRegisterReport.Columns.Remove("Edit");
                Session["ListforDriverloanentries"] = SalesRegisterReport;
               
              
                Session["Values"] = "Amount";
            }

            if (Value == "Pending Summary")
            {
                SalesRegisterReport = db.sub_GetDatatable("USP_GetDriverLoanSummary '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd HH:mm") + "','" + Value + "'");
                json = JsonConvert.SerializeObject(SalesRegisterReport);
                Session["ListforDriverloanentries"] = SalesRegisterReport;
                Session["Values"] = "Amount";
            }

           // var json = JsonConvert.SerializeObject(SalesRegisterReport);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult AjaxCheckLicenceNo(string DlNo)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.CheckDriverLiceneceDuplicate(DlNo);
            return Json(message);
        }

     
        public ActionResult ExportToExcelDriverloandeduction()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["ListforDriverloanentries"];
            //dt.Columns.Remove("Edit");
            //DataTable dt = (DataTable)ViewData["VoucherDetails"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Driver loan deduction summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Driver loan deduction summary<strong></td></tr>");
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

        //
        public ActionResult BlackListPersonDetails()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            return View();
        }
        // Done By Sagar
        //===========================================================================================================================
        [HttpPost]
        public ActionResult AddBLPerson(string Name, string AdharNo, string Penno, string ContactNo, string VoterID, string Reference, string Remarks)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.SaveBLDetails(Name, AdharNo, Penno, ContactNo, VoterID, Reference, Remarks, Userid);
            return Json(message);
        }
        //==========================================================================================================================
        public JsonResult BLPList(string Search)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ActivityOBJ = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ActivityOBJ = db.sub_GetDatatable("SP_LPersonList '" + Search + "'");
            Session["ExpInvList"] = ActivityOBJ;
            //Session["fromdate"] = AGID;
            //Session["todate"] = ToDate;
            var jsonResult = Json(JsonConvert.SerializeObject(ActivityOBJ), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ExportToBLPList()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable obj = (DataTable)Session["ExpInvList"]; //object.columns.remove("column_name");
            
            GridView gridview = new GridView();
            gridview.DataSource = obj;
            gridview.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Black-List-Details.xls");
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

    }
}