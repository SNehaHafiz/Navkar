using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using IM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.DomesticInventoryManagement;//   DomesticInventoryManagement;
using BO = TrackerMVCEntities.BusinessEntities;
using BE = TrackerMVCEntities.BusinessEntities;
using System.Web.UI.WebControls;
using System.Data;

//namespace TrackerMVC.Controllers.DomesticInventoryManagement
//{
//    public class DomesticInventoryManagementController : Controller
//    {
//        // GET: DomesticInventoryManagement
//        public ActionResult Index()
//        {
//            return View();
//        }
//    }


//}

namespace TrackerMVC.Controllers.DomesticInventoryManagement
{
    [UserAuthenticationFilter]
    public class DomesticInventoryManagementController : Controller
    {
        IM. InventoryManagementDataProvider IMBL = new IM.InventoryManagementDataProvider();
        // GET: InventoryManagement
        public ActionResult ContainerIN()
        {
            return View();
        }

        public ActionResult ContainerIssue()
        {
            List<BE.ContainerStockM> deptList = new List<BE.ContainerStockM>();
            deptList = IMBL.GetDeptList();
            ViewBag.ddlDeptList = new SelectList(deptList, "DeptID", "DeptName");
            return View();
        }

        public ActionResult ContainerReturn()
        {
            List<BE.ContainerStockM> deptList = new List<BE.ContainerStockM>();
            deptList = IMBL.GetDeptList();
            ViewBag.ddlDeptList = new SelectList(deptList, "DeptID", "DeptName");

            return View();
        }

        public ActionResult ContainerSold()
        {
            List<BE.ContainerStockM> deptList = new List<BE.ContainerStockM>();
            deptList = IMBL.GetDeptList();
            ViewBag.ddlDeptList = new SelectList(deptList, "DeptID", "DeptName");

            return View();
        }
        public ActionResult AtAGlance()
        {
            //DateTime FromDate = DateTime.Now;
            //DateTime ToDate = DateTime.Now;
            //string Type = "ALL";
            //List<BE.ContainerStockM> containerList = new List<BE.ContainerStockM>();
            //containerList = IMBL.GetSummarydetails(FromDate, ToDate, Type);
            //ViewBag.ContainerList = containerList;
            return View();
        }

        [HttpPost]
        public JsonResult AjaxGetAutoContainerNoList(string prefixText, string Type)
        {
            DataTable dataTable = new DataTable();
            List<BE.ContainerStockM> containerList = new List<BE.ContainerStockM>();
            containerList = IMBL.GetAutoContainerNoList(prefixText, Type);
            return Json(containerList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDetailsAgainstContainerNo(string ContainerNo)
        {
            BE.ContainerStockM containerDetails = new BE.ContainerStockM();
            containerDetails = IMBL.GetDetailsAgainstContainerNo(ContainerNo);
            var JsonResult = Json(containerDetails, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }
        public ActionResult GetDetailsAgainstContainerNoForReturn(string ContainerNo)
        {
            BE.ContainerStockM containerDetails = new BE.ContainerStockM();
            containerDetails = IMBL.GetDetailsAgainstContainerNoForReturn(ContainerNo);
            var JsonResult = Json(containerDetails, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public ActionResult GetDetailsAgainstDept(int DeptID)
        {
            BE.ContainerStockM deptDetails = new BE.ContainerStockM();
            deptDetails = IMBL.GetDetailsAgainstDept(DeptID);
            var JsonResult = Json(deptDetails, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }
        public ActionResult GetSummarydetails(DateTime FromDate, DateTime ToDate, string Type)
        {
            List<BE.ContainerStockM> containerList = new List<BE.ContainerStockM>();
            containerList = IMBL.GetSummarydetails(FromDate, ToDate, Type);
            var JsonResult = Json(containerList, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        //// Saving Code
        ///

        [HttpPost]
        public ActionResult SaveNewContainerIN(BO.ContainerStockM ContainerINList)
        {
            BO.ResponseMessage message = new BO.ResponseMessage();
            try
            {
                ContainerINList.AddedBy = Convert.ToInt16(Session["userid"]);
                ContainerINList.ModifiedBy = Convert.ToInt16(Session["userid"]);
                message = IMBL.SaveNewContainerIN(ContainerINList);
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.Message = ex.Message;
                //Error Log Code
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                var currentMethodName = sf.GetMethod();
                string Name = currentMethodName.Name;
                string Controller = currentMethodName.ReflectedType.Namespace;
                string errorLocation = "Controller: " + Controller + ". ActionName: " + Name;
                addErrorLogDetails(ContainerINList.AddedBy, ContainerINList.AddedOn, errorLocation, ex.Message);
            }
            return Json(message);
        }

        [HttpPost]
        public ActionResult SaveIssuedContainer(BO.ContainerStockM ContainerIssueList)
        {
            BO.ResponseMessage message = new BO.ResponseMessage();
            try
            {
                ContainerIssueList.AddedBy = Convert.ToInt16(Session["userid"]);
                ContainerIssueList.ModifiedBy = Convert.ToInt16(Session["userid"]);
                message = IMBL.SaveIssuedContainer(ContainerIssueList);
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.Message = ex.Message;
                //Error Log Code
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                var currentMethodName = sf.GetMethod();
                string Name = currentMethodName.Name;
                string Controller = currentMethodName.ReflectedType.Namespace;
                string errorLocation = "Controller: " + Controller + ". ActionName: " + Name;
                addErrorLogDetails(ContainerIssueList.AddedBy, ContainerIssueList.AddedOn, errorLocation, ex.Message);
            }
            return Json(message);
        }

        [HttpPost]
        public ActionResult SaveReturnedContainer(BO.ContainerStockM ContainerReturnList)
        {
            BO.ResponseMessage message = new BO.ResponseMessage();
            try
            {
                ContainerReturnList.AddedBy = Convert.ToInt16(Session["userid"]);
                ContainerReturnList.ModifiedBy = Convert.ToInt16(Session["userid"]);
                message = IMBL.SaveReturnedContainer(ContainerReturnList);
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.Message = ex.Message;
                //Error Log Code
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                var currentMethodName = sf.GetMethod();
                string Name = currentMethodName.Name;
                string Controller = currentMethodName.ReflectedType.Namespace;
                string errorLocation = "Controller: " + Controller + ". ActionName: " + Name;
                addErrorLogDetails(ContainerReturnList.AddedBy, ContainerReturnList.AddedOn, errorLocation, ex.Message);
            }
            return Json(message);
        }

        [HttpPost]
        public ActionResult SaveSoldContainer(BO.ContainerStockM ContainerSoldList)
        {
            BO.ResponseMessage message = new BO.ResponseMessage();
            try
            {
                ContainerSoldList.AddedBy = Convert.ToInt16(Session["userid"]);
                ContainerSoldList.ModifiedBy = Convert.ToInt16(Session["userid"]);
                message = IMBL.SaveSoldContainer(ContainerSoldList);
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.Message = ex.Message;
                //Error Log Code
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                var currentMethodName = sf.GetMethod();
                string Name = currentMethodName.Name;
                string Controller = currentMethodName.ReflectedType.Namespace;
                string errorLocation = "Controller: " + Controller + ". ActionName: " + Name;
                addErrorLogDetails(ContainerSoldList.AddedBy, ContainerSoldList.AddedOn, errorLocation, ex.Message);
            }
            return Json(message);
        }

        private void addErrorLogDetails(int addedBy, DateTime addedOn, string errorLocation, string message)
        {
            throw new NotImplementedException();
        }
    }
}