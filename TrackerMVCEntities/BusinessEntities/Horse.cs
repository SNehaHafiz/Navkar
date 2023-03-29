using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class Horse
    {
        
        public string EntryDate { get; set; }
        public string HorseNumber { get; set; }
        public string InstalledTyres { get; set; }
        public string Weight { get; set; }
        public string Model { get; set; }
        public string AddedBy { get; set; }
    }
}
