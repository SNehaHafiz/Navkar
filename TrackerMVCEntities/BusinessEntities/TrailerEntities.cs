using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class TrailerEntities
    {
        public DateTime EntryDate { get; set; }
        public int TransID { get; set; }
        public int VehicleTypeID { get; set; }
        public int UserID { get; set; }
        public string TrailerNo { get; set; }
        public string OwnedBy { get; set; }
        public int chkIsActive { get; set; }
        public string Permit { get; set; }
        public DateTime PermitExpity { get; set; }
        public string VehicleGroup { get; set; }
        public string IsShifting { get; set; }
        public string VendorName { get; set; }
        public int Vehicletypegroup { get; set; }
        public int Fueltype { get; set; }
        public int ModelID { get; set; }



        public string PurchasedDate { get; set; }
        public string SoldDate { get; set; }
        public string MFGYR { get; set; }
        public string Mileage { get; set; }
        public string GVW { get; set; }
       

        public TrailerEntities()
        {
            TransporterList = new List<TransporterEntities>();
            VehicleTypeList = new List<VehicleTypeEntities>();
            TrailersEntities = new List<TrailersEntities>();
        }
        public List<TransporterEntities> TransporterList { get; set; }

        public List<VehicleTypeEntities> VehicleTypeList { get; set; }


        public List<TrailersEntities> TrailersEntities { get; set; }
    }
    public class TransporterEntities
    {
        public int TransID { get; set; }

        public string TransName { get; set; }
        public string FuelShortCode { get; set; }
        public string DriverCount { get; set; }
        public string TrailerCount { get; set; }
    }
    public class VehicleTypeEntities
    {
        public int VehicleTypeID { get; set; }

        public string VehicleType { get; set; }
    }

    public class ScanTypeEntities
    {
        public int ScanID { get; set; }

        public string ScanType { get; set; }
    }

    public class DriverTypeEntities
    {
        public int DriverID { get; set; }

        public string DriverType { get; set; }
    }


    public class VehicleTypeGroup
    {
        public int VehicleTypeid { get; set; }

        public string VehicleTypeGroupNae { get; set; }
    }


    public class FuelType
    {
        public int Fuelid { get; set; }

        public string FuelName { get; set; }
    }
    public class Model_M
    {
        public int Modelid { get; set; }

        public string ModelName { get; set; }
    }

}
