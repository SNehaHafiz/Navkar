using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.InvoiceChangesRequest;
using CI = TrackerMVCEntities.BusinessEntities;
using System.Data;
using CD = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.IO;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace TrackerMVC.Controllers.InvoiceChangesRequest
{
    [UserAuthenticationFilter]
    public class InvoiceChangesRequestController : Controller
    {
        DP.InvoiceChangesRequest objTrailerProvider = new DP.InvoiceChangesRequest();

        public JsonResult Save(FormCollection fileData)
        {
           
            string PathSave = "";
           
         
            CD.DBOperations db = new CD.DBOperations();
            //if (Request.Files.Count > 0)
            //{

            //    try
            //    {
            //        //  Get all files from Request object  
            //        HttpFileCollectionBase files = Request.Files;
            //        for (int i = 0; i < files.Count; i++)
            //        {
            //            //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
            //            //string filename = Path.GetFileName(Request.Files[i].FileName);  

            //            HttpPostedFileBase file = files[i];
            //            string fname;

            //            // Checking for Internet Explorer  
            //            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
            //            {
            //                string[] testfiles = file.FileName.Split(new char[] { '\\' });
            //                fname = testfiles[testfiles.Length - 1];
            //            }
            //            else
            //            {
            //                fname = file.FileName;
            //            }
            //            string contentType;
            //            fname = Path.Combine(Server.MapPath("~/InvoiceRequest/"), fname);
            //            string root = Server.MapPath("~/InvoiceRequest/");
            //             PathSave = "~/InvoiceRequest/" + file.FileName;
            //            if (!Directory.Exists(root))
            //            {
            //                Directory.CreateDirectory(root);
            //            }
            //            file.SaveAs(fname);
                      

            //            //return Json("File uploaded Successfully!");
            //        }



            //    }
            //    catch (Exception ex)
            //    {
            //        return Json(1);
            //    }



            //}
            DataTable dt = new DataTable();
            DataTable AttachDT = new DataTable();
            List<CI.InvoiceChangesRequestEntities> AttachmentList = new List<CI.InvoiceChangesRequestEntities>();
            AttachmentList = JsonConvert.DeserializeObject<List<CI.InvoiceChangesRequestEntities>>(fileData["table"].ToString());
            string InvoiceNo = fileData["InvoiceNo"].ToString();
            string InvoiceDate = fileData["InvoiceDate"].ToString();
            string txtremarks = fileData["txtremarks"].ToString();
            txtremarks = txtremarks.Replace("'", "''");
            string BilledTo = fileData["BilledTo"].ToString();
            string Activity = fileData["Activity"].ToString();
            CI.ResponseMessage message = new CI.ResponseMessage();
            dt = db.sub_GetDatatable("usp_invoice_changes_Request '" + InvoiceNo + "','" + InvoiceDate + "','" + txtremarks + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + PathSave + "','"+ BilledTo + "','"+ Activity + "'");
            if (AttachmentList != null)
            {
                foreach (CI.InvoiceChangesRequestEntities item in AttachmentList)
                {
                    AttachDT = db.sub_GetDatatable("USP_InsertInvoiceRequestDocs '" + InvoiceNo + "','"+ Activity + "','" + item.FilePath + "','" + item.DocFileName + "', '" + item.ContentType + "'");
                }
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    message.Message = Convert.ToString(row["message"]);
                    message.Status = Convert.ToInt32(row["Status"]);
                }
            }
            
            return Json(message);
        }
  
        // GET: InvoiceChangesRequest
        public ActionResult InvoiceChangesRequest()
        {
            string name = "";
            DP.InvoiceChangesRequest reportprovider = new DP.InvoiceChangesRequest();
            List<CI.InvoiceChangesRequestEntities> InvoiceList = new List<CI.InvoiceChangesRequestEntities>();
            InvoiceList = reportprovider.GetExisitingInvoiceChangesRequest(name);
            ViewBag.InvoiceList = new SelectList(InvoiceList,"", "Criteria");
            return View();
        }
        public ActionResult InvoiceChangesValidate(string Activity, string AssessNo)
        {
            
           
            DP.InvoiceChangesRequest reportprovider = new DP.InvoiceChangesRequest();
            List<CI.InvoiceChangesRequestEntities> InvoiceList = new List<CI.InvoiceChangesRequestEntities>();
            InvoiceList = reportprovider.GetExisitingInvoiceChangesValidate(Activity, AssessNo);
            ViewBag.InvoiceList = new SelectList(InvoiceList, "InvoiceDate", "BillTo", "GrandTotal");
            return Json(InvoiceList);
        }

        public ActionResult InvoiceChangesSummary()
        {
            List<CI.InvoiceChangesRequestEntities> TrolleyList = new List<CI.InvoiceChangesRequestEntities>();
            TrolleyList = objTrailerProvider.getInvoiceList();
            return PartialView(TrolleyList);
            //return View(TrolleyList);
            //return View();
        }

        public JsonResult cancelInvoiceNo(string AssessNo)
        {
            //string message = "";
            //int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            //message = objTrailerProvider.cancelInvoices(AssessNo, Userid);
            //return Json("Record cancelled successfully");
            List<CI.InvoiceChangesRequestEntities> TrolleyList = new List<CI.InvoiceChangesRequestEntities>();
            string message = "";
            //var EntryDate = Trolleytransport.EntryDate;
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_CANCEL_ASSESS_NO '" +  AssessNo + "','" +  Convert.ToInt32(Session["Tracker_userID"]) + "'");
            //Master();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    message = Convert.ToString(row["message"]);
                }
            }
            return Json(message);
        }

        public JsonResult ApprovedInvoice(string AssessNo)
        {
            //string message = "";
            //int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            //message = objTrailerProvider.cancelInvoices(AssessNo, Userid);
            //return Json("Record cancelled successfully");
            List<CI.InvoiceChangesRequestEntities> TrolleyList = new List<CI.InvoiceChangesRequestEntities>();
            string message = "";
            //var EntryDate = Trolleytransport.EntryDate;
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_Approved_ASSESS_NO '" + AssessNo + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");
            //Master();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    message = Convert.ToString(row["message"]);
                }
            }
            return Json(message);
        }

        public ActionResult AwaitingInvoiceRequestToApproved()
        {
            List<CI.InvoiceChangesRequestEntities> TrolleyList = new List<CI.InvoiceChangesRequestEntities>();
            TrolleyList = objTrailerProvider.getInvoiceList();
            return View(TrolleyList);
        }

        public JsonResult InvoiceChangeRequestSummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_ApprovedInvoiceChangeRequestList'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["InvoiceChangeRequestSummary"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;

            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult ExportToExcelInvoiceChangeRequestSummary()
        {

            DataTable dt = (DataTable)Session["InvoiceChangeRequestSummary"];

            CD.DBOperations db = new CD.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ExportLoadInventory = dt;
            string Tittle = "Invoice Change Request Summary";
            GridView gridview = new GridView();
            gridview.DataSource = ExportLoadInventory;
            gridview.DataBind();
            ////////gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Invoice Change Request Summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Voucher Report In and Out Details<strong></td></tr>");
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

        public ActionResult RevisedInvoiceWithChangeRequest()
        {
      
            return View();
        }
        public JsonResult RevisedInvoiceWithChangeRequestSummary(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_ShowChangeInvoiceRequestWithRevisedInv'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["USP_ShowChangeInvoiceRequestWithRevisedInv"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;

            var json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult ExportToExcelRevisedInvoiceWithChangeRequestSummary()
        {

            DataTable dt = (DataTable)Session["USP_ShowChangeInvoiceRequestWithRevisedInv"];

            CD.DBOperations db = new CD.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ExportLoadInventory = dt;
            string Tittle = "Invoice Change Request Summary";
            GridView gridview = new GridView();
            gridview.DataSource = ExportLoadInventory;
            gridview.DataBind();
            ////////gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Invoice Change Request Summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Voucher Report In and Out Details<strong></td></tr>");
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

        /***********************For Invoice Request*****************************/
        [HttpPost]
        public ActionResult AddAttachmentInvoiceRequest(string InvoiceNo)
        {
            ViewBag.InvoiceNo = InvoiceNo;
            return PartialView();
        }
        [HttpPost]
        public ActionResult UploadVoucherAttachment(CI.InvoiceChangesRequestEntities fileData)
        {
            CI.InvoiceChangesRequestEntities InvoiceAttachment = new CI.InvoiceChangesRequestEntities();

            
            if (Request.Files.Count > 0)
            {
                try
                {
                    int Userid = Convert.ToInt16(Session["userid"]);
                    string Type = fileData.AssessNo;
                    Type = Type.Replace('/', '_');
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fname;
                        string root = Server.MapPath("~/InvoiceRequest/" + Type + "/");
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
                        contentType = MimeMapping.GetMimeMapping(fname);

                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        string check = Path.Combine(Server.MapPath("~/InvoiceRequest/" + Type + "/"), fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(check);
                        InvoiceAttachment.DocName = fname;
                        InvoiceAttachment.FilePath = check;
                        InvoiceAttachment.ContentType = contentType;
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return Json(InvoiceAttachment);
        }
        [HttpPost]
        public ActionResult StoreFileDataInTemp(string DocName, string Filepath, string ContentType)
        {
            TempData["DocName"] = DocName;
            TempData["Filepath"] = Filepath;
            TempData["ContentType"] = ContentType;
            int i = 1;
            return Json(i);

        }
        public FileResult DownLoadFile(int id)
        {
            string DocName = Convert.ToString(TempData["DocName"]);
            string Filepath = Convert.ToString(TempData["Filepath"]);
            string ContentType = Convert.ToString(TempData["ContentType"]);

            byte[] fileBytes = System.IO.File.ReadAllBytes(Filepath);

            return File(fileBytes, ContentType, DocName);

        }

        public ActionResult DownLoadFilesIR(string InvoiceNo)
        {
            try
            {
                CD.DBOperations db = new CD.DBOperations();
                DataTable dataTable = new DataTable();
                List<CI.InvoiceChangesRequestEntities> DescList = new List<CI.InvoiceChangesRequestEntities>();
                dataTable = db.sub_GetDatatable("USP_InvoiceRequestDocList '" + InvoiceNo + "'");
                int i = 0;
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        CI.InvoiceChangesRequestEntities item = new CI.InvoiceChangesRequestEntities();
                        item.RunningID = Convert.ToInt32(row["RunningID"]); 
                        item.FilePath = Convert.ToString(row["FilePath"]);
                        item.DocFileName = Convert.ToString(row["FileName"]);
                        item.ContentType = Convert.ToString(row["ContentType"]);
                      
                        DescList.Add(item);
                    }
                }
                var jsonResult = Json(DescList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
                // return Json(DescList);
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult ApproveedInvoiceChangeRequestList()
        {

            return View();
        }

    }

}