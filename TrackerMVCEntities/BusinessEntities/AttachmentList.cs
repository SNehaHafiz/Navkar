using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class AttachmentList
    {
        public int SrNo { get; set; }
        public int AttachAutoID { get; set; }
        public int runningno { get; set; }
        public int ParameterID { get; set; }
        public int DeptID { get; set; }
        public string Type { get; set; }
        public string MSNoFile { get; set; }
        public int DocID { get; set; }
        public string DocumentName { get; set; }
        public string DocName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string DocType { get; set; }
        public string UploadFor { get; set; }
        public string url { get; set; }
        public AttachmentList()
        {
            DocList = new List<DocList1>();
        }
        public List<DocList1> DocList { get; set; }
    }
    public class DocList1
    {
        public int DocID { get; set; }

        public string DocName { get; set; }
    }
}
