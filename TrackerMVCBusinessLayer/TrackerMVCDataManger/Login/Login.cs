using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
namespace TrackerMVCBusinessLayer.TrackerMVCDataManger.Login
{
    public class Login
    {
        DB.TrackerMVCDBManager Trackerdatamanager = new DB.TrackerMVCDBManager();
        public EN.LoginEntites LogingetData(string name, string pass)
        {

            DataTable loginData = new DataTable();

            //DB.GetMyREQDBManager getmyreqdatamanager = new DB.GetMyREQDBManager();
            loginData = Trackerdatamanager.getLogin(name, pass);

            EN.LoginEntites objloginentities = new EN.LoginEntites();


            if (loginData.Rows.Count > 0)
            {

                foreach (DataRow row in loginData.Rows)
                {

                    objloginentities.UserID = Convert.ToInt32(row["UserID"]);
                    objloginentities.UserName = Convert.ToString(row["UserName"]);
                    objloginentities.DepType = Convert.ToString(row["DepType"]);
                }
            }
            return objloginentities;
        }
    }
}
