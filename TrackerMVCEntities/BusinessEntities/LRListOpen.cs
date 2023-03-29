using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class LRListOpen
    {
        public int  LRNo { get; set; }      
        public string LRDate { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string VehicleNo { get; set; }
        public string Customer { get; set; }
        public string LineName { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string DwellDays { get; set; }
        public string PreparedBy { get; set; }
        public int DocID { get; set; }
        public string DocName { get; set; }
        public string FactoryReportingTime { get; set; }
        public string FactoryInTime { get; set; }
        public string FactoryOutTime { get; set; }
        public string CloseRemarks { get; set; }
        public string LRNoFile { get; set; }
        

        public LRListOpen()
        {
            DocList = new List<DocList>();
        }
        public List<DocList> DocList { get; set; }        
    }
    public class DocList
    {
        public int DocID { get; set; }

        public string DocName { get; set; }
    }
}