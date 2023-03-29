using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using BE = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;


namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.EmptyOut
{
    public class EmptyOut
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<BE.EyardOut> getLoaction()
        {
            List<BE.EyardOut> ExpensesrDL = new List<BE.EyardOut>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "Ext_Location_M";
            string Column = "isnull(LocationID,0)LocationID,Location";
            string Condition = "";
            string OrderBy = "Location";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.EyardOut CustomerList = new BE.EyardOut();
                    CustomerList.LocationIID = Convert.ToInt32(row["LocationID"]);
                    CustomerList.Location = Convert.ToString(row["Location"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }

        public List<BE.EyardOut> getVessel()
        {
            List<BE.EyardOut> ExpensesrDL = new List<BE.EyardOut>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "vessels";
            string Column = "isnull(VesselID,0)VesselID,VesselName";
            string Condition = "";
            string OrderBy = "VesselName";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.EyardOut CustomerList = new BE.EyardOut();
                    CustomerList.VesselID = Convert.ToInt32(row["VesselID"]);
                    CustomerList.VesselName = Convert.ToString(row["VesselName"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }

        public List<BE.EyardOut> getActivity()
        {
            List<BE.EyardOut> ExpensesrDL = new List<BE.EyardOut>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "trackerNCFS.dbo.ActivityMaster";
            string Column = "isnull(AutoID,0)AutoID,Activity";
            string Condition = "islr=1 ";
            string OrderBy = "Activity";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.EyardOut CustomerList = new BE.EyardOut();
                    CustomerList.AutoID = Convert.ToInt32(row["AutoID"]);
                    CustomerList.Activity = Convert.ToString(row["Activity"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }


        public List<BE.EyardOut> getLineName()
        {
            List<BE.EyardOut> ExpensesrDL = new List<BE.EyardOut>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "trackerNCFS.dbo.ShipLines";
            string Column = "isnull(SLID,0)SLID,SLName";
            string Condition = "";
            string OrderBy = "SLNAME";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.EyardOut CustomerList = new BE.EyardOut();
                    CustomerList.SLID = Convert.ToInt32(row["SLID"]);
                    CustomerList.SLName = Convert.ToString(row["SLName"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }


        public List<BE.EyardOut> getCustomer()
        {
            List<BE.EyardOut> ExpensesrDL = new List<BE.EyardOut>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "trackerNCFS.dbo.CUSTOMER";
            string Column = "isnull(AGID,0)AGID,AGNAME";
            string Condition = "";
            string OrderBy = "AGNAME";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.EyardOut CustomerList = new BE.EyardOut();
                    CustomerList.AGID = Convert.ToInt32(row["AGID"]);
                    CustomerList.AGNAME = Convert.ToString(row["AGNAME"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }


        public List<BE.EyardOut> getLocation()
        {
            List<BE.EyardOut> ExpensesrDL = new List<BE.EyardOut>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "trackerNCFS.dbo.Ext_Location_M";
            string Column = "isnull(LocationID,0)LocationID,Location";
            string Condition = "";
            string OrderBy = "Location";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.EyardOut CustomerList = new BE.EyardOut();
                    CustomerList.LocationID = Convert.ToInt32(row["LocationID"]);
                    CustomerList.Locations = Convert.ToString(row["Location"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }


        public List<BE.EyardOut> getTransporter()
        {
            List<BE.EyardOut> ExpensesrDL = new List<BE.EyardOut>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "trackerNCFS.dbo.Transporter";
            string Column = "isnull(TransID,0)TransID,TransName";
            string Condition = "";
            string OrderBy = "TransName";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.EyardOut CustomerList = new BE.EyardOut();
                    CustomerList.TransID = Convert.ToInt32(row["TransID"]);
                    CustomerList.TransName = Convert.ToString(row["TransName"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }

        public List<BE.EyardOut> getType()
        {
            List<BE.EyardOut> ExpensesrDL = new List<BE.EyardOut>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "containertype";
            string Column = "isnull(ContainerTypeID,0)ContainerTypeID,ContainerType";
            string Condition = "ctr_Is_Active=1";
            string OrderBy = "ContainerType";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.EyardOut CustomerList = new BE.EyardOut();
                    CustomerList.ContainerTypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    CustomerList.ContainerType = Convert.ToString(row["ContainerType"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }

        public string SavepartyDebitEntry(DataTable Invoicedata, int Lrno, string LrDate, string ShippingLine, string Customer,
     string Consignee,int Userid)
        {
            string Message = "";
             
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

             
            string strSql = "select ISNULL(max(LRNO),0)+1 as LRNO from Lorry_receipt  with(xlock) ";
            DataTable dt1 = db.sub_GetDatatable(strSql);
            if (dt1.Rows.Count > 0)
            {
                Lrno = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("Lrno", Lrno);
            parameterList.Add("LrDate", LrDate);
            parameterList.Add("ShippingLine", ShippingLine);
            parameterList.Add("Customer", Customer);
            parameterList.Add("Consignee", Consignee);
            parameterList.Add("ADDEDBY", Userid);

            for (int k = 0; k < Invoicedata.Rows.Count; k++)
            {
                if (Invoicedata.Rows[k].Field<string>("Activity") == "58")
                {
                    strSql = "select *  from Lorry_Receipt where Iscancel=0 and isout=0  and ContainerNo='" + Invoicedata.Rows[k].Field<string>("ContainerNo") + "'";
                    DataTable dt = db.sub_GetDatatable(strSql);
                    if (dt.Rows.Count > 0)
                    {
                        Message = "Batch no already saved !";
                    }
                }
            }

            if (Message == "")
            {
                int i = db.AddTypeTableData("USP_INSERT_LORRY_RECEIPT_Web", parameterList, Invoicedata, "LorryReceipt", "@LorryReceipt");


                if (i > 0)
                {
                    string Messageget = "Record saved successfully";
                    Message = Messageget + ',' + Lrno;

                }
                else
                {
                    Message = "Record not saved please try again!";

                }
            }
              
                
         
            return Message;
        }

        public List<BE.LorryReceiptReport> getLorryReceiptReport(string FromDate, string ToDate, string category, string size)
        {
            DataTable Tues = new DataTable();
            Tues = trackerdatamanager.DomesticLorryReceiptReportList(FromDate, ToDate, category, size);
            List<BE.LorryReceiptReport> ContainerList = new List<BE.LorryReceiptReport>();
            int Count = 1;
            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    BE.LorryReceiptReport Tues1 = new BE.LorryReceiptReport();
                    Tues1.SrNo = Convert.ToInt32(Count++);
                    Tues1.LRNo = Convert.ToString(row["LR No"]);
                    Tues1.LRDate = Convert.ToString(row["LR Date"]);
                    Tues1.BookingNo = Convert.ToString(row["Booking No"]);
                    Tues1.AGName = Convert.ToString(row["Customer Name"]);
                    Tues1.ShippingLine = Convert.ToString(row["Shipping Line"]);
                    Tues1.FromLocation = Convert.ToString(row["From Location"]);
                    Tues1.ToLocation = Convert.ToString(row["To Location"]);
                    Tues1.ContainerNo = Convert.ToString(row["Container No"]);
                    Tues1.Size = Convert.ToString(row["Size"]);
                    Tues1.Type = Convert.ToString(row["Type"]);
                    Tues1.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    Tues1.AgentSeal = Convert.ToString(row["Agent Seal"]);
                    Tues1.BOENo = Convert.ToString(row["BOE No"]);
                    Tues1.AddedBy = Convert.ToString(row["Added By"]);
                    Tues1.Remarks = Convert.ToString(row["Remarks"]);


                    if (category == "ALL")
                    {
                        Tues1.Activity = Convert.ToString(row["Activity"]);
                    }
                    else
                        Tues1.Activity = " ";


                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        public string DomesticUpdateHub(DataTable Invoicedata, string Lrno, string LrDate)
        {
            string Message = "";

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();


            //string strSql = "select ISNULL(max(LRNO),0)+1 as LRNO from Lorry_receipt  with(xlock) ";
            //DataTable dt1 = db.sub_GetDatatable(strSql);
            //if (dt1.Rows.Count > 0)
            //{
            //    Lrno = Convert.ToInt32(dt1.Rows[0][0].ToString());
            //}

            Dictionary<object, object> parameter = new Dictionary<object, object>();
            parameter.Add("Lrno", Lrno);
            parameter.Add("LrDate", LrDate);

            int i = db.DomesticTypeTableData("USP_Update_Domestic_Hub_LORRY_RECEIPT_Web", parameter, Invoicedata, "Domestic_Hub_LorryReceipt", "@Domestic_Hub_LorryReceipt");


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


    }
}