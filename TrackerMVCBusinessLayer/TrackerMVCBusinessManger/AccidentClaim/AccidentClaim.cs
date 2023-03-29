using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.AccidentClaim
{
   public class AccidentClaim
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.AccidentClaimEntities> GetAccidentSearchDetails(string EntryID)
        {
            List<EN.AccidentClaimEntities> ImportSearchList = new List<EN.AccidentClaimEntities>();
            DataTable ImportDL = new DataTable();
            ImportDL = trackerdatamanager.GetAccidentSearch(EntryID);

            if (ImportDL.Rows.Count != 0)
            {
                foreach (DataRow row in ImportDL.Rows)
                {
                    EN.AccidentClaimEntities ImportSearchListdl = new EN.AccidentClaimEntities();
                    ImportSearchListdl.trailername = Convert.ToString(row["trailername"]);
                    ImportSearchListdl.AccidentDate = Convert.ToString(row["AccidentDate"]);
                    ImportSearchListdl.Accident_Place = Convert.ToString(row["Accident_Place"]);
                    ImportSearchListdl.Driver_Name = Convert.ToString(row["Driver_Name"]);
                    ImportSearchListdl.PolicyNo = Convert.ToString(row["PolicyNo"]);
                    ImportSearchListdl.InsuranceCompany = Convert.ToString(row["InsuranceCompany"]);
                    ImportSearchListdl.EntryID = Convert.ToString(row["entryid"]);
                    
                    ImportSearchList.Add(ImportSearchListdl);
                }
            }

            return ImportSearchList;
        }
    }
}
