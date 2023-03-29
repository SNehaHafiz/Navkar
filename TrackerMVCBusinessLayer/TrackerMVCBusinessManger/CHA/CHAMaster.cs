using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.CHA
{
    public class CHAMaster
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public int AddCHAMasterDetails(EN.CHAMaster chaMaster)
        {

            int loginData; 
            //DB.GetMyREQDBManager getmyreqdatamanager = new DB.GetMyREQDBManager();
            loginData = trackerdatamanager.AddCHAMasterDetails(chaMaster.CHAID, chaMaster.CHAName, chaMaster.Address, chaMaster.City, chaMaster.ContactPerson, chaMaster.Designation, chaMaster.ContactNo1, chaMaster.ContactNo2, chaMaster.FaxNumber, chaMaster.CellNumber, chaMaster.eMailIDs, chaMaster.Remarks, chaMaster.IsActive, chaMaster.Addedby, chaMaster.CHACode);
            return loginData;
        }
        public bool GetExisitingCHACode(string chaCode)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingCHACode(chaCode);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public bool GetExisitingCHAName(string chaName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingCHAName(chaName);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public int GetNewCHAID()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNewCHAID();
            int isCHACode = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    isCHACode = Convert.ToInt32(row["CHAID"]); ;
                }
                
            }
            return isCHACode;
        }

        public List<string> GetNameForAutoComplete(string chaName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNameForAutoComplete(chaName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["CHAName"]);
                    isCHACode.Add(name) ;
                }

            }
            return isCHACode;
        }
        public List<string> GetContactPersonForAutoComplete(string cpName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetContactPersonForAutoComplete(cpName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["ContactPerson"]);
                    isCHACode.Add(name) ;
                }

            }
            return isCHACode;
        }
        public List<string> GetDesignationForAutoComplete(string cpName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetDesignationForAutoComplete(cpName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["Designation"]);
                    isCHACode.Add(name) ;
                }

            }
            return isCHACode;
        }
        
    }
}
