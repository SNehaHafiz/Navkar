using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerMVCEntities.BusinessEntities
{
    public class PumpAcknowledgementEntites
    {

        public string DriverName { get; set; }
        public string Fuelqty { get; set; }
        public string SLipdate { get; set; }
        public string Fuelrate { get; set; }
        public string totalamount { get; set; }
    }
 

     

    public class GetTransportBillDetails
    {


        public GetTransportBillDetails()
        {
            TransportBillMsg = new List<TransportBillMsg>();
            DataGetBillChecking = new List<DataGetBillChecking>();


        }
        public List<TransportBillMsg> TransportBillMsg = new List<TransportBillMsg>();
        public List<DataGetBillChecking> DataGetBillChecking = new List<DataGetBillChecking>();
    }





    public class DataGetBillChecking
    {
        public string select { get; set; }
        public string cmbActivity { get; set; }
        public string dtpbilldate { get; set; }
        public string cmbTransporter { get; set; }
        public string dtpFromDate { get; set; }
        public string dtpToDate { get; set; }
        public string Process { get; set; }
        public string EntryIDJONo { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string INDateTime { get; set; }
        public string InvoiceNo { get; set; }
        public string Port { get; set; }
        public string TrailerNo { get; set; }
        public string TranspoterName { get; set; }
        public string OutDateTime { get; set; }
        public double TransportationAmt { get; set; }
        public string MessageValidation1 { get; set; }
        public string fromdate { get; set; }
        public string Todate { get; set; }
        public string Checkon { get; set; }
        public string Activity { get; set; }
        public string BillNo { get; set; }




    }

    public class TransportBillMsg
    {

        public string ValidateMSG { get; set; }
        public string ValidateContainerno { get; set; }
    }
    public class InvoiceUpload
    {
        public string Amount { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string Group { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string PKGS { get; set; }
        public string Weight { get; set; }
        public string FromLocation { get; set; }
        public string TOlocation { get; set; }
        public string Vehicleno { get; set; }
        public string IGSTcal { get; set; }
        public string SGSTcal { get; set; }
        public string CGSTcal { get; set; }
        public string FromLocationID { get; set; }
        public string TOlocationID { get; set; }
        public double TransportID { get; set; }
        public string CargoTypeID { get; set; }
        public string TransportType { get; set; }
        public string CargoType { get; set; }
        public string checkGST { get; set; }

        public string message { get; set; }

    }
}
