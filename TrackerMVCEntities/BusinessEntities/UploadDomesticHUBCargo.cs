using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class UploadDomesticHUBCargo
    {
        public int SrNo { get; set; }
        public string WagonNo { get; set; }
        public string ContainerNo { get; set; }
        public int Size { get; set; }
        public string DisplaySize { get; set; }
        public string Stats { get; set; }
        public string Stack { get; set; }
        public string TriffType { get; set; }
        public string SealNo { get; set; }
        public string LRNo { get; set; }
        public decimal TareWt { get; set; }
        public string DisplayTareWt { get; set; }
        public decimal CargoWt { get; set; }
        public string DisplayCargoWt { get; set; }
        public string Destination { get; set; }
        public string OrderNo { get; set; }
        public string Shipment { get; set; }
        public string DES { get; set; }
        public string Grade { get; set; }
        public string TaxInvNo { get; set; }
        public DateTime TaxInvDate { get; set; }
        public string DisplayTaxInvDate { get; set; }
        public string DeliveryNo { get; set; }
        public string DCPINo { get; set; }
        public string EWayBillNo { get; set; }
        public DateTime EWayBillValidUpTo { get; set; }
        public string DisplayEWayBillValidUpTo { get; set; }
        public string ShipToParty { get; set; }
        public int EntryId { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
