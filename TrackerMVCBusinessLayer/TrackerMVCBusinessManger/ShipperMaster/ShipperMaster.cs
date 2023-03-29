using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql; 
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ShipperMaster
{
    public class ShipperMaster
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public int AddShipperMasterDetails(EN.ShipperMaster shipperMaster)
        {

            int loginData;
            //DB.GetMyREQDBManager getmyreqdatamanager = new DB.GetMyREQDBManager();
            loginData = trackerdatamanager.AddShipperMasterDetails(shipperMaster.ShipperID, shipperMaster.ShipperIECNO, shipperMaster.ShipperName, shipperMaster.ShipperAddress, shipperMaster.ShipperCity, shipperMaster.ShipperContactPerson, shipperMaster.ShipperPersonDesig, shipperMaster.ShipperContactNO1, shipperMaster.ShipperContatcNO2, shipperMaster.ShipperFax, shipperMaster.ShipperCellNo, shipperMaster.ShipperEMail, shipperMaster.Remarks, shipperMaster.IsActive, shipperMaster.AddedBY, shipperMaster.ModifiedBY);
            return loginData;
        }

        public bool GetExisitingShipperCode(string shipperCode)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingShipperCode(shipperCode);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public bool GetExisitingShipperName(string shipperName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingShipperName(shipperName);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public int GetNewShipperID()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNewShipperID();
            int isCHACode = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    isCHACode = Convert.ToInt32(row["shipperID"]); ;
                }

            }
            return isCHACode;
        }
        public List<string> GetNameForAutoComplete(string custName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetShipperNameForAutoComplete(custName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["shippername"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
        public List<string> GetContactPersonForAutoComplete(string cpName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetShipperContactPersonForAutoComplete(cpName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["ContactPerson"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
        public List<string> GetDesignationForAutoComplete(string cpName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetShipperDesignationForAutoComplete(cpName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["Designation"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
    }
}
