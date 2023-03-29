using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using System.Data;
using HC = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Data.OleDb;

namespace TrackerMVC.Controllers
{
    [UserAuthenticationFilter] 
    public class LoadingPlanController : Controller
    {
        BM.LoadingPlan.LoadingPlan LP = new BM.LoadingPlan.LoadingPlan();
        BM.BLDataManager.GenerateBL BL = new BM.BLDataManager.GenerateBL();
        DataTable dtNew = new DataTable();
        // GET: LoadingPlan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadingPlan()
        {
            List<BE.LoadingPlanEntities> LoadingPlanList = new List<BE.LoadingPlanEntities>();
            LoadingPlanList = LP.getLoadingContainerList();
           // return Json(LoadingPlanList);
            return View(LoadingPlanList);
        }

        public ActionResult LoadingConfirmation()
        {
            List<BE.TrailersEntities> KalmarList = new List<BE.TrailersEntities>();
          KalmarList = LP.getKalmarDropDownList();
          ViewBag.KalmarList = new SelectList(KalmarList, "trailerid", "trailername");

            List<BE.LoadingPlanEntities> LoadingConfirmationList = new List<BE.LoadingPlanEntities>();
            LoadingConfirmationList = LP.getLoadingConfirmationList();
            // return Json(LoadingPlanList);
            return View(LoadingConfirmationList);
        }

        public ActionResult BookingContainerAllocation()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetContainerForAutoComplete(string ContainerNo)
        {
            List<string> nameList = new List<string>();
            if (ContainerNo != "")
            {
                nameList = LP.GetContainerAutoComplete(ContainerNo);
            }


            return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveLoadingPlan(string containerNo, string TrailerNo, long JONO)
        { 
        string message="";
        int Userid = Convert.ToInt32(Session["Tracker_userID"]);
        message = LP.SaveLoadingPlan(containerNo, TrailerNo, JONO, Userid);
        return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveLoadingConfirmation(string containerNo, string vehicleNo, int kalmarNo, long JONO)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.SaveLoadingConfirmation(containerNo, vehicleNo, kalmarNo,JONO, Userid);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FuelSlip()
        {
            //List<BE.FuelEntities> FuelList = new List<BE.FuelEntities>();
            //FuelList = LP.getFuelList();

            return View();

        }


        //code by prashant
        public ActionResult GetFuelSLipdata(string trailername)
        {
            List<BE.FuelEntities> FuelList = new List<BE.FuelEntities>();
            FuelList = LP.getFuelList(trailername);

            var jsonResult = Json(FuelList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        //code end by prashant

        [HttpPost]
        public JsonResult GetValidateTPDetails(string trailername)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.GetTPValidatedetails(trailername);
            return Json(message);

        }

        [HttpPost]
        //public ActionResult GenerateFuel(int planID, string TrailerNo, string Passing, string driver, string Transportor,
        //     string Activity, string containerCount, string Type, string From, string To)
        public ActionResult GenerateFuel(int planID)
        {
            //BE.FuelEntities FuelData = new BE.FuelEntities();

            //FuelData = GS.GetCutomerData(ID);
            //return PartialView(FuelData);
            //FuelData.TrailerNo = TrailerNo;
           // FuelData.Passing = Passing;
            //FuelData.driver = driver;
            //FuelData.Transportor = Transportor;
            //FuelData.Activity = Activity;
            //FuelData.containerCount = containerCount;
            //FuelData.Type = Type;
            //FuelData.From = From;
            //FuelData.To = To;
           // ViewBag.containerCount = Convert.ToString("1 X 20");
            //TempData["containerCount"] = Convert.ToString("1 X 20");
            //return RedirectToAction("DirectFuelSlip", "LoadingPlan");
            return View();
        }

         public ActionResult DirectFuelSlip()
         {
           //  List<BE.FuelEntities> FuelList = new List<BE.FuelEntities>();
            // FuelList = LP.getFuelList();
             List<BE.CargoType> CargoType = new List<BE.CargoType>();
             CargoType = BL.getCargoType();
             List<BE.DriversEntities> Drivers = new List<BE.DriversEntities>();
             Drivers = LP.getDrivers();
             List<BE.TransporterEntities> Transpoter = new List<BE.TransporterEntities>();
             Transpoter = LP.getTranspoter();

             List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
             Location = LP.getLocation();

             List<BE.PassingEntities> Passing = new List<BE.PassingEntities>();
             Passing = LP.getPassing();

            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = LP.getActivity();

            ViewBag.CargoType = new SelectList(CargoType, "Cargotypeid", "Cargotype");
            ViewBag.Drivers = new SelectList(Drivers, "driverID", "driverName");
            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            ViewBag.Passing = new SelectList(Passing, "passing", "Passing");
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");

            BE.FuelEntities fe = new BE.FuelEntities();
           // fe.containerCount = Convert.ToString(TempData["containerCount"]);
            return View();

         }

         



        public ActionResult PrintFuelSlip(string containerCount, string containerTypeID, string TrailerNo, string Passing, string driverID, string TransportorID, string FromID, string ToID, string Activity, string ReadingFrom, string ReadingTo, string fuel, string Amount1, string Amount2, string AdjustFuel)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            //LP.AddDirectFuel(containerCount, containerTypeID, TrailerNo, Passing, driverID, TransportorID, FromID, ToID, Activity, ReadingFrom, ReadingTo, fuel, Amount1, Amount2, AdjustFuel, Userid);

            int i = 1;
            TempData["containerCount"] = containerCount;
            TempData["containerTypeID"] = containerTypeID;
            TempData["TrailerNo"] = TrailerNo;
            TempData["Passing"] = Passing;
            TempData["driverID"] = driverID;
            TempData["TransportorID"] = TransportorID;
            TempData["FromID"] = FromID;
            TempData["ToID"] = ToID;
            TempData["Activity"] = Activity;
            TempData["ReadingFrom"] = ReadingFrom;
            TempData["ReadingTo"] = ReadingTo;
            TempData["fuel"] = fuel;
            TempData["Amount1"] = Amount1;
            TempData["Amount2"] = Amount2;
            TempData["AdjustFuel"] = AdjustFuel;
            //TempData["VoucherNo"] = voucherNo;
            //TempData["voucherDate"] = voucherDate;

            return Json(i);


        }

         public ActionResult PrintFuelSlipDirect(string voucherNo, string AutoID)
        {
            BE.FuelEntities fe = new BE.FuelEntities();
            //  fe = LP.getFuelDataForDirect(fl.TransportorID, fl.Activity, fl.Type, fl.containerCount,fl.From,fl.To);
            fe = LP.getPrintDataForFuelTarriff(voucherNo,AutoID);

           // return Json(fe, JsonRequestBehavior.AllowGet);

            ViewBag.containerCount = fe.containerCount;
            ViewBag.containerTypeID =fe.Type ;
            ViewBag.TrailerNo = fe.TrailerNo;
            ViewBag.Passing = fe.Passing;
            ViewBag.driverID =fe.driver;
            ViewBag.TransportorID = fe.Transportor;
            ViewBag.FromID =fe.From ;
            ViewBag.ToID = fe.To;
            ViewBag.Activity =fe.Activity ;
            ViewBag.ReadingFrom =fe.ReadingFrom ;
            ViewBag.ReadingTo = fe.ReadingTo;
            ViewBag.fuel = fe.fuel;
            ViewBag.Amount1 = fe.amount1;
            ViewBag.Amount2 = fe.amount2;
            ViewBag.AdjustFuel =fe.adjustAmount;
           // ViewBag.Date = fe.VoucherDate;
            ViewBag.VoucherDate = fe.VoucherDate;
            ViewBag.Passing = fe.Passing;
            ViewBag.SlipNo = fe.VoucherNo;
            //ViewBag.Addedby = ;
            //ViewBag.voucherNo = voucherNo;s
            return View();


        }
        public ActionResult PrintFuelSlipDirectNew(string voucherNo, string AutoID)
        {
            BE.FuelEntities fe = new BE.FuelEntities();
            //  fe = LP.getFuelDataForDirect(fl.TransportorID, fl.Activity, fl.Type, fl.containerCount,fl.From,fl.To);
            fe = LP.getPrintDataForFuelTarriff(voucherNo, AutoID);
            LP.UpdateFuelSlipPrint(voucherNo, AutoID);
            // return Json(fe, JsonRequestBehavior.AllowGet);
            ViewBag.containerCount = fe.containerCount;
            ViewBag.containerTypeID = fe.Type;
            ViewBag.TrailerNo = fe.TrailerNo;
            ViewBag.Passing = fe.Passing;
            ViewBag.driverID = fe.driverID;
            ViewBag.TransportorID = fe.Transportor;
            ViewBag.FromID = fe.From;
            ViewBag.ToID = fe.To;
            ViewBag.Activity = fe.Activity;
            ViewBag.ReadingFrom = fe.ReadingFrom;
            ViewBag.ReadingTo = fe.ReadingTo;
            ViewBag.fuel = fe.fuel;
            ViewBag.Amount1 = fe.amount1;
            ViewBag.Amount2 = fe.amount2;
            ViewBag.AdjustFuel = fe.adjustAmount;
            ViewBag.Total = Convert.ToDouble(fe.amount1) + Convert.ToDouble(fe.amount2);
            ViewBag.Date = Convert.ToDateTime(fe.VoucherDate).AddDays(5).ToString("dd/MM/yyyy");
            ViewBag.DateNow = Convert.ToDateTime(DateTime.Now).ToString("dd MMM yyyy HH:mm");
            ViewBag.PreparedBy = fe.preparedBy;
            ViewBag.ConName = fe.Compnayname;
            ViewBag.SLipdate = fe.Slipdate;

            ViewBag.VoucherDate = fe.VoucherDate;
            ViewBag.Passing = fe.Passing;
            ViewBag.SlipNo = Convert.ToString(fe.VoucherNo);
            ViewBag.Shrtcode = fe.ShortCode;
            ViewBag.Litre = fe.Litre;
            ViewBag.DieselNo = fe.DieselSlipNo;
            ViewBag.ContainerNo = fe.ContainerNo;
            ViewBag.fuelamount = fe.fuelAmount;
            string[] values = ViewBag.ContainerNo.Split(',');
            List<BE.FuelEntities> ContainerSet = new List<BE.FuelEntities>();
            for (int i = 0; i < values.Length; i++)
            {
                BE.FuelEntities set = new BE.FuelEntities();
                values[i] = values[i].Trim();
                set.ContainerNo = values[i];
                ContainerSet.Add(set);
            }
            ViewBag.values = ContainerSet;
            // return View();

            return PartialView();
        }

        public ActionResult PrintVoucherFuelSlipDirectNew(string voucherNo, string AutoID)
        {
            BE.FuelEntities fe = new BE.FuelEntities();
            //  fe = LP.getFuelDataForDirect(fl.TransportorID, fl.Activity, fl.Type, fl.containerCount,fl.From,fl.To);
            fe = LP.getPrintDataForFuelTarriff(voucherNo, AutoID);
            LP.UpdateVoucherFuelSlipPrint(voucherNo, AutoID);
            // return Json(fe, JsonRequestBehavior.AllowGet);

            ViewBag.containerCount = fe.containerCount;
            ViewBag.containerTypeID = fe.Type;
            ViewBag.TrailerNo = fe.TrailerNo;
            ViewBag.Passing = fe.Passing;
            ViewBag.driverID = fe.driverID;
            ViewBag.TransportorID = fe.Transportor;
            ViewBag.FromID = fe.From;
            ViewBag.ToID = fe.To;
            ViewBag.Activity = fe.Activity;
            ViewBag.ReadingFrom = fe.ReadingFrom;
            ViewBag.ReadingTo = fe.ReadingTo;
            ViewBag.fuel = fe.fuel;
            ViewBag.Amount1 = fe.amount1;
            ViewBag.Amount2 = fe.amount2;
            ViewBag.AdjustFuel = fe.adjustAmount;
            ViewBag.Total = Convert.ToDouble(fe.amount1) + Convert.ToDouble(fe.amount2);
            ViewBag.Date = Convert.ToDateTime(fe.VoucherDate).AddDays(5).ToString("dd/MM/yyyy");
            ViewBag.VoucherDate = fe.VoucherDate;
            ViewBag.Passing = fe.Passing;
            ViewBag.SlipNo = Convert.ToString(fe.VoucherNo);
            ViewBag.Shrtcode = fe.ShortCode;
            ViewBag.Litre = fe.Litre;
            ViewBag.PreparedBy = fe.preparedBy;
            ViewBag.InOut = fe.INOUT;
            ViewBag.DieselNo = fe.DieselSlipNo;
            ViewBag.ContainerNo = fe.ContainerNo;
            ViewBag.DeliveryAddress = fe.DelAddress;
            ViewBag.InOutDate = fe.InOutDate;
            ViewBag.VaucherNo = voucherNo;
            ViewBag.fuelAmount = fe.fuelAmount;

            //ViewBag.Addedby = ;
            //ViewBag.voucherNo = voucherNo;
            return PartialView();


        }


        [HttpPost]
         public ActionResult GetReadingFromData(string trailerno)
         {
             string ReadingFrom = LP.getReadingFrom(trailerno);
             return Json(ReadingFrom, JsonRequestBehavior.AllowGet);
         }
        public ActionResult GetTrailerData(int trailerID)
        {
            BE.FuelEntities fe = new BE.FuelEntities();
            //  fe = LP.getFuelDataForDirect(fl.TransportorID, fl.Activity, fl.Type, fl.containerCount,fl.From,fl.To);
            fe = LP.getTrailerWiseDriverData(trailerID);

            return Json(fe, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult GetGeneratedFuelSlip()
        {
            List<BE.FuelEntities> fe = new List<BE.FuelEntities>();
            fe = LP.GetGeneratedFuelSlipData();
            return View(fe);
        }

        //codes by Arti

        public ActionResult DirectGenerateFuel()
        {
            List<BE.CargoType> ContainerType = new List<BE.CargoType>();
            ContainerType = LP.getContainerTypeType();

            List<BE.DriversEntities> Drivers = new List<BE.DriversEntities>();
            Drivers = LP.getDrivers();
            List<BE.TransporterEntities> Transpoter = new List<BE.TransporterEntities>();
            Transpoter = LP.getTranspoter();

            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();

            //List<BE.PassingEntities> Passing = new List<BE.PassingEntities>();
            //Passing = LP.getPassing();

            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = LP.getActivity();

            List<BE.TrailersEntities> Trailer = new List<BE.TrailersEntities>();
            Trailer = LP.getTrailerList();

            ViewBag.CargoType = new SelectList(ContainerType, "Cargotypeid", "Cargotype");
            ViewBag.Drivers = new SelectList(Drivers, "driverID", "driverName");
            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            //ViewBag.Passing = new SelectList(Passing, "passing", "Passing");
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");
            ViewBag.Trailer = new SelectList(Trailer, "trailerID", "trailername");
            ViewBag.ForDirectFuelSlip = "1";
            return PartialView("GenerateFuelWithData");

        }
        [HttpPost]
        public ActionResult GenerateFuelWithData(string TrailerNo, string ContainerNo1, string ActivityType, int ActivityTypeID,string LocationYardID)
        {

            List<BE.CargoType> ContainerType = new List<BE.CargoType>();
            ContainerType = LP.getContainerTypeType();
            List<BE.DriversEntities> Drivers = new List<BE.DriversEntities>();
            Drivers = LP.getDrivers();
            List<BE.TransporterEntities> Transpoter = new List<BE.TransporterEntities>();
            Transpoter = LP.getGenerateTranspoter();

            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();

            //List<BE.PassingEntities> Passing = new List<BE.PassingEntities>();
            //Passing = LP.getPassing();

            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = LP.getActivity();

            List<BE.TrailersEntities> Trailer = new List<BE.TrailersEntities>();
            Trailer = LP.getTrailerList();

            List<BE.MovementCountEntities> MovCount = new List<BE.MovementCountEntities>();
            MovCount = LP.getMovCount();

            List<BE.MovementTypeEntities> MovType = new List<BE.MovementTypeEntities>();
            MovType = LP.getMovType();

            List<BE.CostCenterEntities> CostCenter = new List<BE.CostCenterEntities>();
            CostCenter = LP.getCostCenter();


            List<BE.DriverTypeEntities> Drivertype = new List<BE.DriverTypeEntities>();
            Drivertype = LP.getDriverTypeDetails();
            ViewBag.DriverType = new SelectList(Drivertype, "DriverID", "DriverType");


            List<BE.ScanTypeEntities> ScanType = new List<BE.ScanTypeEntities>();
            ScanType = LP.GetSanTypeDetails();
            ViewBag.Scantype = new SelectList(ScanType, "ScanID", "ScanType");

            List<BE.ActivityCycle> Activitycycle = new List<BE.ActivityCycle>();
            Activitycycle = LP.GetActivitycyclemaster();
            ViewBag.Activitycycle = new SelectList(Activitycycle, "ActivitycycleID", "ActivitycycleName");

            ViewBag.CargoType = new SelectList(ContainerType, "Cargotypeid", "Cargotype");
            ViewBag.Drivers = new SelectList(Drivers, "driverID", "driverName");
            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            // ViewBag.Passing = new SelectList(Passing, "passing", "passing");
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");
            ViewBag.Trailer = new SelectList(Trailer, "trailerID", "trailername");
            ViewBag.MovCount = new SelectList(MovCount, "MovCountID", "MovCount");
            ViewBag.MovType = new SelectList(MovType, "Mov_ID", "Mov_Type");
            ViewBag.CostCenter = new SelectList(CostCenter, "Cost_ID", "Cost_Center");

            // Code By Prashant
            List<BE.CostCenterFor> CostCenterFor = new List<BE.CostCenterFor>();
            CostCenterFor = LP.getCostCenterFor();
            ViewBag.CostCenterFor = new SelectList(CostCenterFor, "CodeCenterID", "CodeCenterType");
            //Code End by Prashant

            ViewBag.ForDirectFuelSlip = "0";
            BE.FuelEntities FuelData = new BE.FuelEntities();
            FuelData = LP.GetFuelData(TrailerNo, ContainerNo1, ActivityType, ActivityTypeID, LocationYardID);

            ViewBag.locationyardID = LocationYardID;
            BE.ActivityCycle Activityname = new BE.ActivityCycle();
            Activityname = LP.Getactivitycycle(ActivityTypeID,ActivityType );

            ViewBag.Activitycyclename = Activityname.ActivitycycleName;
            ViewBag.VehicleType = FuelData.VehicleType;
            ViewBag.Shifting = FuelData.Shifting;
            ViewBag.TrailerTransport = FuelData.TrailerTransid;
            ViewBag.PlanID = FuelData.planID;
            ViewBag.ContainerNoString = ContainerNo1;
            ViewBag.ContainerListForFuel = FuelData.FuelEntitiesContainerList;

            if (FuelData.validation == 0)
            {
                return Json(FuelData);
            }

            return PartialView(FuelData);
        }
        [HttpPost]
        public ActionResult UpdateFuelStatus(string TrailerNo, string LocationYardID, string ActivityID)
        {
            int i = 0;
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            i = LP.UpdateFuelStatus(TrailerNo, LocationYardID, ActivityID, Userid);
            return Json(i);
        }
        public ActionResult HoldVoucherSlip()
        {
            return View();
        }
        public ActionResult GetGeneratedFuelSlipForHold(string Date)
        {
            List<BE.FuelEntities> fe = new List<BE.FuelEntities>();
            fe = LP.GetGeneratedFuelSlipDataForHold(Date);
            var jsonResult = Json(fe, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
           // return View(fe);
        }
        [HttpPost]
        public ActionResult HoldVoucher(string voucherNo)
        {
            string message;
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.HoldVoucher(voucherNo, Userid);

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ClearVoucher(string voucherNo)
        {
            string message;
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.ClearVoucher(voucherNo, Userid);

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FuelTariffSetting()
        {
            List<BE.FuelEntities> Tarifflist = new List<BE.FuelEntities>();
            // Tarifflist = LP.GetFuelTariffList();


            List<BE.TransporterEntities> Transpoter = new List<BE.TransporterEntities>();
            Transpoter = LP.getTranspoter();
            List<BE.TransporterEntities> Transpoter1 = new List<BE.TransporterEntities>();
            Transpoter1 = LP.getTranspoterDetails();
            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            List<BE.ContainerSize> ContainerSize = new List<BE.ContainerSize>();
            ContainerSize = BL.getContainerSize();
            List<BE.CargoType> CargoType = new List<BE.CargoType>();
            CargoType = BL.getCargoType();
            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = LP.getActivity();


            List<BE.VehicleTypeEntities> VechileType = new List<BE.VehicleTypeEntities>();
            VechileType = LP.getVehicleDetails();

            List<BE.ScanTypeEntities> Scacntype = new List<BE.ScanTypeEntities>();
            Scacntype = LP.getScanType();

            List<BE.DriverTypeEntities> Drivertype = new List<BE.DriverTypeEntities>();
            Drivertype = LP.getDriverTypeDetails();
            List<BE.VehicleTypeEntities> TrailerType = new List<BE.VehicleTypeEntities>();
            TrailerType = LP.getTrailerTypeDetails();
            ViewBag.TrailerType = new SelectList(TrailerType, "VehicleTypeID", "VehicleType");

            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");
            ViewBag.Transpoter1 = new SelectList(Transpoter1, "TransID", "TransName");
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            ViewBag.ContainerSize = new SelectList(ContainerSize, "ContainerSizeID", "ContainerSizeName");
            ViewBag.CargoType = new SelectList(CargoType, "Cargotypeid", "Cargotype");
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");
            ViewBag.VechileType = new SelectList(VechileType, "VehicleTypeID", "VehicleType");
            ViewBag.Scacntype = new SelectList(Scacntype, "ScanID", "ScanType");
            ViewBag.Driver = new SelectList(Drivertype, "DriverID", "DriverType");
            return View(Tarifflist);
        }
        public ActionResult SaveFuelDirectSetting(int TranspoterID, string FromDate, string ToDate, int ActivityID, string InOut, int ContainerTypeID, string ContaierSize, int Fromlocation, int Tolocation, float Fuel, string Amount1, string Amount2, int RecordID,
           string passing, string TrailerType, string Drivertype, string VehicleType, string ScanType, string Weight, string status, string TxtFuelamount)

        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            LP.SaveFuelDirectSetting(TranspoterID, FromDate, ToDate, ActivityID, InOut, ContainerTypeID, ContaierSize, Fromlocation, Tolocation, Fuel, Amount1, Amount2, RecordID, Userid, passing, TrailerType, Drivertype, VehicleType, ScanType, Weight, status, TxtFuelamount);

            return Json(1);
        }

        public ActionResult GetFuelTarrifData(BE.FuelEntities fe)
        {
            List<BE.FuelEntities> Fuel = new List<BE.FuelEntities>();
            Fuel = LP.GetFuelTariffList(fe);
            //  ViewBag.FuelTarrifList = Fuel;
            //   return Json(Fuel, JsonRequestBehavior.AllowGet); 
            var jsonResult = Json(Fuel, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult CopyFuelTarrif(Int64 ID)
        {
            List<BE.TransporterEntities> Transpoter = new List<BE.TransporterEntities>();
            Transpoter = LP.getTranspoter();
            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            List<BE.ContainerSize> ContainerSize = new List<BE.ContainerSize>();
            ContainerSize = BL.getContainerSize();
            List<BE.CargoType> CargoType = new List<BE.CargoType>();
            CargoType = BL.getCargoType();
            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = LP.getActivity();


            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");
            ViewBag.ContainerSize = new SelectList(ContainerSize, "ContainerSizeID", "ContainerSizeName");
            ViewBag.CargoType = new SelectList(CargoType, "Cargotypeid", "Cargotype");
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");

            List<BE.FuelEntities> Fuel = new List<BE.FuelEntities>();
            // Fuel = LP.GetFuelTariffList(ID);
            return PartialView(Fuel);
        }

        public ActionResult GetActivityMaxFuel(string Activity)
        {
            int MaxFuelValue = 0;
            MaxFuelValue = LP.getActivityMaxFuel(Activity);
            return Json(MaxFuelValue);
        }

        public ActionResult VoucherValidation(string TrailerNo, string ContainerNo1, string ActivityType, int ActivityTypeID,string  LocationYardID)
        {
            if (LocationYardID == null)
            {
                LocationYardID = "0";
            }
            BE.FuelEntities fl = new BE.FuelEntities();
            fl = LP.VoucherValidation(TrailerNo, ContainerNo1, ActivityTypeID, ActivityType, LocationYardID);
            return Json(fl);
        }

        public ActionResult ExportToExcel()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["VoucherDetails"];
            //DataTable dt = (DataTable)ViewData["VoucherDetails"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=VoucherDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Voucher Details Summary<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }
        //by prashant
        public ActionResult DriverPayment()
        {

            List<BE.TransporterEntities> Transpoter = new List<BE.TransporterEntities>();
            Transpoter = LP.getDriverTranspoter();
            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");

            return View();
        }

        public JsonResult AjaxDriverPaymentMode(string FromDate, string ToDate, int Transporterid)
        {
            List<BE.DriverPaymentEntities> drivers = new List<BE.DriverPaymentEntities>();
            drivers = LP.getDriversdetails(FromDate, ToDate, Transporterid);
            var jsonResult = Json(drivers, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        // Codes By Prashant
        public ActionResult VoucherDetailsSummary()
        {


            return View();
        }
        
        [HttpPost]
        public ActionResult AjaxGetVoucherDetails(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            var sessionTime = Session.Timeout;
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_VoucherDetailsSummary '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["VoucherDetails"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult DieselSlipSummary()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AJAXGetDieselSlip(string FromDate, string ToDate)
        {
            List<BE.VoucherDetailsEntities> VoucherList = new List<BE.VoucherDetailsEntities>();
            VoucherList = LP.GetDieselSlip(FromDate, ToDate);
            var jsonResult = Json(VoucherList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        // Codes End By Prashant

        public ActionResult TransportAdminModule()
        {
            List<BE.DriversEntities> Drivers = new List<BE.DriversEntities>();
            Drivers = LP.getDrivers();
            ViewBag.Drivers = new SelectList(Drivers, "driverID", "driverName");
            List<BE.LocationMaster> Location = new List<BE.LocationMaster>();
            Location = LP.getLocation();
            ViewBag.Location = new SelectList(Location, "LocationID", "Location");


            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = LP.getActivity();
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");

            List<BE.MovementCountEntities> MovCount = new List<BE.MovementCountEntities>();
            MovCount = LP.getMovCount();
            ViewBag.MovCount = new SelectList(MovCount, "MovCountID", "MovCount");
            return View();
        }

        public JsonResult AjaxGetVoucherEditDetails(string VoucherNo, string ActivityType)
        {
            List<BE.VoucherDetailsEntities> Containerdetails = new List<BE.VoucherDetailsEntities>();
            Containerdetails = LP.GetVoucherEditDetails(VoucherNo, ActivityType);
            var jsonResult = Json(Containerdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public JsonResult AjaxGetContainerdetails(string VoucherNo, string ActivityType)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_Admin_Transportadmin_module '" + VoucherNo + "','" + ActivityType + "'");

            List<BE.VoucherDetailsEntities> Customerlst = new List<BE.VoucherDetailsEntities>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BE.VoucherDetailsEntities customer = new BE.VoucherDetailsEntities();

                    customer.ContainerNo = Convert.ToString(row["ContainerNo"]);
                    customer.Size = Convert.ToString(row["Size"]);
                    customer.SRNo = Convert.ToString(row["Sr No"]);
                    customer.AutoID = Convert.ToString(row["AutoID"]);

                    Customerlst.Add(customer);
                }
            }

            var jsonResult = Json(Customerlst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult AjaxUpdateVoucherDetails(string VoucherNo, string Fuel, string AountBank, string AmountCash, string ActivityType, string Remarks, int fromlocationID,
        int ToLocationID, string ActivityID, string Fastagamount, string adjFuel, List<BE.VoucherDetailsEntities> Containerdetails)
        {
            string message = "";

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("AutoID");

            if (Containerdetails != null)
            {
                foreach (BE.VoucherDetailsEntities item in Containerdetails)
                {
                    DataRow row = dataTable.NewRow();

                    row["Containerno"] = item.ContainerNo;
                    row["AutoID"] = item.AutoID;

                    dataTable.Rows.Add(row);
                }
            }
            //int Count = 1;

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.UpdateVoucherDetails(VoucherNo, Fuel, AountBank, AmountCash, Userid, ActivityType, Remarks,
                fromlocationID, ToLocationID, ActivityID, Fastagamount, adjFuel, dataTable);
            return Json(message);
        }

        public ActionResult AjaxCancelVoucherDetails(string VoucherNo, string CancelRemarks)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.CancelVoucherDetails(VoucherNo, CancelRemarks, Userid);
            return Json(message);
        }

        public ActionResult ISprintSave(string VoucherNo, string ActivityType, string Remarks)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.REPrintFuelVoucherDetails(VoucherNo, ActivityType, Remarks, Userid);
            return Json(message);
        }

        public ActionResult ISprintVoucherSave(string VoucherNo, string ActivityType, string Remarks)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.REPrintVoucherSlipVoucherDetails(VoucherNo, ActivityType, Remarks, Userid);
            return Json(message);
        }
        [HttpPost]
        public ActionResult GenerateFuelSlip(string Fuel, string AountBank, string AmountCash,
            string VoucherNo, string ActivityType, string Tolocation, string FromLocation,
            string DriverName,string Transporter)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.GenerateFuelSLip(Fuel, AountBank, AmountCash, VoucherNo, ActivityType, Tolocation, FromLocation, DriverName, Userid, Transporter);
            return Json(message);
        }
        [HttpPost]
        public ActionResult ChangeDriver(string Fuel, string AountBank, string AmountCash,
           string VoucherNo, string ActivityType, string Tolocation, string FromLocation,
           string DriverName)
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.ChangeDriverFromVoucher(Fuel, AountBank, AmountCash, VoucherNo, ActivityType, Tolocation, FromLocation, DriverName, Userid);
            return Json(message);
        }
        [HttpPost]
        public ActionResult ChangeSize(string VoucherNo, string ActivityType, string containerCount )
        {
            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.ChangeSizeFromVoucher(  VoucherNo, ActivityType, containerCount , Userid);
            return Json(message);
        }
        //public ActionResult AjaxUpdateVoucherDetails(string VoucherNo,string Fuel, string AountBank, string AmountCash)
        //{
        //    string message = "";
        //    int Userid = Convert.ToInt32(Session["Tracker_userID"]);
        //    message = LP.UpdateVoucherDetails(VoucherNo, Fuel, AountBank, AmountCash, Userid);
        //    return Json(message);
        //}

        //Code start by Rahul
        public ActionResult AjaxGetPendingAdditionalMovementRequest()
        {
            List<BE.PendingAdditionalMovementRequestEntities> PendingAdditionalMovementlist = new List<BE.PendingAdditionalMovementRequestEntities>();
            PendingAdditionalMovementlist = LP.GetPendingAdditionalMovementRequest();
            var jsonResult = Json(PendingAdditionalMovementlist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult GenerateRequest(int RequestID)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.GenerateRequest(RequestID, UserID);
            return Json(message);

        }

        public ActionResult FuelSlipClose()
        {
            List<BE.FuelEntities> FuelList = new List<BE.FuelEntities>();
            FuelList = LP.getFuelCloseList();

            return View(FuelList);

        }
       
        public ActionResult CloseFuelSlipWithData(String TrailerNo)
        {
            //List<BE.FuelEntities> DocList = new List<BE.FuelEntities>();
            //DocList = LP.GetDocList(TrailerNo);
            ////DocLRList = DocList.DocList;
            ////ViewBag.DocList = new SelectList(DocList.DocList, "DocID", "DocName");
            //ViewBag.ContainerNo = DocList[0].ContainerNo;
            //ViewBag.FromLocation = DocList[0].From;
            //ViewBag.ToLocation = DocList[0].To;
            //ViewBag.VehicleNo = DocList[0].TrailerNo;
            ////ViewBag.LorryNo = LRNo;
            //return PartialView(DocList);
            BE.FuelEntities PT = new BE.FuelEntities();
            PT = LP.GetDocList(TrailerNo);
            ViewBag.ContainerNo = PT.ContainerNo;
            ViewBag.FromLocation = PT.From;
            ViewBag.ToLocation = PT.To;
            ViewBag.VehicleNo = PT.TrailerNo;
           // ViewBag.vendorname = PT.vendorname;
            return PartialView();
        }
        //code end by rahul

        //Code by Prashant     24 Oct 2019
        [HttpPost]
        public JsonResult AjaxImportFile()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            BE.UploadVoucherTariffEntities VOuchertraiffList = new BE.UploadVoucherTariffEntities();
            List<BE.UploadVoucherTariffEntities> VouchertraiffDetails = new List<BE.UploadVoucherTariffEntities>();
            if (Request.Files.Count > 0)
            {
                try
                {

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/ImportFile/"), fname);
                        //  fname = Path.Combine(fname);
                        file.SaveAs(fname);
                        string extension = Path.GetExtension(fname);
                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                // conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                            case ".xlsx": //Excel 07 and above.
                                //conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                        }
                        //BL.AddErrorLog(conString.Replace("'",""));
                        //BL.AddErrorLog(fname.Replace("'", ""));
                        //  conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                        //  BL.AddErrorLog("Connection close");
                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            // BL.AddErrorLog("OleDbConnection");
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                // BL.AddErrorLog("cmdExcel");
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    //  BL.AddErrorLog("odaExcel");
                                    DataTable dt = new DataTable();
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    //  BL.AddErrorLog("Open connection");
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    //  BL.AddErrorLog(sheetName);
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();

                                    DataTable VOucherTraifftDT = new DataTable();

                                    VOucherTraifftDT.Columns.Add("Transporter");
                                    VOucherTraifftDT.Columns.Add("Effective From");
                                    VOucherTraifftDT.Columns.Add("Effective Upto");
                                    VOucherTraifftDT.Columns.Add("Activity");
                                    VOucherTraifftDT.Columns.Add("STATUS");
                                    VOucherTraifftDT.Columns.Add("In/OUT");
                                    VOucherTraifftDT.Columns.Add("Container Type");
                                    VOucherTraifftDT.Columns.Add("Size");
                                    VOucherTraifftDT.Columns.Add("From Location");
                                    VOucherTraifftDT.Columns.Add("To Location");
                                    VOucherTraifftDT.Columns.Add("Passing");
                                    VOucherTraifftDT.Columns.Add("Trailer Type");
                                    VOucherTraifftDT.Columns.Add("Driver Type");
                                    VOucherTraifftDT.Columns.Add("Scan Type");
                                    VOucherTraifftDT.Columns.Add("Weight");
                                    VOucherTraifftDT.Columns.Add("Fuel (Liter)");
                                    VOucherTraifftDT.Columns.Add("Fuel (Amount)");
                                    VOucherTraifftDT.Columns.Add("Amount1 (Cash)");
                                    VOucherTraifftDT.Columns.Add("Amount2 (Bank)");
                                    VOucherTraifftDT.Columns.Add("VEH TYPE");

                                    //DriverPaymentDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        if (row[0].ToString() != "" && row[4].ToString() != "" && row[5].ToString() != "" && row[6].ToString() != "" && row[7].ToString() != "" && row[8].ToString() != "" && row[9].ToString() != "" && row[10].ToString() != "" && row[11].ToString() != "" && row[15].ToString() != "" && row[16].ToString() != "" && row[17].ToString() != "" && row[18].ToString() != "")
                                        {


                                            DataRow dar = VOucherTraifftDT.NewRow();

                                            dar["Transporter"] = row[0].ToString().ToUpper();
                                            dar["Effective From"] = row[1].ToString();
                                            dar["Effective Upto"] = row[2].ToString();
                                            dar["Activity"] = row[3].ToString().ToUpper();
                                            dar["STATUS"] = row[4].ToString().ToUpper();
                                            dar["In/OUT"] = row[5].ToString();
                                            dar["Container Type"] = row[6].ToString();
                                            dar["Size"] = row[7].ToString();
                                            dar["From Location"] = row[8].ToString();
                                            dar["To Location"] = row[9].ToString();
                                            dar["Passing"] = row[10].ToString();
                                            dar["Trailer Type"] = row[11].ToString();
                                            dar["Driver Type"] = row[12].ToString();
                                            dar["Scan Type"] = row[13].ToString();
                                            dar["Weight"] = row[14].ToString();
                                            dar["Fuel (Liter)"] = row[15].ToString();
                                            dar["Fuel (Amount)"] = row[16].ToString();
                                            dar["Amount1 (Cash)"] = row[17].ToString();
                                            dar["Amount2 (Bank)"] = row[18].ToString();
                                            dar["VEH TYPE"] = row[19].ToString();

                                            VOucherTraifftDT.Rows.Add(dar);
                                            int linenum = dt.Rows.IndexOf(row);
                                            if (linenum == 1050)
                                            {
                                                int count1 = 0;
                                            }
                                        }
                                        else
                                        {
                                            return Json("Some columns are blank. Please check and re-import...");
                                        }
                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }

                                    if (VOucherTraifftDT != null)
                                    {

                                        VouchertraiffDetails = LP.UpdateVoucherTraiffDetails(VOucherTraifftDT, Userid);

                                        //if (message == "Records updated successfully")
                                        //{
                                        //    message = "Records imported and updated successfully";
                                        //}
                                        //   trainTrackerProvider.UpdateDischargeDateJO(DischargeListDT, userId);
                                        //  DischargeList = trainTrackerProvider.UpdateDischargeDateJO(DischargeListDT, Userid);
                                        var jsonResult = Json(VouchertraiffDetails, JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;
                                        //var returnField = new { Issuedata = 1, message = message };

                                        //return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                                    }

                                }
                            }
                        }
                    }
                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    //  Console.WriteLine("Error occurred. Error details: " + ex.Message);
                    //  BL.AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'",""));
                    // return Json("Error occurred. Error details: " + ex.Message);
                    return Json(1);
                    //  return Json("FormData is not supported.");
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        [HttpPost]
        public JsonResult AjaxValidation(List<BE.UploadVoucherTariffEntities> VoucherDetails)
        {
            string message = "";
            string message1 = "";

            List<BE.UploadVoucherTariffEntities> Validation = new List<BE.UploadVoucherTariffEntities>();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("FromLocation");
            dataTable.Columns.Add("TOlocation");


            foreach (BE.UploadVoucherTariffEntities item in VoucherDetails)
            {
                DataRow row = dataTable.NewRow();


                row["Activity"] = item.Activity;
                row["FromLocation"] = item.FromLocation;
                row["TOlocation"] = item.TOlocation;


                dataTable.Rows.Add(row);
            }

            message = LP.VoucherValidation(dataTable);

            if (message != "")
            {

                message1 += message;
            }
            else
            {
                message1 = "New";
            }
            return new JsonResult() { Data = message1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        [HttpPost]
        public ActionResult SaveVoucherList(List<BE.UploadVoucherTariffEntities> VoucherDetails, DateTime entrydate)
        {

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Transporter");
            dataTable.Columns.Add("EffectiveFrom");
            dataTable.Columns.Add("EffectiveUpto");
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("Status");
            dataTable.Columns.Add("InOut");
            dataTable.Columns.Add("ContainerTYpe");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("FromLocation");
            dataTable.Columns.Add("TOlocation");
            dataTable.Columns.Add("Passing");
            dataTable.Columns.Add("TrailerTYpe");
            dataTable.Columns.Add("DriverType");
            dataTable.Columns.Add("ScanType");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("Fuel");
            dataTable.Columns.Add("FuelAmount");
            dataTable.Columns.Add("Amount1");
            dataTable.Columns.Add("Amount2");
            dataTable.Columns.Add("VehicleType");

            dataTable.TableName = "PT_VoucherTraiffUpload";
            foreach (BE.UploadVoucherTariffEntities item in VoucherDetails)
            {


                DataRow row = dataTable.NewRow();

                row["Transporter"] = item.Transporter;
                row["EffectiveFrom"] = Convert.ToDateTime(item.EffectiveFrom).ToString("dd-MMM-yyyy");
                row["EffectiveUpto"] = Convert.ToDateTime(item.EffectiveUpto).ToString("dd-MMM-yyyy");
                row["Activity"] = item.Activity;
                row["Status"] = item.Status;
                row["InOut"] = item.InOut;
                row["ContainerTYpe"] = item.ContainerTYpe;
                row["Size"] = item.Size;
                row["FromLocation"] = item.FromLocation;
                row["TOlocation"] = item.TOlocation;
                row["Passing"] = item.Passing;
                row["TrailerTYpe"] = item.TrailerTYpe;
                row["DriverType"] = item.DriverType;
                row["ScanType"] = item.ScanType;
                row["Weight"] = item.Weight;
                row["Fuel"] = item.Fuel;
                row["FuelAmount"] = item.FuelAmount;
                row["Amount1"] = item.Amount1;
                row["Amount2"] = item.Amount2;
                row["VehicleType"] = item.VehicleType;

                dataTable.Rows.Add(row);
            }


            int UserID = Convert.ToInt32(Session["Tracker_userID"]);

            BE.UploadVoucherTariffEntities Vouchertariff = new BE.UploadVoucherTariffEntities();
            Vouchertariff = LP.SaveVOucherLIstList(dataTable, UserID, entrydate);
            return new JsonResult() { Data = Vouchertariff, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        [HttpPost]
        public ActionResult CancelVoucherTariff(List<BE.CancelVoucherTariffEntities> TariffNo)
        {


            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("AutoID");
            dataTable.TableName = "PT_CancelVoucherTariff";


            foreach (BE.CancelVoucherTariffEntities item in TariffNo)
            {
                DataRow row = dataTable.NewRow();

                row["AutoID"] = item.AutoID;

                dataTable.Rows.Add(row);
            }

            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = LP.CancelVoucherTariff(dataTable, Userid);
            return Json(message);

        }

        [HttpPost]
        public JsonResult CancelVehicleStatus(string transid, string Remarks)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.validateUser(UserID);
            if (message != "")
            {
                message = LP.GetVehicleStatusCancel(transid, Remarks, UserID);
                message = "record cancel successfully!";
            }
            else
            {
                message = "you are not authorized to perform this operation!";
            }

            return Json(message);

        }

        public ActionResult VoucherCloseDetails()
        {
            


            return View();
        }


        public JsonResult AjaxGetVoucherCloseDetails(string VoucherNo)
        {
            List<BE.VoucherDetailsEntities> Containerdetails = new List<BE.VoucherDetailsEntities>();
            Containerdetails = LP.GetVoucherCloseDetails(VoucherNo);
            var jsonResult = Json(Containerdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult AjaxUpdateVoucherCloseDetails(string VoucherNo1, string Trailername, string Activityname, string FromID, string Toid)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.GetCloseDetails(VoucherNo1, Trailername, Activityname, FromID, Toid, UserID);
            return Json(message);



        }

        //code start by rahul 09-11-2019
        public ActionResult AddDirectFuel(string containerCount, int containerTypeID, string TrailerNo, string Passing, int driverID, int TransportorID, int FromID, int ToID, int Activity, string ReadingFrom, string ReadingTo,
            string fuel, string Amount1, string Amount2, string AdjustFuel, string PlanID, string INOUT, string ContainerNo,
            int TarrifID, string VehTransID, string Remarks, string MovType, string CostCenter, string CostCenterFor,
            string TotalWeight, string Fuelamount,string Fastagamount,string AdvanceFuel, string AdvanceCash, string AdvanceBank, string LoanID,List<BE.FuelEntitiesContainerList> FuelEntitiesContainerList)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("JoNo");
            dataTable.TableName = "PT_ContainerListForFuel";

            //int Count = 1;
            foreach (BE.FuelEntitiesContainerList item in FuelEntitiesContainerList)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Weight"] = item.Weight;
                row["JoNo"] = item.JoNo;
                dataTable.Rows.Add(row);
            }
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.FuelEntities fe = new BE.FuelEntities();
            fe = LP.AddDirectFuel(containerCount, containerTypeID, TrailerNo, Passing, driverID,
                TransportorID, FromID, ToID, Activity, ReadingFrom, ReadingTo, fuel, Amount1, Amount2,
                AdjustFuel, Userid, PlanID, INOUT, ContainerNo, TarrifID, VehTransID, Remarks, MovType,
                CostCenter, CostCenterFor, TotalWeight, Fuelamount, Fastagamount, AdvanceFuel, AdvanceCash, AdvanceBank, LoanID, dataTable);

            return Json(fe, JsonRequestBehavior.AllowGet);

            // return Json(1);
        }
        [HttpPost]
        public ActionResult GetFuelData(BE.FuelEntities fl)
        {

            BE.FuelEntities fe = new BE.FuelEntities();
            //  fe = LP.getFuelDataForDirect(fl.TransportorID, fl.Activity, fl.Type, fl.containerCount,fl.From,fl.To);
            fe = LP.getFuelDataForDirect(fl.TransportorID, fl.ActivityID, fl.ConTypeID, fl.containerCount, fl.FromID, fl.ToID, fl.INOUT, fl.TrailerType, fl.Passing, fl.driver, fl.TrailerNo, fl.TotalWeight, fl.VehTransID,fl.ScanTypeID, fl.ScanCount);

            return Json(fe, JsonRequestBehavior.AllowGet);
        }
        //code end by rahul 09-11-2019
        [HttpPost]
        public ActionResult AjaxGetVoucherClosedDetails(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_VoucherCancelDetailsSummary '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["VoucherDetails"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        [HttpPost]
        public JsonResult GetDriverTypeDetails(int TransportName)
        {


            List<BE.DriverTypeEntities> Trailerno = new List<BE.DriverTypeEntities>();
            Trailerno = LP.getDriverTypeDetailsList(TransportName);
            return new JsonResult() { Data = Trailerno, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        [HttpPost]
        public ActionResult AjaxGetupdatedVoucherDetails(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetTransportUpdatedDetails '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["TransportUpdatedDetails"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public ActionResult ExportToExcelTransport()
        {
            DataTable dt = (DataTable)Session["TransportUpdatedDetails"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=TransportAdminDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Transport Admin Module Summary Report <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }


        public ActionResult FuelBillVerifications()
        {
            List<BE.FuelVendorMaster> FuelVendorMaster = new List<BE.FuelVendorMaster>();
            FuelVendorMaster = LP.getFuelVendorMaster();
            ViewBag.VendorName = new SelectList(FuelVendorMaster, "fuel_Vendorid", "fuelVendorName");

            return View();
        }

        public ActionResult SearchBillNo(string searchcerteria)
        {
            List<BE.FuelBillverificationsEntiites> TripDets = new List<BE.FuelBillverificationsEntiites>();
            TripDets = LP.GetBillNodetails(searchcerteria);
            var jsonResult = Json(TripDets, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult GetFilltablesDetails(string BillYear, string Billno)
        {
            DataTable LoadingExcel = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            LoadingExcel = db.sub_GetDatatable("get_sp_vendorBillmodifySummary '" + Billno + "','" + BillYear + "'");



            string json = JsonConvert.SerializeObject(LoadingExcel);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        [HttpPost]
        public JsonResult AjaxImportFuelBillVerification()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            string message1 = "";
            string messageCheck = "";
            BE.FuelBillVerificationDetails VOuchertraiffList = new BE.FuelBillVerificationDetails();
            List<BE.FuelBillVerificationDetails> VouchertraiffDetails = new List<BE.FuelBillVerificationDetails>();
            if (Request.Files.Count > 0)
            {
                try
                {

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/ImportFileBill/"), fname);
                        //  fname = Path.Combine(fname);
                        file.SaveAs(fname);
                        string extension = Path.GetExtension(fname);
                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                // conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                            case ".xlsx": //Excel 07 and above.
                                //conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                        }
                        //BL.AddErrorLog(conString.Replace("'",""));
                        //BL.AddErrorLog(fname.Replace("'", ""));
                        //  conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                        //  BL.AddErrorLog("Connection close");
                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            // BL.AddErrorLog("OleDbConnection");
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                // BL.AddErrorLog("cmdExcel");
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    //  BL.AddErrorLog("odaExcel");
                                    DataTable dt = new DataTable();
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    //  BL.AddErrorLog("Open connection");
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    //  BL.AddErrorLog(sheetName);
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();

                                    DataTable VOucherTraifftDT = new DataTable();


                                    VOucherTraifftDT.Columns.Add("Slip no");
                                    VOucherTraifftDT.Columns.Add("Fuel qty");
                                    VOucherTraifftDT.Columns.Add("Rate");


                                    //DriverPaymentDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        if (row[0].ToString() != "")
                                        {


                                            DataRow dar = VOucherTraifftDT.NewRow();

                                            dar["Slip no"] = row[0].ToString().ToUpper();
                                            dar["Fuel qty"] = row[1].ToString();
                                            dar["Rate"] = row[2].ToString();
                                            VOucherTraifftDT.Rows.Add(dar);
                                            int linenum = dt.Rows.IndexOf(row);
                                            if (linenum == 1050)
                                            {
                                                int count1 = 0;
                                            }
                                        }
                                        else
                                        {
                                            return Json("Some columns are blank. Please check and re-import...");
                                        }
                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }

                                    if (VOucherTraifftDT != null)
                                    {

                                        messageCheck = LP.CheckSlipExits(VOucherTraifftDT);


                                        if (messageCheck != "")
                                        {
                                            var returnField = new { Getmessage = messageCheck, BillVerificationDetails = "" };
                                            var jsonResult = Json(returnField, JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                        }
                                        else
                                        {
                                            message = LP.CheckBillVerificatioinvalidate(VOucherTraifftDT);
                                        }




                                        if (message != "")
                                        {
                                            var returnField = new { Getmessage = message, BillVerificationDetails = "" };
                                            var jsonResult = Json(returnField, JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                        }
                                        else
                                        {

                                            message1 = LP.CheckFuelBillVerificatioinvalidate(VOucherTraifftDT);


                                        }
                                        if (message1 != "")
                                        {
                                            var returnField = new { Getmessage = message1, BillVerificationDetails = "" };
                                            var jsonResult = Json(returnField, JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                        }
                                        else
                                        {
                                            VouchertraiffDetails = LP.ImportBillVerificationDetails(VOucherTraifftDT);


                                            var returnField = new { Getmessage = "", BillVerificationDetails = VouchertraiffDetails };
                                            var jsonResult = Json(returnField, JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                        }


                                    }

                                }
                            }
                        }
                    }
                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred. Error details: " + ex.Message);
                    BL.AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", ""));
                    return Json("Error occurred. Error details: " + ex.Message);
                    //return Json(1);
                    //  return Json("FormData is not supported.");
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }


        public ActionResult SaveFuelBillVerifications(List<BE.FuelBillVerificationDetails> SlipNoList, string BillYear, string Billnew, string Billdate, string VendorName,
             string Slipno1, string Todate, string Fromdate, string Rate1, string Netamount, string TCSpercentage, string TCSAmount, string Grandtotal, string GSTID)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Slipno");
            dataTable.Columns.Add("VoucherNo");
            dataTable.Columns.Add("SLIpdate");
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("trailerNo");
            dataTable.Columns.Add("Fromloc");
            dataTable.Columns.Add("Toloc");
            dataTable.Columns.Add("fueltype");
            dataTable.Columns.Add("VendorQty");
            dataTable.Columns.Add("FuelQty");
            dataTable.Columns.Add("Rate");
            dataTable.Columns.Add("totamt");
            //int Count = 1;
            foreach (BE.FuelBillVerificationDetails item in SlipNoList)
            {
                DataRow row = dataTable.NewRow();

                row["Slipno"] = item.SlipNo;
                row["VoucherNo"] = item.Voucherno;
                row["SLIpdate"] = item.SLIpdate;
                row["Activity"] = item.Activity;
                row["trailerNo"] = item.trailerNo;
                row["Fromloc"] = item.Fromloc;
                row["Toloc"] = item.Toloc;
                row["fueltype"] = item.fueltype;
                row["VendorQty"] = item.VendorQty;
                row["FuelQty"] = item.FuelQty;
                row["Rate"] = item.Rate;
                row["totamt"] = item.totamt;
                dataTable.Rows.Add(row);
            }
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            message = LP.SaveBillVerificationDetails(dataTable, BillYear, Billnew, Billdate, VendorName, Slipno1, Todate, Fromdate, Rate1, Userid,
                Netamount, TCSpercentage, TCSAmount, Grandtotal, GSTID);

            return Json(message, JsonRequestBehavior.AllowGet);

            // return Json(1);
        }

        [HttpPost]
        public JsonResult GetBillVerificationDetails(string FromDate, string ToDate, string searchcerteria, string Searchtext)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            var InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("get_sp_fuel_BillSummary '" + FromDate + "','" + ToDate + "','" + searchcerteria + "','" + Searchtext + "'");
            Session["BillVerificationDetails"] = tblInvoiceList;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            if (tblInvoiceList.Rows.Count > 0)
            {
                InvoiceList = JsonConvert.SerializeObject(tblInvoiceList);
            }
            else
            {
                InvoiceList = "0";
            }

            var jsonResult = Json(InvoiceList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult ExportToExcelBillVerificationCode()
        {
            DataTable dt = (DataTable)Session["BillVerificationDetails"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Fuel Bill Veriifcations Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Fuel Bill Verification Details Report <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }


        public ActionResult CancelFuelBillverification(List<BE.FuelBillVerificationDetails> SlipNoList)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Slipno");
            dataTable.Columns.Add("Voucherno");

            //int Count = 1;
            foreach (BE.FuelBillVerificationDetails item in SlipNoList)
            {
                DataRow row = dataTable.NewRow();

                row["Slipno"] = item.SlipNo;
                row["Voucherno"] = item.Voucherno;

                dataTable.Rows.Add(row);
            }
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            message = LP.CancelFuelBillverification(dataTable, Userid);

            return Json(message, JsonRequestBehavior.AllowGet);

            // return Json(1);
        }


        public ActionResult ajaxupdateFuelbillverification(string BillNo, string Invoiceno, string vendorid, string billdate, string Workyear)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);
            message = LP.UpdateFuelbillverification(BillNo, Invoiceno, vendorid, billdate, Workyear, UserID);
            return Json(message);
        }
        public ActionResult AddAdditionalChargesForTariff()
        {
            List<BE.TransporterEntities> Transpoter = new List<BE.TransporterEntities>();
            Transpoter = LP.getDriverTranspoter();
            ViewBag.Transpoter = new SelectList(Transpoter, "TransID", "TransName");


            List<BE.DriverTypeEntities> Drivertype = new List<BE.DriverTypeEntities>();
            Drivertype = LP.getDriverTypeDetails();
            ViewBag.DriverType = new SelectList(Drivertype, "DriverID", "DriverType");

            List<BE.ActivityMaster> Activity = new List<BE.ActivityMaster>();
            Activity = LP.getActivity();
            ViewBag.Activity = new SelectList(Activity, "AutoID", "Activity");

            List<BE.MovementCountEntities> MovCount = new List<BE.MovementCountEntities>();
            MovCount = LP.getAddMovCount();
            ViewBag.MovCount = new SelectList(MovCount, "MovCountID", "MovCount");

            return View();
        }



        public ActionResult SaveAdditionalTariffDetails(string Transporter, string Activity, string Drivertype, string Inout, string ContainerCount, string cash, string bank, string Fromdate, string Todate, string Entryid, string Fuel)
        {

            string message = "";
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);

            message = LP.SaveFuelTairffAdd(Transporter, Activity, Drivertype, Inout, ContainerCount, cash, bank, Fromdate, Todate, Userid, Entryid, Fuel);
            return Json(message);
        }


        [HttpPost]
        public ActionResult AjaxGetAdditionaltariffdetails()
        {
            DataTable dt = new DataTable();
            var sessionTime = Session.Timeout;
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetAddadditionalTariffDetails");
            Session["VoucherDetails"] = dt;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult PumpAcknowledgement()
        {
            List<BE.DriversEntities> Drivers = new List<BE.DriversEntities>();
            Drivers = LP.getDrivers();
            ViewBag.Drivers = new SelectList(Drivers, "driverID", "driverName");
            return View();
        }

        public JsonResult GetSLipnowiseFuel(int Trailerid, string SLipNo)
        {
            BE.PumpAcknowledgementEntites GetList = new BE.PumpAcknowledgementEntites();
            GetList = LP.GetSlipWiseFuel(Trailerid, SLipNo);


            var jsonResult = Json(GetList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult SaveFuelAcknowledgement(string Trailerid, string SLipNo, string FuelQty, string FuelRate, string Totalamt, string Remarks)
        {
            string message = "";
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);

            message = LP.CheckSLipNoForFuelAcknowledgement(Trailerid, SLipNo, FuelQty, FuelRate, Totalamt, Remarks, UserID);

            if (message == "")
            {
                message = LP.SaveFuelAcknowledgement(Trailerid, SLipNo, FuelQty, FuelRate, Totalamt, Remarks, UserID);

            }
            return Json(message);
        }

        [HttpPost]
        public ActionResult AjaxGetFuelAcknowledgement(string FromDate, string ToDate)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            DataTable dt = new DataTable();
            var sessionTime = Session.Timeout;
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetFuelAcknowledgement '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Userid + "'");
            Session["GetFuelAcknowledgementExportReport"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelFuelAcknowledgement()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["GetFuelAcknowledgementExportReport"];
            //DataTable dt = (DataTable)ViewData["VoucherDetails"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PumpAcknowledgementReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'> Pump Acknowledgement Summary<strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        //code for driver advance details 
        [HttpPost]
        public ActionResult GetDriverAdvanceFuelData(BE.FuelEntities fl)
        {

            BE.FuelEntities fe = new BE.FuelEntities();
            //  fe = LP.getFuelDataForDirect(fl.TransportorID, fl.Activity, fl.Type, fl.containerCount,fl.From,fl.To);
            fe = LP.getDirectAdvanceFuelDataForDirect(fl.driver);

            return Json(fe, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DrierctVoucherdetailsExportToExcel(string FromDate, string ToDate)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = db.sub_GetDatatable("USP_VoucherDetailsSummary '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            DataTable CompanyMaster = new DataTable();

            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            string Tittle = "From " + FromDate + " To " + ToDate + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=VoucherDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Voucher Details Summary Report <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        public ActionResult CheckContainerNotRepateForTwentyeightDays(List<BE.FuelEntitiesContainerList> FuelEntitiesContainerList, string status, string Activity,
               string FromDestination)
        {
            DataTable dataTable = new DataTable();
            string message = "";

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("JoNo");
            dataTable.TableName = "PT_ContainerListForFuel";

            //int Count = 1;
            foreach (BE.FuelEntitiesContainerList item in FuelEntitiesContainerList)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["Weight"] = item.Weight;
                row["JoNo"] = item.JoNo;
                dataTable.Rows.Add(row);
            }
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            BE.FuelEntities fe = new BE.FuelEntities();
            message = LP.CheckContaineralreadySaved(dataTable, status, Activity, FromDestination);

            return Json(message);

            // return Json(1);
        }


        [HttpPost]
        public ActionResult CheckFuelPrint(string VoucherNo, string AutoID)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("Check_fuelPrint '" + VoucherNo + "','" + AutoID + "'");

            if (tblInvoiceList.Rows.Count > 0)
            {
                InvoiceList = "0";
            }
            else
            {
                InvoiceList = "1";
            }

            return Json(InvoiceList);

        }

        [HttpPost]
        public ActionResult CheckVoucherPrint(string VoucherNo, string AutoID)
        {
            DataTable tblInvoiceList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            string InvoiceList = "";

            tblInvoiceList = db.sub_GetDatatable("Check_VoucherPrint '" + VoucherNo + "','" + AutoID + "'");

            if (tblInvoiceList.Rows.Count > 0)
            {
                InvoiceList = "0";
            }
            else
            {
                InvoiceList = "1";
            }

            return Json(InvoiceList);

        }

        public ActionResult BillChecking()
        {
            List<BE.GetProcesses> ProcessesMaster = new List<BE.GetProcesses>();
            ProcessesMaster = LP.GetProcesses();
            ViewBag.ProcessesName = new SelectList(ProcessesMaster, "ID", "Processes");

            List<BE.VendorList> VandorMaster = new List<BE.VendorList>();
            VandorMaster = LP.getVandorNameList();
            ViewBag.VandorIDName = new SelectList(VandorMaster, "VendorID", "VendorName");


            List<BE.BillVerificationTransport> VendorMaster = new List<BE.BillVerificationTransport>();
            VendorMaster = LP.VendorFill();
            ViewBag.VendorFill = new SelectList(VendorMaster, "transporterID", "vendorname");
            ViewBag.WorkYear = "2021-22";
            return View();
        }

        public JsonResult GetBillForSearch(string Search)
        {

            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_TSBill_Search '" + Search + "'");

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;




        }


        public JsonResult GetBillSearchs(string BillNo, string BillYear)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.DataGetBillChecking> receiptEntries = new List<BE.DataGetBillChecking>();
            dataTable = db.sub_GetDatatable("get_sp_BillmodifySummary_Web '" + BillNo + "','" + BillYear + "'");
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.DataGetBillChecking receiptEntry = new BE.DataGetBillChecking();
                    receiptEntry.select = Convert.ToString(row["select"]);
                    receiptEntry.cmbActivity = Convert.ToString(row["Activity"]);
                    receiptEntry.dtpbilldate = Convert.ToString(row["Bill Date"]);
                    receiptEntry.cmbTransporter = Convert.ToString(row["transporterID"]);
                    receiptEntry.dtpFromDate = Convert.ToString(row["periodfrom"]);
                    receiptEntry.dtpToDate = Convert.ToString(row["periodto"]);
                    receiptEntry.Process = Convert.ToString(row["Process"]);
                    receiptEntry.EntryIDJONo = Convert.ToString(row["EntryID/JONo"]);
                    receiptEntry.ContainerNo = Convert.ToString(row["Container No"]);
                    receiptEntry.Size = Convert.ToString(row["Size"]);
                    receiptEntry.InvoiceNo = Convert.ToString(row["Invoice No"]);
                    receiptEntry.Port = Convert.ToString(row["Port"]);
                    receiptEntry.TrailerNo = Convert.ToString(row["Trailer No"]);

                    receiptEntry.TranspoterName = Convert.ToString(row["Transporter Name"]);
                    receiptEntry.OutDateTime = Convert.ToString(row["Out Date & Time"]);
                    receiptEntry.TransportationAmt = Convert.ToDouble(row["TransportationAmt"]);

                    receiptEntries.Add(receiptEntry);
                }
            }



            var jsonResult = Json(receiptEntries, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        public ActionResult CANCELTRANSPORTBILL(BE.OtherInvoicesEntities AM)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            dataTable = db.sub_GetDatatable("USP_CANCEL_TRANSPORT_BILL '" + AM.Invoiceno + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "'");


            BE.ResponseMessage item = new BE.ResponseMessage();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    item.Status = Convert.ToInt32(row["Status"]);
                    item.Message = Convert.ToString(row["message"]);

                }
            }
            return Json(item);
        }



        public JsonResult BillCheckingSave(List<BE.DataGetBillChecking> Debitdata, string BillYear, string BillNo, string InvoiceNo, string BillDate, string Vendor, string Activity, string ContainerNo, string Amount)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string message = "";
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Process");
            dataTable.Columns.Add("EntryIDJONo");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("INDateTime");
            dataTable.Columns.Add("Port");
            dataTable.Columns.Add("TrailerNo");
            dataTable.Columns.Add("TranspoterName");
            dataTable.Columns.Add("TransportationAmt");

            foreach (BE.DataGetBillChecking item in Debitdata)
            {
                DataRow row = dataTable.NewRow();

                row["Process"] = item.Process;
                row["EntryIDJONo"] = item.EntryIDJONo;
                row["ContainerNo"] = item.ContainerNo;
                row["Size"] = item.Size;
                row["INDateTime"] = item.INDateTime;
                row["Port"] = item.Port;
                row["TrailerNo"] = item.TrailerNo;
                row["TranspoterName"] = item.TranspoterName;
                row["TransportationAmt"] = item.TransportationAmt;

                dataTable.Rows.Add(row);
            }

            message = LP.BillSave(dataTable, UserID, BillYear, BillNo, InvoiceNo, BillDate, Vendor, Activity, ContainerNo, Amount);

            return Json(message);
        }



        [HttpPost]
        public JsonResult AjaxGetTransportBillDetails(BE.DataGetBillChecking fileData)
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            string fromdate = fileData.fromdate; string todate = fileData.Todate; string Checkon = fileData.Checkon; string transporter = fileData.Activity;
            BE.GetTransportBillDetails VouchertraiffDetails = new BE.GetTransportBillDetails();
            List<BE.TransportBillMsg> GetListMsg = new List<BE.TransportBillMsg>();
            if (Request.Files.Count > 0)
            {
                try
                {

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;



                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/ImportFile/"), fname);
                        //  fname = Path.Combine(fname);
                        file.SaveAs(fname);
                        string extension = Path.GetExtension(fname);
                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                // conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                            case ".xlsx": //Excel 07 and above.
                                //conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                        }
                        //BL.AddErrorLog(conString.Replace("'",""));
                        //BL.AddErrorLog(fname.Replace("'", ""));
                        //  conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                        //  BL.AddErrorLog("Connection close");
                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            // BL.AddErrorLog("OleDbConnection");
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                // BL.AddErrorLog("cmdExcel");
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    //  BL.AddErrorLog("odaExcel");
                                    DataTable dt = new DataTable();
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    //  BL.AddErrorLog("Open connection");
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    //  BL.AddErrorLog(sheetName);
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();

                                    DataTable VOucherTraifftDT = new DataTable();


                                    VOucherTraifftDT.Columns.Add("Container No");
                                    VOucherTraifftDT.Columns.Add("Rate");
                                    VOucherTraifftDT.Columns.Add("Activity");
                                    VOucherTraifftDT.Columns.Add("Size");
                                    VOucherTraifftDT.Columns.Add("Type");
                                    VOucherTraifftDT.Columns.Add("Vehicle No");

                                    //DriverPaymentDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        if (row[0].ToString() != "" && row[1].ToString() != "" && row[2].ToString() != "" && row[3].ToString() != "" && row[4].ToString() != "" && row[5].ToString() != "")
                                        {


                                            DataRow dar = VOucherTraifftDT.NewRow();

                                            dar["Container No"] = row[0].ToString().ToUpper();
                                            dar["Rate"] = row[1].ToString().ToUpper();
                                            dar["Activity"] = row[2].ToString();
                                            dar["Size"] = row[3].ToString();
                                            dar["Type"] = row[4].ToString();
                                            dar["Vehicle No"] = row[5].ToString();

                                            VOucherTraifftDT.Rows.Add(dar);
                                            int linenum = dt.Rows.IndexOf(row);
                                            if (linenum == 1050)
                                            {
                                                int count1 = 0;
                                            }
                                        }
                                        else
                                        {
                                            return Json("Some columns are blank. Please check and re-import...");


                                        }
                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }

                                    if (VOucherTraifftDT != null)
                                    {

                                        VouchertraiffDetails = LP.GetTransportBillDetails(VOucherTraifftDT, Userid, fromdate, todate, Checkon, transporter);

                                        GetListMsg = VouchertraiffDetails.TransportBillMsg;



                                        DataTable dataTable = new DataTable();
                                        dataTable.Columns.Add("Container No");
                                        dataTable.Columns.Add("Status");


                                        foreach (BE.TransportBillMsg item in GetListMsg)
                                        {
                                            DataRow row = dataTable.NewRow();

                                            row["Container No"] = item.ValidateContainerno;
                                            row["Status"] = item.ValidateMSG;


                                            dataTable.Rows.Add(row);
                                        }

                                        Session["GetBillVerificationErros"] = dataTable;

                                        var returnField = new { Getmessage = VouchertraiffDetails.TransportBillMsg, TransportbillDetails = VouchertraiffDetails.DataGetBillChecking };
                                        var jsonResult = Json(returnField, JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;





                                    }


                                }
                            }
                        }
                    }
                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred. Error details: " + ex.Message);
                    BL.AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", ""));
                    return Json("Error occurred. Error details: " + ex.Message);

                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult CancelDetailsFortheBill(List<BE.DataGetBillChecking> SlipNoList)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("EntryIDJONo");
            dataTable.Columns.Add("BillNo");

            //int Count = 1;
            foreach (BE.DataGetBillChecking item in SlipNoList)
            {
                DataRow row = dataTable.NewRow();

                row["EntryIDJONo"] = item.EntryIDJONo;
                row["BillNo"] = item.BillNo;


                dataTable.Rows.Add(row);
            }
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            message = LP.CancelBillDetails(dataTable, Userid);

            return Json(message, JsonRequestBehavior.AllowGet);

            // return Json(1);
        }

        [HttpPost]
        public ActionResult GetTransportBillSearchDetails(string SearchCriteria, string SearcNO, string Fromdate, string Todate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_GetDetailsForContainer_TransportBill '" + SearchCriteria + "','" + SearcNO + "','" + Fromdate + "','" + Todate + "'");
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult GetShowFCLDestuffTallyList(String FromDate, string ToDate, string Transport, string ddlSearch, string Search)
        {
            if (Transport == "")
            {
                Transport = "0";
            }

            DataTable dtFCLDestuffTallyList = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            dtFCLDestuffTallyList = db.sub_GetDatatable("exec get_sp_BillSummary' " + FromDate + "','" + ToDate + "','" + Transport + "','" + ddlSearch + "','" + Search + "'");
            dtFCLDestuffTallyList.Columns.Remove("Cancel");
            Session["get_sp_BillSummary"] = dtFCLDestuffTallyList;

            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            string json = JsonConvert.SerializeObject(dtFCLDestuffTallyList);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportTransportErrorLogDetails()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable ErrorLogDetails = (DataTable)Session["GetBillVerificationErros"];
            GridView gridview = new GridView();
            gridview.DataSource = ErrorLogDetails;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Transport Error Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Transport Error Details<strong></td></tr>");

                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        public ActionResult ExportTransport()
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dtFCLDestuffTallyList = (DataTable)Session["get_sp_BillSummary"];
            GridView gridview = new GridView();
            gridview.DataSource = dtFCLDestuffTallyList;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Transport Summary.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>TransportSummary<strong></td></tr>");

                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        public ActionResult ReDateBillverification()
        {

            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = LP.Getcustomer();
            ViewBag.Customer = new SelectList(Customer, "AGID", "AGName");


            List<BE.BilltypeEntities> Billtypedetails = new List<BE.BilltypeEntities>();
            Billtypedetails = LP.GetBilltype();
            ViewBag.Billtype = new SelectList(Billtypedetails, "BillID", "BillName");

            return View();
        }
        [HttpPost]
        public JsonResult AjaxImportRedatebillverificaiton()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            string message1 = "";
            string messageCheck = "";
            BE.Rebatebillverificationentities Rebatedetails = new BE.Rebatebillverificationentities();
            List<BE.Getdetails> Details = new List<BE.Getdetails>();
            if (Request.Files.Count > 0)
            {
                try
                {

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/RedateImport/"), fname);
                        //  fname = Path.Combine(fname);
                        file.SaveAs(fname);
                        string extension = Path.GetExtension(fname);
                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                // conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                            case ".xlsx": //Excel 07 and above.
                                //conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                        }
                        //BL.AddErrorLog(conString.Replace("'",""));
                        //BL.AddErrorLog(fname.Replace("'", ""));
                        //  conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                        //  BL.AddErrorLog("Connection close");
                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            // BL.AddErrorLog("OleDbConnection");
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                // BL.AddErrorLog("cmdExcel");
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    //  BL.AddErrorLog("odaExcel");
                                    DataTable dt = new DataTable();
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    //  BL.AddErrorLog("Open connection");
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    //  BL.AddErrorLog(sheetName);
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();

                                    DataTable VOucherTraifftDT = new DataTable();


                                    VOucherTraifftDT.Columns.Add("Container No");
                                    VOucherTraifftDT.Columns.Add("Amount");



                                    //DriverPaymentDT.TableName = "PT_UpdateDischargeDate";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DateTime dDate;
                                        if (row[0].ToString() != "")
                                        {


                                            DataRow dar = VOucherTraifftDT.NewRow();

                                            dar["Container No"] = row[0].ToString().ToUpper();
                                            dar["Amount"] = row[1].ToString();

                                            VOucherTraifftDT.Rows.Add(dar);
                                            int linenum = dt.Rows.IndexOf(row);
                                            if (linenum == 1050)
                                            {
                                                int count1 = 0;
                                            }
                                        }
                                        else
                                        {
                                            message1 = "Some cloumns is missing Cannot proceed!";
                                            var returnField = new { data = message1 };
                                            var jsonResult = Json(returnField, JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                        }
                                    }

                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }

                                    if (VOucherTraifftDT != null)
                                    {

                                        Rebatedetails = LP.Getdatatabledetails(VOucherTraifftDT);

                                        Details = Rebatedetails.Getdetails;

                                        var returnField = new { tabledata = Details, ContainerError = Rebatedetails.message };
                                        var jsonResult = Json(returnField, JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;

                                    }

                                }
                            }
                        }
                    }
                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred. Error details: " + ex.Message);
                    BL.AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", ""));
                    return Json("Error occurred. Error details: " + ex.Message);
                    //return Json(1);
                    //  return Json("FormData is not supported.");
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult SaveRebatebillverirication(List<BE.Getdetails> SlipNoList, string BillYear, string BillNo, string BillType, string PartyName,
           string netpaid, string SGST, string CGST, string IGST, string Grandtotal, int isGST)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("JONo");
            dataTable.Columns.Add("JOType");
            dataTable.Columns.Add("Size");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("INDateandTime");
            dataTable.Columns.Add("LineName");
            dataTable.Columns.Add("CustomerNmae");
            dataTable.Columns.Add("IsDPD");
            dataTable.Columns.Add("IsUB");
            dataTable.Columns.Add("PaidAmount");
            dataTable.Columns.Add("Lineid");

            //int Count = 1;
            foreach (BE.Getdetails item in SlipNoList)
            {
                DataRow row = dataTable.NewRow();

                row["ContainerNo"] = item.ContainerNo;
                row["JONo"] = item.JONo;
                row["JOType"] = item.JOType;
                row["Size"] = item.Size;
                row["Type"] = item.Type;
                row["INDateandTime"] = item.INDateandTime;
                row["LineName"] = item.LineName;
                row["CustomerNmae"] = item.CustomerNmae;
                row["IsDPD"] = item.IsDPD;
                row["IsUB"] = item.IsUB;
                row["PaidAmount"] = item.PaidAmount;
                row["Lineid"] = item.Lineid;

                dataTable.Rows.Add(row);
            }
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            message = LP.CheckBillAlreadySaved(dataTable, BillYear, BillNo, BillType, PartyName, Userid);

            if (message == "")
            {
                message = LP.CheckBillNoissaved(dataTable, BillYear, BillNo, BillType, PartyName, Userid);

            }
            if (message == "")
            {
                message = LP.SaveBillrebatedetails(dataTable, BillYear, BillNo, BillType, PartyName, Userid, netpaid, SGST, CGST, IGST, Grandtotal, isGST);

            }


            var jsonResult = Json(message, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            // return Json(1);
        }

        public ActionResult CheckBlandContainerno(string Containerno,
           string BLNO)
        {
            BE.Rebatebillverificationentities Rebatedetails = new BE.Rebatebillverificationentities();
            List<BE.Getdetails> Details = new List<BE.Getdetails>();
            string message = "";
            message = LP.CheckBlNumber(Containerno, BLNO);

            return Json(message);
        }

        [HttpPost]
        public ActionResult AjaxGetRebatedetails(string FromDate, string ToDate, string searchcerteria, string Searchtext)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("get_SP_RebateBillContainerNoSummary '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "','" + searchcerteria + "','" + Searchtext + "'");
            Session["RevatebilldetailsList"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            Session["searchcerteria"] = searchcerteria;
            Session["Searchtext"] = Searchtext;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        [HttpPost]
        public JsonResult getAutoCustomerList(string prefixText, string CustomerType)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataTable dataTable = new DataTable();
            List<BE.Customer> Customer = new List<BE.Customer>();
            Customer = BL.getAutoCustomer(prefixText, CustomerType);

            return Json(Customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RebateBill_Search(string Searchtype)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_RebateBill_Search '" + Searchtype + "'");
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult CheckMaualdetails(string BillYear, string BillNo, string BillType, string Containerno,
      string BLNO, string paidamt, string PartyName)
        {
            BE.Rebatebillverificationentities Rebatedetails = new BE.Rebatebillverificationentities();
            List<BE.Getdetails> Details = new List<BE.Getdetails>();

            Rebatedetails = LP.GetMaunalDataDetails(BillYear, BillNo, BillType, Containerno, BLNO, paidamt, PartyName);
            Details = Rebatedetails.Getdetails;
            var returnField = new { tabledata = Details, ContainerError = Rebatedetails.message };
            var jsonResult = Json(returnField, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult DownloadIMGTemplateOnly()
        {

            string fileName = "~/RedateImport/RedateImportFile.xlsx";
            if (fileName != "")
            {
                string filePath = fileName;
                string Fname = "RedateImportFile.xlsx";

                Response.ContentType = "doc/docx";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Fname + "\"");

                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }
            return View();
        }

        //developed by prathamesh
        public ActionResult GetVendorGSTDetailsForbillVerification(string VendorID)
        {
            List<BE.GstDetails> expensesList = new List<BE.GstDetails>();
            expensesList = LP.GetVendorGSTDetails(VendorID);

            var jsonResult = Json(expensesList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult LabourBillVerification()
        {
            //List<BE.BilltypeEntities> Billtypedetails = new List<BE.BilltypeEntities>();
            //Billtypedetails = LP.GetBilltype();
            //ViewBag.Billtype = new SelectList(Billtypedetails, "BillID", "BillName");
            List<BE.VendorList> VandorMaster = new List<BE.VendorList>();
            VandorMaster = LP.getVandorNameList();
            ViewBag.VandorIDName = new SelectList(VandorMaster, "VendorID", "VendorName");
            return View();

        }
        public JsonResult AjaxLabourImportBillVerification(BE.LabourGetdetails fileData)
        {
            //return JsonToken(data);
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            //  string fromdate = fileData.fromdate; string todate = fileData.Todate;
            string message = "";
            string message1 = "";
            string messageCheck = "";
            BE.LabourBillVerification Labourdetails = new BE.LabourBillVerification();
            List<BE.LabourGetdetails> LabourDet = new List<BE.LabourGetdetails>();
            if (Request.Files.Count > 0)
            {
                try
                {

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        fname = Path.Combine(Server.MapPath("~/ImportFile/"), fname);
                        //  fname = Path.Combine(fname);
                        file.SaveAs(fname);
                        string extension = Path.GetExtension(fname);
                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                         // conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                            case ".xlsx": //Excel 07 and above.
                                          //conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                                break;
                        }
                        //BL.AddErrorLog(conString.Replace("'",""));
                        //BL.AddErrorLog(fname.Replace("'", ""));
                        //  conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", fname);
                        //  BL.AddErrorLog("Connection close");
                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            // BL.AddErrorLog("OleDbConnection");
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                // BL.AddErrorLog("cmdExcel");
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    //  BL.AddErrorLog("odaExcel");
                                    DataTable dt = new DataTable();
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    //  BL.AddErrorLog("Open connection");
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    //  BL.AddErrorLog(sheetName);
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();

                                    DataTable LabourDT = new DataTable();

                                    // table data structure

                                    LabourDT.Columns.Add("WO NO");
                                    LabourDT.Columns.Add("Container No");
                                    LabourDT.Columns.Add("SB Number");


                                    LabourDT.Columns.Add("Vehicle No");


                                    LabourDT.Columns.Add("Amount");
                                    LabourDT.TableName = "LoadingSheet";

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        DataRow dar = LabourDT.NewRow();



                                        dar["WO NO"] = row[0].ToString(); // (dt.Rows[1]["WO NO"]);
                                        dar["Container No"] = row[1].ToString(); // (dt.Rows[2]["Work year"]);
                                        dar["SB Number"] = row[2].ToString(); // (dt.Rows[4]["Wo Type"]);
                                        dar["Vehicle No"] = row[3].ToString();  //(dt.Rows[5]["Activity"]);
                                        dar["Amount"] = row[4].ToString(); //(dt.Rows[6]["SB Number"]);

                                        LabourDT.Rows.Add(dar);


                                        int linenum = dt.Rows.IndexOf(row);
                                        //    if (linenum == 1050)
                                        //    {
                                        //        int count1 = 0;
                                        //    }

                                        //    else
                                        //{
                                        //    return Json("Some columns are blank. Please check and re-import...");
                                        //}
                                    }
                                    if (fname != null || fname != string.Empty)
                                    {
                                        if ((System.IO.File.Exists(fname)))
                                        {
                                            System.IO.File.Delete(fname);
                                        }

                                    }

                                    if (LabourDT != null)
                                    {
                                        LabourDet = LP.GetLabourDataTableDet(LabourDT);
                                        // LabourDet = Labourdetails.LabourGetdetails;
                                        string json = JsonConvert.SerializeObject(LabourDet);
                                        var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;


                                    }

                                }
                            }
                        }
                    }
                    return Json("File imported Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred. Error details: " + ex.Message);
                    BL.AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", ""));
                    return Json("Error occurred. Error details: " + ex.Message);
                    //return Json(1);
                    //  return Json("FormData is not supported.");
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }


        public ActionResult SaveLabourbillverirication(List<BE.LabourGetdetails> LabourStore, string BillYear, string BillDate, string InvoiceNo, string BillNo, string BillType, string Vendor,
       string netpaid, string SGST, string CGST, string IGST, string Grandtotal, int isGST, string TQty, string TWeightKGS, string TWeightMTS, string Rate, string TAmount)

        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("WO_NO");
            dataTable.Columns.Add("Work_year");
            dataTable.Columns.Add("SBEntryID");
            dataTable.Columns.Add("Wo_Type");
            dataTable.Columns.Add("Activity");
            dataTable.Columns.Add("SBNO");
            dataTable.Columns.Add("VehicleNo");
            dataTable.Columns.Add("JONoEntryID");
            dataTable.Columns.Add("ContainerNo");
            dataTable.Columns.Add("PKGSDestuff");
            dataTable.Columns.Add("CargoWeight");
            dataTable.Columns.Add("cargoweightMTS");
            dataTable.Columns.Add("Amount");

            //int Count = 1;
            foreach (BE.LabourGetdetails item in LabourStore)
            {
                DataRow row = dataTable.NewRow();

                row["WO_NO"] = item.WO_NO;
                row["Work_year"] = item.work_year;
                row["SBEntryID"] = item.SBEntryID;
                row["Wo_Type"] = item.WO_Type;
                row["Activity"] = item.Activity;
                row["SBNO"] = item.SBNO;
                row["VehicleNo"] = item.VehicleNo;
                //row["CustomerNmae"] = item.CustomerNmae;
                row["JONoEntryID"] = item.JONoEntryID;
                row["ContainerNo"] = item.ContainerNo;
                row["PKGSDestuff"] = item.PKGSDestuff;
                row["CargoWeight"] = item.CargoWeight;
                row["cargoweightMTS"] = item.cargoweightMTS;
                row["Amount"] = item.Amount;
                dataTable.Rows.Add(row);
            }
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            string message = "";
            message = LP.CheckLabourBillAlreadySaved(dataTable, BillYear, BillNo, BillType, Vendor, Userid);

            if (message == "")
            {
                message = LP.CheckLabourBillNoissaved(dataTable, BillYear, BillNo, BillType, Vendor, Userid);

            }
            if (message == "")
            {
                message = LP.SaveLabourBillrebatedetails(dataTable, BillYear, BillNo, BillType, InvoiceNo, Vendor, Userid, netpaid, SGST, CGST, IGST, Grandtotal, isGST, TQty, TWeightKGS, TWeightMTS, Rate, TAmount);

            }


            var jsonResult = Json(message, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            // return Json(1);
        }




        public ActionResult AjaxGetLabour_SSRdetailsExport(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("get_Labour_SSR_BillContainerNoSummary '" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["RevatebilldetailsList"] = dt;
            Session["fromdate"] = FromDate;
            Session["todate"] = ToDate;
            // Session["searchcerteria"] = searchcerteria;
            //Session["Searchtext"] = Searchtext;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcelLabour_SSRBilldetailsExport()
        {
            DataTable dt = (DataTable)Session["RevatebilldetailsList"];
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + "";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Rebate Bill Veriifcations Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Rebate Bill Verification Details Report <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();

        }
        public ActionResult DownloadLabourTemplateOnly()
        {

            string fileName = "~/Format/LabourBillVerification.xlsx";
            if (fileName != "")
            {
                string filePath = fileName;
                string Fname = "LabourBillVerification.xlsx";

                Response.ContentType = "doc/docx";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Fname + "\"");

                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }
            return View();
        }
    }


}
