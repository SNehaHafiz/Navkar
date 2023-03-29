using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.Login
{
    public class Login
    {
        DB.TrackerMVCDBManager Trackerdatamanager = new DB.TrackerMVCDBManager();
        public double totalTEUSForImportArrivalDisplay = 0;
        public double donutValue = 0;
        public double totalTEUSForPipeLineDisplay = 0;
        public double donutValuePipeLineDisplay = 0;
        public EN.LoginEntites LogingetData(string name, string pass)
        {

            DataTable loginData = new DataTable();

            //DB.GetMyREQDBManager getmyreqdatamanager = new DB.GetMyREQDBManager();
            loginData = Trackerdatamanager.getLogin(name, pass);

            EN.LoginEntites objloginentities = new EN.LoginEntites();


            if (loginData.Rows.Count > 0)
            {

                foreach (DataRow row in loginData.Rows)
                {

                    objloginentities.UserID = Convert.ToInt32(row["UserID"]);
                    objloginentities.UserName = Convert.ToString(row["UserName"]);
                    objloginentities.DepType = Convert.ToString(row["DepType"]);
                    objloginentities.UserType = Convert.ToString(row["UserType"]);
                   objloginentities.ConCode = Convert.ToString(row["ConCode"]);

                }
            }
            return objloginentities;
        }

        public List<EN.NotificationList> UserNotificationList(string UserID)
        {

            DataTable notificationData = new DataTable();
            List<EN.NotificationList> newNotification = new List<EN.NotificationList>();
            notificationData = Trackerdatamanager.getCount(UserID);

            EN.NotificationList notificationList = new EN.NotificationList();


            if (notificationData.Rows.Count > 0)
            {

                foreach (DataRow row in notificationData.Rows)
                {

                    notificationList.Desc = row["Description"].ToString();
                    //objloginentities.UserName = Convert.ToString(row["UserName"]);
                    //objloginentities.DepType = Convert.ToString(row["DepType"]);
                    //objloginentities.UserType = Convert.ToString(row["UserType"]);
                    //objloginentities.ConCode = Convert.ToString(row["ConCode"]);
                    newNotification.Add(notificationList);

                }
            }
            return newNotification;
        }

        public DataSet DBLocation(string url)
        {
            DataSet LocationDT = new DataSet();
            LocationDT = Trackerdatamanager.getDBLocationDetails(url);

            return LocationDT;
        }

        public List<EN.SideMenuEntities> getMenuList(int userid)
        {
            List<EN.SideMenuEntities> MenuList = new List<EN.SideMenuEntities>();
            DataTable dt = new DataTable();
            //DB.GetMyREQDBManager getmyreqdatamanager = new DB.GetMyREQDBManager();
            dt = Trackerdatamanager.getMenuList(userid);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.SideMenuEntities MenuListData = new EN.SideMenuEntities();
                    MenuListData.MenuID = Convert.ToInt32(row["MenuID"]);
                    MenuListData.MenuName = Convert.ToString(row["MenuName"]);
                    MenuListData.ParentID = Convert.ToInt32(row["ParentID"]);
                    MenuListData.ControllerName = Convert.ToString(row["Controller"]);
                    MenuListData.Action = Convert.ToString(row["Action"]);
                    MenuListData.menuIcon = Convert.ToString(row["menuIcon"]);
                    MenuList.Add(MenuListData);
                }
            }
            return MenuList;
        }

     
        public List<EN.CustArrivalEntities> GetCustomerArrival(int monthNO)
        {

            List<EN.CustArrivalEntities> CustList = new List<EN.CustArrivalEntities>();
            DataTable dt = new DataTable();
            //DB.GetMyREQDBManager getmyreqdatamanager = new DB.GetMyREQDBManager();
            dt = Trackerdatamanager.GetCustomerArrival(monthNO);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CustArrivalEntities CustListData = new EN.CustArrivalEntities();
                    CustListData.Person = Convert.ToString(row["Person"]);
                    CustListData.size20 = Convert.ToString(row["Size20"]);
                    CustListData.size40 = Convert.ToString(row["Size40"]);
                    CustListData.size45 = Convert.ToString(row["Size45"]);
                    CustListData.TEUS = Convert.ToString(row["Tues"]);
                    CustListData.TTLCount = Convert.ToString(row["TTL CONTS"]);
                    CustListData.css = Convert.ToString(row["css"]);
                    if (CustListData.Person == "Total")
                    { 
                        totalTEUSForImportArrivalDisplay = Convert.ToInt32(row["Tues"]);
                        donutValue = (totalTEUSForImportArrivalDisplay / 10000) ;
                        CustListData.totalTEUSForImportArrivalDisplay = Convert.ToInt32(row["Tues"]);
                        CustListData.donutValue = (totalTEUSForImportArrivalDisplay / 10000);
                    }
                    CustList.Add(CustListData);
                }
            }
            return CustList;
        }

        public DataTable GetPipeLineArrival(int monthNO)
        {
            DataTable dt = new DataTable();
            dt = Trackerdatamanager.GetPipeLineArrival(monthNO);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                { 
                    totalTEUSForPipeLineDisplay += Convert.ToInt32(row["TOTAL"]); 
                }
                donutValuePipeLineDisplay = (totalTEUSForPipeLineDisplay / 5000);
            }
            return dt;
        }

        public int GetSelectedPersonID(string personName)
        {
            DataTable nameDL = new DataTable();
            nameDL = Trackerdatamanager.GetSalesPersonIDForGraph(personName);
            int personID = 0;
            if (nameDL.Rows.Count > 0)
            {
                foreach (DataRow row in nameDL.Rows)
                {
                    personID = Convert.ToInt32(row["SalesPerson_ID1"]);
                }
            }
            return personID;
        }

        public List<EN.GraphDetailsForPerson> GetMonthlyDataForPerson(string personName)
        {

            DataTable graphDL = new DataTable();
            int personID = GetSelectedPersonID(personName);
            graphDL = Trackerdatamanager.GetMonthlyDataForPerson(personID);
            List<EN.GraphDetailsForPerson> graphBL = new List<EN.GraphDetailsForPerson>();
            if (graphDL.Rows.Count > 0)
            {
                foreach (DataRow row in graphDL.Rows)
                {
                    EN.GraphDetailsForPerson graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "May";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "June";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "July";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "August";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "September";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "October";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "November";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "December";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "January";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "February";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "March";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "April";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);
                     
                }
            }
            return graphBL;
        }

        public List<EN.CustArrivalEntities> GetPipeLineArrivalMonthWise(int monthNO)
        {
            DataTable dt = new DataTable();
            dt = Trackerdatamanager.GetPipeLineArrival(monthNO);
            List<EN.CustArrivalEntities> CustList = new List<EN.CustArrivalEntities>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.CustArrivalEntities CustListData = new EN.CustArrivalEntities();
                    CustListData.Person = Convert.ToString(row["Person"]);
                    CustListData.size20 = Convert.ToString(row["20"]);
                    CustListData.size40 = Convert.ToString(row["40"]);
                    CustListData.size45 = Convert.ToString(row["45"]);
                    CustListData.TEUS = Convert.ToString(row["TEUS"]);  
                    
                    totalTEUSForPipeLineDisplay += Convert.ToInt32(row["TEUS"]);
                    CustListData.totalTEUSForImportArrivalDisplay = totalTEUSForPipeLineDisplay;
                    CustList.Add(CustListData);
                }
                donutValuePipeLineDisplay = (totalTEUSForPipeLineDisplay / 5000);
            }
            return CustList;
        }

        public List<EN.GraphDetailsForPerson> GetMonthlyDataForPipeLineReportPerson(string personName)
        {

            DataTable graphDL = new DataTable();
            int personID = GetSelectedPersonID(personName);
            graphDL = Trackerdatamanager.GetPipelineReportForGivenMonth(personID);
            List<EN.GraphDetailsForPerson> graphBL = new List<EN.GraphDetailsForPerson>();
            if (graphDL.Rows.Count > 0)
            {
                foreach (DataRow row in graphDL.Rows)
                {
                    EN.GraphDetailsForPerson graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "May";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "June";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "July";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "August";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "September";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "October";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "November";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "December";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "January";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "February";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "March";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                    graph = new EN.GraphDetailsForPerson();
                    graph.XAxisValue = "April";
                    graph.YAxisValue = Convert.ToDouble(row[graph.XAxisValue]);
                    graphBL.Add(graph);

                }
            }
            return graphBL;
        }

       

        public List<EN.LineGraphValue> GetMonthlyTEUSForImportArrival()
        {
            DataTable graphDL = new DataTable();
            graphDL = Trackerdatamanager.GetMonthlyTEUSForImportArrival();
            List<EN.LineGraphValue> graphBL = new List<EN.LineGraphValue>();

            if (graphDL.Rows.Count > 0)
            {
                foreach (DataRow row in graphDL.Rows)
                {
                    EN.LineGraphValue graph = new EN.LineGraphValue();
                    graph.MonthName = Convert.ToString(row["monthName"]);
                    graph.MonthNo = Convert.ToInt32(row["MonthNo"]);
                    graph.Year = Convert.ToInt32(row["Year"]);
                    graph.Total = Convert.ToInt32(row["Total"]);
                    graph.XAxis = graph.MonthName + "-" + graph.Year.ToString().Substring(2, 2);
                    graphBL.Add(graph);
                }
            }
            return graphBL;
        }

        public List<EN.LineGraphValue> GetMonthlyTEUSForPipeLine()
        {
            DataTable graphDL = new DataTable();
            graphDL = Trackerdatamanager.GetMonthlyTEUSForPipeLine();
            List<EN.LineGraphValue> graphBL = new List<EN.LineGraphValue>();
            if (graphDL.Rows.Count > 0)
            {
                foreach (DataRow row in graphDL.Rows)
                {
                    EN.LineGraphValue graph = new EN.LineGraphValue();
                    graph.MonthName = Convert.ToString(row["monthName"]);
                    graph.MonthNo = Convert.ToInt32(row["MonthNo"]);
                    graph.Year = Convert.ToInt32(row["Year"]);
                    graph.Total = Convert.ToInt32(row["Total"]);
                    graph.XAxis = graph.MonthName + "-" + graph.Year.ToString().Substring(2, 2);
                    graphBL.Add(graph);
                }
            }
            return graphBL;
        }


        //codes by prashant
        public List<EN.DailyMovementRequestEntities> GetrDailyMovement()
        {
            DataTable Tues = new DataTable();
            Tues = Trackerdatamanager.GetDailyMovment();
            List<EN.DailyMovementRequestEntities> ContainerList = new List<EN.DailyMovementRequestEntities>();

            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.DailyMovementRequestEntities Tues1 = new EN.DailyMovementRequestEntities();

                    Tues1.Process = Convert.ToString(row["process"]);
                    Tues1.Size20 = Convert.ToString(row["size20"]);
                    Tues1.Size40 = Convert.ToString(row["size40"]);
                    Tues1.Size45 = Convert.ToString(row["size45"]);
                    Tues1.Total = Convert.ToString(row["TOTAL"]);
                    Tues1.Teus = Convert.ToString(row["TEUS"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }


        public List<EN.JobOrderListSummaryEntities> GetJoborderdetails(int salesid,string fromdate, string todate)
        {
            DataTable Tues = new DataTable();
            Tues = Trackerdatamanager.GetJobOrderDetails(salesid, fromdate, todate);
            List<EN.JobOrderListSummaryEntities> ContainerList = new List<EN.JobOrderListSummaryEntities>();

            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.JobOrderListSummaryEntities Tues1 = new EN.JobOrderListSummaryEntities();

                    Tues1.JoNO = Convert.ToString(row["Jono"]);
                    Tues1.blnumber = Convert.ToString(row["blnumber"]);
                    Tues1.SalesPerson_Name = Convert.ToString(row["SalesPerson_Name"]);
                    Tues1.customer = Convert.ToString(row["customer"]);
                    Tues1.Tot20 = Convert.ToString(row["Tot20"]);
                    Tues1.Tot40 = Convert.ToString(row["Tot40"]);
                    Tues1.Tot45 = Convert.ToString(row["Tot45"]);
                    Tues1.Total = Convert.ToString(row["Total"]);
                    Tues1.Teus = Convert.ToString(row["Teus"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        public List<EN.JobOrderListSummaryEntities> GetCustomerJObOrder(string fromdate, string todate)
        {
            DataTable Tues = new DataTable();
            Tues = Trackerdatamanager.GetCustomerJobOrderDetails(fromdate, todate);
            List<EN.JobOrderListSummaryEntities> ContainerList = new List<EN.JobOrderListSummaryEntities>();

            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.JobOrderListSummaryEntities Tues1 = new EN.JobOrderListSummaryEntities();
                    Tues1.customer = Convert.ToString(row["customer"]);
                    Tues1.Tot20 = Convert.ToString(row["Tot20"]);
                    Tues1.Tot40 = Convert.ToString(row["Tot40"]);
                    Tues1.Tot45 = Convert.ToString(row["Tot45"]);
                    Tues1.Total = Convert.ToString(row["Total"]);
                    Tues1.Teus = Convert.ToString(row["Teus"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        //Codes For Sales
        public List<EN.JobOrderListSummaryEntities> GetSalesWise(string fromdate, string todate)
        {
            DataTable Tues = new DataTable();
            Tues = Trackerdatamanager.GetSalesWiseJObOrderdetails(fromdate, todate);
            List<EN.JobOrderListSummaryEntities> ContainerList = new List<EN.JobOrderListSummaryEntities>();

            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.JobOrderListSummaryEntities Tues1 = new EN.JobOrderListSummaryEntities();
                    Tues1.SalesPerson_Name = Convert.ToString(row["SalesPerson_Name"]);
                    Tues1.Tot20 = Convert.ToString(row["Tot20"]);
                    Tues1.Tot40 = Convert.ToString(row["Tot40"]);
                    Tues1.Tot45 = Convert.ToString(row["Tot45"]);
                    Tues1.Total = Convert.ToString(row["Total"]);
                    Tues1.Teus = Convert.ToString(row["Teus"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }


        public List<EN.DailyMovementRequestEntities> GetPipeline()
        {
            DataTable Tues = new DataTable();
            Tues = Trackerdatamanager.GetPipeLIneReports();
            List<EN.DailyMovementRequestEntities> ContainerList = new List<EN.DailyMovementRequestEntities>();

            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.DailyMovementRequestEntities Tues1 = new EN.DailyMovementRequestEntities();
                    Tues1.Process = Convert.ToString(row["Person"]);
                    Tues1.Size20 = Convert.ToString(row["20"]);
                    Tues1.Size40 = Convert.ToString(row["40"]);
                    Tues1.Size45 = Convert.ToString(row["45"]);
                   
                    Tues1.Teus = Convert.ToString(row["TEUS"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }


        public List<EN.DailyMovementRequestEntities> GetPipelinemonth(string Monthdate)
        {
            DataTable Tues = new DataTable();
            Tues = Trackerdatamanager.AjaxGetPipeLIneReports(Monthdate);
            List<EN.DailyMovementRequestEntities> ContainerList = new List<EN.DailyMovementRequestEntities>();

            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.DailyMovementRequestEntities Tues1 = new EN.DailyMovementRequestEntities();
                    Tues1.Process = Convert.ToString(row["Person"]);
                    Tues1.Size20 = Convert.ToString(row["20"]);
                    Tues1.Size40 = Convert.ToString(row["40"]);
                    Tues1.Size45 = Convert.ToString(row["45"]);

                    Tues1.Teus = Convert.ToString(row["TEUS"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }
        public List<EN.DailyMovementRequestEntities> GetFuelReport(string fromdate, string todate)
        {
            DataTable Tues = new DataTable();
            Tues = Trackerdatamanager.AjaxFuelReport(fromdate, todate);
            List<EN.DailyMovementRequestEntities> ContainerList = new List<EN.DailyMovementRequestEntities>();

            if (Tues.Rows.Count != 0)
            {
                foreach (DataRow row in Tues.Rows)
                {
                    EN.DailyMovementRequestEntities Tues1 = new EN.DailyMovementRequestEntities();
                    Tues1.Process = Convert.ToString(row["Activity"]);
                    Tues1.Size20 = Convert.ToString(row["20"]);
                    Tues1.Size40 = Convert.ToString(row["40"]);
                    Tues1.Size45 = Convert.ToString(row["45"]);

                    Tues1.Teus = Convert.ToString(row["TEUS"]);
                    Tues1.Amount = Convert.ToString(row["Amount"]);
                    Tues1.Fuel = Convert.ToString(row["Fuel"]);
                    Tues1.Total = Convert.ToString(row["Total"]);
                    ContainerList.Add(Tues1);
                }

            }
            return ContainerList;

        }

        // developed by prathamesh
        public List<EN.SubmenuInfo> getMenuList(string SearchText, int userid)
        {
            List<EN.SubmenuInfo> MenuList = new List<EN.SubmenuInfo>();
            DataTable dt = new DataTable();
            //DB.GetMyREQDBManager getmyreqdatamanager = new DB.GetMyREQDBManager();
            dt = Trackerdatamanager.getMenuList(SearchText, userid);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.SubmenuInfo MenuListData = new EN.SubmenuInfo();
                    MenuListData.MenuID = Convert.ToInt16(row["MenuID"]);
                    MenuListData.MenuName = Convert.ToString(row["MenuName"]);
                    MenuListData.SubMenu = Convert.ToString(row["SubMenuName"]);
                    MenuListData.MenuText = Convert.ToString(row["MenuName"]);
                    MenuListData.ParentID = Convert.ToInt16(row["ParentID"]);
                    MenuListData.ControllerName = Convert.ToString(row["Controller"]);
                    MenuListData.Action = Convert.ToString(row["Action"]);
                    MenuListData.menuIcon = Convert.ToString(row["menuIcon"]);
                    MenuListData.IsFavourite = Convert.ToInt32(row["IsFavourite"]);
                    MenuList.Add(MenuListData);

                    //MenuInfoList.MenuID = Convert.ToInt16(row["MenuID"]);
                    //MenuInfoList.MenuText = Convert.ToString(row["MenuName"]);
                    //MenuInfoList.ParentID = Convert.ToInt16(row["ParentID"]);
                    //MenuInfoList.ControllerName = Convert.ToString(row["Controller"]);
                    //MenuInfoList.Action = Convert.ToString(row["Action"]);
                    //MenuInfoList.menuIcon = Convert.ToString(row["Icon"]);
                }
            }
            return MenuList;
        }

        public string SetFavouriteMenu(int MenuID, int UserID)
        {

            try
            {


                string success;

                DataTable sited = new DataTable();



                sited = Trackerdatamanager.SetFavouriteMenu(MenuID, UserID);
                success = Convert.ToString(sited.Rows[0][0]);
                return success;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EN.ContainerStockM> GetStockDetails()
        {
            try
            {
                List<EN.ContainerStockM> stockDL = new List<EN.ContainerStockM>();
                DataTable stockDT = new DataTable();
                stockDT = Trackerdatamanager.GetStockDetails();
                if (stockDT != null)
                {
                    if (stockDT.Rows.Count > 0)
                    {
                        foreach (DataRow row in stockDT.Rows)
                        {
                            EN.ContainerStockM stockList = new EN.ContainerStockM();
                            stockList.ID = Convert.ToInt32(row["ID"]);
                            stockList.TotalCount = Convert.ToInt32(row["TotalCount"]);
                            stockList.Title = Convert.ToString(row["Title"]);
                            stockDL.Add(stockList);
                        }
                    }
                }
                return stockDL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EN.ContainerStockM> GetContainerStockList(int deptID, string title)
        {
            try
            {
                List<EN.ContainerStockM> deptDL = new List<EN.ContainerStockM>();
                DataTable dt = new DataTable();
                dt = Trackerdatamanager.GetContainerStockList(deptID, title);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        EN.ContainerStockM stockList = new EN.ContainerStockM();
                        stockList.Size = Convert.ToInt32(row["Size"]);
                        stockList.DeptID = Convert.ToInt32(row["DeptID"]);
                        stockList.TransMadeAgainst = Convert.ToString(row["TransMadeAgainst"]);
                        stockList.DisplayPurchaseDate = Convert.ToString(row["DisplayPurchaseDate"]);
                        stockList.DeptName = Convert.ToString(row["DeptName"]);
                        stockList.ContactPerson = Convert.ToString(row["ContactPerson"]);
                        stockList.ContactNo = Convert.ToString(row["ContactNo"]);
                        stockList.Type = Convert.ToString(row["Type"]);
                        stockList.VendorName = Convert.ToString(row["VendorName"]);
                        stockList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                        stockList.Remarks = Convert.ToString(row["Remarks"]);
                        deptDL.Add(stockList);
                    }
                }
                return deptDL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
