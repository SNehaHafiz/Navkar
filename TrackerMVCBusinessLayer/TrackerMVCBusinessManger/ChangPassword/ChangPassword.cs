using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using BE = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ChangPassword
{
  public  class ChangPassword
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<BE.ChangPassword> getExpenses()
        {
            List<BE.ChangPassword> ExpensesrDL = new List<BE.ChangPassword>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "UserDetails";
            string Column = "isnull(UserID,0)UserID,UserName";
            string Condition = "";
            string OrderBy = "UserName";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.ChangPassword CustomerList = new BE.ChangPassword();
                    CustomerList.userID = Convert.ToInt32(row["UserID"]);
                    CustomerList.UserName = Convert.ToString(row["UserName"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }
        public string CheckOldPass(string oldtpassword, int userID)
        {
            string Message = "";
            DataTable CheckHorseDlL = new DataTable();
            CheckHorseDlL = trackerdatamanager.CheckOldPass(oldtpassword, userID);

            if (CheckHorseDlL.Rows.Count > 0)
            {
                int HorseCount = Convert.ToInt16(CheckHorseDlL.Rows[0]["Count"]);
                if (HorseCount > 0)
                {
                    Message = "1";

                }
            }


            return Message;
        }
    }
}
