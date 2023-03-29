using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO = TrackerMVCEntities.BusinessEntities;
using BL = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using TrackerMVC.Filters;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Web.Hosting;
using CD = TrackerMVCDataLayer.Helper;
//using DB = FGInspectionDataLayer.Users;

namespace TrackerMVC.Controllers.TicketSystem
{
    [UserAuthenticationFilter]
    public class TicketSystemController : Controller
    {
        TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TicketSystem.TicketSystemBusinessLayer BLTktSystem = new TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TicketSystem.TicketSystemBusinessLayer();
        // BL.TicketSystemBusinessLayer BLTktSystem = new BL.TicketSystemBusinessLayer();
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            try
            {
                int UserID = Convert.ToInt16(Session["Tracker_userID"]);
                List<BO.TicketSystem> list = new List<BO.TicketSystem>();
                list = BLTktSystem.AjaxGetDashboardTicketData(UserID);
                ViewBag.list = list;
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //FOR TICKET SYSTEM
        public ActionResult AjaxStoreDisplayViewInTemp(int ISEntry)
        {
            TempData["ISEntry"] = ISEntry;
            return RedirectToAction("RaiseTicket", "TicketSystem");

        }
        public ActionResult RaiseTicket()
        {
            ViewBag.ISEntry = Convert.ToInt32(TempData["ISEntry"]);
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            ViewBag.Userid = Convert.ToInt32(Session["Tracker_userID"]);

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            string MenuID = "6341";
            dataTable = db.sub_GetDatatable("USP_Check_rights '" + MenuID  + "','" + UserID +  "'");
            if (dataTable.Rows[0].Field<int>("ID") != 0)
            {
                ViewBag.message = "";
            }
            else
            {
                ViewBag.message = "display: none";
            } 

            return View();
        }
        public PartialViewResult _ViewRaisedTicket(int ID,int IsUpdate)
        {
            BO.TicketSystem OL = BLTktSystem.GetDetailsForTicket(ID);
            ViewBag.Data = OL;
            ViewBag.IsUpdate = IsUpdate;
            ViewBag.Attachment = JsonConvert.SerializeObject(ViewBag.Data.Attachment);
            return PartialView();
        }
        public FileResult DownLoadFile(int id)
        {
            string DocName = Convert.ToString(TempData["DocName"]);
            string Filepath = Convert.ToString(TempData["Filepath"]);
            string ContentType = Convert.ToString(TempData["ContentType"]);

            byte[] fileBytes = System.IO.File.ReadAllBytes(Filepath);

            return File(fileBytes, ContentType, DocName);

        }
        [HttpPost]
        public PartialViewResult _AddAttachmentForRaiseTkt(int SrNo1)
        {
            ViewBag.SrNo1 = SrNo1;
            return PartialView();
        }
        [HttpPost]
        public ActionResult UploadNewRaiseTktAttachment(BO.AttachmentList fileData)
        {
            BO.AttachmentList QCAttach = new BO.AttachmentList();
            if (Request.Files.Count > 0)
            {
                try
                {
                    int Userid = Convert.ToInt16(Session["Tracker_userID"]);
                    string Type = fileData.Type;
                    Type = Type + Userid;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fname;
                        string root = Server.MapPath("~/Uploads/TempFolder/" + Type + "/");
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
                        string check = Path.Combine(Server.MapPath("~/Uploads/TempFolder/" + Type + "/"), fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(check);
                        QCAttach.DocName = fname;
                        QCAttach.FilePath = check;
                        QCAttach.ContentType = contentType;
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return Json(QCAttach);
        }
        public JsonResult UploadEmailIDListFileForRaiseTkt(List<HttpPostedFileBase> FileAttach)
        {
            List<BO.AttachmentList> AttachList = new List<BO.AttachmentList>();
            try
            {
                var uploads = HostingEnvironment.MapPath(@"\Uploads\RaisedTktAttachment\" + Convert.ToString(Session["Tracker_userID"]));
                
                if (FileAttach.Count > 0)
                {
                    foreach (HttpPostedFileBase file in FileAttach)
                    {
                        BO.AttachmentList attachment = new BO.AttachmentList();
                        if (file != null && file.ContentLength > 0)
                        {
                            string pathString = System.IO.Path.Combine(uploads);
                            var fileName1 = Path.GetFileName(Guid.NewGuid() + "_Captured_" + file.FileName);
                            bool isExists = System.IO.Directory.Exists(pathString);
                            if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                            var uploadpath = string.Format("{0}\\{1}", pathString, fileName1);
                            file.SaveAs(uploadpath);
                            attachment.FilePath = uploadpath;
                            attachment.DocName = fileName1;
                            attachment.ContentType = MimeMapping.GetMimeMapping(file.FileName);
                            AttachList.Add(attachment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(AttachList);
        }
        public ActionResult AjaxAddOrEditTicketData(BO.TicketSystem TktData, List<BO.AttachmentList> TktAttachment)
        {
            List<BO.TicketSystem> TktDataList = new List<BO.TicketSystem>();
            TktData.AddedBy = Convert.ToInt16(Session["Tracker_userID"]);
           
            var FromDate = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
            FromDate = FromDate.Replace("-", "_");
            FromDate = FromDate.Replace(" ", "_");
            FromDate = FromDate.Replace(":", "_");
            /*************************For Saving Attachments********************************/
            if (TktAttachment != null)
            {
                for (int j = 0; j < TktAttachment.Count; j++)
                {
                    string root = Server.MapPath("~/Uploads/RaisedTktAttachment/AttachmentForTktNoDate_" + FromDate + "/");
                   if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    string RtoReplace = Server.MapPath("~/Captures/");


                    string oldPath = TktAttachment[j].FilePath;
                    oldPath = oldPath.Replace("~/Captures/", RtoReplace);

                    string newPath = Path.Combine(Server.MapPath("~/Uploads/RaisedTktAttachment/AttachmentForTktNoDate_" + FromDate + "/"), TktAttachment[j].DocName);
                    if (!System.IO.File.Exists(newPath))
                    {
                        //if (!System.IO.File.Exists(oldPath))
                        //{
                            System.IO.File.Move(oldPath, newPath);
                        //}

                    }
                    TktAttachment[j].FilePath = newPath;
                }
            }
            DataTable dataTable2 = new DataTable();

            dataTable2.Columns.Add("UploadFor");
            dataTable2.Columns.Add("DocName");
            dataTable2.Columns.Add("FilePath");
            dataTable2.Columns.Add("ContentType");

            dataTable2.TableName = "PT_RaisedTktAttachments";
            if (TktAttachment != null)
            {
                foreach (BO.AttachmentList item in TktAttachment)
                {
                    DataRow row = dataTable2.NewRow();
                    row["UploadFor"] = item.UploadFor;
                    row["DocName"] = item.DocName;
                    row["FilePath"] = item.FilePath;
                    row["ContentType"] = item.ContentType;
                    dataTable2.Rows.Add(row);
                }

            }
            /*************************For Saving Attachments********************************/
            string UserName = Convert.ToString(Session["Tracker_userName"]);
            var strpath = "";
            if (TktAttachment != null)
            {
                foreach (BO.AttachmentList item in TktAttachment)
                {                    
                    strpath = strpath + "," + item.FilePath;
                }

            }
            TktDataList = BLTktSystem.AjaxAddOrEditTicketData(TktData, dataTable2, UserName, strpath);          
            return Json(TktDataList);
            
        }
        public JsonResult Search(string search)
        {
            List<BO.TicketSystem> list = new List<BO.TicketSystem>();
            list = BLTktSystem.AjaxGetOpenTicketData(search);
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult SearchClosed(string FromDate, string ToDate)
        {          
            List<BO.TicketSystem> list = new List<BO.TicketSystem>();
            list = BLTktSystem.AjaxGetClosedTicketData(FromDate, ToDate);
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult SaveClear(string ID, string search, string Reason)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            string Message = "";
            string EmailID = "";
            string UserName = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            List<BO.TicketSystem> list = new List<BO.TicketSystem>();
            Message = BLTktSystem.AjaxDeleteTicketData(ID, search, Reason);
            /*************************For MAil********************************/
            dataTable = db.sub_GetDatatable("select email_ID,UserName from UserDetails where UserID= '" + UserID + "'");
            if (dataTable.Rows[0].Field<string>("email_ID") != "")
            {
                EmailID = Convert.ToString(dataTable.Rows[0]["email_ID"]);
                UserName = Convert.ToString(dataTable.Rows[0]["UserName"]);
            }
            Boolean edata = false;
            string SendTO = EmailID;
            string strText = "";
            string strtable = "";
            string date = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");

            string Issues = "Ticket";
            DataTable dt = new DataTable();
            //DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("Usp_Mail_Settings_Date_Fill '" + Issues + "'");

            string Subject = dt.Rows[0]["SubjectPrefix"] + "Closed ticket no: " +ID+ "";
            strText += "Dear " + UserName + "<br>";
            strText += "<br>";
            strText += "Thank you for contacting to us. This message is to inform you";
            strText += "<br>";
            strText += "that we have resolved and closed your support Ticket No " + ID +".";
            strText += "<br>";
            strText += "<br>";
            strText += "Please email on support.navkar@digidisruptors.com us if you have any more questions, concerns, or issues.";
            strText += "<br>";
            strText += "Thanks & Regards,";
            strText += "<br>";
            strText += "DDPL Support Team.";
            strtable += "<html><body>";
            strtable += "<table align='left' cellpadding='0' cellspacing='0' bordercolor='black'  border='1' Style='width: 100%; height: auto;'>  ";
            strtable += "<tr bgcolor='white'><td style='font-family:Calibri; color:black; size:3 ;padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'>" + strText + "<br> </td></tr>";
            //strtable += "<tr bgcolor='white'><td style='font-family:Calibri; color:black; size:3  ;padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'> Ticket No.:" + TktNo + "<br> </td></tr>";
            //strtable += "<tr bgcolor='white'><td style='font-family:Calibri; color:black; size:3  ;padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'> Ticket Date:" + date + "<br> </td></tr>";
            //strtable += "<tr bgcolor='white'><td style='font-family:Calibri; color:black; size:3  ;padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'> Module/Subject:" + data.Subject + "<br> </td></tr>";
            //strtable += "<tr bgcolor='white'><td style='font-family:Calibri; color:black; size:3  ;padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'> Description:" + data.Description + "<br> </td></tr>";
            strtable += "</table>";
            strtable += "</body></html>";
            strtable += "<font face='Calibri' color='black' size=3>  </font> <br>";
            strtable += "<font face='Calibri' color='black' size=3>  </font> <br>";


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TicketSystem.EmailHelper obj = new TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TicketSystem.EmailHelper();
                    // BL.EmailHelper obj = new BL.EmailHelper();
                    edata = obj.SendEMail(Convert.ToInt32(dt.Rows[0]["SMTPServerPort"]), dt.Rows[0]["SMTPServer"].ToString(), dt.Rows[0]["MailFromID"].ToString(), dt.Rows[0]["UserPassword"].ToString(), SendTO, Subject, strText, dt.Rows[0]["MailCC"].ToString(), dt.Rows[0]["MailBCC"].ToString(), "");
                }
            }
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult ApproveTicket(string ID, string search)
        {
           
            string Message = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            List<BO.TicketSystem> list = new List<BO.TicketSystem>();
            Message = BLTktSystem.AjaxApproveTicketData(ID, search);
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult UpdateTicketTypeDetails(string ID, string search, int TktType)
        {
           
            string Message = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            List<BO.TicketSystem> list = new List<BO.TicketSystem>();
            Message = BLTktSystem.AjaxUpdateTicketTypeDetails(ID, search, TktType);
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult GetCapture()
        {
            try
            {
                string url = TempData["url"].ToString();
                string FilePath = TempData["FilePath"].ToString();
                string ContentType = TempData["ContentType"].ToString();
                string DocName = TempData["DocName"].ToString();
                List<BO.AttachmentList> list = new List<BO.AttachmentList>();
                list = BLTktSystem.AjaxgeCaptureDetails(url, FilePath, DocName, ContentType);

                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult DownloadFileAttached(string DocName, string ContentType, string FilePath)
        {
            byte[] filedata = System.IO.File.ReadAllBytes(FilePath);
            string contentType = ContentType;

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = DocName,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);

        }
        public PartialViewResult _DashboardListAgainstType(string Type)
        {
            ViewBag.Userid = Convert.ToInt32(Session["Tracker_userID"]);
            ViewBag.Type = Type;
            return PartialView();
        }
        public JsonResult AjaxDashboardListAgainstType(string FromDate, string ToDate, string Type)
        {
            List<BO.TicketSystem> list = new List<BO.TicketSystem>();
            list = BLTktSystem.AjaxDashboardListAgainstType(FromDate, ToDate, Type);
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult KeywordSearch()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            ViewBag.Userid = Convert.ToInt32(Session["Tracker_userID"]);
            return View();
        }
        public JsonResult AjaxTicketListAgainstKeyword(string FromDate, string ToDate, string searchText)
        {
            List<BO.TicketSystem> list = new List<BO.TicketSystem>();
            list = BLTktSystem.AjaxTicketListAgainstKeyword(FromDate, ToDate, searchText);
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult RaiseTicketSendotp()
        {
             

            return View();
        }

        public JsonResult SendCheckMail()
        {

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            string MenuID = "6341";
            dataTable = db.sub_GetDatatable("USP_ChecK_EmailID '" + UserID + "'");
            if (dataTable.Rows[0].Field<string>("mgs") != "")
            {
                message = Convert.ToString(dataTable.Rows[0]["mgs"]);
            }
             
            return Json(message);
        }
        public JsonResult SendOtp(string EmailID)
        {

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            string MenuID = "6341";
            dataTable = db.sub_GetDatatable("SentOtpNumber '" + UserID + "'" );
            if (dataTable.Rows[0].Field<int>("OTP") != 0)
            {
                message = Convert.ToString(dataTable.Rows[0]["OTP"]);
            }

            /*************************For MAil********************************/
            Boolean edata = false;
            string SendTO = EmailID;
            string strText = "";
            string strtable = "";
            string date = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");

            string Issues = "Ticket";
            DataTable dt = new DataTable();
            //DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("Usp_Mail_Settings_Date_Fill '" + Issues + "'");
            db.sub_ExecuteNonQuery("update UserDetails set OTP= '" + message + "', where UserID ='" + UserID + "'");
             
            string Subject = dt.Rows[0]["SubjectPrefix"] + "OTP to verify your email  ";
            strText += "Hello, OTP for your email verification is: " + message + "";
            strtable += "<html><body>";
            strtable += "<table align='left' cellpadding='0' cellspacing='0' bordercolor='black'  border='1' Style='width: 100%; height: auto;'>  ";
            strtable += "<tr bgcolor='white'><td style='font-family:Calibri; color:black; size:3 ;padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'>" + strText + "<br> </td></tr>";
            //strtable += "<tr bgcolor='white'><td style='font-family:Calibri; color:black; size:3  ;padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'> Ticket No.:" + TktNo + "<br> </td></tr>";
            //strtable += "<tr bgcolor='white'><td style='font-family:Calibri; color:black; size:3  ;padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'> Ticket Date:" + date + "<br> </td></tr>";
            //strtable += "<tr bgcolor='white'><td style='font-family:Calibri; color:black; size:3  ;padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'> Module/Subject:" + data.Subject + "<br> </td></tr>";
            //strtable += "<tr bgcolor='white'><td style='font-family:Calibri; color:black; size:3  ;padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'> Description:" + data.Description + "<br> </td></tr>";
            strtable += "</table>";
            strtable += "</body></html>";
            strtable += "<font face='Calibri' color='black' size=3>  </font> <br>";
            strtable += "<font face='Calibri' color='black' size=3>  </font> <br>";


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TicketSystem.EmailHelper obj = new TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TicketSystem.EmailHelper();
                    // BL.EmailHelper obj = new BL.EmailHelper();
                    edata = obj.SendEMail(Convert.ToInt32(dt.Rows[0]["SMTPServerPort"]), dt.Rows[0]["SMTPServer"].ToString(), dt.Rows[0]["MailFromID"].ToString(), dt.Rows[0]["UserPassword"].ToString(), SendTO, Subject, strText, dt.Rows[0]["MailCC"].ToString(), dt.Rows[0]["MailBCC"].ToString(), "");
                }
            }
            return Json(message);
        }
        public JsonResult SendCheckMailSumbit(string EmailID,string OTPEnter)
        {

            CD.DBOperations db = new CD.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            string MenuID = "6341";
            dataTable = db.sub_GetDatatable("USP_CheckOTP_EnterOTP '" + OTPEnter + "','" + EmailID + "','"  + UserID + "'");
            if (dataTable.Rows[0].Field<string>("mgs") != "")
            {
                message = Convert.ToString(dataTable.Rows[0]["mgs"]);
            }

            return Json(message);
        }

       
    }
}