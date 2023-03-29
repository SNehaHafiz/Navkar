using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using System.Data;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.LocationMaster
{
   public class LocationMaster
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        //public List<EN.LocationEntities> GetExisitingLocationame(string name, int id)
        //{
        //    DataTable dt = new DataTable();

        //    List<EN.LocationEntities> LocationList = new List<EN.LocationEntities>();
        //    dt = trackerdatamanager.GetExisitingLocationame(name, id);
        //    if (dt.Rows.Count > 0)
        //    {

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            EN.LocationEntities LocationDl = new EN.LocationEntities();
        //            LocationDl.Criteria = Convert.ToString(row["Criteria"]);
        //            LocationDl.ID = Convert.ToInt32(row["ID"]);
        //            LocationList.Add(LocationDl);
        //        }
        //    }
        //    return LocationList;
        //}

        public List<EN.LocationEntities> GetExisitingLocationame()
        {
            List<EN.LocationEntities> passingDL = new List<EN.LocationEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetExisitingLocationame();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationEntities PassingList = new EN.LocationEntities();
                    PassingList.ID = Convert.ToInt32(row["locationgroupid"]);
                    PassingList.Criteria = Convert.ToString(row["locationgroupname"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.LocationEntities> GetExisitingLocationameOther()
        {
            List<EN.LocationEntities> passingDL = new List<EN.LocationEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetExisitingLocationame();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationEntities PassingList = new EN.LocationEntities();
                    PassingList.IDOtherLocation = Convert.ToInt32(row["locationgroupid"]);
                    PassingList.CriteriaOther = Convert.ToString(row["locationgroupname"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.DistanceEntities> GetExisitingDistanceame()
        {
            List<EN.DistanceEntities> passingDL = new List<EN.DistanceEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetExisitingDistanceame();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DistanceEntities PassingList = new EN.DistanceEntities();
                    PassingList.DistanceGroupID = Convert.ToInt32(row["distancegroup_id"]);
                    PassingList.DistanceGroup = Convert.ToString(row["distancegroup_name"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        public string Insertlocation(string LocationName)
        {
            string Message = "";
            DataTable userDataDL = new DataTable();

            userDataDL = trackerdatamanager.InsertLocationGroup(LocationName);
            return Message;
        }
        public string InsertDirectionGroup(string DistanceName)
        {
            string Message = "";
            DataTable userDataDL = new DataTable();

            userDataDL = trackerdatamanager.InsertDrivertion(DistanceName);
            return Message;
        }

        public List<EN.LocationEditEntiites> GetLocationDetails(string LocationID)
        {
            List<EN.LocationEditEntiites> passingDL = new List<EN.LocationEditEntiites>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetLocationDetailsforedit(LocationID);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationEditEntiites PassingList = new EN.LocationEditEntiites();
                    PassingList.ID = Convert.ToInt32(row["LocationID"]);
                    PassingList.Location = Convert.ToString(row["Location Master"]);
                    PassingList.KiloMeter = Convert.ToString(row["Kilo-Meter"]);
                    PassingList.DistanceGroupID = Convert.ToString(row["Distance Group Name"]);
                    PassingList.LocationGroupID = Convert.ToString(row["Location Group Name"]);
                    PassingList.OnWayKM = Convert.ToString(row["One Way KM"]);
                    PassingList.TwoWayKM = Convert.ToString(row["Two Way KM"]);
                    PassingList.LocationOtherGroupId = Convert.ToString(row["LocOtherGroupId"]);

                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
    }
}
