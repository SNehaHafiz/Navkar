using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using train = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrainMaster;
using BE = TrackerMVCEntities.BusinessEntities;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Report;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using TrackerMVC.Filters;
using System.Data;
using System.IO;
using System.Data.Sql;
using HC = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using TrackerMVCEntities.BusinessEntities;

namespace TrackerMVC.Controllers.AuctionController
{
    public class AuctionController : Controller
    {
        // GET: Auction
        BM.Auction.AuctionNoticeGeneration AuctionBl = new BM.Auction.AuctionNoticeGeneration();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImportLongStandingContainer()
        {
            return View();
        }

        public ActionResult AuctionInvoiceRequest()
        {
            return View();
        }

        public ActionResult AuctionNoticeGeneration()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        public ActionResult AuctionNoticeSummary()
        {
            //ViewBag.Date = DateTime.Now.ToLocalTime().ToString("dd MMM yyyy");
            return View();
        }

        public ActionResult AuctionNoticeStatus()
        {
            return View();
        }
        public ActionResult AuctionClearanceStatus()
        {
            List<BE.AuctionClearM> Auction = new List<BE.AuctionClearM>();

            Auction = AuctionBl.getAuctionClear();

            ViewBag.AuctionClear = new SelectList(Auction, "ClearId", "ClearRemarks");

            return View();
        }

        public JsonResult ViewAuctionNoticeGen(string IGMNo,string ItemNo)
        {

            return Json("");
        }

        public JsonResult SaveAuctionForm(IGMEntities viewModel,List<ContainerDetails> containerDetails,string NoticeType,string NoticeDate)
        {
            DataSet dataSet = new DataSet();
            string message = "";
            int userId =Convert.ToInt32(Session["Tracker_userID"]);
            int AuctionId = 0;
            DataTable containerDetailsDT = new DataTable();
            containerDetailsDT.Columns.Add("ContainerNo");
            containerDetailsDT.Columns.Add("ContainerSize");
            containerDetailsDT.Columns.Add("ContainerType");
            containerDetailsDT.Columns.Add("ArrivalDate");
            containerDetailsDT.Columns.Add("Pkgs");
            containerDetailsDT.Columns.Add("Weight");
            containerDetailsDT.Columns.Add("IGMNo");
            containerDetailsDT.Columns.Add("ItemNo");
            containerDetailsDT.Columns.Add("User_id");

            foreach (ContainerDetails container in containerDetails)
            {
                DataRow row = containerDetailsDT.NewRow();
                row["ContainerNo"] = container.ContainerNO;
                row["ContainerSize"] = container.Size;
                row["ContainerType"] = container.Type;
                row["ArrivalDate"] = container.ArrivalDate;
                row["Pkgs"] = container.PKGS;
                row["Weight"] = container.Weight;
                row["IGMNo"] = viewModel.IGMNo;
                row["ItemNo"] = viewModel.ItemNo;
                row["User_id"] = userId;
                containerDetailsDT.Rows.Add(row);
            }
            

            if (message == "")
            {
                dataSet = AuctionBl.AddAuctionGenData(viewModel, containerDetailsDT, userId, NoticeType, NoticeDate);
            }

            DataTable DT1 = new DataTable();
            DataTable DT2 = new DataTable();
            if (dataSet != null)
            {
                DT1 = dataSet.Tables[0];
                DT2 = dataSet.Tables[1];
            }

            var GenDataList = JsonConvert.SerializeObject(DT1);
            var gen = 0;
            if (DT2.Rows.Count <= 0)
            {
                gen = 0;
            }
            else
            {
                Session["AuctionGenAlreadyList"] = DT2;
                gen = 1;
            }
            

            var returnField = new { DataList = GenDataList, ErrorList = gen };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GenerateAutoAuction(string AutoNoticeType)
        {
            DataSet dataSet = new DataSet();
            string message = "";
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            
            if (message == "")
            {
                dataSet = AuctionBl.AddAuctionGenAutoData(userId, AutoNoticeType);
            }

            DataTable DT1 = new DataTable();
            if (dataSet != null)
            {
                DT1 = dataSet.Tables[0];
            }

            var GenDataList = JsonConvert.SerializeObject(DT1);
            var gen = 0;
            if (DT1.Rows.Count <= 0)
            {
                gen = 0;
            }
            else
            {
                gen = 1;
            }


            var returnField = new { DataList = GenDataList, ErrorList = gen };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SaveAuctionStatus(string IGMNo, string ItemNo, string ContainerNo,string Remarks,string ReferenceNo)
        {
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";
            Message=AuctionBl.AddAuctionStatus(IGMNo, ItemNo, ContainerNo,userId, Remarks, ReferenceNo);

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveAuctionClearDetails(string IGMNo, string ItemNo, string ContainerNo, string Value, string DutyValue, string ClearRemarks)
        {
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";
            Message = AuctionBl.AddAuctionClearDetails(IGMNo, ItemNo, ContainerNo, userId, Value, DutyValue, ClearRemarks);

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getImportLongSContainer(string AsOnDate, string Fromday, string Today, string Criteria, string Search)
        {
            DataTable getAuctionNotice = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionNotice = db.sub_GetDatatable("sp_AuctionNotice  '" + AsOnDate + "','" + Fromday + "','" + Today + "','" + Criteria + "','" + Search + "'");
            Session["GetAuctionNotice"] = getAuctionNotice;
            var json = JsonConvert.SerializeObject(getAuctionNotice);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult getAuctionGenList(string IGMNo, string ItemNo)
        {
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("sp_IGMwiseAuctionNotice  '" + "" + "','" + IGMNo + "','" + ItemNo + "'");
            List<ContainerDetails> containerDetailsList = new List<ContainerDetails>();
            List<IGMEntities> documentDetailsList = new List<IGMEntities>();

            foreach (DataRow dataRow in getAuctionGenList.Rows)
            {
                ContainerDetails containerDetails = new ContainerDetails();
                IGMEntities documentDetails = new IGMEntities();
                containerDetails.ContainerNO = dataRow["Container No"].ToString();
                containerDetails.Size = dataRow["Size"].ToString();
                containerDetails.Type = dataRow["Type"].ToString();
                containerDetails.ArrivalDate = dataRow["Arrival Date"].ToString();
                containerDetails.PKGS =Convert.ToInt64(dataRow["No of PKGS"]);
                containerDetails.Weight = Convert.ToDouble(dataRow["Weight"].ToString());

                documentDetails.IGMNo = dataRow["IGM No"].ToString();
                documentDetails.JoNo = dataRow["JO No"].ToString();
                documentDetails.ItemNo = dataRow["Item No"].ToString();
                documentDetails.IGMDate = dataRow["IGM Date"].ToString();
                //documentDetails.Date = dataRow[""].ToString();
                documentDetails.NoticeId = "";// dataRow[""].ToString();
                documentDetails.NoticeDate = "";//dataRow[""].ToString();
                documentDetails.NoticeType = "";//dataRow[""].ToString();
                documentDetails.Agent = dataRow["Line"].ToString();
                documentDetails.CargoDescription = dataRow["Cargo Description"].ToString();
                documentDetails.ImporterName = dataRow["NConsignee"].ToString();
                documentDetails.NotifyParty = "";// dataRow[""].ToString();
                documentDetails.NoOfPkgs = dataRow["No of PKGS"].ToString();
                documentDetails.PackageType = dataRow["PKGS Type"].ToString();
                documentDetails.Weight = dataRow["Gross Wt(Kg.)"].ToString();
                documentDetails.UOM = "";//dataRow[""].ToString();
                documentDetails.BLNo = dataRow["BL No"].ToString();
                documentDetails.BLDate = dataRow["IGM_BLDate"].ToString();

                containerDetailsList.Add(containerDetails);
                documentDetailsList.Add(documentDetails);
            }

            var returnField = new { DocumentList = documentDetailsList, ContainerList = containerDetailsList };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult getUpdateStatus(string IGMNo, string ItemNo,string ContainerNo)
        {
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("sp_IGMwiseAuctionNoticeDet  '" + ContainerNo + "','" + IGMNo + "','" + ItemNo + "'");

            var json = "";

            if (getAuctionGenList.Rows.Count > 0)
            {
                 json = JsonConvert.SerializeObject(getAuctionGenList);
            }
            else { json = "0"; }

            var returnField = new { DocumentList = json };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult getAuctionUpdatedStatus()
        {
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("GetAuctionStatusUpdatedList");

            var json = "";

            if (getAuctionGenList.Rows.Count > 0)
            {
                json = JsonConvert.SerializeObject(getAuctionGenList);
            }
            else { json = "0"; }

            var returnField = new { DocumentList = json };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult getAuctionClearDetails()
        {
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("GetAuctionClearDetailsList");
            Session["AuctionClearDet"] = getAuctionGenList;
            var json = "";

            if (getAuctionGenList.Rows.Count > 0)
            {
                json = JsonConvert.SerializeObject(getAuctionGenList);
            }
            else { json = "0"; }

            var returnField = new { DocumentList = json };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ExportAuctionClearDetails(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["AuctionClearDet"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=AuctionClearDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Auction Clear Details<strong></td></tr>");
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

        

        public ActionResult getAuctionNoticeGeneratedList(string Search,string ReportDate,string IGMNo,string ItemNo)
        {
            if (ReportDate == null)
            {
                ReportDate = "";
            }
            if (IGMNo == null)
            {
                IGMNo = "";
            }
            if (ItemNo == null)
            {
                ItemNo = "";
            }
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("GetAuctionNoticeGeneratedList '" + Search + "','" + ReportDate + "','" + IGMNo + "','" + ItemNo + "'");

            var json = "";

            if (getAuctionGenList.Rows.Count > 0)
            {
                json = JsonConvert.SerializeObject(getAuctionGenList);
            }
            else { json = "0"; }

            var returnField = new { DocumentList = json };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getAuctionNoticeSummary(string NoticeType, string IGMNo, string ItemNo)
        {
            
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("GetAuctionNoticeSummary '" + NoticeType + "','" + IGMNo + "','" + ItemNo + "'");

            var jsonData = JsonConvert.SerializeObject(getAuctionGenList);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalcelAuctionNotice(string NoticeType, string IGMNo, string ItemNo)
        {
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("INS_CancelAuctionNotice '" + NoticeType + "','" + IGMNo + "','" + ItemNo + "','" + userId + "'");
            Message = getAuctionGenList.Rows[0]["ErrMessage"].ToString();
            var jsonData = JsonConvert.SerializeObject(Message);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportToExcelLongStanding()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["GetAuctionNotice"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=LongStandingCtrl.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Import Long Standing Container <strong></td></tr>");
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

        public ActionResult ExportToExcelActionGen()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["AuctionGenAlreadyList"];
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=AlreadyGenAutionNotice.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Action Notice Already Generated <strong></td></tr>");
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
        public ActionResult FirstAuctionNoticePrint(string IGMNo,string ItemNo, string AuctionId)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblDocumentDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetAuctionNoticePrint '" + IGMNo + "','" + ItemNo + "','" + AuctionId + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblDocumentDetails = getJobOrderSet.Tables[1];
                tblContainerDetails = getJobOrderSet.Tables[2];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.CorporateOffice = dr["CorporateOffice"];
                }
                foreach (DataRow dr in tblDocumentDetails.Rows)
                {
                    ViewBag.CustomerName = dr["Consignee"];
                    ViewBag.CustomerAddress = dr["Con_IGMAddress"];
                    ViewBag.BLNo = dr["BLNo"];
                    ViewBag.BLDate = dr["BLDate"];
                    ViewBag.VesselName = dr["VesselName"];
                    ViewBag.VoyageName = dr["VoyageName"];
                    ViewBag.ShippingLine = dr["ShippingLine"];
                    ViewBag.Commodity = dr["Commodity"];
                    ViewBag.Description = dr["Description"];
                    ViewBag.AuctionID = dr["AuctionID"];
                    ViewBag.AuctionDate = dr["AuctionDate"];
                }
            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();

            return PartialView();

        }
        [HttpPost]
        public ActionResult SecondAuctionNoticePrint(string IGMNo, string ItemNo,string AuctionId)
        {
            DataSet getJobOrderSet = new DataSet();
            DataTable tblComDetails = new DataTable();
            DataTable tblDocumentDetails = new DataTable();
            DataTable tblContainerDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetAuctionNoticePrint '" + IGMNo + "','" + ItemNo + "','" + AuctionId + "'");

            if (getJobOrderSet.Tables.Count > 0)
            {
                tblComDetails = getJobOrderSet.Tables[0];
                tblDocumentDetails = getJobOrderSet.Tables[1];
                tblContainerDetails = getJobOrderSet.Tables[2];

                foreach (DataRow dr in tblComDetails.Rows)
                {
                    ViewBag.CompanyName = dr["con_Name"];
                    ViewBag.CompanyAddress = dr["AddressI"];
                    ViewBag.CompanyAddress2 = "";
                    ViewBag.PANNo = dr["PANNo"];
                    ViewBag.GSTINNo = dr["GSTIN"];
                    ViewBag.SubjectRealisation = dr["Subject_Realisation"];
                    ViewBag.ReceiptNote = dr["Receipt_Note"];
                    ViewBag.RegdOffice = dr["RegdOffice"];
                    ViewBag.CorporateOffice = dr["CorporateOffice"];
                }
                foreach (DataRow dr in tblDocumentDetails.Rows)
                {
                    ViewBag.CustomerName = dr["Consignee"];
                    ViewBag.CustomerAddress = dr["Con_IGMAddress"];
                    ViewBag.BLNo = dr["BLNo"];
                    ViewBag.BLDate = dr["BLDate"];
                    ViewBag.VesselName = dr["VesselName"];
                    ViewBag.VoyageName = dr["VoyageName"];
                    ViewBag.ShippingLine = dr["ShippingLine"];
                    ViewBag.Commodity = dr["Commodity"];
                    ViewBag.Description = dr["Description"];
                    ViewBag.AuctionID = dr["AuctionID"];
                    ViewBag.AuctionDate = dr["AuctionDate"];
                }
            }

            //ViewBag.CompanyName = "M/S NAVKAR CORPORATION LTD (NCL3)";
            //ViewBag.CompanyAddress = "Survey No 97, Somathane Village,Kon-Savla";
            //ViewBag.CompanyAddress2 = "Road, Taluka-Panvel, Dist Raigad";
            ViewBag.ContainerDetailsList = tblContainerDetails.AsEnumerable();

            return PartialView();

        }

        public JsonResult getAutoRemarksList(string prefixText, string CustomerType)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<AuctionRemarks> auctionRemarks = new List<AuctionRemarks>();
            dataTable = AuctionBl.AddRemarks(prefixText);
            if(dataTable != null)
            {
                foreach(DataRow row in dataTable.Rows)
                {
                    AuctionRemarks auctions = new AuctionRemarks();
                    auctions.RemarkId = Convert.ToInt64(row["RemarkId"]);
                    auctions.Remarks = Convert.ToString(row["Remarks"]);

                    auctionRemarks.Add(auctions);
                }
            }
            return Json(auctionRemarks, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult NOCLetter(string IGMNo, string ItemNo,string AuctionId)
        {
            string ContainerNo = "";
            string NoticeDate = "";
            long Size20Count = 0;
            long Size40Count = 0;
            long Size45Count = 0;
            string Size20 = "";
            string Size40 = "";
            string Size45 = "";
            string GateInDate = "";
            string IGMDate = "";
            DataSet getJobOrderSet = new DataSet();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblDocumentDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetAuctionNoticePrint '" + IGMNo + "','" + ItemNo + "','" + AuctionId + "'");
            tblDocumentDetails = getJobOrderSet.Tables[1];
            tblContainerDetails = getJobOrderSet.Tables[2];

            if (tblDocumentDetails != null)
            {
                foreach (DataRow row in tblDocumentDetails.Rows)
                {
                    NoticeDate = DateTime.Now.ToString("dd/MM/yyyy");
                    IGMDate = row["BLDate"].ToString();

                }
            }

            if (tblContainerDetails != null)
            {
                foreach(DataRow row in tblContainerDetails.Rows)
                {
                    GateInDate = row["ArrivalDate"].ToString();
                    if (ContainerNo == "")
                    {
                        ContainerNo = row["ContainerNo"].ToString();
                    }
                    else
                    {
                        ContainerNo = ContainerNo + "," + row["ContainerNo"].ToString();
                    }

                    if(row["ContainerNo"].ToString() != "" && row["SizeType"].ToString().Substring(0,2)=="20")
                    {
                        Size20Count += 1;
                    }
                    else if (row["ContainerNo"].ToString() != "" && row["SizeType"].ToString().Substring(0, 2) == "40")
                    {
                        Size40Count += 1;
                    }
                    else if (row["ContainerNo"].ToString() != "" && row["SizeType"].ToString().Substring(0, 2) == "45")
                    {
                        Size45Count += 1;
                    }

                }
            }

            ViewBag.Size20 = "";
            ViewBag.Size40 = "";
            ViewBag.Size45 = "";

            if (Size20Count > 0)
            {
                Size20 =Convert.ToString(Size20Count) + "X20";
                ViewBag.Size20 = Size20;
            }
            if (Size40Count > 0)
            {
                Size40 = Convert.ToString(Size40Count) + "X40";
                ViewBag.Size40 = Size40;
            }
            if (Size45Count > 0)
            {
                Size45 = Convert.ToString(Size45Count) + "X45";
                ViewBag.Size45 = Size45;
            }

            //ViewBag.GateInDate = GateInDate;
            ViewBag.AuctionDate = NoticeDate;
            ViewBag.ContainerNo = ContainerNo;
            ViewBag.IGMNo = IGMNo;
            ViewBag.ItemNo = ItemNo;

            return PartialView();
        }

        [HttpPost]
        public ActionResult NOCHoldLetter(string IGMNo, string ItemNo,string AuctionId)
        {
            string ContainerNo = "";
            string NoticeDate = "";
            long Size20Count = 0;
            long Size40Count = 0;
            long Size45Count = 0;
            string Size20 = "";
            string Size40 = "";
            string Size45 = "";
            string GateInDate = "";
            string IGMDate = "";
            DataSet getJobOrderSet = new DataSet();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblDocumentDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetAuctionNoticePrint '" + IGMNo + "','" + ItemNo + "','" + AuctionId + "'");
            tblDocumentDetails = getJobOrderSet.Tables[1];
            tblContainerDetails = getJobOrderSet.Tables[2];

            if (tblDocumentDetails != null)
            {
                foreach (DataRow row in tblDocumentDetails.Rows)
                {
                    NoticeDate = row["AuctionDate"].ToString();
                    IGMDate = row["BLDate"].ToString();

                }
            }

            if (tblContainerDetails != null)
            {
                foreach (DataRow row in tblContainerDetails.Rows)
                {
                    GateInDate = row["ArrivalDate"].ToString();
                    if (ContainerNo == "")
                    {
                        ContainerNo = row["ContainerNo"].ToString();
                    }
                    else
                    {
                        ContainerNo = ContainerNo + "," + row["ContainerNo"].ToString();
                    }

                    if (row["ContainerNo"].ToString() != "" && row["SizeType"].ToString().Substring(0, 2) == "20")
                    {
                        Size20Count += 1;
                    }
                    else if (row["ContainerNo"].ToString() != "" && row["SizeType"].ToString().Substring(0, 2) == "40")
                    {
                        Size40Count += 1;
                    }
                    else if (row["ContainerNo"].ToString() != "" && row["SizeType"].ToString().Substring(0, 2) == "45")
                    {
                        Size45Count += 1;
                    }

                }
            }

            ViewBag.Size20 = "";
            ViewBag.Size40 = "";
            ViewBag.Size45 = "";

            if (Size20Count > 0)
            {
                Size20 = Convert.ToString(Size20Count) + "X20";
                ViewBag.Size20 = Size20;
            }
            if (Size40Count > 0)
            {
                Size40 = Convert.ToString(Size40Count) + "X40";
                ViewBag.Size40 = Size40;
            }
            if (Size45Count > 0)
            {
                Size45 = Convert.ToString(Size45Count) + "X45";
                ViewBag.Size45 = Size45;
            }

            ViewBag.IGMDate = IGMDate;
            ViewBag.GateInDate = GateInDate;
            ViewBag.AuctionDate = NoticeDate;
            ViewBag.ContainerNo = ContainerNo;
            ViewBag.IGMNo = IGMNo;
            ViewBag.ItemNo = ItemNo;

            return PartialView();
        }
        [HttpPost]
        public ActionResult AnnextureCustom(string IGMNo, string ItemNo,string AuctionId)
        {
            
            DataSet getJobOrderSet = new DataSet();
            DataTable tblContainerDetails = new DataTable();
            DataTable tblDocumentDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("USP_GetAuctionAnnextureDet '" + IGMNo + "','" + ItemNo + "','" + AuctionId + "'");
            tblContainerDetails = getJobOrderSet.Tables[0];
            tblDocumentDetails = getJobOrderSet.Tables[1];
            ViewBag.ContainerDetails = tblContainerDetails.AsEnumerable();
            
            if(tblDocumentDetails != null)
            {
                foreach(DataRow row in tblDocumentDetails.Rows)
                {
                    ViewBag.PendingSince = row["PendingSince"];
                    ViewBag.Package = row["Package"];
                    ViewBag.Weight = row["Weight"];
                    ViewBag.GrossWt = row["GrossWt"];
                    ViewBag.GoodDesc = row["GoodDesc"];
                    ViewBag.GoodDesc2 = row["GoodDesc2"];
                }
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult AuctionIGMPrint(string IGMNo, string ItemNo, string AuctionId)
        {

            DataSet getJobOrderSet = new DataSet();
            DataTable tblContainerDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getJobOrderSet = db.sub_GetDataSets("GetIGMItemPrintAuction '" + IGMNo + "','" + ItemNo + "','" + AuctionId + "'");
            tblContainerDetails = getJobOrderSet.Tables[0];

            if (tblContainerDetails != null)
            {
                foreach (DataRow row in tblContainerDetails.Rows)
                {
                    ViewBag.CustomerName = row["SLName"];
                    ViewBag.CustomerAddress = row["SLAddressI"];
                    ViewBag.NameofShip = row["VesselName"];
                    ViewBag.Voyage = row["Voyage"];
                    ViewBag.PortName = row["PortName"];
                    ViewBag.Nationalityofship = row["Nationalityofship"];
                    ViewBag.Nameofthemaster = row["Nameofthemaster"];
                    ViewBag.PortofLoading = row["IGM_Origin"];
                    ViewBag.ItemNo = row["ItemNo"];
                    ViewBag.BillofLadingNo = row["IGM_BLNo"];
                    ViewBag.NoandKindofPackage = row["IGM_Qty"];
                    ViewBag.MarksAndNo = row["IGM_MarksNos"];
                    ViewBag.GrossWeight = row["IGM_GrossWt"];
                    ViewBag.DescofGoodes = row["IGM_GoodsDesc"];
                    ViewBag.NameofConsignee = row["Consignee"];
                    ViewBag.DateofPresentation = row["AuctionDate"];
                    ViewBag.NameOfCHA = row["CHAName"];
                }
            }

            return PartialView();
        }

        public ActionResult getAuctionInvoicePendingList(string IGMNo, string ItemNo)
        {
           
            DataTable getAuctionGenList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getAuctionGenList = db.sub_GetDatatable("GetAuctionPendingInvList '" + IGMNo + "','" + ItemNo + "'");

            var json = "";

            if (getAuctionGenList.Rows.Count > 0)
            {
                json = JsonConvert.SerializeObject(getAuctionGenList);
            }
            else { json = "0"; }

            var returnField = new { DocumentList = json };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult RequestAuctionInvoice(string IgmNo,string ItemNo)
        {
            HC.DBOperations db = new HC.DBOperations();
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            string Message = "";
            int i = 0;
            i = db.sub_ExecuteNonQuery("USPAuctionInvoiceRequest '" + IgmNo + "','" + ItemNo + "','" + userId + "'");

            if (i > 0)
            {
                Message = "Auction Invoice Request Generated Successfully.";
            }
            else
            {
                Message = "Auction Invoice Request Not Generated Successfully.";
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAuctionSummary(string FromDate, string ToDate)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("GetAuctionPendingInvListSummary '" + FromDate + "','" + ToDate + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult CheckHoldNew(string IGMNo, string ItemNo)
        {
            DataTable dt = new DataTable();
            string message = "";
            string InProcess = "";
            HC.DBOperations db = new HC.DBOperations();
            List<BE.Customer> Customerlst = new List<BE.Customer>();
            dt = db.sub_GetDatatable("USP_HOLD_STATUS_IMPORT '" + IGMNo + "','" + ItemNo + "','" + InProcess + "'");
             if (dt.Rows.Count >0)
             {
                if (Convert.ToString(dt.Rows[0]["msga"]) != "")
                {
                    message = Convert.ToString(dt.Rows[0]["msga"]);
                }
                else
                {
                    message = "";

                }
            }
           

            return Json(message);
        }
    }
    public class AuctionRemarks
    {
        public long RemarkId { get; set; }
        public string Remarks { get; set; }
    }
}

