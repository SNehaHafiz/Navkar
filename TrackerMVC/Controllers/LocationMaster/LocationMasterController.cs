using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.LocationMaster;
using CI = TrackerMVCEntities.BusinessEntities;
using CD = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using HC = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Web.UI;

namespace TrackerMVC.Controllers.LocationMaster
{
    [UserAuthenticationFilter]
    public class LocationMasterController : Controller
    {
        DP.LocationMaster reportprovider = new DP.LocationMaster();
        // GET: LocationMaster
        public ActionResult LocationMaster()
        {
          
            List<CI.LocationEntities> LocationList = new List<CI.LocationEntities>();
            LocationList = reportprovider.GetExisitingLocationame();
            ViewBag.LocationList = new SelectList(LocationList, "ID", "Criteria");

            List<CI.LocationEntities> LocationListOther = new List<CI.LocationEntities>();
            LocationListOther = reportprovider.GetExisitingLocationameOther();
            ViewBag.LocationListOther = new SelectList(LocationListOther, "IDOtherLocation", "CriteriaOther");

            List<CI.DistanceEntities> DistanceList = new List<CI.DistanceEntities>();
            DistanceList = reportprovider.GetExisitingDistanceame();
            ViewBag.DistanceList = new SelectList(DistanceList, "DistanceGroupID", "DistanceGroup");

          
            return View();

           
        }
        public ActionResult Save(CI.LocationM LocationMaster)
        {
            ///var EntryDate = LocationMaster.EntryDate;

            CD.DBOperations db = new CD.DBOperations();
            db.sub_ExecuteNonQuery("Insert_Loctaion_M '" + LocationMaster.Location + "','" + LocationMaster.LocationGroupID + "','" + LocationMaster.DistanceGroupID + "','" + LocationMaster.OnWayKM + "','" + LocationMaster.TwoWayKM + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + LocationMaster.ID + "','" + LocationMaster.IDOtherLocation + "','" + LocationMaster.ISActive + "'");
            //Master();
            return Json(LocationMaster);
        }

        [HttpPost]
        public ActionResult InsertLocation(string LocationName)
        {
            string message = "";
            message = reportprovider.Insertlocation(LocationName);

            return Json(message);
        }
        [HttpPost]
        public ActionResult InsertDirection(string DistanceName)
        {
            string message = "";
            message = reportprovider.Insertlocation(DistanceName);

            return Json(message);
        }


        [HttpPost]
        public ActionResult AjaxGetLocationDetails()
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_LocationMaster");
            Session["LocationMaster"] = dt;
           
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult ExportToExcel()
        {
            DataTable dt = (DataTable)Session["LocationMaster"];
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
            Response.AddHeader("content-disposition", "attachment;filename=VoucherDetails.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Location Summary Report <strong></td></tr>");
                 //   htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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
        [HttpPost]
        public ActionResult GetLocationDetails(string LocationID)
        {
            List<CI.LocationEditEntiites> LocationDetails = new List<CI.LocationEditEntiites>();
            LocationDetails = reportprovider.GetLocationDetails(LocationID);
            var JsonResult = Json(LocationDetails, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }
        public JsonResult MenuRights(string MenuID )
        {

            int i = 0;
            string Messageget = "";
            DataTable dt = new DataTable();
            string strSql = "";
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();

      
            strSql = "";
            strSql = "Usp_Menu_rights'" + MenuID +  "'";
            dt = db.sub_GetDatatable(strSql);
            if (dt.Rows.Count > 0)
            {
                Messageget =   Convert.ToString(dt.Rows[0][0].ToString());
            }
            
            return Json(Messageget);

        }
    }
    
}