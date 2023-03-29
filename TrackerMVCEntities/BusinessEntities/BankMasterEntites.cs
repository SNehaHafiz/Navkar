using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class BankMasterEntites
    {
        public int SRNo { get; set; }
        public string TransDate { get; set; }
        public string Description { get; set; }
        public string RefNo { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string Balance { get; set; }

        

    }

    public class UpdateActivirtEnt
    {
        public string ContainerNo { get; set; }
        public string Activity { get; set; }
    }
}
