using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;


namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ModifyExternalFuelSlip
{
  public  class ModifyExternalFuelSlip
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.ModifyExternalFuelSlipEntities> GetSlipEditDetails(string SlipNo)
        {
            List<EN.ModifyExternalFuelSlipEntities> SlipDetailsDL = new List<EN.ModifyExternalFuelSlipEntities>();
            DataTable dt = new DataTable();

            dt = trackerdatamanager.GetFuelSlipDetails(SlipNo);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    EN.ModifyExternalFuelSlipEntities slipNo = new EN.ModifyExternalFuelSlipEntities();

                    slipNo.TrailerNo = Convert.ToString(row["vehicleNo"]);
                    slipNo.fuelRefNo = Convert.ToString(row["fuelRefNo"]);
                    slipNo.Qty = Convert.ToString(row["Qty"]);
                    slipNo.Remarks = Convert.ToString(row["Remark"]);


                    SlipDetailsDL.Add(slipNo);
                }
            }



            return SlipDetailsDL;
        }

        public string CancelFueldetails(string SlipNo, string CancelRemarks, int Userid)
        {
            int i = 0;
            i = trackerdatamanager.CancelFueldetails(SlipNo, CancelRemarks, Userid);
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

        public string ReprintFuelSlipdetails(string SlipNo)
        {
            int i = 0;
            i = trackerdatamanager.ReprintFuelSlipdetails(SlipNo);
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
