using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using System.Data;


namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.VesselMaster
{
    public class ManualPortOut
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
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

        //public List<EN.ContainerSize> getContainerSize()
        //{
        //    List<EN.ContainerSize> PortsDL = new List<EN.ContainerSize>();
        //    DataTable PortsDT = new DataTable();

        //    PortsDT = TrackerManager.getContainerSize();
        //    if (PortsDT != null)
        //    {
        //        foreach (DataRow row in PortsDT.Rows)
        //        {
        //            EN.ContainerSize PortsList = new EN.ContainerSize();
        //            PortsList.ContainerSizeID = Convert.ToInt32(row["ContainerSizeID"]);
        //            PortsList.ContainerSizeName = Convert.ToString(row["ContainerSize"]);

        //            PortsDL.Add(PortsList);
        //        }
        //    }
        //    return PortsDL;
        //}

        public List<EN.ContainerSize> getContainerSize()
        {
            List<EN.ContainerSize> PortsDL = new List<EN.ContainerSize>();
            DataTable PortsDT = new DataTable();
            string Table = "ContainerSize";
            string Column = "ContainerSizeID,ContainerSize";
            string Condition = "";
            string OrderBy = "ContainerSize";

            PortsDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ContainerSize PortsList = new EN.ContainerSize();
                    PortsList.ContainerSizeID = Convert.ToInt32(row["ContainerSizeID"]);
                    PortsList.ContainerSizeName = Convert.ToString(row["ContainerSize"]);

                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }


        public List<EN.ContainerType> getContainerType()
        {
            List<EN.ContainerType> ContainerTypeDL = new List<EN.ContainerType>();
            DataTable ContainerTypeDT = new DataTable();
            string Table = "ContainerType";
            string Column = "ContainerTypeID,ContainerType";
            string Condition = "";
            string OrderBy = "ContainerType";
            ContainerTypeDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ContainerTypeDT != null)
            {
                foreach (DataRow row in ContainerTypeDT.Rows)
                {
                    EN.ContainerType ContainerTypeList = new EN.ContainerType();
                    ContainerTypeList.ContainerTypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    ContainerTypeList.ContainerTypeName = Convert.ToString(row["ContainerType"]);

                    ContainerTypeDL.Add(ContainerTypeList);
                }
            }
            return ContainerTypeDL;
        }









        //public List<EN.ContainerType> getContainerType()
        //{
        //    List<EN.ContainerType> PortsDL = new List<EN.ContainerType>();
        //    DataTable PortsDT = new DataTable();
        //    PortsDT = TrackerManager.getContainerType();
        //    if (PortsDT != null)
        //    {
        //        foreach (DataRow row in PortsDT.Rows)
        //        {
        //            EN.ContainerType PortsList = new EN.ContainerType();
        //            PortsList.ContainerTypeID = Convert.ToInt32(row["ContainerTypeID"]);
        //            PortsList.ContainerTypeName = Convert.ToString(row["ContainerType"]);

        //            PortsDL.Add(PortsList);
        //        }
        //    }
        //    return PortsDL;
        //}

        //public List<EN.TrailersEntities> getTrailers()
        //{
        //    List<EN.TrailersEntities> PortsDL = new List<EN.TrailersEntities>();
        //    DataTable PortsDT = new DataTable();
        //    PortsDT = TrackerManager.gettrailerno();
        //    if (PortsDT != null)
        //    {
        //        foreach (DataRow row in PortsDT.Rows)
        //        {
        //            EN.TrailersEntities PortsList = new EN.TrailersEntities();
        //            PortsList.trailerid = Convert.ToInt16(row["trailerid"]);
        //            PortsList.trailername = Convert.ToString(row["trailername"]);

        //            PortsDL.Add(PortsList);
        //        }
        //    }
        //    return PortsDL;
        //}

        public List<EN.TrailersEntities> getTrailers()
        {
            List<EN.TrailersEntities> TrailersDL = new List<EN.TrailersEntities>();
            DataTable TrailersDT = new DataTable();

            string Table = "trailers";
            string Column = "trailerid,trailername";
            string Condition = "";
            string OrderBy = "trailername";

            TrailersDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TrailersDT != null)
            {
                foreach (DataRow row in TrailersDT.Rows)
                {
                    EN.TrailersEntities TrailersList = new EN.TrailersEntities();
                    TrailersList.trailerid = Convert.ToInt16(row["trailerid"]);
                    TrailersList.trailername = Convert.ToString(row["trailername"]);

                    TrailersDL.Add(TrailersList);
                }
            }
            return TrailersDL;
        }










        public List<EN.Location> getLocation()
        {
            List<EN.Location> LocationDL = new List<EN.Location>();
            DataTable LocationDT = new DataTable();
            string Table = "Ext_Location_M";
            string Column = "LocationID,Location";
            string Condition = "";
            string OrderBy = "Location";
            LocationDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);


            if (LocationDT != null)
            {
                foreach (DataRow row in LocationDT.Rows)
                {
                    EN.Location LocationList = new EN.Location();
                    LocationList.LocationID = Convert.ToInt16(row["LocationID"]);
                    LocationList.LocationName = Convert.ToString(row["Location"]);

                    LocationDL.Add(LocationList);
                }
            }
            return LocationDL;
        }











        //public List<EN.Location> getLocation()
        //{
        //    List<EN.Location> PortsDL = new List<EN.Location>();
        //    DataTable PortsDT = new DataTable();
        //    PortsDT = TrackerManager.getLocation();


        //    if (PortsDT != null)
        //    {
        //        foreach (DataRow row in PortsDT.Rows)
        //        {
        //            EN.Location PortsList = new EN.Location();
        //            PortsList.LocationID = Convert.ToInt16(row["LocationID"]);
        //            PortsList.LocationName = Convert.ToString(row["Location"]);

        //            PortsDL.Add(PortsList);
        //        }
        //    }
        //    return PortsDL;
        //}

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

        public List<EN.Cycle> getCycle()
        {
            List<EN.Cycle> CycleDL = new List<EN.Cycle>();
            DataTable CycleDT = new DataTable();
            string Table = "Port_Activity_M";
            string Column = "Activity_ID,Activity_Name";
            string Condition = "";
            string OrderBy = "";

            CycleDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (CycleDT != null)
            {
                foreach (DataRow row in CycleDT.Rows)
                {
                    EN.Cycle CycleList = new EN.Cycle();
                    CycleList.Activity_ID = Convert.ToInt32(row["Activity_ID"]);
                    CycleList.Activity_Name = Convert.ToString(row["Activity_Name"]);
                    CycleDL.Add(CycleList);
                }
            }
            return CycleDL;
        }

        public string AddManualportOut(string TrailerNo, string ContainerNo, int ContainerSize, int ContainerType, int Portname, int Cycle, int TransportType, int FromSource, int FromDestination, int TrainNo, string WagonNo, string SMTPReceived, int userId)
        {
            string message = "";
            DataTable Manualport = new DataTable();
            Manualport = TrackerManager.AddManualportout(TrailerNo, ContainerNo, ContainerSize, ContainerType, Portname, Cycle, TransportType, FromSource, FromDestination, TrainNo, WagonNo, SMTPReceived, userId);


            return message;
        }

        public List<EN.TrailersEntities> getTrailerNo(string TrailerNo)
        {
            List<EN.TrailersEntities> PortsDL = new List<EN.TrailersEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = TrackerManager.GetTrailerNo(TrailerNo);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TrailersEntities PortsList = new EN.TrailersEntities();
                    PortsList.trailerid = Convert.ToInt16(row["trailerid"]);
                    PortsList.trailername = Convert.ToString(row["trailername"]);
                    PortsList.TransID = Convert.ToInt32(row["TransID"]);
                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }
        
        public List<EN.TrailersEntities> getTrailerNoFor_Fuel_Con(string TrailerNo)
        {
            List<EN.TrailersEntities> PortsDL = new List<EN.TrailersEntities>();
            DataTable PortsDT = new DataTable();
            PortsDT = TrackerManager.GetTrailerNoFor_Fuel_Con(TrailerNo);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.TrailersEntities PortsList = new EN.TrailersEntities();
                    PortsList.trailerid = Convert.ToInt16(row["trailerid"]);
                    PortsList.trailername = Convert.ToString(row["trailername"]);
                    PortsList.TransID = Convert.ToInt32(row["TransID"]);
                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }
        public List<EN.Train> getTrain()
        {
            List<EN.Train> TransportDL = new List<EN.Train>();
            DataTable TransportDT = new DataTable();
            string Table = "TrainM";
            string Column = "TrainId,TrainNo";
            string Condition = "TrainArrivedDate is null";
            string OrderBy = "TrainNo";

            TransportDT = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TransportDT != null)
            {
                foreach (DataRow row in TransportDT.Rows)
                {
                    EN.Train TransportList = new EN.Train();
                    TransportList.TrainId = Convert.ToInt32(row["TrainId"]);
                    TransportList.TrainNo = Convert.ToString(row["TrainNo"]);
                    TransportDL.Add(TransportList);
                }
            }
            return TransportDL;
        }
        public List<EN.ContainerDetails> GetTEUSCalculation(string TrainNo)
        {
            List<EN.ContainerDetails> PortsDL = new List<EN.ContainerDetails>();
            DataTable PortsDT = new DataTable();
            PortsDT = TrackerManager.GetTEUSCalculation(TrainNo);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ContainerDetails PortsList = new EN.ContainerDetails();
                    PortsList.Message = Convert.ToString(row["validations"]);

                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }

        public List<EN.ContainerDetails> CheckDuplicateContainer(string ContainerNo)
        {
            List<EN.ContainerDetails> PortsDL = new List<EN.ContainerDetails>();
            DataTable PortsDT = new DataTable();
            PortsDT = TrackerManager.CheckDuplicateContainer(ContainerNo);
            if (PortsDT != null)
            {
                foreach (DataRow row in PortsDT.Rows)
                {
                    EN.ContainerDetails PortsList = new EN.ContainerDetails();
                    PortsList.Message = Convert.ToString(row["alert"]);

                    PortsDL.Add(PortsList);
                }
            }
            return PortsDL;
        }
    }
}
