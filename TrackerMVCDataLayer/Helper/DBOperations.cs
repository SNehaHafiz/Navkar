using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;
using DB = TrackerMVCDBConnector;

namespace TrackerMVCDataLayer.Helper
{
    public class DBOperations
    {
        SqlCommand command;
        SqlConnection connection;


        //start : creates a new DB connection
        private void sub_ConnectDB()
        {

            DB.DBConnection objConn = new DB.DBConnection();
            //string connetionString = null;
            connection = objConn.GetConnection;
            // connection = new SqlConnection(connetionString);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    connection = objConn.GetConnection;
                    connection.Open();

                }
            }
        }

        public DataTable sub_GetDatatable(string strSQL)
        {
            DataTable dt = new DataTable();


            try
            {
                sub_ConnectDB();
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                SqlCommand objCommand = new SqlCommand(strSQL, connection);
                SqlDataAdapter da = new SqlDataAdapter();
                objCommand.CommandTimeout = 0;
                da.SelectCommand = objCommand;
                da.Fill(dt);
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

            }

            catch(Exception ex)
            {
            }
            return dt;
        }

        public DataSet sub_GetDataSets(string strSQL)
        {
            DataSet ds = new DataSet();


            try
            {
                sub_ConnectDB();
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                SqlCommand objCommand = new SqlCommand(strSQL, connection);
                SqlDataAdapter da = new SqlDataAdapter();
                objCommand.CommandTimeout = 0;
                da.SelectCommand = objCommand;
                da.Fill(ds);
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

            }

            catch
            {
            }
            return ds;
        }

        public int sub_ExecuteNonQuery(string strSQL)
        {
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return i;
        }

        public DataTable AddDataJobOrderData(long JONo,int AgentID, int SLID, long shipperid, long Importerid, long Chaid, String ViaNo, int VesselID, int PortID, DateTime berthingdate, int Haulage_Type_Id,
           

            int File_Status_Id, int Transport_Type_Id, int POL_ID, int Sales_Person_Id, string BLNumber, DateTime BLReceivedDate, int UserId, string HouseBLNumber,int KAMID,string JoRemarks,int JoTypeId,int FileId,string IGMNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string berthingdateformat = berthingdate.ToString("yyyy-MM-dd HH:mm");
                string bLReceivedDateformat = BLReceivedDate.ToString("yyyy-MM-dd HH:mm");

                if (IGMNo == null)
                {
                    IGMNo = "";
                }
                if (berthingdateformat == "0001-01-01 00:00")
                {
                    berthingdateformat = "1900-01-01 00:00:00.000";
                    //  berthingdateformat = null;

                }

                if (bLReceivedDateformat == "0001-01-01 00:00")
                {
                    bLReceivedDateformat = "1900-01-01 00:00:00.000";
                    //  berthingdateformat = null;

                }

                string HBL = HouseBLNumber;
                sub_ConnectDB();
               

                using (SqlCommand cmd = new SqlCommand("USP_INSERT_JOBORDERM", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JonoNew", JONo);
                    cmd.Parameters.AddWithValue("@AgentID", AgentID);
                    cmd.Parameters.AddWithValue("@SLID", SLID);
                    cmd.Parameters.AddWithValue("@shipperid", shipperid);
                    cmd.Parameters.AddWithValue("@Importerid", Importerid);
                    cmd.Parameters.AddWithValue("@Chaid", Chaid);
                    
                    //  cmd.Parameters.AddWithValue("@ViaNo", Convert.ToString(ViaNo));
                    if (ViaNo != null)
                    {
                        cmd.Parameters.AddWithValue("@ViaNo", Convert.ToString(ViaNo));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ViaNo", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@VesselID", VesselID);
                    cmd.Parameters.AddWithValue("@PortID", Convert.ToInt32(PortID));
                    cmd.Parameters.AddWithValue("@File_Status_Id", File_Status_Id);
                    cmd.Parameters.AddWithValue("@berthingdate", Convert.ToDateTime(berthingdateformat).ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Haulage_Type_Id", Haulage_Type_Id);
                    cmd.Parameters.AddWithValue("@Transport_Type_Id", Transport_Type_Id);
                    cmd.Parameters.AddWithValue("@POL_ID", POL_ID);
                    cmd.Parameters.AddWithValue("@Sales_Person_Id", Sales_Person_Id);
                    cmd.Parameters.AddWithValue("@BLNumber", BLNumber);
                    cmd.Parameters.AddWithValue("@BLReceivedDate", Convert.ToDateTime(bLReceivedDateformat).ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@ADDEDBY", UserId);

                    cmd.Parameters.AddWithValue("@HouseBLNumber", Convert.ToString(HouseBLNumber));
                    cmd.Parameters.AddWithValue("@KAM", Convert.ToInt32(KAMID));
                    cmd.Parameters.AddWithValue("@joRemarks", Convert.ToString(JoRemarks));
                    cmd.Parameters.AddWithValue("@JoTypeId", Convert.ToInt32(JoTypeId));
                    cmd.Parameters.AddWithValue("@FIleId", Convert.ToInt32(FileId));
                    cmd.Parameters.AddWithValue("@IGMNo", Convert.ToString(IGMNo));
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                string error="Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + JONo;
               // AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);
                sub_ExecuteNonQuery("Insert into ErrorLog (Errorlog) values (  '" + error + "')");
            }
            return dt;
        }
        public int SaveBTTCARGO(string SP_Name, Dictionary<object, object> lstparam)
        {
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;

                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return i;
        }
        public int DomesticTypeTableData(string SP_Name, Dictionary<object, object> lstparam, DataTable dt, string typetable, string parameter)
        {
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = parameter;
                    param.Value = dt;
                    param.TypeName = typetable;
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }
                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;


                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return i;
        }
        public DataTable SaveExpCargoGP(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
        {

            DataTable dt1 = new DataTable();
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {

                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@PT_SaveExpGPDetails";
                    param.Value = dt;
                    param.TypeName = "PT_SaveExpGPDetails";
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }


                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt1);

            }
            catch (Exception ex)
            {
            }
            return dt1;
        }

        public int SaveMovementAccCTR(string SP_Name, Dictionary<object, object> lstparam)
        {
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;

                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return i;
        }

        public DataTable SaveSBWiseMovementAcceptEntry(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
        {

            DataTable dt1 = new DataTable();
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {

                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@PT_SaveSBWiseMovement";
                    param.Value = dt;
                    param.TypeName = "PT_SaveSBWiseMovement";
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }


                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt1);

            }
            catch (Exception ex)
            {
            }
            return dt1;

        }
        public int AddJobOrderDetailsList(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
        {
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
                    ////param.ParameterName = "@"+item.Key ;
                    ////param.Value = item.Value;
                    //param.SqlDbType = SqlDbType.Int;
                    //cmd.Parameters.Add(param );
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@PT_JobOrderDetails";
                    param.Value = dt;
                    param.TypeName = "PT_JobOrderDetails";
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }
                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return i;
        }

        public string insLoloData(Dictionary<object, object> lstparam, DataTable dt)
        {
            sub_ConnectDB();
            SqlCommand cmd = new SqlCommand();
            //create object of SqlBulkCopy which help to insert
            //SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection);
            //// assign Destination table name
            //sqlBulkCopy.DestinationTableName = "Temp_WHL_LOLO";
            //sqlBulkCopy.ColumnMappings.Add("Containerno", "ContainerNo");
            //sqlBulkCopy.ColumnMappings.Add("size", "Size");
            //sqlBulkCopy.ColumnMappings.Add("ContainerType", "ContainerType");
            //sqlBulkCopy.ColumnMappings.Add("ActivityDate", "ActivityDate");
            //sqlBulkCopy.ColumnMappings.Add("Activity", "Activity");

            //sqlBulkCopy.WriteToServer(dt);
            //connection.Close();

            return "";
        }


        public DataSet AddImportData(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
        {
            DataSet Containerdt = new DataSet();
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
                    ////param.ParameterName = "@"+item.Key ;
                    ////param.Value = item.Value;
                    //param.SqlDbType = SqlDbType.Int;
                    //cmd.Parameters.Add(param );
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@ContainerDT";
                    param.Value = dt;
                    param.TypeName = "ContainerDT";
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }
                
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(Containerdt);
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);
              //  i = cmd.ExecuteNonQuery();
               
            }

            catch (Exception ex)
            {
            }
            return Containerdt;
        }

        public DataSet AddLoloDataDt(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
        {
            DataSet Containerdt = new DataSet();
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@Temp_WHL_LOLO";
                    param.Value = dt;
                    param.TypeName = "Temp_WHL_LOLO";
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }

                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(Containerdt);
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);
                //  i = cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
            }
            return Containerdt;
        }

        public DataTable AddAttachment(int Userid, byte[] bytes, string contentType, string fname)
        {

            sub_ConnectDB();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("USP_AddTempAttachment", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DOCFILENAME", fname);
                cmd.Parameters.AddWithValue("@DATA", bytes);
                cmd.Parameters.AddWithValue("@CONTENTTYPE", contentType);
                cmd.Parameters.AddWithValue("@UNIQUEID", Userid);
               

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            return dt;
        }

        public DataTable AddAttachment1(string Description, string MailID, string strAttachement, int companyid, string contentType, long Userid,int TicketNumber)
        {

            sub_ConnectDB();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("USP_CompanyTicketInsertDetailss", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@MailID", MailID);
                cmd.Parameters.AddWithValue("@Attachment", strAttachement);
                cmd.Parameters.AddWithValue("@companyid", companyid);
                cmd.Parameters.AddWithValue("@contentType", contentType);
                cmd.Parameters.AddWithValue("@userid", Userid);
                cmd.Parameters.AddWithValue("@countx",TicketNumber);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            return dt;
        }

        public int UpdateTrailerGrade(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
        {
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
                    ////param.ParameterName = "@"+item.Key ;
                    ////param.Value = item.Value;
                    //param.SqlDbType = SqlDbType.Int;
                    //cmd.Parameters.Add(param );
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@PT_TrailerGrade";
                    param.Value = dt;
                    param.TypeName = "PT_TrailerGrade";
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }
                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return i;
        }

        public int UpdateTrailerTrolley(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
        {
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
                    ////param.ParameterName = "@"+item.Key ;
                    ////param.Value = item.Value;
                    //param.SqlDbType = SqlDbType.Int;
                    //cmd.Parameters.Add(param );
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@PT_TrailerTrolley";
                    param.Value = dt;
                    param.TypeName = "PT_TrailerTrolley";
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }
                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return i;
        }

        public int AddMasterData(string Name, string code, string address, string contactPer, string contactDesi, string cont1,
            string cont2, string email, string website, string grade, Boolean chkContract, string remarks, int userId,
            Boolean CHA, Boolean Customer, Boolean shipper, Boolean shippline, Boolean importer, string cellno, string faxno, Boolean isactive, Int64 id, string TalleyMaster, Boolean JV,Boolean Vendor)
        {
            int i = 0;
            sub_ConnectDB();
            DataTable dt = new DataTable();

          //  string adrress = address.Replace("'", "''"); ;
            using (SqlCommand cmd = new SqlCommand("USP_UpdateMasterData", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@AGAID", code);
                cmd.Parameters.AddWithValue("@AGNAME", Name);
                cmd.Parameters.AddWithValue("@AGADDRESS", address);
                cmd.Parameters.AddWithValue("@AGAUTHPERSON", contactPer);
                cmd.Parameters.AddWithValue("@AGAUTHPERSONDESIG", contactDesi);
                cmd.Parameters.AddWithValue("@AGTELI", cont1);
                cmd.Parameters.AddWithValue("@AGTELII", cont2);
                cmd.Parameters.AddWithValue("@AGFAX", faxno);
                cmd.Parameters.AddWithValue("@AGCELLNO", cellno);
                cmd.Parameters.AddWithValue("@AGEMAIL", email);
                cmd.Parameters.AddWithValue("@AGWEBSITE", website);
                cmd.Parameters.AddWithValue("@AGREMARKS", remarks);
                cmd.Parameters.AddWithValue("@AGGRADE", grade);
                cmd.Parameters.AddWithValue("@ISCONTRACT", chkContract);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@CHA", CHA);
                cmd.Parameters.AddWithValue("@Customer", Customer);
                cmd.Parameters.AddWithValue("@Shipper", shipper);
                cmd.Parameters.AddWithValue("@Shipline", shippline);
                cmd.Parameters.AddWithValue("@Importer", importer);
                cmd.Parameters.AddWithValue("@isactive", isactive);
                cmd.Parameters.AddWithValue("@TallyLedger", TalleyMaster);
                cmd.Parameters.AddWithValue("@Vendor", Vendor);


                //cmd.CommandText = SP_Name;
                //cmd.Connection = connection;
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);
                i = cmd.ExecuteNonQuery();


                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);

               // da.Fill(dt);
                
              // cmd.ExecuteNonQuery();

                //////SqlDataAdapter da = new SqlDataAdapter();
                //////da.SelectCommand = cmd;
                
                
                ////////SqlDataAdapter da = new SqlDataAdapter();
                ////////da.SelectCommand = cmd;
                ////////da.Fill(ds);
               
                //////da.Fill(dt);

            }
            return i;
        }


        public int UpdateBLPOD(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
        {
            DataTable Containerdt = new DataTable();
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
                    ////param.ParameterName = "@"+item.Key ;
                    ////param.Value = item.Value;
                    //param.SqlDbType = SqlDbType.Int;
                    //cmd.Parameters.Add(param );
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@PT_PODETAData";
                    param.Value = dt;
                    param.TypeName = "PT_PODETAData";
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }
                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                i = cmd.ExecuteNonQuery();

                //cmd.CommandText = SP_Name;
                //cmd.Connection = connection;
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(Containerdt);
            }
            catch (Exception ex)
            {
            }
            return i;
        }


        public int UpdateTrainDeparture(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
        {
            DataTable Containerdt = new DataTable();
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
                    ////param.ParameterName = "@"+item.Key ;
                    ////param.Value = item.Value;
                    //param.SqlDbType = SqlDbType.Int;
                    //cmd.Parameters.Add(param );
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@PT_DepartureDate";
                    param.Value = dt;
                    param.TypeName = "PT_DepartureDate";
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }
                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                i = cmd.ExecuteNonQuery();

                //cmd.CommandText = SP_Name;
                //cmd.Connection = connection;
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(Containerdt);
            }
            catch (Exception ex)
            {
            }
            return i;
        }

        public int UpdateDischargeDate(string SP_Name, Dictionary<object, object> lstparam, DataTable dt,string typetable,string parameter)
        {
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
                    ////param.ParameterName = "@"+item.Key ;
                    ////param.Value = item.Value;
                    //param.SqlDbType = SqlDbType.Int;
                    //cmd.Parameters.Add(param );
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = parameter;
                    param.Value = dt;
                    param.TypeName = typetable;
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }
                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return i;
        }

        // Code By Arti /

public int SaveContainerList(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
{
int i = 0;
try
{
sub_ConnectDB();
SqlCommand cmd = new SqlCommand();
cmd.CommandType = CommandType.StoredProcedure;
foreach (var item in lstparam)
{
cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
}
if (dt != null)
{
SqlParameter param = new SqlParameter();
param.ParameterName = "@PT_ContainerDetails";
param.Value = dt;
param.TypeName = "PT_ContainerDetails";
param.SqlDbType = SqlDbType.Structured;
cmd.Parameters.Add(param);
}
DataSet ds = new DataSet();
cmd.CommandText = SP_Name;
cmd.Connection = connection;

i = cmd.ExecuteNonQuery();
}
catch (Exception ex)
{
}
return i;
}

public int AddTypeTableData(string SP_Name, Dictionary<object, object> lstparam, DataTable dt, string typetable, string parameter)
{
    int i = 0;
    try
    {
        sub_ConnectDB();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        foreach (var item in lstparam)
        {
            //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
            ////param.ParameterName = "@"+item.Key ;
            ////param.Value = item.Value;
            //param.SqlDbType = SqlDbType.Int;
            //cmd.Parameters.Add(param );
            cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
        }
       
        
        if (dt != null)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameter;
            param.Value = dt;
            param.TypeName = typetable;
            param.SqlDbType = SqlDbType.Structured;
            cmd.Parameters.Add(param);
        }
       
       DataSet ds = new DataSet();
       cmd.CommandText = SP_Name;
       cmd.Connection = connection;

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);
                cmd.CommandTimeout = 100;
                i = cmd.ExecuteNonQuery();
                
                //if(i>0)
                //{
                //    if (cmd.Parameters.it["nn"].Value >0)
                //    {

                //    }
            }
    catch (Exception ex)
    {
    }
    return i;
}

        public int AddTypeTableDataIgm(string SP_Name, Dictionary<object, object> lstparam, DataTable dt, string typetable, string parameter)
        {
            int i = 0;
            int fileid = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                SqlParameter outScore = new SqlParameter("@FILEID", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outScore);

                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = parameter;
                    param.Value = dt;
                    param.TypeName = typetable;
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }

                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                cmd.CommandTimeout = 100;
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);

                i = cmd.ExecuteNonQuery();
                try
                {
                    fileid = Convert.ToInt32(outScore.Value);
                }
                catch (Exception ex) { }
            }
            catch (Exception ex)
            {
            }
            return fileid;
        }



        // Code By Arti /

        ///by durga///
        public DataSet AddTypeRoadPlanningTableData(string SP_Name, Dictionary<object, object> lstparam, DataTable dt, string typetable, string parameter)
{
    DataSet ds = new DataSet();
    try
    {
        sub_ConnectDB();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        foreach (var item in lstparam)
        {
            //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
            ////param.ParameterName = "@"+item.Key ;
            ////param.Value = item.Value;
            //param.SqlDbType = SqlDbType.Int;
            //cmd.Parameters.Add(param );
            cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
        }
        if (dt != null)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameter;
            param.Value = dt;
            param.TypeName = typetable;
            param.SqlDbType = SqlDbType.Structured;
            cmd.Parameters.Add(param);
        }

        cmd.CommandText = SP_Name;
        cmd.Connection = connection;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        cmd.CommandTimeout = 0;
        da.Fill(ds);
        //SqlDataAdapter da = new SqlDataAdapter();
        //da.SelectCommand = cmd;
        //da.Fill(ds);
        //  i = cmd.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
    }
    return ds;
}

        ///end by durga///
public int SaveLocationDetails(string SP_Name, Dictionary<object, object> lstparam)
{
    int i = 0;
    try
    {
        sub_ConnectDB();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        foreach (var item in lstparam)
        {
            cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
        }

        DataSet ds = new DataSet();
        cmd.CommandText = SP_Name;
        cmd.Connection = connection;

        i = cmd.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
    }
    return i;
}

public DataTable DataTableAddTypeTable(string SP_Name, Dictionary<object, object> lstparam, DataTable dt1, string typetable, string parameter)
{

    DataTable dt = new DataTable();
    try
    {
        sub_ConnectDB();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        foreach (var item in lstparam)
        {
            //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
            ////param.ParameterName = "@"+item.Key ;
            ////param.Value = item.Value;
            //param.SqlDbType = SqlDbType.Int;
            //cmd.Parameters.Add(param );
            cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
        }
        if (dt1 != null)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameter;
            param.Value = dt1;
            param.TypeName = typetable;
            param.SqlDbType = SqlDbType.Structured;
            cmd.Parameters.Add(param);
        }

        cmd.CommandText = SP_Name;
        cmd.Connection = connection;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        //SqlDataAdapter da = new SqlDataAdapter();
        //da.SelectCommand = cmd;
        //da.Fill(ds);
        //  i = cmd.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
    }
    return dt;

}

public int AddPortTypeTableData(string SP_Name, Dictionary<object, object> lstparam, DataTable dt, string typetable, string parameter)
{
    int i = 0;
    try
    {
        sub_ConnectDB();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        foreach (var item in lstparam)
        {
            //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
            ////param.ParameterName = "@"+item.Key ;
            ////param.Value = item.Value;
            //param.SqlDbType = SqlDbType.Int;
            //cmd.Parameters.Add(param );
            cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
        }
        if (dt != null)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameter;
            param.Value = dt;
            param.TypeName = typetable;
            param.SqlDbType = SqlDbType.Structured;
            cmd.Parameters.Add(param);
        }
        DataSet ds = new DataSet();
        cmd.CommandText = SP_Name;
        cmd.Connection = connection;

        //SqlDataAdapter da = new SqlDataAdapter();
        //da.SelectCommand = cmd;
        //da.Fill(ds);

        //SqlDataAdapter da = new SqlDataAdapter();
        //da.SelectCommand = cmd;
        //da.Fill(ds);
        i = cmd.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
    }
    return i;
}


        public DataTable AddTPAttachment(string TpNo, string startdate,string enddate, byte[] attachByte,string contentType)
        {

            sub_ConnectDB();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("USP_UpdateTPCloseDetails", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tpno", TpNo);
                cmd.Parameters.AddWithValue("@startdate", startdate);
                cmd.Parameters.AddWithValue("@enddate", enddate);
                cmd.Parameters.AddWithValue("@Attachment", attachByte);
                cmd.Parameters.AddWithValue("@Contenttype", contentType);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            return dt;
        }

        public DataSet AddTypeTableDataSplit(string SP_Name, Dictionary<object, object> lstparam, DataSet ds, Dictionary<object, object> lstparam2)
        {
            int i = 0;
            int fileid = 0;
            DataSet ds1 = new DataSet();
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                
                int count = 0;
                if (ds != null)
                {
                    foreach (var item in lstparam2)
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = item.Value.ToString();
                        param.Value = ds.Tables[count];
                        param.TypeName = item.Key.ToString();
                        param.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(param);
                        count = count + 1;
                    }
                }
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds1);
                //i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return ds1;
        }

        public int AddInvoiceData(string SP_Name, Dictionary<object, object> lstparam, DataSet ds, Dictionary<object, object> lstparam2)
        {
            int i = 0;
            int RetReceiptNo = 0;
            DataTable dt = new DataTable();
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                SqlParameter ReceiptNo = new SqlParameter("@RECEIPTNO", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(ReceiptNo);
                int count = 0;
                if (ds != null)
                {
                    foreach (var item in lstparam2)
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = item.Value.ToString();
                        param.Value = ds.Tables[count];
                        param.TypeName = item.Key.ToString();
                        param.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(param);
                        count = count + 1;
                    }
                }
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds1);
                i = cmd.ExecuteNonQuery();
                try
                {
                    RetReceiptNo = Convert.ToInt32(ReceiptNo.Value);
                }
                catch (Exception ex) { }
            }
            catch (Exception ex)
            {
            }
            return RetReceiptNo;
        }

        public Dictionary<object,object> AddCreditData(string SP_Name, Dictionary<object, object> lstparam, DataTable dt, Dictionary<object, object> lstparam2)
        {
            Dictionary<object, object> OutPut = new Dictionary<object, object>();
            
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                SqlParameter TransId = new SqlParameter("@TransId", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };

                cmd.Parameters.Add(TransId);
                cmd.Parameters.Add(Message);

                if (dt != null)
                {
                    foreach (var item in lstparam2)
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = item.Value.ToString();
                        param.Value = dt;
                        param.TypeName = item.Key.ToString();
                        param.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(param);
                    }
                }

                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                i = cmd.ExecuteNonQuery();
                try
                {
                    if(Message.Value.ToString() != "")
                    {
                        OutPut.Add("Message",Message.Value.ToString());
                        OutPut.Add("TransNo", 0);
                    }
                    else
                    {
                        OutPut.Add("Message", "");
                        OutPut.Add("TransNo", Convert.ToInt32(TransId.Value));
                    }
                }
                catch (Exception ex) {
                    OutPut.Add("Message", ex.Message.ToString());
                    OutPut.Add("TransNo",0);
                }
            }
            catch (Exception ex)
            {
                OutPut.Add("Message", ex.Message.ToString());
                OutPut.Add("TransNo", 0);
            }
            return OutPut;
        }

        public DataSet AddAuctionData(string SP_Name, Dictionary<object, object> lstparam, DataTable ds, Dictionary<object, object> lstparam2)
        {
            DataSet dt = new DataSet();
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                if (ds != null)
                {
                    foreach(var item in lstparam2)
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = item.Value.ToString();
                        param.Value = ds;
                        param.TypeName = item.Key.ToString();
                        param.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(param);
                    }
                }
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                //i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public int AddBCNWagonCLPData(string SP_Name, Dictionary<object, object> lstparam, DataTable ds, Dictionary<object, object> lstparam2)
        {
            DataSet dt = new DataSet();
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                if (ds != null)
                {
                    foreach (var item in lstparam2)
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = item.Value.ToString();
                        param.Value = ds;
                        param.TypeName = item.Key.ToString();
                        param.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(param);
                    }
                }
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(dt);
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return i;
        }

        public DataSet AddAuctionAutonData(string SP_Name, Dictionary<object, object> lstparam)
        {
            DataSet dt = new DataSet();
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                cmd.CommandTimeout = 30;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                //i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public int AddAuctionStatus(string SP_Name, Dictionary<object, object> lstparam)
        {
            int i = 0;
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return i;
        }

        public DataTable AddDriverAttachment(int Userid, byte[] bytes, string contentType, string fname, string root)
        {

            sub_ConnectDB();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("USP_AddDriverTempAttachment", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DOCFILENAME", fname);
                cmd.Parameters.AddWithValue("@DATA", bytes);
                cmd.Parameters.AddWithValue("@CONTENTTYPE", contentType);
                cmd.Parameters.AddWithValue("@UNIQUEID", Userid);
                cmd.Parameters.AddWithValue("@FILEPATH", root);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            return dt;
        }

        public int AddDataRRDownloadSaveData(long FileId, string TrainNo,long FromStation,long ToStation, long CommodityId, long PartyId, string WagonRRNo, DateTime ArrivalDate, double FreightAmount, long UserId,string WagonContRRNo)
        {
            int retVal = 0;
            try
            {
                string berthingdateformat = ArrivalDate.ToString("yyyy-MM-dd HH:mm");

                if (TrainNo == null)
                {
                    TrainNo = "";
                }

                if (WagonRRNo == null)
                {
                    WagonRRNo = "";
                }

                if (WagonContRRNo == null)
                {
                    WagonContRRNo = "";
                }

                if (berthingdateformat == "0001-01-01 00:00")
                {
                    berthingdateformat = "1900-01-01 00:00:00.000";
                    //  berthingdateformat = null;

                }

                sub_ConnectDB();


                using (SqlCommand cmd = new SqlCommand("USP_InsRRDownloadData", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FileId", FileId);
                    cmd.Parameters.AddWithValue("@TrainNo", TrainNo);
                    cmd.Parameters.AddWithValue("@FromStation", FromStation);
                    cmd.Parameters.AddWithValue("@ToStation", ToStation);
                    cmd.Parameters.AddWithValue("@CommodityId", CommodityId);
                    cmd.Parameters.AddWithValue("@PartyId", PartyId);
                    cmd.Parameters.AddWithValue("@WagonRRNo", WagonRRNo);
                    cmd.Parameters.AddWithValue("@ArrivalDate", berthingdateformat);
                    cmd.Parameters.AddWithValue("@FreightAmount", FreightAmount);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserId);
                    cmd.Parameters.AddWithValue("@WagonContRRNo", WagonContRRNo);

                    retVal = cmd.ExecuteNonQuery();
                    //SqlDataAdapter da = new SqlDataAdapter();
                    //da.SelectCommand = cmd;
                    //da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                string error = "Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + TrainNo;
                sub_ExecuteNonQuery("Insert into ErrorLog (Errorlog) values (  '" + error + "')");
            }
            return retVal;
        }

        public string AddTypeTableDataTariff(string SP_Name, Dictionary<object, object> lstparam, DataTable dt, string typetable, string parameter)
        {
            string retVal = "";
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                SqlParameter outScore = new SqlParameter("@ErrMsg", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outScore);

                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = parameter;
                    param.Value = dt;
                    param.TypeName = typetable;
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }

                DataSet ds = new DataSet();
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                if(outScore.Value.ToString() != "")
                {
                    retVal = outScore.Value.ToString();
                }
                else
                {
                    retVal = "Record Save Successfully.";
                }

            }
            catch (Exception ex)
            {
                retVal = ex.Message.ToString();
            }
            return retVal;
        }

        public DataTable AddDataJobOrderDatas(long JONo, int AgentID, int SLID, long shipperid, long Importerid, long Chaid, String ViaNo, int VesselID, int PortID, DateTime berthingdate, int Haulage_Type_Id,


int File_Status_Id, int Transport_Type_Id, int POL_ID, int Sales_Person_Id, string BLNumber, DateTime BLReceivedDate, int UserId, string HouseBLNumber, int KAMID, string JoRemarks, int JoTypeId, int FileId, string IGMNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string berthingdateformat = berthingdate.ToString("yyyy-MM-dd HH:mm");
                string bLReceivedDateformat = BLReceivedDate.ToString("yyyy-MM-dd HH:mm");

                if (IGMNo == null)
                {
                    IGMNo = "";
                }
                if (berthingdateformat == "0001-01-01 00:00")
                {
                    berthingdateformat = "1900-01-01 00:00:00.000";
                    // berthingdateformat = null;

                }

                if (bLReceivedDateformat == "0001-01-01 00:00")
                {
                    bLReceivedDateformat = "1900-01-01 00:00:00.000";
                    // berthingdateformat = null;

                }

                string HBL = HouseBLNumber;
                sub_ConnectDB();


                using (SqlCommand cmd = new SqlCommand("USP_INSERT_IMPORT_BLM", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JonoNew", JONo);
                    cmd.Parameters.AddWithValue("@AgentID", AgentID);
                    cmd.Parameters.AddWithValue("@SLID", SLID);
                    cmd.Parameters.AddWithValue("@shipperid", shipperid);
                    cmd.Parameters.AddWithValue("@Importerid", Importerid);
                    cmd.Parameters.AddWithValue("@Chaid", Chaid);
                    // cmd.Parameters.AddWithValue("@ViaNo", Convert.ToString(ViaNo));
                    if (ViaNo != null)
                    {
                        cmd.Parameters.AddWithValue("@ViaNo", Convert.ToString(ViaNo));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ViaNo", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@VesselID", VesselID);
                    cmd.Parameters.AddWithValue("@PortID", Convert.ToInt32(PortID));
                    cmd.Parameters.AddWithValue("@File_Status_Id", File_Status_Id);
                    cmd.Parameters.AddWithValue("@berthingdate", Convert.ToDateTime(berthingdateformat).ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Haulage_Type_Id", Haulage_Type_Id);
                    cmd.Parameters.AddWithValue("@Transport_Type_Id", Transport_Type_Id);
                    cmd.Parameters.AddWithValue("@POL_ID", POL_ID);
                    cmd.Parameters.AddWithValue("@Sales_Person_Id", Sales_Person_Id);
                    cmd.Parameters.AddWithValue("@BLNumber", BLNumber);
                    cmd.Parameters.AddWithValue("@BLReceivedDate", Convert.ToDateTime(bLReceivedDateformat).ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@ADDEDBY", UserId);

                    cmd.Parameters.AddWithValue("@HouseBLNumber", Convert.ToString(HouseBLNumber));
                    cmd.Parameters.AddWithValue("@KAM", Convert.ToInt32(KAMID));
                    cmd.Parameters.AddWithValue("@joRemarks", Convert.ToString(JoRemarks));
                    cmd.Parameters.AddWithValue("@JoTypeId", Convert.ToInt32(JoTypeId));
                    cmd.Parameters.AddWithValue("@FIleId", Convert.ToInt32(FileId));
                    cmd.Parameters.AddWithValue("@IGMNo", Convert.ToString(IGMNo));
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                string error = "Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + JONo;
                // AddErrorLog("Error occurred. Error details: " + ex.Message.Replace("'", "") + "for" + viewModel.JONo);
                sub_ExecuteNonQuery("Insert into ErrorLog (Errorlog) values ( '" + error + "')");
            }
            return dt;
        }

        public DataTable DataTableAddTwoTypeTable(string SP_Name, Dictionary<object, object> lstparam, DataTable dt1, string typetable, string parameter, DataTable dt2, string typetable1, string parameter1)
        {

            DataTable dt = new DataTable();
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {
                    //SqlParameter param = new SqlParameter("@" + item.Key, item.Value);
                    ////param.ParameterName = "@"+item.Key ;
                    ////param.Value = item.Value;
                    //param.SqlDbType = SqlDbType.Int;
                    //cmd.Parameters.Add(param );
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt1 != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = parameter;
                    param.Value = dt1;
                    param.TypeName = typetable;
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }
                if (dt2 != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = parameter1;
                    param.Value = dt2;
                    param.TypeName = typetable1;
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }
                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(ds);
                //  i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            return dt;

        }

        public DataTable AddAttachment(string Description, string MailID, byte[] attachByte, int companyid, string contentType)
        {

            sub_ConnectDB();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("USP_CompanyTicketInsertDetails", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@MailID", MailID);
                cmd.Parameters.AddWithValue("@Attachment", attachByte);
                cmd.Parameters.AddWithValue("@companyid", companyid);
                cmd.Parameters.AddWithValue("@contentType", contentType);
                cmd.Parameters.AddWithValue("@Subject", contentType);


                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            return dt;
        }
        public DataTable SaveImportIGMDetails(string SP_Name, Dictionary<object, object> lstparam, DataTable dt)
        {

            DataTable dt1 = new DataTable();
            try
            {
                sub_ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in lstparam)
                {

                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                if (dt != null)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@PT_SaveIGMDetails";
                    param.Value = dt;
                    param.TypeName = "PT_SaveIGMDetails";
                    param.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(param);
                }


                cmd.CommandText = SP_Name;
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt1);

            }
            catch (Exception ex)
            {
            }
            return dt1;
        }


    }
}
