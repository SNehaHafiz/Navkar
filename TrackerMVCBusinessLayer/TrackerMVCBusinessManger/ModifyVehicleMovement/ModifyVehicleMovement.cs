using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ModifyVehicleMovement
{
   public class ModifyVehicleMovement
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public EN.ModifyVehicleMovementEntities GetAccidentSearchDetails(string Activity, string ContainerNo)
        {
            EN.ModifyVehicleMovementEntities ImportSearchList = new EN.ModifyVehicleMovementEntities();
            DataTable ImportDL = new DataTable();
            ImportDL = trackerdatamanager.GetActivitySearch(Activity, ContainerNo);

            if (ImportDL.Rows.Count != 0)
            {
                foreach (DataRow row in ImportDL.Rows)
                {
                    EN.ModifyVehicleMovementEntities ImportSearchListdl = new EN.ModifyVehicleMovementEntities();
                    ImportSearchList.fromid = Convert.ToString(row["fromid"]);
                    ImportSearchList.toid = Convert.ToString(row["toid"]);
                    ImportSearchList.TrailerNo = Convert.ToString(row["TrailerNo"]);
                   

                    //ImportSearchList.Add(ImportSearchListdl);
                }
            }

            return ImportSearchList;
        }

        public List<EN.LocationFrom> GetExisitingLocationame(string name, int id)
        {
            List<EN.LocationFrom> passingDL = new List<EN.LocationFrom>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetLocationame(name, id);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationFrom PassingList = new EN.LocationFrom();
                    PassingList.ID = Convert.ToInt32(row["LocationID"]);
                    PassingList.Criteria = Convert.ToString(row["Location"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.LocationTo> GetELocationame(string name, int id)
        {
            List<EN.LocationTo> passingDL = new List<EN.LocationTo>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetLocationame(name, id);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationTo PassingList = new EN.LocationTo();
                    PassingList.ToID = Convert.ToInt32(row["LocationID"]);
                    PassingList.ToCriteria = Convert.ToString(row["Location"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.trailer> getTrailerNo(string TrailerNo)
        {
            List<EN.trailer> PortsDL = new List<EN.trailer>();
            DataTable PortsDT = new DataTable();
            PortsDT = trackerdatamanager.GetTrailerNo(TrailerNo);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.trailer PortsList = new EN.trailer();
                    //PortsList.trailerid = Convert.ToInt16(row["trailerid"]);
                    PortsList.trailerName = Convert.ToString(row["trailername"]);

                        PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }


        public string UpdateTrailerDetails(DataTable dataTable, string MovementType, string VehicleNo,
            string LocationFrom, string Tolocation, string NewVehicleNo,
            string Remarks ,int userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            // parameterList.Add("Voucherno", Voucherno);
            parameterList.Add("MovementType", MovementType);
            parameterList.Add("VehicleNo", VehicleNo);
            parameterList.Add("LocationFrom", LocationFrom);
            parameterList.Add("Tolocation", Tolocation);
            parameterList.Add("NewVehicleNo", NewVehicleNo);
            parameterList.Add("Remarks", Remarks);
            parameterList.Add("userid", userid);
           
            int i = db.AddTypeTableData("USP_UpdateVehicleMovementDetails", parameterList, dataTable, "PT_AddVehicleMovementDetails", "@PT_AddVehicleMovementDetails");


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


        public List<EN.modifyVehiclemovemetDetailsentiites> GetVehicleMovementdetails(string VehicleNo, string Actvitiy)
        {
            List<EN.modifyVehiclemovemetDetailsentiites> passingDL = new List<EN.modifyVehiclemovemetDetailsentiites>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetModifiymovmentdetails(VehicleNo, Actvitiy);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.modifyVehiclemovemetDetailsentiites PassingList = new EN.modifyVehiclemovemetDetailsentiites();
                    PassingList.Transdate = Convert.ToString(row["Transdate"]);
                    PassingList.Activity = Convert.ToString(row["Activity"]);
                    PassingList.From = Convert.ToString(row["From"]);
                    PassingList.To = Convert.ToString(row["To"]);
                    PassingList.ContainerNo = Convert.ToString(row["Container No"]);
                    PassingList.Size = Convert.ToString(row["Size"]);
                    PassingList.Weight = Convert.ToString(row["Weight"]);
                    PassingList.JoNoEntryID = Convert.ToString(row["JoNoEntryID"]);
                    PassingList.ContainerType = Convert.ToString(row["Type"]);
                    PassingList.TransID = Convert.ToString(row["TransID"]);
                    PassingList.Process = Convert.ToString(row["Process"]);
                    PassingList.IsCancel = Convert.ToString(row["IsCancel"]);
                    PassingList.FromID = Convert.ToString(row["From ID"]);
                    PassingList.ToID = Convert.ToString(row["TO ID"]);
                    passingDL.Add(PassingList);


                    
                }
            }
            return passingDL;
        }
    }
}
