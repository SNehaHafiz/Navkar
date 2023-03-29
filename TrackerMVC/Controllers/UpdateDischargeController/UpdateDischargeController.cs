using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using DP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrailerTransport;
using train = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrainMaster;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.LoadingPlan;
using SB = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ExportSBBL;

using BMM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.BLDataManager;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using HC = TrackerMVCDataLayer.Helper;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;

namespace TrackerMVC.Controllers.UpdateDischargeController
{
    [UserAuthenticationFilter]
    public class UpdateDischargeController : Controller
    {
        // GET: UpdateDischarge

        DP.TrailerDataProvider objTrailerProvider = new DP.TrailerDataProvider();
        train.UpdateDischargeDate trainTrackerProvider = new train.UpdateDischargeDate();
        BMM.GenerateBL BL = new BMM.GenerateBL();
        BM.LoadingPlan LP = new BM.LoadingPlan();
        SB.ExportSBBL SB = new SB.ExportSBBL();

        // GET: TrainDeparture_From_Port
        public ActionResult UpdateDischargeDate()
        {
            List<BE.Port> portList = new List<BE.Port>();
            portList = trainTrackerProvider.GetPortList();
            ViewBag.portList = portList;

            List<BE.DischargeDateContainerDetails> containerList = new List<BE.DischargeDateContainerDetails>();
            if (portList != null && portList.Count != 0)
            {
                containerList = trainTrackerProvider.GetContainerForUpdateDischargeDate(portList[0].PortID, Convert.ToInt32(Session["Tracker_userID"]));
            }
            ViewBag.containerList = containerList;
            List<string> trainNO = new List<string>();
            trainNO = trainTrackerProvider.GetTrainNOForUpdateDischargeDate();
            ViewBag.TrainNO = new SelectList(trainNO, "trainNO"); ;
            return View(containerList);
        }

        public JsonResult GetContainerForUpdateDischargeDate(int portID)
        {
            List<BE.DischargeDateContainerDetails> containerList = new List<BE.DischargeDateContainerDetails>();
            containerList = trainTrackerProvider.GetContainerForUpdateDischargeDate(portID, Convert.ToInt32(Session["Tracker_userID"]));
            ViewBag.containerList = containerList;
            return Json(containerList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateSelectedContainer(int joNo, string containerNo, bool isChecked)
        {
            int totalCount = 0;
            totalCount = trainTrackerProvider.UpdateSelectedContainer(joNo, containerNo, isChecked, Convert.ToInt32(Session["Tracker_userID"]));
            return Json(totalCount, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTotalCountForUpdateDischargeDate()
        {
            int totalCount = 0;
            totalCount = trainTrackerProvider.GetTotalCountForUpdateDischargeDate(Convert.ToInt32(Session["Tracker_userID"]));
            return Json(totalCount, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateDischargeDateForSelectedContainer(string dischargeDate, string containerNo)
        {
            int totalCount = 0;
            totalCount = trainTrackerProvider.UpdateDischargeDateForSelectedContainer(dischargeDate, containerNo, Convert.ToInt32(Session["Tracker_userID"]));
            return Json(totalCount, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateTrainNoForSelectedContainer(string trainNo)
        {
            int totalCount = 0;
            if (trainNo != " " && trainNo != "" && trainNo != null && trainNo != "undefined")
            {
                totalCount = trainTrackerProvider.UpdateTrainNoForSelectedContainer(trainNo, Convert.ToInt32(Session["Tracker_userID"]));
            }


            return Json(totalCount, JsonRequestBehavior.AllowGet);
        }

        ////////by durga//////////
        public ActionResult UpdateDischargeDateDetails()
        {
            string ContainerNo = "";
            List<BE.DischargeDateContainerDetails> containerList = new List<BE.DischargeDateContainerDetails>();
            containerList = trainTrackerProvider.GetContainerUpdateDischargeData(ContainerNo);

            return View(containerList);
        }
        [HttpPost]
        public JsonResult ContainerWiseUpdateDischargeData(string containerNo)
        {
            List<BE.DischargeDateContainerDetails> containerList = new List<BE.DischargeDateContainerDetails>();
            containerList = trainTrackerProvider.GetContainerUpdateDischargeData(containerNo);

            return Json(containerList, JsonRequestBehavior.AllowGet);

            // return View(containerList);
        }

        public JsonResult SaveDischargeData(List<BE.DischargeDateContainerDetails> DischargeList)
        {
            string message = "";

            int userId = Convert.ToInt32(Session["Tracker_userID"]);

            //  message = BL.AddJobOrderData(viewModel, containerData, userId);
            // ViewBag.Message = message;
            DataTable DischargeListDT = new DataTable();

            DischargeListDT.Columns.Add("JONO");
            DischargeListDT.Columns.Add("ContainerNO");
            DischargeListDT.Columns.Add("DischargeDate");
            DischargeListDT.Columns.Add("userID");

            DischargeListDT.TableName = "PT_UpdateDischargeDate";
            foreach (BE.DischargeDateContainerDetails item in DischargeList)
            {
                DataRow row = DischargeListDT.NewRow();
                row["JONO"] = item.JONO;
                row["ContainerNO"] = item.ContainerNo;
                row["DischargeDate"] = Convert.ToDateTime(item.DischargeDate).ToString("yyyy-MM-dd HH:mm:ss");
                row["userID"] = userId;
                DischargeListDT.Rows.Add(row);
            }

            message = trainTrackerProvider.UpdateDischargeDateJO(DischargeListDT, userId);

            return Json(message);
        }
        public JsonResult ImportFile()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            BE.DischargeDateContainerDetails DischargeList = new BE.DischargeDateContainerDetails();

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

                                    DataTable DischargeListDT = new DataTable();

                                    DischargeListDT.Columns.Add("JONO");
                                    DischargeListDT.Columns.Add("ContainerNO");
                                    DischargeListDT.Columns.Add("DischargeDate");
                                    DischargeListDT.Columns.Add("userID");

                                    DischargeListDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        if (row["Discharge Date"].ToString() != "" && row["Container No"].ToString() != "")
                                        {

                                            if (DateTime.TryParse(row["Discharge Date"].ToString(), out dDate))
                                            {
                                                String.Format("{0:d/MM/yyyy}", dDate);
                                            }
                                            else
                                            {
                                                var AlertValue = new { ColumnData = "Discharge Date", ValueData = row["Discharge Date"].ToString() };
                                                var returnField = new { Issuedata = AlertValue, message = "" };

                                                return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                                // return Json(1); // <-- Control flow goes here
                                            }
                                            int length = row["Container No"].ToString().Length;
                                            if (length != 11)
                                            {

                                                var AlertValue = new { ColumnData = "Container No.", ValueData = row["Container No"].ToString() };
                                                var returnField = new { Issuedata = AlertValue, message = "" };

                                                return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                                // return Json(1);
                                            }
                                            // string container = row["Container No"].ToString().ToUpper();
                                            //  string dichargedate = row["Discharge Date"].ToString();

                                            DataRow dar = DischargeListDT.NewRow();
                                            dar["JONO"] = 0;
                                            dar["ContainerNO"] = row["Container No"].ToString().ToUpper();
                                            dar["DischargeDate"] = Convert.ToDateTime(row["Discharge Date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                            dar["userID"] = Userid;
                                            DischargeListDT.Rows.Add(dar);
                                        }

                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }

                                    if (DischargeListDT != null)
                                    {

                                        message = trainTrackerProvider.UpdateDischargeDateJO(DischargeListDT, Userid);

                                        if (message == "Records updated successfully")
                                        {
                                            message = "Records imported and updated successfully";
                                        }
                                        //   trainTrackerProvider.UpdateDischargeDateJO(DischargeListDT, userId);
                                        //  DischargeList = trainTrackerProvider.UpdateDischargeDateJO(DischargeListDT, Userid);

                                        var returnField = new { Issuedata = 1, message = message };

                                        return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

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
        /////end by durga/////////

        // Codes By prashant
        public ActionResult LoadedContainerArrival()
        {


            return View();
        }


        public JsonResult getLoadedContainerArrival(string SearchCriteria, string SearchText, DateTime FromDate, DateTime ToDate)
        {

            //List<BE.LoadedContainerArrival> ContainerArrivalLIst = new List<BE.LoadedContainerArrival>();
            //ContainerArrivalLIst = trainTrackerProvider.GetLoadedContainerDetails(SearchCriteria, SearchText, FromDate, ToDate);
            DataTable ContainerArrival = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ContainerArrival = db.sub_GetDatatable("SP_GetImportArrivalAllDetails '" + SearchCriteria + "','" + SearchText + "','" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            //DataTable dt = converter.ToDataTable(upcomingList);
            Session["ContainerArrival"] = ContainerArrival;
            Session["fromdate"] = Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm");
            Session["todate"] = Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm");
            var jsonResult = Json(JsonConvert.SerializeObject(ContainerArrival), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }

        [HttpPost]
        public ActionResult ImportPortWiseMovementDetails()
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("Import_PortWise_movement_tues_report '" + Session["fromdate"] + "','" + Session["todate"] + "'");
            Session["Import_PortWise_movement_tues_report"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelImportSummary()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ContainerArrival = (DataTable)Session["ContainerArrival"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            //GridView gridview = new GridView();
            //gridview.DataSource = ContainerArrival;
            //gridview.DataBind();
            DataTable Summarry = new DataTable();
            //HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            Summarry = db.sub_GetDatatable("Import_PortWise_movement_tues_report'" + Session["fromdate"] + "','" + Session["todate"] + "'");
            GridView gridview = new GridView();
            GridView gridview1 = new GridView();
            GridView gridview2 = new GridView();
            gridview1.DataSource = ContainerArrival;
            gridview1.DataBind();
            gridview2.DataSource = Summarry;
            gridview2.DataBind();
            if (gridview1.Rows.Count != 0)
            {
                PrepareForExport(gridview1);
            }
            if (gridview2.Rows.Count != 0)
            {
                PrepareForExport(gridview2);

            }

            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Controls.Add(gridview1);

            tr1.Cells.Add(cell1);

            TableCell cell3 = new TableCell();

            cell3.Controls.Add(gridview2);

            TableCell cell2 = new TableCell();

            cell2.Text = " ";


            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);

            TableRow tr3 = new TableRow();

            tr3.Cells.Add(cell3);

            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.Rows.Add(tr3);



            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=LoadedContainerArrival.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Import Loaded Container Arrival<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    tb.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        protected void PrepareForExport(GridView Gridview)
        {

            // Gridview.AllowPaging = Convert.ToBoolean(rbPaging.SelectedItem.Value);

            Gridview.AllowPaging = false;
            //GridView2.AllowPaging = false;
            //btnview_Click1(this, null);
            // Gridview.DataBind();



            //Change the Header Row back to white color

            Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF");



            //Apply style to Individual Cells

            for (int k = 0; k < Gridview.HeaderRow.Cells.Count; k++)
            {

                Gridview.HeaderRow.Cells[k].Style.Add("background-color", "green");

            }



            for (int i = 0; i < Gridview.Rows.Count; i++)

            {

                GridViewRow row = Gridview.Rows[i];

                row.BackColor = System.Drawing.Color.White;

                row.Attributes.Add("class", "textmode");

                if (i % 2 != 0)
                {

                    for (int j = 0; j < Gridview.Rows[i].Cells.Count; j++)
                    {

                        row.Cells[j].Style.Add("background-color", "#C2D69B");

                    }

                }

            }

        }

        public ActionResult IGMDetails()
        {


            return View();
        }

        [HttpPost]
        public JsonResult getIGMDetails(string SearchText, string SearchType)
        {

            List<BE.IGMDetailsEntities> IGMDetailLIst = new List<BE.IGMDetailsEntities>();
            IGMDetailLIst = trainTrackerProvider.GetIGMdetails(SearchText, SearchType);
            //return Json(IGMDetailLIst);

            // return new JsonResult() { Data = IGMDetailLIst, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            var jsonResult = Json(IGMDetailLIst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult getLineWiseSummaryDet(string SearchText, string SearchType)
        {

            DataTable getLineWiseSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getLineWiseSummary = db.sub_GetDatatable("GetIgmDetSummaryList  '" + SearchText + "','" + SearchType + "'");
            Session["getLineWiseSummary"] = getLineWiseSummary;
            Session["IGMnumber"] = SearchText;
            Session["SearchType"] = SearchType;
            var json = JsonConvert.SerializeObject(getLineWiseSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        // Codes By prashant
        // Codes By prashant

        public ActionResult SearchOnIGMNumber()
        {

            return View();
        }

        [HttpPost]
        public JsonResult AjaxGetItemDetails(string IGMNumber, string BLNumber)
        {
            BE.IGMItemNumberListEntities IGMDetails = new BE.IGMItemNumberListEntities();
            IGMDetails = trainTrackerProvider.SearchIGMNoDetails(IGMNumber, BLNumber);
            //return Json(IGMDetails);
            return Json(IGMDetails);

        }


        [HttpPost]
         

        public ActionResult AjaxItemInvoicedetails(string IGMNumber, string BLNumber)
        {
            DataTable dt1 = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt1 = db.sub_GetDatatable("USP_ItemSearchInvoiceDetails'" + IGMNumber + "','" + BLNumber + "'");
            Session["InvoiceDetails1"] = dt1;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt1);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        public ActionResult AjaxItemInvoicontainer(string IGMNumber, string BLNumber)
        {
            DataTable dt1 = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt1 = db.sub_GetDatatable("sp_SearchIGMItem_New'" + IGMNumber + "','" + BLNumber + "'");
            Session["SearchAssessmnet_CreditNote"] = dt1;

            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt1);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        [HttpPost]
        public ActionResult AjaxITemSearchSummarydetails(string IGMNumber, string BLNumber)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_IGM_ITEM_WISE_SEARCH'" + IGMNumber + "','" + BLNumber + "'");
            Session["IGM_ITEM_WISE_SEARCH"] = dt;
            Session["IGMNumber"] = IGMNumber;
            Session["BLNumber"] = BLNumber;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        // Codes By Prashant

        public ActionResult ImportSearch()
        {

            return View();
        }

        [HttpPost]
        public JsonResult ajaxImportSearchlistdetails(string ContainerNo, string Jono)
        {
            string ContainerNumber = Convert.ToString(ContainerNo);
            string Jonumber = Convert.ToString(Jono);

            BE.ImportSearchEntities ImportSearchdetails = new BE.ImportSearchEntities();
            ImportSearchdetails = trainTrackerProvider.GetImportSearchDetails(ContainerNumber, Jonumber);

            List<BE.GetIGMDetailsOnJONO> ImportSearchdetailsList = new List<BE.GetIGMDetailsOnJONO>();
            ImportSearchdetailsList = trainTrackerProvider.SearchImportSearchList(Jonumber, ContainerNumber);


            var returnField = new { ImportList = ImportSearchdetails, ImportDetails = ImportSearchdetailsList };
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult AjaxImportSearchHoldingList(string Jono1, string ContainerNo1)
        {
            List<BE.HoldDetailsEntities> Holdingdetails = new List<BE.HoldDetailsEntities>();
            Holdingdetails = trainTrackerProvider.SearchImportSearchHoldingList(Jono1, ContainerNo1);
            var jsonResult = Json(Holdingdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public JsonResult ajaxJoNoList(string ContainerNo3)
        {
            List<BE.JobOrderDEntities> Shippers = new List<BE.JobOrderDEntities>();
            Shippers = trainTrackerProvider.getjoborderlist(ContainerNo3);
            return new JsonResult() { Data = Shippers, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public ActionResult TripEntry()
        {


            return View();
        }

        [HttpPost]
        public JsonResult Ajaxgetdriverbytrailer(string trailername)
        {

            BE.TrailersEntities Trailerno = new BE.TrailersEntities();
            Trailerno = trainTrackerProvider.getdrivername(trailername);
            return new JsonResult() { Data = Trailerno, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }



        [HttpPost]
        public JsonResult AjaxCheckTrailerPermit(string trailername)
        {

            string message = "";
            message = trainTrackerProvider.CheckTrailerPermit(trailername);
            return Json(message);

        }

        [HttpPost]
        public JsonResult AjaxInsertTPdetails(string TPdate, int Trailerid, int Driverid, string amount, string TPlocation, string TPfor)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            message = trainTrackerProvider.InsertTPdetails(TPdate, Trailerid, Driverid, amount, Userid, TPlocation, TPfor);
            return Json(message);

        }


        [HttpPost]
        public JsonResult ajaxTPSummaryDetails()
        {

            List<BE.TPEntryEntities> TPdetails = new List<BE.TPEntryEntities>();
            TPdetails = trainTrackerProvider.GetTPList();

            var jsonResult = Json(TPdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }


        public ActionResult PrintTPDetails(int trailerno, int tpNo)
        {
            BE.TPEntryEntities PT = new BE.TPEntryEntities();
            PT = trainTrackerProvider.GetPrintTPList(trailerno, tpNo);
            ViewBag.TPDate = PT.TPdate;
            ViewBag.TrailerName = PT.trailername;
            ViewBag.drivername = PT.drivername;
            ViewBag.startdate = PT.startdate;
            ViewBag.enddate = PT.enddate;
            ViewBag.amount = PT.amount;
            ViewBag.AmountWords = PT.AmountWords;
            ViewBag.TPNumber = PT.TPNumber;


            return PartialView();
        }



        public ActionResult ContainerSummary()
        {

            return View();
        }

        public JsonResult AjaxGetContainerList(string ContainerNo)
        {
            List<BE.ContainerDetailsEntities> Containerdetails = new List<BE.ContainerDetailsEntities>();
            Containerdetails = trainTrackerProvider.GetContainerDetails(ContainerNo);
            var jsonResult = Json(Containerdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public ActionResult Ajaxinsertcontainer(string containerNo, int jono, string shippingline, string Indate, string size, string type, string Examine)
        {
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            message = trainTrackerProvider.SaveContainerDetails(containerNo, jono, Examine, userId);

            return Json(message);
        }

        public ActionResult TPapproveSummary()
        {
            List<BE.TPEntryEntities> TPApprovedetails = new List<BE.TPEntryEntities>();
            TPApprovedetails = trainTrackerProvider.GetTPList();
            return View(TPApprovedetails);


        }

        public ActionResult TPapproveInsertdetails(int trailerno)
        {
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            message = trainTrackerProvider.TPApproveSaveDetails(trailerno, userId);
            return Json(message);
        }


        public ActionResult TPDetailsPrint()
        {
            List<BE.TPEntryEntities> TPApproveprintdetails = new List<BE.TPEntryEntities>();
            TPApproveprintdetails = trainTrackerProvider.GetApprovedTPList();
            return View(TPApproveprintdetails);

        }
        public ActionResult CheckCustomValidate(int jono1, string containerNo1)
        {
            string message = "";

            message = trainTrackerProvider.CheckCustomExamineDetails(jono1, containerNo1);

            return Json(message);

        }


        public JsonResult AjaxGetCustomExamine()
        {
            List<BE.ContainerDetailsEntities> Customdetails = new List<BE.ContainerDetailsEntities>();
            Customdetails = trainTrackerProvider.GetCustomExamineFetchDetails();
            var jsonResult = Json(Customdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult PortInSummary()
        {

            List<BE.TrainMaster> trainnolist = new List<BE.TrainMaster>();
            trainnolist = trainTrackerProvider.GetTrainNumberExportDetails();
            ViewBag.TrainList = new SelectList(trainnolist, "TrainID", "TrainNO");
            return View();
        }

        public ActionResult AjaxGetPortInDetails(string trainno)
        {
            List<BE.PortInEntities> PortInDetailslist = new List<BE.PortInEntities>();
            PortInDetailslist = trainTrackerProvider.GetPortInDetails(trainno);
            var jsonResult = Json(PortInDetailslist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }


        public ActionResult UpdatePortIN(List<BE.PortInEntities> Portin)
        {

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("EntryID");
            dataTable.Columns.Add("ContainerNo");


            dataTable.TableName = "PT_PortInDetails";
            foreach (BE.PortInEntities item in Portin)
            {
                DataRow row = dataTable.NewRow();



                row["EntryID"] = item.EntryID;
                row["ContainerNo"] = item.ContainerNo;


                dataTable.Rows.Add(row);
            }
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);

            // BE.JobOrderMEntities ValidationList = new BE.JobOrderMEntities();
            string Message = "";
            Message = trainTrackerProvider.UpdatePortINList(dataTable, UserID);
            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };



        }

        public ActionResult TPCloseSummary()
        {
            List<BE.TPEntryEntities> TPCloseDetails = new List<BE.TPEntryEntities>();
            TPCloseDetails = trainTrackerProvider.GetTPCloseDetails();
            return View(TPCloseDetails);

        }

        //public ActionResult ajaxAddTPCloseDetails(int TpNo, string startdate, string enddate)
        //{
        //    string message = "";
        //    message = trainTrackerProvider.AddToCloseDetails(TpNo, startdate, enddate);
        //    return Json(message);

        //}
        public JsonResult ajaxAddTPCloseDetails(TrackerMVCEntities.BusinessEntities.TPEntryEntities TPDetails, HttpPostedFileBase file)
        {
            List<BE.TPEntryEntities> CompanyticketList = new List<BE.TPEntryEntities>();
            string message = "";
            if (TPDetails.TPNumber != null)
            {

                var TPNumber = TPDetails.TPNumber;
                var StartDate = TPDetails.startdate;
                var EndDate = TPDetails.enddate;

                string contentType;
                string fname;
                fname = file.FileName;

                byte[] attachByte = null;
                BinaryReader rdr = new BinaryReader(file.InputStream);
                attachByte = rdr.ReadBytes((int)file.ContentLength);
                contentType = MimeMapping.GetMimeMapping(fname);

                CompanyticketList = trainTrackerProvider.AddToCloseDetails(TPDetails, attachByte, contentType);

            }

            return Json(message);
        }

        //Codes start By Prashant 03 July 2019


        public ActionResult VehicleInOutDetails()
        {

            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = trainTrackerProvider.getVehicleActivity();
            List<BE.ActivityCycle> Activitycycle = new List<BE.ActivityCycle>();
            Activitycycle = LP.GetActivitVehicle();
            ViewBag.Activitycycle = new SelectList(Activitycycle, "ActivitycycleID", "ActivitycycleName");
            List<BE.ContainerCountEntities> GetContainercount = new List<BE.ContainerCountEntities>();
            GetContainercount = trainTrackerProvider.GetContainerCount();

            List<BE.ContainerCountEntities> GetContainerType = new List<BE.ContainerCountEntities>();
            GetContainerType = trainTrackerProvider.GetContainerType();

            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");
            ViewBag.ContainerCount = new SelectList(GetContainercount, "ContainerCountID", "ContainerCountSize");
            ViewBag.Containertype = new SelectList(GetContainerType, "ContainerCountID", "ContainerCountSize");

            return View();
        }


        public ActionResult AjaxSaveVehicleInOutDetails(string ActivityDate, int ActivityName, int FromLocation, int ToLocation, string trailerid, string party, string Remarks, string Containercount, string INout, string ContainerNo1, string ContainerNo2, string Activitycycle, string ContainerType)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = trainTrackerProvider.SaveVehicleInOut(ActivityDate, ActivityName, FromLocation, ToLocation, trailerid, party, Remarks, UserID, Containercount, INout, ContainerNo1, ContainerNo2, Activitycycle, ContainerType);
            return Json(message);
        }


        // Codes By Prashant 4 july 2019

        public ActionResult AdditionalMovementRequest()
        {
            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = LP.getActivity();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");
            return View();
        }

        public ActionResult AjaxSaveAdditionalMovementRequest(string Containerno, string VoucherNo, int activity, int FromLocation, int Tolocation, string remarks, string Fuel, string kilometer)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = trainTrackerProvider.SaveAdditionMovmentrequest(Containerno, VoucherNo, activity, FromLocation, Tolocation, remarks, Fuel, kilometer, UserID);
            return Json(message);
        }

        //Code End By prashant

        public ActionResult AjaxGetAdditionalMovementRequest()
        {
            List<BE.AdditionalMovmentrequestEntities> AdditionalMovementlist = new List<BE.AdditionalMovmentrequestEntities>();
            AdditionalMovementlist = trainTrackerProvider.GetAdditionalMovementRequest();
            var jsonResult = Json(AdditionalMovementlist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult UpdateApproveRequest(int RequestID, string fuel)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = trainTrackerProvider.ApproveRequest(RequestID, fuel, UserID);
            return Json(message);

        }
        public ActionResult ajaxCheckVouchernO(string Voucherno)
        {
            string message = "";

            message = trainTrackerProvider.CheckAdditionalMovment(Voucherno);

            return Json(message);

        }


        //Codes for TP Summary 

        public ActionResult TPClosedetails()
        {
            List<BE.TPEntryEntities> TPCloseSummary = new List<BE.TPEntryEntities>();
            TPCloseSummary = trainTrackerProvider.GetTPCloseDetailsList();
            return View(TPCloseSummary);


        }
        [HttpPost]
        public FileResult DownLoadFile(int TPNumber)
        {

            BE.TPEntryEntities CompanyticketList = new BE.TPEntryEntities();
            CompanyticketList = trainTrackerProvider.getAttachment(TPNumber);
            return File(CompanyticketList.Attachment, CompanyticketList.ContentType);
        }



        public ActionResult GetTPHistory(int trailerno)
        {
            List<BE.TPEntryEntities> TPHistorySummary = new List<BE.TPEntryEntities>();
            TPHistorySummary = trainTrackerProvider.GetTPHistory(trailerno);
            var jsonResult = Json(TPHistorySummary, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Codes by Manish
        public ActionResult InternalFuelStock()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            List<BE.fuelType> fuelType = new List<BE.fuelType>();
            fuelType = trainTrackerProvider.getfuelType();
            ViewBag.FuelType = new SelectList(fuelType, "Fuelid", "FuelType");
            List<BE.FuelVendorMaster> FuelVendorMaster = new List<BE.FuelVendorMaster>();
            FuelVendorMaster = trainTrackerProvider.getFuelVendorMaster();
            ViewBag.VendorName = new SelectList(FuelVendorMaster, "fuel_Vendorid", "fuelVendorName");


            // Code By Prashant
            List<BE.CostCenterFor> CostCenterFor = new List<BE.CostCenterFor>();
            CostCenterFor = trainTrackerProvider.getCostCenterFor();
            ViewBag.CostCenterFor = new SelectList(CostCenterFor, "CodeCenterID", "CodeCenterType");
            //Code End by Prashant
            List<BE.CostCenter> CostCenter = new List<BE.CostCenter>();
            CostCenter = trainTrackerProvider.getCostCenter();
            ViewBag.CostCenter = new SelectList(CostCenter, "Costid", "CostCenterName");
            return View();
        }

        public ActionResult Save(string txtentryDate, float txtFuelQty, int txtRate, int ddlVendorName, string trailerid, string ddlFuelType, int ddlCostCenter, string CostCenterFor, string ddlFuelFor)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = trainTrackerProvider.SaveInternalFuelStock(txtentryDate, txtFuelQty, txtRate, ddlVendorName, trailerid, ddlFuelType, UserID, ddlCostCenter, CostCenterFor, ddlFuelFor);
            return Json(message);
        }

        public ActionResult UpdateBillNOForInternalstock(string BillNo, string AmountForBill, string FUelID, string QtyModify, string RateModify)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = trainTrackerProvider.updatesTockBillNo(BillNo, AmountForBill, FUelID, QtyModify, RateModify);
            return Json(message);
        }



        public ActionResult FuelConsumption()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            List<BE.fuelType> fuelType = new List<BE.fuelType>();
            fuelType = trainTrackerProvider.getfuelType();
            ViewBag.FuelType = new SelectList(fuelType, "Fuelid", "FuelType");
            List<BE.FuelVendorMaster> FuelVendorMaster = new List<BE.FuelVendorMaster>();
            FuelVendorMaster = trainTrackerProvider.getFuelVendorMaster();
            ViewBag.VendorName = new SelectList(FuelVendorMaster, "fuel_Vendorid", "fuelVendorName");
            List<BE.CostCenter> CostCenter = new List<BE.CostCenter>();
            CostCenter = trainTrackerProvider.getCostCenter();
            ViewBag.CostCenter = new SelectList(CostCenter, "Costid", "CostCenterName");
            List<BE.DriversEntities> Drivers = new List<BE.DriversEntities>();
            Drivers = trainTrackerProvider.getICD_Internal_Driver(); ViewBag.Drivers = new SelectList(Drivers, "driverID", "driverName");
            List<BE.VehicleTypeEntities> VehicleTypeList = new List<BE.VehicleTypeEntities>();
            BE.TrailerEntities TrailerList = objTrailerProvider.getDropDownList();
            VehicleTypeList = TrailerList.VehicleTypeList;
            ViewBag.VehicleTypeList = new SelectList(VehicleTypeList, "VehicleTypeID", "VehicleType");
            // Code By Prashant
            List<BE.CostCenterFor> CostCenterFor = new List<BE.CostCenterFor>();
            CostCenterFor = trainTrackerProvider.getCostCenterFor();
            ViewBag.CostCenterFor = new SelectList(CostCenterFor, "CodeCenterID", "CodeCenterType");
            //Code End by Prashant
            //Code For Sub Group 

            List<BE.PurposeEntites> PurposeList = new List<BE.PurposeEntites>();
            PurposeList = trainTrackerProvider.getVehiclePurpose();
            ViewBag.PurposeID = new SelectList(PurposeList, "PurposeID", "PurposeName");


            List<BE.VehicleSubGoupEntites> Subgroup = new List<BE.VehicleSubGoupEntites>();
            Subgroup = trainTrackerProvider.getVehicleSubgroup();
            ViewBag.Subgroup = new SelectList(Subgroup, "GroupID", "Group");
            //Code
            return View();
        }


        //public ActionResult CmbFuelTypeOnChange(string ddlFuelType, int CostCenter)
        //{
        //    List<BE.FuelConsumption> CmbFuelTypeOnChange = new List<BE.FuelConsumption>();
        //    CmbFuelTypeOnChange = trainTrackerProvider.GetCmbFuelTypeOnChange(ddlFuelType, CostCenter);
        //    //ViewBag.BalQty = CmbFuelTypeOnChange[0].BalQty;
        //    var jsonResult = Json(CmbFuelTypeOnChange, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
        public ActionResult CmbFuelTypeOnChange(string ddlFuelType, int CostCenter, string ddlCostCenterFor)
        {
            BE.FuelConsumption CmbFuelTypeOnChange = new BE.FuelConsumption();
            CmbFuelTypeOnChange = trainTrackerProvider.GetCmbFuelTypeOnChange(ddlFuelType, CostCenter, ddlCostCenterFor);
            //ViewBag.BalQty = CmbFuelTypeOnChange[0].BalQty;
            var jsonResult = Json(CmbFuelTypeOnChange, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult AjaxgetFromReading(string trailername ,string CostCenterFor, string FuelType)
        {

            BE.FuelConsumption FromReading = new BE.FuelConsumption();
            FromReading = trainTrackerProvider.AjaxgetFromReading(trailername, CostCenterFor,FuelType);
            var jsonResult = Json(FromReading, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }




        [HttpPost]
        public ActionResult PrintInternalFuelStock(string FuelFor)
        {
            BE.FuelStockSummary PT = new BE.FuelStockSummary();
            PT = trainTrackerProvider.PrintInternalFuelStock(FuelFor);
            ViewBag.fuelRefNo = PT.fuelRefNo;
            ViewBag.IssueDate = PT.EntryDate;
            ViewBag.qty = PT.qty;
            ViewBag.fuelType = PT.fuelType;
            ViewBag.vendorname = PT.vendorname;
            ViewBag.CostCenterFor = PT.CostCenterFor;
            ViewBag.FuelFor = PT.FuelFor;
            return PartialView();


        }
        [HttpPost]
        public ActionResult GetFuelStockSummaryDetails(string cmbFueltype, string fromdate, string ToDate, string FuelForSearch)
        {
            List<BE.FuelStockSummary> FuelStockSummary = new List<BE.FuelStockSummary>();

            FuelStockSummary = trainTrackerProvider.GetFuelStockSummaryDetails(cmbFueltype, fromdate, ToDate, FuelForSearch);
            string costcenter = "";
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_fuel_stock_summary1 '" + fromdate + "','" + ToDate + "','" + costcenter + "','" + cmbFueltype + "','" + FuelForSearch + "'");
            dt.Columns.Remove("fuelregid");
            dt.Columns.Remove("btnClass");
            Session["FuelStockSummary"] = dt;
            var jsonResult = Json(FuelStockSummary, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public ActionResult PrintInternalFuelStockReprint(string fuelregid, string FuelFor)
        {
            BE.FuelStockSummary PT = new BE.FuelStockSummary();
            PT = trainTrackerProvider.PrintInternalFuelStockReprint(fuelregid, FuelFor);
            ViewBag.fuelRefNo = PT.fuelRefNo;
            ViewBag.IssueDate = PT.EntryDate;
            ViewBag.qty = PT.qty;
            ViewBag.fuelType = PT.fuelType;
            ViewBag.vendorname = PT.vendorname;
            ViewBag.CostCenterFor = PT.CostCenterFor;
            ViewBag.FuelFor = PT.FuelFor;
            return PartialView();


        }

        //[HttpPost]
        //public ActionResult PrintFuelConsumptions()
        //{
        //    BE.FuelConsumption PT = new BE.FuelConsumption();
        //    PT = trainTrackerProvider.PrintFuelConsumption();
        //    ViewBag.fuelRefNo = PT.fuelRefNo;
        //    ViewBag.IssueDate = PT.IssueDate;
        //    ViewBag.trailername = PT.trailername;
        //    ViewBag.drivername = PT.drivername;
        //    ViewBag.BalQty = PT.BalQty;
        //    ViewBag.issueQty = PT.issueQty;
        //    ViewBag.fuelType = PT.fuelType;
        //    ViewBag.lastReading = PT.lastReading;
        //    ViewBag.currentReading = PT.currentReading;
        //    ViewBag.CostCenterFor = PT.CostCenterFor;

        //    return PartialView();


        //}

        [HttpPost]
        public ActionResult PrintFuelConsumptions(string costcenterfor)
        {
            BE.FuelConsumption PT = new BE.FuelConsumption();
            PT = trainTrackerProvider.PrintFuelConsumption(costcenterfor);
            ViewBag.fuelRefNo = PT.fuelRefNo;
            ViewBag.IssueDate = PT.IssueDate;
            ViewBag.trailername = PT.trailername;
            ViewBag.drivername = PT.drivername;
            ViewBag.BalQty = PT.BalQty;
            ViewBag.issueQty = PT.issueQty;
            ViewBag.fuelType = PT.fuelType;
            ViewBag.lastReading = PT.lastReading;
            ViewBag.currentReading = PT.currentReading;
            ViewBag.CostCenterFor = PT.CostCenterFor;
            ViewBag.DifferenceType = PT.Differencereading;
            ViewBag.fueltype = PT.Fueltype;
            ViewBag.VehicleType = PT.VehicleTypeName;

            return PartialView();


        }

        [HttpPost]
        public ActionResult ImportJobOrderReportPrint(string id)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable JoDetails = new DataTable();
            DataTable JoDetailSummary = new DataTable();
            DataTable JoContainerDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GETIMPORTJOBORDERREPORTSDET " + id);

            if (getJobOrderSet.Tables.Count > 0)
            {
                JoDetails = getJobOrderSet.Tables[0];
                JoDetailSummary = getJobOrderSet.Tables[1];
                JoContainerDetails = getJobOrderSet.Tables[2];
            }

            ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            foreach (DataRow dr in JoDetails.Rows)
            {
                ViewBag.Port = dr["PORT"];
                ViewBag.Vessel = dr["VESSEL"];
                ViewBag.VIANo = dr["VIA NO"];
                ViewBag.JOBNo = dr["JOB NO"];
                ViewBag.IGMNo = dr["IGM NO"];
            }

            ViewBag.JobOrderList = JoDetailSummary.AsEnumerable();
            ViewBag.JoContainerList = JoContainerDetails.AsEnumerable();
            ViewBag.json = JsonConvert.SerializeObject(JoContainerDetails);


            return PartialView();


        }

        [HttpPost]
        public ActionResult ImportStackingReportPrint(string SearchText, string value)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable JoItemList = new DataTable();
            DataTable JoContainerList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetImportStackingReport '" + SearchText + "','" + value + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                JoItemList = getJobOrderSet.Tables[0];
                JoContainerList = getJobOrderSet.Tables[1];

                foreach (DataRow dr in JoItemList.Rows)
                {
                    ViewBag.IGMNo = dr["IGMNo"];
                    ViewBag.IGMDate = dr["IGMDate"];
                    ViewBag.VesselName = dr["VesselName"];
                }
            }

            ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.ItemList = JoItemList.AsEnumerable();
            ViewBag.ContainerList = JoContainerList.AsEnumerable();

            return PartialView();


        }

        [HttpPost]
        public ActionResult GenerateRailEIRCopyPrint(string SearchType, string SearchText)
        {
            DataTable JoContainerList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JoContainerList = db.sub_GetDatatable("GetTrainEIRCopy '" + SearchType + "','" + SearchText + "'");
            ViewBag.ContainerList = JoContainerList.AsEnumerable();
            return PartialView();
        }

        public ActionResult GetFuelConsumptionsSummary(string txtsearch, string fromdate, string todate)
        {
            List<BE.FuelConsumption> FuelStockSummary = new List<BE.FuelConsumption>();

            FuelStockSummary = trainTrackerProvider.FuelConsumptionSummary(txtsearch, fromdate, todate);

            var jsonResult = Json(FuelStockSummary, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult AjaxDriverfor()
        {

            List<BE.DriversEntities> Drivers = new List<BE.DriversEntities>();
            Drivers = LP.getIcdDrivers();
            return new JsonResult() { Data = Drivers, JsonRequestBehavior = JsonRequestBehavior.AllowGet };



        }


        public ActionResult SaveICDInternalDriverName(string txtDriver)
        {
            string message = "";
            message = trainTrackerProvider.SaveICDInternalDriverName(txtDriver);


            return Json(message);
        }



        public ActionResult PrintVehicleInout()
        {
            BE.VehicleActivityInOut PT = new BE.VehicleActivityInOut();
            PT = trainTrackerProvider.PrintVehicleInout();
            ViewBag.ActivityDate = PT.ActivityDate;
            ViewBag.VehicleNo = PT.VehicleNo;
            ViewBag.FromLocation = PT.FromLocation;
            ViewBag.ToLocation = PT.ToLocation;
            ViewBag.Activity = PT.Activity;
            ViewBag.partyName = PT.partyName;
            ViewBag.Drivername = PT.Drivername;
            ViewBag.Drivercontact = PT.Drivercontact;
            ViewBag.ContainerNo = PT.ContainerNo;

            if (ViewBag.ContainerNo != "")
            {
                string[] values1 = ViewBag.ContainerNo.Split(',');
                List<BE.VehicleActivityInOut> Remark = new List<BE.VehicleActivityInOut>();
                for (int i = 0; i < values1.Length; i++)
                {
                    BE.VehicleActivityInOut set = new BE.VehicleActivityInOut();
                    values1[i] = values1[i].Trim();
                    set.ContainerNo = values1[i];
                    Remark.Add(set);
                }
                ViewBag.values1 = Remark;
            }
            else
            {
                ViewBag.values1 = "";
            }

            return PartialView();


        }

        public ActionResult RePrintVehicleInout(string Entryid)
        {
            BE.VehicleActivityInOut PT = new BE.VehicleActivityInOut();
            PT = trainTrackerProvider.REPrintVehicleInout(Entryid);
            ViewBag.ActivityDate = PT.ActivityDate;
            ViewBag.VehicleNo = PT.VehicleNo;
            ViewBag.FromLocation = PT.FromLocation;
            ViewBag.ToLocation = PT.ToLocation;
            ViewBag.Activity = PT.Activity;
            ViewBag.partyName = PT.partyName;
            ViewBag.Drivername = PT.Drivername;
            ViewBag.Drivercontact = PT.Drivercontact;
            ViewBag.ContainerNo = PT.ContainerNo;


            if (ViewBag.ContainerNo != "")
            {
                string[] values1 = ViewBag.ContainerNo.Split(',');
                List<BE.VehicleActivityInOut> Remark = new List<BE.VehicleActivityInOut>();
                for (int i = 0; i < values1.Length; i++)
                {
                    BE.VehicleActivityInOut set = new BE.VehicleActivityInOut();
                    values1[i] = values1[i].Trim();
                    set.ContainerNo = values1[i];
                    Remark.Add(set);
                }
                ViewBag.values1 = Remark;
            }
            else
            {
                ViewBag.values1 = "";
            }
            return PartialView();

        }
        public ActionResult ajaxTrailerStatus(string Trailername)
        {
            string message = "";

            message = trainTrackerProvider.GetTrailerStatus(Trailername);

            return Json(message);

        }

        public JsonResult ajaxGetVehileoutLatestRecord(string Vehicleno)
        {
            List<BE.VehicleInOutEntryEntitiessummary> TPdetails = new List<BE.VehicleInOutEntryEntitiessummary>();
            TPdetails = trainTrackerProvider.GetTrailerLastDetails(Vehicleno);
            var jsonResult = Json(TPdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }

        public ActionResult GetVehicleInoutSummary(string fromdate, string todate)
        {
            List<BE.VehicleActivityInOut> FuelStockSummary = new List<BE.VehicleActivityInOut>();

            FuelStockSummary = trainTrackerProvider.GetVehicleInoutSummary(fromdate, todate);

            var jsonResult = Json(FuelStockSummary, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelGetFuelStockSummary()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            // DataTable dt = List<BE.UpcomingVehicleStatusEntities> Session["upcomingList"];
            //  DataTable dt = new DataTable();
            // List<BE.UpcomingVehicleStatusEntities> obj = new List<BE.UpcomingVehicleStatusEntities>();
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = FuelStockSummary;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=FuelStockSummary .xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Fuel Stock Summary Report <strong></td></tr>");
                    //htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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
        //code end by manish
        public ActionResult DownloadPDF(string id, string Name)
        {
            string viewName = "ImportJobOrderReportPrint";
            string HTMLContent = "";
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "PDFfileJoNo" + id.ToString() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ImportJobOrderReportPrint(id);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                HTMLContent = writer.ToString();
            };

            string urlLogo = "/images/NavkarLogo.jpg";
            string path = Server.MapPath(urlLogo);
            HTMLContent = HTMLContent.Replace(urlLogo, path.Trim());

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                //StringReader sr = new StringReader(HTMLContent);
                Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 40f, 5f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                using (var html = new MemoryStream(Encoding.Default.GetBytes(HTMLContent)))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, html, null, Encoding.Default, FontFactory.FontImp);
                }
                //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr, Encoding.Default);
                pdfDoc.Close();
                Response.BinaryWrite(stream.ToArray());
            };
            Response.End();

            return View();
        }


        [HttpPost]
        public ActionResult CheckTrailerStatusMovementType(string TrailerNo, string INout)
        {
            string message = "";

            message = trainTrackerProvider.GetVehiclemOvementType(TrailerNo, INout);

            return Json(message);
        }

        //Code start by Rahul 02-11-2019
        public ActionResult IGMManual()
        {
            BE.IGMManualEntities IGMManualList = new BE.IGMManualEntities();
            List<BE.IGMFiLeType> IgmFileType = new List<BE.IGMFiLeType>();
            IGMManualList = trainTrackerProvider.GetDropDownListIGMManual();
            ViewBag.PackageIGMList = new SelectList(IGMManualList.PackageIGMList, "CodeID", "Package");
            IgmFileType = BL.getIGMFileType();
            ViewBag.IgmFileType = new SelectList(IgmFileType, "FileTypeId", "FileTypeName");
            return View();
        }
        [HttpPost]
        public JsonResult getAutoImporterName(string prefixText)
        {
            List<BE.ConsigneeIGMManualEntities> Consignee = new List<BE.ConsigneeIGMManualEntities>();
            Consignee = trainTrackerProvider.getAutoConsigneeIGM(prefixText);

            return Json(Consignee, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getAutoPOL(string prefixText)
        {
            List<BE.POL> POL = new List<BE.POL>();
            POL = trainTrackerProvider.getAutoPOL(prefixText);

            return Json(POL, JsonRequestBehavior.AllowGet);
        }

        //USP_IGM_DETAILS_SEARCH_IGM_MANUAL
        //code end by rahul 02-11-2019
        //Code start by Rahul 05-11-2019
        [HttpPost]
        public JsonResult getSearchIGMManual(string Search, string IGMNo, string ItemNo, string ContainerNo, string ViaNo)
        {
            List<BE.IGMManualEntities> IGMManualList = new List<BE.IGMManualEntities>();
            IGMManualList = trainTrackerProvider.getSearchIGMManualList(Search, IGMNo, ItemNo, ContainerNo, ViaNo);

            var jsonResult = Json(IGMManualList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        //code end by rahul 05-11-2019

        //code start by rahul 06-11-2019
        [HttpPost]
        public JsonResult getEditIGMNoDetails(string IGMNo, string ItemNo)
        {
            BE.IGMManualEntities IGMManualList = new BE.IGMManualEntities();
            IGMManualList = trainTrackerProvider.getIGMDetailsListforEdit(IGMNo, ItemNo);

            var jsonResult = Json(IGMManualList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        [HttpPost]
        public JsonResult getAddIGMDetails(string JONo)
        {
            BE.IGMManualEntities IGMManualList = new BE.IGMManualEntities();
            IGMManualList = trainTrackerProvider.getIGMDetailsListforAdd(JONo);

            var jsonResult = Json(IGMManualList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        //code end by rahul 06-11-2019

        //Code start by rahul 07-11-2019
        public JsonResult IGMManualSave(List<BE.ConDetailsIGMManualEntities> Containerdata, string IGMNo, string ItemNo, string IGM_BLNo, string IGM_BLDate, string Consignee, string Con_IGMAddress, string NConsignee, string NCon_IGMAddress, string IGM_GoodsDesc, string TotIGMQty, string PKGType, string TotIGMWt, string JONo,string POL)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("ContainerType");
            dataTable.Columns.Add("Cargotype");
            dataTable.Columns.Add("InDate");
            dataTable.Columns.Add("SealNo");
            dataTable.Columns.Add("IGMQty");
            dataTable.Columns.Add("IGMWt");

            foreach (BE.ConDetailsIGMManualEntities item in Containerdata)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["ContainerType"] = item.ContainerType;
                row["Cargotype"] = item.Cargotype;
                row["InDate"] = item.InDate;
                row["SealNo"] = item.SealNo;
                row["IGMQty"] = item.IGMQty;
                row["IGMWt"] = item.IGMWt;
                dataTable.Rows.Add(row);
            }

            string message = trainTrackerProvider.SaveIGMManual(dataTable, IGMNo, ItemNo, IGM_BLNo, IGM_BLDate, Consignee, Con_IGMAddress, NConsignee, NCon_IGMAddress, IGM_GoodsDesc, TotIGMQty, PKGType, TotIGMWt, JONo, UserID,POL);
            return Json(message);
        }
        //code end by rahul 07-11-2019
        //[HttpPost]
        //public ActionResult REPrintFuelConsumptions(string id)
        //{
        //    BE.FuelConsumption PT = new BE.FuelConsumption();
        //    PT = trainTrackerProvider.REPrintFuelConsumption(id);
        //    ViewBag.fuelRefNo = PT.fuelRefNo;
        //    ViewBag.IssueDate = PT.IssueDate;
        //    ViewBag.trailername = PT.trailername;
        //    ViewBag.drivername = PT.drivername;
        //    ViewBag.BalQty = PT.BalQty;
        //    ViewBag.issueQty = PT.issueQty;
        //    ViewBag.fuelType = PT.fuelType;
        //    ViewBag.lastReading = PT.lastReading;
        //    ViewBag.currentReading = PT.currentReading;
        //    ViewBag.CostCenterFor = PT.CostCenterFor;
        //    ViewBag.DifferenceType = PT.Differencereading;
        //    ViewBag.fueltype = PT.Fueltype;
        //    ViewBag.VehicleType = PT.VehicleTypeName;
        //    return PartialView();


        //}


        [HttpPost]
        public JsonResult FUELAjaxDriverNo(string DriverCOde)
        {
            List<BE.TrailersEntities> Trailerno = new List<BE.TrailersEntities>();
            Trailerno = trainTrackerProvider.ICDGetDriverno(DriverCOde);
            return new JsonResult() { Data = Trailerno, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public ActionResult SaveFuelConsumption(string txtentryDate, int ddlCostCenter, int trailerid,
      double txtBalQty, string txtIssueQty, string ddlFuelType, int txtReadFrom,
      int txtReadingTo, int ddlDriverName, string txtRemark, string ddlUnit,
      int ddlvehicleType, string CostCenterFor, string DifferenceReading, string VehiclePurpose, string Vehiclesubgroup, string Stockslipno)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = trainTrackerProvider.SaveFuelConsumption(txtentryDate, ddlCostCenter, trailerid, txtBalQty, txtIssueQty,
                ddlFuelType, txtReadFrom, txtReadingTo, ddlDriverName, txtRemark, ddlUnit, UserID, ddlvehicleType,
                CostCenterFor, DifferenceReading, VehiclePurpose, Vehiclesubgroup, Stockslipno);
            return Json(message);
        }

        public DataSet getDropDownListExternal()
        {
            DataSet ddWODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            ddWODS = db.sub_GetDataSets("usp_External_job_order");
            return ddWODS;
        }

        public ActionResult ExternalmovementJobOrder()
        {

            BE.DischargeDateContainerDetails externallist = new BE.DischargeDateContainerDetails();
            externallist = trainTrackerProvider.GetDropDownListWorkOrder();
            ViewBag.Port = new SelectList(externallist.portList, "PortID", "ports");
            ViewBag.Line = new SelectList(externallist.lineList, "LineID", "LineName");
            ViewBag.Vessel = new SelectList(externallist.VesseslList, "VesselID", "VesselName");
            ViewBag.Type = new SelectList(externallist.TypeList, "TypeID", "Type");
            ViewBag.From = new SelectList(externallist.FromList, "fromid", "From");
            ViewBag.To = new SelectList(externallist.ToList, "Toid", "To");
            ViewBag.Size = new SelectList(externallist.SizeList, "Sizeid", "Sizec");
            return View();
        }

        public JsonResult ExternalmovementSave(List<BE.DischargeDateContainerDetails> Externaldata, string JoDate, string Criteria, string Line, string Vessel, string Port, string ViaNo, string CutOfdate,string ShipmentNo,string JoNo)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);

            string message = "";

            if (JoNo == "New")
            {
                JoNo = "0";
            }

            if (CutOfdate == "")
            {
                CutOfdate = "1900-01-01 00:00:00.000";
            }

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("SizeID");
            dataTable.Columns.Add("TypeID");
            dataTable.Columns.Add("fromid");
            dataTable.Columns.Add("Toid");
            dataTable.Columns.Add("JoNo");

            if (JoNo == null)
            {
                JoNo = "0";
            }


            foreach (BE.DischargeDateContainerDetails item in Externaldata)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["SizeID"] = item.Sizeid;
                row["TypeID"] = item.TypeID;
                row["fromid"] = item.fromid;
                row["Toid"] = item.Toid;
                row["JoNo"] = JoNo;

                dataTable.Rows.Add(row);

                message = ValidateDuplicate(item.ContainerNo, Vessel, ViaNo, Criteria);

            }

            if (message == "")
            {
                message = trainTrackerProvider.SaveExternal(dataTable, JoDate, Criteria, Line, Vessel, Port, ViaNo, CutOfdate, UserID, ShipmentNo,JoNo);
            }

            return Json(message);
        }

        public JsonResult UploadIGMFile(BE.JobOrderMEntities fileData)
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
                                            foreach(DataRow row in dt.Rows)
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
                                                catch (Exception ex) {
                                                    message = "Invalid Size. Please check and correct it.";
                                                }

                                                try
                                                {
                                                    type = externallist.TypeList.FirstOrDefault(s => s.Type == row["TYPE"].ToString().Trim().ToUpper());
                                                }
                                                catch (Exception ex) {
                                                    message = "Invalid Container Type. Please check and correct it.";
                                                }

                                                try
                                                {
                                                    from = externallist.FromList.FirstOrDefault(s => s.From == row["FROM"].ToString().Trim().ToUpper());
                                                    if (from == null)
                                                    {
                                                        message = "Invalid From Location. Please check and correct it. :- " + row["FROM"].ToString();
                                                    }
                                                }
                                                catch (Exception ex) {
                                                    message = "Invalid From Location. Please check and correct it.";
                                                }

                                                try
                                                {
                                                    to = externallist.ToList.FirstOrDefault(s => s.To == row["TO"].ToString().Trim().ToUpper());
                                                    if (to == null)
                                                    {
                                                        message = "Invalid To Location. Please check and correct it. :- " + row["TO"].ToString();
                                                    }
                                                }
                                                catch (Exception ex) {
                                                    message = "Invalid To Location. Please check and correct it.";
                                                }

                                                if (message == "")
                                                {
                                                    container.ContainerNo = row["CONTAINER NO"].ToString().Trim() + "<input Name=ContainerNo type=hidden id=ContainerNo   value='" + row["CONTAINER NO"].ToString().Trim() + "'>";
                                                    container.Size = row["SIZE"].ToString().Trim() + "<input Name=Size type=hidden id=Sizeid   value=" + size.Sizeid + ">";
                                                    container.Type = row["TYPE"].ToString().Trim() + "<input Name=Type type=hidden id=TypeID   value='" + type.TypeID + "'>";
                                                    container.From = row["FROM"].ToString().Trim() + "<input Name=from type=hidden id=fromid   value='" + from.fromid + "'>";
                                                    container.To = row["TO"].ToString().Trim() + "<input Name=To type=hidden id=Toid   value='" + to.Toid + "'>";

                                                    containerList.Add(container);
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

                                        var returnField = new { ContainerList = containerList, message = message};

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

            // return Json(JOAttachmentList);
            //var Data = JsonConvert.SerializeObject(JOAttachmentList, Formatting.Indented,
            //            new JsonSerializerSettings
            //            {
            //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //            });
            return Json(message);
            // return new JsonResult() { Data = JOAttachmentList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public ActionResult REPrintFuelConsumptions(string SlipNo)
        {
            BE.FuelConsumption PT = new BE.FuelConsumption();
            PT = trainTrackerProvider.REPrintFuelConsumption(SlipNo);
            ViewBag.fuelRefNo = PT.fuelRefNo;
            ViewBag.IssueDate = PT.IssueDate;
            ViewBag.trailername = PT.trailername;
            ViewBag.drivername = PT.drivername;
            ViewBag.BalQty = PT.BalQty;
            ViewBag.issueQty = PT.issueQty;
            ViewBag.fuelType = PT.fuelType;
            ViewBag.lastReading = PT.lastReading;
            ViewBag.currentReading = PT.currentReading;
            ViewBag.CostCenterFor = PT.CostCenterFor;
            ViewBag.DifferenceType = PT.Differencereading;
            ViewBag.fueltype = PT.Fueltype;
            ViewBag.VehicleType = PT.VehicleTypeName;
            return PartialView();


        }

        [HttpPost]
        public JsonResult getVesselDetails(string viaNo)
        {
            BE.JobOrderMEntities JE = new BE.JobOrderMEntities();
            JE = BL.getVesselDetailsExp(viaNo);
            return Json(JE);
        }

        [HttpPost]
        public ActionResult ExternalMovSummary(string JoNo, string ShipmentNo,string ContainerNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("GetEmptyJoMovement '" + JoNo + "','" + ShipmentNo + "','" + ContainerNo + "'");
            //dt.Columns.Remove("fuelregid");
            //dt.Columns.Remove("btnClass");
            //Session["FuelStockSummary"] = dt;
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult EditMovement(string JoNo)
        {
            string Message = "";
            BE.DischargeDateContainerDetails headerData = new BE.DischargeDateContainerDetails();
            List<BE.DischargeDateContainerDetails> containerList = new List<BE.DischargeDateContainerDetails>();
            try
            {
                if(JoNo=="" && JoNo == null)
                {
                    Message = "Invalid Jo Number.";
                }

                DataSet ds = new DataSet();
                DataTable dtHeaderData = new DataTable();
                DataTable dtContainerDet = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                ds = db.sub_GetDataSets("getMovementDetailsByJoNo '" + JoNo + "'");

                if(ds != null)
                {
                    dtHeaderData = ds.Tables[0];

                    if (dtHeaderData.Rows.Count > 0)
                    {
                        headerData.JONO = Convert.ToInt32(dtHeaderData.Rows[0]["JONo"]);
                        headerData.JoDate = dtHeaderData.Rows[0]["JODate"].ToString();
                        headerData.ShipmentNo = dtHeaderData.Rows[0]["ShipmentNo"].ToString();
                        headerData.lineid = Convert.ToInt32(dtHeaderData.Rows[0]["SLID"]);
                        headerData.VIANo = dtHeaderData.Rows[0]["ViaNo"].ToString();
                        headerData.CutoffDate = dtHeaderData.Rows[0]["CutofDate"].ToString();
                        headerData.POL = dtHeaderData.Rows[0]["POL"].ToString();
                        headerData.vesselName = dtHeaderData.Rows[0]["VesselName"].ToString();
                        headerData.Criteria = dtHeaderData.Rows[0]["Criteria"].ToString();
                        headerData.Size20 = Convert.ToInt32(dtHeaderData.Rows[0]["Total_20"]);
                        headerData.Size40 = Convert.ToInt32(dtHeaderData.Rows[0]["Total_40"]);
                        headerData.Size45 = Convert.ToInt32(dtHeaderData.Rows[0]["Total_45"]);
                        headerData.Total = Convert.ToInt32(dtHeaderData.Rows[0]["Total"]);

                        dtContainerDet = ds.Tables[1];
                        if (dtContainerDet.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtContainerDet.Rows)
                            {
                                BE.DischargeDateContainerDetails container = new BE.DischargeDateContainerDetails();
                                container.ContainerNo = row["ContainerNo"].ToString() + "<input Name=ContainerNo type=hidden id=ContainerNo   value='" + row["ContainerNo"].ToString() + "'>";
                                container.Size = row["Size"].ToString() + "<input Name=Size type=hidden id=Sizeid   value=" + row["ContainerSizeID"].ToString() + ">";
                                container.Type = row["ContainerType"].ToString() + "<input Name=Type type=hidden id=TypeID   value='" + row["ContainerTypeID"].ToString() + "'>";
                                container.From = row["LocationFrom"].ToString() + "<input Name=from type=hidden id=fromid   value='" + row["LFrom"].ToString() + "'>";
                                container.To = row["LocationTo"].ToString() + "<input Name=To type=hidden id=Toid   value='" + row["LTo"].ToString() + "'>";

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
                else
                {
                    Message = "No Data Found.";
                }
            }
            catch (Exception ex)
            {
                Message = "Invalid Jo Number.";
            }

            var returnField = new { ContainerList = containerList, message = Message,HeaderDetails= headerData };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult ModifyExistingJo(string JoNo,string ShipmentNo,string ContainerNo,string SizeId,string ContainerTypeId,string FromLocationId,string ToLocationId)
        {
            string Message = "";
            List<BE.DischargeDateContainerDetails> containerList = new List<BE.DischargeDateContainerDetails>();
            try
            {
                if (JoNo == "" && JoNo == null)
                {
                    Message = "Invalid Jo Number.";
                }

                if (ShipmentNo == null)
                {
                    ShipmentNo = "";
                }

                DataTable dtContainerDet = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                dtContainerDet = db.sub_GetDatatable("getMovementDetailsByJoNoEdit '" + JoNo + "','" + ShipmentNo + "','" + ContainerNo + "','" + SizeId + "','" + ContainerTypeId + "','" + FromLocationId + "','" + ToLocationId + "'" );

                if (dtContainerDet != null)
                {
                    if (dtContainerDet.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtContainerDet.Rows)
                        {
                            BE.DischargeDateContainerDetails container = new BE.DischargeDateContainerDetails();
                            container.ContainerNo = row["ContainerNo"].ToString() + "<input Name=ContainerNo type=hidden id=ContainerNo   value='" + row["ContainerNo"].ToString() + "'>";
                            container.Size = row["Size"].ToString() + "<input Name=Size type=hidden id=Sizeid   value=" + row["ContainerSizeID"].ToString() + ">";
                            container.Type = row["ContainerType"].ToString() + "<input Name=Type type=hidden id=TypeID   value='" + row["ContainerTypeID"].ToString() + "'>";
                            container.From = row["LocationFrom"].ToString() + "<input Name=from type=hidden id=fromid   value='" + row["LFrom"].ToString() + "'>";
                            container.To = row["LocationTo"].ToString() + "<input Name=To type=hidden id=Toid   value='" + row["LTo"].ToString() + "'>";

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

            var returnField = new { ContainerList = containerList, message = Message};

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult DeleteContainerNo(string ContainerNo,string JoNo)
        {
            string Message = "";

            try
            {
                DataTable dt = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
                dt = db.sub_GetDatatable("USP_CancelExtMovContainer '" + ContainerNo + "','" + JoNo + "'");

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Message = dt.Rows[0]["Message"].ToString();
                    }
                    else
                    {
                        Message = "Invalid Container No.";
                    }
                }
                else
                {
                    Message = "Invalid Container No.";
                }
            }
            catch(Exception ex)
            {
                Message = "Error Occured :- " + ex.Message;
            }

            var jsonResult = Json(Message, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult CancelJoDet(string JoNo)
        {
            int UserId = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";
            DataTable dt1 = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt1 = db.sub_GetDatatable("USP_CancelExternalMovJo '" + JoNo + "','" + UserId  + "'");

            if (dt1.Rows.Count > 0)
            {
                Message = dt1.Rows[0]["Message"].ToString();
            }
            else
            {
                Message = "Invalid Details. Please try again.";
            }

            var jsonResult = Json(Message, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public string ValidateDuplicate(string ContaienrNo, string VesselId, string VIANo, string CycleType)
        {
            string Message = "";
            try
            {
                DataTable dt = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                dt = db.sub_GetDatatable("USP_ValidateExtMovCont '" + ContaienrNo + "','" + VesselId + "','" + VIANo + "','" + CycleType + "'");

                if (dt.Rows.Count > 0)
                {
                    Message = dt.Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return Message;

        }

        [HttpPost]
        public ActionResult GenerateJobOrderFrom13(string SearchType, string SearchText)
        {
            DataTable JoContainerList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JoContainerList = db.sub_GetDatatable("GetJobOrderFrom13 '" + SearchType + "','" + SearchText + "'");
            ViewBag.ContainerList = JoContainerList.AsEnumerable();
            return PartialView();
        }

        public ActionResult GetBLWiseIGMdetails(string BLNumber)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetIGM_ITEM_No '" + BLNumber + "'");


            BE.IGMEntities IGMdetails = new BE.IGMEntities();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    IGMdetails.IGMNo = Convert.ToString(row["IGMNo"]);
                    IGMdetails.ItemNo = Convert.ToString(row["ItemNo"]);
                }

            }
            var jsonResult = Json(IGMdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ViewTraiffDetails()
        {

            List<BE.TraiffViewsEntities> ContainerType = new List<BE.TraiffViewsEntities>();
            ContainerType = trainTrackerProvider.GetContainerTypeDetails();
            List<BE.TraiffViewsEntities> Accountmaster = new List<BE.TraiffViewsEntities>();
            Accountmaster = trainTrackerProvider.GetAccountnameDetails();

            List<BE.TraiffViewsEntities> Traiffdetails = new List<BE.TraiffViewsEntities>();
            Traiffdetails = trainTrackerProvider.GetTraiffDetails();

            List<BE.TraiffViewsEntities> BondTraiffdetails = new List<BE.TraiffViewsEntities>();
            BondTraiffdetails = trainTrackerProvider.GetBondTraiffDetails();

            List<BE.TraiffViewsEntities> DomesticTraiffdetails = new List<BE.TraiffViewsEntities>();
            DomesticTraiffdetails = trainTrackerProvider.GetDomesticraiffDetails();


            List<BE.TraiffViewsEntities> Export = new List<BE.TraiffViewsEntities>();
            Export = trainTrackerProvider.GetExportMovementtypeDetails();
            List<BE.TraiffViewsEntities> Bond = new List<BE.TraiffViewsEntities>();
            Bond = trainTrackerProvider.GetBondDeliveryTypeDetails();



            List<BE.TraiffViewsEntities> Domestic = new List<BE.TraiffViewsEntities>();
            Domestic = trainTrackerProvider.GetDomesticDeliveryTypeDetails();

            ViewBag.Containertype = new SelectList(ContainerType, "ContainerTypeID", "ContainertypeName");
            ViewBag.AccountmasterList = new SelectList(Accountmaster, "AccountID", "AccountName");
            ViewBag.TraiffDetailsList = new SelectList(Traiffdetails, "TraiffID", "TraiffDesc");
            ViewBag.TraiffExportDetailsList = new SelectList(Export, "ExportID", "ExportName");
            ViewBag.TraiffBondDetailsList = new SelectList(Bond, "BondID", "BondName");
            ViewBag.TraiffDomesticDetailsList = new SelectList(Domestic, "BondID", "BondName");
            ViewBag.TraiffBondList = new SelectList(BondTraiffdetails, "TraiffID", "TraiffDesc");
            ViewBag.TraiffDomesticList = new SelectList(DomesticTraiffdetails, "TraiffID", "TraiffDesc");

            return View();
        }
        public ActionResult GetTraiffDetails(string TraiffID, string AccountID, string ContainerID, string movementID)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_GetimportViewTariffDetailsummary '" + TraiffID + "','" + AccountID + "','" + ContainerID + "','" + movementID + "'");
            Session["GetTraiffDetails"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }


        public ActionResult GetExportTraiffDetails(string TraiffID, string AccountID, string ContainerID, string movementID)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_GetExportViewTariffDetailsummary '" + TraiffID + "','" + AccountID + "','" + ContainerID + "','" + movementID + "'");
            Session["GetTraiffDetails"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetBondTraiffDetails(string TraiffID, string AccountID, string movementID)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_GetBondViewTariffDetailsummary '" + TraiffID + "','" + AccountID + "','" + movementID + "'");
            Session["GetTraiffDetails"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult GetDomesticTraiffDetails(string TraiffID, string AccountID, string movementID)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_GetDomesticViewTariffDetailsummary '" + TraiffID + "','" + AccountID + "','" + movementID + "'");
            Session["GetTraiffDetails"] = dt;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }


        public ActionResult ExportToExcelTraiffDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable GetGSTReturnCreditNoteSummary = (DataTable)Session["GetTraiffDetails"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = GetGSTReturnCreditNoteSummary;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=TraiffViewDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td colspan ='7'><h1 style='text-align:center'>" + CompanyName + " </h1></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>" + CompanyAddress + " </h3></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h3 style='text-align:center'>Traiff View Summary Report</h3></td></tr>");

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

        public string CHecktrailerfueltype(string trailerid, string FuelType)
        {
            string Message = "";
            try
            {
                DataTable dt = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                dt = db.sub_GetDatatable("USP_Check_trailer_FuelType '" + trailerid + "','" + FuelType + "'");

                if (dt.Rows.Count > 0)
                {
                    Message = dt.Rows[0]["MSG"].ToString();
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return Message;

        }

        [HttpPost]
        public JsonResult AjaxGetManuallipStock(string ddlFuelType, string ddlCostCenter1)
        {


            List<BE.ModifyInternalFuelConsumptionEntities> Trailerno = new List<BE.ModifyInternalFuelConsumptionEntities>();
            Trailerno = trainTrackerProvider.getSlipdetails(ddlFuelType, ddlCostCenter1);
            var jsonResult = Json(Trailerno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }

        public JsonResult GetStockSlipFuel(string SLipNo)
        {
            BE.ModifyInternalFuelConsumptionEntities GetList = new BE.ModifyInternalFuelConsumptionEntities();
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            GetList = trainTrackerProvider.GetStockSlipWiseFuel(SLipNo, Userid);


            var jsonResult = Json(GetList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult CargoBTTVehicleIn()
        {

            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = trainTrackerProvider.getVehicleActivity();
            List<BE.ActivityCycle> Activitycycle = new List<BE.ActivityCycle>();
            Activitycycle = LP.GetActivitVehicle();
            ViewBag.Activitycycle = new SelectList(Activitycycle, "ActivitycycleID", "ActivitycycleName");
            List<BE.ContainerCountEntities> GetContainercount = new List<BE.ContainerCountEntities>();
            GetContainercount = trainTrackerProvider.GetContainerCount();

            List<BE.ContainerCountEntities> GetContainerType = new List<BE.ContainerCountEntities>();
            GetContainerType = trainTrackerProvider.GetContainerType();

            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");
            ViewBag.ContainerCount = new SelectList(GetContainercount, "ContainerCountID", "ContainerCountSize");
            ViewBag.Containertype = new SelectList(GetContainerType, "ContainerCountID", "ContainerCountSize");
            List<BE.ExportShippingBillEnt> CustomerName = new List<BE.ExportShippingBillEnt>();
            CustomerName = SB.getCustomerList();
            //AccountID And Name Is Entities same as entities
            ViewBag.CustomerList = new SelectList(CustomerName, "AGID", "AGName");
            return View();
        }

        public ActionResult SaveBTTVehicleIn(BE.ActivityMaster AM)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            dataTable = db.sub_GetDatatable("USP_SAVE_BTT_VEHICLE '" + AM.ActivityID + "','" + AM.Trailerno + "','" + AM.PartyName + "','" + AM.SBNo + "','" + AM.Remarks + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");


            BE.ResponseMessage item = new BE.ResponseMessage();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    item.Status = Convert.ToInt32(row["Status"]);
                    item.Message = Convert.ToString(row["message"]);

                }
            }
            return Json(item);
        }



        public ActionResult GetBTTVehicleIn(string FromDate, string ToDate)
        {
            DataTable dtGRNList = new DataTable();

            int UserID = Convert.ToInt16(Session["userid"]);
            HC.DBOperations db = new HC.DBOperations();
            dtGRNList = db.sub_GetDatatable("USP_GET_BTT_IN_DETS'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["USP_GET_BTT_IN_DETS"] = dtGRNList;

            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;

            string json = JsonConvert.SerializeObject(dtGRNList);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelGetDailyMovementTeusSummary()
        {

            DataTable dt = (DataTable)Session["USP_GET_BTT_IN_DETS"];

            HC.DBOperations db = new HC.DBOperations();

            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ExportLoadInventory = dt;
            string Tittle = "For :- " + Session["For"] + "From Date :- " + Session["FromDate"] + " To Date :- " + Session["ToDAte"];
            GridView gridview = new GridView();
            gridview.DataSource = ExportLoadInventory;
            gridview.DataBind();
            ////////gridview.HeaderRow.Style.Add("background-color", "Yellow");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=VoucherReportInandOutDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; ><strong style='font-size: 20px'>" + "SrNo" + " <strong></td></tr>");
                    //htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; ><strong style='font-size: 20px'>" + "Date/Year" + " <strong></td></tr>");

                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center '; colspan ='8'><strong style='font-size: 15px'>EXPORT<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center; border:1px solid black'; colspan ='20'><strong style='font-size: 22px'>" + "IMPORT DETAILS" + "<strong></td><td style='font-weight: bold; text-align: center; border:1px solid black'; colspan ='16'><strong style='font-size: 20px'>" + "MTY REPO DETAILS" + " <strong></td><td style='font-weight: bold; text-align: center; border:1px solid black'; colspan ='8'><strong style='font-size: 20px'>" + "EXPORT" + " <strong></td></tr>");
                    //htw.Write("<table><tr></tr>");
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


        public ActionResult PrintBTTInSlip(string EntryID)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblInvoiceDetails = new DataTable();


            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("USP_PRINT_GET_BTT_PRINT '" + EntryID + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {

                tblComDetails = getJobOrderSet.Tables[0];


                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.EntryID = dr["EntryID"];
                    ViewBag.indate = dr["indate"];
                    ViewBag.VehicleNO = dr["VehicleNO"];
                    ViewBag.PartyName = dr["PartyName"];
                    ViewBag.Activity = dr["Activity"];
                    ViewBag.SBNO = dr["SB NO"];
                    ViewBag.AddedBy = dr["Added By"];
                    ViewBag.Remarks = dr["Remarks"];

                }

            }

            ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";

            return PartialView();
        }


        public ActionResult getCustomerVendorAutoComp(string Name)        {            try            {                HC.DBOperations db = new HC.DBOperations();                DataTable dataTable = new DataTable();                List<BE.ImportAccountMaster> CustomerList = new List<BE.ImportAccountMaster>();                dataTable = db.sub_GetDatatable("USP_getCustomerAutoComp_BTT '" + Name + "'");                if (dataTable != null)                {                    foreach (DataRow row in dataTable.Rows)                    {                        BE.ImportAccountMaster item = new BE.ImportAccountMaster();                        item.AccountID = Convert.ToInt32(row["M_Common_ID"]);                        item.AccountName = Convert.ToString(row["Name"]);                        CustomerList.Add(item);                    }                }                return Json(CustomerList);            }            catch (Exception ex)            {                return Json(ex.Message);            }        }

    }
}
