using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.VehicleDetails
{
    public class VehicleDetails
    {
        DB.TrackerMVCDBManager transportdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.VehicleDetails> getVehicleList(DateTime FromDate, DateTime ToDate, string searchText)
        {
            string message;
            int i = 0;
            DataTable dt = new DataTable();
            List<EN.VehicleDetails>VehicleDL = new List<EN.VehicleDetails>();
            dt = transportdatamanager.GetVehicleList(FromDate, ToDate,searchText);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.VehicleDetails VehicleList = new EN.VehicleDetails();
                    i++;
                    VehicleList.SrNo = i ;
                    VehicleList.VehicleNo = Convert.ToString(row["Trailer No"]);
                    VehicleList.Transporter = Convert.ToString(row["fuelshortcode"]);
                    VehicleList.TotalTeus = Convert.ToString(row["Size"]);
                    VehicleList.Activity = Convert.ToString(row["Activity"]);
                    VehicleList.Customer = Convert.ToString(row["Customer Name"]);
                    VehicleList.Destination = Convert.ToString(row["To Location"]);
                    VehicleList.Fuel = Convert.ToString(row["Fuel"]);
                    VehicleList.Amount = Convert.ToString(row["Amount Bank"]);
                    VehicleList.FitnessUpto = Convert.ToString(row["Fitness UpTo"]);
                    VehicleList.InsuranceUpto = Convert.ToString(row["Insurance Upto"]);
                    VehicleList.TaxUpto = Convert.ToString(row["Tax Upto"]);

                    VehicleDL.Add(VehicleList);
                }
            }

            return VehicleDL;
        }


        public List<EN.LocationWisePerTeusBillingEntities> getLocationBilling(string FromDate, string ToDate, int Locationid)
        {
            
            int i = 1;
            DataTable dt = new DataTable();
            List<EN.LocationWisePerTeusBillingEntities> VehicleDL = new List<EN.LocationWisePerTeusBillingEntities>();
            dt = transportdatamanager.GetLOcationBilling(FromDate, ToDate, Locationid);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationWisePerTeusBillingEntities VehicleList = new EN.LocationWisePerTeusBillingEntities();
                
                    VehicleList.SrNo = Convert.ToString(i++); 
                    VehicleList.MovementDAte = Convert.ToString(row["addedon"]);
                    VehicleList.ContainerNo = Convert.ToString(row["containerno"]);
                    VehicleList.Size = Convert.ToString(row["size"]);
                    VehicleList.PartyName = Convert.ToString(row["PartyName"]);
                    VehicleList.BillingamOUNT = Convert.ToString(row["BillingAMount"]);
                    VehicleList.tRAILERNO = Convert.ToString(row["trailername"]);
                 

                    VehicleDL.Add(VehicleList);
                }
            }

            return VehicleDL;
        }

        // Insurance Details
    
        public List<EN.InsuranceandTrackeringEntiites> GetInsuranceDetails(string Trailername)
        {

            int i = 1;
            DataTable dt = new DataTable();
            List<EN.InsuranceandTrackeringEntiites> VehicleDLList = new List<EN.InsuranceandTrackeringEntiites>();
            dt = transportdatamanager.GetInsuranceVehicleFecth(Trailername);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.InsuranceandTrackeringEntiites VehicleDL = new EN.InsuranceandTrackeringEntiites();
                    VehicleDL.sRNO = Convert.ToString(i++);
                    VehicleDL.tRAILERNAME = Convert.ToString(row["Vehicleno"]);
                    VehicleDL.Regdate = Convert.ToString(row["regdate"]);
                    VehicleDL.regno = Convert.ToString(row["regno"]);
                    VehicleDL.yearofmanufacture = Convert.ToString(row["yearofmanufacture"]);
                    VehicleDL.makeby = Convert.ToString(row["makeby"]);
                    VehicleDL.regname = Convert.ToString(row["regname"]);
                  
                    
                    VehicleDLList.Add(VehicleDL);


                }
            }

            return VehicleDLList;
        }

        // COde for drop Permit dropdown

        public List<EN.PermitTypeEntities> GetPermitTYpe()
        {
            DataTable dt = new DataTable();
            List<EN.PermitTypeEntities> PermittypeDL = new List<EN.PermitTypeEntities>();
            dt = transportdatamanager.GetPermitType();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.PermitTypeEntities PermitList = new EN.PermitTypeEntities();
                    PermitList.PermitID = Convert.ToInt16(row["PermitID"]);
                    PermitList.PermitType = Convert.ToString(row["PermitType"]);
                    PermittypeDL.Add(PermitList);
                }
            }

            return PermittypeDL;
        }

        public List<EN.TaxTypeEntities> GetTaxType()
        {
            DataTable dt = new DataTable();
            List<EN.TaxTypeEntities> TaxDL = new List<EN.TaxTypeEntities>();
            dt = transportdatamanager.GetTaxType();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TaxTypeEntities TaxType = new EN.TaxTypeEntities();
                    TaxType.Taxid = Convert.ToInt16(row["TaxID"]);
                    TaxType.TaxType = Convert.ToString(row["TaxType"]);
                    TaxDL.Add(TaxType);
                }
            }

            return TaxDL;
        }

        // Code End By Prashant

        public string SaveInsuranceDate(DataTable RTODetails, string TrailerName, string RegNo, string Regdate,
            string RegOwner,
            string VehicleType, string yearofmanufacture, string Make, string ChasisNo, string ENgineNo, string GrossWeight,
            string Model, string InVoiceAmount, string RCBookField,int userId, string PolicyType, string PolicyNumber, string InsuranceCompany,
            string PremiumAmount, string SumOFinsured, string PolicyDate, string ValidUpto, string PaymentDate, string bankname,
            string ENdDate, string EmiAMount, string TaxType, string PermitType, string PermitNo,string RGno)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Vehicle", TrailerName);
            parameterList.Add("RegNo", RegNo);
            parameterList.Add("RegDate", Convert.ToDateTime(Regdate).ToString("yyyy-MM-dd hh:mm"));
            parameterList.Add("RegName", RegOwner);
            parameterList.Add("vehicletype", VehicleType);
           
            parameterList.Add("ManufactureYear", Convert.ToDateTime(yearofmanufacture).ToString("yyyy-MM-dd hh:mm"));
            parameterList.Add("MakeBy", Make);
            parameterList.Add("Chasis", ChasisNo);
            parameterList.Add("Engine", ENgineNo);
            parameterList.Add("grosstype", GrossWeight);
            parameterList.Add("model", Model);
            parameterList.Add("invoicemount", InVoiceAmount);
            parameterList.Add("IsActive", RCBookField);
            parameterList.Add("AddedBy", userId);
            parameterList.Add("PolicyType", PolicyType);
            parameterList.Add("PolicyNo", PolicyNumber);
            parameterList.Add("PolicyDate", Convert.ToDateTime(PolicyDate).ToString("yyyy-MM-dd hh:mm"));
            parameterList.Add("Company", InsuranceCompany);
           
            parameterList.Add("ValidUpto", Convert.ToDateTime(ValidUpto).ToString("yyyy-MM-dd hh:mm"));
            parameterList.Add("PaymentAmt", PremiumAmount);
            parameterList.Add("PaymentDate", Convert.ToDateTime(ENdDate).ToString("yyyy-MM-dd hh:mm"));
            parameterList.Add("SumInsured", SumOFinsured);
            parameterList.Add("TaxType", TaxType);
            parameterList.Add("PermitType", PermitType);
            parameterList.Add("PermitNo", PermitNo);
            parameterList.Add("RevNo", RGno);
           
           
            
            parameterList.Add("TakenFrom", bankname);
            parameterList.Add("EndDate", ENdDate);
            parameterList.Add("EMIAmt", EmiAMount);

            int i = db.AddTypeTableData("SP_SaveInsurance_And_RTO_TrackingDetails", parameterList, RTODetails, "PT_AddRTODetailList", "@PT_AddRTODetailList");


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


        public List<EN.ModelEntities> GetModelList()
        {
            DataTable dt = new DataTable();
            List<EN.ModelEntities> ModelDL = new List<EN.ModelEntities>();
            dt = transportdatamanager.GetModelList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ModelEntities TaxType = new EN.ModelEntities();
                    TaxType.ModelID = Convert.ToInt16(row["ModelID"]);
                    TaxType.ModelName = Convert.ToString(row["modelname"]);
                    ModelDL.Add(TaxType);
                }
            }

            return ModelDL;
        }

        public List<EN.InsuranceCompanyEntities> GetInsuranceCompanyList()
        {
            DataTable dt = new DataTable();
            List<EN.InsuranceCompanyEntities> InsuranceDL = new List<EN.InsuranceCompanyEntities>();
            dt = transportdatamanager.GetInsuranceCompanyList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.InsuranceCompanyEntities TaxType = new EN.InsuranceCompanyEntities();
                    TaxType.InsurancID = Convert.ToInt16(row["InsuranceID"]);
                    TaxType.InsuranceCompany = Convert.ToString(row["Insurancecompany"]);
                    InsuranceDL.Add(TaxType);
                }
            }

            return InsuranceDL;
        }

        public List<EN.MakeEntities> GetMakeList()
        {
            DataTable dt = new DataTable();
            List<EN.MakeEntities> MakeDL = new List<EN.MakeEntities>();
            dt = transportdatamanager.GetMakeList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.MakeEntities TaxType = new EN.MakeEntities();
                    TaxType.MakeID = Convert.ToInt16(row["MakeID"]);
                    TaxType.MakeName = Convert.ToString(row["make"]);
                    MakeDL.Add(TaxType);
                }
            }

            return MakeDL;
        }


        public EN.InsuranceAndRTOTracking GetTrailerWiseInsuradDetails(string Trailername)
        {

            int i = 1;
            DataTable dt = new DataTable();
            EN.InsuranceAndRTOTracking VehicleDLList = new EN.InsuranceAndRTOTracking();
            dt = transportdatamanager.GetInsuranceModuleDetails(Trailername);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {


                    VehicleDLList.tRAILERNAME = Convert.ToString(row["VehicleNo"]);
                    VehicleDLList.vehicletype = Convert.ToString(row["vehicletype"]);
                    VehicleDLList.modelname = Convert.ToString(row["modelid"]);
                    VehicleDLList.chasisno = Convert.ToString(row["chasisno"]);
                    VehicleDLList.engineno = Convert.ToString(row["engineno"]);
                    VehicleDLList.rcbookfiled = Convert.ToString(row["isactive"]);
                    VehicleDLList.regno = Convert.ToString(row["RegNo"]);
                    VehicleDLList.yearofmanufacture = Convert.ToString(row["yearofmanufacture"]);
                    VehicleDLList.makeby = Convert.ToString(row["makeby"]);
                    VehicleDLList.TakenFrom = Convert.ToString(row["TakenFrom"]);
                    VehicleDLList.EndDate = Convert.ToString(row["enddate"]);
                    VehicleDLList.EMIAmt = Convert.ToString(row["emiamt"]);
                    VehicleDLList.PolicyType = Convert.ToString(row["PolicyType"]);
                    VehicleDLList.PolicyNo = Convert.ToString(row["PolicyNo"]);
                    VehicleDLList.PolicyDate = Convert.ToString(row["PolicyDate"]);
                    VehicleDLList.InsuranceCompany = Convert.ToString(row["InsuranceCompany"]);
                    VehicleDLList.PaymentAmt = Convert.ToString(row["PaymentAmt"]);
                    VehicleDLList.PaymentDate = Convert.ToString(row["PaymentDate"]);
                    VehicleDLList.SumInsured = Convert.ToString(row["suminsured"]);
                   VehicleDLList.Regdate = Convert.ToString(row["RegDate"]);
                    VehicleDLList.regname = Convert.ToString(row["regname"]);
                    VehicleDLList.ValidUpTo = Convert.ToString(row["ValidUpTo"]);
                    VehicleDLList.GrossWeight = Convert.ToString(row["grossweight"]);
                    VehicleDLList.InVoiceAmount = Convert.ToString(row["Invoiceamount"]);
                    VehicleDLList.Isactive = Convert.ToString(row["isactive"]);
               

                }
            }

            return VehicleDLList;
        }

        public List<EN.RTODetails> GetTrailerWiseRtoDetails(string Trailername)
        {
            DataTable dt = new DataTable();
            List<EN.RTODetails> MakeDL = new List<EN.RTODetails>();
            dt = transportdatamanager.RtoDetails(Trailername);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.RTODetails TaxType = new EN.RTODetails();
                  
                    TaxType.rtotax = Convert.ToString(row["rtotax"]);
                    TaxType.fromdate = Convert.ToString(row["fromdate"]);
                    TaxType.todate = Convert.ToString(row["todate"]);
                    TaxType.taxamt = Convert.ToString(row["taxamt"]);
                    TaxType.permitno = Convert.ToString(row["permitno"]);
                    TaxType.permittype = Convert.ToString(row["permittype"]);
                    TaxType.Vehicleno = Convert.ToString(row["Vehicleno"]);
                    TaxType.TaxType = Convert.ToString(row["taxtype"]);
                    MakeDL.Add(TaxType);
                }
            }

            return MakeDL;
        }



        public string RenevalRTODetails(DataTable RTODetails, string TrailerName, string RegNo, string Regdate,
           string RegOwner,
           string VehicleType, string yearofmanufacture, string Make, string ChasisNo, string ENgineNo, string GrossWeight,
           string Model, string InVoiceAmount, string RCBookField, int userId, string PolicyType, string PolicyNumber, string InsuranceCompany,
           string PremiumAmount, string SumOFinsured, string PolicyDate, string ValidUpto, string PaymentDate, string bankname,
           string ENdDate, string EmiAMount, string TaxType, string PermitType, string PermitNo, string RGno)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Vehicle", TrailerName);
            parameterList.Add("RegNo", RegNo);
            parameterList.Add("RegDate", Convert.ToDateTime(Regdate).ToString("yyyy-MM-dd hh:mm"));
            parameterList.Add("RegName", RegOwner);
            parameterList.Add("vehicletype", VehicleType);

            parameterList.Add("ManufactureYear", Convert.ToDateTime(yearofmanufacture).ToString("yyyy-MM-dd hh:mm"));
            parameterList.Add("MakeBy", Make);
            parameterList.Add("Chasis", ChasisNo);
            parameterList.Add("Engine", ENgineNo);
            parameterList.Add("grosstype", GrossWeight);
            parameterList.Add("model", Model);
            parameterList.Add("invoicemount", InVoiceAmount);
            parameterList.Add("IsActive", RCBookField);
            parameterList.Add("AddedBy", userId);
            parameterList.Add("PolicyType", PolicyType);
            parameterList.Add("PolicyNo", PolicyNumber);
            parameterList.Add("PolicyDate", Convert.ToDateTime(PolicyDate).ToString("yyyy-MM-dd hh:mm"));
            parameterList.Add("Company", InsuranceCompany);

            parameterList.Add("ValidUpto", Convert.ToDateTime(ValidUpto).ToString("yyyy-MM-dd hh:mm"));
            parameterList.Add("PaymentAmt", PremiumAmount);
            parameterList.Add("PaymentDate", Convert.ToDateTime(ENdDate).ToString("yyyy-MM-dd hh:mm"));
            parameterList.Add("SumInsured", SumOFinsured);
            parameterList.Add("TaxType", TaxType);
            parameterList.Add("PermitType", PermitType);
            parameterList.Add("PermitNo", PermitNo);
            parameterList.Add("RevNo", RGno);



            parameterList.Add("TakenFrom", bankname);
            parameterList.Add("EndDate", ENdDate);
            parameterList.Add("EMIAmt", EmiAMount);

            int i = db.AddTypeTableData("SP_RenevalInsurance_And_RTO_TrackingDetails", parameterList, RTODetails, "PT_AddRTODetailList", "@PT_AddRTODetailList");


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

        //Code For getVehicleTYpe

        public List<EN.TrailersEntities> getVehicleDocType(string VehicleTypeid)
        {
            List<EN.TrailersEntities> PortsDL = new List<EN.TrailersEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = transportdatamanager.GetVehicleDocumentTypeDetails(VehicleTypeid);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TrailersEntities PortsList = new EN.TrailersEntities();
                    PortsList.trailerid = Convert.ToInt16(row["VehicleDociD"]);
                    PortsList.trailername = Convert.ToString(row["VehicleDoctype"]);

                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }


        public List<EN.DriverAttachment> GetTrailerdocView(string Trailername)
        {
            List<EN.DriverAttachment> AttachmentList = new List<EN.DriverAttachment>();
            DataTable AttachmentDT = new DataTable();
            int i = 0;
            AttachmentDT = transportdatamanager.GetVehicleListDocumentDetails(Trailername);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {
                    EN.DriverAttachment AttachmentDataList = new EN.DriverAttachment();
                    i++;
                    AttachmentDataList.SrNo = i;
                    AttachmentDataList.DocID = Convert.ToInt32(row["DocID"]);
                    // AttachmentDataList.DocumentType = Convert.ToString(row["DocumentType"]);
                    AttachmentDataList.FilePath = Convert.ToString(row["VehicleDoctype"]);


                    AttachmentList.Add(AttachmentDataList);
                }
            }
            return AttachmentList;
        }

        public EN.DriverAttachment GetVehicleDocumentDownloadList(int id, string id1)
        {
            EN.DriverAttachment AttachmentList = new EN.DriverAttachment();
            DataTable AttachmentDT = new DataTable();
            int i = 0;
            AttachmentDT = transportdatamanager.GetVehicleDownloadList(id, id1);
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
    }
}
