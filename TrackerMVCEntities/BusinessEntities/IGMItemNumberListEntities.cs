using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class IGMItemNumberListEntities
    {

        public IGMItemNumberListEntities()
        {

            IGMData = new IGMItemsNumber();
            IGNList = new List<IGMItemsNumber>();
        }
        public IGMItemsNumber IGMData { get; set; }
        public List<IGMItemsNumber> IGNList { get; set; }
    }
}
