using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerMVCEntities.BusinessEntities;
using BE = TrackerMVCEntities.BusinessEntities;
using DL = TrackerMVCDataLayer;



namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.DomesticInventoryManagement
{
    public class InventoryManagementDataProvider
    {
        DL.TrackerMVCDBManager IMDBManager = new DL.TrackerMVCDBManager();

        //DL.InventoryManagementDBManager IMDBManager = new DL.InventoryManagementDBManager();

        public List<ContainerStockM> GetAutoContainerNoList(string prefixText, string type)
        {
            try
            {
                List<BE.ContainerStockM> containerDL = new List<BE.ContainerStockM>();
                DataTable contaiDT = new DataTable();
                contaiDT = IMDBManager.GetAutoContainerNoList(prefixText, type);
                if (contaiDT != null)
                {
                    if (contaiDT.Rows.Count > 0)
                    {
                        foreach (DataRow row in contaiDT.Rows)
                        {
                            BE.ContainerStockM containerNoL = new BE.ContainerStockM();
                            containerNoL.EntryID = Convert.ToInt32(row["EntryID"]);
                            containerNoL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                            containerNoL.TransMadeAgainst = Convert.ToString(row["TransMadeAgainst"]);
                            containerNoL.IsAvailable = Convert.ToBoolean(row["IsAvailable"]);
                            containerDL.Add(containerNoL);
                        }
                    }
                    else
                    {
                        BE.ContainerStockM containerNoL = new BE.ContainerStockM();
                        containerNoL.ContainerNo = "No Data Found";
                        containerDL.Add(containerNoL);
                    }
                }
                return containerDL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContainerStockM> GetContainerStockList(int deptID, string title)
        {
            try
            {
                List<BE.ContainerStockM> deptDL = new List<BE.ContainerStockM>();
                DataTable dt = new DataTable();
                dt = IMDBManager.GetContainerStockList(deptID, title);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        BE.ContainerStockM stockList = new BE.ContainerStockM();
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

        public List<ContainerStockM> GetDeptList()
        {
            try
            {
                List<BE.ContainerStockM> deptDL = new List<BE.ContainerStockM>();
                DataTable dt = new DataTable();
                string Table = "DeptM";
                string Column = "Distinct ISNULL(DeptID,0) DeptID,ISNULL(DeptName,0) DeptName";
                string Condition = "IsActive = 1";
                string OrderBy = "DeptName";

                dt = IMDBManager.GetDropdownListDomestic(Table, Column, Condition, OrderBy);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        BE.ContainerStockM deptList = new BE.ContainerStockM();
                        deptList.DeptID = Convert.ToInt32(row["DeptID"]);
                        deptList.DeptName = Convert.ToString(row["DeptName"]);
                        deptDL.Add(deptList);
                    }
                }
                return deptDL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContainerStockM GetDetailsAgainstContainerNo(string containerNo)
        {
            try
            {
                BE.ContainerStockM deptDL = new BE.ContainerStockM();
                DataTable dt = new DataTable();
                string Table = "ContainerIN";
                string Column = "Distinct ISNULL(Size,0) Size,ISNULL(Type,'') Type, ISNULL(VendorName,'') VendorName ";
                string Condition = "IsActive = 1 AND ContainerNo = '" + containerNo + "'";
                string OrderBy = "";

                dt = IMDBManager.GetDropdownListDomestic(Table, Column, Condition, OrderBy);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        deptDL.Size = Convert.ToInt32(row["Size"]);
                        deptDL.Type = Convert.ToString(row["Type"]);
                        deptDL.VendorName = Convert.ToString(row["VendorName"]);
                    }
                }
                return deptDL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContainerStockM GetDetailsAgainstContainerNoForReturn(string containerNo)
        {
            try
            {
                BE.ContainerStockM deptDL = new BE.ContainerStockM();
                DataTable dt = new DataTable();
                dt = IMDBManager.GetDetailsAgainstContainerNoForReturn(containerNo);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        deptDL.Size = Convert.ToInt32(row["Size"]);
                        deptDL.DeptID = Convert.ToInt32(row["DeptID"]);
                        deptDL.DeptName = Convert.ToString(row["DeptName"]);
                        deptDL.ContactPerson = Convert.ToString(row["ContactPerson"]);
                        deptDL.ContactNo = Convert.ToString(row["ContactNo"]);
                        deptDL.Type = Convert.ToString(row["Type"]);
                        deptDL.VendorName = Convert.ToString(row["VendorName"]);
                        deptDL.ContainerNo = Convert.ToString(row["ContainerNo"]);
                        deptDL.Remarks = Convert.ToString(row["Remarks"]);
                    }
                }
                return deptDL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContainerStockM GetDetailsAgainstDept(int DeptID)
        {
            try
            {
                BE.ContainerStockM deptDL = new BE.ContainerStockM();
                DataTable dt = new DataTable();
                string Table = "DeptM";
                string Column = "Distinct ISNULL(ContactPerson,'') ContactPerson,ISNULL(ContactNo,'') ContactNo ";
                string Condition = "IsActive = 1 AND DeptID = '" + DeptID + "'";
                string OrderBy = "";

                dt = IMDBManager.GetDropdownListDomestic(Table, Column, Condition, OrderBy);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        deptDL.ContactPerson = Convert.ToString(row["ContactPerson"]);
                        deptDL.ContactNo = Convert.ToString(row["ContactNo"]);
                    }
                }
                return deptDL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContainerStockM> GetStockDetails()
        {
            try
            {
                List<BE.ContainerStockM> stockDL = new List<BE.ContainerStockM>();
                DataTable stockDT = new DataTable();
                stockDT = IMDBManager.GetStockDetails();
                if (stockDT != null)
                {
                    if (stockDT.Rows.Count > 0)
                    {
                        foreach (DataRow row in stockDT.Rows)
                        {
                            BE.ContainerStockM stockList = new BE.ContainerStockM();
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

        public List<ContainerStockM> GetSummarydetails(DateTime fromDate, DateTime toDate, string type)
        {
            try
            {
                List<BE.ContainerStockM> stockDL = new List<BE.ContainerStockM>();
                DataTable stockDT = new DataTable();
                stockDT = IMDBManager.GetSummarydetails(fromDate, toDate, type);
                if (stockDT != null)
                {
                    if (stockDT.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow row in stockDT.Rows)
                        {
                            BE.ContainerStockM stockList = new BE.ContainerStockM();
                            i = i + 1;
                            stockList.SrNo = i;
                            stockList.TransID = Convert.ToInt32(row["TransID"]);
                            stockList.TransID = Convert.ToInt32(row["TransID"]);
                            stockList.TransMadeAgainst = Convert.ToString(row["TransMadeAgainst"]);
                            stockList.DisplayDate = Convert.ToString(row["Date"]);
                            stockList.DeptName = Convert.ToString(row["DeptName"]);
                            stockList.ContainerNo = Convert.ToString(row["ContainerNo"]);
                            stockList.Size = Convert.ToInt32(row["Size"]);
                            stockList.Type = Convert.ToString(row["Type"]);
                            stockList.VendorName = Convert.ToString(row["VendorName"]);
                            stockList.Remarks = Convert.ToString(row["Remarks"]);
                            stockList.DisplayAddedOn = Convert.ToString(row["DisplayAddedOn"]);
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

        public ResponseMessage SaveIssuedContainer(ContainerStockM containerIssueList)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            try
            {
                DataTable data = new DataTable();
                data = IMDBManager.SaveIssuedContainer(containerIssueList);
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        message.Message = Convert.ToString(row["message"]);
                        message.Status = Convert.ToInt32(row["Status"]);
                    }
                }
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.Message = ex.Message;
            }
            return message;
        }

        public ResponseMessage SaveNewContainerIN(ContainerStockM containerINList)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            try
            {
                DataTable data = new DataTable();
                data = IMDBManager.SaveNewContainerIN(containerINList);
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        message.Message = Convert.ToString(row["message"]);
                        message.Status = Convert.ToInt32(row["Status"]);
                    }
                }
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.Message = ex.Message;
            }
            return message;
        }

        public ResponseMessage SaveReturnedContainer(ContainerStockM containerReturnList)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            try
            {
                DataTable data = new DataTable();
                data = IMDBManager.SaveReturnedContainer(containerReturnList);
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        message.Message = Convert.ToString(row["message"]);
                        message.Status = Convert.ToInt32(row["Status"]);
                    }
                }
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.Message = ex.Message;
            }
            return message;
        }

        public ResponseMessage SaveSoldContainer(ContainerStockM containerSoldList)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            try
            {
                DataTable data = new DataTable();
                data = IMDBManager.SaveSoldContainer(containerSoldList);
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        message.Message = Convert.ToString(row["message"]);
                        message.Status = Convert.ToInt32(row["Status"]);
                    }
                }
            }
            catch (Exception ex)
            {
                message.Status = 0;
                message.Message = ex.Message;
            }
            return message;
        }
    }
}
