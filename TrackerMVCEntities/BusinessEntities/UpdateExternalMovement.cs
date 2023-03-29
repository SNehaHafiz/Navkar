using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class SaveUpdateMovementEntities
    {
        public string fromid { get; set; }
        public string toid { get; set; }
        public string Size { get; set; }
        public int Sizeid { get; set; }
        public string Type { get; set; }
        public int TypeID { get; set; }
        public string Reamrks { get; set; }
        public string Vehicle { get; set; }
        public string MovementDate { get; set; }
        public string ContainerNo { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string jono { get; set; }
        public string ShipmentNo { get; set; }

    }

    public class UpdateExternalMovement
    {
        public string ID { get; set; }
        public string Criteria { get; set; }
        public string containerno { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string vehicleno { get; set; }
        public string status { get; set; }
        public int SrNo { get; set; }
        public string ContainerType { get; set; }
        public string ActivityDate { get; set; }
        public string Activity { get; set; }

    }

    public class UpdateContainerMovementEntities
    {
        public string fromid { get; set; }
        public string toid { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string ContainerNo { get; set; }
        public string ShipmentNo { get; set; }

    }
    //Code By Rahul
    public class ExternalMovementReport
    {
        public int SrNo { get; set; }
        public string Category { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string OutDate { get; set; }
        public string Location { get; set; }
        public string To { get; set; }
        public string Vehicle { get; set; }
        public string Line { get; set; }
        public string Transporter { get; set; }
        public string VesselName { get; set; }
        public string NavkarBillNo { get; set; }
        public string NavkarBillAmount { get; set; }
        public string BillNumbers { get; set; }
        public string TransporterAmt { get; set; }
        public string GateInType { get; set; }

        public ExternalMovementReport()
        {
            LineListExt = new List<LineListExt>();
            TransporterListExt = new List<TransporterListExt>();
        }
        public List<LineListExt> LineListExt { get; set; }
        public List<TransporterListExt> TransporterListExt { get; set; }
    }
    public class LineListExt
    {
        public int LineID { get; set; }

        public string LineName { get; set; }
    }
    public class TransporterListExt
    {
        public int TransID { get; set; }

        public string TransName { get; set; }
    }
    //Code end by rahul

  
}
