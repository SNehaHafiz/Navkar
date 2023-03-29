using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Report
{
    public class Report
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
       
        public List<EN.PendingContainerAgainZeroJo> GetpendingContainer()
        {
            DataTable pendingContainerDL = new DataTable();
            pendingContainerDL = trackerdatamanager.getPendingContainerAgainzeroJo();
            List<EN.PendingContainerAgainZeroJo> pendingcontainer = new List<EN.PendingContainerAgainZeroJo>();

            if (pendingContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in pendingContainerDL.Rows)
                {
                    EN.PendingContainerAgainZeroJo pendingcontainerlist = new EN.PendingContainerAgainZeroJo();
                    pendingcontainerlist.SrNo = Convert.ToInt32(row["Sr No"]);
                    pendingcontainerlist.JONo = Convert.ToInt32(row["JO No"]);
                    pendingcontainerlist.ContainerNo = Convert.ToString(row["Container No"]);
                    pendingcontainerlist.SealNo = Convert.ToString(row["Seal No"]);
                    pendingcontainerlist.TruckNo = Convert.ToString(row["Truck No"]);
                    pendingcontainerlist.EIRNo = Convert.ToString(row["EIR No"]);
                    pendingcontainerlist.EIRDate = Convert.ToString(row["EIR Date"]);
                    pendingcontainerlist.InDate = Convert.ToString(row["In Date"]);
                    pendingcontainerlist.AddedBy = Convert.ToString(row["Added By"]);
                
                    pendingcontainer.Add(pendingcontainerlist);
                }

            }
            return pendingcontainer;
        }


        public List<EN.PendingContainersForJoUpdationsEntities> GetPendingContainerForJOUpdation()
        {
            DataTable pendingContainerDL = new DataTable();
            pendingContainerDL = trackerdatamanager.getPendingContainerForJo();
            List<EN.PendingContainersForJoUpdationsEntities> pendingcontainer = new List<EN.PendingContainersForJoUpdationsEntities>();

            if (pendingContainerDL.Rows.Count != 0)
            {
               
                foreach (DataRow row in pendingContainerDL.Rows)
                {
                    EN.PendingContainersForJoUpdationsEntities pendingcontainerlist = new EN.PendingContainersForJoUpdationsEntities();
                    pendingcontainerlist.SrNo = Convert.ToString(row["Sr No"]);
                    pendingcontainerlist.ContainerNo = Convert.ToString(row["Container No"]);
                    pendingcontainerlist.Size = Convert.ToInt32(row["Size"]);
                    pendingcontainerlist.FL = Convert.ToString(row["FL"]);
                    pendingcontainerlist.Type = Convert.ToString(row["Type"]);

                   
                    pendingcontainerlist.ISOCode = Convert.ToString(row["ISO Code"]);
                    pendingcontainerlist.CargoWt = Convert.ToString(row["Cargo Wt"]);
                    pendingcontainerlist.Igmno = Convert.ToString(row["IGM No"]);

                    pendingcontainerlist.Itemno = Convert.ToString(row["Item No"]);
                    pendingcontainerlist.SMTPNO = Convert.ToString(row["SMTP NO"]);
                    pendingcontainerlist.SMTPDate = Convert.ToString(row["SMTP Date"]);
                    pendingcontainerlist.Consignee = Convert.ToString(row["Consignee"]);
                    pendingcontainerlist.CargoDescriptions = Convert.ToString(row["Cargo Descriptions"]);
                    pendingcontainerlist.BLNumber = Convert.ToString(row["BL Number"]);
                    pendingcontainerlist.SealNo = Convert.ToString(row["Seal No"]);
                    pendingcontainerlist.UpdatedBy = Convert.ToString(row["Updated By"]);
                
                    pendingcontainer.Add(pendingcontainerlist);
                }

            }
            return pendingcontainer;
        }

        public List<EN.PendingContainersForJoUpdationsEntities> GetInventoryList(string AsonDate)
        {
            List<EN.PendingContainersForJoUpdationsEntities> InventoryList = new List<EN.PendingContainersForJoUpdationsEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetInventory(AsonDate);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.PendingContainersForJoUpdationsEntities INventoryDL = new EN.PendingContainersForJoUpdationsEntities();
                    INventoryDL.SrNo = Convert.ToString(row["Sr No"]);
                    INventoryDL.Activity = Convert.ToString(row["Activity"]);
                    INventoryDL.Status = Convert.ToString(row["Status"]);
                    INventoryDL.ContainerNo = Convert.ToString(row["Container No"]);
                    INventoryDL.Size = Convert.ToInt32(row["Size"]);

                    INventoryDL.Type = Convert.ToString(row["Type"]);
                    INventoryDL.InDate = Convert.ToString(row["In Date"]);
                    INventoryDL.LineName = Convert.ToString(row["Line Name"]);
                    INventoryDL.DwellDays = Convert.ToString(row["Dwell Days"]);
                    InventoryList.Add(INventoryDL);
                }
            }
            
            return InventoryList;
        }

        public List<EN.CriteriaEntities> GetCriteria()
        {
            DataTable dt = new DataTable();

            List<EN.CriteriaEntities> CriteriaList = new List<EN.CriteriaEntities>();
            dt = trackerdatamanager.getCriteriaList();
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.CriteriaEntities CriteriaDL = new EN.CriteriaEntities();
                    CriteriaDL.Criteria = Convert.ToString(row["criteria"]);
                    CriteriaDL.ID = Convert.ToInt32(row["ID"]);
                    CriteriaList.Add(CriteriaDL);
                }
            }
            return CriteriaList;
        }

        //Code by Rahul
        public List<EN.MovementAtICD> GetMovementAtICD(string Criteria, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            List<EN.MovementAtICD> MovementICDList = new List<EN.MovementAtICD>();
            dt = trackerdatamanager.getMovementICD(Criteria, FromDate, ToDate);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.MovementAtICD movementDL = new EN.MovementAtICD();
                    movementDL.SrNo = Convert.ToString(row["Sr No"]);
                    movementDL.VesselName = Convert.ToString(row["Vessel Name"]);
                    movementDL.ViaNo = Convert.ToString(row["Via No"]);
                    movementDL.ETA = Convert.ToString(row["ETA"]);
                    movementDL.PortName = Convert.ToString(row["PortName"]);
                    movementDL.DischargeFromVessel = Convert.ToString(row["Discharge Date"]);
                    movementDL.HBL = Convert.ToString(row["HBL Number"]);
                    movementDL.BLNumber = Convert.ToString(row["BL Number"]);
                    movementDL.BLDate = Convert.ToString(row["BL Entry date"]);
                    movementDL.BLReceivedDate = Convert.ToString(row["BL Received date"]);
                    movementDL.ShipingLine = Convert.ToString(row["Line"]);
                    movementDL.ShipperName = Convert.ToString(row["Shipper Name"]);
                    movementDL.Size = Convert.ToString(row["Size"]);

                    int Containersize = Convert.ToInt16(row["Size"]);
                    if (Containersize == 20)
                    {
                        movementDL.TEUS = "1";

                    }
                    else if (Containersize == 40)
                    {
                        movementDL.TEUS = "2";

                    }
                    else if (Containersize == 45)
                    {
                        movementDL.TEUS = "2";

                    }
                    movementDL.ImporterName = Convert.ToString(row["Importer Name"]);
                    movementDL.CHA = Convert.ToString(row["CHA Name"]);
                    movementDL.BillingParty = Convert.ToString(row["Billing Party"]);
                    movementDL.Haulage = Convert.ToString(row["Haulage Type"]);
                    movementDL.Cargo = Convert.ToString(row["Commodity"]);
                    movementDL.IGM = Convert.ToString(row["IGM Status"]);
                    movementDL.ModeOfTransport = Convert.ToString(row["Mode Of Transport"]);
                    movementDL.PortOfLoading = Convert.ToString(row["Port Of Loading"]);
                    movementDL.Container = Convert.ToString(row["Container No."]);
                    movementDL.ScannigData = Convert.ToString(row["Scan Type"]);
                    movementDL.Remarks = Convert.ToString(row["Remarks"]);
                    movementDL.SMTPDate = Convert.ToString(row["SMTP Date"]);
                    movementDL.OutDate = Convert.ToString(row["Out Date From Port"]);
                    movementDL.InTransit = Convert.ToString(row["In Transit"]);
                    movementDL.GateICDStatus = Convert.ToString(row["Indate & Time"]);
                    movementDL.OutofChargeDate = Convert.ToString(row["Out of Charge Date"]);
                    movementDL.DocumentReceiveDate = Convert.ToString(row["Document Receive Date at ICD"]);
                    movementDL.LoadedGate = Convert.ToString(row["Loaded Gate Out Date"]);
                    movementDL.GateOut = Convert.ToString(row["Gate Out Trailer No"]);
                    movementDL.ReachedtimeAtFactory = Convert.ToString(row["Reached time at Factory"]);
                    movementDL.OutFromFactoryDate = Convert.ToString(row["Out from Factory Date & Time"]);
                    movementDL.DwellHours = Convert.ToString(row["Dwell Hours"]);
                    movementDL.EmptyGateInTrailerNo = Convert.ToString(row["Empty Gate In Trailer No"]);
                    movementDL.EmptyGateInDate = Convert.ToString(row["Empty Gate In Date"]);
                    movementDL.OffloadingYard = Convert.ToString(row["Offloading Yard"]);
                    movementDL.EmptyGateOutDate = Convert.ToString(row["Empty Gate Out Date"]);
                    movementDL.EmptyGateOutTrailerNo = Convert.ToString(row["Empty Gate Out Trailer No"]);
                    movementDL.EmptyOffloadedYardName = Convert.ToString(row["Empty Offloaded at Yard Name"]);
                    movementDL.EmptyOffloadedDate = Convert.ToString(row["Empty Offloaded Date & Time"]);
                    movementDL.EmptyValidity = Convert.ToString(row["Empty Validity"]);
                    movementDL.KAMTeam = Convert.ToString(row["KAM"]);
                    movementDL.ManagementTeam = Convert.ToString(row["Marketing Person"]);

                    MovementICDList.Add(movementDL);
                }
            }
            return MovementICDList;
        }
        //Code end by Rahul

        public List<EN.JobOrderDEntities> getContainerSearchList(string containerNo)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getContainerSearchList(containerNo);
            List<EN.JobOrderDEntities> ContainerList = new List<EN.JobOrderDEntities>();
            int Count = 1;
            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.JobOrderDEntities ContainerDL = new EN.JobOrderDEntities();
                    ContainerDL.srno = Convert.ToString(Count++);
                    ContainerDL.ContainerNo = Convert.ToString(row["Container No"]);
                    ContainerDL.Activity = Convert.ToString(row["Activity"]);
                    ContainerDL.InDate = Convert.ToString(row["Activity Date"]);
                    ContainerDL.JONo = Convert.ToInt64(row["JoNo/EntryID"]);
                    ContainerDL.Line = Convert.ToString(row["Line"]);
                    ContainerDL.sizeType = Convert.ToString(row["Size/Type"]);
                    ContainerDL.validityDate = Convert.ToString(row["Do Validity Date"]);

                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        
        }

        public List<EN.GSTReturnEntities> getGSTReturnList(string FromDate, string ToDate)
        {
            DataTable DT = new DataTable();
            DT = trackerdatamanager.getGSTReturnList(FromDate, ToDate);
            List<EN.GSTReturnEntities> GSTList = new List<EN.GSTReturnEntities>();
            int Count = 1;
            if (DT.Rows.Count != 0)
            {
                foreach (DataRow row in DT.Rows)
                {
                   
                    EN.GSTReturnEntities GSTDL = new EN.GSTReturnEntities();
                    GSTDL.srno = Convert.ToString(Count++);
                    GSTDL.Category = Convert.ToString(row["Category"]);
                    GSTDL.GSTRecipient = Convert.ToString(row["GSTIN/UIN of Recipient"]);
                    GSTDL.ReceiverName = Convert.ToString(row["Receiver Name"]);
                    GSTDL.InvoiceNo = Convert.ToString(row["Invoice Number"]);
                    GSTDL.InvoiceDate = Convert.ToString(row["Invoice date"]);
                    GSTDL.InvoiceValue = Convert.ToString(row["Invoice Value"]);
                    GSTDL.PlaceOfSupply = Convert.ToString(row["Place Of Supply"]);
                    GSTDL.ReverseChange = Convert.ToString(row["Reverse Charge"]);

                    GSTDL.InvoiceType = Convert.ToString(row["Invoice Type"]);
                    GSTDL.Rate = Convert.ToString(row["Rate"]);
                    GSTDL.TaxValue = Convert.ToString(row["Taxable Value"]);
                    GSTDL.IGST = Convert.ToString(row["IGST Amount"]);
                    GSTDL.CGST = Convert.ToString(row["CGST Amount"]);
                    GSTDL.SGST = Convert.ToString(row["SGST Amount"]);
                    GSTDL.TotalGST = Convert.ToString(row["Total GST"]);
                    GSTDL.HSNCode = Convert.ToString(row["HSN/SAC code"]);

                    GSTList.Add(GSTDL);
                }

            }
            return GSTList;
        }

        public List<EN.ShipLines> getShipLinesForCodeco()
        {
            List<EN.ShipLines> ShipLinesDL = new List<EN.ShipLines>();
            DataTable ShipLinesDT = new DataTable();
            string Table = "ShipLines";
            string Column = "SLID,SLName,SaID";
            string Condition = "SLIsActive=1";
            string OrderBy = "SaID";

            ShipLinesDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ShipLinesDT != null)
            {
                foreach (DataRow row in ShipLinesDT.Rows)
                {
                    EN.ShipLines ShipLinesList = new EN.ShipLines();
                    ShipLinesList.SLID = Convert.ToInt32(row["SLID"]);
                    ShipLinesList.SLName = Convert.ToString(row["SLName"]);
                    ShipLinesList.SLCode = Convert.ToString(row["SaID"]);

                    ShipLinesDL.Add(ShipLinesList);
                }
            }
            return ShipLinesDL;
        }

        public List<EN.CodecoStatusEntties> getCodecoStatusList(string FromDate, string ToDate, string ShipLines, string Event, string ContainerNo, string SearchCriteria)
        {
            DataTable DT = new DataTable();
            DT = trackerdatamanager.getCodecoStatusList(FromDate, ToDate, ShipLines, Event, ContainerNo, SearchCriteria);
            List<EN.CodecoStatusEntties> CodecoStatusList = new List<EN.CodecoStatusEntties>();
            int Count = 1;
            if (DT.Rows.Count != 0)
            {
                foreach (DataRow row in DT.Rows)
                {

                    EN.CodecoStatusEntties DL = new EN.CodecoStatusEntties();
                    DL.srno = Convert.ToString(Count++);
                    DL.SentDate = Convert.ToString(row["SendDate"]);
                    DL.ShippingLine = Convert.ToString(row["SaID"]);
                    DL.ContainerNo = Convert.ToString(row["Containers"]);
                    DL.Event = Convert.ToString(row["event"]);
                    DL.FileName = Convert.ToString(row["FileName"]);
                    CodecoStatusList.Add(DL);
                }

            }
            return CodecoStatusList;
        
        }
        //Codes by Prashant

        public List<EN.PendingContainerForEmptyOffLoading> GetImportInList()
        {
            DataTable ImportInDL = new DataTable();
            ImportInDL = trackerdatamanager.getPendingContainerForEmptyoffloading();
            List<EN.PendingContainerForEmptyOffLoading> ImportIn = new List<EN.PendingContainerForEmptyOffLoading>();
            int Count = 1;
            if (ImportInDL.Rows.Count != 0)
            {
                foreach (DataRow row in ImportInDL.Rows)
                {
                    EN.PendingContainerForEmptyOffLoading pendingcontainerlist = new EN.PendingContainerForEmptyOffLoading();
                    pendingcontainerlist.SrNo = Convert.ToInt16(Count++);
                    pendingcontainerlist.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    pendingcontainerlist.SLName = Convert.ToString(row["SLName"]);
                    pendingcontainerlist.DoValiddate = Convert.ToString(row["DoValiddate"]);
                    pendingcontainerlist.BLNumber = Convert.ToString(row["BLNumber"]);
                    pendingcontainerlist.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    pendingcontainerlist.OutDate = Convert.ToString(row["OutDate"]);
                    pendingcontainerlist.ImporterName = Convert.ToString(row["ImporterName"]);
                    pendingcontainerlist.InDate = Convert.ToString(row["InDate"]);
                    pendingcontainerlist.OffloadingLocation = Convert.ToString(row["Offloading Location"]);
                    pendingcontainerlist.Deliverytype = Convert.ToString(row["Deliverytype"]);
                    pendingcontainerlist.validdate = Convert.ToString(row["validdate"]);
                    pendingcontainerlist.remarks = Convert.ToString(row["remarks"]);
                    pendingcontainerlist.TypeSize = Convert.ToString(row["Size/Type"]);
                    pendingcontainerlist.Type = Convert.ToString(row["ContainerType"]);
                    pendingcontainerlist.Size = Convert.ToString(row["Size"]);
                    pendingcontainerlist.DocReceivedDate = Convert.ToString(row["Document Received Date"]);
                    pendingcontainerlist.CHAName = Convert.ToString(row["CHAName"]);
                    ImportIn.Add(pendingcontainerlist);
                }

            }
            return ImportIn;
        }

        // Codes by Prashant
        public List<EN.ConsigneeEntities> GetConsigneeDetails(string FromDate, string Todate)
        {
            DataTable ConsigneeDL = new DataTable();
            ConsigneeDL = trackerdatamanager.GetConsigneeDetails(FromDate, Todate);
            List<EN.ConsigneeEntities> ConsigneeList = new List<EN.ConsigneeEntities>();

            if (ConsigneeDL.Rows.Count != 0)
            {
                foreach (DataRow row in ConsigneeDL.Rows)
                {
                    EN.ConsigneeEntities Consigneelist = new EN.ConsigneeEntities();
                    Consigneelist.ImporterName = Convert.ToString(row["Importer Name"]);
                    Consigneelist.CHAName = Convert.ToString(row["CHA Name"]);
                    Consigneelist.BillingPartyName = Convert.ToString(row["Billing Party Name"]);
                    Consigneelist.ContainerNo = Convert.ToString(row["Container No"]);
                    Consigneelist.ModeofTransport = Convert.ToString(row["Mode of Transport"]);
                    Consigneelist.IGMNo = Convert.ToString(row["IGM No"]);
                    Consigneelist.ItemNo = Convert.ToString(row["Item No"]);
                    Consigneelist.ICDGateIn = Convert.ToString(row["ICD Gate In"]);
                    Consigneelist.ClearanceDate = Convert.ToString(row["Clearance Date"]);
                    Consigneelist.MovementDatetoFactory = Convert.ToString(row["Movement Date to Factory"]);
                    Consigneelist.LRNo = Convert.ToString(row["LR No"]);
                    Consigneelist.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    Consigneelist.ReportingDateatFactory = Convert.ToString(row["Reporting Date at Factory"]);
                    Consigneelist.VehicleReleasedfromFactory = Convert.ToString(row["Vehicle Released from Factory"]);
                    Consigneelist.ValidTill = Convert.ToString(row["Valid Till"]);
                    Consigneelist.BOENo = Convert.ToString(row["BOE No"]);
                    Consigneelist.TotalHours = Convert.ToString(row["Total Hours"]);


                    ConsigneeList.Add(Consigneelist);
                }

            }
            return ConsigneeList;
        }

        //Codes by Prashant 1 July 2019

        public List<EN.EmptyYardGateINSummaryEntities> GetEmptyAddGateInDetails(string fromdate, string todate, string search, string name)
        {

            List<EN.EmptyYardGateINSummaryEntities> EmptyYardINList = new List<EN.EmptyYardGateINSummaryEntities>();
            DataTable EmptyDL = new DataTable();
            EmptyDL = trackerdatamanager.GetEmptyYardGateINSummary(fromdate, todate, search, name);
            if (EmptyDL.Rows.Count > 0)
            {
                foreach (DataRow row in EmptyDL.Rows)
                {
                    EN.EmptyYardGateINSummaryEntities EmptyyardDL = new EN.EmptyYardGateINSummaryEntities();

                    EmptyyardDL.SrNo = Convert.ToString(row["Sr No"]);
                    EmptyyardDL.ContainerNo = Convert.ToString(row["Container No"]);
                    EmptyyardDL.Size = Convert.ToString(row["Size"]);
                    EmptyyardDL.ContainerType = Convert.ToString(row["Type"]);
                    EmptyyardDL.ISOCode = Convert.ToString(row["ISO Code"]);
                    EmptyyardDL.tareweight = Convert.ToString(row["Tare Weight"]);
                    EmptyyardDL.statustype = Convert.ToString(row["Status"]);
                    EmptyyardDL.InDate = Convert.ToString(row["In date & Time"]);
                    EmptyyardDL.Intime = Convert.ToString(row["Intime"]);
                    EmptyyardDL.SLName = Convert.ToString(row["Shipping Line"]);
                    EmptyyardDL.consignee = Convert.ToString(row["Customer Name"]);
                    EmptyyardDL.Trailername = Convert.ToString(row["Trailer name"]);
                    EmptyyardDL.Transporter = Convert.ToString(row["Transporter"]);
                    EmptyyardDL.Location = Convert.ToString(row["Location"]);
                    EmptyyardDL.BookingNo = Convert.ToString(row["Booking No"]);
                    EmptyyardDL.Condition = Convert.ToString(row["Condition"]);
                    EmptyyardDL.MFGDate = Convert.ToString(row["MFG Date"]);
                    EmptyyardDL.DoValidDate = Convert.ToString(row["Do Valid Date"]);
                    EmptyyardDL.grossweight = Convert.ToString(row["Gross Weight"]);
                    EmptyyardDL.Remarks = Convert.ToString(row["Remarks"]);
                    EmptyyardDL.DwellDays = Convert.ToString(row["Dwell Days"]);
                    EmptyYardINList.Add(EmptyyardDL);

                }
            }


            return EmptyYardINList;
        }

        public List<EN.EmptyYardGateOutSummaryEntities> GetEmptyAddGateOutDetails(string fromdate, string todate, string search, string name)
        {

            List<EN.EmptyYardGateOutSummaryEntities> EmptyYardOutList = new List<EN.EmptyYardGateOutSummaryEntities>();
            DataTable EmptyDL = new DataTable();
            EmptyDL = trackerdatamanager.GetEmptyYardGateOutSummary(fromdate, todate, search, name);
            if (EmptyDL.Rows.Count > 0)
            {
                foreach (DataRow row in EmptyDL.Rows)
                {
                    EN.EmptyYardGateOutSummaryEntities EmptyyardDL = new EN.EmptyYardGateOutSummaryEntities();

                    EmptyyardDL.SrNo = Convert.ToString(row["Sr No"]);
                    EmptyyardDL.gpno = Convert.ToString(row["GP No"]);
                    EmptyyardDL.ContainerNo = Convert.ToString(row["Container No"]);
                    EmptyyardDL.Size = Convert.ToString(row["Size"]);
                    EmptyyardDL.ContainerType = Convert.ToString(row["Type"]);
                    EmptyyardDL.ISOCode = Convert.ToString(row["ISO Code"]);
                    EmptyyardDL.tareweight = Convert.ToString(row["Tare Weight"]);
                    EmptyyardDL.statustype = Convert.ToString(row["Status"]);
                    EmptyyardDL.InDate = Convert.ToString(row["In date & Time"]);
                    EmptyyardDL.OutDate = Convert.ToString(row["Out Date & Time"]);
                    EmptyyardDL.OUTTime = Convert.ToString(row["OUT Time"]);
                    EmptyyardDL.DwellDays = Convert.ToString(row["Dwell Days"]);
                    EmptyyardDL.SLName = Convert.ToString(row["Shipping Line"]);
                    EmptyyardDL.TrailerName = Convert.ToString(row["Trailer name"]);
                    EmptyyardDL.Transporter = Convert.ToString(row["Transporter"]);
                    EmptyyardDL.Location = Convert.ToString(row["Location"]);
                    EmptyyardDL.Source = Convert.ToString(row["Source"]);
                    EmptyyardDL.bookingno = Convert.ToString(row["Booking No"]);
                    EmptyyardDL.SealNo = Convert.ToString(row["Seal No"]);
                    EmptyyardDL.ShipperName = Convert.ToString(row["Shipper Name"]);
                    EmptyyardDL.pod = Convert.ToString(row["POD"]);
                    EmptyyardDL.TransportMode = Convert.ToString(row["Transport Mode"]);
                    EmptyyardDL.TrainNo = Convert.ToString(row["Train No"]);
                    EmptyyardDL.Remarks = Convert.ToString(row["Remarks"]);
                    EmptyYardOutList.Add(EmptyyardDL);

                }
            }


            return EmptyYardOutList;
        }

        // Codes By Prashant 02 july 2019
        public List<EN.ExportEmptyGateInSummaryEntities> GetExportEmptyAddGateINDetails(string fromdate, string todate, string search, string search1)
        {

            List<EN.ExportEmptyGateInSummaryEntities> EmptyYardOutList = new List<EN.ExportEmptyGateInSummaryEntities>();
            DataTable EmptyDL = new DataTable();
            EmptyDL = trackerdatamanager.GetExportEmptyYardGateOutSummary(fromdate, todate, search, search1);
            if (EmptyDL.Rows.Count > 0)
            {
                foreach (DataRow row in EmptyDL.Rows)
                {
                    EN.ExportEmptyGateInSummaryEntities EmptyyardDL = new EN.ExportEmptyGateInSummaryEntities();

                    EmptyyardDL.SrNo = Convert.ToString(row["Sr. No."]);
                    EmptyyardDL.CFSCode = Convert.ToString(row["CFS Code"]);
                    EmptyyardDL.containerNo = Convert.ToString(row["Container NO."]);
                    EmptyyardDL.Size = Convert.ToString(row["Size"]);
                    EmptyyardDL.ContainerType = Convert.ToString(row["Container Type"]);
                    EmptyyardDL.indate = Convert.ToString(row["In Date And Time"]);
                    EmptyyardDL.TareWeight = Convert.ToString(row["Tare Weight"]);
                    EmptyyardDL.GrossWeight = Convert.ToString(row["Gross Weight"]);
                    EmptyyardDL.PayLoad = Convert.ToString(row["Pay Load"]);
                    EmptyyardDL.trailerno = Convert.ToString(row["Truck No."]);
                    EmptyyardDL.MType = Convert.ToString(row["Move Type"]);
                    EmptyyardDL.condition = Convert.ToString(row["Condition"]);
                    EmptyyardDL.LineName = Convert.ToString(row["Line Name"]);
                    EmptyyardDL.AGName = Convert.ToString(row["On Account"]);
                    EmptyyardDL.Source = Convert.ToString(row["Source"]);
                    EmptyyardDL.SealNo = Convert.ToString(row["RFID"]);
                    EmptyyardDL.EntryType = Convert.ToString(row["Entry Type"]);
                    EmptyyardDL.BookingNo = Convert.ToString(row["Booking No."]);
                    EmptyyardDL.Pickup = Convert.ToString(row["Pickup"]);
                    EmptyyardDL.Remarks = Convert.ToString(row["Remarks"]);
                    EmptyyardDL.UserName = Convert.ToString(row["Gate IN By"]);

                    EmptyYardOutList.Add(EmptyyardDL);

                }
            }


            return EmptyYardOutList;
        }



        public List<EN.ShipLines> getGetShippingName(string name)
        {
            DataTable DT = new DataTable();
            DT = trackerdatamanager.GetShippingORAccountname(name);
            List<EN.ShipLines> ShippingList = new List<EN.ShipLines>();

            if (DT.Rows.Count != 0)
            {
                foreach (DataRow row in DT.Rows)
                {

                    EN.ShipLines DL = new EN.ShipLines();

                    DL.SLID = Convert.ToInt16(row["ID"]);
                    DL.SLName = Convert.ToString(row["Name"]);
                    ShippingList.Add(DL);
                }
            }
            return ShippingList;

        }


        public List<EN.ExportLoadedGateInSummaryEntities> GetExportLoadedAddGateINDetails(string fromdate, string todate, string search, string search1, string Search2)
        {

            List<EN.ExportLoadedGateInSummaryEntities> ExportLoadedList = new List<EN.ExportLoadedGateInSummaryEntities>();
            DataTable EmptyDL = new DataTable();
            EmptyDL = trackerdatamanager.GetExportLoadedGateInSummary(fromdate, todate, search, search1, Search2);
            if (EmptyDL.Rows.Count > 0)
            {
                foreach (DataRow row in EmptyDL.Rows)
                {
                    EN.ExportLoadedGateInSummaryEntities EmptyyardDL = new EN.ExportLoadedGateInSummaryEntities();

                    EmptyyardDL.SrNo = Convert.ToString(row["Sr. No."]);
                    EmptyyardDL.entryID = Convert.ToString(row["CFS Code"]);
                    EmptyyardDL.indate = Convert.ToString(row["In Date And Time"]);
                    EmptyyardDL.FactoryRDate = Convert.ToString(row["Factory RDate"]);
                    EmptyyardDL.FactoryInDate = Convert.ToString(row["Factory InDate"]);
                    EmptyyardDL.FactoryOutDate = Convert.ToString(row["Factory OutDate"]);
                    EmptyyardDL.DwellHours = Convert.ToString(row["Dwell Hours"]);
                    EmptyyardDL.containerNo = Convert.ToString(row["Container NO."]);
                    EmptyyardDL.Size = Convert.ToString(row["Size"]);
                    EmptyyardDL.ContainerType = Convert.ToString(row["Container Type"]);
                    EmptyyardDL.TareWeight = Convert.ToString(row["Tare Weight"]);
                    EmptyyardDL.GrossWeight = Convert.ToString(row["Gross Weight"]);
                    EmptyyardDL.PayLoad = Convert.ToString(row["Pay Load"]);
                    EmptyyardDL.trailerno = Convert.ToString(row["Truck No."]);
                    EmptyyardDL.MType = Convert.ToString(row["Move Type"]);
                    EmptyyardDL.condition = Convert.ToString(row["Condition"]);
                    EmptyyardDL.said = Convert.ToString(row["Line Name"]);
                    EmptyyardDL.AGName = Convert.ToString(row["On Account"]);
                    EmptyyardDL.Source = Convert.ToString(row["Source"]);
                    EmptyyardDL.SealNo = Convert.ToString(row["RFID"]);
                    EmptyyardDL.EntryType = Convert.ToString(row["Entry Type"]);
                    EmptyyardDL.BookingNo = Convert.ToString(row["Booking No."]);
                    EmptyyardDL.Remarks = Convert.ToString(row["Remarks"]);
                    EmptyyardDL.UserName = Convert.ToString(row["Gate IN By"]);


                    ExportLoadedList.Add(EmptyyardDL);

                }
            }


            return ExportLoadedList;
        }
        //Codes by Prashant
        //Codes  by Manish

        // start by code line wise report
        public List<EN.LinewiseTuesReport> getLineWiseTuesReportList(string Activity, string date,string Yard)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getLineWiseTuesReportList(Activity, date,Yard);
            List<EN.LinewiseTuesReport> ContainerList = new List<EN.LinewiseTuesReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.LinewiseTuesReport Tues1 = new EN.LinewiseTuesReport();
                    Tues1.srno = Convert.ToInt32(Count++);
                    Tues1.Lines = Convert.ToString(row["LINE"]);
                    Tues1.Size20 = Convert.ToString(row["SIZE_20"]);
                    Tues1.Size40 = Convert.ToString(row["SIZE_40"]);
                    Tues1.Size45 = Convert.ToString(row["SIZE_45"]);
                    Tues1.Total = Convert.ToString(row["TOTAL"]);
                    Tues1.Tues = Convert.ToString(row["TEUS"]);

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        public List<EN.LinewiseTuesReport> getLineWiseReportList(string Activity, string FromDate,string ToDate, string Yard)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getLineWiseReportList(Activity, FromDate,ToDate, Yard);
            List<EN.LinewiseTuesReport> ContainerList = new List<EN.LinewiseTuesReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.LinewiseTuesReport Tues1 = new EN.LinewiseTuesReport();
                    Tues1.srno = Convert.ToInt32(Count++);
                    Tues1.Lines = Convert.ToString(row["LINE"]);
                    Tues1.Size20 = Convert.ToString(row["SIZE_20"]);
                    Tues1.Size40 = Convert.ToString(row["SIZE_40"]);
                    Tues1.Size45 = Convert.ToString(row["SIZE_45"]);
                    Tues1.Total = Convert.ToString(row["TOTAL"]);
                    Tues1.Tues = Convert.ToString(row["TEUS"]);

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }
        // end by code line wise report

        // start by code port wise Tues report
        public List<EN.PortWiseTuesReport> getportTuesReportList(string Activity)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getPortWiseTuesReportList(Activity);
            List<EN.PortWiseTuesReport> ContainerList = new List<EN.PortWiseTuesReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.PortWiseTuesReport Tues1 = new EN.PortWiseTuesReport();
                    Tues1.Srno = Convert.ToInt32(Count++);
                    Tues1.day = Convert.ToString(row["days"]);
                    Tues1.BMCT = Convert.ToString(row["BMCT"]);
                    Tues1.GTI = Convert.ToString(row["GTI"]);
                    Tues1.NSICT = Convert.ToString(row["NSICT"]);
                    Tues1.NSIGT = Convert.ToString(row["NSIGT"]);
                    Tues1.HAZIRA = Convert.ToString(row["HAZIRA"]);
                    Tues1.NSA = Convert.ToString(row["NSA"]);
                    Tues1.JNPT = Convert.ToString(row["JNPT"]);
                    Tues1.GrandTotal = Convert.ToString(row["GrandTotal"]);

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }
        // end by port wise tues report

        public List<EN.SalesRegisterrReport> getSalesRegisterrReportList(string Activity, string fdate, string Todate)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getSalesRegisterrReportList(Activity, fdate, Todate);
            List<EN.SalesRegisterrReport> ContainerList = new List<EN.SalesRegisterrReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.SalesRegisterrReport Tues1 = new EN.SalesRegisterrReport();
                    Tues1.Srno = Convert.ToInt32(Count++);
                    Tues1.Category = Convert.ToString(row["Category"]);
                    Tues1.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    Tues1.AssessDate = Convert.ToString(row["Invoice Date"]);
                    Tues1.Deliverytype = Convert.ToString(row["Invoice Type"]);
                    Tues1.BLNumber = Convert.ToString(row["BL Number"]);
                    Tues1.CHAName = Convert.ToString(row["CHA Name"]);
                    Tues1.SLName = Convert.ToString(row["Shipping Line"]);
                    Tues1.Consignee = Convert.ToString(row["Importer/Exporter"]);
                    Tues1.GSTName = Convert.ToString(row["GST Party Name"]);
                    Tues1.GSTIn_uniqID = Convert.ToString(row["GST Number"]);
                    Tues1.NetTotal = Convert.ToString(row["Net Total"]);
                    Tues1.DiscAmount = Convert.ToString(row["Discount Amt"]);
                    Tues1.TaxableAmount = Convert.ToString(row["Taxable Amount"]);
                    Tues1.TotalGST = Convert.ToString(row["Total GST"]);
                    Tues1.GrandTotal = Convert.ToString(row["Grand Total"]);
                    Tues1.Remarks = Convert.ToString(row["Remarks"]);
                    Tues1.UserName = Convert.ToString(row["Invoice Generated By"]);
                    Tues1.DocketNo = Convert.ToString(row["Docket No"]);
                    Tues1.DocketDate = Convert.ToString(row["Dispatch Date"]);

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        public List<EN.ExportStuffingPlanReport> getExportStuffingPlanReport(string AsOnDate)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getExportStuffingPlanReportList(AsOnDate);
            List<EN.ExportStuffingPlanReport> ContainerList = new List<EN.ExportStuffingPlanReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.ExportStuffingPlanReport Tues1 = new EN.ExportStuffingPlanReport();
                    Tues1.Srno = Convert.ToInt32(Count++);
                    Tues1.AllowID = Convert.ToString(row["Allow ID"]);
                    Tues1.StuffingDate = Convert.ToString(row["Stuffing Date"]);
                    Tues1.StuffingType = Convert.ToString(row["Stuffing Type"]);
                    Tues1.Size = Convert.ToString(row["Size"]);
                    Tues1.Type = Convert.ToString(row["Type"]);
                    Tues1.Teus = Convert.ToString(row["Teus"]);
                    Tues1.LineName = Convert.ToString(row["Line Name"]);
                    Tues1.BookingNo = Convert.ToString(row["Booking No"]);
                    Tues1.Allotment = Convert.ToString(row["Allotment"]);
                    Tues1.ClearanceStatus = Convert.ToString(row["Clearance Status"]);
                    Tues1.DoValidity = Convert.ToString(row["Do EXP Validity"]);
                    Tues1.PartyName = Convert.ToString(row["Party Name"]);
                    Tues1.POL = Convert.ToString(row["POL"]);
                    Tues1.PODName = Convert.ToString(row["PODName"]);
                    Tues1.VesselName = Convert.ToString(row["Vessel Name"]);
                    Tues1.CHAName = Convert.ToString(row["CHA Name"]);
                    Tues1.Remarks = Convert.ToString(row["Remarks"]);
                    Tues1.AddedBy = Convert.ToString(row["Added By"]);
                    //Tues1.TotalGST = Convert.ToString(row["Total GST"]);
                    //Tues1.GrandTotal = Convert.ToString(row["Grand Total"]);
                    //Tues1.Remarks = Convert.ToString(row["Remarks"]);
                    //Tues1.UserName = Convert.ToString(row["Invoice Generated By"]);

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        public List<EN.SalesRegisterContainerWiseReport> getSalesRegisterrContainerWiseReportList(string Activity, string fdate, string Todate)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getSalesRegisterrContainerWiseReportList(Activity, fdate, Todate);
            List<EN.SalesRegisterContainerWiseReport> ContainerList = new List<EN.SalesRegisterContainerWiseReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.SalesRegisterContainerWiseReport Tues1 = new EN.SalesRegisterContainerWiseReport();
                    Tues1.Srno = Convert.ToInt32(Count++);
                    Tues1.Category = Convert.ToString(row["Category"]);
                    Tues1.partyName = Convert.ToString(row["PartyName"]);
                    Tues1.LINENAME = Convert.ToString(row["LINE NAME"]);
                    Tues1.CHANAME = Convert.ToString(row["CHA NAME"]);
                    Tues1.FINALINVOICEREFNO = Convert.ToString(row["FINAL INVOICE REF NO"]);
                    Tues1.ContainerNo = Convert.ToString(row["Container No"]);
                    Tues1.size = Convert.ToString(row["size"]);
                    Tues1.ContainerType = Convert.ToString(row["Container Type"]);
                    Tues1.CargoType = Convert.ToString(row["Cargo Type"]);
                    Tues1.INVOICEDATE = Convert.ToString(row["INVOICE DATE"]);
                    Tues1.receiptREFno = Convert.ToString(row["receipt REF no"]);
                    Tues1.BILLITEMDESCRIPTION = Convert.ToString(row["BILL ITEM DESCRIPTION"]);
                    Tues1.billamount = Convert.ToString(row["bill amount"]);
                    Tues1.GSTAmt = Convert.ToString(row["GST Amt"]);
                    Tues1.GST = Convert.ToString(row["GST"]);
                    //Tues1.TotalGST = Convert.ToString(row["Total GST"]);
                    //Tues1.GrandTotal = Convert.ToString(row["Grand Total"]);
                    //Tues1.Remarks = Convert.ToString(row["Remarks"]);
                    //Tues1.UserName = Convert.ToString(row["Invoice Generated By"]);

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }


        public List<EN.EmptyBookingReport> getEmptyBookingReport(string FromDate, string ToDate, string category, string search)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getEmptyBookingReportList(FromDate, ToDate, category, search);
            List<EN.EmptyBookingReport> ContainerList = new List<EN.EmptyBookingReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.EmptyBookingReport Tues1 = new EN.EmptyBookingReport();
                    Tues1.SrNo = Convert.ToInt32(Count++);
                    Tues1.DoDate = Convert.ToString(row["Do Date"]);
                    Tues1.Time = Convert.ToString(row["Time"]);
                    Tues1.StuffingDate = Convert.ToString(row["Stuffing Date"]);
                    Tues1.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    Tues1.ContainerNo = Convert.ToString(row["Container No"]);
                    Tues1.Size = Convert.ToString(row["Size"]);
                    Tues1.Type = Convert.ToString(row["Type"]);
                    Tues1.Teus = Convert.ToString(row["Teus"]);
                    Tues1.LineName = Convert.ToString(row["Line Name"]);
                    Tues1.BookingNo = Convert.ToString(row["Booking No"]);
                    Tues1.SealNo = Convert.ToString(row["Seal No"]);
                    Tues1.Allotment = Convert.ToString(row["Allotment"]);
                    Tues1.ClearanceStatus = Convert.ToString(row["Clearance Status"]);
                    Tues1.DoEXPValidity = Convert.ToString(row["Do EXP Validity"]);
                    Tues1.CHAName = Convert.ToString(row["Party Name"]);
                    Tues1.POL = Convert.ToString(row["POL"]);
                    Tues1.VesselName = Convert.ToString(row["Vessel Name"]);
                    Tues1.GateOpenDate = Convert.ToString(row["Gate Open Date"]);
                    Tues1.CutOFFDate = Convert.ToString(row["Cut OFF Date"]);
                    Tues1.CurrentLocation = Convert.ToString(row["Current Location"]);
                    Tues1.MovementDate = Convert.ToString(row["Movement Date"]);
                    Tues1.Remarks = Convert.ToString(row["Remarks"]);
                    Tues1.TrainNo = Convert.ToString(row["Train No"]);
                    Tues1.From = Convert.ToString(row["From"]);
                    Tues1.LEO = Convert.ToString(row["LEO"]);
                    Tues1.Team = Convert.ToString(row["Team"]);
                    Tues1.AddedBy = Convert.ToString(row["Added By"]);

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }
        public List<EN.LorryReceiptReport> getLorryReceiptReport(string FromDate, string ToDate, string category)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getLorryReceiptReportList(FromDate, ToDate, category);
            List<EN.LorryReceiptReport> ContainerList = new List<EN.LorryReceiptReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.LorryReceiptReport Tues1 = new EN.LorryReceiptReport();
                    Tues1.SrNo = Convert.ToInt32(Count++);
                    Tues1.LRNo = Convert.ToString(row["LR No"]);
                    Tues1.LRDate = Convert.ToString(row["LR Date"]);
                    Tues1.BookingNo = Convert.ToString(row["Booking No"]);
                    Tues1.AGName = Convert.ToString(row["Customer Name"]);
                    Tues1.ShippingLine = Convert.ToString(row["Shipping Line"]);
                    Tues1.FromLocation = Convert.ToString(row["From Location"]);
                    Tues1.ToLocation = Convert.ToString(row["To Location"]);
                    Tues1.ContainerNo = Convert.ToString(row["Container No"]);
                    Tues1.Size = Convert.ToString(row["Size"]);
                    Tues1.Type = Convert.ToString(row["Type"]);
                    Tues1.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    Tues1.AgentSeal = Convert.ToString(row["Agent Seal"]);
                    Tues1.BOENo = Convert.ToString(row["BOE No"]);
                    Tues1.AddedBy = Convert.ToString(row["Added By"]);

                    if (category == "ALL")
                    {
                        Tues1.Activity = Convert.ToString(row["Activity"]);
                    }
                    else
                        Tues1.Activity = " ";


                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }


        public List<EN.CustomerMaster> getCustomerMaster()
        {
            List<EN.CustomerMaster> passingDL = new List<EN.CustomerMaster>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getCustomerMaster();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CustomerMaster PassingList = new EN.CustomerMaster();
                    PassingList.AGID = Convert.ToInt32(row["AGID"]);
                    PassingList.AGName = Convert.ToString(row["AGName"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        public List<EN.BankMaster> getBankMaster()
        {
            List<EN.BankMaster> passingDL = new List<EN.BankMaster>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getBankMaster();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.BankMaster PassingList = new EN.BankMaster();
                    PassingList.AccountID = Convert.ToInt32(row["Con_Acc_ID"]);
                    PassingList.AccountNumber = Convert.ToString(row["AccountNo"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        public List<EN.BankMaster> getBankName()
        {
            List<EN.BankMaster> passingDL = new List<EN.BankMaster>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getBankName();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.BankMaster PassingList = new EN.BankMaster();
                    PassingList.AccountID = Convert.ToInt32(row["Con_Acc_ID"]);
                    PassingList.BankName = Convert.ToString(row["Bank_Name"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }


        public List<EN.ExportEmptyOutReport> getExportEmptyOutReport(string FromDate, string ToDate, string Customer)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getExportEmptyOutReportList(FromDate, ToDate, Customer);
            List<EN.ExportEmptyOutReport> ContainerList = new List<EN.ExportEmptyOutReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.ExportEmptyOutReport Tues1 = new EN.ExportEmptyOutReport();
                    Tues1.SrNo = Convert.ToInt32(Count++);
                    Tues1.ContainerNo = Convert.ToString(row["Container No"]);
                    Tues1.LineName = Convert.ToString(row["Line Name"]);
                    Tues1.Size = Convert.ToString(row["Size"]);
                    Tues1.Type = Convert.ToString(row["Type"]);
                    Tues1.indate = Convert.ToString(row["InDate & Time"]);
                    Tues1.Outdate = Convert.ToString(row["Out Date & Time"]);
                    Tues1.agname = Convert.ToString(row["Agency Name"]);
                    Tues1.TruckNo = Convert.ToString(row["Vehicle No"]);
                    Tues1.Transporter = Convert.ToString(row["Transporter"]);
                    Tues1.Location = Convert.ToString(row["Location"]);
                    Tues1.BookingNo = Convert.ToString(row["Booking No"]);
                    Tues1.Remark = Convert.ToString(row["Remark"]);
                    Tues1.Allotment = Convert.ToString(row["Allotment"]);
                    Tues1.addedby = Convert.ToString(row["GP Generated by"]);


                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }
        public List<EN.SalesRegisterContainerWiseHeadReport> getSalesRegisterContainerWiseHeadReport(string FromDate, string ToDate)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getSalesRegisterContainerWiseHeadReportList(FromDate, ToDate);
            List<EN.SalesRegisterContainerWiseHeadReport> ContainerList = new List<EN.SalesRegisterContainerWiseHeadReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.SalesRegisterContainerWiseHeadReport Tues1 = new EN.SalesRegisterContainerWiseHeadReport();
                    Tues1.SrNo = Convert.ToInt32(Count++);
                    Tues1.importername = Convert.ToString(row["importername"]);
                    Tues1.gstaddress = Convert.ToString(row["gstaddress"]);
                    Tues1.CHAName = Convert.ToString(row["CHA NAME"]);
                    Tues1.Linename = Convert.ToString(row["LINE NAME"]);
                    Tues1.agname = Convert.ToString(row["Customer"]);
                    Tues1.GSTName = Convert.ToString(row["PartyName"]);
                    Tues1.blnumber = Convert.ToString(row["blnumber"]);
                    Tues1.boeno = Convert.ToString(row["boeno"]);
                    Tues1.Haluage = Convert.ToString(row["Haluage"]);
                    Tues1.AssessType = Convert.ToString(row["AssessType"]);
                    Tues1.AssessDate = Convert.ToString(row["Invoice Date"]);
                    Tues1.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    Tues1.IGMNo = Convert.ToString(row["IGMNo"]);
                    Tues1.ItemNo = Convert.ToString(row["ItemNo"]);
                    Tues1.ContainerNo = Convert.ToString(row["Container No"]);
                    Tues1.size = Convert.ToString(row["size"]);
                    Tues1.ContainerType = Convert.ToString(row["Container Type"]);
                    Tues1.CargoType = Convert.ToString(row["Cargo Type"]);
                    Tues1.CargoDesc = Convert.ToString(row["Cargo Desc"]);
                    Tues1.CargoWt = Convert.ToString(row["Cargo Wt"]);
                    Tues1.TareWt = Convert.ToString(row["Tare Wt"]);
                    Tues1.GrossWt = Convert.ToString(row["Gross Wt"]);
                    Tues1.Commodity = Convert.ToString(row["Commodity"]);
                    Tues1.INDate = Convert.ToString(row["ICD GATE-in date"]);
                    Tues1.outdate = Convert.ToString(row["Gate-out date"]);
                    Tues1.ValidUptoDate = Convert.ToString(row["ValidUptoDate"]);
                    Tues1.DWELLDAYS = Convert.ToString(row["DWELL DAYS"]);
                    Tues1.FreeDay = Convert.ToString(row["FREE DAYS"]);
                    Tues1.accountname = Convert.ToString(row["BILL ITEM DESCRIPTION"]);
                    Tues1.PortName = Convert.ToString(row["Port Name"]);
                    Tues1.MovementRMS = Convert.ToString(row["RMS"]);
                    Tues1.TariffDescription = Convert.ToString(row["Tariff Descriptions"]);
                    Tues1.JOType = Convert.ToString(row["JO Type"]);
                    Tues1.ScanType = Convert.ToString(row["Scan Type"]);
                    Tues1.LIFTONOFF = Convert.ToString(row["LIFT ON/OFF (YES/NO)"]);
                    Tues1.EMPTYOFF = Convert.ToString(row["EMPTY OFF (YES/NO)"]);
                    Tues1.LOADINGANDUNLOADING = Convert.ToString(row["LOADING  AND  UNLOADING (L/UL)"]);
                    Tues1.FactorygateinDate = Convert.ToString(row["REPORTING DATE & TIME TO FACTORY"]);
                    Tues1.FactorygateoutDate = Convert.ToString(row["GATE-OUT FACTORY DATE & TIME"]);
                    Tues1.SURVEY = Convert.ToString(row["SURVEY(YES/NO)"]);
                    Tues1.deliverytype = Convert.ToString(row["Delivery Type"]);
                    Tues1.GST = Convert.ToString(row["GST"]);
                    Tues1.NetAmount = Convert.ToString(row["bill amount"]);
                    Tues1.GSTAmt = Convert.ToString(row["GST Amt"]);
                    Tues1.Remarks = Convert.ToString(row["Remarks"]);
                    Tues1.PreparedBy = Convert.ToString(row["Prepared By"]);
                    Tues1.CheckedBy = Convert.ToString(row["Checked By"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }


        public List<EN.ExportOutReport> GetExportOutReport(string Customer, string FromDate, string ToDate)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetExportOutReport(Customer, FromDate, ToDate);
            List<EN.ExportOutReport> ContainerList = new List<EN.ExportOutReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.ExportOutReport Tues1 = new EN.ExportOutReport();
                    Tues1.SrNo = Convert.ToInt32(Count++);
                    Tues1.ContainerNo = Convert.ToString(row["Container No"]);
                    Tues1.Size = Convert.ToString(row["Size"]);
                    Tues1.Type = Convert.ToString(row["Type"]);
                    Tues1.Commodity = Convert.ToString(row["Commodity"]);
                    Tues1.StuffingDate = Convert.ToString(row["Stuffing Date"]);
                    Tues1.GPDate = Convert.ToString(row["GP Date"]);
                    Tues1.MovOrderDate = Convert.ToString(row["Movement Order Date"]);
                    Tues1.CHAName = Convert.ToString(row["CHA Name"]);
                    Tues1.OnAccount = Convert.ToString(row["On Account"]);
                    Tues1.ShippingLine = Convert.ToString(row["Shipping Line"]);
                    Tues1.VesselName = Convert.ToString(row["Vessel Name"]);
                    Tues1.ExportName = Convert.ToString(row["Export Name"]);
                    Tues1.Port = Convert.ToString(row["Port"]);
                    Tues1.POD = Convert.ToString(row["POD"]);
                    Tues1.FOD = Convert.ToString(row["FOD"]);
                    Tues1.TrailerNo = Convert.ToString(row["Trailer No"]);
                    Tues1.Transporter = Convert.ToString(row["Transporter"]);
                    Tues1.EmptyInDate = Convert.ToString(row["Empty In Date"]);
                    Tues1.EmptyPickupYard = Convert.ToString(row["Empty Pickup Yard"]);
                    Tues1.SBNo = Convert.ToString(row["SB No"]);
                    Tues1.SBDate = Convert.ToString(row["SB Date"]);
                    Tues1.SBCargoType = Convert.ToString(row["SB Cargo Type"]);
                    Tues1.FoB = Convert.ToString(row["FOB Value"]);
                    Tues1.MoveType = Convert.ToString(row["Move Type"]);
                    Tues1.TransTYpe = Convert.ToString(row["Trans Type"]);
                    Tues1.AgentSeal = Convert.ToString(row["Agent Seal"]);
                    Tues1.CustomSeal = Convert.ToString(row["Custom Seal"]);
                    Tues1.NetWeight = Convert.ToString(row["NetWeight"]);
                    Tues1.GrossWeight = Convert.ToString(row["GrossWeight"]);
                    Tues1.TareWeight = Convert.ToString(row["TareWeight"]);
                    Tues1.Remarks = Convert.ToString(row["Remarks"]);
                    Tues1.GPGeneratedBy = Convert.ToString(row["GP Generated By"]);
                    Tues1.FinalOutDate = Convert.ToString(row["Final Out Date"]);
                    Tues1.Location = Convert.ToString(row["Location"]);



                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        //Codes End by Manish


        //Codes by Prashant

        //public List<EN.ExportEmptyOutReport> GetExportOutReport(string Customer,string FromDate, string ToDate )
        //{
        //    DataTable Tues = new DataTable();
        //    Tues = trackerdatamanager.GetExportOutReport(Customer, FromDate, ToDate);
        //    List<EN.ExportEmptyOutReport> ContainerList = new List<EN.ExportEmptyOutReport>();
        //    int Count = 1;
        //    if (Tues.Rows.Count != 0)
        //    {
        //        foreach (DataRow row in Tues.Rows)
        //        {
        //            EN.ExportEmptyOutReport Tues1 = new EN.ExportEmptyOutReport();
        //            Tues1.SrNo = Convert.ToInt32(Count++);
        //            Tues1.ContainerNo = Convert.ToString(row["Container No"]);
        //            Tues1.LineName = Convert.ToString(row["Line Name"]);
        //            Tues1.Size = Convert.ToString(row["Size"]);
        //            Tues1.Type = Convert.ToString(row["Type"]);
        //            Tues1.indate = Convert.ToString(row["InDate & Time"]);
        //            Tues1.Outdate = Convert.ToString(row["Out Date & Time"]);
        //            Tues1.agname = Convert.ToString(row["Agency Name"]);
        //            Tues1.TruckNo = Convert.ToString(row["Vehicle No"]);
        //            Tues1.Transporter = Convert.ToString(row["Transporter"]);
        //            Tues1.Location = Convert.ToString(row["Location"]);
        //            Tues1.BookingNo = Convert.ToString(row["Booking No"]);
        //            Tues1.Remark = Convert.ToString(row["Remark"]);
        //            Tues1.Allotment = Convert.ToString(row["Allotment"]);
        //            Tues1.addedby = Convert.ToString(row["GP Generated by"]);

        //            ContainerList.Add(Tues1);
        //        }

        //    }
        //    return ContainerList;

        //}

        //14 july 2019

        public List<EN.LinewiseTuesReport> GetImportEmptyList()
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetImportEmpty();
            List<EN.LinewiseTuesReport> ContainerList = new List<EN.LinewiseTuesReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.LinewiseTuesReport Tues1 = new EN.LinewiseTuesReport();
                    Tues1.srno = Convert.ToInt32(Count++);
                    Tues1.Lines = Convert.ToString(row["LINE"]);
                    Tues1.Size20 = Convert.ToString(row["SIZE_20"]);
                    Tues1.Size40 = Convert.ToString(row["SIZE_40"]);
                    Tues1.Size45 = Convert.ToString(row["SIZE_45"]);
                    Tues1.Total = Convert.ToString(row["TOTAL"]);
                    Tues1.Tues = Convert.ToString(row["TEUS"]);
                    Tues1.LineID = Convert.ToString(row["sLID"]);
                    Tues1.SlCode = Convert.ToString(row["said"]);

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }


        public List<EN.LinewiseTuesReport> GetImportLoadedList()
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetImportLoaded();
            List<EN.LinewiseTuesReport> ContainerList = new List<EN.LinewiseTuesReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.LinewiseTuesReport Tues1 = new EN.LinewiseTuesReport();
                    Tues1.srno = Convert.ToInt32(Count++);
                    Tues1.Lines = Convert.ToString(row["LINE"]);
                    Tues1.Size20 = Convert.ToString(row["SIZE_20"]);
                    Tues1.Size40 = Convert.ToString(row["SIZE_40"]);
                    Tues1.Size45 = Convert.ToString(row["SIZE_45"]);
                    Tues1.Total = Convert.ToString(row["TOTAL"]);
                    Tues1.Tues = Convert.ToString(row["TEUS"]);
                    Tues1.LineID = Convert.ToString(row["sLID"]);
                    Tues1.SlCode = Convert.ToString(row["said"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }


        public List<EN.LinewiseTuesReport> GetExportEmpty()
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetExportEmpty();
            List<EN.LinewiseTuesReport> ContainerList = new List<EN.LinewiseTuesReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.LinewiseTuesReport Tues1 = new EN.LinewiseTuesReport();
                    Tues1.srno = Convert.ToInt32(Count++);
                    Tues1.Lines = Convert.ToString(row["LINE"]);
                    Tues1.Size20 = Convert.ToString(row["SIZE_20"]);
                    Tues1.Size40 = Convert.ToString(row["SIZE_40"]);
                    Tues1.Size45 = Convert.ToString(row["SIZE_45"]);
                    Tues1.Total = Convert.ToString(row["TOTAL"]);
                    Tues1.Tues = Convert.ToString(row["TEUS"]);
                    Tues1.LineID = Convert.ToString(row["sLID"]);
                    Tues1.SlCode = Convert.ToString(row["said"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }


        public List<EN.LinewiseTuesReport> GetExportLoaded()
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetExportLoaded();
            List<EN.LinewiseTuesReport> ContainerList = new List<EN.LinewiseTuesReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.LinewiseTuesReport Tues1 = new EN.LinewiseTuesReport();
                    Tues1.srno = Convert.ToInt32(Count++);
                    Tues1.Lines = Convert.ToString(row["LINE"]);
                    Tues1.Size20 = Convert.ToString(row["SIZE_20"]);
                    Tues1.Size40 = Convert.ToString(row["SIZE_40"]);
                    Tues1.Size45 = Convert.ToString(row["SIZE_45"]);
                    Tues1.Total = Convert.ToString(row["TOTAL"]);
                    Tues1.Tues = Convert.ToString(row["TEUS"]);
                    Tues1.LineID = Convert.ToString(row["sLID"]);
                    Tues1.SlCode = Convert.ToString(row["said"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        // For 17 July 2019


        public List<EN.DoValidityEntities> GetDoValidityExpired()
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetDOValidityExpired();
            List<EN.DoValidityEntities> DoValidityExpired = new List<EN.DoValidityEntities>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.DoValidityEntities DoValidityList = new EN.DoValidityEntities();

                    DoValidityList.SrNo = Convert.ToInt16(Count++);
                    DoValidityList.ContainerNo = Convert.ToString(row["Container No"]);
                    DoValidityList.Size = Convert.ToString(row["Size"]);
                    DoValidityList.Type = Convert.ToString(row["Type"]);
                    DoValidityList.InDate = Convert.ToString(row["In Date"]);
                    DoValidityList.dovalidity = Convert.ToString(row["dovalidity"]);
                    DoValidityList.SLName = Convert.ToString(row["Line Name"]);
                    DoValidityList.DwellDays = Convert.ToString(row["Dwell Days"]);
                    DoValidityList.Yardoffloading = Convert.ToString(row["yardoffloading"]);

                    DoValidityExpired.Add(DoValidityList);
                }

            }
            return DoValidityExpired;

        }

        public List<EN.DoValidityEntities> GetDoValidityExpiringInSevenDays()
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetDOValidityExpiringinSevenDays();
            List<EN.DoValidityEntities> DoValidityExpired = new List<EN.DoValidityEntities>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.DoValidityEntities DoValidityList = new EN.DoValidityEntities();

                    DoValidityList.SrNo = Convert.ToInt16(Count++);
                    DoValidityList.ContainerNo = Convert.ToString(row["Container No"]);
                    DoValidityList.Size = Convert.ToString(row["Size"]);
                    DoValidityList.Type = Convert.ToString(row["Type"]);
                    DoValidityList.InDate = Convert.ToString(row["In Date"]);
                    DoValidityList.dovalidity = Convert.ToString(row["dovalidity"]);
                    DoValidityList.SLName = Convert.ToString(row["Line Name"]);
                    DoValidityList.DwellDays = Convert.ToString(row["Dwell Days"]);
                    DoValidityList.Yardoffloading = Convert.ToString(row["yardoffloading"]);

                    DoValidityExpired.Add(DoValidityList);
                }

            }
            return DoValidityExpired;

        }

        public List<EN.DoValidityEntities> GetInventoryCountDetails(string Activity, int slid, int size)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetInvenotyCountDetails(Activity, slid, size);
            List<EN.DoValidityEntities> GetInventoryList = new List<EN.DoValidityEntities>();
          
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.DoValidityEntities INventoryLIst = new EN.DoValidityEntities();

                    INventoryLIst.ContainerNo = Convert.ToString(row["containerno"]);
                    INventoryLIst.Size = Convert.ToString(row["size"]);
                    INventoryLIst.Type = Convert.ToString(row["ContainerType"]);
                    INventoryLIst.InDate = Convert.ToString(row["indate"]);
                    INventoryLIst.dovalidity = Convert.ToString(row["dovalidity"]);
                    INventoryLIst.SLName = Convert.ToString(row["slname"]);
                    INventoryLIst.DwellDays = Convert.ToString(row["Dwell Days"]);
                    INventoryLIst.Yardoffloading = Convert.ToString(row["yardoffloading"]);
                    //INventoryLIst.ContainerNo = Convert.ToString(row["Containerno"]);
                    //INventoryLIst.Size = Convert.ToString(row["size"]);


                    GetInventoryList.Add(INventoryLIst);
                }

            }
            return GetInventoryList;

        }


        public List<EN.UpcomingVehicleStatusEntities> GetUpComingVehicleDetails()
        {

            List<EN.UpcomingVehicleStatusEntities> GetUploadingVehicleLIst = new List<EN.UpcomingVehicleStatusEntities>();
            DataTable UpcomingDL = new DataTable();
            UpcomingDL = trackerdatamanager.GetUpComingVehicleStatus();
            int Count = 1;
            if(UpcomingDL.Rows.Count != 0)
            {
                foreach (DataRow row in UpcomingDL.Rows)
                {
                    EN.UpcomingVehicleStatusEntities UpComingList = new EN.UpcomingVehicleStatusEntities();
                    UpComingList.SrNo = Convert.ToInt16(Count++);
                    UpComingList.Date = Convert.ToString(row["Date"]);
                    UpComingList.Time = Convert.ToString(row["Time"]);
                    UpComingList.Trailername = Convert.ToString(row["Vehicle No"]);
                    UpComingList.Activity = Convert.ToString(row["Activity"]);
                    UpComingList.agname = Convert.ToString(row["Party"]);
                    UpComingList.Location = Convert.ToString(row["Location"]);
                    UpComingList.ContainerNo = Convert.ToString(row["Container No"]);
                    UpComingList.Size = Convert.ToString(row["Size"]);
                    UpComingList.slname = Convert.ToString(row["Line"]);
                    UpComingList.TotalHours = Convert.ToString(row["Total Hours"]);
                    UpComingList.DelayHours = Convert.ToString(row["Delay Hours"]);
                    UpComingList.KAM = Convert.ToString(row["KAM Name"]);
                    GetUploadingVehicleLIst.Add(UpComingList);

                }
            }
            return GetUploadingVehicleLIst;
        }





        public List<EN.CustomerWiseLDDReport> GetCustomerWiseImportInventory(string customerid,string date)
        {

            List<EN.CustomerWiseLDDReport> CustomerInventory = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetCustomerWiseImportInventory(customerid, date);

            if(CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport CustomerDL = new EN.CustomerWiseLDDReport();
                    CustomerDL.CustomerNAme = Convert.ToString(row["Customer"]);
                    CustomerDL.T20 = Convert.ToString(row["T20"]);
                    CustomerDL.T40 = Convert.ToString(row["T40"]);
                    CustomerDL.T45 = Convert.ToString(row["T45"]);
                    CustomerDL.Total = Convert.ToString(row["Total"]);
                    CustomerDL.Teus = Convert.ToString(row["TEUS"]);
                    CustomerInventory.Add(CustomerDL);
                }
            }
            return CustomerInventory;


        }


        public List<EN.CustomerWiseLDDReport> GetSalesWiseImportInventory(string salesid, string date)
        {

            List<EN.CustomerWiseLDDReport> SalesInventory = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetSalesWiseImportInventory(salesid, date);

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport SalesDL = new EN.CustomerWiseLDDReport();
                    SalesDL.SalesName = Convert.ToString(row["Sales Person"]);
                    SalesDL.T20 = Convert.ToString(row["T20"]);
                    SalesDL.T40 = Convert.ToString(row["T40"]);
                    SalesDL.T45 = Convert.ToString(row["T45"]);
                    SalesDL.Total = Convert.ToString(row["Total"]);
                    SalesDL.Teus = Convert.ToString(row["TEUS"]);
                    SalesInventory.Add(SalesDL);
                }
            }
            return SalesInventory;


        }


        public List<EN.CustomerWiseLDDReport> GetPortWisePendency()
        {

            List<EN.CustomerWiseLDDReport> SalesInventory = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GePortWisePendency();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport SalesDL = new EN.CustomerWiseLDDReport();
                    SalesDL.Port = Convert.ToString(row["portname"]);
                    SalesDL.T20 = Convert.ToString(row["T20"]);
                    SalesDL.T40 = Convert.ToString(row["T40"]);
                    SalesDL.T45 = Convert.ToString(row["T45"]);
                    SalesDL.Total = Convert.ToString(row["Total"]);
                    SalesDL.Teus = Convert.ToString(row["TEUS"]);
                    SalesInventory.Add(SalesDL);
                }
            }
            return SalesInventory;


        }
        //Code end by Prashant 

        public List<EN.RevenueAlloperationEntities> GetImportEmptyPortMovement()
        {

            List<EN.RevenueAlloperationEntities> SalesInventory = new List<EN.RevenueAlloperationEntities>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetimportEmptyPortMovement();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.RevenueAlloperationEntities SalesDL = new EN.RevenueAlloperationEntities();
                    SalesDL.PortName = Convert.ToString(row["PORTNAME"]);
                    SalesDL.Rail20 = Convert.ToString(row["Size20 Rail"]);
                    SalesDL.Rail40 = Convert.ToString(row["Size40 Rail"]);
                    SalesDL.Rail45 = Convert.ToString(row["Size45 Rail"]);
                    SalesDL.Road20 = Convert.ToString(row["Size20 Road"]);
                    SalesDL.Road40 = Convert.ToString(row["Size40 Road"]);
                    SalesDL.Road45 = Convert.ToString(row["Size45 Road"]);
                    SalesDL.TotalTues = Convert.ToString(row["Total Tues"]);
                    SalesInventory.Add(SalesDL);
                }
            }
            return SalesInventory;


        }


        public List<EN.RevenueAlloperationEntities> GetExportEmptyPortMovement(string fromdate,string todate)
        {

            List<EN.RevenueAlloperationEntities> SalesInventory = new List<EN.RevenueAlloperationEntities>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetExportEmptyPortMovement(fromdate, todate);

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.RevenueAlloperationEntities SalesDL = new EN.RevenueAlloperationEntities();
                    SalesDL.PortName = Convert.ToString(row["Portname"]);
                    SalesDL.Rail20 = Convert.ToString(row["Size20 Rail"]);
                    SalesDL.Rail40 = Convert.ToString(row["Size40 Rail"]);
                    SalesDL.Rail45 = Convert.ToString(row["Size45 Rail"]);
                    SalesDL.Road20 = Convert.ToString(row["Size20 Road"]);
                    SalesDL.Road40 = Convert.ToString(row["Size40 Road"]);
                    SalesDL.Road45 = Convert.ToString(row["Size45 Road"]);
                    SalesDL.TotalTues = Convert.ToString(row["Total Tues"]);
                    SalesInventory.Add(SalesDL);
                }
            }
            return SalesInventory;


        }

        public List<EN.RevenueAlloperationEntities> GetHaziraEmptyPortMovement(string fromdate, string todate)
        {

            List<EN.RevenueAlloperationEntities> SalesInventory = new List<EN.RevenueAlloperationEntities>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetHaziraEmptyPortMovement(fromdate, todate);

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.RevenueAlloperationEntities SalesDL = new EN.RevenueAlloperationEntities();
                    SalesDL.PortName = Convert.ToString(row["Portname"]);
                    SalesDL.Rail20 = Convert.ToString(row["Size20 Rail"]);
                    SalesDL.Rail40 = Convert.ToString(row["Size40 Rail"]);
                    SalesDL.Rail45 = Convert.ToString(row["Size45 Rail"]);
                    SalesDL.Road20 = Convert.ToString(row["Size20 Road"]);
                    SalesDL.Road40 = Convert.ToString(row["Size40 Road"]);
                    SalesDL.Road45 = Convert.ToString(row["Size45 Road"]);
                    SalesDL.TotalTues = Convert.ToString(row["Total Tues"]);
                    SalesInventory.Add(SalesDL);
                }
            }
            return SalesInventory;


        }

        public List<EN.RevenueAlloperationICDEntities> GetRevenue(string fromdate, string todate)
        {

            List<EN.RevenueAlloperationICDEntities> RevenueList = new List<EN.RevenueAlloperationICDEntities>();
            DataTable RevenueDL = new DataTable();
            RevenueDL = trackerdatamanager.GetRevenueOpeations(fromdate, todate);

            if (RevenueDL.Rows.Count != 0)
            {

                foreach (DataRow row in RevenueDL.Rows)
                {
                    EN.RevenueAlloperationICDEntities SalesDL = new EN.RevenueAlloperationICDEntities();
                    SalesDL.EventName = Convert.ToString(row["Event Name"]);
                    SalesDL.NoOFRecord = Convert.ToString(row["No. of Record"]);
                    SalesDL.NetAmount = Convert.ToString(row["Net Amount"]);
                    SalesDL.GST = Convert.ToString(row["GST"]);
                    SalesDL.Amount = Convert.ToString(row["Amount"]);
                
                    RevenueList.Add(SalesDL);
                }
            }
            return RevenueList;



        }

        public List<EN.FuelStockSummary> GetFuelStockSummary(string cmbFueltype, string FromDate, string ToDate)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetFuelStockSummary(cmbFueltype, FromDate, ToDate);
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

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        public List<EN.TrailerDropDown> GetTrailerDropDown()
        {
            DataTable DT = new DataTable();
            DT = trackerdatamanager.GetTrailerNo();
            List<EN.TrailerDropDown> ShippingList = new List<EN.TrailerDropDown>();

            if (DT.Rows.Count != 0)
            {
                foreach (DataRow row in DT.Rows)
                {

                    EN.TrailerDropDown DL = new EN.TrailerDropDown();

                    DL.trailerid = Convert.ToInt16(row["trailerid"]);
                    DL.trailername = Convert.ToString(row["trailername"]);
                    ShippingList.Add(DL);
                }
            }
            return ShippingList;

        }
        public List<EN.VoucherNoDropDown> GetVOucherDropDown()
        {
            DataTable DT = new DataTable();
            DT = trackerdatamanager.GetVehicleNO();
            List<EN.VoucherNoDropDown> ShippingList = new List<EN.VoucherNoDropDown>();

            if (DT.Rows.Count != 0)
            {
                foreach (DataRow row in DT.Rows)
                {

                    EN.VoucherNoDropDown DL = new EN.VoucherNoDropDown();

                    DL.tripID = Convert.ToInt16(row["tripID"]);
                    DL.VoucherNo = Convert.ToString(row["VoucherNo"]);
                    ShippingList.Add(DL);
                }
            }
            return ShippingList;

        }
        public List<EN.SlipDropDown> GetSlipDropDown()
        {
            DataTable DT = new DataTable();
            DT = trackerdatamanager.GetSlipNO();
            List<EN.SlipDropDown> ShippingList = new List<EN.SlipDropDown>();

            if (DT.Rows.Count != 0)
            {
                foreach (DataRow row in DT.Rows)
                {

                    EN.SlipDropDown DL = new EN.SlipDropDown();

                    DL.SLipID = Convert.ToString(row["SLipID"]);
                    DL.SlipNo = Convert.ToString(row["SlipNo"]);
                    ShippingList.Add(DL);
                }
            }
            return ShippingList;

        }
        public List<EN.TPDropDown> GetTPDropDown()
        {
            DataTable DT = new DataTable();
            DT = trackerdatamanager.GetTPNO();
            List<EN.TPDropDown> ShippingList = new List<EN.TPDropDown>();

            if (DT.Rows.Count != 0)
            {
                foreach (DataRow row in DT.Rows)
                {

                    EN.TPDropDown DL = new EN.TPDropDown();

                    DL.TPNo = Convert.ToInt16(row["TPNo"]);
                    DL.TPSLipNo = Convert.ToString(row["TPSLipNo"]);
                    ShippingList.Add(DL);
                }
            }
            return ShippingList;

        }
        // Codes By Prashant
        public List<EN.VoucherDetailsEntities> GetMovementDetails(string VoucherNo, string SlipNo, string Containerno, string TrailerNo)
        {
            List<EN.VoucherDetailsEntities> VoucherDetailsDL = new List<EN.VoucherDetailsEntities>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.MovementDetails(VoucherNo, SlipNo, Containerno, TrailerNo);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.VoucherDetailsEntities Voucher = new EN.VoucherDetailsEntities();

                    Voucher.VoucherDate = Convert.ToString(row["VoucherDate"]);
                    Voucher.TrailerNo = Convert.ToString(row["TrailerNo"]);
                    Voucher.Status = Convert.ToString(row["Status"]);
                    Voucher.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    Voucher.Size = Convert.ToString(row["Size"]);
                    Voucher.Drivername = Convert.ToString(row["Drivername"]);
                    Voucher.Activity = Convert.ToString(row["Activity"]);
                    Voucher.AmountCash = Convert.ToString(row["AmountCash"]);
                    Voucher.AmountBank = Convert.ToString(row["AmountBank"]);
                    Voucher.Fuel = Convert.ToString(row["Fuel"]);
                    Voucher.AdjustmentsFuel = Convert.ToString(row["AdjustmentsFuel"]);
                    Voucher.FromLocation = Convert.ToString(row["FromLocation"]);
                    Voucher.ToLocation = Convert.ToString(row["ToLocation"]);
                    Voucher.StartReading = Convert.ToString(row["StartReading"]);
                    Voucher.EndReading = Convert.ToString(row["EndReading"]);
                    Voucher.VoucherNO = Convert.ToString(row["VoucherNo"]);
                    Voucher.SlipNo = Convert.ToString(row["SlipNo"]);
                    Voucher.InOut = Convert.ToString(row["InOut"]);
                    Voucher.ENTRY_Type = Convert.ToString(row["ENTRY_Type"]);
                    Voucher.CustomerName = Convert.ToString(row["CustomerName"]);
                    Voucher.CustomerAddress = Convert.ToString(row["CustomerAddress"]);
                    Voucher.TrollySize = Convert.ToString(row["TrollySize"]);
                    Voucher.FullSlipVendorBillNo = Convert.ToString(row["Full Slip Vendor Bill No"]);
                    Voucher.CashPaymentTransID = Convert.ToString(row["Cash Payment Trans ID"]);


                    VoucherDetailsDL.Add(Voucher);
                }
            }



            return VoucherDetailsDL;
        }
        // Codes end By Prashant


        public List<EN.TPEntryEntities> AjaxGetTPCloseDetailsList(string TpNumber)
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetVoucherTPCloseDetailsList(TpNumber);
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
                    DL.ISApproved = Convert.ToString(row["isapproved"]);
                    DL.AddedBy = Convert.ToString(row["username"]);
                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }

        public List<EN.VoucherDetailsEntities> GettrailerMovementDetails(string TrailerNo, string fromdate, string Todate)
        {
            List<EN.VoucherDetailsEntities> VoucherDetailsDL = new List<EN.VoucherDetailsEntities>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.gettrailerMovementDetails(TrailerNo, fromdate, Todate);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.VoucherDetailsEntities Voucher = new EN.VoucherDetailsEntities();

                    Voucher.VoucherDate = Convert.ToString(row["VoucherDate"]);
                    Voucher.TrailerNo = Convert.ToString(row["TrailerNo"]);
                    Voucher.Status = Convert.ToString(row["Status"]);
                    Voucher.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    Voucher.Size = Convert.ToString(row["Size"]);
                    Voucher.Drivername = Convert.ToString(row["Drivername"]);
                    Voucher.Activity = Convert.ToString(row["Activity"]);
                    Voucher.AmountCash = Convert.ToString(row["AmountCash"]);
                    Voucher.AmountBank = Convert.ToString(row["AmountBank"]);
                    Voucher.Fuel = Convert.ToString(row["Fuel"]);
                    Voucher.AdjustmentsFuel = Convert.ToString(row["AdjustmentsFuel"]);
                    Voucher.FromLocation = Convert.ToString(row["FromLocation"]);
                    Voucher.ToLocation = Convert.ToString(row["ToLocation"]);
                    Voucher.StartReading = Convert.ToString(row["StartReading"]);
                    Voucher.EndReading = Convert.ToString(row["EndReading"]);
                    Voucher.VoucherNO = Convert.ToString(row["VoucherNo"]);
                    Voucher.SlipNo = Convert.ToString(row["SlipNo"]);
                    Voucher.InOut = Convert.ToString(row["InOut"]);
                    Voucher.ENTRY_Type = Convert.ToString(row["ENTRY_Type"]);
                    Voucher.CustomerName = Convert.ToString(row["CustomerName"]);
                    Voucher.CustomerAddress = Convert.ToString(row["CustomerAddress"]);
                    Voucher.TrollySize = Convert.ToString(row["TrollySize"]);
                    Voucher.FullSlipVendorBillNo = Convert.ToString(row["Full Slip Vendor Bill No"]);
                    Voucher.CashPaymentTransID = Convert.ToString(row["Cash Payment Trans ID"]);


                    VoucherDetailsDL.Add(Voucher);
                }
            }



            return VoucherDetailsDL;
        }
        public List<EN.TPEntryEntities> AjaxTrailerGetTPCloseDetailsList(string trailerno)
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetTrailerTPCloseDetailsList(trailerno);
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
                    DL.ISApproved = Convert.ToString(row["isapproved"]);
                    DL.AddedBy = Convert.ToString(row["username"]);
                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }


        public List<EN.MonthlyEntites> GetMonthlyreport()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetMonthlyReport();
            List<EN.MonthlyEntites> GetTPDataList = new List<EN.MonthlyEntites>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.MonthlyEntites DL = new EN.MonthlyEntites();
                    DL.Year = Convert.ToString(row["Year"]);
                    DL.MonthNo = Convert.ToString(row["MonthNo"]);
                    DL.MonthName = Convert.ToString(row["MonthName"]);
                    DL.GetName = Convert.ToString(row["Grandtotal"]);
                   
                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }

        public List<EN.CustomerWiseLDDReport> GetImportJo()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetIMportInHand();
            List<EN.CustomerWiseLDDReport> GetTPDataList = new List<EN.CustomerWiseLDDReport>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.CustomerWiseLDDReport DL = new EN.CustomerWiseLDDReport();
                    DL.Port = Convert.ToString(row["portname"]);
                    DL.T20 = Convert.ToString(row["T20"]);
                    DL.T40 = Convert.ToString(row["T40"]);
                    DL.T45 = Convert.ToString(row["T45"]);
                    DL.Total = Convert.ToString(row["Total"]);
                    DL.Teus = Convert.ToString(row["TEUS"]);
                   

                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }
        public List<EN.CustomerWiseLDDReport> GetImportDelivery()
        {

            List<EN.CustomerWiseLDDReport> SalesInventory = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetImPortDelivery();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport SalesDL = new EN.CustomerWiseLDDReport();
                    SalesDL.Process = Convert.ToString(row["process"]);
                    SalesDL.T20 = Convert.ToString(row["size20"]);
                    SalesDL.T40 = Convert.ToString(row["size40"]);
                    SalesDL.T45 = Convert.ToString(row["size45"]);

                    SalesDL.Total = Convert.ToString(row["TOTAL"]);
                    SalesDL.Teus = Convert.ToString(row["TEUS"]);
                    SalesInventory.Add(SalesDL);
                }
            }
            return SalesInventory;


        }

        public List<EN.CustomerWiseLDDReport> GetImportStuffDelivery()
        {

            List<EN.CustomerWiseLDDReport> SalesInventory = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetDestuffDelivery();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport SalesDL = new EN.CustomerWiseLDDReport();
                    SalesDL.Process = Convert.ToString(row["process"]);
                    SalesDL.T20 = Convert.ToString(row["size20"]);
                    SalesDL.T40 = Convert.ToString(row["size40"]);
                    SalesDL.T45 = Convert.ToString(row["size45"]);
                    SalesDL.Total = Convert.ToString(row["TOTAL"]);
                    SalesDL.Teus = Convert.ToString(row["TEUS"]);
                    SalesInventory.Add(SalesDL);
                }
            }
            return SalesInventory;


        }

        public List<EN.CustomerWiseLDDReport> GetImportInventory()
        {

            List<EN.CustomerWiseLDDReport> SalesInventory = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetImportInventory();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport SalesDL = new EN.CustomerWiseLDDReport();
                    SalesDL.Process = Convert.ToString(row["Process"]);
                    SalesDL.T20 = Convert.ToString(row["20"]);
                    SalesDL.T40 = Convert.ToString(row["40"]);
                    SalesDL.T45 = Convert.ToString(row["45"]);
                    SalesDL.Total = Convert.ToString(row["TOTAL"]);
                    SalesDL.Teus = Convert.ToString(row["TEUS"]);
                    SalesInventory.Add(SalesDL);
                }
            }
            return SalesInventory;


        }


        // ICD - Export Summary

        public List<EN.CustomerWiseLDDReport> GetICDExportArrival()
        {

            List<EN.CustomerWiseLDDReport> ICDExportLIst = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDImportArrival();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["size20"]);
                    ICDExportDL.T40 = Convert.ToString(row["size40"]);
                    ICDExportDL.T45 = Convert.ToString(row["size45"]);
                    ICDExportDL.Total = Convert.ToString(row["TOTAL"]);
                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;


        }

        public int GetICDExportArrivalCOunt()
        {
            int message = 0; ;
            EN.CustomerWiseLDDReport ICDExportLIst = new EN.CustomerWiseLDDReport();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDImportArrival();
          //  string T45Total = "";
            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportLIst.Process = Convert.ToString(row["process"]);
                    ICDExportLIst.T20 = Convert.ToString(row["size20"]);
                    ICDExportLIst.T40 = Convert.ToString(row["size40"]);
                    ICDExportLIst.T45 = Convert.ToString(row["size45"]);
                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    if (ICDExportLIst.Process == "Current Month")
                    {
                        message = Convert.ToInt16(ICDExportDL.Teus);
                    }
                 
                }
            }
            return message;


        }

        public List<EN.CustomerWiseLDDReport> GetICDExportStuffed()
        {

            List<EN.CustomerWiseLDDReport> ICDExportstuffedLIst = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDImportExportStuffed();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["SIZE20"]);
                    ICDExportDL.T40 = Convert.ToString(row["SIZE40"]);
                    ICDExportDL.T45 = Convert.ToString(row["SIZE45"]);
                    ICDExportDL.Total = Convert.ToString(row["TOTAL"]);
                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    ICDExportstuffedLIst.Add(ICDExportDL);
                }
            }
            return ICDExportstuffedLIst;


        }

        public int GetICDExportstuffedCOunt()
        {
            int message = 0; ;
            EN.CustomerWiseLDDReport ICDExportLIst = new EN.CustomerWiseLDDReport();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDImportExportStuffed();
            //  string T45Total = "";
            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportLIst.Process = Convert.ToString(row["process"]);
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["SIZE20"]);
                    ICDExportDL.T40 = Convert.ToString(row["SIZE40"]);
                    ICDExportDL.T45 = Convert.ToString(row["SIZE45"]);

                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    if (ICDExportLIst.Process == "Current Month")
                    {
                        message = Convert.ToInt16(ICDExportDL.Teus);
                    }

                }
            }
            return message;


        }

        // ICD Export Movement
        public List<EN.CustomerWiseLDDReport> GetICDExportMovement()
        {

            List<EN.CustomerWiseLDDReport> ICDExportLIst = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportMovement();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["size20"]);
                    ICDExportDL.T40 = Convert.ToString(row["size40"]);
                    ICDExportDL.T45 = Convert.ToString(row["size45"]);
                    ICDExportDL.Total = Convert.ToString(row["TOTAL"]);
                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;


        }

        public int GetICDExportMovementCount()
        {
            int message = 0; ;
            EN.CustomerWiseLDDReport ICDExportLIst = new EN.CustomerWiseLDDReport();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportMovement();
            //  string T45Total = "";
            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportLIst.Process = Convert.ToString(row["process"]);
                    ICDExportLIst.T20 = Convert.ToString(row["size20"]);
                    ICDExportLIst.T40 = Convert.ToString(row["size40"]);
                    ICDExportLIst.T45 = Convert.ToString(row["size45"]);
                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    if (ICDExportLIst.Process == "Current Month")
                    {
                        message = Convert.ToInt16(ICDExportDL.Teus);
                    }

                }
            }
            return message;


        }

        //ICD Export Loaded inventory

        public List<EN.CustomerWiseLDDReport> GetICDExportInventory()
        {

            List<EN.CustomerWiseLDDReport> ICDExportLIst = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportInventory();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["Process"]);
                    ICDExportDL.T20 = Convert.ToString(row["20"]);
                    ICDExportDL.T40 = Convert.ToString(row["40"]);
                    ICDExportDL.T45 = Convert.ToString(row["45"]);
                    ICDExportDL.Total = Convert.ToString(row["TOTAL"]);
                    ICDExportDL.Teus = Convert.ToString(row["Tues"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;


        }

        public int GetICDExportInventoryCount()
        {
            int message = 0; ;
            EN.CustomerWiseLDDReport ICDExportLIst = new EN.CustomerWiseLDDReport();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportInventory();
            //  string T45Total = "";
            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportLIst.Process = Convert.ToString(row["Process"]);
                    ICDExportLIst.T20 = Convert.ToString(row["20"]);
                    ICDExportLIst.T40 = Convert.ToString(row["40"]);
                    ICDExportLIst.T45 = Convert.ToString(row["45"]);
                    ICDExportDL.Teus = Convert.ToString(row["Tues"]);
                   
                        message = message + Convert.ToInt16(ICDExportDL.Teus);
                   

                }
            }
            return message;


        }


        // Code For ICD empty Out

        public List<EN.CustomerWiseLDDReport> GetICDExportemptyOut()
        {

            List<EN.CustomerWiseLDDReport> ICDExportLIst = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportEmptyOut();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["size20"]);
                    ICDExportDL.T40 = Convert.ToString(row["size40"]);
                    ICDExportDL.T45 = Convert.ToString(row["size45"]);
                    ICDExportDL.Total = Convert.ToString(row["TOTAL"]);
                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;


        }

        public int GetICDExportEmptyOutCount()
        {
            int message = 0; ;
            EN.CustomerWiseLDDReport ICDExportLIst = new EN.CustomerWiseLDDReport();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportEmptyOut();
            //  string T45Total = "";
            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportLIst.Process = Convert.ToString(row["process"]);
                    ICDExportLIst.T20 = Convert.ToString(row["size20"]);
                    ICDExportLIst.T40 = Convert.ToString(row["size40"]);
                    ICDExportLIst.T45 = Convert.ToString(row["size45"]);
                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    if (ICDExportLIst.Process == "Current Month")
                    {
                        message = Convert.ToInt16(ICDExportDL.Teus);
                    }

                }
            }
            return message;


        }



        // COdes for ICD - Export Month Wise Collection


      

        

        public List<EN.ActivityMaster> getActivityMasterCreditNote()
        {
            List<EN.ActivityMaster> passingDL = new List<EN.ActivityMaster>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getActivityMasterCreditNote();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ActivityMaster PassingList = new EN.ActivityMaster();
                    PassingList.TYPEID = Convert.ToString(row["TYPEID"]);
                    PassingList.Activity = Convert.ToString(row["TYPE"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.PartyMaster> getPartyMaster()
        {
            List<EN.PartyMaster> passingDL = new List<EN.PartyMaster>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getPartyMaster();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.PartyMaster PassingList = new EN.PartyMaster();
                    PassingList.GSTID = Convert.ToInt32(row["GSTID"]);
                    PassingList.GSTName = Convert.ToString(row["GSTName"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.CategorywiseInvoice> GetSearchInvoice(string category, string Search, string txtItemNo)
        {
            List<EN.CategorywiseInvoice> passingDL = new List<EN.CategorywiseInvoice>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetSearchInvoice(category, Search, txtItemNo);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CategorywiseInvoice PassingList = new EN.CategorywiseInvoice();
                    PassingList.SRNO = Convert.ToInt32(Count++);
                    PassingList.Category = Convert.ToString(row["Category"]);
                    PassingList.AssessNo = Convert.ToString(row["Assessment No"]);
                    PassingList.WorkYear = Convert.ToString(row["Work Year"]);
                    PassingList.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    PassingList.InvoiceDate = Convert.ToString(row["Invoice Date"]);
                    PassingList.GrandTotal = Convert.ToInt32(row["Grand Total"]);
                   PassingList.BtnEditCss = Convert.ToString(row["BtnEditCss"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.LinewiseTuesReport> GetLineWiseOpeningTuesReport(string Fromdate, string Todate)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.GetLineWiseOpeningTuesReport( Fromdate, Todate);
            List<EN.LinewiseTuesReport> ContainerList = new List<EN.LinewiseTuesReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.LinewiseTuesReport Tues1 = new EN.LinewiseTuesReport();
                    Tues1.srno = Convert.ToInt32(Count++);
                    Tues1.Lines = Convert.ToString(row["Line Name"]);
                    Tues1.Opening = Convert.ToString(row["Opening"]);
                    Tues1.Balance = Convert.ToString(row["Balance"]);
                    Tues1.Destuff = Convert.ToString(row["Destuff"]);
                    Tues1.Container = Convert.ToString(row["Container"]);
                    Tues1.Factory = Convert.ToString(row["Factory"]);
                    Tues1.Return = Convert.ToString(row["Return"]);
                    Tues1.Userfor = Convert.ToString(row["Userfor"]);
                    Tues1.Export = Convert.ToString(row["Export"]);
                    Tues1.Empty = Convert.ToString(row["Empty"]);
                    Tues1.Repo = Convert.ToString(row["Repo"]);
                    Tues1.Other = Convert.ToString(row["Other"]);
                    Tues1.Yard = Convert.ToString(row["Yard"]);
                    Tues1.closing = Convert.ToString(row["closing"]);
                    Tues1.Balances = Convert.ToString(row["Balances"]);
                 
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }



        public List<EN.CriteriaEntities> GetExisitingAccountName(string name, int id)
        {
            DataTable dt = new DataTable();

            List<EN.CriteriaEntities> AccountHeadList = new List<EN.CriteriaEntities>();
            dt = trackerdatamanager.GetExisitingAccountName(name, id);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.CriteriaEntities AccountDl = new EN.CriteriaEntities();
                    AccountDl.Criteria = Convert.ToString(row["AccountName"]);
                    AccountDl.ID = Convert.ToInt32(row["AccountId"]);
                    AccountHeadList.Add(AccountDl);
                }
            }
            return AccountHeadList;
        }

        public List<EN.CriteriaEntities> GetExisitingSlnameName(string name, int id)
        {
            DataTable dt = new DataTable();

            List<EN.CriteriaEntities> SlnameList = new List<EN.CriteriaEntities>();
            dt = trackerdatamanager.GetExisitingSlnameName(name, id);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.CriteriaEntities SlnameDl = new EN.CriteriaEntities();
                    SlnameDl.Criteria = Convert.ToString(row["SLName"]);
                    SlnameDl.ID = Convert.ToInt32(row["SLId"]);
                    SlnameList.Add(SlnameDl);
                }
            }
            return SlnameList;
        }

       
        public List<EN.CategorySearchInvoice> GetAssessNoInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<EN.CategorySearchInvoice> passingDL = new List<EN.CategorySearchInvoice>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetSearchInvoiceVeiw(AssessNo, WorkYear, Category);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CategorySearchInvoice PassingList = new EN.CategorySearchInvoice();
                    PassingList.Category = Convert.ToString(row["Category"]);
                    PassingList.AssessNo = Convert.ToString(row["AssessNo"]);
                    PassingList.AssessYear = Convert.ToString(row["WorkYear"]);
                    PassingList.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    PassingList.AssessDate = Convert.ToString(row["AssessDate"]);
                    PassingList.ValidData = Convert.ToString(row["ValidUptoDate"]);
                    PassingList.AssessType = Convert.ToString(row["AssessType"]);
                    PassingList.DeliveryType = Convert.ToString(row["DeliveryType"]);
                    PassingList.BIrtingDate = Convert.ToString(row["Birthdt"]);
                    PassingList.IGMNo = Convert.ToString(row["IGMNo"]);
                    PassingList.ItemNo = Convert.ToString(row["ItemNo"]);
                    PassingList.BlNumberNO = Convert.ToString(row["BLNumber"]);
                    PassingList.TariffID = Convert.ToString(row["TariffID"]);
                    PassingList.Des = Convert.ToString(row["TariffDescription"]);
                    PassingList.LineName = Convert.ToString(row["SLName"]);
                    PassingList.CHaNAme = Convert.ToString(row["CHAName"]);
                    PassingList.Consignee = Convert.ToString(row["Consignee"]);
                    PassingList.WaiverRemarks = Convert.ToString(row["WRemarks"]);
                    PassingList.TallyLedger = Convert.ToString(row["LedgerName"]);
                    //PassingList.Remarks = Convert.ToString(row["BtnEditCss"]);
                    PassingList.AssessmentBy = Convert.ToString(row["Added By"]);
                    PassingList.AssessmentOn = Convert.ToString(row["AddedOn"]);
                    PassingList.ReceiptBy = Convert.ToString(row["UserName"]);
                    PassingList.ReceipOn = Convert.ToString(row["AddedOn"]);
                    PassingList.NetTotal = Convert.ToString(row["NetTotal"]);
                    PassingList.DiscountAmt = Convert.ToString(row["DiscAmount"]);
                    PassingList.TariffMappedBy = Convert.ToString(row["Name"]);
                    PassingList.TariffMappedOn = Convert.ToString(row["AddedOn"]);
                    PassingList.ServiceTax = Convert.ToString(row["ServiceTax"]);
                    PassingList.SGST = Convert.ToString(row["SGST"]);
                    //PassingList.RebataBillNo = Convert.ToString(row["BtnEditCss"]);
                    //PassingList.PortChargesinvoiceNo = Convert.ToString(row["BtnEditCss"]);
                    PassingList.CGST = Convert.ToString(row["CGST"]);
                    PassingList.IGST = Convert.ToString(row["IGST"]);
                    PassingList.GSTInNumber = Convert.ToString(row["GSTIn_uniqID"]);
                    PassingList.PartyName = Convert.ToString(row["GSTName"]);
                    PassingList.GrandTotal = Convert.ToString(row["GrandTotal"]);
                    PassingList.Checkedby = Convert.ToString(row["Checked By"]);
                    PassingList.AuditRemark = Convert.ToString(row["CheckedRemarks"]);
                    PassingList.Value = Convert.ToString(row["Value"]);
                    PassingList.Duty = Convert.ToString(row["Duty"]);
                    PassingList.Jono = Convert.ToString(row["JONo"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.CategorySearchInvoice> ExportAssessNoInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<EN.CategorySearchInvoice> passingDL = new List<EN.CategorySearchInvoice>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.ExportSearchInvoiceVeiw(AssessNo, WorkYear, Category);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CategorySearchInvoice PassingList = new EN.CategorySearchInvoice();
                    PassingList.Category = Convert.ToString(row["Category"]);
                    PassingList.AssessNo = Convert.ToString(row["AssessNo"]);
                    PassingList.AssessYear = Convert.ToString(row["WorkYear"]);
                    PassingList.InvoiceNo = Convert.ToString(row["InvNo"]);
                    PassingList.AssessDate = Convert.ToString(row["assessdate"]);
                    PassingList.ValidData = Convert.ToString(row["ValidUptodate"]);
                    PassingList.AssessType = Convert.ToString(row["assesstype"]);
                    PassingList.DeliveryType = Convert.ToString(row["ECess"]);

                    PassingList.Sbno = Convert.ToString(row["SBNo"]);
                    PassingList.TariffID = Convert.ToString(row["tariffID"]);
                    PassingList.Des = Convert.ToString(row["TariffDescription"]);
                    PassingList.LineName = Convert.ToString(row["linename"]);
                    PassingList.CHaNAme = Convert.ToString(row["CHAName"]);
                    PassingList.Consignee = Convert.ToString(row["AGName"]);
                    PassingList.WaiverRemarks = Convert.ToString(row["DiscRemarks"]);
                    PassingList.Remarks = Convert.ToString(row["Remarks"]);
                    PassingList.ShipperName = Convert.ToString(row["shippername"]);
                    PassingList.AssessmentBy = Convert.ToString(row["Assess Prepared By"]);
                    PassingList.AssessmentOn = Convert.ToString(row["Assess Date"]);
                    PassingList.ReceiptBy = Convert.ToString(row["Receipt Prepared By"]);
                    PassingList.ReceipOn = Convert.ToString(row["Receipt Date"]);
                    PassingList.NetTotal = Convert.ToString(row["nettotal"]);
                    PassingList.DiscountAmt = Convert.ToString(row["DiscAmount"]);
                    //PassingList.TariffMappedBy = Convert.ToString(row["Name"]);
                    //PassingList.TariffMappedOn = Convert.ToString(row["AddedOn"]);
                    //PassingList.ServiceTax = Convert.ToString(row["ServiceTax"]);
                    PassingList.SGST = Convert.ToString(row["SGST"]);
                    //PassingList.RebataBillNo = Convert.ToString(row["BtnEditCss"]);
                    //PassingList.PortChargesinvoiceNo = Convert.ToString(row["BtnEditCss"]);
                    PassingList.CGST = Convert.ToString(row["CGST"]);
                    PassingList.IGST = Convert.ToString(row["IGST"]);
                    PassingList.GSTInNumber = Convert.ToString(row["GST Number"]);
                    PassingList.PartyName = Convert.ToString(row["GST Name"]);
                    PassingList.GrandTotal = Convert.ToString(row["grandtotal"]);

                    PassingList.AuditRemark = Convert.ToString(row["DiscRemarks"]);

                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.CategorySearchInvoice> BondAssessNoInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<EN.CategorySearchInvoice> passingDL = new List<EN.CategorySearchInvoice>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.BondSearchInvoiceVeiw(AssessNo, WorkYear, Category);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CategorySearchInvoice PassingList = new EN.CategorySearchInvoice();
                    PassingList.Category = Convert.ToString(row["Category"]);
                    PassingList.AssessNo = Convert.ToString(row["AssessNo"]);
                    PassingList.AssessYear = Convert.ToString(row["WorkYear"]);
                    PassingList.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    PassingList.AssessDate = Convert.ToString(row["Assessment Date"]);
                    PassingList.ValidData = Convert.ToString(row["ValidUptoDate"]);
                    PassingList.AssessType = Convert.ToString(row["BondType"]);
                    PassingList.DeliveryType = Convert.ToString(row["DeliveryType"]);
                    PassingList.IGMNo = Convert.ToString(row["IGMNo"]);
                    PassingList.ItemNo = Convert.ToString(row["ItemNo"]);
                    //PassingList.Sbno = Convert.ToString(row["SBNo"]);
                    PassingList.TariffID = Convert.ToString(row["TariffID"]);
                    PassingList.Des = Convert.ToString(row["TariffDescription"]);
                    PassingList.CHaNAme = Convert.ToString(row["CHAName"]);
                    PassingList.Consignee = Convert.ToString(row["ImporterName"]);
                    //PassingList.Value = Convert.ToString(row["Value"]);
                    //PassingList.Duty = Convert.ToString(row["Duty"]);
                    //PassingList.Remarks = Convert.ToString(row["Remarks"]);
                    PassingList.AssessmentBy = Convert.ToString(row["Assessmet Users"]);
                    PassingList.AssessmentOn = Convert.ToString(row["AddedOn"]);
                    PassingList.Noof20 = Convert.ToString(row["T20"]);
                    PassingList.Noof40 = Convert.ToString(row["T40"]);
                    //PassingList.ReceiptBy = Convert.ToString(row["Receipt Prepared By"]);
                    //PassingList.ReceipOn = Convert.ToString(row["Receipt Date"]);
                    PassingList.NetTotal = Convert.ToString(row["NetTotal"]);
                    //PassingList.DiscountAmt = Convert.ToString(row["DiscAmount"]);
                    PassingList.SGST = Convert.ToString(row["SGST"]);
                    PassingList.CGST = Convert.ToString(row["CGST"]);
                    PassingList.IGST = Convert.ToString(row["IGST"]);
                    PassingList.GSTInNumber = Convert.ToString(row["GSTIn_uniqID"]);
                    PassingList.PartyName = Convert.ToString(row["GSTName"]);
                    PassingList.GrandTotal = Convert.ToString(row["GrandTotal"]);



                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.CategorySearchInvoice> DomestcAssessNoInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<EN.CategorySearchInvoice> passingDL = new List<EN.CategorySearchInvoice>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.DomestcSearchInvoiceVeiw(AssessNo, WorkYear, Category);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CategorySearchInvoice PassingList = new EN.CategorySearchInvoice();
                    PassingList.Category = Convert.ToString(row["Category"]);
                    PassingList.AssessNo = Convert.ToString(row["AssessNo"]);
                    PassingList.AssessYear = Convert.ToString(row["WorkYear"]);
                    PassingList.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    PassingList.AssessDate = Convert.ToString(row["Assessment Date"]);
                    PassingList.ValidData = Convert.ToString(row["ValidUptoDate"]);
                    //PassingList.AssessType = Convert.ToString(row["BondType"]);
                    PassingList.DeliveryType = Convert.ToString(row["DeliveryType"]);



                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }


        public List<EN.CategorySearchInvoice> MNRAssessNoInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<EN.CategorySearchInvoice> passingDL = new List<EN.CategorySearchInvoice>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.MNRSearchInvoiceVeiw(AssessNo, WorkYear, Category);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CategorySearchInvoice PassingList = new EN.CategorySearchInvoice();
                    PassingList.Category = Convert.ToString(row["Category"]);
                    PassingList.AssessNo = Convert.ToString(row["AssessNo"]);
                    PassingList.AssessYear = Convert.ToString(row["WorkYear"]);
                    PassingList.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    PassingList.AssessDate = Convert.ToString(row["Assessment Date"]);
                    PassingList.ValidData = Convert.ToString(row["ValidUptoDate"]);
                    //PassingList.AssessType = Convert.ToString(row["BondType"]);
                    PassingList.DeliveryType = Convert.ToString(row["DeliveryType"]);



                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.CategorySearchInvoice> CartingAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<EN.CategorySearchInvoice> passingDL = new List<EN.CategorySearchInvoice>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.CartingAssessInvoice(AssessNo, WorkYear, Category);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CategorySearchInvoice PassingList = new EN.CategorySearchInvoice();
                    PassingList.Category = Convert.ToString(row["Category"]);
                    PassingList.AssessNo = Convert.ToString(row["AssessNo"]);
                    PassingList.AssessYear = Convert.ToString(row["WorkYear"]);
                    PassingList.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    PassingList.AssessDate = Convert.ToString(row["Assessment Date"]);
                    //PassingList.ValidData = Convert.ToString(row["ValidUptoDate"]);
                    //PassingList.AssessType = Convert.ToString(row["BondType"]);
                    PassingList.DeliveryType = Convert.ToString(row["DeliveryType"]);



                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.CategorySearchInvoice> CreditAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<EN.CategorySearchInvoice> passingDL = new List<EN.CategorySearchInvoice>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.CreditAssessInvoice(AssessNo, WorkYear, Category);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CategorySearchInvoice PassingList = new EN.CategorySearchInvoice();
                    PassingList.Category = Convert.ToString(row["Category"]);
                    PassingList.AssessNo = Convert.ToString(row["CreditNoteNo"]);
                    PassingList.AssessYear = Convert.ToString(row["WorkYear"]);
                    



                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }


        // Code for ICD Export Port Wise movement Pendency

        public List<EN.CustomerWiseLDDReport> GetICDExportPortWiseMovement()
        {

            List<EN.CustomerWiseLDDReport> ICDExportLIst = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportPortWiseMovement();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["POL"]);
                    ICDExportDL.T20 = Convert.ToString(row["P20"]);
                    ICDExportDL.T40 = Convert.ToString(row["P40"]);
                    ICDExportDL.T45 = Convert.ToString(row["P45"]);
                    ICDExportDL.Total = Convert.ToString(row["TOTAL"]);
                    ICDExportDL.Teus = Convert.ToString(row["Teus"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;


        }

        public int GetICDExportEmptyPortWiseMovement()
        {
            int message = 0; ;
            EN.CustomerWiseLDDReport ICDExportLIst = new EN.CustomerWiseLDDReport();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportPortWiseMovement();
            //  string T45Total = "";
            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportLIst.Process = Convert.ToString(row["POL"]);
                    ICDExportLIst.T20 = Convert.ToString(row["P20"]);
                    ICDExportLIst.T40 = Convert.ToString(row["P40"]);
                    ICDExportLIst.T45 = Convert.ToString(row["P45"]);
                    ICDExportDL.Teus = Convert.ToString(row["Teus"]);
                    if (ICDExportLIst.Process == "Total:")
                    {
                        message = Convert.ToInt16(ICDExportDL.Teus);
                    }

                }
            }
            return message;


        }


        //ICD Export Port Shut Out

        public List<EN.CustomerWiseLDDReport> GetICDExportShutOut()
        {

            List<EN.CustomerWiseLDDReport> ICDExportLIst = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportPortShutOut();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["size20"]);
                    ICDExportDL.T40 = Convert.ToString(row["size20"]);
                    ICDExportDL.T45 = Convert.ToString(row["size20"]);
                    ICDExportDL.Total = Convert.ToString(row["TOTAL"]);
                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;


        }

        public int GetICDExportShutOutCount()
        {
            int message = 0; ;
            EN.CustomerWiseLDDReport ICDExportLIst = new EN.CustomerWiseLDDReport();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportPortShutOut();
            //  string T45Total = "";
            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["size20"]);
                    ICDExportDL.T40 = Convert.ToString(row["size20"]);
                    ICDExportDL.T45 = Convert.ToString(row["size20"]);

                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);

                    if (ICDExportLIst.Process == "Current Month")
                    {
                        message = Convert.ToInt16(ICDExportDL.Teus);
                    }


                }
            }
            return message;


        }


        //ICD Export Re Movement

        public List<EN.CustomerWiseLDDReport> GetICDExportReMovement()
        {

            List<EN.CustomerWiseLDDReport> ICDExportLIst = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportPortShutOut();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["size20"]);
                    ICDExportDL.T40 = Convert.ToString(row["size20"]);
                    ICDExportDL.T45 = Convert.ToString(row["size20"]);
                    ICDExportDL.Total = Convert.ToString(row["TOTAL"]);
                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;


        }

        public int GetICDExportReMovementCount()
        {
            int message = 0; ;
            EN.CustomerWiseLDDReport ICDExportLIst = new EN.CustomerWiseLDDReport();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportPortShutOut();
            //  string T45Total = "";
            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["size20"]);
                    ICDExportDL.T40 = Convert.ToString(row["size20"]);
                    ICDExportDL.T45 = Convert.ToString(row["size20"]);

                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);

                    if (ICDExportLIst.Process == "Current Month")
                    {
                        message = Convert.ToInt16(ICDExportDL.Teus);
                    }


                }
            }
            return message;


        }

        //ICD Export Re Working

        public List<EN.CustomerWiseLDDReport> GetICDExportReWorking()
        {

            List<EN.CustomerWiseLDDReport> ICDExportLIst = new List<EN.CustomerWiseLDDReport>();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportReWorking();

            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["SIZE20"]);
                    ICDExportDL.T40 = Convert.ToString(row["SIZE40"]);
                    ICDExportDL.T45 = Convert.ToString(row["SIZE45"]);
                    ICDExportDL.Total = Convert.ToString(row["TOTAL"]);
                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;


        }

        public int GetICDExportReWorkingCount()
        {
            int message = 0; ;
            EN.CustomerWiseLDDReport ICDExportLIst = new EN.CustomerWiseLDDReport();
            DataTable CustomerInventoryDL = new DataTable();
            CustomerInventoryDL = trackerdatamanager.GetICDIExportReWorking();
            //  string T45Total = "";
            if (CustomerInventoryDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerInventoryDL.Rows)
                {
                    EN.CustomerWiseLDDReport ICDExportDL = new EN.CustomerWiseLDDReport();
                    ICDExportDL.Process = Convert.ToString(row["process"]);
                    ICDExportDL.T20 = Convert.ToString(row["SIZE20"]);
                    ICDExportDL.T40 = Convert.ToString(row["SIZE40"]);
                    ICDExportDL.T45 = Convert.ToString(row["SIZE45"]);

                    ICDExportDL.Teus = Convert.ToString(row["TEUS"]);

                    if (ICDExportLIst.Process == "Current Month")
                    {
                        message = Convert.ToInt16(ICDExportDL.Teus);
                    }


                }
            }
            return message;


        }

        public List<EN.CustomerWiseLDDReport> GetJOInHandMonthWise()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetJOInHandMonthWise();
            List<EN.CustomerWiseLDDReport> GetTPDataList = new List<EN.CustomerWiseLDDReport>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.CustomerWiseLDDReport DL = new EN.CustomerWiseLDDReport();
                    DL.Month = Convert.ToString(row["Month"]);
                    DL.T20 = Convert.ToString(row["T20"]);
                    DL.T40 = Convert.ToString(row["T40"]);
                    DL.T45 = Convert.ToString(row["T45"]);
                    DL.Total = Convert.ToString(row["Total"]);
                    DL.Teus = Convert.ToString(row["TEUS"]);


                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }
        public List<EN.CustomerWiseLDDReport> GetJOReceived()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetJOReceived();
            List<EN.CustomerWiseLDDReport> GetTPDataList = new List<EN.CustomerWiseLDDReport>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.CustomerWiseLDDReport DL = new EN.CustomerWiseLDDReport();
                    DL.Process = Convert.ToString(row["Process"]);
                    DL.T20 = Convert.ToString(row["size20"]);
                    DL.T40 = Convert.ToString(row["size40"]);
                    DL.T45 = Convert.ToString(row["size45"]);
                    DL.Total = Convert.ToString(row["Total"]);
                    DL.Teus = Convert.ToString(row["TEUS"]);


                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }
        public List<EN.CustomerWiseLDDReport> GetINTRANSIT()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetInTransit();
            List<EN.CustomerWiseLDDReport> GetTPDataList = new List<EN.CustomerWiseLDDReport>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.CustomerWiseLDDReport DL = new EN.CustomerWiseLDDReport();
                    DL.Port = Convert.ToString(row["PortName"]);
                    DL.T20 = Convert.ToString(row["Size20"]);
                    DL.T40 = Convert.ToString(row["Size40"]);
                    DL.T45 = Convert.ToString(row["Size45"]);
                    DL.Total = Convert.ToString(row["Total"]);
                    DL.Teus = Convert.ToString(row["TEUS"]);


                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }


        public List<EN.MonthlyEntites> GetICDExportMonthlyWiseCollection()
        {
            DataTable MonthDL = new DataTable();
            MonthDL = trackerdatamanager.GetICDIExportMonthWiseMovement();
            List<EN.MonthlyEntites> GetTPDataList = new List<EN.MonthlyEntites>();

            if (MonthDL.Rows.Count != 0)
            {
                foreach (DataRow row in MonthDL.Rows)
                {
                    EN.MonthlyEntites DL = new EN.MonthlyEntites();
                    DL.Year = Convert.ToString(row["Year"]);
                    DL.MonthNo = Convert.ToString(row["MonthNo"]);
                    DL.Date = Convert.ToString(row["Date"]);
                    DL.P20 = Convert.ToString(row["20"]);
                    DL.P40 = Convert.ToString(row["40"]);
                    DL.P45 = Convert.ToString(row["45"]);
                    DL.GrandTotal = Convert.ToString(row["TOTAL"]);
                    DL.TEUS = Convert.ToString(row["Tues"]);


                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }

        public int GetICDExportMonthlyWiseCollectionCount()
        {
            int message = 0; ;
            DataTable MonthDL = new DataTable();
            MonthDL = trackerdatamanager.GetICDIExportMonthWiseMovement();
            List<EN.MonthlyEntites> GetTPDataList = new List<EN.MonthlyEntites>();

            if (MonthDL.Rows.Count != 0)
            {
                foreach (DataRow row in MonthDL.Rows)
                {
                    EN.MonthlyEntites DL = new EN.MonthlyEntites();
                    DL.Year = Convert.ToString(row["Year"]);
                    DL.MonthNo = Convert.ToString(row["MonthNo"]);
                    DL.Date = Convert.ToString(row["Date"]);
                    DL.P20 = Convert.ToString(row["20"]);
                    DL.P40 = Convert.ToString(row["40"]);
                    DL.P45 = Convert.ToString(row["45"]);
                    DL.TEUS = Convert.ToString(row["Tues"]);
                    if (DL.MonthNo == "12")
                    {
                        message = Convert.ToInt16(DL.TEUS);
                    }

                    GetTPDataList.Add(DL);

                }

            }
            return message;


        }


        public List<EN.MonthlyEntites> GetICDExportMonthlyWiseMovement()
        {
            DataTable MonthDL = new DataTable();
            MonthDL = trackerdatamanager.GetICDIExportMonthWiseCollection();
            List<EN.MonthlyEntites> GetTPDataList = new List<EN.MonthlyEntites>();

            if (MonthDL.Rows.Count != 0)
            {
                foreach (DataRow row in MonthDL.Rows)
                {
                    EN.MonthlyEntites DL = new EN.MonthlyEntites();
                    DL.Year = Convert.ToString(row["Year"]);
                    DL.MonthNo = Convert.ToString(row["MonthNo"]);
                    DL.MonthName = Convert.ToString(row["MonthName"]);
                    DL.GrandTotal = Convert.ToString(row["Grandtotal"]);



                    GetTPDataList.Add(DL);

                }

            }
            return GetTPDataList;


        }

        public string GetICDExportMonthlyWiseMovementCount()
        {
            string message = "";
            DataTable MonthDL = new DataTable();
            MonthDL = trackerdatamanager.GetICDIExportMonthWiseCollection();
            List<EN.MonthlyEntites> GetTPDataList = new List<EN.MonthlyEntites>();

            if (MonthDL.Rows.Count != 0)
            {
                foreach (DataRow row in MonthDL.Rows)
                {
                    EN.MonthlyEntites DL = new EN.MonthlyEntites();
                    DL.Year = Convert.ToString(row["Year"]);
                    DL.MonthNo = Convert.ToString(row["MonthNo"]);
                    DL.MonthName = Convert.ToString(row["MonthName"]);
                    DL.GrandTotal = Convert.ToString(row["Grandtotal"]);
                    if (DL.MonthName == "Dec-19")
                    {
                        message = Convert.ToString(DL.GrandTotal);
                    }

                    GetTPDataList.Add(DL);

                }

            }
            return message;


        }
        public string UnlockDetails(string ddlCriteria, string ddlType, string txtInvoice, string Fromdate, string Todate)
        {
            int i = 0;
            i = trackerdatamanager.UnlockDetails(ddlCriteria, ddlType, txtInvoice, Fromdate, Todate);
            string message;
            if (i > 0)
            {
                message = "Assessment number or Receipt number Unlocked successfully";
            }
            else
            {
                message = "Record not Unlocked, Please try again!";
            }
            return message;
        }
        public string CancelDtails(string ddlCriteria, string ddlType, string txtInvoice)
        {
            int i = 0;
            i = trackerdatamanager.CancelDtails(ddlCriteria, ddlType, txtInvoice);
            string message;
            if (i > 0)
            {
                message = "Assessment number or Receipt number Unlocked successfully";
            }
            else
            {
                message = "Record not Unlocked, Please try again!";
            }
            return message;
        }

        public List<EN.TeusSearch> teusSearche(string txtbookingNo)
        {
            List<EN.TeusSearch> passingDL = new List<EN.TeusSearch>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.Teussearch(txtbookingNo);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TeusSearch PassingList = new EN.TeusSearch();
                    PassingList.txt20 = Convert.ToString(row["T20"]);
                    PassingList.txt40 = Convert.ToString(row["T40"]);
                    PassingList.txt45 = Convert.ToString(row["T45"]);
                    PassingList.Teus = Convert.ToString(row["Teus"]);





                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        public List<EN.TeusSearch> teusSearchStock(string AGID, string ToDate)
        {
            List<EN.TeusSearch> passingDL = new List<EN.TeusSearch>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.Teussearchstock(AGID, ToDate);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TeusSearch PassingList = new EN.TeusSearch();
                    PassingList.txt20 = Convert.ToString(row["T20"]);
                    PassingList.txt40 = Convert.ToString(row["T40"]);
                    PassingList.txt45 = Convert.ToString(row["T45"]);
                    PassingList.Teus = Convert.ToString(row["Teus"]);





                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        public List<EN.TeusSearch> teusSearchLoaded(string DepartmentID, string FromDate)
        {
            List<EN.TeusSearch> passingDL = new List<EN.TeusSearch>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.TeussearchLoaded(DepartmentID, FromDate);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TeusSearch PassingList = new EN.TeusSearch();
                    PassingList.txt20 = Convert.ToString(row["T20"]);
                    PassingList.txt40 = Convert.ToString(row["T40"]);
                    PassingList.txt45 = Convert.ToString(row["T45"]);
                    PassingList.Teus = Convert.ToString(row["Teus"]);





                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        public List<EN.PartyNameEntities> Getpartyname()
        {
            List<EN.PartyNameEntities> locationDL = new List<EN.PartyNameEntities>();
            DataTable dt = new DataTable();
            string Table = "Party_GST_M";
            string Column = "Distinct Common_ID,GSTName";
            string Condition = "Common_ID is not null";
            string OrderBy = "GSTName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.PartyNameEntities locationList = new EN.PartyNameEntities();
                    locationList.Common_ID = Convert.ToInt32(row["Common_ID"]);
                    locationList.GSTName = Convert.ToString(row["GSTName"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<EN.PartyNameEntities> VendorFill()
        {
            List<EN.PartyNameEntities> locationDL = new List<EN.PartyNameEntities>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.VendorFill();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.PartyNameEntities locationList = new EN.PartyNameEntities();
                    locationList.transporterID = Convert.ToInt32(row["transporterID"]);
                    locationList.vendorname = Convert.ToString(row["vendorname"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<EN.Customer> getParty()
        {
            List<EN.Customer> CustomerDL = new List<EN.Customer>();
            DataTable CustomerDT = new DataTable();
            string Table = "Party_GST_M";
            string Column = "distinct Common_ID,GSTName";
            string Condition = "";
            string OrderBy = "GSTName";

            CustomerDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.Customer CustomerList = new EN.Customer();
                    CustomerList.AGID = Convert.ToInt32(row["Common_ID"]);
                    CustomerList.AGName = Convert.ToString(row["GSTName"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }

        public List<EN.DriverpaymentDetailsentities> GetDriverdetailssummary(string FromDate, string ToDate, string Value)
        {

            List<EN.DriverpaymentDetailsentities> passingDL = new List<EN.DriverpaymentDetailsentities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetDriverwisePaymentdetails(FromDate, ToDate, Value);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DriverpaymentDetailsentities PassingList = new EN.DriverpaymentDetailsentities();
                    PassingList.SRNo = Convert.ToString(row["SR No"]);
                    PassingList.driverid = Convert.ToString(row["Driver Code"]);
                    PassingList.Drivername = Convert.ToString(row["Driver Name"]);
                    PassingList.HT = Convert.ToString(row["HT"]);
                    PassingList.NCL = Convert.ToString(row["NCL"]);
                    PassingList.NPT = Convert.ToString(row["NPT"]);
                    PassingList.Total = Convert.ToString(row["Total"]);

                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.SalesRegisterrReport> getEInvoiceReportList(string Activity, string fdate)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.getEInvoiceRegisterrReportList(Activity, fdate);
            List<EN.SalesRegisterrReport> ContainerList = new List<EN.SalesRegisterrReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.SalesRegisterrReport Tues1 = new EN.SalesRegisterrReport();
                    Tues1.Srno = Convert.ToInt32(Count++);
                    Tues1.Category = Convert.ToString(row["Category"]);
                    Tues1.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    Tues1.AssessDate = Convert.ToString(row["Invoice Date"]);
                    Tues1.Deliverytype = Convert.ToString(row["Invoice Type"]);
                   
                    
                    
                    Tues1.GSTName = Convert.ToString(row["GST Party Name"]);
                    Tues1.GSTIn_uniqID = Convert.ToString(row["GST Number"]);
                    Tues1.NetTotal = Convert.ToString(row["Taxable Amount"]);
                    
                    Tues1.TaxableAmount = Convert.ToString(row["Taxable Amount"]);
                    Tues1.TotalGST = Convert.ToString(row["Total GST"]);
                    Tues1.GrandTotal = Convert.ToString(row["Grand Total"]);
                    
                    Tues1.UserName = Convert.ToString(row["Invoice Generated By"]);
            
                     

                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        public EN.RevenueAlloperationEntities GetICDFullImportExportSeparate(string FromDate, string ToDate)
        {
            EN.RevenueAlloperationEntities CFSFullImportExport = new EN.RevenueAlloperationEntities();
            List<EN.ICDFullImport> CFSFullImportList = new List<EN.ICDFullImport>();
            List<EN.ICDFullExport> CFSFullExportList = new List<EN.ICDFullExport>();
            List<EN.ICDFullExportRepo> CFSFullExportRepoList = new List<EN.ICDFullExportRepo>();

            DataSet CFSFullDS = new DataSet();


            CFSFullDS = trackerdatamanager.GetCFSFULLImportExportData(FromDate,ToDate);

            if (CFSFullDS.Tables[0] != null)
            {
                foreach (DataRow row in CFSFullDS.Tables[0].Rows)
                {
                    EN.ICDFullImport ICDImport = new EN.ICDFullImport();
                    ICDImport.PortName = Convert.ToString(row["Yard"]);
                    ICDImport.Road20 = Convert.ToString(row["RD20"]);
                    ICDImport.Road40 = Convert.ToString(row["RD40"]);
                    ICDImport.Road45 = Convert.ToString(row["RD45"]);
                    ICDImport.Rail20 = Convert.ToString(row["RL20"]);
                    ICDImport.Rail40 = Convert.ToString(row["RL40"]);
                    ICDImport.Rail45 = Convert.ToString(row["RL45"]);
                    ICDImport.TotalTues = Convert.ToString(row["TotTeus"]);
                    CFSFullImportList.Add(ICDImport);
                }
            }

            if (CFSFullDS.Tables[1] != null)
            {
                foreach (DataRow row in CFSFullDS.Tables[1].Rows)
                {
                    EN.ICDFullExport ICDExport = new EN.ICDFullExport();
                    ICDExport.PortName = Convert.ToString(row["Yard"]);
                    ICDExport.Road20 = Convert.ToString(row["RD20"]);
                    ICDExport.Road40 = Convert.ToString(row["RD40"]);
                    ICDExport.Road45 = Convert.ToString(row["RD45"]);
                    ICDExport.Rail20 = Convert.ToString(row["RL20"]);
                    ICDExport.Rail40 = Convert.ToString(row["RL40"]);
                    ICDExport.Rail45 = Convert.ToString(row["RL45"]);
                    ICDExport.TotalTues = Convert.ToString(row["TotTeus"]);
                    CFSFullExportList.Add(ICDExport);
                }
            }

            if (CFSFullDS.Tables[2] != null)
            {
                foreach (DataRow row in CFSFullDS.Tables[2].Rows)
                {
                    EN.ICDFullExportRepo ICDExportRepo = new EN.ICDFullExportRepo();
                    ICDExportRepo.PortName = Convert.ToString(row["Yard"]);
                    ICDExportRepo.Road20 = Convert.ToString(row["RD20"]);
                    ICDExportRepo.Road40 = Convert.ToString(row["RD40"]);
                    ICDExportRepo.Road45 = Convert.ToString(row["RD45"]);
                    ICDExportRepo.Rail20 = Convert.ToString(row["RL20"]);
                    ICDExportRepo.Rail40 = Convert.ToString(row["RL40"]);
                    ICDExportRepo.Rail45 = Convert.ToString(row["RL45"]);
                    ICDExportRepo.TotalTues = Convert.ToString(row["TotTeus"]);
                    CFSFullExportRepoList.Add(ICDExportRepo);
                }
            }

            CFSFullImportExport.MonthName = Convert.ToString(CFSFullDS.Tables[3].Rows[0]["MonthName"]);
            CFSFullImportExport.ICDFullImport = CFSFullImportList;
            CFSFullImportExport.ICDFullExport = CFSFullExportList;
            CFSFullImportExport.ICDFullExportRepo = CFSFullExportRepoList;
            return CFSFullImportExport;
        }

        public List<EN.VesselandCustomerWisePendency> GetVesselCustomerWisePendency()
        {
            List<EN.VesselandCustomerWisePendency> ICDExportLIst = new List<EN.VesselandCustomerWisePendency>();
            DataTable CustomerPortPendencyDL = new DataTable();
            CustomerPortPendencyDL = trackerdatamanager.GetICDIExportVesselANdCustomerWisePendency();

            if (CustomerPortPendencyDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerPortPendencyDL.Rows)
                {
                    EN.VesselandCustomerWisePendency ICDExportDL = new EN.VesselandCustomerWisePendency();
                    ICDExportDL.VesselName = Convert.ToString(row["VesselName"]);
                    ICDExportDL.portname = Convert.ToString(row["portname"]);
                    ICDExportDL.customer = Convert.ToString(row["customer"]);
                    ICDExportDL.PortID = Convert.ToString(row["PortID"]);
                    ICDExportDL.CFS = Convert.ToString(row["CFS"]);
                    ICDExportDL.cutofdate = Convert.ToString(row["cutofdate"]);
                    ICDExportDL.R20 = Convert.ToString(row["R20"]);
                    ICDExportDL.R40 = Convert.ToString(row["R40"]);
                    ICDExportDL.R45 = Convert.ToString(row["R45"]);
                    ICDExportDL.M20 = Convert.ToString(row["M20"]);
                    ICDExportDL.M40 = Convert.ToString(row["M40"]);
                    ICDExportDL.Mteus = Convert.ToString(row["Mteus"]);
                    ICDExportDL.M45 = Convert.ToString(row["M45"]);
                    ICDExportDL.P20 = Convert.ToString(row["P20"]);
                    ICDExportDL.P40 = Convert.ToString(row["P40"]);
                    ICDExportDL.P45 = Convert.ToString(row["P45"]);
                    ICDExportDL.PTeus = Convert.ToString(row["PTeus"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;
        }

        public List<EN.VesselandCustomerWisePendency> GetVesselWisePendency()
        {
            List<EN.VesselandCustomerWisePendency> ICDExportLIst = new List<EN.VesselandCustomerWisePendency>();
            DataTable CustomerPortPendencyDL = new DataTable();
            CustomerPortPendencyDL = trackerdatamanager.GetICDIExportVesselWisePendency();

            if (CustomerPortPendencyDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerPortPendencyDL.Rows)
                {
                    EN.VesselandCustomerWisePendency ICDExportDL = new EN.VesselandCustomerWisePendency();
                    ICDExportDL.VesselName = Convert.ToString(row["VesselName"]);
                    ICDExportDL.portname = Convert.ToString(row["portname"]);
                    ICDExportDL.cutofdate = Convert.ToString(row["cutofdate"]);
                    ICDExportDL.R20 = Convert.ToString(row["R20"]);
                    ICDExportDL.R40 = Convert.ToString(row["R40"]);
                    ICDExportDL.R45 = Convert.ToString(row["R45"]);
                    ICDExportDL.M20 = Convert.ToString(row["M20"]);
                    ICDExportDL.M40 = Convert.ToString(row["M40"]);
                    ICDExportDL.Mteus = Convert.ToString(row["Mteus"]);
                    ICDExportDL.M45 = Convert.ToString(row["M45"]);
                    ICDExportDL.P20 = Convert.ToString(row["P20"]);
                    ICDExportDL.P40 = Convert.ToString(row["P40"]);
                    ICDExportDL.P45 = Convert.ToString(row["P45"]);
                    ICDExportDL.PTeus = Convert.ToString(row["PTeus"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;
        }

        public List<EN.VesselandCustomerWisePendency> GetCustomerWisePendency()
        {
            List<EN.VesselandCustomerWisePendency> ICDExportLIst = new List<EN.VesselandCustomerWisePendency>();
            DataTable CustomerPortPendencyDL = new DataTable();
            CustomerPortPendencyDL = trackerdatamanager.GetICDIExportCustomerWisePendency();

            if (CustomerPortPendencyDL.Rows.Count != 0)
            {

                foreach (DataRow row in CustomerPortPendencyDL.Rows)
                {
                    EN.VesselandCustomerWisePendency ICDExportDL = new EN.VesselandCustomerWisePendency();
                    ICDExportDL.customer = Convert.ToString(row["customer"]);
                    ICDExportDL.R20 = Convert.ToString(row["R20"]);
                    ICDExportDL.R40 = Convert.ToString(row["R40"]);
                    ICDExportDL.R45 = Convert.ToString(row["R45"]);
                    ICDExportDL.M20 = Convert.ToString(row["M20"]);
                    ICDExportDL.M40 = Convert.ToString(row["M40"]);
                    ICDExportDL.Mteus = Convert.ToString(row["Mteus"]);
                    ICDExportDL.M45 = Convert.ToString(row["M45"]);
                    ICDExportDL.P20 = Convert.ToString(row["P20"]);
                    ICDExportDL.P40 = Convert.ToString(row["P40"]);
                    ICDExportDL.P45 = Convert.ToString(row["P45"]);
                    ICDExportDL.PTeus = Convert.ToString(row["PTeus"]);
                    ICDExportLIst.Add(ICDExportDL);
                }
            }
            return ICDExportLIst;
        }

        public DataTable GetExportReportDetails(string searchtype, string searchon, string Fromdate, string Todate)
        {
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetExportReportDetails(searchtype, searchon, Fromdate, Todate);
            return dt;
        
        }
        public List<EN.JobOrderDEntities> getExportSBENtryidlist(string SBNumber)
        {
            DataTable jonoDL = new DataTable();
            jonoDL = trackerdatamanager.getExportSBEntryidforexpin(SBNumber);
            List<EN.JobOrderDEntities> joborderDL = new List<EN.JobOrderDEntities>();

            if (jonoDL.Rows.Count != 0)
            {
                foreach (DataRow row in jonoDL.Rows)
                {
                    EN.JobOrderDEntities DL = new EN.JobOrderDEntities();

                    DL.JONo = Convert.ToInt32(row["EntryID"]);
                    joborderDL.Add(DL);
                }

            }
            return joborderDL;
        }

        public List<EN.ShippingBillWiseSearch> GetShippingBillExportWiseContainerDetails(string Containerno, string Jono)
        {
            DataTable dt = new DataTable();

            List<EN.ShippingBillWiseSearch> CustomerList = new List<EN.ShippingBillWiseSearch>();
            dt = trackerdatamanager.GetSBlingNoDEtails(Containerno, Jono);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.ShippingBillWiseSearch CustomerDL = new EN.ShippingBillWiseSearch();
                    CustomerDL.EntryID = Convert.ToString(row["EntryID"]);
                    CustomerDL.CHAName = Convert.ToString(row["CHAName"]);
                    CustomerDL.AGName = Convert.ToString(row["agencyName"]);
                    CustomerDL.Exporter = Convert.ToString(row["Exporter"]);

                    CustomerDL.Stuffingtype = Convert.ToString(row["StuffingType"]);
                    CustomerDL.TotalPKGS = Convert.ToString(row["CargoQty"]);
                    CustomerDL.CargoWeight = Convert.ToString(row["CargoWeight"]);
                    CustomerDL.CargoDesc = Convert.ToString(row["CargoDesc"]);
                    CustomerDL.SBDate = Convert.ToString(row["SBDate"]);
                    CustomerDL.fobvalue = Convert.ToString(row["FOB"]);
                    CustomerDL.addedby = Convert.ToString(row["Entry By"]);
                    CustomerDL.CargoType = Convert.ToString(row["CargoType"]);
                    CustomerDL.ClassNo = Convert.ToString(row["ClassNo"]);
                    CustomerDL.UNNo = Convert.ToString(row["UNNo"]);


                    CustomerList.Add(CustomerDL);
                }
            }
            return CustomerList;
        }


        public List<EN.ShippingBillWiseSearch> GetSBNoCartingDetailsDetails(string Containerno, string Jono)
        {
            DataTable dt = new DataTable();

            List<EN.ShippingBillWiseSearch> CustomerList = new List<EN.ShippingBillWiseSearch>();
            dt = trackerdatamanager.GetSBlingCartingBalDetails(Containerno, Jono);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.ShippingBillWiseSearch CustomerDL = new EN.ShippingBillWiseSearch();
                    CustomerDL.CartingBal = Convert.ToString(row["CartingBal"]);
                    CustomerDL.CargoBal = Convert.ToString(row["cargobal"]);


                    CustomerList.Add(CustomerDL);
                }
            }
            return CustomerList;
        }

        public List<EN.JobOrderDEntities> getENtryidlist(string Containerno)
        {
            DataTable jonoDL = new DataTable();
            jonoDL = trackerdatamanager.getEntryidforexpin(Containerno);
            List<EN.JobOrderDEntities> joborderDL = new List<EN.JobOrderDEntities>();

            if (jonoDL.Rows.Count != 0)
            {
                foreach (DataRow row in jonoDL.Rows)
                {
                    EN.JobOrderDEntities DL = new EN.JobOrderDEntities();

                    DL.JONo = Convert.ToInt32(row["EntryID"]);
                    joborderDL.Add(DL);
                }

            }
            return joborderDL;
        }
        public List<EN.ExportContainerSearchEntities> GetExportWiseContainerDetails(string Containerno, string Jono)
        {
            DataTable dt = new DataTable();

            List<EN.ExportContainerSearchEntities> CustomerList = new List<EN.ExportContainerSearchEntities>();
            dt = trackerdatamanager.GetExportDetails(Containerno, Jono);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.ExportContainerSearchEntities CustomerDL = new EN.ExportContainerSearchEntities();
                    CustomerDL.EntryID = Convert.ToString(row["EntryID"]);
                    CustomerDL.Size = Convert.ToString(row["Size"]);
                    CustomerDL.AGName = Convert.ToString(row["AGName"]);
                    CustomerDL.TrailerNo = Convert.ToString(row["TrailerNo"]);
                    CustomerDL.containertype = Convert.ToString(row["containertype"]);
                    CustomerDL.movementtype = Convert.ToString(row["movementtype"]);
                    CustomerDL.stuffingdate = Convert.ToString(row["stuffingdate"]);
                    CustomerDL.ISOCode = Convert.ToString(row["ISOCode"]);
                    CustomerDL.GateBy = Convert.ToString(row["Gate By"]);
                    CustomerDL.remarks = Convert.ToString(row["Gate Remarks"]);
                    CustomerDL.agencyid = Convert.ToString(row["agencyid"]);
                    CustomerDL.movementrcvd = Convert.ToString(row["movementrcvd"]);
                    CustomerDL.viano = Convert.ToString(row["viano"]);
                    CustomerDL.SLName = Convert.ToString(row["linename"]);
                    CustomerDL.CLPNo = Convert.ToString(row["CLPNo"]);
                    CustomerDL.EntryDate = Convert.ToString(row["CLP Date"]);
                    CustomerDL.GPNo = Convert.ToString(row["GPNo"]);
                    CustomerDL.GPDate = Convert.ToString(row["GPDate"]);
                    CustomerDL.returndate = Convert.ToString(row["ReturnDate"]);
                    CustomerDL.Portreturn = Convert.ToString(row["Port return"]);
                    CustomerDL.CHAName = Convert.ToString(row["CHAName"]);
                    CustomerDL.PluginDate = Convert.ToString(row["PluginDate"]);
                    CustomerDL.UnplugInDate = Convert.ToString(row["UnplugInDate"]);
                    CustomerDL.sealno = Convert.ToString(row["sealno"]);
                    CustomerDL.TransName = Convert.ToString(row["TransName"]);
                    CustomerDL.Indateandtime = Convert.ToString(row["Indate"]);
                    CustomerDL.CutOffdate = Convert.ToString(row["cutofdate"]);
                    CustomerDL.port = Convert.ToString(row["Port"]);
                    CustomerList.Add(CustomerDL);
                }
            }
            return CustomerList;
        }

        public List<EN.ExportContainerSearchEntities> GetExportWiseContainerTypeDetails(string Containerno, string Jono)
        {
            DataTable dt = new DataTable();

            List<EN.ExportContainerSearchEntities> CustomerList = new List<EN.ExportContainerSearchEntities>();
            dt = trackerdatamanager.GetExportContainertypeDetails(Containerno, Jono);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.ExportContainerSearchEntities CustomerDL = new EN.ExportContainerSearchEntities();

                    CustomerDL.containertype = Convert.ToString(row["containertype"]);
                    CustomerDL.entrytype = Convert.ToString(row["entrytype"]);

                    CustomerList.Add(CustomerDL);
                }
            }
            return CustomerList;
        }

        public List<EN.ExportContainerSearchEntities> GetExportWiseContainerSlipNoDetails(string Containerno, string Jono)
        {
            DataTable dt = new DataTable();

            List<EN.ExportContainerSearchEntities> CustomerList = new List<EN.ExportContainerSearchEntities>();
            dt = trackerdatamanager.GetExportContainerSlipNoDetails(Containerno, Jono);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.ExportContainerSearchEntities CustomerDL = new EN.ExportContainerSearchEntities();

                    CustomerDL.SlipNo = Convert.ToString(row["Slipno"]);
                    CustomerDL.Addedon = Convert.ToString(row["addedon"]);

                    CustomerList.Add(CustomerDL);
                }
            }
            return CustomerList;
        }

        public List<EN.BankMasterEntites> UpdateVoucherPaymentDetails(DataTable DriverPaymentDT, Int32 userId)
        {
            //EN.DriverPaymentReco objdriverpaymententities = new EN.DriverPaymentReco();
            List<EN.BankMasterEntites> VoucherPaymentList = new List<EN.BankMasterEntites>();
            if (DriverPaymentDT != null)
            {
                int count = 1;
                foreach (DataRow row in DriverPaymentDT.Rows)
                {
                    EN.BankMasterEntites DPDTList = new EN.BankMasterEntites();
                    DPDTList.SRNo = count++;
                    DPDTList.TransDate = Convert.ToString(row["TransDate"]);
                    DPDTList.Description = Convert.ToString(row["Description"]);
                    DPDTList.RefNo = Convert.ToString(row["RefNo"]);
                    DPDTList.Debit = Convert.ToString(row["Debit"]);
                    DPDTList.Credit = Convert.ToString(row["Credit"]);
                    DPDTList.Balance = Convert.ToString(row["Balance"]);

                    VoucherPaymentList.Add(DPDTList);

                }
            }
            return VoucherPaymentList;
        }

        public List<EN.CategorySearchInvoice> OtherAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            List<EN.CategorySearchInvoice> passingDL = new List<EN.CategorySearchInvoice>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.OtherAssessInvoice(AssessNo, WorkYear, Category);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CategorySearchInvoice PassingList = new EN.CategorySearchInvoice();
                    PassingList.Category = Convert.ToString(row["Category"]);
                    PassingList.AssessNo = Convert.ToString(row["AssessNo"]);
                    PassingList.AssessYear = Convert.ToString(row["WorkYear"]);
                    PassingList.InvoiceNo = Convert.ToString(row["InvoiceNo"]);



                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public string Cancelreceiptdetails(DataTable Invoicedata, int Userid,string ReceiptRefNo)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("Userid", Userid);
            parameterList.Add("ReceiptRefNo", ReceiptRefNo);
            int i = db.AddTypeTableData("usp_cancelinvreceipt", parameterList, Invoicedata, "Canceldirectreceipt", "@Canceldirectreceipt");


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
        public string BankRecoValidation(DataTable PaymentDT)
        {
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";

            if (PaymentDT != null)
            {
                foreach (DataRow row in PaymentDT.Rows)
                {
                    DataTable PortsDS = new DataTable();
                    PortsDS = trackerdatamanager.getDuplicateDataForBankReco(Convert.ToString(row["TransDate"]), Convert.ToString(row["Description"]), Convert.ToString(row["RefNo"]));
                    if (PortsDS.Rows.Count > 0)
                    {
                        message = "Following description already found in database: \n" + Convert.ToString(PortsDS.Rows[0][0]);

                    }
                    else
                    {
                        message = "";
                    }


                    if ((message != ""))
                    {
                        message2 += message + "\n";
                    }
                }

            }
            return message2;
        }
        public List<EN.GSTReturnEntities> GetTripList()
        {
            try
            {


                List<EN.GSTReturnEntities> CargoTypeList = new List<EN.GSTReturnEntities>();
                DataTable Result = trackerdatamanager.GetTrip();
                if (Result != null)
                {
                    foreach (DataRow row in Result.Rows)
                    {
                        EN.GSTReturnEntities item = new EN.GSTReturnEntities();
                        item.Trip = Convert.ToString(row["Trip"]);
                        item.Trip = Convert.ToString(row["Trip"]);
                        CargoTypeList.Add(item);
                    }

                }
                return CargoTypeList;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<EN.OutActivity> OutActi()
        {
            try
            {


                List<EN.OutActivity> CargoTypeList = new List<EN.OutActivity>();
                DataTable Result = trackerdatamanager.GetActivity();
                if (Result != null)
                {
                    foreach (DataRow row in Result.Rows)
                    {
                        EN.OutActivity item = new EN.OutActivity();
                        item.OutActivityID = Convert.ToString(row["AutoID"]);
                        item.OutActivityName = Convert.ToString(row["Activity"]);
                        CargoTypeList.Add(item);
                    }

                }
                return CargoTypeList;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<EN.InActivity> InActi()
        {
            try
            {


                List<EN.InActivity> CargoTypeList = new List<EN.InActivity>();
                DataTable Result = trackerdatamanager.GetActivity();
                if (Result != null)
                {
                    foreach (DataRow row in Result.Rows)
                    {
                        EN.InActivity item = new EN.InActivity();
                        item.InActivityID = Convert.ToString(row["AutoID"]);
                        item.InActivityName = Convert.ToString(row["Activity"]);
                        CargoTypeList.Add(item);
                    }

                }
                return CargoTypeList;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<EN.ActivityMaster> getPartyWiseActivity()
        {
            List<EN.ActivityMaster> passingDL = new List<EN.ActivityMaster>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getPartyWiseActivity();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ActivityMaster PassingList = new EN.ActivityMaster();
                    PassingList.ID = Convert.ToInt32(row["ID"]);
                    PassingList.TYPE = Convert.ToString(row["TYPE"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        public List<EN.PartyWiseHold> getHoldDetailsLists()
        {
            DataTable dt = new DataTable();

            List<EN.PartyWiseHold> HoldLists = new List<EN.PartyWiseHold>();
            dt = trackerdatamanager.getHoldLists();
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.PartyWiseHold HoldMDL = new EN.PartyWiseHold();
                    HoldMDL.HoldID = Convert.ToInt32(row["HoldID"]);
                    HoldMDL.HoldDate = Convert.ToString(row["HoldDate"]);
                    HoldMDL.Activity = Convert.ToString(row["Activity"]);
                    HoldMDL.Hold_To = Convert.ToString(row["Hold_To"]);
                    HoldMDL.Hold_To_Name = Convert.ToString(row["Hold_To_Name"]);

                    HoldMDL.Hold_Reason = Convert.ToString(row["Hold_Reason"]);
                    HoldMDL.HoldReamrks = Convert.ToString(row["HoldReamrks"]);

                    HoldMDL.DisplayHoldedBy = Convert.ToString(row["DisplayHoldedBy"]);
                    HoldMDL.HoldedOn = Convert.ToString(row["HoldedOn"]);

                    HoldLists.Add(HoldMDL);
                }
            }
            return HoldLists;
        }
        public EN.ResponseMessage HoldEntryReleased(EN.PartyWiseHold data)
        {
            try
            {


                EN.ResponseMessage response = new EN.ResponseMessage();

                DataTable itemD = new DataTable();

                itemD = trackerdatamanager.HoldEntryReleased(data.ReleasedBy, data.ReleasedRemarks, data.HoldID);

                if (itemD != null)
                {
                    //success = Convert.ToString(itemD.Rows[0][0]);

                    foreach (DataRow row in itemD.Rows)
                    {
                        response.Status = Convert.ToInt32(row["Status"]);
                        response.Message = Convert.ToString(row["message"]);

                    }
                }

                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EN.PartyWiseHold> getReleaseDetailsList(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();

            List<EN.PartyWiseHold> ReleaseLists = new List<EN.PartyWiseHold>();
            dt = trackerdatamanager.getReleaseLists(FromDate, ToDate);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.PartyWiseHold ReleaseMDL = new EN.PartyWiseHold();
                    ReleaseMDL.HoldID = Convert.ToInt32(row["HoldID"]);
                    ReleaseMDL.ReleasedRemarks = Convert.ToString(row["ReleasedRemarks"]);
                    ReleaseMDL.ReleaseBy = Convert.ToString(row["ReleaseBy"]);
                    ReleaseMDL.ReleasedOn = Convert.ToString(row["ReleasedOn"]);




                    ReleaseLists.Add(ReleaseMDL);
                }
            }
            return ReleaseLists;
        }
        public string CancelWorkDetails(DataTable dataTable, long UserID)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSQL = "";


            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_CancelWorkOrder '" + dataTable.Rows[k].Field<string>("WoNo") + "','" + dataTable.Rows[k].Field<string>("containerNo") + "','" + dataTable.Rows[k].Field<string>("JoEntryID") + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }
        public List<EN.ImportExportProcessActivitycs> GetImportExportReport()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportCurrentMonth = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetImportendExportMovementReport();
            PortsDTCFS = trackerdatamanager.GetMovementReportCFS();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(row["Process"]);
                    RevenueList.Import = Convert.ToString(row["IMP"]);
                    RevenueList.Export = Convert.ToString(row["EXP"]);
                    RevenueList.Total = Convert.ToString(row["TOTAL"]);

                    ImportExportCurrentMonth.Add(RevenueList);
                }
            }


            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(row["Process"]);
                    RevenueListCFS.Import = Convert.ToString(row["IMP"]);
                    RevenueListCFS.Export = Convert.ToString(row["EXP"]);
                    RevenueListCFS.Total = Convert.ToString(row["TOTAL"]);
                    RevenueListCFS.CurrentMonthName = Convert.ToString(row["MonthName"]);
                    ImportExportCurrentMonth.Add(RevenueListCFS);
                }
            }
            return ImportExportCurrentMonth;
        }



        public List<EN.ImportExportProcessActivitycs> GetLastImportExportReport()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportLastMonth = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetLastImportendExportMovementReport();
            PortsDTCFS = trackerdatamanager.GetLastMovementReportCFS();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(row["Process"]);
                    RevenueList.Import = Convert.ToString(row["IMP"]);
                    RevenueList.Export = Convert.ToString(row["EXP"]);
                    RevenueList.Total = Convert.ToString(row["TOTAL"]);

                    ImportExportLastMonth.Add(RevenueList);
                }
            }


            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(row["Process"]);
                    RevenueListCFS.Import = Convert.ToString(row["IMP"]);
                    RevenueListCFS.Export = Convert.ToString(row["EXP"]);
                    RevenueListCFS.Total = Convert.ToString(row["TOTAL"]);
                    RevenueListCFS.LastMonthName = Convert.ToString(row["MonthName"]);
                    ImportExportLastMonth.Add(RevenueListCFS);
                }
            }
            return ImportExportLastMonth;
        }


        //code for last 25 hours 

        public List<EN.ImportExportProcessActivitycs> GetLastDayImportExportReport()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportLastDays = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetlastDayImportendExportMovementReport();
            PortsDTCFS = trackerdatamanager.GetLastDayMovementReportCFS();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(row["Process"]);
                    RevenueList.Import = Convert.ToString(row["IMP"]);
                    RevenueList.Export = Convert.ToString(row["EXP"]);
                    RevenueList.Total = Convert.ToString(row["TOTAL"]);

                    ImportExportLastDays.Add(RevenueList);
                }
            }


            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(row["Process"]);
                    RevenueListCFS.Import = Convert.ToString(row["IMP"]);
                    RevenueListCFS.Export = Convert.ToString(row["EXP"]);
                    RevenueListCFS.Total = Convert.ToString(row["TOTAL"]);
                    RevenueListCFS.Last24HRSName = Convert.ToString(row["MonthName"]);
                    ImportExportLastDays.Add(RevenueListCFS);
                }
            }
            return ImportExportLastDays;
        }




        // code for port pendency



        public List<EN.ImportExportProcessActivitycs> GetPortPenencyReportReport()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportLastDays = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetPortPendencyReport();
            PortsDTCFS = trackerdatamanager.GetPortPendencyReportCFS();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(1);
                    RevenueList.ICDRail = Convert.ToString(row["ICD rail"]);
                    RevenueList.ICDRoad = Convert.ToString(row["ICD Road"]);

                    ImportExportLastDays.Add(RevenueList);
                }
            }

            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(1);
                    RevenueListCFS.CFSRail = Convert.ToString(row["CFS Rail"]);
                    RevenueListCFS.CFSRoad = Convert.ToString(row["CFS Road"]);
                    ImportExportLastDays.Add(RevenueListCFS);
                }
            }


            return ImportExportLastDays;
        }





        public List<EN.ImportExportProcessActivitycs> GetInventoryDetails()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportLastDays = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetInventoryDetails();
            PortsDTCFS = trackerdatamanager.GetInventoryDetailsCFS();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(row["ICD"]);
                    RevenueList.BalInventory = Convert.ToString(row["MTY Inventory"]);
                    RevenueList.Inventory = Convert.ToString(row["BAL Inventory"]);
                    RevenueList.ExportBalInventory = Convert.ToString(row["EXP Inventory"]);

                    ImportExportLastDays.Add(RevenueList);
                }
            }


            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(row["CFS"]);
                    RevenueListCFS.BalInventory = Convert.ToString(row["MTY Inventory"]);
                    RevenueListCFS.Inventory = Convert.ToString(row["BAL Inventory"]);
                    RevenueListCFS.ExportBalInventory = Convert.ToString(row["EXP Inventory"]);

                    ImportExportLastDays.Add(RevenueListCFS);
                }
            }
            return ImportExportLastDays;
        }
        //Code end By Prashant


        //code by rahul
        //code by rahul
        public EN.ImportExportProcessActivitycs GetCFSImportExportRailRoadMovement()
        {
            EN.ImportExportProcessActivitycs CFSImportExport = new EN.ImportExportProcessActivitycs();

            List<EN.ImportExportRailRoadLast24HrsCFS> CFSLAST24HRS = new List<EN.ImportExportRailRoadLast24HrsCFS>();
            List<EN.ImportExportRailRoadCurrentMonthCFS> CFSCURRENTMONTH = new List<EN.ImportExportRailRoadCurrentMonthCFS>();
            List<EN.ImportExportRailRoadLastMonthCFS> CFSLASTMONTH = new List<EN.ImportExportRailRoadLastMonthCFS>();

            DataSet CFS = new DataSet();
            CFS = trackerdatamanager.GetCFSImportExportRailRoadData();

            if (CFS.Tables[0] != null)
            {
                foreach (DataRow row in CFS.Tables[0].Rows)
                {
                    EN.ImportExportRailRoadLast24HrsCFS CFS24 = new EN.ImportExportRailRoadLast24HrsCFS();
                    CFS24.ImportRoad = Convert.ToString(row["ImportRoad"]);
                    CFS24.ImportRail = Convert.ToString(row["ImportRail"]);
                    CFS24.ExportRoad = Convert.ToString(row["ExportRoad"]);
                    CFS24.ExportRail = Convert.ToString(row["ExportRail"]);
                    CFS24.TotalTeus = Convert.ToString(row["Total"]);
                    CFSLAST24HRS.Add(CFS24);
                }
            }
            if (CFS.Tables[1] != null)
            {
                foreach (DataRow row in CFS.Tables[1].Rows)
                {
                    EN.ImportExportRailRoadCurrentMonthCFS CFSCUR = new EN.ImportExportRailRoadCurrentMonthCFS();
                    CFSCUR.ImportRoad = Convert.ToString(row["ImportRoad"]);
                    CFSCUR.ImportRail = Convert.ToString(row["ImportRail"]);
                    CFSCUR.ExportRoad = Convert.ToString(row["ExportRoad"]);
                    CFSCUR.ExportRail = Convert.ToString(row["ExportRail"]);
                    CFSCUR.TotalTeus = Convert.ToString(row["Total"]);
                    CFSCURRENTMONTH.Add(CFSCUR);
                }
            }
            if (CFS.Tables[2] != null)
            {
                foreach (DataRow row in CFS.Tables[2].Rows)
                {
                    EN.ImportExportRailRoadLastMonthCFS CFSLAST = new EN.ImportExportRailRoadLastMonthCFS();
                    CFSLAST.ImportRoad = Convert.ToString(row["ImportRoad"]);
                    CFSLAST.ImportRail = Convert.ToString(row["ImportRail"]);
                    CFSLAST.ExportRoad = Convert.ToString(row["ExportRoad"]);
                    CFSLAST.ExportRail = Convert.ToString(row["ExportRail"]);
                    CFSLAST.TotalTeus = Convert.ToString(row["Total"]);
                    CFSLASTMONTH.Add(CFSLAST);
                }
            }
            CFSImportExport.ImportExportRailRoadLast24HrsCFS = CFSLAST24HRS;
            CFSImportExport.ImportExportRailRoadCurrentMonthCFS = CFSCURRENTMONTH;
            CFSImportExport.ImportExportRailRoadLastMonthCFS = CFSLASTMONTH;
            CFSImportExport.CurrentMonthName = Convert.ToString(CFS.Tables[1].Rows[0]["Date"]);
            CFSImportExport.LastMonthName = Convert.ToString(CFS.Tables[2].Rows[0]["Date"]);
            CFSImportExport.Last24HRSName = Convert.ToString(CFS.Tables[0].Rows[0]["Date"]);
            return CFSImportExport;
        }
        public List<EN.ImportExportProcessActivitycs> GetDestuffDetails()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportLastDays = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetDMRDestuffICDDetails();
            PortsDTCFS = trackerdatamanager.GetDMRCFSdestuffdetails();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(row["process"]);
                    RevenueList.FCLDestuff = Convert.ToString(row["FCL Destuff"]);
                    RevenueList.LCLDestuff = Convert.ToString(row["LCL Destuff"]);
                    RevenueList.IMPLoadedDelivery = Convert.ToString(row["IMP Loaded Delivery"]);

                    ImportExportLastDays.Add(RevenueList);
                }
            }


            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(row["process"]);
                    RevenueListCFS.FCLDestuff = Convert.ToString(row["FCL Destuff"]);
                    RevenueListCFS.LCLDestuff = Convert.ToString(row["LCL Destuff"]);
                    RevenueListCFS.IMPLoadedDelivery = Convert.ToString(row["IMP Loaded Delivery"]);

                    ImportExportLastDays.Add(RevenueListCFS);
                }
            }
            return ImportExportLastDays;
        }


        public List<EN.ImportExportProcessActivitycs> GetTodayDestuffDetails()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportLastDays = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetTodayDMRDestuffICDDetails();
            PortsDTCFS = trackerdatamanager.GetTodayDMRCFSdestuffdetails();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(row["process"]);
                    RevenueList.FCLDestuff = Convert.ToString(row["FCL Destuff"]);
                    RevenueList.LCLDestuff = Convert.ToString(row["LCL Destuff"]);
                    RevenueList.IMPLoadedDelivery = Convert.ToString(row["IMP Loaded Delivery"]);

                    ImportExportLastDays.Add(RevenueList);
                }
            }


            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(row["process"]);
                    RevenueListCFS.FCLDestuff = Convert.ToString(row["FCL Destuff"]);
                    RevenueListCFS.LCLDestuff = Convert.ToString(row["LCL Destuff"]);
                    RevenueListCFS.IMPLoadedDelivery = Convert.ToString(row["IMP Loaded Delivery"]);

                    ImportExportLastDays.Add(RevenueListCFS);
                }
            }
            return ImportExportLastDays;
        }

        public List<EN.ImportExportProcessActivitycs> GetImportExportCompareReport()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportCurrentMonth = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetImportandExportCompareMovementReport();
            PortsDTCFS = trackerdatamanager.GetCompareMovementReportCFS();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(row["Process"]);
                    RevenueList.Import = Convert.ToString(row["IMP"]);
                    RevenueList.Export = Convert.ToString(row["EXP"]);
                    RevenueList.Total = Convert.ToString(row["TOTAL"]);

                    ImportExportCurrentMonth.Add(RevenueList);
                }
            }


            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(row["Process"]);
                    RevenueListCFS.Import = Convert.ToString(row["IMP"]);
                    RevenueListCFS.Export = Convert.ToString(row["EXP"]);
                    RevenueListCFS.Total = Convert.ToString(row["TOTAL"]);
                    RevenueListCFS.CurrentMonthName = Convert.ToString(row["MonthName"]);
                    ImportExportCurrentMonth.Add(RevenueListCFS);
                }
            }
            return ImportExportCurrentMonth;
        }

        public List<EN.ImportExportProcessActivitycs> GetLastImportExportCompareReport()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportLastMonth = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetLastImportendExportCompareMovementReport();
            PortsDTCFS = trackerdatamanager.GetLastCompareMovementReportCFS();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(row["Process"]);
                    RevenueList.Import = Convert.ToString(row["IMP"]);
                    RevenueList.Export = Convert.ToString(row["EXP"]);
                    RevenueList.Total = Convert.ToString(row["TOTAL"]);

                    ImportExportLastMonth.Add(RevenueList);
                }
            }


            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(row["Process"]);
                    RevenueListCFS.Import = Convert.ToString(row["IMP"]);
                    RevenueListCFS.Export = Convert.ToString(row["EXP"]);
                    RevenueListCFS.Total = Convert.ToString(row["TOTAL"]);
                    RevenueListCFS.LastMonthName = Convert.ToString(row["MonthName"]);
                    ImportExportLastMonth.Add(RevenueListCFS);
                }
            }
            return ImportExportLastMonth;
        }


        public List<EN.ImportExportProcessActivitycs> GetLastDayImportExportCompareReport()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportLastDays = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetlastDayImportendExportCompareMovementReport();
            PortsDTCFS = trackerdatamanager.GetLastDayCompareMovementReportCFS();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(row["Process"]);
                    RevenueList.Import = Convert.ToString(row["IMP"]);
                    RevenueList.Export = Convert.ToString(row["EXP"]);
                    RevenueList.Total = Convert.ToString(row["TOTAL"]);

                    ImportExportLastDays.Add(RevenueList);
                }
            }


            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(row["Process"]);
                    RevenueListCFS.Import = Convert.ToString(row["IMP"]);
                    RevenueListCFS.Export = Convert.ToString(row["EXP"]);
                    RevenueListCFS.Total = Convert.ToString(row["TOTAL"]);
                    RevenueListCFS.Last24HRSName = Convert.ToString(row["MonthName"]);
                    ImportExportLastDays.Add(RevenueListCFS);
                }
            }
            return ImportExportLastDays;
        }

        public List<EN.ImportExportProcessActivitycs> GetLastDay12HrsImportExportReport()
        {
            List<EN.ImportExportProcessActivitycs> ImportExportLastDays = new List<EN.ImportExportProcessActivitycs>();
            DataTable PortsDT = new DataTable();
            DataTable PortsDTCFS = new DataTable();
            PortsDT = trackerdatamanager.GetTodayImportendExportMovementReport();
            PortsDTCFS = trackerdatamanager.GetTodayMovementReportCFS();
            //call prashant ment
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueList = new EN.ImportExportProcessActivitycs();
                    RevenueList.process = Convert.ToString(row["Process"]);
                    RevenueList.Import = Convert.ToString(row["IMP"]);
                    RevenueList.Export = Convert.ToString(row["EXP"]);
                    RevenueList.Total = Convert.ToString(row["TOTAL"]);

                    ImportExportLastDays.Add(RevenueList);
                }
            }


            if (PortsDTCFS != null)
            {
                foreach (DataRow row in PortsDTCFS.Rows)
                {
                    EN.ImportExportProcessActivitycs RevenueListCFS = new EN.ImportExportProcessActivitycs();
                    RevenueListCFS.process = Convert.ToString(row["Process"]);
                    RevenueListCFS.Import = Convert.ToString(row["IMP"]);
                    RevenueListCFS.Export = Convert.ToString(row["EXP"]);
                    RevenueListCFS.Total = Convert.ToString(row["TOTAL"]);
                    RevenueListCFS.Last24HRSName = Convert.ToString(row["MonthName"]);
                    ImportExportLastDays.Add(RevenueListCFS);
                }
            }
            return ImportExportLastDays;
        }
        //CODE END BY RAHUL 23-01-2020
    }

}
