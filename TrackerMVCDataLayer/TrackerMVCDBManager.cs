using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using HC = TrackerMVCDataLayer.Helper;
using DB = TrackerMVCDBConnector;
using EN = TrackerMVCEntities.BusinessEntities;
using System.Data.SqlClient;
using TrackerMVCEntities.BusinessEntities;

namespace TrackerMVCDataLayer
{
    public class TrackerMVCDBManager
    {
        private string sqlCommandstring;

        public DataTable getLogin(string UserID, string Pass)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("GET_UserLoginDetails_WEB  '" + UserID + "','" + Pass + "'");
            return LoginDT;
        }

        public DataTable CheckalreadyimportIn(long jono)
        {
            DataTable CheckMailSave = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CheckMailSave = db.sub_GetDatatable("USP_Check_Jo_arrived_In '" + jono + "'");
            return CheckMailSave;
        }

       

        public DataTable getCount(string UserID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_Notif_Count  '" + UserID +  "'");
            return LoginDT;
        }

        public DataTable GetCustomerArrival(int monthNO)
        {

            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_MIS_IMPORT_GATE_IN_TUES_REPORT  '" + monthNO + "','" + DateTime.Now.Year + "'");
            return DT;
        }
        public DataTable GetPipeLineArrival(int monthNO)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_MIS_IMPORT_PIPELINE_TUES_REPORT " + monthNO + "," + DateTime.Now.Year);
            return DT;
        }
        public DataTable getMenuList(int userid)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //LoginDT = db.sub_GetDatatable("USP_Menu_Details_User  " + userid + "");
            LoginDT = db.sub_GetDatatable("USP_Menu_Details_User_New  " + userid + ""); 
            return LoginDT;
        }
        public DataTable VendorFill()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("usp_Vendor_Fill  ");
            return LoginDT;
        }
        public DataTable GetDropdownList(string Table, string Column, string Condition, string OrderBy)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("get_sp_table  '" + Table + "','" + Column + "','" + Condition + "','" + OrderBy + "'");
            return DT;
        }
        public DataTable GetWorkordercategory(string category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetWork_Order_Details_Drop_Down '" + category + "'");
            return dt;
        }
        public DataTable GetInvoicenoforverification(string Invoiceno, string workyear)
        {
            DataTable Invoiceno1 = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Invoiceno1 = db.sub_GetDatatable("USP_getbillnoforverification  '" + Invoiceno + "','" + workyear + "'");
            return Invoiceno1;
        }

        public DataTable GetAssessReciept(string CHEQUENO)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_ChequeDets_Riceipt '" + CHEQUENO + "'");

            return dt;
        }


        //  public DataTable AddJobOrderData(int AgentID, int SLID, long shipperid, long Importerid, long Chaid, String ViaNo, int VesselID, int PortID, string berthingdate, int Haulage_Type_Id, int File_Status_Id, int Transport_Type_Id, int POL_ID, int Sales_Person_Id,string BLNumber,DateTime BLReceivedDate, int UserId)
        public DataTable AddJobOrderDataIntoTable(long JONo,int AgentID, int SLID, long shipperid, long Importerid, long Chaid, String ViaNo, int VesselID, int PortID, DateTime berthingdate, int Haulage_Type_Id, int File_Status_Id, int Transport_Type_Id, int POL_ID, int Sales_Person_Id, string BLNumber, DateTime BLReceivedDate, int UserId, string HouseBLNumber, int KAMID,string JoRemarks,int JoTypeId,int FileId,string IGMNo)
       
       
    {
            DataTable JobOrderData = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JobOrderData = db.AddDataJobOrderData(JONo, AgentID, SLID, shipperid, Importerid, Chaid, ViaNo, VesselID, PortID, berthingdate, Haulage_Type_Id, File_Status_Id, Transport_Type_Id, POL_ID, Sales_Person_Id, BLNumber, BLReceivedDate, UserId, HouseBLNumber, KAMID,JoRemarks,JoTypeId,FileId,IGMNo);
            return JobOrderData;

        }

        public DataTable AddRRDownloadDataIntoTable(long JONo, int AgentID, int SLID, long shipperid, long Importerid, long Chaid, String ViaNo, int VesselID, int PortID, DateTime berthingdate, int Haulage_Type_Id, int File_Status_Id, int Transport_Type_Id, int POL_ID, int Sales_Person_Id, string BLNumber, DateTime BLReceivedDate, int UserId, string HouseBLNumber, int KAMID, string JoRemarks, int JoTypeId, int FileId, string IGMNo)


        {
            DataTable JobOrderData = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JobOrderData = db.AddDataJobOrderData(JONo, AgentID, SLID, shipperid, Importerid, Chaid, ViaNo, VesselID, PortID, berthingdate, Haulage_Type_Id, File_Status_Id, Transport_Type_Id, POL_ID, Sales_Person_Id, BLNumber, BLReceivedDate, UserId, HouseBLNumber, KAMID, JoRemarks, JoTypeId, FileId, IGMNo);
            return JobOrderData;

        }

        public int AddInvoiceDataIntoTable(long BillParty, long CHAID, DataTable dtInvoiceList, DataTable dtPaymentList, long UserId, string Category, string TDSPerc, string ReceiptType, string Remarks, string PayFrom, string ADVReceiptNo, int AccountNoid,string Search,string TDSWorkyear)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            int ReceiptNo = 0;
            DataSet ds = new DataSet();
            int i;
            int IgmFileId = 0;
            //string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("BillParty", BillParty);
            lstparam.Add("CHAID", CHAID);
            lstparam.Add("user_id", UserId);
            lstparam.Add("Category", Category);
            lstparam.Add("TDSPerc", TDSPerc);
            lstparam.Add("ReceiptType", ReceiptType);
            lstparam.Add("Remarks", Remarks);
            lstparam.Add("PayFrom", PayFrom);
            lstparam.Add("ADVReceiptNo", ADVReceiptNo);
            lstparam.Add("AccountNoId", AccountNoid);
            lstparam.Add("Search", Search);
            lstparam.Add("TDSWorkyear", TDSWorkyear);

            Dictionary<object, object> lstparam2 = new Dictionary<object, object>();
            lstparam2.Add("InvoiceDT", "@InvoiceDT");
            lstparam2.Add("PaymentDT", "@PaymentDT");

            ds.Tables.Add(dtInvoiceList);
            ds.Tables.Add(dtPaymentList);

            

            ReceiptNo = db.AddInvoiceData("USP_Receipt_Generation", lstparam, ds, lstparam2);
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return ReceiptNo;

        }

       

        public Dictionary<object, object> AddCreditDataIntoTable(DataTable dtInvoiceDetails, int userId, string Category, string ReceiptType, string Remarks,string PendingPendingChequeRTGS)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> output;
            Dictionary<object, object> lstparam = new Dictionary<object, object>();

            lstparam.Add("Category", Category);
            lstparam.Add("RECEIPTTYPE", ReceiptType);
            lstparam.Add("UserId", userId);
            lstparam.Add("Remarks", Remarks);
            lstparam.Add("PendingCheque_RTGS", PendingPendingChequeRTGS);

            Dictionary<object, object> lstparam2 = new Dictionary<object, object>();
            lstparam2.Add("InvoiceDT", "@InvoiceDT");

            output = db.AddCreditData("USP_Credit_Receipt_Generation", lstparam,dtInvoiceDetails,lstparam2);
            
            return output;

        }

        public string AddJobOrderDetailsIntoTable(DataTable ContainerList, Int64 JONO)
        {
            string message = "";

          
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("JONO", JONO);
            HC.DBOperations db = new HC.DBOperations();

            int i = db.AddJobOrderDetailsList("USP_INSERT_JOBORDERD", lstparam, ContainerList);

            if (i != 0)
            {
                message = "Record added successfully";
            }
            else
            {
                message = "Record not added, Please try again!";
            }
           

            return message;
        }

        public DataTable getVesselDetails(string ViaNo)
        {
            DataTable VesselDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VesselDT = db.sub_GetDatatable("USP_IMP_Vessel_Dets  '" + ViaNo + "'");
            return VesselDT;
        }

        public DataTable getVesselDetailsExp(string ViaNo)
        {
            DataTable VesselDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VesselDT = db.sub_GetDatatable("USP_EXP_Vessel_Dets  '" + ViaNo + "'");
            return VesselDT;
        }

        public DataTable getAutoCustomer(string Prefix,string PayFrom)
        {
            DataTable VesselDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VesselDT = db.sub_GetDatatable("GetCustomerDet  '" + Prefix + "','" + PayFrom + "'");
            return VesselDT;
        }

        public DataTable CheckContainerValidation(string containerno)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("SELECT dbo.CONTAINER_NUMBER_CHECK_DIGIT('" + containerno + "')AS IsContainerNo");
            return DT;
        }

        public DataTable GetJOList(DateTime FromDate, DateTime ToDate, string SearchCriteria, string SearchText, int Userid, Boolean IsDate)
        {
            DataTable JODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JODT = db.sub_GetDatatable("USP_FetchJobOrderList '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + SearchCriteria + "','" + SearchText + "'," + Userid + "," + IsDate + "");
            return JODT;
        }

        public DataTable GetEditList(string VO_No)
        {
            DataTable JODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JODT = db.sub_GetDatatable("usp_edit_VoucherNo '" + VO_No + "'");
            return JODT;
        }

        public DataTable GetViewList(string VOUCHER_NO)
        {
            DataTable JODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JODT = db.sub_GetDatatable("usp_View_VoucherNo '" + VOUCHER_NO + "'");
            return JODT;
        }

        public DataTable CancelJO(long id, int Userid,int ReasonId)
        {
            DataTable CancelJODL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CancelJODL = db.sub_GetDatatable("USP_CancelJODL  " + id + "," + Userid + "," + ReasonId + "");
            return CancelJODL;
        }
        public DataTable SubmitJO(long id, int Userid)
        {
            DataTable SubmitJODL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            SubmitJODL = db.sub_GetDatatable("USP_SubmitJODL  " + id + "," + Userid + "");
            return SubmitJODL;
        }

        public DataSet getJOViewDetails(long JONO, int Userid)
        {
            DataSet JODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            JODS = db.sub_GetDataSets("USP_GetJODetails  " + JONO + "," + Userid + "");
            return JODS;
        }

        public DataSet AddContainerData(long JONO, string ContainerNo, int Size, string FL, string ISOCode, int Cargotypeid, int Commodity_group_id, int Userid)
        {
            //  DataTable ContainerJODL = new DataTable();
            //  HC.DBOperations db = new HC.DBOperations();
            ////  string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            //  ContainerJODL = db.sub_GetDatatable("USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "");
            //  return ContainerJODL;
            int fileid = 0;
            DataSet ContainerJODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            //  string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            ContainerJODS = db.sub_GetDataSets("USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "," +fileid+"");
            return ContainerJODS;
        }

        public DataTable DeleteContainerData(string id, int Userid)
        {
            DataTable ContainerJODL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
           // string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            ContainerJODL = db.sub_GetDatatable("USP_DeleteTempContainerJODL  " + id + "," + Userid + "");
            return ContainerJODL;
        }
        public DataSet DeleteContainerDataNew(string id, int Userid)
        {
            DataSet ContainerJODL = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            // string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            ContainerJODL = db.sub_GetDataSets("USP_DeleteTempContainerJODLBL  " + id + "," + Userid + "");
            return ContainerJODL;
        }

        public DataSet AddImportData(DataTable ContainerList, int Userid, long jono)
        {
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("Userid", Userid);
            lstparam.Add("JONo", jono);

            //DataTable ContainerJODL = new DataTable();
            
            //HC.DBOperations db = new HC.DBOperations();

            //ContainerJODL = db.AddImportData("USP_AddImportData", lstparam, ContainerList);

            //return ContainerJODL;
            DataSet ContainerJODS = new DataSet();

            HC.DBOperations db = new HC.DBOperations();

            ContainerJODS = db.AddImportData("USP_AddImportData", lstparam, ContainerList);

            return ContainerJODS;
        }

        public void DeleteTempData(int userid )
        {
            HC.DBOperations db = new HC.DBOperations();
            int i = db.sub_ExecuteNonQuery("USP_DeleteDataFromTemp " + userid + "");
        }

        public DataTable GetGstList(String SearchText, String Master)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            DT = db.sub_GetDatatable("USP_FetchGSTSummary  '" + SearchText + "','" + Master + "'");
            return DT;
        }

        public DataTable GetGLobalGSTList(String SearchText)
        {
            DataTable GlobalListDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            GlobalListDT = db.sub_GetDatatable("USP_GetGlobalSearchDataNew  '" + SearchText + "'");
            return GlobalListDT;
        }
        public int UpdateMaster(string Name, string code, string address, string contactPer, string contactDesi, string cont1, string cont2, string email, string website, string grade, Boolean chkContract, string remarks,
            int userId, Boolean CHA, Boolean Customer, Boolean shipper, Boolean shippline, Boolean importer,string cellno,string faxno,Boolean isactive,Int64 id,string TalleyMaster, Boolean JV,Boolean Vendor)
        {
            DataTable dt = new DataTable();
            int i = 0;
            HC.DBOperations db = new HC.DBOperations();
            i = db.AddMasterData(Name, code, address, contactPer, contactDesi, cont1, cont2, email, website, grade, chkContract,
                remarks, userId, CHA, Customer, shipper, shippline, importer, cellno, faxno, isactive, id, TalleyMaster,JV,Vendor);
             

            return i;

        }
        public DataTable GetUserData(Int64 id)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            DT = db.sub_GetDatatable("USP_GetUserData  " + id + "");
            return DT;
        }

  

        public DataTable GetMasterExistData(String SearchText)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            DT = db.sub_GetDatatable("USP_GetChecklistMaster  '" + SearchText + "'");
            return DT;
        }

       

        public DataTable CheckDuplicateContainer(int userid)
        {
            
            HC.DBOperations db = new HC.DBOperations();
            DataTable ContainerDL = new DataTable();
            ContainerDL = db.sub_GetDatatable("USP_CheckDuplicateContainerNo  " + userid + "");

            return ContainerDL;
        }

        public DataTable GetLogEvent(long jono)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable LogEventDT = new DataTable();
            LogEventDT = db.sub_GetDatatable("USP_GetLogData  " + jono + "");

            return LogEventDT;
        }

        public DataTable AddAttachment(int Userid, byte[] bytes, string contentType, string fname)
        {
           
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.AddAttachment(Userid, bytes, contentType, fname);
            return AttachmentDT;
        }

        public DataTable GetAttachment(long id)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_GetJOFileList  " + id + "");
            return AttachmentDT;
        }
        public DataTable GetFileData(long id)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_GetJOFileDataForDownload  " + id + "");
            return AttachmentDT;
        }

        public DataTable DeleteFile(long id,int userid)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_DeleteJOFileData  " + id + "," + userid + "");
            return AttachmentDT;
        }

        public void AddErrorLog(string error)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            db.sub_ExecuteNonQuery("Insert into ErrorLog (Errorlog) values (  '" + error + "')");
           
        }

        public DataTable GetTrailerList()
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable TrailerDT = new DataTable();
           TrailerDT= db.sub_GetDatatable("USP_GetTrailerList");
            return TrailerDT;
        
        }

        public DataTable GetTrollyList()
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable TrollyDT = new DataTable();
            TrollyDT = db.sub_GetDatatable("USP_GetTrollyList");
            return TrollyDT;
        }


        public DataTable GetTrollyMappingList()
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable TrollyDT = new DataTable();
            TrollyDT = db.sub_GetDatatable("USP_GetTrollyListForMapping");
            return TrollyDT;
        }

        public string AddTrollyData(long ID, string TrolleyNo, Boolean Isactive, int userID, decimal TrolleySize, decimal TrolleyWeight)
        {
            string message = "";
            HC.DBOperations db = new HC.DBOperations();
            DataTable TrollyDT = new DataTable();
            TrollyDT = db.sub_GetDatatable("USP_AddTrollyList " + ID + ",'" + TrolleyNo + "'," + Isactive + "," + userID + "," + TrolleySize + "," + TrolleyWeight + "");
            if (TrollyDT.Rows.Count > 0)
            {
                message = Convert.ToString(TrollyDT.Rows[0][0]);

            }
            return message;
        }

        public DataTable GetExisitingCode(string Code)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("get_sp_table 'Common_Master','*', 'RTRIM(CommonCode) =   RTRIM(''" + Code + "'')',''");
            return DT;
        }

        public DataTable GetExisitingName(string Name,Int64 id)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("get_sp_table 'Common_Master','*', 'RTRIM(Name) =   RTRIM(''" + Name + "'') and M_Common_ID <> "+ id +"',''");
            return DT;
        }

        //15 may
        public DataTable GetRoadTrailerList()
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_List_ContainersOut_By_Road");
            return DT;
        }
        //end
        public DataTable GetJOListForPOD()
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_GetBLListForPDA");
            return DT;
        }

        public DataTable GetTrainForUpdateDeparture()
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_GetTrainListForDeparture");
            return DT;
        }


        public DataTable GetContainerRoadList(int portID)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_RoadPlanningFetchContainerList " + portID + "");
            return dt;
        }

        public int SaveRemarks(string containerno, string remarks, int userId)
        {
            int i = 0;
            HC.DBOperations db = new HC.DBOperations();

            i = db.sub_ExecuteNonQuery("USP_AddRemarks '" + containerno + "', '" + remarks + "'," + userId + "");
            return i;
        }

        public DataTable GetContainerRemarksList(string containerNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            return dt;
        }

        public int DeleteRemarks(string containerno, string remarks, long jono, int userId, long surveyID)
        {
            int i = 0;
            HC.DBOperations db = new HC.DBOperations();

            i = db.sub_ExecuteNonQuery("USP_DeleteRemarks '" + containerno + "', '" + remarks + "'," + jono + "," + userId + "," + surveyID + "");
            return i;
        }


        public DataTable getActivityList()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
           // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("get_sp_table 'Codeco_Activity','*', '','activity asc'");

            return dt;
        }

        public int SaveActivity(string containerno, string activity, int userId)
        {
            int i = 0;
            HC.DBOperations db = new HC.DBOperations();

            i = db.sub_ExecuteNonQuery("USP_REAPPLICABLECODECO '" + containerno + "', '" + activity + "'");
            return i;
        }

        public DataTable getTransporterSummary()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetTransportorList");

            return dt;
        }
        public DataTable getLoadingContainerList()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_LST_Loading_Plan");

            return dt;
        }

        public DataTable getLoadingConfirmationList()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_LoadingConfirmation");

            return dt;
        }
        public DataTable GetContainerAutoComplete(string containerNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetContainerNumber_LoadingPlan  '" + containerNo + "'");

            return dt;
        }



        public DataTable SaveLoadingPlan(string containerNo, string trailerno, long JONO, int Userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_AddLoadingPlan  '" + containerNo + "','" + trailerno + "'," + JONO + "," + Userid + "");

            return dt;
        }

        public int SaveLoadingConfirmation(string containerNo, string vehicleNo, int kalmarNo, long JONO, int Userid)
        {
            int i = 0;
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            i = db.sub_ExecuteNonQuery("USP_AddLoadingConfirmation  '" + containerNo + "','" + vehicleNo + "'," + kalmarNo + "," + JONO + "," + Userid + "");

            return i;
        }
        public DataSet getTransportData(int id, int Userid)
        {
            DataSet ds = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ds = db.sub_GetDataSets("USP_GetTranspotorData  " + id + "," + Userid + "");

            return ds;
        }

        public DataTable getKalmarList()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_KalmarList");

            return dt;
        }

        public DataTable getFuelList(string vehiclename)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_FuelList '" + vehiclename + "'");

            return dt;
        }
        public DataTable GetTPValidate(string Trailername)
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_ValidateTrailerTPDetails '" + Trailername + "'");
            return DriverListDL;
        }
        //public DataTable VoucherValidation(string TrailerNo, string ContainerNo, int ActivityTypeID, string InOutStatus)
        //{
        //    DataTable dt = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    //  dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan ");

        //    dt = db.sub_GetDatatable("USP_VoucherValidation '" + TrailerNo + "','" + ContainerNo + "'," + ActivityTypeID + ",'" + InOutStatus + "'");

        //    return dt;
        //}

        public DataTable getContainerListForFuelGenerate(string TrailerNo, string Activity, int ActivityTypeID, string ContainerNo,string LocationYardID)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Generate_Fuel_Slip_Container_Dets '" + TrailerNo + "','" + Activity + "'," + ActivityTypeID + ",'" + ContainerNo + "','"+ LocationYardID + "'");

            return dt;
        }

        public DataTable GetCostCenterFor()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_CostCenterForDetails");

            return dt;
        }

        //public DataTable getFuelData(int planID)
        //{
        //    DataTable dt = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
        //    dt = db.sub_GetDatatable("USP_FuelData " + planID + "");

        //    return dt;
        //}

        public DataTable getPassing()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_PassingData");

            return dt;
        }

        

        public int UpdatePassing(string trailerNo, string NewtrailerNo, string Remarks, long trailerID, int Userid)
        {
            DataTable dt = new DataTable();
            int i;
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            i = db.sub_ExecuteNonQuery("USP_UpdatePassing  '" + trailerNo + "','" + NewtrailerNo + "','" + Remarks + "'," + trailerID + "," + Userid + "");

            return i;
        }
        public DataTable GetTrailerDriverList()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetTrailerDriverData");

            return dt;
        }

        public int UpdateTrailerDriver(string trailerID, string driverID, int userId)
        {
            DataTable dt = new DataTable();
            int i;
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            i = db.sub_ExecuteNonQuery("USP_UpdateDriverTrolley " + trailerID + "," + driverID + "," + userId + "");

            return i;
        }

        public DataTable getLineWiseContainer(string lineId)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
          //  dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan ");

            dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan  '" + lineId + "'");

            return dt;
        }

        public DataTable GetTrainExportList()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //  dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan ");

            dt = db.sub_GetDatatable("USP_Export_Train_Planned_List");

            return dt;
        }
        public DataTable GetRoadExportList()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //  dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan ");

            dt = db.sub_GetDatatable("USP_Export_Road_Planned_List");

            return dt;
        }
        public DataTable getReadingFrom(string trailerNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //  dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan ");

            dt = db.sub_GetDatatable("USP_GetReadingFrom '" + trailerNo + "'");

            return dt;
        }

        public DataTable VoucherValidation(string TrailerNo, string ContainerNo, int ActivityTypeID, string InOutStatus,string LocationYardID)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //  dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan ");

            dt = db.sub_GetDatatable("USP_VoucherValidation '" + TrailerNo + "','" + ContainerNo + "'," + ActivityTypeID + ",'" + InOutStatus + "','"+ LocationYardID + "'");

            return dt;
        }



        /// <summary>
        /// ////////////by Aarti
        /// </summary>
        /// <returns></returns>
        public DataTable GetIGMFileStatus()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_BLSummaryForPODETAFetchIGMFileStatusList");
            return LoginDT;
        }

 

        public DataTable UpdateBLData(int JONO, string PODETADate, int VesselID, int File_Status_Id, string IGMNo, int userID)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_BLSummaryForPODETAUpdateDetails " + JONO + ",'" + PODETADate + "'," + VesselID + "," + File_Status_Id + ",'" + IGMNo + "'," + userID);
            return AttachmentDT;
        }





        /////////////////.............By Monisha 18 March 2019................/////////////
        public int AddCHAMasterDetails(int chaID, string chaName, string address, string city, string contactPerson, string designation, string contactNO1, string contactNO2, string faxNumber, string cellNumber, string emailsIDs, string remarks, bool isActive, string addedBY, string chaCode)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("SP_SaveCHADetails  '" + chaID + "','" + chaName + "','" + address + "','" + city + "','" + contactPerson + "', '" + designation + "','" + contactNO1 + "','" + contactNO2 + "','" + faxNumber + "','" + cellNumber + "','" + emailsIDs + "','" + remarks + "'," + isActive + ",'" + addedBY + "','" + chaCode + "'");
            return LoginDT;
        }


        public int AddImporterMasterDetails(int importerID, string importerCode, string importerName, string impAddress, string impCity, string impAuthPerson, string impAuthPersonDesig, string impTelI, string impTelII, string impFax, string impCellNo, string impEMail, string impPANNo, string remarks, bool isActive, int addedby)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("sp_UpdateImporter  " + importerID + ",'" + importerCode + "','" + importerName + "','" + impAddress + "','" + impCity + "', '" + impAuthPerson + "','" + impAuthPersonDesig + "','" + impTelI + "','" + impTelII + "','" + impFax + "','" + impCellNo + "','" + impEMail + "','" + impPANNo + "','" + remarks + "'," + isActive + "," + addedby);
            return LoginDT;
        }



        public int AddShipLineMasterDetails(int slID, string saID, string slName, string slAddress, string slCity, string slAuthPerson, string slAuthPersonDesig, string slTelI, string slTelII, string slFax, string slAuthPersonCellNo, string slEMail, string remarks, bool isActive, string addedBY, string modifiedBY, bool isContract)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("SP_SaveShippingLineDetails  " + slID + ",'" + saID + "','" + slName + "','" + slAddress + "','" + slCity + "', '" + slAuthPerson + "','" + slAuthPersonDesig + "','" + slTelI + "','" + slTelII + "','" + slFax + "','" + slAuthPersonCellNo + "','" + slEMail + "','" + remarks + "'," + isActive + ", '" + addedBY + "','" + modifiedBY + "'," + isContract);
            return LoginDT;
        }

  
        public DataTable GetExisitingCHACode(string chaCode)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'CHA','*', 'RTRIM(CHACOde) =   RTRIM(''" + chaCode + "'')',''");
            return LoginDT;
        }

        public DataTable GetExisitingCHAName(string chaName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'CHA','*', 'RTRIM(CHACOde) =   RTRIM(''" + chaName + "'')',''");
            return LoginDT;
        }

        public DataTable GetNewCHAID()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'CHA','isnull (max(Cast(CHAID as int ) ),0)+1 as CHAID', '',''");
            return LoginDT;
        }
        public DataTable GetNameForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'CHA','distinct CHAName', 'CHAName like ''" + name + "%''','CHAName'");
            return LoginDT;
        }
        public DataTable GetContactPersonForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'CHA','distinct ContactPerson', 'ContactPerson like ''" + name + "%''',''");
            return LoginDT;
        }
        public DataTable GetDesignationForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'CHA','distinct Designation', 'Designation like ''" + name + "%''',''");
            return LoginDT;
        }
        public DataTable GetExisitingImporterCode(string impCode)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Importer','*', 'RTRIM(ImporterCode) =   RTRIM(''" + impCode + "'')',''");
            return LoginDT;
        }

        public DataTable GetExisitingImporterName(string impName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Importer','*', 'RTRIM(ImporterName) =   RTRIM(''" + impName + "'')',''");
            return LoginDT;
        }

        public DataTable GetNewImporterID()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Importer','isnull (max(Cast(ImporterID as int ) ),0)+1 as ImporterID', '',''");
            return LoginDT;
        }

        public DataTable GetImpNameForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Importer','distinct ImporterName', 'ImporterName like ''" + name + "%''',''");
            return LoginDT;
        }
        public DataTable GetImpContactPersonForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Importer','distinct ImpAuthPerson', 'ImpAuthPerson like ''" + name + "%''',''");
            return LoginDT;
        }
        public DataTable GetImpDesignationForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Importer','distinct ImpAuthPersonDesig', 'ImpAuthPersonDesig like ''" + name + "%''',''");
            return LoginDT;
        }

        public int AddShipperMasterDetails(int shipperID, string shipperIECNo, string shipperName, string shipperAddress, string shipperCity, string shipperContactPerson, string shipperPersonDesig, string shipperTelI, string shipperTelII, string shipperFax, string shipperCellNo, string shipperEMail, string remarks, bool isActive, string addedBY, string modifiedBY)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("SP_SaveExportShipperMaster  " + shipperID + ",'" + shipperIECNo + "','" + shipperName + "','" + shipperAddress + "','" + shipperCity + "', '" + shipperContactPerson + "','" + shipperPersonDesig + "','" + shipperTelI + "','" + shipperTelII + "','" + shipperFax + "','" + shipperCellNo + "','" + shipperEMail + "','" + remarks + "'," + isActive + ",'" + addedBY + "','" + modifiedBY + "'");
            return LoginDT;
        }

        public DataTable GetNewShippingLineID()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'ShipLines','isnull (max(Cast(SLID as int ) ),0)+1 as SLID', '',''");
            return LoginDT;
        }

        public DataTable GetExisitingShippingLineCode(string shippingLineCode)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'ShipLines','*', 'RTRIM(SaID) =   RTRIM(''" + shippingLineCode + "'')',''");
            return LoginDT;
        }

        public DataTable GetExisitingShippingLineName(string shippingLineName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'ShipLines','*', 'RTRIM(SLName) =   RTRIM(''" + shippingLineName + "'')',''");
            return LoginDT;
        }
        public DataTable GetSLNameForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'ShipLines','distinct SLName', 'SLName like ''" + name + "%''',''");
            return LoginDT;
        }
        public DataTable GetSLContactPersonForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'ShipLines','distinct SLAuthPerson', 'SLAuthPerson like ''" + name + "%''',''");
            return LoginDT;
        }
        public DataTable GetSLDesignationForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'ShipLines','distinct SLAuthPersonDesig', 'SLAuthPersonDesig like ''" + name + "%''',''");
            return LoginDT;
        }

        public DataTable GetNewShipperID()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'exp_shippers','isnull (max(Cast(shipperID as int ) ),0)+1 as shipperID', '',''");
            return LoginDT;
        }

        public DataTable GetExisitingShipperCode(string shipperCode)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'exp_shippers','*', 'RTRIM(Shipper_IEC_No) =   RTRIM(''" + shipperCode + "'')',''");
            return LoginDT;
        }

        public DataTable GetExisitingShipperName(string shipperName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'exp_shippers','*', 'RTRIM(shippername) =   RTRIM(''" + shipperName + "'')',''");
            return LoginDT;
        }
        public DataTable GetShipperNameForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'exp_shippers','distinct shippername', 'shippername like ''" + name + "%''',''");
            return LoginDT;
        }
        public DataTable GetShipperContactPersonForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'exp_shippers','distinct ContactPerson', 'ContactPerson like ''" + name + "%''',''");
            return LoginDT;
        }
        public DataTable GetShipperDesignationForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'exp_shippers','distinct Designation', 'Designation like ''" + name + "%''',''");
            return LoginDT;
        }

        public int AddCustomerMasterDetails(int aGID, string aGaID, string aGName, string aGAddress, string aGCity, string aGAuthPerson, string aGAuthPersonDesig, string aGTelI, string aGTelII, string aGFax, string aGCellNo, string aGEMail, string aGWebsite, int CreditPeriod, string remarks, bool isActive, int addedby, string grade)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("sp_UpdateCustomer  " + aGID + ",'" + aGaID + "','" + aGName + "','" + aGAddress + "','" + aGCity + "', '" + aGAuthPerson + "','" + aGAuthPersonDesig + "','" + aGTelI + "','" + aGTelII + "','" + aGFax + "','" + aGCellNo + "','" + aGEMail + "','" + aGWebsite + "'," + CreditPeriod + ",'" + remarks + "'," + isActive + "," + addedby + ",'" + grade + "'");
            return LoginDT;
        }
        public DataTable GetNewCustomerID()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable(" get_sp_table 'customer','isnull (max(Cast(AGID as int ) ),0)+1 as customerID', '',''");
            return LoginDT;
        }
        public DataTable GetCustNameForAutoComplete(string custName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("select AGName from Customer where AGName like '" + custName + "%'");
            return LoginDT;
        }
        public DataTable GetCustContactPersonForAutoComplete(string custName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'customer','distinct AGAuthPerson', 'AGAuthPerson like ''" + custName + "%''',''");
            return LoginDT;
        }
        public DataTable GetCustDesignationForAutoComplete(string custName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'customer','distinct AGAuthPersonDesig', 'AGAuthPersonDesig like ''" + custName + "%''',''");
            return LoginDT;
        }
        public DataTable GetExisitingAgentCode(string agentCode)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'customer','*', 'RTRIM(AGaID) =   RTRIM(''" + agentCode + "'')',''");
            return LoginDT;
        }

        public DataTable GetExisitingCustomerName(string agentName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'customer','*', 'RTRIM(AGName) =   RTRIM(''" + agentName + "'')',''");
            return LoginDT;
        }

        public int AddVesselMasterDetails(int vesselID, string vesselName, bool isActive, int addedBY, int modifiedBY)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("USP_INSERT_VESSELS  " + vesselID + ",'" + vesselName + "'," + isActive + ",'" + addedBY + "','" + modifiedBY + "'");
            return LoginDT;
        }

        public DataTable GetExisitingVesselName(string vesselName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'vessels','vesselname', 'vesselname=''" + vesselName + "''','vesselname'"); //"select * from VESSELS where RTRIM(VesselName) = RTRIM('" + vesselName + "')"
            return LoginDT;
        }

        public DataTable GetVesselNameForAutoComplete(string vesselName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'vessels','distinct vesselname', 'vesselname like ''" + vesselName + "%''','vesselname'"); // "select distinct VesselName from VESSELS where VesselName like '" + vesselName + "%'"
            return LoginDT;
        }

        public DataTable GetNewVesselID()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'vessels','isnull (max(Cast(VesselID as int ) ),0)+1 as VesselID', '',''"); //" Select isnull (max(Cast(VesselID as int ) ),0)+1 as VesselID from VESSELS"
            return LoginDT;
        }

        public int AddTrainMasterDetails(int trainID, string trainNO, string portFrom, string portTo, string operatorID, string operatorName, string date, int addedBY, int modifiedBY, string IsImportORExportSelected)
        {
            DataTable LoginDT = new DataTable();

            int i = 0;

            HC.DBOperations db = new HC.DBOperations();

            i = db.sub_ExecuteNonQuery("USP_INSERT_TRAINM " + trainID + ",'" + trainNO + "','" + portFrom + "','" + portTo + "','" + operatorID + "','" + operatorName + "','" + date + "','" + addedBY + "','" + modifiedBY + "','" + IsImportORExportSelected + "'");

            return i;

        }

        public DataTable GetExisitingTrainNO(string trainNO)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'trainm','*', 'RTRIM(trainno) =   RTRIM(''" + trainNO + "'')',''");
            return LoginDT;
        }

        public DataTable GetNewTrainID()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable(" get_sp_table 'trainm','isnull (max(Cast(trainid as int ) ),0)+1 as trainid', '',''");
            return LoginDT;
        }
        public DataTable GetTrainNoForAutoComplete(string trainNo)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'trainm','distinct trainno', 'trainno like ''" + trainNo + "%''',''");
            return LoginDT;
        }
        public DataTable GetNOCForAutoComplete(string NOCNo)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("SELECT distinct NOCRefNo,NOCNo,OrginStationId,SLid,CommodityId,FORMAT(PlannedDate,'dd MMM yyyy hh:MM')PlannedDate FROM BCN_NocUpdation WHERE NOCRefNo like '" + NOCNo + "%'");
            return LoginDT;
        }
        public DataTable GetOperatorListForTrainMaster()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'operatorm', 'ID , Code, Name','',''");
            return LoginDT;
        }
        public DataTable GetNewOperatorID()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'operatorm','isnull (max(Cast(ID as int ) ),0)+1 as ID', '',''");
            return LoginDT;
        }
        public DataTable GetExisitingOperatorName(string operatorName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable(" get_sp_table 'operatorm','*', 'RTRIM(Name) =   RTRIM(''" + operatorName + "'')',''");
            return LoginDT;
        }
        public DataTable GetOperatorNameForAutoComplete(string operatorName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'operatorm','distinct Name', 'Name like ''" + operatorName + "%''',''");
            return LoginDT;
        }
        public DataTable GetTrainSummary(string process)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'trainm', 'TrainID,TrainNO,PortFrom,PortTo,Operator,OperatorName,process','process=''" + process + "''','TrainID desc'");
            return LoginDT;
        }
        public DataTable EditTrainSummary(int trainID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'trainm', 'TrainID,TrainNO,PortFrom,PortTo,Operator,OperatorName','trainid=" + trainID + "','TrainID desc'");
            return LoginDT;
        }
        public int UpdateTrainMasterDetails(int trainID, string trainNO, string portFrom, string portTo, string operatorID, string operatorName, int modifiedBY)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("USP_UpdateTrainMasterDetails " + trainID + ",'" + trainNO + "','" + portFrom + "','" + portTo + "','" + operatorID + "','" + operatorName + "'," + modifiedBY);
            return LoginDT;
        }
        public int AddOperatorMasterDetails(int operaID, string operName, bool isActive, int addedby)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("USP_INSERT_OPERATORM  " + operaID + ",'','" + operName + "','','','','','','','','','',''," + isActive + ",0," + addedby);
            return LoginDT;
        }
        public DataTable GetPortList()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Ports', '*','',''");
            return LoginDT;
        }
        public int AddVesselMovementDetails(int vesselID, string viaNO, int portId, DateTime berthingdate, int addedBy, int modifiedBy, string voyagedNO, string igmno, DateTime igmDate)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            ////string bertdate, igmdate;
            ////if (Convert.ToString(berthingdate).Contains('.') || Convert.ToString(igmDate).Contains('.'))
            ////{
            ////    igmDate = Convert.ToDateTime(Convert.ToString(igmDate).Replace('.', ':'));
            ////    igmdate = Convert.ToString(igmDate).Replace('.', ':');

            ////    berthingdate = Convert.ToDateTime(Convert.ToString(berthingdate).Replace('.', ':'));
            ////    bertdate = Convert.ToString(berthingdate).Replace('.', ':');
            ////}
            ////else
            ////{
            ////    igmdate = Convert.ToString(igmDate);
            ////    bertdate = Convert.ToString(berthingdate);
            ////}
            LoginDT = db.sub_ExecuteNonQuery("USP_INSERT_IMP_MOVEMENT " + vesselID + ",'" + viaNO + "'," + portId + ",'" + Convert.ToDateTime(berthingdate).ToString("yyyy-MM-dd HH:mm:ss") + "'," + addedBy + "," + modifiedBy + ", '" + voyagedNO + "','" + igmno + "','" + Convert.ToDateTime(igmDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            return LoginDT;
        }
        public DataTable GetExisitingVesselIDForVesselMovement(string vesselNO)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable(" get_sp_table 'imp_movement','*', 'RTRIM(VesselID) =   RTRIM(''" + vesselNO + "'')',''");
            return LoginDT;
        }
        //update by durga 29 Mar
        public DataTable GetExisitingViaNOForVesselMovement(string igmno)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // LoginDT = db.sub_GetDatatable("select * from imp_movement where RTRIM(viaNO) =  RTRIM('" + viaNO + "')");
            dt = db.sub_GetDatatable("USP_FetchVesselDetal '" + igmno + "'");
            return dt;
        }
        //End
        public DataTable GetNewVesselMovementID()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'imp_movement','isnull (max(Cast(MovementID as int ) ),0)+1 as MovementID', '',''");
            return LoginDT;
        }

        public DataTable GetViaNOForAutoComplete(string viaNO)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("select distinct IgmNo from imp_movement where IgmNo like '%" + viaNO + "%'");
            // LoginDT = db.sub_GetDatatable("USP_FetchVesselDetal '" + viaNO + "'");

            return LoginDT;
        }

        public DataTable GetVesselListForVesselMovement()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Vessels', 'VesselID, VesselName','','VesselName'");
            return LoginDT;
        }

        public DataTable GetPortForTrainDeparture()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("exec USP_LST_PORT");
            return LoginDT;
        }

        public DataTable GetContainerForTrainDeparture(int portID, int userID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("exec USP_LST_Pending_CTR_MAP_IMP_Train " + portID + "," + userID);
            return LoginDT;
        }
        public DataTable GetTrainNOForTrainDeparture()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'TrainM', 'TrainId,TrainNo','TrainArrivedDate is null','TrainNo'");
            return LoginDT;
        }
        public DataTable UpdateSelectedContainer(int joNo, string containerNo, bool isChecked, int userID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("exec  USP_UpdateContainerState " + joNo + "," + isChecked + ",'" + containerNo + "'," + userID);
            return LoginDT;
        }
        public DataTable GetTotalCount(int userID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Train_Container_Selection', ' count(*) as TotalSelected',' IsContainerSelected = 1 and userid =" + userID + "','' ");
            return LoginDT;
        }
        public int UpdateWagonNoForSelectedContainer(string wagonNo, string containerNo, int userID)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("USP_UpdateWagonNo '" + wagonNo + "','" + containerNo + "'," + userID);
            return LoginDT;
        }
        public int UpdateTrainNoForSelectedContainer(string trainNO, int userID)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("USP_UpdateTrainNo '" + trainNO + "'," + userID);
            return LoginDT;
            //DataTable dt = new DataTable();
            //HC.DBOperations db = new HC.DBOperations();
            //dt = db.sub_GetDatatable("USP_UpdateTrainNo '" + trainNO + "'," + userID);
            //return dt;
        }
        public DataTable GetVesselSummaryList()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'vessels','VesselID,VesselName', 'isactive = 1','VesselName asc'");
            return LoginDT;
        }
        public DataTable EditVesselSummary(int vesselID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'vessels','VesselID,VesselName, ISActive', 'vesselid=" + vesselID + "','VesselID'");
            return LoginDT;
        }
        public DataTable updateVesselMasterDetails(int vesselID, string vesselName, bool isActive)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("exec  USP_UpdateVesselDetails " + vesselID + ",'" + vesselName + "'," + isActive);
            return LoginDT;
        }
        public DataTable GetContainerForUpdateDischargeDate(int portID, int userID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("exec USP_Discharge_Container_Selection_List " + portID + "," + userID);
            return LoginDT;
        }

        //Codes by Monisha
        public DataTable GetMonthlyDataForPerson(int personID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("exec MIS_SALESPERSONWISE_MONTHLY_ARRIVAL " + personID);
            return LoginDT;
        }
        public DataTable GetPipelineReportForGivenMonth(int personID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("exec MIS_SALESPERSONWISE_MONTHLY_PIPILINE " + personID);
            return LoginDT;
        }

        public DataTable GetSalesPersonIDForGraph(string personName)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable(" get_sp_table 'SalesPersonM','SalesPerson_ID1', 'SalesPerson_Name = ''" + personName + "'''");
            return LoginDT;
        }
        public DataTable GetMonthlyTEUSForImportArrival()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("exec MIS_ARRIVAL_TOTAL_TUES_MONTHLY");
            return LoginDT;
        }
        public DataTable GetMonthlyTEUSForPipeLine()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("exec MIS_ARRIVAL_TOTAL_PIPELINE_MONTHLY");
            return LoginDT;
        }

        //Codes end by monisha

        public DataTable UpdateSelectedContainerForUpdateDischargeDate(int joNo, string containerNo, bool isChecked, int userID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("exec  USP_UpdateContainerStateForDischargeDate " + joNo + "," + isChecked + ",'" + containerNo + "'," + userID);
            return LoginDT;
        }

        public DataTable GetTotalCountForUpdateDischargeDate(int userID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Discharge_Container_Selection', ' count(*) as TotalSelected',' IsContainerSelected = 1 and userid =" + userID + "','' ");
            return LoginDT;
        }

        public int UpdateDischargeDateForSelectedContainer(DateTime dischargeDate, string containerNo, int userID)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("USP_UpdateDischargeDate '" + Convert.ToDateTime(dischargeDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + containerNo + "'," + userID);
            return LoginDT;
        }

        public int UpdateTrainNoForSelectedContainerForUpdateDischargeDate(string trainNO, int userID)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("USP_UpdateTrainNoForDischargeDate '" + trainNO + "'," + userID);
            return LoginDT;
        }

        // by durga
        public int UpdateRemarksForSelectedContainer(string Remarks, string containerNo, string jono, int userID)
        {
            int i = new int();
            HC.DBOperations db = new HC.DBOperations();
            i = db.sub_ExecuteNonQuery("USP_UpdateRemarks '" + Remarks + "','" + containerNo + "'," + jono + "," + userID);
            return i;
        }
        public void DeleteFromTempTrain_Container_Selection( int userID)
        {
            int i = new int();
            HC.DBOperations db = new HC.DBOperations();
            i = db.sub_ExecuteNonQuery("USP_DeleteFromTempTrain_Container_Selection  " + userID);
            
        }

        public DataTable GetContainerUpdateDischargeData(string ContainerNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_getUpdateDischargeData  '" + ContainerNo + "'");
            return dt;
        }


        public DataTable getPendingContainerForJo()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_Listpendingjo ");
            return dt;
        }

        public DataTable GetInventory(string AsonDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("sp_categorywiseinventory '" + Convert.ToDateTime(AsonDate).ToString("yyyyMMddHHmm") + "','" + Convert.ToDateTime(AsonDate).ToString("dd-MMM-yyyy HH:mm") + "'");
            return dt;
        }

        public DataTable getCriteriaList()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_getCriteria ");
            return dt;
        }

        public DataTable getMovementICD(string Criteria, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_MovementAtICD  '" + FromDate + "','" + ToDate + "','" + Criteria + "'");
            return dt;
        }

        public DataTable getTrolleytransportlist()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetTrolleyList ");
            return dt;
        }

        public DataTable getContainerSearchList(string containerno)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_ContainerNoSearch '" + containerno + "'");
            return dt;
        }
        public DataTable getGSTReturnList(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GST_RETUN_SUMMARY '" + FromDate + "','" + ToDate + "'");
            return dt;
        }
        public DataTable getCodecoStatusList(string FromDate, string ToDate, string ShipLines, string Event, string ContainerNo, string SearchCriteria)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_CodecoStatusList '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + ShipLines + "','" + Event + "','" + ContainerNo + "'," + SearchCriteria + "");
            return dt;
        }
   ///////end by durga
   
        ////code added by aarti

        public DataTable getCategorywiseInvoice(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_CategoryWiseShowXML '" + Date + "','" + Category + "'");

            return dt;
        }

        public DataTable getCategorywiseInvoiceCarting(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_CategoryWiseShowXMLCarting '" + Date + "','" + Category + "'");

            return dt;
        }

        public DataTable getCategorywiseInvoiceEDI(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_CategoryWiseShowXMLEDI '" + Date + "','" + Category + "'");

            return dt;
        }

        public DataTable getCategorywiseYardInvoice(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_CategoryWiseShowXMLMty '" + Date + "','" + Category + "'");

            return dt;
        }

        public DataTable GetExporttoExcelData(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("TALLY_INTERFACE_INVOICE_ALLnew '" + Date + "','" + Date + "','" + Category + "'");

            return dt;
        }

        public DataTable getCategorywiseReceipt(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_CategoryWiseShowXMLReceipt '" + Date + "','" + Category + "'");

            return dt;
        }
        //FetchCategorywiseEDI
        public DataTable FetchCategorywiseEDI(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_CategoryWiseShowXMLReceipt_EDI '" + Date + "','" + Category + "'");

            return dt;
        }
        public DataTable getJVDBReceipt(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_JVXMLReceipt '" + Date + "','" + Category + "'");

            return dt;
        }

        public DataTable getCategorywiseYardReceipt(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_CategoryWiseShowXMLReceiptYard '" + Date + "','" + Category + "'");

            return dt;
        }

        public DataTable GetExporttoExcelDataReceipt(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("TALLY_INTERFACE_RECEIPT '" + Date + "','" + Date + "','" + Category + "'");

            return dt;
        }


        public DataTable AddManualportout(string TrailerNo, string ContainerNo, int ContainerSize, int ContainerType, int Portname, int Cycle, int TransportType, int FromSource, int FromDestination, int TrainNo, string WagonNo, string SMTPReceived, int userId)
        {
            DataTable Manualportout = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Manualportout = db.sub_GetDatatable("USP_INSERT_PORT_GATEOUTDETAILS '" + TrailerNo + "','" + ContainerNo + "'," + ContainerSize + "," + ContainerType + "," + Portname + "," + Cycle + "," + TransportType + "," + FromSource + "," + FromDestination + "," + TrainNo + ",'" + WagonNo + "','" + SMTPReceived + "'," + userId + "");
            return Manualportout;
        }

        public DataTable GetTrailerNo(string TrailerNo)
        {
            DataTable TrailerNoDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TrailerNoDL = db.sub_GetDatatable("USP_TrailerNumberFetch '" + TrailerNo + "'");
            return TrailerNoDL;
        }
        public DataTable GetTrailerNoFor_Fuel_Con(string TrailerNo)
        {
            DataTable TrailerNoDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TrailerNoDL = db.sub_GetDatatable("USP_TrailerNumberFetch_For_Fuel_Con '" + TrailerNo + "'");
            return TrailerNoDL;
        }
        public DataTable GetContainerList(int portID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_TrainPlanningFetchContainerList " + portID + "");
            return LoginDT;
        }

        public DataTable GetExistingContainerList(int portID, int trainID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_TrainPlanningFetchExistingContainerList " + portID + "," + trainID + "");
            return LoginDT;
        }
        public DataSet TrolleyTransportDetails()
        {
            DataSet TTDS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            TTDS = db.sub_GetDataSets("usp_Trolley_DropDown  ");
            return TTDS;
        }

        public DataTable GetHorseSummary()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_HORSE_SEARCH_SUMMARY");
            return LoginDT;
        }

        public DataSet getDropDownTrailerData()
        {
            DataSet TrailerDropDownDS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            TrailerDropDownDS = db.sub_GetDataSets("USP_FILL_DROPDOWN_TRAILER_TRANSPORT");

            return TrailerDropDownDS;
        }

        public DataTable AddBankData(string BankID, string AccountNo, string IFSCCode, string AccountName, string BranchName, string EmailID, bool IsActive, int Userid, string BankName)
        {
            //  DataTable ContainerJODL = new DataTable();
            //  HC.DBOperations db = new HC.DBOperations();
            ////  string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            //  ContainerJODL = db.sub_GetDatatable("USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "");
            //  return ContainerJODL;
            DataTable BankDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //  string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            BankDT = db.sub_GetDatatable("USP_INSERT_TEMP_TRANSPORTER_BANK  '" + BankID + "','" + AccountNo + "','" + IFSCCode + "','" + AccountName + "','" + BranchName + "','" + EmailID + "'," + IsActive + "," + Userid + ",'" + BankName + "'");
            return BankDT;
        }

        public DataTable DeleteBankData(string id, int Userid)
        {
            DataTable ContainerJODL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            ContainerJODL = db.sub_GetDatatable("USP_Delete_Temp_Transporter_Bank  " + id + "," + Userid + "");
            return ContainerJODL;
        }

        public DataTable GetTransportBankData()
        {
            DataTable TransportBankDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TransportBankDL = db.sub_GetDatatable("select * from import_banks order by bankname");
            return TransportBankDL;
        }

        public void DeleteTempTransporterData(int userid)
        {
            HC.DBOperations db = new HC.DBOperations();
            int i = db.sub_ExecuteNonQuery("USP_DeleteDataFrom_Temp_Transporter_Bank " + userid + "");
        }
        //public DataTable GetLocationDetails()
        //{
        //    DataTable LoginDT = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    LoginDT = db.sub_GetDatatable("get_sp_table 'ext_location_m', 'LocationID,Location','','Location'");
        //    return LoginDT;
        //}

        public DataTable GetGSTRegistrationType()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'GST_Registration_Type', 'RGID,RGType','','RGType'");
            return LoginDT;
        }


        public DataTable GetStateCode(string GstNo)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("Sp_stateFilter '" + GstNo + "'");
            return LoginDT;
        }
        public DataTable GetContainerListforTrainNo(string TrainNo)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_ShowContainerArrival " + TrainNo + "");
            return LoginDT;
        }

        ///by durga
        public DataTable GetCustomerLocationList(Int64 id)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_getCustomerWiseLocationList " + id + "");
            return LoginDT;
        }


        public DataTable GetGSTLocationWiseData(Int32 LocationID, Int64 Common_ID, Int64 GSTID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_getGSTWiseLocationData " + LocationID + "," + Common_ID + "," + GSTID + "");
            return LoginDT;
        }


        public int UpdateFuelStatus(string TrailerNo, string LocationYardID, string ActivityID, int Userid)
        {
            int i = 0;
            HC.DBOperations db = new HC.DBOperations();
            i = db.sub_ExecuteNonQuery("USP_ClosedFuelStatus  '" + TrailerNo + "','" + LocationYardID + "','" + ActivityID + "','" + Userid + "'");
            return i;
        }
        ///end by durga


        public DataTable GetTEUSCalculation(string TrainNo)
        {

            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_Validations_Train_TEUS '" + TrainNo + "'");
            return LoginDT;
        }

        public DataTable CheckDuplicateContainer(string ContainerNo)
        {

            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("ContainerExistValidation_Port_Out '" + ContainerNo + "'");
            return LoginDT;
        }
        public DataTable GetTrainStatusSheetList()
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_GetTrainStatusSheetList");
            return DT;
        }


        public DataTable GetTrainStatusSheetCompletedList()
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("[USP_GetTrainStatusSheetCompletedList]");
            return DT;
        }


        public DataTable AddDirectFuel(string containerCount, int containerTypeID, string TrailerNo, string Passing, int driverID, int TransportorID, int FromID, int ToID, int Activity, string ReadingFrom, string ReadingTo, string fuel, string Amount1, string Amount2, string AdjustFuel, int Userid, string PlanID, string INOUT, string ContainerNo, int TarrifID, string VehTransID, string Remarks)
        {
            DataTable VendorDataDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string str = "";
            str = "USP_FuelDirectInsertDetails '" + containerCount + "' ,'" + containerTypeID + "' ,'" + TrailerNo + "' ,'" + Passing + "' ," + driverID + " ," + TransportorID + " ," + FromID + " ,'" + ToID + "','" + Activity + "','" + ReadingFrom + "','" + ReadingTo + "','" + fuel + "','" + Amount1 + "','" + Amount2 + "','" + AdjustFuel + "'," + Userid + ",'" + PlanID + "','" + INOUT + "','" + ContainerNo + "'," + TarrifID + ",'" + VehTransID + "','" + Remarks + "'";
            VendorDataDL = db.sub_GetDatatable(str);

            return VendorDataDL;
        }

        public DataTable getActivity()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_ActivityMaster");

            return dt;
        }

        public DataTable getTrailerWiseDriverData(int trailerID)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_getTrailerWiseDriverData " + trailerID + "");
            return dt;
        
        }

        public DataTable getFuelDataForGenerate(string TrailerNo, string ContainerNo, string Activity, int ActivityTypeID, string LocationYardID)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_getFuelDataForGenerate '" + TrailerNo + "','" + ContainerNo + "','" + Activity + "'," + ActivityTypeID + ",'" + LocationYardID + "'");

            return dt;
        }

        public DataTable getPrintDataForFuelTarriff(string fuelID, string AutoID)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_getFuelDataForPrint '" + fuelID + "','" + AutoID + "'");

            return dt;
        }

        //public DataTable SaveFuelDirectSetting(int TranspoterID, string FromDate, string ToDate, int ActivityID, string InOut, int ContainerTypeID, string ContaierSize, int Fromlocation, int Tolocation, float Fuel, string Amount1, string Amount2, int RecordID, int Userid, string passing, string TrailerType)
        //{
        //    DataTable VendorDataDL = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    string str = "";
        //    str = "USP_FuelTariffSettingInsertDetails " + TranspoterID + " ,'" + FromDate + "' ,'" + ToDate + "' ," + ActivityID + " ,'" + InOut + "' ," + ContainerTypeID + " ,'" + ContaierSize + " '," + Fromlocation + "," + Tolocation + "," + Fuel + "," + Amount1 + "," + Amount2 + "," + RecordID + "," + Userid + ",'" + passing + "','" + TrailerType + "'";
        //    VendorDataDL = db.sub_GetDatatable(str);

        //    return VendorDataDL;
        //}

        public DataTable GetFuelTariffList(int TransportorID, int ActivityID, string INOUT, int ConTypeID, string size, int FromID, int ToID, string Passing, string TrailerType, string DrivertypeID, string VehicleTypeID, string Scantype)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            dt = db.sub_GetDatatable("USP_FuelTariffSettingFetchDetails " + TransportorID + "," + ActivityID + ",'" + INOUT + "'," + ConTypeID + ",'" + size + "'," + FromID + "," + ToID + ",'" + Passing + "','" + TrailerType + "','" + DrivertypeID + "','" + VehicleTypeID + "','" + Scantype + "'");

            return dt;
        }

        public DataTable GetGeneratedFuelSlipData()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            dt = db.sub_GetDatatable("USP_GetGeneratedFuelSlipList");

            return dt;
        }
        public DataTable GetGeneratedFuelSlipDataForHold(string Date)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            dt = db.sub_GetDatatable("USP_GetGeneratedFuelSlipHoldList '" + Date + "'");

            return dt;
        }
        public int HoldVoucher(string voucherNo, int Userid)
        {
            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("USP_HoldVoucher '" + voucherNo + "'," + Userid + "");

            return i;
        }

        public int ClearVoucher(string voucherNo, int Userid)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("USP_ClearVoucher '" + voucherNo + "'," + Userid + "");

            return i;
        }
        public DataTable getActivityMaxFuel(string Activity)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            dt = db.sub_GetDatatable("USP_GetActivityMaxFuel '" + Activity + "'");

            return dt;
        }
        public void UpdateVoucherFuelSlipPrint(string voucherNo, string AutoID)
        { HC.DBOperations db = new HC.DBOperations();

        db.sub_ExecuteNonQuery("USP_UpdateVoucherFuelPrint '" + voucherNo + "','" + AutoID + "'");
        }

        public void UpdateFuelSlipPrint(string voucherNo, string AutoID)
        { HC.DBOperations db = new HC.DBOperations();

        db.sub_ExecuteNonQuery("USP_UpdateFuelPrint '" + voucherNo + "','" + AutoID + "'");
        }
        /////end by aarti

        // Codes Start By Prashant
        public DataTable GetCountainerCount()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_GetContainerCountDetails");

            return ExpenseDt;
        }

        public DataTable GetTrailerMovementDetails(string Trailername, string INOut)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Vehicle_Status  '" + Trailername + "','" + INOut + "'");

            return dt;
        }
        public DataTable VoucherDetails(string FromDate, string ToDate)
        {
            DataTable VoucherDetaildt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherDetaildt = db.sub_GetDatatable("USP_VoucherDetailsSummary '" + FromDate + "','" + ToDate + "'");

            return VoucherDetaildt;
        }
        public DataTable GetSMPTList(int portID)
        {
            DataTable SMPTDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            SMPTDT = db.sub_GetDatatable("USP_SMPTDateFetchDetails " + portID + "");
            return SMPTDT;
        }
        public DataTable GetEmptyYardGateINSummary(string FromDate, string ToDate, string Search, string name)
        {
            DataTable YardGateDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            YardGateDt = db.sub_GetDatatable("Get_Sp_EyardInDetails '" + FromDate + "','" + ToDate + "','" + Search + "','" + name + "'");

            return YardGateDt;
        }

        public DataTable GetEmptyYardGateOutSummary(string FromDate, string ToDate, string Search, string name)
        {
            DataTable YardGateOutDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            YardGateOutDt = db.sub_GetDatatable("Get_Sp_EyardoUTDetails '" + FromDate + "','" + ToDate + "','" + Search + "','" + name + "'");

            return YardGateOutDt;
        }
        public DataTable GetExportEmptyYardGateOutSummary(string FromDate, string ToDate, string Search, string Search1)
        {
            DataTable YardGateOutDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            YardGateOutDt = db.sub_GetDatatable("USP_GET_INEXP_EMPTY '" + FromDate + "','" + ToDate + "','" + Search + "','" + Search1 + "'");

            return YardGateOutDt;
        }
        public DataTable GetExportLoadedGateInSummary(string FromDate, string ToDate, string Search, string Search1, string Search2)
        {
            DataTable YardGateOutDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            YardGateOutDt = db.sub_GetDatatable("USP_Loaded_OUT_SUMMARY '" + FromDate + "','" + ToDate + "','" + Search + "','" + Search1 + "','" + Search2 + "'");

            return YardGateOutDt;
        }

        public DataTable GetShippingORAccountname(string Name)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetShippingnameORAccounting '" + Name + "'");
            return dt;
        }
      
        public DataTable getMovementPendancyList()
        {
            DataTable MovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            MovementDL = db.sub_GetDatatable("UPComingShipement");
            return MovementDL;
        }
        public DataTable getLoadedContainerArrival(string SearchCriteria, string SearchText, DateTime FromDate, DateTime TODate)
        {
            DataTable ContainerArrivalDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ContainerArrivalDL = db.sub_GetDatatable("SP_GetImportArrivalAllDetails '" + SearchCriteria + "','" + SearchText + "','" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(TODate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            return ContainerArrivalDL;
        }
        public DataTable getIGMListDetails(string IGMnumber, string SearchType)
        {
            DataTable ContainerArrivalDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ContainerArrivalDL = db.sub_GetDatatable("USP_IGM_DETAILS '" + IGMnumber + "','" + SearchType +"'");
            return ContainerArrivalDL;
        }
        public DataTable getPendingContainerAgainzeroJo()
        {
            DataTable PendingContainerDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PendingContainerDL = db.sub_GetDatatable("Sp_UpdateJODateList ");
            return PendingContainerDL;
        }
        public DataTable getPendingContainerForEmptyoffloading()
        {
            DataTable PendingContainerEmptyDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PendingContainerEmptyDL = db.sub_GetDatatable("USP_ListPendingoffloading ");
            return PendingContainerEmptyDL;
        }

        public DataTable CheckTrailerNumber(string trailernumber)
        {
            DataTable TrailerNumberDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TrailerNumberDL = db.sub_GetDatatable("USP_CheckTrailerNumber '" + trailernumber + "'");
            return TrailerNumberDL;
        }

        public DataTable GetTrailerSummarydetails(string vehicletype)
        {
            DataTable TrailerSummaryDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TrailerSummaryDL = db.sub_GetDatatable("USP_TrailersFetchDetails '" + vehicletype + "'");
            return TrailerSummaryDL;
        }
        public DataTable CheckHorseNumber(string HorseNumber)
        {
            DataTable HorseNumberDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            HorseNumberDL = db.sub_GetDatatable("USP_CheckHorseNumber '" + HorseNumber + "'");
            return HorseNumberDL;
        }
        public DataTable GetConsigneeDetails(string FromDate, string ToDate)
        {
            DataTable ConsigneeDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ConsigneeDL = db.sub_GetDatatable("USP_CONSIGNEE_WISE_REPORT '" + FromDate + "','" + ToDate + "'");
            return ConsigneeDL;
        }


        public DataSet GetSearchOnItemNumber(string ItemNo, string BLNumber)
        {
            DataSet ConsigneeDL = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            ConsigneeDL = db.sub_GetDataSets("sp_SearchIGMItem_New '" + ItemNo + "','" + BLNumber + "'");
            return ConsigneeDL;
        }


        public DataTable GetSearchOnItemNumberInvoiceDetails(string ItemNo, string BLNumber)
        {
            DataTable invoicedetailsDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            invoicedetailsDL = db.sub_GetDatatable("USP_ItemSearchInvoiceDetails '" + ItemNo + "','" + BLNumber + "'");
            return invoicedetailsDL;
        }


        public DataTable GetSearchOnItemNumberSummarydetails(string ItemNo, string BLNumber)
        {
            DataTable invoicedetailsDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            invoicedetailsDL = db.sub_GetDatatable("USP_IGM_ITEM_WISE_SEARCH '" + ItemNo + "','" + BLNumber + "'");
            return invoicedetailsDL;
        }
        public DataTable GetImportSearchDetails(string Containerno, string Jono)
        {
            DataTable ImportsearchDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchDL = db.sub_GetDatatable("get_sp_ImportSearchNew '" + Containerno + "','" + Jono + "'");
            return ImportsearchDL;
        }
        public DataTable GetTimeLine(string ContainerNo, string Jono)
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_IMP_TimeLine_Dets'" + Jono + "','" + ContainerNo + "'");

            return TPCloseDt;
        }

        public DataTable GetTimeLineExport(string ContainerNo, string Jono)
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("usp_Export_TimeLine'" + ContainerNo + "','" + Jono + "'");

            return TPCloseDt;
        }

        public DataTable GetTimeLineContainer(string ContainerNo, string Jono)
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_ContainerSreach_Time_Line'" + ContainerNo + "','" + Jono + "'");

            return TPCloseDt;
        }

        public DataTable GetImportSearchDetailsList(string Jono, string Containerno)
        {
            DataTable ImportsearchListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchListDL = db.sub_GetDatatable("sp_GetIGMDetailsOn_JOConts2 '" + Jono + "','" + Containerno + "'");
            return ImportsearchListDL;
        }


        public DataTable GetImportSearchHoldingDetails(string Jono, string Containerno)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("usp_Holddet '" + Jono + "','" + Containerno + "'");
            return ImportsearchHoldingListDL;
        }
        public DataTable getjono(string Containerno)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_GetJoNoList '" + Containerno + "'");
            return ImportsearchHoldingListDL;
        }
        public DataTable CheckUserValidation(string DriverName)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_DriverMasterCheckUser '" + DriverName + "'");
            return ImportsearchHoldingListDL;
        }
        public DataTable InsertDrivertion(string Driection)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_InsertDirection '" + Driection + "'");
            return ImportsearchHoldingListDL;
        }
        public DataTable InsertLocationGroup(string Driection)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_InsertLocation '" + Driection + "'");
            return ImportsearchHoldingListDL;
        }

        public DataTable SaveDriverDetails(string DriverName, bool IsActive, long DriverID, int addedby, string BankName, string BankID, string AccountNo, string IFSCCode, string AccountName, string BranchName, string PaymentMode, int TranspoterID
        , string DlNo, string DlType, string DlDate,
        string DLExpityDate, string EquipmentType, string DContactNo, string Insuranceno, string InsuranceCompany, string Referenceby,
        string ReferenceContactno, string CurrentAddress, string PermanentAddress, string chkIsBlackList,
        string TxtRemakrs, string Vehicletype, string Employetype, string AdharNumber, string Pannumber, string Drivertype,string DOBDATE)
        {
            DataTable DriverJODL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverJODL = db.sub_GetDatatable("USP_INSERT_DRIVER '" + DriverName.Replace("'", "''") + "','" + IsActive + "','" + DriverID + "'," + addedby + ",'" + BankName.Replace("'", "''") + "','" + BankID + "','" + AccountNo + "','" + IFSCCode + "','" + AccountName + "','" + BranchName.Replace("'", "''") + "','" + PaymentMode + "'," + TranspoterID + "" +
            ",'" + DlNo + "','" + DlType + "','" + DlDate + "','" + DLExpityDate + "','" + EquipmentType + "','" + DContactNo + "','" + Insuranceno + "','" + InsuranceCompany + "','" + Referenceby + "','" + ReferenceContactno + "'" +
            ",'" + CurrentAddress + "','" + PermanentAddress + "','" + chkIsBlackList + "','" + TxtRemakrs + "','" + Vehicletype + "','" + Employetype + "','" + AdharNumber + "','" + Pannumber + "','" + Drivertype + "','" + DOBDATE + "'");
            return DriverJODL;
        }

        public DataTable DriverDetails()
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_driverSummaryDetails ");
            return DriverListDL;
        }

        public DataTable GetDriverDetails(string Fromdate, string todate, int transpoterid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetPaymentDetails '" + Convert.ToDateTime(Fromdate).ToString("yyyy-MM-dd HH:mm:ss") + "' ,'" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm:ss") + "'," + transpoterid + "");

            return dt;
        }
        public DataTable GetDriverPaymentHoldDetails(string drivercode)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetDriverList '" + drivercode + "'");

            return dt;
        }


        public DataTable Driverholidingid(string driverid, int userid, string Remakrs)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_InsertholdingDriverDetails '" + driverid + "'," + userid + ",'" + Remakrs + "'");

            return dt;
        }
        public DataTable GetDriverHoldDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetDriverHoldDetails");

            return dt;
        }


        public DataTable getdrivername(string trailername)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_getdrivername '" + trailername + "'");

            return dt;
        }


        public DataTable SaveTPEntryDetails(string tpdate, int Trailerid, int Driverid,  string amount, int userid, string TPlocation, string TPfor)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_InsertTPDetails '" + tpdate + "'," + Trailerid + ",'" + Driverid + "','" + amount + "'," + userid + ",'" + TPlocation + "','" + TPfor + "'");
            return dt;
        }

        public DataTable GetTPDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_TPSummaryDetails");

            return dt;
        }
        public DataTable GetPrintTPDetails(int Trailerno,int tpNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_PrintTPSummaryDetails " + Trailerno + "," + tpNo + "");

            return dt;
        }
        public DataTable GetContainerType()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetContainerType");

            return dt;
        }

        public DataTable GetContainerDetials(string Containerno)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_ContainerFetchDetails '" + Containerno + "'");

            return dt;
        }


        public DataTable SaveContainerDetails(string Containerno, int jono, string Examine, int userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_InsertIRMSContainer '" + Containerno + "','" + jono + "','" + Examine + "'," + userid + "");

            return dt;
        }

        public DataTable TPApproveSaveDetails(int trailerid, int userid)
        {
            DataTable Approvedt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            Approvedt = db.sub_GetDatatable("USP_InsertTPApproveDetails " + trailerid + "," + userid + "");

            return Approvedt;
        }

        public DataTable GetTPApprovedDetails()
        {
            DataTable PrintDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            PrintDt = db.sub_GetDatatable("USP_TPPrintSummaryDetails ");

            return PrintDt;
        }


        public DataTable GetExpensesDropDownList()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_ExpensesDropDownList ");

            return ExpenseDt;
        }


        

       
        public DataTable GetCustomExamineValidate(int jono, string Containerno)
        {
            DataTable CustomExamineDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CustomExamineDL = db.sub_GetDatatable("USP_CheckCustomExamineDetails " + jono + ",'" + Containerno + "'");

            return CustomExamineDL;
        }


        public DataTable GetCustomFetchDetails()
        {
            DataTable CustomExamineDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CustomExamineDL = db.sub_GetDatatable("USP_CustomSummaryFetchDetails");

            return CustomExamineDL;
        }
        public DataTable checktrailerpermitexits(string trailername)
        {
            DataTable Approvedt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            Approvedt = db.sub_GetDatatable("USP_CheckTrailerPermitDetails '" + trailername + "'");

            return Approvedt;
        }
        public DataTable SaveTransportExpenses(string ExpensesDate, string ExpensesFor, string Amount, string Remark, int userid, string Voucherno)
        {
            DataTable TransExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TransExpenseDt = db.sub_GetDatatable("USP_InsertTransportExpensesdetails '" + ExpensesDate + "','" + ExpensesFor + "','" + Amount + "','" + Remark.Replace("'","''") + "'," + userid + ",'" + Voucherno + "'");

            return TransExpenseDt;
        }




        public DataTable GetTransportExpenses(string FromDate, string ToDate, int Userid)
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_TransportExpensesFetchDetails '" + FromDate + "','" + ToDate + "','" + Userid + "'");

            return ExpenseDt;
        }

        public DataTable GetVoucherNumer(string Containerno)
        {
            DataTable VoucherNumereDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherNumereDL = db.sub_GetDatatable("USP_GETVoucherNumber '" + Containerno + "'");

            return VoucherNumereDL;
        }

        public DataTable SaveExpenseName(string Expensename)
        {
            DataTable TransExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TransExpenseDt = db.sub_GetDatatable("USP_INSERT_TRANSEXPENSES '" + Expensename + "'");

            return TransExpenseDt;
        }

        public DataTable CheckExpenseFor(string Voucherno, int ExpenseFordata, string ContainerNo, string Type)
        {
            DataTable VoucherNumereDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherNumereDL = db.sub_GetDatatable("USP_CheckTransportExpense '" + Voucherno + "','" + ExpenseFordata + "','" + ContainerNo + "','" + Type + "'");

            return VoucherNumereDL;
        }
        //public DataTable getActivity()
        //{
        //    DataTable dt = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
        //    dt = db.sub_GetDatatable("USP_ActivityMaster");

        //    return dt;
        //}
        public DataTable SaveVehicleInOutDetails(string ActivityDate, int Activity, int Fromlocation, int tolocation, string trailerid, string party, string remarks, int addedby, string Containercount, string INout, string ContainerNo1, string ContainerNo2, string Activitycycle, string ContainerType)
        {
            DataTable VehicleInOutDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VehicleInOutDt = db.sub_GetDatatable("USP_INSERT_VEHICLE_IN_OUT_DETS '" + ActivityDate + "'," + Activity + "," + Fromlocation + "," + tolocation + ",'" + trailerid + "','" + party + "','" + remarks.Replace("'", "''") + "'," + addedby + ",'" + Containercount + "','" + INout + "','" + ContainerNo1.Replace("'", "''") + "','" + ContainerNo2.Replace("'", "''") + "','" + Activitycycle + "','" + ContainerType + "'");

            return VehicleInOutDt;
        }

        public DataTable SaveAdditinalmovmentrequest(string Containerno, string VoucherNo, int activity, int FromLocation, int Tolocation, string remarks, string Fuel,string kilometer, int userid)
        {
            DataTable VehicleInOutDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VehicleInOutDt = db.sub_GetDatatable("USP_INSERT_FUEL_ADDITIONAL_MOV_REQUEST '" + Containerno + "','" + VoucherNo + "'," + activity + "," + FromLocation + "," + Tolocation + ",'" + remarks.Replace("'","''") + "','"+ Fuel + "','"+ kilometer + "'," + userid + "");

            return VehicleInOutDt;
        }

        public DataTable GetAdditionalmovmentDetails()
        {
            DataTable AdditionalmovmentDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            AdditionalmovmentDL = db.sub_GetDatatable("USP_FuelAdditionalMovementResuestDetails");

            return AdditionalmovmentDL;
        }
        public DataTable ApproveRequest(int RequestID, string fuel, int UserID)
        {
            DataTable VehicleInOutDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VehicleInOutDt = db.sub_GetDatatable("USP_ApproveAdditionalMovementRequest '" + RequestID + "','"+ fuel + "'," + UserID + "");

            return VehicleInOutDt;
        }

        public DataTable getVehicleActivity()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_VehicleActivityMaster");

            return dt;
        }
        public DataTable getCheckVoucherno(string Voucherno)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_CheckVoucherno '"+ Voucherno + "'");

            return dt;
        }
        public DataTable GETDieselSlipDetails(string FromDate, string ToDate)
        {
            DataTable VoucherDetaildt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherDetaildt = db.sub_GetDatatable("USP_DieselSlipSummaryDetails '" + FromDate + "','" + ToDate + "'");

            return VoucherDetaildt;
        }


        public DataTable GetTPCloseDetailsList()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_TPDetailsSummary ");

            return TPCloseDt;
        }

        public DataTable GetTPHistory(int trailerid)
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_GetTPHistory "+ trailerid + "");

            return TPCloseDt;
        }
        public DataTable GetAttachmentdetails(int id)
        {
            DataTable CompanyTicketDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CompanyTicketDL = db.sub_GetDatatable("USP_TPCloseAttachmentFetchDetails " + id + "");
            return CompanyTicketDL;
        }
        public DataTable GetTranspotername()
        {
            DataTable Transpotername = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            Transpotername = db.sub_GetDatatable("USP_TranspoterDashboardFetchDetails ");

            return Transpotername;
        }
        public DataTable GetDriversTotalCount()
        {
            DataTable DriverCount = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            DriverCount = db.sub_GetDatatable("USP_GetDriverCount ");

            return DriverCount;
        }
        public DataTable GetTrailersTotalCount()
        {
            DataTable trailerscount = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            trailerscount = db.sub_GetDatatable("USP_GetTrailerCount ");

            return trailerscount;
        }


        public DataTable GetTrailerlocation()
        {
            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            trailerLocation = db.sub_GetDatatable("USP_Trailer_Location ");

            return trailerLocation;
        }
        public DataTable GetTrailerlocationttoalCount()
        {
            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            trailerLocation = db.sub_GetDatatable("USP_Trailer_Location_Count ");

            return trailerLocation;
        }
        public DataTable GetTrailerlocationICDTUMB()
        {
            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            trailerLocation = db.sub_GetDatatable("USP_Trailer_Location_ICDTUMB ");

            return trailerLocation;
        }

        public DataTable GetLocationTrailerList(int locationid)
        {
            DataTable TrailerLIST = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TrailerLIST = db.sub_GetDatatable("USP_Trailer_Location_AT "+ locationid + "");

            return TrailerLIST;
        }

        //public DataTable GetExportOutReport(string Customerstring ,string FromDate, string ToDate)
        //{
        //    DataTable dt = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    dt = db.sub_GetDatatable("Sp_ExpOutReport '" + Customerstring + "','"+ FromDate + "','"+ ToDate + "'");
        //    return dt;
        //}

        public DataTable GetImportEmpty()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_IMPORTEMPTY ");

            return TPCloseDt;
        }

        public DataTable GetImportLoaded()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_IMPORT_LOADED ");

            return TPCloseDt;
        }
        public DataTable GetExportEmpty()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_ExportEmpty ");

            return TPCloseDt;
        }

        public DataTable GetExportLoaded()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_EXPORT_LOADED ");

            return TPCloseDt;
        }
        public DataTable GetDailyMovment()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_CFS_IMP_ARRIVAL_DMR ");

            return TPCloseDt;
        }
        public DataTable GetJobOrderDetails(int salesid,string fromdate,string todate)
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_SALES_PERSON_WISE_JO_DETS "+ salesid + ",'" + fromdate + "','"+ todate + "'");

            return TPCloseDt;
        }
        public DataTable GetCustomerJobOrderDetails(string fromdate, string todate)
        {
            DataTable CustomerDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CustomerDT = db.sub_GetDatatable("USP_Customer_Wise_Report '" + fromdate + "','" + todate + "'");

            return CustomerDT;
        }
        public DataTable GetSalesWiseJObOrderdetails(string fromdate, string todate)
        {
            DataTable SaleDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            SaleDT = db.sub_GetDatatable("USP_SALES_WISE_JOB_ORDER '" + fromdate + "','" + todate + "'");

            return SaleDT;
        }
        public DataTable SaveVehicleReporting(string Reprtingdate, string Trailerno, int Driverid, string Contactno, string Remakes,int userid)
        {
            DataTable VehicleDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VehicleDL = db.sub_GetDatatable("USP_INSERT_VEHICLE_REPORTING_DETS '" + Reprtingdate + "','"+ Trailerno + "',"+ Driverid + ",'"+ Contactno + "','"+ Remakes + "',"+ userid + "");
            return VehicleDL;
        }
        public DataTable GetVehicleReportingDetails()
        {
            DataTable VehicleDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VehicleDL = db.sub_GetDatatable("USP_VehicleReportingFetchSummary");
            return VehicleDL;
        }
        public DataTable GetDOValidityExpired()
        {
            DataTable DOValidityDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DOValidityDL = db.sub_GetDatatable("USP_DO_VALIDITY_EXPIRED");
            return DOValidityDL;
        }
        public DataTable GetDOValidityExpiringinSevenDays()
        {
            DataTable DOValiditySevenDaysDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DOValiditySevenDaysDL = db.sub_GetDatatable("USP_DO_ValidityExpiring_Seven_Days");
            return DOValiditySevenDaysDL;
        }

        public DataTable GetPipeLIneReports()
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_MIS_IMPORT_PIPELINE_TUES_REPORT " + DateTime.Now.Month + "," + DateTime.Now.Year);
            return DT;
        }
        public DataTable AjaxGetPipeLIneReports(string Month)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_MIS_IMPORT_PIPELINE_TUES_REPORT " + Month + "," + DateTime.Now.Year);
            return DT;
        }

        public DataTable GetReadyVehicleReports()
        {
            DataTable ReadyVehicleDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ReadyVehicleDL = db.sub_GetDatatable("USP_READY_VEHICLES_FOR_MOVEMENT");
            return ReadyVehicleDL;
        }

        public DataTable VehicleSaveLoadingPlan(string containerNo, string trailerno, long JONO, int Userid, string TOdegistaton, string Size)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_AddVehicleReporting  '" + containerNo + "','" + trailerno + "'," + JONO + "," + Userid + ",'"+ TOdegistaton + "','"+ Size + "'");

            return dt;
        }
        public DataTable GetVehicleReportingPrintDetails(string containerNo, string TrailerNo, long JONO)
        {
            DataTable ReadyVehicleDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ReadyVehicleDL = db.sub_GetDatatable("USP_GetVehicleReportingPrint '"+ containerNo + "','"+ TrailerNo + "','"+ JONO + "'");
            return ReadyVehicleDL;
        }

        public DataTable GetInvenotyCountDetails(string Activity,int slid,int size)
        {
            DataTable GetInventoryDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            GetInventoryDL = db.sub_GetDatatable("USP_Inventory_Count '" + Activity + "'," + slid + "," + size + "");
            return GetInventoryDL;
        }

        public DataTable GetUpComingVehicleStatus()
        {
            DataTable ReadyVehicleDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ReadyVehicleDL = db.sub_GetDatatable("USP_UPCOMMING_VEHICLES_STATUS");
            return ReadyVehicleDL;
        }
        public DataTable GetCustomerWiseImportInventory(string salesid,string date)
        {
            DataTable ReadyVehicleDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ReadyVehicleDL = db.sub_GetDatatable("USP_IMP_CUST_WISE_LDD_INVENTORY '"+ salesid + "','" + Convert.ToDateTime(date).ToString("yyyyMMdd") + "'");
            return ReadyVehicleDL;
        }
        public DataTable GetSalesWiseImportInventory(string salesid, string date)
        {
            DataTable ReadyVehicleDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ReadyVehicleDL = db.sub_GetDatatable("USP_IMP_SALES_PERSON_WISE_LDD_INVENTORY '" + salesid + "','" + Convert.ToDateTime(date).ToString("yyyyMMdd") + "'");
            return ReadyVehicleDL;
        }
        public DataTable GePortWisePendency()
        {
            DataTable ReadyVehicleDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ReadyVehicleDL = db.sub_GetDatatable("USP_IMP_PORT_WISE_PENDENCY");
            return ReadyVehicleDL;
        }
        public DataTable GetVoucherEditDetails(string Voucherno)
        {
            DataTable VoucherDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VoucherDL = db.sub_GetDatatable("TransportEditVoucherSummary '" + Voucherno + "'");
            return VoucherDL;
        }
        //public int UpdateVoucherdetails(string VoucherNo, string Fuel, string AountBank, string AmountCash, int Userid)
        //{

        //    HC.DBOperations db = new HC.DBOperations();
        //    int i;
        //    i = db.sub_ExecuteNonQuery("USP_UpdateVehicleDetails '" + VoucherNo + "','" + Fuel + "','"+ AountBank + "','"+ AmountCash + "','"+ Userid + "'");

        //    return i;
        //}
        public DataTable GetimportEmptyPortMovement()
        {
            DataTable PortMovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PortMovementDL = db.sub_GetDatatable("Import_PortWise_movement_tues_report");
            return PortMovementDL;
        }
        public DataTable GetExportEmptyPortMovement(string fromdate, string todate)
        {
            DataTable PortMovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PortMovementDL = db.sub_GetDatatable("Export_PortWise_movement_tues_report '" + Convert.ToDateTime(fromdate).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(todate).ToString("yyyyMMdd") + "'");
            return PortMovementDL;
        }
        public DataTable GetHaziraEmptyPortMovement(string fromdate, string todate)
        {
            DataTable PortMovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PortMovementDL = db.sub_GetDatatable("EmptyRepoPort_hazira_movement_tues_report '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:MM") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:MM") + "'");
            return PortMovementDL;
        }
        public DataTable GetRevenueOpeations(string fromdate, string todate)
        {
            DataTable PortMovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PortMovementDL = db.sub_GetDatatable("SP_DayBookStatement '" + fromdate + "','" + todate + "'");
            return PortMovementDL;
        }

        public DataTable GetVehicleNO()
        {
            DataTable PortMovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PortMovementDL = db.sub_GetDatatable("USP_VOucherNoDROPDownLIst");
            return PortMovementDL;
        }
        public DataTable GetSlipNO()
        {
            DataTable PortMovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PortMovementDL = db.sub_GetDatatable("USP_SLipNoDROPDownLIst");
            return PortMovementDL;
        }
        public DataTable GetTrailerNo()
        {
            DataTable PortMovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PortMovementDL = db.sub_GetDatatable("USP_TrailerNoDropDownLIst");
            return PortMovementDL;
        }
        public DataTable GetTPNO()
        {
            DataTable PortMovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PortMovementDL = db.sub_GetDatatable("USP_TPNoDropDownLIst");
            return PortMovementDL;
        }
        public DataTable MovementDetails(string VoucherNo, string SlipNo, string Containerno, string TrailerNo)
        {
            DataTable VoucherDetaildt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherDetaildt = db.sub_GetDatatable("USP_VOUCHER_SEARCH '" + VoucherNo + "','" + SlipNo + "','"+ Containerno + "','"+ TrailerNo + "'");

            return VoucherDetaildt;
        }
        public DataTable gettrailerMovementDetails(string TrailerNo, string fromdate, string Todate)
        {
            DataTable VoucherDetaildt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherDetaildt = db.sub_GetDatatable("USP_VOUCHER_SEARCH_by_TrailerNo '" + TrailerNo + "','" + fromdate + "','" + Todate + "'");

            return VoucherDetaildt;
        }

        public DataTable GetVoucherTPCloseDetailsList(string TpNumber)
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_VoucherSearchTPDetailsSummary '"+ TpNumber + "'");

            return TPCloseDt;
        }
        public DataTable GetTrailerTPCloseDetailsList(string Trailerno)
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_trailerSearchTPDetailsSummary '" + Trailerno + "'");

            return TPCloseDt;
        }
        public DataTable GetMonthlyReport()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_Month_wise_imp_collections_dets");

            return TPCloseDt;
        }
        public DataTable GetIMportInHand()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_IMP_JO_IN_HAND");

            return TPCloseDt;
        }

        public DataTable GetImPortDelivery()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_CfS_IMP_loaded_del_dmr");

            return TPCloseDt;
        }

        public DataTable GetDestuffDelivery()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TPCloseDt = db.sub_GetDatatable("USP_CfS_IMP_destuff_del_dmr");

            return TPCloseDt;
        }
        public DataTable GetImportInventory()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TPCloseDt = db.sub_GetDatatable("usp_imp_inventory");

            return TPCloseDt;
        }

        public DataTable GetICDImportArrival()
        {
            DataTable ICDImportDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDImportDt = db.sub_GetDatatable("USP_CFS_Export_ARRIVAL_DMR");

            return ICDImportDt;
        }

        public DataTable GetICDImportExportStuffed()
        {
            DataTable ICDImportDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDImportDt = db.sub_GetDatatable("USP_CFS_Export_STUFFED_DMR");

            return ICDImportDt;
        }

        public DataTable GetICDIExportMovement()
        {
            DataTable ICDMovementDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDMovementDt = db.sub_GetDatatable("USP_CFS_Export_MOVEMENT_DMR");

            return ICDMovementDt;
        }

        public DataTable GetICDIExportInventory()
        {
            DataTable ICDMovementDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDMovementDt = db.sub_GetDatatable("USP_EXPORT_INVENTORY_DMR");

            return ICDMovementDt;
        }

        public DataTable GetICDIExportEmptyOut()
        {
            DataTable ICDMovementDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDMovementDt = db.sub_GetDatatable("USP_CFS_Export_EMPTY_OUT_DMR");

            return ICDMovementDt;
        }

       

       
        public DataTable GetExisitingLocationame()
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_Get_location_G");
            return DT;
        }

        public DataTable GetExisitingDistanceame()
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_Distance_G");
            return DT;
        }
        public DataTable GetLocationDetailsforedit(string LocationID)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_GetLocationDetails '" + LocationID + "'");
            return DT;
        }



        public DataTable DocketgetCategorywiseInvoice(string Date, string Category, string FromDate, string BasedOn)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_CategoryWiseShowDocketNumber '" + Convert.ToDateTime(Date).ToString("yyyy-MM-dd HH:mm") + "','" + Category + "','" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + BasedOn + "'");

            return dt;
        }

        public DataTable GetLrWiseContainerno(string LRNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("usp_LrWiseContainerno '" + LRNo + "'");

            return dt;
        }

        public DataTable GetCouriorAddress(int Courioird)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_getCouriorAddress " + Courioird + "");

            return dt;
        }
        public DataTable GetExisitingAccountName(string Name, Int64 id)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("get_sp_table 'Import_accountmaster','AccountName,AccountId','','AccountName'");
            return DT;
        }
        public DataTable GetExisitingSlnameName(string Name, Int64 id)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("get_sp_table 'ShipLines','SLName,SlID','','SLName'");
            return DT;
        }
        public DataTable GetExisitingInvoiceChangesRequest(string Name)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("get_sp_table 'Invoice_Summary','Category','','InvoiceCode '");
            return DT;
        }

        public DataTable GetExisitingInvoiceChangesValidate(string category, string AssessNo)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_Validate_Invoice_No'" + category + "','" + AssessNo + "'");
            return DT;
        }

        public DataTable getTrolleyInvoicelist()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("usp_invoice_change_summary ");
            return dt;
        }
        public DataTable Getactivitymaster(int activityid,string activitytype )
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetActivityCycle '"+ activityid + "','"+ activitytype + "'");
            return dt;
        }

        public DataTable ReGetExpencePrint(string entryid)
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_TransportExpensesRePrintFetchDetails '" + entryid + "'");

            return ExpenseDt;
        }

        public DataTable GetExpencePrint()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_TransportExpensesPrintFetchDetails");

            return ExpenseDt;
        }

        public DataTable GetDriverNo(string Driverno)
        {
            DataTable TrailerNoDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TrailerNoDL = db.sub_GetDatatable("USP_DriverNumberFetch '" + Driverno + "'");
            return TrailerNoDL;
        }
        //Codes End By Prashant

        //by rahul
        public DataSet ImportFinalOutFillDropdown()
        {
            DataSet IFODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            IFODS = db.sub_GetDataSets("USP_FILL_DROPDOWN_IMPORT_FINAL_OUT_SUMMARY");
            return IFODS;
        }
        public DataTable ImportFinalOut(DateTime FromDate, DateTime ToDate, string DeliveryType, string For, string PortID, string LineID)
        {
            DataTable IFODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            IFODT = db.sub_GetDatatable("sp_Import_Out_Dets '" + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy HH:mm") + "','" + DeliveryType + "','" + For + "','" + PortID + "','" + LineID + "'");
            return IFODT;
        }


        //public DataTable UpdateTrailerDetails(int trailerid, string ownedby, string Transportername, string passing, string grade, string modelno, string chasisno, string engine, string trailertype, string permit, string permittype, int userid, int EditchkIsActive, string EditVehicleTYpe, string EditIsshifting, string EditVehicleGroupType, string PolicyDate, string GreenTax, string PucDate, string UsedFor, string Taxdate,
        //    string Fitnessdate, string EditVEhicleTypeID, string EditVendorName, string TrailerSize)
        //{
        //    DataTable TrailerupdateDL = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    TrailerupdateDL = db.sub_GetDatatable("USP_UpdateTrailerDetails " + trailerid + ",'" + ownedby + "','" + Transportername + "','" + passing + "','" + grade + "','" + modelno + "','" + chasisno + "','" + engine + "','" + trailertype + "','" + permit + "','" + Convert.ToDateTime(permittype).ToString("dd-MMM-yyyy HH:mm") + "'," + userid + "," + EditchkIsActive + ",'" + EditVehicleTYpe + "','" + EditIsshifting + "','" + EditVehicleGroupType + "','" + PolicyDate + "','" + GreenTax + "','" + PucDate + "','" + UsedFor + "','" + Taxdate + "','" + Fitnessdate + "','" + EditVEhicleTypeID + "','" + EditVendorName + "','" + TrailerSize + "'");
        //    return TrailerupdateDL;
        //}
        public DataTable GetPendingFactoryReturn()
        {
            DataTable pendingDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            pendingDT = db.sub_GetDatatable("USP_IMP_Pending_Ctrs_Factory_Return");
            return pendingDT;
        }


        public DataTable GetTrainNumber()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetExportTrainNO");

            return dt;
        }


        public DataTable GetPortINdetails(string trainnumber)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_PortInDetails '" + trainnumber + "'");

            return dt;
        }
        public DataTable GetDeliveryAddressData(Int64 id)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GET_DELIVERY_ADDRESSES " + id + "");
            return LoginDT;
        }
        public DataTable GetDeliveryAdressWiseData(Int32 LocationID, Int64 Common_ID, Int64 ID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_LOCATION_WISE_DELIVERY_ADDRESS " + LocationID + "," + Common_ID + "," + ID + "");
            return LoginDT;
        }
        public DataTable GetDuplicateDeliveryAddressValidation(Int32 LocationID, string Address, Int64 Common_ID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_VALIDATION_DUPLICATE_DELIVERY_ADDRESSES " + LocationID + ",'" + Address.Replace("'", "''") + "'," + Common_ID + "");
            return LoginDT;
        }
        public DataTable SaveDeliveryAddresses(int LocationID, string Address, int userId, long common_id, long ID, int IsCopy)
        {
            DataTable DriverJODL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            int i = db.sub_ExecuteNonQuery("USP_INSERT_DELIVERY_ADDRESSES " + LocationID + ",'" + Address.Replace("'", "''") + "'," + userId + "," + common_id + "," + ID + "," + IsCopy + "");
            return DriverJODL;
        }

        public DataTable getPortPendencyList()
        {
            DataTable MovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            MovementDL = db.sub_GetDatatable("USP_IMPORT_PORT_PENDENCY");
            return MovementDL;
        }

        public DataTable GetTPCloseDetails()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("USP_TPCloseSummaryDetails ");

            return TPCloseDt;
        }


        public DataTable SaveTPCloseDetails(string TpNo, string startdate, string enddate,byte[] attachByte,string contentType)
        {
            DataTable Approvedt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            Approvedt = db.AddTPAttachment(TpNo,startdate,enddate,attachByte, contentType);

            return Approvedt;
        }
        public DataSet getDuplicateData(string Transporter, string Driver, string VoucherNo, string Amount, string VoucherFor)
        {
            DataSet PaymentDS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            PaymentDS = db.sub_GetDataSets("USP_GET_DUPLICATE_DRIVER_TRANSPORTER '" + Transporter + "','" + Driver + "','" + VoucherNo + "','" + Amount + "','" + VoucherFor + "'");
            return PaymentDS;
        }

        //Code Start by Rahul
        public DataTable getDriverSummary(string FromDate, string ToDate, string Voucher, string Driver)
        {
            DataTable DriverDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverDT = db.sub_GetDatatable("USP_DRIVER_PAYMENT_RECO_SUMMARY '" + FromDate + "','" + ToDate + "','" + Voucher + "','" + Driver + "'");
            return DriverDT;
        }
        public DataTable getDriverSummaryDropDown()
        {
            DataTable DriverDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverDT = db.sub_GetDatatable("USP_DRIVER_RECO_SUMMARY_DROPDOWN");
            return DriverDT;
        }
        public DataTable GetPendningAdditionalmovmentDetails()
        {
            DataTable AdditionalmovmentDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            AdditionalmovmentDL = db.sub_GetDatatable("USP_FuelPendingAdditionalMovementResuestDetails");

            return AdditionalmovmentDL;
        }
        public DataTable GenerateRequest(int RequestID, int UserID)
        {
            DataTable RequestDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            RequestDt = db.sub_GetDatatable("USP_GENERATE_ADDITIONAL_MOVEMENT_REQUEST '" + RequestID + "'," + UserID + "");
            return RequestDt;
        }
        //Code End by rahul
        //Codes By Manish

        public DataTable getLineWiseTuesReportList(string Activity, string date,string Yard)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations(); 
            dt = db.sub_GetDatatable("LINEWISE_TUES_REPORT_EMPTY_LOADED '" + Activity + "','" + Convert.ToDateTime(date).ToString("yyyyMMddHHmmss") + "','" + Yard + "'");
            return dt;
        }

        public DataTable getLineWiseReportList(string Activity, string FromDate,string ToDate, string Yard)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("getLineWiseSummary '" + Activity + "','" + FromDate + "','" + ToDate + "','" + Yard + "'");
            return dt;
        }

        public DataTable getPortWiseTuesReportList(string Activity)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("Port_Pendency_dayWise_BYRoad '" + Activity + "'");
            return dt;
        }


        public DataTable GetExporttoExcelDataCreditNote(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("TALLY_INTERFACE_INVOICE_CREDITNOTE_ALLnew '" + Date + "','" + Date + "','" + Category + "'");

            return dt;
        }

        public DataTable getCategorywiseCreditNote(string Date, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_CategoryWiseShowXML_creditNote '" + Date + "','" + Category + "'");

            return dt;
        }


        public DataTable getSalesRegisterrReportList(string Activity, string fDate, string Todate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_SalesRegister1 '" + Activity + "','" + fDate + "','" + Todate + "'");
            return dt;
        }

        public DataTable getEInvoiceRegisterrReportList(string Activity, string fDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("Sales_GST_ACK_List_Request '" + Activity + "','" + fDate + "'");
            return dt;
        }


        public DataTable getSalesRegisterrContainerWiseReportList(string Activity, string fDate, string Todate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SalesRegister_ContainerWise '" + Activity + "','" + fDate + "','" + Todate + "'");
            return dt;
        }

        public DataTable getExportStuffingPlanReportList(string AsOndate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("Sp_ExportStuffingPlan '" + AsOndate + "'");
            return dt;
        }

        public DataTable getEmptyBookingReportList(string FromDate, string ToDate, string category, string search)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("Sp_ExpEmptBookingReport '" + FromDate + "','" + ToDate + "','" + category + "','" + search + "'");
            return dt;
        }

        public DataTable getLorryReceiptReportList(string FromDate, string ToDate, string category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_ShowLorryReceiptReport '" + FromDate + "','" + ToDate + "','" + category + "'");
            return dt;
        }

        public DataTable getCustomerMaster()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_CustomerMaster");

            return dt;
        }
        public DataTable getBankMaster()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_BankMaster");

            return dt;
        }
        public DataTable getBankName()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_BankName");

            return dt;
        }

        public DataTable getExportEmptyOutReportList(string FromDate, string ToDate, string Customer)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("Sp_ExpOutReport '" + Customer + "','" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            return dt;
        }
        public DataTable getSalesRegisterContainerWiseHeadReportList(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_SalesRegister_Container_HeadWise_Report '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            return dt;
        }

        public DataTable GetExportOutReport(string Customerstring, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_ShowExportOutdetails '" + FromDate + "','" + ToDate + "','" + Customerstring + "'");
            return dt;
        }
        public DataTable GetVehicleReportingSummary()
        {
            DataTable VehicleDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VehicleDL = db.sub_GetDatatable("USP_READY_VEHICLES_FOR_REPORTING");
            return VehicleDL;
        }

        public DataTable SaveVehiclePendencySummary(string Reprtingdate, string Trailerno, string Driverid, string Contactno, string Remakes, int userid)
        {
            DataTable VehicleDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VehicleDL = db.sub_GetDatatable("USP_INSERT_VEHICLE_REPORTING_DETS_LIST '" + Reprtingdate + "','" + Trailerno + "','" + Driverid + "','" + Contactno + "','" + Remakes + "'," + userid + "");
            return VehicleDL;
        }

        public DataTable getFuelType()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_FuelType");

            return dt;
        }

        public DataTable getFuelVendorMaster()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_FuelVendorMaster");

            return dt;
        }

        public DataTable getCostCenter()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_FuelCostCenter");

            return dt;
        }

        public DataTable SaveInternalFuelStock(string txtentryDate, float txtFuelQty, int txtRate, int ddlVendorName, string trailerid, string ddlFuelType, int UserID, int ddlCostCenter, string CostCenterFor, string FuelFor)
        {
            DataTable SaveInternalFuelStock = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            SaveInternalFuelStock = db.sub_GetDatatable("InternalFuelStock '" + ddlFuelType + "','" + trailerid + "'," + ddlCostCenter + "," + txtFuelQty + ",'" + txtRate + "','" + ddlVendorName + "'," + UserID + ",'" + CostCenterFor + "','" + FuelFor + "'");

            return SaveInternalFuelStock;
        }

        public DataTable GetCmbFuelTypeOnChange(string Fueltype, int CostCenter, string ddlCostCenterFor)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_fuel_consumption_indexchange '" + Fueltype + "','" + CostCenter + "','" + ddlCostCenterFor + "'");

            return dt;
        }

        public DataTable AjaxgetFromReading(string trailername, string CostCenterFor, string FuelType)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_fuel_consumption_ReadingFrom '" + trailername + "','" + CostCenterFor + "','" + FuelType + "'");

            return dt;
        }


        public DataTable SaveFuelConsumption(string txtentryDate, int ddlCostCenter, int trailerid, double txtBalQty, string txtIssueQty,
            string ddlFuelType, int txtReadFrom, int txtReadingTo, int ddlDriverName, string txtRemark,
            string ddlUnit, int UserID, int ddlvehicleType, string CostCenterFor, string differencereading,
            string VehiclePurpose, string Vehiclesubgroup, string Stockslipno)
        {
            DataTable SaveInternalFuelStock = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            SaveInternalFuelStock = db.sub_GetDatatable("SP_Fuel_consumption '" + Convert.ToDateTime(txtentryDate).ToString("yyyy-MM-dd HH:mm") + "','" + ddlCostCenter + "'," + trailerid + ",'" + txtBalQty + "','" + txtIssueQty + "','" + ddlFuelType + "','" + txtReadFrom + "','" + txtReadingTo + "','" + ddlDriverName + "','" + UserID + "','" + txtRemark + "','" + ddlvehicleType + "','" + CostCenterFor + "','" + differencereading + "','" + VehiclePurpose + "','" + Vehiclesubgroup + "','"+ Stockslipno + "'");

            return SaveInternalFuelStock;
        }

        public DataTable PrintFuelConsumption(string costcenterfor)
        {
            DataTable PrintFuelConsumption = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PrintFuelConsumption = db.sub_GetDatatable("Print_fuelConsumptionNew '"+ costcenterfor + "'");
            return PrintFuelConsumption;
        }

        public DataTable GetFuelStockSummary(string cmbFueltype, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string costcenter = "";
            dt = db.sub_GetDatatable("SP_fuel_stock_summary '" + FromDate + "','" + ToDate + "','" + costcenter + "','" + cmbFueltype + "'");
            return dt;
        }
        //Code End By Manish
        public DataTable GetDriverPaymentCalendar()
        {
            DataTable DataDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DataDT = db.sub_GetDatatable("USP_DriverPaymentReportCalendarView");
            return DataDT;
        }
        public DataTable PrintInternalFuelStock(string FuelFor)
        {
            DataTable PrintInternalFuelStock = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PrintInternalFuelStock = db.sub_GetDatatable("Print_FuelStock '" + FuelFor + "'");
            return PrintInternalFuelStock;
        }

        public DataTable PrintInternalFuelStockReprint(string fuelregid, string FuelFor)
        {
            DataTable PrintInternalFuelStockReprint = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PrintInternalFuelStockReprint = db.sub_GetDatatable("Print_FuelStockREprint '" + fuelregid + "','" + FuelFor + "'");
            return PrintInternalFuelStockReprint;
        }
        public DataTable GetFuelStockSummary1(string cmbFueltype, string FromDate, string ToDate, string FuelFor)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string costcenter = "";
            dt = db.sub_GetDatatable("SP_fuel_stock_summary1 '" + FromDate + "','" + ToDate + "','" + costcenter + "','" + cmbFueltype + "','" + FuelFor + "'");
            return dt;
        }
        //public DataTable PrintFuelConsumption()
        //{
        //    DataTable PrintFuelConsumption = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    PrintFuelConsumption = db.sub_GetDatatable("Print_fuelConsumptionNew");
        //    return PrintFuelConsumption;
        //}

        public DataTable REPrintFuelConsumption(string id)
        {
            DataTable PrintFuelConsumption = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PrintFuelConsumption = db.sub_GetDatatable("REPrint_fuelConsumptionNew '" + id + "'");
            return PrintFuelConsumption;
        }

        public DataTable FuelConsumptionSummary(string txtsearch, string fromdate, string todate)
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("SP_Fill_Fuel_consumption '" + txtsearch + "','" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd HH:mm") + "'");

            return TPCloseDt;
        }


        public DataTable SaveICDInternalDriverName(string txtDriver)
        {
            DataTable TransExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TransExpenseDt = db.sub_GetDatatable("USP_INSERT_ICD_DRIVER_INTERNAL '" + txtDriver + "'");

            return TransExpenseDt;
        }

        public DataTable PrintVehicleInout()
        {
            DataTable PrintInternalFuelStock = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PrintInternalFuelStock = db.sub_GetDatatable("Print_Vehicle_IN_OUT_Dets");
            return PrintInternalFuelStock;
        }
        public DataTable GetVehicleStatus(string Trailername)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetTrailerStatusOpen_CLose '" + Trailername + "'");

            return dt;
        }
        public DataTable GetVehicleInOutLatestDetails(string Trailername)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_VehicleInOutLatestRecord_Wise_vehicle '" + Trailername + "'");

            return dt;
        }
        public DataTable GetVehicleInoutSummary(string Fromdate, string Todate)
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("Vehicle_IN_OUT_Dets_summary '" + Convert.ToDateTime(Fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd HH:mm") + "'");

            return TPCloseDt;
        }

        public DataTable REPrintVehicleInout(string Entryid)
        {
            DataTable PrintInternalFuelStock = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PrintInternalFuelStock = db.sub_GetDatatable("RePrint_Vehicle_IN_OUT_Dets '" + Entryid + "'");
            return PrintInternalFuelStock;
        }

        public DataTable getActivityMasterCreditNote()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_ActivityMasterCreditNote");

            return dt;
        }

        public DataTable getPartyMaster()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_PartyMaster");

            return dt;
        }

        public DataTable GetSearchInvoice(string category, string Search, string txtItemNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_InvoiceSearch'" + category + "','" + Search + "','" + txtItemNo + "'");

            return dt;
        }

        public DataTable getFuelCloseList()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_FuelCloseList");

            return dt;
        }

        public DataTable DocDropDownList(string TrailerNo)
        {
            DataTable ds = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //  dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan ");

            ds = db.sub_GetDatatable("USP_FuelCloseBindUpdateList '" + TrailerNo + "'");

            return ds;
        }
        public DataSet DocDropDownList(int LRNo)
        {
            DataSet ds = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            //  dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan ");

            ds = db.sub_GetDataSets("USP_LR_NO_DETAILS " + LRNo + "");

            return ds;
        }
        public DataTable GridViewBindDetailsNew(int IGMFileId)
        {
            DataTable MovementDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            MovementDL = db.sub_GetDatatable("Usp_joborder_GridViewBind " + IGMFileId);
            return MovementDL;
        }

        public DataTable GetLineWiseOpeningTuesReport(string Fromdate, string Todate)
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TPCloseDt = db.sub_GetDatatable("LineWise_Opening_and_Closing_tues_Report '" + Convert.ToDateTime(Fromdate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd HH:mm") + "'");

            return TPCloseDt;
        }

        public DataSet AddAuctionGenIntoTable(string JoNo, string IGMNo, string ItemNo,  string ImporterName,  string CargoDescription,  string Weight,  string NoOfPkgs,  string BLNo,  string BLDate,DataTable dtContainer,string NoticeType,string NoticeDate)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            DataSet dataSet = new DataSet();
            //string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("NoticeType", NoticeType);
            lstparam.Add("JoNo", JoNo);
            lstparam.Add("IGMNo", IGMNo);
            lstparam.Add("ItemNo", ItemNo);
            lstparam.Add("Consignee", ImporterName);
            lstparam.Add("Conadd", 0);
            lstparam.Add("VesselName", "");
            lstparam.Add("LineName", "");
            lstparam.Add("Cargodesc", CargoDescription);
            lstparam.Add("PrevAuctionID", 0);
            lstparam.Add("PrevAuctDate", "");
            lstparam.Add("FPrevAuctionID", 0);
            lstparam.Add("FPrevAuctDate", "");
            lstparam.Add("Weight", Weight);
            lstparam.Add("PKGType", "");
            lstparam.Add("Qty", NoOfPkgs);
            lstparam.Add("BLNo", BLNo);
            lstparam.Add("BLDate", BLDate);
            lstparam.Add("NoticeDate", NoticeDate);

            Dictionary<object, object> lstparam2 = new Dictionary<object, object>();
            lstparam2.Add("ContainerDTAuction", "@ContainerDTAuction");

            dataSet = db.AddAuctionData("Sp_SaveTempAuction", lstparam, dtContainer, lstparam2);
            
            return dataSet;

        }

        public DataSet AddAuctionGenAutoIntoTable(int UserId, string NoticeType)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            DataSet dataSet = new DataSet();
            //string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("user_id", UserId);
            lstparam.Add("noticeType", NoticeType);

            dataSet = db.AddAuctionAutonData("SP_GenerateAuctionNoticeAuto", lstparam);

            return dataSet;

        }

        public int AddAuctionGenStatus(string IGMNo, string ItemNo, string ContainerNo, int UserId, string Remarks, string ReferenceNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            int i = 0;
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("IGMNo", IGMNo);
            lstparam.Add("ItemNo", ItemNo);
            lstparam.Add("ContainerNo", ContainerNo);
            lstparam.Add("UserId", UserId);
            lstparam.Add("Remarks", Remarks);
            lstparam.Add("Reference", ReferenceNo);

            i = db.AddAuctionStatus("USP_updateAuctionStatus", lstparam);

            return i;

        }

        public int AddAuctionClearDetails(string IGMNo, string ItemNo, string ContainerNo, int UserId, string Value, string DutyValue,string ClearRemarks)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            int i = 0;
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("IGMNo", IGMNo);
            lstparam.Add("ItemNo", ItemNo);
            lstparam.Add("ContainerNo", ContainerNo);
            lstparam.Add("UserId", UserId);
            lstparam.Add("Value", Value);
            lstparam.Add("DutyValue", DutyValue);
            lstparam.Add("ClearRemarks", ClearRemarks);

            i = db.AddAuctionStatus("USP_updateAuctionDetails", lstparam);

            return i;

        }

        public DataTable DocDropDownListForUpload()
        {
            DataTable ds = new DataTable();
            ds = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //  dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan ");

            ds = db.sub_GetDatatable("USP_GetDocuemntTypeDETAILS");

            return ds;
        }

        //public DataTable GetContainerDocsDetails(string Containerno)
        //{
        //    DataTable dt = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
        //    dt = db.sub_GetDatatable("USP_GetContainerDocsDetails '" + Containerno + "'");

        //    return dt;
        //}

        public DataTable getCategoryListList()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("get_sp_table 'Category_M','*', '','name asc'");

            return dt;
        }

        public DataTable GetDownloadAttachment(string autoid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetContainerAttachmentDocsDetails '" + autoid + "'");

            return dt;
        }

        public DataSet getDBLocationDetails(string url)
        {
            DataSet DBLocationDT = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            DBLocationDT = db.sub_GetDataSets("UPS_GetDBLocationDetails'" + url + "'");
            return DBLocationDT;
        }

        public DataTable GetRemarks(string Prefix)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("GetAuctionRemarks '" + Prefix + "'");
            return DT;
        }

        public DataTable GetLocationDetails()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'ext_location_m', 'LocationID,Location','','Location'");
            return LoginDT;
        }

        public DataTable GetSizeDetails()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'ContainerSize', 'ContainerSizeID,ContainerSize','',''");
            return LoginDT;
        }


        public DataTable GetTypeDetails()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'ContainerType', 'ContainerTypeID,ContainerType','','ContainerType'");
            return LoginDT;
        }

        public DataTable GetVesselDetails()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Vessels', 'VesselID,VesselName','','VesselID' ");
            return LoginDT;
        }

        public DataTable GetshiplineDetails()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'ShipLines', 'SLID,SLName','','SLName' ");
            return LoginDT;
        }

        public DataTable GetPortsDetails()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Ports', 'PortID,PortName','','PortName' ");
            return LoginDT;
        }

        public DataTable GetTransportDetails()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'Transporter', 'TransID,TransName','','TransName' ");
            return LoginDT;
        }

        public DataTable GetActivitySearch(string ContainerNo ,int Jono)
        {
            DataTable ImportsearchDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchDL = db.sub_GetDatatable("usp_update_movement_External '" + ContainerNo + "','" + Jono+ "'");
            return ImportsearchDL;
        }

        public DataSet GetActivityDropDownTariffList()
        {
            DataSet ImportsearchDL = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchDL = db.sub_GetDataSets("getDetWorkTariff");
            return ImportsearchDL;
        }

        public DataSet getDropDownListExportWorkOrder()
        {
            DataSet ddWODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            ddWODS = db.sub_GetDataSets("USP_FILL_DROPDOWN_EXPORT_WORKORDER");
            return ddWODS;
        }

        public DataSet getDropDownListImportWorkOrder()
        {
            DataSet ddWODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            ddWODS = db.sub_GetDataSets("USP_FILL_DROPDOWN_IMPORT_WORKORDER");
            return ddWODS;
        }
        public DataSet getDropDownListImportWorkOrderssr()
        {
            DataSet ddWODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            ddWODS = db.sub_GetDataSets("USP_FILL_DROPDOWN_IMPORT_WORKORDERSSR");
            return ddWODS;
        }
        public DataTable GetSBData(string SBNO, string WorkType)
        {
            DataTable WODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            WODT = db.sub_GetDatatable("SP_SHOWSBNoWISEDETAILS '" + WorkType + "','" + SBNO + "'");
            return WODT;
        }

        public DataTable GetSBDataByInvoice(string Invoice, string WorkType)
        {
            DataTable WODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            WODT = db.sub_GetDatatable("SP_SHOWINVOICEWISEDETAILS '" + WorkType + "','" + Invoice + "'");
            return WODT;
        }

        public DataTable GetCartWeightData(string SBNO, string WorkType)
        {
            DataTable WODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            WODT = db.sub_GetDatatable("SP_SHOWCartPKGWT '" + WorkType + "','" + SBNO + "'");
            return WODT;
        }

        public DataTable GetCartWeightDataInvoice(string InvoiceNo, string WorkType)
        {
            DataTable WODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            WODT = db.sub_GetDatatable("SP_SHOWCartPKGWTInvoice '" + WorkType + "','" + InvoiceNo + "'");
            return WODT;
        }

        public DataTable GetVehicleData(string SBEntryID, string SBNO)
        {
            DataTable VehDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VehDT = db.sub_GetDatatable("USP_GET_VEHICLE_NOS_FROM_SBNO '" + SBEntryID + "','" + SBNO + "'");
            return VehDT;
        }
        public DataTable GetPkgData(string SBEntryID, string SBNO, string VehicleNo,string CartingId)
        {
            DataTable PKGDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PKGDT = db.sub_GetDatatable("USP_GET_PKGS_VEHICLE_WISE '" + SBEntryID + "','" + SBNO + "','" + VehicleNo + "','" + CartingId + "'");
            return PKGDT;
        }
        public DataTable GetOpenWOList()
        {
            DataTable WODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            WODT = db.sub_GetDatatable("USP_OPEN_WORK_ORDER_LIST");
            return WODT;
        }
        public DataTable GetOpenWOListImport()
        {
            DataTable WODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            WODT = db.sub_GetDatatable("USP_OPEN_WORK_ORDER_LIST_IMPORT");
            return WODT;
        }
        public DataTable GetContainerData(string SBEntryID, string SBNO)
        {
            DataTable ContDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ContDT = db.sub_GetDatatable("USP_GET_CONTAINER_NOS_FROM_SBNO '" + SBEntryID + "','" + SBNO + "'");
            return ContDT;
        }
        public DataTable GetConValData(string ContainerNo)
        {
            DataTable CDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CDT = db.sub_GetDatatable("USP_GET_CONTAINER_NOS_VALIDATION '" + ContainerNo + "'");
            return CDT;
        }
        public DataTable GetConValDataImport(string ContainerNo, string Category, string ddlWOType)
        {
            DataTable CDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CDT = db.sub_GetDatatable("USP_GET_CONTAINER_NOS_IMPORT '" + ContainerNo + "','" + Category + "','" + ddlWOType + "'");
            return CDT;
        }
        public DataTable GetTruckValDataImport(string TruckNo, string Category, string ddlWOType)
        {
            DataTable CDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CDT = db.sub_GetDatatable("USP_GET_TRUCK_NOS_IMPORT '" + TruckNo + "','" + Category + "','" + ddlWOType + "'");
            return CDT;
        }
        public DataTable GetBOEValDataImport(string TruckNo, string Category, string ddlWOType)
        {
            DataTable CDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CDT = db.sub_GetDatatable("USP_GET_TRUCK_NOS_IMPORT '" + TruckNo + "','" + Category + "','" + ddlWOType + "'");
            return CDT;
        }
        public DataSet GetPrintWODate(string WONo)
        {
            DataSet ddWODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            ddWODS = db.sub_GetDataSets("USP_WO_PRINT_DETAILS '" + WONo + "'");
            return ddWODS;
        }
        public DataSet GetVOucherUploadValidation(string Activity, string Fromlocation, string Tolocation)
        {
            DataSet PaymentDS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            PaymentDS = db.sub_GetDataSets("USP_VoucherTraiffUploadCheckValidation '" + Activity + "','" + Fromlocation + "','" + Tolocation + "'");
            return PaymentDS;
        }
        public DataTable DeleteDriverFile(long id, int userid)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_DeleteDriverFileData  " + id + "," + userid + "");
            return AttachmentDT;
        }
        public DataTable DriverApproveDetails(int driverid, string trailername, int Userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_ApproveDriver '" + driverid + "'," + trailername + "," + Userid + "");

            return dt;
        }

        public DataTable Employeetype()
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_GetEmployeeType");
            return DriverListDL;
        }

        public DataTable GetDriverApproveDetails()
        {
            DataTable ICDAhutOUtDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDAhutOUtDt = db.sub_GetDatatable("USP_TrailerDriverApprove");

            return ICDAhutOUtDt;
        }

        public DataTable GetDriverList(int LRNo)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_GetDriverDocumentDetails  " + LRNo + "");
            return AttachmentDT;
        }

        public DataTable GetDriverMaxID()
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_GetDriverMaxID");
            return AttachmentDT;
        }

        public DataTable DriverDlType()
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_GetDlType");
            return DriverListDL;
        }

        public DataTable GetDriverDocumentList()
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_GetDriverdocumenttype");
            return AttachmentDT;
        }

        public DataTable GetDriverDownloadList(int id, string id1)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_GetDriverDocumentDownloadDetails  " + id + "," + id1 + "");
            return AttachmentDT;
        }

        public DataTable GetEquipmentType()
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_GetEquipmentM");
            return DriverListDL;
        }

        public DataTable InsuranceCompany()
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_GetInsuranceCompanyMaster");
            return DriverListDL;
        }

        public DataTable AddAttachment(int Userid, byte[] bytes, string contentType, string fname, string root)
        {

            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.AddDriverAttachment(Userid, bytes, contentType, fname, root);
            return AttachmentDT;
        }

        public int AddBCNNocDetails(string NOCNo, string NOCDate, long SlId, string ShipperName, long OrginStationId, long CommodityId, long StuffingTypeId, long NoofWagonPlaned, string PlanedDate, string StuffingDate, string ETADate, long CreatedBy)
        {
            int LoginDT = new int();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_ExecuteNonQuery("USP_INSERT_BCNNOCUPDATION  '" + NOCNo + "','" + NOCDate + "','" + ShipperName + "','" + SlId + "','" + OrginStationId + "','" + CommodityId + "','" + StuffingTypeId + "', '" + NoofWagonPlaned + "','" + PlanedDate + "','" + StuffingDate + "','" + ETADate + "','" + CreatedBy + "'");
            return LoginDT;
        }
        public DataTable GetWOReportData(string FromDate, string ToDate, string Criteria, string SearchText)
        {
            DataTable CDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CDT = db.sub_GetDatatable("USP_WORK_ORDER_REPORT '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + Criteria + "','" + SearchText + "'");
            return CDT;
        }

        public int AddRRWagonDataIntoTable(long FileId, string TrainNo, long FromStation, long ToStation, long CommodityId, long PartyId, string WagonRRNo, DateTime ArrivalDate, double FreightAmount, long UserId,string WagonContRRNo)
        {
            int val = 0;
            HC.DBOperations db = new HC.DBOperations();
            val = db.AddDataRRDownloadSaveData(FileId, TrainNo, FromStation, ToStation, CommodityId, PartyId, WagonRRNo, ArrivalDate, FreightAmount, UserId, WagonContRRNo);
            return val;
        }

        public DataTable SaveFuelDirectSetting(int TranspoterID, string FromDate, string ToDate, int ActivityID, string InOut, int ContainerTypeID, string ContaierSize, int Fromlocation, int Tolocation, float Fuel, string Amount1, string Amount2, int RecordID, int Userid, string passing, string TrailerType, string Drivertype, string VehicleType, string ScanType, string Weight, string status, string TxtFuelamount)
        {
            DataTable VendorDataDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string str = "";
            str = "USP_FuelTariffSettingInsertDetails " + TranspoterID + " ,'" + FromDate + "' ,'" + ToDate + "' ," + ActivityID + " ,'" + InOut + "' ," + ContainerTypeID + " ,'" + ContaierSize + " '," + Fromlocation + "," + Tolocation + "," + Fuel + "," + Amount1 + "," + Amount2 + "," + RecordID + "," + Userid + ",'" + passing + "','" + TrailerType + "','" + Drivertype + "','" + VehicleType + "','" + ScanType + "','" + Weight + "','" + status + "','" + TxtFuelamount + "'";
            VendorDataDL = db.sub_GetDatatable(str);

            return VendorDataDL;
        }

        public DataTable SaveTrainDetails(string TrainNo, string NoCno, string OwnerStatus, string RankOperator, string FromLocation, string Tolocation, string OriginDepartureDate, string ETA
         , string ETD, string ATA, string ATD, string LoadingStartDate, string LoadingCompleteDate, string Unloadingstartdate, string unloadingcompletedate,
         string LIneNoalloated, string Trainstatus, string StatusUpdatedDate, string Remarks, int UserID, string PartyId)
        {
            if (OriginDepartureDate == "")
            {
                OriginDepartureDate = "1901-01-01 00:00";
            }
            if (ETA == "")
            {
                ETA = "1901-01-01 00:00";
            }
            if (ETD == "")
            {
                ETD = "1901-01-01 00:00";
            }
            if (ATA == "")
            {
                ATA = "1901-01-01 00:00";
            }
            if (ATD == "")
            {
                ATD = "1901-01-01 00:00";
            }
            if (LoadingStartDate == "")
            {
                LoadingStartDate = "1901-01-01 00:00";
            }
            if (LoadingCompleteDate == "")
            {
                LoadingCompleteDate = "1901-01-01 00:00";
            }
            if (Unloadingstartdate == "")
            {
                Unloadingstartdate = "1901-01-01 00:00";
            }
            if (unloadingcompletedate == "")
            {
                unloadingcompletedate = "1901-01-01 00:00";
            }
            if (StatusUpdatedDate == "")
            {
                StatusUpdatedDate = "1901-01-01 00:00";
            }

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            DataSet dataSet = new DataSet();
            //string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("TRAINNO", TrainNo);
            lstparam.Add("NOCNO", NoCno);
            lstparam.Add("OWNERSTATUS", OwnerStatus);
            lstparam.Add("OPERATOR", RankOperator);
            lstparam.Add("ORIGIN", FromLocation);
            lstparam.Add("DESTINATION", Tolocation);
            lstparam.Add("ORIGINDEPTDATE", OriginDepartureDate);
            lstparam.Add("ETADATE", ETA);
            lstparam.Add("ETDDATE", ETD);
            lstparam.Add("ATADATE", ATA);
            lstparam.Add("ATDDATE", ATD);
            lstparam.Add("LOADINGSTARTDATE", LoadingStartDate);
            lstparam.Add("LOADINGCOMPLETEDATE", LoadingCompleteDate);
            lstparam.Add("UNLOADINGSTARTDATE", Unloadingstartdate);
            lstparam.Add("UNLOADINGCOMPLETEDATE", unloadingcompletedate);
            lstparam.Add("LINENOALLOTTED", LIneNoalloated);
            lstparam.Add("TRAINSTATUS", Trainstatus);
            lstparam.Add("STATUSUPDATEDDATE", StatusUpdatedDate);
            lstparam.Add("REMARKS", Remarks);
            lstparam.Add("ADDEDBY", UserID);
            lstparam.Add("PartyId", PartyId);

            Dictionary<object, object> lstparam2 = new Dictionary<object, object>();
            lstparam2.Add("ContainerDTAuction", "@ContainerDTAuction");

            dataSet = db.AddAuctionData("USP_INSERT_BCN_TRAINMASTER", lstparam, null, null);

            return dataSet.Tables[0];
        }

        public int AddInvoiceDataIntoTableExport(long BillParty, long CHAID, DataTable dtInvoiceList, DataTable dtPaymentList, long UserId, string Category, string TDSPerc, string ReceiptType, string Remarks)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            int ReceiptNo = 0;
            DataSet ds = new DataSet();
            int i;
            int IgmFileId = 0;
            //string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("BillParty", BillParty);
            lstparam.Add("CHAID", CHAID);
            lstparam.Add("user_id", UserId);
            lstparam.Add("Category", Category);
            lstparam.Add("TDSPerc", TDSPerc);
            lstparam.Add("ReceiptType", ReceiptType);
            lstparam.Add("Remarks", Remarks);

            Dictionary<object, object> lstparam2 = new Dictionary<object, object>();
            lstparam2.Add("InvoiceDT", "@InvoiceDT");
            lstparam2.Add("PaymentDT", "@PaymentDT");

            ds.Tables.Add(dtInvoiceList);
            ds.Tables.Add(dtPaymentList);

            ReceiptNo = db.AddInvoiceData("USP_Receipt_Generation_Export", lstparam, ds, lstparam2);
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return ReceiptNo;

        }


        //code by prashant


        public DataTable GetVoucherEditDetails(string Voucherno, string ActivityType)
        {
            DataTable VoucherDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VoucherDL = db.sub_GetDatatable("TransportEditVoucherSummary '" + Voucherno + "','" + ActivityType + "'");
            return VoucherDL;
        }
        public int UpdateVoucherdetails(string VoucherNo, string Fuel, string AountBank, string AmountCash, int Userid, string ActivityType, string Remarks, int fromlocationID, int ToLocationID, string ActivityID, string FastagAmount)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("USP_UpdateVehicleDetails '" + VoucherNo + "','" + Fuel + "','" + AountBank + "','" + AmountCash + "','" + Userid + "','" + ActivityType + "','" + Remarks + "'," + fromlocationID + "," + ToLocationID + ",'" + ActivityID + "','" + FastagAmount + "'");

            return i;
        }
        public int CancelVoucherdetails(string VoucherNo, string CancelRemarks, int Userid)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("USP_CancelVoucherDetails '" + VoucherNo + "','" + CancelRemarks.Replace("'", "''") + "'," + Userid + "");

            return i;
        }
        public int ReprintfuelVoucherdetails(string VoucherNo, string ActivityType, string Remarks, int Userid)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("USP_ReprintFuelVoucherDetails '" + VoucherNo + "','" + ActivityType + "','" + Remarks + "'," + Userid + "");

            return i;
        }

        public int ReprintVoucherSlipVoucherdetails(string VoucherNo, string ActivityType, string Remarks, int Userid)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("USP_ReprintVoucherSLipVoucherDetails '" + VoucherNo + "','" + ActivityType + "','" + Remarks + "'," + Userid + "");

            return i;
        }

        public int GenerateFuelSlip(string Fuel, string AountBank, string AmountCash,
 string VoucherNo, string ActivityType, string Tolocation, string FromLocation,
 string DriverName, int Userid, string transporter)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("USP_GenerateFuelSlip '" + Fuel + "','" + AountBank + "','" + AmountCash + "','" + VoucherNo + "','" + ActivityType + "'," +
            "'" + Tolocation + "','" + FromLocation + "','" + DriverName + "'," + Userid + ",'" + transporter + "'");

            return i;
        }

        public int ChangeVoucherDriver(string Fuel, string AountBank, string AmountCash,
           string VoucherNo, string ActivityType, string Tolocation, string FromLocation,
           string DriverName, int Userid)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("USP_TransportAdminDriverchange '" + Fuel + "','" + AountBank + "','" + AmountCash + "','" + VoucherNo + "','" + ActivityType + "'," +
                "'" + Tolocation + "','" + FromLocation + "','" + DriverName + "'," + Userid + "");

            return i;
        }
        public int ChangeSizeFromVoucher(string VoucherNo, string ActivityType, string containerCount, int Userid)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("USP_TransportAdminSizechange '" +   VoucherNo + "','" + ActivityType + "'," +
                "'" + containerCount + "'," + Userid + "");

            return i;
        }
        public DataTable SetVehicleCancelStatus(string transid, string Remarks, int UserID)
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_CancelVehicle_Transactions '" + transid + "','" + Remarks + "'," + UserID + "");
            return DriverListDL;
        }

        public DataTable ValidateUser(int userid)
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USPValidateUserForCancel " + userid + "");
            return DriverListDL;
        }

        //start rahul 02112019
        public DataSet getDropDownListIGMManual()
        {
            DataSet ddIGMDS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            ddIGMDS = db.sub_GetDataSets("USP_FILL_DROPDOWNLIST_IGM_MANUAL");
            return ddIGMDS;
        }
        public DataTable getAutoConsigneeIGM(string Prefix)
        {
            DataTable CDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CDT = db.sub_GetDatatable("USP_FILL_DROPDOWNLIST_CONSIGNEE_IGM_MANUAL '" + Prefix + "'");
            return CDT;
        }
        //end rahul 02112019
        //code start by 05112019
        public DataTable getSeachIGMManualData(string Search, string IGMNo, string ItemNo, string ContainerNo, string ViaNo)
        {
            DataTable CDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CDT = db.sub_GetDatatable("USP_IGM_DETAILS_SEARCH_IGM_MANUAL '" + Search + "','" + IGMNo + "','" + ItemNo + "','" + ContainerNo + "','" + ViaNo + "'");
            return CDT;
        }
        //code end by 05112019
        //code start by 06112019
        public DataSet getIGMDataforEdit(string IGMNo, string ItemNo)
        {
            DataSet ddIGMDS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            ddIGMDS = db.sub_GetDataSets("USP_EDIT_IGM_NO_DETAILS '" + IGMNo + "','" + ItemNo + "'");
            return ddIGMDS;
        }
        public DataTable getIGMDataforAdd(string JONo)
        {
            DataTable CDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CDT = db.sub_GetDatatable("USP_IGM_DETAILS_FOR_ADD '" + JONo + "'");
            return CDT;
        }
        //code end by 06112019


        public DataTable GetCountainerType()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_GetContainerTypeDetails");

            return ExpenseDt;
        }

        public DataTable GetVoucherCloseDetails(string Voucherno)
        {
            DataTable VoucherDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VoucherDL = db.sub_GetDatatable("GetTransportCloseVoucherSummary '" + Voucherno + "'");
            return VoucherDL;
        }
        public DataTable CLoseDetails(string VoucherNo1, string Trailername, string Activityname, string FromID, string Toid, int Userid)
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_CLoseVoucherDetailsEntry '" + VoucherNo1 + "','" + Trailername + "','" + Activityname + "','" + FromID + "','" + Toid + "','" + Userid + "'");
            return DriverListDL;
        }

        public DataTable GetLocationame(string Name, Int64 id)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("get_sp_table 'Ext_Location_M','Location,LocationID','','Location'");
            return DT;
        }

        public DataTable GetActivitySearch(string Activity, string ContainerNo)
        {
            DataTable ImportsearchDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchDL = db.sub_GetDatatable("USP_Vehicle_Transactions_ConatinerNo '" + Activity + "','" + ContainerNo + "'");
            return ImportsearchDL;
        }

        public DataTable GetModifiymovmentdetails(string VehicleNo, string Actvitiy)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Vehicle_Status_Movement_TOP2 '" + VehicleNo + "', '" + Actvitiy + "'");

            return dt;
        }
        //code start by rahul 09-11-2019
        public DataTable getFuelData(int TransportorID, int Activity, int Type, string containerCount, int from, int to, string InOut, string trailerType, string passing, string driverid, string TrailerNo, string TotalWeight, long VehTransID,string ScanTypeID,string ScanCount)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetFuelData  '" + TransportorID + "'," + Activity + "," + Type + ",'" + containerCount + "'," + from + "," + to + ",'" + InOut + "','" + trailerType + "','" + passing + "'," + driverid + ",'" + TrailerNo + "','" + TotalWeight + "'," + VehTransID + ",'"+ ScanTypeID + "','"+ ScanCount + "'");

            return dt;
        }
        //code end by rahul 09-11-2019
        public int AddContainerBCNDataIntoTable( DataTable dtContainerList,long UserId, string TrainNo, string WagonRRNo)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            int ReceiptNo = 0;
            DataSet ds = new DataSet();
            int i;
            int IgmFileId = 0;
            //string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("UserId", UserId);
            lstparam.Add("TrainNo", TrainNo);
            lstparam.Add("WagonRRNo", WagonRRNo);

            Dictionary<object, object> lstparam2 = new Dictionary<object, object>();
            lstparam2.Add("ContainerWagonDT", "@ContainerWagonDT");

            ds.Tables.Add(dtContainerList);

            ReceiptNo = db.AddBCNWagonCLPData("USP_WagonContainerMapping", lstparam, dtContainerList, lstparam2);
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return ReceiptNo;

        }

        public int AddSBMappingBCNDataIntoTable(DataTable dtContainerList, long UserId, string Condition,string RRType)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            int ReceiptNo = 0;
            DataSet ds = new DataSet();
            int i;
            int IgmFileId = 0;
            //string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("UserId", UserId);
            lstparam.Add("Condition", Condition);
            lstparam.Add("RRType", RRType);

            Dictionary<object, object> lstparam2 = new Dictionary<object, object>();
            lstparam2.Add("ContainerSBMappingDT", "@ContainerSBMappingDT");

            ds.Tables.Add(dtContainerList);

            ReceiptNo = db.AddBCNWagonCLPData("USP_InsBCNSBContainerMapping", lstparam, dtContainerList, lstparam2);
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return ReceiptNo;

        }

        public DataTable GetPOLForAutoComplete(string name)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("get_sp_table 'IGMDetails','distinct IGM_Origin', 'IGM_Origin like ''" + name + "%''','IGM_Origin'");
            return LoginDT;
        }

        public DataTable SaveFuelConsumption(string txtentryDate, int ddlCostCenter, int trailerid, int txtBalQty, string txtIssueQty, string ddlFuelType, int txtReadFrom, int txtReadingTo, int ddlDriverName, string txtRemark, string ddlUnit, int UserID, int ddlvehicleType, string CostCenterFor, string differencereading)
        {
            DataTable SaveInternalFuelStock = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            SaveInternalFuelStock = db.sub_GetDatatable("SP_Fuel_consumption '" + Convert.ToDateTime(txtentryDate).ToString("yyyy-MM-dd HH:mm") + "','" + ddlCostCenter + "'," + trailerid + "," + txtBalQty + ",'" + txtIssueQty + "','" + ddlFuelType + "','" + txtReadFrom + "','" + txtReadingTo + "','" + ddlDriverName + "','" + UserID + "','" + txtRemark + "','" + ddlvehicleType + "','" + CostCenterFor + "','" + differencereading + "'");

            return SaveInternalFuelStock;
        }

        public DataTable GetEditTransportExpenses(string Entryid, string transexpenseID)
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_GetEditTransportList '" + Entryid + "','" + transexpenseID + "'");

            return ExpenseDt;
        }

        public DataTable CheckDuplicatefastagdetails(string UniqueTransactionID)
        {
            DataTable PaymentDS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PaymentDS = db.sub_GetDatatable("USP_CheckFastagTranscationDuplicate '" + UniqueTransactionID + "'");
            return PaymentDS;
        }

        public DataTable CheckForMenudetails(int Userid)
        {
            DataTable TransExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TransExpenseDt = db.sub_GetDatatable("USP_validatecheckUserExits '" + Userid + "'");

            return TransExpenseDt;
        }


        public DataTable DeleteTransportExpenses(int Entryid, int transexpenseID)
        {
            DataTable TransExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TransExpenseDt = db.sub_GetDatatable("USP_DeleteRecordFromtransportExpense " + Entryid + "," + transexpenseID + "");

            return TransExpenseDt;
        }

        //public DataTable GetSearchInvoice(string category, string Search, string txtItemNo)
        //{
        //    DataTable dt = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
        //    dt = db.sub_GetDatatable("SP_InvoiceSearch'" + category + "','" + Search + "','" + txtItemNo + "'");

        //    return dt;
        //}

        public DataTable GetSearchInvoiceVeiw(string AssessNo, string WorkYear, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Invoive_Search_View'" + AssessNo + "','" + WorkYear + "','" + Category + "'");

            return dt;
        }

        public DataTable GetChequeBounce(string CHEQUENO)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("SP_ChequeDets '" + CHEQUENO + "'");

            return dt;
        }

        public DataTable ExportSearchInvoiceVeiw(string AssessNo, string WorkYear, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("GET_Sp_EXPAssessDetails'" + AssessNo + "','" + WorkYear + "','" + Category + "'");

            return dt;
        }

        public DataTable BondSearchInvoiceVeiw(string AssessNo, string WorkYear, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("usp_invoice_search'" + AssessNo + "','" + WorkYear + "','" + Category + "'");

            return dt;
        }

        public DataTable DomestcSearchInvoiceVeiw(string AssessNo, string WorkYear, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Domestic_AssessN'" + AssessNo + "','" + WorkYear + "','" + Category + "'");

            return dt;
        }

        public DataTable MNRSearchInvoiceVeiw(string AssessNo, string WorkYear, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_MNR_AssessN'" + AssessNo + "','" + WorkYear + "','" + Category + "'");

            return dt;
        }

        public DataTable CartingAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Carting_AssessN'" + AssessNo + "','" + WorkYear + "','" + Category + "'");

            return dt;
        }
        public DataTable CreditAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_CreditNote'" + AssessNo + "','" + WorkYear + "','" + Category + "'");

            return dt;
        }
        //code by rahul
        public DataSet getDDListForExternalReport()
        {
            DataSet DBLocationDT = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            DBLocationDT = db.sub_GetDataSets("USP_GET_DD_LIST_EXTERNAL_REPORT");
            return DBLocationDT;
        }
        public DataTable GetExternalMovementRegister(string FROMDATE, string TODATE, int SEARCHCRITERIA, string SEARCHTEXT)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_EXTERNAL_MOVEMENT_REPORT'" + Convert.ToDateTime(FROMDATE).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(TODATE).ToString("yyyy-MM-dd HH:mm") + "','" + SEARCHCRITERIA + "','" + SEARCHTEXT.Replace("'", "''") + "'");

            return dt;
        }
        //code end by rahul

        public DataTable GetICDIExportPortWiseMovement()
        {
            DataTable ICDMovementDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDMovementDt = db.sub_GetDatatable("sp_exppendancy_new");

            return ICDMovementDt;
        }




        public DataTable GetICDIExportPortShutOut()
        {
            DataTable ICDAhutOUtDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDAhutOUtDt = db.sub_GetDatatable("USP_CFS_Export_SHUTOUT_DMR");

            return ICDAhutOUtDt;
        }

        public DataTable GetICDIExportReMovement()
        {
            DataTable ICDAhutOUtDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDAhutOUtDt = db.sub_GetDatatable("USP_CFS_Export_Re_MOVEMENT_DMR");

            return ICDAhutOUtDt;
        }

        public DataTable GetICDIExportReWorking()
        {
            DataTable ICDAhutOUtDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDAhutOUtDt = db.sub_GetDatatable("USP_CFS_Export_REWORKED_DMR");

            return ICDAhutOUtDt;
        }


        

        public DataTable GetJOInHandMonthWise()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TPCloseDt = db.sub_GetDatatable("USP_IMP_JO_IN_HAND_MONTH_WISE");

            return TPCloseDt;
        }
        public DataTable GetJOReceived()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TPCloseDt = db.sub_GetDatatable("USP_CFS_IMP_JOReceived_DMR");

            return TPCloseDt;
        }
        public DataTable GetInTransit()
        {
            DataTable TPCloseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TPCloseDt = db.sub_GetDatatable("USP_IMP_IN_TRANSIT");

            return TPCloseDt;
        }

        public DataTable AjaxFuelReport(string fromdate, string todate)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_Get_Fuel_Report '" + fromdate + "','" + todate + "'");
            return DT;
        }

        public DataTable GetICDIExportMonthWiseCollection()
        {
            DataTable ICDMovementDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDMovementDt = db.sub_GetDatatable("USP_MONTH_WISE_EXPORT_Collections_DETS");

            return ICDMovementDt;
        }

        public DataTable GetICDIExportMonthWiseMovement()
        {
            DataTable ICDMovementDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDMovementDt = db.sub_GetDatatable("USP_MONTH_WISE_EXPORT_MOVEMENTS_DETS");

            return ICDMovementDt;
        }

        //code by rahul 03-12-2019
        public DataTable GetGSTSearch(string SearchText)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_get_list_Gst_EDI '" + SearchText.Replace("'", "''") + "'");
            return LoginDT;
        }
        public DataSet GetExisitinchaname()
        {
            DataSet DS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDataSets("USP_FILL_DROPDOWN_EDI_COLLECTION");
            return DS;
        }
        public DataTable GetTariffAmount(int PageFrom, int AccountID, int State)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_CALCULATION_FOR_EDI_TARIFF " + PageFrom + "," + AccountID + "," + State + "");
            return LoginDT;
        }

        public DataSet GetEDIPrintDetails(int AssessNo, string WorkYear)
        {
            DataSet DS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDataSets("USP_EDI_PRINT '" + AssessNo + "','" + WorkYear + "'");
            return DS;
        }
        public DataTable GetEDISummaryData(string FromDate, string ToDate, string category, string Search1, string Search)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_EDI_SUMMARY '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + category + "','" + Search1 + "','" + Search + "'");
            return LoginDT;
        }

        public DataTable SaveFastagDetails(string trailerid, string tagno, string remakrs, int chkIsActive, int Userid)
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_INSERT_FASTAG_MASTER '" + trailerid + "','" + tagno + "','" + remakrs + "'," + chkIsActive + "," + Userid + "");

            return ExpenseDt;
        }


        public DataTable CheckFastagNo(string trailerid, string tagno, string remakrs, int chkIsActive)
        {
            DataTable VoucherNumereDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherNumereDL = db.sub_GetDatatable("USP_CheckTragNo '" + trailerid + "','" + tagno + "'");

            return VoucherNumereDL;
        }

        public DataTable GetFastagDetails()
        {
            DataTable VoucherNumereDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherNumereDL = db.sub_GetDatatable("USP_GetFastagDetails");

            return VoucherNumereDL;
        }


        public DataTable EditFastagDetails(string EditEntryid, string EditTagNo, int chkIsActive, int userid)
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_EditFasttagDetails '" + EditEntryid + "','" + EditTagNo + "','" + chkIsActive + "'," + userid + "");

            return ExpenseDt;
        }

        public DataTable SaveFastagTariffDetails(int fromlocation, int Tolocation, int Toll, int amount, string Passing, int Userid)
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_INSERT_FASTAG_TARIFF_D " + fromlocation + "," + Tolocation + "," + Toll + "," + amount + ",'" + Passing + "'," + Userid + "");

            return ExpenseDt;
        }

        public DataTable CheckFastagTariffExists(int fromlocation, int Tolocation, int Toll, int amount, string Passing)
        {
            DataTable VoucherNumereDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherNumereDL = db.sub_GetDatatable("USP_ChecktariffAlreadyexists " + fromlocation + "," + Tolocation + "," + Toll + "," + amount + ",'" + Passing + "'");

            return VoucherNumereDL;
        }


        public DataTable GetFastagtariffreport()
        {
            DataTable VoucherNumereDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherNumereDL = db.sub_GetDatatable("USP_getfastagTariffDetails");

            return VoucherNumereDL;
        }


        public DataTable Canceldetails(int entryid)
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_Cancelfastagdetails " + entryid + "");

            return ExpenseDt;
        }


        public DataTable SaveTollDetails(string TollName, int OngoingAmount, int Returnamount, string Passing, int Userid)
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_INSERT_TOLL_MASTER '" + TollName + "'," + OngoingAmount + "," + Returnamount + ",'" + Passing + "'," + Userid + "");

            return ExpenseDt;
        }


        public DataTable GetTollMasterDetails()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_getTollMasterDetails");

            return ExpenseDt;

        }


        public DataTable EditTollDetails(string TollName, int OngoingAmount, int Returnamount, int TollID, string PassingEdit, int Userid)
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_EditTollMasterDetails '" + TollName + "'," + OngoingAmount + "," + Returnamount + "," + TollID + ",'" + PassingEdit + "'," + Userid + "");

            return ExpenseDt;
        }

        public DataTable GetTollMasterDetailsDropDown()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_GetTollMasterDropWOnList");

            return ExpenseDt;
        }

        public DataTable Gettollamount(int Toll, string Passing)
        {
            DataTable VoucherNumereDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherNumereDL = db.sub_GetDatatable("USP_GetTollAmountDetails " + Toll + ",'" + Passing + "'");

            return VoucherNumereDL;
        }

        public DataTable ChecltollName(string TollName, string Passing)
        {
            DataTable VoucherNumereDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherNumereDL = db.sub_GetDatatable("USP_CheckTollNameAlreadyexists '" + TollName + "','" + Passing + "'");

            return VoucherNumereDL;
        }

        public DataTable SaveTollName(string ToolName)
        {
            DataTable TransExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TransExpenseDt = db.sub_GetDatatable("USP_INSERT_TollName '" + ToolName + "'");

            return TransExpenseDt;
        }

        // code end by rahul 03-12-2019

        public DataTable UpdateTrailerDetails(int trailerid, string ownedby, string Transportername, string passing, string grade, string modelno, string chasisno,
            string engine, string trailertype, string permit, string permittype, int userid, int EditchkIsActive, string EditVehicleTYpe,
            string EditIsshifting, string EditVehicleGroupType, string PolicyDate, string GreenTax, string PucDate, string UsedFor, string Taxdate,
            string Fitnessdate, string EditVEhicleTypeID, string EditVendorName, string TrailerSize, string VehicleGroupType, string FuelType,string Model, string SoldDateEdit, string MFGYREdit, string MileageEDIt, string GVWEDIT)
        {
            DataTable TrailerupdateDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TrailerupdateDL = db.sub_GetDatatable("USP_UpdateTrailerDetails " + trailerid + ",'" + ownedby + "','" + Transportername + "','" + passing + "','" + grade + "','" + modelno + "','" + chasisno + "','" + engine + "','" + trailertype + "','" + permit + "','" + Convert.ToDateTime(permittype).ToString("dd-MMM-yyyy HH:mm") + "','" + userid + "','" + EditchkIsActive + "','" + EditVehicleTYpe + "','" + EditIsshifting + "','" + EditVehicleGroupType + "','" + EditVEhicleTypeID + "','" + EditVendorName + "','" + Model + "','" + SoldDateEdit + "','" + MFGYREdit + "','" + MileageEDIt + "','" + GVWEDIT  + "','" + FuelType + "'");
            return TrailerupdateDL;
        }

        public DataTable getContainerListDataExpternalSearch()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_CONTAINER_LIST_FOR_MODIFY_EXTERNAL_MOVEMENT");
            return LoginDT;
        }

        //public string SaveVendorName(string TrailerName, string vendor)
        //{
        //    string message = "";
        //    DataTable ContainerDT = new DataTable();
        //    ContainerDT = transportdatamanager.SaveVendorName(TrailerName, vendor);
        //    if (ContainerDT.Rows.Count > 0)
        //    {
        //        message = "Record Saved successfully";
        //    }
        //    return message;
        //}

        public int CancelDtails(string ddlCriteria, string ddlType, string txtInvoice)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("usp_update_unlocktransactions '" + ddlCriteria + "','" + ddlType + "'," + txtInvoice + "");

            return i;
        }

        // COde by Prashant 07 Jan 2020
        public DataTable GetInsuranceANDRTOCount(string Parameter)
        {

            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            trailerLocation = db.sub_GetDatatable("USP_GetInsuranceandRtoCountDetails '" + Parameter + "'");

            return trailerLocation;
        }


        public DataTable GetTrailerGarageDetails()
        {
            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            trailerLocation = db.sub_GetDatatable("[USP_Trailer_Location_Count_garage] ");

            return trailerLocation;
        }

        public DataTable GetDriverwithourdate()
        {
            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            trailerLocation = db.sub_GetDatatable("USP_getCount_drivers_without_trailers");

            return trailerLocation;
        }

        public DataTable GetAccidentDetails()
        {
            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            trailerLocation = db.sub_GetDatatable("USP_get_Accident_Count");

            return trailerLocation;
        }

        public DataTable GetBreakDownList()
        {
            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            trailerLocation = db.sub_GetDatatable("USP_Break_Down_list");

            return trailerLocation;
        }

        public DataTable GetWithoutpaperList()
        {
            DataTable WODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            WODT = db.sub_GetDatatable("USP_GetWithPaperDriversList_count");
            return WODT;
        }

        public DataTable GetTrailerShiftingDEtails()
        {
            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            trailerLocation = db.sub_GetDatatable("USP_GetTrailerShiftingCountDetails");

            return trailerLocation;
        }

        public DataTable getServicingDetails()
        {
            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            trailerLocation = db.sub_GetDatatable("USP_GetServicingDetails_count");
            return trailerLocation;
        }


        public DataTable GetAccidentSearch(string EntryID)
        {
            DataTable ImportsearchDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchDL = db.sub_GetDatatable("USP_Accident_Claim_Show '" + EntryID + "'");
            return ImportsearchDL;
        }

        public DataTable Gettrailerdetails(string Trailername)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetTrailerGPSLocation '" + Trailername + "'");

            return dt;
        }

        public DataTable GetTrailerDetails(string trailername)
        {
            DataTable ICDAhutOUtDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDAhutOUtDt = db.sub_GetDatatable("USP_GetDriverDetailsByTrailerID " + trailername + "");

            return ICDAhutOUtDt;
        }
        public DataTable ReleaseDriverhold(string Driverid, string HoldID, int Userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_ReleaseDriverDetails  '" + Driverid + "','" + HoldID + "'," + Userid + "");

            return dt;
        }
        public DataTable SaveDriverLoanDetails(string Loadid, string LoanDate, string Driverid, string DriverName, string SlipNo, string FuelQty, string FuelCash, string DeductionType, string DeductionFuel
            , string DeductionAmount, string Deductionbank, string Remarks, int Userid, string deductionFor)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_INSERT_DRIVER_LOAN_M  '" + Loadid + "','" + Convert.ToDateTime(LoanDate).ToString("dd MMM yyyy hh:mm") + "','" + Driverid + "','" + DriverName + "','" + SlipNo + "','" + FuelQty + "','" + FuelCash + "','" + DeductionType + "','" + DeductionFuel + "','" + DeductionAmount + "','" + Deductionbank + "','" + Remarks + "'," + Userid + ",'" + deductionFor + "'");

            return dt;
        }


        public DataTable SaveLeaveDetails(string Driverid, string TrailerNo, string DriverleaveFor, string Remarks, int Userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_INSERT_DRIVER_LEAVE_ENTRIES  '" + Driverid + "','" + TrailerNo + "'," + DriverleaveFor + ",'" + Remarks + "'," + Userid + "");

            return dt;
        }



        //07 jan 2020 2:57
        public DataTable GetVehicleDownloadList(int id, string id1)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_GetVehicleDocumentDownloadDetails  " + id + "," + id1 + "");
            return AttachmentDT;
        }

        public DataTable GetVehicleListDocumentDetails(string Trailername)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_GetTrailerNameDocumentDetails  '" + Trailername + "'");
            return AttachmentDT;
        }

        public DataTable GetVehicleDocumentTypeDetails(string VehicleType)
        {
            DataTable TrailerNoDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TrailerNoDL = db.sub_GetDatatable("USP_TrailerDocumentTypeDetails '" + VehicleType + "'");
            return TrailerNoDL;
        }

        public DataTable RtoDetails(string Trailername)
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_FetchRtoDetails '" + Trailername + "'");
            return DriverListDL;
        }


        public DataTable GetInsuranceModuleDetails(string Trailername)
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_InsuranceAndRtoDetailsSummary '" + Trailername + "'");
            return DriverListDL;
        }
        public DataTable GetPermitType()
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_PermitTypeDropDownList");
            return DriverListDL;
        }
        public DataTable GetTaxType()
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_Get_TaxTypeList");
            return DriverListDL;
        }
        public DataTable GetMakeList()
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_Get_MakeList");
            return DriverListDL;
        }
        public DataTable GetModelList()
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_Get_ModelList");
            return DriverListDL;
        }
        public DataTable GetInsuranceCompanyList()
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_Get_InsuranceCompanyList");
            return DriverListDL;
        }

        public DataTable GetInsuranceVehicleFecth(string Trailername)
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_InsuranceDetailsSummary " + Trailername + "");
            return DriverListDL;
        }

        public DataTable GetLOcationBilling(string fromdate, string todate, int locationid)
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("USP_LocationWiseBillingDetails '" + fromdate + "','" + todate + "'," + locationid + "");
            return DriverListDL;
        }

        public DataTable GetVehicleList(DateTime FromDate, DateTime ToDate, string searchText)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GETVouhcerTrailerDetails '" + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy HH:mm") + "'");
            return dt;
        }

        public DataTable SaveVendorName(string TrailerName, string vendor)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_UpdateTrailersVendorName '" + TrailerName + "','" + vendor + "'");

            return dt;
        }

        //Code By prashant

        public DataTable GetICDDriverNo(string Driverno)
        {
            DataTable TrailerNoDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TrailerNoDL = db.sub_GetDatatable("USP_FuelConsumptionDriverNumberFetch '" + Driverno + "'");
            return TrailerNoDL;
        }

        public DataTable getTrailerLocationCountNavkar()
        {
            DataTable trailerLocation = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            trailerLocation = db.sub_GetDatatable("USP_GetNavkarCount");

            return trailerLocation;
        }

        public DataSet getDropDownListExternal()
        {
            DataSet ddWODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            ddWODS = db.sub_GetDataSets("usp_External_job_order");
            return ddWODS;
        }

        public DataTable GetSBBOEData(string Category, string SBNo, string BOENo)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GET_SB_BOE_DATA_EDI '" + Category + "','" + SBNo + "','" + BOENo + "'");
            return LoginDT;
        }
        //End By Rahul 20-01-2020

        public DataTable Teussearch(string txtbookingNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("usp_booking_Search_Teus'" + txtbookingNo + "'");

            return dt;
        }

        public DataTable Teussearchstock(string AGID, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("usp_Stock_Search_Teus'" + AGID + "','" + ToDate + "'");

            return dt;
        }

        public DataTable TeussearchLoaded(string DepartmentID, string FromDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("usp_Loaded_Search_Teus'" + DepartmentID + "','" + FromDate + "'");

            return dt;
        }

        public DataSet TeussearchPord()
        {
            DataSet JODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            JODS = db.sub_GetDataSets("USP_IMPORT_PORT_PENDENCY  ");
            return JODS;
            
        }

        public DataSet TeussearchPords(string AsonDate)
        {
            DataSet JODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            JODS = db.sub_GetDataSets("sp_categorywiseinventory '" + Convert.ToDateTime(AsonDate).ToString("yyyyMMddHHmm") + "','" + Convert.ToDateTime(AsonDate).ToString("dd-MMM-yyyy HH:mm") + "'");
            return JODS;

        }
        public DataSet teusSearchLoadedAll(string AsonDate)
        {
            DataSet JODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            JODS = db.sub_GetDataSets("sp_categorywiseinventory_All_Yard '" + Convert.ToDateTime(AsonDate).ToString("yyyyMMddHHmm") + "','" + Convert.ToDateTime(AsonDate).ToString("dd-MMM-yyyy HH:mm") + "'");
            return JODS;

        }

        public DataTable MenuNameFill()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("Select Distinct MenuDesc From MenuDetails'");
            // LoginDT = db.sub_GetDatatable("USP_FetchVesselDetal '" + viaNO + "'");

            return LoginDT;
        }

        public DataTable UserNameFill()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("Select  UserID, UserName From UserDetails where IsActive=1 UNION Select UserID, UserName From UserDetails where IsActive=1 Order By UserID");
            // LoginDT = db.sub_GetDatatable("USP_FetchVesselDetal '" + viaNO + "'");

            return LoginDT;
        }

        public DataTable IconNameFill()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("select DISTINCT   Icon from MenuDept_Master");
            // LoginDT = db.sub_GetDatatable("USP_FetchVesselDetal '" + viaNO + "'");

            return LoginDT;
        }

        public DataTable CheckFirstName(string FirstName)
        {
            DataTable FirstNameDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            FirstNameDL = db.sub_GetDatatable("USP_CheckCreateUserName '" + FirstName + "'");
            return FirstNameDL;
        }

        public DataTable CheckvendorName(string Vendorid)
        {
            DataTable FirstNameDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            FirstNameDL = db.sub_GetDatatable("USP_CheckCreatevendorName '" + Vendorid + "'");
            return FirstNameDL;
        }


        public DataTable EditUserID(string UserID)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_Update_Create_User '" + UserID + "'");
            return DT;
        }

        //suraj

        public DataTable AddJobOrderDataIntoTables(long JONo, int AgentID, int SLID, long shipperid, long Importerid, long Chaid, String ViaNo, int VesselID, int PortID, DateTime berthingdate, int Haulage_Type_Id, int File_Status_Id, int Transport_Type_Id, int POL_ID, int Sales_Person_Id, string BLNumber, DateTime BLReceivedDate, int UserId, string HouseBLNumber, int KAMID, string JoRemarks, int JoTypeId, int FileId, string IGMNo)


        {
            DataTable JobOrderData = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JobOrderData = db.AddDataJobOrderDatas(JONo, AgentID, SLID, shipperid, Importerid, Chaid, ViaNo, VesselID, PortID, berthingdate, Haulage_Type_Id, File_Status_Id, Transport_Type_Id, POL_ID, Sales_Person_Id, BLNumber, BLReceivedDate, UserId, HouseBLNumber, KAMID, JoRemarks, JoTypeId, FileId, IGMNo);
            return JobOrderData;

        }


        //suraj




        public DataTable GetJOListBL(DateTime FromDate, DateTime ToDate, string SearchCriteria, string SearchText, int Userid, Boolean IsDate)
        {
            DataTable JODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JODT = db.sub_GetDatatable("USP_FetchImportList '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + SearchCriteria + "','" + SearchText + "'," + Userid + "," + IsDate + "");
            return JODT;
        }

        //su
        public DataTable CancelJOBL(long id, int Userid, int ReasonId)
        {
            DataTable CancelJODL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CancelJODL = db.sub_GetDatatable("USP_CancelImport_BlM  " + id + "," + Userid + "," + ReasonId + "");
            return CancelJODL;
        }
        public DataTable SubmitJOBL(long id, int Userid)
        {
            DataTable SubmitJODL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            SubmitJODL = db.sub_GetDatatable("USP_Submit_Import_BL  " + id + "," + Userid + "");
            return SubmitJODL;
        }

        public DataSet getJOViewDetailsBl(long JONO, int Userid)
        {
            DataSet JODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            JODS = db.sub_GetDataSets("USP_Get_Import_BlDetails  " + JONO + "," + Userid + "");
            return JODS;
        }

        public DataTable GetFuelConsumptionDetails(string SlipNo)
        {
            DataTable VoucherDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VoucherDL = db.sub_GetDatatable("USP_FuelConsumption_show '" + SlipNo + "'");
            return VoucherDL;
        }

        public int CancelFuelConsumptiondetails(string SlipNo, string CancelRemarks, int Userid)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("usp_cancel_FuelConsumption '" + SlipNo + "','" + CancelRemarks.Replace("'", "''") + "'," + Userid + "");

            return i;
        }

        public int ReprintFuelConsumptiondetails(string SlipNo)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("usp_IsPrint_FuelConsumption '" + SlipNo + "'");

            return i;
        }

        public DataTable MapCustomerWiseAutomailsave(string Activity, string Entrydate, string mailto, string mailtoID, string mailtoemailid,
           string cc, string Replyto, int IsActive, string entryid, int userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_INSERT_MAPPED_AUTO_MAIL_CUST_INVOICE '" + Entrydate + "','" + Activity + "','" + mailto + "','" + mailtoID + "','" + mailtoemailid + "','" + IsActive + "','" + userid + "','" + Replyto + "','" + cc + "','" + entryid + "'");

            return dt;
        }


        public DataTable GetMapEmailidcheck(string mailto, string mailtoID)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_checkmappingdetails '" + mailto + "','" + mailtoID + "'");
            return AttachmentDT;
        }

        public DataTable GetMapEmailid(int Userid,int MenuID)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_CheckEdit_Rights_Web '" + Userid + "','"+20+"'");
            return AttachmentDT;
        }

        public DataTable GetIGMVeiw(string IGMNo, string temNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_IGM_BID_FILL_Dec'" + IGMNo + "','" + temNo + "'");

            return dt;
        }


        public DataTable GetjonoVeiw(string Jono)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("usp_jono_Add_container'" + Jono + "'");

            return dt;
        }

        public DataTable updateStatus(string ContainerNo)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_updatecontainerstatus '" + ContainerNo + "'");
            return DT;
        }

        public string AddLoloData(DataTable dtPaymentList, long UserId)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            string ReceiptNo ="";
            DataSet ds = new DataSet();
            //string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("UserId", UserId);

            ds = db.AddLoloDataDt("USP_INSERT_TEMP_WHL_LOLO", lstparam, dtPaymentList);
          
            return ReceiptNo;

        }

        public DataTable UpdateTrailerDetails(int trailerid, string ownedby, string Transportername, string passing, string grade, string modelno, string chasisno, string engine, string trailertype, string permit, string permittype, int userid, int EditchkIsActive, string EditVehicleTYpe, string EditIsshifting, string EditVehicleGroupType, string PolicyDate, string GreenTax, string PucDate, string UsedFor, string Taxdate,
           string Fitnessdate, string EditVEhicleTypeID, string EditVendorName, string TrailerSize, string VehicleGroupType, string FuelType)
        {
            DataTable TrailerupdateDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TrailerupdateDL = db.sub_GetDatatable("USP_UpdateTrailerDetails " + trailerid + ",'" + ownedby + "','" + Transportername + "','" + passing + "','" + grade + "','" + modelno + "','" + chasisno + "','" + engine + "','" + trailertype + "','" + permit + "','" + Convert.ToDateTime(permittype).ToString("dd-MMM-yyyy HH:mm") + "'," + userid + "," + EditchkIsActive + ",'" + EditVehicleTYpe + "','" + EditIsshifting + "','" + EditVehicleGroupType + "','" + PolicyDate + "','" + GreenTax + "','" + PucDate + "','" + UsedFor + "','" + Taxdate + "','" + Fitnessdate + "','" + EditVEhicleTypeID + "','" + EditVendorName + "','" + TrailerSize + "','" + VehicleGroupType + "','" + FuelType + "'");
            return TrailerupdateDL;
        }


        public DataTable AddCompanyTicketDetails1(string Description, string MailID, string strAttachement, int companyid, string contentType ,long Userid, int TicketNumber)
        {
            DataTable userDataDL = new DataTable();

            HC.DBOperations db = new HC.DBOperations();

            userDataDL = db.AddAttachment1(Description, MailID, strAttachement, companyid, contentType, Userid, TicketNumber);
            return userDataDL;
        }

        public DataTable ReceiptFillDate(string Search, string ReceiptNo, string WorkYear)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_RCPT_Mode_Dets '" + Search + "','" + ReceiptNo + "','" + WorkYear + "'");
            return DT;
        }

        public DataTable getAutoCustomerDetails(string Prefix)
        {
            DataTable VesselDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VesselDT = db.sub_GetDatatable("GetCustomerDetdebit  '" + Prefix + "'");
            return VesselDT;
        }

        public DataTable getAutodebitforcustomer(string Prefix)
        {
            DataTable VesselDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VesselDT = db.sub_GetDatatable("GetCustomerDetdebitfor  '" + Prefix + "'");
            return VesselDT;
        }
        public DataTable GetGSTDetailsForCustomer(string gstID)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_GetGSTNoForCustomer '" + gstID + "'");
            return AttachmentDT;
        }

        public DataTable CancelDebitdetails(string debitnoteno, int Userid)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_CancelDebitDetails '" + debitnoteno + "'," + Userid + "");
            return AttachmentDT;
        }
        //
        public DataTable MapCustomerWiseAutomailsaveMap(string Activity, string Entrydate, string mailto, string mailtoID, string mailtoemailid,
      string cc, string Replyto, int IsActive, string entryid, int userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_INSERT_MAPPED_AUTO_MAIL_CUST_INVOICE '" + Entrydate + "','" + Activity + "','" + mailto + "','" + mailtoID + "','" + mailtoemailid + "','" + IsActive + "','" + userid + "','" + Replyto + "','" + cc + "','" + entryid + "'");

            return dt;
        }
      
        public DataTable GetMapEmailidcheckss(string mailto, string mailtoID)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_checkmappingdetails '" + mailto + "','" + mailtoID + "'");
            return AttachmentDT;
        }

        public DataTable MapMailSave(string mailtoID, string mailto, string mailcc, int userid, string Entryid, string mailtoemailid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_InsertIntocustomer '" + mailtoID + "','" + mailto + "','" + mailcc + "','" + userid + "','" + Entryid + "','" + mailtoemailid + "'");

            return dt;
        }   

        public DataTable USP_Check_rights_To_Save_Mail(int Userid)
        {
            DataTable CheckMailSave = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CheckMailSave = db.sub_GetDatatable("USP_Check_rights_To_Save_Mail " + Userid + "");
            return CheckMailSave;
        }
        public DataTable GetBillVerications(string searchcerteria)
        {
            DataTable tripDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            tripDT = db.sub_GetDatatable("USP_GetBillVerifications '" + searchcerteria + "'");
            return tripDT;
        }

        public DataTable GetDriverwisePaymentdetails(string FromDate, string ToDate, string Value)
        {
            DataTable WODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            WODT = db.sub_GetDatatable("USP_DriverPaymentDetails '" + FromDate + "','" + ToDate + "','" + Value + "'");
            return WODT;
        }
        public DataTable Updatefuelbillverificationdetails(string BillNo, string Invoiceno, string vendorid, string billdate, string Workyear, int Userid)
        {
            DataTable DriverListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDatatable("UpdateFuelBillverificationDetails '" + BillNo + "','" + Invoiceno + "','" + vendorid + "','" + billdate + "','" + Workyear + "','" + Userid + "'");
            return DriverListDL;
        }
        public DataTable CheckOldPass(string oldtpassword, int userID)
        {
            DataTable HorseNumberDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            HorseNumberDL = db.sub_GetDatatable("USP_OLD_PASSWORD '" + oldtpassword + "','" + userID + "'");
            return HorseNumberDL;
        }

        public int SaveAddadditionaltariffCenter(string Transporter, string Activity, string Drivertype, string Inout, string ContainerCount, string cash,
            string bank, string Fromdate, string Todate, int Userid, string Entryid,string fuel)
        {
            int i = 0;
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            i = db.sub_ExecuteNonQuery("USP_INSERT_VOUCHER_FUELTARIFF_FIXED  '" + Drivertype + "','" + Transporter + "','" + Activity + "','" + bank + "','" + Fromdate + "','" + Todate + "','" + cash + "','" + Inout + "','" + Userid + "','" + ContainerCount + "','" + Entryid + "','" + fuel + "'");

            return i;
        }

        public DataTable CheckDriverDuplicate(string DLno)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_CheckDriverLiceneceDuplicate  '" + DLno + "'");

            return dt;
        }
        public DataTable CheckDuplicateDetailsMail(string mailto, string mailtoID, string Entryid)
        {
            DataTable CheckMailSave = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            CheckMailSave = db.sub_GetDatatable("USP_CheckDuplicateFormail '" + mailto + "','" + mailtoID + "','" + Entryid + "'");
            return CheckMailSave;
        }


        public DataTable GetSLipWiseFueldetails(int Trailerid, string SLipNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetSlipWiseFuelQty '" + Trailerid + "','" + SLipNo + "'");

            return dt;
        }


        public int SaveFuelAcknowledgement(string Trailerid, string SLipNo, string FuelQty, string FuelRate, string Totalamt, string Remarks, int userid)
        {
            int i = 0;
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            i = db.sub_ExecuteNonQuery("USP_SaveFuelRefilling  '" + Trailerid + "','" + SLipNo + "','" + FuelQty + "','" + FuelRate + "','" + Totalamt + "','" + Remarks + "','" + userid + "'");

            return i;
        }
        public DataTable CheckSLipNoForFuelAcknowledgement(string Trailerid, string SLipNo, string FuelQty, string FuelRate, string Totalamt, string Remarks, int userid)
        {
            DataTable ListDl = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ListDl = db.sub_GetDatatable("USP_CHECKSLIpNoForFuelRefillingDetails  '" + Trailerid + "','" + SLipNo + "','" + FuelQty + "','" + FuelRate + "','" + Totalamt + "','" + Remarks + "','" + userid + "'");

            return ListDl;
        }

        //code for driver details

        public DataTable getDriverAdvanceFuelData(string driverid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Balance_Driver_Advance  " + driverid + "");

            return dt;
        }

        public DataSet GetDropDownListForJoType()
        {
            DataSet LoginDT = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDataSets("USP_GetGenerateCargojo");
            return LoginDT;
        }
        public DataTable GetInvoiceDetailsForUploadFile(string Invoiceno)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GetUploadCopyDetails '" + Invoiceno + "'");
            return LoginDT;
        }

        public DataTable GetDropDownListDocs()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GetDocsSeriesForUploasCopy");
            return LoginDT;
        }
        public DataTable GetInvoiceDocumentList1(int DocID, string InvoiceNO)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_InvoiceNOWISE_GETDocumentList1  " + DocID + ", " + InvoiceNO + "");
            return AttachmentDT;
        }

        public DataTable GetInvoiceDocumentList(string InvoiceNO)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_InvoiceNOWISE_GETDocumentList  " + InvoiceNO + "");
            return AttachmentDT;
        }

        public DataTable SaveBLDet(string Name, string AdharNo, string Penno, string ContactNo, string VoterID, string Reference, string Remarks, int Userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Insert_Black_list_persons  '" + Name + "','" + AdharNo + "','" + Penno + "','" + ContactNo + "','" + VoterID + "','" + Reference + "','" + Remarks + "','" + Userid + "'");

            return dt;
        }

        public DataTable GetClosedLRList(string FromDate, string ToDate, string Activity, string SearchCerteria, string LrNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_LST_CLOSED_LR_test '" + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy HH:mm") + "','" + Activity + "','" + SearchCerteria + "','" + LrNo + "'");
            return dt;
        }
        //get activity master data
        public DataTable getActivityMaster()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_ActivityMaster");
            return dt;
        }
        //get LR Document List
        public DataTable GetLRDocumentList(string LRNo)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_LRNOWISE_GETDocumentList  " + LRNo + "");
            return AttachmentDT;
        }

        public DataTable GetLRDocumentList1(int DocID, string LRNo)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_LRNOWISE_GETDocumentList1  " + DocID + ", " + LRNo + "");
            return AttachmentDT;
        }

        public DataTable GetEntryIdForLorryReceipt(string containerno, string Activitye, string bookingno)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("LorryreceptDetails '" + containerno + "','" + Activitye + "','" + bookingno + "'");

            return dt;
        }

        public DataTable GetLRList(string fromDate, string ToDate, string Searchcerteria, string txtNumber)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //  dt = db.sub_GetDatatable("USP_MTY_Pre_Out_Plan ");

            dt = db.sub_GetDatatable("USP_LIST_LR_OPEN '" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + Searchcerteria + "','" + txtNumber + "'");

            return dt;
        }
        public DataTable GetPendingClosedLRList(string FromDate, string ToDate, string Activity)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_PendingDiversionLocation '" + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy HH:mm") + "','" + Activity + "'");
            return dt;
        }

        public DataTable GetICDFullImportDataSeparate(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("Import_PortWise_movement_tues_report_MIS_NEW '" + FromDate + "','" + ToDate + "'");
            return dt;
        }
        public DataTable GetICDFullExportDataSeparate(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("Export_PortWise_movement_tues_report_MIS_NEW '" + FromDate + "','" + ToDate + "'");
            return dt;
        }


        public DataSet GetCFSFULLImportExportData(string FromDate, string ToDate)
        {
            DataSet dt = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDataSets("USP_IMPORT_EXPORT_CFS_FULL_REPORT_Web '" + FromDate + "','" + ToDate + "'");
            return dt;
        }



        public DataTable GetICDFullExportRepoDataSeparate(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("EmptyRepoPort_hazira_movement_tues_report_MIS_new '" + FromDate + "','" + ToDate + "'");
            return dt;
        }
        public DataTable UpdateBIllNoforInternalstock(string BillNo, string AmountForBill, string FUelID, string QtyModify, string RateModify)
        {
            DataTable SaveInternalFuelStock = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            SaveInternalFuelStock = db.sub_GetDatatable("USP_UpdateFuelInternalStockBillNo '" + BillNo + "','" + AmountForBill + "'," + FUelID + ",'" + QtyModify + "','" + RateModify + "'");

            return SaveInternalFuelStock;
        }

        public DataTable GetFuelSlipDetails(string SlipNo)
        {
            DataTable VoucherDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VoucherDL = db.sub_GetDatatable("USP_FuelM_Update '" + SlipNo + "'");
            return VoucherDL;
        }

        public int CancelFueldetails(string SlipNo, string CancelRemarks, int Userid)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("usp_cancel_fuel_M '" + SlipNo + "','" + CancelRemarks.Replace("'", "''") + "'," + Userid + "");

            return i;
        }

        public int ReprintFuelSlipdetails(string SlipNo)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("usp_IsPrint_Fuel_M '" + SlipNo + "'");

            return i;
        }
        public DataTable GetICDIExportVesselANdCustomerWisePendency()
        {
            DataTable ICDAhutOUtDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDAhutOUtDt = db.sub_GetDatatable("USP_CFS_PendingMovement");

            return ICDAhutOUtDt;
        }

        public DataTable GetICDIExportVesselWisePendency()
        {
            DataTable ICDAhutOUtDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDAhutOUtDt = db.sub_GetDatatable("USP_CFS_PendingMovement_vesselWise");

            return ICDAhutOUtDt;
        }
        public DataTable GetICDIExportCustomerWisePendency()
        {
            DataTable ICDAhutOUtDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ICDAhutOUtDt = db.sub_GetDatatable("USP_CFS_PendingMovement_CUSTOMERWise");

            return ICDAhutOUtDt;
        }

        public DataTable GetExportReportDetails(string searchtype, string seachon, string Fromdate, string Todate)
        {
            DataTable DT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DT = db.sub_GetDatatable("USP_GetEXportReportSearchDetails '" + searchtype + "','" + seachon + "','" + Fromdate + "','" + Todate + "'");
            return DT;
        }

        public DataTable getExportSBEntryidforexpin(string Containerno)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_GetExportshippingbill_Detail '" + Containerno + "'");
            return ImportsearchHoldingListDL;
        }

        public DataTable GetSBlingNoDEtails(string SBNo, string SBentryID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("GET_sp_ExportSBNoDtels '" + SBNo + "','" + SBentryID + "'");
            return LoginDT;
        }
        public DataTable GetSBlingCartingBalDetails(string SBNo, string SBentryID)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("GET_sp_ExportSBNocartingDtels '" + SBNo + "','" + SBentryID + "'");
            return LoginDT;
        }

        public DataTable getEntryidforexpin(string Containerno)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_GetEntryidExpInList '" + Containerno + "'");
            return ImportsearchHoldingListDL;
        }
        public DataTable GetExportDetails(string Containerno, string Jono)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("GET_sp_ExportConatinerDtels '" + Containerno + "','" + Jono + "'");
            return LoginDT;
        }

        public DataTable GetExportContainertypeDetails(string Containerno, string Jono)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GetExportSearchContainerentrytypeDetails '" + Containerno + "','" + Jono + "'");
            return LoginDT;
        }

        public DataTable GetExportContainerSlipNoDetails(string Containerno, string Jono)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GetExportSearchSLipNoDetails '" + Containerno + "','" + Jono + "'");
            return LoginDT;
        }

        //invoice
        public DataSet GetDropDownListProformaInvoice()
        {
            DataSet DS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDataSets("USP_FILL_DROPDOWN_IMPORT_INVOICE");
            return DS;
        }
        public DataTable GetGSTSearchImportProforma(string SearchText)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_get_list_Gst_EDI '" + SearchText.Replace("'", "''") + "'");
            return LoginDT;
        }

        public DataSet CheckImporttariffList(string size, string Type)
        {
            DataSet PaymentDS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            PaymentDS = db.sub_GetDataSets("USP_GET_DUPLICATE_IMPORT_TARIFF_SETTING '" + size + "','" + Type + "'");
            return PaymentDS;
        }

        public DataTable GetCancelDetailsForTariffSetting(string TariffId, string deliveryType, string containerType, string accounthead)
        {
            DataTable PaymentDS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PaymentDS = db.sub_GetDatatable("USP_Transport_CANCELTariffDetails '" + TariffId + "','" + deliveryType + "','" + containerType + "','" + accounthead + "'");
            return PaymentDS;
        }

        public DataTable GetAddedTariffsettingDetails(int Userid)
        {
            DataTable PaymentDS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PaymentDS = db.sub_GetDatatable("USP_GetTariffSettingDetails '" + Userid + "'");
            return PaymentDS;
        }

        public DataSet validateIGMNItemNoForPIAndGetDetails(string AssessmentType, string IGMNo, string ItemNo)
        {
            DB.DBConnection objConn = new DB.DBConnection();


            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_ValidateIGMNItemNoForPI";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@AssessType", AssessmentType);
                    objCommand.Parameters.AddWithValue("@IGMNo", IGMNo);
                    objCommand.Parameters.AddWithValue("@ItemNo", ItemNo);


                    DataSet dsProforma = new DataSet();


                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;

                        _Data.Fill(dsProforma);
                    }
                    connection.Close();

                    return dsProforma;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }

        public DataTable CancelAssessmentDetails(string remarks, string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_CancelAssessNo'" + remarks + "','" + AssessNo + "','" + workyear + "'," + userId + "");
            return Cargotype;
        }
        public DataTable CheckCancelAssessmentDetails(string Invoiceno)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_Validate_Cancel_Invoice '" + Invoiceno + "'");
            return Cargotype;
        }

        public DataTable CheckAckAssessmentDetails(string Invoiceno,string WorkYear)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_Validation_ACK_Status_Other '" + Invoiceno + "','" + WorkYear + "'");
            return Cargotype;
        }

        public DataTable GetAllocatedSpace()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GetallowtedSpace");
            return LoginDT;
        }

        public DataTable CancelOtherInvoiceProforma(string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_CancelOtherinvoicePorforma '" + AssessNo + "','" + workyear + "'," + userId + "");
            return Cargotype;
        }

        public DataTable SubmitFinalotherInvoiceproforma(string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_Insert_Import_Other_Assessment_Final '" + AssessNo + "','" + workyear + "'," + userId + "");
            return Cargotype;
        }

        public DataSet GetDropDownListExportProformaInvoice()
        {
            DataSet DS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDataSets("USP_GetExportInvoiceProforma_DropDown");
            return DS;
        }

        public DataTable GetExportAddedTariffsettingDetails(int Userid)
        {
            DataTable PaymentDS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PaymentDS = db.sub_GetDatatable("USP_GetExportTariffSettingDetails '" + Userid + "'");
            return PaymentDS;
        }

        public DataTable GetGSTSearchExportProforma(string SearchText)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_get_list_Gst_EDI '" + SearchText.Replace("'", "''") + "'");
            return LoginDT;
        }

        public DataTable GetExportadditionialChargesdetails(string SearchOn, string number)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_Showadditional '" + SearchOn + "','" + number + "'");
            return LoginDT;
        }
        public DataSet GetEmptyInDropdown()
        {
            DataSet DriverListDL = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            DriverListDL = db.sub_GetDataSets("USP_GetDropdownForEmptyIn");
            return DriverListDL;
        }

        public DataSet GetDropDownListExporttariffsetting()
        {
            DataSet DS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDataSets("USP_GetExportTariffSettingDetails_DropDown");
            return DS;
        }

        public DataTable GetExporttariffDetails(string TariffId, string deliveryType, string containerType)
        {
            DataTable PaymentDS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PaymentDS = db.sub_GetDatatable("Get_SPViewTariffDetails '" + TariffId + "','" + deliveryType + "','" + containerType + "'");
            return PaymentDS;
        }

        public DataTable GetShippingBillDetailsSBWise(string Containerno)
        {
            DataTable DS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDatatable("USP_Export_DOCK_SB_Dets_INV_SBWise '" + Containerno + "'");
            return DS;
        }

        public DataTable GetContainerWiseDetailForExportProforma(string Containerno, string MovementType)
        {
            DataTable DS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDatatable("USP_ShowExportContainerWiseDetails '" + Containerno + "','" + MovementType + "'");
            return DS;
        }

        public DataTable GetShippingBillDetailsContainerWise(string Containerno)
        {
            DataTable DS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDatatable("USP_Export_Buffer_SB_Dets_INV '" + Containerno + "'");
            return DS;
        }
        public DataTable CancelExportInvoiceProforma(string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_CancelinvoiceExportPorforma '" + AssessNo + "','" + workyear + "'," + userId + "");
            return Cargotype;
        }

        public DataTable gettheExportInvoicedocseries2(string AssessNo, string workyear, string assesstype)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("usp_gettheExportInvoicedocseries '" + assesstype + "'");
            return Cargotype;
        }

        public DataTable GetExportMaxallowid(string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("SELECT isnull(MAX(exp_assessno),0) as[exp_assessno] FROM settings_assess WHERE imp_assessWY='" + workyear + "'");
            return Cargotype;
        }

        public DataTable SubmitFinalExportInvoiceproforma(string AssessNo, string workyear, int userId, string strinvoiceNo, int allowind)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_Insert_Export_Assessment_Final '" + AssessNo + "','" + workyear + "'," + userId + ",'" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy HH:mm") + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy HH:mm") + "'," + userId + ",'" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy HH:mm") + "','" + allowind + "','" + strinvoiceNo + "'");
            return Cargotype;
        }


        public DataTable CancelExportAssessmentDetails(string remarks, string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_CancelExportAssessNo'" + remarks + "','" + AssessNo + "','" + workyear + "'," + userId + "");
            return Cargotype;
        }
        public DataTable GetSBWiseDetailForExportProforma(string Containerno)
        {
            DataTable DS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDatatable("USP_ShowExportContainerWiseDetails_fACTOY_sb '" + Containerno + "'");
            return DS;
        }

        public DataTable GetBLExportProforma(string Containerno)
        {
            DataTable DS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDatatable("USP_ShowExportContainerWiseDetails_Dock_BLWISE '" + Containerno + "'");
            return DS;
        }

        public DataTable GetBLDetailsSBWise(string Containerno)
        {
            DataTable DS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDatatable("USP_Export_DOCK_SB_Dets_INV_SBWise_BLWise '" + Containerno + "'");
            return DS;
        }

        public DataTable GetadditionialChanrgesdetails(string workyear, string IgmN0, string ItemNo)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_Fetch_Additional_Charges_IM '" + IgmN0 + "','" + ItemNo + "'");
            return LoginDT;
        }

        public DataTable GetUpdateRmsDetails(string IgmN0, string ItemNo)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_Fetch_RMS '" + IgmN0 + "','" + ItemNo + "'");
            return LoginDT;
        }

        public DataTable UpdateCargoTypeDetails(string IGmNo, string ItemNo, string Consignee, string Con_IGMAddress,
           string NConsignee, string NCon_IGMAddres, string IGM_BLNo, string IGM_GoodsDesc, string IGM_GrossWt, string IGM_WtUnit,
           string IGM_Qty, string IGM_PackType, string Remarks, int userId)
        {
            string IGM_MT_Wt = "";
            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("SP_SaveImpAmmendmentSeas '" + IGmNo + "','" + ItemNo + "','" + ItemNo + "','" + Consignee + "','" + Con_IGMAddress + "','" + NConsignee + "'," +
                "'" + NCon_IGMAddres + "','" + IGM_BLNo + "','" + IGM_GoodsDesc + "','" + IGM_GrossWt + "','" + IGM_WtUnit + "','" + IGM_Qty + "','" + IGM_PackType + "'," +
                "'" + IGM_MT_Wt + "','" + Remarks + "'," + userId + "");
            return Cargotype;
        }

        public DataTable CancelInvoiceProforma(string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_CancelinvoicePorforma '" + AssessNo + "','" + workyear + "'," + userId + "");
            return Cargotype;
        }

        public DataTable SubmitInvoiceDetails(string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_JOType '" + AssessNo + "','" + workyear + "'");
            return Cargotype;
        }

        public DataTable gettheInvoicedocseries2()
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("select Code from docseries where documentName='IMPINVOICE'");
            return Cargotype;
        }

        public DataTable gettheInvoicedocseries()
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("select Code from docseries where documentName='IMPINVOICE'");
            return Cargotype;
        }

        public DataTable GetMaxallowid(string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_GetTransportassess_no '" + workyear + "'");
            return Cargotype;
        }

        public DataTable SubmitFinalInvoiceproforma(string AssessNo, string workyear, int userId, string transid, string AssessType)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_Insert_party_Assessment_Final '" + AssessNo + "','" + workyear + "'," + userId + ",'" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy HH:mm") + "','" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy HH:mm") + "'," + userId + ",'" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy HH:mm") + "','" + transid + "','" + AssessType + "'");
            return Cargotype;
        }


        public DataSet GetDropDownListImporttariffsetting()
        {
            DataSet DS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDataSets("USP_GetTariffSettingDetails_DropDown");
            return DS;
        }

        public DataTable GetAllVehicle(string Location, string Activity, string ContainerNo)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_SHOWVEHICLEMODIFY_Web '" + Location + "','" + Activity + "','" + ContainerNo + "'");

            return dt;
        }


        public DataTable UpdateModifyVehicle(string Location, string Activity, string ContainerNo, string EntryID, string VehicleNo, string OldVehicleNo, string Remarks, int Userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_UPDATEVEHICLEMODIFY_Web '" + Location + "','" + Activity + "','" + ContainerNo + "','" + EntryID + "','" + VehicleNo + "','" + OldVehicleNo + "','" + Remarks + "','" + Userid + "'");

            return dt;
        }

        public DataTable OtherAssessInvoice(string AssessNo, string WorkYear, string Category)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");OtherAssessInvoice
            dt = db.sub_GetDatatable("usp_invoice_searchMISC'" + AssessNo + "','" + WorkYear + "','" + Category + "'");

            return dt;
        }

        public DataTable GetVendorListForExpenses()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_vendorListforexpensesEdit");

            return ExpenseDt;
        }


        public DataTable GetTaxListForExpenses()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_settingTaxesForExpenses ");

            return ExpenseDt;
        }
        public DataTable CancelExpenseID(int Entryid, int transexpenseID, int Userid)
        {
            DataTable TransExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            TransExpenseDt = db.sub_GetDatatable("USP_DeleteRecordFromtransportExpense " + Entryid + "," + transexpenseID + ",'" + Userid + "'");

            return TransExpenseDt;
        }

        public DataTable GetoffloadingTransdetails(string Containerno)
        {
            DataTable VoucherNumereDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            VoucherNumereDL = db.sub_GetDatatable("USP_GETOffloadingTransdetails '" + Containerno + "'");

            return VoucherNumereDL;
        }
        public DataTable GetVendorGTDetails(string VendorID)
            {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_getVendorGSTDetails_ForTransportExpenses '" + VendorID + "'");
            return dt;
        }
        public DataTable GetTransportDOcList(int Entryid)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = db.sub_GetDatatable("USP_GeTransportExpensesDocumentDetails  " + Entryid + "");
            return AttachmentDT;
        }
        public DataTable GetContainerTypeDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetContainerTypeDetails");

            return dt;
        }
        public DataTable Getimport_accountmasterDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_import_accountmasterDetails");

            return dt;
        }

        public DataTable GetTraiffDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetTariffDetails");

            return dt;
        }
        public DataTable GetExportTypeDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetExportMovementTypeDetails");

            return dt;
        }
        public DataTable GetBondTypeDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetBondMovementTypeDetails");

            return dt;
        }
        public DataTable GetDomesticTypeDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetDomesticMovementTypeDetails");

            return dt;
        }
        public DataTable GetBondTraiffDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetBondTariffDetails");

            return dt;
        }

        public DataTable GetDomesticTraiffDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_GetDomesticTariffDetails");

            return dt;
        }

        public DataTable GetImpJoDetails(string ContainerNo)
        {
            DataTable VesselDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            VesselDT = db.sub_GetDatatable("USP_Get_Import_Container_D  '" + ContainerNo + "'");
            return VesselDT;
        }
       //Marging File

        public DataTable SaveDetailsDL(string ContainerNo, int ContainerTypeID, int JoTypeId, int Size, int Cargotypeid, string SealNo, string BL_Number, int Qty, float GrossWT, int ISOID, int Tare_Weight, string Remarks, int AddedBy)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_UPDATE_CONTAINER_DETAILS";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ContainerNo", ContainerNo);
                    objCommand.Parameters.AddWithValue("@ContainerTypeID", ContainerTypeID);
                    objCommand.Parameters.AddWithValue("@JoTypeId", JoTypeId);
                    objCommand.Parameters.AddWithValue("@Size", Size);
                    objCommand.Parameters.AddWithValue("@Cargotypeid", Cargotypeid);
                    objCommand.Parameters.AddWithValue("@SealNo", SealNo);
                    objCommand.Parameters.AddWithValue("@BL_Number", BL_Number);
                    objCommand.Parameters.AddWithValue("@Qty", Qty);
                    objCommand.Parameters.AddWithValue("@GrossWT", GrossWT);
                    objCommand.Parameters.AddWithValue("@ISOID", ISOID);
                    objCommand.Parameters.AddWithValue("@Tare_Weight", Tare_Weight);
                    objCommand.Parameters.AddWithValue("@Remarks", Remarks);
                    objCommand.Parameters.AddWithValue("@AddedBy", AddedBy);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public DataTable LRContainerSearch(string Containerno)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("Usp_Containerno_Search '" + Containerno + "'");
            return LoginDT;
        }
        public DataTable SaveDetailsDL(string LrDate, string ContainerNo, int ENTRYID, int LineID, int Size, int ContainerTypeID, int AddedBy)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_INSERT_DOMESTIC_HUB_LR";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@LrDate", LrDate);
                    objCommand.Parameters.AddWithValue("@CONTAINERNO", ContainerNo);
                    objCommand.Parameters.AddWithValue("@ENTRYID", ENTRYID);
                    objCommand.Parameters.AddWithValue("@SLID", LineID);
                    objCommand.Parameters.AddWithValue("@SIZE", Size);
                    objCommand.Parameters.AddWithValue("@CONTAINERTYPEID", ContainerTypeID);
                    objCommand.Parameters.AddWithValue("@ADDEDBY", AddedBy);


                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        // code by Pk
        public DataTable InsertExcelDomesticHubData(DataTable domesticHubData, int AddedBy, DateTime AddedOn)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_InsertDomesticHubData";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@AddedBy", AddedBy);
                    objCommand.Parameters.AddWithValue("@AddedOn", AddedOn);
                    if (domesticHubData != null)
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@PT_InsertDomesticHub";
                        param.Value = domesticHubData;
                        param.TypeName = "PT_InsertDomesticHub";
                        param.SqlDbType = SqlDbType.Structured;
                        objCommand.Parameters.Add(param);
                    }
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable DomesticTrainData(Int64 TripNo, string TrainNo, string Origin, string Destination, int userId)
        {
            DataTable DriverJODL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverJODL = db.sub_GetDatatable("USP_INSERT_Domestic_TrainLoad_M " + TripNo + ",'" + TrainNo + "','" + Origin + "','" + Destination + "'," + userId + "");
            return DriverJODL;

        }

        public DataTable SubmitDomesticTrainTripData(Int64 TripNo, string TrainNo, int userId)
        {
            DataTable DriverJODL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DriverJODL = db.sub_GetDatatable("USP_SUBMIT_TRAIN_Domestic_TrainLoad_M_TRIP_DATA " + TripNo + ",'" + TrainNo + "'," + userId + "");
            return DriverJODL;
        }

        public DataTable AddDomesticWagonDataOnly(string TrainNo, int Userid, Int64 TripNo)
        {

            DataTable ContainerJODS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            //  string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            ContainerJODS = db.sub_GetDatatable("USP_Domestic_INSERT_TEMP_WAGONNODATAOnly  '" + TrainNo + "'," + Userid + "," + TripNo + "");
            return ContainerJODS;
        }
        public DataTable GetdOMESTICTripListForModify()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GET_Domestic_loading_TRIPLIST_Modify");
            return LoginDT;
        }


        // code by prashant K

        public DataTable GetELocationame()
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetLocations";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetDetailsContainerWise(string containerNo)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetContainerWiseDetail";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ContainerNo", containerNo);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable SaveDomesticHubOffLoading(EN.DomesticHubOffLoadDts hubOffLoadingData, int userid)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_InsertHubOffData";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@Entrydate", hubOffLoadingData.Entrydate);
                    objCommand.Parameters.AddWithValue("@ContainerNo", hubOffLoadingData.ContainerNo);
                    objCommand.Parameters.AddWithValue("@EntryID", hubOffLoadingData.EntryID);
                    objCommand.Parameters.AddWithValue("@TrailerNo", hubOffLoadingData.TrailerNo);
                    objCommand.Parameters.AddWithValue("@LocationID", hubOffLoadingData.LocationID);
                    objCommand.Parameters.AddWithValue("@Remarks", hubOffLoadingData.Remarks);
                    objCommand.Parameters.AddWithValue("@AddedBy", userid);
                    objCommand.Parameters.AddWithValue("@AddedOn", hubOffLoadingData.AddedOn);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable DomesticLorryReceiptReportList(string FromDate, string ToDate, string category, string size)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_ShowLorryReceiptReport_Domestic '" + FromDate + "','" + ToDate + "','" + category + "','" + size + "'");
            return dt;
        }

        public DataTable GetTripList()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GET_TRIPLIST");
            return LoginDT;
        }

        public void DeleteWagonTempData(int userid)
        {
            HC.DBOperations db = new HC.DBOperations();
            int i = db.sub_ExecuteNonQuery("[USP_DeleteWagonDataFromTemp] " + userid + "");
        }
        public DataTable DeleteWagonTempData(Int64 TripNo, int Userid)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_delete_TEMP_WAGONNODATA " + TripNo + "," + Userid + "");
            return LoginDT;
        }
        public DataTable GetTrainNo(Int64 TripNo)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GET_TRANINO_AGAINST_TRIPNO " + TripNo + "");
            return LoginDT;
        }
        public DataTable GetWagonList(string TrainNo)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GET_Wagonlist_AGAINST_TrainNo " + TrainNo + "");
            return LoginDT;
        }
        public DataTable GetContainerList()
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GET_ContainerList1");
            return LoginDT;
        }
        public DataTable getTripDetails()
        {
            DataTable tripDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            tripDT = db.sub_GetDatatable("USP_TRIP_DETS_NOT_ARRIVED");
            return tripDT;
        }
        public DataSet GetExportSizeDS(string ContainerNo, int Number, string WagonNo, int TripNo, int UserID)
        {
            DataSet SizeDS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            SizeDS = db.sub_GetDataSets("USP_UPDATE_temp_EXpORT_wagonNoData '" + ContainerNo + "','" + Number + "','" + WagonNo + "','" + TripNo + "','" + UserID + "'");
            return SizeDS;
        }
        public DataTable DeletetheContainernofromwagon(string ContainerNo, int Number, string WagonNo, int TripNo, int userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_DeleteContainerN_From_Wagaon '" + ContainerNo + "','" + Number + "','" + WagonNo + "','" + TripNo + "','" + userid + "'");

            return dt;
        }

        public DataTable UpdateExportTempData(string ContainerNo, int Number, string WagonNo, int TripNo, int UserID, int Size, string NetWt1, string TareWt1, string ActualWt1, string Status1, string commodity1, string POL1)
        {
            DataTable tripDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            tripDT = db.sub_GetDatatable("USP_UPDATE_TEMP_Export_WAGONDATA '" + ContainerNo + "','" + Number + "','" + WagonNo + "','" + TripNo + "','" + UserID + "','" + Size + "','" + NetWt1 + "','" + TareWt1 + "','" + ActualWt1 + "','" + Status1 + "','" + commodity1 + "','" + POL1 + "'");
            return tripDT;
        }
        public DataSet AddWagonData(string WagonNo, string ContainerNo1, string ContainerNo2, int Userid, Int64 TripNo)
        {

            DataSet ContainerJODS = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            //  string str = "USP_AddTemoContainerJODL  " + JONO + ",'" + ContainerNo + "'," + Size + ",'" + FL + "','" + ISOCode + "'," + Cargotypeid + "," + Commodity_group_id + "," + Userid + "";
            ContainerJODS = db.sub_GetDataSets("USP_INSERT_TEMP_WAGONNODATA  '" + WagonNo + "','" + ContainerNo1 + "','" + ContainerNo2 + "'," + Userid + "," + TripNo + "");
            return ContainerJODS;
        }
        public DataTable GetSize1(string ContainerNo1)
        {
            DataTable TripList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TripList = db.sub_GetDatatable("USP_GET_ContainerList_SizeWise '" + ContainerNo1 + "'");

            return TripList;
        }

        public DataTable SaveJVDetails(string CustomerName, string CustomerName2, string PendingReceipt, string TransAmt, string Remarks, int Userid,string EntryType,string Criteria,string JVFor)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_INSERT_JV_AMOUNT '" + CustomerName + "','" + CustomerName2 + "','" + PendingReceipt + "','" + TransAmt + "','" + Remarks + "','" + Userid + "','" + EntryType + "','" + Criteria + "','" + JVFor + "'" );

            return dt;
        }

        public DataTable SaveDetailsCRDL(string InvoiceNo, string Remarks, int AddedBy)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_UPDATE_CNRem_DETAILS";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);

                    objCommand.Parameters.AddWithValue("@Remarks", Remarks);
                    objCommand.Parameters.AddWithValue("@AddedBy", AddedBy);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable DeleteContainerExport(int EntryID, string ContainerNo, string Remarks, int AddedBy)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_CANCEL_EXPORT_CONT";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@EntryID", EntryID);
                    objCommand.Parameters.AddWithValue("@ContainerNo", ContainerNo);
                    objCommand.Parameters.AddWithValue("@Remarks", Remarks);
                    objCommand.Parameters.AddWithValue("@AddedBy", AddedBy);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public DataTable SaveExportTariffSettingDetails(int Userid, DataTable dataTable)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_MODIFY_CLP_DETAILS";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ADDEDBY", Userid);
                    if (dataTable != null)
                    {
                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@UTYPE_EXPORT_CLP_MODIFY";
                        param1.Value = dataTable;
                        param1.TypeName = "UTYPE_EXPORT_CLP_MODIFY";
                        param1.SqlDbType = SqlDbType.Structured;
                        objCommand.Parameters.Add(param1);
                    }
                    DataTable dtClass = new DataTable();


                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;

                        _Data.Fill(dtClass);
                    }
                    connection.Close();

                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetledgerDetailsForXML()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_XML_Ledger");

            return dt;
        }

        public DataTable GetInvoicesDetailsForXML(DataTable table)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Invoice_tally_Ledger");

            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_Invoice_tally_Ledger";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandTimeout = 600;
                    if (table != null)
                    {
                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@PT_XMLReceiptDownload";
                        param1.Value = table;
                        param1.TypeName = "PT_XMLReceiptDownload";
                        param1.SqlDbType = SqlDbType.Structured;
                        objCommand.Parameters.Add(param1);
                    }

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }


        }

        public DataTable CreditNoteSeach(string ddlCategory, string SearchType, string Search)
        {
            DataTable JODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JODT = db.sub_GetDatatable("USP_INVOICES_FOR_CREDIT_NOTES '" + ddlCategory + "','" + SearchType + "','" + Search + "'");
            return JODT;
        }

        public DataTable CreditNoteSeachGrid(int Userid)
        {
            DataTable JODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JODT = db.sub_GetDatatable("USP_fetch_from_temp_credit_note '" + Userid + "'");
            return JODT;
        }
        public DataSet CreditNote(string AssessNo, string WorkYear, int Userid, string Category)
        {
            DataSet JOD = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            JOD = db.sub_GetDataSets("USP_fetch_details_for_credit_note '" + AssessNo + "','" + WorkYear + "','" + Userid + "','" + Category + "'");
            return JOD;
        }

        //Reverse Debit Note 

        public DataSet GetReverseDebitShow(string CreditNoteNo)
        {
            DataSet JOD = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            JOD = db.sub_GetDataSets("USP_CreditNote_Data_Show '" + CreditNoteNo + "'");
            return JOD;
        }
        public DataTable GatDebitGridNote(string Activity, string OldCredit, string WorkYear)
        {
            DataTable JODT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            JODT = db.sub_GetDatatable("USP_Credit_Grid_Data_Show '" + Activity + "','" + OldCredit + "','" + WorkYear + "'");
            return JODT;
        }

        public DataTable GetCreditNoteList(String CrNote)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_Show_CR_Dets '" + CrNote + "'");
            return LoginDT;
        }

        public DataTable GetLedgerDetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_ShowTallyLedger");

            return dt;
        }


        // Prashant K

        public DataTable getSelfAuditDDLValues()
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetSelfAuditDDL";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public DataTable SaveInvoiceAuditForm(EN.Audit_Invoice_Note invoiceAuditForm, int userid)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_InsertAuditNote";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@NoteDate", invoiceAuditForm.NoteDate);
                    objCommand.Parameters.AddWithValue("@InvoiceNo", invoiceAuditForm.InvoivceNo);
                    objCommand.Parameters.AddWithValue("@NoteDesc", invoiceAuditForm.NoteDesc);
                    objCommand.Parameters.AddWithValue("@NoteBy", userid);
                    objCommand.Parameters.AddWithValue("@NoteOn", invoiceAuditForm.NoteOn);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }
        public DataTable GetCategoryList()
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetCreditNoteCatgory";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public DataTable GetAccountListMaster()
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetAccountMaster";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public DataTable ShowJONoForDDL(string containerNo)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GET_JONo";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ContainerNo", containerNo);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


    
        public DataTable GetAdditionalRemarksDetails(string containerNo, int jONo)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GETInfoForUpdateRemarks";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ContainerNo", containerNo);
                    objCommand.Parameters.AddWithValue("@JONo", jONo);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public DataTable SaveRemarksData(EN.ImportUpdateCharges formData, int userID)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_InsertRemarksFormData";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ContainerNo", formData.ContainerNo);
                    objCommand.Parameters.AddWithValue("@JONo", formData.JONo);

                    if (formData.IGMNo == null)
                    {
                        objCommand.Parameters.AddWithValue("@IGMNo", " ");
                    }
                    else
                    {
                        objCommand.Parameters.AddWithValue("@IGMNo", formData.IGMNo);
                    }

                    objCommand.Parameters.AddWithValue("@itemNo", formData.ItemNo);
                    objCommand.Parameters.AddWithValue("@Remarks", formData.Remarks);
                    objCommand.Parameters.AddWithValue("@FirstpartyName", formData.FirstPartyName);
                    objCommand.Parameters.AddWithValue("@SecPartyName", formData.SecondPartyName);
                    objCommand.Parameters.AddWithValue("@AddedBy", userID);
                    objCommand.Parameters.AddWithValue("@AddedOn", formData.AddedOn);
                    objCommand.Parameters.AddWithValue("@AccountID", formData.AccountID);
                    objCommand.Parameters.AddWithValue("@Amount", formData.AmountCollect);

                    if (formData.BLNo == null)
                    {
                        objCommand.Parameters.AddWithValue("@BLNo", " ");
                    }
                    else
                    {
                        objCommand.Parameters.AddWithValue("@BLNo", formData.BLNo);
                    }

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable UpdateDocForInv(int GPNo, int EntryID, string ContainerNo, string Remarks, int AddedBy)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_UPDATE_EXPORT_DOCUMENTS_FOR_INV";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@GPNo", GPNo);
                    objCommand.Parameters.AddWithValue("@EntryID", EntryID);
                    objCommand.Parameters.AddWithValue("@ContainerNo", ContainerNo);
                    objCommand.Parameters.AddWithValue("@Remarks", Remarks);
                    objCommand.Parameters.AddWithValue("@ModifyBy", AddedBy);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public DataTable UpdateCargoTypeInv(string containerNo, int EntryID, int Cargotypeid, string Cargo_Type, string Remarks, int AddedBy)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_UPDATE_EXPORT_Cargo_Type";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@containerNo", containerNo);
                    objCommand.Parameters.AddWithValue("@EntryID", EntryID);
                    objCommand.Parameters.AddWithValue("@Cargotypeid", Cargotypeid);
                    objCommand.Parameters.AddWithValue("@Cargo_Type", Cargo_Type);
                    objCommand.Parameters.AddWithValue("@Remarks", Remarks);
                    objCommand.Parameters.AddWithValue("@ModifyBy", AddedBy);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public int UnlockDetails(string ddlCriteria, string ddlType, string txtInvoice, string Fromdate, string Todate)
        {

            HC.DBOperations db = new HC.DBOperations();
            int i;
            i = db.sub_ExecuteNonQuery("usp_update_unlocktransactions '" + ddlCriteria + "','" + ddlType + "','" + txtInvoice + "','" + Fromdate + "','" + Todate + "'");

            return i;
        }



        // other Invoies

        // Other Invoice Details
        public DataTable CancelOtherInvoiceProforma1(string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_CancelOtherinvoicePorforma '" + AssessNo + "','" + workyear + "'," + userId + "");
            return Cargotype;
        }

        public DataTable SubmitFinalotherInvoiceproforma1(string AssessNo, string workyear, int userId)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_Insert_Import_Other_Assessment_Final '" + AssessNo + "','" + workyear + "'," + userId + "");
            return Cargotype;
        }

        public DataTable SaveFreeInvoiceDataDL(int AGID, string FromDate, string ToDate, string Remarks, bool IsActive)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_MAKE_FREE_WARAI_CUST";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@AGID", AGID);
                    objCommand.Parameters.AddWithValue("@FromDate", FromDate);
                    objCommand.Parameters.AddWithValue("@ToDate", ToDate);
                    objCommand.Parameters.AddWithValue("@Remarks", Remarks);
                    objCommand.Parameters.AddWithValue("@IsActive", IsActive);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }
        public DataTable UpdateSBDirectCartingAllowDetails(string SBNo, int EntryID, string Remarks, int AddedBy)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_UPDATE_SB_ALLOW";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@SBNo", SBNo);
                    objCommand.Parameters.AddWithValue("@EntryID", EntryID);
                    objCommand.Parameters.AddWithValue("@Remarks", Remarks);
                    objCommand.Parameters.AddWithValue("@ModifyBy", AddedBy);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable getDuplicateDataForBankReco(string Tansdate, string Desc, string Refno)
        {
            DataTable PaymentDS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            PaymentDS = db.sub_GetDatatable("USP_CheckduplicateforBankReco '" + Tansdate + "','" + Desc + "','" + Refno + "'");
            return PaymentDS;
        }

        public DataTable SaveUpdateOpeningDtl(EN.UpdateOpeningAmount fromData, int userid)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_InsertOpeningAmount";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@TransactionType", fromData.TransactionType);
                    objCommand.Parameters.AddWithValue("@CustomerID", fromData.CustomerID);
                    objCommand.Parameters.AddWithValue("@OpeningAmount", fromData.OpeningAmount);
                    objCommand.Parameters.AddWithValue("@ReferenceNo", fromData.ReferenceNo);
                    objCommand.Parameters.AddWithValue("@ReferenceDate", fromData.ReferenceDate);
                    objCommand.Parameters.AddWithValue("@AddedBy", userid);
                    objCommand.Parameters.AddWithValue("@AddedOn", fromData.AddedOn);
                    objCommand.Parameters.AddWithValue("@CriteriaID", fromData.Criteria);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable SaveHoldDtls(EN.UpdateOpeningAmount fromData, int userid)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_InsertHoldDtls";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ReferenceNo", fromData.ReferenceNo);
                    objCommand.Parameters.AddWithValue("@Remarks", fromData.Remarks);
                    objCommand.Parameters.AddWithValue("@AddedBy", userid);
                    objCommand.Parameters.AddWithValue("@AddedOn", fromData.AddedOn);


                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public DataTable SaveVendorDetails(string Vendor,int Party, bool IsActive, int AddedBy,int EntryID)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_FUEL_VENDOR";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@Vendor", Vendor);
                    objCommand.Parameters.AddWithValue("@Party", Party);


                    objCommand.Parameters.AddWithValue("@IsActive", IsActive);
                    objCommand.Parameters.AddWithValue("@AddedBy", AddedBy);
                    objCommand.Parameters.AddWithValue("@EntryID", EntryID);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable SaveHSNCodeDL(string HSNCode, string Description, int AddedBy)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_INSERT_HSN";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@HSNCode", HSNCode);

                    objCommand.Parameters.AddWithValue("@Description", Description);
                    objCommand.Parameters.AddWithValue("@AddedBy", AddedBy);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }



        // developedd by prathamesh
        public DataTable GetMaterialsReceivingConfirmationsList()
        {
            string sqlCommandString;
            TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetMaterialsReceivingConfirmationsList";
                    //sqlCommandString = "USP_JobCardDetails";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@JCNO", JCNo);
                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }

        public DataTable UpdateMaterialGroup(string AutoID, int MaterialGroupID)
        {
            string sqlCommandString;
            TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_UpdateMaterialGroupID";
                    //sqlCommandString = "USP_JobCardDetails";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@AutoID", AutoID);
                    objCommand.Parameters.AddWithValue("@MaterialGroupID", MaterialGroupID);
                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        //public DataTable GetMappedMaterialsReceivingConfirmationsList(string ItemName, string AllAutoID)
        //{
        //    string sqlCommandString;
        //    TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
        //    using (SqlConnection connection = objConn.GetConnection)
        //    {
        //        //connection.Open();
        //        try
        //        {
        //            if (connection.State != ConnectionState.Open)
        //            {
        //                connection.Open();
        //            }
        //            sqlCommandString = "USP_GetMappedMaterialsReceivingConfirmationsList";
        //            //sqlCommandString = "USP_JobCardDetails";
        //            SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
        //            objCommand.CommandType = CommandType.StoredProcedure;
        //            objCommand.Parameters.AddWithValue("@ItemName", ItemName);
        //            objCommand.Parameters.AddWithValue("@AllAutoID", AllAutoID);
        //            DataTable dtLoginDetails = new DataTable();

        //            using (SqlDataAdapter _Data = new SqlDataAdapter())
        //            {
        //                _Data.SelectCommand = objCommand;
        //                _Data.Fill(dtLoginDetails);
        //            }
        //            connection.Close();
        //            return dtLoginDetails;
        //        }
        //        catch (Exception ex)
        //        {
        //            if (connection.State == ConnectionState.Open)
        //            {
        //                connection.Close();
        //            }
        //            throw ex;
        //        }
        //    }

        //}

        public DataTable GetCartingIDDetailsForworkOrder_LashingAndChocking(string cartingID)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_GetcartingDetailsforworkorder '" + cartingID + "'");
            return ImportsearchHoldingListDL;
        }

        public DataTable GetSBNODetailsForworkOrder_LashingAndChocking(string RequestID)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_GetSBNODetailsforworkorder '" + RequestID + "'");
            return ImportsearchHoldingListDL;
        }
        public DataTable GetRequestDetailsForworkOrder_LashingAndChocking(string RequestID)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_GetSBrequestDetailsforworkorder '" + RequestID + "'");
            return ImportsearchHoldingListDL;
        }

        public DataTable GetContainerwiseworkOrderDetails_LashingAndChocking(string RequestID)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_GetContainerWiseworkorderdetails '" + RequestID + "'");
            return ImportsearchHoldingListDL;
        }

        public DataTable GetEditExportDetailsgrid(string WONo)
        {
            DataTable DlList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DlList = db.sub_GetDatatable("usp_grid_bind_Export_work_order '" + WONo + "'");
            return DlList;

        }
        public DataTable GetStuffingRequestDone(string RequestID, string WOType)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_CheckCarting_stuffingDone '" + RequestID + "','" + WOType + "'");
            return ImportsearchHoldingListDL;
        }

        public DataTable ClearExportTempWorkorder(string ContainerNo, int UserID)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_ClearExportTempWorkorder'" + ContainerNo + "','" + UserID + "'");
            return ImportsearchHoldingListDL;
        }
        public DataTable SaveImportIGMDetails(DataTable ContainerList, int Userid)
        {
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("UserId", Userid);


            DataTable ContainerJODS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ContainerJODS = db.SaveImportIGMDetails("USP_SaveImportIGMDetails", lstparam, ContainerList);
            return ContainerJODS;

        }

        // developed by prathamesh

        public DataTable GetLashingAndChockingExpVsRevenueReport(string FromDate, string ToDate)
        {
            DataTable ImportsearchHoldingListDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ImportsearchHoldingListDL = db.sub_GetDatatable("USP_GetLashingAndChockingExpVsRevenueReport'" + FromDate + "','" + ToDate + "'");
            return ImportsearchHoldingListDL;
        }

      
        public DataTable GetVendorGTDetailsForFuelBill(string VendorID)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GETVendorGSTDetails_For_fuelbillVerification '" + VendorID + "'");
            return dt;
        }

        public DataTable getMenuList(string SearchText, int userid)
        {
            string sqlCommandString;
            TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_Menu_Details_User_Dashboard";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@SearchText", SearchText);
                    objCommand.Parameters.AddWithValue("@UserID", userid);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }

        public DataTable SetFavouriteMenu(int MenuID, int UserID)
        {
            string sqlCommandString;
            TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_INSERTFAVOURITEMENUEM";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@MenuID", MenuID);
                    objCommand.Parameters.AddWithValue("@UserID", UserID);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }

        public DataTable CheckContainerGenInvoiceDL(string Containerno)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_CHECK_OTHER_FINAL_CON '" + Containerno + "'");
            return Cargotype;
        }

        public DataTable UpdateExcessAmtDL(string ReceiptNo, float Amount, int AddedBy)
        {


            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_Access_Update";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@ReceiptNo", ReceiptNo);

                    objCommand.Parameters.AddWithValue("@Amount", Amount);
                    objCommand.Parameters.AddWithValue("@AddedBy", AddedBy);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetStockSlipnodetails(string ddlFuelType, string ddlCostCenter1)
        {
            DataTable TrailerNoDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            TrailerNoDL = db.sub_GetDatatable("USP_GetStockInternalSlip '" + ddlFuelType + "','" + ddlCostCenter1 + "'");
            return TrailerNoDL;
        }

        public DataTable GetStockSLipWiseFueldetails(string SLipNo, int Userid)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_getStockSlipFuelRate '" + SLipNo + "','" + Userid + "'");

            return dt;
        }


        public DataTable GetMappedMaterialsReceivingConfirmationsList(string ItemName, string AllAutoID, DataTable MaterialDetails)
        {
            string sqlCommandString;
            TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetMappedMaterialsReceivingConfirmationsList";
                    //sqlCommandString = "USP_JobCardDetails";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ItemName", ItemName);
                    objCommand.Parameters.AddWithValue("@AllAutoID", AllAutoID);

                    if (MaterialDetails != null)
                    {
                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@PT_MaterialDetailsLAndC";
                        param1.Value = MaterialDetails;
                        param1.TypeName = "PT_MaterialDetailsLAndC";
                        param1.SqlDbType = SqlDbType.Structured;
                        objCommand.Parameters.Add(param1);
                    }
                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }

        public DataTable GetActivityWiseDetails()
        {
            DataTable HorseNumberDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            HorseNumberDL = db.sub_GetDatatable("USP_GETMovtype");
            return HorseNumberDL;
        }
        public DataTable GetTransportbillingcode(string Transid)
        {

            DataTable Cargotype = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            Cargotype = db.sub_GetDatatable("USP_GetTransport_Bill_Prefix '" + Transid + "'");
            return Cargotype;
        }
        public DataTable GetLocationMasterdetails()
        {
            DataTable DS = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DS = db.sub_GetDatatable("USP_GetLocationMasterDropDown");
            return DS;
        }
        public DataTable GetCustomerTariffDetails(string SearchText)
        {
            DataTable LoginDT = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoginDT = db.sub_GetDatatable("USP_GetCustomerWiseTariff '" + SearchText.Replace("'", "''") + "'");
            return LoginDT;
        }
        public DataTable GetStatusWiseactivity(string Status)
        {
            DataTable HorseNumberDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            HorseNumberDL = db.sub_GetDatatable("USP_GetStatusWiseactivity '" + Status + "'");
            return HorseNumberDL;
        }

        ////================================WORK ORDER MATERIAL ================================================
        ///CODE BY SAGAR
        public DataTable SaveMaterialDL(int Userid, int DepartID, string Remarks, DataTable dataTable)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_INSERT_WO_MATERIAL";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ADDEDBY", Userid);
                    objCommand.Parameters.AddWithValue("@DepartID", DepartID);
                    objCommand.Parameters.AddWithValue("@Remarks", Remarks);
                    if (dataTable != null)
                    {
                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@UTYPE_INSERT_WO_MATERAL_DET";
                        param1.Value = dataTable;
                        param1.TypeName = "UTYPE_INSERT_WO_MATERAL_DET";
                        param1.SqlDbType = SqlDbType.Structured;
                        objCommand.Parameters.Add(param1);
                    }
                    DataTable dtClass = new DataTable();


                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;

                        _Data.Fill(dtClass);
                    }
                    connection.Close();

                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public DataTable GetDrivertype()
        {
            DataTable HorseNumberDL = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            HorseNumberDL = db.sub_GetDatatable("USP_Driver_type");
            return HorseNumberDL;
        }
        public DataTable GetTrip()
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    DataTable dt = new DataTable();
                    sqlCommandString = "USP_Movement_Trip_DropDown";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dt);
                    }
                    connection.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetActivity()
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    DataTable dt = new DataTable();
                    sqlCommandString = "Usp_ActivityMaster_dropdown";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dt);
                    }
                    connection.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }
 

 public DataTable getPartyWiseActivity()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            dt = db.sub_GetDatatable("USP_GetPartyWiseActivity");

            return dt;
        }

        public DataTable getHoldLists()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_getHoldDetailsLists ");
            return dt;
        }


        public DataTable HoldEntryReleased(int releasedBy, string releasedRemarks, int holdID)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_UpdateForHoldReleased";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@HoldID", holdID);
                    objCommand.Parameters.AddWithValue("@ReleasedRemarks", releasedRemarks);
                    objCommand.Parameters.AddWithValue("@ReleasedBy", releasedBy);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable getReleaseLists(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_getReleaseLists '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            return dt;
        }
        public DataSet AjxImportWorkDetails(string ContainerNo, string IGMNo, string ItemNo, string Category, string SearchOn)
        {
            DataSet dt = new DataSet();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDataSets("GET_CONTAINER_NOS_IMPORT1 '" + ContainerNo + "','" + IGMNo + "','" + ItemNo + "','" + Category + "','" + SearchOn + "'");

            return dt;
        }
        public DataTable GetEditDetails(string WONo)
        {
            DataTable DlList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DlList = db.sub_GetDatatable("usp_edit_Work_order_M '" + WONo + "'");
            return DlList;

        }
        public DataTable GetEditDetailsgrid(string WONo)
        {
            DataTable DlList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            DlList = db.sub_GetDatatable("usp_grid_bind_work_order '" + WONo + "'");
            return DlList;

        }

        public DataTable SaveMaterialTransferDL(int Userid, int Location, DataTable dataTable)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_INSERT_WO_MATERIAL_Transfer";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ADDEDBY", Userid);
                    objCommand.Parameters.AddWithValue("@Location", Location);

                    if (dataTable != null)
                    {
                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@UTYPE_INSERT_WO_MATERAL_Transfer";
                        param1.Value = dataTable;
                        param1.TypeName = "UTYPE_INSERT_WO_MATERAL_Transfer";
                        param1.SqlDbType = SqlDbType.Structured;
                        objCommand.Parameters.Add(param1);
                    }
                    DataTable dtClass = new DataTable();


                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;

                        _Data.Fill(dtClass);
                    }
                    connection.Close();

                    return dtClass;

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }
        public DataTable SaveReleaseRequest(ReleaseRequest data, int userid)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_SaveReleaseRequest";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@RequestDate", data.RequestDate);
                    objCommand.Parameters.AddWithValue("@CustID", data.CustomerID);
                    objCommand.Parameters.AddWithValue("@DaysFor", data.DaysFor);
                    objCommand.Parameters.AddWithValue("@RequestBy", userid);
                    objCommand.Parameters.AddWithValue("@Remarks", data.Remarks);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetReleaseRequestSummary(string asOn)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetReleaseRquestSummary";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@AsOn", asOn);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable ApproveReleaseRequest(int limitDays, long cRID, int userid, DateTime AddedOn)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_UpdateReleaseRequest";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@Condition", "Approve");
                    objCommand.Parameters.AddWithValue("@CRLimitID", cRID);
                    objCommand.Parameters.AddWithValue("@DaysFor", limitDays);
                    objCommand.Parameters.AddWithValue("@AddedBy", userid);
                    objCommand.Parameters.AddWithValue("@AddedOn", AddedOn);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }


        public DataTable DeclineReleaseRequest(int limitDays, long cRID, int userid, DateTime addedOn)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_UpdateReleaseRequest";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@Condition", "Decline");
                    objCommand.Parameters.AddWithValue("@CRLimitID", cRID);
                    objCommand.Parameters.AddWithValue("@DaysFor", limitDays);
                    objCommand.Parameters.AddWithValue("@AddedBy", userid);
                    objCommand.Parameters.AddWithValue("@AddedOn", addedOn);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetreleaseRequestSatus(string asOn)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetreleaseRequestSatus";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@AsOn", asOn);
                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetImportendExportMovementReport()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_ICD_PROGRESS_DMR_Movement_ICD");


            return ExpenseDt;
        }
        public DataTable GetMovementReportCFS()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_ICD_PROGRESS_DMR_Movement_CFS";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@LoginName", loginID.Trim());

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable GetLastImportendExportMovementReport()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_ICD_PROGRESS_DMR_Movement_ICD_Last_Month");


            return ExpenseDt;
        }


        public DataTable GetLastMovementReportCFS()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_ICD_PROGRESS_DMR_Movement_CFS_Last_Month";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@LoginName", loginID.Trim());

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }


        }

        public DataTable GetlastDayImportendExportMovementReport()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_ICD_PROGRESS_DMR_Movement_ICD_Last_24Hours");


            return ExpenseDt;
        }
        public DataTable GetLastDayMovementReportCFS()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_ICD_PROGRESS_DMR_Movement_CFS_Last_24Hours";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@LoginName", loginID.Trim());

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }


        }


        public DataTable GetPortPendencyReport()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_PORT_WISE_PENDENCY_ICD");


            return ExpenseDt;
        }


        public DataTable GetPortPendencyReportCFS()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_PORT_WISE_PENDENCY_CFS";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@LoginName", loginID.Trim());

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }


        public DataTable GetInventoryDetails()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_imp_INVENTORY_ICD");


            return ExpenseDt;
        }


        public DataTable GetInventoryDetailsCFS()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_imp_INVENTORY_CFS";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@LoginName", loginID.Trim());

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataSet GetCFSImportExportRailRoadData()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_IMPORT_EXPORT_RAIL_ROAD_WISE_REPORT";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@LoginName", loginID.Trim());

                    DataSet dtLoginDetails = new DataSet();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }

        public DataTable GetDMRDestuffICDDetails()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_ICD_DMR_DeStuff_Report");


            return ExpenseDt;
        }


        public DataTable GetDMRCFSdestuffdetails()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_CFS_DMR_DeStuff_Report";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@LoginName", loginID.Trim());

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }


        public DataTable GetTodayDMRDestuffICDDetails()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_ICD_Today_DMR_DeStuff_Report");


            return ExpenseDt;
        }


        public DataTable GetTodayDMRCFSdestuffdetails()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_CFS_Today_DMR_DeStuff_Report";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@LoginName", loginID.Trim());

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable GetImportandExportCompareMovementReport()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_ICD_PROGRESS_DMR_Movement_ICD_Last_To_Current_Compare");


            return ExpenseDt;
        }
        public DataTable GetCompareMovementReportCFS()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_ICD_PROGRESS_DMR_Movement_CFS_Last_To_Current_Compare";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@LoginName", loginID.Trim());
                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }

        public DataTable GetLastImportendExportCompareMovementReport()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            ExpenseDt = db.sub_GetDatatable("USP_ICD_PROGRESS_DMR_Movement_ICD_Last_To_Last_Month");


            return ExpenseDt;
        }

        public DataTable GetLastCompareMovementReportCFS()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_ICD_PROGRESS_DMR_Movement_CFS_Last_TO_Last_Month";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable GetlastDayImportendExportCompareMovementReport()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ExpenseDt = db.sub_GetDatatable("USP_ICD_PROGRESS_DMR_Movement_ICD_Last_24Hours_Last_Month");


            return ExpenseDt;
        }

        public DataTable GetLastDayCompareMovementReportCFS()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_ICD_PROGRESS_DMR_Movement_CFS_Last_24Hours_Last_Month";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable GetTodayImportendExportMovementReport()
        {
            DataTable ExpenseDt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            ExpenseDt = db.sub_GetDatatable("USP_ICD_PROGRESS_DMR_Movement_ICD_Last_12Hours");


            return ExpenseDt;
        }
        public DataTable GetTodayMovementReportCFS()
        {
           DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_ICD_PROGRESS_DMR_Movement_CFS_Last_12Hours";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
  

        public DataTable CheckVianoforSealcut(string ViaNo)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                //connection.Open();
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandstring = "USP_ValidateSealCutVia";
                    SqlCommand objCommand = new SqlCommand(sqlCommandstring, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ViaNo", ViaNo);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        //-------------------Inventory Management

        public DataTable GetAutoContainerNoList(string prefixText, string type)
        {
            DB.DBConnection objConn = new DB.DBConnection();

            string sqlCommandString;
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "GetContainerList";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@SearchText", prefixText);
                    objCommand.Parameters.AddWithValue("@Type", type);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetDropdownListDomestic(string table, string column, string condition, string orderBy)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetMasterTableList";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@TABLENAME", table);
                    objCommand.Parameters.AddWithValue("@COLUMNNAME", column);
                    objCommand.Parameters.AddWithValue("@CONDITION", condition);
                    objCommand.Parameters.AddWithValue("@ORDERBY", orderBy);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable SaveIssuedContainer(ContainerStockM containerIssueList)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_SaveIssuedContainerM";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@IssueDate", containerIssueList.IssueDate);
                    objCommand.Parameters.AddWithValue("@ContainerNo", containerIssueList.ContainerNo);
                    objCommand.Parameters.AddWithValue("@DeptID", containerIssueList.DeptID);
                    objCommand.Parameters.AddWithValue("@Remarks", containerIssueList.Remarks);
                    objCommand.Parameters.AddWithValue("@AddedBy", containerIssueList.AddedBy);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable SaveReturnedContainer(ContainerStockM containerReturnList)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_SaveReturndContainerM";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ReturnDate", containerReturnList.ReturnDate);
                    objCommand.Parameters.AddWithValue("@ContainerNo", containerReturnList.ContainerNo);
                    objCommand.Parameters.AddWithValue("@DeptID", containerReturnList.DeptID);
                    objCommand.Parameters.AddWithValue("@Remarks", containerReturnList.Remarks);
                    objCommand.Parameters.AddWithValue("@AddedBy", containerReturnList.AddedBy);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable SaveNewContainerIN(ContainerStockM containerINList)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_SaveNewContainerM";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@EntryID", containerINList.EntryID);
                    objCommand.Parameters.AddWithValue("@INDate", containerINList.INDate);
                    objCommand.Parameters.AddWithValue("@PurchaseDate", containerINList.PurchaseDate);
                    objCommand.Parameters.AddWithValue("@ContainerNo", containerINList.ContainerNo);
                    objCommand.Parameters.AddWithValue("@Size", containerINList.Size);
                    objCommand.Parameters.AddWithValue("@Type", containerINList.Type);
                    objCommand.Parameters.AddWithValue("@VendorName", containerINList.VendorName);
                    objCommand.Parameters.AddWithValue("@Remarks", containerINList.Remarks);
                    objCommand.Parameters.AddWithValue("@AddedBy", containerINList.AddedBy);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable SaveSoldContainer(ContainerStockM containerSoldList)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_SaveSoldContainerM";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@SoldDate", containerSoldList.SoldDate);
                    objCommand.Parameters.AddWithValue("@ContainerNo", containerSoldList.ContainerNo);
                    objCommand.Parameters.AddWithValue("@DeptID", containerSoldList.DeptID);
                    objCommand.Parameters.AddWithValue("@Remarks", containerSoldList.Remarks);
                    objCommand.Parameters.AddWithValue("@AddedBy", containerSoldList.AddedBy);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetStockDetails()
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetContainerStockCount";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //objCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    //objCommand.Parameters.AddWithValue("@ToDate",toDate);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetSummarydetails(DateTime fromDate, DateTime toDate, string type)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetContainerDetailSummary";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    objCommand.Parameters.AddWithValue("@ToDate", toDate);
                    objCommand.Parameters.AddWithValue("@Type", type);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetDetailsAgainstContainerNoForReturn(string containerNo)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_FetchContainerDetailsForReturn";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ContainerNo", containerNo);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        public DataTable GetContainerStockList(int deptID, string title)
        {
            string sqlCommandString;
            DB.DBConnection objConn = new DB.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetContainerStockList";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@DeptID", deptID);
                    objCommand.Parameters.AddWithValue("@TileTitle", title);

                    DataTable dtClass = new DataTable();
                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtClass);
                    }
                    connection.Close();
                    return dtClass;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }
        }

        //-----------------------------end of domestic inv manag

        //------------------Get Customer List
        public DataTable GetCustomerdetails()
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            dt = db.sub_GetDatatable("USP_Customerdetails");

            return dt;
        }
    }
}

