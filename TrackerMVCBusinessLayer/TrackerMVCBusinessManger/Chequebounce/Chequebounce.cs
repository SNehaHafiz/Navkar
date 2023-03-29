using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using BE = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;


namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.BL
{
   public  class Chequebounce
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<BE.Chequebounce> GetAssessNoInvoice(string CHEQUENO)
        {
            List<BE.Chequebounce> passingDL = new List<BE.Chequebounce>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetChequeBounce(CHEQUENO);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.Chequebounce PassingList = new BE.Chequebounce();
                    PassingList.RECEIPTNO = Convert.ToString(row["receiptno"]);
                    PassingList.ACTIVITY = Convert.ToString(row["Activity"]);
                    PassingList.CHEQUE_AMOUNT = Convert.ToString(row["modeamount"]);
                    PassingList.CHEQUEDATE = Convert.ToString(row["modedate"]);
                    PassingList.BANKNAME = Convert.ToString(row["bankname"]);

                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }


        public List<BE.Customer> getParty()
        {
            List<BE.Customer> CustomerDL = new List<BE.Customer>();
            DataTable CustomerDT = new DataTable();
            string Table = "payment_modes";
            string Column = "isnull(PayModeId,0)PayModeId,Paymode";
            string Condition = "";
            string OrderBy = "Paymode";

            CustomerDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    BE.Customer CustomerList = new BE.Customer();
                    CustomerList.AGID = Convert.ToInt32(row["PayModeId"]);
                    CustomerList.AGName = Convert.ToString(row["Paymode"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }

        public List<BE.Chequebounce> GetAssessReciept(string CHEQUENO)
        {
            List<BE.Chequebounce> passingDL = new List<BE.Chequebounce>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetAssessReciept(CHEQUENO);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.Chequebounce PassingList = new BE.Chequebounce();
                    PassingList.RECEIPTNO = Convert.ToString(row["receiptno"]);
                    PassingList.ACTIVITY = Convert.ToString(row["Activity"]);
                    PassingList.CHEQUE_AMOUNT = Convert.ToString(row["modeamount"]);
                    PassingList.CHEQUEDATE = Convert.ToString(row["modedate"]);
                    PassingList.BANKNAME = Convert.ToString(row["bankname"]);

                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

    }
}
