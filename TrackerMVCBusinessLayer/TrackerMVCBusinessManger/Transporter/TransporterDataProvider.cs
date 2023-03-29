using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using HC = TrackerMVCDataLayer.Helper;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Transporter
{
    public class TransporterDataProvider
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public EN.TransporterBankIEntities AddContainerData(EN.TransportEntities JD, int Userid)
        {
            List<EN.TransportEntities> BankList = new List<EN.TransportEntities>();            

            DataTable ContainerDT = new DataTable();
            ContainerDT = TrackerManager.AddBankData(JD.BankID, JD.AccountNo, JD.IFSCCode, JD.AccountName, JD.BranchName, JD.EmailID, JD.IsActive, Userid, JD.BankName);

            if (ContainerDT != null)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.TransportEntities JODTList = new EN.TransportEntities();

                    JODTList.BankID = Convert.ToString(row["BankID"]);
                    JODTList.BankName = Convert.ToString(row["BankName"]) + "<input Name=BankName type=hidden id=ddlBankName   value=" + Convert.ToString(row["BankName"]) + ">"; ;
                    JODTList.AccountNo = Convert.ToString(row["AccountNo"]) + "<input Name=AccountNo type=hidden id=txtAccountNumber   value=" + Convert.ToString(row["AccountNo"]) + ">"; ;
                    JODTList.IFSCCode = Convert.ToString(row["IFSCCode"]) + "<input Name=IFSCCode type=hidden id=txtIFSCCode   value=" + Convert.ToString(row["IFSCCode"]) + ">";
                    JODTList.AccountName = Convert.ToString(row["AccountName"]) + "<input Name=AccountName type=hidden id=txtAccountName   value=" + Convert.ToString(row["AccountName"]) + ">";

                    JODTList.BranchName = Convert.ToString(row["BranchName"]) + "<input Name=BranchName type=hidden id=txtBranchName   value=" + Convert.ToString(row["BranchName"]) + ">";
                    JODTList.EmailID = Convert.ToString(row["EmailID"]) + "<input Name=EmailID type=hidden id=txtEmailIDBank   value=" + Convert.ToString(row["EmailID"]) + ">";
                    JODTList.TempID = Convert.ToInt32(row["AutoID"]);
                    BankList.Add(JODTList);
                }

                // return JOrderList;
            }

            EN.TransporterBankIEntities Banklist = new EN.TransporterBankIEntities();

            Banklist.Banklist = BankList;

            return Banklist;
        }
        public List<EN.TransportEntities> DeleteBankData(string id, int Userid)
        {
            List<EN.TransportEntities> BankList = new List<EN.TransportEntities>();
            DataTable ContainerDT = new DataTable();

            ContainerDT = TrackerManager.DeleteBankData(id, Userid);
            if (ContainerDT != null)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.TransportEntities JODTList = new EN.TransportEntities();
                    //JODTList.JONo = Convert.ToInt32(row["JONo"]);
                    //JODTList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    //JODTList.FL = Convert.ToString(row["FL"]);
                    //JODTList.ISOCodeName = Convert.ToString(row["ISOCodeName"]);
                    //JODTList.Commodity_group_id = Convert.ToInt32(row["Commodity_group_id"]);
                    //JODTList.Commodity_Group_Name = Convert.ToString(row["CommodityGroup"]);
                    //JODTList.Cargotype = Convert.ToString(row["Cargotype"]);
                    //JODTList.Cargotypeid = Convert.ToInt32(row["Cargotypeid"]);
                    //JODTList.ISOCode = Convert.ToString(row["ISOCode"]);
                    //JODTList.Size = Convert.ToInt16(row["Size"]);

                    JODTList.BankID = Convert.ToString(row["BankID"]);
                    JODTList.BankName = Convert.ToString(row["BankName"]) + "<input Name=BankName type=hidden id=ddlBankName   value=" + Convert.ToString(row["BankName"]) + ">"; ;
                    JODTList.AccountNo = Convert.ToString(row["AccountNo"]) + "<input Name=AccountNo type=hidden id=txtAccountNumber   value=" + Convert.ToString(row["AccountNo"]) + ">"; ;
                    JODTList.IFSCCode = Convert.ToString(row["IFSCCode"]) + "<input Name=IFSCCode type=hidden id=txtIFSCCode   value=" + Convert.ToString(row["IFSCCode"]) + ">";
                    JODTList.AccountName = Convert.ToString(row["AccountName"]) + "<input Name=AccountName type=hidden id=txtAccountName   value=" + Convert.ToString(row["AccountName"]) + ">";

                    JODTList.BranchName = Convert.ToString(row["BranchName"]) + "<input Name=BranchName type=hidden id=txtBranchName   value=" + Convert.ToString(row["BranchName"]) + ">";
                    JODTList.EmailID = Convert.ToString(row["EmailID"]) + "<input Name=EmailID type=hidden id=txtEmailIDBank   value=" + Convert.ToString(row["EmailID"]) + ">";
                    JODTList.TempID = Convert.ToInt64(row["AutoID"]);

                    BankList.Add(JODTList);
                }
            }
            return BankList;
        }
        public EN.TransportEntities getDropDownList()
        {
            EN.TransportEntities objtransportbankentities = new EN.TransportEntities();
            DataTable DropDownList = new DataTable();
            DropDownList = TrackerManager.GetTransportBankData();

            List<EN.TransporterBankEntities> TransporterBankList = new List<EN.TransporterBankEntities>();

            //List<EN.MenuInfo> MenuList = new List<EN.MenuInfo>();
            if (DropDownList.Rows.Count > 0)
            {
                foreach (DataRow row in DropDownList.Rows)
                {
                    EN.TransporterBankEntities Transporter = new EN.TransporterBankEntities();
                    Transporter.bankID = Convert.ToString(row["bankID"]);
                    Transporter.bankname = Convert.ToString(row["bankname"]);
                    TransporterBankList.Add(Transporter);
                }
            }
            objtransportbankentities.BankList = TransporterBankList;         

            return objtransportbankentities;
        }
        public void DeleteDataFromTemp_Transport_Table(int userid)
        {
            TrackerManager.DeleteTempTransporterData(userid);
        }

        public List<EN.TransportEntities> getTransporterSummary()
        {
            List<EN.TransportEntities> TransportorList = new List<EN.TransportEntities>();
            DataTable dt = new DataTable();
            dt = TrackerManager.getTransporterSummary();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TransportEntities TransportorDL = new EN.TransportEntities();
                    TransportorDL.TRANSNAME = Convert.ToString(row["TransName"]);
                    TransportorDL.RegDateString = Convert.ToString(row["RegDate"]);
                    TransportorDL.TransID = Convert.ToInt32(row["TransID"]);
                    TransportorList.Add(TransportorDL);
                }
            }
            return TransportorList;
        }

        public EN.TransportData getTransporterData(string ID, int Userid)
        {
            EN.TransportData TransportDL = new EN.TransportData();   
            try {
                int TransID = Convert.ToInt32(ID);
                EN.TransportEntities TransportList = new EN.TransportEntities();
                List<EN.BankEntities> BankList = new List<EN.BankEntities>();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                ds = TrackerManager.getTransportData(TransID, Userid);
                dt = ds.Tables[0];
                dt1 = ds.Tables[1];

                foreach (DataRow row in dt.Rows)
                {

                    TransportList.TransID = Convert.ToInt32(row["TransID"]);
                    TransportList.TRANSNAME = Convert.ToString(row["TransName"]);
                    TransportList.RegDateString = Convert.ToString(row["RegDateFormat"]);
                    TransportList.ADDRESS = Convert.ToString(row["Address"]);
                    TransportList.CONTACTPERSON = Convert.ToString(row["ContactPerson"]);
                    TransportList.MOBILENO = Convert.ToString(row["MobileNo"]);
                    TransportList.EmailID = Convert.ToString(row["EmailID"]);
                   
                }
                foreach (DataRow row1 in dt1.Rows)
                {
                    EN.BankEntities BankDL = new EN.BankEntities();
                    BankDL.BankID = Convert.ToString(row1["BankID"]);
                    BankDL.BankName = Convert.ToString(row1["BankName"]);
                    BankDL.AccountNo = Convert.ToString(row1["AccountNo"]);
                    BankDL.IFSCCode = Convert.ToString(row1["IFSCCode"]);
                    BankDL.AccountName = Convert.ToString(row1["AccountName"]);
                    BankDL.BranchName = Convert.ToString(row1["BranchName"]);
                    BankDL.EmailID = Convert.ToString(row1["EmailID"]);
                    BankDL.TempID = Convert.ToInt32(row1["AutoID"]);
                    BankList.Add(BankDL);

                }

                TransportDL.TransportList = TransportList;
                TransportDL.Banklist = BankList;
                return TransportDL;
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return TransportDL;
        }
        //Codes By Prashant
        public List<EN.TransportExpenses> GettransportDropdownList()
        {

            List<EN.TransportExpenses> TransportExpenses = new List<EN.TransportExpenses>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetExpensesDropDownList();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.TransportExpenses ExpensesList = new EN.TransportExpenses();

                    ExpensesList.transexpenseid = Convert.ToInt32(row["transexpenseid"]);
                    ExpensesList.transexpensename = Convert.ToString(row["transexpensename"]);
                    TransportExpenses.Add(ExpensesList);
                }
            }
            return TransportExpenses;
        }



        

        public string SaveExpensesDetails(string ExpensesDate, string ExpensesFor, string Amount, string Remark, int userid, string Voucherno)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.SaveTransportExpenses(ExpensesDate, ExpensesFor, Amount, Remark, userid, Voucherno);

            message = "Record saved successfully!";
            return message;
        }




        public List<EN.ExpensesForTransport> GetVoucherNumber(string Containerno)
        {
            List<EN.ExpensesForTransport> VoucherList = new List<EN.ExpensesForTransport>();
            DataTable VoucherDL = new DataTable();

            VoucherDL = TrackerManager.GetVoucherNumer(Containerno);
            int Count = 1;
            if (VoucherDL != null)
            {
                foreach (DataRow row in VoucherDL.Rows)
                {
                    EN.ExpensesForTransport VoucherDataList = new EN.ExpensesForTransport();

                    VoucherDataList.VoucherNumber = Convert.ToString(row["VoucherNo"]);
                    VoucherDataList.VoucherDate = Convert.ToString(row["VoucherDate"]);
                    VoucherDataList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    VoucherDataList.Size = Convert.ToString(row["Size"]);
                    VoucherDataList.Drivername = Convert.ToString(row["Drivername"]);
                    VoucherDataList.FromLocation = Convert.ToString(row["FromLocation"]);
                    VoucherDataList.ToLocation = Convert.ToString(row["ToLocation"]);
                    VoucherDataList.Activity = Convert.ToString(row["Activity"]);
                    VoucherDataList.Party = Convert.ToString(row["Party"]);
                    VoucherDataList.AutoID = Convert.ToString(row["AutoID"]);
                    VoucherDataList.TrailerName = Convert.ToString(row["TrailerNo"]);

                    VoucherList.Add(VoucherDataList);
                }
            }
            return VoucherList;
        }

        public List<EN.ExpensesForTransport> GetOffloadingtransdetails(string Containerno)
        {
            List<EN.ExpensesForTransport> VoucherList = new List<EN.ExpensesForTransport>();
            DataTable VoucherDL = new DataTable();

            VoucherDL = TrackerManager.GetoffloadingTransdetails(Containerno);
            int Count = 1;
            if (VoucherDL != null)
            {
                foreach (DataRow row in VoucherDL.Rows)
                {
                    EN.ExpensesForTransport VoucherDataList = new EN.ExpensesForTransport();

                    VoucherDataList.VoucherNumber = Convert.ToString(row["VoucherNo"]);
                    VoucherDataList.VoucherDate = Convert.ToString(row["VoucherDate"]);
                    VoucherDataList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    VoucherDataList.Size = Convert.ToString(row["Size"]);
                    VoucherDataList.Drivername = Convert.ToString(row["Drivername"]);
                    VoucherDataList.FromLocation = Convert.ToString(row["FromLocation"]);
                    VoucherDataList.ToLocation = Convert.ToString(row["ToLocation"]);
                    VoucherDataList.Activity = Convert.ToString(row["Activity"]);
                    VoucherDataList.Party = Convert.ToString(row["Party"]);
                    VoucherDataList.AutoID = Convert.ToString(row["AutoID"]);
                    VoucherDataList.TrailerName = Convert.ToString(row["TrailerNo"]);

                    VoucherList.Add(VoucherDataList);
                }
            }
            return VoucherList;
        }

        public string SaveExpenseName(string Expensename)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.SaveExpenseName(Expensename);
            if (ExpensesDL != null)
            {
                message = Convert.ToString(ExpensesDL.Rows[0]["record"]);
            }


            return message;
        }



        public string CheckVoucherno(string Voucher, int ExpenseFordata, string ContainerNo, string Type)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.CheckExpenseFor(Voucher, ExpenseFordata, ContainerNo, Type);
            if (ExpensesDL.Rows.Count > 0)
            {

                message = "1";
            }
            else
            {
                message = "0";
            }

            return message;
        }
        // Codes END By Prashant

        public List<EN.TransporterEntities> GetTransportername()
        {
            List<EN.TransporterEntities> VoucherList = new List<EN.TransporterEntities>();
            DataTable VoucherDL = new DataTable();

            VoucherDL = TrackerManager.GetTranspotername();
           
            if (VoucherDL != null)
            {
                foreach (DataRow row in VoucherDL.Rows)
                {
                    EN.TransporterEntities VoucherDataList = new EN.TransporterEntities();

                    VoucherDataList.TransID = Convert.ToInt16(row["transid"]);
                    VoucherDataList.TransName = Convert.ToString(row["transname"]);
                    VoucherDataList.FuelShortCode = Convert.ToString(row["FuelShortCode"]);
                    VoucherDataList.DriverCount = Convert.ToString(row["trailerCount"]);
                    VoucherDataList.TrailerCount = Convert.ToString(row["driversCount"]);
                   

                    VoucherList.Add(VoucherDataList);
                }
            }
            return VoucherList;
        }

        public List<EN.DriverEntities> GetdriverCount()
        {
            List<EN.DriverEntities> DriversList = new List<EN.DriverEntities>();
            DataTable DriversDL = new DataTable();

            DriversDL = TrackerManager.GetDriversTotalCount();

            if (DriversDL != null)
            {
                foreach (DataRow row in DriversDL.Rows)
                {
                    EN.DriverEntities VoucherDataList = new EN.DriverEntities();

                    VoucherDataList.DriverID = Convert.ToInt16(row["driverid"]);



                    DriversList.Add(VoucherDataList);
                }
            }
            return DriversList;
        }


        public List<EN.TrailersEntities> GetTrailersCount()
        {
            List<EN.TrailersEntities> TrailersList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetTrailersTotalCount();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();

                    TrailersDataList.trailerid = Convert.ToInt16(row["trailerid"]);



                    TrailersList.Add(TrailersDataList);
                }
            }
            return TrailersList;
        }



        public List<EN.TrailersEntities> GetTrailersLocation()
        {
            List<EN.TrailersEntities> TrailerslocationList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetTrailerlocation();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();

                    TrailersDataList.Location = Convert.ToString(row["Location"]);
                    TrailersDataList.LocationID = Convert.ToInt16(row["LocationID"]);
                    TrailersDataList.Count = Convert.ToInt16(row["COUNT"]);



                    TrailerslocationList.Add(TrailersDataList);
                }
            }
            return TrailerslocationList;
        }


        public List<EN.TrailersEntities> GetTrailersLocationICDTUMB()
        {
            List<EN.TrailersEntities> TrailerslocationICDList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetTrailerlocationICDTUMB();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();

                    TrailersDataList.Location = Convert.ToString(row["Location"]);
                    TrailersDataList.LocationID = Convert.ToInt16(row["LocationID"]);
                    TrailersDataList.Count = Convert.ToInt16(row["COUNT"]);



                    TrailerslocationICDList.Add(TrailersDataList);
                }
            }
            return TrailerslocationICDList;
        }




        public List<EN.TrailersEntities> GetTrailersLocationCount()
        {
            List<EN.TrailersEntities> TrailerslocationCOuntList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetTrailerlocationttoalCount();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();

                  
                    TrailersDataList.Count = Convert.ToInt16(row["COUNT"]);



                    TrailerslocationCOuntList.Add(TrailersDataList);
                }
            }
            return TrailerslocationCOuntList;
        }

        public List<EN.TrailersEntities> GetTrailersLocationlIST(int Locationid)
        {
            List<EN.TrailersEntities> TrailerslocationCOuntList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetLocationTrailerList(Locationid);

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();


                    TrailersDataList.Location = Convert.ToString(row["trailername"]);
                    TrailersDataList.trans_date = Convert.ToString(row["trans_date"]);



                    TrailerslocationCOuntList.Add(TrailersDataList);
                }
            }
            return TrailerslocationCOuntList;
        }
        public string SaveTransportExpenses(DataTable Invoicedata, string InvoiceDate, string InvoiceNo, int Userid,string ddlGSTNo)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("Userid", Userid);
            parameterList.Add("InvoiceDate", InvoiceDate);
            parameterList.Add("InvoiceNo", InvoiceNo);
            parameterList.Add("ddlGSTNo", ddlGSTNo);
            int i = db.AddTypeTableData("USP_InsertTransportExpensesdetails", parameterList, Invoicedata, "PT_ExpensesEntry", "@PT_ExpensesEntry");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public EN.ExpensesForTransport ReGetPrintdetails(string Entryid)
        {
            EN.ExpensesForTransport transportList = new EN.ExpensesForTransport();
            DataTable DL = new DataTable();
            DL = TrackerManager.ReGetExpencePrint(Entryid);
            int count = 1;
            if (DL != null)
            {
                foreach (DataRow rows in DL.Rows)
                {
                    EN.ExpensesForTransport expenses = new EN.ExpensesForTransport();
                    transportList.SrNo = Convert.ToInt32(count++);
                    transportList.EntryDate = Convert.ToString(rows["Entry Date"]);
                    transportList.Amount = Convert.ToString(rows["Amount"]);
                    transportList.transexpensename = Convert.ToString(rows["Expence Name"]);
                    transportList.Remark = Convert.ToString(rows["Remark"]);
                    transportList.VoucherNumber = Convert.ToString(rows["Voucher No"]);
                    transportList.TrailerNo = Convert.ToString(rows["TrailerNo"]);
                    transportList.ContainerNo = Convert.ToString(rows["ContainerNo"]);
                    transportList.Size = Convert.ToString(rows["Size"]);
                    transportList.Username = Convert.ToString(rows["UserName"]);
                    transportList.ExpenseSlipNo = Convert.ToString(rows["expenseslipno"]);

                    // transportList.Add(expenses);
                }
            }
            return transportList;

        }

        public EN.ExpensesForTransport GetPrintdetails()
        {
            EN.ExpensesForTransport transportList = new EN.ExpensesForTransport();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetExpencePrint();
            int count = 1;
            if (DL != null)
            {
                foreach (DataRow rows in DL.Rows)
                {
                    EN.ExpensesForTransport expenses = new EN.ExpensesForTransport();
                    transportList.SrNo = Convert.ToInt32(count++);
                    transportList.EntryDate = Convert.ToString(rows["Entry Date"]);
                    transportList.Amount = Convert.ToString(rows["Amount"]);
                    transportList.transexpensename = Convert.ToString(rows["Expence Name"]);
                    transportList.Remark = Convert.ToString(rows["Remark"]);
                    transportList.VoucherNumber = Convert.ToString(rows["Voucher No"]);
                    transportList.TrailerNo = Convert.ToString(rows["TrailerNo"]);
                    transportList.ContainerNo = Convert.ToString(rows["ContainerNo"]);
                    transportList.Size = Convert.ToString(rows["Size"]);
                    transportList.Username = Convert.ToString(rows["UserName"]);
                    transportList.ExpenseSlipNo = Convert.ToString(rows["expenseslipno"]);

                    // transportList.Add(expenses);
                }
            }
            return transportList;

        }



        public List<EN.ExpensesForTransport> gettransportexpenses(string FromDate, string ToDate, int Userid)
        {
            List<EN.ExpensesForTransport> transportList = new List<EN.ExpensesForTransport>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetTransportExpenses(FromDate, ToDate, Userid);
            int count = 1;
            if (DL != null)
            {
                foreach (DataRow rows in DL.Rows)
                {
                    EN.ExpensesForTransport expenses = new EN.ExpensesForTransport();
                    expenses.SrNo = Convert.ToInt32(count++);
                    expenses.EntryDate = Convert.ToString(rows["EntryDate"]);
                    expenses.Amount = Convert.ToString(rows["Amount"]);
                    expenses.transexpensename = Convert.ToString(rows["transexpensename"]);
                    expenses.Remark = Convert.ToString(rows["Remark"]);
                    expenses.VoucherNumber = Convert.ToString(rows["VoucherNo"]);
                    expenses.TrailerNo = Convert.ToString(rows["TrailerNo"]);
                    expenses.ContainerNo = Convert.ToString(rows["ContainerNo"]);
                    expenses.Size = Convert.ToString(rows["Size"]);
                    expenses.Entryid = Convert.ToInt32(rows["entryid"]);
                    expenses.ActivityMaster = Convert.ToString(rows["Activity"]);
                    expenses.ExpenseSlipNo = Convert.ToString(rows["expenseslipno"]);
                    expenses.Username = Convert.ToString(rows["UserName"]);
                    expenses.PaymentMode = Convert.ToString(rows["paymode"]);
                    expenses.transexpenseID = Convert.ToInt32(rows["transexpenseid"]);
                    expenses.vendorID = Convert.ToString(rows["vendor_name"]);
                    expenses.CGST = Convert.ToInt32(rows["CGST"]);
                    expenses.SGST = Convert.ToInt32(rows["SGST"]);
                    expenses.IGST = Convert.ToInt32(rows["IGST"]);
                    expenses.GrandTotal = Convert.ToInt32(rows["grandamount"]);
                    expenses.Taxid = Convert.ToString(rows["taxid"]);
                    expenses.InvoiceNo = Convert.ToString(rows["InvoiceNo"]);
                    expenses.InvoiceDate = Convert.ToString(rows["InvoiceDate"]);


                    expenses.CollectInvoiceNo = Convert.ToString(rows["TaxInvoiceNo"]);
                    expenses.CollectInvoiceDate = Convert.ToString(rows["TaxInvoiceDate"]);
                    expenses.CollectParty = Convert.ToString(rows["PartyName"]);
                    expenses.CollectAmount = Convert.ToString(rows["InvoiceAmount"]);
                    transportList.Add(expenses);
                }
            }
            return transportList;

        }


        public string CancelexpenseID(int Entryid, int transexpenseID, int Userid)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.CancelExpenseID(Entryid, transexpenseID, Userid);
            return message;
        }
        //public List<EN.ExpensesForTransport> GetEditExpenseHead(string Entryid)
        //{
        //    List<EN.ExpensesForTransport> transportList = new List<EN.ExpensesForTransport>();
        //    DataTable DL = new DataTable();
        //    DL = TrackerManager.GetEditTransportExpenses(Entryid);
        //    int count = 1;
        //    if (DL != null)
        //    {
        //        foreach (DataRow rows in DL.Rows)
        //        {
        //            EN.ExpensesForTransport expenses = new EN.ExpensesForTransport();
        //            expenses.SrNo = Convert.ToInt32(count++);
        //            expenses.VoucherNumber = Convert.ToString(rows["VoucherNo"]);
        //            expenses.transexpensename = Convert.ToString(rows["transexpensename"]);
        //            expenses.ExpensesheadID = Convert.ToString(rows["transexpenseid"]);
        //            expenses.TrailerNo = Convert.ToString(rows["TrailerNo"]);
        //            expenses.ContainerNo = Convert.ToString(rows["ContainerNo"]);
        //            expenses.Size = Convert.ToString(rows["Size"]);
        //            expenses.ActivityMaster = Convert.ToString(rows["Activity"]);
        //            expenses.AutoActivityid = Convert.ToString(rows["AutoID"]);
        //            expenses.Entryid = Convert.ToInt32(rows["entryid"]);
        //            expenses.Amount = Convert.ToString(rows["amount"]);
        //            expenses.Remark = Convert.ToString(rows["Remark"]);
        //            expenses.PaymentMode = Convert.ToString(rows["PayMode"]);
        //            expenses.PaymentmodeID = Convert.ToString(rows["paymodeid"]);
        //            transportList.Add(expenses);
        //        }
        //    }
        //    return transportList;

        //}

        public List<EN.PaymentModes> getForPaymentModes()
        {
            List<EN.PaymentModes> CustomerDL = new List<EN.PaymentModes>();
            DataTable CustomerDT = new DataTable();
            string Table = "payment_modes";
            string Column = "paymodeID,paymode";
            string Condition = "";
            string OrderBy = "paymodeID";

            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.PaymentModes CustomerList = new EN.PaymentModes();
                    CustomerList.PaymentId = Convert.ToInt32(row["paymodeID"]);
                    CustomerList.PaymentMode = Convert.ToString(row["paymode"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }

        public List<EN.VendorMaster> GetVendorDetails()
        {

            List<EN.VendorMaster> TransportExpenses = new List<EN.VendorMaster>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetVendorListForExpenses();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.VendorMaster ExpensesList = new EN.VendorMaster();

                    ExpensesList.VendorId = Convert.ToInt32(row["Vendor_ID"]);
                    ExpensesList.Name = Convert.ToString(row["Vendor_Name"]);
                    TransportExpenses.Add(ExpensesList);
                }
            }
            return TransportExpenses;
        }

        public List<EN.SettingTaxdetails> GetsettingDetails()
        {

            List<EN.SettingTaxdetails> TransportExpenses = new List<EN.SettingTaxdetails>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetTaxListForExpenses();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.SettingTaxdetails ExpensesList = new EN.SettingTaxdetails();

                    ExpensesList.TaxID = Convert.ToInt32(row["settingsid"]);
                    ExpensesList.Taxname = Convert.ToString(row["taxname"]);
                    TransportExpenses.Add(ExpensesList);
                }
            }
            return TransportExpenses;
        }

        public string EditSaveTransportExpenses(DataTable Invoicedata, int Userid, int Editentryid, int Activityid, int ExpensesID, string ExpensesDate)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("Userid", Userid);
            parameterList.Add("entryid", Editentryid);
            parameterList.Add("Activity", Activityid);
            parameterList.Add("ExpensesID", ExpensesID);
            parameterList.Add("ExpensesDate", ExpensesDate);
            int i = db.AddTypeTableData("USP_EditTransportExpensesdetails", parameterList, Invoicedata, "PT_EditExpensesEntry", "@PT_EditExpensesEntry");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public List<EN.GstDetails> GetVendorGSTDetails(string VendorID)
        {

            List<EN.GstDetails> TransportExpenses = new List<EN.GstDetails>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetVendorGTDetails(VendorID);

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.GstDetails ExpensesList = new EN.GstDetails();
                    ExpensesList.GSTuniqueid = Convert.ToInt32(row["GSTID"]);
                    ExpensesList.GstuniqueName = Convert.ToString(row["GSTIn_uniqID"]);
            
                    TransportExpenses.Add(ExpensesList);
                }
            }
            return TransportExpenses;
        }
        public List<EN.TransportexpensesAttachment> GetTransportExpensesDoc(int Entryid)
        {
            List<EN.TransportexpensesAttachment> AttachmentList = new List<EN.TransportexpensesAttachment>();
            DataTable AttachmentDT = new DataTable();
            int i = 0;
            AttachmentDT = TrackerManager.GetTransportDOcList(Entryid);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {
                    EN.TransportexpensesAttachment AttachmentDataList = new EN.TransportexpensesAttachment();
                    i++;
                    AttachmentDataList.SrNo = i;
                    AttachmentDataList.DocID = Convert.ToInt32(row["DocID"]);
                    AttachmentDataList.FilePath = Convert.ToString(row["FilePath"]);
                    AttachmentDataList.Entryid = Convert.ToString(row["Transidentryid"]);


                    AttachmentList.Add(AttachmentDataList);
                }
            }
            return AttachmentList;
        }

        public string CheckUservalidate(int Userid)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.CheckForMenudetails(Userid);

            if (ExpensesDL.Rows.Count > 0)
            {

                message = "1";
            }
            else
            {
                message = "0";
            }


            return message;
        }


        public string DeleteExpenseID(int Entryid, int transexpenseID)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.DeleteTransportExpenses(Entryid, transexpenseID);
            return message;
        }


        //Code By Prashant   7 Jan 2019





        public List<EN.TrailersEntities> GetInsuranceANDRTOCount(string paramerter)
        {
            List<EN.TrailersEntities> TrailerslocationCOuntList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            if (paramerter == "Insurance")
            {
                trailersDL = TrackerManager.GetInsuranceANDRTOCount(paramerter);
            }
            if (paramerter == "Tax")
            {
                trailersDL = TrackerManager.GetInsuranceANDRTOCount(paramerter);
            }
            if (paramerter == "Fitness")
            {
                trailersDL = TrackerManager.GetInsuranceANDRTOCount(paramerter);
            }
            if (paramerter == "Green Tax")
            {
                trailersDL = TrackerManager.GetInsuranceANDRTOCount(paramerter);
            }
            if (paramerter == "National Permit Expiry")
            {
                trailersDL = TrackerManager.GetInsuranceANDRTOCount(paramerter);
            }
            if (paramerter == "Local Permit Expiry")
            {
                trailersDL = TrackerManager.GetInsuranceANDRTOCount(paramerter);
            }
            if (paramerter == "")
            {
                trailersDL = TrackerManager.GetInsuranceANDRTOCount(paramerter);
            }



            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();


                    TrailersDataList.Policy_End_Date = Convert.ToString(row["Process"]);



                    TrailerslocationCOuntList.Add(TrailersDataList);
                }
            }
            return TrailerslocationCOuntList;
        }

        public List<EN.TrailersEntities> GetTrailerWihoutDriver()
        {
            List<EN.TrailersEntities> TrailerslocationCOuntList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetDriverwithourdate();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();


                    TrailersDataList.Count = Convert.ToInt16(row["Count"]);



                    TrailerslocationCOuntList.Add(TrailersDataList);
                }
            }
            return TrailerslocationCOuntList;
        }

        public List<EN.TrailersEntities> Getaccident()
        {
            List<EN.TrailersEntities> TrailerslocationCOuntList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetAccidentDetails();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();


                    TrailersDataList.Count = Convert.ToInt16(row["Count"]);



                    TrailerslocationCOuntList.Add(TrailersDataList);
                }
            }
            return TrailerslocationCOuntList;
        }

        public List<EN.TrailersEntities> GetBreakDown()
        {
            List<EN.TrailersEntities> TrailerslocationCOuntList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetBreakDownList();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();


                    TrailersDataList.Count = Convert.ToInt16(row["Count"]);



                    TrailerslocationCOuntList.Add(TrailersDataList);
                }
            }
            return TrailerslocationCOuntList;
        }

        public List<EN.TrailersEntities> GetServicingDetails()
        {
            List<EN.TrailersEntities> TrailerslocationCOuntList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.getServicingDetails();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();


                    TrailersDataList.Count = Convert.ToInt16(row["Count"]);



                    TrailerslocationCOuntList.Add(TrailersDataList);
                }
            }
            return TrailerslocationCOuntList;
        }

        public List<EN.TrailersEntities> GetTrailerGarageCount()
        {
            List<EN.TrailersEntities> TrailerslocationCOuntList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetTrailerGarageDetails();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();


                    TrailersDataList.Count = Convert.ToInt16(row["COUNT"]);



                    TrailerslocationCOuntList.Add(TrailersDataList);
                }
            }
            return TrailerslocationCOuntList;
        }


        public List<EN.TrailersEntities> GetTrailerShifting()
        {
            List<EN.TrailersEntities> TrailerslocationCOuntList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetTrailerShiftingDEtails();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();


                    TrailersDataList.Count = Convert.ToInt16(row["Count"]);



                    TrailerslocationCOuntList.Add(TrailersDataList);
                }
            }
            return TrailerslocationCOuntList;
        }

        public List<EN.TrailersEntities> GetWithoutpaper()
        {
            List<EN.TrailersEntities> TrailerslocationCOuntList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.GetWithoutpaperList();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();


                    TrailersDataList.Count = Convert.ToInt16(row["Count"]);



                    TrailerslocationCOuntList.Add(TrailersDataList);
                }
            }
            return TrailerslocationCOuntList;
        }

        //Code By Prashant 
        public List<EN.TrailersEntities> GetTrailerLocationCount()
        {
            List<EN.TrailersEntities> TrailerslocationICDList = new List<EN.TrailersEntities>();
            DataTable trailersDL = new DataTable();

            trailersDL = TrackerManager.getTrailerLocationCountNavkar();

            if (trailersDL != null)
            {
                foreach (DataRow row in trailersDL.Rows)
                {
                    EN.TrailersEntities TrailersDataList = new EN.TrailersEntities();
                    TrailersDataList.Count = Convert.ToInt16(row["COUNT"]);



                    TrailerslocationICDList.Add(TrailersDataList);
                }
            }
            return TrailerslocationICDList;
        }

        public string SavefastagNoDetails(string trailerid, string tagno, string remakrs, int chkIsActive, int Userid)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.SaveFastagDetails(trailerid, tagno, remakrs, chkIsActive, Userid);



            return message;
        }


        public string CheckFastagNo(string trailerid, string tagno, string remakrs, int chkIsActive)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.CheckFastagNo(trailerid, tagno, remakrs, chkIsActive);
            if (ExpensesDL.Rows.Count > 0)
            {

                message = "1";
            }
            else
            {
                message = "0";
            }

            return message;
        }



        public List<EN.FastagMasterEntities> GetFastagMaster()
        {
            List<EN.FastagMasterEntities> transportList = new List<EN.FastagMasterEntities>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetFastagDetails();

            if (DL != null)
            {
                foreach (DataRow rows in DL.Rows)
                {
                    EN.FastagMasterEntities expenses = new EN.FastagMasterEntities();
                    expenses.entryid = Convert.ToInt32(rows["EntryID"]);
                    expenses.Entrydate = Convert.ToString(rows["Entry Date"]);
                    expenses.VehicleNo = Convert.ToString(rows["Vehicle No"]);
                    expenses.tagID = Convert.ToString(rows["Tag ID"]);
                    expenses.Remarks = Convert.ToString(rows["Remarks"]);
                    expenses.UserName = Convert.ToString(rows["UserName"]);
                    expenses.isactive = Convert.ToInt32(rows["isactive"]);
                    expenses.Status = Convert.ToString(rows["Status"]);

                    transportList.Add(expenses);
                }
            }
            return transportList;

        }


        public string EditFastagDetails(string EditEntryid, string EditTagNo, int chkIsActive, int userid)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.EditFastagDetails(EditEntryid, EditTagNo, chkIsActive, userid);



            return message;
        }

        public string GetfastagtariffDetails(int fromlocation, int Tolocation, int Toll, int amount, string Passing, int Userid)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.SaveFastagTariffDetails(fromlocation, Tolocation, Toll, amount, Passing, Userid);



            return message;
        }


        public string Checkfastagtariffalready(int fromlocation, int Tolocation, int Toll, int amount, string Passing)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.CheckFastagTariffExists(fromlocation, Tolocation, Toll, amount, Passing);
            if (ExpensesDL.Rows.Count > 0)
            {

                message = "1";
            }
            else
            {
                message = "0";
            }

            return message;
        }


        public List<EN.FastagMasterEntities> GetFastagtariffDetails()
        {
            List<EN.FastagMasterEntities> transportList = new List<EN.FastagMasterEntities>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetFastagtariffreport();

            if (DL != null)
            {
                foreach (DataRow rows in DL.Rows)
                {
                    EN.FastagMasterEntities expenses = new EN.FastagMasterEntities();
                    expenses.entryid = Convert.ToInt32(rows["Entry ID"]);
                    expenses.Entrydate = Convert.ToString(rows["Entry date"]);
                    expenses.Fromid = Convert.ToString(rows["From Location"]);
                    expenses.Toid = Convert.ToString(rows["TO Location"]);
                    expenses.toll = Convert.ToString(rows["Toll Location"]);
                    expenses.Amount = Convert.ToString(rows["amount"]);
                    expenses.UserName = Convert.ToString(rows["UserName"]);
                    expenses.Fromlocationid = Convert.ToInt32(rows["fromid"]);
                    expenses.tolocationid = Convert.ToInt32(rows["toid"]);
                    expenses.tollid = Convert.ToInt32(rows["tollid"]);
                    expenses.passing = Convert.ToString(rows["passing"]);


                    transportList.Add(expenses);
                }
            }
            return transportList;

        }


        public string CanceltariffDetails(int entryid)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.Canceldetails(entryid);



            return message;
        }





        public string SaveTollDetails(string TollName, int OngoingAmount, int Returnamount, string Passing, int Userid)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.SaveTollDetails(TollName, OngoingAmount, Returnamount, Passing, Userid);



            return message;
        }


        public List<EN.TollSummary> GetTollDetails()
        {
            List<EN.TollSummary> TollList = new List<EN.TollSummary>();
            DataTable DL = new DataTable();
            int Srno = 1;
            DL = TrackerManager.GetTollMasterDetails();

            if (DL != null)
            {
                foreach (DataRow rows in DL.Rows)
                {
                    EN.TollSummary TollListDl = new EN.TollSummary();

                    TollListDl.TollID = Convert.ToInt32(rows["Tollid"]);
                    TollListDl.SRno = Convert.ToInt32(Srno++);
                    TollListDl.TollName = Convert.ToString(rows["TollName"]);
                    TollListDl.Amount = Convert.ToInt32(rows["Amount"]);
                    TollListDl.Returnamount = Convert.ToInt32(rows["Returnamount"]);
                    TollListDl.Passing = Convert.ToString(rows["Passing"]);
                    TollListDl.tollMasterid = Convert.ToString(rows["tollMasterid"]);




                    TollList.Add(TollListDl);
                }
            }
            return TollList;

        }

        public string EditTollDetails(string TollName, int OngoingAmount, int Returnamount, int TollID, string PassingEdit, int Userid)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.EditTollDetails(TollName, OngoingAmount, Returnamount, TollID, PassingEdit, Userid);



            return message;
        }

        public List<EN.TollSummary> getTollLocation()
        {
            List<EN.TollSummary> locationDL = new List<EN.TollSummary>();
            DataTable dt = new DataTable();
            dt = TrackerManager.GetTollMasterDetailsDropDown();


            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TollSummary locationList = new EN.TollSummary();
                    locationList.TollID = Convert.ToInt32(row["Tollid"]);
                    locationList.TollName = Convert.ToString(row["TollName"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }


        public string GetTrollAmount(int TollID, string Passing)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.Gettollamount(TollID, Passing);
            if (ExpensesDL.Rows.Count > 0)
            {

                message = Convert.ToString(ExpensesDL.Rows[0]["Amount"]);
            }


            return message;
        }


        public string ChecktollName(string TollNames, string Passing)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.ChecltollName(TollNames, Passing);
            if (ExpensesDL.Rows.Count > 0)
            {

                message = "1";
            }
            else
            {
                message = "0";
            }

            return message;
        }


        public string SaveTollName(string Tollname)
        {
            string message = "";
            DataTable ExpensesDL = new DataTable();
            ExpensesDL = TrackerManager.SaveTollName(Tollname);
            if (ExpensesDL != null)
            {
                message = Convert.ToString(ExpensesDL.Rows[0]["record"]);
            }


            return message;
        }

        public List<EN.ExpensesForTransport> GetEditExpenseHead(string Entryid, string transexpenseID)
        {
            List<EN.ExpensesForTransport> transportList = new List<EN.ExpensesForTransport>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetEditTransportExpenses(Entryid, transexpenseID);
            int count = 1;
            if (DL != null)
            {
                foreach (DataRow rows in DL.Rows)
                {
                    EN.ExpensesForTransport expenses = new EN.ExpensesForTransport();
                    expenses.SrNo = Convert.ToInt32(count++);
                    expenses.VoucherNumber = Convert.ToString(rows["VoucherNo"]);
                    expenses.transexpensename = Convert.ToString(rows["transexpensename"]);
                    expenses.ExpensesheadID = Convert.ToString(rows["transexpenseid"]);
                    expenses.TrailerNo = Convert.ToString(rows["TrailerNo"]);
                    expenses.ContainerNo = Convert.ToString(rows["ContainerNo"]);
                    expenses.Size = Convert.ToString(rows["Size"]);
                    expenses.ActivityMaster = Convert.ToString(rows["Activity"]);
                    expenses.AutoActivityid = Convert.ToString(rows["AutoID"]);
                    expenses.Entryid = Convert.ToInt32(rows["entryid"]);
                    expenses.Amount = Convert.ToString(rows["amount"]);
                    expenses.Remark = Convert.ToString(rows["Remark"]);
                    expenses.PaymentMode = Convert.ToString(rows["PayMode"]);
                    expenses.PaymentmodeID = Convert.ToString(rows["paymodeid"]);
                    expenses.vendorID = Convert.ToString(rows["vendor_id"]);
                    expenses.Taxid = Convert.ToString(rows["taxid"]);
                    expenses.CGST = Convert.ToDouble(rows["CGST"]);
                    expenses.CGST = Convert.ToDouble(rows["SGST"]);
                    expenses.IGST = Convert.ToDouble(rows["IGST"]);
                    expenses.GrandTotal = Convert.ToDouble(rows["grandamount"]);
                    expenses.InvoiceNo = Convert.ToString(rows["InvoiceNo"]);
                    transportList.Add(expenses);
                }
            }
            return transportList;

        }


        public List<EN.FastagDetailsEntries> UpdateTransportRecoDetails(DataTable DriverPaymentDT, Int32 userId)
        {
            //EN.DriverPaymentReco objdriverpaymententities = new EN.DriverPaymentReco();
            List<EN.FastagDetailsEntries> DriverPaymentList = new List<EN.FastagDetailsEntries>();
            if (DriverPaymentDT != null)
            {
                int count = 1;
                foreach (DataRow row in DriverPaymentDT.Rows)
                {
                    EN.FastagDetailsEntries DPDTList = new EN.FastagDetailsEntries();
                    DPDTList.SRNo = count++;
                    DPDTList.TransactionDateTime = Convert.ToString(row["Transaction Date Time"]);
                    DPDTList.ProcessedDateTime = Convert.ToString(row["Processed Date Time"]);
                    DPDTList.LicencePlateNo = Convert.ToString(row["Licence Plate No"]);
                    DPDTList.TagAccountNo = Convert.ToString(row["Tag Account No."]);
                    DPDTList.Group = Convert.ToString(row["Group"]);
                    DPDTList.Activity = Convert.ToString(row["Activity"]);
                    DPDTList.PlazaCode = Convert.ToString(row["Plaza Code"]);
                    DPDTList.TransactionDescription = Convert.ToString(row["Transaction Description"]);
                    DPDTList.UniqueTransactionID = Convert.ToString(row["Unique Transaction ID"]);
                    DPDTList.AmountCR = Convert.ToString(row["Amount(CR)"]);
                    DPDTList.AmountDR = Convert.ToString(row["Amount(DR)"]);

                    DriverPaymentList.Add(DPDTList);
                }
            }
            return DriverPaymentList;
        }


        public EN.FastagDetailsEntries SavePaymentList(DataTable Itemdata, int UserID, DateTime entrydate)
        {
            //string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("UserID", UserID);
            parameterList.Add("entryDate", entrydate);
            DataSet ds = new DataSet();

            ds = db.AddTypeRoadPlanningTableData("USP_INSERT_INTO_Fastag_PAYMENT_RECO", parameterList, Itemdata, "PT_FastagSaveDetails", "@PT_FastagSaveDetails");


            //int i = db.SaveContainerList("USP_TainPlanningInsertContainerList", parameterList, Itemdata);

            // VendorDataDL = objgetMyREQDBManager.ApprovedStatusList(OrderID, companyid);

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            EN.FastagDetailsEntries PaymentList = new EN.FastagDetailsEntries();

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

        public string CancelfastagUploadedDetails(DataTable dataTable, int Userid)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            string message = "";

            DataTable VendorDataDL = new DataTable();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("Userid", Userid);

            /*VendorDataDL = trackerdatamanager.AddDirectFuel(containerCount, containerTypeID, TrailerNo, Passing, driverID, TransportorID, FromID, ToID, Activity, ReadingFrom, ReadingTo, fuel, Amount1, Amount2, AdjustFuel, Userid, PlanID, INOUT, ContainerNo, TarrifID, VehTransID, Remarks);*/

            VendorDataDL = db.DataTableAddTypeTable("CancelFastagBillDetails", parameterList, dataTable, "CancelFastagDetails", "@CancelFastagDetails");


            return message;

        }

        public string fatsgDetailsValidation(DataTable PaymentDT)
        {
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";

            if (PaymentDT != null)
            {
                foreach (DataRow row in PaymentDT.Rows)
                {
                    DataTable PortsDS = new DataTable();
                    PortsDS = TrackerManager.CheckDuplicatefastagdetails(Convert.ToString(row["UniqueTransactionID"]));
                    if (PortsDS.Rows.Count > 0)
                    {
                        message = "Following UniqueTransactionID = " + PortsDS.Rows[0].Field<string>("UniqueTransactionID") + " alerady saved for transcation Date again found in database: \n" + Convert.ToString(row["TransactionDateTime"]);
                    }
                    else
                    {
                        message = "";
                    }

                }
                if ((message != ""))
                {
                    message2 = message;
                }
            }
            return message2;
        }

    }
}
