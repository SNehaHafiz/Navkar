using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class ExportShippingBillEnt
    {
        public int ImporterID { get; set; }
        public string ImporterName { get; set; }
        public int SLID { get; set; }
        public string SLName { get; set; }
        public string Cancel { get; set; }
        public int EntryID { get; set; }
        public string SBNO { get; set; }
        public int SrNo { get; set; }
        public float BttArea { get; set; }
        public string JODate { get; set; }
        public string InvNo { get; set; }
        public string JONO { get; set; }
        public string con { get; set; }
        public string conaddrs { get; set; }
        public string CargoDesc { get; set; }
        public float FobVal { get; set; }
        public int StuffID { get; set; }
        public string Stuffing { get; set; }
        public float StuffQty { get; set; }
        public float StuffWT { get; set; }
        public float BttQty { get; set; }
        public float BttWt { get; set; }
        public string Type { get; set; }
        public float carQty { get; set; }
        public float cargowt { get; set; }
        public string kgwt { get; set; }
        public string Remark { get; set; }
        public string EntryDate { get; set; }
        public string SBDate { get; set; }
        public int AGID { get; set; }
        public string AGName { get; set; }
        public int shipperid { get; set; }
        public string Shippername { get; set; }
     
        public int CHAID { get; set; }
        public string CHAName { get; set; }
        public int CID { get; set; }
        public string CType { get; set; }
        public int pkgid { get; set; }
        public string pkgtype { get; set; }
        public int PodID { get; set; }
        public string PODName { get; set; }
        public string ComID { get; set; }
        public string CommodityName { get; set; }
        public int ContainerTypeID { get; set; }
        public string ContainerType { get; set; }
        public int PID { get; set; }
        public string PName { get; set; }
        public int PCountryID { get; set; }
        public string CountryName { get; set; }
        public string ContNo { get; set; }
        public int TareWt { get; set; }
        public int Size { get; set; }



        public int IsCancel { get; set; }
        public int IsOpen { get; set; }
        public int Series { get; set; }
        public int LRNo { get; set; }
        public string Activity { get; set; }
        public string ContainerNo { get; set; }
        public string LRDate { get; set; }
        public string BookingNo { get; set; }
        public int FromLocationID { get; set; }
        public int ToLocationID { get; set; }
        public string BOENo { get; set; }
        public string VehicleNo { get; set; }
        public string consignee { get; set; }

        public float balanceqty { get; set; }
        public float balancewt { get; set; }
    }

    public class VesselCutOff : ExportShippingBillEnt
    {
        public int VesselID { get; set; }
        public string VesselName { get; set; }
        public string containerNo { get; set; }
        public string ViaNo { get; set; }
        public string Voyage { get; set; }
        public string RotationNo { get; set; }
        public string CutofDate { get; set; }
        public string gateopendate { get; set; }
        public bool IsOpen { get; set; }
        public int PortID { get; set; }
        public string PortName { get; set; }
        //  public int PODID { get; set; }
        public int csize { get; set; }
        public string CargoType { get; set; }
        public string Select { get; set; }
        public int SBEntryID { get; set; }
        //public string PODName { get; set; }

    }

    public class Items
    {
        public string name { get; set; }
        public int itemid { get; set; }


        public int ID { get; set; }
        public string Department { get; set; }

        public int Itemno { get; set; }
        public string ItemnoName { get; set; }
        public int Qty { get; set; }
        public int Rate { get; set; }
        public int DepartID { get; set; }
        public string Remarks { get; set; }
        public string Tax_Type_Desc { get; set; }



    }

}
