using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class LineGraphValue
    {
        public int Year { get; set; }
        public string MonthName { get; set; }
        public int MonthNo { get; set; }
        public int Total { get; set; }
        public string XAxis { get; set; }
    }
}
