using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TrolleyTransport
{
    
    public  class TrolleyTransport
    {
        DB.TrackerMVCDBManager transportdatamanager = new DB.TrackerMVCDBManager();
        public EN.Trolleytransport getDropDownList()
        {
            EN.Trolleytransport objtrailerentities = new EN.Trolleytransport();
            DataSet DropDownList = new DataSet();
            DropDownList = transportdatamanager.TrolleyTransportDetails();

            List<EN.TransportTrolleyEntities> TransporterList = new List<EN.TransportTrolleyEntities>();
            List<EN.VehicleTypeIEntities> VehicleTypeList = new List<EN.VehicleTypeIEntities>();

            //List<EN.MenuInfo> MenuList = new List<EN.MenuInfo>();
            if (DropDownList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in DropDownList.Tables[0].Rows)
                {
                    EN.TransportTrolleyEntities Transporter = new EN.TransportTrolleyEntities();
                    Transporter.ID = Convert.ToInt32(row["ID"]);
                    Transporter.trailerType = Convert.ToString(row["trailerType"]);
                    TransporterList.Add(Transporter);
                }
            }
            if (DropDownList.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow row in DropDownList.Tables[1].Rows)
                {
                    EN.VehicleTypeIEntities VehicleType = new EN.VehicleTypeIEntities();
                    VehicleType.ContainerSizeID = Convert.ToInt32(row["ContainerSizeID"]);
                    VehicleType.ContainerSize = Convert.ToString(row["ContainerSize"]);
                    VehicleTypeList.Add(VehicleType);
                }
            }

            objtrailerentities.TransporterList = TransporterList;
            objtrailerentities.VehicleTypeList = VehicleTypeList;


            return objtrailerentities;
        }

        public List<EN.Trolleytransport> getTrolleyList()
        {
            DataTable dt = new DataTable();

            List<EN.Trolleytransport> Trolleytransportlist = new List<EN.Trolleytransport>();
            dt = transportdatamanager.getTrolleytransportlist();
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.Trolleytransport TrolleytransportDL = new EN.Trolleytransport();
                   // TrolleytransportDL.EntryDate = Convert.ToString(row["criteria"]);
                    TrolleytransportDL.TrolleyNumber = Convert.ToString(row["TrolleyNo"]);
                    TrolleytransportDL.InstalledTyres = Convert.ToString(row["InstalledTyres"]);
                   // TrolleytransportDL.TrolleyType = Convert.ToString(row["ID"]);
                    TrolleytransportDL.Weight = Convert.ToString(row["TrolleyWeight"]);
                    TrolleytransportDL.size = Convert.ToString(row["TrolleySize"]);
                    TrolleytransportDL.XLType = Convert.ToString(row["XLType"]);
                    TrolleytransportDL.TrailerType = Convert.ToString(row["TrailerType"]);
                    TrolleytransportDL.horstyres = Convert.ToString(row["Hores Tyres"]);
                    TrolleytransportDL.horseno = Convert.ToString(row["Horse No"]);
                    TrolleytransportDL.Status = Convert.ToString(row["Status"]);
                    TrolleytransportDL.GPSst = Convert.ToString(row["GPS Status"]);
                    TrolleytransportDL.AddedBy = Convert.ToString(row["Added By"]);

                    Trolleytransportlist.Add(TrolleytransportDL);
                }
            }
            return Trolleytransportlist;
        }
    }
}
