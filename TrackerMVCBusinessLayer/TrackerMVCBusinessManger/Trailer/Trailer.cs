using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Trailer
{
    public class Trailer
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public List<EN.TrailersEntities> getTrailer()
        {
            List<EN.TrailersEntities> TrailerList = new List<EN.TrailersEntities>();
            DataTable TrailerDT = new DataTable();

            TrailerDT = TrackerManager.GetTrailerList();
            if (TrailerDT != null)
            {
                foreach (DataRow row in TrailerDT.Rows)
                {
                    EN.TrailersEntities TrailerDTList = new EN.TrailersEntities();
                    TrailerDTList.trailerid = Convert.ToInt16(row["trailerid"]);
                    TrailerDTList.trailername = Convert.ToString(row["trailername"]);
                    TrailerDTList.Grade = Convert.ToString(row["Grade"]);
                    TrailerDTList.TransName = Convert.ToString(row["TransName"]);
                    TrailerDTList.TrollyID = Convert.ToInt64(row["TrolleyID"]);
                    TrailerList.Add(TrailerDTList);
                }
            }
            return TrailerList;
        }

        public String UpdateTrailerGrade(DataTable trailerList,Int32 userId)
        {
            string message = "";

            
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userId);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            int i = db.UpdateTrailerGrade("USP_UpdateGrade", lstparam, trailerList);
            if (i != 0)
            {
                message = "Records updated successfully";
            }
            else {
                message = "Records not update, please try again";
            }
            return message;
        }

        public String UpdateTrailerTrolley(DataTable trailerList, Int32 userId)
        {
            string message = "";

            
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userId);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            int i = db.UpdateTrailerTrolley("USP_UpdateTrolley", lstparam, trailerList);
            if (i != 0)
            {
                message = "Records updated successfully";
            }
            else {
                message = "Records not update, please try again";
            }
            return message;
        }
        public List<EN.TrolleyEntities> getTrolly()
        {

            List<EN.TrolleyEntities> TrollyList = new List<EN.TrolleyEntities>();
            DataTable TrollyDT = new DataTable();

            TrollyDT = TrackerManager.GetTrollyList();
            if (TrollyDT != null)
            {
                foreach (DataRow row in TrollyDT.Rows)
                {
                    EN.TrolleyEntities TrollyDTList = new EN.TrolleyEntities();
                    TrollyDTList.TrollyID = Convert.ToInt16(row["TrolleyID"]);
                    TrollyDTList.TrolleyNo = Convert.ToString(row["TrolleyNo"]);
                    TrollyDTList.TrolleySize = Convert.ToString(row["TrolleySize"]);
                    TrollyDTList.TrolleyWeight = Convert.ToString(row["TrolleyWeight"]);
                    TrollyList.Add(TrollyDTList);
                }
            }
            return TrollyList;
        }

        public List<EN.TrailersEntities> getTrollyForMapping()
        {

            List<EN.TrailersEntities> TrollyList = new List<EN.TrailersEntities>();
            DataTable TrollyDT = new DataTable();

            TrollyDT = TrackerManager.GetTrollyMappingList();
            if (TrollyDT != null)
            {
                foreach (DataRow row in TrollyDT.Rows)
                {
                    EN.TrailersEntities TrollyDTList = new EN.TrailersEntities();
                    TrollyDTList.TrollyID = Convert.ToInt16(row["TrolleyID"]);
                    TrollyDTList.TrolleyNo = Convert.ToString(row["TrolleyNo"]);
                    TrollyList.Add(TrollyDTList);
                }
            }
            return TrollyList;
        }
        public string AddTrolleyData(EN.TrailersEntities TrolleyData, int userId)
        {
            string message = "";
            try
            {

                message = TrackerManager.AddTrollyData(TrolleyData.TrollyID, TrolleyData.TrolleyNo, TrolleyData.IsActive, userId, TrolleyData.TrolleySize, TrolleyData.TrolleyWeight);

            }
            catch (Exception ex)
            {

            }
            return message;
        }

        public String UpdatePassing(string trailerNo, string NewtrailerNo, string Remarks, long trailerID, int Userid)
        {
            string message = "";
            int i;
            try
            {

                i = TrackerManager.UpdatePassing(trailerNo, NewtrailerNo, Remarks, trailerID, Userid);
                if (i > 0)
                {
                    message = "Record saved successfully!";
                }

            }
            catch (Exception ex)
            {

            }
            return message;
        }



        public List<EN.TrailersEntities> getTrailerDriver()
        {
            List<EN.TrailersEntities> TrailerList = new List<EN.TrailersEntities>();
            DataTable TrailerDT = new DataTable();

            TrailerDT = TrackerManager.GetTrailerDriverList();
            if (TrailerDT != null)
            {
                foreach (DataRow row in TrailerDT.Rows)
                {
                    EN.TrailersEntities TrailerDTList = new EN.TrailersEntities();
                    TrailerDTList.trailerid = Convert.ToInt16(row["trailerid"]);
                    TrailerDTList.trailername = Convert.ToString(row["trailername"]);                   
                    TrailerDTList.TransName = Convert.ToString(row["TransName"]);
                    TrailerDTList.DriverID1 = Convert.ToString(row["DriverID"]);
                    TrailerDTList.DriverName = Convert.ToString(row["drivername"]);
                    TrailerList.Add(TrailerDTList);
                }
            }
            return TrailerList;
        }

        //public string UpdateTrailerDriver(DataTable trailerList, Int32 userId)
        //{
        //    string message = "";
        //    int i;
        //    DataSet ds = new DataSet();
        //    Dictionary<object, object> lstparam = new Dictionary<object, object>();
        //    lstparam.Add("userId", userId);

        //    TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

        //    i = db.AddTypeTableData("USP_UpdateDriverTrolley", lstparam, trailerList, "PT_DriverTrailer", "@PT_DriverTrailer");
        //  //  ds = db.AddTypeRoadPlanningTableData("USP_UpdateDriverTrolley", lstparam, trailerList, "PT_DriverTrailer", "@PT_DriverTrailer");



        //    if (i != 0)
        //    {
        //        message = "Records updated successfully";
        //    }
        //    else
        //    {
        //        message = "Records not update, please try again";
        //    }
        //    return message;
        //}

        public string UpdateTrailerDriver(string trailerID, string driverID, int userId)
        {
            string message = ""; 
            int i;
            //DataTable ContainerDT = new DataTable();
            i = TrackerManager.UpdateTrailerDriver(trailerID, driverID, userId);
            if (i > 0)
            {
                message = "Record Saved successfully";
            }
            return message;
        }

        public List<EN.TrailersEntities> GetDriverno(string TrailerNo)
        {
            List<EN.TrailersEntities> PortsDL = new List<EN.TrailersEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = TrackerManager.GetDriverNo(TrailerNo);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TrailersEntities PortsList = new EN.TrailersEntities();
                    PortsList.DriverID = Convert.ToInt32(row["Driverid"]);
                    PortsList.trailername = Convert.ToString(row["drivername"]);

                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }

        public List<EN.TrailerSearchEntities> GetTrailerDetails(string Trailername)
        {
            List<EN.TrailerSearchEntities> TrailerList = new List<EN.TrailerSearchEntities>();
            DataTable TrailerDT = new DataTable();

            TrailerDT = TrackerManager.Gettrailerdetails(Trailername);
            if (TrailerDT != null)
            {
                foreach (DataRow row in TrailerDT.Rows)
                {
                    EN.TrailerSearchEntities TrailerDTList = new EN.TrailerSearchEntities();
                    TrailerDTList.CurrentLocation = Convert.ToString(row["location"]);
                    TrailerDTList.Status = Convert.ToString(row["vehiclestatus"]);
                    TrailerDTList.Trasnporter = Convert.ToString(row["transName"]);
                    TrailerDTList.Driver = Convert.ToString(row["drivername"]);
                    TrailerDTList.Fitness_Due_Date = Convert.ToString(row["Fitness_Due_Date"]);
                    TrailerDTList.Policy_End_Date = Convert.ToString(row["Policy_End_Date"]);
                    TrailerDTList.Tax_Due_Date = Convert.ToString(row["Tax_Due_Date"]);
                    TrailerDTList.usedfor = Convert.ToString(row["usedfor"]);
                    TrailerDTList.Lastdate = Convert.ToString(row["Lastdate"]);
                    TrailerDTList.green_tax_due = Convert.ToString(row["green_tax_due"]);
                    TrailerDTList.puc_date = Convert.ToString(row["puc_date"]);
                    TrailerList.Add(TrailerDTList);
                }
            }
            return TrailerList;
        }
    }
}
