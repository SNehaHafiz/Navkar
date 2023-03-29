using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class Trolleytransport
    {
        public string EntryDate { get; set; }
        public string TrolleyNumber { get; set; }
        public string InstalledTyres { get; set; }
        public string TrolleyType { get; set; }
        public string size { get; set; }

        public int ID { get; set; }
        public string Weight { get; set; }
        public int ContainerSizeID { get; set; }
        public string AddedBy { get; set; }
        public string XLType { get; set; }
        public string horstyres { get; set; }
        public string horseno { get; set; }
        public string Status { get; set; }
        public string GPSst { get; set; }
        public string TrailerType { get; set; }
        public Trolleytransport()
        {
            TransporterList = new List<TransportTrolleyEntities>();
            VehicleTypeList = new List<VehicleTypeIEntities>();
        }
        public List<TransportTrolleyEntities> TransporterList { get; set; }

        public List<VehicleTypeIEntities> VehicleTypeList { get; set; }
    }
    public class TransportTrolleyEntities
    {
        public int ID { get; set; }

        public string trailerType { get; set; }
    }

    public class VehicleTypeIEntities
    {
        public int ContainerSizeID { get; set; }

        public string ContainerSize { get; set; }
    }
}
