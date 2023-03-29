using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using EN = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;
using HC = TrackerMVCDataLayer.Helper;
namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.CreditNote
{
  public  class CreditNote
    {
        DB.TrackerMVCDBManager TrackerManager = new DB.TrackerMVCDBManager();
        public List<EN.CreditNoteEntities> getModeGroup()
        {
            
            List<EN.CreditNoteEntities> ModeGroupDL = new List<EN.CreditNoteEntities>();
            DataTable ModeGroupDLL = new DataTable();
            string Table = "CREDIT_NOTE_CATEGORY_M";
            string Column = "ID,TYPE";
            string Condition = "";
            string OrderBy = "";

            ModeGroupDLL = TrackerManager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ModeGroupDLL != null)
            {
                foreach (DataRow row in ModeGroupDLL.Rows)
                {
                    EN.CreditNoteEntities CommodityGroupList = new EN.CreditNoteEntities();
                    CommodityGroupList.CATEGORYID = Convert.ToString(row["ID"]);
                    CommodityGroupList.CATEGORYType = Convert.ToString(row["TYPE"]);
                    ModeGroupDL.Add(CommodityGroupList);
                }
            }
            return ModeGroupDL;
        }

        public List<EN.CreditNoteEntities> GetJOListForSummary(string ddlCategory, string SearchType, string Search)
        {
            List<EN.CreditNoteEntities> JOList = new List<EN.CreditNoteEntities>();
            DataTable JODT = new DataTable();

            JODT = TrackerManager.CreditNoteSeach(ddlCategory, SearchType, Search);
            if (JODT != null)
            {
                foreach (DataRow row in JODT.Rows)
                {
                    EN.CreditNoteEntities JODTList = new EN.CreditNoteEntities();
                    JODTList.AssessNo = Convert.ToString(row["AssessNo"]);
                    JODTList.WorkYear = Convert.ToString(row["WorkYear"]);
                    JODTList.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    JODTList.GSTName = Convert.ToString(row["GSTName"]);
                    JODTList.GSTAddress = Convert.ToString(row["GSTAddress"]);
                    
                    JOList.Add(JODTList);
                }
            }
            return JOList;
        }

        public string SaveLoadedGatepassdetails(DataTable dataTable, string ddlCategory, int Userid)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
             
            string strQuerymax = "";
            string txtbarcode = "";
            string StrGPCode = "";
          
            for (int k = 0; k < dataTable.Rows.Count; k++)
            {

 
                string strSQL = "";


                int i = db.sub_ExecuteNonQuery( "Exec USP_INSERT_INTO_TEMP_CREDIT_NOTE_FOR_MULTIPLE_INVOICES '" + ddlCategory + "','" + dataTable.Rows[k].Field<string>("AssessNo") + "','" + dataTable.Rows[k].Field<string>("WorkYear") + "','" + dataTable.Rows[k].Field<string>("InvoiceNo") + "','" + Userid + "'");
               
                 




                string Messageget = "Record Saved Successfully";
                Message = Messageget;

            }


            return Message;
        }


        public List<EN.CreditNoteEntities> GetGridSummary(int Userid)
        {
            List<EN.CreditNoteEntities> JOList = new List<EN.CreditNoteEntities>();
            DataTable JODT = new DataTable();

            JODT = TrackerManager.CreditNoteSeachGrid(Userid);
            if (JODT != null)
            {
                foreach (DataRow row in JODT.Rows)
                {
                    EN.CreditNoteEntities JODTList = new EN.CreditNoteEntities();
                    JODTList.AssessNo = Convert.ToString(row["AssessNo"]);
                    JODTList.WorkYear = Convert.ToString(row["WorkYear"]);
                    JODTList.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    JODTList.IsTax = Convert.ToString(row["IsTax"]);
                    JODTList.JONo = Convert.ToString(row["JONo"]);
                    JODTList.TaxID = Convert.ToString(row["TaxID"]);
                    JODTList.AccountID = Convert.ToString(row["AccountID"]);
                    JODTList.accountname = Convert.ToString(row["accountname"]);
                    JODTList.CONTAINERNO = Convert.ToString(row["CONTAINERNO"]);
                    JODTList.Size = Convert.ToString(row["Size"]);
                    JODTList.amount = Convert.ToString(row["amount"]);
                    JODTList.creditamount = Convert.ToString(row["creditamount"]);
                    JODTList.PaidAmount = Convert.ToString(row["PaidAmount"]);
                    JODTList.Srno = Convert.ToString(row["Srno"]);
                    JODTList.ZeroAmount = Convert.ToString(0);

                    JOList.Add(JODTList);
                }
            }
            return JOList;
        }


        public List<EN.CreditNoteAmountEntities> GetAssessno(string AssessNo, string WorkYear, int Userid, string Categorys)
        {
            List<EN.CreditNoteAmountEntities> JOList = new List<EN.CreditNoteAmountEntities>();
            DataSet JODT = new DataSet();

            JODT = TrackerManager.CreditNote(AssessNo, WorkYear, Userid, Categorys);
            DataTable JOContainerDT = new DataTable();
             
            JOContainerDT = JODT.Tables[0];
            if (JOContainerDT != null)
            {
                foreach (DataRow row in JOContainerDT.Rows)
                {
                    EN.CreditNoteAmountEntities JODTList = new EN.CreditNoteAmountEntities();
                    JODTList.GSTName = Convert.ToString(row["GSTName"]);
                    JODTList.GSTAddress = Convert.ToString(row["GSTAddress"]);
                    JODTList.statecode = Convert.ToString(row["state_Code"]);
                    JODTList.nettotal = Convert.ToString(row["NetTotal"]);
                    JODTList.sgst = Convert.ToString(row["SGST"]);
                    JODTList.cgst = Convert.ToString(row["CGST"]);
                    JODTList.igst = Convert.ToString(row["IGST"]);
                    JODTList.grandtotal = Convert.ToString(row["GrandTotal"]);
                    JODTList.partyid = Convert.ToString(row["PartyID"]);
                     
                    JOList.Add(JODTList);
                }
            }
            return JOList;
        }

        public List<EN.CreditNoteCre> GetTaxIDAmount(string PaidAmount, int lblTaxID, string statecode)
        {
            List<EN.CreditNoteCre> JOList = new List<EN.CreditNoteCre>();
            DataTable JODT = new DataTable();

            
                double dbltotal = 0d;
                double dblvalSGST = 0d;
                double dbltotalsgst = 0d;
                double dbltotalcgst = 0d;
                double dbltotaligst = 0d;
                double dblvalCGST = 0d;
                double dblvalIGST = 0d;
                double dbldisc = 0d;
                double dblalltotal = 0d;
                string dblTaxAmount = "";
                string dblGroup1Amt = "";
                double dblSGST = 0; double dblCGST = 0; double dblIGST = 0;
                string strSGSTPer;
                    string StrCGSTPEr;
                string StrIGSTPer;
                int dbltaxgroupid = 0;
                DataTable dt1 = new DataTable();
                DataTable dt9 = new DataTable();
                DataTable dt = new DataTable();
                HC.DBOperations db = new HC.DBOperations();
                 
                if (lblTaxID != 0)
                {
                string strSql = "";
                string compid = "";
                   
                    strSql = "";
                    strSql += "select Tinnumber from settings";
                    dt9 = db.sub_GetDatatable(strSql);
                    if (dt9.Rows.Count > 0)
                    {
                        compid = Convert.ToString(dt9.Rows[0][0].ToString());
                    }

                    strSql = "";
                    strSql += "USP_GST_Cal_CreditNote " + lblTaxID + "";
                    dt9 = db.sub_GetDatatable(strSql);
                    if (dt9.Rows.Count > 0)
                    {
                        dblSGST = Convert.ToDouble(dt9.Rows[0]["SGST"].ToString());
                        dblCGST =   Convert.ToDouble(dt9.Rows[0]["CGST"].ToString()); 
                        dblIGST = Convert.ToDouble(dt9.Rows[0]["IGST"].ToString());  
                        dbltaxgroupid = Convert.ToInt32(dt9.Rows[0]["settingsID"].ToString());  
                        strSGSTPer = "SGST " + dblSGST + "%";
                        StrCGSTPEr = "CGST " + dblCGST + "%";
                        StrIGSTPer = "IGST " + dblIGST + "%";
                        if (statecode == compid)
                        {
                            dblIGST = 0;
                            StrIGSTPer = "IGST " + dblIGST + "%";
                        }
                        else
                        {
                            dblSGST = 0;
                            dblCGST = 0;
                            strSGSTPer = "SGST " + dblSGST + "%";
                            StrCGSTPEr = "CGST " + dblCGST + "%";
                        }
                    }
                            dblTaxAmount = PaidAmount;
              
                    dbltotalcgst = dbltotalcgst = Convert.ToDouble(dbltotalcgst + (Convert.ToDouble(dblTaxAmount) * (dblSGST / 100)));
                    dbltotalsgst = dbltotalsgst = Convert.ToDouble(dbltotalsgst + (Convert.ToDouble(dblTaxAmount) * (dblCGST / 100)));
                    dbltotaligst = dbltotaligst = Convert.ToDouble(dbltotaligst + (Convert.ToDouble(dblTaxAmount) * (dblIGST / 100)));

                    dblGroup1Amt += PaidAmount;


                     
                    strSql = "";
                    strSql += "select "+ dblGroup1Amt + " as dblGroup1Amt , round(" + dbltotalcgst + ",0) as totalcgst,round(" + dbltotalsgst + ",0) as totalsgst,round(convert(numeric(10,0)," + dbltotaligst + "),0) as totaligst";
                JODT = db.sub_GetDatatable(strSql);
                    
                    //txtcreditamount.Text = dblGroup1Amt;
                    //txtsgstcredit.Text = Val(dt.Rows(0)("totalsgst"));
                    //txtcgstcredit.Text = Val(dt.Rows(0)("totalcgst"));
                    //txtigstcredit.Text = Val(dt.Rows(0)("totaligst"));
                    //txtcredittotal.Text = dblalltotal;

                if (JODT != null)
                {
                    foreach (DataRow row in JODT.Rows)
                    {
                        EN.CreditNoteCre JODTList = new EN.CreditNoteCre();
                        JODTList.txtcreditamount = Convert.ToString(row["dblGroup1Amt"]);
                        JODTList.txtsgstcredit = Convert.ToString(row["totalsgst"]);
                        JODTList.txtcgstcredit = Convert.ToString(row["totalcgst"]);
                        JODTList.txtigstcredit = Convert.ToString(row["totaligst"]);
                        JODTList.txtcredittotal = Convert.ToDouble(Convert.ToDouble(row["dblGroup1Amt"])) + Convert.ToDouble(Convert.ToDouble(row["totalsgst"])) + Convert.ToDouble(Convert.ToDouble(row["totalcgst"])) + Convert.ToDouble(Convert.ToDouble(row["totaligst"]));

                        JOList.Add(JODTList);
                    }
                }
            }



            
            return JOList;
        }



        public string SaveLoaded(DataTable dataTable, string CreditNoteNo, string Assessno, string CreditNoteDate, string Category, string PartyID, string GrandTotal, string remarks , long UserID,string statecode)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            double dbltotal = 0d;
            double dblvalSGST = 0d;
            double dbltotalsgst = 0d;
            double dbltotalcgst = 0d;
            double dbltotaligst = 0d;
            double dblvalCGST = 0d;
            double dblvalIGST = 0d;
            double dbldisc = 0d;
            double dblalltotal = 0d;
            string dblTaxAmount = "";
            string dblGroup1Amt = "";
            double dblSGST = 0; double dblCGST = 0; double dblIGST = 0;
            string strSGSTPer;
            string StrCGSTPEr;
            string StrIGSTPer;
            int dbltaxgroupid = 0;
            int IFCLID = 0;
            string strQuerymax = "";
            string txtbarcode = "";
            string StrGPCode = "";
            DataTable dt9 = new DataTable();
            double dblSumSGSTAmt = 0;
            double dblSumNetAmtTotal = 0;
            double dblSumCGSTAmt = 0; double dblSumIGSTAmt = 0;
            double dblgrandtotal = 0;
            string strSql = "";
            string strSql1 = "";
            string strWorkYear = "2021-22";
            strQuerymax = "SELECT isnull(MAX(CreditNoteNo),0) +1 as  CreditNoteNo FROM CreditNoteM   WHERE WorkYear='" + strWorkYear + "'";
            dtget = db.sub_GetDatatable(strQuerymax);
            if (dtget.Rows.Count > 0)
            {
                IFCLID = Convert.ToInt16(dtget.Rows[0]["CreditNoteNo"]);
            }
            else
            {
                IFCLID = 1;
            }
            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
   
                string compid = "";

                strSql = "";
                strSql = "select Tinnumber from settings";
                dt9 = db.sub_GetDatatable(strSql);
                if (dt9.Rows.Count > 0)
                {
                    compid = Convert.ToString(dt9.Rows[0][0].ToString());
                }

                strSql = "";
                strSql = "USP_GST_Cal_CreditNote " + dataTable.Rows[k].Field<string>("TaxID") + "";
                dt9 = db.sub_GetDatatable(strSql);
                if (dt9.Rows.Count > 0)
                {
                    dblSGST = Convert.ToDouble(dt9.Rows[0]["SGST"].ToString());
                    dblCGST = Convert.ToDouble(dt9.Rows[0]["CGST"].ToString());
                    dblIGST = Convert.ToDouble(dt9.Rows[0]["IGST"].ToString());
                    dbltaxgroupid = Convert.ToInt32(dt9.Rows[0]["settingsID"].ToString());
                    strSGSTPer = "SGST " + dblSGST + "%";
                    StrCGSTPEr = "CGST " + dblCGST + "%";
                    StrIGSTPer = "IGST " + dblIGST + "%";
                    if (statecode == compid)
                    {
                        dblIGST = 0;
                        StrIGSTPer = "IGST " + dblIGST + "%";
                    }
                    else
                    {
                        dblSGST = 0;
                        dblCGST = 0;
                        strSGSTPer = "SGST " + dblSGST + "%";
                        StrCGSTPEr = "CGST " + dblCGST + "%";
                    }
                }



                strSql = "Exec USP_insert_into_creditnoted '" + IFCLID + "','" + strWorkYear + "','" + dataTable.Rows[k].Field<string>("AccountID") + "','" + dataTable.Rows[k].Field<string>("NetAmount") + "','" + dataTable.Rows[k].Field<string>("CreditAmount") + "',";
                strSql += " '" + Convert.ToDouble(dataTable.Rows[k].Field<string>("CreditAmount")) *(dblSGST / 100) + "','" + Convert.ToDouble(dataTable.Rows[k].Field<string>("CreditAmount")) * (dblCGST / 100) + "','" + Convert.ToDouble(dataTable.Rows[k].Field<string>("CreditAmount")) * (dblIGST / 100) + "','" + dataTable.Rows[k].Field<string>("TaxID") + "','" + dataTable.Rows[k].Field<string>("InvoiceNo") + "',";
                strSql += "'" + dataTable.Rows[k].Field<string>("WorkYear") + "','" + "" + "','" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + dataTable.Rows[k].Field<string>("Size") + "',";
                strSql += "'" + dataTable.Rows[k].Field<string>("JONo") + "'";
                dt = db.sub_GetDatatable(strSql);

            }


            strSql1 = "";
            strSql1 = "get_sum_charges_creditnote '" + IFCLID + "','" + strWorkYear + "'";
            dt = db.sub_GetDatatable(strSql1);
            if (dt.Rows.Count > 0)
            {
                dblSumSGSTAmt = Convert.ToDouble(dt.Rows[0]["SGST"].ToString());  
                dblSumCGSTAmt = Convert.ToDouble(dt.Rows[0]["CGST"].ToString());  
                dblSumIGSTAmt = Convert.ToDouble(dt.Rows[0]["IGST"].ToString());  
                dblSumNetAmtTotal = Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                dblgrandtotal = dblSumSGSTAmt + dblSumCGSTAmt + dblSumIGSTAmt + dblSumNetAmtTotal;
            }

            strSql = "";
            strSql = " exec USP_insert_into_creditnotem " + IFCLID + ",'" + strWorkYear + "','" + Convert.ToDateTime(CreditNoteDate).ToString("dd-MMM-yyyy HH:mm:ss") + "','" +Assessno+ "','" + strWorkYear + "','" +PartyID+ "','" +GrandTotal+ "',";
            strSql += "'" + dblSumNetAmtTotal + "','" + dblSumSGSTAmt + "','" + dblSumCGSTAmt + "','" + dblSumIGSTAmt + "','" + dblgrandtotal + "','" + remarks + "','" + UserID + "','" + Category + "'";
            dt = db.sub_GetDatatable(strSql);
            string Messageget = "Record Saved Successfully!";
            string message = Messageget;

            return message;
        }



        public List<EN.PartyMaster> getPartyMaster()
        {
            List<EN.PartyMaster> passingDL = new List<EN.PartyMaster>();
            DataTable dt = new DataTable();
            dt = TrackerManager.getPartyMaster();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.PartyMaster PassingList = new EN.PartyMaster();
                    PassingList.GSTID = Convert.ToInt32(row["GSTID"]);
                    PassingList.GSTName = Convert.ToString(row["GSTName"]);
                    passingDL.Add(PassingList);
                }
            }
            return passingDL;
        }


        //Reverse Debit Note 


        public List<EN.ReverseDebitNoteEntities> GetReverseDebitShow(string CreditNoteNo)
        {
            List<EN.ReverseDebitNoteEntities> JOList = new List<EN.ReverseDebitNoteEntities>();
            DataSet JODT = new DataSet();

            JODT = TrackerManager.GetReverseDebitShow(CreditNoteNo);
            DataTable JOContainerDT = new DataTable();

            JOContainerDT = JODT.Tables[0];
            if (JOContainerDT != null)
            {
                foreach (DataRow row in JOContainerDT.Rows)
                {
                    EN.ReverseDebitNoteEntities JODTList = new EN.ReverseDebitNoteEntities();
                    JODTList.PartName = Convert.ToString(row["GSTName"]);
                    JODTList.Activity = Convert.ToString(row["Activity"]);
                    JODTList.CreditAmount = Convert.ToString(row["CreditAmt"]);
                    JODTList.CreditSGSTt = Convert.ToString(row["sgst"]);
                    JODTList.CreditCGST = Convert.ToString(row["cgst"]);
                    JODTList.CreditIGST = Convert.ToString(row["igst"]);
                    JODTList.CreditTotal = Convert.ToString(row["GrandTotal"]);
                    JODTList.WorkYear = Convert.ToString(row["WorkYear"]);
                    JODTList.OldCredit = Convert.ToString(row["CreditNoteNo"]);
                    JODTList.statecode = Convert.ToString(row["state_Code"]);
                    JODTList.partyid = Convert.ToString(row["PartyID"]);
                    JOList.Add(JODTList);
                }
            }
            return JOList;
        }


        public List<EN.CreditNoteEntities> GatDebitGridNote(string Activity, string OldCredit, string WorkYear)
        {
            List<EN.CreditNoteEntities> JOList = new List<EN.CreditNoteEntities>();
            DataTable JODT = new DataTable();

            JODT = TrackerManager.GatDebitGridNote(Activity, OldCredit, WorkYear);
            if (JODT != null)
            {
                foreach (DataRow row in JODT.Rows)
                {
                    EN.CreditNoteEntities JODTList = new EN.CreditNoteEntities();
                    
                    JODTList.WorkYear = Convert.ToString(row["InvoiceWorkYear"]);
                    JODTList.InvoiceNo = Convert.ToString(row["InvoiceNo"]);
                    JODTList.JONo = Convert.ToString(row["JONo"]);
                    JODTList.TaxID = Convert.ToString(row["taxgroupid"]);
                    JODTList.AccountID = Convert.ToString(row["AccountID"]);
                    JODTList.accountname = Convert.ToString(row["accountname"]);
                    JODTList.CONTAINERNO = Convert.ToString(row["ContainerNo"]);
                    JODTList.Size = Convert.ToString(row["Size"]);
                    JODTList.amount = Convert.ToString(row["AssessAmt"]);
                    JODTList.creditamount = Convert.ToString(row["CreditAmt"]);
                    JODTList.PaidAmount = Convert.ToString(row["PaidAmount"]);
                    JODTList.Srno = Convert.ToString(row["Srno"]);
                    JODTList.ZeroAmount = Convert.ToString(0);

                    JOList.Add(JODTList);
                }
            }
            return JOList;
        }

        public List<EN.CreditNoteCre> GetTaxIDDebit(string PaidAmount, int lblTaxID, string statecode)
        {
            List<EN.CreditNoteCre> JOList = new List<EN.CreditNoteCre>();
            DataTable JODT = new DataTable();


            double dbltotal = 0d;
            double dblvalSGST = 0d;
            double dbltotalsgst = 0d;
            double dbltotalcgst = 0d;
            double dbltotaligst = 0d;
            double dblvalCGST = 0d;
            double dblvalIGST = 0d;
            double dbldisc = 0d;
            double dblalltotal = 0d;
            string dblTaxAmount = "";
            string dblGroup1Amt = "";
            double dblSGST = 0; double dblCGST = 0; double dblIGST = 0;
            string strSGSTPer;
            string StrCGSTPEr;
            string StrIGSTPer;
            int dbltaxgroupid = 0;
            DataTable dt1 = new DataTable();
            DataTable dt9 = new DataTable();
            DataTable dt = new DataTable();
            HC.DBOperations db = new HC.DBOperations();

            if (lblTaxID != 0)
            {
                string strSql = "";
                string compid = "";

                strSql = "";
                strSql += "select Tinnumber from settings";
                dt9 = db.sub_GetDatatable(strSql);
                if (dt9.Rows.Count > 0)
                {
                    compid = Convert.ToString(dt9.Rows[0][0].ToString());
                }

                strSql = "";
                strSql += "USP_GST_Cal_CreditNote " + lblTaxID + "";
                dt9 = db.sub_GetDatatable(strSql);
                if (dt9.Rows.Count > 0)
                {
                    dblSGST = Convert.ToDouble(dt9.Rows[0]["SGST"].ToString());
                    dblCGST = Convert.ToDouble(dt9.Rows[0]["CGST"].ToString());
                    dblIGST = Convert.ToDouble(dt9.Rows[0]["IGST"].ToString());
                    dbltaxgroupid = Convert.ToInt32(dt9.Rows[0]["settingsID"].ToString());
                    strSGSTPer = "SGST " + dblSGST + "%";
                    StrCGSTPEr = "CGST " + dblCGST + "%";
                    StrIGSTPer = "IGST " + dblIGST + "%";
                    if (statecode == compid)
                    {
                        dblIGST = 0;
                        StrIGSTPer = "IGST " + dblIGST + "%";
                    }
                    else
                    {
                        dblSGST = 0;
                        dblCGST = 0;
                        strSGSTPer = "SGST " + dblSGST + "%";
                        StrCGSTPEr = "CGST " + dblCGST + "%";
                    }
                }
                dblTaxAmount = PaidAmount;

                dbltotalcgst = dbltotalcgst = Convert.ToDouble(dbltotalcgst + (Convert.ToDouble(dblTaxAmount) * (dblSGST / 100)));
                dbltotalsgst = dbltotalsgst = Convert.ToDouble(dbltotalsgst + (Convert.ToDouble(dblTaxAmount) * (dblCGST / 100)));
                dbltotaligst = dbltotaligst = Convert.ToDouble(dbltotaligst + (Convert.ToDouble(dblTaxAmount) * (dblIGST / 100)));

                dblGroup1Amt += PaidAmount;



                strSql = "";
                strSql += "select " + dblGroup1Amt + " as dblGroup1Amt , round(" + dbltotalcgst + ",0) as totalcgst,round(" + dbltotalsgst + ",0) as totalsgst,round(convert(numeric(10,0)," + dbltotaligst + "),0) as totaligst";
                JODT = db.sub_GetDatatable(strSql);

                //txtcreditamount.Text = dblGroup1Amt;
                //txtsgstcredit.Text = Val(dt.Rows(0)("totalsgst"));
                //txtcgstcredit.Text = Val(dt.Rows(0)("totalcgst"));
                //txtigstcredit.Text = Val(dt.Rows(0)("totaligst"));
                //txtcredittotal.Text = dblalltotal;

                if (JODT != null)
                {
                    foreach (DataRow row in JODT.Rows)
                    {
                        EN.CreditNoteCre JODTList = new EN.CreditNoteCre();
                        JODTList.txtcreditamount = Convert.ToString(row["dblGroup1Amt"]);
                        JODTList.txtsgstcredit = Convert.ToString(row["totalsgst"]);
                        JODTList.txtcgstcredit = Convert.ToString(row["totalcgst"]);
                        JODTList.txtigstcredit = Convert.ToString(row["totaligst"]);
                        JODTList.txtcredittotal = Convert.ToDouble(Convert.ToDouble(row["dblGroup1Amt"])) + Convert.ToDouble(Convert.ToDouble(row["totalsgst"])) + Convert.ToDouble(Convert.ToDouble(row["totalcgst"])) + Convert.ToDouble(Convert.ToDouble(row["totaligst"]));

                        JOList.Add(JODTList);
                    }
                }
            }




            return JOList;
        }


        public string DeditSave(DataTable dataTable, string DeditNoteNo, string OldCreditNOte, string DebitNoteDate, string Category, string PartyID, string GrandTotal, string remarks, long UserID, string statecode)
        {
            string Message = "";


            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();
            Dictionary<object, object> parameterList = new Dictionary<object, object>();

            DataTable dt = new DataTable();
            DataTable dtget = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            double dbltotal = 0d;
            double dblvalSGST = 0d;
            double dbltotalsgst = 0d;
            double dbltotalcgst = 0d;
            double dbltotaligst = 0d;
            double dblvalCGST = 0d;
            double dblvalIGST = 0d;
            double dbldisc = 0d;
            double dblalltotal = 0d;
            string dblTaxAmount = "";
            string dblGroup1Amt = "";
            double dblSGST = 0; double dblCGST = 0; double dblIGST = 0;
            string strSGSTPer;
            string StrCGSTPEr;
            string StrIGSTPer;
            int dbltaxgroupid = 0;
            int IFCLID = 0;
            string strQuerymax = "";
            string txtbarcode = "";
            string StrGPCode = "";
            DataTable dt9 = new DataTable();
            double dblSumSGSTAmt = 0;
            double dblSumNetAmtTotal = 0;
            double dblSumCGSTAmt = 0; double dblSumIGSTAmt = 0;
            double dblgrandtotal = 0;
            string strSql = "";
            string strSql1 = "";
            string strWorkYear = "2021-22";
            string Messageget = "";
            Int64 intid = 0;
            string strinvoiceNo = "";
            Int64 txtassessno = 0;
            strQuerymax = "SELECT isnull(MAX(DebitNoteNo),0) +1 as  DebitNoteNo FROM DebitNote_M   WHERE WorkYear='" + strWorkYear + "'";
            dtget = db.sub_GetDatatable(strQuerymax);
            if (dtget.Rows.Count > 0)
            {
                IFCLID = Convert.ToInt16(dtget.Rows[0]["DebitNoteNo"]);
            }
            else
            {
                IFCLID = 1;
            }
            for (int k = 0; k < dataTable.Rows.Count; k++)
            {

                string compid = "";

                strSql = "";
                strSql = "select Tinnumber from settings";
                dt9 = db.sub_GetDatatable(strSql);
                if (dt9.Rows.Count > 0)
                {
                    compid = Convert.ToString(dt9.Rows[0][0].ToString());
                }

                strSql = "";
                strSql = "USP_GST_Cal_CreditNote " + dataTable.Rows[k].Field<string>("TaxID") + "";
                dt9 = db.sub_GetDatatable(strSql);
                if (dt9.Rows.Count > 0)
                {
                    dblSGST = Convert.ToDouble(dt9.Rows[0]["SGST"].ToString());
                    dblCGST = Convert.ToDouble(dt9.Rows[0]["CGST"].ToString());
                    dblIGST = Convert.ToDouble(dt9.Rows[0]["IGST"].ToString());
                    dbltaxgroupid = Convert.ToInt32(dt9.Rows[0]["settingsID"].ToString());
                    strSGSTPer = "SGST " + dblSGST + "%";
                    StrCGSTPEr = "CGST " + dblCGST + "%";
                    StrIGSTPer = "IGST " + dblIGST + "%";
                    if (statecode == compid)
                    {
                        dblIGST = 0;
                        StrIGSTPer = "IGST " + dblIGST + "%";
                    }
                    else
                    {
                        dblSGST = 0;
                        dblCGST = 0;
                        strSGSTPer = "SGST " + dblSGST + "%";
                        StrCGSTPEr = "CGST " + dblCGST + "%";
                    }
                }



                strSql = "Exec USP_insert_into_DebitNote_D '" + IFCLID + "','" + strWorkYear + "','" + dataTable.Rows[k].Field<string>("AccountID") + "','" + dataTable.Rows[k].Field<string>("NetAmount") + "','" + dataTable.Rows[k].Field<string>("CreditAmount") + "',";
                strSql += " '" + Convert.ToDouble(dataTable.Rows[k].Field<string>("CreditAmount")) * (dblSGST / 100) + "','" + Convert.ToDouble(dataTable.Rows[k].Field<string>("CreditAmount")) * (dblCGST / 100) + "','" + Convert.ToDouble(dataTable.Rows[k].Field<string>("CreditAmount")) * (dblIGST / 100) + "','" + dataTable.Rows[k].Field<string>("TaxID") + "','" + dataTable.Rows[k].Field<string>("InvoiceNo") + "',";
                strSql += "'" + dataTable.Rows[k].Field<string>("WorkYear") + "','" + "" + "','" + dataTable.Rows[k].Field<string>("ContainerNo") + "','" + dataTable.Rows[k].Field<string>("Size") + "',";
                strSql += "'" + dataTable.Rows[k].Field<string>("JONo") + "'";
                dt = db.sub_GetDatatable(strSql);

            }


            strSql1 = "";
            strSql1 = "get_sum_charges_DebitNote_D '" + IFCLID + "','" + strWorkYear + "'";
            dt = db.sub_GetDatatable(strSql1);
            if (dt.Rows.Count > 0)
            {
                dblSumSGSTAmt = Convert.ToDouble(dt.Rows[0]["SGST"].ToString());
                dblSumCGSTAmt = Convert.ToDouble(dt.Rows[0]["CGST"].ToString());
                dblSumIGSTAmt = Convert.ToDouble(dt.Rows[0]["IGST"].ToString());
                dblSumNetAmtTotal = Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                dblgrandtotal = dblSumSGSTAmt + dblSumCGSTAmt + dblSumIGSTAmt + dblSumNetAmtTotal;
            }

           

          

            strSql = "";
            strSql = " exec USP_insert_into_DebitNote_M " + IFCLID + ",'" + strWorkYear + "','" + Convert.ToDateTime(DebitNoteDate).ToString("dd-MMM-yyyy HH:mm:ss") + "','" + OldCreditNOte + "','" + strWorkYear + "','" + PartyID + "','" + GrandTotal + "',";
            strSql += "'" + dblSumNetAmtTotal + "','" + dblSumSGSTAmt + "','" + dblSumCGSTAmt + "','" + dblSumIGSTAmt + "','" + dblgrandtotal + "','" + remarks + "','" + UserID + "','" + Category +   "'";
            dt = db.sub_GetDatatable(strSql);
            if (dt.Rows.Count > 0)
            {
                Messageget = Convert.ToString(dt.Rows[0]["mgs"]);
            }

          
            string message = Messageget;

            return message;
        }

  

        public EN.TaxSettings GetCreditNoteList(String CrNote)
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.GetCreditNoteList(CrNote);

            EN.TaxSettings DLList = new EN.TaxSettings();
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DLList.PartyName = Convert.ToString(row["PartyName"]);
                    DLList.CreditDate = Convert.ToString(row["CreditDate"]);
                    DLList.Netotal = Convert.ToInt32(row["Netotal"]);
                    DLList.CGST = Convert.ToDecimal(row["CGST"]);
                    DLList.SGST = Convert.ToDecimal(row["SGST"]);
                    DLList.IGST = Convert.ToDecimal(row["IGST"]);
                    DLList.GrandTotal = Convert.ToInt32(row["GrandTotal"]);

                }

            }
            return DLList;
        }



        // Prashant Kuppili

        public List<EN.SelfAudit> getSelfAuditDDLValues()
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.getSelfAuditDDLValues();
            List<EN.SelfAudit> List = new List<EN.SelfAudit>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.SelfAudit item = new EN.SelfAudit();
                    item.SelfAuditID = Convert.ToInt32(row["SelfAuditID"]);
                    item.ValuesTypes = Convert.ToString(row["ValuesTypes"]);
                    List.Add(item);
                }
            }
            return List;
        }


        public EN.ResponseMessage SaveInvoiceAuditForm(EN.Audit_Invoice_Note invoiceAuditForm, int userid)
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.SaveInvoiceAuditForm(invoiceAuditForm, userid);
            EN.ResponseMessage item = new EN.ResponseMessage();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    item.Status = Convert.ToInt32(row["Status"]);
                    item.Message = Convert.ToString(row["message"]);

                }
            }
            return item;
        }


        public List<EN.Audit_Invoice_Note> GetCategoryList()
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.GetCategoryList();
            List<EN.Audit_Invoice_Note> ContainerList = new List<EN.Audit_Invoice_Note>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.Audit_Invoice_Note item = new EN.Audit_Invoice_Note();
                    item.CreditCategoryID = Convert.ToInt32(row["ID"]);
                    item.CreditCategoryType = Convert.ToString(row["TYPE"]);
                    ContainerList.Add(item);
                }
            }
            return ContainerList;
        }

        public List<EN.ImportUpdateCharges> GetAccountListMaster()
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.GetAccountListMaster();
            List<EN.ImportUpdateCharges> accountList = new List<EN.ImportUpdateCharges>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ImportUpdateCharges item = new EN.ImportUpdateCharges();
                    item.AccountID = Convert.ToInt32(row["AccountID"]);
                    item.AccountName = Convert.ToString(row["AccountName"]);
                    accountList.Add(item);
                }

            }
            return accountList;
        }


        public List<EN.ImportUpdateCharges> ShowJONoForDDL(string containerNo)
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.ShowJONoForDDL(containerNo);
            List<EN.ImportUpdateCharges> JoList = new List<EN.ImportUpdateCharges>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    EN.ImportUpdateCharges item = new EN.ImportUpdateCharges();
                    item.JONo = Convert.ToInt32(row["JONo"]);

                    JoList.Add(item);
                }

            }
            return JoList;
        }



        public EN.ImportUpdateCharges GetAdditionalRemarksDetails(string containerNo, int jONo)
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.GetAdditionalRemarksDetails(containerNo, jONo);
            EN.ImportUpdateCharges info = new EN.ImportUpdateCharges();
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    info.FirstPartyName = Convert.ToString(row["FirstPartyName"]);
                    info.SecondPartyName = Convert.ToString(row["SecPArtyName"]);
                    info.AmountCollect = Convert.ToDecimal(row["Amount"]);
                    info.AccountID = Convert.ToInt32(row["AccountID"]);
                    info.Remarks = Convert.ToString(row["Remarks"]);
                    info.IsChecked = Convert.ToInt32(row["Ischecked"]);
                    info.ContainerNo = Convert.ToString(row["ContainerNo"]);
                }
            }
            return info;
        }

        public EN.ResponseMessage SaveRemarksData(EN.ImportUpdateCharges formData, int userID)
        {
            DataTable dt = new DataTable();
            dt = TrackerManager.SaveRemarksData(formData, userID);
            EN.ResponseMessage responseMessage = new EN.ResponseMessage();
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    responseMessage.Message = Convert.ToString(row["message"]);
                    responseMessage.Status = Convert.ToInt32(row["Status"]);
                }
            }
            return responseMessage;
        }
    }
}
