using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using BE = TrackerMVCEntities.BusinessEntities;
using DB = TrackerMVCDataLayer;

namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.ExpensesHead
{
     public class ExpensesHead
    {
        DB.TrackerMVCDBManager trackerdatamanager = new DB.TrackerMVCDBManager();
        public List<BE.ExpHeadMasterEnt> getExpenses()
        {
            List<BE.ExpHeadMasterEnt> ExpensesrDL = new List<BE.ExpHeadMasterEnt>();
            DataTable ExpensesrDT = new DataTable();
            string Table = "Expenses_Head_M";
            string Column = "isnull(Expenses_Head_Id,0)EntryID,HeadName";
            string Condition = "";
            string OrderBy = "HeadName";

            ExpensesrDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesrDT != null)
            {
                foreach (DataRow row in ExpensesrDT.Rows)
                {
                    BE.ExpHeadMasterEnt CustomerList = new BE.ExpHeadMasterEnt();
                    CustomerList.EntryID = Convert.ToInt32(row["EntryID"]);
                    CustomerList.HeadName = Convert.ToString(row["HeadName"]);

                    ExpensesrDL.Add(CustomerList);
                }
            }
            return ExpensesrDL;
        }

        public List<BE.ExpHeadMasterEnt> getExpensesType()
        {
            List<BE.ExpHeadMasterEnt> ExpensesTypeDL = new List<BE.ExpHeadMasterEnt>();
            DataTable ExpensesTypeDT = new DataTable();
            string Table = "Expenses_Type_M";
            string Column = "isnull(EntryID,0)EntryID,ExpType";
            string Condition = "";
            string OrderBy = "ExpType";

            ExpensesTypeDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (ExpensesTypeDT != null)
            {
                foreach (DataRow row in ExpensesTypeDT.Rows)
                {
                    BE.ExpHeadMasterEnt CustomerList = new BE.ExpHeadMasterEnt();
                    CustomerList.EntryID = Convert.ToInt32(row["EntryID"]);
                    CustomerList.ExpType = Convert.ToString(row["ExpType"]);

                    ExpensesTypeDL.Add(CustomerList);
                }
            }
            return ExpensesTypeDL;
        }

        public List<BE.ExpHeadMasterEnt> getTaxName()
        {
            List<BE.ExpHeadMasterEnt> TaxDL = new List<BE.ExpHeadMasterEnt>();
            DataTable TaxNameDT = new DataTable();
            string Table = "settings_taxes";
            string Column = "isnull(settingsID,0)settingsID,taxname";
            string Condition = "";
            string OrderBy = "taxname";

            TaxNameDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TaxNameDT != null)
            {
                foreach (DataRow row in TaxNameDT.Rows)
                {
                    BE.ExpHeadMasterEnt CustomerList = new BE.ExpHeadMasterEnt();
                    CustomerList.TaxID = Convert.ToInt32(row["settingsID"]);
                    CustomerList.TaxName = Convert.ToString(row["taxname"]);

                    TaxDL.Add(CustomerList);
                }
            }
            return TaxDL;
        }

        public List<BE.ExpHeadMasterEnt> getPayMode()
        {
            List<BE.ExpHeadMasterEnt> TaxDL = new List<BE.ExpHeadMasterEnt>();
            DataTable TaxNameDT = new DataTable();
            string Table = "TDS_Expenses_M";
            string Column = "isnull(PER_VALUES,0)TDS_PER_ID,PERCENTAGE";
            string Condition = "";
            string OrderBy = "PERCENTAGE";

            TaxNameDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TaxNameDT != null)
            {
                foreach (DataRow row in TaxNameDT.Rows)
                {
                    BE.ExpHeadMasterEnt CustomerList = new BE.ExpHeadMasterEnt();
                    CustomerList.PaymodeId = Convert.ToString(row["TDS_PER_ID"]);
                    CustomerList.Paymode = Convert.ToString(row["PERCENTAGE"]);

                    TaxDL.Add(CustomerList);
                }
            }
            return TaxDL;
        }

        public List<BE.ExpHeadMasterEnt> getEntryType()
        {
            List<BE.ExpHeadMasterEnt> TaxDL = new List<BE.ExpHeadMasterEnt>();
            DataTable TaxNameDT = new DataTable();
            string Table = "Expenses_Entry_Type_M";
            string Column = "isnull(Exp_Entry_Type_ID,0)Exp_Entry_Type_ID,EntryTYpe";
            string Condition = "";
            string OrderBy = "EntryTYpe";

            TaxNameDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (TaxNameDT != null)
            {
                foreach (DataRow row in TaxNameDT.Rows)
                {
                    BE.ExpHeadMasterEnt CustomerList = new BE.ExpHeadMasterEnt();
                    CustomerList.EntryTypeID = Convert.ToInt32(row["Exp_Entry_Type_ID"]);
                    CustomerList.EntryType = Convert.ToString(row["EntryTYpe"]);

                    TaxDL.Add(CustomerList);
                }
            }
            return TaxDL;
        }

        public string SavepartyDebitEntry(DataTable Invoicedata, double VendorID, string Billno, string Billdate,
         string Grandtotal, string Sgst, string Cgst, string IGST, string Remarks ,int Userid,int entryTypeID, string NetAmounts, string VOUCHER_NO,string VoucherDate)
        {
            string Message = "";
            TrackerMVCDataLayer.Helper.DBOperations db = new TrackerMVCDataLayer.Helper.DBOperations();

            Dictionary<object, object> parameterList = new Dictionary<object, object>();
            parameterList.Add("VendorID", VendorID);
            parameterList.Add("Billno", Billno);
            parameterList.Add("Billdate", Billdate);
            parameterList.Add("Grandtotal", Grandtotal);
            parameterList.Add("Sgst", Sgst);
            parameterList.Add("Cgst", Cgst);
            parameterList.Add("IGST", IGST);
            parameterList.Add("Remarks", Remarks);
            parameterList.Add("Userid", Userid);
            parameterList.Add("entryID", entryTypeID);
            parameterList.Add("netAmount", NetAmounts);
            parameterList.Add("VOUCHER_NO", VOUCHER_NO);
            parameterList.Add("VOUCHER_DATE", VoucherDate);
            int i = db.AddTypeTableData("USP_INSERT_EXPENSES_VOUCHER_M", parameterList, Invoicedata, "PT_ExpensesVoucher", "@PT_ExpensesVoucher");


            if (i > 0)
            {

                Message = "Record saved successfully";

            }
            else
            {
                Message = "Record not saved please try again!";

            }
            return Message;



        }

        public List<BE.ExpHeadMasterEnt> GetJOListForSummary(string VO_No)
        {
            List<BE.ExpHeadMasterEnt> JOList = new List<BE.ExpHeadMasterEnt>();
            DataTable JODT = new DataTable();

            JODT = trackerdatamanager.GetEditList(VO_No);
            if (JODT != null)
            {
                foreach (DataRow row in JODT.Rows)
                {
                    BE.ExpHeadMasterEnt JODTList = new BE.ExpHeadMasterEnt();
                    JODTList.DEbitRefernceno = Convert.ToString(row["VOUCHER_NO"]);
                    JODTList.FromDate = Convert.ToString(row["FROM_DATE"]);
                    JODTList.ToDate = Convert.ToString(row["TO_DATE"]);
                    JODTList.ExpensesHead = Convert.ToString(row["HeadName"]);
                    JODTList.Description = Convert.ToString(row["Descriptions"]);
                    JODTList.HSNCode = Convert.ToInt32(row["Unit"]);
                    JODTList.HSNCode1 = Convert.ToString(row["HSNCode"]);
                    JODTList.Netamount = Convert.ToString(row["Amount"]);
                    JODTList.CGST1 = Convert.ToInt32(row["D_CGST"]);
                    JODTList.SGST1 = Convert.ToInt32(row["D_SGST"]);
                    JODTList.SIGST1 = Convert.ToInt32(row["D_IGST"]);
                    JODTList.Grandtotal = Convert.ToString(row["GrandTotal"]);
                    JODTList.TaxName1 = Convert.ToString(row["taxname"]);

                    JODTList.netDiscount = Convert.ToString(row["Discper"]);
                    JODTList.netDiscountS = Convert.ToString(row["DiscAmt"]);
                    JODTList.ddlparTDS = Convert.ToString(row["TDSPerID"]);
                    JODTList.tdsAmount = Convert.ToString(row["TDSAmt"]);
                    JODTList.ExpensesHeadID = Convert.ToString(row["Expenses_Head_ID"]);
                    JODTList.TaxName = Convert.ToString(row["Tax_Code_ID"]);
                    JOList.Add(JODTList);
                }
            }
            return JOList;
        }

        public List<BE.ExpHeadMasterEnt> GetViewSummary(string VOUCHER_NO)
        {
            List<BE.ExpHeadMasterEnt> JOList = new List<BE.ExpHeadMasterEnt>();
            DataTable JODT = new DataTable();

            JODT = trackerdatamanager.GetViewList(VOUCHER_NO);
            if (JODT != null)
            {
                foreach (DataRow row in JODT.Rows)
                {
                    BE.ExpHeadMasterEnt JODTList = new BE.ExpHeadMasterEnt();
                    JODTList.DEbitRefernceno = Convert.ToString(row["VOUCHER_NO"]);
                    JODTList.FromDate = Convert.ToString(row["FROM_DATE"]);
                    JODTList.ToDate = Convert.ToString(row["TO_DATE"]);
                    JODTList.ExpensesHead = Convert.ToString(row["HeadName"]);
                    JODTList.Description = Convert.ToString(row["Descriptions"]);
                    JODTList.HSNCode = Convert.ToInt32(row["HSNCode"]);
                    JODTList.HSNCode1 = Convert.ToString(row["Unit"]);
                    JODTList.Netamount = Convert.ToString(row["Amount"]);
                    JODTList.CGST1 = Convert.ToInt32(row["D_CGST"]);
                    JODTList.SGST1 = Convert.ToInt32(row["D_SGST"]);
                    JODTList.IGST1 = Convert.ToInt32(row["D_IGST"]);
                    JODTList.Grandtotal = Convert.ToString(row["GrandTotal"]);
                    JODTList.TaxName = Convert.ToString(row["Tax_Code_ID"]);

                    JODTList.netDiscount = Convert.ToString(row["Discper"]);
                    JODTList.netDiscountS = Convert.ToString(row["DiscAmt"]);
                    JODTList.ddlparTDS = Convert.ToString(row["TDSPerID"]);
                    JODTList.tdsAmount = Convert.ToString(row["TDSAmt"]);
                    JOList.Add(JODTList);
                }
            }
            return JOList;
        }

        public List<BE.ExpHeadMasterEnt> GroupGetDDl()
        {
            List<BE.ExpHeadMasterEnt> ExpGroupDDL = new List<BE.ExpHeadMasterEnt>();
            DataTable EXPGroupDT = new DataTable();
            string Table = "expenses_group_m";
            string Column = "expenses_group_id,Group_name";
            string Condition = "";
            string OrderBy = "";

            EXPGroupDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (EXPGroupDT != null)
            {
                foreach (DataRow row in EXPGroupDT.Rows)
                {
                    BE.ExpHeadMasterEnt ExpGroupList = new BE.ExpHeadMasterEnt();
                    ExpGroupList.ExpGroupID = Convert.ToInt32(row["expenses_group_id"]);
                    ExpGroupList.ExpGroupName = Convert.ToString(row["Group_name"]);

                    ExpGroupDDL.Add(ExpGroupList);
                }
            }
            return ExpGroupDDL;
        }
        public List<BE.ExpHeadMasterEnt> SubEXPGetDll()
        {
            List<BE.ExpHeadMasterEnt> SubExpDDL = new List<BE.ExpHeadMasterEnt>();
            DataTable subexpdt = new DataTable();
            string Table = "expenses_sub_type_m";
            string Column = "isnull(Sub_Exp_Head_ID,0)Sub_Exp_Head_ID,Sub_Exp_Head";
            string Condition = "";
            string OrderBy = "Sub_Exp_Head";

            subexpdt = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (subexpdt != null)
            {
                foreach (DataRow row in subexpdt.Rows)
                {
                    BE.ExpHeadMasterEnt ExpsubList = new BE.ExpHeadMasterEnt();
                    ExpsubList.SubExpID = Convert.ToInt32(row["Sub_Exp_Head_ID"]);
                    ExpsubList.SubExpHead = Convert.ToString(row["Sub_Exp_Head"]);

                    SubExpDDL.Add(ExpsubList);
                }
            }
            return SubExpDDL;
        }
        public List<BE.ExpHeadMasterEnt> InvoiceTypeDDL()
        {
            List<BE.ExpHeadMasterEnt> InvoiceType = new List<BE.ExpHeadMasterEnt>();
            DataTable InvType = new DataTable();
            string Table = "import_invoicetype";
            string Column = "ID,Invoice_Type";
            string Condition = "";
            string OrderBy = "Invoice_Type";

            InvType = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (InvType != null)
            {
                foreach (DataRow row in InvType.Rows)
                {
                    BE.ExpHeadMasterEnt INVTypeList = new BE.ExpHeadMasterEnt();
                    INVTypeList.InvTId = Convert.ToInt32(row["ID"]);
                    INVTypeList.InvType = Convert.ToString(row["Invoice_Type"]);

                    InvoiceType.Add(INVTypeList);
                }
            }
            return InvoiceType;
        }
        public List<BE.ExpHeadMasterEnt> HSNCodeDDL()
        {
            List<BE.ExpHeadMasterEnt> HSNSelect = new List<BE.ExpHeadMasterEnt>();
            DataTable dataTable = new DataTable();
            string Table = "HSN_MASTER";
            string Column = "isnull(ID,0)ID,HSN_Code";
            string Condition = "";
            string OrderBy = "HSN_Code";

            dataTable = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpHeadMasterEnt HSNSelectlist = new BE.ExpHeadMasterEnt();
                    HSNSelectlist.HSNID = Convert.ToString(row["HSN_Code"]);
                    HSNSelectlist.HSNCodeL = Convert.ToString(row["HSN_Code"]);

                    HSNSelect.Add(HSNSelectlist);
                }
            }
            return HSNSelect;
        }

        public List<BE.ExpHeadMasterEnt> IMPGroupDDl()
        {
            List<BE.ExpHeadMasterEnt> IMPGroup = new List<BE.ExpHeadMasterEnt>();
            DataTable dataTable = new DataTable();
            string Table = "import_accgroups";
            string Column = "isnull(GroupID,0)GroupID,groupName";
            string Condition = "";
            string OrderBy = "groupName";

            dataTable = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    BE.ExpHeadMasterEnt IMPGLIst = new BE.ExpHeadMasterEnt();
                    IMPGLIst.IMPGID = Convert.ToInt32(row["GroupID"]);
                    IMPGLIst.IMPGName = Convert.ToString(row["groupName"]);

                    IMPGroup.Add(IMPGLIst);
                }
            }
            return IMPGroup;
        }

        public List<BE.CityMaster> GetState()
        {
            List<BE.CityMaster> ExpGroupDDL = new List<BE.CityMaster>();
            DataTable EXPGroupDT = new DataTable();
            string Table = "statemaster";
            string Column = "state_ID,State";
            string Condition = "";
            string OrderBy = "";

            EXPGroupDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (EXPGroupDT != null)
            {
                foreach (DataRow row in EXPGroupDT.Rows)
                {
                    BE.CityMaster ExpGroupList = new BE.CityMaster();
                    ExpGroupList.StateID = Convert.ToInt32(row["state_ID"]);
                    ExpGroupList.StateName = Convert.ToString(row["State"]);

                    ExpGroupDDL.Add(ExpGroupList);
                }
            }

            return ExpGroupDDL;
        }

        public List<BE.LockData> UserDetails()
        {
            List<BE.LockData> ExpGroupDDL = new List<BE.LockData>();
            DataTable EXPGroupDT = new DataTable();
            string Table = "UserDetails";
            string Column = "UserID,UserName";
            string Condition = "";
            string OrderBy = "";

            EXPGroupDT = trackerdatamanager.GetDropdownList(Table, Column, Condition, OrderBy);
            if (EXPGroupDT != null)
            {
                foreach (DataRow row in EXPGroupDT.Rows)
                {
                    BE.LockData ExpGroupList = new BE.LockData();
                    ExpGroupList.UserID = Convert.ToInt32(row["UserID"]);
                    ExpGroupList.UserName = Convert.ToString(row["UserName"]);

                    ExpGroupDDL.Add(ExpGroupList);
                }
            }

            return ExpGroupDDL;
        }

        public BE.ResponseMessage SaveUpdateOpeningDtl(BE.UpdateOpeningAmount fromData, int userid)
        {
            BE.ResponseMessage item = new BE.ResponseMessage();
            DataTable result = new DataTable();
            result = trackerdatamanager.SaveUpdateOpeningDtl(fromData, userid);
            if (result != null)
            {
                foreach (DataRow row in result.Rows)
                {
                    item.Status = Convert.ToInt32(row["Status"]);
                    item.Message = Convert.ToString(row["message"]);
                }
            }
            return item;
        }

        public BE.ResponseMessage SaveHoldDtls(BE.UpdateOpeningAmount fromData, int userid)
        {
            BE.ResponseMessage item = new BE.ResponseMessage();
            DataTable result = new DataTable();
            result = trackerdatamanager.SaveHoldDtls(fromData, userid);
            if (result != null)
            {
                foreach (DataRow row in result.Rows)
                {
                    item.Status = Convert.ToInt32(row["Status"]);
                    item.Message = Convert.ToString(row["message"]);
                }
            }
            return item;
        }
    }
}
