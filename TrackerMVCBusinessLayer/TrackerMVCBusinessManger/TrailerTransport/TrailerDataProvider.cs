using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrailerTransport
{
    public class TrailerDataProvider
    {
        DB.TrackerMVCDBManager transportdatamanager = new DB.TrackerMVCDBManager();
        public EN.TrailerEntities getDropDownList()
        {
            EN.TrailerEntities objtrailerentities = new EN.TrailerEntities();
            DataSet DropDownList = new DataSet();
            DropDownList = transportdatamanager.getDropDownTrailerData();

            List<EN.TransporterEntities> TransporterList = new List<EN.TransporterEntities>();
            List<EN.VehicleTypeEntities> VehicleTypeList = new List<EN.VehicleTypeEntities>();

            //List<EN.MenuInfo> MenuList = new List<EN.MenuInfo>();
            if (DropDownList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in DropDownList.Tables[0].Rows)
                {
                    EN.TransporterEntities Transporter = new EN.TransporterEntities();
                    Transporter.TransID = Convert.ToInt32(row["TransID"]);
                    Transporter.TransName = Convert.ToString(row["TransName"]);
                    TransporterList.Add(Transporter);
                }
            }
            if (DropDownList.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow row in DropDownList.Tables[1].Rows)
                {
                    EN.VehicleTypeEntities VehicleType = new EN.VehicleTypeEntities();
                    VehicleType.VehicleTypeID = Convert.ToInt32(row["VehicleTypeID"]);
                    VehicleType.VehicleType = Convert.ToString(row["VehicleType"]);
                    VehicleTypeList.Add(VehicleType);
                }
            }

            objtrailerentities.TransporterList = TransporterList;
            objtrailerentities.VehicleTypeList = VehicleTypeList;
            

            return objtrailerentities;
        }

        //Codes By Prashant

        public string CheckTrailerNumber(string TrailerNumber)
        {
            string Message = "";
            DataTable ChecktrailerDlL = new DataTable();
            ChecktrailerDlL = transportdatamanager.CheckTrailerNumber(TrailerNumber);

            if (ChecktrailerDlL.Rows.Count > 0)
            {
                int trailerCount = Convert.ToInt16(ChecktrailerDlL.Rows[0]["Trailername"]);
                if (trailerCount > 0)
                {
                    Message = "1";

                }
            }


            return Message;
        }

        public string UpdateTrailerDetails(int trailerid, string ownedby, string Transportername, string passing, string grade, string modelno, string chasisno,
            string engine, string trailertype, string permit, string permittype, int userid, int EditchkIsActive, string EditVehicleTYpe,
            string EditIsshifting, string EditVehicleGroupType, string PolicyDate, string GreenTax, string PucDate, string UsedFor, string Taxdate,
            string Fitnessdate, string EditVEhicleTypeID, string EditVendorName, string TrailerSize, string VehicleGroupType, string FuelType,string Model, string SoldDateEdit, string MFGYREdit, string MileageEDIt, string GVWEDIT)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = transportdatamanager.UpdateTrailerDetails(trailerid, ownedby, Transportername, passing, grade, modelno,
                chasisno, engine, trailertype, permit, permittype, userid, EditchkIsActive, EditVehicleTYpe, EditIsshifting,
                EditVehicleGroupType, PolicyDate, GreenTax, PucDate, UsedFor, Taxdate, Fitnessdate, EditVEhicleTypeID, EditVendorName, TrailerSize, VehicleGroupType, FuelType, Model, SoldDateEdit, MFGYREdit, MileageEDIt, GVWEDIT);
            //if (updateDl.Rows.Count > 0)
            //   {
            //       message = "Records updated successfully";
            //   }
            //   else
            //   {
            //       message = "Records not update, please try again";
            //   }
            message = "Records Updated Successfully!";
            return message;
        }



        public List<EN.VehicleTypeGroup> GetVehicleGroup()
        {
            List<EN.VehicleTypeGroup> transpoterDL = new List<EN.VehicleTypeGroup>();
            DataTable dt = new DataTable();
            string Table = "vehiclegroup";
            string Column = "Groupid,GroupName";
            string Condition = "";
            string OrderBy = "GroupName";

            dt = transportdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.VehicleTypeGroup transpoterList = new EN.VehicleTypeGroup();
                    transpoterList.VehicleTypeid = Convert.ToInt32(row["Groupid"]);
                    transpoterList.VehicleTypeGroupNae = Convert.ToString(row["GroupName"]);
                    transpoterDL.Add(transpoterList);
                }
            }
            return transpoterDL;
        }

        public List<EN.FuelType> GetFuelMaster()
        {
            List<EN.FuelType> transpoterDL = new List<EN.FuelType>();
            DataTable dt = new DataTable();
            string Table = "FuelMaster";
            string Column = "FuelID,FuelType";
            string Condition = "";
            string OrderBy = "FuelType";

            dt = transportdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.FuelType transpoterList = new EN.FuelType();
                    transpoterList.Fuelid = Convert.ToInt32(row["FuelID"]);
                    transpoterList.FuelName = Convert.ToString(row["FuelType"]);
                    transpoterDL.Add(transpoterList);
                }
            }
            return transpoterDL;
        }


        public List<EN.Model_M> GetModel_Master()
        {
            List<EN.Model_M> transpoterDL = new List<EN.Model_M>();
            DataTable dt = new DataTable();
            string Table = "Model_M";
            string Column = "ModelID,ModelName";
            string Condition = "";
            string OrderBy = "ModelName";

            dt = transportdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.Model_M transpoterList = new EN.Model_M();
                    transpoterList.Modelid = Convert.ToInt32(row["ModelID"]);
                    transpoterList.ModelName = Convert.ToString(row["ModelName"]);
                    transpoterDL.Add(transpoterList);
                }
            }
            return transpoterDL;
        }

        public List<EN.TrailersEntities> GetTrailersDetails(string vehicletype)
        {
            List<EN.TrailersEntities> TrailerList = new List<EN.TrailersEntities>();
            DataTable TrailerDL = new DataTable();
            TrailerDL = transportdatamanager.GetTrailerSummarydetails(vehicletype);

            if (TrailerDL != null)
            {
                foreach (DataRow row in TrailerDL.Rows)
                {
                    EN.TrailersEntities TrailerDatalist = new EN.TrailersEntities();
                    TrailerDatalist.SrNo = Convert.ToInt16(row["SrNo"]);
                    TrailerDatalist.trailerid = Convert.ToInt16(row["trailerid"]);
                    TrailerDatalist.trailername = Convert.ToString(row["trailername"]);
                    TrailerDatalist.OwnedBy = Convert.ToString(row["OwnedBy"]);
                    TrailerDatalist.FuelShortCode = Convert.ToString(row["FuelShortCode"]);
                    TrailerDatalist.VehicleType = Convert.ToString(row["VehicleType"]);
                    TrailerDatalist.Regdate = Convert.ToString(row["RegDate"]);
                    TrailerDatalist.Passing = Convert.ToString(row["Passing"]);
                    TrailerDatalist.Permit = Convert.ToString(row["Permit"]);
                    TrailerDatalist.Grade = Convert.ToString(row["Grade"]);
                    TrailerDatalist.ModelNo = Convert.ToString(row["ModelNo"]);
                    TrailerDatalist.ChasisNo = Convert.ToString(row["ChasisNo"]);
                    TrailerDatalist.EngineNo = Convert.ToString(row["EngineNo"]);
                    TrailerDatalist.PermitExpiry = Convert.ToString(row["PermitExpiry"]);
                    TrailerDatalist.trailerType = Convert.ToString(row["trailerType"]);
                    TrailerDatalist.TransID = Convert.ToInt16(row["transid"]);
                    TrailerDatalist.TransName = Convert.ToString(row["TransName"]);
                    TrailerDatalist.CheckIsActive = Convert.ToInt16(row["IsActive"]);
                    TrailerDatalist.TrolleyNo = Convert.ToString(row["TrollyNo"]);
                    TrailerDatalist.TrolleySizecount = Convert.ToString(row["Trollysize"]);
                    TrailerDatalist.VehicleGroup = Convert.ToString(row["VehicleGroup"]);
                    TrailerDatalist.IsShifting = Convert.ToString(row["ISShifting"]);
                    TrailerDatalist.Isshiftingint = Convert.ToString(row["isshiftingint"]);
                    TrailerDatalist.VehicleGrouptype = Convert.ToString(row["Vehicle Gourp Type"]);
                    TrailerDatalist.VehicleGrouptypeID = Convert.ToString(row["Vehicle Model ID"]);
                    TrailerDatalist.Fitness_Due_Date = Convert.ToString(row["Fitness_Due_Date"]);
                    TrailerDatalist.Tax_Due_Date = Convert.ToString(row["Tax_Due_Date"]);
                    TrailerDatalist.Policy_End_Date = Convert.ToString(row["Policy_End_Date"]);
                    TrailerDatalist.Green_Tax_Due = Convert.ToString(row["Green_Tax_Due"]);
                    TrailerDatalist.Puc_date = Convert.ToString(row["Puc_date"]);
                    TrailerDatalist.Usedfor = Convert.ToString(row["Usedfor"]);
                    TrailerDatalist.VehicleTypeidEdit = Convert.ToInt32(row["VehicleTypeID"]);
                    TrailerDatalist.Groupid = Convert.ToString(row["groupid"]);
                    TrailerDatalist.Fuelid = Convert.ToString(row["FuelID"]);
                    TrailerDatalist.GroupName = Convert.ToString(row["Group Name"]);
                    TrailerDatalist.FuelType = Convert.ToString(row["FuelType"]);

                    TrailerDatalist.Model = Convert.ToString(row["Model"]);
                    TrailerDatalist.SoldDate = Convert.ToString(row["SoldDate"]);
                    TrailerDatalist.MFGYR = Convert.ToString(row["MFGYR"]);
                    TrailerDatalist.Mileage = Convert.ToString(row["Mileage"]);
                    TrailerDatalist.GVW = Convert.ToString(row["GVW"]);

                    TrailerList.Add(TrailerDatalist);

                }
            }
            return TrailerList;

        }


        public List<EN.TransporterEntities> getTranspoter()
        {
            List<EN.TransporterEntities> transpoterDL = new List<EN.TransporterEntities>();
            DataTable dt = new DataTable();
            string Table = "Transporter";
            string Column = "TransID,TransName";
            string Condition = "";
            string OrderBy = "TransName";

            dt = transportdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TransporterEntities transpoterList = new EN.TransporterEntities();
                    transpoterList.TransID = Convert.ToInt32(row["TransID"]);
                    transpoterList.TransName = Convert.ToString(row["TransName"]);
                    transpoterDL.Add(transpoterList);
                }
            }
            return transpoterDL;
        }



       

        public string SaveVehicleReporting(string Reprtingdate, string Trailerno, int Driverid, string Contactno, string Remakes,int userid)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = transportdatamanager.SaveVehicleReporting(Reprtingdate, Trailerno, Driverid, Contactno, Remakes, userid);
            if (updateDl.Rows.Count > 0)
            {
                message = Convert.ToString(updateDl.Rows[0][0]);
              
            }

            message = "Records saved Successfully!";
            return message;
        }

        public List<EN.VehicleReportingEntities> GetVehicleReporting()
        {
            List<EN.VehicleReportingEntities> Vehiclereporting = new List<EN.VehicleReportingEntities>();
            DataTable VehicleDL = new DataTable();
            VehicleDL = transportdatamanager.GetVehicleReportingDetails();
            int Count = 1;
            if (VehicleDL != null)
            {
                foreach (DataRow row in VehicleDL.Rows)
                {
                    EN.VehicleReportingEntities VehicleDataList = new EN.VehicleReportingEntities();
                    VehicleDataList.SrNo = Convert.ToInt16(Count++);
                    VehicleDataList.ReportingID = Convert.ToInt16(row["reportingid"]);
                    VehicleDataList.Reportingon = Convert.ToString(row["Reportedon"]);
                    VehicleDataList.TrailerNO = Convert.ToString(row["Trailerno"]);
                    VehicleDataList.Driverid = Convert.ToString(row["driverid"]);
                    VehicleDataList.DriverName = Convert.ToString(row["drivername"]);
                    VehicleDataList.Contact = Convert.ToString(row["ContactNo"]);
                    VehicleDataList.Remarks = Convert.ToString(row["Remarsk"]);
                    VehicleDataList.DWellHours = Convert.ToString(row["DwellHours"]);
                 

                    Vehiclereporting.Add(VehicleDataList);

                }
            }
            return Vehiclereporting;

        }

        public List<EN.ReadyVehicleMovementEntities> GetReadyVehicleReporting()
        {
            List<EN.ReadyVehicleMovementEntities> readyVehiclereporting = new List<EN.ReadyVehicleMovementEntities>();
            DataTable VehicleDL = new DataTable();
            VehicleDL = transportdatamanager.GetReadyVehicleReports();
            int Count = 1;
            if (VehicleDL != null)
            {
                foreach (DataRow row in VehicleDL.Rows)
                {
                    EN.ReadyVehicleMovementEntities VehicleDataList = new EN.ReadyVehicleMovementEntities();
                    VehicleDataList.SrNo = Convert.ToInt16(Count++);
                    //VehicleDataList.Priority = Convert.ToString(row["Priority"]);
                    VehicleDataList.trailername = Convert.ToString(row["Trailer No"]);
                    VehicleDataList.TrailerSize = Convert.ToString(row["Trailer Size"]);
                    VehicleDataList.Grade = Convert.ToString(row["Grade"]);
                    VehicleDataList.FromLocation = Convert.ToString(row["From Location"]);
                    VehicleDataList.Drivername = Convert.ToString(row["Drivername"]);
                    VehicleDataList.contactno = Convert.ToString(row["contactno"]);
                    VehicleDataList.TransDate = Convert.ToString(row["Indate & Time"]);
                    VehicleDataList.reportedon = Convert.ToString(row["Reported On"]);
                    VehicleDataList.DwellHours = Convert.ToString(row["Dwell Hours"]);
                    VehicleDataList.Transporter = Convert.ToString(row["Transporter"]);
                  


                    readyVehiclereporting.Add(VehicleDataList);

                }
            }
            return readyVehiclereporting;

        }

        // Codes End By Prashant

        public List<EN.VehicleReportingEntities> GetVehicleReportingSummary()
        {
            List<EN.VehicleReportingEntities> Vehiclereporting = new List<EN.VehicleReportingEntities>();
            DataTable VehicleDL = new DataTable();
            VehicleDL = transportdatamanager.GetVehicleReportingSummary();
            int Count = 1;
            if (VehicleDL != null)
            {
                foreach (DataRow row in VehicleDL.Rows)
                {
                    EN.VehicleReportingEntities VehicleDataList = new EN.VehicleReportingEntities();
                    VehicleDataList.SrNo = Convert.ToInt16(Count++);
                    VehicleDataList.TrailerNO = Convert.ToString(row["Trailer No"]);
                    VehicleDataList.FromLocation = Convert.ToString(row["From Location"]);
                    VehicleDataList.ToLocation = Convert.ToString(row["To Location"]);
                    VehicleDataList.TransDate = Convert.ToString(row["TransDate"]);
                    VehicleDataList.Size = Convert.ToString(row["Trailer Size"]);
                    VehicleDataList.DriverName = Convert.ToString(row["Drivername"]);
                    VehicleDataList.Transporter = Convert.ToString(row["Transporter"]);
                    VehicleDataList.MobileNo = Convert.ToString(row["MobileNo"]);
                    VehicleDataList.TrolleyNo = Convert.ToString(row["TrolleyNo"]);
                    VehicleDataList.DWellHours = Convert.ToString(row["Dwell Hours"]);
                    VehicleDataList.Driverid = Convert.ToString(row["driverid"]);

                    Vehiclereporting.Add(VehicleDataList);

                }
            }
            return Vehiclereporting;

        }

        public string SaveVehiclePendencySummary(string Reprtingdate, string Trailerno, string Driverid, string Contactno, string Remakes, int userid)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = transportdatamanager.SaveVehiclePendencySummary(Reprtingdate, Trailerno, Driverid, Contactno, Remakes, userid);
            if (updateDl.Rows.Count > 0)
            {
                message = Convert.ToString(updateDl.Rows[0][0]);

            }

            message = "Records saved Successfully!";
            return message;
        }
        public string VehicleSaveLoadingPlan(string containerNo, string TrailerNo, long JONO, int Userid, string TOdegistaton, string Size)
        {
            string message = "";
            DataTable ContainerDT = new DataTable();
            ContainerDT = transportdatamanager.VehicleSaveLoadingPlan(containerNo, TrailerNo, JONO, Userid, TOdegistaton, Size);
            if (ContainerDT.Rows.Count > 0)
            {
                message = "Record Saved successfully";
            }
            return message;
        }


        public List<EN.PrintVehicleDetails> GetPrintDetailsforVehicle(string containerNo, string TrailerNo, long JONO)
        {
            List<EN.PrintVehicleDetails> readyVehiclereporting = new List<EN.PrintVehicleDetails>();
            DataTable VehicleDL = new DataTable();
            VehicleDL = transportdatamanager.GetVehicleReportingPrintDetails(containerNo, TrailerNo, JONO);
            int Count = 1;
            if (VehicleDL != null)
            {
                foreach (DataRow row in VehicleDL.Rows)
                {
                    EN.PrintVehicleDetails VehicleDataList = new EN.PrintVehicleDetails();
                  
                    VehicleDataList.PlanID = Convert.ToString(row["PlanID"]);
                    VehicleDataList.PlanDate = Convert.ToString(row["PlanDate"]);
                    VehicleDataList.JoNO = Convert.ToString(row["JoNO"]);
                    VehicleDataList.TrailerNo = Convert.ToString(row["trailername"]);
                    VehicleDataList.Containerno = Convert.ToString(row["Containerno"]);
                    VehicleDataList.fromlocation = Convert.ToString(row["fromlocation"]);
                    VehicleDataList.tolocation = Convert.ToString(row["tolocation"]);
                    VehicleDataList.size = Convert.ToString(row["size"]);
                    VehicleDataList.Drivername = Convert.ToString(row["drivername"]);
                    VehicleDataList.ChaName = Convert.ToString(row["chaname"]);
                  



                    readyVehiclereporting.Add(VehicleDataList);

                }
            }
            return readyVehiclereporting;

        }

        public string SaveVendorName(string TrailerName, string vendor)
        {
            string message = "";
            DataTable ContainerDT = new DataTable();
            ContainerDT = transportdatamanager.SaveVendorName(TrailerName, vendor);
            if (ContainerDT.Rows.Count > 0)
            {
                message = "Record Saved successfully";
            }
            return message;
        }

    }
}
