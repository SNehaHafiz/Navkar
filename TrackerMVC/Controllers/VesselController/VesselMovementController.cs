using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using vessel = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.VesselMovement;
using TrackerMVC.Filters;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using HC = TrackerMVCDataLayer.Helper;

namespace TrackerMVC.Controllers.VesselController
{
   [UserAuthenticationFilter] 
    public class VesselMovementController : Controller
    {
        vessel.VesselMovement vesselTrackerProvider = new vessel.VesselMovement();
        // GET: VesselMovement
        public ActionResult NewVesselMovementDetails()
        {
            List<BE.VesselMaster> vesselList = new List<BE.VesselMaster>();
            vesselList = vesselTrackerProvider.GetVesselListForVesselMovement();
            ViewBag.VesselList = new SelectList(vesselList, "vesselID", "vesselName");
            List<BE.Ports> portList = new List<BE.Ports>();
            //portList = vesselTrackerProvider.GetPortList();
            portList = vesselTrackerProvider.getPorts();
           // ViewBag.portList = portList;
            ViewBag.portList = new SelectList(portList, "PortID", "PortName");
            if (Convert.ToString(TempData["Message"]) != null)
            {
                //string msg = TempData["url"].ToString();
                string msg1 = Convert.ToString(TempData["Message"]);
                ViewBag.Message = msg1;
            }
            return View();
        }
        public JsonResult GetExisitingViaNO(string igmno)
        {
            BE.VesselMovement VesselData = new BE.VesselMovement();
            VesselData = vesselTrackerProvider.GetExisitingViaNO(igmno);
            return Json(VesselData);
            //bool isCodeExisiting = false;
            //if (viaNO != "")
            //{
            //    isCodeExisiting = vesselTrackerProvider.GetExisitingViaNO(viaNO);
            //}
            //return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSealCuttViaNO(string ViaNo)
        {
            string Message = "";
            Message = vesselTrackerProvider.CheckVianoforSealcut(ViaNo);
            return Json(Message);
            //bool isCodeExisiting = false;
            //if (viaNO != "")
            //{
            //    isCodeExisiting = vesselTrackerProvider.GetExisitingViaNO(viaNO);
            //}
            //return Json(isCodeExisiting, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveVesselMovementDetails(BE.VesselMovement vesselDetails)
        {
            int saved = 0;
            string message = "";
            if (ModelState.IsValid &&  vesselDetails != null && vesselDetails.VesselID != 0 &&  vesselDetails.ViaNO != "" && vesselDetails.ViaNO != null)
            {
               // bool isViaNo;

               // isViaNo = vesselTrackerProvider.GetExisitingViaNO(vesselDetails.ViaNO);
                //if (isViaNo)
                //{
                    //vesselDetails.MovementID = vesselTrackerProvider.GetNewVesselMovementID();
                    vesselDetails.AddedBy = Convert.ToInt32(Session["Tracker_userID"]);                   
                    saved = vesselTrackerProvider.AddVesselMovementDetails(vesselDetails);
               // }

                //return Json(saved, JsonRequestBehavior.AllowGet);

            }
            //List<BE.VesselMaster> vesselList = new List<BE.VesselMaster>();
            //vesselList = vesselTrackerProvider.GetVesselListForVesselMovement();
            //ViewBag.VesselList = new SelectList(vesselList, "vesselID", "vesselName");
            //List<BE.Port> portList = new List<BE.Port>();
            //portList = vesselTrackerProvider.GetPortList();
          //  ViewBag.portList = portList;
          //  ViewBag.portList = new SelectList(portList, "PortID", "PortName");
            if (saved == 0)
            {
                message = "Data provided incomplete. Try again!";
                TempData["Message"] = message;
                return RedirectToAction("NewVesselMovementDetails");
            }
            else if (saved > 0)
            {
                message = "Record saved successfully";
                //ModelState.Clear();
                TempData["Message"] = message;
                return RedirectToAction("NewVesselMovementDetails");
            }

           // ViewBag.Message = message;
            
        return View("NewVesselMovementDetails");
        }
        [HttpPost]
        public JsonResult GetContainerForAutoComplete(string viaNO)
        {

            List<string> nameList = new List<string>();
            if (viaNO != "")
            {
                nameList = vesselTrackerProvider.GetViaNOForAutoComplete(viaNO);
            }
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getVesselSummaryDet()
        {

            DataTable getLineWiseSummary = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            getLineWiseSummary = db.sub_GetDatatable("GET_SPVesselBerthing");
            Session["getVesselSummary"] = getLineWiseSummary;
            var json = JsonConvert.SerializeObject(getLineWiseSummary);
            var jsonResult = Json(json, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ExportToExcelVesselMovement(string fromdate, string todate)
        {
            DataTable CompanyMaster = new DataTable();
            HC.DBOperations db = new HC.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable getMovementICDNew = (DataTable)Session["getVesselSummary"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = getMovementICDNew;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=VesselMovementReport.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))

                {

                    // render the GridView to the HtmlTextWriter
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Vessel Movement Report<strong></td></tr>");
                    //htw.Write("<table><tr><td  style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
                    htw.Write("<table><tr><td colspan='7'><h6 style='text-align:left'> *output generated by tracker </h6></td></tr>");
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