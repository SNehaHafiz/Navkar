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

namespace TrackerMVC.Controllers.BL
{
    [UserAuthenticationFilter] 
    public class BLEntryController : Controller
    {
        BM.IGM.IGM IG = new BM.IGM.IGM();
        BM.BLDataManager.GenerateBLEntry BL = new BM.BLDataManager.GenerateBLEntry();

        // GET: BLEntry
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerateBL()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            if (ModelState.IsValid)
            {
                List<BE.Shippers> Shippers = new List<BE.Shippers>();
                List<BE.ShipLines> ShipLines = new List<BE.ShipLines>();
                List<BE.Customer> Customer = new List<BE.Customer>();
                List<BE.Consignee> Consignee = new List<BE.Consignee>();
                List<BE.CHA> CHA = new List<BE.CHA>();
                List<BE.Vessel> Vessel = new List<BE.Vessel>();
                List<BE.Ports> Ports = new List<BE.Ports>();
                List<BE.Haulage> Haulage = new List<BE.Haulage>();
                List<BE.IGMFileStatus> IGMFileStatus = new List<BE.IGMFileStatus>();
                List<BE.Transport> Transport = new List<BE.Transport>();
                List<BE.POL> POL = new List<BE.POL>();
                List<BE.ContainerSize> ContainerSize = new List<BE.ContainerSize>();
                List<BE.ISOCodes> ISOCodes = new List<BE.ISOCodes>();
                List<BE.CargoType> CargoType = new List<BE.CargoType>();
                List<BE.CommodityGroup> CommodityGroup = new List<BE.CommodityGroup>();
                List<BE.SalesPersonM> SalesPersonM = new List<BE.SalesPersonM>();
                List<BE.KAMM> KAMM = new List<BE.KAMM>();
                List<BE.ImportJOType > JoType = new List<BE.ImportJOType>();
                List<BE.IGMFiLeType> IgmFileType = new List<BE.IGMFiLeType>();

                //if (JOMaster.ViaNo != null)
                //{


                //    // return RedirectToAction("PrintVisitorDetails", new { id = 5 });
                //}
                Shippers  = BL.getShippers();
                ShipLines = BL.getShipLines();
                Customer = BL.getCustomer();
                Consignee = BL.getConsignee();
                CHA = BL.getCHA();
                Vessel = BL.getVessel();
                Ports = BL.getPorts();
                Haulage = BL.getHaulage();
                IGMFileStatus = BL.getIGMFileStatus();
                Transport = BL.getTransport();
                POL = BL.getPOL();
                ContainerSize = BL.getContainerSize();
                ISOCodes = BL.getISOCodes();
                CargoType = BL.getCargoType();
                CommodityGroup = BL.getCommodityGroup();
                SalesPersonM = BL.getSalesPersonM();
                KAMM = BL.getKAMM();
                JoType = BL.getJOType();
                IgmFileType = BL.getIGMFileType();

                BL.DeleteDataFromTempTable(Userid);
               // ViewBag.Shippers(Shippers);
               // ViewBag.ShipLines(ShipLines);
                ViewBag.Shippers = new SelectList(Shippers, "shipperID", "shippername");
                ViewBag.ShipLines = new SelectList(ShipLines, "SLID", "SLName");
                ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
                ViewBag.Consignee = new SelectList(Consignee, "ImporterID", "ImporterName");
                ViewBag.CHA = new SelectList(CHA, "CHANo", "CHAName");
                ViewBag.Vessel = new SelectList(Vessel, "VesselID", "VesselName");
                ViewBag.Ports = new SelectList(Ports, "PortID", "PortName");
                ViewBag.Haulage = new SelectList(Haulage, "Haulage_Type_ID", "Haulage_Type");
                ViewBag.IGMFileStatus = new SelectList(IGMFileStatus, "File_Status_ID", "File_Status");
                ViewBag.Transport = new SelectList(Transport, "Transport_Type_ID", "Transport_Type");
                ViewBag.POL = new SelectList(POL, "PODID", "PODName");
                ViewBag.ContainerSize = new SelectList(ContainerSize, "ContainerSizeID", "ContainerSizeName");
                ViewBag.ISOCodes = new SelectList(ISOCodes, "ISOID", "ISOCode");
                ViewBag.CargoType = new SelectList(CargoType, "Cargotypeid", "Cargotype");
                ViewBag.CommodityGroup = new SelectList(CommodityGroup, "Commodity_Group_ID", "Commodity_Group_Name");
                ViewBag.SalesPersonM = new SelectList(SalesPersonM, "SalesPerson_ID1", "SalesPerson_Name");
                ViewBag.KAM = new SelectList(KAMM, "KAMID", "KAM");
                ViewBag.JoTypes = new SelectList(JoType, "JotypeId", "Jotype");
                ViewBag.IgmFileType = new SelectList(IgmFileType, "FileTypeId", "FileTypeName");



            }
            ModelState.Clear();

            return View();     
        }

        [HttpPost]
        public JsonResult SaveBLForm(BE.JobOrderMEntities viewModel, List<BE.JobOrderDEntities>  containerData)
        {
            string message = "";
            int IgmId = Convert.ToInt32(Session["IgmFileId1"]);
            int userId = Convert.ToInt32(Session["Tracker_userID"]);
            if(IgmId<=0 && viewModel.JONo<=0)
            {
                message = "Data not properly inserted. Plz Try Again";
            }
            else
            {
                message = BL.AddJobOrderData(viewModel, containerData, userId, IgmId);
            }
           // ViewBag.Message = message;

            return Json(message);
        }

        public ActionResult BLEntrySummary()
        {
            

            List<BE.Shippers> Shippers = new List<BE.Shippers>();
            List<BE.ShipLines> ShipLines = new List<BE.ShipLines>();
            List<BE.Customer> Customer = new List<BE.Customer>();
            List<BE.Consignee> Consignee = new List<BE.Consignee>();
            List<BE.CHA> CHA = new List<BE.CHA>();
            List<BE.Vessel> Vessel = new List<BE.Vessel>();
            List<BE.Ports> Ports = new List<BE.Ports>();
            List<BE.Haulage> Haulage = new List<BE.Haulage>();
            List<BE.IGMFileStatus> IGMFileStatus = new List<BE.IGMFileStatus>();
            List<BE.Transport> Transport = new List<BE.Transport>();
            List<BE.POL> POL = new List<BE.POL>();
            List<BE.ContainerSize> ContainerSize = new List<BE.ContainerSize>();
            List<BE.ISOCodes> ISOCodes = new List<BE.ISOCodes>();
            List<BE.CargoType> CargoType = new List<BE.CargoType>();
            List<BE.CommodityGroup> CommodityGroup = new List<BE.CommodityGroup>();
            List<BE.SalesPersonM> SalesPersonM = new List<BE.SalesPersonM>();
            List<BE.KAMM> KAMM = new List<BE.KAMM>();
            List<BE.ImportJOType> JoType = new List<BE.ImportJOType>();
            List<BE.IGMFiLeType> IgmFileType = new List<BE.IGMFiLeType>();


            //if (JOMaster.ViaNo != null)
            //{


            //    // return RedirectToAction("PrintVisitorDetails", new { id = 5 });
            //}
            Shippers = BL.getShippers();
            ShipLines = BL.getShipLines();
            Customer = BL.getCustomer();
            Consignee = BL.getConsignee();
            CHA = BL.getCHA();
            Vessel = BL.getVessel();
            Ports = BL.getPorts();
            Haulage = BL.getHaulage();
            IGMFileStatus = BL.getIGMFileStatus();
            Transport = BL.getTransport();
            POL = BL.getPOL();
            ContainerSize = BL.getContainerSize();
            ISOCodes = BL.getISOCodes();
            CargoType = BL.getCargoType();
            CommodityGroup = BL.getCommodityGroup();
            SalesPersonM = BL.getSalesPersonM();
            KAMM = BL.getKAMM();
            JoType = BL.getJOType();
            IgmFileType = BL.getIGMFileType();

            // ViewBag.Shippers(Shippers);
            // ViewBag.ShipLines(ShipLines);
            ViewBag.Shippers = new SelectList(Shippers, "shipperID", "shippername");
            ViewBag.ShipLines = new SelectList(ShipLines, "SLID", "SLName");
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");
            ViewBag.Consignee = new SelectList(Consignee, "ImporterID", "ImporterName");
            ViewBag.CHA = new SelectList(CHA, "CHANo", "CHAName");
            ViewBag.Vessel = new SelectList(Vessel, "VesselID", "VesselName");
            ViewBag.Ports = new SelectList(Ports, "PortID", "PortName");
            ViewBag.Haulage = new SelectList(Haulage, "Haulage_Type_ID", "Haulage_Type");
            ViewBag.IGMFileStatus = new SelectList(IGMFileStatus, "File_Status_ID", "File_Status");
            ViewBag.Transport = new SelectList(Transport, "Transport_Type_ID", "Transport_Type");
            ViewBag.POL = new SelectList(POL, "PODID", "PODName");
            ViewBag.ContainerSize = new SelectList(ContainerSize, "ContainerSizeID", "ContainerSizeName");
            ViewBag.ISOCodes = new SelectList(ISOCodes, "ISOID", "ISOCode");
            ViewBag.CargoType = new SelectList(CargoType, "Cargotypeid", "Cargotype");
            ViewBag.CommodityGroup = new SelectList(CommodityGroup, "Commodity_Group_ID", "Commodity_Group_Name");
            ViewBag.SalesPersonM = new SelectList(SalesPersonM, "SalesPerson_ID1", "SalesPerson_Name");
            ViewBag.KAM = new SelectList(KAMM, "KAMID", "KAM");
            ViewBag.JoTypes = new SelectList(JoType, "JotypeId", "Jotype");
            ViewBag.IgmFileType = new SelectList(IgmFileType, "FileTypeId", "FileTypeName");

            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, (now.Day)).ToString("dd MMM yyyy HH:mm"); ;
            ViewBag.FromDate = startDate;


            return View();
        }

        public JsonResult getJobOrderList(DateTime FromDate, DateTime ToDate, string SearchCriteria, string SearchText, Boolean IsDate)
        {
            
             int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.JobOrderMEntities> JOLIst = new List<BE.JobOrderMEntities>();
            JOLIst = BL.GetJOListForSummary(FromDate, ToDate, SearchCriteria, SearchText, Userid, IsDate);
            return Json(JOLIst);
            //var jsonResult = Json(JOLIst, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return Json(jsonResult);

        }
       
        public JsonResult getVesselDetails(string viaNo)
        {
            BE.JobOrderMEntities JE = new BE.JobOrderMEntities();
            JE = BL.getVesselDetails(viaNo);
            return Json(JE);
        }

        public JsonResult ContainerNoValidation(string ContainerNo)
        {
            Boolean IsContainerNo;
            IsContainerNo = BL.ContainerNoValidation(ContainerNo);
            return Json(IsContainerNo);
        }

        [HttpPost]
        public ActionResult CancelBLJO(long jono)
        {
            //string message = "";
            //int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            //message = BL.CancelJO(jono, Userid);
            //return Json(message);

            List<BE.ReasonMasterEntities> ReasonList = new List<BE.ReasonMasterEntities>();
            ReasonList = BL.GetReason();
            ViewBag.JONo = jono;
            ViewBag.ReasonDropdownList = new SelectList(ReasonList, "ReasonsId", "Reasons"); ;
            return PartialView();
        }
        public JsonResult CancelJOWithReason(long jono, int ReasonId)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.CancelJO(jono, Userid, ReasonId);
            return Json(message);

            
        }
             [HttpPost]
        public JsonResult SubmitJO(long jono)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = BL.SubmitJO(jono, Userid);
            return Json(message);
        }
         [HttpPost]
             public JsonResult ViewJODetails(long jono)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.JoOrderData JOViewDetails = new BE.JoOrderData();
            JOViewDetails = BL.getJOViewData(jono, Userid);


            return Json(JOViewDetails);
        }

         [HttpPost] 
         public JsonResult EditJODetails(long jono)
         {
             int Userid = Convert.ToInt32(Session["Tracker_userID"]);
             BE.JoOrderData JOEditDetails = new BE.JoOrderData();
             JOEditDetails = BL.getJOViewData(jono, Userid);


             return Json(JOEditDetails);
         }

        [HttpPost]
        public ActionResult AjaxGetJobDetailsModify(string ContainerNo, string JoNo)
        {
            DataTable dt = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("GETJOBDETAILSCONTNO_Import '" + ContainerNo + "'," + JoNo );
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
         public JsonResult UploadFiles(BE.JobOrderMEntities fileData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.JobOrderContainerEntities ContainerList = new BE.JobOrderContainerEntities();

            List<BE.ContainerSize> ContainerSize = new List<BE.ContainerSize>();
            List<BE.ISOCodes> ISOCodes = new List<BE.ISOCodes>();
            List<BE.CargoType> CargoType = new List<BE.CargoType>();
            List<BE.CommodityGroup> CommodityGroup = new List<BE.CommodityGroup>();
            ContainerSize = BL.getContainerSize();
            ISOCodes = BL.getISOCodes();
            CargoType = BL.getCargoType();
            CommodityGroup = BL.getCommodityGroup();
            // Checking no of files injected in Request object  
      
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

                                    DataTable ContainerDT = new DataTable();

                                    ContainerDT.Columns.Add("ContainerNo");
                                    ContainerDT.Columns.Add("ContainerSize");
                                    ContainerDT.Columns.Add("FL");
                                    ContainerDT.Columns.Add("ISOCode");
                                    ContainerDT.Columns.Add("Cargotypeid");
                                    ContainerDT.Columns.Add("Commodity_group_id");
                                    ContainerDT.Columns.Add("User_id");


                                    int j = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        bool alreadyExistsContainerSize = ContainerSize.Any(x => x.ContainerSizeName.ToUpper() == row["Size"].ToString().ToUpper());
                                        bool alreadyExistsCommodity = CommodityGroup.Any(x => x.Commodity_Group_Name.ToUpper() == row["Commodity"].ToString().ToUpper());
                                        bool alreadyExistsCargo = CargoType.Any(x => x.Cargotype.ToUpper() == row["Cargo Type"].ToString().ToUpper());
                                        bool alreadyExistsISO = ISOCodes.Any(x => x.ISOCode.ToUpper() == row["ISO"].ToString().ToUpper());
                                        var FCL = row["FCL/LCL"].ToString().ToUpper();
                                        
                                        int FCLId = 0;
                                        if (FCL == "FCL")
                                        {
                                            FCLId = 1;
                                        }
                                        else if (FCL == "LCL")
                                        {
                                            FCLId = 2;
                                        }
                                        else{
                                            var AlertValue = new { ColumnData = "FCL/LCL", ValueData = row["FCL/LCL"].ToString() };
                                   
                                        var returnField = new { Issuedata = AlertValue, ContainerData = ContainerList };

                                        return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                           // return Json(1);
                                        }

                                        if (!alreadyExistsContainerSize )
                                        {
                                          
                                            var AlertValue = new { ColumnData = "Size", ValueData = row["Size"].ToString() };

                                            var returnField = new { Issuedata = AlertValue, ContainerData = ContainerList };

                                            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                        }
                                       
                                        else if (!alreadyExistsCommodity)
                                        {
                                           
                                           var AlertValue = new { ColumnData = "Commodity", ValueData = row["Commodity"].ToString() };

                                           var returnField = new { Issuedata = AlertValue, ContainerData = ContainerList };

                                           return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                        }
                                        else if (!alreadyExistsISO)
                                        {
                                           // var IssueDesc = row["ISO"].ToString();
                                            
                                            var AlertValue = new { ColumnData = "ISO", ValueData = row["ISO"].ToString() };

                                            var returnField = new { Issuedata = AlertValue, ContainerData = ContainerList };

                                            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                        }
                                        

                                        if (!alreadyExistsContainerSize || !alreadyExistsCommodity || !alreadyExistsCommodity || !alreadyExistsISO)
                                        {
                                            return Json(1);
                                        }
                                       
                                        if (alreadyExistsContainerSize || alreadyExistsCommodity || alreadyExistsCommodity || alreadyExistsISO)
                                        {
                                            var ContainerNo = row["Container No"].ToString();
                                            
                                            var size = ContainerSize.Find(x => x.ContainerSizeName == row["Size"].ToString());
                                            int id = Convert.ToInt32(size.ContainerSizeID);
                                            String SizeInString = Convert.ToString(size.ContainerSizeName);

                                            var CommodityGroupName = CommodityGroup.Find(x => x.Commodity_Group_Name.ToUpper() == row["Commodity"].ToString().ToUpper());
                                            int CommodityGroupid = Convert.ToInt32(CommodityGroupName.Commodity_Group_ID);
                                            String CommodityGroupString = Convert.ToString(CommodityGroupName.Commodity_Group_Name);

                                            var ISOCode = ISOCodes.Find(x => x.ISOCode.ToUpper() == row["ISO"].ToString().ToUpper());
                                            int ISOCodeID = Convert.ToInt32(ISOCode.ISOID);
                                            String ISOCodeString = Convert.ToString(ISOCode.ISOCode);

                                            var CargoTypeName = CargoType.Find(x => x.Cargotype.ToUpper() == row["Cargo Type"].ToString().ToUpper());
                                            int CargoTypeID = Convert.ToInt32(CargoTypeName.Cargotypeid);
                                            String CargoTypeString = Convert.ToString(CargoTypeName.Cargotype);

                                            DataRow dar = ContainerDT.NewRow();
                                            ////dar["ContainerNo"] = ContainerNo;

                                            ////dar["Container"] = ContainerNo + "<input Name=ContainerNo type=hidden id=ContainerNo   value=" + ContainerNo.ToString() + ">";
                                            ////dar["ContainerSize"] = SizeInString.ToString() + "<input Name=Size type=hidden id=Size   value=" + id.ToString() + ">";
                                            ////dar["FCL"] = FCL.ToString() + "<input Name=FLid type=hidden id=FL   value=" + FCLId.ToString() + ">";
                                            ////dar["ISO"] = ISOCodeString.ToString() + "<input Name=ISOCodeID type=hidden id=ISOCode   value=" + ISOCodeID + ">";
                                            ////dar["CargoType"] = CargoTypeString.ToString() + "<input Name=Cargotypeid type=hidden id=Cargotypeid   value=" + CargoTypeID + ">";
                                            ////dar["Commodity"] = CommodityGroupString.ToString() + "<input Name=Commodity_group_id type=hidden id=Commodity_group_id   value=" + CommodityGroupid + ">";
                                            dar["ContainerNo"] = ContainerNo.ToUpper();
                                            dar["ContainerSize"] = SizeInString;
                                            dar["FL"] = FCL;
                                            dar["ISOCode"] = ISOCodeID;
                                            dar["Cargotypeid"] = CargoTypeID;
                                            dar["Commodity_group_id"] = CommodityGroupid;
                                            dar["User_id"] = Userid;

                                            ContainerDT.Rows.Add(dar);
                                        }                                       
                                           // string str = "";

                                        //customers.Add(new CustomerModel
                                        //{
                                        //    CustomerId = Convert.ToInt32(row["Id"]),
                                        //    Name = row["Name"].ToString(),
                                        //    Country = row["Country"].ToString()
                                        //});
                                        j++;
                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }
                                   
                                    if (ContainerDT != null)
                                    {
                                        //foreach (DataRow row in ContainerDT.Rows)
                                        //{
                                        //    BE.JobOrderDEntities JODList = new BE.JobOrderDEntities();
                                        //    JODList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                                        //    JODList.ContainerSize = Convert.ToString(row["ContainerSize"]);
                                        //    JODList.FLName = Convert.ToString(row["FCL"]);
                                        //    JODList.Commodity_Group_Name = Convert.ToString(row["CommodityID"]);
                                        //    JODList.Cargotype = Convert.ToString(row["CargoTypeID"]);
                                        //    JODList.ISOCodeName = Convert.ToString(row["ISO"]);
                                        //    JODList.Container = Convert.ToString(row["Container"]);

                                            
                                        //    ContainerList.Add(JODList);
                                        //}
                                        //return Json(ContainerList);
                                        long jono = fileData.JONo;
                                        ContainerList = BL.AddImportDataIntoTable(ContainerDT, Userid, jono);

                                        var returnField = new { Issuedata = 1, ContainerData = ContainerList };

                                        return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                       // return Json(ContainerList);
                                    }

                                }
                                
                            }
                        }
                    }

                    //if (fname != null || fname != string.Empty)
                    //{
                    //    if ((System.IO.File.Exists(fname)))
                    //    {
                    //        System.IO.File.Delete(fname);
                    //    }

                    //}
                    // Returns message that successfully uploaded  
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

        [HttpPost]
        public JsonResult AddTempContainerData( BE.JobOrderDEntities jd)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.JobOrderContainerEntities JDContainerList = new BE.JobOrderContainerEntities();
            JDContainerList = BL.AddContainerData(jd, Userid);
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            // dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = converter.ToDataTable(JDContainerList.Containerlist);
            if (dt != null) {
                foreach (DataRow row in dt.Rows)
                {
                    //Session["IgmFileId1"] = 0;
                    Session["IgmFileId1"] = Convert.ToString(row["TempfLID"]);
                    break;

                }
            }

                return Json(JDContainerList);
        }

        
            [HttpPost]
        public JsonResult DeleteContainerData(string ContainerId)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.JobOrderContainerEntities JDContainerList = new BE.JobOrderContainerEntities();
            JDContainerList = BL.DeleteContainerData(ContainerId, Userid);
            return Json(JDContainerList);
        }

            public JsonResult CheckDuplicateContainer()
            {
                int Userid = Convert.ToInt32(Session["Tracker_userID"]);
                Boolean result = BL.CheckContainerData(Userid);
                return Json(result);
            }
            public JsonResult DeleteTempAllRecords()
            {
                int Userid = Convert.ToInt32(Session["Tracker_userID"]);
                BL.DeleteDataFromTempTable(Userid);
                return Json("dataDelete");
            }
            [HttpPost]
            public ActionResult GetJOEvent(long jono)
            {
                List<BE.AmmendmentlogEntities> LogEventList = new List<BE.AmmendmentlogEntities>();
                LogEventList = BL.GetLogEvent(jono);
                ViewBag.JONo = jono;
                return PartialView(LogEventList);

            }
            [HttpPost]
            public ActionResult AddAttachment()
            {
                List<BE.JobOrderAttachmentEntities> JOAttachmentList = new List<BE.JobOrderAttachmentEntities>();
                //int Userid = Convert.ToInt32(Session["Tracker_userID"]);
                //JOAttachmentList = BL.GetJOAttachment(Userid);

                return PartialView(JOAttachmentList);

            }
            [HttpPost]
            public JsonResult UploadJOAttachment()
            {
                List<BE.JobOrderAttachmentEntities> JOAttachmentList = new List<BE.JobOrderAttachmentEntities>();
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
                            string  contentType;

                         //   byte[] Bytes = ReadAllBytes(fname);

                            Stream fs = Request.Files[i].InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
 
                            contentType = MimeMapping.GetMimeMapping(fname);
                            JOAttachmentList = BL.AddJOAttachment(Userid, bytes, contentType, fname);
                            
                            // Get the complete folder path and store the file inside it.  
                           // fname = Path.Combine(Server.MapPath("~/ImportFile/"), fname);
                          //  file.SaveAs(fname);
                          //  string extension = Path.GetExtension(fname);
                         //   string conString = string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json("Error occurred. Error details: " + ex.Message);
                    }
                }

               // return Json(JOAttachmentList);
                //var Data = JsonConvert.SerializeObject(JOAttachmentList, Formatting.Indented,
                //            new JsonSerializerSettings
                //            {
                //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //            });
                var jsonResult = Json(JOAttachmentList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
               // return new JsonResult() { Data = JOAttachmentList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            [HttpPost]
            public FileResult DownLoadFile(int id)
            {

                BE.JobOrderAttachmentEntities JOA = new BE.JobOrderAttachmentEntities();
                JOA = BL.GetFile(id);

                //var FileById = (from FC in ObjFiles
                //                where FC.Id.Equals(id)
                //                select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();

                return File(JOA.Data,JOA.ContentType,JOA.DocFileName);

            }

            [HttpPost]
            public JsonResult DeleteFile(string id)
            {
                long AttachmentId = Convert.ToInt64(id);
                int Userid = Convert.ToInt32(Session["Tracker_userID"]);                

                string message = "";
                
                message = BL.DeleteFile(AttachmentId, Userid);
                return Json(message);
            }

         [HttpPost]
            public JsonResult GetAttachment()
            {
                List<BE.JobOrderAttachmentEntities> JOAttachmentList = new List<BE.JobOrderAttachmentEntities>();
                int Userid = Convert.ToInt32(Session["Tracker_userID"]);
                JOAttachmentList = BL.GetJOAttachment(Userid);
                var jsonResult = Json(JOAttachmentList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

         public ActionResult BLSummaryForPODETA()
         {
             List<BE.JobOrderMEntities> JOList = new List<BE.JobOrderMEntities>();
             JOList = BL.getBLList();


             List<BE.IGMFileStatusInfo> IGMFileList = new List<BE.IGMFileStatusInfo>();
             IGMFileList = BL.GetIGMFileStatus();
             //ViewBag.JONo = jono;
             ViewBag.ddlIGMFileStatus = new SelectList(IGMFileList, "File_Status_ID", "File_Status");

             vessel.VesselSummary vesselTrackerProvider = new vessel.VesselSummary();
             List<BE.Vessel> vesselList = new List<BE.Vessel>();
             vesselList = vesselTrackerProvider.GetVesselSummaryList();

             ViewBag.ddlVesselName = new SelectList(vesselList, "VesselID", "VesselName");



             return View(JOList);
         }

         [HttpPost]
         public JsonResult ModifyPODETA(List<BE.JobOrderMEntities> BLPODlist)
         {
             string message = "";

             int userId = Convert.ToInt32(Session["Tracker_userID"]);

             //  message = BL.AddJobOrderData(viewModel, containerData, userId);
             // ViewBag.Message = message;
             DataTable DT = new DataTable();

             DT.Columns.Add("BLNo");
             DT.Columns.Add("PODETADate");
             DT.Columns.Add("userID");

             DT.TableName = "PT_PODETAData";
             foreach (BE.JobOrderMEntities item in BLPODlist)
             {
                 DataRow row = DT.NewRow();
                 row["BLNo"] = item.JONo;
                 row["PODETADate"] = Convert.ToDateTime(item.PODETADate).ToString("yyyy-MM-dd 00:00:00");
                 row["userID"] = userId;

                 DT.Rows.Add(row);
             }

             message = BL.UpdateBLPODETA(DT, userId);

             return Json(message);
         }
         [HttpPost]
         public JsonResult AjaxUpdateBLdetails(int JONO, string PODETADate, int VesselID, int File_Status_Id, string IGMNo)
         {
             int userId = Convert.ToInt32(Session["Tracker_userID"]);
             string msg = "";
             msg = BL.UpdateBLData(JONO, PODETADate, VesselID, File_Status_Id, IGMNo, userId);


             List<BE.JobOrderMEntities> JOList = new List<BE.JobOrderMEntities>();
             JOList = BL.getBLList();
             return Json(JOList);
         }
         // Codes By Prashant
         //mODIFY BY rAHUL

         public ActionResult MovementPendancy()
         {
             //List<BE.MovementPendancyEntities> MovementPendancyList = new List<BE.MovementPendancyEntities>();
             //MovementPendancyList = BL.GetMovementPendancyDetails();
             return View();
         }

        public ActionResult AllPortPendency()
        {
            //List<BE.MovementPendancyEntities> MovementPendancyList = new List<BE.MovementPendancyEntities>();
            //MovementPendancyList = BL.GetMovementPendancyDetails();
            return View();
        }

        //Codes end by Prashant
        //Codes by Rahul
        [HttpPost]
         public JsonResult MovementPendancy1()
         {
             List<BE.MovementPendancyEntities> MovementPendancyList = new List<BE.MovementPendancyEntities>();
             MovementPendancyList = BL.GetMovementPendancyDetails();
             //return Json(MovementPendancyList.ToList());
             var jsonResult = Json(MovementPendancyList, JsonRequestBehavior.AllowGet);
             jsonResult.MaxJsonLength = int.MaxValue;
             return jsonResult;
         }
         public ActionResult PortPendency()
         {
             //List<BE.MovementPendancyEntities> MovementPendancyList = new List<BE.MovementPendancyEntities>();
             //MovementPendancyList = BL.GetMovementPendancyDetails();
             return View();
         }
         [HttpPost]
         public JsonResult PortPendency1()
         {
             //List<BE.PortPendancyEntities> PortPendencyList = new List<BE.PortPendancyEntities>();
             //PortPendencyList = BL.GetPortPendancyDetails();
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_IMPORT_PORT_PENDENCY");
            //DataTable dt = converter.ToDataTable(upcomingList);
            Session["PortPendencyList"] = dt;
            // return View(upcomingList);
            //return Json(MovementPendancyList.ToList());
            var PortPendencyList = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(PortPendencyList, JsonRequestBehavior.AllowGet);
             jsonResult.MaxJsonLength = int.MaxValue;
             return jsonResult;
         }

        public ActionResult HBLSummaryDetails()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PortPendency2()
        {
            //List<BE.PortPendancyEntities> PortPendencyList = new List<BE.PortPendancyEntities>();
            //PortPendencyList = BL.GetPortPendancyDetails();
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_IMPORT_PORT_PENDENCYALL");
            //DataTable dt = converter.ToDataTable(upcomingList);
            Session["PortPendencyList"] = dt;
            CountTeus count = new CountTeus();
            if(dt != null)
            {
                foreach(DataRow row in dt.Rows)
                {
                    if (row["Size"].ToString() == "20")
                    {
                        count.Total20 = Convert.ToInt32(count.Total20) + 1;
                    }
                    else if (row["Size"].ToString() == "40")
                    {
                        count.Total40 = Convert.ToInt32(count.Total40) + 1;
                    }
                    else if (row["Size"].ToString() == "45")
                    {
                        count.Total45 = Convert.ToInt32(count.Total45) + 1;
                    }
                }
            }
            // return View(upcomingList);
            //return Json(MovementPendancyList.ToList());
            var PortPendencyList = JsonConvert.SerializeObject(dt);
            var returnField = new { Issuedata = count, ContainerData = PortPendencyList };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public ActionResult ExportToExcelPortPendency(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable PortPendencyList = (DataTable)Session["PortPendencyList"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = PortPendencyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PortPendencyList.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Port Pendency Report<strong></td></tr>");
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

        [HttpPost]
        public JsonResult UploadIGMFile(BE.JobOrderMEntities fileData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.IGMEntities> IGMList = new List<BE.IGMEntities>();
            BE.JobOrderContainerEntities ContainerList = new BE.JobOrderContainerEntities();
            long IgmFileId = 0;
            List<BE.ContainerSize> ContainerSize = new List<BE.ContainerSize>();
            string message = "";
            if (Request.Files.Count > 0)
            {
                //===Upload Data By Select Type=====================
                if (fileData.FileTypeId == 1)//==For IGM
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
                            string contentType;
                            fname = Path.Combine(Server.MapPath("~/ImportFile/"), fname);
                            file.SaveAs(fname);
                            //   byte[] Bytes = ReadAllBytes(fname);

                            Stream fs = Request.Files[i].InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);

                            contentType = MimeMapping.GetMimeMapping(fname);
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
                                    counter++;
                                    DataRow dar = DT.NewRow();
                                    dar["SeqNo"] = counter;
                                    dar["Line"] = ln;
                                    DT.Rows.Add(dar);

                                }
                                file12.Close();

                                if (DT != null)
                                {
                                    IgmFileId = IG.AddIGMFileDataBL(DT, Userid,fileData.ViaNo,fileData.JONo);
                                    if (IgmFileId <= 0)
                                    {
                                        return new JsonResult() { Data = "Job Order Not Save Successfully. Plz Upload Again !", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                    }
                                    //Session.Add("IgmFileId1", IgmFileId);
                                    Session["IgmFileId1"] = IgmFileId;
                                }
                                
                                DataTable dt = new DataTable();
                                Int64 returnValue = 0;
                                HC.DBOperations db = new HC.DBOperations();
                                dt = db.sub_GetDatatable("GetIgmDownloadErrorLog "+ IgmFileId);
                                if (dt.Rows.Count > 0)
                                {
                                    returnValue = 0;
                                    //Session.Add("IGMError", dt);
                                    Session["IGMError"] = dt;
                                }
                                else
                                {
                                    returnValue = IgmFileId;
                                }
                                
                                return new JsonResult() { Data = returnValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                                //}

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        //return Json("Error occurred. Error details: " + ex.Message);
                    }
                }
                else if (fileData.FileTypeId == 2)//===For CGM
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
                                    IgmFileId = IG.AddCGMFileData(DT, Userid, fileData.ViaNo,fileData.JONo);
                                    if (IgmFileId <= 0)
                                    {
                                        return new JsonResult() { Data = "Job Order Not Save Successfully. Plz Upload Again !", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                    }
                                    //Session.Add("IgmFileId1", IgmFileId);
                                    Session["IgmFileId1"] = IgmFileId;
                                }

                                DataTable dt = new DataTable();
                                Int64 returnValue = 0;
                                HC.DBOperations db = new HC.DBOperations();
                                dt = db.sub_GetDatatable("GetIgmDownloadErrorLog " + IgmFileId);
                                if (dt.Rows.Count > 0)
                                {
                                    returnValue = 0;
                                    //Session.Add("IGMError", dt);
                                    Session["IGMError"] = dt;
                                }
                                else
                                {
                                    returnValue = IgmFileId;
                                }
                                return new JsonResult() { Data = returnValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        //return Json("Error occurred. Error details: " + ex.Message);
                    }
                }
                else if (fileData.FileTypeId==3)//==For Excel
                {
                    List<BE.ISOCodes> ISOCodes = new List<BE.ISOCodes>();
                    List<BE.CargoType> CargoType = new List<BE.CargoType>();
                    List<BE.CommodityGroup> CommodityGroup = new List<BE.CommodityGroup>();
                    ContainerSize = BL.getContainerSize();
                    ISOCodes = BL.getISOCodes();
                    CargoType = BL.getCargoType();
                    CommodityGroup = BL.getCommodityGroup();

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

                                        if (dt != null)
                                        {
                                            IgmFileId = IG.AddIGMFileDataExcel(dt, Userid, fileData.ViaNo);
                                            if (IgmFileId <= 0)
                                            {
                                                return new JsonResult() { Data = "Job Order Not Save Successfully. Plz Upload Again !", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                            }
                                            //Session.Add("IgmFileId1", IgmFileId);
                                            Session["IgmFileId1"] = IgmFileId;
                                        }

                                        
                                        Int64 returnValue = 0;
                                        HC.DBOperations db = new HC.DBOperations();
                                        dt = db.sub_GetDatatable("GetIgmDownloadErrorLog " + IgmFileId);
                                        if (dt.Rows.Count > 0)
                                        {
                                            returnValue = 0;
                                            //Session.Add("IGMError", dt);
                                            Session["IGMError"] = dt;
                                        }
                                        else
                                        {
                                            returnValue = IgmFileId;
                                        }

                                        if (fname != null || fname != string.Empty)
                                        {
                                            if ((System.IO.File.Exists(fname)))
                                            {
                                                System.IO.File.Delete(fname);
                                            }

                                        }

                                        return new JsonResult() { Data = returnValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
                        //  Console.WriteLine("Error occurred. Error details: " + ex.Message);
                        //  BL.AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'",""));
                        // return Json("Error occurred. Error details: " + ex.Message);
                        return Json(1);
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

        public ActionResult GridViewBindDetails(string Category, string fromdate, string todate)
        {
            DataTable GridViewBindDetails = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            GridViewBindDetails = db.sub_GetDatatable("Usp_joborder_GridViewBind");
           
            var json = JsonConvert.SerializeObject(GridViewBindDetails);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public JsonResult GridViewBindDetailsNew(string Igm)
        {
            int igmid = Convert.ToInt32(Igm);
            List<BE.MovementPendancyEntities> MovementPendancyList = new List<BE.MovementPendancyEntities>();
            MovementPendancyList = BL.GridViewBindDetailsNew(igmid);
            //return Json(MovementPendancyList.ToList());
            var jsonResult = Json(MovementPendancyList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ExportToExcelIgmError()
        {
            try
            {
                DataTable getMovementICDNew = (DataTable)Session["IGMError"];

                GridView gridview = new GridView();
                gridview.DataSource = getMovementICDNew;
                gridview.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=IgmDownloadErrorLog.xls");
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        // render the GridView to the HtmlTextWriter
                        gridview.RenderControl(htw);
                        // Output the GridView content saved into StringWriter
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex) { }

            return View();
        }

        [HttpPost]
        public JsonResult getHBLSummaryDetails(string FromDate,string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("getHBLSummaryDetails '" + FromDate + "','" + ToDate + "'");
            Session["HBLSummaryList"] = dt;
            Session["FromDate"] = FromDate;
            Session["ToDate"] = ToDate;
            var HBLSummaryList = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(HBLSummaryList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult HBLSummaryReport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            var Tittle = "From :- " + Session["FromDate"] + " ToDate:-" + Session["ToDate"] + ".";

            DataTable upcomingList1 = (DataTable)Session["HBLSummaryList"];
            GridView gridview = new GridView();
            gridview.DataSource = upcomingList1;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=HBLSummary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>HBL Summary Details<strong></td></tr>");
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

        public ActionResult ExportToFormat()
        {
            string fileName = "~/Format/ManualExlDownloadFormat.xlsx" ;
            if (fileName !="")
            {
                string filePath = fileName;
                Response.ContentType = "doc/docx";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }

            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("ContainerNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("Size", typeof(string)));
            //dt.Columns.Add(new DataColumn("Commodity", typeof(string)));
            //dt.Columns.Add(new DataColumn("Cargo Type", typeof(string)));
            //dt.Columns.Add(new DataColumn("ISOCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("FL", typeof(string)));
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            //dt.Rows.Add("");
            ////dt.Columns.Add(new DataColumn("FL", typeof(string)));
            //GridView gridview = new GridView();
            //gridview.DataSource = dt;
            //gridview.DataBind();

            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment;filename=ExcelUploadFormat.xls");
            //using (StringWriter sw = new StringWriter())
            //{
            //    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            //    {
            //        // render the GridView to the HtmlTextWriter


            //        gridview.RenderControl(htw);
            //        // Output the GridView content saved into StringWriter
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }
            //}

            return View();
        }

        //Codes end by RAHUL
    }

    public class CountTeuss
    {
        public int Total20 { get; set; }
        public int Total40 { get; set; }
        public int Total45 { get; set; }

        public CountTeuss()
        {
            Total20 = 0;
            Total40 = 0;
            Total45 = 0;
        }
    }
}