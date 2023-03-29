using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;


namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ImportInvoice
{
    public class ImportInvoice
    {
        DB.TrackerMVCDBManager importDB = new DB.TrackerMVCDBManager();
        public BE.ProformaEntities ProformaFillDropDownList()
        {
            BE.ProformaEntities ProformaDDList = new BE.ProformaEntities();
            List<BE.DeliveryProformaEntities> DeliveryList = new List<BE.DeliveryProformaEntities>();
            List<BE.InvoiceProformaEntities> InvoiceList = new List<BE.InvoiceProformaEntities>();
            List<BE.CustomerProformaEntities> CustomerList = new List<BE.CustomerProformaEntities>();
            List<BE.ShippingLineProformaEntities> LineList = new List<BE.ShippingLineProformaEntities>();
            List<BE.CHAProformaEntities> CHAList = new List<BE.CHAProformaEntities>();
            List<BE.ImporterProformaEntities> ImporterList = new List<BE.ImporterProformaEntities>();
            List<BE.MovementProformaEntities> MovementList = new List<BE.MovementProformaEntities>();
            List<BE.CommodityProformaEntities> CommodityList = new List<BE.CommodityProformaEntities>();
            List<BE.HaulageProformaEntities> HaulageList = new List<BE.HaulageProformaEntities>();
            List<BE.YardLocationProformaEntities> YardLocationList = new List<BE.YardLocationProformaEntities>();
            List<BE.TariffForProformaEntities> TariffForList = new List<BE.TariffForProformaEntities>();
            List<BE.TariffProformaEntities> TariffList = new List<BE.TariffProformaEntities>();
            List<BE.AccountProformaEntities> AccountList = new List<BE.AccountProformaEntities>();

            DataSet ds = new DataSet();
            ds = importDB.GetDropDownListProformaInvoice();

            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        BE.DeliveryProformaEntities Delivery = new BE.DeliveryProformaEntities();
                        Delivery.ID = Convert.ToString(row["ID"]);
                        Delivery.Name = Convert.ToString(row["Name"]);

                        DeliveryList.Add(Delivery);
                    }
                }
            }
            if (ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        BE.InvoiceProformaEntities Invoice = new BE.InvoiceProformaEntities();
                        Invoice.ID = Convert.ToString(row["ID"]);
                        Invoice.Name = Convert.ToString(row["Name"]);

                        InvoiceList.Add(Invoice);
                    }
                }
            }
            if (ds.Tables[2] != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        BE.CustomerProformaEntities Customer = new BE.CustomerProformaEntities();
                        Customer.ID = Convert.ToInt16(row["ID"]);
                        Customer.Name = Convert.ToString(row["Name"]);

                        CustomerList.Add(Customer);
                    }
                }
            }
            if (ds.Tables[3] != null)
            {
                if (ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[3].Rows)
                    {
                        BE.ShippingLineProformaEntities ShippingLine = new BE.ShippingLineProformaEntities();
                        ShippingLine.ID = Convert.ToInt16(row["ID"]);
                        ShippingLine.Name = Convert.ToString(row["Name"]);

                        LineList.Add(ShippingLine);
                    }
                }
            }
            if (ds.Tables[4] != null)
            {
                if (ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[4].Rows)
                    {
                        BE.CHAProformaEntities CHA = new BE.CHAProformaEntities();
                        CHA.ID = Convert.ToInt16(row["ID"]);
                        CHA.Name = Convert.ToString(row["Name"]);

                        CHAList.Add(CHA);
                    }
                }
            }
            if (ds.Tables[5] != null)
            {
                if (ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[5].Rows)
                    {
                        BE.ImporterProformaEntities Importer = new BE.ImporterProformaEntities();
                        Importer.ID = Convert.ToInt16(row["ID"]);
                        Importer.Name = Convert.ToString(row["Name"]);

                        ImporterList.Add(Importer);
                    }
                }
            }
            if (ds.Tables[6] != null)
            {
                if (ds.Tables[6].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[6].Rows)
                    {
                        BE.MovementProformaEntities Movement = new BE.MovementProformaEntities();
                        Movement.ID = Convert.ToString(row["ID"]);
                        Movement.Name = Convert.ToString(row["Name"]);

                        MovementList.Add(Movement);
                    }
                }
            }
            if (ds.Tables[7] != null)
            {
                if (ds.Tables[7].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[7].Rows)
                    {
                        BE.CommodityProformaEntities Commodity = new BE.CommodityProformaEntities();
                        Commodity.ID = Convert.ToInt16(row["ID"]);
                        Commodity.Name = Convert.ToString(row["Name"]);

                        CommodityList.Add(Commodity);
                    }
                }
            }
            if (ds.Tables[8] != null)
            {
                if (ds.Tables[8].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[8].Rows)
                    {
                        BE.HaulageProformaEntities Haulage = new BE.HaulageProformaEntities();
                        Haulage.ID = Convert.ToInt16(row["ID"]);
                        Haulage.Name = Convert.ToString(row["Name"]);

                        HaulageList.Add(Haulage);
                    }
                }
            }
            if (ds.Tables[9] != null)
            {
                if (ds.Tables[9].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[9].Rows)
                    {
                        BE.YardLocationProformaEntities YardLocation = new BE.YardLocationProformaEntities();
                        YardLocation.ID = Convert.ToInt16(row["ID"]);
                        YardLocation.Name = Convert.ToString(row["Name"]);

                        YardLocationList.Add(YardLocation);
                    }
                }
            }
            if (ds.Tables[10] != null)
            {
                if (ds.Tables[10].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[10].Rows)
                    {
                        BE.TariffForProformaEntities TariffFor = new BE.TariffForProformaEntities();
                        TariffFor.ID = Convert.ToString(row["ID"]);
                        TariffFor.Name = Convert.ToString(row["Name"]);

                        TariffForList.Add(TariffFor);
                    }
                }
            }
            if (ds.Tables[11] != null)
            {
                if (ds.Tables[11].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[11].Rows)
                    {
                        BE.TariffProformaEntities Tariff = new BE.TariffProformaEntities();
                        Tariff.ID = Convert.ToInt16(row["ID"]);
                        Tariff.Name = Convert.ToString(row["Name"]);

                        TariffList.Add(Tariff);
                    }
                }
            }
            if (ds.Tables[12] != null)
            {
                if (ds.Tables[12].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[12].Rows)
                    {
                        BE.AccountProformaEntities Account = new BE.AccountProformaEntities();
                        Account.ID = Convert.ToInt16(row["ID"]);
                        Account.Name = Convert.ToString(row["Name"]);

                        AccountList.Add(Account);
                    }
                }
            }
            ProformaDDList.DeliveryProformaEntities = DeliveryList;
            ProformaDDList.InvoiceProformaEntities = InvoiceList;
            ProformaDDList.CustomerProformaEntities = CustomerList;
            ProformaDDList.ShippingLineProformaEntities = LineList;
            ProformaDDList.CHAProformaEntities = CHAList;
            ProformaDDList.ImporterProformaEntities = ImporterList;
            ProformaDDList.MovementProformaEntities = MovementList;
            ProformaDDList.CommodityProformaEntities = CommodityList;
            ProformaDDList.HaulageProformaEntities = HaulageList;
            ProformaDDList.YardLocationProformaEntities = YardLocationList;
            ProformaDDList.TariffForProformaEntities = TariffForList;
            ProformaDDList.TariffProformaEntities = TariffList;
            ProformaDDList.AccountProformaEntities = AccountList;

            return ProformaDDList;
        }
        public List<BE.ImportProformaSearchGST> GetGSTList(string SearchText)
        {
            DataTable codeDL = new DataTable();
            codeDL = importDB.GetGSTSearchImportProforma(SearchText);
            List<BE.ImportProformaSearchGST> isCHACode = new List<BE.ImportProformaSearchGST>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    BE.ImportProformaSearchGST oper = new BE.ImportProformaSearchGST();
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


      

       


        public List<BE.ImportInvoiceContainerDetails> GetAdditionalcharges(string workyear, string IgmN0, string ItemNo)
        {
            DataTable codeDL = new DataTable();
            codeDL = importDB.GetadditionialChanrgesdetails(workyear, IgmN0, ItemNo);
            List<BE.ImportInvoiceContainerDetails> isCHACode = new List<BE.ImportInvoiceContainerDetails>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    BE.ImportInvoiceContainerDetails oper = new BE.ImportInvoiceContainerDetails();
                   
                    oper.JONo = Convert.ToString(row["Jo No"]);
                    oper.ContainerNo = Convert.ToString(row["Container No"]);
                    oper.Size = Convert.ToString(row["Size"]);
                    oper.Cargotype = Convert.ToString(row["Cargo Type"]);
                    oper.Amount = Convert.ToString(row["Amount"]);
                    
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }

        public string Saveadditionalcharges(DataTable Invoicedata  , string WorkorderNo, string Workorderyear,
            string accountheadID, string additionnarritioin, string AdditionalIGmno, string additionalItem, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("WorkorderNo", WorkorderNo);
            parameterList.Add("Workorderyear", Workorderyear);
            parameterList.Add("accountheadID", accountheadID);
            parameterList.Add("additionnarritioin", additionnarritioin);
            parameterList.Add("AdditionalIGmno", AdditionalIGmno);
            parameterList.Add("additionalItem", additionalItem);
            parameterList.Add("Userid", Userid);
            int i = db.AddTypeTableData("SP_SaveimportAccountCWeb", parameterList, Invoicedata, "PT_Addadditionaldetails", "@PT_Addadditionaldetails");


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


        public List<BE.ImportInvoiceContainerDetails> GetUpdateRmsDetails(string IgmN0, string ItemNo)
        {
            DataTable codeDL = new DataTable();
            codeDL = importDB.GetUpdateRmsDetails(IgmN0, ItemNo);
            List<BE.ImportInvoiceContainerDetails> isCHACode = new List<BE.ImportInvoiceContainerDetails>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    BE.ImportInvoiceContainerDetails oper = new BE.ImportInvoiceContainerDetails();

                    oper.JONo = Convert.ToString(row["Jo No"]);
                    oper.ContainerNo = Convert.ToString(row["Container No"]);
                    oper.Size = Convert.ToString(row["Size"]);
                    oper.Cargotype = Convert.ToString(row["Cargo Type"]);
                    oper.RMS = Convert.ToString(row["RMS Per"]);

                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }


        public string SaveUpdateRmsDetails(DataTable Invoicedata, string UpdateRmsIGmNo, string UpdateRmsItemno, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("IGMNo", UpdateRmsIGmNo);
            parameterList.Add("ItemNo", UpdateRmsItemno);
           
            parameterList.Add("Userid", Userid);
            int i = db.AddTypeTableData("SP_UpdateCustomExamineWeb", parameterList, Invoicedata, "PT_AddUpdateRmsdetails", "@PT_AddUpdateRmsdetails");


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

        public List<BE.Customer> getParty()
        {
            List<BE.Customer> CustomerDL = new List<BE.Customer>();
            DataTable CustomerDT = new DataTable();
            string Table = "Party_GST_M";
            string Column = "isnull(Common_ID,0)Common_ID,GSTName";
            string Condition = "";
            string OrderBy = "GSTName";

            CustomerDT = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    BE.Customer CustomerList = new BE.Customer();
                    CustomerList.AGID = Convert.ToInt32(row["Common_ID"]);
                    CustomerList.AGName = Convert.ToString(row["GSTName"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }

        public List<BE.CargoType> getContainerTypeDetails()
        {
            List<BE.CargoType> CustomerDL = new List<BE.CargoType>();
            DataTable CustomerDT = new DataTable();
            string Table = "CargoType";
            string Column = "Cargotypeid,CargoType";
            string Condition = "";
            string OrderBy = "CargoType";

            CustomerDT = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    BE.CargoType CustomerList = new BE.CargoType();
                    CustomerList.Cargotypeid = Convert.ToInt32(row["Cargotypeid"]);
                    CustomerList.Cargotype = Convert.ToString(row["CargoType"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }

        public List<BE.ISOCodes> GetISOCodetails()
        {
            List<BE.ISOCodes> CustomerDL = new List<BE.ISOCodes>();
            DataTable CustomerDT = new DataTable();
            string Table = "ISOCodes";
            string Column = "ISOID,ISOCode";
            string Condition = "";
            string OrderBy = "ISOCode";

            CustomerDT = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustomerDT != null)
            {
                foreach (DataRow row in CustomerDT.Rows)
                {
                    BE.ISOCodes CustomerList = new BE.ISOCodes();
                    CustomerList.ISOID = Convert.ToInt32(row["ISOID"]);
                    CustomerList.ISOCode = Convert.ToString(row["ISOCode"]);

                    CustomerDL.Add(CustomerList);
                }
            }
            return CustomerDL;
        }


        public string UpdateCargoDetailsInvoiceProforma(string IGmNo, string ItemNo, string Consignee, string Con_IGMAddress,
            string NConsignee, string NCon_IGMAddres, string IGM_BLNo, string IGM_GoodsDesc, string IGM_GrossWt, string IGM_WtUnit,
            string IGM_Qty, string IGM_PackType, string Remarks,int userId)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = importDB.UpdateCargoTypeDetails(IGmNo, ItemNo, Consignee, Con_IGMAddress, NConsignee, NCon_IGMAddres,
                IGM_BLNo, IGM_GoodsDesc, IGM_GrossWt, IGM_WtUnit, IGM_Qty, IGM_PackType, Remarks, userId);
           
            message = "Records Updated Successfully!";
            return message;
        }

        public string UpdateCargoDetails(DataTable Invoicedata, string IGmNo, string ItemNo, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            parameterList.Add("IGmNo", IGmNo);
            parameterList.Add("ItemNo", ItemNo);

            parameterList.Add("Userid", Userid);
            int i = db.AddTypeTableData("USP_UPDATEJOBORDER_SIZE_WEB", parameterList, Invoicedata, "UpdateCargoTypeDetails", "@UpdateCargoTypeDetails");


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

        public string CancelInvoicePorforma(string AssessNo, string workyear, int userId)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = importDB.CancelInvoiceProforma(AssessNo, workyear, userId);

            message = "Records Cancel Successfully!";
            return message;
        }
        public string SubmitDetailsforporforma(string AssessNo, string workyear, int userId)
        {

            string message = "";
            string message1 = "";
            DataTable updateDl = new DataTable();
            DataTable updateD2 = new DataTable();
       
            updateDl = importDB.SubmitInvoiceDetails(AssessNo, workyear, userId);
        

            if (message == "3")
            {
                updateD2 = importDB.gettheInvoicedocseries();
                message1= Convert.ToString(updateD2.Rows[0][0]);
            }
            else
            {
                updateD2 = importDB.gettheInvoicedocseries2();
                if (updateD2.Rows.Count > 0)
                {
                    message1 = Convert.ToString(updateD2.Rows[0][0]);
                }
                
            }
            return message1;
        }

        public int getallowid(string AssessNo, string workyear, int userId)
        {

            int message = 0;
            DataTable updateDl = new DataTable();
            updateDl = importDB.GetMaxallowid(AssessNo, workyear, userId);
            message = Convert.ToInt32(updateDl.Rows[0][0]);

            return message;
        }


        public string SubmitFinalDetails(string AssessNo, string workyear, int userId, string transid, string AssessType)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = importDB.SubmitFinalInvoiceproforma(AssessNo, workyear, userId, transid, AssessType);

            message = "Final invoice is generated successfully!";
            return message;
        }



        //Code For Tariff Setting Moduel
        public BE.ImportTraiffSettingEntities ImporttrariffSettingDropDown()
        {
            BE.ImportTraiffSettingEntities ImporttariffSettingID = new BE.ImportTraiffSettingEntities();
            List<BE.ImportaccountMaster> ImportaccountMaster = new List<BE.ImportaccountMaster>();
            List<BE.CommodityMaster> CommodityList = new List<BE.CommodityMaster>();
            List<BE.ImportInvoiceType> ImportInvoiceList = new List<BE.ImportInvoiceType>();
            List<BE.ChagresBasedOn> ChargesbasedList = new List<BE.ChagresBasedOn>();
            List<BE.SettingTax> SettingTaxList = new List<BE.SettingTax>();
            List<BE.TransportType_m> TransportList = new List<BE.TransportType_m>();
            List<BE.ImportJoType> ImportJoList = new List<BE.ImportJoType>();
            List<BE.PortsEntites> PortList = new List<BE.PortsEntites>();
            List<BE.CargoEntititesList> CargoList = new List<BE.CargoEntititesList>();
         

            DataSet ds = new DataSet();
            ds = importDB.GetDropDownListImporttariffsetting();

            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        BE.ImportaccountMaster Account = new BE.ImportaccountMaster();
                        Account.AccountID = Convert.ToInt32(row["AccountID"]);
                        Account.AccountName = Convert.ToString(row["AccountName"]);

                        ImportaccountMaster.Add(Account);
                    }
                }
            }
            if (ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        BE.CommodityMaster Commodity = new BE.CommodityMaster();
                        Commodity.Commodity_Group_ID = Convert.ToInt32(row["Commodity_Group_ID"]);
                        Commodity.Commodity_Group_Name = Convert.ToString(row["Commodity_Group_Name"]);

                        CommodityList.Add(Commodity);
                    }
                }
            }
            if (ds.Tables[2] != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        BE.ImportInvoiceType ImportInvoice = new BE.ImportInvoiceType();
                        ImportInvoice.ID = Convert.ToInt16(row["ID"]);
                        ImportInvoice.InvoiceType = Convert.ToString(row["Invoice_type"]);

                        ImportInvoiceList.Add(ImportInvoice);
                    }
                }
            }
            if (ds.Tables[3] != null)
            {
                if (ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[3].Rows)
                    {
                        BE.ChagresBasedOn Chargesbased = new BE.ChagresBasedOn();
                        Chargesbased.Chargeid = Convert.ToInt16(row["ID"]);
                        Chargesbased.BasedOn = Convert.ToString(row["basedon"]);

                        ChargesbasedList.Add(Chargesbased);
                    }
                }
            }
            if (ds.Tables[4] != null)
            {
                if (ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[4].Rows)
                    {
                        BE.SettingTax SettingTax = new BE.SettingTax();
                        SettingTax.Settingid = Convert.ToInt16(row["settingsid"]);
                        SettingTax.TaxName = Convert.ToString(row["taxname"]);

                        SettingTaxList.Add(SettingTax);
                    }
                }
            }
            if (ds.Tables[5] != null)
            {
                if (ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[5].Rows)
                    {
                        BE.TransportType_m Transport = new BE.TransportType_m();
                        Transport.TransportID = Convert.ToInt16(row["Transport_Type_ID"]);
                        Transport.TransportType = Convert.ToString(row["Transport_Type"]);

                        TransportList.Add(Transport);
                    }
                }
            }
            if (ds.Tables[6] != null)
            {
                if (ds.Tables[6].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[6].Rows)
                    {
                        BE.ImportJoType ImportJo = new BE.ImportJoType();
                        ImportJo.Jo_type_id = Convert.ToInt16(row["Jo_Type_ID"]);
                        ImportJo.Jo_type = Convert.ToString(row["Jo_Type"]);

                        ImportJoList.Add(ImportJo);
                    }
                }
            }
            if (ds.Tables[7] != null)
            {
                if (ds.Tables[7].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[7].Rows)
                    {
                        BE.PortsEntites Port = new BE.PortsEntites();
                        Port.Portid = Convert.ToInt16(row["PortID"]);
                        Port.PortName = Convert.ToString(row["PortName"]);

                        PortList.Add(Port);
                    }
                }
            }
            if (ds.Tables[8] != null)
            {
                if (ds.Tables[8].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[8].Rows)
                    {
                        BE.CargoEntititesList Cargo = new BE.CargoEntititesList();
                        Cargo.cargoid = Convert.ToInt16(row["Cargotypeid"]);
                        Cargo.cargoname = Convert.ToString(row["CargoType"]);

                        CargoList.Add(Cargo);
                    }
                }
            }

            ImporttariffSettingID.ImportaccountMaster = ImportaccountMaster;
            ImporttariffSettingID.CommodityMaster = CommodityList;
            ImporttariffSettingID.ImportInvoiceType = ImportInvoiceList;
            ImporttariffSettingID.ChagresBasedOn = ChargesbasedList;
            ImporttariffSettingID.SettingTax = SettingTaxList;
            ImporttariffSettingID.TransportType_m = TransportList;
            ImporttariffSettingID.ImportJoType = ImportJoList;
            ImporttariffSettingID.PortsEntites = PortList;
            ImporttariffSettingID.CargoEntititesList = CargoList;
         

            return ImporttariffSettingID;
        }

        public List<BE.LocationMaster> getLocation()
        {
            List<BE.LocationMaster> locationDL = new List<BE.LocationMaster>();
            DataTable dt = new DataTable();
            string Table = "Ext_Location_m";
            string Column = "LocationID,Location";
            string Condition = "";
            string OrderBy = "Location";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.LocationMaster locationList = new BE.LocationMaster();
                    locationList.LocationID = Convert.ToInt32(row["LocationID"]);
                    locationList.Location = Convert.ToString(row["Location"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<BE.ContainerSize> GetSizeDetails()
        {
            List<BE.ContainerSize> locationDL = new List<BE.ContainerSize>();
            DataTable dt = new DataTable();
            string Table = "ContainerSize";
            string Column = "ContainerSizeID,ContainerSize";
            string Condition = "";
            string OrderBy = "ContainerSize";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ContainerSize locationList = new BE.ContainerSize();
                    locationList.ContainerSizeID = Convert.ToInt32(row["ContainerSizeID"]);
                    locationList.ContainerSizeName = Convert.ToString(row["ContainerSize"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }



        public List<BE.TariffAddDetailsEntites> SaveCargoDetails(DataTable Invoicedata, int Userid ,string tariffid ,DataTable DeliveryType,string Accounting,string AccountingID)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            string strSQL1 = "";
            string strSQL = "";
            string strSQL4 = "";
            int dblSlabID = 0;
            string SlIDMSize = "";
            string SlIDMType = "";
            string SlIDMBaseON = "";

            int dblSlabID2 = 0;
            string dbltype = "";
            string blnaddtariff = "True";

            string dblsize = "";
            DataTable dt = new DataTable();
            strSQL1 = "SELECT isnull(MAX(SlabID),0)+1 as [Slab ID] FROM import_slabs";
            dt = db.sub_GetDatatable(strSQL1);

            if (dt.Rows.Count > 0)
            {
                dblSlabID = Convert.ToInt32(dt.Rows[0]["Slab ID"]);
            }

            for (int k = 0; k < Invoicedata.Rows.Count; k++)
            {


                DataTable PortsDS = new DataTable();
                DataTable PortsDS1 = new DataTable();
                DataTable PortsDS3 = new DataTable();
                
                if (dblsize != Invoicedata.Rows[k].Field<string>("SlabSize") || dbltype != Invoicedata.Rows[k].Field<string>("SlabCargoType"))
                {
                    DataTable dt4 = new DataTable();
                    strSQL4 = "SELECT isnull(MAX(SlabID),0)+1 as [Slab ID] FROM import_slabs";
                    dt4 = db.sub_GetDatatable(strSQL4);
                    if (dt4.Rows.Count > 0)
                    {

                        dblSlabID = Convert.ToInt32(dt4.Rows[0]["Slab ID"]);



                        for (int i = 0; i < DeliveryType.Rows.Count; i++)
                        {
                            
                                if (Accounting != "")
                                {

                                    BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                                    DPDTList.TariffID = Convert.ToString(tariffid);
                                    DPDTList.DeliveryType = Convert.ToString(DeliveryType.Rows[i].Field<string>("DeliveryType"));
                                    DPDTList.From = Convert.ToString("");
                                    DPDTList.TO = Convert.ToString("");
                                    DPDTList.Accounting = Convert.ToString(Accounting);
                                DPDTList.Size = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabSize"));
                                DPDTList.Type = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabCargoType"));



                                string strSQL5 = "";
                                strSQL5 = " USP_Show_ChargeBasedON '" + Invoicedata.Rows[k].Field<string>("SlabOn") + "'";
                                PortsDS3 = db.sub_GetDatatable(strSQL5);
                                    if (PortsDS3.Rows.Count > 0)
                                    {
                                        DPDTList.Ftype = Convert.ToString(PortsDS3.Rows[0]["sorf"]);
                                    }
                                    else
                                    {
                                        DPDTList.Ftype = Convert.ToString(0);
                                    }
                                    
                                        DPDTList.Slabid = Convert.ToString(dblSlabID);
                                                                                                      
                                        DPDTList.FixedAmt = Convert.ToString(0);
                              
                                   
                                        DPDTList.Insurance = Convert.ToString(0);
                                  
                                   
                                        DPDTList.rate = Convert.ToString(0);
                                    
                                    
                                    
                                        DPDTList.Days = Convert.ToString(0);
                                    
                                    DPDTList.Tax = Convert.ToString("");
                                    DPDTList.InvoiceType = Convert.ToString(0);
                                    DPDTList.CurrencyType = Convert.ToString('I');
                                    DPDTList.TransType = Convert.ToString("");
                                    DPDTList.Port = Convert.ToString("");
                                    DPDTList.Freedays = Convert.ToString("");
                                    DPDTList.Location = Convert.ToString(Invoicedata.Rows[k].Field<string>("Location"));
                                    DPDTList.Jotype = Convert.ToString("");
                                    DPDTList.ScanType = Convert.ToString("");
                                    DPDTList.AccountingID = Convert.ToString(AccountingID);

                                    Getdetails.Add(DPDTList);

                                }
                        }

                    }

                    
                }
                if (k > 0)
                {
                    dblsize = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabSize"));
                    dbltype = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabCargoType"));
                }
                else
                {
                    dblsize = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabSize"));
                    dbltype = Convert.ToString(Invoicedata.Rows[k].Field<string>("SlabCargoType"));
                }

                if (Invoicedata.Rows[k].Field<string>("SlabOn") != "")
                {
                    string strSQL3 = "";
                    strSQL3 = "INSERT INTO import_slabs(SlabID,SlabOn,fromslab,toslab,Value,Size,CargoType) values (" + dblSlabID + ",'" + Invoicedata.Rows[k].Field<string>("SlabOn") + "','" + Invoicedata.Rows[k].Field<string>("RangeFrom") + "','" + Invoicedata.Rows[k].Field<string>("RangeTo") + "','" + Invoicedata.Rows[k].Field<string>("Value") + "','" + Invoicedata.Rows[k].Field<string>("SlabSize") + "','" + Invoicedata.Rows[k].Field<string>("SlabCargoType") + "')";
                    dt = db.sub_GetDatatable(strSQL3);
                }
            }

            return Getdetails;

        }


        public List<BE.SlabDetailsEntites> UpdateDriverPaymentDetails(DataTable DriverPaymentDT, Int32 userId)
        {
            //EN.DriverPaymentReco objdriverpaymententities = new EN.DriverPaymentReco();
            List<BE.SlabDetailsEntites> DriverPaymentList = new List<BE.SlabDetailsEntites>();
            if (DriverPaymentDT != null)
            {
                int count = 1;
                foreach (DataRow row in DriverPaymentDT.Rows)
                {
                    BE.SlabDetailsEntites DPDTList = new BE.SlabDetailsEntites();
                    DPDTList.SRNo = Convert.ToString(count++);
                    DPDTList.SlabOn = Convert.ToString(row["Slab On"]);
                    DPDTList.RangeFrom = Convert.ToString(row["From"]);
                    DPDTList.RangeTo = Convert.ToString(row["To"]);
                    DPDTList.Value = Convert.ToString(row["Value"]);
                    DPDTList.SlabSize = Convert.ToString(row["Size"]);
                    DPDTList.SlabCargoType = Convert.ToString(row["Cargo Type"]);
                 
                    DriverPaymentList.Add(DPDTList);
                }
            }
            return DriverPaymentList;
        }


           public string ImportValidation(DataTable Invoicedata)
        {
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";

            if (Invoicedata != null)
            {
                foreach (DataRow row in Invoicedata.Rows)
                {
                    DataSet PortsDS = new DataSet();
                    PortsDS = importDB.CheckImporttariffList(Convert.ToString(row["SlabSize"]), Convert.ToString(row["SlabCargoType"]));
                    if (PortsDS.Tables[0].Rows.Count == 0)
                    {
                        if (message == ""){
                            message = "Following Size not found in database: \n" + Convert.ToString(row["SlabSize"]);
                        }
                        else
                        if (Convert.ToString(row["SlabSize"]).Contains(message))
                        {
                        }
                        else
                        {
                            message += "," + Convert.ToString(row["SlabSize"]);
                        }
                    }
                    if (PortsDS.Tables[1].Rows.Count == 0)
                    {
                        if (message1 == "")
                        {
                            message1 = "Following Cargo Type not found in database: \n" + Convert.ToString(row["SlabCargoType"]);
                        }
                        else
                        {
                            if (Convert.ToString(row["SlabCargoType"]).Contains(message1))
                            {
                            }
                            else
                            {
                                message1 += "," + Convert.ToString(row["SlabCargoType"]);
                            }
                        }
                    }
                    
                }
                if ((message!="") || (message1 != ""))
                {
                    message2 = message + "\n" + message1 ;
                }                   
            }
            return message2;
        }

        public List<BE.DeliveryTypeDetails> GetDeliveryDetails()
        {
            List<BE.DeliveryTypeDetails> locationDL = new List<BE.DeliveryTypeDetails>();
            DataTable dt = new DataTable();
            string Table = "Delivery_Type";
            string Column = "DeliveryTypeiD,DeliveryType";
            string Condition = "";
            string OrderBy = "DeliveryType";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.DeliveryTypeDetails locationList = new BE.DeliveryTypeDetails();
                    locationList.DeliveryID = Convert.ToInt32(row["DeliveryTypeiD"]);
                    locationList.DeliveryType = Convert.ToString(row["DeliveryType"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<BE.SlabDetails> GetSlabDetails()
        {
            List<BE.SlabDetails> locationDL = new List<BE.SlabDetails>();
            DataTable dt = new DataTable();
            string Table = "import_slabs";
            string Column = "distinct slabid";
            string Condition = "slabID not in (select slabID from import_tariffdetails) order by slabid";
            string OrderBy = "";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.SlabDetails locationList = new BE.SlabDetails();
                    locationList.SlabId = Convert.ToInt32(row["slabid"]);
                    locationList.SlabId = Convert.ToInt32(row["slabid"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }


        public List<BE.TariffAddDetailsEntites> GetAddTabledetails(string TariffID, string From, string TO, string Accounting
           , string Size, string ChargesBased, string FixedAmt, string Days, string rate
           , string Slabid, string ScanType, string Location, string Jotype, string Commodity, string TransType,
           string Port, string Insurance, string AccountingID, DataTable dataTable, DataTable dataTable1, string TaxID, string InvoiceType, string fromlocation,
           string tolocation, string Drivertype)
        {
            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable1.Rows.Count; j++)
                {
                    if (Accounting != "")
                    {

                        BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                        DPDTList.TariffID = Convert.ToString(TariffID);
                        DPDTList.DeliveryType = Convert.ToString(dataTable.Rows[i].Field<string>("DeliveryType"));
                        DPDTList.From = Convert.ToString(Convert.ToDateTime(From).ToString("dd-MMM-yyyy"));
                        DPDTList.TO = Convert.ToString(Convert.ToDateTime(TO).ToString("dd-MMM-yyyy"));
                        DPDTList.Accounting = Convert.ToString(Accounting);
                        DPDTList.Size = Convert.ToString(Size);
                        DPDTList.Type = Convert.ToString(dataTable1.Rows[j].Field<string>("Type"));


                        DPDTList.InvoiceType = Convert.ToString(InvoiceType);


                        string strSQL = "";
                        strSQL = " USP_Show_ChargeBasedON '" + ChargesBased + "'";
                        dt1 = db.sub_GetDatatable(strSQL);
                        if (dt1.Rows.Count > 0)
                        {
                            DPDTList.Ftype = Convert.ToString(dt1.Rows[0]["sorf"]);
                        }
                        else
                        {
                            DPDTList.Ftype = Convert.ToString(0);
                        }
                        if (Slabid != null)
                        {
                            DPDTList.Slabid = Convert.ToString(Slabid);
                        }
                        else
                        {
                            DPDTList.Slabid = Convert.ToString(0);
                        }
                        if (FixedAmt != null)
                        {
                            DPDTList.FixedAmt = Convert.ToString(FixedAmt);
                        }
                        else
                        {
                            DPDTList.FixedAmt = Convert.ToString(0);
                        }
                        if (Insurance != null)
                        {
                            DPDTList.Insurance = Convert.ToString(Insurance);
                        }
                        else
                        {
                            DPDTList.Insurance = Convert.ToString(0);
                        }
                        if (rate != null)
                        {
                            DPDTList.rate = Convert.ToString(rate);
                        }
                        else
                        {
                            DPDTList.rate = Convert.ToString(0);
                        }
                        if (Days != null)
                        {
                            DPDTList.Days = Convert.ToString(Days);
                        }
                        else
                        {
                            DPDTList.Days = Convert.ToString(0);
                        }
                        DPDTList.Tax = Convert.ToString(TaxID);

                        DPDTList.CurrencyType = Convert.ToString('I');
                        DPDTList.TransType = Convert.ToString(TransType);
                        DPDTList.Port = Convert.ToString(Port);
                        DPDTList.Freedays = Convert.ToString(Days);
                        DPDTList.Location = Convert.ToString(fromlocation);
                        DPDTList.Tolocation = Convert.ToString(tolocation);
                        DPDTList.Jotype = Convert.ToString(Jotype);
                        DPDTList.ScanType = Convert.ToString(ScanType);
                        DPDTList.AccountingID = Convert.ToString(AccountingID);
                        DPDTList.Drivertype = Convert.ToString(Drivertype);

                        Getdetails.Add(DPDTList);

                    }
                }
            }

            return Getdetails;
        }

        public string CheckEffective(DataTable ImportData, int Userid,string commodity)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int k = 0; k < ImportData.Rows.Count; k++)
            {


                DataTable PortsDS = new DataTable();
                DataTable PortsDS1 = new DataTable();
                string strSQL = "";
                strSQL = "Update import_tariffdetails set EffectiveUpto='"  + Convert.ToDateTime(ImportData.Rows[k].Field<string>("TO")).ToString("yyyyMMdd") + "' WHERE TariffID='" + ImportData.Rows[k].Field<string>("TariffID") + "'";
                strSQL += " AND DeliveryType='" + ImportData.Rows[k].Field<string>("DeliveryType") + "' AND AccountID=" + ImportData.Rows[k].Field<string>("AccountingID") + " AND ContainerSize='" + ImportData.Rows[k].Field<string>("Size") + "'";
                strSQL += " AND ContainerType='" + ImportData.Rows[k].Field<string>("Type") + "' AND EffectiveUpTo >'" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "' AND CommodityID ='" + commodity + "' AND LocationID ='" + ImportData.Rows[k].Field<string>("Location") + "' AND JoType ='" + ImportData.Rows[k].Field<string>("Jotype") + "'";
                PortsDS = db.sub_GetDatatable(strSQL);


              string  strSQL1 = "";
                strSQL1 = "select FORMAT(cast(effectivefrom as datetime),'dd MMM yyyy HH:mm') as effectivefrom from Import_tariffdetails WHERE TariffID='" + ImportData.Rows[k].Field<string>("TariffID") + "'";
                strSQL1 += " AND DeliveryType='" + ImportData.Rows[k].Field<string>("DeliveryType") + "' AND AccountID=" + ImportData.Rows[k].Field<string>("AccountingID") + " AND ContainerSize=" + ImportData.Rows[k].Field<string>("Size") + "";
                strSQL1 += " AND ContainerType='" + ImportData.Rows[k].Field<string>("Type") + "' AND EffectiveUpTo >'" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "' AND CommodityID ='" + commodity + "' AND LocationID ='" + ImportData.Rows[k].Field<string>("Location") + "' AND JoType ='" + ImportData.Rows[k].Field<string>("Jotype") + "'";
                PortsDS = db.sub_GetDatatable(strSQL);


                PortsDS1 = db.sub_GetDatatable(strSQL1);
                if (PortsDS1.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(ImportData.Rows[k].Field<string>("From")) > Convert.ToDateTime(PortsDS1.Rows[0].Field<string>("effectivefrom")))
                    {

                        Message += "Effective From date cannot be less than effective previous. " + ImportData.Rows[k].Field<string>("Size") + " + " + ImportData.Rows[k].Field<string>("Type") + "";
                    }
                    else
                    {
                        Message += "";
                    }
                }
               



            }


            return Message;
        }



        public string SaveImportSettingTariff(DataTable ImportData, int Userid,string commodity, string FromDate, string Todate)
        {
            string Message = ""; string ScanType = ""; string strcurrency = "";  int MaxDays = 0;  int isTax = 0;  int isSplit = 0;
            int Transid = 0;
            int Portname = 0;
            int Location = 0;
            int Jotype = 0;
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            DataTable dtDelete = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int k = 0; k < ImportData.Rows.Count; k++)
            {
                DataTable PortsDS = new DataTable();
                DataTable PortsDS1 = new DataTable();
                if (ImportData.Rows[k].Field<string>("AccountingID")  == "30016")
                {
                    ScanType = "M";
                }
                if (ImportData.Rows[k].Field<string>("AccountingID") == "30017" || ImportData.Rows[k].Field<string>("AccountingID") == "30018")
                {
                    ScanType = "F";
                }
                if (ImportData.Rows[k].Field<string>("AccountingID") == "30171")
                {
                    ScanType = "D";
                }
                if (ImportData.Rows[k].Field<string>("Freedays") != "0" && ImportData.Rows[k].Field<string>("Freedays") != "")
                {
                    MaxDays = Convert.ToInt32(ImportData.Rows[k].Field<string>("Freedays"));
                }
                else
                {
                    MaxDays = 0;
                }
                if (ImportData.Rows[k].Field<string>("Tax") == "Yes")
                {
                    isTax = 1;
                }
                else
                {
                    isTax = 0;
                }
                if (ImportData.Rows[k].Field<string>("CurrencyType") == "Dollor")
                {
                    strcurrency = "D";
                }
                else
                {
                    strcurrency = "I";
                }
                isSplit = 0;

                string maxif = "";
                int maxif1 = 0;
                maxif = "select Transport_Type_ID from Transport_Type_M where Transport_Type ='" + ImportData.Rows[k].Field<string>("TransType") + "'";
                PortsDS1 = db.sub_GetDatatable(maxif);
                if (PortsDS1.Rows.Count > 0)
                {
                    maxif1 = PortsDS1.Rows[0].Field<int>("Transport_Type_ID");
                }


                string PortID = "";
                int POrtName = 0;
                PortID = "select PortID from Ports where PortName ='" + ImportData.Rows[k].Field<string>("Port") + "'";
                PortsDS1 = db.sub_GetDatatable(PortID);
                if (PortsDS1.Rows.Count > 0)
                {
                    POrtName = PortsDS1.Rows[0].Field<int>("PortID");
                }


                string Locationid = "";
                int GetLocation = 0;
                Locationid = "select locationid from ext_location_m where Location ='" + ImportData.Rows[k].Field<string>("Location") + "'";
                PortsDS1 = db.sub_GetDatatable(Locationid);
                if (PortsDS1.Rows.Count > 0)
                {
                    GetLocation = PortsDS1.Rows[0].Field<int>("locationid");
                }


                string GetJoTYpe = "";
                int Typejo = 0;
                GetJoTYpe = "select Jo_Type_ID from Import_Jo_Type where Jo_Type ='" + ImportData.Rows[k].Field<string>("Jotype") + "'";
                PortsDS1 = db.sub_GetDatatable(GetJoTYpe);
                if (PortsDS1.Rows.Count > 0)
                {
                    Typejo = PortsDS1.Rows[0].Field<int>("Jo_Type_ID");
                }


                string strSQL1 = "";
                strSQL1 = "INSERT INTO Import_tariffdetails(tariffID,deliverytype,EffectiveFrom,EffectiveUpto,AccountID,ScanType,ContainerSize,ContainerType,SorF,SlabID,fixedAmt,insRate,RateperMT,MaxDays,TaxID,InvoiceType,IsSQM,isTotDays, Currency_type,TransType,PortName,FreeDay,CommodityID,LocationID,JoType) VALUES";
                strSQL1 += " ('" + ImportData.Rows[k].Field<string>("TariffID") + "','" + ImportData.Rows[k].Field<string>("DeliveryType") + "','" + Convert.ToDateTime(FromDate).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(Todate).ToString("yyyyMMdd") + "',";
                strSQL1 += " '" + ImportData.Rows[k].Field<string>("AccountingID") + "','" + ImportData.Rows[k].Field<string>("ScanType") + "','" + ImportData.Rows[k].Field<string>("Size") + "','" + ImportData.Rows[k].Field<string>("Type") + "','" + ImportData.Rows[k].Field<string>("Ftype") + "',";
                strSQL1 += " '" + ImportData.Rows[k].Field<string>("Slabid") + "','" + ImportData.Rows[k].Field<string>("FixedAmt") + "','" + ImportData.Rows[k].Field<string>("Insurance") + "','" + ImportData.Rows[k].Field<string>("rate") + "','" + MaxDays + "',";
                strSQL1 += " '" + ImportData.Rows[k].Field<string>("Tax") + "','" + ImportData.Rows[k].Field<string>("InvoiceType") + "','" + 0 + "','" + 0 + "', '" + strcurrency + "','"+ maxif1 + "','" + POrtName + "','" + ImportData.Rows[k].Field<string>("Freedays") + "','" + commodity + "','" + GetLocation + "','" + Typejo + "')";
                 PortsDS1 = db.sub_GetDatatable(strSQL1);



                Message = "Record saved successfully.";



            }
            string Srtdelete  = "";
            Srtdelete = "delete from Tempimport_tariffdetails where addedby ='" + Userid + "'  ";
            dtDelete = db.sub_GetDatatable(Srtdelete);
            return Message;
        }
        public List<BE.importtariffdetails> Getimporttariffdetails()
        {
            List<BE.importtariffdetails> locationDL = new List<BE.importtariffdetails>();
            DataTable dt = new DataTable();
            string Table = "customer_tariffmaster";
            string Column = "Distinct TariffID,TariffDescription";
            string Condition = "";
            string OrderBy = "TariffDescription";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.importtariffdetails locationList = new BE.importtariffdetails();
                    locationList.TariffID = Convert.ToInt32(row["TariffID"]);
                    locationList.TariffDescription = Convert.ToString(row["TariffDescription"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }



        public List<BE.ContainerType> GetContainerType()
        {
            List<BE.ContainerType> locationDL = new List<BE.ContainerType>();
            DataTable dt = new DataTable();
            string Table = "ContainerType";
            string Column = "Distinct ContainerTypeID,ContainerType";
            string Condition = "";
            string OrderBy = "ContainerType";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ContainerType locationList = new BE.ContainerType();
                    locationList.ContainerTypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    locationList.ContainerTypeName = Convert.ToString(row["ContainerType"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }


        public List<BE.ImportAccountMaster> GetAccountHead()
        {
            List<BE.ImportAccountMaster> locationDL = new List<BE.ImportAccountMaster>();
            DataTable dt = new DataTable();
            string Table = "accountmaster";
            string Column = "Distinct AccountID,AccountName";
            string Condition = "";
            string OrderBy = "AccountName";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ImportAccountMaster locationList = new BE.ImportAccountMaster();
                    locationList.AccountID = Convert.ToInt32(row["AccountID"]);
                    locationList.AccountName = Convert.ToString(row["AccountName"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }


        public List<BE.TariffAddDetailsEntites> GetCancelTariffDetails(string TariffId, string deliveryType, string containerType, string accounthead)
        {
            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            DataTable dt = new DataTable();

            dt = importDB.GetCancelDetailsForTariffSetting(TariffId, deliveryType, containerType, accounthead);
            foreach (DataRow row in dt.Rows)
            {

                BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                DPDTList.TariffID = Convert.ToString(row["Tariff ID"]);
                DPDTList.DeliveryType = Convert.ToString(row["Delivery Type"]);
                DPDTList.From = Convert.ToString(row["Effective from"]);
                DPDTList.TO = Convert.ToString(row["Effective Upto"]);
                DPDTList.Accounting = Convert.ToString(row["Account Name"]);
                DPDTList.Size = Convert.ToString(row["Size"]);
                DPDTList.Type = Convert.ToString(row["Type"]);
                DPDTList.FixedAmt = Convert.ToString(row["Fixed Amt"]);
                DPDTList.rate = Convert.ToString(row["Rate Per Amount"]);
                DPDTList.Insurance = Convert.ToString(row["Ins Rate"]);
                DPDTList.Tax = Convert.ToString(row["isSTax"]);
                DPDTList.Freedays = Convert.ToString(row["Free Day"]);
                DPDTList.Location = Convert.ToString(row["Location"]);
                DPDTList.username = Convert.ToString(row["Approved By"]);
                DPDTList.Entryid = Convert.ToString(row["Entryid"]);
                DPDTList.Approved = Convert.ToString(row["isapproved"]);
                DPDTList.Tolocation = Convert.ToString(row["To Location"]);
                DPDTList.Drivertype = Convert.ToString(row["Drivertype"]);

                Getdetails.Add(DPDTList);
            }
            return Getdetails;
        }


        public string CancelDetailsTariff(DataTable ImportData, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int k = 0; k < ImportData.Rows.Count; k++)
            {

                DataTable PortsDS = new DataTable();
                string strSQL = "";
                strSQL = "UPDATE customer_tariffdetails SET IsCancel=1,CancelledOn=GETDATE(),CancelledBy='" + Userid + "' where entryid= '" + ImportData.Rows[k].Field<string>("Entryid") + "'";
                PortsDS = db.sub_GetDatatable(strSQL);
                Message = "Record Cancelled Successfully!";
            }

            return Message;
        }

        public string ApproveDetailsTariff(DataTable ImportData, int Userid)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int k = 0; k < ImportData.Rows.Count; k++)
            {

                DataTable PortsDS = new DataTable();


                string strSQL = "";
                strSQL = "UPDATE customer_tariffdetails SET IsApproved=1,ApprovedOn=GETDATE(),ApprovedBy='" + Userid + "' where entryid= '" + ImportData.Rows[k].Field<string>("Entryid") + "'";
                PortsDS = db.sub_GetDatatable(strSQL);
                Message = "Record Approved Successfully!";
            }

            return Message;
        }




        public List<BE.TariffAddDetailsEntites> SavedataForGetdata(DataTable ImportData, int Userid)
        {


            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();




            for (int k = 0; k < ImportData.Rows.Count; k++)
            {

                string Accountid = "";
                int getaccountid = 0;
                DataTable Accountdl = new DataTable();
                Accountid = "select AccountID from accountmaster where AccountName ='" + ImportData.Rows[k].Field<string>("Accounting") + "'";
                Accountdl = db.sub_GetDatatable(Accountid);
                if (Accountdl.Rows.Count > 0)
                {
                    getaccountid = Convert.ToInt32(Accountdl.Rows[0][0]);
                }
                if (ImportData.Rows[k].Field<string>("From") != "")
                {
                    string strSQL1 = "";
                    strSQL1 = "INSERT INTO Tempimport_tariffdetails(TARIFFID, DELIVERYTYPE, EFFECTIVEFROM, EFFECTIVEUPTO, ACCOUNTNAME, CONTAINERSIZE, CONTAINERTYPE, SORF, SLABID, INSRATE, FIXEDAMT, RATEPERMT, TAXID, INVOICETYPE, CURRENCY_TYPE, TRANSTYPE, PORTNAME, FREEDAY, LOCATIONID, JOTYPE, SCANTYPE, ACCOUNTID, ADDEDBY,Tolocation,drivertype) VALUES";
                    strSQL1 += " ('" + ImportData.Rows[k].Field<string>("TariffID") + "','" + ImportData.Rows[k].Field<string>("DeliveryType11") + "','" + Convert.ToDateTime(ImportData.Rows[k].Field<string>("From")).ToString("dd MMM yyyy HH:mm") + "','" + Convert.ToDateTime(ImportData.Rows[k].Field<string>("TO")).ToString("dd MMM yyyy HH:mm") + "',";
                    strSQL1 += " '" + ImportData.Rows[k].Field<string>("AccountingID") + "','" + ImportData.Rows[k].Field<string>("Size") + "','" + ImportData.Rows[k].Field<string>("Type1") + "','" + ImportData.Rows[k].Field<string>("Ftype") + "','" + ImportData.Rows[k].Field<string>("Slabid") + "','" + ImportData.Rows[k].Field<string>("Insurance") + "','" + ImportData.Rows[k].Field<string>("FixedAmt") + "',";
                    strSQL1 += " '" + ImportData.Rows[k].Field<string>("rate") + "','" + ImportData.Rows[k].Field<string>("Tax") + "','" + ImportData.Rows[k].Field<string>("InvoiceType") + "','" + ImportData.Rows[k].Field<string>("CurrencyType") + "','" + ImportData.Rows[k].Field<string>("TransType") + "','" + ImportData.Rows[k].Field<string>("Port") + "','" + ImportData.Rows[k].Field<string>("Freedays") + "',";
                    strSQL1 += " '" + ImportData.Rows[k].Field<string>("Location") + "','" + ImportData.Rows[k].Field<string>("Jotype") + "','" + ImportData.Rows[k].Field<string>("ScanType") + "','" + getaccountid + "','" + Userid + "','" + ImportData.Rows[k].Field<string>("Tolocation") + "','" + ImportData.Rows[k].Field<string>("Drivertype") + "')";
                    dt = db.sub_GetDatatable(strSQL1);
                }
                else
                {
                    string strSQL2 = "";
                    strSQL2 = "INSERT INTO Tempimport_tariffdetails(TARIFFID, DELIVERYTYPE, EFFECTIVEFROM, EFFECTIVEUPTO, ACCOUNTNAME, CONTAINERSIZE, CONTAINERTYPE, SORF, SLABID, INSRATE, FIXEDAMT, RATEPERMT, TAXID, INVOICETYPE, CURRENCY_TYPE, TRANSTYPE, PORTNAME, FREEDAY, LOCATIONID, JOTYPE, SCANTYPE, ACCOUNTID, ADDEDBY,Tolocation,drivertype) VALUES";
                    strSQL2 += " ('" + ImportData.Rows[k].Field<string>("TariffID") + "','" + ImportData.Rows[k].Field<string>("DeliveryType11") + "','" + ImportData.Rows[k].Field<string>("From") + "','" + ImportData.Rows[k].Field<string>("TO") + "',";
                    strSQL2 += " '" + ImportData.Rows[k].Field<string>("AccountingID") + "','" + ImportData.Rows[k].Field<string>("Size") + "','" + ImportData.Rows[k].Field<string>("Type1") + "','" + ImportData.Rows[k].Field<string>("Ftype") + "','" + ImportData.Rows[k].Field<string>("Slabid") + "','" + ImportData.Rows[k].Field<string>("Insurance") + "','" + ImportData.Rows[k].Field<string>("FixedAmt") + "',";
                    strSQL2 += " '" + ImportData.Rows[k].Field<string>("rate") + "','" + ImportData.Rows[k].Field<string>("Tax") + "','" + ImportData.Rows[k].Field<string>("InvoiceType") + "','" + ImportData.Rows[k].Field<string>("CurrencyType") + "','" + ImportData.Rows[k].Field<string>("TransType") + "','" + ImportData.Rows[k].Field<string>("Port") + "','" + ImportData.Rows[k].Field<string>("Freedays") + "',";
                    strSQL2 += " '" + ImportData.Rows[k].Field<string>("Location") + "','" + ImportData.Rows[k].Field<string>("Jotype") + "','" + ImportData.Rows[k].Field<string>("ScanType") + "','" + getaccountid + "','" + Userid + "','" + ImportData.Rows[k].Field<string>("Tolocation") + "','" + ImportData.Rows[k].Field<string>("Drivertype") + "')";
                    dt = db.sub_GetDatatable(strSQL2);
                }
            }
            DataTable getdt = new DataTable();

            getdt = importDB.GetAddedTariffsettingDetails(Userid);
            foreach (DataRow row in getdt.Rows)
            {

                BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                DPDTList.TariffID = Convert.ToString(row["tariffID"]);
                DPDTList.DeliveryType = Convert.ToString(row["deliverytype"]);
                DPDTList.From = Convert.ToString(row["effectivefrom"]);
                DPDTList.TO = Convert.ToString(row["effectiveupto"]);
                DPDTList.Accounting = Convert.ToString(row["accountName"]);
                DPDTList.Size = Convert.ToString(row["containersize"]);
                DPDTList.Type = Convert.ToString(row["containertype"]);
                DPDTList.Ftype = Convert.ToString(row["SorF"]);
                DPDTList.Slabid = Convert.ToString(row["slabID"]);
                DPDTList.FixedAmt = Convert.ToString(row["fixedamt"]);
                DPDTList.Insurance = Convert.ToString(row["insrate"]);
                DPDTList.rate = Convert.ToString(row["ratepermt"]);
                DPDTList.Days = Convert.ToString(row["FreeDay"]);

                DPDTList.Tax = Convert.ToString(row["TaxID"]);
                DPDTList.InvoiceType = Convert.ToString(row["InvoiceType"]);
                DPDTList.CurrencyType = Convert.ToString(row["Currency_type"]);
                DPDTList.TransType = Convert.ToString(row["TransType"]);
                DPDTList.Port = Convert.ToString(row["PortName"]);
                DPDTList.Freedays = Convert.ToString(row["FreeDay"]);
                DPDTList.Location = Convert.ToString(row["LocationID"]);
                DPDTList.Jotype = Convert.ToString(row["JoType"]);
                DPDTList.ScanType = Convert.ToString(row["ScanType"]);
                DPDTList.AccountingID = Convert.ToString(row["accountID"]);
                DPDTList.Entryid = Convert.ToString(row["Entryid"]);
                DPDTList.Tolocation = Convert.ToString(row["Tolocation"]);
                DPDTList.Drivertype = Convert.ToString(row["Drivertype"]);
                Getdetails.Add(DPDTList);
            }

            return Getdetails;
        }

        public BE.ProformaEntities validateIGMNItemNoForPIAndGetDetails(string AssessmentType , string IGMNo, string ItemNO)
        {
            BE.ProformaEntities GetDetails = new BE.ProformaEntities();
            DataSet codeDL = new DataSet();
            codeDL = importDB.validateIGMNItemNoForPIAndGetDetails(AssessmentType,IGMNo, ItemNO);
            List<BE.ImportInvoiceContainerDetails> importInvoiceContainerDetails = new List<BE.ImportInvoiceContainerDetails>();
            BE.ImportListGetWeight GetWeight = new BE.ImportListGetWeight();
            BE.ImportListArea getAreaList = new BE.ImportListArea();
            BE.ImportListMsg GetMSgdetails = new BE.ImportListMsg();
            BE.Sealcuttingdetails GetsealcuttingDetails = new BE.Sealcuttingdetails();
            BE.ImportProformaSearchGST GetImportProformaSearchGST = new BE.ImportProformaSearchGST();
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt1 = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();


            if (codeDL.Tables.Count > 1)
            {
                if (codeDL.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in codeDL.Tables[0].Rows)
                    {

                        GetWeight.Weight = Convert.ToString(row["Weight"]);
                        GetWeight.PKGS = Convert.ToString(row["PKGS"]);
                    }

                }

                if (codeDL.Tables[1].Rows.Count != 0)
                {
                    foreach (DataRow row in codeDL.Tables[1].Rows)
                    {
                        BE.ImportInvoiceContainerDetails oper = new BE.ImportInvoiceContainerDetails();
                        oper.JONo = Convert.ToString(row["JONo"]);
                        oper.ContainerNo = Convert.ToString(row["ContainerNo"]);
                        oper.Tare_Weight = Convert.ToString(row["tareweight"]);
                        oper.Size = Convert.ToString(row["Size"]);
                        oper.ISOCODE = Convert.ToString(row["ISOCODE"]);
                        oper.ContainerType = Convert.ToString(row["ContainerType"]);
                        oper.InDate = Convert.ToString(row["InDate"]);
                        oper.ScanType = Convert.ToString(row["ScanType"]);
                        oper.IsScan = Convert.ToInt32(row["IsScan"]);
                        oper.Cargotype = Convert.ToString(row["Cargotype"]);
                        oper.Consignee = Convert.ToString(row["Consignee"]);
                        oper.IGM_BLNo = Convert.ToString(row["IGMBLNo"]);
                        oper.AGName = Convert.ToString(row["AGName"]);
                        oper.AgentID = Convert.ToInt32(row["AgentID"]);
                        oper.IGM_MT_Wt = Convert.ToDouble(row["IGMMTWeight"]);
                        oper.arrival = Convert.ToString(row["arrivaldate"]);
                        oper.slid = Convert.ToInt32(row["slid"]);
                        oper.slname = Convert.ToString(row["slname"]);
                        oper.Haulage_Type = Convert.ToString(row["HaulageType"]);
                        oper.Commodity_Group_Name = Convert.ToString(row["CommodityGroupName"]);
                        oper.PORTNAME = Convert.ToString(row["PORTNAME"]);
                        oper.ImporterID = Convert.ToInt32(row["ImporterID"]);
                        oper.ImporterName = Convert.ToString(row["ImporterName"]);
                        oper.Haulage_Type_Id = Convert.ToString(row["HaulageTypeId"]);
                        oper.jotypeid = Convert.ToString(row["jotypeid"]);
                        oper.commodityid = Convert.ToString(row["commodityid"]);
                        oper.transid = Convert.ToInt32(row["transid"]);
                        oper.Jo_Type = Convert.ToString(row["JoType"]);
                        oper.IGM_Qty = Convert.ToString(row["PKGS"]);

                        string strSQL = "";
                        strSQL = "select TOP 1 IGM_MT_Wt , consignee from IGMDetails where igmno='" + IGMNo + "' and itemno='" + ItemNO + "' AND ContainerNo='" + Convert.ToString(row["ContainerNo"]) + "' and jono=" + Convert.ToString(row["JONo"]) + "";
                        dt1 = db.sub_GetDatatable(strSQL);


                        if (oper.Tare_Weight != "")
                        {
                            oper.grossweight = Convert.ToString(Convert.ToInt64(oper.Tare_Weight) + Convert.ToInt64(oper.IGM_MT_Wt));
                        }
                        else
                        {
                            if (oper.Size == "20")
                            {
                                oper.grossweight = Convert.ToString(Convert.ToInt64(dt1.Rows[0].Field<string>("IGM_MT_Wt")) + 2360); 
                            }
                            else
                            {
                                oper.grossweight = Convert.ToString(Convert.ToInt64(dt1.Rows[0].Field<double>("IGM_MT_Wt")) + 4300);
                               
                            }
                        }
                        importInvoiceContainerDetails.Add(oper);
                    }

                }
                if (codeDL.Tables[2].Rows.Count != 0)
                {
                    foreach (DataRow row in codeDL.Tables[2].Rows)
                    {

                        getAreaList.Area = Convert.ToString(row["Area"]);
                        getAreaList.DestuffDate = Convert.ToString(row["DestuffDate"]);
                    }

                }
                if (codeDL.Tables[5].Rows.Count != 0)
                {
                    foreach (DataRow row in codeDL.Tables[5].Rows)
                    {

                        GetMSgdetails.Response = Convert.ToString(row["Response"]);
                        GetMSgdetails.Continues = Convert.ToString(row["Continues"]);
                    }

                }
                if (codeDL.Tables[3].Rows.Count != 0)
                {
                    foreach (DataRow row in codeDL.Tables[3].Rows)
                    {

                        GetsealcuttingDetails.BOENO = Convert.ToString(row["BOENo"]);
                        GetsealcuttingDetails.BOEDATE = Convert.ToString(row["BOEDate"]);
                        GetsealcuttingDetails.CHAID = Convert.ToString(row["chaid"]);
                        GetsealcuttingDetails.value = Convert.ToString(row["value"]);
                        GetsealcuttingDetails.duty = Convert.ToString(row["duty"]);
                        GetsealcuttingDetails.DeliveryType = Convert.ToString(row["deliverytype"]);
                    }

                }
                if (codeDL.Tables[4].Rows.Count != 0)
                {
                    foreach (DataRow row in codeDL.Tables[4].Rows)
                    {

                        GetImportProformaSearchGST.GSTID = Convert.ToString(row["gstid"]);
                        GetImportProformaSearchGST.GSTIn_uniqID = Convert.ToString(row["GSTIn_uniqID"]);
                        GetImportProformaSearchGST.GSTName = Convert.ToString(row["gstname"]);
                        GetImportProformaSearchGST.state_Code = Convert.ToString(row["state_Code"]);
                        GetImportProformaSearchGST.TariffIDSaved = Convert.ToString(row["tariffid"]);
                        GetImportProformaSearchGST.FreeDaysSaved = Convert.ToString(row["FreeDays"]);
                      
                    }

                }
            }
            else
            {
                if (codeDL.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in codeDL.Tables[0].Rows)
                    {

                        GetMSgdetails.Response = Convert.ToString(row["Response"]);
                        GetMSgdetails.Continues = Convert.ToString(row["Continues"]);
                    }

                }
            }

           
            

            GetDetails.ImportInvoiceContainerDetails = importInvoiceContainerDetails;
            GetDetails.ImportListGetWeight = GetWeight;
            GetDetails.ImportListArea = getAreaList;
            GetDetails.ImportListMsg = GetMSgdetails;
            GetDetails.Sealcuttingdetails = GetsealcuttingDetails;
            GetDetails.ImportProformaSearchGST = GetImportProformaSearchGST;
            return GetDetails;
        }
        
        



        public string CancelAssessmentDetails(string remarks,string AssessNo, string workyear, int userId)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = importDB.CancelAssessmentDetails(remarks, AssessNo, workyear, userId);

            message = "Records Cancel Successfully!";
            return message;
        }


        public string CheckCancelAssessmentDetails(string Invoiceno)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = importDB.CheckCancelAssessmentDetails(Invoiceno);


            if (updateDl.Rows.Count > 0)
            {
                message = Convert.ToString(updateDl.Rows[0][0]);
            }
            return message;
        }

        public List<BE.SettingTax> GetSettingtax()
        {
            List<BE.SettingTax> locationDL = new List<BE.SettingTax>();
            DataTable dt = new DataTable();
            string Table = "settings_taxes";
            string Column = "Distinct settingsID,taxname";
            string Condition = "";
            string OrderBy = "taxname";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.SettingTax locationList = new BE.SettingTax();
                    locationList.Settingid = Convert.ToInt32(row["settingsID"]);
                    locationList.TaxName = Convert.ToString(row["taxname"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public BE.SettingTax GetsettingCode()
        {
            BE.SettingTax locationDL = new BE.SettingTax();
            DataTable dt = new DataTable();
            string Table = "settings";
            string Column = "Tinnumber";
            string Condition = "";
            string OrderBy = "";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {



                locationDL.TaxName = Convert.ToString(dt.Rows[0][0]);


            }
            return locationDL;
        }
        public string CancelOtherInvoicePorforma(string AssessNo, string workyear, int userId)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = importDB.CancelOtherInvoiceProforma(AssessNo, workyear, userId);

            message = "Records Cancel Successfully!";
            return message;
        }
        public string SubmitOtherFinalDetails(string AssessNo, string workyear, int userId)
        {

            string message = "";
            DataTable updateDl = new DataTable();
            updateDl = importDB.SubmitFinalotherInvoiceproforma(AssessNo, workyear, userId);

            message = "Final invoice is generated successfully!";
            return message;
        }
        public List<BE.ActivityMaster> Activitymaster()
        {
            List<BE.ActivityMaster> locationDL = new List<BE.ActivityMaster>();
            DataTable dt = new DataTable();
            string Table = "activitymaster";
            string Column = "autoid ,activity";
            string Condition = "";
            string OrderBy = "activity";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ActivityMaster locationList = new BE.ActivityMaster();
                    locationList.Activity = Convert.ToString(row["autoid"]);
                    locationList.Activity = Convert.ToString(row["activity"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<BE.ActivityMaster> Getactivitymasterdetails()
        {
            List<BE.ActivityMaster> locationDL = new List<BE.ActivityMaster>();
            DataTable dt = new DataTable();

            dt = importDB.GetActivityWiseDetails();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ActivityMaster locationList = new BE.ActivityMaster();
                    locationList.Activity = Convert.ToString(row["autoid"]);
                    locationList.Activity = Convert.ToString(row["activity"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<BE.ImportInvoiceType> Invoicetype()
        {
            List<BE.ImportInvoiceType> locationDL = new List<BE.ImportInvoiceType>();
            DataTable dt = new DataTable();
            string Table = "Import_InvoiceType";
            string Column = "Distinct ID,Invoice_Type";
            string Condition = "";
            string OrderBy = "Invoice_Type";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ImportInvoiceType locationList = new BE.ImportInvoiceType();
                    locationList.ID = Convert.ToInt32(row["ID"]);
                    locationList.InvoiceType = Convert.ToString(row["Invoice_Type"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<BE.Customer> GetCustomer()

        {
            List<BE.Customer> locationDL = new List<BE.Customer>();
            DataTable dt = new DataTable();
            string Table = "Customer";
            string Column = "agid,agname";
            string Condition = "";
            string OrderBy = "agname";


            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.Customer MovList = new BE.Customer();
                    MovList.AGID = Convert.ToInt16(row["agid"]);
                    MovList.AGName = Convert.ToString(row["agname"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }
        public List<BE.AccountProformaEntities> accountmasterdetails()
        {
            List<BE.AccountProformaEntities> locationDL = new List<BE.AccountProformaEntities>();
            DataTable dt = new DataTable();
            string Table = "Customer_tariffmaster";
            string Column = "Distinct TariffID,TariffDescription";
            string Condition = "";
            string OrderBy = "TariffDescription";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.AccountProformaEntities locationList = new BE.AccountProformaEntities();
                    locationList.ID = Convert.ToInt32(row["TariffID"]);
                    locationList.Name = Convert.ToString(row["TariffDescription"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<BE.Transport> GetTransportDetails()

        {
            List<BE.Transport> locationDL = new List<BE.Transport>();
            DataTable dt = new DataTable();
            string Table = "Transporter";
            string Column = "Transid,TransName";
            string Condition = "";
            string OrderBy = "TransName";


            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.Transport MovList = new BE.Transport();
                    MovList.Transport_Type_ID = Convert.ToInt16(row["Transid"]);
                    MovList.Transport_Type = Convert.ToString(row["TransName"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }
        public string SaveImportSettingTariff(DataTable ImportData, int Userid, string commodity)
        {
            string Message = ""; string ScanType = ""; string strcurrency = ""; int MaxDays = 0; int isTax = 0; int isSplit = 0;
            int Transid = 0;
            int Portname = 0;
            int Location = 0;
            int Jotype = 0;
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            DataTable dt = new DataTable();
            DataTable dtDelete = new DataTable();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int k = 0; k < ImportData.Rows.Count; k++)
            {
                DataTable PortsDS = new DataTable();
                DataTable PortsDS1 = new DataTable();
                //if (ImportData.Rows[k].Field<string>("AccountingID")  == "30016")
                //{
                //    ScanType = "M";
                //}
                //if (ImportData.Rows[k].Field<string>("AccountingID") == "30017" || ImportData.Rows[k].Field<string>("AccountingID") == "30018")
                //{
                //    ScanType = "F";
                //}
                //if (ImportData.Rows[k].Field<string>("AccountingID") == "30171")
                //{
                //    ScanType = "D";
                //}

                MaxDays = Convert.ToInt32(ImportData.Rows[k].Field<string>("Freedays"));

                MaxDays = 0;


                isTax = 0;


                strcurrency = "I";

                isSplit = 0;

                string maxif = "";

                string tolocationid = "";
                int Gettolocatioin = 0;

                tolocationid = "select locationid from ext_location_m where Location ='" + ImportData.Rows[k].Field<string>("Tolocation") + "'";
                PortsDS1 = db.sub_GetDatatable(tolocationid);
                if (PortsDS1.Rows.Count > 0)
                {
                    Gettolocatioin = PortsDS1.Rows[0].Field<int>("locationid");
                }




                string Locationid = "";
                int GetLocation = 0;
                Locationid = "select locationid from ext_location_m where Location ='" + ImportData.Rows[k].Field<string>("Location") + "'";
                PortsDS1 = db.sub_GetDatatable(Locationid);
                if (PortsDS1.Rows.Count > 0)
                {
                    GetLocation = PortsDS1.Rows[0].Field<int>("locationid");
                }


                string GetJoTYpe = "";



                string Accountid = "";
                int getaccountid = 0;
                DataTable Accountdl = new DataTable();
                Accountid = "select AccountID from accountmaster where AccountName ='" + ImportData.Rows[k].Field<string>("Accounting") + "'";
                Accountdl = db.sub_GetDatatable(Accountid);
                if (Accountdl.Rows.Count > 0)
                {
                    getaccountid = Convert.ToInt32(Accountdl.Rows[0][0]);
                }

                string Driverid = "";
                int getDriverid = 0;
                DataTable getDriverDL = new DataTable();
                Driverid = "select DriverTypeID from driver_type where Drivertype ='" + ImportData.Rows[k].Field<string>("Drivertype") + "'";
                getDriverDL = db.sub_GetDatatable(Driverid);
                if (getDriverDL.Rows.Count > 0)
                {
                    getDriverid = Convert.ToInt32(getDriverDL.Rows[0][0]);
                }




                string strSQL1 = "";
                strSQL1 = "INSERT INTO customer_tariffdetails(tariffID,deliverytype,EffectiveFrom,EffectiveUpto,AccountID,ScanType,ContainerSize,ContainerType,SorF,SlabID,fixedAmt,insRate,RateperMT,MaxDays,TaxID,InvoiceType,IsSQM,isTotDays, Currency_type,TransType,PortName,FreeDay,CommodityID,LocationID,JoType,tolocation,Drivertype) VALUES";
                strSQL1 += " ('" + ImportData.Rows[k].Field<string>("TariffID") + "','" + ImportData.Rows[k].Field<string>("DeliveryType") + "','" + Convert.ToDateTime(ImportData.Rows[k].Field<string>("From")).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(ImportData.Rows[k].Field<string>("TO")).ToString("yyyyMMdd") + "',";
                strSQL1 += " '" + getaccountid + "','" + 0 + "','" + ImportData.Rows[k].Field<string>("Size") + "','" + ImportData.Rows[k].Field<string>("Type") + "','" + GetJoTYpe + "',";
                strSQL1 += " '" + 0 + "','" + ImportData.Rows[k].Field<string>("FixedAmt") + "','" + 0 + "','" + 0 + "','" + MaxDays + "',";
                strSQL1 += " '" + 0 + "','" + GetJoTYpe + "','" + 0 + "','" + 0 + "', '" + strcurrency + "','" + 0 + "','" + 0 + "','" + ImportData.Rows[k].Field<string>("Freedays") + "','" + GetJoTYpe + "','" + GetLocation + "','" + 1 + "','" + Gettolocatioin + "','" + getDriverid + "')";
                PortsDS1 = db.sub_GetDatatable(strSQL1);



                Message = "Record saved successfully.";



            }
            string Srtdelete = "";
            Srtdelete = "delete from Tempimport_tariffdetails where addedby ='" + Userid + "'  ";
            dtDelete = db.sub_GetDatatable(Srtdelete);
            return Message;
        }




        public List<BE.TariffAddDetailsEntites> GetAddTabledetails(string TariffID, string From, string TO, string Accounting
          , string Size, string ChargesBased, string FixedAmt, string Days, string rate
          , string Slabid, string ScanType, string Location, string Jotype, string Commodity, string TransType,
          string Port, string Insurance, string AccountingID, DataTable dataTable, DataTable dataTable1, string TaxID, string InvoiceType, string fromlocation, string tolocation)
        {
            List<BE.TariffAddDetailsEntites> Getdetails = new List<BE.TariffAddDetailsEntites>();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable1.Rows.Count; j++)
                {
                    if (Accounting != "")
                    {

                        BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                        DPDTList.TariffID = Convert.ToString(TariffID);
                        DPDTList.DeliveryType = Convert.ToString(dataTable.Rows[i].Field<string>("DeliveryType"));
                        DPDTList.From = Convert.ToString(Convert.ToDateTime(From).ToString("dd-MMM-yyyy"));
                        DPDTList.TO = Convert.ToString(Convert.ToDateTime(TO).ToString("dd-MMM-yyyy"));
                        DPDTList.Accounting = Convert.ToString(Accounting);
                        DPDTList.Size = Convert.ToString(Size);
                        DPDTList.Type = Convert.ToString(dataTable1.Rows[j].Field<string>("Type"));


                        DPDTList.InvoiceType = Convert.ToString(InvoiceType);


                        string strSQL = "";
                        strSQL = " USP_Show_ChargeBasedON '" + ChargesBased + "'";
                        dt1 = db.sub_GetDatatable(strSQL);
                        if (dt1.Rows.Count > 0)
                        {
                            DPDTList.Ftype = Convert.ToString(dt1.Rows[0]["sorf"]);
                        }
                        else
                        {
                            DPDTList.Ftype = Convert.ToString(0);
                        }
                        if (Slabid != null)
                        {
                            DPDTList.Slabid = Convert.ToString(Slabid);
                        }
                        else
                        {
                            DPDTList.Slabid = Convert.ToString(0);
                        }
                        if (FixedAmt != null)
                        {
                            DPDTList.FixedAmt = Convert.ToString(FixedAmt);
                        }
                        else
                        {
                            DPDTList.FixedAmt = Convert.ToString(0);
                        }
                        if (Insurance != null)
                        {
                            DPDTList.Insurance = Convert.ToString(Insurance);
                        }
                        else
                        {
                            DPDTList.Insurance = Convert.ToString(0);
                        }
                        if (rate != null)
                        {
                            DPDTList.rate = Convert.ToString(rate);
                        }
                        else
                        {
                            DPDTList.rate = Convert.ToString(0);
                        }
                        if (Days != null)
                        {
                            DPDTList.Days = Convert.ToString(Days);
                        }
                        else
                        {
                            DPDTList.Days = Convert.ToString(0);
                        }
                        DPDTList.Tax = Convert.ToString(TaxID);

                        DPDTList.CurrencyType = Convert.ToString('I');
                        DPDTList.TransType = Convert.ToString(TransType);
                        DPDTList.Port = Convert.ToString(Port);
                        DPDTList.Freedays = Convert.ToString(Days);
                        DPDTList.Location = Convert.ToString(fromlocation);
                        DPDTList.Tolocation = Convert.ToString(tolocation);
                        DPDTList.Jotype = Convert.ToString(Jotype);
                        DPDTList.ScanType = Convert.ToString(ScanType);
                        DPDTList.AccountingID = Convert.ToString(AccountingID);

                        Getdetails.Add(DPDTList);

                    }
                }
            }

            return Getdetails;
        }
        public string GetTransportbillingPrefix(string Transid)
        {

            string message = "";
            string message1 = "";
            DataTable updateDl = new DataTable();
            DataTable updateD2 = new DataTable();


            updateD2 = importDB.GetTransportbillingcode(Transid);
            if (updateD2.Rows.Count > 0)
            {
                message1 = Convert.ToString(updateD2.Rows[0][0]);
            }


            return message1;
        }
        public List<BE.LocationMaster> getLocationForall()
        {
            List<BE.LocationMaster> locationDL = new List<BE.LocationMaster>();
            DataTable dt = new DataTable();

            dt = importDB.GetLocationMasterdetails();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.LocationMaster locationList = new BE.LocationMaster();
                    locationList.LocationID = Convert.ToInt32(row["LocationID"]);
                    locationList.Location = Convert.ToString(row["Location"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<BE.ImportProformaSearchGST> GetCustomertariffDetails(string CustomerName)
        {

            DataTable codeDL = new DataTable();
            codeDL = importDB.GetCustomerTariffDetails(CustomerName);
            List<BE.ImportProformaSearchGST> isCHACode = new List<BE.ImportProformaSearchGST>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    BE.ImportProformaSearchGST oper = new BE.ImportProformaSearchGST();
                    oper.GSTIn_uniqID = Convert.ToString(row["GSTIn_uniqID"]);
                    oper.GSTName = Convert.ToString(row["GSTName"]);
                    oper.State = Convert.ToString(row["State"]);
                    oper.GSTAddress = Convert.ToString(row["GSTAddress"]);
                    oper.GSTID = Convert.ToString(row["GSTID"]);
                    oper.state_Code = Convert.ToString(row["state_Code"]);
                    oper.FromDate = Convert.ToString(row["fromdate"]);
                    oper.Todate = Convert.ToString(row["todate"]);
                    oper.TariffID = Convert.ToString(row["TariffID"]);

                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }
        public List<BE.ActivityMaster> getStatusWiseactivity(string status)
        {
            List<BE.ActivityMaster> locationDL = new List<BE.ActivityMaster>();
            DataTable dt = new DataTable();


            dt = importDB.GetStatusWiseactivity(status);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.ActivityMaster locationList = new BE.ActivityMaster();
                    locationList.AutoID = Convert.ToInt32(row["autoid"]);
                    locationList.Activity = Convert.ToString(row["activity"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<BE.TariffAddDetailsEntites> GetCustomerTariffDetails(DataTable DriverPaymentDT, string TariffID, string AccountingName, Int32 userId)
        {
            //EN.DriverPaymentReco objdriverpaymententities = new EN.DriverPaymentReco();
            List<BE.TariffAddDetailsEntites> DriverPaymentList = new List<BE.TariffAddDetailsEntites>();
            if (DriverPaymentDT != null)
            {
                int count = 1;
                foreach (DataRow row in DriverPaymentDT.Rows)
                {

                    BE.TariffAddDetailsEntites DPDTList = new BE.TariffAddDetailsEntites();
                    DPDTList.TariffID = Convert.ToString(row["Tariff"]);
                    DPDTList.DeliveryType = Convert.ToString(row["Activity"]);
                    DPDTList.From = Convert.ToDateTime(row["From"]).ToString("dd MMM yyyy HH:mm");
                    DPDTList.TO = Convert.ToDateTime(row["To"]).ToString("dd MMM yyyy HH:mm");
                    DPDTList.Accounting = Convert.ToString(row["Account Name"]);
                    DPDTList.Size = Convert.ToString(row["Size"]);
                    DPDTList.Type = Convert.ToString(row["Type"]);
                    DPDTList.Ftype = Convert.ToString("");
                    DPDTList.Slabid = Convert.ToString("");
                    DPDTList.FixedAmt = Convert.ToString(row["Amount"]);
                    DPDTList.Insurance = Convert.ToString(row["Insurance"]);
                    DPDTList.rate = Convert.ToString(row["rate"]);
                    DPDTList.Location = Convert.ToString(row["From Location"]);
                    DPDTList.Tolocation = Convert.ToString(row["To Location"]);

                    DPDTList.Days = Convert.ToString("");

                    DPDTList.Tax = Convert.ToString(row["Tax"]);
                    DPDTList.InvoiceType = Convert.ToString("");
                    DPDTList.CurrencyType = Convert.ToString("");
                    DPDTList.TransType = Convert.ToString("");
                    DPDTList.Port = Convert.ToString("");
                    DPDTList.Freedays = Convert.ToString(row["Free Days"]);
                    DPDTList.Drivertype = Convert.ToString(row["Driver_type"]);

                    DPDTList.Jotype = Convert.ToString("");
                    DPDTList.ScanType = Convert.ToString("");
                    DPDTList.AccountingID = Convert.ToString("");
                    DPDTList.Entryid = Convert.ToString(count++);

                    DriverPaymentList.Add(DPDTList);


                }
            }
            return DriverPaymentList;
        }


        public List<BE.ExpHeadMasterEnt> InvoiceTypeDDL()
        {
            List<BE.ExpHeadMasterEnt> InvoiceType = new List<BE.ExpHeadMasterEnt>();
            DataTable InvType = new DataTable();
            string Table = "import_invoicetype";
            string Column = "ID,Invoice_Type";
            string Condition = "";
            string OrderBy = "Invoice_Type";

            InvType = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (InvType != null)
            {
                foreach (DataRow row in InvType.Rows)
                {
                    BE.ExpHeadMasterEnt INVTypeList = new BE.ExpHeadMasterEnt();
                    INVTypeList.InvTId = Convert.ToInt32(row["ID"]);
                    INVTypeList.InvType = Convert.ToString(row["Invoice_Type"]);

                    InvoiceType.Add(INVTypeList);
                }
            }
            return InvoiceType;
        }
        public List<BE.ExpHeadMasterEnt> HSNCodeDDL()
        {
            List<BE.ExpHeadMasterEnt> HSNSelect = new List<BE.ExpHeadMasterEnt>();
            DataTable dataTable = new DataTable();
            string Table = "HSN_MASTER";
            string Column = "isnull(ID,0)ID,HSN_Code";
            string Condition = "";
            string OrderBy = "HSN_Code";

            dataTable = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpHeadMasterEnt HSNSelectlist = new BE.ExpHeadMasterEnt();
                    HSNSelectlist.HSNID = Convert.ToString(row["HSN_Code"]);
                    HSNSelectlist.HSNCodeL = Convert.ToString(row["HSN_Code"]);

                    HSNSelect.Add(HSNSelectlist);
                }
            }
            return HSNSelect;
        }
        public List<BE.ExpHeadMasterEnt> getTaxName()
        {
            List<BE.ExpHeadMasterEnt> TaxDL = new List<BE.ExpHeadMasterEnt>();
            DataTable TaxNameDT = new DataTable();
            string Table = "settings_taxes";
            string Column = "isnull(settingsID,0)settingsID,taxname";
            string Condition = "";
            string OrderBy = "taxname";

            TaxNameDT = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TaxNameDT != null)
            {
                foreach (DataRow row in TaxNameDT.Rows)
                {
                    BE.ExpHeadMasterEnt CustomerList = new BE.ExpHeadMasterEnt();
                    CustomerList.TaxID = Convert.ToInt32(row["settingsID"]);
                    CustomerList.TaxName = Convert.ToString(row["taxname"]);

                    TaxDL.Add(CustomerList);
                }
            }
            return TaxDL;
        }
        public List<BE.ExpHeadMasterEnt> IMPGroupDDl()
        {
            List<BE.ExpHeadMasterEnt> IMPGroup = new List<BE.ExpHeadMasterEnt>();
            DataTable dataTable = new DataTable();
            string Table = "import_accgroups";
            string Column = "isnull(GroupID,0)GroupID,groupName";
            string Condition = "";
            string OrderBy = "groupName";

            dataTable = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpHeadMasterEnt IMPGLIst = new BE.ExpHeadMasterEnt();
                    IMPGLIst.IMPGID = Convert.ToInt32(row["GroupID"]);
                    IMPGLIst.IMPGName = Convert.ToString(row["groupName"]);

                    IMPGroup.Add(IMPGLIst);
                }
            }
            return IMPGroup;
        }
        public List<BE.ShipLines> getShipLines()
        {
            List<BE.ShipLines> ShipLinesDL = new List<BE.ShipLines>();
            DataTable ShipLinesDT = new DataTable();
            string Table = "ShipLines";
            string Column = "SLID,SLName,SaID";
            string Condition = "SLIsActive=1";
            string OrderBy = "SLName";

            ShipLinesDT = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ShipLinesDT != null)
            {
                foreach (DataRow row in ShipLinesDT.Rows)
                {
                    BE.ShipLines ShipLinesList = new BE.ShipLines();
                    ShipLinesList.SLID = Convert.ToInt32(row["SLID"]);
                    ShipLinesList.SLName = Convert.ToString(row["SLName"]);
                    ShipLinesList.SLCode = Convert.ToString(row["SaID"]);

                    ShipLinesDL.Add(ShipLinesList);
                }
            }
            return ShipLinesDL;
        }
        public List<BE.CHA> getCHA()
        {
            List<BE.CHA> ChaDL = new List<BE.CHA>();
            DataTable ChaDT = new DataTable();
            string Table = "CHA";
            string Column = "CHAID,CHAName";
            string Condition = "IsActive=1";
            string OrderBy = "CHAName";

            ChaDT = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ChaDT != null)
            {
                foreach (DataRow row in ChaDT.Rows)
                {
                    BE.CHA ChaList = new BE.CHA();
                    ChaList.CHANo = Convert.ToInt64(row["CHAID"]);
                    ChaList.CHAName = Convert.ToString(row["CHAName"]);

                    ChaDL.Add(ChaList);
                }
            }
            return ChaDL;
        }
        public List<BE.Consignee> GetImporter()

        {
            List<BE.Consignee> locationDL = new List<BE.Consignee>();
            DataTable dt = new DataTable();
            string Table = "Importer";
            string Column = "ImporterID,ImporterName";
            string Condition = "";
            string OrderBy = "ImporterName";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.Consignee MovList = new BE.Consignee();
                    MovList.ImporterID = Convert.ToInt32(row["ImporterID"]);
                    MovList.ImporterName = Convert.ToString(row["ImporterName"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }
        public List<BE.TariffGroup> GettaiffGroup()

        {
            List<BE.TariffGroup> locationDL = new List<BE.TariffGroup>();
            DataTable dt = new DataTable();
            string Table = "TARIFFGROUPMASTER";
            string Column = "Group_ID,Group_Name";
            string Condition = "";
            string OrderBy = "Group_Name";

            dt = importDB.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.TariffGroup MovList = new BE.TariffGroup();
                    MovList.Group_ID = Convert.ToInt16(row["Group_ID"]);
                    MovList.Group_Name = Convert.ToString(row["Group_Name"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }

        public List<BE.DriverTypeEntities> Driver_type()
        {
            List<BE.DriverTypeEntities> locationDL = new List<BE.DriverTypeEntities>();
            DataTable dt = new DataTable();


            dt = importDB.GetDrivertype();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.DriverTypeEntities locationList = new BE.DriverTypeEntities();
                    locationList.DriverID = Convert.ToInt32(row["DriverTypeID"]);
                    locationList.DriverType = Convert.ToString(row["Drivertype"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
    }
}
