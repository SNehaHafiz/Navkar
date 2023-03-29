using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ShipLinesMaster
    {
        public int SLID { get; set; }
        public string SaID { get; set; }
        public string SLName { get; set; }
        public string SLAddressI { get; set; }
        public string SLCity { get; set; }
        public string SLAuthPerson { get; set; }
        public string SLAuthPersonDesig { get; set; }
        public string SLTelI { get; set; }
        public string SLTelII { get; set; }
        public string SLFax { get; set; }
        public string SLAuthPersonCell { get; set; }
        public string SLEMail { get; set; }
        public string Remarks { get; set; }
        public bool SLIsActive { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsContract { get; set; } 

    }
}
