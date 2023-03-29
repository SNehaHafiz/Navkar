using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using BE = TrackerMVCEntities.BusinessEntities;
using Vehicle = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.VehicleDetails;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using DP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrailerTransport;
using System.Data;
using CD = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using HC = TrackerMVCDataLayer.Helper;

namespace TrackerMVC.Controllers.VehicleDetails
{
    [UserAuthenticationFilter]
    public class VehicleDetailsController : Controller
    {
        HC.DBOperations db = new HC.DBOperations();
        DP.TrailerDataProvider objTrailerProvider = new DP.TrailerDataProvider();
        BM.LoadingPlan.LoadingPlan LP = new BM.LoadingPlan.LoadingPlan();
        Vehicle.VehicleDetails VehicleTrackerProvider = new Vehicle.VehicleDetails();
        // GET: VehicleDetails
        public ActionResult VehicleDetails()
        {
            return View();
        }
        public ActionResult GetDateWiseVehicleDetails(DateTime FromDate, DateTime ToDate,string searchText)
        {
            List<BE.VehicleDetails> VehicleList = new List<BE.VehicleDetails>();
            VehicleList = VehicleTrackerProvider.getVehicleList(FromDate, ToDate, searchText);
            var jsonResult = Json(VehicleList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult TestInsert()
        {
            return View();
        }
        

        public ActionResult LocationWiseBilling()
        {
            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocationG();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            return View();
        }
        [HttpPost]
        public ActionResult AjaxGetBillingReport(string Fromdate,string todate,int locationid)
        {
            List<BE.LocationWisePerTeusBillingEntities> VehicleList = new List<BE.LocationWisePerTeusBillingEntities>();
            VehicleList = VehicleTrackerProvider.getLocationBilling(Fromdate, todate, locationid);
            var jsonResult = Json(VehicleList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
            
        }


        public ActionResult InsuranceAndRTOTracking()
        {
            List<BE.VehicleTypeEntities> VehicleTypeList = new List<BE.VehicleTypeEntities>();
            BE.TrailerEntities TrailerList = objTrailerProvider.getDropDownList();
            VehicleTypeList = TrailerList.VehicleTypeList;
            ViewBag.VehicleTypeList = new SelectList(VehicleTypeList, "VehicleTypeID", "VehicleType");

            List<BE.PermitTypeEntities> Permittypelist = new List<BE.PermitTypeEntities>();
            Permittypelist = VehicleTrackerProvider.GetPermitTYpe();
            ViewBag.PermitTypeList = new SelectList(Permittypelist, "PermitID", "PermitType");

            List<BE.TaxTypeEntities> TaxList = new List<BE.TaxTypeEntities>();
            TaxList = VehicleTrackerProvider.GetTaxType();
            ViewBag.TaxTYpe = new SelectList(TaxList, "Taxid", "TaxType");

            List<BE.InsuranceCompanyEntities> InsuranceList = new List<BE.InsuranceCompanyEntities>();
            InsuranceList = VehicleTrackerProvider.GetInsuranceCompanyList();
            ViewBag.InsuranceCompany = new SelectList(InsuranceList, "InsurancID", "InsuranceCompany");

            List<BE.ModelEntities> ModelList = new List<BE.ModelEntities>();
            ModelList = VehicleTrackerProvider.GetModelList();
            ViewBag.ModelList = new SelectList(ModelList, "ModelID", "ModelName");

            List<BE.MakeEntities> MakeLIst = new List<BE.MakeEntities>();
            MakeLIst = VehicleTrackerProvider.GetMakeList();
            ViewBag.Makelist = new SelectList(MakeLIst, "MakeID", "MakeName");



            var IssuesList = new List<System.Collections.IList>();
            IssuesList.Add(new List<string>() { "Tax Validity :", "Fitness Validity" , "Permit Validity", "Green Tax Validity", "All India Permit Validity", "PUC" , "Speed Governor / Due Date" });
            ViewBag.IssuesList = IssuesList;
            return View();
        }
        //[HttpPost]
        //public ActionResult GetInsuranceData(string TrailerName)
        //{
        //    BE.InsuranceandTrackeringEntiites VehicleList = new BE.InsuranceandTrackeringEntiites();
        //    VehicleList = VehicleTrackerProvider.GetInsuranceDetails(TrailerName);
        //    var jsonResult = Json(VehicleList, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;

        //}

        [HttpPost]
        public ActionResult SaveRtoInsuranceData(List<BE.RTOTaxEntities> RTODetails, string TrailerName, string RegNo, string Regdate,
            string RegOwner,
            string VehicleType, string yearofmanufacture, string Make, string ChasisNo, string ENgineNo, string GrossWeight,
            string Model, string InVoiceAmount, string RCBookField, string PolicyType, string PolicyNumber, string InsuranceCompany,
            string PremiumAmount, string SumOFinsured, string PolicyDate, string ValidUpto, string PaymentDate, string bankname,
            string ENdDate, string EmiAMount, string TaxType, string PermitType, string PermitNo,string RGno)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("FromDate");
            dataTable.Columns.Add("TODate");
            dataTable.Columns.Add("Amount");
            dataTable.Columns.Add("VehicleNo");
            dataTable.TableName = "PT_AddRTODetailList";
            foreach (BE.RTOTaxEntities item in RTODetails)
            {
                DataRow row = dataTable.NewRow();

                row["VehicleNo"] = item.VehicleNo;
                row["InvoiceNo"] = item.InvoiceNo;
                row["FromDate"] = Convert.ToDateTime(item.FromDate).ToString("yyyy-MM-dd HH:mm");
                row["TODate"] = Convert.ToDateTime(item.TODate).ToString("yyyy-MM-dd HH:mm");
                row["Amount"] = item.Amount;
                dataTable.Rows.Add(row);
            }

            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string message = VehicleTrackerProvider.SaveInsuranceDate(dataTable, TrailerName, RegNo, Regdate, RegOwner, VehicleType, yearofmanufacture
                , Make, ChasisNo, ENgineNo, GrossWeight, Model, InVoiceAmount, RCBookField, userId, PolicyType, PolicyNumber, InsuranceCompany, PremiumAmount
                , SumOFinsured, PolicyDate, ValidUpto, PaymentDate, bankname, ENdDate, EmiAMount, TaxType, PermitType, PermitNo, RGno
                );
            return Json(message);

        }


        public ActionResult GetTrailerDetails(string TrailerName)
        {
            List<BE.InsuranceandTrackeringEntiites> VehicleList = new List<BE.InsuranceandTrackeringEntiites>();
            VehicleList = VehicleTrackerProvider.GetInsuranceDetails(TrailerName);
          

            return PartialView(VehicleList);
        }

        public ActionResult GetRtoDetails(string TrailerName)
        {
            List<BE.PermitTypeEntities> Permittypelist = new List<BE.PermitTypeEntities>();
            Permittypelist = VehicleTrackerProvider.GetPermitTYpe();
            ViewBag.PermitTypeList = new SelectList(Permittypelist, "PermitID", "PermitType");

            List<BE.TaxTypeEntities> TaxList = new List<BE.TaxTypeEntities>();
            TaxList = VehicleTrackerProvider.GetTaxType();
            ViewBag.TaxTYpe = new SelectList(TaxList, "Taxid", "TaxType");

            List<BE.InsuranceCompanyEntities> InsuranceList = new List<BE.InsuranceCompanyEntities>();
            InsuranceList = VehicleTrackerProvider.GetInsuranceCompanyList();
            ViewBag.InsuranceCompany = new SelectList(InsuranceList, "InsurancID", "InsuranceCompany");

            List<BE.ModelEntities> ModelList = new List<BE.ModelEntities>();
            ModelList = VehicleTrackerProvider.GetModelList();
            ViewBag.ModelList = new SelectList(ModelList, "ModelID", "ModelName");

            List<BE.MakeEntities> MakeLIst = new List<BE.MakeEntities>();
            MakeLIst = VehicleTrackerProvider.GetMakeList();
            ViewBag.Makelist = new SelectList(MakeLIst, "MakeID", "MakeName");
            List<BE.RTODetails> VehicleList = new List<BE.RTODetails>();
           
            VehicleList = VehicleTrackerProvider.GetTrailerWiseRtoDetails(TrailerName);
            if (VehicleList[0].permitno == " ")
            {
                ViewBag.Permitno = 0; ;
            }
            else
            {
                ViewBag.Permitno = VehicleList[0].permitno;
            }
           
            if (VehicleList[0].permittype == " ")
            {
                ViewBag.Permitype = VehicleList[0].permittype;
            }
            else
            {
                ViewBag.Permitype = VehicleList[0].permittype;
            }
            if (VehicleList[0].TaxType == " ")
            {
                ViewBag.TaxTYpeID = VehicleList[0].TaxType;
            }
            else
            {
                ViewBag.TaxTYpeID = VehicleList[0].TaxType;
            }
        

            var IssuesList = new List<System.Collections.IList>();
            IssuesList.Add(new List<string>() { "Tax Validity :", "Fitness Validity", "Permit Validity", "Green Tax Validity", "All India Permit Validity", "PUC", "Speed Governor / Due Date" });
            ViewBag.IssuesList = IssuesList;

            return PartialView(VehicleList);
        }


        public ActionResult GetInsuranceandRtoTracking(string TrailerName)
        {
            List<BE.InsuranceandTrackeringEntiites> VehicleList = new List<BE.InsuranceandTrackeringEntiites>();
            VehicleList = VehicleTrackerProvider.GetInsuranceDetails(TrailerName);
            return View();
        }

        public ActionResult GetTrailerWIseInsuranceDetails(string TrailerName)
        {
            BE.InsuranceAndRTOTracking VehicleList = new BE.InsuranceAndRTOTracking();
            VehicleList = VehicleTrackerProvider.GetTrailerWiseInsuradDetails(TrailerName);
            var jsonResult = Json(VehicleList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
               return jsonResult;
        }




        [HttpPost]
        public ActionResult RenevalRtoInsuranceData(List<BE.RTOTaxEntities> RTODetails, string TrailerName, string RegNo, string Regdate,
           string RegOwner,
           string VehicleType, string yearofmanufacture, string Make, string ChasisNo, string ENgineNo, string GrossWeight,
           string Model, string InVoiceAmount, string RCBookField, string PolicyType, string PolicyNumber, string InsuranceCompany,
           string PremiumAmount, string SumOFinsured, string PolicyDate, string ValidUpto, string PaymentDate, string bankname,
           string ENdDate, string EmiAMount, string TaxType, string PermitType, string PermitNo, string RGno)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("InvoiceNo");
            dataTable.Columns.Add("FromDate");
            dataTable.Columns.Add("TODate");
            dataTable.Columns.Add("Amount");
            dataTable.Columns.Add("VehicleNo");
            dataTable.TableName = "PT_AddRTODetailList";
            foreach (BE.RTOTaxEntities item in RTODetails)
            {
                DataRow row = dataTable.NewRow();

                row["VehicleNo"] = item.VehicleNo;
                row["InvoiceNo"] = item.InvoiceNo;
                row["FromDate"] = Convert.ToDateTime(item.FromDate).ToString("yyyy-MM-dd HH:mm");
                row["TODate"] = Convert.ToDateTime(item.TODate).ToString("yyyy-MM-dd HH:mm");
                row["Amount"] = item.Amount;
                dataTable.Rows.Add(row);
            }

            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string message = VehicleTrackerProvider.RenevalRTODetails(dataTable, TrailerName, RegNo, Regdate, RegOwner, VehicleType, yearofmanufacture
                , Make, ChasisNo, ENgineNo, GrossWeight, Model, InVoiceAmount, RCBookField, userId, PolicyType, PolicyNumber, InsuranceCompany, PremiumAmount
                , SumOFinsured, PolicyDate, ValidUpto, PaymentDate, bankname, ENdDate, EmiAMount, TaxType, PermitType, PermitNo, RGno
                );
            return Json(message);

        }



        public ActionResult GetRenevalDetails(string TrailerName)
        {
            List<BE.PermitTypeEntities> Permittypelist = new List<BE.PermitTypeEntities>();
            Permittypelist = VehicleTrackerProvider.GetPermitTYpe();
            ViewBag.PermitTypeList = new SelectList(Permittypelist, "PermitID", "PermitType");

            List<BE.TaxTypeEntities> TaxList = new List<BE.TaxTypeEntities>();
            TaxList = VehicleTrackerProvider.GetTaxType();
            ViewBag.TaxTYpe = new SelectList(TaxList, "Taxid", "TaxType");

            List<BE.InsuranceCompanyEntities> InsuranceList = new List<BE.InsuranceCompanyEntities>();
            InsuranceList = VehicleTrackerProvider.GetInsuranceCompanyList();
            ViewBag.InsuranceCompany = new SelectList(InsuranceList, "InsurancID", "InsuranceCompany");

            List<BE.ModelEntities> ModelList = new List<BE.ModelEntities>();
            ModelList = VehicleTrackerProvider.GetModelList();
            ViewBag.ModelList = new SelectList(ModelList, "ModelID", "ModelName");

            List<BE.MakeEntities> MakeLIst = new List<BE.MakeEntities>();
            MakeLIst = VehicleTrackerProvider.GetMakeList();
            ViewBag.Makelist = new SelectList(MakeLIst, "MakeID", "MakeName");
            List<BE.RTODetails> VehicleList = new List<BE.RTODetails>();

           

            var IssuesList = new List<System.Collections.IList>();
            IssuesList.Add(new List<string>() { "Tax Validity :", "Fitness Validity", "Permit Validity", "Green Tax Validity", "All India Permit Validity", "PUC", "Speed Governor / Due Date" });
            ViewBag.IssuesList = IssuesList;

            return PartialView();
        }

        [HttpPost]
        public ActionResult TPTariffSummary()
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_InsuranceAndRtoSummaryDetails");
            Session["InsuranceDetails"] = dt;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }


        public ActionResult ExportToExcelDetails()
        {

            DataTable dt = (DataTable)Session["InsuranceDetails"];
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            // string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=InsuranceDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Insurance Details Report <strong></td></tr>");
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
        public JsonResult AjaxGetVehicleDocType(string VehicleTypeid)
        {


            List<BE.TrailersEntities> Trailerno = new List<BE.TrailersEntities>();
            Trailerno = VehicleTrackerProvider.getVehicleDocType(VehicleTypeid);
            return new JsonResult() { Data = Trailerno, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }


        [HttpPost]
        public JsonResult SaveVehicleAttachmentToDirectory(BE.DriverAttachment fileData)
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

                        string root = Path.Combine(Server.MapPath("~/VehicleDocumentDocs/"), MSNO);
                        string PathSave = "~/VehicleDocumentDocs/" + MSNO + "/" + fname;
                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        fname = Path.Combine(Server.MapPath("~/VehicleDocumentDocs/" + MSNO + "/"), fname);
                        //fname = Path.Combine(root, "/" + fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(fname);
                        if (System.IO.File.Exists(fname))
                        {
                            db.sub_ExecuteNonQuery("USP_INSERT_Vehicle_DOCS " + MSNO + ",'" + PathSave + "'," + Userid + ",'" + fname + "','" + contentType + "','" + DocumenttypeID + "'");
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


        public ActionResult ShowVehicleattachment(string tRAILERNAME)
        {
            ViewBag.tRAILERNAME = tRAILERNAME;

            return PartialView();

        }

        public ActionResult ShowVehicleattachments(string Trailername)
        {
            ViewBag.Trailername = Trailername;

            List<BE.DriverAttachment> JOAttachmentList = new List<BE.DriverAttachment>();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            JOAttachmentList = VehicleTrackerProvider.GetTrailerdocView(Trailername);
            var jsonResult = Json(JOAttachmentList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        //[HttpPost]
        public FileResult DownLoadFilesVehicle(int id, string id1)
        {
            BE.DriverAttachment LRDocumentList = new BE.DriverAttachment();
            LRDocumentList = VehicleTrackerProvider.GetVehicleDocumentDownloadList(id, id1);
            //return File(LRDocumentList.FilePath, LRDocumentList.DocumentType);
            return File(LRDocumentList.FilePath, "application.pdf");
        }
    }
}