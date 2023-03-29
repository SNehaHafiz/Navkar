using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerMVC.Filters;
using BE = TrackerMVCEntities.BusinessEntities;
using Survey = TrackerMVCBusinessLayer.TrackerMVCBusinessManger.UpdateSurvey;
using BM = TrackerMVCBusinessLayer.TrackerMVCBusinessManger;
using HC = TrackerMVCDataLayer.Helper;
using Newtonsoft.Json;
using System.Data;

namespace TrackerMVC.Controllers.ImportSpliting
{
    public class ImportSplitingController : Controller
    {
        // GET: ImportSpliting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImportSpliting()
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1).ToString("dd MMM yyyy HH:mm"); ;
            ViewBag.FromDate = startDate;
            return View();
        }

        public JsonResult SubmitData(string IGMNo, string ItemNo, string MSItemNo,string SpBLNo, string SplitType, List<BE.ContainerDetails> containerDet, List<BE.IGMEntities> ItemDet)
        {
            int retVal = 0;
            HC.DBOperations db = new HC.DBOperations();
            BM.IGM.IGM IG = new BM.IGM.IGM();
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataTable DT = new DataTable();
            DataTable DT2 = new DataTable();
            long UserId =Convert.ToInt64(Session["Tracker_userID"]);

            DT.Columns.Add("ContainerNo");
            DT.Columns.Add("ContainerSize");
            DT.Columns.Add("ContainerType");
            DT.Columns.Add("FL");
            DT.Columns.Add("Pkgs");
            DT.Columns.Add("CargoWt");

            DT2.Columns.Add("ItemNo");
            DT2.Columns.Add("Pkgs");
            DT2.Columns.Add("GrossWt");

            if (containerDet.Count > 0)
            {
                foreach(BE.ContainerDetails container in containerDet)
                {
                    DataRow dar = DT.NewRow();
                    dar["ContainerNo"] = container.ContainerNO;
                    dar["ContainerSize"] = container.Size;
                    dar["ContainerType"] = container.Type;
                    dar["FL"] = container.FCLLCType;
                    dar["Pkgs"] = container.PKGS;
                    dar["CargoWt"] = container.Weight;
                    DT.Rows.Add(dar);
                }
            }

            if (ItemDet.Count > 0)
            {
                foreach (BE.IGMEntities iGM in ItemDet)
                {
                    DataRow dar = DT2.NewRow();
                    dar["ItemNo"] = iGM.ItemNo;
                    dar["Pkgs"] = iGM.NoOfPkgs;
                    dar["GrossWt"] = iGM.Weight;
                    DT2.Rows.Add(dar);
                }

            }

            if (DT.Rows.Count > 0)
            {
                ds.Tables.Add(DT);
                ds.Tables.Add(DT2);
            }

            var logDataList = "0";
            ds1 = IG.AddIGMSplitData(ds, IGMNo, ItemNo, SplitType, MSItemNo, UserId,SpBLNo);

            if (ds1.Tables[0].Rows.Count > 0){
                logDataList = JsonConvert.SerializeObject(ds1.Tables[0]);
            }
            
            var returnField = new { DataList = logDataList };

            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetImportSplitingData(string IGMNo,string ItemNo)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataSet ds = new DataSet();
            DataTable dtContainerList = new DataTable();
            DataTable dtItemList = new DataTable();
            DataTable dtMessage = new DataTable();
            List<BE.ContainerDetails> containerDetailslst = new List<BE.ContainerDetails>();
            List<BE.IGMEntities> IGMItemList = new List<BE.IGMEntities>();
            var Error = "";

            ds = db.sub_GetDataSets("GetImportSplitingData '"+ IGMNo + "','" + ItemNo +"'");
            if (ds.Tables.Count > 0)
            {
                dtContainerList = ds.Tables[0];
                dtItemList = ds.Tables[1];
                dtMessage= ds.Tables[2];
                if (dtContainerList.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow dataRow in dtContainerList.Rows)
                        {
                            BE.ContainerDetails container = new BE.ContainerDetails();
                            container.ContainerNO = dataRow["Container No"].ToString();
                            container.Size = dataRow["Size"].ToString();
                            container.Type = dataRow["Type"].ToString();
                            container.FCLLCType = dataRow["Fcl/Lcl"].ToString();
                            container.PKGS = Convert.ToInt64(dataRow["Pkgs"]);
                            container.Weight = Convert.ToDouble(dataRow["Cargo Wt"]);
                            containerDetailslst.Add(container);
                        }
                    }
                    catch(Exception ex)
                    {
                        Error = ex.Message;
                    }

                    try
                    {
                        foreach (DataRow row in dtItemList.Rows)
                        {
                            BE.IGMEntities iGM = new BE.IGMEntities();
                            iGM.ItemNo = row["Item No"].ToString();
                            iGM.NoOfPkgs = row["Pkgs"].ToString();
                            iGM.Weight = row["Wt"].ToString();
                            IGMItemList.Add(iGM);
                        }
                    }
                    catch(Exception ex)
                    {
                        Error = ex.Message;
                    }
                    

                    if (dtMessage.Rows.Count > 0)
                    {
                        foreach(DataRow row in dtMessage.Rows)
                        {
                            Error = row["Message"].ToString();
                        }
                    }
                    if(Error != "")
                    {
                        containerDetailslst = new List<BE.ContainerDetails>();
                        IGMItemList = new List<BE.IGMEntities>();
                    }
                }
                else
                {
                    containerDetailslst = new List<BE.ContainerDetails>();
                    IGMItemList = new List<BE.IGMEntities>();
                }
               
            }

            var returnField = new { Containerlst = containerDetailslst, ItemLst = IGMItemList, message= Error };
            return new JsonResult() { Data = returnField, JsonRequestBehavior=JsonRequestBehavior.AllowGet };
        }

        public JsonResult ExportToExcelSplitingSummary(string FromDate, string ToDate)
        {
            HC.DBOperations db = new HC.DBOperations();
            DataSet ds = new DataSet();
            DataTable dtContainerList = new DataTable();
            var ContainerList = "";
            var ItemList = "";

            dtContainerList = db.sub_GetDatatable("GetSplitingSummaryDet '" + FromDate + "','" + ToDate + "'");

            ContainerList = JsonConvert.SerializeObject(dtContainerList);

            var returnField = new { Containerlst = ContainerList};
            return new JsonResult() { Data = returnField, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}