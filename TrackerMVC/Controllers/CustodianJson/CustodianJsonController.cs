using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using SF = TrackerMVCEntities.CustodianJson;
using ASR = TrackerMVCEntities.CustodianJsonASR;
using AR = TrackerMVCEntities.CustodianJsonAR;
using DP = TrackerMVCEntities.CustodianJsonDP;
using BO = BombayToolBusinessLayer.CutodianJson;
using TrackerMVC.Filters;
using System.Data;
using HC = TrackerMVCDataLayer.Helper;
using BE = TrackerMVCEntities.BusinessEntities;
using System.Threading;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace BombayTools.Controllers.CustodianJson
{
    [UserAuthenticationFilter]
    public class CustodianJsonController : Controller
    {
        BO.CustodianJsonBusinessLayer CustodianJsonBusinessLayer = new BO.CustodianJsonBusinessLayer();
        // GET: CustodianJson
        public ActionResult GenerateJson()
        {
            return View();
        }
        public PartialViewResult _GenerateJson(string FileType, string ContainerNo, string FilterType)
        {
            string fileName = "";
            string path = "";
            var jsonString = "";
            SF.Custodian custodian = new SF.Custodian();
            if (FileType == "SF")
            {
                SF.CustodianJson custodianJson = new SF.CustodianJson();
                SF.EGM eGM = new SF.EGM();
                custodianJson = CustodianJsonBusinessLayer.GenerateJsonSF(FileType, ContainerNo);
                eGM = custodianJson.EGM;
                custodian.custodianJsonSF = custodianJson;
                custodian.FileType = FileType;
                jsonString = JsonConvert.SerializeObject(eGM);
            }
            else if (FileType == "ASR")
            {
                ASR.CustodianJson custodianJsonASR = new ASR.CustodianJson();
                ASR.EGM eGMASR = new ASR.EGM();
                custodianJsonASR = CustodianJsonBusinessLayer.GenerateJsonASR(FileType, ContainerNo, FilterType);
                eGMASR = custodianJsonASR.EGM;
                custodian.custodianJsonASR = custodianJsonASR;
                custodian.FileType = FileType;
                jsonString = JsonConvert.SerializeObject(eGMASR);

            }
            else if (FileType == "AR")
            {
                AR.CustodianJson custodianJsonAR = new AR.CustodianJson();
                AR.EGM eGMAR = new AR.EGM();
                custodianJsonAR = CustodianJsonBusinessLayer.GenerateJsonAR(FileType, ContainerNo);
                eGMAR = custodianJsonAR.EGM;
                jsonString = JsonConvert.SerializeObject(eGMAR);
                fileName = custodianJsonAR.fileName + custodianJsonAR.fileNo + "_" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "_DEC.json";
                path = Path.Combine(custodianJsonAR.filePath, fileName);
                TempData["DocName"] = fileName;
                TempData["Filepath"] = custodianJsonAR.filePath;
                TempData["ContentType"] = "application/json; charset=utf-8";
                System.IO.File.WriteAllText(path, jsonString);
            }
            else if (FileType == "DP")
            {
                DP.CustodianJson custodianJsonDP = new DP.CustodianJson();
                DP.EGM eGMDP = new DP.EGM();
                custodianJsonDP = CustodianJsonBusinessLayer.GenerateJsonDP(FileType, ContainerNo);
                eGMDP = custodianJsonDP.EGM;
                jsonString = JsonConvert.SerializeObject(eGMDP);
                fileName = custodianJsonDP.fileName + custodianJsonDP.fileNo + "_" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "_DEC.json";
                path = Path.Combine(custodianJsonDP.filePath, fileName);
                TempData["DocName"] = fileName;
                TempData["Filepath"] = custodianJsonDP.filePath;
                TempData["ContentType"] = "application/json; charset=utf-8";
                System.IO.File.WriteAllText(path, jsonString);
            }

            //System.IO.File.WriteAllText(path, jsonString);

            return PartialView(custodian);
        }
        [HttpPost]
        public FileResult DownLoadFile(int id)
        {
            string DocName = Convert.ToString(TempData["DocName"]);
            string Filepath = Convert.ToString(TempData["Filepath"]);
            string ContentType = Convert.ToString(TempData["ContentType"]);
            string path = Path.Combine(Filepath, DocName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);

            return File(fileBytes, ContentType, DocName);

        }

        public JsonResult CreateJsonSF(SF.CustodianJson custodian, List<SF.CargoContainer> Container, string FileType, string ContainerNo)
        {

            string fileName = "";
            string path = "";
            var jsonString = "";
            SF.EGM eGM = new SF.EGM();
            eGM = custodian.EGM;
            eGM.master.cargoContainer = Container;
            jsonString = JsonConvert.SerializeObject(eGM);
            fileName = custodian.fileName + custodian.fileNo + "_" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "_DEC.json";
            path = Path.Combine(custodian.filePath, fileName);
            //path = Path.Combine(Server.MapPath("~/ImportFile/"), fileName);
            TempData["DocName"] = fileName;
            TempData["Filepath"] = custodian.filePath;
            TempData["ContentType"] = "application/json; charset=utf-8";
            System.IO.File.WriteAllText(path, jsonString);

            return Json(new { fileName = fileName, filePath = path, errorMessage = "" });

        }

        public JsonResult CreateJsonASR(ASR.CustodianJson custodian, List<SF.CargoContainer> Container, string FileType, string ContainerNo)
        {
            string fileName = "";
            string path = "";
            var jsonString = "";

            ASR.EGM eGMASR = new ASR.EGM();
            eGMASR = custodian.EGM;
            jsonString = JsonConvert.SerializeObject(eGMASR);
            fileName = custodian.fileName + custodian.fileNo + "_" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "_DEC.json";
            path = Path.Combine(custodian.filePath, fileName);
            TempData["DocName"] = fileName;
            TempData["Filepath"] = custodian.filePath;
            TempData["ContentType"] = "application/json; charset=utf-8";
            System.IO.File.WriteAllText(path, jsonString);

            return Json(new { fileName = fileName, filePath = path, errorMessage = "" });

        }
        [HttpPost]
        public JsonResult GetPendingDet(string FromDate, string ToDate, string FileTypeSF)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("USP_Pending_lis '" + FromDate + "','" + ToDate + "','" + FileTypeSF + "'");
            Session["USP_Pending_lis"] = tblInvoiceList;

            var json = JsonConvert.SerializeObject(tblInvoiceList);


            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult CreateJsonSFMult(string FileType, string ConainterNo)
        {
            string fileName = "";
            string path = "";
            var jsonString = "";
            try
            {
                SF.Custodian custodian = new SF.Custodian();



                SF.CustodianJson custodianJson = new SF.CustodianJson();
                SF.EGM eGM = new SF.EGM();
                custodianJson = CustodianJsonBusinessLayer.GenerateJsonSF(FileType, ConainterNo);
                eGM = custodianJson.EGM;
                custodian.custodianJsonSF = custodianJson;
                custodian.FileType = FileType;
                path = custodian.custodianJsonSF.filePath;
                fileName = custodian.custodianJsonSF.fileName;



                jsonString = JsonConvert.SerializeObject(eGM);


                fileName = fileName + custodian.custodianJsonSF.fileNo + "_" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "_DEC.json";


                path = Path.Combine(path, fileName);

                System.IO.File.WriteAllText(path, jsonString);


                //int milliseconds = 2000;
                //Thread.Sleep(milliseconds);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(new { fileName = fileName, filePath = path, errorMessage = "" });
        }
        public FileResult DownloadFileS(string FileName, string filePath)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + filePath;
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                string fileName = FileName;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public JsonResult GetPendingDetAsf(string FromDate, string ToDate, string FileTypeASF)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("USP_Pending_lis '" + FromDate + "','" + ToDate + "','" + FileTypeASF + "'");
            Session["Sales_GST_ACK_List_Request"] = tblInvoiceList;

            var json = JsonConvert.SerializeObject(tblInvoiceList);


            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public JsonResult CreateJsonASFMult(string FileType, string ContainerNo, string FilterType)
        {


            SF.Custodian custodian = new SF.Custodian();
            string fileName = "";
            string path = "";
            var jsonString = "";


            ASR.CustodianJson custodianJsonASR = new ASR.CustodianJson();
            ASR.EGM eGMASR = new ASR.EGM();
            custodianJsonASR = CustodianJsonBusinessLayer.GenerateJsonASR(FileType, ContainerNo, FilterType);
            eGMASR = custodianJsonASR.EGM;
            custodian.custodianJsonASR = custodianJsonASR;
            custodian.FileType = FileType;
            path = custodian.custodianJsonASR.filePath;
            fileName = custodian.custodianJsonASR.fileName;
            jsonString = JsonConvert.SerializeObject(eGMASR);



            fileName = fileName + custodian.custodianJsonASR.fileNo + "_" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "_DEC.json";


            path = Path.Combine(path, fileName);

            System.IO.File.WriteAllText(path, jsonString);


            return Json(new { fileName = fileName, filePath = path, errorMessage = "" });

        }

        public ActionResult GetExcelPendingSFFile()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dtFCLDestuffTallyList = (DataTable)Session["USP_Pending_lis"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=GenerateSFFile.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Generate SF File<strong></td></tr>");

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
        public JsonResult GetPendingListOf(string FromDate, string ToDate, string FileTypeCategory)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("USP_SCMTR_SF_LiST '" + FromDate + "','" + ToDate + "','" + FileTypeCategory + "'");
            Session["USP_SCMTR_SF_LiST"] = tblInvoiceList;

            var json = JsonConvert.SerializeObject(tblInvoiceList);


            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult GetExcelFileStatusSF()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dtFCLDestuffTallyList = (DataTable)Session["USP_SCMTR_SF_LiST"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=SFStatusDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>SF Status Details<strong></td></tr>");

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
        public JsonResult GetPendingListOfASR(string FromDate, string ToDate, string FileTypeCategory)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("USP_SCMTR_ASR_LiST '" + FromDate + "','" + ToDate + "','" + FileTypeCategory + "'");
            Session["USP_SCMTR_ASR_LiST"] = tblInvoiceList;

            var json = JsonConvert.SerializeObject(tblInvoiceList);


            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult GetExcelFileStatusASR()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dtFCLDestuffTallyList = (DataTable)Session["USP_SCMTR_ASR_LiST"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ASRStatusDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>ASR Status Details<strong></td></tr>");

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


        public JsonResult GetSummartGet(string FromDate, string ToDate)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("USP_SCMTR_STATUS '" + FromDate + "','" + ToDate + "'");
            Session["USP_SCMTR_STATUS"] = tblInvoiceList;

            var json = JsonConvert.SerializeObject(tblInvoiceList);


            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public ActionResult GetExcelGetSummartGet()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dtFCLDestuffTallyList = (DataTable)Session["USP_SCMTR_STATUS"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=StatusDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Status Details<strong></td></tr>");

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