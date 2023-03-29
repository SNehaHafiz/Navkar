using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.BLDataManager
{
    
    public class GenerateBL
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();

        public List<EN.Shippers> getShippers()
        {
            List<EN.Shippers> ShippersDL = new List<EN.Shippers>();
            DataTable ShippersDT = new DataTable();
            string Table = "exp_shippers";
            string Column = "shipperID,shippername";
            string Condition = "IsActive=1";
            string OrderBy = "shippername";

            ShippersDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ShippersDT != null)
            {
                foreach (DataRow row in ShippersDT.Rows)
                {
                    EN.Shippers ShippersList = new EN.Shippers();
                    ShippersList.shipperID = Convert.ToInt32(row["shipperID"]);
                    ShippersList.shippername = Convert.ToString(row["shippername"]);

                    ShippersDL.Add(ShippersList);
                }
            }
            return ShippersDL;
        }

        public List<EN.ShipLines> getShipLines()
        {
            List<EN.ShipLines> ShipLinesDL = new List<EN.ShipLines>();
            DataTable ShipLinesDT = new DataTable();
            string Table = "ShipLines";
            string Column = "SLID,SLName,SaID";
            string Condition = "SLIsActive=1";
            string OrderBy = "SLName";

            ShipLinesDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ShipLinesDT != null)
            {
                foreach (DataRow row in ShipLinesDT.Rows)
                {
                    EN.ShipLines ShipLinesList = new EN.ShipLines();
                    ShipLinesList.SLID = Convert.ToInt32(row["SLID"]);
                    ShipLinesList.SLName = Convert.ToString(row["SLName"]);
                    ShipLinesList.SLCode = Convert.ToString(row["SaID"]);

                    ShipLinesDL.Add(ShipLinesList);
                }
            }
            return ShipLinesDL;
        }
        public List<EN.Customer> getCustomer()
        {
            List<EN.Customer> CustomerDL = new List<EN.Customer>();
            DataTable CustomerDT = new DataTable();
            string Table = "Customer";
            string Column = "AGID,AGName";
            string Condition = "IsActive=1";
            string OrderBy = "AGName";

            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.Customer CustomerList = new EN.Customer();
                    CustomerList.AGID = Convert.ToInt32(row["AGID"]);
                    CustomerList.AGName = Convert.ToString(row["AGName"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }

        public List<EN.Customer> getParty()
        {
            List<EN.Customer> CustomerDL = new List<EN.Customer>();
            DataTable CustomerDT = new DataTable();
            string Table = "Party_GST_M";
            string Column = "isnull(Common_ID,0)Common_ID,GSTName";
            string Condition = "";
            string OrderBy = "GSTName";

            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.Customer CustomerList = new EN.Customer();
                    CustomerList.AGID = Convert.ToInt32(row["Common_ID"]);
                    CustomerList.AGName = Convert.ToString(row["GSTName"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }

        public List<EN.Category> getCategory()
        {
            List<EN.Category> CustomerDL = new List<EN.Category>();
            DataTable CustomerDT = new DataTable();
            string Table = "Category";
            string Column = "ID,Category";
            string Condition = "";
            string OrderBy = "Category";

            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.Category CategoryList = new EN.Category();
                    CategoryList.ID = Convert.ToInt32(row["ID"]);
                    CategoryList.CategoryName = Convert.ToString(row["Category"]);

                    CustomerDL.Add(CategoryList);
                }
            }
            return CustomerDL;
        }

        public List<EN.Consignee> getConsignee()
        {
            List<EN.Consignee> ConsigneeDL = new List<EN.Consignee>();
            DataTable ConsigneeDT = new DataTable();
            string Table = "Importer";
            string Column = "ImporterID,ImporterName";
            string Condition = "IsActive=1";
            string OrderBy = "ImporterName";

            ConsigneeDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ConsigneeDT != null)
            {
                foreach (DataRow row in ConsigneeDT.Rows)
                {
                    EN.Consignee ConsigneeList = new EN.Consignee();
                    ConsigneeList.ImporterID = Convert.ToInt32(row["ImporterID"]);
                    ConsigneeList.ImporterName = Convert.ToString(row["ImporterName"]);

                    ConsigneeDL.Add(ConsigneeList);
                }
            }
            return ConsigneeDL;
        }

        public List<EN.CHA> getCHA()
        {
            List<EN.CHA> ChaDL = new List<EN.CHA>();
            DataTable ChaDT = new DataTable();
            string Table = "CHA";
            string Column = "CHAID,CHAName";
            string Condition = "IsActive=1";
            string OrderBy = "CHAName";

            ChaDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ChaDT != null)
            {
                foreach (DataRow row in ChaDT.Rows)
                {
                    EN.CHA ChaList = new EN.CHA();
                    ChaList.CHANo = Convert.ToInt64(row["CHAID"]);
                    ChaList.CHAName = Convert.ToString(row["CHAName"]);

                    ChaDL.Add(ChaList);
                }
            }
            return ChaDL;
        }
        public List<EN.Vessel> getVessel()
        {
            List<EN.Vessel> VesselDL = new List<EN.Vessel>();
            DataTable VesselDT = new DataTable();
            string Table = "Vessels";
            string Column = "VesselID,VesselName";
            string Condition = "IsActive=1";
            string OrderBy = "VesselName";

            VesselDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (VesselDT != null)
            {
                foreach (DataRow row in VesselDT.Rows)
                {
                    EN.Vessel VesselList = new EN.Vessel();
                    VesselList.VesselID = Convert.ToInt32(row["VesselID"]);
                    VesselList.VesselName = Convert.ToString(row["VesselName"]);

                    VesselDL.Add(VesselList);
                }
            }
            return VesselDL;
        }

        public List<EN.Ports> getPorts()
        {
            List<EN.Ports> PortsDL = new List<EN.Ports>();
            DataTable PortsDT = new DataTable();
            string Table = "Ports";
            string Column = "PortID,PortName";
            string Condition = "";
            string OrderBy = "PortName";

            PortsDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.Ports PortsList = new EN.Ports();
                    PortsList.PortID = Convert.ToInt32(row["PortID"]);
                    PortsList.PortName = Convert.ToString(row["PortName"]);

                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }
        public List<EN.Haulage> getHaulage()
        {
            List<EN.Haulage> HaulageDL = new List<EN.Haulage>();
            DataTable HaulageDT = new DataTable();
            string Table = "Haulage_Type_M";
            string Column = "Haulage_Type_ID,Haulage_Type";
            string Condition = "";
            string OrderBy = "Haulage_Type";

            HaulageDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (HaulageDT != null)
            {
                foreach (DataRow row in HaulageDT.Rows)
                {
                    EN.Haulage HaulageList = new EN.Haulage();
                    HaulageList.Haulage_Type_ID = Convert.ToInt32(row["Haulage_Type_ID"]);
                    HaulageList.Haulage_Type = Convert.ToString(row["Haulage_Type"]);

                    HaulageDL.Add(HaulageList);
                }
            }
            return HaulageDL;
        }

        public List<EN.IGMFileStatus> getIGMFileStatus()
        {
            List<EN.IGMFileStatus> IGMFileStatusDL = new List<EN.IGMFileStatus>();
            DataTable IGMFileStatusDT = new DataTable();
            string Table = "IGM_File_Status_M";
            string Column = "File_Status_ID,File_Status";
            string Condition = "";
            string OrderBy = "File_Status";

            IGMFileStatusDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (IGMFileStatusDT != null)
            {
                foreach (DataRow row in IGMFileStatusDT.Rows)
                {
                    EN.IGMFileStatus IGMFileStatusList = new EN.IGMFileStatus();
                    IGMFileStatusList.File_Status_ID = Convert.ToInt32(row["File_Status_ID"]);
                    IGMFileStatusList.File_Status = Convert.ToString(row["File_Status"]);
                    IGMFileStatusDL.Add(IGMFileStatusList);
                }
            }
            return IGMFileStatusDL;
        }

        public List<EN.Transport> getTransport()
        {
            List<EN.Transport> TransportDL = new List<EN.Transport>();
            DataTable TransportDT = new DataTable();
            string Table = "Transport_Type_M";
            string Column = "Transport_Type_ID,Transport_Type";
            string Condition = "";
            string OrderBy = "Transport_Type";

            TransportDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TransportDT != null)
            {
                foreach (DataRow row in TransportDT.Rows)
                {
                    EN.Transport TransportList = new EN.Transport();
                    TransportList.Transport_Type_ID = Convert.ToInt32(row["Transport_Type_ID"]);
                    TransportList.Transport_Type = Convert.ToString(row["Transport_Type"]);
                    TransportDL.Add(TransportList);
                }
            }
            return TransportDL;
        }
        
        public List<EN.POL> getPOL()
        {
            List<EN.POL> PolDL = new List<EN.POL>();
            DataTable PolDT = new DataTable();
            string Table = "PODMaster";
            string Column = "PODID,PODName";
            string Condition = "";
            string OrderBy = "PODName";

            PolDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PolDT != null)
            {
                foreach (DataRow row in PolDT.Rows)
                {
                    EN.POL PolList = new EN.POL();
                    PolList.PODID = Convert.ToInt32(row["PODID"]);
                    PolList.PODName = Convert.ToString(row["PODName"]);
                    PolDL.Add(PolList);
                }
            }
            return PolDL;
        }

        public List<EN.ContainerSize> getContainerSize()
        {
            List<EN.ContainerSize> ContainerSizeDL = new List<EN.ContainerSize>();
            DataTable ContainerSizeDT = new DataTable();
            string Table = "ContainerSize";
            string Column = "ContainerSizeID,ContainerSize";
            string Condition = "";
            string OrderBy = "ContainerSize";

            ContainerSizeDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ContainerSizeDT != null)
            {
                foreach (DataRow row in ContainerSizeDT.Rows)
                {
                    EN.ContainerSize ContainerSizeList = new EN.ContainerSize();
                    ContainerSizeList.ContainerSizeID = Convert.ToInt32(row["ContainerSizeID"]);
                    ContainerSizeList.ContainerSizeName = Convert.ToString(row["ContainerSize"]);
                    ContainerSizeDL.Add(ContainerSizeList);
                }
            }
            return ContainerSizeDL;
        }

        public List<EN.ISOCodes> getISOCodes()
        {
            List<EN.ISOCodes> ISOCodesDL = new List<EN.ISOCodes>();
            DataTable ISOCodesDT = new DataTable();
            string Table = "ISOCodes";
            string Column = "ISOID,ISOCode";
            string Condition = "";
            string OrderBy = "ISOCode";

            ISOCodesDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ISOCodesDT != null)
            {
                foreach (DataRow row in ISOCodesDT.Rows)
                {
                    EN.ISOCodes ISOCodesList = new EN.ISOCodes();
                    ISOCodesList.ISOID = Convert.ToInt32(row["ISOID"]);
                    ISOCodesList.ISOCode = Convert.ToString(row["ISOCode"]);
                    ISOCodesDL.Add(ISOCodesList);
                }
            }
            return ISOCodesDL;
        }

        public List<EN.CargoType> getCargoType()
        {
            List<EN.CargoType> CargoTypeDL = new List<EN.CargoType>();
            DataTable CargoTypeDT = new DataTable();
            string Table = "CargoType";
            string Column = "Cargotypeid,Cargotype";
            string Condition = "";
            string OrderBy = "Cargotype";

            CargoTypeDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CargoTypeDT != null)
            {
                foreach (DataRow row in CargoTypeDT.Rows)
                {
                    EN.CargoType CargoTypeList = new EN.CargoType();
                    CargoTypeList.Cargotypeid = Convert.ToInt32(row["Cargotypeid"]);
                    CargoTypeList.Cargotype = Convert.ToString(row["Cargotype"]);
                    CargoTypeDL.Add(CargoTypeList);
                }
            }
            return CargoTypeDL;
        }

        public List<EN.CommodityGroup> getCommodityGroup()
        {
            List<EN.CommodityGroup> CommodityGroupDL = new List<EN.CommodityGroup>();
            DataTable CommodityGroupDT = new DataTable();
            string Table = "Commodity_Group_M";
            string Column = "Commodity_Group_ID,Commodity_Group_Name";
            string Condition = "IsActive=1";
            string OrderBy = "Commodity_Group_Name";

            CommodityGroupDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CommodityGroupDT != null)
            {
                foreach (DataRow row in CommodityGroupDT.Rows)
                {
                    EN.CommodityGroup CommodityGroupList = new EN.CommodityGroup();
                    CommodityGroupList.Commodity_Group_ID = Convert.ToInt32(row["Commodity_Group_ID"]);
                    CommodityGroupList.Commodity_Group_Name = Convert.ToString(row["Commodity_Group_Name"]);
                    CommodityGroupDL.Add(CommodityGroupList);
                }
            }
            return CommodityGroupDL;
        }

        public List<EN.SalesPersonM> getSalesPersonM()
         {
            List<EN.SalesPersonM> SalesPersonMDL = new List<EN.SalesPersonM>();
            DataTable SalesPersonMDT = new DataTable();
            string Table = "SalesPersonM";
            string Column = "SalesPerson_ID1,SalesPerson_Name";
            string Condition = "IsActive=1";
            string OrderBy = "SalesPerson_Name";

            SalesPersonMDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (SalesPersonMDT != null)
            {
                foreach (DataRow row in SalesPersonMDT.Rows)
                {
                    EN.SalesPersonM SalesPersonMList = new EN.SalesPersonM();
                    SalesPersonMList.SalesPerson_ID1 = Convert.ToInt32(row["SalesPerson_ID1"]);
                    SalesPersonMList.SalesPerson_Name = Convert.ToString(row["SalesPerson_Name"]);
                    SalesPersonMDL.Add(SalesPersonMList);
                }
            }
            return SalesPersonMDL;
        }

        public List<EN.KAMM> getKAMM()
         {
             List<EN.KAMM> KAMDL = new List<EN.KAMM>();
             DataTable KAMMDT = new DataTable();
            string Table = "KAMs";
            string Column = "KAMID,KAM";
            string Condition = "";
            string OrderBy = "KAM";

            KAMMDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (KAMMDT != null)
            {
                foreach (DataRow row in KAMMDT.Rows)
                {
                    EN.KAMM KAMMList = new EN.KAMM();
                    KAMMList.KAMID = Convert.ToInt32(row["KAMID"]);
                    KAMMList.KAM = Convert.ToString(row["KAM"]);
                    KAMDL.Add(KAMMList);
                }
            }
            return KAMDL;
        }


       
        public List<EN.ReasonMasterEntities> GetReason()
        {
            List<EN.ReasonMasterEntities> ReasonDL = new List<EN.ReasonMasterEntities>();
            DataTable ReasonDT = new DataTable();
            string Table = "JOCancelReasons";
            string Column = "ReasonsId,Reasons";
            string Condition = "";
            string OrderBy = "Reasons";

            ReasonDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ReasonDL != null)
            {
                foreach (DataRow row in ReasonDT.Rows)
                {
                    EN.ReasonMasterEntities ReasonList = new EN.ReasonMasterEntities();
                    ReasonList.ReasonsId = Convert.ToInt32(row["ReasonsId"]);
                    ReasonList.Reasons = Convert.ToString(row["Reasons"]);
                    ReasonDL.Add(ReasonList);
                }
            }
            return ReasonDL;
        }

        public List<EN.ImportJOType> getJOType()
        {
            List<EN.ImportJOType> ImportJOTypeDL = new List<EN.ImportJOType>();
            DataTable ImportJOTypeListDT = new DataTable();
            string Table = "Import_Jo_Type";
            string Column = "Jo_Type_ID,Jo_Type";
            string Condition = "";
            string OrderBy = "Jo_Type_ID";

            ImportJOTypeListDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ImportJOTypeListDT != null)
            {
                foreach (DataRow row in ImportJOTypeListDT.Rows)
                {
                    EN.ImportJOType ImportJOTypeList = new EN.ImportJOType();
                    ImportJOTypeList.JotypeId = Convert.ToInt32(row["Jo_Type_ID"]);
                    ImportJOTypeList.Jotype = Convert.ToString(row["Jo_Type"]);
                    ImportJOTypeDL.Add(ImportJOTypeList);
                }
            }
            return ImportJOTypeDL;
        }

        public List<EN.IGMFiLeType> getIGMFileType()
        {
            List<EN.IGMFiLeType> IGMFiLeTypeDL = new List<EN.IGMFiLeType>();
            DataTable IgmFileTypeListDT = new DataTable();
            string Table = "IgmFileType";
            string Column = "FileTypeId,FileTypeName";
            string Condition = "";
            string OrderBy = "FileTypeId";

            IgmFileTypeListDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (IgmFileTypeListDT != null)
            {
                foreach (DataRow row in IgmFileTypeListDT.Rows)
                {
                    EN.IGMFiLeType IGMFileTypeList = new EN.IGMFiLeType();
                    IGMFileTypeList.FileTypeId = Convert.ToInt32(row["FileTypeId"]);
                    IGMFileTypeList.FileTypeName = Convert.ToString(row["FileTypeName"]);
                    IGMFiLeTypeDL.Add(IGMFileTypeList);
                }
            }
            return IGMFiLeTypeDL;
        }


        public string AddJobOrderData(EN.JobOrderMEntities viewModel, List<EN.JobOrderDEntities> JobDetails, int UserId,int igmid)
        {
            string message = "";
            try
            {
                
                Int64 JONO = 0;
                DataTable JobOrderDT = new DataTable();
                //  JobOrderDT = TrackerManager.AddJobOrderData(JobOrderData.AgentID, JobOrderData.SLID, JobOrderData.shipperid, JobOrderData.Importerid, JobOrderData.Chaid, JobOrderData.ViaNo, JobOrderData.VesselID, JobOrderData.PortID, JobOrderData.berthingdate, JobOrderData.Haulage_Type_Id, JobOrderData.File_Status_Id, JobOrderData.Transport_Type_Id, JobOrderData.POL_ID, JobOrderData.Sales_Person_Id,JobOrderData.BLNumber,JobOrderData.BLReceivedDate, UserId);
                JobOrderDT = TrackerManager.AddJobOrderDataIntoTable(viewModel.JONo, viewModel.AgentID, viewModel.SLID, viewModel.shipperid, viewModel.Importerid, viewModel.Chaid, viewModel.ViaNo, viewModel.VesselID, viewModel.PortID, viewModel.berthingdate, viewModel.Haulage_Type_Id, viewModel.File_Status_Id, viewModel.Transport_Type_Id, viewModel.POL_ID, viewModel.Sales_Person_Id, viewModel.BLNumber, viewModel.BLReceivedDate, UserId, viewModel.HouseBLNumber, viewModel.KAMID,viewModel.JoRemarks,viewModel.JoTypeId,igmid,viewModel.IGMNo);

                if (JobOrderDT.Rows.Count > 0)
                {   
                    message = "Record Saved Successfully";
                }
                else
                {
                    message = "Record not Saved Successfully";
                }
                
            }
            catch (Exception ex)
            {
                
                  AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'","") +"for"+  viewModel.JONo );
                
            }
            return message;
        }

        public EN.JobOrderMEntities getVesselDetails(string ViaNo)
        {
            EN.JobOrderMEntities JE = new EN.JobOrderMEntities();
            DataTable VesselDT = new DataTable();
            VesselDT = TrackerManager.getVesselDetails(ViaNo);
            if (VesselDT.Rows.Count > 0)
            {
                JE.VesselID = Convert.ToInt32(VesselDT.Rows[0]["VesselID"]);
                JE.berthingDateInstring = Convert.ToString(VesselDT.Rows[0]["BerthingDate"]);
                JE.PortID = Convert.ToInt16(VesselDT.Rows[0]["portid"]);
                JE.IGMNo = Convert.ToString(VesselDT.Rows[0]["IgmNo"]);
            }


            return JE;
        }

        public Boolean ContainerNoValidation(string containerNo)
        {
            Boolean IsContainerNo=false;
            DataTable ContainerDT = new DataTable();
            ContainerDT = TrackerManager.CheckContainerValidation(containerNo);
            if (ContainerDT.Rows.Count > 0)
            {
                IsContainerNo = Convert.ToBoolean(ContainerDT.Rows[0]["IsContainerNo"]);
            }
            return IsContainerNo;
        }

        public List<EN.JobOrderMEntities> GetJOListForSummary(DateTime FromDate, DateTime ToDate, string SearchCriteria, string SearchText, int Userid, Boolean IsDate)
        {
            List<EN.JobOrderMEntities> JOList = new List<EN.JobOrderMEntities>();
            DataTable JODT = new DataTable();

            JODT = TrackerManager.GetJOList(FromDate, ToDate, SearchCriteria, SearchText, Userid, IsDate);
            if (JODT != null)
            {
                foreach (DataRow row in JODT.Rows)
                {
                    EN.JobOrderMEntities JODTList = new EN.JobOrderMEntities();
                    JODTList.JONo = Convert.ToInt32(row["JONo"]);
                    JODTList.JODateInString = Convert.ToString(row["JoDate"]);
                    JODTList.CustomerName = Convert.ToString(row["AGName"]);
                    JODTList.BLNumber = Convert.ToString(row["BLNumber"]);
                    JODTList.BLReceivedDateInString = Convert.ToString(row["BLReceivedDate"]);
                    JODTList.userName = Convert.ToString(row["UserName"]);
                    JODTList.TEUS = Convert.ToString(row["TEUS"]);
                    JODTList.SubmitCss = Convert.ToString(row["SubmitCss"]);
                    JODTList.SalesPersonName = Convert.ToString(row["SalesPerson_Name"]);
                    JODTList.PortName = Convert.ToString(row["Port name"]);
                    JODTList.Editcss = Convert.ToString(row["Edit"]);
                    JODTList.Remarks = Convert.ToString(row["JoRemarks"]);
                    JOList.Add(JODTList);
                }
            }
            return JOList;
        }

        public string CancelJO(long jono, int userid, int ReasonId)
        {
            string message = "";
            DataTable CancelJODT = new DataTable();
            CancelJODT = TrackerManager.CancelJO(jono, userid, ReasonId);
            if (CancelJODT != null)
            {
                message = Convert.ToString(CancelJODT.Rows[0][0]);
            }
            return message;
        }

        public string SubmitJO(long jono, int userid)
        {
            string message = "";
            DataTable SubmitJODT = new DataTable();
            SubmitJODT = TrackerManager.SubmitJO(jono, userid);
            if (SubmitJODT != null)
            {
                message = Convert.ToString(SubmitJODT.Rows[0][0]);
            }
            return message;
        }

        public EN.JoOrderData getJOViewData(Int64 id, int Userid)
        {
            try
            {
                EN.JobOrderMEntities JobOrderMEntities = new EN.JobOrderMEntities();
                DataSet JODS = new DataSet();
                List<EN.JobOrderDEntities> JOContainerList = new List<EN.JobOrderDEntities>();
                DataTable JOContainerDT = new DataTable();

                JODS = TrackerManager.getJOViewDetails(id, Userid);
                DataTable JODT = new DataTable();
                JODT = JODS.Tables[0];
                JOContainerDT = JODS.Tables[1];

                if (JODT != null)
                {

                    foreach (DataRow row in JODT.Rows)
                    {
                        JobOrderMEntities.JONo = Convert.ToInt64(row["JONo"]);
                        JobOrderMEntities.JODateInString = Convert.ToString(row["JODateInString"]);
                        JobOrderMEntities.SLID = Convert.ToInt32(row["SLID"]);
                        //JobOrderMEntities.shipperid = Convert.ToInt32(row["shipperid"]);
                        JobOrderMEntities.AgentID = Convert.ToInt32(row["AgentID"]);
                        JobOrderMEntities.Importerid = Convert.ToInt32(row["Importerid"]);
                        //JobOrderMEntities.Chaid = Convert.ToInt32(row["Chaid"]);
                        JobOrderMEntities.ViaNo = Convert.ToString(row["ViaNo"]);
                        JobOrderMEntities.VesselID = Convert.ToInt32(row["VesselID"]);
                        JobOrderMEntities.PortID = Convert.ToInt32(row["PortID"]);
                        JobOrderMEntities.berthingDateInstring = Convert.ToString(row["berthingdateInString"]);
                        JobOrderMEntities.BLNumber = Convert.ToString(row["BLNumber"]);
                        JobOrderMEntities.BLReceivedDateInString = Convert.ToString(row["BLReceivedDateInString"]);
                        JobOrderMEntities.File_Status_Id = Convert.ToInt32(row["File_Status_Id"]);
                        JobOrderMEntities.Transport_Type_Id = Convert.ToInt32(row["Transport_Type_Id"]);
                        //JobOrderMEntities.POL_ID = Convert.ToInt32(row["POL_ID"]);
                        JobOrderMEntities.Sales_Person_Id = Convert.ToInt32(row["Sales_Person_Id"]);
                        JobOrderMEntities.Haulage_Type_Id = Convert.ToInt32(row["Haulage_Type_Id"]);
                        //JobOrderMEntities.HouseBLNumber = Convert.ToString(row["HBL"]);
                        JobOrderMEntities.AttachmentCount = Convert.ToString(JODS.Tables[2].Rows[0][0]);
                        JobOrderMEntities.KAMID = Convert.ToInt16(row["JKDM"]);
                        JobOrderMEntities.IGMNo = Convert.ToString(row["IGMNo"]);

                    }
                }
                if (JOContainerDT != null)
                {
                    foreach (DataRow row in JOContainerDT.Rows)
                    {
                        EN.JobOrderDEntities JODTList = new EN.JobOrderDEntities();
                        // JODTList.JONo = Convert.ToInt32(row["JONo"]);
                        // JODTList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                        // JODTList.Size = Convert.ToInt16(row["FL"]);
                        // JODTList.FL = Convert.ToString(row["ISOCode"]);
                        // JODTList.ISOCode = Convert.ToString(row["ISOCode"]);
                        // JODTList.Cargotypeid = Convert.ToInt32(row["Cargotypeid"]);
                        // JODTList.Commodity_group_id = Convert.ToInt32(row["Commodity_group_id"]);
                        // JODTList.FLName = Convert.ToString(row["FLName"]);
                        // JODTList.ContainerSize=  Convert.ToString(row["ContainerSize"]);
                        //JODTList.Commodity_Group_Name=  Convert.ToString(row["Commodity_Group_Name"]);
                        // JODTList.ISOCodeName=  Convert.ToString(row["ISOCodeName"]);
                        // JODTList.Cargotype=  Convert.ToString(row["Cargotype"]);
                        JODTList.JONo = Convert.ToInt32(row["JONo"]);
                        JODTList.ContainerNo = Convert.ToString(row["ContainerNo"]) + "<input Name=ContainerNo type=hidden id=ContainerNo   value=" + Convert.ToString(row["ContainerNo"]) + ">"; ;
                        JODTList.FLName = Convert.ToString(row["FL"]) + "<input Name=FLid type=hidden id=FL   value=" + Convert.ToString(row["FLId"]) + ">";
                        JODTList.ISOCodeName = Convert.ToString(row["ISOCodeName"]) + "<input Name=ISOCodeID type=hidden id=ISOCode   value=" + Convert.ToString(row["ISOCODE"]) + ">";

                        JODTList.Commodity_Group_Name = Convert.ToString(row["CommodityGroup"]) + "<input Name=Commodity_group_id type=hidden id=Commodity_group_id   value=" + Convert.ToInt32(row["Commodity_group_id"]) + ">";
                        JODTList.Cargotype = Convert.ToString(row["Cargotype"]) + "<input Name=Cargotypeid type=hidden id=Cargotypeid   value=" + Convert.ToString(row["CARGOTYPEID"]) + ">";
                        JODTList.ContainerSize = Convert.ToString(row["Size"]) + "<input Name=Size type=hidden id=Size   value=" + Convert.ToString(row["SizeID"]) + ">";
                        JODTList.TempID = Convert.ToInt64(row["ID"]);
                        JODTList.ScanType = Convert.ToString(row["ScanType"]);
                        JODTList.Size = Convert.ToInt16(row["Size"]);
                        JODTList.Temp = Convert.ToString(row["ContainerNo"]);
                        // ContainerList.Add(JODTList);
                        JOContainerList.Add(JODTList);
                    }
                }

                EN.JoOrderData JOrderList = new EN.JoOrderData();
                JOrderList.JOList = JobOrderMEntities;
                JOrderList.Containerlist = JOContainerList;
                return JOrderList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public EN.JobOrderContainerEntities AddContainerData(EN.JobOrderDEntities JD, int Userid)
        {
            List<EN.JobOrderDEntities> ContainerList = new List<EN.JobOrderDEntities>();
            EN.JobOrderDEntities ValidationList = new EN.JobOrderDEntities();

            DataSet ContainerDS = new DataSet();

            DataTable ValidationDT = new DataTable();
            DataTable ContainerDT = new DataTable();
            ContainerDS = TrackerManager.AddContainerData(JD.JONo, JD.ContainerNo, JD.Size, JD.FL, JD.ISOCode, JD.Cargotypeid, JD.Commodity_group_id, Userid);

            ValidationDT = ContainerDS.Tables[0];
            ContainerDT = ContainerDS.Tables[1];

            if (ValidationDT.Rows.Count > 0)
            {
                //EN.JobOrderDEntities Validation = new EN.JobOrderDEntities();
                ValidationList.validationMessage = Convert.ToInt32(ValidationDT.Rows[0][0]);
              //  ValidationList.validationMessage(Convert.ToInt32(ValidationDT.Rows[0][0]));
            }
            //int FLid = 0;
            //if (ContainerDT.Rows.Count > 0)
            //{
            //    //EN.JobOrderDEntities Validation = new EN.JobOrderDEntities();
            //    FLid = Convert.ToInt32(ValidationDT.Rows[13][0]);
            //    Version["flid"] = FLid;
            //    //  ValidationList.validationMessage(Convert.ToInt32(ValidationDT.Rows[0][0]));
            //}

            if (ContainerDT != null)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.JobOrderDEntities JODTList = new EN.JobOrderDEntities();
                    
                    JODTList.JONo = Convert.ToInt32(row["JONo"]);
                    JODTList.ContainerNo = Convert.ToString(row["ContainerNo"]); //+ "<input Name=ContainerNo type=hidden id=ContainerNo   value=" + Convert.ToString(row["ContainerNo"]) + ">"; ;
                    JODTList.FLName = Convert.ToString(row["FL"]);// + "<input Name=FLid type=hidden id=FL   value=" + Convert.ToString(row["FLId"]) + ">";
                    JODTList.ISOCodeName = Convert.ToString(row["ISOCodeName"]);// + "<input Name=ISOCodeID type=hidden id=ISOCode   value=" + Convert.ToString(row["ISOCODE"]) + ">";

                    JODTList.Commodity_Group_Name = Convert.ToString(row["CommodityGroup"]);// + "<input Name=Commodity_group_id type=hidden id=Commodity_group_id   value=" + Convert.ToInt32(row["Commodity_group_id"]) + ">";
                    JODTList.Cargotype = Convert.ToString(row["Cargotype"]);// + "<input Name=Cargotypeid type=hidden id=Cargotypeid   value=" + Convert.ToString(row["CARGOTYPEID"]) + ">";
                    JODTList.ContainerSize = Convert.ToString(row["Size"]);// + "<input Name=Size type=hidden id=Size   value=" + Convert.ToString(row["SizeID"]) + ">";
                    JODTList.TempID = Convert.ToInt64(row["ID"]);
                    JODTList.Size = Convert.ToInt16(row["Size"]);
                    JODTList.TempfLID= Convert.ToInt16(row["FLid"]);
                    JODTList.Temp= Convert.ToString(row["ContainerNo"]);
                    JODTList.ScanType = Convert.ToString(row["ScanType"]);
                    ContainerList.Add(JODTList);
                }
                
               // return JOrderList;
            }

            EN.JobOrderContainerEntities JOrderList = new EN.JobOrderContainerEntities();
            JOrderList.JOValidation = ValidationList;
            JOrderList.Containerlist = ContainerList;

            return JOrderList;
        }

        public EN.JobOrderContainerEntities DeleteContainerData(string id, int Userid)
        {
            DataSet ContainerDS = new DataSet();
            List<EN.JobOrderDEntities> ContainerList = new List<EN.JobOrderDEntities>();
            EN.JobOrderDEntities ValidationList = new EN.JobOrderDEntities();
            DataTable ContainerDT = new DataTable();
            DataTable ValidationDT = new DataTable();

            ContainerDS = TrackerManager.DeleteContainerDataNew(id, Userid);
            ValidationDT = ContainerDS.Tables[0];
            ContainerDT = ContainerDS.Tables[1];

            if (ValidationDT.Rows.Count > 0)
            {
                //EN.JobOrderDEntities Validation = new EN.JobOrderDEntities();
                ValidationList.validationMessage = Convert.ToInt32(ValidationDT.Rows[0][0]);
                //  ValidationList.validationMessage(Convert.ToInt32(ValidationDT.Rows[0][0]));
            }

            if (ContainerDT != null)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.JobOrderDEntities JODTList = new EN.JobOrderDEntities();
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

                    JODTList.JONo = Convert.ToInt32(row["JONo"]);
                    JODTList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                        //+ "<input Name=ContainerNo type=hidden id=ContainerNo   value=" + Convert.ToString(row["ContainerNo"]) + ">"; ;
                    JODTList.FLName = Convert.ToString(row["FL"]) + "<input Name=FLid type=hidden id=FL   value=" + Convert.ToString(row["FLId"]) + ">";
                    JODTList.ISOCodeName = Convert.ToString(row["ISOCodeName"]) + "<input Name=ISOCodeID type=hidden id=ISOCode   value=" + Convert.ToString(row["ISOCODE"]) + ">";

                    JODTList.Commodity_Group_Name = Convert.ToString(row["CommodityGroup"]) + "<input Name=Commodity_group_id type=hidden id=Commodity_group_id   value=" + Convert.ToInt32(row["Commodity_group_id"]) + ">";
                    JODTList.Cargotype = Convert.ToString(row["Cargotype"]) + "<input Name=Cargotypeid type=hidden id=Cargotypeid   value=" + Convert.ToString(row["CARGOTYPEID"]) + ">";
                    JODTList.ContainerSize = Convert.ToString(row["Size"]) + "<input Name=Size type=hidden id=Size   value=" + Convert.ToString(row["SizeID"]) + ">";
                    JODTList.Temp = Convert.ToString(row["ContainerNo"]);

                    ContainerList.Add(JODTList);
                }
            }

            EN.JobOrderContainerEntities JOrderList = new EN.JobOrderContainerEntities();
            JOrderList.JOValidation = ValidationList;
            JOrderList.Containerlist = ContainerList;

            return JOrderList;
        }

        public EN.JobOrderContainerEntities AddImportDataIntoTable(DataTable ContainerDT, int Userid, long jono)
        {
            List<EN.JobOrderDEntities> ContainerList = new List<EN.JobOrderDEntities>();
            EN.JobOrderDEntities ValidationList = new EN.JobOrderDEntities();
            DataSet ContainerDS = new DataSet();

            DataTable ValidationDT = new DataTable();
            DataTable ContainerDataDT = new DataTable();
            DataTable InvalidContainerList = new DataTable();

            ContainerDS = TrackerManager.AddImportData(ContainerDT, Userid, jono);

            ValidationDT = ContainerDS.Tables[0];
            ContainerDataDT = ContainerDS.Tables[2];
            InvalidContainerList = ContainerDS.Tables[1];
            if (ValidationDT.Rows.Count > 0)
            {
                
                ValidationList.validationMessage = Convert.ToInt32(ValidationDT.Rows[0][0]);
                ValidationList.containerList = Convert.ToString(InvalidContainerList.Rows[0][0]);

                
            }
            if (ContainerDataDT != null)
            {
                foreach (DataRow row in ContainerDataDT.Rows)
                {
                    EN.JobOrderDEntities JODTList = new EN.JobOrderDEntities();
                    

                    JODTList.JONo = Convert.ToInt32(row["JONo"]);
                    JODTList.ContainerNo = Convert.ToString(row["ContainerNo"]) + "<input Name=ContainerNo type=hidden id=ContainerNo   value=" + Convert.ToString(row["ContainerNo"]) + ">"; 
                    JODTList.FLName = Convert.ToString(row["FL"]) + "<input Name=FLid type=hidden id=FL   value=" + Convert.ToString(row["FLId"]) + ">";
                    JODTList.ISOCodeName = Convert.ToString(row["ISOCodeName"]) + "<input Name=ISOCodeID type=hidden id=ISOCode   value=" + Convert.ToString(row["ISOCODE"]) + ">";

                    JODTList.Commodity_Group_Name = Convert.ToString(row["CommodityGroup"]) + "<input Name=Commodity_group_id type=hidden id=Commodity_group_id   value=" + Convert.ToInt32(row["Commodity_group_id"]) + ">";
                    JODTList.Cargotype = Convert.ToString(row["Cargotype"]) + "<input Name=Cargotypeid type=hidden id=Cargotypeid   value=" + Convert.ToString(row["CARGOTYPEID"]) + ">";
                    JODTList.ContainerSize = Convert.ToString(row["Size"]) + "<input Name=Size type=hidden id=Size   value=" + Convert.ToString(row["SizeID"]) + ">";
                    JODTList.TempID = Convert.ToInt64(row["ID"]);
                    JODTList.Size = Convert.ToInt16(row["Size"]);
                    ContainerList.Add(JODTList);
                }
            }
            EN.JobOrderContainerEntities JOrderList = new EN.JobOrderContainerEntities();
            JOrderList.JOValidation = ValidationList;
            JOrderList.Containerlist = ContainerList;

            return JOrderList;
        }

        public void DeleteDataFromTempTable(int userid)
        {
            TrackerManager.DeleteTempData(userid);
        }

        public Boolean CheckContainerData(int userid)
        {
            Boolean IsDuplicateContainer = false;
            Int16 i = 0;
             DataTable ContainerDT = new DataTable();
            ContainerDT=TrackerManager.CheckDuplicateContainer(userid);
            if (ContainerDT.Rows.Count > 0)
            {
                i = Convert.ToInt16(ContainerDT.Rows[0]["duplicateContainer"]);
            }
            if (i == 0)
            {
                IsDuplicateContainer = true;
                return IsDuplicateContainer;
            }
            else {
                IsDuplicateContainer = false;
                return IsDuplicateContainer;
            }
            
        }
        public List<EN.AmmendmentlogEntities> GetLogEvent(long jono)
        {
            List<EN.AmmendmentlogEntities> LogEventList = new List<EN.AmmendmentlogEntities>();
            DataTable LogEventDT = new DataTable();

            LogEventDT = TrackerManager.GetLogEvent(jono);
            if (LogEventDT != null)
            {
                foreach (DataRow row in LogEventDT.Rows)
                {
                    EN.AmmendmentlogEntities LogEventDataList = new EN.AmmendmentlogEntities();

                    LogEventDataList.srno = Convert.ToInt32(row["srno"]);
                    LogEventDataList.sentence = Convert.ToString(row["Sentence"]);


                    LogEventList.Add(LogEventDataList);
                }
            }
            return LogEventList;
        }

        public List<EN.JobOrderAttachmentEntities> AddJOAttachment(int Userid, byte[] bytes, string contentType, string fname)
        {
            List<EN.JobOrderAttachmentEntities> AttachmentList = new List<EN.JobOrderAttachmentEntities>();
            DataTable AttachmentDT = new DataTable();

            AttachmentDT = TrackerManager.AddAttachment(Userid, bytes, contentType, fname);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {
                    EN.JobOrderAttachmentEntities AttachmentDataList = new EN.JobOrderAttachmentEntities();

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


        public List<EN.JobOrderAttachmentEntities> GetJOAttachment(int Userid)
        {
            List<EN.JobOrderAttachmentEntities> AttachmentList = new List<EN.JobOrderAttachmentEntities>();
            DataTable AttachmentDT = new DataTable();

            AttachmentDT = TrackerManager.GetAttachment(Userid);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {
                    EN.JobOrderAttachmentEntities AttachmentDataList = new EN.JobOrderAttachmentEntities();

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
        public EN.JobOrderAttachmentEntities GetFile(long id)
        {
            EN.JobOrderAttachmentEntities AttachmentDataList = new EN.JobOrderAttachmentEntities();
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

        public string DeleteFile(long id, int Userid)
        {
            List<EN.JobOrderAttachmentEntities> JOAttachmentList = new List<EN.JobOrderAttachmentEntities>();
            DataTable JOAttachmentDT = new DataTable();

            string message = "";
            JOAttachmentDT = TrackerManager.DeleteFile(id, Userid);
            if (JOAttachmentDT != null)
            {
                message = Convert.ToString(JOAttachmentDT.Rows[0][0]);
            }
            return message;
        }

        public void AddErrorLog(string error)
        {
            TrackerManager.AddErrorLog(error);
        }


        public List<EN.JobOrderMEntities> getBLList()
        {
            List<EN.JobOrderMEntities> JOList = new List<EN.JobOrderMEntities>();
            DataTable JODT = new DataTable();

            JODT = TrackerManager.GetJOListForPOD();
            if (JODT != null)
            {
                int count = 1;
                foreach (DataRow row in JODT.Rows)
                {
                    EN.JobOrderMEntities JODTList = new EN.JobOrderMEntities();
                    JODTList.SRNO = Convert.ToInt32(count++);
                    JODTList.JONo = Convert.ToInt32(row["JONo"]);
                    JODTList.CustomerName = Convert.ToString(row["Customer"]);
                    JODTList.TEUS = Convert.ToString(row["TEUS"]);
                    JODTList.PODETADateString = Convert.ToString(row["PODDate"]);
                    JODTList.shippingLineName = Convert.ToString(row["SLName"]);
                    JODTList.HBLNumber = Convert.ToString(row["HBLNumber"]);
                    JODTList.BLNumber = Convert.ToString(row["BLNumber"]);
                    JODTList.PODETADate = Convert.ToString(row["PODETADate"]);
                    JODTList.VesselID = Convert.ToInt32(row["VesselID"]);
                    JODTList.VesselName = Convert.ToString(row["VesselName"]);
                    JODTList.IGMNo = Convert.ToString(row["IGMNO"]);
                    JODTList.SLID = Convert.ToInt32(row["Line"]);
                    JODTList.File_Status_Id = Convert.ToInt32(row["File_status_id"]);
                    JODTList.File_status_Name = Convert.ToString(row["File_Status"]);
                    JODTList.Dwell_Days = Convert.ToString(row["DwellDays"]);
                    JOList.Add(JODTList);
                }
            }
            return JOList;
        }


        public string UpdateBLPODETA(DataTable BLList, Int32 userId)
           {
               string message = "";


               Dictionary<object, object> lstparam = new Dictionary<object, object>();
               lstparam.Add("userId", userId);

               TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

               int i = db.UpdateBLPOD("USP_UpdateBLData", lstparam, BLList);
               if (i != 0)
               {
                   message = "Records updated successfully";
               }
               else
               {
                   message = "Records not update, please try again";
               }

               return message;
           }

        public List<EN.IGMFileStatusInfo> GetIGMFileStatus()
        {
            List<EN.IGMFileStatusInfo> FilestatusDL = new List<EN.IGMFileStatusInfo>();
            DataTable ReasonDT = new DataTable();


            ReasonDT = TrackerManager.GetIGMFileStatus();
            if (FilestatusDL != null)
            {
                foreach (DataRow row in ReasonDT.Rows)
                {
                    EN.IGMFileStatusInfo StatusList = new EN.IGMFileStatusInfo();
                    StatusList.File_Status_ID = Convert.ToInt32(row["File_Status_ID"]);
                    StatusList.File_Status = Convert.ToString(row["File_Status"]);
                    FilestatusDL.Add(StatusList);
                }
            }
            return FilestatusDL;
        }
        public string UpdateBLData(int JONO, string PODETADate, int VesselID, int File_Status_Id, string IGMNo, int userId)
        {
            List<EN.JobOrderAttachmentEntities> JOAttachmentList = new List<EN.JobOrderAttachmentEntities>();
            DataTable JOAttachmentDT = new DataTable();

            string message = "";
            JOAttachmentDT = TrackerManager.UpdateBLData(JONO, PODETADate, VesselID, File_Status_Id, IGMNo, userId);
            if (JOAttachmentDT != null)
            {
                message = "BL details updated successfully!";
            }
            return message;
        }
        // Codes By Prashant
        // Module : Movement Pendancy 
        public List<EN.MovementPendancyEntities> GetMovementPendancyDetails()
        {
            List<EN.MovementPendancyEntities> MovementPendancyDL = new List<EN.MovementPendancyEntities>();
            DataTable MovementDL = new DataTable();
            MovementDL = TrackerManager.getMovementPendancyList();
            if (MovementDL.Rows.Count > 0)
            {
                double dblSrNo = 0;
                foreach (DataRow row in MovementDL.Rows)
                {
                    EN.MovementPendancyEntities MovementpendancyDataList = new EN.MovementPendancyEntities();
                    dblSrNo += 1;
                    MovementpendancyDataList.SrNo = dblSrNo;
                    MovementpendancyDataList.VesselName = Convert.ToString(row["Vessel Name"]);
                    MovementpendancyDataList.ViaNo = Convert.ToString(row["Via No"]);
                    MovementpendancyDataList.ETA = Convert.ToString(row["ETA"]);
                    MovementpendancyDataList.AGING = Convert.ToString(row["AGING"]);
                    MovementpendancyDataList.TERMINAL = Convert.ToString(row["TERMINAL"]);
                    MovementpendancyDataList.BirthingDateandTime = Convert.ToString(row["Discharge Date"]);
                    MovementpendancyDataList.HBLNumber = Convert.ToString(row["HBL Number"]);
                    MovementpendancyDataList.BLNumber = Convert.ToString(row["BL Number"]);
                    MovementpendancyDataList.BLReceivedDate = Convert.ToString(row["BL Received Date"]);
                    MovementpendancyDataList.BLEntrydate = Convert.ToString(row["B/L Entry date & Time"]);
                    MovementpendancyDataList.ShippingLine = Convert.ToString(row["Shipping Line"]);
                    MovementpendancyDataList.Size = Convert.ToInt32(row["Size"]);
                    MovementpendancyDataList.ShipperName = Convert.ToString(row["Shipper Name"]);
                    MovementpendancyDataList.ImporterName = Convert.ToString(row["Importer Name"]);
                    MovementpendancyDataList.CHAName = Convert.ToString(row["CHA Name"]);
                    MovementpendancyDataList.BillingParty = Convert.ToString(row["Billing Party"]);
                    MovementpendancyDataList.HaulageType = Convert.ToString(row["Haulage Type"]);
                    MovementpendancyDataList.Commodity = Convert.ToString(row["Commodity"]);
                    MovementpendancyDataList.IGMStatus = Convert.ToString(row["IGM Status"]);
                    MovementpendancyDataList.PortOfLoading = Convert.ToString(row["Port Of Loading"]);
                    MovementpendancyDataList.ContainerNo = Convert.ToString(row["Container No."]);
                    MovementpendancyDataList.ScanType = Convert.ToString(row["Scan Type"]);
                    MovementpendancyDataList.Remarks = Convert.ToString(row["Remarks"]);
                    MovementpendancyDataList.SMTPDate = Convert.ToString(row["SMTP Date"]);
                    MovementpendancyDataList.OutDateFromPort = Convert.ToString(row["Out Date From Port"]);
                    MovementpendancyDataList.InTransit = Convert.ToString(row["In Transit"]);
                    MovementpendancyDataList.MarketingPerson = Convert.ToString(row["Marketing Person"]);
                    MovementpendancyDataList.KAM = Convert.ToString(row["KAM"]);
                    MovementpendancyDataList.KAMTeamManagementTeamLastCheckedOn = Convert.ToString(row["Last Checked"]);
                    MovementpendancyDataList.Teus = Convert.ToString(row["Teus"]);
                    MovementPendancyDL.Add(MovementpendancyDataList);
                }
            }
            return MovementPendancyDL;
        }

        public List<EN.TeusSearch> teusSearchLoaded()
        {
            List<EN.TeusSearch> passingDL = new List<EN.TeusSearch>();
            DataTable dt = new DataTable();
            DataSet JODS = new DataSet();
            JODS = TrackerManager.TeussearchPord();
            DataTable JODT = new DataTable();
            JODT = JODS.Tables[1];
            
             
            if (JODT != null)
            {
                foreach (DataRow row in JODT.Rows)
                {
                    EN.TeusSearch PassingList = new EN.TeusSearch();
                    PassingList.txt20 = Convert.ToString(row["T20"]);
                    PassingList.txt40 = Convert.ToString(row["T40"]);
                    PassingList.txt45 = Convert.ToString(row["T45"]);
                    PassingList.Teus = Convert.ToString(row["Teus"]);





                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        
        public List<EN.TeusSearch> teusSearchLoadeds(string AsonDate)
        {
            List<EN.TeusSearch> passingDL = new List<EN.TeusSearch>();
            DataTable dt = new DataTable();
            DataSet JODS = new DataSet();
            JODS = TrackerManager.TeussearchPords(AsonDate);
            DataTable JODT = new DataTable();
            JODT = JODS.Tables[1];


            if (JODT != null)
            {
                foreach (DataRow row in JODT.Rows)
                {
                    EN.TeusSearch PassingList = new EN.TeusSearch();
                    PassingList.txt20 = Convert.ToString(row["T20"]);
                    PassingList.txt40 = Convert.ToString(row["T40"]);
                    PassingList.txt45 = Convert.ToString(row["T45"]);
                    PassingList.Teus = Convert.ToString(row["Teus"]);





                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        public List<EN.TeusSearch> teusSearchLoadedAll(string AsonDate)
        {
            List<EN.TeusSearch> passingDL = new List<EN.TeusSearch>();
            DataTable dt = new DataTable();
            DataSet JODS = new DataSet();
            JODS = TrackerManager.teusSearchLoadedAll(AsonDate);
            DataTable JODT = new DataTable();
            JODT = JODS.Tables[1];


            if (JODT != null)
            {
                foreach (DataRow row in JODT.Rows)
                {
                    EN.TeusSearch PassingList = new EN.TeusSearch();
                    PassingList.txt20 = Convert.ToString(row["T20"]);
                    PassingList.txt40 = Convert.ToString(row["T40"]);
                    PassingList.txt45 = Convert.ToString(row["T45"]);
                    PassingList.Teus = Convert.ToString(row["Teus"]);





                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }
        // Codes End By Prashant
        // Codes End By Rahul
        public List<EN.PortPendancyEntities> GetPortPendancyDetails()
        {
            List<EN.PortPendancyEntities> PortPendancyDL = new List<EN.PortPendancyEntities>();
            DataTable MovementDL = new DataTable();
            MovementDL = TrackerManager.getPortPendencyList();
            if (MovementDL.Rows.Count > 0)
            {
                double dblSrNo = 0;
                foreach (DataRow row in MovementDL.Rows)
                {
                    EN.PortPendancyEntities PortPendencyDataList = new EN.PortPendancyEntities();
                    dblSrNo += 1;
                    PortPendencyDataList.SrNo = dblSrNo;
                    PortPendencyDataList.VesselName = Convert.ToString(row["Vessel Name"]);
                    PortPendencyDataList.ViaNo = Convert.ToString(row["Via No"]);
                    PortPendencyDataList.ETA = Convert.ToString(row["ETA"]);
                    PortPendencyDataList.AGING = Convert.ToString(row["AGING"]);
                    PortPendencyDataList.TERMINAL = Convert.ToString(row["TERMINAL"]);
                    PortPendencyDataList.BirthingDateandTime = Convert.ToString(row["Discharge Date"]);
                    PortPendencyDataList.HBLNumber = Convert.ToString(row["HBL Number"]);
                    PortPendencyDataList.BLNumber = Convert.ToString(row["BL Number"]);
                    PortPendencyDataList.IGMNo = Convert.ToString(row["IGM No"]);
                    PortPendencyDataList.ItemNo = Convert.ToString(row["Item No"]);
                    PortPendencyDataList.BLReceivedDate = Convert.ToString(row["BL Received Date"]);
                    PortPendencyDataList.BLEntrydate = Convert.ToString(row["B/L Entry date & Time"]);
                    PortPendencyDataList.ShippingLine = Convert.ToString(row["Shipping Line"]);
                    PortPendencyDataList.ContainerNo = Convert.ToString(row["Container No."]);
                    PortPendencyDataList.Size = Convert.ToInt32(row["Size"]);
                    PortPendencyDataList.Type = Convert.ToString(row["Type"]);
                    PortPendencyDataList.ImporterName = Convert.ToString(row["Importer Name"]);
                    PortPendencyDataList.CHAName = Convert.ToString(row["CHA Name"]);
                    PortPendencyDataList.HaulageType = Convert.ToString(row["Haulage Type"]);
                    PortPendencyDataList.Commodity = Convert.ToString(row["Commodity"]);
                    PortPendencyDataList.ModeOfTransport = Convert.ToString(row["Mode Of Transport"]);
                    PortPendencyDataList.ScanType = Convert.ToString(row["Scan Type"]);
                    PortPendencyDataList.Remarks = Convert.ToString(row["Remarks"]);
                    PortPendencyDataList.SMTPDate = Convert.ToString(row["SMTP Date"]);
                    PortPendencyDataList.OutDateFromPort = Convert.ToString(row["Out Date From Port"]);
                    PortPendencyDataList.InTransit = Convert.ToString(row["In Transit"]);
                    PortPendencyDataList.Teus = Convert.ToString(row["Teus"]);

                    
                    PortPendancyDL.Add(PortPendencyDataList);
                }
            }
            return PortPendancyDL;
        }

        public List<EN.MovementPendancyEntities> GridViewBindDetailsNew(int IGMFileId)
        {
            List<EN.MovementPendancyEntities> MovementPendancyDL = new List<EN.MovementPendancyEntities>();
            DataTable MovementDL = new DataTable();
            MovementDL = TrackerManager.GridViewBindDetailsNew(IGMFileId);
            if (MovementDL.Rows.Count > 0)
            {
                //double dblSrNo = 0;
                foreach (DataRow row in MovementDL.Rows)
                {
                    EN.MovementPendancyEntities MovementpendancyDataList = new EN.MovementPendancyEntities();
                   // dblSrNo += 1;
                    //MovementpendancyDataList.SrNo = dblSrNo;
                    MovementpendancyDataList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    MovementpendancyDataList.Size = Convert.ToInt32(row["Size"]);
                    MovementpendancyDataList.isocode = Convert.ToString(row["ISOCode"]);
                    MovementpendancyDataList.FclLcl = Convert.ToString(row["FL"]);
                    MovementpendancyDataList.CargoType = Convert.ToString(row["CargoType"]);
                    MovementpendancyDataList.Commodity = Convert.ToString(row["Commdity"]);
                    MovementpendancyDataList.ScanType = Convert.ToString(row["ScanType"]);
                    MovementPendancyDL.Add(MovementpendancyDataList);
                }
            }
            return MovementPendancyDL;
        }

        public List<EN.PaymentModes> getPaymentModes()
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

        public List<EN.ImportBank> getImportBank()
        {
            List<EN.ImportBank> CustomerDL = new List<EN.ImportBank>();
            DataTable CustomerDT = new DataTable();
            string Table = "import_banks";
            string Column = "bankID,bankname";
            string Condition = "";
            string OrderBy = "bankID";

            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.ImportBank CustomerList = new EN.ImportBank();
                    CustomerList.BankId = Convert.ToString(row["bankID"]);
                    CustomerList.BankName = Convert.ToString(row["bankname"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }

        public int AddInvoiceData(EN.CategorywiseInvoice viewModel, DataTable dtInvoiceList, DataTable dtPaymentList, int UserId, string Category, string TDSPerc, string ReceiptType, string Remarks, string PayFrom,  string ADVReceiptNo,string Search, string TDSWorkyear)
        {
            string message = "";
            int value = 0;
            try
            {
                DataTable JobOrderDT = new DataTable();
                value = TrackerManager.AddInvoiceDataIntoTable(viewModel.BillParty, viewModel.CHAID, dtInvoiceList, dtPaymentList, UserId, Category, TDSPerc, ReceiptType, Remarks, PayFrom, ADVReceiptNo,  viewModel.AccountNoId, Search, TDSWorkyear);
            }
            catch (Exception ex)
            {

                //AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);

            }
            return value;
        }

        public Dictionary<object, object> AddCreditInvoiceData(DataTable dtInvoiceDetails,int userId,string Category,string ReceiptType,string Remarks,string PendingPendingChequeRTGS)
        {
            string message = "";
            Dictionary<object, object> output=null;
            try
            {
                output = TrackerManager.AddCreditDataIntoTable(dtInvoiceDetails, userId, Category, ReceiptType, Remarks, PendingPendingChequeRTGS);
            }
            catch (Exception ex)
            {

                //AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);

            }
            return output;
        }

        public List<EN.TDSPerM> getTDSPerM()
        {
            List<EN.TDSPerM> TDSPerM = new List<EN.TDSPerM>();
            DataTable TDSPerMDT = new DataTable();
            string Table = "TDS_PER_M";
            string Column = "TDS_PER_ID,PERCENTAGE";
            string Condition = "";
            string OrderBy = "TDS_PER_ID desc";

            TDSPerMDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TDSPerMDT != null)
            {
                foreach (DataRow row in TDSPerMDT.Rows)
                {
                    EN.TDSPerM TDSPerMList = new EN.TDSPerM();
                    TDSPerMList.TDSPerId = Convert.ToInt32(row["TDS_PER_ID"]);
                    TDSPerMList.Percentage = Convert.ToString(row["PERCENTAGE"]);

                    TDSPerM.Add(TDSPerMList);
                }
            }
            return TDSPerM;
        }
        public List<EN.Customer> getAutoCustomer(string Prefix, string PayFrom)
        {
            List<EN.Customer> CustomerDL = new List<EN.Customer>();
            DataTable CustomerDT = new DataTable();

            CustomerDT = TrackerManager.getAutoCustomer(Prefix, PayFrom);
            if (CustomerDT != null)
            {
                if (CustomerDT.Rows.Count > 0)
                {
                    foreach (DataRow row in CustomerDT.Rows)
                    {
                        EN.Customer CustomerList = new EN.Customer();
                        CustomerList.AGID = Convert.ToInt32(row["AGID"]);
                        CustomerList.AGName = Convert.ToString(row["AGName"]);

                        CustomerDL.Add(CustomerList);
                    }
                }
                else
                {
                    EN.Customer CustomerList = new EN.Customer();
                    CustomerList.AGID = 0;
                    CustomerList.AGName = "No Data Found";

                    CustomerDL.Add(CustomerList);
                }
                
            }
            return CustomerDL;
        }
        // Codes End By Rahul
        public int AddInvoiceDataExport(EN.CategorywiseInvoice viewModel, DataTable dtInvoiceList, DataTable dtPaymentList, int UserId, string Category, string TDSPerc, string ReceiptType, string Remarks)
        {
            string message = "";
            int value = 0;
            try
            {
                DataTable JobOrderDT = new DataTable();
                value = TrackerManager.AddInvoiceDataIntoTableExport(viewModel.BillParty, viewModel.CHAID, dtInvoiceList, dtPaymentList, UserId, Category, TDSPerc, ReceiptType, Remarks);
            }
            catch (Exception ex)
            {

                //AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);

            }
            return value;
        }

        public EN.JobOrderMEntities getVesselDetailsExp(string ViaNo)
        {
            EN.JobOrderMEntities JE = new EN.JobOrderMEntities();
            DataTable VesselDT = new DataTable();
            VesselDT = TrackerManager.getVesselDetailsExp(ViaNo);
            if (VesselDT.Rows.Count > 0)
            {
                JE.VesselID = Convert.ToInt32(VesselDT.Rows[0]["VesselID"]);
                JE.berthingDateInstring = Convert.ToString(VesselDT.Rows[0]["BerthingDate"]);
                JE.PortID = Convert.ToInt16(VesselDT.Rows[0]["portid"]);
                JE.IGMNo = Convert.ToString(VesselDT.Rows[0]["IgmNo"]);
                JE.Voyage = Convert.ToString(VesselDT.Rows[0]["Voyage"]);
                JE.PortName = Convert.ToString(VesselDT.Rows[0]["POL"]);
                JE.RotationNo = Convert.ToString(VesselDT.Rows[0]["RotationNo"]);
            }


            return JE;
        }
        public List<EN.CommonAccountDets> getCommonAccountDet()
        {
            List<EN.CommonAccountDets> CommonAccountDetsListM = new List<EN.CommonAccountDets>();
            DataTable TDSPerMDT = new DataTable();
            string Table = "Company_Account_Dets";
            string Column = "Con_Acc_ID,AccountNo";
            string Condition = "";
            string OrderBy = "Con_Acc_ID desc";

            TDSPerMDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TDSPerMDT != null)
            {
                foreach (DataRow row in TDSPerMDT.Rows)
                {
                    EN.CommonAccountDets CommonAccountDetsList = new EN.CommonAccountDets();
                    CommonAccountDetsList.ConAccId = Convert.ToInt32(row["Con_Acc_ID"]);
                    CommonAccountDetsList.AccountNo = Convert.ToString(row["AccountNo"]);

                    CommonAccountDetsListM.Add(CommonAccountDetsList);
                }
            }
            return CommonAccountDetsListM;
        }

        public List<EN.WorkyearReceipt> getWorkyear()
        {
            List<EN.WorkyearReceipt> CommonAccountDetsListM = new List<EN.WorkyearReceipt>();
            DataTable TDSPerMDT = new DataTable();
            string Table = "WorkyearReceipt";
            string Column = "ID,WorkYear";
            string Condition = "";
            string OrderBy = "WorkYear desc";

            TDSPerMDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TDSPerMDT != null)
            {
                foreach (DataRow row in TDSPerMDT.Rows)
                {
                    EN.WorkyearReceipt CommonAccountDetsList = new EN.WorkyearReceipt();
                    
                    CommonAccountDetsList.WorkyearTDS = Convert.ToString(row["WorkYear"]);

                    CommonAccountDetsListM.Add(CommonAccountDetsList);
                }
            }
            return CommonAccountDetsListM;
        }

        public string mapCustomerWiseAutomailsave(string Activity, string Entrydate, string mailto, string mailtoID, string mailtoemailid,
           string cc, string Replyto, int IsActive, string entryid, int userid)
        {

            string message = "";

            DataTable ContainerDetailsDL = new DataTable();

            ContainerDetailsDL = TrackerManager.MapCustomerWiseAutomailsave(Activity, Entrydate, mailto, mailtoID, mailtoemailid, cc, Replyto, IsActive, entryid, userid);
            message = "Record saved successfully!";
            return message;
        }

        public string CheckMapEmilID(string mailto, string mailtoID)
        {

            List<EN.DriversDocumentDetailsEntiites> AttachmentList = new List<EN.DriversDocumentDetailsEntiites>();
            DataTable JOAttachmentDT = new DataTable();

            string message = "";
            JOAttachmentDT = TrackerManager.GetMapEmailidcheck(mailto, mailtoID);
            if (JOAttachmentDT != null)
            {
                message = Convert.ToString(JOAttachmentDT.Rows[0]["Count"]);
            }
            return message;
        }

        public string CheckMapEmil(int Userid, int MenuID)
        {

            List<EN.DriversDocumentDetailsEntiites> AttachmentList = new List<EN.DriversDocumentDetailsEntiites>();
            DataTable JOAttachmentDT = new DataTable();

            string message = "";
            JOAttachmentDT = TrackerManager.GetMapEmailid(Userid,20);
            if (JOAttachmentDT != null)
            {
                message = Convert.ToString(JOAttachmentDT.Rows[0]["Count"]);
            }
            return message;
        }

        public List<EN.exporterShipping> GetExporterShippingline()
        {
            List<EN.exporterShipping> CustomerDL = new List<EN.exporterShipping>();
            DataTable CustomerDT = new DataTable();
            string Table = "exp_shippers";
            string Column = "ShipperID,ShipperName";
            string Condition = "IsActive=1";
            string OrderBy = "AGName";

            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.exporterShipping CustomerList = new EN.exporterShipping();
                    CustomerList.Exportershippingid = Convert.ToInt32(row["ShipperID"]);
                    CustomerList.ExporterShippingname = Convert.ToString(row["ShipperName"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }


        
        public List<EN.PartyNameEntities> GetGstPartyName()
        {
            List<EN.PartyNameEntities> CustomerDL = new List<EN.PartyNameEntities>();
            DataTable CustomerDT = new DataTable();
            string Table = "party_gst_m";
            string Column = "DISTINCT Common_ID,GSTName";
            string Condition = " isnull(Common_ID,0) <> 0";
            string OrderBy = "GSTName";

            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    EN.PartyNameEntities CustomerList = new EN.PartyNameEntities();
                    CustomerList.Common_ID = Convert.ToInt32(row["Common_ID"]);
                    CustomerList.GSTName = Convert.ToString(row["GSTName"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }


        public List<EN.CategorywiseInvoice> ImportDirectAdjustments(DataTable Itemdata)
        {
            //string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            // parameterList.Add("TDSType", TDSType);
            int Count = 1;
            DataTable ds = new DataTable();

            ds = db.DataTableAddTypeTable("SP_ImportPDADJUSTMENT_Excel_Directadjustments", parameterList, Itemdata, "PT_ImportDirectadjustment", "@PT_ImportDirectadjustment");

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            List<EN.CategorywiseInvoice> VoucherdetailsLIst = new List<EN.CategorywiseInvoice>();

            if (ds.Rows.Count != 0)
            {
                foreach (DataRow row in ds.Rows)
                {
                    EN.CategorywiseInvoice rowdata = new EN.CategorywiseInvoice();
                    rowdata.SRNO = Convert.ToInt32(Count++);
                    rowdata.AssessNo = Convert.ToString(row["Assess No"]);
                    rowdata.AssessYear = Convert.ToString(row["Assess Year"]);
                    rowdata.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    rowdata.InvoiceDate = Convert.ToString(row["Invoice Date"]);
                    rowdata.InvoiceAmount = Convert.ToDouble(row["Invoice Amt"]);
                    rowdata.CreditAmount = Convert.ToDouble(row["Credit Amt"]);
                    rowdata.PrevReceiptAmount = Convert.ToDouble(row["Prev Received Amt"]);
                    rowdata.ReceivalAmount = Convert.ToDouble(row["Receival Amt"]);
                    rowdata.ReceivedAmount = Convert.ToDouble(row["Received Amt"]);
                    rowdata.TDS = Convert.ToDouble(row["TDS"]);
                    rowdata.NetReceivedAmount = Convert.ToDouble(row["Net Received"]);
                    rowdata.OS = Convert.ToDouble(row["OS"]);
                    rowdata.ReceiptNo = Convert.ToInt64(row["Receipt No"]);
                    rowdata.WorkYear = Convert.ToString(row["Work Year"]);
                    rowdata.AssessDate1 = Convert.ToString(row["Invoice Date"]);
                    rowdata.ReferenceNo = Convert.ToString(row["ReferenceNo"]);
                    VoucherdetailsLIst.Add(rowdata);
                }
            }
            return VoucherdetailsLIst;
        }


        public string SaveDirectAdjustmentDetails(DataTable Invoicedata, int Userid,string TDSWorkyear)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("Userid", Userid);
            parameterList.Add("TDSWorkyear", TDSWorkyear);
            int i = db.AddTypeTableData("USP_InsertIntoReceiptDirectAdjustments", parameterList, Invoicedata, "PT_AddReceiptDirectAdjustments", "@PT_AddReceiptDirectAdjustments");


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


        public string SaveAdjsuments(DataTable Invoicedata, int Userid,string TDSWorkyear)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

                parameterList.Add("Userid", Userid);
            parameterList.Add("TDSWorkyear", TDSWorkyear);
            int i = db.AddTypeTableData("SaveOnScreenAdjustment", parameterList, Invoicedata, "PT_OnScreenAdjustment", "@PT_OnScreenAdjustment");
             
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


        public List<EN.Customer> GetcustomerAutocompletedebit(string Prefix)
        {
            List<EN.Customer> CustomerDL = new List<EN.Customer>();
            DataTable CustomerDT = new DataTable();

            CustomerDT = TrackerManager.getAutoCustomerDetails(Prefix);
            if (CustomerDT != null)
            {
                if (CustomerDT.Rows.Count > 0)
                {
                    foreach (DataRow row in CustomerDT.Rows)
                    {
                        EN.Customer CustomerList = new EN.Customer();
                        CustomerList.AGID = Convert.ToInt32(row["AGID"]);
                        CustomerList.AGName = Convert.ToString(row["AGName"]);

                        CustomerDL.Add(CustomerList);
                    }
                }
                else
                {
                    EN.Customer CustomerList = new EN.Customer();
                    CustomerList.AGID = 0;
                    CustomerList.AGName = "No Data Found";

                    CustomerDL.Add(CustomerList);
                }

            }
            return CustomerDL;
        }

        public List<EN.GstDetails> GetDebitamountfor(string Prefix)
        {
            List<EN.GstDetails> CustomerDL = new List<EN.GstDetails>();
            DataTable CustomerDT = new DataTable();

            CustomerDT = TrackerManager.getAutodebitforcustomer(Prefix);
            if (CustomerDT != null)
            {
                if (CustomerDT.Rows.Count > 0)
                {
                    foreach (DataRow row in CustomerDT.Rows)
                    {
                        EN.GstDetails CustomerList = new EN.GstDetails();
                        CustomerList.GSTuniqueid = Convert.ToInt32(row["M_Common_ID"]);
                        CustomerList.GstuniqueName = Convert.ToString(row["Name"]);

                        CustomerDL.Add(CustomerList);
                    }
                }
                else
                {
                    EN.GstDetails CustomerList = new EN.GstDetails();
                    CustomerList.GSTuniqueid = 0;
                    CustomerList.GstuniqueName = "No Data Found";

                    CustomerDL.Add(CustomerList);
                }

            }
            return CustomerDL;
        }

        public string SavepartyDebitEntry(DataTable Invoicedata, string NetAmount, string CGST, string GST, string GrandTotal,
        string DebitAcoount, string Remark, string DebitID, string DebitDateList, string DebitReferenceNo, string GSTNo, string IGST, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("NetAmount", NetAmount);
            parameterList.Add("CGST", CGST);
            parameterList.Add("GST", GST);
            parameterList.Add("GrandTotal", GrandTotal);
            parameterList.Add("DebitAcoount", DebitAcoount);
            parameterList.Add("Remark", Remark);
            parameterList.Add("DebitID", DebitID);
            parameterList.Add("DebitDate", Convert.ToDateTime(DebitDateList).ToString("yyyy-MM-dd HH:mm:ss"));

            parameterList.Add("Userid", Userid);
            parameterList.Add("DebitReferenceNo", DebitReferenceNo);
            parameterList.Add("GSTNo", GSTNo);
            parameterList.Add("IGST", IGST);
            int i = db.AddTypeTableData("USP_InsertIntoCustDebitDetails", parameterList, Invoicedata, "PT_AddDebitDetails", "@PT_AddDebitDetails");


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

        public string GetGSTDetails(string gstID)
        {


            DataTable JOAttachmentDT = new DataTable();

            string message = "";
            JOAttachmentDT = TrackerManager.GetGSTDetailsForCustomer(gstID);
            if (JOAttachmentDT.Rows.Count > 0)
            {
                message = Convert.ToString(JOAttachmentDT.Rows[0]["GSTNo"]);
            }
            else
            {
                message = "";

            }
            return message;
        }
        public string CancelDebitDetails(string debitnoteno, int Userid)
        {


            DataTable JOAttachmentDT = new DataTable();

            string message = "";
            JOAttachmentDT = TrackerManager.CancelDebitdetails(debitnoteno, Userid);

            return message;
        }
        ///
        public string MapCustomerWiseAutomailsaveMap(string Activity, string Entrydate, string mailto, string mailtoID, string mailtoemailid,
          string cc, string Replyto, int IsActive, string entryid, int userid)
        {

            string message = "";

            DataTable ContainerDetailsDL = new DataTable();

            ContainerDetailsDL = TrackerManager.MapCustomerWiseAutomailsaveMap(Activity, Entrydate, mailto, mailtoID, mailtoemailid, cc, Replyto, IsActive, entryid, userid);
            message = "Record saved successfully!";
            return message;
        }
        public string CheckMapEmilIDs(string mailto, string mailtoID)
        {

            List<EN.DriversDocumentDetailsEntiites> AttachmentList = new List<EN.DriversDocumentDetailsEntiites>();
            DataTable JOAttachmentDT = new DataTable();

            string message = "";
            JOAttachmentDT = TrackerManager.GetMapEmailidcheckss(mailto, mailtoID);
            if (JOAttachmentDT != null)
            {
                message = Convert.ToString(JOAttachmentDT.Rows[0]["Count"]);
            }
            return message;
        }

        public string MapMailSave(string mailtoID, string mailto, string mailcc, int userid, string Entryid, string mailtoemailid)
        {


            string message = "";
            DataTable ContainerDetailsDL1 = new DataTable();
            DataTable CheckUser = new DataTable();

             
                ContainerDetailsDL1 = TrackerManager.MapMailSave(mailtoID, mailto, mailcc, userid, Entryid, mailtoemailid);
                message = "Record saved successfully!";
            
            
            return message;
        }
        public string CheckDuplicateMailID(string mailto, string mailtoID, string Entryid)
        {

            string message = "";
            DataTable ContainerDetailsDL1 = new DataTable();
            DataTable CheckUser = new DataTable();

            CheckUser = TrackerManager.CheckDuplicateDetailsMail(mailto, mailtoID, Entryid);
            if (CheckUser.Rows.Count > 0)
            {
                message = Convert.ToString(CheckUser.Rows[0]["Count"]);
            }
            else
            {
                message = "";
            }
            return message;
        }

        public string AddDirectFuel(string JONo, DataTable Containerdetails)
        {
            string message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            for (int i = 0; i < Containerdetails.Rows.Count; i++)
            {
                DataTable PortsDS = new DataTable();
                PortsDS = db.sub_GetDatatable("ContainerExistValidation_DB '" + Containerdetails.Rows[i].Field<string>("ContainerNo") + "','" + JONo + "'");


                if (PortsDS.Rows.Count > 0)
                {
                    message += "" + PortsDS.Rows[0][0] + "" + "";
                }
                else
                {
                    message += "";
                }

            }


            return message;

        }
        public string AddDirectSize(string JONo, DataTable Containerdetails)
        {
            string message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            for (int i = 0; i < Containerdetails.Rows.Count; i++)
            {
                DataTable PortsDS = new DataTable();
                PortsDS = db.sub_GetDatatable("USP_CheckSize_Joborder '" + Containerdetails.Rows[i].Field<string>("ContainerNo") + "','" + Containerdetails.Rows[i].Field<string>("Size") + "'");


                if (PortsDS.Rows.Count > 0)
                {
                    message += "" + PortsDS.Rows[0][0] + "" + "";
                }
                else
                {
                    message += "";
                }

            }


            return message;

        }
        public string CheckalreadyimportIn(long jono)
        {

            string message = "";
            DataTable ContainerDetailsDL1 = new DataTable();
            DataTable CheckUser = new DataTable();

            CheckUser = TrackerManager.CheckalreadyimportIn(jono);
            if (CheckUser.Rows.Count > 0)
            {
                message = "1";
            }
            else
            {
                message = "0";
            }
            return message;
        }



        public List<EN.ContainerType> ContainerType()
        {
            List<EN.ContainerType> ContainerType = new List<EN.ContainerType>();
            DataTable stuffdt = new DataTable();
            string Table = "containertype";
            string Column = "Distinct ContainerTypeID,ContainerType";
            string Condition = "ctr_Is_Active=1";
            string OrderBy = "ContainerType";

            stuffdt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (stuffdt != null)
            {
                foreach (DataRow row in stuffdt.Rows)
                {
                    EN.ContainerType CommoList = new EN.ContainerType();
                    CommoList.ContainerTypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    CommoList.ContainerTypeName = Convert.ToString(row["ContainerType"]);
                    ContainerType.Add(CommoList);
                }
            }
            return ContainerType;
        }
        public List<EN.Condition> GetCondition()        {            List<EN.Condition> CustomerDL = new List<EN.Condition>();            DataTable CustomerDT = new DataTable();            string Table = "Condition";            string Column = "ID,Name";            string Condition = "ID IN (8, 11, 12)";            string OrderBy = "Name";            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);            if (CustomerDT != null)            {                foreach (DataRow row in CustomerDT.Rows)                {                    EN.Condition CustomerList = new EN.Condition();                    CustomerList.ConditionID = Convert.ToInt32(row["ID"]);                    CustomerList.ConditionName = Convert.ToString(row["Name"]);                    CustomerDL.Add(CustomerList);                }            }            return CustomerDL;        }
        public List<EN.Location> GetLocation()        {            List<EN.Location> CustomerDL = new List<EN.Location>();            DataTable CustomerDT = new DataTable();            string Table = "ext_location_m";            string Column = "locationid,location";            string Condition = "IsActive=1";            string OrderBy = "location";            CustomerDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);            if (CustomerDT != null)            {                foreach (DataRow row in CustomerDT.Rows)                {                    EN.Location CustomerList = new EN.Location();                    CustomerList.LocationID = Convert.ToInt32(row["locationid"]);                    CustomerList.LocationName = Convert.ToString(row["location"]);                    CustomerDL.Add(CustomerList);                }            }            return CustomerDL;        }
        public EN.ModifyImpContainer GetContainerWiseDetails(string ContainerNo)
        {
            EN.ModifyImpContainer JE = new EN.ModifyImpContainer();
            DataTable VesselDT = new DataTable();
            VesselDT = TrackerManager.GetImpJoDetails(ContainerNo);
            foreach (DataRow row in VesselDT.Rows)
            {
                JE.ContainerTypeID = Convert.ToInt16(row["ContainerTypeID"]);
                JE.Cargotypeid = Convert.ToInt16(row["Cargotypeid"]);
                JE.Qty = Convert.ToInt32(row["Qty"]);

                JE.BL_Number = Convert.ToString(row["BL Number"]);
                JE.Tare_Weight = Convert.ToInt32(row["Tare_Weight"]);
                JE.GrossWT = Convert.ToInt32(row["GrossWT"]);
                JE.SealNo = Convert.ToString(row["SealNo"]);
                JE.ISOID = Convert.ToInt32(row["ISOID"]);
                JE.Size = Convert.ToInt32(row["Size"]);
                JE.JoTypeId = Convert.ToInt32(row["JoTypeId"]);

            }

            return JE;
        }
        public EN.Response UpdateImportJoDet(EN.ModifyImpContainer data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();
                // var Date = Convert.ToDateTime(data.ETA).ToString("yyyy-MM-dd");
                list = TrackerManager.SaveDetailsDL(data.ContainerNo, data.ContainerTypeID, data.JoTypeId, data.Size, data.Cargotypeid, data.SealNo, data.BL_Number, data.Qty, data.GrossWT, data.ISOID, data.Tare_Weight, data.Remarks, data.AddedBy);

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
        // code by pK




        public List<EN.UploadDomesticHUBCargo> GetUploadingHubData(DataTable domesticHub)
        {
            try
            {



                List<EN.UploadDomesticHUBCargo> GetData = new List<EN.UploadDomesticHUBCargo>();
                if (domesticHub != null)
                {
                    int SrNo = 0;
                    foreach (DataRow row in domesticHub.Rows)
                    {
                        EN.UploadDomesticHUBCargo SingleExcelData = new EN.UploadDomesticHUBCargo();
                        SrNo++;
                        SingleExcelData.SrNo = SrNo;
                        SingleExcelData.WagonNo = Convert.ToString(row["WagonNo"]);
                        SingleExcelData.ContainerNo = Convert.ToString(row["ContainerNo"]);
                        SingleExcelData.DisplaySize = Convert.ToString(row["Size"]);
                        SingleExcelData.Stats = Convert.ToString(row["Stats"]);
                        SingleExcelData.Stack = Convert.ToString(row["Stack"]);
                        SingleExcelData.TriffType = Convert.ToString(row["TriffType"]);
                        SingleExcelData.SealNo = Convert.ToString(row["SealNo"]);
                        SingleExcelData.LRNo = Convert.ToString(row["LRNo"]);
                        SingleExcelData.DisplayTareWt = Convert.ToString(row["TareWt"]);
                        SingleExcelData.DisplayCargoWt = Convert.ToString(row["CargoWt"]);
                        SingleExcelData.OrderNo = Convert.ToString(row["OrderNo"]);
                        SingleExcelData.Shipment = Convert.ToString(row["Shipment"]);
                        SingleExcelData.DES = Convert.ToString(row["DES"]);
                        SingleExcelData.Grade = Convert.ToString(row["Grade"]);
                        SingleExcelData.TaxInvNo = Convert.ToString(row["TaxInvNo"]);
                        SingleExcelData.DisplayTaxInvDate = Convert.ToString(row["TaxInvDate"]);
                        SingleExcelData.DeliveryNo = Convert.ToString(row["DeliveryNo"]);
                        SingleExcelData.DCPINo = Convert.ToString(row["DCPINo"]);
                        SingleExcelData.EWayBillNo = Convert.ToString(row["EWayBillNo"]);
                        SingleExcelData.DisplayEWayBillValidUpTo = Convert.ToString(row["EWayBillValidUpTo"]);
                        SingleExcelData.ShipToParty = Convert.ToString(row["ShipToParty"]);
                        SingleExcelData.Destination = Convert.ToString(row["Destination"]);

                        GetData.Add(SingleExcelData);

                    }
                }
                return GetData;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EN.ResponseMessage InsertExcelDomesticHubData(DataTable domesticHubData, int AddedBy, DateTime AddedOn)
        {
            try
            {


                EN.ResponseMessage responseMessage = new EN.ResponseMessage();
                DataTable Result = TrackerManager.InsertExcelDomesticHubData(domesticHubData, AddedBy, AddedOn);
                if (Result != null)
                {
                    foreach (DataRow row in Result.Rows)
                    {
                        responseMessage.Status = Convert.ToInt32(row["Status"]);
                        responseMessage.Message = Convert.ToString(row["message"]);
                    }

                }
                return responseMessage;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<EN.LocationM> GetELocationame()
        {

            try
            {


                List<EN.LocationM> Location = new List<EN.LocationM>();
                DataTable Result = TrackerManager.GetELocationame();
                if (Result != null)
                {
                    foreach (DataRow row in Result.Rows)
                    {
                        EN.LocationM item = new EN.LocationM();
                        item.ID = Convert.ToInt32(row["LocationID"]);
                        item.Location = Convert.ToString(row["Location"]);
                        Location.Add(item);
                    }

                }
                return Location;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public EN.DomesticHubOffLoadDts GetDetailsContainerWise(string containerNo)
        {
            try
            {


                EN.DomesticHubOffLoadDts item = new EN.DomesticHubOffLoadDts();
                DataTable Result = TrackerManager.GetDetailsContainerWise(containerNo);
                if (Result != null)
                {
                    foreach (DataRow row in Result.Rows)
                    {

                        item.LRNo = Convert.ToString(row["LRNo"]);
                        item.Size = Convert.ToInt32(row["Size"]);
                        item.EntryID = Convert.ToInt32(row["entryId"]);

                    }

                }
                return item;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public EN.ResponseMessage SaveDomesticHubOffLoading(EN.DomesticHubOffLoadDts hubOffLoadingData, int Userid)
        {
            try
            {


                EN.ResponseMessage responseMessage = new EN.ResponseMessage();
                DataTable Result = TrackerManager.SaveDomesticHubOffLoading(hubOffLoadingData, Userid);
                if (Result != null)
                {
                    foreach (DataRow row in Result.Rows)
                    {
                        responseMessage.Status = Convert.ToInt32(row["Status"]);
                        responseMessage.Message = Convert.ToString(row["message"]);
                    }

                }
                return responseMessage;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public EN.ResponseMessage SaveJVDetails(string CustomerName, string CustomerName2, string PendingReceipt, string TransAmt, string Remarks, int Userid,string EntryType,string Criteria,string JVFor)
        {
            EN.ResponseMessage Response = new EN.ResponseMessage();
            try
            {

                DataTable JvDL = new DataTable();
                JvDL = TrackerManager.SaveJVDetails(CustomerName, CustomerName2, PendingReceipt, TransAmt, Remarks, Userid, EntryType, Criteria, JVFor);


                if (JvDL != null)
                {

                    foreach (DataRow row in JvDL.Rows)
                    {

                        Response.Status = Convert.ToInt32(row["Status"]);
                        Response.Message = Convert.ToString(row["message"]);
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Status = 0;
                Response.Message = ex.Message;
            }
            return Response;
        }
        public string GetValidateReceipt(DataTable Itemdata, int UserID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            // parameterList.Add("TDSType", TDSType);

            DataTable ds = new DataTable();

            DataTable VendorDataDL = new DataTable();
            parameterList.Add("Userid", UserID);
            VendorDataDL = db.DataTableAddTypeTable("USP_Validate_Receipt", parameterList, Itemdata, "PT_ImportDirectadjustment", "@PT_ImportDirectadjustment");

            if (VendorDataDL.Rows.Count > 0)
            {
                foreach (DataRow row in VendorDataDL.Rows)
                {

                    Message = Convert.ToString(VendorDataDL.Rows[0][0]);

                }
            }
            return Message;
        }



        public EN.Response UpdateCRNRem(EN.DomesticRemarks data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();
                // var Date = Convert.ToDateTime(data.ETA).ToString("yyyy-MM-dd");
                list = TrackerManager.SaveDetailsCRDL(data.InvoiceNo, data.Remarks, data.AddedBy);

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



        public EN.Response CancelExportContainer(EN.ModifyEXPContainer data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();
                // var Date = Convert.ToDateTime(data.ETA).ToString("yyyy-MM-dd");
                list = TrackerManager.DeleteContainerExport(data.EntryID, data.ContainerNo, data.Remarks, data.AddedBy);

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
        public EN.IGMDetailsData GetValidationForExcelData(DataTable DischargeListDT)
        {
            int i = 0;
            EN.IGMDetailsEntities QuoteDataList = new EN.IGMDetailsEntities();
            List<EN.IGMDetailsEntities> QuoteSystemList = new List<EN.IGMDetailsEntities>();

            DataSet Master = new DataSet();
            DataTable QuoteData = new DataTable();
            DataTable SystemList = new DataTable();

            //Master = DBManager.GetValidationForExcelData(DischargeListDT);
            //QuoteData = Master.Tables[0];
            //SystemList = Master.Tables[1];

            SystemList = DischargeListDT;


 
            if (SystemList != null)
            {
                foreach (DataRow row in SystemList.Rows)
                {
                    EN.IGMDetailsEntities item = new EN.IGMDetailsEntities();
                    i++;

                    item.SrNo = i.ToString();
                    item.SpecID = i;
                    //item.JoNo = Convert.ToString(row["JoNo"]);
                    item.Origin = Convert.ToString(row["Origin"]);
                    item.IGMNo = Convert.ToString(row["IGMNo"]);
                    item.ItemNo = Convert.ToString(row["ItemNo"]);
                    item.BLNo = Convert.ToString(row["BLNo"]);
                    item.BLDate = Convert.ToString(row["BLDate"]);
                    item.Container_No = Convert.ToString(row["ContainerNo"]);

                    item.SealNo1 = Convert.ToString(row["SealNo"]);
                    item.PKGS1 = Convert.ToString(row["PKGS"]);
                    item.PKGSType = Convert.ToString(row["PKGSType"]);
                    item.CargoWeight = Convert.ToString(row["CargoWeight"]);
                    item.Consignee = Convert.ToString(row["Consignee"]);
                    item.Address = Convert.ToString(row["Address"]);
                    item.NotifyConsignee = Convert.ToString(row["NotifyConsignee"]);

                    item.NotifyAddress = Convert.ToString(row["NotifyAddress"]);
                    item.GoodsDescription = Convert.ToString(row["GoodsDescription"]);


                    QuoteSystemList.Add(item);

                }
            }

            EN.IGMDetailsData Data = new EN.IGMDetailsData();

            //Data.QuoteData = QuoteDataList;
            Data.QuoteSystemList = QuoteSystemList;
            return Data;

        }
        public string SaveImportIGMDetails(DataTable IGMElementsdataTable, int userId)
        {
            string message = "";
            try
            {

                TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


                for (int i = 0; i < IGMElementsdataTable.Rows.Count; i++)
                {
                    DataTable PortsDS = new DataTable();
                    PortsDS = db.sub_GetDatatable("USP_GetIGMFileDuplicate '" + IGMElementsdataTable.Rows[i][1] + "','" + IGMElementsdataTable.Rows[i][2] + "','" + IGMElementsdataTable.Rows[i][5] + "'");


                    if (PortsDS.Rows.Count > 0)
                    {
                        message += "Duplicate Container no : '" + IGMElementsdataTable.Rows[i][5] + "' found again IGM No : '" + IGMElementsdataTable.Rows[i][1] + "' and Item No : '" + IGMElementsdataTable.Rows[i][2] + "'";
                    }
                    else
                    {
                        message += "";
                    }

                }



                if (message == "")
                {
                    DataTable JobOrderDT = new DataTable();
                    JobOrderDT = TrackerManager.SaveImportIGMDetails(IGMElementsdataTable, userId);
                    if (JobOrderDT != null)
                    {
                        foreach (DataRow row in JobOrderDT.Rows)
                        {

                            message = Convert.ToString(row["Message"]);

                        }
                    }


                }




                return message;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

    }
}
