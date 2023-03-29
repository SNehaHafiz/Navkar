using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql; 
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.VesselMaster
{
    public class VesselMaster
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public int AddVesselMasterDetails(EN.VesselMaster vessselMaster)
        {

            int loginData; 
            loginData = trackerdatamanager.AddVesselMasterDetails(vessselMaster.VesselID,vessselMaster.VesselName,vessselMaster.IsActive, vessselMaster.AddedBy,vessselMaster.ModifiedBy);
            return loginData;
        } 
        public bool GetExisitingVesselName(string vesselName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingVesselName(vesselName);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public int GetNewVesselID()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNewVesselID();
            int isCHACode = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    isCHACode = Convert.ToInt32(row["VesselID"]); ;
                }

            }
            return isCHACode;
        }

        public List<string> GetNameForAutoComplete(string vesselName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetVesselNameForAutoComplete(vesselName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["VesselName"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
    }
}
