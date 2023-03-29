using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class ExportCartingAllowEntities
    {
        public int EntryID { get; set; }
        public string agency { get; set; }
        public string invoice { get; set; }
        public string sbentryid { get; set; }
        public string cha { get; set; }
        public string exporter { get; set; }
        public string cargodesc { get; set; }
        public string qty { get; set; }
        public string wt { get; set; }
        public string consignee { get; set; }
        public string cargotype { get; set; }
        public string stuffingtype { get; set; }
        public string sbdate { get; set; }
        public string SealNo { get; set; }
        public string bqty { get; set; }
        public float bqty1 { get; set; }
        public string Location { get; set; }
        public int LocationIID { get; set; }

        public string VehicleNo { get; set; }
        public string LocationName { get; set; }
        public string Area { get; set; }
        public string Space { get; set; }
        public string DelQty { get; set; }
        public string ALLOWwT { get; set; }
        public string SBNo { get; set; }
        public string SBEntryIDs { get; set; }
        public string InvoiceNo { get; set; }
        public string Unit { get; set; }

        public int VehicleTypeID { get; set; }
        public string VehicleType { get; set; }
        public string Select { get; set; }
        public string TruckNo { get; set; }
        public string SBQty { get; set; }
        public string SBWeight { get; set; }
        public string GateINQty { get; set; }
        public string GateINWt { get; set; }

        public string Description { get; set; }
        public string AgencyName { get; set; }

        public string Agid { get; set; }

        public int EnID { get; set; }

        public string EquipmentType { get; set; }


    }

    public class ExportCartingTallyEntities
    {
        public string Condition { get; set; }
        public string SBNo { get; set; }
        public int AllowID { get; set; }
        public int AllowIDS { get; set; }
        public string SBDate { get; set; }
        public string AgencyName { get; set; }
        public string CHACode { get; set; }
        public string Exporter { get; set; }
        public string ShippingLine { get; set; }
        public string Consignee { get; set; }
        public string CargoDescription { get; set; }
        public string CargoWt { get; set; }
        public string cargotype { get; set; }

        public string StuffingType { get; set; }
        public string QtY { get; set; }
        public string Wt { get; set; }

        public string BalanceQty { get; set; }
        public string InvoiceNo { get; set; }
        public string VendorName { get; set; }
        public string VehicleNo { get; set; }
        public string SBentryid { get; set; }
        public string GateInQty { get; set; }
        public string GateInWt { get; set; }
        public string RcvdQty { get; set; }
        public string RcvdWt { get; set; }
        public string Area { get; set; }
        public string EquipmentType { get; set; }
        public string Loaction { get; set; }
        public string AllotedSpace { get; set; }
        public string EquipmentTypeID { get; set; }
        public string Location { get; set; }
       
        public string SRNO { get; set; }
        public int VendorID { get; set; }
        public int Unit { get; set; }


    }
    public class CHAtable
    {
        public int CHAID { get; set; }
        public string CHAName { get; set; }
    }

    public class Agencytable
    {
        public int agid { get; set; }
        public string agname { get; set; }
    }

    public class Locationtable
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }

    public class Shippertable
    {
        public int ShipperID { get; set; }
        public string ShipperName { get; set; }
    }

    public class CargoTypes
    {
        public int Cargotypeid { get; set; }
        public string Cargotype { get; set; }
    }

    public class equipmentm
    {
        public int Id { get; set; }
        public string Equipment { get; set; }
    }
  public class ExportStuffingRequest
    {
        public int ShippinglineID { get; set; }
        public string ShippinglineName { get; set; }
        public int lblcontentryID { get; set; }
        public int lblagencyid { get; set; }
        public string agencyName { get; set; }
        public string txtSize { get; set; }
        public string  cmbcontainertype { get; set; }
        public string txttareweight { get; set; }
        public string cmbisocode { get; set; }

        public string cmbstuffingtype { get; set; }
        public string txtcargodesc { get; set; }
        public string txtcha { get; set; }
        public string txtagency { get; set; }
        public string lblsbentryid { get; set; }
        public int lblshipperid { get; set; }
        public string cmbcarotype { get; set; }
        public string txtshipper { get; set; }
        public string lblcagencyid { get; set; }
        public string txttotalpkgs { get; set; }

        public string txttotalwt { get; set; }
        public string txtconsignee { get; set; }
        
        public string dblqty { get; set; }
        public string dblTotalQty { get; set; }
        public string dblqtys { get; set; }

        public string txtvessel { get; set; }
        public string txtpol { get; set; }
        public string txtcutoffdate { get; set; }
        public string txtrotation { get; set; }
        public string lblvesselID { get; set; }
        public string txtvoyage { get; set; }
        public string ShipLinesList { get; set; }
        public string Vehicleno { get; set; }
        public string CargoDesc { get; set; }
    }

    public class ExportStuffingSave
    {
        public int sbentryid { get; set; }
        public string SBNo { get; set; }
        public int lblcontentryID { get; set; }
        public string ContainerNo { get; set; }
        public int Size { get; set; }
        public string ContainerType { get; set; }
        public string Unno { get; set; }
        public string Temp { get; set; }
        public string CargoDesc { get; set; }
        public int dblTotalQty { get; set; }
        public int TareWeight { get; set; }
        public string ddlFPD { get; set; }
        public string CargoType { get; set; }
        public string Classno { get; set; }
        public int lblshipperid { get; set; }
        public int EquipmentID { get; set; }
        public string ScanStatus { get; set; }
        public string NetTotalWeight { get; set; }
        public string DestuffQty { get; set; }
        public string Destuffweight { get; set; }
        public string TotalWeight { get; set; }
    }

    public class ExpCLP
    {
        public string lblContainerNoAgency { get; set; }
        public string lblContainerNoSize { get; set; }
        public string lblContainerNoentryID { get; set; }
        public string lblContainerNoISOCode { get; set; }
        public string lblContainerNocontainertype { get; set; }
        public string lblShippingBillNo { get; set; }


        public string lblAgencySB { get; set; }
        public string lblMType { get; set; }
        public string lblSBEntryID { get; set; }
        public string lblContainer { get; set; } 
    }

    public class WeightmentEntities
    {
        public string lblcfs { get; set; }
        public string txtvehicletarewt { get; set; }
        public string TrailerNo { get; set; }
        public string lblID { get; set; }
        public string cmbvehicletype { get; set; }
        public string cmbvehiclestetus { get; set; }
        public string cmbtranstype { get; set; }
        public string txtpayload { get; set; }
        public string txtgrosswt { get; set; }
        public string txtreceipt { get; set; }
        public string txtremarks { get; set; }
        public string txtslipNo { get; set; }
        public string txtparty { get; set; }
        public string txtnetwt { get; set; }
        public string txtgetweight { get; set; }
        public string txtcontainerno { get; set; }
        public string lblJONo { get; set; }
        public bool CBManually { get; set; }
        public string txtsize { get; set; }
        public string txttarewt { get; set; }
        public string cmbtyres { get; set; }
        public string cmbcontype { get; set; }
        public string lblContainerNo { get; set; }
        public string lblamount { get; set; }
        public string ddlCycle { get; set; }
        public string txtContainerNo2 { get; set; }
        public string txtTareWeight2 { get; set; }
        public string txtSize2 { get; set; }
        public bool chkIsLowVGN { get; set; }
        public string txtKGS { get; set; }
        public string strWorkYear { get; set; }

    }

    public class ExportStuffingTallly
    {
        public string dblCargoQty { get; set; }
        public string dblSQty { get; set; }
        public string txtvessell { get; set; }
        public string txtvia { get; set; }
        public string txtpod { get; set; }
        public string txtshipper { get; set; }

        public string SrNo { get; set; }
        public string SBNumber { get; set; }
        public string InvoiceNo { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string ISOCode { get; set; }
        public string CargoDesc { get; set; }
        public string CargoQty { get; set; }
        public string CargoWeight { get; set; }
        public string StuffedQty { get; set; }
        public string BLQty { get; set; }
        public string QtyUnit { get; set; }
        public string StuffedWeight { get; set; }
        public string BLWeight { get; set; }
        public string TareWeight { get; set; }
        public string AgentSeal { get; set; }
        public string CustomSeal { get; set; }
        public string SealNo { get; set; }
        public string SBEntryID { get; set; }
        public string EntryID { get; set; }
        


    }
}
