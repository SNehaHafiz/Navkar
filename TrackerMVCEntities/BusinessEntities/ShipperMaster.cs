using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ShipperMaster
    {
        public int ShipperID { get; set; }
        public string ShipperIECNO { get; set; }
        public string ShipperName { get; set; }
        public string ShipperAddress { get; set; }
        public string ShipperCity { get; set; }
        public string ShipperContactPerson { get; set; }
        public string ShipperPersonDesig { get; set; }
        public string ShipperContactNO1 { get; set; }
        public string ShipperContatcNO2 { get; set; }
        public string ShipperFax { get; set; }
        public string ShipperCellNo { get; set; }
        public string ShipperEMail { get; set; } 
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public string AddedBY { get; set; }
        public string ModifiedBY { get; set; }
    }
}
