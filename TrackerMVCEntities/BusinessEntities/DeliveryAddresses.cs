using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class DeliveryAddresses
    {
        public int LocationID { get; set; }

        public string Location { get; set; }

        public long ID { get; set; }

        public string Address { get; set; }

        public int IsCopy { get; set; }

        public long common_id { get; set; }

        public long name { get; set; }

        public int UserID { get; set; }

        public bool IsDuplicate { get; set; }
    }
}
