using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using System.Data;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.DomesticSalesRegister
{
    public class DomesticSalesRegister
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public string ValidationForCargoType(DataTable CargoType)
        {
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            if (CargoType != null)
            {
                for (int i = 0; i < CargoType.Rows.Count; i++)
                {
                    DataTable PortsDS = new DataTable();
                    PortsDS = db.sub_GetDatatable("USP_Check_Duplicate_Domestic_CargoType '" + CargoType.Rows[i].Field<string>(0) + "','" + CargoType.Rows[i].Field<string>(2) + "'");

                    if (PortsDS.Rows.Count > 0)
                    {

                        message += "Following Batch Found Already : " + PortsDS.Rows[0].Field<string>("Batch") + " against Invoice No: " + PortsDS.Rows[0].Field<string>("GSTInvoiceno") + ".Cannot proceed.";


                    }
                    else
                    {
                        message += "";
                    }

                }
                if ((message != ""))
                {
                    message2 = message;
                }
            }
            return message2;
        }

        public List<EN.DomesticCargoEntites> GetDomesticCargoType(DataTable CargojoType)
        {

            List<EN.DomesticCargoEntites> DriverPaymentList = new List<EN.DomesticCargoEntites>();
            if (CargojoType != null)
            {

                foreach (DataRow row in CargojoType.Rows)
                {
                    EN.DomesticCargoEntites DPDTList = new EN.DomesticCargoEntites();

                    DPDTList.Batch = Convert.ToString(row["Batch"]);
                    DPDTList.Truckno = Convert.ToString(row["Truck No"]);
                    DPDTList.InvoiceNo = Convert.ToString(row["GST Invoice Number"]);
                    DPDTList.Sold = Convert.ToString(row["Sold to Party"]);
                    DPDTList.Ship = Convert.ToString(row["Ship to Party"]);
                    DPDTList.Destination = Convert.ToString(row["Destination"]);
                    DPDTList.Gross = Convert.ToString(row["Gross Wt"]);
                    DPDTList.InvoiceQty = Convert.ToString(row["Invoice Qty"]);
                    DPDTList.Thickness = Convert.ToString(row["Thickness"]);
                    DPDTList.Diameter = Convert.ToString(row["Width/Diameter"]);
                    DPDTList.BillingDate = Convert.ToString(row["Billing Date"]);
                    DPDTList.LRNo = Convert.ToString(row["LR No."]);
                    DPDTList.EWayBillNo = Convert.ToString(row["E-Way Bill No."]);
                    DPDTList.EWayBillValidity = Convert.ToString(row["E-Way Bill Validity"]);
                    DPDTList.Allocation = Convert.ToString(row["Allocation  zone"]);
                    DPDTList.ROUTES = Convert.ToString(row["MM ROUTES"]);
                    DPDTList.IGST = Convert.ToString(row["Integrated GST"]);
                    DPDTList.CGST = Convert.ToString(row["Central GST"]);
                    DPDTList.SGST = Convert.ToString(row["State GST"]);
                    DPDTList.AssessValue = Convert.ToString(row["Ass_value"]);
                    DPDTList.TotalValue = Convert.ToString(row["Total Value"]);
                    DPDTList.Length = Convert.ToString(row["Length"]);
                    DriverPaymentList.Add(DPDTList);


                }
            }
            return DriverPaymentList;
        }

        public List<EN.CustomerMaster> GetCustomer()
        {
            List<EN.CustomerMaster> ShippersDL = new List<EN.CustomerMaster>();
            DataSet ShippersDT = new DataSet();
         

            ShippersDT = trackerdatamanager.GetDropDownListForJoType();
            if (ShippersDT.Tables[0] != null)
            {
                foreach (DataRow row in ShippersDT.Tables[0].Rows)
                {
                    EN.CustomerMaster ShippersList = new EN.CustomerMaster();
                    ShippersList.AGID = Convert.ToInt32(row["agid"]);
                    ShippersList.AGName = Convert.ToString(row["AGName"]);

                    ShippersDL.Add(ShippersList);
                }
            }
            return ShippersDL;
        }

        public List<EN.CommodityGroup> GetCommodity()
        {
            List<EN.CommodityGroup> ShippersDL = new List<EN.CommodityGroup>();
            DataSet ShippersDT = new DataSet();


            ShippersDT = trackerdatamanager.GetDropDownListForJoType();
            if (ShippersDT.Tables[1] != null)
            {
                foreach (DataRow row in ShippersDT.Tables[1].Rows)
                {
                    EN.CommodityGroup ShippersList = new EN.CommodityGroup();
                    ShippersList.Commodity_Group_ID = Convert.ToInt32(row["ID"]);
                    ShippersList.Commodity_Group_Name = Convert.ToString(row["Commodity"]);

                    ShippersDL.Add(ShippersList);
                }
            }
            return ShippersDL;
        }

        public string AddDomesticCargoType(string JoDate, string Customer, string Commodity, DataTable Cargotype)
        {
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            string message = "";

            DataTable VendorDataDL = new DataTable();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            parameterList.Add("JoDate", Convert.ToDateTime(JoDate).ToString("dd MMM yyyy HH:mm"));
            parameterList.Add("Customer", Customer);
            parameterList.Add("Commodity", Commodity);
            VendorDataDL = db.DataTableAddTypeTable("USP_INSERT_GENERATE_CARGO_JO", parameterList, Cargotype, "GenerateCargoType", "@GenerateCargoType");

           
            return message;

        }
        public EN.DomesticCargoEntites GetUploaddetailsForCopy(string Invoiceno)
        {
            EN.DomesticCargoEntites ShippersDL = new EN.DomesticCargoEntites();
            DataTable ShippersDT = new DataTable();


            ShippersDT = trackerdatamanager.GetInvoiceDetailsForUploadFile(Invoiceno);
            if (ShippersDT != null)
            {
                foreach (DataRow row in ShippersDT.Rows)
                {

                    ShippersDL.InvoiceNo = Convert.ToString(row["GST INVOCIE NO"]);
                    ShippersDL.Batch = Convert.ToString(row["BATCH"]);
                    ShippersDL.JoNo = Convert.ToString(row["Jo No"]);
                    ShippersDL.Jodate = Convert.ToString(row["Jo Date"]);
                    ShippersDL.Truckno = Convert.ToString(row["TRUCK NO"]);
                    ShippersDL.Destination = Convert.ToString(row["DESTINATION"]);
                    ShippersDL.Customer = Convert.ToString(row["Customer Name"]);
                    ShippersDL.BillingDate = Convert.ToString(row["BILLINGDATE"]);


                }
            }
            return ShippersDL;
        }

        public List<EN.DocList> GetDocsDetails()
        {
            List<EN.DocList> ShippersDL = new List<EN.DocList>();
            DataTable ShippersDT = new DataTable();


            ShippersDT = trackerdatamanager.GetDropDownListDocs();
            if (ShippersDT != null)
            {
                foreach (DataRow row in ShippersDT.Rows)
                {
                    EN.DocList ShippersList = new EN.DocList();
                    ShippersList.DocID = Convert.ToInt32(row["Doc ID"]);
                    ShippersList.DocName = Convert.ToString(row["Docs Name"]);

                    ShippersDL.Add(ShippersList);
                }
            }
            return ShippersDL;
        }

        public List<EN.DomesticCargoEntites> GetInvoiceAttachment(string InvoiceNO)
        {
            List<EN.DomesticCargoEntites> AttachmentList = new List<EN.DomesticCargoEntites>();
            DataTable AttachmentDT = new DataTable();
            int i = 0;
            AttachmentDT = trackerdatamanager.GetInvoiceDocumentList(InvoiceNO);
            if (AttachmentDT != null)
            {
                foreach (DataRow row in AttachmentDT.Rows)
                {
                    EN.DomesticCargoEntites AttachmentDataList = new EN.DomesticCargoEntites();
                    i++;
                    AttachmentDataList.SrNo = i;
                    AttachmentDataList.DocID = Convert.ToInt32(row["DocID"]);
                    AttachmentDataList.DocumentType = Convert.ToString(row["DocumentType"]);
                    AttachmentDataList.FilePath = Convert.ToString(row["FilePath"]);


                    AttachmentList.Add(AttachmentDataList);
                }
            }
            return AttachmentList;
        }

        public EN.DomesticCargoEntites getAttachment(int DocID, string LRNo)
        {
            EN.DomesticCargoEntites Docdetails = new EN.DomesticCargoEntites();
            DataTable CompanyDL = new DataTable();
            CompanyDL = trackerdatamanager.GetInvoiceDocumentList1(DocID, LRNo);
            if (CompanyDL != null)
            {
                foreach (DataRow row in CompanyDL.Rows)
                {

                    Docdetails.DocID = Convert.ToInt32(row["DocID"]);
                    Docdetails.DocumentType = Convert.ToString(row["DocumentType"]);
                    Docdetails.FilePath = Convert.ToString(row["FilePath"]);

                }
            }
            return Docdetails;
        }
    }
}
