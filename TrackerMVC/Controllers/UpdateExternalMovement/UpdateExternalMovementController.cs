using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.UpdateExternalMovement;
using CD = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;

namespace TrackerMVC.Controllers.UpdateExternalMovement
{
    [UserAuthenticationFilter]
    public class UpdateExternalMovementController : Controller
    {
        // GET: UpdateExternalMovement
        public JsonResult Save(BE.SaveUpdateMovementEntities SaveUpdateMovementEntities)
        {
            int i = 0;
            //var EffectiveFromDate = TPTariffDetailsEntities.EffectiveFromDate;
            //var EffectivetoDate = TPTariffDetailsEntities.EffectivetoDate;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("update_ext_empty_D '" + SaveUpdateMovementEntities.ContainerNo + "','" + Convert.ToDateTime(SaveUpdateMovementEntities.MovementDate).ToString("yyyy-MM-dd HH:mm") + "','" + SaveUpdateMovementEntities.Vehicle + "','" + SaveUpdateMovementEntities.fromid + "','" + SaveUpdateMovementEntities.toid + "','" + SaveUpdateMovementEntities.Size + "','" + SaveUpdateMovementEntities.Type + "','" + SaveUpdateMovementEntities.Reamrks + "','" + SaveUpdateMovementEntities.jono + "','" + UserID + "','" + SaveUpdateMovementEntities.ShipmentNo + "','" + "" + "','" + "" + "'");
            //Master();

            return Json(i);
        }

        BM.UpdateExternalMovement trainTrackerProvider = new BM.UpdateExternalMovement();
        public ActionResult UpdateExternalMovement()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<BE.UpdateExternalMovement> FromList = new List<BE.UpdateExternalMovement>();
            FromList = trainTrackerProvider.GetFromDropdownList();
            ViewBag.FromList = new SelectList(FromList, "ID", "Criteria");

            List<BE.UpdateExternalMovement> SizeList = new List<BE.UpdateExternalMovement>();
            SizeList = trainTrackerProvider.GetSizeDropdownList();
            ViewBag.SizeList = new SelectList(SizeList, "Criteria", "Criteria");

            List<BE.UpdateExternalMovement> TypeList = new List<BE.UpdateExternalMovement>();
            TypeList = trainTrackerProvider.GettypeDropdownList();
            ViewBag.TypeList = new SelectList(TypeList, "ID", "Criteria");

            List<BE.UpdateExternalMovement> VesselList = new List<BE.UpdateExternalMovement>();
            VesselList = trainTrackerProvider.GetVesselDropdownList();
            ViewBag.VesselList = new SelectList(VesselList, "ID", "Criteria");

            List<BE.UpdateExternalMovement> SlList = new List<BE.UpdateExternalMovement>();
            SlList = trainTrackerProvider.GetshiplineDropdownList();
            ViewBag.SlList = new SelectList(SlList, "ID", "Criteria");

            List<BE.UpdateExternalMovement> PortsList = new List<BE.UpdateExternalMovement>();
            PortsList = trainTrackerProvider.GetPortsDropdownList();
            ViewBag.PortsList = new SelectList(PortsList, "ID", "Criteria");

            List<BE.UpdateExternalMovement> TransList = new List<BE.UpdateExternalMovement>();
            TransList = trainTrackerProvider.GetTransportDropdownList();
            ViewBag.TransList = new SelectList(TransList, "ID", "Criteria");

            return View();
        }
        //BM.UpdateExternalMovement trainTrackerProvider = new BM.UpdateExternalMovement();
        public JsonResult ajaxSearchlistdetails(string ContainerNo,int Jono)
        {

            BE.UpdateContainerMovementEntities Searchdetails = new BE.UpdateContainerMovementEntities();
            Searchdetails = trainTrackerProvider.GetAccidentSearchDetails(ContainerNo, Jono);

            var returnField = new { ImportList = Searchdetails };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public ActionResult ExternalMovementPendencyReport()
        {
            return View();
        }

        public ActionResult GetDataBind(DateTime FromDate, DateTime ToDate, string SearchCriteria, string SearchText, string jotype)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("Get_Sp_MovementUpdateSummary '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + SearchCriteria + "','" + SearchText + "','" + jotype + "'");
            Session["updateexternalmo"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        //Code By Rahul
        public ActionResult ExternalMovementReport()
        {
            BE.ExternalMovementReport DDList = new BE.ExternalMovementReport();
            DDList = trainTrackerProvider.GetDDList();
            ViewBag.LineList = new SelectList(DDList.LineListExt, "LineID", "LineName");
            ViewBag.TransporterList = new SelectList(DDList.TransporterListExt, "TransID", "TransName");
            return View();
        }
        public JsonResult GetExternalReport(string FromDate, string ToDate, int SearchCriteria, string SearchText,string CriteriaType)
        {

            List<BE.ExternalMovementReport> GetExternalReport = new List<BE.ExternalMovementReport>();
            DataTable ImpFinalSummary = new DataTable();
            DataTable ImpFinalOut = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            var retureResult = "";
            var retureResult1 = "";

            if (CriteriaType== "SummaryList")
            {
                ImpFinalOut = db.sub_GetDatatable("GetUspMovementWiseMtyComplete '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + SearchCriteria + "','" + SearchText.Replace("'", "''") + "'");
            }
            else
            {
                ImpFinalOut = db.sub_GetDatatable("USP_EXTERNAL_MOVEMENT_REPORT'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + SearchCriteria + "','" + SearchText.Replace("'", "''") + "'");
            }

            ImpFinalSummary = db.sub_GetDatatable("GetUspMovementWiseMtySummary '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + SearchCriteria + "','" + SearchText.Replace("'", "''") + "'");

            Session["MovContWiseComplete"] = ImpFinalOut;
            Session["MovTypeComplete"] = CriteriaType;

            retureResult = JsonConvert.SerializeObject(ImpFinalOut);
            retureResult1 = JsonConvert.SerializeObject(ImpFinalSummary);

            var returnField = new { ContainerList = retureResult, ContainerSummary = retureResult1 };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult ExportToExcelExternalMovemnet(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["updateexternalmo"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExternalMovement.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='" + getMovementICDNew.Columns.Count + "'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='" + getMovementICDNew.Columns.Count + "'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='" + getMovementICDNew.Columns.Count + "'><strong style='font-size: 15px'> External Movements<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='" + getMovementICDNew.Columns.Count + "'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='" + getMovementICDNew.Columns.Count + "'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
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
        public ActionResult ContainerListSearch()
        {

            List<BE.SaveUpdateMovementEntities> ContainerList = new List<BE.SaveUpdateMovementEntities>();
            ContainerList = trainTrackerProvider.GetContainerList();
            //return View();
            //return View(accountingCodeList);
            return PartialView(ContainerList);
        }
        //Code end by rahul

        public JsonResult getListofPenginMov(string SearchCriteria)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable ImpFinalOut = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            if (SearchCriteria == "SummaryList")
            {
                ImpFinalOut = db.sub_GetDatatable("GetUspMovementWiseMty '" + SearchCriteria + "'");
            }
            else
            {
                ImpFinalOut = db.sub_GetDatatable("USP_GetMovementContainerWisePendingList '" + SearchCriteria + "'");
            }

            Session["MovTypeC"] = SearchCriteria;
            Session["MovContWise"] = ImpFinalOut;

            var jsonResult = Json(JsonConvert.SerializeObject(ImpFinalOut), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelListofMvPending(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = new DataTable();
            if (Session["MovTypeC"].ToString() == "SummaryList")
            {
                getMovementICDNew = db.sub_GetDatatable("GetUspMovementWiseMty '" + Session["fromdate"] + "','" + Session["todate"] + "'");
            }
            else
            {
                getMovementICDNew = (DataTable)Session["MovContWise"];
            }
            

            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExternalMovementPendency.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> List of Pending External Movement Report<strong></td></tr>");
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

        public JsonResult ListDOSummary(string BookingNo)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable LorryList = new DataTable();
            DataTable ExpInList = new DataTable();
            DataSet ds = new DataSet();
            CD.DBOperations db = new CD.DBOperations();

            LorryList = db.sub_GetDatatable("USP_GetMovementSummary '" + BookingNo + "'");

            //Session["ListofPenginDoRep"] = ImpFinalOut;

            var jsonLorryList = JsonConvert.SerializeObject(LorryList);

            var returnField = new { LorryList = jsonLorryList };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult UpdateExtMovement(BE.JobOrderMEntities fileData)
        {
            
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            long IgmFileId = 0;
            List<BE.ContainerSize> ContainerSize = new List<BE.ContainerSize>();
            string message = "";
            if (Request.Files.Count > 0)
            {
                //===Upload Data By Select Type=====================
                if (3 == 3)//==For Excel
                {
                    BE.DischargeDateContainerDetails externallist = new BE.DischargeDateContainerDetails();
                    externallist = trainTrackerProvider.GetDropDownListWorkOrder();

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

                                        List<BE.DischargeDateContainerDetails> containerList = new List<BE.DischargeDateContainerDetails>();


                                        if (dt.Rows.Count > 0)
                                        {
                                            foreach (DataRow row in dt.Rows)
                                            {
                                                BE.DischargeDateContainerDetails container = new BE.DischargeDateContainerDetails();
                                                container.SrNo = 0;
                                                BE.SizeEntities size = new BE.SizeEntities();
                                                BE.TypeEntities type = new BE.TypeEntities();
                                                BE.FromEntities from = new BE.FromEntities();
                                                BE.ToEntities to = new BE.ToEntities();
                                                try
                                                {
                                                    size = externallist.SizeList.FirstOrDefault(s => s.Sizec == row["SIZE"].ToString().Trim().ToUpper());
                                                }
                                                catch (Exception ex)
                                                {
                                                    message = "Invalid Size. Please check and correct it.";
                                                }

                                                try
                                                {
                                                    type = externallist.TypeList.FirstOrDefault(s => s.Type == row["TYPE"].ToString().Trim().ToUpper());
                                                }
                                                catch (Exception ex)
                                                {
                                                    message = "Invalid Container Type. Please check and correct it.";
                                                }

                                                try
                                                {
                                                    from = externallist.FromList.FirstOrDefault(s => s.From == row["FROM LOCATION"].ToString().Trim().ToUpper());
                                                }
                                                catch (Exception ex)
                                                {
                                                    message = "Invalid From Location. Please check and correct it.";
                                                }

                                                try
                                                {
                                                    to = externallist.ToList.FirstOrDefault(s => s.To == row["TO LOCATION"].ToString().Trim().ToUpper());
                                                }
                                                catch (Exception ex)
                                                {
                                                    message = "Invalid To Location. Please check and correct it.";
                                                }

                                                string JoNo = "";
                                                try
                                                {
                                                    DataTable dtchk = new DataTable();
                                                    CD.DBOperations db = new CD.DBOperations();
                                                    if (fileData.File_status_Name != "Depot Movement")
                                                    {
                                                        dtchk = db.sub_GetDatatable("getExtJoDetails '" + row["CONTAINER NO"].ToString().Trim() + "','"  + fileData.HBLNumber + "'");
                                                        if (dtchk.Rows.Count > 0)
                                                        {
                                                            JoNo = dtchk.Rows[0]["JoNo"].ToString();

                                                            if (JoNo == "0")
                                                            {
                                                                message = "Invalid Container No.:- " + row["CONTAINER NO"].ToString().Trim();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            message = "Invalid Container No. :- " + row["CONTAINER NO"].ToString().Trim();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        JoNo = fileData.HBLNumber;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    message = "Invalid Container No. :- " + row["CONTAINER NO"].ToString().Trim();
                                                }
                                                try
                                                {
                                                    if (message == "")
                                                    {
                                                        container.ContainerNo = row["CONTAINER NO"].ToString().Trim() + "<input Name=ContainerNo type=hidden id=ContainerNo   value='" + row["CONTAINER NO"].ToString().Trim() + "'>";
                                                        try
                                                        {
                                                            container.Size = row["SIZE"].ToString().Trim() + "<input Name=Size type=hidden id=Sizeid   value=" + size.Sizeid + ">";
                                                        }
                                                        catch(Exception ex) { message = "Invalid Container Size. :- " + row["SIZE"].ToString().Trim(); }
                                                        try
                                                        {
                                                            container.Type = row["TYPE"].ToString().Trim() + "<input Name=Type type=hidden id=TypeID   value='" + type.TypeID + "'>";
                                                        }
                                                        catch (Exception ex) { message = "Invalid Container TYPE. :- " + row["TYPE"].ToString().Trim(); }
                                                        try
                                                        {
                                                            container.From = row["FROM LOCATION"].ToString().Trim() + "<input Name=from type=hidden id=fromid   value='" + from.fromid + "'>";
                                                        }
                                                        catch (Exception ex) { message = "Invalid FROM LOCATION. :- " + row["FROM LOCATION"].ToString().Trim(); }
                                                        try
                                                        {
                                                            container.To = row["TO LOCATION"].ToString().Trim() + "<input Name=To type=hidden id=toid   value='" + to.Toid + "'>";
                                                        }
                                                        catch (Exception ex) { message = "Invalid TO LOCATION. :- " + row["TO LOCATION"].ToString().Trim(); }
                                                        try
                                                        {
                                                            container.DischargeDate = row["MOVEMENT DATE"].ToString().Trim() + "<input Name=To type=hidden id=MovementDate   value='" + row["MOVEMENT DATE"].ToString().Trim() + "'>";
                                                        }
                                                        catch (Exception ex) { message = "Invalid MOVEMENT DATE. :- " + row["MOVEMENT DATE"].ToString().Trim(); }
                                                        try
                                                        {
                                                            container.VehicleNo = row["VEHICLE NO"].ToString().Trim() + "<input Name=To type=hidden id=Vehicle   value='" + row["VEHICLE NO"].ToString().Trim() + "'>";
                                                        }
                                                        catch (Exception ex) { message = "Invalid VEHICLE NO. :- " + row["VEHICLE NO"].ToString().Trim(); }
                                                        try
                                                        {
                                                            container.JoNumber = JoNo + "<input Name=jono type=hidden id=jono   value='" + JoNo + "'>";
                                                        }
                                                        catch (Exception ex) { message = "Invalid JO NO. :- " + JoNo; }

                                                        containerList.Add(container);
                                                    }
                                                }
                                                catch(Exception ex) {
                                                    var returnField1 = new { ContainerList = "", message = "Error occurred.Error details : " + ex.Message + " Container No :-" + container.ContainerNo };
                                                    return new JsonResult() { Data = returnField1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                                }
                                               

                                            }
                                        }

                                        //var jsonResult = JsonConvert.SerializeObject(dt);

                                        if (fname != null || fname != string.Empty)
                                        {
                                            if ((System.IO.File.Exists(fname)))
                                            {
                                                System.IO.File.Delete(fname);
                                            }

                                        }

                                        var returnField = new { ContainerList = containerList, message = message };

                                        return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                                        //return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                    }

                                }
                            }
                        }

                        // Returns message that successfully uploaded  
                        return Json("File imported Successfully!");


                    }
                    catch (Exception ex)
                    {
                        //return Json("Error occurred. Error details: " + ex.Message);
                        var returnField = new { ContainerList = "", message = "Error occurred.Error details: " + ex.Message };
                        return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        //return Json(1);
                        //  return Json("FormData is not supported.");
                    }

                }

            }
           
            return Json(message);
            // return new JsonResult() { Data = JOAttachmentList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult getValidation(string ShipmentJoNo, string Criteria)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";
            DataTable ExpInList = new DataTable();
            DataSet ds = new DataSet();
            CD.DBOperations db = new CD.DBOperations();

            ExpInList = db.sub_GetDatatable("getMovementUpdateValidation '" + ShipmentJoNo + "','" + Criteria + "'");

            if(ExpInList != null)
            {
                if (ExpInList.Rows.Count > 0)
                {
                    Message = ExpInList.Rows[0]["Message"].ToString();
                }
                else
                {
                    Message = "Invalid Details.";
                }
            }
            else
            {
                Message = "Invalid Details.";
            }


            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveExtExcel(List<BE.SaveUpdateMovementEntities> MovData,string Criteria,string ShipmentNo)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            //DataTable dt = new DataTable();
            try
            {
                if (MovData.Count > 0)
                {
                    foreach (BE.SaveUpdateMovementEntities movementEntities in MovData)
                    {
                        i = db.sub_ExecuteNonQuery("update_ext_empty_D '" + movementEntities.ContainerNo + "','" + Convert.ToDateTime(movementEntities.MovementDate).ToString("yyyy-MM-dd HH:mm") + "','" + movementEntities.Vehicle + "','" + movementEntities.fromid + "','" + movementEntities.toid + "','" + movementEntities.Sizeid + "','" + movementEntities.TypeID + "','" + "" + "','" + movementEntities.jono + "','" + UserID + "','" + "" + "','" + "Excel" + "','" + Criteria + "'");
                        if (i == 0)
                        {
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                i = 0;
            }
            
            //Master();

            return Json(i);
        }

        [HttpPost]
        public JsonResult getPendingListExt(string JoNo, string ShipmentNo)
        {
            string Message = "";
            List<BE.DischargeDateContainerDetails> containerList = new List<BE.DischargeDateContainerDetails>();
            try
            {
                if (JoNo == null || JoNo == "")
                {
                    JoNo = "0";
                }

                if (ShipmentNo == null)
                {
                    ShipmentNo = "";
                }

                DataTable dtContainerDet = new DataTable();
                CD.DBOperations db = new CD.DBOperations();
                dtContainerDet = db.sub_GetDatatable("getPendingContainerListExt '" + JoNo + "','" + ShipmentNo + "'");

                if (dtContainerDet != null)
                {
                    if (dtContainerDet.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtContainerDet.Rows)
                        {
                            BE.DischargeDateContainerDetails container = new BE.DischargeDateContainerDetails();
                            container.Criteria = row["Criteria"].ToString();
                            container.JONO = Convert.ToInt32(row["JoNo"]);
                            container.ShipmentNo = row["ShipmentNo"].ToString();
                            container.JoNumber = row["JoNo"].ToString() + "<input Name=JONO type=hidden id=JONO   value='" + row["JoNo"].ToString() + "'>"; ;
                            container.JoDate = Convert.ToString(row["JoDate"]);
                            container.ContainerNo = row["ContainerNo"].ToString();
                            container.Size = Convert.ToString(row["Size"]);
                            container.From = Convert.ToString(row["FromLocation"]);
                            container.To = Convert.ToString(row["ToLocation"]);

                            containerList.Add(container);
                        }
                    }
                    else
                    {
                        Message = "No Data Found.";
                    }
                }
                else
                {
                    Message = "No Data Found.";
                }
            }
            catch (Exception ex)
            {
                Message = "Invalid Jo Number.";
            }

            var returnField = new { ContainerList = containerList, message = Message };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult RemovePendingCont(List<BE.DischargeDateContainerDetails> ExtContData)
        {
            int i = 0;
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            //DataTable dt = new DataTable();
            try
            {
                if (ExtContData.Count > 0)
                {
                    foreach (BE.DischargeDateContainerDetails movementEntities in ExtContData)
                    {
                        i = db.sub_ExecuteNonQuery("USP_CancelExtMovCont '" + movementEntities.ContainerNo + "','" + movementEntities.JONO + "'");

                        if (i == 0)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                i = 0;
            }

            //Master();

            return Json(i);
        }

        public ActionResult ExportToExcelListofMvComplete(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = new DataTable();
            if (Session["MovTypeComplete"].ToString() == "SummaryList")
            {
                getMovementICDNew = (DataTable)Session["MovContWiseComplete"];
            }
            else
            {
                getMovementICDNew = (DataTable)Session["MovContWiseComplete"];
            }


            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExternalMovSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> List of Complete External Movement Report<strong></td></tr>");
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

        public ActionResult ExternalMovementHistory()
        {
            return View();
        }

        public JsonResult GetExtContainerHistory(string ContainerNo)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable LorryList = new DataTable();
            DataTable ExpInList = new DataTable();
            DataSet ds = new DataSet();
            CD.DBOperations db = new CD.DBOperations();

            LorryList = db.sub_GetDatatable("getExtMovContainerHistory '" + ContainerNo + "'");

            Session["ListExtHistory"] = LorryList;
            Session["ExtContainerNo"] = ContainerNo;

            var jsonLorryList = JsonConvert.SerializeObject(LorryList);

            return Json(jsonLorryList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportToExcelExtHistory(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = new DataTable();
            getMovementICDNew = (DataTable)Session["ListExtHistory"];

            string Tittle = "Container No " + Session["ExtContainerNo"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExternalContainerHistory.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> External Movement Container History Report<strong></td></tr>");
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

        public ActionResult UpdateContainerStatus()
        {
            return View();
        }

        public ActionResult GetLocationDetails(string containerno)
        {
            List<BE.UpdateExternalMovement> LocationDetails = new List<BE.UpdateExternalMovement>();
            LocationDetails = trainTrackerProvider.containerstatus(containerno);
            var JsonResult = Json(LocationDetails, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public JsonResult savestatus(BE.UpdateExternalMovement UpdateExternalMovement)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            int retVal = 0;
            retVal = db.sub_ExecuteNonQuery("USP_changecontainerstatus '" + UpdateExternalMovement.containerno + "','" + UpdateExternalMovement.status + "'");
            //Master();

            return Json(retVal);
        }

        public ActionResult WHLLOLOCodeco()
        {
             
            return View();
        }
        public JsonResult ImportFile()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            BE.UpdateExternalMovement DriverPaymentList = new BE.UpdateExternalMovement();
            List<BE.UpdateExternalMovement> DriverPaymentRecoList = new List<BE.UpdateExternalMovement>();
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


                                    DriverPaymentDT.Columns.Add(new DataColumn("ContainerNo",typeof(string)));
                                    DriverPaymentDT.Columns.Add(new DataColumn("Size", typeof(int)));
                                    DriverPaymentDT.Columns.Add(new DataColumn("ContainerType", typeof(string)));
                                    DriverPaymentDT.Columns.Add(new DataColumn("ActivityDate", typeof(DateTime)));
                                    DriverPaymentDT.Columns.Add(new DataColumn("Activity", typeof(string)));
                                    //DriverPaymentDT.Columns.Add("Size");
                                    //DriverPaymentDT.Columns.Add("ContainerType");
                                    //DriverPaymentDT.Columns.Add("ActivityDate");
                                    //DriverPaymentDT.Columns.Add("Activity");

                                    //DriverPaymentDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        if (row[0].ToString() != "" && row[1].ToString() != "" && row[2].ToString() != "" && row[3].ToString() != "" && row[4].ToString() != "")
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

                                            dar["ContainerNo"] = row[0].ToString().ToUpper();
                                            dar["Size"] = row[1].ToString().ToUpper();
                                            dar["ContainerType"] = row[2].ToString();
                                            dar["ActivityDate"] = row[3].ToString();
                                            dar["Activity"] = row[4].ToString();
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

                                        DriverPaymentRecoList = trainTrackerProvider.UpdateDriverPaymentDetails(DriverPaymentDT, Userid);

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

        public ActionResult DownloadLoloDetails(string Activity)
        {
            string strComa = ",";
            string message = "";
            string strFileName = "WHLLolo" + DateTime.Now.ToString("dd-MMM-yyyy")+".txt";
            CD.DBOperations db = new CD.DBOperations();

            string attachment = "attachment; filename=" + strFileName;
            StringBuilder strb = new StringBuilder();
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "text/csv";
            Response.AddHeader("Pragma", "public");

            DataTable CompanyMaster = new DataTable();
            if (Activity == "NCL")
            {
                CompanyMaster = db.sub_GetDatatable("USP_WHL_LOLO_CODEOC_NCL");
            }
            else
            {
                CompanyMaster = db.sub_GetDatatable("USP_WHL_LOLO_CODEOC_ARYAN");
            }
            
            string Strfst = "";
            for (int i = 0; i <= CompanyMaster.Rows.Count - 1; i++)
            {
                Strfst = Strfst + CompanyMaster.Rows[i][0];
                strb.Append(CompanyMaster.Rows[i][0].ToString());
                strb.AppendLine();
            }

            Response.Write(strb.ToString());
            Response.Flush();
            Response.End();

            return View();

        }

        public ActionResult DownloadITemplateOnly()
        {

            string fileName = "~/Format/ExternalMovementSummary.xlsx";
            if (fileName != "")
            {
                string filePath = fileName;
                string Fname = "External Movement Summary(Job Order).xlsx";

                Response.ContentType = "doc/docx";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Fname + "\"");

                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }
            return View();
        }

        public ActionResult DownloadUpTemplateOnly()
        {

            string fileName = "~/Format/UpdateExternalMovement.xlsx";
            if (fileName != "")
            {
                string filePath = fileName;
                string Fname = "Update External Movement.xlsx";

                Response.ContentType = "doc/docx";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Fname + "\"");

                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }
            return View();
        }


    }
}