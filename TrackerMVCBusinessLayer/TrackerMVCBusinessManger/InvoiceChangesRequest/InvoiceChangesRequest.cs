using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using System.Data;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.InvoiceChangesRequest
{
   public class InvoiceChangesRequest
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<EN.InvoiceChangesRequestEntities> GetExisitingInvoiceChangesRequest(string name)
        {
            List<EN.InvoiceChangesRequestEntities> passingDL = new List<EN.InvoiceChangesRequestEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetExisitingInvoiceChangesRequest(name);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.InvoiceChangesRequestEntities PassingList = new EN.InvoiceChangesRequestEntities();
                    
                    PassingList.Criteria = Convert.ToString(row["Category"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.InvoiceChangesRequestEntities> GetExisitingInvoiceChangesValidate(string Activity, string AssessNo)
        {
            List<EN.InvoiceChangesRequestEntities> passingDL = new List<EN.InvoiceChangesRequestEntities>();
            DataTable dt = new DataTable();
            dt = trackerdatamanager.GetExisitingInvoiceChangesValidate(Activity, AssessNo);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.InvoiceChangesRequestEntities PassingList = new EN.InvoiceChangesRequestEntities();

                    PassingList.InvoiceDate = Convert.ToString(row["InvoiceDate"]);
                    PassingList.BillTo = Convert.ToString(row["GSTName"]);
                    PassingList.GrandTotal = Convert.ToString(row["GrandTotal"]);
                    PassingList.BillToID = Convert.ToInt64(row["PartyId"]);

                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }

        public List<EN.InvoiceChangesRequestEntities> getInvoiceList()
        {
            DataTable dt = new DataTable();
            DB.TrackerMVCDBManager transportdatamanager = new DB.TrackerMVCDBManager();
            List<EN.InvoiceChangesRequestEntities> Trolleytransportlist = new List<EN.InvoiceChangesRequestEntities>();
            dt = transportdatamanager.getTrolleyInvoicelist();
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    EN.InvoiceChangesRequestEntities TrolleytransportDL = new EN.InvoiceChangesRequestEntities();
                    // TrolleytransportDL.EntryDate = Convert.ToString(row["criteria"]);
                    TrolleytransportDL.Criteria = Convert.ToString(row["Category"]);
                    TrolleytransportDL.AssessNo = Convert.ToString(row["Invoice No"]);
                    TrolleytransportDL.Requeston = Convert.ToString(row["Requested On"]);
                    TrolleytransportDL.RequestBy = Convert.ToString(row["Requested By"]);
                    TrolleytransportDL.Remarks = Convert.ToString(row["Request Remarks"]);
                    TrolleytransportDL.Dwell = Convert.ToInt32(row["Dwell Hours"]);
                    TrolleytransportDL.FilePath = Convert.ToString(row["FilePath"]);
                    TrolleytransportDL.InvoiceDate = Convert.ToString(row["Invoice Date"]);
                    TrolleytransportDL.BillTo = Convert.ToString(row["BilledTo"]);
                    Trolleytransportlist.Add(TrolleytransportDL);
                }
            }
            return Trolleytransportlist;
        }

        //public  string cancelInvoices(string Assessno, int userid)
        //{
        //    string message = "";
        //    DataTable CancelJODT = new DataTable();
        //    CancelJODT = trackerdatamanager.cancelInvoice(Assessno, userid);
        //    //if (CancelJODT != null)
        //    //{
        //    //    message = Convert.ToString(CancelJODT.Rows[0][0]);
        //    //}
        //    return message;
        //}
    }
}
