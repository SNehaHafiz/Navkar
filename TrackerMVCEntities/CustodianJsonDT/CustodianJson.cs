using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.CustodianJsonDT
{
    public class CustodianJson
    {
        public CustodianJson()
        {
            EGM = new EGM();
        }
        public string filePath { get; set; }
        public string moveFilePath { get; set; }
        public string fileName { get; set; }
        public int fileNo { get; set; }
        public EGM EGM { get; set; }
    }
}
