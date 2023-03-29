using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
  public  class EDICollections
    {
        public string GSTIn_uniqID { get; set; }
        public string GSTName { get; set; }
        public string State { get; set; }
        public string GSTAddress { get; set; }
        public string GSTID { get; set; }
        public string state_Code { get; set; }
        public string NetTotal { get; set; }
        public string SGST { get; set; }
        public string CGST { get; set; }
        public string IGST { get; set; }
        public string GrandTotal { get; set; }
        public string Edit { get; set; }

        public int ModeID { get; set; }
        public float ModeAmount { get; set; }
        public string ModeNo { get; set; }
        public string BankName { get; set; }
        public DateTime ModeDate { get; set; }
        public string Mode { get; set; }

        public int AssessNo { get; set; }
        public string InvoiceNO { get; set; }
        public string WorkYear { get; set; }
        public string Activity { get; set; }
        public string SBNo { get; set; }
        public string SBDate { get; set; }
        public string BOENo { get; set; }
        public string BOEDate { get; set; }
        public string CHA { get; set; }
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        public string AccountName { get; set; }
        public string Message { get; set; }
        public string AssessDate { get; set; }
        public string Con_Name { get; set; }
        public string AddressI { get; set; }
        public string GSTIN { get; set; }
        public string pages { get; set; }
        public string NoteI { get; set; }
        public string NoteII { get; set; }
        public string NoteIII { get; set; }
        public string NoteVI { get; set; }
        public string PANNo { get; set; }
        public string ConFor { get; set; }
        public string SignedQRcode { get; set; }
        public string AckNo { get; set; }
        public string Irn { get; set; }
        public string AckDt { get; set; }
        public string Registered { get; set; }
        public string UPINumber { get; set; }
        public EDICollections()
        {
            ActivityList = new List<ActivityEntities>();
            CHAList = new List<CHAEntities>();
            ModeList = new List<ModeEntities>();
            EDIBankList = new List<EDIBankEntities>();
            EDIAccountHeads = new List<EDIAccountHeads>();
            EDIPrintEntities = new List<EDIPrintEntities>();
            EDIImportExportDets = new List<EDIImportExportDets>();
            EDIImportExportDetsPrint = new List<EDIImportExportDetsPrint>();
        }
        public List<ActivityEntities> ActivityList { get; set; }
        public List<CHAEntities> CHAList { get; set; }
        public List<ModeEntities> ModeList { get; set; }
        public List<EDIBankEntities> EDIBankList { get; set; }
        public List<EDIAccountHeads> EDIAccountHeads { get; set; }
        public List<EDIPrintEntities> EDIPrintEntities { get; set; }
        public List<EDIImportExportDets> EDIImportExportDets { get; set; }
        public List<EDIImportExportDetsPrint> EDIImportExportDetsPrint { get; set; }
    }
    public class EDIImportExportDets
    {
        public string SRNO { get; set; }
        public string SBNo { get; set; }
        public DateTime SBDate { get; set; }
        public string BOENo { get; set; }
        public DateTime BOEDate { get; set; }
    }
    public class ActivityEntities
    {
        public int id { get; set; }
        public string Criteria { get; set; }
        
    }

    public class CHAEntities
    {
        public int id { get; set; }
        public string Criteria { get; set; }

    }
    public class ModeEntities
    {
        public int paymodeID { get; set; }
        public string paymode { get; set; }

    }
    public class EDIBankEntities
    {
        public string bankID { get; set; }
        public string bankname { get; set; }

    }
    public class EDIAccountHeads
    {
        public int Accountid { get; set; }
        public string Accountname { get; set; }

    }

    public class EDIPrintEntities
    {
        public decimal ModeAmount { get; set; }
        public string ModeNo { get; set; }
        public string BankName { get; set; }
        public string ModeDate { get; set; }
        public string Mode { get; set; }
    }
    public class EDISearchGST
    {
        public string GSTID { get; set; }
        public string GSTIn_uniqID { get; set; }
        public string state_Code { get; set; }
        public string GSTName { get; set; }
        public string GSTAddress { get; set; }
        public string State { get; set; }
    }
    public class EDIImportExportDetsPrint
    {
        public string SRNO { get; set; }
        public string SBNo { get; set; }
        public string SBDate { get; set; }
        public string BOENo { get; set; }
        public string BOEDate { get; set; }
    }
}
