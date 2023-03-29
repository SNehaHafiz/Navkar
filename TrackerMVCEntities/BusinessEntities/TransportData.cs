using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class TransportData
    {

        public TransportData()
            {

                TransportList = new TransportEntities();
                Banklist = new List<BankEntities>();
            }
            public TransportEntities TransportList { get; set; }
            public List<BankEntities> Banklist { get; set; }
        
    }
}
