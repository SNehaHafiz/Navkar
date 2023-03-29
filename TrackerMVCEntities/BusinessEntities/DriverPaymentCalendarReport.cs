using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class DriverPaymentCalendarReport
    {
        public string Title { get; set; }
        public string Editable { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public string Start { get; set; }
        public int Type { get; set; }
    }
}
