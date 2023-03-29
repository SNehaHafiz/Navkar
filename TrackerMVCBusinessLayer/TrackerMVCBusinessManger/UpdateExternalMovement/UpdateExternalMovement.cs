using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.UpdateExternalMovement
{
  public class UpdateExternalMovement
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public List<EN.UpdateExternalMovement> GetFromDropdownList()
        {

            List<EN.UpdateExternalMovement> fromList = new List<EN.UpdateExternalMovement>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetLocationDetails();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.UpdateExternalMovement List = new EN.UpdateExternalMovement();

                    List.ID = Convert.ToString(row["LocationID"]);
                    List.Criteria = Convert.ToString(row["Location"]);
                    fromList.Add(List);
                }
            }
            return fromList;
        }

        public List<EN.UpdateExternalMovement> GetSizeDropdownList()
        {

            List<EN.UpdateExternalMovement> SizeList = new List<EN.UpdateExternalMovement>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetSizeDetails();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.UpdateExternalMovement List = new EN.UpdateExternalMovement();

                    List.ID = Convert.ToString(row["ContainerSizeID"]);
                    List.Criteria = Convert.ToString(row["ContainerSize"]);
                    SizeList.Add(List);
                }
            }
            return SizeList;
        }

        public List<EN.UpdateExternalMovement> GettypeDropdownList()
        {

            List<EN.UpdateExternalMovement> TypeList = new List<EN.UpdateExternalMovement>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetTypeDetails();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.UpdateExternalMovement List = new EN.UpdateExternalMovement();

                    List.ID = Convert.ToString(row["ContainerTypeID"]);
                    List.Criteria = Convert.ToString(row["ContainerType"]);
                    TypeList.Add(List);
                }
            }
            return TypeList;
        }

        public List<EN.UpdateExternalMovement> GetVesselDropdownList()
        {

            List<EN.UpdateExternalMovement> vesselList = new List<EN.UpdateExternalMovement>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetVesselDetails();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.UpdateExternalMovement List = new EN.UpdateExternalMovement();

                    List.ID = Convert.ToString(row["VesselID"]);
                    List.Criteria = Convert.ToString(row["VesselName"]);
                    vesselList.Add(List);
                }
            }
            return vesselList;
        }

        public List<EN.UpdateExternalMovement> GetshiplineDropdownList()
        {

            List<EN.UpdateExternalMovement> SLlList = new List<EN.UpdateExternalMovement>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetshiplineDetails();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.UpdateExternalMovement List = new EN.UpdateExternalMovement();

                    List.ID = Convert.ToString(row["SLID"]);
                    List.Criteria = Convert.ToString(row["SLName"]);
                    SLlList.Add(List);
                }
            }
            return SLlList;
        }

        public List<EN.UpdateExternalMovement> GetPortsDropdownList()
        {

            List<EN.UpdateExternalMovement> PortsList = new List<EN.UpdateExternalMovement>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetPortsDetails();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.UpdateExternalMovement List = new EN.UpdateExternalMovement();

                    List.ID = Convert.ToString(row["PortID"]);
                    List.Criteria = Convert.ToString(row["PortName"]);
                    PortsList.Add(List);
                }
            }
            return PortsList;
        }

        public List<EN.UpdateExternalMovement> GetTransportDropdownList()
        {

            List<EN.UpdateExternalMovement> TransportList = new List<EN.UpdateExternalMovement>();
            DataTable DL = new DataTable();
            DL = TrackerManager.GetTransportDetails();

            if (DL != null)
            {
                foreach (DataRow row in DL.Rows)
                {
                    EN.UpdateExternalMovement List = new EN.UpdateExternalMovement();

                    List.ID = Convert.ToString(row["TransID"]);
                    List.Criteria = Convert.ToString(row["TransName"]);
                    TransportList.Add(List);
                }
            }
            return TransportList;
        }


        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public EN.UpdateContainerMovementEntities GetAccidentSearchDetails(string ContainerNo, int Jono)
        {
            EN.UpdateContainerMovementEntities ImportSearchList = new EN.UpdateContainerMovementEntities();
            DataTable ImportDL = new DataTable();
            ImportDL = trackerdatamanager.GetActivitySearch(ContainerNo, Jono);

            if (ImportDL.Rows.Count != 0)
            {
                foreach (DataRow row in ImportDL.Rows)
                {
                    EN.UpdateContainerMovementEntities ImportSearchListdl = new EN.UpdateContainerMovementEntities();
                    ImportSearchList.fromid = Convert.ToString(row["LFrom"]);
                    ImportSearchList.toid = Convert.ToString(row["LTo"]);
                    ImportSearchList.Size = Convert.ToString(row["Size"]);
                    ImportSearchList.Type = Convert.ToString(row["ContainerTypeID"]);
                    ImportSearchList.ShipmentNo = Convert.ToString(row["ShipmentNo"]);

                    //ImportSearchList.Add(ImportSearchListdl);
                }
            }

            return ImportSearchList;
        }
        //Code by rahul
        public EN.ExternalMovementReport GetDDList()
        
        {

            EN.ExternalMovementReport DDList = new EN.ExternalMovementReport();

            List<EN.LineListExt> LineListExt = new List<EN.LineListExt>();
            List<EN.TransporterListExt> TransporterListExt = new List<EN.TransporterListExt>();
            DataSet DS = new DataSet();
            DS = TrackerManager.getDDListForExternalReport();

            if (DS.Tables[0] != null)
            {
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    EN.LineListExt List = new EN.LineListExt();

                    List.LineID = Convert.ToInt16(row["SLID"]);
                    List.LineName = Convert.ToString(row["SLName"]);
                    LineListExt.Add(List);
                }
            }
            if (DS.Tables[1] != null)
            {
                foreach (DataRow row in DS.Tables[1].Rows)
                {
                    EN.TransporterListExt List = new EN.TransporterListExt();

                    List.TransID = Convert.ToInt16(row["TransID"]);
                    List.TransName = Convert.ToString(row["TransName"]);
                    TransporterListExt.Add(List);
                }
            }
            DDList.LineListExt = LineListExt;
            DDList.TransporterListExt = TransporterListExt;

            return DDList;
        }
        public List<EN.ExternalMovementReport> getExternalReport(string FromDate, string ToDate, int SearchCriteria, string SearchText)
        {
            DataTable dt = new DataTable();
            List<EN.ExternalMovementReport> ExternalList = new List<EN.ExternalMovementReport>();
            dt = trackerdatamanager.GetExternalMovementRegister(FromDate, ToDate, SearchCriteria, SearchText);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ExternalMovementReport External = new EN.ExternalMovementReport();
                    External.SrNo = Convert.ToInt16(row["Sr No"]);
                    External.Category = Convert.ToString(row["Category"]);
                    External.ContainerNo = Convert.ToString(row["Container No"]);
                    External.Size = Convert.ToString(row["Size"]);
                    External.Type = Convert.ToString(row["Type"]);
                    External.OutDate = Convert.ToString(row["Out Date"]);
                    External.Location = Convert.ToString(row["Location"]);
                    External.To = Convert.ToString(row["To"]);
                    External.Vehicle = Convert.ToString(row["Vehicle"]);
                    External.Line = Convert.ToString(row["Line"]);
                    External.Transporter = Convert.ToString(row["Transporter"]);
                    External.VesselName = Convert.ToString(row["Vessel Name"]);
                    External.NavkarBillNo = Convert.ToString(row["Navkar Bill No"]);
                    External.NavkarBillAmount = Convert.ToString(row["Navkar Bill Amount"]);
                    External.BillNumbers = Convert.ToString(row["Bill Numbers"]);
                    External.TransporterAmt = Convert.ToString(row["Transporter Amt"]);
                    External.GateInType = Convert.ToString(row["Gate In Type"]);
                    
                    ExternalList.Add(External);
                }
            }
            return ExternalList;
        }
        public List<EN.SaveUpdateMovementEntities> GetContainerList()
        {
            List<EN.SaveUpdateMovementEntities> ContainerList = new List<EN.SaveUpdateMovementEntities>();
            DataTable DT = new DataTable();
            DT = TrackerManager.getContainerListDataExpternalSearch();
            try
            {
                if (DT != null)
                {
                    foreach (DataRow row in DT.Rows)
                    {
                        EN.SaveUpdateMovementEntities EXT = new EN.SaveUpdateMovementEntities();

                        EXT.jono = Convert.ToString(row["JONo"]);
                        EXT.ContainerNo = Convert.ToString(row["ContainerNo"]);
                        EXT.from = Convert.ToString(row["From"]);
                        EXT.to = Convert.ToString(row["To"]);
                        EXT.fromid = Convert.ToString(row["LFrom"]);
                        EXT.toid = Convert.ToString(row["LTo"]);
                        EXT.Vehicle = Convert.ToString(row["vehicleNo"]);
                        EXT.Size = Convert.ToString(row["Size"]);
                        EXT.Type = Convert.ToString(row["ContainerTypeID"]);
                        EXT.Reamrks = Convert.ToString(row["Remarks"]);
                        EXT.MovementDate = Convert.ToDateTime(row["outDate"]).ToString("yyyy-MM-ddTHH:mm");
                        EXT.ShipmentNo = Convert.ToString(row["ShipmentNo"]);
                        ContainerList.Add(EXT);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return ContainerList;
        }
        // code end by rahul

        DB.TrackerMVCDBManager TrackerManager1 = new DB.TrackerMVCDBManager();
        public EN.DischargeDateContainerDetails GetDropDownListWorkOrder()
        {
            EN.DischargeDateContainerDetails External = new EN.DischargeDateContainerDetails();
            DataSet ExternalDl = new DataSet();

            ExternalDl = TrackerManager1.getDropDownListExternal();

            List<EN.portEntities> portList = new List<EN.portEntities>();
            List<EN.lineEntites> lineList = new List<EN.lineEntites>();
            List<EN.VesselEntities> VesseslList = new List<EN.VesselEntities>();
            List<EN.TypeEntities> TypeList = new List<EN.TypeEntities>();
            List<EN.FromEntities> FromList = new List<EN.FromEntities>();
            List<EN.ToEntities> ToList = new List<EN.ToEntities>();
            List<EN.SizeEntities> SizeList = new List<EN.SizeEntities>();


            if (ExternalDl.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[0].Rows)
                {
                    EN.portEntities List = new EN.portEntities();
                    List.portID = Convert.ToInt32(row["PortID"]);
                    List.ports = Convert.ToString(row["PortName"]);
                    portList.Add(List);
                }

            }
            if (ExternalDl.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[1].Rows)
                {
                    EN.lineEntites linlist = new EN.lineEntites();
                    linlist.lineid = Convert.ToInt32(row["SLID"]);
                    linlist.LineName = Convert.ToString(row["SLName"]);
                    lineList.Add(linlist);
                }
            }
            if (ExternalDl.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[2].Rows)
                {
                    EN.VesselEntities vessel = new EN.VesselEntities();
                    vessel.vesselid = Convert.ToInt32(row["VesselID"]);
                    vessel.vesselName = Convert.ToString(row["VesselName"]);
                    VesseslList.Add(vessel);
                }

            }
            if (ExternalDl.Tables[3].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[3].Rows)
                {
                    EN.TypeEntities Type = new EN.TypeEntities();
                    Type.TypeID = Convert.ToInt32(row["ContainerTypeID"]);
                    Type.Type = Convert.ToString(row["ContainerType"]);
                    TypeList.Add(Type);
                }

            }
            if (ExternalDl.Tables[4].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[4].Rows)
                {
                    EN.FromEntities from = new EN.FromEntities();
                    from.fromid = Convert.ToInt32(row["LocationID"]);
                    from.From = Convert.ToString(row["location"]);
                    FromList.Add(from);
                }

            }

            if (ExternalDl.Tables[5].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[5].Rows)
                {
                    EN.ToEntities To = new EN.ToEntities();
                    To.Toid = Convert.ToInt32(row["LocationID"]);
                    To.To = Convert.ToString(row["location"]);
                    ToList.Add(To);
                }

            }

            if (ExternalDl.Tables[6].Rows.Count != 0)
            {
                foreach (DataRow row in ExternalDl.Tables[6].Rows)
                {
                    EN.SizeEntities size = new EN.SizeEntities();
                    size.Sizeid = Convert.ToInt32(row["ContainerSizeID"]);
                    size.Sizec = Convert.ToString(row["ContainerSize"]);
                    SizeList.Add(size);
                }

            }

            External.portList = portList;
            External.lineList = lineList;
            External.VesseslList = VesseslList;
            External.TypeList = TypeList;
            External.FromList = FromList;
            External.ToList = ToList;
            External.SizeList = SizeList;

            return External;
        }

        public List<EN.UpdateExternalMovement> containerstatus(string ContainerNo)
        {
            List<EN.UpdateExternalMovement> passingDL = new List<EN.UpdateExternalMovement>();
            DataTable dt = new DataTable();
            dt = TrackerManager.updateStatus(ContainerNo);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.UpdateExternalMovement PassingList = new EN.UpdateExternalMovement();
                    PassingList.Criteria = Convert.ToString(row["Criteria"]);
                    PassingList.Size = Convert.ToString(row["size"]);
                    PassingList.Type = Convert.ToString(row["ContainerType"]);
                    PassingList.vehicleno = Convert.ToString(row["vehicleno"]);


                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.UpdateExternalMovement> UpdateDriverPaymentDetails(DataTable DriverPaymentDT, long userId)
        {
            string Message = "";
            //EN.DriverPaymentReco objdriverpaymententities = new EN.DriverPaymentReco();
            List<EN.UpdateExternalMovement> DriverPaymentList = new List<EN.UpdateExternalMovement>();
            if (DriverPaymentDT != null)
            {
                DataTable ImportDL = new DataTable();
                Message = trackerdatamanager.AddLoloData(DriverPaymentDT, userId);
                
                foreach (DataRow row in DriverPaymentDT.Rows)
                {
                    EN.UpdateExternalMovement DPDTList = new EN.UpdateExternalMovement();
                    
                    DPDTList.containerno = Convert.ToString(row["CONTAINERNO"]);
                    DPDTList.Size = Convert.ToString(row["SIZE"]);
                    DPDTList.ContainerType = Convert.ToString(row["CONTAINERTYPE"]);
                    DPDTList.ActivityDate = Convert.ToString(row["ACTIVITYDATE"]);
                    DPDTList.Activity = Convert.ToString(row["ACTIVITY"]);
                    
                    DriverPaymentList.Add(DPDTList);
                }
            }
            return DriverPaymentList;
        }

    }
}
