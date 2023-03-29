using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EN = TrackerMVCEntities.BusinessEntities;
//using DB = TrackerMVCDataLayer.TrackerMVCDBManager;
using DA = TrackerMVCDataLayer.Helper;
using DB = TrackerMVCDataLayer;


using System.Data;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.LashingAndChocking
{
    public class LashingAndChockingDataProvider
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.LashingAndChockingInfo> GetMaterialsReceivingConfirmationsList()
        {
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.GetMaterialsReceivingConfirmationsList();
            List<EN.LashingAndChockingInfo> ContainerList = new List<EN.LashingAndChockingInfo>();

            if (ContainerDT.Rows.Count != 0)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.LashingAndChockingInfo ContainerDL = new EN.LashingAndChockingInfo();

                    ContainerDL.AutoID = Convert.ToInt32(row["AutoID"]);
                    ContainerDL.ItemName = Convert.ToString(row["ItemName"]);
                    ContainerDL.ItemID = Convert.ToInt32(row["ItemID"]);
                    ContainerDL.ItemCode = Convert.ToString(row["ItemCode"]);
                    ContainerDL.GRNNo = Convert.ToString(row["GRNNo"]);

                    ContainerDL.IssueNo = Convert.ToString(row["IssueNo"]);
                    ContainerDL.Rate = Convert.ToDecimal(row["Rate"]);
                    ContainerDL.Qty = Convert.ToDecimal(row["Qty"]);
                    ContainerDL.SentByName = Convert.ToString(row["SentByName"]);
                    ContainerDL.SentOn = Convert.ToString(Convert.ToDateTime(row["SentOn"]).ToString("dd MMM yyyy"));

                    ContainerList.Add(ContainerDL);
                }

            }
            return ContainerList;
        }


        public List<EN.LashingAndChockingInfo> GetMaterialGroup()
        {
            List<EN.LashingAndChockingInfo> HSNSelect = new List<EN.LashingAndChockingInfo>();
            DataTable dataTable = new DataTable();
            string Table = "Material_Group_M";
            string Column = "isnull(ID,0)ID,GroupName";
            string Condition = "";
            string OrderBy = "GroupName";

            dataTable = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    EN.LashingAndChockingInfo HSNSelectlist = new EN.LashingAndChockingInfo();
                    HSNSelectlist.MaterialGroupID = Convert.ToInt32(row["ID"]);
                    HSNSelectlist.MaterialGroupName = Convert.ToString(row["GroupName"]);

                    HSNSelect.Add(HSNSelectlist);
                }
            }
            return HSNSelect;
        }

        public string UpdateMaterialGroup(string AutoID, int MaterialGroupID)
        {

            try
            {
                // HC.DBOperationForTrackerPurchase db = new HC.DBOperationForTrackerPurchase();

                string success = "";

                DataTable item = new DataTable();



                item = trackerdatamanager.UpdateMaterialGroup(AutoID, MaterialGroupID);

                if (item != null)
                {
                    success = Convert.ToString(item.Rows[0][0]);
                }
                return success;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // TEMP COM
        //public List<EN.LashingAndChockingInfo> GetMappedMaterialsReceivingConfirmationsList(string ItemName, string AllAutoID)
        //{
        //    DataTable ContainerDT = new DataTable();
        //    ContainerDT = trackerdatamanager.GetMappedMaterialsReceivingConfirmationsList(ItemName, AllAutoID);
        //    List<EN.LashingAndChockingInfo> ContainerList = new List<EN.LashingAndChockingInfo>();

        //    if (ContainerDT.Rows.Count != 0)
        //    {
        //        foreach (DataRow row in ContainerDT.Rows)
        //        {
        //            EN.LashingAndChockingInfo ContainerDL = new EN.LashingAndChockingInfo();

        //            ContainerDL.AutoID = Convert.ToInt32(row["AutoID"]);
        //            ContainerDL.ItemName = Convert.ToString(row["ItemName"]);
        //            ContainerDL.ItemID = Convert.ToInt32(row["ItemID"]);
        //            ContainerDL.ItemCode = Convert.ToString(row["ItemCode"]);
        //            ContainerDL.GRNNo = Convert.ToString(row["GRNNo"]);

        //            ContainerDL.IssueNo = Convert.ToString(row["IssueNo"]);
        //            ContainerDL.Rate = Convert.ToDecimal(row["Rate"]);
        //            ContainerDL.Qty = Convert.ToDecimal(row["Qty"]);
        //            ContainerDL.SentByName = Convert.ToString(row["SentByName"]);
        //            ContainerDL.SentOn = Convert.ToString(Convert.ToDateTime(row["SentOn"]).ToString("dd MMM yyyy"));

        //            ContainerList.Add(ContainerDL);
        //        }

        //    }
        //    return ContainerList;
        //}



        public List<EN.LashingAndChockingInfo> GetMappedMaterialsReceivingConfirmationsList(string ItemName, string AllAutoID, DataTable MaterialDetails)        {            DataTable ContainerDT = new DataTable();            ContainerDT = trackerdatamanager.GetMappedMaterialsReceivingConfirmationsList(ItemName, AllAutoID, MaterialDetails);            List<EN.LashingAndChockingInfo> ContainerList = new List<EN.LashingAndChockingInfo>();            if (ContainerDT.Rows.Count != 0)            {                foreach (DataRow row in ContainerDT.Rows)                {                    EN.LashingAndChockingInfo ContainerDL = new EN.LashingAndChockingInfo();                    ContainerDL.AutoID = Convert.ToInt32(row["AutoID"]);                    ContainerDL.ItemName = Convert.ToString(row["ItemName"]);                    ContainerDL.ItemID = Convert.ToInt32(row["ItemID"]);                    ContainerDL.ItemCode = Convert.ToString(row["ItemCode"]);                    ContainerDL.GRNNo = Convert.ToString(row["GRNNo"]);                    ContainerDL.IssueNo = Convert.ToString(row["IssueNo"]);                    ContainerDL.Rate = Convert.ToDecimal(row["Rate"]);                    ContainerDL.Qty = Convert.ToDecimal(row["Qty"]);                    ContainerDL.SentByName = Convert.ToString(row["SentByName"]);                    ContainerDL.SentOn = Convert.ToString(Convert.ToDateTime(row["SentOn"]).ToString("dd MMM yyyy"));                    ContainerList.Add(ContainerDL);                }            }            return ContainerList;        }

    }
}
