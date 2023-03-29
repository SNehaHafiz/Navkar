using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using System.Data.Sql;
using System.Web.Mvc.Filters;

namespace TrackerMVC.Filters
{
    public class UserAuthenticationFilter: ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //Check Session is Empty Then set as Result is HttpUnauthorizedResult 
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["Tracker_userID"])))
            {
                //filterContext.Result = new HttpUnauthorizedResult();
                // filterContext.Result = new HttpUnauthorizedResult();
                HttpCookie reqCookies = filterContext.HttpContext.Request.Cookies["tracker_userCookies"];
                if (reqCookies == null)
                {

                    filterContext.Result = new RedirectResult("~/Home/index");
                }
                else
                {
                    filterContext.HttpContext.Session["Tracker_userID"] = reqCookies["Tracker_userID"].ToString();
                    filterContext.HttpContext.Session["Tracker_userName"] = reqCookies["Tracker_userName"].ToString();
                    filterContext.HttpContext.Session["Tracker_Password"] = reqCookies["Tracker_Password"].ToString();
                    filterContext.HttpContext.Session["Tracker_DepType"] = reqCookies["Tracker_DepType"].ToString();
                    filterContext.HttpContext.Session["Tracker_ConCode"] = reqCookies["Tracker_ConCode"].ToString();

                }
            }

        }


        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //We are checking Result is null or Result is HttpUnauthorizedResult 
            // if yes then we are Redirect to Error View
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Home/index");
                
            }
        }


    }
}