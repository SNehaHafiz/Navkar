using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class StateMaster
    {
        public long State_ID { get; set; }

        public string State_Code { get; set; }

        public string State { get; set; }

        public int AddedBy { get; set; }

        public DateTime AddedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public char TIN_Number { get; set; }




    }
}
