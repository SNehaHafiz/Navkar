using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ChangPassword
    {
        public int userID { get; set; }
        public String UserName { get; set; }
        public String OldPassword { get; set; }
        public String NewPassword { get; set; }
    }
}
