using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.BID
{
   public class BID
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();

        public List<EN.BIDEntities> GetAssessNoInvoice(string IGMNo, string temNo)
        {
            List<EN.BIDEntities> passingDL = new List<EN.BIDEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetIGMVeiw(IGMNo, temNo);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.BIDEntities PassingList = new EN.BIDEntities();
                    PassingList.Descriptions = Convert.ToString(row["IGM_GoodsDesc"]);
                    //PassingList.AssessNo = Convert.ToString(row["Asse
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.BIDEntities> GetJonoChanges(string Jono)
        {
            List<EN.BIDEntities> passingDL = new List<EN.BIDEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetjonoVeiw(Jono);
            int Count = 1;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.BIDEntities PassingList = new EN.BIDEntities();
                    PassingList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    //PassingList.AssessNo = Convert.ToString(row["Asse
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.PartyNameEntities> GetGstPartyName()
        {
            List<EN.PartyNameEntities> CustomerDL = new List<EN.PartyNameEntities>();
            DataTable CustomerDT = new DataTable();
            string Table = "party_gst_m";
            string Column = "Common_ID,GSTName";
            string Condition = " isnull(Common_ID,0) <> 0";
            string OrderBy = "GSTName";

            CustomerDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.PartyNameEntities CustomerList = new EN.PartyNameEntities();
                    CustomerList.Common_ID = Convert.ToInt32(row["Common_ID"]);
                    CustomerList.GSTName = Convert.ToString(row["GSTName"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }


    }
}
