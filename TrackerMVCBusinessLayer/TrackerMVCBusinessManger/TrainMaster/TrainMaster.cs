using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql; 
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrainMaster
{
    public class TrainMaster
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public int AddTrainMasterDetails(EN.TrainMaster trainMaster)
        {
            int message=0;
          //  DataTable dt = new DataTable();
            message = trackerdatamanager.AddTrainMasterDetails(trainMaster.TrainID, trainMaster.TrainNO, trainMaster.PortFrom, trainMaster.PortTO, trainMaster.OperatorID.ToString(), trainMaster.Operator, trainMaster.DateNTime, trainMaster.AddedBy, trainMaster.ModifiedBy, trainMaster.IsImportORExportSelected);
            //if (dt.Rows.Count > 0)
            //{
            //    message = Convert.ToInt32(dt.Rows[0][0]);
            //}
            return message;
        }

        public bool GetExisitingTrainNO(string trainCode)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingTrainNO(trainCode);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }

        public int GetNewTrainID()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNewTrainID();
            int isCHACode = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    isCHACode = Convert.ToInt32(row["trainID"]); 
                }

            }
            return isCHACode;
        }
        public List<string> GetNoForAutoComplete(string trainNo)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetTrainNoForAutoComplete(trainNo);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["trainno"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }

        public List<EN.OperatorMaster> GetOperatorListForTrainMaster()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetOperatorListForTrainMaster();
            List<EN.OperatorMaster> isCHACode = new List<EN.OperatorMaster>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.OperatorMaster oper = new EN.OperatorMaster();
                    oper.OperatorID = Convert.ToInt32(row["ID"]);
                    oper.OperatorName = Convert.ToString(row["Name"]);
                    oper.Code = Convert.ToString(row["Code"]);
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }


        public int GetNewOperatorID()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNewOperatorID();
            int isCHACode = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    isCHACode = Convert.ToInt32(row["ID"]); ;
                }

            }
            return isCHACode;
        }

        public List<EN.Port> GetPortList()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetPortList();
            List<EN.Port> isCHACode = new List<EN.Port>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.Port oper = new EN.Port();
                    oper.PortID = Convert.ToInt32(row["PortID"]);
                    oper.PortName = Convert.ToString(row["PortName"]);
                    oper.Code = Convert.ToString(row["P_Code"]);
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }


       
        public List<EN.TrainMaster> GetTrainListforExport()
        {
            List<EN.TrainMaster> DL = new List<EN.TrainMaster>();
            DataTable DT = new DataTable();
            string Table = "TrainM";
            string Column = "TrainId,TrainNo";
            string Condition = "Process=''E''";
            string OrderBy = "TrainNo";

            DT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (DT != null)
            {
                foreach (DataRow row in DT.Rows)
                {
                    EN.TrainMaster TrainList = new EN.TrainMaster();
                    TrainList.TrainID = Convert.ToInt32(row["TrainId"]);
                    TrainList.TrainNO = Convert.ToString(row["TrainNo"]);

                   DL.Add(TrainList);
                }
            }
            return DL;
        }

        public List<EN.TrainExportEntities> GetTrainExpList()
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetTrainExportList();
            List<EN.TrainExportEntities> containerDL = new List<EN.TrainExportEntities>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TrainExportEntities containerList = new EN.TrainExportEntities();

                    containerList.Status = Convert.ToString(row["Status"]);
                    containerList.ContainerNo = Convert.ToString(row["Container No"]);
                    containerList.Size = Convert.ToInt16(row["Size"]);
                    containerList.ContainerType = Convert.ToString(row["Container Type"]);
                    containerList.LineName = Convert.ToString(row["Line"]);
                    containerList.IcdIndate = Convert.ToString(row["ICD Indate & Time"]);
                    containerList.DocumentReadyDate = Convert.ToString(row["Document Ready Date"]);
                    containerList.wagonNo = Convert.ToString(row["Wagon No"]);
                    containerList.Remarks = Convert.ToString(row["Remarks"]);
                    containerList.DwellDays = Convert.ToString(row["Dwell Days"]);
                    containerList.TEUS = Convert.ToInt16(row["TEUS"]);
                    containerList.entryid = Convert.ToInt32(row["entryid"]);
                    containerList.process = Convert.ToString(row["process"]);
                    containerList.selected = Convert.ToString(row["selected"]);
                    containerList.TrailerNo = Convert.ToString(row["TrailerNo"]);

                    containerDL.Add(containerList);
                }

            }
            return containerDL;
        }
        public int AddOperatorMasterDetails(EN.OperatorMaster operMaster)
        {

            int loginData = 0;
            loginData = trackerdatamanager.AddOperatorMasterDetails(operMaster.OperatorID, operMaster.OperatorName,true,operMaster.AddedBy);
            return loginData;
        }

        public bool GetExisitingOperatorName(string operatorName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetExisitingOperatorName(operatorName);
            bool isCHACode = false;

            if (codeDL.Rows.Count == 0)
            {
                isCHACode = true;
            }
            return isCHACode;
        }
        public List<string> GetOperatorNameForAutoComplete(string operatorName)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetOperatorNameForAutoComplete(operatorName);
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string name = Convert.ToString(row["Name"]);
                    isCHACode.Add(name);
                }

            }
            return isCHACode;
        }
        public List<EN.TrainMaster> GetTrainSummary(string process)
        {
            DataTable trainDL = new DataTable();
            trainDL = trackerdatamanager.GetTrainSummary(process);

            List<EN.TrainMaster> trainBL = new List<EN.TrainMaster>();


            if (trainDL.Rows.Count != 0)
            {

                foreach (DataRow row in trainDL.Rows)
                {
                    EN.TrainMaster train = new EN.TrainMaster();

                    train.TrainID = Convert.ToInt32(row["TrainID"]);

                    train.TrainNO = Convert.ToString(row["TrainNO"]);

                    train.PortFrom = Convert.ToString(row["PortFrom"]);

                    train.PortTO = Convert.ToString(row["PortTo"]);

                    train.OperatorID = Convert.ToInt32(row["Operator"]);

                    train.Operator = Convert.ToString(row["OperatorName"]);
                    train.IsImportORExportSelected = Convert.ToString(row["process"]);

                    trainBL.Add(train);

                }


            }

            return trainBL;

        }



        public EN.TrainMaster EditTrainSummary(int trainID)
        {
            DataTable trainDL = new DataTable();
            trainDL = trackerdatamanager.EditTrainSummary(trainID);
            EN.TrainMaster trainBL = new EN.TrainMaster();

            if (trainDL.Rows.Count != 0)
            {
                foreach (DataRow row in trainDL.Rows)
                {
                    EN.TrainMaster train = new EN.TrainMaster();
                    trainBL.TrainID = Convert.ToInt32(row["TrainID"]);
                    trainBL.TrainNO = Convert.ToString(row["TrainNO"]);
                    trainBL.PortFrom = Convert.ToString(row["PortFrom"]);
                    trainBL.PortTO = Convert.ToString(row["PortTo"]);
                    trainBL.OperatorID = Convert.ToInt32(row["Operator"]);
                    trainBL.Operator = Convert.ToString(row["OperatorName"]);  
                }

            }
            return trainBL;
        }
        public int UpdateTrainMasterDetails(EN.TrainMaster trainMaster)
        {

            int loginData = 0;
            loginData = trackerdatamanager.UpdateTrainMasterDetails(trainMaster.TrainID, trainMaster.TrainNO, trainMaster.PortFrom, trainMaster.PortTO, trainMaster.OperatorID.ToString(), trainMaster.Operator, trainMaster.ModifiedBy);
            return loginData;
        }

        //public List<string> GetContactPersonForAutoComplete(string cpName)
        //{
        //    DataTable codeDL = new DataTable();
        //    codeDL = trackerdatamanager.GetShipperContactPersonForAutoComplete(cpName);
        //    List<string> isCHACode = new List<string>();

        //    if (codeDL.Rows.Count != 0)
        //    {
        //        foreach (DataRow row in codeDL.Rows)
        //        {
        //            string name = Convert.ToString(row["ContactPerson"]);
        //            isCHACode.Add(name);
        //        }

        //    }
        //    return isCHACode;
        //}
        //public List<string> GetDesignationForAutoComplete(string cpName)
        //{
        //    DataTable codeDL = new DataTable();
        //    codeDL = trackerdatamanager.GetShipperDesignationForAutoComplete(cpName);
        //    List<string> isCHACode = new List<string>();

        //    if (codeDL.Rows.Count != 0)
        //    {
        //        foreach (DataRow row in codeDL.Rows)
        //        {
        //            string name = Convert.ToString(row["Designation"]);
        //            isCHACode.Add(name);
        //        }

        //    }
        //    return isCHACode;
        //}

        ////////added by durga/////////// 


        public List<EN.TrainMaster> getTrainList()
        {
            DataTable trainDL = new DataTable();
            trainDL = trackerdatamanager.GetTrainForUpdateDeparture();
            List<EN.TrainMaster> trainBL = new List<EN.TrainMaster>();

            if (trainDL.Rows.Count != 0)
            {
                foreach (DataRow row in trainDL.Rows)
                {
                    EN.TrainMaster train = new EN.TrainMaster();
                    
                    train.TrainNO = Convert.ToString(row["TrainNO"]);
                    train.TEUS = Convert.ToInt32(row["TEUS"]);
                    train.DepartureDate = Convert.ToString(row["DepartureDate"]);
                    train.RemovalDate = Convert.ToString(row["RemovalDate"]);
                    train.PlacementDate = Convert.ToString(row["PlacementDate"]);
                    train.PortLineNo = Convert.ToString(row["PortLineNo"]);
                    train.PortName = Convert.ToString(row["PortName"]);
                   
                    trainBL.Add(train);
                }

            }
            return trainBL;
        }

        public string UpdateTrainDeparture(DataTable BLList, Int32 userId)
           {
               string message = "";


               Dictionary<object, object> lstparam = new Dictionary<object, object>();
               lstparam.Add("userId", userId);

               TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

               int i = db.UpdateTrainDeparture("USP_UpdateTrainDeparture", lstparam, BLList);
               if (i != 0)
               {
                   message = "Records updated successfully";
               }
               else
               {
                   message = "Records not update, please try again";
               }

               return message;
           }

        public List<EN.TrainExportEntities> getLineWiseContainer(string ShiplineID)
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.getLineWiseContainer(ShiplineID);
            List<EN.TrainExportEntities> containerDL = new List<EN.TrainExportEntities>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TrainExportEntities containerList = new EN.TrainExportEntities();

                    containerList.Status = Convert.ToString(row["Status"]);
                    containerList.ContainerNo = Convert.ToString(row["Container No"]);
                    containerList.Size = Convert.ToInt16(row["Size"]);
                    containerList.ContainerType = Convert.ToString(row["Container Type"]);
                    containerList.LineName = Convert.ToString(row["Line"]);
                    containerList.IcdIndate = Convert.ToString(row["ICD Indate & Time"]);
                    containerList.DocumentReadyDate = Convert.ToString(row["Document Ready Date"]);
                    containerList.PrePlanDate = Convert.ToString(row["Pre Plan Date"]);
                    containerList.Remarks = Convert.ToString(row["Remarks"]);
                    containerList.DwellDays = Convert.ToString(row["Dwell Days"]);
                    containerList.entryid = Convert.ToInt32(row["entryID"]);
                    containerList.selected = Convert.ToString(row["selected"]);
                    containerDL.Add(containerList);
                }

            }
            return containerDL;
        }

        public string SaveContainerPrePlannerList(DataTable dt,int userid)
        {
            string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userid);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            int i = db.AddTypeTableData("USP_AddContainerPrePlanned", lstparam, dt, "PT_ContainerPrePlanned", "@PT_ContainerPrePlanned");
           // int i = db.UpdateTrainDeparture("USP_AddContainerPrePlanned", lstparam, BLList);
            if (i != 0)
            {
                message = "Records updated successfully";
            }
            else
            {
                message = "Records not update, please try again";
            }
            return message;
        
        }
        public string SaveExportTrainPlanned(DataTable dt, int userid, int PortID, int TrainID)
        {
            string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userid);
            lstparam.Add("PortID", PortID);
            lstparam.Add("TrainID", TrainID);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            int i = db.AddTypeTableData("USP_AddContainerExpPlanned", lstparam, dt, "PT_ContainerExpPlanned", "@PT_ContainerExpPlanned");
            // int i = db.UpdateTrainDeparture("USP_AddContainerPrePlanned", lstparam, BLList);
            if (i != 0)
            {
                message = "Records updated successfully";
            }
            else
            {
                message = "Records not update, please try again";
            }
            return message;

        }

            ///////////End by durga/////////

        // Codes by Arti


        public List<EN.ContainerDetails> GetContainerListforTrainNo(string TrainNo)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetContainerListforTrainNo(TrainNo);
            List<EN.ContainerDetails> isCHACode = new List<EN.ContainerDetails>();

            if (codeDL.Rows.Count != 0)
            {
                int Count = 1;
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.ContainerDetails oper = new EN.ContainerDetails();
                    oper.SRNo = Convert.ToInt32(Count++);
                    oper.ContainerNO = Convert.ToString(row["Container No"]);
                    oper.Size = Convert.ToString(row["Size"]);
                    oper.JOStatus = Convert.ToString(row["Jo Status"]);
                    oper.Remarks = Convert.ToString(row["Remarks"]);
                    oper.OutBy = Convert.ToString(row["Out By"]);
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }


        public List<EN.TrainMaster> GetTrainStatusSheetList()
        {
            DataTable trainDL = new DataTable();
            trainDL = trackerdatamanager.GetTrainStatusSheetList();
            List<EN.TrainMaster> trainBL = new List<EN.TrainMaster>();

            if (trainDL.Rows.Count != 0)
            {
                int Count = 1;
                foreach (DataRow row in trainDL.Rows)
                {
                    EN.TrainMaster train = new EN.TrainMaster();

                    train.TrainNO = Convert.ToString(row["TrainNO"]);
                    train.TEUS = Convert.ToInt32(row["TEUS"]);
                    train.DepartureDate = Convert.ToString(row["DepartureDate"]);
                    train.RemovalDate = Convert.ToString(row["RemovalDate"]);
                    train.PlacementDate = Convert.ToString(row["PlacementDate"]);
                    train.PortLineNo = Convert.ToString(row["PortLineNo"]);
                    train.PortName = Convert.ToString(row["PortName"]);
                    train.InTransitSince = Convert.ToString(row["InTransitSince"]);
                    train.SRNo = Convert.ToInt32(Count++);

                    trainBL.Add(train);
                }

            }
            return trainBL;
        }

        public List<EN.TrainMaster> GetTrainStatusSheetCompletedList()
        {
            DataTable trainDL = new DataTable();
            trainDL = trackerdatamanager.GetTrainStatusSheetCompletedList();
            List<EN.TrainMaster> trainBL = new List<EN.TrainMaster>();

            if (trainDL.Rows.Count != 0)
            {
                int Count = 1;
                foreach (DataRow row in trainDL.Rows)
                {
                    EN.TrainMaster train = new EN.TrainMaster();

                    train.TrainNO = Convert.ToString(row["TrainNO"]);
                    train.TEUS = Convert.ToInt32(row["TEUS"]);
                    train.DepartureDate = Convert.ToString(row["DepartureDate"]);
                    train.RemovalDate = Convert.ToString(row["RemovalDate"]);
                    train.PlacementDate = Convert.ToString(row["PlacementDate"]);
                    train.PortLineNo = Convert.ToString(row["PortLineNo"]);
                    train.PortName = Convert.ToString(row["PortName"]);
                    train.TrainArrivedDate = Convert.ToString(row["TrainArrivedDate"]);
                    train.SRNo = Convert.ToInt32(Count++);

                    trainBL.Add(train);
                }

            }
            return trainBL;
        }
        // Codes by Arti
        public List<EN.LocationMaster> getLocation()
        {
            List<EN.LocationMaster> locationDL = new List<EN.LocationMaster>();
            DataTable dt = new DataTable();
            string Table = "Ext_Location_M";
            string Column = "LocationID,Location";
            string Condition = "";
            string OrderBy = "Location";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationMaster locationList = new EN.LocationMaster();
                    locationList.LocationID = Convert.ToInt32(row["LocationID"]);
                    locationList.Location = Convert.ToString(row["Location"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<EN.Customer> getCustomer()
        {
            List<EN.Customer> customerDL = new List<EN.Customer>();
            DataTable dt = new DataTable();
            string Table = "customer";
            string Column = "AGID,AGName";
            string Condition = "";
            string OrderBy = "AGName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.Customer locationList = new EN.Customer();
                    locationList.AGID = Convert.ToInt32(row["AGID"]);
                    locationList.AGName = Convert.ToString(row["AGName"]);
                    customerDL.Add(locationList);
                }
            }
            return customerDL;
        }

        public List<EN.Shippers> getShipper()
        {
            List<EN.Shippers> ShippersDL = new List<EN.Shippers>();
            DataTable ShippersDT = new DataTable();
            string Table = "exp_shippers";
            string Column = "shipperID,shippername";
            string Condition = "IsActive=1";
            string OrderBy = "shippername";

            ShippersDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
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


        public List<EN.OwnerStatus> getOwnerStatus()
        {
            List<EN.OwnerStatus> locationDL = new List<EN.OwnerStatus>();
            DataTable dt = new DataTable();
            string Table = "ownerstatus";
            string Column = "OwnerID,OwnerStatus";
            string Condition = "";
            string OrderBy = "OwnerStatus";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.OwnerStatus locationList = new EN.OwnerStatus();
                    locationList.OwnerID = Convert.ToString(row["OwnerID"]);
                    locationList.OwnerName = Convert.ToString(row["OwnerStatus"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }


        public string SaveTrainMaster(string TrainNo, string NoCno, string OwnerStatus, string RankOperator, string FromLocation, string Tolocation, string OriginDepartureDate, string ETA
            , string ETD, string ATA, string ATD, string LoadingStartDate, string LoadingCompleteDate, string Unloadingstartdate, string unloadingcompletedate,
            string LIneNoalloated, string Trainstatus, string StatusUpdatedDate, string Remarks, int UserID, string PartyId)
        {

            string message = "";
            DataTable ApproveDL = new DataTable();
            ApproveDL = trackerdatamanager.SaveTrainDetails(TrainNo, NoCno, OwnerStatus, RankOperator, FromLocation, Tolocation, OriginDepartureDate, ETA, ETD, ATA
                , ATD, LoadingStartDate, LoadingCompleteDate, Unloadingstartdate, unloadingcompletedate, LIneNoalloated, Trainstatus, StatusUpdatedDate, Remarks, UserID,PartyId);

            message = "Record saved successfully!";
            return message;

        }

        public List<EN.NOCUpdation> GetNoNOCForAutoComplete(string NOCNO)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetNOCForAutoComplete(NOCNO);
            List<EN.NOCUpdation> isCHACode = new List<EN.NOCUpdation>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.NOCUpdation customer = new EN.NOCUpdation();
                    customer.NOCRefNo = Convert.ToString(row["NOCRefNo"]);
                    customer.NOCNo = Convert.ToInt32(row["NOCNo"]);
                    customer.OrginStationId = Convert.ToString(row["OrginStationId"]);
                    customer.SLid = Convert.ToString(row["SLid"]);
                    customer.CommodityId = Convert.ToString(row["CommodityId"]);
                    customer.PlannedDate = Convert.ToString(row["PlannedDate"]);
                    isCHACode.Add(customer);
                }

            }
            return isCHACode;
        }
    }
}
