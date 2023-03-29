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
    public class VesselSummary
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.Vessel> GetVesselSummaryList()
        {

            DataTable vesselDL = new DataTable();
            vesselDL = trackerdatamanager.GetVesselSummaryList();
            List<EN.Vessel> vesselBL = new List<EN.Vessel>();

            if (vesselDL.Rows.Count != 0)
            {
                foreach (DataRow row in vesselDL.Rows)
                {
                    EN.Vessel vessel = new EN.Vessel();
                    vessel.VesselID = Convert.ToInt32(row["VesselID"]);
                    vessel.VesselName = Convert.ToString(row["VesselName"]);
                    vesselBL.Add(vessel);
                }

            }
            return vesselBL;
        }

        public EN.VesselMaster EditVesselSummary(int vesselID)
        {

            DataTable vesselDL = new DataTable();
            vesselDL = trackerdatamanager.EditVesselSummary(vesselID);
            EN.VesselMaster  vesselBL = new  EN.VesselMaster();

            if (vesselDL.Rows.Count != 0)
            {
                foreach (DataRow row in vesselDL.Rows)
                {
                   
                    vesselBL.VesselID = Convert.ToInt32(row["VesselID"]);
                    vesselBL.VesselName = Convert.ToString(row["VesselName"]);
                    vesselBL.IsActive = Convert.ToBoolean(row["ISActive"]);
                }

            }
            return vesselBL;
        }
        public int UpdateVesselMasterDetails(int vesselID, string vesselName, bool isActive)
        {

            DataTable loginData = new DataTable();
            loginData = trackerdatamanager.updateVesselMasterDetails(vesselID, vesselName, isActive);
            int saved = 0;
            if (loginData.Rows != null)
            {
                saved = 1;
            }
            return saved;
        } 
    }
}
