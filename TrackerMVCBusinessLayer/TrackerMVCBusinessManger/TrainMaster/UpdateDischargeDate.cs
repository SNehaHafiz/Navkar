using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrainMaster
{
    public class UpdateDischargeDate
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();

        public List<EN.Port> GetPortList()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetPortForTrainDeparture();
            List<EN.Port> isCHACode = new List<EN.Port>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.Port oper = new EN.Port();
                    oper.PortID = Convert.ToInt32(row["PortID"]);
                    oper.PortName = Convert.ToString(row["PortName"]);
                    oper.PortCode = "port" + oper.PortID;
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }
        public List<EN.DischargeDateContainerDetails> GetContainerForUpdateDischargeDate(int portID, int userID)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetContainerForUpdateDischargeDate(portID, userID);
            List<EN.DischargeDateContainerDetails> isCHACode = new List<EN.DischargeDateContainerDetails>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.DischargeDateContainerDetails oper = new EN.DischargeDateContainerDetails();
                    oper.JONO = Convert.ToInt32(row["jono"]);
                    oper.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    oper.Size = Convert.ToString(row["Size"]);
                    oper.IsSelected = Convert.ToBoolean(row["IsContainerSelected"]);
                    oper.DischargeDate = Convert.ToString(row["DischargeDate"]);
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }
        public List<string> GetTrainNOForUpdateDischargeDate()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetTrainNOForTrainDeparture();
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string oper = Convert.ToString(row["TrainNo"]);
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }

        public int UpdateSelectedContainer(int joNo, string containerNo, bool isChecked, int userID)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.UpdateSelectedContainerForUpdateDischargeDate(joNo, containerNo, isChecked, userID);
            int totalSelectedCount = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    totalSelectedCount = Convert.ToInt32(row["TotalSelected"]);

                }

            }
            return totalSelectedCount;
        }
        public int GetTotalCountForUpdateDischargeDate(int userID)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetTotalCountForUpdateDischargeDate(userID);
            int totalSelectedCount = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    totalSelectedCount = Convert.ToInt32(row["TotalSelected"]);

                }

            }
            return totalSelectedCount;
        }
        public int UpdateDischargeDateForSelectedContainer(string dischargeDate, string containerNo, int userID)
        {
            int codeDL = 0;
            codeDL = trackerdatamanager.UpdateDischargeDateForSelectedContainer(Convert.ToDateTime(dischargeDate), containerNo, userID);
            return codeDL;
        }
        public int UpdateTrainNoForSelectedContainer(string trainNo, int userID)
        {
            int codeDL = 0;
            codeDL = trackerdatamanager.UpdateTrainNoForSelectedContainerForUpdateDischargeDate(trainNo, userID);
            return codeDL;
        }

        ///////by durga///////

        public List<EN.DischargeDateContainerDetails> GetContainerUpdateDischargeData( string containerNo)
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetContainerUpdateDischargeData(containerNo);
            List<EN.DischargeDateContainerDetails> ContainerDL = new List<EN.DischargeDateContainerDetails>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DischargeDateContainerDetails DL = new EN.DischargeDateContainerDetails();
                    DL.JONO = Convert.ToInt32(row["jono"]);
                    DL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    DL.Size = Convert.ToString(row["Size"]);
                    DL.port = Convert.ToString(row["PortName"]);
                    DL.DischargeDate = Convert.ToString(row["DischargeDate"]);
                    ContainerDL.Add(DL);
                }

            }
            return ContainerDL;
        }

        public String UpdateDischargeDateJO(DataTable DischargeListDT, Int32 userId)
        {
            string message = "";


            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userId);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            string typeTable = "PT_UpdateDischargeDate";
            string typeTableParameter = "@PT_UpdateDischargeDate";

            int i = db.UpdateDischargeDate("USP_UpdateDischargeDateNew", lstparam, DischargeListDT, typeTable, typeTableParameter);
            if (i != 0)
            {
                message = "Records updated successfully";
            }
            else
            {
                message = "Records not update, please try again";
            }
            return message;
        }
        /////End by durga////////

        // Codes By prashant

        public List<EN.LoadedContainerArrival> GetLoadedContainerDetails(string SearchCriteria, string SearchText, DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getLoadedContainerArrival(SearchCriteria, SearchText, FromDate, ToDate);
            List<EN.LoadedContainerArrival> ContainerDL = new List<EN.LoadedContainerArrival>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LoadedContainerArrival DL = new EN.LoadedContainerArrival();
                    DL.SrNo = Convert.ToString(row["Sr No"]);
                    DL.CFSCode = Convert.ToString(row["CFS Code"]);
                    DL.Vessel = Convert.ToString(row["Vessel Name"]);
                    DL.PortName = Convert.ToString(row["Port Name"]);
                    DL.ShippingLine = Convert.ToString(row["CFS Code"]);

                    DL.EIRNo = Convert.ToString(row["EIR No"]);
                    DL.JoType = Convert.ToString(row["Jo Type"]);
                    DL.JONo = Convert.ToString(row["JO No"]);
                    DL.ContainerNo = Convert.ToString(row["Container No"]);
                    DL.Size = Convert.ToString(row["Size"]);
                    DL.CargoType = Convert.ToString(row["Cargo Type"]);
                    DL.Type = Convert.ToString(row["Type"]);
                    DL.EIRDate = Convert.ToString(row["EIR Date"]);
                    DL.InDateandTime = Convert.ToString(row["In Date and Time"]);
                    DL.FL = Convert.ToString(row["FL"]);
                    DL.ScanType = Convert.ToString(row["Scan Type"]);
                    DL.Voyage = Convert.ToString(row["Voyage"]);
                    DL.ViaNo = Convert.ToString(row["Via No"]);
                    DL.IGMNo = Convert.ToString(row["IGM No"]);
                    DL.ItemNo = Convert.ToString(row["Item No"]);
                    DL.BLNo = Convert.ToString(row["BL No"]);
                    DL.Consignee = Convert.ToString(row["Consignee"]);
                    DL.AgentName = Convert.ToString(row["Agent Name"]);
                    DL.ShippingLineName = Convert.ToString(row["Shipping Line Name"]);
                    DL.CHAName = Convert.ToString(row["CHA Name"]);
                    DL.VehicleType = Convert.ToString(row["Vehicle Type"]);
                    DL.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    DL.TransporterName = Convert.ToString(row["Transporter Name"]);
                    DL.SealNoI = Convert.ToString(row["Seal No I"]);
                    DL.SealNoII = Convert.ToString(row["Seal No II"]);
                    DL.CargoWeight = Convert.ToString(row["Cargo Weight"]);
                    DL.CargoDesc = Convert.ToString(row["Cargo Desc"]);
                    DL.TrasportMode = Convert.ToString(row["Trasport Mode"]);
                    DL.TrainNo = Convert.ToString(row["Train No"]);
                    DL.WagonNo = Convert.ToString(row["Wagon No"]);
                    DL.Remarks = Convert.ToString(row["Remarks"]);
                    ContainerDL.Add(DL);
                }

            }
            return ContainerDL;
        }
        public List<EN.IGMDetailsEntities> GetIGMdetails(string IGMnumber,string SearchType)
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getIGMListDetails(IGMnumber, SearchType);
            List<EN.IGMDetailsEntities> ContainerDL = new List<EN.IGMDetailsEntities>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.IGMDetailsEntities DL = new EN.IGMDetailsEntities();
                    DL.SrNo = Convert.ToString(row["Sr. No"]);
                    DL.PortName = Convert.ToString(row["Port Name"]);
                    DL.JoNo = Convert.ToString(row["Jo No"]);
                    DL.JoDate = Convert.ToString(row["Jo Date"]);
                    DL.VesselName = Convert.ToString(row["Vessel Name"]);

                    DL.ViaNo = Convert.ToString(row["Via No"]);
                    //DL.SMTPNo = Convert.ToString(row["SMTP No"]);
                    //DL.SMTPDate = Convert.ToString(row["SMTP Date"]);
                    DL.IGMNo = Convert.ToString(row["IGM No"]);
                    DL.ItemNo = Convert.ToString(row["Item No"]);
                    DL.ContainerNO = Convert.ToString(row["Container NO"]);
                    DL.Size = Convert.ToString(row["Size"]);
                    DL.ISOCode = Convert.ToString(row["ISOCode"]);
                    DL.ContainerType = Convert.ToString(row["Container Type"]);
                    DL.CargoType = Convert.ToString(row["Cargo Type"]);
                    DL.CargoWeight = Convert.ToString(row["Cargo Weight"]);
                    DL.BLNumber = Convert.ToString(row["BL Number"]);
                    DL.BLDate = Convert.ToString(row["BL Date"]);
                    DL.IGMNo = Convert.ToString(row["IGM No"]);
                    DL.ItemNo = Convert.ToString(row["Item No"]);
                    DL.Importer = Convert.ToString(row["Importer"]);
                    DL.LineName = Convert.ToString(row["Line Name"]);
                    DL.CargoDescriptions = Convert.ToString(row["Cargo Descriptions"]);
                    DL.Indate = Convert.ToString(row["In Date"]);

                    ContainerDL.Add(DL);
                }

            }
            return ContainerDL;
        }

        public EN.IGMItemNumberListEntities SearchIGMNoDetails(string IGMnumber, string BLnumber)
        {

            DataSet JODS = new DataSet();

            EN.IGMItemsNumber ContainerDL = new EN.IGMItemsNumber();
            DataTable dt1 = new DataTable();

            List<EN.IGMItemsNumber> ContainerDL1 = new List<EN.IGMItemsNumber>();

            JODS = trackerdatamanager.GetSearchOnItemNumber(IGMnumber, BLnumber);
            DataTable Containerlistdl = new DataTable();
            DataTable JOContainerDT = new DataTable();
            Containerlistdl = JODS.Tables[0];
            JOContainerDT = JODS.Tables[0];
            if (Containerlistdl.Rows.Count != 0)
            {
                foreach (DataRow row in Containerlistdl.Rows)
                {

                    ContainerDL.IGM_GoodsDesc = Convert.ToString(row["IGM_GoodsDesc"]);
                    ContainerDL.Consignee = Convert.ToString(row["Consignee"]);
                    ContainerDL.NConsignee = Convert.ToString(row["NConsignee"]);
                    ContainerDL.ViaNo = Convert.ToString(row["ViaNo"]);
                    ContainerDL.IGM_Origin = Convert.ToString(row["IGM_Origin"]);
                    ContainerDL.IGM_Voy = Convert.ToString(row["IGM_Voy"]);
                    ContainerDL.IGMDate = Convert.ToString(row["IGMDate"]);
                    ContainerDL.vesselName = Convert.ToString(row["IGM_Vessel"]);
                    ContainerDL.PortName = Convert.ToString(row["PortName"]);
                    ContainerDL.Slname = Convert.ToString(row["SLName"]);
                    ContainerDL.BOEDate = Convert.ToString(row["BOE Date"]);
                    ContainerDL.BOENo = Convert.ToString(row["BOE No"]);
                    ContainerDL.OOCNo = Convert.ToString(row["OOC No"]);
                    ContainerDL.OOCDate = Convert.ToString(row["OOC Date"]);
                    ContainerDL.CHAName = Convert.ToString(row["CHA Name"]);


                }

            }

            if (JOContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in JOContainerDT.Rows)
                {
                    EN.IGMItemsNumber DL = new EN.IGMItemsNumber();
                    DL.JONo = Convert.ToInt16(row["JO No"]);
                    DL.ContainerNo = Convert.ToString(row["Container No"]);
                    DL.Size = Convert.ToString(row["Size"]);
                    DL.FL = Convert.ToString(row["FL"]);
                    DL.ContainerType = Convert.ToString(row["Type"]);
                    DL.INDateTime = Convert.ToString(row["In Date and Time"]);
                    DL.outDate = Convert.ToString(row["Out Date"]);
                    DL.DeliveryType = Convert.ToString(row["Delivery Type"]);
                    DL.Scan = Convert.ToString(row["Scan"]);
                    DL.TrainNo = Convert.ToString(row["TrainNo"]);
                    DL.TrainArrivedDate = Convert.ToString(row["Train Arrived Date"]);
                    DL.Transport_Type = Convert.ToString(row["Transport Mode"]);
                    DL.IGM_SealNo = Convert.ToString(row["Seal No"]);
                    DL.IGM_BLNo = Convert.ToString(row["BL No"]);
                    DL.IGM_Qty = Convert.ToString(row["QTY"]);
                    DL.Pack = Convert.ToString(row["Pack"]);
                    DL.IGM_MT_Wt = Convert.ToString(row["Wt"]);
                    DL.Unit = Convert.ToString(row["Unit"]);
                    DL.Reason = Convert.ToString(row["Reason"]);
                    DL.GPDate = Convert.ToString(row["GP Date"]);
                    DL.AssessNo = Convert.ToString(row["Assess No"]);
                    DL.WorkYear = Convert.ToString(row["WorkYear "]);
                    DL.IGM_GoodsDesc = Convert.ToString(row["IGM_GoodsDesc"]);
                    DL.Consignee = Convert.ToString(row["Consignee"]);
                    DL.NConsignee = Convert.ToString(row["NConsignee"]);
                    DL.ViaNo = Convert.ToString(row["ViaNo"]);
                    DL.IGM_Origin = Convert.ToString(row["IGM_Origin"]);
                    DL.IGM_Voy = Convert.ToString(row["IGM_Voy"]);
                    DL.IGMDate = Convert.ToString(row["IGMDate"]);
                    DL.vesselName = Convert.ToString(row["IGM_Vessel"]);
                    DL.ScanningINDate = Convert.ToString(row["Scanning IN Date"]);
                    DL.ScanningOutDate = Convert.ToString(row["Scanning Out Date"]);
                    DL.berthingdate = Convert.ToString(row["Birthing Date"]);
                    DL.PortName = Convert.ToString(row["PortName"]);
                    DL.JOType = Convert.ToString(row["JO Type"]);
                    DL.IGMAddedOn = Convert.ToString(row["IGM Added On"]);
                    ContainerDL1.Add(DL);
                }


            }
            EN.IGMItemNumberListEntities JOrderList = new EN.IGMItemNumberListEntities();
            JOrderList.IGMData = ContainerDL;
            JOrderList.IGNList = ContainerDL1;
            return JOrderList;




        }


        public List<EN.ImportAccessMEntities> SearchIGMNoDetailsInvoicedetails(string IGMnumber, string BLnumber)
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetSearchOnItemNumberInvoiceDetails(IGMnumber, BLnumber);
            List<EN.ImportAccessMEntities> invoicedetailsDL = new List<EN.ImportAccessMEntities>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ImportAccessMEntities DL = new EN.ImportAccessMEntities();

                    DL.AssessNO = Convert.ToInt32(row["AssessNO"]);
                    DL.AssessDate = Convert.ToString(row["AssessDate"]);
                    DL.CHAName = Convert.ToString(row["CHAName"]);
                    DL.WorkYear = Convert.ToString(row["WorkYear"]);
                    DL.InvoiceNo = Convert.ToString(row["InvoiceNo"]);

                    invoicedetailsDL.Add(DL);
                }

            }
            return invoicedetailsDL;
        }
        //Codes End By Prashant


        public List<EN.IGMItemsNumber> SearchIGMNoSummarydetails(string IGMnumber, string BLnumber)
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetSearchOnItemNumberSummarydetails(IGMnumber, BLnumber);
            List<EN.IGMItemsNumber> summarydetails = new List<EN.IGMItemsNumber>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.IGMItemsNumber DL = new EN.IGMItemsNumber();

                    DL.Summary = Convert.ToString(row["Summary"]);
                    DL.Twenty = Convert.ToString(row["20"]);
                    DL.Fourty = Convert.ToString(row["40"]);
                    DL.Fourtyfive = Convert.ToString(row["45"]);
                    DL.Tues = Convert.ToString(row["TUES"]);

                    summarydetails.Add(DL);
                }

            }
            return summarydetails;
        }
        //Codes End By Prashant
        //Codes by Prashant

        public EN.ImportSearchEntities GetImportSearchDetails(string ContainerNo, string JoNo)
        {
            EN.ImportSearchEntities ImportSearchList = new EN.ImportSearchEntities();
            DataTable ImportDL = new DataTable();
            ImportDL = trackerdatamanager.GetImportSearchDetails(ContainerNo, JoNo);

            if (ImportDL.Rows.Count != 0)
            {
                foreach (DataRow row in ImportDL.Rows)
                {
                    EN.ImportSearchEntities ImportSearchListdl = new EN.ImportSearchEntities();
                    ImportSearchList.Size = Convert.ToString(row["Size"]);
                    ImportSearchList.ContainerType = Convert.ToString(row["ContainerType"]);
                    ImportSearchList.iSOCode = Convert.ToString(row["iSOCOde"]);
                    ImportSearchList.TrainArrivedDate = Convert.ToString(row["TrainArrivedDate"]);
                    ImportSearchList.BirthingDate = Convert.ToString(row["Birthing Date"]);
                    ImportSearchList.VesselName = Convert.ToString(row["VesselName"]);
                    ImportSearchList.SLName = Convert.ToString(row["SLName"]);
                    ImportSearchList.ViaNo = Convert.ToString(row["ViaNo"]);
                    ImportSearchList.ScanType = Convert.ToString(row["ScanType"]);
                    ImportSearchList.CFSCode = Convert.ToString(row["CFSCode"]);
                    ImportSearchList.PortID = Convert.ToString(row["PortID"]);
                    ImportSearchList.FL = Convert.ToString(row["FL"]);
                    ImportSearchList.Transport_Type = Convert.ToString(row["Transport_Type"]);
                    //ImportSearchListdl.vehicleno = Convert.ToString(row[""]);
                    ImportSearchList.JOdate = Convert.ToString(row["JOdate"]);
                    ImportSearchList.transname = Convert.ToString(row["transname"]);
                    //ImportSearchList.Add(ImportSearchListdl);
                }
            }

            return ImportSearchList;
        }


        public EN.ImportSearchEntities GetImportSearchDetailsS(string ContainerNo, string JoNo)
        {
            EN.ImportSearchEntities ImportSearchList = new EN.ImportSearchEntities();
            DataTable ImportDL = new DataTable();
            ImportDL = trackerdatamanager.GetImportSearchDetails(ContainerNo, JoNo);

            if (ImportDL.Rows.Count != 0)
            {
                foreach (DataRow row in ImportDL.Rows)
                {
                    EN.ImportSearchEntities ImportSearchListdl = new EN.ImportSearchEntities();
                    ImportSearchList.CurrentStatus = Convert.ToString(row["Current Status"]);
                    ImportSearchList.BirthingDate = Convert.ToString(row["Birthing Date"]);
                    ImportSearchList.Size = Convert.ToString(row["SizeType"]);
                    ImportSearchList.DischargeDateTime = Convert.ToString(row["Discharge Date Time"]);
                    ImportSearchList.iSOCode = Convert.ToString(row["iSOCOde"]);
                    ImportSearchList.INDate = Convert.ToString(row["INDate"]);
                    ImportSearchList.CargoType = Convert.ToString(row["cargotype"]);
                    ImportSearchList.ClassUnNo = Convert.ToString(row["Class/Un No"]);
                    ImportSearchList.ViaNo = Convert.ToString(row["ViaNo"]);
                    ImportSearchList.JOdate = Convert.ToString(row["JOdate"]);
                    ImportSearchList.VesselName = Convert.ToString(row["VesselName"]);
                    ImportSearchList.joType = Convert.ToString(row["Jo_type"]);
                    ImportSearchList.PortID = Convert.ToString(row["Portname"]);
                    ImportSearchList.ScanType = Convert.ToString(row["ScanType"]);
                    ImportSearchList.vehicleno = Convert.ToString(row["TruckNo"]);
                    ImportSearchList.ContainerType = Convert.ToString(row["ContainerType"]);
                    ImportSearchList.FL = Convert.ToString(row["FL"]);
                    ImportSearchList.TrainArrivedDate = Convert.ToString(row["TrainArrivedDate"]);
                    ImportSearchList.SLName = Convert.ToString(row["SLName"]);
                    ImportSearchList.CFSCode = Convert.ToString(row["CFSCode"]);
                    ImportSearchList.Transport_Type = Convert.ToString(row["Transport_Type"]);
                    ImportSearchList.transname = Convert.ToString(row["transname"]);
                    ImportSearchList.GateinRemarks = Convert.ToString(row["Gate In remarks"]);
                    ImportSearchList.GateINVehicleNo = Convert.ToString(row["transname"]);
                    ImportSearchList.TrailerType = Convert.ToString(row["Trailer Type"]);
                    ImportSearchList.TransporterName = Convert.ToString(row["transname"]);
                    ImportSearchList.CustomExamine = Convert.ToString(row["customexamineper"]);
                    ImportSearchList.FinalOutDateTime = Convert.ToString(row["finaloutdate"]);
                    ImportSearchList.OutVehicleNo = Convert.ToString(row["OUtvehicleno"]);
                    ImportSearchList.EmptyYard = Convert.ToString(row["Emptyyard"]);
                    ImportSearchList.WeighmentSlipNo = Convert.ToString(row["WeightmentSlipno"]);
                    ImportSearchList.WeighmentSlipDate = Convert.ToString(row["Weightmentslipdate"]);
                    ImportSearchList.GatePassNo = Convert.ToString(row["GatepassNO"]);
                    ImportSearchList.ScanningGPDate = Convert.ToString(row["ScanningOutGpdate"]);
                    ImportSearchList.OOCNO = Convert.ToString(row["occno"]);
                    ImportSearchList.OOCDate = Convert.ToString(row["oocdate"]);
                    ImportSearchList.DODate = Convert.ToString(row["dodate"]);
                    ImportSearchList.DOValidDateTime = Convert.ToString(row["dodate"]);
                    ImportSearchList.BOENO = Convert.ToString(row["BOEno"]);
                    ImportSearchList.BOEDateTIme = Convert.ToString(row["boedate"]);
                    ImportSearchList.GatePassDateTime = Convert.ToString(row["gatepassdate"]);
                    ImportSearchList.InvoiceNoDate = Convert.ToString(row["transname"]);
                    ImportSearchList.ScanningGateInDate = Convert.ToString(row["ScanningInGpdate"]);
                    ImportSearchList.SSRNo = Convert.ToString(row["SSRNO"]);
                    ImportSearchList.SSRDateTime = Convert.ToString(row["SSRDate"]);
                    ImportSearchList.ShippingLineName = Convert.ToString(row["SLName"]);
                    ImportSearchList.CustomerName = Convert.ToString(row["AgName"]);
                    ImportSearchList.SealCuttingDateTime = Convert.ToString(row["Sealcuttingdate"]);
                    ImportSearchList.EmptyGatePassNo = Convert.ToString(row["EmptyGPNo"]);
                    ImportSearchList.EmptyGatePassDateTime = Convert.ToString(row["emptyoutdate"]);
                    ImportSearchList.ScanningLocation = Convert.ToString(row["Scanningloc"]);
                    ImportSearchList.CHAName = Convert.ToString(row["CHAName"]);
                    ImportSearchList.Location = Convert.ToString(row["Location"]);
                    ImportSearchList.jono = Convert.ToString(row["JONo"]);
                    ImportSearchList.AcutionFirstNoticeNo = Convert.ToString(row["AcutionFirstNoticeNo"]);
                    ImportSearchList.AucSeconNO = Convert.ToString(row["AucSeconNO"]);
                    ImportSearchList.aucSecondDate = Convert.ToString(row["aucSecondDate"]);
                    ImportSearchList.AucFirstDate = Convert.ToString(row["AucFirstDate"]);
                    ImportSearchList.EIRDate = Convert.ToString(row["EIRDate"]);

                    //ImportSearchList.Add(ImportSearchListdl);
                }
            }

            return ImportSearchList;
        }

        public List<EN.ImportSearchEntities> GetTimeLine(string ContainerNo, string Jono)
        {

            List<EN.ImportSearchEntities> TimeLineList = new List<EN.ImportSearchEntities>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetTimeLine(Jono, ContainerNo);

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.ImportSearchEntities SalesDL = new EN.ImportSearchEntities();
                    SalesDL.Process = Convert.ToString(row["process"]);
                    SalesDL.JoDate = Convert.ToString(row["Jo Date"]);
                    SalesDL.DwellDay = Convert.ToString(row["Dwell Days"]);
                    SalesDL.PreparedBy = Convert.ToString(row["Prepared By"]);
                    SalesDL.jono = Convert.ToString(row["JONo"]);
                    SalesDL.Containerno = Convert.ToString(row["ContainerNo"]);
                    TimeLineList.Add(SalesDL);
                }
            }
            return TimeLineList;


        }

        public List<EN.ImportSearchEntities> GetTimeLineExport(string ContainerNo, string Jono)
        {

            List<EN.ImportSearchEntities> TimeLineList = new List<EN.ImportSearchEntities>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetTimeLineExport(ContainerNo, Jono);

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.ImportSearchEntities SalesDL = new EN.ImportSearchEntities();
                    SalesDL.Process = Convert.ToString(row["process"]);
                    SalesDL.JoDate = Convert.ToString(row["Date"]);
                    SalesDL.PreparedBy = Convert.ToString(row["Prepared By"]);
                    TimeLineList.Add(SalesDL);
                }
            }
            return TimeLineList;


        }


        public List<EN.ImportSearchEntities> GetTimeLineContainer(string ContainerNo, string Jono)
        {

            List<EN.ImportSearchEntities> TimeLineList = new List<EN.ImportSearchEntities>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetTimeLineContainer(ContainerNo, Jono);

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.ImportSearchEntities SalesDL = new EN.ImportSearchEntities();
                    SalesDL.Process = Convert.ToString(row["process"]);
                    SalesDL.JoDate = Convert.ToString(row["Date"]);
                    SalesDL.PreparedBy = Convert.ToString(row["Prepared By"]);
                    TimeLineList.Add(SalesDL);
                }
            }
            return TimeLineList;


        }

        public List<EN.GetIGMDetailsOnJONO> SearchImportSearchList(string JONO, string Containerno)
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetImportSearchDetailsList(JONO, Containerno);
            List<EN.GetIGMDetailsOnJONO> ImportSearchDL = new List<EN.GetIGMDetailsOnJONO>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.GetIGMDetailsOnJONO DL = new EN.GetIGMDetailsOnJONO();

                    DL.IGMNo = Convert.ToString(row["IGM No"]);
                    DL.ItemNo = Convert.ToString(row["Item No"]);
                    DL.BLNo = Convert.ToString(row["BL No"]);
                    DL.SealNo = Convert.ToString(row["Seal No"]);
                    DL.Consignee = Convert.ToString(row["Consignee"]);
                    DL.NConsignee = Convert.ToString(row["N Consignee"]);
                    DL.Qty = Convert.ToString(row["Qty"]);
                    DL.PackType = Convert.ToString(row["Pack Type"]);
                    DL.GrossWt = Convert.ToString(row["Gross Wt"]);
                    DL.Unit = Convert.ToString(row["Unit"]);
                    DL.GridNo = Convert.ToString(row["Grid No"]);
                    DL.GoodsDesc = Convert.ToString(row["Goods Desc"]);
                    DL.GPNo = Convert.ToString(row["GP No"]);
                    DL.GPDate = Convert.ToString(row["GP Date"]);
                    DL.IGMAddedOn = Convert.ToString(row["IGM Added On"]);

                    ImportSearchDL.Add(DL);
                }

            }
            return ImportSearchDL;
        }


        public List<EN.HoldDetailsEntities> SearchImportSearchHoldingList(string JONO, string Containerno)
        {
            DataTable HoldingDL = new DataTable();
            HoldingDL = trackerdatamanager.GetImportSearchHoldingDetails(JONO, Containerno);
            List<EN.HoldDetailsEntities> ImportSearchHoldingDL = new List<EN.HoldDetailsEntities>();

            if (HoldingDL.Rows.Count != 0)
            {
                foreach (DataRow row in HoldingDL.Rows)
                {
                    EN.HoldDetailsEntities DL = new EN.HoldDetailsEntities();

                    DL.HoldReason = Convert.ToString(row["Hold Reason"]);
                    DL.HoldBy = Convert.ToString(row["Hold By"]);
                    DL.HoldDate = Convert.ToString(row["Hold Date"]);
                    DL.ClearedBy = Convert.ToString(row["Cleared By"]);
                    DL.ClearedOn = Convert.ToString(row["Cleared On"]);
                    DL.Remarks = Convert.ToString(row["Remarks"]);
                    ImportSearchHoldingDL.Add(DL);
                }

            }
            return ImportSearchHoldingDL;
        }

        public List<EN.JobOrderDEntities> getjoborderlist(string Containerno)
        {
            DataTable jonoDL = new DataTable();
            jonoDL = trackerdatamanager.getjono(Containerno);
            List<EN.JobOrderDEntities> joborderDL = new List<EN.JobOrderDEntities>();

            if (jonoDL.Rows.Count != 0)
            {
                foreach (DataRow row in jonoDL.Rows)
                {
                    EN.JobOrderDEntities DL = new EN.JobOrderDEntities();

                    DL.JONo = Convert.ToInt32(row["jono"]);
                    joborderDL.Add(DL);
                }

            }
            return joborderDL;
        }
        //Codes By Prashant

        public EN.TrailersEntities getdrivername(string trailername)
        {
            EN.TrailersEntities DriverDL = new EN.TrailersEntities();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.getdrivername(trailername);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {

                    DriverDL.DriverID = Convert.ToInt32(row["DriverID"]);
                    DriverDL.DriverName = Convert.ToString(row["drivername"]);
                    DriverDL.Permit = Convert.ToString(row["Permit"]);
                    DriverDL.PermitExpiry = Convert.ToString(row["PermitExpiry"]);
                    DriverDL.ContactNo = Convert.ToString(row["D_Contactno"]);
                    DriverDL.trailertype = Convert.ToString(row["trailertype"]);
                }
            }
            return DriverDL;
        }
        public string CheckTrailerPermit(string trailername)
        {
            string message = "";
            DataTable TrailerPermitDL = new DataTable();
            TrailerPermitDL = trackerdatamanager.checktrailerpermitexits(trailername);
            if (TrailerPermitDL.Rows.Count > 0)
            {
                int permitCount = Convert.ToInt16(TrailerPermitDL.Rows[0]["Result"]);
                if (permitCount == 0)
                {
                    message = "0";

                }
                else
                {
                    message = "1";
                }

            }
            return message;
        }


        public string InsertTPdetails(string TPdate, int Trailerid, int Driverid,  string amount, int userid, string TPlocation, string TPfor)
        {

            string message = "";
            DataTable TPdetails = new DataTable();
            TPdetails = trackerdatamanager.SaveTPEntryDetails(TPdate, Trailerid, Driverid, amount, userid, TPlocation, TPfor);
            message = "Record saved Successfully!";
            return message;

        }


        public List<EN.TPEntryEntities> GetTPList()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetTPDetails();
            List<EN.TPEntryEntities> GetTPDataList = new List<EN.TPEntryEntities>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.TPEntryEntities DL = new EN.TPEntryEntities();
                    DL.trailerno = Convert.ToString(row["trailerid"]);
                    DL.TPdate = Convert.ToString(row["TPdate"]);
                    DL.trailername = Convert.ToString(row["trailername"]);
                    DL.drivername = Convert.ToString(row["drivername"]);
                    DL.startdate = Convert.ToString(row["startdate"]);
                    DL.enddate = Convert.ToString(row["enddate"]);
                    DL.amount = Convert.ToString(row["amount"]);
                    DL.TPLocation = Convert.ToString(row["TPLocation"]);
                    DL.TPfor = Convert.ToString(row["TPfor"]);
                    DL.TPNumber = Convert.ToString(row["tpno"]);

                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }


        public EN.TPEntryEntities GetPrintTPList(int trailerid, int tpNo)
        {
            DataTable TrailerDL = new DataTable();
            TrailerDL = trackerdatamanager.GetPrintTPDetails(trailerid, tpNo);
            EN.TPEntryEntities GetTPDataList = new EN.TPEntryEntities();

            if (TrailerDL.Rows.Count != 0)
            {
                foreach (DataRow row in TrailerDL.Rows)
                {
                    EN.TPEntryEntities DL = new EN.TPEntryEntities();
                    GetTPDataList.trailerno = Convert.ToString(row["trailerid"]);
                    GetTPDataList.TPdate = Convert.ToString(row["TPdate"]);
                    GetTPDataList.trailername = Convert.ToString(row["trailername"]);
                    GetTPDataList.drivername = Convert.ToString(row["drivername"]);
                    GetTPDataList.startdate = Convert.ToString(row["startdate"]);
                    GetTPDataList.enddate = Convert.ToString(row["enddate"]);
                    GetTPDataList.amount = Convert.ToString(row["amount"]);
                    GetTPDataList.AmountWords = Convert.ToString(row["Amountword"]);
                    GetTPDataList.TPNumber = Convert.ToString(row["TPNo"]);
                   

                }

            }
            return GetTPDataList;

        }
        //Codes By Prashant 
        public List<EN.ContainerDetailsEntities> GetContainerDetails(string Containerno)
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetContainerDetials(Containerno);
            List<EN.ContainerDetailsEntities> GetContainerList = new List<EN.ContainerDetailsEntities>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.ContainerDetailsEntities DL = new EN.ContainerDetailsEntities();

                    DL.JONO = Convert.ToInt32(row["jono"]);
                    DL.Size = Convert.ToString(row["size"]);
                    DL.ContainerType = Convert.ToString(row["ContainerType"]);
                    DL.INDate = Convert.ToString(row["INdate"]);
                    DL.ShippingLine = Convert.ToString(row["SLname"]);
                    GetContainerList.Add(DL);

                }

            }
            return GetContainerList;

        }




        public string SaveContainerDetails(string Containerno, int jono, string Examine, int userid)
        {

            string message = "";

            DataTable ContainerDetailsDL = new DataTable();

            ContainerDetailsDL = trackerdatamanager.SaveContainerDetails(Containerno, jono, Examine, userid);
            message = "Record saved successfully!";
            return message;
        }

        public string TPApproveSaveDetails(int trailerid, int userId)
        {
            string message = "";

            DataTable ApproveDL = new DataTable();
            ApproveDL = trackerdatamanager.TPApproveSaveDetails(trailerid, userId);

            message = "TP Approved Successfully";
            return message;
        }

        public List<EN.TPEntryEntities> GetApprovedTPList()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetTPApprovedDetails();
            List<EN.TPEntryEntities> GetTPDataList = new List<EN.TPEntryEntities>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.TPEntryEntities DL = new EN.TPEntryEntities();
                    DL.trailerno = Convert.ToString(row["trailerid"]);
                    DL.TPdate = Convert.ToString(row["TPdate"]);
                    DL.trailername = Convert.ToString(row["trailername"]);
                    DL.drivername = Convert.ToString(row["drivername"]);
                    DL.startdate = Convert.ToString(row["startdate"]);
                    DL.enddate = Convert.ToString(row["enddate"]);
                    DL.amount = Convert.ToString(row["amount"]);
                    DL.TPLocation = Convert.ToString(row["TPLocation"]);
                    DL.TPfor = Convert.ToString(row["TPfor"]);
                    DL.TPNumber = Convert.ToString(row["TPno"]);
                    DL.TPNO = Convert.ToString(row["TPNo1"]);

                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }

        public string CheckCustomExamineDetails(int jono, string containerno)
        {
            string message = "";
            DataTable CustomExamineDL = new DataTable();
            CustomExamineDL = trackerdatamanager.GetCustomExamineValidate(jono, containerno);

            if (CustomExamineDL.Rows.Count > 0)
            {
                message = "1";
            }
            else
            {
                message = "0";
            }


            return message;
        }



        public List<EN.ContainerDetailsEntities> GetCustomExamineFetchDetails()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetCustomFetchDetails();
            List<EN.ContainerDetailsEntities> GetContainerList = new List<EN.ContainerDetailsEntities>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.ContainerDetailsEntities DL = new EN.ContainerDetailsEntities();

                    DL.JONO = Convert.ToInt32(row["JoNo"]);
                    DL.ContainerNO = Convert.ToString(row["ContainerNo"]);
                    DL.Size = Convert.ToString(row["Size"]);
                    DL.ContainerType = Convert.ToString(row["ContainerType"]);
                    DL.INDate = Convert.ToString(row["InDate"]);
                    DL.ShippingLine = Convert.ToString(row["SLname"]);
                    DL.RMSPerc = Convert.ToString(row["RMSPerc"]);
                    GetContainerList.Add(DL);

                }

            }
            return GetContainerList;

        }



        public List<EN.TrainMaster> GetTrainNumberExportDetails()
        {
            DataTable TrainDL = new DataTable();
            TrainDL = trackerdatamanager.GetTrainNumber();
            List<EN.TrainMaster> GetTrainnoList = new List<EN.TrainMaster>();

            if (TrainDL.Rows.Count != 0)
            {
                foreach (DataRow row in TrainDL.Rows)
                {
                    EN.TrainMaster DL = new EN.TrainMaster();

                    DL.TrainID = Convert.ToInt16(row["trainid"]);
                    DL.TrainNO = Convert.ToString(row["trainno"]);

                    GetTrainnoList.Add(DL);

                }

            }
            return GetTrainnoList;

        }



        public List<EN.PortInEntities> GetPortInDetails(string trainno)
        {
            DataTable TrainDL = new DataTable();
            TrainDL = trackerdatamanager.GetPortINdetails(trainno);
            List<EN.PortInEntities> GetTrainnoList = new List<EN.PortInEntities>();

            if (TrainDL.Rows.Count != 0)
            {
                foreach (DataRow row in TrainDL.Rows)
                {
                    EN.PortInEntities DL = new EN.PortInEntities();

                    DL.PlanningID = Convert.ToInt16(row["PlanningID"]);
                    DL.PlanningDate = Convert.ToString(row["PlanningDate"]);
                    DL.ProcessType = Convert.ToString(row["ProcessType"]);
                    DL.EntryID = Convert.ToString(row["EntryID"]);
                    DL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    DL.TransType = Convert.ToString(row["Transport_type"]);
                    DL.TrainNo = Convert.ToString(row["TrainNo"]);
                    DL.WagonNo = Convert.ToString(row["WagonNo"]);
                    DL.TrailerNo = Convert.ToString(row["TrailerNo"]);
                    DL.FromDestination = Convert.ToString(row["FromDestination"]);
                    DL.ToDestination = Convert.ToString(row["ToDestination"]);
                    DL.PlannedBy = Convert.ToString(row["PlannedBy"]);
                    DL.PlannedON = Convert.ToString(row["PlannedON"]);
                    DL.Remarks = Convert.ToString(row["Remarks"]);

                    DL.PortIndate = Convert.ToString(row["PortIndate"]);
                    DL.PortInRemarks = Convert.ToString(row["PortInRemarks"]);
                    DL.PortInBy = Convert.ToString(row["PortInBy"]);
                    DL.LoadingBy = Convert.ToString(row["LoadingBy"]);
                    DL.LoadingDate = Convert.ToString(row["LoadingDate"]);

                    GetTrainnoList.Add(DL);

                }

            }
            return GetTrainnoList;

        }


        public string UpdatePortINList(DataTable Itemdata, int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("UserID", UserID);


            DataSet ds = new DataSet();
            int i = 0;
            i = db.AddPortTypeTableData("USP_UpdatePortInList", parameterList, Itemdata, "PT_PortInDetails", "@PT_PortInDetails");
          

            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            // Message = "Record saved successfully";
            return Message;



        }

        //Codes By Prashant

        public List<EN.TPEntryEntities> GetTPCloseDetails()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetTPCloseDetails();
            List<EN.TPEntryEntities> GetTPDataList = new List<EN.TPEntryEntities>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.TPEntryEntities DL = new EN.TPEntryEntities();
                    DL.trailerno = Convert.ToString(row["trailerid"]);
                    DL.TPdate = Convert.ToString(row["TPdate"]);
                    DL.trailername = Convert.ToString(row["trailername"]);
                    DL.drivername = Convert.ToString(row["drivername"]);
                    DL.startdate = Convert.ToString(row["startdate"]);
                    DL.enddate = Convert.ToString(row["enddate"]);
                    DL.amount = Convert.ToString(row["amount"]);
                    DL.TPLocation = Convert.ToString(row["TPLocation"]);
                    DL.TPfor = Convert.ToString(row["TPfor"]);
                    DL.TPNumber = Convert.ToString(row["TPNo"]);
                    DL.TPNO = Convert.ToString(row["TPNo1"]);
                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }


        //public string AddToCloseDetails(int TpNo, string startdate, string enddate)
        //{
        //    string message = "";

        //    DataTable ApproveDL = new DataTable();
        //    ApproveDL = trackerdatamanager.SaveTPCloseDetails(TpNo, startdate, enddate);

        //    message = "TP Closed Successfully";
        //    return message;
        //}

        public List<EN.TPEntryEntities> AddToCloseDetails(EN.TPEntryEntities TPDetails, byte[] attachByte,string contentType)
        {
            List<EN.TPEntryEntities> Companyticket = new List<EN.TPEntryEntities>();
            DataTable CreateUserDL = new DataTable();

            DataTable ApproveDL = new DataTable();
               ApproveDL = trackerdatamanager.SaveTPCloseDetails(TPDetails.TPNumber, TPDetails.startdate,TPDetails.enddate, attachByte, contentType);

            return Companyticket;
        }









        //Codes By Prashant

        public string SaveVehicleInOut(string ActivityDate, int Activity, int Fromlocation, int tolocation, string trailerid, string party, string reamks, int addedby, string Containercount, string INout, string ContainerNo1, string ContainerNo2, string Activitycycle, string ContainerType)
        {

            string message = "";
            DataTable ApproveDL = new DataTable();
            ApproveDL = trackerdatamanager.SaveVehicleInOutDetails(ActivityDate, Activity, Fromlocation, tolocation, trailerid, party, reamks, addedby, Containercount, INout, ContainerNo1, ContainerNo2, Activitycycle, ContainerType);

            message = "Record saved successfully!";
            return message;

        }
        public string SaveAdditionMovmentrequest(string Containerno, string VoucherNo, int activity, int FromLocation, int Tolocation, string remarks, string Fuel,string kilometer, int userid)
        {

            string message = "";
            DataTable ApproveDL = new DataTable();
            ApproveDL = trackerdatamanager.SaveAdditinalmovmentrequest(Containerno, VoucherNo, activity, FromLocation, Tolocation, remarks, Fuel, kilometer, userid);

            message = "Record saved successfully!";
            return message;

        }
        public List<EN.AdditionalMovmentrequestEntities> GetAdditionalMovementRequest()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetAdditionalmovmentDetails();
            List<EN.AdditionalMovmentrequestEntities> GetAdditionalMovment = new List<EN.AdditionalMovmentrequestEntities>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.AdditionalMovmentrequestEntities DL = new EN.AdditionalMovmentrequestEntities();
                    DL.RequestID = Convert.ToString(row["RequestID"]);
                    DL.RequestOn = Convert.ToString(row["RequestOn"]);
                    DL.VoucherNo = Convert.ToString(row["VoucherNo"]);
                    DL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    DL.Activity = Convert.ToString(row["Activity"]);
                    DL.FromLocation = Convert.ToString(row["FromLocation"]);
                    DL.ToLocation = Convert.ToString(row["ToLocation"]);
                    DL.Remarks = Convert.ToString(row["Remarks"]);
                    DL.FuelQuanity = Convert.ToString(row["FuelQuanity"]);
                    DL.Kilometer = Convert.ToString(row["kilometer"]);
                    GetAdditionalMovment.Add(DL);
                }
            }
            return GetAdditionalMovment;
        }

        public string ApproveRequest(int RequestID, string fuel,int UserID)
        {

            string message = "";
            DataTable ApproveDL = new DataTable();
            ApproveDL = trackerdatamanager.ApproveRequest(RequestID, fuel, UserID);

            message = "Record saved successfully!";
            return message;

        }

        public List<EN.ActivityMaster> getVehicleActivity()
        {
            List<EN.ActivityMaster> passingDL = new List<EN.ActivityMaster>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getVehicleActivity();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ActivityMaster PassingList = new EN.ActivityMaster();
                    PassingList.AutoID = Convert.ToInt32(row["AutoID"]);
                    PassingList.Activity = Convert.ToString(row["Activity"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public string CheckAdditionalMovment(string Voucherno)
        {
            string message = "";
            DataTable VouchernoDL = new DataTable();
            VouchernoDL = trackerdatamanager.getCheckVoucherno(Voucherno);

            if (VouchernoDL.Rows.Count > 0)
            {
                int permitCount = Convert.ToInt16(VouchernoDL.Rows[0]["VoucherNO"]);
                if (permitCount == 0)
                {
                    message = "0";

                }
                else
                {
                    message = "1";
                }

            }


            return message;
        }

        public List<EN.TPEntryEntities> GetTPCloseDetailsList()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetTPCloseDetailsList();
            List<EN.TPEntryEntities> GetTPDataList = new List<EN.TPEntryEntities>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.TPEntryEntities DL = new EN.TPEntryEntities();
                    DL.trailerno = Convert.ToString(row["trailerid"]);
                    DL.TPdate = Convert.ToString(row["TPdate"]);
                    DL.trailername = Convert.ToString(row["trailername"]);
                    DL.drivername = Convert.ToString(row["drivername"]);
                    DL.startdate = Convert.ToString(row["startdate"]);
                    DL.enddate = Convert.ToString(row["enddate"]);
                    DL.amount = Convert.ToString(row["amount"]);
                    DL.TPLocation = Convert.ToString(row["TPLocation"]);
                    DL.TPfor = Convert.ToString(row["TPfor"]);
                    DL.TPNumber = Convert.ToString(row["TPNo"]);
                    DL.TPNO = Convert.ToString(row["TPNo1"]);
                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }


        public EN.TPEntryEntities getAttachment(int ID)
        {
            EN.TPEntryEntities Companydetails = new EN.TPEntryEntities();
            DataTable CompanyDL = new DataTable();
            CompanyDL = trackerdatamanager.GetAttachmentdetails(ID);
            if (CompanyDL != null)
            {
                foreach (DataRow row in CompanyDL.Rows)
                {

                    Companydetails.Attachment = (byte[])(row["Attachment"]);
                    Companydetails.ContentType = Convert.ToString(row["Contenttype"]);

                }
            }
            return Companydetails;
        }


        // COdes by Prashant

        public List<EN.TPEntryEntities> GetTPHistory(int trailerid)
        {
            DataTable historyDL = new DataTable();
            historyDL = trackerdatamanager.GetTPHistory(trailerid);
            List<EN.TPEntryEntities> GetTPDataList = new List<EN.TPEntryEntities>();

            if (historyDL.Rows.Count != 0)
            {
                foreach (DataRow row in historyDL.Rows)
                {
                    EN.TPEntryEntities DL = new EN.TPEntryEntities();
                  
                    DL.TPdate = Convert.ToString(row["TPdate"]);
                    DL.TPNO = Convert.ToString(row["TPNo1"]);
                    DL.trailername = Convert.ToString(row["trailername"]);
                    DL.drivername = Convert.ToString(row["drivername"]);
                    DL.startdate = Convert.ToString(row["startdate"]);
                    DL.enddate = Convert.ToString(row["enddate"]);
                    DL.amount = Convert.ToString(row["amount"]);
                    DL.TPLocation = Convert.ToString(row["TPLocation"]);
                    DL.TPfor = Convert.ToString(row["TPfor"]);
                   
                  
                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }

        //codes by manish
        public List<EN.fuelType> getfuelType()
        {
            List<EN.fuelType> passingDL = new List<EN.fuelType>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getFuelType();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.fuelType PassingList = new EN.fuelType();
                    PassingList.Fuelid = Convert.ToInt32(row["Fuelid"]);
                    PassingList.FuelType = Convert.ToString(row["FuelType"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.FuelVendorMaster> getFuelVendorMaster()
        {
            List<EN.FuelVendorMaster> passingDL = new List<EN.FuelVendorMaster>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getFuelVendorMaster();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.FuelVendorMaster PassingList = new EN.FuelVendorMaster();
                    PassingList.fuel_Vendorid = Convert.ToInt32(row["fuel_VendorID"]);
                    PassingList.fuelVendorName = Convert.ToString(row["fuel_VendorName"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.CostCenter> getCostCenter()
        {
            List<EN.CostCenter> passingDL = new List<EN.CostCenter>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getCostCenter();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CostCenter PassingList = new EN.CostCenter();
                    PassingList.Costid = Convert.ToInt32(row["id"]);
                    PassingList.CostCenterName = Convert.ToString(row["Cost_Center"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        public string SaveInternalFuelStock(string txtentryDate, float txtFuelQty, int txtRate, int ddlVendorName, string trailerid, string ddlFuelType, int UserID, int ddlCostCenter, string CostCenterFor, string FuelFor)
        {

            string message = "";
            DataTable InternalFuelStock = new DataTable();
            InternalFuelStock = trackerdatamanager.SaveInternalFuelStock(txtentryDate, txtFuelQty, txtRate, ddlVendorName, trailerid, ddlFuelType, UserID, ddlCostCenter, CostCenterFor, FuelFor);

            message = "Record saved successfully!";
            return message;

        }

        public string SaveFuelConsumption(string txtentryDate, int ddlCostCenter, int trailerid, double txtBalQty, string txtIssueQty,
         string ddlFuelType, int txtReadFrom, int txtReadingTo, int ddlDriverName, string txtRemark, string ddlUnit,
         int UserID, int ddlvehicleType, string CostCenterFor, string differencereading, string VehiclePurpose, string Vehiclesubgroup, string Stockslipno)
        {

            string message = "";
            DataTable InternalFuelStock = new DataTable();
            InternalFuelStock = trackerdatamanager.SaveFuelConsumption(txtentryDate, ddlCostCenter,
                trailerid, txtBalQty, txtIssueQty, ddlFuelType, txtReadFrom, txtReadingTo, ddlDriverName,
                txtRemark, ddlUnit, UserID, ddlvehicleType, CostCenterFor, differencereading, VehiclePurpose, Vehiclesubgroup, Stockslipno);

            message = "Record saved successfully!";
            return message;

        }
        //public string SaveFuelConsumption(string txtentryDate, int ddlCostCenter, int trailerid, int txtBalQty, string txtIssueQty, string ddlFuelType, int txtReadFrom, int txtReadingTo, int ddlDriverName, string txtRemark, string ddlUnit, int UserID, int ddlvehicleType, string CostCenterFor, string differencereading)
        //{

        //    string message = "";
        //    DataTable InternalFuelStock = new DataTable();
        //    InternalFuelStock = trackerdatamanager.SaveFuelConsumption(txtentryDate, ddlCostCenter, trailerid, txtBalQty, txtIssueQty, ddlFuelType, txtReadFrom, txtReadingTo, ddlDriverName, txtRemark, ddlUnit, UserID, ddlvehicleType, CostCenterFor, differencereading);

        //    message = "Record saved successfully!";
        //    return message;

        //}






        //public List<EN.FuelConsumption> GetCmbFuelTypeOnChange(string Fueltype, int CostCenter)
        //{
        //    List<EN.FuelConsumption> passingDL = new List<EN.FuelConsumption>();
        //    DataTable dt = new DataTable();
        //    dt = trackerdatamanager.GetCmbFuelTypeOnChange(Fueltype,CostCenter);

        //    if (dt != null)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            EN.FuelConsumption PassingList = new EN.FuelConsumption();
        //            PassingList.BalQty = Convert.ToString(row["Qty"]);
        //            passingDL.Add(PassingList);
        //        }
        //    }
        //    return passingDL;
        //}
        public EN.FuelConsumption GetCmbFuelTypeOnChange(string Fueltype, int CostCenter, string ddlCostCenterFor)
        {
            EN.FuelConsumption passingDL = new EN.FuelConsumption();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetCmbFuelTypeOnChange(Fueltype, CostCenter, ddlCostCenterFor);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    // EN.FuelConsumption passingDL = new EN.FuelConsumption();
                    passingDL.BalQty = Convert.ToString(row["Qty"]);

                }
            }
            return passingDL;
        }

        public EN.FuelConsumption AjaxgetFromReading(string trailername,string CostCenterFor, string FuelType)
        {
            EN.FuelConsumption passingDL = new EN.FuelConsumption();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.AjaxgetFromReading(trailername, CostCenterFor,FuelType);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    // EN.FuelConsumption passingDL = new EN.FuelConsumption();
                    passingDL.currentReading = Convert.ToString(row["currentReading"]);
                    passingDL.VehicleType = Convert.ToInt32(row["VehicleTypeID"]);

                }
            }
            return passingDL;
        }


        public EN.FuelStockSummary PrintInternalFuelStock(string FuelFor)
        {
            EN.FuelStockSummary passingDL = new EN.FuelStockSummary();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.PrintInternalFuelStock(FuelFor);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    // EN.FuelConsumption passingDL = new EN.FuelConsumption();
                    passingDL.fuelRefNo = Convert.ToString(row["fuelRefNo"]);
                    passingDL.EntryDate = Convert.ToString(row["FuelRegDate"]);
                    passingDL.qty = Convert.ToString(row["Qty"]);
                    passingDL.fuelType = Convert.ToString(row["fuelType"]);
                    passingDL.vendorname = Convert.ToString(row["fuel_VendorName"]);
                    passingDL.FuelFor = Convert.ToString(row["Fuel For"]);


                }
            }
            return passingDL;
        }

        public EN.FuelStockSummary PrintInternalFuelStockReprint(string fuelregid, string FuelFor)
        {
            EN.FuelStockSummary passingDL = new EN.FuelStockSummary();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.PrintInternalFuelStockReprint(fuelregid, FuelFor);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    // EN.FuelConsumption passingDL = new EN.FuelConsumption();
                    passingDL.fuelRefNo = Convert.ToString(row["fuelRefNo"]);
                    passingDL.EntryDate = Convert.ToString(row["FuelRegDate"]);
                    passingDL.qty = Convert.ToString(row["Qty"]);
                    passingDL.fuelType = Convert.ToString(row["fuelType"]);
                    passingDL.vendorname = Convert.ToString(row["fuel_VendorName"]);
                    passingDL.CostCenterFor = Convert.ToString(row["centername"]);
                    passingDL.FuelFor = Convert.ToString(row["Fuel For"]);


                }
            }
            return passingDL;
        }

        public EN.FuelConsumption PrintFuelConsumption(string costcenterfor)
        {
            EN.FuelConsumption passingDL = new EN.FuelConsumption();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.PrintFuelConsumption(costcenterfor);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    // EN.FuelConsumption passingDL = new EN.FuelConsumption();
                    passingDL.fuelRefNo = Convert.ToString(row["fuelRefNo"]);
                    passingDL.IssueDate = Convert.ToString(row["IssueDate"]);
                    passingDL.trailername = Convert.ToString(row["trailername"]);
                    passingDL.drivername = Convert.ToString(row["drivername"]);
                    passingDL.BalQty = Convert.ToString(row["balQty"]);
                    passingDL.issueQty = Convert.ToString(row["issueQty"]);
                    passingDL.fuelType = Convert.ToString(row["fuelType"]);
                    passingDL.lastReading = Convert.ToString(row["lastReading"]);
                    passingDL.currentReading = Convert.ToString(row["currentReading"]);
                    passingDL.CostCenterFor = Convert.ToString(row["Centername"]);
                    passingDL.Differencereading = Convert.ToString(row["differencereading"]);
                    passingDL.Fueltype = Convert.ToString(row["fuelType"]);
                    passingDL.VehicleTypeName = Convert.ToString(row["VehicleType"]);


                }
            }
            return passingDL;
        }


        public List<EN.FuelConsumption> FuelConsumptionSummary(string txtsearch, string fromdate, string todate)
        {
            DataTable historyDL = new DataTable();
            historyDL = trackerdatamanager.FuelConsumptionSummary(txtsearch, fromdate, todate);
            List<EN.FuelConsumption> GetTPDataList = new List<EN.FuelConsumption>();

            if (historyDL.Rows.Count != 0)
            {
                foreach (DataRow row in historyDL.Rows)
                {
                    EN.FuelConsumption DL = new EN.FuelConsumption();

                    DL.id = Convert.ToString(row["ID"]);
                    DL.IssueDate = Convert.ToString(row["Issue Date"]);
                    DL.Costcenter = Convert.ToString(row["Cost Center"]);
                    DL.drivername = Convert.ToString(row["Driver Name"]);
                    DL.trailername = Convert.ToString(row["Vehicle No"]);
                    DL.fuelType = Convert.ToString(row["Fuel Type"]);
                    DL.BalQty = Convert.ToString(row["Qty"]);
                    DL.issueQty = Convert.ToString(row["Issue Qty"]);
                    DL.lastReading = Convert.ToString(row["Last Reading"]);
                    DL.currentReading = Convert.ToString(row["Current Reading"]);
                    DL.AddedBy = Convert.ToString(row["Added By"]);
                    DL.btnClass = Convert.ToString(row["btnClass"]);
                    DL.SlipNo = Convert.ToString(row["SlipNo"]);
                    DL.CostCenterFor = Convert.ToString(row["CostCenterFor"]);
                    DL.CostCenterFid = Convert.ToString(row["CostCenterForid"]);
                    DL.workyear = Convert.ToString(row["Workyear"]);
                    DL.VehicleTypeName = Convert.ToString(row["VehicleType"]);

                    DL.StockSLipNo = Convert.ToString(row["stockslipno"]);
                    DL.ReceiveQty = Convert.ToString(row["Received Qty"]);
                    DL.Rate = Convert.ToString(row["Rate"]);
                    DL.Amount = Convert.ToString(row["Amount"]);
                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }
        public EN.FuelConsumption REPrintFuelConsumption(string id)
        {
            EN.FuelConsumption passingDL = new EN.FuelConsumption();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.REPrintFuelConsumption(id);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    // EN.FuelConsumption passingDL = new EN.FuelConsumption();
                    passingDL.fuelRefNo = Convert.ToString(row["fuelRefNo"]);
                    passingDL.IssueDate = Convert.ToString(row["IssueDate"]);
                    passingDL.trailername = Convert.ToString(row["trailername"]);
                    passingDL.drivername = Convert.ToString(row["drivername"]);
                    passingDL.BalQty = Convert.ToString(row["balQty"]);
                    passingDL.issueQty = Convert.ToString(row["issueQty"]);
                    passingDL.fuelType = Convert.ToString(row["fuelType"]);
                    passingDL.lastReading = Convert.ToString(row["lastReading"]);
                    passingDL.currentReading = Convert.ToString(row["currentReading"]);
                    passingDL.CostCenterFor = Convert.ToString(row["Centername"]);
                    passingDL.Differencereading = Convert.ToString(row["differencereading"]);
                    passingDL.Fueltype = Convert.ToString(row["fuelType"]);
                    passingDL.VehicleTypeName = Convert.ToString(row["VehicleType"]);

                }
            }
            return passingDL;
        }

        public string SaveICDInternalDriverName(string txtDriver)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = trackerdatamanager.SaveICDInternalDriverName(txtDriver);
            if (ExpensesDL != null)
            {
                message = Convert.ToString(ExpensesDL.Rows[0]["record"]);
            }


            return message;
        }

        public EN.VehicleActivityInOut PrintVehicleInout()
        {
            EN.VehicleActivityInOut passingDL = new EN.VehicleActivityInOut();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.PrintVehicleInout();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    // EN.FuelConsumption passingDL = new EN.FuelConsumption();
                    passingDL.ActivityDate = Convert.ToString(row["ActivityDate"]);
                    passingDL.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    passingDL.FromLocation = Convert.ToString(row["FromLocation"]);
                    passingDL.ToLocation = Convert.ToString(row["ToLocation"]);
                    passingDL.Activity = Convert.ToString(row["Activity"]);
                    passingDL.partyName = Convert.ToString(row["partyName"]);
                    passingDL.Drivername = Convert.ToString(row["Drivername"]);
                    passingDL.Drivercontact = Convert.ToString(row["Drivercontact"]);
                    passingDL.ContainerNo = Convert.ToString(row["Container No"]);


                }
            }
            return passingDL;
        }


        public EN.VehicleActivityInOut REPrintVehicleInout(string Entryid)
        {
            EN.VehicleActivityInOut passingDL = new EN.VehicleActivityInOut();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.REPrintVehicleInout(Entryid);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    // EN.FuelConsumption passingDL = new EN.FuelConsumption();
                    passingDL.ActivityDate = Convert.ToString(row["ActivityDate"]);
                    passingDL.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    passingDL.FromLocation = Convert.ToString(row["FromLocation"]);
                    passingDL.ToLocation = Convert.ToString(row["ToLocation"]);
                    passingDL.Activity = Convert.ToString(row["Activity"]);
                    passingDL.partyName = Convert.ToString(row["partyName"]);
                    passingDL.Drivername = Convert.ToString(row["Drivername"]);
                    passingDL.Drivercontact = Convert.ToString(row["Drivercontact"]);
                    passingDL.ContainerNo = Convert.ToString(row["Container No"]);


                }
            }
            return passingDL;
        }
        public string GetTrailerStatus(string TrailerName)
        {
            string message = "";
            DataTable Dt = new DataTable();
            Dt = trackerdatamanager.GetVehicleStatus(TrailerName);
            if (Dt != null)
            {
                try
                {
                    var message1 = Convert.ToString(Dt.Rows[0]["Status"]);
                    message = message1;
                }
                catch (Exception ex)
                {
                    ;
                    return message = "";
                }

            }
            return message;
        }
        public List<EN.VehicleInOutEntryEntitiessummary> GetTrailerLastDetails(string trailername)
        {
            List<EN.VehicleInOutEntryEntitiessummary> DriverDL = new List<EN.VehicleInOutEntryEntitiessummary>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.GetVehicleInOutLatestDetails(trailername);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.VehicleInOutEntryEntitiessummary PortsList = new EN.VehicleInOutEntryEntitiessummary();
                    PortsList.TransDate = Convert.ToString(row["Trans Date"]);
                    PortsList.Trailerno = Convert.ToString(row["Vehicle No"]);
                    PortsList.containerno = Convert.ToString(row["Container No."]);
                    PortsList.status = Convert.ToString(row["Status"]);
                    PortsList.Activity = Convert.ToString(row["Activity"]);
                    PortsList.fromid = Convert.ToString(row["From Location"]);
                    PortsList.toid = Convert.ToString(row["To Location"]);
                    DriverDL.Add(PortsList);
                }
            }
            return DriverDL;
        }
        public List<EN.VehicleActivityInOut> GetVehicleInoutSummary(string Fromdate, string Todate)
        {
            DataTable historyDL = new DataTable();
            historyDL = trackerdatamanager.GetVehicleInoutSummary(Fromdate, Todate);
            List<EN.VehicleActivityInOut> VehicleActivityInOut = new List<EN.VehicleActivityInOut>();

            if (historyDL.Rows.Count != 0)
            {
                foreach (DataRow row in historyDL.Rows)
                {
                    EN.VehicleActivityInOut DL = new EN.VehicleActivityInOut();

                    DL.SrNo = Convert.ToInt32(row["EntryID"]);
                    DL.ActivityDate = Convert.ToString(row["ActivityDate"]);
                    DL.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    DL.FromLocation = Convert.ToString(row["FromLocation"]);
                    DL.ToLocation = Convert.ToString(row["ToLocation"]);
                    DL.Activity = Convert.ToString(row["Activity"]);
                    DL.PartyName = Convert.ToString(row["PartyName"]);
                    DL.UserName = Convert.ToString(row["UserName"]);
                    DL.btnClass = Convert.ToString(row["btnClass"]);
                    DL.Containercount = Convert.ToString(row["containercount"]);
                    DL.Inout = Convert.ToString(row["Inout"]);
                    DL.ContainerNo = Convert.ToString(row["containerno"]);
                    DL.ActivityCycle = Convert.ToString(row["Cycle"]);


                    VehicleActivityInOut.Add(DL);

                }

            }
            return VehicleActivityInOut;


        }
        //code end by manish
        // Code By Prashant

        public List<EN.ScanTypeEntities> getScanType()
        {
            List<EN.ScanTypeEntities> ScanTypeDL = new List<EN.ScanTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "scan_type";
            string Column = "ScanID,ScanType";
            string Condition = "";
            string OrderBy = "ScanType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ScanTypeEntities ScanList = new EN.ScanTypeEntities();
                    ScanList.ScanID = Convert.ToInt32(row["ScanID"]);
                    ScanList.ScanType = Convert.ToString(row["ScanType"]);
                    ScanTypeDL.Add(ScanList);
                }
            }
            return ScanTypeDL;
        }

        public List<EN.VehicleTypeEntities> getVehicleTypeDetails()
        {
            List<EN.VehicleTypeEntities> VehicleTypeDL = new List<EN.VehicleTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "VehicleType_M";
            string Column = "VehicleTypeID,VehicleType";
            string Condition = "";
            string OrderBy = "VehicleType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.VehicleTypeEntities ScanList = new EN.VehicleTypeEntities();
                    ScanList.VehicleTypeID = Convert.ToInt32(row["VehicleTypeID"]);
                    ScanList.VehicleType = Convert.ToString(row["VehicleType"]);
                    VehicleTypeDL.Add(ScanList);
                }
            }
            return VehicleTypeDL;
        }

        public List<EN.DriverTypeEntities> getDriverTypeDetails()
        {
            List<EN.DriverTypeEntities> VehicleTypeDL = new List<EN.DriverTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "Driver_type";
            string Column = "DrivertypeID,DriverType";
            string Condition = "";
            string OrderBy = "DriverType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DriverTypeEntities ScanList = new EN.DriverTypeEntities();
                    ScanList.DriverID = Convert.ToInt32(row["DrivertypeID"]);
                    ScanList.DriverType = Convert.ToString(row["DriverType"]);
                    VehicleTypeDL.Add(ScanList);
                }
            }
            return VehicleTypeDL;
        }
        public List<EN.UploadVoucherTariffEntities> UpdateVoucherTraiffDetails(DataTable VOucherTraifftDT, Int32 userId)
        {
            //EN.DriverPaymentReco objdriverpaymententities = new EN.DriverPaymentReco();
            List<EN.UploadVoucherTariffEntities> VOucherList = new List<EN.UploadVoucherTariffEntities>();
            if (VOucherTraifftDT != null)
            {
                int count = 1;
                foreach (DataRow row in VOucherTraifftDT.Rows)
                {
                    EN.UploadVoucherTariffEntities DPDTList = new EN.UploadVoucherTariffEntities();
                    DPDTList.SrNo = Convert.ToString(count++);
                    DPDTList.Transporter = Convert.ToString(row["Transpoter"]);
                    DPDTList.EffectiveFrom = Convert.ToString(row["Effective From"]);
                    DPDTList.EffectiveUpto = Convert.ToString(row["Effective Upto"]);
                    DPDTList.Activity = Convert.ToString(row["Activity"]);
                    DPDTList.InOut = Convert.ToString(row["In/OUT"]);
                    DPDTList.ContainerTYpe = Convert.ToString(row["Container Type"]);
                    DPDTList.Size = Convert.ToString(row["Size"]);
                    DPDTList.FromLocation = Convert.ToString(row["From Location"]);
                    DPDTList.TOlocation = Convert.ToString(row["To Location"]);
                    DPDTList.Passing = Convert.ToString(row["Passing"]);
                    DPDTList.TrailerTYpe = Convert.ToString(row["Trailer Type"]);
                    DPDTList.Fuel = Convert.ToString(row["Fuel (Liter)"]);
                    DPDTList.Amount1 = Convert.ToString(row["Amount1 (Cash)"]);
                    DPDTList.Amount2 = Convert.ToString(row["Amount2 (Bank)"]);
                    VOucherList.Add(DPDTList);
                }
            }
            return VOucherList;
        }

        public string VoucherValidation(DataTable VoucherDetails)
        {
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";

            if (VoucherDetails != null)
            {
                foreach (DataRow row in VoucherDetails.Rows)
                {
                    DataSet PortsDS = new DataSet();
                    PortsDS = trackerdatamanager.GetVOucherUploadValidation(Convert.ToString(row["Activity"]), Convert.ToString(row["FromLocation"]), Convert.ToString(row["TOlocation"]));
                    if (PortsDS.Tables[0].Rows.Count == 0)
                    {
                        if (message == "")
                        {
                            message = "Following Activity not found in database: \n" + Convert.ToString(row["Activity"]);
                        }
                        else
                        if (Convert.ToString(row["Activity"]).Contains(message))
                        {
                        }
                        else
                        {
                            message += "," + Convert.ToString(row["Activity"]);
                        }
                    }
                    if (PortsDS.Tables[1].Rows.Count == 0)
                    {
                        if (message1 == "")
                        {
                            message1 = "Following From Location not found in database: \n" + Convert.ToString(row["FromLocation"]);
                        }
                        else
                        {
                            if (Convert.ToString(row["FromLocation"]).Contains(message1))
                            {
                            }
                            else
                            {
                                message1 += "," + Convert.ToString(row["FromLocation"]);
                            }
                        }
                    }
                    if (PortsDS.Tables[2].Rows.Count == 0)
                    {
                        if (message3 == "")
                        {
                            message3 = "Following To Location not found in database: \n" + Convert.ToString(row["TOlocation"]);
                        }
                        else
                        {
                            if (Convert.ToString(row["TOlocation"]).Contains(message3))
                            {
                            }
                            else
                            {
                                message3 += "," + Convert.ToString(row["TOlocation"]);
                            }
                        }
                    }
                }
                if ((message != "") || (message1 != "") || (message3 != ""))
                {
                    message2 = message + "\n" + message1 + "\n" + message3;
                }
            }
            return message2;
        }




        public EN.UploadVoucherTariffEntities SaveVOucherLIstList(DataTable Itemdata, int UserID, DateTime entrydate)
        {
            //string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("UserID", UserID);
            parameterList.Add("entryDate", entrydate);
            DataSet ds = new DataSet();

            ds = db.AddTypeRoadPlanningTableData("USP_INSERTVOUCHERTRAIFFUPLOAD", parameterList, Itemdata, "PT_VoucherTraiffUpload", "@PT_VoucherTraiffUpload");


            //int i = db.SaveContainerList("USP_TainPlanningInsertContainerList", parameterList, Itemdata);

            // VendorDataDL = objgetMyREQDBManager.ApprovedStatusList(OrderID, companyid);

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            EN.UploadVoucherTariffEntities VoucherdetailsLIst = new EN.UploadVoucherTariffEntities();

            dt = ds.Tables[0];
            dt1 = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                VoucherdetailsLIst.validationMessage = Convert.ToString(dt.Rows[0][0]);
                VoucherdetailsLIst.TranspoterList = Convert.ToString(dt1.Rows[0][0]);
            }
            // Message = "Record saved successfully";
            return VoucherdetailsLIst;


            // Message = "Record saved successfully";
            ///return Message;



        }
        public string CancelVoucherTariff(DataTable dataTable, int userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            // parameterList.Add("Voucherno", Voucherno);s
            parameterList.Add("Userid", userid);
            int i = db.AddTypeTableData("USP_CancelVouchertariffDetails", parameterList, dataTable, "PT_CancelVoucherTariff", "@PT_CancelVoucherTariff");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public List<EN.ContainerCountEntities> GetContainerCount()
        {
            List<EN.ContainerCountEntities> DriverDL = new List<EN.ContainerCountEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.GetCountainerCount();
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ContainerCountEntities PortsList = new EN.ContainerCountEntities();
                    PortsList.ContainerCountID = Convert.ToInt16(row["MovcountID"]);
                    PortsList.ContainerCountSize = Convert.ToString(row["Movcount"]);

                    DriverDL.Add(PortsList);
                }
            }
            return DriverDL;
        }

        public string GetVehiclemOvementType(string TrailerName, string InOut)
        {
            string message = "";
            DataTable Dt = new DataTable();
            Dt = trackerdatamanager.GetTrailerMovementDetails(TrailerName, InOut);
            if (Dt.Rows.Count > 0)
            {
                message = Convert.ToString(Dt.Rows[0]["MSG"]);

            }
            else
            {
                message = "0";
            }
            return message;
        }

        public List<EN.CostCenterFor> getCostCenterFor()
        {
            List<EN.CostCenterFor> CostCenterDL = new List<EN.CostCenterFor>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetCostCenterFor();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CostCenterFor COstist = new EN.CostCenterFor();
                    COstist.CodeCenterID = Convert.ToString(row["CenterID"]);
                    COstist.CodeCenterType = Convert.ToString(row["CenterName"]);
                    CostCenterDL.Add(COstist);
                }
            }
            return CostCenterDL;
        }


        public List<EN.FuelStockSummary> GetFuelStockSummaryDetails(string cmbFueltype, string FromDate, string ToDate, string FuelFor)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetFuelStockSummary1(cmbFueltype, FromDate, ToDate, FuelFor);
            List<EN.FuelStockSummary> ContainerList = new List<EN.FuelStockSummary>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.FuelStockSummary Tues1 = new EN.FuelStockSummary();
                    Tues1.SrNo = Convert.ToInt32(Count++);
                    Tues1.EntryDate = Convert.ToString(row["Entry Date"]);
                    Tues1.fuelRefNo = Convert.ToString(row["fuelRefNo"]);
                    Tues1.CostCenter = Convert.ToString(row["Cost Center"]);
                    Tues1.vendorname = Convert.ToString(row["Vendor Name"]);
                    Tues1.vehicleNo = Convert.ToString(row["Vehicle No"]);
                    Tues1.fuelType = Convert.ToString(row["Fuel Type"]);
                    Tues1.qty = Convert.ToString(row["Qty"]);
                    Tues1.Rate = Convert.ToString(row["Rate"]);
                    Tues1.username = Convert.ToString(row["Added By"]);
                    Tues1.fuelregid = Convert.ToString(row["fuelregid"]);
                    Tues1.btnClass = Convert.ToString(row["btnClass"]);
                    Tues1.CostCenterFor = Convert.ToString(row["centername"]);
                    Tues1.FuelFor = Convert.ToString(row["FuelFor"]);
                    Tues1.BillNo = Convert.ToString(row["Bill No"]);
                    Tues1.BillAmt = Convert.ToString(row["Bill Amount"]);

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        // code start by rahul 02-11-2019
        public EN.IGMManualEntities GetDropDownListIGMManual()
        {
            EN.IGMManualEntities IGMManual = new EN.IGMManualEntities();
            DataSet IGMManualDL = new DataSet();
            IGMManualDL = trackerdatamanager.getDropDownListIGMManual();

            //List<EN.ConsigneeIGMManualEntities> ConIGMList = new List<EN.ConsigneeIGMManualEntities>();
            //List<EN.NConsigneeIGMManualEntities> NConIGMList = new List<EN.NConsigneeIGMManualEntities>();
            List<EN.PackageIGMManualEntities> PackageIGMList = new List<EN.PackageIGMManualEntities>();

            //if (IGMManualDL.Tables[0].Rows.Count != 0)
            //{
            //    foreach (DataRow row in IGMManualDL.Tables[0].Rows)
            //    {
            //        EN.ConsigneeIGMManualEntities ConIGM = new EN.ConsigneeIGMManualEntities();
            //        ConIGM.ImporterID = Convert.ToInt16(row["ImporterID"]);
            //        ConIGM.ImporterName = Convert.ToString(row["ImporterName"]);
            //        ConIGMList.Add(ConIGM);
            //    }
            //}
            //if (IGMManualDL.Tables[0].Rows.Count != 0)
            //{
            //    foreach (DataRow row in IGMManualDL.Tables[0].Rows)
            //    {
            //        EN.NConsigneeIGMManualEntities NConIGM = new EN.NConsigneeIGMManualEntities();
            //        NConIGM.ImporterID = Convert.ToInt32(row["ImporterID"]);
            //        NConIGM.ImporterName = Convert.ToString(row["ImporterName"]);
            //        NConIGMList.Add(NConIGM);
            //    }
            //}
            if (IGMManualDL.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow row in IGMManualDL.Tables[1].Rows)
                {
                    EN.PackageIGMManualEntities PackageIGM = new EN.PackageIGMManualEntities();
                    PackageIGM.CodeID = Convert.ToInt32(row["CodeID"]);
                    PackageIGM.Package = Convert.ToString(row["Package"]);
                    PackageIGMList.Add(PackageIGM);
                }
            }
            //IGMManual.ConsigneeIGMList = ConIGMList;
            //IGMManual.NConsigneeIGMList = NConIGMList;
            IGMManual.PackageIGMList = PackageIGMList;

            return IGMManual;
        }
        public List<EN.ConsigneeIGMManualEntities> getAutoConsigneeIGM(string Prefix)
        {
            List<EN.ConsigneeIGMManualEntities> ConsigneeDL = new List<EN.ConsigneeIGMManualEntities>();
            DataTable ConsigneeDT = new DataTable();

            ConsigneeDT = trackerdatamanager.getAutoConsigneeIGM(Prefix);
            if (ConsigneeDT != null)
            {
                if (ConsigneeDT.Rows.Count > 0)
                {
                    foreach (DataRow row in ConsigneeDT.Rows)
                    {
                        EN.ConsigneeIGMManualEntities ConsigneeList = new EN.ConsigneeIGMManualEntities();
                        ConsigneeList.ImporterID = Convert.ToInt32(row["ImporterID"]);
                        ConsigneeList.ImporterName = Convert.ToString(row["ImporterName"]);
                        ConsigneeList.ImporterAddress = Convert.ToString(row["Con_IGMAddress"]);

                        ConsigneeDL.Add(ConsigneeList);
                    }
                }
                else
                {
                    EN.ConsigneeIGMManualEntities ConsigneeList = new EN.ConsigneeIGMManualEntities();
                    ConsigneeList.ImporterID = 0;
                    ConsigneeList.ImporterName = "No Data Found";

                    ConsigneeDL.Add(ConsigneeList);
                }

            }
            return ConsigneeDL;
        }

        public List<EN.POL> getAutoPOL(string Prefix)
        {
            List<EN.POL> POLDL = new List<EN.POL>();
            DataTable POLDT = new DataTable();

            POLDT = trackerdatamanager.GetPOLForAutoComplete(Prefix);
            if (POLDT != null)
            {
                if (POLDT.Rows.Count > 0)
                {
                    foreach (DataRow row in POLDT.Rows)
                    {
                        EN.POL POLList = new EN.POL();
                        POLList.PODName = Convert.ToString(row["IGM_Origin"]);
                        POLDL.Add(POLList);
                    }
                }
                else
                {
                    EN.POL POLList = new EN.POL();
                    POLList.PODName = "No Data Found";
                    POLDL.Add(POLList);
                }

            }
            return POLDL;
        }
        //code end by rahul 02-11-2019

        //code start by rahul 05-11-2019
        public List<EN.IGMManualEntities> getSearchIGMManualList(string Search, string IGMNo, string ItemNo, string ContainerNo, string ViaNo)
        {
            List<EN.IGMManualEntities> IGMManualDL = new List<EN.IGMManualEntities>();
            DataTable IGMManualDT = new DataTable();

            IGMManualDT = trackerdatamanager.getSeachIGMManualData(Search, IGMNo, ItemNo, ContainerNo, ViaNo);
            if (IGMManualDT != null)
            {
                if (IGMManualDT.Rows.Count > 0)
                {
                    foreach (DataRow row in IGMManualDT.Rows)
                    {
                        EN.IGMManualEntities IGMManualList = new EN.IGMManualEntities();
                        IGMManualList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                        IGMManualList.Size = Convert.ToInt16(row["Size"]);
                        IGMManualList.ContainerType = Convert.ToString(row["ContainerType"]);
                        IGMManualList.JODate = Convert.ToString(row["JODate"]);
                        IGMManualList.IGMNo = Convert.ToString(row["IGMNo"]);
                        IGMManualList.ItemNo = Convert.ToString(row["ItemNo"]);
                        IGMManualList.JONo = Convert.ToString(row["JONo"]);
                        IGMManualList.LineName = Convert.ToString(row["LineName"]);
                        IGMManualList.BtnEditCss = Convert.ToString(row["BtnEditCss"]);
                        IGMManualList.BtnAddCss = Convert.ToString(row["BtnAddCss"]);

                        IGMManualDL.Add(IGMManualList);
                    }
                }

            }
            return IGMManualDL;
        }
        public EN.IGMManualEntities getIGMDetailsListforEdit(string IGMNo, string ItemNo)
        {
            EN.IGMManualEntities IGMManualList = new EN.IGMManualEntities();
            DataSet IGMManualDL = new DataSet();
            IGMManualDL = trackerdatamanager.getIGMDataforEdit(IGMNo, ItemNo);

            List<EN.ConDetailsIGMManualEntities> ConDetailsList = new List<EN.ConDetailsIGMManualEntities>();
            if (IGMManualDL.Tables[0] != null)
            {
                if (IGMManualDL.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in IGMManualDL.Tables[0].Rows)
                    {
                        EN.ConDetailsIGMManualEntities ContainerList = new EN.ConDetailsIGMManualEntities();
                        ContainerList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                        ContainerList.Size = Convert.ToInt16(row["Size"]);
                        ContainerList.ContainerType = Convert.ToString(row["ContainerType"]);
                        ContainerList.Cargotype = Convert.ToString(row["Cargotype"]);
                        ContainerList.InDate = Convert.ToString(row["InDate"]);
                        ContainerList.SealNo = Convert.ToString(row["SealNo"]);
                        ContainerList.IGMQty = Convert.ToString(row["IGMQty"]);
                        ContainerList.IGMWt = Convert.ToString(row["IGMWt"]);
                        IGMManualList.JONo = Convert.ToString(row["JoNo"]);
                        ConDetailsList.Add(ContainerList);
                    }
                }
            }
            IGMManualList.ConDetailsIGMManualEntities = ConDetailsList;
            if (IGMManualDL.Tables[1] != null)
            {
                if (IGMManualDL.Tables[1].Rows.Count > 0)
                {
                    IGMManualList.IGMNo = Convert.ToString(IGMManualDL.Tables[1].Rows[0]["IGMNo"]);
                    IGMManualList.ItemNo = Convert.ToString(IGMManualDL.Tables[1].Rows[0]["ItemNo"]);
                    IGMManualList.IGM_BLNo = Convert.ToString(IGMManualDL.Tables[1].Rows[0]["IGM_BLNo"]);
                    IGMManualList.IGM_BLDate = Convert.ToDateTime(IGMManualDL.Tables[1].Rows[0]["IGM_BLDate"]).ToString("dd MMM yyyy HH:mm");
                    IGMManualList.Consignee = Convert.ToString(IGMManualDL.Tables[1].Rows[0]["Consignee"]);
                    IGMManualList.Con_IGMAddress = Convert.ToString(IGMManualDL.Tables[1].Rows[0]["Con_IGMAddress"]);
                    IGMManualList.NConsignee = Convert.ToString(IGMManualDL.Tables[1].Rows[0]["NConsignee"]);
                    IGMManualList.NCon_IGMAddress = Convert.ToString(IGMManualDL.Tables[1].Rows[0]["NCon_IGMAddress"]);
                    IGMManualList.IGM_GoodsDesc = Convert.ToString(IGMManualDL.Tables[1].Rows[0]["IGM_GoodsDesc"]);
                    IGMManualList.PKGType = Convert.ToInt16(IGMManualDL.Tables[1].Rows[0]["PKGType"]);
                    IGMManualList.TotIGMQty = Convert.ToDecimal(IGMManualDL.Tables[1].Rows[0]["TotIGMQty"]);
                    IGMManualList.TotIGMWt = Convert.ToDecimal(IGMManualDL.Tables[1].Rows[0]["TotIGMWt"]);
                    IGMManualList.POL = Convert.ToString(IGMManualDL.Tables[1].Rows[0]["IGM_Origin"]);
                }
            }

            return IGMManualList;
        }
        public EN.IGMManualEntities getIGMDetailsListforAdd(string JONo)
        {
            EN.IGMManualEntities IGMManualList = new EN.IGMManualEntities();
            DataTable IGMManualDL = new DataTable();
            IGMManualDL = trackerdatamanager.getIGMDataforAdd(JONo);

            List<EN.ConDetailsIGMManualEntities> ConDetailsList = new List<EN.ConDetailsIGMManualEntities>();
            if (IGMManualDL != null)
            {
                if (IGMManualDL.Rows.Count > 0)
                {
                    foreach (DataRow row in IGMManualDL.Rows)
                    {
                        EN.ConDetailsIGMManualEntities ContainerList = new EN.ConDetailsIGMManualEntities();
                        ContainerList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                        ContainerList.Size = Convert.ToInt16(row["Size"]);
                        ContainerList.ContainerType = Convert.ToString(row["ContainerType"]);
                        ContainerList.Cargotype = Convert.ToString(row["Cargotype"]);
                        ContainerList.InDate = Convert.ToString(row["InDate"]);
                        ContainerList.SealNo = Convert.ToString(row["SealNo"]);
                        ContainerList.IGMQty = Convert.ToString(row["IGMQty"]);
                        ContainerList.IGMWt = Convert.ToString(row["IGMWt"]);

                        ConDetailsList.Add(ContainerList);
                    }
                }
            }
            IGMManualList.JONo = Convert.ToString(JONo);
            IGMManualList.ConDetailsIGMManualEntities = ConDetailsList;

            return IGMManualList;
        }
        //code end by rahul 05-11-2019

        //code start by rahul 06-11-2019
        public string SaveIGMManual(DataTable WOdata, string IGMNo, string ItemNo, string IGM_BLNo, string IGM_BLDate, string Consignee, string Con_IGMAddress, string NConsignee, string NCon_IGMAddress, string IGM_GoodsDesc, string TotIGMQty, string PKGType, string TotIGMWt, string JONo, int UserID,string POL)
        {
            string Message = "";
            DataTable IGMManualDT = new DataTable();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("IGMNo", IGMNo);
            parameterList.Add("ItemNo", ItemNo);
            parameterList.Add("IGM_BLNo", IGM_BLNo);
            parameterList.Add("IGM_BLDate", Convert.ToDateTime(IGM_BLDate).ToString("yyyy-MM-dd HH:mm"));
            parameterList.Add("Consignee", Consignee);
            parameterList.Add("Con_IGMAddress", Con_IGMAddress);
            parameterList.Add("NConsignee", NConsignee);
            parameterList.Add("NCon_IGMAddress", NCon_IGMAddress);
            parameterList.Add("IGM_GoodsDesc", IGM_GoodsDesc);
            parameterList.Add("IGM_MT_Wt", TotIGMWt);
            parameterList.Add("IGM_Qty", TotIGMQty);
            parameterList.Add("IGM_PackType", PKGType);
            parameterList.Add("JONO", JONo);
            parameterList.Add("userid", UserID);
            parameterList.Add("POL", POL);

            IGMManualDT = db.DataTableAddTypeTable("USP_INSERT_N_UPDATE_IGM_DETAILS_FROM_IGM_MANUAL", parameterList, WOdata, "PT_IGMMANUALDETS", "@PT_IGMMANUALDETS");

            if (IGMManualDT != null)
            {
                if (IGMManualDT.Rows.Count > 0)
                {
                    if (Convert.ToString(IGMManualDT.Rows[0]["message"]) == "1")
                    {
                        Message = "1";
                    }
                    else
                    {
                        Message = "0";
                    }
                }
                else
                {
                    Message = "0";
                }
            }
            else
            {
                Message = "0";
            }
            return Message;
        }
        //code end by rahul 06-11-2019



        public List<EN.ContainerCountEntities> GetContainerType()
        {
            List<EN.ContainerCountEntities> DriverDL = new List<EN.ContainerCountEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.GetCountainerType();
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ContainerCountEntities PortsList = new EN.ContainerCountEntities();
                    PortsList.ContainerCountID = Convert.ToInt16(row["CargotypeID"]);
                    PortsList.ContainerCountSize = Convert.ToString(row["Cargotype"]);

                    DriverDL.Add(PortsList);
                }
            }
            return DriverDL;
        }


        public List<EN.DriversEntities> getICD_Internal_Driver()
        {
            List<EN.DriversEntities> DriversDL = new List<EN.DriversEntities>();
            DataTable dt = new DataTable();
            string Table = "ICD_Internal_Driver";
            string Column = "driverid,drivername";
            string Condition = "";
            string OrderBy = "drivername";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DriversEntities DriversList = new EN.DriversEntities();
                    DriversList.driverID = Convert.ToInt32(row["driverid"]);
                    DriversList.driverName = Convert.ToString(row["drivername"]);
                    DriversDL.Add(DriversList);
                }
            }
            return DriversDL;
        }


        public List<EN.PurposeEntites> getVehiclePurpose()
        {
            List<EN.PurposeEntites> DriversDL = new List<EN.PurposeEntites>();
            DataTable dt = new DataTable();
            string Table = "fuel_purpose";
            string Column = "PurposeID,Purpose";
            string Condition = "";
            string OrderBy = "Purpose";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.PurposeEntites DriversList = new EN.PurposeEntites();
                    DriversList.PurposeID = Convert.ToInt32(row["PurposeID"]);
                    DriversList.PurposeName = Convert.ToString(row["Purpose"]);
                    DriversDL.Add(DriversList);
                }
            }
            return DriversDL;
        }

        public List<EN.VehicleSubGoupEntites> getVehicleSubgroup()
        {
            List<EN.VehicleSubGoupEntites> DriversDL = new List<EN.VehicleSubGoupEntites>();
            DataTable dt = new DataTable();
            string Table = "vehicle_subgroup";
            string Column = "SubgroupID,subgroup";
            string Condition = "";
            string OrderBy = "subgroup";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.VehicleSubGoupEntites DriversList = new EN.VehicleSubGoupEntites();
                    DriversList.GroupID = Convert.ToInt32(row["SubgroupID"]);
                    DriversList.Group = Convert.ToString(row["subgroup"]);
                    DriversDL.Add(DriversList);
                }
            }
            return DriversDL;
        }


        public List<EN.TrailersEntities> ICDGetDriverno(string TrailerNo)
        {
            List<EN.TrailersEntities> PortsDL = new List<EN.TrailersEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.GetICDDriverNo(TrailerNo);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TrailersEntities PortsList = new EN.TrailersEntities();
                    PortsList.DriverID = Convert.ToInt32(row["Driverid"]);
                    PortsList.trailername = Convert.ToString(row["drivername"]);

                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }

        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public EN.DischargeDateContainerDetails GetDropDownListWorkOrder()
        {
            EN.DischargeDateContainerDetails External = new EN.DischargeDateContainerDetails();
            DataSet ExternalDl = new DataSet();

            ExternalDl = TrackerManager.getDropDownListExternal();

            List<EN.portEntities> portList = new List<EN.portEntities>();
            List<EN.lineEntites> lineList = new List<EN.lineEntites>();
            List<EN.VesselEntities> VesseslList = new List<EN.VesselEntities>();
            List<EN.TypeEntities> TypeList = new List<EN.TypeEntities>();
            List<EN.FromEntities> FromList = new List<EN.FromEntities>();
            List<EN.ToEntities> ToList = new List<EN.ToEntities>();
            List<EN.SizeEntities> SizeList = new List<EN.SizeEntities>();


            if (ExternalDl.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[0].Rows)
                {
                    EN.portEntities List = new EN.portEntities();
                    List.portID = Convert.ToInt32(row["PortID"]);
                    List.ports = Convert.ToString(row["PortName"]);
                    portList.Add(List);
                }

            }
            if (ExternalDl.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[1].Rows)
                {
                    EN.lineEntites linlist = new EN.lineEntites();
                    linlist.lineid = Convert.ToInt32(row["SLID"]);
                    linlist.LineName = Convert.ToString(row["SLName"]);
                    lineList.Add(linlist);
                }
            }
            if (ExternalDl.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[2].Rows)
                {
                    EN.VesselEntities vessel = new EN.VesselEntities();
                    vessel.vesselid = Convert.ToInt32(row["VesselID"]);
                    vessel.vesselName = Convert.ToString(row["VesselName"]);
                    VesseslList.Add(vessel);
                }

            }
            if (ExternalDl.Tables[3].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[3].Rows)
                {
                    EN.TypeEntities Type = new EN.TypeEntities();
                    Type.TypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    Type.Type = Convert.ToString(row["ContainerType"]);
                    TypeList.Add(Type);
                }

            }
            if (ExternalDl.Tables[4].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[4].Rows)
                {
                    EN.FromEntities from = new EN.FromEntities();
                    from.fromid = Convert.ToInt32(row["LocationID"]);
                    from.From = Convert.ToString(row["location"]);
                    FromList.Add(from);
                }

            }

            if (ExternalDl.Tables[5].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[5].Rows)
                {
                    EN.ToEntities To = new EN.ToEntities();
                    To.Toid = Convert.ToInt32(row["LocationID"]);
                    To.To = Convert.ToString(row["location"]);
                    ToList.Add(To);
                }

            }

            if (ExternalDl.Tables[6].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[6].Rows)
                {
                    EN.SizeEntities size = new EN.SizeEntities();
                    size.Sizeid = Convert.ToInt32(row["ContainerSizeID"]);
                    size.Sizec = Convert.ToString(row["ContainerSize"]);
                    SizeList.Add(size);
                }

            }



            External.portList = portList;
            External.lineList = lineList;
            External.VesseslList = VesseslList;
            External.TypeList = TypeList;
            External.FromList = FromList;
            External.ToList = ToList;
            External.SizeList = SizeList;


            return External;
        }

        public string SaveExternal(DataTable Externaldata, string JoDate, string Criteria, string Line, string Vessel, string Port, string ViaNo, string CutOfdate, int UserID,string ShipmentNo,string JoNo)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("JoDate", Convert.ToDateTime(JoDate).ToString("yyyy-MM-dd HH:mm"));
            parameterList.Add("Criteria", Criteria);
            parameterList.Add("Line", Line);
            parameterList.Add("Vessel", Vessel);
            parameterList.Add("Port", Port);
            parameterList.Add("ViaNo", ViaNo);
            parameterList.Add("CutOfdate", Convert.ToDateTime(CutOfdate).ToString("yyyy-MM-dd HH:mm"));
            parameterList.Add("AddedBy", UserID);
            parameterList.Add("ShipmentNo", ShipmentNo);
            parameterList.Add("JoNo", JoNo);

            int i = db.AddTypeTableData("usp_insert_ext_empty_Jo", parameterList, Externaldata, "PT_ExternalmovementJobOrder", "@PT_ExternalmovementJobOrder");

            if (i > 0)
            {
                Message = "Record saved successfully";
            }
            else
            {
                Message = "Record not saved successfully. Try Again!";
            }
            return Message;
        }

        public string updatesTockBillNo(string BillNo, string AmountForBill, string FUelID, string QtyModify, string RateModify)
        {

            string message = "";
            DataTable InternalFuelStock = new DataTable();
            InternalFuelStock = trackerdatamanager.UpdateBIllNoforInternalstock(BillNo, AmountForBill, FUelID, QtyModify, RateModify);

            message = "Record saved successfully!";
            return message;

        }
        public List<EN.TraiffViewsEntities> GetContainerTypeDetails()
        {
            List<EN.TraiffViewsEntities> ContainerWiseRevenueDL = new List<EN.TraiffViewsEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.GetContainerTypeDetails();
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TraiffViewsEntities RevenueList = new EN.TraiffViewsEntities();
                    RevenueList.ContainerTypeID = Convert.ToString(row["Cargotypeid"]);
                    RevenueList.ContainertypeName = Convert.ToString(row["Cargotype"]);
                    ContainerWiseRevenueDL.Add(RevenueList);
                }
            }
            return ContainerWiseRevenueDL;
        }

        public List<EN.TraiffViewsEntities> GetAccountnameDetails()
        {
            List<EN.TraiffViewsEntities> ContainerWiseRevenueDL = new List<EN.TraiffViewsEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.Getimport_accountmasterDetails();
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TraiffViewsEntities RevenueList = new EN.TraiffViewsEntities();
                    RevenueList.AccountEntryID = Convert.ToString(row["Entryid"]);
                    RevenueList.AccountID = Convert.ToString(row["AccountID"]);
                    RevenueList.AccountName = Convert.ToString(row["AccountName"]);
                    ContainerWiseRevenueDL.Add(RevenueList);
                }
            }
            return ContainerWiseRevenueDL;
        }

        public List<EN.TraiffViewsEntities> GetTraiffDetails()
        {
            List<EN.TraiffViewsEntities> ContainerWiseRevenueDL = new List<EN.TraiffViewsEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.GetTraiffDetails();
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TraiffViewsEntities RevenueList = new EN.TraiffViewsEntities();
                    RevenueList.TraiffID = Convert.ToString(row["TariffID"]);
                    RevenueList.TraiffDesc = Convert.ToString(row["TariffDescription"]);
                    ContainerWiseRevenueDL.Add(RevenueList);
                }
            }
            return ContainerWiseRevenueDL;
        }

        public List<EN.TraiffViewsEntities> GetExportMovementtypeDetails()
        {
            List<EN.TraiffViewsEntities> ContainerWiseRevenueDL = new List<EN.TraiffViewsEntities>();
            DataTable PortsDT = new DataTable();
            int count = 1;
            PortsDT = trackerdatamanager.GetExportTypeDetails();
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TraiffViewsEntities RevenueList = new EN.TraiffViewsEntities();
                    RevenueList.ExportID = Convert.ToString(count++);
                    RevenueList.ExportName = Convert.ToString(row["Deliverytype"]);
                    ContainerWiseRevenueDL.Add(RevenueList);
                }
            }
            return ContainerWiseRevenueDL;
        }

        public List<EN.TraiffViewsEntities> GetBondDeliveryTypeDetails()
        {
            List<EN.TraiffViewsEntities> ContainerWiseRevenueDL = new List<EN.TraiffViewsEntities>();
            DataTable PortsDT = new DataTable();
            int count = 1;
            PortsDT = trackerdatamanager.GetBondTypeDetails();
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TraiffViewsEntities RevenueList = new EN.TraiffViewsEntities();
                    RevenueList.BondID = Convert.ToString(count++);
                    RevenueList.BondName = Convert.ToString(row["Bondtype"]);
                    ContainerWiseRevenueDL.Add(RevenueList);
                }
            }
            return ContainerWiseRevenueDL;
        }

        public List<EN.TraiffViewsEntities> GetDomesticDeliveryTypeDetails()
        {
            List<EN.TraiffViewsEntities> ContainerWiseRevenueDL = new List<EN.TraiffViewsEntities>();
            DataTable PortsDT = new DataTable();
            int count = 1;
            PortsDT = trackerdatamanager.GetDomesticTypeDetails();
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TraiffViewsEntities RevenueList = new EN.TraiffViewsEntities();
                    RevenueList.BondID = Convert.ToString(count++);
                    RevenueList.BondName = Convert.ToString(row["Bondtype"]);
                    ContainerWiseRevenueDL.Add(RevenueList);
                }
            }
            return ContainerWiseRevenueDL;
        }


        public List<EN.TraiffViewsEntities> GetBondTraiffDetails()
        {
            List<EN.TraiffViewsEntities> ContainerWiseRevenueDL = new List<EN.TraiffViewsEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.GetBondTraiffDetails();
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TraiffViewsEntities RevenueList = new EN.TraiffViewsEntities();
                    RevenueList.TraiffID = Convert.ToString(row["TariffID"]);
                    RevenueList.TraiffDesc = Convert.ToString(row["TariffDescription"]);
                    ContainerWiseRevenueDL.Add(RevenueList);
                }
            }
            return ContainerWiseRevenueDL;
        }

        public List<EN.TraiffViewsEntities> GetDomesticraiffDetails()
        {
            List<EN.TraiffViewsEntities> ContainerWiseRevenueDL = new List<EN.TraiffViewsEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.GetDomesticTraiffDetails();
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TraiffViewsEntities RevenueList = new EN.TraiffViewsEntities();
                    RevenueList.TraiffID = Convert.ToString(row["TariffID"]);
                    RevenueList.TraiffDesc = Convert.ToString(row["TariffDescription"]);
                    ContainerWiseRevenueDL.Add(RevenueList);
                }
            }
            return ContainerWiseRevenueDL;
        }
        public List<EN.ModifyInternalFuelConsumptionEntities> getSlipdetails(string ddlFuelType, string ddlCostCenter1)
        {
            List<EN.ModifyInternalFuelConsumptionEntities> PortsDL = new List<EN.ModifyInternalFuelConsumptionEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.GetStockSlipnodetails(ddlFuelType, ddlCostCenter1);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ModifyInternalFuelConsumptionEntities PortsList = new EN.ModifyInternalFuelConsumptionEntities();
                    PortsList.fuelRefNo = Convert.ToString(row["Slip No"]);


                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }

        public EN.ModifyInternalFuelConsumptionEntities GetStockSlipWiseFuel(string SLipNo, int Userid)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.GetStockSLipWiseFueldetails(SLipNo, Userid);
            EN.ModifyInternalFuelConsumptionEntities ContainerList = new EN.ModifyInternalFuelConsumptionEntities();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {

                    ContainerList.fuelqty = Convert.ToString(row["fuel"]);


                }

            }
            return ContainerList;
        }
    }
}
