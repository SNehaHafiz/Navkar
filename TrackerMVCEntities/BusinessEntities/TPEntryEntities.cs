using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class TPEntryEntities
   {
       public string trailerno { get; set; }
       public string TPdate { get; set; }
       public string trailername { get; set; }
       public string drivername { get; set; }
       public string startdate { get; set; }
       public string enddate { get; set; }
       public string amount { get; set; }

       public string TPLocation { get; set; }

       public string TPfor { get; set; }
        public string TPNumber { get; set; }
        public string AmountWords { get; set; }
        public byte[] Attachment { get; set; }
        public string ContentType { get; set; }
        public string TPNO { get; set; }
        public string ISApproved { get; set; }
        public string AddedBy { get; set; }
        
    }
}
