using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = TrackerMVCEntities.BusinessEntities;
using CU = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Login;
using HC = TrackerMVCDataLayer.Helper;
using TrackerMVC.Filters;
using System.Data;
using System.Data.Sql;
using monthEnum = TrackerMVCEntities.Enums;

namespace TrackerMVC.Controllers
{
    
    public class HomeController : Controller
    {
        
            CU.Login objTrackerProvider = new CU.Login();
        public ActionResult Index()
        {
            
            BE.LoginEntites logindata = new BE.LoginEntites();
            string User_name = string.Empty;
            string User_color = string.Empty;
            HttpCookie reqCookies = Request.Cookies["tracker_userCookies"];
            
            if (reqCookies != null)
            {
                logindata.UserName = reqCookies["Tracker_userName"].ToString();
                logindata.UserPass = reqCookies["Tracker_Password"].ToString();
                logindata.rememberme = true;
            }

            return View("index", logindata);
           // return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ApplicationStart()
        {
            string url = Request.Url.Host;
            ////ViewBag.Message = "Your contact page.";
            DataSet dataTableLocation = new DataSet();
            dataTableLocation = objTrackerProvider.DBLocation(url);

            ViewBag.HeaderLocation = dataTableLocation.Tables[0].AsEnumerable();
            ViewBag.HeaderSubLocation = dataTableLocation.Tables[1].AsEnumerable();
            Session["DBLocation"] = dataTableLocation.Tables[1];

            return View();
        }
        [HttpPost]
        public ActionResult login(BE.LoginEntites loginEntities)
        {
            
           // BE.LoginEntites loginEntities = new BE.LoginEntites();
            var name = loginEntities.UserName;
            var pass = loginEntities.UserPass;
            Boolean rememberme = Convert.ToBoolean(loginEntities.rememberme);


            BE.LoginEntites logindata = new BE.LoginEntites();

            logindata = objTrackerProvider.LogingetData(name, pass);
            if (logindata.UserID != 0)
            {
                //Start Validation to Authenticate User For Valid URL Access
                string Message = "";
                Message = ValidateURLUserLogin(logindata.UserID, Request.Url.Host);
                if(Message !="" && Message != "Success")
                {
                    logindata.LoginErrorMessage = Message;
                    return View("index", logindata);
                }

                //End Validation to Authenticate User For Valid URL Access

               

                Session["Tracker_userID"] = logindata.UserID;
                Session["Tracker_userName"] = logindata.UserName;
                Session["Tracker_DepType"] = logindata.DepType;
                Session["Tracker_ConCode"] = logindata.ConCode;

                List<BE.NotificationList> displayNotificaitonList = new List<BE.NotificationList>();
                displayNotificaitonList = objTrackerProvider.UserNotificationList(logindata.UserID.ToString());
                //ViewBag.Count = new SelectList(newList, "Count");
                //{
                //    new BE.UserList { Count = 3, Desc = "Someone likes our post" },
                //    new BE.UserList { Count = 3, Desc = "its very hard to display the notes" },
                //    new BE.UserList { Count = 3, Desc = "its testing  the notes" },
                //     new BE.UserList { Count = 3, Desc = "Someone likes our post" },
                //    new BE.UserList { Count = 3, Desc = "its very hard to display the notes" },
                //    new BE.UserList { Count = 3, Desc = "its testing  the notes" }
                //};

                Session["alertCount"] = displayNotificaitonList.Count();
                
                // Session["DisplayUserList"] = newList;
                Session["DisplayUserList"] = displayNotificaitonList;
                //List<BE.UserList> bst = (List<BE.UserList>)Session["DisplayUserList"];

                //HttpCookie tracker_userCookies = new HttpCookie("tracker_userCookies");
                //tracker_userCookies["Tracker_userID"] = Convert.ToString(logindata.UserID);
                //tracker_userCookies["Tracker_userName"] = logindata.UserName; ;
                //tracker_userCookies["Tracker_Password"] = logindata.DepType;
                //tracker_userCookies["Tracker_DepType"] = logindata.DepType;
                //Response.Cookies.Add(tracker_userCookies);

                RememberMe(name, pass, rememberme, Convert.ToString(logindata.UserID), logindata.DepType, logindata.ConCode);
              //  return RedirectToAction("Dashboard");
                if(logindata.UserType=="Admin" & logindata.UserID==141 || logindata.UserType == "User" & logindata.UserID == 128)
                {
                    return RedirectToAction("AdminDashboard", "Home");
                }
                else{
                    return RedirectToAction("Dashboard", "Home");
                }
               
            }
            else
            {
                logindata.LoginErrorMessage = "Wrong Username or Password.";
              //  return View("index");
                return View("index", logindata);
            }
        }

         private void RememberMe(String name, String password, Boolean rememberme, String UserId, String DepType, String ConCode)
         {

             if (rememberme)
             {
                HttpCookie tracker_userCookies = new HttpCookie("tracker_userCookies");
                tracker_userCookies["Tracker_userID"] = UserId;
                tracker_userCookies["Tracker_userName"] = name;
                tracker_userCookies["Tracker_Password"] = password;
                tracker_userCookies["Tracker_DepType"] = DepType;
                tracker_userCookies["Tracker_ConCode"] = ConCode;
                Response.Cookies.Add(tracker_userCookies);

                //HttpCookie tracker_userInfo = new HttpCookie("tracker_userInfo");
                //tracker_userInfo["Tracker_userName"] = name;
                //tracker_userInfo["Tracker_Password"] = password;
                tracker_userCookies.Expires = DateTime.Now.AddDays(30);
                //Response.Cookies.Add(tracker_userInfo);
            }
             else
             {
                 HttpCookie vms_userInfo = new HttpCookie("tracker_userCookies");
                 vms_userInfo.Expires = DateTime.Now.AddDays(-1);
                 Response.Cookies.Add(vms_userInfo);
                
             }
         }
        [UserAuthenticationFilter] 
        public ActionResult Dashboard()
        {
            return View();
        }

        [UserAuthenticationFilter]
        public ActionResult AdminDashboard()
        {
           
            return View();
        }
        public ActionResult AdminDashobards()
        {
            List<BE.CustArrivalEntities> ce = new List<BE.CustArrivalEntities>();
            ce = objTrackerProvider.GetCustomerArrival(DateTime.Now.Month);
            DataTable dt = new DataTable();
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month - 1, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            //  dt = objTrackerProvider.GetPipeLineArrival(DateTime.Now.Month);
            List<BE.CustArrivalEntities> ce1 = new List<BE.CustArrivalEntities>();
            ce1 = objTrackerProvider.GetPipeLineArrivalMonthWise(DateTime.Now.Month);
            ViewBag.MPArrival = ce;
            ViewBag.totalTEUSForImportArrivalDisplay = objTrackerProvider.totalTEUSForImportArrivalDisplay;
            ViewBag.donutValue = objTrackerProvider.donutValue;
            return View(dt);
        }





        public ActionResult CustPieChart()
        {
            List<BE.CustArrivalEntities> ce = new List<BE.CustArrivalEntities>();
            ce = objTrackerProvider.GetCustomerArrival(  DateTime.Now.Month);
            return Json(ce);
        }
        public ActionResult logout()
        {
            Session.Abandon();
            HttpCookie vms_userInfo = new HttpCookie("tracker_userInfo");
            vms_userInfo.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(vms_userInfo);
            Session.Abandon();
            return RedirectToAction("index", "Home");
        }

        [UserAuthenticationFilter] 
        public ActionResult SideMenu()
        {
            int Userid = Convert.ToInt32(Session["Tracker_userID"]);
            List<BE.SideMenuEntities> MenuList =new List<BE.SideMenuEntities>();
            MenuList = objTrackerProvider.getMenuList(Userid);
            //List<MenuItem> list = new List<MenuItem>();

            //list.Add(new MenuItem { Link = "/Test/Index", LinkName = "Home" });
            //list.Add(new MenuItem { Link = "/Test/Login", LinkName = "Login" });
            //list.Add(new MenuItem { Link = "/Test/Registration", LinkName = "Register" });

            Session["MenuList"] = MenuList;
            return PartialView("SideMenu", MenuList);
        }

        [UserAuthenticationFilter] 
        [HttpPost]
        public JsonResult GetMonthlyDataForPerson(string personName)
        {
            List<BE.GraphDetailsForPerson> nameList = new List<BE.GraphDetailsForPerson>();
              nameList = objTrackerProvider.GetMonthlyDataForPerson(personName);
             
            return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        [UserAuthenticationFilter]
        [HttpPost]
        public JsonResult GetCustomerArrivalForGivenMonth(string month)
        {
            List<BE.CustArrivalEntities> ce = new List<BE.CustArrivalEntities>();
           
            int monthNo = getMonthNO(month);

            ce = objTrackerProvider.GetCustomerArrival((monthNo + 1));
            if (ce.Count != 0)
            {
                ViewBag.newDonutValue = ce[ce.Count - 1].donutValue;
            }
            
            //ModelState.Clear();
            return Json(ce, JsonRequestBehavior.AllowGet);
        }
        [UserAuthenticationFilter] 
        [HttpPost]
        public JsonResult GetMonthlyTEUSForImportArrival()
        {
            List<BE.LineGraphValue> lineGraph = new List<BE.LineGraphValue>();
            lineGraph = objTrackerProvider.GetMonthlyTEUSForImportArrival();

            return Json(lineGraph, JsonRequestBehavior.AllowGet);
        }
        

        private int getMonthNO(string month){
            if (month == "Jan")
            {
                return (int)monthEnum.MonthEnum.Jan;
            }
            if (month == "Feb")
            {
                return (int)monthEnum.MonthEnum.Feb;
            }
            if (month == "Mar")
            {
                return (int)monthEnum.MonthEnum.Mar;
            }
            if (month == "Apr")
            {
                return (int)monthEnum.MonthEnum.Apr;
            }
            if (month == "May")
            {
                return (int)monthEnum.MonthEnum.May;
            }
            if (month == "June")
            {
                return (int)monthEnum.MonthEnum.June;
            }
            if (month == "July")
            {
                return (int)monthEnum.MonthEnum.July;
            }
            if (month == "Aug")
            {
                return (int)monthEnum.MonthEnum.Aug;
            }
            if (month == "Sep")
            {
                return (int)monthEnum.MonthEnum.Sep;
            }
            if (month == "Oct")
            {
                return (int)monthEnum.MonthEnum.Oct;
            }
            if (month == "Nov")
            {
                return (int)monthEnum.MonthEnum.Nov;
            }
            if (month == "Dec")
            {
                return (int)monthEnum.MonthEnum.Dec;
            }
            return 0;
        }
        
        public string ValidateURLUserLogin(long UserId,string URL)
        {
            string Message = "";
            HC.DBOperations db = new HC.DBOperations();
            DataTable dt = new DataTable();
            try
            {
                dt = db.sub_GetDatatable("ValidateURLByUserLogin '" + UserId + "','" + URL + "'");

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Message = dt.Rows[0]["Message"].ToString();
                    }
                }
            }
            catch(Exception ex) { Message = "Success"; }

            return Message;
        }

        //developed by prathamesh
        public ActionResult getMenuList(string SearchText)
        {

            try
            {
                List<BE.SubmenuInfo> MenuList = new List<BE.SubmenuInfo>();

                int UserID = Convert.ToInt32(Session["Tracker_userID"]);
                MenuList = objTrackerProvider.getMenuList(SearchText, UserID);
                var jsonResult = Json(MenuList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public ActionResult SetFavouriteMenu(int MenuID)
        {
            int UserID = Convert.ToInt32(Session["Tracker_userID"]);

            string message = objTrackerProvider.SetFavouriteMenu(MenuID, UserID);
            return Json(message);
        }

        //Domestic Dashboard Inventory
        [UserAuthenticationFilter]
        public ActionResult DomesticInvDashboard()
        {
            //DateTime FromDate = DateTime.Now;
            //DateTime ToDate = DateTime.Now;
            //FromDate = FromDate.AddDays(-7);
            List<BE.ContainerStockM> stockDetails = new List<BE.ContainerStockM>();
            stockDetails = objTrackerProvider.GetStockDetails();
            ViewBag.ContainerStockList = stockDetails;
            return View();
        }
        public ActionResult GetContainerStockList(int DeptID, string Title)
        {
            List<BE.ContainerStockM> stockList = new List<BE.ContainerStockM>();
            stockList = objTrackerProvider.GetContainerStockList(DeptID, Title);
            var JsonResult = Json(stockList, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

    }

    
}
