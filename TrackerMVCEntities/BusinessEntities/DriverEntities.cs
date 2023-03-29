using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class DriverEntities
    {
        public string BankID { get; set; }
        public string BankName { get; set; }

        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string AccountName { get; set; }
        public string BranchName { get; set; }
        public string EmailID { get; set; }
        public bool IsActive { get; set; }
        public long DriverID { get; set; }
        public string DriverName { get; set; }
        public string EMAILIDT { get; set; }
        public string ADDRESS { get; set; }
        public string MOBILENO { get; set; }
        public string CONTACTPERSON { get; set; }
        public int TempID { get; set; }
        public string TRailername { get; set; }

        public int TransporterID { get; set; }
        public string PaymentMode { get; set; }

        public string TransporterName { get; set; }


        // Code for new Details

        public string DlNo { get; set; }
        public string DlType { get; set; }
        public string DlDate { get; set; }
        public string DLExpityDate { get; set; }
        public string EquipmentType { get; set; }
        public string DContactNo { get; set; }
        public string InsuranceCompany { get; set; }
        public string Insuranceno { get; set; }
        public string Referenceby { get; set; }
        public string ReferenceContactno { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string chkIsBlackList { get; set; }
        public string TxtRemakrs { get; set; }
        public string Vehicletype { get; set; }
        public string Employetype { get; set; }
        public string AdharNumber { get; set; }
        public string Pannumber { get; set; }
        public string Drivertype { get; set; }
        public string LicenceNo { get; set; }
        public string TrailerSize { get; set; }
        public string TrailerModel { get; set; }
        public int HoldID { get; set; }
        public string HoldDate { get; set; }
        public string DOBDATE { get; set; }
        
        //Code for end new Details
        public DriverEntities()
        {
            BankList = new List<DriverBankEntities>();            
        }
         public List<DriverBankEntities> BankList { get; set; }
       
    }

    public class DriverBankEntities
    {
        public string bankID { get; set; }

        public string bankname { get; set; }
    }
}
