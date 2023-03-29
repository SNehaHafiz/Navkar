using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.IGM
{
    public class IGM
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public string AddIGMFileData(DataTable dt, int userid)
        {
            List<EN.IGMEntities> IGMList = new List<EN.IGMEntities>();
            DataTable IgmDt = new DataTable();
            int i;
            string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userid);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            i = db.AddTypeTableData("USP_InsertIGMFileData", lstparam, dt, "PT_IGMFile", "@PT_IGMFile");
            if (i > 0)
            {
                message = "File Upload Successfully.";
            }
            return message;
        }
        public int AddIGMFileDataBL(DataTable dt, int userid, string ViaNo,long JoNo)
        {
            List<EN.IGMEntities> IGMList = new List<EN.IGMEntities>();
            DataTable IgmDt = new DataTable();
            int i;
            int IgmFileId = 0;
            string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userid);
            lstparam.Add("viaNO", ViaNo);
            lstparam.Add("JoNo", JoNo);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            IgmFileId = db.AddTypeTableDataIgm("USP_InsertIGMFileData", lstparam, dt, "PT_IGMFile", "@PT_IGMFile");
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return IgmFileId;
        }

        public int AddBCNRRData(DataTable dt, int userid, string TrainNo,string FileType,string WagonRRNo)
        {
            List<EN.IGMEntities> IGMList = new List<EN.IGMEntities>();
            DataTable IgmDt = new DataTable();
            int i;
            int IgmFileId = 0;
            string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userid);
            lstparam.Add("TrainNo", TrainNo);
            lstparam.Add("FileType", FileType);
            lstparam.Add("WagonRRNo", WagonRRNo);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            IgmFileId = db.AddTypeTableDataIgm("USP_InsertRRBCNFileData", lstparam, dt, "PT_IGMFile", "@PT_IGMFile");
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return IgmFileId;
        }

        public int AddIGMFileDataExcel(DataTable dt, int userid, string ViaNo)
        {
            List<EN.IGMEntities> IGMList = new List<EN.IGMEntities>();
            DataTable IgmDt = new DataTable();
            int i;
            int IgmFileId = 0;
            string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userid);
            lstparam.Add("viaNO", ViaNo);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            IgmFileId = db.AddTypeTableDataIgm("USP_InsertIGMFileDataEXCEL", lstparam, dt, "PT_IGMFILEEXCELMANUAL", "@PT_IGMFILEEXCEL");
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return IgmFileId;
        }
        public int AddCGMFileData1(DataTable dt, int userid, string ViaNo, long JoNo, long FileTypeId)
        {
            List<EN.IGMEntities> IGMList = new List<EN.IGMEntities>();
            DataTable IgmDt = new DataTable();
            int i;
            int IgmFileId = 0;
            string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();

            lstparam.Add("userId", userid);
            lstparam.Add("ViaNo", ViaNo);
            lstparam.Add("JoNo", JoNo);
          
            //     lstparam.Add("FILETYPEID", FileTypeId);


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            IgmFileId = db.AddTypeTableDataIgm("USP_InsertCGMFileData", lstparam, dt, "PT_IGMFile", "@PT_IGMFile");
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return IgmFileId;
        }
        public int AddCGMFileData(DataTable dt, int userid, string ViaNo,long JoNo)
        {
            List<EN.IGMEntities> IGMList = new List<EN.IGMEntities>();
            DataTable IgmDt = new DataTable();
            int i;
            int IgmFileId = 0;
            string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("userId", userid);
            lstparam.Add("ViaNo", ViaNo);
            lstparam.Add("JoNo", JoNo);

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            IgmFileId = db.AddTypeTableDataIgm("USP_InsertCGMFileData", lstparam, dt, "PT_IGMFile", "@PT_IGMFile");
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return IgmFileId;
        }

        public DataSet AddIGMSplitData(DataSet ds, string IgmNo, string ItemNo, string SplitType, string MSItemNo,long UserId,string SpBLNo)
        {
            List<EN.IGMEntities> IGMList = new List<EN.IGMEntities>();
            DataTable IgmDt = new DataTable();
            DataTable IgmDt2 = new DataTable();
            DataSet DS = new DataSet();
            int i;
            int IgmFileId = 0;
            string message = "";
            Dictionary<object, object> lstparam = new Dictionary<object, object>();
            lstparam.Add("IGM_NO", IgmNo);
            lstparam.Add("ITEM_NO", ItemNo);
            lstparam.Add("TYPE", SplitType);
            lstparam.Add("MSITEM", MSItemNo);
            lstparam.Add("USER_ID", UserId);
            lstparam.Add("IGM_BLNo", SpBLNo);

            Dictionary<object, object> lstparam2 = new Dictionary<object, object>();
            lstparam2.Add("CONTAINERDTSPLIT", "@CONTAINERDTSPLIT");
            lstparam2.Add("ITEMDTSPLIT", "@ITEMDTSPLIT");

            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            DS = db.AddTypeTableDataSplit("INSSPLITTINGDATA", lstparam, ds,lstparam2);
            if (IgmFileId > 0)
            {
                //message = "File Upload Successfully.";
            }
            return DS;
        }
    }
}
