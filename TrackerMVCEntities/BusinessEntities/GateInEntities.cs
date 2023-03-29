using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class GateInEntities
    {
        public bool IsScan { get; set; }
        public bool IsScan1 { get; set; }
        public bool LowBed { get; set; }
        public int Transporter { get; set; }
        public string TrailerNo { get; set; }
        public string ContainerNo { get; set; }
        public string JONo { get; set; }
        public string ISOCode { get; set; }
        public string CFSCode { get; set; }
        public string MAXCFSCode { get; set; }
        public string Line { get; set; }
        public string SealNo1 { get; set; }
        public string SealNo2 { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string ViaNo { get; set; }
        public string VesselName { get; set; }
        public string VoyageNo { get; set; }
        public string ContainerType { get; set; }
        public string PortName { get; set; }
        public string Weight { get; set; }
        public string Scan { get; set; }
        public string IsLowBed { get; set; }
        public string EIRDate { get; set; }
        public string InDate { get; set; }
        public string ScanMessage { get; set; }
        public string NoJOMessage { get; set; }
        public string IsTrainPlanMessage { get; set; }
        public string IsGateInMessage { get; set; }
        public string ContainerDigitCheck { get; set; }
        public string ExpCheckContainer { get; set; }
        public string IsManual { get; set; }
        public string TransportTypeID { get; set; }
        public string TareWt { get; set; }
        public string Where { get; set; }
        public string Condition { get; set; }
        public string Location { get; set; }
        public string WagonNo { get; set; }
        public string Remarks { get; set; }
        public string ContainerStatus { get; set; }
        public string DOCNo { get; set; }
        public string WeighmentWt { get; set; }
        public string CargoType { get; set; }
        public string CargoTypeID { get; set; }
        public string FL { get; set; }
        public int Count { get; set; }

        public GateInEntities()
        {
            ISOCodesGateIn = new List<ISOCodesGateIn>();
            PortGateIn = new List<PortGateIn>();
            ContainerTypeGateIn = new List<ContainerTypeGateIn>();
            TransporterGateIn = new List<TransporterGateIn>();
            TrailerTypeGateIn = new List<TrailerTypeGateIn>();
            LocationGateIn = new List<LocationGateIn>();
            CargoTypeGate = new List<CargoTypeGate>();
        }
        public List<ISOCodesGateIn> ISOCodesGateIn = new List<ISOCodesGateIn>();
        public List<PortGateIn> PortGateIn = new List<PortGateIn>();
        public List<ContainerTypeGateIn> ContainerTypeGateIn = new List<ContainerTypeGateIn>();
        public List<TransporterGateIn> TransporterGateIn = new List<TransporterGateIn>();
        public List<TrailerTypeGateIn> TrailerTypeGateIn = new List<TrailerTypeGateIn>();
        public List<LocationGateIn> LocationGateIn = new List<LocationGateIn>();
        public List<CargoTypeGate> CargoTypeGate = new List<CargoTypeGate>();
    }

    public class ISOCodesGateIn
    {
        public int ID { get; set; }
        public string ISOCode { get; set; }
    }
    public class PortGateIn
    {
        public int ID { get; set; }
        public string PortName { get; set; }
    }
    public class ContainerTypeGateIn
    {
        public int ID { get; set; }
        public string Type { get; set; }
    }
    public class TransporterGateIn
    {
        public int ID { get; set; }
        public string Transporter { get; set; }
    }
    public class TrailerTypeGateIn
    {
        public int ID { get; set; }
        public string TrailerType { get; set; }
    }
    public class LocationGateIn
    {
        public int ID { get; set; }
        public string Location { get; set; }
    }
    public class SearchContainerGateIn
    {
        public string JONo { get; set; }
        public string JODate { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
    }
    public class CargoTypeGate
    {
        public int Cargotypeid { get; set; }
        public string Cargotypes { get; set; }
    }

    public class HoldReasonsEnt
    {
        public string LineName { get; set; }
        public string Size { get; set; }
        public string Via { get; set; }
        public string VesselName { get; set; }
        public string InDate { get; set; }
        public string JONo { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string InProcess { get; set; }
    }

    public class FactorySBEntr
    {
        public string AGID { get; set; }
        public string CHAID { get; set; }
        public string EXPORTERID { get; set; }
        public string ConsigneeID { get; set; }
        public string SBNo { get; set; }
        public string SBDate { get; set; }
        public string FOBValue { get; set; }
        public string Unit { get; set; }
        public string INVNo { get; set; }
        public string INVDT { get; set; }
        public string TotalPKGS { get; set; }
        public string CargoWeight { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string TypID { get; set; }
        public string CargotypeID { get; set; }
        public string StuffQTY { get; set; }
        public string StuffWT { get; set; }
        public string TareWT { get; set; }
        public string PODID { get; set; }
        public string FPDID { get; set; }
        public string POLID { get; set; }
        public string CountryID { get; set; }
        public string Remarks { get; set; }
        public string lblContainerNo { get; set; }
        public string CargoDesc { get; set; }

    }


}
