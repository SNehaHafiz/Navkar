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

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.BLDataManager
{
    class GenerateInvoice
    {
        //DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        //public string AddJobOrderData(EN.JobOrderMEntities viewModel, List<EN.JobOrderDEntities> JobDetails, int UserId, int igmid)
        //{
        //    string message = "";
        //    try
        //    {

        //        Int64 JONO = 0;
        //        DataTable JobOrderDT = new DataTable();
        //        //  JobOrderDT = TrackerManager.AddJobOrderData(JobOrderData.AgentID, JobOrderData.SLID, JobOrderData.shipperid, JobOrderData.Importerid, JobOrderData.Chaid, JobOrderData.ViaNo, JobOrderData.VesselID, JobOrderData.PortID, JobOrderData.berthingdate, JobOrderData.Haulage_Type_Id, JobOrderData.File_Status_Id, JobOrderData.Transport_Type_Id, JobOrderData.POL_ID, JobOrderData.Sales_Person_Id,JobOrderData.BLNumber,JobOrderData.BLReceivedDate, UserId);
        //        JobOrderDT = TrackerManager.AddInvoiceDataIntoTable(viewModel.JONo, viewModel.AgentID, viewModel.SLID, viewModel.shipperid, viewModel.Importerid, viewModel.Chaid, viewModel.ViaNo, viewModel.VesselID, viewModel.PortID, viewModel.berthingdate, viewModel.Haulage_Type_Id, viewModel.File_Status_Id, viewModel.Transport_Type_Id, viewModel.POL_ID, viewModel.Sales_Person_Id, viewModel.BLNumber, viewModel.BLReceivedDate, UserId, viewModel.HouseBLNumber, viewModel.KAMID, viewModel.JoRemarks, viewModel.JoTypeId, igmid, viewModel.IGMNo);

        //        if (JobOrderDT.Rows.Count > 0)
        //        {
        //            message = "Record Saved Successfully";
        //        }
        //        else
        //        {
        //            message = "Record not Saved Successfully";
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        //AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);

        //    }
        //    return message;
        //}
    }
}
