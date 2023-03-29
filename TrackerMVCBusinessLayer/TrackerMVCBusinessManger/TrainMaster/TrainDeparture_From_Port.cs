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
    public class TrainDeparture_From_Port
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();

        public List<EN.Port> GetPortList()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetPortForTrainDeparture();
            List<EN.Port> isCHACode = new List<EN.Port>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.Port oper = new EN.Port();
                    oper.PortID = Convert.ToInt32(row["PortID"]);
                    oper.PortName = Convert.ToString(row["PortName"]);
                    oper.PortCode = "port" + oper.PortID;
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }

        public List<EN.Port> GetPortExpList()
        {
            List<EN.Port> DL = new List<EN.Port>();

            DataTable DT = new DataTable();
            string Table = "Ports";
            string Column = "PortID,PortName";
            string Condition = "";
            string OrderBy = "PortName";

            DT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (DT.Rows.Count != 0)
            {
                foreach (DataRow row in DT.Rows)
                {
                    EN.Port oper = new EN.Port();
                    oper.PortID = Convert.ToInt32(row["PortID"]);
                    oper.PortName = Convert.ToString(row["PortName"]);
                    oper.PortCode = "port" + oper.PortID;
                    DL.Add(oper);
                }

            }
            return DL;
        }

        public List<EN.ContainerDetails> GetContainerForTrainDeparture(int portID, int userID)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetContainerForTrainDeparture(portID, userID);
            List<EN.ContainerDetails> isCHACode = new List<EN.ContainerDetails>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.ContainerDetails oper = new EN.ContainerDetails();
                    oper.JONO = Convert.ToInt32(row["jono"]);
                    oper.ContainerNO = Convert.ToString(row["ContainerNo"]);
                    oper.Size = Convert.ToString(row["Size"]);
                    oper.IsSelected = Convert.ToBoolean(row["IsContainerSelected"]);
                    oper.WagonNo = Convert.ToString(row["wagonno"]);
                    oper.Remarks = Convert.ToString(row["Remarks"]);
                    oper.TEUS = Convert.ToInt16(row["TEUS"]);

                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }
        public List<string> GetTrainNOForTrainDeparture()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetTrainNOForTrainDeparture();
            List<string> isCHACode = new List<string>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    string oper = Convert.ToString(row["TrainNo"]); 
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }

        public int UpdateSelectedContainer(int joNo,string containerNo, bool isChecked,int userID)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.UpdateSelectedContainer(joNo, containerNo, isChecked, userID);
            int totalSelectedCount = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    totalSelectedCount = Convert.ToInt32(row["TotalSelected"]);
                    
                }

            }
            return totalSelectedCount;
        }
        public int GetTotalCount(int userID)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetTotalCount(userID);
            int totalSelectedCount = 0;

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    totalSelectedCount = Convert.ToInt32(row["TotalSelected"]);
                    
                }

            }
            return totalSelectedCount;
        }
        public int UpdateWagonNoForSelectedContainer(string wagonNo, string containerNo,int userID)
        {
            int codeDL = 0;
            codeDL = trackerdatamanager.UpdateWagonNoForSelectedContainer(wagonNo, containerNo, userID);
            return codeDL;
        }
        public int UpdateTrainNoForSelectedContainer(string trainNo, int userID)
        {
            int codeDL = 0;
            codeDL = trackerdatamanager.UpdateTrainNoForSelectedContainer(trainNo, userID);
            return codeDL;
        }
        //by durga///////
        public int UpdateRemarksSelectedContainer(string Remarks, string containerNo, string jono, int userID)
        {
            int codeDL = 0;
            codeDL = trackerdatamanager.UpdateRemarksForSelectedContainer(Remarks, containerNo, jono, userID);
            return codeDL;
        }


        public void DeleteFromTempTrain_Container_Selection(int userID)
        {

            trackerdatamanager.DeleteFromTempTrain_Container_Selection(userID);
            
        }

       /////////end 


        // Codes By Arti /

public List<EN.ContainerDetails> GetContainerList(int portID)
{
DataTable codeDL = new DataTable();
codeDL = trackerdatamanager.GetContainerList(portID);
List<EN.ContainerDetails> isCHACode = new List<EN.ContainerDetails>();

if (codeDL.Rows.Count != 0)
{
foreach (DataRow row in codeDL.Rows)
{
EN.ContainerDetails oper = new EN.ContainerDetails();
oper.JONO = Convert.ToInt32(row["jono"]);
oper.ContainerNO = Convert.ToString(row["ContainerNo"]);
oper.Size = Convert.ToString(row["Size"]);

oper.WagonNo = Convert.ToString(row["wagonno"]);
oper.Remarks = Convert.ToString(row["Remarks"]);
oper.TEUS = Convert.ToInt16(row["TEUS"]);
oper.DischargeDate = Convert.ToString(row["DischargeDate"]);
oper.selected = Convert.ToString(row["selected"]);

isCHACode.Add(oper);
}

}
return isCHACode;
}


public EN.ContainerDetails SaveContainerList(DataTable Itemdata, int UserID)
{
    //string Message = "";
    TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

    Dictionary<object, object> parameterList = new Dictionary<object, object>();
    parameterList.Add("UserID", UserID);
    DataSet ds = new DataSet();

    ds = db.AddTypeRoadPlanningTableData("USP_TainPlanningInsertContainerList", parameterList, Itemdata, "PT_ContainerDetails", "@PT_ContainerDetails");


    //int i = db.SaveContainerList("USP_TainPlanningInsertContainerList", parameterList, Itemdata);

    // VendorDataDL = objgetMyREQDBManager.ApprovedStatusList(OrderID, companyid);

    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    EN.ContainerDetails ValidationList = new EN.ContainerDetails();

    dt = ds.Tables[0];
    dt1 = ds.Tables[1];
    if (dt.Rows.Count > 0)
    {

        ValidationList.validationMessage = Convert.ToInt32(dt.Rows[0][0]);
        ValidationList.containerList = Convert.ToString(dt1.Rows[0][0]);

    }
    // Message = "Record saved successfully";
    return ValidationList;


    // Message = "Record saved successfully";
    ///return Message;



}


public List<EN.ContainerDetails> FetchExistingContainerList(int portID,int trainid)
{
DataTable codeDL = new DataTable();
codeDL = trackerdatamanager.GetExistingContainerList(portID, trainid);
List<EN.ContainerDetails> isCHACode = new List<EN.ContainerDetails>();

if (codeDL.Rows.Count != 0)
{
foreach (DataRow row in codeDL.Rows)
{
EN.ContainerDetails oper = new EN.ContainerDetails();
//oper.JONO = Convert.ToInt32(row["jono"]);
oper.ContainerNO = Convert.ToString(row["ContainerNo"]);
oper.WagonNo = Convert.ToString(row["wagonno"]);
oper.Remarks = Convert.ToString(row["Remarks"]);
//oper.TEUS = Convert.ToInt16(row["TEUS"]);
//oper.DischargeDate = Convert.ToString(row["DischargeDate"]);

isCHACode.Add(oper);
}

}
return isCHACode;
}

public List<EN.TrainMaster> GetTrainList()
{
    DataTable codeDL = new DataTable();
    codeDL = trackerdatamanager.GetTrainNOForTrainDeparture();


    List<EN.TrainMaster> isCHACode = new List<EN.TrainMaster>();

    if (codeDL.Rows.Count != 0)
    {
        foreach (DataRow row in codeDL.Rows)
        {
            EN.TrainMaster oper = new EN.TrainMaster();
            oper.TrainID = Convert.ToInt32(row["TrainId"]);
            oper.TrainNO = Convert.ToString(row["TrainNo"]);
            isCHACode.Add(oper);
        }

    }
    return isCHACode;
}




// Codes By Arti /

        /////////By durga for road planning/////////////
            public List<EN.ContainerDetails> GetRoadContainerList(int portID)
            {
                DataTable dt = new DataTable();
                dt = trackerdatamanager.GetContainerRoadList(portID);
                List<EN.ContainerDetails> ContainerDL = new List<EN.ContainerDetails>();

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        EN.ContainerDetails DL = new EN.ContainerDetails();
                        DL.JONO = Convert.ToInt32(row["jono"]);
                        DL.ContainerNO = Convert.ToString(row["ContainerNo"]);
                        DL.Size = Convert.ToString(row["Size"]);

                       // oper.WagonNo = Convert.ToString(row["wagonno"]);
                        DL.Remarks = Convert.ToString(row["Remarks"]);
                        DL.TEUS = Convert.ToInt16(row["TEUS"]);
                        DL.DischargeDate = Convert.ToString(row["DischargeDate"]);
                        DL.selected = Convert.ToString(row["selected"]);

                        ContainerDL.Add(DL);
                    }

                }
                return ContainerDL;
            }
            public EN.ContainerDetails SaveRoadContainerList(DataTable Itemdata, int UserID)
            {
               // string Message = "";
                TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

                Dictionary<object, object> parameterList = new Dictionary<object, object>();
                parameterList.Add("UserID", UserID);
                DataSet ds = new DataSet();
                 ds = db.AddTypeRoadPlanningTableData("USP_RoadPlanningInsertContainerList", parameterList, Itemdata, "PT_RoadPlanning", "@PT_RoadPlanning");

                // VendorDataDL = objgetMyREQDBManager.ApprovedStatusList(OrderID, companyid);

                 DataTable dt = new DataTable();
                 DataTable dt1 = new DataTable();
                 EN.ContainerDetails ValidationList = new EN.ContainerDetails();

                 dt = ds.Tables[0];
                 dt1 = ds.Tables[1];
                 if (dt.Rows.Count > 0)
                 {

                     ValidationList.validationMessage = Convert.ToInt32(dt.Rows[0][0]);
                     ValidationList.containerList = Convert.ToString(dt1.Rows[0][0]);

                 }
               // Message = "Record saved successfully";
                return ValidationList;



            }

        public List<EN.ContainerDetails> getTrailerRoadList()
        {
            DataTable trainDL = new DataTable();
            trainDL = trackerdatamanager.GetRoadTrailerList();
            List<EN.ContainerDetails> ContainerBL = new List<EN.ContainerDetails>();

            if (trainDL.Rows.Count != 0)
            {
                foreach (DataRow row in trainDL.Rows)
                {
                    EN.ContainerDetails ContainerDL = new EN.ContainerDetails();

                    ContainerDL.TrailerNo = Convert.ToString(row["Trailer No"]);
                    ContainerDL.ContainerNO = Convert.ToString(row["Container No"]);
                    ContainerDL.portName = Convert.ToString(row["PortName"]);
                    ContainerDL.portOutDate = Convert.ToString(row["Port Out Date"]);
                    ContainerDL.Size = Convert.ToString(row["Size"]);
                    ContainerDL.Remarks = Convert.ToString(row["Remarks"]);


                    ContainerBL.Add(ContainerDL);
                }

            }
            return ContainerBL;
        }
        public List<EN.TrainExportEntities> GetRoadContainerExportList()
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetRoadExportList();
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
                    containerList.TrailerNo = Convert.ToString(row["Trailer No"]);
                    containerList.Remarks = Convert.ToString(row["Remarks"]);
                    containerList.DwellDays = Convert.ToString(row["Dwell Days"]);
                    containerList.TEUS = Convert.ToInt16(row["TEUS"]);
                    containerList.entryid = Convert.ToInt32(row["entryid"]);
                    containerList.process = Convert.ToString(row["process"]);
                    containerList.selected = Convert.ToString(row["selected"]);
                    containerList.trainId = Convert.ToInt16(row["TrainNo"]);

                    containerDL.Add(containerList);
                }

            }
            return containerDL;

        }


        public string SaveExportRoadPlanned(DataTable dt, int userid, int PortID, string TrailerNo)
        {
            string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userid);
            lstparam.Add("PortID", PortID);
            lstparam.Add("TrailerNo", TrailerNo);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            int i = db.AddTypeTableData("USP_AddRoadContainerExpPlanned", lstparam, dt, "PT_ContainerRoadExpPlanned", "@PT_ContainerRoadExpPlanned");
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

////////End by durga for road planning ////////////
/* Codes By Prashant*/

public List<EN.JobOrderMEntities> GetSMPTDetails(int portID)
{
    DataTable SMPTDL = new DataTable();
    SMPTDL = trackerdatamanager.GetSMPTList(portID);
    List<EN.JobOrderMEntities> isSMPT = new List<EN.JobOrderMEntities>();

    if (SMPTDL.Rows.Count != 0)
    {
        foreach (DataRow row in SMPTDL.Rows)
        {
            EN.JobOrderMEntities SMPT = new EN.JobOrderMEntities();

            SMPT.JONo = Convert.ToInt16(row["JONo"]);
            SMPT.ContainerNo = Convert.ToString(row["ContainerNo"]);
            SMPT.Size = Convert.ToInt16(row["Size"]);
            SMPT.ContainerType = Convert.ToString(row["ContainerType"]);
            SMPT.BLNumber = Convert.ToString(row["BLNumber"]);
            SMPT.VesselName = Convert.ToString(row["VesselName"]);


            isSMPT.Add(SMPT);
        }

    }
    return isSMPT;
}

        /* End Code By Prashant*/


public string UpdateSMTPContainerList(DataTable Itemdata, int UserID, string SMTPRCVDDate)
{
    string Message = "";
    TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

    Dictionary<object, object> parameterList = new Dictionary<object, object>();
    parameterList.Add("UserID", UserID);
    parameterList.Add("SMTPRCVDDate", Convert.ToDateTime(SMTPRCVDDate).ToString("yyyy-MM-dd HH:mm:ss")); 

    DataSet ds = new DataSet();
    int i=0;
    i = db.AddTypeTableData("USP_UpdateSMTPContainerList", parameterList, Itemdata, "PT_SMTPContainerDate", "@PT_SMTPContainerDate");

    // VendorDataDL = objgetMyREQDBManager.ApprovedStatusList(OrderID, companyid);

     //DataTable dt = new DataTable();
     //DataTable dt1 = new DataTable();
     //EN.ContainerDetails ValidationList = new EN.ContainerDetails();

     //dt = ds.Tables[0];
     //dt1 = ds.Tables[1];
     if (i > 0)
     {

        Message="Record saved successfully";

     }
     else{
     Message="Record not saved please try again!";

     }
   // Message = "Record saved successfully";
    return Message;



}

    }
}
