using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ProformaEntities
    {
        public ProformaEntities()
        {
            DeliveryProformaEntities = new List<DeliveryProformaEntities>();
            InvoiceProformaEntities = new List<InvoiceProformaEntities>();
            CustomerProformaEntities = new List<CustomerProformaEntities>();
            CHAProformaEntities = new List<CHAProformaEntities>();
            ImporterProformaEntities = new List<ImporterProformaEntities>();
            MovementProformaEntities = new List<MovementProformaEntities>();
            CommodityProformaEntities = new List<CommodityProformaEntities>();
            HaulageProformaEntities = new List<HaulageProformaEntities>();
            YardLocationProformaEntities = new List<YardLocationProformaEntities>();
            TariffForProformaEntities = new List<TariffForProformaEntities>();
            TariffProformaEntities = new List<TariffProformaEntities>();
            AccountProformaEntities = new List<AccountProformaEntities>();
            ShippingLineProformaEntities = new List<ShippingLineProformaEntities>();
            ImportInvoiceContainerDetails = new List<ImportInvoiceContainerDetails>();
            ContainerAmountSHow = new List<ContainerAmountSHow>();
            ImportListGetWeight = new ImportListGetWeight();
            ImportListArea = new ImportListArea();
            ImportListMsg = new ImportListMsg();
            TextBoXValuesForImportPerforma = new TextBoXValuesForImportPerforma();
            Sealcuttingdetails = new Sealcuttingdetails();
            ImportProformaSearchGST = new ImportProformaSearchGST();
        }
        public List<DeliveryProformaEntities> DeliveryProformaEntities = new List<DeliveryProformaEntities>();
        public List<InvoiceProformaEntities> InvoiceProformaEntities = new List<InvoiceProformaEntities>();
        public List<CustomerProformaEntities> CustomerProformaEntities = new List<CustomerProformaEntities>();
        public List<CHAProformaEntities> CHAProformaEntities = new List<CHAProformaEntities>();
        public List<ImporterProformaEntities> ImporterProformaEntities = new List<ImporterProformaEntities>();
        public List<MovementProformaEntities> MovementProformaEntities = new List<MovementProformaEntities>();
        public List<CommodityProformaEntities> CommodityProformaEntities = new List<CommodityProformaEntities>();
        public List<HaulageProformaEntities> HaulageProformaEntities = new List<HaulageProformaEntities>();
        public List<YardLocationProformaEntities> YardLocationProformaEntities = new List<YardLocationProformaEntities>();
        public List<TariffForProformaEntities> TariffForProformaEntities = new List<TariffForProformaEntities>();
        public List<TariffProformaEntities> TariffProformaEntities = new List<TariffProformaEntities>();
        public List<AccountProformaEntities> AccountProformaEntities = new List<AccountProformaEntities>();
        public List<ShippingLineProformaEntities> ShippingLineProformaEntities = new List<ShippingLineProformaEntities>();
        public List<ImportInvoiceContainerDetails> ImportInvoiceContainerDetails = new List<ImportInvoiceContainerDetails>();
        public List<ContainerAmountSHow> ContainerAmountSHow = new List<ContainerAmountSHow>();
        public ImportListGetWeight ImportListGetWeight = new ImportListGetWeight();
        public ImportListArea ImportListArea = new ImportListArea();
        public ImportListMsg ImportListMsg = new ImportListMsg();
        public TextBoXValuesForImportPerforma TextBoXValuesForImportPerforma = new TextBoXValuesForImportPerforma();
        public Sealcuttingdetails Sealcuttingdetails = new Sealcuttingdetails();
        public ImportProformaSearchGST ImportProformaSearchGST = new ImportProformaSearchGST();
    }

    public class DeliveryProformaEntities
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class InvoiceProformaEntities
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class CustomerProformaEntities
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class ShippingLineProformaEntities
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class CHAProformaEntities
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class ImporterProformaEntities
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class MovementProformaEntities
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class CommodityProformaEntities
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class HaulageProformaEntities
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class YardLocationProformaEntities
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class TariffForProformaEntities
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class TariffProformaEntities
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class AccountProformaEntities
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class ImportProformaSearchGST
    {
        public string GSTID { get; set; }
        public string GSTIn_uniqID { get; set; }
        public string state_Code { get; set; }
        public string GSTName { get; set; }
        public string GSTAddress { get; set; }
        public string State { get; set; }
        public string TariffIDSaved { get; set; }
        public string FreeDaysSaved { get; set; }
        public string RegType { get; set; }
        public string FromDate { get; set; }
        public string Todate { get; set; }
        public string TariffID { get; set; }
    }


    public class ImportInvoiceContainerDetails
    {
        public string JONo { get; set; }
        public string ContainerNo { get; set; }
        public string Tare_Weight { get; set; }
        public string Size { get; set; }
        public string ISOCODE { get; set; }
        public string ContainerType { get; set; }
        public string InDate { get; set; }
        public string ScanType { get; set; }
        public int IsScan { get; set; }
        public string Cargotype { get; set; }
        public string Consignee { get; set; }
        public string IGM_BLNo { get; set; }
        public string AGName { get; set; }
        public int AgentID { get; set; }
        public double IGM_MT_Wt { get; set; }
        public string arrival { get; set; }
        public int slid { get; set; }
        public string slname { get; set; }
        public string Haulage_Type { get; set; }
        public string Commodity_Group_Name { get; set; }
        public string PORTNAME { get; set; }
        public int ImporterID { get; set; }
        public string ImporterName { get; set; }
        public string Haulage_Type_Id { get; set; }
        public string jotypeid { get; set; }
        public string commodityid { get; set; }
        public int transid { get; set; }
        public string Jo_Type { get; set; }
        public string IGM_Qty { get; set; }
        public string Amount { get; set; }
        public string RMS { get; set; }
        public string grossweight { get; set; }
        public string Cargoweight { get; set; }
        public string VOucherno { get; set; }
        public string Voucherdate { get; set; }
        public string TrailerName { get; set; }
        public string TransName { get; set; }
        public string Activity { get; set; }
        public string Fromlocation { get; set; }
        public string Tolocation { get; set; }
        public string DriverName { get; set; }
        public string Frlocid { get; set; }
        public string ToLocid { get; set; }
        public string lineID { get; set; }


    }
    public class ImportListGetWeight
    {
        public string Weight { get; set; }
        public string PKGS { get; set; }
     
    }
    public class ImportListArea
    {
        public string Area { get; set; }
        public string DestuffDate { get; set; }

    }
    public class ImportListMsg
    {
        public string Response { get; set; }
        public string Continues { get; set; }

    }


    public class ContainerAmountSHow
    {
        public string AccountName { get; set; }
        public string ContainerNo { get; set; }
        public string NetAmount { get; set; }

    }

    public class TextBoXValuesForImportPerforma
    {
        public double Amount { get; set; }
        public double SGST { get; set; }
        public double CGST { get; set; }
        public double IGST { get; set; }
        public double nettotal { get; set; }
        public string Remarks { get; set; }

    }
    public class Sealcuttingdetails
    {
        public string BOENO { get; set; }
        public string BOEDATE { get; set; }
        public string CHAID { get; set; }
        public string value { get; set; }
        public string duty { get; set; }
        public string DeliveryType { get; set; }

    }

    public class additionalAccountDetails
    {
        public string AccountNameAdditional { get; set; }
        public string ContainernoAdditional { get; set; }
        public string AmountAdditional { get; set; }
        public string Accountadditional { get; set; }
        

    }

}
