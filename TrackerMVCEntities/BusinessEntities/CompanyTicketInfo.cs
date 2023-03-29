using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class CompanyTicketInfo
    {
        public IEnumerable<DataRow> Rows;

        public int ID { get; set; }
        public int SrNo { get; set; }

        public int TicketNumber { get; set; }

        public int CompanyID { get; set; }

        public string Description { get; set; }

        public byte[] Attachment { get; set; }
        public string MailID { get; set; }
        public int ContentType { get; set; }
        public string Subject { get; set; }

        public Boolean MailSent { get; set; }

        public DateTime AddedDate { get; set; }

     
    }
}
