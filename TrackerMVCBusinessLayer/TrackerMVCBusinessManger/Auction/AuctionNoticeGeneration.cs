using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Auction
{
   public class AuctionNoticeGeneration
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public DataSet AddAuctionGenData(EN.IGMEntities viewModel, DataTable dtContainer,int UserId,string NoticeType,string NoticeDate)
        {
            DataSet JobOrderDT = new DataSet();
            try
            {
                JobOrderDT = TrackerManager.AddAuctionGenIntoTable(viewModel.JoNo, viewModel.IGMNo, viewModel.ItemNo,viewModel.ImporterName,viewModel.CargoDescription,viewModel.Weight,viewModel.NoOfPkgs,viewModel.BLNo,viewModel.BLDate,dtContainer, NoticeType, NoticeDate);
            }
            catch (Exception ex)
            {

                //AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);

            }
            return JobOrderDT;
        }

        public DataSet AddAuctionGenAutoData(int UserId, string NoticeType)
        {
            DataSet JobOrderDT = new DataSet();
            try
            {
                JobOrderDT = TrackerManager.AddAuctionGenAutoIntoTable(UserId, NoticeType);
            }
            catch (Exception ex)
            {

                //AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);

            }
            return JobOrderDT;
        }

        public DataTable AddRemarks(string Prefix)
        {
            DataTable JobOrderDT = new DataTable();
            try
            {
                JobOrderDT = TrackerManager.GetRemarks(Prefix);
            }
            catch (Exception ex)
            {

                //AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);

            }
            return JobOrderDT;
        }

        public string AddAuctionStatus(string IGMNo, string ItemNo, string ContainerNo,int UserId, string Remarks, string ReferenceNo)
        {
            string Message = "";
            int i = 0;
            try
            {
                i = TrackerManager.AddAuctionGenStatus(IGMNo, ItemNo, ContainerNo, UserId, Remarks, ReferenceNo);
            }
            catch (Exception ex)
            {

                Message = ex.Message;

            }

            if (i > 0)
            {
                Message = "Data Updated Successfully.";
            }
            else { Message = "Data Not Updated Successfully."; }

            return Message;
        }

        public string AddAuctionClearDetails(string IGMNo, string ItemNo, string ContainerNo, int UserId, string Value, string DutyValue,string ClearRemarks)
        {
            string Message = "";
            int i = 0;
            try
            {
                i = TrackerManager.AddAuctionClearDetails(IGMNo, ItemNo, ContainerNo, UserId, Value, DutyValue, ClearRemarks);
            }
            catch (Exception ex)
            {

                Message = ex.Message;

            }

            if (i > 0)
            {
                Message = "Data Updated Successfully.";
            }
            else { Message = "Data Not Updated Successfully."; }

            return Message;
        }

        public List<EN.AuctionClearM> getAuctionClear()
        {
            List<EN.AuctionClearM> ShippersDL = new List<EN.AuctionClearM>();
            DataTable ShippersDT = new DataTable();
            string Table = "Auction_Cleared_M";
            string Column = "ClearId,ClearRemarks";
            string Condition = "";
            string OrderBy = "ClearId";

            ShippersDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ShippersDT != null)
            {
                foreach (DataRow row in ShippersDT.Rows)
                {
                    EN.AuctionClearM ShippersList = new EN.AuctionClearM();
                    ShippersList.ClearId = Convert.ToInt32(row["ClearId"]);
                    ShippersList.ClearRemarks = Convert.ToString(row["ClearRemarks"]);

                    ShippersDL.Add(ShippersList);
                }
            }
            return ShippersDL;
        }
    }
}
