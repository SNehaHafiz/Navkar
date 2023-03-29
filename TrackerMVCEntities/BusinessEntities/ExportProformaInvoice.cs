using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ExportProformaInvoice
    {
        
        public ExportProformaInvoice()
        {
            ExportProformaCusomter = new List<ExportProformaCusomter>();
            ExportProformaShipper = new List<ExportProformaShipper>();
            ExportProformaCha = new List<ExportProformaCha>();
            ExportProformaTransType = new List<ExportProformaTransType>();
            ExportProformaBillType = new List<ExportProformaBillType>();
            ExportProformaTariffmaster = new List<ExportProformaTariffmaster>();
            ExportProformaAccountmaster = new List<ExportProformaAccountmaster>();
            ExportProformaLocation = new List<ExportProformaLocation>();
            ExportProformaAllotment = new List<ExportProformaAllotment>();
            ExportProformaCommodity = new List<ExportProformaCommodity>();
            ExportProformaTax_Service = new List<ExportProformaTax_Service>();
            ExportProformaInvoiceType = new List<ExportProformaInvoiceType>();
        }
        public List<ExportProformaCusomter> ExportProformaCusomter = new List<ExportProformaCusomter>();
        public List<ExportProformaShipper> ExportProformaShipper = new List<ExportProformaShipper>();
        public List<ExportProformaCha> ExportProformaCha = new List<ExportProformaCha>();
        public List<ExportProformaTransType> ExportProformaTransType = new List<ExportProformaTransType>();
        public List<ExportProformaBillType> ExportProformaBillType = new List<ExportProformaBillType>();
        public List<ExportProformaTariffmaster> ExportProformaTariffmaster = new List<ExportProformaTariffmaster>();
        public List<ExportProformaAccountmaster> ExportProformaAccountmaster = new List<ExportProformaAccountmaster>();
        public List<ExportProformaLocation> ExportProformaLocation = new List<ExportProformaLocation>();
        public List<ExportProformaAllotment> ExportProformaAllotment = new List<ExportProformaAllotment>();
        public List<ExportProformaCommodity> ExportProformaCommodity = new List<ExportProformaCommodity>();
        public List<ExportProformaTax_Service> ExportProformaTax_Service = new List<ExportProformaTax_Service>();
        public List<ExportProformaInvoiceType> ExportProformaInvoiceType = new List<ExportProformaInvoiceType>();

    }
        public class ExportProformaCusomter
    {

        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }

    public class ExportProformaShipper
    {

        public int shipperID { get; set; }
        public string shippername { get; set; }
    }

    public class ExportProformaCha
    {
        public int ChaID { get; set; }
        public string ChaName { get; set; }
    }
    public class ExportProformaTransType
    {
        public int Transport_Type_ID { get; set; }
        public string Transport_Type { get; set; }
    }
    public class ExportProformaBillType
    {
        public int TypeID { get; set; }
        public string BillType { get; set; }
    }

    public class ExportProformaTariffmaster
    {
        public int TariffID { get; set; }
        public string TariffDescription { get; set; }
    }

    public class ExportProformaAccountmaster
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
    }
    public class ExportProformaLocation
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }

    public class ExportProformaAllotment
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class ExportProformaCommodity
    {
        public int Commodity_Group_ID { get; set; }
        public string Commodity { get; set; }
    }

    public class ExportProformaTax_Service
    {
        public int ID { get; set; }
        public string Tax_Type_Desc { get; set; }
    }
    public class ExportProformaInvoiceType
    {
        public string InvoiceTypeID { get; set; }
        public string InvoiceType { get; set; }
    }

    public class ContainerWiseProformaDetails
    {
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string ContainerType { get; set; }
        public string CargoType { get; set; }
        public string indate { get; set; }
        public string StuffingDate { get; set; }
        public string Gpdate { get; set; }
        public string TotalDays { get; set; }
        public string LoadedDays { get; set; }
        public string EmptyDays { get; set; }
        public string PortName { get; set; }
        public string PickUP { get; set; }
        public string Transport_Type { get; set; }
        public string NetWeight { get; set; }
        public string TareWeight { get; set; }
        public string GrossWeight { get; set; }
        public string FactoryInDate { get; set; }
        public string FactoryOutDate { get; set; }
        public string DwellHours { get; set; }
        public string DwellDays { get; set; }
        public string CHAID { get; set; }
        public string CHAName { get; set; }
        public string shipperID { get; set; }
        public string shippername { get; set; }
        public string AGID { get; set; }
        public string AGName { get; set; }
        public string entryID { get; set; }
        public string MovementType { get; set; }
        public string SBNumber { get; set; }
        public string LineID { get; set; }
        public string SLName { get; set; }
        public string Port { get; set; }
        public string PickupLocID { get; set; }
        public string StuffLocID { get; set; }
        public string NoOfStuffLoc { get; set; }
        public string CargoDesc { get; set; }
        public string commodityid { get; set; }
    }


    public class ShippingBIllDetailsForExportProforma
    {
        public string SBNumber { get; set; }
        public string SBDate { get; set; }
        public string CartingDate { get; set; }
        public string StuffingDate { get; set; }
        public string TotalDays { get; set; }
        public string CartingQty { get; set; }
        public string CartingWeight { get; set; }
        public string Area { get; set; }
        public string Space { get; set; }
        public string CargoDescriptions { get; set; }
        public string VehicleNo { get; set; }
        public string entryid { get; set; }
        public string CargoWeight { get; set; }
        public string TotalPKGS { get; set; }
        
    }
    public class TariffDetailsForExport
    {
        public string TariffID { get; set; }
        public string TariffDescription { get; set; }
        

    }
}
