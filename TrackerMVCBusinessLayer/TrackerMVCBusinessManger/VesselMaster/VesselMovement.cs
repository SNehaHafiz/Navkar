using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql; 
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.VesselMovement
{
    
    public class VesselMovement
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public int AddVesselMovementDetails(EN.VesselMovement vessselMovement)
        {
            int loginData;
            
            loginData = trackerdatamanager.AddVesselMovementDetails(vessselMovement.VesselID, vessselMovement.ViaNO, vessselMovement.PortID,vessselMovement.berthingdate, vessselMovement.AddedBy, vessselMovement.ModifiedBy, vessselMovement.VoyageNo, vessselMovement.IGMNo, vessselMovement.IGMDate);
            return loginData;
        }
        public bool GetExisitingVesselID(string vesselNO)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingVesselIDForVesselMovement(vesselNO);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        //public bool GetExisitingViaNO(string viaNO)
        //{
        //    //DataTable codeDL = new DataTable();
        //    //codeDL = trackerdatamanager.GetExisitingViaNOForVesselMovement(viaNO);
        //    //bool isCHACode = false;

        //    //if (codeDL.Rows.Count == 0)
        //    //{
        //    //    isCHACode = true;
        //    //}
        //    //return isCHACode;
        //}

        public EN.VesselMovement GetExisitingViaNO(string igmno)
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetExisitingViaNOForVesselMovement(igmno);
            EN.VesselMovement VesselData = new EN.VesselMovement();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    VesselData.DateNTime = Convert.ToString(row["BerthingDateInformat"]);
                    VesselData.VesselID = Convert.ToInt32(row["VesselID"]);
                    VesselData.PortID = Convert.ToInt32(row["PortID"]);
                    VesselData.VoyageNo = Convert.ToString(row["VoyageNo"]);
                    VesselData.IGMDateFormat = Convert.ToString(row["IgmDateInformat"]);
                    VesselData.ViaNO = Convert.ToString(row["ViaNo"]);
                    VesselData.IGMNo = Convert.ToString(row["IgmNo"]);
                    // VesselData.IGMDate = Convert.ToString(row["ViaNo"]);

                }

            }
            return VesselData;
        }
        public int GetNewVesselMovementID()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNewVesselMovementID();
            int isCHACode = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    isCHACode = Convert.ToInt32(row["MovementID"]); ;
                }

            }
            return isCHACode;
        }

        //public EN.VesselMovement GetViaNOForAutoComplete(string viaNO)
        //{
        //    DataTable dt = new DataTable();
        //    dt = trackerdatamanager.GetViaNOForAutoComplete(viaNO);
        //    EN.VesselMovement VesselData = new EN.VesselMovement();

        //    if (dt.Rows.Count != 0)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            VesselData.DateNTime = Convert.ToString(row["BerthingDateInformat"]);
        //            VesselData.VesselID = Convert.ToInt32(row["VesselID"]);
        //            VesselData.PortID = Convert.ToInt32(row["PortID"]);
        //            VesselData.VoyageNo = Convert.ToString(row["VoyageNo"]);
                    
        //        }

        //    }
        //    return VesselData;
        //}

        public List<string> GetViaNOForAutoComplete(string viaNO)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetViaNOForAutoComplete(viaNO);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["IgmNo"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }

        public List<EN.VesselMaster> GetVesselListForVesselMovement()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetVesselListForVesselMovement();
            List<EN.VesselMaster> isCHACode = new List<EN.VesselMaster>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.VesselMaster name = new EN.VesselMaster();
                    name.VesselID = Convert.ToInt32(row["VesselID"]);
                    name.VesselName = Convert.ToString(row["VesselName"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }

        //public List<EN.Port> GetPortList()
        //{
        //    DataTable codeDL = new DataTable();
        //    codeDL = trackerdatamanager.GetPortList();
        //    List<EN.Port> isCHACode = new List<EN.Port>();

        //    if (codeDL.Rows.Count != 0)
        //    {
        //        foreach (DataRow row in codeDL.Rows)
        //        {
        //            EN.Port oper = new EN.Port();
        //            oper.PortID = Convert.ToInt32(row["PortID"]);
        //            oper.PortName = Convert.ToString(row["PortName"]);
        //            oper.Code = Convert.ToString(row["P_Code"]);
        //            oper.PortCode = "port" + oper.PortID;
        //            isCHACode.Add(oper);
        //        }

        //    }
        //    return isCHACode;
        //}

        public List<EN.Ports> getPorts()
        {
            List<EN.Ports> PortsDL = new List<EN.Ports>();
            DataTable PortsDT = new DataTable();
            string Table = "Ports";
            string Column = "PortID,PortName";
            string Condition = "";
            string OrderBy = "PortName";

            PortsDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.Ports PortsList = new EN.Ports();
                    PortsList.PortID = Convert.ToInt32(row["PortID"]);
                    PortsList.PortName = Convert.ToString(row["PortName"]);

                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }
        public string CheckVianoforSealcut(string ViaNo)
        {
            string Message = "";
            DataTable CheckHorseDlL = new DataTable();
            CheckHorseDlL = trackerdatamanager.CheckVianoforSealcut(ViaNo);

            if (CheckHorseDlL.Rows.Count > 0) {

                Message = "Seal Cutting Entry is done.Cannot proceed.";


            }


            return Message;
        }
    }
}
