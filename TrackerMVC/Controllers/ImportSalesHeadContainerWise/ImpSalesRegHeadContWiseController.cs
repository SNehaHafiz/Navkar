using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using HC = TrackerMVCDataLayer.Helper;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using BE = TrackerMVCEntities.BusinessEntities;
using RP = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Report;


namespace TrackerMVC.Controllers.ImportSalesHeadContainerWise
{
    [UserAuthenticationFilter]
    public class ImpSalesRegHeadContWiseController : Controller
    {
        // GET: ImpSalesRegHeadContWise
        BM.BLDataManager.GenerateBL BL = new BM.BLDataManager.GenerateBL();
        RP.Report reportprovider = new RP.Report();
        public ActionResult ImportSalesHeadContainerWise()
        {
            return View();
        }
        public ActionResult GetDataBind(string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("USP_SalesRegister_Container_HeadWise_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_SalesRegister_Container_HeadWise_Report '" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            Session["ImportSalesHeadContainerDets"] = dt;
            string json = JsonConvert.SerializeObject(dt);            
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        public ActionResult ExportToExcelImpHC()
        {
            DataTable dt = (DataTable)Session["ImportSalesHeadContainerDets"];

            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ImportSalesHeadContainerWise.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
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
        public ActionResult ImportSalesTeusWise()
        {
            List<BE.ActivityMaster> ActivityMaster = new List<BE.ActivityMaster>();
            ActivityMaster = reportprovider.getActivityMasterCreditNote();
            ViewBag.Activity = new SelectList(ActivityMaster, "TYPEID", "Activity");

            List<BE.CHA> CHA = new List<BE.CHA>();
            CHA = BL.getCHA();
            ViewBag.CHA = new SelectList(CHA, "CHANo", "CHAName");
            return View();
        }


        public ActionResult GetTEUSDataBind(string fromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("SP_Final_Sales '" + Convert.ToDateTime(fromDate).ToString("yyyyMMddHHmmss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyyMMddHHmmss") + "','','ALL','','','',''");
            Session["fromdate"] = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy HH:mm");
            Session["todate"] = Convert.ToDateTime(ToDate).ToString("dd MMM yyyy HH:mm");
            Session["ImportSalesTEUSDets"] = dt;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        //public ActionResult GetTEUSDataBind(string arraivaldate, string fromDate, string ToDate,string Category, string ChaName, string Name, string IGMNo,string ItemNo)
        //{
        //    DataTable dt = new DataTable();
        //    HC.DBOperations db = new HC.DBOperations();
        //    dt = db.sub_GetDatatable("SP_Final_Sales '','" + Convert.ToDateTime(fromDate).ToString("yyyyMMddHHmmss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyyMMddHHmmss") + "','"+ Category + "','"+ ChaName + "','"+ Name + "','"+ IGMNo + "',''"+ ItemNo + "");
        //    Session["fromdate"] = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy HH:mm");
        //    Session["todate"] = Convert.ToDateTime(ToDate).ToString("dd MMM yyyy HH:mm");
        //    Session["ImportSalesTEUSDets"] = dt;

        //    string json = JsonConvert.SerializeObject(dt);
        //    var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;

        //    return jsonResult;

        //}


        public ActionResult ExportToExcelImpTEUS()
        {
            HC.DBOperations db = new HC.DBOperations();

            DataTable CompanyMaster = new DataTable();
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable dt = (DataTable)Session["ImportSalesTEUSDets"];
            string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";

            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();
            gridview.HeaderRow.Style.Add("background-color", "LightBlue");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ImportSalesTEUSWise.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='" + dt.Columns.Count + "'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='" + dt.Columns.Count + "'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='" + dt.Columns.Count + "'><strong style='font-size: 15px'> Fuel Slip Details Report<strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='" + dt.Columns.Count + "'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='" + dt.Columns.Count + "'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }


        public ActionResult ExportSalesHeadnContainerWise()
        {
            return View();
        }
        public ActionResult GetExportDataBind(string fromDate, string ToDate,string PartyID)
        {
            DataTable dt = new DataTable();
            //SqlConnection con = new SqlConnection(strval);
            //SqlCommand cmd = new SqlCommand("USP_SalesRegister_Container_HeadWise_Report", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);

            HC.DBOperations db = new HC.DBOperations();
            dt = db.sub_GetDatatable("USP_EXPORT_CTR_HEAD_WISE_CHARGES '" + Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + PartyID + "'");

            Session["ExportSalesHeadContainerDets"] = dt;

            string json = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        public ActionResult ExportToExcelExpHC()
        {
            DataTable dt = (DataTable)Session["ExportSalesHeadContainerDets"];

            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExportSalesHeadContainerWise.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
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