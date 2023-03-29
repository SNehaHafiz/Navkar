using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class EyardOut
    {
        public int EntryID { get; set; }
        public string MovementBy { get; set; }
        public string GatePassDate { get; set; }
        public string MovementType { get; set; }
        public string TruckNo { get; set; }
        public string Transporter { get; set; }
        public string ContainerNo { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string ISOCode { get; set; }
        public string LineName { get; set; }
        public string Location { get; set; }
        public int LocationIID { get; set; }
        public string POD { get; set; }
        public string SealNo { get; set; }
        public string OutStatus { get; set; }
        public string DamageRemarks { get; set; }
        public string ShipperName { get; set; }
        public string VesselName { get; set; }
        public int VesselID { get; set; }
        public string ports { get; set; }
        public string BookingLineID { get; set; }
        public int Pkgs { get; set; }
        public string Commodity { get; set; }



        public int TypeID { get; set; }
        public int lineid { get; set; }
        public int custo { get; set; }
        public String txtForwarderLine { get; set; }
        public String BookingNo { get; set; }


        public int AutoID { get; set; }
        public string Activity { get; set; }

        public int SLID { get; set; }
        public string SLName { get; set; }

        public int AGID { get; set; }
        public string AGNAME { get; set; }

        public int LocationID { get; set; }
        public string Locations { get; set; }

        public int TransID { get; set; }
        public string TransName { get; set; }

        public int ContainerTypeID { get; set; }
        public string ContainerType { get; set; }

        public int cmbline { get; set; }
        public int cmbagent { get; set; }
        public int cmbfrom { get; set; }
        public int cmbto { get; set; }


        public string cmbActivity { get; set; }
        public string cmbsize { get; set; }
        public string cmbsize1 { get; set; }
        public string cmbtype { get; set; }
        public string txtagentseal { get; set; }

        public string txtboeno { get; set; }
        public string txtwt { get; set; }
        public string txtconsignee { get; set; }
        public string txttareWt { get; set; }

        public int FromLocation { get; set; }
        public int ToLocation { get; set; }
        public string Weight { get; set; }
        public int TareWeight { get; set; }
        public string AgentSeal { get; set; }
        public string TrailerNo { get; set; }
        public string BOENo { get; set; }
        public string Types { get; set; }
        public string Remarks { get; set; }
        public string BatchPkgs { get; set; }
        public string ComInvoiceNo { get; set; }
        public string EWayBillNo { get; set; }
        public string CoilSizeDescriptions { get; set; }
        public string TrainNo { get; set; }
    }

}
