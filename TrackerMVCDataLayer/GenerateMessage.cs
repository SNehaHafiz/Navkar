using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCDataLayer
{
   public class GenerateMessage
    {
        public long GenId { get; set; }
        public string GenMessage { get; set; }
        public long OutPutValue { get; set; }

        public GenerateMessage()
        {
            GenId = 0;
            GenMessage = "";
            OutPutValue = 0;
        }
    }   
}
