using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using BE = TrackerMVCEntities.BusinessEntities;
using Survey = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.UpdateSurvey;
using HC = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.IO;
using System.Web.UI;
using System.Text;
using DB = TrackerMVCDataLayer;
using System.Data.OleDb;

namespace TrackerMVC.Controllers.UpdateSurvey

{
    [UserAuthenticationFilter]
    public class UpdateSurveyController : Controller
    {
        Survey.UpdateSurvey surveyTrackerProvider = new Survey.UpdateSurvey();
        DB.Helper.DBOperations db = new HC.DBOperations();
        // GET: UpdateSurvey
        public ActionResult UpdateSurvey()
        {

            return View();
        }

        

        public ActionResult SaveRemarks(string ContainerNo, string Remarks)
        {
            string message;
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = surveyTrackerProvider.SaveRemarks(ContainerNo, Remarks, userId);
            return Json(message, JsonRequestBehavior.AllowGet);

            // return View();

        }
        [HttpPost]
        public ActionResult getContainerRemarksList(string ContainerNo)
        {
            List<BE.JobOrderDEntities> ContainerList = new List<BE.JobOrderDEntities>();
            ContainerList = surveyTrackerProvider.getContainerList(ContainerNo);
            return Json(ContainerList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DeleteRemarks(string ContainerNo, string Remarks, long jono, long surveyID)
        {
            string message;
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = surveyTrackerProvider.DeleteRemarks(ContainerNo, Remarks, jono, userId, surveyID);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult UpdateActivity()
        {
            List<BE.CodecoEntities> ActivityList = new List<BE.CodecoEntities>();
            ActivityList = surveyTrackerProvider.getActivityList();
            ViewBag.ddlActivity = new SelectList(ActivityList, "ID", "Activity");
            //DataTable UpdateActivity = new DataTable();
            //HC.DBOperations db = new HC.DBOperations();
            //// dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            //UpdateActivity = db.sub_GetDatatable("USP_CODECO_NEED_TO_GENERATE");
            //var jsonResult = Json(UpdateActivity, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
            return View();

        }

        public ActionResult CodecoPendingStatus()
        {
            //List<BE.CodecoEntities> ActivityList = new List<BE.CodecoEntities>();
            //ActivityList = surveyTrackerProvider.getActivityList();
            //ViewBag.ddlActivity = new SelectList(ActivityList, "ID", "Activity");
            DataTable UpdateActivity = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            UpdateActivity = db.sub_GetDatatable("USP_CODECO_NEED_TO_GENERATE");
            UpdateActivity.Columns.Remove("slID");
            UpdateActivity.Columns.Remove("LineId");
            var json = JsonConvert.SerializeObject(UpdateActivity);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            //return View();
            return jsonResult;

        }
        [HttpPost]
        public ActionResult SaveActivity(List<BE.UpdateActivirtEnt> Debitdata)
        {
            string message;
            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Activity");

            foreach (BE.UpdateActivirtEnt item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["Activity"] = item.Activity;
                

                dataTable.Rows.Add(row);
            }

            message = surveyTrackerProvider.SaveActivity(dataTable, userId);
            return Json(message, JsonRequestBehavior.AllowGet);

            // return View();

        }

        public ActionResult UploadDocumentForContainer()
        {
            List<BE.DocList> DocList = new List<BE.DocList>();
            DocList = surveyTrackerProvider.GetDocList();
            //DocLRList = DocList.DocList;
            ViewBag.DocList = new SelectList(DocList, "DocID", "DocName");
            List<BE.CodecoEntities> ActivityList = new List<BE.CodecoEntities>();
            ActivityList = surveyTrackerProvider.getActivityList();
            ViewBag.ddlActivity = new SelectList(ActivityList, "ID", "Activity");

            return View();
        }

        [HttpPost]
        public JsonResult SaveContainerAttachmentToDirectory(BE.ContainerDocsEntities fileData)
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
                        string Containerno = fileData.Containerno;
                        string CategoryID = fileData.CategoryID;
                        string MovementType = fileData.MovementType;
                        string DocID = fileData.DocID;
                        //int DocID = fileData.DocID;

                        string root = Path.Combine(Server.MapPath("~/ContainerDocumentDocs/"), Containerno);
                        string PathSave = "~/ContainerDocumentDocs/" + Containerno + "/" + fname;
                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        fname = Path.Combine(Server.MapPath("~/ContainerDocumentDocs/" + Containerno + "/"), fname);
                        //fname = Path.Combine(root, "/" + fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(fname);
                        if (System.IO.File.Exists(fname))
                        {
                            
                            db.sub_ExecuteNonQuery("USP_INSERT_Container_DOCS '" + Containerno + "','" + PathSave + "'," + Userid + ",'" + fname + "','" + contentType + "','" + DocID + "','" + CategoryID + "','" + MovementType + "'");
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

        //[HttpPost]
        //public ActionResult GetContainerDocsDetails(string ContainerNo)
        //{
        //    List<BE.ContainerDocsEntities> ActivityList = new List<BE.ContainerDocsEntities>();
        //    ActivityList = surveyTrackerProvider.GetContainerDocsList(ContainerNo);
        //    var jsonResult = Json(ActivityList, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}


        public JsonResult GetContainerDocsDetails(string ContainerNo)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_GetContainerDocsDetails'" + ContainerNo + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult GetCancelContainerno(string AutoID ,string ContainerNo)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            HC.DBOperations db1 = new HC.DBOperations();
            DataTable Result = db1.sub_GetDatatable("USP_Cancel_ContainerDocs '" + AutoID + "','" + ContainerNo + "','" + UserID + "'");

            if (Result != null)
            {
                foreach (DataRow row1 in Result.Rows)
                {

                    message = Convert.ToString(row1["mgs"]);
                }

            }
            return Json(message);
        }

        //[HttpPost]
        public FileResult DownloadAttachment1(string id)
        {
            BE.ContainerDocsEntities LRDocumentList = new BE.ContainerDocsEntities();
            LRDocumentList = surveyTrackerProvider.GetCountainerNodocs(id);
            //return File(LRDocumentList.FilePath, LRDocumentList.DocumentType);
            return File(LRDocumentList.FilePath, "application.pdf");
        }

        [HttpPost]
        public ActionResult Capture()
        {
            if (Request.InputStream.Length > 0)
            {
                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                    string hexString = Server.UrlEncode(reader.ReadToEnd());
                    string imageName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");
                    string imagePath = string.Format("~/Captures/{0}.png", imageName);
                    System.IO.File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                    Session["CapturedImage"] = VirtualPathUtility.ToAbsolute(imagePath);
                }
            }

            return View();
        }

        [HttpPost]
        public ContentResult GetCapture()
        {
            string url = Session["CapturedImage"].ToString();
            Session["CapturedImage"] = null;
            return Content(url);
        }

        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        //[HttpPost]
        public ActionResult DownloadActivity(string Slid, string Linecode, string Activity)
        {
            string strComa = ",";
            string message = "";
            string strFileName = "";
            HC.DBOperations db = new HC.DBOperations();
            DataTable dtGetLineFileExt = new DataTable();
            dtGetLineFileExt = db.sub_GetDatatable("SELECT FileName,FileType FROM Settings_Codeco Where LineId= '" + Slid + "'");

            if (dtGetLineFileExt.Rows.Count <= 0)
            {
                strFileName = "CodecoTextFile.txt";
            }
            else
            {
                strFileName = dtGetLineFileExt.Rows[0]["FileName"].ToString() + dtGetLineFileExt.Rows[0]["FileType"].ToString();
            }
            
            string attachment = "attachment; filename=" + strFileName;
            StringBuilder strb = new StringBuilder();
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "text/csv";
            Response.AddHeader("Pragma", "public");
            
            DataTable CompanyMaster = new DataTable();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("Usp_Codeco_Generate_manual '" + Slid + "','" + Linecode + "','" + Activity + "'");
            string Strfst = "";
            for (int i = 0; i <= CompanyMaster.Rows.Count - 1; i++)
            {
                Strfst = Strfst + CompanyMaster.Rows[i][0];
                strb.Append(CompanyMaster.Rows[i][0].ToString());
                strb.AppendLine();
            }

            if (Strfst == "")
            {
                Strfst = "Codeco Not Found for selected Line :- " + Linecode;
                strb.Append(Strfst);
            }

            Response.Write(strb.ToString());
            Response.Flush();
            Response.End();

            return View();

        }

        public JsonResult ImportFile()
        {
            int Userid = Convert.ToInt32(Session["userid"]);


            List<BE.UpdateActivirtEnt> DriverPaymentRecoList = new List<BE.UpdateActivirtEnt>();
            if (Request.Files.Count > 0)
            {
                try
                {

                    // Get all files from Request object
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
                        // fname = Path.Combine(fname);
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

                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                // BL.AddErrorLog("cmdExcel");
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    // BL.AddErrorLog("odaExcel");
                                    DataTable dt = new DataTable();
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    // BL.AddErrorLog("Open connection");
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    // BL.AddErrorLog(sheetName);
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();

                                    DataTable DriverPaymentDT = new DataTable();


                                    DriverPaymentDT.Columns.Add("ContainerNo");
                                    DriverPaymentDT.Columns.Add("Activity");
                               

                                    int irow = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        ///string accNo = row[0].ToString();

                                        
                                            DataRow dar = DriverPaymentDT.NewRow();

                                            dar["ContainerNo"] = row[0].ToString().ToUpper();
                                            dar["Activity"] = row[1].ToString().ToUpper();
                                        

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
                                        DriverPaymentRecoList = surveyTrackerProvider.UpdateDetails(DriverPaymentDT);
                                        return Json(DriverPaymentRecoList);


                                    }
                                }
                            }
                        }

                    }

                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    // Console.WriteLine("Error occurred. Error details: " + ex.Message);
                    // BL.AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'",""));
                    // return Json("Error occurred. Error details: " + ex.Message);
                    return Json(1);
                    // return Json("FormData is not supported.");
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    }
}