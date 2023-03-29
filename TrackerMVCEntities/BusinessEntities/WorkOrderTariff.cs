using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class WorkOrderTariff
    {
        public int EntryId { get; set; }
        public int TariffId { get; set; }
        public string Category { get; set; }
        public string WoType { get; set; }
        public string Size { get; set; }
        public string PackageType { get; set; }
        public string CargoType { get; set; }
        public string Commodity { get; set; }
        public int SizeId { get; set; }
        public int PackageId { get; set; }
        public int CargoTypeId { get; set; }
        public int CommodityId { get; set; }
        public string ContainerType { get; set; }
        public string TransType { get; set; }
        public string TranTypeId { get; set; }
        public string UnitType { get; set; }
        public string UnitTypeId { get; set; }
        public string Amount { get; set; }
        public string RangeFrom { get; set; }
        public string RangeTo { get; set; }
        public string ErrMsg { get; set; }


        public WorkOrderTariff()
        {
            EntryId = 0;
            TariffId = 0;
            Category = "";
            WoType = "";
            Size = "";
            PackageType = "";
            CargoType="";
            Commodity="";
            SizeId=0;
            PackageId=0;
            CargoTypeId=0;
            CommodityId=0;
            ContainerType="";
            TransType="";
            TranTypeId="";
            UnitType="";
            UnitTypeId="";
            Amount="";
            RangeFrom="";
            RangeTo = "";
            ErrMsg = "";
    }
    }
}
