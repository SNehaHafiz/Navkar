using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public class ModificationEntities
    {
        //DeclQty,UnitWT,Allowwt,[Location],EntryDate
        public int DeclQty { get; set; }
        public float UnitWT { get; set; }
        public string EntryDate { get; set; }
        public float Allowwt { get; set; }
        public string Location { get; set; }
        public int SrNo { get; set; }
        public int SBEntryID { get; set; }
        public int EntryID { get; set; }
        public int CartingAllowID { get; set; }
        public string Edit { get; set; }
        public string SBNo { get; set; }
        public string VehicleNo { get; set; }
        public string Remarks { get; set; }
        public string Cancel { get; set; }
        public int AGID { get; set; }

        public int AddedBy { get; set; }
        public bool IsActive { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Vendor { get; set; }

        public string Description { get; set; }
        public string HSNCode { get; set; }


        public int CartingID { get; set; }


        public string NOCDate { get; set; }
        public string BOENo { get; set; }
        public string ImporterName { get; set; }
        public string NOCNO { get; set; }

        public int Party { get; set; }

        public string CHAName { get; set; }

    }

    public class CartingLOcation
    {


        public int LocationID { get; set; }
        public string Location { get; set; }


    }


    public class DomesticRemarks
    {
        //DeclQty,UnitWT,Allowwt,[Location],EntryDate

        public int AddedBy { get; set; }
        public string InvoiceNo { get; set; }
        public string Remarks { get; set; }
        public string InWord { get; set; }
        public string OutWord { get; set; }
        public string Commodity { get; set; }
        public float Qty { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string ISACK { get; set; }
        public int receiptEntry { get; set; }
        public int SrNo { get; set; }
        public float oldQty { get; set; }
        public string IsInvLocked { get; set; }

    }

    public class WaraiDetails
    {
        //DeclQty,UnitWT,Allowwt,[Location],EntryDate

        //AssessNo,WorkYear,VehicleNo,CargoQty,CargoWeight,PartyId,NetTotal,IGST,CGST,SGST,AssessDate
        public string InvoiceNo { get; set; }
        public string AssessNo { get; set; }
        public string VehicleNo { get; set; }
        public string CargoQty { get; set; }
        public string CargoWeight { get; set; }
        public string PartyName { get; set; }
        public int PartyID { get; set; }
        public string Stax { get; set; }
        public int TaxID { get; set; }
        public string NetTotal { get; set; }
        public string IGST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string AssessDate { get; set; }
        public string WorkYear { get; set; }


        public int isack { get; set; }
        public int Series { get; set; }
        public string cargodesc { get; set; }
        public string AccountName { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public int FromID { get; set; }
        public int ToID { get; set; }
        public int isinvlocked { get; set; }
        public int Iscancel { get; set; }
        public string TransName { get; set; }
        public string narration { get; set; }
        public string C2 { get; set; }



    }
    public class VendorList
    {
        //DeclQty,UnitWT,Allowwt,[Location],EntryDate

 
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public int TaxID { get; set; }
       
    }

    public class PinCode
    {
        //DeclQty,UnitWT,Allowwt,[Location],EntryDate


        public string Id { get; set; }
        public string PINCODE { get; set; }
    

    }

    public class ActivityListGet
    {
        public string Activity { get; set; }
    }

    public class GetAllVehicleData
    {
        public string VehicleNo { get; set; }

        public string JONo { get; set; }
        public string Date { get; set; }
    }


    public class SBDetails
    {
        public string SBNo { get; set; }
        public string SBDate { get; set; }
        public int SBEntryID { get; set; }

        public string InvoiceNo { get; set; }
        public string AssessDate { get; set; }
        public string NewInvoiceNo { get; set; }



    }
    public class ImportInvoiceMod
    {
        public int chaid { get; set; }
        public int slid { get; set; }
        public int transtypeid { get; set; }
        public string AssessDate { get; set; }
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string Consignee { get; set; }
        public string deliverytype { get; set; }
        public string Movement { get; set; }
        public string Remarks { get; set; }
    }


    public class Response
    {
        public int Status { get; set; }
        public string ResponseMessage { get; set; }
    }

    public class ModifyImpContainer
    {
        public int AddedBy { get; set; }
        public int ContainerTypeID { get; set; }
        public int Cargotypeid { get; set; }
        public int ISOID { get; set; }
        public string BL_Number { get; set; }
        public string INDate { get; set; }
        public int Size { get; set; }
        public int JONo { get; set; }
        public int Qty { get; set; }
        public int Tare_Weight { get; set; }
        public string SealNo { get; set; }
        public string ContainerNo { get; set; }
        public string Remarks { get; set; }
        public string DOCNo { get; set; }
        public float GrossWT { get; set; }
        public int JoTypeId { get; set; }
        public int CFSCode { get; set; }


    }




    public class ModifyEXPContainer
    {

        public int AddedBy { get; set; }
        public int Type { get; set; }
        public int Cargotypeid { get; set; }
        public int ISOID { get; set; }
        public string Agency { get; set; }
        public string INDate { get; set; }
        public int Size { get; set; }
        public int EntryID { get; set; }
        public int Qty { get; set; }
        public int Tare_Weight { get; set; }
        public string AgentSealNo { get; set; }
        public string ContainerNo { get; set; }
        public string Remarks { get; set; }
        public string CustomSealNo { get; set; }
        public float GrossWT { get; set; }

        public int CFSCode { get; set; }



    }

    public class ExportModifyMaster
    {

        public int AddedBy { get; set; }
        public int Type { get; set; }
        public int GPNo { get; set; }
        public int Cargotypeid { get; set; }
        public int ISOID { get; set; }
        public string EntryDate { get; set; }
        public string CargoDesc { get; set; }
        public string Cargo_Type { get; set; }
        public string Edit { get; set; }
        public int Size { get; set; }
        public int EntryID { get; set; }
        public int SBEntryID { get; set; }
        public int Qty { get; set; }
        public int Tare_Weight { get; set; }
        public string AgentSeal { get; set; }
        public string Agent { get; set; }
        public string Shipper { get; set; }
        public string containerNo { get; set; }
        public string Remarks { get; set; }
        public string CustomSeal { get; set; }
        public string TruckNO { get; set; }

        public string SBNo { get; set; }
        public float Weight { get; set; }
        public int CLPNo { get; set; }

        public string ContainerType { get; set; }
        public string ContainerTypeID { get; set; }


    }

    public class ImportCommonEnt
    {

        public int AddedBy { get; set; }
        public int Type { get; set; }
        public int Cargotypeid { get; set; }
        public int ISOID { get; set; }
        public string EntryDate { get; set; }
        public string IGMNo { get; set; }
        public string Cargo_Type { get; set; }
        public string IGM_ItemNo { get; set; }
        public int Size { get; set; }
        public int JONo { get; set; }
        public int SBEntryID { get; set; }
        public int Qty { get; set; }
        public int Tare_Weight { get; set; }
        public string AgentSeal { get; set; }
        public string Agent { get; set; }
        public string Shipper { get; set; }
        public string SLName { get; set; }
        public string containerNo { get; set; }
        public string Remarks { get; set; }
        public string DeliveryType { get; set; }

        public string SBNo { get; set; }
        public float Weight { get; set; }


        public string ContainerType { get; set; }
        public string ContainerTypeID { get; set; }


    }


    public class DomesticTrainMaster
    {


        public int TrainID { get; set; }
        public string TrainNo { get; set; }


    }

    public class DomesticMasterEnt
    {


        public int TrainID { get; set; }
        public string TrainNo { get; set; }
        public int Srno { get; set; }
        public string ContainerNo { get; set; }
        public string LaodedDate { get; set; }
        public string AGName { get; set; }
        public string Remarks { get; set; }
        public int EntryID { get; set; }
        public int LoadingID { get; set; }

        public string OutDate { get; set; }
        public string RakeNo { get; set; }
        public string Edit { get; set; }
        public string BatchNo { get; set; }
        public string VehicleNo { get; set; }
        public string InDate { get; set; }
        public int SlipNo { get; set; }
        public string LRNo { get; set; }

    }

    public class ScanningModule
    {

        public int AddedBy { get; set; }
        public int DriverID { get; set; }
        public int Size { get; set; }
        public int EntryID { get; set; }
        public int SrNo { get; set; }
        public string Type { get; set; }
        public string Edit { get; set; }
        public string ScanType { get; set; }
        public string TransType { get; set; }
        public string OutDate { get; set; }
        public string JODATE { get; set; }
        public string INDate { get; set; }
        public string trailername { get; set; }
        public int JONo { get; set; }
        public string DriverName { get; set; }
        public string ContainerNo { get; set; }
        public string Remarks { get; set; }
        public int TrailerID { get; set; }
        public string TrailerNo { get; set; }
        public string GPNo { get; set; }

    }

    public class UpdateExcessAmt
    {

        public float Amount { get; set; }
        public int AddedBy { get; set; }
        public string ReceiptNo { get; set; }

    }
    public class LabourGetdetails
    {

        public int SrNo { get; set; }
        public string WO_NO { get; set; }
        public string work_year { get; set; }
        public string SBEntryID { get; set; }
        public string WO_Type { get; set; }
        public string Activity { get; set; }
        public string SBNO { get; set; }
        public string VehicleNo { get; set; }
        public string JONoEntryID { get; set; }
        public string ContainerNo { get; set; }
        public string PKGSDestuff { get; set; }
        public string CargoWeight { get; set; }
        public string cargoweightMTS { get; set; }
        public string Amount { get; set; }
        public string fromdate { get; set; }
        public string Todate { get; set; }


    }
    public class LabourBillVerification
    {



    }
    public class CancelWorkOrderDock
    {

        public string WoNo { get; set; }
        public string containerNo { get; set; }
        public string JoEntryID { get; set; }

    }
    public class ImportExportProcessActivitycs
    {
        public string process { get; set; }
        public string Import { get; set; }
        public string Export { get; set; }
        public string Total { get; set; }
        public string Tues { get; set; }
        public string FinalTotal { get; set; }
        public string ICDRail { get; set; }
        public string ICDRoad { get; set; }
        public string CFSRail { get; set; }
        public string CFSRoad { get; set; }
        public string Inventory { get; set; }
        public string BalInventory { get; set; }
        public int TotalCount { get; set; }
        public string ExportBalInventory { get; set; }
        //added by rahul
        public string LastMonthName { get; set; }
        public string CurrentMonthName { get; set; }
        public string Last24HRSName { get; set; }



        //added by prashant
        public string FCLDestuff { get; set; }
        public string LCLDestuff { get; set; }
        public string IMPLoadedDelivery { get; set; }







        public ImportExportProcessActivitycs()
        {
            ImportExportRailRoadLast24HrsCFS = new List<ImportExportRailRoadLast24HrsCFS>();
            ImportExportRailRoadCurrentMonthCFS = new List<ImportExportRailRoadCurrentMonthCFS>();
            ImportExportRailRoadLastMonthCFS = new List<ImportExportRailRoadLastMonthCFS>();
        }
        public List<ImportExportRailRoadLast24HrsCFS> ImportExportRailRoadLast24HrsCFS { get; set; }
        public List<ImportExportRailRoadCurrentMonthCFS> ImportExportRailRoadCurrentMonthCFS { get; set; }
        public List<ImportExportRailRoadLastMonthCFS> ImportExportRailRoadLastMonthCFS { get; set; }

        //end by rahul
    }
    //Added By Rahul
    public class ImportExportRailRoadLast24HrsCFS
    {
        public string ImportRoad { get; set; }
        public string ImportRail { get; set; }
        public string ExportRoad { get; set; }
        public string ExportRail { get; set; }
        public string TotalTeus { get; set; }
    }
    public class ImportExportRailRoadCurrentMonthCFS
    {
        public string ImportRoad { get; set; }
        public string ImportRail { get; set; }
        public string ExportRoad { get; set; }
        public string ExportRail { get; set; }
        public string TotalTeus { get; set; }
    }
    public class ImportExportRailRoadLastMonthCFS
    {
        public string ImportRoad { get; set; }
        public string ImportRail { get; set; }
        public string ExportRoad { get; set; }
        public string ExportRail { get; set; }
        public string TotalTeus { get; set; }
    }
}

