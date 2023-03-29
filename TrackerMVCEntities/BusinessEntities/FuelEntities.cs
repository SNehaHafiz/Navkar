using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class FuelEntities
    {
        public string TrailerNo { get; set; }
        public string Passing { get; set; }
        public string driver { get; set; }
        public string Transportor { get; set; }
        public string Activity { get; set; }
        public string containerCount { get; set; }
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string planID { get; set; }
        public string fuel { get; set; }
        public string fuelAmount { get; set; }
        public string amount1 { get; set; }
        public string amount2 { get; set; }
        public string adjustAmount { get; set; }
        public string ReadingFrom { get; set; }
        public string ReadingTo { get; set; }

        public int TrailerID { get; set; }       
        public int driverID { get; set; }
        public int TransportorID { get; set; }
        public int ActivityID { get; set; }
        public int ConTypeID { get; set; }
        public int FromID { get; set; }
        public int ToID { get; set; }
        public int To1ID { get; set; }

        public string VoucherNo { get; set; }
        public string VoucherDate { get; set; }



        public Int64 AutoID { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveUpto { get; set; }
        public string ConType { get; set; }
        public string FuelType { get; set; }
        public string INOUT { get; set; }
        public int SrNo { get; set; }
        public string TrailerType { get; set; }
        public string ShortCode { get; set; }
        public string Litre { get; set; }

        public string preparedBy { get; set; }
        public string HoldClass { get; set; }
        public string ButtonText { get; set; }
        public string ContainerNo { get; set; }
        public string DieselSlipNo { get; set; }
        public int size { get; set; }
        public string DelAddress { get; set; }
        public string InOutDate { get; set; }
        public Int64 VehTransID { get; set; }
        public string Remarks { get; set; }
        public string VoucherBtnClass { get; set; }
        public int validation { get; set; }
        public string btnClass { get; set; }
        public string VehicleTransID { get; set; }
        public string Vehiclebtncss { get; set; }
        public int Mov_ID { get; set; }
        public int CntCount { get; set; }
        public int CostCenter { get; set; }
        public int CostCenterFor { get; set; }
        public string VehicleType  { get; set; }
        public string Weight { get; set; }
        public string ScanType { get; set; }
        public string Status { get; set; }
        public string Drivertype { get; set; }
        public string VehicleTypeID { get; set; }
        public string ScanTypeID { get; set; }
        public string DrivertypeID { get; set; }
        public string Activitystatuscycle { get; set; }
        public string LocationYardID { get; set; }
        public string LocationYardName { get; set; }
        //change by rahul 09-11-2019
        public string TotalWeight { get; set; }
        public string Fastagamount { get; set; }
        public string ScanCount { get; set; }
        public string AdvanceFuel { get; set; }
        public string AdvanceBank { get; set; }
        public string AdvanceCash { get; set; }
        public string LoanID { get; set; }
        public string Compnayname { get; set; }
        public string Slipdate { get; set; }
        public string Shifting { get; set; }
        public int TrailerTransid { get; set; }

        public FuelEntities()
        {
            FuelEntitiesContainerList = new List<FuelEntitiesContainerList>();
        }
        public List<FuelEntitiesContainerList> FuelEntitiesContainerList { get; set; }
    }
    
    public class MovementCountEntities
    {
        public string MovCountID { get; set; }
        public string MovCount { get; set; }
    }
    public class MovementTypeEntities
    {
        public int Mov_ID { get; set; }
        public string Mov_Type { get; set; }
    }
    public class CostCenterEntities
    {
        public int Cost_ID { get; set; }
        public string Cost_Center { get; set; }
    }

    //code change by rahul 09-11-2019
    public class FuelEntitiesContainerList
    {
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string JoNo { get; set; }
        public string CargoType { get; set; }
        public string ScanType { get; set; }
    }
}
