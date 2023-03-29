using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class DomesticHubOffLoadDts
    {
        public DateTime Entrydate { get; set; }
        public string ContainerNo { get; set; }
        public string LRNo { get; set; }
        public int EntryID { get; set; }
        public int Size { get; set; }
        public string TrailerNo { get; set; }
        public int LocationID { get; set; }
        public string Remarks { get; set; }
        public int AddedBy { get; set; }
        public int AddedOn { get; set; }
        public int IsCancel { get; set; }
        public int CancelledBy { get; set; }
        public int CancelledOn { get; set; }
    }
}
