using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using System.Data;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.HorseSummary
{
   public class HorseSummary
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.HorseSummary> GetHorseSummary()
        {
            DataTable codeDL = new DataTable();
            codeDL = trackerdatamanager.GetHorseSummary();
            List<EN.HorseSummary> isCHACode = new List<EN.HorseSummary>();

            if (codeDL.Rows.Count != 0)
            {
                foreach (DataRow row in codeDL.Rows)
                {
                    EN.HorseSummary oper = new EN.HorseSummary();
                    oper.EntryID = Convert.ToString(row["EntryID"]);
                    oper.HorseNumber = Convert.ToString(row["HorseNumber"]);
                    oper.InstalledTyres = Convert.ToString(row["InstalledTyres"]);
                    oper.Weight = Convert.ToString(row["Weight"]);
                    oper.model = Convert.ToString(row["model"]);
                    isCHACode.Add(oper);
                }

            }
            return isCHACode;
        }

        //Codes By Prashant

        public string CheckHorseNumber(string HorseNumber)
        {
            string Message = "";
            DataTable CheckHorseDlL = new DataTable();
            CheckHorseDlL = trackerdatamanager.CheckHorseNumber(HorseNumber);

            if (CheckHorseDlL.Rows.Count > 0)
            {
                int HorseCount = Convert.ToInt16(CheckHorseDlL.Rows[0]["HorseNumber"]);
                if (HorseCount > 0)
                {
                    Message = "1";

                }
            }


            return Message;
        }
        //Codes End By Prashant
    }
}
