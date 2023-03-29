using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using System.Data;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.DriverPaymentReco
{
    public class DriverPaymentReco
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.DriverPaymentReco> UpdateDriverPaymentDetails(DataTable DriverPaymentDT, Int32 userId)
        {
            //EN.DriverPaymentReco objdriverpaymententities = new EN.DriverPaymentReco();
            List<EN.DriverPaymentReco> DriverPaymentList = new List<EN.DriverPaymentReco>();
            if (DriverPaymentDT != null)
            {
                int count = 1;
                foreach (DataRow row in DriverPaymentDT.Rows)
                {                    
                    EN.DriverPaymentReco DPDTList = new EN.DriverPaymentReco();
                    DPDTList.SRNo = count++;
                    DPDTList.IFSCCode = Convert.ToString(row["IFSCCode"]);
                    DPDTList.Amount = Convert.ToString(row["Amount"]);
                    DPDTList.TransferFromAccnt = Convert.ToString(row["TransferFromAccnt"]);
                    DPDTList.Transporter = Convert.ToString(row["Transporter"]);
                    DPDTList.TransferInAccnt = Convert.ToString(row["TransferInAccnt"]);
                    DPDTList.Driver = Convert.ToString(row["Driver"]);
                    DPDTList.TransferType = Convert.ToString(row["TransferType"]);
                    DPDTList.VoucherNo = Convert.ToString(row["VoucherNo"]);
                    DPDTList.VoucherFor = Convert.ToString(row["VoucherFor"]);
                    DPDTList.FileName = Convert.ToString(row["FileName"]);
                    DPDTList.TransDate = Convert.ToString(row["TransDate"]);
                    DPDTList.PaymentStatus = Convert.ToString(row["PaymentStatus"]);
                    DPDTList.ReferenceNo = Convert.ToString(row["ReferenceNo"]);
                    DriverPaymentList.Add(DPDTList);
                }   
            }
            return DriverPaymentList;
        }
        public string Validation(DataTable PaymentDT)
        {
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";

            if (PaymentDT != null)
            {
                foreach (DataRow row in PaymentDT.Rows)
                {
                    DataSet PortsDS = new DataSet();
                    PortsDS = trackerdatamanager.getDuplicateData(Convert.ToString(row["Transporter"]), Convert.ToString(row["Driver"]), Convert.ToString(row["VoucherNo"]), Convert.ToString(row["Amount"]), Convert.ToString(row["VoucherFor"]));
                    if (PortsDS.Tables[0].Rows.Count == 0)
                    {
                        if (message == "")
                        {
                            message = "Following Transporters not found in database: \n" + Convert.ToString(row["Transporter"]);
                        }
                        else
                        if (Convert.ToString(row["Transporter"]).Contains(message))
                        {
                        }
                        else
                        {
                            message += "," + Convert.ToString(row["Transporter"]);
                        }
                    }
                    if (PortsDS.Tables[1].Rows.Count == 0)
                    {
                        if (message1 == "")
                        {
                            message1 = "Following Drivers not found in database: \n" + Convert.ToString(row["Driver"]);
                        }
                        else
                        {
                            if (Convert.ToString(row["Driver"]).Contains(message1))
                            {
                            }
                            else
                            {
                                message1 += "," + Convert.ToString(row["Driver"]);
                            }
                        }
                    }
                    if (PortsDS.Tables[2].Rows.Count == 0)
                    {
                        if (message3 == "")
                        {
                            message3 = "Following Voucher's amount is not matching : \n" + Convert.ToString(row["VoucherNo"]);
                        }
                        else
                        {
                            if (Convert.ToString(row["VoucherNo"]).Contains(message3))
                            {
                            }
                            else
                            {
                                message3 += "," + Convert.ToString(row["VoucherNo"]);
                            }
                        }
                    }
                }
                if ((message != "") || (message1 != "") || (message3 != ""))
                {
                    message2 = message + "\n" + message1 + "\n" + message3;
                }
            }
            return message2;
        }
        public EN.DriverPaymentReco SavePaymentList(DataTable Itemdata, int UserID,DateTime entrydate)
        {
            //string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("UserID", UserID);
            parameterList.Add("entryDate", entrydate);            
            DataSet ds = new DataSet();

            ds = db.AddTypeRoadPlanningTableData("USP_INSERT_INTO_DRIVER_PAYMENT_RECO", parameterList, Itemdata, "PT_DriverPaymentDetails", "@PT_DriverPaymentDetails");


            //int i = db.SaveContainerList("USP_TainPlanningInsertContainerList", parameterList, Itemdata);

            // VendorDataDL = objgetMyREQDBManager.ApprovedStatusList(OrderID, companyid);

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            EN.DriverPaymentReco PaymentList = new EN.DriverPaymentReco();

            dt = ds.Tables[0];
            dt1 = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                PaymentList.validationMessage = Convert.ToInt32(dt.Rows[0][0]);
                PaymentList.containerList = Convert.ToString(dt1.Rows[0][0]);
            }
            // Message = "Record saved successfully";
            return PaymentList;


            // Message = "Record saved successfully";
            ///return Message;



        }
        public List<EN.DriverPaymentRecoSummaryEntities> GetDriverRecoSummary(string FromDate,string ToDate,string Voucher,string Driver)
        {
            DataTable dt = new DataTable();
            List<EN.DriverPaymentRecoSummaryEntities> DriverPaymentList = new List<EN.DriverPaymentRecoSummaryEntities>();
            dt = trackerdatamanager.getDriverSummary(FromDate, ToDate, Voucher, Driver);
            if (dt.Rows.Count > 0)
            {
                int count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    EN.DriverPaymentRecoSummaryEntities DPDTList = new EN.DriverPaymentRecoSummaryEntities();
                    DPDTList.SRNo = Convert.ToInt32(row["Sr No"]);
                    DPDTList.PaidID = Convert.ToInt32(row["Paid ID"]);
                    DPDTList.FileName = Convert.ToString(row["File Name"]);
                    DPDTList.PaidDate = Convert.ToString(row["Paid Date"]);
                    DPDTList.VoucherNo = Convert.ToString(row["Voucher No"]);
                    DPDTList.TransDate = Convert.ToString(row["Trans Date"]);
                    DPDTList.Amount = Convert.ToString(row["Amount"]);
                    DPDTList.VoucherFor = Convert.ToString(row["Voucher For"]);
                    DPDTList.Transporter = Convert.ToString(row["Transporter"]);
                    DPDTList.Driver = Convert.ToString(row["Driver"]);
                    //DPDTList.VoucherFor = Convert.ToString(row["VoucherFor"]);
                    DPDTList.DriverAccountNo = Convert.ToString(row["Driver Account No"]);
                    DPDTList.TransferType = Convert.ToString(row["Transfer Type"]);                    
                    DPDTList.ReferenceNo = Convert.ToString(row["Reference No"]);
                    DriverPaymentList.Add(DPDTList);
                }
            }
            return DriverPaymentList;
        }
        public EN.DriverPaymentReco getDropDownList()
        {
            EN.DriverPaymentReco objdriverentities = new EN.DriverPaymentReco();
            DataTable DropDownList = new DataTable();
            DropDownList = trackerdatamanager.getDriverSummaryDropDown();

            List<EN.DriverDropDown> DriverList = new List<EN.DriverDropDown>();

            //List<EN.MenuInfo> MenuList = new List<EN.MenuInfo>();
            if (DropDownList.Rows.Count > 0)
            {
                foreach (DataRow row in DropDownList.Rows)
                {
                    EN.DriverDropDown Driver = new EN.DriverDropDown();
                    Driver.DriverID = Convert.ToInt32(row["DRIVERID"]);
                    Driver.DriverName = Convert.ToString(row["DRIVERNAME"]);
                    DriverList.Add(Driver);
                }
            }
            objdriverentities.DriverList = DriverList;

            return objdriverentities;
        }

        public List<EN.DriverPaymentCalendarReport> GetDriverPaymentCalendar()
        {
            DataTable List = new DataTable();
            List = trackerdatamanager.GetDriverPaymentCalendar();

            List<EN.DriverPaymentCalendarReport> ListBL = new List<EN.DriverPaymentCalendarReport>();

            //List<EN.MenuInfo> MenuList = new List<EN.MenuInfo>();
            if (List.Rows.Count > 0)
            {
                foreach (DataRow row in List.Rows)
                {
                    EN.DriverPaymentCalendarReport data = new EN.DriverPaymentCalendarReport();
                    data.Title = Convert.ToString(row["title"]);
                    data.Editable = Convert.ToString(row["editable"]);
                    data.BackgroundColor = Convert.ToString(row["backgroundColor"]);
                    data.TextColor = Convert.ToString(row["textColor"]);
                    data.Start = Convert.ToString(row["start"]);
                    data.Type = Convert.ToInt32(row["Type"]);
                    ListBL.Add(data);
                }
            }

            return ListBL;
        }




        public string CancelDriverRecodetails(DataTable dataTable, int Userid)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            string message = "";

            DataTable VendorDataDL = new DataTable();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("Userid", Userid);

            /*VendorDataDL = trackerdatamanager.AddDirectFuel(containerCount, containerTypeID, TrailerNo, Passing, driverID, TransportorID, FromID, ToID, Activity, ReadingFrom, ReadingTo, fuel, Amount1, Amount2, AdjustFuel, Userid, PlanID, INOUT, ContainerNo, TarrifID, VehTransID, Remarks);*/

            VendorDataDL = db.DataTableAddTypeTable("CancelDriverRecodetails", parameterList, dataTable, "CancelDriverReco", "@CancelDriverReco");


            return message;

        }
    }
}
