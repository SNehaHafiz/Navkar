using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class JobOrderAttachmentEntities
    {
        public long RunningID { get; set; }

        public long JONO { get; set; }

        public string DocFileName { get; set; }

        public byte[] FileDets { get; set; }

        public byte[] Data { get; set; }

        public string ContentType { get; set; }
        public int UniqueID { get; set; }
        public int srno { get; set; }
    }
}
