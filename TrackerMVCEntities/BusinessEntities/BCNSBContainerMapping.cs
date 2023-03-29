using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class BCNSBContainerMapping
    {
        public long EntryId { get; set; }
        public string Condition { get; set; }
        public string StuffingRequestNo { get; set; }
        public string StuffingRequestDate { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string ContainerType { get; set; }
        public string CargoType { get; set; }
        public string UANNo { get; set; }
        public string ISOCode { get; set; }
        public string TareWeight { get; set; }
        public string ClassNo { get; set; }
        public string SBNo { get; set; }
        public string CargoDesc { get; set; }
        public string Temp { get; set; }
        public string ShippingLine { get; set; }
        public string StuffingType { get; set; }
        public string Shipper { get; set; }
        public string EquipmentType { get; set; }
        public string CHAName { get; set; }
        public string FPD { get; set; }
        public string CustomerName { get; set; }
        public string VIANo { get; set; }
        public string VesselName { get; set; }
        public long VesselId { get; set; }
        public string POL { get; set; }
        public string CuttOfDate { get; set; }
        public string Voyage { get; set; }
        public string POD { get; set; }
        public string Remarks { get; set; }
        public string Rotation { get; set; }
        public string LEODate { get; set; }
        public string LEONo { get; set; }
        public string SBEntryId { get; set; }
        public string SBQty { get; set; }
        public string AgenSealNo { get; set; }
        public string CustomSealNo { get; set; }
        public string TrainNo { get; set; }
        public int TotalPkgs { get; set; }
        public double NetWeight { get; set; }
        public string ErrMessage { get; set; }

        public BCNSBContainerMapping()
        {
            EntryId = 0;
            Condition = "";
            StuffingRequestNo = "";
            StuffingRequestDate = "";
            ContainerNo = "";
            Size = "";
            ContainerType = "";
            CargoType = "";
            UANNo = "";
            ISOCode = "";
            TareWeight = "";
            ClassNo = "";
            SBNo = "";
            CargoDesc = "";
            Temp = "";
            ShippingLine = "";
            StuffingType = "";
            Shipper = "";
            EquipmentType = "";
            CHAName = "";
            FPD = "";
            CustomerName = "";
            VIANo = "";
            VesselName = "0";
            POL = "";
            CuttOfDate = "";
            Voyage = "";
            POD = "";
            Remarks = "";
            Rotation = "";
            LEODate = "";
            LEONo = "";
            SBEntryId = "0";
            SBQty = "";
            AgenSealNo = "";
            CustomSealNo = "";
            TrainNo = "";
            ErrMessage = "";
            VesselId = 0;
            TotalPkgs = 0;
            NetWeight = 0;
        }
    }
}
