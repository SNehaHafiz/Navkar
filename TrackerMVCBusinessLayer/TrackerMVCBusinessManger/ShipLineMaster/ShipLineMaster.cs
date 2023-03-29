using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql; 
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ShipLineMaster
{
    public class ShipLineMaster
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public int AddShipLineMasterDetails(EN.ShipLinesMaster shipLineMaster)
        {

            int loginData;
            //DB.GetMyREQDBManager getmyreqdatamanager = new DB.GetMyREQDBManager();
            loginData = trackerdatamanager.AddShipLineMasterDetails(shipLineMaster.SLID,shipLineMaster.SaID, shipLineMaster.SLName, shipLineMaster.SLAddressI, shipLineMaster.SLCity, shipLineMaster.SLAuthPerson, shipLineMaster.SLAuthPersonDesig,shipLineMaster.SLTelI, shipLineMaster.SLTelII, shipLineMaster.SLFax,shipLineMaster.SLAuthPersonCell, shipLineMaster.SLEMail, shipLineMaster.Remarks, shipLineMaster.SLIsActive,shipLineMaster.AddedBy,shipLineMaster.ModifiedBy, shipLineMaster.IsContract);
            return loginData;
        }
        public bool GetExisitingShippingLineCode(string shippingLineCode)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingShippingLineCode(shippingLineCode);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public bool GetExisitingShippingLineName(string shippingLineName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingShippingLineName(shippingLineName);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public int GetNewShippingLineID()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNewShippingLineID();
            int isCHACode = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    isCHACode = Convert.ToInt32(row["SLID"]); ;
                }

            }
            return isCHACode;
        }

        public List<string> GetNameForAutoComplete(string custName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetSLNameForAutoComplete(custName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["SLName"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
        public List<string> GetContactPersonForAutoComplete(string cpName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetSLContactPersonForAutoComplete(cpName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["SLAuthPerson"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
        public List<string> GetDesignationForAutoComplete(string cpName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetSLDesignationForAutoComplete(cpName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["SLAuthPersonDesig"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }

    }
}
