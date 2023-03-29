using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class ExpHeadMasterEnt
    {
        public int EntryID { get; set; }
        public DateTime EntryDate { get; set; }
        public string HeadName { get; set; }
        public int HSNCode { get; set; }
        public Boolean IsActive { get; set; }
        public string ExpType { get; set; }
        public int TaxID { get; set; }
        public string TaxName { get; set; }
        public string PaymodeId { get; set; }
        public string Paymode { get; set; }
        public double TotalAmount { get; set; }
        public int sgst { get; set; }
        public int cgst { get; set; }
        public int igst { get; set; }
        public int ExpGroupID { get; set; }
        public string ExpGroupName { get; set; }
        public int SubExpID { get; set; }
        public string SubExpHead { get; set; }
        public int ExpTypeID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ExpensesHead { get; set; }
        public string Description { get; set; }
        public string Netamount { get; set; }
        public int CGST1 { get; set; }
        public int SGST1 { get; set; }
        public int IGST1 { get; set; }
        public string HSNCode1 { get; set; }
        public string Grandtotal { get; set; }
        public string VoucherNo { get; set; }
        public int EntryTypeID { get; set; }
        public string EntryType { get; set; }
        public string Group_Name { get; set; }
        public string Sub_Exp_Head { get; set; }
        public string ExpensesHeadID { get; set; }
        public string DEbitRefernceno { get; set; }
        public int SIGST1 { get; set; }
        public string TaxName1 { get; set; }

        


        public string netDiscount { get; set; }
        public string netDiscountS { get; set; }
        public string ddlparTDS { get; set; }
        public string tdsAmount { get; set; }
        public string GSTNo { get; set; }
        public int BankID { get; set; }
        public string BankName { get; set; }

        public int InvTId { get; set; }
        public string InvType { get; set; }
        public string HSNID { get; set; }
        public string HSNCodeL { get; set; }
        public int IMPGID { get; set; }
        public string IMPGName { get; set; }
        public int disp { get; set; }
        public Boolean isdpd { get; set; }
        public string chargefors { get; set; }
        public string AcName { get; set; }
        public string TallyName { get; set; }

    }
    public class CityMaster : ExpHeadMasterEnt
    {

        public int StateID { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string PinCode { get; set; }

    }


    public class LockData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }

        public int Locked_ID { get; set; }
        public string Locked_on { get; set; }
        public string Locked_Till_Date { get; set; }
        public string Activity { get; set; }
        public string Remarks { get; set; }

    }


    public class TCSAmountMod
    {
        public float GrandTotal { get; set; }
        public float tcsamt { get; set; }
        public string AssessDate { get; set; }


    }


    public class ActivityMasters
    {
        public int EntryID { get; set; }
        public string ActivityName { get; set; }
        public int TimeType { get; set; }
        public int MaxAllow { get; set; }
        public int Max { get; set; }
        public bool LR { get; set; }
        public bool IO { get; set; }
        public string ActivityStatus { get; set; }
        public string FG { get; set; }
        public string Cycle { get; set; }



        public bool ISIN { get; set; }
        public bool ISOUT { get; set; }
        public bool KMIN { get; set; }
        public bool KMOUT { get; set; }
        public bool IsActive { get; set; }


        
    }
}
