using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class InsuranceCompanyEntities
    {
        public int InsurancID { get; set; }
        public string InsuranceCompany { get; set; }
    }

    public class MakeEntities
    {
        public int MakeID { get; set; }
        public string MakeName { get; set; }
    }
    public class ModelEntities
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; }
    }
}
