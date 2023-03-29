using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class OperatorMaster
    {
        public int OperatorID { get; set; }
        public string OperatorName { get; set; }
        public string Code { get; set; }
        public int AddedBy { get; set; }
    }
}
