using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class TaxSettings
    {
        public string TaxName { get; set; }

        public decimal TaxPercentage { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveUpto { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }

        public int settingsID { get; set; }
        public bool IsActive { get; set; }
        public string PartyName { get; set; }
        public string CreditDate { get; set; }
        public int Netotal { get; set; }
        public int  GrandTotal { get; set; }
        public string dtFromdate { get; set; }
      
        public int CreditNoteNo { get; set; }
        public string Remarks { get; set; }

    }
}
