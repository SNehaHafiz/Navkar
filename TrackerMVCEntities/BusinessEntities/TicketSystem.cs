using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class TicketSystem
    {
        public TicketSystem()
        {
            Attachment = new List<AttachmentList>();

        }
        public List<AttachmentList> Attachment { get; set; }
        public string MailID { get; set; }
        public int ID { get; set; }
        public int SrNo { get; set; }
        public int TktType { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string TktNo { get; set; }
        public int StatusID { get; set; }
        public int IsApproved { get; set; }
        public string Message { get; set; }
        public string TktTypeName { get; set; }
        public string ClosedByName { get; set; }
        public string ClosedOn { get; set; }
        public int PendingSince { get; set; }
        public int AddedBy { get; set; }
        public string AddedOn { get; set; }
        public string UserName { get; set; }
        public int TotalTkt { get; set; }
        public int OpenTkt { get; set; }
        public int OpenNewReqTkt { get; set; }
        public int OpenAwaitApprTkt { get; set; }
        public int OpenApprTkt { get; set; }
        public int OpenBugTkt { get; set; }
        public int OpenCorrectionTkt { get; set; }
        public int OpenBugTktLessthan72 { get; set; }
        public int OpenBugTktGreaterthan72 { get; set; }
        public int ClosedTkt { get; set; }
        public int ClosedNewReqTkt { get; set; }
        public int ClosedBugTkt { get; set; }
        public int ClosedCorrectionTkt { get; set; }
        public string Comment { get; set; }
    }
}
