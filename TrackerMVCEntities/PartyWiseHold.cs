using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class PartyWiseHold
    {
        public int  HoldID { get; set; }
        public string HoldDate { get; set; }
        public string Activity { get; set; }
        public string Hold_To { get; set; }
        public int Hold_TO_ID { get; set; }
        public string Hold_Reason { get; set; }
        public string HoldReamrks { get; set; }
        public int HoldedBy { get; set; }
        public string HoldedOn { get; set; }
        public int ReleasedBy { get; set; }
        public string ReleasedOn { get; set; }
        public string ReleasedRemarks { get; set; }
        public bool IsReleased { get; set; }
        public string Hold_To_Name { get; set; }
        public string DisplayHoldedBy { get; set; }
         public string ReleaseBy { get; set; }

    }
}
