using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class AccidentClaimSaveEntities
    {
        public string ClaimDate { get; set; }
        public int ClaimedBy { get; set; }
        public string Claim_Settle_Date { get; set; }
        public int Claim_SettledBy { get; set; }
        public string Claim_Amount { get; set; }
        public string Claim_Remarks { get; set; }
        public Boolean IsSettled { get; set; }
        public int EntryID { get; set; }
    }

    public  class AccidentClaimEntities
    {
        public string trailername { get; set; }
        public string AccidentDate { get; set; }
        public string Accident_Place { get; set; }
        public string Driver_Name { get; set; }
        public string PolicyNo { get; set; }
        public string InsuranceCompany { get; set; }
        public string EntryID { get; set; }
    }
}
