using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using System.Xml.Linq;
using System.IO;
using System.Data;
namespace TrackerMVC.Controllers.IGM_File
{
    [UserAuthenticationFilter] 
    public class IGMFileController : Controller
    {
        // GET: IGMFile
        BM.IGM.IGM IG= new BM.IGM.IGM();
        public ActionResult IGMFileUpload()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadIGMFile()
        {
            List<BE.IGMEntities> IGMList = new List<BE.IGMEntities>();
            string message="";
            if (Request.Files.Count > 0)
            {
                try
                {
                    int Userid = Convert.ToInt32(Session["Tracker_userID"]);
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
                        string contentType;
                        fname = Path.Combine(Server.MapPath("~/ImportFile/"), fname);
                        file.SaveAs(fname);
                        //   byte[] Bytes = ReadAllBytes(fname);

                        Stream fs = Request.Files[i].InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        contentType = MimeMapping.GetMimeMapping(fname);
                       // var fileContents = System.IO.File.ReadAllText(fname);
                      //  JOAttachmentList = BL.AddJOAttachment(Userid, bytes, contentType, fname);

                        //string filedata = fileContents;
                        DataTable DT = new DataTable();

                        DT.Columns.Add("SeqNo");
                        DT.Columns.Add("Line");
                      //  DT.Columns.Add("ID");

                       using (StreamReader file12 = new StreamReader(fname))
                            {
                            int counter = 0;
                            string ln;
                            
                            while ((ln = file12.ReadLine()) != null)
                            {
                           // Console.WriteLine(ln);
                           // string data = ln;
                            counter++;
                            DataRow dar = DT.NewRow();
                            dar["SeqNo"] = counter;
                            dar["Line"] = ln;
                            DT.Rows.Add(dar);
                                
                            }
                            file12.Close();
                          
                            if (DT != null)
                            {
                                message = IG.AddIGMFileData(DT, Userid);

                            }
                           // Console.WriteLine($"File has {counter} lines.");
                            }
                            
                    }
                }
                catch (Exception ex)
                {
                    //return Json("Error occurred. Error details: " + ex.Message);
                }
            }

            // return Json(JOAttachmentList);
            //var Data = JsonConvert.SerializeObject(JOAttachmentList, Formatting.Indented,
            //            new JsonSerializerSettings
            //            {
            //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //            });
            return Json(message);
            // return new JsonResult() { Data = JOAttachmentList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

       

    }
}