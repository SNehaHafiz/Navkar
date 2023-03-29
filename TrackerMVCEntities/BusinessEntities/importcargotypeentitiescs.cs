using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class importcargotypeentitiescs
    {

        public string Consignee  { get;set;}
        public string Con_IGMAddress { get;set;}
        public string NConsignee { get;set;}
        public string NCon_IGMAddress { get;set;}
        public string IGM_BLNo { get;set;}
        public string IGM_GoodsDesc { get;set;}
        public string IGM_GrossWt { get;set;}
        public string IGM_WtUnit { get;set;}
        public string IGM_Qty { get;set;}
        public string IGM_PackType { get;set;}
        public string Remarks { get;set;}
    }


    public class ContainerCargotypeDetails
    {

        public string containerno { get; set; }
        public int Size { get; set; }
        public string ISOCode { get; set; }
        public string Cargotype { get; set; }
        public string Weight { get; set; }
        public string JONo { get; set; }
        public string PKGS { get; set; }
        public string Cargotypeid { get; set; }
        public string SBNO { get; set; }
        public string entryID { get; set; }
    }
}
