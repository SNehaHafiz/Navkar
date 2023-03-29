using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    
        public class SideMenuEntities
        {

            public string MenuName { get; set; }
            public int MenuID { get; set; }
            public string Controller { get; set; }
            public string Action { get; set; }
            public string ControllerName { get; set; }
            public Nullable<int> ParentID { get; set; }
            public int Priority { get; set; }           
            public string menuIcon { get; set; }
       
    }
}
