using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class MasterEntities
    {
        public Int64 AGID { get; set; }
        public string AGaID { get; set; }
        public string AGName { get; set; }
        public string AGAddress { get; set; }
        public string AGCity { get; set; }
        public string AGAuthPerson { get; set; }
        public string AGAuthPersonDesig { get; set; }
        public string AGTelI { get; set; }
        public string AGTelII { get; set; }
        public string AGFax { get; set; }
        public string AGCellNo { get; set; }
        public string AGEMail { get; set; }
        public string AGWebsite { get; set; }
        public int CreditPeriod { get; set; }
        public string AGRemarks { get; set; }
        public bool IsActive { get; set; }
        public int Addedby { get; set; }
        public string AGGrade { get; set; }
        public bool IsContract { get; set; }

        public Boolean CHA { get; set; }
        public Boolean shippers { get; set; }
        public Boolean ShipLines { get; set; }
        public Boolean Importer { get; set; }
        public Boolean Customer { get; set; }
        public string TallyLedger { get; set; }
        public Boolean JV { get; set; }
        public Boolean Vendor { get; set; }
    }
}
