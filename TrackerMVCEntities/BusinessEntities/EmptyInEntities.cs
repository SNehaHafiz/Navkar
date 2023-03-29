using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class EmptyInEntities
    {
        public string ISOCODE  { get; set; }
        public string Location { get; set; }
        public string ContainerNo { get; set; }
        public string GrossWeight { get; set; }
        public string Size { get; set; }
        public string Payload { get; set; }
        public string tareWeight { get; set; }
        public string Containertype { get; set; }
        public string Tranname { get; set; }
        public string Condition1 { get; set; }
        public string Movementtype { get; set; }
        public string ShipLine { get; set; }
        public string Customer1 { get; set; }
        public string Source { get; set; }
        public string sealNo { get; set; }
        public string Remarks { get; set; }
        public string allowid { get; set; }
        public string BookingNo { get; set; }
        public string TransID { get; set; }
        public string Custid { get; set; }
        public string Lineid { get; set; }
        public string LocationID { get; set; }
        public string ContainertypeID { get; set; }
        public string FactoryOut { get; set; }
        public string Entrytype { get; set; }
        public string TrailerNo { get; set; }
        public string DriverNo { get; set; }
        public string AgentSealNO { get; set; }
        public string ReportingTime { get; set; }
        public string FactoryInTime { get; set; }
        public string ISOIDCode { get; set; }
        public string ISOCODEID { get; set; }
     


       


        public EmptyInEntities()
        {
            ISOCodesGateIn = new List<ISOCodesGateIn>();
            Customer = new List<Customer>();
            ShipLines = new List<ShipLines>();
            ContainerTypeGateIn = new List<ContainerTypeGateIn>();
            TransporterGateIn = new List<TransporterGateIn>();
            LocationGateIn = new List<LocationGateIn>();
            Condition = new List<Condition>();
            MovementType = new List<MovementTypeEmptyIn>();
            SizeEmpty = new List<SizeEmptyIn>();
        }
        public List<ISOCodesGateIn> ISOCodesGateIn = new List<ISOCodesGateIn>();
        public List<Customer> Customer = new List<Customer>();
        public List<ShipLines> ShipLines = new List<ShipLines>();
        public List<Condition> Condition = new List<Condition>();
        public List<MovementTypeEmptyIn> MovementType = new List<MovementTypeEmptyIn>();
        public List<SizeEmptyIn> SizeEmpty = new List<SizeEmptyIn>();

        public List<ContainerTypeGateIn> ContainerTypeGateIn = new List<ContainerTypeGateIn>();
        public List<TransporterGateIn> TransporterGateIn = new List<TransporterGateIn>();

        public List<LocationGateIn> LocationGateIn = new List<LocationGateIn>();
    }

    public class Condition
    {
        public int ConditionID { get; set; }
        public string ConditionName { get; set; }
    }
    public class MovementTypeEmptyIn
    {
    
        public string MovementType { get; set; }
    }
    public class SizeEmptyIn
    {
     
        public string SizeEmpty { get; set; }
    }
}
