using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using System.Data;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.LRClosing
{
    public class LRClosing
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.LRListOpen> GetLRList(string fromDate, string ToDate, string Searchcerteria,string txtNumber)
        {
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetLRList(fromDate, ToDate, Searchcerteria, txtNumber);
            List<EN.LRListOpen> LRDL = new List<EN.LRListOpen>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LRListOpen LRList = new EN.LRListOpen();

                    LRList.LRNo = Convert.ToInt32(row["LR No"]);
                    LRList.LRDate = Convert.ToString(row["LR Date"]);
                    LRList.ContainerNo = Convert.ToString(row["Container No"]);
                    LRList.Size = Convert.ToString(row["Size"]);
                    LRList.Type = Convert.ToString(row["Type"]);
                    LRList.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    LRList.Customer = Convert.ToString(row["Customer"]);
                    LRList.LineName = Convert.ToString(row["Line Name"]);
                    LRList.FromLocation = Convert.ToString(row["From Location"]);
                    LRList.ToLocation = Convert.ToString(row["To Location"]);
                    LRList.DwellDays = Convert.ToString(row["Dwell Days"]);
                    LRList.PreparedBy = Convert.ToString(row["Prepared By"]);                    

                    LRDL.Add(LRList);
                }

            }
            return LRDL;
        }
        public EN.LRListOpen GetDocList(int LRNo)
        {
            EN.LRListOpen objLRentities = new EN.LRListOpen();

            DataSet ds = new DataSet();
            ds = trackerdatamanager.DocDropDownList(LRNo);
            List<EN.DocList> DocList = new List<EN.DocList>();
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EN.DocList LRList = new EN.DocList();

                    LRList.DocID = Convert.ToInt32(row["DocID"]);
                    LRList.DocName = Convert.ToString(row["DocumentType"]);

                    DocList.Add(LRList);
                }
            }
            if (ds.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    //EN.LRListOpen LRList = new EN.LRListOpen();

                    objLRentities.ContainerNo = Convert.ToString(row["CONTAINERNO"]);
                    objLRentities.VehicleNo = Convert.ToString(row["VEHICLENO"]);
                    objLRentities.FromLocation = Convert.ToString(row["FROMlOCATION"]);
                    objLRentities.ToLocation = Convert.ToString(row["TOLOCATION"]);
                    objLRentities.FactoryReportingTime = Convert.ToString(row["Factory Reporting Time"]);
                    objLRentities.FactoryInTime = Convert.ToString(row["Factory In Time"]);
                    objLRentities.FactoryOutTime = Convert.ToString(row["Factory Out Time"]);
                    objLRentities.LRDate = Convert.ToString(row["LR Date"]);

                    objLRentities.DocID = 0;
                    
                    //DocList.Add(LRList);
                }
            }
            objLRentities.DocList = DocList;
            //objLRentities. = DocList;

            return objLRentities;
        }

        public List<EN.LocationMaster> getLocation()
        {
            List<EN.LocationMaster> locationDL = new List<EN.LocationMaster>();
            DataTable dt = new DataTable();
            string Table = "Ext_Location_M";
            string Column = "LocationID,Location";
            string Condition = "";
            string OrderBy = "Location";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.LocationMaster locationList = new EN.LocationMaster();
                    locationList.LocationID = Convert.ToInt32(row["LocationID"]);
                    locationList.Location = Convert.ToString(row["Location"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<EN.ActivityMaster> GetActivity()
        {
            List<EN.ActivityMaster> locationDL = new List<EN.ActivityMaster>();
            DataTable dt = new DataTable();
            string Table = "ActivityMaster";
            string Column = "Autoid,Activity";
            string Condition = "";
            string OrderBy = "Activity";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ActivityMaster locationList = new EN.ActivityMaster();
                    locationList.AutoID = Convert.ToInt32(row["Autoid"]);
                    locationList.Activity = Convert.ToString(row["Activity"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }

        public List<EN.ShipLines> getShipLines()
        {
            List<EN.ShipLines> ShipLinesDL = new List<EN.ShipLines>();
            DataTable ShipLinesDT = new DataTable();
            string Table = "ShipLines";
            string Column = "SLID,SLName,SaID";
            string Condition = "SLIsActive=1";
            string OrderBy = "SLName";

            ShipLinesDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ShipLinesDT != null)
            {
                foreach (DataRow row in ShipLinesDT.Rows)
                {
                    EN.ShipLines ShipLinesList = new EN.ShipLines();
                    ShipLinesList.SLID = Convert.ToInt32(row["SLID"]);
                    ShipLinesList.SLName = Convert.ToString(row["SLName"]);
                   // ShipLinesList.SLCode = Convert.ToString(row["SaID"]);

                    ShipLinesDL.Add(ShipLinesList);
                }
            }
            return ShipLinesDL;
        }

        public List<EN.MovementCountEntities> getMovCount()
        {
            List<EN.MovementCountEntities> locationDL = new List<EN.MovementCountEntities>();
            DataTable dt = new DataTable();
            string Table = "Mov_Count";
            string Column = "MovCount,MovCount";
            string Condition = "";
            string OrderBy = "MovCountID";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.MovementCountEntities MovList = new EN.MovementCountEntities();
                    MovList.MovCountID = Convert.ToString(row["MovCount"]);
                    MovList.MovCount = Convert.ToString(row["MovCount"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }
        public List<EN.TransporterEntities> getTranspoter()
        {
            List<EN.TransporterEntities> transpoterDL = new List<EN.TransporterEntities>();
            DataTable dt = new DataTable();
            string Table = "Transporter";
            string Column = "TransID,TransName";
            string Condition = "";
            string OrderBy = "TransName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TransporterEntities transpoterList = new EN.TransporterEntities();
                    transpoterList.TransID = Convert.ToInt32(row["TransID"]);
                    transpoterList.TransName = Convert.ToString(row["TransName"]);
                    transpoterDL.Add(transpoterList);
                }
            }
            return transpoterDL;
        }

        public List<EN.MovementTypeEntities> getMovType()

        {
            List<EN.MovementTypeEntities> locationDL = new List<EN.MovementTypeEntities>();
            DataTable dt = new DataTable();
            string Table = "Transport_MOV_Type";
            string Column = "Mov_ID,Mov_Type";
            string Condition = "";
            string OrderBy = "Mov_ID";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.MovementTypeEntities MovList = new EN.MovementTypeEntities();
                    MovList.Mov_ID = Convert.ToInt16(row["Mov_ID"]);
                    MovList.Mov_Type = Convert.ToString(row["Mov_Type"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }

        public List<EN.Customer> GetCustomer()

        {
            List<EN.Customer> locationDL = new List<EN.Customer>();
            DataTable dt = new DataTable();
            string Table = "CUSTOMER";
            string Column = "AGID,AGNAME";
            string Condition = "";
            string OrderBy = "AGNAME";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.Customer MovList = new EN.Customer();
                    MovList.AGID = Convert.ToInt16(row["AGID"]);
                    MovList.AGName = Convert.ToString(row["AGName"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }


        public string GetENtryid(string containerno, string Activitye)
        {
            string message = "";
            DataTable Dt = new DataTable();
            string bookingno = "";
            Dt = trackerdatamanager.GetEntryIdForLorryReceipt(containerno, Activitye, bookingno);
            if (Dt != null)
            {
                try
                {
                    var message1 = Convert.ToString(Dt.Rows[0]["Status"]);
                    message = message1;
                }
                catch (Exception ex)
                {
                    ;
                    return message = "";
                }

            }
            return message;
        }

        public List<EN.CHA> getCHA()
        {
            List<EN.CHA> ChaDL = new List<EN.CHA>();
            DataTable ChaDT = new DataTable();
            string Table = "CHA";
            string Column = "CHAID,CHAName";
            string Condition = "IsActive=1";
            string OrderBy = "CHAName";

            ChaDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
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

        public List<EN.Consignee> GetImporter()

        {
            List<EN.Consignee> locationDL = new List<EN.Consignee>();
            DataTable dt = new DataTable();
            string Table = "Importer";
            string Column = "ImporterID,ImporterName";
            string Condition = "";
            string OrderBy = "ImporterName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.Consignee MovList = new EN.Consignee();
                    MovList.ImporterID = Convert.ToInt16(row["ImporterID"]);
                    MovList.ImporterName = Convert.ToString(row["ImporterName"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }
        public List<EN.TariffGroup> GettaiffGroup()

        {
            List<EN.TariffGroup> locationDL = new List<EN.TariffGroup>();
            DataTable dt = new DataTable();
            string Table = "TARIFFGROUPMASTER";
            string Column = "Group_ID,Group_Name";
            string Condition = "";
            string OrderBy = "Group_Name";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.TariffGroup MovList = new EN.TariffGroup();
                    MovList.Group_ID = Convert.ToInt16(row["Group_ID"]);
                    MovList.Group_Name = Convert.ToString(row["Group_Name"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }

        public List<EN.importtariffdetails> Getimporttariffdetails()
        {
            List<EN.importtariffdetails> locationDL = new List<EN.importtariffdetails>();
            DataTable dt = new DataTable();
            string Table = "import_tariffmaster";
            string Column = "Distinct TariffID,TariffDescription";
            string Condition = "";
            string OrderBy = "TariffDescription";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.importtariffdetails locationList = new EN.importtariffdetails();
                    locationList.TariffID = Convert.ToInt32(row["TariffID"]);
                    locationList.TariffDescription = Convert.ToString(row["TariffDescription"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<EN.ContainerType> GetContainerType()
        {
            List<EN.ContainerType> locationDL = new List<EN.ContainerType>();
            DataTable dt = new DataTable();
            string Table = "ContainerType";
            string Column = "Distinct ContainerTypeID,ContainerType";
            string Condition = "";
            string OrderBy = "ContainerType";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ContainerType locationList = new EN.ContainerType();
                    locationList.ContainerTypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    locationList.ContainerTypeName = Convert.ToString(row["ContainerType"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }


        public List<EN.ImportAccountMaster> GetAccountHead()
        {
            List<EN.ImportAccountMaster> locationDL = new List<EN.ImportAccountMaster>();
            DataTable dt = new DataTable();
            string Table = "import_accountmaster";
            string Column = "Distinct AccountID,AccountName";
            string Condition = "";
            string OrderBy = "AccountName";

            dt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ImportAccountMaster locationList = new EN.ImportAccountMaster();
                    locationList.AccountID = Convert.ToInt32(row["AccountID"]);
                    locationList.AccountName = Convert.ToString(row["AccountName"]);
                    locationDL.Add(locationList);
                }
            }
            return locationDL;
        }
        public List<EN.DomesticHubLrEntities> LRContainerSearch(String Containerno)
        {
            List<EN.DomesticHubLrEntities> ContainerList = new List<EN.DomesticHubLrEntities>();
            DataTable ContainerDT = new DataTable();
            ContainerDT = trackerdatamanager.LRContainerSearch(Containerno);

            if (ContainerDT != null)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.DomesticHubLrEntities ContainerSearch = new EN.DomesticHubLrEntities();
                    ContainerSearch.ContainerTypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    ContainerSearch.ContainerType = Convert.ToString(row["Type"]);
                    ContainerSearch.LineID = Convert.ToInt32(row["LineID"]);
                    ContainerSearch.LineName = Convert.ToString(row["Line Name"]);
                    ContainerSearch.Size = Convert.ToInt32(row["Size"]);
                    ContainerSearch.ENTRYID = Convert.ToInt32(row["EntryID"]);
                    ContainerList.Add(ContainerSearch);
                }
            }
            return ContainerList;
        }
        public EN.Response UpdateImportJoDet(EN.DomesticHubLrEntities data)
        {
            EN.Response response = new EN.Response();
            try
            {
                DataTable list = new DataTable();
                // var Date = Convert.ToDateTime(data.ETA).ToString("yyyy-MM-dd");
                list = trackerdatamanager.SaveDetailsDL(data.LrDate, data.ContainerNo, data.ENTRYID, data.LineID, data.Size, data.ContainerTypeID, data.AddedBy);

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

        public List<EN.Customer> GetCustomer1()

        {
            List<EN.Customer> locationDL = new List<EN.Customer>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetCustomerdetails();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.Customer MovList = new EN.Customer();
                    MovList.AGID = Convert.ToInt16(row["AGID"]);
                    MovList.AGName = Convert.ToString(row["AGName"]);
                    locationDL.Add(MovList);
                }
            }
            return locationDL;
        }
    }
}
