using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class WorkOrderEntities
    {
        public string Wo_Type { get; set; }
        public string CHAName { get; set; }
        public int Vendor { get; set; }
        public int CHAID { get; set; }
        public int OnAccountID { get; set; }
        public int Id { get; set; }
        public int WHID { get; set; }
        public int SBEntryID { get; set; }
        public string SBNo { get; set; }
        public string Type { get; set; }
        public string GroupCode { get; set; }

        public string ShipperName { get; set; }
        public string OnAccount { get; set; }
        public string ManifestQty { get; set; }
        public string CargoDescription { get; set; }
        public decimal TotPkgs { get; set; }
        public decimal TotWeight { get; set; }
        public decimal UnitWt { get; set; }
        public decimal PrevPkgs { get; set; }
        public decimal ContPrevPkgs { get; set; }
        public int Pallets { get; set; }
        public int Shifts { get; set; }
        public bool IsStax { get; set; }


        public string StuffLocation { get; set; }
        public string ContainerNo { get; set; }
        public int WareHouseID { get; set; }
        public string VehicleNo { get; set; }
        public decimal StuffPkgs { get; set; }
        public int VendorID { get; set; }
        public int EquipmentID { get; set; }
        public string Description { get; set; }
        public int PkgTypeID { get; set; }
        public string Weight { get; set; }
        public int Unit { get; set; }
        public int CartingAllowId { get; set; }
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string CargoType { get; set; }
        public string Size { get; set; }
        public int EntryId { get; set; }
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public double AcountToCollect { get; set; }
        public string Narration { get; set; }
        public string PackageType { get; set; }
        public string WoNo { get; set; }
        public string WoDate { get; set; }
        public string SrNo { get; set; }
        public long JoNo { get; set; }
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public double ExamPerc { get; set; }
        public double Amount { get; set; }
        public string CMB { get; set; }
        public string OpPkgs { get; set; }
        public string OpWt { get; set; }
        public string Examine { get; set; }
        public string Surveyor { get; set; }
        public string MaterialQty { get; set; }
        public string MaterialDescriptions { get; set; }
        public int MaterialMappingAutoID { get; set; }
        public string Hours { get; set; }
        public WorkOrderEntities()
        {
            Narration = "";
            WOTypeList = new List<WOTypeEntities>();
            WHList = new List<WarehouseEntities>();
            VendorWOList = new List<VendorWOEntities>();
            EQWOList = new List<EquipmentWOEntities>();
            PKGList = new List<PackageWOEntities>();
            ExportAccountMasterList = new List<ExportAccountMaster>();
            VendorMasterList = new List<VendorMaster>();
            ImportAccountMasterList = new List<ImportAccountMaster>();
            CHAList = new List<CHA>();
            containerSizesList = new List<ContainerSize>();
            containerTypesList = new List<ContainerType>();
            cargoTypesList = new List<CargoType>();
            commodityGroupsList = new List<CommodityGroup>();

        }
        public List<WOTypeEntities> WOTypeList { get; set; }
        public List<WarehouseEntities> WHList { get; set; }
        public List<VendorWOEntities> VendorWOList { get; set; }
        public List<EquipmentWOEntities> EQWOList { get; set; }
        public List<PackageWOEntities> PKGList { get; set; }
        public List<ExportAccountMaster> ExportAccountMasterList { get; set; }
        public List<VendorMaster> VendorMasterList { get; set; }
        public List<ImportAccountMaster> ImportAccountMasterList { get; set; }
        public List<CHA> CHAList { get; set; }
        public List<ContainerSize> containerSizesList { get; set; }
        public List<ContainerType> containerTypesList { get; set; }
        public List<CargoType> cargoTypesList { get; set; }
        public List<CommodityGroup> commodityGroupsList { get; set; }
        

    }
    public class WOTypeEntities
    {
        public string Wo_Type { get; set; }
    }
    public class WarehouseEntities
    {
        public int WHID { get; set; }

        public string WHName { get; set; }
    }
    public class VendorWOEntities
    {
        public int VendorId { get; set; }

        public string Name { get; set; }
    }
    public class EquipmentWOEntities
    {
        public int Id { get; set; }

        public string Equipment { get; set; }
    }
    public class VehicleWOEntities
    {
        public string CartingId { get; set; }
        public string VehicleNo { get; set; }
    }
    public class ContainerWOEntities
    {
        public string ContainerNo { get; set; }
    }
    public class PackageWOEntities
    {
        public int CodeID { get; set; }
        public string Package { get; set; }
    }
    public class ExportAccountMaster
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
    }
    public class ImportAccountMaster
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
    }
    public class VendorMaster
    {
        public int VendorId { get; set; }
        public string Name { get; set; }
    }
    public class OpenWOListEntities
    {
        public string SrNo { get; set; }
        public string WONo { get; set; }
        public string WODate { get; set; }
        public string WOType { get; set; }
        public string SBNo { get; set; }
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string OnAccount { get; set; }
        public string CHA { get; set; }
        public string VehicleNo { get; set; }
        public string ContainerNo { get; set; }
        public string Warehouse { get; set; }
        public string Pkgs { get; set; }
        public string Vendor { get; set; }
        public string SequenceNo { get; set; }
    }
    public class PrintWOEntities
    {
        public string WONo { get; set; }
        public string WODate { get; set; }
        public string WOType { get; set; }
        public string Agent { get; set; }
        public string CHAName { get; set; }
        public string SBNoPrint { get; set; }

        public string StuffLocation { get; set; }
        public string ContainerNo { get; set; }
        public string LocationName { get; set; }
        public string VehicleNo { get; set; }
        public string PKGSDestuff { get; set; }
        public string Package { get; set; }
        public string weight { get; set; }
        public string Equipment { get; set; }
        public string Vendor { get; set; }
        public string Remarks { get; set; }
    }
    public class WorkOrderReportEntities
    {
        public string SrNo { get; set; }
        public string SBNo { get; set; }
        public string StuffLocation { get; set; }
        public string ContainerNo { get; set; }
        public string LocationName { get; set; }
        public string VehicleNo { get; set; }
        public string PKGSDestuff { get; set; }
        public string Package { get; set; }
        public string weight { get; set; }
        public string Equipment { get; set; }
        public string Vendor { get; set; }
        public string Remarks { get; set; }
    }
    public class SettingTaxdetails
    {
        public int TaxID { get; set; }
        public string Taxname { get; set; }
    }
    public class Surveyor
    {
        public int SurveyorId { get; set; }
        public string SurveyorName { get; set; }
    }
    public class WorkOrderReport
    {
        public string DestuffDate { get; set; }
        public string Category { get; set; }
        public string WOType { get; set; }
        public string CHAID { get; set; }
        public string Vendorname { get; set; }
        public string JONo { get; set; }
        public string Containerno { get; set; }
        public string Size { get; set; }
        public string DestuffQty { get; set; }
        public string Weight { get; set; }
        public string Examine { get; set; }
        public string Equipment { get; set; }
        public string Remarks { get; set; }

        public string Hours { get; set; }
        public string CGM { get; set; }
        public string VehicleNo { get; set; }
        public string DeliveryType { get; set; }
        public string IGMQty { get; set; }
        public string igmWeight { get; set; }
        public string Unit { get; set; }
        public string EntryID { get; set; }
        public string IGMNo { get; set; }
        public string ITEMNO { get; set; }
        public string Userid { get; set; }
        public string ShortQty { get; set; }
        public string StuffLocation { get; set; }
        public string Warehouse { get; set; }
        public string CartingAllowID { get; set; }
        public string SBNo { get; set; }
        public string SBEntryID { get; set; }
        public string CHANAME { get; set; }
        public string CustomerName { get; set; }
        public string Shippername { get; set; }
        public string Agid { get; set; }
        public string Shipperid { get; set; }
        public string SRNO { get; set; }
        public string OpeartionalPksg { get; set; }
        public string OperationWt { get; set; }
        public string Surveyor { get; set; }
        public int MaterialQty { get; set; }
        public string MaterialDescriptions { get; set; }


    }

    public class FCLsummaryDetails
    {
        public string JONo { get; set; }
        public string Containerno { get; set; }
        public string Size { get; set; }
        public string IGMQty { get; set; }
        public string Weight { get; set; }
        public string Unit { get; set; }
        public string DestuffQty { get; set; }
        public string ShortQty { get; set; }
        public string ExcessQty { get; set; }
        public string TypeFL { get; set; }
        public string Remarks { get; set; }
        public string EntryID { get; set; }
        public string SRNO { get; set; }
        public string Equipment { get; set; }
        public string Examine { get; set; }
        public string DeliveryType { get; set; }
        public string Hours { get; set; }
        public string CGM { get; set; }
        public string Location { get; set; }
        public string VehicleNo { get; set; }
        public string WeightS { get; set; }
        public string StuffLocation { get; set; }
        public string WarehouseID { get; set; }
        public string PKGSDestuff { get; set; }
        public string PkgType { get; set; }
        public string Vendor { get; set; }

        public string PackageID { get; set; }
        public string CartingAllowId { get; set; }
        public string Vendorid { get; set; }
        public string EquipmentID { get; set; }
        public string SBNO { get; set; }
        public string SBentryid { get; set; }
        public string OpeartionalPksg { get; set; }
        public string OperationWt { get; set; }
        public string Surveyor { get; set; }
        public string cha { get; set; }
        public string shippername { get; set; }
        public string Onaccount { get; set; }
        public string CargoDesc { get; set; }
        public string IGMNo { get; set; }
        public int ItemNo { get; set; }
        public string MaterialQty { get; set; }
        public string MaterialDescriptions { get; set; }
    }
}
