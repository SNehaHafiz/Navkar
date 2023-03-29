using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class IGMDetailsEntities
    {
        public string SrNo { get; set; }
        public string PortName { get; set; }
        public string JoNo { get; set; }
        public string JoDate { get; set; }
        public string VesselName { get; set; }
        public string ViaNo { get; set; }
        public string SMTPNo { get; set; }
        public string SMTPDate { get; set; }
        public string IGMNo { get; set; }
        public string ItemNo { get; set; }
        public string ContainerNO { get; set; }
        public string Size { get; set; }
        public string ISOCode { get; set; }
        public string ContainerType { get; set; }
        public string CargoType { get; set; }
        public string CargoWeight { get; set; }
        public string BLNumber { get; set; }
        public string BLDate { get; set; }
        public string Importer { get; set; }
        public string LineName { get; set; }
        public string CargoDescriptions { get; set; }
        public string BLNo { get; set; }
        public string Indate { get; set; }
        public string Origin { get; set; }


        public string Container_No { get; set; }
        public int SealNo { get; set; }
        public string SealNo1 { get; set; }
        public int PKGS { get; set; }
        public string PKGS1 { get; set; }
        public string PKGSType { get; set; }
        public string Consignee { get; set; }
        public string Address { get; set; }
        public string NotifyConsignee { get; set; }
        public string NotifyAddress { get; set; }
        public string GoodsDescription { get; set; }
        public string Itemstatus { get; set; }
        public int SpecID { get; set; }
        public int FileTypeId { get; set; }
        public long JoNo1 { get; set; }
    }
}

