using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class AmmendmentlogEntities
    {
        public long RunningNo { get; set; }

        public string ChangesDesc { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string LoginIP { get; set; }
        public string sentence { get; set; }
        public int srno { get; set; }
    }
}
