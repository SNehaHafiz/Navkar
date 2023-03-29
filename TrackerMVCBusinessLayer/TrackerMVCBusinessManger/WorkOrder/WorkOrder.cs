using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.WorkOrder
{
    public class WorkOrder
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public EN.WorkOrderEntities GetDropDownListWorkOrder()
        {
            EN.WorkOrderEntities WorkOrder = new EN.WorkOrderEntities();
            DataSet WorkOrderDL = new DataSet();

            WorkOrderDL = TrackerManager.getDropDownListExportWorkOrder();

            List<EN.WOTypeEntities> WOTypeList = new List<EN.WOTypeEntities>();
            List<EN.WarehouseEntities> WHList = new List<EN.WarehouseEntities>();
            List<EN.VendorWOEntities> VendorWOList = new List<EN.VendorWOEntities>();
            List<EN.EquipmentWOEntities> EquipmentWOList = new List<EN.EquipmentWOEntities>();
            List<EN.PackageWOEntities> PackageWOList = new List<EN.PackageWOEntities>();
            List<EN.ExportAccountMaster> ExportAccountMasterList = new List<EN.ExportAccountMaster>();
            List<EN.VendorMaster> VendorMasterList = new List<EN.VendorMaster>();

            if (WorkOrderDL.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[0].Rows)
                {
                    EN.WOTypeEntities WOType = new EN.WOTypeEntities();
                    WOType.Wo_Type = Convert.ToString(row["Wo_Type"]);
                    WOTypeList.Add(WOType);
                }

            }
            if (WorkOrderDL.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[1].Rows)
                {
                    EN.WarehouseEntities WH = new EN.WarehouseEntities();
                    WH.WHID = Convert.ToInt32(row["WHID"]);
                    WH.WHName = Convert.ToString(row["WHName"]);
                    WHList.Add(WH);
                }
            }
            if (WorkOrderDL.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[2].Rows)
                {
                    EN.VendorWOEntities Vendor = new EN.VendorWOEntities();
                    Vendor.VendorId = Convert.ToInt32(row["VendorId"]);
                    Vendor.Name = Convert.ToString(row["Name"]);
                    VendorWOList.Add(Vendor);
                }

            }
            if (WorkOrderDL.Tables[3].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[3].Rows)
                {
                    EN.EquipmentWOEntities EQ = new EN.EquipmentWOEntities();
                    EQ.Id = Convert.ToInt32(row["Id"]);
                    EQ.Equipment = Convert.ToString(row["Equipment"]);
                    EquipmentWOList.Add(EQ);
                }

            }
            if (WorkOrderDL.Tables[4].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[4].Rows)
                {
                    EN.PackageWOEntities PKG = new EN.PackageWOEntities();
                    PKG.CodeID = Convert.ToInt32(row["CodeID"]);
                    PKG.Package = Convert.ToString(row["Package"]);
                    PackageWOList.Add(PKG);
                }

            }

            if (WorkOrderDL.Tables[5].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[5].Rows)
                {
                    EN.ExportAccountMaster export = new EN.ExportAccountMaster();
                    export.AccountID = Convert.ToInt32(row["AccountID"]);
                    export.AccountName = Convert.ToString(row["AccountName"]);
                    ExportAccountMasterList.Add(export);
                }

            }

            if (WorkOrderDL.Tables[6].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[6].Rows)
                {
                    EN.VendorMaster export = new EN.VendorMaster();
                    export.VendorId = Convert.ToInt32(row["VendorId"]);
                    export.Name = Convert.ToString(row["Name"]);
                    VendorMasterList.Add(export);
                }

            }

            WorkOrder.WOTypeList = WOTypeList;
            WorkOrder.WHList = WHList;
            WorkOrder.VendorWOList = VendorWOList;
            WorkOrder.EQWOList = EquipmentWOList;
            WorkOrder.PKGList = PackageWOList;
            WorkOrder.ExportAccountMasterList = ExportAccountMasterList;
            WorkOrder.VendorMasterList = VendorMasterList;

            return WorkOrder;
        }

        public EN.WorkOrderEntities GetDropDownListWorkOrderTariff()
        {
            EN.WorkOrderEntities WorkOrder = new EN.WorkOrderEntities();
            DataSet WorkOrderDL = new DataSet();

            WorkOrderDL = TrackerManager.GetActivityDropDownTariffList();

            List<EN.ContainerSize> sizesList = new List<EN.ContainerSize>();
            List<EN.ContainerType> TypeList = new List<EN.ContainerType>();
            List<EN.CargoType> CargoList = new List<EN.CargoType>();
            List<EN.CommodityGroup> CommodityList = new List<EN.CommodityGroup>();
            List<EN.PackageWOEntities> PackageWOList = new List<EN.PackageWOEntities>();


            if (WorkOrderDL.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[0].Rows)
                {
                    EN.ContainerSize sizes = new EN.ContainerSize();
                    sizes.ContainerSizeID = Convert.ToInt32(row["ContainerSizeID"]);
                    sizes.ContainerSizeName = Convert.ToString(row["ContainerSize"]);
                    sizesList.Add(sizes);
                }

            }
            if (WorkOrderDL.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[1].Rows)
                {
                    EN.ContainerType WH = new EN.ContainerType();
                    WH.ContainerTypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    WH.ContainerTypeName = Convert.ToString(row["ContainerType"]);
                    TypeList.Add(WH);
                }
            }
            if (WorkOrderDL.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[2].Rows)
                {
                    EN.CargoType Vendor = new EN.CargoType();
                    Vendor.Cargotypeid = Convert.ToInt32(row["Cargotypeid"]);
                    Vendor.Cargotype = Convert.ToString(row["Cargotype"]);
                    CargoList.Add(Vendor);
                }

            }
            if (WorkOrderDL.Tables[3].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[3].Rows)
                {
                    EN.CommodityGroup EQ = new EN.CommodityGroup();
                    EQ.Commodity_Group_ID = Convert.ToInt32(row["Commodity_Group_ID"]);
                    EQ.Commodity_Group_Name = Convert.ToString(row["Commodity_Group_Name"]);
                    CommodityList.Add(EQ);
                }

            }
            if (WorkOrderDL.Tables[4].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[4].Rows)
                {
                    EN.PackageWOEntities package  = new EN.PackageWOEntities();
                    package.CodeID = Convert.ToInt32(row["CodeID"]);
                    package.Package = Convert.ToString(row["Package"]);
                    PackageWOList.Add(package);
                }

            }

           
            WorkOrder.containerSizesList = sizesList;
            WorkOrder.containerTypesList = TypeList;
            WorkOrder.cargoTypesList = CargoList;
            WorkOrder.commodityGroupsList = CommodityList;
            WorkOrder.PKGList = PackageWOList;

            return WorkOrder;
        }

        public EN.WorkOrderEntities GetDropDownListImportWorkOrder()
        {
            EN.WorkOrderEntities WorkOrder = new EN.WorkOrderEntities();
            DataSet WorkOrderDL = new DataSet();

            WorkOrderDL = TrackerManager.getDropDownListImportWorkOrderssr();

            List<EN.WOTypeEntities> WOTypeList = new List<EN.WOTypeEntities>();
            List<EN.WarehouseEntities> WHList = new List<EN.WarehouseEntities>();
            List<EN.VendorWOEntities> VendorWOList = new List<EN.VendorWOEntities>();
            List<EN.EquipmentWOEntities> EquipmentWOList = new List<EN.EquipmentWOEntities>();
            List<EN.ImportAccountMaster> ImportAccountMasterList = new List<EN.ImportAccountMaster>();
            List<EN.PackageWOEntities> PackageWOList = new List<EN.PackageWOEntities>();
            List<EN.CHA> CHAList = new List<EN.CHA>();

            if (WorkOrderDL.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[0].Rows)
                {
                    EN.WOTypeEntities WOType = new EN.WOTypeEntities();
                    WOType.Wo_Type = Convert.ToString(row["Wo_Type"]);
                    WOTypeList.Add(WOType);
                }

            }
            if (WorkOrderDL.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[1].Rows)
                {
                    EN.WarehouseEntities WH = new EN.WarehouseEntities();
                    WH.WHID = Convert.ToInt32(row["WHID"]);
                    WH.WHName = Convert.ToString(row["WHName"]);
                    WHList.Add(WH);
                }
            }
            if (WorkOrderDL.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[2].Rows)
                {
                    EN.VendorWOEntities Vendor = new EN.VendorWOEntities();
                    Vendor.VendorId = Convert.ToInt32(row["VendorId"]);
                    Vendor.Name = Convert.ToString(row["Name"]);
                    VendorWOList.Add(Vendor);
                }

            }
            if (WorkOrderDL.Tables[3].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[3].Rows)
                {
                    EN.EquipmentWOEntities EQ = new EN.EquipmentWOEntities();
                    EQ.Id = Convert.ToInt32(row["Id"]);
                    EQ.Equipment = Convert.ToString(row["Equipment"]);
                    EquipmentWOList.Add(EQ);
                }

            }
            if (WorkOrderDL.Tables[4].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[4].Rows)
                {
                    EN.PackageWOEntities PKG = new EN.PackageWOEntities();
                    PKG.CodeID = Convert.ToInt32(row["CodeID"]);
                    PKG.Package = Convert.ToString(row["Package"]);
                    PackageWOList.Add(PKG);
                }

            }

            if (WorkOrderDL.Tables[5].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[5].Rows)
                {
                    EN.ImportAccountMaster export = new EN.ImportAccountMaster();
                    export.AccountID = Convert.ToInt32(row["AccountID"]);
                    export.AccountName = Convert.ToString(row["AccountName"]);
                    ImportAccountMasterList.Add(export);
                }

            }

            if (WorkOrderDL.Tables[6].Rows.Count != 0)
            {
                foreach (DataRow row in WorkOrderDL.Tables[6].Rows)
                {
                    EN.CHA export = new EN.CHA();
                    export.CHANo = Convert.ToInt32(row["CHAID"]);
                    export.CHAName = Convert.ToString(row["CHAName"]);
                    CHAList.Add(export);
                }

            }

            WorkOrder.WOTypeList = WOTypeList;
            WorkOrder.WHList = WHList;
            WorkOrder.VendorWOList = VendorWOList;
            WorkOrder.EQWOList = EquipmentWOList;
            WorkOrder.PKGList = PackageWOList;
            WorkOrder.ImportAccountMasterList = ImportAccountMasterList;
            WorkOrder.CHAList = CHAList;

            return WorkOrder;
        }
        public List<EN.WorkOrderEntities> GetSBDets(string SBNO, string WorkType)
        {
            List<EN.WorkOrderEntities> WOList = new List<EN.WorkOrderEntities>();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetSBData(SBNO, WorkType);

            if (WODT != null)
            {
                foreach (DataRow row in WODT.Rows)
                {
                    EN.WorkOrderEntities WO = new EN.WorkOrderEntities();
                    WO.CHAName = Convert.ToString(row["CHAName"]);
                    WO.ShipperName = Convert.ToString(row["shippername"]);
                    WO.OnAccount = Convert.ToString(row["AGName"]);
                    WO.ManifestQty = Convert.ToString(row["TotalPKGS"]);
                    WO.CargoDescription = Convert.ToString(row["cargodesc"]);
                    WO.SBEntryID = Convert.ToInt32(row["SBEntryID"]);
                    WO.CHAID = Convert.ToInt32(row["CHAID"]);
                    WO.OnAccountID = Convert.ToInt32(row["AGID"]);
                    WO.Unit = Convert.ToInt32(row["PKGType"]);

                    WOList.Add(WO);
                }
            }
            return WOList;
        }

        public List<EN.WorkOrderEntities> GetSBDetsByInvoiceNo(string InvoiceNo, string WorkType)
        {
            List<EN.WorkOrderEntities> WOList = new List<EN.WorkOrderEntities>();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetSBDataByInvoice(InvoiceNo, WorkType);

            if (WODT != null)
            {
                foreach (DataRow row in WODT.Rows)
                {
                    EN.WorkOrderEntities WO = new EN.WorkOrderEntities();
                    WO.CHAName = Convert.ToString(row["CHAName"]);
                    WO.ShipperName = Convert.ToString(row["shippername"]);
                    WO.OnAccount = Convert.ToString(row["AGName"]);
                    WO.ManifestQty = Convert.ToString(row["TotalPKGS"]);
                    WO.CargoDescription = Convert.ToString(row["cargodesc"]);
                    WO.SBEntryID = Convert.ToInt32(row["SBEntryID"]);
                    WO.CHAID = Convert.ToInt32(row["CHAID"]);
                    WO.OnAccountID = Convert.ToInt32(row["AGID"]);
                    WO.Unit = Convert.ToInt32(row["PKGType"]);

                    WOList.Add(WO);
                }
            }
            return WOList;
        }

        public EN.WorkOrderEntities GetCartWeightPkgs(string SBNO, string WorkType)
        {
            EN.WorkOrderEntities WO1 = new EN.WorkOrderEntities();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetCartWeightData(SBNO, WorkType);

            if (WODT != null)
            {
                foreach (DataRow row in WODT.Rows)
                {
                    WO1.TotPkgs = Convert.ToDecimal(row["PKGSDestuff"]);
                    WO1.TotWeight = Convert.ToDecimal(row["Weight"]);
                    WO1.UnitWt = Convert.ToDecimal(row["UNITWT"]);
                }
            }
            return WO1;
        }

        public EN.WorkOrderEntities GetCartWeightPkgsInvoice(string InvoiceNo, string WorkType)
        {
            EN.WorkOrderEntities WO1 = new EN.WorkOrderEntities();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetCartWeightDataInvoice(InvoiceNo, WorkType);

            if (WODT != null)
            {
                foreach (DataRow row in WODT.Rows)
                {
                    WO1.TotPkgs = Convert.ToDecimal(row["PKGSDestuff"]);
                    WO1.TotWeight = Convert.ToDecimal(row["Weight"]);
                    WO1.UnitWt = Convert.ToDecimal(row["UNITWT"]);
                }
            }
            return WO1;
        }

        public List<EN.VehicleWOEntities> GetVehicleNos(string SBEntryID, string SBNO)
        {
            List<EN.VehicleWOEntities> VehicleList = new List<EN.VehicleWOEntities>();
            DataTable VehicleDT = new DataTable();
            VehicleDT = TrackerManager.GetVehicleData(SBEntryID, SBNO);

            if (VehicleDT != null)
            {
                foreach (DataRow row in VehicleDT.Rows)
                {
                    EN.VehicleWOEntities VEH = new EN.VehicleWOEntities();
                    VEH.VehicleNo = Convert.ToString(row["TruckNO"]);
                    VEH.CartingId = Convert.ToString(row["CartingID"]);

                    VehicleList.Add(VEH);
                }
            }
            return VehicleList;
        }
        
        public List<EN.WorkOrderEntities> GetPkgsVehicleWise(string SBEntryID, string SBNO, string VehicleNo,string CartingId)
        {
            List<EN.WorkOrderEntities> PKGList = new List<EN.WorkOrderEntities>();
            DataTable PKGDT = new DataTable();
            PKGDT = TrackerManager.GetPkgData(SBEntryID, SBNO, VehicleNo, CartingId);

            if (PKGDT != null)
            {
                foreach (DataRow row in PKGDT.Rows)
                {
                    EN.WorkOrderEntities PKG = new EN.WorkOrderEntities();
                    PKG.TotPkgs = Convert.ToDecimal(row["GateInQty"]);
                    PKG.TotWeight = Convert.ToDecimal(row["GateInWt"]);
                    PKG.UnitWt = Convert.ToDecimal(row["UnitWT"]);
                    PKG.PrevPkgs = Convert.ToDecimal(row["PrevPkgs"]);
                    PKGList.Add(PKG);
                }
            }
            return PKGList;
        }
        public string SaveWorkOrder(DataTable WOdata, string WODate, string WOType, string SBNo, int SBEntryID, int CHAID, int OnAccountID, int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("WO_DATE", Convert.ToDateTime(WODate).ToString("yyyy-MM-dd HH:mm"));
            parameterList.Add("WO_TYPE", WOType);
            parameterList.Add("ADDEDBY", UserID);
            parameterList.Add("AGENTID", OnAccountID);
            parameterList.Add("CHAID", CHAID);
            parameterList.Add("SBNO", SBNo);
            parameterList.Add("SBENTRYID", SBEntryID);
            
            int i = db.AddTypeTableData("USP_INSERT_INTO_EXPORT_WORK_ORDER", parameterList, WOdata, "PT_ExportWorkOrder", "@PT_ExportWorkOrder");

            if (i > 0)
            {
                Message = "1";
            }
            else
            {
                Message = "0";
            }
            return Message;
        }

        public string SaveWorkOrderTariff(DataTable WOdata, string Category, string RangeFrom, string RangeTo, int UserID,string TariffId)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Category", Category);
            parameterList.Add("RangeFrom", RangeFrom);
            parameterList.Add("RangeTo", RangeTo);
            parameterList.Add("ADDEDBY", UserID);
            parameterList.Add("TariffId", TariffId);

            Message = db.AddTypeTableDataTariff("USP_INSERT_INTO_EXPORT_WORK_ORDER", parameterList, WOdata, "PT_WorkOrderTariff", "@PT_WorkOrderTariff");

            
            return Message;
        }

        public string SaveSpecialWorkOrder(DataTable WOdata, string JoType, string SSRType, string IsServiceTax, int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("SSRType", SSRType);
            parameterList.Add("IsStax", IsServiceTax);
            parameterList.Add("JoType", JoType);
            parameterList.Add("UserId", UserID);

            int i = db.AddTypeTableData("INS_ExportSpecialWorkOrder", parameterList, WOdata, "PT_ExportSpecialWorkOrder", "@PT_ExportSpecialWorkOrder");

            if (i > 0)
            {
                Message = "1";
            }
            else
            {
                Message = "0";
            }
            return Message;
        }
        public string SaveWorkOrderImport(DataTable WOdata, string WODate, string WOType, int UserID,int CHAID,int AgentId,string Category,
            string TruckNo, string CargoDescription)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("WO_DATE", Convert.ToDateTime(WODate).ToString("yyyy-MM-dd HH:mm"));
            parameterList.Add("WO_TYPE", WOType);
            parameterList.Add("ADDEDBY", UserID);
            parameterList.Add("CHAId", CHAID);
            parameterList.Add("AgentId", AgentId);
            parameterList.Add("Category", Category);
            parameterList.Add("VehicleNo", TruckNo);
            parameterList.Add("CargoDescription", CargoDescription);
            int i = db.AddTypeTableData("USP_INSERT_INTO_IMPORT_WORK_ORDER", parameterList, WOdata, "PT_ImportWorkOrder", "@PT_ImportWorkOrder");

            if (i > 0)
            {
                Message = "1";
            }
            else
            {
                Message = "0";
            }
            return Message;
        }

        public string SaveSSRImport(DataTable dataTable,string SSRType, string Equipment, string Vendor, string UserID,string CHA, string AccountID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("SSRType", SSRType);
            parameterList.Add("Equipment", Equipment);
            parameterList.Add("Vendor", Vendor);
            parameterList.Add("UserId", UserID);
            parameterList.Add("CHAId", CHA);
            parameterList.Add("AccountID", AccountID);
            int i = db.AddTypeTableData("USP_InsImportSSR", parameterList, dataTable, "ImportSSRDT", "@ImportSSRDT");

            if (i > 0)
            {
                Message = "1";
            }
            else
            {
                Message = "0";
            }
            return Message;
        }

        public List<EN.OpenWOListEntities> GetOpenWOList()
        {
            List<EN.OpenWOListEntities> OpenWOList = new List<EN.OpenWOListEntities>();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetOpenWOList();

            if (WODT != null)
            {
                foreach (DataRow row in WODT.Rows)
                {
                    EN.OpenWOListEntities WO = new EN.OpenWOListEntities();
                    WO.SrNo = Convert.ToString(row["SRNO"]);
                    WO.WONo = Convert.ToString(row["WO_NO"]);
                    WO.WODate = Convert.ToString(row["WO_Date"]);
                    WO.WOType = Convert.ToString(row["Wo_Type"]);
                    WO.OnAccount = Convert.ToString(row["AGName"]);
                    WO.CHA = Convert.ToString(row["CHAName"]);
                    WO.SBNo = Convert.ToString(row["SbNo"]);
                    WO.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    WO.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    WO.Warehouse = Convert.ToString(row["LocationName"]);
                    WO.Pkgs = Convert.ToString(row["PKGSDestuff"]);
                    WO.Vendor = Convert.ToString(row["Name"]);
                    WO.SequenceNo = Convert.ToString(row["SequenceNo"]);
                    OpenWOList.Add(WO);
                }
            }
            return OpenWOList;
        }

        public List<EN.OpenWOListEntities> GetOpenWOListImport()
        {
            List<EN.OpenWOListEntities> OpenWOList = new List<EN.OpenWOListEntities>();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetOpenWOListImport();

            if (WODT != null)
            {
                foreach (DataRow row in WODT.Rows)
                {
                    EN.OpenWOListEntities WO = new EN.OpenWOListEntities();
                    WO.SrNo = Convert.ToString(row["SRNO"]);
                    WO.WONo = Convert.ToString(row["WO_NO"]);
                    WO.WODate = Convert.ToString(row["WO_Date"]);
                    WO.WOType = Convert.ToString(row["Wo_Type"]);
                    WO.OnAccount = Convert.ToString(row["AGName"]);
                    WO.CHA = Convert.ToString(row["CHAName"]);
                    WO.IGMNo = Convert.ToString(row["IGMNo"]);
                    WO.ItemNo = Convert.ToString(row["ItemNo"]);
                    WO.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    WO.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    WO.Warehouse = Convert.ToString(row["LocationName"]);
                    WO.Pkgs = Convert.ToString(row["PKGSDestuff"]);
                    WO.Vendor = Convert.ToString(row["Name"]);
                    WO.SequenceNo = Convert.ToString(row["SequenceNo"]);
                    OpenWOList.Add(WO);
                }
            }
            return OpenWOList;
        }
        public List<EN.WorkOrderEntities> GetConValList(string ContainerNo)
        {
            List<EN.WorkOrderEntities> CONList = new List<EN.WorkOrderEntities>();
            DataTable CDT = new DataTable();
            CDT = TrackerManager.GetConValData(ContainerNo);

            EN.WorkOrderEntities CON = new EN.WorkOrderEntities();
            if (CDT != null)
            {
                if (CDT.Rows.Count > 0)
                {
                    foreach(DataRow dataRow in CDT.Rows)
                    {
                        CON.ContainerNo = "1";
                        CON.ContPrevPkgs =Convert.ToDecimal(dataRow["PrevPKGS"]);
                        CON.VendorID = Convert.ToInt16(dataRow["Vendor"]);
                    }
                }
                else
                {
                    CON.ContainerNo = "0";
                }
            }
            else
            {
                CON.ContainerNo = "0";
            }
            CONList.Add(CON);
            return CONList;
        }
        public List<EN.WorkOrderEntities> GetConValListImport(string ContainerNo, string Category, string ddlWOType)
        {
            List<EN.WorkOrderEntities> CONList = new List<EN.WorkOrderEntities>();
            DataTable CDT = new DataTable();
            CDT = TrackerManager.GetConValDataImport(ContainerNo, Category, ddlWOType);

            EN.WorkOrderEntities CON = new EN.WorkOrderEntities();
            if (CDT != null)
            {
                if (CDT.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in CDT.Rows)
                    {
                        CON.ContainerNo = "1";
                        CON.CHAID = Convert.ToInt32(dataRow["CHAID"]);
                        CON.CHAName = Convert.ToString(dataRow["CHAName"]);
                        CON.ShipperName = Convert.ToString(dataRow["Consignee"]);
                        CON.OnAccount = Convert.ToString(dataRow["OnAccountName"]);
                        CON.OnAccountID = Convert.ToInt32(dataRow["OnAccountId"]);
                        CON.ManifestQty = Convert.ToString(dataRow["IGM_Qty"]);
                        CON.Weight = Convert.ToString(dataRow["Weight"]);
                        CON.CargoDescription = Convert.ToString(dataRow["IGM_GoodsDesc"]);
                        CON.SBNo = Convert.ToString(dataRow["IGMNo"]);
                        CON.SBEntryID = Convert.ToInt32(dataRow["JoNo"]);
                    }
                }
                else
                {
                    CON.ContainerNo = "0";
                }
            }
            else
            {
                CON.ContainerNo = "0";
            }
            CONList.Add(CON);
            return CONList;
        }
        public List<EN.WorkOrderEntities> GetTruckValListImport(string TruckNo, string Category, string ddlWOType)
        {
            List<EN.WorkOrderEntities> CONList = new List<EN.WorkOrderEntities>();
            DataTable CDT = new DataTable();
            CDT = TrackerManager.GetTruckValDataImport(TruckNo, Category, ddlWOType);

            EN.WorkOrderEntities CON = new EN.WorkOrderEntities();
            if (CDT != null)
            {
                if (CDT.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in CDT.Rows)
                    {
                        CON.ContainerNo = Convert.ToString(dataRow["ContainerNo"]);
                        CON.CHAID = Convert.ToInt32(dataRow["CHAID"]);
                        CON.CHAName = Convert.ToString(dataRow["CHAName"]);
                        CON.ShipperName = Convert.ToString(dataRow["Consignee"]);
                        CON.OnAccount = Convert.ToString(dataRow["OnAccountName"]);
                        CON.OnAccountID = Convert.ToInt32(dataRow["OnAccountId"]);
                        CON.ManifestQty = Convert.ToString(dataRow["IGM_Qty"]);
                        CON.Weight = Convert.ToString(dataRow["Weight"]);
                        CON.CargoDescription = Convert.ToString(dataRow["IGM_GoodsDesc"]);
                        CON.SBNo = Convert.ToString(dataRow["IGMNo"]);
                        CON.SBEntryID = Convert.ToInt32(dataRow["JoNo"]);

                    }
                }
                else
                {
                    CON.ContainerNo = "0";
                }
            }
            else
            {
                CON.ContainerNo = "0";
            }
            CONList.Add(CON);
            return CONList;
        }

        public List<EN.WorkOrderEntities> GetBOEValListImport(string TruckNo, string Category, string ddlWOType)
        {
            List<EN.WorkOrderEntities> CONList = new List<EN.WorkOrderEntities>();
            DataTable CDT = new DataTable();
            CDT = TrackerManager.GetBOEValDataImport(TruckNo, Category, ddlWOType);

            EN.WorkOrderEntities CON = new EN.WorkOrderEntities();
            if (CDT != null)
            {
                if (CDT.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in CDT.Rows)
                    {
                        CON.ContainerNo = Convert.ToString(dataRow["ContainerNo"]);
                        CON.CHAID = Convert.ToInt32(dataRow["CHAID"]);
                        CON.CHAName = Convert.ToString(dataRow["CHAName"]);
                        CON.ShipperName = Convert.ToString(dataRow["Consignee"]);
                        CON.OnAccount = Convert.ToString(dataRow["OnAccountName"]);
                        CON.OnAccountID = Convert.ToInt32(dataRow["OnAccountId"]);
                        CON.ManifestQty = Convert.ToString(dataRow["IGM_Qty"]);
                        CON.Weight = Convert.ToString(dataRow["Weight"]);
                        CON.CargoDescription = Convert.ToString(dataRow["IGM_GoodsDesc"]);
                        CON.SBNo = Convert.ToString(dataRow["IGMNo"]);
                        CON.SBEntryID = Convert.ToInt32(dataRow["JoNo"]);
                        CON.VehicleNo = Convert.ToString(dataRow["VehicleNo"]);


                    }
                }
                else
                {
                    CON.ContainerNo = "0";
                }
            }
            else
            {
                CON.ContainerNo = "0";
            }
            CONList.Add(CON);
            return CONList;
        }
        public List<EN.PrintWOEntities> GetWOPrintList(string WONo)
        {
            List<EN.PrintWOEntities> WOList = new List<EN.PrintWOEntities>();
            DataSet CDS = new DataSet();
            CDS = TrackerManager.GetPrintWODate(WONo);

            if (CDS.Tables[0] != null)
            {
                foreach (DataRow row in CDS.Tables[1].Rows)
                {
                    EN.PrintWOEntities WO = new EN.PrintWOEntities();
                    WO.WONo = Convert.ToString(CDS.Tables[0].Rows[0]["WO_NO"]);
                    WO.WODate = Convert.ToString(CDS.Tables[0].Rows[0]["WO_Date"]);
                    WO.WOType = Convert.ToString(CDS.Tables[0].Rows[0]["Wo_Type"]);
                    WO.Agent = Convert.ToString(CDS.Tables[0].Rows[0]["AGName"]);
                    WO.CHAName = Convert.ToString(CDS.Tables[0].Rows[0]["CHAName"]);
                    WO.SBNoPrint = Convert.ToString(CDS.Tables[0].Rows[0]["SbNo"]);

                    WO.StuffLocation = Convert.ToString(row["StuffLocation"]);
                    WO.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    WO.LocationName = Convert.ToString(row["LocationName"]);
                    WO.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    WO.PKGSDestuff = Convert.ToString(row["PKGSDestuff"]);
                    WO.Package = Convert.ToString(row["Package"]);
                    WO.weight = Convert.ToString(row["weight"]);
                    WO.Equipment = Convert.ToString(row["Equipment"]);
                    WO.Vendor = Convert.ToString(row["Vendor"]);
                    WO.Remarks = Convert.ToString(row["DESCRIPTIONS"]);
                    WOList.Add(WO);
                }
            }
            return WOList;
        }
        public List<EN.WorkOrderReportEntities> GetWOReportList(string FromDate, string ToDate, string Criteria, string SearchText)
        {
            List<EN.WorkOrderReportEntities> WOReportList = new List<EN.WorkOrderReportEntities>();
            DataTable WORDT = new DataTable();
            WORDT = TrackerManager.GetWOReportData(FromDate, ToDate, Criteria, SearchText);

            if (WORDT != null)
            {
                foreach (DataRow row in WORDT.Rows)
                {
                    EN.WorkOrderReportEntities WOR = new EN.WorkOrderReportEntities();
                    WOR.SrNo = Convert.ToString(row["SrNo"]);
                    WOR.SBNo = Convert.ToString(row["SbNo"]);
                    WOR.StuffLocation = Convert.ToString(row["StuffLocation"]);
                    WOR.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    WOR.LocationName = Convert.ToString(row["LocationName"]);
                    WOR.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    WOR.PKGSDestuff = Convert.ToString(row["PKGSDestuff"]);
                    WOR.Package = Convert.ToString(row["Package"]);
                    WOR.weight = Convert.ToString(row["Weight"]);
                    WOR.Equipment = Convert.ToString(row["Equipment"]);
                    WOR.Vendor = Convert.ToString(row["Name"]);
                    WOR.Remarks = Convert.ToString(row["descriptions"]);
                    WOReportList.Add(WOR);
                }
            }
            return WOReportList;
        }

        public int AddRailRRData(DataTable dt, int userid, string TrainNo, string FileType,string RRNo, string RRDate)
        {
            List<EN.IGMEntities> IGMList = new List<EN.IGMEntities>();
            DataTable IgmDt = new DataTable();
            int i;
            int IgmFileId = 0;
            string message = "";
            
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userid);
            lstparam.Add("TrainNo", TrainNo);
            lstparam.Add("FileType", FileType);
            lstparam.Add("RRNo", RRNo);
            lstparam.Add("RRDate", RRDate);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            IgmFileId = db.AddTypeTableDataIgm("USP_InsertRailFileData", lstparam, dt, "PT_IGMFile", "@PT_IGMFile");
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return IgmFileId;
        }

        public string SaveRrailWorkOrder(DataTable WOdata, string TrainNo, string RRNo, string FileType,int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("TrainNo",TrainNo);
            parameterList.Add("RRNo", RRNo);
            parameterList.Add("UserId", UserID);
            parameterList.Add("WO_TYPE", FileType);

            int i = db.AddTypeTableData("USP_InsRailWorkOrder", parameterList, WOdata, "RailWorkOrderDT", "@RailWorkOrderDT");

            if (i > 0)
            {
                Message = "1";
            }
            else
            {
                Message = "0";
            }
            return Message;
        }


        // other Invoies




        public List<EN.SettingTax> GetSettingtax()
        {
            List<EN.SettingTax> locationDL = new List<EN.SettingTax>();
            DataTable dt = new DataTable();
            string Table = "settings_taxes";
            string Column = "Distinct settingsID,taxname";
            string Condition = "";
            string OrderBy = "taxname";

            dt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.SettingTax locationList = new EN.SettingTax();
                    locationList.Settingid = Convert.ToInt32(row["settingsID"]);
                    locationList.TaxName = Convert.ToString(row["taxname"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public EN.SettingTax GetsettingCode()
        {
            EN.SettingTax locationDL = new EN.SettingTax();
            DataTable dt = new DataTable();
            string Table = "settings";
            string Column = "Tinnumber";
            string Condition = "";
            string OrderBy = "";

            dt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {



                locationDL.TaxName = Convert.ToString(dt.Rows[0][0]);


            }
            return locationDL;
        }
        public string CancelOtherInvoicePorforma(string AssessNo, string workyear, int userId)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = TrackerManager.CancelOtherInvoiceProforma1(AssessNo, workyear, userId);

            message = "Records Cancel Successfully!";
            return message;
        }
        public string SubmitOtherFinalDetails(string AssessNo, string workyear, int userId)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = TrackerManager.SubmitFinalotherInvoiceproforma1(AssessNo, workyear, userId);

            message = "Final invoice is generated successfully!";
            return message;
        }
        public string CheckCancelAssessmentDetails(string Invoiceno, string WorkYear)        {            string message = "";            DataTable updateDl = new DataTable();            updateDl = TrackerManager.CheckAckAssessmentDetails(Invoiceno, WorkYear);            if (updateDl.Rows.Count > 0)            {                message = Convert.ToString(updateDl.Rows[0][0]);            }            return message;        }
        public string CancelAssessmentDetails(string remarks, string AssessNo, string workyear, int userId)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = TrackerManager.CancelAssessmentDetails(remarks, AssessNo, workyear, userId);

            message = "Records Cancel Successfully!";
            return message;
        }
        public List<EN.Customer> getParty()
        {
            List<EN.Customer> CustomerDL = new List<EN.Customer>();
            DataTable CustomerDT = new DataTable();
            string Table = "Party_GST_M";
            string Column = "isnull(Common_ID,0)Common_ID,GSTName";
            string Condition = "";
            string OrderBy = "GSTName";

            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
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

        public List<EN.ImportAccountMaster> GetAccountHead()
        {
            List<EN.ImportAccountMaster> locationDL = new List<EN.ImportAccountMaster>();
            DataTable dt = new DataTable();
            string Table = "import_accountmaster";
            string Column = "Distinct AccountID,AccountName";
            string Condition = "";
            string OrderBy = "AccountName";

            dt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ImportAccountMaster locationList = new EN.ImportAccountMaster();
                    locationList.AccountID = Convert.ToInt32(row["AccountID"]);
                    locationList.AccountName = Convert.ToString(row["AccountName"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<EN.CHA> getCHA()
        {
            List<EN.CHA> ChaDL = new List<EN.CHA>();
            DataTable ChaDT = new DataTable();
            string Table = "CHA";
            string Column = "CHAID,CHAName";
            string Condition = "IsActive=1";
            string OrderBy = "CHAName";

            ChaDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ChaDT != null)
            {
                foreach (DataRow row in ChaDT.Rows)
                {
                    EN.CHA ChaList = new EN.CHA();
                    ChaList.CHANo = Convert.ToInt64(row["CHAID"]);
                    ChaList.CHAName = Convert.ToString(row["CHAName"]);

                    ChaDL.Add(ChaList);
                }
            }
            return ChaDL;
        }

        public List<EN.LocationMaster> getLocation()
        {
            List<EN.LocationMaster> locationDL = new List<EN.LocationMaster>();
            DataTable dt = new DataTable();
            string Table = "Ext_Location_m";
            string Column = "LocationID,Location";
            string Condition = "";
            string OrderBy = "Location";

            dt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationMaster locationList = new EN.LocationMaster();
                    locationList.LocationID = Convert.ToInt32(row["LocationID"]);
                    locationList.Location = Convert.ToString(row["Location"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<EN.Commodity> getAccountAdd()
        {
            List<EN.Commodity> locationDL = new List<EN.Commodity>();
            DataTable dt = new DataTable();
            string Table = "Commodity_Group_M";
            string Column = "Commodity_Group_ID,Commodity_Group_Name";
            string Condition = "";
            string OrderBy = "Commodity_Group_Name";

            dt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.Commodity locationList = new EN.Commodity();
                    locationList.CommodityID = Convert.ToInt32(row["Commodity_Group_ID"]);
                    locationList.CommodityName = Convert.ToString(row["Commodity_Group_Name"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<EN.TransportMode> GetTranspot()
        {
            List<EN.TransportMode> Mode = new List<EN.TransportMode>();
                DataTable dt = new DataTable();

            string Table = "Transport_Type_M";
            string Column = "Transport_Type_ID,Transport_Type";
            string Condition = "";
            string OrderBy = "Transport_Type";

            dt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TransportMode transport = new EN.TransportMode();
                    transport.TrasportID = Convert.ToInt32(row["Transport_Type_ID"]);
                    transport.TransportName = Convert.ToString(row["Transport_Type"]);
                    Mode.Add(transport);
                }
            }
            return Mode;
        }

        public List<EN.CargoType> GatCargoTypeFill()
        {
            List<EN.CargoType> CargoLD = new List<EN.CargoType>();
            DataTable dt = new DataTable();

            string Table = "CargoType";
            string Column = "Cargotypeid,CargoType";
            string Condition = "";
            string OrderBy = "CargoType";

            dt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CargoType Cargo = new EN.CargoType();
                    Cargo.Cargotypeid = Convert.ToInt32(row["Cargotypeid"]);
                    Cargo.Cargotype = Convert.ToString(row["CargoType"]);
                    CargoLD.Add(Cargo);
                }
            }
            return CargoLD;
        }

        public List<EN.ImportProformaSearchGST> GetGSTList(string SearchText)
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetGSTSearchImportProforma(SearchText);
            List<EN.ImportProformaSearchGST> isCHACode = new List<EN.ImportProformaSearchGST>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.ImportProformaSearchGST oper = new EN.ImportProformaSearchGST();
                    oper.GSTIn_uniqID = Convert.ToString(row["GSTIn_uniqID"]);
                    oper.GSTName = Convert.ToString(row["GSTName"]);
                    oper.State = Convert.ToString(row["State"]);
                    oper.GSTAddress = Convert.ToString(row["GSTAddress"]);
                    oper.GSTID = Convert.ToString(row["GSTID"]);
                    oper.state_Code = Convert.ToString(row["state_Code"]);
                    oper.RegType = Convert.ToString(row["TyepReg"]);

                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }
        public List<EN.EquipmentWOEntities> GetEquipment()
        {
            List<EN.EquipmentWOEntities> EquipmentDL = new List<EN.EquipmentWOEntities>();
            DataTable dt = new DataTable();
            string Table = "EquipmentM";
            string Column = "Id,Equipment";
            string Condition = "";
            string OrderBy = "Id desc";

            dt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.EquipmentWOEntities EquipmentList = new EN.EquipmentWOEntities();
                    EquipmentList.Id = Convert.ToInt32(row["Id"]);
                    EquipmentList.Equipment = Convert.ToString(row["Equipment"]);
                    EquipmentDL.Add(EquipmentList);
                }
            }
            return EquipmentDL;
        }
        public List<EN.Surveyor> GetSurveyor()
        {
            List<EN.Surveyor> CustomerList = new List<EN.Surveyor>();
            DataTable dt = new DataTable();
            string Table = "Surveyor_Master";
            string Column = "SurveyorId,SurveyorName";
            string Condition = "";
            string OrderBy = "SurveyorName";

            dt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.Surveyor Customers = new EN.Surveyor();
                    Customers.SurveyorId = Convert.ToInt32(row["SurveyorId"]);
                    Customers.SurveyorName = Convert.ToString(row["SurveyorName"]);
                    CustomerList.Add(Customers);
                }
            }
            return CustomerList;
        }
        public string WorkOrderSaveLashingAndChocking(DataTable WOdata, string WODate, string WOType, string SBNo, string SBEntryID, string CHAID, string OnAccountID, int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("WO_DATE", Convert.ToDateTime(WODate).ToString("yyyy-MM-dd HH:mm"));
            parameterList.Add("WO_TYPE", WOType);
            parameterList.Add("ADDEDBY", UserID);
            parameterList.Add("AGENTID", OnAccountID);
            parameterList.Add("CHAID", CHAID);
            parameterList.Add("SBNO", SBNo);
            parameterList.Add("SBENTRYID", SBEntryID);

            int i = db.AddTypeTableData("USP_INSERT_INTO_EXPORT_WORK_ORDER_WithLashing_Chocking", parameterList, WOdata, "PT_ExportWorkOrder_LAndC", "@PT_ExportWorkOrder_LAndC");
            //int i = db.AddTypeTableData("USP_InsRailWorkOrder", parameterList, WOdata, "RailWorkOrderDT", "@RailWorkOrderDT");
            if (i > 0)
            {
                Message = "1";
            }
            else
            {
                Message = "0";
            }
            return Message;
        }
        public List<EN.WorkOrderReport> GetcartingIDForworkorder_LashingAndChocking(string cartingiD)
        {
            List<EN.WorkOrderReport> WOList = new List<EN.WorkOrderReport>();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetCartingIDDetailsForworkOrder_LashingAndChocking(cartingiD);

            if (WODT != null)
            {
                foreach (DataRow row in WODT.Rows)
                {
                    EN.WorkOrderReport WO = new EN.WorkOrderReport();
                    WO.StuffLocation = Convert.ToString(row["Stuff Location"]);
                    WO.Containerno = Convert.ToString(row["Container No"]);
                    WO.Warehouse = Convert.ToString(row["Warehouse"]);
                    WO.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    WO.DestuffQty = Convert.ToString(row["Pkgs"]);
                    WO.Unit = Convert.ToString(row["Pkg Type"]);
                    WO.Weight = Convert.ToString(row["Weight"]);
                    WO.Vendorname = Convert.ToString(row["Vendor"]);
                    WO.Equipment = Convert.ToString(row["Equipment type"]);
                    WO.Remarks = Convert.ToString(row["Remarks"]);
                    WO.Hours = Convert.ToString(row["Hours"]);
                    WO.CGM = Convert.ToString(row["CBM"]);
                    WO.CartingAllowID = Convert.ToString(row["CartingAllowID"]);
                    WO.SBNo = Convert.ToString(row["SBNo"]);
                    WO.SBEntryID = Convert.ToString(row["SBEntryID"]);
                    WO.CHAID = Convert.ToString(row["CHAID"]);
                    WO.CHANAME = Convert.ToString(row["CHAName"]);
                    WO.Shipperid = Convert.ToString(row["shipperID"]);
                    WO.Shippername = Convert.ToString(row["shippername"]);
                    WO.Agid = Convert.ToString(row["AGID"]);
                    WO.CustomerName = Convert.ToString(row["AGName"]);
                    WO.SRNO = Convert.ToString(row["Sr No"]);
                    //WO.SRNO = Convert.ToString("");
                    WO.OpeartionalPksg = Convert.ToString(row["Carting pkgs"]);
                    WO.OperationWt = Convert.ToString(row["Carting Weight"]);
                    WO.Examine = Convert.ToString("0");
                    WO.Surveyor = Convert.ToString("0");
                    WO.MaterialQty = Convert.ToInt32("0");
                    WO.MaterialDescriptions = Convert.ToString("");

                    WOList.Add(WO);
                }
            }
            return WOList;
        }

        public List<EN.WorkOrderReport> GetSBNOForworkorder_LashingAndChocking(string SBNO)
        {
            List<EN.WorkOrderReport> WOList = new List<EN.WorkOrderReport>();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetSBNODetailsForworkOrder_LashingAndChocking(SBNO);

            if (WODT != null)
            {
                foreach (DataRow row in WODT.Rows)
                {
                    EN.WorkOrderReport WO = new EN.WorkOrderReport();
                    WO.StuffLocation = Convert.ToString(row["Stuff Location"]);
                    WO.Containerno = Convert.ToString(row["Container No"]);
                    WO.Warehouse = Convert.ToString(row["Warehouse"]);
                    WO.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    WO.DestuffQty = Convert.ToString(row["Pkgs"]);
                    WO.Unit = Convert.ToString(row["Pkg Type"]);
                    WO.Weight = Convert.ToString(row["Weight"]);
                    WO.Vendorname = Convert.ToString(row["Vendor"]);
                    WO.Equipment = Convert.ToString(row["Equipment type"]);
                    WO.Remarks = Convert.ToString(row["Remarks"]);
                    WO.Hours = Convert.ToString(row["Hours"]);
                    WO.CGM = Convert.ToString(row["CBM"]);
                    WO.CartingAllowID = Convert.ToString(row["CartingAllowID"]);
                    WO.SBNo = Convert.ToString(row["SBNo"]);
                    WO.SBEntryID = Convert.ToString(row["SBEntryID"]);
                    WO.CHAID = Convert.ToString(row["CHAID"]);
                    WO.CHANAME = Convert.ToString(row["CHAName"]);
                    WO.Shipperid = Convert.ToString(row["shipperID"]);
                    WO.Shippername = Convert.ToString(row["shippername"]);
                    WO.Agid = Convert.ToString(row["AGID"]);
                    WO.CustomerName = Convert.ToString(row["AGName"]);
                    WO.SRNO = Convert.ToString(row["Sr No"]);
                    WO.OpeartionalPksg = Convert.ToString("0");
                    WO.OperationWt = Convert.ToString("0");
                    WO.Examine = Convert.ToString("0");
                    WO.Surveyor = Convert.ToString("0");
                    WO.MaterialQty = Convert.ToInt32("0");
                    WO.MaterialDescriptions = Convert.ToString("");
                    WOList.Add(WO);
                }
            }
            return WOList;
        }

        public List<EN.WorkOrderReport> GetRequestIDForworkorder_LashingAndChocking(string Requestid)
        {
            List<EN.WorkOrderReport> WOList = new List<EN.WorkOrderReport>();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetRequestDetailsForworkOrder_LashingAndChocking(Requestid);

            if (WODT != null)
            {
                foreach (DataRow row in WODT.Rows)
                {
                    EN.WorkOrderReport WO = new EN.WorkOrderReport();
                    WO.StuffLocation = Convert.ToString(row["Stuff Location"]);
                    WO.Containerno = Convert.ToString(row["Container No"]);
                    WO.Warehouse = Convert.ToString(row["Warehouse"]);
                    WO.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    WO.DestuffQty = Convert.ToString(row["Pkgs"]);
                    WO.Unit = Convert.ToString(row["Pkg Type"]);
                    WO.Weight = Convert.ToString(row["Weight"]);
                    WO.Vendorname = Convert.ToString(row["Vendor"]);
                    WO.Equipment = Convert.ToString(row["Equipment type"]);
                    WO.Remarks = Convert.ToString(row["Remarks"]);
                    WO.Hours = Convert.ToString(row["Hours"]);
                    WO.CGM = Convert.ToString(row["CBM"]);
                    WO.CartingAllowID = Convert.ToString(row["CartingAllowID"]);
                    WO.SBNo = Convert.ToString(row["SBNo"]);
                    WO.SBEntryID = Convert.ToString(row["SBEntryID"]);
                    WO.CHAID = Convert.ToString(row["CHAID"]);
                    WO.CHANAME = Convert.ToString(row["CHAName"]);
                    WO.Shipperid = Convert.ToString(row["shipperID"]);
                    WO.Shippername = Convert.ToString(row["shippername"]);
                    WO.Agid = Convert.ToString(row["AGID"]);
                    WO.CustomerName = Convert.ToString(row["AGName"]);
                    WO.SRNO = Convert.ToString(row["Sr No"]);
                    WO.OpeartionalPksg = Convert.ToString(row["OpeartionalPksg"]);
                    WO.OperationWt = Convert.ToString(row["OperationWt"]);
                    WO.Examine = Convert.ToString("0");
                    WO.Surveyor = Convert.ToString("0");
                    WO.MaterialQty = Convert.ToInt32("0");
                    WO.MaterialDescriptions = Convert.ToString("");
                    WOList.Add(WO);
                }
            }
            return WOList;
        }

        public List<EN.WorkOrderReport> GetContainerWiseworkorderDetails_LashingAndChocking(string Containerno)
        {
            List<EN.WorkOrderReport> WOList = new List<EN.WorkOrderReport>();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetContainerwiseworkOrderDetails_LashingAndChocking(Containerno);

            if (WODT != null)
            {
                foreach (DataRow row in WODT.Rows)
                {
                    EN.WorkOrderReport WO = new EN.WorkOrderReport();
                    WO.StuffLocation = Convert.ToString(row["Stuff Location"]);
                    WO.Containerno = Convert.ToString(row["Container No"]);
                    WO.Warehouse = Convert.ToString(row["Warehouse"]);
                    WO.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    WO.DestuffQty = Convert.ToString(row["Pkgs"]);
                    WO.Unit = Convert.ToString(row["Pkg Type"]);
                    WO.Weight = Convert.ToString(row["Weight"]);
                    WO.Vendorname = Convert.ToString(row["Vendor"]);
                    WO.Equipment = Convert.ToString(row["Equipment type"]);
                    WO.Remarks = Convert.ToString(row["Remarks"]);
                    WO.Hours = Convert.ToString(row["Hours"]);
                    WO.CGM = Convert.ToString(row["CBM"]);
                    WO.CartingAllowID = Convert.ToString(row["CartingAllowID"]);
                    WO.SBNo = Convert.ToString(row["SBNo"]);
                    WO.SBEntryID = Convert.ToString(row["SBEntryID"]);
                    WO.CHAID = Convert.ToString(row["CHAID"]);
                    WO.CHANAME = Convert.ToString(row["CHAName"]);
                    WO.Shipperid = Convert.ToString(row["shipperID"]);
                    WO.Shippername = Convert.ToString(row["shippername"]);
                    WO.Agid = Convert.ToString(row["AGID"]);
                    WO.CustomerName = Convert.ToString(row["AGName"]);
                    WO.SRNO = Convert.ToString(row["Sr No"]);
                    WO.OpeartionalPksg = Convert.ToString("0");
                    WO.OperationWt = Convert.ToString("0");
                    WO.Examine = Convert.ToString("0");
                    WO.Surveyor = Convert.ToString("0");
                    WO.MaterialQty = Convert.ToInt32("0");
                    WO.MaterialDescriptions = Convert.ToString("");
                    WOList.Add(WO);
                }
            }
            return WOList;
        }

        public List<EN.FCLsummaryDetails> GetExportEditDetailsgird(string WONo)
        {

            List<EN.FCLDestuffDetailsEntites> HoldList = new List<EN.FCLDestuffDetailsEntites>();
            List<EN.FCLsummaryDetails> FCLdetailssumary = new List<EN.FCLsummaryDetails>();
            DataTable Dl = new DataTable();

            Dl = TrackerManager.GetEditExportDetailsgrid(WONo);

            if (Dl != null)
            {
                foreach (DataRow row in Dl.Rows)
                {
                    EN.FCLsummaryDetails FCLlist1 = new EN.FCLsummaryDetails();
                    FCLlist1.StuffLocation = Convert.ToString(row["StuffLocation"]);
                    FCLlist1.Containerno = Convert.ToString(row["ContainerNo"]);
                    FCLlist1.WarehouseID = Convert.ToString(row["WarehouseID"]);
                    FCLlist1.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    FCLlist1.PKGSDestuff = Convert.ToString(row["PKGSDestuff"]);
                    FCLlist1.PkgType = Convert.ToString(row["PkgType"]);
                    FCLlist1.Weight = Convert.ToString(row["Weight"]);
                    FCLlist1.Vendor = Convert.ToString(row["Vendor"]);
                    FCLlist1.Equipment = Convert.ToString(row["Equipment"]);
                    FCLlist1.Remarks = Convert.ToString(row["REMARKS"]);
                    FCLlist1.Equipment = Convert.ToString(row["Equipment"]);
                    FCLlist1.Hours = Convert.ToString(row["Hours"]);
                    FCLlist1.CGM = Convert.ToString(row["CGM"]);
                    FCLlist1.Vendorid = Convert.ToString(row["Vendorid"]);
                    FCLlist1.WarehouseID = Convert.ToString(row["WarehouseID"]);
                    FCLlist1.PackageID = Convert.ToString(row["PackageID"]);
                    FCLlist1.CartingAllowId = Convert.ToString(row["CartingAllowId"]);
                    FCLlist1.EntryID = Convert.ToString(row["Entryid"]);
                    FCLlist1.SRNO = Convert.ToString(row["Entryid"]);
                    FCLlist1.SBNO = Convert.ToString(row["sbno"]);
                    FCLlist1.SBentryid = Convert.ToString(row["sbentryid"]);
                    FCLlist1.OpeartionalPksg = Convert.ToString(row["PKGSRestuff"]);
                    FCLlist1.OperationWt = Convert.ToString(row["CargoWeight"]);
                    FCLlist1.Examine = Convert.ToString(row["Examine"]);
                    FCLlist1.Surveyor = Convert.ToString(row["Surveyorid"]);
                    FCLlist1.cha = Convert.ToString(row["CHAName"]);
                    FCLlist1.shippername = Convert.ToString(row["shippername"]);
                    FCLlist1.CargoDesc = Convert.ToString(row["cargodesc"]);
                    FCLlist1.Onaccount = Convert.ToString(row["AGName"]);

                    FCLlist1.MaterialQty = Convert.ToString(row["MaterialQty"]);
                    FCLlist1.MaterialDescriptions = Convert.ToString(row["MaterialDescriptions"]);


                    FCLdetailssumary.Add(FCLlist1);
                }
            }
            return FCLdetailssumary;
        }
        public string GetCheckStuffingRequestDone(string Requestid, string wotype)
        {
            string message = "";
            DataTable WODT = new DataTable();
            WODT = TrackerManager.GetStuffingRequestDone(Requestid, wotype);

            if (WODT.Rows.Count > 0)
            {
                message = "record already saved Please check.";
            }
            else
            {
                message = "0";
            }
            return message;
        }

        public List<EN.FCLsummaryDetails> ExportSaveTempWorkOrder(DataTable Containerdetails, string Containerno1, int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            List<EN.FCLsummaryDetails> obj = new List<EN.FCLsummaryDetails>();



            parameterList.Add("Containerno1", Containerno1);
            parameterList.Add("ADDEDBY", UserID);
            DataTable dt = new DataTable();


            dt = db.DataTableAddTypeTable("USP_INSERT_Export_WORK_ORDER_TEMP_Close", parameterList, Containerdetails, "PT_ExportWorkOrder_Temp", "@PT_ExportWorkOrder_Temp");
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.FCLsummaryDetails FCLlist1 = new EN.FCLsummaryDetails();
                    FCLlist1.StuffLocation = Convert.ToString(row["StuffLocation"]);
                    FCLlist1.Containerno = Convert.ToString(row["ContainerNo"]);
                    FCLlist1.WarehouseID = Convert.ToString(row["WarehouseID"]);
                    FCLlist1.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    FCLlist1.PKGSDestuff = Convert.ToString(row["PKGSDestuff"]);
                    FCLlist1.PkgType = Convert.ToString(row["PkgType"]);
                    FCLlist1.Weight = Convert.ToString(row["Weight"]);
                    FCLlist1.Vendor = Convert.ToString(row["Vendor"]);
                    FCLlist1.Equipment = Convert.ToString(row["Equipment"]);
                    FCLlist1.Remarks = Convert.ToString(row["REMARKS"]);
                    FCLlist1.Hours = Convert.ToString(row["Hours"]);
                    FCLlist1.CGM = Convert.ToString(row["CGM"]);
                    FCLlist1.EntryID = Convert.ToString(row["Entryid"]);
                    FCLlist1.SRNO = Convert.ToString(row["Entryid"]);
                    FCLlist1.CartingAllowId = Convert.ToString(row["CartingAllowId"]);
                    FCLlist1.SBNO = Convert.ToString(row["SBNO"]);
                    FCLlist1.SBentryid = Convert.ToString(row["SBentryid"]);
                    FCLlist1.OpeartionalPksg = Convert.ToString(row["OpPkgs"]);
                    FCLlist1.OperationWt = Convert.ToString(row["Opweight"]);
                    FCLlist1.Examine = Convert.ToString(row["Examine"]);
                    FCLlist1.Surveyor = Convert.ToString(row["Surveyorid"]);
                    FCLlist1.MaterialQty = Convert.ToString(row["MaterialQty"]);
                    FCLlist1.MaterialDescriptions = Convert.ToString(row["MaterialDescriptions"]);
                    obj.Add(FCLlist1);



                }
            }

            return obj;
        }

        public List<EN.WorkOrderReport> ClearExportTempWorkorder(string ContainerNo, int UserID)
        {
            List<EN.WorkOrderReport> WOList = new List<EN.WorkOrderReport>();
            DataTable WODT = new DataTable();
            WODT = TrackerManager.ClearExportTempWorkorder(ContainerNo, UserID);

            EN.WorkOrderReport WO = new EN.WorkOrderReport();
            WO.Remarks = "Success";


            WOList.Add(WO);

            return WOList;
        }

        // developed by prathamesh 

        //developed by prathamesh
        public DataTable GetLashingAndChockingExpVsRevenueReport(string FromDate, string ToDate)
        {
            try
            {
                DataTable ItemDL = new DataTable();
                ItemDL = TrackerManager.GetLashingAndChockingExpVsRevenueReport(FromDate, ToDate);
                return ItemDL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CheckContainerGenInvoiceBL(string Containerno)        {            string message = "";            DataTable updateDl = new DataTable();            updateDl = TrackerManager.CheckContainerGenInvoiceDL(Containerno);            if (updateDl.Rows.Count > 0)            {                message = Convert.ToString(updateDl.Rows[0][0]);            }            return message;        }
    }
}
