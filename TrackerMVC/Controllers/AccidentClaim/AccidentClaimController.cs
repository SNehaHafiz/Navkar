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
using DB = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.AccidentClaim;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace TrackerMVC.Controllers.AccidentClaim
{
    [UserAuthenticationFilter]
    public class AccidentClaimController : Controller
    {
        public JsonResult Save(CI.AccidentClaimSaveEntities AccidentClaimSaveEntities)
        {
            int i = 0;
            //var EffectiveFromDate = TPTariffDetailsEntities.EffectiveFromDate;
            //var EffectivetoDate = TPTariffDetailsEntities.EffectivetoDate;
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_Update_Accident_Claim '" + Convert.ToDateTime(AccidentClaimSaveEntities.ClaimDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + AccidentClaimSaveEntities.Claim_Settle_Date + "','" + Convert.ToInt32(Session["Tracker_userID"]) + "','" + AccidentClaimSaveEntities.Claim_Amount + "','" + AccidentClaimSaveEntities.Claim_Remarks + "','" + AccidentClaimSaveEntities.IsSettled + "','" + AccidentClaimSaveEntities.EntryID + "'");
            //Master();

            return Json(i);
        }

        // GET: AccidentClaim
        DB.AccidentClaim trainTrackerProvider = new DB.AccidentClaim();
        public ActionResult AccidentClaim()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ajaxSearchlistdetails(string EntryID)
        {

            List<CI.AccidentClaimEntities> ImportSearchdetails = new List<CI.AccidentClaimEntities>();
            ImportSearchdetails = trainTrackerProvider.GetAccidentSearchDetails(EntryID);


            var jsonResult = Json(ImportSearchdetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult AccidentClaimSummary()
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
            dt = db.sub_GetDatatable("USP_ClaimReport '" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'");
            Session["USP_ClaimReport"] = dt;
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
            DataTable dt = (DataTable)Session["USP_ClaimReport"];
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
            Response.AddHeader("content-disposition", "attachment;filename=VehicleClaimReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Accident claim Summary Report <strong></td></tr>");
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