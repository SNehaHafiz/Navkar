using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using HC = TrackerMVCDataLayer.Helper;
using System.Data;
using Newtonsoft.Json;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Xml;

namespace TrackerMVC.Controllers
{
    public class ReceiptTallyController : Controller
    {
        BM.TallySyncronizations.TallySyncronizations TS = new BM.TallySyncronizations.TallySyncronizations();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReceiptTallyInfo()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        public ActionResult CartingReceiptTallyInfo()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        public ActionResult JVReceiptTally()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        public ActionResult YardReceiptTallyInfo()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }
        public ActionResult EDIReceiptTallyInfo()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        [HttpPost]
        public JsonResult FetchCategorywiseReceipt(string Date, string Category)
        {

            List<BE.ImportReceipt> Invoicelist = new List<BE.ImportReceipt>();

            Invoicelist = TS.getCategorywiseReceipt(Date, Category);
            Session["RCategoryInfo"] = Category;
            Session["RDateInfo"] = Date;

            return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult FetchCategorywiseEDI(string Date, string Category)
        {

            List<BE.ImportReceipt> Invoicelist = new List<BE.ImportReceipt>();

            Invoicelist = TS.FetchCategorywiseEDI(Date, Category);
            Session["RCategoryInfo"] = Category;
            Session["RDateInfo"] = Date;
            var jsonResult = Json(Invoicelist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
            //return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public JsonResult FetchJVReceipt(string Date, string Category)
        {

            List<BE.ImportReceipt> Invoicelist = new List<BE.ImportReceipt>();

            Invoicelist = TS.getJVReceipt(Date, Category);
            Session["RCategoryInfo"] = Category;
            Session["RDateInfo"] = Date;

            return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public JsonResult FetchCategorywiseYardReceipt(string Date, string Category)
        {

            List<BE.ImportReceipt> Invoicelist = new List<BE.ImportReceipt>();

            Invoicelist = TS.getCategorywiseYardReceipt(Date, Category);
            Session["RCategoryInfo"] = Category;
            Session["RDateInfo"] = Date;

            return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public ActionResult ExportToExcelTallyJVInfo()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string frmDate = Session["RDateInfo"].ToString() + " 00:00";
            string toDate = Session["RDateInfo"].ToString() + " 23:59";
            DataTable dt = new DataTable();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("TALLY_INTERFACE_RECEIPT_CSV_JV_BAL '" + frmDate + "','" + toDate + "'");
            DataTable getMovementICDNew = dt;
            string Tittle = "As On Date:- " + Session["RDateInfo"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ReceiptJVInterface.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Receipt Tally Report<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            //List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            //Invoicelist = TS.GetExporttoExcelData(Date, Category);

            //return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return View();

        }

        public ActionResult ExportToExcelTallyInvoiceInfo()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string frmDate = Session["RDateInfo"].ToString() + " 00:00";
            string toDate = Session["RDateInfo"].ToString() + " 23:59";
            string Category = Session["RCategoryInfo"].ToString();
            DataTable dt = new DataTable();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("TALLY_INTERFACE_RECEIPT '" + frmDate + "','" + toDate + "','" + Category + "'");
            DataTable getMovementICDNew = dt;
            string Tittle = "As On Date:- " + Session["RDateInfo"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ReceiptInterface.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Receipt Tally Report<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            //List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            //Invoicelist = TS.GetExporttoExcelData(Date, Category);

            //return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return View();

        }

        public ActionResult ExportToExcelTallyInvoiceInfo_EDi()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string frmDate = Session["RDateInfo"].ToString() + " 00:00";
            string toDate = Session["RDateInfo"].ToString() + " 23:59";
            string Category = Session["RCategoryInfo"].ToString();
            DataTable dt = new DataTable();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("TALLY_INTERFACE_RECEIPT_EDI '" + frmDate + "','" + toDate + "','" + Category + "'");
            DataTable getMovementICDNew = dt;
            string Tittle = "As On Date:- " + Session["RDateInfo"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ReceiptInterface.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Receipt Tally Report<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            //List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            //Invoicelist = TS.GetExporttoExcelData(Date, Category);

            //return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return View();

        }

        public ActionResult ExportToExcelTallyYardReceiptInfo()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string frmDate = Session["RDateInfo"].ToString() + " 00:00";
            string toDate = Session["RDateInfo"].ToString() + " 23:59";
            DataTable dt = new DataTable();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("TALLY_INTERFACE_RECEIPT_YARD '" + frmDate + "','" + toDate + "','" + Session["RCategoryInfo"].ToString() + "'");
            DataTable getMovementICDNew = dt;
            string Tittle = "As On Date:- " + Session["RDateInfo"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=YardReceiptInterface.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Yard Recetip Tally Report<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            //List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            //Invoicelist = TS.GetExporttoExcelData(Date, Category);

            //return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return View();

        }

        [HttpPost]
        public ActionResult LockReceiptData(List<BE.ImportReceipt> Receiptdata,string CategoryName)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("ReceiptNo");
            dataTable.Columns.Add("Category");


            dataTable.TableName = "PT_ReceiptLock";

            int Count = 1;
            foreach (BE.ImportReceipt item in Receiptdata)
            {
                DataRow row = dataTable.NewRow();
                if (item.Category == null)
                {
                    item.Category = "";
                }
                row["ID"] = Count++;
                row["ReceiptNo"] = item.ReceiptNo;
                row["Category"] = item.Category;
                dataTable.Rows.Add(row);
            }


            string message = TS.LockReceiptData(dataTable, CategoryName);
            // return Json(message);
            var json = JsonConvert.SerializeObject(message);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult LockEDItData(List<BE.ImportReceipt> Receiptdata, string CategoryName)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("ReceiptNo");
            dataTable.Columns.Add("Category");


            dataTable.TableName = "PT_ReceiptLock";

            int Count = 1;
            foreach (BE.ImportReceipt item in Receiptdata)
            {
                DataRow row = dataTable.NewRow();
                if (item.Category == null)
                {
                    item.Category = "";
                }
                row["ID"] = Count++;
                row["ReceiptNo"] = item.ReceiptNo;
                row["Category"] = item.Category;
                dataTable.Rows.Add(row);
            }


            string message = TS.LockEDIData(dataTable, CategoryName);
            // return Json(message);
            var json = JsonConvert.SerializeObject(message);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public ActionResult LockReceiptYardData(List<BE.ImportReceipt> Receiptdata, string CategoryName)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("ReceiptNo");
            dataTable.Columns.Add("Category");


            dataTable.TableName = "PT_ReceiptLock";

            int Count = 1;
            foreach (BE.ImportReceipt item in Receiptdata)
            {
                DataRow row = dataTable.NewRow();
                if (item.Category == null)
                {
                    item.Category = "";
                }
                row["ID"] = Count++;
                row["ReceiptNo"] = item.ReceiptNo;
                row["Category"] = item.Category;
                dataTable.Rows.Add(row);
            }


            string message = TS.LockReceiptYardData(dataTable, CategoryName);
            // return Json(message);
            var json = JsonConvert.SerializeObject(message);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        //Codes By Manish
        public ActionResult CategoryWiseCreditNote()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        public JsonResult GetExporttoExcelDataCreditNote(string Date, string Category)
        {

            List<BE.CategorywiseCreditNote> Invoicelist = new List<BE.CategorywiseCreditNote>();

            Invoicelist = TS.GetExporttoExcelDataCreditNote(Date, Category);

            return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public JsonResult FetchCategorywiseCreditNote(string Date, string Category)
        {

            List<BE.CategorywiseCreditNote> Invoicelist = new List<BE.CategorywiseCreditNote>();

            Invoicelist = TS.getCategorywiseCreditNote1(Date, Category);

            return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public ActionResult LockCreditNoteData(List<BE.CategorywiseCreditNote> Invoicedata, String CategoryName)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("WorkYear");
            dataTable.Columns.Add("Category");


            dataTable.TableName = "PT_CreditNoteLock";

            int Count = 1;
            foreach (BE.CategorywiseCreditNote item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["ID"] = Count++;
                row["InvoiceNo"] = item.InvoiceNo;
                row["WorkYear"] = item.WorkYear;
                row["Category"] = item.Category;
                dataTable.Rows.Add(row);
            }


            string message = TS.LockCreditNoteData(dataTable, CategoryName);
            return Json(message);

        }

        string OutBoundProcessPath = "~/Uploads/ExportToTally/";

        public ActionResult XMLDETAILSstorage(List<BE.ImportReceipt> Receiptdata)
        {
            TempData["TallyM"] = "";
            return Json("success");
        }

        public ActionResult XMLDETAILS(List<BE.ImportReceipt> Receiptdata,string CategoryName)        {
            //string OutBoundProcessPath = "~/Uploads/ExportToTally/";
            //List<BE.ImportReceipt> Receiptdata = JsonConvert.DeserializeObject(TempData["TallyM"]);
            string datetime = DateTime.Now.ToString("ddMMMyyyyHHmmss");
            string filename = @"\Receipt" + datetime + "_" + ".xml";
            ///
            OutBoundProcessPath = Server.MapPath(OutBoundProcessPath);
            //
            if (!Directory.Exists(OutBoundProcessPath + @"\XML"))
            {
                Directory.CreateDirectory(OutBoundProcessPath + @"\XML");
            }
            DirectoryInfo dir = new DirectoryInfo(OutBoundProcessPath + @"\XML");
            foreach (FileInfo f1 in dir.GetFiles())
            {
                //f1.Delete();//DELETE ALL FILES
            }
            string filePath = OutBoundProcessPath + @"\XML" + filename;



            string strComa = ",";
            string message = "";
            string strFileName = "";
            HC.DBOperations db = new HC.DBOperations();
            DataTable dtGetLineFileExt = new DataTable();
            string attachment = "";
            StringBuilder strb = new StringBuilder();
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "text/csv";
            Response.AddHeader("Pragma", "public");

            DataTable CompanyMaster = new DataTable();
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("ReceiptNo");
            dataTable.Columns.Add("Category");


        

            int Count = 1;
            foreach (BE.ImportReceipt item in Receiptdata)
            {
                DataRow row = dataTable.NewRow();
                if (item.Category == null)
                {
                    item.Category = "";
                }
                row["ID"] = Count++;
                row["ReceiptNo"] = item.ReceiptNo;
                row["Category"] = item.CategoryName;
                dataTable.Rows.Add(row);

                
            }
            DataTable data = new DataTable();
   
            data = db.sub_GetDatatable("truncate table Temp_XML_Receipt");

            for (int k = 0; k < dataTable.Rows.Count; k++)
            { 
                int i = db.sub_ExecuteNonQuery("Exec usp_Receipt_Xml_No '"  + dataTable.Rows[k].Field<string>("ReceiptNo") +  "'");
            }

            string xml = "";
            CompanyMaster = TS.GetXmlFileDetails(CategoryName);

            string Strfst = "";
             
            xml = "";
            foreach (DataRow row in CompanyMaster.Rows)
            {
                xml = xml + Convert.ToString(row[1]);
            }



            XmlDocument doc = new XmlDocument();

            xml = UnescapeXMLValue(xml);
            doc.LoadXml(xml.Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&").Replace("\r\n", "").Replace("&", "&amp;"));


            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            // Save the document to a file and auto-indent the output.
            XmlWriter writer = XmlWriter.Create(filePath, settings);
            doc.Save(writer);

            string Message = "";
            {
                FileInfo xmlFile = new FileInfo(OutBoundProcessPath + @"\XML" + filename);
                if (xmlFile.Length > 60)
                {
                    var rarResult = true;
                    if (rarResult == true)
                    {
                        string copyPath = "~/Uploads/ExportToTally/";
                        copyPath = Server.MapPath(copyPath);
                        if (!Directory.Exists(copyPath + @"\PO"))
                        {
                            Directory.CreateDirectory(copyPath + @"\PO");
                        }
                        System.IO.File.Copy(filePath, copyPath + @"\PO\" + filename);
                        byte[] fileBytes = System.IO.File.ReadAllBytes(copyPath + @"\PO\" + filename);
                        return Json(filename);

                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
                    }
                }
                else
                {

                    Message = "This file has nothing to send...";
                    return View("ErrorPage", Message);
                }
                return View("ErrorPage", Message);
            }

        }

        public ActionResult DownloadFile(string FileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"/Uploads/ExportToTally/PO/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + FileName);
            string fileName = FileName;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public string UnescapeXMLValue(string xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            return xml.Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&");
        }

        public ActionResult ReleaseRequest()
        {

            return View();
        }
       
        public ActionResult GetCustomerDDL(string prefixText)
        {

            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.ReleaseRequest> List = new List<BE.ReleaseRequest>();
            dataTable = db.sub_GetDatatable("USP_GetCustomerDDL '" + prefixText + "'");

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ReleaseRequest item = new BE.ReleaseRequest();
                    item.CustomerID = Convert.ToInt32(row["M_Common_ID"]);
                    item.CustomerName = Convert.ToString(row["Name"]);
                    List.Add(item);
                }
            }

            var jsonResult = Json(List, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult SaveReleaseRequest(BE.ReleaseRequest Data)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.ResponseMessage responseMessage = new BE.ResponseMessage();
            responseMessage = TS.SaveReleaseRequest(Data, Userid);
            return Json(responseMessage);
        }

        public ActionResult GetReleaseRequestSummary(string asOn)
        {
            List<BE.ReleaseRequest> summaryList = new List<BE.ReleaseRequest>();
            summaryList = TS.GetReleaseRequestSummary(asOn);
            return Json(summaryList);
        }

        public ActionResult ApproveReleaseRequest(int limitDays, long CRID, DateTime AddedOn)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.ResponseMessage responseMessage = new BE.ResponseMessage();
            responseMessage = TS.ApproveReleaseRequest(limitDays, CRID, Userid, AddedOn);
            return Json(responseMessage);
        }

        public ActionResult DeclineReleaseRequest(int limitDays, long CRID, DateTime AddedOn)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.ResponseMessage responseMessage = new BE.ResponseMessage();
            responseMessage = TS.DeclineReleaseRequest(limitDays, CRID, Userid, AddedOn);
            return Json(responseMessage);
        }

        public ActionResult PendingDeclineList()
        {
            return View();
        }

        public ActionResult GetreleaseRequestSatus(string asOn)
        {
            List<BE.ReleaseRequest> summaryList = new List<BE.ReleaseRequest>();
            summaryList = TS.GetreleaseRequestSatus(asOn);
            return Json(summaryList);
        }
    }
}