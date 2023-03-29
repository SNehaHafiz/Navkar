using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class FastagMasterEntities
    {
        public int entryid { get; set; }
        public string  Entrydate { get; set; }
        public string  VehicleNo { get; set; }
        public string  tagID { get; set; }
        public string  Remarks { get; set; }
        public string  UserName { get; set; }
        public int  isactive { get; set; }
        public string Fromid { get; set; }
        public string Toid { get; set; }
        public string Amount { get; set; }
        public string toll { get; set; }
        public int Fromlocationid { get; set; }
        public int tolocationid { get; set; }
        public int tollid { get; set; }
        public string passing { get; set; }
        public string Status { get; set; }
    }
}
