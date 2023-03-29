using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class Audit_Invoice_Note
    {
        public int SrNo { get; set; }
        public int NoteID { get; set; }
        public DateTime NoteDate { get; set; }
        public string InvoivceNo { get; set; }
        public string NoteDesc { get; set; }
        public int NoteBy { get; set; }
        public DateTime NoteOn { get; set; }
        public int CreditCategoryID { get; set; }
        public string CreditCategoryType { get; set; }
    }
}
