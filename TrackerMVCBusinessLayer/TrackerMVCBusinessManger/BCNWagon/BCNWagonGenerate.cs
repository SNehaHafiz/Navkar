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

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.BCNWagon
{
   public class BCNWagonGenerate
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();

        public List<EN.ShipLines> getShipLines()
        {
            List<EN.ShipLines> ShipLinesDL = new List<EN.ShipLines>();
            DataTable ShipLinesDT = new DataTable();
            string Table = "ShipLines";
            string Column = "SLID,SLName,SaID";
            string Condition = "SLIsActive=1";
            string OrderBy = "SLName";

            ShipLinesDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ShipLinesDT != null)
            {
                foreach (DataRow row in ShipLinesDT.Rows)
                {
                    EN.ShipLines ShipLinesList = new EN.ShipLines();
                    ShipLinesList.SLID = Convert.ToInt32(row["SLID"]);
                    ShipLinesList.SLName = Convert.ToString(row["SLName"]);
                    ShipLinesList.SLCode = Convert.ToString(row["SaID"]);

                    ShipLinesDL.Add(ShipLinesList);
                }
            }
            return ShipLinesDL;
        }

        public List<EN.Shippers> getShipper()
        {
            List<EN.Shippers> ShippersDL = new List<EN.Shippers>();
            DataTable ShippersDT = new DataTable();
            string Table = "exp_shippers";
            string Column = "shipperID,shippername";
            string Condition = "IsActive=1";
            string OrderBy = "shippername";

            ShippersDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ShippersDT != null)
            {
                foreach (DataRow row in ShippersDT.Rows)
                {
                    EN.Shippers ShipLinesList = new EN.Shippers();
                    ShipLinesList.shipperID = Convert.ToInt32(row["shipperID"]);
                    ShipLinesList.shippername = Convert.ToString(row["shippername"]);

                    ShippersDL.Add(ShipLinesList);
                }
            }
            return ShippersDL;
        }

        public List<EN.Ports> getPorts()
        {
            List<EN.Ports> PortsDL = new List<EN.Ports>();
            DataTable PortsDT = new DataTable();
            string Table = "Ports";
            string Column = "PortID,PortName";
            string Condition = "";
            string OrderBy = "PortName";

            PortsDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.Ports PortsList = new EN.Ports();
                    PortsList.PortID = Convert.ToInt32(row["PortID"]);
                    PortsList.PortName = Convert.ToString(row["PortName"]);

                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }

        public List<EN.StuffingType> getStuffing()
        {
            List<EN.StuffingType> stuffingType = new List<EN.StuffingType>();
            DataTable PortsDT = new DataTable();
            string Table = "StuffingTypeM";
            string Column = "StuffingTypeId,StuffingType";
            string Condition = "IsActive=1";
            string OrderBy = "StuffingType";

            PortsDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.StuffingType PortsList = new EN.StuffingType();
                    PortsList.StuffingTypeId = Convert.ToInt64(row["StuffingTypeId"]);
                    PortsList.StuffingTypeM = Convert.ToString(row["StuffingType"]);

                    stuffingType.Add(PortsList);
                }
            }
            return stuffingType;
        }

        public List<EN.Customer> getCustomer()
        {
            List<EN.Customer> CustomerDL = new List<EN.Customer>();
            DataTable CustomerDT = new DataTable();
            string Table = "Customer";
            string Column = "AGID,AGName";
            string Condition = "IsActive=1";
            string OrderBy = "AGName";

            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.Customer CustomerList = new EN.Customer();
                    CustomerList.AGID = Convert.ToInt32(row["AGID"]);
                    CustomerList.AGName = Convert.ToString(row["AGName"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }

        public List<EN.Location> getLocation()
        {
            List<EN.Location> LocationDL = new List<EN.Location>();
            DataTable LocationDT = new DataTable();
            string Table = "Ext_Location_M";
            string Column = "LocationId,Location";
            string Condition = "IsActive=1";
            string OrderBy = "LocationId";

            LocationDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (LocationDT != null)
            {
                foreach (DataRow row in LocationDT.Rows)
                {
                    EN.Location CustomerList = new EN.Location();
                    CustomerList.LocationID = Convert.ToInt32(row["LocationId"]);
                    CustomerList.LocationName = Convert.ToString(row["Location"]);

                    LocationDL.Add(CustomerList);
                }
            }
            return LocationDL;
        }

        public List<EN.CommodityGroup> getCommodityGroup()
        {
            List<EN.CommodityGroup> CommodityGroupDL = new List<EN.CommodityGroup>();
            DataTable CommodityGroupDT = new DataTable();
            string Table = "Commodity_Group_M";
            string Column = "Commodity_Group_ID,Commodity_Group_Name";
            string Condition = "IsActive=1";
            string OrderBy = "Commodity_Group_Name";

            CommodityGroupDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CommodityGroupDT != null)
            {
                foreach (DataRow row in CommodityGroupDT.Rows)
                {
                    EN.CommodityGroup CommodityGroupList = new EN.CommodityGroup();
                    CommodityGroupList.Commodity_Group_ID = Convert.ToInt32(row["Commodity_Group_ID"]);
                    CommodityGroupList.Commodity_Group_Name = Convert.ToString(row["Commodity_Group_Name"]);
                    CommodityGroupDL.Add(CommodityGroupList);
                }
            }
            return CommodityGroupDL;
        }

        public int AddBCNNocDetails(EN.BCNNocUpdation cNNocUpdation)
        {
            int loginData;
            loginData = TrackerManager.AddBCNNocDetails(cNNocUpdation.NOCNo,cNNocUpdation.NOCDate,cNNocUpdation.ShipperId, cNNocUpdation.ShipperName, cNNocUpdation.OrginStationId, cNNocUpdation.CommodityId, cNNocUpdation.StuffingTypeId,cNNocUpdation.NoOfWagonPlanned,cNNocUpdation.PlanedDate,cNNocUpdation.StuffingDate, cNNocUpdation.ETADate, cNNocUpdation.CreatedBy);
            return loginData;
        }

        public string AddBCNRRDownloadSave(EN.BCNRRDownload viewModel, int UserId, int FileId)
        {
            string message = "";
            int retVal = 0;
            try
            {

                retVal = TrackerManager.AddRRWagonDataIntoTable(FileId,viewModel.TrainNo,viewModel.FromStationId,viewModel.ToStationId,viewModel.CommodityId,viewModel.PartyId,viewModel.RRWagonNo,viewModel.ArrivalDate,viewModel.FreightAmount,UserId,viewModel.RRWagonContNo);

                if (retVal > 0)
                {
                    message = "Record Saved Successfully";
                }
                else
                {
                    message = "Record not Saved Successfully";
                }

            }
            catch (Exception ex)
            {

                //AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);

            }
            return message;
        }

        public int AddContainerCLPData(DataTable dtContainerList, int UserId, string TrainNo, string WagonRRNo)
        {
            string message = "";
            int value = 0;
            try
            {
                DataTable JobOrderDT = new DataTable();
                value = TrackerManager.AddContainerBCNDataIntoTable(dtContainerList, UserId, TrainNo, WagonRRNo);
            }
            catch (Exception ex)
            {

                //AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);

            }
            return value;
        }

        public int AddSBMappingData(DataTable dtContainerList, int UserId, string Condition,string RRType)
        {
            string message = "";
            int value = 0;
            try
            {
                DataTable JobOrderDT = new DataTable();
                value = TrackerManager.AddSBMappingBCNDataIntoTable(dtContainerList, UserId, Condition,RRType);
            }
            catch (Exception ex)
            {

                //AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);

            }
            return value;
        }

        public EN.JobOrderMEntities getVesselDetails(string ViaNo)
        {
            EN.JobOrderMEntities JE = new EN.JobOrderMEntities();
            DataTable VesselDT = new DataTable();
            VesselDT = TrackerManager.getVesselDetails(ViaNo);
            if (VesselDT.Rows.Count > 0)
            {
                JE.VesselID = Convert.ToInt32(VesselDT.Rows[0]["VesselID"]);
                JE.berthingDateInstring = Convert.ToString(VesselDT.Rows[0]["BerthingDate"]);
                JE.PortID = Convert.ToInt16(VesselDT.Rows[0]["portid"]);
                JE.IGMNo = Convert.ToString(VesselDT.Rows[0]["IgmNo"]);
            }


            return JE;
        }

        public EN.JobOrderMEntities getVesselDetailsExp(string ViaNo)
        {
            EN.JobOrderMEntities JE = new EN.JobOrderMEntities();
            DataTable VesselDT = new DataTable();
            VesselDT = TrackerManager.getVesselDetailsExp(ViaNo);
            if (VesselDT.Rows.Count > 0)
            {
                JE.VesselID = Convert.ToInt32(VesselDT.Rows[0]["VesselID"]);
                JE.berthingDateInstring = Convert.ToString(VesselDT.Rows[0]["BerthingDate"]);
                JE.PortID = Convert.ToInt16(VesselDT.Rows[0]["portid"]);
                JE.IGMNo = Convert.ToString(VesselDT.Rows[0]["IgmNo"]);
                JE.Voyage = Convert.ToString(VesselDT.Rows[0]["Voyage"]);
                JE.PortName = Convert.ToString(VesselDT.Rows[0]["POL"]);
                JE.RotationNo = Convert.ToString(VesselDT.Rows[0]["RotationNo"]);
            }


            return JE;
        }

        public List<EN.Vessel> getVessel()
        {
            List<EN.Vessel> VesselDL = new List<EN.Vessel>();
            DataTable VesselDT = new DataTable();
            string Table = "Vessels";
            string Column = "VesselID,VesselName";
            string Condition = "IsActive=1";
            string OrderBy = "VesselName";

            VesselDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (VesselDT != null)
            {
                foreach (DataRow row in VesselDT.Rows)
                {
                    EN.Vessel VesselList = new EN.Vessel();
                    VesselList.VesselID = Convert.ToInt32(row["VesselID"]);
                    VesselList.VesselName = Convert.ToString(row["VesselName"]);

                    VesselDL.Add(VesselList);
                }
            }
            return VesselDL;
        }

        public List<EN.POL> getPOL()
        {
            List<EN.POL> PolDL = new List<EN.POL>();
            DataTable PolDT = new DataTable();
            string Table = "PODMaster";
            string Column = "PODID,PODName";
            string Condition = "";
            string OrderBy = "PODName";

            PolDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PolDT != null)
            {
                foreach (DataRow row in PolDT.Rows)
                {
                    EN.POL PolList = new EN.POL();
                    PolList.PODID = Convert.ToInt32(row["PODID"]);
                    PolList.PODName = Convert.ToString(row["PODName"]);
                    PolDL.Add(PolList);
                }
            }
            return PolDL;
        }
    }
}
