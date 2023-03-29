using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB = TrackerMVCDataLayer;
using EN = TrackerMVCEntities.BusinessEntities;
using System.Data;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.PendingFactoryReturn
{
    public class PendingFactoryReturn
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();

        public List<EN.PendingFactoryReturn> getPendingFactoryReturn()
        {
            List<EN.PendingFactoryReturn> PendingFactoryReturnList = new List<EN.PendingFactoryReturn>();
            DataTable PendingFactoryReturnDT = new DataTable();
            PendingFactoryReturnDT = TrackerManager.GetPendingFactoryReturn();
            if (PendingFactoryReturnDT != null)
            {
                double dblSrNo = 0;
                foreach (DataRow row in PendingFactoryReturnDT.Rows)
                {
                    EN.PendingFactoryReturn PendingFactoryReturnDTList = new EN.PendingFactoryReturn();
                    dblSrNo += 1;
                    PendingFactoryReturnDTList.SrNo = Convert.ToString(dblSrNo);
                    PendingFactoryReturnDTList.ContainerNo = Convert.ToString(row["Container No"]);
                    PendingFactoryReturnDTList.Size = Convert.ToString(row["Size"]);
                    PendingFactoryReturnDTList.Type = Convert.ToString(row["Type"]);
                    PendingFactoryReturnDTList.DOValidDate = Convert.ToString(row["DO Valid Date"]);
                    PendingFactoryReturnDTList.ReceivedDate = Convert.ToString(row["Received Date"]);
                    PendingFactoryReturnDTList.ShippingLine = Convert.ToString(row["Shipping Line"]);
                    PendingFactoryReturnDTList.IGMNo = Convert.ToString(row["IGM No"]);
                    PendingFactoryReturnDTList.ItemNo = Convert.ToString(row["Item No"]);
                    PendingFactoryReturnDTList.CHA = Convert.ToString(row["CHA"]);
                    PendingFactoryReturnDTList.Importer = Convert.ToString(row["Importer"]);
                    PendingFactoryReturnDTList.BOENo = Convert.ToString(row["BOE No"]);
                    PendingFactoryReturnDTList.DeliveryType = Convert.ToString(row["Delivery Type"]);
                    PendingFactoryReturnDTList.FinalDestination = Convert.ToString(row["Final Destination"]);
                    PendingFactoryReturnDTList.EWayBillNo = Convert.ToString(row["E-Way Bill No"]);
                    PendingFactoryReturnDTList.VehicleNo = Convert.ToString(row["Vehicle No"]);
                    PendingFactoryReturnDTList.OutDate = Convert.ToString(row["Out Date"]);
                    PendingFactoryReturnDTList.DwellHours = Convert.ToString(row["Dwell Hours"]);
                    PendingFactoryReturnDTList.DwellDays = Convert.ToString(row["Dwell Days"]);
                    
                    
                    
                    
                    PendingFactoryReturnDTList.Remarks = Convert.ToString(row["remarks"]);

                    PendingFactoryReturnList.Add(PendingFactoryReturnDTList);
                }
            }
            return PendingFactoryReturnList;
        }
    }
}
