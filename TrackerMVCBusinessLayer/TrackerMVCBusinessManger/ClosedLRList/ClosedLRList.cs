using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ClosedLRList
{
    public  class ClosedLRList
    {
        DB.TrackerMVCDBManager transportdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.ClosedLRList> getClosedLRList(string FromDate, string ToDate, string Activity, string SearchCerteria, string LrNo)
        {
            string message;
            int i = 0;
            DataTable dt = new DataTable();
            List<EN.ClosedLRList> VehicleDL = new List<EN.ClosedLRList>();
            dt = transportdatamanager.GetClosedLRList(FromDate, ToDate, Activity, SearchCerteria, LrNo);
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.ClosedLRList VehicleList = new EN.ClosedLRList();
                    i++;
                    VehicleList.SrNo = i;
                    VehicleList.LRNo = Convert.ToString(row["LR No"]);
                    VehicleList.LRDate = Convert.ToString(row["LR Date"]);
                    VehicleList.ContainerNo = Convert.ToString(row["Container No"]);
                    VehicleList.Size = Convert.ToString(row["Size"]);
                    VehicleList.Type = Convert.ToString(row["Type"]);
                    VehicleList.Activity = Convert.ToString(row["Activity"]);
                    VehicleList.Vehicleno = Convert.ToString(row["Vehicle no"]);
                    VehicleList.Customer = Convert.ToString(row["Customer"]);
                    VehicleList.F_location = Convert.ToString(row["From Location"]);
                    VehicleList.T_Location = Convert.ToString(row["To Location"]);
                    VehicleList.Weight = Convert.ToString(row["Weight"]);
                    VehicleList.BOENo = Convert.ToString(row["BOE No"]);
                    VehicleList.Stuffloc = Convert.ToString(row["Stuffing Location"]);

                    VehicleList.LRdateOpen = Convert.ToString(row["LR Open On"]);
                    VehicleList.LRdateClose = Convert.ToString(row["LR Closed On"]);
                    VehicleList.DwellHours = Convert.ToString(row["Dwell Hours"]);
                    VehicleList.FactoryReportingTime = Convert.ToString(row["Factory Reporting Time"]);
                    VehicleList.FactoryInTime = Convert.ToString(row["Factory In Time"]);
                    VehicleList.FactoryOutTime = Convert.ToString(row["Factory Out Date & Time"]);
                    VehicleList.DwellHoursFactoryInOut = Convert.ToString(row["Dwell Hours Factory In & Out"]);
                    VehicleList.PreparedBy = Convert.ToString(row["Prepared By"]);
                    VehicleList.LRClosedBy = Convert.ToString(row["LR Closed By"]);
                  
                    VehicleDL.Add(VehicleList);

                }
            }

            return VehicleDL;
        }
        public List<EN.ActivityMaster> getActivityMaster()
        {
            List<EN.ActivityMaster> passingDL = new List<EN.ActivityMaster>();
            DataTable dt = new DataTable();
            dt = transportdatamanager.getActivityMaster();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ActivityMaster PassingList = new EN.ActivityMaster();
                    PassingList.TYPEID = Convert.ToString(row["AutoID"]);
                    PassingList.Activity = Convert.ToString(row["Activity"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        public List<EN.ClosedLRList> GetLRAttachment(string LRNo)
        {
            List<EN.ClosedLRList> AttachmentList = new List<EN.ClosedLRList>();
            DataTable AttachmentDT = new DataTable();
            int i=0;
            AttachmentDT = transportdatamanager.GetLRDocumentList(LRNo);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {
                    EN.ClosedLRList AttachmentDataList = new EN.ClosedLRList();
                    i++;
                    AttachmentDataList.SrNo = i;
                    AttachmentDataList.DocID = Convert.ToInt32(row["DocID"]);
                    AttachmentDataList.DocumentType = Convert.ToString(row["DocumentType"]);
                    AttachmentDataList.FilePath = Convert.ToString(row["FilePath"]);
                    

                    AttachmentList.Add(AttachmentDataList);
                }
            }
            return AttachmentList;
        }

        public EN.ClosedLRList getAttachment(int DocID, string LRNo)
        {
            EN.ClosedLRList Docdetails = new EN.ClosedLRList();
            DataTable CompanyDL = new DataTable();
            CompanyDL = transportdatamanager.GetLRDocumentList1(DocID,LRNo);
            if (CompanyDL != null)
            {
                foreach (DataRow row in CompanyDL.Rows)
                {

                    Docdetails.DocID = Convert.ToInt32(row["DocID"]);
                    Docdetails.DocumentType = Convert.ToString(row["DocumentType"]);
                    Docdetails.FilePath = Convert.ToString(row["FilePath"]);

                }
            }
            return Docdetails;
        }

        // Code by Prashant
        public List<EN.ClosedLRList> getPendingClosedLRList(string FromDate, string ToDate, string Activity)
        {
            string message;
            int i = 0;
            DataTable dt = new DataTable();
            List<EN.ClosedLRList> VehicleDL = new List<EN.ClosedLRList>();
            dt = transportdatamanager.GetPendingClosedLRList(FromDate, ToDate, Activity);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.ClosedLRList VehicleList = new EN.ClosedLRList();
                    i++;
                    VehicleList.SrNo = i;
                    VehicleList.LRNo = Convert.ToString(row["LR No"]);
                    VehicleList.LRDate = Convert.ToString(row["LR Date"]);
                    VehicleList.ContainerNo = Convert.ToString(row["Container No"]);
                    VehicleList.Size = Convert.ToString(row["Size"]);
                    VehicleList.Type = Convert.ToString(row["Type"]);
                    VehicleList.Activity = Convert.ToString(row["Activity"]);
                    VehicleList.Vehicleno = Convert.ToString(row["Vehicle no"]);
                    VehicleList.Customer = Convert.ToString(row["Customer"]);
                    VehicleList.F_location = Convert.ToString(row["From Location"]);
                    VehicleList.T_Location = Convert.ToString(row["To Location"]);
                    VehicleList.Weight = Convert.ToString(row["Weight"]);
                    VehicleList.BOENo = Convert.ToString(row["BOE No"]);
                    VehicleList.Stuffloc = Convert.ToString(row["Stuffing Location"]);

                    VehicleList.LRdateOpen = Convert.ToString(row["LR Open On"]);
                    VehicleList.LRdateClose = Convert.ToString(row["LR Closed On"]);
                    VehicleList.DwellHours = Convert.ToString(row["Dwell Hours"]);
                    VehicleList.FactoryReportingTime = Convert.ToString(row["Factory Reporting Time"]);
                    VehicleList.FactoryInTime = Convert.ToString(row["Factory In Time"]);
                    VehicleList.FactoryOutTime = Convert.ToString(row["Factory Out Date & Time"]);
                    VehicleList.DwellHoursFactoryInOut = Convert.ToString(row["Dwell Hours Factory In & Out"]);
                    VehicleList.PreparedBy = Convert.ToString(row["Prepared By"]);
                    VehicleList.LRClosedBy = Convert.ToString(row["LR Closed By"]);

                    VehicleDL.Add(VehicleList);

                }
            }

            return VehicleDL;
        }
        //code end by Prashant
    }
}
