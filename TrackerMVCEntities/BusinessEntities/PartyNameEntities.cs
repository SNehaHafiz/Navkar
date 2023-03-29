using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class PartyNameEntities
    {
        public int Common_ID  { get; set; }
        public string GSTName { get; set; }

        public int transporterID { get; set; }
        public string vendorname { get; set; }
    }

    public class BillVerificationTransport
    {
        public int transporterID { get; set; }
        public string vendorname { get; set; }
    }
}
