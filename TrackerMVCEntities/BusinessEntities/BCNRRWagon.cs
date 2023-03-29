using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class BCNRRWagon
    {
        public long EntryId { get; set; }
        public long RRNo { get; set; }
        public long WagonTypeId { get; set; }
        public string WagonType { get; set; }
        public string WagonNo { get; set; }
        public string Wongly { get; set; }
        public double PCCWeight { get; set; }
        public double CCWeight { get; set; }
        public double TareWeight { get; set; }
        public double Pkgs { get; set; }
        public double RemainingPkgs { get; set; }
        public double RemainingWt { get; set; }
        public double StuffPkgs { get; set; }
        public double StuffWt { get; set; }
        public string Remarks { get; set; }
        public string ContainerNo { get; set; }
        public string BookingNo { get; set; }
        public string WTRepNo { get; set; }
        public string Date { get; set; }
        public double NetWeight { get; set; }
        public int SRNo { get; set; }
        public string CustomSealNo { get; set; }
        public string AgentSealNo { get; set; }
        public string ErrorMessage { get; set; }

        public BCNRRWagon()
        {
            EntryId = 0;
            SRNo = 0;
            RRNo = 0;
            WagonTypeId = 0;
            WagonType = "";
            WagonNo = "";
            Wongly= "";
            PCCWeight = 0;
            CCWeight = 0;
            TareWeight = 0;
            Pkgs = 0;
            RemainingPkgs = 0;
            RemainingWt = 0;
            StuffPkgs = 0;
            StuffWt = 0;
            Remarks = "";
            ContainerNo = "";
            BookingNo = "";
            WTRepNo = "";
            Date = "";
            NetWeight = 0;
            AgentSealNo = "";
            CustomSealNo = "";
            ErrorMessage = "";
        }
    }
}
