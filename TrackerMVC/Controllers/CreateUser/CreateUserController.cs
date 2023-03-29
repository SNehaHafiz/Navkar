using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.CreateUser;
using CD = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace TrackerMVC.Controllers.CreateUser
{
    //[UserAuthenticationFilter]
    public class CreateUserController : Controller
    {
        // GET: CreateUser
        BM.CreateUser trainTrackerProvider = new BM.CreateUser();
        public ActionResult CreateUser()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<BE.CreateUser> Location = new List<BE.CreateUser>();
            Location = trainTrackerProvider.GetFrommenuList();
            ViewBag.Location = new SelectList(Location, "GroupID", "Name");


            List<BE.CreateUser> icon = new List<BE.CreateUser>();
            icon = trainTrackerProvider.GetIconList();
            ViewBag.icon = new SelectList(icon, "icon", "icon");

            List<BE.CreateUser> User = new List<BE.CreateUser>();
            User = trainTrackerProvider.UserName();
            ViewBag.User = new SelectList(User, "UserID", "USerName");


            return View();
        }

        public JsonResult Save(BE.CreateUser CreateUser)
        { 
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            int retVal = 0;
            retVal = db.sub_ExecuteNonQuery("USP_Create_User_insert '" + CreateUser.firstname + "','" + CreateUser.lastname + "','" + CreateUser.gender + "','" + Convert.ToDateTime(CreateUser.DOB).ToString("yyyy-MM-dd HH:mm") + "','" + CreateUser.employeeid + "','" + CreateUser.password + "','" + CreateUser.department + "','" + CreateUser.userType + "','" + CreateUser.emailid + "','" + CreateUser.passEmail + "','" + CreateUser.Contact + "','" + UserID + "','"  +CreateUser.UseriDN+ "'");
            //Master();

            return Json(retVal);
        }

        public JsonResult AddMenu(BE.CreateUser CreateUser)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            string Message = "";
            try
            {
                CD.DBOperations db = new CD.DBOperations();
                DataTable table = new DataTable();
                table = db.sub_GetDatatable("usp_insert_Into_Menudetails '" + CreateUser.MenuDesc + "','" + CreateUser.MenuDept + "','" + CreateUser.fromName + "','" + CreateUser.menuName + "','" + CreateUser.Controller + "','" + CreateUser.Action + "','" + CreateUser.Menu + "'");

                if (table != null)
                {
                    Message = table.Rows[0]["ErrMessage"].ToString();
                }
            }
            catch(Exception ex)
            {
                //Message = ex.Message;
            }


            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AjaxCheckFirstname(string firstname)
        {
            string Message = "";
            Message = trainTrackerProvider.CheckFirstName(firstname);

            return Json(Message);
        }



        public JsonResult CreateSearch(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("SearchEmp '" + search + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult BindMenuDetails(string UserName,string Menusfrom)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("GetMenuUserDetails '" + UserName + "','" + Menusfrom + "'");
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetLocationDetails(string UserID)
        {
            List<BE.CreateUser> LocationDetails = new List<BE.CreateUser>();
            LocationDetails = trainTrackerProvider.UserDetails(UserID);
            var JsonResult = Json(LocationDetails, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        [HttpPost]
        public JsonResult SaveAssingRight(string UserId,List<BE.UserRights>  RightsData)
        {
            string message = "";
            try
            {
                DataTable UserRightDT = new DataTable();
                UserRightDT.Columns.Add("MenuId");
                UserRightDT.Columns.Add("IsAccess");
                UserRightDT.Columns.Add("CanAdd");
                UserRightDT.Columns.Add("CanView");
                UserRightDT.Columns.Add("CanUpdate");
                UserRightDT.Columns.Add("CanMail");
                UserRightDT.Columns.Add("CanCancel");

                if (RightsData.Count > 0)
                {
                    foreach(BE.UserRights rights in RightsData)
                    {
                        DataRow row = UserRightDT.NewRow();
                        row["MenuId"] = rights.MenuId;
                        row["IsAccess"] = rights.IsAccess;
                        row["CanAdd"] = rights.CanAdd;
                        row["CanView"] = rights.CanView;
                        row["CanUpdate"] = rights.CanUpdate;
                        row["CanMail"] = rights.CanMail;
                        row["CanCancel"] = rights.CanCancel;

                        UserRightDT.Rows.Add(row);
                    }
                }

                if(UserRightDT != null)
                {
                    message = trainTrackerProvider.SaveUserRight(UserRightDT, Convert.ToInt32(UserId));
                }

            }catch(Exception ex) { }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FuelRateSetting()
        {
            ViewBag.Date = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");

            List<BE.CreateUser> FuelV = new List<BE.CreateUser>();
            FuelV = trainTrackerProvider.FuelRateSetting();
            ViewBag.VendorList = new SelectList(FuelV, "FuelVID", "FuelVName");
            //Fuel Master
            List<BE.CreateUser> FuelList = new List<BE.CreateUser>();
            FuelList = trainTrackerProvider.FuelType();
            ViewBag.FuelList = new SelectList(FuelList, "FuelTID", "FuelType");
            return View();


        }

        public JsonResult SaveFuel(BE.CreateUser CreateUser)
        {
            int UserID = Convert.ToInt16(Session["Tracker_userID"]);
            CD.DBOperations db = new CD.DBOperations();
            int retVal = 0;
            retVal = db.sub_ExecuteNonQuery("USP_INSERT_FUEL_RATE_MASTER '" + Convert.ToDateTime(CreateUser.fuelDate).ToString("yyyy-MM-dd HH:mm") + "','" + CreateUser.FuelVID + "','" + CreateUser.FuelTID + "','" + CreateUser.fuelrate + "','" + CreateUser.isactive + "','" + UserID + "'");
            //Master();

            return Json(retVal);
        }

        public JsonResult FuelSearch(string search)
        {
            CD.DBOperations db = new CD.DBOperations();
            DataTable dt = new DataTable();
            dt = db.sub_GetDatatable("USP_FuelSummary_Rate_list '" + search + "'");
            Session["FuelRateDetails"] = dt;
            var summaryDet = JsonConvert.SerializeObject(dt);
            var jsonResult = Json(summaryDet, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult CheckvendorName(string vendorID)
        {
            string Message = "";
            Message = trainTrackerProvider.CheckvendorName(vendorID);

            return Json(Message);
        }


        public ActionResult ExportToExcelFuelrateSetting()
        {
            DataTable dt = (DataTable)Session["FuelRateDetails"];
            DataTable CompanyMaster = new DataTable();
            CD.DBOperations db = new CD.DBOperations();
            // dt = db.sub_GetDatatable("USP_GetContainerSurveyRemarks '" + containerNo + "'");
            CompanyMaster = db.sub_GetDatatable("USP_COMPANYDETAILS");
            var CompanyName = Convert.ToString(CompanyMaster.Rows[0]["con_Name"]);
            var CompanyAddress = Convert.ToString(CompanyMaster.Rows[0]["AddressI"]);
            DataTable FuelStockSummary = (DataTable)Session["FuelStockSummary"];
            //string Tittle = "From " + Session["fromdate"] + " To " + Session["todate"] + ".";
            GridView gridview = new GridView();
            gridview.DataSource = dt;
            gridview.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Fuel rate Setting Details.xls");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 26px'>" + CompanyName + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + CompanyAddress + " <strong></td></tr>");
                    htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>Fuel Rate Setting Report <strong></td></tr>");
                    // htw.Write("<table><tr><td style='font-weight: bold; text-align: center'; colspan ='7'><strong style='font-size: 15px'>" + Tittle + " <strong></td></tr>");
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