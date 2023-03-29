using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class PendingContainersForJoUpdationsEntities
    {
        public string SrNo { get; set; }
        public string ContainerNo { get; set; }
        public int Size { get; set; }
        public string FL { get; set; }
        public string Type { get; set; }
        public string ISOCode { get; set; }
        public string CargoWt { get; set; }
        public string FIGMNoL { get; set; }
        public string Itemno { get; set; }
        public string Igmno { get; set; }

        public string SMTPNO { get; set; }
        public string SMTPDate { get; set; }
        public string Consignee { get; set; }
        public string CargoDescriptions { get; set; }
        public string BLNumber { get; set; }
        public string SealNo { get; set; }
        public string UpdatedBy { get; set; }

        public string Activity { get; set; }
        public string InDate { get; set; }
        public string LineName { get; set; }
        public string DwellDays { get; set; }
        public string Status { get; set; }

    }
}
