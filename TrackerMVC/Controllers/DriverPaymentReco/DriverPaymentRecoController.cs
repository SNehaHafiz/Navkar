using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using BE = TrackerMVCEntities.BusinessEntities;
using driver = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.DriverPaymentReco;
using System.Globalization;
using Newtonsoft.Json;

namespace TrackerMVC.Controllers.DriverPaymentReco
{
    [UserAuthenticationFilter]
    public class DriverPaymentRecoController : Controller
    {
        // GET: DriverPaymentReco
        
        driver.DriverPaymentReco driverTrackerProvider = new driver.DriverPaymentReco();
        public ActionResult DriverPaymentReco()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            List<BE.DriverDropDown> DriverList = new List<BE.DriverDropDown>();
            BE.DriverPaymentReco Drivers = driverTrackerProvider.getDropDownList();
            DriverList = Drivers.DriverList;
            ViewBag.DriverList = new SelectList(DriverList, "DriverID", "DriverName");
            return View();
        }
        public JsonResult ImportFile()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            BE.DriverPaymentReco DriverPaymentList = new BE.DriverPaymentReco();
            List<BE.DriverPaymentReco> DriverPaymentRecoList = new List<BE.DriverPaymentReco>();
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

                                    
                                    DriverPaymentDT.Columns.Add("IFSCCode");
                                    DriverPaymentDT.Columns.Add("Amount");
                                    DriverPaymentDT.Columns.Add("TransferFromAccnt");
                                    DriverPaymentDT.Columns.Add("Transporter");
                                    DriverPaymentDT.Columns.Add("TransferInAccnt");
                                    DriverPaymentDT.Columns.Add("Driver");
                                    DriverPaymentDT.Columns.Add("TransferType");
                                    DriverPaymentDT.Columns.Add("VoucherNo");
                                    DriverPaymentDT.Columns.Add("VoucherFor");
                                    DriverPaymentDT.Columns.Add("FileName");
                                    DriverPaymentDT.Columns.Add("TransDate");
                                    DriverPaymentDT.Columns.Add("PaymentStatus");
                                    DriverPaymentDT.Columns.Add("ReferenceNo");
                                    //DriverPaymentDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        if (row[2].ToString() != "" && row[3].ToString() != "" && row[5].ToString() != "" && row[6].ToString() != "" && row[8].ToString() != "" && row[9].ToString() != "" && row[11].ToString() != "" && row[13].ToString() != "" && row[14].ToString() != "" && row[15].ToString() != "" && row[16].ToString() != "" && row[17].ToString() != "" )
                                        {

                                            //if (DateTime.TryParse(row[17].ToString(), out dDate))
                                            //{
                                            //    String.Format("{0:d/MM/yyyy}", dDate);
                                            //}
                                            //else
                                            //{
                                            //    var AlertValue = new { ColumnData = "Trans Date", ValueData = row[17].ToString() };
                                            //    var returnField = new { Issuedata = AlertValue, message = "" };

                                            //    return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                            //    // return Json(1); // <-- Control flow goes here
                                            //}
                                            
                                            //if (row[12].ToString() == "")
                                            //{

                                            //    var AlertValue = new { ColumnData = "Container No.", ValueData = row["Container No"].ToString() };
                                            //    var returnField = new { Issuedata = AlertValue, message = "" };

                                            //    return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                            //    // return Json(1);
                                            //}
                                            // string container = row["Container No"].ToString().ToUpper();
                                            //  string dichargedate = row["Discharge Date"].ToString();

                                            DataRow dar = DriverPaymentDT.NewRow();

                                            dar["IFSCCode"] = row[2].ToString().ToUpper();
                                            dar["Amount"] = row[3].ToString().ToUpper();
                                            dar["TransferFromAccnt"] = row[5].ToString();
                                            dar["Transporter"] = row[6].ToString();
                                            dar["TransferInAccnt"] = row[8].ToString();
                                            dar["Driver"] = row[9].ToString();
                                            dar["TransferType"] = row[11].ToString();
                                            dar["VoucherNo"] = row[13].ToString();
                                            dar["VoucherFor"] = row[14].ToString();
                                            dar["FileName"] = row[15].ToString();                                            
                                           // dar["TransDate"] = row[16].ToString();
                                            dar["TransDate"] = Convert.ToDateTime(row[16].ToString()).ToString("yyyy-MM-dd HH:mm");
                                            //Convert.ToDateTime(row[16].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                            dar["PaymentStatus"] = row[17].ToString();
                                            dar["ReferenceNo"] = row[18].ToString();
                                            //Convert.ToDateTime(row["Discharge Date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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

                                        DriverPaymentRecoList = driverTrackerProvider.UpdateDriverPaymentDetails(DriverPaymentDT, Userid);

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
        public JsonResult Validation(List<BE.DriverPaymentReco> Paymentdata)
        {
            string message = "";
            string message1 = "";

            List<BE.DriverPaymentReco> Validation = new List<BE.DriverPaymentReco>();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Transporter");
            dataTable.Columns.Add("Driver");
            dataTable.Columns.Add("VoucherNo");
            dataTable.Columns.Add("Amount");
            dataTable.Columns.Add("VoucherFor");

            foreach (BE.DriverPaymentReco item in Paymentdata)
            {
                DataRow row = dataTable.NewRow();


                row["Transporter"] = item.Transporter;
                row["Driver"] = item.Driver;
                row["VoucherNo"] = item.VoucherNo;
                row["Amount"] = item.Amount;
                row["VoucherFor"] = item.VoucherFor;

                dataTable.Rows.Add(row);
            }

            message = driverTrackerProvider.Validation(dataTable);

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

        [HttpPost]
        public ActionResult SavePaymentList(List<BE.DriverPaymentReco> Paymentdata, DateTime entrydate)
        {

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("VoucherNo");
            dataTable.Columns.Add("VoucherFor");
            dataTable.Columns.Add("Amount");
            dataTable.Columns.Add("TransferFromAccnt");
            dataTable.Columns.Add("Transporter");
            dataTable.Columns.Add("TransferInAccnt");
            dataTable.Columns.Add("Driver");
            dataTable.Columns.Add("TransferType");
            dataTable.Columns.Add("ReferenceNo");
            dataTable.Columns.Add("FileName");
            dataTable.Columns.Add("TransDate");

            dataTable.TableName = "PT_DriverPaymentDetails";
            foreach (BE.DriverPaymentReco item in Paymentdata)
            {

                if (item.PaymentStatus.ToString().ToUpper() == "SUCCESS")
                {
                    DataRow row = dataTable.NewRow();

                    row["VoucherNo"] = item.VoucherNo;
                    row["VoucherFor"] = item.VoucherFor;
                    row["Amount"] = item.Amount;
                    row["TransferFromAccnt"] = item.TransferFromAccnt;
                    row["Transporter"] = item.Transporter;
                    row["TransferInAccnt"] = item.TransferInAccnt;
                    row["Driver"] = item.Driver;
                    row["TransferType"] = item.TransferType;
                    row["ReferenceNo"] = item.ReferenceNo;
                    row["FileName"] = item.FileName;
                    row["TransDate"] = Convert.ToDateTime(item.TransDate).ToString("dd MMM yyyy");

                    dataTable.Rows.Add(row);
                }                
            }
            
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);

            BE.DriverPaymentReco PaymentList = new BE.DriverPaymentReco();
            PaymentList = driverTrackerProvider.SavePaymentList(dataTable, UserID, entrydate);
            return new JsonResult() { Data = PaymentList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult GetRecoSummary(string FromDate, string ToDate,string Voucher,string Driver)
        {
            List<BE.DriverPaymentRecoSummaryEntities> trainList = new List<BE.DriverPaymentRecoSummaryEntities>();
            trainList = driverTrackerProvider.GetDriverRecoSummary(FromDate,ToDate, Voucher, Driver);

            var jsonResult = Json(trainList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
            
        }


        // VIEW FOR CALENDAR
        public ActionResult DriverPaymentCalendar()
        {
            List<BE.DriverPaymentCalendarReport> PaymentList = driverTrackerProvider.GetDriverPaymentCalendar();
            ViewBag.PaymentList = JsonConvert.SerializeObject(PaymentList);
            return View();
        }
        public ActionResult CancelDriverRecodetails(List<BE.DriverPaymentRecoSummaryEntities> Driverrecodetails)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("VoucherNo");
            dataTable.Columns.Add("ReferenceNo");

            //int Count = 1;
            foreach (BE.DriverPaymentRecoSummaryEntities item in Driverrecodetails)
            {
                DataRow row = dataTable.NewRow();

                row["VoucherNo"] = item.VoucherNo;
                row["ReferenceNo"] = item.ReferenceNo;

                dataTable.Rows.Add(row);
            }
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            message = driverTrackerProvider.CancelDriverRecodetails(dataTable, Userid);

            return Json(message, JsonRequestBehavior.AllowGet);

            // return Json(1);
        }
    }
}