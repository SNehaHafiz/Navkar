using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ImporterMaster
    {
        public int ImporterID { get; set; }
        public string ImporterCode { get; set; }
        public string ImporterName { get; set; }
        public string ImpAddress { get; set; }
        public string ImpCity { get; set; }
        public string ImpAuthPerson { get; set; }
        public string ImpAuthPersonDesig { get; set; }
        public string ImpTelI { get; set; }
        public string ImpTelII { get; set; }
        public string ImpFax { get; set; }
        public string ImpCellNo { get; set; }
        public string ImpEMail { get; set; }

        public string ImpPANNo { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }

        public int Addedby { get; set; }

        
    }
}
