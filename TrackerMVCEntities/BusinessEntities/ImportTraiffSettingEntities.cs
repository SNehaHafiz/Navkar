using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ImportTraiffSettingEntities
    {
        public string name { get; set; }

        public ImportTraiffSettingEntities()
        {
            ImportaccountMaster = new List<ImportaccountMaster>();
            CommodityMaster = new List<CommodityMaster>();
            ImportInvoiceType = new List<ImportInvoiceType>();
            ChagresBasedOn = new List<ChagresBasedOn>();
            SettingTax = new List<SettingTax>();
            PortsEntites = new List<PortsEntites>();
            ImportJoType = new List<ImportJoType>();
            TransportType_m = new List<TransportType_m>();
            CargoEntititesList = new List<CargoEntititesList>();

        }

        public List<ImportaccountMaster> ImportaccountMaster = new List<ImportaccountMaster>();
        public List<CommodityMaster> CommodityMaster = new List<CommodityMaster>();
        public List<ImportInvoiceType> ImportInvoiceType = new List<ImportInvoiceType>();
        public List<ChagresBasedOn> ChagresBasedOn = new List<ChagresBasedOn>();
        public List<SettingTax> SettingTax = new List<SettingTax>();
        public List<PortsEntites> PortsEntites = new List<PortsEntites>();
        public List<ImportJoType> ImportJoType = new List<ImportJoType>();
        public List<TransportType_m> TransportType_m = new List<TransportType_m>();
        public List<CargoEntititesList> CargoEntititesList = new List<CargoEntititesList>();
    }

    public class ImportaccountMaster
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
    }
    public class CommodityMaster
    {
        public int Commodity_Group_ID { get; set; }
        public string Commodity_Group_Name { get; set; }
    }
    public class ImportInvoiceType
    {
        public int ID { get; set; }
        public string InvoiceType { get; set; }
    }
    public class ChagresBasedOn
    {
        public int Chargeid { get; set; }
        public string BasedOn { get; set; }
    }

    public class SettingTax
    {
        public int Settingid { get; set; }
        public string TaxName { get; set; }
    }
    public class PortsEntites
    {
        public int Portid { get; set; }
        public string PortName { get; set; }
    }
    public class ImportJoType
    {
        public int Jo_type_id { get; set; }
        public string Jo_type { get; set; }
    }
    public class TransportType_m
    {
        public int TransportID { get; set; }
        public string TransportType { get; set; }
    }
    public class CargoEntititesList
    {
        public int cargoid { get; set; }
        public string cargoname { get; set; }
    }


    public class SlabDetailsEntites
    {
        public string SlabOn { get; set; }
        public string RangeFrom { get; set; }
        public string RangeTo { get; set; }
        public string Value { get; set; }
        public string SlabSize { get; set; }
        public string SlabCargoType { get; set; }
        public string SRNo { get; set; }
        public string Location { get; set; }
    }

    public class SDetailsEntites
    {
        public string ContainerNo { get; set; }
        public string ScanType { get; set; }
        public string ScanStatus { get; set; }
        public string Size { get; set; }
        public string JoNo { get; set; }
        public string igmno { get; set; }

        public string Itemno { get; set; }
        public string HoldReasonIgm { get; set; }
        public string ProcessesID { get; set; }
        public string RemarksIgm { get; set; }
    }

    public class HoldReasons
    {
        public int HoldReasonID { get; set; }
        public string HoldReason { get; set; }
        
    }

    public class GetProcesses
    {
        public int ID { get; set; }
        public string Processes { get; set; }

    }

    public class ExpProcess : HoldReasons
    {
        public int ID { get; set; }
        public string Processes { get; set; }
        public string HoldDate { get; set; }
        public string Containerno { get; set; }
        public int Reason { get; set; }
        public string Remarks { get; set; }
        public string SBNo { get; set; }
    }

    // weightment
    public class Weightment
    {
        public int ID { get; set; }
        public string VehicleType { get; set; }
        public string ContainerNo { get; set; }
        public string VehicleNo { get; set; }
        public string WeighDate1 { get; set; }
        public string AddedOn { get; set; }
        public string Size { get; set; }
        public string TareWeight { get; set; }
        public string GrossWeight { get; set; }
        public string VehicleTareWT { get; set; }
        public string NetWeight { get; set; }
        public string GetWeigth { get; set; }

    }

    public class IMPModify : Transporter
    {
        public int ISOID { get; set; }
        public string ISOCode { get; set; }
        public int ContainerTypeID { get; set; }
        public string ContainerType { get; set; }
        public int CargoTypeID { get; set; }
        public string CargoType { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ScanType { get; set; }
        public string I_Size { get; set; }
        public string TareWeight { get; set; }
        public string INDate { get; set; }
        public string EIRDate { get; set; }
        public string Size { get; set; }
        public string SealNoI { get; set; }
        public string I_Wt { get; set; }
        public string jono { get; set; }
    }

    public class ExpModify : IMPModify
    {
        public string BookingNo { get; set; }
        public string condition { get; set; }
        public string EntryType { get; set; }
        public string Source { get; set; }
        public string Remarks { get; set; }

    }


    public class ImportGPAudit
    {
        public int CHAID { get; set; }
        public string CHAName { get; set; }
        public string GPNo { get; set; }
        public string GPDATE { get; set; }
        public string BOENo { get; set; }
        public string BOEDate { get; set; }
        public string OOCNo { get; set; }
        public string OOCDate { get; set; }
        public string Consignee { get; set; }
        public string value { get; set; }
        public string duty { get; set; }

    }

    public class IGMDetails
    {
        public string Cancel { get; set; }
        public int EntryID { get; set; }
        public int JONo { get; set; }
        public string ContainerNo { get; set; }
        public int SIZE { get; set; }
        public string Type { get; set; }
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string Qty { get; set; }
        public string Consignee { get; set; }


    }

}
