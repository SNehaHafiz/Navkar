using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Driver
{
   public class DriverDataProvider
    {
       DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public EN.DriverEntities getDropDownList()
        {
            EN.DriverEntities objtransportbankentities = new EN.DriverEntities();
            DataTable DropDownList = new DataTable();
            DropDownList = TrackerManager.GetTransportBankData();

            List<EN.DriverBankEntities> DriverBankBankList = new List<EN.DriverBankEntities>();

            //List<EN.MenuInfo> MenuList = new List<EN.MenuInfo>();
            if (DropDownList.Rows.Count > 0)
            {
                foreach (DataRow row in DropDownList.Rows)
                {
                    EN.DriverBankEntities Transporter = new EN.DriverBankEntities();
                    Transporter.bankID = Convert.ToString(row["bankID"]);
                    Transporter.bankname = Convert.ToString(row["bankname"]);
                    DriverBankBankList.Add(Transporter);
                }
            }
            objtransportbankentities.BankList = DriverBankBankList;

            return objtransportbankentities;
        }


        public string AddDriversDetails(string DriverName, bool IsActive, long DriverID, int Userid, string BankName, string BankID, string AccountNo, string IFSCCode,
            string AccountName, string BranchName, string PaymentMode, int TranspoterID, string DlNo, string DlType, string DlDate,
            string DLExpityDate, string EquipmentType, string DContactNo, string Insuranceno, string InsuranceCompany, string Referenceby,
            string ReferenceContactno, string CurrentAddress, string PermanentAddress, string chkIsBlackList,
            string TxtRemakrs, string Vehicletype, string Employetype,string AdharNumber,string Pannumber,string Drivertype,string DOBDATE)
        {
            string Message = "";
            DataTable DriverDataDL = new DataTable();

            DriverDataDL = TrackerManager.SaveDriverDetails(DriverName, IsActive, DriverID, Userid, BankName, BankID, AccountNo, IFSCCode, AccountName, BranchName, PaymentMode, TranspoterID, DlNo, DlType, DlDate, DLExpityDate, EquipmentType, DContactNo, Insuranceno, InsuranceCompany, Referenceby, ReferenceContactno,
                CurrentAddress, PermanentAddress, chkIsBlackList, TxtRemakrs, Vehicletype, Employetype, AdharNumber, Pannumber, Drivertype, DOBDATE);



            if (DriverDataDL.Rows.Count > 0)
            {

                Message = Convert.ToString(DriverDataDL.Rows[0]["driverid"]);
            }
            else
            {
                Message = "0";
            }
            return Message;
        }


        //public string AddDriversDetails(string DriverName, bool IsActive, long DriverID, int Userid, string BankName, string BankID, string AccountNo, string IFSCCode, string AccountName, string BranchName, string PaymentMode, int TranspoterID)
        //{
        //    string Message = "";
        //    DataTable DriverDataDL = new DataTable();

        //    DriverDataDL = TrackerManager.SaveDriverDetails(DriverName, IsActive, DriverID, Userid, BankName, BankID, AccountNo, IFSCCode, AccountName, BranchName, PaymentMode, TranspoterID);



        //    Message = "Record saved successfully!";
        //    return Message;
        //}


        // code for check user
        public string checkUser(string DriverName)
        {
            string Message = "";
            DataTable userDataDL = new DataTable();

            userDataDL = TrackerManager.CheckUserValidation(DriverName);

            if (userDataDL.Rows.Count > 0)
            {

                Message = "1";
            }else
            {
                Message = "0";
            }
            return Message;
        }


        public List<EN.DriversummaryEntiites> GetDrivers()
        {
            DataTable pendingContainerDL = new DataTable();
            pendingContainerDL = TrackerManager.DriverDetails();
            List<EN.DriversummaryEntiites> Driver = new List<EN.DriversummaryEntiites>();

            if (pendingContainerDL.Rows.Count != 0)
            {
                foreach (DataRow row in pendingContainerDL.Rows)
                {
                    EN.DriversummaryEntiites DriverList = new EN.DriversummaryEntiites();
                    DriverList.DriverID = Convert.ToInt32(row["driverid"]);
                    DriverList.DriverName = Convert.ToString(row["drivername"]);
                    DriverList.DlNo = Convert.ToString(row["DLnumber"]);
                    DriverList.DlType = Convert.ToString(row["dltype"]);
                    DriverList.DlDate = Convert.ToString(row["DLDate"]);
                    DriverList.DLExpityDate = Convert.ToString(row["DL_ExpiryDate"]);
                    DriverList.EquipmentType = Convert.ToString(row["equipment"]);
                    DriverList.DContactNo = Convert.ToString(row["D_ContactNo"]);
                    DriverList.InsuranceCompany = Convert.ToString(row["InsuranceCompany"]);
                    DriverList.Insuranceno = Convert.ToString(row["InsuranceNo"]);
                    DriverList.Referenceby = Convert.ToString(row["Reference_By"]);
                    DriverList.ReferenceContactno = Convert.ToString(row["Reference_ContactNo"]);
                    DriverList.CurrentAddress = Convert.ToString(row["CurrentAddress"]);
                    DriverList.PermanentAddress = Convert.ToString(row["PermanentAddress"]);
                    DriverList.chkIsBlackList = Convert.ToString(row["IsBlackList"]);
                    DriverList.TxtRemakrs = Convert.ToString(row["Remarks"]);
                    DriverList.Vehicletype = Convert.ToString(row["driver_vehicletypeID"]);
                    DriverList.Employetype = Convert.ToString(row["Emp_Type"]);

                    DriverList.driver_vehicletypeID = Convert.ToString(row["driver_vehicletypeID"]);
                    DriverList.DLTypeID = Convert.ToString(row["DLTypeID"]);
                    DriverList.EquipmentTypeID = Convert.ToString(row["EquipmentType"]);
                    DriverList.Driver_vehicletypeID = Convert.ToString(row["Driver_vehicletypeID"]);
                    DriverList.InsuranceCompanyID = Convert.ToString(row["InsuranceCompany"]);
                    DriverList.Employee_TypeID = Convert.ToString(row["Employee_TypeID"]);



                    DriverList.AccountNo = Convert.ToString(row["AccountNo"]);
                    DriverList.IFSCCode = Convert.ToString(row["IFSCCode"]);
                    DriverList.BankName = Convert.ToString(row["bankname"]);
                    DriverList.BankID = Convert.ToString(row["BankID"]);

                    DriverList.BranchName = Convert.ToString(row["branch"]);
                    DriverList.AccountName = Convert.ToString(row["AccountName"]);
                    DriverList.IsActive = Convert.ToBoolean(row["IsActive"]);
                    DriverList.PaymentMode = Convert.ToString(row["PaymentMode"]);
                    DriverList.TransporterID = Convert.ToInt32(row["TransporterID"]);
                    DriverList.TransporterName = Convert.ToString(row["TransName"]);
                    DriverList.AdharNumber = Convert.ToString(row["adharnumber"]);
                    DriverList.PanNo = Convert.ToString(row["PANNo"]);
                    DriverList.Drivertype = Convert.ToString(row["drivertype"]);
                    DriverList.drivertypeid = Convert.ToString(row["drivertypeid"]);
                    DriverList.ActiveStatus = Convert.ToString(row["Acitve Status"]);
                    DriverList.BlackListedStatus = Convert.ToString(row["Blacklisted Status"]);
                    DriverList.Modifiedby = Convert.ToString(row["Modified By"]);
                    DriverList.Modifieddate = Convert.ToString(row["Modified date"]);
                    DriverList.Addedon = Convert.ToString(row["Added date"]);
                    DriverList.Addedby = Convert.ToString(row["Added By"]);
                    DriverList.DOBDATE = Convert.ToString(row["DOBDate"]);
                    Driver.Add(DriverList);
                }

            }
            return Driver;
        }
        //Codes by prashant
        public List<EN.DriverEntities> GetDriverDetails(string Driverid)
        {
            DataTable DriverHoldDL = new DataTable();
            DriverHoldDL = TrackerManager.GetDriverPaymentHoldDetails(Driverid);
            List<EN.DriverEntities> Driver = new List<EN.DriverEntities>();

            if (DriverHoldDL.Rows.Count != 0)
            {
                foreach (DataRow row in DriverHoldDL.Rows)
                {
                    EN.DriverEntities DriverList = new EN.DriverEntities();
                    DriverList.DriverID = Convert.ToInt32(row["driverid"]);
                    DriverList.DriverName = Convert.ToString(row["drivername"]);
                    DriverList.AccountNo = Convert.ToString(row["accountno"]);
                    DriverList.IFSCCode = Convert.ToString(row["ifsccode"]);
                    DriverList.BankName = Convert.ToString(row["bankname"]);
                    DriverList.BranchName = Convert.ToString(row["Branch"]);
                    DriverList.PaymentMode = Convert.ToString(row["paymentMode"]);
                    Driver.Add(DriverList);
                }

            }
            return Driver;
        }


     
        public string GetDriveridholding(string driverid, int userid, string Remakrs)
        {
            string message = "";

            DataTable holdingid = new DataTable();
            holdingid = TrackerManager.Driverholidingid(driverid, userid, Remakrs);
            message = "Record saved successfully!";
            return message;
        }
        public List<EN.DriverEntities> GetDriverHoldDetails()
        {
            DataTable DriverHoldDL = new DataTable();
            DriverHoldDL = TrackerManager.GetDriverHoldDetails();
            List<EN.DriverEntities> Driver = new List<EN.DriverEntities>();

            if (DriverHoldDL.Rows.Count != 0)
            {
                foreach (DataRow row in DriverHoldDL.Rows)
                {
                    EN.DriverEntities DriverList = new EN.DriverEntities();
                    DriverList.DriverID = Convert.ToInt32(row["Driver Code"]);
                    DriverList.HoldID = Convert.ToInt32(row["Hold ID"]);
                    DriverList.DriverName = Convert.ToString(row["Driver name"]);
                    DriverList.HoldDate = Convert.ToString(row["Hold Date"]);
                    DriverList.TxtRemakrs = Convert.ToString(row["remarks"]);

                    Driver.Add(DriverList);
                }

            }
            return Driver;
        }
        //Codes end by prashant

        // COde for driver approve

        public List<EN.DriverEntities> GetDriverApproveDetails()
        {
            DataTable DriverHoldDL = new DataTable();
            DriverHoldDL = TrackerManager.GetDriverApproveDetails();
            List<EN.DriverEntities> Driver = new List<EN.DriverEntities>();

            if (DriverHoldDL.Rows.Count != 0)
            {
                foreach (DataRow row in DriverHoldDL.Rows)
                {
                    EN.DriverEntities DriverList = new EN.DriverEntities();
                    DriverList.DriverID = Convert.ToInt32(row["driverid"]);
                    DriverList.DriverName = Convert.ToString(row["drivername"]);
                    DriverList.TransporterName = Convert.ToString(row["transname"]);
                    DriverList.CONTACTPERSON = Convert.ToString(row["D_contactno"]);
                    DriverList.TRailername = Convert.ToString(row["trailername"]);
                  
                    Driver.Add(DriverList);
                }

            }
            return Driver;
        }

        public string GetDriveridApproveTrailerDetails(int driverid, string trailername,int Userid)
        {
            string message = "";

            DataTable holdingid = new DataTable();
            holdingid = TrackerManager.DriverApproveDetails(driverid, trailername, Userid);
            message = "Record approved successfully!";
            return message;
        }



        public List<EN.DriverDropDownEntites> GetDLtypeList()
        {
            DataTable DriverHoldDL = new DataTable();
            DriverHoldDL = TrackerManager.DriverDlType();
            List<EN.DriverDropDownEntites> DriverDLtype = new List<EN.DriverDropDownEntites>();

            if (DriverHoldDL.Rows.Count != 0)
            {
                foreach (DataRow row in DriverHoldDL.Rows)
                {
                    EN.DriverDropDownEntites DriverList = new EN.DriverDropDownEntites();
                    DriverList.DLID = Convert.ToInt32(row["DLID"]);
                    DriverList.Dltype = Convert.ToString(row["Dltype"]);

                    DriverDLtype.Add(DriverList);
                }

            }
            return DriverDLtype;
        }

        public List<EN.DriverDropDownEntites> GetEquipment()
        {
            DataTable DriverHoldDL = new DataTable();
            DriverHoldDL = TrackerManager.GetEquipmentType();
            List<EN.DriverDropDownEntites>EquipmentType = new List<EN.DriverDropDownEntites>();

            if (DriverHoldDL.Rows.Count != 0)
            {
                foreach (DataRow row in DriverHoldDL.Rows)
                {
                    EN.DriverDropDownEntites DriverList = new EN.DriverDropDownEntites();
                    DriverList.ID = Convert.ToInt32(row["ID"]);
                    DriverList.Equipment = Convert.ToString(row["Equipment"]);

                    EquipmentType.Add(DriverList);
                }

            }
            return EquipmentType;
        }

        public List<EN.DriverDropDownEntites> GetInsuranceCompany()
        {
            DataTable DriverHoldDL = new DataTable();
            DriverHoldDL = TrackerManager.InsuranceCompany();
            List<EN.DriverDropDownEntites> Insurancetype = new List<EN.DriverDropDownEntites>();

            if (DriverHoldDL.Rows.Count != 0)
            {
                foreach (DataRow row in DriverHoldDL.Rows)
                {
                    EN.DriverDropDownEntites DriverList = new EN.DriverDropDownEntites();
                    DriverList.InsuranceID = Convert.ToInt32(row["InsuranceID"]);
                    DriverList.InsuranceCompany = Convert.ToString(row["InsuranceCompany"]);

                    Insurancetype.Add(DriverList);
                }

            }
            return Insurancetype;
        }

        public List<EN.DriverDropDownEntites> GetEmployeetype()
        {
            DataTable DriverHoldDL = new DataTable();
            DriverHoldDL = TrackerManager.Employeetype();
            List<EN.DriverDropDownEntites> Insurancetype = new List<EN.DriverDropDownEntites>();

            if (DriverHoldDL.Rows.Count != 0)
            {
                foreach (DataRow row in DriverHoldDL.Rows)
                {
                    EN.DriverDropDownEntites DriverList = new EN.DriverDropDownEntites();
                    DriverList.Emp_Type_ID = Convert.ToInt32(row["Emp_Type_ID"]);
                    DriverList.Emp_Type = Convert.ToString(row["Emp_Type"]);

                    Insurancetype.Add(DriverList);
                }

            }
            return Insurancetype;
        }


        public List<EN.DriversDocumentDetailsEntiites> AddJOAttachment(int Userid, byte[] bytes, string contentType, string fname, string root)
        {
           
            List<EN.DriversDocumentDetailsEntiites> AttachmentList = new List<EN.DriversDocumentDetailsEntiites>();
            DataTable AttachmentDT = new DataTable();

            AttachmentDT = TrackerManager.AddAttachment(Userid, bytes, contentType, fname, root);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {
                    EN.DriversDocumentDetailsEntiites AttachmentDataList = new EN.DriversDocumentDetailsEntiites();

                    AttachmentDataList.UniqueID = Convert.ToInt32(row["UniqueID"]);
                   AttachmentDataList.RunningID = Convert.ToInt64(row["RunningID"]);
                    AttachmentDataList.Data = (byte[])(row["Data"]);
                    AttachmentDataList.DocFileName = Convert.ToString(row["DocFileName"]);
                    AttachmentDataList.ContentType = Convert.ToString(row["ContentType"]);
                    AttachmentDataList.srno = Convert.ToInt32(row["srno"]);

                    AttachmentList.Add(AttachmentDataList);
                }
            }
            return AttachmentList;
        }

        public string DeleteFile(long id, int Userid)
        {
         
            List<EN.DriversDocumentDetailsEntiites> AttachmentList = new List<EN.DriversDocumentDetailsEntiites>();
            DataTable JOAttachmentDT = new DataTable();

            string message = "";
            JOAttachmentDT = TrackerManager.DeleteDriverFile(id, Userid);
            if (JOAttachmentDT != null)
            {
                message = Convert.ToString(JOAttachmentDT.Rows[0][0]);
            }
            return message;
        }


        public EN.DriversDocumentDetailsEntiites GetFile(int id)
        {

            EN.DriversDocumentDetailsEntiites AttachmentDataList = new EN.DriversDocumentDetailsEntiites();
            DataTable AttachmentDT = new DataTable();
            AttachmentDT = TrackerManager.GetFileData(id);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {

                    AttachmentDataList.UniqueID = Convert.ToInt32(row["UniqueID"]);
                    AttachmentDataList.RunningID = Convert.ToInt64(row["RunningID"]);
                    AttachmentDataList.Data = (byte[])(row["Data"]);
                    AttachmentDataList.DocFileName = Convert.ToString(row["DocFileName"]);
                    AttachmentDataList.ContentType = Convert.ToString(row["ContentType"]);
                    //AttachmentDataList.srno = Convert.ToInt32(row["srno"]);


                }
            }
            return AttachmentDataList;
        }


        public string GetDrivermaxID()
        {

            List<EN.DriversDocumentDetailsEntiites> AttachmentList = new List<EN.DriversDocumentDetailsEntiites>();
            DataTable JOAttachmentDT = new DataTable();

            string message = "";
            JOAttachmentDT = TrackerManager.GetDriverMaxID();
            if (JOAttachmentDT != null)
            {
                message = Convert.ToString(JOAttachmentDT.Rows[0]["driverID"]);
            }
            return message;
        }


        public List<EN.DriverAttachment> GetDriverdocumentList(int DriverID)
        {
            List<EN.DriverAttachment> AttachmentList = new List<EN.DriverAttachment>();
            DataTable AttachmentDT = new DataTable();
            int i = 0;
            AttachmentDT = TrackerManager.GetDriverList(DriverID);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {
                    EN.DriverAttachment AttachmentDataList = new EN.DriverAttachment();
                    i++;
                    AttachmentDataList.SrNo = i;
                    AttachmentDataList.DocID = Convert.ToInt32(row["DocID"]);
                   // AttachmentDataList.DocumentType = Convert.ToString(row["DocumentType"]);
                    AttachmentDataList.FilePath = Convert.ToString(row["DocumentType"]);


                    AttachmentList.Add(AttachmentDataList);
                }
            }
            return AttachmentList;
        }


        public EN.DriverAttachment GetDriverdocumentDownloadList(int id, string id1)
        {
            EN.DriverAttachment AttachmentList = new EN.DriverAttachment();
            DataTable AttachmentDT = new DataTable();
            int i = 0;
            AttachmentDT = TrackerManager.GetDriverDownloadList(id, id1);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {
                    AttachmentList.DocID = Convert.ToInt32(row["DocID"]);
                    AttachmentList.DocumentType = Convert.ToString(row["DocumentType"]);
                    AttachmentList.FilePath = Convert.ToString(row["FilePath"]);
                    
                }
            }
            return AttachmentList;
        }


        //public List<EN.DriverDocumentTypeEntites> GetDriverdocumenttype()
        //{
        //    List<EN.DriverDocumentTypeEntites> AttachmentList = new List<EN.DriverDocumentTypeEntites>();
        //    DataTable AttachmentDT = new DataTable();
         
        //    AttachmentDT = TrackerManager.GetDriverDocumentList();
        //    if (AttachmentDT != null)
        //    {
        //        foreach (DataRow row in AttachmentDT.Rows)
        //        {
        //            EN.DriverDocumentTypeEntites AttachmentDataList = new EN.DriverDocumentTypeEntites();

        //            AttachmentDataList.DocumentID = Convert.ToInt32(row["documentID"]);
        //            AttachmentDataList.DocumentType = Convert.ToString(row["documenttype"]);


        //            AttachmentList.Add(AttachmentDataList);
        //        }
        //    }
        //    return AttachmentList;
        //}

        public List<EN.DriverDocumentTypeEntites> GetDriverdocumenttype()
        {
            DataTable DriverHoldDL = new DataTable();
            DriverHoldDL = TrackerManager.GetDriverDocumentList();
            List<EN.DriverDocumentTypeEntites> DriverTypeID = new List<EN.DriverDocumentTypeEntites>();

            if (DriverHoldDL.Rows.Count != 0)
            {
                foreach (DataRow row in DriverHoldDL.Rows)
                {
                    EN.DriverDocumentTypeEntites AttachmentDataList = new EN.DriverDocumentTypeEntites();
                    AttachmentDataList.DocumentID = Convert.ToInt32(row["documentID"]);
                    AttachmentDataList.DocumentType = Convert.ToString(row["documenttype"]);

                    DriverTypeID.Add(AttachmentDataList);
                }

            }
            return DriverTypeID;
        }

        public List<EN.DriverLeaveEntities> GetDriverLeaveDetails()
        {
            List<EN.DriverLeaveEntities> DriversDL = new List<EN.DriverLeaveEntities>();
            DataTable dt = new DataTable();
            string Table = "leave_master_dets";
            string Column = "LeaveID,LeaveName";
            string Condition = "";
            string OrderBy = "LeaveName";

            dt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.DriverLeaveEntities DriversList = new EN.DriverLeaveEntities();
                    DriversList.LeaveID = Convert.ToInt32(row["LeaveID"]);
                    DriversList.LeaveName = Convert.ToString(row["LeaveName"]);
                    DriversDL.Add(DriversList);
                }
            }
            return DriversDL;
        }

        public string SaveLeaveDetails(string Driverid, string TrailerNo, string DriverleaveFor, string Remarks, int Userid)
        {
            string message = "";
            DataTable ContainerDT = new DataTable();
            ContainerDT = TrackerManager.SaveLeaveDetails(Driverid, TrailerNo, DriverleaveFor, Remarks, Userid);
            if (ContainerDT.Rows.Count > 0)
            {
                message = "Record Saved successfully";
            }
            return message;
        }

        public EN.DriverEntities GetTrailerDetails(string Trailername)
        {
            DataTable DriverHoldDL = new DataTable();
            DriverHoldDL = TrackerManager.GetTrailerDetails(Trailername);
            EN.DriverEntities Driver = new EN.DriverEntities();

            if (DriverHoldDL.Rows.Count != 0)
            {
                foreach (DataRow row in DriverHoldDL.Rows)
                {
                    Driver.DriverID = Convert.ToInt32(row["driverid"]);
                    Driver.DriverName = Convert.ToString(row["drivername"]);
                    Driver.LicenceNo = Convert.ToString(row["LicenseNo"]);
                    Driver.AdharNumber = Convert.ToString(row["AdharNumber"]);
                    Driver.Pannumber = Convert.ToString(row["PANNo"]);
                    Driver.TrailerSize = Convert.ToString(row["tsize"]);
                    Driver.TrailerModel = Convert.ToString(row["ModelNo"]);
                }
            }
            return Driver;
        }

        public string ReleaseDriverid(string Driverid, string HoldID, int Userid)
        {
            string message = "";
            DataTable ContainerDT = new DataTable();
            ContainerDT = TrackerManager.ReleaseDriverhold(Driverid, HoldID, Userid);
            if (ContainerDT.Rows.Count > 0)
            {
                message = "Record Saved successfully";
            }
            return message;
        }
        public string SaveDriverLoanDetails(string Loadid, string LoanDate, string Driverid, string DriverName, string SlipNo, string FuelQty, string FuelCash, string DeductionType, string DeductionFuel
              , string DeductionAmount, string Deductionbank, string Remarks, int Userid, string DeductionFor)
        {
            string message = "";
            DataTable ContainerDT = new DataTable();
            ContainerDT = TrackerManager.SaveDriverLoanDetails(Loadid, LoanDate, Driverid, DriverName, SlipNo, FuelQty, FuelCash, DeductionType, DeductionFuel, DeductionAmount, Deductionbank, Remarks, Userid, DeductionFor);
            if (ContainerDT != null)
            {
                message = Convert.ToString(ContainerDT.Rows[0]["GetValue"]);

            }

            return message;
        }
        public string CheckDriverLiceneceDuplicate(string DLno)
        {
            string message = "";
            DataTable ContainerDT = new DataTable();
            ContainerDT = TrackerManager.CheckDriverDuplicate(DLno);
            if (ContainerDT.Rows.Count > 0)
            {
                message = Convert.ToString(ContainerDT.Rows[0]["drivername"]);

            }
            else
            {
                message = "Clear";
            }
            return message;
        }

        public string SaveBLDetails(string Name, string AdharNo, string Penno, string ContactNo, string VoterID, string Reference, string Remarks, int Userid)
        {
            string message = "";
            DataTable ContainerDT = new DataTable();
            ContainerDT = TrackerManager.SaveBLDet(Name, AdharNo, Penno, ContactNo, VoterID, Reference, Remarks, Userid);
            if (ContainerDT.Rows.Count > 0)
            {
                message = "Record Saved successfully";
            }
            return message;
        }
    }
}
