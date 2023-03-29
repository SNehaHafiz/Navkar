using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class PortWiseTuesReport
    {

       public int Srno { get; set; }

       public string day { get; set; }
        public string Activity { get; set; }

        public string BMCT { get; set; }

        public string GTI { get; set; }

        public string NSICT { get; set; }

        public string NSIGT { get; set; }

        public string HAZIRA { get; set; }

        public string NSA { get; set; }

        public string JNPT { get; set; }

        public string GrandTotal { get; set; }
    }
}
