using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.BLDataManager
{
    public class GSTSummary
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public List<EN.GSTEntities> getShippers(String SearchText, String Master)
        {
            List<EN.GSTEntities> GSTSummaryList = new List<EN.GSTEntities>();
            DataTable GSTDT = new DataTable();
            GSTDT = TrackerManager.GetGstList(SearchText, Master);
            if (GSTDT != null)
            {
                foreach (DataRow row in GSTDT.Rows)
                {
                    EN.GSTEntities GSTList = new EN.GSTEntities();
                    GSTList.Id = Convert.ToString(row["ID"]);
                    GSTList.Name = Convert.ToString(row["Name"]);
                    GSTList.Address = Convert.ToString(row["Address"]);
                    GSTList.ContactNo = Convert.ToString(row["contactNo"]);
                    GSTList.Email = Convert.ToString(row["MailID"]);
                    GSTList.ContactPerson = Convert.ToString(row["ContactPerson"]);
                    GSTSummaryList.Add(GSTList);
                }
            }
            return (GSTSummaryList);
        }

        public List<EN.GSTEntities> getGlobalGSTList(String SearchText)
        {
            List<EN.GSTEntities> GSTSummaryList = new List<EN.GSTEntities>();
            DataTable GSTDT = new DataTable();
            GSTDT = TrackerManager.GetGLobalGSTList(SearchText);
            if (GSTDT != null)
            {
                foreach (DataRow row in GSTDT.Rows)
                {
                    EN.GSTEntities GSTList = new EN.GSTEntities();
                    GSTList.Id = Convert.ToString(row["ID"]);
                    GSTList.Name = Convert.ToString(row["Name"]);
                    GSTList.Address = Convert.ToString(row["Address"]);
                    GSTList.ContactNo = Convert.ToString(row["contactNo"]);                    
                    GSTList.ContactPerson = Convert.ToString(row["ContactPerson"]);

                    GSTList.CHA = Convert.ToString(row["CHA"]);
                    GSTList.Shipper = Convert.ToString(row["Shipper"]);
                    GSTList.ShippingLine = Convert.ToString(row["ShippingLine"]);
                    GSTList.Consinee = Convert.ToString(row["consinee"]);
                    GSTList.Customer = Convert.ToString(row["Customer"]);
                    GSTList.Vendor = Convert.ToString(row["Vendor"]);

                    GSTSummaryList.Add(GSTList);
                }
            }
            return (GSTSummaryList);
        }

        public EN.MasterEntities GetCutomerData(Int64 id)
        {
            EN.MasterEntities UserData = new EN.MasterEntities();
            DataTable DT = new DataTable();
            DataTable MasterDT = new DataTable();

            DT = TrackerManager.GetUserData(id);
          //  MasterDT = TrackerManager.GetMasterExistData(Name);

            if (DT != null)
            {
                foreach (DataRow row in DT.Rows)
                {
                    UserData.AGID = Convert.ToInt64(row["ID"]);
                    UserData.AGaID = Convert.ToString(row["CommonCode"]);
                    UserData.AGName = Convert.ToString(row["Name"]);
                    UserData.AGAddress = Convert.ToString(row["Address"]);
                    UserData.AGAuthPerson = Convert.ToString(row["ContactPerson"]);
                    UserData.AGAuthPersonDesig = Convert.ToString(row["Designation"]);
                    UserData.AGTelI = Convert.ToString(row["ContactNo1"]);
                    UserData.AGTelII = Convert.ToString(row["ContactNo2"]);
                    UserData.AGFax = Convert.ToString(row["FaxNumber"]);
                    UserData.AGCellNo = Convert.ToString(row["cellnumber"]);
                    UserData.AGEMail = Convert.ToString(row["eMailIDs"]);
                    UserData.AGWebsite = Convert.ToString(row["webSite"]);
                    UserData.AGGrade = Convert.ToString(row["Grade"]);
                    UserData.IsContract = Convert.ToBoolean(row["Iscontract"]);
                    UserData.IsActive = Convert.ToBoolean(row["Isactive"]);
                    UserData.CHA = Convert.ToBoolean(row["Ischa"]);
                    UserData.Customer = Convert.ToBoolean(row["ISCustomer"]);
                    UserData.ShipLines = Convert.ToBoolean(row["ISLINE"]);
                    UserData.shippers = Convert.ToBoolean(row["ISShipper"]);
                    UserData.Importer = Convert.ToBoolean(row["ISImporter"]);
                    UserData.TallyLedger = Convert.ToString(row["TallyLedger"]);
                    UserData.JV = Convert.ToBoolean(row["JV"]);
                    UserData.Vendor = Convert.ToBoolean(row["Vendor"]);

                }
            }
            //if (MasterDT != null)
            //{
            //    foreach (DataRow row in MasterDT.Rows)
            //    {
            //        UserData.CHA = Convert.ToBoolean(row["CHA"]);
            //        UserData.Customer = Convert.ToBoolean(row["Customer"]);
            //        UserData.ShipLines = Convert.ToBoolean(row["ShipLines"]);
            //        UserData.shippers = Convert.ToBoolean(row["shippers"]);
            //        UserData.Importer = Convert.ToBoolean(row["Importer"]);
                   
            //    }
            //}

            return UserData;
        }

        public string UpdateMasterData(EN.MasterEntities MasterData, int userId)
        {
            string Message = "";
            DataTable dt = new DataTable();
            int i = 0;
            i = TrackerManager.UpdateMaster(MasterData.AGName, MasterData.AGaID, MasterData.AGAddress, MasterData.AGAuthPerson, MasterData.AGAuthPersonDesig,
                MasterData.AGTelI, MasterData.AGTelII, MasterData.AGEMail, MasterData.AGWebsite, MasterData.AGGrade, MasterData.IsContract, 
                MasterData.AGRemarks, userId, MasterData.CHA, MasterData.Customer, MasterData.shippers, MasterData.ShipLines, MasterData.Importer,
                MasterData.AGCellNo,MasterData.AGFax,MasterData.IsActive,MasterData.AGID,MasterData.TallyLedger,MasterData.JV,MasterData.Vendor);
            if (i == 0)
            {
                Message = "Record not saved, Please try again!";
                
            }
            else {
                Message = "Record saved successfully.";
            }
            return Message;
        }

        public int GetExisitingCode(string Code)
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetExisitingCode(Code);
          //  bool isCode = false;
            int res = 1;

            if (codeDL.Rows.Count == 0)
            {
               // isCode = true;
                res = 0;
            }
           // return isCode;
            return res;
        }

        public int GetExisitingName(string Name,Int64 id)
        {
            DataTable NameDT = new DataTable();
            NameDT = TrackerManager.GetExisitingName(Name,id);
            bool isCode = false;
            int res = 1;
            if (NameDT.Rows.Count == 0)
            {
               // isCode = true;
                res = 0;
            }
            return res;
        }

        //Codes By Arti

        public List<EN.Ext_location_Master> GetLocationList()
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetLocationDetails();


            List<EN.Ext_location_Master> isCHACode = new List<EN.Ext_location_Master>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.Ext_location_Master oper = new EN.Ext_location_Master();
                    oper.LocationID = Convert.ToInt32(row["LocationID"]);
                    oper.Location = Convert.ToString(row["Location"]);
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }

        public List<EN.GST_Registration_Type> GetGSTRegistrationType()
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetGSTRegistrationType();


            List<EN.GST_Registration_Type> isCHACode = new List<EN.GST_Registration_Type>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.GST_Registration_Type oper = new EN.GST_Registration_Type();
                    oper.RGID = Convert.ToInt32(row["RGID"]);
                    oper.RGType = Convert.ToString(row["RGType"]);
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }

        public string AddLocationDetails(EN.LocationMaster MasterData, int userId)
        {
            string Message = "";
            DataTable dt = new DataTable();


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("Common_ID", MasterData.Common_ID);
            parameterList.Add("Name", MasterData.Name);
            parameterList.Add("GSTIn_uniqID", MasterData.GSTIn_uniqID);
            parameterList.Add("RegistrationType", MasterData.TyepReg);
            parameterList.Add("RegDate", MasterData.RegDate);
            parameterList.Add("PANNo", MasterData.PANNo);
            parameterList.Add("LocationID", MasterData.LocationID);
            parameterList.Add("State", MasterData.State);
            parameterList.Add("state_Code", MasterData.state_Code);
            parameterList.Add("GSTAddress", MasterData.GSTAddress);
            parameterList.Add("Emailid", MasterData.Emailid);
            parameterList.Add("MobNo", MasterData.MobNo);
            parameterList.Add("userId", userId);
            parameterList.Add("IsCopyID", MasterData.IsCopy);
            parameterList.Add("GSTID", MasterData.GSTID);
            parameterList.Add("PINCODE", MasterData.PINCODE);
            parameterList.Add("IsActive", MasterData.IsActive);
            parameterList.Add("TANNo", MasterData.TANNo);


            int i = db.SaveLocationDetails("USP_LocationGSTInsertDetails", parameterList);


            if (i == 0)
            {
                Message = "Record not save, Please try again!";

            }
            else
            {
                Message = "Recode saved successfully!";
            }
            return Message;
        }

        public List<EN.StateMaster> getStateCode(string GSTNO)
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetStateCode(GSTNO);


            List<EN.StateMaster> isCHACode = new List<EN.StateMaster>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.StateMaster oper = new EN.StateMaster();
                    oper.State_Code = Convert.ToString(row["state_code"]);
                    oper.State = Convert.ToString(row["State"]);
                    oper.State_ID = Convert.ToInt16(row["State_ID"]);
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }



        //Codes By Arti

        ///By durga
        public List<EN.Ext_location_Master> GetCustomerLocationList(Int64 id)
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.GetCustomerLocationList(id);


            List<EN.Ext_location_Master> DL = new List<EN.Ext_location_Master>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.Ext_location_Master locationList = new EN.Ext_location_Master();
                    locationList.LocationID = Convert.ToInt16(row["LocationID"]);
                    locationList.Location = Convert.ToString(row["Location"]);
                    locationList.GSTID = Convert.ToInt64(row["GSTID"]);
                    DL.Add(locationList);
                }

            }
            return DL;
        }



        public EN.LocationMaster getLocationDataCustomerWise(Int32 LocationID, Int64 Common_ID, Int64 GSTID)
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.GetGSTLocationWiseData(LocationID, Common_ID, GSTID);


            EN.LocationMaster DL = new EN.LocationMaster();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    
                    DL.LocationID = Convert.ToInt16(row["LocationID"]);
                    DL.TyepReg = Convert.ToString(row["TyepReg"]);
                    DL.Emailid = Convert.ToString(row["Emailid"]);
                    DL.GSTIn_uniqID = Convert.ToString(row["GSTIn_uniqID"]);
                    DL.PANNo = Convert.ToString(row["panno"]);
                    DL.GSTregDate = Convert.ToString(row["RegDate"]);
                    DL.State = Convert.ToString(row["State"]);
                    DL.state_Code = Convert.ToString(row["state_Code"]);
                    DL.GSTAddress = Convert.ToString(row["GSTAddress"]);
                    DL.MobNo = Convert.ToString(row["MobNo"]);
                    DL.RGID = Convert.ToInt32(row["RGID"]);
                    DL.GSTID = Convert.ToInt64(row["GSTID"]);
                    DL.State_ID = Convert.ToInt32(row["State_ID"]);
                    DL.PINCODE = Convert.ToString(row["PINCODE"]);
                    DL.TANNo = Convert.ToString(row["TANNo"]);
                }

            }
            return DL;
        }
        //start by rahul
        public List<EN.DeliveryAddresses> GetDeliveryAddresses()
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetLocationDetails();


            List<EN.DeliveryAddresses> deliveryAddresses = new List<EN.DeliveryAddresses>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.DeliveryAddresses delivery = new EN.DeliveryAddresses();
                    delivery.LocationID = Convert.ToInt32(row["LocationID"]);
                    delivery.Location = Convert.ToString(row["Location"]);
                    deliveryAddresses.Add(delivery);
                }

            }
            return deliveryAddresses;
        }
        public List<EN.DeliveryAddresses> GetPreviousDeliveryAddresses(Int64 id)
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.GetDeliveryAddressData(id);


            List<EN.DeliveryAddresses> DL = new List<EN.DeliveryAddresses>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DeliveryAddresses deliveryList = new EN.DeliveryAddresses();
                    deliveryList.LocationID = Convert.ToInt16(row["LocationID"]);
                    deliveryList.Location = Convert.ToString(row["Location"]);
                    deliveryList.ID = Convert.ToInt64(row["ID"]);
                    DL.Add(deliveryList);
                }

            }
            return DL;
        }
        public EN.DeliveryAddresses getLocationWiseDeliveryAddress(Int32 LocationID, Int64 Common_ID, Int64 GSTID)
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.GetDeliveryAdressWiseData(LocationID, Common_ID, GSTID);


            EN.DeliveryAddresses DL = new EN.DeliveryAddresses();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    DL.LocationID = Convert.ToInt16(row["LocationID"]);
                    DL.Address = Convert.ToString(row["DeliveryAddress"]);
                    DL.ID = Convert.ToInt16(row["ID"]);

                }

            }
            return DL;
        }
        public string AddDeliveryAddresses(EN.DeliveryAddresses MasterData, int userId)
        {
            string Message = "";
            DataTable dt = new DataTable();
            dt = TrackerManager.SaveDeliveryAddresses(MasterData.LocationID, MasterData.Address, userId, MasterData.common_id, MasterData.ID, MasterData.IsCopy);            
            return Message;
        }
        public EN.DeliveryAddresses getDuplicateValidation(EN.DeliveryAddresses MasterData)
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.GetDuplicateDeliveryAddressValidation(MasterData.LocationID, MasterData.Address, MasterData.common_id);


            EN.DeliveryAddresses DL = new EN.DeliveryAddresses();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    DL.IsDuplicate = Convert.ToBoolean(row["IsDuplicate"]);

                }

            }
            return DL;
        }
        //end by rahul
    }
}
