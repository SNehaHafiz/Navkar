using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJson
{
    public class master
    {
        public master()
        {
            declaration = new Declaration();
            location = new Location();
            cargoContainer = new List<CargoContainer>();
        }
        public Declaration declaration { get; set; }
        public Location location { get; set; }
        public List<CargoContainer> cargoContainer { get; set; }
    }
}
