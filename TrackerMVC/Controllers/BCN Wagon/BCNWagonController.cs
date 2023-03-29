using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using vessel = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.VesselMaster;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using HC = TrackerMVCDataLayer.Helper;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using TrackerMVCEntities.BusinessEntities;

namespace TrackerMVC.Controllers.BCN_Wagon
{
    public class BCNWagonController : Controller
    {
        BM.BCNWagon.BCNWagonGenerate BL = new BM.BCNWagon.BCNWagonGenerate();
        BM.IGM.IGM IG = new BM.IGM.IGM();
        // GET: BCNWagon
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BCNNoc()
        {
            ViewBag.Message = TempData["MessageValue"];

            ViewBag.NOCDate = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            ViewBag.PlanedDate = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");

            List<BE.CommodityGroup> CommodityGroup = new List<BE.CommodityGroup>();
            List<BE.Location> ports = new List<BE.Location>();
            List<BE.StuffingType> stuffingTypes = new List<BE.StuffingType>();
            List<BE.Shippers> shippers = new List<BE.Shippers>();

            CommodityGroup = BL.getCommodityGroup();
            ports = BL.getLocation();
            stuffingTypes = BL.getStuffing();
            shippers = BL.getShipper();

            ViewBag.CommodityGroup = new SelectList(CommodityGroup, "Commodity_Group_ID", "Commodity_Group_Name");
            ViewBag.StuffType = new SelectList(stuffingTypes, "StuffingTypeId", "StuffingTypeM");
            ViewBag.OrginStation = new SelectList(ports, "LocationId", "LocationName");
            ViewBag.shippers = new SelectList(shippers, "shipperID", "shippername");

            return View();
        }

        public ActionResult BCNWagonMapping()
        {
            return View();
        }

        public ActionResult BCNInvoiceSummary()
        {
            return View();
        }

        public ActionResult AddNewWagonBCN()
        {
            return PartialView();
        }

        public ActionResult BCNMappingSBContainer()
        {
            List<BE.Vessel> Vessel = new List<BE.Vessel>();
            List<BE.Ports> Ports = new List<BE.Ports>();
            List<BE.POL> POL = new List<BE.POL>();

            Vessel = BL.getVessel();
            Ports = BL.getPorts();
            POL = BL.getPOL();

            ViewBag.Vessel = new SelectList(Vessel, "VesselID", "VesselName");
            ViewBag.Ports = new SelectList(Ports, "PortID", "PortName");
            ViewBag.POL = new SelectList(POL, "PODID", "PODName");

            return View();
        }

        public ActionResult RRDownload()
        {
            List<BE.CommodityGroup> CommodityGroup = new List<BE.CommodityGroup>();
            List<BE.Shippers> shippers = new List<BE.Shippers>();
            List<BE.Location> Location = new List<BE.Location>();

            CommodityGroup = BL.getCommodityGroup();
            shippers = BL.getShipper();
            Location = BL.getLocation();

            ViewBag.CommodityGroup = new SelectList(CommodityGroup, "Commodity_Group_ID", "Commodity_Group_Name");
            ViewBag.Customer = new SelectList(shippers, "shipperID", "shippername");
            ViewBag.Location = new SelectList(Location, "LocationId", "LocationName");

            return View();
        }

        public JsonResult ValidateTrain(string TrainNo,string FileType,string RRNo)
        {
            string Message = "";
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            dataTable = db.sub_GetDatatable("ValidateRRDownload '" + TrainNo + "','" + FileType + "','" + RRNo + "'");
            if(dataTable != null)
            {
                foreach(DataRow row in dataTable.Rows)
                {
                    Message = row["Message"].ToString();
                }
            }
            else
            {
                Message = "Invalid Details.";
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BCNNocSave(BE.BCNNocUpdation bCNNoc)
        {
            int saveint = 0;
            string message = "";
            if (ModelState.IsValid)
            {
                bCNNoc.CreatedBy = Convert.ToInt64(Session["Tracker_userID"]); 
                saveint =BL.AddBCNNocDetails(bCNNoc);
            }

            if (saveint == 0)
            {
                message = "Data Not Save Successfully.";
                TempData["MessageValue"] = message;
            }
            else if (saveint >= 1)
            {
                message = "Data Save Successfully.";
                TempData["MessageValue"] = message;
                ModelState.Clear();
                return RedirectToAction("BCNNoc");
            }

            ViewBag.Message = message;
            return View("BCNNoc");
        }

        [HttpPost]
        public JsonResult getNOCDet(string Category, string FromDate, string ToDate)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("GetNOCUpdationDet '" + Category + "','" + FromDate + "','" + ToDate + "'");
            if (tblInvoiceList.Rows.Count > 0)
            {
                Session["NOCSummary"] = tblInvoiceList;
                Session["NOCFromDate"] = FromDate;
                Session["NOCToDate"] = ToDate;
                InvoiceList = JsonConvert.SerializeObject(tblInvoiceList);
            }
            else
            {
                InvoiceList = "0";
            }

            return new JsonResult() { Data = InvoiceList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult CancelBCNNoc(string NocNo,string WOYear)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            var retVal = 0;
            string Message = "";

            retVal = db.sub_ExecuteNonQuery("usp_Cancel_Bcn_updation '" + NocNo + "','" + WOYear + "','" + userId + "'");

            if (retVal > 0)
            {
                Message = "Noc Cancel Successfully.";
            }
            else
            {
                Message = "Noc Not Cancel Successfully.";
            }

            return new JsonResult() { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult BCNNOCSummaryExport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string FromDate = Session["NOCFromDate"].ToString();
            string ToDate = Session["NOCToDate"].ToString();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["NOCSummary"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BCNNocSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> BCN NOC Summary Details<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> FromDate :- " + FromDate + " To Date:- " + todate + "<strong></td></tr>");
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

        [HttpPost]
        public ActionResult BCNNocPrint(string NocNo, String WorkYear)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblNOCDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string CompanyName = "";
            string CompanyAddress = "";
            getJobOrderSet = db.sub_GetDataSets("GetNOCPrintDet '" + NocNo + "','" + WorkYear + "'");
            if(getJobOrderSet != null)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblNOCDetails = getJobOrderSet.Tables[1];
            }

            if(tblComDetails != null)
            {
                foreach(DataRow row in tblComDetails.Rows)
                {
                    CompanyName = row["con_Name"].ToString();
                    CompanyAddress = row["AddressI"].ToString();
                }
            }

            if (tblNOCDetails != null)
            {
                foreach (DataRow row in tblNOCDetails.Rows)
                {
                    ViewBag.NOCNo = row["NOCNo"].ToString();
                    ViewBag.NOCRefNo = row["NOCRefNo"].ToString();
                    ViewBag.NOCDate = row["NOCDate"].ToString();
                    ViewBag.CustomerName = row["ShipperName"].ToString();
                    ViewBag.OrginStation = row["OrginStation"].ToString();
                    ViewBag.Commodity = row["Commodity"].ToString();
                    ViewBag.StuffType = row["StuffType"].ToString();
                    ViewBag.NoofWagonPlanned = row["NoofWagonPlanned"].ToString();
                    ViewBag.PlannedDate = row["PlannedDate"].ToString();
                    ViewBag.StuffingDate = row["StuffingDate"].ToString();
                    ViewBag.ETADate = row["ETADate"].ToString();
                }
            }

            ViewBag.CompanyName = CompanyName;
            ViewBag.CompanyAddress = CompanyAddress;
           

            return PartialView("NOCPrint");

        }

        [HttpPost]
        public JsonResult getRRDownloadDet(string FromDate, string ToDate,string TrainNo,string RRNo)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("GetRRDOwnloadSummary '" + FromDate + "','" + ToDate + "','" + TrainNo + "','" + RRNo + "'");
            Session["RRWagonDetails"] = tblInvoiceList;
            if (tblInvoiceList.Rows.Count > 0)
            {
                InvoiceList = JsonConvert.SerializeObject(tblInvoiceList);
            }
            else
            {
                InvoiceList = "0";
            }

            return new JsonResult() { Data = InvoiceList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult UploadRRFile(BE.BCNRRDownload fileData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.IGMEntities> IGMList = new List<BE.IGMEntities>();
            BE.JobOrderContainerEntities ContainerList = new BE.JobOrderContainerEntities();
            long RRFileId = 0;
            List<BE.ContainerSize> ContainerSize = new List<BE.ContainerSize>();
            string message = "";

            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
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

                        if (extension == ".csv")
                        {
                            string contentType;
                            Stream fs = Request.Files[i].InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);

                            contentType = MimeMapping.GetMimeMapping(fname);
                            //string filedata = fileContents;
                            DataTable DT = new DataTable();

                            DT.Columns.Add("SeqNo");
                            DT.Columns.Add("Line");

                            using (StreamReader file12 = new StreamReader(fname))
                            {
                                int counter = 0;
                                int counter1 = 0;
                                string ln;

                                while ((ln = file12.ReadLine()) != null)
                                {
                                    if (counter1 == 0)
                                    {
                                        counter1++;
                                    }
                                    else
                                    {
                                        counter++;
                                        DataRow dar = DT.NewRow();
                                        dar["SeqNo"] = counter;
                                        dar["Line"] = ln.Replace(",", "~");
                                        DT.Rows.Add(dar);
                                    }
                                }
                                file12.Close();
                            }

                            if (fileData.RRWagonNo == "")
                            {
                                fileData.RRWagonNo = fileData.RRWagonContNo;
                            }

                            RRFileId = IG.AddBCNRRData(DT, Userid, fileData.TrainNo,fileData.FileType, fileData.RRWagonNo);
                            if (RRFileId <= 0)
                            {
                                return new JsonResult() { Data = "RR Download not save successfully. Plz Upload Again !", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                            }
                            //Session.Add("IgmFileId1", IgmFileId);
                            Session["RRFileId"] = RRFileId;
                        }
                        else
                        {
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

                                        DataTable DT1 = new DataTable();
                                        String strb = "";
                                        string strSeprate = "~";
                                        int counter = 0;
                                        DT1.Columns.Add("SeqNo");
                                        DT1.Columns.Add("Line");

                                        if (dt != null)
                                        {
                                            foreach(DataRow dataRow in dt.Rows)
                                            {
                                                counter += 1;
                                                strb = "";
                                                foreach (DataColumn column in dt.Columns)
                                                {
                                                    if (strb == "")
                                                    {
                                                        strb = dataRow[column].ToString().Replace("~","");
                                                    }
                                                    else
                                                    {
                                                        strb = strb + strSeprate + dataRow[column].ToString().Replace("~", "");
                                                    }
                                                }
                                                DataRow dar = DT1.NewRow();
                                                dar["SeqNo"] = counter;
                                                dar["Line"] = strb;
                                                DT1.Rows.Add(dar);
                                            }
                                            if (fileData.RRWagonNo == "")
                                            {
                                                fileData.RRWagonNo = fileData.RRWagonContNo;
                                            }
                                            RRFileId = IG.AddBCNRRData(DT1, Userid, fileData.TrainNo,fileData.FileType, fileData.RRWagonNo);
                                            if (RRFileId <= 0)
                                            {
                                                return new JsonResult() { Data = "RR Download not save successfully. Plz Upload Again !", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                            }
                                            //Session.Add("IgmFileId1", IgmFileId);
                                            Session["RRFileId"] = RRFileId;
                                        }


                                        //Int64 returnValue = 0;
                                        //HC.DBOperations db = new HC.DBOperations();
                                        //dt = db.sub_GetDatatable("GetIgmDownloadErrorLog " + RRFileId);
                                        //if (dt.Rows.Count > 0)
                                        //{
                                        //    returnValue = 0;
                                        //    //Session.Add("IGMError", dt);
                                        //    Session["IGMError"] = dt;
                                        //}
                                        //else
                                        //{
                                        //    returnValue = IgmFileId;
                                        //}

                                        //if (fname != null || fname != string.Empty)
                                        //{
                                        //    if ((System.IO.File.Exists(fname)))
                                        //    {
                                        //        System.IO.File.Delete(fname);
                                        //    }

                                        //}
                                        //return new JsonResult() { Data = RRFileId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                    }
                                }
                            }
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    message ="Exception:- " + ex.Message;
                }
            }
            return Json(message);
        }

        [HttpPost]
        public JsonResult GridViewBindDetailsNew(string Igm)
        {
            int igmid = Convert.ToInt32(Session["RRFileId"]);
            DataSet ds = new DataSet();
            BE.BCNRRDownload bCNRRDownload = new BCNRRDownload();
            List<BE.BCNRRWagon> cNRRWagonsList = new List<BCNRRWagon>();
            DataTable HData = new DataTable();
            DataTable WagonData = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ds = db.sub_GetDataSets("Usp_BCNRR_GridViewBind " + igmid);
            HData = ds.Tables[0];
            WagonData = ds.Tables[1];

            CountTeus count = new CountTeus();

            if(HData != null)
            {
                if (HData.Rows.Count > 0)
                {
                    foreach(DataRow data in HData.Rows)
                    {
                        bCNRRDownload.RRWagonNo = data["RRNo"].ToString();
                        bCNRRDownload.ArrivalDate =Convert.ToDateTime(data["RRDate"].ToString());
                    }
                }
            }

            if (WagonData != null)
            {
                if (WagonData.Rows.Count > 0)
                {
                    foreach (DataRow data in WagonData.Rows)
                    {
                        BCNRRWagon wagon = new BCNRRWagon();
                        wagon.SRNo = Convert.ToInt32(data["SrNo"]);
                        wagon.WagonType = data["WagonType"].ToString();
                        wagon.WagonNo = data["WagonNo"].ToString();
                        wagon.Wongly = data["Wongly"].ToString();
                        wagon.PCCWeight = Convert.ToDouble(data["PCCWeight"]);
                        wagon.CCWeight = Convert.ToDouble(data["CCWeight"]);
                        wagon.TareWeight = Convert.ToDouble(data["TareWt"]);
                        wagon.Pkgs = Convert.ToDouble(data["Pkg"]);

                        count.Package = count.Package + Convert.ToInt32(data["Pkg"]);
                        count.CCWeight = count.CCWeight + (Convert.ToInt32(data["PCCWeight"])*1000);
                        count.counter = count.counter + 1;

                        cNRRWagonsList.Add(wagon);
                    }
                }
            }

            var ContainerList = JsonConvert.SerializeObject(WagonData);

            var returnField = new { RRdata = bCNRRDownload, WagonList = cNRRWagonsList,WagonSum=count };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult ShowBCNWagonMapping(string TrainNo, string WagonRRNo,string WagonNo,string WagonPkgs,string WagonWt,string ContainerNo,string StuffPkgs,string StuffWt,string OldWagonNo,string IsReplace)
        {
            string Message = "";

            if (TrainNo == null && Session["BCNTrainNo"] !=null)
            {
                TrainNo = Session["BCNTrainNo"].ToString();
            }

            if (WagonRRNo == null && Session["BCNWagonRRNo"] !=null)
            {
                WagonRRNo = Session["BCNWagonRRNo"].ToString();
            }

            try
            {
                if (WagonNo != null && ContainerNo != null && StuffPkgs != null)
                {
                    DataTable dtNewWagon = new DataTable();
                    HC.DBOperations dbNew = new HC.DBOperations();
                    dtNewWagon = dbNew.sub_GetDatatable("USP_InsTempBCNAddNewWagonData '" + TrainNo + "','" + WagonRRNo + "','" + WagonNo + "','" + WagonPkgs + "','" + WagonWt + "','" + ContainerNo + "','" + StuffPkgs + "','" + StuffWt + "','" + OldWagonNo + "','" + IsReplace + "'");

                    if (dtNewWagon.Rows.Count > 0)
                    {
                        Session["BCNTrainNo"] = TrainNo;
                        Session["BCNWagonRRNo"] = WagonRRNo;
                        Message = dtNewWagon.Rows[0]["Message"].ToString();
                    }
                    else
                    {
                        Message = "Invalid Details. Please try Again.";
                    }

                }
            }
            catch(Exception ex)
            {
                Message = "Invalid Details. Please try Again.";
            }

            List<BE.BCNRRWagon> cNRRWagons = new List<BCNRRWagon>();

            if (Message == "")
            {
                DataTable dt = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                dt = db.sub_GetDatatable("GetWagonContainerMappingDet '" + TrainNo + "','" + WagonRRNo + "'");

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow data in dt.Rows)
                        {
                            BE.BCNRRWagon bCNRRWagon = new BCNRRWagon();
                            bCNRRWagon.SRNo = Convert.ToInt32(data["SrNo"]);
                            bCNRRWagon.WagonNo = Convert.ToString(data["WagonNo"]);
                            bCNRRWagon.Pkgs = Convert.ToDouble(data["WagonPkgs"]);
                            bCNRRWagon.CCWeight = Convert.ToDouble(data["WagonWt"]);
                            bCNRRWagon.RemainingPkgs = Convert.ToDouble(data["RemainingPkgs"]);
                            bCNRRWagon.RemainingWt = Convert.ToDouble(data["RemainingWt"]);
                            bCNRRWagon.ContainerNo = Convert.ToString(data["ContainerNo"]);
                            bCNRRWagon.Date = Convert.ToString(data["StuffOrderDate"]);
                            bCNRRWagon.StuffPkgs = Convert.ToDouble(data["StuffPkgs"]);
                            bCNRRWagon.StuffWt = Convert.ToDouble(data["StuffWt"]);
                            bCNRRWagon.Remarks = Convert.ToString(data["Remarks"]);
                            bCNRRWagon.EntryId = Convert.ToInt64(data["EntryId"]);

                            cNRRWagons.Add(bCNRRWagon);
                        }
                    }
                }
            }

            //var ContainerList = JsonConvert.SerializeObject(cNRRWagons);

            var returnField = new { WagonContainerList = cNRRWagons , Message=Message ,TrainNumber=TrainNo,WagonRRNumber=WagonRRNo };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult GridViewBindContDetailsNew(string RRNo)
        {
            int igmid = Convert.ToInt32(Session["RRFileId"]);
            DataSet ds = new DataSet();
            BE.BCNRRDownload bCNRRDownload = new BCNRRDownload();
            List<BE.BCNRRWagon> cNRRWagonsList = new List<BCNRRWagon>();
            DataTable WagonData = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ds = db.sub_GetDataSets("Usp_BCNContainer_GridViewBind '" + igmid + "','" + RRNo + "'");
            WagonData = ds.Tables[0];

            CountTeus count = new CountTeus();

            if (WagonData != null)
            {
                if (WagonData.Rows.Count > 0)
                {
                    foreach (DataRow data in WagonData.Rows)
                    {
                        BCNRRWagon wagon = new BCNRRWagon();
                        wagon.SRNo = Convert.ToInt32(data["SrNo"]);
                        wagon.ContainerNo = data["ContainerNo"].ToString();
                        wagon.BookingNo = data["BookingNo"].ToString();
                        wagon.WTRepNo = data["WtReptNo"].ToString();
                        wagon.Date = Convert.ToString(data["DateTime"]);
                        wagon.CCWeight = Convert.ToDouble(data["ConTare"]);
                        wagon.Pkgs = Convert.ToDouble(data["Package"]);
                        wagon.PCCWeight = Convert.ToDouble(data["GrossWeight"]);
                        wagon.TareWeight = Convert.ToDouble(data["TareWeight"]);
                        wagon.NetWeight = Convert.ToDouble(data["NetWeight"]);
                        wagon.CustomSealNo = Convert.ToString(data["CustomSealNo"]);
                        wagon.AgentSealNo = Convert.ToString(data["AgentSealNo"]);

                        count.Package = count.Package + Convert.ToInt32(data["Package"]);
                        count.CCWeight = count.CCWeight + Convert.ToInt32(data["GrossWeight"]);
                        count.counter = count.counter + 1;

                        cNRRWagonsList.Add(wagon);
                    }
                }
            }

            var ContainerList = JsonConvert.SerializeObject(WagonData);

            var returnField = new { RRdata = bCNRRDownload, WagonList = cNRRWagonsList, WagonSum = count };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult SaveRRDownloadForm(BE.BCNRRDownload viewModel)
        {
            string message = "";
            int RRFileId = Convert.ToInt32(Session["RRFileId"]);
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            if (RRFileId <= 0 )
            {
                message = "Data not properly inserted. Plz Try Again";
            }
            else
            {
                message = BL.AddBCNRRDownloadSave(viewModel, userId, RRFileId);
            }
            // ViewBag.Message = message;

            return Json(message);
        }

        public ActionResult ExportToExcelRRDownload()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ContainerArrival = (DataTable)Session["RRWagonDetails"];
            GridView gridview1 = new GridView();
            gridview1.DataSource = ContainerArrival;
            gridview1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=RRDownloadSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> RR Download Summary Details<strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview1.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        [HttpPost]
        public JsonResult SaveBCNMapping(List<BCNRRWagon> containerData,string TrainNo, string WagonRRNo)
        {
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            int RetVal = 0;
            if (ModelState.IsValid)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("WagonNo");
                dt.Columns.Add("EntryId");
                dt.Columns.Add("ContainerNo");
                dt.Columns.Add("StuffPkgs");
                dt.Columns.Add("StuffWt");
                dt.Columns.Add("Remarks");

                if (containerData.Count > 0)
                {
                    foreach(BCNRRWagon wagon in containerData)
                    {
                        DataRow row = dt.NewRow();
                        row["WagonNo"] = wagon.WagonNo;
                        row["EntryId"] = wagon.EntryId;
                        row["ContainerNo"] = wagon.ContainerNo;
                        row["StuffPkgs"] = wagon.StuffPkgs;
                        row["StuffWt"] = wagon.StuffWt;
                        row["Remarks"] = wagon.Remarks;
                        dt.Rows.Add(row);
                    }
                }

                RetVal = BL.AddContainerCLPData(dt, userId, TrainNo, WagonRRNo);

                Session.Remove("BCNTrainNo");
                Session.Remove("BCNWagonRRNo");
            }

           return Json(RetVal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetBCNCLPSealDet(string TrainNo,string ContainerNo)
        {
            BE.BCNSBContainerMapping containerMapping = new BCNSBContainerMapping();
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            try
            {
                dt = db.sub_GetDatatable("GetBCNCLPSealDet '" + TrainNo + "','" + ContainerNo + "'");

                if(dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        containerMapping.CustomSealNo = Convert.ToString(dt.Rows[0]["CustomSealNo"]);
                        containerMapping.AgenSealNo = Convert.ToString(dt.Rows[0]["AgentSealNo"]);
                        containerMapping.TotalPkgs = Convert.ToInt32(dt.Rows[0]["StuffPkgs"]);
                        containerMapping.TareWeight = Convert.ToString(dt.Rows[0]["StuffWt"]);
                    }
                }
            }
            catch (Exception ex) { }

            return Json(containerMapping, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ContainerShow(string ContainerNo,string RRType)
        {
            BE.BCNSBContainerMapping containerMapping = new BCNSBContainerMapping();
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            DataTable ValidateDt = new DataTable();
            List<BE.Train> trainList = new List<BE.Train>();

            ValidateDt = db.sub_GetDatatable("ValidateBCNCLPCont '" + ContainerNo + "','" + RRType + "'");

            if(ValidateDt.Rows.Count > 0)
            {
                if (ValidateDt.Rows.Count <= 0)
                {
                    containerMapping.ErrMessage = "Invalid Container No.";
                }
                else
                {
                    if (RRType == "MultipleRR")
                    {
                        foreach(DataRow row  in ValidateDt.Rows)
                        {
                            BE.Train train = new Train();
                            train.TrainNo = row["TrainNo"].ToString();
                            trainList.Add(train);
                        }
                    }
                    else
                    {
                        containerMapping.TrainNo = Convert.ToString(ValidateDt.Rows[0]["TrainNo"]);
                        BE.Train train = new Train();
                        train.TrainNo = containerMapping.TrainNo;
                        trainList.Add(train);
                        if (Convert.ToInt32(ValidateDt.Rows[0]["StuffRequestId"]) > 0)
                        {
                            containerMapping.ErrMessage = "Container Already Mapped with Train No :- " + containerMapping.TrainNo;
                        }
                    }
                }
            }
             

            if (containerMapping.ErrMessage == "")
            {
                dt = db.sub_GetDatatable("USP_ShowContainerwiseDetails '" + ContainerNo + "'");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        containerMapping.ShippingLine = Convert.ToString(dt.Rows[0]["SLName"]);
                        containerMapping.EntryId = Convert.ToInt64(dt.Rows[0]["entryid"]);
                        containerMapping.Size = Convert.ToString(dt.Rows[0]["size"]);
                        containerMapping.ContainerType = Convert.ToString(dt.Rows[0]["containertype"]);
                        containerMapping.TareWeight = Convert.ToString(dt.Rows[0]["tareweight"]);
                        containerMapping.ISOCode = Convert.ToString(dt.Rows[0]["ISOCODE"]);
                        containerMapping.CustomerName = Convert.ToString(dt.Rows[0]["AGName"]);
                        containerMapping.TotalPkgs = Convert.ToInt32(dt.Rows[0]["StuffPkgs"]);
                        containerMapping.NetWeight = Convert.ToDouble(dt.Rows[0]["StuffWt"]);

                        if (ValidateContHold(containerMapping.ContainerNo, containerMapping.EntryId) == false)
                        {
                            containerMapping.ErrMessage = "Container are on Hold. Cant not be proceed.";
                        }
                    }
                    else
                    {
                        containerMapping.ErrMessage = "Invlaid Container No.";
                    }
                }
                else
                {
                    containerMapping.ErrMessage = "Invlaid Container No.";
                }
            }

            var returnField = new { ContainerDetails = containerMapping , TrainList=trainList };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return Json(containerMapping, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ShippingBillShow(string ShippingBillNo)
        {
            BE.BCNSBContainerMapping containerMapping = new BCNSBContainerMapping();
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_ShowSBWiseSTUFFING '" + ShippingBillNo + "'");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    containerMapping.StuffingType = Convert.ToString(dt.Rows[0]["stuffingtype"]);
                    containerMapping.CargoDesc = Convert.ToString(dt.Rows[0]["ShipperName"]);
                    containerMapping.CHAName = Convert.ToString(dt.Rows[0]["CHAName"]);
                    containerMapping.LEONo = Convert.ToString(dt.Rows[0]["LEONo"]);
                    containerMapping.LEODate = Convert.ToString(dt.Rows[0]["LEODate"]);
                    containerMapping.CargoDesc = Convert.ToString(dt.Rows[0]["CargoDesc"]);
                    containerMapping.SBEntryId = Convert.ToString(dt.Rows[0]["ENTRYID"]);
                    containerMapping.SBQty = Convert.ToString(dt.Rows[0]["TotalPKGS"]);
                    containerMapping.CargoType = Convert.ToString(dt.Rows[0]["Cargotype"]);
                    containerMapping.Shipper = Convert.ToString(dt.Rows[0]["shippername"]);
                    containerMapping.ClassNo = Convert.ToString(dt.Rows[0]["ClassNo"]);
                }
                else
                {
                    containerMapping.ErrMessage = "Invlaid Shipping Bill No.";
                }
            }
            else
            {
                containerMapping.ErrMessage = "Invlaid Shipping Bill No.";
            }

            return Json(containerMapping, JsonRequestBehavior.AllowGet);
        }

        private bool ValidateContHold(string ContainerNo,long EntryId)
        {
            string strSQL = "";
            bool retval = true;
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            strSQL = "SELECT  * FROM exp_HoldDetails, exp_HoldReasons WHERE exp_HoldReasons.ReasonID=exp_HoldDetails.HoldID AND containerno='" + ContainerNo + "' and EntryID=" + EntryId + "  and Status='H' and IsCancel=0 ";
            dt = db.sub_GetDatatable(strSQL);
            if (dt.Rows.Count > 0)
            {
                retval = false;
            }

           return retval;
        }

        [HttpPost]
        public JsonResult BCNMappingSBSave(List<BCNSBContainerMapping> ContData, string StuffingType,string VIANo,string VesselName,string POL,string POD,string CutoffDate,string Voyage,string Remarks,string Rotation,string RRType)
        {
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            int RetVal = 0;
            int counter = 1;
            try
            {
                if (ModelState.IsValid)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("SrNo");
                    dt.Columns.Add("ContainerNo");
                    dt.Columns.Add("EntryId");
                    dt.Columns.Add("ISOCode");
                    dt.Columns.Add("SBNo");
                    dt.Columns.Add("SBEntryId");
                    dt.Columns.Add("VIANo");
                    dt.Columns.Add("VesselName");
                    dt.Columns.Add("POL");
                    dt.Columns.Add("POD");
                    dt.Columns.Add("CutoffDate");
                    dt.Columns.Add("Voyage");
                    dt.Columns.Add("Remarks");
                    dt.Columns.Add("AgentSealNo");
                    dt.Columns.Add("CustomSealNo");
                    dt.Columns.Add("TrainNo");
                    dt.Columns.Add("TareWeight");
                    dt.Columns.Add("NetWeight");
                    dt.Columns.Add("TotalPkgs");

                    if (ContData.Count > 0)
                    {
                        foreach (BCNSBContainerMapping mapping in ContData)
                        {
                            DataRow dr = dt.NewRow();
                            dr["SrNo"] = counter;
                            dr["ContainerNo"] = mapping.ContainerNo;
                            dr["EntryId"] = mapping.EntryId;
                            dr["ISOCode"] = mapping.ISOCode;
                            dr["SBNo"] = mapping.SBNo;
                            dr["SBEntryId"] = mapping.SBEntryId;
                            dr["VIANo"] = VIANo;
                            dr["VesselName"] = VesselName;
                            dr["POL"] = POL;
                            dr["POD"] = POD;
                            dr["CutoffDate"] = CutoffDate;
                            dr["Voyage"] = Voyage;
                            dr["Remarks"] = Remarks;
                            dr["AgentSealNo"] = mapping.AgenSealNo;
                            dr["CustomSealNo"] = mapping.CustomSealNo;
                            dr["TrainNo"] = mapping.TrainNo;
                            dr["TareWeight"] = mapping.TareWeight;
                            dr["NetWeight"] = mapping.NetWeight;
                            dr["TotalPkgs"] = mapping.TotalPkgs;
                            dt.Rows.Add(dr);
                            counter = counter + 1;
                        }
                    }

                    RetVal = BL.AddSBMappingData(dt, userId, StuffingType,RRType);
                }
            }
            catch(Exception ex)
            {

            }
            
            return Json(RetVal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidateSBTrain(string ContainerNo,string TrainNo)
        {
            string Message = "";

            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();

            try
            {
                dt = db.sub_GetDatatable("GetValidateTrainBCN '" + ContainerNo + "','" + TrainNo + "'");

                if(dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Message = dt.Rows[0]["ErrorMessage"].ToString();
                    }
                    else
                    {
                        Message = "Invlaid Train No";
                    }
                }
                else
                {
                    Message = "Invlaid Train No";
                }
            }
            catch(Exception ex)
            {
                Message = "Invlaid Train No";
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getVesselDetails(string viaNo)
        {
            BE.JobOrderMEntities JE = new BE.JobOrderMEntities();
            JE = BL.getVesselDetailsExp(viaNo);
            return Json(JE);
        }

        [HttpPost]
        public ActionResult BCNCLPSummary(string FromDate, string ToDate)
        {
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("GetCLPSummary '" + FromDate + "','" + ToDate + "'");
            Session["BCNCLPSummaryDet"] = getAuctionGenList;
            Session["CFromDate"] = FromDate;
            Session["CToDate"] = ToDate;
            var json = "";

            if (getAuctionGenList.Rows.Count > 0)
            {
                json = JsonConvert.SerializeObject(getAuctionGenList);
            }
            else { json = "0"; }

            var returnField = new { DocumentList = json };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult BCNCLPSummaryExport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string FromDate = Session["CFromDate"].ToString();
            string ToDate = Session["CToDate"].ToString();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["BCNCLPSummaryDet"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BCNCLPSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> BCN CLP SUmmary Details<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> FromDate :- " + FromDate + " To Date:- "+ todate + "<strong></td></tr>");
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

        [HttpPost]
        public ActionResult BCNWagonMappingSummary(string FromDate, string ToDate,string TrainNo)
        {
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("GetWagonMappingSummary '" + FromDate + "','" + ToDate + "','" + TrainNo + "'");
            Session["BCNWagonSummaryDet"] = getAuctionGenList;
            Session["CFromDate"] = FromDate;
            Session["CToDate"] = ToDate;
            var json = "";

            if (getAuctionGenList.Rows.Count > 0)
            {
                json = JsonConvert.SerializeObject(getAuctionGenList);
            }
            else { json = "0"; }

            var returnField = new { DocumentList = json };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult BCNWagonMappingSummaryExport(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string FromDate = Session["CFromDate"].ToString();
            string ToDate = Session["CToDate"].ToString();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["BCNWagonSummaryDet"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BCNWagonMappingSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " </strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " </strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> BCN Wagon Mapping Summary </strong ></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> FromDate :- " + FromDate + " To Date:- " + ToDate + "</strong></td></tr>");
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

        [HttpPost]
        public JsonResult getAutoRRNoList(string prefixText, string CustomerType)
        {
            List<autoRRWagonNo> wagonList = new List<autoRRWagonNo>();
            DataTable ddt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            if (prefixText != "")
            {
                ddt = db.sub_GetDatatable("getAutoTrainDet '" + prefixText + "'");

                if(ddt != null)
                {
                    foreach(DataRow dataRow in ddt.Rows)
                    {
                        autoRRWagonNo autoRR = new autoRRWagonNo();
                        autoRR.TrainNo = dataRow["TrainNo"].ToString();
                        autoRR.WagonRRNo = dataRow["WagonRRNo"].ToString();
                        autoRR.WagonRRId = dataRow["WagonRRNo"].ToString();
                        autoRR.FromStationId = Convert.ToInt32(dataRow["OrginStationId"]);
                        autoRR.ToStationId = Convert.ToInt32(dataRow["ToLocation"]);
                        autoRR.CommodityId = Convert.ToInt32(dataRow["CommodityId"]);
                        autoRR.PartyId = Convert.ToInt32(dataRow["PartyId"]);

                        wagonList.Add(autoRR);
                    }
                }
            }

            return Json(wagonList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getWagonRRNo(string TrainNo)
        {
            BE.BCNSBContainerMapping containerMapping = new BCNSBContainerMapping();
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("SELECT WagonRRNo FROM BCN_RRDownload WHERE TrainNo='" + TrainNo + "'");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    containerMapping.TrainNo = dt.Rows[0]["WagonRRNo"].ToString();
                }
                else
                {
                    containerMapping.ErrMessage = "Invlaid Train No.";
                }
            }
            else
            {
                containerMapping.ErrMessage = "Invlaid Train No.";
            }

            return Json(containerMapping, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getBCNInvoiceSummary(string Criteria, string FromDate, string ToDate)
        {
            DataTable getMovementICDNew = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            getMovementICDNew = db.sub_GetDatatable("getBCNInvoiceSummaryReport  '" + FromDate + "','" + ToDate + "','" + Criteria + "'");
            //GetDailyActivityTripINOutreport.Columns.Remove("SR_NO");
            Session["getBCNSummary"] = getMovementICDNew;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            var json = JsonConvert.SerializeObject(getMovementICDNew);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelBCNSummary(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["getBCNSummary"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BCNInvoiceSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> BCN Invocie Summary Report<strong></td></tr>");
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

        [HttpPost]
        public JsonResult BCNEditNOC(string NocNo, string WOYear)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            BE.BCNNocUpdation nOCUpdation = new BCNNocUpdation();

            tblInvoiceList = db.sub_GetDatatable("getNOCUpdateionDet '" + NocNo + "','" + WOYear + "'");
            if (tblInvoiceList.Rows.Count > 0)
            {
                nOCUpdation.NOCNo =Convert.ToString(tblInvoiceList.Rows[0]["NOCNo"]);
                nOCUpdation.NOCDate = Convert.ToString(tblInvoiceList.Rows[0]["NOCDate"]);
                nOCUpdation.ShipperId = Convert.ToInt32(tblInvoiceList.Rows[0]["SLid"]);
                nOCUpdation.OrginStationId = Convert.ToInt32(tblInvoiceList.Rows[0]["OrginStationId"]);
                nOCUpdation.CommodityId = Convert.ToInt32(tblInvoiceList.Rows[0]["CommodityId"]);
                nOCUpdation.StuffingTypeId = Convert.ToInt32(tblInvoiceList.Rows[0]["StuffingTypeId"]);
                nOCUpdation.NoOfWagonPlanned = Convert.ToInt32(tblInvoiceList.Rows[0]["NoofWagonPlanned"]);
                nOCUpdation.PlanedDate = Convert.ToString(tblInvoiceList.Rows[0]["PlannedDate"]);
                nOCUpdation.StuffingDate = Convert.ToString(tblInvoiceList.Rows[0]["StuffingDate"]);
                nOCUpdation.ETADate = Convert.ToString(tblInvoiceList.Rows[0]["ETADate"]);
                nOCUpdation.ArrivalDate = Convert.ToString(tblInvoiceList.Rows[0]["ArrivalDate"]);
                nOCUpdation.ErrorMessage = "";

            }
            else
            {
                nOCUpdation.ErrorMessage = "No Data Found.";
            }

            return new JsonResult() { Data = nOCUpdation, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult BCNWagonPrint(string RRNo)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblHeaderDetails = new DataTable();
            DataTable tblBCNWagonDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetBCNWagonPrint '" + RRNo + "'");
            if (getJobOrderSet != null)
            {
                tblHeaderDetails = getJobOrderSet.Tables[0];
                tblBCNWagonDetails = getJobOrderSet.Tables[1];
            }

            if (tblHeaderDetails != null)
            {
                ViewBag.TrainNo = tblHeaderDetails.Rows[0]["TrainNo"].ToString();
                ViewBag.WagonRRNo = tblHeaderDetails.Rows[0]["WagonRRNo"].ToString();
                ViewBag.PartyName = tblHeaderDetails.Rows[0]["PartyName"].ToString();
                ViewBag.Commodity = tblHeaderDetails.Rows[0]["Commodity"].ToString();
                ViewBag.ArrivalDate = tblHeaderDetails.Rows[0]["ArrivalDate"].ToString();
            }

            ViewBag.WagonList = tblBCNWagonDetails.AsEnumerable();


            return PartialView("BCNWagonPrint");

        }
        public JsonResult ShowCanclBCNWagonMapping(string TrainNo, string WagonRRNo )
        {
            string Message = "";

            

            List<BE.BCNRRWagon> cNRRWagons = new List<BCNRRWagon>();

            if (Message == "")
            {
                DataTable dt = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                dt = db.sub_GetDatatable("GetWagonContainerMappingDet '" + TrainNo + "','" + WagonRRNo + "'");

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow data in dt.Rows)
                        {
                            BE.BCNRRWagon bCNRRWagon = new BCNRRWagon();
                            bCNRRWagon.SRNo = Convert.ToInt32(data["SrNo"]);
                            bCNRRWagon.WagonNo = Convert.ToString(data["WagonNo"]);
                            bCNRRWagon.Pkgs = Convert.ToDouble(data["WagonPkgs"]);
                            bCNRRWagon.CCWeight = Convert.ToDouble(data["WagonWt"]);
                            bCNRRWagon.RemainingPkgs = Convert.ToDouble(data["RemainingPkgs"]);
                            bCNRRWagon.RemainingWt = Convert.ToDouble(data["RemainingWt"]);
                            bCNRRWagon.ContainerNo = Convert.ToString(data["ContainerNo"]);
                            bCNRRWagon.Date = Convert.ToString(data["StuffOrderDate"]);
                            bCNRRWagon.StuffPkgs = Convert.ToDouble(data["StuffPkgs"]);
                            bCNRRWagon.StuffWt = Convert.ToDouble(data["StuffWt"]);
                            bCNRRWagon.Remarks = Convert.ToString(data["Remarks"]);
                            bCNRRWagon.EntryId = Convert.ToInt64(data["EntryId"]);

                            cNRRWagons.Add(bCNRRWagon);
                        }
                    }
                }
            }

            //var ContainerList = JsonConvert.SerializeObject(cNRRWagons);

            var returnField = new { WagonContainerList = cNRRWagons, Message = Message, TrainNumber = TrainNo, WagonRRNumber = WagonRRNo };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }

    public class CountTeus
    {
        public int counter { get; set; }
        public int CCWeight { get; set; }
        public int TareWeight { get; set; }
        public int Package { get; set; }

        public CountTeus()
        {
            counter = 0;
            CCWeight = 0;
            TareWeight = 0;
            Package = 0;
        }
    }

    public class autoRRWagonNo
    {
        public string TrainNo { get; set; }
        public string WagonRRNo { get; set; }
        public string WagonRRId { get; set; }
        public int FromStationId { get; set; }
        public int ToStationId { get; set; }
        public int CommodityId { get; set; }
        public int PartyId { get; set; }

        public autoRRWagonNo()
        {
            TrainNo = "";
            WagonRRNo ="";
            WagonRRId = "";
            FromStationId = 0;
            ToStationId = 0;
            CommodityId = 0;
            PartyId = 0;
        }
    }
}