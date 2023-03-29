using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class DriverAttachment
    {
        public int SrNo { get; set; }
        public string MSNoFile { get; set; }
        public int DocID { get; set; }
        public string DocName { get; set; }
        public string FilePath { get; set; }
        public string DocumentType { get; set; }
        public string DocumenttypeID { get; set; }

        public DriverAttachment()
        {
            DocList11 = new List<DocList11>();
        }
        public List<DocList11> DocList11 { get; set; }
    }
    public class DocList11
    {
        public int DocID { get; set; }

        public string DocName { get; set; }
    }

}
