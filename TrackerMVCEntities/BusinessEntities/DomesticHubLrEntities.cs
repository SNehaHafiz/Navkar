using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class DomesticHubLrEntities
    {
        public int ContainerTypeID { get; set; }
        public string ContainerType { get; set; }
        public int Size { get; set; }
        public int LineID { get; set; }
        public string LineName { get; set; }
        public string ContainerNo { get; set; }
        public int AddedBy { get; set; }
        public int ENTRYID { get; set; }
        public string LrDate { get; set; }
    }


}
