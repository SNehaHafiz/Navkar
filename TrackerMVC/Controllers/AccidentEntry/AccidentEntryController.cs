using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using CI = TrackerMVCEntities.BusinessEntities;
using CD = TrackerMVCDataLayer.Helper;
using DB = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace TrackerMVC.Controllers.AccidentEntry
{
    [UserAuthenticationFilter]
    public class AccidentEntryController : Controller
    {
        public JsonResult Save(CI.AccidentEntryEntities AccidentEntryEntities)
        {
            int i = 0;
            //var EffectiveFromDate = TPTariffDetailsEntities.EffectiveFromDate;
            //var EffectivetoDate = TPTariffDetailsEntities.EffectivetoDate;
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_INSERT_VEHICLE_ACCIDENT_DETS '" + Convert.ToDateTime(AccidentEntryEntities.EntryDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(AccidentEntryEntities.AccidentDate).ToString("yyyy-MM-dd HH:mm") + "','" + AccidentEntryEntities.trailerno + "','" + AccidentEntryEntities.AccidentPlace + "','" + AccidentEntryEntities.DriverName + "','" + AccidentEntryEntities.Remarks +"','"+ Convert.ToInt32(Session["Tracker_userID"]) + "','" + Convert.ToDateTime(AccidentEntryEntities.EntryDate).ToString("yyyy-MM-dd HH:mm") + "'");
            //Master();

            return Json(i);
        }
        // GET: AccidentEntry
        public ActionResult AccidentEntry()
        {
            return View();
        }
        

        public ActionResult GetDataBind(string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_AccidentDetsforSearch_Report '" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["USP_AccidentDetsforSearch_Report"] = dt;
            Session["fromdate"] = fromDate;
            Session["todate"] = ToDate;
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExcelImpHC()
        {
            DataTable dt = (DataTable)Session["USP_AccidentDetsforSearch_Report"];
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();

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
            Response.AddHeader("content-disposition", "attachment;filename=VehicleAccidentReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Accident Summary Report Report <strong></td></tr>");
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




        public ActionResult GetDataBindDetails()
        {
            DataTable dt = new DataTable();
            DataTable CompanyMaster = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("usp_BondSaleReg_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            CD.DBOperations db = new CD.DBOperations();
            dt = db.sub_GetDatatable("USP_AccidentDetsforSearch_Report_list_Five");
            Session["LastFiveRecordForAccident"] = dt;
           
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        public ActionResult ExportToExcelacidentdetails()
        {
            DataTable dt = (DataTable)Session["LastFiveRecordForAccident"];
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();

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
            Response.AddHeader("content-disposition", "attachment;filename=VehicleAccidentReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Accident Summary Report Report <strong></td></tr>");
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
    }
}