using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.CustomerMaster
{
    public class CustomerMaster
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public int AddCustomerMasterDetails(EN.CustomerMaster customerMaster)
        {

            int loginData;
            //DB.GetMyREQDBManager getmyreqdatamanager = new DB.GetMyREQDBManager();
            loginData = trackerdatamanager.AddCustomerMasterDetails(customerMaster.AGID, customerMaster.AGaID, customerMaster.AGName, customerMaster.AGAddress, customerMaster.AGCity, customerMaster.AGAuthPerson, customerMaster.AGAuthPersonDesig, customerMaster.AGTelI, customerMaster.AGTelII, customerMaster.AGFax, customerMaster.AGCellNo, customerMaster.AGEMail, customerMaster.AGWebsite, customerMaster.CreditPeriod, customerMaster.AGRemarks, customerMaster.IsActive,customerMaster.Addedby,customerMaster.AGGrade);
            return loginData;
        }
        public bool GetExisitingCustomerCode(string customerCode)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingAgentCode(customerCode);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public bool GetExisitingCustomerName(string customerName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingCustomerName(customerName);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public int GetNewCustomerID()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNewCustomerID();
            int isCHACode = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    isCHACode = Convert.ToInt32(row["customerID"]); ;
                }

            }
            return isCHACode;
        }

        public List<string> GetNameForAutoComplete(string custName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetCustNameForAutoComplete(custName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["AGName"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
        public List<string> GetContactPersonForAutoComplete(string cpName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetCustContactPersonForAutoComplete(cpName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["AGAuthPerson"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
        public List<string> GetDesignationForAutoComplete(string cpName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetCustDesignationForAutoComplete(cpName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["AGAuthPersonDesig"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }

    }
}
