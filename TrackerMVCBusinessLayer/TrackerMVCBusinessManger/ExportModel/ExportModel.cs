using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using BE = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ExportModel
{
   public class ExportModel
    {

        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<BE.ExportCartingAllowEntities> getLoaction()
        {
            List<BE.ExportCartingAllowEntities> ExpensesrDL = new List<BE.ExportCartingAllowEntities>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "LocationMaster";
            string Column = "isnull(LocationID,0)LocationID,LocationName";
            string Condition = "";
            string OrderBy = "LocationName";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.ExportCartingAllowEntities CustomerList = new BE.ExportCartingAllowEntities();
                    CustomerList.LocationIID = Convert.ToInt32(row["LocationID"]);
                    CustomerList.Location = Convert.ToString(row["LocationName"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }

        public string SavepartyDebitEntry(DataTable Invoicedata, int CartingAllowNo, string AllowFromDate, int Userid)
        {
            string Message = "";
            string SrNo = "1";

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


            string strSql = "SELECT CASE WHEN MAX(CartingAllowID) IS NULL THEN 1 ELSE MAX(CartingAllowID)+1 END FROM exp_cartingallow ";
            DataTable dt1 = db.sub_GetDatatable(strSql);
            if (dt1.Rows.Count > 0)
            {
                CartingAllowNo = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("CartingAllowNo", CartingAllowNo);
            parameterList.Add("SrNo", SrNo);
            parameterList.Add("AllowFromDate", AllowFromDate);
            parameterList.Add("ADDEDBY", Userid);
            int i = db.AddTypeTableData("USP_insert_cartingallow", parameterList, Invoicedata, "cartingallow", "@cartingallow");


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


        public List<BE.ExportCartingAllowEntities> GetEquipment()
        {
            List<BE.ExportCartingAllowEntities> EquipmentDL = new List<BE.ExportCartingAllowEntities>();
            DataTable dt = new DataTable();
            string Table = "Carting_VehicleType_M";
            string Column = "VehicleTypeID,VehicleType";
            string Condition = "";
            string OrderBy = "VehicleType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ExportCartingAllowEntities EquipmentList = new BE.ExportCartingAllowEntities();
                    EquipmentList.VehicleTypeID = Convert.ToInt32(row["VehicleTypeID"]);
                    EquipmentList.VehicleType = Convert.ToString(row["VehicleType"]);
                    EquipmentDL.Add(EquipmentList);
                }
            }
            return EquipmentDL;
        }


        public List<BE.ExportCartingAllowEntities> GetEquipmentType()
        {
            List<BE.ExportCartingAllowEntities> EquipmentDL = new List<BE.ExportCartingAllowEntities>();
            DataTable dt = new DataTable();
            string Table = "equipmentm";
            string Column = "Id,Equipment";
            string Condition = "";
            string OrderBy = "Id desc";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ExportCartingAllowEntities EquipmentList = new BE.ExportCartingAllowEntities();
                    EquipmentList.EnID = Convert.ToInt32(row["Id"]);
                    EquipmentList.EquipmentType = Convert.ToString(row["Equipment"]);
                    EquipmentDL.Add(EquipmentList);
                }
            }
            return EquipmentDL;
        }

        public string SaveExportTruckIn(DataTable tablearray, int GateInAllowID, string GateInNoDate, int Userid)
        {
            string Message = "";
          
            int GateInNo = 0;

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


            string strSql = "Select IsNull(MAX (GateIn ),0)+1 from export_TruckIN WITH(XLOCK)  where IsCancel =0  ";
            DataTable dt1 = db.sub_GetDatatable(strSql);
            if (dt1.Rows.Count > 0)
            {
                GateInNo = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("GateInNo", GateInNo);
            parameterList.Add("GateInNoDate", GateInNoDate);
            parameterList.Add("ADDEDBY", Userid);
            parameterList.Add("GateInAllowID", GateInAllowID);
            int i = db.AddTypeTableData("USP_Insert_EXPORT_TRUCKIN", parameterList, tablearray, "ExportTruckIn", "@ExportTruckIn");


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


        public List<BE.ExportCartingTallyEntities> getVendor()
        {
            List<BE.ExportCartingTallyEntities> Vendor = new List<BE.ExportCartingTallyEntities>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "Vendor_M";
            string Column = "isnull(VendorId,0)VendorId,Name";
            string Condition = "";
            string OrderBy = "Name";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.ExportCartingTallyEntities CustomerList = new BE.ExportCartingTallyEntities();
                    CustomerList.VendorID = Convert.ToInt32(row["VendorId"]);
                    CustomerList.VendorName = Convert.ToString(row["Name"]);

                    Vendor.Add(CustomerList);
                }
            }
            return Vendor;
        }

        public string SaveCartingTally(DataTable Invoicedata, string VendorID, string CartingID, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("VendorID", VendorID);
            parameterList.Add("CartingAllowID", CartingID);
            parameterList.Add("AddedBy", Userid);
            int i = db.AddTypeTableData("usp_insert_carting_tally", parameterList, Invoicedata, "Carting", "@Carting");


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

        public List<BE.CHAtable> CHAtable()
        {
            List<BE.CHAtable> CHADL = new List<BE.CHAtable>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "CHA";
            string Column = "isnull(CHAID,0)CHAID,CHAName";
            string Condition = "";
            string OrderBy = "CHAName";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.CHAtable CHAList = new BE.CHAtable();
                    CHAList.CHAID = Convert.ToInt32(row["CHAID"]);
                    CHAList.CHAName = Convert.ToString(row["CHAName"]);

                    CHADL.Add(CHAList);
                }
            }
            return CHADL;
        }

        public List<BE.Agencytable> Agencytable()
        {
            List<BE.Agencytable> agencytables = new List<BE.Agencytable>();
            DataTable AgencyDT = new DataTable();
            string Table = "customer";
            string Column = "isnull(agid,0)agid,agname";
            string Condition = "";
            string OrderBy = "agname";
            AgencyDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (AgencyDT != null)
                {
                   foreach (DataRow row in AgencyDT.Rows)
                   {
                    BE.Agencytable AgencyList = new BE.Agencytable();
                    AgencyList.agid = Convert.ToInt32(row["agid"]);
                    AgencyList.agname = Convert.ToString(row["agname"]);

                    agencytables.Add(AgencyList);
                }
            }
            return agencytables; 
        }

        public List<BE.Shippertable> Shippertable()
        {
            List<BE.Shippertable> Shippertable = new List<BE.Shippertable>();
            DataTable ShippertDT = new DataTable();
            string Table = "customer";
            string Column = "isnull(agid,0)agid,agname";
            string Condition = "";
            string OrderBy = "agname";
            ShippertDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ShippertDT != null)
            {
                foreach (DataRow row in ShippertDT.Rows)
                {
                    BE.Shippertable ShippertList = new BE.Shippertable();
                    ShippertList.ShipperID = Convert.ToInt32(row["agid"]);
                    ShippertList.ShipperName = Convert.ToString(row["agname"]);

                    Shippertable.Add(ShippertList);
                }
            }
            return Shippertable;
        }

        public List<BE.CargoTypes> CargoType()
        {
            List<BE.CargoTypes> CargoTypetable = new List<BE.CargoTypes>();
            DataTable CargoTypesDT = new DataTable();
            string Table = "CargoType";
            string Column = "isnull(Cargotypeid,0)Cargotypeid,Cargotype";
            string Condition = "";
            string OrderBy = "Cargotype";
            CargoTypesDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CargoTypesDT != null)
            {
                foreach (DataRow row in CargoTypesDT.Rows)
                {
                    BE.CargoTypes CargoTypeList = new BE.CargoTypes();
                    CargoTypeList.Cargotypeid = Convert.ToInt32(row["Cargotypeid"]);
                    CargoTypeList.Cargotype = Convert.ToString(row["Cargotype"]);

                    CargoTypetable.Add(CargoTypeList);
                }
            }
            return CargoTypetable;
        }


        public List<BE.Ports> getPorts()
        {
            List<BE.Ports> PortsDL = new List<BE.Ports>();
            DataTable PortsDT = new DataTable();
            string Table = "Ports";
            string Column = "PortID,PortName";
            string Condition = "";
            string OrderBy = "PortName";

            PortsDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    BE.Ports PortsList = new BE.Ports();
                    PortsList.PortID = Convert.ToInt32(row["PortID"]);
                    PortsList.PortName = Convert.ToString(row["PortName"]);

                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }

        public List<BE.StuffingType> getStuffing()
        {
            List<BE.StuffingType> stuffingType = new List<BE.StuffingType>();
            DataTable PortsDT = new DataTable();
            string Table = "StuffingType";
            string Column = "TypeId,StuffingType";
            string Condition = "";
            string OrderBy = "StuffingType";

            PortsDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    BE.StuffingType PortsList = new BE.StuffingType();
                    PortsList.StuffingTypeId = Convert.ToInt64(row["TypeId"]);
                    PortsList.StuffingTypeM = Convert.ToString(row["StuffingType"]);

                    stuffingType.Add(PortsList);
                }
            }
            return stuffingType;
        }

        public List<BE.Vessel> getVessel()
        {
            List<BE.Vessel> VesselDL = new List<BE.Vessel>();
            DataTable VesselDT = new DataTable();
            string Table = "Vessels";
            string Column = "VesselID,VesselName";
            string Condition = "IsActive=1";
            string OrderBy = "VesselName";

            VesselDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (VesselDT != null)
            {
                foreach (DataRow row in VesselDT.Rows)
                {
                    BE.Vessel VesselList = new BE.Vessel();
                    VesselList.VesselID = Convert.ToInt32(row["VesselID"]);
                    VesselList.VesselName = Convert.ToString(row["VesselName"]);

                    VesselDL.Add(VesselList);
                }
            }
            return VesselDL;
        }

        public List<BE.POL> getPOL()
        {
            List<BE.POL> PolDL = new List<BE.POL>();
            DataTable PolDT = new DataTable();
            string Table = "PODMaster";
            string Column = "PODID,PODName";
            string Condition = "";
            string OrderBy = "PODName";

            PolDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PolDT != null)
            {
                foreach (DataRow row in PolDT.Rows)
                {
                    BE.POL PolList = new BE.POL();
                    PolList.PODID = Convert.ToInt32(row["PODID"]);
                    PolList.PODName = Convert.ToString(row["PODName"]);
                    PolDL.Add(PolList);
                }
            }
            return PolDL;
        }

        public List<BE.equipmentm> equipmentm()
        {
            List<BE.equipmentm> equipmentm = new List<BE.equipmentm>();
            DataTable PolDT = new DataTable();
            string Table = "equipmentm";
            string Column = "Id,Equipment";
            string Condition = "";
            string OrderBy = "Equipment";

            PolDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PolDT != null)
            {
                foreach (DataRow row in PolDT.Rows)
                {
                    BE.equipmentm equipmentmList = new BE.equipmentm();
                    equipmentmList.Id = Convert.ToInt32(row["Id"]);
                    equipmentmList.Equipment = Convert.ToString(row["Equipment"]);
                    equipmentm.Add(equipmentmList);
                }
            }
            return equipmentm;
        }

        public string StuffingSave(DataTable Invoicedata, int StuffingRequestNo, string lblcagencyid, string lblvesselID, string VIANo, string POD, string Rotation, string Remarks, int Userid)
        {
            string Message = "";
            string SrNo = "1";

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


            string strSql = "SELECT CASE WHEN MAX(STUFFREQUESTID) IS NULL THEN 1 ELSE MAX(STUFFREQUESTID)+1 END FROM exp_stuffingrequest ";
            DataTable dt1 = db.sub_GetDatatable(strSql);
            if (dt1.Rows.Count > 0)
            {
                StuffingRequestNo = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("STUFFREQUESTID", StuffingRequestNo);
            parameterList.Add("AGENCYID", lblcagencyid);
            parameterList.Add("VESSELID", lblvesselID);
            parameterList.Add("VIANO", VIANo);
            parameterList.Add("POD", POD);
            parameterList.Add("ROTATION", Rotation);
            parameterList.Add("REMARKS", Remarks);
            parameterList.Add("ADDEDBY", Userid);
            int i = db.AddTypeTableData("USP_INSERT_EXP_STUFFINGREQUEST_Web", parameterList, Invoicedata, "StuffingRequest", "@StuffingRequest");


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

        public List<BE.Customer> getCustomer()
        {
            List<BE.Customer> CustomerDL = new List<BE.Customer>();
            DataTable CustomerDT = new DataTable();
            string Table = "Customer";
            string Column = "AGID,AGName";
            string Condition = "IsActive=1";
            string OrderBy = "AGName";

            CustomerDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    BE.Customer CustomerList = new BE.Customer();
                    CustomerList.AGID = Convert.ToInt32(row["AGID"]);
                    CustomerList.AGName = Convert.ToString(row["AGName"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }

        public BE.EmptyInEntities GetEmptyInDetails()
        {
            DataSet DDS = new DataSet();
            DDS = trackerdatamanager.GetEmptyInDropdown();
            BE.EmptyInEntities EmptyIn = new BE.EmptyInEntities();
            List<BE.ISOCodesGateIn> ISOCodesGateInList = new List<BE.ISOCodesGateIn>();
            List<BE.Customer> CustomerEmptyIn = new List<BE.Customer>();
            List<BE.ShipLines> ShipList = new List<BE.ShipLines>();
            List<BE.ContainerTypeGateIn> ContainerList = new List<BE.ContainerTypeGateIn>();
            List<BE.TransporterGateIn> TransporterGateInList = new List<BE.TransporterGateIn>();
            List<BE.LocationGateIn> LocationList = new List<BE.LocationGateIn>();
            List<BE.Condition> ConditionList = new List<BE.Condition>();
            List<BE.MovementTypeEmptyIn> MovementList = new List<BE.MovementTypeEmptyIn>();
            List<BE.SizeEmptyIn> SizeEmptyList = new List<BE.SizeEmptyIn>();
            if (DDS.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in DDS.Tables[0].Rows)
                {
                    BE.ISOCodesGateIn ISO = new BE.ISOCodesGateIn();
                    ISO.ID = Convert.ToInt16(row["ISOID"]);
                    ISO.ISOCode = Convert.ToString(row["ISOCode"]);

                    ISOCodesGateInList.Add(ISO);
                }
            }
            if (DDS.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow row in DDS.Tables[1].Rows)
                {
                    BE.Customer customer = new BE.Customer();
                    customer.AGID = Convert.ToInt16(row["AGID"]);
                    customer.AGName = Convert.ToString(row["AGNAME"]);

                    CustomerEmptyIn.Add(customer);
                }
            }
            if (DDS.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow row in DDS.Tables[2].Rows)
                {
                    BE.ShipLines Ship = new BE.ShipLines();
                    Ship.SLID = Convert.ToInt16(row["SLID"]);
                    Ship.SLName = Convert.ToString(row["SLNAME"]);

                    ShipList.Add(Ship);
                }
            }

            if (DDS.Tables[3].Rows.Count != 0)
            {
                foreach (DataRow row in DDS.Tables[3].Rows)
                {
                    BE.ContainerTypeGateIn CType = new BE.ContainerTypeGateIn();
                    CType.ID = Convert.ToInt16(row["ContainerTypeID"]);
                    CType.Type = Convert.ToString(row["ContainerType"]);

                    ContainerList.Add(CType);
                }
            }

            if (DDS.Tables[4].Rows.Count != 0)
            {
                foreach (DataRow row in DDS.Tables[4].Rows)
                {
                    BE.TransporterGateIn Transporter = new BE.TransporterGateIn();
                    Transporter.ID = Convert.ToInt16(row["TransID"]);
                    Transporter.Transporter = Convert.ToString(row["TransName"]);

                    TransporterGateInList.Add(Transporter);
                }
            }
            if (DDS.Tables[5].Rows.Count != 0)
            {
                foreach (DataRow row in DDS.Tables[5].Rows)
                {
                    BE.LocationGateIn Location = new BE.LocationGateIn();
                    Location.ID = Convert.ToInt16(row["LocationID"]);
                    Location.Location = Convert.ToString(row["Location"]);

                    LocationList.Add(Location);
                }
            }

            if (DDS.Tables[6].Rows.Count != 0)
            {
                foreach (DataRow row in DDS.Tables[6].Rows)
                {
                    BE.Condition Condition = new BE.Condition();

                    Condition.ConditionName = Convert.ToString(row["Condition"]);

                    ConditionList.Add(Condition);
                }
            }
            if (DDS.Tables[7].Rows.Count != 0)
            {
                foreach (DataRow row in DDS.Tables[7].Rows)
                {
                    BE.MovementTypeEmptyIn Movementype = new BE.MovementTypeEmptyIn();

                    Movementype.MovementType = Convert.ToString(row["MovementType"]);

                    MovementList.Add(Movementype);
                }
            }
            if (DDS.Tables[8].Rows.Count != 0)
            {
                foreach (DataRow row in DDS.Tables[8].Rows)
                {
                    BE.SizeEmptyIn Size = new BE.SizeEmptyIn();

                    Size.SizeEmpty = Convert.ToString(row["Size"]);

                    SizeEmptyList.Add(Size);
                }
            }
            EmptyIn.ISOCodesGateIn = ISOCodesGateInList;
            EmptyIn.Customer = CustomerEmptyIn;
            EmptyIn.ShipLines = ShipList;
            EmptyIn.ContainerTypeGateIn = ContainerList;
            EmptyIn.TransporterGateIn = TransporterGateInList;
            EmptyIn.LocationGateIn = LocationList;
            EmptyIn.Condition = ConditionList;
            EmptyIn.MovementType = MovementList;
            EmptyIn.SizeEmpty = SizeEmptyList;

            return EmptyIn;
        }


        public string SaveBTTCARGO(BE.ExportShippingBillEnt BttcargoJOData, int Userid)
        {
            string Message = "";
            int JONO = 0;

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


            string strSql = "SELECT CASE WHEN MAX(BTTID) IS NULL THEN 1 ELSE MAX(BTTID)+1 END FROM exp_bttcargo  ";
            DataTable dt1 = db.sub_GetDatatable(strSql);
            if (dt1.Rows.Count > 0)
            {
                JONO = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("BTTID", JONO);
            parameterList.Add("Bttdate", BttcargoJOData.JODate);
            parameterList.Add("sbno", BttcargoJOData.SBNO);
            parameterList.Add("sbentryid", BttcargoJOData.EntryID);
            parameterList.Add("bttqty", BttcargoJOData.BttQty);
            parameterList.Add("bttwt", BttcargoJOData.BttWt);
            parameterList.Add("bttarea", BttcargoJOData.BttArea);
            parameterList.Add("type", BttcargoJOData.Type);
            parameterList.Add("VehicleNo", BttcargoJOData.VehicleNo);

            parameterList.Add("addedby", Userid);
            parameterList.Add("addedon", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
            int i = db.SaveBTTCARGO("SP_INSERT_BTTCARGO", parameterList);


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

        public string SaveExpCargoGP(BE.GatePassEntities BttcargoGPData, DataTable GPElementsdataTable, int UserId)
        {
            string message = "";
            int GatePassNo = 0;
            DataTable db = new DataTable();

            try
            {
                TrackerMVCDataLayer.Helper.DBOperations DataManager = new TrackerMVCDataLayer.Helper.DBOperations();

                //BttcargoJOData.AddedBy = UserId;

                string strSql = "SELECT CASE WHEN MAX(GPNo) IS NULL THEN 1 ELSE MAX(GPNo)+1 END FROM exp_bttcargogatepass ";
                DataTable dt1 = DataManager.sub_GetDatatable(strSql);
                if (dt1.Rows.Count > 0)
                {
                    GatePassNo = Convert.ToInt32(dt1.Rows[0][0].ToString());
                }

                Dictionary<object, object> parameterList = new Dictionary<object, object>();
                parameterList.Add("GPNO", GatePassNo);
                parameterList.Add("GPDATE", Convert.ToDateTime(BttcargoGPData.GPDate).ToString("dd-MMM-yyyy HH:mm:ss"));
                parameterList.Add("ADDEDBY", UserId);

                parameterList.Add("ADDEDON", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
                parameterList.Add("BTTTYPE", BttcargoGPData.BTTType);


                DataTable JobOrderDT = new DataTable();
                JobOrderDT = DataManager.SaveExpCargoGP("USP_SAVE_EXP_BTTCARGOGATEPASS", parameterList, GPElementsdataTable);
                if (JobOrderDT != null)
                {
                    foreach (DataRow row in JobOrderDT.Rows)
                    {

                        message = Convert.ToString(row["Message"]);

                    }
                }

                return message;

            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public string SaveMovementAccCTR(BE.MovementAcceptCTR MovementCTRData, int Userid)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


            //string strSql = "SELECT CASE WHEN MAX(BTTID) IS NULL THEN 1 ELSE MAX(BTTID)+1 END FROM exp_bttcargo  ";
            //DataTable dt1 = db.sub_GetDatatable(strSql);
            //if (dt1.Rows.Count > 0)
            //{
            //    JONO = Convert.ToInt32(dt1.Rows[0][0].ToString());
            //}

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("MOVEMENTRCVD", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
            parameterList.Add("AGENCYID", MovementCTRData.AgencyID);
            parameterList.Add("ENTRYID", MovementCTRData.EntryID);
            parameterList.Add("CONTAINERNO", MovementCTRData.ContainerNo);
            parameterList.Add("CSIZE", MovementCTRData.Size);
            parameterList.Add("MOVETYPE", MovementCTRData.Cargotype);
            parameterList.Add("VIANO", MovementCTRData.Viano);
            parameterList.Add("ISWEIGHED", 0);
            parameterList.Add("ADDEDBY", Userid);
            parameterList.Add("ADDEDON", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
            parameterList.Add("Remarks", Userid);
            parameterList.Add("POLID", MovementCTRData.POLID);
            parameterList.Add("TransTypeID", MovementCTRData.TransTypeID);
            parameterList.Add("leono", MovementCTRData.LEONo);
            parameterList.Add("deodate", MovementCTRData.LEODate);

            int i = db.SaveMovementAccCTR("USP_INSERT_EXP_MOVEMENTACCEPT", parameterList);


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

        public string SaveSBWiseMovementAcceptEntry(BE.MovementAcceptCTR SBWiseMovementAccData, DataTable SBElementsdataTable, int UserId)
        {
            string message = "";
            int GatePassNo = 0;
            DataTable db = new DataTable();

            try
            {
                TrackerMVCDataLayer.Helper.DBOperations DataManager = new TrackerMVCDataLayer.Helper.DBOperations();

                //BttcargoJOData.AddedBy = UserId;

                //string strSql = "SELECT CASE WHEN MAX(GPNo) IS NULL THEN 1 ELSE MAX(GPNo)+1 END FROM exp_bttcargogatepass ";
                //DataTable dt1 = DataManager.sub_GetDatatable(strSql);
                //if (dt1.Rows.Count > 0)
                //{
                //    GatePassNo = Convert.ToInt32(dt1.Rows[0][0].ToString());
                //}



                Dictionary<object, object> parameterList = new Dictionary<object, object>();
                parameterList.Add("MOVEMENTRCVD", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
                parameterList.Add("MOVETYPE", SBWiseMovementAccData.MoveType);
                parameterList.Add("TransTypeID", SBWiseMovementAccData.TransTypeID);
                parameterList.Add("ISWEIGHED", SBWiseMovementAccData.ISWeighed);
                parameterList.Add("Viano", SBWiseMovementAccData.Viano);
                parameterList.Add("ADDEDBY", UserId);
                parameterList.Add("ADDEDON", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
                parameterList.Add("Remarks", SBWiseMovementAccData.Remarks);
                parameterList.Add("POLID", SBWiseMovementAccData.POLID);
                parameterList.Add("LEONo", SBWiseMovementAccData.LEONo);
                parameterList.Add("LEODate", SBWiseMovementAccData.LEODate);


                DataTable JobOrderDT = new DataTable();
                JobOrderDT = DataManager.SaveSBWiseMovementAcceptEntry("USP_SaveSBWiseMovementEntry", parameterList, SBElementsdataTable);
                if (JobOrderDT != null)
                {
                    foreach (DataRow row in JobOrderDT.Rows)
                    {

                        message = Convert.ToString(row["Message"]);

                    }
                }

                return message;

            }
            catch (Exception ex)
            {

                throw ex;

            }

        }



        /// Code for the Export tariff

        public List<BE.ExpHeadMasterEnt> InvoiceTypeDDL()
        {
            List<BE.ExpHeadMasterEnt> InvoiceType = new List<BE.ExpHeadMasterEnt>();
            DataTable InvType = new DataTable();
            string Table = "import_invoicetype";
            string Column = "ID,Invoice_Type";
            string Condition = "";
            string OrderBy = "Invoice_Type";

            InvType = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (InvType != null)
            {
                foreach (DataRow row in InvType.Rows)
                {
                    BE.ExpHeadMasterEnt INVTypeList = new BE.ExpHeadMasterEnt();
                    INVTypeList.InvTId = Convert.ToInt32(row["ID"]);
                    INVTypeList.InvType = Convert.ToString(row["Invoice_Type"]);

                    InvoiceType.Add(INVTypeList);
                }
            }
            return InvoiceType;
        }
        public List<BE.ExpHeadMasterEnt> HSNCodeDDL()
        {
            List<BE.ExpHeadMasterEnt> HSNSelect = new List<BE.ExpHeadMasterEnt>();
            DataTable dataTable = new DataTable();
            string Table = "HSN_MASTER";
            string Column = "isnull(ID,0)ID,HSN_Code";
            string Condition = "";
            string OrderBy = "HSN_Code";

            dataTable = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpHeadMasterEnt HSNSelectlist = new BE.ExpHeadMasterEnt();
                    HSNSelectlist.HSNID = Convert.ToString(row["HSN_Code"]);
                    HSNSelectlist.HSNCodeL = Convert.ToString(row["HSN_Code"]);

                    HSNSelect.Add(HSNSelectlist);
                }
            }
            return HSNSelect;
        }

        public List<BE.ExpHeadMasterEnt> IMPGroupDDl()
        {
            List<BE.ExpHeadMasterEnt> IMPGroup = new List<BE.ExpHeadMasterEnt>();
            DataTable dataTable = new DataTable();
            string Table = "exp_accountGroups";
            string Column = "isnull(GroupID,0)GroupID,groupName";
            string Condition = "";
            string OrderBy = "groupName";

            dataTable = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpHeadMasterEnt IMPGLIst = new BE.ExpHeadMasterEnt();
                    IMPGLIst.IMPGID = Convert.ToInt32(row["GroupID"]);
                    IMPGLIst.IMPGName = Convert.ToString(row["groupName"]);

                    IMPGroup.Add(IMPGLIst);
                }
            }
            return IMPGroup;
        }

        public List<BE.ExpHeadMasterEnt> getTaxName()
        {
            List<BE.ExpHeadMasterEnt> TaxDL = new List<BE.ExpHeadMasterEnt>();
            DataTable TaxNameDT = new DataTable();
            string Table = "settings_taxes";
            string Column = "isnull(settingsID,0)settingsID,taxname";
            string Condition = "";
            string OrderBy = "taxname";

            TaxNameDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TaxNameDT != null)
            {
                foreach (DataRow row in TaxNameDT.Rows)
                {
                    BE.ExpHeadMasterEnt CustomerList = new BE.ExpHeadMasterEnt();
                    CustomerList.TaxID = Convert.ToInt32(row["settingsID"]);
                    CustomerList.TaxName = Convert.ToString(row["taxname"]);

                    TaxDL.Add(CustomerList);
                }
            }
            return TaxDL;
        }

        public List<BE.exporterShipping> GetExportShippingDetails()
        {
            List<BE.exporterShipping> ShipLinesDL = new List<BE.exporterShipping>();
            DataTable ShipLinesDT = new DataTable();
            string Table = "exp_shippers";
            string Column = "shipperID,shippername";
            string Condition = "isactive=1";
            string OrderBy = "shippername";

            ShipLinesDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ShipLinesDT != null)
            {
                foreach (DataRow row in ShipLinesDT.Rows)
                {
                    BE.exporterShipping ShipLinesList = new BE.exporterShipping();
                    ShipLinesList.Exportershippingid = Convert.ToInt32(row["shipperID"]);
                    ShipLinesList.ExporterShippingname = Convert.ToString(row["shippername"]);


                    ShipLinesDL.Add(ShipLinesList);
                }
            }
            return ShipLinesDL;
        }


        //Code For Tariff Setting Moduel
        public BE.ImportTraiffSettingEntities ExporttrariffSettingDropDown()
        {
            BE.ImportTraiffSettingEntities ImporttariffSettingID = new BE.ImportTraiffSettingEntities();
            List<BE.ImportaccountMaster> ImportaccountMaster = new List<BE.ImportaccountMaster>();
            List<BE.CommodityMaster> CommodityList = new List<BE.CommodityMaster>();
            List<BE.ImportInvoiceType> ImportInvoiceList = new List<BE.ImportInvoiceType>();
            List<BE.ChagresBasedOn> ChargesbasedList = new List<BE.ChagresBasedOn>();
            List<BE.SettingTax> SettingTaxList = new List<BE.SettingTax>();
            List<BE.TransportType_m> TransportList = new List<BE.TransportType_m>();
            List<BE.ImportJoType> ImportJoList = new List<BE.ImportJoType>();
            List<BE.PortsEntites> PortList = new List<BE.PortsEntites>();
            List<BE.CargoEntititesList> CargoList = new List<BE.CargoEntititesList>();


            DataSet ds = new DataSet();
            ds = trackerdatamanager.GetDropDownListExporttariffsetting();

            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        BE.ImportaccountMaster Account = new BE.ImportaccountMaster();
                        Account.AccountID = Convert.ToInt32(row["AccountID"]);
                        Account.AccountName = Convert.ToString(row["AccountName"]);

                        ImportaccountMaster.Add(Account);
                    }
                }
            }
            if (ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        BE.CommodityMaster Commodity = new BE.CommodityMaster();
                        Commodity.Commodity_Group_ID = Convert.ToInt32(row["Commodity_Group_ID"]);
                        Commodity.Commodity_Group_Name = Convert.ToString(row["Commodity_Group_Name"]);

                        CommodityList.Add(Commodity);
                    }
                }
            }
            if (ds.Tables[2] != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        BE.ImportInvoiceType ImportInvoice = new BE.ImportInvoiceType();
                        ImportInvoice.ID = Convert.ToInt16(row["ID"]);
                        ImportInvoice.InvoiceType = Convert.ToString(row["Invoice_type"]);

                        ImportInvoiceList.Add(ImportInvoice);
                    }
                }
            }
            if (ds.Tables[3] != null)
            {
                if (ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[3].Rows)
                    {
                        BE.ChagresBasedOn Chargesbased = new BE.ChagresBasedOn();
                        Chargesbased.Chargeid = Convert.ToInt16(row["ID"]);
                        Chargesbased.BasedOn = Convert.ToString(row["basedon"]);

                        ChargesbasedList.Add(Chargesbased);
                    }
                }
            }
            if (ds.Tables[4] != null)
            {
                if (ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[4].Rows)
                    {
                        BE.SettingTax SettingTax = new BE.SettingTax();
                        SettingTax.Settingid = Convert.ToInt16(row["settingsid"]);
                        SettingTax.TaxName = Convert.ToString(row["taxname"]);

                        SettingTaxList.Add(SettingTax);
                    }
                }
            }
            if (ds.Tables[5] != null)
            {
                if (ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[5].Rows)
                    {
                        BE.TransportType_m Transport = new BE.TransportType_m();
                        Transport.TransportID = Convert.ToInt16(row["Transport_Type_ID"]);
                        Transport.TransportType = Convert.ToString(row["Transport_Type"]);

                        TransportList.Add(Transport);
                    }
                }
            }
            if (ds.Tables[6] != null)
            {
                if (ds.Tables[6].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[6].Rows)
                    {
                        BE.ImportJoType ImportJo = new BE.ImportJoType();
                        ImportJo.Jo_type_id = Convert.ToInt16(row["Jo_Type_ID"]);
                        ImportJo.Jo_type = Convert.ToString(row["Jo_Type"]);

                        ImportJoList.Add(ImportJo);
                    }
                }
            }
            if (ds.Tables[7] != null)
            {
                if (ds.Tables[7].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[7].Rows)
                    {
                        BE.PortsEntites Port = new BE.PortsEntites();
                        Port.Portid = Convert.ToInt16(row["PortID"]);
                        Port.PortName = Convert.ToString(row["PortName"]);

                        PortList.Add(Port);
                    }
                }
            }
            if (ds.Tables[8] != null)
            {
                if (ds.Tables[8].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[8].Rows)
                    {
                        BE.CargoEntititesList Cargo = new BE.CargoEntititesList();
                        Cargo.cargoid = Convert.ToInt16(row["Cargotypeid"]);
                        Cargo.cargoname = Convert.ToString(row["CargoType"]);

                        CargoList.Add(Cargo);
                    }
                }
            }

            ImporttariffSettingID.ImportaccountMaster = ImportaccountMaster;
            ImporttariffSettingID.CommodityMaster = CommodityList;
            ImporttariffSettingID.ImportInvoiceType = ImportInvoiceList;
            ImporttariffSettingID.ChagresBasedOn = ChargesbasedList;
            ImporttariffSettingID.SettingTax = SettingTaxList;
            ImporttariffSettingID.TransportType_m = TransportList;
            ImporttariffSettingID.ImportJoType = ImportJoList;
            ImporttariffSettingID.PortsEntites = PortList;
            ImporttariffSettingID.CargoEntititesList = CargoList;


            return ImporttariffSettingID;
        }


        public List<BE.LocationMaster> getLocation()
        {
            List<BE.LocationMaster> locationDL = new List<BE.LocationMaster>();
            DataTable dt = new DataTable();
            string Table = "Ext_Location_m";
            string Column = "LocationID,Location";
            string Condition = "";
            string OrderBy = "Location";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.LocationMaster locationList = new BE.LocationMaster();
                    locationList.LocationID = Convert.ToInt32(row["LocationID"]);
                    locationList.Location = Convert.ToString(row["Location"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<BE.ContainerSize> GetSizeDetails()
        {
            List<BE.ContainerSize> locationDL = new List<BE.ContainerSize>();
            DataTable dt = new DataTable();
            string Table = "ContainerSize";
            string Column = "ContainerSizeID,ContainerSize";
            string Condition = "";
            string OrderBy = "ContainerSize";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ContainerSize locationList = new BE.ContainerSize();
                    locationList.ContainerSizeID = Convert.ToInt32(row["ContainerSizeID"]);
                    locationList.ContainerSizeName = Convert.ToString(row["ContainerSize"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<BE.DeliveryTypeDetails> GetDeliveryDetails()
        {
            List<BE.DeliveryTypeDetails> locationDL = new List<BE.DeliveryTypeDetails>();
            DataTable dt = new DataTable();
            string Table = "Exp_BillingType";
            string Column = "Typeid,billtype";
            string Condition = "";
            string OrderBy = "billtype";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.DeliveryTypeDetails locationList = new BE.DeliveryTypeDetails();
                    locationList.DeliveryID = Convert.ToInt32(row["Typeid"]);
                    locationList.DeliveryType = Convert.ToString(row["billtype"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<BE.SlabDetails> GetSlabDetails()
        {
            List<BE.SlabDetails> locationDL = new List<BE.SlabDetails>();
            DataTable dt = new DataTable();
            string Table = "import_slabs";
            string Column = "distinct slabid";
            string Condition = "slabID not in (select slabID from import_tariffdetails) order by slabid";
            string OrderBy = "";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.SlabDetails locationList = new BE.SlabDetails();
                    locationList.SlabId = Convert.ToInt32(row["slabid"]);
                    locationList.SlabId = Convert.ToInt32(row["slabid"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<BE.importtariffdetails> Getimporttariffdetails()
        {
            List<BE.importtariffdetails> locationDL = new List<BE.importtariffdetails>();
            DataTable dt = new DataTable();
            string Table = "exp_tariffmaster";
            string Column = "Distinct TariffID,TariffDescription";
            string Condition = "";
            string OrderBy = "TariffDescription";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.importtariffdetails locationList = new BE.importtariffdetails();
                    locationList.TariffID = Convert.ToInt32(row["TariffID"]);
                    locationList.TariffDescription = Convert.ToString(row["TariffDescription"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }



        public List<BE.ContainerType> GetContainerType()
        {
            List<BE.ContainerType> locationDL = new List<BE.ContainerType>();
            DataTable dt = new DataTable();
            string Table = "ContainerType";
            string Column = "Distinct ContainerTypeID,ContainerType";
            string Condition = "";
            string OrderBy = "ContainerType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ContainerType locationList = new BE.ContainerType();
                    locationList.ContainerTypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    locationList.ContainerTypeName = Convert.ToString(row["ContainerType"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }


        public List<BE.ImportAccountMaster> GetAccountHead()
        {
            List<BE.ImportAccountMaster> locationDL = new List<BE.ImportAccountMaster>();
            DataTable dt = new DataTable();
            string Table = "Exp_accountMaster";
            string Column = "Distinct AccountID,AccountName";
            string Condition = "";
            string OrderBy = "AccountName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ImportAccountMaster locationList = new BE.ImportAccountMaster();
                    locationList.AccountID = Convert.ToInt32(row["AccountID"]);
                    locationList.AccountName = Convert.ToString(row["AccountName"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<BE.TariffAddDetailsEntites> GetExportAddTabledetails(string TariffID, string From, string TO, string Accounting
           , string Size, string ChargesBased, string FixedAmt, string Days, string rate
           , string Slabid, string ScanType, string Location, string StuffLocation, string Jotype, string Commodity, string TransType,
           string Port, string Insurance, string AccountingID, DataTable dataTable, DataTable dataTable1, string TaxID, string InvoiceType)
        {
            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable1.Rows.Count; j++)
                {
                    if (Accounting != "")
                    {

                        BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                        DPDTList.TariffID = Convert.ToString(TariffID);
                        DPDTList.DeliveryType = Convert.ToString(dataTable.Rows[i].Field<string>("DeliveryType"));
                        DPDTList.From = Convert.ToString(Convert.ToDateTime(From).ToString("dd-MMM-yyyy"));
                        DPDTList.TO = Convert.ToString(Convert.ToDateTime(TO).ToString("dd-MMM-yyyy"));
                        DPDTList.Accounting = Convert.ToString(Accounting);
                        DPDTList.Size = Convert.ToString(Size);
                        DPDTList.Type = Convert.ToString(dataTable1.Rows[j].Field<string>("Type"));


                        DPDTList.InvoiceType = Convert.ToString(InvoiceType);


                        string strSQL = "";
                        strSQL = " USP_Show_ExportChargeBasedON '" + ChargesBased + "'";
                        dt1 = db.sub_GetDatatable(strSQL);
                        if (dt1.Rows.Count > 0)
                        {
                            DPDTList.Ftype = Convert.ToString(dt1.Rows[0]["sorf"]);
                        }
                        else
                        {
                            DPDTList.Ftype = Convert.ToString(0);
                        }
                        if (Slabid != null)
                        {
                            DPDTList.Slabid = Convert.ToString(Slabid);
                        }
                        else
                        {
                            DPDTList.Slabid = Convert.ToString(0);
                        }
                        if (FixedAmt != null)
                        {
                            DPDTList.FixedAmt = Convert.ToString(FixedAmt);
                        }
                        else
                        {
                            DPDTList.FixedAmt = Convert.ToString(0);
                        }
                        if (Insurance != null)
                        {
                            DPDTList.Insurance = Convert.ToString(Insurance);
                        }
                        else
                        {
                            DPDTList.Insurance = Convert.ToString(0);
                        }
                        if (rate != null)
                        {
                            DPDTList.rate = Convert.ToString(rate);
                        }
                        else
                        {
                            DPDTList.rate = Convert.ToString(0);
                        }
                        if (Days != null)
                        {
                            DPDTList.Days = Convert.ToString(Days);
                        }
                        else
                        {
                            DPDTList.Days = Convert.ToString(0);
                        }
                        DPDTList.Tax = Convert.ToString(TaxID);

                        DPDTList.CurrencyType = Convert.ToString('I');
                        DPDTList.TransType = Convert.ToString(TransType);
                        DPDTList.Port = Convert.ToString(Port);
                        DPDTList.Freedays = Convert.ToString(Days);
                        DPDTList.Location = Convert.ToString(Location);
                        DPDTList.StuffLocation = Convert.ToString(StuffLocation);
                        DPDTList.Jotype = Convert.ToString(Jotype);
                        DPDTList.ScanType = Convert.ToString(ScanType);
                        DPDTList.AccountingID = Convert.ToString(AccountingID);
                        DPDTList.Entryid = Convert.ToString(AccountingID);

                        Getdetails.Add(DPDTList);

                    }
                }
            }

            return Getdetails;
        }

        public List<BE.TariffAddDetailsEntites> ExportSavedataForGetdata(DataTable ImportData, int Userid)
        {


            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();



            for (int k = 0; k < ImportData.Rows.Count; k++)
            {







                string strSQL2 = "";
                strSQL2 = "INSERT INTO TempExport_tariffdetails(TARIFFID, DELIVERYTYPE, EFFECTIVEFROM, EFFECTIVEUPTO, ACCOUNTNAME, CONTAINERSIZE, CONTAINERTYPE, SORF, SLABID, INSRATE, FIXEDAMT, RATEPERMT, TAXID, INVOICETYPE, CURRENCY_TYPE, TRANSTYPE, PORTNAME, FREEDAY, LOCATIONID, Stufflocation,JOTYPE, SCANTYPE, ACCOUNTID, ADDEDBY) VALUES";
                strSQL2 += " ('" + ImportData.Rows[k].Field<string>("TariffID") + "','" + ImportData.Rows[k].Field<string>("DeliveryType11") + "','" + ImportData.Rows[k].Field<string>("From") + "','" + ImportData.Rows[k].Field<string>("TO") + "',";
                strSQL2 += " '" + ImportData.Rows[k].Field<string>("AccountingID") + "','" + ImportData.Rows[k].Field<string>("Size") + "','" + ImportData.Rows[k].Field<string>("Type1") + "','" + ImportData.Rows[k].Field<string>("Ftype") + "','" + ImportData.Rows[k].Field<string>("Slabid") + "','" + ImportData.Rows[k].Field<string>("Insurance") + "','" + ImportData.Rows[k].Field<string>("FixedAmt") + "',";
                strSQL2 += " '" + ImportData.Rows[k].Field<string>("rate") + "','" + ImportData.Rows[k].Field<string>("Tax") + "','" + ImportData.Rows[k].Field<string>("InvoiceType") + "','" + ImportData.Rows[k].Field<string>("CurrencyType") + "','" + ImportData.Rows[k].Field<string>("TransType") + "','" + ImportData.Rows[k].Field<string>("Port") + "','" + ImportData.Rows[k].Field<string>("Freedays") + "',";
                strSQL2 += " '" + ImportData.Rows[k].Field<string>("Location") + "','" + ImportData.Rows[k].Field<string>("StuffLocation") + "','" + ImportData.Rows[k].Field<string>("Jotype") + "','" + ImportData.Rows[k].Field<string>("ScanType") + "','" + ImportData.Rows[k].Field<string>("AccountingID") + "','" + Userid + "')";
                dt = db.sub_GetDatatable(strSQL2);



            }







            DataTable getdt = new DataTable();

            getdt = trackerdatamanager.GetExportAddedTariffsettingDetails(Userid);
            foreach (DataRow row in getdt.Rows)
            {

                BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                DPDTList.TariffID = Convert.ToString(row["tariffID"]);
                DPDTList.DeliveryType = Convert.ToString(row["deliverytype"]);
                DPDTList.From = Convert.ToString(row["effectivefrom"]);
                DPDTList.TO = Convert.ToString(row["effectiveupto"]);
                DPDTList.Accounting = Convert.ToString(row["accountName"]);
                DPDTList.Size = Convert.ToString(row["containersize"]);
                DPDTList.Type = Convert.ToString(row["containertype"]);
                DPDTList.Ftype = Convert.ToString(row["SorF"]);
                DPDTList.Slabid = Convert.ToString(row["slabID"]);
                DPDTList.FixedAmt = Convert.ToString(row["fixedamt"]);
                DPDTList.Insurance = Convert.ToString(row["insrate"]);
                DPDTList.rate = Convert.ToString(row["ratepermt"]);
                DPDTList.Days = Convert.ToString(row["FreeDay"]);

                DPDTList.Tax = Convert.ToString(row["TaxID"]);
                DPDTList.InvoiceType = Convert.ToString(row["InvoiceType"]);
                DPDTList.CurrencyType = Convert.ToString(row["Currency_type"]);
                DPDTList.TransType = Convert.ToString(row["TransType"]);
                DPDTList.Port = Convert.ToString(row["PortName"]);
                DPDTList.Freedays = Convert.ToString(row["FreeDay"]);
                DPDTList.Location = Convert.ToString(row["LocationID"]);
                DPDTList.StuffLocation = Convert.ToString(row["StuffLocation"]);
                DPDTList.Jotype = Convert.ToString(row["JoType"]);
                DPDTList.ScanType = Convert.ToString(row["ScanType"]);
                DPDTList.AccountingID = Convert.ToString(row["accountID"]);
                DPDTList.Entryid = Convert.ToString(row["Entryid"]);
                Getdetails.Add(DPDTList);


            }

            return Getdetails;
        }



        public string ExportCheckEffective(DataTable ImportData, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int k = 0; k < ImportData.Rows.Count; k++)
            {


                DataTable PortsDS = new DataTable();
                DataTable PortsDS1 = new DataTable();



                string strSQL1 = "";
                strSQL1 = "select FORMAT(cast(effectivefrom as datetime),'dd MMM yyyy HH:mm') as effectivefrom from Export_tariffdetails WHERE TariffID='" + ImportData.Rows[k].Field<string>("TariffID") + "'";
                strSQL1 += " AND DeliveryType='" + ImportData.Rows[k].Field<string>("DeliveryType") + "' AND AccountID=" + ImportData.Rows[k].Field<string>("AccountingID") + " AND ContainerSize=" + ImportData.Rows[k].Field<string>("Size") + "";
                strSQL1 += " AND ContainerType='" + ImportData.Rows[k].Field<string>("Type") + "' AND EffectiveUpTo >'" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "', AND LocationID ='" + ImportData.Rows[k].Field<string>("Location") + "' AND JoType ='" + ImportData.Rows[k].Field<string>("Jotype") + "'";


                PortsDS1 = db.sub_GetDatatable(strSQL1);
                if (PortsDS1.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(ImportData.Rows[k].Field<string>("From")) > Convert.ToDateTime(PortsDS1.Rows[0].Field<string>("effectivefrom")))
                    {

                        Message += "Effective From date cannot be less than effective previous. " + ImportData.Rows[k].Field<string>("Size") + " + " + ImportData.Rows[k].Field<string>("Type") + "";
                    }
                    else
                    {
                        Message += "";
                    }
                }




            }


            return Message;
        }



        public string SaveExportSettingTariff(DataTable ImportData, int Userid, string commodity,string Fromdate, string Todate)
        {
            string Message = ""; string ScanType = ""; string strcurrency = ""; int MaxDays = 0; int isTax = 0; int isSplit = 0;
            int Transid = 0;
            int Portname = 0;
            int Location = 0;
            int Jotype = 0;
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            DataTable dtDelete = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int k = 0; k < ImportData.Rows.Count; k++)
            {
                DataTable PortsDS = new DataTable();
                DataTable PortsDS1 = new DataTable();
                if (ImportData.Rows[k].Field<string>("AccountingID") == "30016")
                {
                    ScanType = "M";
                }
                if (ImportData.Rows[k].Field<string>("AccountingID") == "30017" || ImportData.Rows[k].Field<string>("AccountingID") == "30018")
                {
                    ScanType = "F";
                }
                if (ImportData.Rows[k].Field<string>("AccountingID") == "30171")
                {
                    ScanType = "D";
                }
                if (ImportData.Rows[k].Field<string>("Freedays") != "0" && ImportData.Rows[k].Field<string>("Freedays") != "")
                {
                    MaxDays = Convert.ToInt32(ImportData.Rows[k].Field<string>("Freedays"));
                }
                else
                {
                    MaxDays = 0;
                }
                if (ImportData.Rows[k].Field<string>("Tax") == "Yes")
                {
                    isTax = 1;
                }
                else
                {
                    isTax = 0;
                }
                if (ImportData.Rows[k].Field<string>("CurrencyType") == "Dollor")
                {
                    strcurrency = "D";
                }
                else
                {
                    strcurrency = "I";
                }
                isSplit = 0;

                string maxif = "";
                int maxif1 = 0;
                maxif = "select Transport_Type_ID from Transport_Type_M where Transport_Type ='" + ImportData.Rows[k].Field<string>("TransType") + "'";
                PortsDS1 = db.sub_GetDatatable(maxif);
                if (PortsDS1.Rows.Count > 0)
                {
                    maxif1 = PortsDS1.Rows[0].Field<int>("Transport_Type_ID");
                }


                string PortID = "";
                int POrtName = 0;
                PortID = "select PortID from Ports where PortName ='" + ImportData.Rows[k].Field<string>("Port") + "'";
                PortsDS1 = db.sub_GetDatatable(PortID);
                if (PortsDS1.Rows.Count > 0)
                {
                    POrtName = PortsDS1.Rows[0].Field<int>("PortID");
                }


                string Locationid = "";
                int GetLocation = 0;
                Locationid = "select locationid from ext_location_m where Location ='" + ImportData.Rows[k].Field<string>("Location") + "'";
                PortsDS1 = db.sub_GetDatatable(Locationid);
                if (PortsDS1.Rows.Count > 0)
                {
                    GetLocation = PortsDS1.Rows[0].Field<int>("locationid");
                }


                string sLocationid = "";
                int sGetLocation = 0;
                sLocationid = "select locationid from ext_location_m where Location ='" + ImportData.Rows[k].Field<string>("StuffLocation") + "'";
                PortsDS1 = db.sub_GetDatatable(sLocationid);
                if (PortsDS1.Rows.Count > 0)
                {
                    sGetLocation = PortsDS1.Rows[0].Field<int>("locationid");
                }


                string GetJoTYpe = "";
                int Typejo = 0;
                GetJoTYpe = "select Jo_Type_ID from Import_Jo_Type where Jo_Type ='" + ImportData.Rows[k].Field<string>("Jotype") + "'";
                PortsDS1 = db.sub_GetDatatable(GetJoTYpe);
                if (PortsDS1.Rows.Count > 0)
                {
                    Typejo = PortsDS1.Rows[0].Field<int>("Jo_Type_ID");
                }


                string strSQL1 = "";
                strSQL1 = "INSERT INTO Export_tariffdetails(tariffID,deliverytype,EffectiveFrom,EffectiveUpto,AccountID,ScanType,ContainerSize,ContainerType,SorF,SlabID,fixedAmt,insRate,RateperMT,MaxDays,TaxID,InvoiceType,IsSQM,isTotDays, Currency_type,TransType,PortName,FreeDay,CommodityID,LocationID,pickuplocation) VALUES";
                strSQL1 += " ('" + ImportData.Rows[k].Field<string>("TariffID") + "','" + ImportData.Rows[k].Field<string>("DeliveryType") + "','" + Convert.ToDateTime(Fromdate).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(Todate).ToString("yyyyMMdd") + "',";
                strSQL1 += " '" + ImportData.Rows[k].Field<string>("AccountingID") + "','" + ImportData.Rows[k].Field<string>("ScanType") + "','" + ImportData.Rows[k].Field<string>("Size") + "','" + ImportData.Rows[k].Field<string>("Type") + "','" + ImportData.Rows[k].Field<string>("Ftype") + "',";
                strSQL1 += " '" + ImportData.Rows[k].Field<string>("Slabid") + "','" + ImportData.Rows[k].Field<string>("FixedAmt") + "','" + ImportData.Rows[k].Field<string>("Insurance") + "','" + ImportData.Rows[k].Field<string>("rate") + "','" + MaxDays + "',";
                strSQL1 += " '" + ImportData.Rows[k].Field<string>("Tax") + "','" + ImportData.Rows[k].Field<string>("InvoiceType") + "','" + 0 + "','" + 0 + "', '" + strcurrency + "','" + maxif1 + "','" + POrtName + "','" + ImportData.Rows[k].Field<string>("Freedays") + "','" + commodity + "','" + GetLocation + "','" + sGetLocation + "')";
                PortsDS1 = db.sub_GetDatatable(strSQL1);



                Message = "Record saved successfully.";



            }
            string Srtdelete = "";
            Srtdelete = "delete from TempExport_tariffdetails where addedby ='" + Userid + "'  ";
            dtDelete = db.sub_GetDatatable(Srtdelete);
            return Message;
        }


        public List<BE.TariffAddDetailsEntites> GetexporttariffDetails(string TariffId, string deliveryType, string containerType)
        {
            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetExporttariffDetails(TariffId, deliveryType, containerType);
            foreach (DataRow row in dt.Rows)
            {

                BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                DPDTList.TariffID = Convert.ToString(row["Tariff ID"]);
                DPDTList.DeliveryType = Convert.ToString(row["Movement Type"]);
                DPDTList.From = Convert.ToString(row["Effective from"]);
                DPDTList.TO = Convert.ToString(row["Effective Upto"]);
                DPDTList.Accounting = Convert.ToString(row["Account Name"]);
                DPDTList.Size = Convert.ToString(row["Size"]);
                DPDTList.Type = Convert.ToString(row["Type"]);
                DPDTList.FixedAmt = Convert.ToString(row["Fixed Amt"]);
                DPDTList.Freedays = Convert.ToString(row["Free Days"]);

                DPDTList.Slabid = Convert.ToString(row["Slab ID"]);
                DPDTList.Tax = Convert.ToString(row["isSTax"]);
                DPDTList.username = Convert.ToString(row["Approved By"]);
                DPDTList.Approved = Convert.ToString(row["isapproved"]);
                DPDTList.Entryid = Convert.ToString(row["Entryid"]);








                Getdetails.Add(DPDTList);


            }

            return Getdetails;
        }


        public string ApproveDetailsExportTariff(DataTable ImportData, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int k = 0; k < ImportData.Rows.Count; k++)
            {

                DataTable PortsDS = new DataTable();


                string strSQL = "";
                strSQL = "UPDATE Export_tariffdetails SET IsApproved=1,ApprovedOn=GETDATE(),ApprovedBy='" + Userid + "' where entryid= '" + ImportData.Rows[k].Field<string>("Entryid") + "'";
                PortsDS = db.sub_GetDatatable(strSQL);
                Message = "Record Approved Successfully!";
            }

            return Message;
        }

        public string CancelDetailsExportTariff(DataTable ImportData, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int k = 0; k < ImportData.Rows.Count; k++)
            {

                DataTable PortsDS = new DataTable();
                string strSQL = "";
                strSQL = "UPDATE Export_tariffdetails SET IsCancel=1,CancelledOn=GETDATE(),CancelledBy='" + Userid + "' where entryid= '" + ImportData.Rows[k].Field<string>("Entryid") + "'";
                PortsDS = db.sub_GetDatatable(strSQL);
                Message = "Record Cancelled Successfully!";
            }

            return Message;
        }

        //code by prashant

       
        // Work For Proforma Details

        public BE.ExportProformaInvoice ProformaFillDropDownList()
        {
            BE.ExportProformaInvoice ProformaDDList = new BE.ExportProformaInvoice();
            List<BE.ExportProformaCusomter> CustomerList = new List<BE.ExportProformaCusomter>();
            List<BE.ExportProformaShipper> ShipperList = new List<BE.ExportProformaShipper>();
            List<BE.ExportProformaCha> ChaList = new List<BE.ExportProformaCha>();
            List<BE.ExportProformaTransType> TransTypeList = new List<BE.ExportProformaTransType>();
            List<BE.ExportProformaBillType> BillList = new List<BE.ExportProformaBillType>();
            List<BE.ExportProformaTariffmaster> TariffmasterList = new List<BE.ExportProformaTariffmaster>();
            List<BE.ExportProformaAccountmaster> AccountmasterList = new List<BE.ExportProformaAccountmaster>();
            List<BE.ExportProformaLocation> LocationList = new List<BE.ExportProformaLocation>();
            List<BE.ExportProformaAllotment> AllotmentList = new List<BE.ExportProformaAllotment>();
            List<BE.ExportProformaCommodity> CommodityList = new List<BE.ExportProformaCommodity>();
            List<BE.ExportProformaTax_Service> Tax_servicesList = new List<BE.ExportProformaTax_Service>();
            List<BE.ExportProformaInvoiceType> InvoiceType = new List<BE.ExportProformaInvoiceType>();



            DataSet ds = new DataSet();
            ds = trackerdatamanager.GetDropDownListExportProformaInvoice();

            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        BE.ExportProformaCusomter Delivery = new BE.ExportProformaCusomter();
                        Delivery.CustomerID = Convert.ToInt32(row["AGID"]);
                        Delivery.CustomerName = Convert.ToString(row["AGNAME"]);

                        CustomerList.Add(Delivery);
                    }
                }
            }
            if (ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        BE.ExportProformaShipper Invoice = new BE.ExportProformaShipper();
                        Invoice.shipperID = Convert.ToInt32(row["shipperID"]);
                        Invoice.shippername = Convert.ToString(row["shippername"]);

                        ShipperList.Add(Invoice);
                    }
                }
            }
            if (ds.Tables[2] != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        BE.ExportProformaCha Customer = new BE.ExportProformaCha();
                        Customer.ChaID = Convert.ToInt32(row["CHAID"]);
                        Customer.ChaName = Convert.ToString(row["CHANAME"]);

                        ChaList.Add(Customer);
                    }
                }
            }
            if (ds.Tables[3] != null)
            {
                if (ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[3].Rows)
                    {
                        BE.ExportProformaTransType ShippingLine = new BE.ExportProformaTransType();
                        ShippingLine.Transport_Type_ID = Convert.ToInt32(row["Transport_Type_ID"]);
                        ShippingLine.Transport_Type = Convert.ToString(row["Transport_Type"]);

                        TransTypeList.Add(ShippingLine);
                    }
                }
            }
            if (ds.Tables[4] != null)
            {
                if (ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[4].Rows)
                    {
                        BE.ExportProformaBillType CHA = new BE.ExportProformaBillType();
                        CHA.TypeID = Convert.ToInt32(row["TypeID"]);
                        CHA.BillType = Convert.ToString(row["BillType"]);

                        BillList.Add(CHA);
                    }
                }
            }
            if (ds.Tables[5] != null)
            {
                if (ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[5].Rows)
                    {
                        BE.ExportProformaTariffmaster Importer = new BE.ExportProformaTariffmaster();
                        Importer.TariffID = Convert.ToInt32(row["TariffID"]);
                        Importer.TariffDescription = Convert.ToString(row["TariffDescription"]);

                        TariffmasterList.Add(Importer);
                    }
                }
            }
            if (ds.Tables[6] != null)
            {
                if (ds.Tables[6].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[6].Rows)
                    {
                        BE.ExportProformaAccountmaster Movement = new BE.ExportProformaAccountmaster();
                        Movement.AccountID = Convert.ToInt32(row["AccountID"]);
                        Movement.AccountName = Convert.ToString(row["AccountName"]);

                        AccountmasterList.Add(Movement);
                    }
                }
            }
            if (ds.Tables[7] != null)
            {
                if (ds.Tables[7].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[7].Rows)
                    {
                        BE.ExportProformaLocation Commodity = new BE.ExportProformaLocation();
                        Commodity.LocationID = Convert.ToInt32(row["ID"]);
                        Commodity.LocationName = Convert.ToString(row["Name"]);

                        LocationList.Add(Commodity);
                    }
                }
            }
            if (ds.Tables[8] != null)
            {
                if (ds.Tables[8].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[8].Rows)
                    {
                        BE.ExportProformaAllotment Haulage = new BE.ExportProformaAllotment();
                        Haulage.ID = Convert.ToInt32(row["ID"]);
                        Haulage.Name = Convert.ToString(row["Name"]);

                        AllotmentList.Add(Haulage);
                    }
                }
            }
            if (ds.Tables[9] != null)
            {
                if (ds.Tables[9].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[9].Rows)
                    {
                        BE.ExportProformaCommodity YardLocation = new BE.ExportProformaCommodity();
                        YardLocation.Commodity_Group_ID = Convert.ToInt32(row["ID"]);
                        YardLocation.Commodity = Convert.ToString(row["Commodity"]);

                        CommodityList.Add(YardLocation);
                    }
                }
            }
            if (ds.Tables[10] != null)
            {
                if (ds.Tables[10].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[10].Rows)
                    {
                        BE.ExportProformaTax_Service TariffFor = new BE.ExportProformaTax_Service();
                        TariffFor.ID = Convert.ToInt32(row["ID"]);
                        TariffFor.Tax_Type_Desc = Convert.ToString(row["Tax_Type_Desc"]);

                        Tax_servicesList.Add(TariffFor);
                    }
                }
            }
            if (ds.Tables[11] != null)
            {
                if (ds.Tables[11].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[11].Rows)
                    {
                        BE.ExportProformaInvoiceType TariffForList = new BE.ExportProformaInvoiceType();
                        TariffForList.InvoiceTypeID = Convert.ToString(row["InvoiceTYpe"]);
                        TariffForList.InvoiceType = Convert.ToString(row["InvoiceTYpe"]);

                        InvoiceType.Add(TariffForList);
                    }
                }
            }

            ProformaDDList.ExportProformaCusomter = CustomerList;
            ProformaDDList.ExportProformaShipper = ShipperList;
            ProformaDDList.ExportProformaCha = ChaList;
            ProformaDDList.ExportProformaTransType = TransTypeList;
            ProformaDDList.ExportProformaBillType = BillList;
            ProformaDDList.ExportProformaTariffmaster = TariffmasterList;
            ProformaDDList.ExportProformaAccountmaster = AccountmasterList;
            ProformaDDList.ExportProformaLocation = LocationList;
            ProformaDDList.ExportProformaAllotment = AllotmentList;
            ProformaDDList.ExportProformaCommodity = CommodityList;
            ProformaDDList.ExportProformaTax_Service = Tax_servicesList;
            ProformaDDList.ExportProformaInvoiceType = InvoiceType;





            return ProformaDDList;
        }


        public List<BE.ContainerWiseProformaDetails> GetContainerWiseDetailForExportProforma(string Containerno,string MovementType)
        {
            List<BE.ContainerWiseProformaDetails> Getdetails = new List<BE.ContainerWiseProformaDetails>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetContainerWiseDetailForExportProforma(Containerno, MovementType);
            foreach (DataRow row in dt.Rows)
            {

                BE.ContainerWiseProformaDetails DPDTList = new BE.ContainerWiseProformaDetails();
                DPDTList.ContainerNo = Convert.ToString(row["Container No"]);
                DPDTList.Size = Convert.ToString(row["Size"]);
                DPDTList.ContainerType = Convert.ToString(row["Container Type"]);
                DPDTList.CargoType = Convert.ToString(row["Cargo Type"]);
                DPDTList.indate = Convert.ToString(row["In Date"]);
                DPDTList.StuffingDate = Convert.ToString(row["Stuffing Date"]);
                DPDTList.Gpdate = Convert.ToString(row["Gpdate"]);
                DPDTList.TotalDays = Convert.ToString(row["Total Days"]);
                DPDTList.LoadedDays = Convert.ToString(row["Loaded Days"]);

                DPDTList.EmptyDays = Convert.ToString(row["Empty Days"]);
                DPDTList.PortName = Convert.ToString(row["Port Name"]);
                DPDTList.PickUP = Convert.ToString(row["Pick UP"]);
                DPDTList.Transport_Type = Convert.ToString(row["Transport_Type"]);
                DPDTList.NetWeight = Convert.ToString(row["Net Weight"]);
                DPDTList.TareWeight = Convert.ToString(row["Tare Weight"]);
                DPDTList.GrossWeight = Convert.ToString(row["Gross Weight"]);
                DPDTList.FactoryInDate = Convert.ToString(row["Factory In Date"]);
                DPDTList.FactoryOutDate = Convert.ToString(row["Factory Out Date"]);
                DPDTList.DwellHours = Convert.ToString(row["Dwell Hours"]);
                DPDTList.DwellDays = Convert.ToString(row["Dwell Days"]);
                DPDTList.CHAID = Convert.ToString(row["CHAID"]);
                DPDTList.CHAName = Convert.ToString(row["CHAName"]);
                DPDTList.shipperID = Convert.ToString(row["shipperID"]);
                DPDTList.shippername = Convert.ToString(row["shippername"]);
                DPDTList.AGID = Convert.ToString(row["AGID"]);
                DPDTList.AGName = Convert.ToString(row["AGName"]);
                DPDTList.entryID = Convert.ToString(row["CtrentryID"]);
                DPDTList.MovementType = Convert.ToString(row["Movement Type"]);
                DPDTList.SBNumber = Convert.ToString(row["SB Number"]);
                DPDTList.LineID = Convert.ToString(row["LineID"]);
                DPDTList.SLName = Convert.ToString(row["SLName"]);
                DPDTList.Port = Convert.ToString(row["Port"]);
                DPDTList.PickupLocID = Convert.ToString(row["PickupLocID"]);
                DPDTList.StuffLocID = Convert.ToString(row["StuffLocID"]);
                DPDTList.NoOfStuffLoc = Convert.ToString(row["No Of Stuff Loc"]);
                DPDTList.CargoDesc = Convert.ToString(row["cargodesc"]);
                DPDTList.commodityid = Convert.ToString(row["commodityid"]);
                Getdetails.Add(DPDTList);
            }

            return Getdetails;
        }

        public List<BE.ShippingBIllDetailsForExportProforma> GetContainerWiseDetailForExportProformaShippingbill(string Containerno)
        {
            List<BE.ShippingBIllDetailsForExportProforma> Getdetails = new List<BE.ShippingBIllDetailsForExportProforma>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetShippingBillDetailsContainerWise(Containerno);
            foreach (DataRow row in dt.Rows)
            {

                BE.ShippingBIllDetailsForExportProforma DPDTList = new BE.ShippingBIllDetailsForExportProforma();
                DPDTList.SBNumber = Convert.ToString(row["SB Number"]);
                DPDTList.SBDate = Convert.ToString(row["SB Date"]);
                DPDTList.CartingDate = Convert.ToString(row["Carting Date"]);
                DPDTList.StuffingDate = Convert.ToString(row["Stuffing Date"]);
                DPDTList.TotalDays = Convert.ToString(row["Total Days"]);
                DPDTList.CartingQty = Convert.ToString(row["Carting Qty"]);
                DPDTList.CartingWeight = Convert.ToString(row["Carting Weight"]);
                DPDTList.Area = Convert.ToString(row["Area"]);
                DPDTList.Space = Convert.ToString(row["Space"]);

                DPDTList.CargoDescriptions = Convert.ToString(row["Cargo Descriptions"]);
                DPDTList.VehicleNo = Convert.ToString(row["Vehicle No"]);
                DPDTList.entryid = Convert.ToString(row["entryid"]);
                DPDTList.CargoWeight = Convert.ToString(row["CargoWeight"]);
                DPDTList.TotalPKGS = Convert.ToString(row["TotalPKGS"]);

                Getdetails.Add(DPDTList);
            }

            return Getdetails;
        }

        public List<BE.ContainerWiseProformaDetails> GetsbWiseDetailForExportProforma(string Containerno)
        {
            List<BE.ContainerWiseProformaDetails> Getdetails = new List<BE.ContainerWiseProformaDetails>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetSBWiseDetailForExportProforma(Containerno);
            foreach (DataRow row in dt.Rows)
            {

                BE.ContainerWiseProformaDetails DPDTList = new BE.ContainerWiseProformaDetails();
                DPDTList.ContainerNo = Convert.ToString(row["Container No"]);
                DPDTList.Size = Convert.ToString(row["Size"]);
                DPDTList.ContainerType = Convert.ToString(row["Container Type"]);
                DPDTList.CargoType = Convert.ToString(row["Cargo Type"]);
                DPDTList.indate = Convert.ToString(row["In Date"]);
                DPDTList.StuffingDate = Convert.ToString(row["Stuffing Date"]);
                DPDTList.Gpdate = Convert.ToString(row["Gpdate"]);

                DPDTList.LoadedDays = Convert.ToString(row["Loaded Days"]);

                DPDTList.EmptyDays = Convert.ToString(row["Empty Days"]);
                DPDTList.PortName = Convert.ToString(row["Port Name"]);
                DPDTList.PickUP = Convert.ToString(row["Pick UP"]);
                DPDTList.Transport_Type = Convert.ToString(row["Transport_Type"]);
                DPDTList.NetWeight = Convert.ToString(row["Net Weight"]);
                DPDTList.TareWeight = Convert.ToString(row["Tare Weight"]);
                DPDTList.GrossWeight = Convert.ToString(row["Gross Weight"]);

                DPDTList.CHAID = Convert.ToString(row["CHAID"]);
                DPDTList.CHAName = Convert.ToString(row["CHAName"]);
                DPDTList.shipperID = Convert.ToString(row["shipperID"]);
                DPDTList.shippername = Convert.ToString(row["shippername"]);
                DPDTList.AGID = Convert.ToString(row["AGID"]);
                DPDTList.AGName = Convert.ToString(row["AGName"]);
                DPDTList.entryID = Convert.ToString(row["CtrentryID"]);
                DPDTList.MovementType = Convert.ToString(row["Movement Type"]);
                DPDTList.SBNumber = Convert.ToString(row["SB Number"]);
                DPDTList.LineID = Convert.ToString(row["LineID"]);
                DPDTList.SLName = Convert.ToString(row["SLName"]);
                DPDTList.Port = Convert.ToString(row["Port"]);
                DPDTList.PickupLocID = Convert.ToString(row["PickupLocID"]);
                DPDTList.StuffLocID = Convert.ToString(row["StuffLocID"]);
                DPDTList.NoOfStuffLoc = Convert.ToString(row["No Of Stuff Loc"]);

                Getdetails.Add(DPDTList);
            }

            return Getdetails;
        }

        public List<BE.ShippingBIllDetailsForExportProforma> GetsbWiseDetailForExportProformaShippingbill(string Containerno)
        {
            List<BE.ShippingBIllDetailsForExportProforma> Getdetails = new List<BE.ShippingBIllDetailsForExportProforma>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetShippingBillDetailsSBWise(Containerno);
            foreach (DataRow row in dt.Rows)
            {

                BE.ShippingBIllDetailsForExportProforma DPDTList = new BE.ShippingBIllDetailsForExportProforma();
                DPDTList.SBNumber = Convert.ToString(row["SB Number"]);
                DPDTList.SBDate = Convert.ToString(row["SB Date"]);
                DPDTList.CartingDate = Convert.ToString(row["Carting Date"]);
                DPDTList.StuffingDate = Convert.ToString(row["Stuffing Date"]);
                DPDTList.TotalDays = Convert.ToString(row["Total Days"]);
                DPDTList.CartingQty = Convert.ToString(row["Carting Qty"]);
                DPDTList.CartingWeight = Convert.ToString(row["Carting Weight"]);
                DPDTList.Area = Convert.ToString(row["Area"]);
                DPDTList.Space = Convert.ToString(row["Space"]);

                DPDTList.CargoDescriptions = Convert.ToString(row["Cargo Descriptions"]);
                DPDTList.VehicleNo = Convert.ToString(row["Vehicle No"]);
                DPDTList.entryid = Convert.ToString(row["entryid"]);
                DPDTList.CargoWeight = Convert.ToString(row["CargoWeight"]);
                DPDTList.TotalPKGS = Convert.ToString(row["TotalPKGS"]);

                Getdetails.Add(DPDTList);
            }

            return Getdetails;
        }



        //Bl Wise DEtails

        public List<BE.ContainerWiseProformaDetails> GetBLWiseDetailForExportProforma(string BLNumber)
        {
            List<BE.ContainerWiseProformaDetails> Getdetails = new List<BE.ContainerWiseProformaDetails>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetBLExportProforma(BLNumber);
            foreach (DataRow row in dt.Rows)
            {

                BE.ContainerWiseProformaDetails DPDTList = new BE.ContainerWiseProformaDetails();
                DPDTList.ContainerNo = Convert.ToString(row["Container No"]);
                DPDTList.Size = Convert.ToString(row["Size"]);
                DPDTList.ContainerType = Convert.ToString(row["Container Type"]);
                DPDTList.CargoType = Convert.ToString(row["Cargo Type"]);
                DPDTList.indate = Convert.ToString(row["In Date"]);
                DPDTList.StuffingDate = Convert.ToString(row["Stuffing Date"]);
                DPDTList.Gpdate = Convert.ToString(row["Gpdate"]);

                DPDTList.LoadedDays = Convert.ToString(row["Loaded Days"]);

                DPDTList.EmptyDays = Convert.ToString(row["Empty Days"]);
                DPDTList.PortName = Convert.ToString(row["Port Name"]);
                DPDTList.PickUP = Convert.ToString(row["Pick UP"]);
                DPDTList.Transport_Type = Convert.ToString(row["Transport_Type"]);
                DPDTList.NetWeight = Convert.ToString(row["Net Weight"]);
                DPDTList.TareWeight = Convert.ToString(row["Tare Weight"]);
                DPDTList.GrossWeight = Convert.ToString(row["Gross Weight"]);

                DPDTList.CHAID = Convert.ToString(row["CHAID"]);
                DPDTList.CHAName = Convert.ToString(row["CHAName"]);
                DPDTList.shipperID = Convert.ToString(row["shipperID"]);
                DPDTList.shippername = Convert.ToString(row["shippername"]);
                DPDTList.AGID = Convert.ToString(row["AGID"]);
                DPDTList.AGName = Convert.ToString(row["AGName"]);
                DPDTList.entryID = Convert.ToString(row["CtrentryID"]);
                DPDTList.MovementType = Convert.ToString(row["Movement Type"]);
                DPDTList.SBNumber = Convert.ToString(row["SB Number"]);
                DPDTList.LineID = Convert.ToString(row["LineID"]);
                DPDTList.SLName = Convert.ToString(row["SLName"]);
                DPDTList.Port = Convert.ToString(row["Port"]);
                DPDTList.PickupLocID = Convert.ToString(row["PickupLocID"]);
                DPDTList.StuffLocID = Convert.ToString(row["StuffLocID"]);
                DPDTList.NoOfStuffLoc = Convert.ToString(row["No Of Stuff Loc"]);

                Getdetails.Add(DPDTList);
            }

            return Getdetails;
        }

        public List<BE.ShippingBIllDetailsForExportProforma> GeTBLWiseDetailForExportProformaShippingbill(string BLNumber)
        {
            List<BE.ShippingBIllDetailsForExportProforma> Getdetails = new List<BE.ShippingBIllDetailsForExportProforma>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetBLDetailsSBWise(BLNumber);
            foreach (DataRow row in dt.Rows)
            {

                BE.ShippingBIllDetailsForExportProforma DPDTList = new BE.ShippingBIllDetailsForExportProforma();
                DPDTList.SBNumber = Convert.ToString(row["SB Number"]);
                DPDTList.SBDate = Convert.ToString(row["SB Date"]);
                DPDTList.CartingDate = Convert.ToString(row["Carting Date"]);
                DPDTList.StuffingDate = Convert.ToString(row["Stuffing Date"]);
                DPDTList.TotalDays = Convert.ToString(row["Total Days"]);
                DPDTList.CartingQty = Convert.ToString(row["Carting Qty"]);
                DPDTList.CartingWeight = Convert.ToString(row["Carting Weight"]);
                DPDTList.Area = Convert.ToString(row["Area"]);
                DPDTList.Space = Convert.ToString(row["Space"]);

                DPDTList.CargoDescriptions = Convert.ToString(row["Cargo Descriptions"]);
                DPDTList.VehicleNo = Convert.ToString(row["Vehicle No"]);
                DPDTList.entryid = Convert.ToString(row["entryid"]);
                DPDTList.CargoWeight = Convert.ToString(row["CargoWeight"]);
                DPDTList.TotalPKGS = Convert.ToString(row["TotalPKGS"]);

                Getdetails.Add(DPDTList);
            }

            return Getdetails;
        }


        public string CancelExportInvoicePorforma(string AssessNo, string workyear, int userId)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = trackerdatamanager.CancelExportInvoiceProforma(AssessNo, workyear, userId);

            message = "Records Cancel Successfully!";
            return message;
        }


        public string SubmitExportDetailsforporforma(string AssessNo, string workyear,  string assesstype)
        {

            string message = "";
            string message1 = "";
            DataTable updateDl = new DataTable();
            DataTable updateD2 = new DataTable();


            updateD2 = trackerdatamanager.gettheExportInvoicedocseries2(AssessNo, workyear, assesstype);
            if (updateD2.Rows.Count > 0)
            {
                message1 = Convert.ToString(updateD2.Rows[0][0]);
            }


            return message1;
        }

        public int getExportallowid(string AssessNo, string workyear, int userId)
        {

            int message = 0;
            DataTable updateDl = new DataTable();
            updateDl = trackerdatamanager.GetExportMaxallowid(AssessNo, workyear, userId);
            message = Convert.ToInt32(updateDl.Rows[0][0]);

            return message;
        }


        public string SubmitExportFinalDetails(string AssessNo, string workyear, int userId, string strinvoiceNo, int allowind)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = trackerdatamanager.SubmitFinalExportInvoiceproforma(AssessNo, workyear, userId, strinvoiceNo, allowind);

            message = "Final invoice is generated successfully!";
            return message;
        }


        public string CancelExportAssessmentDetails(string remarks, string AssessNo, string workyear, int userId)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = trackerdatamanager.CancelExportAssessmentDetails(remarks, AssessNo, workyear, userId);

            message = "Records Cancel Successfully!";
            return message;
        }

        //Transporter

        public List<BE.ExportGatePassEntities> Transport()
        {
            List<BE.ExportGatePassEntities> locationDL = new List<BE.ExportGatePassEntities>();
            DataTable dt = new DataTable();
            string Table = "Transporter";
            string Column = "TransID, TransName";
            string Condition = "";
            string OrderBy = "";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ExportGatePassEntities locationList = new BE.ExportGatePassEntities();
                    locationList.TransporterID = Convert.ToInt32(row["TransID"]);
                    locationList.TrasporterName = Convert.ToString(row["TransName"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }


        public string ExportStuffingTallySave(DataTable dataTable, long UserID,string txtStuffingRequestID,string txtStuffingDate, string txtPOD,string hdnddlVendorID)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dtsb = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt3 = new DataTable();

            int IFCLID = 0;
            string strQuerymax = "";
            
            strQuerymax = "select ISNULL(max(stuffingNo),0)+1 as entryid from exp_stuffing  ";
            dtget = db.sub_GetDatatable(strQuerymax);
            if (dtget.Rows.Count > 0)
            {
                IFCLID = Convert.ToInt16(dtget.Rows[0]["entryid"]);
            }
            else
            {
                IFCLID = 1;
            }
            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                string strSQL = "";
                string lblfob = "";
                string lblchaid = "";
                string lblagencyid = "";
                string lblshipperid = "";
                string lblmovtype = "";
                string lblcontainer = "";
                string lblsize = "";
                string lblcontainertype = "";
                string lblsbno = "";
                string lblinvoice = "";
                string txtvia = "";
                string lblcargotype = "";
                string lblequipmentid = "";
                string lblunno = "";
                string lblclass = "";
                string lbltem = "";
                strSQL = "";
                strSQL = "SELECT * FROM exp_shippingbill WHERE entryid=" + dataTable.Rows[k].Field<string>("SBEntryID")  + "";
                dt = db.sub_GetDatatable(strSQL);
                if (dt.Rows.Count > 0)
                {
                    lblfob = Convert.ToString(dt.Rows[0]["FOBValue"]);  
                    lblchaid = Convert.ToString(dt.Rows[0]["chaid"]);  
                    lblagencyid = Convert.ToString(dt.Rows[0]["agid"]);  
                    lblshipperid = Convert.ToString(dt.Rows[0]["shipperid"]);
                    strSQL = "";
                    strSQL = "SELECT stuffingtype FROM StuffingType WHERE TypeID=" + Convert.ToString(dt.Rows[0]["stuffingtypeid"]);  
                    dtsb = db.sub_GetDatatable(strSQL);
                    if (dtsb.Rows.Count > 0)
                    {
                        lblmovtype = Convert.ToString(dtsb.Rows[0]["stuffingtype"]);
                    }
                }

                strSQL = "";
                strSQL = "SELECT SBNo, InvoiceNo, ViaNo, POD, ISOCodeID, CargoType, EquipmentID FROM exp_stuffingrequest WHERE SBentryID=" + dataTable.Rows[k].Field<string>("SBentryID")  + " AND Stuffrequestid=" + txtStuffingRequestID + "";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    lblsbno = Convert.ToString(dt1.Rows[0]["SBNo"]);
                    lblinvoice = Convert.ToString(dt1.Rows[0]["InvoiceNo"]); 
                    txtvia = Convert.ToString(dt1.Rows[0]["ViaNo"]);
                    
                        lblcargotype = Convert.ToString(dt1.Rows[0]["CargoType"]);
                     

                    
                        lblequipmentid = Convert.ToString(dt1.Rows[0]["EquipmentID"]);
                     
                }

                strSQL = "";
                strSQL = "SELECT ContainerNo, size, containertypeid, ISOCodeID  FROM Exp_In WHERE EntryID=" + dataTable.Rows[k].Field<string>("EntryID");
                dt3 = db.sub_GetDatatable(strSQL);
                if (dt3.Rows.Count > 0)
                {
                    lblcontainer=Convert.ToString(dt.Rows[0]["ContainerNo"]); 
                    lblsize = Convert.ToString(dt.Rows[0]["size"]); 
                    // objRSSave.Fields("processtype") = "E"

                    strSQL = "";
                    strSQL = "SELECT Containertype FROM Containertype WHERE containertypeid=" + Convert.ToString(dt3.Rows[0]["containertypeid"]);
                    dt1 = db.sub_GetDatatable(strSQL);
                    if (dt1.Rows.Count > 0)
                    {
                        lblcontainertype = Convert.ToString(dt1.Rows[0]["Containertype"]);
                    }

                   
                    var dt4 = new DataTable();
                    strSQL = "";
                    strSQL = "SELECT UNNo, temperature, ClassNO FROM exp_stuffingrequest WHERE StuffRequestID=" + txtStuffingRequestID + "";
                    dt4 = db.sub_GetDatatable(strSQL);
                    if (dt4.Rows.Count > 0)
                    {
                        lblunno = Convert.ToString(dt4.Rows[0]["UNNo"]);
                        lblclass = Convert.ToString(dt4.Rows[0]["ClassNO"]); 
                        lbltem = Convert.ToString(dt4.Rows[0]["temperature"]); 
                    }
                }


                strSQL = "Exec USP_INSERT_EXP_STUFFING '" + IFCLID + "','" + txtStuffingDate + "','" + txtStuffingRequestID + "','" + lblagencyid + "','" + "0" + "','" + lblmovtype + "','" + dataTable.Rows[k].Field<string>("SBEntryID") + "',";
                strSQL += " '" + lblsbno + "','" + lblinvoice + "','" + dataTable.Rows[k].Field<string>("EntryID") + "','" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + lblsize + "',";
                strSQL += "'" + dataTable.Rows[k].Field<string>("StuffedQty") + "','" + dataTable.Rows[k].Field<string>("QtyUnit") + "','" + dataTable.Rows[k].Field<string>("StuffedWeight") + "','" + dataTable.Rows[k].Field<string>("StuffedWeight") + "','" + lblcontainertype + "',";
                strSQL += "'" + txtvia + "','" + txtPOD + "','"+ 0 +"','" + dataTable.Rows[k].Field<string>("ISOCode") + "','" + "0" + "','" + dataTable.Rows[k].Field<string>("AgentSeal") + "',";
                strSQL += "'" + dataTable.Rows[k].Field<string>("CustomSeal") + "','" + lblunno + "',' " + lbltem + "',' " + dataTable.Rows[k].Field<string>("TareWeight") + "', '" + lblfob + "','" + UserID + "','" + "0" + "','" + "0" + "','" + lblchaid + "','" + lblshipperid +   "','" + lblequipmentid + "','" + lblclass + "','" + dataTable.Rows[k].Field<string>("BLQty") + "','" + dataTable.Rows[k].Field<string>("BLWeight") + "'";
                dt = db.sub_GetDatatable(strSQL);




                string Messageget = "Record Saved Successfully";
                Message = Messageget;

            }


            return Message;
        }

        public string SaveLoadedGatepassdetails(DataTable dataTable, long UserID, string TxtTranstype)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            int IFCLID = 0;
            string strQuerymax = "";
            string txtbarcode = "";
            string StrGPCode = "";
            strQuerymax = "SELECT ISNULL(MAX(GPNo),0) + 1 as GPNo  FROM exp_gatepass  ";
            dtget = db.sub_GetDatatable(strQuerymax);
            if (dtget.Rows.Count > 0)
            {
                IFCLID = Convert.ToInt16(dtget.Rows[0]["GPNo"]);
            }
            else
            {
                IFCLID = 1;
            }
            for (int k = 0; k < dataTable.Rows.Count; k++)
            {


                txtbarcode = "E" + IFCLID;
                StrGPCode = "EXPL-" + IFCLID;
                string strSQL = "";


                strSQL = "Exec USP_SAVE_EXP_GATE_PAS_Web '" + IFCLID + "','" + dataTable.Rows[k].Field<string>("AGID") + "','" + dataTable.Rows[k].Field<string>("entryID") + "','" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + 'F' + "','" + dataTable.Rows[k].Field<string>("Size") + "',";
                strSQL += " '" + dataTable.Rows[k].Field<string>("NetWeight") + "','" + dataTable.Rows[k].Field<string>("GrossWeight") + "','" + dataTable.Rows[k].Field<string>("AgentSeal") + "','" + dataTable.Rows[k].Field<string>("CustomSeal") + "','" + TxtTranstype + "',";
                strSQL += "'" + dataTable.Rows[k].Field<string>("VehicleID") + "','" + dataTable.Rows[k].Field<string>("Vehicle") + "','" + dataTable.Rows[k].Field<string>("ViaNo") + "','" + dataTable.Rows[k].Field<string>("Remark") + "','" + UserID + "',";
                strSQL += "'" + dataTable.Rows[k].Field<string>("TransporterID") + "','" + dataTable.Rows[k].Field<string>("VesselName") + "','" + StrGPCode + "','" + dataTable.Rows[k].Field<string>("Train") + "','" + dataTable.Rows[k].Field<string>("Wagon") + "'";

                dt = db.sub_GetDatatable(strSQL);




                string Messageget = "Record Saved Successfully";
                Message = Messageget;

            }


            return Message;
        }


        public List<BE.TariffAddDetailsEntites> SaveExportCargoDetails(DataTable Invoicedata, int Userid, string tariffid, DataTable DeliveryType, string Accounting, string AccountingID,string Location, string StuffLocation)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            string strSQL1 = "";
            string strSQL = "";
            string strSQL4 = "";
            int dblSlabID = 0;
            string SlIDMSize = "";
            string SlIDMType = "";
            string SlIDMBaseON = "";

            int dblSlabID2 = 0;
            string dbltype = "";
            string blnaddtariff = "True";

            string dblsize = "";
            DataTable dt = new DataTable();
            strSQL1 = "SELECT isnull(MAX(SlabID),0)+1 as [Slab ID] FROM exp_slabs";
            dt = db.sub_GetDatatable(strSQL1);

            if (dt.Rows.Count > 0)
            {
                dblSlabID = Convert.ToInt32(dt.Rows[0]["Slab ID"]);
            }

            for (int k = 0; k < Invoicedata.Rows.Count; k++)
            {


                DataTable PortsDS = new DataTable();
                DataTable PortsDS1 = new DataTable();
                DataTable PortsDS3 = new DataTable();

                if (dblsize != Invoicedata.Rows[k].Field<string>("SlabSize") || dbltype != Invoicedata.Rows[k].Field<string>("SlabCargoType"))
                {
                    DataTable dt4 = new DataTable();
                    strSQL4 = "SELECT isnull(MAX(SlabID),0)+1 as [Slab ID] FROM exp_slabs";
                    dt4 = db.sub_GetDatatable(strSQL4);
                    if (dt4.Rows.Count > 0)
                    {

                        dblSlabID = Convert.ToInt32(dt4.Rows[0]["Slab ID"]);



                        for (int i = 0; i < DeliveryType.Rows.Count; i++)
                        {

                            if (Accounting != "")
                            {

                                BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                                DPDTList.TariffID = Convert.ToString(tariffid);
                                DPDTList.DeliveryType = Convert.ToString(DeliveryType.Rows[i].Field<string>("DeliveryType"));
                                DPDTList.From = Convert.ToString("");
                                DPDTList.TO = Convert.ToString("");
                                DPDTList.Accounting = Convert.ToString(Accounting);
                                DPDTList.Size = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabSize"));
                                DPDTList.Type = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabCargoType"));



                                string strSQL5 = "";
                                strSQL5 = " USP_Show_ChargeBasedON '" + Invoicedata.Rows[k].Field<string>("SlabOn") + "'";
                                PortsDS3 = db.sub_GetDatatable(strSQL5);
                                if (PortsDS3.Rows.Count > 0)
                                {
                                    DPDTList.Ftype = Convert.ToString(PortsDS3.Rows[0]["sorf"]);
                                }
                                else
                                {
                                    DPDTList.Ftype = Convert.ToString(0);
                                }

                                DPDTList.Slabid = Convert.ToString(dblSlabID);

                                DPDTList.FixedAmt = Convert.ToString(0);


                                DPDTList.Insurance = Convert.ToString(0);


                                DPDTList.rate = Convert.ToString(0);



                                DPDTList.Days = Convert.ToString(0);

                                DPDTList.Tax = Convert.ToString("");
                                DPDTList.InvoiceType = Convert.ToString(0);
                                DPDTList.CurrencyType = Convert.ToString('I');
                                DPDTList.TransType = Convert.ToString("");
                                DPDTList.Port = Convert.ToString("");
                                DPDTList.Freedays = Convert.ToString("");
                                DPDTList.Location = Convert.ToString(Location);
                                DPDTList.StuffLocation = Convert.ToString(StuffLocation);
                                DPDTList.Jotype = Convert.ToString("");
                                DPDTList.ScanType = Convert.ToString("");
                                DPDTList.AccountingID = Convert.ToString(AccountingID);

                                Getdetails.Add(DPDTList);

                            }
                        }

                    }


                }
                if (k > 0)
                {
                    dblsize = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabSize"));
                    dbltype = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabCargoType"));
                }
                else
                {
                    dblsize = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabSize"));
                    dbltype = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabCargoType"));
                }

                if (Invoicedata.Rows[k].Field<string>("SlabOn") != "")
                {
                    string strSQL3 = "";
                    strSQL3 = "INSERT INTO exp_slabs(SlabID,SlabOn,fromslab,toslab,Value,Size,CargoType) values (" + dblSlabID + ",'" + Invoicedata.Rows[k].Field<string>("SlabOn") + "','" + Invoicedata.Rows[k].Field<string>("RangeFrom") + "','" + Invoicedata.Rows[k].Field<string>("RangeTo") + "','" + Invoicedata.Rows[k].Field<string>("Value") + "','" + Invoicedata.Rows[k].Field<string>("SlabSize") + "','" + Invoicedata.Rows[k].Field<string>("SlabCargoType") + "')";
                    dt = db.sub_GetDatatable(strSQL3);
                }
            }

            return Getdetails;

        }

        public List<BE.TariffAddDetailsEntites> Export1SavedataForGetdata(DataTable ImportData, int Userid)
        {


            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();



            for (int k = 0; k < ImportData.Rows.Count; k++)
            {





               
                    string strSQL2 = "";
                    strSQL2 = "INSERT INTO TempExport_tariffdetails(TARIFFID, DELIVERYTYPE, EFFECTIVEFROM, EFFECTIVEUPTO, ACCOUNTNAME, CONTAINERSIZE, CONTAINERTYPE, SORF, SLABID, INSRATE, FIXEDAMT, RATEPERMT, TAXID, INVOICETYPE, CURRENCY_TYPE, TRANSTYPE, PORTNAME, FREEDAY, LOCATIONID, Stufflocation,JOTYPE, SCANTYPE, ACCOUNTID, ADDEDBY) VALUES";
                    strSQL2 += " ('" + ImportData.Rows[k].Field<string>("TariffID") + "','" + ImportData.Rows[k].Field<string>("DeliveryType11") + "','" + ImportData.Rows[k].Field<string>("From") + "','" + ImportData.Rows[k].Field<string>("TO") + "',";
                    strSQL2 += " '" + ImportData.Rows[k].Field<string>("AccountingID") + "','" + ImportData.Rows[k].Field<string>("Size") + "','" + ImportData.Rows[k].Field<string>("Type1") + "','" + ImportData.Rows[k].Field<string>("Ftype") + "','" + ImportData.Rows[k].Field<string>("Slabid") + "','" + ImportData.Rows[k].Field<string>("Insurance") + "','" + ImportData.Rows[k].Field<string>("FixedAmt") + "',";
                    strSQL2 += " '" + ImportData.Rows[k].Field<string>("rate") + "','" + ImportData.Rows[k].Field<string>("Tax") + "','" + ImportData.Rows[k].Field<string>("InvoiceType") + "','" + ImportData.Rows[k].Field<string>("CurrencyType") + "','" + ImportData.Rows[k].Field<string>("TransType") + "','" + ImportData.Rows[k].Field<string>("Port") + "','" + ImportData.Rows[k].Field<string>("Freedays") + "',";
                    strSQL2 += " '" + ImportData.Rows[k].Field<string>("Location") + "','" + ImportData.Rows[k].Field<string>("StuffLocation") + "','" + ImportData.Rows[k].Field<string>("Jotype") + "','" + ImportData.Rows[k].Field<string>("ScanType") + "','" + ImportData.Rows[k].Field<string>("AccountingID") + "','" + Userid + "')";
                    dt = db.sub_GetDatatable(strSQL2);
                


            }







            DataTable getdt = new DataTable();

            getdt = trackerdatamanager.GetExportAddedTariffsettingDetails(Userid);
            foreach (DataRow row in getdt.Rows)
            {

                BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                DPDTList.TariffID = Convert.ToString(row["tariffID"]);
                DPDTList.DeliveryType = Convert.ToString(row["deliverytype"]);
                DPDTList.From = Convert.ToString(row["effectivefrom"]);
                DPDTList.TO = Convert.ToString(row["effectiveupto"]);
                DPDTList.Accounting = Convert.ToString(row["accountName"]);
                DPDTList.Size = Convert.ToString(row["containersize"]);
                DPDTList.Type = Convert.ToString(row["containertype"]);
                DPDTList.Ftype = Convert.ToString(row["SorF"]);
                DPDTList.Slabid = Convert.ToString(row["slabID"]);
                DPDTList.FixedAmt = Convert.ToString(row["fixedamt"]);
                DPDTList.Insurance = Convert.ToString(row["insrate"]);
                DPDTList.rate = Convert.ToString(row["ratepermt"]);
                DPDTList.Days = Convert.ToString(row["FreeDay"]);

                DPDTList.Tax = Convert.ToString(row["TaxID"]);
                DPDTList.InvoiceType = Convert.ToString(row["InvoiceType"]);
                DPDTList.CurrencyType = Convert.ToString(row["Currency_type"]);
                DPDTList.TransType = Convert.ToString(row["TransType"]);
                DPDTList.Port = Convert.ToString(row["PortName"]);
                DPDTList.Freedays = Convert.ToString(row["FreeDay"]);
                DPDTList.Location = Convert.ToString(row["LocationID"]);
                DPDTList.Jotype = Convert.ToString(row["JoType"]);
                DPDTList.ScanType = Convert.ToString(row["ScanType"]);
                DPDTList.AccountingID = Convert.ToString(row["accountID"]);
                DPDTList.Entryid = Convert.ToString(row["Entryid"]);
                DPDTList.StuffLocation = Convert.ToString(row["Stufflocation"]);
                Getdetails.Add(DPDTList);


            }

            return Getdetails;
        }
        public List<BE.ImportProformaSearchGST> GetExportGSTList(string SearchText)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetGSTSearchExportProforma(SearchText);
            List<BE.ImportProformaSearchGST> isCHACode = new List<BE.ImportProformaSearchGST>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    BE.ImportProformaSearchGST oper = new BE.ImportProformaSearchGST();
                    oper.GSTIn_uniqID = Convert.ToString(row["GSTIn_uniqID"]);
                    oper.GSTName = Convert.ToString(row["GSTName"]);
                    oper.State = Convert.ToString(row["State"]);
                    oper.GSTAddress = Convert.ToString(row["GSTAddress"]);
                    oper.GSTID = Convert.ToString(row["GSTID"]);
                    oper.state_Code = Convert.ToString(row["state_Code"]);

                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }

        public List<BE.ImportInvoiceContainerDetails> GetExportAdditionalcharges(string SearchOn, string number)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExportadditionialChargesdetails(SearchOn, number);
            List<BE.ImportInvoiceContainerDetails> isCHACode = new List<BE.ImportInvoiceContainerDetails>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    BE.ImportInvoiceContainerDetails oper = new BE.ImportInvoiceContainerDetails();

                    oper.JONo = Convert.ToString(row["Entry ID"]);
                    oper.ContainerNo = Convert.ToString(row["Container No"]);
                    oper.Size = Convert.ToString(row["Size"]);
                    oper.Cargotype = Convert.ToString(row["Type"]);
                    oper.Amount = Convert.ToString(row["Amount"]);

                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }

        public string ExportSaveadditionalcharges(DataTable Invoicedata, string WorkorderNo, string Workorderyear,
          string accountheadID, string additionnarritioin, string Number, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("WorkorderNo", WorkorderNo);
            parameterList.Add("Workorderyear", Workorderyear);
            parameterList.Add("accountheadID", accountheadID);
            parameterList.Add("additionnarritioin", additionnarritioin);
            parameterList.Add("Number", Number);
            parameterList.Add("Userid", Userid);
            int i = db.AddTypeTableData("SP_SaveExportAccountCWEB", parameterList, Invoicedata, "PT_Addadditionaldetails", "@PT_Addadditionaldetails");


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

           public List<BE.Allocatedspace> GetAllocatedSpace()
        {
            List<BE.Allocatedspace> locationDL = new List<BE.Allocatedspace>();
            DataTable dt = new DataTable();
           

            dt = trackerdatamanager.GetAllocatedSpace();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.Allocatedspace locationList = new BE.Allocatedspace();
                    locationList.AllocatedID = Convert.ToString(row["ID"]);
                    locationList.AllocatedName = Convert.ToString(row["Name"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public string UpdateCargoDetailsForExport(DataTable Invoicedata, string SBNO)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("SBNO", SBNO);
            int i = db.AddTypeTableData("USP_CargoUpdateSBDetails", parameterList, Invoicedata, "Export_cargo_type_update", "@Export_cargo_type_update");
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
 
        public List<BE.Surveyor> GetSurveyor()
        {
            List<BE.Surveyor> CustomerList = new List<BE.Surveyor>();
            DataTable dt = new DataTable();
            string Table = "Surveyor_Master";
            string Column = "SurveyorId,SurveyorName";
            string Condition = "";
            string OrderBy = "SurveyorName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.Surveyor Customers = new BE.Surveyor();
                    Customers.SurveyorId = Convert.ToInt32(row["SurveyorId"]);
                    Customers.SurveyorName = Convert.ToString(row["SurveyorName"]);
                    CustomerList.Add(Customers);
                }
            }
            return CustomerList;
        }
        public List<BE.VendorMaster> Getvendor()
        {
            List<BE.VendorMaster> EquipmentDL = new List<BE.VendorMaster>();
            DataTable dt = new DataTable();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            dt = db.sub_GetDatatable("Usp_VendorName ");
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.VendorMaster EquipmentList = new BE.VendorMaster();
                    EquipmentList.VendorId = Convert.ToInt32(row["vendorid"]);
                    EquipmentList.Name = Convert.ToString(row["VendorName"]);
                    EquipmentDL.Add(EquipmentList);
                }
            }
            return EquipmentDL;
        }



        public List<BE.Customer> GetCustomer()
        {
            List<BE.Customer> CustomerList = new List<BE.Customer>();
            DataTable dt = new DataTable();
            string Table = "customer";
            string Column = "Agid,Agname";
            string Condition = "";
            string OrderBy = "Agname";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.Customer Customers = new BE.Customer();
                    Customers.AGID = Convert.ToInt32(row["Agid"]);
                    Customers.AGName = Convert.ToString(row["Agname"]);
                    CustomerList.Add(Customers);
                }
            }
            return CustomerList;
        }
        public List<BE.CHA> Getcha()
        {
            List<BE.CHA> CustomerList = new List<BE.CHA>();
            DataTable dt = new DataTable();
            string Table = "Cha";
            string Column = "Chaid,Chaname";
            string Condition = "";
            string OrderBy = "Chaname";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.CHA Customers = new BE.CHA();
                    Customers.CHANo = Convert.ToInt32(row["Chaid"]);
                    Customers.CHAName = Convert.ToString(row["Chaname"]);
                    CustomerList.Add(Customers);
                }
            }
            return CustomerList;
        }
        public List<BE.Category> getWorkordercategory(string WorkType)
        {
            List<BE.Category> List = new List<BE.Category>();
            DataTable result = new DataTable();
            result = trackerdatamanager.GetWorkordercategory(WorkType);
            if (result != null)
            {
                foreach (DataRow row in result.Rows)
                {
                    BE.Category item = new BE.Category();

                    item.CategoryName = Convert.ToString(row["Wo_Type"]);
                    List.Add(item);
                }
            }
            return List;
        }
        public BE.FCLDestuffDetailsEntites AjxImportWorkDetails(string ContainerNo, string IGMNo, string ItemNo, string Category, string SearchOn)
        {

            BE.FCLDestuffDetailsEntites FCLDetails = new BE.FCLDestuffDetailsEntites();
            List<BE.FCLsummaryDetails> FCLdetailssumary = new List<BE.FCLsummaryDetails>();
            DataSet DT = new DataSet();
            DT = trackerdatamanager.AjxImportWorkDetails(ContainerNo, IGMNo, ItemNo, Category, SearchOn);

            if (DT.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in DT.Tables[0].Rows)
                {

                    FCLDetails.Consignee = Convert.ToString(row["Consignee"]);
                    FCLDetails.IGM_GoodsDesc = Convert.ToString(row["IGM_GoodsDesc"]);
                    FCLDetails.IGM_BLNo = Convert.ToString(row["IGM_BLNo"]);
                    FCLDetails.IGM_PackType = Convert.ToString(row["IGM_PackType"]);
                    FCLDetails.IGM_GrossVol = Convert.ToString(row["IGM_GrossVol"]);
                    FCLDetails.IGM_GrossWt = Convert.ToString(row["IGM_GrossWt"]);
                    FCLDetails.IGM_WtUnit = Convert.ToString(row["IGM_WtUnit"]);
                    FCLDetails.CHAID = Convert.ToString(row["CHAID"]);
                    FCLDetails.CHAName = Convert.ToString(row["CHAName"]);
                    FCLDetails.NocNo = Convert.ToString(row["NOC_No"]);


                }
            }
            if (DT.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow row in DT.Tables[1].Rows)
                {
                    BE.FCLsummaryDetails FCLlist1 = new BE.FCLsummaryDetails();
                    FCLlist1.JONo = Convert.ToString(row["JO No"]);
                    FCLlist1.Containerno = Convert.ToString(row["Container No"]);
                    FCLlist1.Size = Convert.ToString(row["Size"]);
                    FCLlist1.IGMQty = Convert.ToString(row["IGM Qty"]);
                    FCLlist1.Weight = Convert.ToString(row["Weight"]);
                    FCLlist1.Unit = Convert.ToString(row["Unit"]);
                    FCLlist1.DestuffQty = Convert.ToString(row["Destuff Qty"]);
                    FCLlist1.ShortQty = Convert.ToString(row["Short Qty"]);
                    FCLlist1.ExcessQty = Convert.ToString(row["Excess Qty"]);
                    FCLlist1.TypeFL = Convert.ToString(row["Type(F/L)"]);
                    FCLlist1.Remarks = Convert.ToString(row["Remarks"]);
                    FCLlist1.EntryID = Convert.ToString(row["EntryID"]);
                    FCLlist1.Equipment = Convert.ToString(row["Equipment Type"]);
                    FCLlist1.Examine = Convert.ToString(row["Examine%"]);
                    FCLlist1.DeliveryType = Convert.ToString(row["Delivery Type"]);
                    FCLlist1.Hours = Convert.ToString(row["Hours"]);
                    FCLlist1.CGM = Convert.ToString(row["CGM"]);
                    FCLlist1.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    FCLlist1.MaterialQty = Convert.ToString(row["Material Qty"]);
                    FCLlist1.MaterialDescriptions = Convert.ToString(row["Material Descriptions"]);
                    FCLdetailssumary.Add(FCLlist1);

                }
            }




            FCLDetails.FCLsummaryDetails = FCLdetailssumary;
            return FCLDetails;

        }
        public List<BE.FCLsummaryDetails> SaveTempWorkOrder(DataTable Containerdetails, string Containerno1, int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            List<BE.FCLsummaryDetails> obj = new List<BE.FCLsummaryDetails>();



            parameterList.Add("Containerno1", Containerno1);
            parameterList.Add("ADDEDBY", UserID);
            DataTable dt = new DataTable();


            dt = db.DataTableAddTypeTable("USP_INSERT_IMPORT_WORK_ORDER_TEMP_Close", parameterList, Containerdetails, "PT_WorkOrder_Temp", "@PT_WorkOrder_Temp");
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.FCLsummaryDetails FCLlist1 = new BE.FCLsummaryDetails();
                    FCLlist1.JONo = Convert.ToString(row["JoNo"]);
                    FCLlist1.Containerno = Convert.ToString(row["ContainerNo"]);
                    FCLlist1.Size = Convert.ToString(row["Size"]);
                    FCLlist1.IGMQty = Convert.ToString(row["IGMQty"]);
                    FCLlist1.Weight = Convert.ToString(row["Weight"]);
                    FCLlist1.Unit = Convert.ToString(row["Unit"]);
                    FCLlist1.DestuffQty = Convert.ToString(row["DestuffQty"]);
                    FCLlist1.ShortQty = Convert.ToString(row["ShortQty"]);

                    FCLlist1.Remarks = Convert.ToString(row["Remarks"]);
                    FCLlist1.EntryID = Convert.ToString(row["EntryID"]);
                    FCLlist1.Equipment = Convert.ToString(row["EquipmentID"]);
                    FCLlist1.Examine = Convert.ToString(row["Examine"]);
                    FCLlist1.DeliveryType = Convert.ToString(row["DeliveryType"]);
                    FCLlist1.Hours = Convert.ToString(row["Hours"]);
                    FCLlist1.CGM = Convert.ToString(row["CGM"]);
                    FCLlist1.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    FCLlist1.CGM = Convert.ToString(row["CGM"]);
                    FCLlist1.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    FCLlist1.MaterialQty = Convert.ToString(row["MaterialQty"]);
                    FCLlist1.MaterialDescriptions = Convert.ToString(row["MaterialDescriptions"]);
                    obj.Add(FCLlist1);

                }
            }

            return obj;
        }
        public string WorkOrder(DataTable Containerdetails, List<BE.WorkOrderReport> containerdetails, string DestuffDate, string Category, string WOType, string CHAID, string Vendorname, int UserID, string Surveyor, string NocNo,string DoValidate)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            string WoNo = "";
            string workyear = "";
            DataTable dt = new DataTable();
            parameterList.Add("WO_DATE", Convert.ToDateTime(DestuffDate).ToString("yyyy-MM-dd HH:mm"));
            parameterList.Add("Category", Category);
            parameterList.Add("WO_TYPE", WOType);
            parameterList.Add("CHAID", CHAID);
            parameterList.Add("Vendorname", Vendorname);
            parameterList.Add("ADDEDBY", UserID);
            parameterList.Add("Surveyor", Surveyor);
            parameterList.Add("NocNo", NocNo);
            parameterList.Add("DoValidate", DoValidate);

            dt = db.DataTableAddTypeTable("USP_INSERT_IMPORT_WORK_ORDER_Loded", parameterList, Containerdetails, "PT_WorkOrder", "@PT_WorkOrder");


          
            if (dt.Rows.Count > 0)
            {
                WoNo= Convert.ToString(dt.Rows[0].Field<string>("Wono"));
                workyear = Convert.ToString(dt.Rows[0].Field<string>("workyear"));
                Message = "1" +','+ WoNo+','+ workyear;
            }
            else
            {
                Message = "0";
            }
            return Message;
        }
        public List<BE.FCLDestuffDetailsEntites> GetEditDetails(string WONo)
        {

            List<BE.FCLDestuffDetailsEntites> HoldList = new List<BE.FCLDestuffDetailsEntites>();
            DataTable Dl = new DataTable();

            Dl = trackerdatamanager.GetEditDetails(WONo);

            if (Dl != null)
            {
                foreach (DataRow row in Dl.Rows)
                {
                    BE.FCLDestuffDetailsEntites DlList = new BE.FCLDestuffDetailsEntites();
                    DlList.Consignee = Convert.ToString(row["Consignee"]);
                    DlList.IGM_GoodsDesc = Convert.ToString(row["IGM_GoodsDesc"]);
                    DlList.IGM_BLNo = Convert.ToString(row["IGM_BLNo"]);
                    DlList.IGM_PackType = Convert.ToString(row["IGM_PackType"]);
                    DlList.IGM_GrossVol = Convert.ToString(row["IGM_GrossVol"]);
                    DlList.IGM_GrossWt = Convert.ToString(row["IGM_GrossWt"]);
                    DlList.IGM_WtUnit = Convert.ToString(row["IGM_WtUnit"]);
                    DlList.CHAID = Convert.ToString(row["CHAID"]);
                    DlList.CHAName = Convert.ToString(row["CHAName"]);
                    DlList.Wo_Type = Convert.ToString(row["Wo_Type"]);
                    DlList.VendorId = Convert.ToInt32(row["Vendor"]);
                    DlList.Activity = Convert.ToString(row["Activity"]);

                    HoldList.Add(DlList);
                }
            }
            return HoldList;
        }
        public List<BE.FCLsummaryDetails> GetEditDetailsgird(string WONo)
        {

            List<BE.FCLDestuffDetailsEntites> HoldList = new List<BE.FCLDestuffDetailsEntites>();
            List<BE.FCLsummaryDetails> FCLdetailssumary = new List<BE.FCLsummaryDetails>();
            DataTable Dl = new DataTable();

            Dl = trackerdatamanager.GetEditDetailsgrid(WONo);

            if (Dl != null)
            {
                foreach (DataRow row in Dl.Rows)
                {
                    BE.FCLsummaryDetails FCLlist1 = new BE.FCLsummaryDetails();
                    FCLlist1.JONo = Convert.ToString(row["JO No"]);
                    FCLlist1.Containerno = Convert.ToString(row["Container No"]);
                    FCLlist1.Size = Convert.ToString(row["Size"]);
                    FCLlist1.IGMQty = Convert.ToString(row["IGM Qty"]);
                    FCLlist1.Weight = Convert.ToString(row["Weight"]);
                    FCLlist1.Unit = Convert.ToString(row["Unit"]);
                    FCLlist1.DestuffQty = Convert.ToString(row["Destuff Qty"]);
                    FCLlist1.ShortQty = Convert.ToString(row["Short Qty"]);
                    FCLlist1.ExcessQty = Convert.ToString(row["Excess Qty"]);
                    FCLlist1.TypeFL = Convert.ToString(row["Type(F/L)"]);
                    FCLlist1.Remarks = Convert.ToString(row["Remarks"]);
                    FCLlist1.EntryID = Convert.ToString(row["EntryID"]);
                    FCLlist1.Equipment = Convert.ToString(row["Equipment Type"]);
                    FCLlist1.Examine = Convert.ToString(row["Examine%"]);
                    FCLlist1.DeliveryType = Convert.ToString(row["Delivery Type"]);
                    FCLlist1.Hours = Convert.ToString(row["Hours"]);
                    FCLlist1.CGM = Convert.ToString(row["CGM"]);
                    FCLlist1.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    FCLlist1.WeightS = Convert.ToString(row["Weights"]);
                    FCLlist1.MaterialQty = Convert.ToString(row["MaterialQty"]);
                    FCLlist1.MaterialDescriptions = Convert.ToString(row["MaterialDescriptions"]);
                    FCLdetailssumary.Add(FCLlist1);
                }
            }
            return FCLdetailssumary;
        }
        public string WorkOrderUpdate(DataTable Containerdetails, List<BE.WorkOrderReport> containerdetails, string DestuffDate, string Category, string WOType, string CHAID, string Vendorname, string WoNo, int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("WO_DATE", Convert.ToDateTime(DestuffDate).ToString("yyyy-MM-dd HH:mm"));
            parameterList.Add("Category", Category);
            parameterList.Add("WO_TYPE", WOType);
            parameterList.Add("CHAID", CHAID);
            parameterList.Add("Vendorname", Vendorname);
            parameterList.Add("ADDEDBY", UserID);
            parameterList.Add("WoNo", WoNo);


            int i = db.AddTypeTableData("USP_UPdate_IMPORT_WORK_ORDER", parameterList, Containerdetails, "PT_WorkOrder_Close", "@PT_WorkOrder_Close");

            if (i > 0)
            {

                string Messageget = "Record Saved Successfully";
                Message = Messageget + ',' + WoNo;
            }
            else
            {
                Message = "Record not saved successfully. Try Again!";
            }
            return Message;
        }
    }
}
