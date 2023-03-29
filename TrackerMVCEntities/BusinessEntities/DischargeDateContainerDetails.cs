using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class DischargeDateContainerDetails
    {
        public int JONO { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }

        public int SrNo { get; set; }
        public bool IsSelected { get; set; }
        public string DischargeDate { get; set; }
        public string port { get; set; }
        public int Sizeid { get; set; }
        public int TypeID { get; set; }
        //public int portID { get; set; }
        //public string LineName { get; set; }
        public int lineid { get; set; }
        public string vesselName { get; set; }
        //public int vesselid { get; set; }

        public string Type { get; set; }
        //public int TypeID { get; set; }
        public string From { get; set; }
        public int fromid { get; set; }

        public string To { get; set; }
       public int Toid { get; set; }
        public string JoDate { get; set; }
        public string ShipmentNo { get; set; }
        public string VIANo { get; set; }
        public string CutoffDate { get; set; }
        public string POL { get; set; }
        public string Criteria { get; set; }
        public int Size20 { get; set; }
        public int Size40 { get; set; }
        public int Size45 { get; set; }
        public int Total { get; set; }
        public string VehicleNo { get; set; }
        public string JoNumber { get; set; }

        public DischargeDateContainerDetails()
        {
            //Narration = "";
            portList = new List<portEntities>();
            lineList = new List<lineEntites>();
            VesseslList = new List<VesselEntities>();
            TypeList = new List<TypeEntities>();
            FromList = new List<FromEntities>();
            ToList = new List<ToEntities>();
            SizeList = new List<SizeEntities>();

        }
        public List<portEntities> portList { get; set; }
        public List<lineEntites> lineList { get; set; }
        public List<VesselEntities> VesseslList { get; set; }
        public List<TypeEntities> TypeList { get; set; }
        public List<FromEntities> FromList { get; set; }
        public List<ToEntities> ToList { get; set; }
        public List<SizeEntities> SizeList { get; set; }

    }


    public class portEntities
    {
        public string ports { get; set; }
        public int portID { get; set; }
    }
    public class lineEntites
    {
        public string LineName { get; set; }
        public int lineid { get; set; }
    }
    public class VesselEntities
    {
        public string vesselName { get; set; }
        public int vesselid { get; set; }
    }
    public class TypeEntities
    {
        public string Type { get; set; }
        public int TypeID { get; set; }
    }
    public class FromEntities
    {
        public string From { get; set; }
        public int fromid { get; set; }
    }
    public class ToEntities
    {
        public string To { get; set; }
        public int Toid { get; set; }
    }

    public class SizeEntities
    {
        public string Sizec { get; set; }
        public int Sizeid { get; set; }
    }

}
