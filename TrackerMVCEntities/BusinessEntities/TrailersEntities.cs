using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class TrailersEntities
    {
        public short trailerid { get; set; }

        public string trailername { get; set; }

        public string OwnedBy { get; set; }

        public int TransID { get; set; }

        public string VehicleType { get; set; }

        public bool IsActive { get; set; }

        public long AddedBy { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime RegDate { get; set; }

        public string trailerType { get; set; }

        public float VehicleWeight { get; set; }

        public DateTime TPDate { get; set; }

        public string Grade { get; set; }

        public string Passing { get; set; }

        public string TruckModel { get; set; }

        public string DriverID1 { get; set; }

        public string TransName { get; set; }

        public long TrollyID { get; set; }
        public string TrolleyNo { get; set; }
        public string TrolleySizecount { get; set; }

        public decimal TrolleySize { get; set; }

        public decimal TrolleyWeight { get; set; }
        public int SrNo { get; set; }
        public string Regdate { get; set; }

        public string DriverName { get; set; }

        public string Permit { get; set; }

        public string PermitExpiry { get; set; }


        public string ChasisNo { get; set; }


        public string ModelNo { get; set; }

        public string EngineNo { get; set; }

        public string FuelShortCode { get; set; }

        public int DriverID { get; set; }
        public int CheckIsActive { get; set; }
        public string Location { get; set; }
        public int Count { get; set; }
        public int LocationID { get; set; }
        public string trans_date { get; set; }
        public string VehicleGroup { get; set; }
        public string IsShifting { get; set; }
        public string Isshiftingint { get; set; }
        public string ContactNo { get; set; }
        public string VehicleGrouptype { get; set; }
        public string VehicleGrouptypeID { get; set; }
        public string Policy_End_Date { get; set; }

        public string Fitness_Due_Date { get; set; }
        public string Tax_Due_Date { get; set; }
        public string RcBookFiled { get; set; }
        public string Status { get; set; }
      
        public string Green_Tax_Due { get; set; }
        public string Puc_date { get; set; }
        public string Usedfor { get; set; }
        public int VehicleTypeidEdit { get; set; }
        public string Groupid { get; set; }
        public string Fuelid { get; set; }
        public string GroupName{ get; set; }
        public string FuelType { get; set; }
        public string trailertype { get; set; }



        public string Model { get; set; }
        public string SoldDate { get; set; }
        public string MFGYR { get; set; }
        public string Mileage { get; set; }
        public string GVW { get; set; }
    }
    public class Trailer
    {
        public long TrailerID { get; set; }
        public string TrailerNo { get; set; }
    }
    public class TrolleyM
    {
        public long TrolleyID { get; set; }
        public string TrolleyNo { get; set; }
    }

}
