using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class DriverDropDownEntites
    {
        public int DLID { get; set; }
        public string Dltype { get; set; }

        public int ID { get; set; }
        public string Equipment { get; set; }


        public int InsuranceID { get; set; }
        public string InsuranceCompany { get; set; }

        public int Emp_Type_ID { get; set; }
        public string Emp_Type { get; set; }
    }
}
