using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
     
   public class InvoiceChangesRequestEntities
    {
      
        
        public string Criteria { get; set; }
        public string AssessNo { get; set; }
        public string WorkYear { get; set; }
        public string Remarks { get; set; }
        public string RequestBy { get; set; }
        public string Requeston { get; set; }
        public int Dwell { get; set; }
        public string Status { get; set; }
        public string InvoiceDate { get; set; }
        public string BillTo { get; set; }
        public string GrandTotal { get; set; }
        public long BillToID { get; set; }
        public string FilePath { get; set; }
        public string DocName { get; set; }
        public string ContentType { get; set; }
        public string DocFileName { get; set; }
        public int RunningID { get; set; }
    }
}
