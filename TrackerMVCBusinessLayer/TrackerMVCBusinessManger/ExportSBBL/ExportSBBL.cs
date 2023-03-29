using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using BL = TrackerMVCBusinessLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ExportSBBL
{
    public class ExportSBBL
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();

        public List<EN.ExportShippingBillEnt> getCustomerList()
        {
            List<EN.ExportShippingBillEnt> CustomerDDL = new List<EN.ExportShippingBillEnt>();
            DataTable CustmorObj = new DataTable();
            string Table = "customer";
            string Column = "agid,agname";
            string Condition = "";
            string OrderBy = "";

            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.ExportShippingBillEnt CustomerGroupList = new EN.ExportShippingBillEnt();
                    CustomerGroupList.AGID = Convert.ToInt32(row["agid"]);
                    CustomerGroupList.AGName = Convert.ToString(row["agname"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }
        public List<EN.ExportShippingBillEnt> ExporterDDL()
        {
            List<EN.ExportShippingBillEnt> shipperDL = new List<EN.ExportShippingBillEnt>();
            DataTable Shippers = new DataTable();
            string Table = "exp_shippers";
            string Column = "shipperID,shippername";
            string Condition = "";
            string OrderBy = "";

            Shippers = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (Shippers != null)
            {
                foreach (DataRow row in Shippers.Rows)
                {
                    EN.ExportShippingBillEnt ShipperList = new EN.ExportShippingBillEnt();
                    ShipperList.shipperid = Convert.ToInt32(row["shipperID"]);
                    ShipperList.Shippername = Convert.ToString(row["shippername"]);
                    shipperDL.Add(ShipperList);
                }
            }
            return shipperDL;
        }
        public List<EN.ExportShippingBillEnt> CHADLL()
        {
            List<EN.ExportShippingBillEnt> CHADLL = new List<EN.ExportShippingBillEnt>();
            DataTable CHA = new DataTable();
            string Table = "CHA";
            string Column = "CHAID,CHAName";
            string Condition = "";
            string OrderBy = "";

            CHA = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CHA != null)
            {
                foreach (DataRow row in CHA.Rows)
                {
                    EN.ExportShippingBillEnt CHAList = new EN.ExportShippingBillEnt();
                    CHAList.CHAID = Convert.ToInt32(row["CHAID"]);
                    CHAList.CHAName = Convert.ToString(row["CHAName"]);
                    CHADLL.Add(CHAList);
                }
            }
            return CHADLL;
        }

        public List<EN.ExportShippingBillEnt> CargoType()
        {
            List<EN.ExportShippingBillEnt> CargoList = new List<EN.ExportShippingBillEnt>();
            DataTable Cargotype = new DataTable();
            string Table = "cargotype";
            string Column = "cargotypeid,cargotype";
            string Condition = "";
            string OrderBy = "";

            Cargotype = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (Cargotype != null)
            {
                foreach (DataRow row in Cargotype.Rows)
                {
                    EN.ExportShippingBillEnt CargoLists = new EN.ExportShippingBillEnt();
                    CargoLists.CID = Convert.ToInt32(row["cargotypeid"]);
                    CargoLists.CType = Convert.ToString(row["cargotype"]);
                    CargoList.Add(CargoLists);
                }
            }
            return CargoList;
        }

        public List<EN.ExportShippingBillEnt> Pkgs()
        {
            List<EN.ExportShippingBillEnt> pkglist = new List<EN.ExportShippingBillEnt>();
            DataTable pkgstable = new DataTable();
            string Table = "PackageM";
            string Column = "CodeID,Package";
            string Condition = "";
            string OrderBy = "";

            pkgstable = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (pkgstable != null)
            {
                foreach (DataRow row in pkgstable.Rows)
                {
                    EN.ExportShippingBillEnt pkglists = new EN.ExportShippingBillEnt();
                    pkglists.pkgid = Convert.ToInt32(row["CodeID"]);
                    pkglists.pkgtype = Convert.ToString(row["Package"]);
                    pkglist.Add(pkglists);
                }
            }
            return pkglist;
        }
        public List<EN.ExportShippingBillEnt> PodMaster()
        {
            List<EN.ExportShippingBillEnt> PodListDDL = new List<EN.ExportShippingBillEnt>();
            DataTable podtb = new DataTable();
            string Table = "PODMaster";
            string Column = "PODID,PODName";
            string Condition = "";
            string OrderBy = "";

            podtb = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (podtb != null)
            {
                foreach (DataRow row in podtb.Rows)
                {
                    EN.ExportShippingBillEnt podlists = new EN.ExportShippingBillEnt();
                    podlists.PodID = Convert.ToInt32(row["PODID"]);
                    podlists.PODName = Convert.ToString(row["PODName"]);
                    PodListDDL.Add(podlists);
                }
            }
            return PodListDDL;
        }
        public List<EN.ExportShippingBillEnt> CommodityM()
        {
            List<EN.ExportShippingBillEnt> CommodityList = new List<EN.ExportShippingBillEnt>();
            DataTable comdt = new DataTable();
            string Table = "Commodity_Group_M";
            string Column = "commodity_group_id,commodity_group_name";
            string Condition = "";
            string OrderBy = "commodity_group_name";

            comdt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (comdt != null)
            {
                foreach (DataRow row in comdt.Rows)
                {
                    EN.ExportShippingBillEnt CommoList = new EN.ExportShippingBillEnt();
                    CommoList.ComID = Convert.ToString(row["commodity_group_name"]);
                    CommoList.CommodityName = Convert.ToString(row["commodity_group_name"]);
                    CommodityList.Add(CommoList);
                }
            }
            return CommodityList;
        }

        public List<EN.ExportShippingBillEnt> Stuffing()
        {
            List<EN.ExportShippingBillEnt> StuffList = new List<EN.ExportShippingBillEnt>();
            DataTable stuffdt = new DataTable();
            string Table = "stuffingtype";
            string Column = "TypeID,StuffingType";
            string Condition = "";
            string OrderBy = "";

            stuffdt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (stuffdt != null)
            {
                foreach (DataRow row in stuffdt.Rows)
                {
                    EN.ExportShippingBillEnt CommoList = new EN.ExportShippingBillEnt();
                    CommoList.StuffID = Convert.ToInt32(row["TypeID"]);
                    CommoList.Stuffing = Convert.ToString(row["StuffingType"]);
                    StuffList.Add(CommoList);
                }
            }
            return StuffList;
        }
        //Codes By Prashant

        //public string CheckSBNo(string Sbno)
        //{
        //    string Message = "";
        //    DataTable CheckHorseDlL = new DataTable();
        //    CheckHorseDlL = TrackerManager.CheckSBNo(Sbno);

        //    if (CheckHorseDlL.Rows.Count > 0)
        //    {
        //        int HorseCount = Convert.ToInt16(CheckHorseDlL.Rows[0]["SBNo"]);
        //        if (HorseCount > 0)
        //        {
        //            Message = "1";

        //        }
        //    }


        //    return Message;
        //}

        //public string CheckStuffSBno(string SBNumber)
        //{
        //    string Message = "";
        //    DataTable CheckHorseDlL = new DataTable();
        //    CheckHorseDlL = TrackerManager.CheckStuffSBno(SBNumber);

        //    if (CheckHorseDlL.Rows.Count > 0)
        //    {
        //        int HorseCount = Convert.ToInt16(CheckHorseDlL.Rows[0]["SBNumber"]);
        //        if (HorseCount > 0)
        //        {
        //            Message = "1";

        //        }
        //    }


        //    return Message;
        //}

        public List<EN.VesselCutOff> Vesseles()
        {
            List<EN.VesselCutOff> VesselDDL = new List<EN.VesselCutOff>();
            DataTable podtb = new DataTable();
            string Table = "Vessels";
            string Column = "VesselID,VesselName";
            string Condition = "";
            string OrderBy = "";

            podtb = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (podtb != null)
            {
                foreach (DataRow row in podtb.Rows)
                {
                    EN.VesselCutOff podlists = new EN.VesselCutOff();
                    podlists.VesselID = Convert.ToInt32(row["VesselID"]);
                    podlists.VesselName = Convert.ToString(row["VesselName"]);
                    VesselDDL.Add(podlists);
                }
            }
            return VesselDDL;
        }

        public List<EN.VesselCutOff> PortMaster()
        {
            List<EN.VesselCutOff> PodListDDL = new List<EN.VesselCutOff>();
            DataTable podtb = new DataTable();
            string Table = "ports";
            string Column = "PortID,PortName";
            string Condition = "";
            string OrderBy = "";

            podtb = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (podtb != null)
            {
                foreach (DataRow row in podtb.Rows)
                {
                    EN.VesselCutOff podlists = new EN.VesselCutOff();
                    podlists.PortID = Convert.ToInt32(row["PortID"]);
                    podlists.PortName = Convert.ToString(row["PortName"]);
                    PodListDDL.Add(podlists);
                }
            }
            return PodListDDL;
        }

        public List<EN.VesselCutOff> PodMasters()
        {
            List<EN.VesselCutOff> VesselDDL = new List<EN.VesselCutOff>();
            DataTable podtb = new DataTable();
            string Table = "PODMaster";
            string Column = "PODID,PODName";
            string Condition = "";
            string OrderBy = "";

            podtb = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (podtb != null)
            {
                foreach (DataRow row in podtb.Rows)
                {
                    EN.VesselCutOff podlists = new EN.VesselCutOff();
                    podlists.PodID = Convert.ToInt32(row["PODID"]);
                    podlists.PODName = Convert.ToString(row["PODName"]);
                    VesselDDL.Add(podlists);
                }
            }
            return VesselDDL;
        }
        // Buffer
        public List<EN.ExportShippingBillEnt> ContainerType()
        {
            List<EN.ExportShippingBillEnt> ContainerType = new List<EN.ExportShippingBillEnt>();
            DataTable stuffdt = new DataTable();
            string Table = "containertype";
            string Column = "ContainerTypeID,ContainerType";
            string Condition = "";
            string OrderBy = "";

            stuffdt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (stuffdt != null)
            {
                foreach (DataRow row in stuffdt.Rows)
                {
                    EN.ExportShippingBillEnt CommoList = new EN.ExportShippingBillEnt();
                    CommoList.ContainerTypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    CommoList.ContainerType = Convert.ToString(row["ContainerType"]);
                    ContainerType.Add(CommoList);
                }
            }
            return ContainerType;
        }

        public List<EN.ExportShippingBillEnt> PName()
        {
            List<EN.ExportShippingBillEnt> PortName = new List<EN.ExportShippingBillEnt>();
            DataTable stuffdt = new DataTable();
            string Table = "ports";
            string Column = "PortID,PortName";
            string Condition = "";
            string OrderBy = "";

            stuffdt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (stuffdt != null)
            {
                foreach (DataRow row in stuffdt.Rows)
                {
                    EN.ExportShippingBillEnt CommoList = new EN.ExportShippingBillEnt();
                    CommoList.PID = Convert.ToInt32(row["PortID"]);
                    CommoList.PName = Convert.ToString(row["PortName"]);
                    PortName.Add(CommoList);
                }
            }
            return PortName;
        }

        public List<EN.ExportShippingBillEnt> PODCountry()
        {
            List<EN.ExportShippingBillEnt> PODCountry = new List<EN.ExportShippingBillEnt>();
            DataTable podtb = new DataTable();
            string Table = "PODMaster";
            string Column = "PODID,Country";
            string Condition = "";
            string OrderBy = "";

            podtb = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (podtb != null)
            {
                foreach (DataRow row in podtb.Rows)
                {
                    EN.VesselCutOff podlists = new EN.VesselCutOff();
                    podlists.PCountryID = Convert.ToInt32(row["PODID"]);
                    podlists.CountryName = Convert.ToString(row["Country"]);
                    PODCountry.Add(podlists);
                }
            }
            return PODCountry;
        }

        public List<EN.ActivityListGet> ActivityGet()
        {
            List<EN.ActivityListGet> PODCountry = new List<EN.ActivityListGet>();
            DataTable podtb = new DataTable();
            string Table = "Settings_Modifiy_Vehicle";
            string Column = "ActivityName,ActivityName";
            string Condition = "";
            string OrderBy = "";

            podtb = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (podtb != null)
            {
                foreach (DataRow row in podtb.Rows)
                {
                    EN.ActivityListGet podlists = new EN.ActivityListGet();
                    podlists.Activity = Convert.ToString(row["ActivityName"]);
                    podlists.Activity = Convert.ToString(row["ActivityName"]);
                    PODCountry.Add(podlists);
                }
            }
            return PODCountry;
        }
        public List<EN.ExportShippingBillEnt> SLID()
        {
            List<EN.ExportShippingBillEnt> PortName = new List<EN.ExportShippingBillEnt>();
            DataTable stuffdt = new DataTable();
            string Table = "ShipLines";
            string Column = "SLID,SLName";
            string Condition = "";
            string OrderBy = "";

            stuffdt = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (stuffdt != null)
            {
                foreach (DataRow row in stuffdt.Rows)
                {
                    EN.ExportShippingBillEnt CommoList = new EN.ExportShippingBillEnt();
                    CommoList.SLID = Convert.ToInt32(row["SLID"]);
                    CommoList.SLName = Convert.ToString(row["SLName"]);
                    PortName.Add(CommoList);
                }
            }
            return PortName;
        }

        public List<EN.Items> GetItemList()
        {
            List<EN.Items> CustomerDDL = new List<EN.Items>();
            DataTable CustmorObj = new DataTable();
            string Table = "trackerPurchase.dbo.item";
            string Column = "itemid,name";
            string Condition = "";
            string OrderBy = "";

            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.Items CustomerGroupList = new EN.Items();
                    CustomerGroupList.itemid = Convert.ToInt32(row["itemid"]);
                    CustomerGroupList.name = Convert.ToString(row["name"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }

        public List<EN.Items> DepartmentList()
        {
            List<EN.Items> CustomerDDL = new List<EN.Items>();
            DataTable CustmorObj = new DataTable();
            string Table = "WO_Department";
            string Column = "ID,Department";
            string Condition = "";
            string OrderBy = "";

            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.Items CustomerGroupList = new EN.Items();
                    CustomerGroupList.ID = Convert.ToInt32(row["ID"]);
                    CustomerGroupList.Department = Convert.ToString(row["Department"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }


        public List<EN.Items> TaxRemarks()
        {
            List<EN.Items> CustomerDDL = new List<EN.Items>();
            DataTable CustmorObj = new DataTable();
            string Table = "Tax_Service_Category";
            string Column = "ID,Tax_Type_Desc";
            string Condition = "";
            string OrderBy = "";

            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.Items CustomerGroupList = new EN.Items();
                    CustomerGroupList.ID = Convert.ToInt32(row["ID"]);
                    CustomerGroupList.Tax_Type_Desc = Convert.ToString(row["Tax_Type_Desc"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }


        public List<EN.WaraiDetails> getCustomerCommonList()
        {
            List<EN.WaraiDetails> CustomerDDL = new List<EN.WaraiDetails>();
            DataTable CustmorObj = new DataTable();
            string Table = "trackerPurchase.dbo.Party_gst_m";
            string Column = "isnull(Common_ID,0) Common_ID,gstname";
            string Condition = "";
            string OrderBy = "gstname";

            CustmorObj = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CustmorObj != null)
            {
                foreach (DataRow row in CustmorObj.Rows)
                {
                    EN.WaraiDetails CustomerGroupList = new EN.WaraiDetails();
                    CustomerGroupList.PartyID = Convert.ToInt32(row["Common_ID"]);
                    CustomerGroupList.PartyName = Convert.ToString(row["gstname"]);
                    CustomerDDL.Add(CustomerGroupList);
                }
            }
            return CustomerDDL;
        }
    }
}
