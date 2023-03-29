using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using System.Data;


namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger
{
   public class EDICollections
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.EDISearchGST> GetGSTList(string SearchText)
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetGSTSearch(SearchText);
            List<EN.EDISearchGST> isCHACode = new List<EN.EDISearchGST>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.EDISearchGST oper = new EN.EDISearchGST();
                    oper.GSTIn_uniqID = Convert.ToString(row["GSTIn_uniqID"]);
                    oper.GSTName = Convert.ToString(row["GSTName"]);
                    oper.State = Convert.ToString(row["State"]);
                    oper.GSTAddress = Convert.ToString(row["GSTAddress"]);
                    oper.GSTID = Convert.ToString(row["GSTID"]);
                    oper.state_Code = Convert.ToString(row["state_Code"]);

                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }

        public List<EN.EDICollections> GetSBBOEList(string Category, string SBNo, string BOENo)
        {
            List<EN.EDICollections> SBBOEDL = new List<EN.EDICollections>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetSBBOEData(Category, SBNo, BOENo);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.EDICollections SBBOEList = new EN.EDICollections();

                    SBBOEList.SBDate = Convert.ToString(row["SBDATE"]);
                    SBBOEList.BOEDate = Convert.ToString(row["BOEDATE"]);
                    SBBOEList.CHA = Convert.ToString(row["CHA"]);
                    SBBOEList.GSTIn_uniqID = Convert.ToString(row["GSTIn_uniqID"]);
                    SBBOEList.state_Code = Convert.ToString(row["state_Code"]);
                    SBBOEList.GSTID = Convert.ToString(row["GSTID"]);
                    SBBOEList.pages = Convert.ToString(row["pages"]);

                    SBBOEDL.Add(SBBOEList);
                }
            }
            return SBBOEDL;
        }

        public EN.EDICollections GetExisitinchaname()
        {
            EN.EDICollections objEDICollection = new EN.EDICollections();

            List<EN.CHAEntities> CHADL = new List<EN.CHAEntities>();
            List<EN.ModeEntities> MODEDL = new List<EN.ModeEntities>();
            List<EN.EDIBankEntities> EDIBANKDL = new List<EN.EDIBankEntities>();
            List<EN.ActivityEntities> ACTIVITYDL = new List<EN.ActivityEntities>();
            List<EN.EDIAccountHeads> AccountDL = new List<EN.EDIAccountHeads>();


            DataSet ds = new DataSet();
            ds = trackerdatamanager.GetExisitinchaname();

            if (ds.Tables[0] != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EN.ActivityEntities ActivityList = new EN.ActivityEntities();
                    ActivityList.id = Convert.ToInt16(row["ID"]);
                    ActivityList.Criteria = Convert.ToString(row["Category"]);
                    ACTIVITYDL.Add(ActivityList);
                }
            }
            if (ds.Tables[2] != null)
            {
                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    EN.CHAEntities CHAList = new EN.CHAEntities();
                    CHAList.id = Convert.ToInt32(row["CHAID"]);
                    CHAList.Criteria = Convert.ToString(row["CHANAME"]);
                    CHADL.Add(CHAList);
                }
            }
            if (ds.Tables[1] != null)
            {
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    EN.ModeEntities ModeList = new EN.ModeEntities();
                    ModeList.paymodeID = Convert.ToInt32(row["paymodeID"]);
                    ModeList.paymode = Convert.ToString(row["paymode"]);
                    MODEDL.Add(ModeList);
                }
            }
            if (ds.Tables[3] != null)
            {
                foreach (DataRow row in ds.Tables[3].Rows)
                {
                    EN.EDIBankEntities EDIBankList = new EN.EDIBankEntities();
                    EDIBankList.bankID = Convert.ToString(row["bankID"]);
                    EDIBankList.bankname = Convert.ToString(row["bankname"]);
                    EDIBANKDL.Add(EDIBankList);
                }
            }
            if (ds.Tables[4] != null)
            {
                foreach (DataRow row in ds.Tables[4].Rows)
                {
                    EN.EDIAccountHeads EDIAccountList = new EN.EDIAccountHeads();
                    EDIAccountList.Accountid = Convert.ToInt32(row["Accountid"]);
                    EDIAccountList.Accountname = Convert.ToString(row["Accountname"]);
                    AccountDL.Add(EDIAccountList);
                }
            }
            objEDICollection.ActivityList = ACTIVITYDL;
            objEDICollection.CHAList = CHADL;
            objEDICollection.ModeList = MODEDL;
            objEDICollection.EDIBankList = EDIBANKDL;
            objEDICollection.EDIAccountHeads = AccountDL;

            return objEDICollection;
        }

        public List<EN.EDICollections> GetAmount(int PageFrom, int AccountID, int State)
        {
            List<EN.EDICollections> AmountDL = new List<EN.EDICollections>();
            DataTable AmountDT = new DataTable();
            AmountDT = trackerdatamanager.GetTariffAmount(PageFrom, AccountID, State);

            if (AmountDT != null)
            {
                foreach (DataRow row in AmountDT.Rows)
                {
                    EN.EDICollections Amount = new EN.EDICollections();
                    Amount.NetTotal = Convert.ToString(row["FixedAmount"]);
                    Amount.SGST = Convert.ToString(row["SGST"]);
                    Amount.CGST = Convert.ToString(row["CGST"]);
                    Amount.IGST = Convert.ToString(row["IGST"]);
                    Amount.GrandTotal = Convert.ToString(row["GrandTotal"]);
                    AmountDL.Add(Amount);
                }
            }
            return AmountDL;
        }
        public EN.EDICollections SaveEDI(DataTable Invoicedata, DataTable ImportExportdata, string InvoiceDate, string Activity, string CHAName, int GSTID, string PageFrom, string PageTo, string AccountID, string NetTotal, string SGST, string CGST, string IGST, string GrandTotal, int UserID)
        {

            EN.EDICollections objEDICollection = new EN.EDICollections();
            //string Message = "";
            DataTable dt = new DataTable();

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            
            parameterList.Add("ASSESSDATE", Convert.ToDateTime(InvoiceDate).ToString("yyyy-MM-dd HH:mm"));
            parameterList.Add("ACTIVITY", Activity);
            //parameterList.Add("SBNO", SBNo);
            //parameterList.Add("SBDATE", Convert.ToDateTime(SBDate).ToString("yyyy-MM-dd HH:mm"));
            //parameterList.Add("BOENO", BOENo);
            //parameterList.Add("BOEDATE", Convert.ToDateTime(BOEDate).ToString("yyyy-MM-dd HH:mm"));
            parameterList.Add("CHAID", CHAName);
            parameterList.Add("PARTYID", GSTID);
            parameterList.Add("PAGEFROM", PageFrom);
            parameterList.Add("PAGETO", PageTo);
            parameterList.Add("ACCOUNTID", AccountID);
            parameterList.Add("NETTOTAL", NetTotal);
            parameterList.Add("SGST", SGST);
            parameterList.Add("CGST", CGST);
            parameterList.Add("IGST", IGST);
            parameterList.Add("GRANDTOTAL", GrandTotal);
            parameterList.Add("ADDEDBY", UserID);

            dt = db.DataTableAddTwoTypeTable("USP_INSERT_EDI_ASSESSM", parameterList, Invoicedata, "PT_EDIReceiptDetails", "@PT_EDIReceiptDetails", ImportExportdata, "PT_EDIImportExportDetails", "@PT_EDIImportExportDetails");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    objEDICollection.Message = "1";
                    objEDICollection.AssessNo = Convert.ToInt16(dt.Rows[0]["AssessNo"]);
                    objEDICollection.WorkYear = Convert.ToString(dt.Rows[0]["Workyear"]);
                }
                else
                {
                    objEDICollection.Message = "0";
                    objEDICollection.AssessNo = 0;
                    objEDICollection.WorkYear = "";
                }
            }
            else
            {
                objEDICollection.Message = "0";
                objEDICollection.AssessNo = 0;
                objEDICollection.WorkYear = "";
            }
            
            return objEDICollection;
        }

        public EN.EDICollections GetPrintEDIList(int AssessNo, string WorkYear)
        {
            EN.EDICollections objEDICollection = new EN.EDICollections();

            List<EN.EDIPrintEntities> EDI = new List<EN.EDIPrintEntities>();
            List<EN.EDIImportExportDetsPrint> EDIIMPEXP = new List<EN.EDIImportExportDetsPrint>();

            DataSet ds = new DataSet();
            ds = trackerdatamanager.GetEDIPrintDetails(AssessNo, WorkYear);
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objEDICollection.AssessNo = Convert.ToInt16(ds.Tables[0].Rows[0]["AssessNo"]);
                    objEDICollection.WorkYear = Convert.ToString(ds.Tables[0].Rows[0]["WorkYear"]);
                    objEDICollection.Activity = Convert.ToString(ds.Tables[0].Rows[0]["Activity"]);
                    //objEDICollection.SBNo = Convert.ToString(ds.Tables[0].Rows[0]["SBNo"]);
                    //objEDICollection.SBDate = Convert.ToString(ds.Tables[0].Rows[0]["SBDate"]);
                    //objEDICollection.BOENo = Convert.ToString(ds.Tables[0].Rows[0]["BOENo"]);
                    //objEDICollection.BOEDate = Convert.ToString(ds.Tables[0].Rows[0]["BOEDate"]);
                    objEDICollection.CHA = Convert.ToString(ds.Tables[0].Rows[0]["CHAName"]);
                    objEDICollection.PageFrom = Convert.ToInt16(ds.Tables[0].Rows[0]["PageFrom"]);
                    objEDICollection.PageTo = Convert.ToInt16(ds.Tables[0].Rows[0]["PageTo"]);
                    objEDICollection.AccountName = Convert.ToString(ds.Tables[0].Rows[0]["AccountName"]);
                    objEDICollection.GSTIn_uniqID = Convert.ToString(ds.Tables[0].Rows[0]["GSTIn_uniqID"]);
                    objEDICollection.GSTName = Convert.ToString(ds.Tables[0].Rows[0]["GSTName"]);
                    objEDICollection.State = Convert.ToString(ds.Tables[0].Rows[0]["State"]);
                    objEDICollection.GSTAddress = Convert.ToString(ds.Tables[0].Rows[0]["GSTAddress"]);
                    objEDICollection.NetTotal = Convert.ToString(ds.Tables[0].Rows[0]["NetTotal"]);
                    objEDICollection.SGST = Convert.ToString(ds.Tables[0].Rows[0]["SGST"]);
                    objEDICollection.CGST = Convert.ToString(ds.Tables[0].Rows[0]["CGST"]);
                    objEDICollection.IGST = Convert.ToString(ds.Tables[0].Rows[0]["IGST"]);
                    objEDICollection.GrandTotal = Convert.ToString(ds.Tables[0].Rows[0]["GrandTotal"]);
                    objEDICollection.InvoiceNO = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNO"]);

                    objEDICollection.SignedQRcode = Convert.ToString(ds.Tables[0].Rows[0]["SignedQRcode"]);
                    objEDICollection.AckNo = Convert.ToString(ds.Tables[0].Rows[0]["AckNo"]);
                    objEDICollection.Irn = Convert.ToString(ds.Tables[0].Rows[0]["Irn"]);
                    objEDICollection.AckDt = Convert.ToString(ds.Tables[0].Rows[0]["AckDt"]);
                    objEDICollection.Registered = Convert.ToString(ds.Tables[0].Rows[0]["TyepReg"]);
                }
            }
            if (ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        EN.EDIPrintEntities EDIList = new EN.EDIPrintEntities();
                        EDIList.Mode = Convert.ToString(row["paymode"]);
                        EDIList.ModeAmount = Convert.ToDecimal(row["ModeAmount"]);
                        EDIList.ModeNo = Convert.ToString(row["ModeNo"]);
                        EDIList.BankName = Convert.ToString(row["BankName"]);
                        EDIList.ModeDate = Convert.ToString(row["ModeDate"]);
                        EDI.Add(EDIList);
                    }
                }
            }
            if (ds.Tables[2] != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        EN.EDIImportExportDetsPrint EDIIE = new EN.EDIImportExportDetsPrint();
                        EDIIE.SRNO = Convert.ToString(row["SRNO"]);
                        EDIIE.SBNo = Convert.ToString(row["SBNo"]);
                        EDIIE.SBDate = Convert.ToString(row["SBDate"]);
                        EDIIE.BOENo = Convert.ToString(row["BOENo"]);
                        EDIIE.BOEDate = Convert.ToString(row["BOEDate"]);
                        EDIIMPEXP.Add(EDIIE);
                    }
                }
            }
            if (ds.Tables[3] != null)
            {
                if (ds.Tables[3].Rows.Count > 0)
                {
                    objEDICollection.Con_Name = Convert.ToString(ds.Tables[3].Rows[0]["Con_Name"]);
                    objEDICollection.AddressI = Convert.ToString(ds.Tables[3].Rows[0]["AddressI"]);
                    objEDICollection.GSTIN = Convert.ToString(ds.Tables[3].Rows[0]["GSTIN"]);
                    objEDICollection.NoteI = Convert.ToString(ds.Tables[3].Rows[0]["NoteI"]);
                    objEDICollection.NoteII = Convert.ToString(ds.Tables[3].Rows[0]["NoteII"]);
                    objEDICollection.NoteIII = Convert.ToString(ds.Tables[3].Rows[0]["NoteIII"]);
                    objEDICollection.NoteVI = Convert.ToString(ds.Tables[3].Rows[0]["NoteVI"]);
                    objEDICollection.PANNo = Convert.ToString(ds.Tables[3].Rows[0]["PANNo"]);
                    objEDICollection.ConFor = Convert.ToString(ds.Tables[3].Rows[0]["Con_For"]);
                    objEDICollection.UPINumber = Convert.ToString(ds.Tables[3].Rows[0]["UPINumber"]);
                }
            }
            objEDICollection.EDIPrintEntities = EDI;
            objEDICollection.EDIImportExportDetsPrint = EDIIMPEXP;
            return objEDICollection;
        }
        public List<EN.EDICollections> GetEDISummary(string FromDate, string ToDate, string category, string Search1, string Search)
        {
            List<EN.EDICollections> AmountDL = new List<EN.EDICollections>();
            DataTable AmountDT = new DataTable();
            AmountDT = trackerdatamanager.GetEDISummaryData(FromDate, ToDate, category, Search1, Search);
            if (AmountDT != null)
            {
                if (AmountDT.Rows.Count > 0)
                {
                    foreach (DataRow row in AmountDT.Rows)
                    {
                        EN.EDICollections Amount = new EN.EDICollections();
                        Amount.AssessNo = Convert.ToInt16(row["AssessNo"]);
                        Amount.WorkYear = Convert.ToString(row["WorkYear"]);
                        Amount.AssessDate = Convert.ToString(row["AssessDate"]);
                        Amount.Activity = Convert.ToString(row["Activity"]);
                        Amount.SBNo = Convert.ToString(row["SBNo"]);
                        //Amount.SBDate = Convert.ToString(row["SBDate"]);
                        Amount.BOENo = Convert.ToString(row["BOENo"]);
                        //Amount.BOEDate = Convert.ToString(row["BOEDate"]);
                        Amount.CHA = Convert.ToString(row["CHAName"]);
                        Amount.PageFrom = Convert.ToInt16(row["PageFrom"]);
                        //Amount.PageTo = Convert.ToInt16(row["PageTo"]);
                        Amount.AccountName = Convert.ToString(row["AccountName"]);
                        Amount.GSTName = Convert.ToString(row["GSTName"]);
                        Amount.NetTotal = Convert.ToString(row["NetTotal"]);
                        Amount.SGST = Convert.ToString(row["SGST"]);
                        Amount.CGST = Convert.ToString(row["CGST"]);
                        Amount.IGST = Convert.ToString(row["IGST"]);
                        Amount.GrandTotal = Convert.ToString(row["GrandTotal"]);
                        Amount.Edit = Convert.ToString(row["Edit"]);
                        AmountDL.Add(Amount);
                    }
                }
            }
            return AmountDL;
        }
    }
}
