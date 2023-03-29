using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class Ext_location_Master
    {

        public int LocationID { get; set; }

        public string Location { get; set; }

        public int addedby { get; set; }

        public DateTime addedon { get; set; }

        public bool isactive { get; set; }

        public float Km { get; set; }

        public long GSTID { get; set; }
    }
}

