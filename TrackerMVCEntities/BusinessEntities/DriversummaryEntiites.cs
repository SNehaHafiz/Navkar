using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class DriversummaryEntiites
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
        public string ActiveStatus { get; set; }
        public string BlackListedStatus { get; set; }
        public int TransporterID { get; set; }
        public string PaymentMode { get; set; }

        public string TransporterName { get; set; }
        public string DOBDATE { get; set; }

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
        public string driver_vehicletypeID { get; set; }
        public string DLTypeID { get; set; }
        public string EquipmentTypeID { get; set; }
        public string Driver_vehicletypeID { get; set; }
        public string InsuranceCompanyID { get; set; }
        public string Employee_TypeID { get; set; }
        public string AdharNumber { get; set; }
        public string PanNo { get; set; }
        public string Drivertype { get; set; }
        public string drivertypeid { get; set; }
        public string Modifiedby { get; set; }
        public string Modifieddate { get; set; }
        public string Addedon { get; set; }
        public string Addedby { get; set; }
    }
}
