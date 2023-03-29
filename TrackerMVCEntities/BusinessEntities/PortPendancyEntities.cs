using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class PortPendancyEntities
    {
        public double SrNo { get; set; }
        public string VesselName {get;set;}
        public string ViaNo {get;set;}
        public string ETA { get;set;}
        public string AGING { get;set;}
        public string TERMINAL { get;set;}
        public string BirthingDateandTime {get;set;}
        public string HBLNumber {get;set;}
        public string BLNumber {get;set;}
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string BLReceivedDate { get; set; }
        public string BLEntrydate {get;set;}
        public string ShippingLine { get;set;}
        public string ContainerNo { get; set; }
        public int Size { get;set;}
        public string Type { get; set; }
        public string ImporterName {get;set;}
        public string CHAName {get;set;}
        public string HaulageType {get;set;}
        public string CargoType {get;set;}
        public string Commodity { get; set; }
        public string ScanType { get; set; }        
        public string ModeOfTransport { get; set; }
        public string Remarks { get;set;}
        public string SMTPDate {get;set;}
        public string OutDateFromPort {get;set;}
        public string InTransit {get;set;}
        public string Teus { get; set; }
        
    }
}
