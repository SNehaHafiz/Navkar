using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
   public class TrainTrip
    {
        public TrainTrip()
        {
            WangonList = new List<WagonList>();
            LocotionTrainList = new List<LocotionTrainList>();
            TrainTrip1 = new List<TrainTrip>();
            TotalCountSizeWise = new List<TotalCountSizeWise>();
        }
        public int portId { get; set; }
        public int entryid { get; set; }
        public string process { get; set; }
        public string selected { get; set; }
        public string TrailerNo { get; set; }
        public int TEUS { get; set; }
        public Int64 SrNo { get; set; }
        public Int64 TripID { get; set; }
        public Int64 TripNo { get; set; }
        public Int64 TrainID { get; set; }
        public string TrainNo { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public int AddedBy { get; set; }
        public string AddedOn { get; set; }
        public int Count { get; set; }
        public Int64 WagonID { get; set; }
        public string WagonNo { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerNo1 { get; set; }
        public string ContainerNo2 { get; set; }
        public int validationMessage { get; set; }
        public string Size1 { get; set; }
        public string Size2 { get; set; }
        public string OperatorName { get; set; }
        public string OperatorGST { get; set; }
        public string ContainerType  { get; set; }
        public short Size { get; set; }
        public string Status { get; set; }
        public string Srno1 { get; set; }
        public string LineName { get; set; }
        public int LineId { get; set; }
        public string IcdIndate { get; set; }
        public string DocumentReadyDate { get; set; }
        public string PrePlanDate { get; set; }
        public string Remarks { get; set; }
        public string DwellDays { get; set; }
        public string CHAName { get; set; }
        public string DocumentReceivedDate { get; set; }
        public string Vessel { get; set; }
        public string CutofDate { get; set; }
        public string CutofTime { get; set; }
        public string Gate { get; set; }
        public string from11 { get; set; }
        public string SealNo { get; set; }
        public string POD { get; set; }
        public string Grossweight { get; set; }
        public string SAID { get; set; }
        public string Teus1 { get; set; }
        public string SbNo { get; set; }
        public string modeoftransport { get; set; }
        public string Exportername { get; set; }
        public string commodity { get; set; }
        public string PortTrainNo { get; set; }
        public string NetWt1 { get; set; }
        public string TareWt1 { get; set; }
        public string NetWt2 { get; set; }
        public string TareWt2 { get; set; }
        public string PortWt1 { get; set; }
        public string ActualWt1 { get; set; }
        public string PortWt2 { get; set; }
        public string ActualWt2 { get; set; }
        public string ContainerCount { get; set; }
        public string TotalTues { get; set; }
        public int SUMSIZE { get; set; }
        public string LoadedWagon { get; set; }
        public string EmptyWagon { get; set; }
        public string LE { get; set; }
        public string BOND { get; set; }
        public string CompGSTNo { get; set; }
        public string Status1 { get; set; }
        public string Status2 { get; set; }
        public string commodity1 { get; set; }
        public string POL1 { get; set; }
        public string commodity2 { get; set; }
        public string POL2 { get; set; }
        public string FromPort { get; set; }
        public string Toport { get; set; }
        public string removaldate { get; set; }


        public List<WagonList> WangonList { get; set; }
        public List<LocotionTrainList> LocotionTrainList { get; set; }
        public List<TrainTrip> TrainTrip1 { get; set; }

        public List<TotalCountSizeWise> TotalCountSizeWise { get; set; }


        //public string CHAName { get; set; }
        //public string DocumentReceivedDate { get; set; }
        //public string Vessel { get; set; }
        //public string CutofDate { get; set; }
        //public string CutofTime { get; set; }
        //public string Gate { get; set; }
        //public string from11 { get; set; }
        //public string SealNo { get; set; }
        //public string POD { get; set; }
        //public string Grossweight { get; set; }
        //public string SAID { get; set; }
        //public string Teus1 { get; set; }
        //public string SbNo { get; set; }
        //public string modeoftransport { get; set; }
        //public string Exportername { get; set; }
        //public string commodity { get; set; }
    }
    public class LocotionTrainList
    {
        public int LocationID { get; set; }
        public string Location { get; set; }
    }
    public class TotalCountSizeWise
    {
        public string Size20 { get; set; }
        public string Size40 { get; set; }
        public string Size45 { get; set; }
        public string SizeTeus { get; set; }
    }
}
