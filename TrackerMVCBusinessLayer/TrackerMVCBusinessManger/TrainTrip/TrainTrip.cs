using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrainTrip
{
   public class TrainTrip
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public List<EN.TrainTrip> GetTripList(int Userid)
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetTripList();


            List<EN.TrainTrip> TripList = new List<EN.TrainTrip>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.TrainTrip trip = new EN.TrainTrip();
                    trip.TripID = Convert.ToInt64(row["TripID"]);
                    trip.TripNo = Convert.ToInt32(row["TripNo"]);
                    TripList.Add(trip);
                }

            }
            return TripList;
        }

        public void DeleteDataFromTempTable(int userid)
        {
            TrackerManager.DeleteWagonTempData(userid);
        }

        public List<EN.TrainTrip> GetTrainNo(Int64 TripNo, int Userid)
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetTrainNo(TripNo);
            DataTable codeDL1 = new DataTable();
            codeDL1 = TrackerManager.DeleteWagonTempData(TripNo, Userid);

            List<EN.TrainTrip> Trainlist = new List<EN.TrainTrip>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.TrainTrip train = new EN.TrainTrip();
                    train.TrainNo = Convert.ToString(row["TrainNo"]);
                    train.Origin = Convert.ToString(row["Origin"]);
                    train.Destination = Convert.ToString(row["Destination"]);
                    Trainlist.Add(train);
                }

            }
            return Trainlist;
        }
        public List<EN.TrainTrip> GetWagonList(string TrainNo)
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetWagonList(TrainNo);


            List<EN.TrainTrip> WagonList = new List<EN.TrainTrip>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.TrainTrip Wagon = new EN.TrainTrip();
                    Wagon.WagonID = Convert.ToInt64(row["WagonID"]);
                    Wagon.WagonNo = Convert.ToString(row["WagonNo"]);
                    WagonList.Add(Wagon);
                }

            }
            return WagonList;
        }
        public List<EN.TrainTrip> GetContainerList()
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetContainerList();


            List<EN.TrainTrip> ContainerList = new List<EN.TrainTrip>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.TrainTrip Container = new EN.TrainTrip();
                    Container.SrNo = Convert.ToInt64(row["SrNo"]);
                    Container.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    ContainerList.Add(Container);
                }

            }
            return ContainerList;
        }
        public List<EN.TrainTrip> GetTripDets()

        {
            DataTable TripDT = new DataTable();
            TripDT = TrackerManager.getTripDetails();
            List<EN.TrainTrip> TripList = new List<EN.TrainTrip>();

            if (TripDT.Rows.Count != 0)
            {
                foreach (DataRow row in TripDT.Rows)
                {
                    EN.TrainTrip trip = new EN.TrainTrip();
                    trip.TrainNo = Convert.ToString(row["TrainNo"]);
                    trip.TripNo = Convert.ToInt32(row["TripNo"]);
                    trip.Origin = Convert.ToString(row["PortFrom"]);
                    trip.Destination = Convert.ToString(row["PortTo"]);
                    trip.PortTrainNo = Convert.ToString(row["PortTrainNo"]);

                    TripList.Add(trip);
                }

            }
            return TripList;
        }

        public EN.TrainTrip AddDomsticWagonDataOnly(string TrainNo, int Userid, Int64 TripNo)
        {
            List<EN.TrainTrip> ContainerList = new List<EN.TrainTrip>();
            List<EN.TotalCountSizeWise> CountList = new List<EN.TotalCountSizeWise>();

            EN.TrainTrip ValidationList = new EN.TrainTrip();
            EN.TotalCountSizeWise SizeCountList = new EN.TotalCountSizeWise();

            DataSet ContainerDS = new DataSet();

            DataTable ValidationDT = new DataTable();
            DataTable ContainerDT = new DataTable();
            ContainerDT = TrackerManager.AddDomesticWagonDataOnly(TrainNo, Userid, TripNo);



            int TTeus = 0, Size20 = 0, Size40 = 0, Size45 = 0;
            if (ContainerDT != null)
            {
                foreach (DataRow row in ContainerDT.Rows)
                {
                    EN.TrainTrip JODTList = new EN.TrainTrip();

                    JODTList.WagonNo = Convert.ToString(row["WagonNo"]);
                    JODTList.ContainerNo1 = Convert.ToString(row["ContainerNo1"]);
                    JODTList.ContainerNo2 = Convert.ToString(row["ContainerNo2"]);
                    JODTList.Size1 = Convert.ToString(row["Size1"]);
                    JODTList.Size2 = Convert.ToString(row["Size2"]);
                    JODTList.NetWt1 = Convert.ToString(row["NetWt1"]);
                    JODTList.TareWt1 = Convert.ToString(row["TareWt1"]);
                    JODTList.NetWt2 = Convert.ToString(row["NetWt2"]);
                    JODTList.TareWt2 = Convert.ToString(row["TareWt2"]);
                    //               JODTList.PortWt1 = Convert.ToString(row["PortWt1"]);
                    JODTList.ActualWt1 = Convert.ToString(row["ActualWt1"]);
                    //               JODTList.PortWt2 = Convert.ToString(row["PortWt2"]);
                    JODTList.ActualWt2 = Convert.ToString(row["ActualWt2"]);
                    JODTList.Status1 = Convert.ToString(row["Status1"]);
                    JODTList.Status2 = Convert.ToString(row["Status2"]);


                    JODTList.commodity1 = Convert.ToString(row["commodity1"]);
                    JODTList.POL1 = Convert.ToString(row["POL1"]);
                    JODTList.commodity2 = Convert.ToString(row["commodity2"]);
                    JODTList.POL2 = Convert.ToString(row["POL2"]);
                    if (JODTList.Size1 == "20")
                    {
                        Size20 += 1;
                    }
                    else if (JODTList.Size1 == "40")
                    {
                        Size40 += 1;
                    }
                    else if (JODTList.Size1 == "45")
                    {
                        Size45 += 1;
                    }
                    else
                    {
                        Size20 += 0;
                        Size40 += 0;
                        Size45 += 0;
                    }
                    if (JODTList.Size2 == "20")
                    {
                        Size20 += 1;
                    }
                    else if (JODTList.Size2 == "40")
                    {
                        Size40 += 1;
                    }
                    else if (JODTList.Size2 == "45")
                    {
                        Size45 += 1;
                    }
                    else
                    {
                        Size20 += 0;
                        Size40 += 0;
                        Size45 += 0;
                    }
                    ContainerList.Add(JODTList);
                }
                TTeus = Size20 + Size40 * 2 + Size45 * 2;
                SizeCountList.Size20 = Convert.ToString(Size20);
                SizeCountList.Size40 = Convert.ToString(Size40);
                SizeCountList.Size45 = Convert.ToString(Size45);
                SizeCountList.SizeTeus = Convert.ToString(TTeus);

                CountList.Add(SizeCountList);
                ValidationList.TrainTrip1 = ContainerList;
                ValidationList.TotalCountSizeWise = CountList;

                //CountList.Add(ValidationList);
                // return JOrderList;
            }


            return ValidationList;
        }

        public EN.TrainTrip GetExportSizeList(string ContainerNo, int Number, string WagonNo, int TripNo, int UserID)
        {
            DataSet SizeDS = new DataSet();
            SizeDS = TrackerManager.GetExportSizeDS(ContainerNo, Number, WagonNo, TripNo, UserID);
            EN.TrainTrip TrainTrip = new EN.TrainTrip();
            List<EN.TrainTrip> TripList = new List<EN.TrainTrip>();
            List<EN.TotalCountSizeWise> SizeList = new List<EN.TotalCountSizeWise>();

            int TTeus = 0, Size20 = 0, Size40 = 0, Size45 = 0;

            if (SizeDS.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in SizeDS.Tables[0].Rows)
                {
                    EN.TrainTrip trip = new EN.TrainTrip();
                    trip.ContainerCount = Convert.ToString(row["CCount"]);
                    trip.SUMSIZE = Convert.ToInt16(row["SumSize"]);
                    trip.TotalTues = Convert.ToString(row["TotalTues"]);

                    TripList.Add(trip);
                }
            }
            if (SizeDS.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow row in SizeDS.Tables[1].Rows)
                {
                    EN.TrainTrip trip = new EN.TrainTrip();
                    trip.Size = Convert.ToInt16(row["Size"]);
                    trip.NetWt1 = Convert.ToString(row["NetWt"]);
                    trip.TareWt1 = Convert.ToString(row["TareWt"]);
                    trip.Status1 = Convert.ToString(row["Status1"]);
                    trip.commodity1 = Convert.ToString(row["commodity"]);
                    trip.POL1 = Convert.ToString(row["POL"]);
                    trip.ActualWt1 = Convert.ToString(row["ActualWt"]);

                    TripList.Add(trip);
                }
            }
            if (SizeDS.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow row in SizeDS.Tables[2].Rows)
                {
                    EN.TotalCountSizeWise trip = new EN.TotalCountSizeWise();
                    Size20 = Convert.ToInt16(row["Size20"]);
                    Size40 = Convert.ToInt16(row["Size40"]);
                    Size45 = Convert.ToInt16(row["Size45"]);
                    TTeus = Size20 + Size40 * 2 + Size45 * 2;
                    trip.Size20 = Convert.ToString(row["Size20"]);
                    trip.Size40 = Convert.ToString(row["Size40"]);
                    trip.Size45 = Convert.ToString(row["Size45"]);
                    trip.SizeTeus = Convert.ToString(TTeus);

                    SizeList.Add(trip);
                }
            }
            TrainTrip.TrainTrip1 = TripList;
            TrainTrip.TotalCountSizeWise = SizeList;

            return TrainTrip;

        }

        public string DeletetheContainernofromwagon(string ContainerNo, int Number, string WagonNo, int TripNo, int userid)
        {
            string messgae = "";
            DataTable TrailerDL = new DataTable();
            TrailerDL = TrackerManager.DeletetheContainernofromwagon(ContainerNo, Number, WagonNo, TripNo, userid);



            return messgae;

        }

        public List<EN.TotalCountSizeWise> UpdateExportData(string ContainerNo, int Number, string WagonNo, int TripNo, int UserID, int Size, string NetWt1, string TareWt1, string ActualWt1, string Status1, string commodity1, string POL1)
        {
            DataTable TripDT = new DataTable();
            TripDT = TrackerManager.UpdateExportTempData(ContainerNo, Number, WagonNo, TripNo, UserID, Size, NetWt1, TareWt1, ActualWt1, Status1, commodity1, POL1);
            List<EN.TotalCountSizeWise> SizeList = new List<EN.TotalCountSizeWise>();

            int TTeus = 0, Size20 = 0, Size40 = 0, Size45 = 0;
            if (TripDT.Rows.Count != 0)
            {
                foreach (DataRow row in TripDT.Rows)
                {
                    EN.TotalCountSizeWise trip = new EN.TotalCountSizeWise();
                    Size20 = Convert.ToInt16(row["Size20"]);
                    Size40 = Convert.ToInt16(row["Size40"]);
                    Size45 = Convert.ToInt16(row["Size45"]);
                    TTeus = Size20 + Size40 * 2 + Size45 * 2;
                    trip.Size20 = Convert.ToString(row["Size20"]);
                    trip.Size40 = Convert.ToString(row["Size40"]);
                    trip.Size45 = Convert.ToString(row["Size45"]);
                    trip.SizeTeus = Convert.ToString(TTeus);

                    SizeList.Add(trip);
                }
            }
            return SizeList;

        }
        public EN.WagonContainerList AddWagonData(string WagonNo, string ContainerNo1, string ContainerNo2, int Userid, Int64 TripNo)
        {
            List<EN.TrainTrip> ContainerList = new List<EN.TrainTrip>();
            EN.TrainTrip ValidationList = new EN.TrainTrip();

            DataSet ContainerDS = new DataSet();

            DataTable ValidationDT = new DataTable();
            DataTable ContainerDT = new DataTable();
            ContainerDS = TrackerManager.AddWagonData(WagonNo, ContainerNo1, ContainerNo2, Userid, TripNo);

            ValidationDT = ContainerDS.Tables[0];
            ContainerDT = ContainerDS.Tables[0];

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
                    EN.TrainTrip JODTList = new EN.TrainTrip();

                    JODTList.WagonNo = Convert.ToString(row["WagonNo"]);
                    JODTList.ContainerNo1 = Convert.ToString(row["ContainerNo1"]);
                    JODTList.ContainerNo2 = Convert.ToString(row["ContainerNo2"]);

                    //JODTList.ContainerNo = Convert.ToString(row["ContainerNo"]) + "<input Name=ContainerNo type=hidden id=ContainerNo   value=" + Convert.ToString(row["ContainerNo"]) + ">"; ;
                    //JODTList.FLName = Convert.ToString(row["FL"]) + "<input Name=FLid type=hidden id=FL   value=" + Convert.ToString(row["FLId"]) + ">";
                    //JODTList.ISOCodeName = Convert.ToString(row["ISOCodeName"]) + "<input Name=ISOCodeID type=hidden id=ISOCode   value=" + Convert.ToString(row["ISOCODE"]) + ">";

                    //JODTList.Commodity_Group_Name = Convert.ToString(row["CommodityGroup"]) + "<input Name=Commodity_group_id type=hidden id=Commodity_group_id   value=" + Convert.ToInt32(row["Commodity_group_id"]) + ">";
                    //JODTList.Cargotype = Convert.ToString(row["Cargotype"]) + "<input Name=Cargotypeid type=hidden id=Cargotypeid   value=" + Convert.ToString(row["CARGOTYPEID"]) + ">";
                    //JODTList.ContainerSize = Convert.ToString(row["Size"]) + "<input Name=Size type=hidden id=Size   value=" + Convert.ToString(row["SizeID"]) + ">";
                    //JODTList.TempID = Convert.ToInt64(row["ID"]);
                    //JODTList.Size = Convert.ToInt16(row["Size"]);

                    ContainerList.Add(JODTList);
                }

                // return JOrderList;
            }

            EN.WagonContainerList WagonList = new EN.WagonContainerList();
            WagonList.JOValidation = ValidationList;
            WagonList.Containerlist = ContainerList;

            return WagonList;
        }

        public List<EN.TrainTrip> GetSize1(string Container1)
        {
            try
            {
                DataTable codeDL = new DataTable();
                codeDL = TrackerManager.GetSize1(Container1);
                List<EN.TrainTrip> EmailList = new List<EN.TrainTrip>();

                if (codeDL.Rows.Count != 0)
                {
                    foreach (DataRow row in codeDL.Rows)
                    {
                        EN.TrainTrip Size1 = new EN.TrainTrip();

                        Size1.Size1 = Convert.ToString(row["Size"]);
                        EmailList.Add(Size1);
                    }

                }
                else
                {
                    EN.TrainTrip Size1 = new EN.TrainTrip();

                    Size1.Size1 = "";
                    EmailList.Add(Size1);
                }
                return EmailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DomesticTrainData(EN.TrainTrip MasterData, int userId)
        {
            string Message = "";
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt = TrackerManager.DomesticTrainData(MasterData.TripNo, MasterData.TrainNo, MasterData.Origin, MasterData.Destination, userId);

            Message = Convert.ToString(dt.Rows[0][0]);
            return Message;
        }
        public List<EN.TrainTrip> GetDomesticTrainTripsForModify()
        {
            DataTable codeDL = new DataTable();
            codeDL = TrackerManager.GetdOMESTICTripListForModify();


            List<EN.TrainTrip> TripList = new List<EN.TrainTrip>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.TrainTrip trip = new EN.TrainTrip();
                    trip.SrNo = Convert.ToInt64(row["SrNo"]);
                    trip.TripNo = Convert.ToInt32(row["TripNo"]);
                    trip.TrainNo = Convert.ToString(row["TrainNo"]);
                    trip.Origin = Convert.ToString(row["Origin"]);
                    trip.Destination = Convert.ToString(row["Destination"]);
                    trip.AddedOn = Convert.ToString(row["AddedOn"]);
                    trip.PortTrainNo = Convert.ToString(row["PortTrainNo"]);

                    TripList.Add(trip);
                }

            }
            return TripList;
        }
        public string SubmitEDomesticTrainTripList(EN.TrainTrip MasterData, int userId)
        {
            string Message = "";
            DataTable dt = new DataTable();
            dt = TrackerManager.SubmitDomesticTrainTripData(MasterData.TripNo, MasterData.TrainNo, userId);
            Message = Convert.ToString(dt.Rows[0][0]);
            return Message;
        }
    }
}
