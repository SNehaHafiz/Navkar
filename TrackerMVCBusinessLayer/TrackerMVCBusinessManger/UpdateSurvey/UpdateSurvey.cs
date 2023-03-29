using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using HC = TrackerMVCDataLayer.Helper;
using TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.UpdateSurvey
{
    public class UpdateSurvey
    {
        DB.TrackerMVCDBManager transportdatamanager = new DB.TrackerMVCDBManager();

        public string SaveRemarks(string containerno, string remarks, int userId)
        {
            string message;
            int i = 0;

            i = transportdatamanager.SaveRemarks(containerno, remarks, userId);
            if (i == 0)
            {
                message = "Record not saved please try again!";

              
            }
            else {
                message = "Record saved successfully";
            
            }

            return message;
        }


        public List<EN.JobOrderDEntities> getContainerList(string containerno)
        {
            string message;
            DataTable dt = new DataTable();
            List<EN.JobOrderDEntities> ContainerDL = new List<EN.JobOrderDEntities>();
            dt = transportdatamanager.GetContainerRemarksList(containerno);
            if (dt.Rows.Count> 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.JobOrderDEntities ContainerList = new EN.JobOrderDEntities();
                    ContainerList.ContainerNo = Convert.ToString(row["containerno"]);
                    ContainerList.Remarks = Convert.ToString(row["Remarks"]);
                    ContainerList.JONo = Convert.ToInt32(row["jono"]);
                    ContainerList.surveyID = Convert.ToInt64(row["surveyID"]);

                    ContainerDL.Add(ContainerList);
                }
            }

            return ContainerDL;
        }



        public string DeleteRemarks(string containerno, string remarks, long jono, int userId, long surveyID)
        {
            string message;
            int i = 0;

            i = transportdatamanager.DeleteRemarks(containerno, remarks, jono, userId, surveyID);
            if (i == 0)
            {
                message = "Record not cancelled please try again!";

              
            }
            else {
                message = "Record cancelled successfully";
            
            }

            return message;
        }

        
              public List<EN.CodecoEntities> getActivityList()
        {
            string message;
            DataTable dt = new DataTable();
            List<EN.CodecoEntities> ActivityDL = new List<EN.CodecoEntities>();
            dt = transportdatamanager.getActivityList();
            if (dt.Rows.Count> 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CodecoEntities CodecoList = new EN.CodecoEntities();
                    CodecoList.ID = Convert.ToInt32(row["ID"]);
                    CodecoList.Activity = Convert.ToString(row["Activity"]);

                    ActivityDL.Add(CodecoList);
                }
            }

            return ActivityDL;
        }


              public string SaveActivity(DataTable Invoicedata ,int userId)
        {
            string message;
            string strSql = "";
            HC.DBOperations db = new HC.DBOperations();
            int i = 0;
            for (int k = 0; k < Invoicedata.Rows.Count; k++)
            {
                 
                    i = db.sub_ExecuteNonQuery("USP_REAPPLICABLECODECO '" + Invoicedata.Rows[k].Field<string>("ContainerNo") + "','" + Invoicedata.Rows[k].Field<string>("Activity") +  "'");
                    
                 
            }
            ///i = transportdatamanager.SaveActivity(containerno, Activity, userId);
            if (i == 0)
            {
                message = "Record not saved please try again!";

              
            }
            else {
                message = "Record saved successfully";
            
            }

            return message;
        }

        // Code By Prashant
        public List<EN.DocList> GetDocList()
        {
            List<EN.DocList> objLRentities = new List<EN.DocList>();
            DataTable ds = new DataTable();
            ds = transportdatamanager.DocDropDownListForUpload();

            if (ds.Rows.Count != 0)
            {
                foreach (DataRow row in ds.Rows)
                {
                    EN.DocList LRList = new EN.DocList();

                    LRList.DocID = Convert.ToInt32(row["DocID"]);
                    LRList.DocName = Convert.ToString(row["DocumentType"]);

                    objLRentities.Add(LRList);
                }
            }


            return objLRentities;
        }

        //public List<EN.ContainerDocsEntities> GetContainerDocsList(string containerno)
        //{
        //    List<EN.ContainerDocsEntities> objLRentities = new List<EN.ContainerDocsEntities>();
        //    DataTable ds = new DataTable();
        //    ds = transportdatamanager.GetContainerDocsDetails(containerno);
        //    int count = 1;
        //    if (ds.Rows.Count != 0)
        //    {
        //        foreach (DataRow row in ds.Rows)
        //        {
        //            EN.ContainerDocsEntities LRList = new EN.ContainerDocsEntities();
        //            LRList.autoid = Convert.ToInt16(row["autoid"]);
        //            LRList.DocID = Convert.ToString(row["DocID"]);
        //            LRList.Containerno = Convert.ToString(row["ContainerNo"]);
        //            LRList.process = Convert.ToString(row["process"]);
        //            LRList.name = Convert.ToString(row["name"]);
        //            LRList.jono = Convert.ToString(row["Jono"]);
        //            LRList.documenttype = Convert.ToString(row["documenttype"]);
        //            LRList.srno = Convert.ToString(count++);

        //            objLRentities.Add(LRList);
        //        }
        //    }


        //    return objLRentities;
        //}


        public EN.ContainerDocsEntities GetCountainerNodocs(string autoid)
        {
            EN.ContainerDocsEntities AttachmentList = new EN.ContainerDocsEntities();
            DataTable AttachmentDT = new DataTable();
            int i = 0;
            AttachmentDT = transportdatamanager.GetDownloadAttachment(autoid);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {
                    AttachmentList.DocID = Convert.ToString(row["DocID"]);
                    AttachmentList.documenttype = Convert.ToString(row["documenttype"]);
                    AttachmentList.FilePath = Convert.ToString(row["FilePath"]);

                }
            }
            return AttachmentList;
        }
        public List<EN.UpdateActivirtEnt> UpdateDetails(DataTable DriverPaymentDT)
        {
            //EN.DriverPaymentReco objdriverpaymententities = new EN.DriverPaymentReco();
            List<EN.UpdateActivirtEnt> VoucherPaymentList = new List<EN.UpdateActivirtEnt>();
            if (DriverPaymentDT != null)
            {
                int count = 1;
                foreach (DataRow row in DriverPaymentDT.Rows)
                {
                    EN.UpdateActivirtEnt DPDTList = new EN.UpdateActivirtEnt();
                     
                    DPDTList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    DPDTList.Activity = Convert.ToString(row["Activity"]);
               

                    VoucherPaymentList.Add(DPDTList);

                }
            }
            return VoucherPaymentList;
        }
    }
}
