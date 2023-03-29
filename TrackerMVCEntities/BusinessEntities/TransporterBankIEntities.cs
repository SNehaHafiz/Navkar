using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class TransporterBankIEntities
    {
        public TransporterBankIEntities()
        {


            Banklist = new List<TransportEntities>();
        }
        public List<TransportEntities> Banklist { get; set; }
        
    }
}
