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
    [UserAuthenticationFilter]
    public class TallySyncronizationsController : Controller
    {
        BM.TallySyncronizations.TallySyncronizations TS = new BM.TallySyncronizations.TallySyncronizations();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryWiseInvoice()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        public ActionResult TallyInterfaceYard()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        public ActionResult CartingWiseInvoice()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        public ActionResult EDIWiseInvoice()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        [HttpPost]
        public JsonResult FetchCategorywiseInvoice(string Date, string Category)
        {

            List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            Invoicelist = TS.getCategorywiseInvoice(Date, Category);
            Session["CategoryInfo"] = Category;
            Session["DateInfo"] = Date;

            var jsonResult = Json(Invoicelist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public JsonResult FetchCategorywiseInvoiceCarting(string Date, string Category)
        {
            List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            Invoicelist = TS.getCategorywiseInvoiceCarting(Date, Category);
            Session["CategoryInfo"] = Category;
            Session["DateInfo"] = Date;
            var jsonResult = Json(Invoicelist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            //return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return jsonResult;
        }
        [HttpPost]
        public JsonResult FetchCategorywiseInvoiceEDI(string Date, string Category)
        {
            List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            Invoicelist = TS.getCategorywiseInvoiceEDI(Date, Category);
            Session["CategoryInfo"] = Category;
            Session["DateInfo"] = Date;
            var jsonResult = Json(Invoicelist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult FetchCategorywiseYardInvoice(string Date, string Category)
        {

            List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            Invoicelist = TS.getCategorywiseYardInvoice(Date, Category);
            Session["CategoryInfo"] = Category;
            Session["DateInfo"] = Date;

            var jsonResult = Json(Invoicelist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            //return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public ActionResult ExportToExcelTallyInvoiceInfo()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = new DataTable();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("TALLY_INTERFACE_INVOICE_ALLnew '" + Session["DateInfo"].ToString() + "','" + Session["DateInfo"].ToString() + "','" + Session["CategoryInfo"].ToString() + "'");
            DataTable getMovementICDNew = dt;
            string Tittle = "As On Date:- " + Session["DateInfo"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=MovementAtIcdReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Invoice Tally Report<strong></td></tr>");
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

        public ActionResult ExportToExcelTallyInvoiceInfo_EDI()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = new DataTable();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("TALLY_INTERFACE_INVOICE_EDI '" + Session["DateInfo"].ToString() + "','" + Session["DateInfo"].ToString() + "','" + Session["CategoryInfo"].ToString() + "'");
            DataTable getMovementICDNew = dt;
            string Tittle = "As On Date:- " + Session["DateInfo"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=EDIReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Invoice Tally Report<strong></td></tr>");
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

        public ActionResult ExportToExcelTallyYardInvoiceInfo()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = new DataTable();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("TALLY_INTERFACE_INVOICE_ALLnewYard '" + Session["DateInfo"].ToString() + "','" + Session["DateInfo"].ToString() + "','" + Session["CategoryInfo"].ToString() + "'");
            DataTable getMovementICDNew = dt;
            string Tittle = "As On Date:- " + Session["DateInfo"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=YardMovementAtIcdReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Yard Invoice Tally Report<strong></td></tr>");
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
        public ActionResult LockInvoiceData(List<BE.CategorywiseInvoice> Invoicedata, String CategoryName)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("WorkYear");
            dataTable.Columns.Add("Category");


            dataTable.TableName = "PT_InvoiceLock";

            int Count = 1;
            foreach (BE.CategorywiseInvoice item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["ID"] = Count++;
                row["InvoiceNo"] = item.InvoiceNo;
                row["WorkYear"] = item.WorkYear;
                row["Category"] = item.Category;
                dataTable.Rows.Add(row);
            }


            string message = TS.LockInvoiceData(dataTable, CategoryName);
            // return Json(message);
            var json = JsonConvert.SerializeObject(message);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult LockInvoiceDataEDI(List<BE.CategorywiseInvoice> Invoicedata, String CategoryName)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("WorkYear");
            dataTable.Columns.Add("Category");


            dataTable.TableName = "PT_InvoiceLock";

            int Count = 1;
            foreach (BE.CategorywiseInvoice item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["ID"] = Count++;
                row["InvoiceNo"] = item.InvoiceNo;
                row["WorkYear"] = item.WorkYear;
                row["Category"] = item.Category;
                dataTable.Rows.Add(row);
            }


            string message = TS.LockInvoiceDataEDI(dataTable, CategoryName);
            // return Json(message);
            var json = JsonConvert.SerializeObject(message);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public ActionResult LockInvoiceYardData(List<BE.CategorywiseInvoice> Invoicedata, String CategoryName)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("WorkYear");
            dataTable.Columns.Add("Category");


            dataTable.TableName = "PT_InvoiceLock";

            int Count = 1;
            foreach (BE.CategorywiseInvoice item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["ID"] = Count++;
                row["InvoiceNo"] = item.InvoiceNo;
                row["WorkYear"] = item.WorkYear;
                row["Category"] = item.Category;
                dataTable.Rows.Add(row);
            }


            string message = TS.LockInvoiceYardData(dataTable, CategoryName);
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



        //Codes End Manish


        //code by prashant   12 Nov 2019

        public ActionResult DispatchIntimationEntry()
        {

            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        public JsonResult AjaxPocketFetchCategorywiseInvoice(string Date, string Category, string FromDate, string BasedOn)
        {

            List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            Invoicelist = TS.getPocketCategorywiseInvoice(Date, Category, FromDate, BasedOn);
            var jsonResult = Json(Invoicelist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            // return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
        public JsonResult AjaxLRcontainerList(string LRno)
        {

            List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            Invoicelist = TS.getLRWIseContainere(LRno);
            var jsonResult = Json(Invoicelist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            // return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
        public ActionResult CourierAddressPrint(int CourierID)
        {

            List<BE.CategorywiseInvoice> Invoicelist = new List<BE.CategorywiseInvoice>();

            Invoicelist = TS.getCouriorAddress(CourierID);
            ViewBag.Address = Invoicelist[0].CourierAddress;
            string[] values = ViewBag.Address.Split(',');
            List<BE.CategorywiseInvoice> ContainerSet = new List<BE.CategorywiseInvoice>();
            for (int i = 0; i < values.Length; i++)
            {
                BE.CategorywiseInvoice set = new BE.CategorywiseInvoice();
                values[i] = values[i].Trim();
                set.CourierAddress = values[i];
                ContainerSet.Add(set);
            }
            ViewBag.values = ContainerSet;
            return PartialView();
        }


        public ActionResult AjaxDispathuntimationAll(string Fromdate, string category)
        {
            DataTable GetCreditNoteSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GetCreditNoteSummary = db.sub_GetDatatable("SP_CategoryWiseShowDocketNumber '" + Convert.ToDateTime(Fromdate).ToString("yyyy-MM-dd") + "','" + category + "'");

            var json = JsonConvert.SerializeObject(GetCreditNoteSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        //Code end by Prashant

        [HttpPost]
        public ActionResult LockInvoiceDocketData(List<BE.CategorywiseInvoice> Invoicedata, String CategoryName)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("DocketNo");
            dataTable.Columns.Add("DocketDate");
            dataTable.Columns.Add("Category");


            dataTable.TableName = "InvoiceDocketType";

            int Count = 1;
            foreach (BE.CategorywiseInvoice item in Invoicedata)
            {
                DataRow row = dataTable.NewRow();

                row["ID"] = Count++;
                row["InvoiceNo"] = item.InvoiceNo;
                row["DocketNo"] = item.DocketNo;
                row["DocketDate"] = item.DocketDate;
                row["Category"] = item.Category;
                dataTable.Rows.Add(row);
            }


            string message = TS.LockInvoiceDocketData(dataTable, CategoryName);
            return Json(message);

        }
        public ActionResult CustomerCreditLimitMapping()
        {

            List<BE.KDM> Kdmlist = new List<BE.KDM>();
            Kdmlist = TS.GetKDM();
            ViewBag.kdmlist = new SelectList(Kdmlist, "Kdmid", "KdmName");

            List<BE.SalesPersonM> SalesPersonM = new List<BE.SalesPersonM>();
            SalesPersonM = TS.GetSalesPersonM();
            ViewBag.SalesPersonMlist = new SelectList(SalesPersonM, "SalesPerson_ID1", "SalesPerson_Name");

            List<BE.SalesPersonM> grade = new List<BE.SalesPersonM>();
            grade = TS.Getgrade();
            ViewBag.gradelist = new SelectList(grade, "gradeid", "gradename");

            return View();
        }
        public JsonResult update(BE.SalesPersonM SalesPersonM)
        {
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            HC.DBOperations db = new HC.DBOperations();
            //int retVal = 0;
            db.sub_ExecuteNonQuery("USP_updateCreditMapping '" + SalesPersonM.ReplaceID + "','" + SalesPersonM.txtcreditlimit + "','" + SalesPersonM.gradename + "','" + SalesPersonM.KDM + "','" + SalesPersonM.SalesPerson_ID1 + "','" + SalesPersonM.team + "'");
            //Master();

            return Json(SalesPersonM);
        }

        public JsonResult getPartyNameReceipt(string prefixText, string Mode)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dataTable = db.sub_GetDatatable("USP_GetPartyNameReceipt '" + Mode + "','" + prefixText + "'");

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
        string OutBoundProcessPath = "~/Uploads/ExportToTally/";
        public ActionResult XMLDETAILSForLedger()        {
            //string OutBoundProcessPath = "~/Uploads/ExportToTally/";
            //List<BE.ImportReceipt> Receiptdata = JsonConvert.DeserializeObject(TempData["TallyM"]);
            string datetime = DateTime.Now.ToString("ddMMMyyyyHHmmss");
            string filename = @"\Ledger" + datetime + "_" + ".xml";
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


            string xml = "";
            CompanyMaster = TS.GetLedgerXMLdetails();

            string Strfst = "";
            //for (int i = 0; i <= CompanyMaster.Rows.Count - 1; i++)
            //{ Strfst = Strfst + CompanyMaster.Rows[i][1];
            //    strb.Append(CompanyMaster.Rows[i][1].ToString());
            //    strb.AppendLine();

            //    xml += Convert.ToString(strb);


            //}
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

        public string UnescapeXMLValue(string xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            return xml.Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&");
        }


        public ActionResult XMLDETAILSForInvoicesDetails(List<BE.CategorywiseInvoice> list)        {

            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("ReceiptNo");
            table.Columns.Add("Category");
            table.TableName = "PT_XMLReceiptDownload";
            foreach (BE.CategorywiseInvoice item in list)
            {
                DataRow row = table.NewRow();

                row["ID"] = 0;
                row["ReceiptNo"] = item.InvoiceNo;
                row["Category"] = item.InvoiceNo;



                table.Rows.Add(row);
            }
            //string OutBoundProcessPath = "~/Uploads/ExportToTally/";
            //List<BE.ImportReceipt> Receiptdata = JsonConvert.DeserializeObject(TempData["TallyM"]);
            string datetime = DateTime.Now.ToString("ddMMMyyyyHHmmss");
            string filename = @"\Invoice" + datetime + "_" + ".xml";
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


            string xml = "";
            CompanyMaster = TS.GetInvoiceXMLdetails(table);

            string Strfst = "";
            //for (int i = 0; i <= CompanyMaster.Rows.Count - 1; i++)
            //{ Strfst = Strfst + CompanyMaster.Rows[i][1];
            //    strb.Append(CompanyMaster.Rows[i][1].ToString());
            //    strb.AppendLine();

            //    xml += Convert.ToString(strb);


            //}
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



        public ActionResult XMLDETAILSFOrCredit(List<BE.ImportReceipt> Receiptdata, string category)        {
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


            dataTable.TableName = "PT_XMLReceiptDownload";

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
            DataTable data = new DataTable();

            data = db.sub_GetDatatable("truncate table Temp_XML_Receipt");

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                int i = db.sub_ExecuteNonQuery("Exec usp_Receipt_Xml_No '" + dataTable.Rows[k].Field<string>("ReceiptNo") + "'");
            }

            string xml = "";
            CompanyMaster = TS.GetCreditFileDetails(category);

            string Strfst = "";
            //for (int i = 0; i <= CompanyMaster.Rows.Count - 1; i++)
            //{ Strfst = Strfst + CompanyMaster.Rows[i][1];
            //    strb.Append(CompanyMaster.Rows[i][1].ToString());
            //    strb.AppendLine();

            //    xml += Convert.ToString(strb);


            //}
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
            string path = AppDomain.CurrentDomain.BaseDirectory + @"/Uploads/ExportToTally/XML/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + FileName);
            string fileName = FileName;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult DownloadFile1(string FileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"/Uploads/ExportToTally/XML/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + FileName);
            string fileName = FileName;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public JsonResult GetLedgerDetails()
        {
            List<BE.LedgerDetails> Invoicelist = new List<BE.LedgerDetails>();
            Invoicelist = TS.GetLedgerDetails();
            return new JsonResult() { Data = Invoicelist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ExportToExcelAryanDepot(string FromDate ,string ToDate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = new DataTable();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("Tracker_Aryan_Depot.dbo.TALLY_INTERFACE_INVOICE_MNR '" + FromDate + "','" + ToDate  + "'");
            DataTable getMovementICDNew = dt;
            string Tittle = "As On Date:- " + Session["DateInfo"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=YardInvoiceReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Yard Invoice Report<strong></td></tr>");
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
    }
}