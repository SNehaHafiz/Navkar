using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ModifyInternalFuelConsumption
{
  public  class ModifyInternalFuelConsumption
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.ModifyInternalFuelConsumptionEntities> GetFuelConsumptionDetails(string SlipNo)
        {
            List<EN.ModifyInternalFuelConsumptionEntities> SlipDetailsDL = new List<EN.ModifyInternalFuelConsumptionEntities>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetFuelConsumptionDetails(SlipNo);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.ModifyInternalFuelConsumptionEntities slipNo = new EN.ModifyInternalFuelConsumptionEntities();

                    slipNo.TrailerNo = Convert.ToString(row["trailername"]);
                    slipNo.fuelRefNo = Convert.ToString(row["SlipNo"]);
                    slipNo.Qty = Convert.ToString(row["balQty"]);

                    slipNo.currentReading = Convert.ToString(row["currentReading"]);
                    slipNo.lastReading = Convert.ToString(row["lastReading"]);
                    SlipDetailsDL.Add(slipNo);
                }
            }



            return SlipDetailsDL;
        }

        public string CancelFuelConsumptiondetails(string SlipNo, string CancelRemarks, int Userid)
        {
            int i = 0;
            i = trackerdatamanager.CancelFuelConsumptiondetails(SlipNo, CancelRemarks, Userid);
            string message;
            if (i > 0)
            {
                message = "Record Cancelled Successfully.";
            }
            else
            {
                message = "Record not Cancelled, Please try again!";
            }
            return message;
        }

        public string ReprintFuelConsumptiondetails(string SlipNo)
        {
            int i = 0;
            i = trackerdatamanager.ReprintFuelConsumptiondetails(SlipNo);
            string message;
            if (i > 0)
            {
                message = "Record saved Successfully.";
            }
            else
            {
                message = "Record not saved, Please try again!";
            }
            return message;
        }
    }
}
