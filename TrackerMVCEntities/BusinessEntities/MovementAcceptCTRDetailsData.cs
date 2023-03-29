using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class MovementAcceptCTRDetailsData
    {
        public MovementAcceptCTRDetailsData()
        {
            MovementAcceptCTRData = new MovementAcceptCTR();
            MovementAccCTRItemList = new List<MovementAcceptCTR>();


        }
        public MovementAcceptCTR MovementAcceptCTRData { get; set; }
        public List<MovementAcceptCTR> MovementAccCTRItemList { get; set; }
    }
}
