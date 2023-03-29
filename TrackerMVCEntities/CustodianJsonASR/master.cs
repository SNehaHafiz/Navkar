using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonASR
{
    public class Master
    {
        public Master()
        {
            declaration = new Declaration();
            location = new Location();
            cargoDetails = new List<CargoDetails>();
        }
        public Declaration declaration { get; set; }
        public Location location { get; set; }
        public List<CargoDetails> cargoDetails { get; set; }
    }
}
