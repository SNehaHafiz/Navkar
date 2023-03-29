using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.LoadingPlan
{
    public class LoadingPlan
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();

        public List<EN.LoadingPlanEntities> getLoadingContainerList()
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getLoadingContainerList();
            List<EN.LoadingPlanEntities> ContainerList = new List<EN.LoadingPlanEntities>();
            int Count = 1;
            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.LoadingPlanEntities ContainerDL = new EN.LoadingPlanEntities();

                    ContainerDL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    ContainerDL.CustomerName = Convert.ToString(row["CHAName"]);
                    ContainerDL.GPNo = Convert.ToString(row["GPNo"]);
                    ContainerDL.Destination = Convert.ToString(row["Location"]);
                    ContainerDL.TrailerNo = Convert.ToString(row["trailerno"]);
                    ContainerDL.JONo = Convert.ToInt64(row["jono"]);
                    ContainerDL.Size = Convert.ToInt16(row["Size"]);
                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }


        public List<EN.LoadingPlanEntities> getLoadingConfirmationList()
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.getLoadingConfirmationList();
            List<EN.LoadingPlanEntities> ContainerList = new List<EN.LoadingPlanEntities>();
            int Count = 1;
            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.LoadingPlanEntities ContainerDL = new EN.LoadingPlanEntities();

                    ContainerDL.ContainerNo = Convert.ToString(row["ContainerNo"]);                    
                    ContainerDL.TrailerNo = Convert.ToString(row["trailerno"]);
                    ContainerDL.JONo = Convert.ToInt64(row["jono"]);
                    ContainerDL.Size = Convert.ToInt16(row["Size"]);
                    ContainerDL.KalmarNo = Convert.ToString(row["trailername"]);
                    ContainerDL.KalmarID = Convert.ToInt16(row["trailerid"]);
                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }

        public List<string> GetContainerAutoComplete(string containerNo)
       
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.GetContainerAutoComplete(containerNo);
            List<string> ContainerList = new List<string>();
            int Count = 1;
            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {


                    string name = Convert.ToString(row["ContainerNo"]);
                    ContainerList.Add(name);
                }

            }
            return ContainerList;

            
        }

        public string SaveLoadingPlan(string containerNo, string TrailerNo, long JONO, int Userid)
        {
            string message="";
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.SaveLoadingPlan(containerNo, TrailerNo, JONO, Userid);
            if (ContainerDT.Rows.Count > 0)
            {
                message = "Record Saved successfully";
            }
            return message;
        }

        public List<EN.TrailersEntities> getKalmarDropDownList()
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getKalmarList();
            List<EN.TrailersEntities> KalmarList = new List<EN.TrailersEntities>();
            
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TrailersEntities KalmarDL = new EN.TrailersEntities();

                    KalmarDL.trailerid = Convert.ToInt16(row["trailerid"]);
                    KalmarDL.trailername = Convert.ToString(row["trailername"]);


                    KalmarList.Add(KalmarDL);
                }

            }
            return KalmarList;
        }



        public string SaveLoadingConfirmation(string containerNo, string vehicleNo, int kalmarNo, long JONO, int Userid)
        {
            string message = ""; int i;
            //DataTable ContainerDT = new DataTable();
            i = trackerdatamanager.SaveLoadingConfirmation(containerNo, vehicleNo, kalmarNo, JONO, Userid);
            if (i > 0)
            {
                message = "Record Saved successfully";
            }
            return message;
        }

        //public List<EN.FuelEntities> getFuelList()
        //{
        //    DataTable dt = new DataTable();
        //    dt = trackerdatamanager.getFuelList();
        //    List<EN.FuelEntities> FuelList = new List<EN.FuelEntities>();

        //    if (dt.Rows.Count != 0)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            EN.FuelEntities FuelDL = new EN.FuelEntities();

        //            FuelDL.TrailerNo = Convert.ToString(row["Trailername"]);
        //            FuelDL.Passing = Convert.ToString(row["passing"]);
        //            FuelDL.driver = Convert.ToString(row["drivername"]);
        //            FuelDL.Transportor = Convert.ToString(row["transname"]);
        //            FuelDL.Activity = Convert.ToString(row["Activity"]);
        //            FuelDL.containerCount = Convert.ToString(row["Size"]);
        //            FuelDL.Type = Convert.ToString(row["Type"]);
        //            FuelDL.From = Convert.ToString(row["FromLocation"]);
        //            FuelDL.To = Convert.ToString(row["toLocation"]);
        //            FuelDL.planID = Convert.ToString(row["PlanID"]);
        //            FuelDL.INOUT = Convert.ToString(row["Status"]);
        //            FuelDL.ContainerNo = Convert.ToString(row["ContainerNo"]);
        //            FuelDL.TrailerType = Convert.ToString(row["trailerType"]);
        //            FuelDL.DelAddress = Convert.ToString(row["DelAddress"]);
        //            FuelDL.InOutDate = Convert.ToString(row["InOutDate"]);
        //            FuelDL.btnClass = Convert.ToString(row["btnCss"]);

        //            FuelDL.ActivityID = Convert.ToInt32(row["ActivityID"]);

        //            FuelList.Add(FuelDL);
        //        }

        //    }
        //    return FuelList;
        //}


        public List<EN.FuelEntities> getFuelList(string Vehiclename)
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getFuelList(Vehiclename);
            List<EN.FuelEntities> FuelList = new List<EN.FuelEntities>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.FuelEntities FuelDL = new EN.FuelEntities();

                    FuelDL.TrailerNo = Convert.ToString(row["Trailername"]);
                    FuelDL.Passing = Convert.ToString(row["passing"]);
                    FuelDL.driver = Convert.ToString(row["drivername"]);
                    FuelDL.Transportor = Convert.ToString(row["transname"]);
                    FuelDL.Activity = Convert.ToString(row["Activity"]);
                    FuelDL.containerCount = Convert.ToString(row["Size"]);
                    FuelDL.Type = Convert.ToString(row["Type"]);
                    FuelDL.From = Convert.ToString(row["FromLocation"]);
                    FuelDL.To = Convert.ToString(row["toLocation"]);
                    FuelDL.planID = Convert.ToString(row["PlanID"]);
                    FuelDL.INOUT = Convert.ToString(row["Status"]);
                    FuelDL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    FuelDL.TrailerType = Convert.ToString(row["trailerType"]);
                    FuelDL.DelAddress = Convert.ToString(row["DelAddress"]);
                    FuelDL.InOutDate = Convert.ToString(row["InOutDate"]);
                    FuelDL.btnClass = Convert.ToString(row["btnCss"]);
                    FuelDL.Vehiclebtncss = Convert.ToString(row["btncancelcss"]);
                    FuelDL.ActivityID = Convert.ToInt32(row["ActivityID"]);
                    FuelDL.LocationYardID = Convert.ToString(row["YardID"]);
                    FuelDL.LocationYardName = Convert.ToString(row["Yard"]);
                    FuelDL.VehTransID = Convert.ToInt32(row["transID"]);

                    FuelList.Add(FuelDL);
                }

            }
            return FuelList;
        }



        public string GetTPValidatedetails(string Trailername)
        {
            string message = "";
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetTPValidate(Trailername);
            if (dt != null)
            {
                try
                {
                    var message1 = Convert.ToString(dt.Rows[0]["TPClose"]);
                    message = message1;
                }
                catch (Exception ex)
                {
                    ;
                    return message = "";
                }

            }

            return message;
        }

        //public EN.FuelEntities getFuelData(int planID)
        //{
        //    DataTable dt = new DataTable();
        //    dt = trackerdatamanager.getFuelData(planID);
        //    EN.FuelEntities FuelData = new EN.FuelEntities();
           
        //    if (dt.Rows.Count != 0)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            EN.FuelEntities FuelDL = new EN.FuelEntities();

        //            FuelDL.fuel = Convert.ToString(row["fuel"]);
        //            FuelDL.amount1 = Convert.ToString(row["fuelamount"]);
        //            FuelDL.amount2 = Convert.ToString(row["fuelamount1"]);
        //            //FuelDL.Transportor = Convert.ToString(row["transname"]);
        //            //FuelDL.Activity = Convert.ToString(row["Activity"]);
        //            //FuelDL.containerCount = Convert.ToString(row["Size"]);
        //            //FuelDL.Type = Convert.ToString(row["Type"]);
        //            //FuelDL.From = Convert.ToString(row["FromLocation"]);
        //            //FuelDL.To = Convert.ToString(row["toLocation"]);
        //            //FuelDL.planID = Convert.ToInt64(row["PlanID"]);
                   
        //        }

        //    }
        //    return FuelData;
        //}


        public List<EN.DriversEntities> getDrivers()
        {
            List<EN.DriversEntities> DriversDL = new List<EN.DriversEntities>();
            DataTable dt = new DataTable();
            string Table = "Drivers";
            string Column = "driverid,drivername";
            string Condition = "";
            string OrderBy = "drivername";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DriversEntities DriversList = new EN.DriversEntities();
                    DriversList.driverID = Convert.ToInt32(row["driverid"]);
                    DriversList.driverName = Convert.ToString(row["drivername"]);
                    DriversDL.Add(DriversList);
                }
            }
            return DriversDL;
        }

        public List<EN.TransporterEntities> getTranspoter()
        {
            List<EN.TransporterEntities> transpoterDL = new List<EN.TransporterEntities>();
            DataTable dt = new DataTable();
            string Table = "Transport_TariffGroup";
            string Column = "GroupTariffID,GroupTariffName";
            string Condition = "";
            string OrderBy = "GroupTariffName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TransporterEntities transpoterList = new EN.TransporterEntities();
                    transpoterList.TransID = Convert.ToInt32(row["GroupTariffID"]);
                    transpoterList.TransName = Convert.ToString(row["GroupTariffName"]);
                    transpoterDL.Add(transpoterList);
                }
            }
            return transpoterDL;
        }


        public List<EN.TransporterEntities> getDriverTranspoter()
        {
            List<EN.TransporterEntities> transpoterDL = new List<EN.TransporterEntities>();
            DataTable dt = new DataTable();
            string Table = "Transporter";
            string Column = "Transid,Transname";
            string Condition = "";
            string OrderBy = "Transname";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TransporterEntities transpoterList = new EN.TransporterEntities();
                    transpoterList.TransID = Convert.ToInt32(row["Transid"]);
                    transpoterList.TransName = Convert.ToString(row["Transname"]);
                    transpoterDL.Add(transpoterList);
                }
            }
            return transpoterDL;
        }


        public List<EN.TrailersEntities> getTrailerList()
        {
            List<EN.TrailersEntities> trailerDL = new List<EN.TrailersEntities>();
            DataTable dt = new DataTable();
            string Table = "trailers";
            string Column = "trailerID,trailername";
            string Condition = "";
            string OrderBy = "trailername";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TrailersEntities transpoterList = new EN.TrailersEntities();
                    transpoterList.trailerid = Convert.ToInt16(row["trailerID"]);
                    transpoterList.trailername = Convert.ToString(row["trailername"]);
                    trailerDL.Add(transpoterList);
                }
            }
            return trailerDL;
        }

        public List<EN.LocationMaster> getLocation()
        {
            List<EN.LocationMaster> locationDL = new List<EN.LocationMaster>();
            DataTable dt = new DataTable();
            string Table = "Ext_Location_M";
            string Column = "LocationID,Location";
            string Condition = "IsActive=1";
            string OrderBy = "Location";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationMaster locationList = new EN.LocationMaster();
                    locationList.LocationID = Convert.ToInt32(row["LocationID"]);
                    locationList.Location = Convert.ToString(row["Location"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }


        public List<EN.PassingEntities> getPassing()
        {
            List<EN.PassingEntities> passingDL = new List<EN.PassingEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getPassing();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.PassingEntities PassingList = new EN.PassingEntities();
                    //PassingList.PassingID = Convert.ToInt32(row["PassingID"]);
                    PassingList.Passing = Convert.ToString(row["Passing"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        

        public string getReadingFrom(string trailerNo)
        {
            DataTable dt = new DataTable();
            EN.FuelEntities FuelDL = new EN.FuelEntities();
            string ReadingFrom = "";
            dt = trackerdatamanager.getReadingFrom(trailerNo);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    //PassingList.PassingID = Convert.ToInt32(row["PassingID"]);
                    ReadingFrom = Convert.ToString(row["ReadingFrom"]);
                    
                }
            }
            return ReadingFrom;
        }

        //codes by Arti


        


        public List<EN.ActivityMaster> getActivity()
        {
            List<EN.ActivityMaster> passingDL = new List<EN.ActivityMaster>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getActivity();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ActivityMaster PassingList = new EN.ActivityMaster();
                    PassingList.AutoID = Convert.ToInt32(row["AutoID"]);
                    PassingList.Activity = Convert.ToString(row["Activity"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }



        public EN.FuelEntities getTrailerWiseDriverData(int trailerID)
        {
            EN.FuelEntities FuelDL = new EN.FuelEntities();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getTrailerWiseDriverData(trailerID);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    FuelDL.Passing = Convert.ToString(row["Passing"]);
                    FuelDL.driverID = Convert.ToInt32(row["DriverID"]);
                    FuelDL.TransportorID = Convert.ToInt32(row["TransID"]);
                    FuelDL.TrailerType = Convert.ToString(row["trailerType"]);
                   
                }
            }
            return FuelDL;
        }




        public List<EN.FuelEntities> GetGeneratedFuelSlipData()
        {
            List<EN.FuelEntities> FuelList = new List<EN.FuelEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetGeneratedFuelSlipData();

            if (dt != null)
            {
                int Count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    EN.FuelEntities FuelDL = new EN.FuelEntities();
                    FuelDL.SrNo = Convert.ToInt32(Count++);
                    FuelDL.Activity = Convert.ToString(row["activity"]);
                    FuelDL.adjustAmount = Convert.ToString(row["adjustmentsFuel"]);
                    FuelDL.amount1 = Convert.ToString(row["AmountCash"]);
                    FuelDL.amount2 = Convert.ToString(row["Amountbank"]);
                    FuelDL.driver = Convert.ToString(row["drivername"]);
                    FuelDL.Transportor = Convert.ToString(row["transName"]);
                    FuelDL.VoucherDate = Convert.ToString(row["voucherdate"]);
                    FuelDL.VoucherNo = Convert.ToString(row["voucherNo"]);
                    FuelDL.TrailerNo = Convert.ToString(row["trailername"]);
                    FuelDL.ReadingFrom = Convert.ToString(row["StartReading"]);
                    FuelDL.ReadingTo = Convert.ToString(row["EndReading"]);
                    FuelDL.From = Convert.ToString(row["FromLoc"]);
                    FuelDL.To = Convert.ToString(row["ToLocation"]);
                    FuelDL.fuel = Convert.ToString(row["Fuel"]);
                    FuelDL.Passing = Convert.ToString(row["passing"]);
                    FuelDL.VoucherNo = Convert.ToString(row["voucherNo"]);
                    FuelDL.HoldClass = Convert.ToString(row["btnClass"]);
                    FuelDL.TrailerType = Convert.ToString(row["trailerType"]);
                    FuelDL.DelAddress = Convert.ToString(row["DelAddress"]);
                    FuelDL.InOutDate = Convert.ToString(row["InOutDate"]);
                    FuelDL.VoucherBtnClass = Convert.ToString(row["btnVoucherClass"]);
                    FuelDL.AutoID = Convert.ToInt64(row["AutoID"]);
                    FuelList.Add(FuelDL);
                }
            }
            return FuelList;
        }



        public List<EN.FuelEntities> GetGeneratedFuelSlipDataForHold(string Date)
        {
            List<EN.FuelEntities> FuelList = new List<EN.FuelEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetGeneratedFuelSlipDataForHold(Date);

            if (dt != null)
            {
                int Count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    EN.FuelEntities FuelDL = new EN.FuelEntities();
                    FuelDL.SrNo = Convert.ToInt32(Count++);
                    FuelDL.Activity = Convert.ToString(row["activity"]);
                    FuelDL.adjustAmount = Convert.ToString(row["adjustmentsFuel"]);
                    FuelDL.amount1 = Convert.ToString(row["AmountCash"]);
                    FuelDL.amount2 = Convert.ToString(row["Amountbank"]);
                    FuelDL.driver = Convert.ToString(row["drivername"]);
                    FuelDL.Transportor = Convert.ToString(row["transName"]);
                    FuelDL.VoucherDate = Convert.ToString(row["voucherdate"]);
                    FuelDL.VoucherNo = Convert.ToString(row["voucherNo"]);
                    FuelDL.TrailerNo = Convert.ToString(row["trailername"]);
                    FuelDL.ReadingFrom = Convert.ToString(row["StartReading"]);
                    FuelDL.ReadingTo = Convert.ToString(row["EndReading"]);
                    FuelDL.From = Convert.ToString(row["FromLoc"]);
                    FuelDL.To = Convert.ToString(row["ToLocation"]);
                    FuelDL.fuel = Convert.ToString(row["Fuel"]);
                    FuelDL.Passing = Convert.ToString(row["passing"]);
                    FuelDL.VoucherNo = Convert.ToString(row["voucherNo"]);
                    FuelDL.HoldClass = Convert.ToString(row["HoldClass"]);
                    FuelDL.ButtonText = Convert.ToString(row["ButtonText"]);
                    FuelList.Add(FuelDL);
                }
            }
            return FuelList;
        }

        public string HoldVoucher(string VoucherNo, int Userid)
        {
            int i = 0;
            i = trackerdatamanager.HoldVoucher(VoucherNo, Userid);
            string message;
            if (i > 0)
            {
                message = "Record modified Successfully.";
            }
            else {
                message = "Record not modify, Please try again!";
            }
            return message;
        }


        public string ClearVoucher(string VoucherNo, int Userid)
        {
            int i = 0;
            i = trackerdatamanager.ClearVoucher(VoucherNo, Userid);
            string message;
            if (i > 0)
            {
                message = "Record modified Successfully.";
            }
            else {
                message = "Record not modify, Please try again!";
            }
            return message;
        }
        public EN.FuelEntities SaveFuelDirectSetting(int TranspoterID, string FromDate, string ToDate, int ActivityID, string InOut, int ContainerTypeID, string ContaierSize, int Fromlocation, int Tolocation, float Fuel, string Amount1, string Amount2, int RecordID, int Userid, string passing, string TrailerType, string Drivertype, string VehicleType, string ScanType, string Weight, string status, string TxtFuelamount)
        {
            EN.FuelEntities vendorList = new EN.FuelEntities();

            DataTable VendorDataDL = new DataTable();


            VendorDataDL = trackerdatamanager.SaveFuelDirectSetting(TranspoterID, FromDate, ToDate, ActivityID, InOut, ContainerTypeID, ContaierSize, Fromlocation, Tolocation, Fuel, Amount1, Amount2, RecordID, Userid, passing, TrailerType, Drivertype, VehicleType, ScanType, Weight, status, TxtFuelamount);

            return vendorList;

        }

        public List<EN.FuelEntities> GetFuelTariffList(EN.FuelEntities fe)
        {
            List<EN.FuelEntities> passingDL = new List<EN.FuelEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetFuelTariffList(fe.TransportorID, fe.ActivityID, fe.INOUT, fe.ConTypeID, fe.containerCount, fe.FromID, fe.ToID, fe.Passing, fe.TrailerType, fe.DrivertypeID, fe.VehicleTypeID, fe.ScanType);

            if (dt != null)
            {
                int Count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    EN.FuelEntities PassingList = new EN.FuelEntities();

                    PassingList.SrNo = Convert.ToInt32(Count++);
                    PassingList.AutoID = Convert.ToInt32(row["AutoID"]);
                    PassingList.TransportorID = Convert.ToInt32(row["TransID"]);
                    PassingList.Transportor = Convert.ToString(row["TransName"]);
                    PassingList.EffectiveFrom = Convert.ToString(row["Effectivefrom"]);
                    PassingList.EffectiveUpto = Convert.ToString(row["Effectiveupto"]);
                    PassingList.ActivityID = Convert.ToInt32(row["ActivityID"]);
                    PassingList.Activity = Convert.ToString(row["Activity"]);
                    PassingList.ConTypeID = Convert.ToInt32(row["ConTypeID"]);
                    PassingList.ConType = Convert.ToString(row["Cargotype"]);
                    PassingList.FuelType = Convert.ToString(row["FuelType"]);
                    PassingList.fuel = Convert.ToString(row["Fuel"]);
                    PassingList.amount1 = Convert.ToString(row["FuelAmount"]);

                    PassingList.amount2 = Convert.ToString(row["FuelAmount1"]);

                    PassingList.FromID = Convert.ToInt32(row["FrLocID"]);
                    PassingList.From = Convert.ToString(row["FromLocation"]);
                    PassingList.ToID = Convert.ToInt32(row["ToLocID"]);
                    PassingList.To = Convert.ToString(row["ToLocation"]);
                    PassingList.INOUT = Convert.ToString(row["INOUT"]);
                    PassingList.containerCount = Convert.ToString(row["ContSize"]);
                    PassingList.Passing = Convert.ToString(row["Passing"]);
                    PassingList.TrailerType = Convert.ToString(row["TrailerType"]);
                    PassingList.VehicleType = Convert.ToString(row["VehicleType"]);
                    PassingList.Weight = Convert.ToString(row["Weight"]);
                    PassingList.ScanType = Convert.ToString(row["ScanType"]);
                    PassingList.Status = Convert.ToString(row["Status"]);
                    PassingList.Drivertype = Convert.ToString(row["Drivertype"]);
                    PassingList.VehicleTypeID = Convert.ToString(row["VehicleTypeID"]);
                    PassingList.ScanTypeID = Convert.ToString(row["ScanID"]);
                    PassingList.DrivertypeID = Convert.ToString(row["DriverTypeID"]);
                    PassingList.fuelAmount = Convert.ToString(row["DieselFuelamount"]);



                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }


        public int getActivityMaxFuel(string Activity)
        {
            int MaxFuel = 0;
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getActivityMaxFuel(Activity);
            if (dt.Rows.Count > 0)
            {
                MaxFuel = Convert.ToInt32(dt.Rows[0][0]);
            }
            return MaxFuel;
        }

        public int UpdateFuelStatus(string TrailerNo, string LocationYardID, string ActivityID, int Userid)
        {
            int i = 0;

            i = trackerdatamanager.UpdateFuelStatus(TrailerNo, LocationYardID, ActivityID, Userid);

            return i;
        }
        public EN.FuelEntities VoucherValidation(string TrailerNo, string ContainerNo, int ActivityTypeID, string ActivityType,string LocationYardID)
        {
            EN.FuelEntities FuelDL = new EN.FuelEntities();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            dt = trackerdatamanager.VoucherValidation(TrailerNo, ContainerNo, ActivityTypeID, ActivityType, LocationYardID);
            dt1 = trackerdatamanager.getContainerListForFuelGenerate(TrailerNo, ActivityType, ActivityTypeID, ContainerNo, LocationYardID);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FuelDL.VoucherNo = Convert.ToString(row["VoucherNo"]);
                    FuelDL.VoucherDate = Convert.ToString(row["Voucherdate"]);
                }
            }
            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    FuelDL.CntCount = 1;
                }
                else
                {
                    FuelDL.CntCount = 0;
                }
            }
            return FuelDL;
        }


        //codes by Arti




        public EN.FuelEntities getPrintDataForFuelTarriff(string fuelID, string AutoID)
        {
            EN.FuelEntities FuelDL = new EN.FuelEntities();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getPrintDataForFuelTarriff(fuelID, AutoID);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FuelDL.Activity = Convert.ToString(row["activity"]);
                    FuelDL.adjustAmount = Convert.ToString(row["adjustmentsFuel"]);
                    FuelDL.amount1 = Convert.ToString(row["AmountCash"]);
                    FuelDL.amount2 = Convert.ToString(row["Amountbank"]);

                    FuelDL.driver = Convert.ToString(row["drivername"]);
                    FuelDL.Transportor = Convert.ToString(row["transName"]);
                    FuelDL.VoucherDate = Convert.ToString(row["voucherdate"]);
                    FuelDL.VoucherNo = Convert.ToString(row["VoucherPrintNo"]);
                    FuelDL.TrailerNo = Convert.ToString(row["trailername"]);
                    FuelDL.ReadingFrom = Convert.ToString(row["StartReading"]);
                    FuelDL.ReadingTo = Convert.ToString(row["EndReading"]);
                    FuelDL.From = Convert.ToString(row["FromLoc"]);
                    FuelDL.To = Convert.ToString(row["ToLocation"]);
                    FuelDL.fuel = Convert.ToString(row["Fuel"]);
                    FuelDL.Passing = Convert.ToString(row["passing"]);
                    //  FuelDL.VoucherNo = Convert.ToString(row["voucherNo"]);
                    FuelDL.ShortCode = Convert.ToString(row["SHORTCODE"]);
                    FuelDL.Litre = Convert.ToString(row["Litre"]);
                    FuelDL.INOUT = Convert.ToString(row["Activitytype"]);
                    FuelDL.preparedBy = Convert.ToString(row["UserName"]);
                    FuelDL.AutoID = Convert.ToInt32(row["autoid"]);
                    FuelDL.DieselSlipNo = Convert.ToString(row["SlipNo"]);
                    FuelDL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    FuelDL.DelAddress = Convert.ToString(row["DelAddress"]);
                    FuelDL.InOutDate = Convert.ToString(row["InOutDate"]);
                    FuelDL.driverID = Convert.ToInt32(row["driverid"]);
                    FuelDL.fuelAmount = Convert.ToString(row["Diesel Fuel amount"]);
                    FuelDL.Compnayname = Convert.ToString(row["Company Name"]);
                    FuelDL.Slipdate = Convert.ToString(row["slipDate"]);

                }
            }
            return FuelDL;
        }

        public void UpdateVoucherFuelSlipPrint(string voucherNo, string AutoID)
        {
            trackerdatamanager.UpdateVoucherFuelSlipPrint(voucherNo, AutoID);
        }

        public void UpdateFuelSlipPrint(string voucherNo, string AutoID)
        {
            trackerdatamanager.UpdateFuelSlipPrint(voucherNo, AutoID);
        }
        //by prashant
        public List<EN.MovementCountEntities> getMovCount()
        {
            List<EN.MovementCountEntities> locationDL = new List<EN.MovementCountEntities>();
            DataTable dt = new DataTable();
            string Table = "Mov_Count";
            string Column = "MovCount,MovCount";
            string Condition = "";
            string OrderBy = "MovCountID";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.MovementCountEntities MovList = new EN.MovementCountEntities();
                    MovList.MovCountID = Convert.ToString(row["MovCount"]);
                    MovList.MovCount = Convert.ToString(row["MovCount"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }
        public List<EN.MovementTypeEntities> getMovType()

        {
            List<EN.MovementTypeEntities> locationDL = new List<EN.MovementTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "Transport_MOV_Type";
            string Column = "Mov_ID,Mov_Type";
            string Condition = "";
            string OrderBy = "Mov_ID";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.MovementTypeEntities MovList = new EN.MovementTypeEntities();
                    MovList.Mov_ID = Convert.ToInt16(row["Mov_ID"]);
                    MovList.Mov_Type = Convert.ToString(row["Mov_Type"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }


        public List<EN.CostCenterEntities> getCostCenter()
        {
            List<EN.CostCenterEntities> CCDL = new List<EN.CostCenterEntities>();
            DataTable dt = new DataTable();
            string Table = "Cost_CenterM";
            string Column = "ID,COST_CENTER";
            string Condition = "";
            string OrderBy = "COST_CENTER";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CostCenterEntities CCList = new EN.CostCenterEntities();
                    CCList.Cost_ID = Convert.ToInt16(row["ID"]);
                    CCList.Cost_Center = Convert.ToString(row["COST_CENTER"]);
                    CCDL.Add(CCList);
                }
            }
            return CCDL;
        }

        public List<EN.ActivityCycle> GetActivitycyclemaster()
        {
            List<EN.ActivityCycle> CCDL = new List<EN.ActivityCycle>();
            DataTable dt = new DataTable();
            string Table = "Activity_Cycle_M";
            string Column = "CycleID,Cycle";
            string Condition = "";
            string OrderBy = "Cycle";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ActivityCycle CCList = new EN.ActivityCycle();
                    CCList.ActivitycycleID = Convert.ToInt16(row["CycleID"]);
                    CCList.ActivitycycleName = Convert.ToString(row["Cycle"]);
                    CCDL.Add(CCList);
                }
            }
            return CCDL;
        }

        public List<EN.ActivityCycle> GetActivitVehicle()
        {
            List<EN.ActivityCycle> CCDL = new List<EN.ActivityCycle>();
            DataTable dt = new DataTable();
            string Table = "Cycle";
            string Column = "CycleID,Cycle";
            string Condition = "";
            string OrderBy = "Cycle";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ActivityCycle CCList = new EN.ActivityCycle();
                    CCList.ActivitycycleID = Convert.ToInt16(row["CycleID"]);
                    CCList.ActivitycycleName = Convert.ToString(row["Cycle"]);
                    CCDL.Add(CCList);
                }
            }
            return CCDL;
        }

        public List<EN.CostCenterFor> getCostCenterFor()
        {
            List<EN.CostCenterFor> CostCenterDL = new List<EN.CostCenterFor>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetCostCenterFor();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CostCenterFor COstist = new EN.CostCenterFor();
                    COstist.CodeCenterID = Convert.ToString(row["CenterID"]);
                    COstist.CodeCenterType = Convert.ToString(row["CenterName"]);
                    CostCenterDL.Add(COstist);
                }
            }
            return CostCenterDL;
        }
        public List<EN.TransporterEntities> getGenerateTranspoter()
        {
            List<EN.TransporterEntities> transpoterDL = new List<EN.TransporterEntities>();
            DataTable dt = new DataTable();
            string Table = "Transporter";
            string Column = "Transid,Transname";
            string Condition = "";
            string OrderBy = "Transname";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TransporterEntities transpoterList = new EN.TransporterEntities();
                    transpoterList.TransID = Convert.ToInt32(row["Transid"]);
                    transpoterList.TransName = Convert.ToString(row["Transname"]);
                    transpoterDL.Add(transpoterList);
                }
            }
            return transpoterDL;
        }

        public List<EN.DriverPaymentEntities> getDriversdetails(string Fromdate, string todate, int transpoterid)
        {

            List<EN.DriverPaymentEntities> DriversdetailsList = new List<EN.DriverPaymentEntities>();
            DataTable Driversdetails = new DataTable();
            int Count = 1;
            Driversdetails = trackerdatamanager.GetDriverDetails(Fromdate, todate, transpoterid);

            if (Driversdetails != null)
            {
                foreach (DataRow row in Driversdetails.Rows)
                {

                    EN.DriverPaymentEntities driverpaymentdataList = new EN.DriverPaymentEntities();

                    driverpaymentdataList.SrNo = Convert.ToInt32(Count++);
                    driverpaymentdataList.CUSTOMER_CODE = Convert.ToString(row["BankID"]);
                    driverpaymentdataList.BENEFICARY_BRANCH_CODE = Convert.ToString(row["BENEFICARY_BRANCH_CODE"]);
                    driverpaymentdataList.AMOUNT = Convert.ToString(row["AmountBank"]);
                    driverpaymentdataList.REMARKS_1 = Convert.ToString("-");
                    driverpaymentdataList.DEBIT_ACCOUNT_NO = Convert.ToString(row["DEBIT_ACCOUNT_NO"]);
                    driverpaymentdataList.CUSTOMER_NAME = Convert.ToString(row["CUSTOMER_NAME"]);
                    driverpaymentdataList.CITY = Convert.ToString(row["CITY"]);
                    driverpaymentdataList.CREDIT_ACCOUNT_NO = Convert.ToString(row["driver_accno"]);
                    driverpaymentdataList.BENEFICARY_NAME = Convert.ToString(row["DRIVER_Name"]);
                    driverpaymentdataList.REMARK_2 = Convert.ToString(row["Driver_CITY"]);
                    driverpaymentdataList.PRODUCT_CODE = Convert.ToString(row["PRODUCT_CODE"]);
                    driverpaymentdataList.E_MAIL_ID = Convert.ToString(row["EmailIDBank"]);
                    driverpaymentdataList.REMARK_3 = Convert.ToString(row["REMARK_VoucherNo"]);
                    driverpaymentdataList.REMARK_4 = Convert.ToString(row["REMARK_4"]);
                    driverpaymentdataList.FILE_NAME = Convert.ToString(row["FILE_NAME"]);
                    DriversdetailsList.Add(driverpaymentdataList);
                }
            }



            return DriversdetailsList;
        }

        public List<EN.CargoType> getContainerTypeType()
        {
            List<EN.CargoType> ContainerDL = new List<EN.CargoType>();
            DataTable dt = new DataTable();
            int Count = 1;
            dt = trackerdatamanager.GetContainerType();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.CargoType Container = new EN.CargoType();
                    Container.Cargotype = Convert.ToString(row["FuelGroupType"]);
                    Container.Cargotypeid = Convert.ToInt32(row["id"]);
                    ContainerDL.Add(Container);
                }
            }



            return ContainerDL;
        }
        // Codes By Prashant
        public List<EN.VoucherDetailsEntities> GetVoucherDetails(string FromDate, string ToDate)
        {
            List<EN.VoucherDetailsEntities> VoucherDetailsDL = new List<EN.VoucherDetailsEntities>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.VoucherDetails(FromDate, ToDate);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.VoucherDetailsEntities Voucher = new EN.VoucherDetailsEntities();

                    Voucher.VoucherDate = Convert.ToString(row["VoucherDate"]);
                    Voucher.TrailerNo = Convert.ToString(row["TrailerNo"]);
                    Voucher.Status = Convert.ToString(row["Status"]);
                    Voucher.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    Voucher.Size = Convert.ToString(row["Size"]);
                    Voucher.Drivername = Convert.ToString(row["Drivername"]);
                    Voucher.Activity = Convert.ToString(row["Activity"]);
                    Voucher.AmountCash = Convert.ToString(row["AmountCash"]);
                    Voucher.AmountBank = Convert.ToString(row["AmountBank"]);
                    Voucher.Fuel = Convert.ToString(row["Fuel"]);
                    Voucher.AdjustmentsFuel = Convert.ToString(row["AdjustmentsFuel"]);
                    Voucher.FromLocation = Convert.ToString(row["FromLocation"]);
                    Voucher.ToLocation = Convert.ToString(row["ToLocation"]);
                    Voucher.StartReading = Convert.ToString(row["StartReading"]);
                    Voucher.EndReading = Convert.ToString(row["EndReading"]);
                    Voucher.VoucherNO = Convert.ToString(row["VoucherNo"]);
                    Voucher.SlipNo = Convert.ToString(row["SlipNo"]);
                    Voucher.InOut = Convert.ToString(row["InOut"]);
                    Voucher.ENTRY_Type = Convert.ToString(row["ENTRY_Type"]);
                    Voucher.CustomerName = Convert.ToString(row["CustomerName"]);
                    Voucher.CustomerAddress = Convert.ToString(row["CustomerAddress"]);
                    Voucher.TrollySize = Convert.ToString(row["TrollySize"]);
                   

                    VoucherDetailsDL.Add(Voucher);
                }
            }



            return VoucherDetailsDL;
        }
        // Codes end By Prashant

        public List<EN.VoucherDetailsEntities> GetDieselSlip(string FromDate, string ToDate)
        {
            List<EN.VoucherDetailsEntities> VoucherDetailsDL = new List<EN.VoucherDetailsEntities>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GETDieselSlipDetails(FromDate, ToDate);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.VoucherDetailsEntities Voucher = new EN.VoucherDetailsEntities();

                    Voucher.SlipID = Convert.ToString(row["slipID"]);
                    Voucher.TrailerNo = Convert.ToString(row["TrailerNo"]);
                    Voucher.Drivername = Convert.ToString(row["Drivername"]);
                    Voucher.Fuel = Convert.ToString(row["Fuel"]);
                    Voucher.SlipNo = Convert.ToString(row["SlipNo"]);
                    Voucher.Transpoter = Convert.ToString(row["Transporter"]);
                    Voucher.SlipDate = Convert.ToString(row["Slipdate"]);
                   


                    VoucherDetailsDL.Add(Voucher);
                }
            }



            return VoucherDetailsDL;
        }


        public List<EN.VoucherDetailsEntities> GetVoucherEditDetails(string Voucherno, string ActivityType)
        {
            List<EN.VoucherDetailsEntities> VoucherDetailsDL = new List<EN.VoucherDetailsEntities>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetVoucherEditDetails(Voucherno, ActivityType);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.VoucherDetailsEntities Voucher = new EN.VoucherDetailsEntities();

                    Voucher.VoucherNO = Convert.ToString(row["Voucherno"]);
                    Voucher.TrailerNo = Convert.ToString(row["TrailerNo"]);
                    Voucher.Drivername = Convert.ToString(row["Drivername"]);
                    Voucher.Fuel = Convert.ToString(row["Fuel"]);
                    Voucher.SlipNo = Convert.ToString(row["SlipNo"]);
                    Voucher.Transpoter = Convert.ToString(row["transname"]);
                    Voucher.FromLocation = Convert.ToString(row["FromLocation"]);
                    Voucher.ToLocation = Convert.ToString(row["ToLocation"]);
                    Voucher.AmountBank = Convert.ToString(row["AmountBank"]);
                    Voucher.AmountCash = Convert.ToString(row["AmountCash"]);
                    Voucher.remarks = Convert.ToString(row["modifyremarks"]);
                    Voucher.LocationID = Convert.ToInt32(row["TOLocationID"]);
                    Voucher.Driverid = Convert.ToInt32(row["driverid"]);
                    Voucher.FromLocationIDd = Convert.ToInt32(row["From ID"]);
                    Voucher.TOLocationIDd = Convert.ToInt32(row["To ID"]);
                    Voucher.ActivityAutoid = Convert.ToString(row["Activity ID"]);
                    Voucher.Fastagamount = Convert.ToString(row["fastagamount"]);
                    Voucher.AdjustmentsFuel = Convert.ToString(row["adjustmentsfuel"]);
                    Voucher.Count = Convert.ToString(row["Count"]);
                    Voucher.VoucherDate = Convert.ToString(row["voucherdate"]);


                    VoucherDetailsDL.Add(Voucher);
                }
            }



            return VoucherDetailsDL;
        }




        //Code Start by Rahul
        public List<EN.PendingAdditionalMovementRequestEntities> GetPendingAdditionalMovementRequest()
        {
            DataTable ContainerDL = new DataTable();
            ContainerDL = trackerdatamanager.GetPendningAdditionalmovmentDetails();
            List<EN.PendingAdditionalMovementRequestEntities> GetPendingAdditionalMovment = new List<EN.PendingAdditionalMovementRequestEntities>();

            if (ContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDL.Rows)
                {
                    EN.PendingAdditionalMovementRequestEntities DL = new EN.PendingAdditionalMovementRequestEntities();
                    DL.RequestID = Convert.ToString(row["RequestID"]);
                    DL.RequestOn = Convert.ToString(row["RequestOn"]);
                    DL.VoucherNo = Convert.ToString(row["VoucherNo"]);
                    DL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    DL.Activity = Convert.ToString(row["Activity"]);
                    DL.FromLocation = Convert.ToString(row["FromLocation"]);
                    DL.ToLocation = Convert.ToString(row["ToLocation"]);
                    DL.Remarks = Convert.ToString(row["Remarks"]);
                    DL.FuelQuanity = Convert.ToString(row["FuelQuanity"]);
                    DL.Kilometer = Convert.ToString(row["kilometer"]);
                    GetPendingAdditionalMovment.Add(DL);
                }
            }
            return GetPendingAdditionalMovment;
        }
        public string GenerateRequest(int RequestID, int UserID)
        {

            string message = "";
            DataTable ApproveDL = new DataTable();
            ApproveDL = trackerdatamanager.GenerateRequest(RequestID, UserID);
            if (ApproveDL.Rows.Count > 0)
            {
                message = Convert.ToString(ApproveDL.Rows[0][0]);
            }
            else
            {
                message = "Record not saved successfully. Please Try again!";
            }
            return message;
        }
        //Code End by Rahul


        public List<EN.DriversEntities> getIcdDrivers()
        {
            List<EN.DriversEntities> DriversDL = new List<EN.DriversEntities>();
            DataTable dt = new DataTable();
            string Table = "ICD_Internal_Driver";
            string Column = "Driverid,DriverName";
            string Condition = "";
            string OrderBy = "DriverName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DriversEntities DriversList = new EN.DriversEntities();
                    DriversList.driverID = Convert.ToInt32(row["Driverid"]);
                    DriversList.driverName = Convert.ToString(row["DriverName"]);
                    DriversDL.Add(DriversList);
                }
            }
            return DriversDL;
        }

        public List<EN.FuelEntities> getFuelCloseList()
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getFuelCloseList();
            List<EN.FuelEntities> FuelList = new List<EN.FuelEntities>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.FuelEntities FuelDL = new EN.FuelEntities();

                    FuelDL.TrailerNo = Convert.ToString(row["Trailername"]);
                    FuelDL.Passing = Convert.ToString(row["passing"]);
                    FuelDL.driver = Convert.ToString(row["drivername"]);
                    FuelDL.Transportor = Convert.ToString(row["transname"]);
                    FuelDL.Activity = Convert.ToString(row["Activity"]);
                    FuelDL.containerCount = Convert.ToString(row["Size"]);
                    FuelDL.Type = Convert.ToString(row["Type"]);
                    FuelDL.From = Convert.ToString(row["FromLocation"]);
                    FuelDL.To = Convert.ToString(row["toLocation"]);
                    FuelDL.planID = Convert.ToString(row["PlanID"]);
                    FuelDL.INOUT = Convert.ToString(row["Status"]);
                    FuelDL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    FuelDL.TrailerType = Convert.ToString(row["trailerType"]);
                    FuelDL.DelAddress = Convert.ToString(row["DelAddress"]);
                    FuelDL.InOutDate = Convert.ToString(row["InOutDate"]);
                    FuelDL.btnClass = Convert.ToString(row["btnCss"]);

                    FuelDL.ActivityID = Convert.ToInt32(row["ActivityID"]);

                    FuelList.Add(FuelDL);
                }

            }
            return FuelList;
        }

        //public EN.FuelEntities GetDocList(string TrailerNo)
        //{
        //    List<EN.FuelEntities> FuelList = new List<EN.FuelEntities>();
        //    //EN.FuelEntities objLRentities = new EN.FuelEntities();

        //    DataSet ds = new DataSet();
        //    ds = trackerdatamanager.DocDropDownList(TrailerNo);
        //    List<EN.FuelEntities> DocList = new List<EN.FuelEntities>();

        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            EN.FuelEntities LRList = new EN.FuelEntities();

        //            LRList.ContainerNo = Convert.ToString(row["ContainerNo"]);
        //            LRList.TrailerNo = Convert.ToString(row["Trailername"]);
        //            LRList.From = Convert.ToString(row["FromLocation"]);
        //            LRList.To = Convert.ToString(row["toLocation"]);

        //            FuelList.Add(LRList);
        //        }
        //    }


        //    return FuelList;
        //}

        //public List<EN.FuelEntities> GetDocList(string TrailerNo)
        //{
        //    DataTable dt = new DataTable();
        //    dt = trackerdatamanager.DocDropDownList(TrailerNo);
        //    List<EN.FuelEntities> FuelList = new List<EN.FuelEntities>();

        //    if (dt.Rows.Count != 0)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            EN.FuelEntities FuelDL = new EN.FuelEntities();

        //            FuelDL.ContainerNo = Convert.ToString(row["ContainerNo"]);
        //            FuelDL.TrailerNo = Convert.ToString(row["Trailername"]);
        //            FuelDL.From = Convert.ToString(row["FromLocation"]);
        //            FuelDL.To = Convert.ToString(row["toLocation"]);

        //            FuelList.Add(FuelDL);
        //        }

        //    }
        //    return FuelList;
        //}

        public EN.FuelEntities GetDocList(string TrailerNo)
        {
            EN.FuelEntities passingDL = new EN.FuelEntities();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.DocDropDownList( TrailerNo);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    passingDL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    passingDL.TrailerNo = Convert.ToString(row["Trailername"]);
                    passingDL.From = Convert.ToString(row["FromLocation"]);
                    passingDL.To = Convert.ToString(row["toLocation"]);


                }
            }
            return passingDL;
        }

        // Code By Prashant

        public List<EN.ScanTypeEntities> getScanType()
        {
            List<EN.ScanTypeEntities> ScanTypeDL = new List<EN.ScanTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "scan_type";
            string Column = "ScanID,ScanType";
            string Condition = "";
            string OrderBy = "ScanType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ScanTypeEntities ScanList = new EN.ScanTypeEntities();
                    ScanList.ScanID = Convert.ToInt32(row["ScanID"]);
                    ScanList.ScanType = Convert.ToString(row["ScanType"]);
                    ScanTypeDL.Add(ScanList);
                }
            }
            return ScanTypeDL;
        }

        public List<EN.VehicleTypeEntities> getVehicleTypeDetails()
        {
            List<EN.VehicleTypeEntities> VehicleTypeDL = new List<EN.VehicleTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "VehicleType_M";
            string Column = "VehicleTypeID,VehicleType";
            string Condition = "";
            string OrderBy = "VehicleType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.VehicleTypeEntities ScanList = new EN.VehicleTypeEntities();
                    ScanList.VehicleTypeID = Convert.ToInt32(row["VehicleTypeID"]);
                    ScanList.VehicleType = Convert.ToString(row["VehicleType"]);
                    VehicleTypeDL.Add(ScanList);
                }
            }
            return VehicleTypeDL;
        }

        public List<EN.DriverTypeEntities> getDriverTypeDetails()
        {
            List<EN.DriverTypeEntities> VehicleTypeDL = new List<EN.DriverTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "Driver_type";
            string Column = "DrivertypeID,DriverType";
            string Condition = "";
            string OrderBy = "DriverType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DriverTypeEntities ScanList = new EN.DriverTypeEntities();
                    ScanList.DriverID = Convert.ToInt32(row["DrivertypeID"]);
                    ScanList.DriverType = Convert.ToString(row["DriverType"]);
                    VehicleTypeDL.Add(ScanList);
                }
            }
            return VehicleTypeDL;
        }
        public List<EN.UploadVoucherTariffEntities> UpdateVoucherTraiffDetails(DataTable VOucherTraifftDT, Int32 userId)
        {
            //EN.DriverPaymentReco objdriverpaymententities = new EN.DriverPaymentReco();
            List<EN.UploadVoucherTariffEntities> VOucherList = new List<EN.UploadVoucherTariffEntities>();
            if (VOucherTraifftDT != null)
            {
                int count = 1;
                foreach (DataRow row in VOucherTraifftDT.Rows)
                {
                    EN.UploadVoucherTariffEntities DPDTList = new EN.UploadVoucherTariffEntities();
                    DPDTList.SrNo = Convert.ToString(count++);
                    DPDTList.Transporter = Convert.ToString(row["Transporter"]);
                    DPDTList.EffectiveFrom = Convert.ToString(row["Effective From"]);
                    DPDTList.EffectiveUpto = Convert.ToString(row["Effective Upto"]);
                    DPDTList.Activity = Convert.ToString(row["Activity"]);
                    DPDTList.Status = Convert.ToString(row["STATUS"]);
                    DPDTList.InOut = Convert.ToString(row["In/OUT"]);
                    DPDTList.ContainerTYpe = Convert.ToString(row["Container Type"]);
                    DPDTList.Size = Convert.ToString(row["Size"]);
                    DPDTList.FromLocation = Convert.ToString(row["From Location"]);
                    DPDTList.TOlocation = Convert.ToString(row["To Location"]);
                    DPDTList.Passing = Convert.ToString(row["Passing"]);
                    DPDTList.TrailerTYpe = Convert.ToString(row["Trailer Type"]);
                    DPDTList.DriverType = Convert.ToString(row["Driver Type"]);
                    DPDTList.ScanType = Convert.ToString(row["Scan Type"]);
                    DPDTList.Weight = Convert.ToString(row["Weight"]);
                    DPDTList.Fuel = Convert.ToString(row["Fuel (Liter)"]);
                    DPDTList.FuelAmount = Convert.ToString(row["Fuel (Amount)"]);
                    DPDTList.Amount1 = Convert.ToString(row["Amount1 (Cash)"]);
                    DPDTList.Amount2 = Convert.ToString(row["Amount2 (Bank)"]);
                    DPDTList.VehicleType = Convert.ToString(row["VEH TYPE"]);

                    VOucherList.Add(DPDTList);
                }
            }
            return VOucherList;
        }

        public string VoucherValidation(DataTable VoucherDetails)
        {
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";

            if (VoucherDetails != null)
            {
                foreach (DataRow row in VoucherDetails.Rows)
                {
                    DataSet PortsDS = new DataSet();
                    PortsDS = trackerdatamanager.GetVOucherUploadValidation(Convert.ToString(row["Activity"]), Convert.ToString(row["FromLocation"]), Convert.ToString(row["TOlocation"]));
                    if (PortsDS.Tables[0].Rows.Count == 0)
                    {
                        if (message == "")
                        {
                            message = "Following Activity not found in database: \n" + Convert.ToString(row["Activity"]);
                        }
                        else
                        if (Convert.ToString(row["Activity"]).Contains(message))
                        {
                        }
                        else
                        {
                            message += "," + Convert.ToString(row["Activity"]);
                        }
                    }
                    if (PortsDS.Tables[1].Rows.Count == 0)
                    {
                        if (message1 == "")
                        {
                            message1 = "Following From Location not found in database: \n" + Convert.ToString(row["FromLocation"]);
                        }
                        else
                        {
                            if (Convert.ToString(row["FromLocation"]).Contains(message1))
                            {
                            }
                            else
                            {
                                message1 += "," + Convert.ToString(row["FromLocation"]);
                            }
                        }
                    }
                    if (PortsDS.Tables[2].Rows.Count == 0)
                    {
                        if (message3 == "")
                        {
                            message3 = "Following To Location not found in database: \n" + Convert.ToString(row["TOlocation"]);
                        }
                        else
                        {
                            if (Convert.ToString(row["TOlocation"]).Contains(message3))
                            {
                            }
                            else
                            {
                                message3 += "," + Convert.ToString(row["TOlocation"]);
                            }
                        }
                    }
                }
                if ((message != "") || (message1 != "") || (message3 != ""))
                {
                    message2 = message + "\n" + message1 + "\n" + message3;
                }
            }
            return message2;
        }




        public EN.UploadVoucherTariffEntities SaveVOucherLIstList(DataTable Itemdata, int UserID, DateTime entrydate)
        {
            //string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("UserID", UserID);
            parameterList.Add("entryDate", entrydate);
            DataSet ds = new DataSet();

            ds = db.AddTypeRoadPlanningTableData("USP_INSERTVOUCHERTRAIFFUPLOAD", parameterList, Itemdata, "PT_VoucherTraiffUpload", "@PT_VoucherTraiffUpload");


            //int i = db.SaveContainerList("USP_TainPlanningInsertContainerList", parameterList, Itemdata);

            // VendorDataDL = objgetMyREQDBManager.ApprovedStatusList(OrderID, companyid);

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            EN.UploadVoucherTariffEntities VoucherdetailsLIst = new EN.UploadVoucherTariffEntities();

            dt = ds.Tables[0];
            dt1 = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                VoucherdetailsLIst.validationMessage = Convert.ToString(dt.Rows[0][0]);
                VoucherdetailsLIst.TranspoterList = Convert.ToString(dt1.Rows[0][0]);
            }
            // Message = "Record saved successfully";
            return VoucherdetailsLIst;


            // Message = "Record saved successfully";
            ///return Message;



        }

        public string CancelVoucherTariff(DataTable dataTable, int userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            // parameterList.Add("Voucherno", Voucherno);s
            parameterList.Add("Userid", userid);
            int i = db.AddTypeTableData("USP_CancelVouchertariffDetails", parameterList, dataTable, "PT_CancelVoucherTariff", "@PT_CancelVoucherTariff");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public List<EN.ContainerSize> getContainerSize()
        {
            List<EN.ContainerSize> ContainerSizeDL = new List<EN.ContainerSize>();
            DataTable ContainerSizeDT = new DataTable();
            string Table = "ContainerSize";
            string Column = "ContainerSizeID,ContainerSize";
            string Condition = "";
            string OrderBy = "ContainerSize";

            ContainerSizeDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ContainerSizeDT != null)
            {
                foreach (DataRow row in ContainerSizeDT.Rows)
                {
                    EN.ContainerSize ContainerSizeList = new EN.ContainerSize();
                    ContainerSizeList.ContainerSizeID = Convert.ToInt32(row["ContainerSizeID"]);
                    ContainerSizeList.ContainerSizeName = Convert.ToString(row["ContainerSize"]);
                    ContainerSizeDL.Add(ContainerSizeList);
                }
            }
            return ContainerSizeDL;
        }

        public List<EN.CargoType> getCargoType()
        {
            List<EN.CargoType> CargoTypeDL = new List<EN.CargoType>();
            DataTable CargoTypeDT = new DataTable();
            string Table = "CargoType";
            string Column = "Cargotypeid,Cargotype";
            string Condition = "";
            string OrderBy = "Cargotype";

            CargoTypeDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CargoTypeDT != null)
            {
                foreach (DataRow row in CargoTypeDT.Rows)
                {
                    EN.CargoType CargoTypeList = new EN.CargoType();
                    CargoTypeList.Cargotypeid = Convert.ToInt32(row["Cargotypeid"]);
                    CargoTypeList.Cargotype = Convert.ToString(row["Cargotype"]);
                    CargoTypeDL.Add(CargoTypeList);
                }
            }
            return CargoTypeDL;
        }

        public List<EN.VehicleTypeEntities> getVehicleDetails()
        {
            List<EN.VehicleTypeEntities> VehicleTypeDL = new List<EN.VehicleTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "Tariff_VehicleType";
            string Column = "VehicleTypeId,VehicleType";
            string Condition = "";
            string OrderBy = "VehicleType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.VehicleTypeEntities CargoTypeList = new EN.VehicleTypeEntities();
                    CargoTypeList.VehicleTypeID = Convert.ToInt32(row["VehicleTypeId"]);
                    CargoTypeList.VehicleType = Convert.ToString(row["VehicleType"]);
                    VehicleTypeDL.Add(CargoTypeList);
                }
            }
            return VehicleTypeDL;
        }

        public EN.ActivityCycle Getactivitycycle(int activityid,string activitype)
        {
            EN.ActivityCycle passingDL = new EN.ActivityCycle();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.Getactivitymaster(activityid, activitype);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    passingDL.ActivitycycleName = Convert.ToString(row["Cycle"]);
                   


                }
            }
            return passingDL;
        }

        //public string UpdateVoucherDetails(string VoucherNo, string Fuel, string AountBank, string AmountCash, int Userid, string ActivityType, string Remarks, int fromlocationID, int ToLocationID, string ActivityID, string FastagAmount)
        //{
        //    int i = 0;
        //    i = trackerdatamanager.UpdateVoucherdetails(VoucherNo, Fuel, AountBank, AmountCash, Userid, ActivityType, Remarks, fromlocationID, ToLocationID, ActivityID, FastagAmount);
        //    string message;
        //    if (i > 0)
        //    {
        //        message = "Record modified Successfully.";
        //    }
        //    else
        //    {
        //        message = "Record not modify, Please try again!";
        //    }
        //    return message;
        //}
        public string UpdateVoucherDetails(string VoucherNo, string Fuel, string AountBank, string AmountCash, int Userid, string ActivityType, string Remarks,
        int fromlocationID, int ToLocationID, string ActivityID, string Fastagamount,string adjFuel, DataTable dataTable)
        {

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            string message = "";

            DataTable VendorDataDL = new DataTable();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("VoucherNo", VoucherNo);
            parameterList.Add("Fuel", Fuel);
            parameterList.Add("amountbank", AountBank);
            parameterList.Add("AmountCash", AmountCash);
            parameterList.Add("Userid", Userid);
            parameterList.Add("activity", ActivityType);
            parameterList.Add("remakrs", Remarks);
            parameterList.Add("fromLocationid", fromlocationID);
            parameterList.Add("tolocationid", ToLocationID);
            parameterList.Add("activityid", ActivityID);
            parameterList.Add("fastagamount", Fastagamount);
            parameterList.Add("adjFuel", adjFuel);


            VendorDataDL = db.DataTableAddTypeTable("USP_UpdateVehicleDetails", parameterList, dataTable, "PT_AddContainerForUpdate", "@PT_AddContainerForUpdate");
            message = "Record saved successfully!";

            return message;
        }

        public string CancelVoucherDetails(string VoucherNo, string CancelRemarks, int Userid)
        {
            int i = 0;
            i = trackerdatamanager.CancelVoucherdetails(VoucherNo, CancelRemarks, Userid);
            string message;
            if (i > 0)
            {
                message = "Record Cancelled Successfully.";
            }
            else
            {
                message = "Record not Cancelled, Please try again!";
            }
            return message;
        }
        public string REPrintFuelVoucherDetails(string VoucherNo, string ActivityType, string Remarks, int Userid)
        {
            int i = 0;
            i = trackerdatamanager.ReprintfuelVoucherdetails(VoucherNo, ActivityType, Remarks, Userid);
            string message;
            if (i > 0)
            {
                message = "Record saved Successfully.";
            }
            else
            {
                message = "Record not saved, Please try again!";
            }
            return message;
        }

        public string REPrintVoucherSlipVoucherDetails(string VoucherNo, string ActivityType, string Remarks, int Userid)
        {
            int i = 0;
            i = trackerdatamanager.ReprintVoucherSlipVoucherdetails(VoucherNo, ActivityType, Remarks, Userid);
            string message;
            if (i > 0)
            {
                message = "Record saved Successfully.";
            }
            else
            {
                message = "Record not saved, Please try again!";
            }
            return message;
        }


        public string GenerateFuelSLip(string Fuel, string AountBank, string AmountCash,
            string VoucherNo, string ActivityType, string Tolocation, string FromLocation,
            string DriverName, int Userid,string Transporter)
        {
            int i = 0;
            i = trackerdatamanager.GenerateFuelSlip(Fuel, AountBank, AmountCash, VoucherNo, ActivityType, Tolocation, FromLocation, DriverName, Userid, Transporter);
            string message;
            if (i > 0)
            {
                message = "Record saved Successfully.";
            }
            else
            {
                message = "Record not saved, Please try again!";
            }
            return message;
        }

        public string ChangeDriverFromVoucher(string Fuel, string AountBank, string AmountCash,
            string VoucherNo, string ActivityType, string Tolocation, string FromLocation,
            string DriverName, int Userid)
        {
            int i = 0;
            i = trackerdatamanager.ChangeVoucherDriver(Fuel, AountBank, AmountCash, VoucherNo, ActivityType, Tolocation, FromLocation, DriverName, Userid);
            string message;
            if (i > 0)
            {
                message = "Record saved Successfully.";
            }
            else
            {
                message = "Record not saved, Please try again!";
            }
            return message;
        }

        public string ChangeSizeFromVoucher(string VoucherNo, string ActivityType, string containerCount, int Userid)
        {
            int i = 0;
            i = trackerdatamanager.ChangeSizeFromVoucher(VoucherNo, ActivityType, containerCount, Userid);
            string message;
            if (i > 0)
            {
                message = "Record saved Successfully.";
            }
            else
            {
                message = "Record not saved, Please try again!";
            }
            return message;
        }


        public string GetVehicleStatusCancel(string transid, string Remarks, int UserID)
        {

            string message = "";
            DataTable dt = new DataTable();
            dt = trackerdatamanager.SetVehicleCancelStatus(transid, Remarks, UserID);


            return message;
        }

        public string validateUser(int USERID)
        {
            string message = "";
            DataTable dt = new DataTable();
            dt = trackerdatamanager.ValidateUser(USERID);
            if (dt != null)
            {
                try
                {
                    message = Convert.ToString(dt.Rows[0]["userid"]);

                }
                catch (Exception ex)
                {

                    return message = "";
                }

            }

            return message;
        }

        public List<EN.VehicleTypeEntities> getTrailerTypeDetails()
        {
            List<EN.VehicleTypeEntities> VehicleTypeDL = new List<EN.VehicleTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "trailerType";
            string Column = "ID,trailerType";
            string Condition = "";
            string OrderBy = "trailerType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.VehicleTypeEntities ScanList = new EN.VehicleTypeEntities();
                    ScanList.VehicleTypeID = Convert.ToInt32(row["ID"]);
                    ScanList.VehicleType = Convert.ToString(row["trailerType"]);
                    VehicleTypeDL.Add(ScanList);
                }
            }
            return VehicleTypeDL;
        }


        public List<EN.VoucherDetailsEntities> GetVoucherCloseDetails(string Voucherno)
        {
            List<EN.VoucherDetailsEntities> VoucherDetailsDL = new List<EN.VoucherDetailsEntities>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetVoucherCloseDetails(Voucherno);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.VoucherDetailsEntities Voucher = new EN.VoucherDetailsEntities();

                    Voucher.VoucherNO = Convert.ToString(row["Voucherno"]);
                    Voucher.VoucherDate = Convert.ToString(row["VoucherDate"]);
                    Voucher.TrailerNo = Convert.ToString(row["TrailerNo"]);
                    Voucher.Drivername = Convert.ToString(row["Drivername"]);
                    Voucher.Fuel = Convert.ToString(row["Fuel"]);
                    Voucher.SlipNo = Convert.ToString(row["SlipNo"]);
                    Voucher.Transpoter = Convert.ToString(row["transname"]);
                    Voucher.FromLocation = Convert.ToString(row["FromLocation"]);
                    Voucher.ToLocation = Convert.ToString(row["ToLocation"]);
                    Voucher.AmountBank = Convert.ToString(row["AmountBank"]);
                    Voucher.AmountCash = Convert.ToString(row["AmountCash"]);
                    Voucher.remarks = Convert.ToString(row["modifyremarks"]);
                    Voucher.LocationID = Convert.ToInt32(row["TOLocationID"]);
                    Voucher.Driverid = Convert.ToInt32(row["driverid"]);
                    Voucher.FromLocationIDd = Convert.ToInt32(row["From ID"]);
                    Voucher.TOLocationIDd = Convert.ToInt32(row["To ID"]);
                    Voucher.ActivityAutoid = Convert.ToString(row["Activity ID"]);
                    Voucher.ActivityName = Convert.ToString(row["ActivityName"]);
                    Voucher.ContainerNo = Convert.ToString(row["containerno1"]);
                    Voucher.ContainerSize = Convert.ToString(row["count"]);



                    VoucherDetailsDL.Add(Voucher);
                }
            }



            return VoucherDetailsDL;
        }


        public string GetCloseDetails(string VoucherNo1, string Trailername, string Activityname, string FromID, string Toid, int Userid)
        {
            string message = "";
            DataTable dt = new DataTable();
            dt = trackerdatamanager.CLoseDetails(VoucherNo1, Trailername, Activityname, FromID, Toid, Userid);
            //if (dt != null)
            //{
            //    try
            //    {
            //        message = Convert.ToString(dt.Rows[0]["userid"]);

            //    }
            //    catch (Exception ex)
            //    {

            //        return message = "";
            //    }

            //}

            return message;
        }
        //code by rahul 069-11-2019
        public EN.FuelEntities AddDirectFuel(string containerCount, int containerTypeID, string TrailerNo, string Passing, int driverID, int TransportorID, int FromID,
            int ToID, int Activity, string ReadingFrom, string ReadingTo, string fuel, string Amount1, string Amount2, string AdjustFuel, int Userid,
            string PlanID, string INOUT, string ContainerNo, int TarrifID, string VehTransID, string Remarks, string MovType,
            string CostCenter, string CostCenterFor, string TotalWeight, string Fuelamount,string Fastagamount, string AdvanceFuel, string AdvanceCash, string AdvanceBank, string LoanID, DataTable FuelEntitiesContainerList)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            EN.FuelEntities vendorList = new EN.FuelEntities();

            DataTable VendorDataDL = new DataTable();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("containerCount", containerCount);
            parameterList.Add("containerTypeID", containerTypeID);
            parameterList.Add("TrailerNo", TrailerNo);
            parameterList.Add("passing", Passing);
            parameterList.Add("driverID", driverID);
            parameterList.Add("TransportorID", TransportorID);
            parameterList.Add("FromID", FromID);
            parameterList.Add("ToID", ToID);
            parameterList.Add("Activity", Activity);
            parameterList.Add("ReadingFrom", ReadingFrom);
            parameterList.Add("ReadingTo", ReadingTo);
            parameterList.Add("fuel", fuel);
            parameterList.Add("Amount1", Amount1);
            parameterList.Add("Amount2", Amount2);
            parameterList.Add("AdjustFuel", AdjustFuel);
            parameterList.Add("UserID", Userid);
            parameterList.Add("PlanID", PlanID);
            parameterList.Add("InOut", INOUT);
            parameterList.Add("ContainerNo", ContainerNo);
            parameterList.Add("TarrifID", TarrifID);
            parameterList.Add("VehTransID", VehTransID);
            parameterList.Add("Remarks", Remarks);
            parameterList.Add("MovType", MovType);
            parameterList.Add("CostCenter", CostCenter);
            parameterList.Add("CostCenterFor", CostCenterFor);
            parameterList.Add("TotalWeight", TotalWeight);
            parameterList.Add("Fuelamount", Fuelamount);
            parameterList.Add("Fastagamount", Fastagamount);
            parameterList.Add("AdvanceFuel", AdvanceFuel);
            parameterList.Add("AdvanceCash", AdvanceCash);
            parameterList.Add("AdvanceBank", AdvanceBank);
            parameterList.Add("LoanID", LoanID);


            /*VendorDataDL = trackerdatamanager.AddDirectFuel(containerCount, containerTypeID, TrailerNo, Passing, driverID, TransportorID, FromID, ToID, Activity, ReadingFrom, ReadingTo, fuel, Amount1, Amount2, AdjustFuel, Userid, PlanID, INOUT, ContainerNo, TarrifID, VehTransID, Remarks);*/

            VendorDataDL = db.DataTableAddTypeTable("USP_FuelDirectInsertDetails", parameterList, FuelEntitiesContainerList, "PT_ContainerListForFuel", "@PT_ContainerListForFuel");

            if (VendorDataDL.Rows.Count > 0)
            {
                foreach (DataRow row in VendorDataDL.Rows)
                {
                    vendorList.VoucherNo = Convert.ToString(row["VoucherNo"]);
                    vendorList.VoucherDate = Convert.ToString(row["VoucherDate"]);
                }
            }
            return vendorList;

        }
        public EN.FuelEntities getFuelDataForDirect(int TransportorID, int Activity, int Type, string containerCount, int from, int to, string Inout, string TrailerType, string Passing, string driverid, string TrailerNo, string TotalWeight, long VehTransID,string ScanTypeID, string ScanCount)
        {
            DataTable dt = new DataTable();
            EN.FuelEntities FuelDL = new EN.FuelEntities();
            dt = trackerdatamanager.getFuelData(TransportorID, Activity, Type, containerCount, from, to, Inout, TrailerType, Passing, driverid, TrailerNo, TotalWeight, VehTransID,ScanTypeID, ScanCount);
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    //PassingList.PassingID = Convert.ToInt32(row["PassingID"]);
                    FuelDL.amount1 = Convert.ToString(row["fuelamount"]);
                    FuelDL.fuel = Convert.ToString(row["fuel"]);
                    FuelDL.amount2 = Convert.ToString(row["fuelamount1"]);
                    FuelDL.fuelAmount = Convert.ToString(row["DieselFuelamount"]);
                    FuelDL.AutoID = Convert.ToInt32(row["AutoID"]);
                    FuelDL.Fastagamount = Convert.ToString(row["FastagAmount"]);
                }
            }
            return FuelDL;
        }
        public EN.FuelEntities GetFuelData(string TrailerNo, string ContainerNo, string Activity, int ActivityTypeID, string LocationYardID)
        {
            EN.FuelEntities FuelDL = new EN.FuelEntities();
            List<EN.FuelEntitiesContainerList> FuelEntitiesContainerList = new List<EN.FuelEntitiesContainerList>();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            dt = trackerdatamanager.getFuelDataForGenerate(TrailerNo, ContainerNo, Activity, ActivityTypeID, LocationYardID);
            dt1 = trackerdatamanager.getContainerListForFuelGenerate(TrailerNo, Activity, ActivityTypeID, ContainerNo, LocationYardID);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FuelDL.ActivityID = Convert.ToInt32(row["ActivityID"]);
                    //FuelDL.containerCount = Convert.ToString(row["Size"]);
                    FuelDL.ConTypeID = Convert.ToInt32(row["ContainerTypeiD"]);
                    FuelDL.TransportorID = Convert.ToInt32(row["TransporterID"]);
                    FuelDL.TrailerTransid = Convert.ToInt32(row["TrailerTransid"]);

                    FuelDL.driverID = Convert.ToInt32(row["driverID"]);
                    FuelDL.FromID = Convert.ToInt32(row["FromID"]);
                    FuelDL.ToID = Convert.ToInt32(row["toID"]);
                    FuelDL.Passing = Convert.ToString(row["passing"]);
                    FuelDL.TrailerID = Convert.ToInt32(row["trailerId"]);
                    FuelDL.INOUT = Convert.ToString(row["Status"]);
                    FuelDL.planID = Convert.ToString(row["transid"]);
                    FuelDL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    FuelDL.TrailerType = Convert.ToString(row["trailerType"]);
                    FuelDL.InOutDate = Convert.ToString(row["InOutDate"]);
                    FuelDL.VehTransID = Convert.ToInt64(row["TransID"]);
                    FuelDL.validation = Convert.ToInt32(row["validation"]);
                    FuelDL.Activitystatuscycle = Convert.ToString(row["validation"]);
                    FuelDL.DrivertypeID = Convert.ToString(row["drivertype"]);
                    FuelDL.ScanTypeID = Convert.ToString(row["ScanType"]);
                    FuelDL.VehicleType = Convert.ToString(row["VehicleGroup"]);
                    FuelDL.Shifting = Convert.ToString(row["isshifting"]);
                    FuelDL.LocationYardID = Convert.ToString(LocationYardID);
                    if ((FuelDL.containerCount == "1 X 20") || (FuelDL.containerCount == "1 X 40") || (FuelDL.containerCount == "2 X 20"))
                    {
                        FuelDL.Mov_ID = 1;
                    }
                    else if (FuelDL.containerCount == "PL")
                    {
                        FuelDL.Mov_ID = 2;
                    }
                   
                     else if ((FuelDL.containerCount == "1 X COIL") || (FuelDL.containerCount == "2 X COIL") || (FuelDL.containerCount == "3 X COIL"))
                    {
                        FuelDL.Mov_ID = 4;
                    }
                    else
                    {
                        FuelDL.Mov_ID = 3;
                    }
                }
            }
            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    FuelDL.CntCount = 1;
                    double TotWeight = 0;
                    foreach (DataRow row in dt1.Rows)
                    {
                        EN.FuelEntitiesContainerList CNDL = new EN.FuelEntitiesContainerList();
                        CNDL.ContainerNo = Convert.ToString(row["Container No"]);
                        CNDL.Size = Convert.ToString(row["Size"]);
                        CNDL.Weight = Convert.ToString(row["Weight"]);
                        CNDL.JoNo = Convert.ToString(row["JoNoEntryID"]);
                        CNDL.CargoType = Convert.ToString(row["CargoType"]);
                        CNDL.ScanType = Convert.ToString(row["ScanType"]);
                        TotWeight += Convert.ToDouble(row["Weight"]);
                        FuelDL.containerCount = Convert.ToString(row["CSize"]);
                        FuelEntitiesContainerList.Add(CNDL);
                    }
                    FuelDL.TotalWeight = Convert.ToString(TotWeight);
                }
                else
                {
                    FuelDL.CntCount = 0;
                }
            }

            FuelDL.FuelEntitiesContainerList = FuelEntitiesContainerList;
            return FuelDL;
        }
        //code end by rahul 09-11-2019


        public List<EN.DriverTypeEntities> getDriverTypeDetailsList(int TransportName)
        {
            List<EN.DriverTypeEntities> VehicleTypeDL = new List<EN.DriverTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "Driver_type";
            string Column = "DrivertypeID,DriverType";
            string Condition = "Transport_tariffID =" + @TransportName + "";
            string OrderBy = "DriverType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DriverTypeEntities ScanList = new EN.DriverTypeEntities();
                    ScanList.DriverID = Convert.ToInt32(row["DrivertypeID"]);
                    ScanList.DriverType = Convert.ToString(row["DriverType"]);
                    VehicleTypeDL.Add(ScanList);
                }
            }
            return VehicleTypeDL;
        }


        public List<EN.TransporterEntities> getTranspoterDetails()
        {
            List<EN.TransporterEntities> transpoterDL = new List<EN.TransporterEntities>();
            DataTable dt = new DataTable();
            string Table = "Transport_TariffGroup";
            string Column = "GroupTariffID,GroupTariffName";
            string Condition = "GroupTariffID in(1,2,5,6,7,8)";
            string OrderBy = "GroupTariffName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TransporterEntities transpoterList = new EN.TransporterEntities();
                    transpoterList.TransID = Convert.ToInt32(row["GroupTariffID"]);
                    transpoterList.TransName = Convert.ToString(row["GroupTariffName"]);
                    transpoterDL.Add(transpoterList);
                }
            }
            return transpoterDL;
        }
        public List<EN.LocationMaster> getLocationG()
        {
            List<EN.LocationMaster> locationDL = new List<EN.LocationMaster>();
            DataTable dt = new DataTable();
            string Table = "Ext_Location_G";
            string Column = "LocationGroupID,LocationGroupName";
            string Condition = "";
            string OrderBy = "LocationGroupName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationMaster locationList = new EN.LocationMaster();
                    locationList.LocationID = Convert.ToInt32(row["LocationGroupID"]);
                    locationList.Location = Convert.ToString(row["LocationGroupName"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<EN.ScanTypeEntities> GetSanTypeDetails()
        {
            List<EN.ScanTypeEntities> locationDL = new List<EN.ScanTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "Scan_type";
            string Column = "ScanID,ScanType";
            string Condition = "";
            string OrderBy = "Scantype";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ScanTypeEntities locationList = new EN.ScanTypeEntities();
                    locationList.ScanID = Convert.ToInt32(row["ScanID"]);
                    locationList.ScanType = Convert.ToString(row["ScanType"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<EN.FuelBillverificationsEntiites> GetBillNodetails(string searchcerteria)

        {
            DataTable TripDT = new DataTable();
            TripDT = trackerdatamanager.GetBillVerications(searchcerteria);
            List<EN.FuelBillverificationsEntiites> TripList = new List<EN.FuelBillverificationsEntiites>();

            if (TripDT.Rows.Count != 0)
            {
                foreach (DataRow row in TripDT.Rows)
                {
                    EN.FuelBillverificationsEntiites trip = new EN.FuelBillverificationsEntiites();
                    trip.SRno = Convert.ToInt16(row["Sr No"]);
                    trip.ID = Convert.ToInt16(row["ID"]);
                    trip.BillYear = Convert.ToString(row["Bill Year"]);
                    trip.Billno = Convert.ToString(row["Bill No"]);
                    trip.BillDate = Convert.ToString(row["Bill Date"]);

                    TripList.Add(trip);
                }

            }
            return TripList;
        }
        public string CheckSlipExits(DataTable Itemdata)
        {
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            if (Itemdata != null)
            {
                for (int i = 0; i < Itemdata.Rows.Count; i++)
                {
                    DataTable PortsDS = new DataTable();

                    PortsDS = db.sub_GetDatatable("USP_CheckFuelslipExits '" + Itemdata.Rows[i].Field<string>(0) + "'");

                    if (PortsDS.Rows.Count == 0)
                    {

                        message += "following slip no is not found : " + Itemdata.Rows[i].Field<string>("Slip No") + "  .Cannot proceed.";
                    }
                    else
                    {
                        message += "";
                    }

                }
                if ((message != ""))
                {
                    message2 = message;
                }
            }
            return message2;
        }


        public string CheckBillVerificatioinvalidate(DataTable Itemdata)
        {
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            if (Itemdata != null)
            {
                for (int i = 0; i < Itemdata.Rows.Count; i++)
                {
                    DataTable PortsDS = new DataTable();

                    dt1 = db.sub_GetDatatable("select top(1)M.billno,billdate ,fuel_vendorname   from fuel_vendor_BillMaster M inner join fuel_vendor_BillDetails  D on D.BillNo =M.BillNo  and M.BillYear =d.BillYear inner join fuel_vendorM V on V.fuel_vendorid =M.vendorid  and M.IsCancel =0  where SlipNo = '" + Itemdata.Rows[i].Field<string>(0) + "'");
                    dt = dt1;
                    if (dt1.Rows.Count > 0)
                    {

                        message += "Bill is already generated for this Slip no: " + Itemdata.Rows[i].Field<string>("Slip No") + " by vendor = " + dt.Rows[0].Field<string>(2) + "  .Cannot proceed.";
                    }
                    else
                    {
                        message += "";
                    }

                }
                if ((message != ""))
                {
                    message2 = message;
                }
            }
            return message2;
        }


        public string CheckFuelBillVerificatioinvalidate(DataTable Itemdata)
        {
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            if (Itemdata != null)
            {
                for (int i = 0; i < Itemdata.Rows.Count; i++)
                {
                    DataTable PortsDS = new DataTable();
                    PortsDS = db.sub_GetDatatable("USP_Verifiy_Fuel_Slip '" + Itemdata.Rows[i].Field<string>(0) + "'");

                    if (Convert.ToInt32(PortsDS.Rows[0][8]) != Convert.ToInt32(Itemdata.Rows[i][1]))
                    {

                        message += "Mismatch found in Fuel Qty again slip no: " + Itemdata.Rows[i].Field<string>("Slip No") + " Generated Fuel Qty: " + Convert.ToInt32(Itemdata.Rows[i][1]) + " vendor fuel qty .Cannot proceed.";


                    }
                    else
                    {
                        message += "";
                    }

                }
                if ((message != ""))
                {
                    message2 = message;
                }
            }
            return message2;
        }

        public List<EN.FuelBillVerificationDetails> ImportBillVerificationDetails(DataTable Itemdata)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            DataTable ds = new DataTable();


            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            List<EN.FuelBillVerificationDetails> VoucherdetailsLIst = new List<EN.FuelBillVerificationDetails>();
            for (int i = 0; i < Itemdata.Rows.Count; i++)
            {

                dt = db.sub_GetDatatable("USP_Verifiy_Fuel_Slip '" + Itemdata.Rows[i].Field<string>(0) + "'");
                dt2 = dt;
                if (dt2.Rows.Count != 0)
                {
                    foreach (DataRow row in dt2.Rows)
                    {
                        EN.FuelBillVerificationDetails trip = new EN.FuelBillVerificationDetails();
                        trip.SlipNo = Convert.ToString(row["Slip No"]);
                        trip.Voucherno = Convert.ToString(row["Vouche No"]);
                        trip.SLIpdate = Convert.ToString(row["Slip Date"]);
                        trip.Activity = Convert.ToString(row["Activity"]);
                        trip.trailerNo = Convert.ToString(row["Trailer Name"]);

                        trip.Fromloc = Convert.ToString(row["From"]);
                        trip.Toloc = Convert.ToString(row["Upto"]);
                        trip.fueltype = Convert.ToString(row["Fuel Type"]);
                        trip.FuelQty = Convert.ToString(row["Fuel Qty"]);


                        trip.totamt = Convert.ToDouble(row["Fuel Qty"]) * Convert.ToDouble(Itemdata.Rows[i].Field<string>(2));
                        trip.VendorQty = Itemdata.Rows[i].Field<string>(1);
                        trip.Rate = Itemdata.Rows[i].Field<string>(2);


                        VoucherdetailsLIst.Add(trip);
                    }

                }

            }
            return VoucherdetailsLIst;
        }
        public List<EN.FuelVendorMaster> getFuelVendorMaster()
        {
            List<EN.FuelVendorMaster> passingDL = new List<EN.FuelVendorMaster>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getFuelVendorMaster();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.FuelVendorMaster PassingList = new EN.FuelVendorMaster();
                    PassingList.fuel_Vendorid = Convert.ToInt32(row["fuel_VendorID"]);
                    PassingList.fuelVendorName = Convert.ToString(row["fuel_VendorName"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }


        public string SaveBillVerificationDetails(DataTable dataTable, string BillYear, string Billnew, string Billdate, string VendorName,
            string Slipno1,
               string Todate, string Fromdate, string Rate1, int Userid, string Netamount, string TCSpercentage, string TCSAmount, string Grandtotal, string GSTID)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            string message = "";

            DataTable VendorDataDL = new DataTable();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("BillYear", BillYear);
            parameterList.Add("Billnew", Billnew);
            parameterList.Add("Billdate", Billdate);
            parameterList.Add("VendorName", VendorName);
            parameterList.Add("Slipno1", Slipno1);
            parameterList.Add("Todate", Todate);
            parameterList.Add("Fromdate", Fromdate);
            parameterList.Add("Rate1", Rate1);
            parameterList.Add("Userid", Userid);
            parameterList.Add("Netamount", Netamount);
            parameterList.Add("TCSpercentage", TCSpercentage);
            parameterList.Add("TCSAmount", TCSAmount);
            parameterList.Add("Grandtotal", Grandtotal);
            parameterList.Add("GSTID", GSTID);
            /*VendorDataDL = trackerdatamanager.AddDirectFuel(containerCount, containerTypeID, TrailerNo, Passing, driverID, TransportorID, FromID, ToID, Activity, ReadingFrom, ReadingTo, fuel, Amount1, Amount2, AdjustFuel, Userid, PlanID, INOUT, ContainerNo, TarrifID, VehTransID, Remarks);*/

            VendorDataDL = db.DataTableAddTypeTable("InsertBillVerificationDetails", parameterList, dataTable, "AddBillverification", "AddBillverification");


            return message;

        }

        public string CancelFuelBillverification(DataTable dataTable, int Userid)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            string message = "";

            DataTable VendorDataDL = new DataTable();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("Userid", Userid);

            /*VendorDataDL = trackerdatamanager.AddDirectFuel(containerCount, containerTypeID, TrailerNo, Passing, driverID, TransportorID, FromID, ToID, Activity, ReadingFrom, ReadingTo, fuel, Amount1, Amount2, AdjustFuel, Userid, PlanID, INOUT, ContainerNo, TarrifID, VehTransID, Remarks);*/

            VendorDataDL = db.DataTableAddTypeTable("CancelBillVerificationDetails", parameterList, dataTable, "CancelBillverification", "@CancelBillverification");


            return message;

        }

        public string UpdateFuelbillverification(string BillNo, string Invoiceno, string vendorid, string billdate, string Workyear, int Userid)
        {
            string message = "";
            DataTable dt = new DataTable();
            dt = trackerdatamanager.Updatefuelbillverificationdetails(BillNo, Invoiceno, vendorid, billdate, Workyear, Userid);
            //if (dt != null)
            //{
            //    try
            //    {
            //        message = Convert.ToString(dt.Rows[0]["userid"]);

            //    }
            //    catch (Exception ex)
            //    {

            //        return message = "";
            //    }

            //}

            return message;
        }
      

        public List<EN.MovementCountEntities> getAddMovCount()
        {
            List<EN.MovementCountEntities> locationDL = new List<EN.MovementCountEntities>();
            DataTable dt = new DataTable();
            string Table = "Mov_Count";
            string Column = "MovCountID,MovCount";
            string Condition = "";
            string OrderBy = "MovCountID";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.MovementCountEntities MovList = new EN.MovementCountEntities();
                    MovList.MovCountID = Convert.ToString(row["MovCountID"]);
                    MovList.MovCount = Convert.ToString(row["MovCount"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }
        public string SaveFuelTairffAdd(string Transporter, string Activity, string Drivertype, string Inout, string ContainerCount, string cash, string bank, string Fromdate, string Todate, int Userid, string Entryid,string fuel)
        {
            string message = ""; int i;
            //DataTable ContainerDT = new DataTable();
            i = trackerdatamanager.SaveAddadditionaltariffCenter(Transporter, Activity, Drivertype, Inout, ContainerCount, cash, bank, Fromdate, Todate, Userid, Entryid, fuel);
            if (i > 0)
            {
                message = "Record Saved successfully";
            }
            return message;
        }

        public EN.PumpAcknowledgementEntites GetSlipWiseFuel(int Trailerid, string SLipNo)
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.GetSLipWiseFueldetails(Trailerid, SLipNo);
            EN.PumpAcknowledgementEntites ContainerList = new EN.PumpAcknowledgementEntites();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {


                    ContainerList.DriverName = Convert.ToString(row["driverid"]);
                    ContainerList.Fuelqty = Convert.ToString(row["Fuel"]);
                    ContainerList.SLipdate = Convert.ToString(row["slipDate"]);
                    ContainerList.Fuelrate = Convert.ToString(row["Fuel rate"]);
                    ContainerList.totalamount = Convert.ToString(row["Total Amount"]);


                }

            }
            return ContainerList;
        }


        public string SaveFuelAcknowledgement(string Trailerid, string SLipNo, string FuelQty, string FuelRate, string Totalamt, string Remarks, int userid)
        {
            string message = ""; int i;
            //DataTable ContainerDT = new DataTable();
            i = trackerdatamanager.SaveFuelAcknowledgement(Trailerid, SLipNo, FuelQty, FuelRate, Totalamt, Remarks, userid);
            if (i > 0)
            {
                message = "Record saved successfully";
            }
            return message;
        }

        public string CheckSLipNoForFuelAcknowledgement(string Trailerid, string SLipNo, string FuelQty, string FuelRate, string Totalamt, string Remarks, int userid)
        {
            string message = "";
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.CheckSLipNoForFuelAcknowledgement(Trailerid, SLipNo, FuelQty, FuelRate, Totalamt, Remarks, userid);
            if (ContainerDT.Rows.Count > 0)
            {
                message = "Slip no. already exists";
            }
            else
            {
                message = "";
            }
            return message;
        }
        public EN.FuelEntities getDirectAdvanceFuelDataForDirect(string driverid)
        {
            DataTable dt = new DataTable();
            EN.FuelEntities FuelDL = new EN.FuelEntities();
            dt = trackerdatamanager.getDriverAdvanceFuelData(driverid);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    FuelDL.AdvanceFuel = Convert.ToString(row["deductfuel"]);
                    FuelDL.AdvanceBank = Convert.ToString(row["deductbank"]);
                    FuelDL.AdvanceCash = Convert.ToString(row["deductCash"]);
                    //FuelDL.LoanID = Convert.ToString(row["LoanID"]);

                }
            }
            return FuelDL;
        }


        public string CheckContaineralreadySaved(DataTable FuelEntitiesContainerList, string status, string Activity,
        string FromDestination)
        {
            string message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            for (int i = 0; i < FuelEntitiesContainerList.Rows.Count; i++)
            {
                DataTable PortsDS = new DataTable();
                PortsDS = db.sub_GetDatatable("USP_CheckContainerRepate '" + FuelEntitiesContainerList.Rows[i].Field<string>("ContainerNo") + "','" + status + "','" + Activity + "','" + FromDestination + "'");


                if (PortsDS.Rows.Count > 0)
                {
                    message += "" + PortsDS.Rows[0][0] + "" + "";
                }
                else
                {
                    message += "";
                }

            }


            return message;

        }

        public List<EN.GetProcesses> GetProcesses()
        {
            List<EN.GetProcesses> HoldReasons = new List<EN.GetProcesses>();
            DataTable dt = new DataTable();
            string Table = "Hold_Processes";
            string Column = "ID,Processes";
            string Condition = "";
            string OrderBy = "Processes";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.GetProcesses EquipmentList = new EN.GetProcesses();
                    EquipmentList.ID = Convert.ToInt32(row["ID"]);
                    EquipmentList.Processes = Convert.ToString(row["Processes"]);
                    HoldReasons.Add(EquipmentList);
                }
            }
            return HoldReasons;
        }

        public List<EN.VendorList> getVandorNameList()
        {
            List<EN.VendorList> VendorDL = new List<EN.VendorList>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "Vendor";
            string Column = "isnull(vendorid,0)vendorid,VendorName";
            string Condition = "";
            string OrderBy = "VendorName";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    EN.VendorList VandorNameList = new EN.VendorList();
                    VandorNameList.VendorID = Convert.ToInt32(row["vendorid"]);
                    VandorNameList.VendorName = Convert.ToString(row["VendorName"]);

                    VendorDL.Add(VandorNameList);
                }
            }
            return VendorDL;
        }

        public List<EN.BillVerificationTransport> VendorFill()
        {
            List<EN.BillVerificationTransport> locationDL = new List<EN.BillVerificationTransport>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.VendorFill();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.BillVerificationTransport locationList = new EN.BillVerificationTransport();
                    locationList.transporterID = Convert.ToInt32(row["transporterID"]);
                    locationList.vendorname = Convert.ToString(row["vendorname"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public EN.GetTransportBillDetails GetTransportBillDetails(DataTable TransportBillData, Int32 userId, string fromdate, string todate, string process, string Transporter)
        {
            //EN.DriverPaymentReco objdriverpaymententities = new EN.DriverPaymentReco();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            string message = "";
            string message1 = "";
            string messagefuell = "";
            string strsub = "";
            string strSQL = "";

            DataTable dt1 = new DataTable();
            DataTable CheckContainernoDL = new DataTable();
            DataTable ContainerDetailsDL = new DataTable();
            DataTable dtchecktrailerno = new DataTable();
            DataTable dtAgreeAmt = new DataTable();
            DataTable GetJonoDL = new DataTable();

            EN.GetTransportBillDetails GetContainerFullList = new EN.GetTransportBillDetails();
            // EN.TransportBillMsg getMessgageList = new EN.TransportBillMsg();
            List<EN.DataGetBillChecking> GetContainerList = new List<EN.DataGetBillChecking>();
            List<EN.TransportBillMsg> GetErrorList = new List<EN.TransportBillMsg>();

            if (TransportBillData != null)
            {
                //for (int i = 0; i < TransportBillData.Rows.Count; i++)
                //{

                //    if (Convert.ToString(TransportBillData.Rows[i][2]) == "")
                //    {


                //        getMessgageList.ValidateContainerno += "" + TransportBillData.Rows[i][0] + ".";
                //        getMessgageList.ValidateMSG += "Invalid Activity Name Cannot proceed.";
                //        continue;
                //    }
                //    else
                //    {
                //        message += "";
                //    }
                //}


                for (int i = 0; i < TransportBillData.Rows.Count; i++)
                {
                    EN.TransportBillMsg getMessgageList = new EN.TransportBillMsg();


                    GetJonoDL = db.sub_GetDatatable("USP_CHECK_DATA_TNS_BILL_ALL_LOC '" + TransportBillData.Rows[i][2] + "','" + TransportBillData.Rows[i][0] + "','" + Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy HH:mm") + "','" + Convert.ToDateTime(todate).ToString("dd-MMM-yyyy HH:mm") + "','" + TransportBillData.Rows[i][5] + "'");

                    if (GetJonoDL.Rows.Count > 0)
                    {
                        CheckContainernoDL = db.sub_GetDatatable("USP_CheckTransportBillVerficiationDuplicateCheck '" + TransportBillData.Rows[i][0] + "','" + GetJonoDL.Rows[0][0] + "','" + TransportBillData.Rows[i][2] + "','" + TransportBillData.Rows[i][2] + "','" + Transporter + "','" + TransportBillData.Rows[i][5] + "'");

                        if (CheckContainernoDL.Rows.Count > 0)
                        {
                            getMessgageList.ValidateContainerno += "" + TransportBillData.Rows[i][0] + ".";
                            getMessgageList.ValidateMSG += "Bill is already generated for this container no " + TransportBillData.Rows[i][0] + " again bill no: " + CheckContainernoDL.Rows[0][0] + " dated on " + Convert.ToDateTime(CheckContainernoDL.Rows[0][1]).ToString("dd-MMM-yyyy HH:mm") + " again vendor " + (CheckContainernoDL.Rows[0][2]) + ".Cannot proceed.";
                            //  getMessgageList.ValidateMSG += "Bill is already generated for this container no " + TransportBillData.Rows[i][0] + " again bill no: " + CheckContainernoDL.Rows[0][0] + " dated on " +  Convert.ToDateTime(CheckContainernoDL.Rows[0][1]).ToString("dd-MMM-yyyy HH:mm") + " again vendor " + (CheckContainernoDL.Rows[0][2]) + ".Cannot proceed.";
                            GetErrorList.Add(getMessgageList);
                            continue;
                        }
                        else
                        {
                            message += "";

                        }


                        strsub = "";
                        strsub = "exec get_sp_BillTransporterByContainerNo_All_LOC '" + TransportBillData.Rows[i][2] + "','" + TransportBillData.Rows[i][0] + "','" + GetJonoDL.Rows[0][0] + "','" + Transporter + "'";
                        ContainerDetailsDL = db.sub_GetDatatable(strsub);

                        message1 = "";
                        if (ContainerDetailsDL.Rows.Count > 0)
                        {
                            for (int iadd = 0; iadd <= ContainerDetailsDL.Rows.Count - 1; iadd++)
                            {
                                if (Convert.ToString(ContainerDetailsDL.Rows[iadd][3]) != Convert.ToString(TransportBillData.Rows[i][3]))
                                {
                                    getMessgageList.ValidateContainerno += "container no " + TransportBillData.Rows[i][0] + "";
                                    getMessgageList.ValidateMSG += "size is mismatch : " + TransportBillData.Rows[i][3] + " .Cannot proceed.";
                                    GetErrorList.Add(getMessgageList);
                                    continue;
                                }

                                if (Convert.ToString(ContainerDetailsDL.Rows[iadd][6]) != Convert.ToString(TransportBillData.Rows[i][5]))
                                {
                                    getMessgageList.ValidateContainerno += "container no " + TransportBillData.Rows[i][0] + "";
                                    getMessgageList.ValidateMSG += "trailer no is mismatch : " + Convert.ToString(ContainerDetailsDL.Rows[iadd][6]) + " .Cannot proceed.";
                                    GetErrorList.Add(getMessgageList);
                                    continue;
                                }

                                strSQL = "";
                                strSQL = "select top 1 * from trailers where trailername = '" + Convert.ToString(ContainerDetailsDL.Rows[iadd][6]) + "' and OwnedBy ='Own' and IsActive =1";
                                dtchecktrailerno = db.sub_GetDatatable(strSQL);
                                if (dtchecktrailerno.Rows.Count < 0)
                                {
                                    getMessgageList.ValidateContainerno += "Container no " + TransportBillData.Rows[i][0] + "";
                                    getMessgageList.ValidateMSG += "trailer no is found in NCL master : " + Convert.ToString(ContainerDetailsDL.Rows[iadd][6]) + " .Cannot proceed.";
                                    GetErrorList.Add(getMessgageList);
                                    continue;
                                }
                                messagefuell += message1 + " \n" + message;
                                if (message1 == "")
                                {
                                    foreach (DataRow row in ContainerDetailsDL.Rows)
                                    {
                                        EN.DataGetBillChecking ContList = new EN.DataGetBillChecking();
                                        ContList.Process = Convert.ToString(row["Process"]);
                                        ContList.EntryIDJONo = Convert.ToString(row["EntryID/JONo"]);
                                        ContList.ContainerNo = Convert.ToString(row["Container No"]);
                                        ContList.Size = Convert.ToString(row["Size"]);
                                        ContList.INDateTime = Convert.ToString(row["IN Date & Time"]);
                                        ContList.Port = Convert.ToString(row["Port"]);
                                        ContList.TrailerNo = Convert.ToString(row["Trailer No"]);
                                        ContList.TranspoterName = Convert.ToString(row["Transpoter Name"]);
                                        ContList.OutDateTime = Convert.ToString(row["Out Date & Time"]);
                                        ContList.TransportationAmt = Convert.ToDouble(TransportBillData.Rows[i][1]);
                                        ContList.MessageValidation1 = messagefuell;
                                        GetContainerList.Add(ContList);
                                    }
                                }



                            }

                        }
                    }
                    else
                    {
                        getMessgageList.ValidateContainerno += "" + TransportBillData.Rows[i][0] + "";
                        getMessgageList.ValidateMSG += "Data not found again container no " + TransportBillData.Rows[i][0] + ".Cannot proceed.";
                        GetErrorList.Add(getMessgageList);

                    }




                }
            }

            GetContainerFullList.TransportBillMsg = GetErrorList;
            GetContainerFullList.DataGetBillChecking = GetContainerList;
            return GetContainerFullList;
        }


        public string CancelBillDetails(DataTable dataTable, int Userid)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            string message = "";

            DataTable VendorDataDL = new DataTable();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("Userid", Userid);

            /*VendorDataDL = trackerdatamanager.AddDirectFuel(containerCount, containerTypeID, TrailerNo, Passing, driverID, TransportorID, FromID, ToID, Activity, ReadingFrom, ReadingTo, fuel, Amount1, Amount2, AdjustFuel, Userid, PlanID, INOUT, ContainerNo, TarrifID, VehTransID, Remarks);*/

            VendorDataDL = db.DataTableAddTypeTable("CancelTransportBillVerificationDetails", parameterList, dataTable, "CancelTransportBillVerification", "@CancelTransportBillVerification");


            return message;

        }

        public string BillSave(DataTable dataTable, long UserID, string BillYear, string BillNo, string InvoiceNo, string BillDate, string Vendor, string Activity, string ContainerNo, string Amount)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dtsb = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            int IFCLID = 0;
            string strQuerymax = "";
            string activityCheck = "";
            string GetInvoiceno = "";



            dt4 = trackerdatamanager.GetInvoicenoforverification(InvoiceNo, BillYear);

            if (dt4.Rows.Count > 0)
            {
                BillNo = Convert.ToString(dt4.Rows[0][0]);
            }


            //if (BillNo != "")
            //{
            //    strQuerymax = "";
            //    strQuerymax = "usp_Update_Bill_Master '" + UserID + "','" + BillNo + "','" + BillYear + "'";
            //    dtget = db.sub_GetDatatable(strQuerymax);

            //    strQuerymax = "";
            //    strQuerymax = "delete from BillDetails where billNo=" + BillNo + " and billYear='" + BillYear + "'";
            //    dtsb = db.sub_GetDatatable(strQuerymax);


            //}
            string strSQL = "";
            int IntBillNo = 0;
            strSQL = "";
            strSQL = "Select IsNull(MAX (BillNo ),0) + 1 as BillNo from BillMaster WITH(XLOCK) ";
            dt.Clear();
            dt = db.sub_GetDatatable(strSQL);
            if (dt.Rows.Count > 0)
            {
                IntBillNo = Convert.ToInt32(dt.Rows[0][0]);
            }

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {


                strSQL = "Exec Usp_Bill_Check '" + dataTable.Rows[k].Field<string>("Process") + "','" + dataTable.Rows[k].Field<string>("EntryIDJONo") + "',";
                strSQL += " '" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + dataTable.Rows[k].Field<string>("Process") + "'";
                dt5 = db.sub_GetDatatable(strSQL);
                Message = Convert.ToString(dt5.Rows[0]["mgs"]);


            }
            if (Message == "")
            {

                strSQL = "";
                strSQL = "USP_Insert_Into_Bill_Master'" + IntBillNo + "','" + InvoiceNo + "','" + BillYear + "','" + Vendor + "','" + activityCheck + "','" + Activity + "','" + UserID + "','" + BillDate + "'";
                dt3 = db.sub_GetDatatable(strSQL);


                //if (BillNo != "")
                //{
                //    for (int k = 0; k < dataTable.Rows.Count; k++)
                //    {


                //        strSQL = "Exec usp_insert_Bill_Details '" + BillNo + "','" + BillYear + "','" + Vendor + "','" + dataTable.Rows[k].Field<string>("Process") + "','" + dataTable.Rows[k].Field<string>("EntryIDJONo") + "',";
                //        strSQL += " '" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + dataTable.Rows[k].Field<string>("Size") + "','" + dataTable.Rows[k].Field<string>("INDateTime") + "',";
                //        strSQL += "'" + dataTable.Rows[k].Field<string>("Port") + "','" + dataTable.Rows[k].Field<string>("TrailerNo") + "','" + dataTable.Rows[k].Field<string>("Process") + "','" + dataTable.Rows[k].Field<string>("TransportationAmt") + "'";
                //        dt2 = db.sub_GetDatatable(strSQL);




                //        string Messageget = "Record Saved Successfully";
                //        Message = Messageget;

                //    }
                //}

                for (int k = 0; k < dataTable.Rows.Count; k++)
                {


                    strSQL = "Exec usp_insert_Bill_Details '" + IntBillNo + "','" + BillYear + "','" + Vendor + "','" + dataTable.Rows[k].Field<string>("Process") + "','" + dataTable.Rows[k].Field<string>("EntryIDJONo") + "',";
                    strSQL += " '" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + dataTable.Rows[k].Field<string>("Size") + "','" + dataTable.Rows[k].Field<string>("INDateTime") + "',";
                    strSQL += "'" + dataTable.Rows[k].Field<string>("Port") + "','" + dataTable.Rows[k].Field<string>("TrailerNo") + "','" + dataTable.Rows[k].Field<string>("Process") + "','" + dataTable.Rows[k].Field<string>("TransportationAmt") + "'";
                    dt2 = db.sub_GetDatatable(strSQL);




                    string Messageget = "Record Saved Successfully";
                    Message = Messageget;

                }


            }


            return Message;
        }

        public List<EN.Customer> Getcustomer()
        {
            List<EN.Customer> locationDL = new List<EN.Customer>();
            DataTable dt = new DataTable();
            string Table = "customer";
            string Column = "agid,agname";
            string Condition = "";
            string OrderBy = "agname";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.Customer MovList = new EN.Customer();
                    MovList.AGID = Convert.ToInt32(row["agid"]);
                    MovList.AGName = Convert.ToString(row["agname"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }

        public List<EN.BilltypeEntities> GetBilltype()
        {
            List<EN.BilltypeEntities> locationDL = new List<EN.BilltypeEntities>();
            DataTable dt = new DataTable();
            string Table = "Rebate_billType_M";
            string Column = "BillID,BillName";
            string Condition = "";
            string OrderBy = "BillName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.BilltypeEntities MovList = new EN.BilltypeEntities();
                    MovList.BillID = Convert.ToInt32(row["BillID"]);
                    MovList.BillName = Convert.ToString(row["BillName"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }

        public EN.Rebatebillverificationentities Getdatatabledetails(DataTable Itemdata)
        {
            string message = "";
            int count = 1;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            EN.Rebatebillverificationentities Rebatedetails = new EN.Rebatebillverificationentities();
            List<EN.Getdetails> Getdetails = new List<EN.Getdetails>();
            if (Itemdata != null)
            {
                for (int i = 0; i < Itemdata.Rows.Count; i++)
                {
                    DataTable PortsDS = new DataTable();
                    PortsDS = db.sub_GetDatatable("get_SP_RebateBillContainerNo '" + Itemdata.Rows[i].Field<string>(0) + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy") + "'");

                    if (PortsDS.Rows.Count > 0)
                    {
                        foreach (DataRow row in PortsDS.Rows)
                        {
                            EN.Getdetails RebateList = new EN.Getdetails();
                            RebateList.SrNo = Convert.ToString(count++);
                            RebateList.ContainerNo = Convert.ToString(row["Container No"]);
                            RebateList.JONo = Convert.ToString(row["JO No"]);
                            RebateList.JOType = Convert.ToString(row["JO Type"]);
                            RebateList.Size = Convert.ToString(row["Size"]);
                            RebateList.Type = Convert.ToString(row["Type"]);
                            RebateList.INDateandTime = Convert.ToString(row["IN Date and Time"]);
                            RebateList.LineName = Convert.ToString(row["Line Name"]);
                            RebateList.CustID = Convert.ToString(row["CustID"]);
                            RebateList.CustomerNmae = Convert.ToString(row["Customer Name"]);
                            RebateList.IsDPD = Convert.ToString(row["IsDPD"]);
                            RebateList.IsUB = Convert.ToString(row["IsUB"]);
                            RebateList.Lineid = Convert.ToString(row["LineID"]);
                            RebateList.PaidAmount = Convert.ToString(Itemdata.Rows[i].Field<string>(1));
                            Getdetails.Add(RebateList);

                        }

                    }
                    else
                    {


                        Rebatedetails.message += Convert.ToString("Invalid Container No: " + Itemdata.Rows[i].Field<string>(0) + "\n");



                    }

                }

            }
            Rebatedetails.Getdetails = Getdetails;
            return Rebatedetails;
        }

        public string CheckBillAlreadySaved(DataTable Itemdata, string BillYear, string BillNo, string BillType, string PartyName, int userid)
        {
            string message = "";
            string message2 = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            if (Itemdata != null)
            {
                for (int i = 0; i < Itemdata.Rows.Count; i++)
                {
                    DataTable PortsDS = new DataTable();
                    string strSQL = "";
                    strSQL = "Select distinct  ContainerNo ,M.BillNo , convert (varchar(20),M.EntryDate,113) as EntryDate ,dbo.Fn_GET_NAME (M.PartyID ,'Party Name') as Party  from Import_levybillM M ";
                    strSQL = strSQL + " inner join  Import_levybillD D on D.PaidNo =M.PaidNo and D.BillYear =M.BillYear where M.BillType  ='" + BillType + "' and  containerNo = '" + Itemdata.Rows[i].Field<string>("ContainerNo") + "' and jono ='" + Itemdata.Rows[i].Field<string>("JONo") + "' ";
                    PortsDS = db.sub_GetDatatable(strSQL);

                    if (PortsDS.Rows.Count > 0)
                    {

                        message += "Container No is already saved on '" + PortsDS.Rows[0].Field<string>("EntryDate") + "' of container No ='" + PortsDS.Rows[0].Field<string>("ContainerNo") + "' against BillType  ='" + BillType + "' ,Bill No=" + PortsDS.Rows[0].Field<string>("BillNo") + "  and Party Name =" + PortsDS.Rows[0].Field<string>("Party") + "";


                    }
                    else
                    {
                        message += "";
                    }

                }
                if ((message != ""))
                {
                    message2 = message;
                }
            }
            return message2;
        }

        public string CheckBillNoissaved(DataTable Itemdata, string BillYear, string BillNo, string BillType, string PartyName, int userid)
        {
            string message = "";
            string message2 = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            DataTable PortsDS = new DataTable();
            string strSQL = "";
            strSQL = "Select distinct  M.PaidNo,M.billNo from Import_levybillM M inner join  Import_levybillD D on D.PaidNo =M.PaidNo and D.BillYear =M.BillYear   where   M.BillYear ='" + BillYear + "' and M.BillNo='" + BillNo + "' ";
            PortsDS = db.sub_GetDatatable(strSQL);

            if (PortsDS.Rows.Count > 0)
            {

                message = "Bill is already generated against this bill No ";


            }
            else
            {
                message += "";
            }


            if ((message != ""))
            {
                message2 = message;
            }

            return message2;
        }




        public string SaveBillrebatedetails(DataTable Itemdata, string BillYear, string BillNo, string BillType, string PartyName, int userid, string netpaid, string SGST, string CGST, string IGST, string Grandtotal, int isGST)
        {
            string message = "";

            string message2 = "";
            DataTable Dt = new DataTable();
            DataTable Dt1 = new DataTable();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            if (Itemdata != null)
            {

                string strSQ1L = "";
                int StrpaidNo = 0;
                strSQ1L = "Select ISNULL (max(PaidNo),0 ) + 1 as paidno from Import_levybillM";
                Dt = db.sub_GetDatatable(strSQ1L);

                if (Dt.Rows.Count > 0)
                {
                    StrpaidNo = Convert.ToInt32(Dt.Rows[0][0]);
                }
                else
                {
                    StrpaidNo = 1;
                }

                string subQry = "";
                subQry = "INSERT INTO Import_levybillM( PaidNo,BillNo ,EntryDate ,billtype ,BillYear,partyid ,addedBy ,addedon ,istax,KKcess,SBcess,NetAmt,GrandAmt,ServiceTax ) ";
                subQry += " values ('" + StrpaidNo + "','" + BillNo + "' ,'" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MMM-dd HH:mm:ss") + "','" + BillType + "','" + BillYear + "'," + PartyName + "," + userid + " ,Getdate()," + isGST + ",'" + IGST + "','" + CGST + "','" + netpaid + "','" + Grandtotal + "','" + SGST + "')";
                Dt1 = db.sub_GetDatatable(subQry);



                for (int i = 0; i < Itemdata.Rows.Count; i++)
                {
                    DataTable PortsDS = new DataTable();


                    if (Itemdata.Rows[i].Field<string>("PaidAmount") != "0")
                    {
                        string subQry1 = "";
                        subQry1 = "INSERT INTO Import_levybillD (PaidNo  ,BillYear  ,ContainerNo  ,Size ,type ,jono ,paidAmt ,billType ,lineid )";
                        subQry1 += " VALUES ('" + StrpaidNo + "'  ,'" + BillYear + "'  ,'" + Itemdata.Rows[i].Field<string>("ContainerNo") + "','" + Itemdata.Rows[i].Field<string>("Size") + "','" + Itemdata.Rows[i].Field<string>("JOType") + "' ,'" + Itemdata.Rows[i].Field<string>("JONo") + "' ,'" + Itemdata.Rows[i].Field<string>("PaidAmount") + "' ,'" + BillType + "' ,'" + Itemdata.Rows[i].Field<string>("Lineid") + "')";
                        PortsDS = db.sub_GetDatatable(subQry1);


                    }
                    else
                    {
                        message += "Please enter Paid Amount  For this Container No" + Itemdata.Rows[i].Field<string>("ContainerNo") + "";
                    }


                    if (message == "")
                    {
                        message2 = "Record Saved Successfully!";
                    }



                }

            }
            return message2;
        }

        public string CheckBlNumber(string Containerno,
                 string BLNO)
        {
            string message = "";
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            if (BLNO != "")
            {
                string strSQL = "";
                strSQL = "SELECT * FROM IGMDetails WHERE IGM_BLNo = '" + BLNO + "' AND IsCancel = 0";
                dt = db.sub_GetDatatable(strSQL);


                if (dt.Rows.Count > 0)
                {
                    message = "";
                }
                else
                {
                    message = "Bl Number Invalid";
                }

            }
            if (Containerno != "")
            {
                string strSQL = "";
                strSQL = "SELECT * FROM IGMDetails WHERE containerno = '" + Containerno + "' AND IsCancel = 0";
                dt = db.sub_GetDatatable(strSQL);


                if (dt.Rows.Count > 0)
                {
                    message = "";
                }
                else
                {
                    message = "Container No is Invalid";
                }

            }
            return message;
        }
        public EN.Rebatebillverificationentities GetMaunalDataDetails(string BillYear, string BillNo, string BillType, string Containerno,
            string BLNO, string paidamt, string PartyName)
        {
            string message = "";
            string Containerno1 = "";
            int count = 1;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            EN.Rebatebillverificationentities Rebatedetails = new EN.Rebatebillverificationentities();
            List<EN.Getdetails> Getdetails = new List<EN.Getdetails>();


            if (BLNO != "")
            {
                string strSQL = "";
                strSQL = "SELECT * FROM IGMDetails WHERE IGM_BLNo = '" + BLNO + "' AND IsCancel = 0";
                dt = db.sub_GetDatatable(strSQL);



                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Containerno1 = Convert.ToString(dt.Rows[i].Field<string>("containerno"));
                        if (Containerno1 != "")
                        {
                            string strSQL1 = "";
                            strSQL1 = "SELECT * FROM import_stock S INNER JOIN imp_agents A ON A.agentID =S.AgentID  ";
                            strSQL1 += " WHERE containerNo = '" + Containerno1 + "'  AND S.AgentID = '" + PartyName + "' ORDER BY inDate DESC ";
                            dt1 = db.sub_GetDatatable(strSQL1);


                            if (dt1.Rows.Count > 0)
                            {
                                Rebatedetails.message += Convert.ToString("Selected Customer is not against Enter Container No,Want to Procceed ? " + Containerno1 + "\n");

                            }
                            else
                            {

                                DataTable PortsDS = new DataTable();
                                PortsDS = db.sub_GetDatatable("get_SP_RebateBillContainerNo '" + Containerno1 + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy") + "'");

                                if (PortsDS.Rows.Count > 0)
                                {
                                    foreach (DataRow row in PortsDS.Rows)
                                    {
                                        EN.Getdetails RebateList = new EN.Getdetails();
                                        RebateList.SrNo = Convert.ToString(count++);
                                        RebateList.ContainerNo = Convert.ToString(row["Container No"]);
                                        RebateList.JONo = Convert.ToString(row["JO No"]);
                                        RebateList.JOType = Convert.ToString(row["JO Type"]);
                                        RebateList.Size = Convert.ToString(row["Size"]);
                                        RebateList.Type = Convert.ToString(row["Type"]);
                                        RebateList.INDateandTime = Convert.ToString(row["IN Date and Time"]);
                                        RebateList.LineName = Convert.ToString(row["Line Name"]);
                                        RebateList.CustID = Convert.ToString(row["CustID"]);
                                        RebateList.CustomerNmae = Convert.ToString(row["Customer Name"]);
                                        RebateList.IsDPD = Convert.ToString(row["IsDPD"]);
                                        RebateList.IsUB = Convert.ToString(row["IsUB"]);
                                        RebateList.Lineid = Convert.ToString(row["LineID"]);
                                        RebateList.PaidAmount = Convert.ToString(paidamt);
                                        Getdetails.Add(RebateList);

                                    }


                                }
                            }
                        }
                    }

                }
            }
            else
            {
                DataTable PortsDS = new DataTable();
                PortsDS = db.sub_GetDatatable("get_SP_RebateBillContainerNo '" + Containerno + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy") + "'");

                if (PortsDS.Rows.Count > 0)
                {
                    foreach (DataRow row in PortsDS.Rows)
                    {
                        EN.Getdetails RebateList = new EN.Getdetails();
                        RebateList.SrNo = Convert.ToString(count++);
                        RebateList.ContainerNo = Convert.ToString(row["Container No"]);
                        RebateList.JONo = Convert.ToString(row["JO No"]);
                        RebateList.JOType = Convert.ToString(row["JO Type"]);
                        RebateList.Size = Convert.ToString(row["Size"]);
                        RebateList.Type = Convert.ToString(row["Type"]);
                        RebateList.INDateandTime = Convert.ToString(row["IN Date and Time"]);
                        RebateList.LineName = Convert.ToString(row["Line Name"]);
                        RebateList.CustID = Convert.ToString(row["CustID"]);
                        RebateList.CustomerNmae = Convert.ToString(row["Customer Name"]);
                        RebateList.IsDPD = Convert.ToString(row["IsDPD"]);
                        RebateList.IsUB = Convert.ToString(row["IsUB"]);
                        RebateList.Lineid = Convert.ToString(row["LineID"]);
                        RebateList.PaidAmount = Convert.ToString(paidamt);
                        Getdetails.Add(RebateList);

                    }


                }
            }

            Rebatedetails.Getdetails = Getdetails;
            return Rebatedetails;

        }

        //developed by prathamesh
        public List<EN.GstDetails> GetVendorGSTDetails(string VendorID)
        {

            List<EN.GstDetails> TransportExpenses = new List<EN.GstDetails>();
            DataTable DL = new DataTable();
            DL = trackerdatamanager.GetVendorGTDetailsForFuelBill(VendorID);

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.GstDetails ExpensesList = new EN.GstDetails();

                    ExpensesList.GSTuniqueid = Convert.ToInt32(row["gstid"]);
                    ExpensesList.GstuniqueName = Convert.ToString(row["GSTIn_uniqID"]);
                    TransportExpenses.Add(ExpensesList);
                }
            }
            return TransportExpenses;
        }
        public List<EN.LabourGetdetails> GetLabourDataTableDet(DataTable LabourDT)        {
            //string message = "";
            int count = 1;            DataTable Labour = new DataTable();            DataTable dt1 = new DataTable();            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            //EN.LabourBillVerification Labourdetails = new EN.LabourBillVerification();
            List<EN.LabourGetdetails> LabourGetdetails = new List<EN.LabourGetdetails>();            if (LabourDT != null)            {                DataTable PortsDS = new DataTable();                foreach (DataRow Excelrow in LabourDT.Rows)                {                    PortsDS = db.sub_GetDatatable("get_sp_BillVendorWiseStuffing '" + Convert.ToString(Excelrow["WO No"]) + "','"+ Convert.ToString(Excelrow["Container No"]) + "'");                    foreach (DataRow row in PortsDS.Rows)                    {                        EN.LabourGetdetails Labourlist = new EN.LabourGetdetails();                        if (PortsDS.Rows.Count > 0)                        {                            Labourlist.SrNo = count++;                            Labourlist.WO_NO = Convert.ToString(row["WO No"]);                            Labourlist.work_year = Convert.ToString(row["Work year"]);                            Labourlist.SBEntryID = Convert.ToString(row["SBEntry ID"]);                            Labourlist.WO_Type = Convert.ToString(row["Wo Type"]);                            Labourlist.Activity = Convert.ToString(row["Activity"]);                            Labourlist.SBNO = Convert.ToString(row["SB Number"]);                            Labourlist.VehicleNo = Convert.ToString(row["Vehicle No"]);                            Labourlist.JONoEntryID = Convert.ToString(row["JONoEntryid"]);                            Labourlist.ContainerNo = Convert.ToString(row["Container No"]);                            Labourlist.PKGSDestuff = Convert.ToString(row["Qty"]);                            Labourlist.CargoWeight = Convert.ToString(row["Weight KGS"]);                            Labourlist.cargoweightMTS = Convert.ToString(row["Weight MTS"]);
                            //  Labourlist.Size = Convert.ToString(Excelrow["Size"]);
                            Labourlist.Amount = Convert.ToString(Excelrow["Amount"]);

                            LabourGetdetails.Add(Labourlist);                        }                    }                }            }            return LabourGetdetails;        }
        public string CheckLabourBillAlreadySaved(DataTable Itemdata, string BillYear, string BillNo, string BillType, string Vendor, int userid)        {            string message = "";            string message2 = "";            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();            if (Itemdata != null)            {                for (int i = 1; i < Itemdata.Rows.Count; i++)                {                    DataTable PortsDS = new DataTable();                    string strSQL = "";                    strSQL = "Select distinct M.Vendor_InvoiceNo, m.billNo, d.wono from labour_Bill_m M ";                    strSQL = strSQL + " inner join  labour_Bill_d D on D.billno =M.billno and D.BillYear =M.BillYear  where d.WONo ='" + Convert.ToString(Itemdata.Rows[i]["WO_NO"]) + "'  and d.WoYear='" + Convert.ToString(Itemdata.Rows[i]["Work_year"]) + "' ";                    PortsDS = db.sub_GetDatatable(strSQL);                    if (PortsDS.Rows.Count > 0)                    {                        message += "BILL NO is already saved  against workorder";                    }                    else                    {                        message += "";                    }                }                if ((message != ""))                {                    message2 = message;                }            }            return message2;        }
        public string CheckLabourBillNoissaved(DataTable Itemdata, string BillYear, string BillNo, string InvoiceNo, string Vendor, int userid)        {            string message = "";            string message2 = "";            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();            for (int i = 0; i < Itemdata.Rows.Count; i++)            {                DataTable PortsDS = new DataTable();                string strSQL = "";                strSQL = "Select distinct M.Vendor_InvoiceNo, M.billNo, D.WONO from labour_Bill_m M inner join  labour_Bill_d D on D.billno =M.billno and D.BillYear =M.BillYear   where   M.BillYear ='" + BillYear + "' and M.BillNo='" + InvoiceNo + "'";                PortsDS = db.sub_GetDatatable(strSQL);                if (PortsDS.Rows.Count > 0)                {                    message = "Bill is already generated against this bill No ";                }                else                {                    message += "";                }            }            if ((message != ""))            {                message2 = message;            }            return message2;        }        public string SaveLabourBillrebatedetails(DataTable Itemdata, string BillYear, string BillNo, string BillType, string InvoiceNo, string vendor, int userid, string netpaid, string SGST, string CGST, string IGST, string Grandtotal, int isGST, string TQty, string TWeightKGS, string TWeightMTS, string Rate, string TAmount)        {            string message = "";            string message2 = "";            DataTable Dt = new DataTable();            DataTable Dt1 = new DataTable();
            // DataTable dt4 = new DataTable();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            //dt4 = trackerdatamanager.GetInvoicenoforverification(InvoiceNo, BillYear);

            //if (dt4.Rows.Count > 0)
            //{
            //    BillNo = Convert.ToString(dt4.Rows[0][0]);
            //}


            if (Itemdata != null)            {                string strSQ1L = "";                int INtBillNo = 0;                strSQ1L = "Select IsNull(MAX (BillNo ),0)+1 from labour_Bill_m WITH(XLOCK)";                Dt = db.sub_GetDatatable(strSQ1L);                if (Dt.Rows.Count > 0)                {                    INtBillNo = Convert.ToInt32(Dt.Rows[0][0]);                }                else                {                    INtBillNo = 1;                }                string subQry = "";                subQry = "Insert Into labour_Bill_m(Vendor_InvoiceNo, Vendor_InvoiceDate, BillDate, BillNo, BillYear, VendorID, AddedBy, AddedOn, TotalPkgs,TotalWt,NetAmount,SGST,CGST,IGST,GrandTotal,IsGST)";                subQry += " values ('" + InvoiceNo + "','" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MMM-dd HH:mm:ss") + "','" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MMM-dd HH:mm:ss") + "','" + INtBillNo + "','" + BillYear + "'," + vendor + "," + userid + " ,Getdate(),'" + TQty + "','" + TWeightKGS + "' ,'" + netpaid + "','" + SGST + "','" + CGST + "','" + IGST + "','" + Grandtotal + "','" + isGST + "')";                Dt1 = db.sub_GetDatatable(subQry);                for (int i = 0; i < Itemdata.Rows.Count; i++)                {                    DataTable PortsDS = new DataTable();                    string subQry1 = "";                    subQry1 = "INSERT INTO labour_Bill_d(BillNo ,BillYear ,SBNO ,sbentryid,ContainerNo ,EntryId_JONo ,VehicleNo ,PKGS ,PKGStype,Weight ,Rate ,Size,BillType,Activity ,WoNo,WoYear)";                    subQry1 += " VALUES ('" + INtBillNo + "'  ,'" + BillYear + "'  ,'" + Convert.ToString(Itemdata.Rows[i]["SBNO"]) + "','" + Convert.ToString(Itemdata.Rows[i]["sbentryid"]) + "','" + Convert.ToString(Itemdata.Rows[i]["ContainerNo"]) + "' ,'" + Convert.ToString(Itemdata.Rows[i]["JONoEntryID"]) + "','" + Convert.ToString(Itemdata.Rows[i]["VehicleNo"]) + "' ,'" + Convert.ToString(Itemdata.Rows[i]["PKGSDestuff"]) + "',0,'" + Convert.ToString(Itemdata.Rows[i]["CargoWeight"]) + "' , '" + Convert.ToString(Itemdata.Rows[i]["Amount"]) + "' ,0,'" + BillType + "' ,'" + Convert.ToString(Itemdata.Rows[i]["Activity"]) + "','" + Convert.ToString(Itemdata.Rows[i]["WO_NO"]) + "','" + Convert.ToString(Itemdata.Rows[i]["Work_year"]) + "')";                    PortsDS = db.sub_GetDatatable(subQry1);                    if (message == "")                    {                        message2 = "Record Saved Successfully!";                    }                }            }            return message2;        }

    }
}
