using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class FCLDestuffDetailsEntites
    {
        public string Consignee { get; set; }
        public string IGM_GoodsDesc { get; set; }
        public string IGM_BLNo { get; set; }
        public string IGM_PackType { get; set; }
        public string IGM_GrossVol { get; set; }
        public string IGM_GrossWt { get; set; }
        public string IGM_WtUnit { get; set; }
        public string DOValidity { get; set; }
        public string YardName { get; set; }
        public string CHAID { get; set; }
        public string CHAName { get; set; }
        public string Wo_Type { get; set; }
        public int IGMNo { get; set; }
        public int Jono { get; set; }
        public string NocNo { get; set; }
        public int VendorId { get; set; }
        public string Activity { get; set; }
        public FCLDestuffDetailsEntites()
        {
            FCLsummaryDetails = new List<FCLsummaryDetails>();
        }
        public List<FCLsummaryDetails> FCLsummaryDetails { get; set; }
    }

     
    public class LocationEntitiesGateIn
    {
        public int LocationID { get; set; }

        public string Location { get; set; }
    }


}
