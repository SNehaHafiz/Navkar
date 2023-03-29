using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class LocationMaster
    {
        public long GSTID { get; set; }

        public string GSTName { get; set; }

        public string GSTAddress { get; set; }

        public string State { get; set; }
        public int State_ID { get; set; }
        public string state_Code { get; set; }
        public string PINCODE { get; set; }
        public string GSTIn_uniqID { get; set; }
        public int IsActive { get; set; }
        public string PANNo { get; set; }

        public string TANNo { get; set; }

        public string Partytype { get; set; }

        public long PartyTypeID { get; set; }

        public int AddedBy { get; set; }

        public DateTime AddedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string Emailid { get; set; }

        public string MobNo { get; set; }

        public DateTime RegDate { get; set; }

        public string TyepReg { get; set; }

        public bool ISRegistration { get; set; }

        public string Remarks { get; set; }

        public string LegalName { get; set; }

        public bool IsApproved { get; set; }

        public int ApprovedBy { get; set; }

        public DateTime ApprovedOn { get; set; }

        public long LedgerID { get; set; }
        public int Common_ID { get; set; }
        public string Name { get; set; }
        public int LocationID { get; set; }

        public string GSTregDate { get; set; }
        public int RGID { get; set; }
        public string Location { get; set; }

        public int IsCopy { get; set; }
    }
    public class Commodity
    {
        public int CommodityID { get; set; }
        public string CommodityName { get; set; }
    }
    public class TransportMode
    {
        public int TrasportID { get; set; }
        public string TransportName { get; set; }
    }
}
