using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.CreateUser
{
  public  class CreateUser
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        //public List<EN.CreateUser> GetFromDropdownList()
        //{

        //    List<EN.CreateUser> fromList = new List<EN.CreateUser>();
        //    DataTable DL = new DataTable();
        //    DL = TrackerManager.GetYardDetails();

        //    if (DL != null)
        //    {
        //        foreach (DataRow row in DL.Rows)
        //        {
        //            EN.CreateUser List = new EN.CreateUser();

        //            List.GroupID = Convert.ToString(row["GroupID"]);
        //            List.Name = Convert.ToString(row["GroupName"]);
        //            fromList.Add(List);
        //        }
        //    }
        //    return fromList;
        //}

        public List<EN.CreateUser> GetFrommenuList()
        {

            List<EN.CreateUser> fromList = new List<EN.CreateUser>();
            DataTable DL = new DataTable();
            DL = TrackerManager.MenuNameFill();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.CreateUser List = new EN.CreateUser();

                    List.GroupID = Convert.ToString(row["MenuDesc"]);
                    List.Name = Convert.ToString(row["MenuDesc"]);
                    fromList.Add(List);
                }
            }
            return fromList;
        }

        public List<EN.CreateUser> GetIconList()
        {

            List<EN.CreateUser> fromList = new List<EN.CreateUser>();
            DataTable DL = new DataTable();
            DL = TrackerManager.IconNameFill();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.CreateUser List = new EN.CreateUser();

                    List.icon = Convert.ToString(row["Icon"]);
                    List.icon = Convert.ToString(row["Icon"]);
                    fromList.Add(List);
                }
            }
            return fromList;
        }
        public List<EN.CreateUser> UserName()
        {

            List<EN.CreateUser> fromList = new List<EN.CreateUser>();
            DataTable DL = new DataTable();
            DL = TrackerManager.UserNameFill();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.CreateUser List = new EN.CreateUser();

                    List.UserID = Convert.ToString(row["UserID"]);
                    List.USerName = Convert.ToString(row["UserName"]);
                    fromList.Add(List);
                }
            }
            return fromList;
        }
        public string CheckFirstName(string HorseNumber)
        {
            string Message = "";
            DataTable CheckHorseDlL = new DataTable();
            CheckHorseDlL = TrackerManager.CheckFirstName(HorseNumber);

            if (CheckHorseDlL.Rows.Count > 0)
            {
                int HorseCount = Convert.ToInt16(CheckHorseDlL.Rows[0]["UserName"]);
                if (HorseCount > 0)
                {
                    Message = "1";

                }
            }


            return Message;
        }

        public List<EN.CreateUser> UserDetails(string UserID)
        {
            List<EN.CreateUser> passingDL = new List<EN.CreateUser>();
            DataTable dt = new DataTable();
            dt = TrackerManager.EditUserID(UserID);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CreateUser PassingList = new EN.CreateUser();
                    PassingList.firstname = Convert.ToString(row["UserName"]);
                    PassingList.lastname = Convert.ToString(row["UserName"]);
                    PassingList.gender = Convert.ToString(row["Gender"]);
                    PassingList.DOB = Convert.ToString(row["DOB"]);
                    PassingList.employeeid = Convert.ToString(row["EMPID"]);
                    PassingList.password = Convert.ToString(row["UserPass"]);
                    PassingList.department = Convert.ToString(row["DepType"]);
                    PassingList.userType = Convert.ToString(row["UserType"]);
                    PassingList.emailid = Convert.ToString(row["email_ID"]);
                    PassingList.passEmail = Convert.ToString(row["email_Pswrd"]);
                    PassingList.Contact = Convert.ToString(row["ContactNo"]);
                    PassingList.isactive = Convert.ToString(row["IsActive"]);
                    PassingList.UseriDN = Convert.ToInt32(row["UserID"]);

                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.CreateUser> FuelRateSetting()
        {

            List<EN.CreateUser> FuelVDDL = new List<EN.CreateUser>();
            DataTable vendorOBJ = new DataTable();
            string Table = "fuel_vendorM";
            string Column = "fuel_VendorID,Fuel_VendorName";
            string Condition = "";
            string OrderBy = "";

            vendorOBJ = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (vendorOBJ != null)
            {
                foreach (DataRow row in vendorOBJ.Rows)
                {
                    EN.CreateUser FuelVendorList = new EN.CreateUser();
                    FuelVendorList.FuelVID = Convert.ToInt32(row["fuel_VendorID"]);
                    FuelVendorList.FuelVName = Convert.ToString(row["Fuel_VendorName"]);
                    FuelVDDL.Add(FuelVendorList);
                }
            }
            return FuelVDDL;

        }
        public List<EN.CreateUser> FuelType()
        {
            List<EN.CreateUser> FuelDDl = new List<EN.CreateUser>();
            DataTable FuelTypes = new DataTable();
            string Table = "FuelMaster";
            string Column = "FuelID,FuelType";
            string Condition = "";
            string OrderBy = "";

            FuelTypes = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (FuelTypes != null)
            {
                foreach (DataRow row in FuelTypes.Rows)
                {
                    EN.CreateUser FuelList = new EN.CreateUser();
                    FuelList.FuelTID = Convert.ToInt32(row["FuelID"]);
                    FuelList.FuelType = Convert.ToString(row["FuelType"]);
                    FuelDDl.Add(FuelList);
                }
            }
            return FuelDDl;
        }

        public string CheckvendorName(string vendorid)
        {
            string Message = "";
            DataTable CheckHorseDlL = new DataTable();
            CheckHorseDlL = TrackerManager.CheckvendorName(vendorid);

            if (CheckHorseDlL.Rows.Count > 0)
            {
                int HorseCount = Convert.ToInt16(CheckHorseDlL.Rows[0]["VENDORID"]);
                if (HorseCount > 0)
                {
                    Message = "1";

                }
            }


            return Message;
        }

        public string SaveUserRight(DataTable UserRightData, int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("UserId", UserID);

            int i = db.AddTypeTableData("USP_InsUserRights", parameterList, UserRightData, "UserRightsDT", "@UserRightsDT");

            if (i > 0)
            {
                Message = "Rights Assing Successfully";
            }
            else
            {
                Message = "Rights Not Assing Successfully";
            }
            return Message;
        }
    }
}
