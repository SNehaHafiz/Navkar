using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB = TrackerMVCDataLayer;
using EN = TrackerMVCEntities.BusinessEntities;
using System.Data;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ImportFinalOut
{
    public class ImportFinalOut
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public EN.ImportFinalOutEntities getFillDropdownList()
        {
            EN.ImportFinalOutEntities objimportfinalentities = new EN.ImportFinalOutEntities();
            DataSet ImportDS = new DataSet();
            DataTable PortDT = new DataTable();
            DataTable LineDT = new DataTable();
            List<EN.PortEntities> PortList = new List<EN.PortEntities>();
            List<EN.LineEntities> LineList = new List<EN.LineEntities>();

            ImportDS = TrackerManager.ImportFinalOutFillDropdown();
            PortDT = ImportDS.Tables[0];
            LineDT = ImportDS.Tables[1];
            if (PortDT != null)
            {
                foreach (DataRow row in PortDT.Rows)
                {
                    EN.PortEntities PortDTList = new EN.PortEntities();
                    PortDTList.PortID = Convert.ToInt16(row["PortID"]);
                    PortDTList.PortName = Convert.ToString(row["PortName"]);
                    PortList.Add(PortDTList);
                }
            }
            if (LineDT != null)
            {
                foreach (DataRow row in LineDT.Rows)
                {
                    EN.LineEntities LineDTList = new EN.LineEntities();
                    LineDTList.LineID = Convert.ToInt16(row["SLID"]);
                    LineDTList.LineName = Convert.ToString(row["SLName"]);
                    LineList.Add(LineDTList);
                }
            }
            objimportfinalentities.PortList = PortList;
            objimportfinalentities.LineList = LineList;
            return objimportfinalentities;
        }
        public List<EN.ImportFinalOutEntities> getImportFinalOut(DateTime FromDate, DateTime ToDate, string DeliveryType, string For, string PortID, string LineID)
        {
            List<EN.ImportFinalOutEntities> ImportFinalOutList = new List<EN.ImportFinalOutEntities>();
            DataTable ImportFinalOutDT = new DataTable();
            if (PortID =="")
            {
                PortID = "0";
            }
            if (LineID == "")
            {
                LineID = "0";
            }
            ImportFinalOutDT = TrackerManager.ImportFinalOut(FromDate,ToDate,DeliveryType,For,PortID,LineID);
            if (ImportFinalOutDT != null)
            {
                double dblSrNo = 0;
                foreach (DataRow row in ImportFinalOutDT.Rows)
                {
                    EN.ImportFinalOutEntities ImportFinalDTList = new EN.ImportFinalOutEntities();
                    dblSrNo += 1;
                    ImportFinalDTList.SrNo = Convert.ToString(dblSrNo);
                    ImportFinalDTList.GPNo = Convert.ToString(row["GP No"]);
                    ImportFinalDTList.GPDate = Convert.ToString(row["GP Date"]);
                    ImportFinalDTList.DeliveryType = Convert.ToString(row["Delivery Type"]);
                    ImportFinalDTList.ContainerNo = Convert.ToString(row["Container No"]);
                    ImportFinalDTList.Size = Convert.ToString(row["Size"]);
                    ImportFinalDTList.Type = Convert.ToString(row["Type"]);
                    ImportFinalDTList.IGMNo = Convert.ToString(row["IGM No"]);
                    ImportFinalDTList.ItemNo = Convert.ToString(row["Item No"]);
                    ImportFinalDTList.BLNo = Convert.ToString(row["BL No"]);
                    ImportFinalDTList.InDate = Convert.ToString(row["In Date"]);
                    ImportFinalDTList.OutDate = Convert.ToString(row["Out Date"]);
                    ImportFinalDTList.DwellDays = Convert.ToString(row["Dwell Days"]);
                    ImportFinalDTList.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    ImportFinalDTList.PKGS = Convert.ToString(row["PKGS"]);
                    ImportFinalDTList.PKGSType = Convert.ToString(row["PKGS Type"]);
                    ImportFinalDTList.TareWeight = Convert.ToString(row["Tare Weight"]);
                    ImportFinalDTList.CargoWeight = Convert.ToString(row["Cargo Weight"]);
                    ImportFinalDTList.GrossWeight = Convert.ToString(row["Gross Weight"]);
                    ImportFinalDTList.Commodity = Convert.ToString(row["Commodity"]);
                    ImportFinalDTList.HaulagetType = Convert.ToString(row["Haulaget Type"]);
                    ImportFinalDTList.Line = Convert.ToString(row["Line"]);
                    ImportFinalDTList.Customer = Convert.ToString(row["Customer"]);
                    ImportFinalDTList.Importer = Convert.ToString(row["Importer"]);
                    ImportFinalDTList.CHAName = Convert.ToString(row["CHA Name"]);
                    ImportFinalDTList.BerthingDateTime = Convert.ToString(row["Berthing Date & Time"]);
                    ImportFinalDTList.JoDateTime = Convert.ToString(row["Jo Date & Time"]);
                    ImportFinalDTList.PortName = Convert.ToString(row["Port Name"]);
                    ImportFinalDTList.GoodsDesc = Convert.ToString(row["Goods Desc"]);
                    ImportFinalDTList.BOENo = Convert.ToString(row["BOE No"]);
                    ImportFinalDTList.BOEDate = Convert.ToString(row["BOE Date"]);
                    ImportFinalDTList.OOCNo = Convert.ToString(row["OOC No"]);
                    ImportFinalDTList.OOCDate = Convert.ToString(row["OOC Date"]);
                    ImportFinalDTList.Value = Convert.ToString(row["Value"]);
                    ImportFinalDTList.Duty = Convert.ToString(row["Duty"]);
                    ImportFinalDTList.DoValidDate = Convert.ToString(row["Do Valid Date"]);
                    ImportFinalDTList.PreparedBy = Convert.ToString(row["Prepared By"]);
                    ImportFinalDTList.CargoType = Convert.ToString(row["Cargo Type"]);

                    ImportFinalOutList.Add(ImportFinalDTList);
                }
            }
            return ImportFinalOutList;
        }
    }
}
