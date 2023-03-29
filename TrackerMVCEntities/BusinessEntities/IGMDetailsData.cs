using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class IGMDetailsData
    {
        public IGMDetailsData()
        {
            QuoteData = new IGMDetailsEntities();
            QuoteSystemList = new List<IGMDetailsEntities>();

        }
        public IGMDetailsEntities QuoteData { get; set; }
       
        public List<IGMDetailsEntities> QuoteSystemList { get; set; }
    }
}
