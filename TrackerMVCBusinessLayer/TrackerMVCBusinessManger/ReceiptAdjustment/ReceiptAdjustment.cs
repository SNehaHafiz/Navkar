using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ReceiptAdjustment
{
   public class ReceiptAdjustment
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public List<EN.ReceiptAdjustments> ReceiptFillDate(string Search, string ReceiptNo, string WorkYear)
        {
            List<EN.ReceiptAdjustments> passingDL = new List<EN.ReceiptAdjustments>();
            DataTable dt = new DataTable();
            dt = TrackerManager.ReceiptFillDate(Search,ReceiptNo, WorkYear);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ReceiptAdjustments PassingList = new EN.ReceiptAdjustments();
                    PassingList.Payfromid = Convert.ToString(row["payfromID"]);
                   PassingList.RecieptDate = Convert.ToString(row["ReceiptDate"]);
                   PassingList.NetAmount = Convert.ToString(row["ReceiptAmount"]);
                    PassingList.Criteria = Convert.ToString(row["Category"]);


                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.ReceiptAdjustments> getModeGroup()
        {
            List<EN.ReceiptAdjustments> ModeGroupDL = new List<EN.ReceiptAdjustments>();
            DataTable ModeGroupDLL = new DataTable();
            string Table = "payment_modes";
            string Column = "paymodeID,paymode";
            string Condition = "";
            string OrderBy = "";

            ModeGroupDLL = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ModeGroupDLL != null)
            {
                foreach (DataRow row in ModeGroupDLL.Rows)
                {
                    EN.ReceiptAdjustments CommodityGroupList = new EN.ReceiptAdjustments();
                    CommodityGroupList.ModeID = Convert.ToInt32(row["paymodeID"]);
                    CommodityGroupList.Mode = Convert.ToString(row["paymode"]);
                    ModeGroupDL.Add(CommodityGroupList);
                }
            }
            return ModeGroupDL;
        }

        public List<EN.ReceiptAdjustments> getBankGroup()
        {
            List<EN.ReceiptAdjustments> BankGroupDL = new List<EN.ReceiptAdjustments>();
            DataTable BankGroupDLL = new DataTable();
            string Table = "import_banks";
            string Column = "bankID,bankname";
            string Condition = "";
            string OrderBy = "";

            BankGroupDLL = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (BankGroupDLL != null)
            {
                foreach (DataRow row in BankGroupDLL.Rows)
                {
                    EN.ReceiptAdjustments BankGroupList = new EN.ReceiptAdjustments();
                    BankGroupList.BankID = Convert.ToString(row["bankID"]);
                    BankGroupList.BankName = Convert.ToString(row["bankname"]);
                    BankGroupDL.Add(BankGroupList);
                }
            }
            return BankGroupDL;
        }

        public List<EN.ReceiptAdjustments> Category()
        {
            List<EN.ReceiptAdjustments> Category = new List<EN.ReceiptAdjustments>();
            DataTable BankGroupDLL = new DataTable();
            string Table = "Category";
            string Column = "Value,Category";
            string Condition = "";
            string OrderBy = "";

            BankGroupDLL = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (BankGroupDLL != null)
            {
                foreach (DataRow row in BankGroupDLL.Rows)
                {
                    EN.ReceiptAdjustments CategoryList = new EN.ReceiptAdjustments();
                    CategoryList.ID = Convert.ToString(row["Value"]);
                    CategoryList.Criteria = Convert.ToString(row["Category"]);
                    Category.Add(CategoryList);
                }
            }
            return Category;
        }

        public List<EN.ReceiptAdjustments> Categorys()
        {
            List<EN.ReceiptAdjustments> Category = new List<EN.ReceiptAdjustments>();
            DataTable BankGroupDLL = new DataTable();
            string Table = "Category";
            string Column = "ID,Category";
            string Condition = "";
            string OrderBy = "";

            BankGroupDLL = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (BankGroupDLL != null)
            {
                foreach (DataRow row in BankGroupDLL.Rows)
                {
                    EN.ReceiptAdjustments CategoryList = new EN.ReceiptAdjustments();
                    CategoryList.ID = Convert.ToString(row["ID"]);
                    CategoryList.Criteria = Convert.ToString(row["Category"]);
                    Category.Add(CategoryList);
                }
            }
            return Category;
        }

        //public String SaveExternal(Int32 userId, string ReceiptNo, string WorkYear, DataTable SelectedData)
        //{
        //    string message = "";

        //    TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
        //    Dictionary<object, object> lstparam = new Dictionary<object, object>();
        //    lstparam.Add("userId", userId);

        //    TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


        //    int i = db.UpdateDischargeDate("USP_RECEIPT_ADJUSTMENT_INSERT", lstparam, ReceiptNo, WorkYear, SelectedData);
        //    if (i != 0)
        //    {
        //        message = "Records updated successfully";
        //    }
        //    else
        //    {
        //        message = "Records not update, please try again";
        //    }
        //    return message;
        //}


        public List<EN.Customer> getParty()
        {
            List<EN.Customer> CustomerDL = new List<EN.Customer>();
            DataTable CustomerDT = new DataTable();
            string Table = "Common_Master";
            string Column = "distinct m_common_id,Name";
            string Condition = "";
            string OrderBy = "Name";

            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.Customer CustomerList = new EN.Customer();
                    CustomerList.AGID = Convert.ToInt32(row["m_common_id"]);
                    CustomerList.AGName = Convert.ToString(row["Name"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }



        public string SaveReceiptChangesDetails(string ReceiptNo, string WorkYear, string ReceiptDate, string Payfromid,  int Userid, string Category, DataTable SelectedData)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("ReceiptNo", ReceiptNo);
            parameterList.Add("WorkYear", WorkYear);
            parameterList.Add("ReceiptDate", ReceiptDate);
            parameterList.Add("Payfromid", Payfromid);
            parameterList.Add("Userid", Userid);
            parameterList.Add("Category", Category);
            int i = db.AddTypeTableData("USP_RECEIPT_ADJUSTMENT_INSERT", parameterList, SelectedData, "ReceiptMovementSave", "@ReceiptMovementSave");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }
    }
}
