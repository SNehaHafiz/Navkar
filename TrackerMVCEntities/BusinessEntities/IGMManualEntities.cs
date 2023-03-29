using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class IGMManualEntities
    {
        public int ImporterID { get; set; }
        public int NImporterID { get; set; }
        public int CodeID { get; set; }

        public string ContainerNo { get; set; }
        public int Size { get; set; }
        public string ContainerType { get; set; }
        public string JODate { get; set; }
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string JONo { get; set; }
        public string LineName { get; set; }
        public string BtnEditCss { get; set; }
        public string BtnAddCss { get; set; }
        public string IGM_BLNo { get; set; }
        public string IGM_BLDate { get; set; }
        public string Consignee { get; set; }
        public string Con_IGMAddress { get; set; }
        public string NConsignee { get; set; }
        public string NCon_IGMAddress { get; set; }
        public string IGM_GoodsDesc { get; set; }
        public decimal TotIGMQty { get; set; }
        public decimal TotIGMWt { get; set; }
        public int PKGType { get; set; }
        public string POL { get; set; }

        public IGMManualEntities()
        {
            ConsigneeIGMList = new List<ConsigneeIGMManualEntities>();
            NConsigneeIGMList = new List<NConsigneeIGMManualEntities>();
            PackageIGMList = new List<PackageIGMManualEntities>();
            ConDetailsIGMManualEntities = new List<ConDetailsIGMManualEntities>();
        }
        public List<ConsigneeIGMManualEntities> ConsigneeIGMList { get; set; }
        public List<NConsigneeIGMManualEntities> NConsigneeIGMList { get; set; }
        public List<PackageIGMManualEntities> PackageIGMList { get; set; }
        public List<ConDetailsIGMManualEntities> ConDetailsIGMManualEntities { get; set; }
    }
    public class ConsigneeIGMManualEntities
    {
        public int ImporterID { get; set; }

        public string ImporterName { get; set; }

        public string ImporterAddress { get; set; }
    }
    public class NConsigneeIGMManualEntities
    {
        public int ImporterID { get; set; }

        public string ImporterName { get; set; }
    }
    public class PackageIGMManualEntities
    {
        public int CodeID { get; set; }
        public string Package { get; set; }
    }
    public class ConDetailsIGMManualEntities
    {
        public string ContainerNo { get; set; }
        public int Size { get; set; }
        public string ContainerType { get; set; }
        public string Cargotype { get; set; }
        public string InDate { get; set; }
        public string SealNo { get; set; }
        public string IGMQty { get; set; }
        public string IGMWt { get; set; }
       
    }
}
