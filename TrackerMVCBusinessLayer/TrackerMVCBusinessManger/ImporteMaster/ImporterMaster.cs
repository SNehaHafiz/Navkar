using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ImporteMaster
{
    public class ImporterMaster
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public int AddImporterMasterDetails(EN.ImporterMaster importerMaster)
        {

            int loginData;
            //DB.GetMyREQDBManager getmyreqdatamanager = new DB.GetMyREQDBManager();
            loginData = trackerdatamanager.AddImporterMasterDetails(importerMaster.ImporterID, importerMaster.ImporterCode, importerMaster.ImporterName, importerMaster.ImpAddress, importerMaster.ImpCity, importerMaster.ImpAuthPerson, importerMaster.ImpAuthPersonDesig, importerMaster.ImpTelI, importerMaster.ImpTelII, importerMaster.ImpFax, importerMaster.ImpCellNo, importerMaster.ImpEMail, importerMaster.ImpPANNo, importerMaster.Remarks, importerMaster.IsActive,importerMaster.Addedby);
            return loginData;
        }

        public bool GetExisitingImporterCode(string impCode)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingImporterCode(impCode);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public bool GetExisitingImporterName(string impName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingImporterName(impName);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public int GetNewImporterID()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNewImporterID();
            int isCHACode = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    isCHACode = Convert.ToInt32(row["ImporterID"]); ;
                }

            }
            return isCHACode;
        }
        public List<string> GetNameForAutoComplete(string custName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetImpNameForAutoComplete(custName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["ImporterName"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
        public List<string> GetContactPersonForAutoComplete(string cpName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetImpContactPersonForAutoComplete(cpName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["ImpAuthPerson"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
        public List<string> GetDesignationForAutoComplete(string cpName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetImpDesignationForAutoComplete(cpName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["ImpAuthPersonDesig"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
    }
}
