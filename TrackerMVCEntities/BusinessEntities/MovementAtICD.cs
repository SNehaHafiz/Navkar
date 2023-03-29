using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class MovementAtICD
    {
         public string SrNo{get;set;}
        public string VesselName {get;set;}
        public string ViaNo { get; set; }
        public string ETA {get;set;}
        public string PortName { get;set;}        
        public string DischargeFromVessel {get;set;}
        public string HBL {get;set;}
        public string BLNumber { get; set; }
        public string BLDate {get;set;}
        public string BLReceivedDate { get; set; }
        public string ShipingLine {get;set;}
        public string Size { get; set; }
        public string TEUS { get; set; }
        public string ShipperName {get;set;}
        public string ImporterName {get;set;}
        public string CHA { get; set; }
        public string BillingParty { get; set; }
        public string Haulage { get; set; }
        public string Cargo { get; set; }
        public string IGM { get; set; }
        public string ModeOfTransport { get; set; }
        public string PortOfLoading { get; set; }
        public string Container { get; set; }
        public string ScannigData { get; set; }
        public string Remarks { get; set; }
        public string SMTPDate { get; set; }
        public string OutDate { get; set; }
        public string InTransit { get; set; }
        public string GateICDStatus { get; set; }        
        public string OutofChargeDate { get; set; }
        public string DocumentReceiveDate  { get; set; }
        public string LoadedGate { get; set; }
        public string GateOut { get; set; }
        public string ReachedtimeAtFactory { get; set; }
        public string OutFromFactoryDate { get; set; }
        public string DwellHours { get; set; }
        public string EmptyGateInTrailerNo { get; set; }
        public string EmptyGateInDate { get; set; }
        public string OffloadingYard { get; set; }
        public string EmptyGateOutDate { get; set; }
        public string EmptyGateOutTrailerNo { get; set; }        
        public string EmptyOffloadedYardName { get; set; }
        public string EmptyOffloadedDate { get; set; }
        public string EmptyValidity { get; set; }
        public string KAMTeam { get; set; }
        public string ManagementTeam { get; set; }
       
    }
}
