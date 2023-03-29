using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;


namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ModifyBL
{
    public class ModifyBL
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();

        public List<EN.WaraiDetails> getCustomerList()
        {
            List<EN.WaraiDetails> CustomerDDL = new List<EN.WaraiDetails>();
            DataTable CustmorObj = new DataTable();
            string Table = "Party_gst_m";
            string Column = "gstid,gstname";
            string Condition = "";
            string OrderBy = "gstname";

            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.WaraiDetails CustomerGroupList = new EN.WaraiDetails();
                    CustomerGroupList.PartyID = Convert.ToInt32(row["gstid"]);
                    CustomerGroupList.PartyName = Convert.ToString(row["gstname"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }
        public List<EN.WaraiDetails> GetTaxList()
        {
            List<EN.WaraiDetails> CustomerDDL = new List<EN.WaraiDetails>();
            DataTable CustmorObj = new DataTable();
            string Table = "settings_taxes";
            string Column = "settingsID,taxname";
            string Condition = "";
            string OrderBy = "taxname";
            
            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.WaraiDetails CustomerGroupList = new EN.WaraiDetails();
                    CustomerGroupList.TaxID = Convert.ToInt32(row["settingsID"]);
                    CustomerGroupList.Stax = Convert.ToString(row["taxname"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }

        public List<EN.VendorList> VendorList()
        {
            List<EN.VendorList> CustomerDDL = new List<EN.VendorList>();
            DataTable CustmorObj = new DataTable();
            string Table = "vendor_m";
            string Column = "VendorId,Name";
            string Condition = "";
            string OrderBy = "Name";

            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.VendorList CustomerGroupList = new EN.VendorList();
                    CustomerGroupList.VendorID = Convert.ToInt32(row["VendorId"]);
                    CustomerGroupList.VendorName = Convert.ToString(row["Name"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }

        public List<EN.GetAllVehicleData> GetAllVehicle(string Location, string Activity, string ContainerNo)
        {
            DataTable DriverHoldDL = new DataTable();
            DriverHoldDL = TrackerManager.GetAllVehicle(Location, Activity, ContainerNo);
            List<EN.GetAllVehicleData> Driver = new List<EN.GetAllVehicleData>();

            if (DriverHoldDL.Rows.Count != 0)
            {
                foreach (DataRow row in DriverHoldDL.Rows)
                {
                    EN.GetAllVehicleData DriverList = new EN.GetAllVehicleData();

                    DriverList.VehicleNo = Convert.ToString(row["VehicleNo"]);
                    DriverList.JONo = Convert.ToString(row["JONo"]);
                    DriverList.Date = Convert.ToString(row["Date"]);

                    Driver.Add(DriverList);
                }

            }
            return Driver;
        }

        public string UpdateModifyVehicle(string Location, string Activity, string ContainerNo, string EntryID, string VehicleNo, string OldVehicleNo, string Remarks, int Userid)
        {
            string message = "";

            DataTable holdingid = new DataTable();
            holdingid = TrackerManager.UpdateModifyVehicle(Location, Activity, ContainerNo, EntryID, VehicleNo, OldVehicleNo, Remarks, Userid);
            message = "Record Updated  successfully!";
            return message;
        }

        // SAGAR CODE
        public EN.Response SaveExportTariffSettingDetails(int Userid, DataTable dataTable)
        {
            EN.Response message = new EN.Response();
            try
            {
                DataTable data = new DataTable();
                data = TrackerManager.SaveExportTariffSettingDetails(Userid, dataTable);
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        message.ResponseMessage = Convert.ToString(row["message"]);
                        message.Status = Convert.ToInt32(row["Status"]);
                    }
                }
                return message;
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.ResponseMessage = ex.Message;
                return message;
            }
        }

        public string CancelSBDetails(DataTable dataTable, string Remarks, long UserID)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSQL = "";


            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_CancelSBEntry '" + dataTable.Rows[k].Field<string>("SBNO") + "','" + dataTable.Rows[k].Field<string>("EntryID") + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }


        public string CancelSRDetails(DataTable dataTable, string Remarks, long UserID)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSQL = "";


            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_Cancel_Stuffing_Request_Entry '" + dataTable.Rows[k].Field<string>("SBNO") + "','" + dataTable.Rows[k].Field<string>("EntryID") + "','" + dataTable.Rows[k].Field<string>("containerNo") + "','" + dataTable.Rows[k].Field<string>("SBEntryID") + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }

        public string CancelStuffingDetails(DataTable dataTable, string Remarks, long UserID)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSQL = "";


            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_Cancel_Stuffing_Entry '" + dataTable.Rows[k].Field<string>("SBNO") + "','" + dataTable.Rows[k].Field<string>("EntryID") + "','" + dataTable.Rows[k].Field<string>("containerNo") + "','" + dataTable.Rows[k].Field<string>("SBEntryID") + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }

        public string UpdateExpSealNo(DataTable dataTable, string Remarks, long UserID)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSQL = "";


            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_Update_SealNoWeb '" + dataTable.Rows[k].Field<string>("EntryID") + "','" + dataTable.Rows[k].Field<string>("containerNo") + "','" + dataTable.Rows[k].Field<string>("AgentSeal") + "','" + dataTable.Rows[k].Field<string>("CustomSeal") + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }


        public string UpdateDelType(DataTable dataTable, string Remarks, string GPNo, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_Convert_Import_GP '" + dataTable.Rows[k].Field<string>("JONo") + "','" + dataTable.Rows[k].Field<string>("containerNo") + "','" + Remarks + "','" + GPNo + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }


        public List<EN.DomesticTrainMaster> TrainM()
        {
            List<EN.DomesticTrainMaster> CustomerDDL = new List<EN.DomesticTrainMaster>();
            DataTable CustmorObj = new DataTable();
            string Table = "Domestic_TrainMaster";
            string Column = "TrainID,TrainNo";
            string Condition = "";
            string OrderBy = "TrainNo";

            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.DomesticTrainMaster CustomerGroupList = new EN.DomesticTrainMaster();
                    CustomerGroupList.TrainID = Convert.ToInt32(row["TrainID"]);
                    CustomerGroupList.TrainNo = Convert.ToString(row["TrainNo"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }

        public string UpdateDomesticData(DataTable dataTable, int TrainID, int AGID, string Remarks, int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_UPDATE_DOMESTIC_CUSTOMER_TRAIN '" + dataTable.Rows[k].Field<string>("LoadingID") + "','" + dataTable.Rows[k].Field<string>("EntryID") + "','" + TrainID + "','" + AGID + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }

        public EN.Response UpdateDocForInvExport(EN.ExportModifyMaster data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();
                // var Date = Convert.ToDateTime(data.ETA).ToString("yyyy-MM-dd");
                list = TrackerManager.UpdateDocForInv(data.GPNo, data.EntryID, data.containerNo, data.Remarks, data.AddedBy);

                if (list != null)
                {
                    foreach (DataRow row in list.Rows)
                    {
                        response.Status = Convert.ToInt32(row["Status"]);
                        response.ResponseMessage = Convert.ToString(row["message"]);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return response;

        }


        public EN.Response UpdateCargoTypeExport(EN.ExportModifyMaster data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();
                // var Date = Convert.ToDateTime(data.ETA).ToString("yyyy-MM-dd");
                list = TrackerManager.UpdateCargoTypeInv(data.containerNo, data.EntryID, data.Cargotypeid, data.Cargo_Type, data.Remarks, data.AddedBy);

                if (list != null)
                {
                    foreach (DataRow row in list.Rows)
                    {
                        //  response.Status = Convert.ToInt32(row["Status"]);
                        response.ResponseMessage = Convert.ToString(row["message"]);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return response;

        }

        public List<EN.ScanningModule> DriverDDL()
        {
            List<EN.ScanningModule> CustomerDDL = new List<EN.ScanningModule>();
            DataTable CustmorObj = new DataTable();
            string Table = "drivers";
            string Column = "driverid,drivername";
            string Condition = "";
            string OrderBy = "drivername";

            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.ScanningModule CustomerGroupList = new EN.ScanningModule();
                    CustomerGroupList.DriverID = Convert.ToInt32(row["driverid"]);
                    CustomerGroupList.DriverName = Convert.ToString(row["drivername"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }

        public string SaveIMPOutGP(DataTable dataTable, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec get_sp_GPScanningSaveWeb '" + dataTable.Rows[k].Field<string>("JONo") + "','" + dataTable.Rows[k].Field<string>("containerNo") + "','" + dataTable.Rows[k].Field<string>("Size") + "','" + dataTable.Rows[k].Field<string>("trailerID") + "','" + dataTable.Rows[k].Field<string>("driverID") + "','" + dataTable.Rows[k].Field<string>("Remarks") + "','" + dataTable.Rows[k].Field<string>("trailername") + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }


        public string UpdateScanTypeDate(DataTable dataTable, string ScanType, string Remarks, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            string strSQL = "";

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_SCANNING_VALIDATION '" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + dataTable.Rows[k].Field<string>("JONo") + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = "Invoice generated cannot proceed";
                }

                else
                {
                    for (int j = 0; j < dataTable.Rows.Count; j++)
                    {
                        strSQL = "Exec USP_UPDATE_Scan_Type_Import '" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + dataTable.Rows[k].Field<string>("JONo") + "','" + ScanType + "','" + Remarks + "','" + UserID + "'";
                        dt2 = db.sub_GetDatatable(strSQL);
                        if (dt2.Rows.Count > 0)
                        {
                            Message = Convert.ToString(dt2.Rows[0]["message"]);
                        }
                    }

                }

            }



            return Message;
        }

        public string SaveIMPINGP(DataTable dataTable, bool IsScan, bool CSD, string Remarks, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_IN_GPScanningSaveWeb '" + dataTable.Rows[k].Field<string>("JONo") + "','" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + dataTable.Rows[k].Field<string>("EntryID") + "','" + IsScan + "','" + CSD + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }

        public EN.Response SaveFreeInvoiceDataBL(EN.ModificationEntities data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();
                var FromDate = Convert.ToDateTime(data.FromDate).ToString("yyyy-MM-dd");
                var ToDate = Convert.ToDateTime(data.ToDate).ToString("yyyy-MM-dd");
                list = TrackerManager.SaveFreeInvoiceDataDL(data.AGID, data.FromDate, data.ToDate, data.Remarks, data.IsActive);

                if (list != null)
                {
                    foreach (DataRow row in list.Rows)
                    {
                        response.Status = Convert.ToInt32(row["Status"]);
                        response.ResponseMessage = Convert.ToString(row["message"]);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return response;

        }


        public string UpdateCLPDate(DataTable dataTable, string Remarks, string Date, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            DataTable dt1 = new DataTable();

            string strSQL = "";


            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_UPDATE_CLP_DATE '" + dataTable.Rows[k].Field<string>("SBNO") + "','" + dataTable.Rows[k].Field<string>("EntryID") + "','" + dataTable.Rows[k].Field<string>("containerNo") + "','" + dataTable.Rows[k].Field<string>("SBEntryID") + "','" + Remarks + "','" + Convert.ToDateTime(Date).ToString("yyyy-MM-dd HH:mm:ss") + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }

        public EN.Response UpdateSBDirectCartingAllowDetails(EN.ExportModifyMaster data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();
                // var Date = Convert.ToDateTime(data.ETA).ToString("yyyy-MM-dd");
                list = TrackerManager.UpdateSBDirectCartingAllowDetails(data.SBNo, data.EntryID, data.Remarks, data.AddedBy);

                if (list != null)
                {
                    foreach (DataRow row in list.Rows)
                    {
                        //  response.Status = Convert.ToInt32(row["Status"]);
                        response.ResponseMessage = Convert.ToString(row["message"]);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return response;

        }

        public string DeleteDomesticOut(DataTable dataTable, string Remarks, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_Cancel_Domestic_Truck_Out '" + dataTable.Rows[k].Field<string>("Srno") + "','" + dataTable.Rows[k].Field<string>("SlipNo") + "','" + dataTable.Rows[k].Field<string>("BatchNo") + "','" + dataTable.Rows[k].Field<string>("LRNo") + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }

        public string MakeWaivedOffInvoiceBL(DataTable dataTable, string Remarks, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";



            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_Make_Waived_Off_Invoice '" + dataTable.Rows[k].Field<string>("EntryID") + "','" + dataTable.Rows[k].Field<string>("GPNo") + "','" + dataTable.Rows[k].Field<string>("TruckNO") + "','" + dataTable.Rows[k].Field<string>("SBNo") + "','" + dataTable.Rows[k].Field<string>("SBEntryID") + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }


        public EN.Response SaveFuelVendorDataBL(EN.ModificationEntities data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();

                list = TrackerManager.SaveVendorDetails(data.Vendor, data.Party,data.IsActive, data.AddedBy,data.EntryID);

                if (list != null)
                {
                    foreach (DataRow row in list.Rows)
                    {
                        response.Status = Convert.ToInt32(row["Status"]);
                        response.ResponseMessage = Convert.ToString(row["message"]);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return response;

        }



        public EN.Response SaveHSNCodeBL(EN.ModificationEntities data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();

                list = TrackerManager.SaveHSNCodeDL(data.HSNCode, data.Description, data.AddedBy);

                if (list != null)
                {
                    foreach (DataRow row in list.Rows)
                    {
                        response.Status = Convert.ToInt32(row["Status"]);
                        response.ResponseMessage = Convert.ToString(row["message"]);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return response;

        }

        public string UpdateOtherInvoicedetailsBL(DataTable dataTable, string Remarks, string CargoDesc, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_UPDATE_OTHER_INVOICE '" + dataTable.Rows[k].Field<string>("AssessNo") + "','" + dataTable.Rows[k].Field<string>("WorkYear") + "','" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + dataTable.Rows[k].Field<string>("VehicleNo") + "','" + dataTable.Rows[k].Field<string>("FromID") + "','" + dataTable.Rows[k].Field<string>("ToID") + "','" + dataTable.Rows[k].Field<string>("narration") + "','" + dataTable.Rows[k].Field<string>("C2") + "','" + Remarks + "','" + CargoDesc + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }

        public List<EN.CartingLOcation> CartingLocation()
        {
            List<EN.CartingLOcation> CustomerDDL = new List<EN.CartingLOcation>();
            DataTable CustmorObj = new DataTable();
            string Table = "carting_location_m";
            string Column = "locationid,carting_location";
            string Condition = "";
            string OrderBy = "carting_location";

            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.CartingLOcation CustomerGroupList = new EN.CartingLOcation();
                    CustomerGroupList.LocationID = Convert.ToInt32(row["locationid"]);
                    CustomerGroupList.Location = Convert.ToString(row["carting_location"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }


        public string UpdateCartingLocationDetBL(DataTable dataTable, string Remarks, long UserID)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSQL = "";


            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                strSQL = "Exec USP_UPDATE_CARTING_LOCATION '" + dataTable.Rows[k].Field<string>("CartingAllowID") + "','" + dataTable.Rows[k].Field<string>("VehicleNo") + "','" + dataTable.Rows[k].Field<string>("SBNo") + "','" + dataTable.Rows[k].Field<string>("Location") + "','" + dataTable.Rows[k].Field<string>("SBEntryID") + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }


        public string UpdateLorryReceiptdetailsBL(DataTable dataTable, string Remarks, string AGID, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";

            for (int k = 0; k < dataTable.Rows.Count; k++)
            {




                strSQL = "Exec USP_UPDATE_Lorry_Receipt_Details '" + dataTable.Rows[k].Field<string>("LRNo") + "','" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + dataTable.Rows[k].Field<string>("VehicleNo") + "','" + dataTable.Rows[k].Field<string>("BookingNo") + "','" + dataTable.Rows[k].Field<string>("BOENo") + "','" + dataTable.Rows[k].Field<string>("FromLocationID") + "','" + dataTable.Rows[k].Field<string>("ToLocationID") + "','" + dataTable.Rows[k].Field<string>("consignee") + "','" + Remarks + "','" + AGID + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }


        public EN.Response UpdateExcessAmtBL(EN.UpdateExcessAmt data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();

                list = TrackerManager.UpdateExcessAmtDL(data.ReceiptNo, data.Amount, data.AddedBy);

                if (list != null)
                {
                    foreach (DataRow row in list.Rows)
                    {
                        response.Status = Convert.ToInt32(row["Status"]);
                        response.ResponseMessage = Convert.ToString(row["message"]);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.ResponseMessage = ex.Message;
            }
            return response;

        }


        public string CancelExpTruckInBL(DataTable dataTable, string Remarks, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";


            for (int k = 0; k < dataTable.Rows.Count; k++)
            {




                strSQL = "Exec USP_CANCEL_EXP_TRUCK_IN '" + dataTable.Rows[k].Field<string>("SrNo") + "','" + dataTable.Rows[k].Field<string>("CartingAllowID") + "','" + dataTable.Rows[k].Field<string>("VehicleNo") + "','" + dataTable.Rows[k].Field<string>("SBNo") + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }

        public string CancelCartingTallyBL(DataTable dataTable, string Remarks, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";


            for (int k = 0; k < dataTable.Rows.Count; k++)
            {




                strSQL = "Exec USP_CANCEL_EXP_Cating_Tally '" + dataTable.Rows[k].Field<string>("SrNo") + "','" + dataTable.Rows[k].Field<string>("CartingID") + "','" + dataTable.Rows[k].Field<string>("VehicleNo") + "','" + dataTable.Rows[k].Field<string>("SBNo") + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }

        public string UpdateModifyExportCartingDateBL(DataTable dataTable, string Date, string Remarks, long UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt1 = new DataTable();
            string strSQL = "";


            for (int k = 0; k < dataTable.Rows.Count; k++)
            {


                strSQL = "Exec USP_UPDATE_EXP_Cating_Tally '" + dataTable.Rows[k].Field<string>("SrNo") + "','" + dataTable.Rows[k].Field<string>("CartingID") + "','" + dataTable.Rows[k].Field<string>("VehicleNo") + "','" + dataTable.Rows[k].Field<string>("SBNo") + "','" + Convert.ToDateTime(Date).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Remarks + "','" + UserID + "'";
                dt1 = db.sub_GetDatatable(strSQL);
                if (dt1.Rows.Count > 0)
                {
                    Message = Convert.ToString(dt1.Rows[0]["message"]);
                }
            }
            return Message;
        }


        public string CancelCartingallowID(DataTable dataTable, long UserID)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();



            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                string strSQL = "";
                strSQL = "Exec USP_CancelAwllowID '" + dataTable.Rows[k].Field<string>("CartingAllowID") + "','" + UserID + "'";

                dt = db.sub_GetDatatable(strSQL);
                string Messageget = "Record Deleted Successfully";
                Message = Messageget;
            }
            return Message;
        }

        public EN.Response SaveWOMaterialBL(int Userid, int DepartID, string Remarks, DataTable dataTable)
        {
            EN.Response message = new EN.Response();
            try
            {
                DataTable data = new DataTable();
                data = TrackerManager.SaveMaterialDL(Userid, DepartID, Remarks, dataTable);
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        message.ResponseMessage = Convert.ToString(row["message"]);
                        message.Status = Convert.ToInt32(row["Status"]);
                    }
                }
                return message;
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.ResponseMessage = ex.Message;
                return message;
            }
        }




        public EN.Response SaveWOMaterialTransferBL(int Userid, int Location, DataTable dataTable)
        {
            EN.Response message = new EN.Response();
            try
            {
                DataTable data = new DataTable();
                data = TrackerManager.SaveMaterialTransferDL(Userid, Location, dataTable);
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        message.ResponseMessage = Convert.ToString(row["message"]);
                        message.Status = Convert.ToInt32(row["Status"]);
                    }
                }
                return message;
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.ResponseMessage = ex.Message;
                return message;
            }
        }

    }
}
