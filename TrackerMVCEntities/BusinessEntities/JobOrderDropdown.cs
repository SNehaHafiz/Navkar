using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class JobOrderDropdown
    {

        public List<ShipLines> ShipLines { get; set; }

        public List<Shippers> Shippers { get; set; }
    }
        public class ShipLines
        {
            public int SLID { get; set; }
            public string SLName { get; set; }
            public string SLCode { get; set; } 

        }
        public class Shippers
        {
            public int shipperID { get; set; }
            public string shippername { get; set; }
        }

        public class Customer
        {
            public int AGID { get; set; }
            public string AGName { get; set; }
        public int GSTuniqueid { get; set; }
        public string GstuniqueName { get; set; }
        public int CHAID { get; set; }
        public string CHAName { get; set; }
    }
    public class GstDetails
    {
        public int GSTuniqueid { get; set; }
        public string GstuniqueName { get; set; }
    }
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
    }

    public class NOCUpdation
        {
            public int NOCNo { get; set; }
            public int NOCDate { get; set; }
            public string NOCRefNo { get; set; }
            public string OrginStationId { get; set; }
            public string SLid { get; set; }
            public string CommodityId { get; set; }
            public string PlannedDate { get; set; }
            public string StuffingTypeId { get; set; }
    }

    public class Consignee
        {
            public int ImporterID { get; set; }
            public string ImporterName { get; set; }
        }
        public class CHA
        {
            public Int64 CHANo { get; set; }
            public string CHAName { get; set; }
        }
        public class Vessel
        {
            public int VesselID { get; set; }
            public string VesselName { get; set; }
        }
        public class Ports
        {
            public int PortID { get; set; }
            public string PortName { get; set; }
        }
        public class Haulage
        {
            public int Haulage_Type_ID { get; set; }
            public string Haulage_Type { get; set; }
        }
        public class IGMFileStatus
        {
            public int File_Status_ID { get; set; }
            public string File_Status { get; set; }
        }

        public class Transport
        {
            public int Transport_Type_ID { get; set; }
            public string Transport_Type { get; set; }
        }
        public class POL
        {
            public int PODID { get; set; }
            public string PODName { get; set; }
        }
        public class ContainerSize
        {
            public int ContainerSizeID { get; set; }
            public string ContainerSizeName { get; set; }
        }
        public class ISOCodes
        {
            public int ISOID { get; set; }
            public string ISOCode { get; set; }
        }
        public class CargoType
        {
            public int Cargotypeid { get; set; }
            public string Cargotype { get; set; }
        }
        public class CommodityGroup
        {
            public int Commodity_Group_ID { get; set; }
            public string Commodity_Group_Name { get; set; }
        }
        public class SalesPersonM
        {
        public string txtcustomername { get; set; }
        public string txtcreditlimit { get; set; }
        public string team { get; set; }
        public string ReplaceID { get; set; }

        public Int64 SalesPerson_ID1 { get; set; }
        public string SalesPerson_Name { get; set; }

        public long gradeid { get; set; }
        public string gradename { get; set; }
        public string KDM { get; set; }
    }

    public class KDM
    {
        public long Kdmid { get; set; }
        public string KdmName { get; set; }
    }
    public class KAMM
        {
            public Int64 KAMID { get; set; }
            public string KAM { get; set; }
        }

        public class Location
        {
            public int LocationID { get; set; }
            public string LocationName { get; set; }
        }

        public class ContainerType
        {
            public int ContainerTypeID { get; set; }
            public string ContainerTypeName { get; set; }
        }



        public class Cycle
        {
            public int Activity_ID { get; set; }
            public string Activity_Name { get; set; }
        }

        public class Train
        {
            public int TrainId { get; set; }
            public string TrainNo { get; set; }
        }
        public class TransportExpenses
        {
            public int transexpenseid { get; set; }
            public string transexpensename { get; set; }
        }
    public class TrailerDropDown
    {
        public int trailerid { get; set; }
        public string trailername { get; set; }
    }
    public class VoucherNoDropDown
    {
        public int tripID { get; set; }
        public string VoucherNo { get; set; }
    }
    public class SlipDropDown
    {
        public string SLipID { get; set; }
        public string SlipNo { get; set; }
    }
    public class TPDropDown
    {
        public int TPNo { get; set; }
        public string TPSLipNo { get; set; }
    }
    public class ImportJOType
    {
        public int JotypeId { get; set; }
        public string Jotype { get; set; }
    }
    public class IGMFiLeType
    {
        public int FileTypeId { get; set; }
        public string FileTypeName { get; set; }
    }

    public class PaymentModes
    {
        public int PaymentId { get; set; }
        public string PaymentMode { get; set; }
    }

    public class ImportBank
    {
        public string BankId { get; set; }
        public string BankName { get; set; }
    }
    public class TDSPerM
    {
        public int TDSPerId { get; set; }
        public string Percentage { get; set; }
    }
    public class AuctionClearM
    {
        public int ClearId { get; set; }
        public string ClearRemarks { get; set; }
    }

    public class StuffingType
    {
        public long StuffingTypeId { get; set; }
        public string StuffingTypeM { get; set; }
    }
    public class CommonAccountDets
    {
        public int ConAccId { get; set; }
        public string AccountNo { get; set; }
    }
    public class WorkyearReceipt
    {
        
        public string WorkyearTDS { get; set; }
    }
    public class exporterShipping
    {
        public long Exportershippingid { get; set; }
        public string ExporterShippingname { get; set; }

    }

    public class DeliveryTypeDetails
    {
        public int DeliveryID { get; set; }
        public string DeliveryType { get; set; }
        public string DeliveryName { get; set; }
    }
    public class SlabDetails
    {
        public int SlabId { get; set; }
        public string SlabName { get; set; }
    }

    public class importtariffdetails
    {
        public int TariffID { get; set; }
        public string TariffDescription { get; set; }
    }
    public class TariffGroup
    {
        public int Group_ID { get; set; }
        public string Group_Name { get; set; }
    }

    public class Allocatedspace
    {
        public string AllocatedID { get; set; }
        public string AllocatedName { get; set; }
    }

    public class Transporter : ShipLines
    {
        public int TransID { get; set; }
        public string Transporters { get; set; }
    }
}



