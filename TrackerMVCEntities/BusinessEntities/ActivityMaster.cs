using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ActivityMaster
    {
        public int  AutoID { get; set; }
      
        public string Activity { get; set; }

        public string TYPEID { get; set; }
        public int ID { get; set; }
        public string TYPE { get; set; }
        public int ActivityID { get; set; }
        public string SBNo { get; set; }
        public string PartyName { get; set; }
        public string Trailerno { get; set; }
        public string Remarks { get; set; }

    }
}
